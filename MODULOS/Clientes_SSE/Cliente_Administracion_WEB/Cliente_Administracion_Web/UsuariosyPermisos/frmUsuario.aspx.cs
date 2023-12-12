using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UsuariosyPermisos_frmUsuario : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DtGeneral.Empresa == "001")
            {
                btnAdd.Attributes.Add("class", "m-btn green");
            }
            else
            {
                btnAdd.Attributes.Add("class", "m-btn blue");
            }
            contFarmacia.InnerHtml ="<span class=\"add-on\">Farmacia :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Farmacia", "", "", "", "", false, true) +
                                    "</select>";
            
        }
    }
}