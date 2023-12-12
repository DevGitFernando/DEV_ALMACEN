using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

using DevExpress.Web;


using System.Data;


//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using System.Collections;
using DevExpress.Export;
using DevExpress.Web.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

using Cliente_Regional.Code;

using System.IO;

public partial class Generales_frm_BI_UNI_RPT__001__ClavesSSA__Unidosis : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RestartControls();
    }
    protected void Menu_ItemClick(object source, MenuItemEventArgs e)
    {
        var name = e.Item.Name;
        if (IsPostBack)
        {
            switch (name)
            {
                case "Save":
                    //Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
                    //https://www.intermedgto.mx:8443/BI/frameset?__report=UNI_RPT__001__ClavesSSA__Unidosis.rptdesign
                    string sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&IdEmpresa={2}&IdEstado={3}", DtGeneral.ServidorBI, "UNI_RPT__001__ClavesSSA__Unidosis", DtGeneral.IdEmpresa, DtGeneral.IdEstado);
                    FrameResult.GetPaneByName("ContentUrlPane").ContentUrl = sUrlReporte;
                    break;
                case "New":
                    RestartControls();
                    break;
            }
        }
    }

    private void RestartControls()
    {
        // -- Reporte -- 
        FrameResult.GetPaneByName("ContentUrlPane").ContentUrl = "";
    }
}