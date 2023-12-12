using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

using ClosedXML.Excel;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using DllFarmaciaSoft.Reporteador;
using Dll_IFacturacion;

namespace Facturacion.GenerarRemisiones
{
    class clsRemision_GenerarDocumentos
    {
        #region Declaracion de Variables
        string sGUID = "";
        Label lblFechaProcesando = null;

        basGenerales Fg = new basGenerales();
        DateTime dtMarcaTiempo = DateTime.Now;
        string sMarcaTiempo = "";
        string sFile_PDF = "";
        clsDatosCliente DatosCliente;

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsLeer myLeer;
        clsLeer leerInf;
        clsLeer leerExec;
        clsGrabarError Error;
        string sRutaDestino = "";
        string sRutaDestino_PDF = "";
        string sRutaDestino_EXCEL = "";
        DataSet dtsRemision_Encabezado;
        DataSet dtsRemision_Detalles;


        clsLeer leerRemision_Encabezado;
        clsLeer leerRemision_Detalles;
        string sTabla_01_Encabezado = "Encabezado";
        string sTabla_02_Detalle = "Detalles";

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_REMISIONES_VALIDAR.xlsx";
        bool bExistePlantilla_Excel = false;

        bool bRemisionesManuales = false; 
        bool bGenerar_PDF = false;
        bool bGenerar_EXCEL = false;
        bool bGenerar_EXCEL_Contrarecibo = false;
        bool bGenerarDirectorio_Farmacia = false;

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public clsRemision_GenerarDocumentos()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, "clsRemision_GenerarDocumentos", "");

            //sRutaDestino = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //sRutaDestino += @"\DOCUMENTOS_REMISIONES\";
            //CrearDirectorio(sRutaDestino);

            cnn = new clsConexionSQL();
            cnn.DatosConexion.Servidor = General.DatosConexion.Servidor;
            cnn.DatosConexion.BaseDeDatos = General.DatosConexion.BaseDeDatos;
            cnn.DatosConexion.Usuario = General.DatosConexion.Usuario;
            cnn.DatosConexion.Password = General.DatosConexion.Password;
            cnn.DatosConexion.Puerto = General.DatosConexion.Puerto;
            cnn.DatosConexion.ConexionDeConfianza = General.DatosConexion.ConexionDeConfianza;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 

