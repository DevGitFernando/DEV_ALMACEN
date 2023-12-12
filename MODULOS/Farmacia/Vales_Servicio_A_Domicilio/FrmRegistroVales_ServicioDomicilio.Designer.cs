namespace Farmacia.Vales_Servicio_A_Domicilio
{
    partial class FrmRegistroVales_ServicioDomicilio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroVales_ServicioDomicilio));
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType11 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType12 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType13 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType14 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType15 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_02 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_03 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_04 = new System.Windows.Forms.ToolStripSeparator();
            this.btnServicioADomicilio = new System.Windows.Forms.ToolStripButton();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.lblBeneficiario = new System.Windows.Forms.Label();
            this.lblTipoSurt = new System.Windows.Forms.Label();
            this.chkEsReembolso = new System.Windows.Forms.CheckBox();
            this.txtVale = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.txtSubTotal = new SC_ControlsCS.scNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReferenciaDocto = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label11 = new System.Windows.Forms.Label();
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator_01,
            this.btnGuardar,
            this.toolStripSeparator_02,
            this.btnCancelar,
            this.toolStripSeparator_03,
            this.btnImprimir,
            this.toolStripSeparator_04,
            this.btnServicioADomicilio});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // toolStripSeparator_01
            // 
            this.toolStripSeparator_01.Name = "toolStripSeparator_01";
            this.toolStripSeparator_01.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator_02
            // 
            this.toolStripSeparator_02.Name = "toolStripSeparator_02";
            this.toolStripSeparator_02.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator_03
            // 
            this.toolStripSeparator_03.Name = "toolStripSeparator_03";
            this.toolStripSeparator_03.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator_04
            // 
            this.toolStripSeparator_04.Name = "toolStripSeparator_04";
            this.toolStripSeparator_04.Size = new System.Drawing.Size(6, 25);
            // 
            // btnServicioADomicilio
            // 
            this.btnServicioADomicilio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnServicioADomicilio.Enabled = false;
            this.btnServicioADomicilio.Image = ((System.Drawing.Image)(resources.GetObject("btnServicioADomicilio.Image")));
            this.btnServicioADomicilio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnServicioADomicilio.Name = "btnServicioADomicilio";
            this.btnServicioADomicilio.Size = new System.Drawing.Size(23, 22);
            this.btnServicioADomicilio.Text = "&Imprimir (CTRL + P)";
            this.btnServicioADomicilio.Visible = false;
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEncabezado.Controls.Add(this.lblBeneficiario);
            this.FrameEncabezado.Controls.Add(this.lblTipoSurt);
            this.FrameEncabezado.Controls.Add(this.chkEsReembolso);
            this.FrameEncabezado.Controls.Add(this.txtVale);
            this.FrameEncabezado.Controls.Add(this.label13);
            this.FrameEncabezado.Controls.Add(this.txtTotal);
            this.FrameEncabezado.Controls.Add(this.txtIva);
            this.FrameEncabezado.Controls.Add(this.txtSubTotal);
            this.FrameEncabezado.Controls.Add(this.label9);
            this.FrameEncabezado.Controls.Add(this.label8);
            this.FrameEncabezado.Controls.Add(this.label7);
            this.FrameEncabezado.Controls.Add(this.txtReferenciaDocto);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.lblCancelado);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(9, 28);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(967, 98);
            this.FrameEncabezado.TabIndex = 0;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales";
            // 
            // lblBeneficiario
            // 
            this.lblBeneficiario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBeneficiario.BackColor = System.Drawing.Color.Transparent;
            this.lblBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficiario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeneficiario.Location = new System.Drawing.Point(203, 42);
            this.lblBeneficiario.Name = "lblBeneficiario";
            this.lblBeneficiario.Size = new System.Drawing.Size(568, 20);
            this.lblBeneficiario.TabIndex = 50;
            this.lblBeneficiario.Text = "Beneficiario";
            this.lblBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTipoSurt
            // 
            this.lblTipoSurt.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoSurt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoSurt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoSurt.Location = new System.Drawing.Point(203, 66);
            this.lblTipoSurt.Name = "lblTipoSurt";
            this.lblTipoSurt.Size = new System.Drawing.Size(131, 20);
            this.lblTipoSurt.TabIndex = 49;
            this.lblTipoSurt.Text = "TIPO SURT";
            this.lblTipoSurt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTipoSurt.Visible = false;
            // 
            // chkEsReembolso
            // 
            this.chkEsReembolso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsReembolso.Enabled = false;
            this.chkEsReembolso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsReembolso.Location = new System.Drawing.Point(442, 17);
            this.chkEsReembolso.Name = "chkEsReembolso";
            this.chkEsReembolso.Size = new System.Drawing.Size(111, 18);
            this.chkEsReembolso.TabIndex = 48;
            this.chkEsReembolso.Text = "Es Reembolso";
            this.chkEsReembolso.UseVisualStyleBackColor = true;
            this.chkEsReembolso.Visible = false;
            this.chkEsReembolso.CheckedChanged += new System.EventHandler(this.chkEsReembolso_CheckedChanged);
            // 
            // txtVale
            // 
            this.txtVale.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtVale.Decimales = 2;
            this.txtVale.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtVale.ForeColor = System.Drawing.Color.Black;
            this.txtVale.Location = new System.Drawing.Point(97, 42);
            this.txtVale.MaxLength = 8;
            this.txtVale.Name = "txtVale";
            this.txtVale.PermitirApostrofo = false;
            this.txtVale.PermitirNegativos = false;
            this.txtVale.Size = new System.Drawing.Size(100, 20);
            this.txtVale.TabIndex = 6;
            this.txtVale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVale.Validating += new System.ComponentModel.CancelEventHandler(this.txtVale_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(30, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "Folio Vale:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = true;
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.DigitsInGroup = 3;
            this.txtTotal.Enabled = false;
            this.txtTotal.Flags = 7680;
            this.txtTotal.Location = new System.Drawing.Point(857, 66);
            this.txtTotal.MaxDecimalPlaces = 4;
            this.txtTotal.MaxLength = 15;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.Text = "1.0000";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.Visible = false;
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = true;
            this.txtIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIva.DigitsInGroup = 3;
            this.txtIva.Enabled = false;
            this.txtIva.Flags = 7680;
            this.txtIva.Location = new System.Drawing.Point(857, 42);
            this.txtIva.MaxDecimalPlaces = 4;
            this.txtIva.MaxLength = 15;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308D;
            this.txtIva.RangeMin = -1.7976931348623157E+308D;
            this.txtIva.Size = new System.Drawing.Size(100, 20);
            this.txtIva.TabIndex = 8;
            this.txtIva.Text = "1.0000";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIva.Visible = false;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.AllowNegative = true;
            this.txtSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubTotal.DigitsInGroup = 3;
            this.txtSubTotal.Enabled = false;
            this.txtSubTotal.Flags = 7680;
            this.txtSubTotal.Location = new System.Drawing.Point(857, 18);
            this.txtSubTotal.MaxDecimalPlaces = 4;
            this.txtSubTotal.MaxLength = 15;
            this.txtSubTotal.MaxWholeDigits = 9;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Prefix = "";
            this.txtSubTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubTotal.TabIndex = 7;
            this.txtSubTotal.Text = "1.0000";
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotal.Visible = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(777, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "Total :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Visible = false;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(777, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "Iva :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(777, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Sub-Total :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // txtReferenciaDocto
            // 
            this.txtReferenciaDocto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaDocto.Decimales = 2;
            this.txtReferenciaDocto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaDocto.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaDocto.Location = new System.Drawing.Point(97, 66);
            this.txtReferenciaDocto.MaxLength = 20;
            this.txtReferenciaDocto.Name = "txtReferenciaDocto";
            this.txtReferenciaDocto.PermitirApostrofo = false;
            this.txtReferenciaDocto.PermitirNegativos = false;
            this.txtReferenciaDocto.Size = new System.Drawing.Size(100, 20);
            this.txtReferenciaDocto.TabIndex = 3;
            this.txtReferenciaDocto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReferenciaDocto.Validating += new System.ComponentModel.CancelEventHandler(this.txtReferenciaDocto_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Referencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(671, 16);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(562, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(203, 18);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(131, 20);
            this.lblCancelado.TabIndex = 5;
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
            this.txtFolio.Location = new System.Drawing.Point(97, 18);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(54, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(9, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(967, 404);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Registro";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(9, 19);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(948, 378);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdProductos_CellClick);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 11;
            this.grdProductos_Sheet1.RowCount = 11;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 9).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Tasa Iva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "P. Máximo Público Actual";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Costo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ImporteIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ImporteTotal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Captura Por";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType7.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType7.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 150F;
            textCellType8.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType8.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Código";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType9;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 500F;
            numberCellType7.DecimalPlaces = 0;
            numberCellType7.MaximumValue = 100D;
            numberCellType7.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType7;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Tasa Iva";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 40F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.DecimalSeparator = ".";
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = 0D;
            numberCellType8.Separator = ",";
            numberCellType8.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType8;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 115F;
            currencyCellType11.DecimalSeparator = ".";
            currencyCellType11.Separator = ",";
            this.grdProductos_Sheet1.Columns.Get(5).CellType = currencyCellType11;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "P. Máximo Público Actual";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 85F;
            currencyCellType12.DecimalPlaces = 4;
            currencyCellType12.DecimalSeparator = ".";
            currencyCellType12.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType12.Separator = ",";
            currencyCellType12.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = currencyCellType12;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Costo";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 80F;
            currencyCellType13.DecimalPlaces = 4;
            currencyCellType13.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType13.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = currencyCellType13;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "(RC[-3]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 90F;
            currencyCellType14.DecimalPlaces = 4;
            currencyCellType14.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType14.ShowCurrencySymbol = false;
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType14;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(8).Label = "ImporteIva";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).Visible = false;
            currencyCellType15.DecimalPlaces = 4;
            currencyCellType15.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType15.ShowCurrencySymbol = false;
            this.grdProductos_Sheet1.Columns.Get(9).CellType = currencyCellType15;
            this.grdProductos_Sheet1.Columns.Get(9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(9).Label = "ImporteTotal";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 92F;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = numberCellType9;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "Captura Por";
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Visible = false;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 537);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(984, 24);
            this.label11.TabIndex = 9;
            this.label11.Text = " <F7> Ver / Agregar lotes a artículo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmRegistroVales_ServicioDomicilio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRegistroVales_ServicioDomicilio";
            this.Text = "Registro de Servicio a Domicilio Vales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistroVales_ServicioDomicilio_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_01;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_02;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_03;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtReferenciaDocto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private SC_ControlsCS.scNumericTextBox txtSubTotal;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtVale;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.CheckBox chkEsReembolso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_04;
        private System.Windows.Forms.ToolStripButton btnServicioADomicilio;
        private System.Windows.Forms.Label lblTipoSurt;
        private System.Windows.Forms.Label lblBeneficiario;
    }
}