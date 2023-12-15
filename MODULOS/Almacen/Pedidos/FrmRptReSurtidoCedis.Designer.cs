namespace Almacen.Pedidos
{
    partial class FrmRptReSurtidoCedis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptReSurtidoCedis));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameMesesRevision = new System.Windows.Forms.GroupBox();
            this.lblMesesRev = new System.Windows.Forms.Label();
            this.nmMesesCad = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameMesesRevision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCad)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(380, 27);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(29, 24);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameMesesRevision
            // 
            this.FrameMesesRevision.Controls.Add(this.lblMesesRev);
            this.FrameMesesRevision.Controls.Add(this.nmMesesCad);
            this.FrameMesesRevision.Location = new System.Drawing.Point(13, 28);
            this.FrameMesesRevision.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameMesesRevision.Name = "FrameMesesRevision";
            this.FrameMesesRevision.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameMesesRevision.Size = new System.Drawing.Size(351, 65);
            this.FrameMesesRevision.TabIndex = 12;
            this.FrameMesesRevision.TabStop = false;
            // 
            // lblMesesRev
            // 
            this.lblMesesRev.Location = new System.Drawing.Point(43, 25);
            this.lblMesesRev.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesesRev.Name = "lblMesesRev";
            this.lblMesesRev.Size = new System.Drawing.Size(172, 22);
            this.lblMesesRev.TabIndex = 4;
            this.lblMesesRev.Text = "Caducidad en meses <=";
            this.lblMesesRev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMesesCad
            // 
            this.nmMesesCad.Location = new System.Drawing.Point(223, 25);
            this.nmMesesCad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nmMesesCad.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.nmMesesCad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesCad.Name = "nmMesesCad";
            this.nmMesesCad.Size = new System.Drawing.Size(85, 22);
            this.nmMesesCad.TabIndex = 3;
            this.nmMesesCad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesCad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrmRptReSurtidoCedis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 105);
            this.Controls.Add(this.FrameMesesRevision);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmRptReSurtidoCedis";
            this.ShowIcon = false;
            this.Text = "Reporte para resurtido de ubicaciones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptReSurtidoCedis_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameMesesRevision.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameMesesRevision;
        private System.Windows.Forms.Label lblMesesRev;
        private System.Windows.Forms.NumericUpDown nmMesesCad;
    }
}