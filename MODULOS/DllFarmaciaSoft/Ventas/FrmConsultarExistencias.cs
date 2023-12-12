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

using DllFarmaciaSoft;
using DllFarmaciaSoft.Conexiones;
////using SC_SolutionsSystem.ExportarDatos;
using DllFarmaciaSoft.ExportarExcel;

namespace DllFarmaciaSoft.Ventas
{
    public partial class FrmConsultarExistencias : FrmBaseExt
    {
        #region Enum 
        private enum Cols
        {
            Ninguna = 0,
            IdFarmacia = 2, Farmacia = 3, Existencia = 4
        }
        #endregion Enum 

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeerWebExt leerWeb;
        clsLeer leer, leerLocal, leerExportar;
        clsAyudas ayuda;
        clsConsultas query;
        clsGrid grid;
        clsConexionClienteUnidad Cliente;
        wsFarmaciaSoftGn.wsConexion conexionWeb;
        ///clsExportarExcelPlantilla xpExcel;
        DataSet dtsExistencias = new DataSet();
        DataTable dtExistencias = new DataTable();

        Color colorEjecutando = Color.DarkSeaGreen;
        Color colorEjecucionExito = Color.White;
        Color colorEjecucionError = Color.BurlyWood;

        int iBusquedasEnEjecucion = 0;
        int iEsEmpresaConsignacion = Convert.ToInt32(DtGeneral.EsEmpresaDeConsignacion);
        DataSet dtsEstados = new DataSet();
        clsGenerarExcel excel = new clsGenerarExcel();

        string sSqlFarmacias = "";

        public FrmConsultarExistencias()
        {
            InitializeComponent();
            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "ConsultaDeExistenciaFarmacias");

            leerWeb = new clsLeerWebExt(ref cnn, General.Url, General.ArchivoIni, datosCliente);
            leer = new clsLeer(ref cnn);
            leerLocal = new clsLeer(ref cnn);
            leerExportar = new clsLeer(ref cnn);
            ayuda = new clsAyudas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            query = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            conexionWeb = new wsFarmaciaSoftGn.wsConexion();
            conexionWeb.Url = General.Url;

            grid = new clsGrid(ref grdExistencia, this);
            grid.EstiloGrid(eModoGrid.SeleccionSimple);
            grid.AjustarAnchoColumnasAutomatico = true; 

            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            sSqlFarmacias = string.Format(" Select IdFarmacia, (IdFarmacia + ' - ' + Farmacia) as Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{3}' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada, iBusquedasEnEjecucion); 

            lblConsultando.BackColor = colorEjecutando;
            lblFinExito.BackColor = colorEjecucionExito;
            lblFinError.BackColor = colorEjecucionError;
        }

        #region Botones 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            iBusquedasEnEjecucion = 0;
            grid.Limpiar(false);
            Fg.IniciaControles();
            dtsExistencias = new DataSet();
            dtExistencias = new DataTable(); 

            CrearTabla();
            btnExportar.Enabled = false;

            rdoTodas.Checked = true;
            cboFarmacias.Enabled = false;

            txtClaveSSA.Focus();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (rdoFarmacia.Checked && cboFarmacias.SelectedIndex == 0)
            {
                General.msjUser("Seleccione una Farmacia por favor.");
            }
            else
            {
                btnExportar.Enabled = false;
                CargarFarmaciasGrid();
                IniciarConsultaExistencias();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }
        #endregion Botones

        private void FrmConsultarExistenciasEnFarmacias_Load(object sender, EventArgs e)
        {
            CargarEmpresas(); 
            btnNuevo_Click(null, null); 
        }

        #region Funciones 
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

            sSqlFarmacias = string.Format(" Select IdFarmacia, (IdFarmacia + ' -- ' + Farmacia) as Farmacia, UrlFarmacia " +
                            " From vw_Farmacias_Urls (NoLock) " +
                            " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                            " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ", //and EsDeConsignacion = '{3}' ",
                            cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada); //, iBusquedasEnEjecucion);

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
            if (rdoFarmacia.Checked)
                cboFarmacias.Enabled = true;
        }

