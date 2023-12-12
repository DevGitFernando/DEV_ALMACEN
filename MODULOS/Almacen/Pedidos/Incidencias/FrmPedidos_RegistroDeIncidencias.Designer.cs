namespace Almacen.Pedidos
{
    partial class FrmPedidos_RegistroDeIncidencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedidos_RegistroDeIncidencias));
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.GrupoProductos = new System.Windows.Forms.GroupBox();
            this.grdDetalles = new FarPoint.Win.Spread.FpSpread();
            this.grdDetalles_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.GrupoProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnSalir,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 39);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(36, 36);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(36, 36);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(77, 36);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // GrupoProductos
            // 
            this.GrupoProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrupoProductos.Controls.Add(this.grdDetalles);
            this.GrupoProductos.Location = new System.Drawing.Point(9, 40);
            this.GrupoProductos.Name = "GrupoProductos";
            this.GrupoProductos.Size = new System.Drawing.Size(963, 441);
            this.GrupoProductos.TabIndex = 3;
            this.GrupoProductos.TabStop = false;
            this.GrupoProductos.Text = "Detalle";
            // 
            // grdDetalles
            // 
            this.grdDetalles.AccessibleDescription = "grdDetalles, Sheet1, Row 0, Column 0, ";
            this.grdDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDetalles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdDetalles.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdDetalles.Location = new System.Drawing.Point(9, 19);
            this.grdDetalles.Name = "grdDetalles";
            this.grdDetalles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDetalles.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDetalles_Sheet1});
            this.grdDetalles.Size = new System.Drawing.Size(946, 411);
            this.grdDetalles.TabIndex = 0;
            this.grdDetalles.TabStripRatio = 0.477568740955137D;
            // 
            // grdDetalles_Sheet1
            // 
            this.grdDetalles_Sheet1.Reset();
            this.grdDetalles_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDetalles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDetalles_Sheet1.ColumnCount = 7;
            this.grdDetalles_Sheet1.RowCount = 20;
            this.grdDetalles_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Cells.Get(0, 2).Locked = true;
            this.grdDetalles_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Cells.Get(1, 3).Locked = false;
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Secuencial";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Id";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Incidencia";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Registrar";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Es informativo";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Observaciones adicionales";
            this.grdDetalles_Sheet1.ColumnHeader.Rows.Get(0).Height = 55F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            this.grdDetalles_Sheet1.Columns.Get(0).CellType = numberCellType1;
            this.grdDetalles_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(0).Label = "Secuencial";
            this.grdDetalles_Sheet1.Columns.Get(0).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(0).Width = 100F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            this.grdDetalles_Sheet1.Columns.Get(1).CellType = numberCellType2;
            this.grdDetalles_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(1).Label = "Id";
            this.grdDetalles_Sheet1.Columns.Get(1).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdDetalles_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.grdDetalles_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(2).Label = "Incidencia";
            this.grdDetalles_Sheet1.Columns.Get(2).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(2).Width = 80F;
            this.grdDetalles_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.grdDetalles_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalles_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdDetalles_Sheet1.Columns.Get(3).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(3).Width = 240F;
            this.grdDetalles_Sheet1.Columns.Get(4).CellType = checkBoxCellType1;
            this.grdDetalles_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(4).Label = "Registrar";
            this.grdDetalles_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(5).Label = "Es informativo";
            this.grdDetalles_Sheet1.Columns.Get(5).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(5).Width = 90F;
            textCellType3.MaxLength = 200;
            this.grdDetalles_Sheet1.Columns.Get(6).CellType = textCellType3;
            this.grdDetalles_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalles_Sheet1.Columns.Get(6).Label = "Observaciones adicionales";
            this.grdDetalles_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(6).Width = 400F;
            this.grdDetalles_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdDetalles_Sheet1.Rows.Default.Height = 35F;
            this.grdDetalles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmPedidos_RegistroDeIncidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 486);
            this.Controls.Add(this.GrupoProductos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmPedidos_RegistroDeIncidencias";
            this.Text = "Pedidos Incidencias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPedidos_RegistroDeIncidencias_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.GrupoProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox GrupoProductos;
        private FarPoint.Win.Spread.FpSpread grdDetalles;
        private FarPoint.Win.Spread.SheetView grdDetalles_Sheet1;
    }
}