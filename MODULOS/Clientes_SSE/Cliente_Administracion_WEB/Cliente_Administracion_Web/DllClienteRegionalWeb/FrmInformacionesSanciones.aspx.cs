using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DllClienteRegionalWeb_FrmInformacionesSanciones : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ComboJ.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    DtGeneral.CreateDropList("cboJurisdiccion", "m-wrap", DtGeneral.Jurisdicciones, "IdJurisdiccion", "NombreJurisdiccion", true) +
                                "</label>" +
                                "<div class=\"clear\"></div>";
    }
}