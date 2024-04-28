namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmUsuariosPasswordsStandard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsuariosPasswordsStandard));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_02 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPassword = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_03 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado01 = new System.Windows.Forms.Label();
            this.lblEmpresa01 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.lblFarmaciaxx = new System.Windows.Forms.Label();
            this.lblAvance = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalUsuarios = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.rdoSinPassword = new System.Windows.Forms.RadioButton();
            this.txtFarmaciaFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFarmaciaInicial = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator_01,
            this.btnEjecutar,
            this.toolStripSeparator_02,
            this.btnGenerarPassword,
            this.toolStripSeparator_03,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(486, 25);
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
            // toolStripSeparator_01
            // 
            this.toolStripSeparator_01.Name = "toolStripSeparator_01";
            this.toolStripSeparator_01.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator_02
            // 
            this.toolStripSeparator_02.Name = "toolStripSeparator_02";
            this.toolStripSeparator_02.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarPassword
            // 
            this.btnGenerarPassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPassword.Image")));
            this.btnGenerarPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPassword.Name = "btnGenerarPassword";
            this.btnGenerarPassword.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPassword.Text = "Generar passwords";
            this.btnGenerarPassword.Click += new System.EventHandler(this.btnGenerarPassword_Click);
            // 
            // toolStripSeparator_03
            // 
            this.toolStripSeparator_03.Name = "toolStripSeparator_03";
            this.toolStripSeparator_03.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFarmaciaFinal);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtFarmaciaInicial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboFarmacias);
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.lblEstado01);
            this.groupBox1.Controls.Add(this.lblEmpresa01);
            this.groupBox1.Location = new System.Drawing.Point(8, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 113);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Usuario";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(80, 52);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(373, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(80, 24);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(373, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblEstado01
            // 
            this.lblEstado01.Location = new System.Drawing.Point(27, 25);
            this.lblEstado01.Name = "lblEstado01";
            this.lblEstado01.Size = new System.Drawing.Size(51, 17);
            this.lblEstado01.TabIndex = 21;
            this.lblEstado01.Text = "Estado :";
            this.lblEstado01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmpresa01
            // 
            this.lblEmpresa01.Location = new System.Drawing.Point(19, 52);
            this.lblEmpresa01.Name = "lblEmpresa01";
            this.lblEmpresa01.Size = new System.Drawing.Size(59, 17);
            this.lblEmpresa01.TabIndex = 20;
            this.lblEmpresa01.Text = "Farmacia :";
            this.lblEmpresa01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFarmacia);
            this.groupBox2.Controls.Add(this.lblFarmaciaxx);
            this.groupBox2.Controls.Add(this.lblAvance);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblNombreUsuario);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotalUsuarios);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 102);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(80, 49);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(373, 17);
            this.lblFarmacia.TabIndex = 28;
            this.lblFarmacia.Text = "Usuarios :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFarmaciaxx
            // 
            this.lblFarmaciaxx.Location = new System.Drawing.Point(6, 49);
            this.lblFarmaciaxx.Name = "lblFarmaciaxx";
            this.lblFarmaciaxx.Size = new System.Drawing.Size(74, 17);
            this.lblFarmaciaxx.TabIndex = 27;
            this.lblFarmaciaxx.Text = "Farmacia :";
            this.lblFarmaciaxx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAvance
            // 
            this.lblAvance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAvance.Location = new System.Drawing.Point(322, 25);
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(131, 17);
            this.lblAvance.TabIndex = 26;
            this.lblAvance.Text = "Usuarios :";
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(263, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 25;
            this.label4.Text = "Avance :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreUsuario.Location = new System.Drawing.Point(80, 74);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(373, 17);
            this.lblNombreUsuario.TabIndex = 24;
            this.lblNombreUsuario.Text = "Usuarios :";
            this.lblNombreUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Nombre :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalUsuarios
            // 
            this.lblTotalUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalUsuarios.Location = new System.Drawing.Point(80, 25);
            this.lblTotalUsuarios.Name = "lblTotalUsuarios";
            this.lblTotalUsuarios.Size = new System.Drawing.Size(131, 17);
            this.lblTotalUsuarios.TabIndex = 22;
            this.lblTotalUsuarios.Text = "Usuarios :";
            this.lblTotalUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Usuarios :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoTodos);
            this.groupBox3.Controls.Add(this.rdoSinPassword);
            this.groupBox3.Location = new System.Drawing.Point(8, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(468, 52);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos del ticket";
            // 
            // rdoTodos
            // 
            this.rdoTodos.Location = new System.Drawing.Point(263, 19);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(111, 17);
            this.rdoTodos.TabIndex = 3;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // rdoSinPassword
            // 
            this.rdoSinPassword.Location = new System.Drawing.Point(94, 19);
            this.rdoSinPassword.Name = "rdoSinPassword";
            this.rdoSinPassword.Size = new System.Drawing.Size(128, 17);
            this.rdoSinPassword.TabIndex = 2;
            this.rdoSinPassword.Text = "Sin Contraseña";
            this.rdoSinPassword.UseVisualStyleBackColor = true;
            // 
            // txtFarmaciaFinal
            // 
            this.txtFarmaciaFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmaciaFinal.Decimales = 2;
            this.txtFarmaciaFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmaciaFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFarmaciaFinal.Location = new System.Drawing.Point(222, 79);
            this.txtFarmaciaFinal.MaxLength = 4;
            this.txtFarmaciaFinal.Name = "txtFarmaciaFinal";
            this.txtFarmaciaFinal.PermitirApostrofo = false;
            this.txtFarmaciaFinal.PermitirNegativos = false;
            this.txtFarmaciaFinal.Size = new System.Drawing.Size(85, 20);
            this.txtFarmaciaFinal.TabIndex = 3;
            this.txtFarmaciaFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(172, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 39;
            this.label6.Text = "Hasta :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFarmaciaInicial
            // 
            this.txtFarmaciaInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmaciaInicial.Decimales = 2;
            this.txtFarmaciaInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmaciaInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFarmaciaInicial.Location = new System.Drawing.Point(80, 79);
            this.txtFarmaciaInicial.MaxLength = 4;
            this.txtFarmaciaInicial.Name = "txtFarmaciaInicial";
            this.txtFarmaciaInicial.PermitirApostrofo = false;
            this.txtFarmaciaInicial.PermitirNegativos = false;
            this.txtFarmaciaInicial.Size = new System.Drawing.Size(85, 20);
            this.txtFarmaciaInicial.TabIndex = 2;
            this.txtFarmaciaInicial.Text = "01234567";
            this.txtFarmaciaInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "Desde :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmUsuariosPasswordsStandard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 302);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmUsuariosPasswordsStandard";
            this.Text = "Registro de Usuarios de sistema";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmUsuariosPasswordsStandard_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_01;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_03;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEstado01;
        private System.Windows.Forms.Label lblEmpresa01;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.ToolStripButton btnGenerarPassword;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalUsuarios;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAvance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFarmacia;
        private System.Windows.Forms.Label lblFarmaciaxx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_02;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.RadioButton rdoSinPassword;
        private SC_ControlsCS.scTextBoxExt txtFarmaciaFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFarmaciaInicial;
        private System.Windows.Forms.Label label2;
    }
}