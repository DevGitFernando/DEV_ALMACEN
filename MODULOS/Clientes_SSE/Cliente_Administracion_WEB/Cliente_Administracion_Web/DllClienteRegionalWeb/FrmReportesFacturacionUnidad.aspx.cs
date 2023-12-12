using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using SC_SolutionsSystem.OfficeExcel;
using SC_SolutionsSystem.OfficeExcel.Style;
using System.Drawing;
using System.Reflection;


public partial class DllClienteRegionalWeb_FrmReportesFacturacionUnidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "DownloadExcel" || Request.Form["__EVENTTARGET"] == ",DownloadExcel")
        {
            //Metodo_Click(this, new EventArgs());
            //ExportarExcel(Request.Form["__EVENTARGUMENT"]);
            DumpExcel(Request.Form["__EVENTARGUMENT"]);
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
                                                "</label>" +
                                                "<label class=\"m-wrap inline\">" +
                                                        "<span class=\"add-on\">Programa :</span>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"txtPrograma\" value=\"\" placeholder=\"\" maxlength=\"4\"/ disabled/>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"lblPrograma\" value=\"\" placeholder=\"\" disabled>" +
                                                "</label>" +
                                                "<div class=\"clear\"></div>" +
                                                "<label class=\"m-wrap inline\">" +
                                                        "<span class=\"add-on\">Sub-Programa :</span>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"txtSubPrograma\" value=\"\" placeholder=\"\" maxlength=\"4\" disabled/>" +
                                                        "<input type=\"text\" class=\"m-wrap\" id=\"lblSubPrograma\" value=\"\" placeholder=\"\" disabled>" +
                                                "</label>", DtGeneral.IdCliente, DtGeneral.Cliente, DtGeneral.IdSubCliente, DtGeneral.SubCliente);
            if (DtGeneral.Empresa == "001")
            {
                btnAdd.Attributes.Add("class", "m-btn green");
            }
            else
            {
                btnAdd.Attributes.Add("class", "m-btn blue");
            }
            if (!DtGeneral.CteSeguroPopular)
            {
                rdoInsumoMedicamentoNOSP.Disabled = true;
                rdoInsumoMedicamentoSP.Disabled = true;
            }
        }
    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["ReportesDispensacion"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');
        string sTituloPeriodo = "";
        string sPlantilla = "";
        string sNombreReporte = "";
        string sIdFarmacia = sCadena[4];
        string sFarmacia = sCadena[5];

        DateTime dtpFechaInicial = Convert.ToDateTime(sCadena[2]);
        DateTime dtpFechaFinal = Convert.ToDateTime(sCadena[3]);

        int iColActiva = 0;

        sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Reportes_Administrativos_Dispensacion);
        sTituloPeriodo = string.Format("{0} del {1} al {2} ", sCadena[1], General.FechaYMD(dtpFechaInicial), General.FechaYMD(dtpFechaFinal));
        sNombreReporte = sCadena[1];

        switch (sCadena[0])
        {
            case "CteUni_Admon_Validacion":
                leerToExcel.DataTableClase = dtsExcel.Tables[0];
                break;
            case "CteUni_Admon_DiagnosticosDetallado":
                leerToExcel.DataTableClase = dtsExcel.Tables[1];
                break;
            case "CteUni_Admon_MedicosDetallado":
                leerToExcel.DataTableClase = dtsExcel.Tables[2];
                break;
            case "CteUni_Admon_CostoPorMedico":
                leerToExcel.DataTableClase = dtsExcel.Tables[3];
                break;
            case "CteUni_Admon_PacientesDet":
                leerToExcel.DataTableClase = dtsExcel.Tables[4];
                break;
            case "CteUni_Admon_CostoPorPaciente":
                leerToExcel.DataTableClase = dtsExcel.Tables[5];
                break;
            default:
                break;
        }

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        
        int iRow = 9;
        int iCol = 2;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();
            
            xpExcel.Agregar(DtGeneral.NombreEmpresa, 2, 2);
            if (DtGeneral.EncabezadoPersonalizado)
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DE " + DtGeneral.EncabezadoReporteExcel, 3, 2);
            }
            else
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 3, 2);
            }
            
            xpExcel.Agregar(sIdFarmacia + " - " + sFarmacia, 4, 2);
            xpExcel.Agregar(sTituloPeriodo, 5, 2);

            xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToLongDateString(), 7, 2);

            string[] sColumnas = leerToExcel.ColumnasNombre;
            
            foreach (var item in sColumnas)
            {
                xpExcel.Agregar(item, 9, iCol);
                iCol++;
            }

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
            Download_Reporte(xpExcel.ExcelGeneradoBytes, sNombreReporte);
        }
    }

    private void Download_Reporte(byte[] Excel, string sNombreReporte)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreDocumentoDescarga = this.Title + "_" + sNombreReporte + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
        sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);

        Response.BinaryWrite(Excel);
        Response.AddHeader("Content-Length", Excel.Length.ToString());
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;  filename=" + sNombreDocumentoDescarga + ".xlsx");
        Response.Flush();
        Response.End();
    }

    private void DumpExcel(string sData)
    {
        string sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Reportes_InmovilizarColumna_10);
        FileInfo fi1 = new FileInfo(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));
        Color cBgEmpresa = DtGeneral.ColorBgReportes;
        Color cTxtEmpresa = DtGeneral.ColorDeTextoHexadecimal;
        //using (ExcelPackage pck = new ExcelPackage())
        using (ExcelPackage pck = new ExcelPackage(fi1))
        {
            DataSet dtsExcel = (DataSet)Session["ReportesDispensacion"];
            clsLeer leerToExcel = new clsLeer();
            
            leerToExcel.DataSetClase = dtsExcel;

            string[] sCadena = sData.Split(',');
            string sTituloPeriodo = "";
            string sNombreReporte = "";
            string sIdFarmacia = sCadena[4];
            string sFarmacia = sCadena[5];

            DateTime dtpFechaInicial = Convert.ToDateTime(sCadena[2]);
            DateTime dtpFechaFinal = Convert.ToDateTime(sCadena[3]);

            sTituloPeriodo = string.Format("{0} del {1} al {2} ", sCadena[1], General.FechaYMD(dtpFechaInicial), General.FechaYMD(dtpFechaFinal));
            sNombreReporte = sCadena[1];

            switch (sCadena[0])
            {
                case "CteUni_Admon_Validacion":
                    leerToExcel.DataTableClase = dtsExcel.Tables[0];
                    break;
                case "CteUni_Admon_DiagnosticosDetallado":
                    leerToExcel.DataTableClase = dtsExcel.Tables[1];
                    break;
                case "CteUni_Admon_MedicosDetallado":
                    leerToExcel.DataTableClase = dtsExcel.Tables[2];
                    break;
                case "CteUni_Admon_CostoPorMedico":
                    leerToExcel.DataTableClase = dtsExcel.Tables[3];
                    break;
                case "CteUni_Admon_PacientesDet":
                    leerToExcel.DataTableClase = dtsExcel.Tables[4];
                    break;
                case "CteUni_Admon_CostoPorPaciente":
                    leerToExcel.DataTableClase = dtsExcel.Tables[5];
                    break;
                default:
                    break;
            }

            //Create the worksheet
            //ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sCadena[1]);
            ExcelWorksheet ws = pck.Workbook.Worksheets["Reporte"];
            //ws.Name = sCadena[1];
            
            //Table to export
            DataTable tbl = leerToExcel.DataTableClase;

            int iMerge = tbl.Columns.Count + 1;
            int rowIndex = 9;
            int colIndex = 2;
            var cell = ws.Cells;
            var fill = cell.Style.Fill;

            foreach (DataColumn dc in tbl.Columns) //Creating Headings
            {
                cell = ws.Cells[rowIndex, colIndex];
                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(cTxtEmpresa);
                cell.Style.Font.Size = 12;
                cell.AutoFilter = true;
                cell.AutoFitColumns(20);
                
                //Setting the background color of header cells to Gray
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(cBgEmpresa);
                
                //Setting Top/left,right/bottom borders.
                var border = cell.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = 
                    border.Bottom.Style =ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value = dc.ColumnName;
                colIndex++;
            }
            
            ws.Cells[9, 2, 9, iMerge].AutoFilter = true;
            
            foreach (DataRow dr in tbl.Rows) // Adding Data into rows
            {
                colIndex = 2;
                rowIndex++;
                foreach (DataColumn dc in tbl.Columns)
                {
                    cell = ws.Cells[rowIndex, colIndex];
                    //Setting Value in cell
                    cell.Value = dr[dc.ColumnName];
                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;
                    colIndex++;
                }
            }

            cell = ws.Cells[9 + tbl.Rows.Count, 2, 9 + tbl.Rows.Count, iMerge];
            var borderBottom = cell.Style.Border;
            borderBottom.Bottom.Style = ExcelBorderStyle.Thin;

            //Empresa
            ws.Cells[2, 2].Value = DtGeneral.NombreEmpresa;
            ws.Cells[2, 2, 2, iMerge].Merge = true;
            ws.Cells[2, 2, 2, iMerge].Style.Font.Bold = true;
            ws.Cells[2, 2, 2, iMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            
            cell = ws.Cells[2, 2];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(cBgEmpresa);
            ws.Cells[2, 2].Style.Font.Color.SetColor(cTxtEmpresa);
            ws.Cells[2, 2].Style.Font.Size = 20;


            // Encabezado
            if (DtGeneral.EncabezadoPersonalizado)
            {
                ws.Cells[3, 2].Value = "SERVICIOS DE SALUD DE " + DtGeneral.EncabezadoReporteExcel;
            }
            else
            {
                ws.Cells[3, 2].Value = "SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado;
            }

            ws.Cells[3, 2, 3, iMerge].Merge = true;
            ws.Cells[3, 2, 3, iMerge].Style.Font.Bold = true;
            ws.Cells[3, 2, 3, iMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            cell = ws.Cells[3, 2];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(cBgEmpresa);
            ws.Cells[3, 2].Style.Font.Color.SetColor(cTxtEmpresa);
            ws.Cells[3, 2].Style.Font.Size = 14;

            //Farmacia
            ws.Cells[4, 2].Value = sIdFarmacia + " - " + sFarmacia;
            ws.Cells[4, 2, 4, iMerge].Merge = true;
            ws.Cells[4, 2, 4, iMerge].Style.Font.Bold = true;
            ws.Cells[4, 2, 4, iMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            cell = ws.Cells[4, 2];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(cBgEmpresa);
            ws.Cells[4, 2].Style.Font.Color.SetColor(cTxtEmpresa);
            ws.Cells[4, 2].Style.Font.Size = 14;

            // Periodo
            ws.Cells[5, 2].Value = sTituloPeriodo;
            ws.Cells[5, 2, 5, iMerge].Merge = true;
            ws.Cells[5, 2, 5, iMerge].Style.Font.Bold = true;
            ws.Cells[5, 2, 5, iMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            cell = ws.Cells[5, 2];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(cBgEmpresa);
            ws.Cells[5, 2].Style.Font.Color.SetColor(cTxtEmpresa);
            ws.Cells[5, 2].Style.Font.Size = 12;

            //Fecha reporte
            ws.Cells[7, 2].Value = "Fecha de reporte : " + DateTime.Now.ToLongDateString();
            ws.Cells[7, 2, 7, iMerge].Merge = true;
            ws.Cells[7, 2, 7, iMerge].Style.Font.Bold = true;
            ws.Cells[7, 2, 7, iMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            cell = ws.Cells[7, 2];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(cBgEmpresa);
            ws.Cells[7, 2].Style.Font.Color.SetColor(cTxtEmpresa);
            ws.Cells[7, 2].Style.Font.Size = 12;

            ////Autofit columns for all cells            
            //ws.Cells.AutoFitColumns(0);


            Download_Reporte(pck.GetAsByteArray(), sNombreReporte);

        }
    }
}