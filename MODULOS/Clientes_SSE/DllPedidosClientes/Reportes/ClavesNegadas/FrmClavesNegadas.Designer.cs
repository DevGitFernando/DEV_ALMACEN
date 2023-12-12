namespace DllPedidosClientes.Reportes
{
    partial class FrmClavesNegadas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClavesNegadas));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkDiaEspecificado = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lstClaves = new SC_ControlsCS.scListView();
            this.FrameResumen = new System.Windows.Forms.GroupBox();
            this.lblPiezas = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblClaves = new SC_ControlsCS.scLabelExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            this.FrameResumen.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1016, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblFarmacia);
            this.groupBox5.Controls.Add(this.txtFarmacia);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(10, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(589, 82);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(151, 48);
            this.lblFarmacia.MostrarToolTip = false;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(422, 20);
            this.lblFarmacia.TabIndex = 23;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(83, 48);
            this.txtFarmacia.MaxLength = 4;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(62, 20);
            this.txtFarmacia.TabIndex = 1;
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmacia_KeyDown);
            this.txtFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmacia_Validating);
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(83, 110);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(390, 21);
            this.cboFarmacias.TabIndex = 21;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(83, 21);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(490, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(26, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.chkDiaEspecificado);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(605, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(195, 82);
            this.FrameFechas.TabIndex = 6;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Periodo de revisión";
            // 
            // chkDiaEspecificado
            // 
            this.chkDiaEspecificado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDiaEspecificado.Location = new System.Drawing.Point(59, 46);
            this.chkDiaEspecificado.Name = "chkDiaEspecificado";
            this.chkDiaEspecificado.Size = new System.Drawing.Size(110, 19);
            this.chkDiaEspecificado.TabIndex = 3;
            this.chkDiaEspecificado.Text = "Dia seleccionado";
            this.chkDiaEspecificado.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Periodo :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(82, 25);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.lstClaves);
            this.FrameResultado.Location = new System.Drawing.Point(10, 112);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(996, 337);
            this.FrameResultado.TabIndex = 7;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Listado de Claves";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(10, 19);
            this.lstClaves.LockColumnSize = false;
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(980, 300);
            this.lstClaves.TabIndex = 2;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // FrameResumen
            // 
            this.FrameResumen.Controls.Add(this.lblPiezas);
            this.FrameResumen.Controls.Add(this.label3);
            this.FrameResumen.Controls.Add(this.lblClaves);
            this.FrameResumen.Controls.Add(this.label2);
            this.FrameResumen.Location = new System.Drawing.Point(806, 28);
            this.FrameResumen.Name = "FrameResumen";
            this.FrameResumen.Size = new System.Drawing.Size(200, 82);
            this.FrameResumen.TabIndex = 8;
            this.FrameResumen.TabStop = false;
            this.FrameResumen.Text = "Resumen";
            // 
            // lblPiezas
            // 
            this.lblPiezas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPiezas.Location = new System.Drawing.Point(82, 46);
            this.lblPiezas.MostrarToolTip = false;
            this.lblPiezas.Name = "lblPiezas";
            this.lblPiezas.Size = new System.Drawing.Size(104, 20);
            this.lblPiezas.TabIndex = 26;
            this.lblPiezas.Text = "FARMACIA";
            this.lblPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Piezas :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaves
            // 
            this.lblClaves.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaves.Location = new System.Drawing.Point(82, 21);
            this.lblClaves.MostrarToolTip = false;
            this.lblClaves.Name = "lblClaves";
            this.lblClaves.Size = new System.Drawing.Size(104, 20);
            this.lblClaves.TabIndex = 24;
            this.lblClaves.Text = "FARMACIA";
            this.lblClaves.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Claves :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmClavesNegadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 460);
            this.Controls.Add(this.FrameResumen);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmClavesNegadas";
            this.Text = "Analísis claves no surtidas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaFarmacias_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameResultado.ResumeLayout(false);
            this.FrameResumen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.CheckBox chkDiaEspecificado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameResumen;
        private SC_ControlsCS.scLabelExt lblClaves;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scLabelExt lblPiezas;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scListView lstClaves;
    }
}