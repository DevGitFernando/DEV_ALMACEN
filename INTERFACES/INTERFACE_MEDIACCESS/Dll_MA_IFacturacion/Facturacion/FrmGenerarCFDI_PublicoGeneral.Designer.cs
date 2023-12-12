namespace Dll_MA_IFacturacion.Facturacion
{
    partial class FrmGenerarCFDI_PublicoGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarCFDI_PublicoGeneral));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSendEmail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirDirectorio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatosSucursal = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboUsosCFDI = new SC_ControlsCS.scComboBoxExt();
            this.lblRazonSocial = new SC_ControlsCS.scLabelExt();
            this.btnCliente = new System.Windows.Forms.Button();
            this.txtRFC = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSerieFactura = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubTotal_NoGravado = new SC_ControlsCS.scNumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubTotal_Gravado = new SC_ControlsCS.scNumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIVA = new SC_ControlsCS.scNumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSubTotal_NoGravado_AntesDescuento = new SC_ControlsCS.scNumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSubTotal_Gravado_AntesDescuento = new SC_ControlsCS.scNumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtImporte_Factura = new SC_ControlsCS.scNumericTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescuento = new SC_ControlsCS.scNumericTextBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosSucursal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnSendEmail,
            this.toolStripSeparator3,
            this.btnAbrirDirectorio,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1301, 25);
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
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar factura electrónica";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(23, 22);
            this.btnValidarDatos.Text = "Vista previa del documento";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // toolStripSeparatorVistaPrevia
            // 
            this.toolStripSeparatorVistaPrevia.Name = "toolStripSeparatorVistaPrevia";
            this.toolStripSeparatorVistaPrevia.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSendEmail.Image = ((System.Drawing.Image)(resources.GetObject("btnSendEmail.Image")));
            this.btnSendEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(23, 22);
            this.btnSendEmail.Text = "Enviar correo electrónico";
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirDirectorio
            // 
            this.btnAbrirDirectorio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirDirectorio.Image")));
            this.btnAbrirDirectorio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirDirectorio.Name = "btnAbrirDirectorio";
            this.btnAbrirDirectorio.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirDirectorio.Text = "Abrir directorio XML - PDF";
            this.btnAbrirDirectorio.Click += new System.EventHandler(this.btnAbrirDirectorio_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDatosSucursal
            // 
            this.FrameDatosSucursal.Controls.Add(this.label12);
            this.FrameDatosSucursal.Controls.Add(this.cboUsosCFDI);
            this.FrameDatosSucursal.Controls.Add(this.lblRazonSocial);
            this.FrameDatosSucursal.Controls.Add(this.btnCliente);
            this.FrameDatosSucursal.Controls.Add(this.txtRFC);
            this.FrameDatosSucursal.Controls.Add(this.label3);
            this.FrameDatosSucursal.Controls.Add(this.label2);
            this.FrameDatosSucursal.Location = new System.Drawing.Point(16, 34);
            this.FrameDatosSucursal.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatosSucursal.Name = "FrameDatosSucursal";
            this.FrameDatosSucursal.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatosSucursal.Size = new System.Drawing.Size(1085, 123);
            this.FrameDatosSucursal.TabIndex = 1;
            this.FrameDatosSucursal.TabStop = false;
            this.FrameDatosSucursal.Text = "Información del Cliente";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 90);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "Uso del CFDI :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboUsosCFDI
            // 
            this.cboUsosCFDI.BackColorEnabled = System.Drawing.Color.White;
            this.cboUsosCFDI.Data = "";
            this.cboUsosCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsosCFDI.Filtro = " 1 = 1";
            this.cboUsosCFDI.FormattingEnabled = true;
            this.cboUsosCFDI.ListaItemsBusqueda = 20;
            this.cboUsosCFDI.Location = new System.Drawing.Point(124, 86);
            this.cboUsosCFDI.Margin = new System.Windows.Forms.Padding(4);
            this.cboUsosCFDI.MostrarToolTip = false;
            this.cboUsosCFDI.Name = "cboUsosCFDI";
            this.cboUsosCFDI.Size = new System.Drawing.Size(735, 24);
            this.cboUsosCFDI.TabIndex = 22;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRazonSocial.Location = new System.Drawing.Point(124, 54);
            this.lblRazonSocial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRazonSocial.MostrarToolTip = false;
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(735, 25);
            this.lblRazonSocial.TabIndex = 14;
            this.lblRazonSocial.Text = "scLabelExt1";
            this.lblRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCliente
            // 
            this.btnCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnCliente.Image")));
            this.btnCliente.Location = new System.Drawing.Point(867, 22);
            this.btnCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(208, 88);
            this.btnCliente.TabIndex = 2;
            this.btnCliente.Text = "Clientes";
            this.btnCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC.Decimales = 2;
            this.txtRFC.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC.ForeColor = System.Drawing.Color.Black;
            this.txtRFC.Location = new System.Drawing.Point(124, 22);
            this.txtRFC.Margin = new System.Windows.Forms.Padding(4);
            this.txtRFC.MaxLength = 13;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.PermitirApostrofo = false;
            this.txtRFC.PermitirNegativos = false;
            this.txtRFC.Size = new System.Drawing.Size(165, 22);
            this.txtRFC.TabIndex = 0;
            this.txtRFC.Text = "123456789012345";
            this.txtRFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRFC.TextChanged += new System.EventHandler(this.txtRFC_TextChanged);
            this.txtRFC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRFC_KeyDown);
            this.txtRFC.Validated += new System.EventHandler(this.txtRFC_Validated);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "RFC :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Razón Social  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSerieFactura);
            this.groupBox2.Controls.Add(this.lblCancelado);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtSubTotal_NoGravado);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtSubTotal_Gravado);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtIVA);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSubTotal_NoGravado_AntesDescuento);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSubTotal_Gravado_AntesDescuento);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtImporte_Factura);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtDescuento);
            this.groupBox2.Controls.Add(this.dtpFechaRegistro);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtFolio);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(16, 163);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1271, 529);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Venta";
            // 
            // lblSerieFactura
            // 
            this.lblSerieFactura.BackColor = System.Drawing.Color.Transparent;
            this.lblSerieFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSerieFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerieFactura.Location = new System.Drawing.Point(907, 22);
            this.lblSerieFactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerieFactura.Name = "lblSerieFactura";
            this.lblSerieFactura.Size = new System.Drawing.Size(61, 25);
            this.lblSerieFactura.TabIndex = 62;
            this.lblSerieFactura.Text = "CANCELADA";
            this.lblSerieFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSerieFactura.Visible = false;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(299, 18);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(560, 25);
            this.lblCancelado.TabIndex = 61;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(839, 378);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(243, 30);
            this.label6.TabIndex = 60;
            this.label6.Text = "Sub-Total Sin Gravar :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubTotal_NoGravado
            // 
            this.txtSubTotal_NoGravado.AllowNegative = true;
            this.txtSubTotal_NoGravado.DigitsInGroup = 3;
            this.txtSubTotal_NoGravado.Flags = 7680;
            this.txtSubTotal_NoGravado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_NoGravado.Location = new System.Drawing.Point(1083, 378);
            this.txtSubTotal_NoGravado.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_NoGravado.MaxDecimalPlaces = 2;
            this.txtSubTotal_NoGravado.MaxLength = 15;
            this.txtSubTotal_NoGravado.MaxWholeDigits = 9;
            this.txtSubTotal_NoGravado.Name = "txtSubTotal_NoGravado";
            this.txtSubTotal_NoGravado.Prefix = "";
            this.txtSubTotal_NoGravado.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_NoGravado.TabIndex = 7;
            this.txtSubTotal_NoGravado.Text = "1.00";
            this.txtSubTotal_NoGravado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(839, 415);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(243, 30);
            this.label7.TabIndex = 58;
            this.label7.Text = "Sub-Total Gravado :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubTotal_Gravado
            // 
            this.txtSubTotal_Gravado.AllowNegative = true;
            this.txtSubTotal_Gravado.DigitsInGroup = 3;
            this.txtSubTotal_Gravado.Flags = 7680;
            this.txtSubTotal_Gravado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_Gravado.Location = new System.Drawing.Point(1083, 415);
            this.txtSubTotal_Gravado.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_Gravado.MaxDecimalPlaces = 2;
            this.txtSubTotal_Gravado.MaxLength = 15;
            this.txtSubTotal_Gravado.MaxWholeDigits = 9;
            this.txtSubTotal_Gravado.Name = "txtSubTotal_Gravado";
            this.txtSubTotal_Gravado.Prefix = "";
            this.txtSubTotal_Gravado.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_Gravado.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_Gravado.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_Gravado.TabIndex = 6;
            this.txtSubTotal_Gravado.Text = "1.00";
            this.txtSubTotal_Gravado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(839, 452);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(243, 30);
            this.label11.TabIndex = 56;
            this.label11.Text = "IVA :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIVA
            // 
            this.txtIVA.AllowNegative = true;
            this.txtIVA.DigitsInGroup = 3;
            this.txtIVA.Flags = 7680;
            this.txtIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIVA.Location = new System.Drawing.Point(1083, 452);
            this.txtIVA.Margin = new System.Windows.Forms.Padding(4);
            this.txtIVA.MaxDecimalPlaces = 2;
            this.txtIVA.MaxLength = 15;
            this.txtIVA.MaxWholeDigits = 9;
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.Prefix = "";
            this.txtIVA.RangeMax = 1.7976931348623157E+308D;
            this.txtIVA.RangeMin = -1.7976931348623157E+308D;
            this.txtIVA.Size = new System.Drawing.Size(172, 29);
            this.txtIVA.TabIndex = 8;
            this.txtIVA.Text = "1.00";
            this.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 418);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(405, 30);
            this.label9.TabIndex = 54;
            this.label9.Text = "Sub-Total Sin Gravar antes de descuento :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Visible = false;
            // 
            // txtSubTotal_NoGravado_AntesDescuento
            // 
            this.txtSubTotal_NoGravado_AntesDescuento.AllowNegative = true;
            this.txtSubTotal_NoGravado_AntesDescuento.DigitsInGroup = 3;
            this.txtSubTotal_NoGravado_AntesDescuento.Flags = 7680;
            this.txtSubTotal_NoGravado_AntesDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_NoGravado_AntesDescuento.Location = new System.Drawing.Point(421, 418);
            this.txtSubTotal_NoGravado_AntesDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_NoGravado_AntesDescuento.MaxDecimalPlaces = 2;
            this.txtSubTotal_NoGravado_AntesDescuento.MaxLength = 15;
            this.txtSubTotal_NoGravado_AntesDescuento.MaxWholeDigits = 9;
            this.txtSubTotal_NoGravado_AntesDescuento.Name = "txtSubTotal_NoGravado_AntesDescuento";
            this.txtSubTotal_NoGravado_AntesDescuento.Prefix = "";
            this.txtSubTotal_NoGravado_AntesDescuento.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado_AntesDescuento.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_NoGravado_AntesDescuento.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_NoGravado_AntesDescuento.TabIndex = 4;
            this.txtSubTotal_NoGravado_AntesDescuento.Text = "1.00";
            this.txtSubTotal_NoGravado_AntesDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotal_NoGravado_AntesDescuento.Visible = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 384);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(405, 30);
            this.label5.TabIndex = 50;
            this.label5.Text = "Sub-Total Gravado antes de descuento :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // txtSubTotal_Gravado_AntesDescuento
            // 
            this.txtSubTotal_Gravado_AntesDescuento.AllowNegative = true;
            this.txtSubTotal_Gravado_AntesDescuento.DigitsInGroup = 3;
            this.txtSubTotal_Gravado_AntesDescuento.Flags = 7680;
            this.txtSubTotal_Gravado_AntesDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal_Gravado_AntesDescuento.Location = new System.Drawing.Point(421, 384);
            this.txtSubTotal_Gravado_AntesDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal_Gravado_AntesDescuento.MaxDecimalPlaces = 2;
            this.txtSubTotal_Gravado_AntesDescuento.MaxLength = 15;
            this.txtSubTotal_Gravado_AntesDescuento.MaxWholeDigits = 9;
            this.txtSubTotal_Gravado_AntesDescuento.Name = "txtSubTotal_Gravado_AntesDescuento";
            this.txtSubTotal_Gravado_AntesDescuento.Prefix = "";
            this.txtSubTotal_Gravado_AntesDescuento.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal_Gravado_AntesDescuento.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal_Gravado_AntesDescuento.Size = new System.Drawing.Size(172, 29);
            this.txtSubTotal_Gravado_AntesDescuento.TabIndex = 3;
            this.txtSubTotal_Gravado_AntesDescuento.Text = "1.00";
            this.txtSubTotal_Gravado_AntesDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotal_Gravado_AntesDescuento.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(839, 486);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(243, 32);
            this.label8.TabIndex = 48;
            this.label8.Text = "Importe :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImporte_Factura
            // 
            this.txtImporte_Factura.AllowNegative = true;
            this.txtImporte_Factura.DigitsInGroup = 3;
            this.txtImporte_Factura.Flags = 7680;
            this.txtImporte_Factura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte_Factura.Location = new System.Drawing.Point(1083, 489);
            this.txtImporte_Factura.Margin = new System.Windows.Forms.Padding(4);
            this.txtImporte_Factura.MaxDecimalPlaces = 2;
            this.txtImporte_Factura.MaxLength = 15;
            this.txtImporte_Factura.MaxWholeDigits = 9;
            this.txtImporte_Factura.Name = "txtImporte_Factura";
            this.txtImporte_Factura.Prefix = "";
            this.txtImporte_Factura.RangeMax = 1.7976931348623157E+308D;
            this.txtImporte_Factura.RangeMin = -1.7976931348623157E+308D;
            this.txtImporte_Factura.Size = new System.Drawing.Size(172, 29);
            this.txtImporte_Factura.TabIndex = 9;
            this.txtImporte_Factura.Text = "1.00";
            this.txtImporte_Factura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 454);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(405, 30);
            this.label10.TabIndex = 46;
            this.label10.Text = "Descuento :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // txtDescuento
            // 
            this.txtDescuento.AllowNegative = true;
            this.txtDescuento.DigitsInGroup = 3;
            this.txtDescuento.Flags = 7680;
            this.txtDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuento.Location = new System.Drawing.Point(421, 454);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescuento.MaxDecimalPlaces = 2;
            this.txtDescuento.MaxLength = 15;
            this.txtDescuento.MaxWholeDigits = 9;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Prefix = "";
            this.txtDescuento.RangeMax = 1.7976931348623157E+308D;
            this.txtDescuento.RangeMin = -1.7976931348623157E+308D;
            this.txtDescuento.Size = new System.Drawing.Size(172, 29);
            this.txtDescuento.TabIndex = 5;
            this.txtDescuento.Text = "1.00";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDescuento.Visible = false;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1135, 22);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1001, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Fecha de Registro :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(124, 18);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(165, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validated += new System.EventHandler(this.txtFolio_Validated);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 36;
            this.label4.Text = "Folio de venta :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(13, 53);
            this.grdProductos.Margin = new System.Windows.Forms.Padding(4);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1243, 320);
            this.grdProductos.TabIndex = 2;
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 15;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(0, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(0, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(1, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(1, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(2, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(2, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(3, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(3, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(4, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(4, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(5, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(5, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(6, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(6, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(7, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(7, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(8, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(8, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(9, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(9, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(10, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(10, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(11, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(11, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(11, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(12, 8).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(12, 9).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.Cells.Get(12, 10).Value = FarPoint.CalcEngine.CalcError.Reference;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Precio base";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Porcentaje";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Precio unitario";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Tasa de iva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Sub-Total";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "IVA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Total";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Unidad de Medida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "SAT_ClaveProductoServicio";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "SAT_UnidadDeMedida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "Producto para facturacion";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
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
            this.grdProductos_Sheet1.Columns.Get(2).Width = 350F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Precio base";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 90F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Porcentaje";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 90F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Precio unitario";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 90F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.DecimalSeparator = ".";
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 70F;
            numberCellType5.DecimalPlaces = 2;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.MaximumValue = 100D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = numberCellType5;
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Tasa de iva";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            currencyCellType1.CurrencySymbol = "$";
            currencyCellType1.DecimalPlaces = 2;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType1;
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "Sub-Total";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 90F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(9).CellType = currencyCellType2;
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "IVA";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 90F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(10).CellType = currencyCellType3;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "Total";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Width = 90F;
            this.grdProductos_Sheet1.Columns.Get(11).CellType = textCellType4;
            this.grdProductos_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.grdProductos_Sheet1.Columns.Get(11).Label = "Unidad de Medida";
            this.grdProductos_Sheet1.Columns.Get(11).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(11).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(11).Width = 80F;
            this.grdProductos_Sheet1.Columns.Get(12).CellType = textCellType5;
            this.grdProductos_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(12).Label = "SAT_ClaveProductoServicio";
            this.grdProductos_Sheet1.Columns.Get(12).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(12).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(12).Width = 135F;
            this.grdProductos_Sheet1.Columns.Get(13).CellType = textCellType6;
            this.grdProductos_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(13).Label = "SAT_UnidadDeMedida";
            this.grdProductos_Sheet1.Columns.Get(13).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(13).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(13).Width = 135F;
            this.grdProductos_Sheet1.Columns.Get(14).CellType = checkBoxCellType1;
            this.grdProductos_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(14).Label = "Producto para facturacion";
            this.grdProductos_Sheet1.Columns.Get(14).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(14).Visible = false;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmProceso
            // 
            this.tmProceso.Enabled = true;
            this.tmProceso.Interval = 500;
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPago);
            this.groupBox1.Controls.Add(this.btnObservacionesGral);
            this.groupBox1.Location = new System.Drawing.Point(1108, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(179, 123);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(13, 67);
            this.btnPago.Margin = new System.Windows.Forms.Padding(4);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(152, 39);
            this.btnPago.TabIndex = 21;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(13, 25);
            this.btnObservacionesGral.Margin = new System.Windows.Forms.Padding(4);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(152, 39);
            this.btnObservacionesGral.TabIndex = 20;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(173, 276);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Size = new System.Drawing.Size(947, 123);
            this.FrameProceso.TabIndex = 16;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 28);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(907, 79);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // FrmGenerarCFDI_PublicoGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 702);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameDatosSucursal);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmGenerarCFDI_PublicoGeneral";
            this.Text = "Facturación de ventas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGenerarCFDI_PublicoGeneral_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosSucursal.ResumeLayout(false);
            this.FrameDatosSucursal.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private System.Windows.Forms.GroupBox FrameDatosSucursal;
        private SC_ControlsCS.scTextBoxExt txtRFC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scNumericTextBox txtDescuento;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scNumericTextBox txtImporte_Factura;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_Gravado_AntesDescuento;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_NoGravado_AntesDescuento;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_NoGravado;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scNumericTextBox txtSubTotal_Gravado;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scNumericTextBox txtIVA;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scLabelExt lblRazonSocial;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPago;
        private System.Windows.Forms.Button btnObservacionesGral;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSendEmail;
        private System.Windows.Forms.ToolStripButton btnAbrirDirectorio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label lblSerieFactura;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scComboBoxExt cboUsosCFDI;
    }
}