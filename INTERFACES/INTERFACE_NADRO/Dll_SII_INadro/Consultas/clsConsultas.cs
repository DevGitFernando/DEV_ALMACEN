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

using DllFarmaciaSoft; 

namespace Dll_SII_INadro 
{
    public class clsConsultas
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
        string sInicio = "Set DateFormat YMD ";

        string sNameDll = "";
        string sPantalla = "";
        string sVersion = "";
        bool bMostrarMsjLeerVacio = true;
        bool bEsPublicoGeneral = false;
        ////bool bEsClienteIMach = false; //IMach4.EsClienteIMach4; 

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

        ////    bEsClienteIMach = IMach4.EsClienteIMach4; 
        ////}

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

            ////bEsClienteIMach = IMach4.EsClienteIMach4; 
        }

        public clsConsultas(clsDatosConexion Conexion, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

            ////bEsClienteIMach = IMach4.EsClienteIMach4; 
        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version)
        {
            //General.msjAviso(Conexion.CadenaConexion); 
            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

            ////bEsClienteIMach = IMach4.EsClienteIMach4; 
        }

        public clsConsultas(clsDatosConexion Conexion, string Modulo, string Pantalla, string Version, bool MostrarMsjLeerVacio)
        {
            //General.msjAviso(Conexion.CadenaConexion); 

            this.sNameDll = Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;

            DatosConexion = Conexion;
            ConexionSql = new clsConexionSQL(Conexion);
            //ConexionSql.SetConnectionString();

            error = new clsErrorManager();
            errorLog = new clsLogError();
            Fg = new basGenerales();
            Cryp = new clsCriptografo();

            ////bEsClienteIMach = IMach4.EsClienteIMach4; 
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
            string sMsjError = "Ocurrio un error al obtener los datos de Empresas";
            string sMsjNoEncontrado = "No se encontraron Empresas registradas, verifique.";

            sQuery = sInicio + " Select * From CatEmpresas (NoLock) ";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Clientes(string Funcion)
        {
            return Clientes("", Funcion);
        }

        public DataSet Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos de Clientes";
            string sMsjNoEncontrado = "No se encontraron Clientes registrados, verifique.";
            string sWhere = "";

            if (IdCliente.Trim() != "")
            {
                sWhere = string.Format(" Where IdCliente = '{0}' ", Fg.PonCeros(IdCliente, 4));
            }

            sQuery = sInicio + string.Format(" Select * From INT_ND_Clientes (NoLock) {0} ", sWhere);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Proveedores(string IdProveedor, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Proveedor";
            string sMsjNoEncontrado = "Clave de Proveedor no encontrado, verifique.";                        

            sQuery = sInicio + string.Format(
                " Select * From vw_Proveedores (NoLock) " +
                " Where IdProveedor = '{0}' ",
                Fg.PonCeros(IdProveedor, 4));
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

        public DataSet ClavesSSA(string ClaveSSA, string Funcion)
        {
            return ClavesSSA(ClaveSSA, true, Funcion);
        }

        public DataSet ClavesSSA(string ClaveSSA, bool BuscarPorClaveSSA, string Funcion)
        {
            return ClavesSSA(ClaveSSA, BuscarPorClaveSSA, 2, Funcion);
        }

        public DataSet ClavesSSA(string ClaveSSA, bool BuscarPorClaveSSA, int ClavesDeControlados, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = ""; 
            string sFiltro = "";
            string sFiltroControlados = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            sFiltro = string.Format(" Where IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(ClaveSSA, 4));
            if (BuscarPorClaveSSA)
            {
                sFiltro = string.Format(" Where  ClaveSSA = '{0}' ", ClaveSSA);
            }

            if (ClavesDeControlados != 2)
            {
                sFiltroControlados = string.Format("  and EsControlado = '{0}' ", ClavesDeControlados);
                sFiltro += sFiltroControlados;
            }


            ////sQuery = sInicio + string.Format(" Select * " + 
            ////    " From CatClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

            sQuery = sInicio + string.Format(" Select *, DescripcionClave as Descripcion " +
                " From vw_ClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_ND(string ClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = ""; 
            string sFiltro = "";
            string sFiltroControlados = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave no encontrada, verifique.";


            sFiltro = string.Format(" Where  ClaveSSA_ND = '{0}' ", ClaveSSA);

            sQuery = sInicio + string.Format(" Select *, Descripcion as DescripcionClave " +
                " From INT_ND_Claves (NoLock)   {0}   ", sFiltro);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Consultas Catalogos
        #endregion Modulos SII

        #region Pedidos de Unidades 
        public DataSet ND_Pedidos_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_INT_ND_PedidosEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ND_Pedidos_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos de los Detalles del Folio";
            string sMsjNoEncontrado = "Detalles del Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(
                " Select CodigoEAN, IdProducto, DescProducto, TasaIva, CantidadPrometida, CantidadPrometidaPiezas, CantidadRecibida, Costo, SubTotal, ImpteIva, Importe " +
                " From vw_INT_ND_PedidosDet (NoLock) " +
                " Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Folio = '{3}' ",
                IdEmpresa, IdEstado, IdFarmacia, Folio);

            bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ND_Pedidos_Det_Lotes(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener la información de compras.";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select IdSubFarmacia, SubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                " datediff(mm, getdate(), FechaCad) as MesesCad, " +
                " FechaReg, FechaCad, Status, Existencia, CantidadRecibida as Cantidad " +
                " From vw_INT_ND_PedidosDet_Lotes (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia =  '{2}' and Folio = '{3}'  ", IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 

            return myDataset;
        }

        public DataSet ND_Pedidos_Det_Lotes__Ubicaciones(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener la información de ventas.";
            string sMsjNoEncontrado = "Folio de Orden de Compra no encontrado, verifique.";

            sQuery = sInicio + string.Format("	" +
                   " Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, " +
                   "    IdPasillo, IdEstante, IdEntrepaño as IdEntrepano, " +
                   "    Status, cast(Existencia as Int) as Existencia, cast(Cantidad as int) as Cantidad " +
                   " From vw_INT_ND_PedidosDet_Lotes_Ubicaciones  (NoLock) " +
                   " Where IdEmpresa = '{0}' and IdEstado =  '{1}' and IdFarmacia = '{2}' and Folio = '{3}' " +
                   " Order by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ",
                   IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), FolioPedido);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado); // Order By KeyxDetalleLote 

            return myDataset;
        }

        public DataSet ND_Pedidos_Ingresados(string IdEmpresa, string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_INT_ND_PedidosEnc (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and ReferenciaFolioPedido = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), Folio.Trim() );

            bMostrarMsjLeerVacio = bMostrarMsjLeerVacio;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado );

            return myDataset; 
        }

        public DataSet ND_ProductosPedido(string IdEmpresa, string IdEstado, string ReferenciaPedido, string IdCodigo, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Producto";
            string sMsjNoEncontrado = "Clave de Producto no encontrada ó No Pertenece a las claves de del Pedido, verifique.";


            sQuery = sInicio + string.Format(
                " Select P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.IdProducto, P.CodigoEAN, P.Descripcion, " +
                " P.TasaIva, dbo.fg_ObtenerCostoPromedio('{0}', '{1}', '{5}', P.IdProducto) as CostoPromedio " +
                " From vw_Productos_CodigoEAN P (Nolock) \n" +
                " Inner Join \n" +
                " ( \n" +
                " \tSelect x.ClaveSSA \n " +
                " \tFrom CatProductos_CodigosRelacionados C (NoLock) \n" +
                " \tInner Join CatProductos P (NoLock) On ( C.IdProducto = P.IdProducto ) \n" +
                " \tInner Join CatClavesSSA_Sales X (NoLock) On ( P.IdClaveSSA_Sal = X.IdClaveSSA_Sal ) \n" +
                " \tInner Join INT_ND_Pedidos_Enviados_Det Y(NoLock) On ( C.CodigoEAN = Y.CodigoEAN ) " +
                " \tWhere Y.IdEmpresa = '{0}' And Y.IdEstado = '{1}' And Y.ReferenciaPedido = '{2}' \n" +
                " ) A On ( P.ClaveSSA = A.ClaveSSA) \n" +
                " Where ( P.CodigoEAN = '{3}' OR P.IdProducto = '{4}') \n",
                Fg.PonCeros(IdEmpresa, 3), Fg.PonCeros(IdEstado, 2), ReferenciaPedido,
                IdCodigo.Trim(), Fg.PonCeros(IdCodigo, 8), DtGeneral.FarmaciaConectada);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Pedidos de Unidades

        #region Validar Pedidos 
        public DataSet ND_Validar_Pedidos_Integrados(string IdEmpresa, string IdEstado, string IdFarmacia, string ReferenciaFolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select Top 1 * " + 
                " From vw_INT_ND_PedidosUnidades (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and ReferenciaFolioPedido = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), ReferenciaFolioPedido.Trim());

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ND_Validar_Pedidos_Integrados_Detalles(string IdEmpresa, string IdEstado, string IdFarmacia, string ReferenciaFolioPedido, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos del Encabezado del Folio";
            string sMsjNoEncontrado = "Folio de Pedido no encontrado, verifique.";

            sQuery = sInicio + string.Format("Select CodigoEAN, IdProducto, Descripcion,  " +
                " CantidadCajasPrometida as Cantidad, (CantidadCajas + CantidadCajasExcedente + CantidadCajasDañadoMalEstado + CantidadCajasCaducado) as Cantidad, " +
                " CantidadCajasDañadoMalEstado, CantidadCajasCaducado " +   
                " From vw_INT_ND_PedidosUnidades (NoLock) " +
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and ReferenciaFolioPedido = '{3}' ",
                IdEmpresa, IdEstado, Fg.PonCeros(IdFarmacia, 4), ReferenciaFolioPedido.Trim());

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Validar Pedidos

        #region Lotes
        ////public DataSet ND_LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN, string Funcion)
        ////{
        ////    return ND_LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, TiposDeInventario.Todos, TiposDeSubFarmacia.Todos, Funcion);
        ////}

        public DataSet ND_LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, string Funcion)
        {
            return ND_LotesDeCodigo_CodigoEAN(IdEmpresa, IdEstado, IdFarmacia, "", IdCodigo, IdCodEAN, TipoDeInventario, TipoDeSubFarmacia, Funcion);
        }

        public DataSet ND_LotesDeCodigo_CodigoEAN(string IdEmpresa, string IdEstado, string IdFarmacia, string IdSubFarmacia, string IdCodigo, string IdCodEAN,
            TiposDeInventario TipoDeInventario, TiposDeSubFarmacia TipoDeSubFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los lotes del Producto";
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

            if (IdSubFarmacia != "")
            {
                sFiltroSubFarmacia = string.Format(" and L.IdSubFarmacia = '{0}' ", IdSubFarmacia);
            }

            sQuery = sInicio + string.Format("	" +
                   " Select F.IdSubFarmacia, F.Descripcion as SubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, " +
                   "    datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, " +
                   "    L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, " +
                   "    (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, " +
                   "    cast((L.Existencia - L.ExistenciaEnTransito) as Int) as Existencia, " +
                   "    cast((L.Existencia - L.ExistenciaEnTransito) as Int) as ExistenciaDisponible, " +
                   "    0 as Cantidad " +
                   " From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) " +
                   " Inner join vw_Farmacias_SubFarmacias F (NoLock) " +
                   "    On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia ) " +
                   " Where L.Status = 'A' and L.IdEmpresa = '{0}' and L.IdEstado =  '{1}' and L.IdFarmacia = '{2}' {6}  " +
                   "    and L.IdProducto = '{3}' and CodigoEAN = '{4}' {5}  " +
                   " Order by L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote ",
                   IdEmpresa, IdEstado, IdFarmacia, IdCodigo, IdCodEAN, sFiltroConsignacion, sFiltroSubFarmacia);

            // No mostrar el Mensaje de vacio, los lotes se registran en el momento 
            bMostrarMsjLeerVacio = false;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);
            bMostrarMsjLeerVacio = true;

            return myDataset;
        }
        #endregion Lotes

        #region Configuración de Informacion 
        public DataSet FirmasDeValidaciones(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrio un error al obtener los datos para firmas de validaciones";
            string sMsjNoEncontrado = "No se encontró información de firmas.";

            sQuery = sInicio + string.Format(
                " Select * " + 
                " From INT_ND_FirmasUnidad (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado, false);

            return myDataset;
        }
        #endregion Configuración de Informacion
        
        #region Funciones y procedimientos privados
        private DataSet EjecutarQuery(string prtQuery, string NombreTabla)
        {
            clsLeer leer = new clsLeer();
            leer.DataSetClase = EjecutarQuery("Funcion local", prtQuery, "Ocurrio un error al obtener la información", "Información no encontrada");

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

        private DataSet EjecutarQuery(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado, bool MostrarMensajeVacio )
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
                    if (MostrarMensajeVacio)
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

        private bool ExistenDatosEnDataset(DataSet dtsRevisar)
        {
            bool bRegresa = false;

            if (dtsRevisar.Tables.Count > 0)
            {
                bRegresa = dtsRevisar.Tables[0].Rows.Count > 0; 
            }

            return bRegresa;
        }
        #endregion Funciones y procedimientos privados 

    }
}
