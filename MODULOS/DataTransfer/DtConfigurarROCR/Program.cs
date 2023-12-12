using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllTransferenciaSoft.IntegrarBD;

using SC_SolutionsSystem; 

namespace DtConfigurarROCR 
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


            General.DatosConexion.Servidor = "LCOALHOST";
            General.DatosConexion.BaseDeDatos = "SII_21_Regional";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234"; 

            //DtGeneral.ConexionOficinaCentral = true;
            //DtGeneral.ConexionOficinaCentral = false;

            ////Application.Run(new FrmMttoFTP()); 

            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 

        }
    }
}
