using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics; 

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos; 
using DllFarmaciaSoft;

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.Properties; 

namespace Dll_MA_IFacturacion.Configuracion
{
    public partial class FrmFactEmpresas : FrmBaseExt 
    {
        private enum Cols_Detalle
        {
            IdEmpresa = 0, IdEstado = 1, IdSucursal = 2,
            Año = 3, Aprobacion = 4, Serie = 5, Documento = 6, FolioInicio = 7, FolioFinal = 8, FolioUltimo = 8
        }

        private enum Cols
        {
            Año = 1, Aprobacion = 2, Serie = 3, IdTipoDocto = 4, TipoDocumento = 5, 
            Documento = 6, FolioInicio = 7, FolioFinal = 8, FolioUltimo = 9 
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query;
        clsListView listRegimen;
        clsLeer TiposDeDocumentos = new clsLeer(); 

        OpenFileDialog openDialog = new OpenFileDialog();
        SaveFileDialog saveDialog = new SaveFileDialog();
        FileInfo fInfo;
        FileInfo fInfo_Certificado;
        FileInfo fInfo_Key;

        clsAyudas_CFDI Ayuda;
        clsConsultas_CFDI Consulta; 

        bool bCertificadosCargados = false;
        string sInfo_Certificado = "";
        string sInfo_Key = "";
        string sInfo_Pfx = "";
        string sCertificado = "";
        string sLlavePrivada = "";
        string sPfx = "";
        string sFileGnPFX = "Generar PFX.exe"; 

        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();

        string sImagenLogo = "";
        string sNombreLogo = "";
        byte[] byteimagen;


        public FrmFactEmpresas()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            Consulta = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            listRegimen = new clsListView(lstRegimen); 

            CargarEmpresas();
            Cargar_PACs(); 
        }

        #region Form 
        private void FrmFactEmpresas_Load(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        } 
        #endregion Form 

        #region Botones 
        private void InicializaToolBar(bool Guardar, bool Ejecutar)
        {
            btnGuardar.Enabled = Guardar;
            btnEjecutar.Enabled = Ejecutar; 
        }

        private void InicializaPantalla()
        {
            Fg.IniciaControles();
            InicializaToolBar(false, true);

            btnConsultarTimbresDisponibles.Enabled = false;
            lblConsultarFoliosDisponibles.Visible = false; 
            tabOpcionesConfiguracion.SelectTab(0);
            listRegimen.LimpiarItems(); 
            //////dtpDesde.Enabled = false;
            //////dtpDesdeHora.Enabled = false;
            //////dtpHasta.Enabled = false;
            //////dtpHastaHora.Enabled = false; 

            cboEmpresas.Focus(); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializaPantalla(); 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaDatos())
            {
                GuardarInformacion(); 
            } 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (cboEmpresas.SelectedIndex == 0)
            {
                General.msjUser("No ha seleccionado una empresa válida, verifique."); 
                cboEmpresas.Focus(); 
            }
            else
            {
                CargarInformacionEmpresa(); 
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private bool GuardarInformacion()
        {
            bool bRegresa = false;

            if (!cnn.Abrir())
            {
                General.msjErrorAlAbrirConexion(); 
            } 
            else
            {
                cnn.IniciarTransaccion();

                //// Proceso de configuracion 
                bRegresa = GuardarEmpresa(); 

                if (!bRegresa)
                {
                    Error.GrabarError(leer, "");
                    cnn.DeshacerTransaccion();
                    General.msjError("Ocurrió un error al guardar la configuración de factura electrónica."); 
                }
                else
                {
                    cnn.CompletarTransaccion(); 
                    General.msjUser("Se guardó satisfactoriamente la configuración de factura electrónica.");
                    InicializaPantalla(); 
                }

                cnn.Cerrar(); 
            }

            return bRegresa; 
        }

        private bool GuardarEmpresa()
        {

            bool bRegresa = false;
            string sSql = string.Format("");
            int iEsDomEmision =  0;

            int iEsPersonaFisica = 0;
            int iPublicoGeneral_AplicaIva = 0;

            sSql = string.Format("Exec spp_Mtto_CFDI_Emisores @IdEmpresa = '{0}', @NombreFiscal = '{1}', @NombreComercial = '{2}', @RFC = '{3}', " +
                " @Telefonos = '{4}', @Fax = '{5}', @Email = '{6}', @DomExpedicion_DomFiscal = '{7}', " +
                " @Pais = '{8}', @Estado = '{9}', @Municipio = '{10}', @Colonia = '{11}', @Calle = '{12}', @NoExterior = '{13}', @NoInterior = '{14}', " +
                " @CodigoPostal = '{15}', @Referencia = '{16}', " +
                " @EPais = '{17}', @EEstado = '{18}', @EMunicipio = '{19}', @EColonia = '{20}', @ECalle = '{21}', " +
                " @ENoExterior = '{22}', @ENoInterior = '{23}', @ECodigoPostal = '{24}', @EReferencia = '{25}', " +
                " @Status = '{26}', @EsPersonaFisica = '{27}', @PublicoGeneral_AplicaIva = '{28}' ",
                cboEmpresas.Data, txtRazonSocial.Text.Trim(), txtRazonSocial.Text.Trim(),
                txtRFC.Text.Trim(), "", "", "email_general", iEsDomEmision,
                txtD_Pais.Text.Trim(),
                txtD_Estado.Text, txtD_Municipio.Text, txtD_Colonia.Text,
                txtD_Calle.Text.Trim(), txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(),
                txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(),
                txtD_Pais.Text.Trim(),
                txtD_Estado.Text, txtD_Municipio.Text, txtD_Colonia.Text,
                txtD_Calle.Text.Trim(), txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(),
                txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(),
                "A", iEsPersonaFisica, iPublicoGeneral_AplicaIva);

            bRegresa = leer.Exec(sSql); 
            if (bRegresa)
            {
                bRegresa = GuardarPAC(); 
            }

            return bRegresa; 
        }

        private bool GuardarPAC()
        {
            bool bRegresa = false;
            string sSql = string.Format("");
            int iEnProduccion = chkEnProduccion.Checked ? 1 : 0;

            sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_PAC @IdEmpresa = '{0}', @IdPAC = '{1}', " + 
                " @Usuario = '{2}', @Password = '{3}', @EnProduccion = '{4}' ",
                cboEmpresas.Data, cboPAC.Data, txtUsuario.Text.Trim(), txtPasswordPAC.Text.Trim(), iEnProduccion );

            bRegresa = leer.Exec(sSql);
            if (bRegresa)
            {
                bRegresa = GuardarCertificado_Key();
            }

            return bRegresa;
        }

