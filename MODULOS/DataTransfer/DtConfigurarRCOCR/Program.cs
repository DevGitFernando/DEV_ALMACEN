using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem;
using DllFarmaciaSoft; 


namespace DtConfigurarRCOCR
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

            //General.ProcesoEnEjecucion("Servicio Cliente");


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 


        }
    }
}
