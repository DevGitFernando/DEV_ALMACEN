using System;
using System.Collections.Generic;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllTransferenciaSoft.ReplicacionSQL; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace DtDemonioReplicacion
{

    /* 
     * M ==> Modulo 
     * V ==> Version 
     * S ==> Es Servidor de Red Local 
     * C ==> Mensaje de Confirmacion 
     * I ==> Mostrar Interface 
     * i ==> Actualizacion Manual 
     * E ==> Habilitar WithEncryption 
     * a ==> Es Almacen 
     * s ==> Servidor Central | Regional 
    */

    static class Program
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
            clsReplicacionSQL replicacion = null; //// = new clsReplicacionSQL();
            string sMsj = "";

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "CFG Replicación";

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


                //// Revisar si la se esta ejecutando una Instalacion Manual 
                if (appArgumentos.GetValor("R") != "0")
                {
                    iDiasRevision = Convert.ToInt32("0" + appArgumentos.GetValor("R")); 
                }

                if (appArgumentos.GetValor("D") != "")
                {
                    bDetener = appArgumentos.GetValor("D") == "1";
                }

                if (appArgumentos.GetValor("P") == "S")
                {
                    if (appArgumentos.GetValor("F") != "" && appArgumentos.GetValor("f") != "")
                    {
                        bRevisionPorFechas = true;
                        sFechaInicial = appArgumentos.GetValor("F");
                        sFechaFinal = appArgumentos.GetValor("f");
                    }
                }

                if (appArgumentos.GetValor("P") == "R")
                {
                    bRevisionPorFechas = true;
                    bRevisionPorFechaServidor = true;
                    System.Console.WriteLine("Replicación por módulos.");
                }

                if (appArgumentos.GetValor("T") != "1" && appArgumentos.GetValor("T") != "")
                {
                    iTipoDeProceso = Convert.ToInt32("0" + appArgumentos.GetValor("T"));
                }

                if (appArgumentos.GetValor("U") != "1" && appArgumentos.GetValor("U") != "")
                {
                    iTipoDeEnvio = Convert.ToInt32("0" + appArgumentos.GetValor("U"));
                }

                if (appArgumentos.GetValor("I") != "S")
                {
                    System.Console.WriteLine(General.DatosConexion.CadenaConexion);
                }

                if (appArgumentos.GetValor("E") == "S")
                {
                    bEstadoEspecifico = true;
                    bEstadoEspecifico = appArgumentos.GetValor("O").Trim() != "";
                    if (appArgumentos.GetValor("O").Trim() != "")
                    {
                        sIdEstado = appArgumentos.GetValor("O").Trim();
                    }
                }


                //// 
                if (!bRevisionPorFechas)
                {
                    replicacion = new clsReplicacionSQL(iTipoDeEnvio, iTipoDeProceso, iDiasRevision);
                }
                else
                {
                    if (!bRevisionPorFechaServidor)
                    {
                        replicacion = new clsReplicacionSQL(iTipoDeEnvio, iTipoDeProceso, sFechaInicial, sFechaFinal);
                    }
                    else
                    {
                        replicacion = new clsReplicacionSQL(iTipoDeEnvio, iTipoDeProceso, bRevisionPorFechaServidor);
                    }
                }

                Parametros param = new Parametros("", "", "");


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

                replicacion.SoloEstadoEspecificado = bEstadoEspecifico;
                replicacion.IdEstadoEnvio = sIdEstado;


                if (replicacion.GenerarArchivos())
                {
                    replicacion.EnviarArchivos();
                }

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