        private void rdoTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoTodas.Checked)
                cboFarmacias.Enabled = false;
        }

        private void CargarFarmaciasGrid()
        {
            if (rdoFarmacia.Checked)
            {
                //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                //    " From vw_Farmacias_Urls (NoLock) " +
                //    " Where IdEstado = '{0}' and IdFarmacia = '{1}' and FarmaciaStatus = 'A' ",
                //    DtGeneral.EstadoConectado, cboFarmacias.Data);

                sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                                " From vw_Farmacias_Urls (NoLock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) and IdFarmacia = '{3}' " +
                                " and FarmaciaStatus = 'A' and StatusRelacion = 'A' and EsDeConsignacion = '{4}' ",
                    cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada, cboFarmacias.Data, iEsEmpresaConsignacion); 
            }
            else
            {
                //sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                //    " From vw_Farmacias_Urls (NoLock) " +
                //    " Where IdEstado = '{0}' and FarmaciaStatus = 'A' ",
                //    DtGeneral.EstadoConectado );
                
                sSqlFarmacias = string.Format(" Select IdFarmacia, Farmacia, UrlFarmacia " +
                                " From vw_Farmacias_Urls (NoLock) " +
                                " Where IdEmpresa = '{0}' and IdEstado = '{1}' and ( IdFarmacia <> '{2}' ) " +
                                " and FarmaciaStatus = 'A' and StatusRelacion = 'A' ", //and EsDeConsignacion = '{3}' ",
                    cboEmpresas.Data, cboEstados.Data, DtGeneral.FarmaciaConectada); //, iEsEmpresaConsignacion ); 
            }


            if (!leer.Exec(sSqlFarmacias))
            {
                Error.GrabarError(leer, "CargarFarmacias()");
                General.msjError("Ocurrió un error al obtener la lista de farmacias.");
            }
            else
            {
                grid.Limpiar(false);
                grid.LlenarGrid(leer.DataSetClase);
            }
        }

        private void IniciarConsultaExistencias()
        {
            btnNuevo.Enabled = false;
            btnEjecutar.Enabled = false;
            tmEjecuciones.Enabled = true;
            tmEjecuciones.Start();
            CrearTabla();

            for (int i = 1; i <= grid.Rows; i++)
            {
                Thread _workerThread = new Thread(this.ConsultarExistenciaFarmacia);
                _workerThread.Name = grid.GetValue(i, 2);
                _workerThread.Start(i);
            }

        }

        private void ConsultarExistenciaFarmacia(object Renglon)
        {
            DataSet dtsInformacion, dtsDatosCliente;
            int iRow = (int)Renglon;
            string sIdFarmacia = grid.GetValue(iRow, 1);
            string sUrl = grid.GetValue(iRow, 3);
            string sValor = "-- " + cboEstados.Data + "-" + sIdFarmacia;

            string sSql = string.Format(" Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, sum(Existencia) as Existencia  " +
                " from vw_ExistenciaPorCodigoEAN_Lotes " +
                " where IdEstado = '{0}' and IdFarmacia = '{1}' and IdClaveSSA_Sal = '{2}' " +
                " group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal ", DtGeneral.EstadoConectado, sIdFarmacia, txtClaveSSA.Text);

            grid.ColorRenglon(iRow, colorEjecutando);
            grid.SetValue(iRow, 4, "0");
            iBusquedasEnEjecucion++;            

            // clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, General.ArchivoIni, datosCliente);
            //clsLeerWebExt myWeb = new clsLeerWebExt(ref cnn, sUrl, "FarmaciaPtoVta", datosCliente);

            dtsInformacion = ObtenerDataSetInformacion(sIdFarmacia, sSql);
            dtsDatosCliente = datosCliente.DatosCliente();
            try
            {
                leerLocal.Reset();
                leerLocal.DataSetClase = conexionWeb.ExecuteRemoto(dtsInformacion, dtsDatosCliente);
            }
            catch { }

            if (leerLocal.SeEncontraronErrores())
            {
                Error.LogError(sValor + " -- " + sUrl + " ----  " + leerLocal.Error.Message);
                grid.ColorRenglon(iRow, colorEjecucionError);
            }
            else
            {
                 if (leerLocal.Leer())
                {
                    grid.SetValue(iRow, 4, leerLocal.Campo("Existencia"));
                    dtExistencias.ImportRow(leerLocal.DataRowClase);
                    btnExportar.Enabled = true;                    
                }

                grid.ColorRenglon(iRow, colorEjecucionExito);
            }
            iBusquedasEnEjecucion--;
            // grid.SetValue(iRow, 4, sIdFarmacia);
        }


        #region Datos para consulta 
        private void txtClaveSSA_Validating(object sender, CancelEventArgs e)
        {
            if (txtClaveSSA.Text.Trim() != "")
            {
                leer.DataSetClase = query.ClavesSSA_Sales(txtClaveSSA.Text, true, "txtClaveSSA_Validating");
                if (leer.Leer())
                {
                    txtClaveSSA.Enabled = false;
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("Descripcion");

                    txtId.Enabled = false;
                    txtCodEAN.Enabled = false;
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Clave SSA no encontrada, verifique.");
                }
            }
        }

        private void txtClaveSSA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.ClavesSSA_Sales("txtClaveSSA_KeyDown");
                if (leer.Leer())
                {
                    // txtClaveSSA.Enabled = false;
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");   // Codigo 
                    txtClaveSSA_Validating(null, null);
                }
            }
        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            if (txtId.Text.Trim() != "")
            {
                leer.DataSetClase = query.Productos(txtId.Text, "txtId_Validating");
                if (leer.Leer())
                {
                    txtId.Enabled = false;
                    txtClaveSSA.Enabled = false;
                    txtId.Text = leer.Campo("IdProducto");
                    lblDescripcion.Text = leer.Campo("Descripcion");
                    txtClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblClaveSSA.Text = leer.Campo("ClaveSSA");
                    lblDescripcionClave.Text = leer.Campo("DescripcionSal");

                    txtCodEAN.Enabled = false;
                }
                else
                {
                    e.Cancel = true;
                    General.msjUser("Clave de Producto no encontrada, verifique.");
                }
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                leer.DataSetClase = ayuda.Productos("txtId_KeyDown");
                if (leer.Leer())
                {
                    txtId.Text = leer.Campo("IdProducto");
                    txtId_Validating(null, null);
                }
            }
        }

        private void txtCodEAN_Validating(object sender, CancelEventArgs e)
        {
            if (txtCodEAN.Text.Trim() != "")
            {
                string sSql = string.Format(" Select IdProducto, CodigoEAN, CodigoEAN_Interno " +
                    " From CatProductos_CodigosRelacionados (NoLock) Where CodigoEAN = '{0}' or CodigoEAN_Interno = '{1}' ",
                    txtCodEAN.Text, Fg.PonCeros(txtCodEAN.Text, 13));

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "txtCodEAN_Validating");
                    General.msjError("Ocurió un error al válidar el Codigo EAN");
                }
                else
                {
                    if (leer.Leer())
                    {
                        txtCodEAN.Enabled = false;
                        txtCodEAN.Text = leer.Campo("CodigoEAN");
                        txtId.Text = leer.Campo("IdProducto");
                        txtId_Validating(null, null);
                    }
                    else
                    {
                        e.Cancel = true;
                        General.msjUser("Codigo EAN no encontrado, verifique.");
                    }
                }
            }
        }
        #endregion Datos para consulta

        private void tmEjecuciones_Tick(object sender, EventArgs e)
        {
            if (iBusquedasEnEjecucion == 0)
            {
                tmEjecuciones.Stop();
                tmEjecuciones.Enabled = false;

                btnEjecutar.Enabled = true;
                btnNuevo.Enabled = true;
            }
        }

        private void FrmConsultarExistenciasEnFarmacias_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iBusquedasEnEjecucion != 0)
            {
                e.Cancel = true;
            }
        }

        private void FrmConsultarExistenciasEnFarmacias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void cboEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEmpresas.SelectedIndex != 0)
            {
                cboEmpresas.Enabled = false; 
                CargarEstados(); 
            } 
        }

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fg.IniciaControles(this, true, FrameClaveSSA); 
            if (cboEstados.SelectedIndex != 0)
            {
                grid.Limpiar();
                cboEstados.Enabled = false; 
                CargarFarmacias(); 
            } 
        }

        private DataSet ObtenerDataSetInformacion(string IdFarmacia, string sSentencia)
        {
            DataSet dtsInformacion;
            Cliente = new clsConexionClienteUnidad();

            Cliente.Empresa = cboEmpresas.Data;
            Cliente.Estado = cboEstados.Data;
            Cliente.Farmacia = IdFarmacia;
            Cliente.Sentencia = sSentencia;
            Cliente.ArchivoConexionCentral = DtGeneral.CfgIniOficinaCentral;
            Cliente.ArchivoConexionUnidad = DtGeneral.CfgIniPuntoDeVenta;

            dtsInformacion = Cliente.dtsInformacion;

            return dtsInformacion;

        }


        private void GenerarExcel() 
        {
            string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\OCEN_Existencia_Unidades.xls";
            this.Cursor = Cursors.WaitCursor;
            bool bRegresa = false; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "OCEN_Existencia_Unidades.xls", datosCliente);

            ////if (!bRegresa)
            ////{
            ////    this.Cursor = Cursors.Default;
            ////    General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
            ////}
            ////else
            {
                dtsExistencias = new DataSet();
                dtsExistencias.Tables.Add(dtExistencias);

                ////xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
                ////xpExcel.AgregarMarcaDeTiempo = true;

                ////this.Cursor = Cursors.Default;
                ////if (xpExcel.PrepararPlantilla())
                ////{
                ////    this.Cursor = Cursors.WaitCursor;

                ExportarExistencias();

                ////    if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
                ////    {
                ////        xpExcel.AbrirDocumentoGenerado();
                ////    }
                ////}

                ////this.Cursor = Cursors.Default;
            }
        }

        private void ExportarExistencias()
        {

            ////leer.DataSetClase = dtsExistencias;

            string sNombreDocumento = string.Format("ReporteDeExistencias");
            string sNombreHoja = "Existencias";
            string sConcepto = "REPORTE DE EXISTENCIAS";

            string sEmpresa = DtGeneral.EmpresaConectadaNombre;
            string sEstado = DtGeneral.EstadoConectadoNombre;
            string sFarmacia = DtGeneral.FarmaciaConectada + " -- " + DtGeneral.FarmaciaConectadaNombre;
            string sFechaImpresion = General.FechaSistemaFecha.ToString();


            bool bRegresa = true; // DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "PtoVta_Vales_Emitidos_Mes.xls", DatosCliente); 
            int iHoja = 1, iRenglon = 15;
            int iColBase = 2;
            int iColsEncabezado = 8;


            excel = new clsGenerarExcel();
            excel.RutaArchivo = @"C:\\Excel";
            excel.NombreArchivo = sNombreDocumento;
            excel.AgregarMarcaDeTiempo = true;


            this.Cursor = Cursors.Default;

            if (excel.PrepararPlantilla(sNombreDocumento))
            {
                this.Cursor = Cursors.WaitCursor;

                excel.ArchivoExcel.Worksheets.Add(sNombreHoja);
                excel.EscribirCeldaEncabezado(sNombreHoja, 2, iColBase, iColsEncabezado, 20, DtGeneral.EmpresaConectadaNombre);
                excel.EscribirCeldaEncabezado(sNombreHoja, 3, iColBase, iColsEncabezado, 16, sFarmacia);
                excel.EscribirCeldaEncabezado(sNombreHoja, 4, iColBase, iColsEncabezado, 14, sConcepto);
                excel.EscribirCeldaEncabezado(sNombreHoja, 6, iColBase, iColsEncabezado, 12, string.Format("Fecha de Impresión: {0} ", General.FechaSistemaObtener()));

                iRenglon = 8;
                excel.InsertarTabla(sNombreHoja, iRenglon, iColBase, dtsExistencias);

                //excel.ArchivoExcel.Worksheet(sNombreHoja).Cell(iRenglon, iColBase).InsertTable(leer.DataTableClase, sNombreHoja);
                excel.ArchivoExcel.Worksheet(sNombreHoja).Columns().AdjustToContents(1, 65);


                excel.CerraArchivo();

                this.Cursor = Cursors.Default;

                excel.AbrirDocumentoGenerado(true);
            }


            this.Cursor = Cursors.Default;

        }

        private void CrearTabla()
        {
            dtExistencias = new DataTable();

            dtExistencias.Columns.Add("IdEstado");
            dtExistencias.Columns.Add("Estado");
            dtExistencias.Columns.Add("IdFarmacia");
            dtExistencias.Columns.Add("Farmacia");
            dtExistencias.Columns.Add("IdClaveSSA_Sal");
            dtExistencias.Columns.Add("Existencia");
        }

        private void cboFarmacias_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid.Limpiar();
            btnExportar.Enabled = false;
        }
        #endregion Funciones

        


    }
}
