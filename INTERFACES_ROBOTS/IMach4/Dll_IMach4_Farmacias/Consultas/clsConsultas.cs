using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4
{
    public class clsConsultas
    {
        #region Declaración de variables
        //private wsConexion.wsControlObras cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();
        private string strCnnString = General.CadenaDeConexion;
        private bool bUsarCnnRedLocal = true, bExistenDatos = false, bEjecuto = false;
        private DataSet myDataset;
        string sQuery = "";
        string sInicio = "Set DateFormat YMD ";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false; 

        private basGenerales Fg = new basGenerales();
        private clsCriptografo Cryp = new clsCriptografo();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;

        #endregion
        
        #region Constructores de clase y destructor
        private clsConsultas()
        {
            bUsarCnnRedLocal = General.ServidorEnRedLocal;
            strCnnString = General.CadenaDeConexion;
        }

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
        }

        #endregion

        #region Propiedades publicas
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool Ejecuto
        {
            get { return bEjecuto; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set { bEsPublicoGeneral = value; }
        } 
        #endregion

        #region Modulos Dll_IMach4
        public DataSet Clientes(string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }

        public DataSet Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet(); 
            return myDataset;
        }   

        public DataSet Farmacias(string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }

        public DataSet Parametros()
        {
            myDataset = new DataSet();

            sQuery = sInicio + " Select Parametro, Valor, Descripcion From Parametros (nolock) " +
                " Where IdSucursal = '" + General.EntidadConectada + "'";
            myDataset = (DataSet)EjecutarQuery(sQuery, "Parametros");

            return myDataset;
        }

        public DataSet Terminales(string IdTerminal, string Funcion)
        {
            myDataset = new DataSet(); 
            return myDataset;
        }

        public DataSet Clientes_Terminales(string IdCliente, string Funcion)
        {
            myDataset = new DataSet(); 
            return myDataset;
        } 

        public DataSet Clientes_Terminales(string IdCliente, string IdTerminal, string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }

        public DataSet Productos_CodigoEAN(string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            return myDataset;
        }
        #endregion Modulos Dll_IMach4

        #region Funciones y procedimientos privados
        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado )
        {
            clsLeer Leer = new clsLeer( ref ConexionSql );
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            Leer.Conexion.SetConnectionString();
            if (!Leer.Exec(prtQuery))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    if (bMostrarMsjLeerVacio)
                        General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    dtsResultados = Leer.DataSetClase;
                }
                
            }

            return dtsResultados;
        }

        private object EjecutarQuery(string prtQuery, string prtTabla)
        {
            object objRetorno = null;
            DataSet dtsRetorno = new DataSet("Vacio");
            Datos.CadenaDeConexion = strCnnString;

            // Si ocurre algun error evitar que traten de accesar un dataset vacio
            bExistenDatos = false;

            try
            {
                if (bUsarCnnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }
                else
                {
                    //cnnWebServ = new wsConexion.wsControlObras();
                   // cnnWebServ.Url = General.Url;
                    //objRetorno = (object)cnnWebServ.ObtenerDataset(Cryp.Encriptar(strCnnString), prtQuery, prtTabla);
                }

                dtsRetorno = (DataSet)objRetorno;
                if (error.ExistenErrores(dtsRetorno))
                {
                    // Buscar en el dataset la tabla de errores                    
                    myResult = error.MostrarVentanaError(true, false, dtsRetorno);
                    dtsRetorno = new DataSet("Vacio");
                    objRetorno = (object)dtsRetorno;
                }

                bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

            }
            catch (Exception e)
            {
                if ( objRetorno != null)
                    e = (Exception)objRetorno;


                dtsRetorno = new DataSet("Vacio");
                objRetorno = (object)dtsRetorno;

                errorLog = new clsLogError(e);
                error = new clsErrorManager(errorLog.ListaErrores);
                myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
            }

            return objRetorno;
        }

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                    bRegresa = true;
            }

            return bRegresa;
        }
        #endregion
    }
}
