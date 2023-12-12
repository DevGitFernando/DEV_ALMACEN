namespace Dll_SII_INadro.FTP
{
    partial class FrmFTP_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFTP_Manager));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFtpConectar = new System.Windows.Forms.ToolStripButton();
            this.btnFtpDesconectar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnFtpConectar,
            this.toolStripSeparator3,
            this.btnFtpDesconectar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1083, 39);
            this.toolStripBarraMenu.TabIndex = 1;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnFtpConectar
            // 
            this.btnFtpConectar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFtpConectar.Image = ((System.Drawing.Image)(resources.GetObject("btnFtpConectar.Image")));
            this.btnFtpConectar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFtpConectar.Name = "btnFtpConectar";
            this.btnFtpConectar.Size = new System.Drawing.Size(36, 36);
            this.btnFtpConectar.Text = "Conectar ftp";
            // 
            // btnFtpDesconectar
            // 
            this.btnFtpDesconectar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFtpDesconectar.Image = ((System.Drawing.Image)(resources.GetObject("btnFtpDesconectar.Image")));
            this.btnFtpDesconectar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFtpDesconectar.Name = "btnFtpDesconectar";
            this.btnFtpDesconectar.Size = new System.Drawing.Size(36, 36);
            this.btnFtpDesconectar.Text = "Desconectar ftp";
            // 
            // FrmFTP_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 499);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmFTP_Manager";
            this.Text = "FTP Manager";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFtpConectar;
        private System.Windows.Forms.ToolStripButton btnFtpDesconectar;
    }
}