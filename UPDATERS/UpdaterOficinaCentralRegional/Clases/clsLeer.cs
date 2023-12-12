#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using Microsoft.VisualBasic;
#endregion using

namespace UpdaterOficinaCentralRegional.Data
{
    public enum TipoParam
    {
        Cadena = 1, Entero = 2, Flotante = 3, Fecha = 4, Boleano = 5
    }

    public class clsItemLeer
    {
        private string _sNombreParametro = "";
        private object _objValor = "";
        private TipoParam _tpTipoParam = TipoParam.Cadena;
        private string _ReturnCadena = "";

        public clsItemLeer(string Nombre, object Valor, TipoParam Tipo)
        {
            this._sNombreParametro = Nombre;
            this._objValor = Valor;
            this._tpTipoParam = Tipo;
            ArmarValor();
        }

        #region Propiedades
        public string GetConjunto
        {
            get { return _ReturnCadena; }
        }

        public string Nombre
        {
            get { return _sNombreParametro; }
            set { _sNombreParametro = value; }
        }

        public object Valor
        {
            get { return _objValor; }
            set { _objValor = value; }
        }

        public TipoParam Tipo
        {
            get { return _tpTipoParam; }
            set { _tpTipoParam = value; }
        }
        #endregion Propiedades

        #region Funciones y procedimientos
        private void ArmarValor()
        {
            switch (_tpTipoParam)
            {
                case TipoParam.Cadena:
                    _ReturnCadena = "@" + _sNombreParametro + " = '" + GetCadena(_objValor) + "' ";
                    break;

                case TipoParam.Entero:
                    _ReturnCadena = "@" + _sNombreParametro + " = '" + GetEntero(_objValor) + "' ";
                    break;

                case TipoParam.Flotante:
                    _ReturnCadena = "@" + _sNombreParametro + " = '" + GetFlotante(_objValor) + "' ";
                    break;

                case TipoParam.Fecha:
                    _ReturnCadena = "@" + _sNombreParametro + " = '" + GetFecha(_objValor) + "' ";
                    break;

                case TipoParam.Boleano:
                    _ReturnCadena = "@" + _sNombreParametro + " = '" + GetBooleano(_objValor) + "' ";
                    break;
            }
        }

        private string GetCadena(object Valor)
        {
            string sRegresa = "";
            try
            {
                sRegresa = (string)Valor;
            }
            catch { }
            return sRegresa;
        }

        private string GetEntero(object Valor)
        {
            int iRegresa = 0;
            try
            {
                iRegresa = Convert.ToInt32(Valor);
            }
            catch { }
            return iRegresa.ToString();
        }

        private string GetFlotante(object Valor)
        {
            double dbRegresa = 0;
            try
            {
                dbRegresa = Convert.ToDouble(Valor);
            }
            catch { }
            return dbRegresa.ToString();
        }

        private string GetFecha(object Valor)
        {
            string sRegresa = "";
            try
            {
                sRegresa = General.FechaYMD(Convert.ToDateTime(Valor));
            }
            catch { }
            return sRegresa;
        }

        private string GetBooleano(object Valor)
        {
            bool bRegresa = false;
            try
            {
                bRegresa = Convert.ToBoolean(Valor);
            }
            catch { }
            return bRegresa.ToString();
        }

        #endregion Funciones y procedimientos
    }

    public class clsLeerItem
    {
        public string Item = "";
        public object Valor = new object();

        public clsLeerItem()
        {
        }

        public clsLeerItem(string Item, object Valor)
        {
            this.Item = Item;
            this.Valor = Valor;
        }

    }

    public class clsLeerColumna
    {
        public string Nombre = "";
        public object Tipo = "";

        public clsLeerColumna()
        {
        }

        public clsLeerColumna(string Nombre, object Tipo)
        {
            this.Nombre = Nombre;
            this.Tipo = Tipo;
        }
    }

    public class clsLeerError
    {
        public string Mensaje = "";
        public string NumError = "";
        public string SqlEstado = "";

        public clsLeerError(string Mensaje, string NumError, string SqlEstado)
        {
            this.Mensaje = Mensaje;
            this.NumError = NumError;
            this.SqlEstado = SqlEstado;
        }
    }


