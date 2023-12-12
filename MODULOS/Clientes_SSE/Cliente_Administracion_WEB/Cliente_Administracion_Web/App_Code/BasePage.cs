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
        DtGeneral.Autenticado = false;
        if ((Session["Autenticado"] == null) || (!(bool)Session["Autenticado"]))
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}