namespace MA_Facturacion
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.scComboBoxExt1 = new SC_ControlsCS.scComboBoxExt();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.scTabControlExt2 = new SC_ControlsCS.scTabControlExt();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.scTabControlExt2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Location = new System.Drawing.Point(14, 39);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(200, 67);
            this.fpSpread1.TabIndex = 0;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(192, 71);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "tabPage1";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(192, 71);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "tabPage2";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // scComboBoxExt1
            // 
            this.scComboBoxExt1.BackColorEnabled = System.Drawing.Color.White;
            this.scComboBoxExt1.Data = "";
            this.scComboBoxExt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scComboBoxExt1.Filtro = " 1 = 1";
            this.scComboBoxExt1.FormattingEnabled = true;
            this.scComboBoxExt1.ListaItemsBusqueda = 20;
            this.scComboBoxExt1.Location = new System.Drawing.Point(12, 12);
            this.scComboBoxExt1.MostrarToolTip = false;
            this.scComboBoxExt1.Name = "scComboBoxExt1";
            this.scComboBoxExt1.Size = new System.Drawing.Size(121, 21);
            this.scComboBoxExt1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(340, 70);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "tabPage3";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(340, 133);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "tabPage4";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // scTabControlExt2
            // 
            this.scTabControlExt2.Appearance = SC_ControlsCS.scTabAppearance.Buttons;
            this.scTabControlExt2.BackColor = System.Drawing.Color.Bisque;
            this.scTabControlExt2.BorderColor = System.Drawing.Color.Black;
            this.scTabControlExt2.Controls.Add(this.tabPage10);
            this.scTabControlExt2.Controls.Add(this.tabPage11);
            this.scTabControlExt2.CustomBackColor = true;
            this.scTabControlExt2.Location = new System.Drawing.Point(42, 160);
            this.scTabControlExt2.MostrarBorde = false;
            this.scTabControlExt2.Name = "scTabControlExt2";
            this.scTabControlExt2.SelectedIndex = 0;
            this.scTabControlExt2.Size = new System.Drawing.Size(223, 100);
            this.scTabControlExt2.TabIndex = 8;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(4, 28);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(215, 68);
            this.tabPage10.TabIndex = 0;
            this.tabPage10.Text = "tabPage10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(4, 28);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(215, 68);
            this.tabPage11.TabIndex = 1;
            this.tabPage11.Text = "tabPage11";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(739, 417);
            this.Controls.Add(this.scTabControlExt2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scComboBoxExt1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.fpSpread1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "Form1";
            this.Text = "Form1";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.scTabControlExt2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button button2;
        private SC_ControlsCS.scComboBoxExt scComboBoxExt1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private SC_ControlsCS.scTabControlExt scTabControlExt2;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
    }
}