using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem.QRCode;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data; 

namespace SC_QRCode_Test
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            General.DatosConexion = new clsDatosConexion();
            General.DatosConexion.Servidor = "intermedpuebla.homeip.net:8090";

            Application.Run(new QrCodeSampleApp());
        }
    }
}
