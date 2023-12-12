namespace Facturacion.Informacion
{
    partial class FrmInformacionRemisionadoPendiente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacionRemisionadoPendiente));
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.FrameValidar = new System.Windows.Forms.GroupBox();
            this.chk_02_Servicio = new System.Windows.Forms.CheckBox();
            this.chk_01_Insumo = new System.Windows.Forms.CheckBox();
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
            this.FrameResultados = new System.Windows.Forms.GroupBox();
            this.tabControl_Resultado = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblFinanciamiento = new System.Windows.Forms.Label();
            this.lblFuenteFinanciamiento = new System.Windows.Forms.Label();
            this.txtIdFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdFuenteFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOrigenConsigna = new System.Windows.Forms.CheckBox();
            this.chkOrigenVenta = new System.Windows.Forms.CheckBox();
            this.FrameFacturas.SuspendLayout();
            this.FrameValidar.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.menuFacturas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultados.SuspendLayout();
            this.tabControl_Resultado.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFacturas.Controls.Add(this.groupBox1);
            this.FrameFacturas.Controls.Add(this.FrameValidar);
            this.FrameFacturas.Controls.Add(this.FrameUnidades);
            this.FrameFacturas.Controls.Add(this.FrameDatosOperacion);
            this.FrameFacturas.Controls.Add(this.FrameFechaDeProceso);
            this.FrameFacturas.Location = new System.Drawing.Point(10, 27);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(1163, 160);
            this.FrameFacturas.TabIndex = 1;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Parámetros";
            // 
            // FrameValidar
            // 
            this.FrameValidar.Controls.Add(this.chk_02_Servicio);
            this.FrameValidar.Controls.Add(this.chk_01_Insumo);
            this.FrameValidar.Location = new System.Drawing.Point(611, 101);
            this.FrameValidar.Name = "FrameValidar";
            this.FrameValidar.Size = new System.Drawing.Size(266, 52);
            this.FrameValidar.TabIndex = 2;
            this.FrameValidar.TabStop = false;
            this.FrameValidar.Text = "Validar";
            // 
            // chk_02_Servicio
            // 
            this.chk_02_Servicio.Location = new System.Drawing.Point(141, 19);
            this.chk_02_Servicio.Name = "chk_02_Servicio";
            this.chk_02_Servicio.Size = new System.Drawing.Size(103, 19);
            this.chk_02_Servicio.TabIndex = 3;
            this.chk_02_Servicio.Text = "Servicio";
            this.chk_02_Servicio.UseVisualStyleBackColor = true;
            // 
            // chk_01_Insumo
            // 
            this.chk_01_Insumo.Location = new System.Drawing.Point(29, 19);
            this.chk_01_Insumo.Name = "chk_01_Insumo";
            this.chk_01_Insumo.Size = new System.Drawing.Size(103, 19);
            this.chk_01_Insumo.TabIndex = 2;
            this.chk_01_Insumo.Text = "Producto";
            this.chk_01_Insumo.UseVisualStyleBackColor = true;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.cboTiposDeUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(611, 18);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(265, 77);
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
            this.cboTiposDeUnidades.Location = new System.Drawing.Point(15, 20);
            this.cboTiposDeUnidades.MostrarToolTip = false;
            this.cboTiposDeUnidades.Name = "cboTiposDeUnidades";
            this.cboTiposDeUnidades.Size = new System.Drawing.Size(235, 21);
            this.cboTiposDeUnidades.TabIndex = 0;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.lblFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label9);
            this.FrameDatosOperacion.Controls.Add(this.label7);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Controls.Add(this.txtCte);
            this.FrameDatosOperacion.Controls.Add(this.lblCliente);
            this.FrameDatosOperacion.Controls.Add(this.lblSubCliente);
            this.FrameDatosOperacion.Controls.Add(this.txtSubCte);
            this.FrameDatosOperacion.Controls.Add(this.label5);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(9, 18);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(596, 135);
            this.FrameDatosOperacion.TabIndex = 10;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información general";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
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
            this.txtCte.Location = new System.Drawing.Point(145, 28);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(77, 20);
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
            this.lblCliente.Location = new System.Drawing.Point(229, 28);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(351, 21);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(229, 51);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(351, 21);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(145, 51);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(77, 20);
            this.txtSubCte.TabIndex = 1;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(882, 18);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(266, 77);
            this.FrameFechaDeProceso.TabIndex = 1;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Fechas de dispensación";
            this.FrameFechaDeProceso.Enter += new System.EventHandler(this.FrameFechaDeProceso_Enter);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(69, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(68, 19);
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
            this.dtpFechaFinal.Location = new System.Drawing.Point(115, 45);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(115, 19);
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
            this.lstFacturas.Location = new System.Drawing.Point(795, 7);
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
            this.FrameResultados.Controls.Add(this.tabControl_Resultado);
            this.FrameResultados.Location = new System.Drawing.Point(10, 193);
            this.FrameResultados.Name = "FrameResultados";
            this.FrameResultados.Size = new System.Drawing.Size(1163, 380);
            this.FrameResultados.TabIndex = 2;
            this.FrameResultados.TabStop = false;
            this.FrameResultados.Text = "Resultados";
            // 
            // tabControl_Resultado
            // 
            this.tabControl_Resultado.Controls.Add(this.tabPage1);
            this.tabControl_Resultado.Location = new System.Drawing.Point(9, 19);
            this.tabControl_Resultado.Name = "tabControl_Resultado";
            this.tabControl_Resultado.SelectedIndex = 0;
            this.tabControl_Resultado.Size = new System.Drawing.Size(1143, 355);
            this.tabControl_Resultado.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1135, 329);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblFinanciamiento
            // 
            this.lblFinanciamiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinanciamiento.Location = new System.Drawing.Point(229, 98);
            this.lblFinanciamiento.Name = "lblFinanciamiento";
            this.lblFinanciamiento.Size = new System.Drawing.Size(351, 23);
            this.lblFinanciamiento.TabIndex = 52;
            this.lblFinanciamiento.Text = "Segmento financiamiento :";
            this.lblFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFuenteFinanciamiento
            // 
            this.lblFuenteFinanciamiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFuenteFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuenteFinanciamiento.Location = new System.Drawing.Point(229, 74);
            this.lblFuenteFinanciamiento.Name = "lblFuenteFinanciamiento";
            this.lblFuenteFinanciamiento.Size = new System.Drawing.Size(351, 23);
            this.lblFuenteFinanciamiento.TabIndex = 50;
            this.lblFuenteFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdFinanciamiento
            // 
            this.txtIdFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFinanciamiento.Decimales = 2;
            this.txtIdFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFinanciamiento.Location = new System.Drawing.Point(145, 99);
            this.txtIdFinanciamiento.MaxLength = 4;
            this.txtIdFinanciamiento.Name = "txtIdFinanciamiento";
            this.txtIdFinanciamiento.PermitirApostrofo = false;
            this.txtIdFinanciamiento.PermitirNegativos = false;
            this.txtIdFinanciamiento.Size = new System.Drawing.Size(77, 20);
            this.txtIdFinanciamiento.TabIndex = 3;
            this.txtIdFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFinanciamiento_TextChanged);
            this.txtIdFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFinanciamiento_KeyDown);
            this.txtIdFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFinanciamiento_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(2, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 18);
            this.label9.TabIndex = 49;
            this.label9.Text = "Fuente de financiamiento :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 51;
            this.label7.Text = "Segmento financiamiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFuenteFinanciamiento
            // 
            this.txtIdFuenteFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFuenteFinanciamiento.Decimales = 2;
            this.txtIdFuenteFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFuenteFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFuenteFinanciamiento.Location = new System.Drawing.Point(145, 75);
            this.txtIdFuenteFinanciamiento.MaxLength = 4;
            this.txtIdFuenteFinanciamiento.Name = "txtIdFuenteFinanciamiento";
            this.txtIdFuenteFinanciamiento.PermitirApostrofo = false;
            this.txtIdFuenteFinanciamiento.PermitirNegativos = false;
            this.txtIdFuenteFinanciamiento.Size = new System.Drawing.Size(77, 20);
            this.txtIdFuenteFinanciamiento.TabIndex = 2;
            this.txtIdFuenteFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFuenteFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFuenteFinanciamiento_TextChanged);
            this.txtIdFuenteFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFuenteFinanciamiento_KeyDown);
            this.txtIdFuenteFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFuenteFinanciamiento_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOrigenConsigna);
            this.groupBox1.Controls.Add(this.chkOrigenVenta);
            this.groupBox1.Location = new System.Drawing.Point(882, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 52);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origen de insumo";
            // 
            // chkOrigenConsigna
            // 
            this.chkOrigenConsigna.Location = new System.Drawing.Point(141, 19);
            this.chkOrigenConsigna.Name = "chkOrigenConsigna";
            this.chkOrigenConsigna.Size = new System.Drawing.Size(103, 19);
            this.chkOrigenConsigna.TabIndex = 3;
            this.chkOrigenConsigna.Text = "Consigna";
            this.chkOrigenConsigna.UseVisualStyleBackColor = true;
            // 
            // chkOrigenVenta
            // 
            this.chkOrigenVenta.Location = new System.Drawing.Point(29, 19);
            this.chkOrigenVenta.Name = "chkOrigenVenta";
            this.chkOrigenVenta.Size = new System.Drawing.Size(103, 19);
            this.chkOrigenVenta.TabIndex = 2;
            this.chkOrigenVenta.Text = "Propio";
            this.chkOrigenVenta.UseVisualStyleBackColor = true;
            // 
            // FrmInformacionRemisionadoPendiente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 581);
            this.Controls.Add(this.lstFacturas);
            this.Controls.Add(this.FrameResultados);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInformacionRemisionadoPendiente";
            this.Text = "Reporte de información por remisionar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacionRemisionadoPendiente_Load);
            this.FrameFacturas.ResumeLayout(false);
            this.FrameValidar.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.menuFacturas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultados.ResumeLayout(false);
            this.tabControl_Resultado.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameResultados;
        private System.Windows.Forms.TabControl tabControl_Resultado;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scComboBoxExt cboTiposDeUnidades;
        private System.Windows.Forms.GroupBox FrameValidar;
        private System.Windows.Forms.CheckBox chk_02_Servicio;
        private System.Windows.Forms.CheckBox chk_01_Insumo;
        private System.Windows.Forms.Label lblFinanciamiento;
        private System.Windows.Forms.Label lblFuenteFinanciamiento;
        private SC_ControlsCS.scTextBoxExt txtIdFinanciamiento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtIdFuenteFinanciamiento;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOrigenConsigna;
        private System.Windows.Forms.CheckBox chkOrigenVenta;
    }
}