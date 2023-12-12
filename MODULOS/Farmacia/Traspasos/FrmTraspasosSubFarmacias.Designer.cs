namespace Farmacia.Traspasos
{
    partial class FrmTraspasosSubFarmacias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTraspasosSubFarmacias));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameCapturo = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSubFarmaciaDestino = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSubFarmaciaOrigen = new SC_ControlsCS.scComboBoxExt();
            this.chkTipoImpresion = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubTotal = new SC_ControlsCS.scNumericTextBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCapturo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1084, 25);
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
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
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
            // FrameCapturo
            // 
            this.FrameCapturo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameCapturo.Controls.Add(this.lblPersonal);
            this.FrameCapturo.Controls.Add(this.txtIdPersonal);
            this.FrameCapturo.Controls.Add(this.label12);
            this.FrameCapturo.Location = new System.Drawing.Point(9, 532);
            this.FrameCapturo.Name = "FrameCapturo";
            this.FrameCapturo.Size = new System.Drawing.Size(1067, 49);
            this.FrameCapturo.TabIndex = 3;
            this.FrameCapturo.TabStop = false;
            this.FrameCapturo.Text = "Información de registro de traspaso";
            // 
            // lblPersonal
            // 
            this.lblPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(191, 18);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(860, 21);
            this.lblPersonal.TabIndex = 9;
            this.lblPersonal.Text = "Proveedor :";
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(85, 18);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(100, 20);
            this.txtIdPersonal.TabIndex = 0;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(15, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Personal :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboSubFarmaciaDestino);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboSubFarmaciaOrigen);
            this.groupBox1.Controls.Add(this.chkTipoImpresion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTotal);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtIva);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSubTotal);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 158);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos generales de Traspaso";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "Destino :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubFarmaciaDestino
            // 
            this.cboSubFarmaciaDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubFarmaciaDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubFarmaciaDestino.Data = "";
            this.cboSubFarmaciaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubFarmaciaDestino.Filtro = " 1 = 1";
            this.cboSubFarmaciaDestino.FormattingEnabled = true;
            this.cboSubFarmaciaDestino.ListaItemsBusqueda = 20;
            this.cboSubFarmaciaDestino.Location = new System.Drawing.Point(97, 75);
            this.cboSubFarmaciaDestino.MostrarToolTip = false;
            this.cboSubFarmaciaDestino.Name = "cboSubFarmaciaDestino";
            this.cboSubFarmaciaDestino.Size = new System.Drawing.Size(735, 21);
            this.cboSubFarmaciaDestino.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Origen :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubFarmaciaOrigen
            // 
            this.cboSubFarmaciaOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubFarmaciaOrigen.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubFarmaciaOrigen.Data = "";
            this.cboSubFarmaciaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubFarmaciaOrigen.Filtro = " 1 = 1";
            this.cboSubFarmaciaOrigen.FormattingEnabled = true;
            this.cboSubFarmaciaOrigen.ListaItemsBusqueda = 20;
            this.cboSubFarmaciaOrigen.Location = new System.Drawing.Point(97, 47);
            this.cboSubFarmaciaOrigen.MostrarToolTip = false;
            this.cboSubFarmaciaOrigen.Name = "cboSubFarmaciaOrigen";
            this.cboSubFarmaciaOrigen.Size = new System.Drawing.Size(735, 21);
            this.cboSubFarmaciaOrigen.TabIndex = 1;
            this.cboSubFarmaciaOrigen.SelectedIndexChanged += new System.EventHandler(this.cboSubFarmaciaOrigen_SelectedIndexChanged);
            // 
            // chkTipoImpresion
            // 
            this.chkTipoImpresion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTipoImpresion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.Location = new System.Drawing.Point(881, 20);
            this.chkTipoImpresion.Name = "chkTipoImpresion";
            this.chkTipoImpresion.Size = new System.Drawing.Size(176, 17);
            this.chkTipoImpresion.TabIndex = 8;
            this.chkTipoImpresion.Text = "Imprimir Traspaso en Ticket";
            this.chkTipoImpresion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(853, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 38;
            this.label9.Text = "Total :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = false;
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.DigitsInGroup = 0;
            this.txtTotal.Flags = 65536;
            this.txtTotal.Location = new System.Drawing.Point(957, 129);
            this.txtTotal.MaxDecimalPlaces = 4;
            this.txtTotal.MaxLength = 8;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.Text = "1";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTotal.Validating += new System.ComponentModel.CancelEventHandler(this.txtTotal_Validating);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(853, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 37;
            this.label8.Text = "Iva :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = false;
            this.txtIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIva.DigitsInGroup = 0;
            this.txtIva.Flags = 65536;
            this.txtIva.Location = new System.Drawing.Point(957, 107);
            this.txtIva.MaxDecimalPlaces = 4;
            this.txtIva.MaxLength = 8;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308D;
            this.txtIva.RangeMin = -1.7976931348623157E+308D;
            this.txtIva.Size = new System.Drawing.Size(100, 20);
            this.txtIva.TabIndex = 6;
            this.txtIva.Text = "1";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIva.Validating += new System.ComponentModel.CancelEventHandler(this.txtIva_Validating);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(853, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 36;
            this.label7.Text = "Sub-Total :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.AllowNegative = false;
            this.txtSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubTotal.DigitsInGroup = 0;
            this.txtSubTotal.Flags = 65536;
            this.txtSubTotal.Location = new System.Drawing.Point(957, 84);
            this.txtSubTotal.MaxDecimalPlaces = 4;
            this.txtSubTotal.MaxLength = 8;
            this.txtSubTotal.MaxWholeDigits = 9;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Prefix = "";
            this.txtSubTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal.Size = new System.Drawing.Size(100, 20);
            this.txtSubTotal.TabIndex = 5;
            this.txtSubTotal.Text = "1";
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubTotal.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubTotal_Validating);
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(957, 44);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaRegistro.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(853, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(97, 102);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(735, 47);
            this.txtObservaciones.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 14);
            this.label10.TabIndex = 26;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(203, 21);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 25;
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
            this.txtFolio.Location = new System.Drawing.Point(97, 21);
            this.txtFolio.MaxLength = 11;
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
            this.label1.Location = new System.Drawing.Point(55, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(9, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1067, 339);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Traspaso";
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
            this.grdProductos.Size = new System.Drawing.Size(1048, 315);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
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
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "TasaIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Costo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ImporteIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ImporteTotal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Captura Por";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "EsIMach4";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 97F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Código";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 358F;
            numberCellType1.DecimalPlaces = 2;
            numberCellType1.MaximumValue = 100D;
            numberCellType1.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "TasaIva";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = currencyCellType1;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Costo";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 80F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.DecimalSeparator = ".";
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.Separator = ",";
            currencyCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = currencyCellType2;
            this.grdProductos_Sheet1.Columns.Get(6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 80F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(7).CellType = currencyCellType3;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "ImporteIva";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            currencyCellType4.DecimalPlaces = 4;
            currencyCellType4.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType4;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "ImporteTotal";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 92F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 5D;
            numberCellType3.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(9).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Captura Por";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = checkBoxCellType1;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "EsIMach4";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
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
            this.label11.Location = new System.Drawing.Point(0, 587);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1084, 24);
            this.label11.TabIndex = 10;
            this.label11.Text = "<F7> Ver lotes a artículo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmTraspasosSubFarmacias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameCapturo);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmTraspasosSubFarmacias";
            this.Text = "Registro de Traspasos Internos entre Sub-Farmacias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTraspasosSubAlmacen_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCapturo.ResumeLayout(false);
            this.FrameCapturo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameCapturo;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scNumericTextBox txtSubTotal;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkTipoImpresion;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboSubFarmaciaOrigen;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboSubFarmaciaDestino;
    }
}