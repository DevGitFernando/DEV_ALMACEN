using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Data;

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
using Dll_IFacturacion;

namespace Facturacion.GenerarRemisiones
{
    class clsOffice
    {

        basGenerales Fg = new basGenerales();
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        ExcelPackage ArchivoExcel;
        ExcelWorksheet HojaDeTrabajo;

        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error;

        string sNombreArchivo = "";
        string sRutaArchivo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);



        int iTamañoLetra = 11;
        int iTamañoLetraEncabezado = 20;
        FontStyle fEstiloLetra = FontStyle.Regular;
        string sFuente = "Calibri";
        Color cColorDeLetra = Color.Black;
        Color cColorDeFondo = Color.Gray;
        
        SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment eOrientacionHorizontal= SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
        SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment eOrientacionVertical = SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle eEstiloDeRelleno= SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle.Solid;


        #region Constructor de Clase
        public clsOffice()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, "clsOffice", "");

            //sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //sRutaDestino += @"\DOCUMENTOS_REMISIONES\";
            //CrearDirectorio(sRutaDestino);

            leer = new clsLeer(ref cnn);

            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "Facturacion.GenerarRemisiones.clsOffice");


            AsignarColorDeFondo();

        }

        ~clsOffice()
        {
        }
        #endregion Constructor de Clase


        #region Propiedades
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

        public Color ColorDeLetra 
        {
            get { return cColorDeLetra; }
            set { cColorDeLetra = value; }
        }

        public Color ColorDeFondo 
        {
            get { return cColorDeFondo; }
            set { cColorDeFondo = value; }
        }

        public SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment OrientacionHorizontal
        {
            get { return eOrientacionHorizontal; }
            set { eOrientacionHorizontal = value; }
        }

        public SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment OrientacionVertical
        {
            get { return eOrientacionVertical; }
            set { eOrientacionVertical = value; }
        }

        public SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle EstiloDeRelleno
        {
            get { return eEstiloDeRelleno; }
            set { eEstiloDeRelleno = value; }
        }        

        #endregion Propiedades


        #region Funciones y Procedimientos privados

        private void AsignarColorDeFondo()
        {
            if (DtGeneral.EmpresaConectada == "001")
            {
                cColorDeFondo = Color.DarkSeaGreen;
            }

            if (DtGeneral.EmpresaConectada == "002")
            {
                cColorDeFondo = Color.CornflowerBlue;
            }
        }

        #endregion Funciones y Procedimientos privados


        #region Funciones y Procedimientos Publicos

        public bool CrearArchivo()
        {
            return CrearArchivo(true, "HOJA1");
        }

        public bool CrearArchivo(string NombreHoja)
        {
           return CrearArchivo(false, NombreHoja);
        }

        public bool CrearArchivo(bool Reemplazar, string  NombreHoja)
        {
            bool bRegresa = false;
            string sRutaCompleta = Path.Combine(sRutaArchivo, sNombreArchivo);

            FileInfo File = new FileInfo(sRutaCompleta);

            try
            {
                if (Reemplazar && File.Exists)
                {
                    File.Delete();
                }

                File = new FileInfo(sRutaCompleta);

                if (!File.Exists)
                {
                    ArchivoExcel = new ExcelPackage(File);
                    HojaNueva(NombreHoja);
                    bRegresa = true;
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
            };

            return bRegresa;
        }
        
        public void HojaNueva(string NombreHoja)
        {
            try
            {

                HojaDeTrabajo = ArchivoExcel.Workbook.Worksheets.Add(NombreHoja);
            }
            catch { };
        }

        public void AutoAjustaColumnas()
        {
            AutoAjustaColumnas(30, 100);
        }

        public void AutoAjustaColumnas(int Minimo, int Maximo )
        {
            HojaDeTrabajo.Cells.AutoFitColumns(Minimo, Maximo);
        }


        //public void EscribirCelda(int Renglon, int Columna, int ColumnaFin, string Informacion)
        //{
        //    EscribirCeldafull(Renglon, Columna, ColumnaFin, sFuente, Informacion, iTamañoLetra, FontStyle.Regular, cColorDeLetra, cColorDeFondo,
        //        SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment.Center,
        //        SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle.Solid, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        //}

        public void EscribirCelda(int Renglon, int Columna, string Informacion, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment OrientacionHorizontal, string Tipo)
        {
            string Formato = "@"; //Texto
            SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment exOrientacionHorizontal = OrientacionHorizontal;

            switch (Tipo)
                {
                    case "DateTime":
                        Formato = "yyyy/mm/dd";
                        break;

                    case "Int":
                        Formato = "#,##0";
                        exOrientacionHorizontal = SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        break;
                    case "Decimal":
                        Formato = "#,##0.0000";
                        exOrientacionHorizontal = SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        break;
                }

            if (HojaDeTrabajo != null)
            {
                //Create Headers and format them 
                HojaDeTrabajo.Cells[Renglon, Columna].Value = Informacion;
                ExcelRange r = HojaDeTrabajo.Cells[Renglon, Columna];

                r.Style.Font.SetFromFont(new Font(Fuente, TamañoLetra, EstiloLetra));
                r.Style.Font.Color.SetColor(ColorDeLetra);
                r.Style.HorizontalAlignment = exOrientacionHorizontal;
                r.Style.VerticalAlignment = OrientacionVertical;
                r.Style.Numberformat.Format = Formato;
            }
        }

        public void EscribirCeldaEncabezado(int Renglon, int Columna, int ColumnaFin, string Informacion)
        {
            EscribirCeldafull(Renglon, Columna, ColumnaFin, sFuente, Informacion, iTamañoLetraEncabezado, FontStyle.Bold, cColorDeLetra, cColorDeFondo,
                SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment.Center,
                SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle.Solid, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        }

        public void EscribirCeldaEncabezado(int Renglon, int Columna, int ColumnaFin, string Informacion, int TamañoLetra,
                SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment OrientacionHorizontal)
        {
            EscribirCeldafull(Renglon, Columna, ColumnaFin, sFuente, Informacion, TamañoLetra, FontStyle.Bold, cColorDeLetra, cColorDeFondo,
                OrientacionHorizontal, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment.Center,
                SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle.Solid, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        }
        
        public void EscribirCeldafull
            (
            int Renglon, int Columna, int ColumnaFin, string Fuente, string Informacion, int TamañoLetra, FontStyle EstiloLetra,
            Color ColorDeLetra, Color ColorDeFondo,
            SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment OrientacionHorizontal,
            SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment OrientacionVertical,
            SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle EstiloDeRelleno,
            SC_SolutionsSystem.OfficeOpenXml.Style.ExcelBorderStyle EstiloDeBorde
            )
        {
            if (HojaDeTrabajo != null)
            {
                //Create Headers and format them 
                HojaDeTrabajo.Cells[Renglon, Columna].Value = Informacion;
                ExcelRange r = HojaDeTrabajo.Cells[Renglon, Columna, Renglon, ColumnaFin];

                r.Merge = true;
                r.Style.Font.SetFromFont(new Font(Fuente, TamañoLetra, EstiloLetra));
                r.Style.Font.Color.SetColor(ColorDeLetra);
                r.Style.HorizontalAlignment = OrientacionHorizontal;
                r.Style.VerticalAlignment = OrientacionVertical;
                r.Style.Fill.PatternType = EstiloDeRelleno;
                r.Style.Fill.BackgroundColor.SetColor(ColorDeFondo);
                r.Style.Border.BorderAround(EstiloDeBorde);
            }
        }


        public void LlenarDetalleVertical(int Renglon, int Columna, DataSet Datos)
        {
            leer.DataSetClase = Datos;

            int iRenglon = Renglon;

            foreach (DataColumn col in leer.DataTableClase.Columns)
            {
                EscribirCeldaEncabezado(iRenglon++, Columna, col.ColumnName, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Right);
            }

            while (leer.Leer())
            {
                Columna++;
                iRenglon = Renglon;

                foreach ( DataColumn col  in leer.DataTableClase.Columns)
                {

                   EscribirCelda(iRenglon, Columna, leer.Campo(col.ColumnName), SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, col.DataType.Name);
                   iRenglon++;
                }
            }

        }


        public void LlenarDetalleHorizontal(int Renglon, int Columna, DataSet Datos)
        {
            leer.DataSetClase = Datos;


            int iColumna = Columna;

            foreach (DataColumn col in leer.DataTableClase.Columns)
            {
                EscribirCeldaEncabezado(Renglon, iColumna++, col.ColumnName, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Center);
            }

            while (leer.Leer()) 
            {
                Renglon++;
                iColumna = Columna;

                foreach (DataColumn col in leer.DataTableClase.Columns)
                {
                    EscribirCelda(Renglon, iColumna, leer.Campo(col.ColumnName), SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, col.DataType.Name);
                    iColumna++;
                }
            }
        }

        public void CerraArchivo()
        {
            ArchivoExcel.Save();
        }

        #endregion Funciones y Procedimientos Publicos

        private void EscribirCeldaEncabezado(int Renglon, int Columna, string Informacion, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelHorizontalAlignment OrientacionHorizontal)
        {
            EscribirCeldafull(Renglon, Columna, Columna, sFuente, Informacion, iTamañoLetra + 3, FontStyle.Bold, cColorDeLetra, cColorDeFondo,
                OrientacionHorizontal, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelVerticalAlignment.Center,
                SC_SolutionsSystem.OfficeOpenXml.Style.ExcelFillStyle.Solid, SC_SolutionsSystem.OfficeOpenXml.Style.ExcelBorderStyle.Thick);
        }
    }
}
