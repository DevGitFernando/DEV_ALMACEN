namespace Facturacion.GenerarRemisiones
{
    partial class FrmRemisionAdministracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemisionAdministracion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.txtConcepto = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtRubro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboTipoUnidades = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControlFacturacion = new SC_ControlsCS.scTabControlExt();
            this.tabPagParametros = new System.Windows.Forms.TabPage();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.rdoConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoVenta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoMedicamento = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnAgregarProgramasDeAtencion = new System.Windows.Forms.Button();
            this.lblSubPrograma = new System.Windows.Forms.Label();
            this.txtSubPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkRemisionarClave = new System.Windows.Forms.CheckBox();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.nmMontoAFacturar = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblPorAplicarConcepto = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPorAplicarRubro = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.lblIdSubCliente = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.tabInformacionFacturar = new System.Windows.Forms.TabPage();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.grdReporte_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardarGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlFacturacion.SuspendLayout();
            this.tabPagParametros.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMontoAFacturar)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabInformacionFacturar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).BeginInit();
            this.toolMenuFacturacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1021, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Visible = false;
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
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
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
            this.btnImprimir.Text = "Imprimir";
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(154, 112);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(337, 21);
            this.lblSubCliente.TabIndex = 40;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(154, 82);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(337, 21);
            this.lblCliente.TabIndex = 37;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(42, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConcepto
            // 
            this.lblConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConcepto.Location = new System.Drawing.Point(154, 52);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(337, 21);
            this.lblConcepto.TabIndex = 46;
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConcepto
            // 
            this.txtConcepto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConcepto.Decimales = 2;
            this.txtConcepto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtConcepto.ForeColor = System.Drawing.Color.Black;
            this.txtConcepto.Location = new System.Drawing.Point(89, 52);
            this.txtConcepto.MaxLength = 4;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.PermitirApostrofo = false;
            this.txtConcepto.PermitirNegativos = false;
            this.txtConcepto.Size = new System.Drawing.Size(59, 20);
            this.txtConcepto.TabIndex = 1;
            this.txtConcepto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            this.txtConcepto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConcepto_KeyDown);
            this.txtConcepto.Validating += new System.ComponentModel.CancelEventHandler(this.txtConcepto_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Concepto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRubro
            // 
            this.lblRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRubro.Location = new System.Drawing.Point(154, 21);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(337, 21);
            this.lblRubro.TabIndex = 43;
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRubro
            // 
            this.txtRubro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRubro.Decimales = 2;
            this.txtRubro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRubro.ForeColor = System.Drawing.Color.Black;
            this.txtRubro.Location = new System.Drawing.Point(89, 21);
            this.txtRubro.MaxLength = 4;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.PermitirApostrofo = false;
            this.txtRubro.PermitirNegativos = false;
            this.txtRubro.Size = new System.Drawing.Size(59, 20);
            this.txtRubro.TabIndex = 0;
            this.txtRubro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRubro.TextChanged += new System.EventHandler(this.txtRubro_TextChanged);
            this.txtRubro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRubro_KeyDown);
            this.txtRubro.Validating += new System.ComponentModel.CancelEventHandler(this.txtRubro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(22, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Rubro :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(518, 156);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(468, 76);
            this.FrameFechas.TabIndex = 9;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas Periodos Cerrados";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(290, 31);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(258, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(91, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(134, 31);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboTipoUnidades);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(11, 238);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(501, 52);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipos de Unidades";
            // 
            // cboTipoUnidades
            // 
            this.cboTipoUnidades.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoUnidades.Data = "";
            this.cboTipoUnidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoUnidades.Filtro = " 1 = 1";
            this.cboTipoUnidades.FormattingEnabled = true;
            this.cboTipoUnidades.ListaItemsBusqueda = 20;
            this.cboTipoUnidades.Location = new System.Drawing.Point(89, 19);
            this.cboTipoUnidades.MostrarToolTip = false;
            this.cboTipoUnidades.Name = "cboTipoUnidades";
            this.cboTipoUnidades.Size = new System.Drawing.Size(402, 21);
            this.cboTipoUnidades.TabIndex = 0;
            this.cboTipoUnidades.SelectedIndexChanged += new System.EventHandler(this.cboTipoUnidades_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(36, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tipo :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboJurisdicciones);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(11, 291);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jurisdicciones";
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(89, 16);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(402, 21);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Jurisdicción :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControlFacturacion
            // 
            this.tabControlFacturacion.Appearance = SC_ControlsCS.scTabAppearance.Buttons;
            this.tabControlFacturacion.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabControlFacturacion.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabControlFacturacion.Controls.Add(this.tabPagParametros);
            this.tabControlFacturacion.Controls.Add(this.tabInformacionFacturar);
            this.tabControlFacturacion.CustomBackColor = false;
            this.tabControlFacturacion.CustomBackColorPages = false;
            this.tabControlFacturacion.HotTrack = true;
            this.tabControlFacturacion.Location = new System.Drawing.Point(10, 33);
            this.tabControlFacturacion.MostrarBorde = false;
            this.tabControlFacturacion.Name = "tabControlFacturacion";
            this.tabControlFacturacion.SelectedIndex = 0;
            this.tabControlFacturacion.Size = new System.Drawing.Size(1000, 492);
            this.tabControlFacturacion.TabIndex = 1;
            this.tabControlFacturacion.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlFacturacion_DrawItem);
            // 
            // tabPagParametros
            // 
            this.tabPagParametros.BackColor = System.Drawing.SystemColors.Info;
            this.tabPagParametros.Controls.Add(this.FrameOrigenInsumo);
            this.tabPagParametros.Controls.Add(this.FrameTipoInsumo);
            this.tabPagParametros.Controls.Add(this.groupBox8);
            this.tabPagParametros.Controls.Add(this.groupBox7);
            this.tabPagParametros.Controls.Add(this.groupBox6);
            this.tabPagParametros.Controls.Add(this.groupBox4);
            this.tabPagParametros.Controls.Add(this.groupBox2);
            this.tabPagParametros.Controls.Add(this.groupBox3);
            this.tabPagParametros.Controls.Add(this.groupBox1);
            this.tabPagParametros.Controls.Add(this.FrameFechas);
            this.tabPagParametros.Controls.Add(this.groupBox5);
            this.tabPagParametros.Location = new System.Drawing.Point(4, 28);
            this.tabPagParametros.Name = "tabPagParametros";
            this.tabPagParametros.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagParametros.Size = new System.Drawing.Size(992, 460);
            this.tabPagParametros.TabIndex = 0;
            this.tabPagParametros.Text = "Parámetros";
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Controls.Add(this.rdoConsignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoVenta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(11, 402);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(215, 52);
            this.FrameOrigenInsumo.TabIndex = 5;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Tipo de Insumos";
            // 
            // rdoConsignacion
            // 
            this.rdoConsignacion.Location = new System.Drawing.Point(100, 21);
            this.rdoConsignacion.Name = "rdoConsignacion";
            this.rdoConsignacion.Size = new System.Drawing.Size(93, 17);
            this.rdoConsignacion.TabIndex = 1;
            this.rdoConsignacion.TabStop = true;
            this.rdoConsignacion.Text = "Consignación";
            this.rdoConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoVenta
            // 
            this.rdoVenta.Location = new System.Drawing.Point(21, 21);
            this.rdoVenta.Name = "rdoVenta";
            this.rdoVenta.Size = new System.Drawing.Size(65, 17);
            this.rdoVenta.TabIndex = 0;
            this.rdoVenta.TabStop = true;
            this.rdoVenta.Text = "Venta";
            this.rdoVenta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Controls.Add(this.rdoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(232, 402);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(280, 52);
            this.FrameTipoInsumo.TabIndex = 6;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoMaterialDeCuracion
            // 
            this.rdoMaterialDeCuracion.Location = new System.Drawing.Point(135, 21);
            this.rdoMaterialDeCuracion.Name = "rdoMaterialDeCuracion";
            this.rdoMaterialDeCuracion.Size = new System.Drawing.Size(122, 17);
            this.rdoMaterialDeCuracion.TabIndex = 1;
            this.rdoMaterialDeCuracion.TabStop = true;
            this.rdoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoMedicamento
            // 
            this.rdoMedicamento.Location = new System.Drawing.Point(23, 21);
            this.rdoMedicamento.Name = "rdoMedicamento";
            this.rdoMedicamento.Size = new System.Drawing.Size(89, 17);
            this.rdoMedicamento.TabIndex = 0;
            this.rdoMedicamento.TabStop = true;
            this.rdoMedicamento.Text = "Medicamento";
            this.rdoMedicamento.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnAgregarProgramasDeAtencion);
            this.groupBox8.Controls.Add(this.lblSubPrograma);
            this.groupBox8.Controls.Add(this.txtSubPrograma);
            this.groupBox8.Controls.Add(this.label17);
            this.groupBox8.Controls.Add(this.lblPrograma);
            this.groupBox8.Controls.Add(this.txtPrograma);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Location = new System.Drawing.Point(11, 156);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(501, 76);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Parámetros de Dispensación";
            // 
            // btnAgregarProgramasDeAtencion
            // 
            this.btnAgregarProgramasDeAtencion.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProgramasDeAtencion.Image")));
            this.btnAgregarProgramasDeAtencion.Location = new System.Drawing.Point(459, 20);
            this.btnAgregarProgramasDeAtencion.Name = "btnAgregarProgramasDeAtencion";
            this.btnAgregarProgramasDeAtencion.Size = new System.Drawing.Size(32, 50);
            this.btnAgregarProgramasDeAtencion.TabIndex = 42;
            this.btnAgregarProgramasDeAtencion.UseVisualStyleBackColor = true;
            // 
            // lblSubPrograma
            // 
            this.lblSubPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPrograma.Location = new System.Drawing.Point(154, 45);
            this.lblSubPrograma.Name = "lblSubPrograma";
            this.lblSubPrograma.Size = new System.Drawing.Size(299, 21);
            this.lblSubPrograma.TabIndex = 40;
            this.lblSubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPrograma
            // 
            this.txtSubPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPrograma.Decimales = 2;
            this.txtSubPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtSubPrograma.Location = new System.Drawing.Point(89, 45);
            this.txtSubPrograma.MaxLength = 4;
            this.txtSubPrograma.Name = "txtSubPrograma";
            this.txtSubPrograma.PermitirApostrofo = false;
            this.txtSubPrograma.PermitirNegativos = false;
            this.txtSubPrograma.Size = new System.Drawing.Size(59, 20);
            this.txtSubPrograma.TabIndex = 1;
            this.txtSubPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPrograma.TextChanged += new System.EventHandler(this.txtSubPrograma_TextChanged);
            this.txtSubPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPrograma_KeyDown);
            this.txtSubPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPrograma_Validating);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "Sub-Programa :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrograma
            // 
            this.lblPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrograma.Location = new System.Drawing.Point(154, 20);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(299, 21);
            this.lblPrograma.TabIndex = 37;
            this.lblPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrograma
            // 
            this.txtPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPrograma.Decimales = 2;
            this.txtPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtPrograma.Location = new System.Drawing.Point(89, 20);
            this.txtPrograma.MaxLength = 4;
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.PermitirApostrofo = false;
            this.txtPrograma.PermitirNegativos = false;
            this.txtPrograma.Size = new System.Drawing.Size(59, 20);
            this.txtPrograma.TabIndex = 0;
            this.txtPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrograma.TextChanged += new System.EventHandler(this.txtPrograma_TextChanged);
            this.txtPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrograma_KeyDown);
            this.txtPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrograma_Validating);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(25, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 16);
            this.label19.TabIndex = 36;
            this.label19.Text = "Programa :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkRemisionarClave);
            this.groupBox7.Controls.Add(this.lblClaveSSA);
            this.groupBox7.Controls.Add(this.txtClaveSSA);
            this.groupBox7.Location = new System.Drawing.Point(518, 238);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(468, 216);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Clave a Remisionar";
            // 
            // chkRemisionarClave
            // 
            this.chkRemisionarClave.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRemisionarClave.Location = new System.Drawing.Point(167, 19);
            this.chkRemisionarClave.Name = "chkRemisionarClave";
            this.chkRemisionarClave.Size = new System.Drawing.Size(289, 20);
            this.chkRemisionarClave.TabIndex = 1;
            this.chkRemisionarClave.Text = "Remisionar por Clave Específica";
            this.chkRemisionarClave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRemisionarClave.UseVisualStyleBackColor = true;
            this.chkRemisionarClave.CheckedChanged += new System.EventHandler(this.chkRemisionarClave_CheckedChanged);
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(17, 42);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(439, 159);
            this.lblClaveSSA.TabIndex = 2;
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 0;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(17, 19);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(144, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.TextChanged += new System.EventHandler(this.txtClaveSSA_TextChanged);
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.nmMontoAFacturar);
            this.groupBox6.Location = new System.Drawing.Point(518, 79);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(468, 76);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Monto a Remisionar";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(112, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 43);
            this.label14.TabIndex = 50;
            this.label14.Text = "Monto :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMontoAFacturar
            // 
            this.nmMontoAFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmMontoAFacturar.Location = new System.Drawing.Point(239, 20);
            this.nmMontoAFacturar.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nmMontoAFacturar.Name = "nmMontoAFacturar";
            this.nmMontoAFacturar.Size = new System.Drawing.Size(217, 44);
            this.nmMontoAFacturar.TabIndex = 0;
            this.nmMontoAFacturar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmMontoAFacturar.ThousandsSeparator = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblPorAplicarConcepto);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lblPorAplicarRubro);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(518, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(468, 76);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información de Montos";
            // 
            // lblPorAplicarConcepto
            // 
            this.lblPorAplicarConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPorAplicarConcepto.Location = new System.Drawing.Point(239, 45);
            this.lblPorAplicarConcepto.Name = "lblPorAplicarConcepto";
            this.lblPorAplicarConcepto.Size = new System.Drawing.Size(217, 21);
            this.lblPorAplicarConcepto.TabIndex = 1;
            this.lblPorAplicarConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(36, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(199, 16);
            this.label13.TabIndex = 51;
            this.label13.Text = "Por Aplicar Concepto :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPorAplicarRubro
            // 
            this.lblPorAplicarRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPorAplicarRubro.Location = new System.Drawing.Point(239, 20);
            this.lblPorAplicarRubro.Name = "lblPorAplicarRubro";
            this.lblPorAplicarRubro.Size = new System.Drawing.Size(217, 21);
            this.lblPorAplicarRubro.TabIndex = 0;
            this.lblPorAplicarRubro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(36, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(199, 16);
            this.label11.TabIndex = 49;
            this.label11.Text = "Por Aplicar Rubro :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblIdCliente);
            this.groupBox2.Controls.Add(this.lblIdSubCliente);
            this.groupBox2.Controls.Add(this.lblConcepto);
            this.groupBox2.Controls.Add(this.lblSubCliente);
            this.groupBox2.Controls.Add(this.lblRubro);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtConcepto);
            this.groupBox2.Controls.Add(this.lblCliente);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRubro);
            this.groupBox2.Location = new System.Drawing.Point(11, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 152);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fuente de Finaciamiento";
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdCliente.Location = new System.Drawing.Point(89, 82);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(59, 21);
            this.lblIdCliente.TabIndex = 2;
            this.lblIdCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdSubCliente
            // 
            this.lblIdSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdSubCliente.Location = new System.Drawing.Point(89, 112);
            this.lblIdSubCliente.Name = "lblIdSubCliente";
            this.lblIdSubCliente.Size = new System.Drawing.Size(59, 21);
            this.lblIdSubCliente.TabIndex = 3;
            this.lblIdSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboFarmacias);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(11, 344);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(501, 52);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Farmacias";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(89, 16);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(402, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabInformacionFacturar
            // 
            this.tabInformacionFacturar.BackColor = System.Drawing.Color.OldLace;
            this.tabInformacionFacturar.Controls.Add(this.grdReporte);
            this.tabInformacionFacturar.Controls.Add(this.toolMenuFacturacion);
            this.tabInformacionFacturar.Location = new System.Drawing.Point(4, 28);
            this.tabInformacionFacturar.Name = "tabInformacionFacturar";
            this.tabInformacionFacturar.Padding = new System.Windows.Forms.Padding(3);
            this.tabInformacionFacturar.Size = new System.Drawing.Size(992, 460);
            this.tabInformacionFacturar.TabIndex = 1;
            this.tabInformacionFacturar.Text = "Información a Remisionar";
            // 
            // grdReporte
            // 
            this.grdReporte.AccessibleDescription = "grdReporte, Sheet1, Row 0, Column 0, ";
            this.grdReporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdReporte.Location = new System.Drawing.Point(6, 34);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdReporte.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdReporte_Sheet1});
            this.grdReporte.Size = new System.Drawing.Size(981, 420);
            this.grdReporte.TabIndex = 3;
            // 
            // grdReporte_Sheet1
            // 
            this.grdReporte_Sheet1.Reset();
            this.grdReporte_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdReporte_Sheet1.ColumnCount = 10;
            this.grdReporte_Sheet1.RowCount = 10;
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Jurisdicción";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre jurisdicción";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Farmacia";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Nombre Farmacia";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha de Registro";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Folio";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha Mínima de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha Máxima de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Remisionar";
            this.grdReporte_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            this.grdReporte_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdReporte_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Label = "Jurisdicción";
            this.grdReporte_Sheet1.Columns.Get(0).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Width = 65F;
            this.grdReporte_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdReporte_Sheet1.Columns.Get(1).Label = "Nombre jurisdicción";
            this.grdReporte_Sheet1.Columns.Get(1).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Width = 120F;
            this.grdReporte_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdReporte_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Label = "Farmacia";
            this.grdReporte_Sheet1.Columns.Get(2).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdReporte_Sheet1.Columns.Get(3).Label = "Nombre Farmacia";
            this.grdReporte_Sheet1.Columns.Get(3).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).Width = 180F;
            this.grdReporte_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdReporte_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Label = "Fecha de Registro";
            this.grdReporte_Sheet1.Columns.Get(4).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.grdReporte_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Label = "Folio";
            this.grdReporte_Sheet1.Columns.Get(5).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.grdReporte_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(6).Label = "Fecha de Cierre";
            this.grdReporte_Sheet1.Columns.Get(6).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(6).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(7).CellType = textCellType8;
            this.grdReporte_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(7).Label = "Fecha Mínima de Cierre";
            this.grdReporte_Sheet1.Columns.Get(7).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(7).Width = 100F;
            this.grdReporte_Sheet1.Columns.Get(8).CellType = textCellType9;
            this.grdReporte_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(8).Label = "Fecha Máxima de Cierre";
            this.grdReporte_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(8).Width = 100F;
            this.grdReporte_Sheet1.Columns.Get(9).CellType = checkBoxCellType1;
            this.grdReporte_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(9).Label = "Remisionar";
            this.grdReporte_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(9).Width = 66F;
            this.grdReporte_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolMenuFacturacion
            // 
            this.toolMenuFacturacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.btnGuardarGrid,
            this.toolStripSeparator4,
            this.toolStripButton3,
            this.toolStripSeparator5,
            this.toolStripButton4});
            this.toolMenuFacturacion.Location = new System.Drawing.Point(3, 3);
            this.toolMenuFacturacion.Name = "toolMenuFacturacion";
            this.toolMenuFacturacion.Size = new System.Drawing.Size(986, 25);
            this.toolMenuFacturacion.TabIndex = 2;
            this.toolMenuFacturacion.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Nuevo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardarGrid
            // 
            this.btnGuardarGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardarGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarGrid.Image")));
            this.btnGuardarGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardarGrid.Name = "btnGuardarGrid";
            this.btnGuardarGrid.Size = new System.Drawing.Size(23, 22);
            this.btnGuardarGrid.Text = "Guardar";
            this.btnGuardarGrid.Click += new System.EventHandler(this.btnGuardarGrid_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Cancelar";
            this.toolStripButton3.ToolTipText = "Cancelar";
            this.toolStripButton3.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Imprimir";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(250, 89);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage1";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(992, 162);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmRemisionAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 538);
            this.Controls.Add(this.tabControlFacturacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRemisionAdministracion";
            this.Text = "Remisiones de Administración";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRemisionAdministracion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmRemisionAdministracion_Paint);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControlFacturacion.ResumeLayout(false);
            this.tabPagParametros.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMontoAFacturar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabInformacionFacturar.ResumeLayout(false);
            this.tabInformacionFacturar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).EndInit();
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
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
        private System.Windows.Forms.Label lblConcepto;
        private SC_ControlsCS.scTextBoxExt txtConcepto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRubro;
        private SC_ControlsCS.scTextBoxExt txtRubro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboTipoUnidades;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTabControlExt tabControlFacturacion;
        private System.Windows.Forms.TabPage tabPagParametros;
        private System.Windows.Forms.TabPage tabInformacionFacturar;
        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardarGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblPorAplicarRubro;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblPorAplicarConcepto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown nmMontoAFacturar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblSubPrograma;
        private SC_ControlsCS.scTextBoxExt txtSubPrograma;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPrograma;
        private SC_ControlsCS.scTextBoxExt txtPrograma;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblIdSubCliente;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private FarPoint.Win.Spread.SheetView grdReporte_Sheet1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameTipoInsumo;
        private System.Windows.Forms.RadioButton rdoMaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdoMedicamento;
        private System.Windows.Forms.CheckBox chkRemisionarClave;
        private System.Windows.Forms.Button btnAgregarProgramasDeAtencion;
        private System.Windows.Forms.GroupBox FrameOrigenInsumo;
        private System.Windows.Forms.RadioButton rdoConsignacion;
        private System.Windows.Forms.RadioButton rdoVenta;
    }
}