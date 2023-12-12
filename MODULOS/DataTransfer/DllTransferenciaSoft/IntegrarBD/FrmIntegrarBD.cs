using System;
using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales; 
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.SistemaOperativo;

using DllTransferenciaSoft.Zip;
using DllTransferenciaSoft.IntegrarInformacion; 

namespace DllTransferenciaSoft.IntegrarBD
{
    public partial class FrmIntegrarBD : FrmBaseExt 
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
        int iMesesInformacion_Migracion = 4; 

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
        bool bHabilitarLog = false;
        int iNumFiles = 0; 

        Thread thProceso;
        Thread thProceso_FTP;
        clsIniciarConSO SO;
        string sAppName = "DemonioDB";
        string sAppRuta = Application.ExecutablePath;

        string sBD_Activa = "";
        int iBD_Activa = 0;
        int iTabla_Activa = 0;
        int iMeses_Migracion = 0; 
        string sDB_EnProceso = ""; 
        FileInfo fileDB_Activo; 

        string sFormato = "#,###,##0.#0";
        string sFormato01 = "#,###,##0.000#";
        string sFormatoAux = "#,###,##0";

        int iTiempo = 0;
        TiempoIntegracion eTiempoIntegracion = TiempoIntegracion.Segundos;

        DataSet dtsFTP = new DataSet(); 
        DataSet dtsCentral = new DataSet();
        DataSet dtsRegional = new DataSet();
        bool bFTP_Central = false;
        bool bFTP_Regional = false;
        bool bFTP_Configurado = false;
        string sRuta_FTP = "";


        // bool bProcesoConfigurado = false;
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

        string sTablasMigrar = " Select NombreTabla, cast(0 as float) as Registros, cast(0 as float) as RegistrosMigrar, '' as Status " + 
            " From CFGC_EnvioDetalles (NoLock) " + 
            " Where Status = 'A' Order By IdOrden ";
        #endregion Declaracion de variables
        
        public FrmIntegrarBD()
        {
            InitializeComponent();          
            CheckForIllegalCrossThreadCalls = false;

            Fg.IniciaControles(); 

            SO = new clsIniciarConSO(sAppName, sAppRuta);

            cboTiposMigracion.SelectedIndex = 0;
            cboItemsFTP.SelectedIndex = 1;
            cboItemsFTP_Copiado.SelectedIndex = iIndex_0;
            cboMesesMigracion.SelectedIndex = 1; 

            datosCnn = new clsDatosConexion();
            datosCnn.Servidor = General.DatosConexion.Servidor;
            datosCnn.BaseDeDatos = General.DatosConexion.BaseDeDatos ;
            datosCnn.Usuario = General.DatosConexion.Usuario;
            datosCnn.Password = General.DatosConexion.Password;
            datosCnn.ConectionTimeOut = TiempoDeEspera.SinLimite; 

            cnn = new clsConexionSQL(datosCnn); 
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;

            datosTablas = new clsDatosConexion(); 
            datosTablas.Servidor = General.DatosConexion.Servidor;
            datosTablas.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            datosTablas.Usuario = General.DatosConexion.Usuario;
            datosTablas.Password = General.DatosConexion.Password;
            datosTablas.ConectionTimeOut = TiempoDeEspera.SinLimite; 


            leer = new clsLeer(ref cnn); 
            leerBDS = new clsLeer(ref cnn);
            leerTablasExcepcion = new clsLeer(ref cnn); 

            datosBorrar = new clsDatosConexion();
            datosBorrar.Servidor = General.DatosConexion.Servidor; 
            datosBorrar.BaseDeDatos = "master";
            datosBorrar.Usuario = General.DatosConexion.Usuario;
            datosBorrar.Password = General.DatosConexion.Password; 


            cnnBorrar = new clsConexionSQL(datosBorrar); 
            leerBorrar = new clsLeer(ref cnnBorrar); 
            
            lstBD = new clsListView(lstwBasesDeDatos); 
            lstTablas = new clsListView(lstwTablasMigrar); 

            lstBD.OrdenarColumnas = false; 
            lstTablas.OrdenarColumnas = false;

            AjustarTamaño_Pantalla();
            chkInicioSO.BackColor = General.BackColorBarraMenu; 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(General.DatosConexion, Transferencia.DatosApp, this.Name); 
        }

        #region Form 
        private void AjustarTamaño_Pantalla()
        {
            iAnchoListView = lstwBasesDeDatos.Width;
            iAnchoFrameInformacion = lstwBasesDeDatos.Width;

            iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);

            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;

