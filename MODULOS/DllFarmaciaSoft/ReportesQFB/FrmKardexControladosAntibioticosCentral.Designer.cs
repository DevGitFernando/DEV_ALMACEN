﻿namespace DllFarmaciaSoft.ReportesQFB
{
    partial class FrmKardexControladosAntibioticosCentral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexControladosAntibioticosCentral));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameTipoSales = new System.Windows.Forms.GroupBox();
            this.rdoAntibioticos = new System.Windows.Forms.RadioButton();
            this.rdoControlados = new System.Windows.Forms.RadioButton();
            this.FrameTipoReporte = new System.Windows.Forms.GroupBox();
            this.rdoTodasClaves = new System.Windows.Forms.RadioButton();
            this.rdoPorProducto = new System.Windows.Forms.RadioButton();
            this.rdoPorClave = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLote = new System.Windows.Forms.CheckBox();
            this.txtClaveLote = new SC_ControlsCS.scTextBoxExt();
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
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoSales.SuspendLayout();
            this.FrameTipoReporte.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).BeginInit();
            this.FrameFechas.SuspendLayout();
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
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(986, 25);
            this.toolStripBarraMenu.TabIndex = 15;
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
            // FrameTipoSales
            // 
            this.FrameTipoSales.Controls.Add(this.rdoAntibioticos);
            this.FrameTipoSales.Controls.Add(this.rdoControlados);
            this.FrameTipoSales.Location = new System.Drawing.Point(12, 104);
            this.FrameTipoSales.Name = "FrameTipoSales";
            this.FrameTipoSales.Size = new System.Drawing.Size(251, 51);
            this.FrameTipoSales.TabIndex = 1;
            this.FrameTipoSales.TabStop = false;
            this.FrameTipoSales.Text = "Tipo de Claves";
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
            this.FrameTipoReporte.Location = new System.Drawing.Point(270, 104);
            this.FrameTipoReporte.Name = "FrameTipoReporte";
            this.FrameTipoReporte.Size = new System.Drawing.Size(366, 51);
            this.FrameTipoReporte.TabIndex = 2;
            this.FrameTipoReporte.TabStop = false;
            this.FrameTipoReporte.Text = "Tipo de Reporte";
            // 
            // rdoTodasClaves
            // 
            this.rdoTodasClaves.Checked = true;
            this.rdoTodasClaves.Location = new System.Drawing.Point(37, 20);
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
            this.rdoPorProducto.Location = new System.Drawing.Point(240, 19);
            this.rdoPorProducto.Name = "rdoPorProducto";
            this.rdoPorProducto.Size = new System.Drawing.Size(90, 17);
            this.rdoPorProducto.TabIndex = 1;
            this.rdoPorProducto.Text = "Por Producto";
            this.rdoPorProducto.UseVisualStyleBackColor = true;
            this.rdoPorProducto.CheckedChanged += new System.EventHandler(this.rdoPorProducto_CheckedChanged);
            // 
            // rdoPorClave
            // 
            this.rdoPorClave.Location = new System.Drawing.Point(146, 20);
            this.rdoPorClave.Name = "rdoPorClave";
            this.rdoPorClave.Size = new System.Drawing.Size(90, 15);
            this.rdoPorClave.TabIndex = 0;
            this.rdoPorClave.Text = "Por Clave";
            this.rdoPorClave.UseVisualStyleBackColor = true;
            this.rdoPorClave.CheckedChanged += new System.EventHandler(this.rdoPorClave_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLote);
            this.groupBox1.Controls.Add(this.txtClaveLote);
            this.groupBox1.Controls.Add(this.chkBuscarClave);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(965, 67);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del producto";
            // 
            // chkLote
            // 
            this.chkLote.Location = new System.Drawing.Point(498, 15);
            this.chkLote.Name = "chkLote";
            this.chkLote.Size = new System.Drawing.Size(115, 20);
            this.chkLote.TabIndex = 54;
            this.chkLote.Text = "Buscar Clave Lote";
            this.chkLote.UseVisualStyleBackColor = true;
            this.chkLote.CheckedChanged += new System.EventHandler(this.chkLote_CheckedChanged);
            // 
            // txtClaveLote
            // 
            this.txtClaveLote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveLote.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveLote.Decimales = 2;
            this.txtClaveLote.Enabled = false;
            this.txtClaveLote.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveLote.ForeColor = System.Drawing.Color.Black;
            this.txtClaveLote.Location = new System.Drawing.Point(619, 16);
            this.txtClaveLote.MaxLength = 50;
            this.txtClaveLote.Name = "txtClaveLote";
            this.txtClaveLote.PermitirApostrofo = false;
            this.txtClaveLote.PermitirNegativos = false;
            this.txtClaveLote.Size = new System.Drawing.Size(338, 20);
            this.txtClaveLote.TabIndex = 53;
            // 
            // chkBuscarClave
            // 
            this.chkBuscarClave.AutoSize = true;
            this.chkBuscarClave.Location = new System.Drawing.Point(325, 18);
            this.chkBuscarClave.Name = "chkBuscarClave";
            this.chkBuscarClave.Size = new System.Drawing.Size(139, 17);
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
            this.label1.Text = "ClaveSSA -- Producto :";
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
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(141, 41);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(816, 20);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "label4";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(141, 18);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(176, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "ClaveSSA -- Producto :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdMovtos);
            this.groupBox2.Location = new System.Drawing.Point(12, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(965, 348);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detallado de Movimientos";
            // 
            // grdMovtos
            // 
            this.grdMovtos.AccessibleDescription = "grdMovtos, Sheet1, Row 0, Column 0, ";
            this.grdMovtos.AllowUndo = false;
            this.grdMovtos.AllowUserZoom = false;
            this.grdMovtos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMovtos.Location = new System.Drawing.Point(11, 19);
            this.grdMovtos.Name = "grdMovtos";
            this.grdMovtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMovtos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMovtos_Sheet1});
            this.grdMovtos.Size = new System.Drawing.Size(947, 321);
            this.grdMovtos.TabIndex = 0;
            // 
            // grdMovtos_Sheet1
            // 
            this.grdMovtos_Sheet1.Reset();
            this.grdMovtos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMovtos_Sheet1.ColumnCount = 8;
            this.grdMovtos_Sheet1.RowCount = 14;
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave SSA";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Entrada";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Salida";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdMovtos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Label = "Fecha";
            this.grdMovtos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(0).Width = 79F;
            this.grdMovtos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdMovtos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdMovtos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(1).Width = 100F;
            this.grdMovtos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMovtos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdMovtos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(2).Width = 220F;
            this.grdMovtos_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdMovtos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Label = "Clave SSA";
            this.grdMovtos_Sheet1.Columns.Get(3).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Width = 92F;
            this.grdMovtos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(4).Label = "Descripción";
            this.grdMovtos_Sheet1.Columns.Get(4).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(4).Width = 220F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -1000D;
            numberCellType1.NegativeRed = true;
            numberCellType1.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.grdMovtos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(5).Label = "Entrada";
            this.grdMovtos_Sheet1.Columns.Get(5).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -1000D;
            numberCellType2.NegativeRed = true;
            numberCellType2.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.grdMovtos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Label = "Salida";
            this.grdMovtos_Sheet1.Columns.Get(6).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = -1000D;
            numberCellType3.NegativeRed = true;
            numberCellType3.Separator = ",";
            this.grdMovtos_Sheet1.Columns.Get(7).CellType = numberCellType3;
            this.grdMovtos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Label = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(7).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label4);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(642, 104);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(335, 51);
            this.FrameFechas.TabIndex = 3;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(214, 16);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(177, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 16);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(71, 16);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.cboEmpresas);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(12, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(965, 75);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(497, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(568, 46);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(352, 21);
            this.cboFarmacias.TabIndex = 2;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
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
            this.cboEmpresas.Size = new System.Drawing.Size(352, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(37, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Empresa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(99, 46);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(352, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(45, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Estado :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmKardexControladosAntibioticosCentral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 580);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameTipoReporte);
            this.Controls.Add(this.FrameTipoSales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmKardexControladosAntibioticosCentral";
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
            this.FrameFechas.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtClaveLote;
        private System.Windows.Forms.CheckBox chkLote;
    }
}