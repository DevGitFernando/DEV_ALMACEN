using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//System Extra
using System.Data;

//Sc Solutions
using SC_SolutionsSystem.Data;

/// <summary>
/// Descripción breve de clsConsultas
/// </summary>
public class clsConsultas
{
	public clsConsultas()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }

    #region Funciones publicas
    public DataSet Permisos()
    {
        DataSet dtsPermisos = new DataSet("dtsPermisos");

        //string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, DtGeneral.Arbol, HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"]);
        string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, DtGeneral.Arbol, DtGeneral.ObtenerValorCookie("LoginUser"));
        //string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, DtGeneral.Arbol, "INTERMED");

        dtsPermisos = DtGeneral.ExecQuery(sSql, "Permisos");

        return dtsPermisos;
    }

    public DataSet GetArbolNavegacion(string sArbol)
    {
        DataSet dtsReturn = new DataSet();
        //string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), sArbol, HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"]);
        string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.ObtenerValorCookie("IdSucursal"), sArbol, DtGeneral.ObtenerValorCookie("LoginUser"));

        dtsReturn = DtGeneral.ExecQuery(sSql, "Arbol");
        return dtsReturn;
    }

    public DataSet GetBeneficiariosAlmacen() {
        string sTipoUnidadAlmacen = "006";
        string sSql = string.Format(@"Set DateFormat YMD
	                                    Select
		                                    B.IdBeneficiario, B.NombreCompleto		
	                                    From vw_Beneficiarios As B (NoLock)
	                                    Inner Join CatFarmacias As CF (NoLock) On ( B.IdEstado =  CF.IdEstado And B.IdFarmacia = CF.IdFarmacia )
	                                    Inner Join BI_RPT__CatFarmacias_A_Procesar As FP On ( B.IdEstado = FP.IdEstado  And B.IdFarmacia = FP.IdFarmacia )
	                                    Where B.IdEstado = '{0}' And B.IdCliente = '{1}' And B.IdSubCliente = '{2}' And CF.IdTipoUnidad = '{3}' And B.Status = 'A'",
                                        DtGeneral.IdEstado, DtGeneral.IdCliente, DtGeneral.IdSubCliente, sTipoUnidadAlmacen);

        DataSet dtsReturn = DtGeneral.ExecQuery(sSql, "Beneficiarios");
        return dtsReturn;
    }

    public string GetUnidadesAlmacen(string sOpcion, string sUnidad, string sJurisdiccion, string sMunicipio, string sFarmacia, bool bAddSelect, bool bUnique)
    {
        string sEstructura = string.Empty;
        string sOpcionAdministrador = string.Empty;
        string sQuery = null;
        string[] sColumnas = new string[] { };
        if (HttpContext.Current.Session["UnidadesAlmacen"] == null) {
            string sTipoUnidadAlmacen = "006";
            string sSql = string.Format(@"Set DateFormat YMD
                                        Select
                                            F.IdTipoUnidad, F.TipoDeUnidad, F.IdJurisdiccion, F.Jurisdiccion, F.IdMunicipio, F.Municipio, F.IdFarmacia, F.Farmacia
                                        From vw_Farmacias As F (Nolock)
                                        Inner Join BI_RPT__CatFarmacias_A_Procesar As FP On ( F.IdEstado = FP.IdEstado And F.IdFarmacia = FP.IdFarmacia And FP.MostrarEnListado  = '1')
                                        Where F.IdEstado = '{0}' And F.IdTipoUnidad = '{1}' And F.IdNivelDeAtencion = '3' And F.Status = 'A'",
                                        DtGeneral.IdEstado, sTipoUnidadAlmacen);
            HttpContext.Current.Session["UnidadesAlmacen"] = DtGeneral.ExecQuery(sSql, "UnidadesAlmacen");
        }
        DataTable dtData = ((DataSet)HttpContext.Current.Session["UnidadesAlmacen"]).Tables[0];
        

        switch (sOpcion)
        {
            case "Unidad":
                sColumnas = new string[] { "IdTipoUnidad", "TipoDeUnidad" };
                break;
            case "Jurisdiccion":
                sColumnas = new string[] { "IdJurisdiccion", "Jurisdiccion" };
                break;
            case "Municipio":
                sColumnas = new string[] { "IdMunicipio", "Municipio" };
                break;
            case "Farmacia":
                sColumnas = new string[] { "IdFarmacia", "Farmacia" };
                break;
            default:
                break;
        }

        dtData = dtData.Select(sQuery, sColumnas[0]).CopyToDataTable();

        sEstructura += clsToolsHtml.OptionDropList(dtData.DefaultView.ToTable(true, sColumnas), sColumnas[0], sColumnas[1], bAddSelect);

        return sEstructura;
    }

    #endregion Funciones publicas
}