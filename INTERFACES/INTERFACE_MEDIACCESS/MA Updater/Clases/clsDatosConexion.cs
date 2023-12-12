using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MA_Updater.FuncionesGenerales;

namespace MA_Updater.Data
{ 
    /// <summary>
    /// Especifica el tipo de servidor que maneja la conexion actual
    /// </summary>
    public enum TipoServer
    {
        None = 0, Postgres = 1, SqlServer = 2, Oracle = 3, MySql = 4, ODBC = 5, OLEDB = 6, IBM_DB2 = 7, SQLite = 8
    }

    ////public class x
    ////{
    ////    clsDatosConexion x = new clsDatosConexion();

    ////    public void z()
    ////    {
    ////    }
    ////}

    /// <summary>
    /// Clase que contiene los datos para conexion con el servidor de base de datos.
    /// </summary>
    public class clsDatosConexion // : ICloneable 
    {
        #region Declaracion de variables
        basGenerales Fg; // = new basGenerales();

        basSeguridad seguridad;
        // clsCriptografo crypto; //  = new clsCriptografo(); 

        // private int intTipoCnn = 0;
        private TipoServer tpTipoServidor = TipoServer.None;
        private string strTipoCnn = "";
        private string strServer = "";
        private string strPuerto = "1433";
        private string strForzarPuerto = "0";
        private bool bForzarPuerto = false;
        private string strBD = "";
        private string strUser = "";
        private string strPassword = "";
        private string strConexionDeConfianza = "0";
        private string strAppName = "";

        private string sPuertoSQL = "1433";
        private string sPuertoOracle = "1521";
        private string sPuertoPostgres = "5432";
        private string sPuertoMySql = "3306";
        private string sPuertoDB2 = "50000";


        private string strPuertoEstandar = "1433";
        private string strPuertoEstandar_Oracle = "1521";
        private string strPuertoEstandar_Postgres = "5432";
        private string strPuertoEstandar_MySql = "3306";
        private string strPuertoEstandar_DB2 = "50000";


        string sPuertoAsignado = "";

        private TiempoDeEspera tecTiempoDeEspera = TiempoDeEspera.SinLimite;
        private bool bConexionDeConfianza = false;
        #endregion

        #region Constructores de clase y destructor
        /// <summary>
        /// Constructor vacio de la clase
        /// </summary>
        public clsDatosConexion()
            : this(TipoServer.SqlServer)
        {
        }

        public clsDatosConexion(TipoServer Server)
        {
            SetTipoServidor(Server);
        }

        public clsDatosConexion(string Configuracion)
        {
            seguridad = new basSeguridad(Configuracion);
            strTipoCnn = seguridad.TipoDBMS;
            strServer = seguridad.Servidor;
            strBD = seguridad.BaseDeDatos;
            strUser = seguridad.Usuario;
            strPassword = seguridad.Password;
            strConexionDeConfianza = "0";
            strPuerto = seguridad.Puerto;

            NormalizarDatos();
            SetTipoServidor(TipoServer.SqlServer);
        }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="prtTipoCnn">Tipo de conexion</param>
        /// <param name="prtServer">Servidor</param>
        /// <param name="prtBb">Base de datos</param>
        /// <param name="prtUser">Usuario</param>
        /// <param name="prtPassword">Password</param>
        public clsDatosConexion(int prtTipoCnn, string prtServer, string prtBb, string prtUser, string prtPassword) :
            this(prtTipoCnn, prtServer, prtBb, prtUser, prtPassword, TipoServer.SqlServer)
        {
        }

        public clsDatosConexion(int prtTipoCnn, string prtServer, string prtBb, string prtUser, string prtPassword, TipoServer Server)
        {
            SetTipoServidor(Server);

            strTipoCnn = prtTipoCnn.ToString();
            strServer = prtServer;
            strBD = prtBb;
            strUser = prtUser;
            strPassword = prtPassword;
            strConexionDeConfianza = "0";
            NormalizarDatos();
        }

        public clsDatosConexion(DataTable Datos)
            : this(Datos, TipoServer.SqlServer)
        {
        }

        public clsDatosConexion(DataTable Datos, TipoServer Server)
        {
            if (Datos != null)
            {
                SetTipoServidor(Server);
                ExtraerDatosCnn(Datos);
            }
        }

        public clsDatosConexion(DataSet Datos)
            : this(Datos, TipoServer.SqlServer)
        {
        }

