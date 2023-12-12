namespace DllFarmaciaSoft.Usuarios_y_Permisos_Clientes
{
    partial class Frm_Ctes_UsuariosRegional
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Ctes_UsuariosRegional));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuPassword = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.encriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desencriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtPasswordCon = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogin = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEstado01 = new System.Windows.Forms.Label();
            this.txtIdUsuario = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.lblEmpresa01 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
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
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(577, 25);
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.ContextMenuStrip = this.contextMenuPassword;
            this.groupBox1.Controls.Add(this.cboFarmacias);
            this.groupBox1.Controls.Add(this.lblEmpresa01);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtPasswordCon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblEstado01);
            this.groupBox1.Controls.Add(this.txtIdUsuario);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 211);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Usuario";
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
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 0;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(125, 98);
            this.txtNombre.MaxLength = 200;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(232, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(125, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(424, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(209, 76);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtPasswordCon
            // 
            this.txtPasswordCon.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPasswordCon.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasswordCon.Decimales = 2;
            this.txtPasswordCon.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPasswordCon.ForeColor = System.Drawing.Color.Black;
            this.txtPasswordCon.Location = new System.Drawing.Point(125, 174);
            this.txtPasswordCon.MaxLength = 20;
            this.txtPasswordCon.Name = "txtPasswordCon";
            this.txtPasswordCon.PasswordChar = '*';
            this.txtPasswordCon.PermitirApostrofo = false;
            this.txtPasswordCon.PermitirNegativos = false;
            this.txtPasswordCon.Size = new System.Drawing.Size(232, 20);
            this.txtPasswordCon.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 15);
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
            this.txtPassword.Location = new System.Drawing.Point(125, 150);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(232, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 15);
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
            this.txtLogin.Location = new System.Drawing.Point(125, 124);
            this.txtLogin.MaxLength = 20;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.PermitirApostrofo = false;
            this.txtLogin.PermitirNegativos = false;
            this.txtLogin.Size = new System.Drawing.Size(232, 20);
            this.txtLogin.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Login :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Nombre :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado01
            // 
            this.lblEstado01.Location = new System.Drawing.Point(13, 20);
            this.lblEstado01.Name = "lblEstado01";
            this.lblEstado01.Size = new System.Drawing.Size(109, 17);
            this.lblEstado01.TabIndex = 21;
            this.lblEstado01.Text = "Estado :";
            this.lblEstado01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdUsuario
            // 
            this.txtIdUsuario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdUsuario.Decimales = 2;
            this.txtIdUsuario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdUsuario.ForeColor = System.Drawing.Color.Black;
            this.txtIdUsuario.Location = new System.Drawing.Point(125, 73);
            this.txtIdUsuario.MaxLength = 4;
            this.txtIdUsuario.Name = "txtIdUsuario";
            this.txtIdUsuario.PermitirApostrofo = false;
            this.txtIdUsuario.PermitirNegativos = false;
            this.txtIdUsuario.Size = new System.Drawing.Size(78, 20);
            this.txtIdUsuario.TabIndex = 2;
            this.txtIdUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPersonal_KeyDown);
            this.txtIdUsuario.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonal_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Id. Usuario :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(125, 46);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(424, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // lblEmpresa01
            // 
            this.lblEmpresa01.Location = new System.Drawing.Point(13, 47);
            this.lblEmpresa01.Name = "lblEmpresa01";
            this.lblEmpresa01.Size = new System.Drawing.Size(109, 17);
            this.lblEmpresa01.TabIndex = 34;
            this.lblEmpresa01.Text = "Farmacia :";
            this.lblEmpresa01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Frm_Ctes_UsuariosRegional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 246);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Ctes_UsuariosRegional";
            this.Text = "Registro de Usuarios Regionales de sistema";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.Frm_Ctes_UsuariosRegional_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstado01;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtLogin;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtPasswordCon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.ContextMenuStrip contextMenuPassword;
        private System.Windows.Forms.ToolStripMenuItem encriptarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desencriptarToolStripMenuItem;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label lblEmpresa01;
    }
}