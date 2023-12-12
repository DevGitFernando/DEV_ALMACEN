using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using SC_SolutionsSystem;

using Inventarios.DistribucionExcedentes; 

namespace Inventarios
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

            DtGeneral.EmpresaConectada = "1";
            DtGeneral.EstadoConectado = "21";
            DtGeneral.FarmaciaConectada = "224";


            General.DatosConexion.Servidor = "localhost";
            General.DatosConexion.BaseDeDatos = "SII_21_0224"; // "SII_OficinaCentral_Test";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";
            General.DatosConexion.ConexionDeConfianza = false;
            General.Url = "http://localhost/wsFarmacia/wsFarmacia.asmx"; 


            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain()); 

                ////FrmDistribucionExcedentes f = new FrmDistribucionExcedentes();
                ////f.ShowInTaskbar = true; 

                ////Application.Run(f); 
            } 
        }
    }
}
