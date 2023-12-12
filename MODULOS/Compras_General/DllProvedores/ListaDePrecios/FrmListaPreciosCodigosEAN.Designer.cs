namespace DllProveedores.ListaDePrecios
{
    partial class FrmListaPreciosCodigosEAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPreciosCodigosEAN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.PercentCellType percentCellType1 = new FarPoint.Win.Spread.CellType.PercentCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDatosPrecio = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.txtDescuento = new SC_ControlsCS.scNumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrecio = new SC_ControlsCS.scNumericTextBox();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodEAN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).BeginInit();
            this.FrameDatosPrecio.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(920, 25);
            this.toolStripBarraMenu.TabIndex = 8;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
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
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar (CTRL +E)";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdListaDePrecios);
            this.groupBox2.Location = new System.Drawing.Point(10, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(902, 275);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Precios de Productos";
            // 
            // grdListaDePrecios
            // 
            this.grdListaDePrecios.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdListaDePrecios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdListaDePrecios.Location = new System.Drawing.Point(9, 19);
            this.grdListaDePrecios.Name = "grdListaDePrecios";
            this.grdListaDePrecios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdListaDePrecios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdListaDePrecios_Sheet1});
            this.grdListaDePrecios.Size = new System.Drawing.Size(886, 248);
            this.grdListaDePrecios.TabIndex = 0;
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 9;
            this.grdListaDePrecios_Sheet1.RowCount = 10;
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdProducto";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código EAN";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 27F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "IdProducto";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Visible = false;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Width = 80F;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Código EAN";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 120F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 250F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).CellType = currencyCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 70F;
            percentCellType1.DecimalPlaces = 2;
            percentCellType1.MinimumValue = 0;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = percentCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 70F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = currencyCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Width = 70F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType3.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = currencyCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "Fecha registro";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Width = 90F;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Width = 90F;
            this.grdListaDePrecios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDatosPrecio
            // 
            this.FrameDatosPrecio.Controls.Add(this.groupBox3);
            this.FrameDatosPrecio.Controls.Add(this.dtpFechaVigencia);
            this.FrameDatosPrecio.Controls.Add(this.label1);
            this.FrameDatosPrecio.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatosPrecio.Controls.Add(this.label3);
            this.FrameDatosPrecio.Controls.Add(this.txtCodEAN);
            this.FrameDatosPrecio.Controls.Add(this.label2);
            this.FrameDatosPrecio.Controls.Add(this.lblDescripcion);
            this.FrameDatosPrecio.Location = new System.Drawing.Point(10, 26);
            this.FrameDatosPrecio.Name = "FrameDatosPrecio";
            this.FrameDatosPrecio.Size = new System.Drawing.Size(902, 151);
            this.FrameDatosPrecio.TabIndex = 9;
            this.FrameDatosPrecio.TabStop = false;
            this.FrameDatosPrecio.Text = "Información de Precio de Producto";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotal);
            this.groupBox3.Controls.Add(this.txtDescuento);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtPrecio);
            this.groupBox3.Controls.Add(this.txtIva);
            this.groupBox3.Location = new System.Drawing.Point(99, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(795, 50);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Precio";
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = true;
            this.txtTotal.DigitsInGroup = 3;
            this.txtTotal.Enabled = false;
            this.txtTotal.Flags = 7680;
            this.txtTotal.Location = new System.Drawing.Point(685, 19);
            this.txtTotal.MaxDecimalPlaces = 4;
            this.txtTotal.MaxLength = 15;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308;
            this.txtTotal.RangeMin = -1.7976931348623157E+308;
            this.txtTotal.Size = new System.Drawing.Size(84, 20);
            this.txtTotal.TabIndex = 23;
            this.txtTotal.Text = "1.0000";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescuento
            // 
            this.txtDescuento.AllowNegative = true;
            this.txtDescuento.DigitsInGroup = 3;
            this.txtDescuento.Enabled = false;
            this.txtDescuento.Flags = 7680;
            this.txtDescuento.Location = new System.Drawing.Point(358, 18);
            this.txtDescuento.MaxDecimalPlaces = 2;
            this.txtDescuento.MaxLength = 15;
            this.txtDescuento.MaxWholeDigits = 9;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Prefix = "";
            this.txtDescuento.RangeMax = 1.7976931348623157E+308;
            this.txtDescuento.RangeMin = -1.7976931348623157E+308;
            this.txtDescuento.Size = new System.Drawing.Size(47, 20);
            this.txtDescuento.TabIndex = 31;
            this.txtDescuento.Text = "1.00";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDescuento.Validating += new System.ComponentModel.CancelEventHandler(this.txtDescuento_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(41, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "Precio Unitario :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(287, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "Descuento :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(479, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "Iva :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(642, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "Total :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecio
            // 
            this.txtPrecio.AllowNegative = true;
            this.txtPrecio.DigitsInGroup = 3;
            this.txtPrecio.Flags = 7680;
            this.txtPrecio.Location = new System.Drawing.Point(127, 18);
            this.txtPrecio.MaxDecimalPlaces = 4;
            this.txtPrecio.MaxLength = 15;
            this.txtPrecio.MaxWholeDigits = 9;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Prefix = "";
            this.txtPrecio.RangeMax = 1.7976931348623157E+308;
            this.txtPrecio.RangeMin = -1.7976931348623157E+308;
            this.txtPrecio.Size = new System.Drawing.Size(84, 20);
            this.txtPrecio.TabIndex = 21;
            this.txtPrecio.Text = "1.0000";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            this.txtPrecio.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrecio_Validating);
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = true;
            this.txtIva.DigitsInGroup = 3;
            this.txtIva.Enabled = false;
            this.txtIva.Flags = 7680;
            this.txtIva.Location = new System.Drawing.Point(514, 19);
            this.txtIva.MaxDecimalPlaces = 4;
            this.txtIva.MaxLength = 15;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308;
            this.txtIva.RangeMin = -1.7976931348623157E+308;
            this.txtIva.Size = new System.Drawing.Size(69, 20);
            this.txtIva.TabIndex = 22;
            this.txtIva.Text = "1.0000";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVigencia.Enabled = false;
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(803, 16);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVigencia.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(693, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Fecha de vigencia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(561, 16);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(460, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodEAN
            // 
            this.txtCodEAN.Location = new System.Drawing.Point(99, 16);
            this.txtCodEAN.MaxLength = 15;
            this.txtCodEAN.Name = "txtCodEAN";
            this.txtCodEAN.Size = new System.Drawing.Size(150, 20);
            this.txtCodEAN.TabIndex = 9;
            this.txtCodEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodEAN_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Código EAN :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Enabled = false;
            this.lblDescripcion.Location = new System.Drawing.Point(99, 42);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(796, 43);
            this.lblDescripcion.TabIndex = 8;
            this.lblDescripcion.Text = "label2";
            // 
            // FrmListaPreciosCodigosEAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 465);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameDatosPrecio);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListaPreciosCodigosEAN";
            this.Text = "Listado de Precios de Productos - CodigoEAN";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaPreciosCodigosEAN_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).EndInit();
            this.FrameDatosPrecio.ResumeLayout(false);
            this.FrameDatosPrecio.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
        private System.Windows.Forms.GroupBox FrameDatosPrecio;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private SC_ControlsCS.scNumericTextBox txtPrecio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCodEAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scNumericTextBox txtDescuento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}