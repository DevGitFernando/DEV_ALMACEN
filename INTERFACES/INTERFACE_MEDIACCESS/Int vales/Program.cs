using System;
using System.Collections.Generic;
using System.Text;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;
//using Dll_INT_EPharma;
//using Dll_SII_IMediaccess.ValesRecepcion;

//using DllTransferenciaSoft.ReplicacionSQL;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace INT_Vales
{
    class Program
    {
        public static Argumentos appArgumentos;

        internal class Parametros
        {
            public Parametros(string P01, string P02, string P03)
            {
                NombrePametro = P01;
                Parametro = P02;
                Valor = P03;
            }

            public string NombrePametro = "";
            public string Parametro = "";
            public string Valor = "";
        }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
                      List<Parametros> listaParametros = new List<Parametros>(); 
            string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
            string[] argsAux2 = { "iS" };
            appArgumentos = new Argumentos(args); 


            clsEdoLogin Login = new clsEdoLogin();
            //clsReplicacionSQL replicacion = null; //// = new clsReplicacionSQL();
            //Dll_INT_EPharma.wsEPharmaInformacionDeVales.ValesRecepcionRegistrar valesReg = null;
            
            string sMsj = "";

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "CFG Replicación Vales";

            basGenerales Fg = new basGenerales();
            int iDiasRevision = 10;
            bool bDetener = false;
            bool bRevisionPorFechas = false;
            bool bRevisionPorFechaServidor = false;
            string sFechaInicial = "";
            string sFechaFinal = "";
            int iTipoDeProceso = 1;
            int iTipoDeEnvio = 1;
            bool bEstadoEspecifico = false;
            string sIdEstado = ""; 

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Estableciendo conexión con el servidor.");


            Login.XmlEnDirectorioApp = true;
            if (!Login.AutenticarServicioSO())
            {
                System.Console.WriteLine("No fue posible establecer conexión con el servidor.");
                System.Console.WriteLine("Presione una tecla para continuar.");
                System.Console.ReadKey(); 
            }
            else 
            {
                sMsj = "";

                Parametros param = new Parametros("", "", "");

                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine(General.DatosConexion.CadenaConexion);

                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("-------------------------------------PARAMETROS DE ENTRADA");
                System.Console.WriteLine("");

                if (listaParametros.Count == 0)
                {
                    System.Console.WriteLine("No se recibieron parámetros de entrada.");
                }
                else
                {
                    sMsj = "";
                    foreach (Parametros p in listaParametros)
                    {
                        sMsj = string.Format("{0}\t\t{1}: {2}", p.NombrePametro, p.Parametro, p.Valor);
                        System.Console.WriteLine(sMsj);
                    }
                }

                System.Console.WriteLine("");
                System.Console.WriteLine("-------------------------------------PARAMETROS DE ENTRADA");
                System.Console.WriteLine("");
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                //replicacion.SoloEstadoEspecificado = bEstadoEspecifico;
                //replicacion.IdEstadoEnvio = sIdEstado;


                ////////if (replicacion.GenerarArchivos())
                ////////{
                ////////    replicacion.EnviarArchivos();
                ////////}

                //valesReg = new ValesRecepcionRegistrar(General.DatosConexion);


                //if (valesReg.ObtenerInformacion())
                //{
                //    valesReg.EnviarInformacion();
                //}

                System.Console.WriteLine("");
                System.Console.WriteLine("Proceso terminado");

                if (bDetener)
                {
                    System.Console.ReadKey(); 
                }
            }
        }
    }
}
