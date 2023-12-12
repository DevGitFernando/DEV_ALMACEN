namespace DllCompras.Pedidos
{
    partial class FrmCom_GenerarPedidoProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCom_GenerarPedidoProveedor));
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
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
            this.lblProv = new System.Windows.Forms.Label();
            this.txtIdFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRequeridaEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblEntregarEn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.txtPedido = new SC_ControlsCS.scTextBoxExt();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(958, 25);
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
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(9, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(940, 348);
            this.groupBox2.TabIndex = 2;
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
            this.grdClaves.Size = new System.Drawing.Size(918, 322);
            this.grdClaves.TabIndex = 0;
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 7;
            this.grdClaves_Sheet1.RowCount = 10;
            this.grdClaves_Sheet1.Cells.Get(0, 4).Value = 10;
            this.grdClaves_Sheet1.Cells.Get(0, 5).Value = 2;
            this.grdClaves_Sheet1.Cells.Get(0, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(0, 6).Value = 20D;
            this.grdClaves_Sheet1.Cells.Get(1, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(1, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(2, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(3, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(3, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(4, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(4, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(5, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(5, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(6, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(6, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(7, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(7, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(8, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(8, 6).Value = 0D;
            this.grdClaves_Sheet1.Cells.Get(9, 6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Cells.Get(9, 6).Value = 0D;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Precio Unitario Neto";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad Requerida";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Importe";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "IdClaveSSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 68F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 120F;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 120F;
            textCellType4.MaxLength = 1000;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 357F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Precio Unitario Neto";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 85F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Cantidad Requerida";
            this.grdClaves_Sheet1.Columns.Get(5).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 85F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(6).CellType = numberCellType3;
            this.grdClaves_Sheet1.Columns.Get(6).Formula = "RC[-2]*RC[-1]";
            this.grdClaves_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(6).Label = "Importe";
            this.grdClaves_Sheet1.Columns.Get(6).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Width = 95F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDomicilio);
            this.groupBox1.Controls.Add(this.lblNomProv);
            this.groupBox1.Controls.Add(this.lblProv);
            this.groupBox1.Controls.Add(this.txtIdFarmacia);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaRequeridaEntrega);
            this.groupBox1.Controls.Add(this.lblEntregarEn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.txtPedido);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales del Pedido";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Domicilio :";
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
            this.cboEstados.Location = new System.Drawing.Point(96, 68);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(181, 21);
            this.cboEstados.TabIndex = 3;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDomicilio.Location = new System.Drawing.Point(96, 94);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(832, 21);
            this.lblDomicilio.TabIndex = 20;
            // 
            // lblNomProv
            // 
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(96, 43);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(456, 20);
            this.lblNomProv.TabIndex = 19;
            this.lblNomProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProv
            // 
            this.lblProv.Location = new System.Drawing.Point(28, 45);
            this.lblProv.Name = "lblProv";
            this.lblProv.Size = new System.Drawing.Size(65, 16);
            this.lblProv.TabIndex = 18;
            this.lblProv.Text = "Proveedor :";
            this.lblProv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(361, 68);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(95, 20);
            this.txtIdFarmacia.TabIndex = 4;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFarmacia.TextChanged += new System.EventHandler(this.txtIdFarmacia_TextChanged);
            this.txtIdFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFarmacia_KeyDown);
            this.txtIdFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFarmacia_Validating);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(96, 118);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(832, 42);
            this.txtObservaciones.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Observaciones :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(637, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Requerida de Entrega :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRequeridaEntrega
            // 
            this.dtpFechaRequeridaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRequeridaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRequeridaEntrega.Location = new System.Drawing.Point(835, 43);
            this.dtpFechaRequeridaEntrega.Name = "dtpFechaRequeridaEntrega";
            this.dtpFechaRequeridaEntrega.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRequeridaEntrega.TabIndex = 2;
            // 
            // lblEntregarEn
            // 
            this.lblEntregarEn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntregarEn.Location = new System.Drawing.Point(462, 68);
            this.lblEntregarEn.Name = "lblEntregarEn";
            this.lblEntregarEn.Size = new System.Drawing.Size(466, 21);
            this.lblEntregarEn.TabIndex = 5;
            this.lblEntregarEn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(283, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Entregar en :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(745, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha Registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(835, 19);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // txtPedido
            // 
            this.txtPedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPedido.Decimales = 2;
            this.txtPedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPedido.ForeColor = System.Drawing.Color.Black;
            this.txtPedido.Location = new System.Drawing.Point(96, 19);
            this.txtPedido.MaxLength = 8;
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.PermitirApostrofo = false;
            this.txtPedido.PermitirNegativos = false;
            this.txtPedido.Size = new System.Drawing.Size(95, 20);
            this.txtPedido.TabIndex = 0;
            this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pedido :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCom_GenerarPedidoProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 550);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCom_GenerarPedidoProveedor";
            this.Text = "Registro de Pedido para Proveedor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_GenerarPedidoProveedor_Load);
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
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private SC_ControlsCS.scTextBoxExt txtPedido;
        private System.Windows.Forms.Label lblNomProv;
        private System.Windows.Forms.Label lblProv;
        private System.Windows.Forms.Label lblDomicilio;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}