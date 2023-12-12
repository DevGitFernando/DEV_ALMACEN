namespace DllCompras.Consultas
{
    partial class FrmListadoPorClaves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoPorClaves));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblImpteTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grdOrdenCompras = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenCompras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblContPaquete = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblIdClaveSSA = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkTodasLasFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.FrameReporte = new System.Windows.Forms.GroupBox();
            this.rdoDetallado = new System.Windows.Forms.RadioButton();
            this.rdoConcentrado = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.btnExportarExcel,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(734, 25);
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
            this.groupBox2.Controls.Add(this.lblImpteTotal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.grdOrdenCompras);
            this.groupBox2.Location = new System.Drawing.Point(10, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 292);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado Proveedores";
            // 
            // lblImpteTotal
            // 
            this.lblImpteTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImpteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpteTotal.Location = new System.Drawing.Point(544, 266);
            this.lblImpteTotal.Name = "lblImpteTotal";
            this.lblImpteTotal.Size = new System.Drawing.Size(135, 20);
            this.lblImpteTotal.TabIndex = 30;
            this.lblImpteTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(432, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Importe Total :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdOrdenCompras
            // 
            this.grdOrdenCompras.AccessibleDescription = "grdOrdenCompras, Sheet1, Row 0, Column 0, ";
            this.grdOrdenCompras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenCompras.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdOrdenCompras.Location = new System.Drawing.Point(17, 16);
            this.grdOrdenCompras.Name = "grdOrdenCompras";
            this.grdOrdenCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenCompras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenCompras_Sheet1});
            this.grdOrdenCompras.Size = new System.Drawing.Size(682, 247);
            this.grdOrdenCompras.TabIndex = 0;
            this.grdOrdenCompras.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdOrdenCompras_CellDoubleClick);
            // 
            // grdOrdenCompras_Sheet1
            // 
            this.grdOrdenCompras_Sheet1.Reset();
            this.grdOrdenCompras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenCompras_Sheet1.ColumnCount = 4;
            this.grdOrdenCompras_Sheet1.RowCount = 12;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Proveedor";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cajas";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Importe";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 10;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Label = "Id Proveedor";
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Width = 80F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 100;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Width = 325F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Label = "Cajas";
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Width = 100F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 100000000D;
            numberCellType2.MinimumValue = -100000000D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Label = "Importe";
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Width = 120F;
            this.grdOrdenCompras_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblContPaquete);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblIdClaveSSA);
            this.groupBox1.Controls.Add(this.lblPresentacion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(10, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave";
            // 
            // lblContPaquete
            // 
            this.lblContPaquete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContPaquete.Location = new System.Drawing.Point(630, 21);
            this.lblContPaquete.Name = "lblContPaquete";
            this.lblContPaquete.Size = new System.Drawing.Size(69, 20);
            this.lblContPaquete.TabIndex = 24;
            this.lblContPaquete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(563, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Contenido : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(182, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Clave Interna :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdClaveSSA
            // 
            this.lblIdClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdClaveSSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdClaveSSA.Location = new System.Drawing.Point(263, 21);
            this.lblIdClaveSSA.Name = "lblIdClaveSSA";
            this.lblIdClaveSSA.Size = new System.Drawing.Size(80, 20);
            this.lblIdClaveSSA.TabIndex = 28;
            this.lblIdClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(438, 21);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(119, 20);
            this.lblPresentacion.TabIndex = 22;
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(352, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Presentación : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(73, 21);
            this.txtClaveSSA.MaxLength = 30;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(104, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDescripcion.Location = new System.Drawing.Point(73, 44);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(626, 49);
            this.lblDescripcion.TabIndex = 26;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.chkTodasLasFechas);
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(417, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(190, 75);
            this.FrameFechas.TabIndex = 1;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Fechas";
            // 
            // chkTodasLasFechas
            // 
            this.chkTodasLasFechas.Location = new System.Drawing.Point(61, -1);
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
            this.dtpFechaFinal.Location = new System.Drawing.Point(64, 47);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 25);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(64, 22);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2012, 2, 28, 0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cboEmpresas);
            this.groupBox3.Controls.Add(this.cboEdo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(10, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(401, 75);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Estados";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(73, 18);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(299, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(73, 43);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(299, 21);
            this.cboEdo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameReporte
            // 
            this.FrameReporte.Controls.Add(this.rdoDetallado);
            this.FrameReporte.Controls.Add(this.rdoConcentrado);
            this.FrameReporte.Location = new System.Drawing.Point(613, 28);
            this.FrameReporte.Name = "FrameReporte";
            this.FrameReporte.Size = new System.Drawing.Size(111, 74);
            this.FrameReporte.TabIndex = 18;
            this.FrameReporte.TabStop = false;
            this.FrameReporte.Text = "Tipo de Reporte";
            // 
            // rdoDetallado
            // 
            this.rdoDetallado.Location = new System.Drawing.Point(11, 44);
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
            this.rdoConcentrado.Location = new System.Drawing.Point(11, 20);
            this.rdoConcentrado.Name = "rdoConcentrado";
            this.rdoConcentrado.Size = new System.Drawing.Size(87, 15);
            this.rdoConcentrado.TabIndex = 0;
            this.rdoConcentrado.TabStop = true;
            this.rdoConcentrado.Text = "Concentrado";
            this.rdoConcentrado.UseVisualStyleBackColor = true;
            // 
            // FrmListadoPorClaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 510);
            this.Controls.Add(this.FrameReporte);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoPorClaves";
            this.Text = "Listado por Claves";
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
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.CheckBox chkTodasLasFechas;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIdClaveSSA;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblContPaquete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblImpteTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox FrameReporte;
        private System.Windows.Forms.RadioButton rdoDetallado;
        private System.Windows.Forms.RadioButton rdoConcentrado;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}