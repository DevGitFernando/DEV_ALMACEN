using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Timers;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.SistemaOperativo;
using SC_SolutionsSystem.SQL;

using DllFarmaciaSoft;

using DllTransferenciaSoft;
using DllTransferenciaSoft.IntegrarInformacion;
using DllTransferenciaSoft.ObtenerInformacion;
using DllTransferenciaSoft.Zip;

using DllTransferenciaSoft.wsCliente;
using DllTransferenciaSoft.wsOficinaCentral;


namespace DllTransferenciaSoft.Servicio
{
    public class ServicioSII : ServiceBase
    {
        //System.Timers.Timer myTimer;
        //EventLog myEventLog;

        // DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login;
        // Servicio svrServidor = new Servicio(TipoServicio.OficinaCentral);

        #region Declaracion de variables
        #region Control de Procesos
        bool Semaforo = false;
        const bool Verde = true;
        const bool Rojo = false;
        #endregion Control de Procesos

        clsIniciarConSO SO;
        // Form frmPadre;
        string sAppName = "DemonioSOC";
        string sAppRuta = Application.ExecutablePath;

        bool bEjecutarObtencion = false;
        bool bEjecutarIntegracion = false;
        //bool bEstoyEnSystray = false;

        bool bObtencionConfigurada = false;
        bool bIntegracionConfigurada = false;
        bool bRespaldoConfigurado = false;


        // bool bEjecutarProceso = false;
        bool bRepetirProceso = false;
        //bool Inicio = true;
        // int iPeridiocidad = 0;
        int iMinutosObtencion = 0;
        // int iMinutosIntegracion = 0;
        //int iTiempo = 0;
        // int iTiempoTiempo = 0;
        DateTime dHoraEjecucion;

        Peridiocidad ePeriodicidad = Peridiocidad.Diariamente;
        TipoTiempo eTipoTiempo = TipoTiempo.Minutos;
        TiempoIntegracion eTiempoIntegracion = TiempoIntegracion.Segundos;
        TipoTiempo eTiempoRespaldos = TipoTiempo.Horas;
        string sRutaRespaldos = "";
        string sPrefijoRespaldo = "";

        clsConexionSQL cnn; // = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        TipoServicio tpServicio = TipoServicio.Ninguno;
        //string sTitulo = "", sSysTray = "";
        string sObtencion = "", sIntegracion = "", sRespaldoBD = "";

        ////Size sizeFrameManual;
        ////Point pointFrameManual;
        ////Point pointFrameEnEjecucion;

        ////// Manejo de Hilos 
        ////Thread threadObtencion;
        ////Thread threadIntegracion;
        ////Thread threadRespaldos;


        //public FrmServicio()
        //{
        //    InitializeComponent();
        //    InicializaForm();
        //} 

        internal System.Timers.Timer tmObtenerInformacion;
        internal System.Timers.Timer tmIntegrarInformacion;
        internal System.Timers.Timer tmRespaldosBD;
        string Name = "Servicio";
        clsGrabarError Error;

        #endregion Declaracion de variables  


        public ServicioSII(TipoServicio Servicio)
        {
            this.CanPauseAndContinue = true;
            this.CanStop = true;


            tpServicio = Servicio; 
            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;

            // DllTransferenciaSoft.wsCliente.wsCnnCliente svrIniciarCliente;
            // DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral svrIniciarServidor; 

            if (tpServicio == TipoServicio.OficinaCentral)
            {
                General.ArchivoIni = "OficinaCentralRI";
            }

            if (tpServicio == TipoServicio.Cliente)
            {
                General.ArchivoIni = "FarmaciaPtoVtaRI";
            }

        }

        #region Metodos principales 
        protected override void OnStart(string[] args)
        {
            IniciarServicio(); 
        }

        protected override void OnStop()
        {
            tmIntegrarInformacion.Stop();
            tmObtenerInformacion.Stop();
            tmRespaldosBD.Stop(); 
        }

        protected override void OnContinue()
        {
            IniciarServicio(); 
        }

        protected override void OnPause()
        {
            tmIntegrarInformacion.Stop();
            tmObtenerInformacion.Stop();
            tmRespaldosBD.Stop(); 
        }
        #endregion Metodos principales

