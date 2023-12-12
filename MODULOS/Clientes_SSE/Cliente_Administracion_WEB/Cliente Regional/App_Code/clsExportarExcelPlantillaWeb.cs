using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;

using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

using SC_SolutionsSystem.OfficeExcel;
using SC_SolutionsSystem.OfficeExcel.Style;

public class clsEncabezadoExcel
{
    public int Renglon = 1;
    public int Columna = 1;
    public string Titulo = "";
}

/// <summary>
/// Descripción breve de clsExportarExcelPlantillaWeb
/// </summary>
public class clsExportarExcelPlantillaWeb
{
    FileInfo filePlantilla;

    ExcelPackage pckTemplate;
    ExcelPackage pck;
    ExcelWorksheet oSheet;

    ArrayList pEncabezado;

    string sRutaPlantilla = "";

    int iRowHeader_Pos = 1;
    int iColumnHeader = 1;
    int iColumnHeader_Pos = 1;
    int iSeparador = 1;

    bool bSeHabilitoGeneracion = false;

    byte[] btExecelGenerado;
    object oStream;

    public clsExportarExcelPlantillaWeb(string Plantilla)
    {
        pEncabezado = new ArrayList();
        this.sRutaPlantilla = Plantilla;
        filePlantilla = new FileInfo(Plantilla);
    }

    public bool PrepararPlantilla_Web()
    {
        bool bRegresa = false;

        try
        {
            pckTemplate = new ExcelPackage(filePlantilla);
            pckTemplate.Save();
            oStream = pckTemplate.Stream;

            if (oStream != null)
                bRegresa = true;
        }
        catch { }

        return bRegresa;
    }

    public void Exportar(DataSet Informacion, string NombreReporte)
    {
        Exportar(Informacion.Tables[0], NombreReporte);
    }

    #region Propiedades Publicas
    public byte[] ExcelGeneradoBytes
    {
        get { return btExecelGenerado; }
    }
    #endregion Propiedades Publicas

    #region Funciones y Procedimientos Publicos
    public bool GeneraExcel()
    {
        bool bRegresa = false;

        try
        {
            pck = new ExcelPackage(new MemoryStream(), oStream as Stream);
            oSheet = pck.Workbook.Worksheets[1];

            bRegresa = true;
        }
        catch
        {
        }

        bSeHabilitoGeneracion = bRegresa;
        return bRegresa;
    }

    public bool CerrarDocumento()
    {
        bool bRegresa = false;

        try
        {
            btExecelGenerado = pck.GetAsByteArray();

            LiberarObjeto(pckTemplate);
            LiberarObjeto(pck);
            LiberarObjeto(oSheet);

            bRegresa = true;
        }
        catch { }

        return bRegresa;
    }

    public bool Agregar(object Dato, int Renglon, int Columna)
    {
        bool bRegresa = false;

        try
        {
            oSheet.Cells[Renglon, Columna].Value = Dato;
            bRegresa = true;
        }
        catch { }

        return bRegresa;
    }

    private void Exportar(DataTable dtReporte, string NombreReporte)
    {
        int iRow_Inicial = 0;
        int iDez = 1;
        string sNombreReporte = NombreReporte;
        int iColumnas = dtReporte.Columns.Count + iColumnHeader;
        int iRenglones = dtReporte.Rows.Count;
        DataColumn Col = new DataColumn();

        oSheet.Name = dtReporte.TableName;

        // Asignar Encabezado Formateado 
        int rowCount = iRowHeader_Pos;
        foreach (clsEncabezadoExcel enc in pEncabezado)
        {
            oSheet.Cells[enc.Renglon, enc.Columna].Value = enc.Titulo;
            rowCount = enc.Renglon;
        }


        rowCount = rowCount + iSeparador;
        iRow_Inicial = rowCount;
        //foreach (DataRow dr in dtReporte.Rows)
        {
            //// Encabezado de Columnas 
            rowCount += 1;
            for (int i = 1; i < dtReporte.Columns.Count + 1; i++)
            {
                // Add the header the first time through
                //if (rowCount == 2)
                {
                    //rowCount--; 
                    oSheet.Cells[rowCount, (i + (iColumnHeader_Pos - iDez))].Value = dtReporte.Columns[i - 1].ColumnName;
                }
            }
            //break; 
        }

        // Vaciar el contenido de la Tabla  
        int iCol_Head = iColumnHeader_Pos - iDez;
        foreach (DataRow dr in dtReporte.Rows)
        {
            rowCount++;
            for (int i = 1; i < dtReporte.Columns.Count + 1; i++)
            {
                oSheet.Cells[rowCount, i + iCol_Head].Value = dr[i - 1].ToString();
            }
        }


        // Asignar Encabezado Formateado 
        foreach (clsEncabezadoExcel enc in pEncabezado)
        {
            oSheet.Cells[enc.Renglon, enc.Columna].Value = enc.Titulo;
            rowCount = enc.Renglon;
        }

        // Clean up
        // NOTE: When in release mode, this does the trick
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
    #endregion Funciones y Procedimientos Publicos

    #region Funciones y Procedimientos Privados
    private void LiberarObjeto(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        }
        catch { }
        finally
        {
            GC.Collect();
        }
    }

    private string MarcaDeTiempo()
    {
        string sRegresa = "";
        DateTime dttime = DateTime.Now;
        basGenerales Fg = new basGenerales();

        sRegresa = Fg.PonCeros(dttime.Year, 4);
        sRegresa += Fg.PonCeros(dttime.Month, 2);
        sRegresa += Fg.PonCeros(dttime.Day, 2);

        sRegresa += "_";

        sRegresa += Fg.PonCeros(dttime.Hour, 2);
        sRegresa += Fg.PonCeros(dttime.Minute, 2);
        sRegresa += Fg.PonCeros(dttime.Second, 2);

        
        return sRegresa;
    }
    #endregion Funciones y Procedimientos Privados
}