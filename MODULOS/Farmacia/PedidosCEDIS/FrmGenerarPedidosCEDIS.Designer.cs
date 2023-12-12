namespace Farmacia.PedidosCEDIS
{
    partial class FrmGenerarPedidosCEDIS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarPedidosCEDIS));
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType36 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType37 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType38 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType39 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType40 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType41 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType42 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPaqueteDeDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPCM = new System.Windows.Forms.ToolStripButton();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipoPedido = new SC_ControlsCS.scComboBoxExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.frameImportacion = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStripBarraImportacion = new System.Windows.Forms.ToolStrip();
            this.btnExportarPlantilla = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.FrameEncabezado.SuspendLayout();
            this.frameImportacion.SuspendLayout();
            this.toolStripBarraImportacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator5,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnExportarExcel,
            this.toolStripSeparator4,
            this.btnGenerarPaqueteDeDatos,
            this.toolStripSeparator6,
            this.btnGenerarPCM});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarPaqueteDeDatos
            // 
            this.btnGenerarPaqueteDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPaqueteDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPaqueteDeDatos.Image")));
            this.btnGenerarPaqueteDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPaqueteDeDatos.Name = "btnGenerarPaqueteDeDatos";
            this.btnGenerarPaqueteDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPaqueteDeDatos.Text = "Generar paquete de datos de Pedidos";
            this.btnGenerarPaqueteDeDatos.Click += new System.EventHandler(this.btnGenerarPaqueteDeDatos_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarPCM
            // 
            this.btnGenerarPCM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPCM.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPCM.Image")));
            this.btnGenerarPCM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPCM.Name = "btnGenerarPCM";
            this.btnGenerarPCM.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPCM.Text = "Generar PCM";
            this.btnGenerarPCM.ToolTipText = "Generar PCM";
            this.btnGenerarPCM.Click += new System.EventHandler(this.btnGenerarPCM_Click);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.FrameProceso);
            this.FrameDetalles.Controls.Add(this.grdProductos);
            this.FrameDetalles.Location = new System.Drawing.Point(10, 207);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1165, 400);
            this.FrameDetalles.TabIndex = 3;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de Pedido";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(206, 102);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(602, 102);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(574, 64);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdProductos.Location = new System.Drawing.Point(9, 19);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1146, 374);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.TabStripRatio = 0.252417794970986D;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 11;
            this.grdProductos_Sheet1.RowCount = 12;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 149D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(0, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(0, 9).Value = 1.49D;
            this.grdProductos_Sheet1.Cells.Get(0, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(0, 10).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 70D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 50D;
            this.grdProductos_Sheet1.Cells.Get(1, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(1, 9).Value = 1.4D;
            this.grdProductos_Sheet1.Cells.Get(1, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(1, 10).Value = 50D;
            this.grdProductos_Sheet1.Cells.Get(2, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(2, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(2, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(2, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(3, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(3, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(3, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(3, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(3, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(4, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(4, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(4, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(4, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(4, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(5, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(5, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(5, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(5, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(5, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(6, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(6, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(6, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(6, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(6, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(7, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(7, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(7, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(7, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(7, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(8, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(8, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(8, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(8, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(8, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(9, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(9, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(9, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(9, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(9, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(10, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(10, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(10, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(10, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(10, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(11, 6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Cells.Get(11, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(11, 9).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(11, 10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Cells.Get(11, 10).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdClaveSSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Presentación";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Existencia";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Existencia sugerida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cantidad sugerida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Cantidad piezas";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Contenido paquete";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Cantidad cajas";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Cantidad piezas total";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType21.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType21.MaxLength = 20;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType21;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            textCellType22.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType22.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType22;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "IdClaveSSA";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType23;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 300F;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = textCellType24;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Presentación";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 120F;
            numberCellType36.DecimalPlaces = 0;
            numberCellType36.DecimalSeparator = ".";
            numberCellType36.MaximumValue = 10000000D;
            numberCellType36.MinimumValue = 0D;
            numberCellType36.Separator = ",";
            numberCellType36.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType36;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Existencia";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 73F;
            numberCellType37.DecimalPlaces = 0;
            numberCellType37.DecimalSeparator = ".";
            numberCellType37.MaximumValue = 10000000D;
            numberCellType37.MinimumValue = 0D;
            numberCellType37.Separator = ",";
            numberCellType37.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType37;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Existencia sugerida";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 80F;
            numberCellType38.DecimalPlaces = 0;
            numberCellType38.DecimalSeparator = ".";
            numberCellType38.MaximumValue = 10000000D;
            numberCellType38.MinimumValue = 0D;
            numberCellType38.Separator = ",";
            numberCellType38.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType38;
            this.grdProductos_Sheet1.Columns.Get(6).Formula = "IF((RC[-1]-RC[-2])<0,0,RC[-1]-RC[-2])";
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Cantidad sugerida";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 80F;
            numberCellType39.DecimalPlaces = 0;
            numberCellType39.DecimalSeparator = ".";
            numberCellType39.MaximumValue = 10000000D;
            numberCellType39.MinimumValue = 0D;
            numberCellType39.Separator = ",";
            numberCellType39.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = numberCellType39;
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Cantidad piezas";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 80F;
            numberCellType40.DecimalPlaces = 0;
            numberCellType40.DecimalSeparator = ".";
            numberCellType40.MaximumValue = 10000000D;
            numberCellType40.MinimumValue = 0D;
            numberCellType40.Separator = ",";
            numberCellType40.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(8).CellType = numberCellType40;
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "Contenido paquete";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType41.DecimalPlaces = 0;
            numberCellType41.DecimalSeparator = ".";
            numberCellType41.MaximumValue = 10000000D;
            numberCellType41.MinimumValue = 0D;
            numberCellType41.Separator = ",";
            numberCellType41.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(9).CellType = numberCellType41;
            this.grdProductos_Sheet1.Columns.Get(9).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Cantidad cajas";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 80F;
            numberCellType42.DecimalPlaces = 0;
            numberCellType42.DecimalSeparator = ".";
            numberCellType42.MaximumValue = 10000000D;
            numberCellType42.MinimumValue = 0D;
            numberCellType42.Separator = ",";
            numberCellType42.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = numberCellType42;
            this.grdProductos_Sheet1.Columns.Get(10).Formula = "ROUND((RC[-1]),0)*RC[-2]";
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "Cantidad piezas total";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Width = 80F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.cboTipoPedido);
            this.FrameEncabezado.Controls.Add(this.txtObservaciones);
            this.FrameEncabezado.Controls.Add(this.label10);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.lblStatus);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(10, 29);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(1167, 95);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales de Pedido";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 41;
            this.label4.Text = "Tipo de Pedido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoPedido
            // 
            this.cboTipoPedido.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoPedido.Data = "";
            this.cboTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoPedido.Filtro = " 1 = 1";
            this.cboTipoPedido.FormattingEnabled = true;
            this.cboTipoPedido.ListaItemsBusqueda = 20;
            this.cboTipoPedido.Location = new System.Drawing.Point(112, 69);
            this.cboTipoPedido.MostrarToolTip = false;
            this.cboTipoPedido.Name = "cboTipoPedido";
            this.cboTipoPedido.Size = new System.Drawing.Size(296, 21);
            this.cboTipoPedido.TabIndex = 3;
            this.cboTipoPedido.SelectedIndexChanged += new System.EventHandler(this.cboTipoPedido_SelectedIndexChanged);
            this.cboTipoPedido.Validating += new System.ComponentModel.CancelEventHandler(this.cboTipoPedido_Validating);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(112, 44);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(1043, 20);
            this.txtObservaciones.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 14);
            this.label10.TabIndex = 35;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1062, 18);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(919, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(218, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(189, 20);
            this.lblStatus.TabIndex = 32;
            this.lblStatus.Text = "CANCELADO";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(112, 19);
            this.txtFolio.MaxLength = 6;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frameImportacion
            // 
            this.frameImportacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameImportacion.Controls.Add(this.label7);
            this.frameImportacion.Controls.Add(this.toolStripBarraImportacion);
            this.frameImportacion.Controls.Add(this.cboHojas);
            this.frameImportacion.Location = new System.Drawing.Point(10, 126);
            this.frameImportacion.Name = "frameImportacion";
            this.frameImportacion.Size = new System.Drawing.Size(1167, 79);
            this.frameImportacion.TabIndex = 2;
            this.frameImportacion.TabStop = false;
            this.frameImportacion.Text = "Menú de Importación";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "Seleccionar Hoja :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraImportacion
            // 
            this.toolStripBarraImportacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarPlantilla,
            this.toolStripSeparator10,
            this.btnAbrir,
            this.toolStripSeparator7,
            this.btnEjecutar,
            this.toolStripSeparator8,
            this.btnGuardar2,
            this.toolStripSeparator9,
            this.btnValidarDatos});
            this.toolStripBarraImportacion.Location = new System.Drawing.Point(3, 16);
            this.toolStripBarraImportacion.Name = "toolStripBarraImportacion";
            this.toolStripBarraImportacion.Size = new System.Drawing.Size(1161, 25);
            this.toolStripBarraImportacion.TabIndex = 0;
            this.toolStripBarraImportacion.Text = "toolStrip1";
            // 
            // btnExportarPlantilla
            // 
            this.btnExportarPlantilla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPlantilla.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPlantilla.Image")));
            this.btnExportarPlantilla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPlantilla.Name = "btnExportarPlantilla";
            this.btnExportarPlantilla.Size = new System.Drawing.Size(23, 22);
            this.btnExportarPlantilla.Text = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.ToolTipText = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.Click += new System.EventHandler(this.btnExportarPlantilla_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(23, 22);
            this.btnAbrir.Text = "Abrir plantilla de pedido";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Cargar información de plantilla";
            this.btnEjecutar.ToolTipText = "Cargar información de plantilla";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar2
            // 
            this.btnGuardar2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar2.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar2.Image")));
            this.btnGuardar2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar2.Name = "btnGuardar2";
            this.btnGuardar2.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar2.Text = "Subir plantilla ";
            this.btnGuardar2.Click += new System.EventHandler(this.btnGuardar2_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(23, 22);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // cboHojas
            // 
            this.cboHojas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(112, 48);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(1043, 21);
            this.cboHojas.TabIndex = 0;
            this.cboHojas.SelectedIndexChanged += new System.EventHandler(this.cboHojas_SelectedIndexChanged);
            // 
            // tmValidacion
            // 
            this.tmValidacion.Tick += new System.EventHandler(this.tmValidacion_Tick);
            // 
            // FrmGenerarPedidosCEDIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.frameImportacion);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGenerarPedidosCEDIS";
            this.Text = "Registro de Pedidos a CEDIS";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGenerarPedidosCEDIS_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.frameImportacion.ResumeLayout(false);
            this.frameImportacion.PerformLayout();
            this.toolStripBarraImportacion.ResumeLayout(false);
            this.toolStripBarraImportacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnGenerarPaqueteDeDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.GroupBox frameImportacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStrip toolStripBarraImportacion;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnGuardar2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmValidacion;
        private System.Windows.Forms.ToolStripButton btnExportarPlantilla;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboTipoPedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnGenerarPCM;
    }
}