using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DllClienteRegionalWeb_FrmAbastoSurtimiento : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ComboJF.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    DtGeneral.CreateDropList("cboJurisdiccion", "m-wrap", DtGeneral.Jurisdicciones, "IdJurisdiccion", "NombreJurisdiccion", true) +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Farmacia :</span>" +
                                    DtGeneral.CreateDropList("cboFarmacia", "m-wrap") +
                                "</label>" +
                                "<div class=\"clear\"></div>";
    }
}