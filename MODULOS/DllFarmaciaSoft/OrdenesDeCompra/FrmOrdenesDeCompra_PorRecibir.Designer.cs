namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmOrdenesDeCompra_PorRecibir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdenesDeCompra_PorRecibir));
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType17 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType18 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType19 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType20 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdOrdenes = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenes_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(934, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdOrdenes);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(916, 327);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Concentrado por Claves";
            // 
            // grdOrdenes
            // 
            this.grdOrdenes.AccessibleDescription = "grdOrdenes, Sheet1, Row 0, Column 0, ";
            this.grdOrdenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdOrdenes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenes.Location = new System.Drawing.Point(8, 19);
            this.grdOrdenes.Name = "grdOrdenes";
            this.grdOrdenes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenes_Sheet1});
            this.grdOrdenes.Size = new System.Drawing.Size(901, 302);
            this.grdOrdenes.TabIndex = 4;
            // 
            // grdOrdenes_Sheet1
            // 
            this.grdOrdenes_Sheet1.Reset();
            this.grdOrdenes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenes_Sheet1.ColumnCount = 8;
            this.grdOrdenes_Sheet1.RowCount = 10;
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio Orden";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Núm. Proveedor";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Proveedor";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha Entrega";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Claves";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Piezas";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cajas";
            this.grdOrdenes_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdOrdenes_Sheet1.ColumnHeader.Rows.Get(0).Height = 34F;
            this.grdOrdenes_Sheet1.Columns.Get(0).CellType = textCellType17;
            this.grdOrdenes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(0).Label = "Folio Orden";
            this.grdOrdenes_Sheet1.Columns.Get(0).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(0).Width = 85F;
            this.grdOrdenes_Sheet1.Columns.Get(1).CellType = textCellType18;
            this.grdOrdenes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(1).Label = "Núm. Proveedor";
            this.grdOrdenes_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(1).Width = 75F;
            this.grdOrdenes_Sheet1.Columns.Get(2).CellType = textCellType19;
            this.grdOrdenes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenes_Sheet1.Columns.Get(2).Label = "Proveedor";
            this.grdOrdenes_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(2).Width = 250F;
            this.grdOrdenes_Sheet1.Columns.Get(3).CellType = textCellType20;
            this.grdOrdenes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(3).Label = "Fecha Entrega";
            this.grdOrdenes_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(3).Width = 70F;
            numberCellType17.DecimalPlaces = 0;
            numberCellType17.DecimalSeparator = ".";
            numberCellType17.MaximumValue = 10000000D;
            numberCellType17.MinimumValue = -10000000D;
            numberCellType17.Separator = ",";
            numberCellType17.ShowSeparator = true;
            this.grdOrdenes_Sheet1.Columns.Get(4).CellType = numberCellType17;
            this.grdOrdenes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenes_Sheet1.Columns.Get(4).Label = "Claves";
            this.grdOrdenes_Sheet1.Columns.Get(4).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType18.DecimalPlaces = 0;
            numberCellType18.DecimalSeparator = ".";
            numberCellType18.MaximumValue = 10000000D;
            numberCellType18.MinimumValue = -10000000D;
            numberCellType18.Separator = ",";
            numberCellType18.ShowSeparator = true;
            this.grdOrdenes_Sheet1.Columns.Get(5).CellType = numberCellType18;
            this.grdOrdenes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenes_Sheet1.Columns.Get(5).Label = "Piezas";
            this.grdOrdenes_Sheet1.Columns.Get(5).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType19.DecimalPlaces = 2;
            numberCellType19.DecimalSeparator = ".";
            numberCellType19.Separator = ",";
            numberCellType19.ShowSeparator = true;
            this.grdOrdenes_Sheet1.Columns.Get(6).CellType = numberCellType19;
            this.grdOrdenes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenes_Sheet1.Columns.Get(6).Label = "Cajas";
            this.grdOrdenes_Sheet1.Columns.Get(6).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType20.DecimalPlaces = 2;
            numberCellType20.DecimalSeparator = ".";
            numberCellType20.MaximumValue = 10000000D;
            numberCellType20.MinimumValue = 0D;
            numberCellType20.Separator = ",";
            numberCellType20.ShowSeparator = true;
            this.grdOrdenes_Sheet1.Columns.Get(7).CellType = numberCellType20;
            this.grdOrdenes_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenes_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdOrdenes_Sheet1.Columns.Get(7).Locked = true;
            this.grdOrdenes_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenes_Sheet1.Columns.Get(7).Width = 87F;
            this.grdOrdenes_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdOrdenes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmOrdenesDeCompra_PorRecibir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 361);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmOrdenesDeCompra_PorRecibir";
            this.Text = "Listado de Órdenes de Compra por Recepcionar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmOrdenesDeCompra_PorRecibir_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenes_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdOrdenes;
        private FarPoint.Win.Spread.SheetView grdOrdenes_Sheet1;
    }
}