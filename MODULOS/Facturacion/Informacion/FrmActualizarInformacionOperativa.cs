using System;
using System.Collections;
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
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo;

using DllFarmaciaSoft;
using Dll_IFacturacion; 

namespace Facturacion.Informacion
{
    public partial class FrmActualizarInformacionOperativa : FrmBaseExt 
    {
        #region Redimension de controles
        int iAnchoPantalla = (int)(General.Pantalla.Ancho * 1.0);
        int iAltoPantalla = (int)(General.Pantalla.Alto * 0.80);
        int iAnchoListView = 0;
        int iAnchoListView_Ajuste = 0;
        int iDiferenciaListView = 0;

        int iAnchoFrameInformacion = 0;
        int iAnchoFrameInformacion_Ajuste = 0;
        int iDiferenciaFrameInformacion = 0;
        bool bColumnasAjustadas = false;

        int iMesesInformacion_A_Migrar__Default = 12;
        int iMesesInformacion_A_Migrar = 0; 
        #endregion Redimension de controles

        #region Enums 
        enum ColsBD
        {
            Registro = 1, BaseDeDatos = 2, Tamaño = 3, Fecha = 4, Porcentaje = 5, Status = 6 
        }

        enum ColsTablas
        {
            Tabla = 1, Registros = 2, RegistrosMigrar = 3, Status = 4 
        }
        #endregion Enums

        #region Declaracion de variables 
        clsConexionSQL cnn;
        clsDatosConexion datosCnn; 
        clsConexionSQL cnnBorrar;
        clsDatosConexion datosBorrar; 

        clsConexionSQL cnnTablas; 
        clsLeer leerListaTablas;
        clsLeer leerListaTablas_Base; 
        clsDatosConexion datosTablas;
        clsLeer leerTablasExcepcion; 

        clsLeer leerBorrar; 
        clsLeer leer; 
        clsLeer leerBDS;
        clsListView lstBD;
        clsListView lstTablas;
        clsRestore Restore = new clsRestore(General.DatosConexion);
        StreamWriter fileLog;
        bool bHabilitarLog = true;
        int iNumFiles = 0;
        string sFiltro_Where = "";

        Thread thProceso;
        Thread thProceso_FTP;
        clsIniciarConSO SO;
        string sAppName = "DemonioDB";
        string sAppRuta = Application.ExecutablePath;

        string sBD_Activa = "";
        int iBD_Activa = 0;
        int iTabla_Activa = 0;
        int iMeses_Migracion = 120; 
        string sDB_EnProceso = ""; 
        FileInfo fileDB_Activo; 

        string sFormato = "#,###,##0.#0";
        string sFormato01 = "#,###,##0.000#";
        string sFormatoAux = "#,###,##0";

        int iTiempo = 0;

        DataSet dtsFTP = new DataSet(); 
        DataSet dtsCentral = new DataSet();
        DataSet dtsRegional = new DataSet();
        bool bFTP_Central = false;
        bool bFTP_Regional = false;
        bool bFTP_Configurado = false;
        string sRuta_FTP = "";


        // bool bProcesoConfigurado = false;
        bool bConsultaDeInformacion = false; 
        bool bProcesando = false; 
        bool bIntegracionCompleta = false; 
        string sRutaBD = "";
        string sRutaBD_Proceso = "";
        string sRutaBD_Terminada = "";
        string sRutaBD_Error = "";
        string sRutaBD_Error_Descompresion = "";
        string sRutaDescompresion = "";
        string sRutaLog = ""; 
        int iIndex_0 = 4;
        // ArrayList pZIPs = new ArrayList(); 

        bool bExisteTabla_SKU = false; 

        string sTablasMigrar = " Select NombreTabla, cast(0 as float) as Registros, cast(0 as float) as RegistrosMigrar, '' as Status " + 
            " From CFGC_EnvioDetalles (NoLock) " + 
            " Where Status = 'A' Order By IdOrden ";
        #endregion Declaracion de variables
        
        public FrmActualizarInformacionOperativa()
        {
            InitializeComponent();          
            CheckForIllegalCrossThreadCalls = false;

            Fg.IniciaControles(); 

            SO = new clsIniciarConSO(sAppName, sAppRuta);

            General.DatosConexion.BaseDeDatos = "master";


            datosCnn = new clsDatosConexion();
            datosCnn.Servidor = General.DatosConexion.Servidor;
            datosCnn.BaseDeDatos = General.DatosConexion.BaseDeDatos;  //"master";
            datosCnn.Usuario = General.DatosConexion.Usuario;
            datosCnn.Password = General.DatosConexion.Password;
            datosCnn.Puerto = General.DatosConexion.Puerto;
            datosCnn.ForzarImplementarPuerto = true; 
            datosCnn.ConexionDeConfianza = false;
            datosCnn.ConectionTimeOut = TiempoDeEspera.SinLimite; 

            cnn = new clsConexionSQL(datosCnn); 
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;


            datosTablas = new clsDatosConexion(); 
            datosTablas.Servidor = General.DatosConexion.Servidor;
            datosTablas.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            datosTablas.Usuario = General.DatosConexion.Usuario;
            datosTablas.Password = General.DatosConexion.Password;
            datosTablas.Puerto = General.DatosConexion.Puerto;
            datosTablas.ForzarImplementarPuerto = true;
            datosTablas.ConexionDeConfianza = false;
            datosTablas.ConectionTimeOut = TiempoDeEspera.SinLimite; 


            leer = new clsLeer(ref cnn); 
            leerBDS = new clsLeer(ref cnn);
            leerTablasExcepcion = new clsLeer(ref cnn); 

            datosBorrar = new clsDatosConexion();
            datosBorrar.Servidor = General.DatosConexion.Servidor; 
            datosBorrar.BaseDeDatos = "master";
            datosBorrar.Usuario = General.DatosConexion.Usuario;
            datosBorrar.Password = General.DatosConexion.Password;
            datosBorrar.Puerto = General.DatosConexion.Puerto;
            datosBorrar.ForzarImplementarPuerto = true;
            datosBorrar.ConexionDeConfianza = false;


            cnnBorrar = new clsConexionSQL(datosBorrar);
            cnnBorrar.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnBorrar.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;


            leerBorrar = new clsLeer(ref cnnBorrar); 
            lstTablas = new clsListView(lstwTablasMigrar); 

            ////lstBD.OrdenarColumnas = false; 
            lstTablas.OrdenarColumnas = false;

            AjustarTamaño_Pantalla();


            btnIntegrarBD.Enabled = true; 
            btnLogErrores.Enabled = true;

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, General.DatosApp, this.Name);

