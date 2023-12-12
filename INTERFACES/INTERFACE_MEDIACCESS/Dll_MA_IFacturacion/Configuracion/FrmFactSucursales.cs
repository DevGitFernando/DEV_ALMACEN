using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO; 

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

namespace Dll_MA_IFacturacion.Configuracion
{
    public partial class FrmFactSucursales : FrmBaseExt 
    {
        private enum Cols_Detalle
        {
            IdEmpresa = 0, IdEstado = 1, IdSucursal = 2,
            Año = 3, Aprobacion = 4, Serie = 5, FolioInicio = 6, FolioFinal = 7, FolioUltimo = 8, 
            Identificador = 9 
        }

        //////private enum Cols
        //////{
        //////    Asignado = 1, Bloqueado = 2, Año = 3, Aprobacion = 4, 
        //////    Serie = 5, TipoDocumento = 6, DescripcionDocumento = 7, 
        //////    FolioInicio = 8, FolioFinal = 9, FolioUltimo = 10,
        //////    Identificador = 11   
        //////}

        private enum Cols
        {
            Serie = 1, ClaveTipoDocumento = 2, TipoDocumento = 3, FolioInicio = 4, FolioFinal = 5, FolioUltimo = 6, Status = 7
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query; 
        clsListView list;
        clsLeer TiposDeDocumentos = new clsLeer();

        clsAyudas_CFDI Ayuda;
        clsConsultas_CFDI Consulta; 

        DataSet dtsEstados = new DataSet();
        DataSet dtsFarmacias = new DataSet(); 

        public FrmFactSucursales()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 

            Consulta = new clsConsultas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name, false);
            Ayuda = new clsAyudas_CFDI(General.DatosConexion, DtIFacturacion.DatosApp, this.Name, false);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);


            list = new clsListView(lvwFoliosSeries); 

            CargarEmpresas();

