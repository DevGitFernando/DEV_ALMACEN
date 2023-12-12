namespace DllFarmaciaSoft.Ventas
{
    partial class FrmConsultarExistenciasGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultarExistenciasGeneral));
            FarPoint.Win.Spread.CellType.TextCellType textCellType73 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType74 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType75 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType76 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType77 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType78 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType79 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType80 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoDetallado = new System.Windows.Forms.RadioButton();
            this.rdoConcentrado = new System.Windows.Forms.RadioButton();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.lblFinError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(680, 48);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(475, 21);
            this.cboFarmacias.TabIndex = 2;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rdoDetallado);
            this.groupBox3.Controls.Add(this.rdoConcentrado);
            this.groupBox3.Controls.Add(this.grdExistencia);
            this.groupBox3.Location = new System.Drawing.Point(9, 111);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1163, 405);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Existencias";
            // 
            // rdoDetallado
            // 
            this.rdoDetallado.Location = new System.Drawing.Point(172, 0);
            this.rdoDetallado.Name = "rdoDetallado";
            this.rdoDetallado.Size = new System.Drawing.Size(90, 16);
            this.rdoDetallado.TabIndex = 2;
            this.rdoDetallado.Text = "Detallado";
            this.rdoDetallado.UseVisualStyleBackColor = true;
            this.rdoDetallado.CheckedChanged += new System.EventHandler(this.rdoDetallado_CheckedChanged);
            // 
            // rdoConcentrado
            // 
            this.rdoConcentrado.Checked = true;
            this.rdoConcentrado.Location = new System.Drawing.Point(76, 0);
            this.rdoConcentrado.Name = "rdoConcentrado";
            this.rdoConcentrado.Size = new System.Drawing.Size(90, 16);
            this.rdoConcentrado.TabIndex = 1;
            this.rdoConcentrado.TabStop = true;
            this.rdoConcentrado.Text = "Concentrado";
            this.rdoConcentrado.UseVisualStyleBackColor = true;
            this.rdoConcentrado.CheckedChanged += new System.EventHandler(this.rdoConcentrado_CheckedChanged);
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(11, 21);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(1144, 380);
            this.grdExistencia.TabIndex = 0;
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 9;
            this.grdExistencia_Sheet1.RowCount = 12;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave Interna";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción Clave";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "IdProducto";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Producto";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Codigo EAN";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Clave Lote";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha Caducidad";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Existencia";
            this.grdExistencia_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            textCellType73.MaxLength = 6;
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType73;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "Clave Interna";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType74;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 120F;
            textCellType75.MaxLength = 7000;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType75;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Descripción Clave";
            this.grdExistencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 500F;
            this.grdExistencia_Sheet1.Columns.Get(3).CellType = textCellType76;
            this.grdExistencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Label = "IdProducto";
            this.grdExistencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Width = 80F;
            textCellType77.MaxLength = 3000;
            this.grdExistencia_Sheet1.Columns.Get(4).CellType = textCellType77;
            this.grdExistencia_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(4).Label = "Producto";
            this.grdExistencia_Sheet1.Columns.Get(4).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(4).Width = 300F;
            this.grdExistencia_Sheet1.Columns.Get(5).CellType = textCellType78;
            this.grdExistencia_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(5).Label = "Codigo EAN";
            this.grdExistencia_Sheet1.Columns.Get(5).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(5).Width = 90F;
            this.grdExistencia_Sheet1.Columns.Get(6).CellType = textCellType79;
            this.grdExistencia_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(6).Label = "Clave Lote";
            this.grdExistencia_Sheet1.Columns.Get(6).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(6).Width = 90F;
            this.grdExistencia_Sheet1.Columns.Get(7).CellType = textCellType80;
            this.grdExistencia_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(7).Label = "Fecha Caducidad";
            this.grdExistencia_Sheet1.Columns.Get(7).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(7).Width = 90F;
            numberCellType10.DecimalPlaces = 0;
            numberCellType10.DecimalSeparator = ".";
            numberCellType10.MaximumValue = 10000000D;
            numberCellType10.MinimumValue = 0D;
            numberCellType10.Separator = ",";
            numberCellType10.ShowSeparator = true;
            this.grdExistencia_Sheet1.Columns.Get(8).CellType = numberCellType10;
            this.grdExistencia_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(8).Label = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(8).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(8).Width = 87F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(989, 19);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 25);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(862, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con Errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(494, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con Éxito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(606, 19);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 25);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(149, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConsultando
            // 
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(238, 19);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(25, 25);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(9, 522);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1163, 52);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de Colores para Consulta de Existencia";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.cboEmpresas);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(9, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1163, 78);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(609, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(99, 19);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(475, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(33, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Empresa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(99, 48);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(475, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(41, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmConsultarExistenciasGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 581);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConsultarExistenciasGeneral";
            this.Text = "Consulta de Existencia General en Unidades";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConsultarExistenciasGeneralEnFarmacias_FormClosing);
            this.Load += new System.EventHandler(this.FrmConsultarExistenciasGeneralEnFarmacias_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConsultarExistenciasGeneralEnFarmacias_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
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
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoConcentrado;
        private System.Windows.Forms.RadioButton rdoDetallado;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}