namespace OficinaCentral.Inventario
{
    partial class FrmDevoluciones_ComprasProveedor_General
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDevoluciones_ComprasProveedor_General));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rdoTodas = new System.Windows.Forms.RadioButton();
            this.rdoSinPromocion = new System.Windows.Forms.RadioButton();
            this.rdoConPromocion = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grdEstados = new FarPoint.Win.Spread.FpSpread();
            this.grdEstados_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalFarmacias = new System.Windows.Forms.Label();
            this.grdFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmExistencias = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstados_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(956, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
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
            this.groupBox1.Controls.Add(this.lblProveedor);
            this.groupBox1.Controls.Add(this.txtIdProveedor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(939, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Consulta";
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(278, 16);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(526, 21);
            this.lblProveedor.TabIndex = 12;
            this.lblProveedor.Text = "Proveedor :";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(172, 16);
            this.txtIdProveedor.MaxLength = 4;
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(100, 20);
            this.txtIdProveedor.TabIndex = 0;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_KeyDown);
            this.txtIdProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(102, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoTodas);
            this.groupBox6.Controls.Add(this.rdoSinPromocion);
            this.groupBox6.Controls.Add(this.rdoConPromocion);
            this.groupBox6.Location = new System.Drawing.Point(101, 42);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(355, 43);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tipo de Compra";
            // 
            // rdoTodas
            // 
            this.rdoTodas.AutoSize = true;
            this.rdoTodas.Location = new System.Drawing.Point(33, 17);
            this.rdoTodas.Name = "rdoTodas";
            this.rdoTodas.Size = new System.Drawing.Size(55, 17);
            this.rdoTodas.TabIndex = 2;
            this.rdoTodas.TabStop = true;
            this.rdoTodas.Text = "Todas";
            this.rdoTodas.UseVisualStyleBackColor = true;
            // 
            // rdoSinPromocion
            // 
            this.rdoSinPromocion.AutoSize = true;
            this.rdoSinPromocion.Location = new System.Drawing.Point(108, 17);
            this.rdoSinPromocion.Name = "rdoSinPromocion";
            this.rdoSinPromocion.Size = new System.Drawing.Size(58, 17);
            this.rdoSinPromocion.TabIndex = 0;
            this.rdoSinPromocion.TabStop = true;
            this.rdoSinPromocion.Text = "Normal";
            this.rdoSinPromocion.UseVisualStyleBackColor = true;
            // 
            // rdoConPromocion
            // 
            this.rdoConPromocion.AutoSize = true;
            this.rdoConPromocion.Location = new System.Drawing.Point(186, 17);
            this.rdoConPromocion.Name = "rdoConPromocion";
            this.rdoConPromocion.Size = new System.Drawing.Size(136, 17);
            this.rdoConPromocion.TabIndex = 1;
            this.rdoConPromocion.TabStop = true;
            this.rdoConPromocion.Text = "Con Promoción/Regalo";
            this.rdoConPromocion.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpFechaFinal);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.dtpFechaInicial);
            this.groupBox4.Location = new System.Drawing.Point(465, 42);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(339, 43);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(216, 15);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(185, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(40, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(82, 15);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.grdEstados);
            this.groupBox2.Location = new System.Drawing.Point(9, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 369);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Devoluciones por Estado";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(177, 334);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total Estados $ :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(289, 334);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(141, 26);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdEstados
            // 
            this.grdEstados.AccessibleDescription = "grdEstados, Sheet1, Row 0, Column 0, ";
            this.grdEstados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdEstados.Location = new System.Drawing.Point(10, 16);
            this.grdEstados.Name = "grdEstados";
            this.grdEstados.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdEstados.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdEstados_Sheet1});
            this.grdEstados.Size = new System.Drawing.Size(437, 315);
            this.grdEstados.TabIndex = 0;
            this.grdEstados.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdEstados_CellDoubleClick);
            // 
            // grdEstados_Sheet1
            // 
            this.grdEstados_Sheet1.Reset();
            this.grdEstados_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdEstados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdEstados_Sheet1.ColumnCount = 3;
            this.grdEstados_Sheet1.RowCount = 10;
            this.grdEstados_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdEstados_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdEstados_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Compra";
            this.grdEstados_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdEstados_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdEstados_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdEstados_Sheet1.Columns.Get(0).Locked = true;
            this.grdEstados_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstados_Sheet1.Columns.Get(0).Width = 50F;
            this.grdEstados_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdEstados_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdEstados_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdEstados_Sheet1.Columns.Get(1).Locked = true;
            this.grdEstados_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstados_Sheet1.Columns.Get(1).Width = 208F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.ShowSeparator = true;
            this.grdEstados_Sheet1.Columns.Get(2).CellType = currencyCellType1;
            this.grdEstados_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdEstados_Sheet1.Columns.Get(2).Label = "Compra";
            this.grdEstados_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstados_Sheet1.Columns.Get(2).Width = 123F;
            this.grdEstados_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdEstados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblTotalFarmacias);
            this.groupBox3.Controls.Add(this.grdFarmacias);
            this.groupBox3.Location = new System.Drawing.Point(474, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(474, 369);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Devoluciones por Farmacia";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(174, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 26);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total Farmacias $ :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalFarmacias
            // 
            this.lblTotalFarmacias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFarmacias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFarmacias.Location = new System.Drawing.Point(307, 335);
            this.lblTotalFarmacias.Name = "lblTotalFarmacias";
            this.lblTotalFarmacias.Size = new System.Drawing.Size(141, 26);
            this.lblTotalFarmacias.TabIndex = 1;
            this.lblTotalFarmacias.Text = "label3";
            this.lblTotalFarmacias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdFarmacias
            // 
            this.grdFarmacias.AccessibleDescription = "grdFarmacias, Sheet1, Row 0, Column 0, ";
            this.grdFarmacias.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFarmacias.Location = new System.Drawing.Point(10, 16);
            this.grdFarmacias.Name = "grdFarmacias";
            this.grdFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFarmacias_Sheet1});
            this.grdFarmacias.Size = new System.Drawing.Size(454, 315);
            this.grdFarmacias.TabIndex = 0;
            this.grdFarmacias.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdFarmacias_CellDoubleClick);
            // 
            // grdFarmacias_Sheet1
            // 
            this.grdFarmacias_Sheet1.Reset();
            this.grdFarmacias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFarmacias_Sheet1.ColumnCount = 4;
            this.grdFarmacias_Sheet1.RowCount = 10;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Compra";
            this.grdFarmacias_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdFarmacias_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdFarmacias_Sheet1.Columns.Get(0).Visible = false;
            this.grdFarmacias_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdFarmacias_Sheet1.Columns.Get(1).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(1).Width = 70F;
            this.grdFarmacias_Sheet1.Columns.Get(2).CellType = textCellType5;
            this.grdFarmacias_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFarmacias_Sheet1.Columns.Get(2).Label = "Nombre";
            this.grdFarmacias_Sheet1.Columns.Get(2).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(2).Width = 206F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.ShowSeparator = true;
            this.grdFarmacias_Sheet1.Columns.Get(3).CellType = currencyCellType2;
            this.grdFarmacias_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdFarmacias_Sheet1.Columns.Get(3).Label = "Compra";
            this.grdFarmacias_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(3).Width = 122F;
            this.grdFarmacias_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmExistencias
            // 
            this.tmExistencias.Tick += new System.EventHandler(this.tmExistencias_Tick);
            // 
            // FrmDevoluciones_ComprasProveedor_General
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 517);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDevoluciones_ComprasProveedor_General";
            this.Text = "Consulta de Devoluciones de Compras por Proveedor General";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDevoluciones_ComprasProveedor_General_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEstados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstados_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private FarPoint.Win.Spread.FpSpread grdEstados;
        private FarPoint.Win.Spread.SheetView grdEstados_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalFarmacias;
        private FarPoint.Win.Spread.FpSpread grdFarmacias;
        private FarPoint.Win.Spread.SheetView grdFarmacias_Sheet1;
        private System.Windows.Forms.Timer tmExistencias;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rdoTodas;
        private System.Windows.Forms.RadioButton rdoSinPromocion;
        private System.Windows.Forms.RadioButton rdoConPromocion;
        private System.Windows.Forms.Label lblProveedor;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label label1;
    }
}