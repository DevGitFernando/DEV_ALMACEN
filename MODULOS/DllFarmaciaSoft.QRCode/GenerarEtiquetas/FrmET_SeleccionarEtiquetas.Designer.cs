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
            this.rdo_02_Personalizado = new System.Windows.Forms.RadioButton();
            this.rdo_01_Default = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nm_01_Desde = new System.Windows.Forms.NumericUpDown();
            this.nm_02_Hasta = new System.Windows.Forms.NumericUpDown();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nm_01_Desde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_02_Hasta)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdo_02_Personalizado);
            this.groupBox1.Controls.Add(this.rdo_01_Default);
            this.groupBox1.Location = new System.Drawing.Point(8, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(503, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formato";
            // 
            // rdo_02_Personalizado
            // 
            this.rdo_02_Personalizado.Location = new System.Drawing.Point(277, 31);
            this.rdo_02_Personalizado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdo_02_Personalizado.Name = "rdo_02_Personalizado";
            this.rdo_02_Personalizado.Size = new System.Drawing.Size(159, 21);
            this.rdo_02_Personalizado.TabIndex = 1;
            this.rdo_02_Personalizado.TabStop = true;
            this.rdo_02_Personalizado.Text = "Personalizado";
            this.rdo_02_Personalizado.UseVisualStyleBackColor = true;
            this.rdo_02_Personalizado.CheckedChanged += new System.EventHandler(this.rdo_02_Personalizado_CheckedChanged);
            // 
            // rdo_01_Default
            // 
            this.rdo_01_Default.Location = new System.Drawing.Point(65, 31);
            this.rdo_01_Default.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdo_01_Default.Name = "rdo_01_Default";
            this.rdo_01_Default.Size = new System.Drawing.Size(184, 21);
            this.rdo_01_Default.TabIndex = 0;
            this.rdo_01_Default.TabStop = true;
            this.rdo_01_Default.Text = "Default";
            this.rdo_01_Default.UseVisualStyleBackColor = true;
            this.rdo_01_Default.CheckedChanged += new System.EventHandler(this.rdo_01_Default_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nm_01_Desde);
            this.groupBox2.Controls.Add(this.nm_02_Hasta);
            this.groupBox2.Location = new System.Drawing.Point(8, 86);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(503, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Etiquetas";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(260, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(37, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nm_01_Desde
            // 
            this.nm_01_Desde.Location = new System.Drawing.Point(117, 36);
            this.nm_01_Desde.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nm_01_Desde.Name = "nm_01_Desde";
            this.nm_01_Desde.Size = new System.Drawing.Size(135, 22);
            this.nm_01_Desde.TabIndex = 1;
            this.nm_01_Desde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nm_02_Hasta
            // 
            this.nm_02_Hasta.Location = new System.Drawing.Point(340, 36);
            this.nm_02_Hasta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nm_02_Hasta.Name = "nm_02_Hasta";
            this.nm_02_Hasta.Size = new System.Drawing.Size(135, 22);
            this.nm_02_Hasta.TabIndex = 0;
            this.nm_02_Hasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(8, 175);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(247, 58);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(264, 175);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(247, 58);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmET_SeleccionarEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 242);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmET_SeleccionarEtiquetas";
            this.ShowIcon = false;
            this.Text = "Seleccionar etiquetas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nm_01_Desde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nm_02_Hasta)).EndInit();
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