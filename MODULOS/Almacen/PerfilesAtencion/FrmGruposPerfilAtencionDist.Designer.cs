namespace Almacen.PerfilesAtencion
{
    partial class FrmGruposPerfilAtencionDist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGruposPerfilAtencionDist));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosGral = new System.Windows.Forms.GroupBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtDesc = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPerfilAtencion = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
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
            this.FrameDatosGral.Controls.Add(this.txtDesc);
            this.FrameDatosGral.Controls.Add(this.label2);
            this.FrameDatosGral.Controls.Add(this.txtPerfilAtencion);
            this.FrameDatosGral.Controls.Add(this.label4);
            this.FrameDatosGral.Location = new System.Drawing.Point(11, 24);
            this.FrameDatosGral.Name = "FrameDatosGral";
            this.FrameDatosGral.Size = new System.Drawing.Size(580, 82);
            this.FrameDatosGral.TabIndex = 1;
            this.FrameDatosGral.TabStop = false;
            this.FrameDatosGral.Text = "Datos Perfiles de Atención";
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
            this.txtDesc.Size = new System.Drawing.Size(469, 20);
            this.txtDesc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Perfil Atención :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtPerfilAtencion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPerfilAtencion_KeyDown);
            this.txtPerfilAtencion.Validating += new System.ComponentModel.CancelEventHandler(this.txtPerfilAtencion_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 51;
            this.label4.Text = "Descripción :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGruposPerfilAtencionDist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 115);
            this.Controls.Add(this.FrameDatosGral);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGruposPerfilAtencionDist";
            this.Text = "Grupos Perfiles de Atención Distribuidor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposPerfilAtencionDist_Load);
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
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox FrameDatosGral;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtDesc;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtPerfilAtencion;
        private System.Windows.Forms.Label label4;
    }
}