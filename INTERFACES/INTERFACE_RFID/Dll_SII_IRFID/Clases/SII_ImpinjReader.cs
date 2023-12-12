using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading; 

using Impinj.OctaneSdk;


namespace Impinj.OctaneSdk
{
    public class SII_ImpinjReader : Impinj.OctaneSdk.ImpinjReader 
    {
        bool bReaderOnLine = false; 
        bool bEnviaAlertas = false;
        int iTipoDeLectura = 0;
        bool bConexionEstablecida = false;
        bool bRegistrarTAG = false;
        bool bManejaGPO = false;

        bool bPuerto_01_GPO = false;
        bool bPuerto_02_GPO = false;
        bool bPuerto_03_GPO = false;
        bool bPuerto_04_GPO = false;

        string sPuerto_01_GPO_Descripcion = "";
        string sPuerto_02_GPO_Descripcion = "";
        string sPuerto_03_GPO_Descripcion = "";
        string sPuerto_04_GPO_Descripcion = "";

        public SII_ImpinjReader() :base()
        {
        }

        public SII_ImpinjReader(string Address, string Name): base(Address, Name)
        {
        }

        #region Propiedades 
        public bool EnviaAlertas
        {
            get { return bEnviaAlertas; }
            set { bEnviaAlertas = value; }
        }

        public int TipoDeLectura
        {
            get { return iTipoDeLectura; }
            set { iTipoDeLectura = value; }
        }

        public bool RegistrarTAG
        {
            get { return iTipoDeLectura <= 2; }
        }

        public bool ConexionEstablecida
        {
            get { return bConexionEstablecida; }
            set { bConexionEstablecida = value; }
        }

        public bool ManejaGPO
        {
            get { return bManejaGPO; }
            set { bManejaGPO = value; }
        }

        public bool Puerto_01_GPO
        {
            get { return bPuerto_01_GPO; }
            set { bPuerto_01_GPO = value; }
        }

        public bool Puerto_02_GPO
        {
            get { return bPuerto_02_GPO; }
            set { bPuerto_02_GPO = value; }
        }

        public bool Puerto_03_GPO
        {
            get { return bPuerto_03_GPO; }
            set { bPuerto_03_GPO = value; }
        }

        public bool Puerto_04_GPO
        {
            get { return bPuerto_04_GPO; }
            set { bPuerto_04_GPO = value; }
        }

        public string Puerto_01_GPO_Descripcion
        {
            get { return sPuerto_01_GPO_Descripcion; }
            set { sPuerto_01_GPO_Descripcion = value; }
        }

        public string Puerto_02_GPO_Descripcion
        {
            get { return sPuerto_02_GPO_Descripcion; }
            set { sPuerto_02_GPO_Descripcion = value; }
        }

        public string Puerto_03_GPO_Descripcion
        {
            get { return sPuerto_03_GPO_Descripcion; }
            set { sPuerto_03_GPO_Descripcion = value; }
        }

        public string Puerto_04_GPO_Descripcion
        {
            get { return sPuerto_04_GPO_Descripcion; }
            set { sPuerto_04_GPO_Descripcion = value; }
        }

        public bool ReaderOnLine
        {
            get { return bReaderOnLine; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos
        public bool ReaderIsAvailable()
        {
            return ReaderIsAvailable(base.Address); 
        }

        public bool ReaderIsAvailable(string Address)
        {
            ///// Ping the reader.
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            byte[] buffer = Encoding.Default.GetBytes("12345");
            PingReply reply = pingSender.Send(Address, 500, buffer, options);

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reboot()
        {
            Reboot(base.Address); 
        }

        public void Reboot(string Address)
        {
            string reply = " ";
            int iIncremento = 1000;
            int iContador = 0;
            int iTiempoMaximo = (1000 * 60);
            int iEsperaInicial = 5000;


            try
            {

                base.RShell.Open(Address, "root", "impinj", 5000);
                RShellCmdStatus status = base.RShell.Send("reboot", out reply);

                //// Close the RShell connection.
                base.RShell.Close();


                ////// Check the status of the RShell command.
                if (status == RShellCmdStatus.Success)
                {
                    Console.WriteLine("Reader rebooting...\n");
                    Thread.Sleep(iEsperaInicial);
                    iContador += iEsperaInicial;

                    Console.Write("Waiting for reader to come back online.");
                    //////// Ping the reader until it's back online.
                    while (!ReaderIsAvailable(Address))
                    {
                        ////Console.Write(".");
                        Thread.Sleep(iIncremento);

                        iContador += iIncremento;


                        if (iContador > iTiempoMaximo)
                        {
                            break;
                        }
                    }

                    bReaderOnLine = iContador <= iTiempoMaximo;

                    ////Console.WriteLine("\nThe reader is back online. Press enter to reconnect and get tag data.\n");
                    ////Console.ReadLine();
                    ////Console.WriteLine("Reconnecting to reader.");

                }
            }
            catch (OctaneSdkException e1)
            {
                //// Handle Octane SDK errors.
                Console.WriteLine("Octane SDK exception: {0}", e1.Message);
            }
            catch (Exception e2)
            {
                //// Handle other .NET errors.
                Console.WriteLine("Exception : {0}", e2.Message);
            }
        }
        #endregion Funciones y Procedimientos
    }
}
