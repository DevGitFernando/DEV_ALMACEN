using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Cliente_Regional.Model;

namespace Cliente_Regional.Code
{
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
            if (!AuthHelper.IsAuthenticated())
            {
                bool bEndResponse = true;
                if (Page.IsCallback)
                {
                    bEndResponse = false;                    
                }
                HttpContext.Current.Response.Redirect("~/Account/SignIn.aspx", bEndResponse);
            }
        }
    }
}