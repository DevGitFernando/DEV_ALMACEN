﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using DllFarmaciaSoft;
using DllCompras.OrdenesDeCompra;

namespace Planeacion
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
            //Application.Run(new Form1());


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            }
        }
    }
}
