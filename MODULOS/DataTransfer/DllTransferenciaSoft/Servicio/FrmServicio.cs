using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics; 

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Criptografia;
using SC_SolutionsSystem.FTP;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;

using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;
using DllTransferenciaSoft.ObtenerInformacion;
using DllTransferenciaSoft.Zip;

namespace DllTransferenciaSoft.Servicio
{
    public partial class FrmServicio : FrmBaseExt
    {
        #region Declaracion de variables 
        #region Control de Procesos 
        bool Semaforo = false;
        const bool Verde = true;
        const bool Rojo = false;
        #endregion Control de Procesos

        clsDrives drives = new clsDrives();  
        clsIniciarConSO SO;
        // Form frmPadre;
        string sAppName = "DemonioSOC";
        string sAppRuta = Application.ExecutablePath;
        string sServicioParalelo = ""; 

        bool bEjecutarObtencion = false;
        bool bEjecutarIntegracion = false;
        //bool bEstoyEnSystray = false;

        bool bObtencionConfigurada = false;
        bool bIntegracionConfigurada = false;
        bool bRespaldoConfigurado = false;
        bool bEnviandoRespaldo = false;
        int iStatusEnvioFTP = 0; 
        bool bEsInicioDeProceso = true; 


        bool bServicio_Habilitado_Obtencion = false;
        bool bServicio_Habilitado_Integracion = false;
        bool bServicio_Habilitado_Respaldos = false; 


        // bool bEjecutarProceso = false;
        bool bRepetirProceso = false;
        //bool Inicio = true;
        // int iPeridiocidad = 0;
        int iMinutosObtencion = 0;
        // int iMinutosIntegracion = 0;
        //int iTiempo = 0;
        // int iTiempoTiempo = 0;
        DateTime dHoraEjecucion;

        int iDiasRespaldos = 0; 

        Peridiocidad ePeriodicidad = Peridiocidad.Diariamente;
        TipoTiempo eTipoTiempo = TipoTiempo.Minutos;
        TiempoIntegracion eTiempoIntegracion = TiempoIntegracion.Segundos;
        TipoTiempo eTiempoRespaldos = TipoTiempo.Horas;
        string sRutaRespaldos = "";
        string sPrefijoRespaldo = ""; 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsSQL svrSql; 

        TipoServicio tpServicio = TipoServicio.Ninguno;
        string sTitulo = "", sSysTray = "";
        string sObtencion = "", sIntegracion = "", sRespaldoBD = "";
        string sFTP_Conexion = ""; 

        Size sizeFrameManual;
        Point pointFrameManual;
        Point pointFrameEnEjecucion;

        // Manejo de Hilos 
        Thread threadObtencion;
        Thread threadIntegracion;
        Thread threadRespaldos;
        Thread threadRespaldosFTP;

        string sOrigen = ""; 
        string sDestino = "";
        string sFileDestino = "";

        string sFTP_Servidor = ""; 
        string sFTP_Usuario = ""; 
        string sFTP_Password = "";
        bool bFTP_ModoActivoDeTransferencia = false; 

        FrmLogIntegracion fLog;  

        //public FrmServicio()
        //{
        //    InitializeComponent();
        //    InicializaForm();
        //}
        #endregion Declaracion de variables 

        public FrmServicio(TipoServicio Servicio)
        {
            InitializeComponent();
            tpServicio = Servicio;
            InicializaForm();

            //frmPadre = this.MdiParent;
        }

        public FrmServicio(ref Form Padre, TipoServicio Servicio)
        {
            InitializeComponent();
            tpServicio = Servicio;

            this.MdiParent = Padre;
            InicializaForm();
        }

        void InicializaForm()
        {
            this.Size = new Size(245, 436); // 400
            FramePrincipal.Size = new Size(218, 385);  // 362

            FrameProcesosManual.Size = new Size(196, 109);
            sizeFrameManual = new Size(183, 109);
            pointFrameManual = FrameProcesosManual.Location;
            pointFrameEnEjecucion = FrameEnEjecucion.Location;

            ////
            leer = new clsLeer(ref cnn); 

            //FrameEnEjecucion.Location = pointFrameManual; //  FrameProcesosManual.Location; 
            //FrameProcesosManual.BringToFront(); 

            //FrameEnEjecucion.Left = 100; 

            if (tpServicio == TipoServicio.Cliente)
            {
                sTitulo = "Cliente";
                sSysTray = "Servicio Cliente";
                sAppName = "DemonioSC";
                sObtencion = " CFGC_ConfigurarObtencion "; 
                sIntegracion = " CFGC_ConfigurarIntegracion "; 
                sRespaldoBD = " Net_CFGC_Respaldos ";
                sPrefijoRespaldo = "PtoVta";
                sFTP_Conexion = " CFGC_ConfigurarConexion "; 
                iDiasRespaldos = 60; 
            }

            if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            {
                sTitulo = "Cliente Oficina Central Regional";
                sSysTray = "Servicio Cliente Oficina Central Regional";
                sAppName = "DemonioSCR";
                sObtencion = " CFGCR_ConfigurarObtencion ";
                sIntegracion = " CFGCR_ConfigurarIntegracion ";
                sRespaldoBD = " Net_CFGCR_Respaldos "; 
                sPrefijoRespaldo = "PtoCenReg";

                sServicioParalelo = "Servicio Oficina Central Regional"; 

            }

            if (tpServicio == TipoServicio.OficinaCentralRegional)
            {
                sTitulo = "Oficina Central Regional";
                sSysTray = "Servicio Oficina Central Regional";
                sAppName = "DemonioSOCR";
                sObtencion = " CFGS_ConfigurarObtencion ";
                sIntegracion = " CFGS_ConfigurarIntegracion ";
                sRespaldoBD = " Net_CFGS_Respaldos ";
                sPrefijoRespaldo = "PtoCenReg";

                sServicioParalelo = "Servicio Cliente Regional";
                iDiasRespaldos = 180; 
                // ObtenerDatosOrigen(); 
            }

            if (tpServicio == TipoServicio.OficinaCentral)
            {
                sTitulo = "Oficina Central";
                sSysTray = "Servicio Oficina Central";
                sAppName = "DemonioSOC";
                sObtencion = " CFGSC_ConfigurarObtencion "; 
                sIntegracion = " CFGSC_ConfigurarIntegracion "; 
                sRespaldoBD = " Net_CFGSC_Respaldos ";
                sPrefijoRespaldo = "PtoCen";
            }

            SO = new clsIniciarConSO(sAppName, sAppRuta);
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo + ".Servicio", Transferencia.Version, this.Name + "_" + sAppName);

            AsociarMenus(); 
        }

