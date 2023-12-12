using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Dll_ISIADISSEP;
using Dll_ISIADISSEP.wsClases; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace ISIADISSEP_SERVICIOS
{
    public class Program
    {
        public static Argumentos appArgumentos; 

        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);



            string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
            string[] argsAux2 = { "iS" };
            bool bMostrarInterface = true; 
            appArgumentos = new Argumentos(args);


            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "ISIADISSEP SERVICIOS";

            GnDll_SII_SIADISSEP.EsRespuestaWeb = false;


            //clsEdoLogin Login = new clsEdoLogin();
            //clsEnviarCatalogos enviarCatalogos;

            //// Enviar catalogo de precios 
            if (appArgumentos.GetValor("I") != "")
            {
                bMostrarInterface = !(appArgumentos.GetValor("I") == "N"); 
            }


            if (bMostrarInterface)
            {
                //General.msjAviso("Interface"); 
                MostrarInterface(); 
            }
            else 
            {
                //General.msjAviso("Sin interface"); 
                IniciarSinInterface(); 
            }

        }

        private static void IniciarSinInterface()
        {
            clsEdoLogin Login = new clsEdoLogin();
            Login.XmlEnDirectorioApp = true;

            GnDll_SII_SIADISSEP.MensajePantalla();
            GnDll_SII_SIADISSEP.MensajePantalla();
            GnDll_SII_SIADISSEP.MensajePantalla();
            GnDll_SII_SIADISSEP.MensajePantalla("Estableciendo conexión con el servidor.");

            if (!Login.AutenticarServicioSO())
            {
                GnDll_SII_SIADISSEP.MensajePantalla("No fue posible establecer conexión con el servidor de datos.");
            }
            else 
            {
                GnDll_SII_SIADISSEP.ObtenerURL_Interface("", "", "");
                if (GnDll_SII_SIADISSEP.URL_Interface == "")
                {
                    GnDll_SII_SIADISSEP.MensajePantalla("No se encontro configuración para conexión de interface.");
                }
                else
                {
                    ResponseAcuseXML responses = new ResponseAcuseXML(General.DatosConexion, "", "", "");
                    responses.GeneralEnviarAcusesReceta();
                    responses.GeneralEnviarAcusesColectivo(); 
                    responses.EnviarAcusesCancelacionReceta(); 


                }

                //enviarCatalogos = new clsEnviarCatalogos();
                //enviarCatalogos.Enviar();
            }
        }

        private static void MostrarInterface()
        {
            Application.EnableVisualStyles();

            ////try
            ////{

            ////}
            ////catch (Exception ex) 
            ////{
            ////    //General.msjError(ex.Message); 
            ////}

            ////try 
            ////{
            ////    Application.SetCompatibleTextRenderingDefault(false);
            ////}
            ////catch (Exception ex)
            ////{
            ////    General.msjError(ex.Message); 
            ////}
        

            GnDll_SII_SIADISSEP.MostrarInterface = true;
            Application.Run(new FrmMain()); 
        }
    }
}
