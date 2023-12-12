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

namespace SII_Services_DB.SvrServicio
{
    [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
    partial class SII_Services_DB : ServiceBase
    {
        // string sFileList = ""; 

        // String. (Environment.SystemDirectory.ToString(), 1).Trim(); 
        string sUnidadSO = Environment.SystemDirectory.ToString().Substring(0, 1) + @":\\";

        string sFileLog = "";
        string sError = "";
        // string sFileTest = @"D:\PROYECTO SC-SOFT\Archivos_Generados\Servicio Integrador BD.exe";

        string sFileConfig = ""; 
        string sWinRar = "";
        string sTiposArchivos = ""; 
        string sDirBackUp = "";
        string sDirBackUp_Alterno = ""; 
        string sPK01 ="";
        string sPK02 = "";
        string sTime = "";
        int iTime = 10;
        bool bProcesando = false;

        ArrayList pFiles = new ArrayList(); 
        ArrayList pTipos_Files = new ArrayList(); 
        ArrayList pDirBackup_Alterno = new ArrayList(); 


        System.Timers.Timer myTimer; 
        //EventLog EventLog

        public SII_Services_DB() 
        {
            InitializeComponent();

            this.CanPauseAndContinue = true; 
            this.CanShutdown = true; 
            this.CanStop = true;

            bProcesando = false; 
            IniciarServicio(); 
        }

        #region Metodos principales 
        private void MostrarDatos()
        {
            string sMsj = "";

            sMsj = string.Format("WinRar={0}\n", sWinRar);
            sMsj += string.Format("DirBackUp={0}\n", sDirBackUp);
            sMsj += string.Format("DirBackUp_Alterno={0}\n", sDirBackUp); 
            sMsj += string.Format("PK01={0}\n", sPK01);
            sMsj += string.Format("PK02={0}\n", sPK02);
            sMsj += string.Format("TM={0}", sTime);             
            

            MessageBox.Show(sMsj); 
        }

        private void IniciarServicio()
        {
            sFileLog = Path.Combine(Environment.CurrentDirectory.ToString(), "LogSII-Services_DB.txt");
            sFileLog = Path.Combine(sUnidadSO, "LogSII-Services_DB.txt"); 
            sFileConfig = Path.Combine(sUnidadSO, "Services_DB_Cfg.txt");
            sFileConfig = Path.Combine(Application.StartupPath, "Services_DB_Cfg.txt");

            GetConfiguracion(); 
            if (Program.Depurando)
            {
                MostrarDatos(); 
            }

            ////////myTimer = new System.Timers.Timer(); 
            ////////myTimer.Interval = (1000 * 60)* iTime; 
            ////////myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            ////////myTimer.Start();

            // myTimer_Elapsed(null, null); 

            // myTimer_Elapsed(null, null); 
        }

        private void Configurar_Timer()
        {
            myTimer = new System.Timers.Timer();
            myTimer.Interval = (1000 * 60) * iTime;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Start(); 
        }

        private void GetConfiguracion()
        {
            // Preparar el archivo de Configuracion  
            GetFileConfiguracion();  
            ReadConfiguracion();  
            Configurar_Timer(); 


            if (sTiposArchivos != "")
            {
                Obtener_Tipos_Archivos();
            }

            if (sDirBackUp_Alterno != "")
            {
                Obtener_Directorios_De_Respaldo();
            }

        }

        private void ReadConfiguracion()
        {
            string sFile = "";
            int EqualPosition = 0;
            string myKey = ""; 

            using (StreamReader sr = new StreamReader(sFileConfig))
            {
                while (sr.Peek() >= 0)
                {
                    sFile = sr.ReadLine();
                    if (sFile.Length != 0)
                    {
                        EqualPosition = 0; 
                        if (sFile.Substring(0, 1) != "#")
                        {
                            EqualPosition = sFile.IndexOf("=", 0);
                        }

                        if (EqualPosition > 0)
                        {
                            myKey = sFile.Substring(0, EqualPosition).ToUpper();
                            if (myKey == "WinRar".ToUpper())
                                sWinRar = sFile.Substring(EqualPosition + 1).Trim();

                            if (myKey == "TiposArchivos".ToUpper())
                                sTiposArchivos = sFile.Substring(EqualPosition + 1).Trim(); 

                            if (myKey == "DirBackUp".ToUpper()) 
                                sDirBackUp = sFile.Substring(EqualPosition + 1).Trim();

                            if (myKey == "DirBackUp_Alterno".ToUpper())
                                sDirBackUp_Alterno = sFile.Substring(EqualPosition + 1).Trim(); 

                            if (myKey == "PK01".ToUpper())
                                sPK01 = sFile.Substring(EqualPosition + 1).Trim();

                            if (myKey == "PK02".ToUpper())
                                sPK02 = sFile.Substring(EqualPosition + 1).Trim();

                            if (myKey == "TM".ToUpper())
                            {
                                sTime = sFile.Substring(EqualPosition + 1).Trim();

                                try
                                {
                                    iTime = Convert.ToInt32(sTime); 
                                }
                                catch { iTime = 10; }
                            }

                        }
                    }
                }
            } 
        }
        
        private void Obtener_Tipos_Archivos()
        {
            string[] files = sTiposArchivos.Split(';');

            pTipos_Files.Clear();
            foreach (string sFile in files)
            {
                pTipos_Files.Add(sFile); 
            }
        }


        private void Obtener_Directorios_De_Respaldo()
        {
            string[] directorios = sDirBackUp_Alterno.Split(';');

            pDirBackup_Alterno.Clear(); 
            foreach (string sDir in directorios)
            {
                pDirBackup_Alterno.Add(sDir);
                try
                {
                    Directory.CreateDirectory(sDir); 
                }
                catch { }
            }
        }

        private void GetFileConfiguracion()
        {
            if (!File.Exists(sFileConfig))
            {
                StreamWriter f = new StreamWriter(sFileConfig);

                //////f.WriteLine("WinRar=");
                //////f.WriteLine("TiposArchivos=bak;sii"); 
                //////f.WriteLine("DirBackUp="); 
                //////f.WriteLine("DirBackUp_Alterno="); 
                //////f.WriteLine("PK01=");
                //////f.WriteLine("PK02=");
                //////f.WriteLine("TM="); 

                f.Write(Properties.Resources.Services_DB_Cfg); 

                f.Close();
                f = null;  
            }
        }

        protected override void OnStart(string[] args)
        {
            IniciarServicio();
            Log("Iniciando SII Services DB"); 
        }

        protected override void OnStop()
        {
            Log("Deteniendo SII Services DB"); 
            myTimer.Stop(); 
        }

        protected override void OnContinue()
        {
            IniciarServicio();
            Log("Reanudando SII Services DB"); 
        }

        protected override void OnPause()
        {
            Log("Pausando SII Services DB"); 
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
        #endregion Funciones 

        #region Procesos 
        public void Procesar()
        {
            myTimer_Elapsed(null, null); 
        }

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!bProcesando)
            {
                bProcesando = true;
                myTimer.Enabled = false;
                myTimer.Stop(); 

                // AbrirArchivo(sFileTest);  
                Procesar_Bases_De_Datos();
                GetConfiguracion(); 

                myTimer.Enabled = true;
                myTimer.Start();
                bProcesando = false;
            }
        }

        /// <summary>
        /// Iniciar todos los Servicios de Base de Datos y Agente Sql instalados
        /// </summary>
        private void Procesar_Bases_De_Datos()
        {
            string[] filesDb = Directory.GetFiles(sDirBackUp, "*.bak");
            string sMsj = "";

            string myFile = ""; 
            Process rar = new Process();
            string sParametrosBase = " a -m5 -df -p33e790cd8a7fad434930d99b67ba8efd -ep ";
            string sParametrosVariables = "";
            string sRar_copy = "";
            string sRuta_Destino = "";
            string sFileCompres = ""; 

            foreach (string sTipo in pTipos_Files)
            {
                string[] filesProcesar = Directory.GetFiles(sDirBackUp, string.Format("*.{0}", sTipo));
                foreach (string sDir in filesProcesar)
                {
                    pFiles.Add(sDir); 
                }
            }

            foreach (string sFile in pFiles) 
            {
                FileInfo f = new FileInfo(sFile); 

                sMsj += string.Format("{0}\n", sFile);
                myFile = f.Name.Replace(f.Extension, ""); 
                sRar_copy = string.Format("{0}.rar", f.Name.Replace(f.Extension, ""));
                sParametrosVariables = string.Format("{0}.rar {1}", Path.Combine(sDirBackUp, myFile), Path.Combine(sDirBackUp, f.Name));

                sFileCompres = f.FullName; 
                f = null; 

                rar = new Process();
                rar.StartInfo.FileName = sWinRar; 
                rar.StartInfo.Arguments = sParametrosBase + " " + sParametrosVariables;
                rar.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                Log("Comprimiendo : " + sFileCompres); 
                rar.Start();
                rar.WaitForExit(); 

                ///// Jesús Díaz  2K120703.1305 
                foreach (string sDir_Aux in pDirBackup_Alterno)
                {
                    try
                    {
                        sRuta_Destino = Path.Combine(sDir_Aux, sRar_copy);
                        File.Copy(Path.Combine(sDirBackUp, sRar_copy), sRuta_Destino, true);
                        Log("Copiando a ==> " + sRuta_Destino); 
                    }
                    catch (Exception ex)
                    {
                        Log("Error : " + ex.Message);
                    }
                }

                Log(""); 
            }

            // MessageBox.Show(sMsj); 
        }
        #endregion Procesos 
    }
}
