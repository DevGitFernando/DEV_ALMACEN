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
using SC_SolutionsSystem.Reportes;
//using SC_SolutionsSystem.ExportarDatos;

using DllFarmaciaSoft;

namespace Almacen.Pedidos
{
    public partial class FrmMonitorSurtimientoDePedidos_Base : FrmBaseExt
    {
        enum Cols
        {
            IdJurisdiccion = 2, Jurisdiccion = 3,
            IdFarmacia = 4, Farmacia = 5, FolioPedido = 6, FechaPed = 7, FolioSurtido = 8, StatusPedido = 9, Status = 10
        }

        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsDatosCliente datosCliente;
        clsLeer leer;
        clsConsultas Consultas;
        clsListView lst;

        DataSet dtsFarmacias = new DataSet();

        //clsExportarExcelPlantilla xpExcel;
        DataSet dtsPedidos;

        DateTime dtCuenta = DateTime.Now;
        int iMinutosActualizacion = 5;
        string sTituloActualizacion = "";

        TipoMonitorDeSurtido tpTipoDeMonitor = TipoMonitorDeSurtido.Ninguno;

        public FrmMonitorSurtimientoDePedidos_Base(TipoMonitorDeSurtido MonitorDeSurtido)
        {
            InitializeComponent();
            tpTipoDeMonitor = MonitorDeSurtido; 


            int iAnchoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Width * 0.98);
            int iAltoPantalla = (int)((double)Screen.PrimaryScreen.WorkingArea.Size.Height * 0.85);
            this.Width = iAnchoPantalla;
            this.Height = iAltoPantalla;

            General.Pantalla.AjustarTamaño(this, 95, 80);

            AjustarTamañoFuente();
            ConfigurarInterface();


            datosCliente = new clsDatosCliente(DtGeneral.DatosApp, this.Name, "FrmMonitorSurtimientoDePedidos_Base");

            leer = new clsLeer(ref cnn);
            Consultas = new clsConsultas(General.DatosConexion, DtGeneral.DatosApp, this.Name);
            Error = new SC_SolutionsSystem.Errores.clsGrabarError(DtGeneral.DatosApp, this.Name);

            lst = new clsListView(listvwPedidos);