        ////private bool GuardarDomicilioFiscal()
        ////{
        ////    bool bRegresa = true;
        ////    ////string sSql = string.Format(""); 

        ////    ////sSql = string.Format("Exec spp_Mtto_FACT_CFD_Domicilios @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
        ////    ////    " @Pais = '{3}', @Estado = '{4}', @Municipio = '{5}', @Localidad = '{6}', @Colonia = '{7}', " + 
        ////    ////    " @Calle = '{8}', @NoExterior = '{9}', @NoInterior = '{10}', @CodigoPostal = '{11}', @Referencia = '{12}', @Opcion = '{13}'  ",
        ////    ////    cboEmpresas.Data, "00", "0000", txtD_Pais.Text.Trim(), txtD_Estado.Text.Trim(), txtD_Municipio.Text.Trim(),  
        ////    ////    txtD_Localidad.Text.Trim(), txtD_Colonia.Text.Trim(), txtD_Calle.Text.Trim(), 
        ////    ////    txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(), 1);

        ////    ////bRegresa = leer.Exec(sSql); 
        ////    if (bRegresa)
        ////    {
        ////        bRegresa = GuardarCertificado_Key();
        ////    }

        ////    return bRegresa;
        ////}

        private bool GuardarCertificado_Key()
        {
            bool bRegresa = true; 
            if (bCertificadosCargados)
            {
                txtNoCertificado.Text = GetNumeroDeCertificado();
            }

            string sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_Certificados @IdEmpresa = '{0}', " +
                " @NumeroDeCertificado = '{1}', @NombreCertificado = '{2}', @Certificado = '{3}', " +
                " @NombreLlavePrivada = '{4}', @LlavePrivada = '{5}', " +
                " @PasswordPublico = '{6}', @NombreCertificadoPfx = '{7}', @CertificadoPfx = '{8}'  ",
                cboEmpresas.Data, txtNoCertificado.Text, sInfo_Certificado, sCertificado, sInfo_Key, sLlavePrivada, 
                txtPWD1.Text.Trim(), sInfo_Pfx, sPfx );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                ////bRegresa = GuardarSeries();
                bRegresa = Guardar_Regimenes(); 
            }