        public clsDatosConexion(DataSet Datos, TipoServer Server)
        {
            if (Datos != null)
            {
                if (Datos.Tables.Count > 0)
                {
                    SetTipoServidor(Server);
                    ExtraerDatosCnn(Datos.Tables["Conexion"]);
                }
            }
        }
        #endregion

        #region Propiedades publicas
        /// <summary>
        /// Regresa la cadena de conexion que se utiliza actualmente
        /// </summary>       
        public string CadenaDeConexion
        {
            get { return ArmarCadena(); }
            set { }
        }

        /// <summary>
        /// Regresa la cadena de conexion que se utiliza actualmente
        /// </summary>       
        public string CadenaConexion
        {
            get { return ArmarCadena(); }
            set { }
        }

        /// <summary>
        /// Obtiene o establece el servidor actual
        /// </summary>
        public string Servidor
        {
            get { return strServer; }
            set
            {
                strServer = value;

                if (tpTipoServidor != TipoServer.SQLite)
                {
                    if (value.Contains(":"))
                    {
                        int iIndex = value.IndexOf(":") - 0;
                        strServer = value.Substring(0, iIndex);
                    }
                }
            }
        }

        public bool ForzarImplementarPuerto
        {
            get
            {
                //return strForzarPuerto == "1";
                return bForzarPuerto;
            }
            set
            {
                strForzarPuerto = value ? "1" : "0";
                bForzarPuerto = value;
            }
        }

        /// <summary>
        /// Especifica el puerto por el cual escucha el servidor
        /// </summary>
        public string Puerto
        {
            get
            {
                string sRegresa = strPuerto == "" ? strPuertoEstandar : strPuerto;
                return strPuerto;
            }
            set { strPuerto = value; }
        }

        public bool EsPuertoStandard
        {
            get
            {
                bool bRegresa = strPuerto == strPuertoEstandar ? true : false;
                return bRegresa;
            }
        }

        /// <summary>
        /// Obtiene el servidor de base de datos sin la instancia, en caso de que exista
        /// </summary>
        public string ServidorPing
        {
            get { return NombreServidor(); }
            set { ;}
        }

        /// <summary>
        /// Obtiene o estable el nombre de Bd actual
        /// </summary>
        public string BaseDeDatos
        {
            get { return strBD; }
            set { strBD = value; }
        }

        /// <summary>
        /// Obtiene o establece el usuario de la conexion actual
        /// </summary>
        public string Usuario
        {
            get { return strUser; }
            set { strUser = value; }
        }

        /// <summary>
        /// Obtiene o establece el password de la conexion actual
        /// </summary>
        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        /// <summary>
        /// Obtiene o establece el nombre de la aplicación actual
        /// </summary>
        public string AppName
        {
            get { return strAppName; }
            set { strAppName = value; }
        }

        /// <summary>
        /// Obtien o estable el tipo de DMBS actual
        /// </summary>
        public string TipoDBMS
        {
            get { return strTipoCnn; }
            set
            {
                strTipoCnn = value;
                DeterminarTipoServer();
            }
        }

        /// <summary>
        /// Especifica el tiempo que para esperar que se establesca la conexión antes de marcar error
        /// </summary>
        public TiempoDeEspera ConectionTimeOut
        {
            get { return tecTiempoDeEspera; }
            set { tecTiempoDeEspera = value; }
        }

        /// <summary>
        /// Especifica si se debe usar la seguridad de Windows ó Mixta 
        /// </summary>
        public bool ConexionDeConfianza
        {
            get { return bConexionDeConfianza; }
            set
            {
                bConexionDeConfianza = value;
                strConexionDeConfianza = bConexionDeConfianza ? "1" : "0";
            }
        }

        public TipoServer TipoServidor
        {
            get { return tpTipoServidor; }
            set
            {
                tpTipoServidor = value;
                strTipoCnn = ((int)tpTipoServidor).ToString();
                DeterminarTipoServer();
            }
        }
        #endregion Propiedades publicas

