using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Windows.Forms;

using SII_Services_DB.SvrServicio; 

////using DllTransferenciaSoft; 
////using DllTransferenciaSoft.Servicio; 

namespace SII_Services_DB
{
    static class Program
    {
        public static bool Depurando = false; 

        /// <summary>
        /// Punto de entrada principal para la aplicación. 
        /// </summary>
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ////////////SII_Services_DB.SvrServicio.SII_Services_DB x = new SII_Services_DB.SvrServicio.SII_Services_DB();
            ////////////x.Procesar(); 


            string sFileLog = "";
            sFileLog = Environment.CurrentDirectory.ToString();

            SelfInstaller.Servicio = "SII Services DB";
            svrIniciar.IniciarServicio(args);  


        }
    }
}