            CargarBasesDeDatos(); 
            Cargar_TablasDeControl(); 
        }

        #region Form 
        private void AjustarTamaño_Pantalla()
        {
            //////iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            //////iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);

            //////this.Width = iAnchoPantalla;
            //////this.Height = iAltoPantalla;

            //////iDiferenciaListView = iAnchoListView_Ajuste - iAnchoListView; 
            //////iDiferenciaFrameInformacion = iAnchoFrameInformacion_Ajuste - iAnchoFrameInformacion;          
            ///

            ////General.Pantalla.AjustarAlto(this, 80);
        }

        private void AjustarColumnas()
        {
            int iAdicional = ((int)(iDiferenciaFrameInformacion *.85) / 2);
            ////int iAnchoBD = lstBD.AnchoColumna((int)ColsBD.BaseDeDatos) + iAdicional;
            ////int iAnchoStatus = lstBD.AnchoColumna((int)ColsBD.Status) + iAdicional;
            int iAnchoTabla = lstTablas.AnchoColumna((int)ColsTablas.Tabla) + (int)(iDiferenciaFrameInformacion * .90);


            if (!bColumnasAjustadas)
            {
                bColumnasAjustadas = true; 
                ////lstBD.AnchoColumna((int)ColsBD.BaseDeDatos, iAnchoBD); 
                ////lstBD.AnchoColumna((int)ColsBD.Status, iAnchoStatus); 
                lstTablas.AnchoColumna((int)ColsTablas.Tabla, iAnchoTabla);
            }

        }

        private void FrmActualizarInformacionOperativa_Load(object sender, EventArgs e)
        {
            ////chkInicioSO.Checked = SO.Existe();

            ////btnFTP.Enabled = false;
            ////btnAbrirFTP.Enabled = false;
            ////btnDepurarFTP.Enabled = false;

            ////CargarConfiguracion();
            ////chkLog.Checked = true;
            ////cboTiposMigracion.SelectedIndex = 1;
            ////cboItemsFTP.SelectedIndex = 1;
            ////cboItemsFTP_Copiado.SelectedIndex = iIndex_0;
            CargarListaBDS();

            RevisarConfiguracion_FTP();

            CargarBasesDeDatos(); 
        }

