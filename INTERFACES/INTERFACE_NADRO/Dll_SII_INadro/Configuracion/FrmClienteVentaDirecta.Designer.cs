namespace Dll_SII_INadro.Configuracion
{
    partial class FrmClienteVentaDirecta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClienteVentaDirecta));
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCliente = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.FrameDatosPersonales = new System.Windows.Forms.GroupBox();
            this.txtDomicilio = new SC_ControlsCS.scTextBoxExt();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.txtClaveCliente = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.FrameEncabezado.SuspendLayout();
            this.FrameDatosPersonales.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.lblSubCliente);
            this.FrameEncabezado.Controls.Add(this.txtSubCliente);
            this.FrameEncabezado.Controls.Add(this.label5);
            this.FrameEncabezado.Controls.Add(this.lblCliente);
            this.FrameEncabezado.Controls.Add(this.txtCliente);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Location = new System.Drawing.Point(12, 28);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(515, 76);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Información general de operación : ";
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(186, 44);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(316, 21);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCliente
            // 
            this.txtSubCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCliente.Decimales = 2;
            this.txtSubCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCliente.ForeColor = System.Drawing.Color.Black;
            this.txtSubCliente.Location = new System.Drawing.Point(121, 44);
            this.txtSubCliente.MaxLength = 4;
            this.txtSubCliente.Name = "txtSubCliente";
            this.txtSubCliente.PermitirApostrofo = false;
            this.txtSubCliente.PermitirNegativos = false;
            this.txtSubCliente.Size = new System.Drawing.Size(59, 20);
            this.txtSubCliente.TabIndex = 1;
            this.txtSubCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(186, 19);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(316, 21);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(121, 19);
            this.txtCliente.MaxLength = 4;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(59, 20);
            this.txtCliente.TabIndex = 0;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatosPersonales
            // 
            this.FrameDatosPersonales.Controls.Add(this.txtClaveCliente);
            this.FrameDatosPersonales.Controls.Add(this.label4);
            this.FrameDatosPersonales.Controls.Add(this.txtDomicilio);
            this.FrameDatosPersonales.Controls.Add(this.lblDomicilio);
            this.FrameDatosPersonales.Controls.Add(this.lblCancelado);
            this.FrameDatosPersonales.Controls.Add(this.txtBeneficiario);
            this.FrameDatosPersonales.Controls.Add(this.txtNombre);
            this.FrameDatosPersonales.Controls.Add(this.label1);
            this.FrameDatosPersonales.Controls.Add(this.label2);
            this.FrameDatosPersonales.Location = new System.Drawing.Point(12, 106);
            this.FrameDatosPersonales.Name = "FrameDatosPersonales";
            this.FrameDatosPersonales.Size = new System.Drawing.Size(515, 162);
            this.FrameDatosPersonales.TabIndex = 2;
            this.FrameDatosPersonales.TabStop = false;
            this.FrameDatosPersonales.Text = "Datos Beneficiario :";
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDomicilio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDomicilio.Decimales = 2;
            this.txtDomicilio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDomicilio.ForeColor = System.Drawing.Color.Black;
            this.txtDomicilio.Location = new System.Drawing.Point(121, 105);
            this.txtDomicilio.MaxLength = 200;
            this.txtDomicilio.Multiline = true;
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.PermitirApostrofo = false;
            this.txtDomicilio.PermitirNegativos = false;
            this.txtDomicilio.Size = new System.Drawing.Size(381, 46);
            this.txtDomicilio.TabIndex = 3;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.Location = new System.Drawing.Point(12, 105);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(108, 13);
            this.lblDomicilio.TabIndex = 19;
            this.lblDomicilio.Text = "Domicilio :";
            this.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(220, 27);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(121, 23);
            this.txtBeneficiario.MaxLength = 8;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(93, 20);
            this.txtBeneficiario.TabIndex = 0;
            this.txtBeneficiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(121, 77);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(381, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Clave Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(536, 25);
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
            // txtClaveCliente
            // 
            this.txtClaveCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveCliente.Decimales = 2;
            this.txtClaveCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveCliente.ForeColor = System.Drawing.Color.Black;
            this.txtClaveCliente.Location = new System.Drawing.Point(121, 50);
            this.txtClaveCliente.MaxLength = 8;
            this.txtClaveCliente.Name = "txtClaveCliente";
            this.txtClaveCliente.PermitirApostrofo = false;
            this.txtClaveCliente.PermitirNegativos = false;
            this.txtClaveCliente.Size = new System.Drawing.Size(93, 20);
            this.txtClaveCliente.TabIndex = 1;
            this.txtClaveCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Clave Beneficiario :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmClienteVentaDirecta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 278);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.FrameDatosPersonales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmClienteVentaDirecta";
            this.Text = "Clientes para Venta Directa";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmClienteVentaDirecta_Load);
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.FrameDatosPersonales.ResumeLayout(false);
            this.FrameDatosPersonales.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCliente;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameDatosPersonales;
        private SC_ControlsCS.scTextBoxExt txtDomicilio;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private SC_ControlsCS.scTextBoxExt txtClaveCliente;
        private System.Windows.Forms.Label label4;
    }
}