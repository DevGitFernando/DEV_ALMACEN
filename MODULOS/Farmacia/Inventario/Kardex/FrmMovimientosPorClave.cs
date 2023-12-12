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
using SC_SolutionsSystem.FuncionesGrid;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;
using ClosedXML.Excel;

namespace Farmacia.Inventario
{


    public partial class FrmMovimientosPorClave : FrmBaseExt
    {
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsGrid Grid;
        clsLeer Leer;
        clsLeer leer;
        clsConsultas Consultas;
        clsAyudas Ayuda;

        string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\MOVIMIENTO_CLAVE.xls";
        //clsExportarExcelPlantilla xpExcel;
        clsDatosCliente DatosCliente; 

        private enum Cols
        {
            Ninguno = 0, Efecto = 1, Clave = 2, Descripcion = 3, Folios = 4, Piezas = 5
        }

        public FrmMovimientosPorClave()
        {
            InitializeComponent();

            cnn.TiempoDeEsperaConexion = TiempoDeEspera.SinLimite;
            cnn.TiempoDeEsperaEjecucion = TiempoDeEspera.SinLimite;

            DatosCliente = new clsDatosCliente(GnFarmacia.DatosApp, this.Name, ""); 
            Grid = new clsGrid(ref grdConcentradoPorClave, this);
            Grid.EstiloGrid(eModoGrid.ModoRow);
            Grid.AjustarAnchoColumnasAutomatico = true; 

            Leer = new clsLeer(ref cnn);
            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Ayuda = new clsAyudas(General.DatosConexion, GnFarmacia.DatosApp, this.Name, true);
            Inicializar();
        }

        # region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (lblPersonal.Text == "")
            {
                txtPersonal.Text = "";
            }

            if (lblDescripcion.Text == "")
            {
                txtClaveSSA.Text = "";
                CargarGrid();
            }
            else
            {
                if (validarStatus_ClaveSSA())
                {
                    CargarGrid();
                }
            }

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ////Grid.ExportarExcel(true);
            //this.Cursor = Cursors.WaitCursor;
            //bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "MOVIMIENTO_CLAVE.xls", DatosCliente);

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
            //        xpExcel.GeneraExcel();

            //        xpExcel.Agregar(DtGeneral.EmpresaConectadaNombre, 2, 2);
            //        xpExcel.Agregar(DtGeneral.FarmaciaConectadaNombre, 3, 2);
            //        xpExcel.Agregar("De " + General.FechaYMD(dtpFechaInicial.Value) + " a " + General.FechaYMD(dtpFechaFinal.Value), 4, 2);
            //        xpExcel.Agregar(txtClaveSSA.Value + "--" + lblDescripcion.Text, 6, 3);


            //        int iRow = 9;
            //        for (int iRowGrid = 1; Grid.Rows != iRowGrid - 1; iRowGrid++)
            //        {
            //            xpExcel.Agregar(Grid.GetValue(iRowGrid, (int)Cols.Clave), iRow, 2);
            //            xpExcel.Agregar(Grid.GetValue(iRowGrid, (int)Cols.Descripcion), iRow, 3);
            //            xpExcel.Agregar(Grid.GetValueInt(iRowGrid, (int)Cols.Folios), iRow, 4);
            //            xpExcel.Agregar(Grid.GetValueInt(iRowGrid, (int)Cols.Piezas), iRow, 5);
            //            iRow++;
            //        }
            //        xpExcel.CerrarDocumento();

            //        if (General.msjConfirmar("Exportación finalidaza, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
            //        {
            //            xpExcel.AbrirDocumentoGenerado();
            //        }
            //    }
            //}

            //this.Cursor = Cursors.Default;
            GenerarReporteExcel();
        }

