namespace DllCompras.Vales
{
    partial class FrmInformacionVales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacionVales));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameParametros = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabVales = new System.Windows.Forms.TabControl();
            this.pgEmitidos = new System.Windows.Forms.TabPage();
            this.lstValesEmitidos = new SC_ControlsCS.scListView();
            this.pgRegistrados = new System.Windows.Forms.TabPage();
            this.lstValesRegistrados = new SC_ControlsCS.scListView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameParametros.SuspendLayout();
            this.tabVales.SuspendLayout();
            this.pgEmitidos.SuspendLayout();
            this.pgRegistrados.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(962, 25);
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
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
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
            // FrameParametros
            // 
            this.FrameParametros.Controls.Add(this.label3);
            this.FrameParametros.Controls.Add(this.cboEmpresas);
            this.FrameParametros.Controls.Add(this.cboEdo);
            this.FrameParametros.Controls.Add(this.label4);
            this.FrameParametros.Controls.Add(this.dtpFecha);
            this.FrameParametros.Controls.Add(this.label1);
            this.FrameParametros.Location = new System.Drawing.Point(12, 26);
            this.FrameParametros.Name = "FrameParametros";
            this.FrameParametros.Size = new System.Drawing.Size(937, 62);
            this.FrameParametros.TabIndex = 1;
            this.FrameParametros.TabStop = false;
            this.FrameParametros.Text = "Parámetros";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 32;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(76, 22);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(350, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(501, 22);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(268, 21);
            this.cboEdo.TabIndex = 1;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEdo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(451, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(835, 22);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(79, 20);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(779, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Período :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabVales
            // 
            this.tabVales.Controls.Add(this.pgEmitidos);
            this.tabVales.Controls.Add(this.pgRegistrados);
            this.tabVales.Location = new System.Drawing.Point(13, 96);
            this.tabVales.Name = "tabVales";
            this.tabVales.SelectedIndex = 0;
            this.tabVales.Size = new System.Drawing.Size(937, 386);
            this.tabVales.TabIndex = 2;
            // 
            // pgEmitidos
            // 
            this.pgEmitidos.Controls.Add(this.lstValesEmitidos);
            this.pgEmitidos.Location = new System.Drawing.Point(4, 22);
            this.pgEmitidos.Name = "pgEmitidos";
            this.pgEmitidos.Padding = new System.Windows.Forms.Padding(3);
            this.pgEmitidos.Size = new System.Drawing.Size(929, 360);
            this.pgEmitidos.TabIndex = 0;
            this.pgEmitidos.Text = "Vales Emitidos";
            this.pgEmitidos.UseVisualStyleBackColor = true;
            // 
            // lstValesEmitidos
            // 
            this.lstValesEmitidos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstValesEmitidos.Location = new System.Drawing.Point(6, 7);
            this.lstValesEmitidos.LockColumnSize = false;
            this.lstValesEmitidos.Name = "lstValesEmitidos";
            this.lstValesEmitidos.ShowItemToolTips = true;
            this.lstValesEmitidos.Size = new System.Drawing.Size(917, 347);
            this.lstValesEmitidos.TabIndex = 0;
            this.lstValesEmitidos.UseCompatibleStateImageBehavior = false;
            // 
            // pgRegistrados
            // 
            this.pgRegistrados.Controls.Add(this.lstValesRegistrados);
            this.pgRegistrados.Location = new System.Drawing.Point(4, 22);
            this.pgRegistrados.Name = "pgRegistrados";
            this.pgRegistrados.Padding = new System.Windows.Forms.Padding(3);
            this.pgRegistrados.Size = new System.Drawing.Size(929, 360);
            this.pgRegistrados.TabIndex = 1;
            this.pgRegistrados.Text = "Vales Registrados";
            this.pgRegistrados.UseVisualStyleBackColor = true;
            // 
            // lstValesRegistrados
            // 
            this.lstValesRegistrados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstValesRegistrados.Location = new System.Drawing.Point(6, 10);
            this.lstValesRegistrados.LockColumnSize = false;
            this.lstValesRegistrados.Name = "lstValesRegistrados";
            this.lstValesRegistrados.ShowItemToolTips = true;
            this.lstValesRegistrados.Size = new System.Drawing.Size(917, 344);
            this.lstValesRegistrados.TabIndex = 0;
            this.lstValesRegistrados.UseCompatibleStateImageBehavior = false;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmInformacionVales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 494);
            this.Controls.Add(this.tabVales);
            this.Controls.Add(this.FrameParametros);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInformacionVales";
            this.Text = "Reporte de Vales Emitidos y Registrados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacionVales_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameParametros.ResumeLayout(false);
            this.tabVales.ResumeLayout(false);
            this.pgEmitidos.ResumeLayout(false);
            this.pgRegistrados.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox FrameParametros;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabVales;
        private System.Windows.Forms.TabPage pgEmitidos;
        private SC_ControlsCS.scListView lstValesEmitidos;
        private System.Windows.Forms.TabPage pgRegistrados;
        private SC_ControlsCS.scListView lstValesRegistrados;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}