namespace OficinaCentral.Inventario
{
    partial class FrmKardexProductoGral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexProductoGral));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTipoDeReporte = new SC_ControlsCS.scComboBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescripcionSSA = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdMovtos = new FarPoint.Win.Spread.FpSpread();
            this.grdMovtos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(944, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
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
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTipoDeReporte);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCodigoEAN);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblDescripcionSSA);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(272, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 157);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del producto";
            // 
            // cboTipoDeReporte
            // 
            this.cboTipoDeReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoDeReporte.Data = "";
            this.cboTipoDeReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDeReporte.Filtro = " 1 = 1";
            this.cboTipoDeReporte.FormattingEnabled = true;
            this.cboTipoDeReporte.ListaItemsBusqueda = 20;
            this.cboTipoDeReporte.Location = new System.Drawing.Point(377, 123);
            this.cboTipoDeReporte.MostrarToolTip = false;
            this.cboTipoDeReporte.Name = "cboTipoDeReporte";
            this.cboTipoDeReporte.Size = new System.Drawing.Size(152, 21);
            this.cboTipoDeReporte.TabIndex = 3;
            this.cboTipoDeReporte.SelectedIndexChanged += new System.EventHandler(this.cboTipoDeReporte_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(278, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Tipo de reporte :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(124, 123);
            this.txtCodigoEAN.MaxLength = 15;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(143, 20);
            this.txtCodigoEAN.TabIndex = 2;
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(45, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Codigo EAN :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSSA
            // 
            this.lblDescripcionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSSA.Location = new System.Drawing.Point(124, 16);
            this.lblDescripcionSSA.Name = "lblDescripcionSSA";
            this.lblDescripcionSSA.Size = new System.Drawing.Size(533, 45);
            this.lblDescripcionSSA.TabIndex = 14;
            this.lblDescripcionSSA.Text = "label4";
            this.lblDescripcionSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(17, 41);
            this.txtClaveSSA.MaxLength = 8;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(100, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Clave SSA :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(124, 72);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(533, 42);
            this.lblDescripcion.TabIndex = 11;
            this.lblDescripcion.Text = "label4";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(17, 94);
            this.txtCodigo.MaxLength = 8;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Codigo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdMovtos);
            this.groupBox2.Location = new System.Drawing.Point(10, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(925, 382);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detallado de movimientos";
            // 
            // grdMovtos
            // 
            this.grdMovtos.AccessibleDescription = "grdMovtos, Sheet1, Row 0, Column 0, ";
            this.grdMovtos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMovtos.Location = new System.Drawing.Point(6, 19);
            this.grdMovtos.Name = "grdMovtos";
            this.grdMovtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMovtos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMovtos_Sheet1});
            this.grdMovtos.Size = new System.Drawing.Size(913, 356);
            this.grdMovtos.TabIndex = 0;
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
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Entrada";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Salida";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Existencia";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Costo / Precio";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdMovtos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdMovtos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Label = "Fecha";
            this.grdMovtos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Width = 85F;
            this.grdMovtos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdMovtos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdMovtos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Width = 110F;
            this.grdMovtos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMovtos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdMovtos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(2).Width = 310F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -1000D;
            numberCellType1.NegativeRed = true;
            numberCellType1.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdMovtos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Label = "Entrada";
            this.grdMovtos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -1000D;
            numberCellType2.NegativeRed = true;
            numberCellType2.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdMovtos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(4).Label = "Salida";
            this.grdMovtos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = -1000D;
            numberCellType3.NegativeRed = true;
            numberCellType3.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdMovtos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(5).Label = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.NegativeRed = true;
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowCurrencySymbol = false;
            currencyCellType1.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(6).CellType = currencyCellType1;
            this.grdMovtos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Label = "Costo / Precio";
            this.grdMovtos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Width = 85F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.DecimalSeparator = ".";
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.NegativeRed = true;
            currencyCellType2.Separator = ",";
            currencyCellType2.ShowCurrencySymbol = false;
            currencyCellType2.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(7).CellType = currencyCellType2;
            this.grdMovtos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdMovtos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Width = 85F;
            this.grdMovtos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(10, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 47);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(166, 19);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 3;
            this.dtpFechaFinal.ValueChanged += new System.EventHandler(this.dtpFechaFinal_ValueChanged);
            this.dtpFechaFinal.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaFinal_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(135, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(49, 19);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 1;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.dtpFechaInicial_ValueChanged);
            this.dtpFechaInicial.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaInicial_Validating);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboFarmacias);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.cboEstados);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(10, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 105);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos de Farmacia";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(11, 76);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(237, 21);
            this.cboFarmacias.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Farmacia :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(11, 35);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(237, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmKardexProductoGral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 579);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmKardexProductoGral";
            this.Text = "Kardex General de Producto";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmKardexDeProducto_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private System.Windows.Forms.Label lblDescripcion;
        private FarPoint.Win.Spread.FpSpread grdMovtos;
        private FarPoint.Win.Spread.SheetView grdMovtos_Sheet1;
        private System.Windows.Forms.GroupBox groupBox4;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDescripcionSSA;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboTipoDeReporte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}