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

using Dll_IFacturacion; 

namespace Dll_IFacturacion.Configuracion
{
    public partial class FrmFactSucursales : FrmBaseExt 
    {
        private enum Cols_Detalle
        {
            IdEmpresa = 0, IdEstado = 1, IdSucursal = 2,
            Año = 3, Aprobacion = 4, Serie = 5, FolioInicio = 6, FolioFinal = 7, FolioUltimo = 8, 
            Identificador = 9 
        }

        private enum Cols
        {
            Asignado = 1, Bloqueado = 2, Año = 3, Aprobacion = 4, 
            Serie = 5, TipoDocumento = 6, DescripcionDocumento = 7, 
            FolioInicio = 8, FolioFinal = 9, FolioUltimo = 10,
            Identificador = 11   
        } 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsConsultas query; 
        clsListView list;

        DataSet dtsEstados = new DataSet();
        DataSet dtsFarmacias = new DataSet(); 

        public FrmFactSucursales()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            query = new clsConsultas(General.DatosConexion, DtIFacturacion.DatosApp, this.Name); 
            Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, this.Name);

            list = new clsListView(lvwFoliosSeries); 

            CargarEmpresas(); 
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

            HabilitarMenu(); 
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

            sSql = string.Format("Exec spp_Mtto_FACT_CFD_Sucursales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Nombre = '{3}', @RFC = '{4}', @Status = '{5}' ", 
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data,
                txtRazonSocial.Text.Trim(), txtRFC.Text.Trim(), Status);

            bRegresa = leer.Exec(sSql); 
            if (bRegresa)
            {
                bRegresa = GuardarDomicilioFiscal(); 
            }

