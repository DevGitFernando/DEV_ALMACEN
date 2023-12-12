namespace Almacen.Pedidos
{
    partial class FrmRtp_AtencionesSurtidores_Choferes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRtp_AtencionesSurtidores_Choferes));
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
            this.colIdSurtidor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSurtidor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colJurisdiccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmaciaSolicita = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSurtmientos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatusPedido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameStatusPedido = new System.Windows.Forms.GroupBox();
            this.cboStatusPedidos = new SC_ControlsCS.scComboBoxExt();
            this.FrameSuritidores_Choferes = new System.Windows.Forms.GroupBox();
            this.rdoChoferes = new System.Windows.Forms.RadioButton();
            this.rdoSurtidor = new System.Windows.Forms.RadioButton();
            this.cboSurtidor = new SC_ControlsCS.scComboBoxExt();
            this.FrameUnidades.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FramePedidos.SuspendLayout();
            this.FrameStatusPedido.SuspendLayout();
            this.FrameSuritidores_Choferes.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.cboFarmacias);
            this.FrameUnidades.Controls.Add(this.label2);
            this.FrameUnidades.Controls.Add(this.cboJurisdicciones);
            this.FrameUnidades.Controls.Add(this.label1);
            this.FrameUnidades.Location = new System.Drawing.Point(8, 28);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(388, 77);
            this.FrameUnidades.TabIndex = 1;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Información de Unidades";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(82, 46);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(292, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(82, 20);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(292, 21);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 24);
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1087, 25);
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
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(925, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 77);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(63, 41);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 17);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(63, 17);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(79, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FramePedidos
            // 
            this.FramePedidos.Controls.Add(this.listvwPedidos);
            this.FramePedidos.Location = new System.Drawing.Point(8, 105);
            this.FramePedidos.Name = "FramePedidos";
            this.FramePedidos.Size = new System.Drawing.Size(1072, 417);
            this.FramePedidos.TabIndex = 6;
            this.FramePedidos.TabStop = false;
            this.FramePedidos.Text = "Listado de pedidos";
            // 
            // listvwPedidos
            // 
            this.listvwPedidos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdSurtidor,
            this.colSurtidor,
            this.colJurisdiccion,
            this.colIdFarmacia,
            this.colFarmacia,
            this.colFarmaciaSolicita,
            this.colFolio,
            this.colSurtmientos,
            this.colFecha,
            this.colStatus,
            this.colStatusPedido});
            this.listvwPedidos.Location = new System.Drawing.Point(10, 16);
            this.listvwPedidos.Name = "listvwPedidos";
            this.listvwPedidos.Size = new System.Drawing.Size(1053, 391);
            this.listvwPedidos.TabIndex = 0;
            this.listvwPedidos.UseCompatibleStateImageBehavior = false;
            this.listvwPedidos.View = System.Windows.Forms.View.Details;
            // 
            // colIdSurtidor
            // 
            this.colIdSurtidor.Text = "Núm. Personal";
            this.colIdSurtidor.Width = 70;
            // 
            // colSurtidor
            // 
            this.colSurtidor.Text = "Personal";
            this.colSurtidor.Width = 160;
            // 
            // colJurisdiccion
            // 
            this.colJurisdiccion.Text = "Jurisdicción";
            this.colJurisdiccion.Width = 151;
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
            // colFarmaciaSolicita
            // 
            this.colFarmaciaSolicita.Text = "Farmacia solicita";
            this.colFarmaciaSolicita.Width = 160;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.Width = 83;
            // 
            // colSurtmientos
            // 
            this.colSurtmientos.Text = "Surtmientos";
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
            this.colStatusPedido.Text = "Status pedido";
            this.colStatusPedido.Width = 105;
            // 
            // FrameStatusPedido
            // 
            this.FrameStatusPedido.Controls.Add(this.cboStatusPedidos);
            this.FrameStatusPedido.Location = new System.Drawing.Point(663, 28);
            this.FrameStatusPedido.Name = "FrameStatusPedido";
            this.FrameStatusPedido.Size = new System.Drawing.Size(256, 77);
            this.FrameStatusPedido.TabIndex = 3;
            this.FrameStatusPedido.TabStop = false;
            this.FrameStatusPedido.Text = "Status de Surtimiento";
            // 
            // cboStatusPedidos
            // 
            this.cboStatusPedidos.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatusPedidos.Data = "";
            this.cboStatusPedidos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusPedidos.Filtro = " 1 = 1";
            this.cboStatusPedidos.FormattingEnabled = true;
            this.cboStatusPedidos.ListaItemsBusqueda = 20;
            this.cboStatusPedidos.Location = new System.Drawing.Point(14, 40);
            this.cboStatusPedidos.MostrarToolTip = false;
            this.cboStatusPedidos.Name = "cboStatusPedidos";
            this.cboStatusPedidos.Size = new System.Drawing.Size(236, 21);
            this.cboStatusPedidos.TabIndex = 0;
            // 
            // FrameSuritidores_Choferes
            // 
            this.FrameSuritidores_Choferes.Controls.Add(this.rdoChoferes);
            this.FrameSuritidores_Choferes.Controls.Add(this.rdoSurtidor);
            this.FrameSuritidores_Choferes.Controls.Add(this.cboSurtidor);
            this.FrameSuritidores_Choferes.Location = new System.Drawing.Point(401, 28);
            this.FrameSuritidores_Choferes.Name = "FrameSuritidores_Choferes";
            this.FrameSuritidores_Choferes.Size = new System.Drawing.Size(256, 77);
            this.FrameSuritidores_Choferes.TabIndex = 4;
            this.FrameSuritidores_Choferes.TabStop = false;
            this.FrameSuritidores_Choferes.Text = "Personal";
            // 
            // rdoChoferes
            // 
            this.rdoChoferes.Location = new System.Drawing.Point(137, 17);
            this.rdoChoferes.Name = "rdoChoferes";
            this.rdoChoferes.Size = new System.Drawing.Size(74, 15);
            this.rdoChoferes.TabIndex = 2;
            this.rdoChoferes.TabStop = true;
            this.rdoChoferes.Text = "Choferes";
            this.rdoChoferes.UseVisualStyleBackColor = true;
            this.rdoChoferes.CheckedChanged += new System.EventHandler(this.rdoChoferes_CheckedChanged);
            // 
            // rdoSurtidor
            // 
            this.rdoSurtidor.Location = new System.Drawing.Point(45, 17);
            this.rdoSurtidor.Name = "rdoSurtidor";
            this.rdoSurtidor.Size = new System.Drawing.Size(74, 15);
            this.rdoSurtidor.TabIndex = 1;
            this.rdoSurtidor.TabStop = true;
            this.rdoSurtidor.Text = "Surtidores";
            this.rdoSurtidor.UseVisualStyleBackColor = true;
            this.rdoSurtidor.CheckedChanged += new System.EventHandler(this.rdoSurtidor_CheckedChanged);
            // 
            // cboSurtidor
            // 
            this.cboSurtidor.BackColorEnabled = System.Drawing.Color.White;
            this.cboSurtidor.Data = "";
            this.cboSurtidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSurtidor.Filtro = " 1 = 1";
            this.cboSurtidor.FormattingEnabled = true;
            this.cboSurtidor.ListaItemsBusqueda = 20;
            this.cboSurtidor.Location = new System.Drawing.Point(11, 40);
            this.cboSurtidor.MostrarToolTip = false;
            this.cboSurtidor.Name = "cboSurtidor";
            this.cboSurtidor.Size = new System.Drawing.Size(236, 21);
            this.cboSurtidor.TabIndex = 0;
            // 
            // FrmRtp_AtencionesSurtidores_Choferes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 533);
            this.Controls.Add(this.FrameSuritidores_Choferes);
            this.Controls.Add(this.FrameStatusPedido);
            this.Controls.Add(this.FramePedidos);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmRtp_AtencionesSurtidores_Choferes";
            this.Text = "Reporte de atenciones por Surtidores y Choferes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRtp_AtencionesSurtidores_Choferes_Load);
            this.FrameUnidades.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.FramePedidos.ResumeLayout(false);
            this.FrameStatusPedido.ResumeLayout(false);
            this.FrameSuritidores_Choferes.ResumeLayout(false);
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
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colStatusPedido;
        private System.Windows.Forms.ColumnHeader colSurtmientos;
        private System.Windows.Forms.ColumnHeader colFarmaciaSolicita;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.GroupBox FrameStatusPedido;
        private SC_ControlsCS.scComboBoxExt cboStatusPedidos;
        private System.Windows.Forms.GroupBox FrameSuritidores_Choferes;
        private SC_ControlsCS.scComboBoxExt cboSurtidor;
        private System.Windows.Forms.ColumnHeader colIdSurtidor;
        private System.Windows.Forms.ColumnHeader colSurtidor;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.RadioButton rdoChoferes;
        private System.Windows.Forms.RadioButton rdoSurtidor;
    }
}