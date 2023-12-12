namespace DllCompras.OrdenesDeCompra
{
    partial class FrmListadoOrdenesComprasCodigoEAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoOrdenesComprasCodigoEAN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblImpteTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grdOrdenCompras = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenCompras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNomPersonal = new System.Windows.Forms.Label();
            this.txtProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkTodasLasFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresa = new SC_ControlsCS.scComboBoxExt();
            this.cboEstado = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoOtrosStatus = new System.Windows.Forms.RadioButton();
            this.rdoNoColocada = new System.Windows.Forms.RadioButton();
            this.rdoColocada = new System.Windows.Forms.RadioButton();
            this.FrameReporte = new System.Windows.Forms.GroupBox();
            this.rdoDetallado = new System.Windows.Forms.RadioButton();
            this.rdoConcentrado = new System.Windows.Forms.RadioButton();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.FrameReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 16;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblImpteTotal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.grdOrdenCompras);
            this.groupBox2.Location = new System.Drawing.Point(10, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1165, 475);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Órdenes de Compra";
            // 
            // lblImpteTotal
            // 
            this.lblImpteTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImpteTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImpteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpteTotal.Location = new System.Drawing.Point(1024, 447);
            this.lblImpteTotal.Name = "lblImpteTotal";
            this.lblImpteTotal.Size = new System.Drawing.Size(135, 20);
            this.lblImpteTotal.TabIndex = 32;
            this.lblImpteTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(916, 448);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 16);
            this.label11.TabIndex = 31;
            this.label11.Text = "Importe Total :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdOrdenCompras
            // 
            this.grdOrdenCompras.AccessibleDescription = "grdOrdenCompras, Sheet1, Row 0, Column 0, ";
            this.grdOrdenCompras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdOrdenCompras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenCompras.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdOrdenCompras.Location = new System.Drawing.Point(10, 16);
            this.grdOrdenCompras.Name = "grdOrdenCompras";
            this.grdOrdenCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenCompras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenCompras_Sheet1});
            this.grdOrdenCompras.Size = new System.Drawing.Size(1150, 427);
            this.grdOrdenCompras.TabIndex = 0;
            this.grdOrdenCompras.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdOrdenCompras_CellDoubleClick);
            // 
            // grdOrdenCompras_Sheet1
            // 
            this.grdOrdenCompras_Sheet1.Reset();
            this.grdOrdenCompras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenCompras_Sheet1.ColumnCount = 11;
            this.grdOrdenCompras_Sheet1.RowCount = 12;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 5).Locked = true;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Cells.Get(1, 7).Locked = false;
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEmpresa";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Empresa";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "IdEstado";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Estado";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Folio";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Proveedor";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Observaciones";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Status";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Importe";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).CellType = textCellType17;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Label = "IdEmpresa";
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Visible = false;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).CellType = textCellType18;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Label = "Empresa";
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Visible = false;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).CellType = textCellType19;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Label = "IdEstado";
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Visible = false;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).CellType = textCellType20;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Label = "Estado";
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Width = 142F;
            textCellType21.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType21.MaxLength = 10;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).CellType = textCellType21;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Label = "Folio";
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Width = 80F;
            textCellType22.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType22.MaxLength = 100;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).CellType = textCellType22;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Label = "Proveedor";
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Width = 300F;
            this.grdOrdenCompras_Sheet1.Columns.Get(6).Label = "Observaciones";
            this.grdOrdenCompras_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(6).Width = 340F;
            this.grdOrdenCompras_Sheet1.Columns.Get(7).CellType = textCellType23;
            this.grdOrdenCompras_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(7).Label = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.Columns.Get(7).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(7).Width = 70F;
            this.grdOrdenCompras_Sheet1.Columns.Get(8).CellType = textCellType24;
            this.grdOrdenCompras_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(8).Label = "Status";
            this.grdOrdenCompras_Sheet1.Columns.Get(8).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(8).Width = 160F;
            this.grdOrdenCompras_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(9).Label = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.Columns.Get(9).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(9).Width = 70F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000000D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(10).CellType = numberCellType3;
            this.grdOrdenCompras_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(10).Label = "Importe";
            this.grdOrdenCompras_Sheet1.Columns.Get(10).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(10).Width = 120F;
            this.grdOrdenCompras_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtIdPersonal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblNomPersonal);
            this.groupBox1.Controls.Add(this.txtProveedor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblNomProv);
            this.groupBox1.Location = new System.Drawing.Point(10, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 75);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Búsqueda";
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(76, 45);
            this.txtIdPersonal.MaxLength = 8;
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(80, 20);
            this.txtIdPersonal.TabIndex = 28;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPersonal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPersonal_KeyDown);
            this.txtIdPersonal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonal_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "Comprador :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomPersonal
            // 
            this.lblNomPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomPersonal.Location = new System.Drawing.Point(162, 45);
            this.lblNomPersonal.Name = "lblNomPersonal";
            this.lblNomPersonal.Size = new System.Drawing.Size(490, 20);
            this.lblNomPersonal.TabIndex = 29;
            this.lblNomPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProveedor
            // 
            this.txtProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProveedor.Decimales = 2;
            this.txtProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtProveedor.Location = new System.Drawing.Point(76, 21);
            this.txtProveedor.MaxLength = 8;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PermitirApostrofo = false;
            this.txtProveedor.PermitirNegativos = false;
            this.txtProveedor.Size = new System.Drawing.Size(80, 20);
            this.txtProveedor.TabIndex = 0;
            this.txtProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyDown);
            this.txtProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtProveedor_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Proveedor :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomProv
            // 
            this.lblNomProv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(162, 21);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(490, 20);
            this.lblNomProv.TabIndex = 26;
            this.lblNomProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFechas.Controls.Add(this.chkTodasLasFechas);
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(674, 81);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(202, 75);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Fechas";
            // 
            // chkTodasLasFechas
            // 
            this.chkTodasLasFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTodasLasFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodasLasFechas.Location = new System.Drawing.Point(89, 0);
            this.chkTodasLasFechas.Name = "chkTodasLasFechas";
            this.chkTodasLasFechas.Size = new System.Drawing.Size(107, 16);
            this.chkTodasLasFechas.TabIndex = 20;
            this.chkTodasLasFechas.Text = "Todas las fechas";
            this.chkTodasLasFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodasLasFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(77, 48);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(34, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(77, 22);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cboEmpresa);
            this.groupBox3.Controls.Add(this.cboEstado);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(10, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1165, 50);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Empresa  --  Estados";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(47, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresa
            // 
            this.cboEmpresa.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresa.Data = "";
            this.cboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresa.Filtro = " 1 = 1";
            this.cboEmpresa.ListaItemsBusqueda = 20;
            this.cboEmpresa.Location = new System.Drawing.Point(105, 19);
            this.cboEmpresa.MostrarToolTip = false;
            this.cboEmpresa.Name = "cboEmpresa";
            this.cboEmpresa.Size = new System.Drawing.Size(463, 21);
            this.cboEmpresa.TabIndex = 0;
            this.cboEmpresa.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEstado
            // 
            this.cboEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstado.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstado.Data = "";
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Filtro = " 1 = 1";
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.ListaItemsBusqueda = 20;
            this.cboEstado.Location = new System.Drawing.Point(655, 19);
            this.cboEstado.MostrarToolTip = false;
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(463, 21);
            this.cboEstado.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(603, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rdoOtrosStatus);
            this.groupBox4.Controls.Add(this.rdoNoColocada);
            this.groupBox4.Controls.Add(this.rdoColocada);
            this.groupBox4.Location = new System.Drawing.Point(881, 81);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 75);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status OC";
            // 
            // rdoOtrosStatus
            // 
            this.rdoOtrosStatus.Location = new System.Drawing.Point(21, 53);
            this.rdoOtrosStatus.Name = "rdoOtrosStatus";
            this.rdoOtrosStatus.Size = new System.Drawing.Size(102, 15);
            this.rdoOtrosStatus.TabIndex = 4;
            this.rdoOtrosStatus.TabStop = true;
            this.rdoOtrosStatus.Text = "Otros";
            this.rdoOtrosStatus.UseVisualStyleBackColor = true;
            // 
            // rdoNoColocada
            // 
            this.rdoNoColocada.Location = new System.Drawing.Point(21, 34);
            this.rdoNoColocada.Name = "rdoNoColocada";
            this.rdoNoColocada.Size = new System.Drawing.Size(102, 15);
            this.rdoNoColocada.TabIndex = 3;
            this.rdoNoColocada.TabStop = true;
            this.rdoNoColocada.Text = "No Colocada";
            this.rdoNoColocada.UseVisualStyleBackColor = true;
            // 
            // rdoColocada
            // 
            this.rdoColocada.Location = new System.Drawing.Point(21, 16);
            this.rdoColocada.Name = "rdoColocada";
            this.rdoColocada.Size = new System.Drawing.Size(102, 15);
            this.rdoColocada.TabIndex = 2;
            this.rdoColocada.TabStop = true;
            this.rdoColocada.Text = "Colocada";
            this.rdoColocada.UseVisualStyleBackColor = true;
            // 
            // FrameReporte
            // 
            this.FrameReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameReporte.Controls.Add(this.rdoDetallado);
            this.FrameReporte.Controls.Add(this.rdoConcentrado);
            this.FrameReporte.Location = new System.Drawing.Point(1031, 81);
            this.FrameReporte.Name = "FrameReporte";
            this.FrameReporte.Size = new System.Drawing.Size(144, 75);
            this.FrameReporte.TabIndex = 17;
            this.FrameReporte.TabStop = false;
            this.FrameReporte.Text = "Tipo de Reporte";
            // 
            // rdoDetallado
            // 
            this.rdoDetallado.Location = new System.Drawing.Point(29, 44);
            this.rdoDetallado.Name = "rdoDetallado";
            this.rdoDetallado.Size = new System.Drawing.Size(80, 15);
            this.rdoDetallado.TabIndex = 1;
            this.rdoDetallado.TabStop = true;
            this.rdoDetallado.Text = "Detallado";
            this.rdoDetallado.UseVisualStyleBackColor = true;
            // 
            // rdoConcentrado
            // 
            this.rdoConcentrado.Checked = true;
            this.rdoConcentrado.Location = new System.Drawing.Point(29, 20);
            this.rdoConcentrado.Name = "rdoConcentrado";
            this.rdoConcentrado.Size = new System.Drawing.Size(87, 15);
            this.rdoConcentrado.TabIndex = 0;
            this.rdoConcentrado.TabStop = true;
            this.rdoConcentrado.Text = "Concentrado";
            this.rdoConcentrado.UseVisualStyleBackColor = true;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 637);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1184, 24);
            this.lblMensajes.TabIndex = 18;
            this.lblMensajes.Text = " Doble clic sobre el folio para visualizar los detalles";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListadoOrdenesComprasCodigoEAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameReporte);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmListadoOrdenesComprasCodigoEAN";
            this.Text = "Listado de Órdenes de Compra - Código EAN";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.FrameReporte.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdOrdenCompras;
        private FarPoint.Win.Spread.SheetView grdOrdenCompras_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNomProv;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.CheckBox chkTodasLasFechas;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNomPersonal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresa;
        private SC_ControlsCS.scComboBoxExt cboEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblImpteTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoNoColocada;
        private System.Windows.Forms.RadioButton rdoColocada;
        private System.Windows.Forms.RadioButton rdoOtrosStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameReporte;
        private System.Windows.Forms.RadioButton rdoDetallado;
        private System.Windows.Forms.RadioButton rdoConcentrado;
        private System.Windows.Forms.Label lblMensajes;
    }
}