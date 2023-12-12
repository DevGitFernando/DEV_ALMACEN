namespace DllCompras.Pedidos
{
    partial class FrmConcentradoPedidosOCEN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConcentradoPedidosOCEN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdPedidos = new FarPoint.Win.Spread.FpSpread();
            this.cmnPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdPedidos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).BeginInit();
            this.cmnPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(898, 25);
            this.toolStripBarraMenu.TabIndex = 8;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdPedidos);
            this.groupBox1.Location = new System.Drawing.Point(10, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(877, 386);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Claves - Códigos EAN requeridos";
            // 
            // grdPedidos
            // 
            this.grdPedidos.AccessibleDescription = "grdPedidos, Sheet1, Row 0, Column 0, ";
            this.grdPedidos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPedidos.ContextMenuStrip = this.cmnPedidos;
            this.grdPedidos.Location = new System.Drawing.Point(10, 20);
            this.grdPedidos.Name = "grdPedidos";
            this.grdPedidos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPedidos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPedidos_Sheet1});
            this.grdPedidos.Size = new System.Drawing.Size(859, 357);
            this.grdPedidos.TabIndex = 9;
            this.grdPedidos.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdPedidos_CellClick);
            // 
            // cmnPedidos
            // 
            this.cmnPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem,
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem});
            this.cmnPedidos.Name = "cmnPedidos";
            this.cmnPedidos.Size = new System.Drawing.Size(304, 48);
            // 
            // mostrarProveedoresPorClaveCodigoEANToolStripMenuItem
            // 
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem.Name = "mostrarProveedoresPorClaveCodigoEANToolStripMenuItem";
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem.Size = new System.Drawing.Size(303, 22);
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem.Text = "Mostrar Proveedores por Clave-CodigoEAN";
            this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem.Click += new System.EventHandler(this.mostrarProveedoresPorClaveCodigoEANToolStripMenuItem_Click);
            // 
            // mostrarListaDeProveedoresConPedidoToolStripMenuItem
            // 
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem.Name = "mostrarListaDeProveedoresConPedidoToolStripMenuItem";
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem.Size = new System.Drawing.Size(303, 22);
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem.Text = "Mostrar lista de Proveedores con Pedido";
            this.mostrarListaDeProveedoresConPedidoToolStripMenuItem.Click += new System.EventHandler(this.mostrarListaDeProveedoresConPedidoToolStripMenuItem_Click);
            // 
            // grdPedidos_Sheet1
            // 
            this.grdPedidos_Sheet1.Reset();
            this.grdPedidos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPedidos_Sheet1.ColumnCount = 6;
            this.grdPedidos_Sheet1.RowCount = 15;
            this.grdPedidos_Sheet1.Cells.Get(0, 3).Value = 74;
            this.grdPedidos_Sheet1.Cells.Get(0, 4).Value = 50;
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA Interna";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Contenido Paquete";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Unidades a Comprar";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Unidades Asignadas";
            this.grdPedidos_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            this.grdPedidos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdPedidos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(0).Label = "Clave SSA Interna";
            this.grdPedidos_Sheet1.Columns.Get(0).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(0).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(0).Width = 70F;
            this.grdPedidos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdPedidos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdPedidos_Sheet1.Columns.Get(1).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(1).Width = 120F;
            this.grdPedidos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdPedidos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPedidos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdPedidos_Sheet1.Columns.Get(2).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(2).Width = 450F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdPedidos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdPedidos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(3).Label = "Contenido Paquete";
            this.grdPedidos_Sheet1.Columns.Get(3).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(3).Width = 70F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdPedidos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdPedidos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdPedidos_Sheet1.Columns.Get(4).Label = "Unidades a Comprar";
            this.grdPedidos_Sheet1.Columns.Get(4).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(4).Width = 80F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdPedidos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdPedidos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdPedidos_Sheet1.Columns.Get(5).Label = "Unidades Asignadas";
            this.grdPedidos_Sheet1.Columns.Get(5).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(5).Width = 80F;
            this.grdPedidos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboEmpresas);
            this.groupBox2.Controls.Add(this.cboEdo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(10, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(877, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estados";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Empresa :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(84, 17);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(314, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(499, 18);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(349, 21);
            this.cboEdo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(445, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 472);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(898, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "Clic derecho para ver menú de opciones";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmConcentradoPedidosOCEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 496);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConcentradoPedidosOCEN";
            this.Text = "Concentrado de Claves para Pedido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_ConcentradoPedidos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).EndInit();
            this.cmnPedidos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdPedidos;
        private FarPoint.Win.Spread.SheetView grdPedidos_Sheet1;
        private System.Windows.Forms.ContextMenuStrip cmnPedidos;
        private System.Windows.Forms.ToolStripMenuItem mostrarListaDeProveedoresConPedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mostrarProveedoresPorClaveCodigoEANToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMensajes;
    }
}