        private void FrmActualizarInformacionOperativa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    General.TerminarProceso("Servicio Integrador BD.exe");
                    Application.Exit();
                }
                catch
                {
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        ////private void chkInicioSO_CheckedChanged(object sender, EventArgs e)
        ////{
        ////    if (chkInicioSO.Checked)
        ////    {
        ////        SO.Agregar();
        ////    }
        ////    else
        ////    {
        ////        SO.Remover();
        ////    }
        ////}

        #endregion Form 

        #region Timer
        private void tmIntegrarBD_Tick(object sender, EventArgs e)
        {
            if (btnIntegrarBD.Enabled)
            {
                if (PreperarScript())
                {
                    btnNuevo.Enabled = false;
                    btnIntegrarBD.Enabled = false;
                    txtWhere.Enabled = false;

                    cboBasesDeDatosOrigen.Enabled = false;
                    cboBasesDeDatosDestino.Enabled = false;
                    cboTablasDeControl.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;

                    Application.DoEvents(); 

                    sFiltro_Where = txtWhere.Text.Trim();
                    TheadProceso();
                }
            }
        }
        #endregion Timer

        #region Funciones y Procedimientos Privados 
        private void Cargar_TablasDeControl()
        {
            cboTablasDeControl.Clear();
            cboTablasDeControl.Add("0", "<< Seleccione >>");
            cboTablasDeControl.Add("CFGS_EnvioCatalogos", "Catálogos");
            cboTablasDeControl.Add("CFGSC_EnvioDetallesVentas", "Información de detalles de operación");
            cboTablasDeControl.SelectedIndex =0;
        }

        private void CargarBasesDeDatos()
        {
            string sSql = "";
            string sBdSistemas = " 'master', 'model', 'msdb', 'tempdb' ";


            datosCnn = new clsDatosConexion();
            datosCnn = General.DatosConexion;

            cnn = new clsConexionSQL(datosCnn);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            leerBDS = new clsLeer(ref cnn);


            cboBasesDeDatosOrigen.Clear();
            cboBasesDeDatosOrigen.Add();
            cboBasesDeDatosDestino.Clear();
            cboBasesDeDatosDestino.Add();

            sSql = string.Format(
                "Select Name as DataName, Name \n" +
                "From sys.databases \n" +
                "where name in ( '{0}' ) \n" + 
                "order by Name ", DtIFacturacion.BaseDeDatosOperacion);
            if (leerBDS.Exec(sSql))
            {
                cboBasesDeDatosOrigen.Add(leerBDS.DataSetClase, true, "DataName", "Name");
                cboBasesDeDatosOrigen.SelectedIndex = 1;
            }


            sSql = string.Format(
                "Select Name as DataName, Name \n" +
                "From sys.databases \n" +
                "where name in ( '{0}' ) \n" +
                "order by Name ", General.DatosConexion.BaseDeDatos);

            if (leerBDS.Exec(sSql))
            {
                cboBasesDeDatosDestino.Add(leerBDS.DataSetClase, true, "DataName", "Name");
                cboBasesDeDatosDestino.SelectedIndex = 1;
            }
        }

        private void Configurar_MesesInformacionAMigrar()
        { 
        }

        private void CargarExcepciones()
        {
            string sSql = " Select NombreTabla, cast(0 as float) as Registros, cast(0 as float) as RegistrosMigrar, '' as Status " +
                " From CFG_DetallesExcepcionBDS (NoLock) " +
                " Where Status = 'A' Order By IdOrden ";

            ////if (!leerTablasExcepcion.Exec(sSql))
            ////{
            ////    Error.GrabarError(leerTablasExcepcion, "CargarExcepciones()"); 
            ////}
        }

        private void ConfigurarCarpetasDeTrabajo()
        {
            ////if (!Directory.Exists(sRutaBD))
            ////    Directory.CreateDirectory(sRutaBD);

            ////if (!Directory.Exists(sRutaBD_Proceso))
            ////    Directory.CreateDirectory(sRutaBD_Proceso);

            ////if (!Directory.Exists(sRutaBD_Error))
            ////    Directory.CreateDirectory(sRutaBD_Error);

            ////if (!Directory.Exists(sRutaBD_Error_Descompresion))
            ////    Directory.CreateDirectory(sRutaBD_Error_Descompresion); 

            ////if (!Directory.Exists(sRutaBD_Terminada))
            ////    Directory.CreateDirectory(sRutaBD_Terminada);

            ////if (!Directory.Exists(sRutaDescompresion)) 
            ////    Directory.CreateDirectory(sRutaDescompresion);

            if (!Directory.Exists(sRutaLog))
                Directory.CreateDirectory(sRutaLog); 
        }

        private DataTable PreparaListaBDS()
        {
            DataTable dtBDS = new DataTable("BasesDeDatos");

            dtBDS.Columns.Add("Registro", Type.GetType("System." + TypeCode.Int32.ToString()));
            dtBDS.Columns.Add("BaseDeDatos", Type.GetType("System." + TypeCode.String.ToString()));
            dtBDS.Columns.Add("Tamaño", Type.GetType("System." + TypeCode.Double.ToString()));
            dtBDS.Columns.Add("Fecha", Type.GetType("System." + TypeCode.String.ToString()));
            dtBDS.Columns.Add("Porcentaje", Type.GetType("System." + TypeCode.Double.ToString()));
            dtBDS.Columns.Add("Status", Type.GetType("System." + TypeCode.String.ToString())); 

            return dtBDS.Clone(); 
        }

        private void CargarListaBDS()
        {
            //////DataTable dtDBS = PreparaListaBDS();
            //////// FileInfo f; 
            //////string sTitulo_BDS = "Listado de Bases de Datos"; 

            //////try
            //////{
            //////    iNumFiles = 0; 
            //////    CargarArchivos(ref dtDBS, "sii");
            //////    CargarArchivos(ref dtDBS, "bak"); 
            //////}
            //////catch { }

            //////leerBDS.DataTableClase = dtDBS; 
            //////lstBD.LimpiarItems(); 
            //////lstBD.CargarDatos(leerBDS.DataSetClase);
            //////AjustarColumnas();

            //////FrameBasesDeDatos.Text = string.Format("{0} : {1}", sTitulo_BDS, lstBD.Registros); 
        }

        private void CargarArchivos(ref DataTable dtDBS, string Extencion)
        {
            FileInfo f;
            string sPatron = "*." + Extencion; 
            try
            {
                foreach (string sFileDB in Directory.GetFiles(sRutaBD, sPatron))
                {
                    iNumFiles++; 
                    f = new FileInfo(sFileDB);
                    object[] obj = { iNumFiles, f.Name, ((f.Length / 1024.0000) / 1024.0000).ToString(sFormato), f.LastAccessTime.ToString(), 0, "" };
                    dtDBS.Rows.Add(obj);
                }
            }
            catch { }
        }

        #endregion Funciones y Procedimientos Privados

        #region Proceso de Integracion 
        private void btnLogErrores_Click(object sender, EventArgs e)
        {
            FrmListadoDeErrores ex = new FrmListadoDeErrores();
            ex.ShowDialog();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            TheadProceso_Informacion();
        }

        private void btnIntegrarBD_Click(object sender, EventArgs e)
        {
            tmIntegrarBD_Tick(null, null); 
        }

        private void InicializarPantalla()
        {
            CargarListaBDS();

            tmRevisionFTP.Stop();
            tmRevisionFTP.Enabled = false;


            ModificarControles(true);
            btnIntegrarBD.Enabled = false;
            iMeses_Migracion = 120;

            //////chkLog.Checked = true;
            //////cboTiposMigracion.SelectedIndex = 1;
            //////cboItemsFTP.SelectedIndex = 1;
            //////cboItemsFTP_Copiado.SelectedIndex = iIndex_0; 
            lstTablas.LimpiarItems();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            InicializarPantalla();
        }

        private void ModificarControles(bool Enable)
        {
            btnNuevo.Enabled = Enable;
            btnEjecutar.Enabled = Enable; 
            btnIntegrarBD.Enabled = Enable;
            txtWhere.Enabled = Enable;

            cboBasesDeDatosOrigen.Enabled = Enable;
            cboBasesDeDatosDestino.Enabled = Enable;
            cboTablasDeControl.Enabled = Enable;
            button1.Enabled = Enable;
            button2.Enabled = Enable;
        }

        private void TheadProceso()
        {
            CargarListaBDS();

            ModificarControles(false);

            lstTablas.LimpiarItems(); 
            Thread.Sleep(50);

            bConsultaDeInformacion = false;
            bProcesando = true;

            tmRevisionFTP.Enabled = true;
            tmRevisionFTP.Interval = 500;
            tmRevisionFTP.Start();
            Application.DoEvents(); 

            thProceso = new Thread(this.IniciaProceso);
            thProceso.Name = "Integrando Información";
            thProceso.Start();
        }

        private void TheadProceso_Informacion()
        {
            CargarListaBDS();

            ModificarControles(false);
            btnEjecutar.Enabled = false;
            btnIntegrarBD.Enabled = false;

            lstTablas.LimpiarItems();
            Thread.Sleep(50);

            bConsultaDeInformacion = true;
            bProcesando = true;

            tmRevisionFTP.Enabled = true;
            tmRevisionFTP.Interval = 500;
            tmRevisionFTP.Start();
            Application.DoEvents();

            thProceso = new Thread(this.IniciarProceso_Informacion);
            thProceso.Name = "Obteniendo Información";
            thProceso.Start();
        }

        #region Actualizar Datos Pantalla
        private void ActualizarStatusBD(string Mensaje)
        {
            ////lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Status) - 1].Text = Mensaje;
        }

        private void ActualizarNombreBD(string Nombre)
        {
            ////lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.BaseDeDatos) - 1].Text = Nombre;
        }

        private void ActualizarTamañoBD(double Porcentaje)
        {
            ////lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Tamaño) - 1].Text = Porcentaje.ToString(sFormato);
        }

        private void ActualizarPorcentajeBD(double Porcentaje)
        {
            ////lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Porcentaje) - 1].Text = Porcentaje.ToString(sFormato01);
        }

        private void ActualizarStatusTabla(string Mensaje) 
        {
            lstwTablasMigrar.Items[iTabla_Activa - 1].SubItems[((int)ColsTablas.Status) - 1].Text = Mensaje;
        }

        private void ActualizarRegistros(string Mensaje)
        {
            lstwTablasMigrar.Items[iTabla_Activa - 1].SubItems[(int)ColsTablas.Registros - 1].Text = Mensaje;
        }

        private void ActualizarRegistros_A_Migrar(string Mensaje)
        {
            lstwTablasMigrar.Items[iTabla_Activa - 1].SubItems[(int)ColsTablas.RegistrosMigrar - 1].Text = Mensaje;
        }
        #endregion Actualizar Datos Pantalla 

        #region Log 
        private void GenerarLog()
        {
            if (bHabilitarLog)
            {
                sRutaLog = Application.StartupPath + @"\LogIntegracion\";
                ConfigurarCarpetasDeTrabajo(); 

                string sName = sRutaLog + @"\" + cboBasesDeDatosOrigen.Data + ".txt";
                fileLog = new StreamWriter(sName);
            }
        }

        private void Log(string Mensaje)
        {
            if (bHabilitarLog)
            {
                fileLog.WriteLine(Mensaje);
            }
        }

        private void CerrarLog()
        {
            if (bHabilitarLog)
            {
                fileLog.Close();
            }
        }
        #endregion Log

        private bool PreperarScript() 
        {
            bool bRegresa = true; 
            clsLeer leerLocal;


            datosCnn = new clsDatosConexion();
            datosCnn.Servidor = General.DatosConexion.Servidor;
            datosCnn.BaseDeDatos = cboBasesDeDatosDestino.Data;
            datosCnn.Usuario = General.DatosConexion.Usuario;
            datosCnn.Password = General.DatosConexion.Password;
            datosCnn.Puerto = General.DatosConexion.Puerto; 
            //datosCnn.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;
            datosCnn.ConectionTimeOut = TiempoDeEspera.SinLimite;
            datosCnn.ForzarImplementarPuerto = true;

            cnn = new clsConexionSQL(datosCnn);
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
            cnn.EsEquipoDeConfianza = true; 

            leerLocal = new clsLeer(ref cnn); 


            string sSql = "";
            string sFile = Facturacion.Properties.Resources.spp_CFG_Script_Integrar_BaseDeDatos;
            clsScritpSQL SQL_FILE = new clsScritpSQL(sFile, false);

            foreach (string sFragmento in SQL_FILE.ListaScripts)
            {
                sSql = "" + sFragmento;
                if (!leerLocal.Exec(sSql))
                {
                    bRegresa = false; 
                    break;
                }
            }

            if (!bRegresa)
            {
                // General.msjError("Ocurrió un error al preparar la BD destino."); 
                Error.GrabarError(leerLocal, "PreperarScript()"); 
            }

            return bRegresa;
        }

        private void IniciarProceso_Informacion()
        {
            sBD_Activa = "";
            iBD_Activa = 0;
            lstTablas.LimpiarItems();



            bIntegracionCompleta = false;
            sBD_Activa = cboBasesDeDatosOrigen.Data;
            iBD_Activa++;


            GenerarLog();
            lstTablas.LimpiarItems();
            MontarRespaldo(false);

            ModificarControles(true);
            CerrarLog();
            bProcesando = false;


            btnEjecutar.Enabled = false;
            btnIntegrarBD.Enabled = true;
        }
        
        private void IniciaProceso()
        {

            sBD_Activa = ""; 
            iBD_Activa = 0;
            lstTablas.LimpiarItems();

            bIntegracionCompleta = false;
            sBD_Activa = cboBasesDeDatosOrigen.Data; 
            iBD_Activa++;


            GenerarLog(); 
            lstTablas.LimpiarItems();
            MontarRespaldo(true);

            ////////// Finalizar proceso 
            ////if (bIntegracionCompleta)
            ////{
            ////    General.msjUser("Migración terminada satisfactoriamente.");
            ////}
            ////else
            ////{
            ////    General.msjUser("Ocurrió un error al migrar la información.");
            ////    ActualizarStatusBD("Ocurrió un error al migrar la información.");
            ////}

            ////btnNuevo.Enabled = true;
            ////btnIntegrarBD.Enabled = true;
            ////txtWhere.Enabled = true;


            ModificarControles(true);

            ////btnNuevo.Enabled = true;
            ////btnIntegrarBD.Enabled = true;
            ////txtWhere.Enabled = true;

            ////cboBasesDeDatosOrigen.Enabled = true;
            ////cboBasesDeDatosDestino.Enabled = true;
            ////cboTablasDeControl.Enabled = true;
            ////button1.Enabled = true;
            ////button2.Enabled = true;


            CerrarLog();


            bProcesando = false;

            //lstBD.LimpiarItems();
            //lstTablas.LimpiarItems(); 

            ////CargarConfiguracion();
            //CargarListaBDS();
            //lstTablas.LimpiarItems(); 
        } 

        private bool Descomprimir() 
        {
            bool bRegresa = true; 

            return bRegresa; 
        }

        private bool MontarRespaldo(bool EjecutarMigracion) 
        {
            bool bRegresa = false;
            sTablasMigrar = cboTablasDeControl.Data;

            sTablasMigrar = string.Format(" Select NombreTabla, cast(0 as float) as Registros, cast(0 as float) as RegistrosMigrar, '' as Status " +
                " From {0} (NoLock) " +
                " Where Status = 'A' Order By IdOrden ", cboTablasDeControl.Data);

            lstTablas.LimpiarItems();

            sDB_EnProceso = cboBasesDeDatosOrigen.Data;
            sDB_EnProceso = cboBasesDeDatosDestino.Data;
            ActualizarNombreBD(sDB_EnProceso);

            datosTablas.BaseDeDatos = sDB_EnProceso;
            cnnTablas = new clsConexionSQL(datosTablas);
            cnnTablas.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnTablas.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnTablas.FormatoDeFecha = FormatoDeFecha.Ninguno;
            cnnTablas.EsEquipoDeConfianza = true;


            leerListaTablas = new clsLeer(ref cnnTablas);
            leerListaTablas_Base = new clsLeer(ref cnnTablas); 

            cnnTablas.Abrir(); 
            Thread.Sleep(2000);

            ActualizarStatusBD("Migrando información");
            if (leerListaTablas_Base.Exec(sTablasMigrar))
            {
                lstTablas.CargarDatos(leerListaTablas_Base.DataSetClase);
                if (ObtenerRegistrosA_Procesar())
                {
                    if (!EjecutarMigracion)
                    {
                        bIntegracionCompleta = true;
                    }
                    else 
                    {
                        bIntegracionCompleta = MigrarInformacion();
                    }
                }
            }
            cnn.Cerrar(); 

            // Eliminar todos los objetos 
            Restore = null; 
            cnnTablas = null;
            leerListaTablas = null;
            leerListaTablas_Base = null; 

            bRegresa = bIntegracionCompleta; 
            return bRegresa; 
        }

        private bool verificaExistaTabla(string Tabla, bool EsMigracion)
        {
            bool bRegresa = false;
            string sSql = string.Format("If Exists ( Select Name From [{0}].dbo.Sysobjects (NoLock) Where Name = '{1}' and xType = 'U' ) Select 1 as Existe ", sDB_EnProceso, Tabla);

            if ( leerListaTablas.Exec(sSql) )
            {
                bRegresa = leerListaTablas.Leer(); 
            }

            if(bRegresa && EsMigracion)
            {
                if(Tabla.ToUpper() == "FarmaciaProductos_Skus".ToUpper())
                {
                    bExisteTabla_SKU = true; 
                    MigrarInformacion_SKU();
                }
            }

            return bRegresa;
        }

        private bool MigrarInformacion_SKU()
        {
            bool bRegresa = false;
            string sSql = 
                string.Format(
                "Insert Into [{0}].dbo.[FarmaciaProductos_Skus] ( IdEmpresa, IdEstado, IdFarmacia, IdTipoMovto_Inv, Folio, FechaRegistro, SKU, HostName_Terminal  ) \n" +
                "Select IdEmpresa, IdEstado, IdFarmacia, IdTipoMovto_Inv, Folio, FechaRegistro, SKU, HostName_Terminal \n" +
                "From [{1}].dbo.[FarmaciaProductos_Skus] Bd_O(NoLock) \n" +
                "Where Not Exists(Select * From [{0}].dbo.[FarmaciaProductos_Skus] Bd_D (NoLock) Where Bd_D.SKU = Bd_O.SKU )  \n", 
                cboBasesDeDatosDestino.Data, cboBasesDeDatosOrigen.Data);

            if(!leerListaTablas.Exec(sSql))
            {
                Error.GrabarError(leerListaTablas, "MigrarInformacion_SKU()");
            }
            else 
            {
                bRegresa = leerListaTablas.Leer();
            }

            return bRegresa;
        }

        private bool DepurarInformacion_SKU()
        {
            bool bRegresa = false;
            string sSql =
                string.Format(
                "Delete [{0}].dbo.[FarmaciaProductos_Skus] \n" +
                "From [{0}].dbo.[FarmaciaProductos_Skus] Bd_O (NoLock) \n" +
                "Where Not Exists(Select * From [{0}].dbo.[FarmaciaProductos_CodigoEAN_Lotes] Bd_L (NoLock) Where Bd_L.SKU = Bd_O.SKU)  \n",
                cboBasesDeDatosDestino.Data);

            if(!leerListaTablas.Exec(sSql))
            {
                Error.GrabarError(leerListaTablas, "DepurarInformacion_SKU()"); 
            }
            else 
            {
                bRegresa = leerListaTablas.Leer();
            }

            return bRegresa;
        }


        private string verificaExistaTabla_FechaControl(string Tabla)
        {
            string sRegresa = "";
            //////string sSql = string.Format("If Exists ( Select So.Name From {0}.dbo.Sysobjects So (NoLock) " + 
            //////    " Inner Join Syscolumns sc On ( So.Id = SC.Id ) " +  
            //////    " Where So.Name = '{1}' and Sc.Name = 'FechaControl' and So.xType = 'U' ) Select 1 as Existe ", sDB_EnProceso, Tabla);

            //////if (leerListaTablas.Exec(sSql))
            //////{
            //////    if (leerListaTablas.Leer())
            //////    {
            //////        try
            //////        {
            //////            sRegresa = string.Format("Where convert(varchar(10), FechaControl, 120) >= convert(varchar(10), dateadd(mm, -1 * {0}, getdate()), 120) ",
            //////                iMeses_Migracion);
            //////        }catch 
            //////        {
            //////        }
            //////    } 
            //////}

            return sRegresa;
        }

        private bool validarProcesarTabla(string Tabla)
        {
            bool bRegresa = true;

            ////if (!verificaExistaTabla(Tabla))
            ////{
            ////    bRegresa = false;
            ////}

            if (bRegresa)
            {
                bRegresa = PreparaTablaMigrarTipo(Tabla)== 0 ? false: true; 
            }

            return bRegresa; 
        }

        private bool ObtenerRegistrosA_Procesar() 
        {
            bool bRegresa = true; 
            cnnTablas = new clsConexionSQL(datosTablas);
            cnnTablas.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnTablas.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnTablas.FormatoDeFecha = FormatoDeFecha.Ninguno;
            cnnTablas.EsEquipoDeConfianza = true;

            leerListaTablas = new clsLeer(ref cnnTablas);
            
            
            string sSQL = "";
            string sTabla = ""; 
            int iRegistros = 0;
            int iRegistrosMigrar = 0;
            string sFiltro_FechaControl = "";
            string sFiltro_Where = txtWhere.Text.Trim() != "" ? string.Format("Where {0} ", txtWhere.Text) : "";

            for(int i = 1; i <= lstwTablasMigrar.Items.Count; i++)
            {
                lstTablas.ColorRowsTexto(i, Color.Black);
            }

                iTabla_Activa = 0; 
            for (int i = 0; i <= lstwTablasMigrar.Items.Count - 1; i++)
            {
                iRegistros = 0;
                iRegistrosMigrar = 0;
                sTabla = lstwTablasMigrar.Items[i].Text;
                iTabla_Activa = i + 1;
                lstTablas.ColorRowsTexto(i + 1, Color.Blue);

                if (verificaExistaTabla(sTabla, false))
                {
                    sFiltro_FechaControl = verificaExistaTabla_FechaControl(sTabla); 
                    sSQL = string.Format("Select count(*) From [{0}]..[{1}] Bd_O (NoLock) {2} {3} ", cboBasesDeDatosOrigen.Data, sTabla, sFiltro_FechaControl, sFiltro_Where);
                    if (!leerListaTablas.Exec(sSQL))
                    {
                        //bRegresa = false;
                        Error.GrabarError(datosTablas.BaseDeDatos + " : " + leerListaTablas.Error.Message + "   " + sSQL, "ObtenerRegistrosA_Procesar()");
                        lstTablas.ColorRowsTexto(i + 1, Color.Red); 
                        //break;
                    }
                    else
                    {
                        leerListaTablas.Leer();
                        iRegistros = leerListaTablas.CampoInt(1);

                        // sSQL = string.Format("Select count(*) From {0} (NoLock) Where Actualizado in ( 0, 2) ", lstwTablasMigrar.Items[i].Text);
                        sSQL = PreparaTablaMigrar(sTabla);
                        sSQL = string.Format("Select count(*) From [{0}]..[{1}] Bd_O (NoLock) {2} {3} ", cboBasesDeDatosDestino.Data, sTabla, sFiltro_FechaControl, sFiltro_Where);

                        if (!leerListaTablas.Exec(sSQL))
                        {
                            //bRegresa = false;
                            Error.GrabarError(datosTablas.BaseDeDatos + " : " + leerListaTablas.Error.Message + "   " + sSQL, "ObtenerRegistrosA_Procesar()");
                            lstTablas.ColorRowsTexto(i + 1, Color.Red);
                            //break;
                        }
                        else
                        {
                            leerListaTablas.Leer();
                            iRegistrosMigrar = leerListaTablas.CampoInt(1);
                        }
                    }
                }

                ActualizarRegistros(iRegistros.ToString(sFormatoAux)); 
                ActualizarRegistros_A_Migrar(iRegistrosMigrar.ToString(sFormatoAux));
                ////lstwTablasMigrar.Items[i].SubItems[(int)ColsTablas.Registros].Text = iRegistros.ToString(sFormatoAux);
                ////lstwTablasMigrar.Items[i].SubItems[(int)ColsTablas.RegistrosMigrar].Text = iRegistrosMigrar.ToString(sFormatoAux);
            }

            return bRegresa; 
        }

        private string PreparaTablaMigrar(string Tabla)
        {
            string sRegresa = "";
            clsLeer ex = new clsLeer();
            string sFiltro_Where = ""; // txtWhere.Text.Trim() != "" ? string.Format("Where {0} ", txtWhere.Text) : "";

            sRegresa = ""; //// cboTiposMigracion.SelectedIndex == 0 ? " Where Actualizado in ( 0, 2) " : "";

            ////ex.DataRowsClase = leerTablasExcepcion.DataTableClase.Select(string.Format(" NombreTabla = '{0}' ", Tabla));
            ////if (ex.Leer())
            ////{
            ////    sRegresa = " Where Actualizado in ( 0, 2) ";
            ////}

            sRegresa = string.Format("Select count(*) From [{0}] Bd_O (NoLock) {1} ", Tabla, sFiltro_Where);  
            return sRegresa; 
        }

        private int PreparaTablaMigrarTipo(string Tabla)
        {
            int iRegresa = 1;
            clsLeer ex = new clsLeer();

            ////ex.DataRowsClase = leerTablasExcepcion.DataTableClase.Select(string.Format(" NombreTabla = '{0}' ", Tabla));
            ////if (ex.Leer())
            ////{
            ////    iRegresa = 0; 
            ////}

            return iRegresa;
        }

        private bool MigrarInformacion() 
        {
            bool bRegresa = true;

            datosCnn.BaseDeDatos = cboBasesDeDatosDestino.Data;

            cnnTablas = new clsConexionSQL(datosCnn);
            cnnTablas.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnTablas.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnTablas.FormatoDeFecha = FormatoDeFecha.Ninguno;
            cnnTablas.EsEquipoDeConfianza = true;

            leerListaTablas = new clsLeer(ref cnnTablas);
            
            string sSQL = ""; 
            double iRegistros = (double)lstwTablasMigrar.Items.Count;
            double dPorcentaje = 0;
            bool bSoloDiferencias = false;
            int iSoloDiferencias = 0;
            string sTabla = "";

            for(int i = 1; i <= lstwTablasMigrar.Items.Count; i++)
            {
                lstTablas.ColorRowsTexto(i, Color.Black);
            }

            bExisteTabla_SKU = false;
            iTabla_Activa = 0; 
            for (int i = 0; i <= lstwTablasMigrar.Items.Count - 1; i++)
            {
                sTabla = lstwTablasMigrar.Items[i].Text;

                if (verificaExistaTabla(sTabla, true))
                {
                    iTabla_Activa = i + 1;
                    if (!validarProcesarTabla(sTabla))
                    {
                        ActualizarStatusTabla("Procesada");
                    }
                    else
                    {
                        /* 
                            @BaseDeDatosOrigen varchar(100) = 'SII_11_0003__CEDIS_GTO', 
                            @BaseDeDatosDestino varchar(100) = 'SII_Regional_Guanajuato_2K131125', 
	                        @BaseDeDatosEstructura varchar(100) = 'SII_Regional_Guanajuato_2K131125', 	     
                            @Tabla varchar(50) = 'VentasEnc', 
	                        @SalidaFinal varchar(8000) = '' output, 
                            @Update bit = true, 
                            @SoloDiferencias bit = true, 
                            @Meses_FechaControl int = 2   
                        */
                        sSQL = string.Format("\n\nDeclare @sSQL varchar(max) \n\n");

                        sSQL += string.Format("Exec [{1}].dbo.spp_CFG_Script_Integrar_BaseDeDatos \n" + 
                            "\t@BaseDeDatosOrigen = '[{0}]', \n" + 
                            "\t@BaseDeDatosDestino = '[{1}]', \n" + 
                            "\t@BaseDeDatosEstructura = '[{1}]', \n" + //// El destino es la base para la replicación
                            "\t@ValidarEstructura_Origen = '1', \n" + 
                            "\t@Tabla = '{2}', \n" + 
                            "\t@SalidaFinal = @sSQL output, \n" + 
                            "\t@Update = '{3}', \n" + 
                            "\t@SoloDiferencias = '{4}', \n" + 
                            "\t@Meses_FechaControl = '{5}', \n" + 
                            "\t@Criterio = [ {6} ] \n\n", 
                            cboBasesDeDatosOrigen.Data, cboBasesDeDatosDestino.Data, sTabla, 1, PreparaTablaMigrarTipo(sTabla), iMeses_Migracion, sFiltro_Where); 
                        
                            
                        sSQL += string.Format("Exec(@sSQL) \n\n", sFiltro_Where); 
                        sSQL += string.Format("Select @sSQL as SQL, @@rowcount as Registros "); 

                        // iTabla_Activa = i + 1;
                        ActualizarStatusTabla("Migrando información"); 
                        ActualizarPorcentajeBD(dPorcentaje); 

                        if (!leerListaTablas.Exec(sSQL)) 
                        {
                            ////leerListaTablas.Leer();

                            Log("");
                            Log("");
                            Log("Error");
                            Log(sSQL);

                            ActualizarStatusTabla("Error");
                            Error.GrabarError(leerListaTablas, "MigrarInformacion()");
                            bRegresa = false;

                            lstTablas.ColorRowsTexto(i + 1, Color.Red);

                            //break;
                        }
                        else
                        {
                            leerListaTablas.Leer();
                            Log(leerListaTablas.Campo("SQL"));
                            Log("Registros afectados : " + leerListaTablas.Campo("Registros"));
                            Log("");
                            Log("");

                            lstTablas.ColorRowsTexto(i + 1, Color.DarkBlue);
                            ActualizarStatusTabla("Procesada");
                        }
                    }
                }

                dPorcentaje = (iTabla_Activa / iRegistros) * 100.00; 
                ActualizarPorcentajeBD(dPorcentaje); 
            }


            if(bExisteTabla_SKU)
            {
                DepurarInformacion_SKU();
            }


            cnnTablas = null;
            leerListaTablas = null;


            return bRegresa; 
        }

        private void KillProcesos()
        {
            cnnBorrar = new clsConexionSQL(datosBorrar);
            cnnBorrar.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnnBorrar.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnnBorrar.FormatoDeFecha = FormatoDeFecha.Ninguno;
            cnnBorrar.EsEquipoDeConfianza = true;

            leerBorrar = new clsLeer(ref cnnBorrar);
            clsLeer leerAux = new clsLeer(); 

            string sSql = string.Format("", sDB_EnProceso);

            if (leerBorrar.Exec("Exec sp_who"))
            {
                leerAux.DataRowsClase = leerBorrar.DataTableClase.Select(string.Format(" DbName = '{0}' ", sDB_EnProceso));
                while (leerAux.Leer())
                {
                    sSql = string.Format("Kill {0}", leerAux.Campo("SPID"));
                    leerBorrar.Exec(sSql); 
                }
            }
        }

        private string FormatearNombreBD(string NombreBaseDeDatos)
        {
            string sRegresa = NombreBaseDeDatos;
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            sRegresa = sRegresa.Replace("-", "_");
            sRegresa = sRegresa.Replace(" ", "_");

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
            }

            NombreBaseDeDatos = NombreBaseDeDatos.Replace("-", "_"); 
            return sRegresa;
        }
        #endregion Proceso de Integracion

        #region Configuracion FTP 
        private void RevisarConfiguracion_FTP()
        {
            dtsCentral = new DataSet();
            dtsRegional = new DataSet(); 
            bFTP_Central = false; 
            bFTP_Regional = false;

            tmRevisionFTP.Enabled = false;
            tmRevisionFTP.Stop(); 

            GetConfiguracionFTP(1);
            GetConfiguracionFTP(2);

            if (bFTP_Central && bFTP_Regional)
            {
            }
            else
            {
                if (bFTP_Central)
                {
                    ConfigurarFTP(dtsCentral); 
                }

                if (bFTP_Regional)
                {
                    ConfigurarFTP(dtsRegional); 
                }
            }
        }

        private void ConfigurarFTP(DataSet Datos_FTP)
        {
        }

        private bool GetConfiguracionFTP(int Tipo)
        {
            bool bRegresa = false;

            return bRegresa; 
        } 
        #endregion Configuracion FTP

        #region Procesos FTP 
        private void btnFTP_Click(object sender, EventArgs e)
        {
            //////tmRevisionFTP_Tick(null, null); 
        }

        ////private void tmRevisionFTP_Tick(object sender, EventArgs e)
        ////{
        ////    if (btnFTP.Enabled)
        ////    {
        ////        btnNuevo.Enabled = false;
        ////        btnIntegrarBD.Enabled = false;
        ////        btnFTP.Enabled = false;
        ////        btnDepurarFTP.Enabled = false; 
        ////        TheadProceso_FTP(); 
        ////    } 
        ////}

        //////private void TheadProceso_FTP()
        //////{
        //////    thProceso_FTP = new Thread(this.ProcesarFTP);
        //////    thProceso_FTP.Name = "Revisar FTP [Bases De Datos]";
        //////    thProceso_FTP.Start();
        //////}

        ////private void ProcesarFTP()
        ////{ 
        ////    // sRutaBD 
        ////    clsFTP_BaseDeDatos f = new clsFTP_BaseDeDatos(sRuta_FTP, sRutaBD);
        ////    f.Dias_Integracion = Convert.ToInt32(cboItemsFTP_Copiado.Text); 
        ////    f.Procesar();

        ////    btnNuevo.Enabled = true;
        ////    btnIntegrarBD.Enabled = true;
        ////    btnFTP.Enabled = true;
        ////    btnDepurarFTP.Enabled = true; 

        ////    tmIntegrarBD_Tick(null, null); 
        ////}

        //////private void btnDepurarFTP_Click(object sender, EventArgs e)
        //////{
        //////    //tmDepurarFTP_Tick(null, null);
            
        //////    btnNuevo.Enabled = false;
        //////    btnIntegrarBD.Enabled = false;
        //////    btnFTP.Enabled = false;
        //////    btnDepurarFTP.Enabled = false;
        //////    TheadProceso_FTP_Depurar();
        //////}

        private void tmDepurarFTP_Tick(object sender, EventArgs e)
        {
            //////if (btnDepurarFTP.Enabled)
            //////{
            //////    btnNuevo.Enabled = false;
            //////    btnIntegrarBD.Enabled = false;
            //////    btnFTP.Enabled = false;
            //////    btnDepurarFTP.Enabled = false;
            //////    TheadProceso_FTP_Depurar();
            //////} 
        }

        //////private void TheadProceso_FTP_Depurar()
        //////{
        //////    thProceso_FTP = new Thread(this.ProcesarFTP_Depurar);
        //////    thProceso_FTP.Name = "Depurar Directorio FTP [Bases De Datos]";
        //////    thProceso_FTP.Start();
        //////}

        //////private void ProcesarFTP_Depurar()
        //////{
        //////    // sRutaBD 
        //////    clsFTP_BaseDeDatos f = new clsFTP_BaseDeDatos(sRuta_FTP);
        //////    f.Depurar_NumArchivos = Convert.ToInt32(cboItemsFTP.Text); 
        //////    f.Depurar();

        //////    btnNuevo.Enabled = true;
        //////    btnIntegrarBD.Enabled = true;
        //////    btnFTP.Enabled = true;
        //////    btnDepurarFTP.Enabled = true; 
        //////}


        //////private void btnAbrirFTP_Click(object sender, EventArgs e)
        //////{
        //////    if (bFTP_Configurado)
        //////    {
        //////        string sRuta = string.Format(@"{0}\{1}", sRuta_FTP, Transferencia.DirectorioFTP);
        //////        General.AbrirDirectorio(sRuta);  
        //////    }
        //////}
        #endregion Procesos FTP

        #region Directorios Integracion 
        private void btnMnRepositorio_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD);  
        }

        private void btnMnDescompresion_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaDescompresion);  
        }

        private void btnMnErrores_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Error);  
        }

        private void btnMnLog_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaLog);  
        }

        private void btnMnProcesso_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Proceso);  
        }

        private void btnMnIntegradas_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Terminada);  
        }
        #endregion Directorios Integracion

        private void button1_Click(object sender, EventArgs e)
        {
            CargarBasesDeDatos(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CargarBasesDeDatos(); 
        }

        private void tmRevisionFTP_Tick( object sender, EventArgs e )
        {
            if(!bProcesando)
            {

                tmRevisionFTP.Stop();
                tmRevisionFTP.Enabled = false;

                if (!bConsultaDeInformacion)
                {
                    //// Finalizar proceso 
                    if (bIntegracionCompleta)
                    {
                        General.msjUser("Migración terminada satisfactoriamente.");
                    }
                    else
                    {
                        General.msjUser("Ocurrió un error al migrar la información.");
                        ActualizarStatusBD("Ocurrió un error al migrar la información.");
                    }
                }
            }
        }


    }
}