            return bRegresa; 
        }

        ////private bool GuardarSeries()
        ////{
        ////    bool bRegresa = true;
        ////    string sSql = string.Format("");
        ////    string sAño = "";
        ////    string sNumAprobacion = "";
        ////    string sSerie = "";
        ////    string sIdTipoDocto = ""; 
        ////    string sNombreDocumento = ""; 
        ////    string sFolioInicial = "";
        ////    string sFolioFinal = "";
        ////    string sStatus = "";

        ////    for (int i = 1; i <= lvwFoliosSeries.Items.Count; i++)  
        ////    {
        ////        sAño = list.GetValue(i, (int)Cols.Año);
        ////        sNumAprobacion = list.GetValue(i, (int)Cols.Aprobacion);
        ////        sSerie = list.GetValue(i, (int)Cols.Serie);
        ////        sIdTipoDocto = list.GetValue(i, (int)Cols.IdTipoDocto);
        ////        sNombreDocumento = list.GetValue(i, (int)Cols.Documento);

        ////        sFolioInicial = list.GetValue(i, (int)Cols.FolioInicio);
        ////        sFolioFinal = list.GetValue(i, (int)Cols.FolioFinal);
        ////        sStatus = list.GetValue(i, (int)Cols.Serie); 

        ////        sSql = string.Format("Exec spp_Mtto_FACT_CFD_SeriesFolios @IdEmpresa = '{0}', @AñoAprobacion = '{1}', @NumAprobacion = '{2}', " +
        ////            " @Serie = '{3}', @IdTipoDocumento = '{4}', @NombreDocumento = '{5}', @FolioInicial = '{6}', @FolioFinal = '{7}', @FolioUtilizado = '{8}', @Status = '{9}' ",
        ////            cboEmpresas.Data, sAño, sNumAprobacion, sSerie, sIdTipoDocto, sNombreDocumento, sFolioInicial, sFolioFinal, 0, 'A');

        ////        if (!leer.Exec(sSql)) 
        ////        {
        ////            bRegresa = false;
        ////            break; 
        ////        }
        ////    }

        ////    if (bRegresa)
        ////    {
        ////        bRegresa = Guardar_Logo(); 
        ////    }

        ////    return bRegresa;
        ////}

        private bool Guardar_Regimenes()
        {
            bool bRegresa = true;
            string sSql = ""; // string.Format("Delete From CFDI_Emisores_Regimenes Where IdEmisor = '{0}' \n\n", sIdEmisor);
            string sIdRegimen = "";

            for (int i = 1; i <= lstRegimen.Items.Count; i++)
            {
                if (lstRegimen.Items[i - 1].Checked)
                {
                    sIdRegimen = lstRegimen.Items[i - 1].Tag.ToString();
                    sSql += string.Format("Exec spp_Mtto_CFDI_Emisores_Regimenes @IdEmpresa = '{0}', @IdRegimen = '{1}' \n", cboEmpresas.Data, sIdRegimen);
                }
            }

            if (sSql != "")
            {
                sSql = string.Format("Delete From CFDI_Emisores_Regimenes Where IdEmpresa = '{0}' \n\n", cboEmpresas.Data) + sSql;
            }


            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                bRegresa = Guardar_Logo(); 
            }

            return bRegresa;
        }

        private bool Guardar_Logo()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_Logos  @IdEmpresa = '{0}', @Logo = '{1}' ",
                  cboEmpresas.Data, sImagenLogo);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }

            if (bRegresa)
            {
                //bRegresa = Guardar_Regimenes();
            }

            return bRegresa;
        }

        private bool validaDatos()
        {
            bool bRegresa = true;

            if (cboEmpresas.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado una empresa válida, verifique.");
                cboEmpresas.Focus(); 
            }

            if (bRegresa && txtRazonSocial.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Razón social no debe ser vacia, verifique."); 
                txtRazonSocial.Focus(); 
            }

            if (bRegresa && txtRFC.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El RFC no debe ser vacio, verifique.");
                txtRFC.Focus();
            }

            if (bRegresa && cboPAC.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjUser("No ha seleccionado un proveedor de timbrado, verifique.");
                cboPAC.Focus();
            }

            if (bRegresa && txtUsuario.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado un Usuario válido para el servicio de timbrado, verifique.");
                txtUsuario.Focus(); 
            }

            if (bRegresa && txtPasswordPAC.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("No ha capturado el Password de la cuenta de timbrado.");
                txtPasswordPAC.Focus();
            }

            if (bRegresa)
            {
                if ( txtPasswordPAC.Text.Trim() != txtPasswordPACConfirmacion.Text.Trim()) 
                {
                    bRegresa = false;
                    General.msjUser("El password no coincide con el password confirmación, verifique.");
                    txtPasswordPAC.Focus();
                }
            }


            ////if (bRegresa && txtNombreProveedor.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Nombre del proveedor del servicio de timbrado, verifique.");
            ////    txtNombreProveedor.Focus();
            ////}

            ////if (bRegresa && txtDireccionUrl.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado la Dirección del servicio de timbrado, verifique.");
            ////    txtDireccionUrl.Focus();
            ////}

            ////if (bRegresa && txtKeyLicencia.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("No ha capturado el Key-Licencia para el servicio de timbrado, verifique.");
            ////    txtKeyLicencia.Focus(); 
            ////}

            if (bRegresa)
            {
                bRegresa = validaDatosDomicilio(); 
            }

            return bRegresa;
        }

        private bool validaDatosDomicilio()
        {
            bool bRegresa = true;

            if (txtD_Pais.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Pais no debe ser vacio, verifique.");
                txtD_Pais.Focus(); 
            }

            if (bRegresa && txtD_Estado.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Estado no debe ser vacio, verifique.");
                txtD_Estado.Focus();
            }

            if (bRegresa && txtD_Municipio.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Municipio no debe ser vacio, verifique.");
                txtD_Municipio.Focus();
            }

            //////if (bRegresa && txtD_Localidad.Text.Trim() == "")
            //////{
            //////    bRegresa = false;
            //////    General.msjUser("La Localidad no debe ser vacio, verifique.");
            //////    txtD_Localidad.Focus();
            //////}

            if (bRegresa && txtD_Colonia.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Colonia no debe ser vacio, verifique.");
                txtD_Colonia.Focus();
            }

            if (bRegresa && txtD_Calle.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Calle no debe ser vacio, verifique.");
                txtD_Calle.Focus();
            }

            if (bRegresa && txtD_NoExterior.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El número exterior no debe ser vacio, verifique.");
                txtD_NoExterior.Focus();
            }

            ////if (bRegresa && txtD_NoInterior.Text.Trim() == "")
            ////{
            ////    bRegresa = false;
            ////    General.msjUser("El número interior no debe ser vacio, verifique.");
            ////    txtD_NoInterior.Focus();
            ////}

            if (bRegresa && txtD_CodigoPostal.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("El Codigo postal no debe ser vacio, verifique.");
                txtD_CodigoPostal.Focus();
            }

            return bRegresa;
        }

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEmpresas.Add(query.Empresas("CargarEmpresas()"), true, "IdEmpresa", "Nombre");
            ////TiposDeDocumentos.DataSetClase = query.CFDI_TipoDeDocumentos("", "CargarEmpresas()");   
            cboEmpresas.SelectedIndex = 0; 
        }

        private void Cargar_PACs()
        {
            string sSql = "select IdPAC, NombrePAC From CFDI_PACs (NoLock) Order By IdPac ";
            cboPAC.Clear();
            cboPAC.Add();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Cargar_PACs()");
                General.msjError("Ocurrió un error al cargar la lista de PAC(s)");
            }
            else
            {
                cboPAC.Add(leer.DataSetClase, true, "IdPAC", "NombrePAC");
            }

            cboPAC.SelectedIndex = 0;
        }

        private void CargarInformacionEmpresa()
        {
            string sSql = string.Format("Exec spp_CFDI_Emisores_Configuracion  @IdEmpresa = '{0}' ", cboEmpresas.Data);  
            string sMensaje = "No se encontro configuración de factura electrónica para la empresa seleccionada.\n\n" + 
                "¿ Desea generar el registro ? "; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarInformacionEmpresa()");
                General.msjError("Ocurrió un error al obtener la información de la Empresa."); 
            }
            else
            {
                if (!leer.Leer())
                {
                    InicializaToolBar(false, true);
                    if (General.msjConfirmar(sMensaje) == DialogResult.Yes)
                    {
                        InicializaToolBar(true, false);
                        cboEmpresas.Enabled = false;
                        txtRazonSocial.Focus(); 
                    }
                }
                else 
                {
                    InicializaToolBar(true, false); 
                    cboEmpresas.Enabled = false;
                    leer.RenombrarTabla(1, "Empresa");
                    leer.RenombrarTabla(2, "Domicilio");
                    leer.RenombrarTabla(3, "Certificados");
                    leer.RenombrarTabla(4, "Series");
                    leer.RenombrarTabla(5, "Logos");
                    leer.RenombrarTabla(6, "Regimenes");

                    CargarDatosEmpresa(leer.Tabla("Empresa"));
                    CargarDatosDomicilio(leer.Tabla("Domicilio"));
                    CargarDatosCertificados(leer.Tabla("Certificados"));
                    ////CargarDatosSeries(leer.Tabla("Series"));
                    CargarDatosLogos(leer.Tabla("Logos"));
                    CargarDatosRegimenes(leer.Tabla("Regimenes"));

                    btnConsultarTimbresDisponibles.Enabled = true;
                    txtRazonSocial.Focus(); 
                } 
            }
        }

        private void CargarDatosEmpresa(DataTable Datos) 
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;

            datosLeer.Leer(); 
            txtRazonSocial.Text = datosLeer.Campo("Nombre");
            txtRFC.Text = datosLeer.Campo("RFC");

            cboPAC.Data = datosLeer.Campo("IdPAC");
            txtUsuario.Text = datosLeer.Campo("Usuario");
            txtPasswordPAC.Text = datosLeer.Campo("KeyLicencia");
            txtPasswordPACConfirmacion.Text = txtPasswordPAC.Text;
            txtDireccionUrl.Text = datosLeer.Campo("DireccionUrl");
            chkEnProduccion.Checked = datosLeer.CampoBool("EnProduccion");
            chkEnProduccion.Enabled = !datosLeer.CampoBool("EnProduccion");

            if (!chkEnProduccion.Enabled)
            {
                chkEnProduccion.Enabled = DtGeneral.EsAdministrador;
            }

            //////txtKeyLicencia.Text = datosLeer.Campo("KeyLicencia");
            //////txtNombreProveedor.Text = datosLeer.Campo("NombreProveedor");
            ////////txtDireccionUrl.Text = datosLeer.Campo("DireccionUrl");
            //////txtTelefonos.Text = datosLeer.Campo("Telefonos");
            //////// IdEmpresa, Nombre, RFC, KeyLicencia, NombreProveedor, DireccionUrl, Telefonos, Status
        }

        private void CargarDatosDomicilio(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;

            datosLeer.Leer();
            txtD_Pais.Text = datosLeer.Campo("Pais");
            txtD_Estado.Text = datosLeer.Campo("IdEstado");
            lblD_Estado.Text = datosLeer.Campo("Estado");
            txtD_Municipio.Text = datosLeer.Campo("IdMunicipio");
            lblD_Municipio.Text = datosLeer.Campo("Municipio");
            txtD_Colonia.Text = datosLeer.Campo("IdColonia");
            lblD_Colonia.Text = datosLeer.Campo("Colonia");
            txtD_Calle.Text = datosLeer.Campo("Calle");
            txtD_Referencia.Text = datosLeer.Campo("Referencia");
            txtD_NoExterior.Text = datosLeer.Campo("NoExterior");
            txtD_NoInterior.Text = datosLeer.Campo("NoInterior");
            txtD_CodigoPostal.Text = datosLeer.Campo("CodigoPostal");
            // IdEmpresa, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
        }

        private void CargarDatosCertificados(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos; 
            datosLeer.Leer();

            txtNoCertificado.Text = datosLeer.Campo("NumeroDeCertificado");
            sInfo_Certificado = datosLeer.Campo("NombreCertificado");
            txtCer.Text = sInfo_Certificado;
            sCertificado = datosLeer.Campo("Certificado");

            sInfo_Key = datosLeer.Campo("NombreLlavePrivada");
            txtKey.Text = sInfo_Key;
            sLlavePrivada = datosLeer.Campo("LlavePrivada");
            txtPWD1.Text = datosLeer.Campo("PasswordPublico");
            txtPWD2.Text = txtPWD1.Text;

            sInfo_Pfx = datosLeer.Campo("NombreCertificadoPfx");
            txtPFX.Text = sInfo_Pfx;
            sPfx = datosLeer.Campo("CertificadoPfx");

            
        }

        ////private void CargarDatosSeries(DataTable Datos)
        ////{
        ////    clsLeer datosLeer = new clsLeer();
        ////    datosLeer.DataTableClase = Datos;
        ////    datosLeer.Leer();

        ////    list.CargarDatos(datosLeer.DataSetClase, false, false); 
        ////}

        private void CargarDatosRegimenes(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;
            //datosLeer.Leer();

            listRegimen.LimpiarItems();
            //listRegimen.CargarDatos(datosLeer.DataSetClase, false, false); 
            while (datosLeer.Leer())
            {
                ListViewItem item = this.lstRegimen.Items.Add("");
                item.Checked = datosLeer.CampoBool("Activo");
                item.Tag = datosLeer.Campo(2);
                item.SubItems.Add(datosLeer.Campo(2).ToString());
                item.SubItems.Add(datosLeer.Campo(3).ToString());
            }
        }

        private void CargarDatosLogos(DataTable Datos)
        {
            byte[] bytes;
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;
            datosLeer.Leer();

            try
            {
                pcLogo.Image = null;
                bytes = datosLeer.CampoByte("Logo");
                byteimagen = bytes;
                sImagenLogo = Fg.ConvertirStringB64(byteimagen);
                //sImagenLogo = Fg.conv

                IntPtr intr = new IntPtr(0);

                MemoryStream ms = new MemoryStream(bytes);
                Image returnImage = Image.FromStream(ms);

                pcLogo.Image = returnImage;

            }
            catch { }
        }

        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRFC.Text = "";
            if (cboEmpresas.SelectedIndex != 0)
            {
                lblRFC.Text = cboEmpresas.ItemActual.GetItem("RFC");  
            }
        }
        #endregion Eventos  

        #region Cerfiticados y Llaves 
        private void btnNoCertificado_Click(object sender, EventArgs e)
        {
            txtNoCertificado.Text = GetNumeroDeCertificado();
        }

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            openDialog.Title = "Archivos de Certificado";
            openDialog.Filter = "Archivo de Certificado | *.cer";

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fInfo_Certificado = new FileInfo(openDialog.FileName);
                txtCer.Text = fInfo_Certificado.FullName;
                sInfo_Certificado = fInfo_Certificado.Name;
                //lblCertificadoGuardado.Text = fInfo.Name;

                Certificado_Key.FileName = openDialog.FileName;
                sCertificado = Certificado_Key.Codificar();
            }
        }

        private void btnLlavePrivada_Click(object sender, EventArgs e)
        {
            openDialog.Title = "Archivos de Llave Privada";
            openDialog.Filter = "Archivo de Llave Privada | *.key";

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fInfo_Key = new FileInfo(openDialog.FileName);
                txtKey.Text = fInfo_Key.FullName;
                sInfo_Key = fInfo_Key.Name;
                //lblLlavePrivadaGuardada.Text = fInfo.Name;

                Certificado_Key.FileName = openDialog.FileName;
                sLlavePrivada = Certificado_Key.Codificar();
            }
        }

        private void btnPFX_Click(object sender, EventArgs e)
        {
            openDialog.Title = "Archivos Pfx";
            openDialog.Filter = "Archivo Pfx | *.pfx";

            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fInfo_Key = new FileInfo(openDialog.FileName);
                txtPFX.Text = fInfo_Key.FullName;
                sInfo_Pfx = fInfo_Key.Name;
                //lblLlavePrivadaGuardada.Text = fInfo.Name;

                Certificado_Key.FileName = openDialog.FileName;
                sPfx = Certificado_Key.Codificar();
                //LeerCertificado();
            }
        }

        private void btnGenerarPFX_Click(object sender, EventArgs e)
        {
            if (validarDatos_Cer_Key())
            {
                generarPFX();
            }
        }

        private void generarPFX()
        {
            Process gnPFX = new Process();
            string sRuta = GetParametrosPFX();
            int iPfx = -1;
            string sMsj = ""; 

            sRuta = DtIFacturacion.RutaCFDI + @"\Tools";
            DtIFacturacion.CrearDirectorio(sRuta);

            ////if (!File.Exists(sRuta)) 
            ////{
            ////    Fg.ConvertirBytesEnArchivo(sFileGnPFX, sRuta, Resources.Generar_PFX, true);
            ////}

            FileInfo f = new FileInfo(txtCer.Text);
            string sFilePfx = f.DirectoryName + "" + f.Name;
            sFilePfx = f.FullName.Replace(f.Extension, "") + "_pfx.pfx";

            //iPfx = CryptoSysPKI.Pfx.MakeFile(sFilePfx, txtCer.Text + "w", txtKey.Text, txtPWD1.Text.Trim(), f.Name.Replace(f.Extension, ""), false);

            sRuta = sRuta + @"\" + sFileGnPFX;
            gnPFX.StartInfo.FileName = sRuta;
            gnPFX.StartInfo.Arguments = GetParametrosPFX();
            gnPFX.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            try
            {
                gnPFX.Start();
                gnPFX.WaitForExit();
            }
            catch (Exception ex)
            {
                sMsj = ex.Message; 
                General.msjError("Ocurrió un error al generar el archivo PFX.");
            }
        }

        private string GetParametrosPFX()
        {
            string sRegresa = "";
            string sFilePfx = "";
            FileInfo f = new FileInfo(txtCer.Text);
            sFilePfx = f.DirectoryName + "" + f.Name;
            sFilePfx = f.FullName.Replace(f.Extension, "") + "_pfx.pfx";

            sRegresa += string.Format(" GS ");
            sRegresa += string.Format(" C{0} ", Comillas(txtCer.Text));
            sRegresa += string.Format(" K{0} ", Comillas(txtKey.Text));
            sRegresa += string.Format(" P{0} ", Comillas(sFilePfx));
            sRegresa += string.Format(" p{0} ", Comillas(txtPWD1.Text.Trim()));
            return sRegresa;
        }

        private string Comillas(string Valor)
        {
            string sRegresa = Fg.Comillas() + Valor + Fg.Comillas();

            return sRegresa;
        }

        private bool validarDatos_Cer_Key()
        {
            bool bRegresa = true;

            if (this.txtNoCertificado.Text.Trim() == "")
            {
                General.msjAviso("Escriba el número de certificado.");
                this.txtNoCertificado.Focus();
                bRegresa = false;
            }

            if (this.txtNoCertificado.Text.Trim().Length != 20)
            {
                General.msjAviso("El número de certificado debe contener 20 caracteres.");
                this.txtNoCertificado.Focus();
                bRegresa = false;
            }

            if (this.txtKey.Text.Trim() == "")
            {
                General.msjAviso("Seleccione la ubicación de la llave privada.");
                this.txtKey.Focus();
                bRegresa = false;
            }

            if ((this.txtPWD1.Text == "") && (this.txtPWD2.Text == ""))
            {
                General.msjAviso("La contraseña no puede estar vacía.");
                this.txtPWD1.Focus();
                bRegresa = false;
            }

            if (this.txtPWD1.Text != this.txtPWD2.Text)
            {
                General.msjAviso("La contraseña no coincide.");
                this.txtPWD1.Focus();
                bRegresa = false;
            }

            return bRegresa;
        }

        private string GetNumeroDeCertificado()
        {
            string sRegresa = "";
            try
            {
                string sRuta = Application.StartupPath;
                string sFileC = txtCer.Text;
                string sFileK = txtKey.Text;

                SelloDigital sello = new SelloDigital(sFileC, sFileK, txtPFX.Text);
                sello.ObtenerDatos();
                sRegresa = sello.NumeroDeCertificado;

                ////x.LiberarLibreria();
                sello = null;
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message; 
            }

            return sRegresa;
        }
        #endregion Cerfiticados y Llaves 

        #region Series y Folios
        private void agregarSeries_Click(object sender, EventArgs e)
        {
            ////CargarSeries(1);
        }

        private void modificarSeries_Click(object sender, EventArgs e)
        {
            ////CargarSeries(2);
        }

        //////private void CargarSeries(int Opcion)
        //////{
        //////    FrmSeries f = new FrmSeries();
        //////    f.TiposDeDocumentos = TiposDeDocumentos; 

        //////    if (Opcion == 1)
        //////    {
        //////        f.CargarPantalla();
        //////        if (f.Guardado)
        //////        {
        //////            ////ListViewItem itmX = lvwFoliosSeries.Items.Add(cboEmpresa.Data);
        //////            ////itmX.SubItems.Add(cboEstado.Data);
        //////            ////itmX.SubItems.Add(cboSucursal.Data);

        //////            ListViewItem itmX = lvwFoliosSeries.Items.Add(f.sAño);  
        //////            itmX.SubItems.Add(f.sAprobacion); 
        //////            itmX.SubItems.Add(f.sSerie);
        //////            itmX.SubItems.Add(f.sIdTipoDocto);
        //////            itmX.SubItems.Add(f.sNombreTipoDocto);
        //////            itmX.SubItems.Add(f.sNombreDocumento); 
        //////            itmX.SubItems.Add(f.sFolioInicio); 
        //////            itmX.SubItems.Add(f.sFolioFinal); 
        //////            itmX.SubItems.Add(f.sUltimoFolio); 
        //////        }
        //////    }
        //////    else
        //////    {
        //////        string sAño = "", sAprobacion = "", sSerie = "", sIdTipoDocto = "", sNombreTipoDocto = ""; 
        //////        string sNombreDocumento = "", sFolioInicio = "", sFolioFinal = "", sUltimoFolio = "";

        //////        try
        //////        {
        //////            sAño = list.GetValue((int)Cols.Año);
        //////            sAprobacion = list.GetValue((int)Cols.Aprobacion);
        //////            sSerie = list.GetValue((int)Cols.Serie);
        //////            sIdTipoDocto = list.GetValue((int)Cols.IdTipoDocto);
        //////            sNombreTipoDocto = list.GetValue((int)Cols.TipoDocumento);
        //////            sNombreDocumento = list.GetValue((int)Cols.Documento);
        //////            sFolioInicio = list.GetValue((int)Cols.FolioInicio);
        //////            sFolioFinal = list.GetValue((int)Cols.FolioFinal);
        //////            sUltimoFolio = list.GetValue((int)Cols.FolioUltimo);
        //////            f.CargarPantalla(sAño, sAprobacion, sSerie, sIdTipoDocto, sNombreDocumento, sFolioInicio, sFolioFinal, sUltimoFolio);

        //////            if (f.Guardado)
        //////            {
        //////                list.SetValue((int)Cols.Año, f.sAño);
        //////                list.SetValue((int)Cols.Aprobacion, f.sAprobacion);
        //////                list.SetValue((int)Cols.Serie, f.sSerie);
        //////                list.SetValue((int)Cols.IdTipoDocto, f.sIdTipoDocto);
        //////                list.SetValue((int)Cols.TipoDocumento, f.sNombreTipoDocto);

        //////                list.SetValue((int)Cols.Documento, f.sNombreDocumento); 
        //////                list.SetValue((int)Cols.FolioInicio, f.sFolioInicio);
        //////                list.SetValue((int)Cols.FolioFinal, f.sFolioFinal);
        //////                list.SetValue((int)Cols.FolioUltimo, f.sUltimoFolio); 
        //////            }
        //////        }
        //////        catch { }
        //////    }
        //////}
        #endregion Series y Folios

        private void tabOpcionesConfiguracion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sPage = "";

            switch (sPage)
            {
                case "tabDatosSucursal":
                    txtRazonSocial.Focus();
                    break;

                case "tabDomicilioFiscal":
                    txtD_Pais.Focus();
                    break; 
            }
        }

        #region Logo 
        private void btnLogo_Click(object sender, EventArgs e)
        {
            FormatosImagen formato = FormatosImagen.Ninguno;
            //byte[] byteimagen;
            string sFiltro = "(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF";
            string sIcoAux = "";
            MemoryStream ms = new MemoryStream();

            file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Seleccione la Imagen a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            file.Filter = sFiltro;

            if (file.ShowDialog() == DialogResult.OK)
            {
                sImagenLogo = "";
                sNombreLogo = "";
                FileInfo imagen = new FileInfo(file.FileName);
                sNombreLogo = imagen.Name;
                formato = (FormatosImagen)file.FilterIndex;

                MostrarFoto(file.FileName);

                //byteimagen = ConvertirImagenABytes(pcLogo.Image, formato);
                pcLogo.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byteimagen = ms.ToArray();

                sImagenLogo = Convert.ToBase64String(byteimagen, 0, byteimagen.Length);

            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        private void MostrarFoto(string RutaImagen)
        {
            IntPtr intr = new IntPtr(0);
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            //pcFotografia.ImageLocation = RutaImagen;
            pcLogo.Image = Image.FromFile(RutaImagen);

            pcLogo.Image = pcLogo.Image.GetThumbnailImage(pcLogo.Width, pcLogo.Height, myCallback, intr);
        }
        #endregion Logo 

        #region Geograficos
        #region Domicilio fiscal
        private void CargarGeograficos(int Tipo)
        {
            if (Tipo == 1)
            {
                General.CargarPantalla("FrmEstados", "Ortopedic Control", this, true);
            }

            if (Tipo == 2)
            {
                General.CargarPantalla("FrmMunicipios", "Ortopedic Control", this, true);
            }

            if (Tipo == 3)
            {
                General.CargarPantalla("FrmColonias", "Ortopedic Control", this, true);
            }
        }

        private void btnD_Estados_Click(object sender, EventArgs e)
        {
            CargarGeograficos(1);
        }

        private void btnD_Municipios_Click(object sender, EventArgs e)
        {
            CargarGeograficos(2);
        }

        private void btnD_Colonias_Click(object sender, EventArgs e)
        {
            CargarGeograficos(3);
        }

        private void CargarInfD_Estado()
        {
            txtD_Estado.Text = leer.Campo("IdEstado");
            lblD_Estado.Text = leer.Campo("Descripcion");
        }

        private void CargarInfD_Municipio()
        {
            txtD_Municipio.Text = leer.Campo("IdMunicipio");
            lblD_Municipio.Text = leer.Campo("Descripcion");
        }

        private void CargarInfD_Colonia()
        {
            txtD_Colonia.Text = leer.Campo("IdColonia");
            lblD_Colonia.Text = leer.Campo("Descripcion");
            txtD_CodigoPostal.Text = leer.Campo("CodigoPostal");
        }

        private void txtD_Estado_TextChanged(object sender, EventArgs e)
        {
            lblD_Estado.Text = "";
            txtD_Municipio.Text = "";
            lblD_Municipio.Text = "";
            lblD_Colonia.Text = "";
        }

        private void txtD_Estado_Validating(object sender, CancelEventArgs e)
        {
            if (txtD_Estado.Text != "")
            {
                leer.DataSetClase = Consulta.CFDI_Estados(txtD_Estado.Text, "txtD_Estado_KeyDown");
                if (!leer.Leer())
                {
                    txtD_Estado.Focus();
                }
                else
                {
                    CargarInfD_Estado();
                }
            }
        }

        private void txtD_Estado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayuda.CFDI_Estados("txtD_Estado_KeyDown");
                if (leer.Leer())
                {
                    CargarInfD_Estado();
                }
            }
        }

        private void txtD_Municipio_TextChanged(object sender, EventArgs e)
        {
            lblD_Municipio.Text = "";
            txtD_Colonia.Text = "";
            lblD_Colonia.Text = "";
        }

        private void txtD_Municipio_Validating(object sender, CancelEventArgs e)
        {
            if (txtD_Estado.Text.Trim() != "" && txtD_Municipio.Text.Trim() != "")
            {
                leer.DataSetClase = Consulta.CFDI_Municipios(txtD_Estado.Text, txtD_Municipio.Text, "txtD_Municipio_Validating");
                if (!leer.Leer())
                {
                    txtD_Municipio.Focus();
                }
                else
                {
                    CargarInfD_Municipio();
                }
            }
        }

        private void txtD_Municipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtD_Estado.Text != "")
                {
                    leer.DataSetClase = Ayuda.CFDI_Municipios(txtD_Estado.Text, "txtD_Estado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfD_Municipio();
                    }
                }
            }
        }

        private void txtD_Colonia_TextChanged(object sender, EventArgs e)
        {
            lblD_Colonia.Text = "";
            txtD_CodigoPostal.Text = "";
        }

        private void txtD_Colonia_Validating(object sender, CancelEventArgs e)
        {
            if (txtD_Municipio.Text.Trim() != "" && txtD_Colonia.Text != "")
            {
                leer.DataSetClase = Consulta.CFDI_Colonias(txtD_Estado.Text, txtD_Municipio.Text, txtD_Colonia.Text, "txtD_Colonia_Validating");
                if (!leer.Leer())
                {
                    txtD_Colonia.Focus();
                }
                else
                {
                    CargarInfD_Colonia();
                }
            }
        }

        private void txtD_Colonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (txtD_Municipio.Text != "")
                {
                    leer.DataSetClase = Ayuda.CFDI_Colonias(txtD_Estado.Text, txtD_Municipio.Text, "txtD_Estado_KeyDown");
                    if (leer.Leer())
                    {
                        CargarInfD_Colonia();
                    }
                }
            }
        }
        #endregion Domicilio fiscal 

        private void btnConsultarTimbresDisponibles_Click(object sender, EventArgs e)
        {
            btnConsultarTimbresDisponibles.Enabled = false; 
            string sTexto = "Timbres disponibles : ";

            lblConsultarFoliosDisponibles.Visible = true;
            lblConsultarFoliosDisponibles.Text = "Consultando información ...";
            Application.DoEvents();
            System.Threading.Thread.Sleep(200); 

            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.RFC_Emisor = lblRFC.Text;
            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.Usuario = txtUsuario.Text;
            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.Password = txtPasswordPAC.Text;
            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.Url = txtDireccionUrl.Text;
            Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.PAC = (PACs_Timbrado)(Convert.ToInt32("0" + cboPAC.Data));

            lblConsultarFoliosDisponibles.Text = string.Format("{0} {1}", sTexto, Dll_MA_IFacturacion.CFDI.Timbrar.clsTimbrar.ConsultarCreditos() );
            btnConsultarTimbresDisponibles.Enabled = true;
            Application.DoEvents();
            System.Threading.Thread.Sleep(100); 
        }
        #endregion Geograficos
    }
}
