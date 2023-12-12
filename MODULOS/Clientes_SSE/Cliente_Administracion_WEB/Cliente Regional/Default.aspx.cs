using System;
using System.IO;
using Cliente_Regional.Model;
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        //TextContent.InnerHtml = File.ReadAllText(Server.MapPath(@"~/App_Data/Overview.html"));

        //TableOfContentsTreeView.DataBind();
        //TableOfContentsTreeView.ExpandAll();
        
        //Si no tiene iniciada sesión lo redireccionamoas al login

        if (!AuthHelper.IsAuthenticated())
        {
            Response.Redirect("~/Account/SignIn.aspx");
        }
    }
    }