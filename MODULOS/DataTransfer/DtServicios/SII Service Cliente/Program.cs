using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SII_Servicio_Cliente.SvrServicio;

////using DllTransferenciaSoft; 
////using DllTransferenciaSoft.Servicio; 

namespace SII_Servicio_Cliente
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

            SelfInstaller.Servicio = "SII Servicio Cliente";
            svrIniciar.IniciarServicio(args);
        }
    }
}
