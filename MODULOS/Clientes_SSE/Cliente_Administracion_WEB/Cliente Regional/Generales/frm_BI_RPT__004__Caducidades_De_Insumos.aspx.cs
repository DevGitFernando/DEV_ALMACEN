﻿using System;
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
public partial class frm_BI_RPT__004__Caducidades_De_Insumos : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //cboSemafo.Items.Add(new DevExpress.Web.ListEditItem("Todos"));
        //cboSemafo.Items.Add(new DevExpress.Web.ListEditItem("Proximos a caducar"));

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
                    //Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
                    //https://www.intermedgto.mx:8443/BI/frameset?__report=RPT__004__Caducidades_De_Insumos.rptdesign&IdEmpresa=001&IdEstado=11&IdMunicipio=0015&IdJurisdiccion=001&IdFarmacia=3131&Fecha=2021-11-08&Status_Semaforizacion=0&Procedencia=
                    DateTime dtFecha = (DateTime)DtFecha.Value;
                    string sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&IdEmpresa={2}&IdEstado={3}&IdMunicipio={4}&IdJurisdiccion={5}&IdFarmacia={6}&Fecha={7}&Status_Semaforizacion={8}&Procedencia={9}", DtGeneral.ServidorBI, "RPT__004__Caducidades_De_Insumos", DtGeneral.IdEmpresa, DtGeneral.IdEstado, cboLocalidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboFarmacia.Value.ToString(), dtFecha.ToString("yyyy-MM-dd"), cboSemafo.Value, txtProcedencia.Text);

                    sUrlReporte = "";

                    sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&", DtGeneral.ServidorBI, "RPT__004__Caducidades_De_Insumos");
                    sUrlReporte += string.Format("IdEmpresa={0}&", DtGeneral.IdEmpresa);
                    sUrlReporte += string.Format("IdEstado={0}&", DtGeneral.IdEstado);
                    sUrlReporte += string.Format("IdMunicipio={0}&", cboLocalidad.Value.ToString());
                    sUrlReporte += string.Format("IdJurisdiccion={0}&", cboJurisdiccion.Value.ToString());

                    sUrlReporte += string.Format("IdFarmacia={0}&", cboFarmacia.Value.ToString());
                    sUrlReporte += string.Format("Fecha={0}&", dtFecha.ToString("yyyy-MM-dd"));
                    sUrlReporte += string.Format("Status_Semaforizacion={0}&", cboSemafo.Value);
                    sUrlReporte += string.Format("Procedencia={0}", txtProcedencia.Text); // el ultimo parámentro no requiere el & 
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
        // -- Fechas --
        DtFecha.Date = DateTime.Now;
        // -- Combos -- 
        Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, "*", "*", "*");
        cboFarmacia.SelectedIndex = 0;
        // -- Parámetros -- 
        cboSemafo.SelectedIndex = 0;
        txtProcedencia.Text = "";
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

        cboFarmacia.DataSource = ClsConsultas.Farmacias(sIdTipoDeUnidad, sIdJurisdiccion, sIdMunicipio);
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