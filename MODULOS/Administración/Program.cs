using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem.GAC;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.Criptografia;

using DllFarmaciaSoft; 
using DllFarmaciaSoft.Procesos;

namespace Administracion
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

            General.DatosConexion.Servidor = "localhost";
            General.DatosConexion.BaseDeDatos = "SII_OficinaCentral"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";

            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 

        }
    }
}
