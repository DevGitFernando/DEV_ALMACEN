using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//System Extra
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;

//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;


/// <summary>
/// Descripción breve de DtGeneral
/// </summary>
public static class DtGeneral
{
    #region Declaración De Variables
    //Conexión
    static clsDatosConexion datosCnn = new clsDatosConexion();
    static clsGrabarError Error;
    static clsConexionSQL cnn;
    static clsLeer leer;
    
    //Datos aplicación
    static string sRutaApp = HttpContext.Current.Server.MapPath("~");
    static string sModulo = "ClienteRegionalWeb";
    static string sVersion = "0.9.1.8";
    static bool bEsEquipoDeDesarrollo = File.Exists(sRutaApp + @"\Dev.xml");
    static clsDatosApp dpDatosApp = new clsDatosApp(sModulo, sVersion);
    static string CfgIniPuntoDeVenta = "FarmaciaPtoVta";

    //Variable aplicación
    static string sNombreEmpresa = string.Empty;
    static string sNombreCortoEmpresa = string.Empty;
    static string sIdEstadoConectado = string.Empty;
    static string sNombreEstado = string.Empty;
    static string sFiltroUnidad = string.Empty;
    static string sLoginUser = string.Empty;
    static string sJurisdiccion = string.Empty;
    static string sMunicipio = string.Empty;
    static string sSucursal = string.Empty;
    static string sTipoDeUnidad = string.Empty;
    static string sPassword = string.Empty;
    static string sArbol = string.Empty;
    static string sEmpresa = string.Empty;
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

    #endregion Declaración De Variables
    
