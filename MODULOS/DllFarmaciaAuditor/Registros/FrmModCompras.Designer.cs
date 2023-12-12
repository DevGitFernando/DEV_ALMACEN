namespace DllFarmaciaAuditor.Registros
{
    partial class FrmModCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModCompras));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.lblCorregido = new System.Windows.Forms.Label();
            this.dtpFechaVenceDocto = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaDocto = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReferenciaDocto = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameCambio = new System.Windows.Forms.GroupBox();
            this.dtpFechaDocto_Nuevo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReferenciaDocto_Nuevo = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblProveedor_Nuevo = new System.Windows.Forms.Label();
            this.txtIdProveedor_Nuevo = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.FrameCambio.SuspendLayout();
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
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(702, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.lblCorregido);
            this.FrameEncabezado.Controls.Add(this.dtpFechaVenceDocto);
            this.FrameEncabezado.Controls.Add(this.label6);
            this.FrameEncabezado.Controls.Add(this.dtpFechaDocto);
            this.FrameEncabezado.Controls.Add(this.label5);
            this.FrameEncabezado.Controls.Add(this.txtReferenciaDocto);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.lblProveedor);
            this.FrameEncabezado.Controls.Add(this.txtIdProveedor);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(12, 28);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(679, 108);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Actuales de la Compra";
            // 
            // lblCorregido
            // 
            this.lblCorregido.BackColor = System.Drawing.Color.Transparent;
            this.lblCorregido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCorregido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorregido.Location = new System.Drawing.Point(203, 27);
            this.lblCorregido.Name = "lblCorregido";
            this.lblCorregido.Size = new System.Drawing.Size(98, 20);
            this.lblCorregido.TabIndex = 14;
            this.lblCorregido.Text = "CORREGIDO";
            this.lblCorregido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCorregido.Visible = false;
            // 
            // dtpFechaVenceDocto
            // 
            this.dtpFechaVenceDocto.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVenceDocto.Enabled = false;
            this.dtpFechaVenceDocto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVenceDocto.Location = new System.Drawing.Point(576, 75);
            this.dtpFechaVenceDocto.Name = "dtpFechaVenceDocto";
            this.dtpFechaVenceDocto.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVenceDocto.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(442, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Fecha vence documento :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDocto
            // 
            this.dtpFechaDocto.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDocto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDocto.Location = new System.Drawing.Point(321, 75);
            this.dtpFechaDocto.Name = "dtpFechaDocto";
            this.dtpFechaDocto.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaDocto.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(221, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fecha documento :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferenciaDocto
            // 
            this.txtReferenciaDocto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaDocto.Decimales = 2;
            this.txtReferenciaDocto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaDocto.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaDocto.Location = new System.Drawing.Point(97, 75);
            this.txtReferenciaDocto.MaxLength = 20;
            this.txtReferenciaDocto.Name = "txtReferenciaDocto";
            this.txtReferenciaDocto.PermitirApostrofo = false;
            this.txtReferenciaDocto.PermitirNegativos = false;
            this.txtReferenciaDocto.Size = new System.Drawing.Size(100, 20);
            this.txtReferenciaDocto.TabIndex = 3;
            this.txtReferenciaDocto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Referencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(576, 27);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(468, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(203, 51);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(464, 20);
            this.lblProveedor.TabIndex = 6;
            this.lblProveedor.Text = "Proveedor :";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(97, 51);
            this.txtIdProveedor.MaxLength = 4;
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(100, 20);
            this.txtIdProveedor.TabIndex = 2;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Proveedor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(97, 27);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameCambio
            // 
            this.FrameCambio.Controls.Add(this.dtpFechaDocto_Nuevo);
            this.FrameCambio.Controls.Add(this.label8);
            this.FrameCambio.Controls.Add(this.txtReferenciaDocto_Nuevo);
            this.FrameCambio.Controls.Add(this.label9);
            this.FrameCambio.Controls.Add(this.lblProveedor_Nuevo);
            this.FrameCambio.Controls.Add(this.txtIdProveedor_Nuevo);
            this.FrameCambio.Controls.Add(this.label13);
            this.FrameCambio.Location = new System.Drawing.Point(12, 138);
            this.FrameCambio.Name = "FrameCambio";
            this.FrameCambio.Size = new System.Drawing.Size(679, 82);
            this.FrameCambio.TabIndex = 2;
            this.FrameCambio.TabStop = false;
            this.FrameCambio.Text = "Datos Nuevos de la Compra";
            // 
            // dtpFechaDocto_Nuevo
            // 
            this.dtpFechaDocto_Nuevo.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDocto_Nuevo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDocto_Nuevo.Location = new System.Drawing.Point(303, 48);
            this.dtpFechaDocto_Nuevo.Name = "dtpFechaDocto_Nuevo";
            this.dtpFechaDocto_Nuevo.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaDocto_Nuevo.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(203, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Fecha documento :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferenciaDocto_Nuevo
            // 
            this.txtReferenciaDocto_Nuevo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaDocto_Nuevo.Decimales = 2;
            this.txtReferenciaDocto_Nuevo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaDocto_Nuevo.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaDocto_Nuevo.Location = new System.Drawing.Point(97, 48);
            this.txtReferenciaDocto_Nuevo.MaxLength = 20;
            this.txtReferenciaDocto_Nuevo.Name = "txtReferenciaDocto_Nuevo";
            this.txtReferenciaDocto_Nuevo.PermitirApostrofo = false;
            this.txtReferenciaDocto_Nuevo.PermitirNegativos = false;
            this.txtReferenciaDocto_Nuevo.Size = new System.Drawing.Size(100, 20);
            this.txtReferenciaDocto_Nuevo.TabIndex = 1;
            this.txtReferenciaDocto_Nuevo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Referencia :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor_Nuevo
            // 
            this.lblProveedor_Nuevo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor_Nuevo.Location = new System.Drawing.Point(203, 23);
            this.lblProveedor_Nuevo.Name = "lblProveedor_Nuevo";
            this.lblProveedor_Nuevo.Size = new System.Drawing.Size(464, 20);
            this.lblProveedor_Nuevo.TabIndex = 6;
            this.lblProveedor_Nuevo.Text = "Proveedor :";
            this.lblProveedor_Nuevo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor_Nuevo
            // 
            this.txtIdProveedor_Nuevo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor_Nuevo.Decimales = 2;
            this.txtIdProveedor_Nuevo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor_Nuevo.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor_Nuevo.Location = new System.Drawing.Point(97, 23);
            this.txtIdProveedor_Nuevo.MaxLength = 4;
            this.txtIdProveedor_Nuevo.Name = "txtIdProveedor_Nuevo";
            this.txtIdProveedor_Nuevo.PermitirApostrofo = false;
            this.txtIdProveedor_Nuevo.PermitirNegativos = false;
            this.txtIdProveedor_Nuevo.Size = new System.Drawing.Size(100, 20);
            this.txtIdProveedor_Nuevo.TabIndex = 0;
            this.txtIdProveedor_Nuevo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor_Nuevo.TextChanged += new System.EventHandler(this.txtIdProveedor_Nuevo_TextChanged);
            this.txtIdProveedor_Nuevo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_Nuevo_KeyDown);
            this.txtIdProveedor_Nuevo.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Nuevo_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "Proveedor :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmModCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 228);
            this.Controls.Add(this.FrameCambio);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmModCompras";
            this.Text = "Modificacion de Compras";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmModCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.FrameCambio.ResumeLayout(false);
            this.FrameCambio.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaVenceDocto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaDocto;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtReferenciaDocto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProveedor;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameCambio;
        private System.Windows.Forms.DateTimePicker dtpFechaDocto_Nuevo;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtReferenciaDocto_Nuevo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblProveedor_Nuevo;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor_Nuevo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblCorregido;
    }
}