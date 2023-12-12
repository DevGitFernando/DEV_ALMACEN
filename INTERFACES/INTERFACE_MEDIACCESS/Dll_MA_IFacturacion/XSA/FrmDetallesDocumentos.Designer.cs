namespace Dll_MA_IFacturacion.XSA
{
    partial class FrmDetallesDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetallesDocumentos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.btnConceptos = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTiposImpuestos = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipoUnidad = new SC_ControlsCS.scComboBoxExt();
            this.nmTasaIva = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nmPrecioUnitario = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nmCantidad = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClave = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdentificador = new SC_ControlsCS.scTextBoxExt();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmTasaIva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioUnitario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCantidad)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(625, 25);
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
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.btnConceptos);
            this.FrameInformacion.Controls.Add(this.label7);
            this.FrameInformacion.Controls.Add(this.cboTiposImpuestos);
            this.FrameInformacion.Controls.Add(this.label6);
            this.FrameInformacion.Controls.Add(this.cboTipoUnidad);
            this.FrameInformacion.Controls.Add(this.nmTasaIva);
            this.FrameInformacion.Controls.Add(this.label5);
            this.FrameInformacion.Controls.Add(this.nmPrecioUnitario);
            this.FrameInformacion.Controls.Add(this.label4);
            this.FrameInformacion.Controls.Add(this.nmCantidad);
            this.FrameInformacion.Controls.Add(this.label3);
            this.FrameInformacion.Controls.Add(this.txtDescripcion);
            this.FrameInformacion.Controls.Add(this.label2);
            this.FrameInformacion.Controls.Add(this.txtClave);
            this.FrameInformacion.Controls.Add(this.label1);
            this.FrameInformacion.Controls.Add(this.txtIdentificador);
            this.FrameInformacion.Controls.Add(this.label35);
            this.FrameInformacion.Location = new System.Drawing.Point(12, 28);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(602, 329);
            this.FrameInformacion.TabIndex = 1;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información del Concepto";
            // 
            // btnConceptos
            // 
            this.btnConceptos.Location = new System.Drawing.Point(291, 46);
            this.btnConceptos.Name = "btnConceptos";
            this.btnConceptos.Size = new System.Drawing.Size(30, 21);
            this.btnConceptos.TabIndex = 58;
            this.btnConceptos.Text = "...";
            this.btnConceptos.UseVisualStyleBackColor = true;
            this.btnConceptos.Click += new System.EventHandler(this.btnConceptos_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 20);
            this.label7.TabIndex = 57;
            this.label7.Text = "Impuesto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTiposImpuestos
            // 
            this.cboTiposImpuestos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTiposImpuestos.Data = "";
            this.cboTiposImpuestos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposImpuestos.Filtro = " 1 = 1";
            this.cboTiposImpuestos.FormattingEnabled = true;
            this.cboTiposImpuestos.ListaItemsBusqueda = 20;
            this.cboTiposImpuestos.Location = new System.Drawing.Point(115, 298);
            this.cboTiposImpuestos.MostrarToolTip = false;
            this.cboTiposImpuestos.Name = "cboTiposImpuestos";
            this.cboTiposImpuestos.Size = new System.Drawing.Size(206, 21);
            this.cboTiposImpuestos.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 55;
            this.label6.Text = "Unidad de Medida :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoUnidad
            // 
            this.cboTipoUnidad.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoUnidad.Data = "";
            this.cboTipoUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoUnidad.Filtro = " 1 = 1";
            this.cboTipoUnidad.FormattingEnabled = true;
            this.cboTipoUnidad.ListaItemsBusqueda = 20;
            this.cboTipoUnidad.Location = new System.Drawing.Point(115, 240);
            this.cboTipoUnidad.MostrarToolTip = false;
            this.cboTipoUnidad.Name = "cboTipoUnidad";
            this.cboTipoUnidad.Size = new System.Drawing.Size(235, 21);
            this.cboTipoUnidad.TabIndex = 3;
            // 
            // nmTasaIva
            // 
            this.nmTasaIva.Location = new System.Drawing.Point(526, 269);
            this.nmTasaIva.Name = "nmTasaIva";
            this.nmTasaIva.Size = new System.Drawing.Size(67, 20);
            this.nmTasaIva.TabIndex = 6;
            this.nmTasaIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmTasaIva.ThousandsSeparator = true;
            this.nmTasaIva.ValueChanged += new System.EventHandler(this.nmTasaIva_ValueChanged);
            this.nmTasaIva.Enter += new System.EventHandler(this.nmTasaIva_Enter);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(459, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 52;
            this.label5.Text = "Tasa iva :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmPrecioUnitario
            // 
            this.nmPrecioUnitario.DecimalPlaces = 4;
            this.nmPrecioUnitario.Location = new System.Drawing.Point(330, 269);
            this.nmPrecioUnitario.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmPrecioUnitario.Name = "nmPrecioUnitario";
            this.nmPrecioUnitario.Size = new System.Drawing.Size(120, 20);
            this.nmPrecioUnitario.TabIndex = 5;
            this.nmPrecioUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmPrecioUnitario.ThousandsSeparator = true;
            this.nmPrecioUnitario.Value = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmPrecioUnitario.ValueChanged += new System.EventHandler(this.nmPrecioUnitario_ValueChanged);
            this.nmPrecioUnitario.Enter += new System.EventHandler(this.nmPrecioUnitario_Enter);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(238, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "Precio Unitario :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmCantidad
            // 
            this.nmCantidad.Location = new System.Drawing.Point(115, 269);
            this.nmCantidad.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmCantidad.Name = "nmCantidad";
            this.nmCantidad.Size = new System.Drawing.Size(98, 20);
            this.nmCantidad.TabIndex = 4;
            this.nmCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmCantidad.ThousandsSeparator = true;
            this.nmCantidad.Value = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmCantidad.ValueChanged += new System.EventHandler(this.nmCantidad_ValueChanged);
            this.nmCantidad.Enter += new System.EventHandler(this.nmCantidad_Enter);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(28, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 48;
            this.label3.Text = "Cantidad :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(115, 72);
            this.txtDescripcion.MaxLength = 500;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(478, 162);
            this.txtDescripcion.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 47;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClave
            // 
            this.txtClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClave.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClave.Decimales = 2;
            this.txtClave.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClave.ForeColor = System.Drawing.Color.Black;
            this.txtClave.Location = new System.Drawing.Point(115, 46);
            this.txtClave.MaxLength = 20;
            this.txtClave.Name = "txtClave";
            this.txtClave.PermitirApostrofo = false;
            this.txtClave.PermitirNegativos = false;
            this.txtClave.Size = new System.Drawing.Size(173, 20);
            this.txtClave.TabIndex = 1;
            this.txtClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClave.TextChanged += new System.EventHandler(this.txtClave_TextChanged);
            this.txtClave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClave_KeyDown);
            this.txtClave.Validating += new System.ComponentModel.CancelEventHandler(this.txtClave_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 45;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdentificador.Decimales = 2;
            this.txtIdentificador.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdentificador.ForeColor = System.Drawing.Color.Black;
            this.txtIdentificador.Location = new System.Drawing.Point(115, 20);
            this.txtIdentificador.MaxLength = 6;
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.PermitirApostrofo = false;
            this.txtIdentificador.PermitirNegativos = false;
            this.txtIdentificador.Size = new System.Drawing.Size(88, 20);
            this.txtIdentificador.TabIndex = 0;
            this.txtIdentificador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(28, 20);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(86, 20);
            this.label35.TabIndex = 43;
            this.label35.Text = "Identificador :";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblIva);
            this.groupBox2.Controls.Add(this.lblSubTotal);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(12, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 99);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Importes de Concepto";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(446, 67);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(146, 20);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Núm. Cotización :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(327, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 16);
            this.label15.TabIndex = 52;
            this.label15.Text = "Total : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(327, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 16);
            this.label11.TabIndex = 50;
            this.label11.Text = "Sub-Total : ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(446, 42);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(146, 20);
            this.lblIva.TabIndex = 1;
            this.lblIva.Text = "Núm. Cotización :";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(446, 16);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(146, 20);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "Núm. Cotización :";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(327, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 16);
            this.label13.TabIndex = 51;
            this.label13.Text = "Iva : ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDetallesDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 465);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDetallesDocumentos";
            this.Text = "Concepto de Documento Electrónico";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDetallesDocumentos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameInformacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmTasaIva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPrecioUnitario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCantidad)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private SC_ControlsCS.scTextBoxExt txtIdentificador;
        private System.Windows.Forms.Label label35;
        private SC_ControlsCS.scTextBoxExt txtClave;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nmCantidad;
        private System.Windows.Forms.NumericUpDown nmTasaIva;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmPrecioUnitario;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboTipoUnidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scComboBoxExt cboTiposImpuestos;
        private System.Windows.Forms.Button btnConceptos;
    }
}