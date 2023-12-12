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
        if (HttpContext.Current.Request.Cookies["Recetario"] != null)
        {
            if (HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] != null && Convert.ToBoolean(HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"]))
            {
                Response.Redirect("~/UsuariosyPermisos");
            }
        }

        if (!IsPostBack)
        {
            DtGeneral.Init();
            titulo.InnerHtml = DtGeneral.NombreLogin;
            //cboEstado.InnerHtml = ClsToolsHtml.OptionDropList(DtGeneral.CatEstadosFarmacias, "IdEstado", "Estado", true, "Selecciona un Estado");

            //wrapper.InnerHtml = string.Format("<div class=\"title_login\">Inicia sesión</div>" +
            //    "<select name=\"Estado\" id=\"cboEstado\" class=\"icoestado\" tabindex=\"1\">" +
            //        "{0}" +
            //    "</select>" +
            //    "<select name=\"Farmacia\" id=\"cboFarmacia\" class=\"icofarmacia\" disabled=\"disabled\" tabindex=\"2\">" +
            //        "<option value=\"0\">&lt;&lt;Seleccione una Clínica&gt;&gt</option> " +
            //    "</select>"+
            //    "<input id=\"txtUser\" type=\"text\"  class=\"icouser input_txt\" placeholder=\"Clave Médico\" maxlength=\"500\" tabindex=\"3\"/>" +
            //    "<input id=\"txtPassword\" type=\"password\"  class=\"icopassword input_txt\" name=\"Contrasena\" value=\"\" placeholder=\"Contraseña\" maxlength=\"500\" tabindex=\"4\"/>" +
            //    "<div id=\"btn_sign\" tabindex=\"5\">INGRESAR</div>", ClsToolsHtml.OptionDropList(DtGeneral.getEstados(), "IdEstado", "Estado", true, true, "Seleccione un Estado"));

        }
    }
}