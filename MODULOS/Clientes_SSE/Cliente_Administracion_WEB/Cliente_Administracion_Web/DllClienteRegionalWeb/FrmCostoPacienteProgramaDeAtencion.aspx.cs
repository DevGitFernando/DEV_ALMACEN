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

public partial class DllClienteRegionalWeb_FrmCostoPacienteProgramaDeAtencion : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Download" || Request.Form["__EVENTTARGET"] == ",Download")
        {
            //Metodo_Click(this, new EventArgs());
            //Download(Request.Form["__EVENTARGUMENT"]);
            ExportarExcel(Request.Form["__EVENTARGUMENT"]);
        }
        if (!IsPostBack)
        {
            Combos.InnerHtml = "<label class=\"m-wrap\">" +
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
                                "<label class=\"m-wrap\">" +
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
        DataSet dtsExcel = (DataSet)Session["CostoPacienteProgramaDeAtencion"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');

        int iColActiva = 0;
        string sTituloPeriodo = "", sTituloReporte = "", sMunicipio = "", sTipoUnidad = "", sPlantilla = "";

        if (sCadena[4] == "2")
        {
            sTituloReporte = " COSTOS DE BENEFICIARIOS POR PROGRAMA DE ATENCION  ";
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Salidas_Costos_Programas_Atencion);
        }
        else
        {
            sTituloReporte = " COSTOS DE BENEFICIARIOS CONCENTRADO POR FARMACIA  ";
            sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Salidas_Costos_Programas_Atencion_Concentrado_Farmacia);
        }

        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        sMunicipio = " MUNICIPIO :  " + sCadena[1];
        sTipoUnidad = " TIPO UNIDAD :  " + sCadena[0];

        DateTime dtpFechaInicial = DateTime.ParseExact(sCadena[2], "yyyy-MM-dd", null);
        DateTime dtpFechaFinal = DateTime.ParseExact(sCadena[3], "yyyy-MM-dd", null);

        sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial) + " " + dtpFechaInicial.Year.ToString();
        sTituloPeriodo += " A " + General.FechaNombreMes(dtpFechaFinal) + " " + dtpFechaFinal.Year.ToString();

        int iRow = 9;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + "PUEBLA", 2, 2);
            //xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 2, 2);
            if (DtGeneral.EncabezadoPersonalizado)
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DE " + DtGeneral.EncabezadoReporteExcel, 2, 2);
            }
            else
            {
                xpExcel.Agregar("SERVICIOS DE SALUD DEL ESTADO DE " + DtGeneral.NombreEstado, 2, 2);
            }
            xpExcel.Agregar(sTituloReporte + sTituloPeriodo, 3, 2);
            xpExcel.Agregar(sMunicipio, 4, 2);
            xpExcel.Agregar(sTipoUnidad, 5, 2);

            xpExcel.Agregar("Fecha de reporte : " + DateTime.Now.ToLongDateString(), 7, 2);

            leerToExcel.RegistroActual = 1;
            iRow++;
            while (leerToExcel.Leer())
            {
                iColActiva = 2;
                //foreach (string sCol in leer.ColumnasNombre)
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