    public class clsLeer
    {
        #region Declaracion de variables
        protected clsConexionSQL Cnn;
        protected bool bHuboError = false;
        protected Exception myException = new Exception("Sin error");
        protected DataSet dtsListaErrores;

        // Consulta ejecutada
        protected string sConsultaExec = "";

        //Variables protegidas 
        protected DataSet dtsClase = new DataSet();
        protected DataSet dtsPaso = new DataSet();
        protected DataRow dtActual;
        protected bool bExistenDatos = false;
        protected string NombreTabla = "LeerGenerico";
        protected int iRegistros = 0;
        protected int iPosActualReg = 0;
        protected string sUrl = "";
        protected string[] sListaDeColumnas;


        // Lista de columas contenidas en el DataSet "Interfaz"
        private static Dictionary<string, string> ListaColumnas = new Dictionary<string, string>();

        #endregion Declaracion de variables

        #region Constructor
        public clsLeer()
        {
        }

        public clsLeer(ref clsConexionSQL Conexion)
        {
            Cnn = Conexion;
            //if (!Cnn.ConnectionString)
            //    Cnn.SetConnectionString();

            PrepararDtsErrores();
        }
        #endregion

        #region Funciones y procedimientos publicos
        /// <summary>
        /// Ejecuta el query solicitado
        /// </summary>
        /// <param name="Cadena">Query a ejecutar</param>
        /// <returns>bool</returns>
        public virtual bool Exec(string Cadena)
        {
            return Exec(NombreTabla, Cadena);
        }

        /// <summary>
        /// Ejecuta el query solicitado
        /// </summary>
        /// <param name="Tabla">Nombre que se asignara al conjunto de resultados</param>
        /// <param name="Cadena">Query a ejecutar</param>
        /// <returns>bool</returns>
        public virtual bool Exec(string Tabla, string Cadena)
        {
            bool bRegresa = false;
            object objRecibir = null;

            try
            {
                // Asegurar que se establezca esta propiedad
                if (!Cnn.ConnectionString)
                    Cnn.SetConnectionString();

                bHuboError = false;
                sConsultaExec = Cadena;
                myException = new Exception("Sin error");
                objRecibir = Cnn.Execute(Tabla, Cadena);
                dtsClase = (DataSet)objRecibir;
                iPosActualReg = 0;

                try
                {
                    // Ejecucion sin retorno de registros
                    iRegistros = dtsClase.Tables[0].Rows.Count;
                    ObtenerListaColumnas();
                }
                catch
                {
                    iRegistros = 0;
                }

                bRegresa = true;
            }
            catch
            {
                bRegresa = false;
                bHuboError = true;
                myException = (Exception)objRecibir;
                ObtenerErrores(objRecibir);
            }
            return bRegresa;
        }

        public DataSet ListaDeErrores()
        {
            return dtsListaErrores;
        }

        protected void ObtenerErrores(object Error)
        {
            string sMensaje = "", sNumError = "", sSqlEstado = "";

            OdbcException myOdbcException;
            Exception myException;

            PrepararDtsErrores();
            if (Error is OdbcException)
            {
                myOdbcException = (OdbcException)Error;

                foreach (OdbcError myError in myOdbcException.Errors)
                {
                    sMensaje = myError.Message.Replace("'", "" + Strings.Chr(34) + "");
                    sNumError = myError.NativeError.ToString().Replace("'", "" + Strings.Chr(34) + "");
                    sSqlEstado = myError.SQLState.ToString().Replace("'", "" + Strings.Chr(34) + "");

                    object[] obj = { sMensaje, sNumError, sSqlEstado };
                    dtsListaErrores.Tables["Errores"].Rows.Add(obj);
                }

            }
            else if (Error is Exception)
            {
                myException = (Exception)Error;

                sMensaje = myException.Message.Replace("'", "" + Strings.Chr(34) + "");
                sNumError = "0";
                sSqlEstado = "";

                object[] obj = { sMensaje, sNumError, sSqlEstado };
                dtsListaErrores.Tables["Errores"].Rows.Add(obj);
            }
        }

