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
public partial class DllClienteRegionalWeb_FrmCortesDiarios : BasePage
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
                                        //"<option value=\"*\" selected>Todas las jurisdicciones</option>" +
                                        DtGeneral.OptionDropList(DtGeneral.Jurisdicciones, "IdJurisdiccion", "NombreJurisdiccion", false) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Farmacia :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                        //"<option value=\"*\" selected>Todas las Farmacias</option>" +
                                        DtGeneral.GetInfo("Farmacia", "*", "001", "*", "*", false, false) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
        }
    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["CortesDiarios"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);
        
        string[] sCadena = sData.Split(',');
        
        int iColActiva = 0;
        string sTituloPeriodo = "";
        string sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Cortes_Diarios_Farmacias);

        DateTime dtpFechaInicial = DateTime.ParseExact(sCadena[1], "yyyy-MM-dd", null);

        sTituloPeriodo = General.FechaNombreDia(dtpFechaInicial) + ",  " + General.FechaDiaSemana(dtpFechaInicial) + " de " + General.FechaNombreMes(dtpFechaInicial) + " de " + dtpFechaInicial.Year.ToString();

        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        int iRow = 10;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            //xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + "PUEBLA", 3, 2);
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
            xpExcel.Agregar("UNIDAD " + sCadena[0], 4, 2);
            xpExcel.Agregar("CIERRE DEL DIA : " + sTituloPeriodo, 5, 2);
            xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToLongDateString(), 8, 2);

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
            Download_Reporte(xpExcel.ExcelGeneradoBytes);
        }
    }

    private void Download_Reporte(byte[] Excel)
    {
        string sNombreDocumentoDescarga = string.Empty;
        sNombreDocumentoDescarga = this.Title + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss");
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