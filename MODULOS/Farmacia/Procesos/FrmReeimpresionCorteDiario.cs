using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos; 
using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Procesos
{
    public partial class FrmReeimpresionCorteDiario : FrmBaseExt
    {
        clsConexionSQL ConexionLocal = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;

        // Manejo de reportes  
        clsDatosCliente DatosCliente;
        wsFarmacia.wsCnnCliente conexionWeb; // = new Farmacia.wsFarmacia.wsCnnCliente();
        //clsExportarExcelPlantilla xpExcel; 

        public FrmReeimpresionCorteDiario()
        {
            InitializeComponent();

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
            conexionWeb = new Farmacia.wsFarmacia.wsCnnCliente();
            conexionWeb.Url = General.Url;

            leer = new clsLeer(ref ConexionLocal);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(GnFarmacia.DatosApp, this.Name);
            dtpFechaSistema.MaxDate = GnFarmacia.FechaOperacionSistema;
        }

        #region Impresion
        private void ImprimirCorteDiario()
        {
            DatosCliente.Funcion = "ImprimirCorteDiario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CorteDiario.rpt";  // Tira de Auditoria 

            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FechaDeSistema", General.FechaYMD(dtpFechaSistema.Value, "-"));
            myRpt.Add("@IdPersonal", "");

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente); 

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if (!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }

        private void ImprimirTiraDeAuditoria()
        {
            DatosCliente.Funcion = "ImprimirCorteDiario()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;
            bool bRegresa = false; 

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_CorteTiraDeAutoria.rpt";  // Tira de Auditoria 

            myRpt.Add("IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("FechaDeSistemaCorte", General.FechaYMD(dtpFechaSistema.Value, "-"));

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            ////DataSet InfoWeb = myRpt.ObtenerInformacionWeb();
            ////DataSet datosC = DatosCliente.DatosCliente();

            ////btReporte = conexionWeb.Reporte(InfoWeb, datosC);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
        #endregion Impresion 

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (validaFechaCorte())
            {
                btnImprimir.Enabled = false;

                if (rdoCorteDiario.Checked)
                {
                    ImprimirCorteDiario();
                }
                
                if(rdoTiraDeAuditoria.Checked)
                {
                    ImprimirTiraDeAuditoria();
                }

                if (rdoDetDisp.Checked)
                {
                    ImprimirCorteParcialDetallado();
                    ImprimirCorteParcialDetallado_Vales(); 
                }

                btnImprimir.Enabled = true;
            }
        }

        private bool validaFechaCorte()
        {
            bool bRegresa = true;

            return bRegresa;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = true;
            rdoCorteDiario.Checked = true;

            if (!GnFarmacia.GeneraReporteDispensacionPersonal)
            {
                rdoDetDisp.Checked = false;
                rdoDetDisp.Enabled = false;
            }
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
            myRpt.Add("@EsReporteGeneral", 1);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            else
            {
                ImprimirCorteParcialDetallado_Excel();
            }
        }

        private void ImprimirCorteParcialDetallado_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 1, DtGeneral.IdPersonal);

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
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            //clsLeer leer = new clsLeer();

            //leer.DataSetClase = leer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, "Todos los dispensadores");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void ImprimirCorteParcialDetallado_ExcelExportar()
        //{
        //    //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    int iCol = 0; 
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Todos los dispensadores", 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (leer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (leer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                } 

        //                iCol = 2;
        //                xpExcel.Agregar(DtGeneral.NombrePersonal, iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Folio"), iRow, iCol++);
        //                iCol++; 
        //                //xpExcel.Agregar(leer.Campo("Folio"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Medico"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("NumReceta"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("FechaReceta"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("FolioReferencia"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Cantidad"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("PrecioLicitacion"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Importe"), iRow, iCol++);
        //                xpExcel.Agregar(sStatus, iRow, iCol++);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

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
            myRpt.Add("@EsReporteGeneral", 1);
            myRpt.Add("@IdPersonal", DtGeneral.IdPersonal);

            // myRpt.Add("Folio", "II" + txtFolio.Text); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if (!bRegresa)
            {
                if(!DtGeneral.CanceladoPorUsuario)
                {
                    General.msjError("Ocurrió un error al cargar el reporte.");
                }
            }
            else
            {
                ImprimirCorteParcialDetallado_Vales_Excel();
            }
        }

        private void ImprimirCorteParcialDetallado_Vales_Excel()
        {
            string sSql = string.Format("Exec spp_Rpt_Administrativos_CortesDiarios_Vales " +
                " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaDeSistema = '{3}', @EsReporteGeneral = '{4}', @IdPersonal = '{5}' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                General.FechaYMD(dtpFechaSistema.Value, "-"), 1, DtGeneral.IdPersonal);

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
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            //string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            string sNombre = string.Format("Reporte de dispensación del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            //clsLeer leer = new clsLeer();

            //leer.DataSetClase = leer.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, "Todos los dispensadores");
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                generarExcel.CerraArchivo();

                generarExcel.AbrirDocumentoGenerado(true);
            }

        }

        //private void ImprimirCorteParcialDetallado_Vales_ExcelExportar()
        //{            //// int iRenglon = 8; 
        //    bool bRegresa = false;
        //    int iRow = 10;
        //    int iCol = 0; 
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx";

        //    this.Cursor = Cursors.WaitCursor;
        //    bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Admon_Validacion_CortesDiarios_Vales.xlsx", DatosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;

        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            xpExcel.GeneraExcel();

        //            xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
        //            xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
        //            xpExcel.Agregar("Todos los dispensadores", 4, 2);
        //            xpExcel.Agregar(string.Format("Reporte de emisión de vales del día {0}", General.FechaYMD(dtpFechaSistema.Value, "-")), 5, 2);
        //            xpExcel.Agregar(string.Format("Fecha de impresión : {0} ", DateTime.Now.ToString()), 7, 2);

        //            while (leer.Leer())
        //            {
        //                string sStatus = "Activo";
        //                if (leer.Campo("Status") == "C")
        //                {
        //                    sStatus = "Cancelado";
        //                }

        //                iCol = 2;
        //                xpExcel.Agregar(leer.Campo("NombrePersonal"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("FolioVenta"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Folio"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Medico"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("NumReceta"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("FechaReceta"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("FolioReferencia"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Beneficiario"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("DescripcionCortaClave"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Cantidad"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("PrecioLicitacion"), iRow, iCol++);
        //                xpExcel.Agregar(leer.Campo("Importe"), iRow, iCol++);
        //                xpExcel.Agregar(sStatus, iRow, iCol++);

        //                iRow++;
        //            }

        //            //// Finalizar el Proceso 
        //            xpExcel.CerrarDocumento();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        private void FrmReeimpresionCorteDiario_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(null, null);
        }
    }
}
