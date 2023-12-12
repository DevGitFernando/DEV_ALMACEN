namespace DllFarmaciaSoft.QRCode.GenerarEtiquetas
{
    partial class FrmET_SeleccionarEtiquetas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdo_01_Default = new System.Windows.Forms.RadioButton();
            this.rdo_02_Personalizado = new System.Windows.Forms.RadioButton();
            this.nm_02_Hasta = new System.Windows.Forms.NumericUpDown();
            this.nm_01_Desde = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nm_02_Hasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_01_Desde)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdo_02_Personalizado);
            this.groupBox1.Controls.Add(this.rdo_01_Default);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formato";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nm_01_Desde);
            this.groupBox2.Controls.Add(this.nm_02_Hasta);
            this.groupBox2.Location = new System.Drawing.Point(6, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 66);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Etiquetas";
            // 
            // rdo_01_Default
            // 
            this.rdo_01_Default.Location = new System.Drawing.Point(49, 25);
            this.rdo_01_Default.Name = "rdo_01_Default";
            this.rdo_01_Default.Size = new System.Drawing.Size(138, 17);
            this.rdo_01_Default.TabIndex = 0;
            this.rdo_01_Default.TabStop = true;
            this.rdo_01_Default.Text = "Default";
            this.rdo_01_Default.UseVisualStyleBackColor = true;
            this.rdo_01_Default.CheckedChanged += new System.EventHandler(this.rdo_01_Default_CheckedChanged);
            // 
            // rdo_02_Personalizado
            // 
            this.rdo_02_Personalizado.Location = new System.Drawing.Point(208, 25);
            this.rdo_02_Personalizado.Name = "rdo_02_Personalizado";
            this.rdo_02_Personalizado.Size = new System.Drawing.Size(119, 17);
            this.rdo_02_Personalizado.TabIndex = 1;
            this.rdo_02_Personalizado.TabStop = true;
            this.rdo_02_Personalizado.Text = "Personalizado";
            this.rdo_02_Personalizado.UseVisualStyleBackColor = true;
            this.rdo_02_Personalizado.CheckedChanged += new System.EventHandler(this.rdo_02_Personalizado_CheckedChanged);
            // 
            // nm_02_Hasta
            // 
            this.nm_02_Hasta.Location = new System.Drawing.Point(255, 29);
            this.nm_02_Hasta.Name = "nm_02_Hasta";
            this.nm_02_Hasta.Size = new System.Drawing.Size(101, 20);
            this.nm_02_Hasta.TabIndex = 0;
            this.nm_02_Hasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nm_01_Desde
            // 
            this.nm_01_Desde.Location = new System.Drawing.Point(88, 29);
            this.nm_01_Desde.Name = "nm_01_Desde";
            this.nm_01_Desde.Size = new System.Drawing.Size(101, 20);
            this.nm_01_Desde.TabIndex = 1;
            this.nm_01_Desde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(195, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(6, 142);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(185, 47);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(198, 142);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(185, 47);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmET_SeleccionarEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 197);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmET_SeleccionarEtiquetas";
            this.Text = "Seleccionar etiquetas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nm_02_Hasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_01_Desde)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdo_02_Personalizado;
        private System.Windows.Forms.RadioButton rdo_01_Default;
        private System.Windows.Forms.NumericUpDown nm_01_Desde;
        private System.Windows.Forms.NumericUpDown nm_02_Hasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}