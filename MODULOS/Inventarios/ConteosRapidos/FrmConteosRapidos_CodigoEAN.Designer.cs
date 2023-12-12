namespace Inventarios.ConteosRapidos
{
    partial class FrmConteosRapidos_CodigoEAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConteosRapidos_CodigoEAN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType13 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType14 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType15 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirReportesInternos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProductosNE = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTituloCapturaHabilitada = new System.Windows.Forms.Label();
            this.chkActivar = new System.Windows.Forms.CheckBox();
            this.txtEAN = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameConteos = new System.Windows.Forms.GroupBox();
            this.chkConteo3 = new System.Windows.Forms.CheckBox();
            this.chkConteo1 = new System.Windows.Forms.CheckBox();
            this.chkConteo2 = new System.Windows.Forms.CheckBox();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameConteos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
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
            this.toolStripSeparator4,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnImprimirReportesInternos,
            this.toolStripSeparator3,
            this.btnProductosNE});
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
            this.btnCancelar.Text = "&Cancelar (CTRL + E) ";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimirReportesInternos
            // 
            this.btnImprimirReportesInternos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimirReportesInternos.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirReportesInternos.Image")));
            this.btnImprimirReportesInternos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimirReportesInternos.Name = "btnImprimirReportesInternos";
            this.btnImprimirReportesInternos.Size = new System.Drawing.Size(23, 22);
            this.btnImprimirReportesInternos.Text = "Imprimir reportes internos";
            this.btnImprimirReportesInternos.Click += new System.EventHandler(this.btnImprimirReportesInternos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProductosNE
            // 
            this.btnProductosNE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProductosNE.Image = ((System.Drawing.Image)(resources.GetObject("btnProductosNE.Image")));
            this.btnProductosNE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProductosNE.Name = "btnProductosNE";
            this.btnProductosNE.Size = new System.Drawing.Size(23, 22);
            this.btnProductosNE.Text = "Productos No Identificados";
            this.btnProductosNE.Click += new System.EventHandler(this.btnProductosNE_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblTituloCapturaHabilitada);
            this.groupBox1.Controls.Add(this.chkActivar);
            this.groupBox1.Controls.Add(this.txtEAN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FrameConteos);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1015, 166);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales del Inventario";
            // 
            // lblTituloCapturaHabilitada
            // 
            this.lblTituloCapturaHabilitada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTituloCapturaHabilitada.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloCapturaHabilitada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCapturaHabilitada.Location = new System.Drawing.Point(717, 100);
            this.lblTituloCapturaHabilitada.Name = "lblTituloCapturaHabilitada";
            this.lblTituloCapturaHabilitada.Size = new System.Drawing.Size(277, 20);
            this.lblTituloCapturaHabilitada.TabIndex = 29;
            this.lblTituloCapturaHabilitada.Text = "CANCELADA";
            this.lblTituloCapturaHabilitada.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTituloCapturaHabilitada.Visible = false;
            // 
            // chkActivar
            // 
            this.chkActivar.Location = new System.Drawing.Point(264, 100);
            this.chkActivar.Name = "chkActivar";
            this.chkActivar.Size = new System.Drawing.Size(299, 19);
            this.chkActivar.TabIndex = 4;
            this.chkActivar.Text = "Habilitar / Deshabilitar Captura de productos adicionales";
            this.chkActivar.UseVisualStyleBackColor = true;
            this.chkActivar.CheckedChanged += new System.EventHandler(this.chkActivar_CheckedChanged);
            // 
            // txtEAN
            // 
            this.txtEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEAN.Decimales = 2;
            this.txtEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtEAN.ForeColor = System.Drawing.Color.Black;
            this.txtEAN.Location = new System.Drawing.Point(97, 98);
            this.txtEAN.MaxLength = 20;
            this.txtEAN.Name = "txtEAN";
            this.txtEAN.PermitirApostrofo = false;
            this.txtEAN.PermitirNegativos = false;
            this.txtEAN.Size = new System.Drawing.Size(158, 20);
            this.txtEAN.TabIndex = 3;
            this.txtEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtEAN_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Código EAN :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameConteos
            // 
            this.FrameConteos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameConteos.Controls.Add(this.chkConteo3);
            this.FrameConteos.Controls.Add(this.chkConteo1);
            this.FrameConteos.Controls.Add(this.chkConteo2);
            this.FrameConteos.Location = new System.Drawing.Point(97, 121);
            this.FrameConteos.Name = "FrameConteos";
            this.FrameConteos.Size = new System.Drawing.Size(908, 39);
            this.FrameConteos.TabIndex = 5;
            this.FrameConteos.TabStop = false;
            this.FrameConteos.Text = "Conteos Realizados";
            // 
            // chkConteo3
            // 
            this.chkConteo3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkConteo3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo3.Location = new System.Drawing.Point(649, 16);
            this.chkConteo3.Name = "chkConteo3";
            this.chkConteo3.Size = new System.Drawing.Size(83, 17);
            this.chkConteo3.TabIndex = 2;
            this.chkConteo3.Text = "Conteo # 3";
            this.chkConteo3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo3.UseVisualStyleBackColor = true;
            // 
            // chkConteo1
            // 
            this.chkConteo1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkConteo1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo1.Location = new System.Drawing.Point(176, 16);
            this.chkConteo1.Name = "chkConteo1";
            this.chkConteo1.Size = new System.Drawing.Size(83, 17);
            this.chkConteo1.TabIndex = 0;
            this.chkConteo1.Text = "Conteo # 1";
            this.chkConteo1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo1.UseVisualStyleBackColor = true;
            // 
            // chkConteo2
            // 
            this.chkConteo2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkConteo2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo2.Location = new System.Drawing.Point(415, 16);
            this.chkConteo2.Name = "chkConteo2";
            this.chkConteo2.Size = new System.Drawing.Size(83, 17);
            this.chkConteo2.TabIndex = 1;
            this.chkConteo2.Text = "Conteo # 2";
            this.chkConteo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConteo2.UseVisualStyleBackColor = true;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(97, 52);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(908, 41);
            this.txtObservaciones.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(914, 25);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(806, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(203, 27);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 5;
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
            this.txtFolio.Location = new System.Drawing.Point(97, 27);
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
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(12, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1014, 455);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Inventario";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(9, 19);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(995, 427);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 6;
            this.grdProductos_Sheet1.RowCount = 18;
            this.grdProductos_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Unidad de Medida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Existencia";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad Bodega";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType13.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType13.MaxLength = 20;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType13;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType14;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 350F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType15;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Unidad de Medida";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 170F;
            numberCellType13.DecimalPlaces = 0;
            numberCellType13.DecimalSeparator = ".";
            numberCellType13.MaximumValue = 10000000D;
            numberCellType13.MinimumValue = -10000000D;
            numberCellType13.Separator = ",";
            numberCellType13.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType13;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Existencia";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 80F;
            numberCellType14.DecimalPlaces = 0;
            numberCellType14.MaximumValue = 10000000D;
            numberCellType14.MinimumValue = -10000000D;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType14;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType15.DecimalPlaces = 0;
            numberCellType15.DecimalSeparator = ".";
            numberCellType15.MaximumValue = 10000000D;
            numberCellType15.MinimumValue = 0D;
            numberCellType15.Separator = ",";
            numberCellType15.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType15;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Cantidad Bodega";
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 70F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmConteosRapidos_CodigoEAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 661);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConteosRapidos_CodigoEAN";
            this.Text = "Conteos rápidos por Códigos EAN";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConteosRapidos_CodigoEAN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConteosRapidos_CodigoEAN_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameConteos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.CheckBox chkConteo3;
        private System.Windows.Forms.CheckBox chkConteo2;
        private System.Windows.Forms.CheckBox chkConteo1;
        private System.Windows.Forms.GroupBox FrameConteos;
        private System.Windows.Forms.CheckBox chkActivar;
        private SC_ControlsCS.scTextBoxExt txtEAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProductosNE;
        private System.Windows.Forms.Label lblTituloCapturaHabilitada;
        private System.Windows.Forms.ToolStripButton btnImprimirReportesInternos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}