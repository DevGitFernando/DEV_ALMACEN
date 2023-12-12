using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;

//using System.Web.Script.Services;
using System.Web.Services;


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
        if (HttpContext.Current.Request.Cookies["Recetario"] != null)
        {
            if (HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] == null || !Convert.ToBoolean(HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"]))
            {
                HttpContext.Current.Request.Cookies["Recetario"]["Autenticado"] = null;
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
    }
}