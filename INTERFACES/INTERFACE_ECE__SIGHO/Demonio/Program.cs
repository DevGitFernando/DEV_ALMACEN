using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using Dll_IRE_SIGHO.Clases;
using Dll_IRE_SIGHO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

namespace ISIIECE_SERVICIOS
{
    class Program
    {
        public static Argumentos appArgumentos; 

        static void Main(string[] args)
        {

            string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
            string[] argsAux2 = { "iS" };
            GnDll_SII_RE_SIGHO.MostrarInterface = true;
            appArgumentos = new Argumentos(args);


            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "ISIIECE SERVICIOS";

            basGenerales Fg = new basGenerales();
            bool bEsEnvio = false;
            //int iDiasRevision = 10;
            //bool bRevisionPorFechas = false;
            //bool bRevisionPorFechaServidor = false;
            //string sFechaInicial = "";
            //string sFechaFinal = "";
            //int iTipoDeProceso = 1;
            //int iTipoDeEnvio = 1;
            //bool bEstadoEspecifico = false;
            //string sIdEstado = ""; 

            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
            System.Console.WriteLine("Estableciendo conexión con el servidor.");


            if (appArgumentos.GetValor("S") != "")
            {
                bEsEnvio =  (appArgumentos.GetValor("S") == "E");
            }

            if (appArgumentos.GetValor("I") != "")
            {
                GnDll_SII_RE_SIGHO.MostrarInterface = !(appArgumentos.GetValor("I") == "N");
            }


            if (GnDll_SII_RE_SIGHO.MostrarInterface)
            {
                //General.msjAviso("Interface");
                MostrarInterface();
            }
            else
            {
                //General.msjAviso("Sin interface"); 
                //IniciarSinInterface(bEsEnvio);
            }
        }


        //private static void IniciarSinInterface(bool bEsEnvio)
        //{
        //    ClsReplicacioneRecetaElectronica replicacion = null;
        //    clsEdoLogin Login = new clsEdoLogin();
        //    string sMsj = "";
        //    bool bDetener = true;

        //    Login.XmlEnDirectorioApp = true;

        //    if (!Login.AutenticarServicioSO())
        //    {
        //        System.Console.WriteLine("No fue posible establecer conexión con el servidor.");
        //        System.Console.WriteLine("Presione una tecla para continuar.");
        //        System.Console.ReadKey();
        //    }
        //    else
        //    {
        //        sMsj = "";



        //        replicacion = new ClsReplicacioneRecetaElectronica();


        //        System.Console.WriteLine("");
        //        System.Console.WriteLine("");

        //        if (!bEsEnvio)
        //        {
        //            replicacion.ObtenerRecetasElectronicas();
        //        }
        //        else
        //        {
        //            replicacion.EnviarRecetasElectronicasAtendidas();
        //        }

        //        System.Console.WriteLine("");
        //        System.Console.WriteLine("Proceso terminado");

        //        if (bDetener)
        //        {
        //            System.Console.ReadKey();
        //        }
        //    }
        //}

        private static void MostrarInterface()
        {
            Application.EnableVisualStyles();

            Application.Run(new FrmMain());
        }

    }
}
