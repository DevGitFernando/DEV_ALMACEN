namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmRechazosRecepcionOC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRechazosRecepcionOC));
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.dtpFechaResurtido = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoNotaCredito = new System.Windows.Forms.RadioButton();
            this.rdoResurtido = new System.Windows.Forms.RadioButton();
            this.txtRecibeRechazo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrden = new SC_ControlsCS.scTextBoxExt();
            this.lblIdProv = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdRechazos = new FarPoint.Win.Spread.FpSpread();
            this.grdRechazos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRechazos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRechazos_Sheet1)).BeginInit();
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
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(884, 25);
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
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEncabezado.Controls.Add(this.dtpFechaResurtido);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.rdoNotaCredito);
            this.FrameEncabezado.Controls.Add(this.rdoResurtido);
            this.FrameEncabezado.Controls.Add(this.txtRecibeRechazo);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Controls.Add(this.txtOrden);
            this.FrameEncabezado.Controls.Add(this.lblIdProv);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label14);
            this.FrameEncabezado.Controls.Add(this.label13);
            this.FrameEncabezado.Controls.Add(this.txtObservaciones);
            this.FrameEncabezado.Controls.Add(this.label10);
            this.FrameEncabezado.Controls.Add(this.lblProveedor);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Location = new System.Drawing.Point(10, 25);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(864, 158);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales de Compra";
            // 
            // dtpFechaResurtido
            // 
            this.dtpFechaResurtido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaResurtido.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaResurtido.Enabled = false;
            this.dtpFechaResurtido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaResurtido.Location = new System.Drawing.Point(759, 49);
            this.dtpFechaResurtido.Name = "dtpFechaResurtido";
            this.dtpFechaResurtido.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaResurtido.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(656, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Fecha Resurtido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoNotaCredito
            // 
            this.rdoNotaCredito.AutoSize = true;
            this.rdoNotaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNotaCredito.Location = new System.Drawing.Point(247, 26);
            this.rdoNotaCredito.Name = "rdoNotaCredito";
            this.rdoNotaCredito.Size = new System.Drawing.Size(114, 17);
            this.rdoNotaCredito.TabIndex = 29;
            this.rdoNotaCredito.TabStop = true;
            this.rdoNotaCredito.Text = "Nota de Crédito";
            this.rdoNotaCredito.UseVisualStyleBackColor = true;
            this.rdoNotaCredito.CheckedChanged += new System.EventHandler(this.rdoNotaCredito_CheckedChanged);
            // 
            // rdoResurtido
            // 
            this.rdoResurtido.AutoSize = true;
            this.rdoResurtido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoResurtido.Location = new System.Drawing.Point(247, 48);
            this.rdoResurtido.Name = "rdoResurtido";
            this.rdoResurtido.Size = new System.Drawing.Size(79, 17);
            this.rdoResurtido.TabIndex = 28;
            this.rdoResurtido.TabStop = true;
            this.rdoResurtido.Text = "Resurtido";
            this.rdoResurtido.UseVisualStyleBackColor = true;
            this.rdoResurtido.CheckedChanged += new System.EventHandler(this.rdoResurtido_CheckedChanged);
            // 
            // txtRecibeRechazo
            // 
            this.txtRecibeRechazo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecibeRechazo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRecibeRechazo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRecibeRechazo.Decimales = 2;
            this.txtRecibeRechazo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRecibeRechazo.ForeColor = System.Drawing.Color.Black;
            this.txtRecibeRechazo.Location = new System.Drawing.Point(151, 102);
            this.txtRecibeRechazo.MaxLength = 100;
            this.txtRecibeRechazo.Name = "txtRecibeRechazo";
            this.txtRecibeRechazo.PermitirApostrofo = false;
            this.txtRecibeRechazo.PermitirNegativos = false;
            this.txtRecibeRechazo.Size = new System.Drawing.Size(699, 20);
            this.txtRecibeRechazo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(25, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "Quién Recibe Rechazo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(151, 22);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(78, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrden
            // 
            this.txtOrden.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtOrden.Decimales = 2;
            this.txtOrden.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtOrden.ForeColor = System.Drawing.Color.Black;
            this.txtOrden.Location = new System.Drawing.Point(151, 49);
            this.txtOrden.MaxLength = 8;
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.PermitirApostrofo = false;
            this.txtOrden.PermitirNegativos = false;
            this.txtOrden.Size = new System.Drawing.Size(78, 20);
            this.txtOrden.TabIndex = 1;
            this.txtOrden.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOrden.Validating += new System.ComponentModel.CancelEventHandler(this.txtOrden_Validating);
            // 
            // lblIdProv
            // 
            this.lblIdProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdProv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdProv.Location = new System.Drawing.Point(151, 77);
            this.lblIdProv.Name = "lblIdProv";
            this.lblIdProv.Size = new System.Drawing.Size(78, 21);
            this.lblIdProv.TabIndex = 2;
            this.lblIdProv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(759, 19);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(665, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Fecha Registro :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(25, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 12);
            this.label13.TabIndex = 23;
            this.label13.Text = "Orden Compra :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(151, 127);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(699, 20);
            this.txtObservaciones.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(25, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(235, 77);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(615, 21);
            this.lblProveedor.TabIndex = 6;
            this.lblProveedor.Text = "Proveedor :";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(25, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proveedor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdRechazos);
            this.groupBox1.Location = new System.Drawing.Point(10, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(864, 317);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motivos por los cuales se rechaza la recepción de ordenes de compras.";
            // 
            // grdRechazos
            // 
            this.grdRechazos.AccessibleDescription = "grdOrdenes, Sheet1, Row 0, Column 0, ";
            this.grdRechazos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRechazos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdRechazos.Location = new System.Drawing.Point(13, 19);
            this.grdRechazos.Name = "grdRechazos";
            this.grdRechazos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdRechazos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdRechazos_Sheet1});
            this.grdRechazos.Size = new System.Drawing.Size(837, 290);
            this.grdRechazos.TabIndex = 0;
            // 
            // grdRechazos_Sheet1
            // 
            this.grdRechazos_Sheet1.Reset();
            this.grdRechazos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdRechazos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdRechazos_Sheet1.ColumnCount = 3;
            this.grdRechazos_Sheet1.RowCount = 10;
            this.grdRechazos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Rechazo";
            this.grdRechazos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Rechazo";
            this.grdRechazos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Marcar";
            this.grdRechazos_Sheet1.ColumnHeader.Rows.Get(0).Height = 34F;
            this.grdRechazos_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.grdRechazos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRechazos_Sheet1.Columns.Get(0).Label = "Id Rechazo";
            this.grdRechazos_Sheet1.Columns.Get(0).Locked = true;
            this.grdRechazos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRechazos_Sheet1.Columns.Get(0).Visible = false;
            this.grdRechazos_Sheet1.Columns.Get(0).Width = 75F;
            this.grdRechazos_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.grdRechazos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRechazos_Sheet1.Columns.Get(1).Label = "Rechazo";
            this.grdRechazos_Sheet1.Columns.Get(1).Locked = true;
            this.grdRechazos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRechazos_Sheet1.Columns.Get(1).Width = 450F;
            this.grdRechazos_Sheet1.Columns.Get(2).CellType = checkBoxCellType5;
            this.grdRechazos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRechazos_Sheet1.Columns.Get(2).Label = "Marcar";
            this.grdRechazos_Sheet1.Columns.Get(2).Locked = false;
            this.grdRechazos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRechazos_Sheet1.Columns.Get(2).Width = 90F;
            this.grdRechazos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdRechazos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmRechazosRecepcionOC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 511);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRechazosRecepcionOC";
            this.Text = "Motivos de Rechazo para Recepción de Orden de Compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRechazosRecepcionOC_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRechazos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRechazos_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdRechazos;
        private FarPoint.Win.Spread.SheetView grdRechazos_Sheet1;
        private SC_ControlsCS.scTextBoxExt txtOrden;
        private System.Windows.Forms.Label lblIdProv;
        private SC_ControlsCS.scTextBoxExt txtRecibeRechazo;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaResurtido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoNotaCredito;
        private System.Windows.Forms.RadioButton rdoResurtido;
    }
}