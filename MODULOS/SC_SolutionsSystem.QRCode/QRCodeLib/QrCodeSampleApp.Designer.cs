namespace SC_SolutionsSystem.QRCode
{
    partial class QrCodeSampleApp
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabEncode = new System.Windows.Forms.TabPage();
            this.btnLector = new System.Windows.Forms.Button();
            this.lblColorTexto = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblColorFondo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCorrectionLevel = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.btnEncode = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEncoding = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEncodeData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picEncode = new System.Windows.Forms.PictureBox();
            this.tabDecode = new System.Windows.Forms.TabPage();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.txtDecodedData = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.picDecode = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.txtFormato = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabEncode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
            this.tabDecode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabEncode);
            this.tabMain.Controls.Add(this.tabDecode);
            this.tabMain.Location = new System.Drawing.Point(12, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(621, 477);
            this.tabMain.TabIndex = 0;
            // 
            // tabEncode
            // 
            this.tabEncode.Controls.Add(this.txtFormato);
            this.tabEncode.Controls.Add(this.label8);
            this.tabEncode.Controls.Add(this.btnLector);
            this.tabEncode.Controls.Add(this.lblColorTexto);
            this.tabEncode.Controls.Add(this.label10);
            this.tabEncode.Controls.Add(this.lblColorFondo);
            this.tabEncode.Controls.Add(this.label7);
            this.tabEncode.Controls.Add(this.cboCorrectionLevel);
            this.tabEncode.Controls.Add(this.btnPrint);
            this.tabEncode.Controls.Add(this.label5);
            this.tabEncode.Controls.Add(this.btnSave);
            this.tabEncode.Controls.Add(this.txtSize);
            this.tabEncode.Controls.Add(this.btnEncode);
            this.tabEncode.Controls.Add(this.label4);
            this.tabEncode.Controls.Add(this.cboVersion);
            this.tabEncode.Controls.Add(this.label3);
            this.tabEncode.Controls.Add(this.cboEncoding);
            this.tabEncode.Controls.Add(this.label2);
            this.tabEncode.Controls.Add(this.txtEncodeData);
            this.tabEncode.Controls.Add(this.label1);
            this.tabEncode.Controls.Add(this.picEncode);
            this.tabEncode.Location = new System.Drawing.Point(4, 22);
            this.tabEncode.Name = "tabEncode";
            this.tabEncode.Padding = new System.Windows.Forms.Padding(3);
            this.tabEncode.Size = new System.Drawing.Size(613, 451);
            this.tabEncode.TabIndex = 0;
            this.tabEncode.Text = "Codificar";
            this.tabEncode.UseVisualStyleBackColor = true;
            // 
            // btnLector
            // 
            this.btnLector.Location = new System.Drawing.Point(513, 381);
            this.btnLector.Name = "btnLector";
            this.btnLector.Size = new System.Drawing.Size(94, 27);
            this.btnLector.TabIndex = 15;
            this.btnLector.Text = "Leer código";
            this.btnLector.UseVisualStyleBackColor = true;
            this.btnLector.Click += new System.EventHandler(this.btnLector_Click);
            // 
            // lblColorTexto
            // 
            this.lblColorTexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColorTexto.Location = new System.Drawing.Point(98, 386);
            this.lblColorTexto.Name = "lblColorTexto";
            this.lblColorTexto.Size = new System.Drawing.Size(158, 16);
            this.lblColorTexto.TabIndex = 14;
            this.lblColorTexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblColorTexto.Click += new System.EventHandler(this.lblColorTexto_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Color texto : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblColorFondo
            // 
            this.lblColorFondo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColorFondo.Location = new System.Drawing.Point(98, 360);
            this.lblColorFondo.Name = "lblColorFondo";
            this.lblColorFondo.Size = new System.Drawing.Size(158, 16);
            this.lblColorFondo.TabIndex = 12;
            this.lblColorFondo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblColorFondo.Click += new System.EventHandler(this.lblColorFondo_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 362);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Color fondo : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCorrectionLevel
            // 
            this.cboCorrectionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCorrectionLevel.FormattingEnabled = true;
            this.cboCorrectionLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.cboCorrectionLevel.Location = new System.Drawing.Point(380, 305);
            this.cboCorrectionLevel.Name = "cboCorrectionLevel";
            this.cboCorrectionLevel.Size = new System.Drawing.Size(84, 21);
            this.cboCorrectionLevel.TabIndex = 10;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(513, 418);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(94, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(262, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nivel de correción : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(418, 418);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(380, 329);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(43, 20);
            this.txtSize.TabIndex = 8;
            this.txtSize.Text = "4";
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(323, 418);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(94, 27);
            this.btnEncode.TabIndex = 1;
            this.btnEncode.Text = "Codificar";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click_1);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(339, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Size : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboVersion
            // 
            this.cboVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersion.FormattingEnabled = true;
            this.cboVersion.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "40",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40"});
            this.cboVersion.Location = new System.Drawing.Point(99, 332);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(157, 21);
            this.cboVersion.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(44, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Versión : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEncoding
            // 
            this.cboEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncoding.FormattingEnabled = true;
            this.cboEncoding.Items.AddRange(new object[] {
            "AlphaNumeric",
            "Numeric",
            "Byte"});
            this.cboEncoding.Location = new System.Drawing.Point(99, 305);
            this.cboEncoding.Name = "cboEncoding";
            this.cboEncoding.Size = new System.Drawing.Size(157, 21);
            this.cboEncoding.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(34, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Encoding : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEncodeData
            // 
            this.txtEncodeData.Location = new System.Drawing.Point(76, 245);
            this.txtEncodeData.Name = "txtEncodeData";
            this.txtEncodeData.Size = new System.Drawing.Size(531, 20);
            this.txtEncodeData.TabIndex = 2;
            this.txtEncodeData.Text = "SC_SolutionsSystem Interactive Technologies";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(34, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data : ";
            // 
            // picEncode
            // 
            this.picEncode.BackColor = System.Drawing.Color.White;
            this.picEncode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picEncode.Location = new System.Drawing.Point(6, 6);
            this.picEncode.Name = "picEncode";
            this.picEncode.Size = new System.Drawing.Size(601, 201);
            this.picEncode.TabIndex = 0;
            this.picEncode.TabStop = false;
            // 
            // tabDecode
            // 
            this.tabDecode.Controls.Add(this.btnOpen);
            this.tabDecode.Controls.Add(this.btnDecode);
            this.tabDecode.Controls.Add(this.txtDecodedData);
            this.tabDecode.Controls.Add(this.label6);
            this.tabDecode.Controls.Add(this.picDecode);
            this.tabDecode.Location = new System.Drawing.Point(4, 22);
            this.tabDecode.Name = "tabDecode";
            this.tabDecode.Padding = new System.Windows.Forms.Padding(3);
            this.tabDecode.Size = new System.Drawing.Size(475, 451);
            this.tabDecode.TabIndex = 1;
            this.tabDecode.Text = "Decodificar";
            this.tabDecode.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(260, 368);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(94, 27);
            this.btnOpen.TabIndex = 7;
            this.btnOpen.Text = "Abrir";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(370, 368);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(94, 27);
            this.btnDecode.TabIndex = 4;
            this.btnDecode.Text = "Decodificar";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click_1);
            // 
            // txtDecodedData
            // 
            this.txtDecodedData.Location = new System.Drawing.Point(51, 331);
            this.txtDecodedData.Name = "txtDecodedData";
            this.txtDecodedData.Size = new System.Drawing.Size(413, 20);
            this.txtDecodedData.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Data : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picDecode
            // 
            this.picDecode.BackColor = System.Drawing.Color.White;
            this.picDecode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDecode.Location = new System.Drawing.Point(6, 6);
            this.picDecode.Name = "picDecode";
            this.picDecode.Size = new System.Drawing.Size(458, 319);
            this.picDecode.TabIndex = 3;
            this.picDecode.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.fileToolStripMenuItem.Text = "&Archivo";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem.Text = "S&alir";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // txtFormato
            // 
            this.txtFormato.Location = new System.Drawing.Point(76, 271);
            this.txtFormato.Name = "txtFormato";
            this.txtFormato.Size = new System.Drawing.Size(531, 20);
            this.txtFormato.TabIndex = 17;
            this.txtFormato.Text = "SC_SolutionsSystem Interactive Technologies";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(18, 271);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Formato : ";
            // 
            // QrCodeSampleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 510);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QrCodeSampleApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QRCode .NET Library";
            this.Load += new System.EventHandler(this.frmSample_Load);
            this.tabMain.ResumeLayout(false);
            this.tabEncode.ResumeLayout(false);
            this.tabEncode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
            this.tabDecode.ResumeLayout(false);
            this.tabDecode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabEncode;
        private System.Windows.Forms.TabPage tabDecode;
        private System.Windows.Forms.PictureBox picEncode;
        private System.Windows.Forms.TextBox txtEncodeData;
        private System.Windows.Forms.ComboBox cboCorrectionLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboEncoding;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.TextBox txtDecodedData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox picDecode;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnOpen;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblColorFondo;
        private System.Windows.Forms.Label lblColorTexto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnLector;
        private System.Windows.Forms.TextBox txtFormato;
        private System.Windows.Forms.Label label8;
    }
}

