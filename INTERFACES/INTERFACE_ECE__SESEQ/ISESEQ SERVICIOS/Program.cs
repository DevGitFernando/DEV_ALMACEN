using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Dll_ISESEQ;
using Dll_ISESEQ.wsClases;
using Dll_ISESEQ.InformacionOperacion;


using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace ISESEQ_SERVICIOS
{
    public enum TipoDeProceso
    {
        Ninguno = 0, 
        EnviarRespuestaRecetaElectronica = 1, 
        EnviarInformacionGeneral = 2 
    }
    
    public class Program
    {
        public static Argumentos appArgumentos;
        public static TipoDeProceso tpTipoDeProceso = TipoDeProceso.EnviarRespuestaRecetaElectronica; 
        [STAThread]
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
            General.ArchivoIni = "ISESEQ SERVICIOS";

            DtGeneral.ModuloEnEjecucion = TipoModulo.Ninguno; 
            DtGeneral.SoloMostrarUnidadesConfiguradas = false; 
            GnDll_SII_SESEQ.EsRespuestaWeb = true;


            //clsEdoLogin Login = new clsEdoLogin();
            //clsEnviarCatalogos enviarCatalogos;

            GnDll_SII_SESEQ.MensajePantalla(string.Format("I:{0}", appArgumentos.GetValor("I")));
            GnDll_SII_SESEQ.MensajePantalla(string.Format("P:{0}", appArgumentos.GetValor("P")));

            //// Enviar catalogo de precios 
            if (appArgumentos.GetValor("I") != "")
            {
                bMostrarInterface = !(appArgumentos.GetValor("I") == "N"); 
            }

            //// Verificar el tipo de proceso solicitado 
            if(appArgumentos.GetValor("P") != "")
            {

                try
                {
                    tpTipoDeProceso = (TipoDeProceso)Convert.ToInt32(appArgumentos.GetValor("P"));
                }
                catch 
                {
                    tpTipoDeProceso = TipoDeProceso.EnviarRespuestaRecetaElectronica; 
                }
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

            GnDll_SII_SESEQ.MensajePantalla();
            GnDll_SII_SESEQ.MensajePantalla();
            GnDll_SII_SESEQ.MensajePantalla();
            GnDll_SII_SESEQ.MensajePantalla("Estableciendo conexión con el servidor.");

            if (!Login.AutenticarServicioSO())
            {
                GnDll_SII_SESEQ.MensajePantalla("No fue posible establecer conexión con el servidor de datos.");
            }
            else 
            {
                GnDll_SII_SESEQ.ObtenerURL_Interface("", "", "");
                if (GnDll_SII_SESEQ.URL_Interface == "")
                {
                    GnDll_SII_SESEQ.MensajePantalla("No se encontro configuración para conexión de interface.");
                }
                else
                {
                    if(tpTipoDeProceso == TipoDeProceso.EnviarRespuestaRecetaElectronica || tpTipoDeProceso == TipoDeProceso.Ninguno) 
                    {
                        ResponseAcuseXML responses = new ResponseAcuseXML(General.DatosConexion, "", "", "");

                        responses.GeneralEnviarAcusesReceta(); 
                        responses.GeneralEnviarAcusesColectivo(); 

                        responses.GeneralEnviarAcuses_Devoluciones(TipoProcesoReceta.Receta);
                        responses.GeneralEnviarAcuses_Devoluciones(TipoProcesoReceta.Colectivo);

                        responses.GeneralEnviarAcusesDigitalizacionReceta();
                        responses.GeneralEnviarAcusesDigitalizacionColectivo();
                    }

                    if(tpTipoDeProceso == TipoDeProceso.EnviarInformacionGeneral)
                    {
                        EnviarInformacionOperacion responses = new EnviarInformacionOperacion(General.DatosConexion, "", "", "");
                        responses.EnviarInformacion();
                        //General.msjAviso("Proceso terminado.");
                    }

                    ////responses.GeneralEnviarAcusesColectivo(); 
                    ////responses.EnviarAcusesCancelacionReceta(); 


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
        

            GnDll_SII_SESEQ.MostrarInterface = true;
            Application.Run(new FrmMain()); 
        }
    }
}