        protected void PrepararDtsErrores()
        {
            dtsListaErrores = new DataSet("ListaErrores");
            DataTable dtTabla = new DataTable("Errores");

            dtTabla.Columns.Add("Mensaje", Type.GetType("System.String"));
            dtTabla.Columns.Add("NumError", Type.GetType("System.String"));
            dtTabla.Columns.Add("SqlEstado", Type.GetType("System.String"));

            dtsListaErrores.Tables.Add(dtTabla);

        }

        public bool SeEncontraronErrores(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            foreach (DataTable dtTabla in dtsRevisar.Tables)
            {
                if (dtTabla.TableName.ToUpper() == "Errores".ToUpper())
                {
                    bHuboError = true;
                    dtsListaErrores = dtsRevisar;
                    bRegresa = true;
                    break;
                }
            }

            return bRegresa;
        }

        public bool ValidarExistenCampos(string []CamposRevisar)
        {
            bool bRegresa = false;

            if (dtsClase != null)
            {
                if (dtsClase.Tables.Count > 0)
                {
                    bRegresa = true;
                    ListaColumnas = new Dictionary<string, string>();

                    foreach(DataColumn dtCol in dtsClase.Tables[0].Columns)
                    {
                        ListaColumnas.Add(dtCol.ColumnName, dtCol.ColumnName);
                    }

                    bRegresa = true;
                    // Revisar que todas las Columnas existan
                    foreach (string Col in CamposRevisar)
                    {
                        if (!ListaColumnas.ContainsKey(Col))
                        {
                            bRegresa = false;
                            break;
                        }
                    }
                    
                }
            }

            return bRegresa;
        }

        /// <summary>
        /// Leé un registro del conjunto de datos almacenado
        /// </summary>
        /// <returns>bool</returns>
        public bool Leer()
        {
            bool bRegresa = false;

            if (iRegistros != 0)
            {
                if ((iPosActualReg + 1) <= iRegistros)
                {
                    dtActual = dtsClase.Tables[0].Rows[iPosActualReg];
                    iPosActualReg++;
                    bRegresa = true;
                }
            }

            bExistenDatos = bRegresa;
            return bRegresa;
        }

        /// <summary>
        /// Devuelve el mensaje del error detectado
        /// </summary>
        public string MensajeError
        {
            get { return myException.Message; }
        }

        /// <summary>
        /// Devuelve el Error detectado
        /// </summary>
        public Exception Error
        {
            get
            {
                if (!bHuboError)
                    myException = new Exception("Sin error");

                return myException;
            }
        }

        public clsLeerError[] Errores
        {
            get
            {
                List<clsLeerError> pListaErrores = null;

                if ( bHuboError )
                    pListaErrores = new List<clsLeerError>();

                try
                {
                    foreach (DataRow row in dtsListaErrores.Tables["Errores"].Rows)
                    {
                        clsLeerError myErr = new clsLeerError(row["Mensaje"].ToString(), row["NumError"].ToString(), row["SqlEstado"].ToString());
                        pListaErrores.Add(myErr);
                    }
                }
                catch { }

                return pListaErrores.ToArray();
            }
        } 

        #endregion

        #region Funciones y Procedimientos privados
        private void ObtenerListaColumnas()
        {
            try
            {
                int iCol = 0;
                sListaDeColumnas = new string[dtsClase.Tables[0].Columns.Count];
                foreach (DataColumn col in dtsClase.Tables[0].Columns)
                {
                    sListaDeColumnas[iCol] = col.ColumnName;
                    iCol++;
                }
            }
            catch
            {
                sListaDeColumnas = null;
            }
        }

        protected bool ValidarURL(string Url)
        {
            bool bRegresa = false;
            Url = Url.Trim();

            if (Url.Trim() == "")
            {
                //throw new ArgumentException("La propiedad URL no puede ser vacia");
                // General.msjError("Error al establecer propiedad", "La propiedad URL no puede ser vacia");
            }
            else
            {
                try
                {
                    string[] myUrl = Url.Split((char)'/');
                    string sHttp = myUrl[0].ToUpper();
                    string sServicio = myUrl[myUrl.Length - 1].ToUpper();
                    string sAsmx = sServicio.Substring(sServicio.Length - 4);

                    if (sHttp.Contains("HTTP:"))
                    {
                        sHttp = sHttp.Substring(0, 4);
                        if (sHttp == "HTTP")
                        {
                            sAsmx = sAsmx.Replace(".", "");
                            if (sAsmx == "ASMX")
                            {
                                this.sUrl = Url;
                                //conexion.Url = this.sUrl;
                                bRegresa = true;
                            }
                        }
                    }
                }
                catch
                {
                    // throw new ArgumentException("La propiedad URL no es válida");
                    // General.msjError("Error al establecer propiedad", "La propiedad URL no es válida");
                }
            }

            if (!bRegresa)
                General.msjError("Error al establecer propiedad", "La propiedad URL no es válida");

            return bRegresa;
        }
        #endregion Funciones y Procedimientos privados

