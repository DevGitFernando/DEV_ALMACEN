namespace Dll_MA_IFacturacion.Catalogos
{
    partial class FrmEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmail));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnProveedorEMail = new System.Windows.Forms.Button();
            this.cboProveedoresEMail = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTiposEmail = new System.Windows.Forms.Button();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.lblTipoCorreo = new SC_ControlsCS.scLabelExt();
            this.txtIdTipoCorreo = new SC_ControlsCS.scTextBoxExt();
            this.txtCorreo = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProveedorEMail);
            this.groupBox1.Controls.Add(this.cboProveedoresEMail);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnTiposEmail);
            this.groupBox1.Controls.Add(this.chkActivo);
            this.groupBox1.Controls.Add(this.lblTipoCorreo);
            this.groupBox1.Controls.Add(this.txtIdTipoCorreo);
            this.groupBox1.Controls.Add(this.txtCorreo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(10, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 120);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnProveedorEMail
            // 
            this.btnProveedorEMail.Location = new System.Drawing.Point(453, 68);
            this.btnProveedorEMail.Name = "btnProveedorEMail";
            this.btnProveedorEMail.Size = new System.Drawing.Size(26, 22);
            this.btnProveedorEMail.TabIndex = 73;
            this.btnProveedorEMail.Text = "...";
            this.btnProveedorEMail.UseVisualStyleBackColor = true;
            this.btnProveedorEMail.Click += new System.EventHandler(this.btnProveedorEMail_Click);
            // 
            // cboProveedoresEMail
            // 
            this.cboProveedoresEMail.BackColorEnabled = System.Drawing.Color.White;
            this.cboProveedoresEMail.Data = "";
            this.cboProveedoresEMail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProveedoresEMail.Filtro = " 1 = 1";
            this.cboProveedoresEMail.FormattingEnabled = true;
            this.cboProveedoresEMail.ListaItemsBusqueda = 20;
            this.cboProveedoresEMail.Location = new System.Drawing.Point(101, 70);
            this.cboProveedoresEMail.MostrarToolTip = false;
            this.cboProveedoresEMail.Name = "cboProveedoresEMail";
            this.cboProveedoresEMail.Size = new System.Drawing.Size(346, 21);
            this.cboProveedoresEMail.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Proveedor Email :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTiposEmail
            // 
            this.btnTiposEmail.Location = new System.Drawing.Point(453, 17);
            this.btnTiposEmail.Name = "btnTiposEmail";
            this.btnTiposEmail.Size = new System.Drawing.Size(26, 22);
            this.btnTiposEmail.TabIndex = 70;
            this.btnTiposEmail.Text = "...";
            this.btnTiposEmail.UseVisualStyleBackColor = true;
            this.btnTiposEmail.Click += new System.EventHandler(this.btnTiposEmail_Click);
            // 
            // chkActivo
            // 
            this.chkActivo.Location = new System.Drawing.Point(101, 95);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(104, 20);
            this.chkActivo.TabIndex = 5;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // lblTipoCorreo
            // 
            this.lblTipoCorreo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoCorreo.Location = new System.Drawing.Point(152, 18);
            this.lblTipoCorreo.MostrarToolTip = false;
            this.lblTipoCorreo.Name = "lblTipoCorreo";
            this.lblTipoCorreo.Size = new System.Drawing.Size(295, 21);
            this.lblTipoCorreo.TabIndex = 54;
            this.lblTipoCorreo.Text = "scLabelExt1";
            this.lblTipoCorreo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdTipoCorreo
            // 
            this.txtIdTipoCorreo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdTipoCorreo.Decimales = 2;
            this.txtIdTipoCorreo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdTipoCorreo.ForeColor = System.Drawing.Color.Black;
            this.txtIdTipoCorreo.Location = new System.Drawing.Point(101, 18);
            this.txtIdTipoCorreo.Name = "txtIdTipoCorreo";
            this.txtIdTipoCorreo.PermitirApostrofo = false;
            this.txtIdTipoCorreo.PermitirNegativos = false;
            this.txtIdTipoCorreo.Size = new System.Drawing.Size(47, 20);
            this.txtIdTipoCorreo.TabIndex = 3;
            this.txtIdTipoCorreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdTipoCorreo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdTipoCorreo_KeyDown);
            this.txtIdTipoCorreo.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdTipoCorreo_Validating);
            // 
            // txtCorreo
            // 
            this.txtCorreo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCorreo.Decimales = 2;
            this.txtCorreo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCorreo.ForeColor = System.Drawing.Color.Black;
            this.txtCorreo.Location = new System.Drawing.Point(101, 44);
            this.txtCorreo.MaxLength = 100;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.PermitirApostrofo = false;
            this.txtCorreo.PermitirNegativos = false;
            this.txtCorreo.Size = new System.Drawing.Size(378, 20);
            this.txtCorreo.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(5, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Correo :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tipo de Correo :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(514, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // FrmEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 155);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmEmail";
            this.Text = "Email";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEmail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt lblTipoCorreo;
        private SC_ControlsCS.scTextBoxExt txtIdTipoCorreo;
        private SC_ControlsCS.scTextBoxExt txtCorreo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Button btnTiposEmail;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboProveedoresEMail;
        private System.Windows.Forms.Button btnProveedorEMail;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}