            TiposDeDocumentos.DataSetClase = Consulta.CFDI_TipoDeDocumentos("", "FrmEmisor_Load()");
        }

        #region Form 
        private void FrmFactSucursales_Load(object sender, EventArgs e)
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

            txtRazonSocial.Enabled = false;
            txtRFC.Enabled = false;
            txtD_CodigoPostal.Enabled = false; 

            tabOpcionesConfiguracion.SelectTab(0); 
            list.LimpiarItems(); 

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
                GuardarInformacion("A"); 
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
                CargarInformacion(); 
            }
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados 
        private bool GuardarInformacion(string Status)
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
                bRegresa = GuardarSucursal(Status); 

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

        private bool GuardarSucursal(string Status)
        {
            bool bRegresa = false;
            string sSql = string.Format("");
            int iEsDomEmision = 0;

            int iEsPersonaFisica = 0;
            int iPublicoGeneral_AplicaIva = 0;


            sSql = string.Format("Exec spp_Mtto_FACT_CFD_Sucursales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Nombre = '{3}', @RFC = '{4}', @Status = '{5}' ", 
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data,
                txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), Status);


            sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_Sucursales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @NombreFiscal = '{3}', @NombreComercial = '{4}', @RFC = '{5}', " + 
                " @Telefonos = '{6}', @Fax = '{7}', @Email = '{8}', @DomExpedicion_DomFiscal = '{9}', " 
                + " @Pais = '{10}', @Estado = '{11}', @Municipio = '{12}', @Colonia = '{13}', @Calle = '{14}', @NoExterior = '{15}', @NoInterior = '{16}', " + 
                " @CodigoPostal = '{17}', @Referencia = '{18}', @EPais = '{19}', @EEstado = '{20}', @EMunicipio = '{21}', @EColonia = '{22}', " + 
                " @ECalle = '{23}', @ENoExterior = '{24}', @ENoInterior = '{25}', @ECodigoPostal = '{26}', @EReferencia = '{27}', @Status = '{28}', " + 
                " @EsPersonaFisica = '{29}', @PublicoGeneral_AplicaIva = '{30}' ",
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, txtRazonSocial.Text.Trim(), txtRazonSocial.Text.Trim(),
                txtRFC.Text.Trim(), "", "", "email_general", iEsDomEmision,
                txtD_Pais.Text.Trim(),
                txtD_Estado.Text, txtD_Municipio.Text, txtD_Colonia.Text,
                txtD_Calle.Text.Trim(), txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(),
                txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(),
                txtD_Pais.Text.Trim(),
                txtD_Estado.Text, txtD_Municipio.Text, txtD_Colonia.Text,
                txtD_Calle.Text.Trim(), txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(),
                txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(),
                Status, iEsPersonaFisica, iPublicoGeneral_AplicaIva);


            bRegresa = leer.Exec(sSql); 
            if (bRegresa)
            {
                ////bRegresa = GuardarDomicilioFiscal();
                bRegresa = GuardarSeries();
            }

            return bRegresa; 
        }

        private bool GuardarDomicilioFiscal()
        {
            bool bRegresa = false;
            ////string sSql = string.Format(""); 

            ////sSql = string.Format("Exec spp_Mtto_FACT_CFD_Domicilios @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
            ////    " @Pais = '{3}', @Estado = '{4}', @Municipio = '{5}', @Localidad = '{6}', @Colonia = '{7}', " + 
            ////    " @Calle = '{8}', @NoExterior = '{9}', @NoInterior = '{10}', @CodigoPostal = '{11}', @Referencia = '{12}', @Opcion = '{13}'  ",
            ////    cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, txtD_Pais.Text.Trim(), txtD_Estado.Text.Trim(), txtD_Municipio.Text.Trim(),  
            ////    txtD_Localidad.Text.Trim(), txtD_Colonia.Text.Trim(), txtD_Calle.Text.Trim(), 
            ////    txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(), 2);

            ////bRegresa = leer.Exec(sSql);
            ////if (bRegresa)
            ////{
            ////    bRegresa = GuardarSeries(); 
            ////}
            return bRegresa;
        }

        private bool GuardarSeries()
        {
            bool bRegresa = true;
            string sSql = string.Format("");
            string sSerie = "";
            string sIdTipoDocto = "";
            string sFolioInicial = "";
            string sFolioFinal = "";
            string sFolioUtilizado = "";
            string sStatus = "";

            for (int i = 1; i <= list.Registros; i++) 
            {
                sSerie = list.GetValue(i, (int)Cols.Serie);
                sIdTipoDocto = list.GetValue(i, (int)Cols.ClaveTipoDocumento);
                sFolioInicial = list.GetValue(i, (int)Cols.FolioInicio);
                sFolioFinal = list.GetValue(i, (int)Cols.FolioFinal);
                sFolioUtilizado = list.GetValue(i, (int)Cols.FolioUltimo);
                sStatus = list.GetValue(i, (int)Cols.Status);
                ////sStatus = sStatus == "" ? "A" : "C";

                sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_SeriesFolios_Series " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', " + 
                    " @IdTipoDocumento = '{4}', @FolioInicial = '{5}', @FolioFinal = '{6}', @FolioUtilizado = '{7}', @Status = '{8}' ",
                    cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, sSerie, sIdTipoDocto, sFolioInicial, sFolioFinal, sFolioUtilizado, sStatus);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
            }


            if (bRegresa)
            {
                bRegresa = Guardar_Email();
            }

            return bRegresa;
        }


        private bool Guardar_Email()
        {
            bool bRegresa = true;
            string sSql = string.Format("Exec spp_Mtto_CFDI_Emisores_mail " + 
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Servidor = '{3}', @Puerto = '{4}', @TiempoEspera = '{5}', " + 
                " @Usuario = '{6}', @Password = '{7}', @EnableSSL = '{8}', @EmailRespuesta = '{9}', " + 
                " @NombreParaMostrar = '{10}', @CC = '{11}', @Asunto = '{12}', @MensajePredeterminado = '{13}' ",
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, 
                txtSMTP.Text.Trim(), txtPort.Text.Trim(), txtTime.Text.Trim(), txtUser.Text.Trim(), txtEmailPass.Text.Trim(), chkSSL.Checked,
                txtMail.Text.Trim(), txtNombreParaMostrar.Text.Trim(), txtCC.Text.Trim(), txtAsunto.Text.Trim(), txtMensaje.Text.Trim()
                );

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
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
            string sSql = ""; 
            cboEstados.Clear();
            cboEstados.Add();
            cboSucursales.Clear();
            cboSucursales.Add(); 

            cboEmpresas.Clear(); 
            cboEmpresas.Add(); 
            cboEmpresas.Add(query.Empresas("CargarEmpresas()"), true, "IdEmpresa", "Nombre"); 
            cboEmpresas.SelectedIndex = 0;

            sSql = "Select IdEstado, NombreEstado as Estado, ClaveRenapo, IdEmpresa " + 
                " From vw_EmpresasEstados (NoLock) " + 
                " Where StatusEdo = 'A' Order by IdEstado ";

            leer.Exec(sSql); 
            dtsEstados = leer.DataSetClase;

            sSql = "Select L.IdEstado, L.IdEmpresa, L.IdEstado, L.Estado, " +
                " L.IdFarmacia, L.Farmacia, (L.IdFarmacia + ' - ' + L.Farmacia) as NombreFarmacia " + 
                " From vw_EmpresasFarmacias L (NoLock) " +
                " Where L.Status = 'A' and " + 
                "   Exists " + 
                "   ( " + 
                "       Select * From vw_Farmacias F Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia  " + 
                "            " + 
                "   ) " +
                " Order by L.IdEstado, L.IdFarmacia ";


            sSql = "Select L.IdEstado, L.IdEmpresa, L.IdEstado, L.Estado, " +
                " L.IdFarmacia, L.Farmacia, (L.IdFarmacia + ' - ' + L.Farmacia) as NombreFarmacia " +
                " From vw_EmpresasFarmacias L (NoLock) " +
                " Where L.Status = 'A' " + 
                " Order by L.IdEstado, L.IdFarmacia ";

            leer.Exec(sSql);
            dtsFarmacias = leer.DataSetClase; 
        }

        private void CargarInformacion()
        {
            string sSql = string.Format("Exec spp_CFDI_Emisores_Sucursales_Configuracion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", 
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data );
            string sMensaje = "No se encontro configuración de factura electrónica para la sucursal seleccionada.\n\n" +
                "¿ Desea generar el registro ? "; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarInformacionEmpresa()");
                General.msjError("Ocurrió un error al obtener la información de la Empresa."); 
            }
            else
            {
                InicializaToolBar(true, false); 
                if (!leer.Leer())
                {
                    InicializaToolBar(false, true);
                    if (General.msjConfirmar(sMensaje) == DialogResult.Yes)
                    {
                        InicializaToolBar(true, false);
                        cboEmpresas.Enabled = false;
                        CargarDatosEmpresa(); 
                        txtRazonSocial.Focus(); 
                    }
                }
                else 
                {
                    cboEmpresas.Enabled = false;
                    cboEstados.Enabled = false;
                    cboSucursales.Enabled = false; 

                    leer.RenombrarTabla(1, "Sucursal_Domicilio");
                    leer.RenombrarTabla(2, "Series");
                    leer.RenombrarTabla(3, "Email");

                    CargarDatosSucursal(leer.Tabla("Sucursal_Domicilio"));
                    CargarDatosDomicilio(leer.Tabla("Sucursal_Domicilio"));
                    CargarDatosSeries(leer.Tabla("Series"));
                    CargarDatosEmail(leer.Tabla("Email"));
                } 
            }
        }

        private void CargarDatosEmpresa()
        {
            string sSql = string.Format("Select * From CFDI_Emisores  (NoLock) Where IdEmpresa = '{0}' ", cboEmpresas.Data);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "");
                General.msjError("Ocurrió un error al obtener la información de la empresa."); 
            }
            else 
            {
                if (leer.Leer())
                {
                    cboEmpresas.Data = leer.Campo("IdEmpresa");
                    cboEmpresas.Enabled = false;
                    cboEstados.Enabled = false;
                    cboSucursales.Enabled = false;

                    txtRazonSocial.Enabled = false;
                    txtRFC.Enabled = false; 
                    txtRazonSocial.Text = leer.Campo("NombreFiscal");
                    txtRFC.Text = leer.Campo("RFC");
                }
            }
        }

        private void CargarDatosSucursal(DataTable Datos) 
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;

            datosLeer.Leer();
            cboEmpresas.Data = datosLeer.Campo("IdEmpresa"); 
            cboEmpresas.Enabled = false; 
            cboEstados.Data = datosLeer.Campo("IdEstado");
            cboEstados.Enabled = false;
            cboSucursales.Data = datosLeer.Campo("IdFarmacia");
            cboSucursales.Enabled = false; 

            txtRazonSocial.Text = datosLeer.Campo("NombreFiscal");
            txtRFC.Text = datosLeer.Campo("RFC"); 
        }

        private void CargarDatosDomicilio(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;

            datosLeer.Leer();
            txtD_Pais.Text = datosLeer.Campo("EPais");
            txtD_Estado.Text = datosLeer.Campo("EIdEstado");
            lblD_Estado.Text = datosLeer.Campo("EEstado");
            txtD_Municipio.Text = datosLeer.Campo("EIdMunicipio");
            lblD_Municipio.Text = datosLeer.Campo("EMunicipio");
            txtD_Colonia.Text = datosLeer.Campo("EIdColonia");
            lblD_Colonia.Text = datosLeer.Campo("EColonia");
            txtD_Calle.Text = datosLeer.Campo("ECalle");
            txtD_Referencia.Text = datosLeer.Campo("EReferencia");
            txtD_NoExterior.Text = datosLeer.Campo("ENoExterior");
            txtD_NoInterior.Text = datosLeer.Campo("ENoInterior");
            txtD_CodigoPostal.Text = datosLeer.Campo("ECodigoPostal");
            // IdEmpresa, Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia 
        }

        private void CargarDatosCertificados(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos; 
            datosLeer.Leer(); 

            ////// NombreLlavePrivada, LlavePrivada, PasswordPublico, AvisoVencimiento, TiempoAviso, Status
            ////// txtD_CodigoPostal.Text = datosLeer.Campo("IdEmpresa"); 
            ////lblCertificadoGuardado .Text = datosLeer.Campo("NombreCertificado");
            ////sCertificado = datosLeer.Campo("Certificado"); 
            ////lblDesde.Text = datosLeer.Campo("ValidoDesde");
            ////lblHasta.Text = datosLeer.Campo("ValidoHasta");
            ////dtpDesde.Value = datosLeer.CampoFecha("FechaInicio");
            ////dtpDesdeHora.Value = datosLeer.CampoFecha("FechaInicio");
            ////dtpHasta.Value = datosLeer.CampoFecha("FechaFinal");
            ////dtpHastaHora.Value = datosLeer.CampoFecha("FechaFinal");
            ////lblSerie.Text = datosLeer.Campo("Serie");
            ////lblSerial.Text = datosLeer.Campo("Serial");

            ////lblLlavePrivadaGuardada.Text = datosLeer.Campo("NombreLlavePrivada");
            ////sLlavePrivada = datosLeer.Campo("LlavePrivada");
            ////txtPassword.Text = datosLeer.Campo("PasswordPublico");
            ////// txtD_CodigoPostal.Text = datosLeer.Campo("AvisoVencimiento");
            ////// txtD_CodigoPostal.Text = datosLeer.Campo("TiempoAviso"); 
        }

        private void CargarDatosSeries(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;
            datosLeer.Leer();

            list.CargarDatos(datosLeer.DataSetClase, false, false); 
        }

        private void CargarDatosEmail(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;
            datosLeer.Leer();

            //Servidor, Puerto, TiempoDeEspera, Usuario, Password, EnableSSL, EmailRespuesta, NombreParaMostrar, CC, Asunto, MensajePredeterminado

            txtMail.Text = datosLeer.Campo("EmailRespuesta");
            txtNombreParaMostrar.Text = datosLeer.Campo("NombreParaMostrar");
            txtCC.Text = datosLeer.Campo("CC");
            txtAsunto.Text = datosLeer.Campo("Asunto");
            txtMensaje.Text = datosLeer.Campo("MensajePredeterminado");

            txtSMTP.Text = datosLeer.Campo("Servidor");
            txtPort.Text = datosLeer.CampoInt("Puerto").ToString();
            txtTime.Text = datosLeer.CampoInt("TiempoDeEspera").ToString();
            txtUser.Text = datosLeer.Campo("Usuario");
            txtEmailPass.Text = datosLeer.Campo("Password");
            txtEmailPassConfirmacion.Text = datosLeer.Campo("Password");
            chkSSL.Checked = datosLeer.CampoBool("EnableSSL");

        }
        #endregion Funciones y Procedimientos Privados

        #region Eventos 
        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRFC.Text = "";
            cboEstados.Clear();
            cboEstados.Add();

            if (cboEmpresas.SelectedIndex != 0)
            {
                lblRFC.Text = cboEmpresas.ItemActual.GetItem("RFC");
                
                cboEstados.Filtro = string.Format(" IdEmpresa = '{0}' ", cboEmpresas.Data);
                cboEstados.Add(dtsEstados, true, "IdEstado", "Estado"); 
            }

            cboEstados.SelectedIndex = 0;  
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSucursales.Clear();
            cboSucursales.Add();

            if (cboEstados.SelectedIndex != 0)
            {
                cboSucursales.Filtro = string.Format(" IdEmpresa = '{0}' and IdEstado = '{1}' ", cboEmpresas.Data, cboEstados.Data);
                cboSucursales.Add(dtsFarmacias, true, "IdFarmacia", "NombreFarmacia");
            } 

            cboSucursales.SelectedIndex = 0; 
        }
        #endregion Eventos  

        #region Series y Folios
        private void agregarSeries_Click(object sender, EventArgs e)
        {
            CargarSeries(1);
        }

        private void modificarSeries_Click(object sender, EventArgs e)
        {
            CargarSeries(2);
        }

        private void CargarSeries(int Opcion)
        {
            FrmSeries f = new FrmSeries();
            f.TiposDeDocumentos = TiposDeDocumentos;

            if (Opcion == 1)
            {
                f.CargarPantalla();
                if (f.Guardado)
                {
                    ////ListViewItem itmX = lvwFoliosSeries.Items.Add(cboEmpresa.Data);
                    ////itmX.SubItems.Add(cboEstado.Data);
                    ////itmX.SubItems.Add(cboSucursal.Data);

                    ListViewItem itmX = lvwFoliosSeries.Items.Add(f.sSerie);
                    itmX.SubItems.Add(f.sIdTipoDocto);
                    itmX.SubItems.Add(f.sNombreTipoDocto);
                    itmX.SubItems.Add(f.sFolioInicio);
                    itmX.SubItems.Add(f.sFolioFinal);
                    itmX.SubItems.Add(f.sUltimoFolio);
                    itmX.SubItems.Add(f.sStatus);
                }
            }
            else
            {
                string sSerie = "", sIdTipoDocto = "";
                string sIdTipoDoctoDescripcion = "", sFolioInicio = "", sFolioFinal = "", sUltimoFolio = "", sStatus = "";

                try
                {
                    sSerie = list.GetValue((int)Cols.Serie);
                    sIdTipoDocto = list.GetValue((int)Cols.ClaveTipoDocumento);
                    sIdTipoDoctoDescripcion = list.GetValue((int)Cols.TipoDocumento);
                    sFolioInicio = list.GetValue((int)Cols.FolioInicio);
                    sFolioFinal = list.GetValue((int)Cols.FolioFinal);
                    sUltimoFolio = list.GetValue((int)Cols.FolioUltimo);
                    sStatus = list.GetValue((int)Cols.Status);
                    f.CargarPantalla(sSerie, sIdTipoDocto, sFolioInicio, sFolioFinal, sUltimoFolio, sStatus);

                    if (f.Guardado)
                    {
                        list.SetValue((int)Cols.Serie, f.sSerie);
                        list.SetValue((int)Cols.ClaveTipoDocumento, f.sIdTipoDocto);
                        list.SetValue((int)Cols.TipoDocumento, f.sNombreTipoDocto);
                        list.SetValue((int)Cols.FolioInicio, f.sFolioInicio);
                        list.SetValue((int)Cols.FolioFinal, f.sFolioFinal);
                        list.SetValue((int)Cols.FolioUltimo, f.sUltimoFolio);
                        list.SetValue((int)Cols.Status, f.sStatus);
                    }
                }
                catch (Exception ex)
                {
                    ex = ex;
                }
            }
        }
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
        #endregion Geograficos
    }
}
