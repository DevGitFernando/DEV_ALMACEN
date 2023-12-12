namespace DllFarmaciaSoft.Facturacion
{
    partial class FrmReporteadorValidaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteadorValidaciones));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType6 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.cboQuincena = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameCliente_old = new System.Windows.Forms.GroupBox();
            this.chkPrograma_SubPrograma = new System.Windows.Forms.CheckBox();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.FrameInsumos = new System.Windows.Forms.GroupBox();
            this.rdoInsumoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumosAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumosMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDispAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.chkMostrarLotes = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.FrameSubFarmacias = new System.Windows.Forms.GroupBox();
            this.chkMostrarPrecios = new System.Windows.Forms.CheckBox();
            this.chkMostrarPaquetes = new System.Windows.Forms.CheckBox();
            this.chkMostrarSubFarmacias = new System.Windows.Forms.CheckBox();
            this.FrameTipoDocoto = new System.Windows.Forms.GroupBox();
            this.chkExcel_Concentrado = new System.Windows.Forms.CheckBox();
            this.rdoDoctoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoDoctoExcel = new System.Windows.Forms.RadioButton();
            this.rdoDoctoPDF = new System.Windows.Forms.RadioButton();
            this.FrameFormatosExcel = new System.Windows.Forms.GroupBox();
            this.rdoFormatoExcel_01 = new System.Windows.Forms.RadioButton();
            this.rdoFormatoExcel_02 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostrarDevoluciones = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameCliente_old.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.FrameSubFarmacias.SuspendLayout();
            this.FrameTipoDocoto.SuspendLayout();
            this.FrameFormatosExcel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnGenerarDocumentos,
            this.toolStripSeparator3,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1112, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarDocumentos
            // 
            this.btnGenerarDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarDocumentos.Image")));
            this.btnGenerarDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarDocumentos.Name = "btnGenerarDocumentos";
            this.btnGenerarDocumentos.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarDocumentos.Text = "Generar validaciónes";
            this.btnGenerarDocumentos.Click += new System.EventHandler(this.btnGenerarDocumentos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Visible = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.cboQuincena);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(584, 34);
            this.FrameFechas.Margin = new System.Windows.Forms.Padding(4);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Padding = new System.Windows.Forms.Padding(4);
            this.FrameFechas.Size = new System.Drawing.Size(236, 79);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Periodo";
            // 
            // cboQuincena
            // 
            this.cboQuincena.BackColorEnabled = System.Drawing.Color.White;
            this.cboQuincena.Data = "";
            this.cboQuincena.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuincena.Filtro = " 1 = 1";
            this.cboQuincena.FormattingEnabled = true;
            this.cboQuincena.ListaItemsBusqueda = 20;
            this.cboQuincena.Location = new System.Drawing.Point(103, 46);
            this.cboQuincena.Margin = new System.Windows.Forms.Padding(4);
            this.cboQuincena.MostrarToolTip = false;
            this.cboQuincena.Name = "cboQuincena";
            this.cboQuincena.Size = new System.Drawing.Size(115, 24);
            this.cboQuincena.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Quincena :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Año-Mes :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(103, 16);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.ShowUpDown = true;
            this.dtpFechaInicial.Size = new System.Drawing.Size(115, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameCliente_old
            // 
            this.FrameCliente_old.Controls.Add(this.chkPrograma_SubPrograma);
            this.FrameCliente_old.Controls.Add(this.lblSubPro);
            this.FrameCliente_old.Controls.Add(this.txtSubPro);
            this.FrameCliente_old.Controls.Add(this.label7);
            this.FrameCliente_old.Controls.Add(this.lblPro);
            this.FrameCliente_old.Controls.Add(this.txtPro);
            this.FrameCliente_old.Controls.Add(this.label9);
            this.FrameCliente_old.Controls.Add(this.lblSubCte);
            this.FrameCliente_old.Controls.Add(this.txtSubCte);
            this.FrameCliente_old.Controls.Add(this.label1);
            this.FrameCliente_old.Enabled = false;
            this.FrameCliente_old.Location = new System.Drawing.Point(405, 288);
            this.FrameCliente_old.Margin = new System.Windows.Forms.Padding(4);
            this.FrameCliente_old.Name = "FrameCliente_old";
            this.FrameCliente_old.Padding = new System.Windows.Forms.Padding(4);
            this.FrameCliente_old.Size = new System.Drawing.Size(697, 123);
            this.FrameCliente_old.TabIndex = 10;
            this.FrameCliente_old.TabStop = false;
            this.FrameCliente_old.Text = "Parametros de Cliente";
            // 
            // chkPrograma_SubPrograma
            // 
            this.chkPrograma_SubPrograma.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrograma_SubPrograma.Location = new System.Drawing.Point(413, 0);
            this.chkPrograma_SubPrograma.Margin = new System.Windows.Forms.Padding(4);
            this.chkPrograma_SubPrograma.Name = "chkPrograma_SubPrograma";
            this.chkPrograma_SubPrograma.Size = new System.Drawing.Size(269, 23);
            this.chkPrograma_SubPrograma.TabIndex = 0;
            this.chkPrograma_SubPrograma.Text = "Aplicar filtro Programa-SubPrograma";
            this.chkPrograma_SubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrograma_SubPrograma.UseVisualStyleBackColor = true;
            this.chkPrograma_SubPrograma.CheckedChanged += new System.EventHandler(this.chkPrograma_SubPrograma_CheckedChanged);
            this.chkPrograma_SubPrograma.EnabledChanged += new System.EventHandler(this.chkPrograma_SubPrograma_EnabledChanged);
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(205, 84);
            this.lblSubPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(477, 26);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(119, 84);
            this.txtSubPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(77, 22);
            this.txtSubPro.TabIndex = 3;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 86);
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
            this.lblPro.Location = new System.Drawing.Point(205, 53);
            this.lblPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(477, 26);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(119, 53);
            this.txtPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(77, 22);
            this.txtPro.TabIndex = 2;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(29, 55);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 20);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(205, 23);
            this.lblSubCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(477, 26);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(119, 23);
            this.txtSubCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(77, 22);
            this.txtSubCte.TabIndex = 1;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(180, 21);
            this.lblCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(595, 26);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(93, 21);
            this.txtCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(77, 22);
            this.txtCte.TabIndex = 0;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(31, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(195, 288);
            this.FrameInsumos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameInsumos.Size = new System.Drawing.Size(203, 123);
            this.FrameInsumos.TabIndex = 9;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(21, 84);
            this.rdoInsumoMatCuracion.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(163, 18);
            this.rdoInsumoMatCuracion.TabIndex = 3;
            this.rdoInsumoMatCuracion.TabStop = true;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(21, 23);
            this.rdoInsumosAmbos.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(163, 18);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(21, 53);
            this.rdoInsumosMedicamento.Margin = new System.Windows.Forms.Padding(4);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(163, 18);
            this.rdoInsumosMedicamento.TabIndex = 2;
            this.rdoInsumosMedicamento.TabStop = true;
            this.rdoInsumosMedicamento.Text = "Medicamento";
            this.rdoInsumosMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDispAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(15, 288);
            this.FrameDispensacion.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDispensacion.Size = new System.Drawing.Size(172, 123);
            this.FrameDispensacion.TabIndex = 8;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(23, 23);
            this.rdoTpDispAmbos.Margin = new System.Windows.Forms.Padding(4);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(125, 18);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(24, 81);
            this.rdoTpDispConsignacion.Margin = new System.Windows.Forms.Padding(4);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(125, 21);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(23, 53);
            this.rdoTpDispVenta.Margin = new System.Windows.Forms.Padding(4);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(125, 18);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.TabStop = true;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboJurisdicciones);
            this.groupBox5.Controls.Add(this.cboEmpresas);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(15, 34);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(563, 137);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de operación";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Jurisdicción :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(101, 92);
            this.cboJurisdicciones.Margin = new System.Windows.Forms.Padding(4);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(441, 24);
            this.cboJurisdicciones.TabIndex = 2;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(101, 23);
            this.cboEmpresas.Margin = new System.Windows.Forms.Padding(4);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(441, 24);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Empresa :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(101, 58);
            this.cboEstados.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(441, 24);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 63);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(15, 415);
            this.FrameUnidades.Margin = new System.Windows.Forms.Padding(4);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Padding = new System.Windows.Forms.Padding(4);
            this.FrameUnidades.Size = new System.Drawing.Size(1088, 277);
            this.FrameUnidades.TabIndex = 11;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(905, 0);
            this.chkMarcarDesmarcar.Margin = new System.Windows.Forms.Padding(4);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(168, 21);
            this.chkMarcarDesmarcar.TabIndex = 2;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(201, 81);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Size = new System.Drawing.Size(629, 47);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 22);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(591, 15);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(13, 21);
            this.grdUnidades.Margin = new System.Windows.Forms.Padding(4);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(1060, 243);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 4;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Unidad";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 100F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Nombre Unidad";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 480F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = checkBoxCellType5;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType6;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(3).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 80F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(15, 172);
            this.FrameDirectorioDeTrabajo.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(787, 55);
            this.FrameDirectorioDeTrabajo.TabIndex = 5;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(738, 17);
            this.btnDirectorio.Margin = new System.Windows.Forms.Padding(4);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(37, 28);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(15, 20);
            this.lblDirectorioTrabajo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(715, 26);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.chkMostrarLotes);
            this.FrameCliente.Controls.Add(this.label3);
            this.FrameCliente.Controls.Add(this.txtCte);
            this.FrameCliente.Controls.Add(this.lblCte);
            this.FrameCliente.Location = new System.Drawing.Point(15, 229);
            this.FrameCliente.Margin = new System.Windows.Forms.Padding(4);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Padding = new System.Windows.Forms.Padding(4);
            this.FrameCliente.Size = new System.Drawing.Size(787, 55);
            this.FrameCliente.TabIndex = 7;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Parametros de Cliente";
            // 
            // chkMostrarLotes
            // 
            this.chkMostrarLotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarLotes.Location = new System.Drawing.Point(316, 24);
            this.chkMostrarLotes.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarLotes.Name = "chkMostrarLotes";
            this.chkMostrarLotes.Size = new System.Drawing.Size(144, 21);
            this.chkMostrarLotes.TabIndex = 18;
            this.chkMostrarLotes.Text = "Mostrar lotes y caducidades";
            this.chkMostrarLotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarLotes.UseVisualStyleBackColor = true;
            this.chkMostrarLotes.Visible = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 699);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1112, 30);
            this.label11.TabIndex = 17;
            this.label11.Text = "<F7> Seleccionar Sub-Farmacias";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameSubFarmacias
            // 
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarPrecios);
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarPaquetes);
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarSubFarmacias);
            this.FrameSubFarmacias.Location = new System.Drawing.Point(584, 116);
            this.FrameSubFarmacias.Margin = new System.Windows.Forms.Padding(4);
            this.FrameSubFarmacias.Name = "FrameSubFarmacias";
            this.FrameSubFarmacias.Padding = new System.Windows.Forms.Padding(4);
            this.FrameSubFarmacias.Size = new System.Drawing.Size(519, 55);
            this.FrameSubFarmacias.TabIndex = 4;
            this.FrameSubFarmacias.TabStop = false;
            this.FrameSubFarmacias.Text = "En impresión mostrar";
            // 
            // chkMostrarPrecios
            // 
            this.chkMostrarPrecios.Location = new System.Drawing.Point(377, 21);
            this.chkMostrarPrecios.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarPrecios.Name = "chkMostrarPrecios";
            this.chkMostrarPrecios.Size = new System.Drawing.Size(104, 21);
            this.chkMostrarPrecios.TabIndex = 2;
            this.chkMostrarPrecios.Text = "Precios";
            this.chkMostrarPrecios.UseVisualStyleBackColor = true;
            // 
            // chkMostrarPaquetes
            // 
            this.chkMostrarPaquetes.Location = new System.Drawing.Point(173, 21);
            this.chkMostrarPaquetes.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarPaquetes.Name = "chkMostrarPaquetes";
            this.chkMostrarPaquetes.Size = new System.Drawing.Size(199, 21);
            this.chkMostrarPaquetes.TabIndex = 1;
            this.chkMostrarPaquetes.Text = "Validación por paquetes";
            this.chkMostrarPaquetes.UseVisualStyleBackColor = true;
            // 
            // chkMostrarSubFarmacias
            // 
            this.chkMostrarSubFarmacias.Location = new System.Drawing.Point(36, 21);
            this.chkMostrarSubFarmacias.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarSubFarmacias.Name = "chkMostrarSubFarmacias";
            this.chkMostrarSubFarmacias.Size = new System.Drawing.Size(132, 21);
            this.chkMostrarSubFarmacias.TabIndex = 0;
            this.chkMostrarSubFarmacias.Text = "Sub-farmacias";
            this.chkMostrarSubFarmacias.UseVisualStyleBackColor = true;
            // 
            // FrameTipoDocoto
            // 
            this.FrameTipoDocoto.Controls.Add(this.chkExcel_Concentrado);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoAmbos);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoExcel);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoPDF);
            this.FrameTipoDocoto.Location = new System.Drawing.Point(827, 34);
            this.FrameTipoDocoto.Margin = new System.Windows.Forms.Padding(4);
            this.FrameTipoDocoto.Name = "FrameTipoDocoto";
            this.FrameTipoDocoto.Padding = new System.Windows.Forms.Padding(4);
            this.FrameTipoDocoto.Size = new System.Drawing.Size(276, 79);
            this.FrameTipoDocoto.TabIndex = 3;
            this.FrameTipoDocoto.TabStop = false;
            this.FrameTipoDocoto.Text = "Generar documentos en ";
            // 
            // chkExcel_Concentrado
            // 
            this.chkExcel_Concentrado.Location = new System.Drawing.Point(117, 44);
            this.chkExcel_Concentrado.Margin = new System.Windows.Forms.Padding(4);
            this.chkExcel_Concentrado.Name = "chkExcel_Concentrado";
            this.chkExcel_Concentrado.Size = new System.Drawing.Size(155, 23);
            this.chkExcel_Concentrado.TabIndex = 3;
            this.chkExcel_Concentrado.Text = "Excel concentrado";
            this.chkExcel_Concentrado.UseVisualStyleBackColor = true;
            // 
            // rdoDoctoAmbos
            // 
            this.rdoDoctoAmbos.Location = new System.Drawing.Point(25, 20);
            this.rdoDoctoAmbos.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDoctoAmbos.Name = "rdoDoctoAmbos";
            this.rdoDoctoAmbos.Size = new System.Drawing.Size(83, 23);
            this.rdoDoctoAmbos.TabIndex = 0;
            this.rdoDoctoAmbos.TabStop = true;
            this.rdoDoctoAmbos.Text = "Ambos";
            this.rdoDoctoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoDoctoExcel
            // 
            this.rdoDoctoExcel.Location = new System.Drawing.Point(25, 44);
            this.rdoDoctoExcel.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDoctoExcel.Name = "rdoDoctoExcel";
            this.rdoDoctoExcel.Size = new System.Drawing.Size(83, 23);
            this.rdoDoctoExcel.TabIndex = 2;
            this.rdoDoctoExcel.TabStop = true;
            this.rdoDoctoExcel.Text = "Excel";
            this.rdoDoctoExcel.UseVisualStyleBackColor = true;
            // 
            // rdoDoctoPDF
            // 
            this.rdoDoctoPDF.Location = new System.Drawing.Point(117, 20);
            this.rdoDoctoPDF.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDoctoPDF.Name = "rdoDoctoPDF";
            this.rdoDoctoPDF.Size = new System.Drawing.Size(77, 23);
            this.rdoDoctoPDF.TabIndex = 1;
            this.rdoDoctoPDF.TabStop = true;
            this.rdoDoctoPDF.Text = "Pdf";
            this.rdoDoctoPDF.UseVisualStyleBackColor = true;
            // 
            // FrameFormatosExcel
            // 
            this.FrameFormatosExcel.Controls.Add(this.rdoFormatoExcel_01);
            this.FrameFormatosExcel.Controls.Add(this.rdoFormatoExcel_02);
            this.FrameFormatosExcel.Location = new System.Drawing.Point(812, 172);
            this.FrameFormatosExcel.Margin = new System.Windows.Forms.Padding(4);
            this.FrameFormatosExcel.Name = "FrameFormatosExcel";
            this.FrameFormatosExcel.Padding = new System.Windows.Forms.Padding(4);
            this.FrameFormatosExcel.Size = new System.Drawing.Size(291, 55);
            this.FrameFormatosExcel.TabIndex = 6;
            this.FrameFormatosExcel.TabStop = false;
            this.FrameFormatosExcel.Text = "Formatos de excel";
            // 
            // rdoFormatoExcel_01
            // 
            this.rdoFormatoExcel_01.Location = new System.Drawing.Point(49, 21);
            this.rdoFormatoExcel_01.Margin = new System.Windows.Forms.Padding(4);
            this.rdoFormatoExcel_01.Name = "rdoFormatoExcel_01";
            this.rdoFormatoExcel_01.Size = new System.Drawing.Size(83, 24);
            this.rdoFormatoExcel_01.TabIndex = 19;
            this.rdoFormatoExcel_01.TabStop = true;
            this.rdoFormatoExcel_01.Text = "Tipo 1";
            this.rdoFormatoExcel_01.UseVisualStyleBackColor = true;
            // 
            // rdoFormatoExcel_02
            // 
            this.rdoFormatoExcel_02.Location = new System.Drawing.Point(158, 21);
            this.rdoFormatoExcel_02.Margin = new System.Windows.Forms.Padding(4);
            this.rdoFormatoExcel_02.Name = "rdoFormatoExcel_02";
            this.rdoFormatoExcel_02.Size = new System.Drawing.Size(83, 24);
            this.rdoFormatoExcel_02.TabIndex = 20;
            this.rdoFormatoExcel_02.TabStop = true;
            this.rdoFormatoExcel_02.Text = "Tipo 2";
            this.rdoFormatoExcel_02.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostrarDevoluciones);
            this.groupBox1.Location = new System.Drawing.Point(812, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 53);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Devoluciones";
            // 
            // chkMostrarDevoluciones
            // 
            this.chkMostrarDevoluciones.Location = new System.Drawing.Point(71, 21);
            this.chkMostrarDevoluciones.Margin = new System.Windows.Forms.Padding(4);
            this.chkMostrarDevoluciones.Name = "chkMostrarDevoluciones";
            this.chkMostrarDevoluciones.Size = new System.Drawing.Size(170, 21);
            this.chkMostrarDevoluciones.TabIndex = 5;
            this.chkMostrarDevoluciones.Text = "Mostrar devoluciones";
            this.chkMostrarDevoluciones.UseVisualStyleBackColor = true;
            // 
            // FrmReporteadorValidaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 729);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFormatosExcel);
            this.Controls.Add(this.FrameTipoDocoto);
            this.Controls.Add(this.FrameSubFarmacias);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameCliente_old);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmReporteadorValidaciones";
            this.Text = "Generar reportes de validación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReporteadorValidaciones_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReporteadorValidaciones_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameCliente_old.ResumeLayout(false);
            this.FrameCliente_old.PerformLayout();
            this.FrameInsumos.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.FrameSubFarmacias.ResumeLayout(false);
            this.FrameTipoDocoto.ResumeLayout(false);
            this.FrameFormatosExcel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameCliente_old;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoInsumosAmbos;
        private System.Windows.Forms.RadioButton rdoInsumosMedicamento;
        private System.Windows.Forms.RadioButton rdoInsumoMatCuracion;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.ToolStripButton btnGenerarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.GroupBox FrameCliente;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scComboBoxExt cboQuincena;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameSubFarmacias;
        private System.Windows.Forms.CheckBox chkMostrarSubFarmacias;
        private System.Windows.Forms.CheckBox chkPrograma_SubPrograma;
        private System.Windows.Forms.CheckBox chkMostrarPaquetes;
        private System.Windows.Forms.CheckBox chkMostrarPrecios;
        private System.Windows.Forms.GroupBox FrameTipoDocoto;
        private System.Windows.Forms.RadioButton rdoDoctoAmbos;
        private System.Windows.Forms.RadioButton rdoDoctoExcel;
        private System.Windows.Forms.RadioButton rdoDoctoPDF;
        private System.Windows.Forms.CheckBox chkExcel_Concentrado;
        private System.Windows.Forms.GroupBox FrameFormatosExcel;
        private System.Windows.Forms.RadioButton rdoFormatoExcel_01;
        private System.Windows.Forms.RadioButton rdoFormatoExcel_02;
        private System.Windows.Forms.CheckBox chkMostrarLotes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkMostrarDevoluciones;
    }
}