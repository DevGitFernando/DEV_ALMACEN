namespace Dll_IFacturacion.XSA
{
    partial class FrmFormaMetodoPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFormaMetodoPago));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameFormaMetodoDePago = new System.Windows.Forms.GroupBox();
            this.lblImportePorCobrar = new System.Windows.Forms.Label();
            this.lblImporteCobrado = new System.Windows.Forms.Label();
            this.lblImporteACobrar = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grdMetodosDePago = new FarPoint.Win.Spread.FpSpread();
            this.grdMetodosDePago_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label2 = new System.Windows.Forms.Label();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.txtCondicionesPago = new SC_ControlsCS.scTextBoxExt();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.chkVencimiento = new System.Windows.Forms.CheckBox();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.cboFormasDePago = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFormaMetodoDePago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMetodosDePago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMetodosDePago_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnFacturar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(895, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar factura electrónica";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameFormaMetodoDePago
            // 
            this.FrameFormaMetodoDePago.Controls.Add(this.lblImportePorCobrar);
            this.FrameFormaMetodoDePago.Controls.Add(this.lblImporteCobrado);
            this.FrameFormaMetodoDePago.Controls.Add(this.lblImporteACobrar);
            this.FrameFormaMetodoDePago.Controls.Add(this.label7);
            this.FrameFormaMetodoDePago.Controls.Add(this.label6);
            this.FrameFormaMetodoDePago.Controls.Add(this.label5);
            this.FrameFormaMetodoDePago.Controls.Add(this.grdMetodosDePago);
            this.FrameFormaMetodoDePago.Controls.Add(this.label2);
            this.FrameFormaMetodoDePago.Controls.Add(this.scLabelExt4);
            this.FrameFormaMetodoDePago.Controls.Add(this.txtObservaciones);
            this.FrameFormaMetodoDePago.Controls.Add(this.scLabelExt1);
            this.FrameFormaMetodoDePago.Controls.Add(this.txtCondicionesPago);
            this.FrameFormaMetodoDePago.Controls.Add(this.dtpFechaVencimiento);
            this.FrameFormaMetodoDePago.Controls.Add(this.chkVencimiento);
            this.FrameFormaMetodoDePago.Controls.Add(this.scLabelExt2);
            this.FrameFormaMetodoDePago.Controls.Add(this.cboFormasDePago);
            this.FrameFormaMetodoDePago.Location = new System.Drawing.Point(16, 34);
            this.FrameFormaMetodoDePago.Margin = new System.Windows.Forms.Padding(4);
            this.FrameFormaMetodoDePago.Name = "FrameFormaMetodoDePago";
            this.FrameFormaMetodoDePago.Padding = new System.Windows.Forms.Padding(4);
            this.FrameFormaMetodoDePago.Size = new System.Drawing.Size(867, 539);
            this.FrameFormaMetodoDePago.TabIndex = 4;
            this.FrameFormaMetodoDePago.TabStop = false;
            this.FrameFormaMetodoDePago.Text = "Información de Pago";
            // 
            // lblImportePorCobrar
            // 
            this.lblImportePorCobrar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImportePorCobrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportePorCobrar.Location = new System.Drawing.Point(679, 80);
            this.lblImportePorCobrar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportePorCobrar.Name = "lblImportePorCobrar";
            this.lblImportePorCobrar.Size = new System.Drawing.Size(165, 25);
            this.lblImportePorCobrar.TabIndex = 44;
            this.lblImportePorCobrar.Text = "Importe por cobrar :";
            this.lblImportePorCobrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImporteCobrado
            // 
            this.lblImporteCobrado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImporteCobrado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteCobrado.Location = new System.Drawing.Point(679, 52);
            this.lblImporteCobrado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImporteCobrado.Name = "lblImporteCobrado";
            this.lblImporteCobrado.Size = new System.Drawing.Size(165, 25);
            this.lblImporteCobrado.TabIndex = 43;
            this.lblImporteCobrado.Text = "Importe cobrado :";
            this.lblImporteCobrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImporteACobrar
            // 
            this.lblImporteACobrar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImporteACobrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteACobrar.Location = new System.Drawing.Point(679, 23);
            this.lblImporteACobrar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImporteACobrar.Name = "lblImporteACobrar";
            this.lblImporteACobrar.Size = new System.Drawing.Size(165, 25);
            this.lblImporteACobrar.TabIndex = 42;
            this.lblImporteACobrar.Text = "Importe a cobrar :";
            this.lblImporteACobrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(505, 79);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 25);
            this.label7.TabIndex = 41;
            this.label7.Text = "Importe restante :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(505, 50);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 25);
            this.label6.TabIndex = 40;
            this.label6.Text = "Importe pagado :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(505, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 25);
            this.label5.TabIndex = 39;
            this.label5.Text = "Importe total :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdMetodosDePago
            // 
            this.grdMetodosDePago.AccessibleDescription = "grdMetodosDePago, Sheet1, Row 0, Column 0, ";
            this.grdMetodosDePago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMetodosDePago.Location = new System.Drawing.Point(25, 151);
            this.grdMetodosDePago.Margin = new System.Windows.Forms.Padding(4);
            this.grdMetodosDePago.Name = "grdMetodosDePago";
            this.grdMetodosDePago.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMetodosDePago.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMetodosDePago_Sheet1});
            this.grdMetodosDePago.Size = new System.Drawing.Size(821, 317);
            this.grdMetodosDePago.TabIndex = 38;
            this.grdMetodosDePago.EditModeOff += new System.EventHandler(this.grdMetodosDePago_EditModeOff);
            // 
            // grdMetodosDePago_Sheet1
            // 
            this.grdMetodosDePago_Sheet1.Reset();
            this.grdMetodosDePago_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMetodosDePago_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMetodosDePago_Sheet1.ColumnCount = 5;
            this.grdMetodosDePago_Sheet1.RowCount = 10;
            this.grdMetodosDePago_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave";
            this.grdMetodosDePago_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Método de pago";
            this.grdMetodosDePago_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Requiere referencia";
            this.grdMetodosDePago_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Importe pago";
            this.grdMetodosDePago_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Referencia";
            this.grdMetodosDePago_Sheet1.ColumnHeader.Rows.Get(0).Height = 34F;
            this.grdMetodosDePago_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdMetodosDePago_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(0).Label = "Clave";
            this.grdMetodosDePago_Sheet1.Columns.Get(0).Locked = true;
            this.grdMetodosDePago_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdMetodosDePago_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMetodosDePago_Sheet1.Columns.Get(1).Label = "Método de pago";
            this.grdMetodosDePago_Sheet1.Columns.Get(1).Locked = true;
            this.grdMetodosDePago_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(1).Width = 180F;
            this.grdMetodosDePago_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMetodosDePago_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(2).Label = "Requiere referencia";
            this.grdMetodosDePago_Sheet1.Columns.Get(2).Locked = true;
            this.grdMetodosDePago_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(2).Width = 80F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 999999999.99D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdMetodosDePago_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdMetodosDePago_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMetodosDePago_Sheet1.Columns.Get(3).Label = "Importe pago";
            this.grdMetodosDePago_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(3).Width = 120F;
            textCellType4.MaxLength = 10;
            this.grdMetodosDePago_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.grdMetodosDePago_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMetodosDePago_Sheet1.Columns.Get(4).Label = "Referencia";
            this.grdMetodosDePago_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMetodosDePago_Sheet1.Columns.Get(4).Width = 120F;
            this.grdMetodosDePago_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMetodosDePago_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 22);
            this.label2.TabIndex = 37;
            this.label2.Text = "3. Seleccione las Formas de Pago que apliquen :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(20, 476);
            this.scLabelExt4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(117, 22);
            this.scLabelExt4.TabIndex = 23;
            this.scLabelExt4.Text = "Observaciones :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(143, 476);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.MaxLength = 200;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(700, 48);
            this.txtObservaciones.TabIndex = 22;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(20, 63);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = true;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(117, 41);
            this.scLabelExt1.TabIndex = 21;
            this.scLabelExt1.Text = "Condiciones de Pago :";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCondicionesPago
            // 
            this.txtCondicionesPago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCondicionesPago.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCondicionesPago.Decimales = 2;
            this.txtCondicionesPago.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCondicionesPago.ForeColor = System.Drawing.Color.Black;
            this.txtCondicionesPago.Location = new System.Drawing.Point(143, 57);
            this.txtCondicionesPago.Margin = new System.Windows.Forms.Padding(4);
            this.txtCondicionesPago.MaxLength = 200;
            this.txtCondicionesPago.Multiline = true;
            this.txtCondicionesPago.Name = "txtCondicionesPago";
            this.txtCondicionesPago.PermitirApostrofo = false;
            this.txtCondicionesPago.PermitirNegativos = false;
            this.txtCondicionesPago.Size = new System.Drawing.Size(353, 62);
            this.txtCondicionesPago.TabIndex = 20;
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaVencimiento.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(741, 122);
            this.dtpFechaVencimiento.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(101, 22);
            this.dtpFechaVencimiento.TabIndex = 3;
            // 
            // chkVencimiento
            // 
            this.chkVencimiento.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVencimiento.Location = new System.Drawing.Point(528, 122);
            this.chkVencimiento.Margin = new System.Windows.Forms.Padding(4);
            this.chkVencimiento.Name = "chkVencimiento";
            this.chkVencimiento.Size = new System.Drawing.Size(208, 25);
            this.chkVencimiento.TabIndex = 2;
            this.chkVencimiento.Text = "Vencimiento";
            this.chkVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVencimiento.UseVisualStyleBackColor = true;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(8, 23);
            this.scLabelExt2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(129, 26);
            this.scLabelExt2.TabIndex = 16;
            this.scLabelExt2.Text = "Método de Pago :";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFormasDePago
            // 
            this.cboFormasDePago.BackColorEnabled = System.Drawing.Color.White;
            this.cboFormasDePago.Data = "";
            this.cboFormasDePago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormasDePago.Filtro = " 1 = 1";
            this.cboFormasDePago.FormattingEnabled = true;
            this.cboFormasDePago.ListaItemsBusqueda = 20;
            this.cboFormasDePago.Location = new System.Drawing.Point(143, 23);
            this.cboFormasDePago.Margin = new System.Windows.Forms.Padding(4);
            this.cboFormasDePago.MostrarToolTip = false;
            this.cboFormasDePago.Name = "cboFormasDePago";
            this.cboFormasDePago.Size = new System.Drawing.Size(353, 24);
            this.cboFormasDePago.TabIndex = 0;
            // 
            // FrmFormaMetodoPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 587);
            this.Controls.Add(this.FrameFormaMetodoDePago);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmFormaMetodoPago";
            this.Text = "Información de Forma y Método de pago";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFormaMetodoPago_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFormaMetodoDePago.ResumeLayout(false);
            this.FrameFormaMetodoDePago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMetodosDePago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMetodosDePago_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameFormaMetodoDePago;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.CheckBox chkVencimiento;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scComboBoxExt cboFormasDePago;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scTextBoxExt txtCondicionesPago;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label lblImportePorCobrar;
        private System.Windows.Forms.Label lblImporteCobrado;
        private System.Windows.Forms.Label lblImporteACobrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private FarPoint.Win.Spread.FpSpread grdMetodosDePago;
        private FarPoint.Win.Spread.SheetView grdMetodosDePago_Sheet1;
        private System.Windows.Forms.Label label2;
    }
}