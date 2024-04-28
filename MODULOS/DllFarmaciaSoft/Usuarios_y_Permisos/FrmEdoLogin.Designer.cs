namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmEdoLogin
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
            this.components = new System.ComponentModel.Container();
            this.FrameLogin = new System.Windows.Forms.GroupBox();
            this.lblAutenticando = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cboSucursales = new SC_ControlsCS.scComboBoxExt();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.txtUsuario = new SC_ControlsCS.scTextBoxExt();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.FrameLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameLogin
            // 
            this.FrameLogin.Controls.Add(this.lblAutenticando);
            this.FrameLogin.Controls.Add(this.btnCancelar);
            this.FrameLogin.Controls.Add(this.cboSucursales);
            this.FrameLogin.Controls.Add(this.btnAceptar);
            this.FrameLogin.Controls.Add(this.lblFarmacia);
            this.FrameLogin.Controls.Add(this.lblPassword);
            this.FrameLogin.Controls.Add(this.lblUsuario);
            this.FrameLogin.Controls.Add(this.txtPassword);
            this.FrameLogin.Controls.Add(this.txtUsuario);
            this.FrameLogin.Location = new System.Drawing.Point(12, 1);
            this.FrameLogin.Margin = new System.Windows.Forms.Padding(4);
            this.FrameLogin.Name = "FrameLogin";
            this.FrameLogin.Padding = new System.Windows.Forms.Padding(4);
            this.FrameLogin.Size = new System.Drawing.Size(422, 176);
            this.FrameLogin.TabIndex = 0;
            this.FrameLogin.TabStop = false;
            // 
            // lblAutenticando
            // 
            this.lblAutenticando.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutenticando.Location = new System.Drawing.Point(42, 247);
            this.lblAutenticando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutenticando.Name = "lblAutenticando";
            this.lblAutenticando.Size = new System.Drawing.Size(383, 24);
            this.lblAutenticando.TabIndex = 18;
            this.lblAutenticando.Text = "ACCESO DE USUARIO";
            this.lblAutenticando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.ForeColor = System.Drawing.Color.Red;
            this.btnCancelar.Location = new System.Drawing.Point(238, 128);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(137, 36);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "[-> Salir";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cboSucursales
            // 
            this.cboSucursales.BackColorEnabled = System.Drawing.Color.White;
            this.cboSucursales.Data = "";
            this.cboSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSucursales.Filtro = " 1 = 1";
            this.cboSucursales.ListaItemsBusqueda = 20;
            this.cboSucursales.Location = new System.Drawing.Point(22, 40);
            this.cboSucursales.Margin = new System.Windows.Forms.Padding(4);
            this.cboSucursales.MostrarToolTip = false;
            this.cboSucursales.Name = "cboSucursales";
            this.cboSucursales.Size = new System.Drawing.Size(380, 24);
            this.cboSucursales.TabIndex = 2;
            this.cboSucursales.SelectedIndexChanged += new System.EventHandler(this.cboSucursales_SelectedIndexChanged);
            // 
            // btnAceptar
            // 
            this.btnAceptar.ForeColor = System.Drawing.Color.Blue;
            this.btnAceptar.Location = new System.Drawing.Point(45, 128);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(137, 36);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "->] Entrar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.Location = new System.Drawing.Point(19, 20);
            this.lblFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(88, 16);
            this.lblFarmacia.TabIndex = 13;
            this.lblFarmacia.Text = "Unidad :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(219, 69);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(114, 22);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Contraseña :";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Location = new System.Drawing.Point(19, 69);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(98, 22);
            this.lblUsuario.TabIndex = 8;
            this.lblUsuario.Text = "Usuario :";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(222, 95);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.MaxLength = 40;
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(180, 25);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "0123456789012345678901234567890123456789";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUsuario
            // 
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUsuario.Decimales = 2;
            this.txtUsuario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUsuario.ForeColor = System.Drawing.Color.Black;
            this.txtUsuario.Location = new System.Drawing.Point(22, 95);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Multiline = true;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PermitirApostrofo = false;
            this.txtUsuario.PermitirNegativos = false;
            this.txtUsuario.Size = new System.Drawing.Size(180, 25);
            this.txtUsuario.TabIndex = 3;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(657, 60);
            this.lblEmpresa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(117, 16);
            this.lblEmpresa.TabIndex = 17;
            this.lblEmpresa.Text = "Compañia :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(779, 58);
            this.cboEmpresas.Margin = new System.Windows.Forms.Padding(4);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(380, 24);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(657, 91);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(117, 16);
            this.lblEstado.TabIndex = 15;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(779, 89);
            this.cboEstados.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(380, 24);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // FrmEdoLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 185);
            this.ControlBox = false;
            this.Controls.Add(this.FrameLogin);
            this.Controls.Add(this.lblEmpresa);
            this.Controls.Add(this.cboEmpresas);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.cboEstados);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmEdoLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
            this.Text = "Control de Acceso";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEdoLogin_FormClosing);
            this.Load += new System.EventHandler(this.FrmEdoLogin_Load);
            this.Shown += new System.EventHandler(this.FrmEdoLogin_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEdoLogin_KeyDown);
            this.FrameLogin.ResumeLayout(false);
            this.FrameLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameLogin;
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scComboBoxExt cboSucursales;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsuario;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private SC_ControlsCS.scTextBoxExt txtUsuario;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblEmpresa;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label lblAutenticando;
    }
}