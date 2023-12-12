namespace DllCompras.Pedidos
{
    partial class FrmCom_ProveedoresConPedidos
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalImpte = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.grdProveedores = new FarPoint.Win.Spread.FpSpread();
            this.menuProveedores = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generarPrePedidoProveedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdProveedores_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedores)).BeginInit();
            this.menuProveedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedores_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblTotalImpte);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblCantidad);
            this.groupBox1.Controls.Add(this.grdProveedores);
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(816, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Proveedores para Generación de Pedidos";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(544, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 29;
            this.label4.Text = "Total Importe :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalImpte
            // 
            this.lblTotalImpte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalImpte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalImpte.Location = new System.Drawing.Point(644, 273);
            this.lblTotalImpte.Name = "lblTotalImpte";
            this.lblTotalImpte.Size = new System.Drawing.Size(145, 20);
            this.lblTotalImpte.TabIndex = 28;
            this.lblTotalImpte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(247, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Total  Cajas  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidad
            // 
            this.lblCantidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(381, 273);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(145, 20);
            this.lblCantidad.TabIndex = 26;
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdProveedores
            // 
            this.grdProveedores.AccessibleDescription = "grdProveedores, Sheet1, Row 0, Column 0, ";
            this.grdProveedores.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProveedores.ContextMenuStrip = this.menuProveedores;
            this.grdProveedores.Location = new System.Drawing.Point(9, 17);
            this.grdProveedores.Name = "grdProveedores";
            this.grdProveedores.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProveedores.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProveedores_Sheet1});
            this.grdProveedores.Size = new System.Drawing.Size(799, 252);
            this.grdProveedores.TabIndex = 0;
            // 
            // menuProveedores
            // 
            this.menuProveedores.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarPrePedidoProveedorToolStripMenuItem});
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.Size = new System.Drawing.Size(214, 26);
            // 
            // generarPrePedidoProveedorToolStripMenuItem
            // 
            this.generarPrePedidoProveedorToolStripMenuItem.Name = "generarPrePedidoProveedorToolStripMenuItem";
            this.generarPrePedidoProveedorToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.generarPrePedidoProveedorToolStripMenuItem.Text = "Generar Orden de Compra";
            this.generarPrePedidoProveedorToolStripMenuItem.Click += new System.EventHandler(this.generarPrePedidoProveedorToolStripMenuItem_Click);
            // 
            // grdProveedores_Sheet1
            // 
            this.grdProveedores_Sheet1.Reset();
            this.grdProveedores_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProveedores_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProveedores_Sheet1.ColumnCount = 4;
            this.grdProveedores_Sheet1.RowCount = 15;
            this.grdProveedores_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave Proveedor";
            this.grdProveedores_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdProveedores_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad Cajas";
            this.grdProveedores_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Importe Total";
            this.grdProveedores_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.grdProveedores_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProveedores_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProveedores_Sheet1.Columns.Get(0).Label = "Clave Proveedor";
            this.grdProveedores_Sheet1.Columns.Get(0).Locked = true;
            this.grdProveedores_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedores_Sheet1.Columns.Get(0).Width = 73F;
            this.grdProveedores_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProveedores_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProveedores_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdProveedores_Sheet1.Columns.Get(1).Locked = true;
            this.grdProveedores_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedores_Sheet1.Columns.Get(1).Width = 430F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProveedores_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdProveedores_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProveedores_Sheet1.Columns.Get(2).Label = "Cantidad Cajas";
            this.grdProveedores_Sheet1.Columns.Get(2).Locked = true;
            this.grdProveedores_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedores_Sheet1.Columns.Get(2).Width = 120F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProveedores_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdProveedores_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProveedores_Sheet1.Columns.Get(3).Label = "Importe Total";
            this.grdProveedores_Sheet1.Columns.Get(3).Locked = true;
            this.grdProveedores_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProveedores_Sheet1.Columns.Get(3).Width = 120F;
            this.grdProveedores_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdProveedores_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 318);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(837, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "Clic derecho para ver menú de opciones";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCom_ProveedoresConPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 342);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCom_ProveedoresConPedidos";
            this.Text = "Proveedores con Pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_ProveedoresConPedidos_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedores)).EndInit();
            this.menuProveedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProveedores_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdProveedores;
        private FarPoint.Win.Spread.SheetView grdProveedores_Sheet1;
        private System.Windows.Forms.ContextMenuStrip menuProveedores;
        private System.Windows.Forms.ToolStripMenuItem generarPrePedidoProveedorToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalImpte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblMensajes;
    }
}