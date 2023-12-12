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
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType31 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType32 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType22 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType23 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType24 = new FarPoint.Win.Spread.CellType.NumberCellType();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1034, 25);
            this.toolStripBarraMenu.TabIndex = 0;
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
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
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
            // FrameTipoSales
            // 
            this.FrameTipoSales.Controls.Add(this.rdoClavesLibres);
            this.FrameTipoSales.Controls.Add(this.rdoAntibioticos);
            this.FrameTipoSales.Controls.Add(this.rdoControlados);
            this.FrameTipoSales.Location = new System.Drawing.Point(12, 28);
            this.FrameTipoSales.Name = "FrameTipoSales";
            this.FrameTipoSales.Size = new System.Drawing.Size(251, 51);
            this.FrameTipoSales.TabIndex = 1;
            this.FrameTipoSales.TabStop = false;
            this.FrameTipoSales.Text = "Tipo de Claves";
            // 
            // rdoClavesLibres
            // 
            this.rdoClavesLibres.Location = new System.Drawing.Point(101, 8);
            this.rdoClavesLibres.Name = "rdoClavesLibres";
            this.rdoClavesLibres.Size = new System.Drawing.Size(88, 15);
            this.rdoClavesLibres.TabIndex = 2;
            this.rdoClavesLibres.TabStop = true;
            this.rdoClavesLibres.Text = "Claves libres";
            this.rdoClavesLibres.UseVisualStyleBackColor = true;
            this.rdoClavesLibres.Visible = false;
            // 
            // rdoAntibioticos
            // 
            this.rdoAntibioticos.Location = new System.Drawing.Point(25, 20);
            this.rdoAntibioticos.Name = "rdoAntibioticos";
            this.rdoAntibioticos.Size = new System.Drawing.Size(82, 15);
            this.rdoAntibioticos.TabIndex = 0;
            this.rdoAntibioticos.TabStop = true;
            this.rdoAntibioticos.Text = "Antibióticos";
            this.rdoAntibioticos.UseVisualStyleBackColor = true;
            // 
            // rdoControlados
            // 
            this.rdoControlados.Location = new System.Drawing.Point(138, 20);
            this.rdoControlados.Name = "rdoControlados";
            this.rdoControlados.Size = new System.Drawing.Size(88, 15);
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
            this.FrameTipoReporte.Location = new System.Drawing.Point(270, 28);
            this.FrameTipoReporte.Name = "FrameTipoReporte";
            this.FrameTipoReporte.Size = new System.Drawing.Size(366, 51);
            this.FrameTipoReporte.TabIndex = 2;
            this.FrameTipoReporte.TabStop = false;
            this.FrameTipoReporte.Text = "Tipo de Reporte";
            // 
            // rdoTodasClaves
            // 
            this.rdoTodasClaves.Checked = true;
            this.rdoTodasClaves.Location = new System.Drawing.Point(29, 20);
            this.rdoTodasClaves.Name = "rdoTodasClaves";
            this.rdoTodasClaves.Size = new System.Drawing.Size(90, 15);
            this.rdoTodasClaves.TabIndex = 0;
            this.rdoTodasClaves.TabStop = true;
            this.rdoTodasClaves.Text = "Todas Claves";
            this.rdoTodasClaves.UseVisualStyleBackColor = true;
            this.rdoTodasClaves.CheckedChanged += new System.EventHandler(this.rdoTodasClaves_CheckedChanged);
            // 
            // rdoPorProducto
            // 
            this.rdoPorProducto.Location = new System.Drawing.Point(248, 19);
            this.rdoPorProducto.Name = "rdoPorProducto";
            this.rdoPorProducto.Size = new System.Drawing.Size(90, 17);
            this.rdoPorProducto.TabIndex = 2;
            this.rdoPorProducto.Text = "Por Producto";
            this.rdoPorProducto.UseVisualStyleBackColor = true;
            this.rdoPorProducto.CheckedChanged += new System.EventHandler(this.rdoPorProducto_CheckedChanged);
            // 
            // rdoPorClave
            // 
            this.rdoPorClave.Location = new System.Drawing.Point(146, 20);
            this.rdoPorClave.Name = "rdoPorClave";
            this.rdoPorClave.Size = new System.Drawing.Size(90, 15);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1013, 67);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Producto";
            // 
            // chkBuscarClave
            // 
            this.chkBuscarClave.Location = new System.Drawing.Point(325, 18);
            this.chkBuscarClave.Name = "chkBuscarClave";
            this.chkBuscarClave.Size = new System.Drawing.Size(137, 17);
            this.chkBuscarClave.TabIndex = 1;
            this.chkBuscarClave.Text = "Consulta por Clave SSA";
            this.chkBuscarClave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Clave SSA -- Producto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(65, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Clave :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(171, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(402, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(141, 41);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(857, 20);
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
            this.txtCodigo.Location = new System.Drawing.Point(141, 18);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(176, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1013, 451);
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
            this.grdMovtos.Location = new System.Drawing.Point(9, 19);
            this.grdMovtos.Name = "grdMovtos";
            this.grdMovtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMovtos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMovtos_Sheet1});
            this.grdMovtos.Size = new System.Drawing.Size(994, 423);
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
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción movimiento";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave SSA";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción comercial";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Entrada";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Salida";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(0).CellType = textCellType29;
            this.grdMovtos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Label = "Fecha";
            this.grdMovtos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Width = 75F;
            this.grdMovtos_Sheet1.Columns.Get(1).CellType = textCellType30;
            this.grdMovtos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdMovtos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Width = 90F;
            this.grdMovtos_Sheet1.Columns.Get(2).CellType = textCellType31;
            this.grdMovtos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(2).Label = "Descripción movimiento";
            this.grdMovtos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(2).Width = 220F;
            this.grdMovtos_Sheet1.Columns.Get(3).CellType = textCellType32;
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
            numberCellType22.DecimalPlaces = 0;
            numberCellType22.DecimalSeparator = ".";
            numberCellType22.MaximumValue = 10000000D;
            numberCellType22.MinimumValue = -1000D;
            numberCellType22.NegativeRed = true;
            numberCellType22.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(5).CellType = numberCellType22;
            this.grdMovtos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(5).Label = "Entrada";
            this.grdMovtos_Sheet1.Columns.Get(5).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType23.DecimalPlaces = 0;
            numberCellType23.DecimalSeparator = ".";
            numberCellType23.MaximumValue = 10000000D;
            numberCellType23.MinimumValue = -1000D;
            numberCellType23.NegativeRed = true;
            numberCellType23.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(6).CellType = numberCellType23;
            this.grdMovtos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Label = "Salida";
            this.grdMovtos_Sheet1.Columns.Get(6).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType24.DecimalPlaces = 0;
            numberCellType24.DecimalSeparator = ".";
            numberCellType24.MaximumValue = 10000000D;
            numberCellType24.MinimumValue = -1000D;
            numberCellType24.NegativeRed = true;
            numberCellType24.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(7).CellType = numberCellType24;
            this.grdMovtos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Label = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(7).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFecha
            // 
            this.FrameFecha.Controls.Add(this.dtpFechaFinal);
            this.FrameFecha.Controls.Add(this.label2);
            this.FrameFecha.Controls.Add(this.label4);
            this.FrameFecha.Controls.Add(this.dtpFechaInicial);
            this.FrameFecha.Location = new System.Drawing.Point(642, 28);
            this.FrameFecha.Name = "FrameFecha";
            this.FrameFecha.Size = new System.Drawing.Size(383, 51);
            this.FrameFecha.TabIndex = 3;
            this.FrameFecha.TabStop = false;
            this.FrameFecha.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(244, 16);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(207, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(42, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(89, 16);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrmKardexDetallado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 611);
            this.Controls.Add(this.FrameFecha);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameTipoReporte);
            this.Controls.Add(this.FrameTipoSales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmKardexDetallado";
            this.Text = "Kardex de Controlados y Antibióticos";
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
            this.PerformLayout();

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