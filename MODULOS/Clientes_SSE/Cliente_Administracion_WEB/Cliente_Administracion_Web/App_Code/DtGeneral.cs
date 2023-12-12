using System;
using System.IO;
using System.Data;
using System.Collections; 
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using System.Web.Script.Serialization;
using SC_SolutionsSystem.FuncionesGenerales;

using DllFarmaciaSoft;
using System.Drawing;

using Microsoft.VisualBasic.FileIO;
using System.Text;
using wsConexion;
using System.Globalization;

//using Newtonsoft;
using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;


/// <summary>
/// Descripción breve de DtGeneral
/// </summary>
public static class DtGeneral
{
    #region Declaración de variables
    static clsDatosConexion datosCnn = new clsDatosConexion();
    static DataSet dtsPermisos = new DataSet();
    static DataSet dtsEstados = new DataSet(); 
    static DataSet dtsJurisdicciones = new DataSet();
    static DataSet dtsParametros = new DataSet();
    static DataSet dtsClavesSSA_Sales = new DataSet("ClavesSSA_Sales");
    static DataSet dtsInfo = new DataSet("InformacionGeneral");
    
    static string sPlantilla = string.Empty;
    static string sRutaDeReportes = "";
    
    //Variable para la autenticación
    static string sEstadoConectado = string.Empty;
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
    static string sNombreEmpresa = string.Empty;
    static string sNombreModulo = string.Empty;
    static string sIdCliente = string.Empty;
    static string sCliente = string.Empty;
    static string sIdSubCliente = string.Empty;
    static string sSubCliente = string.Empty;
    static string sEncabezado = string.Empty;
    static bool bEncPersonalizado = false;
    static bool bMostraCedis = false;
    static bool bAutenticado = false;
    static bool bFechaCompleta = false;
    static bool bUsaPedidos = false;
    static bool bCteSeguroPopular = true;

    static int iColorR = 0;
    static int iColorG = 0;
    static int iColorB = 0;
    static Color txtColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    static string sArbolConfiguracion = string.Empty;
    static bool bUsaBI = false;
    static string sServidorBI = string.Empty;

    static clsLeer leer;
    static clsConexionSQL cnn;
    static clsConsultas Consultas;

    static clsDatosConexion datosCnnCedis = new clsDatosConexion();
    static clsLeer leerCedis;
    static clsConexionSQL cnnCedis;
    static wsCnnCliente cnnCteCedis = new wsCnnCliente();

    static basGenerales Fg = new basGenerales();

   static clsReportes.Reportes cfgQuery = new clsReportes.Reportes();
    
    #endregion Declaración de variables

    #region Constructor
    static DtGeneral()
    {
        General.ArchivoIni = "SII-Regional";
        General.DatosConexion = GetConexion(General.ArchivoIni);
        
        cnn = new clsConexionSQL(General.DatosConexion);
        leer = new clsLeer(ref cnn);
        
        //Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
        Consultas = new clsConsultas(General.DatosConexion, "Cliente Regional Web", "Ayuda", "0.1");


        //cnnCte.Url = "http://intermedguanajuato.dyndns-ip.com/wsInt-OficinaCentral/wsOficinaCentral.asmx";
        //DataSet dtConexion = cnnCte.Conexion();
        //clsLeer myleer = new clsLeer();
        //while (myleer.Leer())
        //{
        //    datosCnnWeb.Servidor = myleer.Campo("Servidor");
        //}

        //leerWeb = new clsLeer(ref cnnWeb);
        //Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name);
        //Consultas = new clsConsultas(General.DatosConexion, "Cliente Regional Web", "Ayuda", "0.1");

        //General.ArchivoIni = SII-Cedis;
        //General.DatosConexion = GetConexionCedis(General.ArchivoIni);
        cnnCedis = new clsConexionSQL(GetConexionCedis("SII-Cedis"));
        leerCedis = new clsLeer(ref cnnCedis);
    }

    public static clsDatosConexion GetConexion(string ArchivoConexion)
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

    public static clsDatosConexion GetConexionCedis(string ArchivoConexion)
    {
        clsCriptografo crypto = new clsCriptografo();
        basSeguridad fg = new basSeguridad(ArchivoConexion);

        datosCnnCedis.Servidor = fg.Servidor;
        datosCnnCedis.BaseDeDatos = fg.BaseDeDatos;
        datosCnnCedis.Usuario = fg.Usuario;
        datosCnnCedis.Password = fg.Password;
        datosCnnCedis.TipoDBMS = fg.TipoDBMS;
        datosCnnCedis.Puerto = fg.Puerto;
        datosCnnCedis.NormalizarDatos();

        return datosCnnCedis;
    }
    #endregion Constructor
    
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


    //public static void GetPermisos(string Estado, string Farmacia, string Arbol, string Login)
    //{
    //    string sSql = "";
    //    sSql = string.Format("Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", Estado, Farmacia, Arbol, Login);
        

