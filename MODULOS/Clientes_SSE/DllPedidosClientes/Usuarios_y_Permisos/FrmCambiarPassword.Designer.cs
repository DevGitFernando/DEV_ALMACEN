namespace DllPedidosClientes.Usuarios_y_Permisos
{
    partial class FrmCambiarPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCambiarPassword));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPasswordAnterior = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPasswordCon = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogin = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPasswordAnterior);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPasswordCon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Usuario";
            // 
            // txtPasswordAnterior
            // 
            this.txtPasswordAnterior.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPasswordAnterior.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasswordAnterior.Decimales = 2;
            this.txtPasswordAnterior.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPasswordAnterior.ForeColor = System.Drawing.Color.Black;
            this.txtPasswordAnterior.Location = new System.Drawing.Point(118, 43);
            this.txtPasswordAnterior.MaxLength = 20;
            this.txtPasswordAnterior.Name = "txtPasswordAnterior";
            this.txtPasswordAnterior.PasswordChar = '*';
            this.txtPasswordAnterior.PermitirApostrofo = false;
            this.txtPasswordAnterior.PermitirNegativos = false;
            this.txtPasswordAnterior.Size = new System.Drawing.Size(217, 20);
            this.txtPasswordAnterior.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 15);
            this.label6.TabIndex = 33;
            this.label6.Text = "Password anterior :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPasswordCon
            // 
            this.txtPasswordCon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPasswordCon.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasswordCon.Decimales = 2;
            this.txtPasswordCon.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPasswordCon.ForeColor = System.Drawing.Color.Black;
            this.txtPasswordCon.Location = new System.Drawing.Point(118, 93);
            this.txtPasswordCon.MaxLength = 20;
            this.txtPasswordCon.Name = "txtPasswordCon";
            this.txtPasswordCon.PasswordChar = '*';
            this.txtPasswordCon.PermitirApostrofo = false;
            this.txtPasswordCon.PermitirNegativos = false;
            this.txtPasswordCon.Size = new System.Drawing.Size(217, 20);
            this.txtPasswordCon.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Confirmar Password :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(118, 69);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(217, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(47, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 29;
            this.label4.Text = "Password :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLogin
            // 
            this.txtLogin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLogin.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtLogin.Decimales = 2;
            this.txtLogin.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtLogin.ForeColor = System.Drawing.Color.Black;
            this.txtLogin.Location = new System.Drawing.Point(118, 17);
            this.txtLogin.MaxLength = 20;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.PermitirApostrofo = false;
            this.txtLogin.PermitirNegativos = false;
            this.txtLogin.Size = new System.Drawing.Size(217, 20);
            this.txtLogin.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(70, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Login :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(373, 25);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmCambiarPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 162);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCambiarPassword";
            this.Text = "Cambiar Password de Usuario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCambiarPassword_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtPasswordCon;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtLogin;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtPasswordAnterior;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}