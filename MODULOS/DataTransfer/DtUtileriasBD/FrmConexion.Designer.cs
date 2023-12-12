namespace DtUtileriasBD
{
    partial class FrmConexion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConexion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCnnConfianza = new System.Windows.Forms.CheckBox();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.mnGenerarPassword = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnGenPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelarConexion = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.mnGenerarPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelarConexion);
            this.groupBox1.Controls.Add(this.chkCnnConfianza);
            this.groupBox1.Controls.Add(this.btnDesconectar);
            this.groupBox1.Controls.Add(this.btnConectar);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkCnnConfianza
            // 
            this.chkCnnConfianza.Location = new System.Drawing.Point(77, 99);
            this.chkCnnConfianza.Name = "chkCnnConfianza";
            this.chkCnnConfianza.Size = new System.Drawing.Size(140, 17);
            this.chkCnnConfianza.TabIndex = 3;
            this.chkCnnConfianza.Text = "Conexión de confianza";
            this.chkCnnConfianza.UseVisualStyleBackColor = true;
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(321, 117);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(100, 24);
            this.btnDesconectar.TabIndex = 5;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(215, 117);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(100, 24);
            this.btnConectar.TabIndex = 4;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtPass
            // 
            this.txtPass.ContextMenuStrip = this.mnGenerarPassword;
            this.txtPass.Location = new System.Drawing.Point(77, 70);
            this.txtPass.MaxLength = 50;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(344, 20);
            this.txtPass.TabIndex = 2;
            // 
            // mnGenerarPassword
            // 
            this.mnGenerarPassword.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenPassword});
            this.mnGenerarPassword.Name = "mnGenerarPassword";
            this.mnGenerarPassword.Size = new System.Drawing.Size(169, 26);
            // 
            // btnGenPassword
            // 
            this.btnGenPassword.Name = "btnGenPassword";
            this.btnGenPassword.Size = new System.Drawing.Size(168, 22);
            this.btnGenPassword.Text = "Generar password";
            this.btnGenPassword.Click += new System.EventHandler(this.btnGenPassword_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(77, 43);
            this.txtUser.MaxLength = 50;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(344, 20);
            this.txtUser.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Usuario :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(77, 17);
            this.txtServer.MaxLength = 75;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(344, 20);
            this.txtServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancelarConexion
            // 
            this.btnCancelarConexion.Enabled = false;
            this.btnCancelarConexion.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarConexion.Image")));
            this.btnCancelarConexion.Location = new System.Drawing.Point(181, 117);
            this.btnCancelarConexion.Name = "btnCancelarConexion";
            this.btnCancelarConexion.Size = new System.Drawing.Size(31, 23);
            this.btnCancelarConexion.TabIndex = 11;
            this.btnCancelarConexion.Text = "...";
            this.btnCancelarConexion.UseVisualStyleBackColor = true;
            this.btnCancelarConexion.Click += new System.EventHandler(this.btnCancelarConexion_Click);
            // 
            // FrmConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 162);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConexion";
            this.Text = "Datos de Conexión";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConexion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mnGenerarPassword.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCnnConfianza;
        private System.Windows.Forms.ContextMenuStrip mnGenerarPassword;
        private System.Windows.Forms.ToolStripMenuItem btnGenPassword;
        private System.Windows.Forms.Button btnCancelarConexion;
    }
}