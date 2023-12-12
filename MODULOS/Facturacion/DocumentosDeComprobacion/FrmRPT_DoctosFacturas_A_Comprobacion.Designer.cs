namespace Facturacion.DocumentosDeComprobacion
{
    partial class FrmRPT_DoctosFacturas_A_Comprobacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRPT_DoctosFacturas_A_Comprobacion));
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
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
            this.FrameResultados = new System.Windows.Forms.GroupBox();
            this.lstFacturas = new SC_ControlsCS.scListView();
            this.rdo_01_PendienteDeComprobar = new System.Windows.Forms.RadioButton();
            this.rdo_02_ComprobacionCompleta = new System.Windows.Forms.RadioButton();
            this.rdo_03_General = new System.Windows.Forms.RadioButton();
            this.FrameFacturas.SuspendLayout();
            this.menuFacturas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFacturas.Controls.Add(this.rdo_03_General);
            this.FrameFacturas.Controls.Add(this.rdo_02_ComprobacionCompleta);
            this.FrameFacturas.Controls.Add(this.rdo_01_PendienteDeComprobar);
            this.FrameFacturas.Location = new System.Drawing.Point(10, 27);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(1163, 59);
            this.FrameFacturas.TabIndex = 1;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Parámetros";
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
            this.btnExportar.Text = "&Exportar";
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
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // FrameResultados
            // 
            this.FrameResultados.Controls.Add(this.lstFacturas);
            this.FrameResultados.Location = new System.Drawing.Point(10, 92);
            this.FrameResultados.Name = "FrameResultados";
            this.FrameResultados.Size = new System.Drawing.Size(1163, 481);
            this.FrameResultados.TabIndex = 2;
            this.FrameResultados.TabStop = false;
            this.FrameResultados.Text = "Resultados";
            // 
            // lstFacturas
            // 
            this.lstFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFacturas.ContextMenuStrip = this.menuFacturas;
            this.lstFacturas.Location = new System.Drawing.Point(15, 19);
            this.lstFacturas.LockColumnSize = false;
            this.lstFacturas.Name = "lstFacturas";
            this.lstFacturas.Size = new System.Drawing.Size(1132, 452);
            this.lstFacturas.TabIndex = 0;
            this.lstFacturas.UseCompatibleStateImageBehavior = false;
            this.lstFacturas.View = System.Windows.Forms.View.Details;
            // 
            // rdo_01_PendienteDeComprobar
            // 
            this.rdo_01_PendienteDeComprobar.Location = new System.Drawing.Point(161, 19);
            this.rdo_01_PendienteDeComprobar.Name = "rdo_01_PendienteDeComprobar";
            this.rdo_01_PendienteDeComprobar.Size = new System.Drawing.Size(225, 24);
            this.rdo_01_PendienteDeComprobar.TabIndex = 0;
            this.rdo_01_PendienteDeComprobar.TabStop = true;
            this.rdo_01_PendienteDeComprobar.Text = "Cantidad parcialmente comprobada";
            this.rdo_01_PendienteDeComprobar.UseVisualStyleBackColor = true;
            // 
            // rdo_02_ComprobacionCompleta
            // 
            this.rdo_02_ComprobacionCompleta.Location = new System.Drawing.Point(469, 19);
            this.rdo_02_ComprobacionCompleta.Name = "rdo_02_ComprobacionCompleta";
            this.rdo_02_ComprobacionCompleta.Size = new System.Drawing.Size(225, 24);
            this.rdo_02_ComprobacionCompleta.TabIndex = 1;
            this.rdo_02_ComprobacionCompleta.TabStop = true;
            this.rdo_02_ComprobacionCompleta.Text = "Cantidad comprobada total";
            this.rdo_02_ComprobacionCompleta.UseVisualStyleBackColor = true;
            // 
            // rdo_03_General
            // 
            this.rdo_03_General.Location = new System.Drawing.Point(777, 19);
            this.rdo_03_General.Name = "rdo_03_General";
            this.rdo_03_General.Size = new System.Drawing.Size(225, 24);
            this.rdo_03_General.TabIndex = 2;
            this.rdo_03_General.TabStop = true;
            this.rdo_03_General.Text = "General";
            this.rdo_03_General.UseVisualStyleBackColor = true;
            // 
            // FrmRPT_DoctosFacturas_A_Comprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 581);
            this.Controls.Add(this.FrameResultados);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRPT_DoctosFacturas_A_Comprobacion";
            this.Text = "Reporte de facturas en comprobación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRPT_DoctosFacturas_A_Comprobacion_Load);
            this.FrameFacturas.ResumeLayout(false);
            this.menuFacturas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultados.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox FrameFacturas;
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
        private System.Windows.Forms.GroupBox FrameResultados;
        private SC_ControlsCS.scListView lstFacturas;
        private System.Windows.Forms.RadioButton rdo_03_General;
        private System.Windows.Forms.RadioButton rdo_02_ComprobacionCompleta;
        private System.Windows.Forms.RadioButton rdo_01_PendienteDeComprobar;
    }
}