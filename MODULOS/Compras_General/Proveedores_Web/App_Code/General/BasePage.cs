using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BasePage : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        if ((Session["Autenticado"] == null) || (!(bool)Session["Autenticado"]))
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}