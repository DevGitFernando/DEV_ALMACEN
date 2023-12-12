﻿using System;
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

public partial class UsuariosyPermisos_frmAuditor_Movimientos : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            DumpExcel(Request.Form["__EVENTARGUMENT"]);
        }

    }

    private void DumpExcel(string sData)
    {
        string sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Reportes_InmovilizarColumna_10);
        FileInfo fi1 = new FileInfo(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));
        Color cBgEmpresa = DtGeneral.ColorBgReportes;
        Color cTxtEmpresa = DtGeneral.ColorTexto;
        using (ExcelPackage pck = new ExcelPackage(fi1))
        {
            DataSet dtsExcel = (DataSet)Session["AuditorMovimientos"];
            clsLeer leerToExcel = new clsLeer();

            leerToExcel.DataSetClase = dtsExcel;

            string[] sCadena = sData.Split(',');
            string sTituloPeriodo = "";
            string sNombreReporte = "";
            string sFarmacia = sCadena[0];

            sTituloPeriodo = string.Format("Movimientos realizados del {0} al {1}", sCadena[0], sCadena[1]);
            sNombreReporte = "Auditor De Movimientos";

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
                    border.Bottom.Style = ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value = dc.ColumnName;
                colIndex++;
            }

            ws.Cells[9, 2, 9, iMerge].AutoFilter = true;
            string[] sFormatColums = new string[iMerge + 1];
            foreach (DataRow dr in tbl.Rows) // Adding Data into rows
            {
                colIndex = 2;
                rowIndex++;
                foreach (DataColumn dc in tbl.Columns)
                {
                    cell = ws.Cells[rowIndex, colIndex];
                    //Setting Value in cell
                    cell.Value = dr[dc.ColumnName];
                    if (sFormatColums[colIndex] == null)
                    {
                        sFormatColums[colIndex] = clsToolsHtml.GetAutoFormat(dc.DataType);
                    }

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

            ///Encabezado del Excel
            //Empresa
            iMerge = 9;
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
                ws.Cells[3, 2].Value = DtGeneral.Encabezado;
            }
            else
            {
                ws.Cells[3, 2].Value = "SERVICIOS DE SALUD DEL ESTADO";
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

            //Titulo
            ws.Cells[4, 2].Value = "Auditor de movimientos";
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

            ///Autofit columns for all cells            
            ws.Cells.AutoFitColumns(0);

            for (int i = 2; i < sFormatColums.Length; i++)
            {

                using (ExcelRange r = ws.Cells[iMerge, i, iMerge + tbl.Rows.Count, i])
                {
                    r.Style.Numberformat.Format = sFormatColums[i];
                }
            }
            
            Download_Reporte(pck.GetAsByteArray(), sNombreReporte);
        }
    }

    private void Download_Reporte(byte[] Excel, string sNombreReporte)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreDocumentoDescarga = sNombreReporte + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
        sNombreDocumentoDescarga = sNombreDocumentoDescarga.Replace(" ", "_");
        sNombreDocumentoDescarga = HttpUtility.UrlEncode(sNombreDocumentoDescarga, System.Text.Encoding.UTF8);

        Response.BinaryWrite(Excel);
        Response.AddHeader("Content-Length", Excel.Length.ToString());
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;  filename=" + sNombreDocumentoDescarga + ".xlsx");
        Response.Flush();
        Response.End();
    }
}