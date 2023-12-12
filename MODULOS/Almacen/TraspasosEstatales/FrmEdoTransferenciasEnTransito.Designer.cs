namespace Almacen.Transferencias
{
    partial class FrmEdoTransferenciasEnTransito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEdoTransferenciasEnTransito));
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstFoliosTransf = new System.Windows.Forms.ListView();
            this.colFolioTransf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNumFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFarmacia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoTodas = new System.Windows.Forms.RadioButton();
            this.rdoNoRecepcionadas = new System.Windows.Forms.RadioButton();
            this.rdoRecepcionadas = new System.Windows.Forms.RadioButton();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.menuTransf = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAplicarTransf = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAplicarTransfMasivo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator_02 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator_03 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStatus_Integrada = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStatus_Integrada_Masivo = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameFolios.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.menuTransf.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.lstFoliosTransf);
            this.FrameFolios.Location = new System.Drawing.Point(8, 128);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(800, 362);
            this.FrameFolios.TabIndex = 2;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Listado de Folios de Transferencias";
            // 
            // lstFoliosTransf
            // 
            this.lstFoliosTransf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFoliosTransf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFolioTransf,
            this.colFecha,
            this.colNumFarmacia,
            this.colFarmacia});
            this.lstFoliosTransf.ContextMenuStrip = this.menuTransf;
            this.lstFoliosTransf.Location = new System.Drawing.Point(10, 19);
            this.lstFoliosTransf.Name = "lstFoliosTransf";
            this.lstFoliosTransf.Size = new System.Drawing.Size(780, 333);
            this.lstFoliosTransf.TabIndex = 0;
            this.lstFoliosTransf.UseCompatibleStateImageBehavior = false;
            this.lstFoliosTransf.View = System.Windows.Forms.View.Details;
            // 
            // colFolioTransf
            // 
            this.colFolioTransf.Text = "Folio Transferencia";
            this.colFolioTransf.Width = 111;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha Registro";
            this.colFecha.Width = 84;
            // 
            // colNumFarmacia
            // 
            this.colNumFarmacia.Text = "Núm. Farmacia Destino";
            this.colNumFarmacia.Width = 99;
            // 
            // colFarmacia
            // 
            this.colFarmacia.Text = "Farmacia Destino";
            this.colFarmacia.Width = 150;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(817, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoTodas);
            this.groupBox1.Controls.Add(this.rdoNoRecepcionadas);
            this.groupBox1.Controls.Add(this.rdoRecepcionadas);
            this.groupBox1.Location = new System.Drawing.Point(8, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Status de Transferencias";
            // 
            // rdoTodas
            // 
            this.rdoTodas.Checked = true;
            this.rdoTodas.Location = new System.Drawing.Point(521, 18);
            this.rdoTodas.Name = "rdoTodas";
            this.rdoTodas.Size = new System.Drawing.Size(209, 24);
            this.rdoTodas.TabIndex = 2;
            this.rdoTodas.TabStop = true;
            this.rdoTodas.Text = "Recepcionadas y No Recepcionadas";
            this.rdoTodas.UseVisualStyleBackColor = true;
            // 
            // rdoNoRecepcionadas
            // 
            this.rdoNoRecepcionadas.Location = new System.Drawing.Point(296, 18);
            this.rdoNoRecepcionadas.Name = "rdoNoRecepcionadas";
            this.rdoNoRecepcionadas.Size = new System.Drawing.Size(209, 24);
            this.rdoNoRecepcionadas.TabIndex = 1;
            this.rdoNoRecepcionadas.Text = "No Recepcionadas";
            this.rdoNoRecepcionadas.UseVisualStyleBackColor = true;
            // 
            // rdoRecepcionadas
            // 
            this.rdoRecepcionadas.Location = new System.Drawing.Point(71, 18);
            this.rdoRecepcionadas.Name = "rdoRecepcionadas";
            this.rdoRecepcionadas.Size = new System.Drawing.Size(209, 24);
            this.rdoRecepcionadas.TabIndex = 0;
            this.rdoRecepcionadas.Text = "Recepcionadas";
            this.rdoRecepcionadas.UseVisualStyleBackColor = true;
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.cboEstados);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Controls.Add(this.cboFarmacias);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Location = new System.Drawing.Point(8, 26);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(800, 48);
            this.FrameEncabezado.TabIndex = 3;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(95, 16);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(298, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Estado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(480, 16);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(298, 21);
            this.cboFarmacias.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(423, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Almacén :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuTransf
            // 
            this.menuTransf.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAplicarTransf,
            this.toolStripSeparator_01,
            this.btnAplicarTransfMasivo,
            this.toolStripSeparator_02,
            this.toolStripSeparator_03,
            this.btnStatus_Integrada,
            this.btnStatus_Integrada_Masivo});
            this.menuTransf.Name = "menuPedidos";
            this.menuTransf.Size = new System.Drawing.Size(229, 110);
            // 
            // btnAplicarTransf
            // 
            this.btnAplicarTransf.Name = "btnAplicarTransf";
            this.btnAplicarTransf.Size = new System.Drawing.Size(228, 22);
            this.btnAplicarTransf.Text = "Aplicar transferencia";
            this.btnAplicarTransf.Click += new System.EventHandler(this.btnAplicarTransf_Click);
            // 
            // toolStripSeparator_01
            // 
            this.toolStripSeparator_01.Name = "toolStripSeparator_01";
            this.toolStripSeparator_01.Size = new System.Drawing.Size(225, 6);
            // 
            // btnAplicarTransfMasivo
            // 
            this.btnAplicarTransfMasivo.Name = "btnAplicarTransfMasivo";
            this.btnAplicarTransfMasivo.Size = new System.Drawing.Size(228, 22);
            this.btnAplicarTransfMasivo.Text = "Aplicar transferencias masivo";
            this.btnAplicarTransfMasivo.Visible = false;
            // 
            // toolStripSeparator_02
            // 
            this.toolStripSeparator_02.Name = "toolStripSeparator_02";
            this.toolStripSeparator_02.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator_03
            // 
            this.toolStripSeparator_03.Name = "toolStripSeparator_03";
            this.toolStripSeparator_03.Size = new System.Drawing.Size(225, 6);
            // 
            // btnStatus_Integrada
            // 
            this.btnStatus_Integrada.Name = "btnStatus_Integrada";
            this.btnStatus_Integrada.Size = new System.Drawing.Size(228, 22);
            this.btnStatus_Integrada.Text = "Status integrada";
            this.btnStatus_Integrada.Click += new System.EventHandler(this.btnStatus_Integrada_Click);
            // 
            // btnStatus_Integrada_Masivo
            // 
            this.btnStatus_Integrada_Masivo.Name = "btnStatus_Integrada_Masivo";
            this.btnStatus_Integrada_Masivo.Size = new System.Drawing.Size(228, 22);
            this.btnStatus_Integrada_Masivo.Text = "Status integrada masivo";
            this.btnStatus_Integrada_Masivo.Click += new System.EventHandler(this.btnStatus_Integrada_Masivo_Click);
            // 
            // FrmEdoTransferenciasEnTransito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 498);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameFolios);
            this.Name = "FrmEdoTransferenciasEnTransito";
            this.Text = "Transferencias Inter-Estatales en Tránsito";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEdoTransferenciasEnTransito_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEdoTransferenciasEnTransito_KeyDown);
            this.FrameFolios.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrameEncabezado.ResumeLayout(false);
            this.menuTransf.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstFoliosTransf;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ColumnHeader colFolioTransf;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colNumFarmacia;
        private System.Windows.Forms.ColumnHeader colFarmacia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoTodas;
        private System.Windows.Forms.RadioButton rdoNoRecepcionadas;
        private System.Windows.Forms.RadioButton rdoRecepcionadas;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ContextMenuStrip menuTransf;
        private System.Windows.Forms.ToolStripMenuItem btnAplicarTransf;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_01;
        private System.Windows.Forms.ToolStripMenuItem btnAplicarTransfMasivo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_02;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_03;
        private System.Windows.Forms.ToolStripMenuItem btnStatus_Integrada;
        private System.Windows.Forms.ToolStripMenuItem btnStatus_Integrada_Masivo;
    }
}