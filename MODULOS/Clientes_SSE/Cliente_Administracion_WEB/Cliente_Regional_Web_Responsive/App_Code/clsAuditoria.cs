using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

/// <summary>
/// Descripción breve de clsAuditoria
/// </summary>
public static class clsAuditoria
{
    #region Declaración de variables

    private static string sNameDll = "DllClienteRegionalWeb";
    private static string sIdSesion = "";
    private static string sSesion = "";

    private static clsLeer leerWeb;
    #endregion

    static clsAuditoria()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        clsConexionSQL cnn = DtGeneral.DatosConexion;
        leerWeb = new clsLeer(ref cnn);
	}

    #region Funciones Publicas
    public static bool GuardarAud_LoginReg()
    {
        bool bRegresa = false;
        //sSesion = HttpContext.Current.Request.Cookies["cteRegional"]["SesionActual"].ToString() == "" ? "*" : HttpContext.Current.Request.Cookies["cteRegional"]["SesionActual"].ToString();
        sSesion = "*";

        string sSql = string.Format(" Exec spp_Mtto_CteReg_Web_Auditoria_Login '{0}', '{1}', '{2}', '{3}', '{4}' ",
                                    DtGeneral.IdEstado, DtGeneral.IdSucursal, HttpContext.Current.Request.Cookies["cteRegional"]["IdPersonal"].ToString(), sSesion, HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);

        if (leerWeb.Exec(sSql))
        {
            if (leerWeb.Leer())
            {
                bRegresa = true;
                sIdSesion = leerWeb.Campo("Clave");
                HttpContext.Current.Request.Cookies["cteRegional"]["SesionActual"] = sIdSesion;
            }
        }

        return bRegresa;
    }

    public static bool GuardarAud_MovtosReg(string Instruccion, string sPantalla)
    {
        bool bRegresa = false;
        sSesion = HttpContext.Current.Request.Cookies["cteRegional"]["SesionActual"].ToString();
        string Url_Farmacia = HttpContext.Current.Request.Url.ToString();
        
        //FileInfo myFile = new FileInfo(HttpContext.Current.Request.PhysicalPath);
        //sNameDll= myFile.Directory.Parent.ToString();

        string sSql = string.Format(" Exec spp_Mtto_CteReg_Web_Auditoria_Movimientos '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}'",
                                    DtGeneral.IdEstado, HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"].ToString(), HttpContext.Current.Request.Cookies["cteRegional"]["IdPersonal"].ToString(), sSesion, "*", HttpContext.Current.Request.ServerVariables["REMOTE_HOST"], sNameDll, sPantalla, Instruccion, Url_Farmacia);

        if (leerWeb.Exec(sSql))
        {
            bRegresa = true;
        }

        return bRegresa;
    }
    #endregion Funciones Publicas
}