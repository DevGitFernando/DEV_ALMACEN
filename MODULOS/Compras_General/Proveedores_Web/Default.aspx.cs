using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;                                  
using SC_SolutionsSystem.Reportes;

using Proveedores_Web;

public partial class Default: System.Web.UI.Page
{
    bool bContinuar = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__IdEmpresa"] != null)
        {
            if (Request.Form["__IdEmpresa"] != "")
            {
                DtGeneral.Empresa = Request.Form["__IdEmpresa"];
                bContinuar = true;
            }
        }
        //if (Request.QueryString["IdEmpresa"] != null)
        //{
        //    if (Request.QueryString["IdEmpresa"] != "")
        //    {
        //        DtGeneral.Empresa = Request.QueryString["IdEmpresa"];
        //        bContinuar = true;
        //    }
        //}
        if (bContinuar)
        {
            if (!IsPostBack)
            {
                DtGeneral.ObtenerConfiguracion();
                string sImagenFondo = string.Empty;

                if (DtGeneral.Empresa == "001")
                {
                    sImagenFondo = DtGeneral.ImagenFondoEmpresa001;
                }
                else if (DtGeneral.Empresa == "002")
                {
                    sImagenFondo = DtGeneral.ImagenFondoEmpresa002;
                }

                this.Header.InnerHtml = "<link rel=\"shortcut icon\" href=\"images/favicon.ico\" />" +
                                        "<style type=\"text/css\">" +
                                            "* " +
                                            "{ " +
                                                "margin: 0px;" +
                                                "padding: 0px;" +
                                                "overflow: hidden;" +
                                            "}" +
                                            "img {" +
                                                "border: 0px;" +
                                            "}" +
                                            "body {" +
                                                "margin: 0px;" +
                                                "padding: 0px;" +
                                                "overflow: hidden;" +
                                                "background-image:url(" + sImagenFondo + ");" +
                                                "background-attachment:fixed;" +
                                                "background-repeat:no-repeat;" +
                                                "background-position: top right;" +
                                                "background-size: 75%;" +
                                                "height: 100%;" +
                                                "width: 100%;" +
                                            "}" +
                                            "#ifrmContenedor, #ifrmLogin" +
                                            "{" +
                                                "position: fixed;" +
                                                "height: 100%;" +
                                                "width: 100%;" +
                                                "overflow: hidden;" +
                                                "border: 0px;" +
                                            "}" +
                                        "</style>" +
                                        "<script type=\"text/javascript\" language=\"javascript\">" +
                                            "if(top.location != this.location) {" +
                                                "top.location=this.location;}" +
                                        "</script>";
                this.Title = DtGeneral.TituloNavegador;
            }
        }
        else
        {
            Response.Redirect("~/errores/Denegado.aspx");
        }
    }
}
