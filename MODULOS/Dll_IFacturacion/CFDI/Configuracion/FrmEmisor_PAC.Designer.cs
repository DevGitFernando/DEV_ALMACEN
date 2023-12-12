namespace Dll_IFacturacion.Configuracion
{
    partial class FrmEmisor_PAC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmisor_PAC));
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRevisarDisponibilidad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FramePAC = new System.Windows.Forms.GroupBox();
            this.chkEnProduccion = new System.Windows.Forms.CheckBox();
            this.txtPasswordConfirmacion = new SC_ControlsCS.scTextBoxExt();
            this.txtPassword = new SC_ControlsCS.scTextBoxExt();
            this.txtUsuario = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.cboPAC = new SC_ControlsCS.scComboBoxExt();
            this.toolMenuFacturacion.SuspendLayout();
            this.FramePAC.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMenuFacturacion
            // 
            this.toolMenuFacturacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnCancelar,
            this.toolStripSeparator5,
            this.btnRevisarDisponibilidad,
            this.toolStripSeparator1});
            this.toolMenuFacturacion.Location = new System.Drawing.Point(0, 0);
            this.toolMenuFacturacion.Name = "toolMenuFacturacion";
            this.toolMenuFacturacion.Size = new System.Drawing.Size(436, 25);
            this.toolMenuFacturacion.TabIndex = 0;
            this.toolMenuFacturacion.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRevisarDisponibilidad
            // 
            this.btnRevisarDisponibilidad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRevisarDisponibilidad.Image = ((System.Drawing.Image)(resources.GetObject("btnRevisarDisponibilidad.Image")));
            this.btnRevisarDisponibilidad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRevisarDisponibilidad.Name = "btnRevisarDisponibilidad";
            this.btnRevisarDisponibilidad.Size = new System.Drawing.Size(23, 22);
            this.btnRevisarDisponibilidad.Text = "Imprimir";
            this.btnRevisarDisponibilidad.Visible = false;
            this.btnRevisarDisponibilidad.Click += new System.EventHandler(this.btnRevisarDisponibilidad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // FramePAC
            // 
            this.FramePAC.Controls.Add(this.chkEnProduccion);
            this.FramePAC.Controls.Add(this.txtPasswordConfirmacion);
            this.FramePAC.Controls.Add(this.txtPassword);
            this.FramePAC.Controls.Add(this.txtUsuario);
            this.FramePAC.Controls.Add(this.scLabelExt4);
            this.FramePAC.Controls.Add(this.scLabelExt3);
            this.FramePAC.Controls.Add(this.scLabelExt2);
            this.FramePAC.Controls.Add(this.scLabelExt1);
            this.FramePAC.Controls.Add(this.cboPAC);
            this.FramePAC.Location = new System.Drawing.Point(10, 28);
            this.FramePAC.Name = "FramePAC";
            this.FramePAC.Size = new System.Drawing.Size(416, 153);
            this.FramePAC.TabIndex = 1;
            this.FramePAC.TabStop = false;
            // 
            // chkEnProduccion
            // 
            this.chkEnProduccion.Location = new System.Drawing.Point(137, 120);
            this.chkEnProduccion.Name = "chkEnProduccion";
            this.chkEnProduccion.Size = new System.Drawing.Size(266, 19);
            this.chkEnProduccion.TabIndex = 4;
            this.chkEnProduccion.Text = "Modo productivo";
            this.chkEnProduccion.UseVisualStyleBackColor = true;
            // 
            // txtPasswordConfirmacion
            // 
            this.txtPasswordConfirmacion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasswordConfirmacion.Decimales = 2;
            this.txtPasswordConfirmacion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPasswordConfirmacion.ForeColor = System.Drawing.Color.Black;
            this.txtPasswordConfirmacion.Location = new System.Drawing.Point(137, 94);
            this.txtPasswordConfirmacion.Name = "txtPasswordConfirmacion";
            this.txtPasswordConfirmacion.PermitirApostrofo = false;
            this.txtPasswordConfirmacion.PermitirNegativos = false;
            this.txtPasswordConfirmacion.Size = new System.Drawing.Size(266, 20);
            this.txtPasswordConfirmacion.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPassword.Decimales = 2;
            this.txtPassword.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(137, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PermitirApostrofo = false;
            this.txtPassword.PermitirNegativos = false;
            this.txtPassword.Size = new System.Drawing.Size(266, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // txtUsuario
            // 
            this.txtUsuario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUsuario.Decimales = 2;
            this.txtUsuario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUsuario.ForeColor = System.Drawing.Color.Black;
            this.txtUsuario.Location = new System.Drawing.Point(137, 46);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.PermitirApostrofo = false;
            this.txtUsuario.PermitirNegativos = false;
            this.txtUsuario.Size = new System.Drawing.Size(266, 20);
            this.txtUsuario.TabIndex = 1;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(17, 98);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(114, 13);
            this.scLabelExt4.TabIndex = 4;
            this.scLabelExt4.Text = "Confirmar password :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.Location = new System.Drawing.Point(17, 74);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(114, 13);
            this.scLabelExt3.TabIndex = 3;
            this.scLabelExt3.Text = "Password :";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(17, 50);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(114, 13);
            this.scLabelExt2.TabIndex = 2;
            this.scLabelExt2.Text = "Usuario :";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(17, 22);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(114, 13);
            this.scLabelExt1.TabIndex = 1;
            this.scLabelExt1.Text = "Proveedor :";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPAC
            // 
            this.cboPAC.BackColorEnabled = System.Drawing.Color.White;
            this.cboPAC.Data = "";
            this.cboPAC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPAC.Filtro = " 1 = 1";
            this.cboPAC.FormattingEnabled = true;
            this.cboPAC.ListaItemsBusqueda = 20;
            this.cboPAC.Location = new System.Drawing.Point(137, 18);
            this.cboPAC.MostrarToolTip = false;
            this.cboPAC.Name = "cboPAC";
            this.cboPAC.Size = new System.Drawing.Size(266, 21);
            this.cboPAC.TabIndex = 0;
            // 
            // FrmEmisor_PAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 189);
            this.Controls.Add(this.FramePAC);
            this.Controls.Add(this.toolMenuFacturacion);
            this.Name = "FrmEmisor_PAC";
            this.Text = "Proveedor de Certificación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEmisor_PAC_Load);
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.FramePAC.ResumeLayout(false);
            this.FramePAC.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnRevisarDisponibilidad;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FramePAC;
        private System.Windows.Forms.CheckBox chkEnProduccion;
        private SC_ControlsCS.scTextBoxExt txtPasswordConfirmacion;
        private SC_ControlsCS.scTextBoxExt txtPassword;
        private SC_ControlsCS.scTextBoxExt txtUsuario;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scComboBoxExt cboPAC;
    }
}