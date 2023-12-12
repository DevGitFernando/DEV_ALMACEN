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

public partial class DllClienteRegionalWeb_FrmSurtimientoRecetas : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            DescargarReporte();
        }
        if (Request.Form["__EVENTTARGET"] == "DownloadExcel" || Request.Form["__EVENTTARGET"] == ",DownloadExcel")
        {
            //Metodo_Click(this, new EventArgs());
            ExportarExcel(Request.Form["__EVENTARGUMENT"]);
        }
        if (!IsPostBack)
        {
            string sFiltro = string.Empty;
            string sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005', '006') ";

            if (HttpContext.Current.Session["IdSucursal"].ToString() != "0001")
            {
                sFiltro = " And IdFarmacia = '" + HttpContext.Current.Session["IdSucursal"] + "'";


                if (DtGeneral.MostraCedis)
                {
                    sFiltroTipoUnidad = " And IdTipoUnidad Not In ('000', '005') ";
                }
            }

            ComboEstados.InnerHtml = "<label class=\"m-wrap\">" +
                                        "<span class=\"add-on\">Estado :</span>" +
                                        "<select id=\"cboEstados\" class=\"m-wrap\" disabled>" +
                                            DtGeneral.OptionDropList(DtGeneral.Estados, "IdEstado", "Estado", false, DtGeneral.EstadoConectado) +
                                        "</select>" +
                                    "</label>" +
                                    "<div class=\"clear\"></div>" +
                                    "<label class=\"m-wrap inline\">" +
                                        "<span class=\"add-on\">Farmacia :</span>" +
                                        "<input type=\"text\" class=\"m-wrap\" id=\"txtFarmacia\" value=\"\" placeholder=\"Clave\" maxlength=\"4\" />" +
                                        "<input type=\"text\" class=\"m-wrap\" id=\"lblFarmacia\" value=\"\" placeholder=\"FARMACIA\" disabled>" +
                                        "<span id=\"msjFarmacia\" class=\"MsjBottom hide\">Presione Enter para obtener la lista de farmacias.</span>" +
                                    "</label>";

            //MsjRpt.InnerHtml = DtGeneral.DtsToTableHtml(DtGeneral.ExecQuery(string.Format(" Select 'Clave de Farmacia' = F.IdFarmacia, F.Farmacia, " +
            //                    " F.Jurisdiccion " +
            //                    " From vw_Farmacias F (NoLock) " +
            //                    " Inner Join vw_Farmacias_Urls U (NoLock) On ( F.IdEstado = U.IdEstado and F.IdFarmacia = U.IdFarmacia and U.StatusUrl = 'A' ) " +
            //                    " Where F.IdEstado = '{0}' {1} And F.IdFarmacia > '{2}'" +
            //                    " Order By F.IdJurisdiccion, F.IdFarmacia ", DtGeneral.EstadoConectado, sFiltro, DtGeneral.FiltroUnidad)), "AyudaFarmacias");

            MsjRpt.InnerHtml = DtGeneral.DtsToTableHtml(DtGeneral.ExecQuery(string.Format(" Select IdFarmacia, Farmacia, Jurisdiccion From vw_Farmacias (NoLock) " +
                                " Where IdEstado = '{0}' And Status = 'A' {1} {2}" +
                                " Order By IdJurisdiccion, IdFarmacia ", DtGeneral.EstadoConectado, sFiltroTipoUnidad, sFiltro)), "AyudaFarmacias");

            Cliente.InnerHtml = string.Format("<label class=\"m-wrap inline\">" +
                                                        "<span class=\"add-on\">Cliente :</span>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"txtCte\" value=\"{0}\" placeholder=\"Cliente\" maxlength=\"4\" disabled/>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"lblCte\" value=\"{1}\" placeholder=\"Cliente\" disabled>" +
                                                "</label>" +
                                                "<div class=\"clear\"></div>" +
                                                "<label class=\"m-wrap inline\">" +
                                                        "<span class=\"add-on\">Sub-Cliente :</span>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"txtSubCte\" value=\"{2}\" placeholder=\"SubCliente\" maxlength=\"4\" disabled/>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"lblSubCte\" value=\"{3}\" placeholder=\"SubCliente\" disabled>" +
                                                "</label>", DtGeneral.IdCliente, DtGeneral.Cliente, DtGeneral.IdSubCliente, DtGeneral.SubCliente);
            if (DtGeneral.Empresa == "001")
            {
                btnAdd.Attributes.Add("class", "m-btn green");
            }
            else
            {
                btnAdd.Attributes.Add("class", "m-btn blue");
            }
        }
    }

    private void DescargarReporte()
    {
        string sNombreReporte = string.Empty;

        ReportDocument reporte = new ReportDocument();
        clsImprimir myRpt = new clsImprimir(General.DatosConexion);

        myRpt.RutaReporte = Server.MapPath("~") + @"/Reportes/";
        myRpt.NombreReporte = "Cte_Admon_SurtimientoRecetas.rpt";
        myRpt.CargarReporte(false, false);

        reporte = myRpt.ReporteWeb;
        MemoryStream mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        Download_Reporte(mStream.ToArray(), "pdf");
    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["ClavesSurtimiento"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');

        string sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.SurmientoClavesDispensada);
        
        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        int iRow = 9;
        int iColActiva = 0;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            xpExcel.Agregar(sCadena[0], 2, 2);
            xpExcel.Agregar(sCadena[1], 3, 2);
            xpExcel.Agregar("REPORTE DE CLAVES DISPENSADAS DE " + sCadena[2], 4, 2);

            //xpExcel.Agregar("Fecha de reporte : " + sCadena[3], 7, 2);
            xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToLongDateString(), 7, 2);

            leerToExcel.RegistroActual = 1;
            iRow++;
            while (leerToExcel.Leer())
            {
                iColActiva = 2;
                foreach (string sCol in leerToExcel.ColumnasNombre)
                {
                    xpExcel.Agregar(leerToExcel.Campo(sCol), iRow, iColActiva);
                    iColActiva++;
                }
                iRow++;
            }

            // Finalizar el Proceso 
            xpExcel.CerrarDocumento();
            Download_Reporte(xpExcel.ExcelGeneradoBytes, "xlsx");
        }
    }

    private void Download_Reporte(byte[] Rpt, string sType)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreDocumentoDescarga = this.Title + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
        sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);

        Response.BinaryWrite(Rpt);
        Response.AddHeader("Content-Length", Rpt.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;  filename=" + sNombreDocumentoDescarga + "." + sType);
        Response.Flush();
        Response.End();
    }
}