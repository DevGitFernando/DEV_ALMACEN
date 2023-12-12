using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//System Extra
using System.IO;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.Collections;

//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

//System extra
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Drawing;

namespace Cliente_Regional.Code
{
    /// <summary>
    /// Descripción breve de DtGeneral
    /// </summary>
    public class DtGeneral
    {
        #region Constructor
        static DtGeneral()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //

            //Inicializamos todo lo necesario
            Init();
        }
        #endregion Constructor

        #region Declaración de Variables
        //Conexión
        static clsDatosConexion datosCnn = new clsDatosConexion();
        static clsGrabarError Error;
        static clsConexionSQL cnn;
        static clsLeer leer;

        //Datos aplicación
        static string sRutaApp = HttpContext.Current.Server.MapPath("~");
        private static readonly string sModulo = "ClienteRegionalWeb";
        private static readonly string sVersion = "0.1.0.0";
        static bool bEsEquipoDeDesarrollo = false;
        private static readonly clsDatosApp dpDatosApp = new clsDatosApp(sModulo, sVersion);
        private static readonly string CfgIniPuntoDeVenta = "FarmaciaPtoVta";
        private static readonly string sArchivoIniConexion = "SII-Conexion";
        private static readonly string sArchivoIniConfiguracion = "Configuracion";
        private static string sPasswordAdministradorSa = string.Empty;
        private static readonly string sNombreCookie = "ClienteRegionalWeb";

        //Variable aplicación
        static string sUsuarioAdmin = string.Empty;
        static string sNombreEmpresa = string.Empty;
        static string sNombreCortoEmpresa = string.Empty;
        static string sIdEstadoConectado = string.Empty;
        static string sNombreEstado = string.Empty;
        static string sFiltroUnidad = string.Empty;
        static string sLoginUser = string.Empty;
        static string sJurisdiccion = string.Empty;
        static string sMunicipio = string.Empty;
        static string sIdSucursal = string.Empty;
        static string sTipoDeUnidad = string.Empty;
        static string sPassword = string.Empty;
        static string sArbol = string.Empty;
        static string sIdEmpresa = string.Empty;
        static string sIdAlmacen = string.Empty;
        static string sNombreModulo = string.Empty;
        static string sIdCliente = string.Empty;
        static string sCliente = string.Empty;
        static string sIdSubCliente = string.Empty;
        static string sSubCliente = string.Empty;
        static string sEncabezado = string.Empty;
        static bool bEncPersonalizado = false;
        static bool bMostraCedis = false;
        static int iTiempoSesion = 8;

        static Color bgColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        static Color txtColor = System.Drawing.ColorTranslator.FromHtml("#000000");
        static string sArbolConfiguracion = string.Empty;
        static bool bUsaBI = false;
        static string sServidorBI = string.Empty;
        public enum FiltrosCombos { TipoDeUnidad, Jurisdiccion, Municipio };

        #endregion Declaración de Variables

