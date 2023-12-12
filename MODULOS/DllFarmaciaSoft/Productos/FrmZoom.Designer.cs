namespace DllFarmaciaSoft.Productos
{
    partial class FrmZoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZoom));
            this.imgDisplay = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRotarIzquierda = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotarDerecha = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgDisplay
            // 
            this.imgDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgDisplay.Location = new System.Drawing.Point(12, 32);
            this.imgDisplay.Name = "imgDisplay";
            this.imgDisplay.Size = new System.Drawing.Size(452, 408);
            this.imgDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDisplay.TabIndex = 8;
            this.imgDisplay.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRotarIzquierda,
            this.toolStripSeparator3,
            this.btnRotarDerecha,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(474, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRotarIzquierda
            // 
            this.btnRotarIzquierda.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotarIzquierda.Image = ((System.Drawing.Image)(resources.GetObject("btnRotarIzquierda.Image")));
            this.btnRotarIzquierda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotarIzquierda.Name = "btnRotarIzquierda";
            this.btnRotarIzquierda.Size = new System.Drawing.Size(23, 22);
            this.btnRotarIzquierda.Text = "Rotar a la izquierda";
            this.btnRotarIzquierda.ToolTipText = "Rotar a la izquierda";
            this.btnRotarIzquierda.Click += new System.EventHandler(this.btnRotarIzquierda_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRotarDerecha
            // 
            this.btnRotarDerecha.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotarDerecha.Image = ((System.Drawing.Image)(resources.GetObject("btnRotarDerecha.Image")));
            this.btnRotarDerecha.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotarDerecha.Name = "btnRotarDerecha";
            this.btnRotarDerecha.Size = new System.Drawing.Size(23, 22);
            this.btnRotarDerecha.Text = "Rotar a la derecha";
            this.btnRotarDerecha.ToolTipText = "Rotar a la derecha";
            this.btnRotarDerecha.Click += new System.EventHandler(this.btnRotarDerecha_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 451);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.imgDisplay);
            this.Name = "FrmZoom";
            this.Text = "Zoom";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmZoom_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgDisplay)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgDisplay;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRotarIzquierda;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRotarDerecha;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}