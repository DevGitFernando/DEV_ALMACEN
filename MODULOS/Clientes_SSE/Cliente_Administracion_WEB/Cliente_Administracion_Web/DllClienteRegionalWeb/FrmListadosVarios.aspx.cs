using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;

using CrystalDecisions.CrystalReports.Engine;

public partial class DllClienteRegionalWeb_FrmListadosVarios : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            //ExportarExcel(Request.Form["__EVENTARGUMENT"]);
            DescargarReporte(Request.Form["__EVENTARGUMENT"]);
        }
        if (!IsPostBack)
        {
            ComboListado.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Listado :</span>" +
                                    "<select id=\"cboListados\" class=\"m-wrap\">" +
                                        "<option value=\"1\" selected>Listado de productos de uso exclusivo sector salud</option>" +
                                        "<option value=\"2\">Listado de productos con registro sanitario</option>" +
                                        "<option value=\"4\">Listado de productos antibioticos</option>" +
                                        "<option value=\"5\">Listado de productos controlados</option>" +
                                        "<option value=\"6\">Listado de productos refrigerados</option>" +
                                    "</select>" +
                                "<div class=\"clear\"></div>";
        }
    }

    private void DescargarReporte(string sIdReporte){
        string sNombreReporte = string.Empty;

        ReportDocument reporte = new ReportDocument();
        clsImprimir myRpt = new clsImprimir(General.DatosConexion);

        switch (sIdReporte)
        {
            case "1":
                sNombreReporte = "CteReg_Admon_Listado_Productos_Sector_Salud";
                break;

            case "2":
                sNombreReporte = "CteReg_Admon__Productos_Registros_Sanitarios";
                break;

            case "3":
                {
                    myRpt.Add("Empresa", DtGeneral.Empresa);
                    myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
                    sNombreReporte = "Ptovta_CB_PorEstado";
                }
                break;

            case "4":
                sNombreReporte = "Central_Listado_Productos_Antibioticos";
                break;

            case "5":
                sNombreReporte = "Central_Listado_Productos_Controlados";
                break;

            case "6":
                sNombreReporte = "Central_Listado_Productos_Refrigerados";
                break;
        }

        myRpt.RutaReporte = Server.MapPath("~") + @"/Reportes/";
        myRpt.NombreReporte = sNombreReporte;
        myRpt.CargarReporte(false, false);

        reporte = myRpt.ReporteWeb;
        MemoryStream mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        Download_Reporte(mStream.ToArray(), sNombreReporte);
    }

    private void Download_Reporte(byte[] Rpt, string sNombreReporte)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreReporte = sNombreReporte.Replace("CteReg_Admon_", "");
        sNombreReporte = sNombreReporte.Replace("Central_", "");
        //sNombreDocumentoDescarga = this.Title + "_" + sNombreReporte +"_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        sNombreDocumentoDescarga = sNombreReporte + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
        sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);

        Response.BinaryWrite(Rpt);
        Response.AddHeader("Content-Length", Rpt.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;  filename=" + sNombreDocumentoDescarga + ".pdf");
        Response.Flush();
        Response.End();
    }

}