        #region Funciones Privadas
        /// <summary>
        /// Función que lees el archivo de configuaración e inicializa valores del sitio web
        /// </summary>
        private static void ReadConfiguracion()
        {
            try
            {
                string sFile = "";
                int EqualPosition = 0;
                string myKey = "";
                sRutaApp = string.Format(@"{0}\{1}.ini", sRutaApp, sArchivoIniConfiguracion);

                using (StreamReader sr = new StreamReader(sRutaApp))
                {
                    while (sr.Peek() >= 0)
                    {
                        sFile = sr.ReadLine();
                        if (sFile.Length != 0)
                        {
                            EqualPosition = 0;
                            if (sFile.Substring(0, 1) != "#" || sFile.Substring(0, 1) != "[")
                            {
                                EqualPosition = sFile.IndexOf("=", 0);
                            }

                            if (EqualPosition > 0)
                            {
                                myKey = sFile.Substring(0, EqualPosition);
                                switch (myKey)
                                {
                                    case "Empresa":
                                        sIdEmpresa = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "Estado":
                                        sIdEstadoConectado = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "Unidad":
                                        sIdSucursal = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "Cliente":
                                        sIdCliente = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "SubCliente":
                                        sIdSubCliente = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "Arbol":
                                        sArbol = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "ArbolConfiguracion":
                                        sArbolConfiguracion = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "MostrarCedis":
                                        bMostraCedis = Convert.ToBoolean(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    case "NombreModulo":
                                        sNombreModulo = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "EncPersonalizado":
                                        bEncPersonalizado = Convert.ToBoolean(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    case "Encabezado":
                                        sEncabezado = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "BackgroundColor":
                                        bgColor = System.Drawing.ColorTranslator.FromHtml(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    case "TextColor":
                                        txtColor = System.Drawing.ColorTranslator.FromHtml(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    case "UsaBI":
                                        bUsaBI = Convert.ToBoolean(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    case "ServidorBI":
                                        sServidorBI = sFile.Substring(EqualPosition + 1).Trim();
                                        break;
                                    case "TiempoSesionHoras":
                                        iTiempoSesion = Convert.ToInt32(sFile.Substring(EqualPosition + 1).Trim());
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Función para inicializar datosCnn con las variables especificadas en el archivo de conexión "ArchivoConexion"
        /// </summary>
        /// <param name="ArchivoConexion">Nombre del archivo previamente configurado donde se obtendran los valores de conexión</param>
        /// <returns>Retorna objeto datosCnn de la clase clsDatosConexion con los datos de conexión </returns>
        private static clsDatosConexion GetConexion(string ArchivoConexion)
        {
            clsCriptografo crypto = new clsCriptografo();
            basSeguridad fg = new basSeguridad(ArchivoConexion);

            datosCnn.Servidor = fg.Servidor;
            datosCnn.BaseDeDatos = fg.BaseDeDatos;
            datosCnn.Usuario = fg.Usuario;
            datosCnn.Password = fg.Password;
            datosCnn.TipoDBMS = fg.TipoDBMS;
            datosCnn.Puerto = fg.Puerto;
            sPasswordAdministradorSa = fg.Password;

            datosCnn.NormalizarDatos();

            return datosCnn;
        }
        /// <summary>
        /// Función para reiniciar la clase leer. En caso de que no este inicializada, la inicializa
        /// </summary>
        private static void ValidarLeer()
        {
            if (leer == null)
            {
                Init();
            }
            else
            {
                leer.Reset();
            }
        }
        #endregion Funciones Privadas

        #region Funciones Públicas
        /// <summary>
        /// Función de inicialización de la conexión y lectura de parametros de configuración.
        /// </summary>
        public static void Init()
        {
            //Cargar conexión y referenciar a la clase leer
            General.ArchivoIni = sArchivoIniConexion;
            General.DatosConexion = GetConexion(General.ArchivoIni);

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(datosCnn, dpDatosApp, "DtGeneral.cs");

            //Cargar configuración del aplicación
            ReadConfiguracion();
        }

        //Obtener información general
        public static string GetInfo(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
        {
            string sEstructura = string.Empty;
            string sOpcionAdministrador = string.Empty;
            string sQuery = null;
            string[] sColumnas = new string[] { };
            DataTable dtData = ((DataSet)HttpContext.Current.Session["CatalogoGeneral"]).Tables[0];

            //if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0001")
            if (ObtenerValorCookie("IdSucursal") == "0001")
            {

                switch (sOpcion)
                {
                    case "Unidad":
                        sOpcionAdministrador = "Todos los tipo de unidades";
                        break;
                    case "Jurisdiccion":
                        sOpcionAdministrador = "Todas las jurisdicciones";
                        break;
                    case "Municipio":
                        sOpcionAdministrador = "Todos los Municipios";
                        break;
                    case "Farmacia":
                        sOpcionAdministrador = "Todas las Farmacias";
                        break;
                    default:
                        break;
                }

                sEstructura = string.Format("<option value=\"*\" selected>{0}</option>", sOpcionAdministrador);

                if (!bUnique)
                {

                    string sOpUnidad = sUnidad == "*" ? "<>" : "=";
                    string sOpJurisdiccion = sJurisdiccion == "*" ? "<>" : "=";
                    string sOpMunicipio = sMunicipio == "*" ? "<>" : "=";
                    string sOpFarmacia = sFarmacia == "*" ? "<>" : "=";

                    switch (sOpcion)
                    {
                        case "Unidad":
                            sQuery = string.Format(" [IdTipoUnidad] {0} '{1}' ", sOpUnidad, sUnidad);
                            break;
                        case "Jurisdiccion":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad);
                            sQuery = string.Format(" [IdTipoUnidad] {0} '{1}' ", sOpUnidad, sUnidad);
                            break;
                        case "Municipio":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio);
                            sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad);
                            break;
                        case "Farmacia":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' And [IdFarmacia] {6} '{7}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio, sOpFarmacia, sFarmacia);
                            sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio);
                            break;
                        default:
                            break;
                    }

                    sQuery = sQuery.Replace("*", "[*]");
                }
            }

            switch (sOpcion)
            {
                case "Unidad":
                    sColumnas = new string[] { "IdTipoUnidad", "TipoDeUnidad" };
                    break;
                case "Jurisdiccion":
                    sColumnas = new string[] { "IdJurisdiccion", "Jurisdiccion" };
                    break;
                case "Municipio":
                    sColumnas = new string[] { "IdMunicipio", "Municipio" };
                    break;
                case "Farmacia":
                    sColumnas = new string[] { "IdFarmacia", "Farmacia" };
                    break;
                default:
                    break;
            }

            dtData = dtData.Select(sQuery, sColumnas[0]).CopyToDataTable();

            sEstructura += clsToolsHtml.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);

            return sEstructura;
        }

        public static DataSet GetFarmacias(string IdEstado)
        {
            string sSql = string.Format(@"Set DateFormat YMD Select  F.IdFarmacia, F.Farmacia From vw_Farmacias As F(Nolock) Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock)On(F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia) Where F.IdEstado = '11' And F.Status = 'A'  And IdTipoUnidad Not In('000', '005', '006') And EsAlmacen = 0 and EsUnidosis = 1");

            DataSet dtsReturn = ExecQuery(sSql, "DtGeneral GetFarmacias");
            return dtsReturn;
        }

        public static string GetInfo2(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
        {
            string sEstructura = string.Empty;
            string sOpcionAdministrador = string.Empty;
            string sQuery = null;
            string[] sColumnas = new string[] { };
            DataTable dtData = ((DataSet)HttpContext.Current.Session["CatalogoAlmacen"]).Tables[0];

            //if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0001")
            if (ObtenerValorCookie("IdSucursal") == "0001")
            {

                switch (sOpcion)
                {
                    case "Unidad":
                        sOpcionAdministrador = "Todos los tipo de unidades";
                        break;
                    case "Jurisdiccion":
                        sOpcionAdministrador = "Todas las jurisdicciones";
                        break;
                    case "Municipio":
                        sOpcionAdministrador = "Todos los Municipios";
                        break;
                    case "Farmacia":
                        sOpcionAdministrador = "Todas las Farmacias";
                        break;
                    default:
                        break;
                }

                sEstructura = string.Format("<option value=\"*\" selected>{0}</option>", sOpcionAdministrador);

                if (!bUnique)
                {

                    string sOpUnidad = sUnidad == "*" ? "<>" : "=";
                    string sOpJurisdiccion = sJurisdiccion == "*" ? "<>" : "=";
                    string sOpMunicipio = sMunicipio == "*" ? "<>" : "=";
                    string sOpFarmacia = sFarmacia == "*" ? "<>" : "=";

                    switch (sOpcion)
                    {
                        case "Unidad":
                            sQuery = string.Format(" [IdTipoUnidad] {0} '{1}' ", sOpUnidad, sUnidad);
                            break;
                        case "Jurisdiccion":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad);
                            sQuery = string.Format(" [IdTipoUnidad] {0} '{1}' ", sOpUnidad, sUnidad);
                            break;
                        case "Municipio":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio);
                            sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad);
                            break;
                        case "Farmacia":
                            //sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' And [IdFarmacia] {6} '{7}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio, sOpFarmacia, sFarmacia);
                            sQuery = string.Format(" [IdJurisdiccion] {0} '{1}' And [IdTipoUnidad] {2} '{3}' And [IdMunicipio] {4} '{5}' ", sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio);
                            break;
                        default:
                            break;
                    }

                    sQuery = sQuery.Replace("*", "[*]");
                }
            }

            switch (sOpcion)
            {
                case "Unidad":
                    sColumnas = new string[] { "IdTipoUnidad", "TipoDeUnidad" };
                    break;
                case "Jurisdiccion":
                    sColumnas = new string[] { "IdJurisdiccion", "Jurisdiccion" };
                    break;
                case "Municipio":
                    sColumnas = new string[] { "IdMunicipio", "Municipio" };
                    break;
                case "Farmacia":
                    sColumnas = new string[] { "IdFarmacia", "Farmacia" };
                    break;
                default:
                    break;
            }

            dtData = dtData.Select(sQuery, sColumnas[0]).CopyToDataTable();

            sEstructura += clsToolsHtml.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);

            return sEstructura;
        }

        public static string GetInfo3(string sOpcion, string sBeneficiario, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
        {
            string sEstructura = string.Empty;
            string sOpcionAdministrador = string.Empty;
            string sQuery = null;
            string[] sColumnas = new string[] { };
            DataTable dtData = ((DataSet)HttpContext.Current.Session["CatalogoBeneficiario"]).Tables[0];

            //if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0001")
            //if (ObtenerValorCookie("IdSucursal") == "0001")
            //{

                switch (sOpcion)
                {
                    case "Beneficiario":
                        sOpcionAdministrador = "Todas las unidades";
                        break;
                }

                sEstructura = string.Format("<option value=\"*\" selected>{0}</option>", sOpcionAdministrador);

                if (!bUnique)
                {

                    string sOpBeneficiario = sBeneficiario == "*" ? "<>" : "=";

                    switch (sOpcion)
                    {
                        case "Beneficiario":
                            sQuery = string.Format(" [IdBeneficiario] {0} '{1}' ", sOpBeneficiario, sBeneficiario);
                            break;
                    }

                    sQuery = sQuery.Replace("*", "[*]");
                }
            //}

            switch (sOpcion)
            {
                case "Beneficiario":
                    sColumnas = new string[] { "IdBeneficiario", "NombreCompleto" };
                    break;
            }

            dtData = dtData.Select(sQuery, sColumnas[0]).CopyToDataTable();

            sEstructura += clsToolsHtml.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);

            return sEstructura;
        }

        public static DataSet GetInfoBeneficiarios()
        {
            //if (HttpContext.Current.Session["CatalogoBeneficiario"] == null)
            //{
                //GetInfoUnidad();
                //GetJurisdicciones(IdEstado);

                //if (DtGeneral.UsaPedidos)
                //{
                //    //Cargar Claves SSA Sales
                //    DtGeneral.GetClaves();
                //}

                //string sFiltro = "";
                //string sFiltro_Pro_SubPro = "";
                //string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') And EsAlmacen = 0 ";
                //string sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005', '006') And C.EsAlmacen = 0 ";
                //string sFiltroPerfiles = "";
                //bool bFiltroJuris = false;
                //bool bFiltroUnidad = false;

                //if (MostrarAlamacenes)
                //{
                //    sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                //    sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005') ";
                //}

                //if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
                //if (ObtenerValorCookie("IdSucursal") != "0001")
                //{
                //    //sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                //    sFiltro = string.Format(" And  IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));
                //    //sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                //    sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));

                //    //Se agrego a petición de Gto ed unificar farmacias
                //    //if (DtGeneral.IdEstado == "11" && HttpContext.Current.Session["IdSucursal"].ToString() == "0088")
                //    if (IdEstado == "11" && ObtenerValorCookie("IdSucursal") == "0088")
                //    {
                //        sFiltro = string.Format(" And IdFarmacia In ('0033', '0088', '0130') ");
                //    }
                //}

                //string stmpFiltro = FiltroPerfiles("Jurisdiccion");

                //if (stmpFiltro != "")
                //{
                //    sFiltroPerfiles = stmpFiltro;
                //    bFiltroJuris = true;
                //}

                //stmpFiltro = FiltroPerfiles("Unidad");

                //if (stmpFiltro != "")
                //{
                //    sFiltroPerfiles += stmpFiltro;
                //    bFiltroUnidad = true;
                //}

                string sTipoUnidadAlmacen = "006";
                string sSql = string.Format(@"Set DateFormat YMD
	                                    Select
		                                    B.IdBeneficiario, B.NombreCompleto		
	                                    From vw_Beneficiarios As B (NoLock)
	                                    Inner Join CatFarmacias As CF (NoLock) On ( B.IdEstado =  CF.IdEstado And B.IdFarmacia = CF.IdFarmacia )
	                                    Inner Join BI_RPT__CatFarmacias_A_Procesar As FP On ( B.IdEstado = FP.IdEstado  And B.IdFarmacia = FP.IdFarmacia )
	                                    Where B.IdEstado = '{0}' And B.IdCliente = '{1}' And B.IdSubCliente = '{2}' And CF.IdTipoUnidad = '{3}' And B.Status = 'A'",
                                                IdEstado, IdCliente, IdSubCliente, sTipoUnidadAlmacen);

                DataSet dtsReturn = DtGeneral.ExecQuery(sSql, "DtGeneral GetInfoBeneficiarios");
                //DataSet dtsInfoBeneficiario = ExecQuery(sSql, "Info", "DtGeneral GetInfoBeneficiarios");
                //HttpContext.Current.Session["CatalogoBeneficiario"] = dtsInfoBeneficiario;


                ////lst.Add("initUser", DtGeneral.ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                //ActualizarValorCookie("initUser", ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                ////lst.Add("ProcesoPorMes", false);
                //ActualizarValorCookie("ProcesoPorMes", false.ToString());
                ////lst.Add("FiltroJuris", bFiltroJuris);
                //ActualizarValorCookie("FiltroJuris", bFiltroJuris.ToString());
                ////lst.Add("FiltroUnidad", bFiltroUnidad);
                //ActualizarValorCookie("FiltroUnidad", bFiltroUnidad.ToString());

                return dtsReturn;
            //}

            //return (DataSet)HttpContext.Current.Session["CatalogoBeneficiario"];
        }

        public static DataSet GetInfoAlmacen()
        {
            if (HttpContext.Current.Session["CatalogoAlmacen"] == null)
            {
                //GetInfoUnidad();
                //GetJurisdicciones(IdEstado);

                //if (DtGeneral.UsaPedidos)
                //{
                //    //Cargar Claves SSA Sales
                //    DtGeneral.GetClaves();
                //}

                string sFiltro = "";
                string sFiltro_Pro_SubPro = "";
                string sFiltroTipoUnidad = " And IdTipoUnidad In ( '006' ) And EsAlmacen = 1 ";
                string sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005', '006') And F.EsAlmacen = 0 ";
                string sFiltroPerfiles = "";
                bool bFiltroJuris = false;
                bool bFiltroUnidad = false;

                if (MostrarAlamacenes)
                {
                    sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                    sFiltroTipoUnidad_Pro_SubPro = " And F.IdTipoUnidad Not In ('000', '005') ";
                }

                //if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
                if (ObtenerValorCookie("IdSucursal") != "0001")
                {
                    //sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                    sFiltro = string.Format(" And  F.IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));
                    //sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                    sFiltro_Pro_SubPro = string.Format(" And  F.IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));

                    //Se agrego a petición de Gto ed unificar farmacias
                    //if (DtGeneral.IdEstado == "11" && HttpContext.Current.Session["IdSucursal"].ToString() == "0088")
                    if (IdEstado == "11" && ObtenerValorCookie("IdSucursal") == "0088")
                    {
                        sFiltro = string.Format(" And F.IdFarmacia In ('0033', '0088', '0130') ");
                    }
                }

                string stmpFiltro = FiltroPerfiles("Jurisdiccion");

                if (stmpFiltro != "")
                {
                    sFiltroPerfiles = stmpFiltro;
                    bFiltroJuris = true;
                }

                stmpFiltro = FiltroPerfiles("Unidad");

                if (stmpFiltro != "")
                {
                    sFiltroPerfiles += stmpFiltro;
                    bFiltroUnidad = true;
                }

                string sTipoUnidadAlmacen = "006";
                string sSql = string.Format(@"Set DateFormat YMD
                                            Select 
                                                F.IdTipoUnidad, F.TipoDeUnidad, F.IdJurisdiccion, F.Jurisdiccion, F.IdMunicipio, F.Municipio, F.IdFarmacia, F.Farmacia 
                                            From vw_Farmacias As F (Nolock) Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) 
                                            Where F.IdEstado = '{0}' And F.Status = 'A' {1} {2} {3} ", 
                                            IdEstado, sFiltroTipoUnidad, sFiltro, sFiltroPerfiles, sTipoUnidadAlmacen);

                DataSet dtsInfoAlmacen = ExecQuery(sSql, "Info", "DtGeneral GetInfoAlmacen");
                HttpContext.Current.Session["CatalogoAlmacen"] = dtsInfoAlmacen;

                //lst.Add("initUser", DtGeneral.ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                ActualizarValorCookie("initUser", ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                //lst.Add("ProcesoPorMes", false);
                ActualizarValorCookie("ProcesoPorMes", false.ToString());
                //lst.Add("FiltroJuris", bFiltroJuris);
                ActualizarValorCookie("FiltroJuris", bFiltroJuris.ToString());
                //lst.Add("FiltroUnidad", bFiltroUnidad);
                ActualizarValorCookie("FiltroUnidad", bFiltroUnidad.ToString());
            }

            return (DataSet)HttpContext.Current.Session["CatalogoAlmacen"];
        }

        public static DataSet GetInfoGeneral()
        {
            if (HttpContext.Current.Session["CatalogoGeneral"] == null)
            {
                //GetInfoUnidad();
                //GetJurisdicciones(IdEstado);

                //if (DtGeneral.UsaPedidos)
                //{
                //    //Cargar Claves SSA Sales
                //    DtGeneral.GetClaves();
                //}

                string sFiltro = "";
                string sFiltro_Pro_SubPro = "";
                string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') And EsAlmacen = 0 ";
                string sFiltroTipoUnidad_Pro_SubPro = " And F.IdTipoUnidad Not In ('000', '005', '006') And F.EsAlmacen = 0 ";
                string sFiltroPerfiles = "";
                bool bFiltroJuris = false;
                bool bFiltroUnidad = false;

                if (MostrarAlamacenes)
                {
                    sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                    sFiltroTipoUnidad_Pro_SubPro = " And F.IdTipoUnidad Not In ('000', '005') ";
                }

                //if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
                if (ObtenerValorCookie("IdSucursal") != "0001")
                {
                    //sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                    sFiltro = string.Format(" And  F.IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));
                    //sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                    sFiltro_Pro_SubPro = string.Format(" And  F.IdFarmacia= '{0}' ", ObtenerValorCookie("IdSucursal"));

                    //Se agrego a petición de Gto ed unificar farmacias
                    //if (DtGeneral.IdEstado == "11" && HttpContext.Current.Session["IdSucursal"].ToString() == "0088")
                    if (IdEstado == "11" && ObtenerValorCookie("IdSucursal") == "0088")
                    {
                        sFiltro = string.Format(" And F.IdFarmacia In ('0033', '0088', '0130') ");
                    }
                }

                string stmpFiltro = FiltroPerfiles("Jurisdiccion");

                if (stmpFiltro != "")
                {
                    sFiltroPerfiles = stmpFiltro;
                    bFiltroJuris = true;
                }

                stmpFiltro = FiltroPerfiles("Unidad");

                if (stmpFiltro != "")
                {
                    sFiltroPerfiles += stmpFiltro;
                    bFiltroUnidad = true;
                }

                string sTipoUnidadAlmacen = "006";
                string sSql = string.Format(
                        @"Set DateFormat YMD " +
                        "Select " + 
                        " F.IdTipoUnidad, F.TipoDeUnidad, F.IdJurisdiccion, F.Jurisdiccion, F.IdMunicipio, F.Municipio, F.IdFarmacia, F.Farmacia, cast(F.EsUnidosis as int) as EsUnidosis " +   
                         "From vw_Farmacias As F (Nolock) Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia) " +  
                         "Where F.IdEstado = '{0}' And F.Status = 'A' {1} {2} {3}", IdEstado, sFiltroTipoUnidad, sFiltro, sFiltroPerfiles, sTipoUnidadAlmacen);

                DataSet dtsInfoGeneral = ExecQuery(sSql, "Info", "DtGeneral GetInfoGeneral");
                HttpContext.Current.Session["CatalogoGeneral"] = dtsInfoGeneral;

                //lst.Add("initUser", DtGeneral.ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                ActualizarValorCookie("initUser", ObtenerValorCookie("IdSucursal") == "0001" ? "true" : "false");
                //lst.Add("ProcesoPorMes", false);
                ActualizarValorCookie("ProcesoPorMes", false.ToString());
                //lst.Add("FiltroJuris", bFiltroJuris);
                ActualizarValorCookie("FiltroJuris", bFiltroJuris.ToString());
                //lst.Add("FiltroUnidad", bFiltroUnidad);
                ActualizarValorCookie("FiltroUnidad", bFiltroUnidad.ToString());
            }

            return (DataSet)HttpContext.Current.Session["CatalogoGeneral"];
        }

        public static void GetInfoUnidad()
        {

            string sSql = string.Format(
                @"Select F.* " +
                "From vw_Farmacias F (Nolock) " +
                "Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia )  " +
                "Where F.IdEstado = '{0}' And F.IdFarmacia = '{1}' ", IdEstado, ObtenerValorCookie("IdSucursal"));
            ValidarLeer();

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    //HttpContext.Current.Session["Jurisdiccion"] = leer.Campo("IdJurisdiccion");
                    //ObtenerValorCookie
                    //HttpContext.Current.Session["Municipio"] = leer.Campo("IdMunicipio");
                    ActualizarValorCookie("IdMunicipio", leer.Campo("IdMunicipio"));
                    //HttpContext.Current.Session["TipoDeUnidad"] = leer.Campo("IdTipoUnidad");
                    ActualizarValorCookie("IdTipoUnidad", leer.Campo("IdTipoUnidad"));
                }

                GetInfoCliente();
                //GetJurisdicciones();
            }
        }

        public static void GetInfoCliente()
        {
            string sSql = string.Format("Select C.IdCliente, C.Nombre As Cliente, S.IdSubCliente, S.Nombre As SubCliente From CatClientes C (NoLock) " +
                                        "Inner Join CatSubClientes S (NoLock) On (C.IdCliente = S.IdCliente) " +
                                        "Where C.IdCliente = '{0}' And S.IdSubCliente = '{1}'", sIdCliente, sIdSubCliente);
            ValidarLeer();

            if (leer.Exec(sSql))
            {
                if (leer.Leer())
                {
                    sCliente = leer.Campo("Cliente");
                    sSubCliente = leer.Campo("SubCliente");
                }
            }
        }

        public static void GetJurisdicciones(String Estado)
        {
            string sFiltro = "";
            //if (clsLogin.Sucursal != "0001")
            //if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
            if (ObtenerValorCookie("IdSucursal") != "0001")
            {
                //sFiltro = "And  IdJurisdiccion= '" + Jurisdiccion + "'";
                sFiltro = "And  IdJurisdiccion= '" + HttpContext.Current.Session["Jurisdiccion"].ToString() + "'";
            }

            //string sSql = string.Format("Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion, (IdJurisdiccion + '  -- ' + Descripcion ) as NombreJurisdiccion" +
            //                    " From CatJurisdicciones (NoLock) " +
            //                    " Where IdEstado = '{0}' {1} Order by IdJurisdiccion ",
            //                    Estado, sFiltro);

            string sSql = string.Format(
                "Select Distinct F.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, (F.IdJurisdiccion + '  -- ' + F.Jurisdiccion ) as NombreJurisdiccion " +
                "From vw_Farmacias F (NoLock) " +
                "Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) " +
                "Where F.IdEstado = '{0}' {1} Order by IdJurisdiccion ",
                Estado, sFiltro);
            clsLeer myLeer = new clsLeer()
            {
                DataSetClase = ExecQuery(sSql, "Jurisdicciones", "DtGenerales GetJurisdicciones")
            };

            if (myLeer.Leer())
            {
                //dtsJurisdicciones = leer.DataSetClase;
                HttpContext.Current.Session["dtsJurisdicciones"] = myLeer.DataSetClase;
            }
        }

        public static string FiltroPerfiles(string sFiltro)
        {
            string sEstructura = string.Empty;

            string sSql = string.Format("Set DateFormat YMD " +
                                        "Select " +
                                            "Distinct(G.IdGrupo), G.NombreGrupo " +
                                        "From Net_CTE_Grupos_De_Usuarios As G (NoLock)" +
                                        "Inner Join Net_CTE_Grupos_Usuarios_Miembros As U (NoLock) On ( G.IdEstado = U.IdEstado And G.IdFarmacia = U.IdFarmacia And G.IdGrupo = U.IdGrupo  And U.Status = 'A' ) " +
                                        "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And U.IdUsuario = '{2}' ", IdEstado, ObtenerValorCookie("IdSucursal"), ObtenerValorCookie("IdPersonal"));

            ValidarLeer();

            if (leer.Exec(sSql))
            {
                if (sFiltro == "Jurisdiccion")
                {
                    string sJurisdicciones = string.Empty;
                    while (leer.Leer())
                    {
                        string sName = leer.Campo("NombreGrupo");
                        if (sName.IndexOf("J --") != -1 && leer.CampoInt("IdGrupo") != 15)
                        {
                            sName = sName.Replace("--", "-");
                            //sEstructura += string.Format(" And IdJurisdiccion = '{0}'", SplitValor(sName, '-', 1).Trim());

                            if (sJurisdicciones != "")
                            {
                                sJurisdicciones += ", ";
                            }

                            sJurisdicciones += string.Format(" '{0}'", SplitValor(sName, '-', 1).Trim());
                        }

                    }

                    if (sJurisdicciones != "")
                    {
                        sEstructura += string.Format(" And IdJurisdiccion In ({0}) ", sJurisdicciones);
                    }

                }
                else if (sFiltro == "Unidad")
                {

                    string sUnidades = string.Empty;

                    while (leer.Leer())
                    {

                        string sName = leer.Campo("NombreGrupo");
                        if (sName.IndexOf("U --") != -1 && leer.CampoInt("IdGrupo") != 15)
                        {
                            sName = sName.Replace("--", "-");
                            //sEstructura += string.Format(" And IdTipoUnidad = '{0}'", SplitValor(sName, '-', 1).Trim());

                            if (sUnidades != "")
                            {
                                sUnidades += ", ";
                            }

                            sUnidades += string.Format(" '{0}'", SplitValor(sName, '-', 1).Trim());
                        }
                    }

                    if (sUnidades != "")
                    {
                        sEstructura = string.Format(" And IdTipoUnidad In ({0}) ", sUnidades);
                    }
                }
            }

            return sEstructura;
        }

        /// <summary>
        /// Obtener la imagen de perfil de un usuario
        /// </summary>
        /// <param name="sIdEstado">Id Estado del personal</param>
        /// <param name="sIdFarmacia">Id Farmacias del personal</param>
        /// <param name="sIdPersonal">Id del presonal</param>
        /// <returns>Imagen de perfil en base 64</returns>
        static public string GetImagePerfil(string sIdEstado, string sIdFarmacia, string sIdPersonal)
        {
            string sImagenPerfil = string.Empty;

            string sSql = string.Format(@"Set DateFormat YMD  
	                                     Select 
		                                    FotoPersonal
	                                    From HelpDesk_Perfil (NoLock)
                                        Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdPersonal = '{2}'",
                                        sIdEstado, sIdFarmacia, sIdPersonal);

            leer.Reset();
            leer.DataSetClase = ExecQuery(sSql, "ImagePerfil", "DtGeneral - GetImagePerfil");

            if (leer.Leer())
            {
                sImagenPerfil = leer.Campo("FotoPersonal");
            }

            return sImagenPerfil;
        }

        /// <summary>
        /// Función para ejecutar una consulta en la base datos previamente configurada en DtGeneral.cs
        /// </summary>
        /// <param name="sSql">Query a ser ejecutada</param>
        /// <param name="sNombreTabla">Nombre que se le pondra a la primera tabla que sea regresada por el servidor</param>
        /// <param name="sFuncion">Nombre del método que esta invocando la función</param>
        /// <returns>Retorna un DataSet con el resultado de la consulta</returns>
        static public DataSet ExecQuery(string sSql, string sNombreTabla, string sFuncion)
        {
            ClsCnn myCnn = new ClsCnn();
            DataSet dtsReturn = myCnn.ExecQuery(sSql, sNombreTabla, sFuncion);
            return dtsReturn;
        }

        public static bool ExecQueryBool(string sSql)
        {
            bool bReturn = false;

            //Validar conexion activa
            ValidarLeer();

            if (leer.Exec(sSql))
            {
                if (leer.Error.Message == "Sin error")
                {
                    bReturn = true;
                }
            }
            return bReturn;
        }

        /// <summary>
        /// Obtiene Cookie con el nombre previamente configurado en sNombreCookie en DtGeneral.cs, valida que este activa y contengo value
        /// </summary>
        /// <returns></returns>
        static public HttpCookie ObtenerCookie()
        {
            HttpCookie myCookie = null;
            if (HttpContext.Current.Request.Cookies[sNombreCookie] != null)
            {
                if (HttpContext.Current.Request.Cookies[sNombreCookie]["value"] != null)
                {
                    DateTime dtCookieRegistro = DateTime.Parse(ObtenerValorCookie("Session"));
                    if (dtCookieRegistro >= DateTime.Now)
                    {
                        myCookie = HttpContext.Current.Request.Cookies[sNombreCookie];
                    }
                }
            }
            return myCookie;
        }

        /// <summary>
        /// Obtiene el valor de la coookie previamente configurada en sNombreCookie en DtGeneral.cs
        /// </summary>
        /// <param name="sKey">Valor que se desea obtener</param>
        /// <returns></returns>
        static public string ObtenerValorCookie(string sKey)
        {
            ClsDictionary myDictionary = new ClsDictionary();
            myDictionary.Descifrar(HttpContext.Current.Request.Cookies[sNombreCookie]["value"].ToString());
            return myDictionary.Search(sKey); ;
        }

        /// <summary>
        /// Actualiza el valor de la coookie previamente configurada en sNombreCookie en DtGeneral.cs
        /// </summary>
        /// <param name="sPropiedad"></param>
        /// <param name="sValue"></param>
        static public void ActualizarValorCookie(string sPropiedad, string sValue)
        {
            ClsDictionary myDictionary = new ClsDictionary();
            myDictionary.Descifrar(HttpContext.Current.Request.Cookies[sNombreCookie]["value"].ToString());
            myDictionary.Edit(sPropiedad, sValue);
            HttpContext.Current.Response.Cookies[sNombreCookie]["Value"] = myDictionary.Cifrar();
        }

        /// <summary>
        /// Funcion que realiza un split a un cadena de texto en base a un caracter separador, regresa el valor en la posición
        /// especificada.
        /// </summary>
        /// <param name="sValor">Cadena a realizar split</param>
        /// <param name="sSeparador">Caracter separador</param>
        /// <param name="iPosicionValor">Posicion que se desea obtener del split realizado (Esta inicia en 0)</param>
        /// <returns>Cadena con el resultado del split</returns>
        public static string SplitValor(string sValor, char sSeparador, int iPosicionValor)
        {
            string[] sCadena = sValor.Split(sSeparador);
            return sCadena[iPosicionValor];
        }
        static public DataSet ExecQuery(string sSql, string sNombreTabla)
        {
            //Validar conexion activa
            ValidarLeer();

            DataSet dtsReturn = new DataSet("dtsReturn");
            if (leer.Exec(sSql))
            {
                //if (leer.Leer())
                {
                    leer.RenombrarTabla(1, sNombreTabla);
                    dtsReturn = leer.DataSetClase;
                }
            }
            return dtsReturn;
        }
        #endregion Funciones Públicas

        #region Propiedades Públicas
        /// <summary>
        /// Verifica si existe el archivo Dev.xml en la raíz del sistema de windows. Si existe el equipo es de desarrollo.
        /// </summary>
        public static bool EsEquipoDeDesarrollo
        {
            get
            {
                try
                {
                    string sRuta = Directory.GetDirectoryRoot(Environment.GetEnvironmentVariable("windir"));

                    //Ruta App
                    sRuta = sRutaApp;

                    bEsEquipoDeDesarrollo = File.Exists(sRuta + @"\Dev.xml");
                }
                catch (Exception e)
                {
                    bEsEquipoDeDesarrollo = false;
                }

                return bEsEquipoDeDesarrollo;
            }
        }

        /// <summary>
        /// Nombre del módulo
        /// </summary>
        public static string Modulo
        {
            get { return sModulo; }
        }

        /// <summary>
        /// Ruta donde se encuentra la aplicación
        /// </summary>
        public static string RutaApp
        {
            get { return sRutaApp; }
        }

        /// <summary>
        /// Nombre del archivo de conexion
        /// </summary>
        public static string ArchivoIniConexion
        {
            get { return sArchivoIniConexion; }
        }

        /// <summary>
        /// Datos de conexión de la base de datos de la clase clsConexionSQL
        /// </summary>
        public static clsConexionSQL DatosConexion
        {
            get { return cnn; }
        }

        /// <summary>
        /// Datos de la aplicación de la clase clsDatosApp
        /// </summary>
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
        }

