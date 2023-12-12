using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using Configuracion.Configuracion;
using Configuracion.ConfigurarPadron; 

using DllFarmaciaSoft.Web;

namespace Configuracion
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
            General.DatosConexion.BaseDeDatos = "SII_PtoVta_EnBlanco";
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";

            ////string sURL = "http://sc-sin-soporte.homeip.net/wsInt-OficinaCentral/wsOficinaCentral.asmx";

            ////FrmExecWebService exec = new FrmExecWebService(sURL, "OficinaCentral");

            ////exec.ShowInTaskbar = true; 
            ////exec.ShowDialog();

            //////// Punto de Entrada        
            if (ConfiguracionRegional.Revisar())
            {
                Application.Run(new FrmMain());
            } 

//////            FrmPadronBeneficiarios f = new FrmPadronBeneficiarios();
//////            f.ShowInTaskbar = true;
//////            f.ShowDialog();
////////            Application.Run(new FrmPadronBeneficiarios());

        }
    }
}
