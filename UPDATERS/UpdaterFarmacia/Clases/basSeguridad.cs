using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using UpdateFarmacia.FuncionesGenerales;

namespace UpdateFarmacia.FuncionesGenerales
{
    public class basSeguridad
    {
        #region Declaracion de variables
        private string sFileConfig = General.UnidadSO + ":\\" + General.ArchivoIni;
        private string sNameFileConfig = General.ArchivoIni;

        public string sServer = "";
        public string sPuerto = "1433";
        public string sBD = "";
        public string sUsuario = "";
        public string sPassword = "";
        public string sTipoDBMS = "";
        private bool bEncryptado = true;

        string sPuertoEstandar = "1433";

        private string sDirectorioBase = "";

        private clsCriptografo Cryp = new clsCriptografo();
        #endregion

        #region Constructores de clase y destructor
        public basSeguridad()
        {
            GetLocalConfigurationKey();
        }

        public basSeguridad(string NombreArchivoConfig)
            : this(NombreArchivoConfig, true)
        {
        }

        public basSeguridad(string NombreArchivoConfig, bool Encryptado)
        {
            General.ArchivoIni = NombreArchivoConfig;
            this.sNameFileConfig = General.ArchivoIni;
            //General.UsarWebService = true;
            bEncryptado = Encryptado;
            GetLocalConfigurationKey();
        }

        #endregion Constructores de clase y destructor

        #region Propiedades publicas
        public string Servidor
        {
            get { return sServer; }
            set { ; }
        }

        public string Puerto
        {
            get
            {
                string sRegresa = sPuerto;
                sRegresa = sPuerto == "" ? sPuertoEstandar : sPuerto;

                return sRegresa;
            }
            set { ; }
        }

        public string BaseDeDatos
        {
            get { return sBD; }
            set { ; }
        }

        public string Usuario
        {
            get { return sUsuario; }
            set { ; }
        }

        public string Password
        {
            get { return sPassword; }
            set { ; }
        }

        public string TipoDBMS
        {
            get { return sTipoDBMS; }
            set { ; }
        }

        public string DirectorioBase
        {
            get { return sDirectorioBase; }
        }
        #endregion Propiedades publicas

        #region Funciones y Procedimientos Privados
        private void GetLocalConfigurationKey()
        {
            string myKey = "";
            string myConnectString;
            string filePath = General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            int EqualPosition = 0;

            int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;

            //if (lenWebServiceName > 10)
            //if (1 == 1)

            ////if (General.UsarWebService)
            ////{
            ////    sDirectorioBase = BaseDir.Substring(0, lenWebServiceName);
            ////    filePath = BaseDir.Substring(0, lenWebServiceName) + sNameFileConfig; //"Config.ini";
            ////}
            ////else
            {
                filePath = sFileConfig; // General.UnidadSO + ":\\Config.ini";
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        myConnectString = sr.ReadLine();

                        if (bEncryptado)
                        {
                            myConnectString = Cryp.Desencriptar(myConnectString);
                        }


                        if (myConnectString.Length != 0)
                        {
                            EqualPosition = myConnectString.IndexOf("=", 0);
                            if (EqualPosition > 0)
                            {
                                myKey = myConnectString.Substring(0, EqualPosition).ToUpper().Trim();
                                if (myKey == "Servidor".ToUpper())
                                    sServer = myConnectString.Substring(EqualPosition + 1).Trim();

                                if (myKey == "Puerto".ToUpper())
                                    sPuerto = myConnectString.Substring(EqualPosition + 1).Trim();

                                if (myKey == "BaseDeDatos".ToUpper())
                                    sBD = myConnectString.Substring(EqualPosition + 1).Trim();

                                if (myKey == "Usuario".ToUpper())
                                    sUsuario = myConnectString.Substring(EqualPosition + 1).Trim();

                                if (myKey == "Password".ToUpper())
                                    sPassword = myConnectString.Substring(EqualPosition + 1).Trim();

                                if (myKey == "TipoDBMS".ToUpper())
                                    sTipoDBMS = myConnectString.Substring(EqualPosition + 1).Trim();
                            }
                        }
                    }
                }
                //return myKey;                
            }
            catch
            {
                //return myKey;
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}