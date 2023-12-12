using System;
using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Timers;

using Microsoft.VisualBasic; 

namespace SII_Services.SvrServicio
{
    [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
    partial class SII_Services : ServiceBase
    {
        string sFileList = "";
        ArrayList pFiles = new ArrayList(); 

        // String. (Environment.SystemDirectory.ToString(), 1).Trim(); 
        string sUnidadSO = Environment.SystemDirectory.ToString().Substring(0, 1) + @":\\";

        string sFileLog = "";
        string sError = "";
        // string sFileTest = @"D:\PROYECTO SC-SOFT\Archivos_Generados\Servicio Integrador BD.exe"; 


        System.Timers.Timer myTimer; 
        //EventLog EventLog

        public SII_Services() 
        {
            InitializeComponent();

            this.CanPauseAndContinue = true; 
            this.CanShutdown = true; 
            this.CanStop = true; 
        }

        #region Metodos principales 
        private void IniciarServicio()
        {
            sFileLog = Path.Combine(Environment.CurrentDirectory.ToString(), "LogSII-Services.txt");
            sFileLog = Path.Combine(sUnidadSO, "LogSII-Services.txt");
            GetFiles();              

            myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000 * 10;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Start();

            // myTimer_Elapsed(null, null); 
        }

        private void GetFiles()
        {
            string sFile = ""; 

            sFileList = Path.Combine(sUnidadSO, "SII-Services.ini");
            pFiles = new ArrayList();

            if (File.Exists(sFileList))
            {
                using (StreamReader sr = new StreamReader(sFileList))
                {
                    while (sr.Peek() >= 0)
                    {
                        sFile = sr.ReadLine();
                        if (File.Exists(sFile))
                        {
                            pFiles.Add(sFile);
                        }
                    }
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            IniciarServicio();
            Log("Iniciando SII Services"); 
        }

        protected override void OnStop()
        {
            Log("Deteniendo SII Services"); 
            myTimer.Stop(); 
        }

        protected override void OnContinue()
        {
            IniciarServicio();
            Log("Reanudando SII Services"); 
        }

        protected override void OnPause()
        {
            Log("Pausando SII Services"); 
            myTimer.Stop(); 
        }
        #endregion Metodos principales

        #region Funciones
        private bool ProcesoEnEjecucion()
        {
            return ProcesoEnEjecucion(Process.GetCurrentProcess().ProcessName);
        }

        private bool ProcesoEnEjecucion(string NombreProceso)
        {
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length >= 1; 
            return bRegresa;
        }

        private void Log(string Cadena)
        {
            try
            {
                StreamWriter f = new StreamWriter(sFileLog, true);

                DateTime dt = DateTime.Now; 
                string sMarcaDeTiempo = "";

                sMarcaDeTiempo += dt.Year.ToString("0000");
                sMarcaDeTiempo += dt.Month.ToString("00");
                sMarcaDeTiempo += dt.Day.ToString("00");
                sMarcaDeTiempo += "_";
                sMarcaDeTiempo += dt.Hour.ToString("00");
                sMarcaDeTiempo += dt.Minute.ToString("00");
                sMarcaDeTiempo += dt.Second.ToString("00");


                f.WriteLine(string.Format("{0}:   {1}", sMarcaDeTiempo, Cadena));
                f.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            } 
        }

        private bool AbrirArchivo(string Archivo)
        {
            bool bRegresa = false;

            try
            {
                if (File.Exists(Archivo))
                {
                    FileInfo f = new FileInfo(Archivo);

                    if (!ProcesoEnEjecucion(f.Name))
                    {
                        // basGenerales Fg = new basGenerales(); 
                        string sFile = Strings.Chr(34) + Archivo + Strings.Chr(34);
                        Process procesoFile = new Process();

                        procesoFile.StartInfo.FileName = sFile;
                        procesoFile.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procesoFile.WaitForExit();
                        procesoFile.Start();
                    }
                }

                bRegresa = true;
            }
            catch
            {
            }

            return bRegresa;
        } 
        #endregion Funciones 

        #region Procesos 
        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            myTimer.Enabled = false;
            myTimer.Stop(); 

            // AbrirArchivo(sFileTest); 
            Servicios(); 

            myTimer.Enabled = true;
            myTimer.Start(); 
        }

        /// <summary>
        /// Iniciar todos los Servicios de Base de Datos y Agente Sql instalados
        /// </summary>
        private void Servicios()
        {
            ServiceController myService = new ServiceController();
            ServiceController[] myServices;// = new ServiceController();


            myServices = ServiceController.GetServices(); 
            foreach (ServiceController servicio in myServices)
            {
                if (EsServicioSQL(servicio.ServiceName))
                {
                    IniciarServicio(servicio); 
                } 
            }

            foreach (ServiceController servicio in myServices)
            {
                if (EsServicioSQL_Agente(servicio.ServiceName))
                {
                    IniciarServicio(servicio); 
                }
            } 
        }

        private void IniciarServicio(ServiceController Servicio)
        {
            try
            {
                ServiceController myService = new ServiceController(Servicio.ServiceName);
                myService.MachineName = Servicio.MachineName;

                if (myService.Status == ServiceControllerStatus.Stopped)
                {
                    myService.Start();
                    Log(string.Format("Iniciando    {0} ", myService.ServiceName)); 
                }

                myService.WaitForStatus(ServiceControllerStatus.Running);
                myService.Close(); 

            }
            catch (Exception ex) 
            {
                sError = ex.Message;
                Log(string.Format("Error al iniciar    {0} ", Servicio.ServiceName));
                Log(sError); 
            }
        }

        private bool EsServicioSQL(string Servicio)
        {
            bool bRegresa = false;
            // int EqualPosition = 0;
            string sSrvCmp = Servicio.Trim().ToUpper();

            // Buscar Instancia Predeterminada 
            if (sSrvCmp.Contains("MSSQLSERVER"))
            {
                bRegresa = sSrvCmp == "MSSQLSERVER" ? true : false; 
            }

            // Buscar Instancias adicionales 
            if (sSrvCmp.Contains("MSSQL$"))
            {
                // bRegresa = sSrvCmp == "MSSQL$" ? true : false;
                bRegresa = true; 
            }

            return bRegresa;
        }

        private bool EsServicioSQL_Agente(string Servicio)
        {
            bool bRegresa = false;
            // int EqualPosition = 0;
            string sSrvCmp = Servicio.Trim().ToUpper();

            // Buscar Instancia Predeterminada 
            if (sSrvCmp.Contains("SQLSERVERAGENT"))
            {
                bRegresa = sSrvCmp == "SQLSERVERAGENT" ? true : false;
            }

            // Buscar Instancias adicionales 
            if (sSrvCmp.Contains("SQLAGENT$"))
            {
                //bRegresa = sSrvCmp == "SQLAgent$" ? true : false;
                bRegresa = true; 
            }


            return bRegresa;
        }
        #endregion Procesos 
    }
}
