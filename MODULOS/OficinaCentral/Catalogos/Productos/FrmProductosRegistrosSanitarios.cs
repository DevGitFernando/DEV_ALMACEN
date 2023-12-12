using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using DllFarmaciaSoft.PDF; 

using OficinaCentral.wsOficinaCentral;
using DllFarmaciaSoft.ExportarExcel;


// using Farmacia;

namespace OficinaCentral.Catalogos.Productos 
{
    public partial class FrmProductosRegistrosSanitarios : FrmBaseExt
    {
        internal class RegistrosSanitarios_ProcesosEspeciales
        {
            clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer;
            clsLeer leer_Auxiliar;
            clsLeer leer_Update;
            
            clsGrabarError Error;
            basGenerales Fg;
            

            string sFolio = ""; 
            string sNombreArchivo = "";
            string sContenido = "";
            double iTam_01 = 0;
            double iTam_02 = 0;
            string sTipoArchivo = "";
            FileInfo fileInf_00;
            FileInfo fileInf_01;
            FileInfo fileInf_02;
            string sRuta_Descarga = ""; 

            public RegistrosSanitarios_ProcesosEspeciales()
            {
                leer = new clsLeer(ref cnn);
                leer_Auxiliar = new clsLeer(ref cnn);
                leer_Update = new clsLeer(ref cnn);
                
                Error = new clsGrabarError(General.DatosConexion, GnOficinaCentral.DatosApp, "RegistrosSanitarios_ProcesosEspeciales");

                Fg = new basGenerales(); 


                sRuta_Descarga = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"REGISTROS_SANITARIOS__PRCSSPCL\");

                if (!Directory.Exists(sRuta_Descarga))
                {
                    Directory.CreateDirectory(sRuta_Descarga); 
                }
            }

