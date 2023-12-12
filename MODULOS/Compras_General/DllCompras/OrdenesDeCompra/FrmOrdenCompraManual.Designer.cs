namespace DllCompras.OrdenesDeCompra
{
    partial class FrmOrdenCompraManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdenCompraManual));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarPDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCerrarOrden = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblImpteTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
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
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnExportarPDF,
            this.toolStripSeparator4,
            this.btnCerrarOrden});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(970, 25);
            this.toolStripBarraMenu.TabIndex = 2;
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
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPDF.Image")));
            this.btnExportarPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(23, 22);
            this.btnExportarPDF.Text = "Exportar a PDF";
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCerrarOrden
            // 
            this.btnCerrarOrden.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCerrarOrden.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarOrden.Image")));
            this.btnCerrarOrden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrarOrden.Name = "btnCerrarOrden";
            this.btnCerrarOrden.Size = new System.Drawing.Size(23, 22);
            this.btnCerrarOrden.Text = "Cerrar Orden";
            this.btnCerrarOrden.Click += new System.EventHandler(this.btnCerrarOrden_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblImpteTotal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(10, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(953, 295);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedido";
            // 
            // lblImpteTotal
            // 
            this.lblImpteTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImpteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpteTotal.Location = new System.Drawing.Point(791, 267);
            this.lblImpteTotal.Name = "lblImpteTotal";
            this.lblImpteTotal.Size = new System.Drawing.Size(135, 20);
            this.lblImpteTotal.TabIndex = 28;
            this.lblImpteTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(679, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Importe Total :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdProductos.Location = new System.Drawing.Point(10, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(933, 248);
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
            this.grdProductos_Sheet1.ColumnCount = 10;
            this.grdProductos_Sheet1.RowCount = 10;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 9).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Precio";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descuento";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "TasaIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Iva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Precio Unitario";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 4;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Id Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 80F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 480F;
            numberCellType1.DecimalPlaces = 4;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Precio";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            numberCellType2.DecimalPlaces = 4;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Descuento";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 100F;
            numberCellType3.DecimalPlaces = 4;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "TasaIva";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 70F;
            numberCellType4.DecimalPlaces = 4;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Iva";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 87F;
            numberCellType5.DecimalPlaces = 4;
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = -10000000D;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = numberCellType5;
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Precio Unitario";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 100F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.MaximumValue = 10000000D;
            numberCellType6.MinimumValue = -10000000D;
            this.grdProductos_Sheet1.Columns.Get(8).CellType = numberCellType6;
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 100F;
            numberCellType7.DecimalPlaces = 4;
            this.grdProductos_Sheet1.Columns.Get(9).CellType = numberCellType7;
            this.grdProductos_Sheet1.Columns.Get(9).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 100F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboEmpresas);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.txtProveedor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblDomicilio);
            this.groupBox1.Controls.Add(this.lblNomProv);
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
            this.groupBox1.Location = new System.Drawing.Point(10, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales del Pedido";
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(96, 46);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(360, 21);
            this.cboEmpresas.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(17, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Facturar A :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(197, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(185, 20);
            this.lblStatus.TabIndex = 26;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProveedor
            // 
            this.txtProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProveedor.Decimales = 2;
            this.txtProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtProveedor.Location = new System.Drawing.Point(96, 73);
            this.txtProveedor.MaxLength = 8;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PermitirApostrofo = false;
            this.txtProveedor.PermitirNegativos = false;
            this.txtProveedor.Size = new System.Drawing.Size(95, 20);
            this.txtProveedor.TabIndex = 4;
            this.txtProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyDown);
            this.txtProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtProveedor_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(14, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Proveedor :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 128);
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
            this.cboEstados.Location = new System.Drawing.Point(96, 100);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(181, 21);
            this.cboEstados.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(33, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDomicilio.Location = new System.Drawing.Point(96, 126);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(847, 21);
            this.lblDomicilio.TabIndex = 20;
            // 
            // lblNomProv
            // 
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(197, 73);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(746, 20);
            this.lblNomProv.TabIndex = 19;
            this.lblNomProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(361, 100);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(95, 20);
            this.txtIdFarmacia.TabIndex = 6;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFarmacia_Validating);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(96, 150);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(847, 42);
            this.txtObservaciones.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Observaciones :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(667, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Requerida de Entrega :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRequeridaEntrega
            // 
            this.dtpFechaRequeridaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRequeridaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRequeridaEntrega.Location = new System.Drawing.Point(850, 47);
            this.dtpFechaRequeridaEntrega.Name = "dtpFechaRequeridaEntrega";
            this.dtpFechaRequeridaEntrega.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRequeridaEntrega.TabIndex = 3;
            // 
            // lblEntregarEn
            // 
            this.lblEntregarEn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntregarEn.Location = new System.Drawing.Point(462, 100);
            this.lblEntregarEn.Name = "lblEntregarEn";
            this.lblEntregarEn.Size = new System.Drawing.Size(481, 21);
            this.lblEntregarEn.TabIndex = 5;
            this.lblEntregarEn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(283, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Entregar en :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(760, 22);
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
            this.dtpFechaRegistro.Location = new System.Drawing.Point(850, 20);
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
            this.txtPedido.Location = new System.Drawing.Point(96, 20);
            this.txtPedido.MaxLength = 8;
            this.txtPedido.Name = "txtPedido";
            this.txtPedido.PermitirApostrofo = false;
            this.txtPedido.PermitirNegativos = false;
            this.txtPedido.Size = new System.Drawing.Size(95, 20);
            this.txtPedido.TabIndex = 0;
            this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPedido.Validating += new System.ComponentModel.CancelEventHandler(this.txtPedido_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Orden Compra :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmOrdenCompraManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmOrdenCompraManual";
            this.Text = "Registro de Orden de Compra Manual";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPedidosCEDIS_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.Label lblNomProv;
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRequeridaEntrega;
        private System.Windows.Forms.Label lblEntregarEn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private SC_ControlsCS.scTextBoxExt txtPedido;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnCerrarOrden;
        private System.Windows.Forms.ToolStripButton btnExportarPDF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblImpteTotal;
        private System.Windows.Forms.Label label11;
    }
}