namespace SII_Servicio_Cliente.SvrServicio
{
    partial class FrmInstalarServicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInstalarServicio));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInicioNormal = new System.Windows.Forms.Button();
            this.btnDesinstalar = new System.Windows.Forms.Button();
            this.btnInstalar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInicioNormal);
            this.groupBox1.Controls.Add(this.btnDesinstalar);
            this.groupBox1.Controls.Add(this.btnInstalar);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // btnInicioNormal
            // 
            this.btnInicioNormal.Location = new System.Drawing.Point(21, 67);
            this.btnInicioNormal.Name = "btnInicioNormal";
            this.btnInicioNormal.Size = new System.Drawing.Size(175, 23);
            this.btnInicioNormal.TabIndex = 2;
            this.btnInicioNormal.Text = "Iniciar";
            this.btnInicioNormal.UseVisualStyleBackColor = true;
            this.btnInicioNormal.Click += new System.EventHandler(this.btnInicioNormal_Click);
            // 
            // btnDesinstalar
            // 
            this.btnDesinstalar.Location = new System.Drawing.Point(21, 43);
            this.btnDesinstalar.Name = "btnDesinstalar";
            this.btnDesinstalar.Size = new System.Drawing.Size(175, 23);
            this.btnDesinstalar.TabIndex = 1;
            this.btnDesinstalar.Text = "Desregistrar";
            this.btnDesinstalar.UseVisualStyleBackColor = true;
            this.btnDesinstalar.Click += new System.EventHandler(this.btnDesinstalar_Click);
            // 
            // btnInstalar
            // 
            this.btnInstalar.Location = new System.Drawing.Point(21, 19);
            this.btnInstalar.Name = "btnInstalar";
            this.btnInstalar.Size = new System.Drawing.Size(175, 23);
            this.btnInstalar.TabIndex = 0;
            this.btnInstalar.Text = "Registrar";
            this.btnInstalar.UseVisualStyleBackColor = true;
            this.btnInstalar.Click += new System.EventHandler(this.btnInstalar_Click);
            // 
            // FrmInstalarServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 115);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInstalarServicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SII Servicio Cliente";
            this.Load += new System.EventHandler(this.FrmInstalarServicio_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDesinstalar;
        private System.Windows.Forms.Button btnInstalar;
        private System.Windows.Forms.Button btnInicioNormal;
    }
}