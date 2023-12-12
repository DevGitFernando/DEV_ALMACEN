using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
using Dll_SII_IMediaccess;
using Dll_SII_IMediaccess.ExportarInformacion;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace MA_Servicios
{
    public class Program
    {
        public static Argumentos appArgumentos; 

        public static void Main(string[] args)
        {
            string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
            string[] argsAux2 = { "iS" };
            appArgumentos = new Argumentos(args); 


            clsEdoLogin Login = new clsEdoLogin();
            clsEnviarCatalogos enviarCatalogos;

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "MA Oficina Central Servicios";

            basGenerales Fg = new basGenerales(); 
            string sServidor = ""; 
            string sBaseDeDatos = ""; 
            //General.ArchivoIni = "MA Farmacia";

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Estableciendo conexión con el servidor.");


            //// Enviar catalogo de precios 
            if (appArgumentos.GetValor("C") != "")
            {
                GnDll_SII_IMediaccess.Enviar_Catalogo_Productos = appArgumentos.GetValor("C") == "S";
            }

            //// Enviar catalogo de precios 
            if (appArgumentos.GetValor("P") == "S")
            {
                GnDll_SII_IMediaccess.Enviar_Catalogo_Precios = true;
            }


            Login.XmlEnDirectorioApp = true; 
            if (Login.AutenticarServicioSO())
            {
                enviarCatalogos = new clsEnviarCatalogos();
                enviarCatalogos.Enviar();
            }

            //////if (args.Length >= 2)
            //////{
            //////    sServidor = Fg.Mid(args[0], 2);
            //////    sBaseDeDatos = Fg.Mid(args[1], 2);

            //////    General.DatosConexion.Servidor = sServidor;
            //////    General.DatosConexion.BaseDeDatos = sBaseDeDatos;
            //////    General.DatosConexion.ConexionDeConfianza = true;

            //////    enviarCatalogos = new clsEnviarCatalogos();
            //////    enviarCatalogos.Enviar(); 
            //////}
        }
    }
}