            iAnchoListView_Ajuste = lstwBasesDeDatos.Width;
            iDiferenciaListView = iAnchoListView_Ajuste - iAnchoListView; 
            iAnchoFrameInformacion_Ajuste = lstwBasesDeDatos.Width;
            iDiferenciaFrameInformacion = iAnchoFrameInformacion_Ajuste - iAnchoFrameInformacion;          
        }

        private void AjustarColumnas()
        {
            int iAdicional = ((int)(iDiferenciaFrameInformacion *.85) / 2);
            int iAnchoBD = lstBD.AnchoColumna((int)ColsBD.BaseDeDatos) + iAdicional;
            int iAnchoStatus = lstBD.AnchoColumna((int)ColsBD.Status) + iAdicional;
            int iAnchoTabla = lstTablas.AnchoColumna((int)ColsTablas.Tabla) + (int)(iDiferenciaFrameInformacion * .90);


            if (!bColumnasAjustadas)
            {
                bColumnasAjustadas = true; 
                lstBD.AnchoColumna((int)ColsBD.BaseDeDatos, iAnchoBD); 
                lstBD.AnchoColumna((int)ColsBD.Status, iAnchoStatus); 
                lstTablas.AnchoColumna((int)ColsTablas.Tabla, iAnchoTabla);
            }

        }

        private void FrmIntegrarBD_Load(object sender, EventArgs e)
        {
            chkInicioSO.Checked = SO.Existe();

            btnFTP.Enabled = false;
            btnAbrirFTP.Enabled = false;
            btnDepurarFTP.Enabled = false;

            CargarConfiguracion();
            chkLog.Checked = true;
            cboTiposMigracion.SelectedIndex = 1;
            cboItemsFTP.SelectedIndex = 1;
            cboItemsFTP_Copiado.SelectedIndex = iIndex_0;
            CargarListaBDS();

            RevisarConfiguracion_FTP(); 
        }

        private void FrmIntegrarBD_FormClosing(object sender, FormClosingEventArgs e)
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

        private void chkInicioSO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInicioSO.Checked)
            {
                SO.Agregar();
            }
            else
            {
                SO.Remover();
            }
        }

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
                    TheadProceso();
                }
            }
        }
        #endregion Timer

        #region Funciones y Procedimientos Privados 
        private void CargarConfiguracion()
        {
            string sSql = string.Format(" Select * From {0} (NoLock) ", " CFG_ConfigurarIntegracionBDS ");

            tmIntegrarBD.Enabled = false;
            tmIntegrarBD.Stop();

            //// Resetear configuracion 
            iMesesInformacion_A_Migrar = 0; 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarConfiguracion()");
            }
            else
            {
                if (leer.Leer())
                {
                    // bProcesoConfigurado = true;  
                    iMesesInformacion_Migracion = leer.CampoInt("MesesInformacionMigracion");
                    iMesesInformacion_A_Migrar = leer.CampoInt("MesesInformacionMigrar");

                    sRutaBD = leer.Campo("RutaBDS_Integrar");
                    sRutaBD_Proceso = sRutaBD + @"\\Proceso\\";
                    sRutaBD_Error = sRutaBD + @"\\Errores\\";
                    sRutaBD_Error_Descompresion = sRutaBD + @"\\Errores_Descompresion\\";
                    sRutaDescompresion = sRutaBD + @"\\Descompresion\\"; 
                    sRutaBD_Terminada = leer.Campo("RutaBDS_Integradas");
                    sRutaLog = sRutaBD + @"\\Log\\"; 
                    ConfigurarCarpetasDeTrabajo(); 

                    eTiempoIntegracion = (TiempoIntegracion)leer.CampoInt("TipoTiempo");
                    iTiempo = leer.CampoInt("Tiempo");

                    switch (eTiempoIntegracion)
                    {
                        //////case TiempoIntegracion.Segundos:
                        //////    tmIntegrarInformacion.Interval = (iTiempo * 1000) + 1;
                        //////    break;

                        case TiempoIntegracion.Minutos:
                            tmIntegrarBD.Interval = (iTiempo * 60000) + 1;
                            break;

                        case TiempoIntegracion.Horas:
                            tmIntegrarBD.Interval = ((iTiempo * 60) * 60000) + 1;
                            break;
                    }
                    tmIntegrarBD.Enabled = true;
                    tmIntegrarBD.Start(); 
                }
            }

            CargarExcepciones();
            Configurar_MesesInformacionAMigrar(); 
        }

        private void Configurar_MesesInformacionAMigrar()
        {
            iMesesInformacion_A_Migrar = iMesesInformacion_A_Migrar == 0 ? iMesesInformacion_A_Migrar__Default : iMesesInformacion_A_Migrar;

            cboMesesMigracion.Items.Clear();
            for (int i = 1; i <= iMesesInformacion_A_Migrar; i++)
            {
                cboMesesMigracion.Items.Add(i.ToString()); 
            }

            cboMesesMigracion.SelectedIndex = 1;
            cboMesesMigracion.Text = iMesesInformacion_Migracion.ToString(); 
        }

        private void CargarExcepciones()
        {
            string sSql = " Select NombreTabla, cast(0 as float) as Registros, cast(0 as float) as RegistrosMigrar, '' as Status " +
                " From CFG_DetallesExcepcionBDS (NoLock) " +
                " Where Status = 'A' Order By IdOrden ";

            if (!leerTablasExcepcion.Exec(sSql))
            {
                Error.GrabarError(leerTablasExcepcion, "CargarExcepciones()"); 
            }
        }

        private void ConfigurarCarpetasDeTrabajo()
        {
            if (!Directory.Exists(sRutaBD))
                Directory.CreateDirectory(sRutaBD);

            if (!Directory.Exists(sRutaBD_Proceso))
                Directory.CreateDirectory(sRutaBD_Proceso);

            if (!Directory.Exists(sRutaBD_Error))
                Directory.CreateDirectory(sRutaBD_Error);

            if (!Directory.Exists(sRutaBD_Error_Descompresion))
                Directory.CreateDirectory(sRutaBD_Error_Descompresion); 

            if (!Directory.Exists(sRutaBD_Terminada))
                Directory.CreateDirectory(sRutaBD_Terminada);

            if (!Directory.Exists(sRutaDescompresion)) 
                Directory.CreateDirectory(sRutaDescompresion);

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
            DataTable dtDBS = PreparaListaBDS();
            // FileInfo f; 
            string sTitulo_BDS = "Listado de Bases de Datos"; 

            try
            {
                iNumFiles = 0; 
                CargarArchivos(ref dtDBS, "sii");
                CargarArchivos(ref dtDBS, "bak"); 
            }
            catch { }

            leerBDS.DataTableClase = dtDBS; 
            lstBD.LimpiarItems(); 
            lstBD.CargarDatos(leerBDS.DataSetClase);
            AjustarColumnas();

            FrameBasesDeDatos.Text = string.Format("{0} : {1}", sTitulo_BDS, lstBD.Registros); 
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
        private void btnLogIntegracion_Click(object sender, EventArgs e)
        {
            FrmLogIntegracion fLog = new FrmLogIntegracion();
            fLog.ShowDialog();
        }

        private void btnLogErrores_Click(object sender, EventArgs e)
        {
            FrmListadoDeErrores ex = new FrmListadoDeErrores();
            ex.ShowDialog();
        }

        private void btnIntegrarBD_Click(object sender, EventArgs e)
        {
            tmIntegrarBD_Tick(null, null); 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CargarListaBDS();

            iMeses_Migracion = 0; 
            chkLog.Checked = true;
            cboTiposMigracion.SelectedIndex = 1;
            cboItemsFTP.SelectedIndex = 1;
            cboItemsFTP_Copiado.SelectedIndex = iIndex_0; 
            lstTablas.LimpiarItems(); 
        } 

        private void TheadProceso()
        {
            CargarListaBDS();
            lstTablas.LimpiarItems(); 
            Thread.Sleep(50);

            thProceso = new Thread(this.IniciaProceso);
            thProceso.Name = "Integrando Bases De Datos";
            thProceso.Start();
        }

        #region Actualizar Datos Pantalla
        private void ActualizarStatusBD(string Mensaje)
        {
            lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Status) - 1].Text = Mensaje;
        }

        private void ActualizarNombreBD(string Nombre)
        {
            lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.BaseDeDatos) - 1].Text = Nombre;
        }

        private void ActualizarTamañoBD(double Porcentaje)
        {
            lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Tamaño) - 1].Text = Porcentaje.ToString(sFormato);
        }

        private void ActualizarPorcentajeBD(double Porcentaje)
        {
            lstwBasesDeDatos.Items[iBD_Activa - 1].SubItems[((int)ColsBD.Porcentaje) - 1].Text = Porcentaje.ToString(sFormato01);
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
                string sName = sRutaLog + @"\" + Fg.Mid(fileDB_Activo.Name, 1, (fileDB_Activo.Name.Length - fileDB_Activo.Extension.Length)) + ".txt";
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

            cnn.EsEquipoDeConfianza = true; 
            cnn.FormatoDeFecha = FormatoDeFecha.Ninguno; 
            leerLocal = new clsLeer(ref cnn); 

            string sSql = "";
            string sFile = DllTransferenciaSoft.Properties.Resources.spp_CFG_Script_Integrar_BaseDeDatos;
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

            sFile = DllTransferenciaSoft.Properties.Resources.spp_CrearStor_Ctl_Replicaciones;
            SQL_FILE = new clsScritpSQL(sFile, false);

            if (bRegresa)
            {
                foreach (string sFragmento in SQL_FILE.ListaScripts)
                {
                    sSql = "" + sFragmento;
                    if (!leerLocal.Exec(sSql))
                    {
                        bRegresa = false;
                        break;
                    }
                }
            }

            if (!bRegresa)
            {
                // General.msjError("Ocurrió un error al preparar la BD destino."); 
                Error.GrabarError(leerLocal, "PreperarScript()"); 
            }

            return bRegresa;
        }


        private void IniciaProceso()
        {
            sBD_Activa = ""; 
            iBD_Activa = 0;
            lstTablas.LimpiarItems();
            iMeses_Migracion = Convert.ToInt32(cboMesesMigracion.Text); 

            bHabilitarLog = chkLog.Checked; 
            while (leerBDS.Leer())
            {
                bIntegracionCompleta = false; 
                sBD_Activa = leerBDS.Campo("BaseDeDatos");
                iBD_Activa++;

                // Procesos 
                fileDB_Activo = new FileInfo(Path.Combine(sRutaBD, sBD_Activa));
                sBD_Activa = fileDB_Activo.Name.Replace(fileDB_Activo.Extension, "");

                GenerarLog();
                if (!RevisarBD_Integrada(sBD_Activa))
                {
                    RegistrarResultado_Integracion(2);
                    ActualizarStatusBD("Información ya integrada.");
                    ProcesarRespaldo(true);
                }
                else
                {
                    if (!Descomprimir())
                    {
                        RegistrarResultado_Integracion(3); 
                        ActualizarStatusBD("Error de descompresión.");
                        ProcesarRespaldo(false); 
                    }
                    else
                    {
                        lstTablas.LimpiarItems();
                        MontarRespaldo();
                        QuitarRespaldo();

                        ProcesarRespaldo(bIntegracionCompleta);

                        // Finalizar proceso 
                        if (bIntegracionCompleta)
                        {
                            RegistrarResultado_Integracion(4); 
                            ActualizarStatusBD("Procesada satisfactoriamente.");
                        }
                        else
                        {
                            ActualizarStatusBD("Integración no completada.");
                        }
                    }
                }

                CerrarLog(); 
                //General.msjUser(sBD_Activa); 
            }

            btnNuevo.Enabled = true;
            btnIntegrarBD.Enabled = true;

            //lstBD.LimpiarItems();
            lstTablas.LimpiarItems(); 

            CargarConfiguracion();
            //CargarListaBDS();
            //lstTablas.LimpiarItems(); 
        } 

        private bool Descomprimir() 
        {
            bool bRegresa = true; 
            bool bEsComprimido = false;

            bEsComprimido = fileDB_Activo.Extension.ToUpper().Contains("SII");
            ////if (!bEsComprimido)
            ////{
            ////    bEsComprimido = fileDB_Activo.Extension.ToUpper().Contains("RAR");
            ////}

            ActualizarStatusBD("Descomprimiendo"); 
            if (bEsComprimido)
            {
                Exception exError = new Exception(); 
                ZipUtil zip = new ZipUtil();
                zip.Type = Transferencia.Modulo + "BackUp";
                string sFile_SII = fileDB_Activo.FullName;

                bRegresa = false;

                try
                {
                    // Borrar cualquier respaldo en el TMP 
                    foreach (string sFileNew in Directory.GetFiles(sRutaDescompresion, "*.bak"))
                    {
                        File.Delete(sFileNew); 
                    }

                    if (!zip.Descomprimir(sRutaDescompresion, fileDB_Activo.FullName, ref exError))
                    {
                        Log(exError.Message); 
                        Error.GrabarError(exError.Message, "Descomprimir()"); 
                        File.Move(fileDB_Activo.FullName, Path.Combine(sRutaBD_Error_Descompresion, fileDB_Activo.Name));
                        File.Delete(sFile_SII);
                    }
                    else 
                    {

                        // Carga el respaldo descomprimido en el TMP 
                        foreach (string sFileNew in Directory.GetFiles(sRutaDescompresion, "*.bak"))
                        {
                            fileDB_Activo = new FileInfo(Path.Combine(sRutaDescompresion, sFileNew));
                            break;
                        }

                        File.Move(fileDB_Activo.FullName, Path.Combine(sRutaBD, fileDB_Activo.Name));
                        fileDB_Activo = new FileInfo(Path.Combine(sRutaBD, fileDB_Activo.Name));
                        sBD_Activa = fileDB_Activo.Name;

                        ActualizarNombreBD(sBD_Activa);
                        ActualizarTamañoBD((double)((fileDB_Activo.Length / 1024.0000) / 1024.0000));
                        File.Delete(sFile_SII);
                        bRegresa = true; 
                    }
                }
                catch (Exception ex)
                {
                    sFile_SII = ex.Message;
                    Log(sFile_SII); 
                } 
            }

            return bRegresa; 
        } 

        private bool MontarRespaldo() 
        {
            bool bRegresa = false; 

            ActualizarStatusBD("Montando respaldo");
            sDB_EnProceso = fileDB_Activo.Name;
            sDB_EnProceso = "INT_" + Fg.Mid(sDB_EnProceso, 1, sDB_EnProceso.Length - (fileDB_Activo.Extension.Length)).Replace("-", "_");

            sBD_Activa = sBD_Activa.Replace("-", "_"); 
            ActualizarNombreBD(sDB_EnProceso); 

            lstTablas.LimpiarItems();
            Restore = new clsRestore(General.DatosConexion);

            if (Restore.ObtenerDatosDeRegistro(fileDB_Activo.FullName))
            {
                Restore.ConfirmarRestore = false;
                if (Restore.Restaurar(sDB_EnProceso, sRutaBD_Proceso))
                {
                    sDB_EnProceso = Restore.BaseDeDatos;
                    ActualizarNombreBD(sDB_EnProceso);

                    datosTablas.BaseDeDatos = sDB_EnProceso;
                    cnnTablas = new clsConexionSQL(datosTablas);
                    cnnTablas.EsEquipoDeConfianza = true; 

                    leerListaTablas = new clsLeer(ref cnnTablas);
                    leerListaTablas_Base = new clsLeer(ref cnn); 

                    cnnTablas.Abrir(); 
                    Thread.Sleep(2000);

                    ActualizarStatusBD("Migrando información");
                    if (leerListaTablas_Base.Exec(sTablasMigrar))
                    {
                        lstTablas.CargarDatos(leerListaTablas_Base.DataSetClase);
                        if (ObtenerRegistrosA_Procesar())
                        {
                            if (MigrarInformacion())
                            {
                                bIntegracionCompleta = RegistrarIntegracion(); 
                            }
                        }
                    }
                    cnn.Cerrar(); 
                } 
            }

            // Eliminar todos los objetos 
            Restore = null; 
            cnnTablas = null;
            leerListaTablas = null;
            leerListaTablas_Base = null; 

            bRegresa = bIntegracionCompleta; 
            return bRegresa; 
        }

        private bool verificaExistaTabla(string Tabla)
        {
            bool bRegresa = false;
            string sSql = string.Format("If Exists ( Select Name From {0}.dbo.Sysobjects (NoLock) Where Name = '{1}' and xType = 'U' ) Select 1 as Existe ", sDB_EnProceso, Tabla);

            if ( leerListaTablas.Exec(sSql) )
            {
                bRegresa = leerListaTablas.Leer(); 
            }

            return bRegresa;
        }

        private string verificaExistaTabla_FechaControl(string Tabla)
        {
            string sRegresa = "";
            string sSql = string.Format("If Exists ( Select So.Name From {0}.dbo.Sysobjects So (NoLock) " + 
                " Inner Join Syscolumns sc On ( So.Id = SC.Id ) " +  
                " Where So.Name = '{1}' and Sc.Name = 'FechaControl' and So.xType = 'U' ) Select 1 as Existe ", sDB_EnProceso, Tabla);

            if (leerListaTablas.Exec(sSql))
            {
                if (leerListaTablas.Leer())
                {
                    try
                    {
                        sRegresa = string.Format("Where convert(varchar(10), FechaControl, 120) >= convert(varchar(10), dateadd(mm, -1 * {0}, getdate()), 120) ",
                            iMeses_Migracion);
                    }catch 
                    {
                    }
                } 
            }

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
            leerListaTablas = new clsLeer(ref cnnTablas);
            string sSQL = "";
            string sTabla = ""; 
            int iRegistros = 0;
            int iRegistrosMigrar = 0;
            string sFiltro_FechaControl = "";

            iTabla_Activa = 0; 
            for (int i = 0; i <= lstwTablasMigrar.Items.Count - 1; i++)
            {
                iRegistros = 0;
                iRegistrosMigrar = 0;
                sTabla = lstwTablasMigrar.Items[i].Text;
                iTabla_Activa = i + 1;

                if (verificaExistaTabla(sTabla))
                {
                    sFiltro_FechaControl = verificaExistaTabla_FechaControl(sTabla); 
                    sSQL = string.Format("Select count(*) From {0} (NoLock) {1} ", sTabla, sFiltro_FechaControl);
                    if (!leerListaTablas.Exec(sSQL))
                    {
                        bRegresa = false;
                        Error.GrabarError(datosTablas.BaseDeDatos + " : " + leerListaTablas.Error.Message + "   " + sSQL, "ObtenerRegistrosA_Procesar()"); 
                        break;
                    }
                    else
                    {
                        leerListaTablas.Leer();
                        iRegistros = leerListaTablas.CampoInt(1);

                        // sSQL = string.Format("Select count(*) From {0} (NoLock) Where Actualizado in ( 0, 2) ", lstwTablasMigrar.Items[i].Text);
                        sSQL = PreparaTablaMigrar(sTabla);
                        if (!leerListaTablas.Exec(sSQL))
                        {
                            bRegresa = false;
                            Error.GrabarError(datosTablas.BaseDeDatos + " : " + leerListaTablas.Error.Message + "   " + sSQL, "ObtenerRegistrosA_Procesar()"); 
                            break;
                        }
                        else
                        {
                            leerListaTablas.Leer();
                            iRegistrosMigrar = leerListaTablas.CampoInt(1);
                        }
                    }
                }

                ActualizarRegistros(iRegistrosMigrar.ToString(sFormatoAux)); 
                ActualizarRegistros_A_Migrar(iRegistros.ToString(sFormatoAux));
                ////lstwTablasMigrar.Items[i].SubItems[(int)ColsTablas.Registros].Text = iRegistros.ToString(sFormatoAux);
                ////lstwTablasMigrar.Items[i].SubItems[(int)ColsTablas.RegistrosMigrar].Text = iRegistrosMigrar.ToString(sFormatoAux);
            }

            return bRegresa; 
        }

        private string PreparaTablaMigrar(string Tabla)
        {
            string sRegresa = "";
            clsLeer ex = new clsLeer(); 

            sRegresa = cboTiposMigracion.SelectedIndex == 0 ? " Where Actualizado in ( 0, 2) " : "";

            ex.DataRowsClase = leerTablasExcepcion.DataTableClase.Select( string.Format(" NombreTabla = '{0}' ", Tabla ) );
            if (ex.Leer())
            {
                sRegresa = " Where Actualizado in ( 0, 2) "; 
            }

            sRegresa = string.Format("Select count(*) From {0} (NoLock) {1} ", Tabla, sRegresa);  
            return sRegresa; 
        }

        private int PreparaTablaMigrarTipo(string Tabla)
        {
            int iRegresa = cboTiposMigracion.SelectedIndex;
            clsLeer ex = new clsLeer();

            ex.DataRowsClase = leerTablasExcepcion.DataTableClase.Select(string.Format(" NombreTabla = '{0}' ", Tabla));
            if (ex.Leer())
            {
                iRegresa = 0; 
            }

            return iRegresa;
        }

        private bool MigrarInformacion() 
        {
            bool bRegresa = true; 
            cnnTablas = new clsConexionSQL(datosCnn);
            leerListaTablas = new clsLeer(ref cnnTablas);
            string sSQL = ""; 
            double iRegistros = (double)lstwTablasMigrar.Items.Count;
            double dPorcentaje = 0;
            bool bSoloDiferencias = cboTiposMigracion.SelectedIndex == 0;
            int iSoloDiferencias = cboTiposMigracion.SelectedIndex == 0 ? 1 : 0;
            string sTabla = ""; 

            iTabla_Activa = 0; 
            for (int i = 0; i <= lstwTablasMigrar.Items.Count - 1; i++)
            {
                sTabla = lstwTablasMigrar.Items[i].Text;


                if (verificaExistaTabla(sTabla))
                {
                    iTabla_Activa = i + 1;
                    if (!validarProcesarTabla(sTabla))
                    {
                        ActualizarStatusTabla("Procesada");
                    }
                    else
                    {
                        sSQL = string.Format("Declare @sSQL varchar(8000) \n");
                        sSQL += string.Format("Exec spp_CFG_Script_Integrar_BaseDeDatos '{0}', '{1}', '{1}', '{2}', @sSQL output, '{3}', '{4}', '{5}' \n",
                            datosTablas.BaseDeDatos, datosCnn.BaseDeDatos, sTabla, 1, PreparaTablaMigrarTipo(sTabla), iMeses_Migracion);
                        sSQL += string.Format("Exec(@sSQL) \n");
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
                            break;
                        }
                        else
                        {
                            leerListaTablas.Leer();
                            Log(leerListaTablas.Campo("SQL"));
                            Log("Registros afetados : " + leerListaTablas.Campo("Registros"));
                            Log("");
                            Log("");


                            ActualizarStatusTabla("Procesada");
                        }
                    }
                }

                dPorcentaje = (iTabla_Activa / iRegistros) * 100.00; 
                ActualizarPorcentajeBD(dPorcentaje); 
            }

            cnnTablas = null;
            leerListaTablas = null;

            return bRegresa; 
        }

        private void KillProcesos()
        {
            cnnBorrar = new clsConexionSQL(datosBorrar);
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

        private void QuitarRespaldo() 
        {
            ActualizarStatusBD("Removiendo respaldo");
            KillProcesos();
            KillProcesos();

            // bool bRegresa = false;
            cnnBorrar = new clsConexionSQL(datosBorrar); 
            leerBorrar = new clsLeer(ref cnnBorrar);

            string sSql = "USE [master] \n";
            sSql += string.Format("Exec msdb.dbo.sp_delete_database_backuphistory @database_name = '{0}' \n", sDB_EnProceso);
            sSql += "USE [master] \n";
            sSql += string.Format("Alter Database [{0}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE \n", sDB_EnProceso); 
            sSql += string.Format("Drop Database [{0}] ", sDB_EnProceso); 

            //sSql = string.Format("Exec sp_dbremove [{0}]", sDB_EnProceso);  
            if (!leerBorrar.Exec(sSql))
            {
                ActualizarStatusBD("Error al remover la BD"); 
            }
            else 
            { 
                ////sSql = string.Format("Drop Database [{0}] ", sDB_EnProceso);
                ////bRegresa = leerBorrar.Exec(sSql);
                ActualizarStatusBD("Removiendo respaldo terminado"); 
            }
        }

        private bool ProcesarRespaldo( bool IntegracionCompleta )
        {
            bool bRegresa = false;
            string sRuta = sRutaBD_Terminada;
            string sOrigen = fileDB_Activo.FullName;
            string sDestino = Path.Combine(sRutaBD_Terminada, fileDB_Activo.Name);

            if (!IntegracionCompleta)
            {
                sDestino = Path.Combine(sRutaBD_Error, fileDB_Activo.Name); 
            }

            try
            {
                fileDB_Activo = new FileInfo(Application.ExecutablePath); 
                if (File.Exists(sDestino))
                {
                    File.Delete(sDestino);
                }

                File.Move(sOrigen, sDestino);
            }
            catch (Exception ex)
            {
                sRuta = ex.Message; 
            }

            return bRegresa; 
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

        private bool RevisarBD_Integrada(string BaseDeDatos)
        {
            bool bRegresa = false;
            string sBD = FormatearNombreBD(BaseDeDatos);

            string sSql = string.Format("Select * From CFG_RegistroIntegracionBD (NoLock) Where NombreBD like '%{0}%' and TipoResultado = 1 ", sBD);

            if (leer.Exec(sSql))
            {
                bRegresa = !leer.Leer(); 
            }

            return bRegresa; 
        }

        private bool RegistrarIntegracion()
        {
            bool bRegresa = false; 
            ////string sSql = string.Format("Insert Into CFG_RegistroIntegracionBD ( NombreBD ) Select '{0}' ", sBD_Activa); 
            ////bRegresa = leer.Exec(sSql); 

            bRegresa = RegistrarResultado_Integracion(1); 

            return bRegresa;
        }

        private bool RegistrarResultado_Integracion(int Resultado)
        {
            bool bRegresa = false;
            string sSql = string.Format("Insert Into CFG_RegistroIntegracionBD ( NombreBD, TipoResultado ) Select '{0}', '{1}' ", sBD_Activa, Resultado);
            bRegresa = leer.Exec(sSql);
            return bRegresa;
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
            int iIntervalo = 0; 
            clsLeer leerFTP = new clsLeer();
            leerFTP.DataSetClase = Datos_FTP;
            leerFTP.Leer(); 

            dtsFTP = new DataSet(); 
            dtsFTP.Tables.Add(leerFTP.DataTableClase.Copy());

            bFTP_Configurado = true;
            btnFTP.Enabled = true;
            btnAbrirFTP.Enabled = true;
            btnDepurarFTP.Enabled = true; 

            eTiempoIntegracion = (TiempoIntegracion)leerFTP.CampoInt("TipoTiempo");
            iTiempo = leerFTP.CampoInt("Tiempo");
            sRuta_FTP = leerFTP.Campo("RutaFTP_BDS_Integrar"); 

            switch (eTiempoIntegracion)
            {
                case TiempoIntegracion.Minutos:
                    iIntervalo = (iTiempo * 60000) + 1;
                    // tmRevisionFTP.Interval = (iTiempo * 60000) + 1;
                    break;

                case TiempoIntegracion.Horas:
                    iIntervalo = ((iTiempo * 60) * 60000) + 1;
                    //tmRevisionFTP.Interval = ((iTiempo * 60) * 60000) + 1;
                    break;
            }

            tmRevisionFTP.Interval = iIntervalo;

            tmRevisionFTP.Enabled = true;
            tmRevisionFTP.Start();  
        }

        private bool GetConfiguracionFTP(int Tipo)
        {
            bool bRegresa = false;
            string sTabla = Tipo == 1 ? " CFGS_ConfigurarFTP_BDS " : " CFGR_ConfigurarFTP_BDS ";
            string sSql = string.Format(" Select * From {0} ", sTabla);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "GetConfiguracionFTP()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    if (Tipo == 1)
                    {
                        bFTP_Central = true;
                        dtsCentral = leer.DataSetClase; 
                    }

                    if (Tipo == 2)
                    {
                        bFTP_Regional = true;
                        dtsRegional = leer.DataSetClase; 
                    }
                }
            }

            return bRegresa; 
        } 
        #endregion Configuracion FTP

        #region Procesos FTP 
        private void btnFTP_Click(object sender, EventArgs e)
        {
            tmRevisionFTP_Tick(null, null); 
        }

        private void tmRevisionFTP_Tick(object sender, EventArgs e)
        {
            if (btnFTP.Enabled)
            {
                btnNuevo.Enabled = false;
                btnIntegrarBD.Enabled = false;
                btnFTP.Enabled = false;
                btnDepurarFTP.Enabled = false; 
                TheadProceso_FTP(); 
            } 
        }

        private void TheadProceso_FTP()
        {
            thProceso_FTP = new Thread(this.ProcesarFTP);
            thProceso_FTP.Name = "Revisar FTP [Bases De Datos]";
            thProceso_FTP.Start();
        }

        private void ProcesarFTP()
        { 
            // sRutaBD 
            clsFTP_BaseDeDatos f = new clsFTP_BaseDeDatos(sRuta_FTP, sRutaBD);
            f.Dias_Integracion = Convert.ToInt32(cboItemsFTP_Copiado.Text); 
            f.Procesar();

            btnNuevo.Enabled = true;
            btnIntegrarBD.Enabled = true;
            btnFTP.Enabled = true;
            btnDepurarFTP.Enabled = true; 

            tmIntegrarBD_Tick(null, null); 
        }

        private void btnDepurarFTP_Click(object sender, EventArgs e)
        {
            //tmDepurarFTP_Tick(null, null);
            
            btnNuevo.Enabled = false;
            btnIntegrarBD.Enabled = false;
            btnFTP.Enabled = false;
            btnDepurarFTP.Enabled = false;
            TheadProceso_FTP_Depurar();
        }

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

        private void TheadProceso_FTP_Depurar()
        {
            thProceso_FTP = new Thread(this.ProcesarFTP_Depurar);
            thProceso_FTP.Name = "Depurar Directorio FTP [Bases De Datos]";
            thProceso_FTP.Start();
        }

        private void ProcesarFTP_Depurar()
        {
            // sRutaBD 
            clsFTP_BaseDeDatos f = new clsFTP_BaseDeDatos(sRuta_FTP);
            f.Depurar_NumArchivos = Convert.ToInt32(cboItemsFTP.Text); 
            f.Depurar();

            btnNuevo.Enabled = true;
            btnIntegrarBD.Enabled = true;
            btnFTP.Enabled = true;
            btnDepurarFTP.Enabled = true; 
        }


        private void btnAbrirFTP_Click(object sender, EventArgs e)
        {
            if (bFTP_Configurado)
            {
                string sRuta = string.Format(@"{0}\{1}", sRuta_FTP, Transferencia.DirectorioFTP);
                if (!Directory.Exists(sRuta))
                {
                    sRuta = string.Format(@"{0}", sRuta_FTP);
                    if (Directory.Exists(sRuta))
                    {
                        General.AbrirDirectorio(sRuta);
                    }
                }

                General.AbrirDirectorio(sRuta);
            }
        }
        #endregion Procesos FTP

        #region Directorios Integracion 
        private void btnDir_BD__Repositorio_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD);  
        }

        private void btnDir_BD__Descompresion_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaDescompresion);  
        }

        private void btnDir_BD__Errores_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Error);  
        }

        private void btnDir_BD__Log_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaLog);  
        }

        private void btnDir_BD__Proceso_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Proceso);  
        }

        private void btnDir_BD__Integradas_Click(object sender, EventArgs e)
        {
            General.AbrirDirectorio(sRutaBD_Terminada);
        }

        #endregion Directorios Integracion

        private void btnIntegraciónDePaquetes_Click(object sender, EventArgs e)
        {
            FrmIntegrarPaquetesDeDatos f = new FrmIntegrarPaquetesDeDatos();
            f.ShowDialog(this); 
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            FrmConfigIntegrarDB f = new FrmConfigIntegrarDB();
            f.ShowDialog(this); 
        }
    }
}
