namespace DllCompras.OrdenCompra
{
    partial class FrmCom_GenerarOrdenDeCompraProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCom_GenerarOrdenDeCompraProveedor));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPedido = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaPromesaEntrega = new System.Windows.Forms.DateTimePicker();
            this.txtEntregarEn = new SC_ControlsCS.scTextBoxExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRequeridaEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblEntregarEn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
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
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(9, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1167, 435);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedido";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(9, 19);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(1149, 409);
            this.grdClaves.TabIndex = 0;
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 8;
            this.grdClaves_Sheet1.RowCount = 10;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Precio Unitario Neto";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad Solicitada";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cantidad Confirmada";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "IdClaveSSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 68F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 97F;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 99F;
            textCellType8.MaxLength = 1000;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 357F;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType4;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Precio Unitario Neto";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 82F;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Cantidad Solicitada";
            this.grdClaves_Sheet1.Columns.Get(5).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 68F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(6).CellType = numberCellType5;
            this.grdClaves_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(6).Label = "Cantidad Confirmada";
            this.grdClaves_Sheet1.Columns.Get(6).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Width = 73F;
            numberCellType6.DecimalPlaces = 4;
            numberCellType6.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(7).CellType = numberCellType6;
            this.grdClaves_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdClaves_Sheet1.Columns.Get(7).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(7).Width = 95F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblPedido);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaPromesaEntrega);
            this.groupBox1.Controls.Add(this.txtEntregarEn);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaRequeridaEntrega);
            this.groupBox1.Controls.Add(this.lblEntregarEn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1167, 146);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales del Pedido";
            // 
            // lblPedido
            // 
            this.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPedido.Location = new System.Drawing.Point(253, 22);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(89, 20);
            this.lblPedido.TabIndex = 21;
            this.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(197, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Pedido :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(870, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Fecha Promesa de Entrega :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaPromesaEntrega
            // 
            this.dtpFechaPromesaEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaPromesaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaPromesaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPromesaEntrega.Location = new System.Drawing.Point(1062, 72);
            this.dtpFechaPromesaEntrega.Name = "dtpFechaPromesaEntrega";
            this.dtpFechaPromesaEntrega.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaPromesaEntrega.TabIndex = 18;
            // 
            // txtEntregarEn
            // 
            this.txtEntregarEn.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntregarEn.Decimales = 2;
            this.txtEntregarEn.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntregarEn.ForeColor = System.Drawing.Color.Black;
            this.txtEntregarEn.Location = new System.Drawing.Point(96, 46);
            this.txtEntregarEn.MaxLength = 8;
            this.txtEntregarEn.Name = "txtEntregarEn";
            this.txtEntregarEn.PermitirApostrofo = false;
            this.txtEntregarEn.PermitirNegativos = false;
            this.txtEntregarEn.Size = new System.Drawing.Size(95, 20);
            this.txtEntregarEn.TabIndex = 17;
            this.txtEntregarEn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(95, 96);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(1063, 45);
            this.txtObservaciones.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Observaciones :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(96, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Requerida de Entrega :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRequeridaEntrega
            // 
            this.dtpFechaRequeridaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRequeridaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRequeridaEntrega.Location = new System.Drawing.Point(287, 72);
            this.dtpFechaRequeridaEntrega.Name = "dtpFechaRequeridaEntrega";
            this.dtpFechaRequeridaEntrega.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRequeridaEntrega.TabIndex = 6;
            // 
            // lblEntregarEn
            // 
            this.lblEntregarEn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntregarEn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntregarEn.Location = new System.Drawing.Point(197, 46);
            this.lblEntregarEn.Name = "lblEntregarEn";
            this.lblEntregarEn.Size = new System.Drawing.Size(958, 20);
            this.lblEntregarEn.TabIndex = 5;
            this.lblEntregarEn.Text = "Entregar en :";
            this.lblEntregarEn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Entregar en :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(972, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha Registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1062, 22);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaRegistro.TabIndex = 2;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(96, 22);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(95, 20);
            this.txtFolio.TabIndex = 1;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(44, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCom_GenerarOrdenDeCompraProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCom_GenerarOrdenDeCompraProveedor";
            this.Text = "Registro de Orden de Compra para Proveedor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_GenerarOrdenDeCompraProveedor_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRequeridaEntrega;
        private System.Windows.Forms.Label lblEntregarEn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtEntregarEn;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaPromesaEntrega;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label7;
    }
}