namespace DllFarmaciaSoft.Proveedores
{
    partial class FrmCfgAccesoProveedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCfgAccesoProveedores));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEncriptar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuPassword = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.encriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desencriptarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtPasswordCon = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogin = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNombrePersonal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnEncriptar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(485, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // btnEncriptar
            // 
            this.btnEncriptar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEncriptar.Image = ((System.Drawing.Image)(resources.GetObject("btnEncriptar.Image")));
            this.btnEncriptar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEncriptar.Name = "btnEncriptar";
            this.btnEncriptar.Size = new System.Drawing.Size(23, 22);
            this.btnEncriptar.Text = "Generar archivo";
            this.btnEncriptar.Click += new System.EventHandler(this.btnEncriptar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.ContextMenuStrip = this.contextMenuPassword;
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtPasswordCon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblNombrePersonal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIdProveedor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 147);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Proveedor";
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
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(368, 22);
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
            this.txtPasswordCon.Location = new System.Drawing.Point(123, 116);
            this.txtPasswordCon.MaxLength = 20;
            this.txtPasswordCon.Name = "txtPasswordCon";
            this.txtPasswordCon.PasswordChar = '*';
            this.txtPasswordCon.PermitirApostrofo = false;
            this.txtPasswordCon.PermitirNegativos = false;
            this.txtPasswordCon.Size = new System.Drawing.Size(217, 20);
            this.txtPasswordCon.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 119);
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
            this.txtPassword.Location = new System.Drawing.Point(123, 92);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(217, 20);
            this.txtPassword.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(52, 95);
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
            this.txtLogin.Location = new System.Drawing.Point(123, 66);
            this.txtLogin.MaxLength = 20;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.PermitirApostrofo = false;
            this.txtLogin.PermitirNegativos = false;
            this.txtLogin.Size = new System.Drawing.Size(217, 20);
            this.txtLogin.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(75, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "Login :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombrePersonal
            // 
            this.lblNombrePersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombrePersonal.Location = new System.Drawing.Point(123, 45);
            this.lblNombrePersonal.Name = "lblNombrePersonal";
            this.lblNombrePersonal.Size = new System.Drawing.Size(326, 18);
            this.lblNombrePersonal.TabIndex = 25;
            this.lblNombrePersonal.Text = "Estado :";
            this.lblNombrePersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(67, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Nombre :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(123, 19);
            this.txtIdProveedor.MaxLength = 4;
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(78, 20);
            this.txtIdProveedor.TabIndex = 8;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_KeyDown);
            this.txtIdProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(49, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Id. Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCfgAccesoProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 184);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCfgAccesoProveedores";
            this.Text = "Configurar Usuario Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
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
        private System.Windows.Forms.ToolStripButton btnEncriptar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtPasswordCon;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNombrePersonal;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuPassword;
        private System.Windows.Forms.ToolStripMenuItem encriptarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desencriptarToolStripMenuItem;
    }
}