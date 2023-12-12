using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading; 

using SC_SolutionsSystem;

namespace DtDemonioRFC
{
    static class Program
    {
        static string sName = "Servicio Cliente";
        static string sSvrName = "svrSII_Cliente";
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

            ////if (!General.ProcesoEnEjecucion(sName))
            ////{
            ////    bExito = true;
            ////}

            string mutexName = String.Format("Global\\{{{0}}}", sName);
            Mutex mutex = new Mutex(true, mutexName, out bExito); 

            if (bExito)
            {
                Application.Run(new FrmMain());
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
