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

namespace DllRegistrosSanitarios
{
    public partial class clsConsultasSQLLite
    {
        #region Declaración de variables
        private clsErrorManager error; // = new clsErrorManager();
        private clsLogError errorLog; //  = new clsLogError();
        private bool bUsarCnnRedLocal = true;
        private bool bExistenDatos = false;
        private bool bEjecuto = false;
        private DataSet myDataset; 
        string sQuery = "";
        string sInicio = " ";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false;

        //PRUEBA
        private basGenerales Fg; // = new basGenerales();
        private clsCriptografo Cryp; // = new clsCriptografo();
        ////private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQLite ConexionSql;

        // Consulta ejecutada
        protected string sConsultaExec = "";
        #endregion  Declaración de variables

        #region Constructores de clase y destructor

        public clsConsultasSQLLite(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQLite(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        public clsConsultasSQLLite(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQLite(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        public clsConsultasSQLLite(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQLite(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        public clsConsultasSQLLite(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 

            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQLite(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

        }

        #endregion  Constructores de clase y destructor

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

        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion

        #region Consultas Catalogos

        public DataSet ClavesSSA_Sales(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatClavesSSA_Sales (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Sales(string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = ""; 
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


 
            ////sQuery = sInicio + string.Format(" Select * " + 
            ////    " From CatClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

            sQuery = sInicio + string.Format(" Select Distinct Folio, ClaveSSA, MD5, Descripcion " +
                "   From RegistrosSanitarios_CodigoEAN Where ClaveSSA = '{0}'   ", ClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 

        public DataSet Productos_CodigosEAN(string IdProducto, string Funcion)
        {
            myDataset = new DataSet(); 
            string sMsjError = "Ocurrió un error al obtener los Códigos EAN del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio +
                string.Format(" Select CodigoEAN, ( Case When Status = 'A' Then 1 Else 0 End ) as Status, " +
                " ContenidoPiezasUnitario, ContenidoCorrugado, Cajas_Cama, Cajas_Tarima " +
                " From CatProductos_CodigosRelacionados (NoLock) " + 
                " Where IdProducto = '{0}' ", Fg.PonCeros(IdProducto, 8));


            //sQuery = sInicio + 
            //    string.Format(" Select CodigoEAN, ( Case When Status = 'A' Then 1 Else 0 End ) as Status, 0 as ContenidoCorrugado, 0 as Cajas_Cama, 0 as Cajas_Tarima " +
            //    " From CatProductos_CodigosRelacionados (NoLock) " +
            //    " Where IdProducto = '{0}' ", Fg.PonCeros(IdProducto, 8));
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }   

        public DataSet Productos_RegistrosSanitarios(string IdProducto, string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Registro Sanitario del Producto";
            string sMsjNoEncontrado = "Clave de Registro Sanitario del Producto no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select * " + 
                " From CatProductos_RegistrosSanitarios (NoLock) " + 
                " Where IdProducto = '{0}' And CodigoEAN = '{1}'", IdProducto, CodigoEAN); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RegistrosSanitarios(string Folio, string Funcion)
        {
            return RegistrosSanitarios(Folio, false, Funcion); 
        }

        public DataSet RegistrosSanitarios(string Folio, bool DescargarDocumento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Registro Sanitario";
            string sMsjNoEncontrado = "Clave de Registro Sanitario no encontrado, verifique.";
            int iDescarga = DescargarDocumento ? 1 : 0;

            sQuery = sInicio + string.Format(
                    "Select C.Folio, C.FechaUltimaActualizacion, C.FechaRegistro, C.IdLaboratorio, L.Descripcion As Laboratorio, C.IdClaveSSA_Sal, ClaveSSA, S.Descripcion, \n " +
                    "   C.Consecutivo, C.Tipo, C.Año, C.FechaVigencia, \n" +
                    "   C.IdPaisFabricacion, PF.NombrePais, C.IdPresentacion, P.Descripcion as Descripcion, C.TipoCaducidad, C.Caducidad, \n" +
                    "   (case when {1} = 1 Then Documento else '' end) as Documento, NombreDocto, \n" + 
                    //"   cast('' as varchar(100)) as Documento, NombreDocto, " + 
                    "   C.Status, IsNull(R.Descripcion, 'Desconocido') as StatusRegistroAux \n" +
                    "From CatRegistrosSanitarios C (NoLock) \n" +
                    "Inner Join CatLaboratorios L (NoLock) On ( C.IdLaboratorio = L.IdLaboratorio ) \n" +
                    "Inner Join CatClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) \n" +
                    "Inner Join CatRegistrosSanitarios_PaisFabricacion PF (NoLock) On ( C.IdPaisFabricacion = PF.IdPais  )  \n" +
                    "Inner Join CatRegistrosSanitarios_Presentaciones P (NoLock) On ( C.IdPresentacion = P.IdPresentacion  )  \n" +
                    "Left Join vw_Status_RegistrosSanitarios R (NoLock) On ( C.Status = R.Status ) \n" + 
                    "Where C.Folio =  '{0}'", Fg.PonCeros(Folio, 8), iDescarga); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RegistrosSanitarios_PaisesDeFabricacion(string IdPais, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de Paises de Fabricación";
            string sMsjNoEncontrado = "Clave de Pais de Fabricación no encontrada, verifique.";
            string sFiltro = "";


            if (IdPais != "")
            {
                sFiltro = string.Format("Where IdPais = '{0}' ", Fg.PonCeros(IdPais, 3));
            }

            sQuery = sInicio + string.Format(
                "Select * " + 
                "From CatRegistrosSanitarios_PaisFabricacion (NoLock) " + 
                "{0}", sFiltro );            
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RegistrosSanitarios_Presentaciones(string IdPresentacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de Paises de Fabricación";
            string sMsjNoEncontrado = "Clave de Pais de Fabricación no encontrada, verifique.";
            string sFiltro = "";


            if (IdPresentacion != "")
            {
                sFiltro = string.Format("Where IdPresentacion = '{0}' ", Fg.PonCeros(IdPresentacion, 3));
            }

            sQuery = sInicio + string.Format(
                "Select * " +
                "From CatRegistrosSanitarios_Presentaciones (NoLock) " +
                "{0}", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Consultas Catalogos

        #region Funciones y procedimientos privados
        private DataSet EjecutarQuery(string prtQuery, string NombreTabla)
        {
            clsLeer leer = new clsLeer();
            leer.DataSetClase = EjecutarQuery("Funcion local", prtQuery, "Ocurrió un error al obtener la información", "Información no encontrada");

            if (!leer.SeEncontraronErrores())
            {
                leer.RenombrarTabla(1, NombreTabla); 
            }

            return leer.DataSetClase;
        }

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado )
        {
            clsLeerSQLite Leer = new clsLeerSQLite(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            sConsultaExec = prtQuery;

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
                    {
                        General.msjUser(MensajeNoEncontrado);
                    }
                }
                else
                {
                    dtsResultados = Leer.DataSetClase;
                }
                
            }

            return dtsResultados;
        }

        //////////////private object EjecutarQuery(string prtQuery, string prtTabla)
        //////////////{
        //////////////    object objRetorno = null;
        //////////////    DataSet dtsRetorno = new DataSet("Vacio");
        //////////////    Datos.CadenaDeConexion = strCnnString;

        //////////////    // Si ocurre algun error evitar que traten de accesar un dataset vacio
        //////////////    bExistenDatos = false;
        //////////////    sConsultaExec = prtQuery; 

        //////////////    try
        //////////////    {
        //////////////        if (bUsarCnnRedLocal)
        //////////////        {
        //////////////            objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
        //////////////        }
        //////////////        else
        //////////////        {
        //////////////            //cnnWebServ = new wsConexion.wsControlObras();
        //////////////           // cnnWebServ.Url = General.Url;
        //////////////            //objRetorno = (object)cnnWebServ.ObtenerDataset(Cryp.Encriptar(strCnnString), prtQuery, prtTabla);
        //////////////        }

        //////////////        dtsRetorno = (DataSet)objRetorno;
        //////////////        if (error.ExistenErrores(dtsRetorno))
        //////////////        {
        //////////////            // Buscar en el dataset la tabla de errores                    
        //////////////            myResult = error.MostrarVentanaError(true, false, dtsRetorno);
        //////////////            dtsRetorno = new DataSet("Vacio");
        //////////////            objRetorno = (object)dtsRetorno;
        //////////////        }

        //////////////        bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

        //////////////    }
        //////////////    catch (Exception e)
        //////////////    {
        //////////////        if ( objRetorno != null)
        //////////////            e = (Exception)objRetorno;


        //////////////        dtsRetorno = new DataSet("Vacio");
        //////////////        objRetorno = (object)dtsRetorno;

        //////////////        errorLog = new clsLogError(e);
        //////////////        error = new clsErrorManager(errorLog.ListaErrores);
        //////////////        myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
        //////////////    }

        //////////////    return objRetorno;
        //////////////}

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                if (dtsRevisar.Tables[0].Rows.Count > 0)
                {
                    bRegresa = true;
                }
            }

            return bRegresa;
        }
        #endregion        
    }
}
