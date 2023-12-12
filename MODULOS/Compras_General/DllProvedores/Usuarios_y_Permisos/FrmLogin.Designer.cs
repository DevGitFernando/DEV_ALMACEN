namespace DllProveedores.Usuarios_y_Permisos
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.FrameLogin = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.txtUsuario = new SC_ControlsCS.scTextBoxExt();
            this.FrameLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameLogin
            // 
            this.FrameLogin.Controls.Add(this.btnCancelar);
            this.FrameLogin.Controls.Add(this.btnAceptar);
            this.FrameLogin.Controls.Add(this.lblPassword);
            this.FrameLogin.Controls.Add(this.lblUsuario);
            this.FrameLogin.Controls.Add(this.txtPassword);
            this.FrameLogin.Controls.Add(this.txtUsuario);
            this.FrameLogin.Location = new System.Drawing.Point(6, 2);
            this.FrameLogin.Name = "FrameLogin";
            this.FrameLogin.Size = new System.Drawing.Size(316, 107);
            this.FrameLogin.TabIndex = 0;
            this.FrameLogin.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(217, 72);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(89, 24);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(122, 72);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(89, 24);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(7, 45);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(91, 15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password :";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Location = new System.Drawing.Point(7, 19);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(91, 15);
            this.lblUsuario.TabIndex = 8;
            this.lblUsuario.Text = "Nombre usuario :";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(100, 42);
            this.txtPassword.MaxLength = 40;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(206, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "0123456789012345678901234567890123456789";
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUsuario
            // 
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUsuario.Decimales = 2;
            this.txtUsuario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUsuario.ForeColor = System.Drawing.Color.Black;
            this.txtUsuario.Location = new System.Drawing.Point(100, 16);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PermitirApostrofo = false;
            this.txtUsuario.PermitirNegativos = false;
            this.txtUsuario.Size = new System.Drawing.Size(206, 20);
            this.txtUsuario.TabIndex = 3;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 117);
            this.Controls.Add(this.FrameLogin);
            this.Name = "FrmLogin";
            this.Text = "Autenticación de usuarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEdoLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEdoLogin_KeyDown);
            this.FrameLogin.ResumeLayout(false);
            this.FrameLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameLogin;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsuario;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private SC_ControlsCS.scTextBoxExt txtUsuario;
    }
}