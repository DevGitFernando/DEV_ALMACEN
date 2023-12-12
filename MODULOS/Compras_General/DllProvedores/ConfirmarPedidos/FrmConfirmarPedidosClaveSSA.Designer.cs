namespace DllProveedores.ConfirmarPedidos
{
    partial class FrmConfirmarPedidosClaveSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmarPedidosClaveSSA));
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtObservacionesComprador = new SC_ControlsCS.scTextBoxExt();
            this.lblIdEstado = new System.Windows.Forms.Label();
            this.lblIdEmpresa = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaPromesaEntrega = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRequeridaEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblEntregarEn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.txtPedido = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(831, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtObservacionesComprador);
            this.groupBox1.Controls.Add(this.lblIdEstado);
            this.groupBox1.Controls.Add(this.lblIdEmpresa);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaPromesaEntrega);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaRequeridaEntrega);
            this.groupBox1.Controls.Add(this.lblEntregarEn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.txtPedido);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(809, 203);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales del Pedido";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(95, 146);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(704, 45);
            this.txtObservaciones.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Observaciones :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservacionesComprador
            // 
            this.txtObservacionesComprador.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservacionesComprador.Decimales = 2;
            this.txtObservacionesComprador.Enabled = false;
            this.txtObservacionesComprador.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservacionesComprador.ForeColor = System.Drawing.Color.Black;
            this.txtObservacionesComprador.Location = new System.Drawing.Point(95, 95);
            this.txtObservacionesComprador.MaxLength = 100;
            this.txtObservacionesComprador.Multiline = true;
            this.txtObservacionesComprador.Name = "txtObservacionesComprador";
            this.txtObservacionesComprador.PermitirApostrofo = false;
            this.txtObservacionesComprador.PermitirNegativos = false;
            this.txtObservacionesComprador.Size = new System.Drawing.Size(704, 45);
            this.txtObservacionesComprador.TabIndex = 5;
            // 
            // lblIdEstado
            // 
            this.lblIdEstado.Location = new System.Drawing.Point(356, 26);
            this.lblIdEstado.Name = "lblIdEstado";
            this.lblIdEstado.Size = new System.Drawing.Size(50, 16);
            this.lblIdEstado.TabIndex = 15;
            this.lblIdEstado.Text = "IdEstado";
            this.lblIdEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIdEstado.Visible = false;
            // 
            // lblIdEmpresa
            // 
            this.lblIdEmpresa.Location = new System.Drawing.Point(267, 26);
            this.lblIdEmpresa.Name = "lblIdEmpresa";
            this.lblIdEmpresa.Size = new System.Drawing.Size(59, 16);
            this.lblIdEmpresa.TabIndex = 14;
            this.lblIdEmpresa.Text = "IdEmpresa";
            this.lblIdEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIdEmpresa.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 27);
            this.label6.TabIndex = 10;
            this.label6.Text = "Observaciones Compras : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(537, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fecha promesa de entrega :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaPromesaEntrega
            // 
            this.dtpFechaPromesaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaPromesaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPromesaEntrega.Location = new System.Drawing.Point(706, 73);
            this.dtpFechaPromesaEntrega.Name = "dtpFechaPromesaEntrega";
            this.dtpFechaPromesaEntrega.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaPromesaEntrega.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha requerida de entrega :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRequeridaEntrega
            // 
            this.dtpFechaRequeridaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRequeridaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRequeridaEntrega.Location = new System.Drawing.Point(270, 72);
            this.dtpFechaRequeridaEntrega.Name = "dtpFechaRequeridaEntrega";
            this.dtpFechaRequeridaEntrega.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRequeridaEntrega.TabIndex = 3;
            // 
            // lblEntregarEn
            // 
            this.lblEntregarEn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntregarEn.Location = new System.Drawing.Point(96, 48);
            this.lblEntregarEn.Name = "lblEntregarEn";
            this.lblEntregarEn.Size = new System.Drawing.Size(703, 20);
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
            this.label2.Location = new System.Drawing.Point(616, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(706, 25);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRegistro.TabIndex = 2;
            // 
            // txtPedido
            // 
            this.txtPedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPedido.Decimales = 2;
            this.txtPedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPedido.ForeColor = System.Drawing.Color.Black;
            this.txtPedido.Location = new System.Drawing.Point(96, 25);
            this.txtPedido.MaxLength = 8;
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.PermitirApostrofo = false;
            this.txtPedido.PermitirNegativos = false;
            this.txtPedido.Size = new System.Drawing.Size(95, 20);
            this.txtPedido.TabIndex = 1;
            this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPedido.Validating += new System.ComponentModel.CancelEventHandler(this.txtPedido_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(44, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(12, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(809, 277);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedido";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(10, 18);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(789, 253);
            this.grdClaves.TabIndex = 0;
            this.grdClaves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdClaves_KeyDown);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 6;
            this.grdClaves_Sheet1.RowCount = 10;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Precio Unitario Neto";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad Requerida";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad Surtible";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "IdClaveSSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 68F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 130F;
            textCellType9.MaxLength = 1000;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType9;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 357F;
            numberCellType7.DecimalPlaces = 4;
            numberCellType7.MinimumValue = 0;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = numberCellType7;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Precio Unitario Neto";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 75F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.MaximumValue = 10000000;
            numberCellType8.MinimumValue = 0;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType8;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Cantidad Requerida";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 85F;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.MaximumValue = 10000000;
            numberCellType9.MinimumValue = 0;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = numberCellType9;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Cantidad Surtible";
            this.grdClaves_Sheet1.Columns.Get(5).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 85F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 524);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(831, 24);
            this.label11.TabIndex = 11;
            this.label11.Text = "<F5>Registrar Codigos a Surtir";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmConfirmarPedidosClaveSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 548);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfirmarPedidosClaveSSA";
            this.Text = "Confirmación de Pedidos por Clave";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfirmarPedidosClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEntregarEn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRequeridaEntrega;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaPromesaEntrega;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIdEstado;
        private System.Windows.Forms.Label lblIdEmpresa;
        private SC_ControlsCS.scTextBoxExt txtObservacionesComprador;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label7;
    }
}