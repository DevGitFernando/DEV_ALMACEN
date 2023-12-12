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
using System.Net;
using System.Net.NetworkInformation;

using Microsoft.VisualBasic;
using Microsoft.Win32; 

using SII_Servicio_Cliente.wsConexion; 

namespace SII_Servicio_Cliente.SvrServicio
{
    [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
    partial class SII_Servicio_Cliente : ServiceBase
    {
        string sFileList = "";
        ArrayList pFiles = new ArrayList(); 

        // String. (Environment.SystemDirectory.ToString(), 1).Trim(); 
        string sUnidadSO = Environment.SystemDirectory.ToString().Substring(0, 1) + @":\\";

        string sFileXml = "";
        string sError = "";
        // string sFileTest = @"D:\PROYECTO SC-SOFT\Archivos_Generados\Servicio Integrador BD.exe"; 


        System.Timers.Timer myTimer; 
        //EventLog EventLog

        DataSet dtsConexion; 
        string sServidor = "";
        string sWebService = "";
        string sDireccion = "";
        string sPaginaASMX = "";

        string sMACS = ""; 

        bool bEsServidorLocal = false; 
        
        wsCnnCliente web; 


        public SII_Servicio_Cliente() 
        {
            InitializeComponent();

            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true; 

            RegistryKey ckey = Registry.LocalMachine.OpenSubKey(string.Format(@"SYSTEM\CurrentControlSet\Services\{0}", "SII Servicio Cliente"), true);
            if (ckey != null)
            {
                if (ckey.GetValue("Type") != null)
                {
                    ckey.SetValue("Type", 272);
                }
            } 
        }

        #region Metodos principales 
        private void IniciarServicio()
        {
            sFileXml = sUnidadSO + "FarmaciaPtoVta.xml";

            ConexionesRed();
            SetConexion(); 

            myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000 * 5;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Start();

            // myTimer_Elapsed(null, null); 
        }

        protected override void OnStart(string[] args)
        {
            IniciarServicio();
        }

        protected override void OnStop()
        {
            myTimer.Stop(); 
        }

        protected override void OnContinue()
        {
            IniciarServicio();
        }

        protected override void OnPause()
        {
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
        #endregion Funciones 

        #region Procesos 
        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            myTimer.Enabled = false;
            myTimer.Stop();

            if (bEsServidorLocal)
            {
                Revisar_ServicioCliente(); 
            }

            myTimer.Enabled = true;
            myTimer.Start(); 
        }

        private void Revisar_ServicioCliente()
        {
            string sModuloTransferencia = "Servicio Cliente.exe";
            sModuloTransferencia = "ConfiguracionCs"; 

            if (!ProcesoEnEjecucionUnica(sModuloTransferencia))
            {
                Process svr = new Process();
                svr.StartInfo.FileName = Application.StartupPath + @"\\" + sModuloTransferencia;
                svr.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                svr.Start();
                System.Threading.Thread.Sleep(500);
            }
        }

        private string FormatearNombreProceso(string NombreProceso)
        {
            NombreProceso = NombreProceso.Replace(".EXE", "");
            NombreProceso = NombreProceso.Replace(".exe", "");
            NombreProceso = NombreProceso.Replace(".DLL", "");
            NombreProceso = NombreProceso.Replace(".dll", "");
            return NombreProceso;
        }

        private bool ProcesoEnEjecucionUnica(string NombreProceso)
        {
            NombreProceso = FormatearNombreProceso(NombreProceso);
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length == 1;

            return bRegresa;
        }
        #endregion Procesos 

        #region Conexion 
        private void SetConexion()
        {
            try
            {
                dtsConexion = new DataSet(); 
                dtsConexion.ReadXml(sFileXml);

                DataRow dt = dtsConexion.Tables[0].Rows[0];

                sServidor = dt["Servidor"].ToString();
                sWebService = dt["WebService"].ToString();
                sPaginaASMX = dt["PaginaASMX"].ToString();

                sDireccion = "http://" + sServidor + "/" + sWebService + "/" + sPaginaASMX + ".asmx";
                sDireccion = sDireccion.Replace(@"\", @"/"); 
                sDireccion = sDireccion.Replace("\\", @"/"); 


                VerificarEsServidor(); 
            }
            catch (Exception ex)
            {
            }
        }

        private void VerificarEsServidor()
        {
            string sSql = string.Format(" Select 1 as EsServidor " +
                " From CFGC_Terminales (NoLock) " +
                " Where replace(MAC_Address, '-', '') in ( {0} ) and EsServidor = 1 ", sMACS);

            bEsServidorLocal = false; 
            try
            {
                web = new wsCnnCliente();
                web.Url = sDireccion;

                dtsConexion = web.ExecuteExt(new DataSet(), "FarmaciaPtoVta", sSql);

                if (dtsConexion.Tables.Count > 0)
                {
                    if (dtsConexion.Tables[0].Rows.Count > 0)
                    {
                        if (dtsConexion.Tables[0].Rows[0]["EsServidor"].ToString() == "1")
                        {
                            bEsServidorLocal = true; 
                        }
                    }
                }
            }
            catch
            { 
            }
        }

        private void ConexionesRed()
        {
            /// Nombre del host
            string sNombreHost = Dns.GetHostName();
            sMACS = ""; 

            try
            {

                //// Mac del host
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();  

                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    PhysicalAddress adaptador = nic.GetPhysicalAddress();

                    sMACS += string.Format("'{0}', ", adaptador.ToString()); 
                }

                sMACS = sMACS.Trim() + "'.'";

            }
            catch
            {
                sMACS = "";
            }
        }
        #endregion Conexion 
    }
}
