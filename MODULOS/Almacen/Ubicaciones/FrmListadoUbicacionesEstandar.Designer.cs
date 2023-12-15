namespace Almacen.Ubicaciones
{
    partial class FrmListadoUbicacionesEstandar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoUbicacionesEstandar));
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdPosiciones = new FarPoint.Win.Spread.FpSpread();
            this.grdPosiciones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPosiciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPosiciones_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1179, 58);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 4);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 4);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(54, 55);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Visible = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdPosiciones);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1155, 427);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Posiciones";
            // 
            // grdPosiciones
            // 
            this.grdPosiciones.AccessibleDescription = "grdPosiciones, Sheet1, Row 0, Column 0, ";
            this.grdPosiciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPosiciones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPosiciones.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdPosiciones.HorizontalScrollBar.Name = "";
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
            this.grdPosiciones.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdPosiciones.HorizontalScrollBar.TabIndex = 2;
            this.grdPosiciones.Location = new System.Drawing.Point(13, 23);
            this.grdPosiciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdPosiciones.Name = "grdPosiciones";
            this.grdPosiciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPosiciones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPosiciones_Sheet1});
            this.grdPosiciones.Size = new System.Drawing.Size(1128, 396);
            this.grdPosiciones.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdPosiciones.TabIndex = 0;
            this.grdPosiciones.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdPosiciones.VerticalScrollBar.Name = "";
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
            this.grdPosiciones.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdPosiciones.VerticalScrollBar.TabIndex = 3;
            // 
            // grdPosiciones_Sheet1
            // 
            this.grdPosiciones_Sheet1.Reset();
            this.grdPosiciones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPosiciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPosiciones_Sheet1.ColumnCount = 5;
            this.grdPosiciones_Sheet1.RowCount = 8;
            this.grdPosiciones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Ubicación";
            this.grdPosiciones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdPosiciones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Rack";
            this.grdPosiciones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Nivel";
            this.grdPosiciones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Posición";
            this.grdPosiciones_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdPosiciones_Sheet1.ColumnHeader.Rows.Get(0).Height = 24F;
            this.grdPosiciones_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdPosiciones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPosiciones_Sheet1.Columns.Get(0).Label = "Ubicación";
            this.grdPosiciones_Sheet1.Columns.Get(0).Locked = true;
            this.grdPosiciones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(0).Width = 121F;
            this.grdPosiciones_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdPosiciones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPosiciones_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdPosiciones_Sheet1.Columns.Get(1).Locked = true;
            this.grdPosiciones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(1).Width = 380F;
            this.grdPosiciones_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdPosiciones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(2).Label = "Rack";
            this.grdPosiciones_Sheet1.Columns.Get(2).Locked = true;
            this.grdPosiciones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(2).Width = 70F;
            this.grdPosiciones_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdPosiciones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(3).Label = "Nivel";
            this.grdPosiciones_Sheet1.Columns.Get(3).Locked = true;
            this.grdPosiciones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(3).Width = 70F;
            textCellType5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdPosiciones_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdPosiciones_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(4).Label = "Posición";
            this.grdPosiciones_Sheet1.Columns.Get(4).Locked = true;
            this.grdPosiciones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPosiciones_Sheet1.Columns.Get(4).Width = 70F;
            this.grdPosiciones_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdPosiciones_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdPosiciones_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdPosiciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmListadoUbicacionesEstandar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 506);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmListadoUbicacionesEstandar";
            this.ShowIcon = false;
            this.Text = "Listado de Ubicaciones Estandar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoUbicacionesEstandar_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPosiciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPosiciones_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdPosiciones;
        private FarPoint.Win.Spread.SheetView grdPosiciones_Sheet1;
    }
}