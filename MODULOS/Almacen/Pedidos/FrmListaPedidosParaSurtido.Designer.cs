namespace Almacen.Pedidos
{
    partial class FrmListaPedidosParaSurtido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPedidosParaSurtido));
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.txtIdBenificiario = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.chkActualizarAlCerrarSurtimientos = new System.Windows.Forms.CheckBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.colJurisdiccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoDePedidoDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdFarmaciaSolicita = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmaciaSolicita = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColNumClaves = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColNumPiezas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaEntrega = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSurtmientos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatusPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuPedidos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSurtirPedido = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSurtirPedido_DescontarEnTransito = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.terminarPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnListadoDeSurtidos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarSalida = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirPedido = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirPedidoSurtido = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.excedentesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label11 = new System.Windows.Forms.Label();
            this.FrameStatusPedido = new System.Windows.Forms.GroupBox();
            this.cboStatusPedidos = new SC_ControlsCS.scComboBoxExt();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.chkFolios = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkFiltro_FechaEntrega = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal_Entrega = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaInicial_Entrega = new System.Windows.Forms.DateTimePicker();
            this.frameRutas = new System.Windows.Forms.GroupBox();
            this.cboRuta = new SC_ControlsCS.scComboBoxExt();
            this.FrameUnidades.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FramePedidos.SuspendLayout();
            this.menuPedidos.SuspendLayout();
            this.FrameStatusPedido.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.frameRutas.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUnidades.Controls.Add(this.lblNombre);
            this.FrameUnidades.Controls.Add(this.cboFarmacias);
            this.FrameUnidades.Controls.Add(this.txtIdBenificiario);
            this.FrameUnidades.Controls.Add(this.label13);
            this.FrameUnidades.Controls.Add(this.label2);
            this.FrameUnidades.Controls.Add(this.cboJurisdicciones);
            this.FrameUnidades.Controls.Add(this.label1);
            this.FrameUnidades.Location = new System.Drawing.Point(11, 67);
            this.FrameUnidades.Margin = new System.Windows.Forms.Padding(4);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Padding = new System.Windows.Forms.Padding(4);
            this.FrameUnidades.Size = new System.Drawing.Size(621, 130);
            this.FrameUnidades.TabIndex = 1;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Información de Unidades";
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.Location = new System.Drawing.Point(241, 94);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(365, 25);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(109, 59);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(496, 24);
            this.cboFarmacias.TabIndex = 1;
            // 
            // txtIdBenificiario
            // 
            this.txtIdBenificiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdBenificiario.Decimales = 2;
            this.txtIdBenificiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdBenificiario.ForeColor = System.Drawing.Color.Black;
            this.txtIdBenificiario.Location = new System.Drawing.Point(109, 94);
            this.txtIdBenificiario.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdBenificiario.MaxLength = 10;
            this.txtIdBenificiario.Name = "txtIdBenificiario";
            this.txtIdBenificiario.PermitirApostrofo = false;
            this.txtIdBenificiario.PermitirNegativos = false;
            this.txtIdBenificiario.Size = new System.Drawing.Size(123, 22);
            this.txtIdBenificiario.TabIndex = 2;
            this.txtIdBenificiario.Text = "0123456789";
            this.txtIdBenificiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdBenificiario.TextChanged += new System.EventHandler(this.txtIdBenificiario_TextChanged);
            this.txtIdBenificiario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdBenificiario_KeyDown);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(11, 96);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 20);
            this.label13.TabIndex = 69;
            this.label13.Text = "Beneficiario :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(109, 26);
            this.cboJurisdicciones.Margin = new System.Windows.Forms.Padding(4);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(496, 24);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1712, 58);
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
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 58);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 58);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(54, 55);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chkFechas);
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(1473, 67);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(229, 130);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de Fechas de captura";
            // 
            // chkFechas
            // 
            this.chkFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.Location = new System.Drawing.Point(29, 94);
            this.chkFechas.Margin = new System.Windows.Forms.Padding(4);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(181, 21);
            this.chkFechas.TabIndex = 2;
            this.chkFechas.Text = "Filtro por Fechas";
            this.chkFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(88, 60);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(116, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(25, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(25, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(88, 28);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(116, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.chkActualizarAlCerrarSurtimientos);
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(11, 204);
            this.FramePedidos.Margin = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Padding = new System.Windows.Forms.Padding(4);
            this.FramePedidos.Size = new System.Drawing.Size(1692, 512);
            this.FramePedidos.TabIndex = 6;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de pedidos";
            // 
            // chkActualizarAlCerrarSurtimientos
            // 
            this.chkActualizarAlCerrarSurtimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActualizarAlCerrarSurtimientos.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActualizarAlCerrarSurtimientos.Location = new System.Drawing.Point(1287, 0);
            this.chkActualizarAlCerrarSurtimientos.Margin = new System.Windows.Forms.Padding(4);
            this.chkActualizarAlCerrarSurtimientos.Name = "chkActualizarAlCerrarSurtimientos";
            this.chkActualizarAlCerrarSurtimientos.Size = new System.Drawing.Size(393, 21);
            this.chkActualizarAlCerrarSurtimientos.TabIndex = 14;
            this.chkActualizarAlCerrarSurtimientos.Text = "Actualizar pedidos al cerrar listado de surtimientos";
            this.chkActualizarAlCerrarSurtimientos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActualizarAlCerrarSurtimientos.UseVisualStyleBackColor = true;
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colJurisdiccion,
            this.colTipoPedido,
            this.colTipoDePedidoDesc,
            this.colIdFarmacia,
            this.colFarmacia,
            this.colIdFarmaciaSolicita,
            this.colFarmaciaSolicita,
            this.colFolio,
            this.ColNumClaves,
            this.ColNumPiezas,
            this.colFechaEntrega,
            this.colSurtmientos,
            this.colFecha,
            this.colStatus,
            this.colStatusPedido});
            this.listvwPedidos.ContextMenuStrip = this.menuPedidos;
            this.listvwPedidos.HideSelection = false;
            this.listvwPedidos.Location = new System.Drawing.Point(13, 28);
            this.listvwPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(1665, 471);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            this.listvwPedidos.SelectedIndexChanged += new System.EventHandler(this.listvwPedidos_SelectedIndexChanged);
            // 
            // colJurisdiccion
            // 
            this.colJurisdiccion.Text = "Jurisdicción";
            this.colJurisdiccion.Width = 151;
            // 
            // colTipoPedido
            // 
            this.colTipoPedido.Text = "ClaveTipoPedido";
            this.colTipoPedido.Width = 0;
            // 
            // colTipoDePedidoDesc
            // 
            this.colTipoDePedidoDesc.Text = "Tipo de pedido";
            this.colTipoDePedidoDesc.Width = 100;
            // 
            // colIdFarmacia
            // 
            this.colIdFarmacia.Text = "Núm. Farmacia";
            this.colIdFarmacia.Width = 70;
            // 
            // colFarmacia
            // 
            this.colFarmacia.Text = "Farmacia";
            this.colFarmacia.Width = 160;
            // 
            // colIdFarmaciaSolicita
            // 
            this.colIdFarmaciaSolicita.Text = "Núm Farmacia Solicita";
            this.colIdFarmaciaSolicita.Width = 70;
            // 
            // colFarmaciaSolicita
            // 
            this.colFarmaciaSolicita.Text = "Farmacia Solicita / Beneficiario";
            this.colFarmaciaSolicita.Width = 160;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.Width = 83;
            // 
            // ColNumClaves
            // 
            this.ColNumClaves.Text = "Núm. De Claves";
            // 
            // ColNumPiezas
            // 
            this.ColNumPiezas.Text = "Núm. de piezas";
            // 
            // colFechaEntrega
            // 
            this.colFechaEntrega.Text = "Fecha Entrega";
            this.colFechaEntrega.Width = 100;
            // 
            // colSurtmientos
            // 
            this.colSurtmientos.Text = "Surtimientos";
            this.colSurtmientos.Width = 82;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.Width = 81;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // colStatusPedido
            // 
            this.colStatusPedido.Text = "Status Pedido";
            this.colStatusPedido.Width = 105;
            // 
            // menuPedidos
            // 
            this.menuPedidos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPedidos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSurtirPedido,
            this.toolStripSeparator6,
            this.btnSurtirPedido_DescontarEnTransito,
            this.toolStripSeparator5,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.terminarPedidoToolStripMenuItem,
            this.toolStripSeparator8,
            this.btnListadoDeSurtidos,
            this.toolStripSeparator3,
            this.btnGenerarSalida,
            this.toolStripSeparator9,
            this.btnImprimirPedido,
            this.toolStripSeparator1,
            this.btnImprimirPedidoSurtido,
            this.toolStripSeparator7,
            this.excedentesToolStripMenuItem});
            this.menuPedidos.Name = "menuPedidos";
            this.menuPedidos.Size = new System.Drawing.Size(323, 268);
            this.menuPedidos.Opened += new System.EventHandler(this.menuPedidos_Opened);
            // 
            // btnSurtirPedido
            // 
            this.btnSurtirPedido.Name = "btnSurtirPedido";
            this.btnSurtirPedido.Size = new System.Drawing.Size(322, 24);
            this.btnSurtirPedido.Text = "Surtir pedido";
            this.btnSurtirPedido.Visible = false;
            this.btnSurtirPedido.Click += new System.EventHandler(this.btnSurtirPedido_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(319, 6);
            this.toolStripSeparator6.Visible = false;
            // 
            // btnSurtirPedido_DescontarEnTransito
            // 
            this.btnSurtirPedido_DescontarEnTransito.Name = "btnSurtirPedido_DescontarEnTransito";
            this.btnSurtirPedido_DescontarEnTransito.Size = new System.Drawing.Size(322, 24);
            this.btnSurtirPedido_DescontarEnTransito.Text = "Surtir pedido (Descontar transito)";
            this.btnSurtirPedido_DescontarEnTransito.Click += new System.EventHandler(this.btnSurtirPedido_DescontarEnTransito_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(319, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(322, 24);
            this.toolStripMenuItem1.Text = "Surtir pedido (Manual)";
            this.toolStripMenuItem1.Visible = false;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(319, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // terminarPedidoToolStripMenuItem
            // 
            this.terminarPedidoToolStripMenuItem.Name = "terminarPedidoToolStripMenuItem";
            this.terminarPedidoToolStripMenuItem.Size = new System.Drawing.Size(322, 24);
            this.terminarPedidoToolStripMenuItem.Text = "Terminar pedido";
            this.terminarPedidoToolStripMenuItem.Click += new System.EventHandler(this.terminarPedidoToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(319, 6);
            // 
            // btnListadoDeSurtidos
            // 
            this.btnListadoDeSurtidos.Name = "btnListadoDeSurtidos";
            this.btnListadoDeSurtidos.Size = new System.Drawing.Size(322, 24);
            this.btnListadoDeSurtidos.Text = "Lista de surtimientos";
            this.btnListadoDeSurtidos.Click += new System.EventHandler(this.btnListadoDeSurtidos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(319, 6);
            // 
            // btnGenerarSalida
            // 
            this.btnGenerarSalida.Name = "btnGenerarSalida";
            this.btnGenerarSalida.Size = new System.Drawing.Size(322, 24);
            this.btnGenerarSalida.Text = "Generar Salida. Traspaso | Dispersión";
            this.btnGenerarSalida.Click += new System.EventHandler(this.btnGenerarSalida_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(319, 6);
            // 
            // btnImprimirPedido
            // 
            this.btnImprimirPedido.Name = "btnImprimirPedido";
            this.btnImprimirPedido.Size = new System.Drawing.Size(322, 24);
            this.btnImprimirPedido.Text = "Imprimir pedido";
            this.btnImprimirPedido.Click += new System.EventHandler(this.btnImprimirPedido_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(319, 6);
            // 
            // btnImprimirPedidoSurtido
            // 
            this.btnImprimirPedidoSurtido.Name = "btnImprimirPedidoSurtido";
            this.btnImprimirPedidoSurtido.Size = new System.Drawing.Size(322, 24);
            this.btnImprimirPedidoSurtido.Text = "Imprimir pedido y surtimiento";
            this.btnImprimirPedidoSurtido.Click += new System.EventHandler(this.btnImprimirPedidoSurtido_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(319, 6);
            // 
            // excedentesToolStripMenuItem
            // 
            this.excedentesToolStripMenuItem.Name = "excedentesToolStripMenuItem";
            this.excedentesToolStripMenuItem.Size = new System.Drawing.Size(322, 24);
            this.excedentesToolStripMenuItem.Text = "Imprimir Reporte de Excedentes";
            this.excedentesToolStripMenuItem.Click += new System.EventHandler(this.excedentesToolStripMenuItem_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.SteelBlue;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(0, 724);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1712, 30);
            this.label11.TabIndex = 11;
            this.label11.Text = "                                                                                 " +
    "                                       Ver opciones  --- Click secundario ---";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameStatusPedido
            // 
            this.FrameStatusPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameStatusPedido.Controls.Add(this.cboStatusPedidos);
            this.FrameStatusPedido.Location = new System.Drawing.Point(640, 67);
            this.FrameStatusPedido.Margin = new System.Windows.Forms.Padding(4);
            this.FrameStatusPedido.Name = "FrameStatusPedido";
            this.FrameStatusPedido.Padding = new System.Windows.Forms.Padding(4);
            this.FrameStatusPedido.Size = new System.Drawing.Size(324, 64);
            this.FrameStatusPedido.TabIndex = 2;
            this.FrameStatusPedido.TabStop = false;
            this.FrameStatusPedido.Text = "Status de Pedido";
            // 
            // cboStatusPedidos
            // 
            this.cboStatusPedidos.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatusPedidos.Data = "";
            this.cboStatusPedidos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusPedidos.Filtro = " 1 = 1";
            this.cboStatusPedidos.FormattingEnabled = true;
            this.cboStatusPedidos.ListaItemsBusqueda = 20;
            this.cboStatusPedidos.Location = new System.Drawing.Point(23, 26);
            this.cboStatusPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatusPedidos.MostrarToolTip = false;
            this.cboStatusPedidos.Name = "cboStatusPedidos";
            this.cboStatusPedidos.Size = new System.Drawing.Size(276, 24);
            this.cboStatusPedidos.TabIndex = 0;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFolios.Controls.Add(this.txtFolioFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFolioInicial);
            this.FrameFolios.Controls.Add(this.label5);
            this.FrameFolios.Controls.Add(this.chkFolios);
            this.FrameFolios.Location = new System.Drawing.Point(972, 67);
            this.FrameFolios.Margin = new System.Windows.Forms.Padding(4);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Padding = new System.Windows.Forms.Padding(4);
            this.FrameFolios.Size = new System.Drawing.Size(245, 130);
            this.FrameFolios.TabIndex = 3;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(84, 60);
            this.txtFolioFinal.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(141, 22);
            this.txtFolioFinal.TabIndex = 1;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Hasta :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicial
            // 
            this.txtFolioInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicial.Decimales = 2;
            this.txtFolioInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicial.Location = new System.Drawing.Point(84, 28);
            this.txtFolioInicial.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioInicial.MaxLength = 8;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(141, 22);
            this.txtFolioInicial.TabIndex = 0;
            this.txtFolioInicial.Text = "01234567";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Desde :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFolios
            // 
            this.chkFolios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.Location = new System.Drawing.Point(21, 94);
            this.chkFolios.Margin = new System.Windows.Forms.Padding(4);
            this.chkFolios.Name = "chkFolios";
            this.chkFolios.Size = new System.Drawing.Size(205, 21);
            this.chkFolios.TabIndex = 2;
            this.chkFolios.Text = "Filtro por Folios";
            this.chkFolios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkFiltro_FechaEntrega);
            this.groupBox4.Controls.Add(this.dtpFechaFinal_Entrega);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.dtpFechaInicial_Entrega);
            this.groupBox4.Location = new System.Drawing.Point(1225, 67);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(233, 130);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rango de fechas de entrega";
            // 
            // chkFiltro_FechaEntrega
            // 
            this.chkFiltro_FechaEntrega.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_FechaEntrega.Location = new System.Drawing.Point(20, 94);
            this.chkFiltro_FechaEntrega.Margin = new System.Windows.Forms.Padding(4);
            this.chkFiltro_FechaEntrega.Name = "chkFiltro_FechaEntrega";
            this.chkFiltro_FechaEntrega.Size = new System.Drawing.Size(181, 21);
            this.chkFiltro_FechaEntrega.TabIndex = 2;
            this.chkFiltro_FechaEntrega.Text = "Filtro por Fechas";
            this.chkFiltro_FechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_FechaEntrega.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal_Entrega
            // 
            this.dtpFechaFinal_Entrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal_Entrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal_Entrega.Location = new System.Drawing.Point(72, 60);
            this.dtpFechaFinal_Entrega.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal_Entrega.Name = "dtpFechaFinal_Entrega";
            this.dtpFechaFinal_Entrega.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaFinal_Entrega.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(23, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Fin :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 28);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 25);
            this.label8.TabIndex = 11;
            this.label8.Text = "Inicio :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial_Entrega
            // 
            this.dtpFechaInicial_Entrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial_Entrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial_Entrega.Location = new System.Drawing.Point(72, 28);
            this.dtpFechaInicial_Entrega.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial_Entrega.Name = "dtpFechaInicial_Entrega";
            this.dtpFechaInicial_Entrega.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaInicial_Entrega.TabIndex = 0;
            this.dtpFechaInicial_Entrega.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // frameRutas
            // 
            this.frameRutas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frameRutas.Controls.Add(this.cboRuta);
            this.frameRutas.Location = new System.Drawing.Point(640, 133);
            this.frameRutas.Margin = new System.Windows.Forms.Padding(4);
            this.frameRutas.Name = "frameRutas";
            this.frameRutas.Padding = new System.Windows.Forms.Padding(4);
            this.frameRutas.Size = new System.Drawing.Size(324, 64);
            this.frameRutas.TabIndex = 3;
            this.frameRutas.TabStop = false;
            this.frameRutas.Text = "Ruta";
            // 
            // cboRuta
            // 
            this.cboRuta.BackColorEnabled = System.Drawing.Color.White;
            this.cboRuta.Data = "";
            this.cboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRuta.Filtro = " 1 = 1";
            this.cboRuta.FormattingEnabled = true;
            this.cboRuta.ListaItemsBusqueda = 20;
            this.cboRuta.Location = new System.Drawing.Point(23, 26);
            this.cboRuta.Margin = new System.Windows.Forms.Padding(4);
            this.cboRuta.MostrarToolTip = false;
            this.cboRuta.Name = "cboRuta";
            this.cboRuta.Size = new System.Drawing.Size(276, 24);
            this.cboRuta.TabIndex = 0;
            // 
            // FrmListaPedidosParaSurtido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1712, 754);
            this.Controls.Add(this.frameRutas);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FrameStatusPedido);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmListaPedidosParaSurtido";
            this.ShowIcon = false;
            this.Text = "Lista Pedidos para Surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaPedidosParaSurtido_Load);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameUnidades.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.FramePedidos.ResumeLayout(false);
            this.menuPedidos.ResumeLayout(false);
            this.FrameStatusPedido.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.frameRutas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView listvwPedidos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ContextMenuStrip menuPedidos;
        private System.Windows.Forms.ToolStripMenuItem btnSurtirPedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnListadoDeSurtidos;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirPedido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirPedidoSurtido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColumnHeader colJurisdiccion;
        private System.Windows.Forms.ColumnHeader colIdFarmacia;
        private System.Windows.Forms.ColumnHeader colFarmacia;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colStatusPedido;
        private System.Windows.Forms.ColumnHeader colSurtmientos;
        private System.Windows.Forms.ColumnHeader colFarmaciaSolicita;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.GroupBox FrameStatusPedido;
        private SC_ControlsCS.scComboBoxExt cboStatusPedidos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem btnSurtirPedido_DescontarEnTransito;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem excedentesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ColumnHeader colTipoDePedidoDesc;
        private System.Windows.Forms.ColumnHeader colFechaEntrega;
        private System.Windows.Forms.ColumnHeader colIdFarmaciaSolicita;
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkFolios;
        private System.Windows.Forms.CheckBox chkFechas;
        private SC_ControlsCS.scTextBoxExt txtIdBenificiario;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ToolStripMenuItem terminarPedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkFiltro_FechaEntrega;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal_Entrega;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial_Entrega;
        private System.Windows.Forms.GroupBox frameRutas;
        private SC_ControlsCS.scComboBoxExt cboRuta;
        private System.Windows.Forms.ColumnHeader ColNumClaves;
        private System.Windows.Forms.ColumnHeader ColNumPiezas;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripMenuItem btnGenerarSalida;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ColumnHeader colTipoPedido;
        private System.Windows.Forms.CheckBox chkActualizarAlCerrarSurtimientos;
    }
}