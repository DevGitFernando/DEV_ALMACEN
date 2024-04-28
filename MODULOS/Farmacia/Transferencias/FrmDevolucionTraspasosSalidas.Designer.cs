namespace Farmacia.Transferencias
{
    partial class FrmDevolucionTraspasosSalidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDevolucionTraspasosSalidas));
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMotivosDev = new System.Windows.Forms.ToolStripButton();
            this.FrameInformacionRegistro = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.FrameDatosGenerales = new System.Windows.Forms.GroupBox();
            this.linkUrlFarmacia = new System.Windows.Forms.LinkLabel();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblTitulo__Estado = new System.Windows.Forms.Label();
            this.chkTipoImpresion = new System.Windows.Forms.CheckBox();
            this.lblTitulo__Total = new System.Windows.Forms.Label();
            this.txtTotal = new SC_ControlsCS.scNumericTextBox();
            this.lblTitulo__IVA = new System.Windows.Forms.Label();
            this.txtIva = new SC_ControlsCS.scNumericTextBox();
            this.lblTitulo__SubTotal = new System.Windows.Forms.Label();
            this.txtSubTotal = new SC_ControlsCS.scNumericTextBox();
            this.lblFarmaciaDestino = new System.Windows.Forms.Label();
            this.txtFarmaciaDestino = new SC_ControlsCS.scTextBoxExt();
            this.lblTitulo__FarmaciaDestino = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.lblTitulo__FechaRegistro = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.lblTitulo__Observaciones = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.lblTitulo__Folio = new System.Windows.Forms.Label();
            this.FrameDetallesTransferencia = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnCodificacion = new System.Windows.Forms.Button();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.lblMensaje_03 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacionRegistro.SuspendLayout();
            this.FrameDatosGenerales.SuspendLayout();
            this.FrameDetallesTransferencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator3,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnMotivosDev});
            this.toolStripBarraMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1352, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(54, 55);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 4);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(54, 55);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(12, 4);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 4);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 4);
            // 
            // btnMotivosDev
            // 
            this.btnMotivosDev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMotivosDev.Image = ((System.Drawing.Image)(resources.GetObject("btnMotivosDev.Image")));
            this.btnMotivosDev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMotivosDev.Name = "btnMotivosDev";
            this.btnMotivosDev.Size = new System.Drawing.Size(54, 55);
            this.btnMotivosDev.Text = "Seleccionar Motivos Devolución";
            this.btnMotivosDev.Click += new System.EventHandler(this.btnMotivosDev_Click);
            // 
            // FrameInformacionRegistro
            // 
            this.FrameInformacionRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameInformacionRegistro.Controls.Add(this.lblPersonal);
            this.FrameInformacionRegistro.Controls.Add(this.txtIdPersonal);
            this.FrameInformacionRegistro.Controls.Add(this.label12);
            this.FrameInformacionRegistro.Location = new System.Drawing.Point(13, 698);
            this.FrameInformacionRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.FrameInformacionRegistro.Name = "FrameInformacionRegistro";
            this.FrameInformacionRegistro.Padding = new System.Windows.Forms.Padding(4);
            this.FrameInformacionRegistro.Size = new System.Drawing.Size(1329, 60);
            this.FrameInformacionRegistro.TabIndex = 3;
            this.FrameInformacionRegistro.TabStop = false;
            this.FrameInformacionRegistro.Text = "Información de Registro de Transferencia";
            // 
            // lblPersonal
            // 
            this.lblPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(255, 22);
            this.lblPersonal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(1061, 26);
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
            this.txtIdPersonal.Location = new System.Drawing.Point(113, 22);
            this.txtIdPersonal.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(132, 22);
            this.txtIdPersonal.TabIndex = 0;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(20, 27);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 15);
            this.label12.TabIndex = 8;
            this.label12.Text = "Personal :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatosGenerales
            // 
            this.FrameDatosGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatosGenerales.Controls.Add(this.linkUrlFarmacia);
            this.FrameDatosGenerales.Controls.Add(this.cboEstados);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__Estado);
            this.FrameDatosGenerales.Controls.Add(this.chkTipoImpresion);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__Total);
            this.FrameDatosGenerales.Controls.Add(this.txtTotal);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__IVA);
            this.FrameDatosGenerales.Controls.Add(this.txtIva);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__SubTotal);
            this.FrameDatosGenerales.Controls.Add(this.txtSubTotal);
            this.FrameDatosGenerales.Controls.Add(this.lblFarmaciaDestino);
            this.FrameDatosGenerales.Controls.Add(this.txtFarmaciaDestino);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__FarmaciaDestino);
            this.FrameDatosGenerales.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__FechaRegistro);
            this.FrameDatosGenerales.Controls.Add(this.txtObservaciones);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__Observaciones);
            this.FrameDatosGenerales.Controls.Add(this.lblCancelado);
            this.FrameDatosGenerales.Controls.Add(this.txtFolio);
            this.FrameDatosGenerales.Controls.Add(this.lblTitulo__Folio);
            this.FrameDatosGenerales.Location = new System.Drawing.Point(13, 62);
            this.FrameDatosGenerales.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatosGenerales.Name = "FrameDatosGenerales";
            this.FrameDatosGenerales.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatosGenerales.Size = new System.Drawing.Size(1329, 169);
            this.FrameDatosGenerales.TabIndex = 1;
            this.FrameDatosGenerales.TabStop = false;
            this.FrameDatosGenerales.Text = "Información";
            // 
            // linkUrlFarmacia
            // 
            this.linkUrlFarmacia.Location = new System.Drawing.Point(613, 257);
            this.linkUrlFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkUrlFarmacia.Name = "linkUrlFarmacia";
            this.linkUrlFarmacia.Size = new System.Drawing.Size(152, 25);
            this.linkUrlFarmacia.TabIndex = 65;
            this.linkUrlFarmacia.TabStop = true;
            this.linkUrlFarmacia.Text = "linkLabel1";
            this.linkUrlFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkUrlFarmacia.Visible = false;
            this.linkUrlFarmacia.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUrlFarmacia_LinkClicked);
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(415, 11);
            this.cboEstados.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(140, 24);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.Visible = false;
            // 
            // lblTitulo__Estado
            // 
            this.lblTitulo__Estado.Location = new System.Drawing.Point(291, 15);
            this.lblTitulo__Estado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__Estado.Name = "lblTitulo__Estado";
            this.lblTitulo__Estado.Size = new System.Drawing.Size(127, 15);
            this.lblTitulo__Estado.TabIndex = 48;
            this.lblTitulo__Estado.Text = "Estado :";
            this.lblTitulo__Estado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTitulo__Estado.Visible = false;
            // 
            // chkTipoImpresion
            // 
            this.chkTipoImpresion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTipoImpresion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.Location = new System.Drawing.Point(458, 216);
            this.chkTipoImpresion.Margin = new System.Windows.Forms.Padding(4);
            this.chkTipoImpresion.Name = "chkTipoImpresion";
            this.chkTipoImpresion.Size = new System.Drawing.Size(214, 21);
            this.chkTipoImpresion.TabIndex = 11;
            this.chkTipoImpresion.Text = "Imprimir Traspaso en Ticket";
            this.chkTipoImpresion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.UseVisualStyleBackColor = true;
            this.chkTipoImpresion.Visible = false;
            // 
            // lblTitulo__Total
            // 
            this.lblTitulo__Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo__Total.Location = new System.Drawing.Point(1055, 136);
            this.lblTitulo__Total.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__Total.Name = "lblTitulo__Total";
            this.lblTitulo__Total.Size = new System.Drawing.Size(111, 15);
            this.lblTitulo__Total.TabIndex = 38;
            this.lblTitulo__Total.Text = "Total :";
            this.lblTitulo__Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotal
            // 
            this.txtTotal.AllowNegative = false;
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.DecimalPoint = '.';
            this.txtTotal.DigitsInGroup = 3;
            this.txtTotal.Double = 1D;
            this.txtTotal.Flags = 65536;
            this.txtTotal.GroupSeparator = ',';
            this.txtTotal.Int = 0;
            this.txtTotal.Location = new System.Drawing.Point(1170, 131);
            this.txtTotal.Long = ((long)(0));
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.MaxDecimalPlaces = 4;
            this.txtTotal.MaxLength = 15;
            this.txtTotal.MaxWholeDigits = 9;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.NegativeSign = '-';
            this.txtTotal.Prefix = "";
            this.txtTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtTotal.Size = new System.Drawing.Size(132, 22);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.Text = "1.0000";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.Validating += new System.ComponentModel.CancelEventHandler(this.txtTotal_Validating);
            // 
            // lblTitulo__IVA
            // 
            this.lblTitulo__IVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo__IVA.Location = new System.Drawing.Point(1055, 109);
            this.lblTitulo__IVA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__IVA.Name = "lblTitulo__IVA";
            this.lblTitulo__IVA.Size = new System.Drawing.Size(111, 15);
            this.lblTitulo__IVA.TabIndex = 37;
            this.lblTitulo__IVA.Text = "Iva :";
            this.lblTitulo__IVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIva
            // 
            this.txtIva.AllowNegative = false;
            this.txtIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIva.DecimalPoint = '.';
            this.txtIva.DigitsInGroup = 3;
            this.txtIva.Double = 1D;
            this.txtIva.Flags = 65536;
            this.txtIva.GroupSeparator = ',';
            this.txtIva.Int = 0;
            this.txtIva.Location = new System.Drawing.Point(1170, 104);
            this.txtIva.Long = ((long)(0));
            this.txtIva.Margin = new System.Windows.Forms.Padding(4);
            this.txtIva.MaxDecimalPlaces = 4;
            this.txtIva.MaxLength = 15;
            this.txtIva.MaxWholeDigits = 9;
            this.txtIva.Name = "txtIva";
            this.txtIva.NegativeSign = '-';
            this.txtIva.Prefix = "";
            this.txtIva.RangeMax = 1.7976931348623157E+308D;
            this.txtIva.RangeMin = -1.7976931348623157E+308D;
            this.txtIva.Size = new System.Drawing.Size(132, 22);
            this.txtIva.TabIndex = 6;
            this.txtIva.Text = "1.0000";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIva.Validating += new System.ComponentModel.CancelEventHandler(this.txtIva_Validating);
            // 
            // lblTitulo__SubTotal
            // 
            this.lblTitulo__SubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo__SubTotal.Location = new System.Drawing.Point(1055, 81);
            this.lblTitulo__SubTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__SubTotal.Name = "lblTitulo__SubTotal";
            this.lblTitulo__SubTotal.Size = new System.Drawing.Size(111, 15);
            this.lblTitulo__SubTotal.TabIndex = 36;
            this.lblTitulo__SubTotal.Text = "Sub-Total :";
            this.lblTitulo__SubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.AllowNegative = false;
            this.txtSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubTotal.DecimalPoint = '.';
            this.txtSubTotal.DigitsInGroup = 3;
            this.txtSubTotal.Double = 1D;
            this.txtSubTotal.Flags = 65536;
            this.txtSubTotal.GroupSeparator = ',';
            this.txtSubTotal.Int = 0;
            this.txtSubTotal.Location = new System.Drawing.Point(1170, 76);
            this.txtSubTotal.Long = ((long)(0));
            this.txtSubTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubTotal.MaxDecimalPlaces = 4;
            this.txtSubTotal.MaxLength = 15;
            this.txtSubTotal.MaxWholeDigits = 9;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.NegativeSign = '-';
            this.txtSubTotal.Prefix = "";
            this.txtSubTotal.RangeMax = 1.7976931348623157E+308D;
            this.txtSubTotal.RangeMin = -1.7976931348623157E+308D;
            this.txtSubTotal.Size = new System.Drawing.Size(132, 22);
            this.txtSubTotal.TabIndex = 5;
            this.txtSubTotal.Text = "1.0000";
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSubTotal.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubTotal_Validating);
            // 
            // lblFarmaciaDestino
            // 
            this.lblFarmaciaDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFarmaciaDestino.Location = new System.Drawing.Point(276, 56);
            this.lblFarmaciaDestino.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmaciaDestino.Name = "lblFarmaciaDestino";
            this.lblFarmaciaDestino.Size = new System.Drawing.Size(506, 26);
            this.lblFarmaciaDestino.TabIndex = 32;
            this.lblFarmaciaDestino.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmaciaDestino
            // 
            this.txtFarmaciaDestino.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmaciaDestino.Decimales = 2;
            this.txtFarmaciaDestino.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmaciaDestino.ForeColor = System.Drawing.Color.Black;
            this.txtFarmaciaDestino.Location = new System.Drawing.Point(135, 58);
            this.txtFarmaciaDestino.Margin = new System.Windows.Forms.Padding(4);
            this.txtFarmaciaDestino.MaxLength = 8;
            this.txtFarmaciaDestino.Name = "txtFarmaciaDestino";
            this.txtFarmaciaDestino.PermitirApostrofo = false;
            this.txtFarmaciaDestino.PermitirNegativos = false;
            this.txtFarmaciaDestino.Size = new System.Drawing.Size(132, 22);
            this.txtFarmaciaDestino.TabIndex = 2;
            this.txtFarmaciaDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmaciaDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmaciaDestino_KeyDown);
            this.txtFarmaciaDestino.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmaciaDestino_Validating);
            // 
            // lblTitulo__FarmaciaDestino
            // 
            this.lblTitulo__FarmaciaDestino.Location = new System.Drawing.Point(7, 60);
            this.lblTitulo__FarmaciaDestino.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__FarmaciaDestino.Name = "lblTitulo__FarmaciaDestino";
            this.lblTitulo__FarmaciaDestino.Size = new System.Drawing.Size(127, 20);
            this.lblTitulo__FarmaciaDestino.TabIndex = 31;
            this.lblTitulo__FarmaciaDestino.Text = "Farmacia Destino :";
            this.lblTitulo__FarmaciaDestino.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1170, 27);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(132, 22);
            this.dtpFechaRegistro.TabIndex = 4;
            // 
            // lblTitulo__FechaRegistro
            // 
            this.lblTitulo__FechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo__FechaRegistro.Location = new System.Drawing.Point(1072, 31);
            this.lblTitulo__FechaRegistro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__FechaRegistro.Name = "lblTitulo__FechaRegistro";
            this.lblTitulo__FechaRegistro.Size = new System.Drawing.Size(94, 16);
            this.lblTitulo__FechaRegistro.TabIndex = 28;
            this.lblTitulo__FechaRegistro.Text = "F. Sistema :";
            this.lblTitulo__FechaRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(135, 87);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(864, 66);
            this.txtObservaciones.TabIndex = 3;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblTitulo__Observaciones
            // 
            this.lblTitulo__Observaciones.Location = new System.Drawing.Point(7, 86);
            this.lblTitulo__Observaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__Observaciones.Name = "lblTitulo__Observaciones";
            this.lblTitulo__Observaciones.Size = new System.Drawing.Size(127, 17);
            this.lblTitulo__Observaciones.TabIndex = 26;
            this.lblTitulo__Observaciones.Text = "Observaciones :";
            this.lblTitulo__Observaciones.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(276, 30);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(131, 25);
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
            this.txtFolio.Location = new System.Drawing.Point(135, 30);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 1;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // lblTitulo__Folio
            // 
            this.lblTitulo__Folio.Location = new System.Drawing.Point(7, 34);
            this.lblTitulo__Folio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo__Folio.Name = "lblTitulo__Folio";
            this.lblTitulo__Folio.Size = new System.Drawing.Size(127, 15);
            this.lblTitulo__Folio.TabIndex = 24;
            this.lblTitulo__Folio.Text = "Folio :";
            this.lblTitulo__Folio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetallesTransferencia
            // 
            this.FrameDetallesTransferencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetallesTransferencia.Controls.Add(this.grdProductos);
            this.FrameDetallesTransferencia.Controls.Add(this.btnCodificacion);
            this.FrameDetallesTransferencia.Location = new System.Drawing.Point(13, 239);
            this.FrameDetallesTransferencia.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDetallesTransferencia.Name = "FrameDetallesTransferencia";
            this.FrameDetallesTransferencia.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDetallesTransferencia.Size = new System.Drawing.Size(1329, 420);
            this.FrameDetallesTransferencia.TabIndex = 2;
            this.FrameDetallesTransferencia.TabStop = false;
            this.FrameDetallesTransferencia.Text = "Captura de datos";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdProductos.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer1.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer1.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer1.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer1.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer1.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdProductos.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer1;
            this.grdProductos.HorizontalScrollBar.TabIndex = 2;
            this.grdProductos.Location = new System.Drawing.Point(13, 23);
            this.grdProductos.Margin = new System.Windows.Forms.Padding(4);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1304, 389);
            this.grdProductos.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdProductos.TabIndex = 13;
            this.grdProductos.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdProductos.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer2.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer2.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer2.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer2.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer2.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdProductos.VerticalScrollBar.Renderer = enhancedScrollBarRenderer2;
            this.grdProductos.VerticalScrollBar.TabIndex = 3;
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 11;
            this.grdProductos_Sheet1.RowCount = 11;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 9).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 9).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "TasaIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad Devuelta";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Costo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ImporteIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "ImporteTotal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Captura Por";
            this.grdProductos_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 92F;
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
            this.grdProductos_Sheet1.Columns.Get(2).Width = 355F;
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
            this.grdProductos_Sheet1.Columns.Get(4).Width = 62F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Cantidad Devuelta";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 67F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowCurrencySymbol = false;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = currencyCellType1;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Costo";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 59F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(7).CellType = currencyCellType2;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 76F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType3;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "((1+(RC[-5]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "ImporteIva";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Visible = false;
            currencyCellType4.DecimalPlaces = 4;
            currencyCellType4.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(9).CellType = currencyCellType4;
            this.grdProductos_Sheet1.Columns.Get(9).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "ImporteTotal";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 92F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 5D;
            numberCellType4.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "Captura Por";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Visible = false;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdProductos_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnCodificacion
            // 
            this.btnCodificacion.Image = ((System.Drawing.Image)(resources.GetObject("btnCodificacion.Image")));
            this.btnCodificacion.Location = new System.Drawing.Point(15, 25);
            this.btnCodificacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCodificacion.Name = "btnCodificacion";
            this.btnCodificacion.Size = new System.Drawing.Size(45, 46);
            this.btnCodificacion.TabIndex = 12;
            this.btnCodificacion.UseVisualStyleBackColor = true;
            this.btnCodificacion.Click += new System.EventHandler(this.btnCodificacion_Click);
            // 
            // lblMensajes
            // 
            this.lblMensajes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMensajes.BackColor = System.Drawing.Color.SteelBlue;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMensajes.Location = new System.Drawing.Point(0, 666);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1445, 30);
            this.lblMensajes.TabIndex = 10;
            this.lblMensajes.Text = "( F7 ) Lotes.   |    Visualizar ";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMensaje_03
            // 
            this.lblMensaje_03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje_03.BackColor = System.Drawing.Color.White;
            this.lblMensaje_03.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje_03.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensaje_03.Location = new System.Drawing.Point(143, 666);
            this.lblMensaje_03.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensaje_03.Name = "lblMensaje_03";
            this.lblMensaje_03.Size = new System.Drawing.Size(1054, 30);
            this.lblMensaje_03.TabIndex = 16;
            this.lblMensaje_03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDevolucionTraspasosSalidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 695);
            this.Controls.Add(this.FrameDatosGenerales);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameDetallesTransferencia);
            this.Controls.Add(this.FrameInformacionRegistro);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.lblMensaje_03);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmDevolucionTraspasosSalidas";
            this.ShowIcon = false;
            this.Text = "Devoluciones Salidas Traspasos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTransferenciaSalidas_Load);
            this.Shown += new System.EventHandler(this.FrmTransferenciaSalidas_Base_Shown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacionRegistro.ResumeLayout(false);
            this.FrameInformacionRegistro.PerformLayout();
            this.FrameDatosGenerales.ResumeLayout(false);
            this.FrameDatosGenerales.PerformLayout();
            this.FrameDetallesTransferencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox FrameInformacionRegistro;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox FrameDatosGenerales;
        private System.Windows.Forms.Label lblTitulo__Total;
        private SC_ControlsCS.scNumericTextBox txtTotal;
        private System.Windows.Forms.Label lblTitulo__IVA;
        private SC_ControlsCS.scNumericTextBox txtIva;
        private System.Windows.Forms.Label lblTitulo__SubTotal;
        private SC_ControlsCS.scNumericTextBox txtSubTotal;
        private System.Windows.Forms.Label lblFarmaciaDestino;
        private SC_ControlsCS.scTextBoxExt txtFarmaciaDestino;
        private System.Windows.Forms.Label lblTitulo__FarmaciaDestino;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label lblTitulo__FechaRegistro;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label lblTitulo__Observaciones;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label lblTitulo__Folio;
        private System.Windows.Forms.GroupBox FrameDetallesTransferencia;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.CheckBox chkTipoImpresion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button btnCodificacion;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblTitulo__Estado;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.LinkLabel linkUrlFarmacia;
        private System.Windows.Forms.Label lblMensaje_03;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnMotivosDev;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
    }
}