namespace DllFarmaciaSoft.ReportesOperacion
{
    partial class FrmRptOP_EntradasConsignacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptOP_EntradasConsignacion));
            this.FrameTipoDeReporte = new System.Windows.Forms.GroupBox();
            this.rdoNoLicitado = new System.Windows.Forms.RadioButton();
            this.rdoLicitado = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoDevoluciones = new System.Windows.Forms.RadioButton();
            this.rdoEntradas = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lstResultado = new System.Windows.Forms.ListView();
            this.FrameTipoDeReporte.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameTipoDeReporte
            // 
            this.FrameTipoDeReporte.Controls.Add(this.rdoNoLicitado);
            this.FrameTipoDeReporte.Controls.Add(this.rdoLicitado);
            this.FrameTipoDeReporte.Controls.Add(this.rdoTodos);
            this.FrameTipoDeReporte.Location = new System.Drawing.Point(12, 28);
            this.FrameTipoDeReporte.Name = "FrameTipoDeReporte";
            this.FrameTipoDeReporte.Size = new System.Drawing.Size(436, 50);
            this.FrameTipoDeReporte.TabIndex = 6;
            this.FrameTipoDeReporte.TabStop = false;
            this.FrameTipoDeReporte.Text = "Tipo de Clave";
            // 
            // rdoNoLicitado
            // 
            this.rdoNoLicitado.Location = new System.Drawing.Point(262, 18);
            this.rdoNoLicitado.Name = "rdoNoLicitado";
            this.rdoNoLicitado.Size = new System.Drawing.Size(87, 17);
            this.rdoNoLicitado.TabIndex = 2;
            this.rdoNoLicitado.TabStop = true;
            this.rdoNoLicitado.Text = "No Licitados";
            this.rdoNoLicitado.UseVisualStyleBackColor = true;
            // 
            // rdoLicitado
            // 
            this.rdoLicitado.Location = new System.Drawing.Point(142, 19);
            this.rdoLicitado.Name = "rdoLicitado";
            this.rdoLicitado.Size = new System.Drawing.Size(69, 17);
            this.rdoLicitado.TabIndex = 1;
            this.rdoLicitado.TabStop = true;
            this.rdoLicitado.Text = "Licitados";
            this.rdoLicitado.UseVisualStyleBackColor = true;
            // 
            // rdoTodos
            // 
            this.rdoTodos.Location = new System.Drawing.Point(37, 18);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(56, 17);
            this.rdoTodos.TabIndex = 0;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todo";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.btnExportarExcel,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Visible = false;
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoDevoluciones);
            this.groupBox1.Controls.Add(this.rdoEntradas);
            this.groupBox1.Location = new System.Drawing.Point(454, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 50);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de reporte";
            // 
            // rdoDevoluciones
            // 
            this.rdoDevoluciones.Location = new System.Drawing.Point(167, 20);
            this.rdoDevoluciones.Name = "rdoDevoluciones";
            this.rdoDevoluciones.Size = new System.Drawing.Size(90, 17);
            this.rdoDevoluciones.TabIndex = 1;
            this.rdoDevoluciones.TabStop = true;
            this.rdoDevoluciones.Text = "Devoluciones";
            this.rdoDevoluciones.UseVisualStyleBackColor = true;
            this.rdoDevoluciones.CheckedChanged += new System.EventHandler(this.rdoDevoluciones_CheckedChanged);
            // 
            // rdoEntradas
            // 
            this.rdoEntradas.Location = new System.Drawing.Point(47, 20);
            this.rdoEntradas.Name = "rdoEntradas";
            this.rdoEntradas.Size = new System.Drawing.Size(70, 17);
            this.rdoEntradas.TabIndex = 0;
            this.rdoEntradas.TabStop = true;
            this.rdoEntradas.Text = "Entradas";
            this.rdoEntradas.UseVisualStyleBackColor = true;
            this.rdoEntradas.CheckedChanged += new System.EventHandler(this.rdoEntradas_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(764, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 50);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(270, 17);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(223, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(60, 18);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(107, 18);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // lstResultado
            // 
            this.lstResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultado.Location = new System.Drawing.Point(14, 85);
            this.lstResultado.Name = "lstResultado";
            this.lstResultado.Size = new System.Drawing.Size(1159, 463);
            this.lstResultado.TabIndex = 10;
            this.lstResultado.UseCompatibleStateImageBehavior = false;
            // 
            // FrmRptOP_EntradasConsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.lstResultado);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameTipoDeReporte);
            this.Name = "FrmRptOP_EntradasConsignacion";
            this.Text = "Entradas por Consignación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptOP_EntradasConsignacion_Load);
            this.FrameTipoDeReporte.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameTipoDeReporte;
        private System.Windows.Forms.RadioButton rdoNoLicitado;
        private System.Windows.Forms.RadioButton rdoLicitado;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoDevoluciones;
        private System.Windows.Forms.RadioButton rdoEntradas;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.ListView lstResultado;
    }
}