            return bRegresa; 
        }

        private bool GuardarDomicilioFiscal()
        {
            bool bRegresa = false;
            string sSql = string.Format(""); 

            sSql = string.Format("Exec spp_Mtto_FACT_CFD_Domicilios @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', " + 
                " @Pais = '{3}', @Estado = '{4}', @Municipio = '{5}', @Localidad = '{6}', @Colonia = '{7}', " + 
                " @Calle = '{8}', @NoExterior = '{9}', @NoInterior = '{10}', @CodigoPostal = '{11}', @Referencia = '{12}', @Opcion = '{13}'  ",
                cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, txtD_Pais.Text.Trim(), txtD_Estado.Text.Trim(), txtD_Municipio.Text.Trim(),  
                txtD_Localidad.Text.Trim(), txtD_Colonia.Text.Trim(), txtD_Calle.Text.Trim(), 
                txtD_NoExterior.Text.Trim(), txtD_NoInterior.Text.Trim(), txtD_CodigoPostal.Text.Trim(), txtD_Referencia.Text.Trim(), 2);

            bRegresa = leer.Exec(sSql);
            if (bRegresa)
            {
                bRegresa = GuardarSeries(); 
            }
            return bRegresa;
        }

        private bool GuardarSeries()
        {
            bool bRegresa = true;
            string sSql = string.Format("");
            string sIdentificador = "";
            string sAsignado = "";
            string sStatus = ""; 

            for (int i = 1; i <= list.Registros; i++) 
            {
                sIdentificador = list.GetValue(i, (int)Cols.Identificador);
                sAsignado = list.GetValue(i, (int)Cols.Asignado);
                sStatus = "C"; 

                if (sAsignado.ToUpper() == "SI")
                {
                    sStatus = "A"; 
                }

                sSql = string.Format("Exec spp_Mtto_FACT_CFD_Sucursales_Series " +
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdentificadorSerie = '{3}', @Status = '{4}' ",
                    cboEmpresas.Data, cboEstados.Data, cboSucursales.Data, sIdentificador, sStatus);
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    break;
                }
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

            if (bRegresa && txtD_Localidad.Text.Trim() == "")
            {
                bRegresa = false;
                General.msjUser("La Localidad no debe ser vacio, verifique.");
                txtD_Localidad.Focus();
            }

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
                " From vw_EmpresasEstados (NoLock) Where StatusEdo = 'A' Order by IdEstado ";

            leer.Exec(sSql); 
            dtsEstados = leer.DataSetClase;

            sSql = "Select L.IdEstado, L.IdEmpresa, L.IdEstado, L.Estado, " +
                " L.IdFarmacia, L.Farmacia, (L.IdFarmacia + ' - ' + L.Farmacia) as NombreFarmacia " + 
                " From vw_EmpresasFarmacias L (NoLock) " +
                " Where L.Status = 'A' and " + 
                "   Exists " + 
                "   ( " + 
                "       Select * From vw_Farmacias F Where L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia  " + 
                "           and F.IdTipoUnidad = '005' " + 
                "   ) " +
                " Order by L.IdEstado, L.IdFarmacia ";

            leer.Exec(sSql);
            dtsFarmacias = leer.DataSetClase; 
        }

        private void CargarInformacion()
        {
            string sSql = string.Format("Exec spp_FACT_CFD_Sucursales_Configuracion @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}' ", 
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

                    leer.RenombrarTabla(1, "Sucursal");
                    leer.RenombrarTabla(2, "Domicilio");
                    ////leer.RenombrarTabla(3, "Certificados");
                    leer.RenombrarTabla(3, "Series");

                    CargarDatosSucursal(leer.Tabla("Sucursal"));
                    CargarDatosDomicilio(leer.Tabla("Domicilio"));
                    ////CargarDatosCertificados(leer.Tabla("Certificados"));
                    CargarDatosSeries(leer.Tabla("Series")); 
                } 
            }
        }

        private void CargarDatosEmpresa()
        {
            string sSql = string.Format("Select IdEmpresa, Nombre, RFC From FACT_CFD_Empresas (NoLock) Where IdEmpresa = '{0}' ", cboEmpresas.Data);

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

                    txtRazonSocial.Text = leer.Campo("Nombre");
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

            txtRazonSocial.Text = datosLeer.Campo("Nombre");
            txtRFC.Text = datosLeer.Campo("RFC"); 
        }

        private void CargarDatosDomicilio(DataTable Datos)
        {
            clsLeer datosLeer = new clsLeer();
            datosLeer.DataTableClase = Datos;

            datosLeer.Leer();
            txtD_Pais.Text = datosLeer.Campo("Pais");
            txtD_Estado.Text = datosLeer.Campo("Estado");
            txtD_Municipio.Text = datosLeer.Campo("Municipio");
            txtD_Localidad.Text = datosLeer.Campo("Localidad");
            txtD_Colonia.Text = datosLeer.Campo("Colonia");
            txtD_Calle.Text = datosLeer.Campo("Calle");
            txtD_NoExterior.Text = datosLeer.Campo("NoExterior");
            txtD_NoInterior.Text = datosLeer.Campo("NoInterior");
            txtD_Referencia.Text = datosLeer.Campo("Referencia");
            txtD_CodigoPostal.Text = datosLeer.Campo("CodigoPostal"); 
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
        private void HabilitarMenu()
        {
            bool bAsignar = false;
            bool bDesasignar = false;

            if (list.GetValue((int)Cols.Bloqueado) == "NO")
            {
                if (list.GetValue((int)Cols.Asignado) == "NO")
                {
                    bAsignar = true;
                }
                else
                {
                    bDesasignar = true;
                }
            }
            else
            {
                if (list.GetValue((int)Cols.Asignado) == "SI")
                {
                    bDesasignar = true;
                } 
            }

            btnAsignar.Enabled = bAsignar;
            btnDesasignar.Enabled = bDesasignar;
        }

        private void lvwFoliosSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarMenu();
        } 

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            ModificarAsignacion("SI"); 
        }

        private void btnDesasignar_Click(object sender, EventArgs e)
        {
            ModificarAsignacion("NO"); 
        }

        private void ModificarAsignacion(string Valor)
        {
            if (list.GetValue((int)Cols.Año) != "")
            {
                list.SetValue((int)Cols.Asignado, Valor.ToUpper());
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
    }
}
