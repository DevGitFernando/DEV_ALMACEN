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

public partial class DllClienteRegionalWeb_FrmTBC_Surtimiento_Regional : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            ExportarExcel(Request.Form["__EVENTARGUMENT"]);
        }
        if (!IsPostBack)
        {
            ComboJF.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                       DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Farmacia :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Farmacia", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
        }

    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["SurtimientoInsumos"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');
        string sTituloPeriodo = "";
        string sPlantilla = "";
        string sNombreReporte = "";

        DateTime dtpFechaInicial = Convert.ToDateTime(sCadena[0]);
        DateTime dtpFechaFinal = Convert.ToDateTime(sCadena[1]);

        int iColActiva = 0;

        if (sCadena[2] == "1")
        {
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.EdoJuris_TBC_Surtimiento);
            sTituloPeriodo = "Surtimiento de insumos del " + General.FechaYMD(dtpFechaInicial) + " al " + General.FechaYMD(dtpFechaFinal);
            sNombreReporte = "Causes";
        }
        else {
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.EdoJuris_TBC_Surtimiento_NoCauses);
            sTituloPeriodo = "Surtimiento de insumos NO CAUSES del " + General.FechaYMD(dtpFechaInicial) + " al " + General.FechaYMD(dtpFechaFinal);
            sNombreReporte = "No Causes";
        }
        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        int iRow = 8;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            //xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
            xpExcel.Agregar(DtGeneral.NombreEmpresa, 2, 2);
            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 3, 2);
            if (DtGeneral.EncabezadoPersonalizado)
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DE " + DtGeneral.EncabezadoReporteExcel, 3, 2);
            }
            else
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 3, 2);
            }
            xpExcel.Agregar(sTituloPeriodo, 4, 2);

            xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToLongDateString(), 6, 2);

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
}