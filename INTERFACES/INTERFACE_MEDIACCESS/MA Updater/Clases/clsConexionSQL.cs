using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using MA_Updater.FuncionesGenerales;

using Microsoft.VisualBasic;

namespace MA_Updater.Data
{
    public enum FormatoDeFecha
    {
        Ninguno = 0, 
        YMD = 1, 
        DMY = 2
    }

    /// <summary>
    /// Tiempo de espera para establecer conexion a la base de datos
    /// </summary>
    public enum TiempoDeEspera
    {
        /// <summary>
        /// Tiempo de espera sin limite, especialmente para procesos largos
        /// </summary>
        SinLimite = 0,
        Limite30 = 30,
        Limite60 = 60,
        Limite90 = 90,
        Limite120 = 120,
        Limite150 = 150,
        Limite180 = 180,
        Limite210 = 210,
        Limite240 = 240,
        Limite270 = 270,
        Limite300 = 300
    }

    public class clsConexionSQL
    {
        #region Declaración de variables
        // Variables para acceso adatos
        private string strCnnString = "";
        // private bool bEjecucionSinErrores = false;
        private bool bSeEstaUsandoTransaccion = false;
        private basGenerales Fg = new basGenerales();

        private clsDatosConexion pDatosConexion;
        private Exception exError = new Exception("Sin error");
        private DataSet dtsListaErrores;

        SqlConnection pCnn = new SqlConnection();        
        SqlTransaction pTran;

        private int iTiempoDeEspera = 0; //0==> Sin limite. 360000; //(60 * 60) * 100 ; // 100 Horas
        private TiempoDeEspera tecTiempoDeEspera = TiempoDeEspera.Limite120;
        private TiempoDeEspera teeTimepoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
        private bool bSetCnnString = false;

        private FormatoDeFecha tpFormatoFecha = FormatoDeFecha.YMD;
        // string sFormatFecha = " YMD ";
        string sInicio = " Set DateFormat YMD ";
        #endregion

        #region Constructores de clase y destructor
        public clsConexionSQL()
        {
            pDatosConexion = new clsDatosConexion();
        }        

        public clsConexionSQL(clsDatosConexion DatosCnn)
        {
            strCnnString = DatosCnn.CadenaDeConexion;
            this.pDatosConexion = DatosCnn;
        }        

        //public clsConexionSQL(clsDatosConexion DatosCnn, ref SqlConnection Conexion, ref SqlTransaction Transaccion)
        //{
        //    this.pDatosCnn = DatosCnn;
        //    strCnnString = DatosCnn.CadenaDeConexion;
        //    pCnn = Conexion;
        //    pTran = Transaccion;
        //}

        /// <summary>
        /// Destructor de la clase, liberar memoria.
        /// </summary>
        ~clsConexionSQL()
        {
            try
            {
                // Terminar la transaccion, con exito o con error. 
                //if (bSeEstaUsandoTransaccion)
                {
                    //if (bEjecucionSinErrores)
                    //{
                    //    pTran.Commit();
                    //}
                    //else
                    //{
                    //    pTran.Rollback();
                    //}
                    pTran = null;
                    pCnn.Close();
                    pCnn = null;
                    exError = null;
                }
            }
            catch 
            {
            }

        }
        #endregion

        #region Propiedades publicas
        private string CadenaDeConexion
        {
            get
            {
                if (bSetCnnString)
                    return pDatosConexion.CadenaDeConexion;
                else
                    return "";
            }
        }

        public clsDatosConexion DatosConexion
        {
            get
            {
                return pDatosConexion;
            }
            set
            {
                pDatosConexion = value;
            }
        }

        public bool ConnectionString
        {
            get { return bSetCnnString; }
        }

        public string MensajeError
        {
            get
            {
                return exError.Message;
            }
        }

        public Exception Error
        {
            get
            {
                return exError;
            }
        }

        public FormatoDeFecha FormatoDeFecha
        {
            get { return tpFormatoFecha; }
            set 
            {
                tpFormatoFecha = value;
                switch (tpFormatoFecha)
                {
                    case FormatoDeFecha.YMD:
                        sInicio = " Set DateFormat YMD ";
                        break;

                    case FormatoDeFecha.DMY:
                        sInicio = " Set DateFormat DMY ";
                        break;

                    case FormatoDeFecha.Ninguno:
                        sInicio = "  "; 
                        break; 

                    default:
                        sInicio = " Set DateFormat DMY ";
                        break;
                }
            }
        }

