namespace Farmacia.Catalogos
{
    partial class FrmBeneficiarios_Domicilios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBeneficiarios_Domicilios));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosCliente = new System.Windows.Forms.GroupBox();
            this.lblNombre = new SC_ControlsCS.scLabelExt();
            this.lblColonia = new SC_ControlsCS.scLabelExt();
            this.txtIdColonia = new SC_ControlsCS.scTextBoxExt();
            this.lblMunicipio = new SC_ControlsCS.scLabelExt();
            this.txtIdMunicipio = new SC_ControlsCS.scTextBoxExt();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.txtTelefonos = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCodigoPostal = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomicilio = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(627, 25);
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
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrameDatosCliente
            // 
            this.FrameDatosCliente.Controls.Add(this.lblNombre);
            this.FrameDatosCliente.Controls.Add(this.lblColonia);
            this.FrameDatosCliente.Controls.Add(this.txtIdColonia);
            this.FrameDatosCliente.Controls.Add(this.lblMunicipio);
            this.FrameDatosCliente.Controls.Add(this.txtIdMunicipio);
            this.FrameDatosCliente.Controls.Add(this.txtReferencia);
            this.FrameDatosCliente.Controls.Add(this.label1);
            this.FrameDatosCliente.Controls.Add(this.lblEstado);
            this.FrameDatosCliente.Controls.Add(this.txtIdEstado);
            this.FrameDatosCliente.Controls.Add(this.txtTelefonos);
            this.FrameDatosCliente.Controls.Add(this.label9);
            this.FrameDatosCliente.Controls.Add(this.txtCodigoPostal);
            this.FrameDatosCliente.Controls.Add(this.label8);
            this.FrameDatosCliente.Controls.Add(this.txtDomicilio);
            this.FrameDatosCliente.Controls.Add(this.label7);
            this.FrameDatosCliente.Controls.Add(this.label6);
            this.FrameDatosCliente.Controls.Add(this.label4);
            this.FrameDatosCliente.Controls.Add(this.label5);
            this.FrameDatosCliente.Controls.Add(this.label2);
            this.FrameDatosCliente.Location = new System.Drawing.Point(12, 28);
            this.FrameDatosCliente.Name = "FrameDatosCliente";
            this.FrameDatosCliente.Size = new System.Drawing.Size(607, 190);
            this.FrameDatosCliente.TabIndex = 1;
            this.FrameDatosCliente.TabStop = false;
            this.FrameDatosCliente.Text = "Datos de Domicilio";
            // 
            // lblNombre
            // 
            this.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombre.Location = new System.Drawing.Point(108, 17);
            this.lblNombre.MostrarToolTip = false;
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(484, 18);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "scLabelExt4";
            // 
            // lblColonia
            // 
            this.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColonia.Location = new System.Drawing.Point(178, 88);
            this.lblColonia.MostrarToolTip = false;
            this.lblColonia.Name = "lblColonia";
            this.lblColonia.Size = new System.Drawing.Size(414, 18);
            this.lblColonia.TabIndex = 6;
            this.lblColonia.Text = "scLabelExt3";
            // 
            // txtIdColonia
            // 
            this.txtIdColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdColonia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdColonia.Decimales = 2;
            this.txtIdColonia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdColonia.ForeColor = System.Drawing.Color.Black;
            this.txtIdColonia.Location = new System.Drawing.Point(108, 87);
            this.txtIdColonia.MaxLength = 4;
            this.txtIdColonia.Name = "txtIdColonia";
            this.txtIdColonia.PermitirApostrofo = false;
            this.txtIdColonia.PermitirNegativos = false;
            this.txtIdColonia.Size = new System.Drawing.Size(64, 20);
            this.txtIdColonia.TabIndex = 5;
            this.txtIdColonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdColonia.TextChanged += new System.EventHandler(this.txtIdColonia_TextChanged);
            this.txtIdColonia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdColonia_KeyDown);
            this.txtIdColonia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdColonia_Validating);
            // 
            // lblMunicipio
            // 
            this.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMunicipio.Location = new System.Drawing.Point(178, 64);
            this.lblMunicipio.MostrarToolTip = false;
            this.lblMunicipio.Name = "lblMunicipio";
            this.lblMunicipio.Size = new System.Drawing.Size(414, 18);
            this.lblMunicipio.TabIndex = 4;
            this.lblMunicipio.Text = "scLabelExt2";
            // 
            // txtIdMunicipio
            // 
            this.txtIdMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdMunicipio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdMunicipio.Decimales = 2;
            this.txtIdMunicipio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdMunicipio.ForeColor = System.Drawing.Color.Black;
            this.txtIdMunicipio.Location = new System.Drawing.Point(108, 63);
            this.txtIdMunicipio.MaxLength = 4;
            this.txtIdMunicipio.Name = "txtIdMunicipio";
            this.txtIdMunicipio.PermitirApostrofo = false;
            this.txtIdMunicipio.PermitirNegativos = false;
            this.txtIdMunicipio.Size = new System.Drawing.Size(64, 20);
            this.txtIdMunicipio.TabIndex = 3;
            this.txtIdMunicipio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdMunicipio.TextChanged += new System.EventHandler(this.txtIdMunicipio_TextChanged);
            this.txtIdMunicipio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdMunicipio_KeyDown);
            this.txtIdMunicipio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdMunicipio_Validating);
            // 
            // txtReferencia
            // 
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(108, 136);
            this.txtReferencia.MaxLength = 100;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(484, 20);
            this.txtReferencia.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Referencia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(178, 40);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(414, 18);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "scLabelExt1";
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(108, 39);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(64, 20);
            this.txtIdEstado.TabIndex = 1;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // txtTelefonos
            // 
            this.txtTelefonos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelefonos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTelefonos.Decimales = 2;
            this.txtTelefonos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTelefonos.ForeColor = System.Drawing.Color.Black;
            this.txtTelefonos.Location = new System.Drawing.Point(332, 160);
            this.txtTelefonos.MaxLength = 30;
            this.txtTelefonos.Name = "txtTelefonos";
            this.txtTelefonos.PermitirApostrofo = false;
            this.txtTelefonos.PermitirNegativos = false;
            this.txtTelefonos.Size = new System.Drawing.Size(260, 20);
            this.txtTelefonos.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(258, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Telefonos :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoPostal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoPostal.Decimales = 2;
            this.txtCodigoPostal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoPostal.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoPostal.Location = new System.Drawing.Point(108, 160);
            this.txtCodigoPostal.MaxLength = 10;
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.PermitirApostrofo = false;
            this.txtCodigoPostal.PermitirNegativos = false;
            this.txtCodigoPostal.Size = new System.Drawing.Size(118, 20);
            this.txtCodigoPostal.TabIndex = 9;
            this.txtCodigoPostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(19, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Código Postal :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDomicilio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDomicilio.Decimales = 2;
            this.txtDomicilio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDomicilio.ForeColor = System.Drawing.Color.Black;
            this.txtDomicilio.Location = new System.Drawing.Point(108, 112);
            this.txtDomicilio.MaxLength = 100;
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.PermitirApostrofo = false;
            this.txtDomicilio.PermitirNegativos = false;
            this.txtDomicilio.Size = new System.Drawing.Size(484, 20);
            this.txtDomicilio.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Domicilio :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(19, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Colonia :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Municipio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmBeneficiarios_Domicilios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 228);
            this.Controls.Add(this.FrameDatosCliente);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmBeneficiarios_Domicilios";
            this.Text = "Domicilio de Beneficiario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmBeneficiarios_Domicilios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosCliente.ResumeLayout(false);
            this.FrameDatosCliente.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameDatosCliente;
        private SC_ControlsCS.scTextBoxExt txtTelefonos;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtCodigoPostal;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtDomicilio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblColonia;
        private SC_ControlsCS.scTextBoxExt txtIdColonia;
        private SC_ControlsCS.scLabelExt lblMunicipio;
        private SC_ControlsCS.scTextBoxExt txtIdMunicipio;
        private SC_ControlsCS.scLabelExt lblNombre;
    }
}