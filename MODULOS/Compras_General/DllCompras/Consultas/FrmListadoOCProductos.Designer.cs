namespace DllCompras.Consultas
{
    partial class FrmListadoOCProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoOCProductos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblImpteTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grdOrdenCompras = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenCompras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaPedido = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPedidoUnidad = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPedido = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(874, 25);
            this.toolStripBarraMenu.TabIndex = 16;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Enabled = false;
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
            this.btnEjecutar.Enabled = false;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblImpteTotal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.grdOrdenCompras);
            this.groupBox2.Location = new System.Drawing.Point(10, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(856, 340);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Órdenes de Compra";
            // 
            // lblImpteTotal
            // 
            this.lblImpteTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblImpteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImpteTotal.Location = new System.Drawing.Point(695, 303);
            this.lblImpteTotal.Name = "lblImpteTotal";
            this.lblImpteTotal.Size = new System.Drawing.Size(135, 20);
            this.lblImpteTotal.TabIndex = 32;
            this.lblImpteTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(583, 304);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 16);
            this.label11.TabIndex = 31;
            this.label11.Text = "Importe Total :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdOrdenCompras
            // 
            this.grdOrdenCompras.AccessibleDescription = "grdOrdenCompras, Sheet1, Row 0, Column 0, ";
            this.grdOrdenCompras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenCompras.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdOrdenCompras.Location = new System.Drawing.Point(10, 16);
            this.grdOrdenCompras.Name = "grdOrdenCompras";
            this.grdOrdenCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenCompras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenCompras_Sheet1});
            this.grdOrdenCompras.Size = new System.Drawing.Size(836, 284);
            this.grdOrdenCompras.TabIndex = 0;
            this.grdOrdenCompras.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdOrdenCompras_CellDoubleClick);
            // 
            // grdOrdenCompras_Sheet1
            // 
            this.grdOrdenCompras_Sheet1.Reset();
            this.grdOrdenCompras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenCompras_Sheet1.ColumnCount = 6;
            this.grdOrdenCompras_Sheet1.RowCount = 12;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Importe";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType5.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType5.MaxLength = 10;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Label = "Folio";
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Width = 80F;
            textCellType6.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType6.MaxLength = 100;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Width = 300F;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Label = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Width = 70F;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Label = "Status";
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Width = 140F;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Label = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Label = "Importe";
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Width = 120F;
            this.grdOrdenCompras_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaPedido);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblPedidoUnidad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPedido);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(856, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Búsqueda";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(574, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "Fecha Pedido :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaPedido
            // 
            this.dtpFechaPedido.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaPedido.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPedido.Location = new System.Drawing.Point(656, 25);
            this.dtpFechaPedido.Name = "dtpFechaPedido";
            this.dtpFechaPedido.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaPedido.TabIndex = 31;
            this.dtpFechaPedido.Value = new System.DateTime(2012, 2, 24, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(288, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "Folio Pedido Unidad :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPedidoUnidad
            // 
            this.lblPedidoUnidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPedidoUnidad.Location = new System.Drawing.Point(414, 25);
            this.lblPedidoUnidad.Name = "lblPedidoUnidad";
            this.lblPedidoUnidad.Size = new System.Drawing.Size(89, 20);
            this.lblPedidoUnidad.TabIndex = 29;
            this.lblPedidoUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(42, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Folio Pedido :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPedido
            // 
            this.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPedido.Location = new System.Drawing.Point(151, 25);
            this.lblPedido.Name = "lblPedido";
            this.lblPedido.Size = new System.Drawing.Size(90, 20);
            this.lblPedido.TabIndex = 26;
            this.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(0, 440);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(874, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Doble clic sobre el renglón para visualizar Detalles de Orden de Compra";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListadoOCProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 464);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoOCProductos";
            this.Text = "Listado de Órdenes de Compra - Código EAN";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdOrdenCompras;
        private FarPoint.Win.Spread.SheetView grdOrdenCompras_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPedido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPedidoUnidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaPedido;
        private System.Windows.Forms.Label lblImpteTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
    }
}