        #region Funciones 
        private void IniciarServicio()
        {
            DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("", "");
            Login.AutenticarServicioSO();

            ////////// Control Proceso 
            Semaforo = Verde;

            // InicializaForm(); 
            // InicarServicios();

            //myTimer = new System.Timers.Timer();
            //myTimer.Interval = 1000 * 60;
            //myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            //myTimer.Start();
            
            //myTimer_Elapsed
        } 

        private bool ProcesoEnEjecucion()
        {
            return ProcesoEnEjecucion(Process.GetCurrentProcess().ProcessName);
        }

        private bool ProcesoEnEjecucion(string NombreProceso)
        {
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length >= 1;
            return bRegresa;
        }

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //myTimer.Enabled = false;
            //string sMsj = "";
            //int iValor = -10; 
            //string sServicio = ""; 
            //string sFile = Application.StartupPath + "\\Servicio Oficina Central.exe";


            //if (tpServicio == TipoServicio.OficinaCentral) 
            //    sServicio = "Servicio Oficina Central"; 

            //if (tpServicio == TipoServicio.Cliente) 
            //    sServicio = "Servicio Cliente"; 


            //if (!ProcesoEnEjecucion(sServicio))
            //{
            //    try
            //    {
            //        if (tpServicio == TipoServicio.OficinaCentral)
            //        {
            //            DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral svrIniciarServidor;
            //            svrIniciarServidor = new DllTransferenciaSoft.wsOficinaCentral.wsCnnOficinaCentral();
            //            svrIniciarServidor.Url = General.Url;

            //            iValor = svrIniciarServidor.IniciarServicio("");
            //        }

            //        if (tpServicio == TipoServicio.Cliente)
            //        {

            //            DllTransferenciaSoft.wsCliente.wsCnnCliente svrIniciarCliente;
            //            svrIniciarCliente = new DllTransferenciaSoft.wsCliente.wsCnnCliente();
            //            svrIniciarCliente.Url = General.Url;

            //            iValor = svrIniciarCliente.IniciarServicio("");
            //        }

            //        sMsj = string.Format("Aplicación {0} con estado {1} ", sServicio, General.Url);
            //        EventLog.WriteEntry(sMsj, EventLogEntryType.Information);
            //    }
            //    catch ( Exception ex ) 
            //    {
            //        EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            //    }
            //}
            
            //myTimer.Enabled = true;
            //myTimer.Start();

        }
        #endregion Funciones


        #region Interfaz
        void InicializaForm()
        {
            if (tpServicio == TipoServicio.Cliente)
            {
                ////sTitulo = "Cliente";
                ////sSysTray = "Servicio Cliente";
                sAppName = "DemonioSC";
                sObtencion = " CFGC_ConfigurarObtencion ";
                sIntegracion = " CFGC_ConfigurarIntegracion ";
                sRespaldoBD = " Net_CFGC_Respaldos ";
                sPrefijoRespaldo = "PtoVta";
            }
            else if (tpServicio == TipoServicio.OficinaCentral)
            {
                ////sTitulo = "Oficina Central";
                ////sSysTray = "Servicio Oficina Central";
                sAppName = "DemonioSOC";
                sObtencion = " CFGS_ConfigurarObtencion ";
                sIntegracion = " CFGS_ConfigurarIntegracion ";
                sRespaldoBD = " Net_CFGS_Respaldos ";
                sPrefijoRespaldo = "PtoCen";
            }

            SO = new clsIniciarConSO(sAppName, sAppRuta);
            cnn = new clsConexionSQL(General.DatosConexion); 
            leer = new clsLeer(ref cnn);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(Transferencia.Modulo + ".Servicio.Servicio", Transferencia.Version, this.Name + "sAppName");

            //myTimer.Interval = 1000 * 30;
            //myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            //myTimer.Start();

            double iMinutos = 5;  

            tmObtenerInformacion = new System.Timers.Timer();
            tmObtenerInformacion.Interval = 60000 * iMinutos;
            tmObtenerInformacion.Enabled = false;
            tmObtenerInformacion.Elapsed += new ElapsedEventHandler(this.tmObtenerInformacion_Tick);


            tmIntegrarInformacion = new System.Timers.Timer();
            tmIntegrarInformacion.Interval = 60000 * iMinutos;
            tmIntegrarInformacion.Enabled = false;
            tmIntegrarInformacion.Elapsed += new ElapsedEventHandler(tmIntegrarInformacion_Tick);


            tmRespaldosBD = new System.Timers.Timer();
            tmRespaldosBD.Interval = 60000 * iMinutos;
            tmRespaldosBD.Enabled = false;
            tmRespaldosBD.Elapsed += new ElapsedEventHandler(tmRespaldosBD_Tick); 
        }