        #endregion

        #region Manejo de conexiones
        public TiempoDeEspera TiempoDeEsperaConexion
        {
            get
            {
                tecTiempoDeEspera = pDatosConexion.ConectionTimeOut;  
                return tecTiempoDeEspera; 
            }
            set
            {
                tecTiempoDeEspera = value;
                pDatosConexion.ConectionTimeOut = value; 
            }
        }

        public TiempoDeEspera TiempoDeEsperaEjecucion
        {
            get
            {
                return teeTimepoDeEsperaEjecucion;
            }
            set
            {
                teeTimepoDeEsperaEjecucion = value;
                iTiempoDeEspera = (int)value;
            }
        }

        public void SetConnectionString()
        {
            // pDatosConexion.ConectionTimeOut = tecTiempoDeEspera;
            pCnn.ConnectionString = pDatosConexion.CadenaDeConexion;
            bSetCnnString = true;
            //pCnn.ConnectionTimeout = (int)tecTiempoDeEspera;
        }

        public bool Abrir()
        {
            bool bRegresa = true;

            try
            {
                PrepararDtsErrores();
                exError = new Exception("Sin error");

                // pDatosConexion.ConectionTimeOut = tecTiempoDeEspera;
                tecTiempoDeEspera = pDatosConexion.ConectionTimeOut; 
                
                pCnn.ConnectionString = pDatosConexion.CadenaDeConexion;
                //pCnn.ConnectionTimeout = (int)tecTiempoDeEspera;
                pCnn.Open();
                bSetCnnString = true;
            }
            catch (Exception e1)
            {
                ObtenerErrores((object)e1);
                exError = e1;
                bRegresa = false;

                try
                {
                    // Asegurar que se cierre la Conexion de Forma Automatica 
                    pCnn.Close(); 
                }
                catch { }
            }

            return bRegresa;
        }

        public bool Cerrar()
        {
            bool bRegresa = true;

            try
            {
                exError = new Exception("Sin error");
                pCnn.Close();
                //pCnn.ConnectionString = "";
            }
            catch (Exception e1)
            {
                exError = e1;
                bRegresa = false;
            }

            return bRegresa;
        }

        #endregion

        #region Métodos para manejo de transacciones
        public void IniciarTransaccion()
        {
            try
            {
                pTran = pCnn.BeginTransaction();
                bSeEstaUsandoTransaccion = true;
                //bEjecucionSinErrores = true;
            }
            catch 
            {
            }

        }

        public void DeshacerTransaccion()
        {
            try
            {
                pTran.Rollback();
                bSeEstaUsandoTransaccion = false;
            }
            catch 
            {
            }
        }

        public void CompletarTransaccion()
        {
            try
            {
                pTran.Commit();
                bSeEstaUsandoTransaccion = false;
                //bEjecucionSinErrores = true;
            }
            catch 
            {
            }
        }
        #endregion

        #region Funciones y procedimientos publicos
        public object Execute(string prtTabla, string prtQuery)
        {
            object objRetorno = null;

            DataSet dtsRetorno = new DataSet();
            SqlCommand pComando = new SqlCommand(sInicio + prtQuery, pCnn);
            SqlDataAdapter adpAdapter = new SqlDataAdapter();

            try
            {
                exError = new Exception("Sin error");
                if (bSeEstaUsandoTransaccion)
                    pComando.Transaction = pTran;

                pComando.CommandType = CommandType.Text;
                // pComando.CommandTimeout = iTiempoDeEspera;
                pComando.CommandTimeout = (int)pDatosConexion.ConectionTimeOut;
                
                adpAdapter.SelectCommand = pComando;
                adpAdapter.Fill(dtsRetorno, prtTabla);

                objRetorno = (object)dtsRetorno;
            }
            catch (Exception e)
            {
                exError = e;
                objRetorno = (object)e;
            }
            finally
            {
                pComando = null;
                adpAdapter = null;
                dtsRetorno = null;
            }

            return objRetorno;
        }