        private void GenerarReporteExcel()
        {
            clsGenerarExcel generarExcel = new clsGenerarExcel();
            int iColBase = 2;
            int iRenglon = 2;
            string sNombreHoja = "";
            string sNombre = string.Format("PERIODO DEL {0} AL {1} ", General.FechaYMD(dtpFechaInicial.Value, "-"), General.FechaYMD(dtpFechaFinal.Value, "-"));
            //string sNombreFile = "COM_Rpt_ConsumoEdo_Concentrado" + "_" + cboEdo.Data;

            //leer.DataSetClase = leerExportarExcel.DataSetClase;

            leer.RegistroActual = 1;


            int iColsEncabezado = iRenglon + leer.Columnas.Length - 1;
            //iColsEncabezado = iRenglon + 8;

            generarExcel.RutaArchivo = @"C:\\Excel";
            generarExcel.NombreArchivo = "";
            generarExcel.AgregarMarcaDeTiempo = true;

            if (generarExcel.PrepararPlantilla())
            {
                sNombreHoja = "Hoja1";

                generarExcel.ArchivoExcel.Worksheets.Add(sNombreHoja);

                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                generarExcel.EscribirCeldaEncabezado(sNombreHoja, iRenglon++, iColBase, iColsEncabezado, 16, DtGeneral.EstadoConectadoNombre);
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

        private void IniciarToolBar(bool Nuevo, bool Ejecutar, bool Imprimir, bool Excel)
        {
            btnNuevo.Enabled = Nuevo;
            btnEjecutar.Enabled = Ejecutar;
            btnImprimir.Enabled = Imprimir;
            btnExportarExcel.Enabled = Excel;
        }

        private void Inicializar()
        {
            Fg.IniciaControles(this, true);
            txtClaveSSA.Text = "";
            lblDescripcion.Text = "";
            lblIdClaveSSA.Text = "";
            IniciarToolBar(true, true, false, false);
            Grid.Limpiar(true);
            dtpFechaInicial.Value = General.FechaSistema;
            dtpFechaFinal.Value = General.FechaSistema;
        }
        #endregion Botones

        #region CargadoClave
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                Leer.DataSetClase = Consultas.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");
                if (Leer.Leer())
                {
                    CargaDatosClave();
                }
                else
                {
                    txtClaveSSA.Focus();
                }
            }
        }