            sTituloActualizacion = lblTiempoActualizacion.Text; 
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0); 
            CargarStatusPedidos();
        }

        private void ConfigurarInterface()
        {

            //////// Configurar titulo de la pantalla 
            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Surtimiento)
            {
                this.Text = "Monitor de pedidos : Surtimiento ";
                this.Name = "FrmMonitorSurtimientoDePedidos";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Entrega_Validacion)
            {
                this.Text = "Monitor de pedidos : En proceso de entrega a Validación ";
                this.Name = "FrmMonitorPedidos_EntregaValidacion";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Validacion)
            {
                this.Text = "Monitor de pedidos : Validación ";
                this.Name = "FrmMonitorPedidos_Validacion";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Entrega_Documentacion)
            {
                this.Text = "Monitor de pedidos : En proceso de entrega a Documentación ";
                this.Name = "FrmMonitorPedidos_EntregaDocumentacion";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Documentacion)
            {
                this.Text = "Monitor de pedidos : Documentación ";
                this.Name = "FrmMonitorPedidos_Documentacion";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Entrega_Embarques)
            {
                this.Text = "Monitor de pedidos : En proceso de entrega a Embarques ";
                this.Name = "FrmMonitorPedidos_EntregaEnbarques";
            }

            if (tpTipoDeMonitor == TipoMonitorDeSurtido.Embarques)
            {
                this.Text = "Monitor de pedidos : Embarques ";
                this.Name = "FrmMonitorPedidos_Embarques";
            }

            //////// Configurar titulo de la pantalla 
        }

        private void AjustarTamañoFuente()
        { 
        }

        private void FrmMonitorSurtimientoDePedidos_Base_Load(object sender, EventArgs e)
        {            
            btnNuevo_Click(null, null);

            tmActualizarInformacion.Interval = (1000 * 60) * iMinutosActualizacion;

            ConfigurarCuentaRegresiva(); 
            CargarListaPedidos(); 
        }

        private void FrmMonitorSurtimientoDePedidos_Base_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    CargarListaPedidos();
                    break;

                default:
                    break;
            }
        }

        #region Botones
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaPantalla();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            CargarListaPedidos();
        }

        private void btnFuente_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = listvwPedidos.Font;
            ////font.Font = FramePedidos.Font;

            if (font.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listvwPedidos.Font = font.Font;
                ////FramePedidos.Font = font.Font; 
                CargarListaPedidos(); 
            }
        }
        #endregion Botones

        #region Funciones
        private void ConfigurarCuentaRegresiva()
        {
            dtCuenta = new DateTime(dtCuenta.Year, dtCuenta.Month, dtCuenta.Day, 0, iMinutosActualizacion, 0);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss")); 
        }

        private void LimpiaPantalla()
        {
            Fg.IniciaControles(this, true);
            ////btnExportarExcel.Enabled = false;

            ////cboJurisdicciones.SelectedIndex = 0;
            ////cboFarmacias.SelectedIndex = 0;
            ////cboStatusPed.SelectedIndex = 0;

            ////dtpFechaInicial.Value = dtpFechaInicial.Value.AddDays(-1); 

            lst.LimpiarItems();
            ////cboJurisdicciones.Focus();
        }
        #endregion Funciones

        #region CargarCombos 
        private void CargarStatusPedidos()
        {
            ////cboStatusPed.Clear();
            ////cboStatusPed.Add("*", "TODOS"); 

            ////leer.DataSetClase = Consultas.PedidosSurtimiento_Status("CargarStatusPedidos()");
            ////if (leer.Leer())
            ////{
            ////    cboStatusPed.Add(leer.DataSetClase, true, "ClaveStatus", "Descripcion");
            ////}

            //////cboStatusPed.Add("*", "TODOS");
            ////cboStatusPed.Add("A", "SURTIMIENTO");
            ////cboStatusPed.Add("S", "SURTIDO");
            ////cboStatusPed.Add("D", "DISTRIBUCION");
            ////cboStatusPed.Add("T", "TRANSITO");
            ////cboStatusPed.Add("R", "REGISTRADO");
            ////cboStatusPed.Add("C", "CANCELADO");

            ////cboStatusPed.SelectedIndex = 0;
        }
        #endregion CargarCombos

        #region CargarPedidos
        private void CargarListaPedidos()
        {
            string sJurisdiccion = "", sFarmaciaPed = "", sStatusPed = "";
            DateTime dtFecha = General.FechaSistema;

            tmCuentaRegresiva.Stop();
            tmCuentaRegresiva.Enabled = false; 

            tmActualizarInformacion.Stop(); 
            tmActualizarInformacion.Enabled = false;

            ////btnExportarExcel.Enabled = false;

            //////if (cboJurisdicciones.Data != "*")
            //////{
            //////    sJurisdiccion = string.Format(" and IdJurisdiccion = '{0}' ", cboJurisdicciones.Data);
            //////}

            //////if (cboFarmacias.Data != "*")
            //////{
            //////    sFarmaciaPed = string.Format(" and IdFarmaciaPedido = '{0}' ", cboFarmacias.Data);
            //////}

            //////if (cboStatusPed.Data != "*")
            //////{
            //////    sStatusPed = string.Format(" and Status = '{0}' ", cboStatusPed.Data);
            //////}

            string sSql = string.Format(
                " Select 'Semaforo' = Prioridad, 'Prioridad' = PrioridadDesc, " + 
                " 'Tipo de pedido' = TipoDePedidoDescripcion, " +
                " 'Núm. Jurisdicción' = IdJurisdiccionPedido, 'Jurisdicción' = JurisdiccionPedido, " +
                " 'Núm. Farmacia Pedido' = IdFarmaciaSolicita, 'Farmacia Pedido' = FarmaciaSolicita, 'Folio Pedido' = FolioPedido, Convert(varchar(10), FechaEntrega, 120) As 'Fecha Entrega', " +
                " 'Fecha Pedido' = Convert(varchar(10), FechaRegistro, 120), 'Folio Surtido' = FolioSurtido, 'Status Pedido' = StatusPedido " + 
                " From vw_PedidosCedis_Surtimiento (Nolock) " + 
                " Where IdEmpresa = '{0}' and IdEstado = '{1}' {2} {3} " + 
                "   and Convert(varchar(10), FechaRegistro, 120) Between '{4}' and '{5}'  {6}  " + 
                " Order By IdFarmacia, FolioPedido ", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, sJurisdiccion, sFarmaciaPed,
                General.FechaYMD(dtFecha.AddDays(-2)), General.FechaYMD(dtFecha), sStatusPed);


            sSql = string.Format("Exec spp_Mtto_Pedidos_Cedis__Monitor " + 
                "\t@IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Tipo = '{3}' \n", 
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada, (int)tpTipoDeMonitor); 

            dtsPedidos = new DataSet();
            lst.Limpiar(); 
            lst.LimpiarItems();

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "CargarListaPedidos()");
                General.msjError("Ocurrió un error al obtener la información requerida.");
            }
            else
            {
                if (!leer.Leer())
                {
                    ////General.msjUser("No se encontro información con los criterios especificados.");
                }
                else
                {
                    dtsPedidos = leer.DataSetClase;
                    lst.CargarDatos(leer.DataSetClase, true, true);
                    ////btnExportarExcel.Enabled = true;
                }
            }

            lst.AjustarColumnas();

            if (lst.Registros <= 0)
            {
                General.msjUser("No se encontro información con los criterios especificados.");
            }

            ConfigurarCuentaRegresiva(); 
            tmCuentaRegresiva.Enabled = true;
            tmCuentaRegresiva.Start();

            tmActualizarInformacion.Enabled = true;
            tmActualizarInformacion.Start(); 

        }
        #endregion CargarPedidos

        #region Eventos_Combos
        private void cboJurisdicciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (cboJurisdicciones.SelectedIndex != 0)
            ////{
            ////    CargarFarmacias();
            ////}
        }
        #endregion Eventos_Combos

        #region Exportar_Excel


        //private void GenerarExcel()
        //{
        //    string sRutaPlantilla = Application.StartupPath + @"\\Plantillas\ALMN_Rpt_Tablero_StatusPedidosSurtido.xls";
        //    bool bRegresa = DtGeneral.GenerarReporteExcel(General.Url, General.ImpresionViaWeb, "ALMN_Rpt_Tablero_StatusPedidosSurtido.xls", datosCliente);

        //    if (!bRegresa)
        //    {
        //        this.Cursor = Cursors.Default;
        //        General.msjAviso("No fue posible descargar la plantilla solicitada, intente de nuevo.");
        //    }
        //    else
        //    {
        //        xpExcel = new clsExportarExcelPlantilla(sRutaPlantilla);
        //        xpExcel.AgregarMarcaDeTiempo = true;
        //        //leer.DataSetClase = dtsExistencias;

        //        this.Cursor = Cursors.Default;
        //        if (xpExcel.PrepararPlantilla())
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ExportarPedidos();

        //            if (General.msjConfirmar("Exportación finalizada, ¿ Desea abrir el documento generado ? ") == DialogResult.Yes)
        //            {
        //                xpExcel.AbrirDocumentoGenerado();
        //            }
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        //private void ExportarPedidos()
        //{
        //    int iHoja = 1, iRenglon = 9;
        //    string sEmpresa = DtGeneral.EmpresaConectadaNombre;
        //    string sEstado = DtGeneral.FarmaciaConectada + "--" + DtGeneral.FarmaciaConectadaNombre + ", " + DtGeneral.EstadoConectadoNombre;
        //    string sConceptoReporte = "Reporte de Status de Pedidos para Surtido";
        //    string sFechaImpresion = General.FechaSistemaFecha.ToString();

        //    leer.DataSetClase = dtsPedidos;
        //    xpExcel.GeneraExcel(iHoja);

        //    xpExcel.Agregar(sEmpresa, 2, 2);
        //    xpExcel.Agregar(sEstado, 3, 2);
        //    xpExcel.Agregar(sConceptoReporte, 4, 2);

        //    //xpExcel.Agregar(leer.CampoFecha("FechaImpresion").ToString(), 6, 3);
        //    xpExcel.Agregar(sFechaImpresion, 6, 3);

        //    while (leer.Leer())
        //    {
        //        xpExcel.Agregar(leer.Campo("Núm. Jurisdicción"), iRenglon, (int)Cols.IdJurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Jurisdicción"), iRenglon, (int)Cols.Jurisdiccion);
        //        xpExcel.Agregar(leer.Campo("Núm. Farmacia Pedido"), iRenglon, (int)Cols.IdFarmacia);
        //        xpExcel.Agregar(leer.Campo("Farmacia Pedido"), iRenglon, (int)Cols.Farmacia);
        //        xpExcel.Agregar(leer.Campo("Folio Pedido"), iRenglon, (int)Cols.FolioPedido);
        //        xpExcel.Agregar(leer.Campo("Fecha Pedido"), iRenglon, (int)Cols.FechaPed);
        //        xpExcel.Agregar(leer.Campo("Folio Surtido"), iRenglon, (int)Cols.FolioSurtido);
        //        xpExcel.Agregar(leer.Campo("Status Pedido"), iRenglon, (int)Cols.StatusPedido);
        //        xpExcel.Agregar(leer.Campo("Status"), iRenglon, (int)Cols.Status);
        //        iRenglon++;
        //    }

        //    // Finalizar el Proceso 
        //    xpExcel.CerrarDocumento();
        //}
        #endregion Exportar_Excel

        private void tmCuentaRegresiva_Tick(object sender, EventArgs e)
        {
            dtCuenta = dtCuenta.AddSeconds(-1);
            lblTiempoActualizacion.Text = string.Format("{0}  {1}", sTituloActualizacion, dtCuenta.ToString("HH:mm:ss")); 
        }

        private void tmActualizarInformacion_Tick(object sender, EventArgs e)
        {
            CargarListaPedidos(); 
        }
    }
}
