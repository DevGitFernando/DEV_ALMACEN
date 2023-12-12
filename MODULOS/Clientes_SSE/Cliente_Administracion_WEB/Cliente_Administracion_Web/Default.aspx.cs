using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Header.Title = DtGeneral.NombreModulo;
        if ((Session["Autenticado"] == null) || (!(bool)Session["Autenticado"]))
        {
            //Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
            frmPrincipal.Attributes.Add("src", "UsuariosyPermisos/EdoLogin.aspx");
            Title = "Autenticación de usuarios";
        }
        else
        {
            //Response.Redirect("~/UsuariosyPermisos/Main.aspx");
            //Response.Redirect("~/UsuariosyPermisos");
            frmPrincipal.Attributes.Add("src", "UsuariosyPermisos");
            Title = DtGeneral.NombreModulo;
        }
    }
}