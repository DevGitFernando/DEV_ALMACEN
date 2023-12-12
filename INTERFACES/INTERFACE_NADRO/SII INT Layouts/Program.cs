using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SII_INT_Layouts.Procesar_Archivos; 

namespace SII_INT_Layouts
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
            Application.Run(new FrmConvertir_TXT_To_XLS());
        }
    }
}
