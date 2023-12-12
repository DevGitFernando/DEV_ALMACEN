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

using DllRobotDispensador;

namespace DllFarmaciaSoft
{
    public partial class clsConsultas
    {
        #region Declaración de variables
        //private wsConexion.wsControlObras cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error; // = new clsErrorManager();
        private clsLogError errorLog; //  = new clsLogError();
        //private DialogResult myResult = new DialogResult();

        //private DataSet dtsError = new DataSet();
        //private DataSet dtsClase = new DataSet();
        //////private string strCnnString = General.CadenaDeConexion;
        private bool bUsarCnnRedLocal = true;
        private bool bExistenDatos = false;
        private bool bEjecuto = false;
        private DataSet myDataset; 
        string sQuery = "";
        string sInicio = "Set DateFormat YMD \n\n";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false;
        bool bEsClienteIMach = false; //RobotDispensador.Robot.EsClienteInterface; 
        int iLenCodigoEAN = DtGeneral.LargoCodigoEAN; 

        //PRUEBA
        private basGenerales Fg; // = new basGenerales();
        private clsCriptografo Cryp; // = new clsCriptografo();
        ////private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;

        // Consulta ejecutada
        protected string sConsultaExec = "";
        #endregion
        
        #region Constructores de clase y destructor
        ////public clsConsultas()
        ////{
        ////}

        ////public clsConsultas(clsDatosConexion Conexion)
        ////{
        ////    General.msjAviso(Conexion.CadenaConexion);
        ////    this.sNameDll = "";
        ////    this.sPantalla = "";
        ////    this.sVersion = "";
        ////    this.bMostrarMsjLeerVacio = false;

        ////    DatosConexion = Conexion;
        ////    ConexionSql = new clsConexionSQL(Conexion);
        ////    //ConexionSql.SetConnectionString();

        ////    error = new clsErrorManager();
        ////    errorLog = new clsLogError();
        ////    Fg = new basGenerales();
        ////    Cryp = new clsCriptografo();

