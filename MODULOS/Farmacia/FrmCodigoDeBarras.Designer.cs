namespace Farmacia
{
    partial class FrmCodigoDeBarras
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
            FarPoint.Win.Spread.CellType.BarCodeCellType barCodeCellType1 = new FarPoint.Win.Spread.CellType.BarCodeCellType();
            FarPoint.Win.Spread.CellType.BarCodeCellType barCodeCellType2 = new FarPoint.Win.Spread.CellType.BarCodeCellType();
            this.grdBarras = new FarPoint.Win.Spread.FpSpread();
            this.mnContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdBarras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.grdBarras)).BeginInit();
            this.mnContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBarras_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdBarras
            // 
            this.grdBarras.AccessibleDescription = "grdBarras, Sheet1, Row 0, Column 0, ";
            this.grdBarras.BackColor = System.Drawing.SystemColors.Control;
            this.grdBarras.ContextMenuStrip = this.mnContext;
            this.grdBarras.Location = new System.Drawing.Point(18, 17);
            this.grdBarras.Name = "grdBarras";
            this.grdBarras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdBarras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdBarras_Sheet1});
            this.grdBarras.Size = new System.Drawing.Size(737, 353);
            this.grdBarras.TabIndex = 0;
            // 
            // mnContext
            // 
            this.mnContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirToolStripMenuItem});
            this.mnContext.Name = "mnContext";
            this.mnContext.Size = new System.Drawing.Size(121, 26);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // grdBarras_Sheet1
            // 
            this.grdBarras_Sheet1.Reset();
            this.grdBarras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdBarras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdBarras_Sheet1.ColumnCount = 7;
            this.grdBarras_Sheet1.RowCount = 4;
            this.grdBarras_Sheet1.Cells.Get(0, 2).Value = "*758104100422*";
            this.grdBarras_Sheet1.Cells.Get(0, 4).Value = "758104100422";
            this.grdBarras_Sheet1.Cells.Get(1, 2).Value = "*758104100422*";
            this.grdBarras_Sheet1.Cells.Get(1, 4).Value = "758104100422";
            this.grdBarras_Sheet1.Cells.Get(2, 2).Value = "*758104100422*";
            this.grdBarras_Sheet1.Cells.Get(2, 4).Value = "758104100422";
            this.grdBarras_Sheet1.Cells.Get(3, 2).Value = "*758104100422*";
            this.grdBarras_Sheet1.Cells.Get(3, 4).Value = "758104100422";
            barCodeCellType1.AdjustSize = false;
            barCodeCellType1.BarCodePadding = new FarPoint.Win.Spread.CellType.BarCode.Quiet("", "");
            barCodeCellType1.BarSize = new FarPoint.Win.Spread.CellType.BarCode.BarSize("50.006mm", "11.906mm");
            barCodeCellType1.Type = new FarPoint.Win.Spread.CellType.BarCode.Code39("");
            this.grdBarras_Sheet1.Columns.Get(2).CellType = barCodeCellType1;
            this.grdBarras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBarras_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBarras_Sheet1.Columns.Get(2).Width = 190F;
            barCodeCellType2.BarCodePadding = new FarPoint.Win.Spread.CellType.BarCode.Quiet("", "");
            barCodeCellType2.BarSize = new FarPoint.Win.Spread.CellType.BarCode.BarSize("50.006mm", "11.906mm");
            barCodeCellType2.Type = new FarPoint.Win.Spread.CellType.BarCode.Code39("");
            this.grdBarras_Sheet1.Columns.Get(4).CellType = barCodeCellType2;
            this.grdBarras_Sheet1.Columns.Get(4).Width = 190F;
            this.grdBarras_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdBarras_Sheet1.Rows.Get(0).Height = 46F;
            this.grdBarras_Sheet1.Rows.Get(1).Height = 46F;
            this.grdBarras_Sheet1.Rows.Get(2).Height = 46F;
            this.grdBarras_Sheet1.Rows.Get(3).Height = 46F;
            this.grdBarras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmCodigoDeBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 385);
            this.Controls.Add(this.grdBarras);
            this.Name = "FrmCodigoDeBarras";
            this.Text = "FrmCodigoDeBarras";
            this.TituloMensajeValidarControl = "SC_Solutions";
            ((System.ComponentModel.ISupportInitialize)(this.grdBarras)).EndInit();
            this.mnContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBarras_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread grdBarras;
        private FarPoint.Win.Spread.SheetView grdBarras_Sheet1;
        private System.Windows.Forms.ContextMenuStrip mnContext;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
    }
}