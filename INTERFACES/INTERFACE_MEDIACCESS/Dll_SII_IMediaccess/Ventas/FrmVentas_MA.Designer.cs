namespace Dll_SII_IMediaccess.Ventas
{
    partial class FrmVentas_MA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVentas_MA));
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType11 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType12 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType13 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType14 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType15 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnVale = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblImporteAPagar = new System.Windows.Forms.Label();
            this.txtTotal_a_Pagar = new SC_ControlsCS.scNumericTextBox();
            this.lblDescuentoCopago = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDescuentoCopago = new SC_ControlsCS.scNumericTextBox();
            this.txtSubTotal_NoGravado = new SC_ControlsCS.scNumericTextBox();
            this.txtSubTotal_Gravado = new SC_ControlsCS.scNumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSubTotalGeneral = new System.Windows.Forms.Label();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.txtImporte_Factura = new SC_ControlsCS.scNumericTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIVA = new SC_ControlsCS.scNumericTextBox();
            this.btnCodificacion = new System.Windows.Forms.Button();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCopago = new SC_ControlsCS.scLabelStatus();
            this.lblCopago_Title = new SC_ControlsCS.scLabelStatus();
            this.chkMostrarPrecios = new System.Windows.Forms.CheckBox();
            this.chkTipoImpresion = new System.Windows.Forms.CheckBox();
            this.dtpFechaDeSistema = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecetasElectronicas = new System.Windows.Forms.ToolStripButton();
            this.btnRecetaElectronica_Claves = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_05 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.btnImprimir,
            this.btnVale,
            this.toolStripSeparator3,
            this.btnRecetasElectronicas,
            this.btnRecetaElectronica_Claves,
            this.toolStripSeparator_05});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1243, 25);
            this.toolStripBarraMenu.TabIndex = 4;
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
            this.btnCancelar.Text = "&Cancelar";
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
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnVale
            // 
            this.btnVale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVale.Enabled = false;
            this.btnVale.Image = ((System.Drawing.Image)(resources.GetObject("btnVale.Image")));
            this.btnVale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVale.Name = "btnVale";
            this.btnVale.Size = new System.Drawing.Size(23, 22);
            this.btnVale.Text = "&Imprimir (CTRL + V)";
            this.btnVale.Visible = false;
            this.btnVale.Click += new System.EventHandler(this.btnVale_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblImporteAPagar);
            this.groupBox2.Controls.Add(this.txtTotal_a_Pagar);
            this.groupBox2.Controls.Add(this.lblDescuentoCopago);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDescuentoCopago);
            this.groupBox2.Controls.Add(this.txtSubTotal_NoGravado);
            this.groupBox2.Controls.Add(this.txtSubTotal_Gravado);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblSubTotalGeneral);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.txtImporte_Factura);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtIVA);
            this.groupBox2.Controls.Add(this.btnCodificacion);
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(12, 186);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1219, 495);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Venta";
            // 
            // lblImporteAPagar
            // 
            this.lblImporteAPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImporteAPagar.Location = new System.Drawing.Point(79, 418);
            this.lblImporteAPagar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImporteAPagar.Name = "lblImporteAPagar";
            this.lblImporteAPagar.Size = new System.Drawing.Size(248, 27);
            this.lblImporteAPagar.TabIndex = 42;
            this.lblImporteAPagar.Text = "Importe a pagar :";
            this.lblImporteAPagar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotal_a_Pagar
            // 
            this.txtTotal_a_Pagar.AllowNegative = true;
            this.txtTotal_a_Pagar.DigitsInGroup = 3;
            this.txtTotal_a_Pagar.Flags = 7680;
            this.txtTotal_a_Pagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal_a_Pagar.Location = new System.Drawing.Point(331, 418);
            this.txtTotal_a_Pagar.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal_a_Pagar.MaxDecimalPlaces = 4;
            this.txtTotal_a_Pagar.MaxLength = 15;
            this.txtTotal_a_Pagar.MaxWholeDigits = 9;
            this.txtTotal_a_Pagar.Name = "txtTotal_a_Pagar";
            this.txtTotal_a_Pagar.Prefix = "";
            this.txtTotal_a_Pagar.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal_a_Pagar.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal_a_Pagar.Size = new System.Drawing.Size(172, 29);
            this.txtTotal_a_Pagar.TabIndex = 41;
            this.txtTotal_a_Pagar.Text = "1.0000";
            this.txtTotal_a_Pagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuentoCopago
            // 
            this.lblDescuentoCopago.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuentoCopago.Location = new System.Drawing.Point(79, 383);
            this.lblDescuentoCopago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuentoCopago.Name = "lblDescuentoCopago";
            this.lblDescuentoCopago.Size = new System.Drawing.Size(248, 27);
            this.lblDescuentoCopago.TabIndex = 44;
            this.lblDescuentoCopago.Text = "Descuento copago :";
            this.lblDescuentoCopago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(776, 348);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(257, 27);
            this.label11.TabIndex = 76;
            this.label11.Text = "Sub-Total Sin Gravar :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescuentoCopago
            // 
            this.txtDescuentoCopago.AllowNegative = true;
            this.txtDescuentoCopago.DigitsInGroup = 3;
            this.txtDescuentoCopago.Flags = 7680;
            this.txtDescuentoCopago.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoCopago.Location = new System.Drawing.Point(331, 383);
            this.txtDescuentoCopago.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescuentoCopago.MaxDecimalPlaces = 4;
            this.txtDescuentoCopago.MaxLength = 15;
            this.txtDescuentoCopago.MaxWholeDigits = 9;
            this.txtDescuentoCopago.Name = "txtDescuentoCopago";
            this.txtDescuentoCopago.Prefix = "";
            this.txtDescuentoCopago.RangeMax = 1.7976931348623157E+308D;
            this.txtDescuentoCopago.RangeMin = -1.7976931348623157E+308D;
            this.txtDescuentoCopago.Size = new System.Drawing.Size(172, 29);
            this.txtDescuentoCopago.TabIndex = 43;
            this.txtDescuentoCopago.Text = "1.0000";
            this.txtDescuentoCopago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotal_NoGravado
            // 
            this.txtSubTotal_NoGravado.AllowNegative = true;
            this.txtSubTotal_NoGravado.DigitsInGroup = 3;
            this.txtSubTotal_NoGravado.Flags = 7680;
            this.txtSubTotal_NoGravado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_NoGravado.Location = new System.Drawing.Point(1035, 347);
            this.txtSubTotal_NoGravado.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_NoGravado.MaxDecimalPlaces = 4;
            this.txtSubTotal_NoGravado.MaxLength = 15;
            this.txtSubTotal_NoGravado.MaxWholeDigits = 9;
            this.txtSubTotal_NoGravado.Name = "txtSubTotal_NoGravado";
            this.txtSubTotal_NoGravado.Prefix = "";
            this.txtSubTotal_NoGravado.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_NoGravado.TabIndex = 70;
            this.txtSubTotal_NoGravado.Text = "1.0000";
            this.txtSubTotal_NoGravado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSubTotal_Gravado
            // 
            this.txtSubTotal_Gravado.AllowNegative = true;
            this.txtSubTotal_Gravado.DigitsInGroup = 3;
            this.txtSubTotal_Gravado.Flags = 7680;
            this.txtSubTotal_Gravado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_Gravado.Location = new System.Drawing.Point(1035, 383);
            this.txtSubTotal_Gravado.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_Gravado.MaxDecimalPlaces = 4;
            this.txtSubTotal_Gravado.MaxLength = 15;
            this.txtSubTotal_Gravado.MaxWholeDigits = 9;
            this.txtSubTotal_Gravado.Name = "txtSubTotal_Gravado";
            this.txtSubTotal_Gravado.Prefix = "";
            this.txtSubTotal_Gravado.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_Gravado.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_Gravado.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_Gravado.TabIndex = 69;
            this.txtSubTotal_Gravado.Text = "1.0000";
            this.txtSubTotal_Gravado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(776, 384);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(257, 27);
            this.label12.TabIndex = 75;
            this.label12.Text = "Sub-Total Gravado :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalGeneral
            // 
            this.lblSubTotalGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalGeneral.Location = new System.Drawing.Point(79, 350);
            this.lblSubTotalGeneral.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTotalGeneral.Name = "lblSubTotalGeneral";
            this.lblSubTotalGeneral.Size = new System.Drawing.Size(248, 27);
            this.lblSubTotalGeneral.TabIndex = 40;
            this.lblSubTotalGeneral.Text = "Sub-Total general :";
            this.lblSubTotalGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = true;
            this.txtTotal.DigitsInGroup = 3;
            this.txtTotal.Flags = 7680;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(331, 347);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.MaxDecimalPlaces = 4;
            this.txtTotal.MaxLength = 15;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal.Size = new System.Drawing.Size(172, 29);
            this.txtTotal.TabIndex = 10;
            this.txtTotal.Text = "1.0000";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtImporte_Factura
            // 
            this.txtImporte_Factura.AllowNegative = true;
            this.txtImporte_Factura.DigitsInGroup = 3;
            this.txtImporte_Factura.Flags = 7680;
            this.txtImporte_Factura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte_Factura.Location = new System.Drawing.Point(1035, 454);
            this.txtImporte_Factura.Margin = new System.Windows.Forms.Padding(4);
            this.txtImporte_Factura.MaxDecimalPlaces = 4;
            this.txtImporte_Factura.MaxLength = 15;
            this.txtImporte_Factura.MaxWholeDigits = 9;
            this.txtImporte_Factura.Name = "txtImporte_Factura";
            this.txtImporte_Factura.Prefix = "";
            this.txtImporte_Factura.RangeMax = 1.7976931348623157E+308D;
            this.txtImporte_Factura.RangeMin = -1.7976931348623157E+308D;
            this.txtImporte_Factura.Size = new System.Drawing.Size(172, 29);
            this.txtImporte_Factura.TabIndex = 72;
            this.txtImporte_Factura.Text = "1.0000";
            this.txtImporte_Factura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(776, 454);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(257, 30);
            this.label13.TabIndex = 73;
            this.label13.Text = "Importe :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(776, 420);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(257, 27);
            this.label14.TabIndex = 74;
            this.label14.Text = "IVA :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIVA
            // 
            this.txtIVA.AllowNegative = true;
            this.txtIVA.DigitsInGroup = 3;
            this.txtIVA.Flags = 7680;
            this.txtIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIVA.Location = new System.Drawing.Point(1035, 418);
            this.txtIVA.Margin = new System.Windows.Forms.Padding(4);
            this.txtIVA.MaxDecimalPlaces = 4;
            this.txtIVA.MaxLength = 15;
            this.txtIVA.MaxWholeDigits = 9;
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.Prefix = "";
            this.txtIVA.RangeMax = 1.7976931348623157E+308D;
            this.txtIVA.RangeMin = -1.7976931348623157E+308D;
            this.txtIVA.Size = new System.Drawing.Size(172, 29);
            this.txtIVA.TabIndex = 71;
            this.txtIVA.Text = "1.0000";
            this.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCodificacion
            // 
            this.btnCodificacion.Image = ((System.Drawing.Image)(resources.GetObject("btnCodificacion.Image")));
            this.btnCodificacion.Location = new System.Drawing.Point(16, 22);
            this.btnCodificacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCodificacion.Name = "btnCodificacion";
            this.btnCodificacion.Size = new System.Drawing.Size(45, 46);
            this.btnCodificacion.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnCodificacion, "Lectura de producto codificado");
            this.btnCodificacion.UseVisualStyleBackColor = true;
            this.btnCodificacion.Click += new System.EventHandler(this.btnCodificacion_Click);
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(13, 20);
            this.grdProductos.Margin = new System.Windows.Forms.Padding(4);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1193, 320);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn_1);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff_1);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance_1);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown_1);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 14;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(11, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(11, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(11, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(12, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(12, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(12, 8).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "TasaIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Precio";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ImporteIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ImporteTotal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Captura Por";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "EsIMach4";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Ultimo Costo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "ClaveSSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "Cantidad maxima";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType9.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType9.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 97F;
            textCellType10.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType10.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Código";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType11;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 490F;
            numberCellType7.DecimalPlaces = 2;
            numberCellType7.MaximumValue = 100D;
            numberCellType7.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType7;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "TasaIva";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.DecimalSeparator = ".";
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = 0D;
            numberCellType8.Separator = ",";
            numberCellType8.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType8;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            currencyCellType11.CurrencySymbol = "$";
            currencyCellType11.DecimalPlaces = 2;
            currencyCellType11.DecimalSeparator = ".";
            currencyCellType11.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType11.Separator = ",";
            currencyCellType11.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = currencyCellType11;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Precio";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 90F;
            currencyCellType12.CurrencySymbol = "$";
            currencyCellType12.DecimalPlaces = 2;
            currencyCellType12.DecimalSeparator = ".";
            currencyCellType12.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType12.Separator = ",";
            currencyCellType12.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = currencyCellType12;
            this.grdProductos_Sheet1.Columns.Get(6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 90F;
            currencyCellType13.DecimalPlaces = 4;
            currencyCellType13.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(7).CellType = currencyCellType13;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "ImporteIva";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            currencyCellType14.DecimalPlaces = 4;
            currencyCellType14.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType14;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "ImporteTotal";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 92F;
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Captura Por";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = checkBoxCellType3;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "EsIMach4";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Visible = false;
            currencyCellType15.DecimalPlaces = 4;
            currencyCellType15.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(11).CellType = currencyCellType15;
            this.grdProductos_Sheet1.Columns.Get(11).Label = "Ultimo Costo";
            this.grdProductos_Sheet1.Columns.Get(11).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(12).CellType = textCellType12;
            this.grdProductos_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(12).Label = "ClaveSSA";
            this.grdProductos_Sheet1.Columns.Get(12).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(12).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(12).Width = 100F;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.DecimalSeparator = ".";
            numberCellType9.MaximumValue = 10000000D;
            numberCellType9.MinimumValue = 0D;
            numberCellType9.Separator = ",";
            numberCellType9.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(13).CellType = numberCellType9;
            this.grdProductos_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(13).Label = "Cantidad maxima";
            this.grdProductos_Sheet1.Columns.Get(13).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(13).Visible = false;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCopago);
            this.groupBox1.Controls.Add(this.lblCopago_Title);
            this.groupBox1.Controls.Add(this.chkMostrarPrecios);
            this.groupBox1.Controls.Add(this.chkTipoImpresion);
            this.groupBox1.Controls.Add(this.dtpFechaDeSistema);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.groupBox1.Controls.Add(this.lblSubPro);
            this.groupBox1.Controls.Add(this.txtSubPro);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblPro);
            this.groupBox1.Controls.Add(this.txtPro);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblSubCte);
            this.groupBox1.Controls.Add(this.txtSubCte);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCte);
            this.groupBox1.Controls.Add(this.txtCte);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1219, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales de Venta";
            // 
            // lblCopago
            // 
            this.lblCopago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCopago.Location = new System.Drawing.Point(113, 21);
            this.lblCopago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopago.Name = "lblCopago";
            this.lblCopago.Size = new System.Drawing.Size(481, 26);
            this.lblCopago.TabIndex = 53;
            this.lblCopago.Text = "Elegibilidad : ";
            this.lblCopago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopago_Title
            // 
            this.lblCopago_Title.Location = new System.Drawing.Point(17, 25);
            this.lblCopago_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopago_Title.Name = "lblCopago_Title";
            this.lblCopago_Title.Size = new System.Drawing.Size(92, 18);
            this.lblCopago_Title.TabIndex = 54;
            this.lblCopago_Title.Text = "Copago :";
            this.lblCopago_Title.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMostrarPrecios
            // 
            this.chkMostrarPrecios.Location = new System.Drawing.Point(529, 55);
            this.chkMostrarPrecios.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarPrecios.Name = "chkMostrarPrecios";
            this.chkMostrarPrecios.Size = new System.Drawing.Size(148, 21);
            this.chkMostrarPrecios.TabIndex = 52;
            this.chkMostrarPrecios.Text = "Mostrar precios";
            this.chkMostrarPrecios.UseVisualStyleBackColor = true;
            // 
            // chkTipoImpresion
            // 
            this.chkTipoImpresion.Location = new System.Drawing.Point(685, 28);
            this.chkTipoImpresion.Margin = new System.Windows.Forms.Padding(4);
            this.chkTipoImpresion.Name = "chkTipoImpresion";
            this.chkTipoImpresion.Size = new System.Drawing.Size(237, 25);
            this.chkTipoImpresion.TabIndex = 50;
            this.chkTipoImpresion.Text = "Imprimir Venta a Detalle";
            this.chkTipoImpresion.UseVisualStyleBackColor = true;
            // 
            // dtpFechaDeSistema
            // 
            this.dtpFechaDeSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeSistema.Enabled = false;
            this.dtpFechaDeSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeSistema.Location = new System.Drawing.Point(1081, 25);
            this.dtpFechaDeSistema.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaDeSistema.Name = "dtpFechaDeSistema";
            this.dtpFechaDeSistema.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaDeSistema.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(948, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 49;
            this.label4.Text = "Fecha de Sistema :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(685, 55);
            this.chkMostrarImpresionEnPantalla.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(237, 21);
            this.chkMostrarImpresionEnPantalla.TabIndex = 47;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar Vista Previa Impresión ";
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(808, 114);
            this.lblSubPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(395, 26);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(721, 114);
            this.txtSubPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(77, 22);
            this.txtSubPro.TabIndex = 4;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(612, 117);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(808, 84);
            this.lblPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(395, 26);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(721, 84);
            this.txtPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(77, 22);
            this.txtPro.TabIndex = 3;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(612, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(200, 114);
            this.lblSubCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(395, 26);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(113, 114);
            this.txtSubCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(77, 22);
            this.txtSubCte.TabIndex = 2;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown_1);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 39;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(200, 84);
            this.lblCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(395, 26);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(113, 84);
            this.txtCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(77, 22);
            this.txtCte.TabIndex = 1;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1081, 53);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(948, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(255, 53);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(131, 25);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(113, 53);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.TextChanged += new System.EventHandler(this.txtFolio_TextChanged);
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 688);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1243, 30);
            this.lblMensajes.TabIndex = 10;
            this.lblMensajes.Text = "<F5> Ver Información Adicional de Venta                      <F9> Ver Captura de " +
    "Claves Solicitadas                      <F7> Ver Lotes por Artículo ";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMensajes.Click += new System.EventHandler(this.lblMensajes_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRecetasElectronicas
            // 
            this.btnRecetasElectronicas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetasElectronicas.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetasElectronicas.Image")));
            this.btnRecetasElectronicas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetasElectronicas.Name = "btnRecetasElectronicas";
            this.btnRecetasElectronicas.Size = new System.Drawing.Size(23, 22);
            this.btnRecetasElectronicas.Text = "Recetas electrónicas";
            this.btnRecetasElectronicas.Click += new System.EventHandler(this.btnRecetasElectronicas_Click);
            // 
            // btnRecetaElectronica_Claves
            // 
            this.btnRecetaElectronica_Claves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetaElectronica_Claves.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetaElectronica_Claves.Image")));
            this.btnRecetaElectronica_Claves.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetaElectronica_Claves.Name = "btnRecetaElectronica_Claves";
            this.btnRecetaElectronica_Claves.Size = new System.Drawing.Size(23, 22);
            this.btnRecetaElectronica_Claves.Text = "Claves de receta electrónica";
            this.btnRecetaElectronica_Claves.Click += new System.EventHandler(this.btnRecetaElectronica_Claves_Click);
            // 
            // toolStripSeparator_05
            // 
            this.toolStripSeparator_05.Name = "toolStripSeparator_05";
            this.toolStripSeparator_05.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmVentas_MA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 718);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmVentas_MA";
            this.Text = "Venta a asegurados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVentas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmVentas_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.DateTimePicker dtpFechaDeSistema;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnVale;
        private System.Windows.Forms.CheckBox chkTipoImpresion;
        private System.Windows.Forms.CheckBox chkMostrarPrecios;
        private System.Windows.Forms.Button btnCodificacion;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private System.Windows.Forms.Label lblSubTotalGeneral;
        private SC_ControlsCS.scLabelStatus lblCopago;
        private SC_ControlsCS.scLabelStatus lblCopago_Title;
        private System.Windows.Forms.Label lblImporteAPagar;
        private SC_ControlsCS.scNumericTextBox txtTotal_a_Pagar;
        private System.Windows.Forms.Label lblDescuentoCopago;
        private SC_ControlsCS.scNumericTextBox txtDescuentoCopago;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_NoGravado;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_Gravado;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scNumericTextBox txtImporte_Factura;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private SC_ControlsCS.scNumericTextBox txtIVA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRecetasElectronicas;
        private System.Windows.Forms.ToolStripButton btnRecetaElectronica_Claves;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_05;
    }
}