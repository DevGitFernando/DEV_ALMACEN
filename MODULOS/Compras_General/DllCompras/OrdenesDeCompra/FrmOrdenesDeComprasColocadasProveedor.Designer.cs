namespace DllCompras.OrdenesDeCompra
{
    partial class FrmOrdenesDeComprasColocadasProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdenesDeComprasColocadasProveedor));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstPedidos = new System.Windows.Forms.ListView();
            this.IdClaveSSA_Sal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPzas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImpte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpUltima = new System.Windows.Forms.GroupBox();
            this.lstUltima = new System.Windows.Forms.ListView();
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMonto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPiezas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpCompras = new System.Windows.Forms.GroupBox();
            this.lstCompras = new System.Windows.Forms.ListView();
            this.colIdProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColFolioOrden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColFechas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImporte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEntregarEn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnUltimaCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.comprasDelProductoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBarraImportacion = new System.Windows.Forms.ToolStrip();
            this.btnExportarPlantilla = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpUltima.SuspendLayout();
            this.grpCompras.SuspendLayout();
            this.menu.SuspendLayout();
            this.toolStripBarraImportacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProveedor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblNomProv);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 58);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Búsqueda";
            // 
            // txtProveedor
            // 
            this.txtProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProveedor.Decimales = 2;
            this.txtProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtProveedor.Location = new System.Drawing.Point(76, 21);
            this.txtProveedor.MaxLength = 8;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PermitirApostrofo = false;
            this.txtProveedor.PermitirNegativos = false;
            this.txtProveedor.Size = new System.Drawing.Size(65, 20);
            this.txtProveedor.TabIndex = 0;
            this.txtProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProveedor_KeyDown);
            this.txtProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtProveedor_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Proveedor :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomProv
            // 
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(147, 21);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(392, 20);
            this.lblNomProv.TabIndex = 26;
            this.lblNomProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(873, 25);
            this.toolStripBarraMenu.TabIndex = 17;
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
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(566, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(296, 58);
            this.FrameFechas.TabIndex = 18;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(192, 21);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(160, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(57, 21);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstPedidos);
            this.groupBox2.Location = new System.Drawing.Point(12, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(850, 170);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordenes Colocadas";
            // 
            // lstPedidos
            // 
            this.lstPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdClaveSSA_Sal,
            this.colClave,
            this.Descripcion,
            this.colPzas,
            this.colImpte});
            this.lstPedidos.Location = new System.Drawing.Point(9, 17);
            this.lstPedidos.Name = "lstPedidos";
            this.lstPedidos.Size = new System.Drawing.Size(833, 146);
            this.lstPedidos.TabIndex = 2;
            this.lstPedidos.UseCompatibleStateImageBehavior = false;
            this.lstPedidos.View = System.Windows.Forms.View.Details;
            // 
            // IdClaveSSA_Sal
            // 
            this.IdClaveSSA_Sal.Text = "IdFarmacia";
            this.IdClaveSSA_Sal.Width = 0;
            // 
            // colClave
            // 
            this.colClave.Text = "Clave SSA";
            this.colClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colClave.Width = 116;
            // 
            // Descripcion
            // 
            this.Descripcion.Text = "Descripción";
            this.Descripcion.Width = 463;
            // 
            // colPzas
            // 
            this.colPzas.Text = "Piezas";
            this.colPzas.Width = 107;
            // 
            // colImpte
            // 
            this.colImpte.Text = "Importe";
            this.colImpte.Width = 114;
            // 
            // grpUltima
            // 
            this.grpUltima.Controls.Add(this.lstUltima);
            this.grpUltima.Location = new System.Drawing.Point(12, 260);
            this.grpUltima.Name = "grpUltima";
            this.grpUltima.Size = new System.Drawing.Size(850, 81);
            this.grpUltima.TabIndex = 28;
            this.grpUltima.TabStop = false;
            this.grpUltima.Text = "Ultima Compra";
            // 
            // lstUltima
            // 
            this.lstUltima.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFolio,
            this.colFecha,
            this.colMonto,
            this.colPiezas});
            this.lstUltima.Location = new System.Drawing.Point(9, 17);
            this.lstUltima.Name = "lstUltima";
            this.lstUltima.Size = new System.Drawing.Size(833, 50);
            this.lstUltima.TabIndex = 2;
            this.lstUltima.UseCompatibleStateImageBehavior = false;
            this.lstUltima.View = System.Windows.Forms.View.Details;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.Width = 199;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFecha.Width = 199;
            // 
            // colMonto
            // 
            this.colMonto.Text = "Monto";
            this.colMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colMonto.Width = 199;
            // 
            // colPiezas
            // 
            this.colPiezas.Text = "Piezas";
            this.colPiezas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colPiezas.Width = 199;
            // 
            // grpCompras
            // 
            this.grpCompras.Controls.Add(this.toolStripBarraImportacion);
            this.grpCompras.Controls.Add(this.lstCompras);
            this.grpCompras.Location = new System.Drawing.Point(9, 343);
            this.grpCompras.Name = "grpCompras";
            this.grpCompras.Size = new System.Drawing.Size(850, 201);
            this.grpCompras.TabIndex = 28;
            this.grpCompras.TabStop = false;
            // 
            // lstCompras
            // 
            this.lstCompras.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdProveedor,
            this.ColProveedor,
            this.ColFolioOrden,
            this.ColFechas,
            this.colImporte,
            this.colCantidad,
            this.colEntregarEn});
            this.lstCompras.Location = new System.Drawing.Point(9, 46);
            this.lstCompras.Name = "lstCompras";
            this.lstCompras.Size = new System.Drawing.Size(833, 146);
            this.lstCompras.TabIndex = 2;
            this.lstCompras.UseCompatibleStateImageBehavior = false;
            this.lstCompras.View = System.Windows.Forms.View.Details;
            // 
            // colIdProveedor
            // 
            this.colIdProveedor.Text = "IdProveedor";
            this.colIdProveedor.Width = 0;
            // 
            // ColProveedor
            // 
            this.ColProveedor.Text = "Proveedor";
            this.ColProveedor.Width = 232;
            // 
            // ColFolioOrden
            // 
            this.ColFolioOrden.Text = "Folio";
            this.ColFolioOrden.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColFolioOrden.Width = 76;
            // 
            // ColFechas
            // 
            this.ColFechas.Text = "Fecha";
            this.ColFechas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColFechas.Width = 76;
            // 
            // colImporte
            // 
            this.colImporte.Text = "Importe";
            this.colImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colImporte.Width = 76;
            // 
            // colCantidad
            // 
            this.colCantidad.Text = "Piezas";
            this.colCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCantidad.Width = 76;
            // 
            // colEntregarEn
            // 
            this.colEntregarEn.Text = "EntregarEn";
            this.colEntregarEn.Width = 265;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUltimaCompra,
            this.comprasDelProductoToolStripMenuItem});
            this.menu.Name = "menuCantidades";
            this.menu.Size = new System.Drawing.Size(194, 48);
            // 
            // btnUltimaCompra
            // 
            this.btnUltimaCompra.Name = "btnUltimaCompra";
            this.btnUltimaCompra.Size = new System.Drawing.Size(193, 22);
            this.btnUltimaCompra.Text = "Última Compra";
            this.btnUltimaCompra.Click += new System.EventHandler(this.btnUltimaCompra_Click);
            // 
            // comprasDelProductoToolStripMenuItem
            // 
            this.comprasDelProductoToolStripMenuItem.Name = "comprasDelProductoToolStripMenuItem";
            this.comprasDelProductoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.comprasDelProductoToolStripMenuItem.Text = "Compras del producto";
            this.comprasDelProductoToolStripMenuItem.Click += new System.EventHandler(this.comprasDelProductoToolStripMenuItem_Click);
            // 
            // toolStripBarraImportacion
            // 
            this.toolStripBarraImportacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarPlantilla});
            this.toolStripBarraImportacion.Location = new System.Drawing.Point(3, 16);
            this.toolStripBarraImportacion.Name = "toolStripBarraImportacion";
            this.toolStripBarraImportacion.Size = new System.Drawing.Size(844, 25);
            this.toolStripBarraImportacion.TabIndex = 3;
            this.toolStripBarraImportacion.Text = "toolStrip1";
            // 
            // btnExportarPlantilla
            // 
            this.btnExportarPlantilla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPlantilla.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPlantilla.Image")));
            this.btnExportarPlantilla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPlantilla.Name = "btnExportarPlantilla";
            this.btnExportarPlantilla.Size = new System.Drawing.Size(23, 22);
            this.btnExportarPlantilla.Text = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.ToolTipText = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.Click += new System.EventHandler(this.btnExportarPlantilla_Click);
            // 
            // FrmOrdenesDeComprasColocadasProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 554);
            this.Controls.Add(this.grpCompras);
            this.Controls.Add(this.grpUltima);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmOrdenesDeComprasColocadasProveedor";
            this.Text = "Ordenes de Compras colocadas por proveedor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmOrdenesDeComprasColocadasProveedor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.grpUltima.ResumeLayout(false);
            this.grpCompras.ResumeLayout(false);
            this.grpCompras.PerformLayout();
            this.menu.ResumeLayout(false);
            this.toolStripBarraImportacion.ResumeLayout(false);
            this.toolStripBarraImportacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNomProv;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstPedidos;
        private System.Windows.Forms.ColumnHeader IdClaveSSA_Sal;
        private System.Windows.Forms.ColumnHeader colClave;
        private System.Windows.Forms.ColumnHeader Descripcion;
        private System.Windows.Forms.ColumnHeader colPzas;
        private System.Windows.Forms.ColumnHeader colImpte;
        private System.Windows.Forms.GroupBox grpUltima;
        private System.Windows.Forms.ListView lstUltima;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colMonto;
        private System.Windows.Forms.ColumnHeader colPiezas;
        private System.Windows.Forms.GroupBox grpCompras;
        private System.Windows.Forms.ListView lstCompras;
        private System.Windows.Forms.ColumnHeader colIdProveedor;
        private System.Windows.Forms.ColumnHeader ColProveedor;
        private System.Windows.Forms.ColumnHeader ColFolioOrden;
        private System.Windows.Forms.ColumnHeader ColFechas;
        private System.Windows.Forms.ColumnHeader colImporte;
        private System.Windows.Forms.ColumnHeader colCantidad;
        private System.Windows.Forms.ColumnHeader colEntregarEn;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem btnUltimaCompra;
        private System.Windows.Forms.ToolStripMenuItem comprasDelProductoToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripBarraImportacion;
        private System.Windows.Forms.ToolStripButton btnExportarPlantilla;
    }
}