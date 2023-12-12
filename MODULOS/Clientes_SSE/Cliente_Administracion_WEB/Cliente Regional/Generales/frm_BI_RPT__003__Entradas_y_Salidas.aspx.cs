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

public partial class Generales_frm_BI_RPT__003__Entradas_y_Salidas : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Init controles
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
                    string sValidar_Entradas = ChecEntrada.Checked ? "1":"0";
                    string sValidar_Salidas = ChecSalida.Checked ? "1" : "0";

                    //Información Extra
                    string sTitulo = EncFormTitle.InnerHtml;
                    string sTipoUnidad = cboUnidad.Text;
                    string sJurisdiccion = cboJurisdiccion.Text;
                    string sMunicipio = cboLocalidad.Text;
                    string sFarmacia = cboUnidad.Text;

                    //Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
                    //https://www.intermedgto.mx:8443/BI/frameset?__report=RPT__003__Entradas_y_Salidas.rptdesign&IdEmpresa=001&IdEstado=11&IdMunicipio=*&IdJurisdiccion=*&IdFarmacia=*&TipoMovto=0&ClaveSSA=&FechaInicial=2021-11-13&FechaFinal=2021-12-13&FuenteDeFinanciamiento=&Semaforizacion=0&Validar_Entradas=0&Entrada_Menor=&Entrada_Mayor=&Validar_Salidas=0&Salida_Menor=&Salida_Mayor=
                    string sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&IdEmpresa={2}&IdEstado={3}&IdMunicipio={4}&IdJurisdiccion={5}&IdFarmacia={6}&TipoMovto={7}&ClaveSSA={8}&FechaInicial={9}&FechaFinal={10}&FuenteDeFinanciamiento={11}&Semaforizacion={12}&Validar_Entradas={13}&Entrada_Menor={14}&Entrada_Mayor={15}&Validar_Salidas={16}&Salida_Menor={17}&Salida_Mayor={18}&Titulo={19}&TipoUnidad={20}&Jurisdiccion={21}&Municipio={22}&Farmacia={23}",
                            //DtGeneral.ServidorBI, "RPT__003__Entradas_y_Salidas", DtGeneral.IdEmpresa, DtGeneral.IdEstado, cboLocalidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboFarmacia.Value.ToString(), txtProcedencia.Text, txtSSA.Text, dtFechaI.ToString("yyyy-MM-dd"), dtFechaF.ToString("yyyy-MM-dd"), txtFuente.Text, cboSemafo.Value.ToString(), ChecEntrada.Value, deInicialE.Value.ToString(), deFinalE.Value.ToString(), ChecSalida.Value, deInicialS.Value.ToString(), deFinalS.Value.ToString());
                            DtGeneral.ServidorBI, "RPT__003__Entradas_y_Salidas", DtGeneral.IdEmpresa, DtGeneral.IdEstado, 
                            cboLocalidad.Value.ToString(), cboJurisdiccion.Value.ToString(), 
                            cboFarmacia.Value.ToString(), 
                            txtProcedencia.Text, txtSSA.Text, dtFechaI.ToString("yyyy-MM-dd"), dtFechaF.ToString("yyyy-MM-dd"), txtFuente.Text, cboSemafo.Value.ToString(), 
                            sValidar_Entradas, deInicialE.Value.ToString(), deFinalE.Value.ToString(), sValidar_Salidas, deInicialS.Value.ToString(), deFinalS.Value.ToString(), 
                            sTitulo, sTipoUnidad, sJurisdiccion, sMunicipio, sFarmacia);


                    sUrlReporte = "";

                    sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&", DtGeneral.ServidorBI,"RPT__003__Entradas_y_Salidas");
                    sUrlReporte += string.Format("IdEmpresa={0}&", DtGeneral.IdEmpresa);
                    sUrlReporte += string.Format("IdEstado={0}&", DtGeneral.IdEstado);
                    sUrlReporte += string.Format("IdMunicipio={0}&", cboLocalidad.Value.ToString());
                    sUrlReporte += string.Format("IdJurisdiccion={0}&", cboJurisdiccion.Value.ToString());

                    sUrlReporte += string.Format("IdFarmacia={0}&", cboFarmacia.Value.ToString());
                    sUrlReporte += string.Format("TipoMovto={0}&", "0");
                    sUrlReporte += string.Format("ClaveSSA={0}&", txtSSA.Text.Trim());
                    sUrlReporte += string.Format("FechaInicial={0}&", dtFechaI.ToString("yyyy-MM-dd"));
                    sUrlReporte += string.Format("FechaFinal={0}&", dtFechaF.ToString("yyyy-MM-dd"));

                    sUrlReporte += string.Format("TipoDeClave={0}&", "0");
                    sUrlReporte += string.Format("FuenteDeFinanciamiento={0}&", "");
                    sUrlReporte += string.Format("Procedencia={0}&", txtProcedencia.Text.Trim());
                    sUrlReporte += string.Format("Semaforizacion={0}&", cboSemafo.Value.ToString());

                    sUrlReporte += string.Format("Validar_Entradas={0}&", sValidar_Entradas);
                    sUrlReporte += string.Format("Entrada_Menor={0}&", deInicialE.Value.ToString());
                    sUrlReporte += string.Format("Entrada_Mayor={0}&", deFinalE.Value.ToString());

                    sUrlReporte += string.Format("Validar_Salidas={0}&", sValidar_Salidas);
                    sUrlReporte += string.Format("Salida_Menor={0}&", deInicialS.Value.ToString());
                    sUrlReporte += string.Format("Salida_Mayor={0}&", deFinalS.Value.ToString());

                    sUrlReporte += string.Format("Titulo={0}&", sTitulo);
                    sUrlReporte += string.Format("TipoUnidad={0}&", sTipoUnidad);
                    sUrlReporte += string.Format("Jurisdiccion={0}&", sJurisdiccion);
                    sUrlReporte += string.Format("Municipio={0}&", sMunicipio);
                    sUrlReporte += string.Format("Farmacia={0}", sFarmacia);   // el ultimo parámentro no requiere el & 

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
        DtFechaInicio.Date = DateTime.Now;
        DtFechaInicio.MaxDate = DateTime.Now;
        DtFechaFin.Date = DateTime.Now;
        DtFechaFin.MaxDate = DateTime.Now;
        Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, "*", "*", "*");

        deInicialE.Value = 0;
        deInicialS.Value = 0;
        deFinalE.Value = 0;
        deFinalS.Value = 0;

        ChecEntrada.Checked = false;
        ChecSalida.Checked = false;

        cboFarmacia.SelectedIndex = 0;
        cboSemafo.SelectedIndex = 0;
        txtFuente.Text = "";
        txtSSA.Text = "";
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