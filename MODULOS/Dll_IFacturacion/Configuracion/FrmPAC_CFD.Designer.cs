namespace Dll_IFacturacion.Configuracion
{
    partial class FrmPAC_CFD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPAC_CFD));
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRevisarDisponibilidad = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.txtTelefonos = new SC_ControlsCS.scTextBoxExt();
            this.txtDireccion = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolMenuFacturacion.SuspendLayout();
            this.FrameDatos.SuspendLayout();
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
            this.toolMenuFacturacion.Size = new System.Drawing.Size(444, 25);
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
            this.btnRevisarDisponibilidad.Click += new System.EventHandler(this.btnRevisarDisponibilidad_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.txtTelefonos);
            this.FrameDatos.Controls.Add(this.txtDireccion);
            this.FrameDatos.Controls.Add(this.txtNombre);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.label5);
            this.FrameDatos.Controls.Add(this.txtId);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Location = new System.Drawing.Point(9, 28);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(428, 185);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos de Proveedor";
            // 
            // txtTelefonos
            // 
            this.txtTelefonos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTelefonos.Decimales = 2;
            this.txtTelefonos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTelefonos.ForeColor = System.Drawing.Color.Black;
            this.txtTelefonos.Location = new System.Drawing.Point(91, 135);
            this.txtTelefonos.MaxLength = 100;
            this.txtTelefonos.Multiline = true;
            this.txtTelefonos.Name = "txtTelefonos";
            this.txtTelefonos.PermitirApostrofo = false;
            this.txtTelefonos.PermitirNegativos = false;
            this.txtTelefonos.Size = new System.Drawing.Size(324, 40);
            this.txtTelefonos.TabIndex = 48;
            // 
            // txtDireccion
            // 
            this.txtDireccion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDireccion.Decimales = 2;
            this.txtDireccion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDireccion.ForeColor = System.Drawing.Color.Black;
            this.txtDireccion.Location = new System.Drawing.Point(91, 69);
            this.txtDireccion.MaxLength = 200;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.PermitirApostrofo = false;
            this.txtDireccion.PermitirNegativos = false;
            this.txtDireccion.Size = new System.Drawing.Size(324, 60);
            this.txtDireccion.TabIndex = 47;
            // 
            // txtNombre
            // 
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(91, 45);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(324, 20);
            this.txtNombre.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "Teléfono(s) :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Nombre :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 33);
            this.label5.TabIndex = 41;
            this.label5.Text = "Dirección de Timbrado:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(91, 19);
            this.txtId.MaxLength = 2;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(58, 20);
            this.txtId.TabIndex = 0;
            this.txtId.Text = "0123456789";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPAC_CFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 220);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolMenuFacturacion);
            this.Name = "FrmPAC_CFD";
            this.Text = "Proveedores de Servicio de Timbrado";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPAC_CFD_Load);
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private SC_ControlsCS.scTextBoxExt txtDireccion;
        private SC_ControlsCS.scTextBoxExt txtTelefonos;
    }
}