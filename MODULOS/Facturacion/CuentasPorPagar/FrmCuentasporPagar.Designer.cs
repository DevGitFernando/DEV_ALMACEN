namespace Facturacion.CuentasPorPagar
{
    partial class FrmCuentasporPagar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCuentasporPagar));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboMetodoPago = new SC_ControlsCS.scComboBoxExt();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.txtSubTotal = new SC_ControlsCS.scNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPorcentajeIva = new System.Windows.Forms.Label();
            this.nudTasaIva = new System.Windows.Forms.NumericUpDown();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaDoc = new System.Windows.Forms.DateTimePicker();
            this.txtRefDocto = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAcreedor = new System.Windows.Forms.Label();
            this.lblServicio = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAcreedor = new SC_ControlsCS.scTextBoxExt();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServicio = new SC_ControlsCS.scTextBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTasaIva)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(709, 25);
            this.toolStripBarraMenu.TabIndex = 2;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.label10);
            this.FrameDatos.Controls.Add(this.cboMetodoPago);
            this.FrameDatos.Controls.Add(this.txtTotal);
            this.FrameDatos.Controls.Add(this.txtIva);
            this.FrameDatos.Controls.Add(this.txtSubTotal);
            this.FrameDatos.Controls.Add(this.label9);
            this.FrameDatos.Controls.Add(this.lblPorcentajeIva);
            this.FrameDatos.Controls.Add(this.nudTasaIva);
            this.FrameDatos.Controls.Add(this.lblPrecio);
            this.FrameDatos.Controls.Add(this.label8);
            this.FrameDatos.Controls.Add(this.label7);
            this.FrameDatos.Controls.Add(this.dtpFechaDoc);
            this.FrameDatos.Controls.Add(this.txtRefDocto);
            this.FrameDatos.Controls.Add(this.label6);
            this.FrameDatos.Controls.Add(this.lblAcreedor);
            this.FrameDatos.Controls.Add(this.lblServicio);
            this.FrameDatos.Controls.Add(this.label5);
            this.FrameDatos.Controls.Add(this.txtObservaciones);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Controls.Add(this.txtAcreedor);
            this.FrameDatos.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.txtServicio);
            this.FrameDatos.Controls.Add(this.lblCancelado);
            this.FrameDatos.Controls.Add(this.txtFolio);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Location = new System.Drawing.Point(12, 27);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(684, 207);
            this.FrameDatos.TabIndex = 0;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Información";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(435, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
            this.label10.TabIndex = 62;
            this.label10.Text = "Método Pago :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMetodoPago
            // 
            this.cboMetodoPago.BackColorEnabled = System.Drawing.Color.White;
            this.cboMetodoPago.Data = "";
            this.cboMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodoPago.Filtro = " 1 = 1";
            this.cboMetodoPago.FormattingEnabled = true;
            this.cboMetodoPago.ListaItemsBusqueda = 20;
            this.cboMetodoPago.Location = new System.Drawing.Point(517, 97);
            this.cboMetodoPago.MostrarToolTip = false;
            this.cboMetodoPago.Name = "cboMetodoPago";
            this.cboMetodoPago.Size = new System.Drawing.Size(154, 21);
            this.cboMetodoPago.TabIndex = 5;
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = true;
            this.txtTotal.DigitsInGroup = 3;
            this.txtTotal.Flags = 7680;
            this.txtTotal.Location = new System.Drawing.Point(573, 125);
            this.txtTotal.MaxDecimalPlaces = 2;
            this.txtTotal.MaxLength = 15;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal.Size = new System.Drawing.Size(98, 20);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.Text = "0.00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
            this.txtTotal.Validating += new System.ComponentModel.CancelEventHandler(this.txtTotal_Validating);
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = true;
            this.txtIva.DigitsInGroup = 3;
            this.txtIva.Enabled = false;
            this.txtIva.Flags = 7680;
            this.txtIva.Location = new System.Drawing.Point(420, 125);
            this.txtIva.MaxDecimalPlaces = 2;
            this.txtIva.MaxLength = 15;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308D;
            this.txtIva.RangeMin = -1.7976931348623157E+308D;
            this.txtIva.Size = new System.Drawing.Size(81, 20);
            this.txtIva.TabIndex = 8;
            this.txtIva.Text = "0.00";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.AllowNegative = true;
            this.txtSubTotal.DigitsInGroup = 3;
            this.txtSubTotal.Enabled = false;
            this.txtSubTotal.Flags = 7680;
            this.txtSubTotal.Location = new System.Drawing.Point(108, 125);
            this.txtSubTotal.MaxDecimalPlaces = 2;
            this.txtSubTotal.MaxLength = 15;
            this.txtSubTotal.MaxWholeDigits = 9;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Prefix = "";
            this.txtSubTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal.Size = new System.Drawing.Size(99, 20);
            this.txtSubTotal.TabIndex = 6;
            this.txtSubTotal.Text = "0.00";
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(507, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 12);
            this.label9.TabIndex = 57;
            this.label9.Text = "Total :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPorcentajeIva
            // 
            this.lblPorcentajeIva.Location = new System.Drawing.Point(249, 125);
            this.lblPorcentajeIva.Name = "lblPorcentajeIva";
            this.lblPorcentajeIva.Size = new System.Drawing.Size(45, 18);
            this.lblPorcentajeIva.TabIndex = 54;
            this.lblPorcentajeIva.Text = "% IVA : ";
            this.lblPorcentajeIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudTasaIva
            // 
            this.nudTasaIva.Location = new System.Drawing.Point(296, 125);
            this.nudTasaIva.Name = "nudTasaIva";
            this.nudTasaIva.Size = new System.Drawing.Size(52, 20);
            this.nudTasaIva.TabIndex = 7;
            this.nudTasaIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTasaIva.ValueChanged += new System.EventHandler(this.nudTasaIva_ValueChanged);
            this.nudTasaIva.Validating += new System.ComponentModel.CancelEventHandler(this.nudTasaIva_Validating);
            // 
            // lblPrecio
            // 
            this.lblPrecio.Location = new System.Drawing.Point(41, 129);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(64, 12);
            this.lblPrecio.TabIndex = 51;
            this.lblPrecio.Text = "Sub-Total :";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(382, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 55;
            this.label8.Text = "IVA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(235, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 49;
            this.label7.Text = "Fecha Documento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDoc
            // 
            this.dtpFechaDoc.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDoc.Enabled = false;
            this.dtpFechaDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDoc.Location = new System.Drawing.Point(341, 97);
            this.dtpFechaDoc.Name = "dtpFechaDoc";
            this.dtpFechaDoc.Size = new System.Drawing.Size(88, 20);
            this.dtpFechaDoc.TabIndex = 4;
            // 
            // txtRefDocto
            // 
            this.txtRefDocto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRefDocto.Decimales = 2;
            this.txtRefDocto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRefDocto.ForeColor = System.Drawing.Color.Black;
            this.txtRefDocto.Location = new System.Drawing.Point(108, 97);
            this.txtRefDocto.MaxLength = 10;
            this.txtRefDocto.Name = "txtRefDocto";
            this.txtRefDocto.PermitirApostrofo = false;
            this.txtRefDocto.PermitirNegativos = false;
            this.txtRefDocto.Size = new System.Drawing.Size(122, 20);
            this.txtRefDocto.TabIndex = 3;
            this.txtRefDocto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 47;
            this.label6.Text = "Referencia Docto :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAcreedor
            // 
            this.lblAcreedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAcreedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcreedor.Location = new System.Drawing.Point(196, 71);
            this.lblAcreedor.Name = "lblAcreedor";
            this.lblAcreedor.Size = new System.Drawing.Size(475, 20);
            this.lblAcreedor.TabIndex = 45;
            this.lblAcreedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblServicio
            // 
            this.lblServicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicio.Location = new System.Drawing.Point(196, 45);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(475, 20);
            this.lblServicio.TabIndex = 44;
            this.lblServicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 43;
            this.label5.Text = "Observaciones :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(108, 151);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(563, 42);
            this.txtObservaciones.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(488, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Fecha Registro :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAcreedor
            // 
            this.txtAcreedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAcreedor.Decimales = 2;
            this.txtAcreedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtAcreedor.ForeColor = System.Drawing.Color.Black;
            this.txtAcreedor.Location = new System.Drawing.Point(108, 71);
            this.txtAcreedor.MaxLength = 4;
            this.txtAcreedor.Name = "txtAcreedor";
            this.txtAcreedor.PermitirApostrofo = false;
            this.txtAcreedor.PermitirNegativos = false;
            this.txtAcreedor.Size = new System.Drawing.Size(83, 20);
            this.txtAcreedor.TabIndex = 2;
            this.txtAcreedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAcreedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAcreedor_KeyDown);
            this.txtAcreedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtAcreedor_Validating);
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(578, 19);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRegistro.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Acreedor :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServicio
            // 
            this.txtServicio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServicio.Decimales = 0;
            this.txtServicio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtServicio.ForeColor = System.Drawing.Color.Black;
            this.txtServicio.Location = new System.Drawing.Point(108, 45);
            this.txtServicio.MaxLength = 3;
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.PermitirApostrofo = false;
            this.txtServicio.PermitirNegativos = false;
            this.txtServicio.Size = new System.Drawing.Size(83, 20);
            this.txtServicio.TabIndex = 1;
            this.txtServicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtServicio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtServicio_KeyDown);
            this.txtServicio.Validating += new System.ComponentModel.CancelEventHandler(this.txtServicio_Validating);
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(205, 22);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 12;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(108, 19);
            this.txtFolio.MaxLength = 6;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(83, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tipo Servicio :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCuentasporPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 244);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCuentasporPagar";
            this.Text = "Registro de Pagos de Servicios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCuentasporPagar_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTasaIva)).EndInit();
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
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtAcreedor;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtServicio;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label lblAcreedor;
        private System.Windows.Forms.Label lblServicio;
        private SC_ControlsCS.scTextBoxExt txtRefDocto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaDoc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPorcentajeIva;
        private System.Windows.Forms.NumericUpDown nudTasaIva;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private SC_ControlsCS.scNumericTextBox txtSubTotal;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scComboBoxExt cboMetodoPago;
    }
}