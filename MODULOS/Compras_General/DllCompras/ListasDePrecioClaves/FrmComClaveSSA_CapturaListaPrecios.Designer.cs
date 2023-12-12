namespace DllCompras.ListasDePrecioClaves
{
    partial class FrmComClaveSSA_CapturaListaPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComClaveSSA_CapturaListaPrecios));
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType5 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType6 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
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
            this.FrameDatosClave = new System.Windows.Forms.GroupBox();
            this.lblContPaquete = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblIdClave = new System.Windows.Forms.Label();
            this.txtCodClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.FrameDatosPrecio = new System.Windows.Forms.GroupBox();
            this.txtImporte = new SC_ControlsCS.scNumericTextBox();
            this.lblPorcentajeIva = new System.Windows.Forms.Label();
            this.nudTasaIva = new System.Windows.Forms.NumericUpDown();
            this.txtDescuento = new SC_ControlsCS.scNumericTextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrecio = new SC_ControlsCS.scNumericTextBox();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEncClaveSSA = new System.Windows.Forms.Label();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDatosProveedor = new System.Windows.Forms.GroupBox();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosClave.SuspendLayout();
            this.FrameDatosPrecio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTasaIva)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).BeginInit();
            this.FrameDatosProveedor.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(922, 25);
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
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameDatosClave
            // 
            this.FrameDatosClave.Controls.Add(this.lblContPaquete);
            this.FrameDatosClave.Controls.Add(this.label10);
            this.FrameDatosClave.Controls.Add(this.lblPresentacion);
            this.FrameDatosClave.Controls.Add(this.label5);
            this.FrameDatosClave.Controls.Add(this.lblIdClave);
            this.FrameDatosClave.Controls.Add(this.txtCodClaveSSA);
            this.FrameDatosClave.Controls.Add(this.lblClaveSSA);
            this.FrameDatosClave.Controls.Add(this.FrameDatosPrecio);
            this.FrameDatosClave.Controls.Add(this.dtpFechaVigencia);
            this.FrameDatosClave.Controls.Add(this.label1);
            this.FrameDatosClave.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatosClave.Controls.Add(this.label3);
            this.FrameDatosClave.Controls.Add(this.lblEncClaveSSA);
            this.FrameDatosClave.Controls.Add(this.lblDescripcionClave);
            this.FrameDatosClave.Location = new System.Drawing.Point(10, 83);
            this.FrameDatosClave.Name = "FrameDatosClave";
            this.FrameDatosClave.Size = new System.Drawing.Size(904, 176);
            this.FrameDatosClave.TabIndex = 2;
            this.FrameDatosClave.TabStop = false;
            this.FrameDatosClave.Text = "Información de Precio de Clave SSA";
            // 
            // lblContPaquete
            // 
            this.lblContPaquete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContPaquete.Location = new System.Drawing.Point(754, 93);
            this.lblContPaquete.Name = "lblContPaquete";
            this.lblContPaquete.Size = new System.Drawing.Size(138, 20);
            this.lblContPaquete.TabIndex = 20;
            this.lblContPaquete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(669, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Contenido : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(172, 94);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(395, 20);
            this.lblPresentacion.TabIndex = 18;
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(86, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Presentación : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtCodClaveSSA.TabIndex = 1;
            this.txtCodClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodClaveSSA_KeyDown);
            this.txtCodClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdClaveSSA_Validating);
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(316, 20);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(163, 20);
            this.lblClaveSSA.TabIndex = 3;
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameDatosPrecio
            // 
            this.FrameDatosPrecio.Controls.Add(this.txtImporte);
            this.FrameDatosPrecio.Controls.Add(this.lblPorcentajeIva);
            this.FrameDatosPrecio.Controls.Add(this.nudTasaIva);
            this.FrameDatosPrecio.Controls.Add(this.txtDescuento);
            this.FrameDatosPrecio.Controls.Add(this.lblPrecio);
            this.FrameDatosPrecio.Controls.Add(this.label4);
            this.FrameDatosPrecio.Controls.Add(this.label8);
            this.FrameDatosPrecio.Controls.Add(this.label9);
            this.FrameDatosPrecio.Controls.Add(this.txtPrecio);
            this.FrameDatosPrecio.Controls.Add(this.txtIva);
            this.FrameDatosPrecio.Location = new System.Drawing.Point(92, 118);
            this.FrameDatosPrecio.Name = "FrameDatosPrecio";
            this.FrameDatosPrecio.Size = new System.Drawing.Size(800, 50);
            this.FrameDatosPrecio.TabIndex = 9;
            this.FrameDatosPrecio.TabStop = false;
            this.FrameDatosPrecio.Text = "Datos de Precio";
            // 
            // txtImporte
            // 
            this.txtImporte.AllowNegative = true;
            this.txtImporte.DigitsInGroup = 3;
            this.txtImporte.Enabled = false;
            this.txtImporte.Flags = 7680;
            this.txtImporte.Location = new System.Drawing.Point(715, 17);
            this.txtImporte.MaxDecimalPlaces = 4;
            this.txtImporte.MaxLength = 15;
            this.txtImporte.MaxWholeDigits = 9;
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Prefix = "";
            this.txtImporte.RangeMax = 1.7976931348623157E+308;
            this.txtImporte.RangeMin = -1.7976931348623157E+308;
            this.txtImporte.Size = new System.Drawing.Size(69, 20);
            this.txtImporte.TabIndex = 9;
            this.txtImporte.Text = "1.0000";
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPorcentajeIva
            // 
            this.lblPorcentajeIva.Location = new System.Drawing.Point(364, 18);
            this.lblPorcentajeIva.Name = "lblPorcentajeIva";
            this.lblPorcentajeIva.Size = new System.Drawing.Size(45, 18);
            this.lblPorcentajeIva.TabIndex = 4;
            this.lblPorcentajeIva.Text = "% Iva: ";
            this.lblPorcentajeIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudTasaIva
            // 
            this.nudTasaIva.Location = new System.Drawing.Point(411, 18);
            this.nudTasaIva.Name = "nudTasaIva";
            this.nudTasaIva.Size = new System.Drawing.Size(52, 20);
            this.nudTasaIva.TabIndex = 5;
            this.nudTasaIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTasaIva.ValueChanged += new System.EventHandler(this.nudTasaIva_ValueChanged);
            // 
            // txtDescuento
            // 
            this.txtDescuento.AllowNegative = true;
            this.txtDescuento.DigitsInGroup = 3;
            this.txtDescuento.Enabled = false;
            this.txtDescuento.Flags = 7680;
            this.txtDescuento.Location = new System.Drawing.Point(287, 18);
            this.txtDescuento.MaxDecimalPlaces = 2;
            this.txtDescuento.MaxLength = 15;
            this.txtDescuento.MaxWholeDigits = 9;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Prefix = "";
            this.txtDescuento.RangeMax = 1.7976931348623157E+308;
            this.txtDescuento.RangeMin = -1.7976931348623157E+308;
            this.txtDescuento.Size = new System.Drawing.Size(47, 20);
            this.txtDescuento.TabIndex = 3;
            this.txtDescuento.Text = "1.00";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDescuento.TextChanged += new System.EventHandler(this.txtDescuento_TextChanged);
            // 
            // lblPrecio
            // 
            this.lblPrecio.Location = new System.Drawing.Point(5, 22);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(86, 12);
            this.lblPrecio.TabIndex = 0;
            this.lblPrecio.Text = "Precio:";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(213, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Descuento :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(500, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "Iva :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(627, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Precio Unitario :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecio
            // 
            this.txtPrecio.AllowNegative = true;
            this.txtPrecio.DigitsInGroup = 3;
            this.txtPrecio.Flags = 7680;
            this.txtPrecio.Location = new System.Drawing.Point(94, 18);
            this.txtPrecio.MaxDecimalPlaces = 4;
            this.txtPrecio.MaxLength = 15;
            this.txtPrecio.MaxWholeDigits = 9;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Prefix = "";
            this.txtPrecio.RangeMax = 1.7976931348623157E+308;
            this.txtPrecio.RangeMin = -1.7976931348623157E+308;
            this.txtPrecio.Size = new System.Drawing.Size(84, 20);
            this.txtPrecio.TabIndex = 1;
            this.txtPrecio.Text = "1.0000";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecio.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            this.txtPrecio.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrecio_Validating);
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = true;
            this.txtIva.DigitsInGroup = 3;
            this.txtIva.Enabled = false;
            this.txtIva.Flags = 7680;
            this.txtIva.Location = new System.Drawing.Point(537, 18);
            this.txtIva.MaxDecimalPlaces = 4;
            this.txtIva.MaxLength = 15;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308;
            this.txtIva.RangeMin = -1.7976931348623157E+308;
            this.txtIva.Size = new System.Drawing.Size(69, 20);
            this.txtIva.TabIndex = 7;
            this.txtIva.Text = "1.0000";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVigencia.Enabled = false;
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(801, 20);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaVigencia.TabIndex = 7;
            this.dtpFechaVigencia.ValueChanged += new System.EventHandler(this.dtpFechaVigencia_ValueChanged);
            this.dtpFechaVigencia.Validating += new System.ComponentModel.CancelEventHandler(this.dtpFechaVigencia_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(690, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fecha de vigencia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(592, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(490, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEncClaveSSA
            // 
            this.lblEncClaveSSA.Location = new System.Drawing.Point(239, 23);
            this.lblEncClaveSSA.Name = "lblEncClaveSSA";
            this.lblEncClaveSSA.Size = new System.Drawing.Size(74, 13);
            this.lblEncClaveSSA.TabIndex = 2;
            this.lblEncClaveSSA.Text = "Id. Clave SSA :";
            this.lblEncClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Location = new System.Drawing.Point(93, 48);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(799, 39);
            this.lblDescripcionClave.TabIndex = 8;
            this.lblDescripcionClave.Text = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdListaDePrecios);
            this.groupBox2.Location = new System.Drawing.Point(10, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 275);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Precios de Claves SSA";
            // 
            // grdListaDePrecios
            // 
            this.grdListaDePrecios.AccessibleDescription = "grdListaDePrecios, Sheet1, Row 0, Column 0, ";
            this.grdListaDePrecios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdListaDePrecios.Location = new System.Drawing.Point(9, 19);
            this.grdListaDePrecios.Name = "grdListaDePrecios";
            this.grdListaDePrecios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdListaDePrecios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdListaDePrecios_Sheet1});
            this.grdListaDePrecios.Size = new System.Drawing.Size(886, 248);
            this.grdListaDePrecios.TabIndex = 0;
            this.grdListaDePrecios.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdListaDePrecios_CellDoubleClick);
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 11;
            this.grdListaDePrecios_Sheet1.RowCount = 10;
            this.grdListaDePrecios_Sheet1.Cells.Get(0, 2).Value = "ACTIVO";
            this.grdListaDePrecios_Sheet1.Cells.Get(1, 2).Value = "CANCELADO";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClave SSA";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "% Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "TasaIva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha Registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 27F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "IdClave SSA";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Visible = false;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Width = 80F;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType5;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 108F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Status";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 73F;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).CellType = textCellType6;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 269F;
            currencyCellType4.DecimalPlaces = 4;
            currencyCellType4.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType4.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = currencyCellType4;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.MinimumValue = 0;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "% Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Width = 81F;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MinimumValue = 0;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "TasaIva";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            currencyCellType5.DecimalPlaces = 4;
            currencyCellType5.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType5.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).CellType = currencyCellType5;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Width = 70F;
            currencyCellType6.DecimalPlaces = 4;
            currencyCellType6.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType6.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).CellType = currencyCellType6;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Label = "Fecha Registro";
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Width = 90F;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Label = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Width = 90F;
            this.grdListaDePrecios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDatosProveedor
            // 
            this.FrameDatosProveedor.Controls.Add(this.lblNombreProveedor);
            this.FrameDatosProveedor.Controls.Add(this.txtIdProveedor);
            this.FrameDatosProveedor.Controls.Add(this.lblProveedor);
            this.FrameDatosProveedor.Location = new System.Drawing.Point(10, 29);
            this.FrameDatosProveedor.Name = "FrameDatosProveedor";
            this.FrameDatosProveedor.Size = new System.Drawing.Size(904, 51);
            this.FrameDatosProveedor.TabIndex = 1;
            this.FrameDatosProveedor.TabStop = false;
            this.FrameDatosProveedor.Text = "Datos del Proveedor";
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreProveedor.Location = new System.Drawing.Point(172, 20);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(720, 20);
            this.lblNombreProveedor.TabIndex = 3;
            this.lblNombreProveedor.Text = "NombreProveedor";
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(97, 20);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(69, 20);
            this.txtIdProveedor.TabIndex = 2;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_KeyDown);
            this.txtIdProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Validating);
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(19, 23);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(77, 13);
            this.lblProveedor.TabIndex = 1;
            this.lblProveedor.Text = "Id. Proveedor: ";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 546);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(922, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "            Los Precios de Claves SSA aqui Mostrados son por CAJA";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmComClaveSSA_CapturaListaPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 570);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameDatosProveedor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameDatosClave);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmComClaveSSA_CapturaListaPrecios";
            this.Text = "Captura de Precios por Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListaPreciosClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosClave.ResumeLayout(false);
            this.FrameDatosClave.PerformLayout();
            this.FrameDatosPrecio.ResumeLayout(false);
            this.FrameDatosPrecio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTasaIva)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).EndInit();
            this.FrameDatosProveedor.ResumeLayout(false);
            this.FrameDatosProveedor.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameDatosClave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblEncClaveSSA;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label label1;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
        private System.Windows.Forms.GroupBox FrameDatosPrecio;
        private SC_ControlsCS.scNumericTextBox txtDescuento;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scNumericTextBox txtPrecio;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private System.Windows.Forms.GroupBox FrameDatosProveedor;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label lblNombreProveedor;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label lblClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtCodClaveSSA;
        private System.Windows.Forms.NumericUpDown nudTasaIva;
        private System.Windows.Forms.Label lblPorcentajeIva;
        private SC_ControlsCS.scNumericTextBox txtImporte;
        private System.Windows.Forms.Label lblIdClave;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.Label lblContPaquete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label label5;
    }
}