        /// <summary>
        /// Devuelve un dataset con el resultado de ejecutar un Query en el servidor.
        /// </summary>
        /// <param name="prtQuery">Instruccion Sql a ejecutar.</param>
        /// <param name="prtTabla">Nombre de la tabla que se devolvera en el dataset.</param>
        /// <returns>Regresa un dataset si la ejecución fue exitosa ó una excepción en case de errores.</returns>
        public  object ObtenerDataset(string prtQuery, string prtTabla)
        {
            object objRetorno = null;

            DataSet dtsRetorno = new DataSet();
            SqlConnection pCnnLeer = new SqlConnection(strCnnString);
            SqlCommand pComando = new SqlCommand(sInicio + prtQuery, pCnnLeer);
            SqlDataAdapter adpAdapter = new SqlDataAdapter();

            try
            {
                pComando.CommandType = CommandType.Text;
                pCnnLeer.Open();
                adpAdapter.SelectCommand = pComando;

                adpAdapter.Fill(dtsRetorno, prtTabla);
                objRetorno = (object)dtsRetorno;
            }
            catch (Exception e)
            {
                objRetorno = (object)e;
            }
            finally
            {
                pCnnLeer.Close();
                pComando = null;
                adpAdapter = null;
                pCnnLeer = null;
            }

            return objRetorno;
        }

        public object ObtenerFolio(string prtQuery, bool Incrementar, int iLargo)
        {
            object objRetorno = null;
            string sRetorno = "";

            DataSet dtsRetorno = new DataSet();
            SqlCommand pComando = new SqlCommand(sInicio + prtQuery, pCnn);
            SqlDataAdapter adpAdapter = new SqlDataAdapter();

            if (bSeEstaUsandoTransaccion)
            {
                pComando.Transaction = pTran;
            }

            try
            {
                pComando.CommandType = CommandType.Text;
                adpAdapter.SelectCommand = pComando;

                adpAdapter.Fill(dtsRetorno, "Folios");

                if (dtsRetorno.Tables.Count > 0)
                {
                    if (dtsRetorno.Tables["Folios"].Rows.Count > 0)
                    {
                        sRetorno = (string)dtsRetorno.Tables["Folios"].Rows[0][0].ToString();
                        if (Incrementar)
                            sRetorno = Fg.PonCeros(Fg.Str(Fg.Val(sRetorno) + 1), iLargo);
                    }
                    else
                    {
                        sRetorno = Fg.PonCeros(Fg.Str(1), iLargo);
                    }
                }

                objRetorno = sRetorno;

            }
            catch (Exception e)
            {
                objRetorno = (object)e;
            }
            finally
            {
                pComando = null;
                adpAdapter = null;
            }

            return objRetorno;
        }

        // Ultimo
        private object EjecutaInsersionSp(string Sp_Name)
        {
            SqlCommand cmd = new SqlCommand(Sp_Name, pCnn);

            if (bSeEstaUsandoTransaccion)
                cmd.Transaction = pTran;

            try
            {
                cmd.ExecuteNonQuery();
                return cmd;
            }
            catch (Exception Error)
            {
                return Error;
            }
        }
        #endregion

        #region Funciones y procedimientos privados
        public DataSet ListaDeErrores()
        {
            return dtsListaErrores;
        }

        protected void ObtenerErrores(object Error)
        {
            string sMensaje = "", sNumError = "", sSqlEstado = "";

            SqlException mySqlException;
            
            Exception myException;

            PrepararDtsErrores();
            if (Error is SqlException)
            {
                mySqlException = (SqlException)Error;

                foreach (SqlError myError in mySqlException.Errors)
                {
                    sMensaje = myError.Message.Replace("'", "" + Strings.Chr(34) + "");
                    sNumError = myError.Number.ToString().Replace("'", "" + Strings.Chr(34) + "");
                    sSqlEstado = myError.State.ToString().Replace("'", "" + Strings.Chr(34) + "");

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
            dtsListaErrores = new DataSet();
            DataTable dtTabla = new DataTable("Errores");

            dtTabla.Columns.Add("Mensaje", Type.GetType("System.String"));
            dtTabla.Columns.Add("NumError", Type.GetType("System.String"));
            dtTabla.Columns.Add("SqlEstado", Type.GetType("System.String"));

            dtsListaErrores.Tables.Add(dtTabla);

        }
        #endregion Funciones y procedimientos privados
    }
}
