namespace Farmacia.Inventario
{
    partial class FrmKardexProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexProducto));
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.lblProductoBloqueadoPorInventario = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdMovtos = new FarPoint.Win.Spread.FpSpread();
            this.grdMovtos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoDetallado = new System.Windows.Forms.RadioButton();
            this.rdoConcentrado = new System.Windows.Forms.RadioButton();
            this.btnEnTransito = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnCancelar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1512, 58);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 4);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(54, 55);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(12, 4);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblCodigoEAN);
            this.groupBox1.Controls.Add(this.lblProductoBloqueadoPorInventario);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(904, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Producto";
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.Location = new System.Drawing.Point(133, 49);
            this.lblCodigoEAN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(180, 25);
            this.lblCodigoEAN.TabIndex = 22;
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductoBloqueadoPorInventario
            // 
            this.lblProductoBloqueadoPorInventario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductoBloqueadoPorInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductoBloqueadoPorInventario.Location = new System.Drawing.Point(321, 20);
            this.lblProductoBloqueadoPorInventario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductoBloqueadoPorInventario.Name = "lblProductoBloqueadoPorInventario";
            this.lblProductoBloqueadoPorInventario.Size = new System.Drawing.Size(568, 25);
            this.lblProductoBloqueadoPorInventario.TabIndex = 21;
            this.lblProductoBloqueadoPorInventario.Text = "El producto se encuentra bloqueado por Inventario";
            this.lblProductoBloqueadoPorInventario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(87, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "label7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 132);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Clave :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(228, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(536, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.Location = new System.Drawing.Point(321, 49);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(568, 25);
            this.lblDescripcion.TabIndex = 11;
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(132, 20);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigo.MaxLength = 15;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(180, 22);
            this.txtCodigo.TabIndex = 11;
            this.txtCodigo.Text = "012345678901234";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(29, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Código EAN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdMovtos);
            this.groupBox2.Location = new System.Drawing.Point(12, 158);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1488, 582);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detallado de movimientos";
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
            this.grdMovtos.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdMovtos.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdMovtos.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdMovtos.HorizontalScrollBar.TabIndex = 2;
            this.grdMovtos.Location = new System.Drawing.Point(16, 22);
            this.grdMovtos.Margin = new System.Windows.Forms.Padding(4);
            this.grdMovtos.Name = "grdMovtos";
            this.grdMovtos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMovtos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMovtos_Sheet1});
            this.grdMovtos.Size = new System.Drawing.Size(1456, 547);
            this.grdMovtos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdMovtos.TabIndex = 4;
            this.grdMovtos.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdMovtos.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdMovtos.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdMovtos.VerticalScrollBar.TabIndex = 3;
            // 
            // grdMovtos_Sheet1
            // 
            this.grdMovtos_Sheet1.Reset();
            this.grdMovtos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMovtos_Sheet1.ColumnCount = 8;
            this.grdMovtos_Sheet1.RowCount = 16;
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Entrada";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Salida";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Existencia";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Costo / Precio";
            this.grdMovtos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdMovtos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdMovtos_Sheet1.ColumnHeader.Rows.Get(0).Height = 26F;
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
            this.grdMovtos_Sheet1.Columns.Get(1).Width = 111F;
            this.grdMovtos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMovtos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMovtos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdMovtos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(2).Width = 325F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdMovtos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMovtos_Sheet1.Columns.Get(3).Label = "Entrada";
            this.grdMovtos_Sheet1.Columns.Get(3).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(3).Width = 80F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdMovtos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMovtos_Sheet1.Columns.Get(4).Label = "Salida";
            this.grdMovtos_Sheet1.Columns.Get(4).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(4).Width = 80F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = -10000000D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdMovtos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMovtos_Sheet1.Columns.Get(5).Label = "Existencia";
            this.grdMovtos_Sheet1.Columns.Get(5).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(5).Width = 85F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.NegativeRed = true;
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowCurrencySymbol = false;
            currencyCellType1.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(6).CellType = currencyCellType1;
            this.grdMovtos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMovtos_Sheet1.Columns.Get(6).Label = "Costo / Precio";
            this.grdMovtos_Sheet1.Columns.Get(6).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(6).Width = 85F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.DecimalSeparator = ".";
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.NegativeRed = true;
            currencyCellType2.Separator = ",";
            currencyCellType2.ShowCurrencySymbol = false;
            currencyCellType2.ShowSeparator = true;
            this.grdMovtos_Sheet1.Columns.Get(7).CellType = currencyCellType2;
            this.grdMovtos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdMovtos_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdMovtos_Sheet1.Columns.Get(7).Locked = true;
            this.grdMovtos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMovtos_Sheet1.Columns.Get(7).Width = 85F;
            this.grdMovtos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdMovtos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdMovtos_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdMovtos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(924, 70);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(208, 86);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(71, 52);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaFinal.TabIndex = 1;
            this.dtpFechaFinal.ValueChanged += new System.EventHandler(this.dtpFechaFinal_ValueChanged);
            this.dtpFechaFinal.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaFinal_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Inicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(71, 20);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.dtpFechaInicial_ValueChanged);
            this.dtpFechaInicial.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaInicial_Validating);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rdoDetallado);
            this.groupBox4.Controls.Add(this.rdoConcentrado);
            this.groupBox4.Location = new System.Drawing.Point(1137, 70);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(133, 86);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo Reporte";
            // 
            // rdoDetallado
            // 
            this.rdoDetallado.Location = new System.Drawing.Point(13, 55);
            this.rdoDetallado.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDetallado.Name = "rdoDetallado";
            this.rdoDetallado.Size = new System.Drawing.Size(112, 18);
            this.rdoDetallado.TabIndex = 1;
            this.rdoDetallado.TabStop = true;
            this.rdoDetallado.Text = "Detallado";
            this.rdoDetallado.UseVisualStyleBackColor = true;
            // 
            // rdoConcentrado
            // 
            this.rdoConcentrado.Checked = true;
            this.rdoConcentrado.Location = new System.Drawing.Point(13, 25);
            this.rdoConcentrado.Margin = new System.Windows.Forms.Padding(4);
            this.rdoConcentrado.Name = "rdoConcentrado";
            this.rdoConcentrado.Size = new System.Drawing.Size(115, 18);
            this.rdoConcentrado.TabIndex = 0;
            this.rdoConcentrado.TabStop = true;
            this.rdoConcentrado.Text = "Concentrado";
            this.rdoConcentrado.UseVisualStyleBackColor = true;
            // 
            // btnEnTransito
            // 
            this.btnEnTransito.Location = new System.Drawing.Point(15, 23);
            this.btnEnTransito.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnTransito.Name = "btnEnTransito";
            this.btnEnTransito.Size = new System.Drawing.Size(196, 49);
            this.btnEnTransito.TabIndex = 6;
            this.btnEnTransito.Text = "Ver Traspasos";
            this.btnEnTransito.UseVisualStyleBackColor = true;
            this.btnEnTransito.Click += new System.EventHandler(this.btnEnTransito_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnEnTransito);
            this.groupBox5.Location = new System.Drawing.Point(1277, 70);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(223, 86);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Existencia en Tránsito";
            // 
            // FrmKardexProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 752);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmKardexProducto";
            this.ShowIcon = false;
            this.Text = "Kardex de Producto";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmKardexDeProducto_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovtos_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private FarPoint.Win.Spread.FpSpread grdMovtos;
        private FarPoint.Win.Spread.SheetView grdMovtos_Sheet1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoConcentrado;
        private System.Windows.Forms.RadioButton rdoDetallado;
        private System.Windows.Forms.Label lblProductoBloqueadoPorInventario;
        private System.Windows.Forms.Button btnEnTransito;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}