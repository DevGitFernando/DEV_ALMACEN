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
    class clsFacturas_GenerarDocumentos
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
        DataSet dtsRemision_Encabezado;
        DataSet dtsRemision_Detalles;


        clsLeer leerRemision_Encabezado;
        clsLeer leerRemision_Detalles;
        string sTabla_01_Encabezado = "Encabezado";
        string sTabla_02_Detalle = "Detalles";

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\FACT_REMISIONES_VALIDAR.xlsx";
        bool bExistePlantilla_Excel = false; 

        bool bGenerar_PDF = false;
        bool bGenerar_EXCEL = false;
        bool bGenerar_EXCEL_Contrarecibo = false;
        bool bGenerarDirectorio_Farmacia = false;

        string sFormato = "#######################0.#0";
        #endregion Declaracion de Variables

        #region Constructor de Clase
        public clsFacturas_GenerarDocumentos()
        {
            dtMarcaTiempo = General.FechaSistema;
            sMarcaTiempo = string.Format("", dtMarcaTiempo.Year, dtMarcaTiempo.Month, dtMarcaTiempo.Day);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, "clsFacturas_GenerarDocumentos", "");

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

            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, "Facturacion.GenerarRemisiones.clsFacturas_GenerarDocumentos");

            DescargarPlantillaExcel(); 
        }

        ~clsFacturas_GenerarDocumentos()
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
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        private void CrearDirectorio(string Directorio)
        {
            Directorio = toUTF8(Directorio); 

            if (!Directory.Exists(Directorio))
            {
                Directory.CreateDirectory(Directorio);
            }
        }

        private string toUTF8(string stext)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            if (stext == null)
            {
                stext = "";
            }

            byte[] bytes = encoding.GetBytes(stext);
            return encoding.GetString(bytes);
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

        public bool GenerarDocumentos(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string Serie, string Folio, int Identificador, string NombreDirectorio, string NombreArchivo)
        {
            bool bRegresa = false;

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerInf = new clsLeer();

            NombreArchivo = FormatoCampos.Formatear_Nombre(NombreArchivo);
            if (bGenerar_PDF)
            {
                bRegresa = GenerarDocumentos_PDF(IdEmpresa, IdEstado, IdFarmaciaGenera, Serie, Folio, Identificador, NombreDirectorio, NombreArchivo);
            }


            if (bGenerar_EXCEL)
            {
                bRegresa = GenerarDocumentos_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, Serie, Folio, "");
            }

            return bRegresa;
        }

        private bool GenerarDocumentos_PDF(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string Serie, string Folio, int Identificador, string NombreDirectorio, string NombreArchivo)
        {
            bool bRegresa = false;
            clsLeer leer_Informacion = new clsLeer();
            string sRutaDestino_PDF = sRutaDestino + @"\" + NombreDirectorio;

            CrearDirectorio(sRutaDestino_PDF);


            bRegresa = ImprimirDocumento_Timbrado(sRutaDestino_PDF, NombreArchivo, Identificador); 


            return bRegresa;
        }

        private bool ImprimirDocumento_Timbrado(string NombreDirectorio, string NombreArchivo, int Identificador)
        {
            bool bRegresa = false;
            bool bVisualizar = true;
            string sImpresion = " spp_FACT_CFDI_GetListaComprobantes ";
            string sSql = string.Format("Exec spp_FACT_CFDI_GetListaComprobantes @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Identificador = {3} ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, Identificador);

            string sMensaje = "¿ Desea ver la vista previa del documento electrónico ? ";
            string sFileName = "";
            string sXML = "";
            string sPDF = "";
            string sEmail = "";


            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "Imprimir()");
            }
            else
            {
                if (!leer.Leer())
                {
                }
                else
                {
                    sFileName = leer.Campo("NombreFiles");
                    sXML = leer.Campo("uf_Xml_Timbrado");
                    sPDF = leer.Campo("uf_xml_Impresion");
                    sEmail = leer.Campo("EmailCliente");
                    sFileName = NombreArchivo; 

                    bRegresa = true;
                    DtIFacturacion.InvocaVisor = null;
                    DtIFacturacion.EsTimbradoMasivo = true;
                    DtIFacturacion.DirectorioAlternoArchivosGenerados = NombreDirectorio;
                    DtIFacturacion.GenerarImpresionWebBrowser(false, sFileName, DtIFacturacion.GetExtras(leer.DataSetClase), sPDF, sXML, bVisualizar, null, true);
                }
            }

            return bRegresa;
        }

        private bool GenerarDocumentos_Excel(string IdEmpresa, string IdEstado, string IdFarmaciaGenera, string Serie, string Folio, string Descripcion)
        {
            bool bRegresa = false;

            bRegresa = GenerarInformacion_Excel(IdEmpresa, IdEstado, IdFarmaciaGenera, Folio, Descripcion); 

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

            string sRutaDestino_EXCEL = sRutaDestino + @"\EXCEL\";


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
