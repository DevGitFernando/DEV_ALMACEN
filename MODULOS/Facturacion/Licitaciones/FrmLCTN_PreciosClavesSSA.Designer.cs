namespace Facturacion.Licitaciones
{
    partial class FrmLCTN_PreciosClavesSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLCTN_PreciosClavesSSA));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameClaveSSA = new System.Windows.Forms.GroupBox();
            this.chkAntibiotico = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkMedicamento = new System.Windows.Forms.CheckBox();
            this.lblDescripcion = new SC_ControlsCS.scLabelExt();
            this.txtContenido = new SC_ControlsCS.scIntegerTextBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPresentaciones = new SC_ControlsCS.scComboBoxExt();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAño = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecioNeto = new SC_ControlsCS.scNumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrecioAdmon = new SC_ControlsCS.scNumericTextBox();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.nudPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtPrecioBase = new SC_ControlsCS.scNumericTextBox();
            this.FramePrecios = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameClaveSSA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAño)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPorcentaje)).BeginInit();
            this.FramePrecios.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(870, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
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
            // FrameClaveSSA
            // 
            this.FrameClaveSSA.Controls.Add(this.chkAntibiotico);
            this.FrameClaveSSA.Controls.Add(this.label11);
            this.FrameClaveSSA.Controls.Add(this.chkMedicamento);
            this.FrameClaveSSA.Controls.Add(this.lblDescripcion);
            this.FrameClaveSSA.Controls.Add(this.txtContenido);
            this.FrameClaveSSA.Controls.Add(this.lblCancelado);
            this.FrameClaveSSA.Controls.Add(this.label10);
            this.FrameClaveSSA.Controls.Add(this.cboPresentaciones);
            this.FrameClaveSSA.Controls.Add(this.txtClaveSSA);
            this.FrameClaveSSA.Controls.Add(this.label3);
            this.FrameClaveSSA.Controls.Add(this.label2);
            this.FrameClaveSSA.Location = new System.Drawing.Point(12, 28);
            this.FrameClaveSSA.Name = "FrameClaveSSA";
            this.FrameClaveSSA.Size = new System.Drawing.Size(847, 134);
            this.FrameClaveSSA.TabIndex = 1;
            this.FrameClaveSSA.TabStop = false;
            this.FrameClaveSSA.Text = "Datos de Claves SSA";
            // 
            // chkAntibiotico
            // 
            this.chkAntibiotico.AutoSize = true;
            this.chkAntibiotico.Location = new System.Drawing.Point(747, 102);
            this.chkAntibiotico.Name = "chkAntibiotico";
            this.chkAntibiotico.Size = new System.Drawing.Size(90, 17);
            this.chkAntibiotico.TabIndex = 5;
            this.chkAntibiotico.Text = "Es Antibiótico";
            this.chkAntibiotico.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(320, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Contenido Paquete :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMedicamento
            // 
            this.chkMedicamento.AutoSize = true;
            this.chkMedicamento.Location = new System.Drawing.Point(558, 102);
            this.chkMedicamento.Name = "chkMedicamento";
            this.chkMedicamento.Size = new System.Drawing.Size(159, 17);
            this.chkMedicamento.TabIndex = 4;
            this.chkMedicamento.Text = "Es Medicamento Controlado";
            this.chkMedicamento.UseVisualStyleBackColor = true;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDescripcion.Location = new System.Drawing.Point(86, 43);
            this.lblDescripcion.MostrarToolTip = false;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(751, 48);
            this.lblDescripcion.TabIndex = 1;
            // 
            // txtContenido
            // 
            this.txtContenido.AllowNegative = true;
            this.txtContenido.DigitsInGroup = 0;
            this.txtContenido.Flags = 0;
            this.txtContenido.Location = new System.Drawing.Point(434, 98);
            this.txtContenido.MaxDecimalPlaces = 0;
            this.txtContenido.MaxWholeDigits = 9;
            this.txtContenido.Name = "txtContenido";
            this.txtContenido.Prefix = "";
            this.txtContenido.RangeMax = 2147483647D;
            this.txtContenido.RangeMin = -2147483648D;
            this.txtContenido.Size = new System.Drawing.Size(60, 20);
            this.txtContenido.TabIndex = 3;
            this.txtContenido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(217, 19);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(103, 20);
            this.lblCancelado.TabIndex = 6;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Presentación :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPresentaciones
            // 
            this.cboPresentaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboPresentaciones.Data = "";
            this.cboPresentaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPresentaciones.Filtro = " 1 = 1";
            this.cboPresentaciones.FormattingEnabled = true;
            this.cboPresentaciones.ListaItemsBusqueda = 20;
            this.cboPresentaciones.Location = new System.Drawing.Point(86, 97);
            this.cboPresentaciones.MostrarToolTip = false;
            this.cboPresentaciones.Name = "cboPresentaciones";
            this.cboPresentaciones.Size = new System.Drawing.Size(219, 21);
            this.cboPresentaciones.TabIndex = 2;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(86, 19);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(127, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Clave SSA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudAño
            // 
            this.nudAño.Location = new System.Drawing.Point(85, 19);
            this.nudAño.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudAño.Minimum = new decimal(new int[] {
            2005,
            0,
            0,
            0});
            this.nudAño.Name = "nudAño";
            this.nudAño.Size = new System.Drawing.Size(67, 20);
            this.nudAño.TabIndex = 0;
            this.nudAño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudAño.Value = new decimal(new int[] {
            2005,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(29, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "Año :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(476, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "Precio Neto :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecioNeto
            // 
            this.txtPrecioNeto.AllowNegative = true;
            this.txtPrecioNeto.DigitsInGroup = 3;
            this.txtPrecioNeto.Enabled = false;
            this.txtPrecioNeto.Flags = 7680;
            this.txtPrecioNeto.Location = new System.Drawing.Point(554, 19);
            this.txtPrecioNeto.MaxDecimalPlaces = 2;
            this.txtPrecioNeto.MaxLength = 15;
            this.txtPrecioNeto.MaxWholeDigits = 9;
            this.txtPrecioNeto.Name = "txtPrecioNeto";
            this.txtPrecioNeto.Prefix = "";
            this.txtPrecioNeto.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecioNeto.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecioNeto.Size = new System.Drawing.Size(84, 20);
            this.txtPrecioNeto.TabIndex = 3;
            this.txtPrecioNeto.Text = "0.00";
            this.txtPrecioNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(690, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "Precio Admon :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecioAdmon
            // 
            this.txtPrecioAdmon.AllowNegative = true;
            this.txtPrecioAdmon.DigitsInGroup = 3;
            this.txtPrecioAdmon.Flags = 7680;
            this.txtPrecioAdmon.Location = new System.Drawing.Point(774, 19);
            this.txtPrecioAdmon.MaxDecimalPlaces = 2;
            this.txtPrecioAdmon.MaxLength = 15;
            this.txtPrecioAdmon.MaxWholeDigits = 9;
            this.txtPrecioAdmon.Name = "txtPrecioAdmon";
            this.txtPrecioAdmon.Prefix = "";
            this.txtPrecioAdmon.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecioAdmon.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecioAdmon.Size = new System.Drawing.Size(62, 20);
            this.txtPrecioAdmon.TabIndex = 4;
            this.txtPrecioAdmon.Text = "0.00";
            this.txtPrecioAdmon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.Location = new System.Drawing.Point(335, 20);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(75, 18);
            this.lblPorcentaje.TabIndex = 42;
            this.lblPorcentaje.Text = "Porcentaje : ";
            this.lblPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudPorcentaje
            // 
            this.nudPorcentaje.Location = new System.Drawing.Point(415, 19);
            this.nudPorcentaje.Name = "nudPorcentaje";
            this.nudPorcentaje.Size = new System.Drawing.Size(56, 20);
            this.nudPorcentaje.TabIndex = 2;
            this.nudPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudPorcentaje.ValueChanged += new System.EventHandler(this.nudPorcentaje_ValueChanged);
            this.nudPorcentaje.Validating += new System.ComponentModel.CancelEventHandler(this.nudPorcentaje_Validating);
            // 
            // lblPrecio
            // 
            this.lblPrecio.Location = new System.Drawing.Point(168, 23);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(73, 12);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio Base :";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrecioBase
            // 
            this.txtPrecioBase.AllowNegative = true;
            this.txtPrecioBase.DigitsInGroup = 3;
            this.txtPrecioBase.Flags = 7680;
            this.txtPrecioBase.Location = new System.Drawing.Point(246, 19);
            this.txtPrecioBase.MaxDecimalPlaces = 2;
            this.txtPrecioBase.MaxLength = 15;
            this.txtPrecioBase.MaxWholeDigits = 9;
            this.txtPrecioBase.Name = "txtPrecioBase";
            this.txtPrecioBase.Prefix = "";
            this.txtPrecioBase.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecioBase.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecioBase.Size = new System.Drawing.Size(84, 20);
            this.txtPrecioBase.TabIndex = 1;
            this.txtPrecioBase.Text = "0.00";
            this.txtPrecioBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecioBase.TextChanged += new System.EventHandler(this.txtPrecioBase_TextChanged);
            this.txtPrecioBase.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrecioBase_Validating);
            // 
            // FramePrecios
            // 
            this.FramePrecios.Controls.Add(this.nudAño);
            this.FramePrecios.Controls.Add(this.txtPrecioBase);
            this.FramePrecios.Controls.Add(this.lblPrecio);
            this.FramePrecios.Controls.Add(this.label5);
            this.FramePrecios.Controls.Add(this.nudPorcentaje);
            this.FramePrecios.Controls.Add(this.label4);
            this.FramePrecios.Controls.Add(this.lblPorcentaje);
            this.FramePrecios.Controls.Add(this.txtPrecioNeto);
            this.FramePrecios.Controls.Add(this.txtPrecioAdmon);
            this.FramePrecios.Controls.Add(this.label1);
            this.FramePrecios.Location = new System.Drawing.Point(12, 165);
            this.FramePrecios.Name = "FramePrecios";
            this.FramePrecios.Size = new System.Drawing.Size(846, 57);
            this.FramePrecios.TabIndex = 2;
            this.FramePrecios.TabStop = false;
            this.FramePrecios.Text = "Datos Precio Claves SSA";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(12, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(847, 207);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Histórico de Precios de Clave";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(10, 16);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(827, 181);
            this.grdClaves.TabIndex = 0;
            this.grdClaves.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdClaves_CellDoubleClick);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 9;
            this.grdClaves_Sheet1.RowCount = 8;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Año";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "PrecioAdmon";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Precio Base";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Porcentaje";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Precio Neto";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Status Letra";
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 90F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Descripción Clave";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 290F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Año";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType2.DecimalPlaces = 2;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "PrecioAdmon";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 87F;
            numberCellType3.DecimalPlaces = 2;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Precio Base";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 75F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = -10000000D;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = numberCellType4;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Porcentaje";
            this.grdClaves_Sheet1.Columns.Get(5).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType5.DecimalPlaces = 2;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(6).CellType = numberCellType5;
            this.grdClaves_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(6).Label = "Precio Neto";
            this.grdClaves_Sheet1.Columns.Get(6).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Width = 75F;
            this.grdClaves_Sheet1.Columns.Get(7).CellType = textCellType3;
            this.grdClaves_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(7).Label = "Status";
            this.grdClaves_Sheet1.Columns.Get(7).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(7).Width = 120F;
            this.grdClaves_Sheet1.Columns.Get(8).CellType = textCellType4;
            this.grdClaves_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(8).Label = "Status Letra";
            this.grdClaves_Sheet1.Columns.Get(8).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(8).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(8).Width = 75F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.Rows.Default.Height = 25F;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(0, 437);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(870, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "<F1> Ayuda de Catálogo General                                            <F2> Ay" +
    "uda de Catálogo de Claves Registradas Precios Causes";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmLCTN_PreciosClavesSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 461);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FramePrecios);
            this.Controls.Add(this.FrameClaveSSA);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmLCTN_PreciosClavesSSA";
            this.Text = "Precios Claves Seguro Popular (Causes Anual)";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmLCTN_PreciosClavesSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameClaveSSA.ResumeLayout(false);
            this.FrameClaveSSA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAño)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPorcentaje)).EndInit();
            this.FramePrecios.ResumeLayout(false);
            this.FramePrecios.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameClaveSSA;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scComboBoxExt cboPresentaciones;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPrecio;
        private SC_ControlsCS.scNumericTextBox txtPrecioBase;
        private System.Windows.Forms.Label lblPorcentaje;
        private System.Windows.Forms.NumericUpDown nudPorcentaje;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scNumericTextBox txtPrecioNeto;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scNumericTextBox txtPrecioAdmon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudAño;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scLabelExt lblDescripcion;
        private System.Windows.Forms.GroupBox FramePrecios;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scIntegerTextBox txtContenido;
        private System.Windows.Forms.CheckBox chkAntibiotico;
        private System.Windows.Forms.CheckBox chkMedicamento;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.Label label6;
    }
}