        #region Propiedades publicas
        /// <summary>
        /// Devuelve ó establece la Conexión que utiliza la instancia de la clase
        /// </summary>
        public clsConexionSQL Conexion
        {
            get { return Cnn; }
            set { Cnn = value; }
        }

        public clsDatosConexion DatosConexion
        {
            get { return Cnn.DatosConexion; }
        }

        public int Registros
        {
            get { return iRegistros; }
        }

        /// <summary>
        /// Devuelve ó estable el registro actual del conjunto de datos almacenado. 
        /// Es posible recorrer 1 ó mas veces el conjunto de datos modificando esta propiedad.
        /// </summary>
        public int RegistroActual
        {
            get
            {
                return iPosActualReg;
            }
            set
            {
                iPosActualReg = value-1;
                if (iPosActualReg < 1)
                    iPosActualReg = 0;

            }
        }

        /// <summary>
        /// Devuelve ó establece el conjunto de datos almacenados por la instancia de la clase
        /// </summary>
        public DataSet DataSetClase
        {
            get { return dtsClase.Copy(); }
            set
            {
                try
                {
                    dtsPaso = value;
                    dtsClase = dtsPaso.Copy();
                    iRegistros = dtsClase.Tables[0].Rows.Count;
                    iPosActualReg = 0;
                    ObtenerListaColumnas();
                }
                catch
                {
                    dtsClase = new DataSet();
                    iRegistros = 0;
                    iPosActualReg = 0;
                }
            }
        }

