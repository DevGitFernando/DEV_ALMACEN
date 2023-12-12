namespace DllFarmaciaSoft.ReportesOperacion
{
    partial class FrmRptOP_MovimientosPorMes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptOP_MovimientosPorMes));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.FrameTipoDeReporte = new System.Windows.Forms.GroupBox();
            this.rdoNoLicitado = new System.Windows.Forms.RadioButton();
            this.rdoLicitado = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameTipoDeReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnExportarExcel,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(468, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dtpFecha);
            this.groupBox3.Location = new System.Drawing.Point(298, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(158, 50);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Reporte";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Fecha :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(70, 19);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(69, 20);
            this.dtpFecha.TabIndex = 0;
            this.dtpFecha.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameTipoDeReporte
            // 
            this.FrameTipoDeReporte.Controls.Add(this.rdoNoLicitado);
            this.FrameTipoDeReporte.Controls.Add(this.rdoLicitado);
            this.FrameTipoDeReporte.Controls.Add(this.rdoTodos);
            this.FrameTipoDeReporte.Location = new System.Drawing.Point(12, 31);
            this.FrameTipoDeReporte.Name = "FrameTipoDeReporte";
            this.FrameTipoDeReporte.Size = new System.Drawing.Size(273, 50);
            this.FrameTipoDeReporte.TabIndex = 5;
            this.FrameTipoDeReporte.TabStop = false;
            this.FrameTipoDeReporte.Text = "Tipo de Reporte";
            // 
            // rdoNoLicitado
            // 
            this.rdoNoLicitado.Location = new System.Drawing.Point(165, 18);
            this.rdoNoLicitado.Name = "rdoNoLicitado";
            this.rdoNoLicitado.Size = new System.Drawing.Size(87, 17);
            this.rdoNoLicitado.TabIndex = 2;
            this.rdoNoLicitado.TabStop = true;
            this.rdoNoLicitado.Text = "No Licitados";
            this.rdoNoLicitado.UseVisualStyleBackColor = true;
            // 
            // rdoLicitado
            // 
            this.rdoLicitado.Location = new System.Drawing.Point(85, 19);
            this.rdoLicitado.Name = "rdoLicitado";
            this.rdoLicitado.Size = new System.Drawing.Size(69, 17);
            this.rdoLicitado.TabIndex = 1;
            this.rdoLicitado.TabStop = true;
            this.rdoLicitado.Text = "Licitados";
            this.rdoLicitado.UseVisualStyleBackColor = true;
            // 
            // rdoTodos
            // 
            this.rdoTodos.Location = new System.Drawing.Point(20, 18);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(56, 17);
            this.rdoTodos.TabIndex = 0;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todo";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // FrmRptOP_MovimientosPorMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 91);
            this.Controls.Add(this.FrameTipoDeReporte);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRptOP_MovimientosPorMes";
            this.Text = "Reporte Mensual de Movimientos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptOP_MovimientosPorMes_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.FrameTipoDeReporte.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.GroupBox FrameTipoDeReporte;
        private System.Windows.Forms.RadioButton rdoNoLicitado;
        private System.Windows.Forms.RadioButton rdoLicitado;
        private System.Windows.Forms.RadioButton rdoTodos;
    }
}