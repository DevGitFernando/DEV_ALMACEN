namespace Dll_SII_INadro.InformacionDeSalida
{
    partial class FrmReporteadorRemisiones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteadorRemisiones));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.cboQuincena = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
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
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.lblTitulo_FiltroSubFarmacia = new System.Windows.Forms.Label();
            this.FrameSubFarmacias = new System.Windows.Forms.GroupBox();
            this.chkMostrarPrecios = new System.Windows.Forms.CheckBox();
            this.chkMostrarPaquetes = new System.Windows.Forms.CheckBox();
            this.chkMostrarSubFarmacias = new System.Windows.Forms.CheckBox();
            this.FrameTipoDocoto = new System.Windows.Forms.GroupBox();
            this.chkPdfConcentrado = new System.Windows.Forms.CheckBox();
            this.rdoDoctoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoDoctoExcel = new System.Windows.Forms.RadioButton();
            this.rdoDoctoPDF = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.chkConsolidarMeses = new System.Windows.Forms.CheckBox();
            this.chkTipoEjecucion = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameCauses = new System.Windows.Forms.GroupBox();
            this.chkSepararPorCauses = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nmCauses = new System.Windows.Forms.NumericUpDown();
            this.lblFechaEnProceso = new System.Windows.Forms.Label();
            this.FrameDocumentosEnResguardo = new System.Windows.Forms.GroupBox();
            this.chkImprimirEnResguardo = new System.Windows.Forms.CheckBox();
            this.FrameSecuenciar = new System.Windows.Forms.GroupBox();
            this.nmNumerador = new System.Windows.Forms.NumericUpDown();
            this.chkSecuenciar = new System.Windows.Forms.CheckBox();
            this.FrameOrigenDeDatos = new System.Windows.Forms.GroupBox();
            this.rdoDatos_Historico = new System.Windows.Forms.RadioButton();
            this.rdoDatos_Generar = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameCliente_old.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.FrameSubFarmacias.SuspendLayout();
            this.FrameTipoDocoto.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameCauses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCauses)).BeginInit();
            this.FrameDocumentosEnResguardo.SuspendLayout();
            this.FrameSecuenciar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumerador)).BeginInit();
            this.FrameOrigenDeDatos.SuspendLayout();
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
            this.btnIntegrarPaquetesDeDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1258, 25);
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
            // btnIntegrarPaquetesDeDatos
            // 
            this.btnIntegrarPaquetesDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarPaquetesDeDatos.Enabled = false;
            this.btnIntegrarPaquetesDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarPaquetesDeDatos.Image")));
            this.btnIntegrarPaquetesDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarPaquetesDeDatos.Name = "btnIntegrarPaquetesDeDatos";
            this.btnIntegrarPaquetesDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarPaquetesDeDatos.Text = "Integrar transferencias";
            this.btnIntegrarPaquetesDeDatos.Visible = false;
            this.btnIntegrarPaquetesDeDatos.Click += new System.EventHandler(this.btnIntegrarPaquetesDeDatos_Click);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.cboQuincena);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Location = new System.Drawing.Point(1026, 123);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(199, 64);
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
            this.cboQuincena.Location = new System.Drawing.Point(86, 43);
            this.cboQuincena.MostrarToolTip = false;
            this.cboQuincena.Name = "cboQuincena";
            this.cboQuincena.Size = new System.Drawing.Size(87, 21);
            this.cboQuincena.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Quincena :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.FrameCliente_old.Location = new System.Drawing.Point(304, 226);
            this.FrameCliente_old.Name = "FrameCliente_old";
            this.FrameCliente_old.Size = new System.Drawing.Size(688, 100);
            this.FrameCliente_old.TabIndex = 12;
            this.FrameCliente_old.TabStop = false;
            this.FrameCliente_old.Text = "Parametros de Cliente";
            // 
            // chkPrograma_SubPrograma
            // 
            this.chkPrograma_SubPrograma.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrograma_SubPrograma.Location = new System.Drawing.Point(474, 0);
            this.chkPrograma_SubPrograma.Name = "chkPrograma_SubPrograma";
            this.chkPrograma_SubPrograma.Size = new System.Drawing.Size(201, 19);
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
            this.lblSubPro.Location = new System.Drawing.Point(156, 68);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(519, 21);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(91, 68);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(59, 20);
            this.txtSubPro.TabIndex = 3;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(156, 43);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(519, 21);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(91, 43);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(59, 20);
            this.txtPro.TabIndex = 2;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(24, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(156, 19);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(519, 21);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(91, 19);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(59, 20);
            this.txtSubCte.TabIndex = 1;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(135, 17);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(355, 21);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(70, 17);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(59, 20);
            this.txtCte.TabIndex = 0;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(146, 226);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(152, 100);
            this.FrameInsumos.TabIndex = 11;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(16, 68);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumoMatCuracion.TabIndex = 3;
            this.rdoInsumoMatCuracion.TabStop = true;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(16, 19);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(16, 43);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(122, 15);
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
            this.FrameDispensacion.Location = new System.Drawing.Point(11, 226);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(129, 100);
            this.FrameDispensacion.TabIndex = 10;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(17, 19);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(18, 66);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(17, 43);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 15);
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
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.label4);
            this.FrameDatosOperacion.Controls.Add(this.cboJurisdicciones);
            this.FrameDatosOperacion.Controls.Add(this.cboEmpresas);
            this.FrameDatosOperacion.Controls.Add(this.label6);
            this.FrameDatosOperacion.Controls.Add(this.cboEstados);
            this.FrameDatosOperacion.Controls.Add(this.label8);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(11, 30);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(490, 100);
            this.FrameDatosOperacion.TabIndex = 1;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
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
            this.cboJurisdicciones.Location = new System.Drawing.Point(76, 68);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(405, 21);
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
            this.cboEmpresas.Location = new System.Drawing.Point(76, 19);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(405, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
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
            this.cboEstados.Location = new System.Drawing.Point(76, 43);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(405, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(11, 327);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(981, 247);
            this.FrameUnidades.TabIndex = 13;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(220, 84);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(371, 57);
            this.FrameProceso.TabIndex = 3;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(345, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(844, 0);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(124, 17);
            this.chkMarcarDesmarcar.TabIndex = 2;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(11, 17);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(964, 222);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 8;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Cliente";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Procesado";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Inicio";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fin";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Procesando";
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Unidad";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 90F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Cliente";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 78F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Nombre Unidad";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 380F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.grdUnidades_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(4).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(5).CellType = textCellType4;
            this.grdUnidades_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(5).Label = "Inicio";
            this.grdUnidades_Sheet1.Columns.Get(5).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).CellType = textCellType5;
            this.grdUnidades_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).Label = "Fin";
            this.grdUnidades_Sheet1.Columns.Get(6).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).CellType = textCellType6;
            this.grdUnidades_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Label = "Procesando";
            this.grdUnidades_Sheet1.Columns.Get(7).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Width = 80F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(11, 131);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(721, 45);
            this.FrameDirectorioDeTrabajo.TabIndex = 5;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(689, 15);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(26, 23);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(11, 16);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(672, 21);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.label3);
            this.FrameCliente.Controls.Add(this.txtCte);
            this.FrameCliente.Controls.Add(this.lblCte);
            this.FrameCliente.Location = new System.Drawing.Point(11, 178);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(503, 45);
            this.FrameCliente.TabIndex = 7;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Parametros de Cliente";
            // 
            // lblTitulo_FiltroSubFarmacia
            // 
            this.lblTitulo_FiltroSubFarmacia.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo_FiltroSubFarmacia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_FiltroSubFarmacia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTitulo_FiltroSubFarmacia.Location = new System.Drawing.Point(749, 2);
            this.lblTitulo_FiltroSubFarmacia.Name = "lblTitulo_FiltroSubFarmacia";
            this.lblTitulo_FiltroSubFarmacia.Size = new System.Drawing.Size(238, 20);
            this.lblTitulo_FiltroSubFarmacia.TabIndex = 17;
            this.lblTitulo_FiltroSubFarmacia.Text = "<F7> Seleccionar Sub-Farmacias";
            this.lblTitulo_FiltroSubFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameSubFarmacias
            // 
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarPrecios);
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarPaquetes);
            this.FrameSubFarmacias.Controls.Add(this.chkMostrarSubFarmacias);
            this.FrameSubFarmacias.Location = new System.Drawing.Point(1026, 32);
            this.FrameSubFarmacias.Name = "FrameSubFarmacias";
            this.FrameSubFarmacias.Size = new System.Drawing.Size(199, 85);
            this.FrameSubFarmacias.TabIndex = 3;
            this.FrameSubFarmacias.TabStop = false;
            this.FrameSubFarmacias.Text = "Impresión";
            // 
            // chkMostrarPrecios
            // 
            this.chkMostrarPrecios.Location = new System.Drawing.Point(13, 54);
            this.chkMostrarPrecios.Name = "chkMostrarPrecios";
            this.chkMostrarPrecios.Size = new System.Drawing.Size(102, 17);
            this.chkMostrarPrecios.TabIndex = 2;
            this.chkMostrarPrecios.Text = "Mostrar precios";
            this.chkMostrarPrecios.UseVisualStyleBackColor = true;
            // 
            // chkMostrarPaquetes
            // 
            this.chkMostrarPaquetes.Location = new System.Drawing.Point(13, 36);
            this.chkMostrarPaquetes.Name = "chkMostrarPaquetes";
            this.chkMostrarPaquetes.Size = new System.Drawing.Size(179, 17);
            this.chkMostrarPaquetes.TabIndex = 1;
            this.chkMostrarPaquetes.Text = "Mostrar validación por paquetes";
            this.chkMostrarPaquetes.UseVisualStyleBackColor = true;
            // 
            // chkMostrarSubFarmacias
            // 
            this.chkMostrarSubFarmacias.Location = new System.Drawing.Point(13, 16);
            this.chkMostrarSubFarmacias.Name = "chkMostrarSubFarmacias";
            this.chkMostrarSubFarmacias.Size = new System.Drawing.Size(145, 19);
            this.chkMostrarSubFarmacias.TabIndex = 0;
            this.chkMostrarSubFarmacias.Text = "Mostrar sub-farmacias";
            this.chkMostrarSubFarmacias.UseVisualStyleBackColor = true;
            // 
            // FrameTipoDocoto
            // 
            this.FrameTipoDocoto.Controls.Add(this.chkPdfConcentrado);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoAmbos);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoExcel);
            this.FrameTipoDocoto.Controls.Add(this.rdoDoctoPDF);
            this.FrameTipoDocoto.Location = new System.Drawing.Point(801, 30);
            this.FrameTipoDocoto.Name = "FrameTipoDocoto";
            this.FrameTipoDocoto.Size = new System.Drawing.Size(191, 100);
            this.FrameTipoDocoto.TabIndex = 4;
            this.FrameTipoDocoto.TabStop = false;
            this.FrameTipoDocoto.Text = "Generar documentos en ";
            // 
            // chkPdfConcentrado
            // 
            this.chkPdfConcentrado.Location = new System.Drawing.Point(78, 45);
            this.chkPdfConcentrado.Name = "chkPdfConcentrado";
            this.chkPdfConcentrado.Size = new System.Drawing.Size(73, 18);
            this.chkPdfConcentrado.TabIndex = 2;
            this.chkPdfConcentrado.Text = "Agrupado";
            this.chkPdfConcentrado.UseVisualStyleBackColor = true;
            this.chkPdfConcentrado.Visible = false;
            // 
            // rdoDoctoAmbos
            // 
            this.rdoDoctoAmbos.Location = new System.Drawing.Point(32, 21);
            this.rdoDoctoAmbos.Name = "rdoDoctoAmbos";
            this.rdoDoctoAmbos.Size = new System.Drawing.Size(64, 19);
            this.rdoDoctoAmbos.TabIndex = 0;
            this.rdoDoctoAmbos.TabStop = true;
            this.rdoDoctoAmbos.Text = "Ambos";
            this.rdoDoctoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoDoctoExcel
            // 
            this.rdoDoctoExcel.Location = new System.Drawing.Point(32, 70);
            this.rdoDoctoExcel.Name = "rdoDoctoExcel";
            this.rdoDoctoExcel.Size = new System.Drawing.Size(64, 19);
            this.rdoDoctoExcel.TabIndex = 3;
            this.rdoDoctoExcel.TabStop = true;
            this.rdoDoctoExcel.Text = "Excel";
            this.rdoDoctoExcel.UseVisualStyleBackColor = true;
            // 
            // rdoDoctoPDF
            // 
            this.rdoDoctoPDF.Location = new System.Drawing.Point(32, 45);
            this.rdoDoctoPDF.Name = "rdoDoctoPDF";
            this.rdoDoctoPDF.Size = new System.Drawing.Size(64, 19);
            this.rdoDoctoPDF.TabIndex = 1;
            this.rdoDoctoPDF.TabStop = true;
            this.rdoDoctoPDF.Text = "Pdf";
            this.rdoDoctoPDF.UseVisualStyleBackColor = true;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.chkConsolidarMeses);
            this.FrameFechaDeProceso.Controls.Add(this.chkTipoEjecucion);
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(648, 30);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(147, 100);
            this.FrameFechaDeProceso.TabIndex = 3;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Periodo";
            // 
            // chkConsolidarMeses
            // 
            this.chkConsolidarMeses.Location = new System.Drawing.Point(17, 78);
            this.chkConsolidarMeses.Name = "chkConsolidarMeses";
            this.chkConsolidarMeses.Size = new System.Drawing.Size(124, 17);
            this.chkConsolidarMeses.TabIndex = 5;
            this.chkConsolidarMeses.Text = "Consolidar meses ";
            this.chkConsolidarMeses.UseVisualStyleBackColor = true;
            // 
            // chkTipoEjecucion
            // 
            this.chkTipoEjecucion.Location = new System.Drawing.Point(17, 61);
            this.chkTipoEjecucion.Name = "chkTipoEjecucion";
            this.chkTipoEjecucion.Size = new System.Drawing.Size(124, 17);
            this.chkTipoEjecucion.TabIndex = 4;
            this.chkTipoEjecucion.Text = "Procesar por dia ";
            this.chkTipoEjecucion.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(14, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(14, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Desde :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(61, 38);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(61, 16);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // FrameCauses
            // 
            this.FrameCauses.Controls.Add(this.chkSepararPorCauses);
            this.FrameCauses.Controls.Add(this.label5);
            this.FrameCauses.Controls.Add(this.nmCauses);
            this.FrameCauses.Location = new System.Drawing.Point(507, 28);
            this.FrameCauses.Name = "FrameCauses";
            this.FrameCauses.Size = new System.Drawing.Size(135, 102);
            this.FrameCauses.TabIndex = 2;
            this.FrameCauses.TabStop = false;
            this.FrameCauses.Text = "Validar claves";
            // 
            // chkSepararPorCauses
            // 
            this.chkSepararPorCauses.Location = new System.Drawing.Point(13, 23);
            this.chkSepararPorCauses.Name = "chkSepararPorCauses";
            this.chkSepararPorCauses.Size = new System.Drawing.Size(109, 18);
            this.chkSepararPorCauses.TabIndex = 6;
            this.chkSepararPorCauses.Text = "Separar causes";
            this.chkSepararPorCauses.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Causes :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmCauses
            // 
            this.nmCauses.Location = new System.Drawing.Point(66, 49);
            this.nmCauses.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.nmCauses.Minimum = new decimal(new int[] {
            2007,
            0,
            0,
            0});
            this.nmCauses.Name = "nmCauses";
            this.nmCauses.Size = new System.Drawing.Size(53, 20);
            this.nmCauses.TabIndex = 4;
            this.nmCauses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmCauses.Value = new decimal(new int[] {
            2007,
            0,
            0,
            0});
            // 
            // lblFechaEnProceso
            // 
            this.lblFechaEnProceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaEnProceso.Location = new System.Drawing.Point(1026, 230);
            this.lblFechaEnProceso.Name = "lblFechaEnProceso";
            this.lblFechaEnProceso.Size = new System.Drawing.Size(105, 57);
            this.lblFechaEnProceso.TabIndex = 19;
            this.lblFechaEnProceso.Text = "label3";
            this.lblFechaEnProceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFechaEnProceso.TextChanged += new System.EventHandler(this.lblFechaEnProceso_TextChanged);
            // 
            // FrameDocumentosEnResguardo
            // 
            this.FrameDocumentosEnResguardo.Controls.Add(this.chkImprimirEnResguardo);
            this.FrameDocumentosEnResguardo.Location = new System.Drawing.Point(520, 178);
            this.FrameDocumentosEnResguardo.Name = "FrameDocumentosEnResguardo";
            this.FrameDocumentosEnResguardo.Size = new System.Drawing.Size(212, 45);
            this.FrameDocumentosEnResguardo.TabIndex = 8;
            this.FrameDocumentosEnResguardo.TabStop = false;
            this.FrameDocumentosEnResguardo.Text = "Impresión";
            // 
            // chkImprimirEnResguardo
            // 
            this.chkImprimirEnResguardo.Location = new System.Drawing.Point(11, 18);
            this.chkImprimirEnResguardo.Name = "chkImprimirEnResguardo";
            this.chkImprimirEnResguardo.Size = new System.Drawing.Size(195, 18);
            this.chkImprimirEnResguardo.TabIndex = 3;
            this.chkImprimirEnResguardo.Text = "Generar documentos en resguardo";
            this.chkImprimirEnResguardo.UseVisualStyleBackColor = true;
            // 
            // FrameSecuenciar
            // 
            this.FrameSecuenciar.Controls.Add(this.nmNumerador);
            this.FrameSecuenciar.Controls.Add(this.chkSecuenciar);
            this.FrameSecuenciar.Location = new System.Drawing.Point(738, 178);
            this.FrameSecuenciar.Name = "FrameSecuenciar";
            this.FrameSecuenciar.Size = new System.Drawing.Size(254, 45);
            this.FrameSecuenciar.TabIndex = 9;
            this.FrameSecuenciar.TabStop = false;
            this.FrameSecuenciar.Text = "Numeración de documentos";
            // 
            // nmNumerador
            // 
            this.nmNumerador.Location = new System.Drawing.Point(117, 15);
            this.nmNumerador.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nmNumerador.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmNumerador.Name = "nmNumerador";
            this.nmNumerador.Size = new System.Drawing.Size(124, 20);
            this.nmNumerador.TabIndex = 5;
            this.nmNumerador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmNumerador.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkSecuenciar
            // 
            this.chkSecuenciar.Location = new System.Drawing.Point(25, 17);
            this.chkSecuenciar.Name = "chkSecuenciar";
            this.chkSecuenciar.Size = new System.Drawing.Size(91, 18);
            this.chkSecuenciar.TabIndex = 4;
            this.chkSecuenciar.Text = "Secuenciar";
            this.chkSecuenciar.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenDeDatos
            // 
            this.FrameOrigenDeDatos.Controls.Add(this.rdoDatos_Historico);
            this.FrameOrigenDeDatos.Controls.Add(this.rdoDatos_Generar);
            this.FrameOrigenDeDatos.Location = new System.Drawing.Point(738, 131);
            this.FrameOrigenDeDatos.Name = "FrameOrigenDeDatos";
            this.FrameOrigenDeDatos.Size = new System.Drawing.Size(254, 45);
            this.FrameOrigenDeDatos.TabIndex = 6;
            this.FrameOrigenDeDatos.TabStop = false;
            this.FrameOrigenDeDatos.Text = "Origen de datos";
            // 
            // rdoDatos_Historico
            // 
            this.rdoDatos_Historico.Location = new System.Drawing.Point(117, 16);
            this.rdoDatos_Historico.Name = "rdoDatos_Historico";
            this.rdoDatos_Historico.Size = new System.Drawing.Size(119, 19);
            this.rdoDatos_Historico.TabIndex = 24;
            this.rdoDatos_Historico.TabStop = true;
            this.rdoDatos_Historico.Text = "Datos historicos";
            this.rdoDatos_Historico.UseVisualStyleBackColor = true;
            this.rdoDatos_Historico.CheckedChanged += new System.EventHandler(this.rdoDatos_Historico_CheckedChanged);
            // 
            // rdoDatos_Generar
            // 
            this.rdoDatos_Generar.Location = new System.Drawing.Point(25, 16);
            this.rdoDatos_Generar.Name = "rdoDatos_Generar";
            this.rdoDatos_Generar.Size = new System.Drawing.Size(82, 19);
            this.rdoDatos_Generar.TabIndex = 23;
            this.rdoDatos_Generar.TabStop = true;
            this.rdoDatos_Generar.Text = "Generar";
            this.rdoDatos_Generar.UseVisualStyleBackColor = true;
            this.rdoDatos_Generar.CheckedChanged += new System.EventHandler(this.rdoDatos_Generar_CheckedChanged);
            // 
            // FrmReporteadorRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 581);
            this.Controls.Add(this.FrameOrigenDeDatos);
            this.Controls.Add(this.FrameSecuenciar);
            this.Controls.Add(this.FrameDocumentosEnResguardo);
            this.Controls.Add(this.lblFechaEnProceso);
            this.Controls.Add(this.FrameCauses);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameTipoDocoto);
            this.Controls.Add(this.FrameSubFarmacias);
            this.Controls.Add(this.lblTitulo_FiltroSubFarmacia);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameCliente_old);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReporteadorRemisiones";
            this.Text = "Generar reportes de remisión";
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
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.FrameSubFarmacias.ResumeLayout(false);
            this.FrameTipoDocoto.ResumeLayout(false);
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameCauses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmCauses)).EndInit();
            this.FrameDocumentosEnResguardo.ResumeLayout(false);
            this.FrameSecuenciar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmNumerador)).EndInit();
            this.FrameOrigenDeDatos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameFechas;
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
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.ToolStripButton btnGenerarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameCliente;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.Label lblTitulo_FiltroSubFarmacia;
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
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.CheckBox chkPdfConcentrado;
        private System.Windows.Forms.GroupBox FrameCauses;
        private System.Windows.Forms.NumericUpDown nmCauses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFechaEnProceso;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.GroupBox FrameDocumentosEnResguardo;
        private System.Windows.Forms.GroupBox FrameSecuenciar;
        private System.Windows.Forms.CheckBox chkImprimirEnResguardo;
        private System.Windows.Forms.CheckBox chkSecuenciar;
        private System.Windows.Forms.NumericUpDown nmNumerador;
        private System.Windows.Forms.CheckBox chkTipoEjecucion;
        private System.Windows.Forms.GroupBox FrameOrigenDeDatos;
        private System.Windows.Forms.RadioButton rdoDatos_Historico;
        private System.Windows.Forms.RadioButton rdoDatos_Generar;
        private System.Windows.Forms.CheckBox chkSepararPorCauses;
        private System.Windows.Forms.CheckBox chkConsolidarMeses;
    }
}