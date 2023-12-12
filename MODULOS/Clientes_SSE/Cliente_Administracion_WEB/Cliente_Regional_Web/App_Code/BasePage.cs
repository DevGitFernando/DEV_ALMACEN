using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
	public BasePage()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    protected override void OnInit(EventArgs e)
    {
        //if (HttpContext.Current.Request.Cookies["cteRegional"] != null)
        //{
        //    if (!Convert.ToBoolean(HttpContext.Current.Request.Cookies["cteRegional"]["Autenticado"]) || Convert.ToDateTime(HttpContext.Current.Request.Cookies["cteRegional"]["Session"]) < DateTime.Now)
        //    {
        //        HttpContext.Current.Request.Cookies["cteRegional"]["Autenticado"] = false.ToString();
        //        HttpContext.Current.Response.Cookies["cteRegional"].Expires = DateTime.Now.AddDays(-1);
        //        HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
        //    }
        //}
        //else
        //{
        //    HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
        //}

        if (HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie] != null)
        {
            ClsDictionary myDictionary = new ClsDictionary();
            myDictionary.Descifrar(HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie]["Value"].ToString());

            if (!Convert.ToBoolean(myDictionary.Search("Autenticado")) || Convert.ToDateTime(myDictionary.Search("Session")) < DateTime.Now)
            {
                HttpContext.Current.Response.Cookies[DtGeneral.NombreCookie].Expires = DateTime.Now.AddDays(-1);

                HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
            }
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
        }

    }
}