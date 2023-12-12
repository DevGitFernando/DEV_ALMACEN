namespace Configuracion.Configuracion
{
    partial class FrmConfigurarDistribuidores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarDistribuidores));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.txtPaginaWeb = new SC_ControlsCS.scTextBoxExt();
            this.txtWebService = new SC_ControlsCS.scTextBoxExt();
            this.txtServidor = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.chkDistribuidor = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdDistribuidor = new SC_ControlsCS.scTextBoxExt();
            this.FrameCSGN = new System.Windows.Forms.GroupBox();
            this.txtCodigoCliente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDistribuidor = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameCSGN.SuspendLayout();
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
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(403, 25);
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
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.txtPaginaWeb);
            this.FrameDatos.Controls.Add(this.txtWebService);
            this.FrameDatos.Controls.Add(this.txtServidor);
            this.FrameDatos.Controls.Add(this.txtNombre);
            this.FrameDatos.Controls.Add(this.chkDistribuidor);
            this.FrameDatos.Controls.Add(this.label7);
            this.FrameDatos.Controls.Add(this.label6);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Location = new System.Drawing.Point(12, 163);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(378, 132);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos Unidad";
            // 
            // txtPaginaWeb
            // 
            this.txtPaginaWeb.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPaginaWeb.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPaginaWeb.Decimales = 2;
            this.txtPaginaWeb.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPaginaWeb.ForeColor = System.Drawing.Color.Black;
            this.txtPaginaWeb.Location = new System.Drawing.Point(91, 97);
            this.txtPaginaWeb.MaxLength = 100;
            this.txtPaginaWeb.Name = "txtPaginaWeb";
            this.txtPaginaWeb.PermitirApostrofo = false;
            this.txtPaginaWeb.PermitirNegativos = false;
            this.txtPaginaWeb.Size = new System.Drawing.Size(167, 20);
            this.txtPaginaWeb.TabIndex = 3;
            // 
            // txtWebService
            // 
            this.txtWebService.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWebService.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtWebService.Decimales = 2;
            this.txtWebService.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtWebService.ForeColor = System.Drawing.Color.Black;
            this.txtWebService.Location = new System.Drawing.Point(91, 71);
            this.txtWebService.MaxLength = 100;
            this.txtWebService.Name = "txtWebService";
            this.txtWebService.PermitirApostrofo = false;
            this.txtWebService.PermitirNegativos = false;
            this.txtWebService.Size = new System.Drawing.Size(274, 20);
            this.txtWebService.TabIndex = 2;
            // 
            // txtServidor
            // 
            this.txtServidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtServidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServidor.Decimales = 2;
            this.txtServidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtServidor.ForeColor = System.Drawing.Color.Black;
            this.txtServidor.Location = new System.Drawing.Point(91, 45);
            this.txtServidor.MaxLength = 100;
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.PermitirApostrofo = false;
            this.txtServidor.PermitirNegativos = false;
            this.txtServidor.Size = new System.Drawing.Size(274, 20);
            this.txtServidor.TabIndex = 1;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(91, 19);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(274, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // chkDistribuidor
            // 
            this.chkDistribuidor.AutoSize = true;
            this.chkDistribuidor.Location = new System.Drawing.Point(272, 99);
            this.chkDistribuidor.Name = "chkDistribuidor";
            this.chkDistribuidor.Size = new System.Drawing.Size(93, 17);
            this.chkDistribuidor.TabIndex = 4;
            this.chkDistribuidor.Text = "Es Distribuidor";
            this.chkDistribuidor.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Pagina Web :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "WebService :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Servidor :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Distribuidor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdDistribuidor
            // 
            this.txtIdDistribuidor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdDistribuidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDistribuidor.Decimales = 2;
            this.txtIdDistribuidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdDistribuidor.ForeColor = System.Drawing.Color.Black;
            this.txtIdDistribuidor.Location = new System.Drawing.Point(91, 73);
            this.txtIdDistribuidor.MaxLength = 4;
            this.txtIdDistribuidor.Name = "txtIdDistribuidor";
            this.txtIdDistribuidor.PermitirApostrofo = false;
            this.txtIdDistribuidor.PermitirNegativos = false;
            this.txtIdDistribuidor.Size = new System.Drawing.Size(51, 20);
            this.txtIdDistribuidor.TabIndex = 2;
            this.txtIdDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDistribuidor.TextChanged += new System.EventHandler(this.txtIdDistribuidor_TextChanged);
            this.txtIdDistribuidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtIdDistribuidor.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // FrameCSGN
            // 
            this.FrameCSGN.Controls.Add(this.txtCodigoCliente);
            this.FrameCSGN.Controls.Add(this.label3);
            this.FrameCSGN.Controls.Add(this.lblDistribuidor);
            this.FrameCSGN.Controls.Add(this.lblCancelado);
            this.FrameCSGN.Controls.Add(this.label1);
            this.FrameCSGN.Controls.Add(this.label5);
            this.FrameCSGN.Controls.Add(this.txtIdDistribuidor);
            this.FrameCSGN.Controls.Add(this.cboFarmacias);
            this.FrameCSGN.Controls.Add(this.lblEstado);
            this.FrameCSGN.Controls.Add(this.cboEstados);
            this.FrameCSGN.Location = new System.Drawing.Point(12, 28);
            this.FrameCSGN.Name = "FrameCSGN";
            this.FrameCSGN.Size = new System.Drawing.Size(378, 129);
            this.FrameCSGN.TabIndex = 0;
            this.FrameCSGN.TabStop = false;
            this.FrameCSGN.Text = "Unidad de Consginación";
            // 
            // txtCodigoCliente
            // 
            this.txtCodigoCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoCliente.Decimales = 2;
            this.txtCodigoCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigoCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoCliente.Location = new System.Drawing.Point(91, 99);
            this.txtCodigoCliente.MaxLength = 20;
            this.txtCodigoCliente.Name = "txtCodigoCliente";
            this.txtCodigoCliente.PermitirApostrofo = false;
            this.txtCodigoCliente.PermitirNegativos = false;
            this.txtCodigoCliente.Size = new System.Drawing.Size(274, 20);
            this.txtCodigoCliente.TabIndex = 3;
            this.txtCodigoCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoCliente_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Codigo Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistribuidor
            // 
            this.lblDistribuidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistribuidor.Location = new System.Drawing.Point(148, 73);
            this.lblDistribuidor.Name = "lblDistribuidor";
            this.lblDistribuidor.Size = new System.Drawing.Size(217, 21);
            this.lblDistribuidor.TabIndex = 38;
            this.lblDistribuidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(285, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(80, 13);
            this.lblCancelado.TabIndex = 17;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.Visible = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Farmacia :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.Location = new System.Drawing.Point(91, 46);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(274, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(32, 20);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(56, 17);
            this.lblEstado.TabIndex = 25;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.Location = new System.Drawing.Point(91, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(274, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // FrmConfigurarDistribuidores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 309);
            this.Controls.Add(this.FrameCSGN);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigurarDistribuidores";
            this.Text = "Configurar Codigo Cliente";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigurarDistribuidores_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.FrameCSGN.ResumeLayout(false);
            this.FrameCSGN.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtIdDistribuidor;
        private System.Windows.Forms.GroupBox FrameCSGN;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.Label lblDistribuidor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkDistribuidor;
        private SC_ControlsCS.scTextBoxExt txtPaginaWeb;
        private SC_ControlsCS.scTextBoxExt txtWebService;
        private SC_ControlsCS.scTextBoxExt txtServidor;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private SC_ControlsCS.scTextBoxExt txtCodigoCliente;

    }
}