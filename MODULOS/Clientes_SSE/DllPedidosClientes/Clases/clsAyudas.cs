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

namespace DllPedidosClientes
{
    public class clsAyudas
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
        private string sSql = "", strResultado = "";  // , sOrderBy = ""; 
        private string strMsjNoDatos = "No existe información para mostrar.";
        string sInicio = "Set DateFormat YMD ";

        private clsConsultas query;
        private basGenerales Fg = new basGenerales();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private FrmAyuda Frm_Ayuda;

        bool bConexionWeb = false;
        string sUrl = "";
        string sArchivoIni = "";
        clsLeerWeb leerWeb; 

        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;        
        private string Name = "DllFarmaciaSoft.clsAyudas";
        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = false;
        bool bEsPublicoGeneral = false;
        // int iColumnaInicial = 1; 

        protected string sConsultaExec = "";
        #endregion

        #region Constructores de clase y destructor
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

        public clsAyudas(string Url, string ArchivoIni, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;
            this.bConexionWeb = true;
            this.sUrl = Url;
            this.sArchivoIni = ArchivoIni;

            clsDatosCliente DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, "clsAyudas", "");
            query = new clsConsultas(Url, ArchivoIni, DatosApp, Pantalla, MostrarMsjLeerVacio); 
            leerWeb = new clsLeerWeb(sUrl, sArchivoIni, DatosCliente); 
        }

        #endregion

        #region Funciones y procedimientos publicos
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

        #region Catalogos 
        public DataSet Clientes(string Funcion)
        {
            string strMsj = "Catalogo de Clientes";

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
            string strMsj = "Catalogo de SubClientes";

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
                    dtsClase = query.SubClientes(strResultado, Funcion);
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
            string strMsj = "Catalogo de SubClientes";

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

        public DataSet Programas(string Funcion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catalogo de Programas";

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
            string strMsj = "Catalogo de SubProgramas";

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

        public DataSet Farmacias(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catalogo de Farmacias";

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

        public DataSet ClavesSSA_Sales(string Funcion)
        {
            string strMsj = "Catalogo de Claves SSA Sales";
            strMsj = "Catalogo de Claves SSA";

            ////sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA' =  C.ClaveSSA, C.Descripcion, 'Grupo Terapeutico' = G.Descripcion, " + 
            ////       " 'Tipo Catálogo' = T.Descripcion, IdClaveSSA_Sal as Codigo " + 
            ////       " From CatClavesSSA_Sales C (noLock) " +
            ////       " Inner Join CatGruposTerapeuticos G (NoLock) On ( C.IdGrupoTerapeutico = G.IdGrupoTerapeutico ) " +
            ////       " Inner Join CatTipoCatalogoClaves T (NoLock) On ( C.TipoCatalogo = T.TipoCatalogo ) " +
            ////       ""; // " Order by C.Descripcion "; 


            // 2K110921.1311  
            sSql = " Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA' = C.ClaveSSA_Base, 'Clave SSA Aux' = C.ClaveSSA, Presentacion, " +
                " 'Descripción Clave' = DescripcionClave, " +
                " 'Grupo Terapeutico' = GrupoTerapeutico, " +
                " 'Tipo Catálogo' = TipoDeCatalogo, IdClaveSSA_Sal as Codigo " +
                " From vw_ClavesSSA_Sales C (noLock) ";

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                // strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_Sales'", true, 5);
                strResultado = MostrarForma(strMsj, dtsClase);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_Sales(strResultado, Funcion);
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
            string strMsj = "Catalogo de Productos";
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

        #endregion Catalogos 

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
                sWhere = string.Format(" and IdCliente = '{0}' ", Fg.PonCeros(IdCliente, 4));
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
            bool bEsCliente = true;

            if (IdPrograma != "")
            {
                sWhere = string.Format(" and IdPrograma = '{0}' ", Fg.PonCeros(IdPrograma, 4));
                bEsCliente = false;
            }

            if (!EsPublicoGral)
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral);

            if (bEsCliente)
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Programa' = Programa, 'Clave de Programa' = IdPrograma " +
                     " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                     " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdSubCliente = '{3}' and StatusRelacion = 'A' {4} ",
                     IdEstado, IdFarmacia, IdCliente, IdSubCliente, sWherePubGral);
            }
            else
            {
                sSql = sInicio + string.Format(" Select distinct 'Nombre Sub-Programa' = SubPrograma, 'Clave de Sub-Programa' = IdSubPrograma " +
                     " From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                     " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdSubCliente = '{3}' and StatusRelacion = 'A' {4} {5} ",
                     IdEstado, IdFarmacia, IdCliente, IdSubCliente, sWherePubGral, sWhere);
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
                        dtsClase = query.Farmacia_Clientes_Programas(IdPublicoGeneral, IdEstado, IdFarmacia, IdCliente, IdSubCliente, strResultado, "", Funcion);
                    else
                        dtsClase = query.Farmacia_Clientes_Programas(IdPublicoGeneral, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, strResultado, Funcion);

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

        #region Farmacias_Urls_Activas
        public DataSet Farmacias_UrlsActivas(string Funcion, string IdEstado)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catalogo de Farmacias";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = " Select 'Clave de Farmacia' = F.IdFarmacia, F.Farmacia, " + 
                    " F.Jurisdiccion, 'Id Farmacia' = F.IdFarmacia " +
                    " From vw_Farmacias F (NoLock) " + 
                    " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                    " Where F.IdEstado = '" + IdEstado + "'  " +
                    " Order By F.IdJurisdiccion, F.IdFarmacia ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Farmacias", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Farmacias_UrlsActivas(IdEstado, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet FarmaciasJurisdiccion_UrlsActivas(string Funcion, string IdEstado, string Jurisdiccion)
        {
            //DataSet dtsClase = new DataSet();
            string strMsj = "Catalogo de Farmacias";

            IdEstado = Fg.PonCeros(IdEstado, 2);
            sSql = " Select 'Clave de Farmacia' = F.IdFarmacia, F.Farmacia, " +
                    " F.Jurisdiccion, 'Id Farmacia' = F.IdFarmacia " +
                    " From vw_Farmacias F (NoLock) " +
                    " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                    " Where F.IdEstado = '" + IdEstado + "'  " + " and F.IdJurisdiccion = '" + Jurisdiccion + "' " +
                    " Order By F.IdJurisdiccion, F.IdFarmacia ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda de Farmacias", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.FarmaciasJurisdiccion_UrlsActivas(IdEstado, Jurisdiccion, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }
        #endregion Farmacias_Urls_Activas
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

        private string MostrarForma(string strMsj, DataSet dts, bool AccesarLocal, int ColInicialCombo)
        {
            //// return MostrarForma(strMsj, dts, AccesarLocal, ColInicialCombo, false, "", ""); 
            // sUrl, sArchivoIni
            // Frm_Ayuda = new FrmAyuda(General.Url, General.ArchivoIni);
            Frm_Ayuda = new FrmAyuda(sUrl, sArchivoIni); 
            Frm_Ayuda.Text = strMsj;
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
            DataSet dtsRetorno = new DataSet();

            if (bConexionWeb)
            {
                dtsRetorno = EjecutarQueryWeb(Funcion, prtQuery, MensajeError, MensajeNoEncontrado);
            }
            else
            {
                dtsRetorno = EjecutarQueryDirecto(Funcion, prtQuery, MensajeError, MensajeNoEncontrado);
            }

            return dtsRetorno;
        }

        private DataSet EjecutarQueryDirecto(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

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

        private DataSet EjecutarQueryWeb(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            bExistenDatos = false;
            if (!leerWeb.Exec(" Set DateFormat YMD " + prtQuery))
            {
                // General.Error.GrabarError(leerWeb.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!leerWeb.Leer())
                {
                    //if (bMostrarMsjLeerVacio)
                    //    General.msjUser(MensajeNoEncontrado);
                }
                else
                {
                    bExistenDatos = true;
                    dtsResultados = leerWeb.DataSetClase;
                }
            }

            return dtsResultados;
        }

        ////private object EjecutarQuery(string prtQuery, string prtTabla)
        ////{
        ////    object objRetorno = null;
        ////    DataSet dtsRetorno = new DataSet("Vacio");
        ////    Datos.CadenaDeConexion = strCnnString;
        ////    bExistenDatos = false;


        ////    try
        ////    {
        ////        if (bUsarCnnRedLocal)
        ////        {
        ////            objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
        ////        }
        ////        else
        ////        {
        ////            // objRetorno = (object)cnnWebServ.ObtenerDataset(strCnnString, prtQuery, prtTabla);
        ////        }

        ////        dtsRetorno = (DataSet)objRetorno;
        ////        if (error.ExistenErrores(dtsRetorno))
        ////        {
        ////            // Buscar en el dataset la tabla de errores                    
        ////            myResult = error.MostrarVentanaError(true, false, dtsRetorno);
        ////            dtsRetorno = new DataSet("Vacio");
        ////            objRetorno = (object)dtsRetorno;
        ////        }

        ////        bExistenDatos = ExistenDatosEnDataset(dtsRetorno);

        ////    }
        ////    catch (Exception e)
        ////    {
        ////        e = (Exception)objRetorno;
        ////        dtsRetorno = new DataSet("Vacio");
        ////        objRetorno = (object)dtsRetorno;

        ////        errorLog = new clsLogError(e);
        ////        error = new clsErrorManager(errorLog.ListaErrores);
        ////        myResult = error.MostrarVentanaError(true, false, errorLog.ListaErrores);
        ////    }

        ////    return objRetorno;
        ////}

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
