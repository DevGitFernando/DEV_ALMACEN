namespace DllPedidosClientes.Reportes.Listados
{
    partial class FrmProductosSS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductosSS));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoRptMaterial = new System.Windows.Forms.RadioButton();
            this.rdoRptMedicamento = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(389, 25);
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoRptMaterial);
            this.FrameDispensacion.Controls.Add(this.rdoRptMedicamento);
            this.FrameDispensacion.Location = new System.Drawing.Point(9, 32);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(370, 44);
            this.FrameDispensacion.TabIndex = 19;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Reporte";
            // 
            // rdoRptMaterial
            // 
            this.rdoRptMaterial.Location = new System.Drawing.Point(198, 19);
            this.rdoRptMaterial.Name = "rdoRptMaterial";
            this.rdoRptMaterial.Size = new System.Drawing.Size(130, 17);
            this.rdoRptMaterial.TabIndex = 2;
            this.rdoRptMaterial.TabStop = true;
            this.rdoRptMaterial.Text = "Material De Curación";
            this.rdoRptMaterial.UseVisualStyleBackColor = true;
            // 
            // rdoRptMedicamento
            // 
            this.rdoRptMedicamento.Location = new System.Drawing.Point(43, 19);
            this.rdoRptMedicamento.Name = "rdoRptMedicamento";
            this.rdoRptMedicamento.Size = new System.Drawing.Size(94, 15);
            this.rdoRptMedicamento.TabIndex = 1;
            this.rdoRptMedicamento.TabStop = true;
            this.rdoRptMedicamento.Text = "Medicamento";
            this.rdoRptMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrmProductosSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 85);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProductosSS";
            this.Text = "Productos Sector Salud";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProductosSS_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDispensacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoRptMaterial;
        private System.Windows.Forms.RadioButton rdoRptMedicamento;
    }
}