using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using UpdateAdministracion.FuncionesGenerales;

namespace UpdateAdministracion.FuncionesGenerales
{
    public class basSeguridad
    {
        #region Declaracion de variables
        private string sFileConfig = General.UnidadSO + ":\\" + General.ArchivoIni;
        private string sNameFileConfig = General.ArchivoIni;
        public string sServer = "";
        public string sBD = "";
        public string sUsuario = "";
        public string sPassword = "";
        public string sTipoDBMS = "";

        private clsCriptografo Cryp = new clsCriptografo();
        #endregion

        #region Constructores de clase y destructor
        public basSeguridad()
        {
            GetLocalConfigurationKey();
        }

        public basSeguridad(string NombreArchivoConfig)
        {
            General.ArchivoIni = NombreArchivoConfig;
            this.sNameFileConfig = General.ArchivoIni;
            // General.UsarWebService = true;
            GetLocalConfigurationKey();
        }

        #endregion

        #region Propiedades publicas
        public string Servidor        
        {
            get
            {
                return sServer;
            }
            set
            {
                ;
            }
        }

        public string BaseDeDatos
        {
            get
            {
                return sBD;
            }
            set
            {
                ;
            }
        }

        public string Usuario
        {
            get
            {
                return sUsuario;
            }
            set
            {
                ;
            }
        }

        public string Password
        {
            get
            {
                return sPassword;
            }
            set
            {
                ;
            }
        }

        public string TipoDBMS
        {
            get
            {
                return sTipoDBMS;
            }
            set
            {
                ;
            }
        }
        #endregion

        #region Funciones y procedimientos privados
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
            ////    filePath = BaseDir.Substring(0, lenWebServiceName) + sNameFileConfig; //"Config.ini";
            ////else
                filePath = sFileConfig; // General.UnidadSO + ":\\Config.ini";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        myConnectString = sr.ReadLine();
                        myConnectString = Cryp.Desencriptar(myConnectString);
                        if (myConnectString.Length != 0)
                        {
                            EqualPosition = myConnectString.IndexOf("=", 0);
                            if (EqualPosition > 0)
                            {
                                myKey = myConnectString.Substring(0, EqualPosition).ToUpper();
                                if (myKey == "Servidor".ToUpper())
                                    sServer = myConnectString.Substring(EqualPosition + 1);

                                if (myKey == "BaseDeDatos".ToUpper())
                                    sBD = myConnectString.Substring(EqualPosition + 1);

                                if (myKey == "Usuario".ToUpper())
                                    sUsuario = myConnectString.Substring(EqualPosition + 1);

                                if (myKey == "Password".ToUpper())
                                    sPassword = myConnectString.Substring(EqualPosition + 1);

                                if (myKey == "TipoDBMS".ToUpper())
                                    sTipoDBMS = myConnectString.Substring(EqualPosition + 1);
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
        #endregion
    }
}
