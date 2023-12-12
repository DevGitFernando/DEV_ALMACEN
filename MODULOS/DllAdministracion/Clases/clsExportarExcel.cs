using System;
using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Reportes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace DllAdministracion
{
    public class clsEncabezadoExcel
    {
        public int Renglon = 1;
        public int Columna = 1;
        public string Titulo = ""; 
    }

    public class clsExportarExcel 
    {
        ArrayList pEncabezado; // = new ArrayList(); 
        string sNombreExportar = "";
        
        int iRowHeader = 1;
        int iRowHeader_Pos = 1; 
        int iColumnHeader = 1;
        int iColumnHeader_Pos = 1;
        int iSeparador = 1; 

        public clsExportarExcel()
        {
            pEncabezado = new ArrayList(); 
        }

        public void Exportar(DataSet dtReporte, string NombreReporte)
        {
            Exportar(dtReporte.Tables[0], NombreReporte);
        }

        #region Propiedades Publicas 
        public int InicioRenglonEncabezado
        {
            get { return iRowHeader; }
            set 
            { 
                iRowHeader = value;
                iRowHeader_Pos = value; 
            }
        }

        public int InicioColumnaEncabezado
        {
            get { return iColumnHeader; }
            set
            {
                iColumnHeader = value;
                iColumnHeader_Pos = value;
            }
        } 

        public int Separador
        {
            get { return iSeparador; }
            set { iSeparador = value; }
        } 
        #endregion Propiedades Publicas

        #region Encabezado Reporte 
        public void AddEncabezado(string Titulo, int Renglon, int Columna)
        {
            clsEncabezadoExcel enc = new clsEncabezadoExcel();

            enc.Renglon = Renglon;
            enc.Columna = Columna;
            enc.Titulo = Titulo; 

            pEncabezado.Add(enc); 

        }
        #endregion Encabezado Reporte

        public void Exportar(DataTable dtReporte, string NombreReporte)
        {
            int iRow_Inicial = 0;
            int iDez = 1; 
            string sNombreReporte = NombreReporte;
            int iColumnas = dtReporte.Columns.Count + iColumnHeader;
            int iRenglones = dtReporte.Rows.Count;
            DataColumn Col = new DataColumn();
            Excel.Application oXL;
            Excel.Workbook oWB;
            Excel.Worksheet oSheet;
            Excel.Range oRange;

            // Start Excel and get Application object.
            oXL = new Excel.Application();

            // Set some properties  
            oXL.Visible = true;
            oXL.DisplayAlerts = false;  

            // Get a new workbook.
            oWB = oXL.Workbooks.Add(Missing.Value);

            // Get the active sheet
            oSheet = (Excel.Worksheet)oWB.ActiveSheet;
            oSheet.Name = "Customers";

            // Process the DataTable
            // BE SURE TO CHANGE THIS LINE TO USE *YOUR* DATATABLE
            //DataTable dtReporte = Customers.RetrieveAsDataTable();

            // Asignar Encabezado Formateado 
            int rowCount = iRowHeader_Pos; 
            foreach(clsEncabezadoExcel enc in pEncabezado )
            {
                // oSheet.Cells[enc.Renglon, enc.Columna]= enc.Titulo;
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
                        oSheet.Cells[rowCount, (i + (iColumnHeader_Pos - iDez))] = dtReporte.Columns[i - 1].ColumnName;

                        // Se pone en negritas y color el encabezado.
                        oSheet.get_Range(oXL.Cells[rowCount, (1 + (iColumnHeader_Pos - iDez))], oXL.Cells[rowCount, iColumnas]).Font.Bold = true; //Letra negrita 
                        oSheet.get_Range(oXL.Cells[rowCount, (1 + (iColumnHeader_Pos - iDez))], oXL.Cells[rowCount, iColumnas]).Interior.ColorIndex = 24; //Color de Fondo, 9 es rojo oscuro, entre 0-56 

                        //// Typear las Columnas 
                        // oSheet.get_Range(oXL.Cells[rowCount, (1 + (iColumnHeader_Pos - iDez))], oXL.Cells[rowCount, iColumnas]).


                        //Se pone el borde.
                        oSheet.get_Range(oXL.Cells[rowCount, (1 + (iColumnHeader_Pos - iDez))], oXL.Cells[rowCount, iColumnas]).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, 1);  
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
                    oSheet.Cells[rowCount, i + iCol_Head] = dr[i - 1].ToString();
                }
            }


            // Resize the columns
            oRange = oSheet.get_Range(oSheet.Cells[iRow_Inicial, iColumnHeader_Pos], oSheet.Cells[iRow_Inicial, dtReporte.Columns.Count]); 
            oRange.EntireColumn.AutoFit();

            // Asignar Encabezado Formateado 
            foreach (clsEncabezadoExcel enc in pEncabezado)
            {
                oSheet.Cells[enc.Renglon, enc.Columna]= enc.Titulo;
                rowCount = enc.Renglon;
            }

            // Save the sheet and close
            oSheet = null; 
            oRange = null; 
            oWB.SaveAs( sNombreReporte + ".xls", Excel.XlFileFormat.xlWorkbookNormal, 
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, 
                Excel.XlSaveAsAccessMode.xlExclusive, 
                Missing.Value, Missing.Value, Missing.Value, 
                Missing.Value, Missing.Value); 
            //oWB.Close(Missing.Value, Missing.Value, Missing.Value);
            oWB = null; 
            //oXL.Quit();

            // Eliminar todos los recursos tomados 
            oRange = null;
            oSheet = null;
            oWB = null;
            oXL = null;
            
            // Clean up
            // NOTE: When in release mode, this does the trick
            GC.WaitForPendingFinalizers(); 
            GC.Collect(); 
            GC.WaitForPendingFinalizers(); 
            GC.Collect(); 


        }
    } 
}
