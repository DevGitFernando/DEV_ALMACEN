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
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

using DllCompras;

namespace Compras_Cuentas_x_Pagar.Registros
{
    public partial class FrmEstadoDeCuenta : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer, leerExportarExcel;

        clsConsultas Consultas;
        clsAyudas Ayudas;
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente datosCliente;

        public FrmEstadoDeCuenta()
        {
            InitializeComponent();
            leer = new clsLeer(ref cnn);
            leerExportarExcel = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayudas = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            datosCliente = new clsDatosCliente(GnCompras.DatosApp, this.Name, "");
        }

        private void FrmEstadoDeCuenta_Load(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (GenerarInformacion())
                GenerarReporteExcel();
        }

        #region Eventos

        private void txtIdProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdProveedor.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Proveedores(txtIdProveedor.Text.Trim(), "txtIdProveedor_Validating()");
                if (leer.Leer())
                {
                    Cargarproveedor();
                }
                else
                {
                    txtIdProveedor.Text = "";
                    lblProveedor.Text = "";
                    txtIdProveedor.Focus();
                }
            }
        }

        private void txtIdProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Proveedores("txtIdProveedor_KeyDown()");
                if (leer.Leer())
                {
                    Cargarproveedor();
                }
            }
        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {
            lblProveedor.Text = "";
        }

        private void Cargarproveedor()
        {
            txtIdProveedor.Text = leer.Campo("IdProveedor");
            lblProveedor.Text = leer.Campo("Nombre");
            txtIdProveedor.Enabled = false;
        }

        private void txtIdEstado_Validating(object sender, CancelEventArgs e)
        {
            if (txtIdEstado.Text.Trim() != "")
            {
                leer.DataSetClase = Consultas.Estados(txtIdEstado.Text.Trim(), "txtIdEstado_Validating()");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                }
                else
                {
                    txtIdEstado.Text = "";
                    lblEstado.Text = "";
                }
            }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = Ayudas.Estados("txtIdEstado_KeyDown()");
                if (leer.Leer())
                {
                    txtIdEstado.Text = leer.Campo("IdEstado");
                    lblEstado.Text = leer.Campo("Nombre");
                    txtIdEstado.Focus();
                }
            }
        }

        private void txtIdEstado_TextChanged(object sender, EventArgs e)
        {
            lblEstado.Text = "";
        }


        #endregion Eventos


        private void Limpiar()
        {
            Fg.IniciaControles();
            rdoAmbos.Checked = true;
            rdoConSaldo.Checked = true;
            txtIdProveedor.Focus();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = "Estado de cuenta";
            string sNombreFile = "Estado de cuenta";




            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla(sNombreFile))
            {
                leer.DataTableClase = leerExportarExcel.Tabla(1);
                leer.RegistroActual = 1;

                iColsEncabezado = iColBase + leer.Columnas.Length - 1;

                iColBase = 2;
                iRenglon = 2;
                sNombreHoja = "Enc";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, sNombre);
                iRenglon++;
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 14, string.Format("Fecha de generación: {0} ", General.FechaSistema), XLAlignmentHorizontalValues.Left);
                iRenglon++;

                generarExcel.InsertarTabla(sNombreHoja, iRenglon, iColBase, leer.DataSetClase);
                generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 75);

                //generarExcel.ArchivoExcel.Worksheet(sNombreHoja).Columns(iColBase, iColBase).Width = 30;

                leer.DataTableClase = leerExportarExcel.Tabla(2);
                leer.RegistroActual = 1;
                iColsEncabezado = iColBase + leer.Columnas.Length - 1;

                iColBase = 2;
                iRenglon = 2;
                sNombreHoja = "Det";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                //generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, cboEdo.Text);
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

        //private void ExportarExcel()
        //{
        //    int iRow = 2, iCol = 2;
        //    string sNombreFile = "";
        //    string sRutaReportes = "";

        //    sRutaReportes = GnCompras.RutaReportes;
        //    DtGeneral.RutaReportes = sRutaReportes;

        //    sRutaReportes = Application.StartupPath + @"\\Plantillas\COM_OCEN_Rpt_Estado_De_Cuenta.xls";
        //    DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "COM_OCEN_Rpt_Estado_De_Cuenta.xls", datosCliente);

        //    xpExcel = new clsExportarExcelPlantilla(sRutaReportes);
        //    xpExcel.AgregarMarcaDeTiempo = false;

        //    if (xpExcel.PrepararPlantilla(sNombreFile))
        //    {
        //        xpExcel.GeneraExcel(1);

        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow = 5;
        //        xpExcel.Agregar("Fecha de reporte: " + DateTime.Now.ToShortDateString(), iRow, 2);

        //        // Se ponen los detalles
        //        leerExportarExcel.DataTableClase = leer.Tabla(1);
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 8;

        //        while (leerExportarExcel.Leer())
        //        {
        //            iCol = 2;
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, iCol++);
        //            //xpExcel.Agregar(leerExportarExcel.Campo("OrdenCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("TipoDeCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            //xpExcel.Agregar(leerExportarExcel.Campo("Factura"), iRow, iCol++);
        //            //xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, iCol++);
        //            //xpExcel.Agregar(leerExportarExcel.Campo("FechaColocacion"), iRow, iCol++);
        //            //xpExcel.Agregar(leerExportarExcel.Campo("FechaDocto"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Total"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Abonos"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Total") - leerExportarExcel.CampoDouble("Abonos"), iRow, iCol++);

        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        xpExcel.GeneraExcel(2);
        //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, iRow, 2);
        //        iRow = 5;
        //        xpExcel.Agregar("Fecha de reporte: " + DateTime.Now.ToShortDateString(), iRow, 2);

        //        // Se ponen los detalles
        //        leerExportarExcel.DataTableClase = leer.Tabla(2);
        //        leerExportarExcel.RegistroActual = 1;
        //        iRow = 8;

        //        while (leerExportarExcel.Leer())
        //        {
        //            iCol = 2;
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdEstado"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Estado"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("OrdenCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("TipoDeCompra"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("IdProveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Proveedor"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("Factura"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaRegistro"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaColocacion"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.Campo("FechaDocto"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Total"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Abonos"), iRow, iCol++);
        //            xpExcel.Agregar(leerExportarExcel.CampoDouble("Total") - leerExportarExcel.CampoDouble("Abonos"), iRow, iCol++);

        //            iRow++;
        //        }

        //        // Finalizar el Proceso 
        //        xpExcel.CerrarDocumento();

        //        if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //        {
        //            xpExcel.AbrirDocumentoGenerado();
        //        }
        //    }
        //}

        private bool GenerarInformacion()
        {
            bool bRegresa = true;

            int iTipoDeOrden = 2, iTipoDeSaldo = 2;

            iTipoDeOrden = rdoCentral.Checked ? 1:iTipoDeOrden;
            iTipoDeOrden = rdoRegional.Checked ? 0:iTipoDeOrden;

            iTipoDeSaldo = rdoLiquidado.Checked ? 0:iTipoDeSaldo;
            iTipoDeSaldo = rdoConSaldo.Checked ? 1:iTipoDeSaldo;

            string sSql = string.Format("Exec spp_Rpt_Estado_De_Cuenta @IdEmpresa = '{0}', @IdEstado = '{1}', @IdProveedor = '{2}', @TipoDeOrden = {3}, @TipoDeSaldo = '{4}'",
                DtGeneral.EmpresaConectada, txtIdEstado.Text.Trim(), txtIdProveedor.Text.Trim(), iTipoDeOrden, iTipoDeSaldo);

            if (!leerExportarExcel.Exec(sSql))
            {
                Error.GrabarError(leerExportarExcel, "GenerarInformacion()");
                General.msjError("Ocurrio un error al obtener la información.");
                bRegresa = false;
            }
            else
            {
                if (!leerExportarExcel.Leer())
                {
                    bRegresa = false;
                    General.msjAviso("No existe información.");
                }
            }

            return bRegresa;
        }
    }
}
