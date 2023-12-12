using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Implementacion de hilos 
using System.Threading;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Lotes;
using DllFarmaciaSoft.Procesos;
using DllFarmaciaSoft.Ayudas;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_OperacionDelDia : FrmBaseExt
    {
        //clsDatosConexion datosDeConexion;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leer2;
        clsConsultas consultas;
        clsAyudas ayuda;
        //clsGrid grid;
        clsListView lst; 

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsLeer leerExportarExcel;

        FrmHelpBeneficiarios helpBeneficiarios;

        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb;
        //Thread _workerThread;

        //bool bEjecutando = false;
        bool bSeEncontroInformacion = false;
        //bool bSeEjecuto = false; 

        string sEmpresa = DtGeneral.EmpresaConectada;
        string sEstado = DtGeneral.EstadoConectado;
        string sFarmacia = DtGeneral.FarmaciaConectada;
        string sIdPublicoGral = GnFarmacia.PublicoGral;

        public FrmRptOP_OperacionDelDia()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_OperacionDelDia");
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite; 
            
            conexionWeb = new wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            consultas = new clsConsultas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.Modulo, this.Name, DtGeneral.Version);
            Error = new clsGrabarError(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            ////lst = new clsListView(lstResultado); 
        }

        #region Form 
        private void FrmRptOP_OperacionDelDia_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
        #endregion Form

        #region Botones 
        private void LimpiarPantalla()
        {
            Fg.IniciaControles(this, true);
            IniciaToolBar(false, false);
            IniciaFrames(true);
            rdoDispensacion.Checked = false;
            rdoVales.Checked = false;

            dtpFechaSistema.Value = GnFarmacia.FechaOperacionSistema;
            dtpFechaSistema.Enabled = false; 
            ////lst.LimpiarItems();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarPantalla(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            ////if (ValidaDatos())
            ////{
            ////    if (rdoDispensacion.Checked || rdoVales.Checked)
            ////    {
            ////        CargarDatos_Salidas();
            ////    }
            ////    else
            ////    {
            ////        General.msjAviso("Seleccione el tipo de Reporte....");
            ////    }
            ////}
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (rdoDispensacion.Checked)
            {
                ImprimirCorteParcialDetallado(); 
            }

            if (rdoVales.Checked)
            {
                ImprimirCorteParcialDetallado_Vales(); 
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
        }
        #endregion Botones 

        #region Funciones y Procedimientos Privados
        private void IniciaToolBar(bool Ejecutar, bool Exportar)
        {
            btnEjecutar.Enabled = Ejecutar;
            btnExportarExcel.Enabled = Exportar;
        }

        private void IniciaFrames(bool Valor)
        {
            FrameFechas.Enabled = Valor;
            FrameTipoDeReporte.Enabled = Valor;
        }
        #endregion Funciones y Procedimientos Privados

        #region Impresion 
        private void ImprimirCorteParcialDetallado_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 0, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ImprimirCorteParcialDetallado_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel del detallado de dispensación.");
            }
            else
            {
                ImprimirCorteParcialDetallado_ExcelExportar();
            }
        }

        private void ImprimirCorteParcialDetallado_ExcelExportar()
        {
            //// int iRenglon = 8; 
            bool bRegresa = false;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios.xlsx";

            this.Cursor = Cursors.WaitCursor;

            string sNombreHoja = "Hoja1";
            string sConcepto = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));

            int iRow = 2;
            int iCol = 2;
            int iColsEncabezado = 10;
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel;

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            //excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 14, sConcepto);
                iRow++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow, iCol, iColsEncabezado, 11, string.Format("Fecha impreción: {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);

                iRow += 2;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRow, iCol, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }


            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios.xlsx", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = true;

            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        xpExcel.GeneraExcel();

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //        xpExcel.Agregar(DtGeneral.NombrePersonal, 4, 2);
            //        xpExcel.Agregar(string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
            //        xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

            //        while (leer.Leer())
            //        {
            //            string sStatus = "Activo";
            //            if (leer.Campo("Status") == "C")
            //            {
            //                sStatus = "Cancelado";
            //            }

            //            xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, 2);
            //            xpExcel.Agregar(leer.Campo("Folio"), iRow, 3);
            //            //xpExcel.Agregar(leer.Campo("Folio"), iRow, 4); 
            //            xpExcel.Agregar(leer.Campo("NumReceta"), iRow, 5);
            //            xpExcel.Agregar(leer.Campo("FechaReceta"), iRow, 6);
            //            xpExcel.Agregar(leer.Campo("FolioReferencia"), iRow, 7);
            //            xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, 8);
            //            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, 9);
            //            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, 10);
            //            xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, 11);
            //            xpExcel.Agregar(leer.Campo("Cantidad"), iRow, 12);
            //            xpExcel.Agregar(leer.Campo("PrecioLicitacion"), iRow, 13);
            //            xpExcel.Agregar(leer.Campo("Importe"), iRow, 14);
            //            xpExcel.Agregar(sStatus, iRow, 15);

            //            iCol = 2;
            //            xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("Folio"), iRow, iCol++);
            //            iCol++;
            //            //xpExcel.Agregar(myLeer.Campo("Folio"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("Medico"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("NumReceta"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("FechaReceta"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("FolioReferencia"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("Cantidad"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("PrecioLicitacion"), iRow, iCol++);
            //            xpExcel.Agregar(leer.Campo("Importe"), iRow, iCol++);
            //            xpExcel.Agregar(sStatus, iRow, iCol++);

            //            iRow++;
            //        }

            //        //// Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}

            this.Cursor = Cursors.Default;
        }

        private void ImprimirCorteParcialDetallado()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 0);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Excel();
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios_Vales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 0, DtGeneral.IdPersonal);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ImprimirCorteParcialDetallado_Vales_Excel()");
                General.msjError("Ocurrió un error al generar el reporte excel de vales generados.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_ExcelExportar();
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        {            //// int iRenglon = 8; 
            //bool bRegresa = false;
            //int iRow = 10;
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx";

            this.Cursor = Cursors.WaitCursor;

            string sNombreHoja = "Hoja1";
            string sConcepto = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));

            int iRow = 2;
            int iCol = 2;
            int iColsEncabezado = 11;
            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel;

            excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            //excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;

            if (excel.PrepararPlantilla())
            {
                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 14, sConcepto);
                iRow++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 11, string.Format("Reporte de emisión de vales del día : {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow, iCol, iColsEncabezado, 11, string.Format("Fecha de impresión: {0} : {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);

                iRow += 2;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                excel.InsertarTabla(sNombreHoja, iRow, iCol, leer.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }
            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = true;

            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        xpExcel.GeneraExcel();

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //        xpExcel.Agregar(DtGeneral.NombrePersonal, 4, 2);
            //        xpExcel.Agregar(string.Format("Reporte de emisión de vales del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
            //        xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

            //        while (leer.Leer())
            //        {
            //            string sStatus = "Activo";
            //            if (leer.Campo("Status") == "C")
            //            {
            //                sStatus = "Cancelado";
            //            }

            //            xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, 2);
            //            xpExcel.Agregar(leer.Campo("FolioVenta"), iRow, 3);
            //            xpExcel.Agregar(leer.Campo("Folio"), iRow, 4);
            //            xpExcel.Agregar(leer.Campo("NumReceta"), iRow, 5);
            //            xpExcel.Agregar(leer.Campo("FechaReceta"), iRow, 6);
            //            xpExcel.Agregar(leer.Campo("FolioReferencia"), iRow, 7);
            //            xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, 8);
            //            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, 9);
            //            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, 10);
            //            xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, 11);
            //            xpExcel.Agregar(leer.Campo("Cantidad"), iRow, 12);
            //            xpExcel.Agregar(leer.Campo("PrecioLicitacion"), iRow, 13);
            //            xpExcel.Agregar(leer.Campo("Importe"), iRow, 14);
            //            xpExcel.Agregar(sStatus, iRow, 15);

            //            iRow++;
            //        }

            //        //// Finalizar el Proceso 
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}

            this.Cursor = Cursors.Default;
        }

        private void ImprimirCorteParcialDetallado_Vales()
        {
            DatosCliente.Funcion = "ImprimirCorteParcialDetallado_Vales()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_Admon_Validacion_CortesDiarios_Vales.rpt";

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);

            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@EsReporteGeneral", 0);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_Excel();
            }
        }
        #endregion Impresion 
    }
}
