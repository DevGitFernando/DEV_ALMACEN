namespace Farmacia.Ventas
{
    partial class FrmVisorReceta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisorReceta));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.positionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageSizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel = new System.Windows.Forms.Panel();
            this.pcImage = new SC_ControlsCS.scImageBox();
            this.cerrarStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.statusStrip);
            this.groupBox.Controls.Add(this.panel);
            this.groupBox.Location = new System.Drawing.Point(11, 8);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(1051, 705);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positionToolStripStatusLabel,
            this.imageSizeToolStripStatusLabel,
            this.zoomToolStripStatusLabel,
            this.cerrarStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(3, 677);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1045, 25);
            this.statusStrip.TabIndex = 2;
            // 
            // positionToolStripStatusLabel
            // 
            this.positionToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.positionToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.positionToolStripStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("positionToolStripStatusLabel.Image")));
            this.positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
            this.positionToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // imageSizeToolStripStatusLabel
            // 
            this.imageSizeToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.imageSizeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.imageSizeToolStripStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("imageSizeToolStripStatusLabel.Image")));
            this.imageSizeToolStripStatusLabel.Name = "imageSizeToolStripStatusLabel";
            this.imageSizeToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // zoomToolStripStatusLabel
            // 
            this.zoomToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.zoomToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.zoomToolStripStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("zoomToolStripStatusLabel.Image")));
            this.zoomToolStripStatusLabel.Name = "zoomToolStripStatusLabel";
            this.zoomToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.pcImage);
            this.panel.Location = new System.Drawing.Point(3, 18);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(624, 438);
            this.panel.TabIndex = 0;
            // 
            // pcImage
            // 
            this.pcImage.AutoScroll = true;
            this.pcImage.AutoSize = false;
            this.pcImage.GridDisplayMode = SC_ControlsCS.ImageBoxGridDisplayMode.None;
            this.pcImage.Location = new System.Drawing.Point(84, 59);
            this.pcImage.Name = "pcImage";
            this.pcImage.Size = new System.Drawing.Size(392, 260);
            this.pcImage.TabIndex = 0;
            this.pcImage.ZoomChanged += new System.EventHandler(this.pcImage_ZoomChanged);
            this.pcImage.Resize += new System.EventHandler(this.pcImage_Resize);
            // 
            // cerrarStripStatusLabel
            // 
            this.cerrarStripStatusLabel.Name = "cerrarStripStatusLabel";
            this.cerrarStripStatusLabel.Size = new System.Drawing.Size(86, 20);
            this.cerrarStripStatusLabel.Text = "[F12] Cerrar";
            // 
            // FrmVisorReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 725);
            this.Controls.Add(this.groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmVisorReceta";
            this.Text = "Visor de recetas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVisorReceta_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmVisorReceta_KeyDown);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Panel panel;
        private SC_ControlsCS.scImageBox pcImage;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel positionToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel imageSizeToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel zoomToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel cerrarStripStatusLabel;
    }
}