        public DataRow[] DataRowsClase
        {
            get { return dtsClase.Tables[0].Select("1=1"); }
            set
            {
                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        CargarDatosRenglones(value);
                    }
                }
            }
        }

        public DataTable DataTableClase
        {
            get { return dtsClase.Tables[0].Copy(); }
            set
            {
                dtsClase = new DataSet();
                try
                {
                    dtsClase.Tables.Add(value.Copy());
                    iRegistros = dtsClase.Tables[0].Rows.Count;
                    iPosActualReg = 0;
                    ObtenerListaColumnas();
                }
                catch 
                {
                    iRegistros = 0;
                    iPosActualReg = 0; 
                }
            }
        }

        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }

        public string[] Columnas
        {
            get
            {
                string []sLista = null;

                try
                {
                    int iCol = 0;
                    sLista = new string[dtsClase.Tables[0].Columns.Count];

                    foreach (DataColumn col in dtsClase.Tables[0].Columns)
                    {
                        sLista[iCol] = col.ColumnName + " ---- " + col.DataType.Name;
                        iCol++;
                    }
                }
                catch { }

                return sLista;
            }
        }

        private void CargarDatosRenglones(DataRow[] dtRowsClase)
        {
            dtsClase = new DataSet();
            DataTable myTabla = new DataTable("LeerGenerico");
            // DataColumn[] Columnas = dtRowsClase[0].Table.Columns;

            // Obtener la lista de columnas que se cargaran 
            foreach(DataColumn dtCol in dtRowsClase[0].Table.Columns)
            {
                myTabla.Columns.Add(dtCol.ColumnName, dtCol.DataType);
            }

            // Copiar los renglones 
            foreach (DataRow dtRow in dtRowsClase)
            {
                myTabla.Rows.Add(dtRow.ItemArray);
            }

            // Asignar los datos al Dataset de la Clase 
            dtsClase.Tables.Add(myTabla);
            iRegistros = dtsClase.Tables[0].Rows.Count;
            iPosActualReg = 0;
            ObtenerListaColumnas();
        }

        #region Leer datos
        public object[] RowActual
        {
            get
            {
                ArrayList myLista = null;

                if (bExistenDatos)
                {
                    try
                    {
                        myLista = new ArrayList();
                        foreach (string sCol in sListaDeColumnas)
                        {
                            myLista.Add(sCol + " ===> " + dtActual[sCol].ToString());
                        }
                    }
                    catch { }
                }
                return myLista.ToArray();
            } 
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>string</returns>
        public string Campo(string NombreCampo)
        {
            string sRegresa = "";
            if (bExistenDatos)
            {
                try
                {
                    sRegresa = dtActual[NombreCampo].ToString().Trim();
                }
                catch ( Exception ex )
                {
                    myException = ex;
                }
            }
            return sRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>string</returns>
        public string Campo(int PosicionCampo)
        {
            string sRegresa = "";
            if (bExistenDatos)
            {
                try
                {
                    sRegresa = dtActual[PosicionCampo - 1].ToString().Trim();
                }
                catch { }
            }
            return sRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>bool</returns>
        public bool CampoBool(string NombreCampo)
        {
            bool bRegresa = false;

            try
            {
                bRegresa = Convert.ToBoolean(dtActual[NombreCampo]);
            }
            catch //(Exception ex )
            {
                //myException = ex;
            }

            return bRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>bool</returns>
        public bool CampoBool(int PosicionCampo)
        {
            bool bRegresa = false;

            try
            {
                bRegresa = Convert.ToBoolean(dtActual[PosicionCampo - 1]);
            }
            catch { }

            return bRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>int</returns>
        public int CampoInt(string NombreCampo)
        {
            int iRegresa = 0;
            try
            {
                iRegresa = int.Parse(dtActual[NombreCampo].ToString());
                //iRegresa = Convert.ToInt32("0" + dtActual[NombreCampo]);
            }
            catch ( Exception ex )
            {
                ex.Source = ex.Source; 
            }
            return iRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>int</returns>
        public int CampoInt(int PosicionCampo)
        {
            int iRegresa = 0;
            try
            {
                iRegresa = int.Parse(dtActual[PosicionCampo - 1].ToString());
                //iRegresa = Convert.ToInt32("0" + Campo(PosicionCampo));
            }
            catch { }
            return iRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>decimal</returns>
        public decimal CampoDec(string NombreCampo)
        {
            decimal dRegresa = 0;
            try
            {
                dRegresa = Convert.ToDecimal("0" + Campo(NombreCampo));
            }
            catch { }
            return dRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>decimal</returns>
        public decimal CampoDec(int PosicionCampo)
        {
            decimal dRegresa = 0;
            try
            {
                dRegresa = Convert.ToDecimal("0" + Campo(PosicionCampo));
            }
            catch { }
            return dRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>double</returns>
        public double CampoDouble(string NombreCampo)
        {
            double dbRegresa = 0;
            try
            {
                dbRegresa = Convert.ToDouble("0" + Campo(NombreCampo));
            }
            catch { }
            return dbRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>double</returns>
        public double CampoDouble(int PosicionCampo)
        {
            double dbRegresa = 0;
            try
            {
                dbRegresa = Convert.ToDouble("0" + Campo(PosicionCampo));
            }
            catch { }
            return dbRegresa;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="NombreCampo">Nombre la columna que contiene el dato a obtener</param>
        /// <returns>DateTime</returns>
        public DateTime CampoFecha(string NombreCampo)
        {
            DateTime dFecha = DateTime.Now;

            if (bExistenDatos)
            {
                try
                {
                    dFecha = (DateTime)dtActual[NombreCampo];
                }
                catch { }
            }

            return dFecha;
        }

        /// <summary>
        /// Devuelve el valor de una columna del conjunto de datos
        /// </summary>
        /// <param name="PosicionCampo">Posición ordinal de la columna a obtener</param>
        /// <returns>DateTime</returns>
        public DateTime CampoFecha(int PosicionCampo)
        {
            DateTime dFecha = DateTime.Now;

            if (bExistenDatos)
            {
                dFecha = (DateTime)dtActual[PosicionCampo - 1];
            }

            return dFecha;
        }
        #endregion Leer datos
        #endregion Propiedades publicas
    }
}
