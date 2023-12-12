namespace Almacen.PerfilesAtencion
{
    partial class FrmGruposPerfilesAtencion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGruposPerfilesAtencion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosGral = new System.Windows.Forms.GroupBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.txtDesc = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.txtPerfilAtencion = new SC_ControlsCS.scTextBoxExt();
            this.lblSubPrograma = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosGral.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.btnCancelar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(602, 25);
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
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
            // FrameDatosGral
            // 
            this.FrameDatosGral.Controls.Add(this.lblCancelado);
            this.FrameDatosGral.Controls.Add(this.txtSubPro);
            this.FrameDatosGral.Controls.Add(this.txtPro);
            this.FrameDatosGral.Controls.Add(this.txtSubCte);
            this.FrameDatosGral.Controls.Add(this.txtDesc);
            this.FrameDatosGral.Controls.Add(this.label2);
            this.FrameDatosGral.Controls.Add(this.txtCte);
            this.FrameDatosGral.Controls.Add(this.txtPerfilAtencion);
            this.FrameDatosGral.Controls.Add(this.lblSubPrograma);
            this.FrameDatosGral.Controls.Add(this.label4);
            this.FrameDatosGral.Controls.Add(this.lblPrograma);
            this.FrameDatosGral.Controls.Add(this.label8);
            this.FrameDatosGral.Controls.Add(this.label9);
            this.FrameDatosGral.Controls.Add(this.lblSubCliente);
            this.FrameDatosGral.Controls.Add(this.lblCliente);
            this.FrameDatosGral.Controls.Add(this.label3);
            this.FrameDatosGral.Controls.Add(this.label1);
            this.FrameDatosGral.Location = new System.Drawing.Point(9, 24);
            this.FrameDatosGral.Name = "FrameDatosGral";
            this.FrameDatosGral.Size = new System.Drawing.Size(586, 188);
            this.FrameDatosGral.TabIndex = 8;
            this.FrameDatosGral.TabStop = false;
            this.FrameDatosGral.Text = "Grupos Perfiles de Atención";
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(176, 25);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 52;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(100, 155);
            this.txtSubPro.MaxLength = 6;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(71, 20);
            this.txtSubPro.TabIndex = 5;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(100, 129);
            this.txtPro.MaxLength = 6;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(71, 20);
            this.txtPro.TabIndex = 4;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(100, 102);
            this.txtSubCte.MaxLength = 6;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(71, 20);
            this.txtSubCte.TabIndex = 3;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // txtDesc
            // 
            this.txtDesc.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDesc.Decimales = 2;
            this.txtDesc.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDesc.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Location = new System.Drawing.Point(100, 51);
            this.txtDesc.MaxLength = 100;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.PermitirApostrofo = false;
            this.txtDesc.PermitirNegativos = false;
            this.txtDesc.Size = new System.Drawing.Size(474, 20);
            this.txtDesc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Perfil Atención :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(100, 76);
            this.txtCte.MaxLength = 6;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(71, 20);
            this.txtCte.TabIndex = 2;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // txtPerfilAtencion
            // 
            this.txtPerfilAtencion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPerfilAtencion.Decimales = 2;
            this.txtPerfilAtencion.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPerfilAtencion.ForeColor = System.Drawing.Color.Black;
            this.txtPerfilAtencion.Location = new System.Drawing.Point(100, 25);
            this.txtPerfilAtencion.MaxLength = 10;
            this.txtPerfilAtencion.Name = "txtPerfilAtencion";
            this.txtPerfilAtencion.PermitirApostrofo = false;
            this.txtPerfilAtencion.PermitirNegativos = false;
            this.txtPerfilAtencion.Size = new System.Drawing.Size(71, 20);
            this.txtPerfilAtencion.TabIndex = 0;
            this.txtPerfilAtencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPerfilAtencion.TextChanged += new System.EventHandler(this.txtPerfilAtencion_TextChanged);
            this.txtPerfilAtencion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPerfilAtencion_KeyDown);
            this.txtPerfilAtencion.Validating += new System.ComponentModel.CancelEventHandler(this.txtPerfilAtencion_Validating);
            // 
            // lblSubPrograma
            // 
            this.lblSubPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPrograma.Location = new System.Drawing.Point(176, 154);
            this.lblSubPrograma.Name = "lblSubPrograma";
            this.lblSubPrograma.Size = new System.Drawing.Size(399, 21);
            this.lblSubPrograma.TabIndex = 45;
            this.lblSubPrograma.Text = "SubPrograma :";
            this.lblSubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 51;
            this.label4.Text = "Descripción :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrograma
            // 
            this.lblPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrograma.Location = new System.Drawing.Point(176, 128);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(399, 21);
            this.lblPrograma.TabIndex = 44;
            this.lblPrograma.Text = "Programa :";
            this.lblPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "Sub-Programa :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 20);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(176, 101);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(399, 21);
            this.lblSubCliente.TabIndex = 39;
            this.lblSubCliente.Text = "SubCliente :";
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(176, 75);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(399, 21);
            this.lblCliente.TabIndex = 38;
            this.lblCliente.Text = "Cliente :";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Sub-Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGruposPerfilesAtencion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 219);
            this.Controls.Add(this.FrameDatosGral);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGruposPerfilesAtencion";
            this.Text = "Grupos Perfiles de Atención";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposPerfilesAtencion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosGral.ResumeLayout(false);
            this.FrameDatosGral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameDatosGral;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtDesc;
        private System.Windows.Forms.Label lblSubPrograma;
        private System.Windows.Forms.Label lblPrograma;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtPerfilAtencion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label lblCancelado;
    }
}