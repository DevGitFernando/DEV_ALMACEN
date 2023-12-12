namespace DllProveedores.ConfirmarPedidos
{
    partial class FrmListaPedidosConfirmar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPedidosConfirmar));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdPedidos = new FarPoint.Win.Spread.FpSpread();
            this.grdPedidos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(496, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            this.btnEjecutar.ToolTipText = "Ejecutar (CTRL +E)";
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
            this.groupBox1.Controls.Add(this.grdPedidos);
            this.groupBox1.Location = new System.Drawing.Point(11, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 307);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Pedidos";
            // 
            // grdPedidos
            // 
            this.grdPedidos.AccessibleDescription = "grdPedidos, Sheet1, Row 0, Column 0, ";
            this.grdPedidos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPedidos.Location = new System.Drawing.Point(10, 19);
            this.grdPedidos.Name = "grdPedidos";
            this.grdPedidos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPedidos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPedidos_Sheet1});
            this.grdPedidos.Size = new System.Drawing.Size(457, 281);
            this.grdPedidos.TabIndex = 0;
            this.grdPedidos.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdPedidos_CellDoubleClick);
            // 
            // grdPedidos_Sheet1
            // 
            this.grdPedidos_Sheet1.Reset();
            this.grdPedidos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPedidos_Sheet1.ColumnCount = 4;
            this.grdPedidos_Sheet1.RowCount = 12;
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Núm. Pedido ";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "IdTipoPedido";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Tipo de Pedido";
            this.grdPedidos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdPedidos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(0).Label = "Núm. Pedido ";
            this.grdPedidos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(0).Width = 90F;
            this.grdPedidos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdPedidos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(1).Label = "Fecha";
            this.grdPedidos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(1).Width = 90F;
            this.grdPedidos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdPedidos_Sheet1.Columns.Get(2).Label = "IdTipoPedido";
            this.grdPedidos_Sheet1.Columns.Get(2).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(2).Width = 82F;
            this.grdPedidos_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdPedidos_Sheet1.Columns.Get(3).Label = "Tipo de Pedido";
            this.grdPedidos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(3).Width = 220F;
            this.grdPedidos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmListaPedidosConfirmar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 344);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListaPedidosConfirmar";
            this.Text = "Listado de Pedidos por Confirmar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaPedidosConfirmar_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).EndInit();
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
        private FarPoint.Win.Spread.FpSpread grdPedidos;
        private FarPoint.Win.Spread.SheetView grdPedidos_Sheet1;
    }
}