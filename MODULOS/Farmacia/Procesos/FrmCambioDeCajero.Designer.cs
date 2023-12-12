namespace Farmacia.Procesos
{
    partial class FrmCambioDeCajero
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPersonalSale = new System.Windows.Forms.Label();
            this.txtIdPersonalSale = new SC_ControlsCS.scTextBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtIdPersonalEntra = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPersonalEntra = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPersonalSale);
            this.groupBox3.Controls.Add(this.txtIdPersonalSale);
            this.groupBox3.Location = new System.Drawing.Point(8, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(476, 49);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Personal que Sale";
            // 
            // lblPersonalSale
            // 
            this.lblPersonalSale.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonalSale.Location = new System.Drawing.Point(82, 18);
            this.lblPersonalSale.Name = "lblPersonalSale";
            this.lblPersonalSale.Size = new System.Drawing.Size(382, 21);
            this.lblPersonalSale.TabIndex = 3;
            this.lblPersonalSale.Text = "Proveedor :";
            this.lblPersonalSale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonalSale
            // 
            this.txtIdPersonalSale.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonalSale.Decimales = 2;
            this.txtIdPersonalSale.Enabled = false;
            this.txtIdPersonalSale.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonalSale.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonalSale.Location = new System.Drawing.Point(17, 18);
            this.txtIdPersonalSale.MaxLength = 4;
            this.txtIdPersonalSale.Name = "txtIdPersonalSale";
            this.txtIdPersonalSale.PermitirApostrofo = false;
            this.txtIdPersonalSale.PermitirNegativos = false;
            this.txtIdPersonalSale.Size = new System.Drawing.Size(60, 20);
            this.txtIdPersonalSale.TabIndex = 2;
            this.txtIdPersonalSale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtIdPersonalEntra);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblPersonalEntra);
            this.groupBox1.Location = new System.Drawing.Point(8, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Personal que Entra";
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.Location = new System.Drawing.Point(82, 48);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtIdPersonalEntra
            // 
            this.txtIdPersonalEntra.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonalEntra.Decimales = 2;
            this.txtIdPersonalEntra.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonalEntra.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonalEntra.Location = new System.Drawing.Point(17, 18);
            this.txtIdPersonalEntra.MaxLength = 4;
            this.txtIdPersonalEntra.Name = "txtIdPersonalEntra";
            this.txtIdPersonalEntra.PermitirApostrofo = false;
            this.txtIdPersonalEntra.PermitirNegativos = false;
            this.txtIdPersonalEntra.Size = new System.Drawing.Size(60, 20);
            this.txtIdPersonalEntra.TabIndex = 0;
            this.txtIdPersonalEntra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPersonalEntra.TextChanged += new System.EventHandler(this.txtIdPersonalEntra_TextChanged);
            this.txtIdPersonalEntra.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonalEntra_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Password:";
            // 
            // lblPersonalEntra
            // 
            this.lblPersonalEntra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonalEntra.Location = new System.Drawing.Point(82, 18);
            this.lblPersonalEntra.Name = "lblPersonalEntra";
            this.lblPersonalEntra.Size = new System.Drawing.Size(382, 21);
            this.lblPersonalEntra.TabIndex = 1;
            this.lblPersonalEntra.Text = "Proveedor :";
            this.lblPersonalEntra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(358, 152);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(126, 23);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Registrar cambio";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FrmCambioDeCajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 183);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCambioDeCajero";
            this.Text = "Cambio de Cajero";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCambioDeCajero_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPersonalSale;
        private SC_ControlsCS.scTextBoxExt txtIdPersonalSale;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPersonalEntra;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtIdPersonalEntra;
        private System.Windows.Forms.TextBox txtPassword;
    }
}