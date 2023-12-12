using System;
using System.Collections.Generic;
using System.Text;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Usuarios_y_Permisos;

using DllTransferenciaSoft.ReplicacionRecetaElectronica;

using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace DTDemonioVales
{
    class Program
    {
        static void Main(string[] args)
        {
            clsEdoLogin Login = new clsEdoLogin();
            ClsReplicacioneRecetaElectronica replicacion = null; //// = new clsReplicacionSQL();
            string sMsj = "";

            General.MultiplesEntidades = true;
            General.bModoDesarrollo = false;
            General.SolicitarConfiguracionWeb = true;
            General.SolicitarConfiguracionLocal = false;
            General.ArchivoIni = "Replicacion_Vales";

            basGenerales Fg = new basGenerales();
            int iDiasRevision = 10;
            bool bDetener = true;
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



                replicacion = new ClsReplicacioneRecetaElectronica();


                System.Console.WriteLine("");
                System.Console.WriteLine("");


                if (replicacion.ObetenerRecetasElectronicas())
                {
                    replicacion.EnviarRecetasElectronicasAtendidas();
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
