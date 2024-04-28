using System;
using System.Collections.Generic;
using System.Windows.Forms;


using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft; 
using DllCompras.OrdenesDeCompra; 

namespace Compras
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


            //General.DatosConexion.Servidor = "localhost";
            //General.DatosConexion.BaseDeDatos = "SII_Regional_Oaxaca";
            //General.DatosConexion.Usuario = "sa";
            //General.DatosConexion.Password = "1234"; 


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 


            ////FrmCfgNotasOrdenCompra f = new FrmCfgNotasOrdenCompra();
            ////f.ShowInTaskbar = true;
            ////f.ShowDialog(); 

        }
    }
}
