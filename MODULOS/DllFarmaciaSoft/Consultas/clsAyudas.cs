using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using DllRobotDispensador;

namespace DllFarmaciaSoft
{
    public partial class clsAyudas
    {
        #region Declaración de variables
        // private wsFarmaciaSoftGn.wsConexion cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        private DialogResult myResult = new DialogResult();

        private DataSet dtsError = new DataSet();
        private DataSet dtsClase = new DataSet();
        // private object objEnviar = null;
        // private object objRecibir = null;
        private string strCnnString = "";
        private bool bUsarCnnRedLocal = true, bExistenDatos = false, bEjecuto = false; //bError = false 
        private string sSql = "", strResultado = ""; // , sOrderBy = ""; 
        private string strMsjNoDatos = "No existe información para mostrar.";
        string sInicio = "Set DateFormat YMD ";

        private clsConsultas query;
        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private FrmAyuda Frm_Ayuda;

        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;        
        private string Name = "DllFarmaciaSoft.clsAyudas";
        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = false;
        bool bEsPublicoGeneral = false;
        int iColumnaInicial = 1; 
        bool bEsClienteIMach = RobotDispensador.Robot.EsClienteInterface;

        // Consulta ejecutada
        protected string sConsultaExec = "";

        #endregion

        #region Constructores de Clase y Destructor
        public clsAyudas()
        {
            bUsarCnnRedLocal = General.ServidorEnRedLocal;
            strCnnString = Name;
            strCnnString = General.CadenaDeConexion;
            //Name = Name;
            // cnnWebServ = new wsConexion.wsConexionDB();
            // cnnWebServ.Url = General.Url;
        }

        public clsAyudas(string prtCnnString, string cnnWebUrl, bool bUsarRedLocal)
        {
            bUsarCnnRedLocal = bUsarRedLocal;
            strCnnString = prtCnnString;
            // cnnWebServ.Url = cnnWebUrl;
        }

