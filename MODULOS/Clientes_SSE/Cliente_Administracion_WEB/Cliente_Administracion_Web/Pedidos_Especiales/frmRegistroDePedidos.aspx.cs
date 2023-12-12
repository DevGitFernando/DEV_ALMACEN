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

public partial class Pedidos_Especiales_frmRegistroDePedidos : BasePage
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
            //MsjRpt.InnerHtml = DtGeneral.DtsToTableHtml((DataSet)HttpContext.Current.Session["dtsClavesSSA_Sales"], "AyudaFarmacias");
            string sSql = string.Format("Select * " +
                                        "From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales " +
                                        "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And Status = 'A' ", DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen);

            /*wsCnnCliente cnnCte = new wsCnnCliente();
            //cnnCte.Url = "http://int-gto-0011.dyndns-ip.com/wsInt-PuntoDeVenta/wsFarmacia.asmx";
            string sFiltro = string.Format("[IdFarmacia] = '{0}'", DtGeneral.IdAlmacen);
            cnnCte.Url = DtGeneral.dtsFiltro(DtGeneral.urlFarmacias, "urlFarmacias", sFiltro, "UrlFarmacia");
            HttpContext.Current.Session["Perfil"] = cnnCte.ExecuteExt(null, "AlamcenPtoVta.ini", sSql);*/

            ComboPA.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Programa :</span>" +
                                    "<select id=\"cboPerfil\" class=\"m-wrap\">" +
                                        DtGeneral.OptionDropList(DtGeneral.ExecQuery(sSql), "IdPerfilAtencion", "PerfilDeAtencion", true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
            
            sSql = string.Format("Select * " +
                                    "From vw_CFGC_ALMN_CB_NivelesAtencion_ProgramasEspeciales_Beneficiarios " +
                                    "Where IdEmpresa = '{0}' And IdEstado = '{1}' And IdFarmacia = '{2}' And IdPerfilAtencion = {3}", DtGeneral.Empresa, DtGeneral.EstadoConectado, DtGeneral.IdAlmacen, 1); 

            ComboB.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Beneficiario :</span>" +
                                    "<select id=\"cboBeneficiario\" class=\"m-wrap\">" +
                                        //DtGeneral.OptionDropList(DtGeneral.ExecQuery(sSql), "IdBeneficiario", "NombreCompleto", true) +
                                        "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>" +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
        }
    }

    private void DescargarReporte(string sFolio)
    {
        ReportDocument reporte = new ReportDocument();
        
        clsImprimir myRpt = new clsImprimir(DtGeneral.clsDatosConexionCedis);

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