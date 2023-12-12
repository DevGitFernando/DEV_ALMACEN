namespace Planeacion.ObtenerInformacion
{
    partial class FrmRegistroDePlaneacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroDePlaneacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAutorizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCantidades = new System.Windows.Forms.CheckBox();
            this.grdConsumos = new FarPoint.Win.Spread.FpSpread();
            this.grdConsumos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.grVta = new System.Windows.Forms.GroupBox();
            this.lblDiasStockSeguridasd = new System.Windows.Forms.Label();
            this.lblMesesStockAlta = new System.Windows.Forms.Label();
            this.lblMesesCaducidad = new System.Windows.Forms.Label();
            this.lblMesesInformacion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMesesStockMedia = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMesesStockBaja = new System.Windows.Forms.Label();
            this.lblTipoDeGeneracion = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).BeginInit();
            this.grVta.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2,
            this.btnAutorizar,
            this.toolStripSeparator5,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1763, 25);
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
            // btnAutorizar
            // 
            this.btnAutorizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutorizar.Image = ((System.Drawing.Image)(resources.GetObject("btnAutorizar.Image")));
            this.btnAutorizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(23, 22);
            this.btnAutorizar.Text = "Autorizar";
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            this.btnExportarExcel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkCantidades);
            this.groupBox1.Controls.Add(this.grdConsumos);
            this.groupBox1.Location = new System.Drawing.Point(11, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1741, 512);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Claves";
            // 
            // chkCantidades
            // 
            this.chkCantidades.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCantidades.Location = new System.Drawing.Point(1020, 3);
            this.chkCantidades.Name = "chkCantidades";
            this.chkCantidades.Size = new System.Drawing.Size(237, 17);
            this.chkCantidades.TabIndex = 3;
            this.chkCantidades.Text = "Igualar / Poner en Ceros las Cantidades";
            this.chkCantidades.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCantidades.UseVisualStyleBackColor = true;
            this.chkCantidades.CheckedChanged += new System.EventHandler(this.chkCantidades_CheckedChanged);
            // 
            // grdConsumos
            // 
            this.grdConsumos.AccessibleDescription = "grdConsumos, Sheet1, Row 0, Column 0, ";
            this.grdConsumos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdConsumos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdConsumos.Location = new System.Drawing.Point(10, 20);
            this.grdConsumos.Margin = new System.Windows.Forms.Padding(2);
            this.grdConsumos.Name = "grdConsumos";
            this.grdConsumos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdConsumos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdConsumos_Sheet1});
            this.grdConsumos.Size = new System.Drawing.Size(1717, 486);
            this.grdConsumos.TabIndex = 0;
            this.grdConsumos.EditModeOff += new System.EventHandler(this.grdConsumos_EditModeOff);
            // 
            // grdConsumos_Sheet1
            // 
            this.grdConsumos_Sheet1.Reset();
            this.grdConsumos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdConsumos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdConsumos_Sheet1.ColumnCount = 20;
            this.grdConsumos_Sheet1.RowCount = 10;
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEstado";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdFarmacia";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Folio";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "IdClaveSSA";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Clave SSA";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Descripción";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Presentación";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Contenido";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Piezas";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Rotación";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "PCM";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Consumo ";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "Existencia";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "Existencia Almacen";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "Piezas sugeridas";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "Cajas sugeridas";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "Cantidad requerida";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "Cantidad autorizada compra";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "Mayor";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "Diferencia detectada";
            this.grdConsumos_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            this.grdConsumos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(0).Label = "IdEstado";
            this.grdConsumos_Sheet1.Columns.Get(0).Visible = false;
            this.grdConsumos_Sheet1.Columns.Get(1).Label = "IdFarmacia";
            this.grdConsumos_Sheet1.Columns.Get(1).Visible = false;
            this.grdConsumos_Sheet1.Columns.Get(1).Width = 75F;
            this.grdConsumos_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.grdConsumos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(2).Label = "Folio";
            this.grdConsumos_Sheet1.Columns.Get(2).Visible = false;
            this.grdConsumos_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.grdConsumos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Label = "IdClaveSSA";
            this.grdConsumos_Sheet1.Columns.Get(3).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Visible = false;
            this.grdConsumos_Sheet1.Columns.Get(3).Width = 73F;
            this.grdConsumos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(4).Label = "Clave SSA";
            this.grdConsumos_Sheet1.Columns.Get(4).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(4).Width = 118F;
            this.grdConsumos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdConsumos_Sheet1.Columns.Get(5).Label = "Descripción";
            this.grdConsumos_Sheet1.Columns.Get(5).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(5).Width = 386F;
            this.grdConsumos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdConsumos_Sheet1.Columns.Get(6).Label = "Presentación";
            this.grdConsumos_Sheet1.Columns.Get(6).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(6).Width = 137F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 100000D;
            numberCellType1.MinimumValue = 1D;
            this.grdConsumos_Sheet1.Columns.Get(7).CellType = numberCellType1;
            this.grdConsumos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(7).Label = "Contenido";
            this.grdConsumos_Sheet1.Columns.Get(7).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(7).Width = 81F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 100000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(8).CellType = numberCellType2;
            this.grdConsumos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(8).Label = "Piezas";
            this.grdConsumos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(8).Width = 81F;
            this.grdConsumos_Sheet1.Columns.Get(9).CellType = textCellType3;
            this.grdConsumos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(9).Label = "Rotación";
            this.grdConsumos_Sheet1.Columns.Get(9).Width = 98F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 100000000D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(10).CellType = numberCellType3;
            this.grdConsumos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(10).Label = "PCM";
            this.grdConsumos_Sheet1.Columns.Get(10).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(10).Width = 70F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.DecimalSeparator = ".";
            numberCellType4.MaximumValue = 100000000D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(11).CellType = numberCellType4;
            this.grdConsumos_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(11).Label = "Consumo ";
            this.grdConsumos_Sheet1.Columns.Get(11).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(11).Width = 70F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.MaximumValue = 100000000D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(12).CellType = numberCellType5;
            this.grdConsumos_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(12).Label = "Existencia";
            this.grdConsumos_Sheet1.Columns.Get(12).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(12).Width = 70F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.DecimalSeparator = ".";
            numberCellType6.MaximumValue = 100000000D;
            numberCellType6.MinimumValue = 0D;
            numberCellType6.Separator = ",";
            numberCellType6.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(13).CellType = numberCellType6;
            this.grdConsumos_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(13).Label = "Existencia Almacen";
            this.grdConsumos_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(13).Width = 70F;
            numberCellType7.DecimalPlaces = 0;
            numberCellType7.DecimalSeparator = ".";
            numberCellType7.MaximumValue = 100000000D;
            numberCellType7.MinimumValue = 0D;
            numberCellType7.Separator = ",";
            numberCellType7.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(14).CellType = numberCellType7;
            this.grdConsumos_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(14).Label = "Piezas sugeridas";
            this.grdConsumos_Sheet1.Columns.Get(14).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(14).Width = 70F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.DecimalSeparator = ".";
            numberCellType8.MaximumValue = 100000000D;
            numberCellType8.MinimumValue = 0D;
            numberCellType8.Separator = ",";
            numberCellType8.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(15).CellType = numberCellType8;
            this.grdConsumos_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(15).Label = "Cajas sugeridas";
            this.grdConsumos_Sheet1.Columns.Get(15).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(15).Width = 70F;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.DecimalSeparator = ".";
            numberCellType9.MaximumValue = 100000000D;
            numberCellType9.MinimumValue = 0D;
            numberCellType9.Separator = ",";
            numberCellType9.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(16).CellType = numberCellType9;
            this.grdConsumos_Sheet1.Columns.Get(16).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdConsumos_Sheet1.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(16).Label = "Cantidad requerida";
            this.grdConsumos_Sheet1.Columns.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(16).Width = 70F;
            numberCellType10.DecimalPlaces = 0;
            numberCellType10.DecimalSeparator = ".";
            numberCellType10.MaximumValue = 100000000D;
            numberCellType10.MinimumValue = 0D;
            numberCellType10.Separator = ",";
            numberCellType10.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(17).CellType = numberCellType10;
            this.grdConsumos_Sheet1.Columns.Get(17).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdConsumos_Sheet1.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(17).Label = "Cantidad autorizada compra";
            this.grdConsumos_Sheet1.Columns.Get(17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(17).Width = 70F;
            this.grdConsumos_Sheet1.Columns.Get(18).CellType = checkBoxCellType1;
            this.grdConsumos_Sheet1.Columns.Get(18).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(18).Label = "Mayor";
            this.grdConsumos_Sheet1.Columns.Get(18).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(18).Visible = false;
            numberCellType11.DecimalPlaces = 0;
            numberCellType11.DecimalSeparator = ".";
            numberCellType11.MaximumValue = 100000000D;
            numberCellType11.MinimumValue = -100000000D;
            numberCellType11.Separator = ",";
            numberCellType11.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(19).CellType = numberCellType11;
            this.grdConsumos_Sheet1.Columns.Get(19).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(19).Label = "Diferencia detectada";
            this.grdConsumos_Sheet1.Columns.Get(19).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(19).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(19).Width = 70F;
            this.grdConsumos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdConsumos_Sheet1.Rows.Default.Height = 25F;
            this.grdConsumos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // grVta
            // 
            this.grVta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grVta.Controls.Add(this.lblTipoDeGeneracion);
            this.grVta.Controls.Add(this.lblMesesStockBaja);
            this.grVta.Controls.Add(this.label8);
            this.grVta.Controls.Add(this.lblMesesStockMedia);
            this.grVta.Controls.Add(this.label7);
            this.grVta.Controls.Add(this.lblDiasStockSeguridasd);
            this.grVta.Controls.Add(this.lblMesesStockAlta);
            this.grVta.Controls.Add(this.lblMesesCaducidad);
            this.grVta.Controls.Add(this.lblMesesInformacion);
            this.grVta.Controls.Add(this.label5);
            this.grVta.Controls.Add(this.label4);
            this.grVta.Controls.Add(this.label2);
            this.grVta.Controls.Add(this.label6);
            this.grVta.Controls.Add(this.dtpFechaRegistro);
            this.grVta.Controls.Add(this.label3);
            this.grVta.Controls.Add(this.lblStatus);
            this.grVta.Controls.Add(this.txtFolio);
            this.grVta.Controls.Add(this.label1);
            this.grVta.Location = new System.Drawing.Point(11, 28);
            this.grVta.Name = "grVta";
            this.grVta.Size = new System.Drawing.Size(1741, 80);
            this.grVta.TabIndex = 1;
            this.grVta.TabStop = false;
            this.grVta.Text = "Datos generales de la Planeación";
            // 
            // lblDiasStockSeguridasd
            // 
            this.lblDiasStockSeguridasd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiasStockSeguridasd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiasStockSeguridasd.Location = new System.Drawing.Point(859, 48);
            this.lblDiasStockSeguridasd.Name = "lblDiasStockSeguridasd";
            this.lblDiasStockSeguridasd.Size = new System.Drawing.Size(72, 20);
            this.lblDiasStockSeguridasd.TabIndex = 66;
            this.lblDiasStockSeguridasd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMesesStockAlta
            // 
            this.lblMesesStockAlta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesStockAlta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesStockAlta.Location = new System.Drawing.Point(573, 23);
            this.lblMesesStockAlta.Name = "lblMesesStockAlta";
            this.lblMesesStockAlta.Size = new System.Drawing.Size(72, 20);
            this.lblMesesStockAlta.TabIndex = 65;
            this.lblMesesStockAlta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMesesCaducidad
            // 
            this.lblMesesCaducidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesCaducidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesCaducidad.Location = new System.Drawing.Point(1125, 23);
            this.lblMesesCaducidad.Name = "lblMesesCaducidad";
            this.lblMesesCaducidad.Size = new System.Drawing.Size(72, 20);
            this.lblMesesCaducidad.TabIndex = 64;
            this.lblMesesCaducidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMesesInformacion
            // 
            this.lblMesesInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesInformacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesInformacion.Location = new System.Drawing.Point(1125, 48);
            this.lblMesesInformacion.Name = "lblMesesInformacion";
            this.lblMesesInformacion.Size = new System.Drawing.Size(72, 20);
            this.lblMesesInformacion.TabIndex = 63;
            this.lblMesesInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(969, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 14);
            this.label5.TabIndex = 59;
            this.label5.Text = "meses de información historica";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(681, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 14);
            this.label4.TabIndex = 58;
            this.label4.Text = "Dias de stock de seguridad:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(947, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 14);
            this.label2.TabIndex = 54;
            this.label2.Text = "Meses de caducidad a validar :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(395, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 14);
            this.label6.TabIndex = 52;
            this.label6.Text = "Meses de stock de Alta rotación :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1636, 23);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(1536, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(87, 47);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(271, 20);
            this.lblStatus.TabIndex = 32;
            this.lblStatus.Text = "CANCELADA";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(87, 23);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 1;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio activo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(392, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 14);
            this.label7.TabIndex = 67;
            this.label7.Text = "Meses de stock de Media rotación :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMesesStockMedia
            // 
            this.lblMesesStockMedia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesStockMedia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesStockMedia.Location = new System.Drawing.Point(573, 48);
            this.lblMesesStockMedia.Name = "lblMesesStockMedia";
            this.lblMesesStockMedia.Size = new System.Drawing.Size(72, 20);
            this.lblMesesStockMedia.TabIndex = 68;
            this.lblMesesStockMedia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(678, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 14);
            this.label8.TabIndex = 69;
            this.label8.Text = "Meses de stock de Baja rotación :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMesesStockBaja
            // 
            this.lblMesesStockBaja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesStockBaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesStockBaja.Location = new System.Drawing.Point(859, 23);
            this.lblMesesStockBaja.Name = "lblMesesStockBaja";
            this.lblMesesStockBaja.Size = new System.Drawing.Size(72, 20);
            this.lblMesesStockBaja.TabIndex = 70;
            this.lblMesesStockBaja.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTipoDeGeneracion
            // 
            this.lblTipoDeGeneracion.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoDeGeneracion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoDeGeneracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDeGeneracion.Location = new System.Drawing.Point(193, 23);
            this.lblTipoDeGeneracion.Name = "lblTipoDeGeneracion";
            this.lblTipoDeGeneracion.Size = new System.Drawing.Size(165, 20);
            this.lblTipoDeGeneracion.TabIndex = 71;
            this.lblTipoDeGeneracion.Text = "CANCELADA";
            this.lblTipoDeGeneracion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRegistroDePlaneacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1763, 629);
            this.Controls.Add(this.grVta);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRegistroDePlaneacion";
            this.Text = "Registro de planeación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistroDePlaneacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).EndInit();
            this.grVta.ResumeLayout(false);
            this.grVta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnAutorizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdConsumos;
        private FarPoint.Win.Spread.SheetView grdConsumos_Sheet1;
        private System.Windows.Forms.GroupBox grVta;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox chkCantidades;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMesesCaducidad;
        private System.Windows.Forms.Label lblMesesInformacion;
        private System.Windows.Forms.Label lblDiasStockSeguridasd;
        private System.Windows.Forms.Label lblMesesStockAlta;
        private System.Windows.Forms.Label lblMesesStockMedia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMesesStockBaja;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTipoDeGeneracion;
    }
}