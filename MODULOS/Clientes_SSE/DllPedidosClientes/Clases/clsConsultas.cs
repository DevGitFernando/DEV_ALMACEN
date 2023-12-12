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

namespace DllPedidosClientes
{
    public class clsConsultas
    {
        #region Declaración de variables
        //private wsConexion.wsControlObras cnnWebServ; // = new wsConexion.wsConexionDB();
        private clsErrorManager error = new clsErrorManager();
        private clsLogError errorLog = new clsLogError();
        // private DialogResult myResult = new DialogResult();

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

        bool bConexionWeb = false;
        string sUrl = "";
        string sArchivoIni = "";
        clsLeerWeb leerWeb; 


        private basGenerales Fg = new basGenerales();
        private clsCriptografo Cryp = new clsCriptografo();
        private Cls_Acceso_a_Datos_Sql Datos = new Cls_Acceso_a_Datos_Sql();
        private clsDatosConexion DatosConexion;
        private clsConexionSQL ConexionSql;

        protected string sConsultaExec = "";

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

        public clsConsultas(string Url, string ArchivoIni, clsDatosApp DatosApp, string Pantalla, bool MostrarMsjLeerVacio)
        {
            this.sNameDll = DatosApp.Modulo;
            this.sPantalla = Pantalla;
            this.sVersion = DatosApp.Version;
            this.bMostrarMsjLeerVacio = MostrarMsjLeerVacio;
            
            this.bConexionWeb = true;
            clsDatosCliente DatosCliente = new clsDatosCliente(DtGeneralPedidos.DatosApp, "clsConsultas", "");
            sUrl = Url;
            sArchivoIni = ArchivoIni; 
            leerWeb = new clsLeerWeb(sUrl, sArchivoIni, DatosCliente); 
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

        public string QueryEjecutado
        {
            get { return sConsultaExec; }
        }
        #endregion

        #region Pedidos Secretaria 
        public DataSet PedidoEspecialRegional_Enc(string IdEstado, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "El Folio ingresado no existe, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CTE_PedidoEspecialRegional_Enc (NoLock) Where IdEstado = '{0}' and Folio = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidoEspecialRegional_Det(string IdEstado, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los detalles del Folio";
            string sMsjNoEncontrado = "El Folio ingresado no contiene detalles, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CTE_PedidoEspecialRegional_Det (NoLock) Where IdEstado = '{0}' and Folio = '{1}' ", Fg.PonCeros(IdEstado, 2), Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidoEspecialUnidad_Enc(string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Folio";
            string sMsjNoEncontrado = "El Folio ingresado no existe, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CTE_PedidoEspecialUnidad_Enc (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' and Folio = '{2}' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet PedidoEspecialUnidad_Det(string IdEstado, string IdFarmacia, string Folio, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los detalles del Folio";
            string sMsjNoEncontrado = "El Folio ingresado no contiene detalles, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_CTE_PedidoEspecialUnidad_Det (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' and Folio = '{2}' ",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4), Fg.PonCeros(Folio, 8));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de la Farmacias";
            string sMsjNoEncontrado = "No se encontraron Farmacias registradas para este estado, verifique.";

            sQuery = sInicio + string.Format(" Select *, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia " +
                " From vw_Farmacias (NoLock) Where IdEstado = '{0}' and Status = 'A'", Fg.PonCeros(IdEstado, 2));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacias(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de la Farmacias";
            string sMsjNoEncontrado = "No se encontraron Farmacias registradas para este estado, verifique.";

            sQuery = sInicio + string.Format(" Select *, (IdFarmacia + ' -- ' + Farmacia) as NombreFarmacia  " +
                " From vw_Farmacias (NoLock) Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A'",
                Fg.PonCeros(IdEstado, 2), Fg.PonCeros(IdFarmacia, 4));
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Parametros()
        {
            myDataset = new DataSet();

            sQuery = sInicio + " Select Parametro, Valor, Descripcion From Parametros (nolock) " +
                " Where IdSucursal = '" + General.EntidadConectada + "'";
            myDataset = (DataSet)EjecutarQuery("Parametros", sQuery, "Ocurrió un error", "Parametros");

            return myDataset;
        } 
        #endregion Pedidos Secretaria

        #region Reportes Administrativos
        public DataSet Programas(string IdPrograma, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Programa";
            string sMsjNoEncontrado = "Id de Programa no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatProgramas (NoLock) Where IdPrograma = '" + Fg.PonCeros(IdPrograma, 4) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet SubProgramas(string IdSubPrograma, string IdPrograma, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del SubPrograma";
            string sMsjNoEncontrado = "Id de SubPrograma no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatSubProgramas (NoLock) Where IdPrograma = '" + Fg.PonCeros(IdPrograma, 4) + "' " +
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

        public DataSet Clientes(string IdCliente, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Cliente";
            string sMsjNoEncontrado = "Clave de Cliente no encontrado, verifique.";

            sQuery = sInicio + " Select * From CatClientes (NoLock) Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'";
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
            string sMsjNoEncontrado = "Clave de SubCliente no encontrado, verifique.";
            string sWhereSubCliente = "";

            if (IdSubCliente != "")
                sWhereSubCliente = " And IdSubCliente = '" + Fg.PonCeros(IdSubCliente, 4) + "'";

            sQuery = sInicio + " Select IdSubCliente, Nombre, " +
                    " PorcUtilidad, PermitirCapturaBeneficiarios, ImportaBeneficiarios, " +
                    " ( Case When Status = 'A' Then 1 Else 0 End ) as Status  " +
                    " From CatSubClientes (NoLock) " +
                    " Where IdCliente = '" + Fg.PonCeros(IdCliente, 4) + "'" + sWhereSubCliente;
            //bMostrarMsjLeerVacio = false;
            myDataset = (DataSet)EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }


        public DataSet Beneficiarios(string IdEstado, string IdFarmacia, string IdCliente, string IdSubCliente, string IdBeneficiario, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Beneficiario";
            string sMsjNoEncontrado = "Clave de Beneficiario no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * " +
                " From vw_Beneficiarios (NoLock) " +
                " Where IdEstado = '{0}' And IdFarmacia = '{1}' " +
                " And IdCliente = '{2}' " + " And IdSubCliente = '{3}' " +
                " And ( IdBeneficiario = '{4}' or FolioReferencia = '{5}' ) ",
                IdEstado, IdFarmacia, IdCliente, IdSubCliente, Fg.PonCeros(IdBeneficiario, 8), IdBeneficiario);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Medicos(string IdEstado, string IdFarmacia, string IdMedico, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos del Medico";
            string sMsjNoEncontrado = "Clave de Medico no encontrada, verifique.";

            sQuery = sInicio + " Select * From vw_Medicos (NoLock) " +
                " Where IdEstado = '" + IdEstado + "' " + " And IdFarmacia = '" + IdFarmacia + "' " +
                " And IdMedico = '" + Fg.PonCeros(IdMedico, 6) + "'";
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Sales(string ClaveSSA_Sal, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sQuery = sInicio + string.Format(" Select * From vw_ClavesSSA_Sales (NoLock) Where ClaveSSA = '{0}' ", ClaveSSA_Sal);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet ClavesSSA_Sales(string IdClaveSSA_Sal, bool BuscarPorClaveSSA, string Funcion)
        {
            myDataset = new DataSet();
            // string sExtra = "";
            string sFiltro = "";
            string sMsjError = "Ocurrió un error al obtener los datos de la Sal";
            string sMsjNoEncontrado = "Clave de Sal no encontrada, verifique.";

            sFiltro = string.Format(" Where IdClaveSSA_Sal = '{0}' ", Fg.PonCeros(IdClaveSSA_Sal, 4));
            if (BuscarPorClaveSSA)
            {
                sFiltro = string.Format(" Where  ClaveSSA = '{0}'  ", IdClaveSSA_Sal);
            }


            ////sQuery = sInicio + string.Format(" Select * " + 
            ////    " From CatClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

            sQuery = sInicio + string.Format(" Select *, DescripcionClave as Descripcion " +
                " From vw_ClavesSSA_Sales (NoLock)   {0}   ", sFiltro);

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

            ////sQuery = sInicio + string.Format(" Select *  From vw_VentasEnc (NoLock) " +
            ////    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' And Folio = '{3}' ",
            ////    IdEmpresa, IdEstado, IdFarmacia, FolioVenta);

            sQuery = sInicio + string.Format(" Select *  From vw_VentasEnc (NoLock) " +
                " Where IdEstado = '{0}' and IdFarmacia = '{1}' And Folio = '{2}' ",
                IdEstado, IdFarmacia, FolioVenta);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Jurisdicciones(string IdEstado, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de jurisdicciones";
            string sMsjNoEncontrado = "Clave de Jurisdicción no encontrada, verifique.";

            sQuery = sInicio + string.Format(
                " Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion, (IdJurisdiccion + '  -- ' + Descripcion ) as NombreJurisdiccion" +
                " From CatJurisdicciones (NoLock) " + 
                " Where IdEstado = '{0}' ", IdEstado);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet TipoUnidades(string IdTipoUnidad, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Tipos de Unidades";
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
        #endregion Reportes Administrativos 

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
            string sMsjNoEncontrado = "Información de Clientes no encontrada, \nó no el Cliente no esta asignado a la Farmacia, verifique.";
            string sWhere = "";
            string sWherePubGral = "";

            IdCliente = Fg.PonCeros(IdCliente, 4);
            if (IdSubCliente != "")
                sWhere = string.Format(" and IdSubCliente = '{0}' ", Fg.PonCeros(IdSubCliente, 4));

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
            string sMsjNoEncontrado = "Información de Programa no encontrada, \nó no el Programa no esta asignado a la Farmacia, verifique.";
            string sWhere = "";
            string sWherePubGral = "";

            IdCliente = Fg.PonCeros(IdCliente, 4);
            IdSubCliente = Fg.PonCeros(IdSubCliente, 4);

            IdPrograma = Fg.PonCeros(IdPrograma, 4);
            if (IdSubPrograma != "")
                sWhere = string.Format(" and IdSubPrograma = '{0}' ", Fg.PonCeros(IdSubPrograma, 4));

            if (!EsPublicoGral)
                sWherePubGral = string.Format(" and ( IdCliente <> '{0}' ) ", IdPublicoGeneral);

            sQuery = sInicio + string.Format(" Select distinct * From vw_Farmacias_Programas_SubPrograma_Clientes (NoLock) " +
                 " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdCliente = '{2}' and IdSubCliente = '{3}' and IdPrograma = '{4}' and StatusRelacion = 'A' {5} {6} ",
                 IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, sWhere, sWherePubGral);

            //bMostrarMsjLeerVacio = true;
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_Servicios(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Servicios";
            string sMsjNoEncontrado = "Información de Servicios no encontrada, verifique.";

            sQuery = sInicio + string.Format(" 	Select Distinct S.IdServicio, S.Servicio " +
                    " From vw_Servicios_Areas S " +
                    " Inner Join CatServicios_Areas_Farmacias C On ( S.IdServicio = C.IdServicio and S.IdArea = C.IdArea ) " +
                    " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' ", IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet Farmacia_ServiciosAreas(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener los datos de Areas";
            string sMsjNoEncontrado = "Información de Areas no encontrada, verifique.";

            sQuery = sInicio + string.Format(" 	Select Distinct S.IdServicio, S.Servicio, S.IdArea, S.Area_Servicio " +
                    " From vw_Servicios_Areas S " +
                    " Inner Join CatServicios_Areas_Farmacias C On ( S.IdServicio = C.IdServicio and S.IdArea = C.IdArea ) " +
                    " Where C.IdEstado = '{0}' and C.IdFarmacia = '{1}' and C.Status = 'A' ", IdEstado, IdFarmacia);
            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        #endregion Datos Configurables por Farmacia

        #region Urls 
        public DataSet Farmacias_UrlsActivas(string IdEstado, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de la Farmacias";
            string sMsjNoEncontrado = "No se encontraron Farmacias registradas para este estado, verifique.";

            sQuery = sInicio + string.Format(" Select F.IdFarmacia, F.Farmacia, F.FarmaciaStatus, U.StatusUrl, F.IdFarmacia As Id_Farmacia, " +
                            " U.UrlFarmacia as Url, C.Servidor  " + 
                            " From vw_Farmacias F (NoLock) " +
                            " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " + 
                            " Where F.IdEstado = '{0}' And F.IdFarmacia = '{1}' " +
                            " Order By F.IdFarmacia ", IdEstado, IdFarmacia);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }

        public DataSet FarmaciasJurisdiccion_UrlsActivas(string IdEstado, string Jurisdiccion, string IdFarmacia, string Funcion)
        {
            myDataset = new DataSet();
            string sMsjError = "Ocurrió un error al obtener la lista de la Farmacias";
            string sMsjNoEncontrado = "No se encontraron Farmacias registradas para esta Jurisdicción, verifique.";

            sQuery = sInicio + string.Format(" Select F.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion, F.FarmaciaStatus, U.StatusUrl, F.IdFarmacia As Id_Farmacia, " +
                            " U.UrlFarmacia as Url, C.Servidor  " +
                            " From vw_Farmacias F (NoLock) " +
                            " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
                            " Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                            " Where F.IdEstado = '{0}' And F.IdJurisdiccion = '{1}' And F.IdFarmacia = '{2}' " +
                            " Order By F.IdFarmacia ", IdEstado, Jurisdiccion, IdFarmacia);

            myDataset = EjecutarQuery(Funcion, sQuery, sMsjError, sMsjNoEncontrado);

            return myDataset;
        }
        #endregion Urls
        
        #region Funciones y procedimientos privados
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

        private DataSet EjecutarQueryDirecto(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado )
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

        private DataSet EjecutarQueryWeb(string Funcion, string prtQuery, string MensajeError, string MensajeNoEncontrado)
        {
            // clsLeer Leer = new clsLeer(ref ConexionSql);
            DataSet dtsResultados = new DataSet();

            bEjecuto = false;
            // Leer.Conexion.SetConnectionString();
            if (!leerWeb.Exec(prtQuery))
            {
                // General.Error.GrabarError(Leer.Error, ConexionSql.DatosConexion, this.sNameDll, this.sVersion, this.sPantalla, Funcion, Leer.QueryEjecutado);
                General.msjError(MensajeError);
            }
            else
            {
                bEjecuto = true;
                if (!leerWeb.Leer())
                {
                    if (bMostrarMsjLeerVacio)
                        General.msjUser(MensajeNoEncontrado);
                }
                else
                {
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

        ////    // Si ocurre algun error evitar que traten de accesar un dataset vacio
        ////    bExistenDatos = false;

        ////    try
        ////    {
        ////        if (bUsarCnnRedLocal)
        ////        {
        ////            objRetorno = (object)Datos.ObtenerDataset(prtQuery, prtTabla);
        ////        }
        ////        else
        ////        {
        ////            //cnnWebServ = new wsConexion.wsControlObras();
        ////           // cnnWebServ.Url = General.Url;
        ////            //objRetorno = (object)cnnWebServ.ObtenerDataset(Cryp.Encriptar(strCnnString), prtQuery, prtTabla);
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
        ////        if ( objRetorno != null)
        ////            e = (Exception)objRetorno;


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
    }
}
