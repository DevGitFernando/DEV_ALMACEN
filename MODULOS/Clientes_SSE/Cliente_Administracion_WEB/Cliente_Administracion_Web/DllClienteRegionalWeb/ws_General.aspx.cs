using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using wsConexion;

public partial class DllClienteRegionalWeb_ws_General : BasePage
{
    public static basGenerales Fg = new basGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~");
    }

    #region Catalogos
    [WebMethod]
    public static string GetInfo(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia)
    {
        string sEstructura = string.Empty;
        sEstructura = DtGeneral.GetInfo(sOpcion, sUnidad, sJurisdiccion, sMunicipio, sFarmacia, false, false);
        return sEstructura;
    }

    [WebMethod]
    public static string GetInfoGeneral()
    {
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        if (HttpContext.Current.Session["InfoGeneral"] == null)
        {
            DtGeneral.GetInfoUnidad();
            DtGeneral.GetJurisdicciones(DtGeneral.EstadoConectado);

            if (DtGeneral.UsaPedidos)
            {
                //Cargar Claves SSA Sales
                DtGeneral.GetClaves();
            }

            string sFiltro = "";
            string sFiltro_Pro_SubPro = "";
            string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') And EsAlmacen = 0 ";
            string sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005', '006') And C.EsAlmacen = 0 ";
            string sFiltroPerfiles = "";
            bool bFiltroJuris = false;
            bool bFiltroUnidad = false;

            if (DtGeneral.MostraCedis)
            {
                sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                sFiltroTipoUnidad_Pro_SubPro = " And C.IdTipoUnidad Not In ('000', '005') ";
            }

            if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
            {
                sFiltro = string.Format(" And  IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                sFiltro_Pro_SubPro = string.Format(" And  P.IdFarmacia= '{0}' ", HttpContext.Current.Session["IdSucursal"].ToString());
                
                //Se agrego a petición de Gto ed unificar farmacias
                if (DtGeneral.EstadoConectado == "11" && HttpContext.Current.Session["IdSucursal"].ToString() == "0088")
                {
                    sFiltro = string.Format(" And IdFarmacia In ('0033', '0088', '0130') ");
                }
            }

            string stmpFiltro = DtGeneral.FiltroPerfiles("Jurisdiccion");
            
            if(stmpFiltro != "")
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
                                "Where IdEstado = '{0}' And Status = 'A' {1} {2} {3}", DtGeneral.EstadoConectado, sFiltroTipoUnidad, sFiltro, sFiltroPerfiles);

            DataSet dtsInfoGeneral = DtGeneral.ExecQuery(sSql, "Info");
            HttpContext.Current.Session["CatalogoGeneral"] = dtsInfoGeneral;
            
            lst.Add("UsaPedidos", DtGeneral.UsaPedidos);
            lst.Add("FechaCompleta", DtGeneral.FechaCompleta);
            lst.Add("AyudaFarmacias", DtGeneral.SerializerDataSet(dtsInfoGeneral));
            
            //sSql = string.Format("Select distinct IdFarmacia, Farmacia, IdPrograma, Programa, IdSubPrograma, SubPrograma  From vw_Farmacias_Programas_SubPrograma_Clientes " +
            //                    "Where IdEstado = '{0}'", DtGeneral.EstadoConectado);
            
            sSql = string.Format("Select " +
		                            "Distinct P.IdFarmacia, P.Farmacia, P.IdPrograma, P.Programa, " +
		                            "P.IdSubPrograma, P.SubPrograma " +
	                            "From vw_Farmacias_Programas_SubPrograma_Clientes P (NoLock) " +
	                            "Inner Join vw_Farmacias C On (C.IdEstado = P.IdEstado And C.IdFarmacia = P.IdFarmacia) " +
                                "Where P.IdEstado = '{0}' And C.Status = 'A' {1} {2}", DtGeneral.EstadoConectado, sFiltroTipoUnidad_Pro_SubPro, sFiltro_Pro_SubPro);
            
            lst.Add("Programas", DtGeneral.SerializerDataSet(DtGeneral.ExecQuery(sSql, "Pro_SubPro")));
            lst.Add("initUser", HttpContext.Current.Session["IdSucursal"].ToString() == "0001" ? "true" : "false");
            lst.Add("ProcesoPorMes", false);
            lst.Add("Empresa",DtGeneral.Empresa);
            lst.Add("Estado", DtGeneral.EstadoConectado);
            lst.Add("UsaBI", DtGeneral.UsaBI);
            lst.Add("ServidorBI", DtGeneral.ServidorBI);
            lst.Add("FiltroJuris", bFiltroJuris);
            lst.Add("FiltroUnidad", bFiltroUnidad);

            HttpContext.Current.Session["InfoGeneral"] = lst;
            
        }
        lst = (Dictionary<string, object>)HttpContext.Current.Session["InfoGeneral"];
        return serializer.Serialize(lst);
    }

    [WebMethod]
    public static string Municipios(string Identificador)
    {
        string sEstructura = string.Empty;
        if (clsLogin.Sucursal == "0001")
        {
            sEstructura = "<option value=\"*\" selected>Todos los Municipios</option>";
        }
        
        sEstructura += DtGeneral.OptionDropList(DtGeneral.CargarMunicipios(Identificador), "IdMunicipio", "Municipio", false);

        return sEstructura;
    }

    [WebMethod]
    public static string CargarFarmacia(string sTipoUnidad, string sJurisdicciones, string sLocalidad)
    {
        string sEstructura = string.Empty;
        
        if (clsLogin.Sucursal == "0001")
        {
            sEstructura = "<option value=\"*\" selected>Todas las Farmacias</option>";
        }

        sEstructura = DtGeneral.GetInfo("Farmacia", sTipoUnidad, sJurisdicciones, sLocalidad, "*", false, false);
        return sEstructura;
    }

    [WebMethod]
    public static string Farmacia(string Identificador)
    {
        string sEstructura = string.Empty;
        
        if (clsLogin.Sucursal == "0001")
        {
            sEstructura = "<option value=\"*\" selected>Todas las Farmacias</option>";
        }

        sEstructura += DtGeneral.OptionDropList(DtGeneral.GetFarmacias(Identificador), "IdFarmacia", "NombreFarmacia", false);

        return sEstructura;
    }

    [WebMethod]
    public static string ChangePassword(string sPasswordAnterior, string sPasswordNuevo)
    {
        clsCriptografo crypto = new clsCriptografo();
        string sEstructura = string.Empty;
        string sSql = "";
        string sCadena = "";
        string sPass = "";

        if (sPasswordAnterior == HttpContext.Current.Session["PasswordLogin"].ToString())
        {
            sCadena = DtGeneral.EstadoConectado + HttpContext.Current.Session["IdSucursal"].ToString() + HttpContext.Current.Session["Personal"].ToString() + sPasswordNuevo.ToUpper();
            sPass = crypto.PasswordEncriptar(sCadena);
            sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6} ",
                DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), HttpContext.Current.Session["IdPersonal"].ToString(),
                HttpContext.Current.Session["NombrePersonal"].ToString(), HttpContext.Current.Session["Personal"].ToString(), sPass, 1);

            if (DtGeneral.ExecQueryBool(sSql))
            {
                sEstructura = "true";
                HttpContext.Current.Session["PasswordLogin"] = sPasswordNuevo;
            }
            else
            {
                sEstructura = "false";
            }
        }
        else 
        {
            sEstructura = "Password anterior incorrecto, verifique.";
        }
        return sEstructura;
    }

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

        //sCadena = DtGeneral.EstadoConectado + HttpContext.Current.Session["IdSucursal"].ToString() + sLogin.ToUpper() + sPassword.ToUpper();
        sCadena = DtGeneral.EstadoConectado + sIdFarmacia + sLogin.ToUpper() + sPassword.ToUpper();
        sPass = crypto.PasswordEncriptar(sCadena);
        sSql = string.Format("Exec spp_Mtto_Net_Regional_Usuarios '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6} ",
            //DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sIdPersonal.ToUpper().Trim(),
            DtGeneral.EstadoConectado, sIdFarmacia, sIdPersonal.ToUpper().Trim(),
            sNombrePersonal.ToUpper().Trim(), sLogin.ToUpper().Trim(), sPass, iProcess);

        
        lst.Add("bExec",false);

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
                
                if (sIdFarmacia != DtGeneral.Sucursal)
                {
                    sSql = string.Format("Set DateFormat  YMD " +
                                        "Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}' ", 
                                        DtGeneral.EstadoConectado, sIdFarmacia, 1, Fg.PonCeros(leer.Campo("Clave"), 4), sLogin);
                                        //DtGeneral.EstadoConectado, DtGeneral.Sucursal, 3, Fg.PonCeros(leer.Campo("Clave"), 4), sLogin);

                    //bContinua = DtGeneral.ExecQueryBool(sSql);
                    DtGeneral.ExecQuery(sSql);
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

        //sSql = string.Format("Select IdFarmacia, IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
        //                    //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status ='A'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());
        //                    "Where IdEstado = '{0}' And IdFarmacia = '{1}'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());

        sSql = string.Format("Select IdFarmacia, IdUsuario, Nombre, Login, Status From Net_Regional_Usuarios (NoLock) " +
            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And Status ='A'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString());
                            "Where IdEstado = '{0}' ", DtGeneral.EstadoConectado);

        sEstructura = DtGeneral.DtsToTableHtml(sSql, "tableUsers");

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
                                    "Where L.IdEstado = '{0}' And Convert(varchar(10), L.FechaRegistro, 120) Between '{1}' And '{2}' ", DtGeneral.EstadoConectado, dtpFechaInicial, dtpFechaFinal);


        HttpContext.Current.Session["AuditorMovimientos"] = DtGeneral.ExecQuery(sSql, "AuditorMovimientos");
        string sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["AuditorMovimientos"], "tableAuditorMovimientos");
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
                            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdUsuario = '{2}' And Status ='A'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), Fg.PonCeros(sIdPersonal,4));
                            //"Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdUsuario = '{2}' ", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), Fg.PonCeros(sIdPersonal, 4));
                            "Where IdEstado = '{0}' And IdFarmacia = '{1}' And IdUsuario = '{2}' ", DtGeneral.EstadoConectado, sIdFarmacia, Fg.PonCeros(sIdPersonal, 4));

        sEstructura = DtGeneral.SerializerDataSet(DtGeneral.ExecQuery(sSql, "User"));

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
        sEstructura = "<span class=\"labelGpoleft\">Perfil</span>" + DtGeneral.GruposUsuariosPerfiles(sOption);

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
                                        "Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}' ", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), iIdGrupo, Fg.PonCeros(sIdUsuario, 4), sLoginUser);

                    if (iIdGrupo != 3 && iIdGrupo != 15)
                    {
                        sQuery += string.Format(" Exec spp_Mtto_Net_CTE_Grupos_Usuarios_Miembros '{0}', '{1}', {2}, '{3}', '{4}'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), 3, Fg.PonCeros(sIdUsuario, 4), sLoginUser);
                    }
                }
                else if (sTipo == "Del")
                {
                    sQuery = string.Format("Set DateFormat YMD " +
                                            "Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                                            "Where IdEstado = {0} and IdFarmacia = {1} and IdGrupo = {2} and IdUsuario = '{3}' ", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), iIdGrupo, Fg.PonCeros(sIdUsuario, 4));

                    if (iIdGrupo != 3 && iIdGrupo != 15)
                    {
                        sQuery += string.Format(" Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                                            "Where IdEstado = {0} and IdFarmacia = {1} and IdGrupo = {2} and IdUsuario = '{3}'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), 3, Fg.PonCeros(sIdUsuario, 4));
                    }
                    //else if (iIdGrupo == 3)
                    //{
                    //    sQuery = string.Format(" Update Net_CTE_Grupos_Usuarios_Miembros Set Status = 'C', Actualizado = 0 " +
                    //                        "Where IdEstado = {0} and IdFarmacia = {1} and IdUsuario = '{2}'", DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), Fg.PonCeros(sIdUsuario, 4));
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


    [WebMethod]
    public static string GetBeneficiariosPedidosEspeciales(string sIdPerfil)
    {
        string sReturn = string.Empty;
        string sSql = string.Format("Select * " +
                                    "From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_Beneficiarios " +
                                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPerfilAtencion = {3}", DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, sIdPerfil);
        

        sReturn = DtGeneral.OptionDropList(DtGeneral.ExecQuery(sSql), "IdBeneficiario", "NombreCompleto", true);
        return sReturn;
    }

    [WebMethod]
    public static string GetCatalogoClaves(string sIdPerfil)
    {
        string sReturn = string.Empty;
        string sFiltro = "";
        if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
        {
            sFiltro = string.Format("And CB.IdFarmacia = '{0}'", HttpContext.Current.Session["IdSucursal"].ToString());
        }

        string sSql = string.Format("Select IdClaveSSA, CONVERT(INT, ClaveSSA) As ClaveSSA, DescripcionClave As Descripcion, Presentacion, ContenidoPaquete As Contenido From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_ClavesSSA (NoLock) " +
                                    "Where  IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPerfilAtencion = {3} Order By ClaveSSA", DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, sIdPerfil);

        clsConexionSQL cnn = DtGeneral.DatosConexionCedis;
        clsLeer myleer = new clsLeer(ref cnn);
        myleer.Exec(sSql);

        sReturn = DtGeneral.DtsToTableHtml(myleer.DataSetClase, "AyudaFarmacias");
        return sReturn;
    }

    [WebMethod]
    public static string GetInfoGeneralPedidosEspeciales()
    {
        if (HttpContext.Current.Session["InfoGeneralPedidosEspeciales"] == null && DtGeneral.UsaPedidos)
        {

            clsConexionSQL cnn = DtGeneral.DatosConexionCedis;
            clsLeer myLeer = new clsLeer(ref cnn);
            DataSet dtsInfo = new DataSet("Info");
            DataTable dt = new DataTable();
            string sQuery = string.Empty;

            string sSql = string.Format("Select " +
                                            "IdPerfilAtencion, PerfilDeAtencion, IdSubPrograma, SubPrograma,  " +
                                            "0 As No, IdClaveSSA, ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, 0 As ContenidoCajas, 0 As CantidadPiezas " +
                                        "From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_ClavesSSA (NoLock) " +
                                        "Where  IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                        "Order By ClaveSSA " +
                                        "Select IdPerfilAtencion, PerfilDeAtencion, IdBeneficiario, NombreCompleto From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_Beneficiarios (NoLock) " +
                                        "Where  IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' " +
                                        "Select " +
                                            "Distinct(IdPerfilAtencion), PerfilDeAtencion " +
                                        "From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_ClavesSSA (NoLock) " +
                                        "Where  IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' ", DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen);

            dt.Columns.Add("IdPerfilAtencion", typeof(string));
            dt.Columns.Add("Tabla", typeof(string));

            string[] sColumnas = new string[] { "No", "IdClaveSSA", "ClaveSSA", "DescripcionClave", "Presentacion", "ContenidoPaquete", "ContenidoCajas", "CantidadPiezas" };
            sQuery = "";

            myLeer.Exec(sSql);
            myLeer.RenombrarTabla(1, "Catalago");
            myLeer.RenombrarTabla(2, "Beneficiarios");
            myLeer.RenombrarTabla(3, "Perfiles");
            DataTable dtData = new DataTable("Cuadro");
            dtsInfo = myLeer.DataSetClase;
            myLeer.DataTableClase = dtsInfo.Tables["Perfiles"];
            string[,] sColumsName = {
                                    {"", ""},
                                    {"", ""},
                                    {"ClaveSSA", "Clave SSA"},
                                    {"DescripcionClave", "Descripción"},
                                    {"Presentacion", "Presentación"},
                                    {"ContenidoPaquete", "Contenido paquete"},
                                    {"ContenidoCajas", "Contenido Cajas"},
                                    {"CantidadPiezas", "Cantidad Piezas"}
                                };

            while (myLeer.Leer())
            {
                sQuery = string.Format(" [IdPerfilAtencion] = '{0}' ", myLeer.Campo("IdPerfilAtencion"));
                dtData = dtsInfo.Tables["Catalago"].Select(sQuery, sColumnas[1]).CopyToDataTable();

                dt.Rows.Add(myLeer.Campo("IdPerfilAtencion"), DtGeneral.DtsToTableHtml(dtData.DefaultView.ToTable(true, sColumnas), "CuadroBasico", sColumsName));
            }

            dt.TableName = "Catalago";
            dtsInfo.Tables.Remove("Catalago");
            dtsInfo.Tables.Add(dt);


            HttpContext.Current.Session["InfoGeneralPedidosEspeciales"] = dtsInfo;
        }

        if (DtGeneral.UsaPedidos)
        {

            return DtGeneral.SerializerDataSet((DataSet)HttpContext.Current.Session["InfoGeneralPedidosEspeciales"]);
        }
        else 
        {
            return "";
        }

    }

    [WebMethod]
    public static string GetClaveSSA(string sClaveSSA)
    {
        Dictionary<string, DataSet> lst = new Dictionary<string, DataSet>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        clsLeer myLeer = new clsLeer();

        string sDescripcionClave = string.Empty;
        
        try
        {
            if (HttpContext.Current.Session["ClavesSSA"] != null)
            {
                lst = (Dictionary<string, DataSet>)HttpContext.Current.Session["ClavesSSA"];
            }

            DataSet dtsClaves = lst[sClaveSSA];

            if (dtsClaves.Tables[0].Rows.Count != 0)
            {
                DataRow row = dtsClaves.Tables[0].Rows[0];
                sDescripcionClave = row["DescripcionClave"].ToString();
            }
        }
        catch (KeyNotFoundException)
        {
            string sSql = string.Format("Select " +
                                            "IdProducto, CodigoEAN, ClaveSSA, DescripcionSal, DescripcionClave, DescripcionCortaClave " +
	                                    "From vw_Productos_CodigoEAN (NoLock) " +
	                                    "Where ClaveSSA = '{0}'", sClaveSSA);
            
            DataSet dtsClaves = DtGeneral.ExecQuery(sSql, "InfoClave");

            if (dtsClaves.Tables[0].Rows.Count != 0)
            {
                lst.Add(sClaveSSA, dtsClaves);
                HttpContext.Current.Session["ClavesSSA"] = lst;
                DataRow row = dtsClaves.Tables[0].Rows[0];
                sDescripcionClave = row["DescripcionClave"].ToString();
            }
            //throw;
        }
        
        return sDescripcionClave;
    }
    #endregion Catalogos

    #region Reportes
    [WebMethod]
    public static string Existencia(string sIdJurisdiccion, string sIdFarmacia, int iTipoExistencia, int iTipoInsumo, int iTipoDispensacion, int iTipoClave, bool bOnline)
    {
        string sEstructura = string.Empty;
        string sMensaje = string.Empty;
        string sTabla = string.Empty;
        Boolean bMostrarResultado = false;
        string sPantalla = "FrmExistencias";

        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        //wsConexion.wsCnnCliente x = new wsCnnCliente();
        //x.Url = "";

        //wsCnnCliente y = new wsCnnCliente();
        //y.Url = "";

        //x.ExecuteExt();
           
        /* 
             solicitud = "FarmaciaPtoVta.ini"
            
        */

        string sSql = string.Format("Set Dateformat YMD Exec  spp_Rpt_CteReg_Existencia_Claves_Farmacia '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}' ",
               DtGeneral.IdCliente, DtGeneral.IdSubCliente, DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, iTipoExistencia, iTipoInsumo, iTipoDispensacion, iTipoClave);

        if (bOnline)
        {
            wsCnnCliente cnnCte = new wsCnnCliente();
            string sFiltro = string.Format("[IdFarmacia] = '{0}'", sIdFarmacia);
            cnnCte.Url = DtGeneral.dtsFiltro(DtGeneral.urlFarmacias, "urlFarmacias", sFiltro, "UrlFarmacia");
            HttpContext.Current.Session["Existencia"] = cnnCte.ExecuteExt(null, "FarmaciaPtoVta.ini", sSql);
            sPantalla = "FrmExistenciasEnLinea";
        }
        else
        {
            //Variables de Sesión
            HttpContext.Current.Session["Existencia"] = DtGeneral.ExecQuery(sSql);
        }

        sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["Existencia"], "Existencia");
        
        lst.Add("Tabla", ValidarResultado(sTabla, "Existencia"));
        bMostrarResultado = ValidarResultado(sTabla);
        lst.Add("MostrarResultado", bMostrarResultado.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, sPantalla);
        
        return sEstructura;
    }

    [WebMethod]
    public static string ExistenciaSPAC(string sIdJurisdiccion, string sIdFarmacia, int iTipoExistencia, int iTipoInsumo, int iTipoDispensacion, int iTipoClave)
    {
        string sEstructura = string.Empty;
        string sMensaje = string.Empty;
        string sTabla = string.Empty;
        Boolean bMostrarResultado = false;
        string sPantalla = "Puebla_frmExistenciaSinProximosACaducar";
        int iMeses = 0;

        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_Existencia_Claves_Farmacia_Sin_Proximos_A_Caducar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}",
               DtGeneral.IdCliente, DtGeneral.IdSubCliente, DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, iTipoExistencia, iTipoInsumo, iTipoDispensacion, iTipoClave, iMeses);

        
        //Variables de Sesión
        HttpContext.Current.Session["ExistenciaSPAC"] = DtGeneral.ExecQuery(sSql);


        sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ExistenciaSPAC"], "ExistenciaSPAC");

        lst.Add("Tabla", ValidarResultado(sTabla, "ExistenciaSPAC"));
        bMostrarResultado = ValidarResultado(sTabla);
        lst.Add("MostrarResultado", bMostrarResultado.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, sPantalla);

        return sEstructura;
    }

    [WebMethod]
    public static string ProximosACaducar(string sIdJurisdiccion, string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal, int iTipoInsumo, int iTipoDispensacion, bool bOnline)
    {
        string sEstructura = string.Empty;
        string sMensaje = string.Empty;
        string sTabla = string.Empty;
        Boolean bMostrarResultado = false;
        string sPantalla = "FrmProximosACaducar";

        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        if (dtpFechaInicial == "")
        {
            dtpFechaInicial = General.FechaYMD(DateTime.Now, "-");
        }

        if (dtpFechaFinal == "")
        {
            dtpFechaFinal = General.FechaYMD(DateTime.Now.AddMonths(3), "-");
        }

        string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_EdoJuris_Proximos_Caducar '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                   DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia,
                   dtpFechaInicial, dtpFechaFinal, 0, iTipoInsumo, iTipoDispensacion);
        
        if (bOnline)
        {
            int iMostrar = 2; // Para que muestre solo la farmacia de la unidad.

            sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_Impresion_Proximos_Caducar '{0}', '{1}', '{2}', '', '', '', '{3}', '{4}', '{5}', '{6}', '{7}' ",
                   "", DtGeneral.EstadoConectado, sIdFarmacia, iMostrar,
                   dtpFechaInicial, dtpFechaFinal, iTipoInsumo, iTipoDispensacion);

            sSql += "\n" + string.Format("Select " +
		                                    "F.IdJurisdiccion, F.Jurisdiccion, "+
		                                    "P.IdFarmacia, P.Farmacia, P.ClaveSSA, P.DescripcionSal As Descripcion, " +
		                                    "P.Presentacion_ClaveSSA As Presentacion, P.ClaveLote, P.FechaCaducidad, " +
		                                    "Sum(P.Existencia) as Existencia " +
	                                    "From Rpt_CteReg_Impresion_Proximos_Caducar P (NoLock) " +
	                                    "Inner Join vw_Farmacias F On (F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia) " +
	                                    "Group By F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, P.Farmacia, P.ClaveSSA, P.DescripcionSal, " +
                                        "P.Presentacion_ClaveSSA, P.ClaveLote, P.FechaCaducidad");

            wsCnnCliente cnnCte = new wsCnnCliente();
            string sFiltro = string.Format("[IdFarmacia] = '{0}'", sIdFarmacia);
            cnnCte.Url = DtGeneral.dtsFiltro(DtGeneral.urlFarmacias, "urlFarmacias", sFiltro, "UrlFarmacia");
            HttpContext.Current.Session["ProximosACaducar"] = cnnCte.ExecuteExt(null, "FarmaciaPtoVta.ini", sSql);
            sPantalla = "FrmProximosACaducarEnLinea";
        }
        else
        {
            //Variables de Sesión
            HttpContext.Current.Session["ProximosACaducar"] = DtGeneral.ExecQuery(sSql);
        }

        sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ProximosACaducar"], "ProximosACaducar");

        lst.Add("Tabla", ValidarResultado(sTabla, "ProximosACaducar"));
        bMostrarResultado = ValidarResultado(sTabla);
        lst.Add("MostrarResultado", bMostrarResultado.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, sPantalla);

        return sEstructura;
    }

    [WebMethod]
    public static string DispensancionClaves(string sTipoUnidad, string sIdJurisdiccion, string sIdFarmacia, string sLocalidad, string dtpFechaInicial, string dtpFechaFinal, int iTipoInsumo, int iTipoDispensacion, int iConcentrado, int iProcesoPorDia)
    {
        string sEstructura = string.Empty;
        int  iTipoMedicamento = 0, iFiltro = 0;
        clsLeer myLeer = new clsLeer();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //int iqueryPersonalizado = 1;
        string sTabla = "";
        string sStored = DtGeneral.queryReporte.DispensancionClaves.@default;
        Boolean bMostrarResultado = false;

        /*string sSql = string.Format("Exec spp_Rpt_CteReg_ValidarOrigenDeDatos '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                    DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdCliente, dtpFechaInicial, dtpFechaFinal);

        myLeer.DataSetClase = DtGeneral.ExecQuery(sSql);
        myLeer.Leer();

        if (myLeer.DataSetClase.Tables.Count > 0)
        {
            iqueryPersonalizado = myLeer.CampoInt("Resultado");
            sMensaje = myLeer.Campo("Mensaje");
        }*/

        //if (iqueryPersonalizado != 0)
        //{
            /*if (iqueryPersonalizado == 2)
            {
                sStored = DtGeneral.queryReporte.DispensancionClaves.personalizado;
            }*/

        string sSql = string.Format(
                "Set Dateformat YMD " +
                //" Exec  spp_Rpt_VentasPorClaveMensual " +
                "Exec {15} " +
                "@IdEstado = '{0}', @IdJurisdiccion = '{1}', @IdFarmacia='{2}', " +
                "@IdClaveSSA = '{3}', @FechaInicial = '{4}', @FechaFinal = '{5}', " +
                "@TipoDispensacion = '{6}', @TipoInsumo = '{7}', @SubFarmacias = '', @AgrupaDispensacion = 0, " +
                "@Filtro = '{8}', @TipoMedicamento = '{9}', @IdMunicipio = '{10}', @IdTipoUnidad = '{11}', @Concentrado = '{12}',  " +
                "@ProcesoPorDia = {13}, @TipoInvocacion = {14} ",
                DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, "*",
                dtpFechaInicial, dtpFechaFinal,
                //iTipoDispensacion, iTipoInsumo, iFiltro, iTipoMedicamento, sLocalidad, sTipoUnidad, iConcentrado, iProcesoPorDia, 1); 
                iTipoDispensacion, iTipoInsumo, iFiltro, iTipoMedicamento, sLocalidad, sTipoUnidad, iConcentrado, iProcesoPorDia, 1, sStored);

        //Variables de Sesión
        HttpContext.Current.Session["DispensancionClaves"] = DtGeneral.ExecQuery(sSql);

        sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["DispensancionClaves"], "DispensancionClaves");
        //}

        //lst.Add("Mensaje", sMensaje);
        //lst.Add("Resultado", iqueryPersonalizado);
        //lst.Add("Tabla", sTabla);


        lst.Add("Tabla", ValidarResultado(sTabla, "DispensancionClaves"));
        bMostrarResultado = ValidarResultado(sTabla);
        lst.Add("MostrarResultado", bMostrarResultado.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmDispensacionClaves.aspx");

        return sEstructura;
    }

    [WebMethod]
    public static string MedicosDiagnostico(string sTipoUnidad,string sIdJurisdiccion, string sIdFarmacia, string sLocalidad, string dtpFechaInicial, string dtpFechaFinal, int iClaves, int iDiagnosticos, int iMedicos, int iProcesoPorDia)
    {
        string sEstructura = "";
        clsLeer myLeer = new clsLeer();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        int iqueryPersonalizado = 1;
        string sTabla = "";
        string sMensaje = "";
        string sStored = DtGeneral.queryReporte.MedicosDiagnostico.@default;

        //string sSql = string.Format("Exec spp_Rpt_CteReg_ValidarOrigenDeDatos '{0}', '{1}', '{2}', '{3}', '{4}' ",
        //                            DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdCliente, dtpFechaInicial, dtpFechaFinal);
        
        //myLeer.DataSetClase = DtGeneral.ExecQuery(sSql);
        //myLeer.Leer();

        //iqueryPersonalizado = myLeer.CampoInt("Resultado");
        //sMensaje = myLeer.Campo("Mensaje");

        //if (iqueryPersonalizado != 0)
            //if (iqueryPersonalizado == 2)
            //{
            //    sStored = DtGeneral.queryReporte.MedicosDiagnostico.personalizado;
            //}

        string sSql = string.Format(
                "Set Dateformat YMD " +
                //"Exec spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}  ",
                "Exec {11} '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}  ",
                DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, sLocalidad, sTipoUnidad,
                //dtpFechaInicial, dtpFechaFinal, iClaves, iDiagnosticos, iMedicos, iProcesoPorDia);
                dtpFechaInicial, dtpFechaFinal, iClaves, iDiagnosticos, iMedicos, iProcesoPorDia, sStored);

        //Variables de Sesión
        HttpContext.Current.Session["MedicosDiagnostico"] = DtGeneral.ExecQuery(sSql);
        sTabla = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["MedicosDiagnostico"], "MedicosDiagnostico");
        //sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["MedicosDiagnostico"], "MedicosDiagnostico");
        lst.Add("Mensaje", sMensaje);
        lst.Add("Resultado", iqueryPersonalizado);
        lst.Add("Tabla", sTabla);
        
        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmMedicosDiagnosticos");

        return sEstructura;
    }

    [WebMethod]
    public static string AntibioticosControlados(string sTipoUnidad, string sIdJurisdiccion, string sIdFarmacia, string sLocalidad, string dtpFechaInicial, string dtpFechaFinal, int iTipoMedicamento)
    {
        string sEstructura = string.Empty;
        int iTipoDispensacion = 0, iTipoInsumo = 0, iFiltro = 1;

        string sSql = string.Format(
               "Set Dateformat YMD " +
               "Exec spp_Rpt_VentasPorClaveMensual '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '', 0, '{8}', '{9}', '{10}', '{11}', {12}",
               DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, "*", dtpFechaInicial, dtpFechaFinal,
               iTipoDispensacion, iTipoInsumo, iFiltro, iTipoMedicamento, sLocalidad, sTipoUnidad, 1);

        //Variables de Sesión
        HttpContext.Current.Session["AntibioticosControlados"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["AntibioticosControlados"], "AntibioticosControlados");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmAntibioticosControlados");

        return sEstructura;
    }

    [WebMethod]
    public static string CostoPacienteProgramaDeAtencion(string sTipoUnidad, string sIdJurisdiccion, string sIdFarmacia, string sLocalidad, string dtpFechaInicial, string dtpFechaFinal, int iTipoReporte)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format(
                "Set Dateformat YMD " +
                " Exec  spp_Rpt_CostosBeneficiarios_SalidasClavesMensual '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'  ",
                DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, sLocalidad, sTipoUnidad,
                dtpFechaInicial, dtpFechaFinal, iTipoReporte);

        //Variables de Sesión
        HttpContext.Current.Session["CostoPacienteProgramaDeAtencion"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["CostoPacienteProgramaDeAtencion"], "CostoPacienteProgramaDeAtencion");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmCostoPacienteProgramaDeAtencion");

        return sEstructura;
    }

    [WebMethod]
    public static string CortesDiarios(string sIdJurisdiccion, string sIdFarmacia, string dtpFechaInicial)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_CortesDiarios_Farmacia '{0}', '{1}', '{2}', '{3}' ",
                   DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, dtpFechaInicial);

        //Variables de Sesión
        HttpContext.Current.Session["CortesDiarios"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["CortesDiarios"], "CortesDiarios");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmCortesDiarios");

        return sEstructura;
    }
    
    [WebMethod]
    public static string ListadosVarios(string sReporte)
    {
        string sEstructura = string.Empty;

        string sSql = ArmarSelect(sReporte);

        //Variables de Sesión
        HttpContext.Current.Session["ListadosVarios"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ListadosVarios"], "ListadosVarios");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmListadosVarios");
        
        return sEstructura;
    }

    [WebMethod]
    public static string ReportesTransferencias(string sTipoTransferencia, string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal, int iTipoInsumo, int iTipoDispensacion, int iTipoDestino)
    {
        string sEstructura = string.Empty;
        string sFiltro = "";
        string sStore = "", sTabla = ""; 
        
        if (sTipoTransferencia == "1")
        {
            sStore = " spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida ";
            sTabla = " Rpt_CteReg_Impresion_Seguimiento_Transferencias_Salida ";
        }
        else
        {
            sStore = " spp_Rpt_CteReg_Impresion_Seguimiento_Transferencias_Entrada ";
            sTabla = " Rpt_CteReg_Impresion_Seguimiento_Transferencias_Entrada ";
        }

        if (iTipoDestino == 1)
        {
            sFiltro = " Where EsAlmacenRecibe = 1";
        }
        else if (iTipoDestino == 0) {
            sFiltro = " Where EsAlmacenRecibe = 0";
        }
        
        string sSql = string.Format("Set Dateformat YMD " +
            " Exec  {0} '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'  ",
            sStore, DtGeneral.EstadoConectado, Fg.PonCeros(sIdFarmacia, 4), dtpFechaInicial, dtpFechaFinal, iTipoDispensacion, iTipoInsumo);
        
        sSql += "\n " + string.Format("Select ClaveSSA, DescripcionSal As Descripcion, CodigoEAN, ClaveLote, Costo, Cantidad, Importe, FechaTransferencia, Folio, IdFarmaciaRecibe, FarmaciaRecibe From {0} (NoLock) {1}", sTabla, sFiltro);
        
        //Variables de Sesión
        HttpContext.Current.Session["ReportesTransferencias"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ReportesTransferencias"], "ReportesTransferencias");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmReportesTransferencias");

        return sEstructura;
    }

    [WebMethod]
    public static string PedidosEspeciales(string sFolioPedido, string sObservaciones ,string sData)
    {
        string sEstructura = string.Empty;
        Dictionary<string, object> lst= new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        clsConexionSQL cnn = DtGeneral.DatosConexion;
        clsLeer leer = new clsLeer(ref cnn);
        bool bContinua = false;

        string sMensaje = "";
        string sFolio = "";
        bool bEsTransferencia = true;

        string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
            DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, HttpContext.Current.Session["IdSucursal"].ToString(),
            "*", HttpContext.Current.Session["IdPersonal"].ToString(), sObservaciones, "A", bEsTransferencia, "0000", "0000", "0000", "0000");
        
        if (cnn.Abrir())
        {
            cnn.IniciarTransaccion();

            //Encabezado
            if (leer.Exec(sSql))
            {

                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");
                    
                    lst.Add("Folio", sFolio);
                    lst.Add("Mensaje", sMensaje);

                    bContinua = true;
                }
            }

            //Detalles
            if (bContinua)
            {
                bContinua = false;

                sSql = string.Format(sData, DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(),
                        sFolio, "Exec spp_Mtto_Pedidos_Cedis_Det");

                if (leer.Exec(sSql))
                {
                    bContinua = true;
                }
            }

            //Se ejecuto todo correctamente    
            if (bContinua)
            {
                cnn.CompletarTransaccion();
                sEstructura = serializer.Serialize(lst);
            }
            else
            {
                cnn.DeshacerTransaccion();
            }

            cnn.Cerrar();
        }

        return sEstructura;
    }

    [WebMethod]
    public static string RegistroPedidosEspeciales(string sFolioPedido, string sObservaciones, string sData, int iIdPerfil, string sIdBeneficiario, string sReferencia)
    {
        string sEstructura = string.Empty;
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        clsConexionSQL cnn = DtGeneral.DatosConexionCedis;
        clsLeer leer = new clsLeer(ref cnn);
        bool bContinua = false;

        string sMensaje = "";
        string sFolio = "";
        bool bEsTransferencia = true;

        string sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis_Enc '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}' ",
            DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, HttpContext.Current.Session["IdSucursal"].ToString(),
            "*", HttpContext.Current.Session["IdPersonal"].ToString(), sObservaciones, "A", bEsTransferencia, "0000", "0000", "0000", "0000");

        string sCadena = sSql;
        
        if (cnn.Abrir())
        {
            cnn.IniciarTransaccion();

            //Encabezado
            if (leer.Exec(sSql))
            {

                if (leer.Leer())
                {
                    sFolio = leer.Campo("Folio");
                    sMensaje = leer.Campo("Mensaje");

                    lst.Add("Folio", sFolio);
                    lst.Add("Mensaje", sMensaje);

                    bContinua = true;
                }
            }

            //Detalles
            if (bContinua)
            {
                bContinua = false;

                sSql = string.Format(sData, DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(),
                        sFolio, "Exec spp_Mtto_Pedidos_Cedis_Det");

                sSql += string.Format(" Exec spp_Mtto_Pedidos_Cedis__InformacionPedidosEspeciales '{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}' ", DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(),
                                        sFolio, iIdPerfil, sIdBeneficiario, sReferencia);

                sCadena += sSql;

                if (leer.Exec(sSql))
                {
                    bContinua = true;
                }
            }

            //Se ejecuto todo correctamente    
            if (bContinua)
            {
                cnn.CompletarTransaccion();
                sEstructura = serializer.Serialize(lst);

                sCadena = sCadena.Replace("'", "\"");
                clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmRegistroDePedidos");
            }
            else
            {
                cnn.DeshacerTransaccion();
            }

            cnn.Cerrar();
        }

        return sEstructura;
    }

    [WebMethod]
    public static string GetInfoRegistroPedidosEspeciales(string sFolio)
    {

        clsConexionSQL cnn = DtGeneral.DatosConexionCedis;
        clsLeer myLeer = new clsLeer(ref cnn);
        DataTable dt = new DataTable();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        DataSet dtsInfo = new DataSet("dtsInfo");
        string sReturn = "";

        myLeer.DataSetClase = DtGeneral.Folio_Pedidos_CEDIS_Enc(DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFolio, "CRTW-ws_General-GetInfoPedidosEspeciales", true);
        myLeer.RenombrarTabla(1, "Enc");
        
        dtsInfo.Tables.Add(myLeer.DataTableClase);

        myLeer.DataSetClase = DtGeneral.Folio_Pedidos_CEDIS_Det(DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), sFolio, "CRTW-ws_General-GetInfoPedidosEspeciales", true);
        myLeer.RenombrarTabla(1, "Det");

        dtsInfo.Tables.Add(myLeer.DataTableClase);

        string sSql = string.Format("Select A.IdPerfilAtencion, A.IdBeneficiario, B.NombreCompleto, A.Referencia  From Pedidos_Cedis__InformacionPedidosEspeciales A (NoLock) " +
                                    "Inner Join vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_Beneficiarios B (NoLock) On (A.IdEmpresa = B.IdEmpresa And A.IdEstado = B.IdEstado And A.IdPerfilAtencion = B.IdPerfilAtencion And A.IdBeneficiario = B.IdBeneficiario)  " +
                                    "Where A.IdEmpresa = '{0}' And A.IdEstado = '{1}' And A.IdFarmacia = '{2}' And A.FolioPedido = '{3}'", DtGeneral.Empresa, DtGeneral.EstadoConectado, HttpContext.Current.Session["IdSucursal"].ToString(), Fg.PonCeros(sFolio, 6));
        
        myLeer.Exec(sSql);
        myLeer.RenombrarTabla(1, "InformacionAdiccional");

        dtsInfo.Tables.Add(myLeer.DataTableClase);

        if (dtsInfo.Tables["Enc"].Rows.Count > 0)
        {
            sReturn = DtGeneral.SerializerDataSet(dtsInfo);
        }

        return sReturn;
    }

    [WebMethod]
    public static string GetInfoPedidosEspeciales(string sFolio)
    {

        clsLeer myLeer = new clsLeer();
        DataTable dt = new DataTable();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        DataSet dtsInfo = new DataSet("dtsInfo");
        string sReturn = "";

        myLeer.DataSetClase = DtGeneral.Folio_Pedidos_CEDIS_Enc(DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, sFolio, "CRTW-ws_General-GetInfoPedidosEspeciales", false);
        myLeer.RenombrarTabla(1, "Enc");

        dtsInfo.Tables.Add(myLeer.DataTableClase);

        myLeer.DataSetClase = DtGeneral.Folio_Pedidos_CEDIS_Det(DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, sFolio, "CRTW-ws_General-GetInfoPedidosEspeciales", false);
        myLeer.RenombrarTabla(1, "Det");

        dtsInfo.Tables.Add(myLeer.DataTableClase);

        if (dtsInfo.Tables["Enc"].Rows.Count > 0)
        {
            sReturn = DtGeneral.SerializerDataSet(dtsInfo);
        }

        return sReturn;
    }
    
    [WebMethod]
    public static string ClavesNegadas(string sIdJurisdiccion, string dtpFechaInicial)
    {

        string sReturn = string.Empty;
        clsLeer myLeer = new clsLeer();
        clsLeer leerResultado = new clsLeer();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        int iOpcion = 0;
        iOpcion = 0; 

        string sSql = string.Format(" Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' ",
                DtGeneral.EstadoConectado, sIdJurisdiccion, "*", dtpFechaInicial, iOpcion, 0, 0);
        myLeer.DataSetClase = DtGeneral.ExecQuery(sSql);
        
        //Variables de Sesión
        HttpContext.Current.Session["ClavesNegadas"] = myLeer.DataSetClase;
        
        leerResultado.DataTableClase = myLeer.Tabla(2);
        leerResultado.Leer();

        lst.Add("Tabla", DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ClavesNegadas"], "ClavesNegadas"));
        lst.Add("Claves", leerResultado.CampoInt("Claves").ToString());
        lst.Add("TotalPiezas", leerResultado.CampoDouble("TotalPiezas").ToString("#,###,###,##0"));

        sReturn = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmClavesNegadas_Regional");

        return sReturn;
    }
    
    [WebMethod]
    public static string SurtimientoInsumos(string sIdJurisdiccion, string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal, int iTipoReporte)
    {
        string sEstructura = string.Empty;

        string sSql = "";

        if (iTipoReporte == 1)
        {
            sSql = string.Format("Set Dateformat YMD Exec  spp_Rpt_EdoJuris_SurtimientoRecetas '{0}', '{1}', '{2}', '{3}', '{4}'  ",
                 DtGeneral.EstadoConectado, sIdJurisdiccion, DtGeneral.IdCliente, dtpFechaInicial, dtpFechaFinal);
        }
        else
        {
            sSql = string.Format("Set Dateformat YMD Exec  spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
               DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, DtGeneral.IdCliente, dtpFechaInicial, dtpFechaFinal);
        }

        //Variables de Sesión
        HttpContext.Current.Session["SurtimientoInsumos"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["SurtimientoInsumos"], "SurtimientoInsumos");

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmTBC_Surtimiento_Regional");

        return sEstructura;
    }

    [WebMethod]
    public static string EfectividadVales(string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format("Exec spp_Rpt_CteReg_Efectividad_Vales '{0}', '{1}', '{2}', '{3}'   ",
                DtGeneral.EstadoConectado, sIdFarmacia, dtpFechaInicial, dtpFechaFinal);

        sEstructura = DtGeneral.SerializerDataSet(DtGeneral.ExecQuery(sSql, "Info"));

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmEfectividadVales");

        return sEstructura;
    }

    [WebMethod]
    public static string SurtimientoRecetas(string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_SurtimientoRecetas '{0}', '{1}', '{2}', '{3}', '{4}', '{5}' ",
               DtGeneral.EstadoConectado, sIdFarmacia, DtGeneral.IdCliente, DtGeneral.IdSubCliente, dtpFechaInicial, dtpFechaFinal);

        //Variables de Sesión
        HttpContext.Current.Session["SurtimientoRecetas"] = DtGeneral.ExecQuery(sSql, "Info");
        sEstructura = DtGeneral.SerializerDataSet((DataSet)HttpContext.Current.Session["SurtimientoRecetas"]);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmSurtimientoRecetas");

        return sEstructura;
    }

    [WebMethod]
    public static string ClavesSurtimiento(int iTipo)
    {
        string sEstructura = string.Empty;

        string sFiltro = iTipo == 0 ? "" : string.Format(" Where Tipo = '{0}' ", iTipo);

        string sSql = string.Format(" Select ClaveSSA, DescripcionClave, CantidadSurtida " +
            " From Rpt_NivelDeAbasto_ClavesDispensadas (Nolock)  {0}   " +
            " Order by DescripcionClave ", sFiltro);

        if (iTipo == 0)
        {
            sSql =
                string.Format(" Select ClaveSSA, DescripcionClave, sum(CantidadSurtida) as CantidadSurtida " +
                " From Rpt_NivelDeAbasto_ClavesDispensadas (Nolock)  {0}   " +
                " Group by IdClaveSSA, ClaveSSA, DescripcionClave " +
                " Order by DescripcionClave ", sFiltro);
        }

        //Variables de Sesión
        HttpContext.Current.Session["ClavesSurtimiento"] = DtGeneral.ExecQuery(sSql);

        sEstructura = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ClavesSurtimiento"], "ClavesSurtimiento");

        return sEstructura;
    }
    
    [WebMethod]
    public static string AbastoFarmacias(string sIdFarmacia)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format(" Exec spp_Rpt_AbastoClaves '{0}', '{1}' ", DtGeneral.EstadoConectado, sIdFarmacia);

        string[,] sColumnasNombres = new string[5, 2] { 
            { "TotalClaves", "Total de Claves" }, 
            { "ConExistencia", "Claves Con Existencia" }, 
            { "SinExistencia", "Claves Sin Existencia" }, 
            { "PorcAbasto", "% de Abasto" }, 
            { "FechaReporte", "Fecha del Reporte" } 
        };

        sEstructura = DtGeneral.DtsToTableHtml(sSql, "AbastoFarmacias", sColumnasNombres);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmAbastoFarmacias");

        return sEstructura;
    }

    [WebMethod]
    public static string AbastoFarmaciasGeneral(int iTipoDeInsumo, int iConcentrado)
    {
        string sEstructura = string.Empty;

        string sSql = string.Format(" Exec spp_Rpt_AbastoClaves_Global '{0}', '{1}', {2}, {3} ", DtGeneral.EstadoConectado, "*", iTipoDeInsumo, iConcentrado);

        string[,] sColumnasNombres = new string[5, 2] { 
            { "TotalClaves", "Total de Claves" }, 
            { "ConExistencia", "Claves Con Existencia" }, 
            { "SinExistencia", "Claves Sin Existencia" }, 
            { "PorcAbasto", "% de Abasto" }, 
            { "FechaReporte", "Fecha del Reporte" } 
        };

        sEstructura = DtGeneral.DtsToTableHtml(sSql, "AbastoFarmaciasGeneral");

        return sEstructura;
    }

    [WebMethod]
    public static string ReportesDispensacion(string sIdFarmacia, string txtCte, string txtSubCte, string txtPro, string txtSubPro, string dtpFechaInicial, string dtpFechaFinal, bool rdoTpDispConsignacion, bool rdoTpDispVenta, bool rdoInsumosMedicamento, bool rdoInsumoMatCuracion, bool rdoInsumoMedicamentoSP, bool rdoInsumoMedicamentoNOSP)
    {
        string sEstructura = string.Empty;
        bool bEjecuto = true;

        int iTipoInsumo = 0, iTipoDispensacion = 0, iTipoInsumoMedicamento = 0;


        // Determinar el tipo de dispensacion a mostrar 
        if (rdoTpDispConsignacion)
            iTipoDispensacion = 1;

        if (rdoTpDispVenta)
            iTipoDispensacion = 2;


        // Determinar que tipo de producto se mostrar 
        if (rdoInsumosMedicamento)
            iTipoInsumo = 1;

        if (rdoInsumoMatCuracion)
            iTipoInsumo = 2;

        // Determinar si pertenecen o no a Seguro Popular
        if (rdoInsumoMedicamentoSP)
        {
            iTipoInsumo = 1;
            iTipoInsumoMedicamento = 1;
        }

        if (rdoInsumoMedicamentoNOSP)
        {
            iTipoInsumo = 1;
            iTipoInsumoMedicamento = 2;
        }

        clsLeer myLeer = new clsLeer();
        Dictionary<string, object> lst = new Dictionary<string, object>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        int iqueryPersonalizado = 1;
        string sMensaje = "";
        string sStored = DtGeneral.queryReporte.ReportesDispensacion.@default;
        string sExportarExcel = DtGeneral.queryReporte.ReportesDispensacion.exportarExcel;
        string sSql = string.Empty;
        string sCadena = string.Empty;

        //string sSql = string.Format("Exec spp_Rpt_CteReg_ValidarOrigenDeDatos '{0}', '{1}', '{2}', '{3}', '{4}' ",
        //                            DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdCliente, dtpFechaInicial, dtpFechaFinal);

        //myLeer.DataSetClase = DtGeneral.ExecQuery(sSql);
        //myLeer.Leer();

        //iqueryPersonalizado = myLeer.CampoInt("Resultado");
        //sMensaje = myLeer.Campo("Mensaje");

        if (iqueryPersonalizado != 0)
        {
            if (iqueryPersonalizado == 2)
            {
                sStored = DtGeneral.queryReporte.ReportesDispensacion.personalizado;
                sExportarExcel = DtGeneral.queryReporte.ReportesDispensacion.exportarExcelPersonalizado;
            }

            sSql = string.Format("Set Dateformat YMD Exec {11} '{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}' ",
                                   DtGeneral.EstadoConectado, sIdFarmacia, txtCte, txtSubCte, txtPro,
                                   txtSubPro, iTipoDispensacion, dtpFechaInicial, dtpFechaFinal, iTipoInsumo, iTipoInsumoMedicamento, sStored);
            
            sSql += "\n " + string.Format("Select top 1 * From RptAdmonDispensacion_Unidad (NoLock) ");

            sCadena = sSql;

            bEjecuto = DtGeneral.ExecQueryBool(sSql);

            if (bEjecuto)
            {
                //sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_Administrativos_Unidad_ExportarExcel '{0}'", DtGeneral.EstadoConectado);
                sSql = string.Format("Set Dateformat YMD Exec {1} '{0}'", DtGeneral.EstadoConectado, sExportarExcel);

                //Variables de Sesión
                HttpContext.Current.Session["ReportesDispensacion"] = DtGeneral.ExecQuery(sSql, "Info");
                sCadena += sSql;
            }
        }

        lst.Add("Mensaje", sMensaje);
        lst.Add("Resultado", iqueryPersonalizado);
        lst.Add("Ejecuto", bEjecuto.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        sCadena = sCadena.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmReportesFacturacionUnidad");

        return sEstructura;
    }

    [WebMethod]
    public static string ConsumosFacturados(string sIdFarmacia, string sIdJurisdiccion, string sFecha)
    {
        string sEstructura = string.Empty;
        sEstructura = "false";

        //string sSql = string.Format("Set Dateformat YMD Exec spp_Rpt_CteReg_EdoJuris_ConsumosFacturados '{0}','{1}', '{2}', '{3}' ",
        //DtGeneral.EstadoConectado, sIdJurisdiccion, sIdFarmacia, sFecha+ "-01");

        string sFiltro = string.Empty;
        if (sIdFarmacia == "*")
        {
            sFiltro = string.Format("Where CF.Fecha_de_Proceso = '{0}' ", sFecha + "-01");
        }
        else
        {
            sFiltro = string.Format("Where CF.IdFarmacia = '{0}' And CF.FechaDeProcesamiento = '{1}' ", sIdFarmacia, sFecha + "-01");
        }
        
        string sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
	                                    "CF.IdEstado, CF.Estado, CF.IdJurisdiccion, CF.Jurisdiccion, " +
	                                    "CF.IdFarmacia, CF.Farmacia, CF.FolioVenta, CF.FolioVale, " +
	                                    "CF.FechaRegistro, CF.NumReceta, CF.IdBeneficiario, " +
	                                    "CF.Beneficiario, CF.NumCedula, CF.NombreMedico, " +
	                                    "CF.ClaveSSA, 'Clave Cliente' = IsNull(CB.Clave_Cliente, 'PF000'), " +
	                                    "CF.DescripcionClaveSSA, CF.Cantidad, CF.PrecioUnitario, " +
	                                    "CF.Importe, CF.NumeroDeFactura, CF.FechaDeProcesamiento " +
                                      "From Rpt_CTE_ConsumosFacturados CF (NoLock) " +
                                      "Left Join CTE_CFG_CB_CAPREPA CB (NoLock) On (CF.ClaveSSA = CB.ClaveSSA) " +
                                    "{0} ", sFiltro);

        HttpContext.Current.Session["ConsumosFacturados"] = DtGeneral.ExecQuery(sSql, "Info");
        if (HttpContext.Current.Session["ConsumosFacturados"] != null)
        {
            if (((DataSet)HttpContext.Current.Session["ConsumosFacturados"]).Tables[0].Rows.Count > 0)
            {
                sEstructura = "true";
            }
        }

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "FrmConsumosFacturados");

        return sEstructura;
    }

    [WebMethod]
    public static string Reporteador(string sColumnas, string sGroup, string sSum, string sFiltros, string sOrdenacion, int iTop, string sIdFarmacia, string dtpFechaInicial, string dtpFechaFinal, string sTipoReporte, string sTipoInformacion)
    {
        string sEstructura = string.Empty;
        string sMensaje = string.Empty;
        int iResultado = 0;
        bool bEjecuto = false;

        Dictionary<string, object> lst = new Dictionary<string, object>();
        Dictionary<string, string> lstColumns = (Dictionary<string, string>)HttpContext.Current.Session["lstColumns"];
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        clsLeer myLeer = new clsLeer();

        string[] aColumnas = sColumnas.Split(',');
        string[] aGroup = sGroup.Split(',');
        string[] aSum = sSum.Split(',');
        string[] aFiltros = sFiltros.Split(',');
        string[] aOrdenacion = sOrdenacion.Split(',');

        string sSQLColumnas = string.Empty;
        string sSQLWhere = string.Empty;
        string sSQLGroupBy = string.Empty;
        string sSQLOrderBy = string.Empty;

        //Agregar Columnas

        if (iTop > 0)
        {
            sSQLColumnas += string.Format("Top {0} ", iTop);
        }

        for (int i = 0; i < aColumnas.Length; i++)
        {
            string[] acol = aColumnas[i].Split('|');
            if (acol[1] == "sum")
            {
                sSQLColumnas += string.Format("Sum({0}) As '{0} Total'", acol[0]);
            }
            else
            {
                //sSQLColumnas += string.Format("{0}", acol[0]);
                sSQLColumnas += string.Format("{0} As '{1}'", acol[0], lstColumns[acol[0]]);
            }

            if (i != aColumnas.Length - 1)
            {
                sSQLColumnas += ", ";
            }
        }

        //Se agrega ya que siempre existira filtro por rango de fechas
        sSQLWhere += "Where ";
        if (sFiltros != "")
        {
            //Agregar Where
            for (int i = 0; i < aFiltros.Length; i++)
            {
                string[] acol = aFiltros[i].Split('|');
                if (acol[1].Trim() != "")
                {
                    string sValue = acol[1].Trim();
                    sValue = sValue.Replace(' ', '%');
                    sSQLWhere += string.Format("{0} Like '%{1}%'", acol[0], sValue);
                    if (i != aFiltros.Length - 1)
                    {
                        sSQLWhere += " And ";
                    }
                }
            }

            //Agregar filtro de fechas
            sSQLWhere += string.Format(" And ");

        }

        if (sTipoInformacion != "1")
        {
            //Agregar filtro de fechas
            sSQLWhere += string.Format(" {0} Between '{1}' And '{2}'", "FechaDeProcesamiento", dtpFechaInicial, dtpFechaFinal);
        }
        else
        {
            dtpFechaInicial = string.Format("{0}-01", dtpFechaInicial.Substring(0, 7));
            //Agregar filtro de fechas
            sSQLWhere += string.Format(" {0} = '{1}'", "FechaDeProcesamiento", dtpFechaInicial);
        }


        //Tipo de reportes
        if (sTipoReporte != "" && sTipoReporte != "0")
        {
            if (sTipoReporte == "1")
            {
                sSQLWhere += string.Format(" And FolioVale = ''");
            }
            else if (sTipoReporte == "2")
            {
                sSQLWhere += string.Format(" And FolioVale <> ''");
            }

        }

        //Tipo de información
        if (sTipoInformacion != "" && sTipoInformacion != "2")
        {
            if (sTipoInformacion == "0")
            {
                sSQLWhere += string.Format(" And EsFacturado = '0'");
            }
            else if (sTipoInformacion == "1")
            {
                sSQLWhere += string.Format(" And EsFacturado = '1'");
            }

        }

        //Filtro de farmacias
        if (sIdFarmacia != "*")
        {
            sSQLWhere += string.Format(" And IdFarmacia = '{0}'", sIdFarmacia);
        }

        //Agregar Group By
        sSQLGroupBy += "Group By ";
        for (int i = 0; i < aGroup.Length; i++)
        {
            sSQLGroupBy += string.Format("{0}", aGroup[i]);
            if (i != aGroup.Length - 1)
            {
                sSQLGroupBy += ", ";
            }
        }

        //Agregar Orderby
        if (sOrdenacion != "")
        {
            sSQLOrderBy += "Order By ";
            for (int i = 0; i < aOrdenacion.Length; i++)
            {
                string[] acol = aOrdenacion[i].Split('|');
                if (acol[1].Trim() != "")
                {
                    string sValue = acol[1].Trim();
                    if (acol[2] == "sum")
                    {
                        sSQLOrderBy += string.Format("sum({0}) {1}", acol[0], sValue);
                    }
                    else
                    {
                        sSQLOrderBy += string.Format("{0} {1}", acol[0], sValue);
                    }


                    if (i != aOrdenacion.Length - 1)
                    {
                        sSQLOrderBy += ", ";
                    }
                }
            }
        }

        string sTable = string.Empty;
        
        switch (DtGeneral.Empresa)
        {
            case "002":
                sTable = "Rpt_CTE_ConsumosFacturados";
                break;
            case "003":
                sTable = "INT_ND_RptAdmonDispensacion_Informacion_PaginaWeb";
                break;
            default:
                break;
        }
        
        string sSql = string.Format("Set DateFormat YMD " +
                                    "Select " +
                                        "{0} " +
                                    //"From Rpt_CTE_ConsumosFacturados (NoLock) " +
                                    "From {4} (NoLock) " +
                                    "{1} " +
                                    "{2} " +
                                    "{3}", sSQLColumnas, sSQLWhere, sSQLGroupBy, sSQLOrderBy, sTable);


        HttpContext.Current.Session["Reporteador"] = DtGeneral.ExecQuery(sSql, "Info");
        if (HttpContext.Current.Session["Reporteador"] != null)
        {
            if (((DataSet)HttpContext.Current.Session["Reporteador"]).Tables[0].Rows.Count > 0)
            {
                bEjecuto = true;
            }
            else
            {
                sMensaje = "No se encontró información con los criterios especificados.";
            }
        }

        lst.Add("Mensaje", sMensaje);
        lst.Add("Resultado", iResultado);
        lst.Add("Ejecuto", bEjecuto.ToString());

        sEstructura = serializer.Serialize(lst);

        //Auditor
        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmReporteador");

        return sEstructura;
    }

    [WebMethod]
    public static string ExistenciaPorClave(string sClaveSSA, string dtpFechaInicial)
    {
        string sReturn = string.Empty;
        string sSql = string.Format("If object_id('tempdb..#tmpFarmacias') Is Not Null " +
		                                "Drop Table #tmpFarmacias; " +
                                    
                                    "If object_id('tempdb..#tmpInformacion') Is Not Null " + 
		                                "Drop Table #tmpInformacion; " +
                                    
                                    "If object_id('tempdb..#tmpInformacionNoExistencia') Is Not Null " +
		                                "Drop Table #tmpInformacionNoExistencia; " +
	                                
                                    "Set DateFormat YMD " +
                                    
                                    "Select " +
                                        "F.IdFarmacia, F.Farmacia As Farmacia, 0 As Existencia, " +
                                        "(Case When " +
                                            "( " +
                                                "Select ClaveSSA From vw_CB_CuadroBasico_Farmacias CB (NoLock) " +
                                                "Where  CB.IdEstado = '21' And CB.IdFarmacia = F.IdFarmacia And CB.ClaveSSA = '010.000.0104.00' And CB.StatusMiembro = 'A' And CB.StatusClave = 'A' " +
                                                "Group By ClaveSSA " +
                                            ") " +
                                        "Is Null Then 'No' Else 'Si' End) As EnCuadroBasico " +
                                    "Into #tmpFarmacias " +
                                    "From vw_Farmacias F (NoLock) "+
                                    "Where F.IdEstado = '{1}' And F.IdTipoUnidad Not In ('000', '005', '006') And F.Status = 'A' " +
                                    
                                    "Select " +
                                        "E.IdFarmacia, F.NombreFarmacia As Farmacia, " +
                                        "Cast(SUM(E.ExistenciaDisponible) As Int) As Existencia " +
                                    "Into #tmpInformacion " +
                                    "From FarmaciaProductos_CodigoEAN_Lotes__Historico E(NoLock) " +
                                    "Inner Join CatFarmacias F On (F.IdEstado = E.IdEstado And F.IdFarmacia = E.IdFarmacia) " +
                                    "Inner Join vw_Productos_CodigoEAN P On (E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN) " +
                                    "Where " +
                                        "E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.FechaOperacion = '{3}'  And P.ClaveSSA = '{2}' " +
                                    "Group By E.IdFarmacia, F.NombreFarmacia "+

                                    "Update F " +
		                                "Set Existencia = I.Existencia " +
                                    "From #tmpFarmacias F " +
                                    "Inner Join #tmpInformacion I On ( F.IdFarmacia = I.IdFarmacia ) " +
	    
                                    "Select " +
                                        "IdFarmacia, Existencia " +
                                    "Into #tmpInformacionNoExistencia " +
                                    "From #tmpFarmacias (NoLock) " +
                                    "Where Existencia = 0 " +
                                    "Order By IdFarmacia " +
		
                                    "Update F " +
		                                "Set Existencia = IsNull( " +
							                                "(Select " +
								                                "Top 1  " +
								                                "Cast(SUM(E.ExistenciaDisponible) As Int) As Existencia " +
							                                "From FarmaciaProductos_CodigoEAN_Lotes__Historico E(NoLock) " +
							                                "Inner Join vw_Productos_CodigoEAN P On (E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN) " +
							                                "Where E.IdEmpresa = '{0}' And E.IdEstado = '{1}' And E.IdFarmacia = F.IdFarmacia And P.ClaveSSA = '{2}' And Convert(varchar(10), E.FechaOperacion, 120) >= DATEADD(mm, -1, '{3}') And Convert(varchar(10), E.FechaOperacion, 120) < '{3}' " +
							                                "Group By E.FechaOperacion " +
							                                "Order by E.FechaOperacion Desc " +
						                                "), 0) " +
                                    "From #tmpInformacionNoExistencia F " +
	
                                    "Update F  " +
		                                "Set Existencia = I.Existencia " +
                                    "From #tmpFarmacias F " +
                                    "Inner Join #tmpInformacionNoExistencia I On ( F.IdFarmacia = I.IdFarmacia ) " +
	
                                    "Select " +
                                        "IdFarmacia As 'Id Farmacia', " +
                                        "Farmacia, Existencia, EnCuadroBasico As 'En Cuadro Básico' " +
                                    "From #tmpFarmacias (NoLock) " +
                                    "Order By IdFarmacia", DtGeneral.Empresa, DtGeneral.EstadoConectado, sClaveSSA, dtpFechaInicial);

        
        //Variables de Sesión
        HttpContext.Current.Session["ExistenciaPorClave"] = DtGeneral.ExecQuery(sSql);
        sReturn = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["ExistenciaPorClave"], "ExistenciaPorClave");

        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmExistenciaPorcClave");

        return sReturn;
    }

    [WebMethod]
    public static string GetPenalizacionesTapachula()
    {
        string sEstructura = string.Empty;
        string sSql = string.Empty;

        sSql = string.Format("Select " +
			                    "Consecutivo As 'Index', " +
                                "MontoContratoSinIva As 'Monto del Contrato Sin IVA', " +
                                "MontoPorDia As 'Monto por Día ', " +
                                "FactorPenalizacion As 'Factor Penalización', " +
                                "TotalPenalizacionPorDia As 'Total de Penalización por Día', " +
                                "DiasIncumplidos As 'Días Incumplidos', " +
                                "TotalPenalizacion As 'Total Penalización', " +
                                "PeriodoPenalizacion As 'Periodo de Penalización' " +
                            "From Rpt_CTE_Penalizaciones (NoLock)");

        //Variables de Sesión
        HttpContext.Current.Session["Penalizaciones"] = DtGeneral.ExecQuery(sSql, "Penalizaciones");
        sEstructura = DtGeneral.DtsToTableHtml(((DataSet)HttpContext.Current.Session["Penalizaciones"]).Tables["Penalizaciones"], "Penalizaciones");

        string sCadena = sSql.Replace("'", "\"");
        clsAuditoria.GuardarAud_MovtosReg(sCadena, "frmPenalizacionTapachula");

        return sEstructura;
    }
    #endregion Reportes

    #region Funciones Privadas
    private static string ArmarSelect(string sOpc)
    {
        string sRegresa = "";

        switch (sOpc)
        {
            case "1":
                sRegresa = "Select 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClave, CodigoEAN, " +
                    "'Nombre comercial' = Descripcion, Presentacion, Laboratorio " +
                    "From vw_Productos_CodigoEAN " +
                    "Where EsSectorSalud = 1 " +
                    "Order By DescripcionClave ";
                break;

            case "2":
                sRegresa = "Select 'Clave SSA' = ClaveSSA, CodigoEAN, RegistroSanitario, " +
                    "'Nombre comercial' = Descripcion, Presentacion, Laboratorio " +
                    "From vw_Productos_RegistrosSanitarios " +
                    "Order By DescripcionClave ";
                break;

            case "3":
                sRegresa = string.Format("Select 'Clave SSA' = ClaveSSA, 'Descripción' =  DescripcionClave, 'Presentación' = Presentacion " +
                "From vw_CB_CuadroBasico_Claves CB (NoLock) " +
                "Where StatusMiembro =  'A' and StatusClave = 'A' and CB.idestado = '{0}' " +
                "Group by ClaveSSA, DescripcionClave, Presentacion " +
                "Order by DescripcionClave ",
                DtGeneral.EstadoConectado);
                break;

            case "4":
                sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                "From vw_Productos " +
                "Where EsAntibiotico = 1 " +
                "Order By ClaveSSA";
                break;

            case "5":
                sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                "From vw_Productos " +
                "Where EsControlado = 1 " +
                "Order By ClaveSSA";
                break;

            case "6":
                sRegresa = "Select IdProducto, ClaveSSA, Descripcion " +
                "From vw_Productos " +
                "Where IdSegmento = '01' " +
                "Order By ClaveSSA";
                break;
        }

        return sRegresa;
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
    #endregion Funciones Privadas
}