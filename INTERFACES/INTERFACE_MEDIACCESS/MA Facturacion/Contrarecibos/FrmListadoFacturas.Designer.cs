namespace MA_Facturacion.Contrarecibos
{
    partial class FrmListadoFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoFacturas));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkTodasFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameInsumos = new System.Windows.Forms.GroupBox();
            this.rdoAmbosInsumos = new System.Windows.Forms.RadioButton();
            this.rdoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameTipoFacturas = new System.Windows.Forms.GroupBox();
            this.rdoSinCobrar = new System.Windows.Forms.RadioButton();
            this.rdoEnCobro = new System.Windows.Forms.RadioButton();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.lstFacturas = new SC_ControlsCS.scListView();
            this.Folio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumFactura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FechaFact = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TipoFact = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Insumo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.lblMensajes = new System.Windows.Forms.Label();
            this.menuFacturas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnExportaFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameTipoFacturas.SuspendLayout();
            this.FrameFacturas.SuspendLayout();
            this.menuFacturas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(872, 25);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
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
            this.toolStripSeparator2.Visible = false;
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
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.chkTodasFechas);
            this.FrameFechas.Controls.Add(this.dtpFechaFin);
            this.FrameFechas.Controls.Add(this.label1);
            this.FrameFechas.Controls.Add(this.dtpFechaInicio);
            this.FrameFechas.Controls.Add(this.label9);
            this.FrameFechas.Location = new System.Drawing.Point(449, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(408, 104);
            this.FrameFechas.TabIndex = 12;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // chkTodasFechas
            // 
            this.chkTodasFechas.AutoSize = true;
            this.chkTodasFechas.Location = new System.Drawing.Point(278, -1);
            this.chkTodasFechas.Name = "chkTodasFechas";
            this.chkTodasFechas.Size = new System.Drawing.Size(110, 17);
            this.chkTodasFechas.TabIndex = 57;
            this.chkTodasFechas.Text = "Todas las Fechas";
            this.chkTodasFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(291, 42);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaFin.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(207, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Fecha Fin :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(92, 42);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaInicio.TabIndex = 53;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Fecha Inicio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoAmbosInsumos);
            this.FrameInsumos.Controls.Add(this.rdoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(13, 81);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(430, 51);
            this.FrameInsumos.TabIndex = 11;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo Insumos";
            // 
            // rdoAmbosInsumos
            // 
            this.rdoAmbosInsumos.Checked = true;
            this.rdoAmbosInsumos.Location = new System.Drawing.Point(329, 20);
            this.rdoAmbosInsumos.Name = "rdoAmbosInsumos";
            this.rdoAmbosInsumos.Size = new System.Drawing.Size(85, 17);
            this.rdoAmbosInsumos.TabIndex = 2;
            this.rdoAmbosInsumos.TabStop = true;
            this.rdoAmbosInsumos.Text = "Ambos";
            this.rdoAmbosInsumos.UseVisualStyleBackColor = true;
            // 
            // rdoMatCuracion
            // 
            this.rdoMatCuracion.Location = new System.Drawing.Point(177, 20);
            this.rdoMatCuracion.Name = "rdoMatCuracion";
            this.rdoMatCuracion.Size = new System.Drawing.Size(127, 17);
            this.rdoMatCuracion.TabIndex = 1;
            this.rdoMatCuracion.Text = "Material de Curación";
            this.rdoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoMedicamento
            // 
            this.rdoMedicamento.Location = new System.Drawing.Point(40, 20);
            this.rdoMedicamento.Name = "rdoMedicamento";
            this.rdoMedicamento.Size = new System.Drawing.Size(122, 17);
            this.rdoMedicamento.TabIndex = 0;
            this.rdoMedicamento.Text = "Medicamento";
            this.rdoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameTipoFacturas
            // 
            this.FrameTipoFacturas.Controls.Add(this.rdoSinCobrar);
            this.FrameTipoFacturas.Controls.Add(this.rdoEnCobro);
            this.FrameTipoFacturas.Controls.Add(this.rdoAmbos);
            this.FrameTipoFacturas.Location = new System.Drawing.Point(13, 28);
            this.FrameTipoFacturas.Name = "FrameTipoFacturas";
            this.FrameTipoFacturas.Size = new System.Drawing.Size(430, 51);
            this.FrameTipoFacturas.TabIndex = 10;
            this.FrameTipoFacturas.TabStop = false;
            this.FrameTipoFacturas.Text = "Tipo Facturas";
            // 
            // rdoSinCobrar
            // 
            this.rdoSinCobrar.Location = new System.Drawing.Point(329, 20);
            this.rdoSinCobrar.Name = "rdoSinCobrar";
            this.rdoSinCobrar.Size = new System.Drawing.Size(85, 17);
            this.rdoSinCobrar.TabIndex = 2;
            this.rdoSinCobrar.Text = "Sin Cobrar";
            this.rdoSinCobrar.UseVisualStyleBackColor = true;
            // 
            // rdoEnCobro
            // 
            this.rdoEnCobro.Location = new System.Drawing.Point(177, 20);
            this.rdoEnCobro.Name = "rdoEnCobro";
            this.rdoEnCobro.Size = new System.Drawing.Size(127, 17);
            this.rdoEnCobro.TabIndex = 1;
            this.rdoEnCobro.Text = "En Cobro";
            this.rdoEnCobro.UseVisualStyleBackColor = true;
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Checked = true;
            this.rdoAmbos.Location = new System.Drawing.Point(40, 20);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(122, 17);
            this.rdoAmbos.TabIndex = 0;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Controls.Add(this.lstFacturas);
            this.FrameFacturas.Location = new System.Drawing.Point(13, 134);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(844, 333);
            this.FrameFacturas.TabIndex = 9;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Listado de Facturas";
            // 
            // lstFacturas
            // 
            this.lstFacturas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Folio,
            this.NumFactura,
            this.FechaFact,
            this.TipoFact,
            this.Total,
            this.Status,
            this.Insumo});
            this.lstFacturas.ContextMenuStrip = this.menuFacturas;
            this.lstFacturas.Location = new System.Drawing.Point(12, 19);
            this.lstFacturas.LockColumnSize = false;
            this.lstFacturas.Name = "lstFacturas";
            this.lstFacturas.Size = new System.Drawing.Size(820, 302);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 469);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(872, 24);
            this.lblMensajes.TabIndex = 13;
            this.lblMensajes.Text = " < Click derecho >  Ver Opciones";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuFacturas
            // 
            this.menuFacturas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportaFactura,
            this.toolStripSeparator3});
            this.menuFacturas.Name = "menuPedidos";
            this.menuFacturas.Size = new System.Drawing.Size(183, 54);
            // 
            // btnExportaFactura
            // 
            this.btnExportaFactura.Name = "btnExportaFactura";
            this.btnExportaFactura.Size = new System.Drawing.Size(182, 22);
            this.btnExportaFactura.Text = "Exportar Facturación";
            this.btnExportaFactura.Click += new System.EventHandler(this.btnExportaFactura_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // FrmListadoFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 493);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.FrameTipoFacturas);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoFacturas";
            this.Text = "Listado de Facturas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoFacturas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameFechas.PerformLayout();
            this.FrameInsumos.ResumeLayout(false);
            this.FrameTipoFacturas.ResumeLayout(false);
            this.FrameFacturas.ResumeLayout(false);
            this.menuFacturas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.CheckBox chkTodasFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoAmbosInsumos;
        private System.Windows.Forms.RadioButton rdoMatCuracion;
        private System.Windows.Forms.RadioButton rdoMedicamento;
        private System.Windows.Forms.GroupBox FrameTipoFacturas;
        private System.Windows.Forms.RadioButton rdoSinCobrar;
        private System.Windows.Forms.RadioButton rdoEnCobro;
        private System.Windows.Forms.RadioButton rdoAmbos;
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
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ContextMenuStrip menuFacturas;
        private System.Windows.Forms.ToolStripMenuItem btnExportaFactura;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}