            public void ActualizarInformacionArchivos()
            {
                string sSql = "Select 50 Folio From CatRegistrosSanitarios (NoLock) Where InfActualizada = 0 Order By Folio ";

                sSql = "Select top 50000 Folio From CatRegistrosSanitarios (NoLock) Where InfActualizada = 0 and Folio >= 1 Order By Folio ";


                if (!leer_Auxiliar.Exec(sSql))
                {
                    Error.GrabarError(leer_Auxiliar, "ActualizarInformacionArchivos()"); 
                }
                else
                {
                    leer.DataSetClase = leer_Auxiliar.DataSetClase;
                    while (leer.Leer())
                    {
                        sFolio = leer.Campo("Folio"); 
                        sSql = string.Format(
                            "Select Folio, IdLaboratorio, IdClaveSSA_Sal, Consecutivo, Tipo, Año, FechaVigencia, Documento, NombreDocto, " + 
                            " Status, Actualizado, IdPaisFabricacion, IdPresentacion, AñosCaducidad, TamañoArchivo_MB, TamañoArchivoAux_MB, TipoArchivo " +
                            " From CatRegistrosSanitarios (NoLock) Where Folio = '{0}' ", sFolio);

                        if (!leer_Auxiliar.Exec(sSql))
                        {
                            Error.GrabarError(leer_Auxiliar, "ActualizarInformacionArchivos()"); 
                        }
                        else
                        {
                            if (leer_Auxiliar.Leer())
                            {
                                sNombreArchivo = leer_Auxiliar.Campo("NombreDocto");
                                sContenido = leer_Auxiliar.Campo("Documento");

                                if (Fg.ConvertirStringB64EnArchivo(sNombreArchivo, sRuta_Descarga, sContenido, true))
                                {
                                    try
                                    {
                                        File.Copy(Path.Combine(sRuta_Descarga, sNombreArchivo), Path.Combine(sRuta_Descarga, sNombreArchivo + "__Base"));

                                        fileInf_00 = new FileInfo(Path.Combine(sRuta_Descarga, sNombreArchivo + "__Base"));
                                        fileInf_01 = new FileInfo(Path.Combine(sRuta_Descarga, sNombreArchivo));

                                        sTipoArchivo = fileInf_01.Extension.Replace(".", "");
                                        iTam_01 = Math.Round(Convert.ToDouble((fileInf_01.Length / 1024.0) / 1024.0), 2);


                                        if (fileInf_01.Extension.ToUpper().Contains("PDF"))
                                        {
                                            iTextSharpUtil.AddTextWatermark(fileInf_00.FullName, "", false);
                                        }

                                        fileInf_00 = new FileInfo(Path.Combine(sRuta_Descarga, sNombreArchivo + "__Base"));
                                        //fileInf_02 = new FileInfo(Path.Combine(sRuta_Descarga, sNombreArchivo));
                                        iTam_02 = Math.Round(Convert.ToDouble((fileInf_00.Length / 1024.0) / 1024.0), 2);

                                        sContenido = Fg.ConvertirArchivoEnStringB64(fileInf_00.FullName);
                                        updateInfo();
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }


            private void updateInfo()
            {
                string sSql = string.Format(
                    "Update F Set InfActualizada = 1, Documento = '{1}', TipoArchivo = '{2}', TamañoArchivo_MB = '{3}', TamañoArchivoAux_MB = '{4}' \n " +
                    "From CatRegistrosSanitarios F (NoLock) \n " + 
                    "Where Folio = '{0}' \n ", sFolio, sContenido, sTipoArchivo, iTam_01, iTam_02);

                leer_Update.Exec(sSql); 
            }
        }

        #region Datos requeridos por esta pantalla
        /*Datos Utilizados para la Conexión con la BD's*/
        clsConexionSQL myConn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer leerListadoDeRegistros;
        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();
        clsDatosCliente datosCliente;
        clsConsultas Consultas;
        clsAyudas Ayudas; 
        DataSet dtsCargarDatos = new DataSet();
        clsGrid grid;
        //  clsDatosCliente myDatosCliente;
        // wsCnnOficinaCentral myConexionWeb;

        /*Variables Globales*/
        bool bContinua = true;
        bool bDocumento_Guardado = false; 
        string sDocumento = "";
        string sNombreDocto = "";
        string sArchivo = "";
        bool bExisteArchivoParaDescarga = false; 
        string sNombreArchivo = "";
        bool bCodigoEAN_Cargado = false;
        bool bInvocado_Externamente = false;

        clsCriptografo crypto = new clsCriptografo(); 
        DateTime dtFechaSistema = DateTime.Now;
        string sFechaSistema = "";
        string sKey = ""; 

        #endregion Datos requeridos por esta pantalla

        #region Documentos
        OpenFileDialog file = new OpenFileDialog();
        FolderBrowserDialog Folder = new FolderBrowserDialog();
        #endregion Documentos

        private enum Status
        {
            Activo = 1, Cancelado = 2
        }

        public FrmProductosRegistrosSanitarios()
        {
            InitializeComponent();

            leer = new clsLeer(ref myConn);
            leerListadoDeRegistros = new clsLeer(ref myConn);
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ProductosRegistrosSanitarios");
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.Modulo, this.Name, GnFarmacia.Version, true);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.Modulo, GnFarmacia.Version, this.Name);

            grid = new clsGrid(ref grdClaves, this);
            grid.EstiloGrid(eModoGrid.ModoRow);
            grdClaves.EditModeReplace = true;

            Ayudas = new clsAyudas(General.DatosConexion, GnOficinaCentral.DatosApp, this.Name);
            
            dtFechaSistema = General.FechaSistema;
            sFechaSistema = General.FechaYMD(dtFechaSistema, "") + General.Hora(dtFechaSistema, "");
            
            sKey = crypto.Encrypt(sFechaSistema, true); 


            CargarPaisesDeFabricacion();
            CargarPresentaciones(); 
        }

        private void FrmProductosRegistrosSanitarios_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null); 
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            sArchivo = "";
            dtpUltimaActualizacionEnSistema.Enabled = false;

            if (!bInvocado_Externamente)
            {
                Fg.IniciaControles();
                cboPaisesDeFabricacion.SelectedIndex = 1;
                cboPresentaciones.SelectedIndex = 1;
                dtpFechaRegistro.Enabled = false;
                dtpUltimaActualizacionEnSistema.Enabled = false;


                CargarComboStatus();

                btnAsignarDocto.Enabled = false;
                btnDescargar.Enabled = false;

                bDocumento_Guardado = false; 
                bCodigoEAN_Cargado = false;
                rdoAños.Checked = false;
                rdoMeses.Checked = false;

                txtFolio.Focus();

                grid.Limpiar(false);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
        }       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidaDatos())
            {
                if (!myConn.Abrir())
                {
                    Error.LogError(myConn.MensajeError);
                    General.msjErrorAlAbrirConexion(); 
                }
                else 
                {
                    myConn.IniciarTransaccion();

                    bContinua = GuardarRegistroSanitario();

                    if (!bContinua) // Si no ocurrio ningun error se llevan a cabo las transacciones.
                    {
                        Error.GrabarError(leer, "btnGuardar_Click");
                        myConn.DeshacerTransaccion();
                        General.msjError("Ocurrió un error al guardar la información.");
                    }
                    else 
                    {
                        myConn.CompletarTransaccion(); 
                        General.msjUser("Información guardada satisfactoriamente."); //Este mensaje lo genera el SP

                        if (!bInvocado_Externamente)
                        {
                            btnNuevo_Click(null, null); 
                        }
                        else 
                        {
                            this.Hide();
                        }
                    }

                    myConn.Cerrar();
                }
            }
        }

        private void btnPaisesDeFabricacion_Click(object sender, EventArgs e)
        {

        }

        private void btnPresentaciones_Click(object sender, EventArgs e)
        {

        }
        #endregion Botones

        #region Funciones y Procedimientos
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control)
            {
                TeclasRapidas(e);
            }
            else
            {
                base.OnKeyDown(e); 
            }
        }

        private void IniciarToolBar(bool bNuevo, bool bGuardar, bool bCancelar, bool bImprimir)
        {
            btnNuevo.Enabled = bNuevo;
            btnGuardar.Enabled = bGuardar;
            btnCancelar.Enabled = bCancelar;
            btnImprimir.Enabled = bImprimir;
        }

