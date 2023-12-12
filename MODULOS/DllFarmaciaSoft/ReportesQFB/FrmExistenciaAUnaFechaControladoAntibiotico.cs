using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comun;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.Reportes;

//using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.ReportesQFB
{
    public partial class FrmExistenciaAUnaFechaControladoAntibiotico : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrid Grid;
        clsDatosCliente DatosCliente;
        ///clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();
        DataSet dtsResultados = new DataSet();

        public FrmExistenciaAUnaFechaControladoAntibiotico()
        {
            InitializeComponent();

            leer = new clsLeer(ref cnn);
            Grid = new clsGrid(ref grdMovtos, this);
            Grid.AjustarAnchoColumnasAutomatico = true;

            DatosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "");
        }

        private void FrmExistenciaAUnaFechaControladoAntibiotico_Load(object sender, EventArgs e)
        {
            btnNuevo_Click(this, null);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;

            int iEsDetallado = rdoDetallado.Checked ? 1 : 0;
            string sSql = "";
            int iTipoProducto = rdoGeneral.Checked ? 0 : -1;


            if(rdoControlados.Checked)
            {
                iTipoProducto = 1;
            }

            if (rdoAntibioticos.Checked)
            {
                iTipoProducto = 2;
            }

            if(rdoLibre.Checked)
            {
                iTipoProducto = 3;
            }
            

            sSql = string.Format("Exec spp_Inventario_A_Una_Fecha_x_Farmacia \n" +
                    "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @FechaRevision = '{3}', @TipoProducto = '{4}', @EsDetallado = '{5}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, General.FechaYMD(dtpFecha.Value), iTipoProducto, iEsDetallado 
                    );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "btnEjecutar_Click()");
                General.msjError("Ocurrió un error al obtener la información de existencias.");
            }
            else 
            {
                Grid.Limpiar(false);
                if (leer.Leer())
                {
                    Grid.LlenarGrid(leer.DataSetClase);

                    dtsResultados = new DataSet();
                    dtsResultados.Tables.Add(leer.Tabla(2)); 

                    btnExportarExcel.Enabled = true;
                    btnImprimir.Enabled = true;
                }
                else
                {
                    General.msjAviso("No existe información para mostrar con los parámetros seleccionados.");
                }
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            rdoAntibioticos.Checked = true;
            rdoConcentrado.Checked = true; 
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;
            dtpFecha.MaxDate = General.FechaSistemaObtener();
            Grid.Limpiar(false);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ////if (rdoConcentrado.Checked)
            ////{
            ////    GenerarExcel_Concentrado(); 
            ////}

            ////if (rdoDetallado.Checked)
            ////{
            ////    GenerarExcel_Detallado(); 
            ////}
            ///

            GenerarExcel(); 
        }

        private void GenerarExcel()
        {
            GenerarExcel_Concentrado(); 
        }

        private void GenerarExcel_Concentrado()
        {
            bool bRegresa = true;
            int iCol = 2;
            int iRow = 0; 
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos.xls";
            string sClave = "";
            int iExistencia = 0;

            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            string sEmpresa = DtGeneral.EmpresaConectadaNombre; 
            string sEstado = DtGeneral.FarmaciaConectadaNombre;
            //string sConceptoReporte = "Reporte Concentrado de Existencias";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            string sNombreDocumento = string.Format("ReporteDeExistenciasAntibioticos");
            string sNombreHoja = "ExistenciasConcentradas";
            string sConcepto = "REPORTE DE EXISTENCIAS CONCENTRADO";


            sNombreHoja = "ExistenciasAntibioticos";
            sConcepto = "REPORTE DE EXISTENCIA DE ANTIBIOTICOS A LA FECHA ";

            if (rdoControlados.Checked)
            {
                sNombreDocumento = string.Format("ReporteDeExistenciasControlados");
                sNombreHoja = "ExistenciasControlados";
                sConcepto = "REPORTE DE EXISTENCIA DE CONTROLADOS A LA FECHA ";
            }


            this.Cursor = Cursors.WaitCursor;
            //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos.xls", DatosCliente);

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                ////xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                ////xpExcel.AgregarMarcaDeTiempo = true;

                ////if (xpExcel.PrepararPlantilla())
                ////{
                ////    this.Cursor = Cursors.WaitCursor;
                ////    xpExcel.GeneraExcel();

                ////    string sEnc = "REPORTE DE EXISTENCIA DE ANTIBIOTICOS A LA FECHA ";

                ////    if (rdoControlados.Checked)
                ////    {
                ////        sEnc = "REPORTE DE EXISTENCIA DE CONTROLADOS A LA FECHA ";
                ////    }


                ////    leer.Leer();
                ////    sEnc = leer.Campo("Encabezado");  
                ////    xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
                ////    xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
                ////    xpExcel.Agregar(sEnc + " " + General.FechaYMD(dtpFecha.Value), 4, 2);
                ////    xpExcel.Agregar(leer.CampoFecha("FechaEmisionReporte").ToString(), 6, 3);

                ////    leer.RegistroActual = 1;
                ////    sClave = "";

                ////    iRow = 8; 
                ////    while(leer.Leer())
                ////    {
                ////        if (sClave != leer.Campo("ClaveSSA"))
                ////        {
                ////            iRow++; 
                ////            iCol = 2;
                ////            sClave = leer.Campo("ClaveSSA");
                ////            xpExcel.Agregar(sClave, iRow, 2);
                ////            xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, 3);
                ////            iExistencia = leer.CampoInt("Existencia_A_La_Fecha");
                ////            xpExcel.Agregar(iExistencia, iRow, 4);
                ////        }
                ////        else
                ////        {
                ////            iExistencia += leer.CampoInt("Existencia_A_La_Fecha");
                ////            xpExcel.Agregar(iExistencia, iRow, 4);
                ////        }
                ////    }

                ////}
                ////xpExcel.CerrarDocumento();

                ////if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                ////{
                ////    xpExcel.AbrirDocumentoGenerado();
                ////}
                ///

                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;

                if (excel.PrepararPlantilla(sNombreDocumento))
                {
                    excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
                    excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                    iRenglon = 8;
                    excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);

                    //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                    excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);


                    excel.CerraArchivo();

                    this.Cursor = Cursors.Default;

                    excel.AbrirDocumentoGenerado(true);

                }
            }
            this.Cursor = Cursors.Default;
        }

        private void GenerarExcel_Detallado()
        {
            ////bool bRegresa = false;
            ////int iCol = 2;
            ////string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos_Detallado.xls";

            ////this.Cursor = Cursors.WaitCursor;
            ////bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos_Detallado.xls", DatosCliente);

            ////if (!bRegresa)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            ////}
            ////else
            ////{
            ////    xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
            ////    xpExcel.AgregarMarcaDeTiempo = true;

            ////    if (xpExcel.PrepararPlantilla())
            ////    {
            ////        this.Cursor = Cursors.WaitCursor;
            ////        xpExcel.GeneraExcel();

            ////        string sEnc = "REPORTE DE EXISTENCIA DE ANTIBIOTICOS A LA FECHA ";

            ////        if (rdoControlados.Checked)
            ////        {
            ////            sEnc = "REPORTE DE EXISTENCIA DE CONTROLADO A LA FECHA ";
            ////        }

            ////        leer.Leer();
            ////        sEnc = leer.Campo("Encabezado");  
            ////        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            ////        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            ////        xpExcel.Agregar(sEnc + " " + General.FechaYMD(dtpFecha.Value), 4, 2);
            ////        xpExcel.Agregar(leer.CampoFecha("FechaEmisionReporte").ToString(), 6, 3);

            ////        leer.RegistroActual = 1;

            ////        for (int iRow = 9; leer.Leer(); iRow++)
            ////        {
            ////            iCol = 2;
            ////            xpExcel.Agregar(leer.Campo("ClaveSSA"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("DescripcionClave"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("CodigoEAN"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("Descripcion"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("IdSubFarmacia"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("ClaveLote"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("FechaCaducidad"), iRow, iCol++);
            ////            xpExcel.Agregar(leer.Campo("Existencia_A_La_Fecha"), iRow, iCol++);
            ////        }

            ////    }
            ////    xpExcel.CerrarDocumento();

            ////    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            ////    {
            ////        xpExcel.AbrirDocumentoGenerado();
            ////    }
            ////}
            ////this.Cursor = Cursors.Default;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            int iTipoProducto = 1;

            string sEnc = "REPORTE DE EXISTENCIA DE ANTIBIOTICOS A LA FECHA ";

            if (rdoControlados.Checked)
            {
                sEnc = "REPORTE DE EXISTENCIA DE CONTROLADO A LA FECHA ";
            }

            if (rdoAntibioticos.Checked)
            {
                iTipoProducto = 2;
            }

            DatosCliente.Funcion = "ImprimirCompra()";
            clsImprimir myRpt = new clsImprimir(General.DatosConexion);
            // byte[] btReporte = null;

            myRpt.RutaReporte = GnFarmacia.RutaReportes;
            myRpt.NombreReporte = "PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos";

            if (rdoDetallado.Checked)
            {
                myRpt.NombreReporte = "PtoVta_ExistenciaAUnaFechaDeControladosYAntibioticos_Detallado";
            }


            myRpt.Add("@IdEmpresa", DtGeneral.EmpresaConectada);
            myRpt.Add("@IdEstado", DtGeneral.EstadoConectado);
            myRpt.Add("@IdFarmacia", DtGeneral.FarmaciaConectada);
            myRpt.Add("@FechaRevision", General.FechaYMD(dtpFecha.Value));
            myRpt.Add("@TipoProducto", iTipoProducto); 

            bRegresa = DtGeneral.GenerarReporte(General.Url, General.ImpresionViaWeb, myRpt, DatosCliente);

            if(!bRegresa && !DtGeneral.CanceladoPorUsuario)
            {
                General.msjError("Ocurrió un error al cargar el reporte.");
            }
        }
    }
}
