namespace Farmacia.Digitalizacion
{
    partial class FrmVentas_Documentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVentas_Documentos));
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType19 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType20 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType28 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType29 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType30 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            this.grVta = new System.Windows.Forms.GroupBox();
            this.lblCorregido = new System.Windows.Forms.Label();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdDigitalizacion = new FarPoint.Win.Spread.FpSpread();
            this.grdDigitalizacion_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripDigitalizacion = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDigitalizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDigitalizarReceta = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.grVta.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDigitalizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDigitalizacion_Sheet1)).BeginInit();
            this.toolStripDigitalizacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grVta
            // 
            this.grVta.Controls.Add(this.lblCorregido);
            this.grVta.Controls.Add(this.lblSubPro);
            this.grVta.Controls.Add(this.txtSubPro);
            this.grVta.Controls.Add(this.label7);
            this.grVta.Controls.Add(this.lblPro);
            this.grVta.Controls.Add(this.txtPro);
            this.grVta.Controls.Add(this.label9);
            this.grVta.Controls.Add(this.lblSubCte);
            this.grVta.Controls.Add(this.txtSubCte);
            this.grVta.Controls.Add(this.label5);
            this.grVta.Controls.Add(this.lblCte);
            this.grVta.Controls.Add(this.txtCte);
            this.grVta.Controls.Add(this.label2);
            this.grVta.Controls.Add(this.dtpFechaRegistro);
            this.grVta.Controls.Add(this.label3);
            this.grVta.Controls.Add(this.lblCancelado);
            this.grVta.Controls.Add(this.txtFolio);
            this.grVta.Controls.Add(this.label1);
            this.grVta.Location = new System.Drawing.Point(8, 27);
            this.grVta.Name = "grVta";
            this.grVta.Size = new System.Drawing.Size(753, 104);
            this.grVta.TabIndex = 6;
            this.grVta.TabStop = false;
            this.grVta.Text = "Datos generales de Venta";
            // 
            // lblCorregido
            // 
            this.lblCorregido.BackColor = System.Drawing.Color.Transparent;
            this.lblCorregido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCorregido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorregido.Location = new System.Drawing.Point(304, 23);
            this.lblCorregido.Name = "lblCorregido";
            this.lblCorregido.Size = new System.Drawing.Size(98, 20);
            this.lblCorregido.TabIndex = 47;
            this.lblCorregido.Text = "CORREGIDO";
            this.lblCorregido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCorregido.Visible = false;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(531, 71);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(212, 21);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(466, 72);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(59, 20);
            this.txtSubPro.TabIndex = 4;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(384, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(531, 47);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(212, 21);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(466, 49);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(59, 20);
            this.txtPro.TabIndex = 3;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(399, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(150, 71);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(212, 21);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(85, 72);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(59, 20);
            this.txtSubCte.TabIndex = 2;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(150, 47);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(212, 21);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(85, 49);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(59, 20);
            this.txtCte.TabIndex = 1;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(38, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(652, 24);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(552, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(191, 23);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(85, 24);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(770, 25);
            this.toolStripBarraMenu.TabIndex = 13;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdDigitalizacion);
            this.groupBox1.Controls.Add(this.toolStripDigitalizacion);
            this.groupBox1.Location = new System.Drawing.Point(8, 136);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(753, 323);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagenes";
            // 
            // grdDigitalizacion
            // 
            this.grdDigitalizacion.AccessibleDescription = "grdDigitalizacion, Sheet1, Row 0, Column 0, ";
            this.grdDigitalizacion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDigitalizacion.Location = new System.Drawing.Point(10, 58);
            this.grdDigitalizacion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdDigitalizacion.Name = "grdDigitalizacion";
            this.grdDigitalizacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDigitalizacion.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDigitalizacion_Sheet1});
            this.grdDigitalizacion.Size = new System.Drawing.Size(732, 258);
            this.grdDigitalizacion.TabIndex = 14;
            this.grdDigitalizacion.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdDigitalizacion_CellClick);
            this.grdDigitalizacion.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdDigitalizacion_ButtonClicked);
            // 
            // grdDigitalizacion_Sheet1
            // 
            this.grdDigitalizacion_Sheet1.Reset();
            this.grdDigitalizacion_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDigitalizacion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDigitalizacion_Sheet1.ColumnCount = 6;
            this.grdDigitalizacion_Sheet1.RowCount = 10;
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Partida";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "TipoDocto";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Tipo de documento";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Remover";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Actualizar";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Visualizar";
            this.grdDigitalizacion_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            numberCellType19.DecimalPlaces = 0;
            numberCellType19.MaximumValue = 100D;
            numberCellType19.MinimumValue = 1D;
            this.grdDigitalizacion_Sheet1.Columns.Get(0).CellType = numberCellType19;
            this.grdDigitalizacion_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(0).Label = "Partida";
            this.grdDigitalizacion_Sheet1.Columns.Get(0).Locked = true;
            this.grdDigitalizacion_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(0).Width = 90F;
            numberCellType20.DecimalPlaces = 0;
            numberCellType20.MaximumValue = 100D;
            numberCellType20.MinimumValue = 1D;
            this.grdDigitalizacion_Sheet1.Columns.Get(1).CellType = numberCellType20;
            this.grdDigitalizacion_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(1).Label = "TipoDocto";
            this.grdDigitalizacion_Sheet1.Columns.Get(1).Locked = true;
            this.grdDigitalizacion_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(1).Width = 90F;
            this.grdDigitalizacion_Sheet1.Columns.Get(2).CellType = textCellType10;
            this.grdDigitalizacion_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(2).Label = "Tipo de documento";
            this.grdDigitalizacion_Sheet1.Columns.Get(2).Locked = true;
            this.grdDigitalizacion_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(2).Width = 200F;
            buttonCellType28.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType28.Text = "...";
            this.grdDigitalizacion_Sheet1.Columns.Get(3).CellType = buttonCellType28;
            this.grdDigitalizacion_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(3).Label = "Remover";
            this.grdDigitalizacion_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(3).Width = 90F;
            buttonCellType29.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType29.Text = "...";
            this.grdDigitalizacion_Sheet1.Columns.Get(4).CellType = buttonCellType29;
            this.grdDigitalizacion_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(4).Label = "Actualizar";
            this.grdDigitalizacion_Sheet1.Columns.Get(4).Locked = false;
            this.grdDigitalizacion_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(4).Width = 90F;
            buttonCellType30.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType30.Text = "...";
            this.grdDigitalizacion_Sheet1.Columns.Get(5).CellType = buttonCellType30;
            this.grdDigitalizacion_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(5).Label = "Visualizar";
            this.grdDigitalizacion_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDigitalizacion_Sheet1.Columns.Get(5).Width = 90F;
            this.grdDigitalizacion_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdDigitalizacion_Sheet1.Rows.Default.Height = 25F;
            this.grdDigitalizacion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripDigitalizacion
            // 
            this.toolStripDigitalizacion.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripDigitalizacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnDigitalizar,
            this.toolStripSeparator4,
            this.btnDigitalizarReceta,
            this.toolStripSeparator3});
            this.toolStripDigitalizacion.Location = new System.Drawing.Point(2, 15);
            this.toolStripDigitalizacion.Name = "toolStripDigitalizacion";
            this.toolStripDigitalizacion.Size = new System.Drawing.Size(749, 39);
            this.toolStripDigitalizacion.TabIndex = 13;
            this.toolStripDigitalizacion.Text = "Digitalizar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnDigitalizar
            // 
            this.btnDigitalizar.Image = ((System.Drawing.Image)(resources.GetObject("btnDigitalizar.Image")));
            this.btnDigitalizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDigitalizar.Name = "btnDigitalizar";
            this.btnDigitalizar.Size = new System.Drawing.Size(150, 36);
            this.btnDigitalizar.Text = "F4 - Digitalizar ticket";
            this.btnDigitalizar.Click += new System.EventHandler(this.btnDigitalizar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnDigitalizarReceta
            // 
            this.btnDigitalizarReceta.Image = ((System.Drawing.Image)(resources.GetObject("btnDigitalizarReceta.Image")));
            this.btnDigitalizarReceta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDigitalizarReceta.Name = "btnDigitalizarReceta";
            this.btnDigitalizarReceta.Size = new System.Drawing.Size(153, 36);
            this.btnDigitalizarReceta.Text = "F6 - Digitalizar receta";
            this.btnDigitalizarReceta.Click += new System.EventHandler(this.btnDigitalizarReceta_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // FrmVentas_Documentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 469);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.grVta);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmVentas_Documentos";
            this.Text = "Digitalización de documentos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVentas_Documentos_Load);
            this.Shown += new System.EventHandler(this.FrmVentas_Documentos_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmVentas_Documentos_KeyDown);
            this.grVta.ResumeLayout(false);
            this.grVta.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDigitalizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDigitalizacion_Sheet1)).EndInit();
            this.toolStripDigitalizacion.ResumeLayout(false);
            this.toolStripDigitalizacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grVta;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCorregido;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStripDigitalizacion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDigitalizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private FarPoint.Win.Spread.FpSpread grdDigitalizacion;
        private FarPoint.Win.Spread.SheetView grdDigitalizacion_Sheet1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDigitalizarReceta;
    }
}