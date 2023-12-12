namespace Almacen.PedidosEspeciales
{
    partial class FrmRegistroPedidosEspecialesVentaSocioComercial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroPedidosEspecialesVentaSocioComercial));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReferenciaPedido = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipoPedido = new SC_ControlsCS.scComboBoxExt();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.txtIdSucursal = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSocioComercial = new System.Windows.Forms.Label();
            this.txtIdSocioComercial = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImportarPedidoMasivo = new System.Windows.Forms.ToolStripButton();
            this.frameImportacion = new System.Windows.Forms.GroupBox();
            this.lblTituloHoja = new System.Windows.Forms.Label();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraImportacion = new System.Windows.Forms.ToolStrip();
            this.btnExportarPlantilla = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar_CargaMasiva = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCajasCompletas = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblProcesados = new SC_ControlsCS.scLabelExt();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.FrameEncabezado.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.frameImportacion.SuspendLayout();
            this.toolStripBarraImportacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEncabezado.Controls.Add(this.dtpFechaEntrega);
            this.FrameEncabezado.Controls.Add(this.label7);
            this.FrameEncabezado.Controls.Add(this.txtReferenciaPedido);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.label6);
            this.FrameEncabezado.Controls.Add(this.cboTipoPedido);
            this.FrameEncabezado.Controls.Add(this.lblSucursal);
            this.FrameEncabezado.Controls.Add(this.txtIdSucursal);
            this.FrameEncabezado.Controls.Add(this.label5);
            this.FrameEncabezado.Controls.Add(this.lblSocioComercial);
            this.FrameEncabezado.Controls.Add(this.txtIdSocioComercial);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Controls.Add(this.txtObservaciones);
            this.FrameEncabezado.Controls.Add(this.label10);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.lblCancelado);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(12, 30);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(971, 173);
            this.FrameEncabezado.TabIndex = 2;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales de Venta a socio comercial";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(652, 20);
            this.dtpFechaEntrega.MinDate = new System.DateTime(2018, 4, 4, 0, 0, 0, 0);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaEntrega.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(544, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 14);
            this.label7.TabIndex = 54;
            this.label7.Text = "Fecha de Entrega :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferenciaPedido
            // 
            this.txtReferenciaPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReferenciaPedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaPedido.Decimales = 2;
            this.txtReferenciaPedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaPedido.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaPedido.Location = new System.Drawing.Point(731, 142);
            this.txtReferenciaPedido.MaxLength = 50;
            this.txtReferenciaPedido.Multiline = true;
            this.txtReferenciaPedido.Name = "txtReferenciaPedido";
            this.txtReferenciaPedido.PermitirApostrofo = false;
            this.txtReferenciaPedido.PermitirNegativos = false;
            this.txtReferenciaPedido.Size = new System.Drawing.Size(231, 20);
            this.txtReferenciaPedido.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(573, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 14);
            this.label4.TabIndex = 52;
            this.label4.Text = "Referencia de pedido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "Tipo de Pedido :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoPedido
            // 
            this.cboTipoPedido.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoPedido.Data = "";
            this.cboTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoPedido.Filtro = " 1 = 1";
            this.cboTipoPedido.FormattingEnabled = true;
            this.cboTipoPedido.ListaItemsBusqueda = 20;
            this.cboTipoPedido.Location = new System.Drawing.Point(112, 142);
            this.cboTipoPedido.MostrarToolTip = false;
            this.cboTipoPedido.Name = "cboTipoPedido";
            this.cboTipoPedido.Size = new System.Drawing.Size(371, 21);
            this.cboTipoPedido.TabIndex = 49;
            this.cboTipoPedido.SelectedIndexChanged += new System.EventHandler(this.cboTipoPedido_SelectedIndexChanged);
            this.cboTipoPedido.Validating += new System.ComponentModel.CancelEventHandler(this.cboTipoPedido_Validating);
            // 
            // lblSucursal
            // 
            this.lblSucursal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSucursal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSucursal.Location = new System.Drawing.Point(219, 68);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(740, 21);
            this.lblSucursal.TabIndex = 27;
            this.lblSucursal.Text = "Sucursal :";
            this.lblSucursal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdSucursal
            // 
            this.txtIdSucursal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdSucursal.Decimales = 2;
            this.txtIdSucursal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdSucursal.ForeColor = System.Drawing.Color.Black;
            this.txtIdSucursal.Location = new System.Drawing.Point(112, 68);
            this.txtIdSucursal.MaxLength = 4;
            this.txtIdSucursal.Name = "txtIdSucursal";
            this.txtIdSucursal.PermitirApostrofo = false;
            this.txtIdSucursal.PermitirNegativos = false;
            this.txtIdSucursal.Size = new System.Drawing.Size(100, 20);
            this.txtIdSucursal.TabIndex = 2;
            this.txtIdSucursal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdSucursal.TextChanged += new System.EventHandler(this.txtIdSucursal_TextChanged);
            this.txtIdSucursal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdSucursal_KeyDown);
            this.txtIdSucursal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdSucursal_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "Sucursal Socio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSocioComercial
            // 
            this.lblSocioComercial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSocioComercial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSocioComercial.Location = new System.Drawing.Point(219, 44);
            this.lblSocioComercial.Name = "lblSocioComercial";
            this.lblSocioComercial.Size = new System.Drawing.Size(740, 21);
            this.lblSocioComercial.TabIndex = 24;
            this.lblSocioComercial.Text = "Socio :";
            this.lblSocioComercial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdSocioComercial
            // 
            this.txtIdSocioComercial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdSocioComercial.Decimales = 2;
            this.txtIdSocioComercial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdSocioComercial.ForeColor = System.Drawing.Color.Black;
            this.txtIdSocioComercial.Location = new System.Drawing.Point(112, 44);
            this.txtIdSocioComercial.MaxLength = 4;
            this.txtIdSocioComercial.Name = "txtIdSocioComercial";
            this.txtIdSocioComercial.PermitirApostrofo = false;
            this.txtIdSocioComercial.PermitirNegativos = false;
            this.txtIdSocioComercial.Size = new System.Drawing.Size(100, 20);
            this.txtIdSocioComercial.TabIndex = 1;
            this.txtIdSocioComercial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdSocioComercial.TextChanged += new System.EventHandler(this.txtIdSocioComercial_TextChanged);
            this.txtIdSocioComercial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdSocioComercial_KeyDown);
            this.txtIdSocioComercial.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdSocioComercial_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "Socio Comercial :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(112, 94);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(850, 39);
            this.txtObservaciones.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(859, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaRegistro.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(752, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(219, 20);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 5;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(112, 20);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnImportarPedidoMasivo});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(995, 27);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(24, 24);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(24, 24);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(24, 24);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(24, 24);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnImportarPedidoMasivo
            // 
            this.btnImportarPedidoMasivo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImportarPedidoMasivo.Image = ((System.Drawing.Image)(resources.GetObject("btnImportarPedidoMasivo.Image")));
            this.btnImportarPedidoMasivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportarPedidoMasivo.Name = "btnImportarPedidoMasivo";
            this.btnImportarPedidoMasivo.Size = new System.Drawing.Size(24, 24);
            this.btnImportarPedidoMasivo.Text = "Carga masiva de pedidos";
            this.btnImportarPedidoMasivo.Visible = false;
            // 
            // frameImportacion
            // 
            this.frameImportacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameImportacion.Controls.Add(this.lblTituloHoja);
            this.frameImportacion.Controls.Add(this.cboHojas);
            this.frameImportacion.Controls.Add(this.toolStripBarraImportacion);
            this.frameImportacion.Location = new System.Drawing.Point(12, 203);
            this.frameImportacion.Name = "frameImportacion";
            this.frameImportacion.Size = new System.Drawing.Size(971, 51);
            this.frameImportacion.TabIndex = 4;
            this.frameImportacion.TabStop = false;
            this.frameImportacion.Text = "Menú de Importación";
            // 
            // lblTituloHoja
            // 
            this.lblTituloHoja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTituloHoja.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloHoja.Location = new System.Drawing.Point(490, 22);
            this.lblTituloHoja.Name = "lblTituloHoja";
            this.lblTituloHoja.Size = new System.Drawing.Size(98, 15);
            this.lblTituloHoja.TabIndex = 22;
            this.lblTituloHoja.Text = "Seleccionar Hoja :";
            this.lblTituloHoja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboHojas
            // 
            this.cboHojas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(589, 19);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(371, 21);
            this.cboHojas.TabIndex = 1;
            this.cboHojas.SelectedIndexChanged += new System.EventHandler(this.cboHojas_SelectedIndexChanged);
            // 
            // toolStripBarraImportacion
            // 
            this.toolStripBarraImportacion.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraImportacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarPlantilla,
            this.toolStripSeparator10,
            this.btnAbrir,
            this.toolStripSeparator7,
            this.btnEjecutar,
            this.toolStripSeparator8,
            this.btnGuardar_CargaMasiva,
            this.toolStripSeparator9,
            this.btnValidarDatos});
            this.toolStripBarraImportacion.Location = new System.Drawing.Point(3, 16);
            this.toolStripBarraImportacion.Name = "toolStripBarraImportacion";
            this.toolStripBarraImportacion.Size = new System.Drawing.Size(965, 27);
            this.toolStripBarraImportacion.TabIndex = 0;
            this.toolStripBarraImportacion.Text = "toolStrip1";
            // 
            // btnExportarPlantilla
            // 
            this.btnExportarPlantilla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPlantilla.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPlantilla.Image")));
            this.btnExportarPlantilla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPlantilla.Name = "btnExportarPlantilla";
            this.btnExportarPlantilla.Size = new System.Drawing.Size(24, 24);
            this.btnExportarPlantilla.Text = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.ToolTipText = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.Click += new System.EventHandler(this.btnExportarPlantilla_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 27);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(24, 24);
            this.btnAbrir.Text = "Abrir plantilla de pedido";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(24, 24);
            this.btnEjecutar.Text = "Cargar información de plantilla";
            this.btnEjecutar.ToolTipText = "Cargar información de plantilla";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar_CargaMasiva
            // 
            this.btnGuardar_CargaMasiva.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar_CargaMasiva.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar_CargaMasiva.Image")));
            this.btnGuardar_CargaMasiva.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar_CargaMasiva.Name = "btnGuardar_CargaMasiva";
            this.btnGuardar_CargaMasiva.Size = new System.Drawing.Size(24, 24);
            this.btnGuardar_CargaMasiva.Text = "Subir plantilla ";
            this.btnGuardar_CargaMasiva.Click += new System.EventHandler(this.btnGuardar_CargaMasiva_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 27);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(24, 24);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkCajasCompletas);
            this.groupBox2.Controls.Add(this.FrameProceso);
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(12, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(971, 347);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedido";
            // 
            // chkCajasCompletas
            // 
            this.chkCajasCompletas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCajasCompletas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCajasCompletas.Location = new System.Drawing.Point(820, -1);
            this.chkCajasCompletas.Name = "chkCajasCompletas";
            this.chkCajasCompletas.Size = new System.Drawing.Size(143, 17);
            this.chkCajasCompletas.TabIndex = 6;
            this.chkCajasCompletas.Text = "Pedidos cajas completas";
            this.chkCajasCompletas.UseVisualStyleBackColor = true;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.lblProcesados);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(168, 135);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(656, 52);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // lblProcesados
            // 
            this.lblProcesados.Location = new System.Drawing.Point(357, 3);
            this.lblProcesados.MostrarToolTip = false;
            this.lblProcesados.Name = "lblProcesados";
            this.lblProcesados.Size = new System.Drawing.Size(284, 14);
            this.lblProcesados.TabIndex = 15;
            this.lblProcesados.Text = "scLabelExt1";
            this.lblProcesados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProcesados.Visible = false;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(10, 22);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(631, 17);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdProductos.Location = new System.Drawing.Point(10, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(952, 324);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 8;
            this.grdProductos_Sheet1.RowCount = 12;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 5).Value = 32D;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 3D;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 10.666666666666666D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 5).Value = 76D;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = 50D;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 1.52D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.Cells.Get(11, 7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(11, 7).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdClaveSSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Presentacion";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Existencia";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad piezas";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Contenido paquete";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Cantidad cajas";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 20;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 130F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "IdClaveSSA";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 350F;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Presentacion";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 126F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Existencia";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Cantidad piezas";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 70F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Contenido paquete";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 70F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "(RC[-2]/RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Cantidad cajas";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 70F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmRegistroPedidosEspecialesVentaSocioComercial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 611);
            this.Controls.Add(this.frameImportacion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameEncabezado);
            this.Name = "FrmRegistroPedidosEspecialesVentaSocioComercial";
            this.Text = "Registro De Pedidos Especiales Venta Socio Comercial";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistroPedidosEspecialesVentaSocioComercial_Load);
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.frameImportacion.ResumeLayout(false);
            this.frameImportacion.PerformLayout();
            this.toolStripBarraImportacion.ResumeLayout(false);
            this.toolStripBarraImportacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.Label lblSucursal;
        private SC_ControlsCS.scTextBoxExt txtIdSucursal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSocioComercial;
        private SC_ControlsCS.scTextBoxExt txtIdSocioComercial;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImportarPedidoMasivo;
        private System.Windows.Forms.GroupBox frameImportacion;
        private System.Windows.Forms.Label lblTituloHoja;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.ToolStrip toolStripBarraImportacion;
        private System.Windows.Forms.ToolStripButton btnExportarPlantilla;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnGuardar_CargaMasiva;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCajasCompletas;
        private System.Windows.Forms.GroupBox FrameProceso;
        private SC_ControlsCS.scLabelExt lblProcesados;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private SC_ControlsCS.scTextBoxExt txtReferenciaPedido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboTipoPedido;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer tmValidacion;
    }
}