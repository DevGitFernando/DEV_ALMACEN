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


public partial class DllClienteRegionalWeb_FrmProximosACaducar : BasePage
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
            ComboUJ.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Tipo de unidad :</span>" +
                                    "<select id=\"cboTipoUnidad\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Unidad", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";

            ComboLF.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Localidad :</span>" +
                                    "<select id=\"cboLocalidad\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Municipio", "", "", "", "", false, true) +
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
        DataSet dtsExcel = (DataSet)Session["DispensancionClaves"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');

        int iColActiva = 0;
        string sTituloPeriodo = "", sPlantilla = "";

        if (sCadena[2] == "1")
        {
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.EdoJuris_Dispensacion);
        }
        else
        {
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.EdoJurisUnidad_Dispensacion);
        }
               
        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        //DateTime dtpFechaInicial = DateTime.ParseExact(sCadena[0] + "-01", "yyyy-MM-dd", null);
        DateTime dtpFechaInicial = DateTime.ParseExact(sCadena[0], "yyyy-MM-dd", null);
        //DateTime dtpFechaFinal = DateTime.ParseExact(sCadena[1] + "-01", "yyyy-MM-dd", null);
        DateTime dtpFechaFinal = DateTime.ParseExact(sCadena[1], "yyyy-MM-dd", null);

        if (DtGeneral.FechaCompleta)
        {
            sTituloPeriodo = "Del " + dtpFechaInicial.Day.ToString() + " de " + General.FechaNombreMes(dtpFechaInicial) + " de " + dtpFechaInicial.Year.ToString();
            sTituloPeriodo += " Al " + dtpFechaFinal.Day.ToString() + " de " + General.FechaNombreMes(dtpFechaFinal) + " de " + dtpFechaFinal.Year.ToString();

        }
        else
        {
            sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial) + " " + dtpFechaInicial.Year.ToString();
            sTituloPeriodo += " A " + General.FechaNombreMes(dtpFechaFinal) + " " + dtpFechaFinal.Year.ToString();
        }

        int iRow = 9;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            //xpExcel.Agregar("INTERCONTINENTAL DE MEDICAMENTOS", 2, 2);
            xpExcel.Agregar(DtGeneral.NombreEmpresa, 2, 2);
            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + "PUEBLA", 3, 2);
            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 3, 2);
            if (DtGeneral.EncabezadoPersonalizado)
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DE " + DtGeneral.EncabezadoReporteExcel, 3, 2);
            }
            else
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 3, 2);
            }
            xpExcel.Agregar("Dispensación de Claves " + sTituloPeriodo, 4, 2);

            //xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 7, 2);
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