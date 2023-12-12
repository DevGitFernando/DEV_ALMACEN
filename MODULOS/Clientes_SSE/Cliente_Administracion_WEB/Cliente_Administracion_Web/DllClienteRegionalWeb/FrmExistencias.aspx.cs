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

public partial class DllClienteRegionalWeb_FrmExistencias : BasePage
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
            ComboJ.InnerHtml =  "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Jurisdicción :</span>" +
                                    "<select id=\"cboJurisdiccion\" class=\"m-wrap\">" +
                                    //(DtGeneral.Sucursal == "0001" ? "<option value=\"*\" selected>Todas las jurisdicciones</option>" : "") +
                                    //(HttpContext.Current.Session["IdSucursal"].ToString() == "0001" ? "<option value=\"*\" selected>Todas las jurisdicciones</option>" : "") +
                                        //DtGeneral.OptionDropList(DtGeneral.Jurisdicciones, "IdJurisdiccion", "NombreJurisdiccion", false) +
                                        //DtGeneral.OptionDropList((DataSet)HttpContext.Current.Session["dtsJurisdicciones"], "IdJurisdiccion", "NombreJurisdiccion", false) +
                                        DtGeneral.GetInfo("Jurisdiccion", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";

            ComboF.InnerHtml = "<label class=\"m-wrap\">" +
                                    "<span class=\"add-on\">Farmacia :</span>" +
                                    "<select id=\"cboFarmacia\" class=\"m-wrap\">" +
                                    //(DtGeneral.Sucursal == "0001" ? "<option value=\"*\" selected>Todas las Farmacias</option>" : "") +
                                    //(HttpContext.Current.Session["IdSucursal"].ToString() == "0001" ? "<option value=\"*\" selected>Todas las Farmacias</option>" : "") +
                                        DtGeneral.GetInfo("Farmacia", "", "", "", "", false, true) +
                                    "</select>" +
                                "</label>" +
                                "<div class=\"clear\"></div>";
        }
    }

    private void ExportarExcel(string sData)
    {
        DataSet dtsExcel = (DataSet)Session["Existencia"];
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsExportarExcelPlantillaWeb xpExcel;
        clsLeer leerToExcel = new clsLeer(ref cnn);
        clsLeer leer = new clsLeer(ref cnn);

        string[] sCadena = sData.Split(',');

        int iColActiva = 0;
        string sFarmacia = "Del Estado de " + DtGeneral.NombreEstado;
        string sPlantilla = DtGeneral.GetNombrePlantilla(DtGeneral.ListaPlantillas.Existencia_Por_ClaveSSA);

        if (sCadena[0] != "*")
        {
            if (sCadena[2] != "*")
            {
                sFarmacia = "De Unidad: " + sCadena[3];
            }
            else
            {
                sFarmacia = "De Jurisdicción: " + sCadena[1];
            }
        }

        if (DtGeneral.EncabezadoPersonalizado)
        { 
            sFarmacia = "de " + DtGeneral.EncabezadoReporteExcel;
        }

        leerToExcel.DataSetClase = dtsExcel;

        xpExcel = new clsExportarExcelPlantillaWeb(Path.Combine(DtGeneral.RutaPlantillas, sPlantilla));

        int iRow = 8;

        if (xpExcel.PrepararPlantilla_Web())
        {
            xpExcel.GeneraExcel();
            leerToExcel.Leer();

            xpExcel.Agregar(DtGeneral.NombreEmpresa, 2, 2);
            xpExcel.Agregar("Reporte de Existencias de Claves SSA " + sFarmacia, 3, 2);
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