using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using SC_SolutionsSystem.ExportarDatos;

namespace DllFarmaciaSoft.ReportesOperacion
{
    public partial class FrmRptOP_EntradasConsignacion : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
        clsLeer leer;
        clsLeer leer2;

        clsDatosCliente DatosCliente;

        clsListView lst;
        clsExportarExcelPlantilla xpExcel;

        public FrmRptOP_EntradasConsignacion()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            leer2 = new clsLeer(ref cnn);
            lst = new clsListView(lstResultado);

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, "");
        }

        private void FrmRptOP_EntradasConsignacion_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnExportarExcel.Enabled = false;
            Fg.IniciaControles();
            rdoTodos.Checked = true;
            rdoEntradas.Checked = false;
            rdoDevoluciones.Checked = false;
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnExportarExcel.Enabled = false;

            if (rdoEntradas.Checked || rdoDevoluciones.Checked)
            {
                CargarDatos();
            }
            else
            {
                General.msjAviso("Seleccione el tipo de Reporte....");
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }
        #endregion Botones

        #region Funciones y procedimientos
        private void CargarDatos()
        {
            int TipoClave = 0, TipoReporte = 1;
            lst.Limpiar();

            if(rdoLicitado.Checked)
            {
                TipoClave = 1;
            }
            else if (rdoNoLicitado.Checked)
            {
                TipoClave = 2;
            }

            if (rdoDevoluciones.Checked)
            {
                TipoReporte = 2;
            }

            string sSql = string.Format("Exec spp_Rpt_OP_EntradasConsignacion \n" +
                "\t@IdEstado = '{0}', @IdFarmacia = '{1}', @FechaInicial = '{2}', @FechaFinal = '{3}', @TipoClave = '{4}', @TipoReporte = '{5}' \n",
                DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"),
                TipoClave, TipoReporte);

            if (!leer2.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarDatos()");
                General.msjError("Ocurrió un error al consultar los datos..");
            }
            else
            {
                if (leer2.Leer())
                {
                    btnExportarExcel.Enabled = true;
                    leer2.RenombrarTabla(1, "Concentrado");
                    leer2.RenombrarTabla(2, "Detallado");

                    leer.DataTableClase = leer2.Tabla("Concentrado");
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    lst.AnchoColumna(3, 150);
                    lst.AnchoColumna(4, 440);
                    lst.AnchoColumna(5, 75);
                }
                else
                {
                    General.msjAviso("No se encontró información bajo los criterios especificados...");

                }
            }


        }

        private void ExportarExcel()
        {
            String sConcepto = "CONCENTRADO";
            string sNombreHoja = "CONCENTRADO";
            int iRow = 2;
            int iCol = 2;
            int iColsEncabezado = 6;
            clsLeer LeerExcel = new clsLeer();

            DllFarmaciaSoft.ExportarExcel.clsGenerarExcel excel = new DllFarmaciaSoft.ExportarExcel.clsGenerarExcel();
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
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow, iCol, iColsEncabezado++, 11, string.Format("Fecha generación: {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);

                iRow += 2;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                LeerExcel.DataTableClase = leer2.Tabla("Concentrado");
                excel.InsertarTabla(sNombreHoja, iRow, iCol, LeerExcel.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                //excel.CerraArchivo();

                sConcepto = sNombreHoja = "DETALLADO";
                iRow = 2;

                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 16, DtGeneral.FarmaciaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow++, iCol, iColsEncabezado, 14, sConcepto);
                iRow++;
                excel.EscribirCeldaEncabezado(sNombreHoja, iRow, iCol, iColsEncabezado, 11, string.Format("Fecha generación: {0} ", General.FechaSistemaObtener()), ClosedXML.Excel.XLAlignmentHorizontalValues.Left);

                iRow += 2;
                //excel.LlenarDetalleHorizontal(sNombreHoja, iRenglon, iColBase, leer.DataSetClase); 
                LeerExcel.DataTableClase = leer2.Tabla("Detallado");
                excel.InsertarTabla(sNombreHoja, iRow, iCol, LeerExcel.DataSetClase);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

                excel.CerraArchivo();

                excel.AbrirDocumentoGenerado(true);
            }

            //string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_OP_EntradasConsignacion.xls";
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_OP_EntradasConsignacion.xls", DatosCliente);

            //if (!bRegresa)
            //{
            //    this.Cursor = Cursors.Default;
            //    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            //}
            //else
            //{
            //    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            //    xpExcel.AgregarMarcaDeTiempo = false;

            //    if (xpExcel.PrepararPlantilla())
            //    {
            //        xpExcel.GeneraExcel(1);

            //        if (rdoDevoluciones.Checked)
            //        {
            //            sConsepto = "devoluciones";
            //        }

            //        sConsepto = "Reporte de " + sConsepto + " de consignación";

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //        xpExcel.Agregar(sConsepto, 4, 2);

            //        xpExcel.Agregar(General.FechaSistemaObtener(), 6, 3);

            //        leer.DataTableClase = leer2.Tabla("Concentrado");
            //        leer.RegistroActual = 1;

            //        for (int iRow = 10; leer.Leer(); iRow++)
            //        {
            //            int Col = 2;
            //            xpExcel.Agregar(leer.Campo("Fecha"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Folio Entrada"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Referencia"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Observaciones"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Piezas"), iRow, Col++);
            //        }

            //        xpExcel.CerrarDocumento();

            //        xpExcel.GeneraExcel(2);


            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //        xpExcel.Agregar(sConsepto, 4, 2);

            //        xpExcel.Agregar(General.FechaSistemaObtener(), 6, 3);

            //        leer.DataTableClase = leer2.Tabla("Detallado");
            //        leer.RegistroActual = 1;

            //        for (int iRow = 9; leer.Leer(); iRow++)
            //        {
            //            int Col = 2;
            //            xpExcel.Agregar(leer.Campo("Fecha"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Folio Entrada"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Referencia"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Observaciones"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Descripción"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Clave SSA"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Descripción Sal"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Lote"), iRow, Col++);
            //            xpExcel.Agregar(leer.Campo("Cantidad"), iRow, Col++);
            //        }


            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}
        }
        #endregion Funciones y procedimientos

        private void rdoEntradas_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            btnExportarExcel.Enabled = false;
        }

        private void rdoDevoluciones_CheckedChanged(object sender, EventArgs e)
        {
            lst.Limpiar();
            btnExportarExcel.Enabled = false;
        }
    }
}
