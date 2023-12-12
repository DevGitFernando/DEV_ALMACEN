namespace DllCompras.ListasDePrecioClaves
{
    partial class FrmComClaveSSA_CapturaPreciosNegociado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComClaveSSA_CapturaPreciosNegociado));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosProveedor = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtPrecio = new SC_ControlsCS.scNumericTextBox();
            this.lblNombreLaboratorio = new System.Windows.Forms.Label();
            this.txtIdLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameDatosClave = new System.Windows.Forms.GroupBox();
            this.lblIdClave = new System.Windows.Forms.Label();
            this.txtCodClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.scNumericTextBox1 = new SC_ControlsCS.scNumericTextBox();
            this.lbldClaveSSA = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosProveedor.SuspendLayout();
            this.FrameDatosClave.SuspendLayout();
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
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(613, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar (CTRL +E)";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Visible = false;
            // 
            // FrameDatosProveedor
            // 
            this.FrameDatosProveedor.Controls.Add(this.lblStatus);
            this.FrameDatosProveedor.Controls.Add(this.txtPrecio);
            this.FrameDatosProveedor.Controls.Add(this.lblNombreLaboratorio);
            this.FrameDatosProveedor.Controls.Add(this.txtIdLaboratorio);
            this.FrameDatosProveedor.Controls.Add(this.lblProveedor);
            this.FrameDatosProveedor.Controls.Add(this.label9);
            this.FrameDatosProveedor.Location = new System.Drawing.Point(4, 121);
            this.FrameDatosProveedor.Name = "FrameDatosProveedor";
            this.FrameDatosProveedor.Size = new System.Drawing.Size(604, 72);
            this.FrameDatosProveedor.TabIndex = 1;
            this.FrameDatosProveedor.TabStop = false;
            this.FrameDatosProveedor.Text = "Datos del Laboratorio y  Precio ";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(168, 46);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(163, 20);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "CANCELADO";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrecio
            // 
            this.txtPrecio.AllowNegative = true;
            this.txtPrecio.DigitsInGroup = 3;
            this.txtPrecio.Enabled = false;
            this.txtPrecio.Flags = 7680;
            this.txtPrecio.Location = new System.Drawing.Point(93, 46);
            this.txtPrecio.MaxDecimalPlaces = 4;
            this.txtPrecio.MaxLength = 15;
            this.txtPrecio.MaxWholeDigits = 9;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Prefix = "";
            this.txtPrecio.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecio.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecio.Size = new System.Drawing.Size(69, 20);
            this.txtPrecio.TabIndex = 1;
            this.txtPrecio.Text = "0.0000";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNombreLaboratorio
            // 
            this.lblNombreLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreLaboratorio.Location = new System.Drawing.Point(168, 20);
            this.lblNombreLaboratorio.Name = "lblNombreLaboratorio";
            this.lblNombreLaboratorio.Size = new System.Drawing.Size(427, 20);
            this.lblNombreLaboratorio.TabIndex = 3;
            this.lblNombreLaboratorio.Text = "NombreLaboratorio";
            this.lblNombreLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdLaboratorio
            // 
            this.txtIdLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdLaboratorio.Decimales = 2;
            this.txtIdLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtIdLaboratorio.Location = new System.Drawing.Point(93, 20);
            this.txtIdLaboratorio.Name = "txtIdLaboratorio";
            this.txtIdLaboratorio.PermitirApostrofo = false;
            this.txtIdLaboratorio.PermitirNegativos = false;
            this.txtIdLaboratorio.Size = new System.Drawing.Size(69, 20);
            this.txtIdLaboratorio.TabIndex = 0;
            this.txtIdLaboratorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdLaboratorio.TextChanged += new System.EventHandler(this.txtIdLaboratorio_TextChanged);
            this.txtIdLaboratorio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdLaboratorio_KeyDown);
            this.txtIdLaboratorio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdLaboratorio_Validating);
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(11, 23);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(81, 13);
            this.lblProveedor.TabIndex = 1;
            this.lblProveedor.Text = "Id. Laboratorio: ";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(43, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Precio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatosClave
            // 
            this.FrameDatosClave.Controls.Add(this.lbldClaveSSA);
            this.FrameDatosClave.Controls.Add(this.lblIdClave);
            this.FrameDatosClave.Controls.Add(this.txtCodClaveSSA);
            this.FrameDatosClave.Controls.Add(this.lblDescripcionClave);
            this.FrameDatosClave.Location = new System.Drawing.Point(4, 26);
            this.FrameDatosClave.Name = "FrameDatosClave";
            this.FrameDatosClave.Size = new System.Drawing.Size(604, 93);
            this.FrameDatosClave.TabIndex = 0;
            this.FrameDatosClave.TabStop = false;
            this.FrameDatosClave.Text = "Información de Clave SSA";
            // 
            // lblIdClave
            // 
            this.lblIdClave.Location = new System.Drawing.Point(11, 20);
            this.lblIdClave.Name = "lblIdClave";
            this.lblIdClave.Size = new System.Drawing.Size(81, 20);
            this.lblIdClave.TabIndex = 0;
            this.lblIdClave.Text = "Clave SSA: ";
            this.lblIdClave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodClaveSSA
            // 
            this.txtCodClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodClaveSSA.Decimales = 2;
            this.txtCodClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtCodClaveSSA.Location = new System.Drawing.Point(93, 20);
            this.txtCodClaveSSA.Name = "txtCodClaveSSA";
            this.txtCodClaveSSA.PermitirApostrofo = false;
            this.txtCodClaveSSA.PermitirNegativos = false;
            this.txtCodClaveSSA.Size = new System.Drawing.Size(140, 20);
            this.txtCodClaveSSA.TabIndex = 0;
            this.txtCodClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodClaveSSA.TextChanged += new System.EventHandler(this.txtCodClaveSSA_TextChanged);
            this.txtCodClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodClaveSSA_KeyDown);
            this.txtCodClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodClaveSSA_Validating);
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionClave.Location = new System.Drawing.Point(93, 48);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(502, 39);
            this.lblDescripcionClave.TabIndex = 8;
            this.lblDescripcionClave.Text = "label2";
            // 
            // scNumericTextBox1
            // 
            this.scNumericTextBox1.AllowNegative = true;
            this.scNumericTextBox1.DigitsInGroup = 3;
            this.scNumericTextBox1.Enabled = false;
            this.scNumericTextBox1.Flags = 7680;
            this.scNumericTextBox1.Location = new System.Drawing.Point(311, 244);
            this.scNumericTextBox1.MaxDecimalPlaces = 4;
            this.scNumericTextBox1.MaxLength = 15;
            this.scNumericTextBox1.MaxWholeDigits = 9;
            this.scNumericTextBox1.Name = "scNumericTextBox1";
            this.scNumericTextBox1.Prefix = "";
            this.scNumericTextBox1.RangeMax = 1.7976931348623157E+308D;
            this.scNumericTextBox1.RangeMin = -1.7976931348623157E+308D;
            this.scNumericTextBox1.Size = new System.Drawing.Size(69, 20);
            this.scNumericTextBox1.TabIndex = 11;
            this.scNumericTextBox1.Text = "0.0000";
            this.scNumericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbldClaveSSA
            // 
            this.lbldClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbldClaveSSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldClaveSSA.Location = new System.Drawing.Point(239, 20);
            this.lbldClaveSSA.Name = "lbldClaveSSA";
            this.lbldClaveSSA.Size = new System.Drawing.Size(163, 20);
            this.lbldClaveSSA.TabIndex = 11;
            this.lbldClaveSSA.Text = "CANCELADO";
            this.lbldClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmComClaveSSA_CapturaPreciosNegociado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 440);
            this.Controls.Add(this.scNumericTextBox1);
            this.Controls.Add(this.FrameDatosClave);
            this.Controls.Add(this.FrameDatosProveedor);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmComClaveSSA_CapturaPreciosNegociado";
            this.Text = "Captura de Precios de Comisión Negociadora";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmComClaveSSA_CapturaPreciosNegociado_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosProveedor.ResumeLayout(false);
            this.FrameDatosProveedor.PerformLayout();
            this.FrameDatosClave.ResumeLayout(false);
            this.FrameDatosClave.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDatosProveedor;
        private System.Windows.Forms.Label lblNombreLaboratorio;
        private SC_ControlsCS.scTextBoxExt txtIdLaboratorio;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.GroupBox FrameDatosClave;
        private System.Windows.Forms.Label lblIdClave;
        private SC_ControlsCS.scTextBoxExt txtCodClaveSSA;
        private SC_ControlsCS.scNumericTextBox txtPrecio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbldClaveSSA;
        private SC_ControlsCS.scNumericTextBox scNumericTextBox1;
    }
}