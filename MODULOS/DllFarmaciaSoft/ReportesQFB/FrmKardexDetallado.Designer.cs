namespace DllFarmaciaSoft.ReportesQFB
{
    partial class FrmKardexDetallado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexDetallado));
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameTipoSales = new System.Windows.Forms.GroupBox();
            this.rdoClavesLibres = new System.Windows.Forms.RadioButton();
            this.rdoAntibioticos = new System.Windows.Forms.RadioButton();
            this.rdoControlados = new System.Windows.Forms.RadioButton();
            this.FrameTipoReporte = new System.Windows.Forms.GroupBox();
            this.rdoTodasClaves = new System.Windows.Forms.RadioButton();
            this.rdoPorProducto = new System.Windows.Forms.RadioButton();
            this.rdoPorClave = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBuscarClave = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdMovtos = new FarPoint.Win.Spread.FpSpread();
            this.grdMovtos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameFecha = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoSales.SuspendLayout();
            this.FrameTipoReporte.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).BeginInit();
            this.FrameFecha.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1379, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 2);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 2);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 2);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(54, 55);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameTipoSales
            // 
            this.FrameTipoSales.Controls.Add(this.rdoClavesLibres);
            this.FrameTipoSales.Controls.Add(this.rdoAntibioticos);
            this.FrameTipoSales.Controls.Add(this.rdoControlados);
            this.FrameTipoSales.Location = new System.Drawing.Point(16, 66);
            this.FrameTipoSales.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoSales.Name = "FrameTipoSales";
            this.FrameTipoSales.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoSales.Size = new System.Drawing.Size(335, 63);
            this.FrameTipoSales.TabIndex = 1;
            this.FrameTipoSales.TabStop = false;
            this.FrameTipoSales.Text = "Tipo Insumos";
            // 
            // rdoClavesLibres
            // 
            this.rdoClavesLibres.Location = new System.Drawing.Point(135, 10);
            this.rdoClavesLibres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoClavesLibres.Name = "rdoClavesLibres";
            this.rdoClavesLibres.Size = new System.Drawing.Size(117, 18);
            this.rdoClavesLibres.TabIndex = 2;
            this.rdoClavesLibres.TabStop = true;
            this.rdoClavesLibres.Text = "Claves libres";
            this.rdoClavesLibres.UseVisualStyleBackColor = true;
            this.rdoClavesLibres.Visible = false;
            // 
            // rdoAntibioticos
            // 
            this.rdoAntibioticos.Location = new System.Drawing.Point(33, 25);
            this.rdoAntibioticos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoAntibioticos.Name = "rdoAntibioticos";
            this.rdoAntibioticos.Size = new System.Drawing.Size(109, 18);
            this.rdoAntibioticos.TabIndex = 0;
            this.rdoAntibioticos.TabStop = true;
            this.rdoAntibioticos.Text = "Antibióticos";
            this.rdoAntibioticos.UseVisualStyleBackColor = true;
            // 
            // rdoControlados
            // 
            this.rdoControlados.Location = new System.Drawing.Point(184, 25);
            this.rdoControlados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoControlados.Name = "rdoControlados";
            this.rdoControlados.Size = new System.Drawing.Size(117, 18);
            this.rdoControlados.TabIndex = 1;
            this.rdoControlados.TabStop = true;
            this.rdoControlados.Text = "Controlados";
            this.rdoControlados.UseVisualStyleBackColor = true;
            // 
            // FrameTipoReporte
            // 
            this.FrameTipoReporte.Controls.Add(this.rdoTodasClaves);
            this.FrameTipoReporte.Controls.Add(this.rdoPorProducto);
            this.FrameTipoReporte.Controls.Add(this.rdoPorClave);
            this.FrameTipoReporte.Location = new System.Drawing.Point(360, 66);
            this.FrameTipoReporte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoReporte.Name = "FrameTipoReporte";
            this.FrameTipoReporte.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoReporte.Size = new System.Drawing.Size(488, 63);
            this.FrameTipoReporte.TabIndex = 2;
            this.FrameTipoReporte.TabStop = false;
            this.FrameTipoReporte.Text = "Tipo Reporte";
            // 
            // rdoTodasClaves
            // 
            this.rdoTodasClaves.Checked = true;
            this.rdoTodasClaves.Location = new System.Drawing.Point(39, 25);
            this.rdoTodasClaves.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoTodasClaves.Name = "rdoTodasClaves";
            this.rdoTodasClaves.Size = new System.Drawing.Size(120, 18);
            this.rdoTodasClaves.TabIndex = 0;
            this.rdoTodasClaves.TabStop = true;
            this.rdoTodasClaves.Text = "Todas Claves";
            this.rdoTodasClaves.UseVisualStyleBackColor = true;
            this.rdoTodasClaves.CheckedChanged += new System.EventHandler(this.rdoTodasClaves_CheckedChanged);
            // 
            // rdoPorProducto
            // 
            this.rdoPorProducto.Location = new System.Drawing.Point(331, 23);
            this.rdoPorProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoPorProducto.Name = "rdoPorProducto";
            this.rdoPorProducto.Size = new System.Drawing.Size(120, 21);
            this.rdoPorProducto.TabIndex = 2;
            this.rdoPorProducto.Text = "Por Producto";
            this.rdoPorProducto.UseVisualStyleBackColor = true;
            this.rdoPorProducto.CheckedChanged += new System.EventHandler(this.rdoPorProducto_CheckedChanged);
            // 
            // rdoPorClave
            // 
            this.rdoPorClave.Location = new System.Drawing.Point(195, 25);
            this.rdoPorClave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoPorClave.Name = "rdoPorClave";
            this.rdoPorClave.Size = new System.Drawing.Size(120, 18);
            this.rdoPorClave.TabIndex = 1;
            this.rdoPorClave.Text = "Por Clave";
            this.rdoPorClave.UseVisualStyleBackColor = true;
            this.rdoPorClave.CheckedChanged += new System.EventHandler(this.rdoPorClave_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkBuscarClave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(16, 133);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1351, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Producto";
            // 
            // chkBuscarClave
            // 
            this.chkBuscarClave.Location = new System.Drawing.Point(433, 22);
            this.chkBuscarClave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkBuscarClave.Name = "chkBuscarClave";
            this.chkBuscarClave.Size = new System.Drawing.Size(183, 21);
            this.chkBuscarClave.TabIndex = 1;
            this.chkBuscarClave.Text = "Consulta por Clave SSA";
            this.chkBuscarClave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Clave SSA -- Producto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(87, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 132);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Clave :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(228, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(536, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(188, 50);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(1143, 25);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "label4";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(188, 22);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(233, 22);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Clave SSA -- Producto :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdMovtos);
            this.groupBox2.Location = new System.Drawing.Point(16, 218);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1351, 516);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detallado de Movimientos";
            // 
            // grdMovtos
            // 
            this.grdMovtos.AccessibleDescription = "grdMovtos, Sheet1, Row 0, Column 0, ";
            this.grdMovtos.AllowUndo = false;
            this.grdMovtos.AllowUserZoom = false;
            this.grdMovtos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMovtos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMovtos.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdMovtos.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdMovtos.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdMovtos.HorizontalScrollBar.TabIndex = 2;
            this.grdMovtos.Location = new System.Drawing.Point(12, 23);
            this.grdMovtos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdMovtos.Name = "grdMovtos";
            this.grdMovtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMovtos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMovtos_Sheet1});
            this.grdMovtos.Size = new System.Drawing.Size(1324, 481);
            this.grdMovtos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdMovtos.TabIndex = 0;
            this.grdMovtos.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdMovtos.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdMovtos.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdMovtos.VerticalScrollBar.TabIndex = 3;
            // 
            // grdMovtos_Sheet1
            // 
            this.grdMovtos_Sheet1.Reset();
            this.grdMovtos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMovtos_Sheet1.ColumnCount = 8;
            this.grdMovtos_Sheet1.RowCount = 5;
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción movimiento";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave SSA";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción comercial";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Entrada";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Salida";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Existencia";
            this.grdMovtos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdMovtos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdMovtos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Label = "Fecha";
            this.grdMovtos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Width = 75F;
            this.grdMovtos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdMovtos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdMovtos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Width = 90F;
            this.grdMovtos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMovtos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(2).Label = "Descripción movimiento";
            this.grdMovtos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(2).Width = 220F;
            this.grdMovtos_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdMovtos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Label = "Clave SSA";
            this.grdMovtos_Sheet1.Columns.Get(3).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Width = 105F;
            this.grdMovtos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(4).Label = "Descripción comercial";
            this.grdMovtos_Sheet1.Columns.Get(4).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(4).Width = 220F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -1000D;
            numberCellType1.NegativeRed = true;
            numberCellType1.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.grdMovtos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(5).Label = "Entrada";
            this.grdMovtos_Sheet1.Columns.Get(5).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -1000D;
            numberCellType2.NegativeRed = true;
            numberCellType2.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.grdMovtos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Label = "Salida";
            this.grdMovtos_Sheet1.Columns.Get(6).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = -1000D;
            numberCellType3.NegativeRed = true;
            numberCellType3.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(7).CellType = numberCellType3;
            this.grdMovtos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Label = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(7).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMovtos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdMovtos_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFecha
            // 
            this.FrameFecha.Controls.Add(this.dtpFechaFinal);
            this.FrameFecha.Controls.Add(this.label2);
            this.FrameFecha.Controls.Add(this.label4);
            this.FrameFecha.Controls.Add(this.dtpFechaInicial);
            this.FrameFecha.Location = new System.Drawing.Point(856, 66);
            this.FrameFecha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameFecha.Name = "FrameFecha";
            this.FrameFecha.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameFecha.Size = new System.Drawing.Size(511, 63);
            this.FrameFecha.TabIndex = 3;
            this.FrameFecha.TabStop = false;
            this.FrameFecha.Text = "Periodo fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(325, 20);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(276, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(56, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(119, 20);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrmKardexDetallado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 752);
            this.Controls.Add(this.FrameFecha);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameTipoReporte);
            this.Controls.Add(this.FrameTipoSales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmKardexDetallado";
            this.ShowIcon = false;
            this.Text = "Kardex Controlados y Antibióticos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSalesControladosAntibioticos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoSales.ResumeLayout(false);
            this.FrameTipoReporte.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).EndInit();
            this.FrameFecha.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameTipoSales;
        private System.Windows.Forms.RadioButton rdoAntibioticos;
        private System.Windows.Forms.RadioButton rdoControlados;
        private System.Windows.Forms.GroupBox FrameTipoReporte;
        private System.Windows.Forms.RadioButton rdoTodasClaves;
        private System.Windows.Forms.RadioButton rdoPorProducto;
        private System.Windows.Forms.RadioButton rdoPorClave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdMovtos;
        private FarPoint.Win.Spread.SheetView grdMovtos_Sheet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBuscarClave;
        private System.Windows.Forms.GroupBox FrameFecha;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.RadioButton rdoClavesLibres;
    }
}