        #region Funciones y procedimientos publicos
        public DataSet DatosCnn()
        {
            clsCriptografo Cryp = new clsCriptografo();
            DataSet dtConexion = new DataSet();
            DataTable dtTabla = new DataTable("Conexion");
            DataRow dtRow;
            DataColumn dtCol;
            string sConexionDeConfianza = "0";

            if (bConexionDeConfianza)
            {
                sConexionDeConfianza = "1";
            }

            dtConexion.Tables.Add(dtTabla);
            // Servidor              ==> Campo1 
            // BaseDeDatos           ==> Campo2 
            // Usuario               ==> Campo3 
            // Password              ==> Campo4 
            // TipoDBMS              ==> Campo5 
            // ConexionDeConfianza   ==> Campo6 
            // Puerto                ==> Campo7 
            // AppName               ==> Campo8 
            // Forzar puerto         ==> Campo9 

            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo01", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo02", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo03", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo04", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo05", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo06", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo07", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo08", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo09", System.Type.GetType("System.String"));

            object[] objRow = { Cryp.Encriptar(strServer), Cryp.Encriptar(strBD), Cryp.Encriptar(strUser), 
                                Cryp.Encriptar(strPassword), Cryp.Encriptar(strTipoCnn), Cryp.Encriptar(sConexionDeConfianza), 
                                Cryp.Encriptar(strPuerto), Cryp.Encriptar(strAppName), Cryp.Encriptar(strForzarPuerto)   
                              };
            dtRow = dtConexion.Tables["Conexion"].Rows.Add(objRow);

            return dtConexion;
        }
        #endregion

        #region Funciones y procedimientos privados
        private void SetTipoServidor(TipoServer Server)
        {
            switch (Server)
            {
                case TipoServer.Postgres:
                    strPuerto = strPuertoEstandar_Postgres;
                    break;

                case TipoServer.SqlServer:
                    strPuerto = strPuertoEstandar;
                    break;

                case TipoServer.Oracle:
                    strPuerto = strPuertoEstandar_Oracle;
                    break;

                case TipoServer.MySql:
                    strPuerto = strPuertoEstandar_MySql;
                    break;

                case TipoServer.IBM_DB2:
                    strPuerto = strPuertoEstandar_DB2;
                    break;

                case TipoServer.SQLite:
                    strPuerto = "";
                    break;
            }

            strTipoCnn = ((int)Server).ToString();
            tpTipoServidor = Server;
        }