        private void CargaDatosClave()
        {
            txtClaveSSA.Text = Leer.Campo("ClaveSSA_Aux");
            lblIdClaveSSA.Text = Leer.Campo("IdClaveSSA_Sal");
            lblDescripcion.Text = Leer.Campo("DescripcionSal"); 
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Leer.DataSetClase = Ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");
                if (Leer.Leer())
                {
                    CargaDatosClave();
                }
            }
        }

        private void txtClaveSSA_TextChanged(object sender, EventArgs e)
        {
            lblDescripcion.Text = "";
            lblIdClaveSSA.Text = "";
        }
        #endregion CargadoClave

        private bool validarStatus_ClaveSSA()
        {
            bool bRegresa = true;
            string sSql = string.Format("Select * " +
                " From vw_ProductosExistenEnEstadoFarmacia (NoLock) " +
                " Where ClaveSSA = '{0}' and StatusDeProducto In ( 'I', 'S' ) ", txtClaveSSA.Text.Trim());

            if (!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "txtCodigo_Validating");
                General.msjError("Ocurrió un error al obtener la información de la Clave SSA.");
            }
            else
            {
                if (Leer.Leer())
                {
                    bRegresa = false;
                    General.msjUser("No es posible generar el reporte por que la Clave SSA se encuentra bloqueda por inventario.");
                }
            }

            return bRegresa;
        }

        private void CargarGrid()
        {
            //if (lblDescripcion.Text == "")
            //{
            //    General.msjAviso("Ingrese la Clave SSA por favor", "Cosulta sin procesar");
            //    txtClaveSSA.Focus();
            //}
            //else
            {
                string sSql = string.Format("Exec spp_MovimientosPorClave '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                              DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                              txtClaveSSA.Value, General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value), txtPersonal.Text);

                Grid.Limpiar(true);

                if (!leer.Exec(sSql)) 
                {
                    Error.GrabarError(leer.Error, "CargarGrid");
                    General.msjError("Ocurrió Un Error al buscar la Información.");
                }
                else
                {
                    IniciarToolBar(true, false, false, true);
                    txtClaveSSA.Enabled = false;
                    txtPersonal.Enabled = false;
                    dtpFechaInicial.Enabled = false;
                    dtpFechaFinal.Enabled = false;
                     
                    ////for (int Reng = 1; Leer.Leer(); Reng++)
                    ////{
                    ////    Grid.AddRow();
                    ////    Grid.SetValue(Reng, (int)Cols.Efecto, Leer.Campo("efecto"));
                    ////    Grid.SetValue(Reng, (int)Cols.Clave, Leer.Campo("tipomovto"));
                    ////    Grid.SetValue(Reng, (int)Cols.Descripcion, Leer.Campo("Descripcion"));
                    ////    Grid.SetValue(Reng, (int)Cols.Folios, Leer.Campo("Folios"));
                    ////    Grid.SetValue(Reng, (int)Cols.Piezas, Leer.Campo("Piezas"));
                    ////}
                    if (leer.Leer())
                    {
                        Grid.LlenarGrid(leer.DataSetClase);
                    }
                }
            }
        }

        private void grdConcentradoPorClave_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            byte bEfecto;
            string Cadena = Grid.GetValue(Grid.ActiveRow,(int)Cols.Efecto); 

            if ("Entrada" == Cadena)
            {
               bEfecto = 0;
            }
            else 
            {
                bEfecto = 1;
            } 

            string sSql = string.Format("exec spp_MovimientosPorClaveDetallado '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                          DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, 
                          General.FechaYMD(dtpFechaInicial.Value), General.FechaYMD(dtpFechaFinal.Value),
                          txtClaveSSA.Text ,bEfecto,Grid.GetValue(Grid.ActiveRow,(int)Cols.Clave), txtPersonal.Text);

            if (!Leer.Exec(sSql))
            {
                Error.GrabarError(Leer, "btnEjecutar_Click");
                General.msjError("Ocurrió un error al obtener la lista de movimientos.");
            }
            else
            {
                if (Leer.Leer())
                {
                    FrmMovimientosPorClaveDetalle F =  new FrmMovimientosPorClaveDetalle(Leer.DataSetClase, 
                                                                                         Grid.GetValue(Grid.ActiveRow,(int)Cols.Clave),
                                                                                         Grid.GetValue(Grid.ActiveRow, (int)Cols.Descripcion), 
                                                                                         General.FechaYMD(dtpFechaInicial.Value),
                                                                                         General.FechaYMD(dtpFechaFinal.Value),txtClaveSSA.Text,
                                                                                         lblDescripcion.Text);
                    F.ShowDialog();
                }
                else
                {
                    General.msjAviso("No hay movimientos para mostrar.");
                }
            }
        }

        private void txtPersonal_Validating(object sender, CancelEventArgs e)
        {
            if (txtPersonal.Text.Trim() != "")
            {
                Leer.DataSetClase = Consultas.Personal(DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, txtPersonal.Text.Trim(), "txtPersonal_Validating()");
                if (Leer.Leer())
                {
                    CargarDatosPersonal();
                }
                else
                {
                    txtPersonal.Focus();
                }
            }
        }

        private void txtPersonal_TextChanged(object sender, EventArgs e)
        {
            lblPersonal.Text = "";
        }

        private void txtPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Leer.DataSetClase = Ayuda.Personal("txtClaveSSA_KeyDown", DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);
                if (Leer.Leer())
                {
                    CargarDatosPersonal();
                }
            }
        }

        private void CargarDatosPersonal()
        {
            txtPersonal.Text = Leer.Campo("IdPersonal");
            lblPersonal.Text = Leer.Campo("NombreCompleto");
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
