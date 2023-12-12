using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DllRecetario_FrmRecetario : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //consultainfo.InnerHtml = string.Format("<div class=\"labelGeneral spacelabel alineado\"> Medico tratante</div>" +
            //       "<select id=\"cboMedico\" class=\"nmedico alineado\" name=\"Medico\" disabled=\"disabled\" >" +
            //        "<option selected= \"selected\">{0}</option>" +
            //       "</select>" +
            //       "<textarea  class=\"observaciones\" name=\"comentarios\" placeholder=\"Escriba aquí sus observaciones\" rows=\"\" cols=\"\"></textarea>" +
            //      "<div  id=\"btnaboratorio\" class=\" alineado spacelabel\">Laboratorio</div>", HttpContext.Current.Request.Cookies["Recetario"]["Personal"]);
        }
    }
}