using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft; 

namespace AuditorFarmacia
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

            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 

        }
    }
}
