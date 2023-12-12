namespace Facturacion.Informacion
{
    partial class FrmInformacionReplicacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacionReplicacion));
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.lstFacturas = new SC_ControlsCS.scListView();
            this.Folio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumFactura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaFact = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TipoFact = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Insumo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuFacturas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnExportaFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnGetInformacion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameFacturas.SuspendLayout();
            this.menuFacturas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFacturas.Controls.Add(this.lstFacturas);
            this.FrameFacturas.Location = new System.Drawing.Point(14, 27);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(1156, 493);
            this.FrameFacturas.TabIndex = 9;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Listado de Unidades";
            // 
            // lstFacturas
            // 
            this.lstFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFacturas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Folio,
            this.NumFactura,
            this.FechaFact,
            this.TipoFact,
            this.Total,
            this.Status,
            this.Insumo});
            this.lstFacturas.ContextMenuStrip = this.menuFacturas;
            this.lstFacturas.HideSelection = false;
            this.lstFacturas.Location = new System.Drawing.Point(12, 19);
            this.lstFacturas.LockColumnSize = false;
            this.lstFacturas.Name = "lstFacturas";
            this.lstFacturas.Size = new System.Drawing.Size(1132, 462);
            this.lstFacturas.TabIndex = 6;
            this.lstFacturas.UseCompatibleStateImageBehavior = false;
            this.lstFacturas.View = System.Windows.Forms.View.Details;
            // 
            // Folio
            // 
            this.Folio.Text = "Folio Factura";
            this.Folio.Width = 120;
            // 
            // NumFactura
            // 
            this.NumFactura.Text = "Núm. Factura";
            this.NumFactura.Width = 180;
            // 
            // FechaFact
            // 
            this.FechaFact.Text = "Fecha Factura";
            this.FechaFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FechaFact.Width = 100;
            // 
            // TipoFact
            // 
            this.TipoFact.Text = "Tipo Factura";
            this.TipoFact.Width = 150;
            // 
            // Total
            // 
            this.Total.Text = "Importe";
            this.Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total.Width = 120;
            // 
            // Status
            // 
            this.Status.Text = "Status Factura";
            this.Status.Width = 120;
            // 
            // Insumo
            // 
            this.Insumo.Text = "Tipo de Insumo";
            this.Insumo.Width = 150;
            // 
            // menuFacturas
            // 
            this.menuFacturas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportaFactura,
            this.toolStripSeparator3});
            this.menuFacturas.Name = "menuPedidos";
            this.menuFacturas.Size = new System.Drawing.Size(184, 32);
            // 
            // btnExportaFactura
            // 
            this.btnExportaFactura.Name = "btnExportaFactura";
            this.btnExportaFactura.Size = new System.Drawing.Size(183, 22);
            this.btnExportaFactura.Text = "Exportar Facturación";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
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
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Imprimir";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportar,
            this.toolStripSeparator2,
            this.btnGetInformacion,
            this.toolStripSeparator4,
            this.btnIntegrarPaquetesDeDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnGetInformacion
            // 
            this.btnGetInformacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGetInformacion.Image = ((System.Drawing.Image)(resources.GetObject("btnGetInformacion.Image")));
            this.btnGetInformacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetInformacion.Name = "btnGetInformacion";
            this.btnGetInformacion.Size = new System.Drawing.Size(23, 22);
            this.btnGetInformacion.Text = "Solicitar Información de Catalogos";
            this.btnGetInformacion.Visible = false;
            this.btnGetInformacion.Click += new System.EventHandler(this.btnGetInformacion_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // btnIntegrarPaquetesDeDatos
            // 
            this.btnIntegrarPaquetesDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarPaquetesDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarPaquetesDeDatos.Image")));
            this.btnIntegrarPaquetesDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarPaquetesDeDatos.Name = "btnIntegrarPaquetesDeDatos";
            this.btnIntegrarPaquetesDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarPaquetesDeDatos.Text = "Integrar transferencias";
            this.btnIntegrarPaquetesDeDatos.Visible = false;
            this.btnIntegrarPaquetesDeDatos.Click += new System.EventHandler(this.btnIntegrarPaquetesDeDatos_Click);
            // 
            // FrmInformacionReplicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 532);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInformacionReplicacion";
            this.Text = "Bitacora de actualización de información";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacionReplicacion_Load);
            this.FrameFacturas.ResumeLayout(false);
            this.menuFacturas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox FrameFacturas;
        private SC_ControlsCS.scListView lstFacturas;
        private System.Windows.Forms.ColumnHeader Folio;
        private System.Windows.Forms.ColumnHeader NumFactura;
        private System.Windows.Forms.ColumnHeader FechaFact;
        private System.Windows.Forms.ColumnHeader TipoFact;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader Insumo;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.ContextMenuStrip menuFacturas;
        private System.Windows.Forms.ToolStripMenuItem btnExportaFactura;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.ToolStripButton btnGetInformacion;
    }
}