using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using UpdaterOficinaCentralRegional.FuncionesGenerales;

namespace UpdaterOficinaCentralRegional.Data
{
    /// <summary>
    /// Especifica el tipo de servidor que maneja la conexion actual
    /// </summary>
    public enum TipoServer
    {
        SqlServer = 1, Postgres = 2
    }

    /// <summary>
    /// Clase que contiene los datos para conexion con el servidor de base de datos.
    /// </summary>
    public class clsDatosConexion
    {
        #region Declaracion de variables
        basGenerales Fg = new basGenerales();

        basSeguridad seguridad;
        // clsCriptografo crypto; //  = new clsCriptografo(); 

        // private int intTipoCnn = 0;
        // private TipoServer strTipoServidor = TipoServer.SqlServer;
        private string strTipoCnn = "";
        private string strServer = "";
        private string strBD = "";
        private string strUser = "";
        private string strPassword = "";
        private TiempoDeEspera tecTiempoDeEspera = TiempoDeEspera.Limite120;
        #endregion

        #region Constructores de clase y destructor
        /// <summary>
        /// Constructor vacio de la clase
        /// </summary>
        public clsDatosConexion()
        {            
        }

        public clsDatosConexion(string Configuracion)
        {
            seguridad = new basSeguridad(Configuracion); 
            strTipoCnn = seguridad.TipoDBMS ;
            strServer = seguridad.Servidor;
            strBD = seguridad.BaseDeDatos;
            strUser = seguridad.Usuario;
            strPassword = seguridad.Password;
            NormalizarDatos();

        } 

        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="prtTipoCnn">Tipo de conexion</param>
        /// <param name="prtServer">Servidor</param>
        /// <param name="prtBb">Base de datos</param>
        /// <param name="prtUser">Usuario</param>
        /// <param name="prtPassword">Password</param>
        public clsDatosConexion(int prtTipoCnn, string prtServer, string prtBb, string prtUser, string prtPassword)
        {
            strTipoCnn = prtTipoCnn.ToString();
            strServer = prtServer;
            strBD = prtBb;
            strUser = prtUser;
            strPassword = prtPassword;
            NormalizarDatos();
        }

        public clsDatosConexion(DataTable Datos)
        {
            if (Datos != null)
            {
                ExtraerDatosCnn(Datos);
            }
        }

