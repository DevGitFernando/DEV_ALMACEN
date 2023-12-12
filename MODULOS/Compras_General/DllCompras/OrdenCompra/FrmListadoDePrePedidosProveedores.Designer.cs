namespace DllCompras.OrdenCompra
{
    partial class FrmListadoDePrePedidosProveedores
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoDePrePedidosProveedores));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdPedidosProveedores = new FarPoint.Win.Spread.FpSpread();
            this.menuPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generarOrdenDeCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdPedidosProveedores_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.txtProveedor = new SC_ControlsCS.scTextBoxExt();
            this.rdoProveedor = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosProveedores)).BeginInit();
            this.menuPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosProveedores_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdPedidosProveedores);
            this.groupBox1.Location = new System.Drawing.Point(10, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1012, 457);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Proveedores para Generación de Pedidos";
            // 
            // grdPedidosProveedores
            // 
            this.grdPedidosProveedores.AccessibleDescription = "grdPedidosProveedores, Sheet1, Row 0, Column 0, ";
            this.grdPedidosProveedores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPedidosProveedores.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPedidosProveedores.ContextMenuStrip = this.menuPedidos;
            this.grdPedidosProveedores.Location = new System.Drawing.Point(9, 19);
            this.grdPedidosProveedores.Name = "grdPedidosProveedores";
            this.grdPedidosProveedores.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPedidosProveedores.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPedidosProveedores_Sheet1});
            this.grdPedidosProveedores.Size = new System.Drawing.Size(992, 430);
            this.grdPedidosProveedores.TabIndex = 0;
            // 
            // menuPedidos
            // 
            this.menuPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarOrdenDeCompraToolStripMenuItem});
            this.menuPedidos.Name = "menuPedidos";
            this.menuPedidos.Size = new System.Drawing.Size(214, 26);
            // 
            // generarOrdenDeCompraToolStripMenuItem
            // 
            this.generarOrdenDeCompraToolStripMenuItem.Name = "generarOrdenDeCompraToolStripMenuItem";
            this.generarOrdenDeCompraToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.generarOrdenDeCompraToolStripMenuItem.Text = "Generar Orden de Compra";
            this.generarOrdenDeCompraToolStripMenuItem.Click += new System.EventHandler(this.generarOrdenDeCompraToolStripMenuItem_Click);
            // 
            // grdPedidosProveedores_Sheet1
            // 
            this.grdPedidosProveedores_Sheet1.Reset();
            this.grdPedidosProveedores_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPedidosProveedores_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPedidosProveedores_Sheet1.ColumnCount = 6;
            this.grdPedidosProveedores_Sheet1.RowCount = 15;
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave Proveedor";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Folio Pedido";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha Expedición";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status Respuesta";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha Respuesta";
            this.grdPedidosProveedores_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).CellType = textCellType11;
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).Label = "Clave Proveedor";
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(0).Width = 73F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(1).Width = 400F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).CellType = textCellType13;
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).Label = "Folio Pedido";
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(2).Width = 80F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).CellType = textCellType14;
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).Label = "Fecha Expedición";
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(3).Width = 80F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(4).Label = "Status Respuesta";
            this.grdPedidosProveedores_Sheet1.Columns.Get(4).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(4).Width = 90F;
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).CellType = textCellType15;
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).Label = "Fecha Respuesta";
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).Locked = true;
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosProveedores_Sheet1.Columns.Get(5).Width = 80F;
            this.grdPedidosProveedores_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdPedidosProveedores_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1034, 25);
            this.toolStripBarraMenu.TabIndex = 10;
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
            this.btnEjecutar.Text = "&Cancelar (CTRL + E) ";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblProveedor);
            this.groupBox2.Controls.Add(this.txtProveedor);
            this.groupBox2.Controls.Add(this.rdoProveedor);
            this.groupBox2.Controls.Add(this.rdoTodos);
            this.groupBox2.Location = new System.Drawing.Point(10, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1012, 71);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Proveedor";
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(211, 39);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(790, 20);
            this.lblProveedor.TabIndex = 38;
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProveedor
            // 
            this.txtProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProveedor.Decimales = 2;
            this.txtProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtProveedor.Location = new System.Drawing.Point(103, 39);
            this.txtProveedor.MaxLength = 6;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PermitirApostrofo = false;
            this.txtProveedor.PermitirNegativos = false;
            this.txtProveedor.Size = new System.Drawing.Size(102, 20);
            this.txtProveedor.TabIndex = 2;
            this.txtProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyDown);
            this.txtProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtProveedor_Validating);
            // 
            // rdoProveedor
            // 
            this.rdoProveedor.Location = new System.Drawing.Point(27, 41);
            this.rdoProveedor.Name = "rdoProveedor";
            this.rdoProveedor.Size = new System.Drawing.Size(77, 17);
            this.rdoProveedor.TabIndex = 1;
            this.rdoProveedor.TabStop = true;
            this.rdoProveedor.Text = "Proveedor";
            this.rdoProveedor.UseVisualStyleBackColor = true;
            this.rdoProveedor.CheckedChanged += new System.EventHandler(this.rdoProveedor_CheckedChanged);
            // 
            // rdoTodos
            // 
            this.rdoTodos.Location = new System.Drawing.Point(27, 17);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(162, 17);
            this.rdoTodos.TabIndex = 0;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todos los Proveedores";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // FrmListadoDePrePedidosProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmListadoDePrePedidosProveedores";
            this.Text = "Listado de Pre-Pedidos de Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoDePrePedidosProveedores_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosProveedores)).EndInit();
            this.menuPedidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosProveedores_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdPedidosProveedores;
        private FarPoint.Win.Spread.SheetView grdPedidosProveedores_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoProveedor;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.Label lblProveedor;
        private SC_ControlsCS.scTextBoxExt txtProveedor;
        private System.Windows.Forms.ContextMenuStrip menuPedidos;
        private System.Windows.Forms.ToolStripMenuItem generarOrdenDeCompraToolStripMenuItem;
    }
}