        private void InicarServicios()
        {
            ConfigObtencion();
            ConfigIntegracion();
            ConfigRespaldosBD();

            bEjecutarObtencion = true;
            bEjecutarIntegracion = true;
            bRespaldoConfigurado = true; 


            //tmRespaldosBD.Interval = 10000;
            tmRespaldosBD.Enabled = bRespaldoConfigurado;
            if (bRespaldoConfigurado)
            {
                tmRespaldosBD.Start();
            }

        }
        #endregion Interfaz

        #region Obtencion de configuraciones
        private bool ConfigObtencion()
        {
            bool bRegresa = true;
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
                if (leer.Leer())
                {
                    bObtencionConfigurada = true;
                    ePeriodicidad = (Peridiocidad)leer.CampoInt("Periodicidad");
                    eTipoTiempo = (TipoTiempo)leer.CampoInt("TipoTiempo");

                    iMinutosObtencion = leer.CampoInt("Tiempo");
                    dHoraEjecucion = leer.CampoFecha("HoraEjecucion");
                    bRepetirProceso = leer.CampoBool("bRepetirProceso");

                    if (eTipoTiempo == TipoTiempo.Minutos && bRepetirProceso)
                    {
                        tmObtenerInformacion.Stop();
                        tmObtenerInformacion.Interval = (iMinutosObtencion * 60000) + 1;
                        //tmObtenerInformacion.Interval = 1500;
                        tmObtenerInformacion.Start();
                    }
                    else
                    {
                        if (bRepetirProceso)
                        {
                            tmObtenerInformacion.Stop();
                            tmObtenerInformacion.Interval = ((iMinutosObtencion * 60) * 60000) + 1;
                            tmObtenerInformacion.Start();
                        }
                    }
                }
            }

