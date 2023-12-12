namespace DllProveedores.ListaDePrecios
{
    partial class FrmListaPreciosClaveSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPreciosClaveSSA));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
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
            this.FrameDatosClave = new System.Windows.Forms.GroupBox();
            this.FrameDatosPrecio = new System.Windows.Forms.GroupBox();
            this.txtImporte = new SC_ControlsCS.scNumericTextBox();
            this.txtDescuento = new SC_ControlsCS.scNumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrecio = new SC_ControlsCS.scNumericTextBox();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.txtCodEAN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodClaveSSA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosClave.SuspendLayout();
            this.FrameDatosPrecio.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(922, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // FrameDatosClave
            // 
            this.FrameDatosClave.Controls.Add(this.FrameDatosPrecio);
            this.FrameDatosClave.Controls.Add(this.lblProducto);
            this.FrameDatosClave.Controls.Add(this.txtCodEAN);
            this.FrameDatosClave.Controls.Add(this.label5);
            this.FrameDatosClave.Controls.Add(this.dtpFechaVigencia);
            this.FrameDatosClave.Controls.Add(this.label1);
            this.FrameDatosClave.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatosClave.Controls.Add(this.label3);
            this.FrameDatosClave.Controls.Add(this.txtCodClaveSSA);
            this.FrameDatosClave.Controls.Add(this.label2);
            this.FrameDatosClave.Controls.Add(this.lblDescripcionClave);
            this.FrameDatosClave.Location = new System.Drawing.Point(10, 28);
            this.FrameDatosClave.Name = "FrameDatosClave";
            this.FrameDatosClave.Size = new System.Drawing.Size(904, 198);
            this.FrameDatosClave.TabIndex = 6;
            this.FrameDatosClave.TabStop = false;
            this.FrameDatosClave.Text = "Información de Precio de Clave SSA";
            // 
            // FrameDatosPrecio
            // 
            this.FrameDatosPrecio.Controls.Add(this.txtImporte);
            this.FrameDatosPrecio.Controls.Add(this.txtDescuento);
            this.FrameDatosPrecio.Controls.Add(this.label7);
            this.FrameDatosPrecio.Controls.Add(this.label4);
            this.FrameDatosPrecio.Controls.Add(this.label8);
            this.FrameDatosPrecio.Controls.Add(this.label9);
            this.FrameDatosPrecio.Controls.Add(this.txtPrecio);
            this.FrameDatosPrecio.Controls.Add(this.txtIva);
            this.FrameDatosPrecio.Location = new System.Drawing.Point(99, 138);
            this.FrameDatosPrecio.Name = "FrameDatosPrecio";
            this.FrameDatosPrecio.Size = new System.Drawing.Size(795, 50);
            this.FrameDatosPrecio.TabIndex = 38;
            this.FrameDatosPrecio.TabStop = false;
            this.FrameDatosPrecio.Text = "Datos de Precio";
            // 
            // txtImporte
            // 
            this.txtImporte.AllowNegative = true;
            this.txtImporte.DigitsInGroup = 3;
            this.txtImporte.Enabled = false;
            this.txtImporte.Flags = 7680;
            this.txtImporte.Location = new System.Drawing.Point(685, 19);
            this.txtImporte.MaxDecimalPlaces = 4;
            this.txtImporte.MaxLength = 15;
            this.txtImporte.MaxWholeDigits = 9;
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Prefix = "";
            this.txtImporte.RangeMax = 1.7976931348623157E+308;
            this.txtImporte.RangeMin = -1.7976931348623157E+308;
            this.txtImporte.Size = new System.Drawing.Size(84, 20);
            this.txtImporte.TabIndex = 23;
            this.txtImporte.Text = "1.0000";
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.txtDescuento.TextChanged += new System.EventHandler(this.txtDescuento_TextChanged);
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
            this.label9.Location = new System.Drawing.Point(631, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "Importe :";
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
            // lblProducto
            // 
            this.lblProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProducto.Location = new System.Drawing.Point(99, 114);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(795, 21);
            this.lblProducto.TabIndex = 37;
            this.lblProducto.Text = "label2";
            // 
            // txtCodEAN
            // 
            this.txtCodEAN.Location = new System.Drawing.Point(99, 91);
            this.txtCodEAN.MaxLength = 15;
            this.txtCodEAN.Name = "txtCodEAN";
            this.txtCodEAN.Size = new System.Drawing.Size(161, 20);
            this.txtCodEAN.TabIndex = 35;
            this.txtCodEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodEAN_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(22, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Código EAN :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVigencia.Enabled = false;
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(803, 21);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVigencia.TabIndex = 29;
            this.dtpFechaVigencia.ValueChanged += new System.EventHandler(this.dtpFechaVigencia_ValueChanged);
            this.dtpFechaVigencia.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaVigencia_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(687, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Fecha de vigencia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(561, 21);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(454, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodClaveSSA
            // 
            this.txtCodClaveSSA.Location = new System.Drawing.Point(99, 21);
            this.txtCodClaveSSA.MaxLength = 15;
            this.txtCodClaveSSA.Name = "txtCodClaveSSA";
            this.txtCodClaveSSA.Size = new System.Drawing.Size(161, 20);
            this.txtCodClaveSSA.TabIndex = 9;
            this.txtCodClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodClaveSSA_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Clave SSA :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Location = new System.Drawing.Point(99, 47);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(795, 39);
            this.lblDescripcionClave.TabIndex = 8;
            this.lblDescripcionClave.Text = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdListaDePrecios);
            this.groupBox2.Location = new System.Drawing.Point(10, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 275);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Precios de Claves SSA";
            // 
            // grdListaDePrecios
            // 
            this.grdListaDePrecios.AccessibleDescription = "grdListaDePrecios, Sheet1, Row 0, Column 0, ";
            this.grdListaDePrecios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdListaDePrecios.Location = new System.Drawing.Point(9, 19);
            this.grdListaDePrecios.Name = "grdListaDePrecios";
            this.grdListaDePrecios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdListaDePrecios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdListaDePrecios_Sheet1});
            this.grdListaDePrecios.Size = new System.Drawing.Size(886, 248);
            this.grdListaDePrecios.TabIndex = 0;
            this.grdListaDePrecios.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdListaDePrecios_CellDoubleClick);
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 12;
            this.grdListaDePrecios_Sheet1.RowCount = 10;
            this.grdListaDePrecios_Sheet1.Cells.Get(0, 3).Value = "ACTIVO";
            this.grdListaDePrecios_Sheet1.Cells.Get(1, 3).Value = "CANCELADO";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClave SSA";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "% Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "TasaIva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Fecha registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 27F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "IdClave SSA";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Visible = false;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Width = 80F;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 108F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 104F;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "Status";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 73F;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "Descripción";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 269F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = currencyCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Width = 70F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.MinimumValue = 0;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = numberCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "% Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Width = 81F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.MinimumValue = 0;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).CellType = numberCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "TasaIva";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Visible = false;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).CellType = currencyCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Width = 70F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType3.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).CellType = currencyCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Label = "Fecha registro";
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Width = 90F;
            this.grdListaDePrecios_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(11).Label = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.Columns.Get(11).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(11).Width = 90F;
            this.grdListaDePrecios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmListaPreciosClaveSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 515);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameDatosClave);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListaPreciosClaveSSA";
            this.Text = "Lista de Precios de Claves SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaPreciosClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosClave.ResumeLayout(false);
            this.FrameDatosClave.PerformLayout();
            this.FrameDatosPrecio.ResumeLayout(false);
            this.FrameDatosPrecio.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameDatosClave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCodClaveSSA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
        private System.Windows.Forms.TextBox txtCodEAN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.GroupBox FrameDatosPrecio;
        private SC_ControlsCS.scNumericTextBox txtImporte;
        private SC_ControlsCS.scNumericTextBox txtDescuento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scNumericTextBox txtPrecio;
        private SC_ControlsCS.scNumericTextBox txtIva;
    }
}