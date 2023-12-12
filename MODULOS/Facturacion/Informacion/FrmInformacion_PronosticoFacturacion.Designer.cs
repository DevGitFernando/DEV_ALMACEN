namespace Facturacion.Informacion
{
    partial class FrmInformacion_PronosticoFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacion_PronosticoFacturacion));
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.cboTiposDeUnidades = new SC_ControlsCS.scComboBoxExt();
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
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
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nmPrecioServicio = new System.Windows.Forms.NumericUpDown();
            this.nmPrecioServicioTasaIVA = new System.Windows.Forms.NumericUpDown();
            this.FrameFacturas.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.menuFacturas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioServicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioServicioTasaIVA)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFacturas.Controls.Add(this.FrameUnidades);
            this.FrameFacturas.Controls.Add(this.FrameDatosOperacion);
            this.FrameFacturas.Controls.Add(this.FrameFechaDeProceso);
            this.FrameFacturas.Location = new System.Drawing.Point(10, 27);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(716, 231);
            this.FrameFacturas.TabIndex = 0;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Parámetros";
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.cboTiposDeUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(6, 159);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(375, 62);
            this.FrameUnidades.TabIndex = 0;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Tipo de unidades";
            // 
            // cboTiposDeUnidades
            // 
            this.cboTiposDeUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTiposDeUnidades.BackColorEnabled = System.Drawing.Color.White;
            this.cboTiposDeUnidades.Data = "";
            this.cboTiposDeUnidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposDeUnidades.Filtro = " 1 = 1";
            this.cboTiposDeUnidades.FormattingEnabled = true;
            this.cboTiposDeUnidades.ListaItemsBusqueda = 20;
            this.cboTiposDeUnidades.Location = new System.Drawing.Point(15, 26);
            this.cboTiposDeUnidades.MostrarToolTip = false;
            this.cboTiposDeUnidades.Name = "cboTiposDeUnidades";
            this.cboTiposDeUnidades.Size = new System.Drawing.Size(345, 21);
            this.cboTiposDeUnidades.TabIndex = 0;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.nmPrecioServicioTasaIVA);
            this.FrameDatosOperacion.Controls.Add(this.nmPrecioServicio);
            this.FrameDatosOperacion.Controls.Add(this.label1);
            this.FrameDatosOperacion.Controls.Add(this.label2);
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Controls.Add(this.txtCte);
            this.FrameDatosOperacion.Controls.Add(this.lblCliente);
            this.FrameDatosOperacion.Controls.Add(this.lblSubCliente);
            this.FrameDatosOperacion.Controls.Add(this.txtSubCte);
            this.FrameDatosOperacion.Controls.Add(this.label5);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(9, 18);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(693, 135);
            this.FrameDatosOperacion.TabIndex = 10;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información general";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 18);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(149, 20);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(80, 20);
            this.txtCte.TabIndex = 0;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(235, 20);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(443, 21);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(235, 43);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(443, 21);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(149, 43);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(80, 20);
            this.txtSubCte.TabIndex = 1;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 18);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(387, 159);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(315, 62);
            this.FrameFechaDeProceso.TabIndex = 1;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Fechas de dispensación";
            this.FrameFechaDeProceso.Enter += new System.EventHandler(this.FrameFechaDeProceso_Enter);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(168, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(19, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Desde :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(214, 26);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(66, 26);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
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
            this.lstFacturas.HideSelection = false;
            this.lstFacturas.Location = new System.Drawing.Point(345, 7);
            this.lstFacturas.LockColumnSize = false;
            this.lstFacturas.Name = "lstFacturas";
            this.lstFacturas.Size = new System.Drawing.Size(76, 18);
            this.lstFacturas.TabIndex = 6;
            this.lstFacturas.UseCompatibleStateImageBehavior = false;
            this.lstFacturas.View = System.Windows.Forms.View.Details;
            this.lstFacturas.Visible = false;
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
            this.btnEjecutar.Enabled = false;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Visible = false;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
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
            this.btnExportar.Enabled = false;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Visible = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.toolStripSeparator4,
            this.btnExportar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(736, 25);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 47;
            this.label1.Text = "Precio del servicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 18);
            this.label2.TabIndex = 48;
            this.label2.Text = "Tasa de IVA del servicio :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmPrecioServicio
            // 
            this.nmPrecioServicio.DecimalPlaces = 2;
            this.nmPrecioServicio.Location = new System.Drawing.Point(149, 68);
            this.nmPrecioServicio.Name = "nmPrecioServicio";
            this.nmPrecioServicio.Size = new System.Drawing.Size(80, 20);
            this.nmPrecioServicio.TabIndex = 2;
            this.nmPrecioServicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmPrecioServicioTasaIVA
            // 
            this.nmPrecioServicioTasaIVA.DecimalPlaces = 2;
            this.nmPrecioServicioTasaIVA.Location = new System.Drawing.Point(149, 94);
            this.nmPrecioServicioTasaIVA.Name = "nmPrecioServicioTasaIVA";
            this.nmPrecioServicioTasaIVA.Size = new System.Drawing.Size(80, 20);
            this.nmPrecioServicioTasaIVA.TabIndex = 3;
            this.nmPrecioServicioTasaIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmInformacion_PronosticoFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 266);
            this.Controls.Add(this.lstFacturas);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInformacion_PronosticoFacturacion";
            this.Text = "Pronóstico de facturación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacion_PronosticoFacturacion_Load);
            this.FrameFacturas.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.menuFacturas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioServicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioServicioTasaIVA)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scComboBoxExt cboTiposDeUnidades;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmPrecioServicioTasaIVA;
        private System.Windows.Forms.NumericUpDown nmPrecioServicio;
    }
}