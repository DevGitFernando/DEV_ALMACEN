using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using ClosedXML.Excel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.OfficeOpenXml;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Reporteador;

//using Aspose.Cells; 

namespace DllFarmaciaSoft.ExportarExcel
{
    public enum ExportFormat
    {
        CSV = 1,
        Excel = 2
    }

    public enum ExportApplicationType
    {
        Win = 1,
        Web = 2
    }

    public class clsGenerarExcel
    {

        basGenerales Fg = new basGenerales();
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";

        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        string sExtension = "";
        string sNombreDocumento_Base = "";
        string sNombreArchivo = "";
        string sRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string sRutaSalidaExcel = "";
        bool bAgregarMarcaDeTiempo = false;

        int iTamañoLetra = 11;
        int iTamañoLetraEncabezado = 20;
        FontStyle fEstiloLetra = FontStyle.Regular;
        string sFuente = "Calibri";

        SC_SolutionsSystem.OfficeOpenXml.ExcelPackage xmlArchivoOpenXmlExcel;
        //SC_SolutionsSystem.OfficeOpenXml.ExcelWorksheet 

        XLWorkbook xmlArchivoExcel;
        IXLWorksheet HojaDeTrabajo;
        XLTableTheme thThema = XLTableTheme.None;

        XLColor cColorDeLetra = XLColor.Black;
        XLColor cColorDeFondo = XLColor.Gray;
        XLThemeColor themeColor = XLThemeColor.Text2;

        XLAlignmentHorizontalValues eOrientacionHorizontal = XLAlignmentHorizontalValues.CenterContinuous;
        XLAlignmentVerticalValues eOrientacionVertical = XLAlignmentVerticalValues.Center;

        XLFillPatternValues eEstiloDeRelleno = XLFillPatternValues.Solid;

        #region Constructor de Clase
        public clsGenerarExcel()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, "clsGenerarExcel", "");

