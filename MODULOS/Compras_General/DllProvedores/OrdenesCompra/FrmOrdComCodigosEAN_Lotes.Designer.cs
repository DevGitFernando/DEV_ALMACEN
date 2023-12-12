namespace DllProveedores.OrdenesCompra
{
    partial class FrmOrdComCodigosEAN_Lotes
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
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.NamedStyle namedStyle2 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdComCodigosEAN_Lotes));
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescripcionSSA = new System.Windows.Forms.Label();
            this.lblCantidadRequerida = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gpoDatosLotes = new System.Windows.Forms.GroupBox();
            this.cboStatus = new SC_ControlsCS.scComboBoxExt();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaCaducidad = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaEntrada = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClaveLote = new SC_ControlsCS.scTextBoxExt();
            this.grdLotes = new FarPoint.Win.Spread.FpSpread();
            this.grdLotes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblAyudaAux = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpoDatosLotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblDescripcionSSA);
            this.groupBox1.Controls.Add(this.lblCantidadRequerida);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 70);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Producto";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(469, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cantidad Requerida :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSSA
            // 
            this.lblDescripcionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSSA.Location = new System.Drawing.Point(94, 42);
            this.lblDescripcionSSA.Name = "lblDescripcionSSA";
            this.lblDescripcionSSA.Size = new System.Drawing.Size(600, 20);
            this.lblDescripcionSSA.TabIndex = 16;
            this.lblDescripcionSSA.Text = "Descripcion CLAVE SSA";
            // 
            // lblCantidadRequerida
            // 
            this.lblCantidadRequerida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCantidadRequerida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadRequerida.Location = new System.Drawing.Point(606, 11);
            this.lblCantidadRequerida.Name = "lblCantidadRequerida";
            this.lblCantidadRequerida.Size = new System.Drawing.Size(88, 23);
            this.lblCantidadRequerida.TabIndex = 5;
            this.lblCantidadRequerida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Código EAN :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(94, 16);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(175, 20);
            this.lblClaveSSA.TabIndex = 15;
            this.lblClaveSSA.Text = "Producto :";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gpoDatosLotes);
            this.groupBox2.Controls.Add(this.grdLotes);
            this.groupBox2.Location = new System.Drawing.Point(8, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(700, 272);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Lotes";
            // 
            // gpoDatosLotes
            // 
            this.gpoDatosLotes.Controls.Add(this.cboStatus);
            this.gpoDatosLotes.Controls.Add(this.btnRegresar);
            this.gpoDatosLotes.Controls.Add(this.btnAgregar);
            this.gpoDatosLotes.Controls.Add(this.label5);
            this.gpoDatosLotes.Controls.Add(this.dtpFechaCaducidad);
            this.gpoDatosLotes.Controls.Add(this.label6);
            this.gpoDatosLotes.Controls.Add(this.dtpFechaEntrada);
            this.gpoDatosLotes.Controls.Add(this.label9);
            this.gpoDatosLotes.Controls.Add(this.txtClaveLote);
            this.gpoDatosLotes.Location = new System.Drawing.Point(81, 87);
            this.gpoDatosLotes.Name = "gpoDatosLotes";
            this.gpoDatosLotes.Size = new System.Drawing.Size(528, 131);
            this.gpoDatosLotes.TabIndex = 6;
            this.gpoDatosLotes.TabStop = false;
            this.gpoDatosLotes.Text = "Datos de lote";
            this.gpoDatosLotes.Visible = false;
            // 
            // cboStatus
            // 
            this.cboStatus.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatus.Data = "";
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(372, 77);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 21);
            this.cboStatus.TabIndex = 6;
            this.cboStatus.Visible = false;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(368, 133);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(125, 23);
            this.btnRegresar.TabIndex = 4;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(233, 133);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 23);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar y regresar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(112, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Fecha de caducidad :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaCaducidad
            // 
            this.dtpFechaCaducidad.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaCaducidad.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCaducidad.Location = new System.Drawing.Point(233, 103);
            this.dtpFechaCaducidad.Name = "dtpFechaCaducidad";
            this.dtpFechaCaducidad.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaCaducidad.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(112, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 18);
            this.label6.TabIndex = 3;
            this.label6.Text = "Fecha de entrada :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaEntrada
            // 
            this.dtpFechaEntrada.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEntrada.Location = new System.Drawing.Point(233, 77);
            this.dtpFechaEntrada.Name = "dtpFechaEntrada";
            this.dtpFechaEntrada.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaEntrada.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(145, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 18);
            this.label9.TabIndex = 1;
            this.label9.Text = "Clave de lote :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveLote
            // 
            this.txtClaveLote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveLote.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveLote.Decimales = 2;
            this.txtClaveLote.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveLote.ForeColor = System.Drawing.Color.Black;
            this.txtClaveLote.Location = new System.Drawing.Point(233, 46);
            this.txtClaveLote.MaxLength = 30;
            this.txtClaveLote.Name = "txtClaveLote";
            this.txtClaveLote.PermitirApostrofo = false;
            this.txtClaveLote.PermitirNegativos = false;
            this.txtClaveLote.Size = new System.Drawing.Size(260, 20);
            this.txtClaveLote.TabIndex = 0;
            // 
            // grdLotes
            // 
            this.grdLotes.AccessibleDescription = "grdLotes, Sheet1, Row 0, Column 0, ";
            this.grdLotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdLotes.Location = new System.Drawing.Point(5, 16);
            this.grdLotes.Name = "grdLotes";
            namedStyle1.BackColor = System.Drawing.Color.DarkGray;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.Renderer = generalCellType1;
            namedStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(196)))), ((int)(((byte)(233)))));
            namedStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle2.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle2.Renderer = enhancedCornerRenderer1;
            namedStyle2.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1,
            namedStyle2});
            this.grdLotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdLotes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdLotes_Sheet1});
            this.grdLotes.Size = new System.Drawing.Size(691, 250);
            this.grdLotes.TabIndex = 5;
            // 
            // grdLotes_Sheet1
            // 
            this.grdLotes_Sheet1.Reset();
            this.grdLotes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdLotes_Sheet1.ColumnCount = 7;
            this.grdLotes_Sheet1.RowCount = 5;
            this.grdLotes_Sheet1.Cells.Get(0, 4).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.Cells.Get(0, 5).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo EAN";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Clave de lote";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Meses por Caducar";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha de entrada";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha de caducidad";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cantidad";
            this.grdLotes_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdLotes_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdLotes_Sheet1.Columns.Get(0).Label = "IdClaveSSA";
            this.grdLotes_Sheet1.Columns.Get(0).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(0).Width = 98F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            this.grdLotes_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdLotes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Label = "Codigo EAN";
            this.grdLotes_Sheet1.Columns.Get(1).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(1).Width = 122F;
            textCellType3.MaxLength = 30;
            this.grdLotes_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdLotes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(2).Label = "Clave de lote";
            this.grdLotes_Sheet1.Columns.Get(2).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Width = 215F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000;
            numberCellType1.MinimumValue = -10000000;
            this.grdLotes_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdLotes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Label = "Meses por Caducar";
            this.grdLotes_Sheet1.Columns.Get(3).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Width = 124F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.UserDefinedFormat = "dd/MM/yyyy";
            this.grdLotes_Sheet1.Columns.Get(4).CellType = dateTimeCellType1;
            this.grdLotes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Label = "Fecha de entrada";
            this.grdLotes_Sheet1.Columns.Get(4).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(4).Width = 123F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2010, 3, 19, 13, 28, 7, 0);
            dateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType2.TimeDefault = new System.DateTime(2010, 3, 19, 13, 28, 7, 0);
            dateTimeCellType2.UserDefinedFormat = "dd/MM/yyyy";
            this.grdLotes_Sheet1.Columns.Get(5).CellType = dateTimeCellType2;
            this.grdLotes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Label = "Fecha de caducidad";
            this.grdLotes_Sheet1.Columns.Get(5).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Width = 165F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000;
            numberCellType2.MinimumValue = 0;
            this.grdLotes_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.grdLotes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Label = "Cantidad";
            this.grdLotes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Width = 142F;
            this.grdLotes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblAyudaAux
            // 
            this.lblAyudaAux.AutoSize = true;
            this.lblAyudaAux.BackColor = System.Drawing.Color.Black;
            this.lblAyudaAux.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAyudaAux.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaAux.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAyudaAux.Location = new System.Drawing.Point(0, 342);
            this.lblAyudaAux.Name = "lblAyudaAux";
            this.lblAyudaAux.Size = new System.Drawing.Size(108, 16);
            this.lblAyudaAux.TabIndex = 15;
            this.lblAyudaAux.Text = "<F12> Cerrar   ";
            this.lblAyudaAux.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblAyudaAux.Visible = false;
            // 
            // lblAyuda
            // 
            this.lblAyuda.BackColor = System.Drawing.Color.Black;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAyuda.Location = new System.Drawing.Point(0, 358);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(716, 24);
            this.lblAyuda.TabIndex = 14;
            this.lblAyuda.Text = "   <F8> Ver / Ocular   Agregar nuevo lote a artículo                             " +
                "                                        <F12> Cerrar";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAyuda.Visible = false;
            // 
            // FrmOrdComCodigosEAN_Lotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 382);
            this.Controls.Add(this.lblAyudaAux);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOrdComCodigosEAN_Lotes";
            this.Text = "Captura de Información de Lotes de Caducidad";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmOrdComCodigosEAN_Lotes_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOrdComCodigosEAN_Lotes_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOrdComCodigosEAN_Lotes_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gpoDatosLotes.ResumeLayout(false);
            this.gpoDatosLotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescripcionSSA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCantidadRequerida;
        private System.Windows.Forms.GroupBox gpoDatosLotes;
        private SC_ControlsCS.scComboBoxExt cboStatus;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaCaducidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrada;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtClaveLote;
        private FarPoint.Win.Spread.FpSpread grdLotes;
        private FarPoint.Win.Spread.SheetView grdLotes_Sheet1;
        private System.Windows.Forms.Label lblAyudaAux;
        private System.Windows.Forms.Label lblAyuda;
    }
}