        public static string ArchivoConexionFarmacia
        {
            get { return CfgIniPuntoDeVenta; }
        }

        public static string RutaPlantillas
        {
            get { return sRutaApp + @"\PLANTILLAS\"; }
        }

        public static Color ColorTexto
        {
            get { return txtColor; }
        }
        public static Color ColorBgReportes
        {
            get { return bgColor; }
        }

        public static string IdEmpresa
        {
            get { return sIdEmpresa; }
        }

        public static string NombreEmpresa
        {
            get { return sNombreEmpresa; }
        }

        public static string NombreCortoEmpresa
        {
            get { return sNombreCortoEmpresa; }
        }

        public static string IdEstado
        {
            get { return sIdEstadoConectado; }
        }

        public static string IdSucursal
        {
            get { return sIdSucursal; }
        }

        public static string Arbol
        {
            get { return sArbol; }
        }

        public static string ArbolConfiguracion
        {
            get { return sArbolConfiguracion; }
        }

        public static bool EncabezadoPersonalizado
        {
            get { return bEncPersonalizado; }
        }

        public static string Encabezado
        {
            get { return sEncabezado; }
        }

        public static string NombreModulo
        {
            get { return sNombreModulo; }
        }

        public static bool MostrarAlamacenes
        {
            get { return bMostraCedis; }
        }

        public static bool UsaBI
        {
            get { return bUsaBI; }
        }

        public static string ServidorBI
        {
            get { return sServidorBI; }
        }

        /// <summary>
        /// Tiempo sesión activa de la cokkie
        /// </summary>
        public static int TiempoSesion
        {
            get { return iTiempoSesion; }
        }

        /// <summary>
        /// Nombre de cookie utilizada en el modulo
        /// </summary>
        public static string NombreCookie
        {
            get { return sNombreCookie; }
        }
        public static string IdCliente
        {
            get { return sIdCliente; }
        }
        public static string IdSubCliente
        {
            get { return sIdSubCliente; }
        }
        #endregion Propiedades Públicas
    }
}