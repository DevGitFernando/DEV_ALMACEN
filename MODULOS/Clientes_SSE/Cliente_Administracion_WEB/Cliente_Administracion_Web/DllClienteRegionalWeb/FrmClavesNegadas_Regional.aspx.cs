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

public partial class DllClienteRegionalWeb_FrmClavesNegadas_Regional : BasePage
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
            ComboEJ.InnerHtml = "<label class=\"m-wrap\">" +
                                        "<span class=\"add-on\">Estado :</span>" +
                                        "<select id=\"cboEstados\" class=\"m-wrap\" disabled>" +
                                            DtGeneral.OptionDropList(DtGeneral.Estados, "IdEstado", "Estado", false, DtGeneral.EstadoConectado) +
                                        "</select>" +
                                    "</label>" +
                                    "<div class=\"clear\"></div>" +
                                    "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                        DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";

        }
    }

    private DtGeneral.ListaPlantillas Plantilla_A_Generar(DateTime dtpFechaInicial)
    {
        int iDiasPeriodo = General.FechaDiasMes(dtpFechaInicial);
        DtGeneral.ListaPlantillas myPlantilla = DtGeneral.ListaPlantillas.SurmientoClavesDispensada;
        switch (iDiasPeriodo)
        {
            case 28:
                myPlantilla = DtGeneral.ListaPlantillas.EdoJuris_ClavesNegadas_28;
                break;

            case 29:
                myPlantilla = DtGeneral.ListaPlantillas.EdoJuris_ClavesNegadas_29;
                break;

            case 30:
                myPlantilla = DtGeneral.ListaPlantillas.EdoJuris_ClavesNegadas_30;
                break;

            case 31:
                myPlantilla = DtGeneral.ListaPlantillas.EdoJuris_ClavesNegadas_31;
                break;
        }

        return myPlantilla;
    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["ClavesNegadas"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string sTituloPeriodo = "";

        DateTime dtpFechaInicial = Convert.ToDateTime(sData);
        int iColActiva = 0;
        string sPlantilla = DtGeneral.GetNombrePlantilla(Plantilla_A_Generar(dtpFechaInicial));

        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));
        
        sTituloPeriodo = General.FechaNombreMes(dtpFechaInicial) + ' ' + dtpFechaInicial.Year.ToString();

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
            xpExcel.Agregar("Analísis de claves negadas de " + sTituloPeriodo, 4, 2);

            //xpExcel.Agregar("Fecha de reporte : " + General.FechaSistema.ToLongDateString(), 6, 2);
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