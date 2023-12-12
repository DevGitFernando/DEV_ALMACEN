namespace SC_SolutionsSystem.QRCode
{
    partial class FrmQRCodeViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQRCodeViewer));
            this.picEncode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
            this.SuspendLayout();
            // 
            // picEncode
            // 
            this.picEncode.BackColor = System.Drawing.Color.White;
            this.picEncode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picEncode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEncode.Location = new System.Drawing.Point(0, 0);
            this.picEncode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picEncode.Name = "picEncode";
            this.picEncode.Size = new System.Drawing.Size(645, 569);
            this.picEncode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picEncode.TabIndex = 1;
            this.picEncode.TabStop = false;
            this.picEncode.WaitOnLoad = true;
            // 
            // FrmQRCodeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 569);
            this.Controls.Add(this.picEncode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(261, 235);
            this.Name = "FrmQRCodeViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QRCodeViewer";
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picEncode;

    }
}