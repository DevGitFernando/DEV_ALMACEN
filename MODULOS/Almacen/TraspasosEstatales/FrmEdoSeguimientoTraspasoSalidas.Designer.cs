namespace Almacen.TraspasosEstatales
{
    partial class FrmEdoSeguimientoTraspasoSalidas
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEdoSeguimientoTraspasoSalidas));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdTransferencia = new FarPoint.Win.Spread.FpSpread();
            this.grdTransferencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActivarServicios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFarmaciasDestino = new SC_ControlsCS.scComboBoxExt();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox4.Location = new System.Drawing.Point(10, 549);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1164, 52);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de colores para consulta de transferencias";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.Location = new System.Drawing.Point(507, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con exito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.Location = new System.Drawing.Point(252, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(895, 16);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 25);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsultando
            // 
            this.lblConsultando.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(349, 16);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(25, 25);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(760, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(627, 16);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 25);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grdTransferencia);
            this.groupBox3.Location = new System.Drawing.Point(10, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1164, 444);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transferencias";
            // 
            // grdTransferencia
            // 
            this.grdTransferencia.AccessibleDescription = "grdTransferencia, Sheet1, Row 0, Column 0, ";
            this.grdTransferencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransferencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdTransferencia.Location = new System.Drawing.Point(9, 19);
            this.grdTransferencia.Name = "grdTransferencia";
            this.grdTransferencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdTransferencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdTransferencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdTransferencia_Sheet1});
            this.grdTransferencia.Size = new System.Drawing.Size(1146, 421);
            this.grdTransferencia.TabIndex = 0;
            // 
            // grdTransferencia_Sheet1
            // 
            this.grdTransferencia_Sheet1.Reset();
            this.grdTransferencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdTransferencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdTransferencia_Sheet1.ColumnCount = 10;
            this.grdTransferencia_Sheet1.RowCount = 15;
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Folio";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "EstadoRecibe";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Consultar";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Folio Entrada";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha de Entrada";
            textCellType1.MaxLength = 6;
            this.grdTransferencia_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdTransferencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdTransferencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Width = 62F;
            this.grdTransferencia_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdTransferencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTransferencia_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdTransferencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(1).Width = 220F;
            textCellType3.MaxLength = 500;
            this.grdTransferencia_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdTransferencia_Sheet1.Columns.Get(2).Label = "Url";
            this.grdTransferencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(2).Visible = false;
            this.grdTransferencia_Sheet1.Columns.Get(2).Width = 347F;
            this.grdTransferencia_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdTransferencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(3).Label = "Folio";
            this.grdTransferencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(3).Width = 88F;
            this.grdTransferencia_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Label = "Fecha";
            this.grdTransferencia_Sheet1.Columns.Get(4).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Width = 95F;
            this.grdTransferencia_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.grdTransferencia_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Label = "EstadoRecibe";
            this.grdTransferencia_Sheet1.Columns.Get(5).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Visible = false;
            this.grdTransferencia_Sheet1.Columns.Get(5).Width = 101F;
            this.grdTransferencia_Sheet1.Columns.Get(6).CellType = checkBoxCellType1;
            this.grdTransferencia_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Label = "Consultar";
            this.grdTransferencia_Sheet1.Columns.Get(6).Locked = false;
            this.grdTransferencia_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Width = 80F;
            this.grdTransferencia_Sheet1.Columns.Get(7).CellType = textCellType6;
            this.grdTransferencia_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Label = "Status";
            this.grdTransferencia_Sheet1.Columns.Get(7).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Width = 180F;
            this.grdTransferencia_Sheet1.Columns.Get(8).CellType = textCellType7;
            this.grdTransferencia_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Label = "Folio Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(8).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Width = 90F;
            this.grdTransferencia_Sheet1.Columns.Get(9).CellType = textCellType8;
            this.grdTransferencia_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Label = "Fecha de Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(9).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Width = 120F;
            this.grdTransferencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdTransferencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnActivarServicios,
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnActivarServicios
            // 
            this.btnActivarServicios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActivarServicios.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarServicios.Image")));
            this.btnActivarServicios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActivarServicios.Name = "btnActivarServicios";
            this.btnActivarServicios.Size = new System.Drawing.Size(23, 22);
            this.btnActivarServicios.Text = "Procesar";
            this.btnActivarServicios.Click += new System.EventHandler(this.btnActivarServicios_Click);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboFarmaciasDestino);
            this.groupBox1.Location = new System.Drawing.Point(10, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(698, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Origen";
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(113, 17);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(572, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Farmacia Destino :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmaciasDestino
            // 
            this.cboFarmaciasDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFarmaciasDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmaciasDestino.Data = "";
            this.cboFarmaciasDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmaciasDestino.Filtro = " 1 = 1";
            this.cboFarmaciasDestino.FormattingEnabled = true;
            this.cboFarmaciasDestino.ListaItemsBusqueda = 20;
            this.cboFarmaciasDestino.Location = new System.Drawing.Point(113, 43);
            this.cboFarmaciasDestino.MostrarToolTip = false;
            this.cboFarmaciasDestino.Name = "cboFarmaciasDestino";
            this.cboFarmaciasDestino.Size = new System.Drawing.Size(572, 21);
            this.cboFarmaciasDestino.TabIndex = 1;
            this.cboFarmaciasDestino.SelectedIndexChanged += new System.EventHandler(this.cboFarmaciasDestino_SelectedIndexChanged);
            // 
            // chkTodas
            // 
            this.chkTodas.Location = new System.Drawing.Point(18, 28);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(119, 24);
            this.chkTodas.TabIndex = 0;
            this.chkTodas.Text = " Seleccionar Todas";
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaFinal);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.dtpFechaInicial);
            this.groupBox2.Location = new System.Drawing.Point(713, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(200, 31);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(173, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fin :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Inicio :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(61, 31);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkTodas);
            this.groupBox5.Location = new System.Drawing.Point(1019, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(155, 71);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // FrmEdoSeguimientoTraspasoSalidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmEdoSeguimientoTraspasoSalidas";
            this.Text = "Seguimiento de Transferencia de Salida";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEdoSeguimientoTraspasoSalidas_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdTransferencia;
        private FarPoint.Win.Spread.SheetView grdTransferencia_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnActivarServicios;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmaciasDestino;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
    }
}