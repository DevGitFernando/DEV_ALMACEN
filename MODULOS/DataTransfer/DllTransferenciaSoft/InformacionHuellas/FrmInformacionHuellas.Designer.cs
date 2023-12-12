namespace DllTransferenciaSoft.InformacionHuellas
{
    partial class FrmInformacionHuellas
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnDescargaInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegraInfo = new System.Windows.Forms.ToolStripButton();
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
            this.btnDescargaInfo,
            this.toolStripSeparator1,
            this.btnIntegraInfo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(347, 64);
            this.toolStrip.TabIndex = 29;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnDescargaInfo
            // 
            this.btnDescargaInfo.AutoSize = false;
            this.btnDescargaInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDescargaInfo.Image = global::DllTransferenciaSoft.Properties.Resources.server_information;
            this.btnDescargaInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargaInfo.Name = "btnDescargaInfo";
            this.btnDescargaInfo.Size = new System.Drawing.Size(66, 61);
            this.btnDescargaInfo.Text = "Descargar Información";
            this.btnDescargaInfo.Click += new System.EventHandler(this.btnDescargaInfo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // btnIntegraInfo
            // 
            this.btnIntegraInfo.AutoSize = false;
            this.btnIntegraInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegraInfo.Image = global::DllTransferenciaSoft.Properties.Resources.server_components;
            this.btnIntegraInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegraInfo.Name = "btnIntegraInfo";
            this.btnIntegraInfo.Size = new System.Drawing.Size(66, 61);
            this.btnIntegraInfo.Text = "Integrar Información Huellas";
            this.btnIntegraInfo.Click += new System.EventHandler(this.btnIntegraInfo_Click);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(12, 90);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(321, 88);
            this.FrameProceso.TabIndex = 30;
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
            // FrmInformacionHuellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 262);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmInformacionHuellas";
            this.Text = "Informacion Huellas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacionHuellas_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnDescargaInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnIntegraInfo;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}