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
public partial class frm_BI_UNI_RPT__014__Validacion__Remisiones : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RestartControls();
        }
    }
    protected void Menu_ItemClick(object source, MenuItemEventArgs e)
    {
        var name = e.Item.Name;
        if (IsPostBack)
        {
            switch (name)
            {
                case "Save":

                    DateTime dtFechaI = (DateTime)DtFechaInicio.Value;
                    DateTime dtFechaF = (DateTime)DtFechaFin.Value;

                    //EXTRAS (Municipio,Jurisdiccion, Farmacia,Titulo)
                    string sMunicipio = cboLocalidad.Text;
                    string sJurisdiccion = cboJurisdiccion.Text;
                    string sFarmacia = cboFarmacia.Text;
                    string sTitulo = "Consumos para validación y remisión para facturación";

                    //Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
                    //https://www.intermedgto.mx:8443/BI/frameset?__report=UNI_RPT__014__Validacion__Remisiones.rptdesign&IdEmpresa=001&IdEstado=11&IdMunicipio=*&IdJurisdiccion=*&IdFarmacia=*&FechaInicial=2021-11-14&FechaFinal=2021-12-14&Remision=
                    //string sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&IdEmpresa={2}&IdEstado={3}&IdMunicipio={4}&IdJurisdiccion={5}&IdFarmacia={6}&FechaInicial={7}&FechaFinal={8}&Remision={8}", DtGeneral.ServidorBI, "UNI_RPT__014__Validacion__Remisiones", DtGeneral.IdEmpresa, DtGeneral.IdEstado, cboLocalidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboFarmacia.Value.ToString(), dtFechaI.ToString("yyyy-MM-dd"), dtFechaF.ToString("yyyy-MM-dd"), txtRemision.Text);

                    string sUrlReporte = "";

                    sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&", DtGeneral.ServidorBI, "UNI_RPT__014__Validacion__Remisiones");
                    sUrlReporte += string.Format("IdEmpresa={0}&", DtGeneral.IdEmpresa);
                    sUrlReporte += string.Format("IdEstado={0}&", DtGeneral.IdEstado);
                    sUrlReporte += string.Format("IdMunicipio={0}&", cboLocalidad.Value.ToString());
                    sUrlReporte += string.Format("IdJurisdiccion={0}&", cboJurisdiccion.Value.ToString());
                    sUrlReporte += string.Format("IdFarmacia={0}&", cboFarmacia.Value.ToString());
                    sUrlReporte += string.Format("FechaInicial={0}&", dtFechaI.ToString("yyyy-MM-dd"));
                    sUrlReporte += string.Format("FechaFinal={0}&", dtFechaF.ToString("yyyy-MM-dd"));
                    sUrlReporte += string.Format("Remision={0}&", txtRemision.Text);

                    sUrlReporte += string.Format("Municipio={0}&", sMunicipio);
                    sUrlReporte += string.Format("Jurisdiccion={0}&", sJurisdiccion);
                    sUrlReporte += string.Format("Farmacia={0}&", sFarmacia);
                    sUrlReporte += string.Format("Titulo={0}", sTitulo);

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
        // -- Fechas - -
        DtFechaInicio.Date = DateTime.Now;
        DtFechaInicio.MaxDate = DateTime.Now;
        DtFechaFin.Date = DateTime.Now;
        DtFechaFin.MaxDate = DateTime.Now;
        // -- Combos -- 
        Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, "*", "*", "*");
        cboFarmacia.SelectedIndex = 0;
        // -- Parámetros -- 
        txtRemision.Text = "";
        txtServicio.Text = "";
        // -- Reporte -- 
        FrameResult.GetPaneByName("ContentUrlPane").ContentUrl = "";
    }

    protected void cboUnidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
    }

    protected void cboJurisdiccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filtros_Combos(DtGeneral.FiltrosCombos.Jurisdiccion, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
    }

    protected void cboLocalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filtros_Combos(DtGeneral.FiltrosCombos.Municipio, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
    }

    protected void Filtros_Combos(Enum Filtro, string sIdTipoDeUnidad, string sIdJurisdiccion, string sIdMunicipio)
    {
        switch (Filtro.ToString())
        {
            case "TipoDeUnidad":
                sIdJurisdiccion = "*";
                sIdMunicipio = "*";
                break;
            case "Jurisdiccion":
                sIdMunicipio = "*";
                break;
        }

        cboUnidad.DataSource = ClsConsultas.TiposDeUnidad();
        cboUnidad.DataBind();

        cboJurisdiccion.DataSource = ClsConsultas.Jurisdicciones(sIdTipoDeUnidad);
        cboJurisdiccion.DataBind();

        cboLocalidad.DataSource = ClsConsultas.Localidades(sIdTipoDeUnidad, sIdJurisdiccion);
        cboLocalidad.DataBind();

        //cboFarmacia.DataSource = ClsConsultas.Farmacias(sIdTipoDeUnidad, sIdJurisdiccion, sIdMunicipio);
        //cboFarmacia.DataBind();

        cboFarmacia.DataSource = ClsConsultas.FarmaciasUnidosis();
        cboFarmacia.DataBind();

        switch (Filtro.ToString())
        {
            case "TipoDeUnidad":
                cboUnidad.Value = sIdTipoDeUnidad;
                cboJurisdiccion.SelectedIndex = 0;
                cboLocalidad.SelectedIndex = 0;
                cboFarmacia.SelectedIndex = 0;
                break;
            case "Jurisdiccion":
                cboUnidad.Value = sIdTipoDeUnidad;
                cboJurisdiccion.Value = sIdJurisdiccion;
                cboLocalidad.SelectedIndex = 0;
                cboFarmacia.SelectedIndex = 0;
                break;
            case "Municipio":
                cboUnidad.Value = sIdTipoDeUnidad;
                cboJurisdiccion.Value = sIdJurisdiccion;
                cboLocalidad.Value = sIdMunicipio;
                cboFarmacia.SelectedIndex = 0;
                break;
        }
    }
}