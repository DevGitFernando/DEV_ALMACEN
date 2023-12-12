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

namespace Dll_SII_INadro
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
            string strMsj = "Catalogo de Empresas";

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
                    ////dtsClase = query.Empresas(strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }
            else
            {
                MsjNoDatos(ref dtsClase, strMsj);
            }

            return dtsClase;
        }

        public DataSet Clientes(string Funcion)
        {
            string strMsj = "Catalogo de Clientes";

            sSql = "Select  'Núm. Cliente' = IdCliente, 'Codigo Cliente' = CodigoCliente, 'Nombre Cliente' = Nombre, IdCliente From INT_ND_Clientes (NoLock) Order by IdCliente ";
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Clientes'", "");

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

        public DataSet Proveedores(string Funcion)
        {
            string strMsj = "Catálogo de Proveedores";           

            sConsultaExec = "";
            
            sSql = string.Format("Select 'Nombre Comercial' = AliasNombre, Nombre as Proveedor, RFC, StatusAux as Status,  'Clave de Proveedor' = IdProveedor " +
                " From vw_Proveedores (NoLock) " +                
                " Order by Nombre ");
            dtsClase = new DataSet();
            dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error la ayuda 'Proveedores'", "");

            if (bExistenDatos)
            {
                bExistenDatos = false;
                strResultado = MostrarForma(strMsj, dtsClase, false, 2);
                dtsClase = new DataSet();
                if (strResultado != "")
                {
                    dtsClase = query.Proveedores(strResultado, Funcion);
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

        public DataSet ClavesSSA(string Funcion)
        {
            return ClavesSSA(2, false, Funcion);
        }

        public DataSet ClavesSSA(int ClavesDeControlados, bool BuscarPorClaveSSA, string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catálogo de Claves SSA";
            sConsultaExec = "";
            string sFiltroControlados = "";
            string sFiltroClaves = "";

            if (ClavesDeControlados != 2)
            {
                sFiltroControlados = string.Format("Where EsControlado = '{0}' ", ClavesDeControlados);
            }

            if (BuscarPorClaveSSA)
            {
                sFiltroClaves = "  , ClaveSSA";
            }

            sSql = string.Format(" Select 'Codigo Interno' = IdClaveSSA_Sal, 'Clave SSA Base' = C.ClaveSSA_Base, 'Clave SSA' = C.ClaveSSA_Aux, Presentacion, " +
                " 'Descripción Clave' = DescripcionClave, " +
                " 'Grupo Terapeutico' = GrupoTerapeutico, " +
                " 'Tipo Catálogo' = TipoDeCatalogo, IdClaveSSA_Sal as Codigo {1} " +
                " From vw_ClavesSSA_Sales C (noLock) " +
                " {0} ", sFiltroControlados, sFiltroClaves);

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA(strResultado, BuscarPorClaveSSA, ClavesDeControlados, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }

            return dtsClase;
        }

        public DataSet ClavesSSA_ND(string Funcion)
        {
            string strMsj = "Catálogo de Claves SSA Sales";
            strMsj = "Catálogo de Claves SSA";
            sConsultaExec = "";
            string sFiltroControlados = "";
            string sFiltroClaves = "";

            sSql = string.Format(" Select 'Descripción Clave' = Descripcion, " +
                " 'Clave SSA' = ClaveSSA_ND " +
                " From INT_ND_Claves C (noLock) "); 

            dtsClase = new DataSet();
            //dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'ClavesSSA_Sales'", "");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                //strResultado = MostrarForma(strMsj, dtsClase, true, 3);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'ClavesSSA_ND'", true, 5);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ClavesSSA_ND(strResultado, Funcion);
                    sConsultaExec = query.QueryEjecutado;
                    ValidaDatos(ref dtsClase);
                }
            }

            return dtsClase;
        }
        #endregion Catalogos 

        #region Pedidos de Unidades
        public DataSet ProductosPedido(string IdEmpresa, string IdEstado, string ReferenciaPedido, string Funcion)
        {
            string strMsj = "Catálogo de Productos";


            sSql = string.Format(
                " Select P.CodigoEAN, P.Descripcion, TipodeProducto as Tipo, " +
                " 'Clave SSA' = P.ClaveSSA, 'Descripción Clave' = P.DescripcionSal, " +
                "  P.Presentacion, P.Laboratorio, P.IdProducto as Codigo, 'Codigo EAN' = P.CodigoEAN \n" +
                " From vw_Productos_CodigoEAN P (Nolock) \n" +
                " Inner Join \n" +
                " ( \n" + 
                " \tSelect x.ClaveSSA \n " +
                " \tFrom CatProductos_CodigosRelacionados C (NoLock) \n" +
                " \tInner Join CatProductos P (NoLock) On ( C.IdProducto = P.IdProducto ) \n" +
                " \tInner Join CatClavesSSA_Sales X (NoLock) On ( P.IdClaveSSA_Sal = X.IdClaveSSA_Sal ) \n" + 
                " \tInner Join INT_ND_Pedidos_Enviados_Det Y(NoLock) On ( C.CodigoEAN = Y.CodigoEAN ) " +
                " \tWhere Y.IdEmpresa = '{0}' And Y.IdEstado = '{1}' And Y.ReferenciaPedido = '{2}' \n" +
                " ) A  \n" + 
                " On ( P.ClaveSSA = A.ClaveSSA) ",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), ReferenciaPedido);

            dtsClase = new DataSet();
            // dtsClase = (DataSet)EjecutarQuery(Funcion, sSql, "Error en la ayuda 'Productos'", "No se encontraron Productos asociados al Estado.");

            //if (bExistenDatos)
            if (1 == 1)
            {
                bExistenDatos = false;
                // strResultado = MostrarForma(strMsj, dtsClase,true, 2);
                strResultado = MostrarForma(strMsj, sSql, "Error en la ayuda 'Productos de Pedido'", true, 2);
                dtsClase = new DataSet();

                if (strResultado != "")
                {
                    dtsClase = query.ND_ProductosPedido(IdEmpresa, IdEstado, ReferenciaPedido, strResultado, Funcion);
                    ValidaDatos(ref dtsClase);
                }
            }

            return dtsClase;
        }
        #endregion Pedidos de Unidades 
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

    }
}
