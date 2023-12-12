using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_MovimientosPorMes : FrmBaseExt
    {
        clsLeer leer;
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente DatosCliente;
        clsExportarExcelPlantilla xpExcel;


        public FrmRptOP_MovimientosPorMes()
        {
            InitializeComponent();
            Fg.IniciaControles();

            leer = new clsLeer(ref cnn);
            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmRptOP_MovimientosPorMes");
        }

        private void FrmRptOP_MovimientosPorMes_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            rdoTodos.Checked = true;
            dtpFecha.Value = General.FechaSistemaObtener();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            int iTipo = 0;
            if(rdoLicitado.Checked)
            {
                iTipo = 1;
            }

            if(rdoNoLicitado.Checked)
            {
                iTipo = 2;
            }


            string sSql = string.Format("Exec spp_Rpt_OP_MovimientosMensual '{0}', '{1}', '{2}', '{3}', {4}", 
                               DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, dtpFecha.Value.Year, dtpFecha.Value.Month, iTipo);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnExportarExcel_Click");
                General.msjError("Ocurrió un error al exportar los datos..");
            }
            else
            {
                if (!leer.Leer())
                {
                    General.msjAviso("No existe información para mostrar.");
                }
                else
                {
                    GenerarExcel();
                }
            }

        }


        private void GenerarExcel()
        {
            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_OP_MovimientosMensual.xls";
            this.Cursor = Cursors.WaitCursor;

            string sNombreHoja = "Hoja1";
            string sConcepto = "Reporte de movimientos mensual del año " + dtpFecha.Value.Year + " y el mes " + dtpFecha.Value.Month;
            
            int iRow = 2;
            int iCol = 2;
            int iColsEncabezado = 7;

            this.Cursor = Cursors.WaitCursor;
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

            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_OP_MovimientosMensual.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = true;
            //    leer.DataSetClase = dtsExistencias;

            //    this.Cursor = Cursors.Default;
            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        int iHoja = 1, iRenglon = 10;
            //        string sEmpresaNom = DtGeneral.EmpresaConectadaNombre;
            //        string sEstadoNom = DtGeneral.EstadoConectadoNombre;
            //        string sFarmaciaNom = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre + ", " + sEstadoNom;
            //        string sConcepto = "Reporte de movimientos mensual del año " + dtpFecha.Value.Year + " y el mes " + dtpFecha.Value.Month;
            //        string sFechaImpresion = "Fecha impreción: " + General.FechaSistemaFecha.ToString();

            //        xpExcel.GeneraExcel(iHoja);

            //        xpExcel.Agregar(sEmpresaNom, 2, 2);
            //        xpExcel.Agregar(sFarmaciaNom, 3, 2);
            //        xpExcel.Agregar(sConcepto, 4, 2);

            //        xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            //        xpExcel.Agregar(sFechaImpresion, 6, 2);

            //        leer.RegistroActual = 1;
            //        ClaveSSA, DescripcionCortaClave As Descripcion, Presentacion, ContenidoPaquete,
            //        II, IIC, EPC, EOC, EC,
            //        TT, TA, TE, SV, EAI, SAI,
            //        EE, SE
            //        while (leer.Leer())
            //        {
            //            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRenglon, 1);
            //            xpExcel.Agregar(leer.Campo("Descripcion"), iRenglon, 2);
            //            xpExcel.Agregar(leer.Campo("Presentacion"), iRenglon, 3);
            //            xpExcel.Agregar(leer.Campo("ContenidoPaquete"), iRenglon, 4);
            //            xpExcel.Agregar(leer.Campo("II"), iRenglon, 5);
            //            xpExcel.Agregar(leer.Campo("IIC"), iRenglon, 6);
            //            xpExcel.Agregar(leer.Campo("EPC"), iRenglon, 7);
            //            xpExcel.Agregar(leer.Campo("EOC"), iRenglon, 8);
            //            xpExcel.Agregar(leer.Campo("EC"), iRenglon, 9);
            //            xpExcel.Agregar(leer.Campo("TT"), iRenglon, 10);
            //            xpExcel.Agregar(leer.Campo("TA"), iRenglon, 11);
            //            xpExcel.Agregar(leer.Campo("TE"), iRenglon, 12);
            //            xpExcel.Agregar(leer.Campo("SV"), iRenglon, 13);
            //            xpExcel.Agregar(leer.Campo("EAI"), iRenglon, 14);
            //            xpExcel.Agregar(leer.Campo("SAI"), iRenglon, 15);
            //            xpExcel.Agregar(leer.Campo("EE"), iRenglon, 16);
            //            xpExcel.Agregar(leer.Campo("SE"), iRenglon, 17);
            //            xpExcel.Agregar(leer.Campo("TasaIva"), iRenglon, 18);
            //            xpExcel.Agregar(leer.Campo("SubTotalLote"), iRenglon, 19);
            //            xpExcel.Agregar(leer.Campo("ImpteIvaLote"), iRenglon, 20);
            //            iRenglon++;
            //        }

            //        Finalizar el Proceso
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }

            //    }
            //}
        }

    }
}
