namespace Dll_IFacturacion.CFDI
{
    partial class FrmVisorCFDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisorCFDI));
            this.FrameNavegador = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.btnImprimirComprobante = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameNavegador.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameNavegador
            // 
            this.FrameNavegador.Controls.Add(this.groupBox1);
            this.FrameNavegador.Controls.Add(this.toolStrip5);
            this.FrameNavegador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrameNavegador.Location = new System.Drawing.Point(0, 0);
            this.FrameNavegador.Name = "FrameNavegador";
            this.FrameNavegador.Size = new System.Drawing.Size(634, 428);
            this.FrameNavegador.TabIndex = 7;
            this.FrameNavegador.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.webBrowser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 370);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 16);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(622, 351);
            this.webBrowser.TabIndex = 4;
            // 
            // toolStrip5
            // 
            this.toolStrip5.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimirComprobante,
            this.toolStripSeparator});
            this.toolStrip5.Location = new System.Drawing.Point(3, 16);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(628, 39);
            this.toolStrip5.TabIndex = 5;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimirComprobante.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirComprobante.Image")));
            this.btnImprimirComprobante.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(36, 36);
            this.btnImprimirComprobante.Text = "&Imprimir";
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // FrmVisorCFDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 428);
            this.Controls.Add(this.FrameNavegador);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "FrmVisorCFDI";
            this.Text = "FrmVisorCFDI";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FrameNavegador.ResumeLayout(false);
            this.FrameNavegador.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameNavegador;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton btnImprimirComprobante;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}