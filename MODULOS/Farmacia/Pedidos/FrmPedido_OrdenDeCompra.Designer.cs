namespace Farmacia.Pedidos
{
    partial class FrmPedido_OrdenDeCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedido_OrdenDeCompra));
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipoPedido = new SC_ControlsCS.scComboBoxExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCalcularPedidosPorPCM = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.tmPedidos = new System.Windows.Forms.Timer(this.components);
            this.frameImportacion = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStripBarraImportacion = new System.Windows.Forms.ToolStrip();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.frameImportacion.SuspendLayout();
            this.toolStripBarraImportacion.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboTipoPedido);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1141, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 15);
            this.label4.TabIndex = 39;
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
            this.cboTipoPedido.Location = new System.Drawing.Point(136, 84);
            this.cboTipoPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTipoPedido.MostrarToolTip = false;
            this.cboTipoPedido.Name = "cboTipoPedido";
            this.cboTipoPedido.Size = new System.Drawing.Size(420, 24);
            this.cboTipoPedido.TabIndex = 38;
            this.cboTipoPedido.SelectedIndexChanged += new System.EventHandler(this.cboTipoPedido_SelectedIndexChanged);
            this.cboTipoPedido.Validating += new System.ComponentModel.CancelEventHandler(this.cboTipoPedido_Validating);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(136, 52);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtObservaciones.MaxLength = 200;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(991, 24);
            this.txtObservaciones.TabIndex = 17;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Observaciones :";
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(136, 18);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolio.MaxLength = 6;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(81, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Folio :";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(1001, 16);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(125, 22);
            this.dtpFecha.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(928, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Fecha :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnCalcularPedidosPorPCM});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1167, 27);
            this.toolStripBarraMenu.TabIndex = 8;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(29, 24);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(29, 24);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnCalcularPedidosPorPCM
            // 
            this.btnCalcularPedidosPorPCM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCalcularPedidosPorPCM.Image = ((System.Drawing.Image)(resources.GetObject("btnCalcularPedidosPorPCM.Image")));
            this.btnCalcularPedidosPorPCM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCalcularPedidosPorPCM.Name = "btnCalcularPedidosPorPCM";
            this.btnCalcularPedidosPorPCM.Size = new System.Drawing.Size(29, 24);
            this.btnCalcularPedidosPorPCM.Text = "Calcular pedidos por PCM";
            this.btnCalcularPedidosPorPCM.Click += new System.EventHandler(this.btnCalcularPedidosPorPCM_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(13, 260);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1141, 388);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Captura de datos";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(13, 20);
            this.grdProductos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1113, 356);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.FloatingFocusOnly;
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
            this.grdProductos_Sheet1.ColumnCount = 7;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 0).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Formula = "IF((RC[-2]/RC[-1])<=0.5,1,(RC[-2]/RC[-1]))";
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(2, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(2, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(3, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(3, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(4, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(4, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(5, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(5, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(6, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(6, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(7, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(7, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(8, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(8, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(9, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(9, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(10, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(10, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(11, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(11, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(12, 6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Cells.Get(12, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Id Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Presentación";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Contenido";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Piezas";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cajas";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 28F;
            textCellType11.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType11.MaxLength = 30;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType11;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Id Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 90F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType13;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 380F;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = textCellType14;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Presentación";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 100F;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = textCellType15;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Contenido";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = -10000000D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType5;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Piezas";
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 90F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.MaximumValue = 10000000D;
            numberCellType6.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType6;
            this.grdProductos_Sheet1.Columns.Get(6).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Cajas";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 90F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPersonal);
            this.groupBox3.Controls.Add(this.txtIdPersonal);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(13, 649);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1141, 63);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos usuarios";
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(203, 22);
            this.lblPersonal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(925, 26);
            this.lblPersonal.TabIndex = 9;
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(93, 22);
            this.txtIdPersonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(100, 22);
            this.txtIdPersonal.TabIndex = 0;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(19, 27);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "Usuario :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmPedidos
            // 
            this.tmPedidos.Tick += new System.EventHandler(this.tmPedidos_Tick);
            // 
            // frameImportacion
            // 
            this.frameImportacion.Controls.Add(this.label7);
            this.frameImportacion.Controls.Add(this.toolStripBarraImportacion);
            this.frameImportacion.Controls.Add(this.cboHojas);
            this.frameImportacion.Location = new System.Drawing.Point(13, 158);
            this.frameImportacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frameImportacion.Name = "frameImportacion";
            this.frameImportacion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frameImportacion.Size = new System.Drawing.Size(1141, 97);
            this.frameImportacion.TabIndex = 13;
            this.frameImportacion.TabStop = false;
            this.frameImportacion.Text = "Menú de Importación";
            this.frameImportacion.Enter += new System.EventHandler(this.frameImportacion_Enter);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 64);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "Seleccionar Hoja :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraImportacion
            // 
            this.toolStripBarraImportacion.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraImportacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbrir,
            this.toolStripSeparator7,
            this.btnEjecutar,
            this.toolStripSeparator8,
            this.btnGuardar2,
            this.toolStripSeparator9,
            this.btnValidarDatos});
            this.toolStripBarraImportacion.Location = new System.Drawing.Point(4, 19);
            this.toolStripBarraImportacion.Name = "toolStripBarraImportacion";
            this.toolStripBarraImportacion.Size = new System.Drawing.Size(1133, 27);
            this.toolStripBarraImportacion.TabIndex = 1;
            this.toolStripBarraImportacion.Text = "toolStrip1";
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(29, 24);
            this.btnAbrir.Text = "Abrir plantilla de pedido";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(29, 24);
            this.btnEjecutar.Text = "Cargar información de plantilla";
            this.btnEjecutar.ToolTipText = "Cargar información de plantilla";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar2
            // 
            this.btnGuardar2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar2.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar2.Image")));
            this.btnGuardar2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar2.Name = "btnGuardar2";
            this.btnGuardar2.Size = new System.Drawing.Size(29, 24);
            this.btnGuardar2.Text = "Subir plantilla ";
            this.btnGuardar2.Click += new System.EventHandler(this.btnGuardar2_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 27);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(29, 24);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // cboHojas
            // 
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(149, 60);
            this.cboHojas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(977, 24);
            this.cboHojas.TabIndex = 0;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(163, 738);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Size = new System.Drawing.Size(803, 126);
            this.FrameProceso.TabIndex = 15;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 28);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(765, 79);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // tmValidacion
            // 
            this.tmValidacion.Tick += new System.EventHandler(this.tmValidacion_Tick);
            // 
            // FrmPedido_OrdenDeCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 892);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.frameImportacion);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmPedido_OrdenDeCompra";
            this.ShowIcon = false;
            this.Text = "Generar Orden de Compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPedido_OrdenDeCompra_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.frameImportacion.ResumeLayout(false);
            this.frameImportacion.PerformLayout();
            this.toolStripBarraImportacion.ResumeLayout(false);
            this.toolStripBarraImportacion.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Timer tmPedidos;
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
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboTipoPedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCalcularPedidosPorPCM;
    }
}