        private void ExtraerDatosCnn(DataTable DatosExtra)
        {
            string[] sCampo = { "Campo01" };
            string sPrefijo = "";

            try
            {
                clsLeer cnn = new clsLeer();
                cnn.DataTableClase = DatosExtra;
                //if (existeTabla(DatosExtra))
                {
                    //// DataRow row; 
                    // Desenciptar los datos de conexion
                    clsCriptografo Cryp = new clsCriptografo();

                    // row = DatosExtra.Rows[0];
                    // Servidor              ==> Campo1
                    // BaseDeDatos           ==> Campo2
                    // Usuario               ==> Campo3
                    // Password              ==> Campo4
                    // TipoDBMS              ==> Campo5
                    // ConexionDeConfianza   ==> Campo6
                    // Puerto                ==> Campo7  
                    // AppName               ==> Campo8  
                    // Forzar puerto         ==> Campo9 
                    cnn.Leer();

                    if (cnn.ValidarExistenCampos(sCampo))
                    {
                        sPrefijo = "0";
                    }

                    strServer = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}1", sPrefijo)));
                    strBD = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}2", sPrefijo)));
                    strUser = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}3", sPrefijo)));
                    strPassword = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}4", sPrefijo)));
                    strTipoCnn = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}5", sPrefijo)));
                    strConexionDeConfianza = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}6", sPrefijo)));
                    strPuerto = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}7", sPrefijo)));
                    strAppName = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}8", sPrefijo)));
                    strForzarPuerto = Cryp.Desencriptar(cnn.Campo(string.Format("Campo{0}9", sPrefijo))); 


                    bForzarPuerto = strForzarPuerto == "1";
                    bConexionDeConfianza = strConexionDeConfianza == "1";

                    //////strServer = Cryp.Desencriptar((string)row["Campo1"].ToString().Trim());
                    //////strBD = Cryp.Desencriptar((string)row["Campo2"].ToString().Trim());
                    //////strUser = Cryp.Desencriptar((string)row["Campo3"].ToString().Trim());
                    //////strPassword = Cryp.Desencriptar((string)row["Campo4"].ToString().Trim());
                    //////strTipoCnn = Cryp.Desencriptar((string)row["Campo5"].ToString().Trim());
                    //////strConexionDeConfianza = Cryp.Desencriptar((string)row["Campo6"].ToString().Trim()); 
                    //////bConexionDeConfianza = strConexionDeConfianza == "1"; 


                    NormalizarDatos();
                }
            }
            catch { }
        }

        /// <summary>D:\PROYECTOS .NET 2.0\SC_SolutionsSystem\Data\clsDatosCliente.cs
        /// Normaliza los datos de conexion, reemplaza el (;) por ("")
        /// </summary>
        public void NormalizarDatos()
        {
            strTipoCnn = strTipoCnn.Replace(";", "").Trim();
            strServer = strServer.Replace(";", "").Trim();
            strServer = strServer.Replace(@"\\", @"\").Trim();
            strBD = strBD.Replace(";", "").Trim();
            strUser = strUser.Replace(";", "").Trim();
            strPassword = strPassword.Replace(";", "").Trim();
            strConexionDeConfianza = strConexionDeConfianza.Replace(";", "").Trim();
            strTipoCnn = strTipoCnn.Trim() == "" ? "2" : strTipoCnn.Trim();
            strPuerto = strPuerto.Replace(";", "").Trim();
            strAppName = strAppName.Replace(";", "").Trim();
            strForzarPuerto = strForzarPuerto.Replace(";", "").Trim();
            //strTipoServidor = (TipoServer)strTipoCnn;

            DeterminarTipoServer();
        }

        private void DeterminarTipoServer()
        {
            int iTipoServer = 0;
            try
            {
                iTipoServer = Convert.ToInt32("0" + strTipoCnn);
            }
            catch
            {
                iTipoServer = (int)TipoServer.SqlServer;
            }

            tpTipoServidor = (TipoServer)iTipoServer;
        }

        /// <summary>
        /// Determina si la tabla con los datos de conexion existe
        /// </summary>
        /// <param name="dtsParametro">Dataset que contiene los datos de conexion</param>
        /// <returns></returns>
        private bool existeTabla(DataSet dtsParametro)
        {
            bool bRegresa = false;
            //DataTable dtTabla;

            foreach (DataTable dt in dtsParametro.Tables)
            {
                if (dt.TableName == "Conexion")
                {
                    bRegresa = true;
                    break;
                }
            }

            return bRegresa;
        }

        /// <summary>
        /// Devuelve la cadena de conexion de acuerdo al DBMS que se este utilizando
        /// </summary>
        /// <returns></returns>
        private string ArmarCadena()
        {
            string strRegresa = "";
            string sEncryption = " Encrypt=yes; ";
            sEncryption = "";

            //// Prepara el formato de los datos 
            NormalizarDatos();

            if (tpTipoServidor == TipoServer.Postgres)
            {
                strRegresa = "Driver={PostgreSQL};database=" + strBD + ";server=" + strServer + ";uid=" + strUser + ";pwd=" + strPassword + ";";
                strRegresa = string.Format("Driver={PostgreSQL};database={0};server={1};uid={2};pwd={3};", strBD, strServer, strUser, strPassword);

                strRegresa = string.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4}; ",
                    strServer, strPuerto, strUser, strPassword, strBD);

                if (bConexionDeConfianza)
                {
                    strRegresa = string.Format("Server={0}; Port={1}; Database={4};Integrated Security=true; ",
                        strServer, strPuerto, strUser, strPassword, strBD);
                }
            }

            if (tpTipoServidor == TipoServer.SqlServer)
            {
                if (strServer.Contains(":"))
                {
                    int iIndex = strServer.IndexOf(":") - 0;
                    strServer = strServer.Substring(0, iIndex);
                }

                //// Puerto en caso de Instnacias 
                //sPuertoAsignado = string.Format(",{0}", strPuertoEstandar); 
                if (bForzarPuerto)
                {
                    sPuertoAsignado = string.Format(",{0}", strPuerto);
                }
                else
                {
                    if (strPuerto != strPuertoEstandar)
                    {
                        sPuertoAsignado = string.Format(",{0}", strPuerto);
                    }
                }

                //// strRegresa = "Data Source=" + strServer + "; Initial Catalog=" + strBD + ";user=" + strUser + ";pwd=" + strPassword + ";";
                // Network Library=DBMSSOCN; 
                strRegresa = string.Format("Data Source={0}{1}; Initial Catalog={2}; user={3}; pwd={4}; Connect Timeout={5}; {6} ",
                    strServer, sPuertoAsignado, strBD, strUser, strPassword, ((int)tecTiempoDeEspera).ToString(), sEncryption);

                if (bConexionDeConfianza)
                {
                    //// strRegresa = string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=SSPI; Connect Timeout={2};", strServer, strBD, ((int)tecTiempoDeEspera).ToString());
                    strRegresa = string.Format("Data Source={0}{1}; Initial Catalog={2}; Integrated Security=SSPI; Connect Timeout={3}; {4} ",
                        strServer, sPuertoAsignado, strBD, ((int)tecTiempoDeEspera).ToString(), sEncryption);
                }

                if (strAppName != "")
                {
                    strRegresa += string.Format(" Application Name={0}; ", strAppName);
                }
            }

            if (tpTipoServidor == TipoServer.Oracle)
            {
                sPuertoAsignado = strPuertoEstandar_Oracle;
                if (strPuerto != strPuertoEstandar_Oracle)
                {
                    sPuertoAsignado = string.Format("{0}", strPuerto);
                }

                // connectionstring ="Provider=msdaora;Data Source=MyOracleDB;User Id=myUsername;Password=myPassword"; 
                strRegresa = string.Format(
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})" +
                    "(PORT={1}))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id={3};Password={4};",
                    strServer, sPuertoAsignado.Replace(",", ""), strBD, strUser, strPassword);
            }

            if (tpTipoServidor == TipoServer.MySql)
            {
                //Server = myServerAddress; Uid = myUsername; Pwd = myPassword;
                strRegresa = string.Format("Server={0}; Database = {1}; User Id={2}; Password={3}; ",
                    strServer, strBD, strUser, strPassword);

                if (strPuerto != strPuertoEstandar_MySql)
                {
                    strRegresa = string.Format("Server={0}; Port = {1}; Database = {2}; User Id={3}; Password={4}; ",
                        strServer, strPuerto, strUser, strPassword, strBD);
                }
            }


            ////if (strTipoCnn == "3x")
            ////{
            ////    //// strRegresa = "Driver={MySQL ODBC 3.51 Driver};server=" + strServer + ";Database=" + strBD + ";User=" + strUser + ";Password=" + strPassword + ";"; //Port=3306 
            ////    strRegresa = string.Format("Driver={MySQL ODBC 3.51 Driver};server={0};Database={1};User={2};Password={3};", strServer, strBD, strUser, strPassword); 
            ////}

            ////if (strTipoCnn == "4x")
            ////{
            ////    //// strRegresa = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strServer + strBD + ";uid=" + strUser + ";pwd=" + strPassword + ";"; 
            ////    strRegresa = string.Format("Driver={Microsoft Access Driver (*.mdb)};DBQ={0}{1};uid={2};pwd={3};", strServer, strBD, strUser, strPassword); 
            ////}


            if (tpTipoServidor == TipoServer.IBM_DB2)
            {
                /////// FUNCIONA 
                strRegresa = string.Format("DataSource={0}; User Id={3}; Password={4}; DefaultCollection = {2}",
                    strServer, strPuerto, strBD, strUser, strPassword);
                if (strPuerto != strPuertoEstandar_DB2)
                {
                    strRegresa = string.Format("DataSource={0}:{1}; User Id={3}; Password={4}; DefaultCollection = {2}",
                        strServer, strPuerto, strBD, strUser, strPassword);
                }


                /////////// FUNCIONA 
                ////strRegresa = string.Format("DataSource={0}; UserId={3}; Password={4};",
                ////    strServer, strPuerto, strBD, strUser, strPassword);
                ////if (strPuerto != strPuertoEstandar_DB2)
                ////{
                ////    strRegresa = string.Format("DataSource={0}:{1}; UserId={3}; Password={4};",
                ////        strServer, strPuerto, strBD, strUser, strPassword);
                ////}

            }

            if (tpTipoServidor == TipoServer.SQLite)
            {
                strRegresa = string.Format("Data Source={0}; UTF8Encoding=True; ", strServer);

                if (strPassword.Trim() != "" && strPassword.Trim() != ".")
                {
                    strRegresa += string.Format("Password={0}; ", strPassword.Trim());
                }
            }

            return strRegresa;
        }

        private string NombreServidor()
        {
            string sRegresa = strServer;
            int iPos = sRegresa.IndexOf("\\");

            if (Fg == null)
            {
                Fg = new basGenerales();
            }

            if (iPos != -1)
            {
                sRegresa = Fg.Left(sRegresa, iPos);
            }

            iPos = 0;
            iPos = sRegresa.IndexOf(",");
            if (iPos != -1)
            {
                sRegresa = Fg.Left(sRegresa, iPos);
            }

            return sRegresa;
        }

        #endregion Funciones y procedimientos privados

        ////#region Herencia 
        ////public object Clone()
        ////{
        ////    clsDatosConexion datosCnn = (clsDatosConexion)this.MemberwiseClone();
        ////    return datosCnn; 
        ////}
        ////#endregion Herencia
    }
}

