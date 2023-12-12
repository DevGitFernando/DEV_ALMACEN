using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

using SC_SolutionsSystem;

using DllFarmaciaSoft; 
using DllTransferenciaSoft.Servicio;

using DllTransferenciaSoft; 
using DllTransferenciaSoft.EnviarInformacion;
using DllTransferenciaSoft.ObtenerInformacion;
using DllTransferenciaSoft.IntegrarInformacion;

namespace DtDemonioRC
{
    static class Program
    {
        static string sName = "Servicio Oficina Central";
        static string sSvrName = "svrSII_OficinaCentral";
        static ServiceController svr;
        static ServiceControllerStatus status;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool bExito = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            General.DatosConexion.Servidor = "localhost";
            General.DatosConexion.BaseDeDatos = "FarmaciaScSoft";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "OficinaCentralRI";

            //DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin Login = new DllFarmaciaSoft.Usuarios_y_Permisos.clsEdoLogin("", "");
            //Login.AutenticarServicioSO(); 

            //Servicio x = new Servicio(TipoServicio.OficinaCentral); 

            if (!General.ProcesoEnEjecucion(sName))
            {
                bExito = true;
            }
            ////else
            ////{
            ////    try
            ////    {
            ////        RevisarEstado();
            ////        if (status == ServiceControllerStatus.Running)
            ////        {
            ////            if (General.msjConfirmar("¿ La aplicación esta abierta en modo Servicio, desea abrirla en modo Windows. ?") == DialogResult.Yes)
            ////            {
            ////                General.GetProceso(sName).CloseMainWindow();
            ////                bExito = true; 
            ////                //svr.Stop();
            ////            }
            ////            else
            ////            {
            ////                bExito = false;
            ////            }
            ////        }
            ////    }
            ////    catch { }
            ////}

            if (bExito)
            {
                //////// Punto de Entrada        
                if (ConfiguracionRegional.Revisar())
                {
                    Application.Run(new FrmMain());
                } 
            }
        }

        private static ServiceControllerStatus RevisarEstado()
        {
            try
            {
                svr = new ServiceController(sSvrName);
                svr.Refresh();
                status = svr.Status;
            }
            catch
            {
            }

            return status;
        }
    }
}
