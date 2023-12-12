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

public partial class Generales_frm_BI_RPT__008__Claves_NoSuministradas__Stock : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSSA.Visible = false;
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
                    //Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, cboUnidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboLocalidad.Value.ToString());
                    //https://www.intermedgto.mx:8443/BI/frameset?__report=RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos.rptdesign&IdEmpresa=001&IdEstado=11&IdMunicipio=0048&IdJurisdiccion=001&IdFarmacia=3005&FechaInicial=2022-04-11&FechaFinal=2022-05-11&ClaveSSA=&ClaveUnidad_Beneficiario=00000004&Unidad_Beneficiario=&IdJurisdiccionEntrega=001&JurisdiccionEntrega=&Titulo=Claves%20de%20medicamento%20y%20material%20de%20curaci%C3%B3n%20suministradas%20por%20stock%20a%20la%20unidad%20m%C3%A9dica%20conforme%20a%20su%20calendario%20y%20programaci%C3%B3n&Jurisdiccion=001%20--%20Guanajuato&Municipio=0048%20--%20SILAO&Farmacia=3005%20--%20INTERCONTINENTAL%20DE%20MEDICAMENTOS,%20S.A.%20DE%20C.V.%20%22CENTRO%20DE%20DISTRIBUCION%20(CEDIS)%20GUANAJUATO%22&JurisEntrega=001%20--%20Guanajuato
                    string sUrlReporte = string.Format("{0}/frameset?__report={1}.rptdesign&IdEmpresa={2}&IdEstado={3}&IdMunicipio={4}&IdJurisdiccion={5}&IdFarmacia={6}&FechaInicial={7}&FechaFinal={8}&ClaveSSA={9}&ClaveUnidad_Beneficiario={10}&Unidad_Beneficiario={11}&IdJurisdiccionEntrega={12}&Titulo={13}&Jurisdiccion={14}&Municipio={15}&Farmacia={16}&JurisEntrega={17}", DtGeneral.ServidorBI,
                        "RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos", DtGeneral.IdEmpresa, DtGeneral.IdEstado, cboLocalidad.Value.ToString(), cboJurisdiccion.Value.ToString(), cboFarmacia.Value.ToString(), dtFechaI.ToString("yyyy-MM-dd"), dtFechaF.ToString("yyyy-MM-dd"), txtSSA.Text,cboBeneficiarios.Value.ToString(), txtUniBeneficiario.Text, cboJuris.Value.ToString(), cboJuris.Text, cboJurisdiccion.Text, cboLocalidad.Text, cboFarmacia.Text, cboJuris.Text);
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
        DtFechaInicio.Date = DateTime.Now;
        DtFechaInicio.MaxDate = DateTime.Now;
        DtFechaFin.Date = DateTime.Now;
        DtFechaFin.MaxDate = DateTime.Now;
        // -- Combos --
        Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad, "*", "*", "*");
        cboFarmacia.SelectedIndex = 0;
        // -- Parámetros
        cboJuris.SelectedIndex = 0;
        cboBeneficiarios.SelectedIndex = 0;
        txtUniBeneficiario.Text = "";
        txtSSA.Text = "";
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

    //protected void cboFarmacia_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Filtros_Combos(DtGeneral.FiltrosCombos.TipoDeUnidad)
    //}

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

        cboUnidad.DataSource = ClsConsultas.TiposDeUnidad2();
        cboUnidad.DataBind();

        cboJurisdiccion.DataSource = ClsConsultas.Jurisdicciones2(sIdTipoDeUnidad);
        cboJurisdiccion.DataBind();

        cboLocalidad.DataSource = ClsConsultas.Localidades2(sIdTipoDeUnidad, sIdJurisdiccion);
        cboLocalidad.DataBind();

        cboFarmacia.DataSource = ClsConsultas.Almacen(sIdTipoDeUnidad, sIdJurisdiccion, sIdMunicipio);
        cboFarmacia.DataBind();

        cboBeneficiarios.DataSource = ClsConsultas.Beneficiarios(sIdTipoDeUnidad, sIdJurisdiccion, sIdMunicipio);
        cboBeneficiarios.DataBind();

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
        //if (!IsPostBack)
        //{
        //    ClsConsultas Consultas = new ClsConsultas();

        //    //Benfeciarios            
        //    DataSet dtsBeneficiarios = Consultas.GetBeneficiariosAlmacen();
        //    Beneficiarios.InnerHtml = string.Format(@"<label class='m-wrap inline'>
        //                                                     <span class='add-on'>Clave Unidad:</span>
        //                                                    <select id ='cboBeneficiarios' class='m-wrap'>{0}</select>
        //                                                </label>", clsToolsHtml.OptionDropList(dtsBeneficiarios.Tables[0], "IdBeneficiario", "NombreCompleto", 1, true, "*", "Todas las unidades"));

        //    //JurisdiccionEntrega.InnerHtml = String.Format(@"<label class='m-wrap inline'>
        //    //                                                    <span class='add-on'>Juris Entrega :</span>
        //    //                                                    <select id ='cboJuris' class='m-wrap'>{0}</select>
        //    //                                                </label>", DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true));
        //}
    }
}