            return bRegresa;
        }

        private void ConfigIntegracion()
        {
            // bool bRegresa = true;
            bIntegracionConfigurada = false;
            string sSql = string.Format("Select * From {0} (NoLock) ", sIntegracion);

            if (!leer.Exec(sSql))
            {
                // bRegresa = true;
                Error.GrabarError(leer, "ConfigIntegracion()");
            }
            else
            {
                General.FechaSistemaObtener();
                if (leer.Leer())
                {
                    bIntegracionConfigurada = true;
                    eTiempoIntegracion = (TiempoIntegracion)leer.CampoInt("TipoTiempo");
                    int iTiempo = leer.CampoInt("Tiempo");

                    tmIntegrarInformacion.Stop();
                    switch (eTiempoIntegracion)
                    {
                        case TiempoIntegracion.Segundos:
                            tmIntegrarInformacion.Interval = (iTiempo * 1000) + 1;
                            break;

                        case TiempoIntegracion.Minutos:
                            tmIntegrarInformacion.Interval = (iTiempo * 60000) + 1;
                            break;

                        case TiempoIntegracion.Horas:
                            tmIntegrarInformacion.Interval = ((iTiempo * 60) * 60000) + 1;
                            break;
                    }
                    tmIntegrarInformacion.Start();
                }
            }
        }

        private void ConfigRespaldosBD()
        {
            // bool bRegresa = true;
            bRespaldoConfigurado = false;
            tmRespaldosBD.Enabled = true;

            string sSql = string.Format("Select * From {0} (NoLock) ", sRespaldoBD);

            if (!leer.Exec(sSql))
            {
                // bRegresa = true;
                Error.GrabarError(leer, "ConfigRespaldosBD()");
            }
            else
            {
                General.FechaSistemaObtener();
                if (leer.Leer())
                {
                    bRespaldoConfigurado = true;
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
                            tmRespaldosBD.Interval = (iTiempo * 60000) + 1;
                            break;

                        case TipoTiempo.Horas:
                            tmRespaldosBD.Interval = ((iTiempo * 60) * 60000) + 1;
                            break;
                    }
                    tmRespaldosBD.Start();
                }
                else
                {
                    // Por Default los respaldos se generan cada hora en caso de no contar con la configuracion 
                    tmRespaldosBD.Interval = (60000 * 60) + 1;
                    tmRespaldosBD.Start();
                }
            }
        }
        #endregion Obtencion de configuraciones

        #region Ejecutar Procesos
        private void Status(bool Estado)
        {
            Transferencia.EjecutandoProcesos = !Estado;
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
                            new DllTransferenciaSoft.ObtenerInformacion.clsOficinaCentral("FarmaciaPtoVta", General.DatosConexion,
                                DtGeneral.IdOficinaCentral, DtGeneral.IdFarmaciaCentral);
                        OficinaCentral.GenerarArchivos(); 
                        OficinaCentral.EnviarArchivos(); 
                    }
                    else if (tpServicio == TipoServicio.Cliente)
                    {
                        DllTransferenciaSoft.ObtenerInformacion.clsCliente Cliente =
                            new DllTransferenciaSoft.ObtenerInformacion.clsCliente("OficinaCentral", General.DatosConexion);
                        Cliente.GenerarArchivos();
                        Cliente.EnviarArchivos();
                    }
                }
                Status(true);
            }
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
                    else if (tpServicio == TipoServicio.Cliente)
                    {
                        DllTransferenciaSoft.IntegrarInformacion.clsCliente Cliente =
                            new DllTransferenciaSoft.IntegrarInformacion.clsCliente(General.DatosConexion);
                        Cliente.Integrar();
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
                if (bIntegracionConfigurada)
                {
                    clsSQL svrSql = new clsSQL(General.DatosConexion, sPrefijoRespaldo, sRutaRespaldos);
                    // svrSql.LogBd.ReducirLog();
                    // svrSql.LogBd.ReducirBD();

                    // Los servidores Regionales y el Central la Base de Datos se reduce manualmente 
                    if (tpServicio == TipoServicio.Cliente)
                    {
                        svrSql.LogBd.ReducirLog();
                        svrSql.LogBd.ReducirBD();
                    }

                    if (svrSql.BackUp.Respaldar())
                    {
                        string sOrigen = sRutaRespaldos + "\\" + svrSql.BackUp.NombreArchivo + ".bak";
                        string sDestino = sRutaRespaldos + "\\" + svrSql.BackUp.NombreArchivo + ".SII";
                        ZipUtil zip = new ZipUtil();
                        zip.Type = Transferencia.Modulo + "Install";
                        zip.Type = Transferencia.Modulo + "BackUp";

                        zip.Comprimir(sOrigen, sDestino, true);
                    }
                }
                Status(true);
            }
        }
        #endregion Ejecutar Procesos

        #region Timers
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

        private void tmObtenerInformacion_Tick(object sender, System.Timers.ElapsedEventArgs e) //object sender, EventArgs e)
        {
            //ThreadObtencion();
            if (bEjecutarObtencion)
            {
                bool bContinuar = true;

                ////if (!bRepetirProceso)
                ////{
                ////    bContinuar = bContinuar;
                ////}

                if (bContinuar)
                {
                    if ((Semaforo == Verde) && validarHoraEjecucion())
                    {
                        Semaforo = Rojo;
                        try
                        {
                            bEjecutarObtencion = false; 
                            this.ObtenerInformacion();
                            // EnviarInformacion();
                        }
                        catch { }
                        finally
                        {
                            ConfigObtencion();
                            Semaforo = Verde;
                            bEjecutarObtencion = true; 
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

        private void tmIntegrarInformacion_Tick(object sender, System.Timers.ElapsedEventArgs e) //object sender, EventArgs e)
        {
            if (bEjecutarIntegracion)
            {
                if (Semaforo == Verde)
                {
                    Semaforo = Rojo;
                    try
                    {
                        bEjecutarIntegracion = false; 
                        this.IntegrarInformacion();
                    }
                    catch { }
                    finally
                    {
                        ConfigIntegracion();
                        Semaforo = Verde; 
                        bEjecutarIntegracion = true; 
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

        private void tmRespaldosBD_Tick(object sender, System.Timers.ElapsedEventArgs e) //object sender, EventArgs e)
        {
            if (bRespaldoConfigurado)
            {
                if (Semaforo == Verde)
                {
                    Semaforo = Rojo;
                    try
                    {
                        bRespaldoConfigurado = false; 
                        GenerarRespaldo();
                    }
                    catch { }
                    finally
                    {
                        ConfigRespaldosBD();
                        Semaforo = Verde;
                        bRespaldoConfigurado = true; 
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
        #endregion Timers 
    }
}
