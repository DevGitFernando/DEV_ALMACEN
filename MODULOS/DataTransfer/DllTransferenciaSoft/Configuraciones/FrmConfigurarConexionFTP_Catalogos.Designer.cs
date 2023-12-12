namespace DllTransferenciaSoft.Configuraciones
{
    partial class FrmConfigurarConexionFTP_Catalogos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarConexionFTP_Catalogos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTestFTP = new System.Windows.Forms.ToolStripButton();
            this.txtDirectorioDefault = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.FrameInfUnidad = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.lblEstado02 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuPassword = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.encriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desencriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPassFTP = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuarioFTP = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServidorFTP = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInfUnidad.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnTestFTP});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(557, 25);
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
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTestFTP
            // 
            this.btnTestFTP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTestFTP.Image = ((System.Drawing.Image)(resources.GetObject("btnTestFTP.Image")));
            this.btnTestFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTestFTP.Name = "btnTestFTP";
            this.btnTestFTP.Size = new System.Drawing.Size(23, 22);
            this.btnTestFTP.Text = "Probar conexión FTP";
            this.btnTestFTP.Click += new System.EventHandler(this.btnTestFTP_Click);
            // 
            // txtDirectorioDefault
            // 
            this.txtDirectorioDefault.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDirectorioDefault.Decimales = 2;
            this.txtDirectorioDefault.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDirectorioDefault.ForeColor = System.Drawing.Color.Black;
            this.txtDirectorioDefault.Location = new System.Drawing.Point(112, 103);
            this.txtDirectorioDefault.MaxLength = 500;
            this.txtDirectorioDefault.Multiline = true;
            this.txtDirectorioDefault.Name = "txtDirectorioDefault";
            this.txtDirectorioDefault.PermitirApostrofo = false;
            this.txtDirectorioDefault.PermitirNegativos = false;
            this.txtDirectorioDefault.Size = new System.Drawing.Size(417, 59);
            this.txtDirectorioDefault.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Directorio default :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInfUnidad
            // 
            this.FrameInfUnidad.Controls.Add(this.lblEstado);
            this.FrameInfUnidad.Controls.Add(this.txtIdEstado);
            this.FrameInfUnidad.Controls.Add(this.lblEstado02);
            this.FrameInfUnidad.Location = new System.Drawing.Point(9, 31);
            this.FrameInfUnidad.Name = "FrameInfUnidad";
            this.FrameInfUnidad.Size = new System.Drawing.Size(538, 49);
            this.FrameInfUnidad.TabIndex = 1;
            this.FrameInfUnidad.TabStop = false;
            this.FrameInfUnidad.Text = "Datos de Estado";
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(177, 19);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(352, 17);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(112, 19);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(59, 20);
            this.txtIdEstado.TabIndex = 0;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // lblEstado02
            // 
            this.lblEstado02.Location = new System.Drawing.Point(63, 21);
            this.lblEstado02.Name = "lblEstado02";
            this.lblEstado02.Size = new System.Drawing.Size(48, 17);
            this.lblEstado02.TabIndex = 19;
            this.lblEstado02.Text = "Estado :";
            this.lblEstado02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.ContextMenuStrip = this.contextMenuPassword;
            this.groupBox1.Controls.Add(this.txtDirectorioDefault);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassFTP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUsuarioFTP);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtServidorFTP);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(9, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 174);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de conexión FTP";
            // 
            // contextMenuPassword
            // 
            this.contextMenuPassword.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encriptarToolStripMenuItem,
            this.desencriptarToolStripMenuItem});
            this.contextMenuPassword.Name = "contextMenuPassword";
            this.contextMenuPassword.Size = new System.Drawing.Size(141, 48);
            // 
            // encriptarToolStripMenuItem
            // 
            this.encriptarToolStripMenuItem.Name = "encriptarToolStripMenuItem";
            this.encriptarToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.encriptarToolStripMenuItem.Text = "Encriptar";
            this.encriptarToolStripMenuItem.Click += new System.EventHandler(this.encriptarToolStripMenuItem_Click);
            // 
            // desencriptarToolStripMenuItem
            // 
            this.desencriptarToolStripMenuItem.Name = "desencriptarToolStripMenuItem";
            this.desencriptarToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.desencriptarToolStripMenuItem.Text = "Desencriptar";
            this.desencriptarToolStripMenuItem.Click += new System.EventHandler(this.desencriptarToolStripMenuItem_Click);
            // 
            // txtPassFTP
            // 
            this.txtPassFTP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassFTP.Decimales = 2;
            this.txtPassFTP.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassFTP.ForeColor = System.Drawing.Color.Black;
            this.txtPassFTP.Location = new System.Drawing.Point(112, 77);
            this.txtPassFTP.MaxLength = 50;
            this.txtPassFTP.Name = "txtPassFTP";
            this.txtPassFTP.PasswordChar = '*';
            this.txtPassFTP.PermitirApostrofo = false;
            this.txtPassFTP.PermitirNegativos = false;
            this.txtPassFTP.Size = new System.Drawing.Size(233, 20);
            this.txtPassFTP.TabIndex = 2;
            this.txtPassFTP.Text = "0123456789012345678901234567890123456789";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Password :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsuarioFTP
            // 
            this.txtUsuarioFTP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUsuarioFTP.Decimales = 2;
            this.txtUsuarioFTP.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUsuarioFTP.ForeColor = System.Drawing.Color.Black;
            this.txtUsuarioFTP.Location = new System.Drawing.Point(112, 51);
            this.txtUsuarioFTP.MaxLength = 50;
            this.txtUsuarioFTP.Name = "txtUsuarioFTP";
            this.txtUsuarioFTP.PermitirApostrofo = false;
            this.txtUsuarioFTP.PermitirNegativos = false;
            this.txtUsuarioFTP.Size = new System.Drawing.Size(233, 20);
            this.txtUsuarioFTP.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Usuario :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServidorFTP
            // 
            this.txtServidorFTP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServidorFTP.Decimales = 2;
            this.txtServidorFTP.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtServidorFTP.ForeColor = System.Drawing.Color.Black;
            this.txtServidorFTP.Location = new System.Drawing.Point(112, 25);
            this.txtServidorFTP.MaxLength = 50;
            this.txtServidorFTP.Name = "txtServidorFTP";
            this.txtServidorFTP.PermitirApostrofo = false;
            this.txtServidorFTP.PermitirNegativos = false;
            this.txtServidorFTP.Size = new System.Drawing.Size(417, 20);
            this.txtServidorFTP.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Servidor :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmConfigurarConexionFTP_Catalogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 266);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameInfUnidad);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigurarConexionFTP_Catalogos";
            this.Text = "Configurar FTP para información de catálogos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigurarConexionFTP_Catalogos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInfUnidad.ResumeLayout(false);
            this.FrameInfUnidad.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuPassword.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private SC_ControlsCS.scTextBoxExt txtDirectorioDefault;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FrameInfUnidad;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private System.Windows.Forms.Label lblEstado02;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtPassFTP;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtUsuarioFTP;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtServidorFTP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuPassword;
        private System.Windows.Forms.ToolStripMenuItem encriptarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desencriptarToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnTestFTP;
    }
}