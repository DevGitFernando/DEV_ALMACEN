using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Generales_frm_BI_RPT__003__Entradas_y_Salidas : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            //DumpExcel(Request.Form["__EVENTARGUMENT"]);
        }

        if (!IsPostBack)
        {
            Combos.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Tipo de unidad :</span>" +
                                    "<select id=\"cboTipoUnidad\" class=\"m-wrap\">" +
                                        //DtGeneral.GetInfo("Unidad", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                        //DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Localidad :</span>" +
                                    "<select id=\"cboLocalidad\" class=\"m-wrap\">" +
                                        //DtGeneral.GetInfo("Municipio", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Farmacia :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                        //DtGeneral.GetInfo("Farmacia", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";

        }
    }
}