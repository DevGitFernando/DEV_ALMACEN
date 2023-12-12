namespace OficinaCentral.CfgPrecios
{
    partial class FrmPreciosClavesSSA_Programas
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreciosClavesSSA_Programas));
            this.FrameListaProductos = new System.Windows.Forms.GroupBox();
            this.grpProductos = new FarPoint.Win.Spread.FpSpread();
            this.grpProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPrecio = new SC_ControlsCS.scNumericTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSubPrograma = new System.Windows.Forms.Label();
            this.txtSubPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPrecioUnitario = new System.Windows.Forms.Label();
            this.lblContenidoPaquete = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTipoInsumo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblFactor = new System.Windows.Forms.Label();
            this.FrameListaProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameListaProductos
            // 
            this.FrameListaProductos.Controls.Add(this.grpProductos);
            this.FrameListaProductos.Location = new System.Drawing.Point(16, 246);
            this.FrameListaProductos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameListaProductos.Name = "FrameListaProductos";
            this.FrameListaProductos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameListaProductos.Size = new System.Drawing.Size(1168, 262);
            this.FrameListaProductos.TabIndex = 3;
            this.FrameListaProductos.TabStop = false;
            this.FrameListaProductos.Text = "Claves SSA asignadas a Sub-Cliente";
            // 
            // grpProductos
            // 
            this.grpProductos.AccessibleDescription = "grpProductos, Sheet1, Row 0, Column 0, ";
            this.grpProductos.AllowUserZoom = false;
            this.grpProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpProductos.Location = new System.Drawing.Point(15, 21);
            this.grpProductos.Margin = new System.Windows.Forms.Padding(4);
            this.grpProductos.Name = "grpProductos";
            this.grpProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grpProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grpProductos_Sheet1});
            this.grpProductos.Size = new System.Drawing.Size(1143, 234);
            this.grpProductos.TabIndex = 0;
            this.grpProductos.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grpProductos_CellDoubleClick);
            // 
            // grpProductos_Sheet1
            // 
            this.grpProductos_Sheet1.Reset();
            this.grpProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grpProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grpProductos_Sheet1.ColumnCount = 8;
            this.grpProductos_Sheet1.RowCount = 15;
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdPrograma";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Programa";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "IdSubPrograma";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Sub - Programa";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Clave SSA";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Descripción";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Precio";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grpProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.grpProductos_Sheet1.Columns.Get(0).CellType = textCellType8;
            this.grpProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(0).Label = "IdPrograma";
            this.grpProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(0).Visible = false;
            this.grpProductos_Sheet1.Columns.Get(0).Width = 65F;
            textCellType9.MaxLength = 20;
            this.grpProductos_Sheet1.Columns.Get(1).CellType = textCellType9;
            this.grpProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(1).Label = "Programa";
            this.grpProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(1).Width = 110F;
            textCellType10.MaxLength = 1000;
            this.grpProductos_Sheet1.Columns.Get(2).CellType = textCellType10;
            this.grpProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grpProductos_Sheet1.Columns.Get(2).Label = "IdSubPrograma";
            this.grpProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(2).Visible = false;
            this.grpProductos_Sheet1.Columns.Get(2).Width = 74F;
            this.grpProductos_Sheet1.Columns.Get(3).CellType = textCellType11;
            this.grpProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grpProductos_Sheet1.Columns.Get(3).Label = "Sub - Programa";
            this.grpProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(3).Width = 140F;
            this.grpProductos_Sheet1.Columns.Get(4).CellType = textCellType12;
            this.grpProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(4).Label = "Clave SSA";
            this.grpProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(4).Width = 90F;
            textCellType13.MaxLength = 700;
            this.grpProductos_Sheet1.Columns.Get(5).CellType = textCellType13;
            this.grpProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grpProductos_Sheet1.Columns.Get(5).Label = "Descripción";
            this.grpProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(5).Width = 299F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 999999999.9999D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grpProductos_Sheet1.Columns.Get(6).CellType = numberCellType2;
            this.grpProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grpProductos_Sheet1.Columns.Get(6).Label = "Precio";
            this.grpProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(6).Width = 85F;
            this.grpProductos_Sheet1.Columns.Get(7).CellType = textCellType14;
            this.grpProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(7).Label = "Status";
            this.grpProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(7).Width = 75F;
            this.grpProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grpProductos_Sheet1.RowHeader.Columns.Get(0).Width = 36F;
            this.grpProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2,
            this.btnCancelar,
            this.toolStripSeparator3,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1199, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
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
            this.btnEjecutar.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrecio);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblSubPrograma);
            this.groupBox1.Controls.Add(this.txtSubPrograma);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPrograma);
            this.groupBox1.Controls.Add(this.txtPrograma);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(16, 181);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1168, 64);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de programa";
            // 
            // txtPrecio
            // 
            this.txtPrecio.AllowNegative = true;
            this.txtPrecio.DigitsInGroup = 3;
            this.txtPrecio.Flags = 7680;
            this.txtPrecio.Location = new System.Drawing.Point(1022, 24);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecio.MaxDecimalPlaces = 4;
            this.txtPrecio.MaxLength = 15;
            this.txtPrecio.MaxWholeDigits = 15;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Prefix = "";
            this.txtPrecio.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecio.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecio.Size = new System.Drawing.Size(132, 22);
            this.txtPrecio.TabIndex = 45;
            this.txtPrecio.Text = "1.0000";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(935, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 44;
            this.label10.Text = "Precio :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubPrograma
            // 
            this.lblSubPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPrograma.Location = new System.Drawing.Point(674, 27);
            this.lblSubPrograma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubPrograma.Name = "lblSubPrograma";
            this.lblSubPrograma.Size = new System.Drawing.Size(276, 26);
            this.lblSubPrograma.TabIndex = 43;
            this.lblSubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPrograma
            // 
            this.txtSubPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPrograma.Decimales = 2;
            this.txtSubPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtSubPrograma.Location = new System.Drawing.Point(588, 29);
            this.txtSubPrograma.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPrograma.MaxLength = 4;
            this.txtSubPrograma.Name = "txtSubPrograma";
            this.txtSubPrograma.PermitirApostrofo = false;
            this.txtSubPrograma.PermitirNegativos = false;
            this.txtSubPrograma.Size = new System.Drawing.Size(77, 22);
            this.txtSubPrograma.TabIndex = 1;
            this.txtSubPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPrograma.TextChanged += new System.EventHandler(this.txtSubPrograma_TextChanged);
            this.txtSubPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPrograma_KeyDown);
            this.txtSubPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPrograma_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(478, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 42;
            this.label3.Text = "Sub-Programa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrograma
            // 
            this.lblPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrograma.Location = new System.Drawing.Point(187, 27);
            this.lblPrograma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(276, 26);
            this.lblPrograma.TabIndex = 40;
            this.lblPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrograma
            // 
            this.txtPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPrograma.Decimales = 2;
            this.txtPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtPrograma.Location = new System.Drawing.Point(100, 29);
            this.txtPrograma.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrograma.MaxLength = 4;
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.PermitirApostrofo = false;
            this.txtPrograma.PermitirNegativos = false;
            this.txtPrograma.Size = new System.Drawing.Size(77, 22);
            this.txtPrograma.TabIndex = 0;
            this.txtPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrograma.TextChanged += new System.EventHandler(this.txtPrograma_TextChanged);
            this.txtPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrograma_KeyDown);
            this.txtPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrograma_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(20, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.TabIndex = 39;
            this.label7.Text = "Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblFactor);
            this.groupBox3.Controls.Add(this.lblPrecio);
            this.groupBox3.Controls.Add(this.lblPorcentaje);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblPrecioUnitario);
            this.groupBox3.Controls.Add(this.lblContenidoPaquete);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblTipoInsumo);
            this.groupBox3.Controls.Add(this.lblDescripcion);
            this.groupBox3.Controls.Add(this.txtClaveSSA);
            this.groupBox3.Controls.Add(this.lblClaveSSA);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(16, 32);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1168, 148);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Clave SSA";
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.Location = new System.Drawing.Point(876, 111);
            this.lblPorcentaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(144, 22);
            this.lblPorcentaje.TabIndex = 60;
            this.lblPorcentaje.Text = "Factor :";
            this.lblPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(876, 84);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 16);
            this.label9.TabIndex = 58;
            this.label9.Text = "Precio unitario :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(876, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "Contenido paquete :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecioUnitario
            // 
            this.lblPrecioUnitario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(1022, 79);
            this.lblPrecioUnitario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecioUnitario.Name = "lblPrecioUnitario";
            this.lblPrecioUnitario.Size = new System.Drawing.Size(132, 25);
            this.lblPrecioUnitario.TabIndex = 56;
            this.lblPrecioUnitario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContenidoPaquete
            // 
            this.lblContenidoPaquete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContenidoPaquete.Location = new System.Drawing.Point(1022, 48);
            this.lblContenidoPaquete.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContenidoPaquete.Name = "lblContenidoPaquete";
            this.lblContenidoPaquete.Size = new System.Drawing.Size(132, 25);
            this.lblContenidoPaquete.TabIndex = 55;
            this.lblContenidoPaquete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(876, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Precio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTipoInsumo
            // 
            this.lblTipoInsumo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoInsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoInsumo.Location = new System.Drawing.Point(520, 45);
            this.lblTipoInsumo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoInsumo.Name = "lblTipoInsumo";
            this.lblTipoInsumo.Size = new System.Drawing.Size(308, 25);
            this.lblTipoInsumo.TabIndex = 20;
            this.lblTipoInsumo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(20, 76);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(808, 60);
            this.lblDescripcion.TabIndex = 17;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.Enabled = false;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(20, 46);
            this.txtClaveSSA.Margin = new System.Windows.Forms.Padding(4);
            this.txtClaveSSA.MaxLength = 8;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(179, 22);
            this.txtClaveSSA.TabIndex = 16;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveSSA.Location = new System.Drawing.Point(206, 45);
            this.lblClaveSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(302, 25);
            this.lblClaveSSA.TabIndex = 18;
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Clave Interna :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecio
            // 
            this.lblPrecio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrecio.Location = new System.Drawing.Point(1022, 16);
            this.lblPrecio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(132, 25);
            this.lblPrecio.TabIndex = 61;
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFactor
            // 
            this.lblFactor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFactor.Location = new System.Drawing.Point(1022, 110);
            this.lblFactor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(132, 25);
            this.lblFactor.TabIndex = 62;
            this.lblFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPreciosClavesSSA_Programas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 520);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameListaProductos);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmPreciosClavesSSA_Programas";
            this.Text = "Asignación de Precios por Clave SSA por Programas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPreciosClavesSSA_Programas_Load);
            this.FrameListaProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameListaProductos;
        private FarPoint.Win.Spread.FpSpread grpProductos;
        private FarPoint.Win.Spread.SheetView grpProductos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSubPrograma;
        private SC_ControlsCS.scTextBoxExt txtSubPrograma;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPrograma;
        private SC_ControlsCS.scTextBoxExt txtPrograma;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTipoInsumo;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scNumericTextBox txtPrecio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPrecioUnitario;
        private System.Windows.Forms.Label lblContenidoPaquete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Label lblPrecio;
    }
}