using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Generales_frm_BI_RPT__008__Claves_NoSuministradas__Stock : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            //DumpExcel(Request.Form["__EVENTARGUMENT"]);
        }

        if(!IsPostBack)
        {
            clsConsultas Consultas = new clsConsultas();

            Combos.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Tipo de unidad :</span>" +
                                    "<select id=\"cboTipoUnidad\" class=\"m-wrap\">" +
                                    Consultas.GetUnidadesAlmacen("Unidad", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                    Consultas.GetUnidadesAlmacen("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Localidad :</span>" +
                                    "<select id=\"cboLocalidad\" class=\"m-wrap\">" +
                                    Consultas.GetUnidadesAlmacen("Municipio", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Almacen :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                    Consultas.GetUnidadesAlmacen("Farmacia", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
            //Benfeciarios            
            DataSet dtsBeneficiarios = Consultas.GetBeneficiariosAlmacen();
            Beneficiarios.InnerHtml = string.Format(@"<label class='m-wrap inline'>
                                                             <span class='add-on'>Clave Unidad:</span>
                                                            <select id ='cboBeneficiarios' class='m-wrap'>{0}</select>
                                                        </label>", clsToolsHtml.OptionDropList(dtsBeneficiarios.Tables[0], "IdBeneficiario", "NombreCompleto", 1, true, "*", "Todas las unidades"));

            JurisdiccionEntrega.InnerHtml = String.Format(@"<label class='m-wrap inline'>
                                                                <span class='add-on'>Juris Entrega :</span>
                                                                <select id ='cboJurisdiccionEntrega' class='m-wrap'>{0}</select>
                                                            </label>", DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true));

        }
    }
}