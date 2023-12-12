using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UsuariosyPermisos_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["RptsEjecutivos"] != null)
        {
            if (HttpContext.Current.Request.Cookies["RptsEjecutivos"]["Autenticado"] != null && Convert.ToBoolean(HttpContext.Current.Request.Cookies["RptsEjecutivos"]["Autenticado"]))
            {
                HttpContext.Current.Response.Redirect("~/main.aspx");
            }
        }

        if (!IsPostBack)
        {
            DtGeneral.Init();
        }
    }
}