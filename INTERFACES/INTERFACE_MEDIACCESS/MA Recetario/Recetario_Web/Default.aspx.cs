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
        if (HttpContext.Current.Request.Cookies["Recetario"] != null)
        {
            if (HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] == null || !Convert.ToBoolean(HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"]))
            {
                HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/Login.aspx");
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/UsuariosyPermisos");
            }
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/Login.aspx");
        }
    }
}