        public clsAyudas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, DatosApp.Modulo, Pantalla, DatosApp.Version);
        }

        public clsAyudas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = false;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, Modulo, Pantalla, Version);
        }

        public clsAyudas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            ConexionSql.SetConnectionString();
            query = new clsConsultas(DatosConexion, Modulo, Pantalla, Version, MostrarMsjLeerVacio);
        }
        #endregion Constructores de Clase y Destructor
        
        #region Funciones y procedimientos publicos
        #region Propiedades
        public bool ExistenDatos
        {
            get { return bExistenDatos; }
        }

        public bool MostrarMsjSiLeerVacio
        {
            get { return bMostrarMsjLeerVacio; }
            set { bMostrarMsjLeerVacio = value; }
        }

        public bool EsPublicoGeneral
        {
            get { return bEsPublicoGeneral; }
            set 
            { 
                bEsPublicoGeneral = value;
                query.EsPublicoGeneral = value; 
            }
        }
        /// <summary>
        /// Devuelve el ultimo Query ejecutado por la instancia de la clase
        /// </summary>
        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion Propiedades 

        #region Catalogos 
        public DataSet Empresas(string Funcion)
        {
            string strMsj = "Catálogo de Empresas";

            sSql = "Select  'Nombre corto de Empresa' = NombreCorto, 'Nombre de Empresa' = Nombre, 'Clave de Empresa' = IdEmpresa From CatEmpresas (NoLock) Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Empresas'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Empresas(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ClasificacionesSSA(string Funcion)
        {
            string strMsj = "Catálogo de Clasificaciones SSA";
            sConsultaExec = "";

            sSql = "Select Descripcion as Clasificacion, 'Clave de Clasificacion' = IdClasificacion From CatClasificacionesSSA (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClasificacionesSSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ClasificacionesSSA(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Familias(string Funcion)
        {
            string strMsj = "Catálogo de Familias";
            sConsultaExec = "";

            sSql = "Select Descripcion as Familia, 'Clave de Familia' = IdFamilia From CatFamilias (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Familias'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Familias(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SubFamilias(string Funcion, string Familia )
        {
            string strMsj = "Catálogo de Sub-Familias";
            sConsultaExec = "";

            sSql = "Select 'Sub-Familia' = Descripcion , 'Clave de Sub-Familia' = IdSubFamilia " + 
                   "From CatSubFamilias (NoLock) " +
                   "Where IdFamilia = '" + Fg.PonCeros(Familia, 2) + "' " +
                   "Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'SubFamilias'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SubFamilias(strResultado, Familia, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Presentaciones(string Funcion)
        {
            string strMsj = "Catálogo de Presentaciones";
            sConsultaExec = "";

            sSql = "Select Descripcion as Presentacion, 'Clave de Presentacion' = IdPresentacion From CatPresentaciones (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Presentaciones'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Presentaciones(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Laboratorios(string Funcion)
        {
            string strMsj = "Catálogo de Laboratorios";
            sConsultaExec = "";

            sSql = "Select Descripcion as Laboratorio, 'Clave de Laboratorio' = IdLaboratorio From CatLaboratorios (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Laboratorios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Laboratorios(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet LaboratoriosPorClaveSSA(string sClaveSSA, string Funcion)
        {
            string strMsj = "Catálogo de Laboratorios";
            sConsultaExec = "";

            sSql = string.Format("Select Distinct L.Descripcion as Laboratorio, 'Clave de Laboratorio' = L.IdLaboratorio From CatLaboratorios L (NoLock) " +
                    "Inner Join vw_Productos_CodigoEAN P (NoLock) On (L.IdLaboratorio = P.IdLaboratorio) Where P.ClaveSSA = '{0}' Order by L.Descripcion ", sClaveSSA);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Laboratorios Por ClaveSSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Laboratorios(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet TipoDeProductos(string Funcion)
        {
            string strMsj = "Catálogo de Tipo de Productos";
            sConsultaExec = "";

            sSql = "Select Descripcion as 'Tipo de Producto', 'Clave de Tipo' = IdTipoProducto From CatTiposDeProducto (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'TipoDeProductos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.TipoDeProductos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Regiones(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Regiones";

            sSql = "Select Descripcion as Region, 'Id de Region' = IdRegion From CatRegiones (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Regiones", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Regiones(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SubRegiones(string Funcion, string Region)
        {
            string strMsj = "Catálogo de SubRegiones";

            sSql = "Select Descripcion as Descripcion, 'Id SubRegion' = IdSubRegion " +
                   "From CatSubRegiones (NoLock) " +
                   "Where IdRegion = '" + Fg.PonCeros(Region, 2) + "' " +
                   "Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de SubRegiones", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SubRegiones(strResultado, Region, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ClavesSSA_Sales(string Funcion)
        {
            return ClavesSSA_Sales(2, 2, false, Funcion); 
        }

        public DataSet ClavesSSA_Sales(int ClavesDeControlados, bool BuscarPorClaveSSA, string Funcion)
        {
            return ClavesSSA_Sales(ClavesDeControlados, 2, false, Funcion); 
        }

        public DataSet ClavesSSA_Sales(int ClavesDeControlados, int ClavesDeAntibioticos, bool BuscarPorClaveSSA, string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catálogo de Claves SSA";
            sConsultaExec = "";
            string sFiltroControlados = "";
            string sFiltroAntibioticos = "";
            string sFiltroClaves = "";
            string sFiltro_General = "Where 1 = 1 ";

            if (ClavesDeControlados != 2)
            {
                sFiltroControlados = string.Format("  And EsControlado = '{0}' ", ClavesDeControlados);
                sFiltro_General += sFiltroControlados; 
            }

            if (ClavesDeAntibioticos != 2)
            {
                sFiltroAntibioticos = string.Format("  And EsAntibiotico = '{0}' ", ClavesDeAntibioticos);
                sFiltro_General += sFiltroAntibioticos;
            }


            if (BuscarPorClaveSSA)
            {
                sFiltroClaves = "  , ClaveSSA";
            }

            ////sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA' =  C.ClaveSSA, C.Descripcion, 'Grupo Terapeutico' = G.Descripcion, " + 
            ////       " 'Tipo Catálogo' = T.Descripcion, IdClaveSSA_Sal as Codigo " + 
            ////       " From CatClavesSSA_Sales C (noLock) " +
            ////       " Inner Join CatGruposTerapeuticos G (NoLock) On ( C.IdGrupoTerapeutico = G.IdGrupoTerapeutico ) " +
            ////       " Inner Join CatTipoCatalogoClaves T (NoLock) On ( C.TipoCatalogo = T.TipoCatalogo ) " +
            ////       ""; // " Order by C.Descripcion "; 


            // 2K110921.1311  
            sSql = string.Format(" Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA Base' = C.ClaveSSA_Base, 'Clave SSA' = C.ClaveSSA_Aux, Presentacion, " + 
                " 'Descripción Clave' = DescripcionClave, " + 
                " 'Grupo Terapeutico' = GrupoTerapeutico, " +
                " 'Tipo Catálogo' = TipoDeCatalogo, IdClaveSSA_Sal as Codigo {1} " +
                " From vw_ClavesSSA_Sales C (noLock) "+
                " {0} ", sFiltro_General, sFiltroClaves); 

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if ( 1 == 1 ) 
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_Sales'", true, 5);
                dtsClase = new DataSet(); 

                if (strResultado != "") 
                {

                    dtsClase = query.ClavesSSA_Sales(strResultado, BuscarPorClaveSSA, ClavesDeControlados, ClavesDeAntibioticos, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                } 
            } 
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet ClavesSSA_Sales_Existencia(string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catalogo de Claves SSA";
            sConsultaExec = "";

            ////sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA' =  C.ClaveSSA, C.Descripcion, 'Grupo Terapeutico' = G.Descripcion, " + 
            ////       " 'Tipo Catálogo' = T.Descripcion, IdClaveSSA_Sal as Codigo " + 
            ////       " From CatClavesSSA_Sales C (noLock) " +
            ////       " Inner Join CatGruposTerapeuticos G (NoLock) On ( C.IdGrupoTerapeutico = G.IdGrupoTerapeutico ) " +
            ////       " Inner Join CatTipoCatalogoClaves T (NoLock) On ( C.TipoCatalogo = T.TipoCatalogo ) " +
            ////       ""; // " Order by C.Descripcion "; 


            // 2K110921.1311  
            sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA Base' = C.ClaveSSA_Base, 'Clave SSA' = C.ClaveSSA_Aux, Presentacion, " +
                " 'Descripción Clave' = DescripcionClave, " +
                " 'Grupo Terapeutico' = GrupoTerapeutico, " +
                " 'Tipo Catálogo' = TipoDeCatalogo, IdClaveSSA_Sal as Codigo, ClaveSSA" +
                " From vw_ClavesSSA_Sales C (noLock) ";

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_Sales'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Sales_Existencia(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Existencia_ClavesSSA_Sales(string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catalogo de Claves SSA";
            sConsultaExec = "";
                        
            sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA Base' = C.ClaveSSA_Base, 'Clave SSA' = C.ClaveSSA_Aux, Presentacion, " +
                " 'Descripción Clave' = DescripcionClave, " +
                " 'Grupo Terapeutico' = GrupoTerapeutico, " +
                " 'Tipo Catálogo' = TipoDeCatalogo, IdClaveSSA_Sal as Codigo, ClaveSSA" +
                " From vw_ClavesSSA_Sales C (noLock) ";

            dtsClase = new DataSet();
            
            if (1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_Sales'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Existencia_ClavesSSA_Sales(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            
            return dtsClase;
        }

        public DataSet Productos(string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            sSql = "Select Descripcion as Producto, 'Clave de Producto' = IdProducto From CatProductos (NoLock) Order by Descripcion ";
            sConsultaExec = ""; 

            sSql = string.Format(" Select 'Código EAN.' = CodigoEAN, 'Descripción' = Descripcion, " + 
                " 'Tipo' = TipodeProducto, 'Clave SSA' = ClaveSSA, 'Sustancia activa' = DescripcionSal, " +
                " 'Presentación' = Presentacion, Laboratorio, " +
                " IdProducto as Codigo, 'Código EAN' = CodigoEAN " +
                " From vw_Productos_CodigoEAN (NoLock) Order by Descripcion  "); 
            // sOrderBy = " Order by Descripcion "; 

            dtsClase = new DataSet();
            ////dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos'", "");

            //// bExistenDatos = true;
            //if (bExistenDatos)
            if ( 1 == 1 ) 
            {
                bExistenDatos = false; 
                //strResultado = MostrarForma(strMsj, dtsClase, true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos'", true, 2); 
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos_CodigosEAN_Datos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado; 
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Sales_Antibioticos_Controlados(bool BuscarPorClaveSSA, bool BuscarPorProducto,
            bool EsControlado, bool EsAntibiotico, string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            sSql = "Select Descripcion as Producto, 'Clave de Producto' = IdProducto From CatProductos (NoLock) Order by Descripcion ";
            sConsultaExec = "";
            string sExtra = "";
            string sTipo = "";

            sTipo = "IdCLaveSSA_Sal As Clave";
            if (EsControlado)
            {
                sExtra = " Where EsControlado = 1  ";
            }
            if (EsAntibiotico)
            {
                sExtra = " Where EsAntibiotico = 1  ";
            }

            if (BuscarPorClaveSSA)
            {
                sTipo = "ClaveSSA As Clave";
            }
                sSql = string.Format(" Select IdCLaveSSA_Sal As IdClave, ClaveSSA, ClaveSSA_Base, DescripcionSal, TipoDeClaveDescripcion As Tipo, {1}" +
                       " From vw_clavesSSA_Sales (NoLock) {0} Order by DescripcionSal  ", sExtra, sTipo);
            

            if (BuscarPorProducto)
            {
                sSql = string.Format(" Select CodigoEAN, Descripcion, TipodeProducto as Tipo, 'Sustancia activa' = DescripcionSal, " +
                       "    Presentacion, Laboratorio, " +
                       "    IdProducto as Codigo" +
                       " From vw_Productos_CodigoEAN (NoLock) {0} Order by Descripcion  ", sExtra);
            }

            dtsClase = new DataSet();

            if (1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos'", true, 2);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Sales_Antibioticos_Controlados(strResultado, BuscarPorClaveSSA, BuscarPorProducto, EsControlado, EsAntibiotico, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }

        public DataSet Productos(string Funcion, string IdProducto)
        {
            string strMsj = "Catálogo de Productos";
            IdProducto = Fg.PonCeros(IdProducto, 8);

            sSql = "Select 'Codigo EAN' = CodigoEAN, Descripcion as Producto, 'Clave de Producto' = IdProducto, CodigoEAN " + 
                " From vw_Productos_CodigoEAN (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos'", "");

            //if (bExistenDatos)
            if ( 1 == 1 )
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos'", true, 2); 
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Productos_CodigoEAN(string Funcion) 
        {
            string strMsj = "Catálogo de Productos";
            // IdProducto = Fg.PonCeros(IdProducto, 8);

            sSql = "Select 'Codigo EAN' = CodigoEAN, 'Clave de Producto' = IdProducto, " + 
                " ClaveSSA, Descripcion as Producto, 'Sustancia Activa' = DescripcionSal, Laboratorio, CodigoEAN " +
                " From vw_Productos_CodigoEAN (NoLock) Order by Descripcion "; 
            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos_CodigosEAN'", "");

            //if (bExistenDatos)
            if ( 1 == 1 ) 
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 4);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_CodigosEAN'", true, 4); 
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos_CodigosEAN_Datos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Productos_CodigoEAN_Etiquetas(string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            // IdProducto = Fg.PonCeros(IdProducto, 8);

            sSql = "Select 'Codigo EAN' = CodigoEAN, 'Clave de Producto' = IdProducto, " +
                " ClaveSSA, Descripcion as Producto, 'Sustancia Activa' = DescripcionSal, Laboratorio, CodigoEAN " +
                " From vw_Productos_CodigoEAN (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos_CodigosEAN'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 4);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_CodigosEAN'", true, 4);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos_CodigosEAN_Datos_Etiquetas(strResultado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Productos_CodigoEAN_Imagenes(string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            // IdProducto = Fg.PonCeros(IdProducto, 8);

            sSql =
                "Select 'Tiene imagenes' = (case when IsNull(I.IdProducto, 'NO') = 'NO' Then 'NO' Else 'SI' end), " + 
                " 'Codigo EAN' = P.CodigoEAN, 'Clave de Producto' = P.IdProducto, " +
                " P.ClaveSSA, P.Descripcion as Producto, 'Sustancia Activa' = P.DescripcionSal, P.Laboratorio, P.CodigoEAN " +
                " From vw_Productos_CodigoEAN P (NoLock) " +
                " Left Join CatProductos_Imagenes I (NoLock) On ( P.IdProducto = I.IdProducto and P.CodigoEAN = I.CodigoEAN ) " + 
                " Order by P.Descripcion ";
            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos_CodigosEAN'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 4);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_CodigoEAN_Imagenes'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos_CodigosEAN_Datos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Clientes(string Funcion)
        {
            string strMsj = "Catálogo de Clientes";

            sSql = "Select Nombre as Cliente, 'Clave de Cliente' = IdCliente From CatClientes (NoLock) Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Clientes'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Clientes(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SubClientes(string Funcion, string sCliente)
        {
            string strMsj = "Catálogo de SubClientes";

            sSql = "Select Nombre as SubCliente, 'Clave de SubCliente' = IdSubCliente From CatSubClientes (NoLock) WHERE IdCliente = '" + Fg.PonCeros(sCliente, 4) + "' " + " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'SubClientes'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SubClientes(sCliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SubClientes_Buscar(string Funcion, string sCliente)
        {
            string strMsj = "Catálogo de SubClientes";

            sSql = "Select Nombre as SubCliente, 'Clave de SubCliente' = IdSubCliente From CatSubClientes (NoLock) WHERE IdCliente = '" + Fg.PonCeros(sCliente, 4) + "' " + " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'SubClientes'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SubClientes(sCliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Proveedores(string Funcion)
        {
            return Proveedores(true, Funcion); 
        }

        public DataSet Proveedores(bool HabilitarProveedorDeReembolso, string Funcion)
        {
            string strMsj = "Catálogo de Proveedores";
            string sFiltroProveedorReembolso = "";

            sConsultaExec = "";
            sFiltroProveedorReembolso = string.Format(" Where IdProveedor >= '0001' ");
            if (HabilitarProveedorDeReembolso)
            {
                sFiltroProveedorReembolso = string.Format(" Where IdProveedor >= '0000' ");
            }

            sSql = string.Format("Select 'Nombre Comercial' = AliasNombre, Nombre as Proveedor, RFC, StatusAux as Status,  'Clave de Proveedor' = IdProveedor " +
                " From vw_Proveedores (NoLock) " + 
                " {0} " +
                " Order by Nombre ", sFiltroProveedorReembolso);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Proveedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Proveedores(strResultado, HabilitarProveedorDeReembolso, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ProveedoresDePedidos(string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Proveedores de Pedidos";
            string sFiltro = "";

            sConsultaExec = "";

            sSql = string.Format("Select 'Nombre Comercial' = P.AliasNombre, P.Nombre as Proveedor, P.RFC, P.StatusAux as Status,  'Clave de Proveedor' = P.IdProveedor " +
                "From vw_Proveedores P (NoLock) " + 
                "Inner Join CFG_CNSGN_Proveedores_SubFarmacias C (NoLock) On ( C.IdEstado = '{0}' and C.IdProveedor = P.IdProveedor ) " +
                "Order by Nombre ", IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Proveedores de pedidos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ProveedoresDePedidos(IdEstado, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        } 

        public DataSet NetProveedores(string Funcion)
        {
            string strMsj = "Catálogo de Proveedores";

            sSql = "Select IsNull(N.LoginProv, '') as Login, 'Nombre Comercial' = P.AliasNombre, P.Nombre as Proveedor, P.RFC, P.StatusAux as Status,  " +
                " 'Clave de Proveedor' = P.IdProveedor " +
                " From vw_Proveedores P (NoLock) " + 
                " Left Join Net_Proveedores N (NoLock) On ( N.IdProveedor = P.IdProveedor ) "  + 
                " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Proveedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 3);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Proveedores(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Programas(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Programas";

            sSql = "Select Descripcion as Programa, 'Id de Programa' = IdPrograma From CatProgramas (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Programas", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Programas(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SubProgramas(string Funcion, string Programa)
        {
            string strMsj = "Catálogo de SubProgramas";

            sSql = "Select Descripcion as Descripcion, 'Id SubPrograma' = IdSubPrograma " +
                   "From CatSubProgramas (NoLock) " +
                   "Where IdPrograma = '" + Fg.PonCeros(Programa, 4) + "' " +
                   "Order by IdSubPrograma ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de SubProgramas", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SubProgramas(Programa, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Estados(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Estados";

            sSql = "Select Nombre as Estado, 'Id de Estado' = IdEstado From CatEstados (NoLock) Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Estados", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Estados(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Municipios(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Estados";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = string.Format("Select Descripcion as Municipio, 'Id de Municipio' = IdMunicipio From CatMunicipios (NoLock) Where IdEstado = '{0}' Order by Descripcion ", IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Municipios", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Municipios(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Colonias(string Funcion, string IdEstado, string IdMunicipio)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Estados";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            IdMunicipio = Fg.PonCeros(IdMunicipio, 4);
            sSql = string.Format("Select Descripcion as Colonia, 'Id de Colonia' = IdColonia " + 
                " From CatColonias (NoLock) Where IdEstado = '{0}' and IdMunicipio = '{1}' Order by Descripcion ", IdEstado, IdMunicipio);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Municipios", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Colonias(IdEstado, IdMunicipio, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Farmacias(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Estados";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = "Select 'Farmacia' = Farmacia, 'Num. Farmacia' = IdFarmacia From vw_Farmacias (NoLock) Where IdEstado = '" + IdEstado + "' Order by Farmacia ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Farmacias", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Farmacias(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        } 

        public DataSet Personal(string Funcion, string IdEstado, string IdFarmacia)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Personal";

            sSql = string.Format(" Select (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, " +
                " 'Clave de personal' = IdPersonal From CatPersonal (NoLock) Where IdEstado = '{0}' and IdFarmacia = '{1}' Order by 1", IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Personal", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Personal(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Jurisdicciones(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Jurisdicciones";

            sSql = string.Format(" Select Descripcion as Jurisdiccion,  'Clave Jurisdiccion' = IdJurisdiccion " +
                " From CatJurisdicciones (NoLock) Where IdEstado = '{0}' Order by 2 ", IdEstado );
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Jurisdicciones", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Jurisdicciones(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet GruposTerapeuticos(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Grupos Terapeuticos";
            sConsultaExec = "";

            sSql = string.Format(" Select Descripcion,  'Clave Grupo' = IdGrupoTerapeutico " +
                " From CatGruposTerapeuticos (NoLock) ");
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Grupos Terapéuticos", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.GruposTerapeuticos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Rotacion(string Empresa, string Estado, string Farmacia, string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Rotacion";
            sConsultaExec = "";

            sSql = string.Format(" Select IdRotacion As Clave, NombreRotacion, Orden, Status, IdRotacion  " +
                " From vw_CFGC_ALMN__Rotacion (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'",
                Empresa, Estado, Farmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Rotación", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Rotacion(Empresa, Estado, Farmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        ////public DataSet PersonalCEDIS(string Empresa, string Estado, string Farmacia, string Funcion)
        ////{
        ////    return PersonalCEDIS(Empresa, Estado, Farmacia, Puestos_CEDIS.Ninguno, Funcion);
        ////}

        public DataSet PersonalCEDIS(string Empresa, string Estado, string Farmacia, Puestos_CEDIS IdPuesto, string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Personal CEDIS";
            sConsultaExec = "";
            string sWhere = "";


            if (IdPuesto != Puestos_CEDIS.Ninguno)
            {
                sWhere += string.Format(" and IdPuesto = '{0}' \n", Fg.PonCeros((int)IdPuesto, 2));
            }


            sSql = string.Format(
                "Select Personal,  'Clave Personal' = IdPersonal \n" +
                "From vw_PersonalCEDIS (NoLock) \n" +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}'\n{3}\n",
                Empresa, Estado, Farmacia, sWhere);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Personal CEDIS", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.PersonalCEDIS(Empresa, Estado, Farmacia, strResultado, IdPuesto, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Vehiculos(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Vehículos";

            sSql = string.Format("Select IdVehiculo As Clave, Marca, Year(Modelo) As Modelo, Descripcion, NumSerie As 'Numero de serie', Placas, Status, IdVehiculo " +
                   "from vw_Vehiculos (NoLock) Where IdEstado = '{0}' And Idfarmacia = '{1}' Order By IdVehiculo ",
                   DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Vehículos", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Vehiculos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        } 

        #endregion Catalogos 

        #region Cuadros Basicos 
        public DataSet CuadrosBasicos_Farmacias(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCliente, string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Cuadro Básico";

            sSql = string.Format(
                "Select 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, ClaveSSA " +
                " From vw_CB_CuadroBasico_Farmacias F (NoLock) " + 
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdCliente = '{2}' ",
                IdEstado, IdFarmacia, IdCliente );
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de CuadrosBasicos_Farmacias", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CuadrosBasicos_Farmacias(IdEmpresa, IdEstado, IdFarmacia, IdCliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Cuadros Basicos

        #region Compras
        public DataSet AlmacenesCompras(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Unidades por Estado";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = string.Format(
                "Select \n" +
                "\t'Almacen' = F.Farmacia, 'Num. Almacen' = F.IdFarmacia \n" +
                "From vw_Farmacias F (NoLock) \n" +
                "Inner Join COM_OCEN_Almacenes_Regionales A (NoLock) On ( F.IdEstado = A.IdEstado and F.IdFarmacia = A.IdFarmacia ) \n" + 
                "Where F.IdEstado = '{0}' and A.Status = 'A' \n"+ 
                "Order by F.Farmacia \n", Fg.PonCeros(IdEstado, 2));
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Almacenes", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.AlmacenesCompras(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet FolioCompras(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Compra";

            sSql = "Select 'Fecha de Registro' = FechaRegistro, 'Clave de Folio' = FolioCompra " +
                   "From ComprasEnc (NoLock) " +
                   "Where IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "And IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "Order by FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'FolioCompras'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FolioEnc_Compras(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Folios_Compras(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Compra";

            sSql = "Select 'Fecha de Registro' = FechaRegistro, 'Clave de Folio' = FolioCompra " +
                   "From ComprasEnc (NoLock) " +
                   "Where IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "And IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "Order by FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'FolioCompras'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FolioEnc_Compras(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        //public DataSet Productos_Compras(string IdEstado, string Funcion)
        //{
        //    string strMsj = "Catálogo de Productos";

        //    sSql = "Select P.Descripcion as Producto, 'Clave de Producto' = P.IdProducto " +
        //    "From CatProductos P(NoLock) " +
        //    "Inner Join CatProductos_Estado PE (NoLock) On ( P.IdProducto = PE.IdProducto ) " +
        //    "Where PE.IdEstado = '" + Fg.PonCeros(IdEstado, 2) + "' " +
        //    "Order by P.Descripcion ";

        //    dtsClase = new DataSet();
        //    // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos_Compras'", "");

        //    // if (bExistenDatos)
        //    if ( 1 == 1 ) 
        //    {
        //        bExistenDatos = false;
        //        //strResultado = MostrarForma(strMsj, dtsClase); 
        //        strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_Compras'", true, 1);
        //        dtsClase = new DataSet();

        //        if (strResultado != "")
        //        {
        //            dtsClase = query.Producto_Compras(strResultado, IdEstado, Funcion);
        //            ValidaDatos(ref dtsClase);
        //        }
        //    }
        //    ////else
        //    ////{
        //    ////    MsjNoDatos(ref dtsClase, strMsj);
        //    ////}

        //    return dtsClase;
        //}

        public DataSet Compras_Clientes(string IdEstado, string IdCliente, string Funcion)
        {
            string sMsjError = "Error en la Ayuda de Clientes Compras";
            string strMsj = "Catálogo de Clientes Compras";
            string sWhere = "";
            string sWherePubGral = "";
            bool bEsCliente = true;


            sSql = sInicio + string.Format(" Select distinct 'Nombre Cliente' = NombreCliente, 'Clave de Cliente' = IdCliente " +
                    " From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                    " Where IdEstado = '{0}' and StatusRelacion = 'A' {1} " + 
                    " Order By IdCliente", IdEstado, sWherePubGral);

            if (IdCliente != "")
            {
                strMsj = "Catálogo de Sub-Clientes Compras";
                sMsjError = "Error en la Ayuda de Sub-Clientes Compras";
                sWhere = string.Format(" and IdCliente = '{0}' ", Fg.PonCeros(IdCliente, 4));
                bEsCliente = false;

                sSql = sInicio + string.Format(" Select distinct 'Nombre SubCliente' = NombreSubCliente, 'Clave de Sub-Cliente' = IdSubCliente " +
                        " From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                        " Where IdEstado = '{0}' and StatusRelacion = 'A' {1} " + 
                        " Order by IdSubCliente ", IdEstado, sWhere); 
            } 

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, sMsjError, "");
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    if (bEsCliente)
                    {
                        dtsClase = query.Compras_Clientes(IdEstado, strResultado, "", Funcion);
                    }
                    else
                    { 
                        dtsClase = query.Compras_Clientes(IdEstado, IdCliente, strResultado, Funcion);
                    }

                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Compras

        #region Usuarios y permisos
        public DataSet Arboles()
        {
            string strMsj = "Catálogo de Árboles";

            sSql = "Select 'Descripción de arbol' = Nombre, 'Clave de arbol' = Arbol " +
                   " From Ctl_Colonias (NoLock)";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(sSql, "Arboles");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Arboles();
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion

        #region Transferencias 
        public DataSet FarmaciasTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, string Funcion)
        {
            return UnidadesTransferencia(IdEmpresa, EsConsignacion, IdEstado, IdFarmaciaLocal, false, Funcion); 
        }

        public DataSet AlmacenesTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, string Funcion)
        {
            return UnidadesTransferencia(IdEmpresa, EsConsignacion, IdEstado, IdFarmaciaLocal, true, Funcion);
        }

        private DataSet UnidadesTransferencia(string IdEmpresa, int EsConsignacion, string IdEstado, string IdFarmaciaLocal, bool SoloAlmacenes, string Funcion)
        {
            string strMsj = "Catálogo de Unidades para Transferencia";

            //                 " Where F.IdEstado = '{1}' and ( F.IdFarmacia <> '{2}' ) and E.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 )  " +
            sSql = string.Format(" Select 'Nombre farmacia' = (F.IdFarmacia + ' - ' + F.Farmacia), 'Status' = F.FarmaciaStatusAux, " +
                " 'Id Jurisdicción' = IdJurisdiccion, 'Jurisdicción' = Jurisdiccion, 'Farmacia' = F.IdFarmacia " +
                " From vw_Farmacias F (NoLock) " +
                " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                " Where F.IdEstado = '{1}' and E.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 ) and Transferencia_RecepcionHabilitada = 1  " +
                " Order by F.Farmacia ",
                IdEmpresa, IdEstado, IdFarmaciaLocal);


            if (GnFarmacia.Transferencias_Interestatales__Farmacias &&
                (DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Almacen || DtGeneral.ModuloMA_EnEjecucion == TipoModulo_MA.Farmacia)
                ) 
            {
                sSql = string.Format(" Select 'Nombre farmacia' = (F.IdFarmacia + ' - ' + F.Farmacia), 'Status' = F.FarmaciaStatusAux, " +
                    " 'Id Jurisdicción' = IdJurisdiccion, 'Jurisdicción' = Jurisdiccion, 'Farmacia' = F.IdFarmacia " +
                    " From vw_Farmacias F (NoLock) " +
                    " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                    " Where F.IdEstado = '{1}' and E.Status = 'A' and F.IdTipoUnidad not in ( 0, 5 ) and Transferencia_RecepcionHabilitada = 1 " +
                    " Order by F.Farmacia ",
                    IdEmpresa, IdEstado, IdFarmaciaLocal);
            }


            if (SoloAlmacenes)
            {
                sSql = string.Format(" Select 'Nombre almacén' = (F.IdFarmacia + ' - ' + F.Farmacia), 'Status' = F.FarmaciaStatusAux, " +
                    " 'Id Jurisdicción' = IdJurisdiccion, 'Jurisdicción' = Jurisdiccion, 'Almacén' = F.IdFarmacia " +
                    " From vw_Farmacias F (NoLock) " + 
                    " Inner Join CFG_EmpresasFarmacias E (NoLock) On ( F.IdEstado = E.IdEstado and F.IdFarmacia = E.IdFarmacia and E.IdEmpresa = '{0}' ) " +
                    " Where F.IdEstado = '{1}' and E.Status = 'A' and EsAlmacen = 1 and F.IdTipoUnidad not in ( 0, 5 ) and Transferencia_RecepcionHabilitada = 1 " +
                    " Order by F.Farmacia ", 
                    IdEmpresa, IdEstado ); 
            }  

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'FarmaciasTransferencia'", 
                    "No se encontraron Unidades para transferencia."); 
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FarmaciasTransferencia(IdEmpresa, EsConsignacion, IdEstado, IdFarmaciaLocal, strResultado, SoloAlmacenes, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Transferencias

        #region Ventas 
        public DataSet Folios_Ventas(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Venta";

            sSql = "SELECT 'Fecha de Registro' = FechaRegistro, 'Folio' = FolioVenta " +
                   "FROM VentasEnc (NoLock) " +
                   "WHERE IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "AND IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "ORDER BY FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'FolioVentas'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FolioEnc_Ventas(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet FormaDePago(string Funcion)
        {
            string strMsj = "Catálogo de Formas de pago";

            sSql = string.Format("Select Descripcion, Status, IdFormasDePago As Clave " + 
                " From CatFormasDepago (NoLock) " +
                " Where IdFormasDePago <> '00' " + 
                " Order By IdFormasDePago ");
            dtsClase = new DataSet();

            bExistenDatos = false;
            strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'FormaDePago'", true, 1);
            dtsClase = new DataSet();

            if (strResultado != "")
            {
                dtsClase = query.FormaDePago(strResultado, Funcion);
                ValidaDatos(ref dtsClase);
            }

            return dtsClase;
        }

        #endregion Ventas 

        #region Inventarios 
        /// <summary>
        /// Muestra el catálogo de Productos válidos para el Estado
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <param name="Funcion"></param>
        /// <returns></returns>
        public DataSet ProductosEstado(string IdEmpresa, string IdEstado, string Funcion) 
        {
            string strMsj = "Catálogo de Productos";
            string sEsSectorSalud = "";

            if (bEsPublicoGeneral)
                sEsSectorSalud = " and EsSectorSalud = 0 "; 


            sSql = string.Format(" Select CodigoEAN, Descripcion, TipodeProducto as Tipo, " +
                   " 'Clave SSA' = ClaveSSA, 'Descripción de la Clave' = DescripcionSal, " + 
		           "    Presentacion, Laboratorio, " + 
		           "    IdProducto as Codigo, 'Codigo EAN' = CodigoEAN " +
	               " From vw_ProductosEstadoFarmacia V (NoLock) Where IdEstado = '{0}' {1} ", Fg.PonCeros(IdEstado,2), sEsSectorSalud);
            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");


            ////if (GnFarmacia.INT_OPM_ProcesoActivo)
            ////{
            ////    ////////////sSql = string.Format(" Select C.CodigoEAN, C.Descripcion, C.TipodeProducto as Tipo, " +
            ////    ////////////        " 'Clave SSA' = C.ClaveSSA, 'Sustancia activa' = C.DescripcionSal, " +
            ////    ////////////        "    C.Presentacion, C.Laboratorio, " +
            ////    ////////////        "    C.IdProducto as Codigo, 'Codigo EAN' = C.CodigoEAN " +
            ////    ////////////        " From vw_ProductosEstadoFarmacia C (NoLock) " + 
            ////    ////////////        " Inner Join INT_ND_Productos I (NoLock) " +
            ////    ////////////        "     On ( I.IdEstado = '{0}' and right(replicate('0', 20) + C.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) ) " +
            ////    ////////////        " Where C.IdEstado = '{0}' {1} ", Fg.PonCeros(IdEstado, 2), sEsSectorSalud);

            ////    //////sSql += "\n ";
            ////    //////sSql += "\t and Exists \n ";
            ////    //////sSql += "\t ( \n ";
            ////    //////sSql += "\t\t Select CodigoEAN_ND From INT_ND_Productos I (NoLock)  \n "; 
            ////    //////sSql += "\t\t Where I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) \n";
            ////    //////sSql += "\t\t\t and Exists ( Select ClaveSSA_ND From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) Where I.ClaveSSA_ND = C.ClaveSSA_ND  ) ";
            ////    //////sSql += "\t ) \n ";
            ////}


            //if (bExistenDatos)
            if ( 1 == 1 ) 
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase,true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_Compras'", true, 2); 
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ProductosEstado(IdEmpresa, IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet ProductosCartaCanje(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioCarta, string Funcion)
        {
            string strMsj = "Catálogo de Productos";

            sSql = string.Format(" Select Distinct(P.CodigoEAN), Descripcion, TipodeProducto as Tipo,  'Clave SSA' = ClaveSSA, 'Sustancia activa' = DescripcionSal, " +
		            " Presentacion, Laboratorio,     P.IdProducto as Codigo, 'Codigo EAN' = P.CodigoEAN " +
                    "From vw_ProductosEstadoFarmacia P (NoLock) " +
                    "Inner Join RutasDistribucionDet_CartasCanje R (NoLock) On (P.IdEstado = R.IdEstado And P.CodigoEAN = R.CodigoEAN) " +
                    "Where R.IdEmpresa = '{0}' And R.IdEstado = '{1}' And R.IdFarmacia = '{2}' And R.FolioCarta = '{3}' ",
                    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(FolioCarta, 8));
            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase,true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos_Compras'", true, 2);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ProductosEstado(IdEmpresa, IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            return ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, "", false, false, Funcion);
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, bool MostrarSoloCuadroBasico, string Funcion)
        {
            return ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, "", MostrarSoloCuadroBasico, false, Funcion); 
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string IdClaveSSA, string Funcion)
        {
            return ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, false, false, Funcion); 
        }

        public DataSet ProductosFarmacia( string IdEmpresa, string IdEstado, string IdFarmacia, string IdClaveSSA,
            bool MostrarSoloCuadroBasico, bool SoloControlados, string Funcion )
        {
            return ProductosFarmacia
                (
                    IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, MostrarSoloCuadroBasico, false, "", "", "", "", 
                    SoloControlados, Funcion
                );
        }

        public DataSet ProductosFarmacia(string IdEmpresa, string IdEstado, string IdFarmacia, string IdClaveSSA,
            bool MostrarSoloCuadroBasico, bool Validar_ClavesEnSubPerfil, 
            string IdCliente, string IdSubCliente, string IdPrograma, string IdSubPrograma, 
            bool SoloControlados, string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            string sMjsError = "Error en la ayuda 'Productos'"; 
            string sEsSectorSalud = "";
            string sFiltro_ClaveSSA = "";
            string sFiltroCuadroBasico = "";
            string sFiltro__SubPerfil = "";
            string sFiltro_Controlados = ""; 

            if (bEsPublicoGeneral)
            {
                sEsSectorSalud = "\n\tand V.EsSectorSalud = 0 ";
            }

            if (IdClaveSSA != "") 
            {
                sFiltro_ClaveSSA = string.Format("\n\tand V.IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA,4));
            }

            if (MostrarSoloCuadroBasico)
            {
                sFiltroCuadroBasico = 
                    "\n\tand Exists \n"+
                    "\t(\n" +
                    "\t\tSelect IdClaveSSA \n" +
                    "\t\tFrom vw_Claves_Precios_Asignados CB (NoLock) \n" +
                    "\t\tWhere CB.IdClaveSSA = V.IdClaveSSA_Sal and CB.Status = 'A' \n" + 
                    "\t) "; 
            }

            // Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdPrograma = '{3}' and IdSubPrograma = '{4}' and Status = 'A'

            if(Validar_ClavesEnSubPerfil)
            {
                sFiltro__SubPerfil = string.Format(
                    "\n\tand Exists \n"+
                    "\t(\n" +
                    "\t\tSelect IdClaveSSA \n" +
                    "\t\tFrom CFG_CB_Sub_CuadroBasico_Claves CB (NoLock) \n" +
                    "\t\tWhere CB.IdClaveSSA = V.IdClaveSSA_Sal and CB.Status = 'A' and " +
                    "\t\t\tCB.IdEstado = '{0}' and CB.IdFarmacia = '{1}' and CB.IdCliente = '{2}' and CB.IdPrograma = '{3}' and CB.IdSubPrograma = '{4}' \n" +
                    "\t) ", IdEstado, IdFarmacia, IdCliente, IdPrograma, IdSubPrograma );
            }

            sFiltro_Controlados = string.Format("\n\tand V.EsControlado in ( 0, 1 ) ");
            if (SoloControlados)
            {
                sFiltro_Controlados = string.Format("\n\tand V.EsControlado in ( 1 ) ");
            }

            ////// Mostrar informacion adicional 
            if (bEsClienteIMach)
            {
                iColumnaInicial = 6;

                if (RobotDispensador.Robot.Interface == RobotDispensador_Interface.Medimat)
                {
                    sSql = string.Format(" Select V.CodigoEAN, cast(V.Existencia as int) as Existencia, \n" +
                           "\t'En Almacen' = (case when IsNull(I.EsMach4, 0) = 0 Then 'NO' Else 'SI' end), \n" +
                           "\t'Stock Almacen' = ExistenciaIMach, \n" +
                           "\t'Clave SSA' = V.ClaveSSA, 'Descripción de la Clave' = V.DescripcionSal, \n" +
                           "\t'Descripción' = V.Descripcion, V.TipodeProducto as Tipo, \n" +
                           "\t'Presentación' = V.Presentacion, V.Laboratorio, \n" +
                           "\t'Código' = V.IdProducto, 'Código EAN' = V.CodigoEAN \n" +
                           "From vw_ProductosExistenEnEstadoFarmacia V (NoLock) \n" +
                           "Left Join vw_Productos_IMach I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) \n" +
                           "Where V.IdEstado = '{0}' {1} and V.IdFarmacia = '{2}' {3} {4} {5} {6}\n",
                           Fg.PonCeros(IdEstado, 2), sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, sFiltroCuadroBasico, sFiltro__SubPerfil, sFiltro_Controlados);
                }

                if (RobotDispensador.Robot.Interface == RobotDispensador_Interface.GPI)
                {
                    sSql = string.Format("Select V.CodigoEAN, cast(V.Existencia as int) as Existencia, \n" +
                           "\t'En Almacen' = (case when IsNull(I.EsGPI, 0) = 0 Then 'NO' Else 'SI' end), \n" +
                           "\t'Stock Almacen' = ExistenciaIGPI, \n" +
                           "\t'Clave SSA' = V.ClaveSSA, 'Descripción de la Clave' = V.DescripcionSal, \n" +
                           "\t'Descripción' = V.Descripcion, V.TipodeProducto as Tipo, \n" +
                           "\t'Presentación' = V.Presentacion, V.Laboratorio, \n" +
                           "\t'Código' = V.IdProducto, 'Código EAN' = V.CodigoEAN \n" +
                           "From vw_ProductosExistenEnEstadoFarmacia V (NoLock) \n" +
                           "Left Join vw_Productos_IGPI I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) \n" +
                           "Where V.IdEstado = '{0}' {1} and V.IdFarmacia = '{2}' {3} {4} {5} {6}\n",
                           Fg.PonCeros(IdEstado, 2), sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, sFiltroCuadroBasico, sFiltro__SubPerfil, sFiltro_Controlados);
                }
            }
            else
            {
                iColumnaInicial = 6;
                sSql = string.Format("Select V.CodigoEAN, cast(V.Existencia as int) as Existencia, \n" +
                      "\t'Descripción' = V.Descripcion, V.TipodeProducto as Tipo, \n" +
                      "\t'Clave SSA' = V.ClaveSSA, 'Descripción de la Clave' = V.DescripcionSal, \n" +
                      "\t'Presentación' = V.Presentacion, V.Laboratorio, \n" +
                      "\t'Código' = V.IdProducto, 'Código EAN' = V.CodigoEAN \n" +
                      "From vw_ProductosExistenEnEstadoFarmacia V (NoLock) \n" +
                      "Where V.IdEstado = '{0}' {1} and V.IdFarmacia = '{2}' {3} {4} {5} {6}\n",
                      Fg.PonCeros(IdEstado, 2), sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, sFiltroCuadroBasico, sFiltro__SubPerfil, sFiltro_Controlados);
            }

            //////if (GnFarmacia.INT_OPM_ProcesoActivo)
            //////{
            //////    //////sSql += "\n ";
            //////    //////sSql += "\t and Exists \n ";
            //////    //////sSql += "\t ( \n ";
            //////    //////sSql += "\t\t Select CodigoEAN_ND From INT_ND_Productos I (NoLock)  \n ";
            //////    //////sSql += "\t\t Where I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20) \n";
            //////    //////sSql += "\t\t\t and Exists ( Select ClaveSSA_ND From INT_ND_CFG_CB_CuadrosBasicos C (NoLock) Where I.ClaveSSA_ND = C.ClaveSSA_ND  ) ";
            //////    //////sSql += "\t ) \n ";


            //////    //////////sSql += "\n ";
            //////    //////////sSql += "\t and Exists \n ";
            //////    //////////sSql += "\t ( \n ";
            //////    //////////sSql += "\t\t Select CodigoEAN From INT_ND_CFG__OPM__CodigosEAN I (NoLock)  \n ";
            //////    //////////sSql += "\t\t Where I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN, 20) \n";
            //////    //////////sSql += "\t ) \n ";  
            //////}            


            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if ( 1 == 1 ) 
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase, true, iColumnaInicial); 
                strResultado = MostrarForma(strMsj, sSql, sMjsError, !Validar_ClavesEnSubPerfil, iColumnaInicial);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, strResultado, IdClaveSSA, SoloControlados, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet ProductosFarmacia_RecetaElectronica(string IdEmpresa, string IdEstado, string IdFarmacia, string ClavesSSA,
            bool MostrarSoloCuadroBasico, string Funcion)
        {
            string strMsj = "Catálogo de Productos";
            string sMjsError = "Error en la ayuda 'Productos'";
            string sEsSectorSalud = "";
            string sFiltro_ClaveSSA = "";
            string sFiltroCuadroBasico = "";

            if (bEsPublicoGeneral)
            {
                sEsSectorSalud = " and V.EsSectorSalud = 0 ";
            }

            if (ClavesSSA != "")
            {
                sFiltro_ClaveSSA = string.Format(" and V.ClaveSSA in ( {0}) ", ClavesSSA );
            }

            if (MostrarSoloCuadroBasico)
            {
                sFiltroCuadroBasico = " and Exists ( " +
                    " Select IdClaveSSA " +
                    " From vw_Claves_Precios_Asignados CB (NoLock) " +
                    " Where CB.IdClaveSSA = V.IdClaveSSA_Sal and CB.Status = 'A' " +
                    " ) ";
            }

            // Mostrar informacion adicional 
            if (bEsClienteIMach)
            {
                iColumnaInicial = 6;
                sSql = string.Format(" Select V.CodigoEAN, cast(V.Existencia as int) as Existencia, " +
                       "    'En Almacen' = (case when IsNull(I.EsMach4, 0) = 0 Then 'NO' Else 'SI' end), " +
                       "    'Stock Almacen' = ExistenciaIMach, " +
                       "    'Clave SSA' = V.ClaveSSA, 'Sustancia activa' = V.DescripcionSal, " +
                       "    V.Descripcion, V.TipodeProducto as Tipo, " +
                       "    V.Presentacion, V.Laboratorio, " +
                       "    V.IdProducto as Codigo, 'Codigo EAN' = V.CodigoEAN " +
                       " From vw_ProductosExistenEnEstadoFarmacia V (NoLock) " +
                       " Left Join vw_Productos_IMach I (noLock) On ( V.IdProducto = I.IdProducto and V.CodigoEAN = I.CodigoEAN  ) " +
                       " Where V.IdEstado = '{0}' {1} and V.IdFarmacia = '{2}' {3} {4} ",
                       Fg.PonCeros(IdEstado, 2), sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, sFiltroCuadroBasico);
            }
            else
            {
                iColumnaInicial = 3;
                sSql = string.Format(" Select V.CodigoEAN, cast(V.Existencia as int) as Existencia, " +
                      "    V.Descripcion, V.TipodeProducto as Tipo, " +
                      "    'Clave SSA' = V.ClaveSSA, 'Sustancia activa' = V.DescripcionSal, " +
                      "    V.Presentacion, V.Laboratorio, " +
                      "    V.IdProducto as Codigo, 'Codigo EAN' = V.CodigoEAN " +
                      " From vw_ProductosExistenEnEstadoFarmacia V (NoLock) " +
                      " Where V.IdEstado = '{0}' {1} and V.IdFarmacia = '{2}' {3} {4} ",
                      Fg.PonCeros(IdEstado, 2), sEsSectorSalud, Fg.PonCeros(IdFarmacia, 4), sFiltro_ClaveSSA, sFiltroCuadroBasico);
            }


            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase, true, iColumnaInicial); 
                strResultado = MostrarForma(strMsj, sSql, sMjsError, true, iColumnaInicial);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ProductosFarmacia(IdEmpresa, IdEstado, IdFarmacia, strResultado, "", false, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Reubicacion_Confirmacion(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de confirmación de reubicaciones";

            sSql = string.Format("Select Folio_Inv As Folio, FolioMovto_Referencia As 'Folio Referencia', IdPersonal_Asignado As 'Id Personal Asignado', " +
                    " PersonalAsignado As 'Personal Asignado', FechaRegistro As 'Fecha Registro', (Case When Confirmada = 1 then 'SI' Else 'NO' End) As Confirmada, Folio_Inv " +
                    "From vw_Ctrl_Reubicaciones C " +
                    "Where C.IdEmpresa = '{0}' And C.IdEstado = '{1}' And C.IdFarmacia = '{2}' ",
                    Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase,true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Reubicacion_Confirmacion'", true, 1);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Reubicacion_Confirmacion(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        #endregion Inventarios

        #region Datos de Hospital
        public DataSet Servicios(string Funcion)
        {
            string strMsj = "Catálogo de Servicios";

            sSql = "Select Descripcion as Servicio, 'Clave de Servicio' = IdServicio From CatServicios (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Servicios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Servicios(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ServiciosAreas(string Funcion, string Servicio)
        {
            string strMsj = "Catálogo de Áreas-Servicios";

            sSql = "Select 'Area' = Descripcion , 'Clave de Area' = IdArea " +
                   "From CatServicios_Areas (NoLock) " +
                   "Where IdArea = '" + Fg.PonCeros(Servicio, 3) + "' " +
                   "Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ServiciosAreas'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ServiciosAreas(Servicio, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Medicos(string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Médicos";

            sSql = "Select (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, 'Cédula Número' = NumCedula, 'Clave de Médico' = IdMedico " +
                   " From CatMedicos (NoLock) " +
                   " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                   " Order by ApPaterno ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Medicos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Medicos(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Especialidades(string Funcion)
        {
            string strMsj = "Catálogo de Especialidades";

            sSql = " Select Descripcion as Especialidad, 'Clave de Especialidad' = IdEspecialidad " +
                   " From CatEspecialidades (NoLock) " + 
                   " Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en ayuda 'Especialidades'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Especialidades(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Beneficiarios( string IdEstado, string IdFarmacia, string Funcion )
        {
            string strMsj = "Catálogo de Beneficiarios";

            sSql = "Select 'Beneficiario' = NombreCompleto, 'Cliente' = NombreCliente, 'Sub Cliente' = NombreSubCliente, 'Clave de Beneficiario' = IdBeneficiario " +
                   " From vw_Beneficiarios (NoLock) " +
                   " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                   "Order by ApPaterno ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Beneficiarios'", "");

            if(bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true);
                dtsClase = new DataSet();
                if(strResultado != "")
                {
                    dtsClase = query.Beneficiarios(IdEstado, IdFarmacia, "", "", strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Beneficiarios(string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string Funcion)
        {
            string strMsj = "Catálogo de Beneficiarios";

            sSql = "Select CURP, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, 'Clave de Beneficiario' = IdBeneficiario " +
                   " From CatBeneficiarios (NoLock) " +
                   " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                   " And IdCliente = '" + IdCliente + "' " + " And IdSubCliente = '" + IdSubCliente + "' " +
                   "Order by ApPaterno ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Beneficiarios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Beneficiarios(IdEstado, IdFarmacia, IdCliente, IdSubCliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet BeneficiosSP(string Funcion)
        {
            string strMsj = "Catálogo de Beneficios SP";

            sSql = "Select 'Clave de Beneficio' = ClaveBeneficio, 'Descripción de Beneficio' = Descripcion, 'Clave' = IdBeneficio " +
                   " From CatBeneficios (NoLock) " +
                   " Where Status = 'A' " +
                   " Order by IdBeneficio ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Beneficios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.BeneficiosSP(strResultado, Funcion);
                    ValidaDatos(ref dtsClase); 
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        public DataSet UnidadesMedicas(string Funcion)
        {
            string strMsj = "Catálogo de Unidades Médicas";
            sConsultaExec = "";

            sSql = "Select NombreUnidadMedica as UnidadMedica, 'Clave de Unidad Medica' = IdUMedica From CatUnidadesMedicas (NoLock) Order by NombreUnidadMedica ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'UnidadesMedicas'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.UnidadesMedicas(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet UnidadesMedicasJurisccion(string IdEstado, string IdJurisdiccion, string Funcion)
        {
            string strMsj = "Catálogo de Unidades Medicas";

            sSql = sInicio + string.Format(" Select CLUES, 'Nombre de Unidad Medica' = NombreUMedica, 'Clave Interna' = IdUmedica " + 
                " From vw_Unidades_Medicas (NoLock) " +
                " Where IdEstado in ( '00', '{0}' ) ", IdEstado, IdJurisdiccion);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Unidades Medicas'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.UnidadesMedicasJurisccion(IdEstado, IdJurisdiccion, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DiagnosticosCIE10(string Funcion)
        {
            string strMsj = "Catálogo de Diagnósticos";

            sSql = "Select 'Clave Diagnóstico' = ClaveDiagnostico, 'Diagnóstico' = Descripcion, 'Clave' = ClaveDiagnostico " +
                   " From CatCIE10_Diagnosticos (NoLock) " +
                   " Where len(ClaveDiagnostico) >= 4 " +
                   " Order by IdDiagnostico ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Diagnosticos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DiagnosticosCIE10(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CURPS_Genericas( string IdEstado, string IdFarmacia, string Funcion )
        {
            string strMsj = "Catálogo de CURPS genéricas";

            sSql = string.Format(
                "Select (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, CURP \n" +
                "From CFGC_CurpsGenericas (NoLock) \n" + 
                "Where IdEstado = '{0}' and IdFarmacia = '{1}'  " +
                "Order by IdCURP \n", IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CURPS Genericas'", "");

            if(bExistenDatos)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if(strResultado != "")
                {
                    dtsClase = query.CURPS_Genericas(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Datos de Hospital

        #region Almacenes Jurisdiccionales 
        public DataSet CentrosDeSalud(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Centros de Salud";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = string.Format("Select 'Centro de Salud' = Descripcion, 'Id de Centro' = IdCentro From CatCentrosDeSalud (NoLock) Where IdEstado = '{0}' Order by Descripcion ", IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Centros de Salud", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CentrosDeSalud(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Almacenes Jurisdiccionales

        #region Datos Configurables por Farmacia
        public DataSet Farmacia_Clientes(string IdPublicoGeneral, bool EsPublicoGral,
            string IdEstado, string IdFarmacia, string Funcion)
        {
            return Farmacia_Clientes(IdPublicoGeneral, EsPublicoGral, IdEstado, IdFarmacia, "", Funcion);
        }

        public DataSet Farmacia_Clientes(string IdPublicoGeneral, bool EsPublicoGral, 
            string IdEstado, string IdFarmacia, string IdCliente, string Funcion)
        {
            string strMsj = "Catálogo de Clientes de Farmacia";
            string sWhere = "";
            string sWherePubGral = "";
            bool bEsCliente = true;

            if (IdCliente != "")
            {
                sWhere = string.Format(" and IdCliente = '{0}' ", Fg.PonCeros(IdCliente,4));
                bEsCliente = false;
            }

            if (!EsPublicoGral)
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral);

            if (bEsCliente)
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Cliente' = NombreCliente, 'Clave de Cliente' = IdCliente " +
                     " From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                     " Where IdEstado = '{0}' and IdFarmacia = '{1}' and StatusRelacion = 'A' {2} ",
                     IdEstado, IdFarmacia, sWherePubGral);
            }
            else
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Sub-Cliente' = NombreSubCliente, 'Clave de Sub-Cliente' = IdSubCliente " +
                     " From vw_Farmacias_Clientes_SubClientes (NoLock) " +
                     " Where IdEstado = '{0}' and IdFarmacia = '{1}' and StatusRelacion = 'A' {2} {3} ",
                     IdEstado, IdFarmacia, sWherePubGral, sWhere); 
            }

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Clientes de Farmacia'", "");
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    if (bEsCliente)
                        dtsClase = query.Farmacia_Clientes(IdPublicoGeneral, false, IdEstado, IdFarmacia, strResultado, "", Funcion);
                    else
                        dtsClase = query.Farmacia_Clientes(IdPublicoGeneral, false, IdEstado, IdFarmacia, IdCliente, strResultado, Funcion);
                    
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }


        public DataSet Farmacia_Clientes_Programas(string IdPublicoGeneral, bool EsPublicoGral,
            string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string Funcion)
        {
            return Farmacia_Clientes_Programas(IdPublicoGeneral, EsPublicoGral, IdEstado, IdFarmacia, IdCliente, IdSubCliente, "", Funcion);
        }

        public DataSet Farmacia_Clientes_Programas(string IdPublicoGeneral, bool EsPublicoGral,
            string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string IdPrograma, string Funcion)
        {
            string strMsj = "Catálogo de Programas de Clientes de Farmacia";
            string sWhere = "";
            string sWherePubGral = "";
            string sWhereFarmacia = ""; 
            bool bEsCliente = true;

            if (IdPrograma != "")
            {
                sWhere = string.Format(" and IdPrograma = '{0}' ", Fg.PonCeros(IdPrograma, 4));
                bEsCliente = false;
            }

            if (sWhereFarmacia != "")
            {
                sWhereFarmacia = string.Format(" and IdFarmacia = '{0}' ", IdFarmacia); 
            }

            if (!EsPublicoGral)
            {
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral);
            }

            if (bEsCliente)
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Programa' = Programa, 'Clave de Programa' = IdPrograma " +
                     " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                     " Where IdEstado = '{0}'  {1}  and IdCliente = '{2}' and IdSubCliente = '{3}' and StatusRelacion = 'A' {4} ",
                     IdEstado, sWhereFarmacia, IdCliente, IdSubCliente, sWherePubGral);
            }
            else
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Sub-Programa' = SubPrograma, 'Clave de Sub-Programa' = IdSubPrograma " +
                     " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                     " Where IdEstado = '{0}'   {1}   and IdCliente = '{2}' and IdSubCliente = '{3}' and StatusRelacion = 'A' {4} {5} ",
                     IdEstado, sWhereFarmacia, IdCliente, IdSubCliente, sWherePubGral, sWhere);
            }

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Programas de Clientes de Farmacia'", "");
            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    if (bEsCliente)
                    {
                        dtsClase = query.Farmacia_Clientes_Programas(IdPublicoGeneral, IdEstado, IdFarmacia, IdCliente, IdSubCliente, strResultado, "", Funcion);
                    }
                    else
                    {
                        dtsClase = query.Farmacia_Clientes_Programas(IdPublicoGeneral, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, strResultado, Funcion);
                    }

                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }        
        
        #endregion Datos Configurables por Farmacia

        #region Tiempo Aire 
        public DataSet CompaniasTA(string Funcion)
        {
            string strMsj = "Catálogo de Compañias de Tiempo Aire";

            sSql = "Select Descripcion as Compañia, 'Clave de Compañia' = IdCompania From CatCompaniasTiempoAire (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CompaniasTA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CompaniasTA(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet PersonalTA(string Funcion)
        {
            string strMsj = "Catálogo de Personal de Tiempo Aire";

            sSql = "Select Nombre, 'Clave de Personal' = IdPersonal " +
                   " From CatPersonalTA (NoLock) " +
                   " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'PersonalTA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.PersonalTA(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Tiempo Aire

        #region Operaciones Supervizadas
        public DataSet OperacionesSupervidas(string Funcion)
        {
            string strMsj = "Catálogo de Operaciones Supervisadas";

            sSql = "Select Status, IdOperacion, Nombre, Descripcion, IdOperacion " +
                   " From Net_Operaciones_Supervisadas (NoLock) " +
                   " Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'OperacionesSupervidas'", "");

            if (bExistenDatos)
            { 
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.OperacionesSupervidas(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Operaciones Supervizadas

        #region CatalogosSAT
        public DataSet CFD_UnidadesDeMedida(string Funcion)
        {
            string strMsj = "Catálogo de Unidades de medida SAT";

            sSql = " Select IdUnidad As Clave, Descripcion As 'Unidad de Medida', DescripcionGeneral As 'Descripcion General', NotaObservacion, Simbolo, Version, Status, IdUnidad As Clave " +
                   " From FACT_CFD_UnidadesDeMedida " +
                   " Order by IdUnidad ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'CFD_UnidadesDeMedida'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFD_UnidadesDeMedida(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Productos_Servicios(string Funcion)
        {
            string strMsj = "Catálogo de productos SAT";

            sSql = " Select Clave As código, Descripcion As Producto, Status, Clave From FACT_CFDI_Productos_Servicios Order by Clave ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'CFDI_Productos_Servicios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Productos_Servicios(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        #endregion CatalogosSAT

        #region Asignacion de Precios Claves SSA
        public DataSet ClientesClavesSSA_Asignadas(string Funcion)
        {
            return ClientesClavesSSA_Asignadas("", Funcion); 
        }

        public DataSet ClientesClavesSSA_Asignadas(string IdCliente, string Funcion)
        {
            string strMsj = "Catálogo de Clientes";
            bool bEsSubCliente = false;

            sSql = string.Format("Select Distinct NombreCliente as Cliente, P.Status, 'Clave de Cliente' = C.IdCliente " + 
                " From vw_Clientes_SubClientes C (NoLock) " +
                " Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdCliente = P.IdCliente and C.IdSubCliente = P.IdSubCliente and P.Status = 'A' ) " + 
                " ");

            if (IdCliente != "")
            {
                bEsSubCliente = true;
                sSql = string.Format("Select Distinct 'Nombre Sub-Cliente' = NombreSubCliente, P.Status, 'Clave de Sub-Cliente' = C.IdSubCliente " +
                    " From vw_Clientes_SubClientes C (NoLock) " +
                    " Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdCliente = P.IdCliente and C.IdSubCliente = P.IdSubCliente and P.Status = 'A' ) " +
                    " Where C.IdCliente = '{0}' ", Fg.PonCeros(IdCliente, 4));
            } 

            
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Clientes'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    if ( bEsSubCliente ) 
                        dtsClase = query.ClientesClavesSSA_Asignadas(IdCliente, strResultado, Funcion); 
                    else
                        dtsClase = query.ClientesClavesSSA_Asignadas(strResultado, Funcion); 

                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ClavesSSA_Asignadas_A_Clientes(string IdCliente, string IdSubCliente, string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Asignadas a Sub-Cliente";

            sSql = string.Format(" Select 'Codigo Interno' = C.IdClaveSSA_Sal, 'Clave SSA' =  C.ClaveSSA, " + 
                " 'Descripción' = C.DescripcionSal, 'Grupo Terapeutico' = C.GrupoTerapeutico, " + 
                " 'Tipo Catálogo' = C.TipoDeCatalogo, C.IdClaveSSA_Sal as Codigo  " +
                " From vw_ClavesSSA_Sales C (NoLock) " +
                " Inner Join CFG_Clientes_SubClientes_Claves P (NoLock) On ( C.IdClaveSSA_Sal = P.IdClaveSSA_Sal )  " +
                " Where P.IdCliente = '{0}' and P.IdSubCliente = '{1}'  and P.Status = 'A' ",
                Fg.PonCeros(IdCliente, 4), Fg.PonCeros(IdSubCliente, 4) ); // + Fg.PonCeros(IdCliente, 4) + "'";


            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Claves SSA'", "");

            //if (bExistenDatos)
            if ( 1 == 1)
            {
                bExistenDatos = false;
                ////strResultado = MostrarForma(strMsj, dtsClase);
                ////strResultado = MostrarForma(strMsj, dtsClase);
                strResultado = MostrarForma(strMsj, sSql, "Error la ayuda 'Productos'", true, 2); 
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Asignadas_A_Clientes(IdCliente, IdSubCliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet ClavesSSA_PreciosAsignados(string Estado, string Cliente, string Funcion)
        {
            string strMsj = "Catálogo de ClavesSSA";

            sSql = "Select 'Id_ClaveSSA' = IdClaveSSA, ClaveSSA, 'Descripción' = DescripcionClave, 'Status' = StatusRelacion, IdClaveSSA " +
                   " From vw_Claves_Precios_Asignados (NoLock) " +
                   " Where IdEstado = '" + Estado + "' And IdCliente = '" + Cliente + "' " +
                   " Order by DescripcionClave ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'ClavesSSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_PreciosAsignados(Estado, Cliente, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        #endregion Asignacion de Precios Claves SSA

        #region Pedidos_Distribuidores
        public DataSet Folios_Pedidos(string IdEmpresa, string IdEstado, string IdFarmacia, string TipoEntrada, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Pedido";

            sSql = "Select 'Fecha de Registro' = FechaRegistro, 'Folio' = FolioPedido " +
                   "From PedidosEnc (NoLock) " +
                   "Where IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "And IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "And TipoDeEntrada  = '" + TipoEntrada + "' " +
                   "Order by FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Folios_Pedidos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Folio_PedidosEnc(IdEmpresa, IdEstado, IdFarmacia, strResultado, TipoEntrada, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Distribuidores(string Funcion)
        {
            string strMsj = "Catálogo de Distribuidores";
            sConsultaExec = "";

            sSql = "Select 'Nombre Distribuidor' = NombreDistribuidor, (case when Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status,  'Clave de Distribuidor' = IdDistribuidor " +
                " From CatDistribuidores (NoLock) Order by NombreDistribuidor ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Distribuidores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 1);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Distribuidores(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DistribuidoresProductos(string Funcion)
        {
            string strMsj = "Catálogo de Productos por Distribuidor";
            sConsultaExec = "";

            sSql = string.Format("Select 'Codigo' = CodigoDistribuidor, CodigoEAN, 'Tipo' = TipoDeCodigo, " +
                " Descripcion, 'Clave SSA' = ClaveSSA, DescripcionCortaClave,  " +
                " 'IdProducto distribucion' = IdProductoDist " +
                " From vw_Productos_Distribuidor V (NoLock) " +
                " Order by IdProductoDist ");
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'DistribuidoresProductos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 4);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DistribuidorProductos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DistribuidoresProductos_ClaveSSA(string Funcion)
        {
            string strMsj = "Catálogo de Productos para Distribución por Clave SSA";
            dtsClase = new DataSet();

            sSql = "Select 'ID. Distribución' = IdProductoDist, 'Distribuidor' = NombreDistribuidor, 'Clave Interna' = IdClaveSSA_Sal, " +
                "'Clave SSA' = ClaveSSA_Base, 'Descripción Sal' = DescripcionSal, 'Código de Distribución' = CodigoDistribuidor, 'Tipo de Distribución' = Descripcion, " +
                "'Status' = StatusAux, IdProductoDist From vw_Distribuidor_Productos_ClaveSSA(NoLock) Order By IdProductoDist";

            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Catálogo de Productos para Distribución por Clave SSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DistribuidorProductos_ClaveSSA(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Productos_ClaveSSA(string Funcion)
        {
            string strMsj = "Catálogo de Productos para Distribución por Clave SSA";
            dtsClase = new DataSet();

            sSql = "Select 'ClaveInterna' = IdClaveSSA_Sal, 'Clave SSA' = ClaveSSA_Base, 'Descripción Sal' = DescripcionSal, IdClaveSSA_Sal " +
                   "From vw_ClavesSSA_Sales(NoLock)" +
                   "Order By IdClaveSSA_Sal";
            dtsClase = EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Catálogo de Productos para Distribución por Clave SSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.Productos_ClaveSSA_Datos(strResultado, Funcion);

                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DistribuidoresProductos(string IdDistribuidor, string TipoCodigo, string Funcion)
        {
            string strMsj = "Catálogo de Productos por Distribuidor";
            string sFiltroTipoCodigo = "";
            sConsultaExec = "";

            if (TipoCodigo != "")
            {
                sFiltroTipoCodigo = string.Format("     and V.TipoCodigo = {0}", TipoCodigo);
            }            
           

            sSql = string.Format("Select 'Codigo' = CodigoDistribuidor, CodigoEAN, 'Tipo' = TipoDeCodigo, " +
                " Descripcion, 'Clave SSA' = ClaveSSA, DescripcionCortaClave,  " + 
                " 'IdProducto distribucion' = IdProductoDist " +
                " From vw_Productos_Distribuidor V (NoLock) " + 
                " Where V.IdDistribuidor = '{0}' {1} " +
                " Order by IdProductoDist ",
                Fg.PonCeros(IdDistribuidor, 4), sFiltroTipoCodigo );
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'DistribuidoresProductos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 4);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DistribuidorProductos(IdDistribuidor, strResultado, TipoCodigo, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DistribuidoresClientes(string IdDistribuidor, string TipoCodigo, string Funcion)
        {
            string strMsj = "Catálogo de Productos por Distribuidor";
            string sFiltroTipoCodigo = "";
            sConsultaExec = "";

            if (TipoCodigo != "")
            {
                sFiltroTipoCodigo = string.Format("     and V.TipoCliente = {0}", TipoCodigo);
            }


            sSql = string.Format("Select V.Estado, V.IdFarmacia, V.Farmacia, 'Nombre Destino' = V.NombreCliente, 'Clave' = V.ClaveCliente  " + 
                " " + 
                " From vw_Distribuidor_Clientes V (NoLock) " + 
                " Where V.IdDistribuidor = '{0}' {1} " +
                " Order by V.NombreCliente ", 
                Fg.PonCeros(IdDistribuidor, 4), sFiltroTipoCodigo);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'DistribuidoresProductos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 4);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DistribuidoresClientes(IdDistribuidor, strResultado, TipoCodigo, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet DistribuidoresProductos_ClaveSSA(string IdDistribuidor, string TipoCodigo, string Funcion)
        {
            string strMsj = "Catálogo de Productos para Pedidos por Clave SSA";

            sSql = string.Format("Select 'Codigo' = CodigoDistribuidor, 'Clave SSA' = ClaveSSA, 'Tipo' = TipoDeCodigo, " +
                " Descripcion, DescripcionCortaClave, 'IdProducto distribucion' = IdProductoDist, IdClaveSSA, IdProductoDist " +
                " From vw_Distribuidor_ClaveSSA V (NoLock) " +
                " Order by IdProductoDist ");

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Productos para Pedidos por Clave SSA'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 4);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.DistribuidorProductos_ClaveSSA(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        #endregion Pedidos_Distribuidores

        #region Clientes Secretaria
        public DataSet Regional_Usuarios(string Funcion, string IdEstado, string IdFarmacia)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Usuarios";

            sSql = string.Format(
                " Select Nombre, Login, 'Clave de usuario' = IdUsuario " +
                "From Net_Regional_Usuarios (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' " + 
                " Order by 1", IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Usuarios", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Regional_Usuarios(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Regional_Usuarios(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Usuarios";

            sSql = string.Format(" Select Nombre, 'Clave de usuario' = IdUsuario " +
                "From Net_Regional_Usuarios (NoLock) Where IdEstado = '{0}' Order by 1", IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Usuarios", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Regional_Usuarios(IdEstado, "", strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Clientes Secretaria

        #region SII-Recetario
        public DataSet Rec_Beneficiarios(string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Beneficiarios";

            sSql = "Select (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, 'Clave de Beneficiario' = IdBeneficiario " +
                   " From Rec_CatBeneficiarios (NoLock) " +
                   " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                   "Order by ApPaterno ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Beneficiarios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Recetario_Beneficiarios(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet REC_Medicos(string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Médicos";

            sSql = "Select (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as Nombre, 'Cedula Número' = NumCedula, 'Clave de Médico' = IdMedico " +
                   " From REC_CatMedicos (NoLock) " +
                   " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                   " Order by ApPaterno ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Medicos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Medicos(IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion SII-Recetario
        
        #region Modulo de Compras  
        #region Ordenes Compras Unidades 
        public DataSet ProductosOrdenCompra(string IdEmpresa, string IdEstado, string FolioOrdenCompra, string Funcion)
        {
            string strMsj = "Catálogo de Productos";

            sSql = string.Format(
                "Select \n " +
                "   P.CodigoEAN, P.Descripcion, TipodeProducto as Tipo, 'Clave SSA' = P.ClaveSSA, 'Descripción Clave' = P.DescripcionSal, P.Presentacion, P.Laboratorio, \n " +
                "   P.IdProducto as Codigo, 'Codigo EAN' = P.CodigoEAN \n " +
                "From vw_Productos_CodigoEAN P (Nolock) \n " +
                "Inner Join \n" + 
	            "( \n" + 
		        "    Select ClaveSSA, 0 as Relacionada   \n" + 
		        "    From vw_Productos_CodigoEAN X (NoLock)  \n" + 
		        "    Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det Y (NoLock) On ( X.IdProducto = Y.IdProducto And X.CodigoEAN = Y.CodigoEAN )  \n" +
                "    Where Y.IdEmpresa = '{0}' And Y.IdEstado = '{1}' And Y.FolioOrden = '{2}' \n" + 
                " \n" + 
		        "    Union \n" + 
                " \n" + 
		        "    Select ClaveSSA_Relacionada, 1 as Relacionada \n" + 
		        "    From vw_Relacion_ClavesSSA_Claves C (NoLock) \n" + 
		        "    Inner Join \n" + 
		        "    (\n" + 
			    "        Select '{1}' as IdEstado, X.ClaveSSA   \n" + 
			    "        From vw_Productos_CodigoEAN X (NoLock)  \n" + 
			    "        Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det Y (NoLock) On ( X.IdProducto = Y.IdProducto And X.CodigoEAN = Y.CodigoEAN )  \n" + 
			    "        Where Y.IdEmpresa = '{0}' And Y.IdEstado = '{1}' And Y.FolioOrden = '{2}' 	  \n" +
                "    ) R  On ( C.IdEstado = R.IdEstado and C.ClaveSSA = R.ClaveSSA and C.Status = 'A' )  \n" + 
	            ") C On ( C.ClaveSSA = P.ClaveSSA ) \n ",
            Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), Fg.PonCeros(FolioOrdenCompra, 8));

            //sSql = string.Format("Exec spp_ProductoOrdenesCompras  " +
            //       " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FolioOrden = '{3}', @IdCodigo = '{4}', @CodigoEAN = '{5}', @TipoDeProceso = '2' ",
            //       IdEmpresa, IdEstado, DtGeneral.FarmaciaConectada, FolioOrdenCompra, "", ""); 

            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase,true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ProductosOrdenCompra'", true, 2);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ProductosOrdenCompra(IdEmpresa, IdEstado, FolioOrdenCompra, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }
        #endregion Ordenes Compras Unidades 

        #region Ordenes Compras
        public DataSet CatMotivos(string Funcion)
        {
            string strMsj = "Catálogo de motivos de sobre Precio de Compra";

            sSql = string.Format(" Select Folio, Descripcion, Folio As Id From CatMotivosSobrePrecio ");

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'CatMotivos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CatMotivos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ClavesSSA_Productos_Proveedor(string IdProveedor, string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA de Proveedores";

            sSql = string.Format(" Select C.IdClaveSSA_Sal As IdClaveSSA, C.ClaveSSA_Base As ClaveSSA, " +
                                " C.DescripcionCortaClave As DescripcionCorta, C.Descripcion, C.ClaveSSA_Base As Clave_SSA " +
                                " From CatClavesSSA_Sales C (NoLock) " +
                                " Inner Join COM_OCEN_ListaDePrecios_Claves L (NoLock) " +
                                    " On( C.IdClaveSSA_Sal = L.IdClaveSSA ) " +
                                " Where L.IdProveedor = '{0}' ", IdProveedor);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos-Proveedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Productos_Proveedor(IdProveedor, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Productos_Proveedor(string IdProveedor, string Funcion)
        {
            return Productos_Proveedor(IdProveedor, 0, 0, Funcion); 
        }

        public DataSet Productos_Proveedor(string IdProveedor, int ValidarProductoEnPerfilOPeracion, int ValidarProductoEnPerfilComprador, string Funcion)
        {
            string strMsj = "Catálogo de Productos de Proveedores";

            sSql = string.Format(
                "Select \n" +
                "\tP.IdProducto, P.CodigoEAN, \n" +
                "\t'Producto para compra' = (case when dbo.fg_ValidarClaveEnPerfil_Operacion('{1}', '{2}', '{3}', P.IdProducto, P.CodigoEAN ) = 1 then 'SI' Else 'NO' end), \n" +
                "\tP.DescripcionCorta As Descripcion, \n" +
                "\t'Id ClaveSSA' = P.IdClaveSSA_Sal, 'Clave SSA' = P.ClaveSSA_Base, 'Precio incluye IVA' = PrecioUnitario, \n" + 
                "\t'Descripción Clave' = P.DescripcionSal, \n" +
                "\tP.CodigoEAN As Codigo_EAN \n" +
                "From vw_Productos_CodigoEAN P (NoLock) \n" +
                "Inner Join COM_OCEN_ListaDePrecios L (NoLock) On( P.CodigoEAN = L.CodigoEAN and L.Status = 'A' ) \n" +
                "Where L.IdProveedor = '{0}' and P.StatusCodigoRel = 'A' \n", 
                IdProveedor,
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, ValidarProductoEnPerfilOPeracion); 

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos-Proveedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, true, 6);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Productos_Proveedor(IdProveedor, strResultado, ValidarProductoEnPerfilOPeracion, ValidarProductoEnPerfilComprador, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Ordenes de Compras
        #endregion Modulo de Compras

        #region Posicionamiento de Productos 
        public DataSet Pasillos(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo Racks";
            sConsultaExec = "";

            sSql = string.Format("Select DescripcionPasillo as Pasillo, 'Clave de Pasillo' = IdPasillo From CatPasillos (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                " Order by DescripcionPasillo ", IdEmpresa, IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Racks'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Pasillos(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Estantes(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string Funcion)
        {
            string strMsj = "Catálogo Niveles";
            sConsultaExec = "";

            sSql = string.Format("Select DescripcionEstante as Estante, 'Clave de Estante' = IdEstante From CatPasillos_Estantes (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' " +
                " Order by DescripcionEstante ", IdEmpresa, IdEstado, IdFarmacia, IdPasillo);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Niveles'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Pasillos_Estantes(IdEmpresa, IdEstado, IdFarmacia, IdPasillo, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Entrepaños(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string IdEstante, string Funcion)
        {
            string strMsj = "Catálogo Posiciones";
            sConsultaExec = "";

            sSql = string.Format("Select DescripcionEntrepaño as Entrepaño, 'Clave de Entrepaño' = IdEntrepaño From CatPasillos_Estantes_Entrepaños (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}' " +
                " Order by DescripcionEntrepaño ", IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Posiciones'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Pasillos_Estantes_Entrepaños(IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Productos_Por_Ubicacion(string IdEmpresa, string IdEstado, string IdFarmacia, string IdPasillo, string IdEstante, string IdEntrepaño, string Funcion)
        {
            string strMsj = "Productos por posición";
            sConsultaExec = "";

            sSql = string.Format("Select ClaveSSA, DescripcionCortaClave, IdProducto, CodigoEAN, DescripcionProducto, CodigoEAN  " +
                " From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPasillo = '{3}' And IdEstante = '{4}' And IdEntrepaño = '{5}' " +
                " Order by DescripcionCortaClave, DescripcionProducto ", IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos_Ubicacion'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                // dtsClase = new DataSet();
                if (strResultado != "")
                {
                    // dtsClase = query.Pasillos_Estantes_Entrepaños(IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;


        }
        #endregion Posicionamiento de Productos 

        #region Farmacias Convenio Vales 
        public DataSet FarmaciasConvenio(string IdEstado, string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Farmacias Convenio";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = 
                string.Format("Select 'Farmacia Convenio' = Nombre, 'Num. Farmacia' = IdFarmaciaConvenio, 'Dirección' = Direccion " + 
                " From CatFarmacias_ConvenioVales (NoLock) " + 
                " Where IdEstado = '{0}' " + 
                " Order by Nombre ", IdEstado );
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Farmacias Convenio", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FarmaciasConvenio(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Farmacias Convenio Vales

        #region Remisiones Distribuidor
        public DataSet RemisionesDistEnc(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Remisiones de Distribuidor";

            sSql = "Select 'Fecha de Registro' = FechaRegistro, 'Folio' = FolioRemision " +
                   "From RemisionesDistEnc (NoLock) " +
                   "Where IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "And IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "Order by FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'RemisionesDistEnc'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.RemisionesDistEnc(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Distribuidor_Clientes(string IdEstado, string IdDistribuidor, string Funcion)
        {
            string strMsj = "Catálogo de Distribuidores Clientes ";
            sConsultaExec = "";

            sSql = string.Format( 
                "Select 'Nombre Cliente' = NombreCliente, 'Código de Cliente' = CodigoCliente, " + 
                " 'Núm. Unidad' = IdFarmacia, 'Unidad' = Farmacia, Status_Aux, " +
                " 'Clave de Cliente' = CodigoCliente " +
                " From vw_Distribuidores_Clientes (NoLock) " + 
                " Where IdEstado = '{0}' and IdDistribuidor = '{1}' " +
                " Order by CodigoCliente ", IdEstado, IdDistribuidor);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Distribuidores Clientes'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 1);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Distribuidor_Clientes(IdEstado, IdDistribuidor, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Remisiones Distribuidor 

        #region Cotizaciones licitaciones 
        public DataSet ClavesSSA_Sales_TipoClaves(string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catálogo de Claves SSA";
            sConsultaExec = "";


            sSql = " Select 'Codigo Interno' = C.IdClaveSSA_Sal, 'Clave SSA Base' = C.ClaveSSA_Base, 'Clave SSA' = C.ClaveSSA_Aux, C.Presentacion, " +
                " 'Descripción Clave' = C.DescripcionClave, " +
                " 'Grupo Terapeutico' = C.GrupoTerapeutico, " +
                " 'Tipo Catálogo' = C.TipoDeCatalogo, 'Tasa Iva' = T.PorcIva, C.IdClaveSSA_Sal as Codigo " +
                " From vw_ClavesSSA_Sales C (noLock) " +
                " Inner Join CatTiposDeProducto T (Nolock) On ( C.TipoDeClave = T.IdTipoProducto ) ";

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1) 
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_Sales'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Sales_TipoClaves(strResultado, false, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }

        public DataSet Costo_Compra_Licitaciones(string IdClaveSSA, string Funcion)
        {
            string strMsj = "Catálogo Costos de Compras";
            sConsultaExec = "";

            sSql = string.Format(" Select 'Clave SSA' = L.ClaveSSA, 'Costo Compra' = L.Importe, 'Producto' = P.IdProducto, " +
                    " 'Codigo EAN' = L.CodigoEAN, 'Descripción' = P.Descripcion, 'Núm. Proveedor' = L.IdProveedor, " +
                    " 'Proveedor' = L.Nombre, 'Núm. Laboratorio' = P.IdLaboratorio, 'Laboratorio' = P.Laboratorio, 'Id Clave SSA' = L.IdClaveSSA " +
                    " From vw_COM_OCEN_ListaDePreciosClaves L (Nolock) " +
                    " Inner Join vw_Productos_CodigoEAN P (Nolock) " +
                        " On ( L.ClaveSSA = P.ClaveSSA And L.CodigoEAN = P.CodigoEAN )  " +
                    " Where L.IdClaveSSA = '{0}'  " +
                    " Order By L.Importe ", IdClaveSSA);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Catálogo Costos de Compras'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 1);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Costo_Compra_Licitaciones(IdClaveSSA, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet ClavesSSA_Sales_PreciosCauses(string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Precios Causes";
            strMsj = "Catalogo de Claves SSA Precios Causes";
            sConsultaExec = "";


            sSql = " Select 'Clave SSA' = ClaveSSA, 'Descripción SSA' = Descripcion, 'Presentación' = Presentacion, " +
                " 'Tipo Clave' = TipoDeClaveDescripcion, 'Anio' = Año, ClaveSSA " +
                " From vw_CatClavesSSA_Causes (noLock) ";

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_PreciosCauses'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Sales_PreciosCauses(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            ////else
            ////{
            ////    MsjNoDatos(ref dtsClase, strMsj);
            ////}

            return dtsClase;
        }
        #endregion Cotizaciones licitaciones

        #region Fuentes de Financiamiento
        public DataSet FuentesDeFinanciamiento_Encabezado(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Fuentes de Financiamiento";

            sSql = "Select Estado, 'Cliente' = Cliente, 'Sub-Cliente' = SubCliente, 'Número de Contrato' = NumeroDeContrato, 'Id Fuente Financiamiento' = IdFuenteFinanciamiento " +
                "From vw_FACT_FuentesDeFinanciamiento (NoLock) Order by Estado ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de FuentesDeFinanciamiento_Encabezado", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FuentesDeFinanciamiento_Encabezado(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet FuentesDeFinanciamiento_Detalle(string Funcion, string sIdFuenteFinanciamiento)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Financiamientos";

            sSql = "Select Financiamiento, 'Id Financiamiento' = IdFinanciamiento " +
                "From vw_FACT_FuentesDeFinanciamiento_Detalle (NoLock) Where IdFuenteFinanciamiento = '" +
                sIdFuenteFinanciamiento + "' Order by IdFinanciamiento ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de FuentesDeFinanciamiento_Detalle", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FuentesDeFinanciamiento_Detalle(sIdFuenteFinanciamiento, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Fuentes de Financiamiento 

        #region ContraRecibos 
        public DataSet FolioFacturas(string Empresa, string Estado, string Farmacia, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Facturas";
            strMsj = "Catalogo de Folios de Facturas";
            sConsultaExec = ""; 

            sSql = " Select 'Folio Factura' = FolioFactura, 'Núm. Factura' = FolioFacturaElectronica, 'Tipo de Factura' = TipoDeFacturaDesc, " +
                " 'Importe' = Total, FolioFactura " +
                " From vw_FACT_Facturas_Remisiones (noLock) ";

            dtsClase = new DataSet();

            if (1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'FolioFacturas'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.FolioFacturas(Empresa, Estado, Farmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }
        #endregion ContraRecibos

        #region Facturacion Electronica 
        public DataSet CFDI_Clientes(string Funcion)
        {
            string strMsj = "Catálogo de Clientes";
            sConsultaExec = "";

            sSql = " Select 'Nombre del cliente'= Nombre, RFC, 'Ubicación' = (Pais + ', ' + Estado), 'Clave de cliente' = IdCliente " +
                " From FACT_CFD_Clientes (noLock) "; 

            dtsClase = new DataSet();

            if (1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'Clientes_CFDI'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Clientes(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }

        public DataSet CFDI_Establecimientos_Emisor( string IdEmpresa, string IdEstado, string IdFarmacia,
            string Funcion )
        {
            string strMsj = "Catálogo de Establecimientos";
            sConsultaExec = "";

            sSql =
                "Select 'Nombre del establecimiento' = NombreEstablecimiento, 'Clave' = IdEstablecimiento " +
                "From FACT_CFD_Establecimientos (noLock) ";

            dtsClase = new DataSet();

            if(1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'Establecimientos emisores'", false, 1);
                dtsClase = new DataSet();

                if(strResultado != "")
                {
                    dtsClase = query.CFDI_Establecimientos_Emisor(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }

        public DataSet CFDI_Clientes_Establecimientos( string IdEmpresa, string IdEstado, string IdFarmacia,
            string IdCliente, string Funcion )
        {
            string strMsj = "Catálogo de Establecimientos del Cliente";
            sConsultaExec = "";

            sSql = 
                "Select 'Nombre del establecimiento' = NombreEstablecimiento, 'Clave' = IdEstablecimiento " +
                "From FACT_CFD_Establecimientos_Receptor (noLock) ";

            dtsClase = new DataSet();

            if(1 == 1)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'Establecimientos receptores'", false, 1);
                dtsClase = new DataSet();

                if(strResultado != "")
                {
                    dtsClase = query.CFDI_Clientes_Establecimientos(IdEmpresa, IdEstado, IdFarmacia, IdCliente, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            return dtsClase;
        }

        public DataSet CFDI_Conceptos(string Funcion)
        {
            string strMsj = "Catálogo de Servicios";

            sSql = "Select Descripcion as Concepto, 'Clave de Concepto' = IdConcepto From FACT_CFD_Conceptos (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Conceptos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Conceptos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Conceptos_Especiales(string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Servicios";

            sSql = "Select Descripcion as Concepto, 'Clave de Concepto' = IdConcepto " + 
                " From FACT_CFD_ConceptosEspeciales (NoLock) " + 
                " Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Conceptos especiales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Conceptos_Especiales(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Facturacion Electronica

        #region Facturacion

        #region CuentasxPagar
        public DataSet Servicios_Facturacion(string Funcion)
        {
            string strMsj = "Catálogo de Servicios";

            sSql = "Select Descripcion as Servicio, 'Clave de Servicio' = IdServicio From FACT_CatServicios (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Servicios'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Servicios_Facturacion(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Acreedores_Facturacion(string Estado, string Funcion)
        {
            string strMsj = "Catálogo de Acreedores";

            sSql = "Select 'Núm. de Acreedor' = IdAcreedor, Nombre, RFC,  " +
                " 'Status' = Case When Status = 'A' Then 'ACTIVO' Else 'CANCELADO' End, 'Clave' = IdAcreedor " +
                " From FACT_CatAcreedores (NoLock) Where IdEstado = '" + Estado + "'  Order by Nombre ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Acreedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Acreedores_Facturacion(Estado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion CuentasxPagar

        #region Cheques
        public DataSet Bancos(string Funcion)
        {
            string strMsj = "Catálogo de Bancos";

            sSql = "Select B.IdBanco, Descripcion, B.IdBanco As Clave From CNT_CatBancos B  (NoLock)";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Bancos'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Bancos(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Chequera(string IdEmpresa, string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Chequera";

            sSql = string.Format("Select IdChequera, Descripcion, IdChequera As Clave From vw_Chequeras  (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}'", IdEmpresa, IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Chequera'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Chequera(IdEmpresa, IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet BeneficiarioChequera(string Funcion)
        {
            string strMsj = "Catálogo de Beneficiarios de Cheques";

            sSql = "Select *, IdBeneficiario As Clave From CNT_CatChequesBeneficiarios C (NoLock)";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'BeneficiarioChequera'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.BeneficiarioChequera(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Cheque(string IdEmpresa, string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de Cheque";

            sSql = string.Format("Select IdCheque, Cheque, IdCheque As Clave From vw_Cheque  (NoLock) Where IdEmpresa = '{0}' And IdEstado = '{1}'", IdEmpresa, IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Cheque'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Cheque(IdEmpresa, IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        #endregion Cheques
        #endregion Facturacion

        #region Perfiles_De_Atencion
        public DataSet PerfilesDeAtencion(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Perfiles de Atención";
            sConsultaExec = "";

            sSql = string.Format("Select 'Perfil Atención' = PerfilDeAtencion, 'Clave de Perfil' = IdPerfilAtencion, 'Núm. Cliente' = IdCliente, " +
                " NombreCliente, 'Núm. Sub-Cliente' = IdSubCliente, NombreSubCliente, 'Núm. Programa' = IdPrograma, Programa, 'Núm. Sub-Programa' = IdSubPrograma, " +
                " SubPrograma, Status, IdPerfilAtencion " +
                " From vw_CFGC_ALMN_CB_NivelesAtencion (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                " Order by IdPerfilAtencion ", IdEmpresa, IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Perfiles de Atención'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.PerfilesAtencion(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet PerfilesDeAtencionDistribuidor(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Perfiles de Atención";
            sConsultaExec = "";

            sSql = string.Format("Select 'Perfil Atención' = PerfilDeAtencion, 'Clave de Perfil' = IdPerfilAtencion,  " +
                " Status, IdPerfilAtencion " +
                " From vw_CFGC_ALMN_DIST_CB_NivelesAtencion (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                " Order by IdPerfilAtencion ", IdEmpresa, IdEstado, IdFarmacia);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Perfiles de Atención'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.PerfilesAtencionDistribuidor(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Perfiles_De_Atencion

        #region Titulos Validacion
        public DataSet Validacion_Titulos_EncabezadoReportes(string IdEstado, string Funcion)
        {
            string strMsj = "Catálogo de títulos de reportes de validación";
            sConsultaExec = "";

            sSql = string.Format("Select 'Titulo' = TituloEncabezadoReporte, 'Clave titulo' = IdTitulo " + 
                " From CFG_EX_Validacion_Titulos_Reportes (NoLock) " +
                " Where IdEstado = '{0}' " +
                " Order by IdTitulo ", IdEstado);
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Perfiles de Atención'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Validacion_Titulos_EncabezadoReportes(IdEstado, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Titulos Validacion

        #region Socios Comerciales
        public DataSet SociosComerciales(string Funcion)
        {
            string strMsj = "Catálogo de Socios Comerciales";
            sConsultaExec = "";

            sSql = string.Format("Select IdSocioComercial, Nombre, RFC, NombreComercial As 'Nombre Comercial', IdSocioComercial As Clave  From CatSociosComerciales Order By IdSocioComercial ");
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.SociosComerciales(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet SociosComerciales_Sucursales( string IdSocioComercial, string Funcion )
        {
            string strMsj = "Catálogo de Sucursales";
            sConsultaExec = "";

            sSql = string.Format("Select S.IdSucursal as Clave, S.NombreSucursal As 'Nombre Sucursal', S.Pais, " +
                "S.IdEstado, E.Nombre As Estado, S.IdMunicipio, M.Descripcion As Municipio, S.IdColonia, O.Descripcion As Colonia, S.Calle, S.NumeroExterior, " +
                "S.NumeroInterior, S.CodigoPostal, S.Referencia, S.Telefonos, S.Status, S.IdSucursal " +
                "From CatSociosComerciales_Sucursales S " +
                "Inner Join CatEstados E (NoLock) On (S.IdEstado = E.IdEstado) " +
                "Inner Join CatMunicipios M (NoLock) On (S.IdEstado = M.IdEstado And S.IdMunicipio = M.IdMunicipio) " +
                "Inner Join CatColonias O (NoLock) On (S.IdEstado = O.IdEstado And S.IdMunicipio = O.IdMunicipio And S.IdColonia = O.IdColonia) " +
                "Where S.IdSocioComercial = '{0}' ", Fg.PonCeros(IdSocioComercial, 8));
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Sucursales'", "");

            if(bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if(strResultado != "")
                {
                    dtsClase = query.SociosComerciales_Sucursales(IdSocioComercial, strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Socios Comerciales

        #region CFDI  
        public DataSet CFDI_Bancos(string Funcion)
        {
            string strMsj = "Catálogo de Bancos";
            sConsultaExec = "";

            sSql = sInicio + string.Format("Select *,RFC From FACT_CFDI__Bancos ");

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Bancos(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Pagos__EmisorBancos(string RFC_Emisor, string RFC_Banco, string Funcion)
        {
            string strMsj = "Catálogo de Socios Comerciales";
            sConsultaExec = "";

            string sRFC = "", sNumero = "";

            sSql = sInicio + string.Format(
                    "Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    "   (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status, " +
                    "   RFC_Banco + '-$-' + NumeroDeCuenta" +
                    " From vw_FACT_BancosCuentas_Emisor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and RFC_Emisor = '{3}' And RFC_Banco = '{4}' " +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Emisor, RFC_Banco);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                if (strResultado.Length > 0)
                {
                    sRFC = strResultado.Substring(0, strResultado.IndexOf("-$-"));
                    sNumero = strResultado.Substring(strResultado.IndexOf("-$-") + 3);
                }

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Pagos__EmisorBancos(RFC_Emisor, sRFC, sNumero, true, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet CFDI_Pagos__ReceptorBancos(string RFC_Banco, string Funcion)
        {
            string strMsj = "Catálogo de Socios Comerciales";
            sConsultaExec = "";

            string sRFC = "", sNumero = "";

            sSql = sInicio + string.Format(
                    "Select RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, " +
                    "   (RFC_Banco + ' -- ' + NombreRazonSocial) as NombreBanco, (NombreCorto + ' -- ' + NumeroDeCuenta) as CuentaBanco, Status, " +
                    "   RFC_Banco + '-$-' + NumeroDeCuenta" +
                    " From vw_FACT_BancosCuentas_Receptor (NoLock) " +
                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And RFC_Banco = '{3}'" +
                    " Group by RFC_Banco, NumeroDeCuenta, NombreCorto, NombreRazonSocial, Status " +
                    " Order by NombreRazonSocial, NumeroDeCuenta ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, RFC_Banco);

            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Socios Comerciales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);

                sRFC = strResultado.Substring(0, strResultado.IndexOf("-$-"));
                sNumero = strResultado.Substring(strResultado.IndexOf("-$-") + 3);

                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.CFDI_Pagos__ReceptorBancos(sRFC, sNumero, true, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion CFDI  

        #endregion Funciones y procedimientos publicos

        #region Funciones y procedimientos privados
        private void MsjNoDatos(ref DataSet dts, string strMsj)
        {
            if (bEjecuto)
            {
                dts = new DataSet("Vacio");
                MessageBox.Show(strMsjNoDatos, strMsj, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private string MostrarForma(string strMsj, DataSet dts)
        {
            return MostrarForma(strMsj, dts, false, 1);
        }

        private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal)
        {
            return MostrarForma(strMsj, dts, AccesarLocal, 1);
        }

        /// <summary>
        /// Muestra la pantalla de Ayudas, diseño para manejo de grandes catalgos.
        /// </summary>
        /// <param name="Titulo">Titulo de la Consulta</param>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <param name="AccesarLocal">Determina si se accesan los datos del servidor</param>
        /// <param name="ColInicialCombo">Columna inicial de busqueda</param>
        /// <returns></returns>
        private string MostrarForma(string Titulo, string Consulta, string MsjError, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda(ConexionSql);
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.CargarAyuda(Consulta, MsjError, ColInicialCombo); 
            Fg.CentrarForma(Frm_Ayuda);
            
            Frm_Ayuda.MostrarPantalla(); 

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        private string MostrarForma(string Titulo, DataSet dts, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            Frm_Ayuda = new FrmAyuda();
            Frm_Ayuda.Text = Titulo;
            Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
            Frm_Ayuda.dtsAyuda = dts;
            Frm_Ayuda.pfConfiguraListView(ColInicialCombo);
            Fg.CentrarForma(Frm_Ayuda);
            Frm_Ayuda.ShowDialog();

            string sRegresa = Frm_Ayuda.strResultado;

            return sRegresa;
        }

        ////private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal, int ColInicialCombo, 
        ////    bool ConsutalDinamica, string OrigenDeDatos, string Ordenamiento)
        ////{
        ////    Frm_Ayuda = new FrmAyuda();

        ////    Frm_Ayuda.Conexion = this.ConexionSql; 
        ////    Frm_Ayuda.ConsultaDinamica = ConsutalDinamica;
        ////    Frm_Ayuda.OrigenDeDatos = OrigenDeDatos;
        ////    Frm_Ayuda.Ordenamiento = Ordenamiento;

        ////    Frm_Ayuda.Text = strMsj;
        ////    Frm_Ayuda.bAccesarA_BD_Local = AccesarLocal;
        ////    Frm_Ayuda.dtsAyuda = dts;
        ////    Frm_Ayuda.pfConfiguraListView(ColInicialCombo);

        ////    Fg.CentrarForma(Frm_Ayuda);
        ////    Frm_Ayuda.ShowDialog();
        ////    string sRegresa = Frm_Ayuda.strResultado;

        ////    return sRegresa;
        ////} 

        private void ValidaDatos(ref DataSet dtsValidar)
        {
            if (error.ExistenErrores(dtsValidar))
            {
                // Buscar en el dataset la tabla de errores                    
                myResult = error.MostrarVentanaError(true, false, dtsValidar);
                dtsValidar = new DataSet("Vacio");
            }

            bExistenDatos = ExistenDatosEnDataset(dtsValidar);
        }

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            sConsultaExec = prtQuery;

            bEjecuto = false;
            bExistenDatos = false;
            if (!Leer.Exec( " Set DateFormat YMD " + prtQuery))
            {
                General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!Leer.Leer())
                {
                    //if (bMostrarMsjLeerVacio)
                    //    General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    bExistenDatos = true;
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
            bExistenDatos = false;

            sConsultaExec = prtQuery;

            try
            {
                if (bUsarCnnRedLocal)
                {
                    objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
                }
                else
                {
                    // objRetorno = (object)cnnWebServ.ObtenerDataset(strCnnString, prtQuery, prtTabla);
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

        #region Devoluciones_A_Proveedor
        public DataSet Folios_Devoluciones_A_Proveedor(string IdEmpresa, string IdEstado, string IdFarmacia, string Funcion)
        {
            string strMsj = "Catálogo de Folios de Devoluciones a Proveedor";

            sSql = "Select 'Fecha de Registro' = FechaRegistro, 'Clave de Folio' = FolioDevProv " +
                   "From DevolucionesProveedorEnc (NoLock) " +
                   "Where IdEstado  = '" + Fg.PonCeros(IdEstado, 2) + "' " +
                   "And IdFarmacia  = '" + Fg.PonCeros(IdFarmacia, 4) + "' " +
                   "Order by FechaRegistro ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Folio Devoluciones a Proveedor'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Folio_DevolucionesProveedorEnc(IdEmpresa, IdEstado, IdFarmacia, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Devoluciones_A_Proveedor

        #region Motivos_Devolucion_Transferencia
        public DataSet Motivos_Dev_Transferencia(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Motivos de Devoluciones para Transferencia";

            sSql = "Select Descripcion as Motivo, 'Id de Motivo' = IdMotivo From CatMotivos_Dev_Transferencia (NoLock) Order by Descripcion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Motivos", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Motivos_Dev_Transferencia(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Motivos_Devolucion_Transferencia

        #region Farmacias_Proveedores_Vales
        public DataSet Proveedores_Vales(string IdEstado, string IdFarmacia, string Funcion)
        {
            return Proveedores_Vales(IdEstado, IdFarmacia, true, Funcion); 
        }

        public DataSet Proveedores_Vales(string IdEstado, string IdFarmacia, bool HabilitarProveedorDeReembolso, string Funcion)
        {
            string strMsj = "Catálogo de Proveedores--Vales";
            string sFiltroProveedor = "";

            if (!HabilitarProveedorDeReembolso)
            {
                sFiltroProveedor += string.Format(" and P.IdProveedor >= '0000' ");
            }

            sSql = string.Format("Select 'Nombre Comercial' = P.AliasNombre, P.Nombre as Proveedor, P.RFC, P.StatusAux as Status,  'Clave de Proveedor' = P.IdProveedor " +
                " From vw_Proveedores P (NoLock) " +
                " Inner Join CatFarmacias_ProveedoresVales F (NOLOCK) On ( P.IdProveedor = F.IdProveedor ) " +
	            " Where F.IdEstado = '{0}' and F.IdFarmacia = '{1}' {2} " +
                " Order by P.Nombre ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), sFiltroProveedor );
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Proveedores--Vales'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Proveedores_Vales(Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), strResultado, HabilitarProveedorDeReembolso, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Farmacias_Proveedores_Vales

        #region Ubicaciones_Estandar
        public DataSet Ubicaciones_Estandar(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catálogo de Posiciones Estandar para Almacenes";

            sSql = "Select 'Nombre Posición Estandar' = NombrePosicion, Descripcion, NombrePosicion From Cat_ALMN_Ubicaciones_Estandar (NoLock) Order by NombrePosicion ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Posiciones Estandar", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.UbicacionesEstandar(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Ubicaciones_Estandar
    }
}