            //sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //sRutaDestino += @"\DOCUMENTOS_REMISIONES\";
            //CrearDirectorio(sRutaDestino);

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "DllFarmaciaSoft.ExportarExcel.clsGenerarExcel");

            AsignarColorDeFondo();
        }

        ~clsGenerarExcel()
        {
        }
        #endregion Constructor de Clase

        #region Propiedades
        public SC_SolutionsSystem.OfficeOpenXml.ExcelPackage ArchivoExcelOpenXml
        {
            get { return xmlArchivoOpenXmlExcel; }
            set { xmlArchivoOpenXmlExcel = value; }
        }
        public XLWorkbook ArchivoExcel
        {
            get { return xmlArchivoExcel; }
            set { xmlArchivoExcel = value; }
        }

        public string NombreDocumento
        {
            get { return sNombreDocumento_Base; }
        }

        public string Extension
        {
            get { return sExtension; }
        }

        public string NombreArchivo
        {
            get { return sNombreArchivo; }
            set { sNombreArchivo = value; }
        }

        public string RutaArchivo
        {
            get { return sRutaArchivo; }
            set { sRutaArchivo = value; }
        }

        public FontStyle EstiloLetra
        {
            get { return fEstiloLetra; }
            set { fEstiloLetra = value; }
        }

        public int TamañoLetra
        {
            get { return iTamañoLetra; }
            set { iTamañoLetra = value; }
        }

        public int TamañoLetraEncabezado
        {
            get { return iTamañoLetraEncabezado; }
            set { iTamañoLetraEncabezado = value; }
        }

        public string Fuente
        {
            get { return sFuente; }
            set { sFuente = value; }
        }

        public XLColor ColorDeLetra 
        {
            get { return cColorDeLetra; }
            set { cColorDeLetra = value; }
        }

        public XLColor ColorDeFondo 
        {
            get { return cColorDeFondo; }
            set { cColorDeFondo = value; }
        }

        public XLAlignmentHorizontalValues OrientacionHorizontal
        {
            get { return eOrientacionHorizontal; }
            set { eOrientacionHorizontal = value; }
        }

        public XLAlignmentVerticalValues OrientacionVertical
        {
            get { return eOrientacionVertical; }
            set { eOrientacionVertical = value; }
        }

        public XLFillPatternValues EstiloDeRelleno
        {
            get { return eEstiloDeRelleno; }
            set { eEstiloDeRelleno = value; }
        }

        public XLTableTheme Theme
        {
            get { return thThema; }
            set { thThema = value;  }
        }

        public bool AgregarMarcaDeTiempo
        {
            get { return bAgregarMarcaDeTiempo; }
            set { bAgregarMarcaDeTiempo = value; }
        }

        public string MarcaDeTiempo
        {
            get { return getMarcaDeTiempo(); }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos privados

        private void AsignarColorDeFondo()
        {
            if (DtGeneral.EmpresaConectada == "001")
            {
                cColorDeFondo = XLColor.DarkSeaGreen;
                thThema = XLTableTheme.TableStyleLight15;
                themeColor = XLThemeColor.Accent2;
            }

            if (DtGeneral.EmpresaConectada == "002")
            {
                cColorDeFondo = XLColor.CornflowerBlue;
                thThema = XLTableTheme.TableStyleLight15;
                themeColor = XLThemeColor.Accent2;
            }

            thThema = XLTableTheme.None; 
            cColorDeFondo = XLColor.FromTheme(themeColor);
            cColorDeFondo = XLColor.Transparent; 
        }

        #endregion Funciones y Procedimientos privados

        #region Formatos de Exportacion 
        public void Exportar_Formato()
        {
            string sFileBase = sRutaSalidaExcel;
            string sFileSalida = "";

            FileInfo fileDatos = new FileInfo(sRutaSalidaExcel);

            sExtension = fileDatos.Extension;
            sFileSalida = fileDatos.FullName.Replace(sExtension, "") + ".csv";

            //Aspose.Cells.Workbook wb = new Workbook(sFileBase);

            //wb.Save(sFileSalida, SaveFormat.Csv); 

            ////IronXL.WorkBook ironWorkBook = IronXL.WorkBook.Load(sRutaSalidaExcel);

            ////ironWorkBook.SaveAsCsv(sFileSalida);

            ////ironWorkBook = null; 
        }
        #endregion Formatos de Exportacion 

        #region Funciones y Procedimientos Publicos 
        public void AbrirDirectorioDestino()
        {
            AbrirDirectorioDestino(false, "");
        }

        public void AbrirDirectorioDestino(bool Confirmar)
        {
            AbrirDirectorioDestino(Confirmar, ""); 
        }

        public void AbrirDirectorioDestino(string Mensaje)
        {
            AbrirDirectorioDestino(true, Mensaje);
        }

        public void AbrirDirectorioDestino(bool Confirmar, string Mensaje)
        {
            bool bAbrir = false;
            string sMensaje = "Exportación finalizada.\n\n¿ Desea abrir el directorio destino ?";

            try
            {
                if (!Confirmar)
                {
                    bAbrir = true;
                }
                else
                {
                    sMensaje = Mensaje != "" ? Mensaje : sMensaje;
                    bAbrir = General.msjConfirmar(sMensaje) == DialogResult.Yes;
                }

                if (bAbrir)
                {
                    General.AbrirDirectorio(sRutaArchivo);
                }
            }
            catch
            {
                General.msjAviso("No fue posible abrir el archivo generado.");
            }
        }

        public void AbrirDocumentoGenerado()
        {
            AbrirDocumentoGenerado(false, ""); 
        }

        public void AbrirDocumentoGenerado(bool Confirmar)
        {
            AbrirDocumentoGenerado(Confirmar, "");
        }

        public void AbrirDocumentoGenerado(string Mensaje)
        {
            AbrirDocumentoGenerado(true, Mensaje);
        }

        public void AbrirDocumentoGenerado(bool Confirmar, string Mensaje)
        {
            bool bAbrir = false;
            string sMensaje = "Exportación finalizada.\n\n¿ Desea abrir el documento generado ?"; 

            try
            {
                if (!Confirmar)
                {
                    bAbrir = true;
                }
                else
                {
                    sMensaje = Mensaje != "" ? Mensaje : sMensaje;
                    bAbrir = General.msjConfirmar(sMensaje) == DialogResult.Yes; 
                }

                if (bAbrir)
                {
                    General.AbrirDocumento(Path.Combine(sRutaArchivo, sNombreArchivo));
                }
            }
            catch
            {
                General.msjAviso("No fue posible abrir el archivo generado.");
            }
        }

        public bool PrepararPlantilla()
        {
            return PrepararPlantilla(""); 
        }

        public bool PrepararPlantilla(string NombreDefaultDocumento)
        {
            bool bRegresa = false;
            string NombreReporte = "";

            NombreReporte = NombreDefaultDocumento;  // .Replace(".xls", ".xls");

            if (bAgregarMarcaDeTiempo)
            {
                NombreReporte = NombreReporte + "_" + getMarcaDeTiempo(); //  NombreReporte.Replace(".xls", "_" + MarcaDeTiempo() + ".xls");
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Guardar información en formato Excel";
            saveFile.Filter = "Archivo de Excel | *.xlsx";
            saveFile.InitialDirectory = Application.StartupPath;
            saveFile.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            saveFile.AddExtension = true;

            saveFile.FileName = NombreReporte;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                NombreReporte = saveFile.FileName;

                FileInfo fileDatos = new FileInfo(NombreReporte);

                sExtension = fileDatos.Extension;
                sNombreDocumento_Base = fileDatos.Name.Replace(sExtension, "");

                bRegresa = CrearArchivo(fileDatos.DirectoryName, fileDatos.Name, true, "Hoja1");
            }

            return bRegresa;
        }

        public bool CrearArchivo()
        {
            bool bRegresa = false;

            bRegresa = CrearArchivo(sRutaArchivo, sNombreArchivo, true, "Hoja1");

            return bRegresa; 
        }

        public bool CrearArchivo(string Ruta, string NombreDocumento)
        {
            bool bRegresa = false;

            bRegresa = CrearArchivo(Ruta, NombreDocumento, true, "Hoja1");

            return bRegresa; 
        }

        public bool AbrirArchivo(string Ruta, string NombreDocumento)
        {
            bool bRegresa = false;
            sRutaArchivo = Ruta;
            sNombreArchivo = NombreDocumento;
            sRutaSalidaExcel = Path.Combine(sRutaArchivo, sNombreArchivo);

            FileInfo File = new FileInfo(sRutaSalidaExcel);

            try
            {
                File = new FileInfo(sRutaSalidaExcel);

                if (File.Exists)
                {
                    xmlArchivoExcel = new XLWorkbook(sRutaSalidaExcel);
                    bRegresa = true;
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            return bRegresa; 
        }
        public bool CrearArchivo(string Ruta, string NombreDocumento, bool Reemplazar, string  NombreHoja)
        {
            bool bRegresa = false;
            sRutaArchivo = Ruta;
            sNombreArchivo = NombreDocumento; 
            sRutaSalidaExcel = Path.Combine(sRutaArchivo, sNombreArchivo);

            FileInfo File = new FileInfo(sRutaSalidaExcel);

            try
            {
                if (Reemplazar && File.Exists)
                {
                    File.Delete();
                }

                File = new FileInfo(sRutaSalidaExcel);

                if (!File.Exists)
                {
                    if (xmlArchivoExcel != null)
                    {
                        xmlArchivoExcel.Dispose();
                        xmlArchivoExcel = null;
                        GC.Collect();
                    }

                    //xmlArchivoOpenXmlExcel = new ExcelPackage();

                    xmlArchivoExcel = new XLWorkbook(XLEventTracking.Disabled);
                    //xmlArchivoExcel.Worksheets.Add("Hoja1");
                    //xmlArchivoExcel.SaveAs(sRutaSalidaExcel);

                    //xmlArchivoExcel = new XLWorkbook(sRutaSalidaExcel, XLEventTracking.Disabled);
                    bRegresa = true;
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            }

            return bRegresa;
        }
        
        public void HojaNueva(string NombreHoja)
        {
            try
            {

                //HojaDeTrabajo = xmlArchivoExcel.Workbook.Worksheets.Add(NombreHoja);
            }
            catch { };
        }

        public void AutoAjustaColumnas()
        {
            AutoAjustaColumnas(30, 100);
        }

        public void AutoAjustaColumnas(int Minimo, int Maximo )
        {
            //HojaDeTrabajo.Cells().a .AutoFitColumns(Minimo, Maximo);
        }

        public void EscribirCelda(string NombreHoja, int Renglon, int Columna, 
            string Informacion, XLAlignmentHorizontalValues OrientacionHorizontal, string Tipo)
        {
            EscribirCelda(NombreHoja, Renglon, Columna, iTamañoLetra, Informacion, OrientacionHorizontal, Tipo); 
        }

        public void EscribirCelda(string NombreHoja, int Renglon, int Columna, int TamañoLetra, 
            string Informacion, XLAlignmentHorizontalValues OrientacionHorizontal, string Tipo)
        {
            string Formato = "@"; //Texto
            XLAlignmentHorizontalValues exOrientacionHorizontal = OrientacionHorizontal; 
            //SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment exOrientacionHorizontal = OrientacionHorizontal;

            switch (Tipo)
                {
                    case "DateTime":
                        Formato = "yyyy/mm/dd";
                        break;

                    case "Int":
                        Formato = "###,###,###,###,##0";
                        exOrientacionHorizontal = XLAlignmentHorizontalValues.Right; 
                        //exOrientacionHorizontal = SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        break;

                    case "Decimal":
                        Formato = "###,###,###,###,##0.0000";
                        exOrientacionHorizontal = XLAlignmentHorizontalValues.Right; 
                        //exOrientacionHorizontal = SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        break;
                }

            //if (HojaDeTrabajo != null)
            {
                xmlArchivoExcel.Worksheet(NombreHoja).Cell(Renglon, Columna).Value = Informacion;
                IXLRange r = xmlArchivoExcel.Worksheet(NombreHoja).Range(Renglon, Columna, Renglon, Columna);

                r.Style.Font.SetFontName(Fuente); //SetFromFont(new Font(Fuente, TamañoLetra, EstiloLetra));
                r.Style.Font.SetFontSize(TamañoLetra);
                r.Style.Font.SetFontColor(ColorDeLetra);
                r.Style.Alignment.SetHorizontal(exOrientacionHorizontal);
                r.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                r.Style.Alignment.SetShrinkToFit(true); 
                r.Style.NumberFormat.SetFormat(Formato); 
            }
        }

        private void EscribirCeldaEncabezado(string NombreHoja, int Renglon, int Columna, string Informacion, XLAlignmentHorizontalValues OrientacionHorizontal)
        {
            EscribirCeldafull(NombreHoja, Renglon, Columna, Columna, sFuente, Informacion, iTamañoLetra, true, cColorDeLetra, cColorDeFondo,
                OrientacionHorizontal, XLAlignmentVerticalValues.Center, XLFillPatternValues.Solid, XLBorderStyleValues.Thick);
        }

        public void EscribirCeldaEncabezado(string NombreHoja, int Renglon, int Columna, int ColumnaFin, string Informacion)
        {
            EscribirCeldafull(NombreHoja, Renglon, Columna, ColumnaFin, sFuente, Informacion, iTamañoLetraEncabezado, true, cColorDeLetra, cColorDeFondo,
                XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLFillPatternValues.Solid, XLBorderStyleValues.Thick);
        }

        public void EscribirCeldaEncabezado(string NombreHoja, int Renglon, int Columna, int ColumnaFin, int TamañoLetra, string Informacion)
        {
            EscribirCeldafull(NombreHoja, Renglon, Columna, ColumnaFin, sFuente, Informacion, TamañoLetra, true, cColorDeLetra, cColorDeFondo,
                XLAlignmentHorizontalValues.Center, XLAlignmentVerticalValues.Center, XLFillPatternValues.Solid, XLBorderStyleValues.Thick);
        }

        public void EscribirCeldaEncabezado(string NombreHoja, int Renglon, int Columna, int ColumnaFin, string Informacion,
                XLAlignmentHorizontalValues OrientacionHorizontal)
        {
            EscribirCeldafull(NombreHoja, Renglon, Columna, ColumnaFin, sFuente, Informacion, iTamañoLetraEncabezado, true, cColorDeLetra, cColorDeFondo,
                OrientacionHorizontal, XLAlignmentVerticalValues.Center, XLFillPatternValues.Solid, XLBorderStyleValues.Thick);
        }

        public void EscribirCeldaEncabezado(string NombreHoja, int Renglon, int Columna, int ColumnaFin, int TamañoLetra, string Informacion, 
                XLAlignmentHorizontalValues OrientacionHorizontal)
        {
            EscribirCeldafull(NombreHoja, Renglon, Columna, ColumnaFin, sFuente, Informacion, TamañoLetra, true, cColorDeLetra, cColorDeFondo,
                OrientacionHorizontal, XLAlignmentVerticalValues.Center, XLFillPatternValues.Solid, XLBorderStyleValues.Thick);
        }
        
        public void EscribirCeldafull
            (
            string NombreHoja, 
            int Renglon, int Columna, int ColumnaFin, string Fuente, string Informacion, int TamañoLetra,  
            bool Bold,
            XLColor ColorDeLetra, XLColor ColorDeFondo, 
            XLAlignmentHorizontalValues OrientacionHorizontal,
            XLAlignmentVerticalValues OrientacionVertical,
            XLFillPatternValues EstiloDeRelleno, 
            XLBorderStyleValues EstiloDeBorde
            )
        {

            //if (HojaDeTrabajo != null)
            {
                IXLStyle style; 
                IXLRange r; 
                IXLWorksheet ws = xmlArchivoExcel.Worksheet(NombreHoja);
                ws.Cell(Renglon, Columna).Value = Informacion;


                //ws.Cell(Renglon, Columna).Style.Border.TopBorder = EstiloDeBorde;
                //ws.Cell(Renglon, Columna).Style.Border.LeftBorder = EstiloDeBorde;
                //ws.Cell(Renglon, Columna).Style.Border.RightBorder = EstiloDeBorde;
                //ws.Cell(Renglon, Columna).Style.Border.BottomBorder = EstiloDeBorde;
                 

                r = ws.Range(Renglon, Columna, Renglon, ColumnaFin); 
                r.Style.Font.SetFontName(Fuente); 
                r.Style.Font.SetFontSize(TamañoLetra);
                r.Style.Font.SetFontColor(ColorDeLetra);
                r.Style.Font.Bold = Bold; 
                r.Style.Alignment.SetHorizontal(OrientacionHorizontal);
                r.Style.Alignment.SetVertical(OrientacionVertical);

                //r.Style.Fill.PatternType = EstiloDeRelleno;
                //r.Style.Fill.SetBackgroundColor(ColorDeFondo); // new Color(ColorDeFondo); 

                r.Style.Border.TopBorder = EstiloDeBorde;
                r.Style.Border.LeftBorder = EstiloDeBorde;
                r.Style.Border.RightBorder = EstiloDeBorde;
                r.Style.Border.BottomBorder = EstiloDeBorde;
                style = r.Style;

                ////style = r.Style;
                ////style.Border.TopBorder = EstiloDeBorde;
                ////style.Border.LeftBorder = EstiloDeBorde;
                ////style.Border.RightBorder = EstiloDeBorde;
                ////style.Border.BottomBorder = EstiloDeBorde; 


               
                //r.Style.Border.BorderAround(EstiloDeBorde);

                r.Merge(); // = true;

            }
        }

        public void LlenarDetalleVertical(string NombreHoja, int Renglon, int Columna, DataSet Datos)
        {
            LlenarDetalleVertical(NombreHoja, Renglon, Columna, iTamañoLetra, iTamañoLetra, Datos); 
        }

        public void LlenarDetalleVertical(string NombreHoja, int Renglon, int Columna, int TamañoLetraTitulo, int TamañoLetraInformacion, DataSet Datos)
        {
            int iRenglon = Renglon;
            leer.DataSetClase = Datos;

            try
            {
                foreach (DataColumn col in leer.DataTableClase.Columns)
                {
                    EscribirCeldaEncabezado(NombreHoja, iRenglon++, Columna, Columna, TamañoLetraTitulo, col.ColumnName, XLAlignmentHorizontalValues.Right);
                }
                xmlArchivoExcel.Worksheet(NombreHoja).Columns().AdjustToContents(Renglon, iRenglon, 100);


                while (leer.Leer())
                {
                    Columna++;
                    iRenglon = Renglon;

                    foreach (DataColumn col in leer.DataTableClase.Columns)
                    {
                        EscribirCelda(NombreHoja, iRenglon, Columna, TamañoLetraInformacion, leer.Campo(col.ColumnName), XLAlignmentHorizontalValues.Justify, col.DataType.Name);
                        iRenglon++;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.GrabarError(ex, "LlenarDetalleHorizontal()");
            }
        }

        public void InsertarTabla(string NombreHoja, int Renglon, int Columna, DataSet Datos)
        {
            InsertarTabla(NombreHoja, Renglon, Columna, Datos, thThema); 
        }

        public void InsertarTabla(string NombreHoja, int Renglon, int Columna, DataSet Datos, int Thema)
        {
            XLTableTheme themeLocal = XLTableTheme.None;

            if (Thema >= 0 && Thema <= 60)
            {
                themeLocal = (XLTableTheme)Thema; 
            }

            InsertarTabla(NombreHoja, Renglon, Columna, Datos, themeLocal); 

        }

        public void InsertarTabla(string NombreHoja, int Renglon, int Columna, DataSet Datos, XLTableTheme Thema)
        {
            leer.DataSetClase = Datos;
            IXLWorksheet ws;
            IXLRange r;

            try
            {
                ////ExcelWorksheet wss = xmlArchivoOpenXmlExcel.Workbook.Worksheets[NombreHoja];
                ////wss.Cells[Renglon, Columna].LoadFromDataTable(leer.DataTableClase, true);
                ////wss.Cells.AutoFitColumns();

                //xmlArchivoExcel.Worksheets.Add(leer.Tabla(1), NombreHoja, Renglon, Columna);
                ws = xmlArchivoExcel.Worksheet(NombreHoja);
                ws.Cell(Renglon, Columna).InsertTable(leer.DataTableClase, NombreHoja).Theme = Thema;
                ////////ws.Cell(Renglon + 1, Columna).InsertData(leer.DataRowsClase);


                ws.SheetView.FreezeRows(Renglon);

                r = ws.Range(Renglon, Columna, Renglon, leer.DataTableClase.Columns.Count + 1);
                r.Style.Font.Bold = true;
                //r.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                r.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                r.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                r.Dispose();
                ws.Dispose();

            }
            catch (Exception ex)
            {
                Error.GrabarError(ex, "LlenarDetalleHorizontal()");
            }
        }

        public void LlenarDetalleHorizontal(string NombreHoja, int Renglon, int Columna, DataSet Datos)
        {
            leer.DataSetClase = Datos;
            IXLWorksheet ws;


            try
            {

                xmlArchivoExcel.Worksheets.Add(leer.Tabla(1), NombreHoja, Renglon, Columna);
                ws = xmlArchivoExcel.Worksheet(NombreHoja);
                ws.SheetView.FreezeRows(Renglon);
            }
            catch (Exception ex)
            {
                Error.GrabarError(ex, "LlenarDetalleHorizontal()");
            }
            //var firstCell = ws.FirstCellUsed();
            //var lastCell = ws.LastCellUsed();
            //var range = ws.Range(firstCell.Address, lastCell.Address);
            //var table = range.CreateTable();
            
            //table.Theme = XLTableTheme.TableStyleLight10;
            //table.ShowAutoFilter = false;



            //int iColumna = Columna;

            //foreach (DataColumn col in leer.DataTableClase.Columns)
            //{
            //    EscribirCeldaEncabezado(Renglon, iColumna++, col.ColumnName, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Center);
            //}

            //while (leer.Leer()) 
            //{
            //    Renglon++;
            //    iColumna = Columna;

            //    foreach (DataColumn col in leer.DataTableClase.Columns)
            //    {
            //        EscribirCelda(Renglon, iColumna, leer.Campo(col.ColumnName), SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, col.DataType.Name);
            //        iColumna++;
            //    }
            //}
        }

        public void GuardarDocumento(bool MantenerAbierto)
        {
            //FileInfo fileSalida = new FileInfo(sRutaSalidaExcel);
            //xmlArchivoOpenXmlExcel.SaveAs(fileSalida);

            //xmlArchivoOpenXmlExcel.Dispose();

            GC.Collect();
            //GC.WaitForFullGCComplete(); 

            try
            {
                xmlArchivoExcel.SaveAs(sRutaSalidaExcel);
            }
            catch(Exception ex)
            {
            }
            finally 
            {
                xmlArchivoExcel.Dispose();
            }
            GC.Collect();
            //////GC.WaitForFullGCComplete();

            //////if (MantenerAbierto)
            //////{
            //////    xmlArchivoExcel = new XLWorkbook(sRutaSalidaExcel, XLEventTracking.Disabled);
            //////}
        }
        public void CerrarArchivo()
        {
            CerraArchivo();
            //GuardarDocumento(); 
        }

        public void CerraArchivo()
        {
            GC.Collect();
            //GC.WaitForFullGCComplete(); 

            try
            {
                xmlArchivoExcel.SaveAs(sRutaSalidaExcel);
            }
            catch
            {
            }
            finally
            {
                xmlArchivoExcel.Dispose();
            }
            GC.Collect();

            ////using (MemoryStream MyMemoryStream = new MemoryStream())
            ////{
            ////    ////SaveOptions saveOptions = new SaveOptions
            ////    ////{
            ////    ////    EvaluateFormulasBeforeSaving = true,
            ////    ////    GenerateCalculationChain = false,
            ////    ////    ValidatePackage = false
            ////    ////};

            ////    xmlArchivoExcel.SaveAs(MyMemoryStream);
            ////    FileStream file = new FileStream(sRutaSalidaExcel, FileMode.Create, FileAccess.Write);
            ////    MyMemoryStream.WriteTo(file);
            ////    file.Close();

            ////    //xmlArchivoExcel.SaveAs(MyMemoryStream);
            ////}
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private string getMarcaDeTiempo()
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
}
