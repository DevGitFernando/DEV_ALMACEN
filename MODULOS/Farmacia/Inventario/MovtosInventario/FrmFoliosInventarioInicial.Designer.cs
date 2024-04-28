namespace Farmacia.Inventario
{
    partial class FrmFoliosInventarioInicial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFoliosInventarioInicial));
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFuente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLicitacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioPresup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCargarFolio = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.FramePedidos.SuspendLayout();
            this.menuPedidos.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(11, 62);
            this.FramePedidos.Margin = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Padding = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Size = new System.Drawing.Size(1069, 428);
            this.FramePedidos.TabIndex = 2;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFolio,
            this.colFuente,
            this.colLicitacion,
            this.colOrden,
            this.colFolioPresup});
            this.listvwPedidos.ContextMenuStrip = this.menuPedidos;
            this.listvwPedidos.HideSelection = false;
            this.listvwPedidos.Location = new System.Drawing.Point(13, 20);
            this.listvwPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(1048, 394);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.Width = 100;
            // 
            // colFuente
            // 
            this.colFuente.Text = "Fuente Financiamiento";
            this.colFuente.Width = 200;
            // 
            // colLicitacion
            // 
            this.colLicitacion.Text = "Licitación";
            this.colLicitacion.Width = 200;
            // 
            // colOrden
            // 
            this.colOrden.Text = "Orden Compra";
            this.colOrden.Width = 100;
            // 
            // colFolioPresup
            // 
            this.colFolioPresup.Text = "Folio Presupuestal";
            this.colFolioPresup.Width = 100;
            // 
            // menuPedidos
            // 
            this.menuPedidos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCargarFolio,
            this.toolStripSeparator9});
            this.menuPedidos.Name = "menuPedidos";
            this.menuPedidos.Size = new System.Drawing.Size(160, 34);
            // 
            // btnCargarFolio
            // 
            this.btnCargarFolio.Name = "btnCargarFolio";
            this.btnCargarFolio.Size = new System.Drawing.Size(159, 24);
            this.btnCargarFolio.Text = "Cargar Folio";
            this.btnCargarFolio.Click += new System.EventHandler(this.btnVerHistorial_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(156, 6);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnSalir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1090, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(104, 55);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FrmFoliosInventarioInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 503);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePedidos);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmFoliosInventarioInicial";
            this.ShowIcon = false;
            this.Text = "Folios Inventario Inicial";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaDeSurtidosPedido_Load);
            this.FramePedidos.ResumeLayout(false);
            this.menuPedidos.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView listvwPedidos;
        private System.Windows.Forms.ContextMenuStrip menuPedidos;
        private System.Windows.Forms.ColumnHeader colFuente;
        private System.Windows.Forms.ColumnHeader colLicitacion;
        private System.Windows.Forms.ColumnHeader colOrden;
        private System.Windows.Forms.ColumnHeader colFolioPresup;
        private System.Windows.Forms.ToolStripMenuItem btnCargarFolio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ColumnHeader colFolio;
    }
}