namespace Test_Modulos
{
    partial class FrmIntegrarInformacion
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
            this.button1 = new System.Windows.Forms.Button();
            this.scComboBoxExt1 = new SC_ControlsCS.scComboBoxExt();
            this.scTextBoxExt1 = new SC_ControlsCS.scTextBoxExt();
            this.button2 = new System.Windows.Forms.Button();
            this.txtServidor = new SC_ControlsCS.scTextBoxExt();
            this.txtBaseDeDatos = new SC_ControlsCS.scTextBoxExt();
            this.txtUsuario = new SC_ControlsCS.scTextBoxExt();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.txtPuerto = new SC_ControlsCS.scTextBoxExt();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(233, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Web Service";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // scComboBoxExt1
            // 
            this.scComboBoxExt1.BackColorEnabled = System.Drawing.Color.White;
            this.scComboBoxExt1.Data = "";
            this.scComboBoxExt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scComboBoxExt1.Filtro = " 1 = 1";
            this.scComboBoxExt1.FormattingEnabled = true;
            this.scComboBoxExt1.ListaItemsBusqueda = 20;
            this.scComboBoxExt1.Location = new System.Drawing.Point(12, 87);
            this.scComboBoxExt1.MostrarToolTip = false;
            this.scComboBoxExt1.Name = "scComboBoxExt1";
            this.scComboBoxExt1.Size = new System.Drawing.Size(232, 21);
            this.scComboBoxExt1.TabIndex = 1;
            // 
            // scTextBoxExt1
            // 
            this.scTextBoxExt1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt1.Decimales = 2;
            this.scTextBoxExt1.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.scTextBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt1.Location = new System.Drawing.Point(12, 114);
            this.scTextBoxExt1.Name = "scTextBoxExt1";
            this.scTextBoxExt1.PermitirApostrofo = false;
            this.scTextBoxExt1.PermitirNegativos = false;
            this.scTextBoxExt1.Size = new System.Drawing.Size(607, 20);
            this.scTextBoxExt1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(233, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Local";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtServidor
            // 
            this.txtServidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServidor.Decimales = 2;
            this.txtServidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtServidor.ForeColor = System.Drawing.Color.Black;
            this.txtServidor.Location = new System.Drawing.Point(255, 207);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.PermitirApostrofo = false;
            this.txtServidor.PermitirNegativos = false;
            this.txtServidor.Size = new System.Drawing.Size(364, 20);
            this.txtServidor.TabIndex = 4;
            // 
            // txtBaseDeDatos
            // 
            this.txtBaseDeDatos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBaseDeDatos.Decimales = 2;
            this.txtBaseDeDatos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtBaseDeDatos.ForeColor = System.Drawing.Color.Black;
            this.txtBaseDeDatos.Location = new System.Drawing.Point(255, 233);
            this.txtBaseDeDatos.Name = "txtBaseDeDatos";
            this.txtBaseDeDatos.PermitirApostrofo = false;
            this.txtBaseDeDatos.PermitirNegativos = false;
            this.txtBaseDeDatos.Size = new System.Drawing.Size(364, 20);
            this.txtBaseDeDatos.TabIndex = 5;
            // 
            // txtUsuario
            // 
            this.txtUsuario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUsuario.Decimales = 2;
            this.txtUsuario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUsuario.ForeColor = System.Drawing.Color.Black;
            this.txtUsuario.Location = new System.Drawing.Point(255, 259);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PermitirApostrofo = false;
            this.txtUsuario.PermitirNegativos = false;
            this.txtUsuario.Size = new System.Drawing.Size(364, 20);
            this.txtUsuario.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(255, 285);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(364, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // txtPuerto
            // 
            this.txtPuerto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPuerto.Decimales = 2;
            this.txtPuerto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPuerto.ForeColor = System.Drawing.Color.Black;
            this.txtPuerto.Location = new System.Drawing.Point(255, 311);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.PermitirApostrofo = false;
            this.txtPuerto.PermitirNegativos = false;
            this.txtPuerto.Size = new System.Drawing.Size(364, 20);
            this.txtPuerto.TabIndex = 8;
            // 
            // FrmIntegrarInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 355);
            this.Controls.Add(this.txtPuerto);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtBaseDeDatos);
            this.Controls.Add(this.txtServidor);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.scTextBoxExt1);
            this.Controls.Add(this.scComboBoxExt1);
            this.Controls.Add(this.button1);
            this.Name = "FrmIntegrarInformacion";
            this.ShowInTaskbar = true;
            this.Text = "FrmIntegrarInformacion";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private SC_ControlsCS.scComboBoxExt scComboBoxExt1;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt1;
        private System.Windows.Forms.Button button2;
        private SC_ControlsCS.scTextBoxExt txtServidor;
        private SC_ControlsCS.scTextBoxExt txtBaseDeDatos;
        private SC_ControlsCS.scTextBoxExt txtUsuario;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private SC_ControlsCS.scTextBoxExt txtPuerto;
    }
}