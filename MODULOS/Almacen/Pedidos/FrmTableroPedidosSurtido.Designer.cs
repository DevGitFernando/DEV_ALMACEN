namespace Almacen.Pedidos
{
    partial class FrmTableroPedidosSurtido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTableroPedidosSurtido));
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
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboStatusPed = new SC_ControlsCS.scComboBoxExt();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkFiltro_FechaEntrega = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal_Entrega = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaInicial_Entrega = new System.Windows.Forms.DateTimePicker();
            this.frameRutas = new System.Windows.Forms.GroupBox();
            this.cboRuta = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FramePedidos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.frameRutas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1579, 27);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(29, 24);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(29, 24);
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
            this.groupBox3.Location = new System.Drawing.Point(993, 32);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(233, 118);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de fechas de proceso";
            // 
            // chkFechas
            // 
            this.chkFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.Location = new System.Drawing.Point(20, 89);
            this.chkFechas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(181, 21);
            this.chkFechas.TabIndex = 13;
            this.chkFechas.Text = "Filtro por Fechas";
            this.chkFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(72, 57);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 23);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(72, 23);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUnidades.Controls.Add(this.cboFarmacias);
            this.FrameUnidades.Controls.Add(this.label2);
            this.FrameUnidades.Controls.Add(this.cboJurisdicciones);
            this.FrameUnidades.Controls.Add(this.label1);
            this.FrameUnidades.Location = new System.Drawing.Point(15, 32);
            this.FrameUnidades.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUnidades.Size = new System.Drawing.Size(728, 118);
            this.FrameUnidades.TabIndex = 1;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Información de Unidades";
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
            this.cboFarmacias.Location = new System.Drawing.Point(105, 57);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(604, 24);
            this.cboFarmacias.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 62);
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
            this.cboJurisdicciones.Location = new System.Drawing.Point(105, 23);
            this.cboJurisdicciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(604, 24);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(15, 153);
            this.FramePedidos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePedidos.Size = new System.Drawing.Size(1555, 530);
            this.FramePedidos.TabIndex = 5;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de Pedidos";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.FullRowSelect = true;
            this.listvwPedidos.HideSelection = false;
            this.listvwPedidos.Location = new System.Drawing.Point(13, 20);
            this.listvwPedidos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listvwPedidos.MultiSelect = false;
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(1528, 496);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboStatusPed);
            this.groupBox1.Location = new System.Drawing.Point(1236, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(333, 58);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status de Pedidos";
            // 
            // cboStatusPed
            // 
            this.cboStatusPed.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatusPed.Data = "";
            this.cboStatusPed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusPed.Filtro = " 1 = 1";
            this.cboStatusPed.FormattingEnabled = true;
            this.cboStatusPed.ListaItemsBusqueda = 20;
            this.cboStatusPed.Location = new System.Drawing.Point(15, 22);
            this.cboStatusPed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboStatusPed.MostrarToolTip = false;
            this.cboStatusPed.Name = "cboStatusPed";
            this.cboStatusPed.Size = new System.Drawing.Size(305, 24);
            this.cboStatusPed.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkFiltro_FechaEntrega);
            this.groupBox4.Controls.Add(this.dtpFechaFinal_Entrega);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.dtpFechaInicial_Entrega);
            this.groupBox4.Location = new System.Drawing.Point(752, 32);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(233, 118);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rango de fechas de entrega";
            // 
            // chkFiltro_FechaEntrega
            // 
            this.chkFiltro_FechaEntrega.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_FechaEntrega.Location = new System.Drawing.Point(20, 89);
            this.chkFiltro_FechaEntrega.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.dtpFechaFinal_Entrega.Location = new System.Drawing.Point(72, 57);
            this.dtpFechaFinal_Entrega.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal_Entrega.Name = "dtpFechaFinal_Entrega";
            this.dtpFechaFinal_Entrega.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaFinal_Entrega.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(23, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Fin :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 25);
            this.label6.TabIndex = 11;
            this.label6.Text = "Inicio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial_Entrega
            // 
            this.dtpFechaInicial_Entrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial_Entrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial_Entrega.Location = new System.Drawing.Point(72, 23);
            this.dtpFechaInicial_Entrega.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial_Entrega.Name = "dtpFechaInicial_Entrega";
            this.dtpFechaInicial_Entrega.Size = new System.Drawing.Size(128, 22);
            this.dtpFechaInicial_Entrega.TabIndex = 0;
            this.dtpFechaInicial_Entrega.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // frameRutas
            // 
            this.frameRutas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frameRutas.Controls.Add(this.cboRuta);
            this.frameRutas.Location = new System.Drawing.Point(1236, 92);
            this.frameRutas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frameRutas.Name = "frameRutas";
            this.frameRutas.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frameRutas.Size = new System.Drawing.Size(333, 58);
            this.frameRutas.TabIndex = 6;
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
            this.cboRuta.Location = new System.Drawing.Point(15, 21);
            this.cboRuta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboRuta.MostrarToolTip = false;
            this.cboRuta.Name = "cboRuta";
            this.cboRuta.Size = new System.Drawing.Size(305, 24);
            this.cboRuta.TabIndex = 0;
            // 
            // FrmTableroPedidosSurtido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 690);
            this.Controls.Add(this.frameRutas);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmTableroPedidosSurtido";
            this.ShowIcon = false;
            this.Text = "Tablero de Control de Pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTableroPedidosSurtido_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FramePedidos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.frameRutas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FramePedidos;
        private System.Windows.Forms.ListView listvwPedidos;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboStatusPed;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkFiltro_FechaEntrega;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal_Entrega;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial_Entrega;
        private System.Windows.Forms.GroupBox frameRutas;
        private SC_ControlsCS.scComboBoxExt cboRuta;
    }
}