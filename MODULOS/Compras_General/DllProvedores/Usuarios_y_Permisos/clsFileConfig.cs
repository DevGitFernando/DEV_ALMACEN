using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using System.IO;
using SC_SolutionsSystem;
using SC_SolutionsSystem.FuncionesGenerales;

namespace DllProveedores.Usuarios_y_Permisos
{
    public class clsFileConfig
    {
        #region Declaracion de variables
        private string sFileConfig = General.UnidadSO + ":\\" + General.ArchivoIni;
        public string sServer = "";
        public string sBD = "";
        public string sUsuario = "";
        public string sPassword = "";
        public string sTipoDBMS = "";

        basGenerales Fg = new basGenerales();
        private clsCriptografo Cryp = new clsCriptografo();
        private StreamReader rReader;
        private StreamWriter rWriter;
        private FileInfo Dir;
        private FileStream myFile;
        private FrmFileConfig Frm = new FrmFileConfig();
        private bool bMostrar = false;

        #endregion

        #region Constructores de clase y destructor
        public clsFileConfig()
        {
            Dir = new FileInfo(sFileConfig);

            if ( ! Dir.Exists )
            {               
                bMostrar = true;
                myFile = new FileStream(sFileConfig, FileMode.OpenOrCreate);
                rReader = new StreamReader(myFile);
                myFile.Close();
                myFile = null;
                //BuscarConfiguracion();
            }
        }

        public clsFileConfig(string sRutaArchivo)
        {
            sFileConfig = sRutaArchivo;

            Dir = new FileInfo(sFileConfig);

            if (!Dir.Exists)
            {
                bMostrar = true;
                myFile = new FileStream(sFileConfig, FileMode.OpenOrCreate);
                rReader = new StreamReader(myFile);
                myFile.Close();
                myFile = null;
                //BuscarConfiguracion();
            }
        }

        public clsFileConfig(int iRuta)
        {
            Fg.CentrarForma(Frm);
            Frm.ShowDialog();

            if (Frm.bAceptar)
            {
                sFileConfig = Frm.sRutaArchivo;
                sServer = Frm.sServer;
                sBD = Frm.sBD;
                sUsuario = Frm.sUsuario;
                sPassword = Frm.sPassword;
                sTipoDBMS = Frm.sTipoDBMS;
                Frm.Close();
                Grabar();
            }
        }

        ~clsFileConfig()
        {
            Fg = null;
            Cryp = null;
            rReader = null;
            rWriter = null;
            Dir = null;
            myFile = null;
            Frm = null;
        }
        #endregion

        #region Propiedades publicas
        public string RutaArchivo
        {
            get
            {
                return sFileConfig;
            }
            set
            {
                sFileConfig = value;
            }
        }

        public string Servidor
        {
            get
            {
                return sServer;
            }
            set
            {
                sServer = value;
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
                sBD = value;
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
                sUsuario = value;
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
                sPassword = value;
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
                sTipoDBMS = value;
            }
        }

        #endregion

        #region Funciones y procedimientos publicos
        public bool CargarArchivo()
        {
            bool bRegresa = true;

            CargaDatos();
            Frm.sServer = sServer;
            Frm.sBD = sBD;
            Frm.sUsuario = sUsuario;
            Frm.sPassword = sPassword;
            Frm.sTipoDBMS = sTipoDBMS;

            Fg.CentrarForma(Frm);
            Frm.ShowDialog();

            if (Frm.bAceptar)
            {
                sServer = Frm.sServer;
                sBD = Frm.sBD;
                sUsuario = Frm.sUsuario;
                sPassword = Frm.sPassword;
                sTipoDBMS = Frm.sTipoDBMS;
                Frm.Close();
                Grabar();
            }
            else
            {
                bRegresa = false;
            }

            return bRegresa;
        }        

        public bool BuscarConfiguracion()
        {
            bool bRegresa = true;

            if (bMostrar)
            {
                sServer = ObtenerClave("Servidor");
                sBD = ObtenerClave("BaseDeDatos");
                sUsuario = ObtenerClave("Usuario");
                sPassword = ObtenerClave("Password");
                sTipoDBMS = ObtenerClave("TipoDBMS");
                rReader.Close();
                rReader = null;
                Dir.Delete();

                Fg.CentrarForma(Frm);
                Frm.ShowDialog();

                if (Frm.bAceptar)
                {
                    sServer = Frm.sServer;
                    sBD = Frm.sBD;
                    sUsuario = Frm.sUsuario;
                    sPassword = Frm.sPassword;
                    sTipoDBMS = Frm.sTipoDBMS;
                    Frm.Close();
                    Grabar();
                }
                else
                {
                    bRegresa = false;
                }
            }
            return bRegresa;
        }

        public void CargaDatos()
        {
            string myKey = "";
            string myConnectString;
            int EqualPosition = 0;

            StreamReader sr = new StreamReader(sFileConfig);
            //sr = rReader;

            sServer = "";
            sBD = "";
            sUsuario = "";
            sPassword = "";
            sTipoDBMS = "";

            try
            {
                using (sr)
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
            }
            catch
            {
            }
        }
        #endregion

        #region Funciones y procedimientos privados
        public void Grabar()
        {
            rWriter = new StreamWriter(sFileConfig);

            rWriter.WriteLine(Cryp.Encriptar("Servidor=" + sServer + ";"));
            rWriter.WriteLine(Cryp.Encriptar("BaseDeDatos=" + sBD + ";"));
            rWriter.WriteLine(Cryp.Encriptar("Usuario=" + sUsuario + ";"));
            rWriter.WriteLine(Cryp.Encriptar("Password=" + sPassword + ";"));
            rWriter.WriteLine(Cryp.Encriptar("TipoDBMS=" + sTipoDBMS + ";"));

            rWriter.Close();
            rWriter = null;
        }

        public string ObtenerClave(string Key)
        {
            string myKey = "";
            string myConnectString;
            int EqualPosition = 0;

            StreamReader sr;
            sr = rReader;

            try
            {
                using( sr )
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
                                if (myConnectString.Substring(0, EqualPosition) == Key)
                                {
                                    myKey = myConnectString.Substring(EqualPosition + 1);
                                    return myKey;
                                }
                            }
                        }
                    }
                }
                return myKey;
            }
            catch //( Exception e )
            {
                return myKey;
            }
        }
        #endregion
    }
}