            myLeer = new clsLeer(ref cnn);
            leerRemision_Encabezado = new clsLeer();
            leerRemision_Detalles = new clsLeer();

            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "Facturacion.GenerarRemisiones.clsRemision_GenerarDocumentos");

            DescargarPlantillaExcel(); 
        }

        ~clsRemision_GenerarDocumentos()
        {
        }
        #endregion Constructor de Clase

        #region Propiedades
        public string GUID
        {
            get
            {
                if (sGUID == null) sGUID = "";
                return sGUID;
            }
            set { sGUID = value; }
        }

        public Label EtiquetaFechaEnProceso
        {
            set { lblFechaProcesando = value; }
        }

        public bool RemisionesManuales
        {
            get { return bRemisionesManuales; } 
            set { bRemisionesManuales = value; }
        }
        public bool Generar_PDF
        {
            get { return bGenerar_PDF; }
            set { bGenerar_PDF = value; }
        }

        public bool Generar_EXCEL
        {
            get { return bGenerar_EXCEL; }
            set { bGenerar_EXCEL = value; }
        }

        public bool Generar_EXCEL_Contrarecibo
        {
            get { return bGenerar_EXCEL_Contrarecibo; }
            set { bGenerar_EXCEL_Contrarecibo = value; }
        }

        public bool GenerarDirectorio_Farmacia
        {
            get { return bGenerarDirectorio_Farmacia; }
            set { bGenerarDirectorio_Farmacia = value; }
        }

        public string RutaDestinoReportes
        {
            get { return sRutaDestino; }
            set
            {
                sRutaDestino = value;
                if (sRutaDestino != "")
                {
                    sRutaDestino += @"\";
                    CrearDirectorio(sRutaDestino);
                }
            }
        }

        public string RutaPDF
        {
            get { return sRutaDestino_PDF; } 
        }
        public string RutaEXCEL
        {
            get { return sRutaDestino_EXCEL; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        private void CrearDirectorio(string Directorio)
        {
            if (!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        public void MsjFinalizado()
        {
            General.msjUser("Archivos de generados satisfactoriamente.");
        }

        public void AbrirDirectorioDeDocumentos()
        {
            if (General.msjConfirmar("¿ Desea abrir el directorio de archivos generados ?") == DialogResult.Yes)
            {
                General.AbrirDirectorio(sRutaDestino);
            }
        }

        public void AbrirDirectorioDeDocumentos_PDF()
        {
            if(General.msjConfirmar("¿ Desea abrir el directorio de archivos Pdf generados ?") == DialogResult.Yes)
            {
                General.AbrirDirectorio(sRutaDestino_PDF);
            }
        }

        public void AbrirDirectorioDeDocumentos_Excel()
        {
            if(General.msjConfirmar("¿ Desea abrir el directorio de archivos Excel generados ?") == DialogResult.Yes)
            {
                General.AbrirDirectorio(sRutaDestino_EXCEL);
            }
        }

        public bool GenerarDocumentos( string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion )
        {
            bool bRegresa = false;

            bRegresa = GenerarDocumentos(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Descripcion, DtIFacturacion.Vta_Impresion_Personalizada_Remision);

            return bRegresa; 
        }

        public bool GenerarDocumentos(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion, string FormatoDeImpresion)
        {
            bool bRegresa = false;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();

            Descripcion = FormatoCampos.Formatear_Nombre(Descripcion);
            if (bGenerar_PDF)
            {
                bRegresa = GenerarDocumentos_PDF(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Descripcion, FormatoDeImpresion);
            }


            if (bGenerar_EXCEL)
            {
                bRegresa = GenerarDocumentos_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Descripcion);
            }

            return bRegresa;
        }

        private bool GenerarDocumentos_PDF(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion, string FormatoDeImpresion )
        {
            bool bRegresa = false;
            clsLeer leer_Informacion = new clsLeer();
            string sNombre = "";
            string sFile = "";
            string replaceWith = "";

            sRutaDestino_PDF = sRutaDestino + @"\PDF\";
            CrearDirectorio(sRutaDestino_PDF);

            DatosCliente.Funcion = "Imprimir()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            clsReporteador Reporteador;  // = new clsReporteador(Reporte, DatosTerminal);
            // byte[] btReporte = null;

            sNombre = string.Format("RM_{0}{1}_{2}.pdf", bRemisionesManuales ? "M" : "", FolioRemision, Descripcion);
            sNombre = sNombre.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

            sFile = Path.Combine(sRutaDestino_PDF, sNombre);

            myRpt.RutaReporte = DtGeneral.RutaReportes;
            myRpt.NombreReporte = FormatoDeImpresion;

            myRpt.Add("@IdEmpresa", IdEmpresa);
            myRpt.Add("@IdEstado", IdEstado);
            myRpt.Add("@IdFarmaciaGenera", IdFarmaciaGenera);
            myRpt.Add("@FolioRemision", FolioRemision);


            Reporteador = new clsReporteador(myRpt, DatosCliente);
            Reporteador.ImpresionViaWeb = General.ImpresionViaWeb;
            Reporteador.Url = General.Url;
            Reporteador.MostrarInterface = false;
            Reporteador.MostrarMensaje_ReporteSinDatos = false;

            bRegresa = Reporteador.ExportarReporte(sFile, FormatosExportacion.PortableDocFormat, true);

            if (!bRegresa)
            {
                //General.msjError("Ocurrió un error al cargar el reporte.");
            }

            bRegresa = File.Exists(sFile); 

            return bRegresa;
        }

        //private bool GenerarDocumentos_FACTURAS(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioFactura)
        //{
        //    bool bRegresa = false;
        //    return bRegresa; 
        //}

        private bool GenerarDocumentos_Excel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion)
        {
            bool bRegresa = false;

            bRegresa = GenerarInformacion_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Descripcion); 

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Publicos

        private void DescargarPlantillaExcel()
        {
            //bExistePlantilla_Excel = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "FACT_REMISIONES_VALIDAR.xlsx", DatosCliente);
            bExistePlantilla_Excel = true;
        }

        private bool GenerarInformacion_Excel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion)
        {
            bool bRegresa = false; 

            string sSql = string.Format("Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel " +
                "  @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmaciaGenera = '{2}', @FolioRemision = '{3}' ",
            IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision);

            leerRemision_Encabezado = new clsLeer();
            leerRemision_Detalles = new clsLeer(); 

            if (!myLeer.Exec(sSql))
            {
                Error.GrabarError(myLeer, "PDFFolio_Validating");
                General.msjError("Ocurrió un error al obtener la información del Folio de Remision."); 
            }
            else
            {
                //dtsRemision_Encabezado.Tables.Add(myLeer.DataSetClase.Tables[0].Copy());
                myLeer.RenombrarTabla(1, sTabla_01_Encabezado);
                myLeer.RenombrarTabla(2, sTabla_02_Detalle);

                leerRemision_Encabezado.DataTableClase = myLeer.Tabla(sTabla_01_Encabezado);
                leerRemision_Detalles.DataTableClase = myLeer.Tabla(sTabla_02_Detalle); 

                if (myLeer.Leer())
                {
                    GenerarExcel(IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Descripcion);
                }
            }

            return bRegresa; 
        }

        private void GenerarExcel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string FolioRemision, string Descripcion)
        {
            clsOffice Excel = new clsOffice();
            clsGenerarExcel generarExcel = new clsGenerarExcel(); 
            string sNombre = "";
            string sNombreHoja = "Detalles remisión"; 

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8; 
            string sFechaImpresion = "", sDetalle = "";

            sRutaDestino_EXCEL = sRutaDestino + @"\EXCEL\";

            myLeer.DataSetClase = dtsRemision_Encabezado;
            CrearDirectorio(sRutaDestino_EXCEL);
            sNombre = string.Format("RM_{0}_{1}.xlsx", FolioRemision, Descripcion);

            generarExcel.RutaArchivo = sRutaDestino_EXCEL;
            generarExcel.NombreArchivo = sNombre;

            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(leerRemision_Detalles.DataTableClase, "Detalles remisión", 10, 2);
            //    wb.SaveAs(sRutaDestino_EXCEL + sNombre);
            //}

            if (generarExcel.CrearArchivo())
            {
                iRenglon = 7 + 2 + leerRemision_Encabezado.DataTableClase.Columns.Count; 


                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 14, string.Format("Fecha de Impresión: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);

                generarExcel.LlenarDetalleVertical(sNombreHoja, 7, iColBase, 12, 10, leerRemision_Encabezado.DataSetClase);


                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leerRemision_Detalles.DataTableClase, sNombreHoja);
                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leerRemision_Detalles.DataSetClase); 
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                //if (generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase + 1).Width > 150)
                //{
                //    generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase + 1).Width = 150;
                //}

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(2, 2).AdjustToContents();
                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(2,2).AdjustToContents(7, iRenglon, 1, 75);

                generarExcel.CerraArchivo();
            }
        }
    }
}
