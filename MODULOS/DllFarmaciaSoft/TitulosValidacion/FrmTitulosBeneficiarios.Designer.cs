namespace DllFarmaciaSoft.TitulosValidacion
{
    partial class FrmTitulosBeneficiarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTitulosBeneficiarios));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.chkReemplazarBeneficiario = new System.Windows.Forms.CheckBox();
            this.chkReemplazarReferencia = new System.Windows.Forms.CheckBox();
            this.txtFolioReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(623, 25);
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.chkActivo);
            this.FrameCliente.Controls.Add(this.chkReemplazarBeneficiario);
            this.FrameCliente.Controls.Add(this.chkReemplazarReferencia);
            this.FrameCliente.Controls.Add(this.txtFolioReferencia);
            this.FrameCliente.Controls.Add(this.label4);
            this.FrameCliente.Controls.Add(this.txtBeneficiario);
            this.FrameCliente.Controls.Add(this.label10);
            this.FrameCliente.Controls.Add(this.lblSubPro);
            this.FrameCliente.Controls.Add(this.txtSubPro);
            this.FrameCliente.Controls.Add(this.label7);
            this.FrameCliente.Controls.Add(this.lblPro);
            this.FrameCliente.Controls.Add(this.txtPro);
            this.FrameCliente.Controls.Add(this.label9);
            this.FrameCliente.Controls.Add(this.lblSubCte);
            this.FrameCliente.Controls.Add(this.txtSubCte);
            this.FrameCliente.Controls.Add(this.label1);
            this.FrameCliente.Controls.Add(this.lblCte);
            this.FrameCliente.Controls.Add(this.txtCte);
            this.FrameCliente.Controls.Add(this.label3);
            this.FrameCliente.Location = new System.Drawing.Point(12, 28);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(600, 225);
            this.FrameCliente.TabIndex = 0;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Datos Titulo Beneficiario";
            // 
            // chkActivo
            // 
            this.chkActivo.Location = new System.Drawing.Point(108, 196);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(96, 18);
            this.chkActivo.TabIndex = 53;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // chkReemplazarBeneficiario
            // 
            this.chkReemplazarBeneficiario.Location = new System.Drawing.Point(302, 172);
            this.chkReemplazarBeneficiario.Name = "chkReemplazarBeneficiario";
            this.chkReemplazarBeneficiario.Size = new System.Drawing.Size(188, 24);
            this.chkReemplazarBeneficiario.TabIndex = 8;
            this.chkReemplazarBeneficiario.Text = "Reemplazar beneficiario";
            this.chkReemplazarBeneficiario.UseVisualStyleBackColor = true;
            // 
            // chkReemplazarReferencia
            // 
            this.chkReemplazarReferencia.Location = new System.Drawing.Point(108, 172);
            this.chkReemplazarReferencia.Name = "chkReemplazarReferencia";
            this.chkReemplazarReferencia.Size = new System.Drawing.Size(188, 24);
            this.chkReemplazarReferencia.TabIndex = 7;
            this.chkReemplazarReferencia.Text = "Reemplazar folio de referencia";
            this.chkReemplazarReferencia.UseVisualStyleBackColor = true;
            // 
            // txtFolioReferencia
            // 
            this.txtFolioReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioReferencia.Decimales = 2;
            this.txtFolioReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtFolioReferencia.Location = new System.Drawing.Point(108, 120);
            this.txtFolioReferencia.MaxLength = 20;
            this.txtFolioReferencia.Multiline = true;
            this.txtFolioReferencia.Name = "txtFolioReferencia";
            this.txtFolioReferencia.PermitirApostrofo = false;
            this.txtFolioReferencia.PermitirNegativos = false;
            this.txtFolioReferencia.Size = new System.Drawing.Size(475, 20);
            this.txtFolioReferencia.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 14);
            this.label4.TabIndex = 52;
            this.label4.Text = "Folio referencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(108, 146);
            this.txtBeneficiario.MaxLength = 100;
            this.txtBeneficiario.Multiline = true;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(475, 20);
            this.txtBeneficiario.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "Titulo Beneficiario :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(173, 94);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(410, 21);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(108, 94);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(59, 20);
            this.txtSubPro.TabIndex = 4;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(173, 69);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(410, 21);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(108, 69);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(59, 20);
            this.txtPro.TabIndex = 3;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(173, 45);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(410, 21);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(108, 45);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(59, 20);
            this.txtSubCte.TabIndex = 2;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(173, 20);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(410, 21);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(108, 20);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(59, 20);
            this.txtCte.TabIndex = 1;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTitulosBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 261);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmTitulosBeneficiarios";
            this.Text = "Titulos beneficiarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTitulosBeneficiarios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameCliente;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private SC_ControlsCS.scTextBoxExt txtFolioReferencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkReemplazarReferencia;
        private System.Windows.Forms.CheckBox chkReemplazarBeneficiario;
        private System.Windows.Forms.CheckBox chkActivo;
    }
}