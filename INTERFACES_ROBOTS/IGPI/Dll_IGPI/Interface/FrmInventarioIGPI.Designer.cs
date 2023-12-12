namespace Dll_IGPI.Interface
{
    partial class FrmInventarioIGPI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventarioIGPI));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.tabInventario = new System.Windows.Forms.TabControl();
            this.tabpClaves = new System.Windows.Forms.TabPage();
            this.lblClavesDistintas = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUnidadesClaves = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabpProductos = new System.Windows.Forms.TabPage();
            this.lblProductos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUnidadesProductos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmK = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoDetalladoProductos = new System.Windows.Forms.RadioButton();
            this.rdoDetalladoClaves = new System.Windows.Forms.RadioButton();
            this.rdoConcentradoClaves = new System.Windows.Forms.RadioButton();
            this.tmK_Expire = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.tabInventario.SuspendLayout();
            this.tabpClaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.tabpProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(802, 25);
            this.toolStripBarraMenu.TabIndex = 15;
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
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.tabpClaves);
            this.tabInventario.Controls.Add(this.tabpProductos);
            this.tabInventario.Location = new System.Drawing.Point(11, 76);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.SelectedIndex = 0;
            this.tabInventario.Size = new System.Drawing.Size(784, 380);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.SelectedIndexChanged += new System.EventHandler(this.tabInventario_SelectedIndexChanged);
            // 
            // tabpClaves
            // 
            this.tabpClaves.Controls.Add(this.lblClavesDistintas);
            this.tabpClaves.Controls.Add(this.label4);
            this.tabpClaves.Controls.Add(this.lblUnidadesClaves);
            this.tabpClaves.Controls.Add(this.label1);
            this.tabpClaves.Controls.Add(this.grdClaves);
            this.tabpClaves.Location = new System.Drawing.Point(4, 22);
            this.tabpClaves.Name = "tabpClaves";
            this.tabpClaves.Padding = new System.Windows.Forms.Padding(3);
            this.tabpClaves.Size = new System.Drawing.Size(776, 354);
            this.tabpClaves.TabIndex = 0;
            this.tabpClaves.Text = "Claves";
            this.tabpClaves.UseVisualStyleBackColor = true;
            // 
            // lblClavesDistintas
            // 
            this.lblClavesDistintas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClavesDistintas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClavesDistintas.Location = new System.Drawing.Point(465, 323);
            this.lblClavesDistintas.Name = "lblClavesDistintas";
            this.lblClavesDistintas.Size = new System.Drawing.Size(81, 20);
            this.lblClavesDistintas.TabIndex = 4;
            this.lblClavesDistintas.Text = "label3";
            this.lblClavesDistintas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(399, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Claves :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUnidadesClaves
            // 
            this.lblUnidadesClaves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUnidadesClaves.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadesClaves.Location = new System.Drawing.Point(673, 323);
            this.lblUnidadesClaves.Name = "lblUnidadesClaves";
            this.lblUnidadesClaves.Size = new System.Drawing.Size(81, 20);
            this.lblUnidadesClaves.TabIndex = 2;
            this.lblUnidadesClaves.Text = "label2";
            this.lblUnidadesClaves.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(599, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unidades :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.Transparent;
            this.grdClaves.Location = new System.Drawing.Point(6, 6);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(766, 310);
            this.grdClaves.TabIndex = 0;
            this.grdClaves.Leave += new System.EventHandler(this.grdClaves_Leave);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 3;
            this.grdClaves_Sheet1.RowCount = 10;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Clave";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 120F;
            textCellType2.MaxLength = 1000;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 490F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Cantidad";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 100F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabpProductos
            // 
            this.tabpProductos.Controls.Add(this.lblProductos);
            this.tabpProductos.Controls.Add(this.label3);
            this.tabpProductos.Controls.Add(this.lblUnidadesProductos);
            this.tabpProductos.Controls.Add(this.label6);
            this.tabpProductos.Controls.Add(this.grdProductos);
            this.tabpProductos.Location = new System.Drawing.Point(4, 22);
            this.tabpProductos.Name = "tabpProductos";
            this.tabpProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabpProductos.Size = new System.Drawing.Size(776, 354);
            this.tabpProductos.TabIndex = 1;
            this.tabpProductos.Text = "Productos";
            this.tabpProductos.UseVisualStyleBackColor = true;
            // 
            // lblProductos
            // 
            this.lblProductos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.Location = new System.Drawing.Point(465, 323);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(81, 20);
            this.lblProductos.TabIndex = 8;
            this.lblProductos.Text = "label3";
            this.lblProductos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(375, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Productos :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUnidadesProductos
            // 
            this.lblUnidadesProductos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUnidadesProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadesProductos.Location = new System.Drawing.Point(673, 323);
            this.lblUnidadesProductos.Name = "lblUnidadesProductos";
            this.lblUnidadesProductos.Size = new System.Drawing.Size(81, 20);
            this.lblUnidadesProductos.TabIndex = 6;
            this.lblUnidadesProductos.Text = "label2";
            this.lblUnidadesProductos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(599, 323);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Unidades :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.Transparent;
            this.grdProductos.Location = new System.Drawing.Point(6, 6);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(766, 310);
            this.grdProductos.TabIndex = 1;
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 3;
            this.grdProductos_Sheet1.RowCount = 10;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 490F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 100F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmK
            // 
            this.tmK.Tick += new System.EventHandler(this.tmK_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoDetalladoProductos);
            this.groupBox2.Controls.Add(this.rdoDetalladoClaves);
            this.groupBox2.Controls.Add(this.rdoConcentradoClaves);
            this.groupBox2.Location = new System.Drawing.Point(11, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 42);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Reporte Impreso";
            // 
            // rdoDetalladoProductos
            // 
            this.rdoDetalladoProductos.Location = new System.Drawing.Point(509, 19);
            this.rdoDetalladoProductos.Name = "rdoDetalladoProductos";
            this.rdoDetalladoProductos.Size = new System.Drawing.Size(156, 18);
            this.rdoDetalladoProductos.TabIndex = 2;
            this.rdoDetalladoProductos.TabStop = true;
            this.rdoDetalladoProductos.Text = "Detallado por Producto";
            this.rdoDetalladoProductos.UseVisualStyleBackColor = true;
            // 
            // rdoDetalladoClaves
            // 
            this.rdoDetalladoClaves.Location = new System.Drawing.Point(312, 19);
            this.rdoDetalladoClaves.Name = "rdoDetalladoClaves";
            this.rdoDetalladoClaves.Size = new System.Drawing.Size(156, 18);
            this.rdoDetalladoClaves.TabIndex = 1;
            this.rdoDetalladoClaves.TabStop = true;
            this.rdoDetalladoClaves.Text = "Detallado por Clave";
            this.rdoDetalladoClaves.UseVisualStyleBackColor = true;
            // 
            // rdoConcentradoClaves
            // 
            this.rdoConcentradoClaves.Location = new System.Drawing.Point(115, 19);
            this.rdoConcentradoClaves.Name = "rdoConcentradoClaves";
            this.rdoConcentradoClaves.Size = new System.Drawing.Size(156, 18);
            this.rdoConcentradoClaves.TabIndex = 0;
            this.rdoConcentradoClaves.TabStop = true;
            this.rdoConcentradoClaves.Text = "Concentrado por Claves";
            this.rdoConcentradoClaves.UseVisualStyleBackColor = true;
            // 
            // tmK_Expire
            // 
            this.tmK_Expire.Tick += new System.EventHandler(this.tmK_Expire_Tick);
            // 
            // FrmInventarioIGPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 466);
            this.Controls.Add(this.tabInventario);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInventarioIGPI";
            this.Text = "Inventario en GPI";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInventarioIGPI_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.tabInventario.ResumeLayout(false);
            this.tabpClaves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.tabpProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.TabControl tabInventario;
        private System.Windows.Forms.TabPage tabpClaves;
        private System.Windows.Forms.TabPage tabpProductos;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.Timer tmK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoDetalladoProductos;
        private System.Windows.Forms.RadioButton rdoDetalladoClaves;
        private System.Windows.Forms.RadioButton rdoConcentradoClaves;
        private System.Windows.Forms.Label lblUnidadesClaves;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClavesDistintas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUnidadesProductos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer tmK_Expire;
    }
}