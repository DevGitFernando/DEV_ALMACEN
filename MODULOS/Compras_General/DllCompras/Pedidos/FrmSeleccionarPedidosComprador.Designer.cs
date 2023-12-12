namespace DllCompras.Pedidos
{
    partial class FrmSeleccionarPedidosComprador
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeleccionarPedidosComprador));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdPedidosDisponibles = new FarPoint.Win.Spread.FpSpread();
            this.cmnPedidosDisponibles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verDetallesDePedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechazarPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grdPedidosDisponibles_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosDisponibles)).BeginInit();
            this.cmnPedidosDisponibles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosDisponibles_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdPedidosDisponibles);
            this.groupBox1.Location = new System.Drawing.Point(12, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(722, 380);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Pedidos Disponibles";
            // 
            // grdPedidosDisponibles
            // 
            this.grdPedidosDisponibles.AccessibleDescription = "grdPedidosDisponibles, Sheet1, Row 0, Column 0, ";
            this.grdPedidosDisponibles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPedidosDisponibles.ContextMenuStrip = this.cmnPedidosDisponibles;
            this.grdPedidosDisponibles.Location = new System.Drawing.Point(10, 20);
            this.grdPedidosDisponibles.Name = "grdPedidosDisponibles";
            this.grdPedidosDisponibles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPedidosDisponibles.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPedidosDisponibles_Sheet1});
            this.grdPedidosDisponibles.Size = new System.Drawing.Size(703, 352);
            this.grdPedidosDisponibles.TabIndex = 1;
            // 
            // cmnPedidosDisponibles
            // 
            this.cmnPedidosDisponibles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verDetallesDePedidoToolStripMenuItem,
            this.rechazarPedidoToolStripMenuItem});
            this.cmnPedidosDisponibles.Name = "cmnPedidosDisponibles";
            this.cmnPedidosDisponibles.Size = new System.Drawing.Size(191, 48);
            // 
            // verDetallesDePedidoToolStripMenuItem
            // 
            this.verDetallesDePedidoToolStripMenuItem.Name = "verDetallesDePedidoToolStripMenuItem";
            this.verDetallesDePedidoToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.verDetallesDePedidoToolStripMenuItem.Text = "Ver detalles de Pedido";
            this.verDetallesDePedidoToolStripMenuItem.Click += new System.EventHandler(this.verDetallesDePedidoToolStripMenuItem_Click);
            // 
            // rechazarPedidoToolStripMenuItem
            // 
            this.rechazarPedidoToolStripMenuItem.Name = "rechazarPedidoToolStripMenuItem";
            this.rechazarPedidoToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.rechazarPedidoToolStripMenuItem.Text = "Rechazar Pedido";
            this.rechazarPedidoToolStripMenuItem.Click += new System.EventHandler(this.rechazarPedidoToolStripMenuItem_Click);
            // 
            // grdPedidosDisponibles_Sheet1
            // 
            this.grdPedidosDisponibles_Sheet1.Reset();
            this.grdPedidosDisponibles_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPedidosDisponibles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPedidosDisponibles_Sheet1.ColumnCount = 8;
            this.grdPedidosDisponibles_Sheet1.RowCount = 15;
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Tipo de Pedido";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Tipos de claves";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Tipo de Pedido";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Folio Pedido";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "FolioPedidounidad";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha";
            this.grdPedidosDisponibles_Sheet1.ColumnHeader.Rows.Get(0).Height = 31F;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(0).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(1).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(1).Width = 259F;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).CellType = textCellType11;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).Label = "Tipo de Pedido";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).Visible = false;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(2).Width = 51F;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).CellType = textCellType12;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).Label = "Tipos de claves";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(3).Visible = false;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(4).CellType = textCellType13;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(4).Label = "Tipo de Pedido";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(4).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(4).Width = 198F;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(5).CellType = textCellType14;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(5).Label = "Folio Pedido";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(5).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).CellType = textCellType15;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).Label = "FolioPedidounidad";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(6).Visible = false;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).CellType = textCellType16;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).Label = "Fecha";
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).Locked = true;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidosDisponibles_Sheet1.Columns.Get(7).Width = 70F;
            this.grdPedidosDisponibles_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdPedidosDisponibles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(743, 25);
            this.toolStripBarraMenu.TabIndex = 9;
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cboEmpresas);
            this.groupBox2.Controls.Add(this.cboEdo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(722, 50);
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
            this.cboEdo.Location = new System.Drawing.Point(465, 18);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(232, 21);
            this.cboEdo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(411, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 462);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(743, 24);
            this.label11.TabIndex = 14;
            this.label11.Text = "Clic derecho para ver menú de opciones";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSeleccionarPedidosComprador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 486);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSeleccionarPedidosComprador";
            this.Text = "Selección  de Pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSeleccionarPedidosComprador_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosDisponibles)).EndInit();
            this.cmnPedidosDisponibles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidosDisponibles_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdPedidosDisponibles;
        private FarPoint.Win.Spread.SheetView grdPedidosDisponibles_Sheet1;
        private System.Windows.Forms.ContextMenuStrip cmnPedidosDisponibles;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verDetallesDePedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rechazarPedidoToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label11;
    }
}