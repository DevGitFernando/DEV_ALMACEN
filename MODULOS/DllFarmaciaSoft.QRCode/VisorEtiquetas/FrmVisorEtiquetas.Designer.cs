namespace DllFarmaciaSoft.QRCode.VisorEtiquetas
{
    partial class FrmVisorEtiquetas
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
            this.Frame = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboDpi = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportToPdf = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.btnToImagePng = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageJpeg = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageTiff = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageGif = new System.Windows.Forms.ToolStripMenuItem();
            this.btnToImageBmp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnXmlTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.imageViewer = new DllFarmaciaSoft.QRCode.VisorEtiquetas.ImageViewer();
            this.Frame.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame.Controls.Add(this.imageViewer);
            this.Frame.Location = new System.Drawing.Point(12, 60);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(1218, 582);
            this.Frame.TabIndex = 1;
            this.Frame.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboDpi,
            this.toolStripSeparator4,
            this.btnRefresh,
            this.toolStripSeparator2,
            this.btnPrint,
            this.toolStripSeparator1,
            this.btnExportToPdf,
            this.toolStripSplitButton1,
            this.toolStripSeparator3,
            this.btnXmlTemplate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1238, 57);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 54);
            this.toolStripLabel1.Text = "Vista DPI";
            // 
            // cboDpi
            // 
            this.cboDpi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDpi.DropDownWidth = 75;
            this.cboDpi.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDpi.Items.AddRange(new object[] {
            "Pantalla",
            "203",
            "300",
            "600"});
            this.cboDpi.Name = "cboDpi";
            this.cboDpi.Size = new System.Drawing.Size(99, 57);
            this.cboDpi.Click += new System.EventHandler(this.cboDpi_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::DllFarmaciaSoft.QRCode.Properties.Resources.ThermalPrinterIcon32x32;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(66, 54);
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            // 
            // btnExportToPdf
            // 
            this.btnExportToPdf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToPdf.Image = global::DllFarmaciaSoft.QRCode.Properties.Resources.PDF;
            this.btnExportToPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToPdf.Name = "btnExportToPdf";
            this.btnExportToPdf.Size = new System.Drawing.Size(111, 54);
            this.btnExportToPdf.Text = "Exportar a PDF";
            this.btnExportToPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToPdf.Click += new System.EventHandler(this.btnExportToPdf_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnToImagePng,
            this.btnToImageJpeg,
            this.btnToImageTiff,
            this.btnToImageGif,
            this.btnToImageBmp});
            this.toolStripSplitButton1.Image = global::DllFarmaciaSoft.QRCode.Properties.Resources.ImageFile;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(148, 54);
            this.toolStripSplitButton1.Text = "Exportar a Imagen";
            this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnToImagePng
            // 
            this.btnToImagePng.Name = "btnToImagePng";
            this.btnToImagePng.Size = new System.Drawing.Size(108, 22);
            this.btnToImagePng.Text = "PNG";
            this.btnToImagePng.Click += new System.EventHandler(this.btnToImagePng_Click);
            // 
            // btnToImageJpeg
            // 
            this.btnToImageJpeg.Name = "btnToImageJpeg";
            this.btnToImageJpeg.Size = new System.Drawing.Size(108, 22);
            this.btnToImageJpeg.Text = "JPEG";
            this.btnToImageJpeg.Click += new System.EventHandler(this.btnToImageJpeg_Click);
            // 
            // btnToImageTiff
            // 
            this.btnToImageTiff.Name = "btnToImageTiff";
            this.btnToImageTiff.Size = new System.Drawing.Size(108, 22);
            this.btnToImageTiff.Text = "TIFF";
            this.btnToImageTiff.Click += new System.EventHandler(this.btnToImageTiff_Click);
            // 
            // btnToImageGif
            // 
            this.btnToImageGif.Name = "btnToImageGif";
            this.btnToImageGif.Size = new System.Drawing.Size(108, 22);
            this.btnToImageGif.Text = "GIF";
            this.btnToImageGif.Click += new System.EventHandler(this.btnToImageGif_Click);
            // 
            // btnToImageBmp
            // 
            this.btnToImageBmp.Name = "btnToImageBmp";
            this.btnToImageBmp.Size = new System.Drawing.Size(108, 22);
            this.btnToImageBmp.Text = "BMP";
            this.btnToImageBmp.Click += new System.EventHandler(this.btnToImageBmp_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 57);
            // 
            // btnXmlTemplate
            // 
            this.btnXmlTemplate.Image = global::DllFarmaciaSoft.QRCode.Properties.Resources.TOXML;
            this.btnXmlTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXmlTemplate.Name = "btnXmlTemplate";
            this.btnXmlTemplate.Size = new System.Drawing.Size(118, 54);
            this.btnXmlTemplate.Text = "Guardar en XML";
            this.btnXmlTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXmlTemplate.Click += new System.EventHandler(this.btnXmlTemplate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 57);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(74, 54);
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // imageViewer
            // 
            this.imageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewer.Location = new System.Drawing.Point(3, 18);
            this.imageViewer.Margin = new System.Windows.Forms.Padding(4);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(1212, 561);
            this.imageViewer.TabIndex = 0;
            // 
            // FrmVisorEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 647);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Frame);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVisorEtiquetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor de etiquetas";
            this.Load += new System.EventHandler(this.FrmVisorEtiquetas_Load);
            this.Shown += new System.EventHandler(this.FrmVisorEtiquetas_Shown);
            this.Frame.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageViewer imageViewer;
        private System.Windows.Forms.GroupBox Frame;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboDpi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportToPdf;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem btnToImagePng;
        private System.Windows.Forms.ToolStripMenuItem btnToImageJpeg;
        private System.Windows.Forms.ToolStripMenuItem btnToImageTiff;
        private System.Windows.Forms.ToolStripMenuItem btnToImageGif;
        private System.Windows.Forms.ToolStripMenuItem btnToImageBmp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnXmlTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnRefresh;
    }
}