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

public partial class Pedidos_FrmRegistroPedidosEspeciales : BasePage
//public partial class Pedidos_FrmRegistroPedidosEspeciales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            DescargarReporte(Request.Form["__EVENTARGUMENT"]);
        }
        if (!IsPostBack)
        {
            dtpFechaRegistro.Value = General.FechaYMD(DateTime.Now);
            dtpFechaRegistro.Disabled = true;
            //MsjRpt.InnerHtml = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["dtsClavesSSA_Sales"], "AyudaClaves");
            MsjRpt.InnerHtml = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["dtsClavesSSA_Sales"], "AyudaFarmacias");
        }
    }

    private void DescargarReporte(string sFolio)
    {
        ReportDocument reporte = new ReportDocument();
        clsImprimir myRpt = new clsImprimir(General.DatosConexion);

        myRpt.RutaReporte = Server.MapPath("~") + @"/Reportes/";

        myRpt.NombreReporte = "PtoVta_Pedidos_CEDIS";
        myRpt.Add("IdEmpresa", DtGeneral.Empresa);
        myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
        myRpt.Add("IdFarmacia", HttpContext.Current.Session["IdSucursal"].ToString());
        myRpt.Add("Folio", General.Fg.PonCeros(sFolio, 6));
        myRpt.CargarReporte(false, false);

        reporte = myRpt.ReporteWeb;
        MemoryStream mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        Download_Reporte(mStream.ToArray());
    }

    private void Download_Reporte(byte[] Rpt)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreDocumentoDescarga = this.Title + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
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