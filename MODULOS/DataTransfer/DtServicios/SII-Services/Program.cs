using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SII_Services.SvrServicio;

////using DllTransferenciaSoft; 
////using DllTransferenciaSoft.Servicio; 

namespace SII_Services
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sFileLog = "";
            sFileLog = Environment.CurrentDirectory.ToString();

            SelfInstaller.Servicio = "SII Services";
            svrIniciar.IniciarServicio(args);
        }
    }
}
