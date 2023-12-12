using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

public partial class DllClienteRegionalWeb_wsGeneral : System.Web.UI.Page
{
    public static basGenerales Fg = new basGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("~/Default.aspx");
    }

    #region Servicios Web
    [WebMethod()]
    public static string Autenticar(string sUsuario, string sPassword)
    {
        string sReturn = string.Empty;

        
        if (clsLogin.AutenticarUsuarioLogin(sUsuario.ToUpper(), sPassword.ToUpper()))
        {
            clsAuditoria.GuardarAud_LoginReg();
        }
        else
        {
            sReturn = "Verifique sus datos";
        }

        if (sReturn != "")
        {
            //sReturn = clsLogin.ErrorAutenticacion;
            sReturn = "Datos de inicio de sesión inválidos";
        }

        return sReturn;
    }
    
    [WebMethod]
    public static string GetInfoGeneral()
    {
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        if (HttpContext.Current.Session["InfoGeneral"] == null)
        {
            DtGeneral.GetInfoUnidad();

            string sFiltro = "";
            string sFiltro_Pro_SubPro = "";
            string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') And EsAlmacen = 0 ";
            string sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005', '006') And C.EsAlmacen = 0 ";
            string sFiltroPerfiles = "";
            bool bFiltroJuris = false;
            bool bFiltroUnidad = false;

            if (DtGeneral.MostrarAlamacenes)
            {
                sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005') ";
            }

            if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] != "0001")
            {
                sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"]);
                sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"]);

                //Se agrego a petición de Gto ed unificar farmacias
                if (DtGeneral.IdEstado == "11" && HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0088")
                {
                    sFiltro = string.Format(" And IdFarmacia In ('0033', '0088', '0130') ");
                }
            }

            string stmpFiltro = DtGeneral.FiltroPerfiles("Jurisdiccion");

            if (stmpFiltro != "")
            {
                sFiltroPerfiles = stmpFiltro;
                bFiltroJuris = true;
            }

            stmpFiltro = DtGeneral.FiltroPerfiles("Unidad");

            if (stmpFiltro != "")
            {
                sFiltroPerfiles += stmpFiltro;
                bFiltroUnidad = true;
            }


            string sSql = string.Format("Select IdTipoUnidad, TipoDeUnidad, IdJurisdiccion, Jurisdiccion, IdMunicipio, Municipio, IdFarmacia, Farmacia From vw_Farmacias (Nolock) " +
                                "Where IdEstado = '{0}' And Status = 'A' {1} {2} {3}", DtGeneral.IdEstado, sFiltroTipoUnidad, sFiltro, sFiltroPerfiles);

            DataSet dtsInfoGeneral = DtGeneral.ExecQuery(sSql, "Info");
            HttpContext.Current.Session["CatalogoGeneral"] = dtsInfoGeneral;

            lst.Add("AyudaFarmacias", clsToolsHtml.SerializerDataSet(dtsInfoGeneral));

            //sSql = string.Format("Select distinct IdFarmacia, Farmacia, IdPrograma, Programa, IdSubPrograma, SubPrograma  From vw_Farmacias_Programas_SubPrograma_Clientes " +
            //                    "Where IdEstado = '{0}'", DtGeneral.IdEstado);

            sSql = string.Format("Select " +
                                    "Distinct P.IdFarmacia, P.Farmacia, P.IdPrograma, P.Programa, " +
                                    "P.IdSubPrograma, P.SubPrograma " +
                                "From vw_Farmacias_Programas_SubPrograma_Clientes P (NoLock) " +
                                "Inner Join vw_Farmacias C On (C.IdEstado = P.IdEstado And C.IdFarmacia = P.IdFarmacia) " +
                                "Where P.IdEstado = '{0}' And C.Status = 'A' {1} {2}", DtGeneral.IdEstado, sFiltroTipoUnidad_Pro_SubPro, sFiltro_Pro_SubPro);

            lst.Add("Programas", clsToolsHtml.SerializerDataSet(DtGeneral.ExecQuery(sSql, "Pro_SubPro")));
            lst.Add("initUser", HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == "0001" ? true : false);
            lst.Add("Empresa", DtGeneral.IdEmpresa);
            lst.Add("Estado", DtGeneral.IdEstado);
            lst.Add("UsaBI", DtGeneral.UsaBI);
            lst.Add("ServidorBI", DtGeneral.ServidorBI);
            lst.Add("FiltroJuris", bFiltroJuris);
            lst.Add("FiltroUnidad", bFiltroUnidad);
            lst.Add("ExpiracionCookie", HttpContext.Current.Request.Cookies["cteRegional"]["Session"]);

            HttpContext.Current.Session["InfoGeneral"] = lst;

        }

        lst = (Dictionary<string, object>)HttpContext.Current.Session["InfoGeneral"];
        return serializer.Serialize(lst);
    }

    [WebMethod]
    public static string ChangePassword(string sPasswordAnterior, string sPasswordNuevo)
    {
        string sMsj = string.Empty;
        string sPass = string.Empty;
        clsCriptografo crypto = new clsCriptografo();

        string sCadena = DtGeneral.IdEstado + HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] + HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"] + sPasswordAnterior.ToUpper();
        sPasswordAnterior = crypto.PasswordEncriptar(sCadena);

        string sPassAnt = getPass();

        if (sPassAnt == sPasswordAnterior)
        {
            var bContinuar = true;
            sCadena = DtGeneral.IdEstado + HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] + HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"] + sPasswordNuevo.ToUpper();
            sPass = crypto.PasswordEncriptar(sCadena);

            string sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6} ",
                DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), HttpContext.Current.Request.Cookies["cteRegional"]["IdPersonal"].ToString(),
                HttpContext.Current.Request.Cookies["cteRegional"]["NombrePersonal"].ToString(), HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"].ToString(), sPass, 1);

            bContinuar = DtGeneral.ExecQueryBool(sSql);

            if (!bContinuar)
            {
                sMsj = "No se pudo cambiar su contraseña, intenténlo nuevamente";
            }
        }
        else
        {
            sMsj = "Contraseña anterior invalida";
        }
        return sMsj;
    }

    [WebMethod]
    public static string AuditorBIRT(string sQuery, string sForm)
    {
        string sReturn = string.Empty;
        
        string sCadena = sQuery.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, sForm);
        
        return sReturn;
    }

    #region Configuracion
    [WebMethod]
    public static string ManageUser(string sIdFarmacia, string sIdPersonal, string sNombrePersonal, string sLogin, string sPassword, int iProcess)
    {
        clsCriptografo crypto = new clsCriptografo();
        string sEstructura = string.Empty;
        string sSql = "";
        string sCadena = "";
        string sPass = "";

        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        clsConexionSQL cnn = DtGeneral.DatosConexion;
        clsLeer leer = new clsLeer(ref cnn);
        bool bContinua = false;

        sCadena = DtGeneral.IdEstado + sIdFarmacia + sLogin.ToUpper() + sPassword.ToUpper();
        sPass = crypto.PasswordEncriptar(sCadena);
        sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6} ",
            DtGeneral.IdEstado, sIdFarmacia, sIdPersonal.ToUpper().Trim(),
            sNombrePersonal.ToUpper().Trim(), sLogin.ToUpper().Trim(), sPass, iProcess);


        lst.Add("bExec", false);

        if (cnn.Abrir())
        {
            cnn.IniciarTransaccion();

            //Encabezado
            if (leer.Exec(sSql))
            {

                if (leer.Leer())
                {
                    lst.Add("Clave", leer.Campo("Clave"));
                    lst.Add("Mensaje", leer.Campo("Mensaje"));

                    bContinua = true;
                }
            }


            //Se ejecuto todo correctamente    
            if (bContinua)
            {
                cnn.CompletarTransaccion();
                lst["bExec"] = true;

                //Agregar permisos si el usuario no pertenece a oficina central

                if (sIdFarmacia != DtGeneral.IdSucursal)
                {
                    sSql = string.Format("Set DateFormat  YMD " +
                                        "Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}' ",
                                        DtGeneral.IdEstado, sIdFarmacia, 1, Fg.PonCeros(leer.Campo("Clave"), 4), sLogin);

                    bContinua = DtGeneral.ExecQueryBool(sSql);
                }

                //Auditor
                sCadena = sSql.Replace("'", "\"");
                clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmUsuario");
            }
            else
            {
                cnn.DeshacerTransaccion();
            }

            cnn.Cerrar();
        }

        sEstructura = serializer.Serialize(lst);

        return sEstructura;
    }

    [WebMethod]
    public static string GetUsers()
    {
        string sEstructura = string.Empty;
        string sSql = string.Empty;

        sSql = string.Format("Select IdFarmacia, IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
                            "Where IdEstado = '{0}' ", DtGeneral.IdEstado);

        sEstructura = clsToolsHtml.DtsToTableHtml(sSql, "tableUsers");

        return sEstructura;
    }

    [WebMethod]
    public static string GetMovtosAuditor(string dtpFechaInicial, string dtpFechaFinal)
    {
        string sEstructura = string.Empty;
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        //"L.IdFarmacia , F.NombreFarmacia , L.IdUsuario, U.Nombre, L.IdSesion, L.FechaRegistro, L.IP_Address As 'IP', M.IdMovto, N.Nombre As Pantalla " +
                                        "L.IdFarmacia As  'Id Farmacia', F.NombreFarmacia As 'Nombre Farmacia', L.IdUsuario As 'Id Usuario', U.Nombre, L.IdSesion As 'Id Sesión', L.FechaRegistro As 'Fecha Registro', L.IP_Address As 'IP', M.IdMovto 'Id Movimiento', N.Nombre As 'Pantalla', M.Instruccion As 'Instrucción', M.Url_Farmacia As 'URL' " +
                                    "From CteReg_Web_Auditoria_Login As L(NoLock) " +
                                    "Left Join CteReg_Web_Auditoria_Movimientos As M (NoLock) On (L.IdEstado = M.IdEstado And L.IdUsuario = M.IdUsuario And L.IdSesion = M.IdSesion) " +
                                    "Inner Join Net_Regional_Usuarios As U (NoLock) On (L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia And L.IdUsuario = U.IdUsuario) " +
                                    "Left Join Net_Navegacion As N (NoLock) On (M.Pantalla = N.FormaLoad) " +
                                    "Inner Join CatFarmacias As F (NoLock) On ( L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia) " +
                                    "Where L.IdEstado = '{0}' And Convert(varchar(10), L.FechaRegistro, 120) Between '{1}' And '{2}' ", DtGeneral.IdEstado, dtpFechaInicial, dtpFechaFinal);


        HttpContext.Current.Session["AuditorMovimientos"] = DtGeneral.ExecQuery(sSql, "AuditorMovimientos");
        string sTabla = clsToolsHtml.DtsToTableHtml((DataSet)HttpContext.Current.Session["AuditorMovimientos"], "tableAuditorMovimientos");
        lst.Add("Tabla", ValidarResultado(sTabla, "AuditorMovimientos"));
        bool bMostrarResultado = ValidarResultado(sTabla);
        lst.Add("MostrarResultado", bMostrarResultado);

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmAuditor_Movimientos");

        return sEstructura;
    }

    [WebMethod]
    public static string GetUser(string sIdFarmacia, string sIdPersonal)
    {
        string sEstructura = string.Empty;
        string sSql = string.Empty;

        sSql = string.Format("Select IdFarmacia, IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdUsuario = '{2}' ", DtGeneral.IdEstado, sIdFarmacia, Fg.PonCeros(sIdPersonal, 4));

        sEstructura = clsToolsHtml.SerializerDataSet(DtGeneral.ExecQuery(sSql, "User"));

        return sEstructura;
    }
    [WebMethod]
    public static string designGroups(string sOption)
    {
        string sEstructura = string.Empty;
        //if (sOption == "A")
        //{
        //    sOption = "G";
        //}
        sOption = string.Format("%{0}%--%", sOption);
        sEstructura = string.Format("<span class=\"labelGpoleft\">Perfil</span>{0}", GruposUsuariosPerfiles(sOption));

        return sEstructura;
    }

    [WebMethod()]
    public static string ManageRel(int iIdGrupo, string sOpcion, string sTipo, string sIdUsuario, string sLoginUser)
    {
        bool bContinua = false;
        string sMensaje = "No se pudo generar la relación intentelo de nuevo.";
        string sQuery = string.Empty;
        clsConexionSQL cnn = DtGeneral.DatosConexion;
        clsLeer leer = new clsLeer(ref cnn);

        if (cnn.Abrir())
        {
            cnn.IniciarTransaccion();

            if (sOpcion == "Miembros")
            {
                if (sTipo == "Add")
                {
                    sQuery = string.Format("Set DateFormat  YMD " +
                                        "Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}' ", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), iIdGrupo, Fg.PonCeros(sIdUsuario, 4), sLoginUser);

                    if (iIdGrupo != 3 && iIdGrupo != 15)
                    {
                        sQuery += string.Format(" Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}'", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), 3, Fg.PonCeros(sIdUsuario, 4), sLoginUser);
                    }
                }
                else if (sTipo == "Del")
                {
                    sQuery = string.Format("Set DateFormat YMD " +
                                            "Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                                            "Where IdEstado = {0} and IdFarmacia = {1} and IdGrupo = {2} and IdUsuario = '{3}' ", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), iIdGrupo, Fg.PonCeros(sIdUsuario, 4));

                    if (iIdGrupo != 3 && iIdGrupo != 15)
                    {
                        sQuery += string.Format(" Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                                            "Where IdEstado = {0} and IdFarmacia = {1} and IdGrupo = {2} and IdUsuario = '{3}'", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), 3, Fg.PonCeros(sIdUsuario, 4));
                    }
                    //else if (iIdGrupo == 3)
                    //{
                    //    sQuery = string.Format(" Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                    //                        "Where IdEstado = {0} and IdFarmacia = {1} and IdUsuario = '{2}'", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), Fg.PonCeros(sIdUsuario, 4));
                    //}
                }
            }
            else
            {
                sQuery = "";
            }

            //Ejecutar Query
            if (leer.Exec(sQuery))
            {
                bContinua = true;
            }

            //Se ejecuto todo correctamente    
            if (bContinua)
            {
                cnn.CompletarTransaccion();
                sMensaje = "";
                string sCadena = sQuery.Replace("'", "\"");
                clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmGruposDeUsuario");
            }
            else
            {
                cnn.DeshacerTransaccion();
                sMensaje = "No se pudo generar la relación intentelo de nuevo.";
            }

            cnn.Cerrar();
        }

        return sMensaje;
    }
    #endregion Configuracion
    
    #endregion Servicios Web

    #region Funciones privadas
    private static string getPass()
    {
        string sPassword = string.Empty;
        string sSql = string.Empty;
        clsLeer myLeerPass = new clsLeer();

        sSql = string.Format("Set DateFormat YMD " +
                            "Select * From  Net_Regional_Usuarios" +
                            " Where IdEstado = '{0}' and IdFarmacia = '{1}' and IdUsuario = '{2}' ",
                            DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"], HttpContext.Current.Request.Cookies["cteRegional"]["IdPersonal"]);

        myLeerPass.DataSetClase = DtGeneral.ExecQuery(sSql, "sInfoUser");

        if (myLeerPass.Leer())
        {
            sPassword = myLeerPass.Campo("Password");
        }

        return sPassword;
    }
    
    private static string GruposUsuariosPerfiles(string sFiltro)
    {
        string sEstructura = string.Empty;
        string sIdGrupo = string.Empty;
        string sSql = string.Empty;
        string sUllista = string.Empty;

        DataSet dtsGruposMiembros = new DataSet("GruposMiembros");

        clsLeer UlListas = new clsLeer();
        clsConexionSQL cnn = DtGeneral.DatosConexion;
        clsLeer leer = new clsLeer(ref cnn);

        sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        "G.IdGrupo, G.NombreGrupo, U.IdUsuario, U.LoginUser " +
                                    "From Net_CTE_Grupos_De_Usuarios As G (NoLock) " +
                                    "Inner Join Net_CTE_Grupos_Usuarios_Miembros As U (NoLock) On ( G.IdEstado = U.IdEstado And G.IdFarmacia = U.IdFarmacia And G.IdGrupo = U.IdGrupo  And U.Status = 'A' ) " +
                                    //"Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And G.NombreGrupo Like '{2}'", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFiltro);
                                    "Where G.IdEstado = '{0}' And G.IdFarmacia = '{1}' And G.Status = 'A' And G.NombreGrupo Like '{2}'", DtGeneral.IdEstado, DtGeneral.IdSucursal, sFiltro);
        if (leer.Exec(sSql))
        {
            leer.RenombrarTabla(1, "GruposMiembros");
            dtsGruposMiembros = leer.DataSetClase;
        }

        sSql = string.Format("Select IdGrupo, NombreGrupo From Net_CTE_Grupos_De_Usuarios (NoLock) " +
                            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", sEstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFiltro);
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status = 'A' And NombreGrupo Like '{2}' Order By IdGrupo", DtGeneral.IdEstado, DtGeneral.IdSucursal, sFiltro);

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

    private static string ValidarResultado(string sTabla, string sIdTabla)
    {
        string sMensaje = string.Empty;
        if (sTabla.Length >= 102400)
        {
            sMensaje = "El resultado de la consulta excede el límite de registros permitidos para mostrar. Pero su reporte esta listo para ser descargado.";
            sTabla = string.Format("<table id=\"{0}\" class=\"display\" cellspacing=\"0\">" +
                                        "<thead>" +
                                            "<tr>" +
                                                "<th>Mensaje</th>" +
                                            "</tr>" +
                                        "</thead>" +
                                        "<tbody>" +
                                            "<tr>" +
                                                "<td>{1}</td>" +
                                            "</tr>" +
                                        "</tbody>" +
                                    "</table>", sIdTabla, sMensaje);
        }
        return sTabla;
    }

    private static bool ValidarResultado(string sTabla)
    {
        Boolean bReturn = true;

        if (sTabla.Length >= 102400)
        {
            bReturn = false;
        }
        return bReturn;
    }
    #endregion Funciones privadas
}