    #region Contructor
    static DtGeneral()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        Init();
    }
    #endregion Contructor

    #region Funciones Privadas
    static clsDatosConexion GetConexion(string ArchivoConexion)
    {
        clsCriptografo crypto = new clsCriptografo();
        basSeguridad fg = new basSeguridad(ArchivoConexion);

        datosCnn.Servidor = fg.Servidor;
        datosCnn.BaseDeDatos = fg.BaseDeDatos;
        datosCnn.Usuario = fg.Usuario;
        datosCnn.Password = fg.Password;
        datosCnn.TipoDBMS = fg.TipoDBMS;
        datosCnn.Puerto = fg.Puerto;
        datosCnn.NormalizarDatos();

        return datosCnn;
    }

    static void DatosDeConexion()
    {
        try
        {
            StreamReader objReader = new StreamReader(sRutaApp + @"\DatosConexion.ini");
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            //El orden de las variables depende del archivo de texto
            sEmpresa = SplitValor(arrText[0].ToString(), '=', 1);
            sIdEstadoConectado = SplitValor(arrText[1].ToString(), '=', 1);
            sSucursal = SplitValor(arrText[2].ToString(), '=', 1);
            sIdCliente = SplitValor(arrText[3].ToString(), '=', 1);
            sIdSubCliente = SplitValor(arrText[4].ToString(), '=', 1);
            sArbol = SplitValor(arrText[5].ToString(), '=', 1);
            sArbolConfiguracion = SplitValor(arrText[6].ToString(), '=', 1);
            bMostraCedis = Convert.ToBoolean(SplitValor(arrText[7].ToString(), '=', 1));
            sNombreModulo = SplitValor(arrText[8].ToString(), '=', 1);
            bEncPersonalizado = Convert.ToBoolean(SplitValor(arrText[9].ToString(), '=', 1));
            sEncabezado = SplitValor(arrText[10].ToString(), '=', 1);
            bgColor = System.Drawing.ColorTranslator.FromHtml(SplitValor(arrText[11].ToString(), '=', 1));
            txtColor = System.Drawing.ColorTranslator.FromHtml(SplitValor(arrText[12].ToString(), '=', 1));
            bUsaBI = Convert.ToBoolean(SplitValor(arrText[13].ToString(), '=', 1));
            sServidorBI = SplitValor(arrText[14].ToString(), '=', 1);
            iTiempoSesion = Convert.ToInt32(SplitValor(arrText[15].ToString(), '=', 1));

        }
        catch { }
    }

    static void ValidarLeer()
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
    public enum ListaPlantillas
    {
        SurmientoClavesDispensada = 1,
        NivelDeAbasto = 2,
        ClavesNegadas_28 = 3,
        ClavesNegadas_29 = 4,
        ClavesNegadas_30 = 5,
        ClavesNegadas_31 = 6,

        EdoJuris_ClavesNegadas_28 = 7,
        EdoJuris_ClavesNegadas_29 = 8,
        EdoJuris_ClavesNegadas_30 = 9,
        EdoJuris_ClavesNegadas_31 = 10,

        EdoJuris_Proximos_Caducar = 11,
        EdoJuris_Dispensacion = 12,
        EdoJurisUnidad_Dispensacion = 13,
        EdoJuris_TBC_Surtimiento = 14,
        EdoJuris_TBC_Surtimiento_NoCauses = 15,

        EdoMovtos_Entradas = 16,
        EdoMovtos_Salidas = 17,
        EdoMovtos_Transferencias = 18,
        // CTE_REG_EdoJuris_Surtimiento 

        ProductosMovimientos_Secretaria = 19,
        CuadrosBasicos = 20,
        Existencia_Por_ClaveSSA = 21,
        CuadrosBasicos_Farmacias = 22,
        Salidas_Antibioticos_Controlados = 23,
        Salidas_Medicos_Diagnosticos = 24,
        Salidas_Costos_Programas_Atencion = 25,
        Salidas_Costos_Programas_Atencion_Concentrado_Farmacia = 26,
        Cortes_Diarios_Farmacias = 27,
        Reportes_Administrativos_Dispensacion = 28,
        Reportes_InmovilizarColumna_10 = 29
    }
    public static void Init()
    {
        //cargar conexión y referenciar a la clase leer
        General.ArchivoIni = "SII-Regional";
        General.DatosConexion = GetConexion(General.ArchivoIni);

        cnn = new clsConexionSQL(General.DatosConexion);
        leer = new clsLeer(ref cnn);
        Error = new clsGrabarError(datosCnn, dpDatosApp, "DtGeneral.cs");
        
        //cargar configuración del aplicación
        DatosDeConexion();

        //Obtener Nombre empresa
        GetNombreEmpresa();
    }

    public static void GetNombreEmpresa()
    {
        string sSql = string.Format("Select * From CatEmpresas (NoLock) " +
                                    "Where IdEmpresa = '{0}'", IdEmpresa);

        ValidarLeer();

        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                sNombreCortoEmpresa = leer.Campo("NombreCorto");
                sNombreEmpresa = leer.Campo("Nombre");
            }
        }
    }

    public static void GetInfoUnidad()
    {
        //string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
        //                            "Where IdEstado = '{0}' And IdFarmacia = '{1}'", EstadoConectado, Sucursal);

        string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
                                    "Where IdEstado = '{0}' And IdFarmacia = '{1}'", IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"]);
        ValidarLeer();

        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                HttpContext.Current.Session["Jurisdiccion"] = leer.Campo("IdJurisdiccion");
                HttpContext.Current.Session["Municipio"] = leer.Campo("IdMunicipio");
                HttpContext.Current.Session["TipoDeUnidad"] = leer.Campo("IdTipoUnidad");
            }

            GetInfoCliente();
            GetJurisdicciones();
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

    public static void GetJurisdicciones()
    {
        string sFiltro = "";
        if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] != "0001")
        {
            sFiltro = string.Format("And IdJurisdiccion = '{0}'", HttpContext.Current.Request.Cookies["cteRegional"]["Jurisdiccion"]);
        }

        string sSql = string.Format("Select Distinct IdEstado, IdJurisdiccion, Jurisdiccion, (IdJurisdiccion + '  -- ' + Jurisdiccion ) as NombreJurisdiccion " +
                            " From vw_Farmacias (NoLock) " +
                            " Where IdEstado = '{0}' {1} " + 
                            " Order by IdJurisdiccion ",
                            IdEstado, sFiltro);

        ValidarLeer();
        
        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            HttpContext.Current.Session["dtsJurisdicciones"] = leer.DataSetClase;
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
                                    "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And U.IdUsuario = '{2}' ", IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"], HttpContext.Current.Request.Cookies["cteRegional"]["IdPersonal"]);

        ValidarLeer();

        if (leer.Exec(sSql))
        {
            if (sFiltro == "Jurisdiccion")
            {
                while (leer.Leer())
                {
                    string sName = leer.Campo("NombreGrupo");
                    if (sName.IndexOf("J --") != -1 && leer.CampoInt("IdGrupo") != 15)
                    {
                        sName = sName.Replace("--", "-");
                        sEstructura += string.Format(" And IdJurisdiccion = '{0}'", SplitValor(sName, '-', 1).Trim());

                        if (IdEstado == "11") //Agregado por petición de Gto
                        {
                            sEstructura += " And IdTipoUnidad Not In ('001', '002', '003') ";
                        }
                    }

                }
            }
            else if (sFiltro == "Unidad")
            {
                while (leer.Leer())
                {

                    string sName = leer.Campo("NombreGrupo");
                    if (sName.IndexOf("U --") != -1 && leer.CampoInt("IdGrupo") != 15)
                    {
                        sName = sName.Replace("--", "-");
                        sEstructura += string.Format(" And IdTipoUnidad = '{0}'", SplitValor(sName, '-', 1).Trim());
                    }
                }
            }
        }

        return sEstructura;
    }

    public static string GetInfo(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
    {
        string sEstructura = string.Empty;
        string sOpcionAdministrador = string.Empty;
        string sQuery = null;
        string[] sColumnas = new string[] { };
        DataTable dtData = ((DataSet)HttpContext.Current.Session["CatalogoGeneral"]).Tables[0];

        if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0001")
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

    public static string GetNombrePlantilla(ListaPlantillas Plantilla)
    {
        string sRegresa = "";

        switch (Plantilla)
        {
            #region Unidad
            case ListaPlantillas.SurmientoClavesDispensada:
                sRegresa = "CTE_REG_Surtimiento_Claves";
                break;

            case ListaPlantillas.NivelDeAbasto:
                sRegresa = "CTE_REG_Nivel_De_Abasto";
                break;

            case ListaPlantillas.ClavesNegadas_28:
                sRegresa = "CTE_REG_Claves_Negadas_28";
                break;

            case ListaPlantillas.ClavesNegadas_29:
                sRegresa = "CTE_REG_Claves_Negadas_29";
                break;

            case ListaPlantillas.ClavesNegadas_30:
                sRegresa = "CTE_REG_Claves_Negadas_30";
                break;

            case ListaPlantillas.ClavesNegadas_31:
                sRegresa = "CTE_REG_Claves_Negadas_31";
                break;
            #endregion Unidad

            #region Regionales
            case ListaPlantillas.EdoJuris_ClavesNegadas_28:
                sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_28";
                break;

            case ListaPlantillas.EdoJuris_ClavesNegadas_29:
                sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_29";
                break;

            case ListaPlantillas.EdoJuris_ClavesNegadas_30:
                sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_30";
                break;

            case ListaPlantillas.EdoJuris_ClavesNegadas_31:
                sRegresa = "CTE_REG_EdoJuris_Claves_Negadas_31";
                break;

            case ListaPlantillas.EdoJuris_Proximos_Caducar:
                sRegresa = "CTE_REG_EdoJuris_Proximos_Caducar";
                break;

            case ListaPlantillas.EdoJuris_Dispensacion:
                sRegresa = "CTE_REG_EdoJuris_Dispensacion";
                break;

            case ListaPlantillas.EdoJurisUnidad_Dispensacion:
                sRegresa = "CTE_REG_EdoJurisUnidad_Dispensacion";
                break;

            case ListaPlantillas.EdoJuris_TBC_Surtimiento:
                sRegresa = "CTE_REG_EdoJuris_TBC_Surtimiento";
                break;

            case ListaPlantillas.EdoJuris_TBC_Surtimiento_NoCauses:
                sRegresa = "CTE_REG_EdoJuris_TBC_Surtimiento_NoCauses";
                break;

            case ListaPlantillas.EdoMovtos_Entradas:
                sRegresa = "CTE_REG_EdoMovtos_Entradas";
                break;

            case ListaPlantillas.EdoMovtos_Salidas:
                sRegresa = "CTE_REG_EdoMovtos_Salidas";
                break;

            case ListaPlantillas.EdoMovtos_Transferencias:
                sRegresa = "CTE_REG_EdoMovtos_Transferencias";
                break;

            case ListaPlantillas.ProductosMovimientos_Secretaria:
                sRegresa = "CTE_REG_Productos_Movimientos_Secretaria";
                break;

            case ListaPlantillas.CuadrosBasicos:
                sRegresa = "CTE_REG_Cuadros_Basicos";
                break;

            case ListaPlantillas.Existencia_Por_ClaveSSA:
                sRegresa = "CTE_REG_Existencia_Claves";
                break;

            case ListaPlantillas.CuadrosBasicos_Farmacias:
                sRegresa = "CTE_REG_Cuadros_Basicos_Farmacias";
                break;

            case ListaPlantillas.Salidas_Antibioticos_Controlados:
                sRegresa = "CTE_REG_Salidas_Antibioticos_Controlados";
                break;

            case ListaPlantillas.Salidas_Medicos_Diagnosticos:
                sRegresa = "CTE_REG_Salidas_Medicos_Diagnosticos";
                break;

            case ListaPlantillas.Salidas_Costos_Programas_Atencion:
                sRegresa = "CTE_REG_Salidas_Costos_Programas_Atencion";
                break;

            case ListaPlantillas.Salidas_Costos_Programas_Atencion_Concentrado_Farmacia:
                sRegresa = "CTE_REG_Salidas_Costos_Programas_Atencion_ConcentradoFarmacia";
                break;

            case ListaPlantillas.Cortes_Diarios_Farmacias:
                sRegresa = "CTE_REG_CortesDiarios_Farmacias";
                break;

            case ListaPlantillas.Reportes_Administrativos_Dispensacion:
                sRegresa = "CTE_REG_Reportes_Dispensacion";
                break;
            case ListaPlantillas.Reportes_InmovilizarColumna_10:
                sRegresa = "CTE_REG_Reportes_InmovilizarColumna_10";
                break;
            #endregion Regionales

            default:
                break;
        }

        return sRegresa + ".xlsx";
    }

    public static int FechaDiasMes(string sFecha)
    {
        string[] sFechaSplit = sFecha.Split('-');
        int iAño = Convert.ToInt32(sFechaSplit[0]);
        int iMes = Convert.ToInt32(sFechaSplit[1]);
        int iDiaMax = 31;

        switch (iMes)
        {
            case 2:
            case 4:
            case 6:
            case 9:
            case 11:
                iDiaMax = 30;
                break;
        }

        if (iMes == 2)
        {
            iDiaMax = 28;
            if ((iAño % 4) == 0)
            {
                iDiaMax = 29;
            }
        }

        return iDiaMax;
    }

    public static string GruposUsuariosPerfiles(string sFiltro)
    {
        string sEstructura = string.Empty;
        string sIdGrupo = string.Empty;
        string sSql = string.Empty;
        string sUllista = string.Empty;

        DataSet dtsGruposMiembros = new DataSet("GruposMiembros");

        clsLeer UlListas = new clsLeer();


        sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        "G.IdGrupo, G.NombreGrupo, U.IdUsuario, U.LoginUser " +
                                    "From Net_CTE_Grupos_De_Usuarios As G (NoLock) " +
                                    "Inner Join Net_CTE_Grupos_Usuarios_Miembros As U (NoLock) On ( G.IdEstado = U.IdEstado And G.IdFarmacia = U.IdFarmacia And G.IdGrupo = U.IdGrupo  And U.Status = 'A' ) " +
            //"Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And G.NombreGrupo Like '{2}'", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFiltro);
                                    "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And G.NombreGrupo Like '{2}'", sIdEstadoConectado, sSucursal, sFiltro);
        if (leer.Exec(sSql))
        {
            leer.RenombrarTabla(1, "GruposMiembros");
            dtsGruposMiembros = leer.DataSetClase;
        }

        sSql = string.Format("Select IdGrupo, NombreGrupo From Net_CTE_Grupos_De_Usuarios (NoLock) " +
            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFiltro);
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", sIdEstadoConectado, sSucursal, sFiltro);

        if (leer.Exec(sSql))
        {
            while (leer.Leer())
            {
                string sNombre = sFiltro.Replace("%", " ");
                sNombre = sNombre.TrimStart();
                sNombre = leer.Campo("NombreGrupo").Replace(sNombre, "");
                sEstructura += string.Format("<div id=\"{0}\" dir=\"{1}\"><span>{0}</span>", sNombre, leer.Campo("IdGrupo"));

                UlListas.DataRowsClase = dtsGruposMiembros.Tables["GruposMiembros"].Select(string.Format(" IdGrupo = {0} ", leer.Campo("IdGrupo")));

                while (UlListas.Leer())
                {

                    sUllista += string.Format("<li>{0}<a class=\"btn_del_relgpo\" rel=\"{2}\" rev=\"{1}\"><i class=\"icon-trash white\"></i></a></li>", UlListas.Campo("LoginUser"), UlListas.Campo("IdUsuario"), UlListas.Campo("IdGrupo"));
                }

                if (sUllista != "")
                {
                    sEstructura += "<ul>" + sUllista + "</ul>";
                    sUllista = string.Empty;
                }

                sEstructura += "</div>";
            }
        }

        return sEstructura;
    }

    public static string GetUsers()
    {
        string sEstructura = string.Empty;
        string sSql = string.Empty;

        sSql = string.Format("Select IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status ='A'", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString());
                            //"Where IdEstado = '{0}' And IdFarmacia = '{1}'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());

        //sEstructura = DtGeneral.DtsToTableHtml(sSql, "tableUsers");
        if (leer.Exec(sSql))
        {
            while (leer.Leer())
            {
                sEstructura += string.Format("<li value=\"{1}\">{0}</li>", leer.Campo("Login"), leer.Campo("IdUsuario"));
            }
        }

        return sEstructura;
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
    #endregion Funciones Públicas

    #region Funciones Generales y utilerías
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
    #endregion Funciones Generales

    #region Propiedades publicas
    public static clsConexionSQL DatosConexion
    {
        get { return cnn; }
    }

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
        get { return sEmpresa; }
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
        get { return sSucursal; }
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

    public static int TiempoSesion
    {
        get { return iTiempoSesion; }
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
    #endregion Propiedades publicas
}