using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem.GAC;
using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.SQL;
using SC_SolutionsSystem.Criptografia;

using Dll_IGPI.Interface;
using Dll_IGPI.Protocolos;

////using Microsoft.VisualBasic;
////using Microsoft.VisualBasic.FileIO;

namespace Dll_IGPI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            basGenerales Fg = new basGenerales();

            General.DatosConexion.Servidor = "sc-sin-soporte.homeip.net";
            General.DatosConexion.BaseDeDatos = "SII_OficinaCentral"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "18cff04136a0d4847b20aaa9ff12043e";


            General.DatosConexion.Servidor = "culiacan1";
            General.DatosConexion.BaseDeDatos = "SII_HospitalGeneral"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "da09cf0ac68b1b1ca71dc972bf2c4d77";

            General.DatosConexion.Servidor = "lapJesus";
            General.DatosConexion.BaseDeDatos = "SII_Pedidos"; // SII_OficinaCentral 
            General.DatosConexion.Usuario = "sa";
            General.DatosConexion.Password = "1234";

            ////string sMsj = "a0000000800x00y0210c000000000000000000ck00109999999999";
            ////// clsI_A_Response a = new clsI_A_Response(sMsj); 
            ////////clsI_I_Request r = new clsI_I_Request(sMsj_I); 


            string[] myArgs = { "S0001" };
            // args = myArgs; 


            ArgumentosDeInicio.ObtenerParametros(args);
            Argumento argSolicitud = ArgumentosDeInicio.GetArgumento("s");

            // Asegurar que la Interface solamente se ejecute una vez en el equipo. 
            if (General.ProcesoEnEjecucion("IGPI.exe"))
            {
                General.msjAviso("La Interface de Comunicación IGPI ya se encuentra en ejecución.");
            }
            else
            {
                if (argSolicitud.EsValido)
                {
                    FrmDetalleCodigoEAN Detalles = new FrmDetalleCodigoEAN();

                    Detalles.ShowInTaskbar = false;
                    Detalles.TopMost = true;
                    Detalles.MostrarPantalla("", "");
                }
                else
                {
                    Application.Run(new FrmMain());
                    General.TerminarProceso("IGPI.exe");
                }
            }
            // Application.Run(new FrmChat()); 

            // General.DatosConexion.Servidor = "culiacan1";
        }
    }
}
