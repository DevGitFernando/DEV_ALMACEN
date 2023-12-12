namespace DllTransferenciaSoft.Procesos
{
    partial class FrmGeneraPaqueteHuellas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneraPaqueteHuellas));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnGeneraPaquete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.toolStrip.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGeneraPaquete});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(434, 80);
            this.toolStrip.TabIndex = 30;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnGeneraPaquete
            // 
            this.btnGeneraPaquete.AutoSize = false;
            this.btnGeneraPaquete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGeneraPaquete.Image = global::DllTransferenciaSoft.Properties.Resources.export_database;
            this.btnGeneraPaquete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGeneraPaquete.Name = "btnGeneraPaquete";
            this.btnGeneraPaquete.Size = new System.Drawing.Size(66, 61);
            this.btnGeneraPaquete.Text = "Genera Paquete Huellas";
            this.btnGeneraPaquete.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnGeneraPaquete.Click += new System.EventHandler(this.btnGeneraPaquete_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 61);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(53, 90);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(321, 88);
            this.FrameProceso.TabIndex = 31;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(290, 49);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // FrmGeneraPaqueteHuellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmGeneraPaqueteHuellas";
            this.Text = "Generar Información Huellas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGeneraPaqueteHuellas_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        //private System.Windows.Forms.ToolStripButton btnGeneraPaquete;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnGeneraPaquete;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}