        public clsDatosConexion(DataSet Datos)
        {
            if (Datos != null)
            {
                if (Datos.Tables.Count > 0)
                {
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
            set { strServer = value; }
        }

        /// <summary>
        /// Obtiene el servidor de base de datos sin la instancia, en caso de que exista
        /// </summary>
        public string ServidorPing
        {
            get { return NombreServidor(); }
            set {;}
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
        /// Obtien o estable el tipo de DMBS actual
        /// </summary>
        public string TipoDBMS
        {
            get { return strTipoCnn; } 
            set { strTipoCnn = value; }
        }

        /// <summary>
        /// Especifica el tiempo que para esperar que se establesca la conexión antes de marcar error
        /// </summary>
        public TiempoDeEspera ConectionTimeOut
        {
            get { return tecTiempoDeEspera; }
            set { tecTiempoDeEspera = value; }
        }
        #endregion

        #region Funciones y procedimientos publicos
        public DataSet DatosCnn()
        {
            DataSet dtConexion = new DataSet();
            DataTable dtTabla = new DataTable("Conexion");
            DataRow dtRow;
            DataColumn dtCol;

            clsCriptografo Cryp = new clsCriptografo();

            dtConexion.Tables.Add(dtTabla);
            // Servidor    ==> Campo1
            // BaseDeDatos ==> Campo2
            // Usuario     ==> Campo3
            // Password    ==> Campo4
            // TipoDBMS    ==> Campo5
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo1", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo2", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo3", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo4", System.Type.GetType("System.String"));
            dtCol = dtConexion.Tables["Conexion"].Columns.Add("Campo5", System.Type.GetType("System.String"));

            object[] objRow = { Cryp.Encriptar(strServer), Cryp.Encriptar(strBD), Cryp.Encriptar(strUser), Cryp.Encriptar(strPassword), Cryp.Encriptar(strTipoCnn) };
            dtRow = dtConexion.Tables["Conexion"].Rows.Add(objRow);

            return dtConexion;
        }
        #endregion

        #region Funciones y procedimientos privados
        private void ExtraerDatosCnn(DataTable DatosExtra)
        {
            try
            {
                //if (existeTabla(DatosExtra))
                {
                    DataRow row;
                    // Desenciptar los datos de conexion
                    clsCriptografo Cryp = new clsCriptografo();

                    row = DatosExtra.Rows[0];
                    // Servidor    ==> Campo1
                    // BaseDeDatos ==> Campo2
                    // Usuario     ==> Campo3
                    // Password    ==> Campo4
                    // TipoDBMS    ==> Campo5
                    strServer = Cryp.Desencriptar((string)row["Campo1"].ToString().Trim());
                    strBD = Cryp.Desencriptar((string)row["Campo2"].ToString().Trim());
                    strUser = Cryp.Desencriptar((string)row["Campo3"].ToString().Trim());
                    strPassword = Cryp.Desencriptar((string)row["Campo4"].ToString().Trim());
                    strTipoCnn = Cryp.Desencriptar((string)row["Campo5"].ToString().Trim());
                    NormalizarDatos();
                }
            }
            catch { }
        }

        /// <summary>D:\PROYECTOS .NET 2.0\UpdaterOficinaCentralRegional\Data\clsDatosCliente.cs
        /// Normaliza los datos de conexion, reemplaza el (;) por ("")
        /// </summary>
        public void NormalizarDatos()
        {
            strTipoCnn = strTipoCnn.Replace(";", "").Trim();
            strServer = strServer.Replace(";", "").Trim();
            strBD = strBD.Replace(";", "").Trim();
            strUser = strUser.Replace(";", "").Trim();
            strPassword = strPassword.Replace(";", "").Trim();

            strTipoCnn = "2";
            //strTipoServidor = (TipoServer)strTipoCnn;


            //if (strTipoCnn == "4")
            //{
            //    //strUser = "admin";

            //    if (Fg.Right(strBD, 4).ToUpper() != ".MDB" )
            //        strBD = strBD + ".mdb";
            //}
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
            NormalizarDatos();

            if (strTipoCnn == "1")
            {
                strRegresa = "Driver={PostgreSQL};database=" + strBD + ";server=" + strServer + ";uid=" + strUser + ";pwd=" + strPassword + ";";
                strRegresa = string.Format("Driver={PostgreSQL};database={0};server={1};uid={2};pwd={3};", strBD, strServer, strUser, strPassword);
            }

            if (strTipoCnn == "2")
            {
                strRegresa = "Data Source=" + strServer + "; Initial Catalog=" + strBD + ";user=" + strUser + ";pwd=" + strPassword + ";";
                strRegresa = string.Format("Data Source={0}; Initial Catalog={1}; user={2}; pwd={3}; Connect Timeout={4};", strServer, strBD, strUser, strPassword, ((int)tecTiempoDeEspera).ToString());
            }

            if (strTipoCnn == "3")
            {
                strRegresa = "Driver={MySQL ODBC 3.51 Driver};server=" + strServer + ";Database=" + strBD + ";User=" + strUser + ";Password=" + strPassword + ";"; //Port=3306
                strRegresa = string.Format("Driver={MySQL ODBC 3.51 Driver};server={0};Database={1};User={2};Password={3};", strServer, strBD, strUser, strPassword);
            }

            if (strTipoCnn == "4")
            {
                strRegresa = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strServer + strBD + ";uid=" + strUser + ";pwd=" + strPassword + ";";
                strRegresa = string.Format("Driver={Microsoft Access Driver (*.mdb)};DBQ={0}{1};uid={2};pwd={3};", strServer, strBD, strUser, strPassword);
            }

            return strRegresa;
        }

        private string NombreServidor()
        {
            string sRegresa = strServer;
            int iPos = sRegresa.IndexOf("\\");

            if (iPos != -1)
                sRegresa = Fg.Left(sRegresa, iPos);

            return sRegresa;
        }

        #endregion
    }
}
