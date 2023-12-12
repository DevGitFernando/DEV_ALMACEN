namespace Almacen.Pedidos
{
    partial class FrmRpt_PorcentajeSurtimientoPedidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRpt_PorcentajeSurtimientoPedidos));
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
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
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FramePedidos = new System.Windows.Forms.GroupBox();
            this.listvwPedidos = new System.Windows.Forms.ListView();
            this.colJurisdiccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmaciaSolicita = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPiezasSolicitadas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPiezasAsignadas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPorcentajeDePiezas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClavesSolicitadas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClavesAsignadas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPorcentajeDeClaves = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameStatusPedido = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scComboBoxExt1 = new SC_ControlsCS.scComboBoxExt();
            this.cboStatusPedidos = new SC_ControlsCS.scComboBoxExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtReferenciaPedido = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.scComboBoxExt2 = new SC_ControlsCS.scComboBoxExt();
            this.colTipoDePedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameUnidades.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FramePedidos.SuspendLayout();
            this.FrameStatusPedido.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUnidades.Controls.Add(this.cboFarmacias);
            this.FrameUnidades.Controls.Add(this.label2);
            this.FrameUnidades.Controls.Add(this.cboJurisdicciones);
            this.FrameUnidades.Controls.Add(this.label1);
            this.FrameUnidades.Location = new System.Drawing.Point(8, 28);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(617, 97);
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
            this.cboFarmacias.Location = new System.Drawing.Point(83, 53);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(524, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
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
            this.cboJurisdicciones.Location = new System.Drawing.Point(83, 23);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(524, 21);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 0;
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
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(1014, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 97);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(65, 51);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(65, 25);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FramePedidos
            // 
            this.FramePedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(8, 126);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Size = new System.Drawing.Size(1167, 434);
            this.FramePedidos.TabIndex = 6;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de Pedidos";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTipoDePedido,
            this.colJurisdiccion,
            this.colIdFarmacia,
            this.colFarmacia,
            this.colFarmaciaSolicita,
            this.colFolio,
            this.colPiezasSolicitadas,
            this.colPiezasAsignadas,
            this.colPorcentajeDePiezas,
            this.colClavesSolicitadas,
            this.colClavesAsignadas,
            this.colPorcentajeDeClaves,
            this.colStatus});
            this.listvwPedidos.Location = new System.Drawing.Point(10, 16);
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(1148, 408);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // colJurisdiccion
            // 
            this.colJurisdiccion.Text = "Jurisdicción";
            this.colJurisdiccion.Width = 151;
            // 
            // colIdFarmacia
            // 
            this.colIdFarmacia.Text = "Núm. Farmacia";
            this.colIdFarmacia.Width = 89;
            // 
            // colFarmacia
            // 
            this.colFarmacia.Text = "Farmacia";
            this.colFarmacia.Width = 160;
            // 
            // colFarmaciaSolicita
            // 
            this.colFarmaciaSolicita.Text = "Farmacia Solicita";
            this.colFarmaciaSolicita.Width = 160;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.Width = 83;
            // 
            // colPiezasSolicitadas
            // 
            this.colPiezasSolicitadas.Text = "Piezas Solicitadas";
            this.colPiezasSolicitadas.Width = 82;
            // 
            // colPiezasAsignadas
            // 
            this.colPiezasAsignadas.Text = "Piezas Asignadas";
            this.colPiezasAsignadas.Width = 81;
            // 
            // colPorcentajeDePiezas
            // 
            this.colPorcentajeDePiezas.Text = "Porcentaje de Piezas";
            // 
            // colClavesSolicitadas
            // 
            this.colClavesSolicitadas.Text = "Claves Solicitadas";
            this.colClavesSolicitadas.Width = 105;
            // 
            // colClavesAsignadas
            // 
            this.colClavesAsignadas.Text = "Claves Asignadas";
            // 
            // colPorcentajeDeClaves
            // 
            this.colPorcentajeDeClaves.Text = "Porcentaje de Claves";
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // FrameStatusPedido
            // 
            this.FrameStatusPedido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameStatusPedido.Controls.Add(this.groupBox1);
            this.FrameStatusPedido.Controls.Add(this.cboStatusPedidos);
            this.FrameStatusPedido.Location = new System.Drawing.Point(631, 28);
            this.FrameStatusPedido.Name = "FrameStatusPedido";
            this.FrameStatusPedido.Size = new System.Drawing.Size(377, 48);
            this.FrameStatusPedido.TabIndex = 3;
            this.FrameStatusPedido.TabStop = false;
            this.FrameStatusPedido.Text = "Status de Pedido";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scComboBoxExt1);
            this.groupBox1.Location = new System.Drawing.Point(6, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 63);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status de Pedido";
            // 
            // scComboBoxExt1
            // 
            this.scComboBoxExt1.BackColorEnabled = System.Drawing.Color.White;
            this.scComboBoxExt1.Data = "";
            this.scComboBoxExt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scComboBoxExt1.Filtro = " 1 = 1";
            this.scComboBoxExt1.FormattingEnabled = true;
            this.scComboBoxExt1.ListaItemsBusqueda = 20;
            this.scComboBoxExt1.Location = new System.Drawing.Point(9, 19);
            this.scComboBoxExt1.MostrarToolTip = false;
            this.scComboBoxExt1.Name = "scComboBoxExt1";
            this.scComboBoxExt1.Size = new System.Drawing.Size(362, 21);
            this.scComboBoxExt1.TabIndex = 0;
            // 
            // cboStatusPedidos
            // 
            this.cboStatusPedidos.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatusPedidos.Data = "";
            this.cboStatusPedidos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusPedidos.Filtro = " 1 = 1";
            this.cboStatusPedidos.FormattingEnabled = true;
            this.cboStatusPedidos.ListaItemsBusqueda = 20;
            this.cboStatusPedidos.Location = new System.Drawing.Point(9, 19);
            this.cboStatusPedidos.MostrarToolTip = false;
            this.cboStatusPedidos.Name = "cboStatusPedidos";
            this.cboStatusPedidos.Size = new System.Drawing.Size(362, 21);
            this.cboStatusPedidos.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtReferenciaPedido);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(631, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Referencia";
            // 
            // txtReferenciaPedido
            // 
            this.txtReferenciaPedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaPedido.Decimales = 2;
            this.txtReferenciaPedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaPedido.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaPedido.Location = new System.Drawing.Point(132, 14);
            this.txtReferenciaPedido.MaxLength = 50;
            this.txtReferenciaPedido.Multiline = true;
            this.txtReferenciaPedido.Name = "txtReferenciaPedido";
            this.txtReferenciaPedido.PermitirApostrofo = false;
            this.txtReferenciaPedido.PermitirNegativos = false;
            this.txtReferenciaPedido.Size = new System.Drawing.Size(239, 20);
            this.txtReferenciaPedido.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 14);
            this.label5.TabIndex = 50;
            this.label5.Text = "Referencia de pedido :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.scComboBoxExt2);
            this.groupBox4.Location = new System.Drawing.Point(6, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(377, 63);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status de Pedido";
            // 
            // scComboBoxExt2
            // 
            this.scComboBoxExt2.BackColorEnabled = System.Drawing.Color.White;
            this.scComboBoxExt2.Data = "";
            this.scComboBoxExt2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scComboBoxExt2.Filtro = " 1 = 1";
            this.scComboBoxExt2.FormattingEnabled = true;
            this.scComboBoxExt2.ListaItemsBusqueda = 20;
            this.scComboBoxExt2.Location = new System.Drawing.Point(9, 19);
            this.scComboBoxExt2.MostrarToolTip = false;
            this.scComboBoxExt2.Name = "scComboBoxExt2";
            this.scComboBoxExt2.Size = new System.Drawing.Size(362, 21);
            this.scComboBoxExt2.TabIndex = 0;
            // 
            // colTipoDePedido
            // 
            this.colTipoDePedido.Text = "Tipo de pedido";
            this.colTipoDePedido.Width = 150;
            // 
            // FrmRpt_PorcentajeSurtimientoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 566);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameStatusPedido);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmRpt_PorcentajeSurtimientoPedidos";
            this.Text = "Porcentaje de Surtimiento de Pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRpt_PorcentajeSurtimientoPedidos_Load);
            this.FrameUnidades.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.FramePedidos.ResumeLayout(false);
            this.FrameStatusPedido.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ColumnHeader colJurisdiccion;
        private System.Windows.Forms.ColumnHeader colIdFarmacia;
        private System.Windows.Forms.ColumnHeader colFarmacia;
        private System.Windows.Forms.ColumnHeader colPiezasAsignadas;
        private System.Windows.Forms.ColumnHeader colPorcentajeDePiezas;
        private System.Windows.Forms.ColumnHeader colClavesSolicitadas;
        private System.Windows.Forms.ColumnHeader colPiezasSolicitadas;
        private System.Windows.Forms.ColumnHeader colFarmaciaSolicita;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.GroupBox FrameStatusPedido;
        private SC_ControlsCS.scComboBoxExt cboStatusPedidos;
        private System.Windows.Forms.ColumnHeader colClavesAsignadas;
        private System.Windows.Forms.ColumnHeader colPorcentajeDeClaves;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt scComboBoxExt1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private SC_ControlsCS.scComboBoxExt scComboBoxExt2;
        private SC_ControlsCS.scTextBoxExt txtReferenciaPedido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader colTipoDePedido;
    }
}