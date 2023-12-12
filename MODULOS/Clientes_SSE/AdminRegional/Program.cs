using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Usuarios_y_Permisos;


using DllPedidosClientes;
using DllPedidosClientes.Reportes;
using DllPedidosClientes.ReportesCentral.Existencias;

namespace AdminRegional
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
            Application.Run(new FrmMain());

            //// LOCAL
            //General.DatosConexion.Servidor = "LocalHost";
            //General.DatosConexion.BaseDeDatos = "SII_Farmacia";
            //General.DatosConexion.Usuario = "sa";
            //General.DatosConexion.Password = "1234";
            //General.Url = @"http://Localhost/myWebFarmacia/wsFarmacia.asmx";
            //DtGeneralPedidos.EstadoConectado = "21";
            //DtGeneralPedidos.EstadoConectadoNombre = "PUEBLA";
            //Application.Run(new FrmExistenciaPorClave());  

            //// SERVIDOR CENTRAL
            //General.DatosConexion.Servidor = "intermed.homeip.net";
            //General.DatosConexion.BaseDeDatos = "SII_OficinaCentral";
            //General.DatosConexion.Usuario = "sa";
            //General.DatosConexion.Password = "E9da87d313c0638839364802f059d2b4";
            //DtGeneralPedidos.EstadoConectado = "21";
            //Application.Run(new FrmCuadrosBasicos());  
            
        }
    }
}