        private void TeclasRapidas(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (btnGuardar.Enabled)
                    {
                        btnGuardar_Click(null, null);
                    }
                    break;
                
                case Keys.F5:
                    if (DtGeneral.EsEquipoDeDesarrollo)
                    {
                        RegistrosSanitarios_ProcesosEspeciales reg = new RegistrosSanitarios_ProcesosEspeciales();
                        reg.ActualizarInformacionArchivos(); 
                    }
                    break;

                default:
                    break;
            }
        }

        private void CargarPaisesDeFabricacion()
        {
            cboPaisesDeFabricacion.Clear();
            cboPaisesDeFabricacion.Add();

            cboPaisesDeFabricacion.Add(Consultas.RegistrosSanitarios_PaisesDeFabricacion("", "CargarPaisesDeFabricacion()"), true, "IdPais", "NombrePais"); 

            cboPaisesDeFabricacion.SelectedIndex = 1;
        }

        private void CargarPresentaciones()
        {
            cboPresentaciones.Clear();
            cboPresentaciones.Add();

            cboPresentaciones.Add(Consultas.RegistrosSanitarios_Presentaciones("", "CargarPresentaciones()"), true, "IdPresentacion", "Descripcion");

            cboPresentaciones.SelectedIndex = 1;
        }

        private void CargarComboStatus()
        {
            //int iVigente = (int)StatusRegistrosSanitarios.Vigente;
            //int iRenovacion = (int)StatusRegistrosSanitarios.Renovacion;
            //int iProrroga = (int)StatusRegistrosSanitarios.Prorroga; 
            //int iRevocado = (int)StatusRegistrosSanitarios.Revocado;
            //int iVencido = (int)StatusRegistrosSanitarios.Vencido;
            //int iOficio = (int)StatusRegistrosSanitarios.Oficio;

            //string sVigente = StatusRegistrosSanitarios.Vigente.ToString();
            //string sRenovacion = StatusRegistrosSanitarios.Renovacion.ToString();
            //string sProrroga = StatusRegistrosSanitarios.Prorroga.ToString();
            //string sRevocado = StatusRegistrosSanitarios.Revocado.ToString(); 
            //string sVencido = StatusRegistrosSanitarios.Vencido.ToString();
            //string sOficio = StatusRegistrosSanitarios.Oficio.ToString();

            cboStatus.Clear(); 
            cboStatus.Add("0", "<< Seleccione >>"); 
            
            ////cboStatus.Add("V", "VIGENTE"); 
            ////cboStatus.Add("R", "PROCESO DE RENOVACION");
            ////cboStatus.Add("P", "PRORROGA");
            ////cboStatus.Add("RV", "REVOCADO");

            ////cboStatus.Add("1", "VIGENTE");
            ////cboStatus.Add("2", "PROCESO DE RENOVACION");
            ////cboStatus.Add("3", "PRORROGA");
            ////cboStatus.Add("4", "REVOCADO");  

            //////cboStatus.Add((int)StatusRegistrosSanitarios.Vigente, (string)StatusRegistrosSanitarios.Vigente);
            //////cboStatus.Add((int)StatusRegistrosSanitarios.Renovacion, (string)StatusRegistrosSanitarios.Renovacion);
            //////cboStatus.Add((int)StatusRegistrosSanitarios.Prorroga, (string)StatusRegistrosSanitarios.Prorroga);
            //////cboStatus.Add((int)StatusRegistrosSanitarios.Revocado, (string)StatusRegistrosSanitarios.Revocado);

            //cboStatus.Add(iVigente.ToString(), sVigente);
            //cboStatus.Add(iRenovacion.ToString(), sRenovacion);
            //cboStatus.Add(iProrroga.ToString(), sProrroga);
            //cboStatus.Add(iRevocado.ToString(), sRevocado);
            //cboStatus.Add(iVencido.ToString(), sVencido);
            //cboStatus.Add(iOficio.ToString(), sOficio);

            leer.DataSetClase = Consultas.TiposRegistrosSanitarios("CargarComboStatus()");
            cboStatus.Add(leer.DataSetClase, true, "IdTipos", "Descripcion");
            cboStatus.SelectedIndex = 0; 
        }       
    
        //public void CargarProductosRegistrosSanitarios(string IdProducto, string CodigoEAN, string DescripcionCorta)
        //{
        //    // bool bRegreso = false;
        //    IniciarToolBar(false, true, false, false);
        //    bInvocado_Externamente = true; 

        //    txtIdProducto.Text = IdProducto;
        //    //txtCodigoEAN.Text = CodigoEAN;
        //    //lblDescripcion.Text = DescripcionCorta;
        //    txtIdProducto_Validating(null, null); 

        //    CargarComboStatus();

        //    lblCancelado.Visible = false;
        //    lblCancelado.Text = "CANCELADO";

        //    bCodigoEAN_Cargado = true; 
        //    txtCodigoEAN.Text = CodigoEAN;
        //    BuscarRegistro(IdProducto, CodigoEAN); 
        //    this.ShowDialog();
        //}

        private bool ValidaDatos()
        {
            bool bRegresa = true;

            if (txtFolioRegistroSanitario.Text == "")
            {
                bRegresa = false;
                General.msjError("No ha capturado la información de Folo de Registro, verifique.");
                txtFolioRegistroSanitario.Focus(); 
                // return bRegresa;
            }

            //if (bRegresa && txtTipo.Text == "")
            //{
            //    bRegresa = false;
            //    General.msjError("No ha capturado la información de Tipo, verifique.");
            //    //General.msjError("Debe ingresar información en el campo Tipo para poder guardar este registro, verifique.");
            //    txtTipo.Focus(); 
            //    // return bRegresa;
            //}

            //if (bRegresa && txtAño.Text == "")
            //{
            //    bRegresa = false;
            //    General.msjError("No ha capturado la información de Año, verifique.");
            //    //General.msjError("Debe ingresar información en el campo Año para poder guardar este registro, verifique.");
            //    txtAño.Focus();
            //    // return bRegresa;
            //}

            if (bRegresa && (dtpFechaVigencia.Value < dtpFechaVigencia.MinDate) || (dtpFechaVigencia.Value > dtpFechaVigencia.MaxDate))
            {
                bRegresa = false;
                General.msjError("Debe capturar una Fecha de Vigencia válida para poder guardar este registro, verifique.");
                dtpFechaVigencia.Focus(); 
                // return bRegresa;
            }

            if (bRegresa && dtpFechaVigencia.Value < General.FechaSistema)
            {
                bRegresa = false;
                                
                //General.msjError("Debe capturar una Fecha de Vigencia igual o posterior a la del Sistema para poder guardar este registro, verifique.");
                if (General.msjConfirmar(string.Format("La fecha de vigencia del registro sanitario es menor a la fecha actual,\n¿ Desea guardar la información con la fecha {0} ? ", General.FechaYMD(dtpFechaVigencia.Value))) == System.Windows.Forms.DialogResult.Yes)
                {
                    bRegresa = true; 
                }
                else 
                {
                    dtpFechaVigencia.Focus();
                }
                
                
                // return bRegresa;
            }

            if (bRegresa && grid.TotalizarColumna(4) == 0)
            {
                bRegresa = false;

                //General.msjError("Debe capturar una Fecha de Vigencia igual o posterior a la del Sistema para poder guardar este registro, verifique.");
                if (General.msjConfirmar("No tiene seleccionado ningun Código EAN,\n¿ Desea guardar la información ? ") == System.Windows.Forms.DialogResult.Yes)
                {
                    bRegresa = true;
                }

                // return bRegresa;
            }

            if (bRegresa && cboStatus.SelectedIndex == 0)
            {
                bRegresa = false; 
                General.msjError("No ha seleccionado un Status, verifique.");
                cboStatus.Focus(); 
                // return bRegresa;
            }

            if (bRegresa && cboPaisesDeFabricacion.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjError("No ha seleccionado un Pais, verifique.");
                cboPaisesDeFabricacion.Focus();
                // return bRegresa;
            }

            if (bRegresa && cboPresentaciones.SelectedIndex == 0)
            {
                bRegresa = false;
                General.msjError("No ha seleccionado una Presentación, verifique.");
                cboPresentaciones.Focus();
                // return bRegresa;
            }

            if (bRegresa && ( !rdoMeses.Checked && !rdoAños.Checked) )
            {
                bRegresa = false;
                General.msjAviso("No ha seleccionado el tipo de caducidad de fabricación, verifique.");
                rdoAños.Focus();
            }

            return bRegresa;    
        }

        private bool GuardarRegistroSanitario()
        {
            bool bRegresa = true;
            string sFolio = txtFolioRegistroSanitario.Text;
            //string sTipo = txtTipo.Text;
            //string sAño = txtAño.Text;
            string sFechavigencia = General.FechaYMD(dtpFechaVigencia.Value);
            // string sStatus = ""; 
            int iOpcion = 1;
            int iTipoCaducidad = rdoAños.Checked ? 1 : 2;
            string sSql = "";

            //sDocumento = "XXX";

            sSql = string.Format("Set DateFormat YMD Exec spp_Mtto_RegistrosSanitarios  " + 
                " @Folio = '{0}', @IdLaboratorio = '{1}', @ClaveSSA = '{2}', @Consecutivo = '{3}', @Tipo = '{4}', @Año = '{5}', " +
                " @Vigencia = '{6}', @Status = '{7}', @Documento = '{8}', @NombreDocto = '{9}', @iOpcion = '{10}', " +
                " @IdPaisFabricacion = '{11}', @IdPresentacion = '{12}', @TipoCaducidad = '{13}', @Caducidad = '{14}', @FolioRegistroSanitario = '{15}' ",
                txtFolio.Text, txtIdLaboratorio.Text, txtClaveSSA.Text, "", "", "", sFechavigencia, 
                cboStatus.Data, sDocumento, sNombreDocto, iOpcion,
                cboPaisesDeFabricacion.Data, cboPresentaciones.Data, iTipoCaducidad, (int)nmAñosCaducidad.Value, sFolio);

            if (!leer.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                leer.Leer();

                txtFolio.Text = leer.Campo("Folio");

                bRegresa =  GuardarRegistroSanitario_CodigosEAN();
            }

            return bRegresa;
        }

        private bool GuardarRegistroSanitario_CodigosEAN()
        {
            bool bRegresa = true;
            string sFolio = txtFolioRegistroSanitario.Text;
            string sSql = "";

            for (int i = 1; grid.Rows >= i && bRegresa; i++)
            {
                if (grid.GetValueBool(i, 4))
                {
                    sSql = string.Format("Set DateFormat YMD Exec spp_Mtto_RegistrosSanitarios_CodigoEAN  @Folio = '{0}', @CodigoEAN = '{1}' ",
                        txtFolio.Text, grid.GetValue(i, 1));

                    if (!leer.Exec(sSql))
                    {
                        bRegresa = false;
                    }
                }
            }

            return bRegresa;
        }

        private void BuscarRegistro()
        {
            //// El documento se envia vacio para reducir el tiempo de carga de la información, si se desea descargar el documento este se obtiene al momento. 
            string sSql = string.Format(
                "Select  FechaUltimaActualizacion, Folio, FechaRegistro, IdLaboratorio, Laboratorio, IdClaveSSA_Sal, ClaveSSA, Descripcion, FolioRegistroSanitario, FechaVigencia, " +
                " '' as Documento, NombreDocto, Status, StatusRegistroAux, IdPaisFabricacion, NombrePais, IdPresentacion, DescripcionPresentacion, TipoCaducidad, Caducidad " + 
                "From vw_RegistrosSanitarios (NoLock) " + 
                "Where IdLaboratorio = '{0}' And ClaveSSA = '{1}'", txtIdLaboratorio.Text.Trim(), txtClaveSSA.Text.Trim());

            /* 
            sQuery = sInicio + string.Format(
                    "Select C.Folio, C.FechaRegistro, C.IdLaboratorio, L.Descripcion As Laboratorio, C.IdClaveSSA_Sal, ClaveSSA, S.Descripcion, \n " +
                    "   C.Consecutivo, C.Tipo, C.Año, C.FechaVigencia, \n" +
                    "   C.IdPaisFabricacion, PF.NombrePais, C.IdPresentacion, P.Descripcion as Descripcion, C.TipoCaducidad, C.Caducidad, \n" +
                    "   (case when {1} = 1 Then Documento else '' end) as Documento, NombreDocto, \n" +
                //"   cast('' as varchar(100)) as Documento, NombreDocto, " + 
                    "   C.Status, IsNull(R.Descripcion, 'Desconocido') as StatusRegistroAux \n" +
                    "From CatRegistrosSanitarios C (NoLock) \n" +
                    "Inner Join CatLaboratorios L (NoLock) On ( C.IdLaboratorio = L.IdLaboratorio ) \n" +
                    "Inner Join CatClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) \n" +
                    "Inner Join CatRegistrosSanitarios_PaisFabricacion PF (NoLock) On ( C.IdPaisFabricacion = PF.IdPais  )  \n" +
                    "Inner Join CatRegistrosSanitarios_Presentaciones P (NoLock) On ( C.IdPresentacion = P.IdPresentacion  )  \n" +
                    "Left Join vw_Status_RegistrosSanitarios R (NoLock) On ( C.Status = R.Status ) \n" +
                    "Where C.Folio =  '{0}'", Fg.PonCeros(Folio, 8), iDescarga); 
            */ 

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    txtFolio.Text = leer.Campo("Folio");
                    txtFolioRegistroSanitario.Text = leer.Campo("FolioRegistroSanitario");
                    dtpFechaVigencia.Value = leer.CampoFecha("FechaVigencia");
                    cboStatus.Data = leer.Campo("Status");

                    dtpUltimaActualizacionEnSistema.Value = leer.CampoFecha("FechaUltimaActualizacion");
                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
                    cboStatus.Data = leer.Campo("Status");
                    cboPaisesDeFabricacion.Data = leer.Campo("IdPaisFabricacion");
                    cboPresentaciones.Data = leer.Campo("IdPresentacion");
                    rdoAños.Checked = leer.CampoInt("TipoCaducidad") == 1;
                    rdoMeses.Checked = leer.CampoInt("TipoCaducidad") == 2;
                    nmAñosCaducidad.Value = leer.CampoDec("Caducidad");

                    dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
                    cboStatus.Data = leer.Campo("Status");
                    cboPaisesDeFabricacion.Data = leer.Campo("IdPaisFabricacion");
                    cboPresentaciones.Data = leer.Campo("IdPresentacion");
                    rdoAños.Checked = leer.CampoInt("TipoCaducidad") == 1;
                    rdoMeses.Checked = leer.CampoInt("TipoCaducidad") == 2;
                    nmAñosCaducidad.Value = leer.CampoDec("Caducidad");

                    
                    sArchivo = leer.Campo("Documento");
                    sNombreArchivo = leer.Campo("NombreDocto");
                    lblRutaDocto.Text = sNombreArchivo;

                    sDocumento = sArchivo;
                    sNombreDocto = sNombreArchivo;

                    if (sNombreArchivo.Trim() != "")
                    {
                        btnDescargar.Enabled = true;
                        btnAsignarDocto.Enabled = true;
                    }
                }
                else
                {
                    grid.Limpiar(true);

                    btnAsignarDocto.Enabled = true;
                    btnDescargar.Enabled = false;
                }
            }
            CargarCodigosEAN();
        }

        private void CargarDatosFolio()
        {
            txtFolio.Text = leer.Campo("Folio");
            txtFolio.Enabled = false;
            txtIdLaboratorio.Text = leer.Campo("IdLaboratorio");
            lblLaboratorio.Text = leer.Campo("Laboratorio");
            txtIdLaboratorio.Enabled = false;
            txtClaveSSA.Text = leer.Campo("ClaveSSA");
            txtClaveSSA.Enabled = false;
            lblDescripcionClaveSSA.Text = leer.Campo("Descripcion");
            txtFolioRegistroSanitario.Text = leer.Campo("FolioRegistroSanitario");
            //txtTipo.Text = leer.Campo("Tipo");
            //txtAño.Text = leer.Campo("Año");
            //txtFolioRegistroSanitario.Enabled = false;
            //txtTipo.Enabled = false;
            //txtAño.Enabled = false;
            dtpFechaVigencia.Value = leer.CampoFecha("FechaVigencia");

            dtpUltimaActualizacionEnSistema.Value = leer.CampoFecha("FechaUltimaActualizacion");
            dtpFechaRegistro.Value = leer.CampoFecha("FechaRegistro");
            cboStatus.Data = leer.Campo("Status");
            cboPaisesDeFabricacion.Data = leer.Campo("IdPaisFabricacion");
            cboPresentaciones.Data = leer.Campo("IdPresentacion");
            rdoAños.Checked = leer.CampoInt("TipoCaducidad") == 1;
            rdoMeses.Checked = leer.CampoInt("TipoCaducidad") == 2; 
            nmAñosCaducidad.Value = leer.CampoDec("Caducidad");


            sArchivo = leer.Campo("Documento");
            sNombreArchivo = leer.Campo("NombreDocto");
            lblRutaDocto.Text = sNombreArchivo;


            sDocumento = sArchivo;
            sNombreDocto = sNombreArchivo;

            CargarCodigosEAN();

            btnAsignarDocto.Enabled = true;

            if (sNombreArchivo.Trim() != "")
            {
                btnDescargar.Enabled = true;
            }
        }

        private void CargarCodigosEAN()
        {
            string sWhere = "";

            //if (txtFolio.Text != "*")
            //{
            //    sWhere = string.Format("And Folio = '{0}'", txtFolio.Text);
            //}

            string sSql = string.Format("Select P.CodigoEAN, P.DescripcionCorta As Descripcion, P.Presentacion, (Case When R.Status = 'A' Then 1 else 0 End) As TieneRegistro " +
                    "From vw_Productos_CodigoEAN P (NoLock) " +
                    "Left Join CatRegistrosSanitarios_CodigoEAN R (NoLock) On (R.IdProducto = P.IdProducto And R.CodigoEAN = P.CodigoEAN ) " +
                    "Where IdLaboratorio = '{0}' And ClaveSSA = '{1}' {2} " +
                    "Order By P.ClaveSSA, R.CodigoEAN  ",
                    Fg.PonCeros(txtIdLaboratorio.Text.Trim(), 4), txtClaveSSA.Text.Trim(), sWhere);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);                    
                }
            }
            else
            {
                Error.GrabarError(leer, "CargarCodigosEAN()");
                General.msjError("Ocurió un error al obtener los Codigos EAN");
            }
        }

        private void CargarCodigoEAN(string CodigoEAN)
        {
            string sSql = string.Format("Select P.ClaveSSA, p.IdProducto, P.CodigoEAN, P.DescripcionCorta As Descripcion, P.Presentacion " +
                            "From  vw_Productos_CodigoEAN P (NoLock) Where CodigoEAN = '{0}'", CodigoEAN);

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    grid.LlenarGrid(leer.DataSetClase);
                }
            }
            else
            {
                Error.GrabarError(leer, "CargarCodigoEAN()");
                General.msjError("Ocurió un error al obtener el Codigos EAN");
            }
        }

        private void CargaDatosLaboratorio()
        {
            //Se hace de esta manera para la ayuda.
            txtIdLaboratorio.Text = leer.Campo("IdLaboratorio");
            lblLaboratorio.Text = leer.Campo("Descripcion");
            txtIdLaboratorio.Enabled = false;

            if (txtClaveSSA.Text.Trim() != "")
            {
                BuscarRegistro();
            }

        }

        private void CargaDatosClaveSSA()
        {
            txtClaveSSA.Text = leer.Campo("ClaveSSA");
            txtClaveSSA.Enabled = false;
            lblDescripcionClaveSSA.Text = leer.Campo("Descripcion");

            if (txtIdLaboratorio.Text.Trim() != "")
            {
                BuscarRegistro();
            }

        }

        #endregion Funciones y Procedimientos

        #region Eventos

        #region Leer documento
        private void btnAsignarDocto_Click(object sender, EventArgs e)
        {

            file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Seleccione el archivo a cargar";
            file.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            file.Filter = "Documentos portables (*.pdf)| *.pdf";

            if (file.ShowDialog() == DialogResult.OK)
            {
                ////sDocumento = "";
                ////sNombreDocto = "";
                ////lblRutaDocto.Text = "";

                //sDocumento = Fg.ConvertirArchivoEnStringB64(file.FileName);
                FileInfo docto = new FileInfo(file.FileName);
                // sDocumento = Codificar(file.FileName); 

                if (!docto.Extension.ToUpper().Contains("PDF"))
                {
                    General.msjAviso("El documento seleccionado debe ser PDF, verifique.");
                }
                else
                {
                    if (docto.Length <= 0)
                    {
                        General.msjUser("El Archivo seleccionado se encuentra vacio, verifique.");
                    }
                    else
                    {
                        sDocumento = Fg.ConvertirArchivoEnStringB64(file.FileName);
                        sNombreDocto = docto.Name;
                        lblRutaDocto.Text = file.FileName;
                    }
                }
            }
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            bExisteArchivoParaDescarga = !(sArchivo == ""); 

            if (sArchivo == "")
            {
                leer.DataSetClase = Consultas.RegistrosSanitarios(txtFolio.Text.Trim(), true, "txtFolio_Validating()");
                if (leer.Leer())
                {
                    bExisteArchivoParaDescarga = true;
                    sArchivo = leer.Campo("Documento");
                    sNombreArchivo = leer.Campo("NombreDocto");

                    sDocumento = sArchivo;
                    sNombreDocto = sNombreArchivo;                    
                }
            }

            if ( bExisteArchivoParaDescarga )
            {
                GuardarArchivo();
            }

        }

        private void GuardarArchivo()
        {
            Folder = new FolderBrowserDialog();
            Folder.RootFolder = Environment.SpecialFolder.Desktop;
            Folder.ShowNewFolderButton = true;
            Folder.Description = "Direcrtorio donde se descargara el documento de Carta de Faltante";
            string sFileSalida = ""; 

            if (Folder.ShowDialog() == DialogResult.OK)
            {
                sFileSalida = string.Format("{0}__{1}__{2}______{3}", txtClaveSSA.Text, General.FechaYMD(dtpFechaVigencia.Value, ""), lblLaboratorio.Text, sNombreArchivo); ;
                //if (Decodificar(sNombreArchivo, Folder.SelectedPath, sArchivo))
                if (Fg.ConvertirStringB64EnArchivo(sFileSalida, Folder.SelectedPath, sArchivo, true))
                {
                    AgregarCadenaDeSeguridad(Path.Combine(Folder.SelectedPath, sFileSalida)); 
                    General.msjUser("Documento descargado satisfactoriamente.");
                    lblRutaDocto.Text = Folder.SelectedPath + sNombreArchivo;
                    General.AbrirDocumento(Path.Combine(Folder.SelectedPath, sFileSalida));
                }
            }
        }

        private void AgregarCadenaDeSeguridad(string File_In)
        {
            string sMarcaDeAgua = "";

            sKey = crypto.Encrypt(txtClaveSSA.Text + txtIdLaboratorio.Text + sFechaSistema, true);

            sKey = crypto.Encrypt(txtFolio.Text, true);
            sMarcaDeAgua = "ASUNTO REGULATORIO" + " " + sKey;
            sMarcaDeAgua = "ARC"; 

            iTextSharpUtil.AddTextWatermark(File_In, sMarcaDeAgua); 
        }
        #endregion Leer documento
        
        //private void txtConsecutivo_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtAño.Text.Trim() != "" && txtTipo.Text.Trim() != "" && txtClaveSSA.Text.Trim() != "" && txtIdLaboratorio.Text.Trim() != "")
        //    {
        //        BuscarRegistro();
        //    }
        //}

        //private void txtTipo_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtAño.Text.Trim() != "" && txtConsecutivo.Text.Trim() != "" && txtClaveSSA.Text.Trim() != "" && txtIdLaboratorio.Text.Trim() != "")
        //    {
        //        BuscarRegistro();
        //    }
        //}

        //private void txtAño_Validating(object sender, CancelEventArgs e)
        //{
        //    if (txtConsecutivo.Text.Trim() != "" && txtTipo.Text.Trim() != "" && txtClaveSSA.Text.Trim() != "" && txtIdLaboratorio.Text.Trim() != "")
        //    {
        //        BuscarRegistro();
        //    }
        //}
        
        private void txtIdLaboratorio_Validating(object sender, CancelEventArgs e)
        {
            string sCadena = "";

            leer = new clsLeer(ref myConn);

            if (txtIdLaboratorio.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Laboratorios(txtIdLaboratorio.Text.Trim(), "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosLaboratorio();
                }
            }
        }

        private void txtIdLaboratorio_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Laboratorios("txtId_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosLaboratorio();
                }
            }
        }

        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref myConn);

            string sCadena = "";

            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text.Trim(), true, "txtId_Validating");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosClaveSSA();
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            string sCadena = "";

            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.ClavesSSA_Sales("txtId_KeyDown");

                sCadena = Ayudas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargaDatosClaveSSA();
                }
            }
        }

        private void txtFolio_Validating(object sender, CancelEventArgs e)
        {
            leer = new clsLeer(ref myConn);

            string sCadena = "";

            if (txtFolio.Text.Trim() == "")
            {
                txtFolio.Text = "*";
                txtFolio.Enabled = false;
            }
            else
            {
                leer.DataSetClase = Consultas.RegistrosSanitarios(txtFolio.Text.Trim(), "txtFolio_Validating()");

                sCadena = Consultas.QueryEjecutado.Replace("'", "\"");

                if (leer.Leer())
                {
                    CargarDatosFolio();
                }
                else
                {
                    btnNuevo_Click(this, null);
                }
            }
        }

        #endregion Eventos

        private void rdoAños_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAños.Checked)
            {
                nmAñosCaducidad.Minimum = 0;
                nmAñosCaducidad.Maximum = 20; 
            }
        }

        private void rdoMeses_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMeses.Checked)
            {
                nmAñosCaducidad.Minimum = 0;
                nmAñosCaducidad.Maximum = 240;
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (Descargar_ListadoDeRegistrosSanitarios())
            {
                GenerarExcel_ListadoDeRegistrosSanitarios();
            }
        }

        #region Reporte Excel
        private bool Descargar_ListadoDeRegistrosSanitarios()
        {
            bool bRegresa = false;
            string sSql = "";


            sSql = "Exec spp_RPT_RegistrosSanitarios_Con_Diferencias ";

            if (!leerListadoDeRegistros.Exec(sSql))
            {
                Error.GrabarError(leerListadoDeRegistros, "btnEjecutar_Click");
                General.msjError("Ocurió un error al ejecutar la consulta");
            }
            else
            {
                if (!leerListadoDeRegistros.Leer())
                {
                    General.msjUser("No se encontro información de registros sanitarios, verifique.");
                }
                else
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private void GenerarExcel_ListadoDeRegistrosSanitarios()
        {
            bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Vales_Emitidos_Mes.xls", DatosCliente); 
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            string sNombreDocumento = string.Format("Listado de Ordenes de Compras Recepcionadas");
            string sNombreHoja = "Emitidos";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            clsLeer datosExportar = new clsLeer();
            DataSet dtDatos = new DataSet();

            {
                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;


                this.Cursor = Cursors.Default;

                if (leerListadoDeRegistros.Leer())
                {
                    if (excel.PrepararPlantilla(sNombreDocumento))
                    {
                        this.Cursor = Cursors.WaitCursor;


                        sNombreHoja = "Incidencias";
                        datosExportar.DataTableClase = leerListadoDeRegistros.Tabla(1);
                        sConcepto = string.Format("");

                        excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                        //excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                        excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                        iRenglon = 8;
                        excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, datosExportar.DataSetClase);

                        //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                        excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);



                        excel.CerraArchivo();

                        this.Cursor = Cursors.Default;

                        excel.AbrirDocumentoGenerado(true);
                    }
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void GenerarExcel_ListadoDeRegistrosSanitarios__OLD()
        {
            ////string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_RegistrosSanitarios_Incidencias.xlsx";
            ////this.Cursor = Cursors.WaitCursor;
            ////bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_RegistrosSanitarios_Incidencias.xlsx", datosCliente);
            ////clsLeer leerResultado = new clsLeer();

            ////if (!bRegresa)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            ////}
            ////else
            ////{

            ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////    xpExcel.AgregarMarcaDeTiempo = true;

            ////    this.Cursor = Cursors.Default;
            ////    if (xpExcel.PrepararPlantilla())
            ////    {
            ////        this.Cursor = Cursors.WaitCursor;
            ////        int iHoja = 1, iCol = 3;

            ////        leerListadoDeRegistros.RenombrarTabla(1, "Concentrado");
            ////        leerResultado.DataSetClase = leerListadoDeRegistros.DataSetClase;


            ////        leerListadoDeRegistros.DataTableClase = leerResultado.Tabla(1);
            ////        iHoja = 1;
            ////        xpExcel.GeneraExcel(iHoja, true);
            ////        xpExcel.NumeroDeRenglonesAProcesar = leerListadoDeRegistros.Registros;

            ////        xpExcel.Agregar("Fecha de impresión : " + General.FechaSistemaFecha.ToString(), 3, 2);
            ////        leerListadoDeRegistros.RegistroActual = 1;
            ////        for (int iRow = 7; leerListadoDeRegistros.Leer(); iRow++)
            ////        {
            ////            iCol = 2;
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("FolioRegistroSanitario"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("FechaVigencia"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("StatusRegistroAux"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("Vigente"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("ClaveSSA"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("Descripcion"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("IdLaboratorio"), iRow, iCol++);
            ////            xpExcel.Agregar(leerListadoDeRegistros.Campo("Laboratorio"), iRow, iCol++);

            ////            xpExcel.NumeroRenglonesProcesados++;
            ////        }

            ////        //// Finalizar el Proceso 
            ////        xpExcel.CerrarDocumento();


            ////        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////        {
            ////            xpExcel.AbrirDocumentoGenerado();
            ////        }
            ////    }
            ////}

            ////this.Cursor = Cursors.Default;
        }
        #endregion Reporte Excel
    }
}
