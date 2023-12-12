using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

public partial class UsuariosyPermisos_EdoLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Autenticado"] == null)
        {
            Session["Autenticado"] = false;
        }

        if ((bool)Session["Autenticado"])
        {
            Response.Redirect("~/Default.aspx");
        }
        //else if (!IsPostBack)
        else
        {
            //Obtenere datos de Estado y sucursal
            DtGeneral.DatosDeConexion();
            DtGeneral.queryPersolizado();

            //Cargar Estados
            DtGeneral.GetEstados();

           Header.InnerHtml =  "<link rel=\"shortcut icon\" href=\"../images/favicon.ico\" /> " +
                                "<link rel=\"stylesheet\" href=\"../css/login.css\" type=\"text/css\" /> " +
                                "<link rel=\"stylesheet\" href=\"../css/login_" + DtGeneral.Empresa + ".css\" type=\"text/css\" /> " +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/jquery.fancybox.css?v=2.1.2\" media=\"screen\" />" +
                                "<script type=\"text/javascript\" src=\"https://code.jquery.com/jquery-1.8.3.js\"></script> " +
                                "<script type=\"text/javascript\" src=\"https://code.jquery.com/ui/1.9.1/jquery-ui.js\"></script>" +
                                "<script type=\"text/javascript\" src=\"../js/jquery.fancybox.js?v=2.1.3\"></script>" +
                                "<script type=\"text/javascript\" src=\"../js/login.js\"></script> " +
                                "<script type=\"text/javascript\" charset=\"utf-8\">" +
                                    "var Id='" + DtGeneral.Empresa + "';" +
                                "</script>";

            Title = "Autenticación de usuarios";
            //NombreEmpresa.InnerHtml = DtGeneral.GetNombreCortoEmpresa(DtGeneral.Empresa);
            //NombreEmpresa.InnerHtml = "INTERMED";
            string sNombreEmpresa = DtGeneral.GetNombreCortoEmpresa(DtGeneral.Empresa);
            if (DtGeneral.Empresa == "003")
            {
                NombreEmpresa.InnerHtml = "Bienvenido";
            }
            else
            {
                NombreEmpresa.InnerHtml = sNombreEmpresa;
            }
            
            if (DtGeneral.Empresa == "")
            {
                Session["Autenticado"] = false;
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    #region Servicios Web
    [WebMethod()]
    public static string Autenticar(string sUser, string sPass)
    {
        string sReturn = string.Empty;

        clsLogin.Empresa = DtGeneral.Empresa;
        clsLogin.Estado = DtGeneral.EstadoConectado;
        clsLogin.Sucursal = DtGeneral.Sucursal;
        clsLogin.Usuario = sUser;
        clsLogin.Password = sPass;
        clsLogin.Arbol = DtGeneral.Arbol;

        if (clsLogin.AutenticarUsuarioLogin())
        {
            //DtGeneral.Autenticado = true;
            HttpContext.Current.Session["SesionActual"] = "";
            HttpContext.Current.Session["Autenticado"] = true;
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
    #endregion Servicios Web
}