        ////    bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface; 
        ////}

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla):this(Conexion, DatosApp, Pantalla, true) 
        {
            //////////General.msjAviso(Conexion.CadenaConexion); 
            ////this.sNameDll = DatosApp.Modulo;
            ////this.sPantalla = Pantalla;
            ////this.sVersion = DatosApp.Version;

            ////DatosConexion = Conexion;
            ////ConexionSql = new clsConexionSQL(Conexion);
            //////////ConexionSql.SetConnectionString();

            ////error = new clsErrorManager();
            ////errorLog = new clsLogError();
            ////Fg = new basGenerales();
            ////Cryp = new clsCriptografo();

            ////if (DtGeneral.IdPersonal != "")
            ////{
            ////    bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface;
            ////}

        }

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
            : this(Conexion, DatosApp.Modulo, Pantalla, DatosApp.Version, MostrarMsjLeerVacio) 
        {
            //////////////////General.msjAviso(Conexion.CadenaConexion); 
            ////this.sNameDll = DatosApp.Modulo;
            ////this.sPantalla = Pantalla;
            ////this.sVersion = DatosApp.Version;
            ////this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            ////DatosConexion = Conexion;
            ////ConexionSql = new clsConexionSQL(Conexion);
            //////////////////ConexionSql.SetConnectionString();

            ////error = new clsErrorManager();
            ////errorLog = new clsLogError();
            ////Fg = new basGenerales();
            ////Cryp = new clsCriptografo();

            ////if (DtGeneral.IdPersonal != "")
            ////{
            ////    bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface;
            ////}

        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version):this(Conexion, Modulo, Pantalla, Version, true) 
        {
            //////////////General.msjAviso(Conexion.CadenaConexion); 
            ////this.sNameDll = Modulo;
            ////this.sPantalla = Pantalla;
            ////this.sVersion = Version;

            ////DatosConexion = Conexion;
            ////ConexionSql = new clsConexionSQL(Conexion);
            //////////////ConexionSql.SetConnectionString();

            ////error = new clsErrorManager();
            ////errorLog = new clsLogError();
            ////Fg = new basGenerales();
            ////Cryp = new clsCriptografo();

            ////if (DtGeneral.IdPersonal != "")
            ////{
            ////    bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface;
            ////}

        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 

            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(DatosConexion);
            ConexionSql.TiempoDeEsperaConexion = TiempoDeEspera.Limite120;
            ConexionSql.TiempoDeEsperaEjecucion = TiempoDeEspera.Limite300;

            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

            if (DtGeneral.IdPersonal != "")
            {
                bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface;
            }
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

        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion

        #region Modulos SII 
        #region Consultas Catalogos
        public DataSet Empresas(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "No fue posible obtener el Catálogo de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";

            sQuery = sInicio + " Select *, (IdEmpresa + ' -- ' + NombreCorto) as NombreEmpresa_Cbo From CatEmpresas (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Empresas(string IdEmpresa, string Funcion)
        {
            myDataset = new DataSet();
            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            string sMsjError = "Ocurrió un error al obtener datos de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatEmpresas (NoLock) Where IdEmpresa = '{0}' ", IdEmpresa);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet EmpresasEstados(string Funcion)
        {
            myDataset = new DataSet();
            // IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            string sMsjError = "Ocurrió un error al obtener el Catálogo de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";

            sQuery = sInicio + string.Format(" Select *, ( IdEstado + '--' + NombreEstado ) as NombreEstado_Completo From vw_EmpresasEstados (NoLock) Order by IdEstado ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet EmpresasEstados(string IdEmpresa, string Funcion)
        {
            myDataset = new DataSet();
            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            string sMsjError = "Ocurrió un error al obtener el Catálogo de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_EmpresasEstados (NoLock) Where IdEmpresa = '{0}' ", IdEmpresa);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClasificacionesSSA(string IdClasificacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener Catálogo de Clasificación SSA";
            string sMsjNoEncontrado = "Clave de Clasificación SSA no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatClasificacionesSSA (NoLock) Where IdClasificacion = '" + Fg.PonCeros(IdClasificacion, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Familias(string IdFamilia, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Familia";
            string sMsjNoEncontrado = "Clave de Familia no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatFamilias (NoLock) Where IdFamilia = '" + Fg.PonCeros(IdFamilia, 2) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubFamilias(string IdFamilia, string IdSubFamilia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la SubFamilia";
            string sMsjNoEncontrado = "Clave de SubFamilia no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatSubFamilias (NoLock) Where IdFamilia = '" + Fg.PonCeros(IdFamilia, 2) + "' " +
                               " And IdSubFamilia = '" + Fg.PonCeros(IdSubFamilia, 2) + "' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Segmentos(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Segmento";
            string sMsjNoEncontrado = "Clave de Segmento no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatSegmentosSubFamilias (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Presentaciones(string IdPresentacion, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener  Presentación";
            string sMsjNoEncontrado = "Clave de Presentación no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatPresentaciones (NoLock) Where IdPresentacion = '" + Fg.PonCeros(IdPresentacion, 3) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Laboratorios(string IdLaboratorio, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Laboratorio";
            string sMsjNoEncontrado = "Clave de Laboratorio no encontrada, verifique.";

            sQuery = sInicio + " Select *, Descripcion As Laboratorio " + 
                " From CatLaboratorios (NoLock) " + 
                " Where IdLaboratorio = '" + Fg.PonCeros(IdLaboratorio, 4) + "'"; 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet LaboratoriosPorClaveSSA(string sClaveSSA, string IdLaboratorio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Laboratorio";
            string sMsjNoEncontrado = "Clave de Laboratorio no encontrada, verifique.";

            sQuery = sInicio + string.Format("	Select Distinct L.* From CatLaboratorios L (NoLock) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On (L.IdLaboratorio = P.IdLaboratorio) Where P.ClaveSSA = '{0}' And L.IdLaboratorio = '{1}'",
                    sClaveSSA, Fg.PonCeros(IdLaboratorio, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TipoDeProductos(string IdTipoDeProducto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Tipo de Producto";
            string sMsjNoEncontrado = "Clave de Tipo de Producto no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatTiposDeProducto (NoLock) Where IdTipoProducto = '" + Fg.PonCeros(IdTipoDeProducto, 2) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CondicionesDeAlmacenamiento(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Condiciones de Almacenamiento";
            string sMsjNoEncontrado = "Condiciones de Almacenamiento no encontradas, verifique.";

            sQuery = sInicio + " Select * From CatCondicionesDeAlmacenamiento (NoLock) Where Status = 'A' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }


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
            return ClavesSSA_Sales(ClaveSSA, true, Funcion);
        }

        public DataSet ClavesSSA_Sales(string ClaveSSA, bool BuscarPorClaveSSA, string Funcion)
        {
            return ClavesSSA_Sales(ClaveSSA, BuscarPorClaveSSA, 2, 2, Funcion);
        }

        public DataSet ClavesSSA_Sales___Controlados(string ClaveSSA, bool BuscarPorClaveSSA, string Funcion)
        {
            return ClavesSSA_Sales(ClaveSSA, BuscarPorClaveSSA, 0, 2, Funcion);
        }

        public DataSet ClavesSSA_Sales___Antibioticos(string ClaveSSA, bool BuscarPorClaveSSA, string Funcion)
        {
            return ClavesSSA_Sales(ClaveSSA, BuscarPorClaveSSA, 2, 0, Funcion);
        }

        public DataSet ClavesSSA_Sales(string ClaveSSA, bool BuscarPorClaveSSA, int ClavesDeControlados, int ClavesDeAntibioticos, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = ""; 
            string sFiltro = "";
            string sFiltroControlados = "";
            string sFiltroAntibioticos = ""; 
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            sFiltro = string.Format(" Where IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(ClaveSSA, 6));
            if (BuscarPorClaveSSA)
            {
                sFiltro = string.Format(" Where  ClaveSSA = '{0}' ", ClaveSSA);
            }

            if (ClavesDeControlados != 2)
            {
                sFiltroControlados = string.Format("  and EsControlado = '{0}' ", ClavesDeControlados); 
                sFiltro += sFiltroControlados; 
            }

            if (ClavesDeAntibioticos != 2)
            {
                sFiltroAntibioticos = string.Format("  and EsAntibiotico = '{0}' ", ClavesDeAntibioticos);
                sFiltro += sFiltroAntibioticos;
            }

            ////sQuery = sInicio + string.Format(" Select * " + 
            ////    " From CatClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

            sQuery = sInicio + string.Format("Select *, DescripcionClave as Descripcion \n" +
                "From vw_ClavesSSA_Sales (NoLock)\n   {0}   \n", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Sales_Existencia(string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            ////"Inner Join vw_ClavesSSA_Sales C (NoLock) On (E.IdClaveSSA_Sal = C.IdClaveSSA_Sal) " + 

            sQuery = sInicio + string.Format("" +
                "Select \n\tE.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal As Descripcion, E.ContenidoPaquete, Sum(E.Existencia) As Existencia, E.Presentacion \n" +
                "From vw_ExistenciaPorSales E (NoLock) \n" +
                "Where E.IdEmpresa In ('', '{0}') And E.IdEstado In ('', '{1}') And E.IdFarmacia In ('', '{2}') And E.ClaveSSA = '{3}' \n" +
                "Group By E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal, E.ContenidoPaquete, E.Presentacion \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Existencia_ClavesSSA_Sales(string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";
            
            sQuery = sInicio + string.Format("" +
                "Select E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal As Descripcion, E.ContenidoPaquete, Sum(E.Existencia) As Existencia, E.Presentacion_ClaveSSA as Presentacion " +
                "From vw_ExistenciaPorCodigoEAN_Lotes E (NoLock) " +
                "Where E.IdEmpresa In ('', '{0}') And E.IdEstado In ('', '{1}') And E.IdFarmacia In ('', '{2}') And E.ClaveSSA = '{3}' " +
                "Group By E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal, E.ContenidoPaquete, E.Presentacion_ClaveSSA",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_SalesGrupoTerapeutico(string IdGrupoTerapeutico, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatClavesSSA_Sales (NoLock) " +
                " Where IdGrupoTerapeutico = '{0}' " +
                " Order by Descripcion ", IdGrupoTerapeutico);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_SalesGrupoTerapeutico(string IdGrupoTerapeutico, string Criterio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatClavesSSA_Sales (NoLock) " +
                " Where IdGrupoTerapeutico = '{0}' and Descripcion like '%{1}%' " +
                " Order by Descripcion ", IdGrupoTerapeutico, Criterio.Replace(" ", "%"));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_SalesGrupoTerapeutico(int IdGrupoTerapeutico, string Criterio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatClavesSSA_Sales (NoLock) " +
                " Where Descripcion like '%{1}%' " +
                " Order by Descripcion ", IdGrupoTerapeutico, Criterio.Replace(" ", "%"));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_SalesGrupoTerapeutico(string IdEstado, string IdGrupoTerapeutico, string Clave, string Funcion) 
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatClavesSSA_Sales C (NoLock) " + 
                " Where IdGrupoTerapeutico = '{1}' and IdClaveSSA_Sal <> '{2}' " + 
                "    and Not Exists ( Select * From vw_Relacion_ClavesSSA_Claves x (NoLock) " + 
                "           Where x.IdEstado = '{0}' and x.IdClaveSSA_Relacionada = C.IdClaveSSA_Sal and x.Status = 'A' ) " +
                " Order by Descripcion ", IdEstado, IdGrupoTerapeutico, Clave );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); 

            return myDataset;
        }

        public DataSet ClavesSSA_SalesGrupoTerapeutico(string IdEstado, string IdGrupoTerapeutico, string Clave, string Criterio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";
            string sFiltro_GrupoTerapeutico = "";


            if (IdGrupoTerapeutico != "")
            {
                sFiltro_GrupoTerapeutico = string.Format(" IdGrupoTerapeutico = '{0}' ", IdGrupoTerapeutico ); 
            }

            sQuery = sInicio + string.Format(" Select * From CatClavesSSA_Sales C (NoLock) " +
                " Where {1} and IdClaveSSA_Sal <> '{2}' " +
                "    and Descripcion like '%{3}%' " + 
                "    and Not Exists ( Select * From vw_Relacion_ClavesSSA_Claves x (NoLock) " +
                "           Where x.IdEstado = '{0}' and x.IdClaveSSA_Relacionada = C.IdClaveSSA_Sal and x.Status = 'A' ) " +
                " Order by Descripcion ", IdEstado, sFiltro_GrupoTerapeutico, Clave, Criterio.Replace(" ", "%"));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos(string IdProducto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            //sQuery = sInicio + " Select * From vw_CatProductos (NoLock) Where IdProducto = '" + Fg.PonCeros(IdProducto, 8) + "'";

            sQuery = sInicio + " Select * From vw_Productos (NoLock) " + 
                " Where IdProducto = '" + Fg.PonCeros(IdProducto, 8) + "'";          
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_Estado(string IdProducto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Estados del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            //sQuery = sInicio + " Select * From vw_CatProductos_Estado (NoLock) Where IdProducto = '" + Fg.PonCeros(IdProducto, 8) + "'";
            //myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            // sQuery = sInicio + string.Format(" Select * From vw_CatProductos_Estado (NoLock) " +
            //    " Where ( IdProducto = '{0}' or IdProducto = '' ) Order By Status Desc , IdEstado ", Fg.PonCeros(IdProducto, 8));


            sQuery = sInicio + string.Format(" Exec spp_CatProductos_Estado '{0}' ", Fg.PonCeros(IdProducto, 8)); 
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

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

        public DataSet Productos_CodigosEAN_Datos(string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Códigos EAN del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + " Select * " +
                               "From vw_Productos_CodigoEAN (NoLock) Where CodigoEAN = '" + CodigoEAN + "'";
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_CodigosEAN_Datos(string CodigoEAN, string CodigoEAN_Interno, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Códigos EAN del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            //// right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) 
            sQuery = sInicio + string.Format( " Select * " +
                               "From vw_Productos_CodigoEAN (NoLock) " +
                               " Where ( right(replicate('0', {0}) + CodigoEAN, {0} ) = '{1}' " + 
                               "    or " +
                               " right(replicate('0', {0}) + CodigoEAN_Interno, {0} ) = '{2}' ) ",
                               iLenCodigoEAN, Fg.PonCeros(CodigoEAN, iLenCodigoEAN), Fg.PonCeros(CodigoEAN_Interno, iLenCodigoEAN));
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_CodigosEAN_Datos_Etiquetas(string CodigoEAN, string CodigoEAN_Interno, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Códigos EAN del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            //// right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) 
            sQuery = sInicio + string.Format(
                " Select \n" +
                "   \tP.*, IP.Descripcion as Descripcion_InformacionAdicional_Comercial, \n" +
                "   \tIP.Laboratorio as Laboratorio_InformacionAdicional, \n" +
                "   \tI.Descripcion as Descripcion_InformacionAdicional, I.Concentracion as Concentracion_InformacionAdicional, I.Presentacion as Presentacion_InformacionAdicional \n" +
                "From vw_Productos_CodigoEAN P (NoLock) \n" +
                "Left Join CatClavesSSA_InformacionAdicional I (NoLock) On ( P.IdClaveSSA_Sal = I.IdClaveSSA_Sal ) \n" +
                "Left Join CatProductos_InformacionAdicional IP (NoLock) On ( P.IdProducto = IP.IdProducto ) \n" +
                " Where ( right(replicate('0', {0}) + P.CodigoEAN, {0} ) = '{1}' \n" +
                "    or \n" +
                " right(replicate('0', {0}) + P.CodigoEAN_Interno, {0} ) = '{2}' ) \n",
                iLenCodigoEAN, Fg.PonCeros(CodigoEAN, iLenCodigoEAN), Fg.PonCeros(CodigoEAN_Interno, iLenCodigoEAN));
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
                    "Select C.Folio, C.FechaUltimaActualizacion, C.FechaRegistro, C.IdLaboratorio, L.Descripcion As Laboratorio, C.IdClaveSSA_Sal, S.ClaveSSA, S.Descripcion, \n " +
                    //"   C.Consecutivo, C.Tipo, C.Año, C.FechaVigencia, \n" +
                    " C.FolioRegistroSanitario, \n" +
                    "   C.IdPaisFabricacion, PF.NombrePais, C.IdPresentacion, P.Descripcion as Descripcion, C.TipoCaducidad, C.Caducidad, \n" +
                    "   (case when {1} = 1 Then IsNull(CRI.Documento, '') else '' end) as Documento, C.NombreDocto, \n" + 
                    //"   cast('' as varchar(100)) as Documento, NombreDocto, " + 
                    "   C.Status, IsNull(R.Descripcion, 'Desconocido') as StatusRegistroAux, C.FechaVigencia \n" +
                    "From CatRegistrosSanitarios C (NoLock) \n" + 
                    "Inner Join CatLaboratorios L (NoLock) On ( C.IdLaboratorio = L.IdLaboratorio ) \n" +
                    "Inner Join CatClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) \n" +
                    "Inner Join CatRegistrosSanitarios_PaisFabricacion PF (NoLock) On ( C.IdPaisFabricacion = PF.IdPais  )  \n" +
                    "Inner Join CatRegistrosSanitarios_Presentaciones P (NoLock) On ( C.IdPresentacion = P.IdPresentacion  )  \n" +
                    "Left Join SII_OficinaCentral__RegistrosSanitarios..CatRegistrosSanitarios CRI (NoLock) On ( C.Folio = CRI.Folio )  " + 
                    "Left Join vw_Status_RegistrosSanitarios R (NoLock) On ( C.Status = R.Status ) \n" + 
                    "Where C.Folio =  '{0}'", Fg.PonCeros(Folio, 8), iDescarga); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); 

            return myDataset;
        }

        public DataSet TiposRegistrosSanitarios(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del tipo de Registro Sanitario";
            string sMsjNoEncontrado = "Clave de Tipo de Registro Sanitario no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select * From vw_Status_RegistrosSanitarios C (NoLock) Where Status = 'A'");
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

        public DataSet Regiones(string IdRegion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Región";
            string sMsjNoEncontrado = "Id de Región no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatRegiones (NoLock) Where IdRegion = '" + Fg.PonCeros(IdRegion, 2) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubRegiones(string IdRegion, string IdSubRegion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la SubRegión";
            string sMsjNoEncontrado = "Id de SubRegión no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatSubRegiones (NoLock) Where IdRegion = '" + Fg.PonCeros(IdRegion, 2) + "' " +
                               " And IdSubRegion = '" + Fg.PonCeros(IdSubRegion, 2) + "' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Programas(string IdPrograma, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Programa";
            string sMsjNoEncontrado = "Id de Programa no encontrado, verifique.";

            sQuery = sInicio + " Select *, Descripcion as Programa From CatProgramas (NoLock) Where IdPrograma = '" + Fg.PonCeros(IdPrograma, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubProgramas(string IdPrograma, string IdSubPrograma, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del SubPrograma";
            string sMsjNoEncontrado = "Id de SubPrograma no encontrado, verifique.";

            sQuery = sInicio + " Select *, Descripcion as SubPrograma From CatSubProgramas (NoLock) Where IdPrograma = '" + Fg.PonCeros(IdPrograma, 4) + "' " +
                               " And IdSubPrograma = '" + Fg.PonCeros(IdSubPrograma, 4) + "' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Clientes(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatClientes (NoLock) Where Status = 'A' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Clientes_ClavesSSA(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select Distinct C.* " + 
                " From CatClientes C (NoLock) " +
                " Inner Join CFG_Clientes_Claves S (NoLock) On ( C.IdCliente = S.IdCliente ) " + 
                " Where C.Status = 'A' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select *, Nombre as NombreCliente From CatClientes (NoLock) Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposDeClientes(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Tipos de Cliente";
            string sMsjNoEncontrado = "Tipos de cliente no encontrados, verifique.";

            sQuery = sInicio + " Select * From CatTiposDeClientes (NoLock) Where Status = 'A' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubClientes(string IdCliente, string Funcion)
        {
            return SubClientes(IdCliente, "", Funcion);
        }

        public DataSet SubClientes(string IdCliente, string IdSubCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los SubClientes del Cliente";
            string sMsjNoEncontrado = "Clave de SubCliente no encontrada, verifique.";
            string sWhereSubCliente = "";

            if (IdSubCliente != "")
                sWhereSubCliente = " And IdSubCliente = '" + Fg.PonCeros(IdSubCliente, 4) + "'";

            sQuery = sInicio + " Select IdSubCliente, Nombre, " +
                    " PorcUtilidad, PermitirCapturaBeneficiarios, ImportaBeneficiarios, " +
                    " ( Case When Status = 'A' Then 1 Else 0 End ) as Status, Nombre as NombreSubCliente  " +
                    " From CatSubClientes (NoLock) " +
                    " Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'" + sWhereSubCliente;
            //bMostrarMsjLeerVacio = false;
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubClientes_Ventas(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del SubCliente";
            string sMsjNoEncontrado = "Clave de SubCliente no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatSubClientes (NoLock) Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'";

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Proveedores(string IdProveedor, string Funcion)
        {
            return Proveedores(IdProveedor, false, Funcion); 
        }

        public DataSet Proveedores(string IdProveedor, bool HabilitarProveedorDeReembolso, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Proveedor";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrada, verifique.";
            string sFiltroProveedorReembolso = "";

            sFiltroProveedorReembolso = string.Format(" and IdProveedor >= '0001' "); 
            if (HabilitarProveedorDeReembolso)
            {
                sFiltroProveedorReembolso = string.Format(" and IdProveedor >= '0000' ");
            }

            sQuery = sInicio + string.Format(
                " Select * From vw_Proveedores (NoLock) " +
                " Where IdProveedor = '{0}' {1} ", 
                Fg.PonCeros(IdProveedor, 4), sFiltroProveedorReembolso);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ProveedoresDePedidos(string IdEstado, string IdProveedor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Proveedor";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrada, verifique.";
            string sFiltroProveedorReembolso = "";

            sQuery = sInicio + string.Format(
                "Select P.* " + 
                "From vw_Proveedores P (NoLock) " +
                "Inner Join CFG_CNSGN_Proveedores_SubFarmacias C (NoLock) On ( C.IdEstado = '{1}' and C.IdProveedor = P.IdProveedor ) " + 
                "Where P.IdProveedor = '{0}' ",
                Fg.PonCeros(IdProveedor, 4), IdEstado);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Cat_Proveedores(string Funcion)
        {
            return Cat_Proveedores("", Funcion);
        }

        public DataSet Cat_Proveedores(string IdProveedor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Proveedor";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrado, verifique.";
            string sFiltroProveedor = "";


            if (IdProveedor.Trim() != "")
            {
                sFiltroProveedor = string.Format(" Where IdProveedor = '{0}' ", Fg.PonCeros(IdProveedor, 4));
            }

            sQuery = sInicio + string.Format(
                " Select * From vw_Proveedores (NoLock) " +
                " {0} ",
                sFiltroProveedor);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Estados(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Estado";
            string sMsjNoEncontrado = "Clave de Estado no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select *, (IdEstado + ' -- ' + Nombre) as EstadoNombre " 
                + " From CatEstados (NoLock) Order By IdEstado "); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet EstadosConFarmacias(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Estado";
            string sMsjNoEncontrado = "Clave de Estado no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select Distinct IdEstado, Estado, Estado as Nombre, (IdEstado + ' -- ' + Estado ) as NombreEstado " + 
                " From vw_Farmacias (NoLock) Order by IdEstado ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Estados(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Estado";
            string sMsjNoEncontrado = "Clave de Estado no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatEstados (NoLock) Where IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }

        public DataSet Municipios(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Municipios";
            string sMsjNoEncontrado = "No se encontraron Municipios, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatMunicipios (NoLock) Where IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Municipios(string IdEstado, string IdMunicipio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Municipio";
            string sMsjNoEncontrado = "Clave de Municipio no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatMunicipios (NoLock) Where IdEstado = '{0}' and IdMunicipio = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdMunicipio,4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Colonias(string IdEstado, string IdMunicipio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Colonias";
            string sMsjNoEncontrado = "No se encontraron Colonias, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatColonias (NoLock) Where IdEstado = '{0}' and IdMunicipio = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdMunicipio, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Colonias(string IdEstado, string IdMunicipio, string IdColonia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Colonia";
            string sMsjNoEncontrado = "Clave de Colonia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatColonias (NoLock) Where IdEstado = '{0}' and IdMunicipio = '{1}' and IdColonia = '{2}' ", 
                    Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdMunicipio, 4), Fg.PonCeros(IdColonia,4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string Funcion)
        {
            return Farmacias_Status(Funcion, "A");
        }

        public DataSet Farmacias_Status(string Funcion, string Status)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener datos de las Farmacias";
            string sMsjNoEncontrado = "No se encontraron Farmacias, verifique.";
            string sFiltro = "";

            if (Status.Trim() != "")
            {
                sFiltro = string.Format(" Where Status = '{0}' ", Status) ; 
            }

            sQuery = sInicio + string.Format(" Select *, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia " +
                " From vw_Farmacias (NoLock) " + 
                " {0} " +
                " Order by IdEstado, IdFarmacia ", sFiltro );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "No se encontraron Farmacias para el Estado, verifique.";

            sQuery = sInicio + string.Format(" Select *, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia " + 
                " From vw_Farmacias (NoLock) " + 
                " Where IdEstado = '{0}' and Status = 'A'", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias_Urls(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select F.*, (F.IdFarmacia + ' -- ' + F.Farmacia) as NombreFarmacia,  " +
                " U.UrlFarmacia, C.Servidor " + 
                " From vw_Farmacias F (NoLock) " + 
                " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado And F.IdFarmacia = U.IdFarmacia )  " +
                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                " Where F.IdEstado = '{0}' and F.Status = 'A'", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias_Urls(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "No se encontraron los datos de la Farmacia, verifique.";

            sQuery = sInicio + string.Format(" Select F.*, (F.IdFarmacia + ' -- ' + F.Farmacia) as NombreFarmacia,  " +
                " U.UrlFarmacia, C.Servidor " +
                " From vw_Farmacias F (NoLock) " +
                " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado And F.IdFarmacia = U.IdFarmacia )  " +
                " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                " Where F.IdEstado = '{0}' and F.IdFarmacia = '{1}' And F.Status = 'A'", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_Farmacias (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet AlmacensPermisosEspeciales( string IdEstado, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select C.IdFarmacia, F.Farmacia, FechaVigencia, (case when C.Status = 'A' Then 1 else 0 end) as Status, C.MD5 " +
                " From CFG_Almacenes_PermisosEspeciales  C (NoLock) " + 
                " Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )  " +
                " Where C.IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2) );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_PermisosEspeciales( string IdEstado, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select C.Tipo, '', (case when C.Status = 'A' Then 1 else 0 end) as Status, C.MD5 " +
                " From CFG_CFDI_PermisosEspeciales  C (NoLock) " + 
                " Where C.IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet FarmaciasRelacionadas(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select C.IdFarmaciaRelacionada, FR.Farmacia, (case when C.Status = 'A' Then 1 else 0 end) as Status, C.MD5 " +
                " From CFG_Transferencias_FarmaciasRelacionadas  C (NoLock) " + 
                " Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )  " +
                " Inner Join vw_Farmacias FR (NoLock) On ( C.IdEstado = FR.IdEstado and C.IdFarmaciaRelacionada = FR.IdFarmacia )  " + 
                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet FarmaciasRelacionadas(string IdEstado, string IdFarmacia, string IdFarmaciaRelacionada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Farmacia";
            string sMsjNoEncontrado = "Clave de Farmacia no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select C.* " +
                " From CFG_Transferencias_FarmaciasRelacionadas  C (NoLock) " +
                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.IdFarmaciaRelacionada = '{2}' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(IdFarmaciaRelacionada, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        } 

        public DataSet TipoUnidades(string IdTipoUnidad, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Tipos de Unidades";
            string sMsjNoEncontrado = "No se encontraron Tipos de Unidades, verifique.";

            string sWhereTipoUnidad = "";

            if (IdTipoUnidad != "")
            {
                sWhereTipoUnidad = string.Format(" and IdTipoUnidad = '{0}' ", Fg.PonCeros(IdTipoUnidad, 3));
            }

            sQuery = sInicio + string.Format(" Select *, " +
                " (IdTipoUnidad + ' -- ' + Descripcion) as NombreTipoUnidad " +
                " From CatTiposUnidades (NoLock) " +
                " Where 1 = 1  {0} ", sWhereTipoUnidad);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet NivelesDeAtencion( string IdNivelDeAtencion, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Niveles de Atención";
            string sMsjNoEncontrado = "No se encontraron Niveles de Atención, verifique.";

            string sWhereNivelDeAtencion = "";

            if(IdNivelDeAtencion != "")
            {
                sWhereNivelDeAtencion = string.Format(" and IdNivelDeAtencion = '{0}' ", IdNivelDeAtencion);
            }

            sQuery = sInicio + string.Format(" Select *, " +
                " (cast(IdNivelDeAtencion as varchar(10))+ ' -- ' + Descripcion) as NivelDeAtencion " +
                " From CatUnidadesMedicas_NivelesDeAtencion (NoLock) " +
                " Where 1 = 1  {0} ", sWhereNivelDeAtencion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Jurisdicciones(string Funcion)
        {
            return Jurisdicciones("", "", Funcion);
        }

        public DataSet Jurisdicciones(string IdEstado, string Funcion)
        {
            return Jurisdicciones(IdEstado, "", Funcion);
        }

        public DataSet Jurisdicciones(string IdEstado, string IdJurisdiccion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Jurisdicciones";
            string sMsjNoEncontrado = "No se encontraron Jurisdicciones, verifique.";

            string sWhereEstado = "";
            string sWhereJuris = "";

            if (IdEstado != "")
                sWhereEstado = string.Format(" and IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2));

            if (IdJurisdiccion != "")
                sWhereJuris = string.Format(" and IdJurisdiccion = '{0}' ", Fg.PonCeros(IdJurisdiccion, 3));

            sQuery = sInicio + string.Format(" Select IdEstado, IdJurisdiccion, Descripcion, Descripcion as Jurisdiccion, " + 
                " (IdJurisdiccion + ' -- ' + Descripcion) as NombreJurisdiccion " + 
                " From CatJurisdicciones (NoLock) " +
                " Where 1 = 1  {0}   {1} ", sWhereEstado, sWhereJuris);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet GruposTerapeuticos(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Grupos Terapéuticos";
            string sMsjNoEncontrado = "No se encontrarón Grupos terapéuticos registrados, verifique.";

            sQuery = sInicio + " Select *, (IdGrupoTerapeutico + ' - ' + Descripcion) as DescripcionGrupo From CatGruposTerapeuticos (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet GruposTerapeuticos(string IdGrupo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Grupos terapéuticos";
            string sMsjNoEncontrado = "No se encontrarón Grupos terapéuticos registrados, verifique.";

            sQuery = sInicio + string.Format(" Select *, (IdGrupoTerapeutico + '-' + Descripcion) as DescripcionGrupo " + 
                " From CatGruposTerapeuticos (NoLock) " + 
                " Where IdGrupoTerapeutico = '{0}' ", Fg.PonCeros(IdGrupo,3));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposCatalogoClaves(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Tipos de Catálogo";
            string sMsjNoEncontrado = "No se encontrarón Tipos de Catálogo registrados, verifique.";

            sQuery = sInicio + " Select * From CatTipoCatalogoClaves (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PuestosCEDIS(string Funcion)
        {
            return PuestosCEDIS("", Funcion);
        }

        public DataSet PuestosCEDIS(string IdPuesto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Puesto";
            string sMsjNoEncontrado = "Clave de Puesto no encontrada, verifique.";
            string sWhere = "";

            if (IdPuesto != "")
            {
                sWhere = string.Format(" Where IdPuesto = '{0}'", Fg.PonCeros(IdPuesto, 2));
            }

            sQuery = sInicio + string.Format(" Select *, ( IdPuesto + ' -- ' + Descripcion ) as NombrePuesto " +
                " From CatPuestosCEDIS (NoLock) {0} Order by IdPuesto ", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Rotacion(string Empresa, string Estado, string Farmacia, string IdRotacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la rotación";
            string sMsjNoEncontrado = "No se encontrado información, verifique.";
            IdRotacion = Fg.PonCeros(IdRotacion, 3);

            sQuery = sInicio + string.Format("Select * from vw_CFGC_ALMN__Rotacion (NoLock) " +
                                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdRotacion = '{3}'",
                                Empresa, Estado, Farmacia, IdRotacion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Rutas(string Estado, string Farmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las ruta.";
            string sMsjNoEncontrado = "No se encontrado Rutas, verifique.";

            sQuery = sInicio + string.Format("Select * From CFGC_ALMN__RutaDistribucion (NoLock) Where IdEstado = '{0}' And IdFarmacia = '{1}'",
                                Estado, Farmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Rutas(string Estado, string Farmacia, string IdRuta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ruta.";
            string sMsjNoEncontrado = "No se encontrado información, verifique.";
            IdRuta = Fg.PonCeros(IdRuta, 4);

            sQuery = sInicio + string.Format("Exec spp_RPT_CFGC_ALMN__RutaDistribucion_Transferencia @IdEstado = '{0}', @IdFarmacia = '{1}', @IdRuta = '{2}'",
                                Estado, Farmacia, IdRuta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet PersonalCEDIS(string Funcion)
        {
            return PersonalCEDIS("", "", "", "", Funcion);
        }

        public DataSet PersonalCEDIS(string Empresa, string Estado, string Farmacia, string IdPersonal, string Funcion)
        {
            return PersonalCEDIS(Empresa, Estado, Farmacia, IdPersonal, Puestos_CEDIS.Ninguno, Funcion);
        }

        public DataSet PersonalCEDIS(string Empresa, string Estado, string Farmacia, string IdPersonal, Puestos_CEDIS IdPuesto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Personal";
            string sMsjNoEncontrado = "Clave de Personal no encontrada, verifique.";
            string sWhere = "";

            if (IdPersonal != "")
            {
                sWhere = string.Format(" and IdPersonal = '{0}'", Fg.PonCeros(IdPersonal, 4));
            }


            if (IdPuesto != Puestos_CEDIS.Ninguno)
            {
                sWhere += string.Format(" and IdPuesto = '{0}'", Fg.PonCeros((int)IdPuesto, 2));

                if(IdPuesto == Puestos_CEDIS.InventariosCiclicos)
                {
                    sWhere = string.Format(" and IdPersonal_Relacionado = '{0}'", Fg.PonCeros(IdPersonal, 4));
                    sWhere += string.Format(" and IdPuesto = '{0}'", Fg.PonCeros((int)IdPuesto, 2));
                }
            }

            sQuery = sInicio + string.Format(" Select * From vw_PersonalCEDIS (NoLock) Where IdEmpresa = '{0}' and IdEstado = '{1}' " +
                                            " and IdFarmacia = '{2}'  {3} Order by IdPersonal ", Empresa, Estado, Farmacia, sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Vehiculos(string IdVehiculo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Personal";
            string sMsjNoEncontrado = "No se encontrado información, verifique.";
            IdVehiculo = Fg.PonCeros(IdVehiculo, 8);

            sQuery = sInicio + string.Format("Select * from vw_Vehiculos (NoLock) Where IdEstado = '{0}' And Idfarmacia = '{1}' And IdVehiculo = '{2}'",
                        DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, IdVehiculo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioCartas(string Empresa, string Estado, string Farmacia, string FolioCarta, bool EsAlmacenRelacionado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del movimiento";
            string sMsjNoEncontrado = "No se encontro el movimiento en una carta canje, verifique.";
            FolioCarta = Fg.PonCeros(FolioCarta, 8);

            sQuery = sInicio + string.Format(
                "Select * \n" +
                "From RutasDistribucionDet_CartasCanje (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioCarta = '{3}' \n",
            Empresa, Estado, Farmacia, FolioCarta);


            if(!EsAlmacenRelacionado)
            {
                myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);
            }
            else
            {
                myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);
            }


            return myDataset;
        }

        public DataSet TiposDeDerechoHabiencias( string IdTipoDerechoHabiencia, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Personal";
            string sMsjNoEncontrado = "No se encontrado información, verifique.";
            string sFiltro = "";

            if(IdTipoDerechoHabiencia != "")
            {
                sFiltro = string.Format("Where IdTipoDerechoHabiencia = '{0}' ", Fg.PonCeros(IdTipoDerechoHabiencia, 3));
            }

            sQuery = sInicio + string.Format(
                "Select *, ( IdTipoDerechoHabiencia + ' - ' + Descripcion ) as DerechoHabiencia \n" +
                "From CatTiposDeDerechohabiencia (NoLock) \n" +
                "{0}\nOrder by Descripcion \n", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposDeIdentificaciones( string IdTipoDeIdentificacion, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Tipos de Identificaciones.";
            string sMsjNoEncontrado = "No se encontrado información, verifique.";
            string sFiltro = "";

            if(IdTipoDeIdentificacion != "")
            {
                sFiltro = string.Format("Where IdTipoDeIdentificacion = '{0}' ", Fg.PonCeros(IdTipoDeIdentificacion, 3));
            }

            sQuery = sInicio + string.Format(
                "Select *, ( IdTipoDeIdentificacion + ' - ' + Descripcion ) as TipoDeIdentificacion \n" +
                "From CatTiposDeIdentificaciones (NoLock) \n" +
                "{0}\nOrder by Descripcion \n", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #region Llenado de Combos

        public DataSet ComboCliente(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el catálogo de Clientes";
            string sMsjNoEncontrado = "No existen Clientes, verifique.";

            sQuery = sInicio + string.Format(" Select IdCliente, (IdCliente + ' - ' + NombreCliente) As NombreCliente From vw_Clientes (NoLock) Order By IdCliente ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet ComboSubCliente(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el catálogo de Sub-Cliente";
            string sMsjNoEncontrado = "No existen Sub-Clientes, verifique.";

            sQuery = sInicio + string.Format(" Select IdSubCliente, (IdSubCliente + ' - ' + NombreSubCliente) As NombreSubCliente From vw_Clientes_SubClientes (NoLock) " +
                    "Where IdCliente = '{0}' Order By IdCliente ", IdCliente);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboClasificacionesSSA(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Clasificaciones SSA";
            string sMsjNoEncontrado = "No existen Clasificaciones SSA registradas, verifique.";

            sQuery = sInicio + " Select * From CatClasificacionesSSA (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboFamilias(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Familias";
            string sMsjNoEncontrado = "No existen Familias registradas, verifique.";

            sQuery = sInicio + " Select * From CatFamilias (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboSubFamilias(string IdFamilia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las SubFamilias";
            string sMsjNoEncontrado = "No existen SubFamilias registradas para la Familia seleccionada, verifique.";

            sQuery = sInicio + "Select IdSubFamilia, Descripcion From CatSubFamilias (NoLock) " +
                               "Where IdFamilia = '" + Fg.PonCeros(IdFamilia, 2) + "' " +
                               "And Status = 'A' Order By Descripcion ";

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboPresentaciones(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Presentaciones";
            string sMsjNoEncontrado = "No existen Presentaciones registradas, verifique.";

            sQuery = sInicio + " Select * From CatPresentaciones (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboLaboratorios(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Laboratorios";
            string sMsjNoEncontrado = "No existen Laboratorios registrados, verifique.";

            sQuery = sInicio + " Select * From CatLaboratorios (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboTiposDeProducto(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Tipos de Producto";
            string sMsjNoEncontrado = "No existen Tipos de Producto registrados, verifique.";

            sQuery = sInicio + " Select * From CatTiposDeProducto (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboClavesSSASales(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Claves SSA Sales";
            string sMsjNoEncontrado = "No existen Claves SSA Sales registradas, verifique.";

            sQuery = sInicio + " Select * From CatClavesSSA_Sales (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboRegiones(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Regiones";
            string sMsjNoEncontrado = "No existen Regiones, verifique.";

            sQuery = sInicio + " Select * From CatRegiones (NoLock) Where Status = 'A' Order By Descripcion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboSubRegiones(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las SubRegiones";
            string sMsjNoEncontrado = "No existen SubRegiones, verifique.";

            sQuery = sInicio + " Select * From CatSubRegiones (NoLock) Where Status = 'A' Order By IdSubRegion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboSubRegiones(string Funcion, string sRegion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las SubRegiones";
            string sMsjNoEncontrado = "No existen SubRegiones, verifique.";

            sQuery = sInicio + " Select * From CatSubRegiones (NoLock) Where IdRegion = '" + Fg.PonCeros(sRegion, 2) + "' AND Status = 'A' Order By IdSubRegion";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboEstados(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Estados";
            string sMsjNoEncontrado = "No existen Estados, verifique.";

            //// sQuery = sInicio + " Select * From CatEstados (NoLock) Where Status = 'A' Order By IdEstado";
            sQuery = sInicio + string.Format(" Select *, (IdEstado + ' -- ' + Nombre) as EstadoNombre "
                + " From CatEstados (NoLock) Where Status = 'A' Order By IdEstado ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboMunicipios(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Municipios";
            string sMsjNoEncontrado = "No existen Municipios registrados para el Estado seleccionado, verifique.";

            sQuery = sInicio + "Select IdMunicipio, Descripcion From CatMunicipios (NoLock) " +
                               "Where IdEstado = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                               "And Status = 'A' Order By Descripcion ";

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboColonias(string IdEstado, string sIdMunicipio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Colonias";
            string sMsjNoEncontrado = "No existen Colonias registradas para el Municipio seleccionado, verifique.";

            sQuery = sInicio + "Select IdColonia, Descripcion From CatColonias (NoLock) " +
                               "Where IdEstado = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                               "And IdMunicipio = '" + Fg.PonCeros(sIdMunicipio, 4) + "' " +
                               "And Status = 'A' Order By Descripcion ";

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboFarmacias(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las Farmacias";
            string sMsjNoEncontrado = "No existen Farmacias, verifique.";

            sQuery = sInicio + " Select *, ( IdFarmacia + ' -- ' + NombreFarmacia ) as NombreFarmacia_Cbo From CatFarmacias (NoLock) Order By IdFarmacia";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ComboProgramas(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Programas";
            string sMsjNoEncontrado = "No existen Programas, verifique.";

            sQuery = sInicio + " Select *, ( IdPrograma + ' -- ' + Descripcion ) as NombrePrograma " + 
                " From CatProgramas (NoLock) Order By IdPrograma";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Llenado de Combos


        public DataSet Parametros()
        {
            myDataset = new DataSet();

            sQuery = sInicio + " Select Parametro, Valor, Descripcion From Parametros (nolock) " +
                " Where IdSucursal = '" + General.EntidadConectada + "'";
            myDataset = (DataSet)EjecutarQuery(sQuery, "Parametros");

            return myDataset;
        }
        #endregion Consultas Catalogos

        #region Ventas

        public DataSet FormaDePago(string idFormasDePago, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las Fomas de pago.";
            string sMsjNoEncontrado = "Clave de Foma de pago no encontrada, verifique.";

            idFormasDePago = Fg.PonCeros(idFormasDePago, 2);

            sQuery = sInicio + string.Format(
                " SELECT *  FROM CatFormasDepago (NoLock) " +
                " Where IdFormasDePago <> '00' and idFormasDePago = '{0}'", idFormasDePago);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Producto_Ventas(string IdCodigo, string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + " Exec Spp_ProductoVentasFarmacia '" + Fg.PonCeros(IdCodigo, iLenCodigoEAN) + "', " +
                               "'" + IdCodigo.Trim() + "', " + "'" + Fg.PonCeros(IdEstado, 2) + "'";

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioEnc_Ventas(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Venta no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVenta = Fg.PonCeros(FolioVenta, 8);

            sQuery = sInicio + string.Format(" SELECT *  FROM vw_VentasEnc (NoLock) " +
                " WHERE IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ", 
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDet_Ventas(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Venta no encontrado, verifique.";

            //sQuery = sInicio + string.Format( "Select VD.CodigoEAN, VD.IdProducto, P.Descripcion, VD.TasaIva, VD.CantidadVendida, VD.PrecioUnitario " +
            //    " From VentasDet VD (NoLock) " +
            //    " Inner Join CatProductos P (NoLock) On ( VD.IdProducto = P.IdProducto ) " +
            //    " Where Vd.IdEmpresa = '{0}' and VD.IdEstado = '{1}' and VD.IdFarmacia  = '{2}' and VD.FolioVenta = '{3}' " +
            //    " Order By VD.Renglon ", IdEmpresa, IdEstado, IdFarmacia, FolioVenta );

            sQuery = sInicio + string.Format("Select CodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, Importe " +
                " From vw_VentasDet_CodigosEAN (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia  = '{2}' and Folio = '{3}' " +
                " Order By Renglon ", IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDetLotes_Ventas(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "FechaReg, FechaCad, Status, Existencia, Cantidad \n" +
                "From vw_VentasDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioEnc_VentasDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Venta no encontrado, verifique.";

            sQuery = sInicio + string.Format(" SELECT * FROM vw_VentasEnc (NoLock) " +
                " WHERE IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDet_VentasDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Venta no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select CodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, 0 as CantidadDevuelta, Importe " +
                " From vw_VentasDet_CodigosEAN (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia  = '{2}' and Folio = '{3}' " +
                " Order By Renglon ", IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDetLotes_VentasDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            // Para el manejo de los lotes es Necesario pasar los nombre de columnas exactos 
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, \n" +
                "\tCantidad as Existencia, \n" +
                "\tCantidad as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_VentasDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet VentaDispensacion_InformacionAdicional(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información.";
            string sMsjNoEncontrado = "Información de Venta no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVenta = Fg.PonCeros(FolioVenta, 8);

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_VentasDispensacion_InformacionAdicional (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet VerificaPrecio(int Tipo, string IdEmpresa, string IdEstado, string IdFarmacia, string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el precio del producto ";
            string sMsjNoEncontrado = "Código EAN no encontrado.";

            sQuery = sInicio + string.Format(" Exec spp_Mtto_VerificadorDePrecios '{0}', '{1}', '{2}', '{3}', '{4}' ", Tipo, IdEmpresa, IdEstado, IdFarmacia, CodigoEAN);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet VerificaPrecioSales(int Tipo, string IdEmpresa, string IdEstado, string IdFarmacia, string IdClaveSSA_Sal, string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el precio del producto ";
            string sMsjNoEncontrado = "Clave SSA no encontrado.";

            sQuery = sInicio + string.Format(" Exec spp_Mtto_VerificadorDePrecios_Sales '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
                Tipo, IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Sal, CodigoEAN);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RemisionesEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioRemision, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Remisión";
            string sMsjNoEncontrado = "Folio de Venta no encontrado. Verifique que Folio ingresado sea de Crédito";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioRemision = Fg.PonCeros(FolioRemision, 8);

            sQuery = sInicio + string.Format(" Select * From vw_RemisionesEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And FolioRemision = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioRemision);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Ventas_ObtenerVale(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Venta no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVenta = Fg.PonCeros(FolioVenta, 8);

            sQuery = sInicio + string.Format(" Select * From vw_Vales_EmisionEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And FolioVenta = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #region Pedidos Especiales Almacen
        public DataSet PedidosEspeciales_ClavesSSA_Existencia( string ClaveSSA, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            ////"Inner Join vw_ClavesSSA_Sales C (NoLock) On (E.IdClaveSSA_Sal = C.IdClaveSSA_Sal) " + 

            sQuery = sInicio + string.Format("\n" +
                "Select \n\tL.IdClaveSSA_Sal, L.ClaveSSA, L.DescripcionSal As Descripcion, L.ContenidoPaquete_ClaveSSA as ContenidoPaquete, \n" +
                "\tSum(Existencia - ( ExistenciaEnTransito + ExistenciaSurtidos ) ) As Existencia, L.Presentacion_ClaveSSA as Presentacion \n" +
                "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) \n" +
                "Inner Join vw_Productos_CodigoEAN L (NoLock) On ( E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN )  \n" + 
                "Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = '{2}' And L.ClaveSSA = '{3}' \n" +
                " 		And Not Exists \n" +
                " 		( \n" +
                " 		    select * \n" + 
                " 		    From Pedidos__Ubicaciones_Excluidas_Surtimiento P(NoLock) \n" +
                " 		    Where E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia \n" +
                " 		          and E.IdPasillo = P.IdPasillo and E.IdEstante = P.IdEstante and E.IdEntrepaño = P.IdEntrepaño And P.Excluida = 1 \n" +
                " 		) \n" +
                "Group By L.IdClaveSSA_Sal, L.ClaveSSA, L.DescripcionSal, L.ContenidoPaquete_ClaveSSA,  L.Presentacion_ClaveSSA \n",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Ventas_EDM_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del encabezado";
            string sMsjNoEncontrado = "El encabezado del Folio no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select IdCliente , NombreCliente, IdSubCliente, NombreSubCliente " +
                    "From vw_Ventas_EDM_Enc S (NoLock) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ", IdEmpresa, IdEstado, IdFarmacia, Folio);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Ventas_EDM_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles";
            string sMsjNoEncontrado = "Detalles del Folio no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select S.CodigoEAN, S.IdProducto, DescripcionCorta, S.TasaIva, Sum(S.CantidadVendida) As Cantidad, Cast(0 As int) As Importe " +
                    "From Ventas_EDM_Det S(NoLock) " +
                    "Inner Join vw_Productos_CodigoEAN P (NoLock) On (S.CodigoEAN = P.CodigoEAN) " +
                    "Where CantidadVendida > 0 And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                    "Group By S.CodigoEAN, S.IdProducto, DescripcionCorta, S.TasaIva",
                    IdEmpresa, IdEstado, IdFarmacia, Folio);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Ventas_EDM_Det_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información.";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select S.IdSubFarmacia, SF.Descripcion As SubFarmacia, S.IdProducto, S.CodigoEAN, S.ClaveLote, " +
                "datediff(mm, getdate(), FP.FechaCaducidad) as MesesCad, Fp.FechaRegistro As FechaReg, FP.FechaCaducidad As FechaCad, " +
                "S.Status, Cast(Existencia As int) As Existencia, Cast(Sum(CantidadVendida) As int) As Cantidad " +
                "From Ventas_EDM_Det_Lotes S (NoLOck) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On (S.CodigoEAN = P.CodigoEAN) " +
                "Inner Join CatEstados_SubFarmacias SF (NoLock) On (S.IdEstado = SF.IdEstado And S.IdSubFarmacia = SF.IdSubFarmacia) " +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes FP (NoLock) " +
                "	On (S.IdEmpresa = FP.IdEmpresa And S.IdEstado = FP.IdEstado And S.IdFarmacia = FP.IdFarmacia And " +
                "		S.IdSubFarmacia = FP.IdSubFarmacia ANd S.ClaveLote = FP.ClaveLote And S.CodigoEAN = FP.CodigoEAN) " +
                "Where CantidadVendida > 0 And S.IdEmpresa = '{0}' And S.IdEstado = '{1}' And S.IdFarmacia = '{2}' And Folio = '{3}'  " +
                " Group By S.IdSubFarmacia, SF.Descripcion, S.IdProducto, S.CodigoEAN, S.ClaveLote, FP.FechaCaducidad, Fp.FechaRegistro, S.Status, Existencia"
                , IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet PedidosEspeciales_GenerarVentaEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del encabezado del surtido";
            string sMsjNoEncontrado = "El encabezado del Folio de surtido no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tDistinct(IdCliente) as IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, \n" +
                "\tIdPrograma, Programa, IdSubPrograma, SubPrograma, IdBeneficiario, ReferenciaInternaPedido, IdSocioComercial, IdSucursal   \n" +
                "From vw_Impresion_Pedidos_Cedis S (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio in ({3}) \n", 
                IdEmpresa, IdEstado, IdFarmacia, FolioPedido); 

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEspeciales_GenerarVenta(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioSurtido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del surtido";
            string sMsjNoEncontrado = "Detalles del Folio de surtido no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select S.CodigoEAN, S.IdProducto, DescripcionCorta, TasaIva, Sum(CantidadAsignada) As Cantidad, PrecioMaxPublico, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', S.IdProducto) As Precio " +
                    "From Pedidos_Cedis_Det_Surtido_Distribucion S (NoLOck) " +
                    "Inner Join vw_Productos_CodigoEAN P (NoLock) On (S.CodigoEAN = P.CodigoEAN) " +
                    "Where CantidadAsignada > 0 And IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioSurtido in ({3}) " +
                    "Group By S.CodigoEAN, S.IdProducto, DescripcionCorta, TasaIva, PrecioMaxPublico", IdEmpresa, IdEstado, IdFarmacia, FolioSurtido);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEspeciales_GenerarVenta_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioSurtido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de surtido.";
            string sMsjNoEncontrado = "Folio de surtido no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select S.IdSubFarmacia, SF.Descripcion As SubFarmacia, S.IdProducto, S.CodigoEAN, S.SKU, S.ClaveLote, " +
                "datediff(mm, getdate(), FP.FechaCaducidad) as MesesCad, Fp.FechaRegistro As FechaReg, FP.FechaCaducidad As FechaCad, S.Status, " +
                "Cast((Existencia - ExistenciaEnTransito) As int) As Existencia, Cast((Existencia - ExistenciaEnTransito) As int) As ExistenciaDisponible, Sum(CantidadAsignada) As Cantidad " +
                "From Pedidos_Cedis_Det_Surtido_Distribucion S (NoLOck) " +
                "Inner Join vw_Productos_CodigoEAN P (NoLock) On (S.CodigoEAN = P.CodigoEAN) " +
                "Inner Join CatEstados_SubFarmacias SF (NoLock) On (S.IdEstado = SF.IdEstado And S.IdSubFarmacia = SF.IdSubFarmacia) " +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes FP (NoLock) " +
                "	On (S.IdEmpresa = FP.IdEmpresa And S.IdEstado = FP.IdEstado And S.IdFarmacia = FP.IdFarmacia And " +
                "		S.SKU = FP.SKU and S.IdSubFarmacia = FP.IdSubFarmacia ANd S.ClaveLote = FP.ClaveLote And S.CodigoEAN = FP.CodigoEAN) " +
                "Where CantidadAsignada > 0 And S.IdEmpresa = '{0}' And S.IdEstado = '{1}' And S.IdFarmacia = '{2}' And FolioSurtido in ({3})  " +
                " Group By S.IdSubFarmacia, SF.Descripcion, S.IdProducto, S.CodigoEAN, S.SKU, S.ClaveLote, FP.FechaCaducidad, Fp.FechaRegistro, S.Status, Existencia, ExistenciaEnTransito"
                , IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioSurtido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet PedidosEspeciales_GenerarVenta_Lotes_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioSurtido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Surtido.";
            string sMsjNoEncontrado = "Folio de Surtido no encontrado, verifique.";

            sQuery = sInicio + string.Format("	" +
                   " Select S.IdSubFarmacia, S.IdProducto, S.CodigoEAN, S.SKU, S.ClaveLote, S.IdPasillo, S.IdEstante, S.IdEntrepaño as IdEntrepano, S.Status " +
                   ", Cast((Existencia - ExistenciaEnTransito) As int) As Existencia, Cast((Existencia - ExistenciaEnTransito) As int) As ExistenciaDisponible, Sum(CantidadAsignada) As Cantidad " +
                   "From Pedidos_Cedis_Det_Surtido_Distribucion S (NoLOck) " +
                   "Inner Join vw_Productos_CodigoEAN P (NoLock) On (S.CodigoEAN = P.CodigoEAN) " +
                   "Inner Join CatEstados_SubFarmacias SF (NoLock) On (S.IdEstado = SF.IdEstado And S.IdSubFarmacia = SF.IdSubFarmacia) " +
                   "Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones FP (NoLock) " +
                   "	On (S.IdEmpresa = FP.IdEmpresa And S.IdEstado = FP.IdEstado And S.IdFarmacia = FP.IdFarmacia And " +
                   "        S.SKU = FP.SKU and S.IdSubFarmacia = FP.IdSubFarmacia ANd S.ClaveLote = FP.ClaveLote And S.CodigoEAN = FP.CodigoEAN And " +
                   "        S.IdPasillo = Fp.IdPasillo And S.IdEstante = Fp.IdEstante And S.IdEntrepaño = Fp.IdEntrepaño) " +
                   "Where CantidadAsignada > 0 And S.IdEmpresa = '{0}' And S.IdEstado = '{1}' And S.IdFarmacia = '{2}' And FolioSurtido in ({3}) " +
                   "Group By S.IdSubFarmacia, S.IdProducto, S.CodigoEAN, S.SKU, S.ClaveLote, S.IdPasillo, S.IdEstante, S.IdEntrepaño, S.Status, Existencia, ExistenciaEnTransito ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioSurtido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Pedidos Especiales Almacen
        #endregion Ventas

        #region Consultas Compras
        public DataSet Compras_Clientes(string IdEstado, string IdCliente, string IdSubCliente, string Funcion)
        {
            // Se muestran solo los clientes de Credito registrados para la Farmacia 

            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Clientes";
            string sMsjNoEncontrado = "Información de Cliente no encontrada, verifique.";
            string sWhere = "";
            string sWherePubGral = "";


            IdCliente = Fg.PonCeros(IdCliente, 4);
            sQuery = sInicio + string.Format(" Select distinct IdCliente, NombreCliente " + 
                 " From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                 " Where IdEstado = '{0}' and IdCliente = '{1}' and StatusRelacion = 'A' {2}  ",
                 IdEstado, IdCliente, sWhere);

            if (IdSubCliente != "")
            {
                sMsjError = "Ocurrió un error al obtener los datos de Sub-Clientes";
                sMsjNoEncontrado = "Información de Sub-Cliente no encontrada, verifique.";
                sWhere = string.Format(" and IdSubCliente = '{0}' ", Fg.PonCeros(IdSubCliente, 4));

                sQuery = sInicio + string.Format(" Select distinct IdSubCliente, NombreSubCliente  " + 
                    "From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                    " Where IdEstado = '{0}' and IdCliente = '{1}' and StatusRelacion = 'A' {2}  ",
                     IdEstado, IdCliente, sWhere);
            }

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Producto_Compras(string IdCodigo, string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + " Exec Spp_ProductoComprasFarmacia '" + Fg.PonCeros(IdCodigo, iLenCodigoEAN) + "', " +
                               "'" + IdCodigo.Trim() + "', " + "'" + Fg.PonCeros(IdEstado, 2) + "'";

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string FolioEnc_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra)
        {
            sQuery = sInicio + string.Format(" Select * From vw_ComprasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioCompra, 8));
            return sQuery; 
        } 

        public DataSet FolioEnc_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Compra no encontrado, verifique.";

            ////sQuery = sInicio + string.Format(" Select * From vw_ComprasEnc (NoLock) " +
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ", 
            ////    IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioCompra,8) );

            sQuery = FolioEnc_Compras(IdEmpresa, IdEstado, IdFarmacia, FolioCompra); 

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string FolioDet_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra)
        {
            sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
                " CD.TasaIva, CD.CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " +
                " CD.Costo, 0 as Importe " +
                " From vw_ComprasDet_CodigosEAN CD (NoLock) " +
                " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioCompra);
            return sQuery; 
        }

        public DataSet FolioDet_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Compra no encontrado, verifique.";

            ////  - CD.Cant_Devuelta 
            //sQuery = sInicio + string.Format( " Select CD.CodigoEAN, CD.IdProducto, P.Descripcion, CD.TasaIva, CD.Cant_Recibida, CD.CostoUnitario, CD.Importe " +
            //    " From ComprasDet CD (NoLock) " +
            //    " Inner Join CatProductos P (NoLock) On ( CD.IdProducto = P.IdProducto ) " +
            //    " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.FolioCompra = '{3}' Order By CD.Renglon ",
            //    IdEmpresa, IdEstado, IdFarmacia, FolioCompra);

            ////sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
            ////    " CD.TasaIva, CD.CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " + 
            ////    " CD.Costo, 0 as Importe " +
            ////    " From vw_ComprasDet_CodigosEAN CD (NoLock) " +
            ////    " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
            ////    IdEmpresa, IdEstado, IdFarmacia, FolioCompra);

            sQuery = FolioDet_Compras(IdEmpresa, IdEstado, IdFarmacia, FolioCompra); 
            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string FolioDetLotes_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta)
        {
            sQuery = sInicio + string.Format("Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad \n" +
                "From vw_ComprasDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);

            return sQuery; 
        }

        public DataSet FolioDetLotes_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de compras no encontrado, verifique.";

            ////sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
            ////    " datediff(mm, getdate(), FechaCad) as MesesCad, " + 
            ////    " FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad " +
            ////    " From vw_ComprasDet_CodigosEAN_Lotes (NoLock) " +
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta); 

            sQuery = FolioDetLotes_Compras(IdEmpresa, IdEstado, IdFarmacia, FolioVenta); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioDet_ComprasDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Compra no encontrado, verifique.";

            //sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, P.Descripcion, CD.TasaIva, " + 
            //    " CD.Cant_Recibida, Cd.Cant_Devuelta, CD.CostoUnitario, CD.Importe " +
            //    " From ComprasDet CD (NoLock) " +
            //    " Inner Join CatProductos P (NoLock) On ( CD.IdProducto = P.IdProducto ) " +
            //    " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.FolioCompra = '{3}' Order By CD.Renglon ",
            //    IdEmpresa, IdEstado, IdFarmacia, FolioCompra);

            sQuery = sInicio + string.Format("Select \n"+
                "\tCD.CodigoEAN, CD.IdProducto, CD.DescProducto, CD.TasaIva, CD.CantidadRecibida, 0 as CantidadDevuelta, CD.Costo, 0 as Importe \n" +
                "From vw_ComprasDet_CodigosEAN CD (NoLock) \n" +
                "Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, FolioCompra);


            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDetLotes_ComprasDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de compras no encontrado, verifique.";

            //sQuery = sInicio + string.Format(" Select IdProducto, CodigoEAN, ClaveLote, FechaReg, FechaCad, Status, Cant_Recibida as Existencia, 0 as Cantidad " +
            //    " From vw_ComprasDet_CodigosEAN_Lotes (NoLock) " +
            //    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'  ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);

            sQuery = sInicio + string.Format("Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, \n" +
                "\tCantidadRecibida as Existencia, \n" +
                "\tdbo.fg_ALMN_DisponibleDevolucion_Lotes(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, CantidadRecibida) as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_ComprasDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'  \n", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Consultas Compras

        #region Usuarios y permisos 
        public DataSet Arboles()
        {
            myDataset = new DataSet();

            sQuery = sInicio + " Select Arbol, Nombre, Keyx From Net_Arboles (nolock) Order by Nombre";
            myDataset = (DataSet)EjecutarQuery(sQuery, "Arboles");


            return myDataset;
        }

        public DataSet PersonalAdmistrador(string IdEstado, string IdFarmacia, string IdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Personal";
            string sMsjNoEncontrado = "Clave de Personal no encontrada, verifique.";
            string sAdmin = "ADMINISTRADORES";

            sQuery = sInicio + string.Format(" select * " + 
	            " from Net_Grupos_De_Usuarios G (noLock) " +
                " inner join Net_Grupos_Usuarios_Miembros U (noLock) " + 
		        "    On ( G.IdEstado = U.IdEstado and G.IdSucursal = U.IdSucursal and G.IdGrupo = U.IdGrupo ) " +
                " Where U.IdEstado = '{0}' and U.IdSucursal = '{1}' and U.IdPersonal = '{2}' and U.Status = 'A' and Upper(ltrim(rtrim(G.NombreGrupo))) = '{3}' ",
                IdEstado, IdFarmacia, IdPersonal, sAdmin);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Personal(string IdEstado, string IdFarmacia, string IdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Personal";
            string sMsjNoEncontrado = "Clave de Personal no encontrada, verifique.";

            if (IdEstado == "")
                IdEstado = "%%";
            else
                IdEstado = Fg.PonCeros(IdEstado, 2);

            if (IdFarmacia == "")
                IdFarmacia = "%%";
            else
                IdFarmacia = Fg.PonCeros(IdFarmacia, 4);

            if (IdPersonal == "")
                IdPersonal = "%%";
            else
                IdPersonal = Fg.PonCeros(IdPersonal, 4);

            sQuery = sInicio + string.Format(" Select *, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as NombreCompleto From CatPersonal (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdPersonal = '{2}' ", IdEstado, IdFarmacia, IdPersonal);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion

        #region Transferencias 
        public DataSet Transferencia(string FolioTransferencia, string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la transferencia";
            string sMsjNoEncontrado = "Folio de transferencia no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select  ", 
                IdEstado, IdFarmacia, FolioTransferencia );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        /// <summary>
        /// Devuelve la lista de farmacias a las cuales se puede realizar una transferencia
        /// </summary>
        /// <param name="IdEstado">Estado de origen de de transferencia </param>
        /// <param name="IdFarmaciaLocal">Farmacia de origen de transferencia</param>
        /// <param name="Funcion">Funcion que ejecuta la consulta</param>
        /// <returns>Lista de farmacias a las cuales se puede enviar una transferencia</returns>
        public DataSet FarmaciasTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener las farmacias disponibles para transferencia";
            string sMsjNoEncontrado = "No se encontraron farmacias disponibles para transferencia, verifique.";
            sMsjNoEncontrado = "No se encontró la Farmacia seleccionada ó la Farmacia no es válida para generar transferencias, verifique.";

            sQuery = sInicio + string.Format(" Select F.* " + 
                " From vw_Farmacias F (NoLock) " +
                " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                " Where F.IdEstado = '{1}' and ( F.IdFarmacia <> '{2}' )  and E.Status = 'A' ", 
                IdEmpresa, IdEstado, IdFarmaciaLocal );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        /// <summary>
        /// Devuelve los datos de una farmacia a la cual se le puede generar transferencia
        /// </summary>
        /// <param name="IdEstado">Estado de origen de de transferencia </param>
        /// <param name="IdFarmaciaLocal">Farmacia de origen de transferencia</param>
        /// <param name="IdFarmacia">Farmacia destino de tranferencia</param>
        /// <param name="Funcion">Funcion que ejecuta la consulta</param>
        /// <returns>Información de la farmacia destino de transferencia</returns>
        public DataSet FarmaciasTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, string IdFarmacia, string Funcion)
        {
            return FarmaciasTransferencia(IdEmpresa, EsConsignacion, IdEstado, IdFarmaciaLocal, IdFarmacia, false, Funcion); 
        }

        public DataSet FarmaciasTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, string IdFarmacia, bool SoloAlmacenes, string Funcion)
        {
            myDataset = new DataSet();
            string sFiltro = ""; 
            string sMsjError = "Ocurrió un error al obtener las farmacias disponibles para transferencia";
            string sMsjNoEncontrado = "No se encontraron farmacias disponibles para transferencias, verifique.";
            sMsjNoEncontrado = "No se encontró la Farmacia seleccionada ó la Farmacia no es válida para generar transferencias, verifique.";
            string sWhereFarmacia = "";

            // " Where F.IdEstado = '{1}' and F.IdFarmacia =  '{2}' {4} and ( F.IdFarmacia <> '{3}' and E.Status = 'A' ) and F.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 )  ",
            if (!SoloAlmacenes)
            {
                sQuery = sInicio + string.Format(" Select F.*, IsNull(U.UrlFarmacia, '') as UrlFarmacia " +
                   " From vw_Farmacias F (NoLock) " +
                   " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                   " Inner Join vw_Farmacias_Urls U ( Nolock ) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia  ) " +
                   " Where F.IdEstado = '{1}' and F.IdFarmacia =  '{2}' {4} and ( E.Status = 'A' ) and F.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 )  ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdFarmaciaLocal, sFiltro);

                if (GnFarmacia.Transferencias_Interestatales__Farmacias && 
                    (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia)
                    ) 
                {
                    sQuery = sInicio + string.Format(" Select F.*, IsNull(U.UrlFarmacia, '') as UrlFarmacia " +
                       " From vw_Farmacias F (NoLock) " +
                       " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                       " Inner Join vw_Farmacias_Urls U ( Nolock ) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia  ) " +
                       " Where F.IdEstado = '{1}' and F.IdFarmacia =  '{2}' {4} and F.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 ) ",
                       IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdFarmaciaLocal, sFiltro);
                }

                myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            }
            else
            {
                if (IdFarmacia.Trim() != "")
                {
                    sWhereFarmacia = string.Format(" and F.IdFarmacia =  '{0}' ", Fg.PonCeros(IdFarmacia, 4));
                }

                sMsjError = "Ocurrió un error al obtener los almacenes disponibles para transferencia";
                sMsjNoEncontrado = "No se encontró el almacén seleccionado ó el almacén no es válido para generar transferencias, verifique.";

                sQuery = sInicio + string.Format(" Select F.*, IsNull(U.UrlFarmacia, '') as UrlFarmacia " +
                   " From vw_Farmacias F (NoLock) " +
                   " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                   " Inner Join vw_Farmacias_Urls U ( Nolock ) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia  ) " +
                   " Where F.IdEstado = '{1}' and E.Status = 'A' and F.Status = 'A' and F.EsAlmacen = 1  {2} and F.IdTipoUnidad not in ( 0, 5 ) ",
                   IdEmpresa, IdEstado, sWhereFarmacia);

                myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); 
            } 

            return myDataset;
        }

        public DataSet FolioTransferencia(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, bool TransfInter, string Funcion)
        {
            DataSet dtsResultado = new DataSet();

            dtsResultado = FolioTransferencia(IdEmpresa, IdEstado, false, ClaveRenapo, IdFarmacia, Folio, TipoTrans, TransfInter, true, Funcion);

            return dtsResultado;
        }

        public DataSet FolioTransferencia(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, bool TransfInter, bool ValidarOrigen, string Funcion)
        {
            DataSet dtsResultado = new DataSet();

            dtsResultado = FolioTransferencia(IdEmpresa, IdEstado, false, ClaveRenapo, IdFarmacia, Folio, TipoTrans, TransfInter, ValidarOrigen, Funcion); 

            return dtsResultado; 
        }

        public DataSet FolioTransferencia(string IdEmpresa, string IdEstado, bool EsAlmacen, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, 
            bool TransfInter, bool ValidarOrigen, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";
            string sFiltro_Origen = ""; 
            int iEsAlmacen = EsAlmacen ? 1 : 0;

            if (ValidarOrigen)
            {
                iEsAlmacen = TransfInter ? 1 : 0;
                sFiltro_Origen = string.Format(" and EsTransferenciaAlmacen = '{0}'  ", iEsAlmacen);
            }


            sQuery = sInicio + string.Format("" + 
                " Select * " + 
                " From vw_TransferenciasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}'  {4} ", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio, sFiltro_Origen);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaDetalles(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format("Select T.CodigoEAN, T.IdProducto, T.DescProducto, T.TasaIva, T.Cantidad, " +
                "T.Costo, T.Importe, 0, 0, T.UnidadDeSalida, R.ContenidoPiezasUnitario As Factor" + 
                " From vw_TransferenciasDet_CodigosEAN T(NoLock) " +
                "Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.IdProducto = R.IdProducto ANd T.CodigoEAN = R.CodigoEAN)" +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaDetallesLotes(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" + 
                "\tIdSubFarmaciaEnvia as IdSubFarmacia, SubFarmaciaEnvia as SubFarmacia, \n" +
                "\tIdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "FechaReg, FechaCad, Status, Existencia, Cantidad \n" +
                "From vw_TransferenciaDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        //public DataSet FolioTransferenciaDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, bool TransfInter, string Funcion)
        //{
        //    DataSet dtsResultado = new DataSet();

        //    dtsResultado = FolioTransferenciaDev(IdEmpresa, IdEstado, false, ClaveRenapo, IdFarmacia, Folio, TipoTrans, TransfInter, true, Funcion);

        //    return dtsResultado;
        //}

        //public DataSet FolioTransferenciaDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, bool TransfInter, bool ValidarOrigen, string Funcion)
        //{
        //    DataSet dtsResultado = new DataSet();

        //    dtsResultado = FolioTransferenciaDev(IdEmpresa, IdEstado, false, ClaveRenapo, IdFarmacia, Folio, TipoTrans, TransfInter, ValidarOrigen, Funcion);

        //    return dtsResultado;
        //}

        public DataSet FolioTransferenciaDev(
                string IdEmpresa, string IdEstado, string IdFarmacia,
                 string IdEstadoRecibe, string IdFarmaciaRecibe, 
                 string Folio, string TipoTrans, string sFolioSalida, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";


            sQuery = sInicio + string.Format(
                " Exec spp_DevolucionTransferenciasEnc_Carga @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdEstadoRecibe = '{3}', @IdFarmaciaRecibe = '{4}', @Folio = '{5}'",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdEstadoRecibe, Fg.PonCeros(IdFarmaciaRecibe, 4), sFolio, sFolioSalida);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        //public DataSet FolioTransferenciaDetallesDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, string Funcion)
        //{
        //    return FolioTransferenciaDetallesDev(IdEmpresa, IdEstado, ClaveRenapo, IdFarmacia, Folio, TipoTrans, "", Funcion);
        //}

        public DataSet FolioTransferenciaDetallesDev(
                string IdEmpresa, string IdEstado, string IdFarmacia,
                string IdEstadoRecibe, string IdFarmaciaRecibe,
                string Folio, string TipoTrans, string sFolioDevolucion, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string TipoTrans2 = "";
            string sFolio = "";
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";


            if(TipoTrans == "SDT")
            {
                TipoTrans2 = "EDT";
            }
            else
            {
                TipoTrans2 = "SDT";
            }

            if (Folio != "")
            {
                sFolio = TipoTrans + Fg.PonCeros(Folio, 8);
            }

            if (sFolioDevolucion != "")
            {
                sFolioDevolucion = TipoTrans2 + Fg.PonCeros(sFolioDevolucion, 8);
            }


            sQuery = sInicio + string.Format(
                //"Select \n" +
                //"\tCodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, 0 As CantidadDevolucion, Costo, Importe, 0, 0, UnidadDeSalida \n" +
                //"From vw_TransferenciasDet_CodigosEAN (NoLock) \n" +
                //"Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n"
                "Exec spp_DevolucionTransferenciasDet_Carga @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdEstadoRecibe = '{3}', @IdFarmaciaRecibe = '{4}', @Folio = '{5}', @FolioDevolucion = '{6}'"

            , IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdEstadoRecibe, Fg.PonCeros(IdFarmaciaRecibe, 4), sFolio, sFolioDevolucion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaDetallesLotesDev(
            string IdEmpresa,
            string IdEstado, string IdFarmacia,
            string IdEstadoRecibe, string IdFarmaciaRecibe,
            string Folio, string TipoTrans, string sFolioDevolucion, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string TipoTrans2 = "";
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";
            string sExist_Disponible = "\tCantidad as ExistenciaDisponible, \n";

            if (TipoTrans == "SDT")
            {
                TipoTrans2 = "EDT";
            }
            else
            {
                TipoTrans2 = "SDT";
            }

            if (Folio != "")
            {
                sFolio = TipoTrans + Fg.PonCeros(Folio, 8);
            }

            if (sFolioDevolucion != "")
            {
                sFolioDevolucion = TipoTrans2 + Fg.PonCeros(sFolioDevolucion, 8);
            }

            sQuery = sInicio + string.Format(
                                //"Select \n" +
                                //"\tIdSubFarmaciaEnvia as IdSubFarmacia, SubFarmaciaEnvia as SubFarmacia, \n" +
                                //"\tIdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                                //"\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                                //"\tFechaReg, FechaCad, Status, Cantidad As Existencia, \n" +
                                //"{4} " +
                                //"\t0 As Cantidad " +
                                //"From vw_TransferenciaDet_CodigosEAN_Lotes (NoLock) \n" +
                                //"Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n"
                                "Exec spp_DevolucionTransferenciasDet_Lotes_Carga @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdEstadoRecibe = '{3}', @IdFarmaciaRecibe = '{4}', @Folio = '{5}', @FolioDevolucion = '{6}'",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdEstadoRecibe, Fg.PonCeros(IdFarmaciaRecibe, 4), sFolio, sFolioDevolucion);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaDetallesLotes_UbicacionesDev(
            string IdEmpresa,
            string IdEstado, string IdFarmacia,
            string IdEstadoRecibe, string IdFarmaciaRecibe,
            string Folio, string TipoTrans, string sFolioDevolucion,
            int IdPasillo, int IdEstante, int IdEntrepaño, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string TipoTrans2 = "";
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";
            string sExist_Disponible = "\tcast(L.CantidadEnviada as Int) as ExistenciaDisponible,\n";

            if (TipoTrans == "SDT")
            {
                TipoTrans2 = "EDT";
            }
            else
            {
                TipoTrans2 = "SDT";
            }

            if (Folio != "")
            {
                sFolio = TipoTrans + Fg.PonCeros(Folio, 8);
            }

            if (sFolioDevolucion != "")
            {
                sFolioDevolucion = TipoTrans2 + Fg.PonCeros(sFolioDevolucion, 8);
            }

            sQuery = sInicio + string.Format(
                //"Select \n" +
                //"\tL.IdSubFarmaciaEnvia as IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.SKU, F.ClaveLote, \n" +
                //"\tF.IdPasillo, F.IdEstante, F.IdEntrepaño as IdEntrepano, \n" +
                //"\t(Case When F.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                //"cast(L.CantidadEnviada as Int) as Existencia, \n" + 
                //"{4} " +
                //"\t0 as Cantidad \n" +
                //"From TransferenciasDet_Lotes_Ubicaciones L (NoLock) \n" +
                //"Inner join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) \n" +
                //"\tOn ( \n" +
                //"\t\tL.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.SKU = F.SKU and L.IdSubFarmaciaEnvia = F.IdSubFarmacia \n" +
                //"\t\tand L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote \n" +
                //"\t\tand L.IdPasillo = F.IdPasillo and L.IdEstante = F.IdEstante and L.IdEntrepaño = F.IdEntrepaño \n" +
                //"\t) \n" +
                //"Where L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.FolioTransferencia = '{3}' \n" +
                //"Order by L.IdSubFarmaciaEnvia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                "Exec spp_DevolucionTransferenciasDet_Lotes_Ubicaciones_Carga @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @IdEstadoRecibe = '{3}', @IdFarmaciaRecibe = '{4}', @Folio = '{5}', @FolioDevolucion = '{6}', @IdPasillo = '{7}', @IdEstante = {8}, @idEntrepaño = {9}  ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdEstadoRecibe, Fg.PonCeros(IdFarmaciaRecibe, 4), sFolio, sFolioDevolucion, IdPasillo.ToString(), IdEstante.ToString(), IdEntrepaño.ToString());

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "SDT" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_DevolucionTransferenciaEnvio_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDetalleDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "SDT" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";

            sQuery = sInicio + string.Format(" Select T.CodigoEAN, T.IdProducto, P.Descripcion As DescProducto, T.TasaIva, T.CantidadEnviada As Cantidad, R.ContenidoPiezasUnitario As Factor, " +
                 "T.CostoUnitario As Costo, T.Importe, 0, 0, 1 As UnidadDeSalida " +
                 "From DevolucionTransferenciasEnvioDet T (NoLock) " +
                 " Inner Join vw_Productos_CodigoEAN P (NoLock) On (T.IdProducto = P.IdProducto And T.CodigoEAN = P.CodigoEAN)" +
                 "Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.IdProducto = R.IdProducto ANd T.CodigoEAN = R.CodigoEAN)" +
                " Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and FolioTransferencia = '{3}' " +
                "Select T.CodigoEAN, T.ClaveSSA, P.DescripcionSal As DescProducto, P.Presentacion, T.Cantidad " +
                "From DevolucionTransferenciasEnvioDet_Manual T(NoLock) " +
                "   inner join vw_ClavesSSA_Sales P On(T.ClaveSSA = P.ClaveSSA) " +
                "Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and FolioTransferencia = '{3}'",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDetalleLotesDev(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "SDT" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmaciaEnvia As IdSubFarmacia, L.SubFarmaciaEnvia As SubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote,\n" +
                "\tL.FechaReg, L.FechaCad, L.Status, L.Cantidad as Existencia, cast(L.Cantidad as int) as ExistenciaDisponible, l.Cantidad \n" +
                "From vw_DevolucionTransferenciaEnvioDet_CodigosEAN_Lotes L(NoLock) \n" +
                "Where L.IdEmpresa = '{0}' and L.IdEstadoEnvia = '{1}' and L.IdFarmaciaEnvia =  '{2}' and L.Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDetalleLotes_Ubicacionesdev
        (
            string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio,
            int Pasillo, int Estante, int Entrepano,
            string Funcion
        )
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "SDT" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";


            ////sQuery = sInicio + string.Format("	" +
            ////       " Select L.IdSubFarmaciaEnvia as IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, " +
            ////       "     IdPasillo, F.IdEstante, F.IdEntrepaño as IdEntrepano, " +
            ////       "    (Case When F.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, " +
            ////       "    cast(F.Existencia as Int) as Existencia, cast(L.CantidadEnviada as int) as Cantidad " +
            ////       " From TransferenciasDet_Lotes_Ubicaciones L (NoLock) " +
            ////       " Inner join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) " +
            ////       "    On ( " +
            ////       "            L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmaciaEnvia = F.IdSubFarmacia " +
            ////       "            and L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote " +
            ////       "            and L.IdPasillo = F.IdPasillo and L.IdEstante = F.IdEstante and L.IdEntrepaño = F.IdEntrepaño  " +
            ////       "        ) " +
            ////       " Where L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.FolioTransferencia = '{3}' " +
            ////       " Order by L.IdSubFarmaciaEnvia, L.IdProducto, L.CodigoEAN, L.ClaveLote ",
            ////       IdEmpresa, IdEstado, IdFarmacia);


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmaciaEnvia As IdSubFarmacia, \n" +
                "\tIdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\t{4} as IdPasillo, {5} as IdEstante, {6} as IdEntrepano, \n" +
                "\t'Cancelado' as Status, cast(CantidadEnviada as int) as Existencia, \n" +
                "\tcast(CantidadEnviada as int) as ExistenciaDisponible, \n" +
                "\tcast(CantidadEnviada as int) as Cantidad \n" +
                "From DevolucionTransferenciasEnvioDet_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and FolioTransferencia = '{3}' \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio, Pasillo, Estante, Entrepano);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntrada(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_TransferenciaEnvio_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and Folio = '{3}' ", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }

        public DataSet FolioTransferenciaEntradaDetalle(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";

            sQuery = sInicio + string.Format(" Select T.CodigoEAN, T.IdProducto, T.DescProducto, T.TasaIva, T.Cantidad, R.ContenidoPiezasUnitario As Factor, " +
                 "T.Costo, T.Importe, 0, 0, T.UnidadDeSalida " +
                 "From vw_TransferenciaEnvioDet_CodigosEAN T (NoLock) " +
                 "Inner Join CatProductos_CodigosRelacionados R (NoLock) On (T.IdProducto = R.IdProducto ANd T.CodigoEAN = R.CodigoEAN)" +
                " Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and Folio = '{3}' ", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDetalleLotes(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmaciaEnvia As IdSubFarmacia, SubFarmaciaEnvia As SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tFechaReg, FechaCad, Status, Cantidad as Existencia, Cantidad as Cantidad \n" +
                "From vw_TransferenciaEnvioDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTransferenciaEntradaDetalleLotes_Ubicaciones
        (
            string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, 
            int Pasillo, int Estante, int Entrepano,  
            string Funcion
        )
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";


            ////sQuery = sInicio + string.Format("	" +
            ////       " Select L.IdSubFarmaciaEnvia as IdSubFarmacia, F.IdProducto, F.CodigoEAN, F.ClaveLote, " +
            ////       "     IdPasillo, F.IdEstante, F.IdEntrepaño as IdEntrepano, " +
            ////       "    (Case When F.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, " +
            ////       "    cast(F.Existencia as Int) as Existencia, cast(L.CantidadEnviada as int) as Cantidad " +
            ////       " From TransferenciasDet_Lotes_Ubicaciones L (NoLock) " +
            ////       " Inner join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) " +
            ////       "    On ( " +
            ////       "            L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmaciaEnvia = F.IdSubFarmacia " +
            ////       "            and L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote " +
            ////       "            and L.IdPasillo = F.IdPasillo and L.IdEstante = F.IdEstante and L.IdEntrepaño = F.IdEntrepaño  " +
            ////       "        ) " +
            ////       " Where L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.FolioTransferencia = '{3}' " +
            ////       " Order by L.IdSubFarmaciaEnvia, L.IdProducto, L.CodigoEAN, L.ClaveLote ",
            ////       IdEmpresa, IdEstado, IdFarmacia);


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmaciaEnvia As IdSubFarmacia, \n" +
                "\tIdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\t{4} as IdPasillo, {5} as IdEstante, {6} as IdEntrepano, \n" +
                "\t'Cancelado' as Status, cast(Cantidad as int) as Existencia, \n" +
                "\tcast(Cantidad as int) as ExistenciaDisponible, \n" +
                "\tcast(Cantidad as int) as Cantidad \n" +
                "From vw_TransferenciaEnvioDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio, Pasillo, Estante, Entrepano);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); 

            return myDataset;
        }

        public DataSet FolioTransferenciaEntrada_UUID(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            //string sFolio = Fg.PonCeros(IdEstado,2)+ Fg.PonCeros(IdFarmacia,4) + ClaveRenapo + "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = "TS" + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el detalle de transferencia de entrada";
            string sMsjNoEncontrado = "No se encontrarón detalles de transferencia, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From Transferencias_UUID_Registrar (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstadoEnvia = '{1}' and IdFarmaciaEnvia =  '{2}' and FolioTransferencia = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Transferencias

        #region Traspasos 
        public DataSet FolioTraspaso(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio,
            string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de traspaso";
            string sMsjNoEncontrado = "No se encontró el folio de traspaso, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_TraspasosEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubFarmacias(string IdEstado, string IdFarmacia, string IdSubFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la SubFarmacia";
            string sMsjNoEncontrado = "Clave de SubFarmacia no encontrada, verifique.";
            string sWhere = "";

            if (IdSubFarmacia != "")
                sWhere = string.Format(" And IdSubFarmacia = '{0}'", Fg.PonCeros(IdSubFarmacia, 2));

            sQuery = sInicio + string.Format(" Select IdSubFarmacia, ( IdSubFarmacia + ' -- ' + Descripcion ) as NombreSubFarmacia " +
                " From CatFarmacias_SubFarmacias (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' {2} Order by IdSubFarmacia ", IdEstado, IdFarmacia, sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTraspasoDetalles(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de Traspaso";
            string sMsjNoEncontrado = "No se encontró el folio de Traspaso, verifique.";

            sQuery = sInicio + string.Format("Select CodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, Costo, Importe, 0, 0, UnidadDeSalida \n" +
                "From vw_TraspasosDet_CodigosEAN (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioTraspasoDetallesLotes(string IdEmpresa, string IdEstado, string ClaveRenapo, string IdFarmacia, string Folio, string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            string sFolio = TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS 
            string sMsjError = "Ocurrió un error al obtener el folio de Traspaso";
            string sMsjNoEncontrado = "No se encontró el folio de Traspaso, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "IdSubFarmacia as IdSubFarmacia, SubFarmacia as SubFarmacia, \n" +
                "IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "datediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "FechaReg, FechaCad, Status, Existencia, Cantidad \n" +
                "From vw_TraspasosDet_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDetLotes_Traspasos_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioTraspaso, string TipoTrans, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";
            string sFolio = TipoTrans + Fg.PonCeros(FolioTraspaso, 8);

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad \n" +
                   "From vw_TraspasosDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), sFolio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Traspasos

        #region Inventarios 
        public DataSet MovtosTiposInventario(string TipoMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Lista de Movimientos de Inventario";
            string sMsjNoEncontrado = "No se encontró el Movimiento de Inventario ingresado. Se registrará como un movimiento NUEVO.";

            sQuery = sInicio + string.Format("Select * From vw_MovtosInv_Tipos(NoLock) Where TipoMovto =  '{0}' ", TipoMovto);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet MovtosTiposInventario_Farmacias(string IdEstado, string IdFarmacia, int Tipo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Lista de Movimientos de Inventario";
            string sMsjNoEncontrado = "No se encontraron Movimientos de Inventario, verifique.";

            sQuery = sInicio + string.Format("	Select *, (left(TipoMovto + cast('        ' as varchar(8)),8) + ' -  ' + DescMovimiento) as DescripcionMovimiento" +
                   " From vw_MovtosInv_Tipos_Farmacia L (NoLock) " +
                   " Where IdEstado =  '{0}' and IdFarmacia = '{1}' and EsMovtoGral = '{2}' ", IdEstado, IdFarmacia, Tipo);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, TiposDeInventario.Todos, EsEntrada, Funcion);
        }

        public DataSet LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TipoDeInventario, TipoDeSubFarmacia, EsEntrada, Funcion); 
        }

        public DataSet LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TipoDeInventario, EsEntrada, Funcion);
        }

        public DataSet LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdCodigo, string IdCodEAN, 
            TiposDeInventario TipoDeInventario, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdCodigo, IdCodEAN, TipoDeInventario, TiposDeSubFarmacia.Todos, EsEntrada, Funcion); 
        }

        public DataSet LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdCodigo, string IdCodEAN, 
            TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, bool EsEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Error al consultar LOTES de Producto.";
            string sMsjNoEncontrado = "LOTE de Producto no valido, Favor de verificar.";
            string sFiltroConsignacion = " ";
            string sFiltroSubFarmacia = " ";
            string[] listaSubFarmacias = null;
            string sListaSubFarmacias = "";

            switch (TipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break; 

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and ClaveLote not like '%*%' ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and ClaveLote like '%*%' ";

                    if (TipoDeSubFarmacia == TiposDeSubFarmacia.Consignacion) 
                    {
                        sFiltroConsignacion += " and EmulaVenta = 0 ";
                    }

                    if (TipoDeSubFarmacia == TiposDeSubFarmacia.ConsignacionEmulaVenta) 
                    {
                        sFiltroConsignacion += " and EmulaVenta = 1 ";
                    }
                    break; 
            }


            if (IdSubFarmacia.Trim() != "" )
            {
                if (IdSubFarmacia.Contains(","))
                {
                    IdSubFarmacia = IdSubFarmacia.Replace(" ", "");
                    listaSubFarmacias = IdSubFarmacia.Split(',');

                    foreach (string sSubFarmaciaLocal in listaSubFarmacias)
                    {
                        if (sSubFarmaciaLocal.Trim() != "")
                        {
                            sListaSubFarmacias += string.Format("'{0}',", Fg.PonCeros(sSubFarmaciaLocal.Trim(), 3));
                        }
                    }

                    sListaSubFarmacias = Fg.Left(sListaSubFarmacias, sListaSubFarmacias.Length - 1); 
                    sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia in (  {0}  ) ", sListaSubFarmacias);
                }
                else
                {
                    sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia = '{0}' ", Fg.PonCeros(IdSubFarmacia, 3));
                }
            }

            sQuery = "";

            sQuery = sInicio + string.Format("" + 
                "Declare \n " +
                "   @Bloquedado int \n " +
                "   Select @Bloquedado = (case when dbo.fg_INV_GetStatusProducto('{0}', '{1}', '{2}', '{3}') In ( 'I', 'S' ) Then 1 else 0 end) \n " + 
                "\n " +
                "Select F.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, L.SKU, L.CodigoEAN, L.ClaveLote, \n" +
                "\t   datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                "\t   L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, \n" +
                "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                "\t   (case when @Bloquedado = 1 then 0 Else cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) End ) as Existencia, \n" +
                "\t   (case when @Bloquedado = 1 then 0 Else cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) End ) as ExistenciaDisponible, \n" +
                "\t   0 as Cantidad \n" +
                "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                "Inner join vw_Farmacias_SubFarmacias F (NoLock) \n" +
                "   On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                "   and L.IdProducto = '{3}' and CodigoEAN = '{4}' {5}  \n" +
                "Order by L.SKU, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia);

            if(EsEntrada)
            {
                sQuery = sInicio + string.Format("" +
                    "Declare \n " +
                    "   @Bloquedado int \n " +
                    "   Select @Bloquedado = (case when dbo.fg_INV_GetStatusProducto('{0}', '{1}', '{2}', '{3}') In ( 'I', 'S' ) Then 1 else 0 end) \n " +
                    "\n " +
                    "Select \n" +
                    "\t   L.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, '' as SKU, L.CodigoEAN, L.ClaveLote, \n" +
                    "\t   datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                    "\t   cast(convert(varchar(10), L.FechaRegistro, 120) as datetime) as FechaReg, L.FechaCaducidad as FechaCad,\n " +
                    "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status,\n " +
                    "\t   (case when @Bloquedado = 1 then 0 Else cast(sum((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos))) as Int) End ) as Existencia, \n" +
                    "\t   (case when @Bloquedado = 1 then 0 Else cast(sum((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos))) as Int) End ) as ExistenciaDisponible, \n" +
                    "\t   0 as Cantidad \n" +
                    "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                    "Inner join vw_Farmacias_SubFarmacias F (NoLock) \n" +
                    "\t   On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                    "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                    "\t   and L.IdProducto = '{3}' and CodigoEAN = '{4}' {5}  \n" +
                    "Group by \n" +
                    "\t   L.IdSubFarmacia, F.Descripcion, L.IdProducto, L.CodigoEAN, L.ClaveLote, \n" +
                    "\t   datediff(mm, getdate(), L.FechaCaducidad), \n" +
                    "\t   cast(convert(varchar(10), L.FechaRegistro, 120) as datetime), \n" + 
                    "\t   L.FechaCaducidad, \n" + 
                    "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) \n" + 
                    "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                    IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia);
            }

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false; 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet LotesDeCodigo_CodigoEAN_CartasCanje(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdCodigo, string IdCodEAN,
            string sFolioCarta, TiposDeInventario TipoDeInventario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sFiltroConsignacion = " ";
            string sFiltroSubFarmacia = " ";

            switch (TipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and L.ClaveLote not like '%*%' ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and L.ClaveLote like '%*%' ";
                    break;
            }

            if (IdSubFarmacia != "")
            {
                sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia = '{0}' ", IdSubFarmacia);
            }

            sQuery = sInicio + string.Format("" +
                "Select " + 
                "\t   F.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                "\t   datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                "\t   L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, \n" +
                "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                "\t   cast((R.Cant_Enviada) as Int) as Existencia, \n" +
                "\t   cast((R.CantidadEnviada) as Int) as ExistenciaDisponible, \n" +
                "\t   0 as Cantidad \n" +
                "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                "Inner Join RutasDistribucionDet_CartasCanje R (NoLock) \n" +
                "\t   On ( L.IdEmpresa = R.IdEmpresa And L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmacia and \n" +
                "\t       L.IdProducto = R.IdProducto And L.CodigoEAN = R.CodigoEAN And L.IdSubFarmacia = R.IdSubFarmacia and L.SKU = R.SKU And L.ClaveLote = R.ClaveLote) \n" +
                "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                "\t   On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                "\t   and L.IdProducto = '{3}' and L.CodigoEAN = '{4}' {5} And R.FolioCarta = '{7}' \n" +
                "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia, sFolioCarta);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }
        
        public DataSet Codigo_CodigoEAN_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdCodigo, string IdCodEAN, string Lote, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";                      

            sQuery = sInicio + string.Format("	" +
                "Declare \n " +
                "   @Bloquedado int \n " +
                "   Select @Bloquedado = (case when dbo.fg_INV_GetStatusProducto('{0}', '{1}', '{2}', '{3}') In ( 'I', 'S' ) Then 1 else 0 end) \n " +
                "\n " +
                "Select \n" +
                "\t   F.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, \n" +
                "\t   datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, \n" +
                "\t   L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, \n" +
                "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n " +
                "\t   (case when @Bloquedado = 1 then 0 Else cast((L.Existencia - L.ExistenciaEnTransito) as Int) End ) as Existencia, \n " +
                "\t   (case when @Bloquedado = 1 then 0 Else cast((L.Existencia - L.ExistenciaEnTransito) as Int) End ) as ExistenciaDisponible, \n " +
                "\t   0 as Cantidad \n " +
                "From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) \n" +
                "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                "\t   On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and F.SKU = L.SKU and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.IdSubFarmacia = '{3}' \n" +
                "\t   and L.IdProducto = '{4}' and CodigoEAN = '{5}' and L.ClaveLote = '{6}' \n" +
                "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdCodigo, IdCodEAN, Lote);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }
        /// <summary>
        /// Devuelve la informacion del Producto asignado al Estado
        /// </summary>
        /// <param name="IdEstado">Estado conectado</param>
        /// <param name="IdCodigo">Codigo interno solicitado</param>
        /// <param name="Funcion">Que función solicita la información</param>
        /// <returns>Dataset con la información solicitada</returns>
        public DataSet ProductosEstado(string IdEmpresa, string IdEstado, string IdCodigo, string Funcion) 
        {
            myDataset = new DataSet(); 
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sEsSectorSalud = ""; 

            if (bEsPublicoGeneral)
            {
                sEsSectorSalud = " and EsSectorSalud = 0 ";
            } 

            sQuery = sInicio + string.Format(" Select *, " +
                " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio " + 
                " From vw_ProductosEstadoFarmacia V (NoLock) " +
                " Where V.IdEstado = '{1}' and ( right(replicate('0', {6}) + V.CodigoEAN, {6} ) = '{2}' or right(replicate('0', {6}) + V.CodigoEAN_Interno, {6} ) = '{3}' ) {5} ",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdCodigo, iLenCodigoEAN),
                Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, iLenCodigoEAN);


            ////if (GnFarmacia.INT_OPM_ProcesoActivo)
            ////{
            ////    ////////sQuery = sInicio + string.Format(" Select V.*, " +
            ////    ////////    " dbo.fg_ObtenerCostoPromedio('{0}', V.IdEstado, '{4}', V.IdProducto) as CostoPromedio " +
            ////    ////////    " From vw_ProductosEstadoFarmacia V (NoLock) " +
            ////    ////////    " Inner Join INT_ND_Productos I (NoLock) " +
            ////    ////////    "     On ( I.IdEstado = '{1}' and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) ) " +
            ////    ////////    " Where V.IdEstado = '{1}' and ( V.CodigoEAN = '{2}' or V.CodigoEAN_Interno = '{3}' ) {5} ",
            ////    ////////    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdCodigo.Trim(),
            ////    ////////    Fg.PonCeros(IdCodigo, 13), DtGeneral.FarmaciaConectada, sEsSectorSalud);

            ////    ////sQuery += "\n ";
            ////    ////sQuery += "\t and Exists \n ";
            ////    ////sQuery += "\t ( \n ";
            ////    ////sQuery += "\t\t Select CodigoEAN_ND From INT_ND_Productos I (NoLock)  \n ";
            ////    ////sQuery += "\t\t Where I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) \n";
            ////    ////sQuery += "\t\t\t and Exists ( Select ClaveSSA_ND From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) Where I.ClaveSSA_ND = C.ClaveSSA_ND  ) "; 
            ////    ////sQuery += "\t ) \n ";
            ////}
            
            
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ProductoCartaCanje(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCarta, 
            bool EsAlmacenRelacionado, 
            string IdCodigo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada en carta canje, verifique.";

            if(!EsAlmacenRelacionado)
            {
                sQuery = sInicio + string.Format(" 	Select *, dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{1}', v.IdProducto) as CostoPromedio \n" +
                        "From vw_ProductosEstadoFarmacia V (NoLock) \n" +
                        "Where Exists \n" +
                        "( \n" +
                        "\tSelect * \n" +
                        "\tFrom RutasDistribucionDet_CartasCanje R (NoLock) \n" +
                        "\tWhere \n" +
                        "\t( \n" +
                        "\t\tR.IdEmpresa = '{0}' And R.IdEstado = '{1}' And R.IdFarmacia = '{2}' And R.FolioCarta = '{3}' " +
                        "\t\tAnd ( right(replicate('0', {6}) + CodigoEAN, {6} ) = '{4}' or right(replicate('0', {6}) + CodigoEAN_Interno, {6} ) = '{5}' ) \n" +
                        "\t\tAnd V.IdEstado = R.IdEstado And V.CodigoEAN = R.CodigoEAN \n" +
                        "\t) \n" +
                        ") \n",
                    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdFarmacia, FolioCarta, Fg.PonCeros(IdCodigo.Trim(), iLenCodigoEAN),
                    Fg.PonCeros(IdCodigo, iLenCodigoEAN), iLenCodigoEAN);
            }
            else
            {
                sQuery = sInicio + string.Format("Select *, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{1}', v.IdProducto) as CostoPromedio \n" +
                        "From vw_Productos_CodigoEAN V (NoLock) \n" +
                        "Where Exists \n" +
                        "( \n" +
                        "\tSelect * \n" +
                        "\tFrom RutasDistribucionDet_CartasCanje R (NoLock) \n" +
                        "\tWhere \n" +
                        "\t( \n" +
                        "\t\tR.IdEmpresa = '{0}' And R.IdEstado = '{1}' And R.IdFarmacia = '{2}' And R.FolioCarta = '{3}' " +
                        "\t\tAnd ( right(replicate('0', {6}) + CodigoEAN, {6} ) = '{4}' or right(replicate('0', {6}) + V.CodigoEAN_Interno, {6} ) = '{5}' ) \n" +
                        "\t\tAnd V.CodigoEAN = R.CodigoEAN \n" +
                        "\t) \n" +
                        ")\n",
                    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdFarmacia, FolioCarta, Fg.PonCeros(IdCodigo.Trim(), iLenCodigoEAN),
                    Fg.PonCeros(IdCodigo, iLenCodigoEAN), iLenCodigoEAN);

            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string Funcion)
        {
            return ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, IdCodigo, "", false, Funcion); 
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, bool SoloControlados, string Funcion)
        {
            return ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, IdCodigo, "", SoloControlados, Funcion);
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdClaveSSA, bool SoloControlados, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sEsSectorSalud = "";
            string sFiltro_ClaveSSA = "";
            string sFiltro_Controlados = ""; 

            if (bEsPublicoGeneral)
            {
                sEsSectorSalud = " and EsSectorSalud = 0 "; 
            }

            if (IdClaveSSA != "")
            {
                sMsjNoEncontrado = "Clave de Producto no encontrada ó no pertenece a la Clave especificada, verifique.";
                sFiltro_ClaveSSA = string.Format(" and V.IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA, 4));
            }

            sFiltro_Controlados = string.Format(" and V.EsControlado in ( 0, 1 ) ");
            if (SoloControlados)
            {
                sFiltro_Controlados = string.Format(" and V.EsControlado in ( 1 ) ");
            }

            if (bEsClienteIMach)
            {
                ////sQuery = sInicio + string.Format(" Select V.*, IsNull(I.EsMach4, 0) as EsMach4, " +
                ////    " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio " +
                ////    " From vw_ProductosExistenEnEstadoFarmacia v (NoLock) " +
                ////    " Left Join vw_Productos_IMach I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) " +
                ////    " Where v.IdEstado = '{1}' and v.IdFarmacia = '{6}' " +
                ////        " and ( right(replicate('0', {8}) + v.CodigoEAN, {8} ) = '{2}' or right(replicate('0', {8}) + v.CodigoEAN_Interno, {8} ) = '{3}' ) {5} {7} ",
                ////    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdCodigo, iLenCodigoEAN),
                ////    Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, iLenCodigoEAN);

                if (RobotDispensador.Robot.Interface == RobotDispensador_Interface.Medimat)
                {
                    sQuery = sInicio + string.Format(" Select V.*, IsNull(I.EsMach4, 0) as EsMach4, \n" +
                        " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio \n" +
                        " From vw_ProductosExistenEnEstadoFarmacia v (NoLock) \n" +
                        " Left Join vw_Productos_IMach I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) \n" +
                        " Where v.IdEstado = '{1}' and v.IdFarmacia = '{6}' \n" +
                        "       and ( v.CodigoEAN = '{2}' or right(replicate('0', {8}) + v.CodigoEAN_Interno, {8} ) = '{3}' ) {5} {7} {9} \n",
                        Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdCodigo.Trim(),
                        Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, iLenCodigoEAN, sFiltro_Controlados);
                }

                if (RobotDispensador.Robot.Interface == RobotDispensador_Interface.GPI)
                {
                    sQuery = sInicio + string.Format(" Select V.*, IsNull(I.EsGPI, 0) as EsMach4, \n" +
                        " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio \n" +
                        " From vw_ProductosExistenEnEstadoFarmacia v (NoLock) \n" +
                        " Left Join vw_Productos_IGPI I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) \n" +
                        " Where v.IdEstado = '{1}' and v.IdFarmacia = '{6}' \n" +
                        "       and ( v.CodigoEAN = '{2}' or right(replicate('0', {8}) + v.CodigoEAN_Interno, {8} ) = '{3}' ) {5} {7} {9} \n",
                        Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdCodigo.Trim(),
                        Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, iLenCodigoEAN, sFiltro_Controlados);
                }

            }
            else
            {
                ////sQuery = sInicio + string.Format(" Select V.*, cast(0 as bit) as EsMach4, " +
                ////    " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio " +
                ////    " From vw_ProductosExistenEnEstadoFarmacia v (NoLock) " +
                ////    " Where v.IdEstado = '{1}' and v.IdFarmacia = '{6}' " +
                ////        " and ( right(replicate('0', {8}) + v.CodigoEAN, {8} ) = '{2}' or right(replicate('0', {8}) + v.CodigoEAN_Interno, {8} ) = '{3}' ) {5} {7} ",
                ////    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdCodigo, iLenCodigoEAN),
                ////    Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, iLenCodigoEAN);

                sQuery = sInicio + string.Format(" Select V.*, cast(0 as bit) as EsMach4, " +
                    " dbo.fg_ObtenerCostoPromedio('{0}', v.IdEstado, '{4}', v.IdProducto) as CostoPromedio " +
                    " From vw_ProductosExistenEnEstadoFarmacia v (NoLock) " +
                    " Where v.IdEstado = '{1}' and v.IdFarmacia = '{6}' " +
                    "       and ( v.CodigoEAN = '{2}' or right(replicate('0', {8}) + v.CodigoEAN_Interno, {8} )= '{3}' ) {5} {7} {9} ",
                    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), IdCodigo.Trim(),
                    Fg.PonCeros(IdCodigo, iLenCodigoEAN), DtGeneral.FarmaciaConectada, sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, iLenCodigoEAN, sFiltro_Controlados);
            }


            //////if (GnFarmacia.INT_OPM_ProcesoActivo)
            //////{
            //////    //////sQuery += "\n ";
            //////    //////sQuery += "\t and Exists \n ";
            //////    //////sQuery += "\t ( \n ";
            //////    //////sQuery += "\t\t Select CodigoEAN_ND From INT_ND_Productos I (NoLock)  \n ";
            //////    //////sQuery += "\t\t Where I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) \n";
            //////    //////sQuery += "\t\t\t and Exists ( Select ClaveSSA_ND From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) Where I.ClaveSSA_ND = C.ClaveSSA_ND  ) ";
            //////    //////sQuery += "\t ) \n ";
            //////}    


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }  

        public DataSet FolioMovtoInventario(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información del movimiento de inventario.";
            string sMsjNoEncontrado = "Folio de movimiento de inventario no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_MovtosInv_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}' ", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioMovto);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioMovtoInventarioDetalle(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información del movimiento de inventario.";
            string sMsjNoEncontrado = "Folio de movimiento de inventario no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select " + 
                " L.CodigoEAN, L.IdProducto, L.DescProducto, L.TasaIva, L.Cantidad, L.Costo, L.Importe, 0, 0, L.UnidadDeSalida " +
                " From vw_MovtosInv_Det_CodigosEAN L (NoLock) " +
                " Where L.IdEmpresa = '{0}' and L.IdEstado = '{1}' and L.IdFarmacia =  '{2}' and L.Folio = '{3}' " +
                "    and L.Status = 'A' " +
                " Order By L.KeyxDetalle ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioMovto);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioMovtoInventarioDetalleLotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información del movimiento de inventario.";
            string sMsjNoEncontrado = "Folio de movimiento de inventario no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select L.IdSubFarmacia, L.SubFarmacia, \n" +
                "\tL.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                "\tdatediff(mm, getdate(), L.FechaCad) as MesesCad, \n" +
                "\tL.FechaReg, L.FechaCad, L.Status, L.Existencia, L.Cantidad \n" +
                "From vw_MovtosInv_Det_CodigosEAN_Lotes L (NoLock) \n" +
                "Where L.IdEmpresa = '{0}' and L.IdEstado = '{1}' and L.IdFarmacia =  '{2}' and L.Folio = '{3}' \n" +
                "\tand L.Status = 'A' \n" +
                "Order By L.KeyxDetalleLote \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioMovto);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_MovtosInvConsignacion_NoEnCatalogo_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select *, '001 -- Venta' As [Sub-Farmacia] " +
                " From MovtosInvConsignacion_NoEnCatalogo_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_MovtosInvConsignacion_NoEnCatalogo_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select ClaveSSA, Descripcion, CodigoEAN, " +
                "NombreComercial, Laboratorio, ClaveLote, convert(varchar(10),FechaCaducidad,120) As  FechaCaducidad, CantidadLote " +
                " From vw_Impresion_InventarioInicial_NoEnCatalogo (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #region Manejo de Ubicaciones
        ////////////////////////////
        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN, bool UbicacionesEstandar, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TiposDeInventario.Todos, TiposDeUbicaciones.Todas, UbicacionesEstandar, EsEntrada, Funcion);
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TiposDeInventario.Todos, TiposDeUbicaciones.Todas, false, EsEntrada, Funcion);
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TipoDeInventario, TiposDeUbicaciones.Todas, false, EsEntrada, Funcion);
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones__Ventas(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, TiposDeUbicaciones TipoDeUbicacion, bool EsEntrada, string Funcion )
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TipoDeInventario, TipoDeUbicacion, false, EsEntrada, Funcion);
        } 

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia,
            string IdCodigo, string IdCodEAN, TiposDeInventario TipoDeInventario, TiposDeUbicaciones TipoDeUbicacion, bool UbicacionesEstandar, bool EsEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Error al consultar LOTES de Producto.";
            string sMsjNoEncontrado = "LOTE de Producto no valido, Favor de verificar.";
            string sFiltroConsignacion = " ";
            string sFiltroSubFarmacia = " ";
            string sFiltroTipoDeUbicaciones = " ";
            string sFiltroUbicacionesEstandar = " ";

            switch (TipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and ClaveLote not like '%*%' ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and ClaveLote like '%*%' ";
                    break;
            }

            if (IdSubFarmacia != "")
            {
                sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia = '{0}' ", Fg.PonCeros(IdSubFarmacia, 3));
            }

            if (TipoDeUbicacion != TiposDeUbicaciones.Todas)
            {
                sFiltroTipoDeUbicaciones = string.Format(" and U.EsDePickeo = {0} ", (int)TipoDeUbicacion); 
            }

            if (UbicacionesEstandar)
            {
                sFiltroUbicacionesEstandar = string.Format("  And Exists (Select * From CFG_ALMN_Ubicaciones_Estandar E (NoLock)  \n " +
                                                                          " Where U.IdEmpresa = E.IdEmpresa and U.IdEstado = E.IdEstado and U.IdFarmacia = E.IdFarmacia and  \n " +
                                                                          " U.IdEstado = E.IdEstado and U.IdFarmacia = E.IdFarmacia And   \n " +
                                                                          " U.IdPasillo = E.IdRack and U.IdEstante = E.IdNivel and L.IdEntrepaño = E.IdEntrepaño) \n ");
            }


            sQuery = sInicio + string.Format("\n" +
                   "Select L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                   "\t   L.IdPasillo, L.IdEstante, L.IdEntrepaño as IdEntrepano, \n" +
                   "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\t   cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) as Existencia, \n" +
                   "\t   cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) as ExistenciaDisponible, \n" +
                   "\t0 as Cantidad \n" + 
                   "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner Join CatPasillos_Estantes_Entrepaños U (NoLock) \n" +
                   "\t\tOn ( L.IdPasillo = U.IdPasillo and L.IdEstante = U.IdEstante and L.IdEntrepaño = U.IdEntrepaño ) \n" + 
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                   "\t\tOn ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and \n" +
                   "\t\t\tL.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia )" +
                   "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                   "\tand L.IdProducto = '{3}' and CodigoEAN = '{4}' {5} {7} {8}\n" +
                   "Order by L.SKU, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                   IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia, sFiltroTipoDeUbicaciones, sFiltroUbicacionesEstandar);

            if( EsEntrada )
            {
                sQuery = sInicio + string.Format("\n" +
                       "Select L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, '' as SKU, L.ClaveLote, \n" +
                       "\t   L.IdPasillo, L.IdEstante, L.IdEntrepaño as IdEntrepano, \n" +
                       "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                       "\t   cast(sum(cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int)) as int) as Existencia, \n" +
                       "\t   cast(sum(cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int)) as int) as ExistenciaDisponible, \n" +
                       "\t0 as Cantidad \n" +
                       "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) \n" +
                       "Inner Join CatPasillos_Estantes_Entrepaños U (NoLock) \n" +
                       "\t\tOn ( L.IdPasillo = U.IdPasillo and L.IdEstante = U.IdEstante and L.IdEntrepaño = U.IdEntrepaño ) \n" +
                       "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                       "\t\tOn ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and \n" +
                       "\t\t\tL.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia )" +
                       "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                       "\tand L.IdProducto = '{3}' and CodigoEAN = '{4}' {5} {7} {8}\n" + 
                       "Group by \n" +
                       "\tL.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.IdPasillo, L.IdEstante, L.IdEntrepaño, \n" +
                       "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) \n" +
                       "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                       IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia, sFiltroTipoDeUbicaciones, sFiltroUbicacionesEstandar);
            }


            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones_CartasCanje(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia,
        string IdCodigo, string IdCodEAN, string sFolioCarta, TiposDeInventario TipoDeInventario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sFiltroConsignacion = " ";
            string sFiltroSubFarmacia = " ";

            switch (TipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and ClaveLote not like '%*%' ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and ClaveLote like '%*%' ";
                    break;
            }

            if (IdSubFarmacia != "")
            {
                sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia = '{0}' ", IdSubFarmacia);
            }

            sQuery = sInicio + string.Format("	" +
                   "Select \n" +
                   "\t   F.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                   "\t   IdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\t   (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\t   cast((R.Cant_Enviada) as Int) as Existencia, \n" +
                   "\t   cast((R.CantidadEnviada) as Int) as ExistenciaDisponible, \n" +
                   "0 as Cantidad \n" +
                   "From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner Join RutasDistribucionDet_CartasCanje R (NoLock) \n" +
                   "\t   On ( L.IdEmpresa = R.IdEmpresa And L.IdEstado = R.IdEstado and L.IdFarmacia = R.IdFarmacia and \n" +
                   "\t       L.IdProducto = R.IdProducto And L.CodigoEAN = R.CodigoEAN And L.SKU = R.SKU and L.IdSubFarmacia = R.IdSubFarmacia And L.ClaveLote = R.ClaveLote) \n" +
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                   "\t   On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  \n" +
                   "\t   and L.IdProducto = '{3}' and L.CodigoEAN = '{4}' {5} And R.FolioCarta = '{7}'   \n" +
                   "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                   IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia, sFolioCarta);
 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones_Movimiento 
            (string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            // string sFiltroConsignacion = " ";
            // string sFiltroSubFarmacia = " ";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tF.IdSubFarmacia, F.IdProducto, F.CodigoEAN, L.SKU, F.ClaveLote, \n" +
                   "\t    F.IdPasillo, F.IdEstante, F.IdEntrepaño as IdEntrepano, \n" +
                   "\t    (Case When F.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\t    cast(F.Existencia as Int) as Existencia, cast(L.Cantidad as int) as Cantidad \n" +
                   "From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) \n" +
                   "\t    On ( \n" +
                   "\t\t            L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.SKU = F.SKU and L.IdSubFarmacia = F.IdSubFarmacia \n" +
                   "\t\t            and L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote \n" +
                   "\t\t            and L.IdPasillo = F.IdPasillo and L.IdEstante = F.IdEstante and L.IdEntrepaño = F.IdEntrepaño  \n" +
                   "\t        ) \n" +
                   "Where L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.FolioMovtoInv = '{3}' \n" +
                   "Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                   IdEmpresa, IdEstado, IdFarmacia, FolioMovto);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones_TransferenciaDeSalida
            (string IdEmpresa, string IdEstado, string IdFarmacia, string FolioTransferencia, string Funcion)
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones_Transferencias(IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, Funcion); 
        }

        public DataSet LotesDeCodigo_CodigoEAN_Ubicaciones_TransferenciaDeEntrada
            (string IdEmpresa, string IdEstado, string IdFarmacia, string FolioTransferencia, string Funcion)
        {
            return LotesDeCodigo_CodigoEAN_Ubicaciones_Transferencias(IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, Funcion);
        }

        private DataSet LotesDeCodigo_CodigoEAN_Ubicaciones_Transferencias
            (string IdEmpresa, string IdEstado, string IdFarmacia, string FolioTransferencia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            // string sFiltroConsignacion = " ";
            // string sFiltroSubFarmacia = " ";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tL.IdSubFarmaciaEnvia as IdSubFarmacia, F.IdProducto, F.CodigoEAN, L.SKU, F.ClaveLote, \n" +
                   "\tF.IdPasillo, F.IdEstante, F.IdEntrepaño as IdEntrepano, \n" +
                   "\t(Case When F.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\tcast(F.Existencia as Int) as Existencia, cast(L.CantidadEnviada as int) as Cantidad \n" +
                   "From TransferenciasDet_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) \n" +
                   "\tOn ( " +
                   "\t\tL.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.SKU = F.SKU and L.IdSubFarmaciaEnvia = F.IdSubFarmacia \n" +
                   "\t\tand L.IdProducto = F.IdProducto and L.CodigoEAN = F.CodigoEAN and L.ClaveLote = F.ClaveLote \n" +
                   "\t\tand L.IdPasillo = F.IdPasillo and L.IdEstante = F.IdEstante and L.IdEntrepaño = F.IdEntrepaño  \n" +
                   "\t) \n" +
                   "Where L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' and L.FolioTransferencia = '{3}' \n" +
                   "Order by L.IdSubFarmaciaEnvia, L.IdProducto, L.CodigoEAN, L.ClaveLote \n",
                   IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet FolioDetLotes_Ventas_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select IdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad \n" +
                   "From vw_VentasDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioDetLotes_VentasDev_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, \n" +
                   "\tcast(Cantidad as Int) as Existencia, \n" +
                   "\tcast(Cantidad as Int) as ExistenciaDisponible, \n" +
                   "\t0 as Cantidad \n" +
                   "From vw_VentasDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVenta);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioDetLotes_Compras_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad \n" +
                   "From vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioCompra);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioDetLotes_ComprasDev_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, \n" +
                   "\tcast(Cantidad as Int) as Existencia, \n" +
                   "\tdbo.fg_ALMN_DisponibleDevolucion_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad) as ExistenciaDisponible, \n" +
                   "\t0 as Cantidad \n" +
                   "From vw_ComprasDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioCompra);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet FolioDetLotes_Pedidos_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("	" +
                   " Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                   "    IdPasillo, IdEstante, IdEntrepaño as IdEntrepano, " +
                   "    Status, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad " +
                   " From vw_PedidosDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " +
                   " Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }        
        ////////////////////////////////// 
        #endregion Manejo de Ubicaciones 

        #region Ajustes De Inventarios 
        public DataSet AjusteInventario(string IdEmpresa, string IdEstado, string IdFarmacia, string Poliza, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de la Póliza.";
            string sMsjNoEncontrado = "Póliza de ajuste de inventario no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * " + 
                " From vw_AjustesInv_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Poliza = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Poliza);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet AjusteInventario_Detalle(string IdEmpresa, string IdEstado, string IdFarmacia, string Poliza, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de la Póliza.";
            string sMsjNoEncontrado = "Póliza de ajuste de inventario no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, DescProducto, TasaIva, ExistenciaFisica, PrecioBase, Costo, Importe, 0, 0, " +
                " UnidadDeSalida, ExistenciaActualFarmacia " +
                " From vw_AjustesInv_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Poliza = '{3}' Order By KeyxDetalle ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Poliza);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet AjusteInventario_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de la Póliza.";
            string sMsjNoEncontrado = "Póliza de ajuste de inventario no contiene Lotes, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, StatusFarmaciaLote as Status, ExistenciaActualFarmacia as Existencia, ExistenciaFisica as Cantidad \n" +
                "From vw_AjustesInv_Det_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Poliza = '{3}' \n" +
                "Order By KeyxDetalleLote \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioMovto);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet AjusteInventario_Lotes_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioMovto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de la Póliza.";
            string sMsjNoEncontrado = "Póliza de ajuste de inventario no contiene ubicaciones de Lotes, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tF.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\t(Case When L.StatusDet_Lotes = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                   "\tcast(L.ExistenciaActualFarmacia as Int) as Existencia, ExistenciaFisica as Cantidad \n" +
                   "From vw_AjustesInv_Det_Lotes_Ubicaciones L (NoLock) \n" +
                   "Inner join CatFarmacias_SubFarmacias F (NoLock) \n" +
                   "\tOn ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) \n" +
                   "Where L.IdEmpresa = '{0}' and L.IdEstado = '{1}' and L.IdFarmacia =  '{2}' and L.Poliza = '{3}' \n" +
                   "Order By KeyxDetalleLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioMovto);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Ajustes De Inventarios 


        #region Reubicaicones
        public DataSet Reubicacion_Confirmacion(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la reubicación";
            string sMsjNoEncontrado = "Folio de Reubicación no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select * " + 
                "From vw_Ctrl_Reubicaciones C (NoLock) " +
                "Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' And C.Folio_Inv = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Reubicaicones

        #endregion Inventarios

        #region Cambios Físicos de Productos por carta canje 
        public DataSet CambiosCartaCanje_EmpresasRelacionados(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = string.Format(sInicio +
                "Select C.IdEmpresa_Relacionada, ( C.IdEmpresa_Relacionada + ' -- ' + E.Nombre ) as Empresa_Relacionada \n" +
                "From CambiosCartasCanje_AlmacenesRelacionados C (NoLock) \n" +
                "Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) \n" +
                "Inner Join CatEmpresas E (NoLock) On ( C.IdEmpresa_Relacionada = E.IdEmpresa ) \n" + 
                "Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}' and C.Status = 'A' \n" +
                "Group by C.IdEmpresa_Relacionada, ( C.IdEmpresa_Relacionada + ' -- ' + E.Nombre ) \n",
                IdEmpresa, IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosCartaCanje_EstadosRelacionados(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = string.Format(sInicio +
                "Select C.IdEstado_Relacionado, F.Estado, C.IdEmpresa_Relacionada, ( C.IdEstado_Relacionado  + ' -- ' + F.Estado ) as Estado_Relacionado \n" +
                "From CambiosCartasCanje_AlmacenesRelacionados C (NoLock) \n" +
                "Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) \n" +
                "Inner Join CatEmpresas E (NoLock) On ( C.IdEmpresa_Relacionada = E.IdEmpresa ) \n" + 
                "Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}' and C.Status = 'A' \n" +
                "Group by C.IdEstado_Relacionado, F.Estado, C.IdEmpresa_Relacionada, ( C.IdEstado_Relacionado  + ' -- ' + F.Estado ) \n",
                IdEmpresa, IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosCartaCanje_AlmacenesRelacionados( string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = string.Format(sInicio +
                "Select C.*, ( C.IdFarmacia_Relacionada + ' -- ' + F.Farmacia ) as AlmacenRelacionado \n" + 
                "From CambiosCartasCanje_AlmacenesRelacionados C (NoLock) \n" +
                "Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado_Relacionado = F.IdEstado and C.IdFarmacia_Relacionada = F.IdFarmacia ) \n" +
                "Where C.IdEmpresa = '{0}' and C.IdEstado = '{1}' and C.IdFarmacia = '{2}' and C.Status = 'A' \n",
                IdEmpresa, IdEstado, IdFarmacia);
                myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }

        public DataSet CambiosCartaCanjeEncabezado(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = string.Format(sInicio + "Select * From vw_CambiosCarta_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}'",
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosCartaCanjeDetalle(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Detalle no Encontrado, verifique.";

            sQuery = string.Format(
                "Select \n"+
                "\tCodigoEAN As 'Codigo EAN / Int',  IdProducto as 'Codigo', DescripcionProducto As 'Descripción', \n" +
                "\tTasaIva, Cantidad, Costo, Importe \n" + 
                "From vw_CambiosCarta_Det_CodigosEAN  (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosCartaCanjeDetallesLotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();

            sQuery = string.Format(
                "Select \n" +
                "\tL.IdSubFarmacia, L.SubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) As MesesCad, L.FechaReg, L.FechaCad, \n" +
                "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                "\tcast(L.Existencia as Int) as Existencia, Cantidad  \n" +
                "From vw_CambiosCarta_Det_CodigosEAN_Lotes L (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8));
            myDataset = (DataSet)EjecutarQuery(sQuery, "vw_CambiosProv_Det_CodigosEAN_Lotes");

            return myDataset;
        }
        #endregion Cambios Físicos de Productos por carta canje

        #region Cambios Físicos de Productos por Proveedor
        public DataSet CambiosProveedorEncabezado(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";

            sQuery = string.Format(sInicio + "Select * From vw_CambiosProv_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}'",
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosProveedorDetalle(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            string sMsjNoEncontrado = "Detalle no Encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" + 
                "\tCodigoEAN As 'Codigo EAN / Int',  IdProducto as 'Codigo', DescripcionProducto As 'Descripción', \n" +
                "\tTasaIva, Cantidad, Costo, Importe \n" +
                "From vw_CambiosProv_Det_CodigosEAN  (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}' \n", 
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CambiosProveedorDetallesLotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Tipo, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            // string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Cambio por Proveedor";
            // string sMsjNoEncontrado = "Lotes del Detalle no Encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tL.IdSubFarmacia, L.SubFarmacia, L.IdProducto, L.CodigoEAN, L.SKU, L.ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) As MesesCad, L.FechaReg, L.FechaCad, \n" +
                "\t(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, \n" +
                "\tcast(L.Existencia as Int) as Existencia, Cantidad  \n" +
                "From vw_CambiosProv_Det_CodigosEAN_Lotes L (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCambio = '{3}{4}'  \n",
                IdEmpresa, IdEstado, IdFarmacia, Tipo, Fg.PonCeros(Folio, 8)); 
            myDataset = (DataSet)EjecutarQuery(sQuery, "vw_CambiosProv_Det_CodigosEAN_Lotes");

            return myDataset;
        }
        #endregion Cambios Físicos de Productos por Proveedor

        #region CortesParciales
        public DataSet PersonalCorte(string IdEstado, string IdFarmacia, string IdPersonal, string sPassword, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Personal";
            string sMsjNoEncontrado = "Clave de Personal no encontrada, verifique.";

            if (IdEstado == "")
                IdEstado = "%%";
            else
                IdEstado = Fg.PonCeros(IdEstado, 2);

            if (IdFarmacia == "")
                IdFarmacia = "%%";
            else
                IdFarmacia = Fg.PonCeros(IdFarmacia, 4);

            if (IdPersonal == "")
                IdPersonal = "%%";
            else
                IdPersonal = Fg.PonCeros(IdPersonal, 4);

            sQuery = sInicio + string.Format(" Select a.*, b.LoginUser, b.Password, (a.ApPaterno + ' ' + a.ApMaterno + ' ' + a.Nombre) as NombreCompleto " +
                "From CatPersonal a(NoLock) " +
                "Inner Join Net_Usuarios b(NoLock) On (a.IdEstado = b.IdEstado And a.IdFarmacia = b.IdSucursal And a.IdPersonal = b.IdPersonal ) " +
                "Where a.IdEstado = '{0}' and a.IdFarmacia = '{1}' and a.IdPersonal = '{2}'  " // + 
                //"And b.Password = '{3}' ", IdEstado, IdFarmacia, IdPersonal, sPassword );
                , IdEstado, IdFarmacia, IdPersonal, sPassword);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        //public DataSet CortesParciales_Status(string IdEmpresa, string IdEstado, string IdFarmacia, string FechaSistema, string IdPersonal, string Funcion)
        //{
        //    return CortesParciales_Status(IdEmpresa, IdEstado, IdFarmacia, FechaSistema, IdPersonal, true, Funcion);
        //}

        public DataSet CortesParciales_Status(string IdEmpresa, string IdEstado, string IdFarmacia, 
            string FechaSistema, string IdPersonal, bool MostrarVacio, string StatusCorte, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al verificar si el usuario puede efectuar el Corte Parcial";
            string sMsjNoEncontrado = "Usted no puede realizar el Corte Parcial debido a que ya ha efectuado uno o no ha generado ninguna venta.";

            sQuery = sInicio + string.Format(" Select * From CtlCortesParciales (NoLock) " +
                                            " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                            " And Convert( varchar(10), FechaSistema, 120 ) = '{3}' " +
                                            " And IdPersonal = '{4}' and Status = '{5}' " +
                                            " Order by FechaCierre Desc ",
                                            IdEmpresa, IdEstado, IdFarmacia, FechaSistema, IdPersonal, StatusCorte);
            //  -- And Status = 'A' 
            bMostrarMsjLeerVacio = MostrarVacio;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CortesParciales_Status_InicioSesion(string IdEmpresa, string IdEstado, string IdFarmacia,
            string FechaSistema, string IdPersonal, bool MostrarVacio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al verificar si el usuario puede efectuar el Corte Parcial";
            string sMsjNoEncontrado = "Usted no puede realizar el Corte Parcial debido a que ya ha efectuado uno o no ha generado ninguna venta.";

            sQuery = sInicio + string.Format(" Select Status From CtlCortesParciales (NoLock) " +
                                            " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                            " And Convert( varchar(10), FechaSistema, 120 ) = '{3}' " +
                                            " And IdPersonal = '{4}' " +
                                            " Order by FechaCierre Desc ",
                                            IdEmpresa, IdEstado, IdFarmacia, FechaSistema, IdPersonal);
            //  -- And Status = 'A' 
            bMostrarMsjLeerVacio = MostrarVacio;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CortesDiarios_Status(string IdEmpresa, string IdEstado, string IdFarmacia, string FechaSistema, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al verificar si el usuario puede efectuar el Corte Parcial";
            string sMsjNoEncontrado = "Existen sesiones de corte abierta. Cierre estas sesiones en la opción Cortes Parciales para continuar.";

            sQuery = sInicio + string.Format(" Select * From CtlCortesParciales (NoLock) " +
                                            " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                            " And Convert( varchar(10), FechaSistema, 120 ) = '{3}' " + 
                                            " And Status = 'A' ", IdEmpresa, IdEstado, IdFarmacia, FechaSistema );
            bMostrarMsjLeerVacio = false; 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true; 

            return myDataset;
        }

        public DataSet CortesParciales_Datos(string IdEmpresa, string IdEstado, string IdFarmacia, string FechaSistema, string IdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del corte parcial";
            string sMsjNoEncontrado = "Ocurrió un error al obtener los datos del corte parcial, verifique.";

            sQuery = sInicio + string.Format(" Exec spp_Mtto_CortesParciales_Datos '{0}', '{1}', '{2}', '{3}', '{4}' ", IdEmpresa, IdEstado, IdFarmacia, FechaSistema, IdPersonal);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion CortesParciales

        #region Consulta de Existencia en Farmacias ( Web ) 
        public DataSet DireccionFarmacias(string IdEstado, string Funcion)
        {
            return DireccionFarmacias(IdEstado, "*", Funcion);
        }

        public DataSet DireccionFarmacias(string IdEstado, string Columnas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de farmacias";
            string sMsjNoEncontrado = "No se encontró información de farmacias, verifique.";

            sQuery = sInicio + string.Format(" Select {0} From vw_Farmacias_Urls (NoLock) Where IdEstado = '{1}' Order by IdEstado, IdFarmacia ", Columnas, IdEstado);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 
        #endregion Consulta de Existencia en Farmacias ( Web )

        #region Consulta a Servidor Central ( Web ) 
        public DataSet DireccionServidorCentralDeFarmacia(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Dirección del Servidor.";
            string sMsjNoEncontrado = "No se encontró información de Dirección, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_OficinaCentralRegional_Url (NoLock) ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }
        #endregion Consulta a Servidor Central ( Web )

        #region Datos de Hospital
        public DataSet Servicios(string Funcion)
        {
            return Servicios("", Funcion);
        }

        public DataSet Servicios(string IdServicio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Servicio";
            string sMsjNoEncontrado = "Clave de Servicio no encontrada, verifique.";
            string sWhere = "";

            if (IdServicio != "")
                sWhere = string.Format(" Where IdServicio = '{0}'", Fg.PonCeros(IdServicio, 3));

            sQuery = sInicio + string.Format(" Select *, ( IdServicio + ' -- ' + Descripcion ) as NombreServicio " + 
                " From CatServicios (NoLock) {0} Order by IdServicio ", sWhere );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ServiciosAreas(string IdServicio, string IdArea, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Área";
            string sMsjNoEncontrado = "Clave de Área no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatServicios_Areas (NoLock) Where IdServicio = '" + Fg.PonCeros(IdServicio, 3) + "' " +
                               " And IdArea = '" + Fg.PonCeros(IdArea, 3) + "' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TiposDeDispensacion(string IdTipoDispensacion, string ClaveDispensacionRecetasVales, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Tipos de Dispensación";
            string sMsjNoEncontrado = "Clave de Tipo de Dispensación no encontrada, verifique.";
            string sFiltro = string.Format(" and IdTipoDeDispensacion <> '{0}' ", ClaveDispensacionRecetasVales);
            int iTipoUnidad = Convert.ToInt32(!DtGeneral.EsAlmacen);
            iTipoUnidad = DtGeneral.EsModuloUnidosisEnEjecucion ? 1 : iTipoUnidad; 


            if (IdTipoDispensacion != "")
            {
                sFiltro = string.Format(" and IdTipoDeDispensacion = '{0}' ", Fg.PonCeros(IdTipoDispensacion, 2));
            }

            sQuery = sInicio + string.Format(
                " Select *, (IdTipoDeDispensacion + ' -- ' + Descripcion) as Dispensacion " + 
                " From CatTiposDispensacion (NoLock) " +
                " Where 1 = 1 and EsDeFarmacia = '{0}' {1} ", iTipoUnidad, sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet UnidadesMedicas(string IdUMedica, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Unidad Médica";
            string sMsjNoEncontrado = "Clave de Unidad Médica no encontrada, verifique.";

            sQuery = sInicio + " Select * From vw_Unidades_Medicas (NoLock) Where IdUMedica = '" + Fg.PonCeros(IdUMedica, 6) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet UnidadesMedicasEstado(string IdEstado, string Funcion)
        {
            return UnidadesMedicasJurisccion(IdEstado, "", "", "");
        }


        public DataSet UnidadesMedicasJurisccion(string IdEstado, string IdJurisdiccion, string IdUMedica, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Unidades Médicas";
            string sMsjNoEncontrado = "Clave de Unidad Médica no encontrada, verifique.";
            // string sFiltro = "";

            string sWhereUMedica = "";

            if (IdUMedica != "")
            {
                sWhereUMedica = string.Format(" and IdUMedica = '{0}'", Fg.PonCeros(IdUMedica, 6));
            }

            sQuery = sInicio + string.Format(" Select *, (Clues + '  ' + NombreuMedica) As CluesNombreuMedica From vw_Unidades_Medicas (NoLock) " +
                " Where IdEstado in ( '00', '{0}' ) {2}", 
                IdEstado, IdJurisdiccion, sWhereUMedica);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet UnidadesMedicasEstado_SIGHO(string IdEstado, string Funcion) 
        {
            return UnidadesMedicasJurisccion_SIGHO(IdEstado, "", "", "");
        }

        public DataSet UnidadesMedicasJurisccion_SIGHO(string IdEstado, string IdJurisdiccion, string IdUMedica, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Unidades Médicas";
            string sMsjNoEncontrado = "Clave de Unidad Médica no encontrada, verifique.";
            // string sFiltro = "";

            string sWhereUMedica = "";

            if (IdUMedica != "")
            {
                sWhereUMedica = string.Format(" and IdUMedica = '{0}'", Fg.PonCeros(IdUMedica, 6));
            }

            sQuery = sInicio + string.Format(" Select Distinct C.*, (Clues + '  ' + NombreuMedica) As CluesNombreuMedica " +
                "From INT_RE_SIGHO__CFG_Farmacias_UMedicas S (NoLock) " +
                "Inner Join vw_Unidades_Medicas C (NoLock) On (Referencia_SIADISSEP = CLUES) " +
                " Where C.IdEstado in ( '00', '{0}' ) {2}",
                IdEstado, IdJurisdiccion, sWhereUMedica);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet UnidadesMedicasEstado_INTERMED(string IdEstado, string Funcion)
        {
            return UnidadesMedicasJurisccion_INTERMED(IdEstado, "", "", "");
        }

        public DataSet UnidadesMedicasJurisccion_INTERMED(string IdEstado, string IdJurisdiccion, string IdUMedica, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Unidades Médicas";
            string sMsjNoEncontrado = "Clave de Unidad Médica no encontrada, verifique.";
            // string sFiltro = "";

            string sWhereUMedica = "";

            if (IdUMedica != "")
            {
                sWhereUMedica = string.Format(" and IdUMedica = '{0}'", Fg.PonCeros(IdUMedica, 6));
            }

            sQuery = sInicio + string.Format(" Select Distinct C.*, (Clues + '  ' + NombreuMedica) As CluesNombreuMedica " +
                "From INT_RE_INTERMED__CFG_Farmacias_UMedicas S (NoLock) " +
                "Inner Join vw_Unidades_Medicas C (NoLock) On (Referencia_SIADISSEP = CLUES) " +
                " Where C.IdEstado in ( '00', '{0}' ) {2}",
                IdEstado, IdJurisdiccion, sWhereUMedica);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Medicos(string IdEstado, string IdFarmacia, string IdMedico, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Médico";
            string sMsjNoEncontrado = "Clave de Médico no encontrada, verifique.";

            sQuery = sInicio + " Select *, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as NombreCompleto From vw_Medicos (NoLock) " +
                " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                " And IdMedico = '" + Fg.PonCeros(IdMedico, 6) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Especialidades(string IdEspecialidad, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Especialidad";
            string sMsjNoEncontrado = "Clave de Especialidad no encontrada, verifique.";
            string sWhere = "";

            if (IdEspecialidad != "")
                sWhere = string.Format(" Where IdEspecialidad = '{0}'", Fg.PonCeros(IdEspecialidad, 4));

            sQuery = sInicio + string.Format(" Select *, ( IdEspecialidad + ' -- ' + Descripcion ) as NombreEspecialidad " +
                " From CatEspecialidades (NoLock) {0} Order by IdEspecialidad ", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Beneficiarios( string IdEstado, string IdFarmacia, string IdBeneficiario, string Funcion )
        {
            return Beneficiarios(IdEstado, IdFarmacia, "", "", IdBeneficiario, Funcion); 
        }

        public DataSet Beneficiarios(string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string IdBeneficiario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Beneficiario";
            string sMsjNoEncontrado = "Clave de Beneficiario no encontrada, verifique.";
            string sFiltro = "";

            if(IdCliente != "" && IdSubCliente != "")
            {
                sFiltro = string.Format(" And B.IdCliente = '{0}' and B.IdSubCliente = '{1}' ", IdCliente, IdSubCliente);
            }

            sQuery = sInicio + string.Format(
                "Select B.*, (case when IsNull(I.IdBeneficiario, 0) = 0 then 0 else 1 end) as TieneIdentificacion \n" +
                "From vw_Beneficiarios B (NoLock) \n" +
                "Left Join CatBeneficiarios_Identificacion I (NoLock) \n" +
                "\t\tOn ( B.IdEstado = I.IdEstado and B.IdFarmacia = I.IdFarmacia and B.IdCliente = I.IdCliente and B.IdSubCliente = I.IdSubCliente and B.IdBeneficiario = I.IdBeneficiario ) \n" + 
                "Where B.IdEstado = '{0}' And B.IdFarmacia = '{1}'  {2}  \n" +
                "And ( B.IdBeneficiario = '{3}' or B.FolioReferencia = '{4}' ) ", 
                IdEstado, IdFarmacia, sFiltro, Fg.PonCeros(IdBeneficiario, 8), IdBeneficiario );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Beneficiarios_Domicilio(string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string IdBeneficiario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Beneficiario";
            string sMsjNoEncontrado = "Clave de Beneficiario no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select B.NombreCompleto, (case when IsNull(D.IdBeneficiario, '') = '' Then 0 else 1 end) as TieneDomicilio, " + 
                " D.* \n" +
                " From vw_Beneficiarios B (NoLock) \n " +  
                "	Left Join vw_Beneficiarios_Domicilios D (NoLock) " +
                "       ON (B.IdEstado = D.IdEstado And B.IdFarmacia = D.IdFarmacia And B.IdCliente = D.IdCliente And B.IdSubCliente = D.IdSubCliente " +
                "       	And B.IdBeneficiario = D.IdBeneficiario) \n" +                
                " Where B.IdEstado = '{0}' And B.IdFarmacia = '{1}' And B.IdCliente = '{2}' " + " And B.IdSubCliente = '{3}' " + 
                " And ( B.IdBeneficiario = '{4}' or B.FolioReferencia = '{5}' ) ",
                IdEstado, IdFarmacia, IdCliente, IdSubCliente, Fg.PonCeros(IdBeneficiario, 8), IdBeneficiario); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet BeneficiosSP(string IdBeneficio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Beneficio";
            string sMsjNoEncontrado = "Clave de Beneficio no encontrada, verifique.";
            // string sWhere = "";

            // ClaveDiagnostico vaida = 4 Caracteres de longitud 
            sQuery = sInicio + string.Format(" Select IdBeneficio, ClaveBeneficio, Descripcion, Tipo, Status " +
                " From CatBeneficios (NoLock) " +
                " where IdBeneficio = '{0}' ", Fg.PonCeros(IdBeneficio, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Beneficiarios_Tipos(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Tipo de Beneficiarios";
            string sMsjNoEncontrado = "Clave de Tipo de Beneficiario no encontrada, verifique.";
            // string sWhere = "";

            // ClaveDiagnostico valida = 4 Caracteres de longitud 
            sQuery = sInicio + string.Format(" Select IdTipoBeneficiario, ( right('0000' + cast(IdTipoBeneficiario as varchar(3)), 3) + ' - ' + Descripcion ) as TipoBeneficiario " +
                " From CatTiposDeBeneficiarios (NoLock) " +
                " Order by IdTipoBeneficiario ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CURPS_Genericas( string IdEstado, string IdFarmacia, string CURP, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Tipo de Beneficiarios";
            string sMsjNoEncontrado = "Clave de Tipo de Beneficiario no encontrada, verifique.";
            // string sWhere = "";

            // ClaveDiagnostico valida = 4 Caracteres de longitud 
            sQuery = sInicio + string.Format(
                "Select * \n" +
                "From CFGC_CurpsGenericas (NoLock) \n" +
                "Where IdEstado = '{0}' and IdFarmacia = '{1}' and CURP = '{2}' \n" +
                "Order by IdCurp \n", IdEstado, IdFarmacia, CURP);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DiagnosticosCIE10(string ClaveDiagnostico, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Diagnóstico";
            string sMsjNoEncontrado = "Clave de Diagnóstico no encontrada, verifique.";
            // string sWhere = "";

            // ClaveDiagnostico vaida = 4 Caracteres de longitud 
            sQuery = sInicio + string.Format(" Select * " + 
                "From CatCIE10_Diagnosticos (NoLock) " +
                " where ClaveDiagnostico = '{0}'  and  len(ClaveDiagnostico) <= {1} ", ClaveDiagnostico, GnFarmacia.ClaveDiagnosticoCaracteres + 1);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }        
        #endregion Datos de Hospital

        #region Almacenes Jurisdiccionales 
        public DataSet CentrosDeSalud(string IdEstado, string IdCentro, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Centro de Salud";
            string sMsjNoEncontrado = "Centro de Salud no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatCentrosDeSalud (NoLock) " +
                "Where IdEstado = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                " And IdCentro = '" + Fg.PonCeros(IdCentro, 4) + "' ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioEnc_ALMJ_Pedidos_RC(string IdEmpresa, string IdEstado, string IdJurisdiccion, string IdFarmacia, string FolioPedidoRC, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_ALMJ_Pedidos_RC (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdJurisdiccion = '{2}' and IdFarmacia = '{3}' and FolioPedidoRC = '{4}' ",
                IdEmpresa, IdEstado, IdJurisdiccion, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioPedidoRC, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FolioDet_ALMJ_Pedidos_RC(string IdEmpresa, string IdEstado, string IdJurisdiccion, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Detalles de los Folios de Compras";
            string sMsjNoEncontrado = "Detalles del Folio de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select ClaveSSA, IdClaveSSA, DescripcionSal, Cantidad " +
                " From vw_ALMJ_Pedidos_RC_Det PD (NoLock) " +
                " Where PD.IdEmpresa =  '{0}' and PD.IdEstado = '{1}' and IdJurisdiccion = '{2}' and PD.IdFarmacia = '{3}' and PD.FolioPedidoRC = '{4}' ",
                IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, FolioCompra);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Almacenes Jurisdiccionales

        #region Datos Configurables por Farmacia
        public DataSet Farmacia_Clientes(string IdPublicoGeneral, string IdEstado, string IdFarmacia, string IdCliente, string Funcion)
        {
            return Farmacia_Clientes(IdPublicoGeneral, false, IdEstado, IdFarmacia, IdCliente, "", Funcion);
        }

        public DataSet Farmacia_Clientes(string IdPublicoGeneral, string IdEstado, string IdFarmacia, 
            string IdCliente, string IdSubCliente, string Funcion)
        {
            return Farmacia_Clientes(IdPublicoGeneral, false, IdEstado, IdFarmacia, IdCliente, IdSubCliente, Funcion);
        }

        public DataSet Farmacia_Clientes(string IdPublicoGeneral, bool EsPublicoGral, string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string Funcion)
        {
            // Se muestran solo los clientes de Credito registrados para la Farmacia 

            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Clientes";
            string sMsjNoEncontrado = "Información de Clientes no encontrada, \nó el Cliente no esta asignado a la Farmacia, verifique.";
            string sWhere = "";
            string sWherePubGral = "";

            IdCliente = Fg.PonCeros(IdCliente, 4);
            if ( IdSubCliente != "" )
                sWhere = string.Format(" and IdSubCliente = '{0}' ", Fg.PonCeros(IdSubCliente,4) );

            if (!EsPublicoGral)
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral); 

            sQuery = sInicio + string.Format(" Select distinct * From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and StatusRelacion = 'A' {3} {4} ",
                 IdEstado, IdFarmacia, IdCliente, sWhere, sWherePubGral);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_Clientes_Programas(string IdPublicoGeneral, string IdEstado,
            string IdFarmacia, string IdCliente, string IdSubCliente, string IdPrograma, string Funcion)
        {
            return Farmacia_Clientes_Programas(IdPublicoGeneral, false, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, "", Funcion);
        }

        public DataSet Farmacia_Clientes_Programas(string IdPublicoGeneral, string IdEstado, 
            string IdFarmacia, string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma, string Funcion)
        {
            return Farmacia_Clientes_Programas(IdPublicoGeneral, false, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Funcion);
        }

        public DataSet Farmacia_Clientes_Programas(string IdPublicoGeneral, bool EsPublicoGral, string IdEstado, string IdFarmacia, 
            string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma, string Funcion)
        {
            // Se muestran solo los clientes de Credito registrados para la Farmacia 

            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Programa";
            string sMsjNoEncontrado = "Información de Programa no encontrada, el Programa no esta asignado a la Farmacia, verifique.";
            string sWhere = "";
            string sWherePubGral = "";
            string sWhereFarmacia = ""; 

            IdCliente = Fg.PonCeros(IdCliente, 4);
            IdSubCliente = Fg.PonCeros(IdSubCliente, 4);

            IdPrograma = Fg.PonCeros(IdPrograma, 4);
            if (IdSubPrograma != "")
            {
                sWhere = string.Format(" and IdSubPrograma = '{0}' ", Fg.PonCeros(IdSubPrograma, 4));
            }

            if (!EsPublicoGral)
            {
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral);
            }

            if (IdFarmacia != "")
            {
                sWhereFarmacia = string.Format(" and IdFarmacia = '{0}'", IdFarmacia);
            } 

            sQuery = sInicio + string.Format(" Select distinct * " + 
                " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                " Where IdEstado = '{0}'  {1}  and IdCliente = '{2}' and IdSubCliente = '{3}' and IdPrograma = '{4}' and StatusRelacion = 'A' {5} {6} ",
                 IdEstado, sWhereFarmacia, IdCliente, IdSubCliente, IdPrograma, sWhere, sWherePubGral);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_Clientes_Programas_Dispensacion(string IdEstado, string IdFarmacia, string Funcion)
        {
            // Se muestran solo los clientes de Credito registrados para la Farmacia 

            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Programa";
            string sMsjNoEncontrado = "Información de Programa no encontrada, el Programa no esta asignado a la Farmacia, verifique."; 


            sQuery =
                sInicio + string.Format(" Select Distinct ( IdCliente + IdSubCliente + IdPrograma + IdSubPrograma ) as Clave_Dispensacion, " + 
                " IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma " + 
                " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and StatusRelacion = 'A'  ",
                IdEstado, IdFarmacia );

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_Servicios(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Servicios";
            string sMsjNoEncontrado = "Información de Servicios no encontrada, verifique.";

            sQuery = sInicio + string.Format(" 	Select Distinct S.IdServicio, S.Servicio, ( S.IdServicio + ' -- ' + S.Servicio) As NombreServicio " + 
	                " From vw_Servicios_Areas S " + 
	                " Inner Join CatServicios_Areas_Farmacias C On ( S.IdServicio = C.IdServicio and S.IdArea = C.IdArea ) " + 
	                " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' " + 
                    " Order by S.Servicio ", IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_ServiciosAreas(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Áreas";
            string sMsjNoEncontrado = "Información de Áreas no encontrada, verifique.";

            sQuery = sInicio + string.Format(" 	Select Distinct S.IdServicio, S.Servicio, S.IdArea, S.Area_Servicio, ( S.IdArea + ' -- ' + S.Area_Servicio ) as NombreArea " +
                    " From vw_Servicios_Areas S " + 
                    " Inner Join CatServicios_Areas_Farmacias C On ( S.IdServicio = C.IdServicio and S.IdArea = C.IdArea ) " +
                    " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' " + 
                    " Order By S.Area_Servicio " , IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Datos Configurables por Farmacia

        #region Tiempo Aire 
        public DataSet CompaniasTA(string Funcion)
        {
            return CompaniasTA("", Funcion);
        }

        public DataSet CompaniasTA(string IdCompania, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Compañia de Tiempo Aire";
            string sMsjNoEncontrado = "No se encontraron Compañias de Tiempo Aire, verifique.";
            string sWhereCompania = "";

            if (IdCompania != "")
                sWhereCompania = string.Format(" and IdCompania = '{0}' ", Fg.PonCeros(IdCompania, 2));

            sQuery = sInicio + string.Format(" Select * From CatCompaniasTiempoAire (NoLock) " +
                " Where 1 = 1  {0} ", sWhereCompania);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CompaniasTA_Montos(string Funcion)
        {
            return CompaniasTA_Montos("", "", Funcion);
        }

        public DataSet CompaniasTA_Montos(string IdCompania, string Funcion)
        {
            return CompaniasTA_Montos(IdCompania, "", Funcion);
        }

        public DataSet CompaniasTA_Montos(string IdCompania, string IdMonto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Montos de la Compañia de Tiempo Aire";
            string sMsjNoEncontrado = "No se encontraron Montos para esta compañia de tiempo aire, verifique.";
            string sWhereCompania = "";
            string sWhereMonto = "";

            if (IdCompania != "")
                sWhereCompania = string.Format(" and IdCompania = '{0}' ", Fg.PonCeros(IdCompania, 2));

            if (IdMonto != "")
            {
                sWhereMonto = string.Format(" and IdMonto = '{0}' ", Fg.PonCeros(IdMonto, 2));
                sMsjNoEncontrado = "Clave de Monto no encontrada para esta compañia de tiempo aire, verifique.";
            }

            sQuery = sInicio + string.Format(" Select IdMonto, Descripcion, Monto, ( Case When Status = 'A' Then 1 Else 0 End ) as Status  " +
                               "From CatCompaniasTA_Montos (NoLock) Where 1 = 1  {0} {1}", sWhereCompania, sWhereMonto);
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PersonalTA(string IdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Personal";
            string sMsjNoEncontrado = "Clave de Personal no encontrada, verifique.";

            sQuery = sInicio + " Select * From CatPersonalTA (NoLock) " +
                " Where IdPersonal = '" + Fg.PonCeros(IdPersonal, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Tiempo Aire

        #region Operaciones Supervizadas 
        public DataSet OperacionesSupervidas(string IdOperacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Operación Supervizada";
            string sMsjNoEncontrado = "Operación Supervizada no encontrada, verifique.";

            sQuery = sInicio + " Select * From Net_Operaciones_Supervisadas (NoLock) " +
                " Where IdOperacion = '" + Fg.PonCeros(IdOperacion, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Operaciones Supervizadas

        #region CatalogosSAT

        public DataSet CFD_UnidadesDeMedida(string IdUnidad, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From FACT_CFD_UnidadesDeMedida C (NoLock) Where C.IdUnidad = '{0}' ", IdUnidad); // + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Productos_Servicios(string Clave, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From FACT_CFDI_Productos_Servicios C (NoLock) Where C.Clave = '{0}' ", Clave); // + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion CatalogosSAT

        #region CFDI

        public DataSet CFDI_Documentos(string IdEmpresa, string IdEstado, string IdFarmacia, string Serie, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Documento no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select *, " +
                                             "dbo.fg_FACT_NotasDeCredito_TasaIva(IdEmpresa, IdEstado, IdFarmacia, Serie, Folio) As TasaIva, " +
                                             "dbo.fg_FACT_NotasDeCredito_Total_Previo(IdEmpresa, IdEstado, IdFarmacia, Serie, Folio) As Anterior " +
                                             "From vw_FACT_CFD_DocumentosElectronicos (NoLock) " +
                                             "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Serie = '{3}' And Folio = '{4}' ",
                                             IdEmpresa, IdEstado, IdFarmacia, Serie, Folio); // + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion CFDI


        #region Asignacion de Precios Claves SSA
        public DataSet ClientesClavesSSA_Asignadas(string IdCliente, string Funcion)
        {
            return ClientesClavesSSA_Asignadas(IdCliente, "", Funcion); 
        }

        public DataSet ClientesClavesSSA_Asignadas(string IdCliente, string IdSubCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";
            string sFiltro = "";

            if (IdSubCliente != "")
                sFiltro = string.Format(" and P.IdSubCliente = '{0}' ", Fg.PonCeros(IdSubCliente, 4));  

            sQuery = sInicio + string.Format(" Select Distinct C.* " + 
                " From vw_Clientes_SubClientes C (NoLock) " +
                " Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdCliente = P.IdCliente and C.IdSubCliente = P.IdSubCliente )  " +
                " Where C.IdCliente = '{0}' {1} and P.Status = 'A' ", Fg.PonCeros(IdCliente,4), sFiltro); // + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClientesClavesSSA_Asignadas_Precios(string IdEstado, string IdCliente, string IdSubCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";

            sQuery = sInicio + string.Format(
                "Select Distinct \n" +
                "\tC.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal, \n" +
                "\tC.TipoDeClaveDescripcion, C.ContenidoPaquete, \n" +
                "\tcast(round((M.Precio / (C.ContenidoPaquete * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario, \n" +
                "\tIsNull(M.Precio, 0) as Precio, ContenidoPaquete_Licitado, IdPresentacion_Licitado, R.Descripcion As Presentacion, Dispensacion_CajasCompletas, \n" +
                "\t(case when M.Status = 'A' then 'Activo' else 'Cancelado' end) as Status, \n" +
                "\tM.SAT_UnidadDeMedida, U.Descripcion As Descripcion_SAT_UnidadDeMedida, \n" +
                "\tM.SAT_ClaveDeProducto_Servicio, S.Descripcion As Descripcion_SAT_ClaveDeProducto_Servicio \n" +
                "From vw_ClavesSSA_Sales C (NoLock) \n" +
                "Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Left Join CFG_ClavesSSA_Precios M (NoLock) On ( M.IdEstado = '{2}' and M.IdCliente = P.IdCliente and M.IdSubCliente = P.IdSubCliente and M.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Left Join CatPresentaciones R (NoLock) On ( M.IdPresentacion_Licitado = R.IdPresentacion ) \n" +
                "Left Join FACT_CFD_UnidadesDeMedida U (NoLock) On ( M.SAT_UnidadDeMedida = U.IdUnidad ) \n" +
                "Left Join FACT_CFDI_Productos_Servicios S (NoLock) On ( M.SAT_ClaveDeProducto_Servicio = S.Clave ) \n" +
                "Where P.IdCliente = '{0}' and P.IdSubCliente = '{1}' \n",
                Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), Fg.PonCeros(IdEstado, 2)); // + Fg.PonCeros(IdCliente, 4) + "'";

            //////sQuery = sInicio + string.Format(
            //////    " Select IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Precio, Status " + 
            //////    " From " + 
            //////    " ( " + 
            //////        " Select Distinct C.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal, " +
            //////        " IsNull(M.Precio, 0) as Precio, (case when M.Status = 'A' then 'Activo' else 'Cancelado' end) as Status, IsNull(M.IdEstado, '') as IdEstado " +
            //////        " From vw_ClavesSSA_Sales C (NoLock) " +
            //////        " Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdClaveSSA_Sal = P.IdClaveSSA_Sal )  " +
            //////        " Left Join CFG_ClavesSSA_Precios M (NoLock) On ( M.IdCliente = P.IdCliente and M.IdSubCliente = P.IdSubCliente and M.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) " +
            //////        " Where P.IdCliente = '{0}' and P.IdSubCliente = '{1}' " +
            //////    " ) as T Where Id Estado = '{2}' ",
            //////    Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), Fg.PonCeros(IdEstado, 2)); // + Fg.PonCeros(IdCliente, 4) + "'";

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            return myDataset; 
        }

        public DataSet ClientesClavesSSA_Asignadas_Precios__DosisUnitaria(string IdEstado, string IdCliente, string IdSubCliente, string Funcion)
        {
            return ClientesClavesSSA_Asignadas_Precios__DosisUnitaria(IdEstado, IdCliente, IdSubCliente, "", Funcion);
        }
        public DataSet ClientesClavesSSA_Asignadas_Precios__DosisUnitaria(string IdEstado, string IdCliente, string IdSubCliente, string IdClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";
            string sFiltro = IdClaveSSA != "" ? string.Format(" and C.IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA, 6)) : "";

            sQuery = sInicio + string.Format(
                "Select Distinct \n" +
                "\tC.IdClaveSSA_Sal, C.ClaveSSA, C.DescripcionSal, \n" +
                "\tC.TipoDeClaveDescripcion, CB.Precio, M.Factor as Factor_DosisUnitaria, M.Precio as Precio_DosisUnitaria, \n" +
                "\t(case when M.Status = 'A' then 'Activo' else 'Cancelado' end) as Status \n" +
                "From vw_ClavesSSA_Sales C (NoLock) \n" +
                "Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Inner Join CFG_ClavesSSA_Precios CB (NoLock) On ( CB.IdEstado = '{2}' and CB.IdCliente = P.IdCliente and CB.IdSubCliente = P.IdSubCliente and CB.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Inner Join CFG_ClavesSSA_Unidosis_Precios M (NoLock) On ( M.IdEstado = '{2}' and M.IdCliente = P.IdCliente and M.IdSubCliente = P.IdSubCliente and M.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Left Join CatPresentaciones R (NoLock) On ( CB.IdPresentacion_Licitado = R.IdPresentacion ) \n" +
                "Left Join FACT_CFD_UnidadesDeMedida U (NoLock) On ( CB.SAT_UnidadDeMedida = U.IdUnidad ) \n" +
                "Left Join FACT_CFDI_Productos_Servicios S (NoLock) On ( CB.SAT_ClaveDeProducto_Servicio = S.Clave ) \n" +
                "Where P.IdCliente = '{0}' and P.IdSubCliente = '{1}' {3} \n",
                Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), Fg.PonCeros(IdEstado, 2), sFiltro); // + Fg.PonCeros(IdCliente, 4) + "'";

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            return myDataset;
        }

        public DataSet ClientesClavesSSA_Asignadas_Precios_Programa(string IdEstado, string IdCliente, string IdSubCliente,string IdClaveSSA_Sal, string IdPrograma, string idSubPrograma, string Funcion)
        {
            string sWhere = string.Format("And P.IdPrograma = '{0}' And P.IdSubPrograma = '{1}'", IdPrograma, idSubPrograma);

            return ClientesClavesSSA_Asignadas_Precios_Programa(IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, sWhere, false, Funcion);
        }

        public DataSet ClientesClavesSSA_Asignadas_Precios_Programa(string IdEstado, string IdCliente, string IdSubCliente, string IdClaveSSA_Sal, string Funcion)
        {
            return ClientesClavesSSA_Asignadas_Precios_Programa(IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, "", true, Funcion);
        }

        public DataSet ClientesClavesSSA_Asignadas_Precios_Programa(string IdEstado, string IdCliente, string IdSubCliente, string IdClaveSSA_Sal, string sWhere, bool MostrarMsjLeerVacio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Claves no encontradas, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tP.IdPrograma, S.Programa, P.IdSubPrograma , S.SubPrograma, ClaveSSA, DescripcionSal, Precio, \n" +
                "\t(case when P.Status = 'A' then 'Activo' else 'Cancelado' end) as Status \n" +
                "From CFG_ClavesSSA_Precios_Programas P (NoLock) \n" +
                "Inner Join vw_ClavesSSA_Sales V (NoLock) On ( P.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) \n" +
                "Inner Join vw_Programas_SubProgramas S (NoLock) On ( P.IdPrograma = S.IdPrograma And P.IdSubPrograma = S.IdSubPrograma ) \n" +
                "Where  P.IdEstado = '{0}' And P.IdCliente = '{1}' and P.IdSubCliente = '{2}' And P.IdClaveSSA_Sal = '{3}' {4} \n",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), IdClaveSSA_Sal, sWhere);
  
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            return myDataset;
        }

        public DataSet ClavesSSA_Asignadas_A_Clientes(string IdCliente, string IdSubCliente, string IdClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";
            // string sFiltro = "";

            sQuery = sInicio + string.Format(
                "Select Distinct \n\tC.*, \n" +
                "\tIsNull(M.Precio, 0) as Precio, \n" +
                "\t(IsNull(M.Precio, 0)) as PrecioCaja, \n" +
                "\tcast(round((M.Precio / (C.ContenidoPaquete * 1.00)), 4) as numeric(14, 4)) as PrecioUnitario, \n" +
                "\tcast(IsNull(M.Precio, 0) as bit) as PrecioAsignado, cast(IsNull(M.Factor, 0) as Int) as Factor, \n" +
                "\tContenidoPaquete_Licitado, IdPresentacion_Licitado, R.Descripcion As Presentacion, Dispensacion_CajasCompletas, \n" +
                "\tM.SAT_UnidadDeMedida, U.Descripcion As Descripcion_SAT_UnidadDeMedida, \n" +
                "\tM.SAT_ClaveDeProducto_Servicio, S.Descripcion As Descripcion_SAT_ClaveDeProducto_Servicio \n" +
                "From vw_ClavesSSA_Sales C (NoLock) \n" +
                "Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdClaveSSA_Sal = P.IdClaveSSA_Sal )  \n" +
                "Left Join CFG_ClavesSSA_Precios M (NoLock) On ( M.IdCliente = P.IdCliente and M.IdSubCliente = P.IdSubCliente and M.IdClaveSSA_Sal = P.IdClaveSSA_Sal ) \n" +
                "Left Join CatPresentaciones R (NoLock) On ( M.IdPresentacion_Licitado = R.IdPresentacion ) \n" +
                "Left Join FACT_CFD_UnidadesDeMedida U (NoLock) On ( M.SAT_UnidadDeMedida = U.IdUnidad ) \n" +
                "Left Join FACT_CFDI_Productos_Servicios S (NoLock) On ( M.SAT_ClaveDeProducto_Servicio = S.Clave ) \n" +
                "Where P.IdCliente = '{0}' and P.IdSubCliente = '{1}' and C.IdClaveSSA_Sal = '{2}' \n",
                Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), Fg.PonCeros(IdClaveSSA, 6)); // + Fg.PonCeros(IdCliente, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Asignadas_A_ClaveSSA(string IdEstado, string IdCliente, string IdSubCliente, string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos.";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";
            // string sFiltro = "";

            sQuery = sInicio + string.Format(
                "Select * \n" +
                "From vw_Relacion_ClavesSSA_Claves (NoLock) \n" +
                "Where IdEstado = '{0}' and IdCliente = '{1}' and IdSubCliente = '{2}' and ClaveSSA = '{3}' \n",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4), ClaveSSA ); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Asignacion de Precios Claves SSA

        #region Consultas Globales
        public string Obtener_Url_ServidorCentral(string Funcion)
        {
            string sUrl = "";
            clsLeer myLeer;
            myLeer = new clsLeer();

            string sMsjError = "Ocurrió un error al obtener la URL del Servidor Central Regional";
            string sMsjNoEncontrado = "URL de Servidor Central Regional no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_OficinaCentral_Url (NoLock) ");
            myLeer.DataSetClase = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            if (myLeer.Leer())
            {
                sUrl = myLeer.Campo("Url");
                bExistenDatos = true; 
            }

            return sUrl;
        }

        public string Obtener_Url_ServidorCentralRegional(string Funcion)
        {
            string sUrl = "";
            clsLeer myLeer;
            myLeer = new clsLeer();

            string sMsjError = "Ocurrió un error al obtener la URL del Servidor Central Regional";
            string sMsjNoEncontrado = "URL de Servidor Central Regional no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_OficinaCentralRegional_Url (NoLock) ");
            myLeer.DataSetClase = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            if (myLeer.Leer())
            {
                sUrl = myLeer.Campo("Url");
                bExistenDatos = true; 
            }

            return sUrl;
        }
        #endregion Consultas Globales

        #region Clientes Secretaria
        public DataSet Unidad_Usuarios(string IdEstado, string IdFarmacia, string IdUsuario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Usuario";
            string sMsjNoEncontrado = "Clave de Usuario no encontrada, verifique.";

            if (IdEstado == "")
            {
                IdEstado = "%%";
            }
            else
            {
                IdEstado = Fg.PonCeros(IdEstado, 2);
            }

            if (IdFarmacia == "")
            {
                IdFarmacia = "%%";
            }
            else
            {
                IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            }

            if (IdUsuario == "")
            {
                IdUsuario = "%%";
            }
            else
            {
                IdUsuario = Fg.PonCeros(IdUsuario, 4);
            }

            sQuery = sInicio + string.Format(" Select * From Net_Regional_Usuarios (NoLock) " + 
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdUsuario = '{2}' ", IdEstado, IdFarmacia, IdUsuario);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Regional_Usuarios(string IdEstado, string IdFarmacia, string IdUsuario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Usuario";
            string sMsjNoEncontrado = "Clave de Usuario no encontrada, verifique.";

            if (IdEstado == "")
            {
                IdEstado = "%%";
            }
            else
            {
                IdEstado = Fg.PonCeros(IdEstado, 2);
            }

            if (IdFarmacia == "")
            {
                IdFarmacia = "%%";
            }
            else
            {
                IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            }

            if (IdUsuario == "")
            {
                IdUsuario = "%%";
            }
            else
            {
                IdUsuario = Fg.PonCeros(IdUsuario, 4);
            }

            sQuery = sInicio + string.Format(" Select * From Net_Regional_Usuarios (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdUsuario = '{2}' ", IdEstado, IdFarmacia, IdUsuario);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); 

            return myDataset;
        }
        #endregion Clientes Secretaria 

        #region Funciones_Controlados_Antibioticos
        public DataSet Sales_Antibioticos_Controlados(string IdClaveSSA_Sal, string Funcion)
        {
            return Sales_Antibioticos_Controlados(IdClaveSSA_Sal, false, false, false, false, Funcion);
        }

        public DataSet Sales_Antibioticos_Controlados(string IdClaveSSA_Sal, bool BuscarPorClaveSSA, bool BuscarPorProducto,
            bool EsControlado, bool EsAntibiotico, string Funcion)
        {
            myDataset = new DataSet();
            string sExtra = "";
            string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal ó Producto";
            string sMsjNoEncontrado = "Clave de Sal ó Producto no encontrada, verifique.";

            sFiltro = string.Format(" Where IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA_Sal, 4));
            if (BuscarPorClaveSSA)
            {
                sFiltro = string.Format(" Where  ClaveSSA = '{0}'  ", IdClaveSSA_Sal);
            }
            if (BuscarPorProducto)
            {
                sFiltro = string.Format(" Where  IdProducto = '{0}'  ", Fg.PonCeros(IdClaveSSA_Sal, 8));
            }
            if (EsControlado)
            {
                sExtra = " And EsControlado = 1  ";
            }
            if (EsAntibiotico)
            {
                sExtra = " And EsAntibiotico = 1  ";
            }

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Productos_CodigoEAN (NoLock)   {0} {1}  ", sFiltro, sExtra);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Funciones_Controlados_Antibioticos

        #region Pedidos_Distribuidores 
        public DataSet Folio_Pedidos_CEDIS_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " + 
                " From vw_Impresion_Pedidos_Cedis (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_Pedidos_CEDIS_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select ClaveSSA, IdClaveSSA, DescripcionClave, Presentacion, Existencia, Cantidad, ContenidoPaquete, CantidadEnCajas " +
                " From vw_Impresion_Pedidos_Cedis (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_Pedidos__Surtidos_Enc( string IdEmpresa, string IdEstado, string IdFarmacia, string FolioSurtido, string Funcion )
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select *, (case when Status in ( 'A', 'S' ) then 1 else 0 end) as HabilitadoParaSurtido\n" +
                "From vw_PedidosCedis_Surtimiento (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioSurtido = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioSurtido, 8)); 

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string Folio_PedidosEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            sQuery = sInicio + string.Format(" Select * From vw_PedidosEnc (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                            IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            return sQuery; 
        }

        public DataSet Folio_PedidosEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string TipoEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";
            FolioPedido = TipoEntrada + Fg.PonCeros(FolioPedido, 8); 

            sQuery = Folio_PedidosEnc(IdEmpresa, IdEstado, IdFarmacia, FolioPedido);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string Folio_PedidosDet(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
                " CD.TasaIva, CD.CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " +
                " CD.Costo, 0 as Importe " +
                " From vw_PedidosDet_CodigosEAN CD (NoLock) " +
                " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioPedido);
            return sQuery;
        }

        public DataSet Folio_PedidosDet(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pedido no encontrado, verifique.";

            sQuery = Folio_PedidosDet(IdEmpresa, IdEstado, IdFarmacia, FolioPedido);
            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string Folio_PedidosDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {
            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, " +
                " FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad " +
                " From vw_PedidosDet_CodigosEAN_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);

            return sQuery;
        }

        public DataSet Folio_PedidosDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Pedidos.";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";


            sQuery = Folio_PedidosDet_Lotes(IdEmpresa, IdEstado, IdFarmacia, FolioPedido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet Distribuidores(string IdDistribuidor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Distribuidor";
            string sMsjNoEncontrado = "Clave de Distribuidor no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatDistribuidores (NoLock) " + 
                " Where IdDistribuidor = '{0}' ",  Fg.PonCeros(IdDistribuidor, 4) );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnvioEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_PedidosEnvioEnc (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnvioDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, Costo, Importe, 0, 0, UnidadDeSalida " +
                " From vw_PedidosEnvioDet_CodigosEAN (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnvioLotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, FechaReg, FechaCad, Status, Existencia, Cantidad " +
                " From vw_PedidosEnvioDet_CodigosEAN_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnvioLotes_Entrada(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string IdFarmaciaConectada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            string sCondicion = string.Format(
                "And Cast( CodigoEAN + ClaveLote as varchar ) Not In " +
                "( " +
                "	Select Cast( CodigoEAN + ClaveLote as varchar ) as Codigo From FarmaciaProductos_CodigoEAN_Lotes(NoLock) " +
                "	Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Existencia > 0 " +
                ") ", IdEmpresa, IdEstado, IdFarmaciaConectada);

            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, FechaReg, FechaCad, Status, Existencia, Cantidad " +
                " From vw_PedidosEnvioDet_CodigosEAN_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " + sCondicion,
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosDistEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Salida no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_PedidosDistEnc (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosDistDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, DescProducto, TasaIva, Cantidad, Costo, Importe, 0, 0, UnidadDeEntrada " +
                " From vw_PedidosDistDet (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosDistDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select IdSubFarmaciaEnvia as IdSubFarmacia, SubFarmaciaEnvia as SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCaducidad) as MesesCad, FechaRegistro as FechaReg, FechaCaducidad as FechaCad, " +
                " StatusLote as Status, Existencia, Cantidad " +
                " From vw_PedidosDistDet_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RepositorioDistribuidor(string IdDistribuidor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Repositorio del Distribuidor";
            string sMsjNoEncontrado = "Clave de Distribuidor no encontrada, verifique.";

            sQuery = sInicio + string.Format(
                " Select R.IdDistribuidor, D.NombreDistribuidor as Distribuidor, R.Direccion, R.Usuario, R.Password, R.Status, R.Actualizado " + 
                " From CatDIST_Repositorios R (NoLock) " +
                " Inner Join CatDistribuidores D (NoLock) On ( R.IdDistribuidor = D.IdDistribuidor ) " +
                " Where R.IdDistribuidor = '{0}' ", Fg.PonCeros(IdDistribuidor, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DistribuidorProductos(string IdProductoDist, string Funcion)
        {
            myDataset = new DataSet(); 
            string sMsjError = "Ocurrió un error al obtener los datos del Producto del Distribuidor";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Productos_Distribuidor V (NoLock) " +
                " Where V.IdProductoDist = '{0}' ",
                Fg.PonCeros(IdProductoDist, 10));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DistribuidorProductos_ClaveSSA(string IdProductoDist, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto del Distribuidor";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Distribuidor_ClaveSSA V (NoLock) " +
                " Where V.IdProductoDist = '{0}' ",
                IdProductoDist);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_ClaveSSA_Datos(string IdClaveSSA_Sal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los Códigos EAN del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\t'ClaveInterna' = IdClaveSSA_Sal, 'Clave SSA' = ClaveSSA_Base, 'Descripción Sal' = DescripcionSal, \n" +
                "\tClaveSSA_Base, DescripcionSal, IdClaveSSA_Sal \n" +
                "From vw_ClavesSSA_Sales (NoLock) \n" + 
                "Where IdClaveSSA_Sal = '{0}' \n", IdClaveSSA_Sal);
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DistribuidorProductos(string IdDistribuidor, string IdProductoDist, string TipoCodigo, string Funcion) 
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto del Distribuidor";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sFiltroTipoCodigo = ""; 

            if ( TipoCodigo != "" )
            {
                sFiltroTipoCodigo = string.Format("     and V.TipoCodigo = {0}", TipoCodigo); 
            }

            sQuery = sInicio + string.Format(" Select * " + 
                " From vw_Productos_Distribuidor V (NoLock) " +
                " Where V.IdDistribuidor = '{0}' and V.IdProductoDist = '{1}' {2} ",
                Fg.PonCeros(IdDistribuidor, 4), Fg.PonCeros(IdProductoDist, 10), sFiltroTipoCodigo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DistribuidoresClientes(string IdDistribuidor, string ClaveCliente, string TipoCodigo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave de Destino de Distribuidor";
            string sMsjNoEncontrado = "Clave de Destino no encontrada, verifique.";
            string sFiltroTipoCodigo = "";

            if (TipoCodigo != "")
            {
                sFiltroTipoCodigo = string.Format("     and V.TipoCliente = {0}", TipoCodigo);
            }

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Distribuidor_Clientes V (NoLock) " +
                " Where V.IdDistribuidor = '{0}' and V.ClaveCliente = '{1}' {2} ",
                Fg.PonCeros(IdDistribuidor, 4), ClaveCliente, sFiltroTipoCodigo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_Pedidos_ABASTO_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio de Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Pedidos_Dist_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioPedido = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Pedidos_Dist_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FolioPedido = '{2}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_Pedidos_ABASTO_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdProductoDist, CodigoDistribuidor, CodigoEAN, ClaveSSA, NombreComercial, Cantidad " +
                " From vw_Pedidos_Dist_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioPedido = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            sQuery = sInicio + string.Format(" Select IdProductoDist, CodigoDistribuidor, CodigoEAN, ClaveSSA, NombreComercial, Cantidad " +
                " From vw_Pedidos_Dist_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FolioPedido = '{2}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(FolioPedido, 6)); 

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_Pedidos_ABASTO_Det_ClaveSSA(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdProductoDist, CodigoDistribuidor, IdClaveSSA, ClaveSSA, NombreComercial, Cantidad " +
                " From vw_Pedidos_Dist_Det_ClaveSSA (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FolioPedido = '{2}' " +
                " Order By IdProductoDist ",
                IdEmpresa, IdEstado, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Exportar_Pedido_Excel(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            return Exportar_Pedido_Excel(IdEmpresa, IdEstado, IdFarmacia, FolioPedido, FolioPedido, Funcion); 
        }

        public DataSet Exportar_Pedido_Excel(string IdEmpresa, string IdEstado, string IdFarmacia,
            string FolioPedidoInicial, string FolioPedidoFinal,  string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio"; 
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            ////sQuery = sInicio + string.Format(
            ////    " Select 'Codigo Cliente' = ClaveCliente, 'Nombre Cliente' = NombreCliente, " +
            ////    " ClaveSSA, cast(CodigoEAN as bigint) as CodigoEAN, 'Codigo' = CodigoDistribuidor, " + 
            ////    " 'Descripción' = DescripcionClave, 'Nombre comercial' = NombreComercial, 'Solicitud' = Cantidad " +
            ////    " From vw_Pedidos_Dist_Det (NoLock) " +
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioPedido Between '{3}' and '{4}' ",
            ////    IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedidoInicial, 6), Fg.PonCeros(FolioPedidoFinal, 6));


            sQuery = sInicio + string.Format(
                " Select 'Codigo Cliente' = ClaveCliente, 'Nombre Cliente' = NombreCliente, " +
                " ClaveSSA, cast(CodigoEAN as bigint) as CodigoEAN, 'Codigo' = CodigoDistribuidor, " +
                " 'Descripción' = DescripcionClave, 'Nombre comercial' = NombreComercial, 'Solicitud' = Cantidad " +
                " From vw_Pedidos_Dist_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FolioPedido Between '{2}' and '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(FolioPedidoInicial, 6), Fg.PonCeros(FolioPedidoFinal, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Exportar_Pedido_Excel_ClaveSSA(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                " Select 'Codigo Cliente' = ClaveCliente, 'Nombre Cliente' = NombreCliente, " +
                " ClaveSSA, 'Codigo' = CodigoDistribuidor, " +
                " 'Descripción' = DescripcionClave, 'Nombre comercial' = NombreComercial, 'Solicitud' = Cantidad " +
                " From vw_Pedidos_Dist_Det_ClaveSSA (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FolioPedido = '{2}' " +
                " Order By Codigo",
                IdEmpresa, IdEstado, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_PedidosDet_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
                " CD.TasaIva, CD.CantidadRecibida, 0 As Cant_Devuelta, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " +
                " CD.Costo, 0 as Importe " +
                " From vw_PedidosDet_CodigosEAN CD (NoLock) " +
                " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioPedido);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_PedidosDet_LotesDev(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de pedidos.";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";


            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, " +
                " FechaReg, FechaCad, Status, " + 
                " CantidadRecibida as Existencia, " +
                " dbo.fg_ALMN_DisponibleDevolucion_Lotes(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, CantidadRecibida) as ExistenciaDisponible, " + 
                " 0 as Cantidad " +
                " From vw_PedidosDet_CodigosEAN_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet Folio_PedidosDet_LotesDevUbicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de pedidos.";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique."; 

            sQuery = sInicio + string.Format("	" +
                   " Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                   "    IdPasillo, IdEstante, IdEntrepaño as IdEntrepano, " +
                   "    Status, " + 
                   " cast(Cantidad as Int) as Existencia, " +
                   " dbo.fg_ALMN_DisponibleDevolucion_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad) as ExistenciaDisponible, " + 
                   " 0 as Cantidad " +
                   " From vw_PedidosDet_CodigosEAN_Lotes_Ubicaciones  (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " +
                   " Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet PedidosOrdenDist_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Impresion_Pedidos_Orden_Distribuidor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosOrdenDist_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select ClaveSSA, IdClaveSSA, DescripcionClave, Cantidad, ContenidoPaquete, CantidadEnCajas " +
                " From vw_Impresion_Pedidos_Orden_Distribuidor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Distribuidor_Configuracion(string IdEmpresa, string IdEstado, string IdFarmacia, string IdDistribuidor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Clave de Distribuidor no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From CFGC_ConfigurarDistribuidor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdDistribuidor = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(IdDistribuidor, 4));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RutasDistribucionEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ruta";
            string sMsjNoEncontrado = "Clave de Ruta no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_RutasDistribucionEnc Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'",
                                    IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet RutasDistribucionDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ruta";
            string sMsjNoEncontrado = "Detallado de Ruta no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select RutaDeDistribucion, FolioTransferenciaVenta, Fecha, FarmaciaRecibe As Ref, Bultos, Piezas,'T' As Tipo " +
                           "From vw_RutasDistribucionDetTrans Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                           "Union " +
                           "Select RutaDeDistribucion, FolioTransferenciaVenta, Fecha, Beneficiario As Ref, Bultos, Piezas,'V' As Tipo " +
                           "From vw_RutasDistribucionDetVentas Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'" 
                                    ,IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet DevolucionDeDoctosDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ruta";
            string sMsjNoEncontrado = "Detallado de Ruta no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select FolioTransferenciaVenta, Fecha, FarmaciaRecibe As Ref, FolioDevuelto, FolioDevuelto,'T' As Tipo " +
                           "From vw_DevolucionDeDoctosDetTrans Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' " +
                           "Union " +
                           "Select FolioTransferenciaVenta, Fecha, Beneficiario As Ref, FolioDevuelto, FolioDevuelto,'V' As Tipo " +
                           "From vw_DevolucionDeDoctosDetVentas Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}'"
                                    , IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet GenerarTransferenciaDetallesLotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            //string sFolio = Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tD.IdSubFarmacia, SubFarmacia,  D.IdProducto, D.CodigoEAN, D.SKU, D.ClaveLote, \n" +
                "\tdatediff(mm, getdate(), E.FechaCaducidad) as MesesCad, E.FechaRegistro As FechaReg, E.FechaCaducidad As FechaCad, E.Status, \n" +
                "\tCast((E.Existencia - E.ExistenciaEnTransito) As Int) As Existencia, Cast((Existencia - ExistenciaEnTransito) As int) As ExistenciaDisponible, Sum(D.CantidadAsignada) As Cantidad \n" +
                "From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) \n" +
                "Inner Join --vw_Farmacias_SubFarmacias S (NoLock) \n" +
                "\t( \n" +
                "\t\tSelect *, Descripcion as SubFarmacia  \n" +
                "\t\tFrom CatFarmacias_SubFarmacias X(NoLock)  \n" +
                "\t) S On(D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia) \n" +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes E (NoLock) \n" +
                "\tOn \n" + 
                "\t( \n" +
                "\t\tD.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmacia And \n" +
                "\t\tD.SKU = E.SKU and D.IdSubFarmacia = E.IdSubFarmacia And D.IdProducto = E.IdProducto And D.CodigoEAN = E.CodigoEAN And D.ClaveLote = E.ClaveLote \n" + 
                "\t) \n" +
                "Where D.IdEmpresa = '{0}' and D.IdEstado = '{1}' and D.IdFarmacia = '{2}' and D.FolioSurtido in ({3}) \n" +
                "Group By \n" +
                "\tD.IdSubFarmacia, SubFarmacia,  D.IdProducto, D.CodigoEAN, D.SKU, D.ClaveLote, datediff(mm, getdate(), E.FechaCaducidad), \n" +
                "\tE.FechaRegistro, E.FechaCaducidad, E.Status, E.Existencia, E.ExistenciaEnTransito\n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet GenerarTransferenciaDetallesLotesUbicacion(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            // string sFolio = Fg.PonCeros(IdEstado, 2) + Fg.PonCeros(IdFarmacia, 4) + ClaveRenapo + TipoTrans + Fg.PonCeros(Folio, 8); // 250001SLTS
            //string sFolio = Fg.PonCeros(Folio, 8); // 250001SLTS
            string sMsjError = "Ocurrió un error al obtener el folio de transferencia";
            string sMsjNoEncontrado = "No se encontró el folio de transferencia, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tD.IdSubFarmacia,  D.IdProducto, D.CodigoEAN, D.SKU, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño as IdEntrepano, \n" +
                "\t(Case When E.Status = 'A' Then 'Activo' Else 'Cancelado' End) As Status, Cast((E.Existencia - E.ExistenciaEnTransito) As Int) As Existencia, \n" +
                "\tCast((Existencia - ExistenciaEnTransito) As int) As ExistenciaDisponible, \n" +
                "\tSum(D.CantidadAsignada) As Cantidad \n" +
                "From Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) \n" +
                "----Inner Join vw_Farmacias_SubFarmacias S (NoLock) On (D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdSubFarmacia = S.IdSubFarmacia) \n" +
                "Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) \n" +
                "\tOn \n" + 
                "\t( \n" +
                "\t\tD.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmacia And \n" +
                "\t\tD.SKU = E.SKU and D.IdSubFarmacia = E.IdSubFarmacia And D.IdProducto = E.IdProducto And D.CodigoEAN = E.CodigoEAN And D.ClaveLote = E.ClaveLote And \n" +
                "\t\tD.IdPasillo = E.IdPasillo and D.IdEstante = E.IdEstante and D.IdEntrepaño = E.IdEntrepaño \n" +
                "\t) \n" +
                "Where D.IdEmpresa = '{0}' and D.IdEstado = '{1}' and D.IdFarmacia = '{2}' and D.FolioSurtido in ({3}) \n" +
                "Group By D.IdSubFarmacia,  D.IdProducto, D.CodigoEAN, D.SKU, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño, E.Status, E.Existencia, E.ExistenciaEnTransito",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Pedidos_Distribuidores

        #region Posicionamiento de Productos  
        public DataSet Pasillos(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Error al consultar Racks";
            string sMsjNoEncontrado = "Rack no encontrado. Favor de verificar.";

            sQuery = sInicio + string.Format(" Select * From CatPasillos (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, IdPasillo);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Pasillos_Estantes(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string IdEstante, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Error al consultar Niveles.";
            string sMsjNoEncontrado = "Nivel no encontrado. Favor de verificar.";

            sQuery = sInicio + string.Format(" Select * From CatPasillos_Estantes (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}' ",
                IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        public DataSet Pasillos_Estantes_Entrepaños(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string IdEstante, string IdEntrepano, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Error al consultar Posiciones.";
            string sMsjNoEncontrado = "Posición no encontrada. Favor de verificar.";

            sQuery = sInicio + string.Format(" Select * From CatPasillos_Estantes_Entrepaños (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                " And IdPasillo = '{3}' And IdEstante = '{4}' And IdEntrepaño = '{5}' ",
                IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepano);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string IdEstante, string IdEntrepano, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Ubicación";
            string sMsjNoEncontrado = "Clave de Pasillo no encontrada, verifique.";
            string sWherePasillo = "", sWhereEstante = "", sWhereEntrepano = "";

            if (IdPasillo != "")
            {
                sWherePasillo = string.Format("And IdPasillo = '{0}' ", IdPasillo);
            }

            if (IdEstante != "")
            {
                sWhereEstante = string.Format("And IdEstante = '{0}' ", IdEstante);
                sMsjNoEncontrado = "Clave de Estante no encontrada, verifique.";
            }

            if (IdEntrepano != "")
            {
                sWhereEntrepano = string.Format("And IdEntrepano = '{0}' ", IdEntrepano);
                sMsjNoEncontrado = "Clave de Entrepaño no encontrada, verifique.";
            }

            sQuery = sInicio + string.Format(" Select * From vw_Pasillos_Estantes_Entrepaños (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' {3} {4} {5} ",
                IdEmpresa, IdEstado, IdFarmacia, sWherePasillo, sWhereEstante, sWhereEntrepano);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Posicionamiento de Productos 
        
        #region Vales de Medicamento
        public DataSet ValesEmisionEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, bool bBuscarPorVenta, bool bValeReembolso, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Vale no encontrado, verifique.";
            string sTipoBusqueda = string.Format("And Folio = '{0}' ", FolioVale);

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVale = Fg.PonCeros(FolioVale, 8);

            if (bBuscarPorVenta)
            {
                sTipoBusqueda = string.Format("And FolioVenta = '{0}' ", FolioVale);
            }

            if (bValeReembolso)
            {
                sTipoBusqueda = string.Format("And FolioValeReembolso = '{0}' ", FolioVale);
            }

            sQuery = sInicio + string.Format("Select * From vw_Vales_EmisionEnc " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + sTipoBusqueda,
                IdEmpresa, IdEstado, IdFarmacia);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ValesEmisionDet(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Vale no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select ClaveSSA, IdClaveSSA_Sal, DescripcionCortaClave, IdPresentacion, Presentacion, Cantidad, CantidadSegundoVale " +
                " From vw_Vales_EmisionDet (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia  = '{2}' and Folio = '{3}' " +
                " Order By ClaveSSA ", IdEmpresa, IdEstado, IdFarmacia, FolioVale);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ValesEmision_InformacionAdicional(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información.";
            string sMsjNoEncontrado = "Información de Venta no encontrada, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVale = Fg.PonCeros(FolioVale, 8);

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Vales_Emision_InformacionAdicional (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVale);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }

        public DataSet ValesEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio.";
            string sMsjNoEncontrado = "Folio de Vale no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVale = Fg.PonCeros(FolioVale, 8);

            sQuery = sInicio + string.Format(" Select * " + 
                " From vw_ValesEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVale);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ValesDet(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Vale no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, DescProducto, " +
                " TasaIva, CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', IdProducto) as CostoPromedio, " +
                " Costo, 0 as Importe " +
                " From vw_ValesDet (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVale);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ValesDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Vale no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad \n" +
                "From vw_ValesDet_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioVale);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ValesServicioADomicilio(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVale, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Vale no encontrado, verifique.";
            string sTipoBusqueda = string.Format("And Folio = '{0}' ", FolioVale);
            bool bMensajeVacio = bMostrarMsjLeerVacio; 

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVale = Fg.PonCeros(FolioVale, 8);

            sQuery = sInicio + string.Format("Select * From vw_Vales_ServiciosADomicilio " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " ,
                IdEmpresa, IdEstado, IdFarmacia, FolioVale);

            bMostrarMsjLeerVacio = false; 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = bMensajeVacio; 

            return myDataset;
        }

        public DataSet Vales_Servicio_A_DomicilioEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioServicioDomicilio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Vale de Servicio a domicilio no encontrado, verifique.";
            string sTipoBusqueda = string.Format("And Folio = '{0}' ", FolioServicioDomicilio);
            bool bMensajeVacio = bMostrarMsjLeerVacio;

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioServicioDomicilio = Fg.PonCeros(FolioServicioDomicilio, 8);

            sQuery = sInicio + string.Format("Select * From vw_Vales_Servicio_A_Domicilio  " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio);

            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = bMensajeVacio;

            return myDataset;
        }

        public DataSet Vales_Servicio_A_DomicilioDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            bMostrarMsjLeerVacio = false;
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Vale de Servicio a domicilio no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, DescProducto, " +
                " TasaIva, CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', IdProducto) as CostoPromedio, " +
                " Costo, 0 as Importe " +
                " From vw_Vales_Servicio_A_DomicilioDet (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Vales_Servicio_A_DomicilioDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            bMostrarMsjLeerVacio = false;
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Vale de Servicio a domicilio no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                 " datediff(mm, getdate(), FechaCad) as MesesCad, " +
                 " FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad " +
                 " From vw_Vales_Servicio_A_DomicilioDet_Lotes (NoLock) " +
                 " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ",
                 IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Vales de Medicamento         

        #region Cuadros Basicos 
        public DataSet ClavesSSA_PreciosAsignados(string Estado, string Cliente, string IdClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ClaveSSA";
            string sMsjNoEncontrado = "ClaveSSA no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_Claves_Precios_Asignados (NoLock) Where IdEstado = '{0}' " +
            " And IdCliente = '{1}' And IdClaveSSA = '{2}' ", Estado, Cliente, Fg.PonCeros(IdClaveSSA, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_PreciosAsignados(string Estado, string Cliente, string SubCliente, string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la ClaveSSA";
            string sMsjNoEncontrado = "ClaveSSA no encontrada, verifique.";

            sQuery = sInicio + string.Format(
                " Select * " + 
                " From vw_Claves_Precios_Asignados (NoLock) " + 
                " Where IdEstado = '{0}' And IdCliente = '{1}' And IdSubCliente = '{2}' And ClaveSSA = '{3}' ",
            Estado, Cliente, SubCliente, ClaveSSA);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet NivelesAtencion(string Funcion)
        {
            return NivelesAtencion("", Funcion);
        }
        public DataSet NivelesAtencion(string IdEstado, string Funcion)
        {
            return NivelesAtencion(IdEstado, "", Funcion);
        }
        public DataSet NivelesAtencion(string IdEstado, string IdCliente, string Funcion)
        {
            return NivelesAtencion(IdEstado, IdCliente, "", Funcion);
        }
        public DataSet NivelesAtencion(string IdEstado, string IdCliente, string IdNivel, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Niveles de Atención";
            string sMsjNoEncontrado = "Nivel de Atencion no encontrado, verifique.";
            string sWhereEstado = "", sWhereCliente = "", sWhereNivel = "";

            if (IdEstado != "")
            {
                sWhereEstado = string.Format(" Where IdEstado = '{0}'", IdEstado);
            }

            if (IdCliente != "")
            {
                sWhereCliente = string.Format(" And IdCliente = '{0}'", IdCliente);
            }

            if (IdNivel != "")
            {
                sWhereNivel = string.Format(" And IdNivel = '{0}'", IdNivel);
            }

            sQuery = sInicio + string.Format(
                " Select * " + 
                " From vw_CB_NivelesAtencion (NoLock) " +
                " {0} {1} {2} ", sWhereEstado, sWhereCliente, sWhereNivel);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet NivelesAtencion_Miembros(string IdEstado, string IdCliente, string IdNivel, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Niveles de Atención";
            string sMsjNoEncontrado = "Miembros de Nivel de Atencion no encontrados, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CB_NivelesAtencion_Miembros (NoLock) " +
                " Where IdEstado = '{0}' And IdCliente = '{1}' And IdNivel = '{2}' ", IdEstado, IdCliente, IdNivel);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CuadrosBasicos(string IdEstado, string IdCliente, string IdNivel, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las Claves del Nivel de Atención";
            string sMsjNoEncontrado = "Claves de Nivel de Atención no encontrados, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdEstado, Estado, IdCliente, Cliente, IdNivel, Nivel, StatusNivel, " +
                " IdClaveSSA, ClaveSSA, DescripcionClave, 'Descripcion Clave' = DescripcionClave, DescripcionCortaClave, IdPresentacion, Presentacion, StatusMiembro " + 
                " From vw_CB_CuadroBasico_Claves (NoLock) " +
                " Where IdEstado = '{0}' And IdCliente = '{1}' And IdNivel = '{2}' ", IdEstado, IdCliente, IdNivel);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CuadrosBasicos_Farmacias(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCliente, string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave del Cuadro básico";
            string sMsjNoEncontrado = "Clave no encontrada en Cuadro básico, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdEstado, Estado, IdCliente, Cliente, IdNivel, Nivel, StatusNivel, " +
                " IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, DescripcionCortaClave, IdPresentacion, Presentacion, StatusMiembro,  " +
                " dbo.fg_Existencia_Clave('{0}', V.IdEstado, V.IdFarmacia, V.ClaveSSA) as ExistenciaClave,  " +
                " cast(dbo.fg_EsClaveFaltante(ClaveSSA) as int) as ClaveFaltante, " +
                " dbo.fg_EmiteVales_Clave('{1}', '{3}', '{4}' ) as EmiteVales, " +
                " dbo.fg_GetParametro_ValidarExistenciaEmisonVales('{1}', '{2}') as ValidarExistencia " + 
                " From vw_CB_CuadroBasico_Farmacias V (NoLock) " +
                " Where IdEstado = '{1}' And IdFarmacia = '{2}' And IdCliente = '{3}' and ClaveSSA = '{4}' ",
                IdEmpresa, IdEstado, IdFarmacia, IdCliente, ClaveSSA);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Cuadros Basicos

        #region Entradas de Consignacion
        public string EntradasEnc_Consignacion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada)
        {
            sQuery = sInicio + string.Format("Select * \n" + 
                "From vw_EntradasEnc_Consignacion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioEntrada, 8));
            return sQuery;
        }

        public DataSet EntradasEnc_Consignacion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Entrada de Consignación no encontrado, verifique.";

            sQuery = EntradasEnc_Consignacion(IdEmpresa, IdEstado, IdFarmacia, FolioEntrada);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string EntradasDet_Consignacion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada)
        {
            sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
                " CD.TasaIva, CD.CantidadRecibida, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " +
                " CD.Costo, 0 as Importe " +
                " From vw_EntradasDet_Consignacion_CodigosEAN CD (NoLock) " +
                " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioEntrada);
            return sQuery;
        }

        public DataSet EntradasDet_Consignacion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pedido de Consignación no encontrado, verifique.";

            sQuery = EntradasDet_Consignacion(IdEmpresa, IdEstado, IdFarmacia, FolioEntrada);
            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public string EntradasDet_Consignacion_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada)
        {
            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad \n" +
                "From vw_EntradasDet_Consignacion_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioEntrada);

            return sQuery;
        }

        public DataSet EntradasDet_Consignacion_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de Pedido de Consignación no encontrado, verifique.";


            sQuery = EntradasDet_Consignacion_Lotes(IdEmpresa, IdEstado, IdFarmacia, FolioEntrada);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet EntradasDet_Consignacion_Lotes_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioEntrada, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("	" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, \n" +
                   "\tcast(Cantidad as Int) as Existencia, \n" +
                   "\tdbo.fg_ALMN_DisponibleDevolucion_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad) as ExistenciaDisponible, \n" +
                   "\tcast(0 as int) as Cantidad \n" +
                   "From vw_EntradasDet_Consignacion_CodigosEAN_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioEntrada);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        #region Devolucion Entradas de Consignacion
        public DataSet EntradasDet_Consignacion_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select CD.CodigoEAN, CD.IdProducto, CD.DescProducto, " +
                " CD.TasaIva, CD.CantidadRecibida, 0 As Cant_Devuelta, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, " +
                " CD.Costo, 0 as Importe " +
                " From vw_EntradasDet_Consignacion_CodigosEAN CD (NoLock) " +
                " Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioPedido);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet EntradasDet_Consignacion_Lotes_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, \n" +
                "\tCantidadRecibida as Existencia, \n" +
                "\tdbo.fg_ALMN_DisponibleDevolucion_Lotes(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Cantidad) as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_EntradasDet_Consignacion_CodigosEAN_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Devolucion Entradas de Consignacion
        #endregion Entradas de Consignacion        
        
        #region Farmacias Convenio Vales 
        public DataSet FarmaciasConvenio(string Funcion)
        {
            return FarmaciasConvenio("", Funcion);
        }

        public DataSet FarmaciasConvenio(string IdEstado, string Funcion)
        {
            return FarmaciasConvenio(IdEstado, "", Funcion);
        }

        public DataSet FarmaciasConvenio(string IdEstado, string IdFarmaciaConvenio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las Farmacias Convenio";
            string sMsjNoEncontrado = "Farmacia Convenio no encontrada, verifique.";
            string sWhereEstado = "", sWhereFarmaciaConvenio = "";

            if (IdEstado != "")
            {
                sWhereEstado = string.Format(" Where IdEstado = '{0}'", Fg.PonCeros(IdEstado, 2));
            }

            if (IdFarmaciaConvenio != "")
            {
                sWhereFarmaciaConvenio = string.Format(" And IdFarmaciaConvenio = '{0}'", Fg.PonCeros(IdFarmaciaConvenio, 4));
            }

            sQuery = sInicio + string.Format( 
                " Select IdEstado, IdFarmaciaConvenio, Nombre, Direccion, Status " +
                " From CatFarmacias_ConvenioVales (NoLock) " +
                " {0} {1} ", sWhereEstado, sWhereFarmaciaConvenio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FarmaciasConvenio_Miembros(string Funcion)
        {
            return FarmaciasConvenio_Miembros("", Funcion);
        }

        public DataSet FarmaciasConvenio_Miembros(string IdEstado, string Funcion)
        {
            return FarmaciasConvenio_Miembros(IdEstado, "", Funcion);
        }

        public DataSet FarmaciasConvenio_Miembros(string IdEstado, string IdFarmacia, string Funcion)
        {
            return FarmaciasConvenio_Miembros(IdEstado, IdFarmacia, "", Funcion);
        }

        public DataSet FarmaciasConvenio_Miembros(string IdEstado, string IdFarmacia, string IdFarmaciaConvenio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Miembros de las Farmacias Convenio";
            string sMsjNoEncontrado = "Farmacia Convenio no encontrada, verifique.";
            string sWhereEstado = "", sWhereFarmacia = "", sWhereFarmaciaConvenio = "";

            if (IdEstado != "")
            {
                sWhereEstado = string.Format(" Where IdEstado = '{0}'", Fg.PonCeros(IdEstado, 2));
            }

            if (IdFarmacia != "")
            {
                sWhereFarmacia = string.Format(" And IdFarmacia = '{0}'", Fg.PonCeros(IdFarmacia, 4));
            }

            if (IdFarmaciaConvenio != "")
            {
                sWhereFarmaciaConvenio = string.Format(" And IdFarmaciaConvenio = '{0}'", Fg.PonCeros(IdFarmaciaConvenio, 4));
            }

            sQuery = sInicio + string.Format(
                " Select IdEstado, Estado, IdFarmacia, Farmacia, IdFarmaciaConvenio, FarmaciaConvenio, Direccion, Status " +
                " From vw_Vales_Farmacias_Convenio (NoLock) " +
                " {0} {1} {2} ", sWhereEstado, sWhereFarmacia, sWhereFarmaciaConvenio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FarmaciaValesHuellas(string IdEstado, string IdFarmacia, string IdPersona, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la persona";
            string sMsjNoEncontrado = "Persona no encontrada, verifique.";

            IdPersona = Fg.PonCeros(IdPersona, 8);

            sQuery = sInicio + string.Format( " Select * From Vales_Huellas " +
                    "Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdPersonaFirma = '{2}'", IdEstado, IdFarmacia, IdPersona);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Parentescos(string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la persona";
            string sMsjNoEncontrado = "Persona no encontrada, verifique.";
            string sWhere = "";

            if (Folio != "")
            {
                Folio = Fg.PonCeros(Folio, 2);
                sWhere = string.Format("And Folio = '{0}'", Folio);
            }
            else
            {
                sWhere = "And Status = 'A'";
            }


            sQuery = sInicio + string.Format(" Select * From CatParentescos Where 1 = 1 {0}", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Farmacias Convenio Vales 

        #region Titulos Validacion 
        public DataSet Validacion_Titulos_EncabezadoReportes(string IdEstado, string IdTitulo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Título";
            string sMsjNoEncontrado = "Clave de título no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                " Select * " +
                " From CFG_EX_Validacion_Titulos_Reportes P (Nolock) " + 
                " Where IdEstado = '{0}' and IdTitulo = '{1}' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdTitulo, 2) );

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Titulos Validacion

        #region Socios Comerciales

        public DataSet SociosComerciales(string IdSocioComercial, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del socio comercial";
            string sMsjNoEncontrado = "Clave de socio comercial no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                " Select * " +
                " From CatSociosComerciales P (Nolock) " +
                " Where IdSocioComercial = '{0}'",
                Fg.PonCeros(IdSocioComercial, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SociosComerciales_Sucursales(string IdSocioComercial, string Sucursal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la sucursal";
            string sMsjNoEncontrado = "Clave de la sucursal no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                " Select * " +
                " From CatSociosComerciales_Sucursales P (Nolock) " +
                " Where IdSocioComercial = '{0}' And IdSucursal = '{1}'",
                Fg.PonCeros(IdSocioComercial, 8), Fg.PonCeros(Sucursal, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet SalidasVentasSociosComercialesENC(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                " Select * " +
                " From vw_VentasEnc_SociosComerciales P (Nolock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}'",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SalidasVentasSociosComercialesDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tCodigoEAN, IdProducto, Descripcion, TasaIva, CantidadVendida, Precio, Precio \n" +
                "From vw_VentasDet_SociosComerciales P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n" +
                "Order By Renglon",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SalidasVentasSociosComercialesDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" + 
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, datediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, Existencia, CantidadVendida as Cantidad \n" +
                "From vw_VentasDet_SociosComerciales_Lotes P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SalidasVentasSociosComercialesDet_Lotes_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                "\tStatus, \n" +
                "\tcast(Cantidad as Int) as Existencia, \n" +
                "\tdbo.fg_ALMN_DisponibleDevolucion_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad) as ExistenciaDisponible, \n" +
                "\tcast(0 as int) as Cantidad \n" + 
                "From vw_VentasDet_SociosComerciales_Lotes_Ubicaciones P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet SalidasVentasSociosComercialesDet_Dev(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tCodigoEAN, IdProducto, Descripcion, TasaIva, CantidadVendida, 0 as CantidadDevuelta, Precio, 0 as Importe \n" +
                "From vw_VentasDet_SociosComerciales P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n" +
                "Order By Renglon \n", 
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SalidasVentasSociosComercialesDet_Lotes_Dev(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote,  datediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, \n" +
                "\tCantidadVendida as Existencia, \n" +
                "\tcast(Cantidad as Int) as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_VentasDet_SociosComerciales_Lotes P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SalidasVentasSociosComercialesDet_Lotes_Ubicaciones_Dev(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del detalle de Venta";
            string sMsjNoEncontrado = "Clave de venta no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                "\tStatus, \n" +
                "\tcast(Cantidad as Int) as Existencia, \n" +
                "\tcast(Cantidad as Int) as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_VentasDet_SociosComerciales_Lotes_Ubicaciones P (Nolock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioVenta = '{3}' \n", 
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Socios Comerciales

        #endregion Modulos SII

        #region SII-Recetario
        public DataSet Recetario_Beneficiarios(string IdEstado, string IdFarmacia, string IdBeneficiario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Beneficiario";
            string sMsjNoEncontrado = "Clave de Beneficiario no encontrada, verifique.";


            sQuery = sInicio + string.Format(" Select *,  " +
                " (case when datediff(dd, getdate(), FechaFinVigencia) < 0 then 0 else 1 end) as EsVigente " +
                " From Rec_CatBeneficiarios (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' " +
                " And ( IdBeneficiario = '{2}' or FolioReferencia = '{3}' ) ",
                IdEstado, IdFarmacia, Fg.PonCeros(IdBeneficiario, 8), IdBeneficiario);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet REC_Medicos(string IdEstado, string IdFarmacia, string IdMedico, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Médico";
            string sMsjNoEncontrado = "Clave de Médico no encontrada, verifique.";

            sQuery = sInicio + " Select *, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as NombreCompleto From REC_CatMedicos (NoLock) " +
                " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                " And IdMedico = '" + Fg.PonCeros(IdMedico, 6) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion SII-Recetario

        #region Modulo de Compras 
        #region Ordenes Compras Unidades
        public DataSet ProductosOrdenCompra(string IdEmpresa, string IdEstado, string FolioOrdenCompra, string IdCodigo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada ó No Pertenece a las claves de la Orden, verifique.";


            ////sQuery = sInicio + string.Format(" Select P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.IdProducto, P.CodigoEAN, P.Descripcion, " +
            ////                                " P.TasaIva, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{5}', P.IdProducto) as CostoPromedio " +
            ////                                " From vw_Productos_CodigoEAN P (Nolock) " +
            ////                                " Inner Join ( Select ClaveSSA From vw_Productos_CodigoEAN X(NoLock) " +
            ////                                             " Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det Y(NoLock)  " +
            ////                                             " On ( X.IdProducto = Y.IdProducto And X.CodigoEAN = Y.CodigoEAN ) " +
            ////                                             " Where Y.IdEmpresa = '{0}' And Y.IdEstado = '{1}' And Y.FolioOrden = '{2}' ) A  " +
            ////                                             " On ( P.ClaveSSA = A.ClaveSSA)  " +
            ////                                " Where ( P.CodigoEAN = '{3}' OR P.IdProducto = '{4}') ",
            ////                                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(FolioOrdenCompra, 8),
            ////                                IdCodigo.Trim(), Fg.PonCeros(IdCodigo, 8), DtGeneral.FarmaciaConectada);

            sQuery = sInicio + string.Format("Exec spp_ProductoOrdenesCompras  " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrden = '{3}', @IdCodigo = '{4}', @CodigoEAN = '{5}' ", 
                IdEmpresa, IdEstado, DtGeneral.FarmaciaConectada, FolioOrdenCompra, IdCodigo, IdCodigo); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Ordenes Compras Unidades
        
        #region Pedidos
        public DataSet AlmacenesCompras(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Almacén";
            string sMsjNoEncontrado = "Clave de Almacén no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select F.*, U.UrlFarmacia as UrlAlmacen, A.Status as Status_OrdenDeCompra " + 
                " From vw_Farmacias F (NoLock) " +
                " Inner Join COM_OCEN_Almacenes_Regionales A (NoLock) On ( F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia )  " + 
                " Left Join vw_Farmacias_Urls U (NoLock) On ( A.IdEstado = U.IdEstado and A.IdFarmacia = U.IdFarmacia ) " + 
                " Where F.IdEstado = '{0}' and F.IdFarmacia = '{1}' ", 
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnc_Compradores(string sIdEmpresa, string sIdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Pedidos";
            string sMsjNoEncontrado = "Pedidos no encontrados, verifique.";

            sQuery = sInicio + string.Format(" SELECT IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, " + 
                " Folio, convert (varchar(10),FechaRegistro, 120 ) " + 
                    " FROM dbo.vw_COM_REG_PedidosEnc E ( nolock ) " + 
                    " WHERE NOT EXISTS ( SELECT FolioPedido FROM COM_OCEN_AsignacionDePedidosCompradores P ( nolock ) " +
                    " WHERE P.IdEmpresa = E.IdEmpresa and P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia " +
                    " and  P.FolioPedido = E.Folio and P.IdTipoPedido = E.IdTipoPedido And P.EsCentral = 1  ) " +
                    " AND IdEstado = '{0}' AND IdEmpresa = '{1}' AND StatusPedido = 0 ", sIdEstado, sIdEmpresa);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidosEnc_Compradores_Regional(string sIdEmpresa, string sIdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Pedidos";
            string sMsjNoEncontrado = "Pedidos no encontrados, verifique.";

            sQuery = sInicio + string.Format(" SELECT IdFarmacia, Farmacia, IdTipoPedido, TipoDeClavesDePedido, TipoDeClavesDePedidoDescripcion, " + 
                " Folio, convert (varchar(10),FechaRegistro, 120 ) " +
                    " FROM dbo.vw_COM_FAR_PedidosEnc E ( nolock ) " +
                    " WHERE NOT EXISTS ( SELECT FolioPedido FROM dbo.COM_OCEN_AsignacionDePedidosCompradores P ( nolock ) " +
                    " WHERE P.IdEmpresa = E.IdEmpresa and P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia and " +
                    " P.FolioPedido = E.Folio and P.IdTipoPedido = E.IdTipoPedido And P.EsCentral = 0  ) " +
                    " AND IdEstado = '{0}' AND IdEmpresa = '{1}' AND StatusPedido = 0 ", sIdEstado, sIdEmpresa);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Pedidos_Comprador(string sIdEmpresa, string sIdEstado, string sIdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Pedidos";
            string sMsjNoEncontrado = "Pedidos no encontrados, verifique.";

            sQuery = sInicio + string.Format(" SELECT A.IdFarmacia, E.Farmacia, A.IdTipoPedido, E.TipoDeClavesDePedido, E.TipoDeClavesDePedidoDescripcion, " + 
                " A.FolioPedido, E.FolioPedidoUnidad, convert (varchar(10),A.FechaRegistro, 120 ) AS FechaRegistro " +
                    " FROM dbo.COM_OCEN_AsignacionDePedidosCompradores A ( nolock ) " +
                    " INNER JOIN dbo.vw_COM_REG_PedidosEnc E ( nolock ) " +
                    "       ON ( A.IdEmpresa = E.IdEmpresa and A.IdEstado = E.IdEstado And A.IdFarmacia = E.IdFarmacia and " +
                    "       A.FolioPedido = E.Folio and A.IdTipoPedido = E.IdTipoPedido ) " +
                    " WHERE NOT EXISTS ( SELECT FolioPedido FROM dbo.COM_OCEN_REG_Pedidos P ( nolock ) " +
                    "                   WHERE E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia and " +
                    "                   E.Folio = P.FolioPedido and E.IdTipoPedido = P.IdTipoPedido ) " +
                    " AND A.IdEstado = '{0}' AND A.IdEmpresa = '{1}' AND A.IdPersonal = '{2}' AND A.StatusPedido = 1 And A.EsCentral = 1 ",
                    sIdEstado, sIdEmpresa, sIdPersonal);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Pedidos_Comprador_Regional(string sIdEmpresa, string sIdEstado, string sIdPersonal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Pedidos";
            string sMsjNoEncontrado = "Pedidos no encontrados, verifique.";

            sQuery = sInicio + string.Format(" SELECT A.IdFarmacia, E.Farmacia, A.IdTipoPedido, E.TipoDeClavesDePedido, E.TipoDeClavesDePedidoDescripcion, " + 
                " A.FolioPedido, '0' As FolioUnidad, convert (varchar(10),A.FechaRegistro, 120 ) AS FechaRegistro " +
                    " FROM dbo.COM_OCEN_AsignacionDePedidosCompradores A ( nolock ) " +
                    " INNER JOIN dbo.vw_COM_FAR_PedidosEnc E ( nolock ) " +
                    "       ON ( A.IdEmpresa = E.IdEmpresa and A.IdEstado = E.IdEstado And A.IdFarmacia = E.IdFarmacia and " +
                    "       A.FolioPedido = E.Folio and A.IdTipoPedido = E.IdTipoPedido ) " +
                    " WHERE NOT EXISTS ( SELECT FolioPedido FROM dbo.COM_OCEN_Pedidos P ( nolock ) " +
                    "                   WHERE E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia and " +
                    "                   E.Folio = P.FolioPedido and E.IdTipoPedido = P.IdTipoPedido ) " +
                    " AND A.IdEstado = '{0}' AND A.IdEmpresa = '{1}' AND A.IdPersonal = '{2}' AND A.StatusPedido = 1 And A.EsCentral = 0 ",
                    sIdEstado, sIdEmpresa, sIdPersonal);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet AlmacenesRegionales(string Funcion)
        {
            return AlmacenesRegionales("", Funcion); 
        }

        public DataSet AlmacenesRegionales(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Almacén";
            string sMsjNoEncontrado = "Clave de Almacén no encontrada, verifique.";
            string sFiltro = "";

            if (IdEstado != "")
            {
                sFiltro = string.Format("Where F.IdEstado = '{0}' ", Fg.PonCeros(IdEstado, 2)); 
            }


            sQuery = sInicio + string.Format(" Select F.*, (F.IdFarmacia + ' - ' + F.Farmacia) as NombreFarmacia " +
                " From vw_Farmacias_urls F (NoLock) " +
                " Inner Join COM_OCEN_Almacenes_Regionales A (NoLock) On ( F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia )  " +
                " {0} ", sFiltro); 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Pedidos 

        #region Ordenes de Compras
        public DataSet OrdenesCompras_PreRecepcion_Enc(string IdEmpresa, string IdEstado, string Origen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pre-Recepción de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_PreRecepcion_OrdenesDeComprasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompras_PreRecepcion_Det(string IdEmpresa, string IdEstado, string Origen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pre-Recepción de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select CodigoEAN, IdProducto, DescProducto, ClaveLote, Caducidad, CantidadRecibida \n" +
                "From vw_PreRecepcion_OrdenesDeComprasDet (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompras_Enc(string IdEmpresa, string IdEstado, string Origen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_OrdenesDeComprasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and FarmaciaGenera = '{2}' and IdFarmacia = '{3}' and Folio = '{4}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(Origen, 4), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompras_Documentacion(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select IdDocumento, NombreDocumento as NombreDocto, Contenido as ContenidoDocto \n" + 
                "From OrdenesDeCompras_Documentacion (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioOrdenCompra = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet OrdenesCompras_Det(string IdEmpresa, string IdEstado, string Origen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select CodigoEAN, IdProducto, DescProducto, TasaIva, CantidadPrometida, CantidadPrometidaPiezas, CantidadRecibida, Costo, SubTotal, ImpteIva, Importe \n" +
                "From vw_OrdenesDeComprasDet (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And FarmaciaGenera = '{2}' And IdFarmacia = '{3}' And Folio = '{4}' \n",
                IdEmpresa, IdEstado, Origen, IdFarmacia, Folio);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompras_Det_Lotes(string IdEmpresa, string IdEstado, string Origen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de Orden de Compras no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad \n" +
                "From vw_OrdenesDeComprasDet_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet OrdenesCompras_Ingresadas(string IdEmpresa, string IdEstado, string IdFarmaciaOrigen, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Orden de Compra no encontrada, verifique.";

            sQuery = sInicio + string.Format("Select * \nFrom vw_OrdenesDeComprasEnc (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and FarmaciaGenera = '{2}' and IdFarmacia = '{3}' and FolioOrdenCompraReferencia = '{4}' \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmaciaOrigen, 4), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompra_LotesCodigoEAN(string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";

            sQuery = sInicio + string.Format("	Select L.IdProducto, L.CodigoEAN, L.ClaveLote, " +
                   " datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, " +
                   " L.FechaRegistro as FechaReg, L.FechaCaducidad, " +
                   " (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, cast(0 as Int) as CantidadPrometida, 0 as Cantidad " +
                   " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                   " Where IdEstado =  '{0}' and IdFarmacia = '{1}' and IdProducto = '{2}' and CodigoEAN = '{3}' ", IdEstado, IdFarmacia, IdCodigo, IdCodEAN);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }

        public DataSet FolioDetLotes_OrdenCompra_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad \n" +
                   "From vw_OrdenesDeComprasDet_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet OrdenesCompras_Det_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tCodigoEAN, IdProducto, DescProducto, TasaIva, CantidadPrometida, CantidadPrometidaPiezas, CantidadRecibida, \n" +
                "\t0 As Cant_Devuelta, Costo, SubTotal, ImpteIva, Importe \n" +
                "From vw_OrdenesDeComprasDet(NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet OrdenesCompras_Det_Lotes_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de Orden de Compras no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, \n" +
                "\tCantidadRecibida as Existencia, \n" +
                "\tdbo.fg_ALMN_DisponibleDevolucion_Lotes(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, CantidadRecibida) as ExistenciaDisponible, \n" +
                "\t0 as Cantidad \n" +
                "From vw_OrdenesDeComprasDet_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet OrdenesCompras_Det_Lotes_Ubicaciones_Devolucion(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" +
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, \n" +
                   "\tcast(Cantidad as int) as Existencia, \n" +
                   "\tdbo.fg_ALMN_DisponibleDevolucion_Ubicaciones(IdEmpresa, IdEstado, IdFarmacia, SKU, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, CantidadRecibida) as ExistenciaDisponible, \n" +
                   "\t0 as Cantidad \n" +
                   "From vw_OrdenesDeComprasDet_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Ordenes de Compra

        #region Ordenes de Compra Consulta 
        public string OrdenesCompras_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {
            sQuery = sInicio + string.Format(" Select * From vw_OrdenesDeComprasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));
            return sQuery;
        }

        public string OrdenesCompras_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {
            sQuery = sInicio + string.Format(
                " Select CodigoEAN, IdProducto, DescProducto, TasaIva, CantidadPrometida, CantidadPrometidaPiezas, CantidadRecibida, Costo, SubTotal, ImpteIva, Importe " +
                " From vw_OrdenesDeComprasDet(NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Folio);
            return sQuery;
        }

        public string OrdenesCompras_Det_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio)
        {           
            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, " +
                " FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad " +
                " From vw_OrdenesDeComprasDet_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ", 
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            return sQuery;
        }

        public string FolioDetLotes_OrdenCompra_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido)
        {            
            sQuery = sInicio + string.Format("	" +
                   " Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                   "    IdPasillo, IdEstante, IdEntrepaño as IdEntrepano, " +
                   "    Status, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad " +
                   " From vw_OrdenesDeComprasDet_Lotes_Ubicaciones  (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " +
                   " Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            return sQuery;
        }
        #endregion Ordenes de Compra Consulta 

        #region Ordenes_Compra_Manual
        public DataSet Folio_OrdenesCompra_Manual_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioOrden, int iTipoOrden, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Orden no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_OrdenesCompras_Claves_Enc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' And TipoOrden = {4} ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioOrden, 8), iTipoOrden);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_OrdenCompra_Manual_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioOrden, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdClaveSSA, ClaveSSA, Descripcion, Precio, Descuento, TasaIva, Iva, PrecioUnitario, Cantidad, Importe " +
                " From vw_OrdenesCompras_Claves_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioOrden, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Productos_Proveedor(string IdProveedor, string Codigo, string Funcion)
        {
            return Productos_Proveedor(IdProveedor, Codigo, 0, 0, Funcion); 
        }

        public DataSet Productos_Proveedor(string IdProveedor, string Codigo, int ValidarProductoEnPerfilOPeracion, int ValidarProductoEnPerfilComprador, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Producto no encontrado ó no esta Asignado al Proveedor, Verifique.";

            sQuery = sInicio + 
            string.Format(
                "Select \n" +
                "\tP.ClaveSSA, P.IdProducto, P.CodigoEAN, P.Descripcion, P.ContenidoPaquete, L.Precio, \n" +
                "\tL.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, \n" +
                "\tdbo.fg_PrecioComisionNegociadora(P.IdClaveSSA_Sal, P.IdLaboratorio) As ComisionNegociadora, \n" +
                "\t0 As Cantidad, 0.0 As Importe, \n" +
                "\tdbo.fg_ValidarClaveEnPerfil_Operacion('{2}', '{3}', '{4}', P.IdProducto, P.CodigoEAN ) as ProductoParaCompra, \n" +
                "\tdbo.fg_ValidarClaveEnPerfil_Comprador('{2}', '{3}', '{5}', '{6}', P.IdProducto, P.CodigoEAN ) As ProductoParaComprador \n" +
                "From vw_Productos_CodigoEAN P (NoLock) \n" +
                "Inner Join COM_OCEN_ListaDePrecios L (NoLock) On( P.CodigoEAN = L.CodigoEAN and L.Status = 'A' ) \n" +
                "Where L.IdProveedor = '{0}' And L.CodigoEAN = '{1}' and P.StatusCodigoRel = 'A' \n",
                IdProveedor, Codigo,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ValidarProductoEnPerfilOPeracion,
                DtGeneral.IdPersonal, ValidarProductoEnPerfilComprador);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_OrdenCompra_CodigosEAN_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioOrden, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el Detalle del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select CodigoEAN, IdProducto, Descripcion, ContenidoPaquete, Precio, Descuento, TasaIva, " +
                " PrecioCaja, CantidadCajas, Importe, Iva, ImporteTotal, CantidadCajas As Cant_Actual, CantidadSobreCompra, PrecioCaja , ObservacionesSobreCompra, " +
                "'' As Observaciones, ClaveSSA, TienePrecioNegociado, " +
                " CodigoEAN_Bloqueado, IdPersonal_Bloquea, Personal_Bloquea, Fecha_Bloqueado, Partida, AplicaCosto  " +
                " From vw_OrdenesCompras_CodigosEAN_Det (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " + 
                " Order by Partida ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioOrden, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Productos_Proveedor(string IdProveedor, string sIdClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave SSA";
            string sMsjNoEncontrado = "Clave SSA no encontrada ó no esta Asignada al Proveedor, Verifique.";

            sQuery = sInicio + string.Format(" Select S.ClaveSSA_Base As ClaveSSA, S.IdClaveSSA_Sal, S.Descripcion, L.Precio, " +
                                     " L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, 0 As Cantidad, 0.0 As Importe " +
                                     " From CatClavesSSA_Sales S (NoLock) " +
                                     " Inner Join COM_OCEN_ListaDePrecios_Claves L (NoLock) " +
                                        " On( S.IdClaveSSA_Sal = L.IdClaveSSA ) " +
                                     " Where L.IdProveedor = '{0}' And S.ClaveSSA_Base = '{1}' ",
                                     IdProveedor, sIdClaveSSA);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Ordenes_Compra_Manual 
        
        #region Proveedores Sancionados 
        public DataSet Proveedores_Sancionados(string Funcion)
        {
            return Proveedores_Sancionados("", Funcion);
        }

        public DataSet Proveedores_Sancionados(string IdProveedor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al revisar si el Proveedor ya se encuentra sancionado";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrada, verifique.";
            string sWhere = "";

            if (IdProveedor != "")
            {
                sWhere = string.Format(" And IdProveedor = '{0}' ", Fg.PonCeros(IdProveedor, 4));
            }

            sQuery = sInicio + string.Format(" Select * From COM_OCEN_Proveedores_Sancionados(NoLock) Where 1 = 1 {0} ", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Proveedores Sancionados

        #region Motivos
        public DataSet CatMotivos(string Funcion)
        {
            return CatMotivos("", Funcion);
        }

        public DataSet CatMotivos(string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de motivos.";
            string sMsjNoEncontrado = "Folio de motivo no encontrado, verifique.";
            string sFiltro = "";

            if (Folio != "")
            {
                sFiltro = string.Format(" Where Folio = '{0}' ", Fg.PonCeros(Folio, 4));
            }

            sQuery = sInicio + string.Format("Select * From CatMotivosSobrePrecio (NoLock){0}", sFiltro);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ObservacionMotivos(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string IdProducto, string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de motivos.";
            string sMsjNoEncontrado = "Folio de motivo no encontrado, verifique.";

            sQuery = sInicio + string.Format("Exec spp_Rpt_COM_OCEN_SobrePrecioDet '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'" +
                                             "Exec spp_Rpt_COM_OCEN_SobrePrecioEnc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ObservacionMotivosEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string IdProducto, string CodigoEAN, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de motivos.";
            string sMsjNoEncontrado = "Folio de motivo no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select * From COM_OCEN_SobrePrecioEnc (NoLock) " +
                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And FolioOrden = '{3}' And Idproducto = '{4}' And CodigoEAN = '{5}'",
                    IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Motivos
        #endregion Modulo de Compras

        #region Auditoria Farmacias
        public DataSet Adt_ComprasEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCompra, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From Adt_ComprasEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioCompra = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioCompra, 8));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Adt_FarmaciaProductos_CodigoEAN_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdProducto, string CodigoEAN, string ClaveLote, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Lote del Producto";
            string sMsjNoEncontrado = "Lote de Producto no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From Adt_FarmaciaProductos_CodigoEAN_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdSubFarmacia = '{3}' and IdProducto = '{4}' " +
                "And CodigoEAN = '{5}' And ClaveLote = '{6}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), IdSubFarmacia, Fg.PonCeros(IdProducto, 8), CodigoEAN, ClaveLote);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Adt_Folio_VentasEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioVenta, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "Folio de Venta no encontrado, verifique.";

            IdEmpresa = Fg.PonCeros(IdEmpresa, 3);
            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdFarmacia = Fg.PonCeros(IdFarmacia, 4);
            FolioVenta = Fg.PonCeros(FolioVenta, 8);

            sQuery = sInicio + string.Format(" SELECT *  FROM Adt_VentasEnc (NoLock) " +
                " WHERE IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And FolioVenta = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Auditoria Farmacias

        #region Remisiones Distribuidor 
        public DataSet RemisionesDistEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_RemisionesDistEnc (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 

        public DataSet RemisionesDistDet(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Pedido";
            string sMsjNoEncontrado = "Folio de Pedido no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select ClaveSSA, IdClaveSSA, DescripcionSal, Presentacion, Precio, Cant_Recibida, CantidadRecibida " +
                " From vw_RemisionesDistDet(NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Distribuidor_Clientes(string IdEstado, string IdDistribuidor, string Funcion)
        {
            return Distribuidor_Clientes(IdEstado, IdDistribuidor, "", Funcion);
        }

        public DataSet Distribuidor_Clientes(string IdEstado, string IdDistribuidor, string CodigoCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Código de Cliente no encontrado, verifique.";
            string sWhere = "";

            if (CodigoCliente != "")
            {
                sWhere = string.Format("And CodigoCliente = {0} ", CodigoCliente);
            }

            sQuery = sInicio + string.Format(" Select * " +
                       " From vw_Distribuidores_Clientes (NoLock) " +
                       " Where IdEstado = '{0}' and IdDistribuidor = '{1}' {2} ",
                       IdEstado, Fg.PonCeros(IdDistribuidor, 4), sWhere);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Remisiones Distribuidor 

        #region Facturacion  
        #region Comprobacion de Documentos y Facturas 
        public DataSet Comprobacion_Facturas(string FolioRelacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tL.FolioRelacion, L.FechaRegistro, L.EsFacturaEnCajas, L.EsComprobada, L.TipoDeUnidades, \n" +
                "\tL.Serie, L.Folio, L.Serie_Relacionada, L.Folio_Relacionado, F.SubTotal, F.Iva, F.Importe, F.Total, \n" + 
                "\tFD.IdCliente, FD.Cliente, FD.IdSubCliente, FD.SubCliente, F.TipoInsumoDescripcion, F.TipoDocumentoDescripcion, \n" +
                "\t(FD.Financiamiento + ' ' + FD.NumeroDeContrato)as FuenteFinanciamiento, FD.Financiamiento \n" + 
                "From FACT_Remisiones__RelacionFacturas_Enc L (NoLock) \n" +
                "Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) \n"+
                "\t On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.Serie_Relacionada = F.Serie and L.Folio_Relacionado = F.Folio ) \n" +
                "Inner Join vw_FACT_FuentesDeFinanciamiento_Detalle FD (NoLock) \n" +
                "\t On ( F.IdFuenteFinanciamiento = FD.IdFuenteFinanciamiento and F.IdFuenteFinanciamiento_Segmento = FD.IdFinanciamiento ) \n" +
                "Where L.FolioRelacion = '{0}' ", Fg.PonCeros(FolioRelacion, 6));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Comprobacion_Documentos(string FolioRelacion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tL.FolioRelacion, L.FechaRegistro, L.EsDocumentoEnCajas, L.EsComprobada, \n" + 
                "\tL.ReferenciaDocumento, L.NombreDocumento, L.MD5_Documento, L.Documento, \n" +
                "\tL.TipoDeUnidades, L.Procesa_Venta, L.Procesa_Consigna, \n" +
                "\tL.Procesa_Producto, L.Procesa_Servicio, L.FechaRegistro, \n" + 
                "\tFD.IdCliente, FD.Cliente, FD.IdSubCliente, FD.SubCliente, \n" + 
                "\tL.IdFuenteFinanciamiento,(FD.Cliente + ' - ' + FD.NumeroDeContrato) as FuenteFinanciamiento \n" + 
                "From FACT_Remisiones__RelacionDocumentos_Enc L (NoLock) \n" + 
                "Inner Join vw_FACT_FuentesDeFinanciamiento FD (NoLock) \n" +
                "\t On ( L.IdFuenteFinanciamiento = FD.IdFuenteFinanciamiento ) \n" +
                "Where L.FolioRelacion = '{0}' ", Fg.PonCeros(FolioRelacion, 6));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Comprobacion de Documentos y Facturas 

        #region Fuentes de Financiamiento
        public DataSet FuentesDeFinanciamiento_Encabezado(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Detalle(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Detalles del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdFinanciamiento, Financiamiento, Alias, Prioridad, MontoDetalle, PiezasDetalle, " + 
                " (Case When StatusDetalle = 'A' Then 1 Else 0 End) as Status, 1 as Guardado, " +
                " ValidarPolizaBeneficiario, LongitudMinimaPoliza, LongitudMaximaPoliza " + 
                " From vw_FACT_FuentesDeFinanciamiento_Detalle (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Detalle(string IdFuenteFinanciamiento, string IdFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el Financiamiento";
            string sMsjNoEncontrado = "Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Detalle (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' And IdFinanciamiento = '{1}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4), Fg.PonCeros(IdFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet FuentesDeFinanciamiento_Claves(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Claves del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Claves (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Claves(string IdFuenteFinanciamiento, string IdFinanciamiento, string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Claves del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Claves (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }  
        #endregion Fuentes de Financiamiento 

        #region Fuentes de Financiamiento_Admon
        public DataSet FuentesDeFinanciamiento_Admon_Encabezado(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Admon (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Admon_Detalle(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Detalles del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdFinanciamiento, Financiamiento, MontoDetalle, PiezasDetalle, " +
                " Case When StatusDetalle = 'A' Then 1 Else 0 End as Status, 1 as Guardado, " +
                " ValidarPolizaBeneficiario, LongitudMinimaPoliza, LongitudMaximaPoliza " +
                " From vw_FACT_FuentesDeFinanciamiento_Admon_Detalle (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Admon_Detalle(string IdFuenteFinanciamiento, string IdFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener el Financiamiento";
            string sMsjNoEncontrado = "Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Admon_Detalle (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' And IdFinanciamiento = '{1}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4), Fg.PonCeros(IdFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Admon_Claves(string IdFuenteFinanciamiento, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Claves del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Admon_Claves (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FuentesDeFinanciamiento_Admon_Claves(string IdFuenteFinanciamiento, string IdFinanciamiento, string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la Fuente de Financiamiento";
            string sMsjNoEncontrado = "Claves del Folio de Financiamento no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_FACT_FuentesDeFinanciamiento_Admon_Claves (NoLock) " +
                " Where IdFuenteFinanciamiento = '{0}' " +
                " Order By IdFinanciamiento ", Fg.PonCeros(IdFuenteFinanciamiento, 4));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Fuentes de Financiamiento_Admon

        #region Facturas
        public DataSet FacturasRemisiones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioFactura, string Funcion)
        { 
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de facturas";
            string sMsjNoEncontrado = "Folio de factura no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_FACT_Facturas_Remisiones (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioFactura = '{3}' ", 
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioFactura, 10)); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset; 
        }

        public DataSet RemisionFacturada(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioRemision, string Status, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de facturas";
            string sMsjNoEncontrado = "Folio de factura no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_FACT_Facturas_Remisiones (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioRemision = '{3}' and Status = '{4}' ",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioRemision, 10), Status);

            bMostrarMsjLeerVacio = !bMostrarMsjLeerVacio;   // Apagar el msj 
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = !bMostrarMsjLeerVacio;   // Encender el msj  

            return myDataset;
        }

        public DataSet Facturacion_Remisiones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioRemision, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de remisiones";
            string sMsjNoEncontrado = "Folio de remisión no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_FACT_Remisiones (NoLock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioRemision = '{3}' ",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioRemision, 10));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        } 
        #endregion Facturas

        #region ContraRecibos 
        public DataSet FolioFacturas(string Empresa, string Estado, string Farmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos del Folio Factura";
            string sMsjNoEncontrado = "Folio no encontrado, verifique.";


            sQuery = sInicio + string.Format(" Select * " +
                " From vw_FACT_Facturas_Remisiones (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and FolioFactura = '{3}' ",
                Empresa, Estado, Farmacia, Fg.PonCeros(Folio, 10));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion ContraRecibos

        #region Cotizaciones licitaciones
        public DataSet ClavesSSA_Sales_TipoClaves(string IdClaveSSA_Sal, bool BuscarPorClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";

            sFiltro = string.Format(" Where S.IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA_Sal, 6));
            if (BuscarPorClaveSSA)
            {
                sFiltro = string.Format(" Where  S.ClaveSSA = '{0}'  ", IdClaveSSA_Sal);
            }

            sQuery = sInicio + string.Format(
                "Select \n" +
                "\tS.*, S.DescripcionClave as Descripcion, T.PorcIva As TasaIva, \n" +
                "\tIsNull(C.PrecioNeto, 0) As PrecioNeto, IsNull(C.Porcentaje, 0) As Porcentaje \n" +
                "From vw_ClavesSSA_Sales S (NoLock) \n" +
                "Inner Join CatTiposDeProducto T (Nolock) On ( S.TipoDeClave = T.IdTipoProducto ) \n" +
                "Left Join CatClavesSSA_Causes C (Nolock) \n" +
                "\tOn ( S.ClaveSSA = C.ClaveSSA And C.Año = (Select Max(Año) From CatClavesSSA_Causes (Nolock)) ) \n" +
                "{0}  \n", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Costo_Compra_Licitaciones(string IdClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos del Costo de Compras";
            string sMsjNoEncontrado = "Costos no encontrados, verifique.";


            sQuery = sInicio + string.Format(" Select L.ClaveSSA, L.Importe, P.IdProducto, " +
                    " L.CodigoEAN, P.Descripcion, L.IdProveedor, " +
                    " L.Nombre, P.IdLaboratorio, P.Laboratorio " +
                    " From vw_COM_OCEN_ListaDePreciosClaves L (Nolock) " +
                    " Inner Join vw_Productos_CodigoEAN P (Nolock) " +
                        " On ( L.ClaveSSA = P.ClaveSSA And L.CodigoEAN = P.CodigoEAN )  " +
                    " Where L.IdClaveSSA = '{0}'  " +
                    " Order By L.Importe ", IdClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Sales_PreciosCauses(string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Clave SSA";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            sQuery = sInicio + string.Format(" Select *, Descripcion As DescripcionSal " +
                " From vw_CatClavesSSA_Causes (NoLock) Where ClaveSSA = '{0}'   ", ClaveSSA);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Cotizaciones licitaciones 

        #region Facturacion Electronica 
        public DataSet CFDI_Establecimientos_Emisor( string IdEmpresa, string IdEstado, string IdFarmacia,
            string IdEstablecimiento, string Funcion )
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select * \n" +
                "From FACT_CFD_Establecimientos (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdEstablecimiento = '{3}' \n",
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(IdEstablecimiento, 6)
                );

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        public DataSet CFDI_Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrada, verifique.";


            sQuery = sInicio + string.Format(" Select IdCliente, Nombre, RFC, " + 
                " Pais, Estado, Municipio, Localidad, Colonia, Calle, NoExterior, NoInterior, CodigoPostal, Referencia, Status " +
                " From FACT_CFD_Clientes (NoLock) " + 
                " Where IdCliente = '{0}'   ", Fg.PonCeros(IdCliente, 6));

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Clientes_Establecimientos( string IdEmpresa, string IdEstado, string IdFarmacia, 
            string IdCliente, string IdEstablecimiento, string Funcion )
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrada, verifique.";


            sQuery = sInicio + string.Format(
                "Select * \n" +
                "From FACT_CFD_Establecimientos_Receptor (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdCliente = '{3}' and IdEstablecimiento = '{4}' \n", 
                IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(IdCliente, 6), Fg.PonCeros(IdEstablecimiento, 6)
                );

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_FormasDePago(string VersionCFDI, string Funcion) 
        {
            return CFDI_FormasDePago("", VersionCFDI, Funcion);  
        }

        public DataSet CFDI_FormasDePago(string IdFormaDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Forma de pago";
            string sMsjNoEncontrado = "Forma de pago no encontrada, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (IdFormaDePago != "")
            {
                sFiltro = string.Format(" And IdFormaDePago = '{0}' ", Fg.PonCeros(IdFormaDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdFormaDePago, Descripcion, Status, " +
            //        " (IdFormaDePago + ' --' + Descripcion) as NombreFormaDePago " +
            //        " From FACT_CFD_FormasDePago (NoLock) {0}  ", sFiltro);
            //}


            //if (VersionCFDI == "3.3")
            {
                sQuery = sInicio + string.Format(" Select IdFormaDePago as IdFormaDePago, Descripcion, " +
                    " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
                    " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
                    " From FACT_CFDI_FormasDePago (NoLock) {0}  " +
                    " Order by IdFormaDePago ", sFiltro);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_ComplementoPagos__FormasDePago(string IdFormaDePago_Excluida, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Forma de pago";
            string sMsjNoEncontrado = "Formas de pago no encontradas, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (IdFormaDePago_Excluida != "")
            {
                sFiltro += string.Format(" and IdFormaDePago <> '{0}' ", Fg.PonCeros(IdFormaDePago_Excluida, 2));
            }

            sQuery = sInicio + string.Format(" Select IdFormaDePago as IdFormaDePago, Descripcion, (IdFormaDePago + ' -- ' + Descripcion) as Descripcion_FormaDePago, " +
                " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " + 
                " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " + 
                " From FACT_CFDI_FormasDePago (NoLock) {0}  " + 
                " Order by IdFormaDePago ", sFiltro); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_MetodosDePago(string VersionCFDI, string Funcion)
        {
            return CFDI_MetodosDePago("", VersionCFDI, Funcion);
        }

        public DataSet CFDI_MetodosDePago(string IdMetodoDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Método de pago";
            string sMsjNoEncontrado = "Método de pago no encontrado, verifique.";
            string sFiltro = "";

            if (IdMetodoDePago != "")
            {
                sFiltro = string.Format(" Where IdMetodoDePago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdMetodoPago, Descripcion, Status, " +
            //        " (IdMetodoPago + ' --' + Descripcion) as NombreMetodoDePago " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  ", sFiltro);
            //}

            //if (VersionCFDI == "3.3")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdMetodoPago, Descripcion, Status, " +
            //        " (IdMetodoPago + ' --' + Descripcion) as NombreMetodoDePago " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  ", sFiltro);
            //}


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_MetodosDePago_Listado(string IdMetodoDePago, string VersionCFDI, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error la información de Método de pago";
            string sMsjNoEncontrado = "Método de pago no encontrado, verifique.";
            string sFiltro = "";
            string sNombreCampo = " IdMetodoDePago ";

            if (IdMetodoDePago != "")
            {
                sFiltro = string.Format(" Where IdMetodoPago = '{0}' ", Fg.PonCeros(IdMetodoDePago, 2));
            }

            //if (VersionCFDI == "3.2")
            //{
            //    sQuery = sInicio + string.Format(" Select IdMetodoPago as IdFormaDePago, Descripcion, " +
            //        " (case when RequiereReferencia = 1 then 'SI' else 'NO' end) as RequiereReferencia, " +
            //        " cast(0 as numeric(14,2)) as Importe, cast('' as varchar(10)) as Referencia \n " +
            //        " From FACT_CFD_MetodosPago (NoLock) {0}  " +
            //        " Order by IdMetodoPago ", sFiltro);
            //}

            //if (VersionCFDI == "3.3")
            {
                //if (sFiltro == "")
                //{
                //    sFiltro += string.Format(" Where Version = '{0}' ", VersionCFDI);
                //}
                //else
                //{
                //    sFiltro += string.Format(" and Version = '{0}' ", VersionCFDI);
                //}

                sFiltro = "Where Status = 'A' "; 
                sFiltro += string.Format(" and Version = '{0}' ", VersionCFDI);


                sQuery = sInicio + string.Format(" Select IdMetodoDePago as IdFormaDePago, Descripcion, (IdMetodoDePago + ' -- ' + Descripcion) as DescripcionMetodoDePago " +
                    " From FACT_CFDI_MetodosPago (NoLock) {0}  " +
                    " Order by IdMetodoDePago ", sFiltro );
            }


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Series_y_Folios(string IdEmpresa, string IdEstado, string IdFarmacia, string IdTipoDocumento, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Serie de facturación";
            string sMsjNoEncontrado = "Serie de facturación no encontradas, verifique.";
            string sFiltro = "";

            if (IdTipoDocumento != "")
            {
                sFiltro = string.Format(" and IdTipoDocumento = '{0}' ", IdTipoDocumento); 
            }

            sQuery = sInicio + string.Format("Select IdEmpresa, IdEstado, IdFarmacia, Bloqueado, Asignado, " +
                " AñoAprobacion, NumAprobacion, IdTipoDocumento, TipoDeDocumento, Serie, NombreDocumento, FolioInicial, FolioFinal, FolioUtilizado, IdentificadorSerie, Status " +
                " From vw_FACT_CFD_Sucursales_Series (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' {3} and Asignado = 'SI' ",
                IdEmpresa, IdEstado, IdFarmacia, sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_TipoDeDocumentos(string IdTipoDocumento, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la  información de Tipos de Documento";
            string sMsjNoEncontrado = "Tipos de Documento no encontrado, verifique.";
            string sFiltro = "";

            if (IdTipoDocumento != "")
            {
                sFiltro = string.Format(" Where IdTipoDocumento = '{0}' ", IdTipoDocumento); 
            }

            sQuery = sInicio + string.Format("Select IdTipoDocumento, NombreDocumento as Documento, Alias, TipoDeComprobante, Status " +
                " From FACT_CFD_TiposDeDocumentos (NoLock) " +
                " {0} Order by IdTipoDocumento ", sFiltro); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_UnidadesDeMedida(string IdUnidad, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Unidades de Medida";
            string sMsjNoEncontrado = "Unidad de Medida no encontrada, verifique.";
            string sFiltro = "";

            if (IdUnidad != "")
            {
                sFiltro = string.Format(" Where IdUnidad = '{0}' ", IdUnidad);
            }

            sQuery = sInicio + string.Format("Select IdUnidad, Descripcion, Status " +
                " From FACT_CFD_UnidadesDeMedida (NoLock) " +
                " {0} Order by IdUnidad ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Conceptos(string IdConcepto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Concepto";
            string sMsjNoEncontrado = "Clave de Concepto no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select *  From FACT_CFD_Conceptos (NoLock)  " +
                " Where IdConcepto = '{0}' Order by IdConcepto ", IdConcepto );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Conceptos_Especiales(string IdEstado, string IdConcepto, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Concepto";
            string sMsjNoEncontrado = "Clave de Concepto no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select *  From FACT_CFD_ConceptosEspeciales (NoLock)  " +
                " Where IdEstado = '{0}' and IdConcepto = '{1}' Order by IdConcepto ", 
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdConcepto, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_UsosDeCFDI(string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Unidades de Medida";
            string sMsjNoEncontrado = "Unidad de Medida no encontrada, verifique.";
            string sFiltro = " Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select Clave, Descripcion, (Clave + ' - ' + Descripcion) as UsoDeCFDI, Status " +
                " From FACT_CFDI_UsosDeCFDI (NoLock) " + 
                " {0} " + 
                " Order by Clave, Descripcion ", sFiltro); 

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Monedas(string Funcion)
        {
            return CFDI_Monedas("", Funcion); 
        }

        public DataSet CFDI_Monedas(string Moneda, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la información de Monedas ";
            string sMsjNoEncontrado = "Monedas no encontradas, verifique.";
            string sFiltro = "Where Status = 'A' ";

            if (Moneda != "")
            {
                sFiltro += string.Format(" and Clave = '{0}' ", Moneda);
            }

            sQuery = sInicio + string.Format("Select Clave, Descripcion, (Clave + ' - ' + Descripcion) as Moneda, Status " +
                " From FACT_CFDI_TiposDeMoneda (NoLock) " + 
                " {0} " + 
                " Order by Clave, Descripcion ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_RegimenFiscal(string IdRegimen, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener la  información de Regimens fiscales";
            string sMsjNoEncontrado = "Regimens fiscales no encontrados, verifique.";
            string sFiltro = "";

            if (IdRegimen != "")
            {
                sFiltro = string.Format(" and IdRegimen = '{0}' ", IdRegimen);
            }

            sQuery = sInicio + string.Format("Select IdRegimen, Descripcion, ( IdRegimen + ' - ' + Descripcion ) as DescripcionRegimen \n" +
                "From FACT_CFDI_RegimenFiscal (NoLock) \n" +
                "Where Status = 'A'  {0}\n" + 
                "Order by IdRegimen \n", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet CFDI_Pagos__EmisorBancos(string RFC_Emisor, string RFC_Banco, string NumeroDeCuenta, bool MostrarCuentas, string Funcion)
       {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";

            sQuery = sInicio + string.Format("Select Distinct RFC_Banco, NombreCorto, NombreRazonSocial, RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}', And RFC_Banco = '{4}' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " +
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status " +
                    " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}' And RFC_Banco = '{4}' And NumeroDeCuenta = '{5}' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco, NumeroDeCuenta);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }


        public DataSet CFDI_Pagos__ReceptorBancos(string RFC_Banco, string NumeroDeCuenta, bool MostrarCuentas, string Funcion)
       {
           myDataset = new DataSet();
           string sMsjError = "Ocurrió un error al obtener la información de Bancos del Receptor ";
           string sMsjNoEncontrado = "Bancos no encontrados, verifique.";

               sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                   " RFC_Banco, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status " +
                   " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And RFC_Banco = '{3}' And NumeroDeCuenta = '{4}' " +
                   " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                   " Order by NombreRazonSocial, NumeroDeCuenta ",
                   DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Banco, NumeroDeCuenta);

           myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

           return myDataset;
       }

        public DataSet CFDI_Bancos(string RFC, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select * From FACT_CFDI__Bancos Where RFC = '{0}'", RFC);


            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Pagos__EmisorBancos_Cuentas(bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select RFC_Banco, NombreCorto, NombreRazonSocial, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " + 
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco " +
                    " From vw_FACT_BancosCuentas_Emisor (NoLock) " + 
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_Pagos__ReceptorBancos_Cuentas(bool MostrarCuentas, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Bancos del Emisor ";
            string sMsjNoEncontrado = "Bancos no encontrados, verifique.";
            string sFiltro = "Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select RFC_Banco, NombreCorto, NombreRazonSocial, (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco " +
                " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                " Group by RFC_Banco, NombreCorto, NombreRazonSocial " +
                " Order by NombreRazonSocial ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (MostrarCuentas)
            {
                sQuery = sInicio + string.Format("Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    " (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco " +
                    " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ", DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            }

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }

        public DataSet CFDI_MotivosDeCancelacion( string Funcion )
        {
            myDataset = new DataSet();
            // string sExtra = "";
            // string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los Motivos de cancelación";
            string sMsjNoEncontrado = "Unidad de Medida no encontrada, verifique.";
            string sFiltro = " Where Status = 'A' ";

            sQuery = sInicio + string.Format("Select Clave, Descripcion, (Clave + ' - ' + Descripcion) as MotivoSAT, Motivo, Status " +
                " From FACT_CFDI_MotivosDeCancelacion (NoLock) " +
                " {0} " +
                " Order by Clave, Descripcion ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }
        #endregion Facturacion Electronica

        #region CuentasxPagar
        public DataSet Servicios_Facturacion(string IdServicio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Servicio";
            string sMsjNoEncontrado = "Clave de Servicio no encontrada, verifique.";
            
            sQuery = sInicio + string.Format(" Select *  " +
                " From FACT_CatServicios (NoLock)  Where IdServicio = '{0}' Order by IdServicio ", Fg.PonCeros(IdServicio, 3));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Acreedores_Facturacion(string Estado, string Acreedor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Acreedor";
            string sMsjNoEncontrado = "Clave de Acreedor no encontrada, verifique.";  
           
            sQuery = sInicio + string.Format(" Select * " +
                " From FACT_CatAcreedores (NoLock) Where IdEstado = '{0}' and IdAcreedor = '{1}' ", 
                Estado, Fg.PonCeros(Acreedor, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Bancos(string Banco, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del banco";
            string sMsjNoEncontrado = "Clave de banco no se a encontrada, verifique.";

            sQuery = sInicio + string.Format("Select * From CNT_CatBancos Where IdBanco = '{0}'", Fg.PonCeros(Banco, 3));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Chequera(string IdEmpresa, string IdEstado, string Chequera, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la chequera";
            string sMsjNoEncontrado = "Clave de la chequera no se a encontrada, verifique.";

            sQuery = sInicio + string.Format("Select * From vw_Chequeras Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdChequera = '{2}'",
                                Fg.PonCeros(IdEmpresa,3), Fg.PonCeros(IdEstado,2), Fg.PonCeros(Chequera, 6));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet BeneficiarioChequera(string Beneficiario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del beneficiario ";
            string sMsjNoEncontrado = "Clave del beneficiario no encontrada, verifique.";

            Beneficiario = Fg.PonCeros(Beneficiario, 6);
            sQuery = sInicio + string.Format("Select *, IdBeneficiario As Clave From CNT_CatChequesBeneficiarios C (NoLock)  Where IdBeneficiario = '{0}'", Beneficiario);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Cheque(string IdEmpresa, string IdEstado, string Cheque, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del cheque";
            string sMsjNoEncontrado = "El cheque no se ha encontrado, verifique.";

            sQuery = sInicio + string.Format("Select * From vw_Cheque Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdCheque = '{2}'",
                                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(Cheque, 6));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion CuentasxPagar

        #endregion Facturacion

        #region Funciones_UrlChecador
        public DataSet Url_ChecadorUnidad(string Funcion)
        {
            myDataset = new DataSet();

            string sMsjError = "Ocurrió un error al obtener la URL del Checador de la Unidad";
            string sMsjNoEncontrado = "URL de Checador de la Unidad no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_Checador (NoLock)  ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Url_PersonalFirma(string Funcion)
        {
            myDataset = new DataSet();

            string sMsjError = "Ocurrió un error al obtener la URL del lector de la Unidad";
            string sMsjNoEncontrado = "URL del lector de la Unidad no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_FirmaHuellas (NoLock)  ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Url_AutorizacionFirma(string Funcion)
        {
            myDataset = new DataSet();

            string sMsjError = "Ocurrió un error al obtener la URL de las huellas de autorización.";
            string sMsjNoEncontrado = "URL las huellas de autorización no encontradas, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_AutorizacionHuellas (NoLock)  ");
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Funciones_UrlChecador

        #region Perfiles_De_Atencion
        public DataSet PerfilesAtencion(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPerfilAtencion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Perfiles de Atención";
            string sMsjNoEncontrado = "Perfiles de Atención no encontrados, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_ALMN_CB_NivelesAtencion (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPerfilAtencion = {3} ",
                IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PerfilesAtencion_ClavesSSA(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPerfilAtencion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las Claves";
            string sMsjNoEncontrado = "Claves de Perfiles de Atencion no encontradas, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPerfilAtencion = {3} ",
                IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PerfilesAtencionDistribuidor(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPerfilAtencion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Perfiles de Atención";
            string sMsjNoEncontrado = "Perfiles de Atención no encontrados, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPerfilAtencion = {3} ",
                IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PerfilesAtencion_ClavesSSADistribuidor(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPerfilAtencion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de las Claves";
            string sMsjNoEncontrado = "Claves de Perfiles de Atención no encontradas, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and IdPerfilAtencion = {3} ",
                IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Perfiles_De_Atencion

        #region Pedidos Almacen
        public DataSet PedidosSurtimiento_Status(string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Perfiles de Atención";
            string sMsjNoEncontrado = "Perfiles de Atención no encontrados, verifique.";

            sQuery = sInicio + string.Format(" Select IdPedidoStatus, ClaveStatus, Descripcion, Status  " + 
                " From Pedidos_Status (NoLock) " +
                " Where Status = 'A' Order by IdPedidoStatus " );
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Pedidos Almacen

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

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            return EjecutarQuery(Funcion, prtQuery, MensajeError, MensajeNoEncontrado, bMostrarMsjLeerVacio); 
        }

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado, bool MostrarMensajeLeerSinDatos )
        {
            clsLeer Leer = new clsLeer( ref ConexionSql );
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            Leer.Conexion.SetConnectionString();
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
                    if (MostrarMensajeLeerSinDatos)
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

        #region Devoluciones_Proveedor        
        public DataSet Folio_DevolucionesProveedorEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDevProv, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Devolución de proveedor no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_DevolucionesProveedorEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioDevProv, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_DevolucionesProveedorDet(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDevProv, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Devolución de proveedor no encontrado, verifique.";

            sQuery = sInicio + string.Format(
               "Select \n" +
               "\tCD.CodigoEAN, CD.IdProducto, CD.DescProducto, \n" +
               "\tCD.TasaIva, CD.CantidadRecibida, 0 as CantidadDevolucion, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{2}', CD.IdProducto) as CostoPromedio, \n" +
               "\tCD.Costo, 0 as Importe \n" +
               "From vw_DevolucionesProveedorDet CD (NoLock) \n" +
               "Where Cd.IdEmpresa =  '{0}' and CD.IdEstado = '{1}' and CD.IdFarmacia = '{2}' and CD.Folio = '{3}' \n",
               IdEmpresa, IdEstado, IdFarmacia, FolioDevProv);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Folio_DevolucionesProveedorDet_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDevProv, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de Devolución de proveedor.";
            string sMsjNoEncontrado = "Folio de Devolución de proveedor no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                "Select \n" + 
                "\tIdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                "\tdatediff(mm, getdate(), FechaCad) as MesesCad, \n" +
                "\tFechaReg, FechaCad, Status, CantidadRecibida as Existencia, 0 as Cantidad \n" +
                "From vw_DevolucionesProveedorDet_Lotes (NoLock) \n" +
                "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  \n",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioDevProv);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }

        public DataSet Folio_DevolucionesProveedorDet_Lotes_Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioDevProv, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de ventas no encontrado, verifique.";

            sQuery = sInicio + string.Format("" +
                   "Select \n" + 
                   "\tIdSubFarmacia, IdProducto, CodigoEAN, SKU, ClaveLote, \n" +
                   "\tIdPasillo, IdEstante, IdEntrepaño as IdEntrepano, \n" +
                   "\tStatus, cast(Cantidad as Int) as Existencia, cast(0 as int) as Cantidad \n" +
                   "From vw_DevolucionesProveedorDet_Lotes_Ubicaciones  (NoLock) \n" +
                   "Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' \n" +
                   "Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote \n",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioDevProv);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 
            return myDataset;
        }
        #endregion Devoluciones_Proveedor

        #region Motivos_Devolucion_Transferencia
        public DataSet Motivos_Dev_Transferencia(string IdMotivo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Motivo de Devolución.";
            string sMsjNoEncontrado = "Clave de Motivo no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From CatMotivos_Dev_Transferencia (NoLock) Where IdMotivo = '{0}' ", Fg.PonCeros(IdMotivo, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Motivos_Devolucion_Transferencia

        #region Farmacias_Proveedores_Vales
        public DataSet Proveedores_Vales(string IdEstado, string IdFarmacia, string Funcion)
        {
            return Proveedores_Vales(IdEstado, IdFarmacia, "", Funcion);
        }

        public DataSet Proveedores_Vales(string IdEstado, string IdFarmacia, string IdProveedor, string Funcion)
        {
            return Proveedores_Vales(IdEstado, IdFarmacia, IdProveedor, true, Funcion);
        }

        public DataSet Proveedores_Vales(string IdEstado, string IdFarmacia, string IdProveedor, bool HabilitarProveedorDeReembolso, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Proveedor";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrado, verifique.";
            string sFiltroProveedor = "";

            
            if (IdProveedor.Trim() != "")
            {
                sFiltroProveedor = string.Format(" and F.IdProveedor = '{0}' ", Fg.PonCeros(IdProveedor, 4));
            }

            if (!HabilitarProveedorDeReembolso)
            {
                sFiltroProveedor += string.Format(" and P.IdProveedor >= '0000' ");
            }

            sQuery = sInicio + string.Format(
                " Select P.*, F.* From vw_Proveedores P (Nolock) " +
                " Inner Join CatFarmacias_ProveedoresVales F (NOLOCK) " +
		            " On ( P.IdProveedor = F.IdProveedor ) " +
                " Where F.IdEstado = '{0}' and F.IdFarmacia = '{1}'  {2} and F.Status = 'A' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), sFiltroProveedor);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Farmacias_Proveedores_Vales

        #region Ubicaciones_Estandar
        public DataSet UbicacionesEstandar(string Funcion)
        {
            return UbicacionesEstandar("", Funcion);
        }

        public DataSet UbicacionesEstandar(string NombrePosicion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Posicion Estandar.";
            string sMsjNoEncontrado = "Nombre de Posición Estandar no encontrada, verifique.";
            string sWhere = "";

            if (NombrePosicion != "")
            {
                sWhere = string.Format(" Where NombrePosicion = '{0}' ", NombrePosicion );
            }

            sQuery = sInicio + string.Format(" Select * From Cat_ALMN_Ubicaciones_Estandar {0}  Order By NombrePosicion ", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet UbicacionesEstandarALMN(string Empresa, string Estado, string Farmacia, string Funcion)
        {
            return UbicacionesEstandarALMN( Empresa, Estado, Farmacia, "", Funcion);
        }

        public DataSet UbicacionesEstandarALMN(string Empresa, string Estado, string Farmacia, string Posicion, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Posicion Estandar.";
            string sMsjNoEncontrado = "Nombre de Posición Estandar no encontrada, verifique.";
            string sWhere = "";

            if (Posicion != "")
            {
                sWhere = string.Format(" and Posicion = '{0}' ", Posicion);
            }

            sQuery = sInicio + string.Format(" Select * From vw_CFG_ALMN_Ubicaciones_Estandar " +
                                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' {3}  Order By Posicion ", 
                                            Empresa, Estado, Farmacia, sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet LotesDeCodigoEAN_Ubicacion_Estandar(string IdEmpresa, string IdEstado, string IdFarmacia, int IdRack, int IdNivel, int IdEntrepaño,
            string IdCodigo, string IdCodEAN, TiposDeInventario TipoDeInventario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los lotes del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada, verifique.";
            string sFiltroConsignacion = " ";
            
            switch (TipoDeInventario)
            {
                case TiposDeInventario.Todos:
                    sFiltroConsignacion = " ";
                    break;

                case TiposDeInventario.Venta:
                    sFiltroConsignacion = " and ClaveLote not like '%*%' ";
                    break;

                case TiposDeInventario.Consignacion:
                    sFiltroConsignacion = " and ClaveLote like '%*%' ";
                    break;
            }

            sQuery = sInicio + string.Format("	" +
                   " Select F.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, " +
                   "    {6} as IdPasillo, {7} as IdEstante, {8} as IdEntrepano, " +
                   "    (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, " +
                   "    cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) as Existencia, " +
                   "    cast((L.Existencia - (L.ExistenciaEnTransito + L.ExistenciaSurtidos)) as Int) as ExistenciaDisponible, " +
                   " 0 as Cantidad " +
                   " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                   " Inner join CatFarmacias_SubFarmacias F (NoLock) " +
                   "    On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) " +
                   " Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}'  " +
                   "    and L.IdProducto = '{3}' and CodigoEAN = '{4}' {5}  " +
                   " Order by F.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote ",
                   IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, IdRack, IdNivel, IdEntrepaño );

      
            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }
        #endregion Ubicaciones_Estandar
        
    }
}