    //    if (!leer.Exec(sSql))
    //    {
    //        // Grabar Error 
    //        dtsPermisos = null;
    //    }
    //    else
    //    {
    //        dtsPermisos = leer.DataSetClase; 
    //    }
    //}

    public static string GetNombreCortoEmpresa(string sIdEmpresa)
    {
        string sReturn = string.Empty;

        string sSql = string.Format("Select * From CatEmpresas (NoLock) " +
	                                "Where IdEmpresa = '{0}'", sIdEmpresa);

        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                sReturn = leer.Campo("NombreCorto");
                sNombreEmpresa = leer.Campo("Nombre");
            }
        }
        
        return sReturn;
    }

    public static void GetEstados() 
    {
        string sSql = "";

        sSql = "Select IdEstado, Nombre as Estado From CatEstados Order by IdEstado";

        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            dtsEstados = leer.DataSetClase;
        }
    }

    public static void GetInfoUnidad()
    {
        //string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
        //                            "Where IdEstado = '{0}' And IdFarmacia = '{1}'", EstadoConectado, Sucursal);

        string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
                                    "Where IdEstado = '{0}' And IdFarmacia = '{1}'", EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());

        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                //Jurisdiccion = leer.Campo("IdJurisdiccion");
                HttpContext.Current.Session["Jurisdiccion"] = leer.Campo("IdJurisdiccion");
                //Municipio = leer.Campo("IdMunicipio");
                HttpContext.Current.Session["Municipio"] = leer.Campo("IdMunicipio");
                //TipoDeUnidad = leer.Campo("IdTipoUnidad");
                HttpContext.Current.Session["TipoDeUnidad"] = leer.Campo("IdTipoUnidad");
            }

            //dtsInfo = leer.DataSetClase;
            //GetInfoGeneral();
            GetInfoCliente();

            
        }
    }

    public static void GetInfoCliente()
    {
        string sSql = string.Format("Select C.IdCliente, C.Nombre As Cliente, S.IdSubCliente, S.Nombre As SubCliente From CatClientes C (NoLock) " +
	                                "Inner Join CatSubClientes S (NoLock) On (C.IdCliente = S.IdCliente) " +
	                                "Where C.IdCliente = '{0}' And S.IdSubCliente = '{1}'", sIdCliente, sIdSubCliente);
        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                sCliente = leer.Campo("Cliente");
                sSubCliente = leer.Campo("SubCliente");
            }
        }
    }

    public static void GetInfoGeneral()
    {
        string sFiltro = "";
        //string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') ";
        string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') And EsAlmacen = 0 ";

        if (bMostraCedis)
        {
            sFiltroTipoUnidad =" And IdTipoUnidad Not In ('000', '005') ";
        }
        if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
        {
            sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
        }

        //string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
        //                            "Where IdEstado = '{0}' And IdFarmacia > {1} {2} {3}", EstadoConectado, FiltroUnidad, sFiltro, sMostrarCedis);
        
        string sSql = string.Format("Select * From vw_Farmacias (Nolock) " +
                            "Where IdEstado = '{0}' And Status = 'A' {1} ", EstadoConectado, sFiltroTipoUnidad);

        if (leer.Exec(sSql))
        {
            dtsInfo = leer.DataSetClase;
            HttpContext.Current.Session["dtsInfo"] = leer.DataSetClase;
        }
    }

    public static DataSet TipoUnidades(string IdTipoUnidad)
    {
        DataSet myDataset = new DataSet();

        string sWhereTipoUnidad = "";
        
        if (clsLogin.Sucursal != "0001")
        {
            IdTipoUnidad = TipoDeUnidad;
        }

        if (IdTipoUnidad != "")
        {
            sWhereTipoUnidad = string.Format(" and IdTipoUnidad = '{0}' ", Fg.PonCeros(IdTipoUnidad, 3));
        }

        //string sSql = string.Format("Set DateFormat YMD Select *, " +
        //    " (IdTipoUnidad + ' -- ' + Descripcion) as NombreTipoUnidad " +
        //    " From CatTiposUnidades (NoLock) " +
        //    " Where 1 = 1  {0} ", sWhereTipoUnidad);

        string sSql = string.Format("Set DateFormat YMD " +
            " Select Distinct IdTipoUnidad,(IdTipoUnidad + ' -- ' + TipoDeUnidad) as Descripcion " +
            " From vw_Farmacias (NoLock) " +
            " Where IdEstado = {0} {1} ",EstadoConectado, sWhereTipoUnidad);

        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            myDataset = leer.DataSetClase;
        }

        return myDataset;
    }

    public static void GetJurisdicciones(String Estado)
    {
        string sFiltro = "";
        //if (clsLogin.Sucursal != "0001")
        if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
        {
            //sFiltro = "And  IdJurisdiccion= '" + Jurisdiccion + "'";
            sFiltro = "And  IdJurisdiccion= '" + HttpContext.Current.Session["Jurisdiccion"].ToString() + "'";
        }

        //string sSql = string.Format("Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion, (IdJurisdiccion + '  -- ' + Descripcion ) as NombreJurisdiccion" +
        //                    " From CatJurisdicciones (NoLock) " +
        //                    " Where IdEstado = '{0}' {1} Order by IdJurisdiccion ",
        //                    Estado, sFiltro);

        string sSql = string.Format("Select Distinct IdEstado, IdJurisdiccion, Jurisdiccion, (IdJurisdiccion + '  -- ' + Jurisdiccion ) as NombreJurisdiccion " +
                            " From vw_Farmacias (NoLock) " +
                            " Where IdEstado = '{0}' {1} Order by IdJurisdiccion ",
                            Estado, sFiltro);
        
        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            dtsJurisdicciones = leer.DataSetClase;
            HttpContext.Current.Session["dtsJurisdicciones"] = leer.DataSetClase;
        }
    }

    public static DataSet CargarMunicipios(string sIdJurisdiccion)
    {
        DataSet myDataset = new DataSet();

        string sSql = "", sWhereJuris = "";

        if (sIdJurisdiccion != "*" && sIdJurisdiccion !="")
        {
            sWhereJuris = string.Format(" and IdJurisdiccion = '{0}' ", sIdJurisdiccion);
        }


        sSql = string.Format(" Select Distinct IdMunicipio, (IdMunicipio + ' -- ' + Municipio) As Municipio From vw_Farmacias (Nolock) " +
                             " Where IdEstado = '{0}'  {1}  Order By IdMunicipio ", sEstadoConectado, sWhereJuris);

        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            myDataset = leer.DataSetClase;
        }
        return myDataset;
    }

    public static string FiltroPerfiles(string sFiltro)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format("Set DateFormat YMD " +
	                                "Select " +
		                                "Distinct(G.IdGrupo), G.NombreGrupo " +
	                                "From Net_CTE_Grupos_De_Usuarios As G (NoLock)" +
	                                "Inner Join Net_CTE_Grupos_Usuarios_Miembros As U (NoLock) On ( G.IdEstado = U.IdEstado And G.IdFarmacia = U.IdFarmacia And G.IdGrupo = U.IdGrupo  And U.Status = 'A' ) " +
                                    "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And U.IdUsuario = '{2}' ", EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), HttpContext.Current.Session["IdPersonal"].ToString());

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

                        if (EstadoConectado == "11") //Agregado por petición de Gto
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

    public static DataSet CargarFarmacias(string sTipoUnidad, string sJurisdicciones, string sLocalidad)
    {
        DataSet myDataset = new DataSet();
        string sSql = "", sWhereJuris = "", sWhereMun = "", sWhereTipoUnidad = "";

        if (sTipoUnidad!= "*")
        {
            sWhereTipoUnidad = string.Format(" and  IdTipoUnidad = '{0}' ", sTipoUnidad);
        }

        if (sJurisdicciones != "*")
        {
            sWhereJuris = string.Format(" and IdJurisdiccion = '{0}' ", sJurisdicciones);
        }

        if (sLocalidad != "*")
        {
            sWhereMun = string.Format(" and IdMunicipio = '{0}' ", sLocalidad);
        }

        sSql = string.Format(
            "Select F.IdFarmacia, (F.IdFarmacia + ' -- ' + F.Farmacia) As Farmacia \n" + 
            "From vw_Farmacias F (Nolock) \n" +
            "Inner Join BI_RPT__CatFarmacias_A_Procesar L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) \n" + 
            "Where F.IdEstado = '{0}' and F.Status = 'A' and F.IdTipoUnidad Not In ('000', '005', '006')  {1}  {2}  {3} \n" +
            "Order By F.IdFarmacia \n",
                             EstadoConectado, sWhereJuris, sWhereMun, sWhereTipoUnidad); 

        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            myDataset = leer.DataSetClase;
        }
        return myDataset;
    }

    public static DataSet GetFarmacias(string sIdJurisdiccion)
    {
        DataSet dtsRetorno = new DataSet("Farmacias");

        string sFiltro = "";
        if (clsLogin.Sucursal != "0001" && sIdJurisdiccion != "" )
        {
            sFiltro = string.Format("And IdJurisdiccion = '{0} 'And IdFarmacia = '{1}'", sIdJurisdiccion, clsLogin.Sucursal);
        }

        //string sSql = string.Format(   "Select IdFarmacia, Farmacia, Farmacia, (IdFarmacia + '  -- ' + Farmacia) NombreFarmacia From vw_Farmacias (NoLock) " +
        //                        "Where IdEstado = '{0}' And Status = 'A' " +
        //                                "And IdFarmacia > 1005 And IdJurisdiccion = '{1}' {2} " +
        //                        "Order by IdFarmacia",
        //                    EstadoConectado, sIdJurisdiccion, "And IdFarmacia <> 1182"); //Leer dato de si se debe mostrar CEDIS


        //string sSql = string.Format("Select IdFarmacia, Farmacia, Farmacia, (IdFarmacia + '  -- ' + Farmacia) NombreFarmacia From vw_Farmacias (NoLock) " +
        //                        "Where IdEstado = '{0}' And Status = 'A' " +
        //                                "And IdFarmacia > {3} And IdJurisdiccion = '{1}' {2} " +
        //                        "Order by IdFarmacia",
        //                    EstadoConectado, sIdJurisdiccion, sFiltro, sFiltroUnidad);

        string sSql = string.Format("Select IdFarmacia, Farmacia, Farmacia, (IdFarmacia + '  -- ' + Farmacia) NombreFarmacia From vw_Farmacias (NoLock) " +
                                "Where IdEstado = '{0}' And Status = 'A' " +
                                        "And IdFarmacia > {1}  {2} " +
                                "Order by IdFarmacia",
                            EstadoConectado, sFiltroUnidad, sFiltro);

        if (!leer.Exec(sSql))
        {
            // Grabar Error 
        }
        else
        {
            dtsRetorno = leer.DataSetClase;
        }
        return dtsRetorno;
    }
    public static DataSet FarmaciasUrl() 
    { 
        DataSet dtsReturn = new DataSet("urlFarmacias");
        string sSql = string.Format("Select Distinct U.IdFarmacia, (U.IdFarmacia + ' - ' + U.Farmacia) as Farmacia, U.UrlFarmacia, C.Servidor  " +
                                 "From vw_Farmacias_Urls U (NoLock) " +
                                 "Inner Join CFGS_ConfigurarConexiones C (NoLock) On ( U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia ) " +
                                 "Where U.IdEstado = '{0}' " +
                                 "and U.FarmaciaStatus = 'A' and U.StatusRelacion = 'A' ", EstadoConectado);
        dtsReturn = ExecQuery(sSql, "urlFarmacias");
        return dtsReturn;
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
                                        "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And G.NombreGrupo Like '{2}'", sEstadoConectado, Sucursal, sFiltro);
        if (leer.Exec(sSql))
        {
            leer.RenombrarTabla(1, "GruposMiembros");
            dtsGruposMiembros = leer.DataSetClase;
        }

        sSql = string.Format("Select IdGrupo, NombreGrupo From Net_CTE_Grupos_De_Usuarios (NoLock) " +
                            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFiltro);
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", sEstadoConectado, Sucursal, sFiltro);

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
    public static void DatosDeConexion()
    {
        string filePath = General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // 
        string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
        int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;

        try
        {
            filePath = BaseDir.Substring(0, lenWebServiceName); //"Config.ini"; 
            
            sRutaDeReportes = filePath.ToString();
            
            StreamReader objReader = new StreamReader(filePath + @"\DatosConexion.ini");
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
            sEstadoConectado = SplitValor(arrText[1].ToString(), '=', 1);
            sSucursal = SplitValor(arrText[2].ToString(), '=', 1);
            sArbol = SplitValor(arrText[3].ToString(), '=', 1);
            sNombreModulo = SplitValor(arrText[4].ToString(), '=', 1);
            sNombreEstado = SplitValor(arrText[5].ToString(), '=', 1);
            bMostraCedis = Convert.ToBoolean(SplitValor(arrText[6].ToString(), '=', 1));
            sIdCliente = SplitValor(arrText[7].ToString(), '=', 1);
            sIdSubCliente = SplitValor(arrText[8].ToString(), '=', 1);
            bEncPersonalizado = Convert.ToBoolean(SplitValor(arrText[9].ToString(), '=', 1));
            sEncabezado = SplitValor(arrText[10].ToString(), '=', 1);
            sIdAlmacen = SplitValor(arrText[11].ToString(), '=', 1);
            bFechaCompleta = Convert.ToBoolean(SplitValor(arrText[12].ToString(), '=', 1));
            bUsaPedidos = Convert.ToBoolean(SplitValor(arrText[13].ToString(), '=', 1));
            bCteSeguroPopular = Convert.ToBoolean(SplitValor(arrText[14].ToString(), '=', 1));
            iColorR = Convert.ToInt32(SplitValor(arrText[15].ToString(), '=', 1));
            iColorG = Convert.ToInt32(SplitValor(arrText[16].ToString(), '=', 1));
            iColorB = Convert.ToInt32(SplitValor(arrText[17].ToString(), '=', 1));
            txtColor = System.Drawing.ColorTranslator.FromHtml(SplitValor(arrText[18].ToString(), '=', 1));
            sArbolConfiguracion = SplitValor(arrText[19].ToString(), '=', 1);
            bUsaBI = Convert.ToBoolean(SplitValor(arrText[20].ToString(), '=', 1));
            sServidorBI = SplitValor(arrText[21].ToString(), '=', 1);
        }
        catch { } 
    }

    public static void queryPersolizado()
    {
        try
        {
            cfgQuery = JsonConvert.DeserializeObject<clsReportes.Reportes>(File.ReadAllText(sRutaDeReportes + @"\queryPersolanizado.ini"));
        }
        catch
        { 
        }
    }
    
    public static bool ExisteVariable(object oVariable)
    { 
        bool bRegresa= false;
        try
        {
            if (oVariable != null)
            {
                bRegresa = true;
            }
        }
        catch
        {
            bRegresa = false;
        }
        return bRegresa;
    }

    public static void CargarParametros()
    {
        string sSql = string.Format("Select ArbolModulo, NombreParametro, Valor, Descripcion  " + // EsDeSistema
            " From Net_CFGS_Parametros (NoLock) " +
            " Where ArbolModulo =  '{0}' ", sArbol);
        if (!leer.Exec(sSql))
        {
            // Error
        }
        else
        {
            dtsParametros = leer.DataSetClase;
        }
    }
    
    public static void GetClaves()
    {
        //leer.DataSetClase = Consultas.ClavesSSA_Sales("GetClaves_ClienteWeb"); 
        string sFiltro = "";
        if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
        {
            sFiltro = string.Format("And CB.IdFarmacia = '{0}'", HttpContext.Current.Session["IdSucursal"].ToString());
        }

        //string sSql = string.Format("Select Distinct(C.IdClaveSSA_Sal), C.ClaveSSA, C.DescripcionCortaClave As Descripcion, C.Presentacion, C.ContenidoPaquete As Contenido From vw_Claves_Precios_Asignados A (NoLock) " +
        //                            "Inner Join vw_ClavesSSA_Sales C (NoLock) On (A.IdClaveSSA = C.IdClaveSSA_Sal)" +
        //                            "Where A.IdEstado ='{0}' And A.Precio > 0", EstadoConectado);
        //sSql = string.Format("Select Distinct(C.IdClaveSSA_Sal), C.ClaveSSA, C.DescripcionCortaClave As Descripcion, C.Presentacion, C.ContenidoPaquete As Contenido From vw_Claves_Precios_Asignados A (NoLock) " +
        //                        "Inner Join vw_ClavesSSA_Sales C (NoLock) On (A.IdClaveSSA = C.IdClaveSSA_Sal)" +
        //                        "Where A.IdEstado ='{0}' And A.Precio > 0", "21");

        string sSql = string.Format("Select Distinct(C.IdClaveSSA_Sal), C.ClaveSSA, C.DescripcionCortaClave As Descripcion, C.Presentacion, C.ContenidoPaquete As Contenido From vw_CB_CuadroBasico_Farmacias CB (NoLock) " +
                                "Inner Join vw_ClavesSSA_Sales C (NoLock) On (CB.IdClaveSSA = C.IdClaveSSA_Sal) " +
                                "Where CB.IdEstado = '{0}' {1} ", EstadoConectado, "");

        leer.DataSetClase = ExecQuery(sSql);
        if(leer.Leer())
        {
            //dtsClavesSSA_Sales = leer.DataSetClase;
            HttpContext.Current.Session["dtsClavesSSA_Sales"] = leer.DataSetClase;
        }
    }

    public static string GetValor(string Parametro)
    {
        string sRegresa = "";
        string sDatos = string.Format("ArbolModulo = '{0}' and NombreParametro = '{1}' ", sArbol, Parametro);

        DataRow[] dt = dtsParametros.Tables[0].Select(sDatos);
        if (dt.Length > 0)
        {
            sRegresa = Convert.ToString(dt[0]["Valor"].ToString());
        }

        return sRegresa;
    }

    static public DataSet ExecQuery(string sSql)
    {
        DataSet dtsReturn = new DataSet();
        if (leer.Exec(sSql))
        {
            //if (leer.Leer())
            {
                dtsReturn = leer.DataSetClase;
            }
        }
        return dtsReturn;
    }
    
    static public DataSet ExecQuery(string sSql, string sNombreTabla)
    {
        DataSet dtsReturn = new DataSet();
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

    static public bool ExecQueryBool(string sSql)
    {
        bool bReturn = false;
        DataSet dtsReturn = new DataSet();
        if (leer.Exec(sSql))
        {
            if (leer.Leer())
            {
                bReturn= true;
            }
        }
        return bReturn;
    }

    static public string dtsFiltro(DataSet dtsGeneral, string sTabla,string sFiltro, string sCampo)
    {
        string sReturn = "";
        leer.DataRowsClase = dtsGeneral.Tables[sTabla].Select(sFiltro);
        if (leer.Leer())
        {
            sReturn = leer.Campo(sCampo);
        }
        return sReturn;
    }

    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();
        
        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;

            sTHead.AppendFormat("<th>{0}</th>", sTextColum);

        }
        sTHead.Append("</thead>");
        if (leer.Leer())
        {

            leer.RenombrarTabla(1, sIdTabla);

            dtsTabla = leer.DataSetClase;
            sTBody.Append("<tbody>");
            foreach (DataRow Row in dtsTabla.Tables[sIdTabla].Rows)
            {
                sTBody.Append("<tr>");
                foreach (object item in Row.ItemArray)
                {
                    if (item is DateTime)
                    {
                        sTBody.AppendFormat("<td>{0}</td>", General.FechaYMD((DateTime)item));
                    }
                    else
                    {
                        sTBody.AppendFormat("<td>{0}</td>", item);
                    }
                }
                sTBody.Append("</tr>");
            }
            sTBody.Append("</tbody>");
        }
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        return sTabla.ToString();
    }

    static public string DtsToTableHtml(DataTable dtGeneral, string sIdTabla)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataTableClase = dtGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();

        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;

            sTHead.AppendFormat("<th>{0}</th>", sTextColum);

        }
        sTHead.Append("</thead>");
        if (leer.Leer())
        {

            leer.RenombrarTabla(1, sIdTabla);

            dtsTabla = leer.DataSetClase;
            sTBody.Append("<tbody>");
            foreach (DataRow Row in dtsTabla.Tables[sIdTabla].Rows)
            {
                sTBody.Append("<tr>");
                foreach (object item in Row.ItemArray)
                {
                    if (item is DateTime)
                    {
                        sTBody.AppendFormat("<td>{0}</td>", General.FechaYMD((DateTime)item));
                    }
                    else
                    {
                        sTBody.AppendFormat("<td>{0}</td>", item);
                    }
                }
                sTBody.Append("</tr>");
            }
            sTBody.Append("</tbody>");
        }
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        return sTabla.ToString();
    }

    static public string DtsToTableHtml(DataTable dtGeneral, string sIdTabla, string[,] sColumnasNombres)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataTableClase = dtGeneral;
        int iCol = 0;
        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();
        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;

            if (sTextColum == sColumnasNombres[iCol, 0])
            {
                sTextColum = sColumnasNombres[iCol, 1];
            }
            iCol++;

            sTHead.Append(string.Format("<th>{0}</th>", sTextColum));

        }
        sTHead.Append("</thead>");

        
        if (leer.Leer())
        {

            leer.RenombrarTabla(1, sIdTabla);

            dtsTabla = leer.DataSetClase;
            sTBody.Append("<tbody>");
            foreach (DataRow Row in dtsTabla.Tables[sIdTabla].Rows)
            {
                sTBody.Append("<tr>");
                foreach (object item in Row.ItemArray)
                {
                    if (item is DateTime)
                    {
                        sTBody.AppendFormat("<td>{0}</td>", General.FechaYMD((DateTime)item));
                    }
                    else
                    {
                        sTBody.AppendFormat("<td>{0}</td>", item);
                    }
                }
                sTBody.Append("</tr>");
            }
            sTBody.Append("</tbody>");
        }
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        return sTabla.ToString();
    }

    static public string DtsToTableHtml(string sSql, string sIdTabla)
    {
        string sTabla = "";
        DataSet dtsTabla = new DataSet(sIdTabla);

        if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                string sTextColum = value;

                sTHead += string.Format("<th>{0}</th>", sTextColum);

            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;

                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        if (item is DateTime)
                        {
                            sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }


    static public string DtsToTableHtml(string sSql, string sIdTabla, string[,] sColumnasNombres)
    {
        string sTabla = "";
        int iCol = 0;
        DataSet dtsTabla = new DataSet(sIdTabla);

        if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                string sTextColum = value;

                if (sTextColum == sColumnasNombres[iCol,0])
                {
                    sTextColum = sColumnasNombres[iCol, 1];
                }
                iCol++;

                sTHead += string.Format("<th>{0}</th>", sTextColum);
                
            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;

                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        if (item is DateTime)
                        {
                            sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }
    /// <summary>
    /// Funcion que realiza un split a un cadena de texto en base a un caracter separador, regresa el valor en la posición
    /// especificada.
    /// </summary>
    /// <param name="sValor">Cadena a realizar split</param>
    /// <param name="sSeparador">Caracter separador</param>
    /// <param name="iPosicionValor">Posicion que se desea obtener del split realizado (Esta inicia en 0)</param>
    /// <returns>Cadena con el resultado del split</returns>
    private static string SplitValor(string sValor, char sSeparador, int iPosicionValor)
    {
        string[] sCadena = sValor.Split(sSeparador);
        return sCadena[iPosicionValor];
    }

    //
    public static string GetInfo(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
    {
        string sEstructura = string.Empty;
        string sOpcionAdministrador = string.Empty;
        string sQuery = null;
        string[] sColumnas= new string[] {} ;
        //DataTable dtData = DtGeneral.InformacionGeneral.Tables[0];
        //DataTable dtData = ((DataSet)HttpContext.Current.Session["dtsInfo"]).Tables[0];
        DataTable dtData = ((DataSet)HttpContext.Current.Session["CatalogoGeneral"]).Tables[0];

        //if (clsLogin.Sucursal == "0001")
        if (HttpContext.Current.Session["IdSucursal"].ToString() == "0001")
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
        
            if(!bUnique){
                
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

        sEstructura += DtGeneral.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);
        
        return sEstructura;
    }

    public static string SerializerDataSet(DataSet dtsInfo)
    {

        DataTable dtTable = new DataTable();
        Dictionary<string, object> lstTables = new Dictionary<string, object>();
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        string sReturn = "";

        if (dtsInfo.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dtsInfo.Tables.Count; i++)
            {

                dtTable = dtsInfo.Tables[i];
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;

                foreach (DataRow dr in dtTable.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtTable.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                lstTables.Add(dtTable.TableName.ToString(), rows);
            }

            sReturn = jsSerializer.Serialize(lstTables);
        }

        return sReturn;
    }

    public static string EscapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
    //

    public static string CreateDropList(string sId, string sClass, DataSet dtsData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        sEstructura += ">";

        sEstructura += OptionDropList(dtsData, sValue, sText, AddSelect);

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string CreateDropList(string sId, string sClass)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        sEstructura += ">";

        sEstructura += "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>";

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>";
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataTable dtData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;
        string sValues = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>";
        }

        leer.DataTableClase = dtData;

        while (leer.Leer())
        {

           sEstructura += string.Format("<option value=\"{0}\">{0} -- {1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, bool AddSelect, string sValueSelected)
    {
        string sEstructura = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>";
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            if (leer.Campo(sValue) == sValueSelected) 
            {
                sEstructura += string.Format("<option value=\"{0}\" selected>{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
            else
            {
                sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, string sValueFirstOption, string sTextFirstOption)
    {
        string sEstructura = string.Empty;

        if (sValueFirstOption != "" && sTextFirstOption != "")
        {
            sEstructura = string.Format("<option value=\"{0}\">{1}</option>", sValueFirstOption, sTextFirstOption);
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string GetAutoFormat(Type type)
    {
        string format = "General";
        if (type == typeof(int))
            format = "0";
        else if (type == typeof(uint))
            format = "0";
        else if (type == typeof(long))
            format = "0";
        else if (type == typeof(ulong))
            format = "0";
        else if (type == typeof(short))
            format = "0";
        else if (type == typeof(ushort))
            format = "0";
        else if (type == typeof(double))
            format = "0.00";
        else if (type == typeof(float))
            format = "0.00";
        else if (type == typeof(decimal))
            //format = NumberFormatInfo.CurrentInfo.CurrencySymbol + " #,##0.00";
            format = "#,##0.00";
        else if (type == typeof(DateTime))
            format = "yyyy-MM-dd";
        else if (type == typeof(string))
            format = "@";
        else if (type == typeof(bool))
            format = "\"" + bool.TrueString + "\";\"" + bool.TrueString + "\";\"" + bool.FalseString + "\"";

        return format;

    }

    #region consultas

    public static DataSet Folio_Pedidos_CEDIS_Enc(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion, bool bConexionCedis)
    {
        DataSet myDataset = new DataSet();
        string sQuery = string.Format("Set DateFormat YMD Select * " +
            " From vw_Impresion_Pedidos_Cedis (NoLock) " +
            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
            IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

        if (bConexionCedis)
        {
            clsLeer myleer = new clsLeer(ref cnnCedis);
            myleer.Exec(sQuery);
            myDataset = myleer.DataSetClase;
        }
        else
        {
            myDataset = ExecQuery(sQuery);
        }

        return myDataset;
    }

    public static DataSet Folio_Pedidos_CEDIS_Det(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioPedido, string Funcion, bool bConexionCedis)
    {
        DataSet myDataset = new DataSet();
        //string sQuery = string.Format("Set DateFormat YMD Select ClaveSSA, IdClaveSSA, DescripcionClave, Presentacion , ContenidoPaquete, CantidadEnCajas, Cantidad " +
        //    " From vw_Impresion_Pedidos_Cedis (NoLock) " +
        //    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
        //    IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

        string sQuery = string.Format("Set DateFormat YMD "  +
                                    "Select " +
                                    //"0 As No, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 0 As ContenidoCajas, 0 As CantidadPiezas 
                                    " '' As No, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, CantidadEnCajas, Cantidad " +
                                    " From vw_Impresion_Pedidos_Cedis (NoLock) " +
                                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Folio = '{3}' ",
                                    IdEmpresa, IdEstado, IdFarmacia, Fg.PonCeros(FolioPedido, 6));

        if (bConexionCedis)
        {
            clsLeer myleer = new clsLeer(ref cnnCedis);
            myleer.Exec(sQuery);
            myDataset = myleer.DataSetClase;
        }
        else
        {
            myDataset = ExecQuery(sQuery);
        }

        return myDataset;
    }

    public static DataSet GetArbolNavegacion(string sArbol)
    {
        DataSet dtsReturn = new DataSet();
        string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sArbol, HttpContext.Current.Session["Personal"].ToString());

        dtsReturn = ExecQuery(sSql, "Arbol");
        return dtsReturn;
    }

    public static string GetUsers()
    {
        string sEstructura = string.Empty;
        string sSql = string.Empty;

        sSql = string.Format("Select IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status ='A'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());
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

    #endregion consultas

    #endregion Funciones Públicas

    #region Propiedades publicas
    public static DataSet Permisos
    {
        get { return dtsPermisos.Copy(); }
        set { dtsPermisos = value; }
    }

    public static clsConexionSQL DatosConexion
    {
        get { return cnn; }
    }

    public static clsConexionSQL DatosConexionCedis
    {
        get { return cnnCedis; }
    }

    public static clsDatosConexion clsDatosConexionCedis
    {
        get { return GetConexionCedis("SII-Cedis"); }
    }
    public static string FiltroUnidad
    {
        get { return sFiltroUnidad; }
    }

    public static string Empresa
    {
        get { return sEmpresa; }
        set { sEmpresa = value.ToUpper(); }
    }

    public static string NombreEmpresa
    {
        get { return sNombreEmpresa; }
    }

    public static string Arbol
    {
        get { return sArbol; }
    }

    public static bool Autenticado
    {
        get { return bAutenticado; }
        set { bAutenticado = value; }
    }

    public static string EstadoConectado
    {
        get { return sEstadoConectado; }
    }

    public static string NombreEstado {
        get { return sNombreEstado.ToUpper(); }
    }

    public static string IdAlmacen
    {
        get { return sIdAlmacen; }
    }


    public static string LoginUser
    {
        get { return sLoginUser; }
        set { sLoginUser = value; }
    }
    
    public static string Jurisdiccion
    {
        get { return sJurisdiccion; }
        set { sJurisdiccion = value; }
    }

    public static string Municipio
    {
        get { return sMunicipio; }
        set { sMunicipio = value; }
    }

    public static string Sucursal
    {
        get { return sSucursal; }
        set { sSucursal = value; }
    }

    public static string RutaReportes
    {
        get { return sRutaDeReportes; }
        set { sRutaDeReportes = value; }
    }

    public static string Password
    {
        get { return sPassword; }
        set { sPassword = value; }
    }

    public static string RutaPlantillas
    {
        get { return sRutaDeReportes + @"\PLANTILLAS\"; }
    }

    public static string RutaPlantillasWeb
    {
        get { return sRutaDeReportes + @"\PLANTILLAS_WEB\"; }
    }

    public static string RutaReportesCR
    {
        get { return sRutaDeReportes + @"\REPORTES\\"; }
    }

    public static DataSet Estados
    {
        get { return dtsEstados.Copy(); }
    }

    public static DataSet Jurisdicciones
    {
        get { return dtsJurisdicciones.Copy(); }
    }

    public static string TipoDeUnidad
    {
        get { return sTipoDeUnidad; }
        set { sTipoDeUnidad = value; }
    }

    public static string NombreModulo
    {
        get { return sNombreModulo; }
    }

    public static bool MostraCedis {
        get { return bMostraCedis; }
    }
    public static string IdCliente {
        get { return sIdCliente; }
    }
    public static string Cliente {
        get { return sCliente; }
    }
    public static string IdSubCliente
    {
        get { return sIdSubCliente; }
    }
    public static string SubCliente
    {
        get { return sSubCliente; }
    }
    public static DataSet InformacionGeneral
    {
        get { return dtsInfo.Copy(); }
    }
    public static clsConsultas ExecConsulta
    {
        get { return Consultas; }
    }
    public static bool EncabezadoPersonalizado
    {
        get { return bEncPersonalizado; }
    }
    public static string EncabezadoReporteExcel
    {
        get { return sEncabezado; }
    }
    public static DataSet urlFarmacias
    {
        get {
            if (HttpContext.Current.Session["urlFarmacias"] == null)
            {
                HttpContext.Current.Session["urlFarmacias"] = FarmaciasUrl();
            }
            return (DataSet)HttpContext.Current.Session["urlFarmacias"];
        }
    }
    public static bool FechaCompleta
    {
        get { return bFechaCompleta; }
    }
    public static bool UsaPedidos
    {
        get { return bUsaPedidos; }
    }
    public static bool CteSeguroPopular
    {
        get { return bCteSeguroPopular; }
    }
    public static Color ColorDeTextoHexadecimal
    {
        get { return txtColor; }
    }
    public static Color ColorBgReportes
    {
        get { return Color.FromArgb(iColorR, iColorG, iColorB); }
    }
    public static clsReportes.Reportes queryReporte
    {
        get { return cfgQuery; }
    }
    public static string ArbolConfiguracion
    {
        get { return sArbolConfiguracion; }
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