        private void AsociarMenus()
        {
            btnObtencion__Habilitar.Tag = btnObtencion.Name;
            btnObtencion__Deshabilitar.Tag = btnObtencion.Name;

            btnIntegracion__Habilitar.Tag = btnIntegracion.Name;
            btnIntegracion__Deshabilitar.Tag = btnIntegracion.Name;

            btnRespaldos__Habilitar.Tag = btnRespaldarBD.Name;
            btnRespaldos__Deshabilitar.Tag = btnRespaldarBD.Name; 


            btnObtencion.ContextMenuStrip = menuObtencion;
            btnIntegracion.ContextMenuStrip = menuIntegracion;
            btnRespaldarBD.ContextMenuStrip = menuRespaldos; 
        }

        #region Propiedades 

        #endregion Propiedades

        private void FrmServicio_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    LogIntegracion(); 
                    break;
                default:
                    break; 
            }
        }

        public void CerrrLogs()
        {
            if (clsDatosIntegracion.LogAbierto)
            {
                try
                {
                    clsDatosIntegracion.LogAbierto = true;  // Evitar se abra de nuevo el modulo
                    fLog.Close();
                    fLog = null;
                }
                catch { }
            }
        }

        private void FrmServicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrrLogs();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; 
            }
            else
            {
                try
                {
                    Application.Exit();
                }
                catch { } 
            }

        }

        private void FrmServicio_Load(object sender, EventArgs e)
        {
            // Forma "Sucia" de evitar errores de Acceso entre procesos 
            CheckForIllegalCrossThreadCalls = false;

            this.Text = sSysTray; 
            this.Text = sTitulo;

            //icoSystray.Text = sSysTray;
            //icoSystray.Icon = General.IconoSistema;
            //icoSystray.ContextMenu = mnOpciones;

            cboProcesos.Clear(); 
            cboProcesos.Add("0", "<< Seleccione >>"); 
            cboProcesos.Add("1", "Obtención de información"); 
            cboProcesos.Add("2", "Integración de informacion"); 
            cboProcesos.SelectedIndex = 0; 

            btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
            btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
            pcServer.Image = pcServerInactivo.Image;

            chkInicioSO.Checked = SO.Existe();

            InicarServicios();

            // Control Proceso 
            Semaforo = Verde; 
            
            //Iniciar la Revision de servicios 
            //if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            if (tpServicio == TipoServicio.OficinaCentralRegional || tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            {
                PrepararIniciosAutomaticos(true); 
            }

            //// Control de la carga inicial de la configuración 
            bEsInicioDeProceso = false;
        }

        private void InicarServicios()
        {
            cboProcesos.Data = "1";
            btnIniciar_Click(null, null);

            cboProcesos.Data = "2";
            btnIniciar_Click(null, null);

            //if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            //{
            //    cboProcesos.Data = "1";
            //    cboProcesos.Enabled = false; 
            //}
            //else
            {
                cboProcesos.Data = "0";
            }

            ConfigRespaldosBD();

            //tmRespaldosBD.Interval = 10000;
            tmRespaldosBD.Enabled = bRespaldoConfigurado;
            if (bRespaldoConfigurado)
            {
                tmRespaldosBD.Start();
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

        #region Activar-Desactivar Procesos 
        private void ObtencionForzar()
        { 
        }

        private void Obtencion(bool Activar)
        {
            if (Activar)
            {
                if (!bEjecutarObtencion)
                {
                    tmObtenerInformacion.Enabled = true;
                    ConfigObtencion();
                    btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
                    btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopActivo];
                    pcServer.Image = pcServerStar.Image;
                    bEjecutarObtencion = true;
                }
            }
            else
            {
                if (bEjecutarObtencion)
                {
                    lblObtencion.Text = "Obtención : Detenida ";
                    tmObtenerInformacion.Enabled = false;
                    btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayActivo];
                    btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
                    pcServer.Image = pcServerStop.Image;
                    bEjecutarObtencion = false;
                }
            }
        }

        private void Integracion(bool Activar)
        {
            if (Activar)
            {
                if (!bEjecutarIntegracion)
                {
                    tmIntegrarInformacion.Enabled = true;
                    ConfigIntegracion();
                    btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
                    btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopActivo];
                    pcServer.Image = pcServerStar.Image;
                    bEjecutarIntegracion = true;
                }
            }
            else
            {
                if (bEjecutarIntegracion)
                {
                    lblIntegracion.Text = "Integración : Detenida ";
                    tmIntegrarInformacion.Enabled = false;
                    btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayActivo];
                    btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
                    pcServer.Image = pcServerStop.Image;
                    bEjecutarIntegracion = false;
                }
            }
        }
        #endregion Activar-Desactivar Procesos

        #region Obtencion de configuraciones 
        private bool CrearDirectorios(string RutaDirectorio)
        {
            bool bRegresa = false;

            try
            {
                if (drives.ExisteUnidad(RutaDirectorio))
                {
                    bRegresa = true; 
                    if (!Directory.Exists(RutaDirectorio))
                    {
                        Directory.CreateDirectory(RutaDirectorio);
                    }
                } 
            }
            catch 
            {
                bRegresa = false; 
            } 


            return bRegresa; 
        }

        private bool ConfigObtencion_FTP()
        {
            bool bRegresa = true;

            if (tpServicio == TipoServicio.Cliente)
            {
                bRegresa = ConfigObtencion_FTP_Interno(); 
            }

            return bRegresa; 
        }

        /// <summary>
        /// Obtiene la configuración del servidor FTP para respaldos 
        /// </summary>
        /// <returns></returns> 
        private bool ConfigObtencion_FTP_Interno() 
        { 
            bool bRegresa = false;
            int iLargo = 0;
            clsCriptografo crypto = new clsCriptografo(); 

            string sSql = string.Format("Select * From {0} (NoLock) ", sFTP_Conexion);
            
            if (!leer.Exec(sSql)) 
            {
                Error.GrabarError(leer, "ConfigObtencion_FTP()");
            }
            else
            {
                bRegresa = leer.Leer(); 
                {
                    // Informacion de FTP 
                    sFTP_Servidor = leer.Campo("ServidorFTP"); 
                    sFTP_Usuario = leer.Campo("UserFTP"); 
                    sFTP_Password = leer.Campo("PasswordFTP");
                    bFTP_ModoActivoDeTransferencia = leer.CampoBool("ModoActivoDeTransferencia"); 

                    try
                    {
                        sFTP_Password = crypto.PasswordDesencriptar(leer.Campo("PasswordFTP")).Substring(iLargo);
                    }
                    catch
                    {
                        sFTP_Password = "";
                    }
                }
            }
            return bRegresa; 
        }

        private bool ConfigObtencion()
        {
            bool bRegresa = true; 
            bool bRutasConfiguradas = false;
            int iTemporizador = 0; 

            DateTime dFechaExec = General.FechaSistema; 
            bObtencionConfigurada = false;
            string sSql = string.Format("Select * From {0} (NoLock) ", sObtencion);

            if (!leer.Exec(sSql))
            {
                bRegresa = true;
                Error.GrabarError(leer, "ConfigObtencion()");
            }
            else
            {
                General.FechaSistemaObtener();
                lblObtencion.Text = "Obtención : ";
                if (!leer.Leer())
                {
                    lblObtencion.Text = "Obtención : Sin configurar";
                }
                else 
                {
                    if (CrearDirectorios(leer.Campo("RutaUbicacionArchivos")))
                    {
                        bRutasConfiguradas = CrearDirectorios(leer.Campo("RutaUbicacionArchivosEnviados")); 
                    }

                    if (!bRutasConfiguradas)
                    {
                        lblObtencion.Text = "Obtención : Error de rutas";
                    }
                    else
                    {
                        bObtencionConfigurada = true;
                        bServicio_Habilitado_Obtencion = leer.CampoBool("ServicioHabilitado"); 
                        ePeriodicidad = (Peridiocidad)leer.CampoInt("Periodicidad");
                        eTipoTiempo = (TipoTiempo)leer.CampoInt("TipoTiempo");

                        iMinutosObtencion = leer.CampoInt("Tiempo");
                        dHoraEjecucion = leer.CampoFecha("HoraEjecucion");
                        bRepetirProceso = leer.CampoBool("bRepetirProceso");

                        ////// Informacion de FTP 
                        ////sFTP_Servidor = leer.Campo("ServidorFTP");
                        ////sFTP_Usuario = leer.Campo("UserFTP");
                        ////sFTP_Password = leer.Campo("PasswordFTP"); 

                        tmObtenerInformacion.Stop();
                        if (!bServicio_Habilitado_Obtencion)
                        {
                            bObtencionConfigurada = false;
                            lblObtencion.Text = "Obtención : Deshabilitada";
                        }
                        else 
                        {
                            if (eTipoTiempo == TipoTiempo.Minutos && bRepetirProceso)
                            {
                                //tmObtenerInformacion.Stop();
                                //tmObtenerInformacion.Interval = (iMinutosObtencion * 60000) + 1;
                                iTemporizador = GetTemporizador(iMinutosObtencion, 1); 
                                //tmObtenerInformacion.Interval = GetTemporizador(iMinutosObtencion, 1); 
                                //tmObtenerInformacion.Interval = 1500;
                                tmObtenerInformacion.Start();
                            }
                            else
                            {
                                if (bRepetirProceso)
                                {
                                    iTemporizador = GetTemporizador(iMinutosObtencion, 2); 
                                    ////tmObtenerInformacion.Interval = GetTemporizador(iMinutosObtencion, 2); 
                                    /////tmObtenerInformacion.Stop();
                                    //tmObtenerInformacion.Interval = ((iMinutosObtencion * 60) * 60000) + 1;
                                    tmObtenerInformacion.Start();
                                }
                            }

                            ////// Revisar las configuraciones 
                            try
                            {
                                tmObtenerInformacion.Interval = iTemporizador;
                            }
                            catch
                            {
                                tmObtenerInformacion.Interval = (60000 * 3);
                                Error.LogError("Error al configurar el proceso de Obtención."); 
                            }


                            dFechaExec = dFechaExec.AddMilliseconds(tmObtenerInformacion.Interval);
                            lblObtencion.Text = "Obtención : " + General.FechaSistema.AddMilliseconds(tmObtenerInformacion.Interval).ToLongTimeString();
                            lblObtencion.Text = "Obtención : " + Fg.PonCeros(dFechaExec.Month, 2) + Fg.PonCeros(dFechaExec.Day, 2) + "  " + dFechaExec.ToLongTimeString();
                            
                        }
                    }
                }
            }


            ConfigObtencion_FTP();

            return bRegresa;
        }

        private int GetTemporizador(int Lapso, int Tipo)
        {
            clsLeer leerTemporizador = new clsLeer(); 
            int iTemporizador = 60000 * 5;
            string sHora = "";
            int iHoraEjecucion = 0;
            int iHoraActual = 0;


            DateTime dtF = General.FechaSistema;
            DateTime dt = new DateTime(dtF.Year, dtF.Month, dtF.Day, 0, 0, 0);

            DataSet dts = new DataSet("DtsEjecucion");
            DataTable dtT = new DataTable("DtEjecucion");
            dtT.Columns.Add("HoraCompleta", Type.GetType("System.String"));
            dtT.Columns.Add("Hora", Type.GetType("System.Int32"));
            dtT.Columns.Add("Minuto", Type.GetType("System.Int32"));
            dtT.Columns.Add("Segundo", Type.GetType("System.Int32"));

            try
            {
                while (General.FechaYMD(dtF, "") == General.FechaYMD(dt, ""))
                {
                    sHora = string.Format("{0}{1}{2}", Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2));
                    object[] obj = { sHora, Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2) };
                    dtT.Rows.Add(obj);

                    if (Tipo == 1)
                    {
                        dt = dt.AddMinutes(Lapso); 
                    }

                    if (Tipo == 2)
                    {
                        dt = dt.AddHours(Lapso); 
                    }

                    if (General.FechaYMD(dtF, "") != General.FechaYMD(dt, ""))
                    {
                        sHora = string.Format("{0}{1}{2}", 23, 59, 59);
                        object[] obj2 = { sHora, 23, 59, 59 };
                        dtT.Rows.Add(obj2);
                    }
                }

            }
            catch (Exception ex) 
            {
                Error.LogError(string.Format("Error al calcular las marcas de tiempo :  {0} ", ex.Message)); 
            }


            try 
            {
                sHora = string.Format("{0}{1}{2}", Fg.PonCeros(dtF.Hour, 2), Fg.PonCeros(dtF.Minute, 2), Fg.PonCeros(dtF.Second, 2));
                leerTemporizador.DataTableClase = dtT;
                leerTemporizador.DataRowsClase = dtT.Select(string.Format(" HoraCompleta >= {0} ", sHora));
                if (!leerTemporizador.Leer())
                {
                    iTemporizador = 60000 * 1;
                }
                else
                {
                    ////dt = new DateTime(dtF.Year, dtF.Month, dtF.Day, leerTemporizador.CampoInt("Hora"), leerTemporizador.CampoInt("Minuto"), 0);
                    ////iTemporizador = (int)dt.Ticks;


                    sHora = string.Format
                        (
                            "{0}{1}{2}", 
                            Fg.PonCeros(leerTemporizador.CampoInt("Hora"), 2), 
                            Fg.PonCeros(leerTemporizador.CampoInt("Minuto"), 2),
                            Fg.PonCeros(leerTemporizador.CampoInt("Segundo"), 2)
                        );

                    sHora = string.Format("{0}", Fg.PonCeros(leerTemporizador.CampoInt("Minuto"), 2));  
                    iHoraEjecucion = Convert.ToInt32(sHora);
                    iHoraEjecucion = (leerTemporizador.CampoInt("Hora") * 60) * 60000;
                    iHoraEjecucion += leerTemporizador.CampoInt("Minuto") * 60000;
                    iHoraEjecucion += leerTemporizador.CampoInt("Segundo") * 1000; 


                    sHora = string.Format("{0}{1}{0}", Fg.PonCeros(dtF.Hour, 2), Fg.PonCeros(dtF.Minute, 2), Fg.PonCeros(dtF.Second, 2));
                    sHora = string.Format("{0}", Fg.PonCeros(dtF.Minute, 2));
                    iHoraActual = Convert.ToInt32(sHora);
                    iHoraActual = (dtF.Hour * 60) * 60000;
                    iHoraActual += dtF.Minute * 60000;
                    iHoraActual += dtF.Second * 1000; 

                    iTemporizador = (iHoraEjecucion - iHoraActual) * 1;
                    
                    if (iTemporizador <= 0)
                    {
                        iTemporizador = 1000 * 5; //// Segundos adicionales 
                    }

                }
            }
            catch (Exception ex) 
            {
                Error.LogError(string.Format("Error al calcular la ejecución :  {0} ", ex.Message)); 
            }


            if (iTemporizador <= 0  )
            {
                iTemporizador = 60000 * 2;
            }

            return iTemporizador; 
        }

        private void ConfigIntegracion()
        {
            // bool bRegresa = true;
            bool bRutasConfiguradas = false;
            int iTemporizador = 0; 
            // DateTime dFechaExec = General.FechaSistema;
            DateTime dFechaExec = General.FechaSistemaObtener(); 

            bIntegracionConfigurada = false;
            string sSql = string.Format("Select * From {0} (NoLock) ", sIntegracion);

            ////if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            ////{
            ////    //btnIntegracion.Enabled = false; 
            ////    //lblIntegracion.Text = "Integración : Deshabilitada";
            ////}
            ////else
            {
                if (!leer.Exec(sSql))
                {
                    // bRegresa = true;
                    Error.GrabarError(leer, "ConfigIntegracion()");
                }
                else
                {
                    General.FechaSistemaObtener();
                    lblIntegracion.Text = "Integración : ";
                    if (!leer.Leer())
                    {
                        lblIntegracion.Text = "Integración : Sin configurar";
                    }
                    else 
                    {
                        if (CrearDirectorios(leer.Campo("RutaArchivosRecibidos")))
                        {
                            bRutasConfiguradas = CrearDirectorios(leer.Campo("RutaArchivosIntegrados"));
                        }

                        if (!bRutasConfiguradas)
                        {
                            lblIntegracion.Text = "Integración : Error de rutas";
                        }
                        else
                        {
                            bIntegracionConfigurada = true;
                            bServicio_Habilitado_Integracion = leer.CampoBool("ServicioHabilitado");

                            eTiempoIntegracion = (TiempoIntegracion)leer.CampoInt("TipoTiempo");
                            int iTiempo = leer.CampoInt("Tiempo");

                            tmIntegrarInformacion.Stop(); 
                            if (!bServicio_Habilitado_Integracion)
                            {
                                bIntegracionConfigurada = false;
                                lblIntegracion.Text = "Integración : Deshabilitada";
                            }
                            else
                            {
                                switch (eTiempoIntegracion)
                                {
                                    case TiempoIntegracion.Segundos:
                                        iTemporizador = (iTiempo * 1000) + 1;
                                        tmIntegrarInformacion.Interval = (iTiempo * 1000) + 1;
                                        break;

                                    case TiempoIntegracion.Minutos:
                                        ////tmIntegrarInformacion.Interval = (iTiempo * 60000) + 1;
                                        iTemporizador = GetTemporizador(iTiempo, 1);
                                        //tmIntegrarInformacion.Interval = GetTemporizador(iTiempo, 1); 
                                        break;

                                    case TiempoIntegracion.Horas:
                                        ////tmIntegrarInformacion.Interval = ((iTiempo * 60) * 60000) + 1;
                                        iTemporizador = GetTemporizador(iTiempo, 2);
                                        //tmIntegrarInformacion.Interval = GetTemporizador(iTiempo, 2); 
                                        break;
                                }

                                ////// Revisar las configuraciones 
                                try
                                {
                                    tmIntegrarInformacion.Interval = iTemporizador;
                                }
                                catch 
                                {
                                    tmIntegrarInformacion.Interval = (60000 * 3);
                                    Error.LogError("Error al configurar el proceso de Integración."); 
                                }


                                dFechaExec = dFechaExec.AddMilliseconds(tmIntegrarInformacion.Interval);
                                lblIntegracion.Text = "Integración : " + General.FechaSistema.AddMilliseconds(tmIntegrarInformacion.Interval).ToLongTimeString();
                                lblIntegracion.Text = "Integración : " + Fg.PonCeros(dFechaExec.Month, 2) + Fg.PonCeros(dFechaExec.Day, 2) + "  " + dFechaExec.ToLongTimeString();

                                tmIntegrarInformacion.Enabled = true; 
                                tmIntegrarInformacion.Start();
                            }
                        }
                    }
                }
            }
        }

        private void ConfigRespaldosBD()
        {
            // bool bRegresa = true;
            bRespaldoConfigurado = false;
            tmRespaldosBD.Enabled = true;
            bServicio_Habilitado_Respaldos = false; 

            string sSql = string.Format("Select * From {0} (NoLock) ", sRespaldoBD);
            if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
            {
                btnRespaldarBD.Enabled = false; 
                lblRespaldo.Text = "Respaldo : Deshabilitado"; 
            }
            else
            {
                if (!leer.Exec(sSql))
                {
                    // bRegresa = true;
                    Error.GrabarError(leer, "ConfigRespaldosBD()");
                }
                else
                {
                    General.FechaSistemaObtener();
                    lblRespaldo.Text = "Respaldo : "; 
                    bRespaldoConfigurado = true; 

                    if (!leer.Leer())
                    {
                        // Por Default los respaldos se generan cada hora en caso de no contar con la configuracion 
                        tmRespaldosBD.Interval = (60000 * 60) + 1;
                        lblRespaldo.Text = "Respaldo : " + General.FechaSistema.AddMilliseconds(tmRespaldosBD.Interval).ToLongTimeString();
                        tmRespaldosBD.Start();
                    }
                    else 
                    {
                        bRespaldoConfigurado = true; 
                        bServicio_Habilitado_Respaldos = leer.CampoBool("ServicioHabilitado");
                        sRutaRespaldos = leer.Campo("RutaDeRespaldos");
                        eTiempoRespaldos = (TipoTiempo)leer.CampoInt("TipoTiempo");
                        int iTiempo = leer.CampoInt("Tiempo");

                        tmRespaldosBD.Stop();
                        switch (eTiempoRespaldos)
                        {
                            //case TiempoIntegracion.Segundos:
                            //    tmRespaldosBD.Interval = (iTiempo * 1000) + 1;
                            //    break;

                            case TipoTiempo.Minutos:
                                ////tmRespaldosBD.Interval = (iTiempo * 60000) + 1;
                                tmRespaldosBD.Interval = GetTemporizador(iTiempo, 1); 
                                break;

                            case TipoTiempo.Horas:
                                ////tmRespaldosBD.Interval = ((iTiempo * 60) * 60000) + 1;
                                tmRespaldosBD.Interval = GetTemporizador(iTiempo, 2); 
                                break;
                        }

                        //////// Crear el Directorio de Respaldos 
                        //////if (!Directory.Exists(sRutaRespaldos)) 
                        //////{
                        //////    Directory.CreateDirectory(sRutaRespaldos);
                        //////}
                        if (!CrearDirectorios(sRutaRespaldos))
                        {
                            sRutaRespaldos = Application.StartupPath; 
                        }

                        if (!bServicio_Habilitado_Respaldos)
                        {
                            bRespaldoConfigurado = false; 
                            lblRespaldo.Text = "Respaldo : Deshabilitado"; 
                        }
                        else 
                        {
                            lblRespaldo.Text = "Respaldo : " + General.FechaSistema.AddMilliseconds(tmRespaldosBD.Interval).ToLongTimeString();
                            tmRespaldosBD.Start();
                        }
                    }

                    tmEnvioFTP.Enabled = true;
                    tmEnvioFTP.Start();  
                }
            }
        }
        #endregion Obtencion de configuraciones 

        #region Ejecutar Procesos 
        private void Status(bool Estado)
        {
            Transferencia.EjecutandoProcesos = !Estado; 

            // FramePrincipal.Enabled = Estado; 
            // FrameEnEjecucion.Visible = !FrameEnEjecucion.Visible;  

            //if ( !Estado )
            //{
            //    FrameProcesosManual.Location = pointFrameEnEjecucion; 
            //    FrameEnEjecucion.Location = pointFrameManual; 
            //}
            //else 
            //{
            //    FrameProcesosManual.Location = pointFrameManual; 
            //    FrameEnEjecucion.Location = pointFrameEnEjecucion; 
            //}

            this.Refresh(); 
            Thread.Sleep(100);
            this.Refresh(); 

            FrameProcesos.Enabled = Estado;
            FrameProcesosManual.Enabled = Estado; 
            FrameTiempoEjecucion.Enabled = Estado;
            // FrameProcesosManual.Visible = Estado; 
            // cboProcesos.Enabled = Estado; 
        }

        private void ObtenerInformacion()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                Status(false);
                if (bObtencionConfigurada)
                {
                    if (tpServicio == TipoServicio.OficinaCentral)
                    {
                        DllTransferenciaSoft.ObtenerInformacion.clsOficinaCentral OficinaCentral =
                            new DllTransferenciaSoft.ObtenerInformacion.clsOficinaCentral(DtGeneral.CfgIniOficinaCentral, General.DatosConexion, 
                                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                        OficinaCentral.GenerarArchivos();
                        OficinaCentral.EnviarArchivos();
                    }

                    if (tpServicio == TipoServicio.OficinaCentralRegional)
                    {
                        DllTransferenciaSoft.ObtenerInformacion.clsOficinaCentralRegional OficinaCentralRegional =
                            new DllTransferenciaSoft.ObtenerInformacion.clsOficinaCentralRegional(DtGeneral.CfgIniPuntoDeVenta, General.DatosConexion,
                                DtGeneral.ClaveRENAPO, DtGeneral.FarmaciaConectada);
                        OficinaCentralRegional.GenerarArchivos();
                        OficinaCentralRegional.EnviarArchivos(); 
                    }

                    if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
                    {
                        DllTransferenciaSoft.ObtenerInformacion.clsClienteOficinaCentralRegional ClienteOficinaCentralRegional =
                            new DllTransferenciaSoft.ObtenerInformacion.clsClienteOficinaCentralRegional(DtGeneral.CfgIniOficinaCentral, 
                                General.DatosConexion, DtGeneral.ClaveRENAPO, DtGeneral.FarmaciaConectada);
                        ClienteOficinaCentralRegional.GenerarArchivos();
                        ClienteOficinaCentralRegional.EnviarArchivos();
                    }

                    if (tpServicio == TipoServicio.Cliente)
                    {
                        DllTransferenciaSoft.ObtenerInformacion.clsCliente Cliente =
                            new DllTransferenciaSoft.ObtenerInformacion.clsCliente(DtGeneral.CfgIniOficinaCentral, General.DatosConexion);
                        Cliente.GenerarArchivos();
                        Cliente.EnviarArchivos();
                    }
                }
                Status(true);
            }
        }

        private void EnviarInformacion()
        {
        }

        private void IntegrarInformacion()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                Status(false);
                if (bIntegracionConfigurada)
                {
                    if (tpServicio == TipoServicio.OficinaCentral)
                    {
                        DllTransferenciaSoft.IntegrarInformacion.clsOficinaCentral OficinaCentral =
                            new DllTransferenciaSoft.IntegrarInformacion.clsOficinaCentral(General.DatosConexion);
                        OficinaCentral.Integrar();
                    }

                    if (tpServicio == TipoServicio.OficinaCentralRegional)
                    {
                        DllTransferenciaSoft.IntegrarInformacion.clsOficinaCentralRegional OficinaCentralRegional =
                            new DllTransferenciaSoft.IntegrarInformacion.clsOficinaCentralRegional(General.DatosConexion);
                        OficinaCentralRegional.Integrar();
                    }


                    if (tpServicio == TipoServicio.Cliente)
                    {
                        DllTransferenciaSoft.IntegrarInformacion.clsCliente Cliente =
                            new DllTransferenciaSoft.IntegrarInformacion.clsCliente(General.DatosConexion);
                        Cliente.Integrar();
                    }

                    //// Este solo es para Reenvio de Informacion desde Servidores Regionales a Servidor Central 
                    if (tpServicio == TipoServicio.ClienteOficinaCentralRegional)
                    {
                        DllTransferenciaSoft.IntegrarInformacion.clsClienteOficinaCentralRegional ClienteOficinaCentralRegional =
                            new DllTransferenciaSoft.IntegrarInformacion.clsClienteOficinaCentralRegional(General.DatosConexion);
                        ClienteOficinaCentralRegional.Integrar();
                    }
                }
                Status(true);
            } 
        }

        private void GenerarRespaldo()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                Status(false);
                if (bRespaldoConfigurado & !bEnviandoRespaldo)
                {
                    svrSql = new clsSQL(General.DatosConexion, sPrefijoRespaldo, sRutaRespaldos);
                    // svrSql.LogBd.ReducirLog();
                    // svrSql.LogBd.ReducirBD();

                    // **** Los servidores Regionales y el Central la Base de Datos se reduce manualmente 
                    // En el Servidor Central la Base De Datos se reduce manualmente. 
                    if (tpServicio == TipoServicio.Cliente || tpServicio == TipoServicio.OficinaCentralRegional)
                    {
                        try
                        {
                            leer.Exec("Drop table CtlErrores");
                            leer.Exec("spp_Mtto_Limpiar_Tablas_ListaLotes"); 
                        } 
                        catch { } 

                        svrSql.LogBd.ReducirLog();
                        svrSql.LogBd.ReducirBD();

                        // Borrar los respaldos anteriores 
                        LimpiarDirectorioRespaldos(); 
                    }

                    if (svrSql.BackUp.Respaldar())
                    {
                        sFileDestino = svrSql.BackUp.NombreArchivo + ".SII";
                        sOrigen = sRutaRespaldos + "\\" + svrSql.BackUp.NombreArchivo + ".bak";
                        sDestino = sRutaRespaldos + "\\" + sFileDestino;

                        ZipUtil zip = new ZipUtil();
                        zip.Type = Transferencia.Modulo + "Install";
                        zip.Type = Transferencia.Modulo + "BackUp";

                        zip.Comprimir(sOrigen, sDestino, true);

                        ////////// Solo enviar los Respaldos de las Farmacias 
                        if (tpServicio == TipoServicio.Cliente)
                        {
                            ThreadEnviarRespaldoFTP();
                        }
                    }

                    //////// Solo enviar los Respaldos de las Farmacias 
                    //////if (tpServicio == TipoServicio.Cliente)
                    //////{
                    //////    ThreadEnviarRespaldoFTP();
                    //////}
                }
                Status(true);
            }
            //tmRespaldosBD.Enabled = false;
            //tmRespaldosBD.Stop();

            //Thread _workerThread = new Thread(this.RespaldarBaseDeDatos);
            ////_workerThread.Name = grid.GetValue(i, 2);
            //_workerThread.Start(); 
        }

        private void LimpiarDirectorioRespaldos()
        {
            // Transferencia.ExtArchivosGenerados 
            DirectoryInfo x = new DirectoryInfo(sRutaRespaldos);

            DateTime dtFechaSistema = General.FechaSistema; 
            SC_ControlsCS.scDateTimePicker dtpDiff = new SC_ControlsCS.scDateTimePicker();
            long iDif = 0; 

            foreach (FileInfo f in x.GetFiles("*.SII"))
            {
                iDif = dtpDiff.DateDiff(SC_ControlsCS.DateInterval.Day, f.LastWriteTime, dtFechaSistema);

                if (iDif > iDiasRespaldos)
                {
                    try
                    {
                        File.Delete(f.FullName); 
                    }
                    catch { }
                }
            }
        }

        #endregion Ejecutar Procesos

        #region Timers 
        private void cboProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ( cboProcesos.Data ) 
            {
                case "0":
                    btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
                    btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
                    pcServer.Image = pcServerInactivo.Image;
                    break;

                case "1":
                    if (bEjecutarObtencion)
                    {
                        btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
                        btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopActivo];
                        pcServer.Image = pcServerStar.Image;
                    }
                    else
                    {
                        btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayActivo];
                        btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
                        pcServer.Image = pcServerStop.Image; 
                    }
                    break;

                case "2":
                    if (bEjecutarIntegracion)
                    {
                        btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayInactivo];
                        btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopActivo];
                        pcServer.Image = pcServerStar.Image;
                    }
                    else
                    {
                        btnIniciar.Image = imgListaOpcionesServer.Images[(int)Iconos.PlayActivo];
                        btnDetener.Image = imgListaOpcionesServer.Images[(int)Iconos.StopInactivo];
                        pcServer.Image = pcServerStop.Image;
                    }
                    break;
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            bool bEjecutar = bEsInicioDeProceso; 

            if (cboProcesos.Data == "1")
            {
                if (!bEsInicioDeProceso)
                {
                    bEjecutar = bServicio_Habilitado_Obtencion; 
                }

                if (bEjecutar)
                {
                    Obtencion(true);
                }
            }

            if (cboProcesos.Data == "2")
            {
                if (!bEsInicioDeProceso)
                {
                    bEjecutar = bServicio_Habilitado_Integracion;
                }

                if (bEjecutar)
                {
                    Integracion(true);
                }
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            if (cboProcesos.Data == "1")
            {
                Obtencion(false);
            }

            if (cboProcesos.Data == "2")
            {
                Integracion(false);
            }
        }

        private bool validarHoraEjecucion()
        {
            bool bRegresa = false;

            if (bObtencionConfigurada)
            {
                General.FechaSistemaObtener();

                DateTimePicker dtpEjecucion = new DateTimePicker();
                DateTimePicker dtpHoraActual = new DateTimePicker();

                dtpEjecucion.Format = DateTimePickerFormat.Time;
                dtpHoraActual.Format = DateTimePickerFormat.Time;

                dtpEjecucion.Value = dHoraEjecucion;
                dtpHoraActual.Value = General.FechaSistema;

                // (General.Hora(dHoraEjecucion) <= General.FechaSistemaHora)            
                if (dtpEjecucion.Value.Hour <= dtpHoraActual.Value.Hour)
                {
                    bRegresa = true;
                }
                else if (dtpEjecucion.Value.Hour <= dtpHoraActual.Value.Hour)
                {
                    if (dtpEjecucion.Value.Minute == dtpHoraActual.Value.Minute)
                    {
                        bRegresa = true;
                    }
                }
            }

            return bRegresa;
        }

        private void tmObtenerInformacion_Tick(object sender, EventArgs e)
        {
            //ThreadObtencion();
            if (bEjecutarObtencion)
            {
                bool bContinuar = true;

                //////if (!bRepetirProceso)
                //////{
                //////    bContinuar = bContinuar;
                //////}

                if (bContinuar)
                {
                    if ((Semaforo == Verde) && validarHoraEjecucion())
                    {
                        Semaforo = Rojo;
                        try
                        {
                            ObtenerInformacion();
                            // EnviarInformacion();
                        }
                        catch 
                        {
                            // Asegurar que se activen las opciones 
                            Transferencia.EjecutandoProcesos = false;
                            Status(true); 
                        }
                        finally
                        {
                            ConfigObtencion(); 
                            Semaforo = Verde;
                        }
                    }
                    else
                    {
                        if ((Semaforo == Rojo) && validarHoraEjecucion())
                        {
                            tmObtenerInformacion.Stop();
                            tmObtenerInformacion.Interval = 20000;
                            tmObtenerInformacion.Start();
                        }
                    }
                }

            }
        } 

        private void tmIntegrarInformacion_Tick(object sender, EventArgs e)
        {
            ThreadIntegracion(); 

            ////if (bEjecutarIntegracion)
            ////{
            ////    if (Semaforo == Verde)
            ////    {
            ////        Semaforo = Rojo;
            ////        try
            ////        {
            ////            IntegrarInformacion();
            ////        }
            ////        catch 
            ////        {
            ////            ////// Asegurar que se activen las opciones 
            ////            Transferencia.EjecutandoProcesos = false;
            ////            Status(true);  
            ////        }
            ////        finally
            ////        {
            ////            ConfigIntegracion();
            ////            Semaforo = Verde;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        tmIntegrarInformacion.Stop();
            ////        tmIntegrarInformacion.Interval = 20000;
            ////        tmIntegrarInformacion.Start(); 
            ////    }
            ////}
        }

        private void tmRespaldosBD_Tick(object sender, EventArgs e)
        {
            if (bRespaldoConfigurado && bServicio_Habilitado_Respaldos)
            {
                if (Semaforo == Verde)
                {
                    Semaforo = Rojo;
                    try
                    {
                        GenerarRespaldo();
                    }
                    catch { }
                    finally
                    {
                        ConfigRespaldosBD();
                        Semaforo = Verde;
                    }
                }
                else
                {
                    tmIntegrarInformacion.Stop();
                    tmIntegrarInformacion.Interval = 20000;
                    tmIntegrarInformacion.Start();
                }
            }
        }

        private void tmEnvioFTP_Tick(object sender, EventArgs e)
        {
            if (iStatusEnvioFTP == 2)
            {
                iStatusEnvioFTP = 0;
                tmEnvioFTP.Stop();
                tmEnvioFTP.Enabled = false;

                try
                {
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    SC_SolutionsSystem.Errores.clsGrabarError.LogFileError("001   " + ex.Message);
                    //General.msjError("001   " + ex.Message); 
                }

                try
                {
                    Application.ExitThread(); 
                }
                catch (Exception ex)
                {
                    SC_SolutionsSystem.Errores.clsGrabarError.LogFileError("002   " + ex.Message);
                    //General.msjError("002   " + ex.Message); 
                }

            }
        }        
        #endregion Timers 

        #region Activar procesos manualmente
        private void btnObtencion_Click(object sender, EventArgs e)
        {
            tmObtenerInformacion_Tick(null, null);  
            //ObtenerInformacion();
            //ConfigObtencion();
        }

        private void btnIntegracion_Click(object sender, EventArgs e)
        {
            //tmIntegrarInformacion_Tick(null, null); 
            
            ThreadIntegracion(); 
        }

        private void btnRespaldarBD_Click(object sender, EventArgs e)
        {
            tmRespaldosBD_Tick(null, null); 
            //GenerarRespaldo();
            //ConfigRespaldosBD();
        }
        #endregion Activar procesos manualmente 

        #region Manejo de Procesos en Hilos
        private void ThreadObtencion()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                threadObtencion = new Thread(this.ObtenerInformacion);
                threadObtencion.Name = "Obtención de Información.";
                threadObtencion.Start();
            }
        }

        private void ThreadIntegracion()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                tmLogIntegracion.Interval = (1000) * 2;
                tmLogIntegracion.Enabled = true;
                tmLogIntegracion.Start(); 

                threadIntegracion = new Thread(this.ProcesoDeIntegracion);
                threadIntegracion.Name = "Integración de Información.";
                threadIntegracion.Start();
            }
        }

        private void ThreadRespaldos()
        {
            if (!Transferencia.EjecutandoProcesos)
            {
                threadRespaldos = new Thread(this.ProcesoDeRespaldos);
                threadRespaldos.Name = "Generar respaldo de Base de Datos.";
                threadRespaldos.Start();
            }
        }

        private void ThreadEnviarRespaldoFTP()
        {
            threadRespaldosFTP = new Thread(this.EnviarRespaldoFTP);
            threadRespaldosFTP.Name = "Enviar Respaldo";
            threadRespaldosFTP.Start(); 
        }

        private void EnviarRespaldoFTP()
        {
            bEnviandoRespaldo = true;
            iStatusEnvioFTP = 1; 

            ////string sMsjFTP = ""; 
            clsFTP FTP = new clsFTP(sFTP_Servidor, sFTP_Usuario, sFTP_Password, false, bFTP_ModoActivoDeTransferencia); 
            string sPath = @"RESPALDOS_DBS/" + DtGeneral.EstadoConectadoNombre + @"/" + DtGeneral.FarmaciaConectada + @"/";
            sPath = Transferencia.DirectorioFTP + @"/" + DtGeneral.EstadoConectadoNombre + @"/" + DtGeneral.FarmaciaConectada + @"/"; 

            // sPath = string.Format(@"{0}/{1}/{2}", Transferencia.DirectorioFTP, DtGeneral.EstadoConectadoNombre, DtGeneral.FarmaciaConectada);    


            ////FTP.Host = Transferencia.ServidorFTP;
            ////FTP.Usuario = "FtpUser";
            ////FTP.Password = SC_SolutionsSystem.Criptografia.clsPassword.FTP(FTP.Host, FTP.Usuario); 
            FTP.Host = sFTP_Servidor; 
            FTP.Usuario = sFTP_Usuario; 
            FTP.Password = sFTP_Password;

            if (ValidarEnvio_FTP())
            {
                FTP.PrepararConexion();
                FTP.CrearDirectorio(sPath);
                FTP.SubirArchivo(sDestino, sPath, sFileDestino);
                //General.msjAviso("Terminar Servicio Cliente"); 
            } 

            bEnviandoRespaldo = false;
            iStatusEnvioFTP = 2; 
        }

        private bool ValidarEnvio_FTP()
        {
            bool bRegresa = false;
            string sSql = "Select *, (case when (HoraActual between Inicio and Fin) then 1 else 0 end) as Enviar \n" +
                "From \n" + 
                "( " +
                string.Format("\n\tSelect EnvioFTP_Habilitado, bLunes, bMartes, bMiercoles, " +
                "\n\t bJueves, bViernes, bSabado, bDomingo, HoraInicia, HoraFinaliza, " +
                "\n\t replace(convert(varchar(5), HoraInicia, 108), ':', '') as Inicio, " +
                "\n\t replace(convert(varchar(5), HoraFinaliza, 108), ':', '') as Fin,  " +
                "\n\t replace(convert(varchar(5), getdate(), 108), ':', '') as HoraActual " +
                "\n\t From {0} (NoLock) \n", sRespaldoBD);
            sSql += ") as C "; 
            string sDia = "b" + General.FechaNombreDia(DateTime.Now);
            string sHora = General.Hora(DateTime.Now, ""); 

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ValidarEnvio_FTP()");
            }
            else
            {
                if (leer.Leer())
                {
                    if (leer.CampoBool("EnvioFTP_Habilitado"))
                    {
                        if (leer.CampoBool(sDia))
                        {
                            bRegresa = leer.CampoBool("Enviar"); 
                        }
                    }
                }
            }

            return bRegresa; 
        }

        private void ProcesoDeObtencion() 
        {
            if (bEjecutarObtencion)
            {
                bool bContinuar = true;

                //////if (!bRepetirProceso)
                //////{
                //////    bContinuar = bContinuar;
                //////}

                if (bContinuar)
                {
                    if ((Semaforo == Verde) && validarHoraEjecucion())
                    {
                        Semaforo = Rojo;
                        try
                        {
                            tmObtenerInformacion.Stop();
                            ObtenerInformacion();
                            EnviarInformacion();
                        }
                        catch { }
                        finally
                        {
                            ConfigObtencion();
                            Semaforo = Verde;
                        }
                    }
                    else
                    {
                        if ((Semaforo == Rojo) && validarHoraEjecucion())
                        {
                            tmObtenerInformacion.Stop();
                            tmObtenerInformacion.Interval = 20000;
                            tmObtenerInformacion.Start();
                        }
                    }
                }
            }

            if (threadObtencion != null)
            {
                threadObtencion = null;
            } 

        }

        private void ProcesoDeIntegracion()
        {
            if (bEjecutarIntegracion)
            {
                if (Semaforo == Verde)
                {
                    Semaforo = Rojo; 

                    try
                    {
                        tmIntegrarInformacion.Stop();
                        IntegrarInformacion();
                    }
                    catch 
                    {
                        // Asegurar que se activen las opciones 
                        Transferencia.EjecutandoProcesos = false;
                        Status(true);   
                    }
                    finally
                    {
                        tmLogIntegracion.Stop(); 
                        tmLogIntegracion.Enabled = false;

                        ConfigIntegracion();
                        Semaforo = Verde;
                    }
                }
                else
                {
                    tmIntegrarInformacion.Stop();
                    tmIntegrarInformacion.Interval = 20000;
                    tmIntegrarInformacion.Start();
                }
            }

            if (threadIntegracion != null)
            {
                threadIntegracion = null;
            }
        }

        private void ProcesoDeRespaldos()
        {
            if (bRespaldoConfigurado && bServicio_Habilitado_Respaldos)
            {
                if (Semaforo == Verde)
                {
                    Semaforo = Rojo;
                    try
                    {
                        GenerarRespaldo();
                    }
                    catch { }
                    finally
                    {
                        ConfigRespaldosBD();
                        Semaforo = Verde;
                    }
                }
                else
                {
                    tmIntegrarInformacion.Stop();
                    tmIntegrarInformacion.Interval = 20000;
                    tmIntegrarInformacion.Start();
                }
            }

            if (threadRespaldos != null)
            {
                threadRespaldos = null;
            }

        }
        #endregion Manejo de Procesos en Hilos

        #region Iniciar Servicios 
        private void PrepararIniciosAutomaticos(bool Activar)
        {
            if (Activar)
            {
                tmIniciarServicios.Interval = (1000) * 5;
                tmIniciarServicios.Enabled = true;
                tmIniciarServicios.Start();
            }
            else
            {
                tmIniciarServicios.Stop();
                tmIniciarServicios.Enabled = false;
            }
        }

        private void tmIniciarServicios_Tick(object sender, EventArgs e)
        {
            if (!DtGeneral.EsEquipoDeDesarrollo)
            {
                if (Semaforo)
                {
                    Semaforo = Rojo;
                    PrepararIniciosAutomaticos(false);

                    string sRutaServicio = Path.Combine(Application.StartupPath, sServicioParalelo) + ".exe";
                    if (File.Exists(sRutaServicio))
                    {
                        if (!General.ProcesoEnEjecucionSimple(sServicioParalelo + ".exe"))
                        {
                            // General.msjAviso(Application.ProductName +  " ===== " +  sServicioParalelo); 
                            Process svr = new Process();
                            svr.StartInfo.FileName = sRutaServicio;
                            svr.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            svr.Start();
                        }
                    }
                    Semaforo = Verde;
                    PrepararIniciosAutomaticos(true);
                }
            }
        }
        #endregion Iniciar Servicios 

        #region Logs 
        private void LogIntegracion()
        {
            if (!clsDatosIntegracion.LogAbierto)
            {
                if (!clsDatosIntegracion.ForzarCierre)
                {
                    fLog = new FrmLogIntegracion();
                    fLog.ShowDialog(); 
                }
            }
        } 

        private void tmLogIntegracion_Tick(object sender, EventArgs e)
        {
            tmLogIntegracion.Interval = (1000) * 60;
            LogIntegracion();
        }
        #endregion Logs

        #region Habilitar-Deshabilitar servicios 
        
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            string sName = ((ToolStripMenuItem)sender).Tag.ToString();            
            ServicioStatus(sName, 1); 
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            string sName = ((ToolStripMenuItem)sender).Tag.ToString();              
            ServicioStatus(sName, 0); 
        }

        private void ServicioStatus(string Opcion, int Valor)
        {
            string sSql = "";
            string sStatus = Valor == 0 ? "deshabilitar" : "habilitar";
            string sServicio = ""; 

            switch (Opcion)
            {
                case "btnObtencion":
                    sServicio = "obtención"; 
                    sSql = string.Format("Update {0} Set ServicioHabilitado = {1} ", sObtencion, Valor); 
                    break;

                case "btnIntegracion":
                    sServicio = "integración"; 
                    sSql = string.Format("Update {0} Set ServicioHabilitado = {1} ", sIntegracion, Valor); 
                    break;

                case "btnRespaldarBD":
                    sServicio = "respaldos"; 
                    sSql = string.Format("Update {0} Set ServicioHabilitado = {1} ", sRespaldoBD, Valor); 
                    break; 
            }

            if (!leer.Exec(sSql))
            {
                General.msjError(string.Format("Ocurrió un error al {0} el servicio de {1}", sStatus, sServicio));
            }
            else
            {
                switch (Opcion)
                {
                    case "btnObtencion":
                        ConfigObtencion(); 
                        break;

                    case "btnIntegracion":
                        ConfigIntegracion(); 
                        break;

                    case "btnRespaldarBD":
                        ConfigRespaldosBD(); 
                        break;
                }
            }
        }
        #endregion Habilitar-Deshabilitar servicios

    }
}
