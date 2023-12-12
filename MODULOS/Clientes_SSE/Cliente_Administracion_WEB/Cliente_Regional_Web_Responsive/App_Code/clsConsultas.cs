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

        string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, DtGeneral.Arbol, HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"]);
        //string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, DtGeneral.IdSucursal, DtGeneral.Arbol, "INTERMED");

        dtsPermisos = DtGeneral.ExecQuery(sSql, "Permisos");

        return dtsPermisos;
    }

    public DataSet GetArbolNavegacion(string sArbol)
    {
        DataSet dtsReturn = new DataSet();
        string sSql = string.Format(" Exec sp_CTE_Permisos '{0}', '{1}', '{2}', '{3}' ", DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), sArbol, HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"]);

        dtsReturn = DtGeneral.ExecQuery(sSql, "Arbol");
        return dtsReturn;
    }

    #endregion Funciones publicas
}