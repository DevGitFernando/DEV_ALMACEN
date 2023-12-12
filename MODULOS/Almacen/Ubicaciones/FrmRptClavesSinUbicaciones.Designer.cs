namespace Almacen.Ubicaciones
{
    partial class FrmRptClavesSinUbicaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptClavesSinUbicaciones));
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.grdClavesSinUbicaciones = new FarPoint.Win.Spread.FpSpread();
            this.grdClavesSinUbicaciones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesSinUbicaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesSinUbicaciones_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            this.toolStripSeparator2.Visible = false;
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
            // grdClavesSinUbicaciones
            // 
            this.grdClavesSinUbicaciones.AccessibleDescription = "grdClavesSinUbicaciones, Sheet1, Row 0, Column 0, ";
            this.grdClavesSinUbicaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClavesSinUbicaciones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClavesSinUbicaciones.Location = new System.Drawing.Point(9, 19);
            this.grdClavesSinUbicaciones.Name = "grdClavesSinUbicaciones";
            this.grdClavesSinUbicaciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClavesSinUbicaciones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClavesSinUbicaciones_Sheet1});
            this.grdClavesSinUbicaciones.Size = new System.Drawing.Size(1148, 503);
            this.grdClavesSinUbicaciones.TabIndex = 1;
            // 
            // grdClavesSinUbicaciones_Sheet1
            // 
            this.grdClavesSinUbicaciones_Sheet1.Reset();
            this.grdClavesSinUbicaciones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClavesSinUbicaciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClavesSinUbicaciones_Sheet1.ColumnCount = 8;
            this.grdClavesSinUbicaciones_Sheet1.RowCount = 20;
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClave SSA";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción Clave";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Producto";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Nombre Comercial";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Codigo EAN";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Clave Lote";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha caducidad";
            this.grdClavesSinUbicaciones_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(0).Label = "IdClave SSA";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(0).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(1).Width = 100F;
            textCellType11.MaxLength = 4000;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(2).CellType = textCellType11;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(2).Label = "Descripción Clave";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(2).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(2).Width = 220F;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).CellType = textCellType12;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).Label = "Producto";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(3).Width = 70F;
            textCellType13.MaxLength = 500;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(4).CellType = textCellType13;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(4).Label = "Nombre Comercial";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(4).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(4).Width = 220F;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).CellType = textCellType14;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).Label = "Codigo EAN";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(5).Width = 100F;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).CellType = textCellType15;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).Label = "Clave Lote";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(6).Width = 100F;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).CellType = textCellType16;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).Label = "Fecha caducidad";
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).Locked = true;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesSinUbicaciones_Sheet1.Columns.Get(7).Width = 70F;
            this.grdClavesSinUbicaciones_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdClavesSinUbicaciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdClavesSinUbicaciones);
            this.groupBox1.Location = new System.Drawing.Point(10, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1166, 529);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Claves - Lotes";
            // 
            // FrmRptClavesSinUbicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRptClavesSinUbicaciones";
            this.Text = "Reporte de Claves-Lotes sin ubicaciones asignadas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptClavesSinUbicaciones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesSinUbicaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesSinUbicaciones_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
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
        private FarPoint.Win.Spread.FpSpread grdClavesSinUbicaciones;
        private FarPoint.Win.Spread.SheetView grdClavesSinUbicaciones_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}