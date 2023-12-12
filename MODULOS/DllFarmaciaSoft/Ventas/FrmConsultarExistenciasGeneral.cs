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
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGrid;
////using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Ventas
{
    public partial class FrmConsultarExistenciasGeneral : FrmBaseExt
    {
        private enum Cols
        {
            IdClave = 1, ClaveSSA = 2, DescripcionClave = 3, 
            IdProducto = 4, DescripcionProducto = 5, CodigoEAN = 6, ClaveLote = 7, 
            FechaCaducidad = 8, Existencia = 9, CostoProm = 10, ImpCostoProm = 11,
            UltimoCosto = 12, ImpUltimoCosto = 13
        }

        private enum ColsCon
        {
            IdClave = 2, ClaveSSA = 3, DescripcionClave = 4, Existencia = 5
        }

        private enum ColsDet
        {
            IdClave = 2, ClaveSSA = 3, DescripcionClave = 4, IdProducto = 5, DescripcionProducto = 6, CodigoEAN = 7, ClaveLote = 8,
            FechaCaducidad = 9, Existencia = 10, CostoProm = 11, ImpCostoProm = 12, UltimoCosto = 13, ImpUltimoCosto = 14
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;

        bool bExistenDatos = false; 
        DataSet dtsDatosConsulta = new DataSet(); 

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet(); 

        string sSqlFarmacias = "";
        string sUrl;

        //clsExportarExcelPlantilla xpExcel;
        clsGenerarExcel excel = new clsGenerarExcel();
        DataSet dtsExistencias;

        public FrmConsultarExistenciasGeneral()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 

            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.SeleccionSimple);

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada ); 

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            rdoConcentrado.Checked = true;
            rdoConcentrado.Enabled = true;
            rdoDetallado.Enabled = true;
           
            MostrarOcultarDetalle(true); 
            iBusquedasEnEjecucion = 0;
            grid.Limpiar(false);
            // grid.Reset(); 

            Fg.IniciaControles();

            cboEstados.Enabled = false; 
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0; 

            cboFarmacias.Enabled = false;
            cboFarmacias.Clear();
            cboFarmacias.Add();
            cboFarmacias.SelectedIndex = 0; 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarFarmaciasGrid();
            IniciarConsultaExistencias();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            grid.ExportarExcel(true); 
        }
        #endregion Botones

        private void FrmConsultarExistenciasGeneralEnFarmacias_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null); 
        }

        private void CargarEmpresas()
        {
            cboEmpresas.Clear();
            cboEmpresas.Add();

            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.SelectedIndex = 0;

            string sSql = "Select Distinct IdEmpresa, NombreEmpresa From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarEmpresas()");
                General.msjError("Ocurrió un error al obtener la lista de Empresas.");
            }
            else
            {
                cboEmpresas.Add(leer.DataSetClase, true, "IdEmpresa", "NombreEmpresa"); 
                sSql = "Select IdEstado, NombreEstado, IdEmpresa, StatusEdo From vw_EmpresasEstados (NoLock) Order By IdEmpresa ";
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "CargarEmpresas()");
                    General.msjError("Ocurrió un error al obtener la lista de Estados por Empresas.");
                }
                else
                {
                    dtsEstados = leer.DataSetClase; 
                }

            }
            cboEmpresas.SelectedIndex = 0; 
        }

        private void CargarEstados()
        {
            string sFiltro = string.Format(" IdEmpresa = '{0}' and StatusEdo = '{1}' ", cboEmpresas.Data, "A"); 
            cboEstados.Clear();
            cboEstados.Add();
            cboEstados.Add(dtsEstados.Tables[0].Select(sFiltro), true, "IdEstado", "NombreEstado"); 
            cboEstados.SelectedIndex = 0;  
        } 

        private void CargarFarmacias()
        {

            sSqlFarmacias = string.Format(" Select IdFarmacia, (IdFarmacia + ' - ' + Farmacia) as Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada );

            cboFarmacias.Clear();
            cboFarmacias.Add("0", "<< Seleccione >>");

            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                cboFarmacias.Add(leer.DataRowsClase, true, "IdFarmacia", "Farmacia");
            }
            cboFarmacias.SelectedIndex = 0;
        }

        private void rdoFarmacia_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoFarmacia.Checked)
            //    cboFarmacias.Enabled = true;
        }

        private void rdoTodas_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoTodas.Checked)
            //    cboFarmacias.Enabled = false;
        }

        private void CargarFarmaciasGrid()
        {
            //////if (rdoFarmacia.Checked)
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado, cboFarmacias.Data);

            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) and IdFarmacia = '{3}' " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{4}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, cboFarmacias.Data, iEsEmpresaConsignacion); 
            //////}
            //////else
            //////{
            //////    //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////    //    " From vw_Farmacias_Urls (NoLock) " +
            //////    //    " Where IdEstado = '{0}' and FarmaciaStatus = 'A' ",
            //////    //    DtGeneral.EstadoConectado );
                
            //////    sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
            //////                    " From vw_Farmacias_Urls (NoLock) " +
            //////                    " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
            //////                    " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{3}' ",
            //////        DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, iEsEmpresaConsignacion ); 
            //////}


            //////if (!leer.Exec(sSqlFarmacias))
            //////{
            //////    Error.GrabarError(leer, "CargarFarmacias()");
            //////    General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            //////}
            //////else
            //////{
            //////    grid.Limpiar(false);
            //////    grid.LlenarGrid(leer.DataSetClase);
            //////}
        }

        private void IniciarConsultaExistencias()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            btnImprimir.Enabled = false;
            btnExportarExcel.Enabled = false;

            iBusquedasEnEjecucion = 1;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start(); 

            rdoConcentrado.Enabled = false;
            rdoDetallado.Enabled = false; 

            //for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.ConsultarExistenciaFarmacia);
                _workerThread.Name = "ConsultaDeInformacion";
                _workerThread.Start(1);
            }
        }

        private void ConsultarExistenciaFarmacia(object Renglon)
        {
            Cursor.Current = Cursors.WaitCursor;
            bExistenDatos = false; 
            dtsDatosConsulta = new DataSet();
            dtsExistencias = new DataSet();

            int iRow = (int)Renglon;
            // string sIdFarmacia = grid.GetValue(iRow, 1);
            sUrl = grid.GetValue(iRow, 3);
            // string sValor = ""; ////  "-- " + DtGeneral.EstadoConectado + "-" + sIdFarmacia;

            sUrl = cboFarmacias.ItemActual.GetItem("UrlFarmacia"); // ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();  

            string sSql = string.Format(" Select 'IdClave SSA' = IdClaveSSA_Sal, " + 
                " 'Clave SSA' = ClaveSSA, 'Descripcion Clave' = DescripcionSal, " +
                " '' as IdProducto, '' as Producto, '' as CodigoEAN, '' as ClaveLote, " +
                " 'Fecha de Caducidad' = '', sum(Existencia) as Existencia  " + 
	            " from vw_ExistenciaPorCodigoEAN_Lotes " + 
	            " where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " + 
	            " group by IdClaveSSA_Sal, ClaveSSA, DescripcionSal " +
                " order by DescripcionSal ", 
                cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data );

            if (rdoDetallado.Checked)
            {
                //sSql = string.Format(" Select 'IdClave SSA' = IdClaveSSA_Sal, " +
                //    " 'Clave SSA' = ClaveSSA, 'Descripcion Clave' = DescripcionSal, " +
                //    " IdProducto, 'Producto' = DescripcionProducto, CodigoEAN, ClaveLote, " + 
                //    " 'Fecha de Caducidad' = convert(varchar(10), FechaCaducidad, 120), Existencia " + 
                //    " from vw_ExistenciaPorCodigoEAN_Lotes " +
                //    " where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' " +
                //    " order by DescripcionSal, DescripcionProducto, FechaCaducidad ",  
                //    cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data); 

                sSql = string.Format(" Select 'IdClave SSA' = E.IdClaveSSA_Sal, " +
                    " 'Clave SSA' = E.ClaveSSA, 'Descripcion Clave' = E.DescripcionSal, " +
                    " E.IdProducto, 'Producto' = E.DescripcionProducto, E.CodigoEAN, E.ClaveLote, " +
                    " 'Fecha de Caducidad' = convert(varchar(10), E.FechaCaducidad, 120), E.Existencia, " +
                    " F.CostoPromedio, ( E.Existencia * F.CostoPromedio ) As ImporteCostoPromedio, " +
                    " F.UltimoCosto, ( E.Existencia * F.UltimoCosto ) As ImporteUltimoCosto " +
                    " from vw_ExistenciaPorCodigoEAN_Lotes E (Nolock)  " +
                    " Inner Join FarmaciaProductos F ( Nolock )  " +
                        " On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia " +
                            " And E.IdProducto = F.IdProducto )  " +
                    " where E.IdEmpresa = '{0}' and E.IdEstado = '{1}' and E.IdFarmacia = '{2}'  " +
                    " order by E.DescripcionSal, E.DescripcionProducto, E.FechaCaducidad ",
                    cboEmpresas.Data, cboEstados.Data, cboFarmacias.Data);
            }

            grid.Limpiar(true);
            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, 3, "");

            //// grid.Reset(); 
            //iBusquedasEnEjecucion++;

            // clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, General.ArchivoIni, datosCliente);
            clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, DtGeneral.CfgIniPuntoDeVenta, datosCliente);
            if (!myWeb.Exec(sSql))
            {
                Error.LogError(cboFarmacias.Text + " ----  " + myWeb.Error.Message); 
                grid.ColorRenglon(iRow, colorEjecucionError);
                rdoConcentrado.Enabled = true;
                rdoDetallado.Enabled = true; 
            }
            else
            {
                if (myWeb.Leer())
                {
                    bExistenDatos = true; 
                    dtsDatosConsulta = myWeb.DataSetClase;
                    dtsExistencias = myWeb.DataSetClase;
                    // grid.SetValue(iRow, 4, myWeb.Campo("Existencia"));
                    // grid.LlenarGrid(myWeb.DataSetClase); 
                }

                grid.ColorRenglon(iRow, colorEjecucionExito); 
            }
            iBusquedasEnEjecucion = 0;
            // grid.SetValue(iRow, 4, sIdFarmacia); 
            Cursor.Current = Cursors.Default;
        }


        #region Datos para consulta 
        #endregion Datos para consulta

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                if (bExistenDatos)
                {
                    bExistenDatos = false; 
                    // grid.LlenarGrid(dtsDatosConsulta, true, false);
                    grid.LlenarGrid(dtsDatosConsulta); 
                }

                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnImprimir.Enabled = true; 
                btnNuevo.Enabled = true;
                btnExportarExcel.Enabled = true;
            }
        }

        private void FrmConsultarExistenciasGeneralEnFarmacias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iBusquedasEnEjecucion != 0)
            {
                e.Cancel = true;
            }
        }

        private void FrmConsultarExistenciasGeneralEnFarmacias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false;
                cboEstados.Enabled = true; 
                CargarEstados(); 
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEstados.SelectedIndex != 0)
            {
                grid.Limpiar();
                cboEstados.Enabled = false;
                cboFarmacias.Enabled = true; 
                CargarFarmacias(); 
            } 
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFarmacias.SelectedIndex != 0)
            {
                sUrl = ((DataRow)cboFarmacias.ItemActual.Item)["UrlFarmacia"].ToString();  
                cboFarmacias.Enabled = false; 
            }
        }

        private void rdoConcentrado_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoConcentrado.Checked)
            {
                MostrarOcultarDetalle(true);
            }
        }

        private void rdoDetallado_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDetallado.Checked)
            {
                MostrarOcultarDetalle(false);
            }
        }

        private void MostrarOcultarDetalle(bool Ocultar)
        {
            grid.OcultarColumna(Ocultar, (int)Cols.IdProducto, (int)Cols.FechaCaducidad);
            grid.OcultarColumna(Ocultar, (int)Cols.CostoProm, (int)Cols.ImpUltimoCosto);
        }

        #region Exportar_A_Excel
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        private void GenerarExcel()
        {
            string sNombreDocumento = string.Format("ReporteDeExistenciasGenerales");
            bool bRegresa = true; 
            string sRutaPlantilla = "";
            this.Cursor = Cursors.WaitCursor;
 
            if (rdoConcentrado.Checked)
            {
                sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_ConcentradoExistencias_Unidad.xls";
                //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_ConcentradoExistencias_Unidad.xls", datosCliente);
            }

            if (rdoDetallado.Checked)
            {
                sRutaPlantilla = Application.StartupPath + @"\\Plantillas\Rpt_DetalladoExistencias_Unidad.xls";
                //bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "Rpt_DetalladoExistencias_Unidad.xls", datosCliente);
            }

            if (!bRegresa)
            {
                this.Cursor = Cursors.Default;
                General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            }
            else
            {
                ////xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                ////xpExcel.AgregarMarcaDeTiempo = true;                

                excel = new clsGenerarExcel();
                excel.RutaArchivo = @"C:\\Excel";
                excel.NombreArchivo = sNombreDocumento;
                excel.AgregarMarcaDeTiempo = true;


                this.Cursor = Cursors.Default;
                if (excel.PrepararPlantilla(sNombreDocumento))
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (rdoConcentrado.Checked)
                    {
                        ExistenciasConcentrado();
                    }

                    if (rdoDetallado.Checked)
                    {
                        ExistenciasDetallado();
                    }

                    this.Cursor = Cursors.Default;



                    excel.CerraArchivo();

                    this.Cursor = Cursors.Default;

                    excel.AbrirDocumentoGenerado(true);


                    ////if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                    ////{
                    ////    xpExcel.AbrirDocumentoGenerado();
                    ////}
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ExistenciasConcentrado()
        {
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            string sEmpresa = ((DataRow)cboEmpresas.ItemActual.Item)["NombreEmpresa"].ToString();
            string sEstado = ((DataRow)cboFarmacias.ItemActual.Item)["Farmacia"].ToString() + ", " + ((DataRow)cboEstados.ItemActual.Item)["NombreEstado"].ToString();
            //string sConceptoReporte = "Reporte Concentrado de Existencias";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            string sNombreHoja = "ExistenciasConcentradas";
            string sConcepto = "REPORTE DE EXISTENCIAS CONCENTRADO";


            leer.DataSetClase = dtsExistencias;
            ////xpExcel.GeneraExcel(iHoja);

            ////xpExcel.Agregar(sEmpresa, 2, 2);
            ////xpExcel.Agregar(sEstado, 3, 2);
            //////xpExcel.Agregar(sConceptoReporte, 4, 2);

            //////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            ////xpExcel.Agregar(sFechaImpresion, 6, 3);

            ////while (leer.Leer())
            ////{
            ////    xpExcel.Agregar(leer.Campo("IdClave SSA"), iRenglon, (int)ColsCon.IdClave);
            ////    xpExcel.Agregar(leer.Campo("Clave SSA"), iRenglon, (int)ColsCon.ClaveSSA);
            ////    xpExcel.Agregar(leer.Campo("Descripcion Clave"), iRenglon, (int)ColsCon.DescripcionClave);
            ////    xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)ColsCon.Existencia);

            ////    iRenglon++;
            ////}

            ////// Finalizar el Proceso 
            ////xpExcel.CerrarDocumento();
            ///

            excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
            excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
            excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
            excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

            iRenglon = 8;
            excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsExistencias);

            //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
            excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);

        }

        private void ExistenciasDetallado()
        {
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;

            string sEmpresa = ((DataRow)cboEmpresas.ItemActual.Item)["NombreEmpresa"].ToString();
            string sEstado = ((DataRow)cboFarmacias.ItemActual.Item)["Farmacia"].ToString() + ", " + ((DataRow)cboEstados.ItemActual.Item)["NombreEstado"].ToString();
            //string sConceptoReporte = "Reporte de Status de Pedidos para Surtido";
            string sFechaImpresion = General.FechaSistemaFecha.ToString();

            string sNombreHoja = "ExistenciasDetalladas";
            string sConcepto = "REPORTE DE EXISTENCIAS DETALLADO";

            leer.DataSetClase = dtsExistencias;
            ////xpExcel.GeneraExcel(iHoja);

            ////xpExcel.Agregar(sEmpresa, 2, 2);
            ////xpExcel.Agregar(sEstado, 3, 2);
            //////xpExcel.Agregar(sConceptoReporte, 4, 2);

            //////xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
            ////xpExcel.Agregar(sFechaImpresion, 6, 3);

            ////while (leer.Leer())
            ////{
            ////    xpExcel.Agregar(leer.Campo("IdClave SSA"), iRenglon, (int)ColsDet.IdClave);
            ////    xpExcel.Agregar(leer.Campo("Clave SSA"), iRenglon, (int)ColsDet.ClaveSSA);
            ////    xpExcel.Agregar(leer.Campo("Descripcion Clave"), iRenglon, (int)ColsDet.DescripcionClave);
            ////    xpExcel.Agregar(leer.Campo("IdProducto"), iRenglon, (int)ColsDet.IdProducto);
            ////    xpExcel.Agregar(leer.Campo("Producto"), iRenglon, (int)ColsDet.DescripcionProducto);
            ////    xpExcel.Agregar(leer.Campo("CodigoEAN"), iRenglon, (int)ColsDet.CodigoEAN);
            ////    xpExcel.Agregar(leer.Campo("ClaveLote"), iRenglon, (int)ColsDet.ClaveLote);
            ////    xpExcel.Agregar(leer.Campo("Fecha de Caducidad"), iRenglon, (int)ColsDet.FechaCaducidad);
            ////    xpExcel.Agregar(leer.Campo("Existencia"), iRenglon, (int)ColsDet.Existencia);
            ////    xpExcel.Agregar(leer.Campo("CostoPromedio"), iRenglon, (int)ColsDet.CostoProm);
            ////    xpExcel.Agregar(leer.Campo("ImporteCostoPromedio"), iRenglon, (int)ColsDet.ImpCostoProm);
            ////    xpExcel.Agregar(leer.Campo("UltimoCosto"), iRenglon, (int)ColsDet.UltimoCosto);
            ////    xpExcel.Agregar(leer.Campo("ImporteUltimoCosto"), iRenglon, (int)ColsDet.ImpUltimoCosto);
            ////    iRenglon++;
            ////}

            ////// Finalizar el Proceso 
            ////xpExcel.CerrarDocumento();
            ///

            excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
            excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
            excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 14, sConcepto);
            excel.EscribirCeldaEncabezado(sNombreHoja, 5, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

            iRenglon = 8;
            excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsExistencias);

            //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
            excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);


        }
        #endregion Exportar_A_Excel
    }
}