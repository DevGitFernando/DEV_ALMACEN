namespace Facturacion.GenerarRemisiones
{
    partial class FrmRemisionesGenerales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType37 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType38 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType39 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType40 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType41 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType42 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType43 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType44 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemisionesGenerales));
            FarPoint.Win.Spread.CellType.TextCellType textCellType45 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType46 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType47 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType48 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType49 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType50 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType51 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType52 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType53 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType54 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType6 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.Frame_04_TipoRemision = new System.Windows.Forms.GroupBox();
            this.chkTipoDeRemision_02_Servicio = new System.Windows.Forms.CheckBox();
            this.chkTipoDeRemision_01_Producto = new System.Windows.Forms.CheckBox();
            this.Frame_05_OrigenInsumo = new System.Windows.Forms.GroupBox();
            this.chkOrigenInsumo_02_Consignacion = new System.Windows.Forms.CheckBox();
            this.chkOrigenInsumo_01_Venta = new System.Windows.Forms.CheckBox();
            this.Frame_06_TipoInsumo = new System.Windows.Forms.GroupBox();
            this.chkTipoDeInsumo_02_MaterialDeCuracion = new System.Windows.Forms.CheckBox();
            this.chkTipoDeInsumo_01_Medicamento = new System.Windows.Forms.CheckBox();
            this.Frame_02_FuentesFinanciamiento = new System.Windows.Forms.GroupBox();
            this.btnLimpiarFF = new System.Windows.Forms.Button();
            this.lblIdSubCliente = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarFuentes = new System.Windows.Forms.Button();
            this.grdFuentesDeFinanciamiento = new FarPoint.Win.Spread.FpSpread();
            this.grdFuentesDeFinanciamiento_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtConcepto = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRubro = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarRemisiones = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarPDF = new System.Windows.Forms.ToolStripButton();
            this.scTabControlExt1 = new SC_ControlsCS.scTabControlExt();
            this.tabPage_01_Parametros = new System.Windows.Forms.TabPage();
            this.Frame_11_RangoDeFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.Frame_10_FormatosDeImpresion = new System.Windows.Forms.GroupBox();
            this.chkGenerarDocumentos = new System.Windows.Forms.CheckBox();
            this.cboFormatosDeImpresion = new SC_ControlsCS.scComboBoxExt();
            this.Frame_08_FacturasAnticipadas = new System.Windows.Forms.GroupBox();
            this.chkProcesar_SoloClavesReferenciaRemisiones = new System.Windows.Forms.CheckBox();
            this.txtFactura_Folio = new SC_ControlsCS.scTextBoxExt();
            this.chkEsRelacionDeMontos = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkEsFacturaPreviaEnCajas = new System.Windows.Forms.CheckBox();
            this.txtFactura_Serie = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.chkEsRelacionFacturaPrevia = new System.Windows.Forms.CheckBox();
            this.Frame_03_InformacionGeneral = new System.Windows.Forms.GroupBox();
            this.chkExcluirCantidadesConDecimales = new System.Windows.Forms.CheckBox();
            this.chkEsRemisionGeneral = new System.Windows.Forms.CheckBox();
            this.chkEsProgramaEspecial = new System.Windows.Forms.CheckBox();
            this.chkAplicarDocumentos = new System.Windows.Forms.CheckBox();
            this.cboTipoDeBeneficiarios = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAsignarReferencias = new System.Windows.Forms.CheckBox();
            this.chkProcesarParcialidades = new System.Windows.Forms.CheckBox();
            this.cboNivelDeInformacion = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.chkProcesarCantidadesExcedentes = new System.Windows.Forms.CheckBox();
            this.chkBeneficiarios_x_Jurisdiccion = new System.Windows.Forms.CheckBox();
            this.Frame_07_OrigenDispensacion = new System.Windows.Forms.GroupBox();
            this.chkEsComplemento = new System.Windows.Forms.CheckBox();
            this.cboOrigenDispensacion = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.chkSeparar__Venta_y_Vales = new System.Windows.Forms.CheckBox();
            this.tabPage_02_ProgramasSubPrograma = new System.Windows.Forms.TabPage();
            this.Frame_21_ProgramasSubProgramas = new System.Windows.Forms.GroupBox();
            this.lblSubPrograma = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtIdSubPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.txtIdPrograma = new SC_ControlsCS.scTextBoxExt();
            this.btnLimpiarPrograma = new System.Windows.Forms.Button();
            this.btnAgregarPrograma = new System.Windows.Forms.Button();
            this.grdProgramasSubProgramas = new FarPoint.Win.Spread.FpSpread();
            this.grdProgramasSubProgramas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage_03_Claves = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdClavesExcluidas = new FarPoint.Win.Spread.FpSpread();
            this.grdClavesExcluidas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnLimpiarClaveExcluida = new System.Windows.Forms.Button();
            this.btnAgregarClaveExcluida = new System.Windows.Forms.Button();
            this.lblClaveExcluida = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtClaveSSA_Excluida = new SC_ControlsCS.scTextBoxExt();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdClavesExclusivas = new FarPoint.Win.Spread.FpSpread();
            this.grdClavesExclusivas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnLimpiarClaveExclusiva = new System.Windows.Forms.Button();
            this.btnAgregarClaveExclusiva = new System.Windows.Forms.Button();
            this.lblClaveExclusiva = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txtClaveSSA_Exclusiva = new SC_ControlsCS.scTextBoxExt();
            this.tabPage_04_InformacionAdicional = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtInfoAdicional_01 = new SC_ControlsCS.scTextBoxExt();
            this.lblInfoAdicional_05 = new System.Windows.Forms.Label();
            this.lblITituloInfoAdicional_01 = new System.Windows.Forms.Label();
            this.lblITituloInfoAdicional_05 = new System.Windows.Forms.Label();
            this.lblInfoAdicional_01 = new System.Windows.Forms.Label();
            this.txtInfoAdicional_05 = new SC_ControlsCS.scTextBoxExt();
            this.txtInfoAdicional_02 = new SC_ControlsCS.scTextBoxExt();
            this.lblInfoAdicional_04 = new System.Windows.Forms.Label();
            this.lblITituloInfoAdicional_02 = new System.Windows.Forms.Label();
            this.lblITituloInfoAdicional_04 = new System.Windows.Forms.Label();
            this.lblInfoAdicional_02 = new System.Windows.Forms.Label();
            this.txtInfoAdicional_04 = new SC_ControlsCS.scTextBoxExt();
            this.txtInfoAdicional_03 = new SC_ControlsCS.scTextBoxExt();
            this.lblInfoAdicional_03 = new System.Windows.Forms.Label();
            this.lblITituloInfoAdicional_03 = new System.Windows.Forms.Label();
            this.tabPage_05_Unidades = new System.Windows.Forms.TabPage();
            this.chkMarcarDesmarcarFarmacias = new System.Windows.Forms.CheckBox();
            this.grdFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage_06_DocumentosDeComprobacion = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdDocumentos = new FarPoint.Win.Spread.FpSpread();
            this.grdDocumentos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPage_07_FacturasRelacionadas = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdFacturas = new FarPoint.Win.Spread.FpSpread();
            this.grdFacturas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.Frame_04_TipoRemision.SuspendLayout();
            this.Frame_05_OrigenInsumo.SuspendLayout();
            this.Frame_06_TipoInsumo.SuspendLayout();
            this.Frame_02_FuentesFinanciamiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuentesDeFinanciamiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuentesDeFinanciamiento_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.scTabControlExt1.SuspendLayout();
            this.tabPage_01_Parametros.SuspendLayout();
            this.Frame_11_RangoDeFechas.SuspendLayout();
            this.Frame_10_FormatosDeImpresion.SuspendLayout();
            this.Frame_08_FacturasAnticipadas.SuspendLayout();
            this.Frame_03_InformacionGeneral.SuspendLayout();
            this.Frame_07_OrigenDispensacion.SuspendLayout();
            this.tabPage_02_ProgramasSubPrograma.SuspendLayout();
            this.Frame_21_ProgramasSubProgramas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas_Sheet1)).BeginInit();
            this.tabPage_03_Claves.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExcluidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExcluidas_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExclusivas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExclusivas_Sheet1)).BeginInit();
            this.tabPage_04_InformacionAdicional.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage_05_Unidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).BeginInit();
            this.tabPage_06_DocumentosDeComprobacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos_Sheet1)).BeginInit();
            this.tabPage_07_FacturasRelacionadas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // Frame_04_TipoRemision
            // 
            this.Frame_04_TipoRemision.Controls.Add(this.chkTipoDeRemision_02_Servicio);
            this.Frame_04_TipoRemision.Controls.Add(this.chkTipoDeRemision_01_Producto);
            this.Frame_04_TipoRemision.Location = new System.Drawing.Point(11, 357);
            this.Frame_04_TipoRemision.Name = "Frame_04_TipoRemision";
            this.Frame_04_TipoRemision.Size = new System.Drawing.Size(281, 50);
            this.Frame_04_TipoRemision.TabIndex = 1;
            this.Frame_04_TipoRemision.TabStop = false;
            this.Frame_04_TipoRemision.Text = "Tipo de remisión";
            // 
            // chkTipoDeRemision_02_Servicio
            // 
            this.chkTipoDeRemision_02_Servicio.Location = new System.Drawing.Point(155, 21);
            this.chkTipoDeRemision_02_Servicio.Name = "chkTipoDeRemision_02_Servicio";
            this.chkTipoDeRemision_02_Servicio.Size = new System.Drawing.Size(109, 19);
            this.chkTipoDeRemision_02_Servicio.TabIndex = 1;
            this.chkTipoDeRemision_02_Servicio.Text = "Servicio";
            this.chkTipoDeRemision_02_Servicio.UseVisualStyleBackColor = true;
            // 
            // chkTipoDeRemision_01_Producto
            // 
            this.chkTipoDeRemision_01_Producto.Location = new System.Drawing.Point(42, 21);
            this.chkTipoDeRemision_01_Producto.Name = "chkTipoDeRemision_01_Producto";
            this.chkTipoDeRemision_01_Producto.Size = new System.Drawing.Size(109, 19);
            this.chkTipoDeRemision_01_Producto.TabIndex = 0;
            this.chkTipoDeRemision_01_Producto.Text = "Producto";
            this.chkTipoDeRemision_01_Producto.UseVisualStyleBackColor = true;
            // 
            // Frame_05_OrigenInsumo
            // 
            this.Frame_05_OrigenInsumo.Controls.Add(this.chkOrigenInsumo_02_Consignacion);
            this.Frame_05_OrigenInsumo.Controls.Add(this.chkOrigenInsumo_01_Venta);
            this.Frame_05_OrigenInsumo.Location = new System.Drawing.Point(11, 305);
            this.Frame_05_OrigenInsumo.Name = "Frame_05_OrigenInsumo";
            this.Frame_05_OrigenInsumo.Size = new System.Drawing.Size(281, 50);
            this.Frame_05_OrigenInsumo.TabIndex = 0;
            this.Frame_05_OrigenInsumo.TabStop = false;
            this.Frame_05_OrigenInsumo.Text = "Origen de Insumos";
            // 
            // chkOrigenInsumo_02_Consignacion
            // 
            this.chkOrigenInsumo_02_Consignacion.Location = new System.Drawing.Point(155, 21);
            this.chkOrigenInsumo_02_Consignacion.Name = "chkOrigenInsumo_02_Consignacion";
            this.chkOrigenInsumo_02_Consignacion.Size = new System.Drawing.Size(109, 19);
            this.chkOrigenInsumo_02_Consignacion.TabIndex = 1;
            this.chkOrigenInsumo_02_Consignacion.Text = "Consignación";
            this.chkOrigenInsumo_02_Consignacion.UseVisualStyleBackColor = true;
            // 
            // chkOrigenInsumo_01_Venta
            // 
            this.chkOrigenInsumo_01_Venta.Location = new System.Drawing.Point(42, 21);
            this.chkOrigenInsumo_01_Venta.Name = "chkOrigenInsumo_01_Venta";
            this.chkOrigenInsumo_01_Venta.Size = new System.Drawing.Size(109, 19);
            this.chkOrigenInsumo_01_Venta.TabIndex = 0;
            this.chkOrigenInsumo_01_Venta.Text = "Venta";
            this.chkOrigenInsumo_01_Venta.UseVisualStyleBackColor = true;
            // 
            // Frame_06_TipoInsumo
            // 
            this.Frame_06_TipoInsumo.Controls.Add(this.chkTipoDeInsumo_02_MaterialDeCuracion);
            this.Frame_06_TipoInsumo.Controls.Add(this.chkTipoDeInsumo_01_Medicamento);
            this.Frame_06_TipoInsumo.Location = new System.Drawing.Point(299, 305);
            this.Frame_06_TipoInsumo.Name = "Frame_06_TipoInsumo";
            this.Frame_06_TipoInsumo.Size = new System.Drawing.Size(338, 50);
            this.Frame_06_TipoInsumo.TabIndex = 2;
            this.Frame_06_TipoInsumo.TabStop = false;
            this.Frame_06_TipoInsumo.Text = "Tipo de Insumos";
            // 
            // chkTipoDeInsumo_02_MaterialDeCuracion
            // 
            this.chkTipoDeInsumo_02_MaterialDeCuracion.Location = new System.Drawing.Point(155, 21);
            this.chkTipoDeInsumo_02_MaterialDeCuracion.Name = "chkTipoDeInsumo_02_MaterialDeCuracion";
            this.chkTipoDeInsumo_02_MaterialDeCuracion.Size = new System.Drawing.Size(132, 19);
            this.chkTipoDeInsumo_02_MaterialDeCuracion.TabIndex = 1;
            this.chkTipoDeInsumo_02_MaterialDeCuracion.Text = "Material de curación";
            this.chkTipoDeInsumo_02_MaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // chkTipoDeInsumo_01_Medicamento
            // 
            this.chkTipoDeInsumo_01_Medicamento.Location = new System.Drawing.Point(42, 21);
            this.chkTipoDeInsumo_01_Medicamento.Name = "chkTipoDeInsumo_01_Medicamento";
            this.chkTipoDeInsumo_01_Medicamento.Size = new System.Drawing.Size(103, 19);
            this.chkTipoDeInsumo_01_Medicamento.TabIndex = 0;
            this.chkTipoDeInsumo_01_Medicamento.Text = "Medicamento";
            this.chkTipoDeInsumo_01_Medicamento.UseVisualStyleBackColor = true;
            // 
            // Frame_02_FuentesFinanciamiento
            // 
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.btnLimpiarFF);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblIdSubCliente);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblSubCliente);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.label1);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.btnAgregarFuentes);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.grdFuentesDeFinanciamiento);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblIdCliente);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblConcepto);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblRubro);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.txtConcepto);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.lblCliente);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.label9);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.label3);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.label7);
            this.Frame_02_FuentesFinanciamiento.Controls.Add(this.txtRubro);
            this.Frame_02_FuentesFinanciamiento.Location = new System.Drawing.Point(11, 6);
            this.Frame_02_FuentesFinanciamiento.Name = "Frame_02_FuentesFinanciamiento";
            this.Frame_02_FuentesFinanciamiento.Size = new System.Drawing.Size(1157, 296);
            this.Frame_02_FuentesFinanciamiento.TabIndex = 0;
            this.Frame_02_FuentesFinanciamiento.TabStop = false;
            this.Frame_02_FuentesFinanciamiento.Text = "Fuentes de Finaciamiento";
            // 
            // btnLimpiarFF
            // 
            this.btnLimpiarFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarFF.Location = new System.Drawing.Point(1007, 66);
            this.btnLimpiarFF.Name = "btnLimpiarFF";
            this.btnLimpiarFF.Size = new System.Drawing.Size(137, 44);
            this.btnLimpiarFF.TabIndex = 49;
            this.btnLimpiarFF.Text = "Limpiar lista";
            this.btnLimpiarFF.UseVisualStyleBackColor = true;
            this.btnLimpiarFF.Click += new System.EventHandler(this.btnLimpiarFF_Click);
            // 
            // lblIdSubCliente
            // 
            this.lblIdSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdSubCliente.Location = new System.Drawing.Point(150, 91);
            this.lblIdSubCliente.Name = "lblIdSubCliente";
            this.lblIdSubCliente.Size = new System.Drawing.Size(100, 23);
            this.lblIdSubCliente.TabIndex = 3;
            this.lblIdSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(256, 91);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(741, 23);
            this.lblSubCliente.TabIndex = 40;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 18);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAgregarFuentes
            // 
            this.btnAgregarFuentes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarFuentes.Location = new System.Drawing.Point(1007, 17);
            this.btnAgregarFuentes.Name = "btnAgregarFuentes";
            this.btnAgregarFuentes.Size = new System.Drawing.Size(137, 44);
            this.btnAgregarFuentes.TabIndex = 48;
            this.btnAgregarFuentes.Text = "Agregar al listado";
            this.btnAgregarFuentes.UseVisualStyleBackColor = true;
            this.btnAgregarFuentes.Click += new System.EventHandler(this.btnAgregarFuentes_Click);
            // 
            // grdFuentesDeFinanciamiento
            // 
            this.grdFuentesDeFinanciamiento.AccessibleDescription = "grdFuentesDeFinanciamiento, Sheet1, Row 0, Column 0, ";
            this.grdFuentesDeFinanciamiento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFuentesDeFinanciamiento.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFuentesDeFinanciamiento.Location = new System.Drawing.Point(8, 117);
            this.grdFuentesDeFinanciamiento.Name = "grdFuentesDeFinanciamiento";
            this.grdFuentesDeFinanciamiento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFuentesDeFinanciamiento.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFuentesDeFinanciamiento_Sheet1});
            this.grdFuentesDeFinanciamiento.Size = new System.Drawing.Size(1141, 175);
            this.grdFuentesDeFinanciamiento.TabIndex = 47;
            this.grdFuentesDeFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFuentesDeFinanciamiento_KeyDown);
            // 
            // grdFuentesDeFinanciamiento_Sheet1
            // 
            this.grdFuentesDeFinanciamiento_Sheet1.Reset();
            this.grdFuentesDeFinanciamiento_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFuentesDeFinanciamiento_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnCount = 9;
            this.grdFuentesDeFinanciamiento_Sheet1.RowCount = 12;
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fuente financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Tipo de fuente";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Es diferencial";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave Financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "IdCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Cliente";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "IdSubCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "SubCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).CellType = textCellType37;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).Label = "Fuente financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(0).Width = 80F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).CellType = textCellType38;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).Label = "Tipo de fuente";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(1).Width = 120F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(2).CellType = checkBoxCellType5;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(2).Label = "Es diferencial";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(2).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(3).CellType = textCellType39;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(3).Label = "Clave Financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(3).Width = 90F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).CellType = textCellType40;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).Label = "Financiamiento";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(4).Width = 240F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(5).CellType = textCellType41;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(5).Label = "IdCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(5).Visible = false;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).CellType = textCellType42;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).Label = "Cliente";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(6).Width = 200F;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(7).CellType = textCellType43;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(7).Label = "IdSubCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(7).Visible = false;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).CellType = textCellType44;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).Label = "SubCliente";
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).Locked = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFuentesDeFinanciamiento_Sheet1.Columns.Get(8).Width = 200F;
            this.grdFuentesDeFinanciamiento_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFuentesDeFinanciamiento_Sheet1.Rows.Default.Height = 25F;
            this.grdFuentesDeFinanciamiento_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdCliente.Location = new System.Drawing.Point(150, 66);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(100, 23);
            this.lblIdCliente.TabIndex = 2;
            this.lblIdCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConcepto
            // 
            this.lblConcepto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConcepto.Location = new System.Drawing.Point(256, 41);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(741, 23);
            this.lblConcepto.TabIndex = 46;
            this.lblConcepto.Text = "Segmento financiamiento :";
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRubro
            // 
            this.lblRubro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRubro.Location = new System.Drawing.Point(256, 17);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(741, 23);
            this.lblRubro.TabIndex = 43;
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConcepto
            // 
            this.txtConcepto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConcepto.Decimales = 2;
            this.txtConcepto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtConcepto.ForeColor = System.Drawing.Color.Black;
            this.txtConcepto.Location = new System.Drawing.Point(150, 41);
            this.txtConcepto.MaxLength = 4;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.PermitirApostrofo = false;
            this.txtConcepto.PermitirNegativos = false;
            this.txtConcepto.Size = new System.Drawing.Size(100, 20);
            this.txtConcepto.TabIndex = 1;
            this.txtConcepto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            this.txtConcepto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConcepto_KeyDown);
            this.txtConcepto.Validating += new System.ComponentModel.CancelEventHandler(this.txtConcepto_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(256, 66);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(741, 23);
            this.lblCliente.TabIndex = 37;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 18);
            this.label9.TabIndex = 42;
            this.label9.Text = "Fuente de financiamiento :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente / SubCliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 45;
            this.label7.Text = "Segmento financiamiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRubro
            // 
            this.txtRubro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRubro.Decimales = 2;
            this.txtRubro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRubro.ForeColor = System.Drawing.Color.Black;
            this.txtRubro.Location = new System.Drawing.Point(150, 17);
            this.txtRubro.MaxLength = 4;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.PermitirApostrofo = false;
            this.txtRubro.PermitirNegativos = false;
            this.txtRubro.Size = new System.Drawing.Size(100, 20);
            this.txtRubro.TabIndex = 0;
            this.txtRubro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRubro.TextChanged += new System.EventHandler(this.txtRubro_TextChanged);
            this.txtRubro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRubro_KeyDown);
            this.txtRubro.Validating += new System.ComponentModel.CancelEventHandler(this.txtRubro_Validating);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnGenerarRemisiones,
            this.toolStripSeparator2,
            this.btnExportarPDF});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 27);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(24, 24);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(24, 24);
            this.btnEjecutar.Text = "Obtener información del folio de venta";
            this.btnEjecutar.ToolTipText = "Obtener información del folio de venta";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGenerarRemisiones
            // 
            this.btnGenerarRemisiones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarRemisiones.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarRemisiones.Image")));
            this.btnGenerarRemisiones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarRemisiones.Name = "btnGenerarRemisiones";
            this.btnGenerarRemisiones.Size = new System.Drawing.Size(24, 24);
            this.btnGenerarRemisiones.Text = "Generar remisiones";
            this.btnGenerarRemisiones.Click += new System.EventHandler(this.btnGenerarRemisiones_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPDF.Enabled = false;
            this.btnExportarPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPDF.Image")));
            this.btnExportarPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(24, 24);
            this.btnExportarPDF.Text = "Exportar a PDF";
            this.btnExportarPDF.Visible = false;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // scTabControlExt1
            // 
            this.scTabControlExt1.Appearance = SC_ControlsCS.scTabAppearance.Buttons;
            this.scTabControlExt1.BackColor = System.Drawing.SystemColors.Control;
            this.scTabControlExt1.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.scTabControlExt1.Controls.Add(this.tabPage_01_Parametros);
            this.scTabControlExt1.Controls.Add(this.tabPage_02_ProgramasSubPrograma);
            this.scTabControlExt1.Controls.Add(this.tabPage_03_Claves);
            this.scTabControlExt1.Controls.Add(this.tabPage_04_InformacionAdicional);
            this.scTabControlExt1.Controls.Add(this.tabPage_05_Unidades);
            this.scTabControlExt1.Controls.Add(this.tabPage_06_DocumentosDeComprobacion);
            this.scTabControlExt1.Controls.Add(this.tabPage_07_FacturasRelacionadas);
            this.scTabControlExt1.CustomBackColor = true;
            this.scTabControlExt1.CustomBackColorPages = true;
            this.scTabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTabControlExt1.Location = new System.Drawing.Point(0, 27);
            this.scTabControlExt1.MostrarBorde = true;
            this.scTabControlExt1.Name = "scTabControlExt1";
            this.scTabControlExt1.SelectedIndex = 0;
            this.scTabControlExt1.Size = new System.Drawing.Size(1184, 638);
            this.scTabControlExt1.TabIndex = 1;
            // 
            // tabPage_01_Parametros
            // 
            this.tabPage_01_Parametros.Controls.Add(this.Frame_11_RangoDeFechas);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_10_FormatosDeImpresion);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_08_FacturasAnticipadas);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_03_InformacionGeneral);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_07_OrigenDispensacion);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_02_FuentesFinanciamiento);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_06_TipoInsumo);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_04_TipoRemision);
            this.tabPage_01_Parametros.Controls.Add(this.Frame_05_OrigenInsumo);
            this.tabPage_01_Parametros.Location = new System.Drawing.Point(4, 28);
            this.tabPage_01_Parametros.Name = "tabPage_01_Parametros";
            this.tabPage_01_Parametros.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_01_Parametros.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_01_Parametros.TabIndex = 0;
            this.tabPage_01_Parametros.Text = "Parámetros generales";
            this.tabPage_01_Parametros.UseVisualStyleBackColor = true;
            // 
            // Frame_11_RangoDeFechas
            // 
            this.Frame_11_RangoDeFechas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_11_RangoDeFechas.Controls.Add(this.dtpFechaFinal);
            this.Frame_11_RangoDeFechas.Controls.Add(this.label2);
            this.Frame_11_RangoDeFechas.Controls.Add(this.label8);
            this.Frame_11_RangoDeFechas.Controls.Add(this.dtpFechaInicial);
            this.Frame_11_RangoDeFechas.Location = new System.Drawing.Point(645, 505);
            this.Frame_11_RangoDeFechas.Name = "Frame_11_RangoDeFechas";
            this.Frame_11_RangoDeFechas.Size = new System.Drawing.Size(523, 92);
            this.Frame_11_RangoDeFechas.TabIndex = 7;
            this.Frame_11_RangoDeFechas.TabStop = false;
            this.Frame_11_RangoDeFechas.Text = "Rango de fechas de dispensación";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(339, 36);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Location = new System.Drawing.Point(287, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.Location = new System.Drawing.Point(97, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Inicio :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(149, 36);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // Frame_10_FormatosDeImpresion
            // 
            this.Frame_10_FormatosDeImpresion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_10_FormatosDeImpresion.Controls.Add(this.chkGenerarDocumentos);
            this.Frame_10_FormatosDeImpresion.Controls.Add(this.cboFormatosDeImpresion);
            this.Frame_10_FormatosDeImpresion.Location = new System.Drawing.Point(299, 357);
            this.Frame_10_FormatosDeImpresion.Name = "Frame_10_FormatosDeImpresion";
            this.Frame_10_FormatosDeImpresion.Size = new System.Drawing.Size(338, 53);
            this.Frame_10_FormatosDeImpresion.TabIndex = 3;
            this.Frame_10_FormatosDeImpresion.TabStop = false;
            this.Frame_10_FormatosDeImpresion.Text = "Formatos de impresión";
            // 
            // chkGenerarDocumentos
            // 
            this.chkGenerarDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGenerarDocumentos.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGenerarDocumentos.Location = new System.Drawing.Point(157, 0);
            this.chkGenerarDocumentos.Name = "chkGenerarDocumentos";
            this.chkGenerarDocumentos.Size = new System.Drawing.Size(168, 19);
            this.chkGenerarDocumentos.TabIndex = 21;
            this.chkGenerarDocumentos.Text = "Generar documentos PDF";
            this.chkGenerarDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGenerarDocumentos.UseVisualStyleBackColor = true;
            // 
            // cboFormatosDeImpresion
            // 
            this.cboFormatosDeImpresion.BackColorEnabled = System.Drawing.Color.White;
            this.cboFormatosDeImpresion.Data = "";
            this.cboFormatosDeImpresion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormatosDeImpresion.Filtro = " 1 = 1";
            this.cboFormatosDeImpresion.FormattingEnabled = true;
            this.cboFormatosDeImpresion.ListaItemsBusqueda = 20;
            this.cboFormatosDeImpresion.Location = new System.Drawing.Point(16, 21);
            this.cboFormatosDeImpresion.MostrarToolTip = false;
            this.cboFormatosDeImpresion.Name = "cboFormatosDeImpresion";
            this.cboFormatosDeImpresion.Size = new System.Drawing.Size(309, 21);
            this.cboFormatosDeImpresion.TabIndex = 20;
            // 
            // Frame_08_FacturasAnticipadas
            // 
            this.Frame_08_FacturasAnticipadas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.chkProcesar_SoloClavesReferenciaRemisiones);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.txtFactura_Folio);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.chkEsRelacionDeMontos);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.label12);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.chkEsFacturaPreviaEnCajas);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.txtFactura_Serie);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.label11);
            this.Frame_08_FacturasAnticipadas.Controls.Add(this.chkEsRelacionFacturaPrevia);
            this.Frame_08_FacturasAnticipadas.Location = new System.Drawing.Point(645, 406);
            this.Frame_08_FacturasAnticipadas.Name = "Frame_08_FacturasAnticipadas";
            this.Frame_08_FacturasAnticipadas.Size = new System.Drawing.Size(520, 96);
            this.Frame_08_FacturasAnticipadas.TabIndex = 6;
            this.Frame_08_FacturasAnticipadas.TabStop = false;
            this.Frame_08_FacturasAnticipadas.Text = "Remisiones asociadas a facturas existentes";
            // 
            // chkProcesar_SoloClavesReferenciaRemisiones
            // 
            this.chkProcesar_SoloClavesReferenciaRemisiones.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkProcesar_SoloClavesReferenciaRemisiones.Location = new System.Drawing.Point(78, 71);
            this.chkProcesar_SoloClavesReferenciaRemisiones.Name = "chkProcesar_SoloClavesReferenciaRemisiones";
            this.chkProcesar_SoloClavesReferenciaRemisiones.Size = new System.Drawing.Size(210, 19);
            this.chkProcesar_SoloClavesReferenciaRemisiones.TabIndex = 3;
            this.chkProcesar_SoloClavesReferenciaRemisiones.Text = "Procesar solo claves con referencias";
            this.chkProcesar_SoloClavesReferenciaRemisiones.UseVisualStyleBackColor = true;
            // 
            // txtFactura_Folio
            // 
            this.txtFactura_Folio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFactura_Folio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFactura_Folio.Decimales = 2;
            this.txtFactura_Folio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFactura_Folio.ForeColor = System.Drawing.Color.Black;
            this.txtFactura_Folio.Location = new System.Drawing.Point(381, 47);
            this.txtFactura_Folio.MaxLength = 8;
            this.txtFactura_Folio.Name = "txtFactura_Folio";
            this.txtFactura_Folio.PermitirApostrofo = false;
            this.txtFactura_Folio.PermitirNegativos = false;
            this.txtFactura_Folio.Size = new System.Drawing.Size(101, 20);
            this.txtFactura_Folio.TabIndex = 5;
            this.txtFactura_Folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkEsRelacionDeMontos
            // 
            this.chkEsRelacionDeMontos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkEsRelacionDeMontos.Location = new System.Drawing.Point(78, 53);
            this.chkEsRelacionDeMontos.Name = "chkEsRelacionDeMontos";
            this.chkEsRelacionDeMontos.Size = new System.Drawing.Size(176, 19);
            this.chkEsRelacionDeMontos.TabIndex = 2;
            this.chkEsRelacionDeMontos.Text = "Relacionar por montos";
            this.chkEsRelacionDeMontos.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.Location = new System.Drawing.Point(289, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "Folio factura :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkEsFacturaPreviaEnCajas
            // 
            this.chkEsFacturaPreviaEnCajas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkEsFacturaPreviaEnCajas.Location = new System.Drawing.Point(78, 35);
            this.chkEsFacturaPreviaEnCajas.Name = "chkEsFacturaPreviaEnCajas";
            this.chkEsFacturaPreviaEnCajas.Size = new System.Drawing.Size(176, 19);
            this.chkEsFacturaPreviaEnCajas.TabIndex = 1;
            this.chkEsFacturaPreviaEnCajas.Text = "Es factura previa en cajas";
            this.chkEsFacturaPreviaEnCajas.UseVisualStyleBackColor = true;
            // 
            // txtFactura_Serie
            // 
            this.txtFactura_Serie.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFactura_Serie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFactura_Serie.Decimales = 2;
            this.txtFactura_Serie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFactura_Serie.ForeColor = System.Drawing.Color.Black;
            this.txtFactura_Serie.Location = new System.Drawing.Point(381, 21);
            this.txtFactura_Serie.MaxLength = 8;
            this.txtFactura_Serie.Name = "txtFactura_Serie";
            this.txtFactura_Serie.PermitirApostrofo = false;
            this.txtFactura_Serie.PermitirNegativos = false;
            this.txtFactura_Serie.Size = new System.Drawing.Size(101, 20);
            this.txtFactura_Serie.TabIndex = 4;
            this.txtFactura_Serie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.Location = new System.Drawing.Point(289, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 12);
            this.label11.TabIndex = 35;
            this.label11.Text = "Serie factura :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkEsRelacionFacturaPrevia
            // 
            this.chkEsRelacionFacturaPrevia.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkEsRelacionFacturaPrevia.Location = new System.Drawing.Point(78, 17);
            this.chkEsRelacionFacturaPrevia.Name = "chkEsRelacionFacturaPrevia";
            this.chkEsRelacionFacturaPrevia.Size = new System.Drawing.Size(176, 19);
            this.chkEsRelacionFacturaPrevia.TabIndex = 0;
            this.chkEsRelacionFacturaPrevia.Text = "Es relacionada a factura";
            this.chkEsRelacionFacturaPrevia.UseVisualStyleBackColor = true;
            this.chkEsRelacionFacturaPrevia.CheckedChanged += new System.EventHandler(this.chkEsRelacionFacturaPrevia_CheckedChanged);
            // 
            // Frame_03_InformacionGeneral
            // 
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkExcluirCantidadesConDecimales);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkEsRemisionGeneral);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkEsProgramaEspecial);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkAplicarDocumentos);
            this.Frame_03_InformacionGeneral.Controls.Add(this.cboTipoDeBeneficiarios);
            this.Frame_03_InformacionGeneral.Controls.Add(this.label5);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkAsignarReferencias);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkProcesarParcialidades);
            this.Frame_03_InformacionGeneral.Controls.Add(this.cboNivelDeInformacion);
            this.Frame_03_InformacionGeneral.Controls.Add(this.label4);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkProcesarCantidadesExcedentes);
            this.Frame_03_InformacionGeneral.Controls.Add(this.chkBeneficiarios_x_Jurisdiccion);
            this.Frame_03_InformacionGeneral.Location = new System.Drawing.Point(11, 406);
            this.Frame_03_InformacionGeneral.Name = "Frame_03_InformacionGeneral";
            this.Frame_03_InformacionGeneral.Size = new System.Drawing.Size(628, 192);
            this.Frame_03_InformacionGeneral.TabIndex = 4;
            this.Frame_03_InformacionGeneral.TabStop = false;
            this.Frame_03_InformacionGeneral.Text = "Información general";
            // 
            // chkExcluirCantidadesConDecimales
            // 
            this.chkExcluirCantidadesConDecimales.Location = new System.Drawing.Point(149, 139);
            this.chkExcluirCantidadesConDecimales.Name = "chkExcluirCantidadesConDecimales";
            this.chkExcluirCantidadesConDecimales.Size = new System.Drawing.Size(210, 19);
            this.chkExcluirCantidadesConDecimales.TabIndex = 5;
            this.chkExcluirCantidadesConDecimales.Text = "Excluir cantidades con decimales";
            this.chkExcluirCantidadesConDecimales.UseVisualStyleBackColor = true;
            // 
            // chkEsRemisionGeneral
            // 
            this.chkEsRemisionGeneral.Location = new System.Drawing.Point(149, 161);
            this.chkEsRemisionGeneral.Name = "chkEsRemisionGeneral";
            this.chkEsRemisionGeneral.Size = new System.Drawing.Size(463, 19);
            this.chkEsRemisionGeneral.TabIndex = 9;
            this.chkEsRemisionGeneral.Text = "Remisión general (Medicamento y Material de curación)";
            this.chkEsRemisionGeneral.UseVisualStyleBackColor = true;
            // 
            // chkEsProgramaEspecial
            // 
            this.chkEsProgramaEspecial.Location = new System.Drawing.Point(385, 119);
            this.chkEsProgramaEspecial.Name = "chkEsProgramaEspecial";
            this.chkEsProgramaEspecial.Size = new System.Drawing.Size(233, 19);
            this.chkEsProgramaEspecial.TabIndex = 8;
            this.chkEsProgramaEspecial.Text = "Es programa especial";
            this.chkEsProgramaEspecial.UseVisualStyleBackColor = true;
            // 
            // chkAplicarDocumentos
            // 
            this.chkAplicarDocumentos.Location = new System.Drawing.Point(149, 117);
            this.chkAplicarDocumentos.Name = "chkAplicarDocumentos";
            this.chkAplicarDocumentos.Size = new System.Drawing.Size(210, 19);
            this.chkAplicarDocumentos.TabIndex = 4;
            this.chkAplicarDocumentos.Text = "Aplicar documentos";
            this.chkAplicarDocumentos.UseVisualStyleBackColor = true;
            // 
            // cboTipoDeBeneficiarios
            // 
            this.cboTipoDeBeneficiarios.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoDeBeneficiarios.Data = "";
            this.cboTipoDeBeneficiarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDeBeneficiarios.Filtro = " 1 = 1";
            this.cboTipoDeBeneficiarios.FormattingEnabled = true;
            this.cboTipoDeBeneficiarios.ListaItemsBusqueda = 20;
            this.cboTipoDeBeneficiarios.Location = new System.Drawing.Point(149, 46);
            this.cboTipoDeBeneficiarios.MostrarToolTip = false;
            this.cboTipoDeBeneficiarios.Name = "cboTipoDeBeneficiarios";
            this.cboTipoDeBeneficiarios.Size = new System.Drawing.Size(463, 21);
            this.cboTipoDeBeneficiarios.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Beneficiarios :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkAsignarReferencias
            // 
            this.chkAsignarReferencias.Location = new System.Drawing.Point(385, 96);
            this.chkAsignarReferencias.Name = "chkAsignarReferencias";
            this.chkAsignarReferencias.Size = new System.Drawing.Size(233, 19);
            this.chkAsignarReferencias.TabIndex = 7;
            this.chkAsignarReferencias.Text = "Asignar referencias";
            this.chkAsignarReferencias.UseVisualStyleBackColor = true;
            // 
            // chkProcesarParcialidades
            // 
            this.chkProcesarParcialidades.Location = new System.Drawing.Point(149, 95);
            this.chkProcesarParcialidades.Name = "chkProcesarParcialidades";
            this.chkProcesarParcialidades.Size = new System.Drawing.Size(210, 19);
            this.chkProcesarParcialidades.TabIndex = 3;
            this.chkProcesarParcialidades.Text = "Procesar parcialidades";
            this.chkProcesarParcialidades.UseVisualStyleBackColor = true;
            // 
            // cboNivelDeInformacion
            // 
            this.cboNivelDeInformacion.BackColorEnabled = System.Drawing.Color.White;
            this.cboNivelDeInformacion.Data = "";
            this.cboNivelDeInformacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNivelDeInformacion.Filtro = " 1 = 1";
            this.cboNivelDeInformacion.FormattingEnabled = true;
            this.cboNivelDeInformacion.ListaItemsBusqueda = 20;
            this.cboNivelDeInformacion.Location = new System.Drawing.Point(149, 19);
            this.cboNivelDeInformacion.MostrarToolTip = false;
            this.cboNivelDeInformacion.Name = "cboNivelDeInformacion";
            this.cboNivelDeInformacion.Size = new System.Drawing.Size(463, 21);
            this.cboNivelDeInformacion.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Información :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkProcesarCantidadesExcedentes
            // 
            this.chkProcesarCantidadesExcedentes.Location = new System.Drawing.Point(385, 73);
            this.chkProcesarCantidadesExcedentes.Name = "chkProcesarCantidadesExcedentes";
            this.chkProcesarCantidadesExcedentes.Size = new System.Drawing.Size(233, 19);
            this.chkProcesarCantidadesExcedentes.TabIndex = 6;
            this.chkProcesarCantidadesExcedentes.Text = "Procesar cantidades excedentes";
            this.chkProcesarCantidadesExcedentes.UseVisualStyleBackColor = true;
            // 
            // chkBeneficiarios_x_Jurisdiccion
            // 
            this.chkBeneficiarios_x_Jurisdiccion.Location = new System.Drawing.Point(149, 73);
            this.chkBeneficiarios_x_Jurisdiccion.Name = "chkBeneficiarios_x_Jurisdiccion";
            this.chkBeneficiarios_x_Jurisdiccion.Size = new System.Drawing.Size(210, 19);
            this.chkBeneficiarios_x_Jurisdiccion.TabIndex = 2;
            this.chkBeneficiarios_x_Jurisdiccion.Text = "Beneficiarios por jurisdicción";
            this.chkBeneficiarios_x_Jurisdiccion.UseVisualStyleBackColor = true;
            // 
            // Frame_07_OrigenDispensacion
            // 
            this.Frame_07_OrigenDispensacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_07_OrigenDispensacion.Controls.Add(this.chkEsComplemento);
            this.Frame_07_OrigenDispensacion.Controls.Add(this.cboOrigenDispensacion);
            this.Frame_07_OrigenDispensacion.Controls.Add(this.label6);
            this.Frame_07_OrigenDispensacion.Controls.Add(this.chkSeparar__Venta_y_Vales);
            this.Frame_07_OrigenDispensacion.Location = new System.Drawing.Point(645, 305);
            this.Frame_07_OrigenDispensacion.Name = "Frame_07_OrigenDispensacion";
            this.Frame_07_OrigenDispensacion.Size = new System.Drawing.Size(520, 100);
            this.Frame_07_OrigenDispensacion.TabIndex = 5;
            this.Frame_07_OrigenDispensacion.TabStop = false;
            this.Frame_07_OrigenDispensacion.Text = "Origen información";
            // 
            // chkEsComplemento
            // 
            this.chkEsComplemento.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkEsComplemento.Location = new System.Drawing.Point(114, 65);
            this.chkEsComplemento.Name = "chkEsComplemento";
            this.chkEsComplemento.Size = new System.Drawing.Size(320, 19);
            this.chkEsComplemento.TabIndex = 2;
            this.chkEsComplemento.Text = "Es complemento (Diferenciador y/o Incremento)";
            this.chkEsComplemento.UseVisualStyleBackColor = true;
            // 
            // cboOrigenDispensacion
            // 
            this.cboOrigenDispensacion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboOrigenDispensacion.BackColorEnabled = System.Drawing.Color.White;
            this.cboOrigenDispensacion.Data = "";
            this.cboOrigenDispensacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrigenDispensacion.Filtro = " 1 = 1";
            this.cboOrigenDispensacion.FormattingEnabled = true;
            this.cboOrigenDispensacion.ListaItemsBusqueda = 20;
            this.cboOrigenDispensacion.Location = new System.Drawing.Point(114, 17);
            this.cboOrigenDispensacion.MostrarToolTip = false;
            this.cboOrigenDispensacion.Name = "cboOrigenDispensacion";
            this.cboOrigenDispensacion.Size = new System.Drawing.Size(368, 21);
            this.cboOrigenDispensacion.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.Location = new System.Drawing.Point(9, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Dispensación :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSeparar__Venta_y_Vales
            // 
            this.chkSeparar__Venta_y_Vales.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkSeparar__Venta_y_Vales.Location = new System.Drawing.Point(114, 44);
            this.chkSeparar__Venta_y_Vales.Name = "chkSeparar__Venta_y_Vales";
            this.chkSeparar__Venta_y_Vales.Size = new System.Drawing.Size(320, 19);
            this.chkSeparar__Venta_y_Vales.TabIndex = 1;
            this.chkSeparar__Venta_y_Vales.Text = "Separar Ventas y Vales";
            this.chkSeparar__Venta_y_Vales.UseVisualStyleBackColor = true;
            // 
            // tabPage_02_ProgramasSubPrograma
            // 
            this.tabPage_02_ProgramasSubPrograma.Controls.Add(this.Frame_21_ProgramasSubProgramas);
            this.tabPage_02_ProgramasSubPrograma.Location = new System.Drawing.Point(4, 28);
            this.tabPage_02_ProgramasSubPrograma.Name = "tabPage_02_ProgramasSubPrograma";
            this.tabPage_02_ProgramasSubPrograma.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_02_ProgramasSubPrograma.TabIndex = 5;
            this.tabPage_02_ProgramasSubPrograma.Text = "Parámetros Programas-SubProgramas";
            // 
            // Frame_21_ProgramasSubProgramas
            // 
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.lblSubPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.lblPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.txtIdSubPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.label40);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.label41);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.txtIdPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.btnLimpiarPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.btnAgregarPrograma);
            this.Frame_21_ProgramasSubProgramas.Controls.Add(this.grdProgramasSubProgramas);
            this.Frame_21_ProgramasSubProgramas.Location = new System.Drawing.Point(11, 6);
            this.Frame_21_ProgramasSubProgramas.Name = "Frame_21_ProgramasSubProgramas";
            this.Frame_21_ProgramasSubProgramas.Size = new System.Drawing.Size(1157, 592);
            this.Frame_21_ProgramasSubProgramas.TabIndex = 4;
            this.Frame_21_ProgramasSubProgramas.TabStop = false;
            this.Frame_21_ProgramasSubProgramas.Text = "Programas - SubProgramas";
            this.Frame_21_ProgramasSubProgramas.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // lblSubPrograma
            // 
            this.lblSubPrograma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPrograma.Location = new System.Drawing.Point(256, 41);
            this.lblSubPrograma.Name = "lblSubPrograma";
            this.lblSubPrograma.Size = new System.Drawing.Size(607, 23);
            this.lblSubPrograma.TabIndex = 55;
            this.lblSubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPrograma
            // 
            this.lblPrograma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrograma.Location = new System.Drawing.Point(256, 17);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(607, 23);
            this.lblPrograma.TabIndex = 53;
            this.lblPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdSubPrograma
            // 
            this.txtIdSubPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdSubPrograma.Decimales = 2;
            this.txtIdSubPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdSubPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtIdSubPrograma.Location = new System.Drawing.Point(150, 41);
            this.txtIdSubPrograma.MaxLength = 4;
            this.txtIdSubPrograma.Name = "txtIdSubPrograma";
            this.txtIdSubPrograma.PermitirApostrofo = false;
            this.txtIdSubPrograma.PermitirNegativos = false;
            this.txtIdSubPrograma.Size = new System.Drawing.Size(100, 20);
            this.txtIdSubPrograma.TabIndex = 51;
            this.txtIdSubPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdSubPrograma.TextChanged += new System.EventHandler(this.txtIdSubPrograma_TextChanged);
            this.txtIdSubPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdSubPrograma_KeyDown);
            this.txtIdSubPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdSubPrograma_Validating);
            // 
            // label40
            // 
            this.label40.Location = new System.Drawing.Point(7, 19);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(138, 18);
            this.label40.TabIndex = 52;
            this.label40.Text = "Programa :";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.Location = new System.Drawing.Point(7, 43);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(138, 18);
            this.label41.TabIndex = 54;
            this.label41.Text = "Sub-Programa :";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdPrograma
            // 
            this.txtIdPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPrograma.Decimales = 2;
            this.txtIdPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtIdPrograma.Location = new System.Drawing.Point(150, 17);
            this.txtIdPrograma.MaxLength = 4;
            this.txtIdPrograma.Name = "txtIdPrograma";
            this.txtIdPrograma.PermitirApostrofo = false;
            this.txtIdPrograma.PermitirNegativos = false;
            this.txtIdPrograma.Size = new System.Drawing.Size(100, 20);
            this.txtIdPrograma.TabIndex = 50;
            this.txtIdPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPrograma.TextChanged += new System.EventHandler(this.txtIdPrograma_TextChanged);
            this.txtIdPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPrograma_KeyDown);
            this.txtIdPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPrograma_Validating);
            // 
            // btnLimpiarPrograma
            // 
            this.btnLimpiarPrograma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarPrograma.Location = new System.Drawing.Point(1012, 14);
            this.btnLimpiarPrograma.Name = "btnLimpiarPrograma";
            this.btnLimpiarPrograma.Size = new System.Drawing.Size(137, 29);
            this.btnLimpiarPrograma.TabIndex = 49;
            this.btnLimpiarPrograma.Text = "Limpiar lista";
            this.btnLimpiarPrograma.UseVisualStyleBackColor = true;
            this.btnLimpiarPrograma.Click += new System.EventHandler(this.btnLimpiarPrograma_Click);
            // 
            // btnAgregarPrograma
            // 
            this.btnAgregarPrograma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarPrograma.Location = new System.Drawing.Point(869, 14);
            this.btnAgregarPrograma.Name = "btnAgregarPrograma";
            this.btnAgregarPrograma.Size = new System.Drawing.Size(137, 29);
            this.btnAgregarPrograma.TabIndex = 48;
            this.btnAgregarPrograma.Text = "Agregar al listado";
            this.btnAgregarPrograma.UseVisualStyleBackColor = true;
            this.btnAgregarPrograma.Click += new System.EventHandler(this.btnAgregarPrograma_Click);
            // 
            // grdProgramasSubProgramas
            // 
            this.grdProgramasSubProgramas.AccessibleDescription = "grdProgramasSubProgramas, Sheet1, Row 0, Column 0, ";
            this.grdProgramasSubProgramas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProgramasSubProgramas.BackColor = System.Drawing.SystemColors.Control;
            this.grdProgramasSubProgramas.Location = new System.Drawing.Point(8, 75);
            this.grdProgramasSubProgramas.Name = "grdProgramasSubProgramas";
            this.grdProgramasSubProgramas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProgramasSubProgramas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProgramasSubProgramas_Sheet1});
            this.grdProgramasSubProgramas.Size = new System.Drawing.Size(1141, 511);
            this.grdProgramasSubProgramas.TabIndex = 47;
            this.grdProgramasSubProgramas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProgramasSubProgramas_KeyDown);
            // 
            // grdProgramasSubProgramas_Sheet1
            // 
            this.grdProgramasSubProgramas_Sheet1.Reset();
            this.grdProgramasSubProgramas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProgramasSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProgramasSubProgramas_Sheet1.ColumnCount = 4;
            this.grdProgramasSubProgramas_Sheet1.RowCount = 10;
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Id SubPrograma";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "SubPrograma";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).CellType = textCellType45;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).Label = "Id Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).Width = 120F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).CellType = textCellType46;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Label = "Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Width = 350F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).CellType = textCellType47;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Label = "Id SubPrograma";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Width = 120F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).CellType = textCellType48;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Label = "SubPrograma";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Width = 350F;
            this.grdProgramasSubProgramas_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdProgramasSubProgramas_Sheet1.Rows.Default.Height = 25F;
            this.grdProgramasSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPage_03_Claves
            // 
            this.tabPage_03_Claves.Controls.Add(this.groupBox4);
            this.tabPage_03_Claves.Controls.Add(this.groupBox3);
            this.tabPage_03_Claves.Location = new System.Drawing.Point(4, 28);
            this.tabPage_03_Claves.Name = "tabPage_03_Claves";
            this.tabPage_03_Claves.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_03_Claves.TabIndex = 6;
            this.tabPage_03_Claves.Text = "Parámetros de Claves ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.grdClavesExcluidas);
            this.groupBox4.Controls.Add(this.btnLimpiarClaveExcluida);
            this.groupBox4.Controls.Add(this.btnAgregarClaveExcluida);
            this.groupBox4.Controls.Add(this.lblClaveExcluida);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.txtClaveSSA_Excluida);
            this.groupBox4.Location = new System.Drawing.Point(11, 307);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1157, 293);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Claves excluidas";
            // 
            // grdClavesExcluidas
            // 
            this.grdClavesExcluidas.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdClavesExcluidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClavesExcluidas.BackColor = System.Drawing.SystemColors.Control;
            this.grdClavesExcluidas.Location = new System.Drawing.Point(8, 49);
            this.grdClavesExcluidas.Name = "grdClavesExcluidas";
            this.grdClavesExcluidas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClavesExcluidas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClavesExcluidas_Sheet1});
            this.grdClavesExcluidas.Size = new System.Drawing.Size(1141, 238);
            this.grdClavesExcluidas.TabIndex = 51;
            this.grdClavesExcluidas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdClavesExcluidas_KeyDown);
            // 
            // grdClavesExcluidas_Sheet1
            // 
            this.grdClavesExcluidas_Sheet1.Reset();
            this.grdClavesExcluidas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClavesExcluidas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClavesExcluidas_Sheet1.ColumnCount = 2;
            this.grdClavesExcluidas_Sheet1.RowCount = 10;
            this.grdClavesExcluidas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdClavesExcluidas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave";
            this.grdClavesExcluidas_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).CellType = textCellType49;
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).Locked = true;
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesExcluidas_Sheet1.Columns.Get(0).Width = 200F;
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).CellType = textCellType50;
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).Label = "Descripción Clave";
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).Locked = true;
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesExcluidas_Sheet1.Columns.Get(1).Width = 550F;
            this.grdClavesExcluidas_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdClavesExcluidas_Sheet1.Rows.Default.Height = 25F;
            this.grdClavesExcluidas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnLimpiarClaveExcluida
            // 
            this.btnLimpiarClaveExcluida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarClaveExcluida.Location = new System.Drawing.Point(1012, 14);
            this.btnLimpiarClaveExcluida.Name = "btnLimpiarClaveExcluida";
            this.btnLimpiarClaveExcluida.Size = new System.Drawing.Size(137, 29);
            this.btnLimpiarClaveExcluida.TabIndex = 49;
            this.btnLimpiarClaveExcluida.Text = "Limpiar lista";
            this.btnLimpiarClaveExcluida.UseVisualStyleBackColor = true;
            this.btnLimpiarClaveExcluida.Click += new System.EventHandler(this.btnLimpiarClaveExcluida_Click);
            // 
            // btnAgregarClaveExcluida
            // 
            this.btnAgregarClaveExcluida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarClaveExcluida.Location = new System.Drawing.Point(869, 14);
            this.btnAgregarClaveExcluida.Name = "btnAgregarClaveExcluida";
            this.btnAgregarClaveExcluida.Size = new System.Drawing.Size(137, 29);
            this.btnAgregarClaveExcluida.TabIndex = 48;
            this.btnAgregarClaveExcluida.Text = "Agregar al listado";
            this.btnAgregarClaveExcluida.UseVisualStyleBackColor = true;
            this.btnAgregarClaveExcluida.Click += new System.EventHandler(this.btnAgregarClaveExcluida_Click);
            // 
            // lblClaveExcluida
            // 
            this.lblClaveExcluida.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaveExcluida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveExcluida.Location = new System.Drawing.Point(287, 17);
            this.lblClaveExcluida.Name = "lblClaveExcluida";
            this.lblClaveExcluida.Size = new System.Drawing.Size(576, 23);
            this.lblClaveExcluida.TabIndex = 43;
            this.lblClaveExcluida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(7, 19);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(93, 18);
            this.label33.TabIndex = 42;
            this.label33.Text = "Clave SSA :";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA_Excluida
            // 
            this.txtClaveSSA_Excluida.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveSSA_Excluida.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA_Excluida.Decimales = 2;
            this.txtClaveSSA_Excluida.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA_Excluida.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA_Excluida.Location = new System.Drawing.Point(101, 18);
            this.txtClaveSSA_Excluida.MaxLength = 30;
            this.txtClaveSSA_Excluida.Name = "txtClaveSSA_Excluida";
            this.txtClaveSSA_Excluida.PermitirApostrofo = false;
            this.txtClaveSSA_Excluida.PermitirNegativos = false;
            this.txtClaveSSA_Excluida.Size = new System.Drawing.Size(180, 20);
            this.txtClaveSSA_Excluida.TabIndex = 0;
            this.txtClaveSSA_Excluida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA_Excluida.TextChanged += new System.EventHandler(this.txtClaveSSA_Excluida_TextChanged);
            this.txtClaveSSA_Excluida.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_Excluida_KeyDown);
            this.txtClaveSSA_Excluida.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Excluida_Validating);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdClavesExclusivas);
            this.groupBox3.Controls.Add(this.btnLimpiarClaveExclusiva);
            this.groupBox3.Controls.Add(this.btnAgregarClaveExclusiva);
            this.groupBox3.Controls.Add(this.lblClaveExclusiva);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.txtClaveSSA_Exclusiva);
            this.groupBox3.Location = new System.Drawing.Point(11, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1157, 293);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Claves exclusivas";
            // 
            // grdClavesExclusivas
            // 
            this.grdClavesExclusivas.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdClavesExclusivas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClavesExclusivas.BackColor = System.Drawing.SystemColors.Control;
            this.grdClavesExclusivas.Location = new System.Drawing.Point(8, 49);
            this.grdClavesExclusivas.Name = "grdClavesExclusivas";
            this.grdClavesExclusivas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClavesExclusivas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClavesExclusivas_Sheet1});
            this.grdClavesExclusivas.Size = new System.Drawing.Size(1141, 238);
            this.grdClavesExclusivas.TabIndex = 50;
            this.grdClavesExclusivas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdClavesExclusivas_KeyDown);
            // 
            // grdClavesExclusivas_Sheet1
            // 
            this.grdClavesExclusivas_Sheet1.Reset();
            this.grdClavesExclusivas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClavesExclusivas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClavesExclusivas_Sheet1.ColumnCount = 2;
            this.grdClavesExclusivas_Sheet1.RowCount = 10;
            this.grdClavesExclusivas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdClavesExclusivas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave";
            this.grdClavesExclusivas_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).CellType = textCellType51;
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).Locked = true;
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesExclusivas_Sheet1.Columns.Get(0).Width = 200F;
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).CellType = textCellType52;
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).Label = "Descripción Clave";
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).Locked = true;
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClavesExclusivas_Sheet1.Columns.Get(1).Width = 550F;
            this.grdClavesExclusivas_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdClavesExclusivas_Sheet1.Rows.Default.Height = 25F;
            this.grdClavesExclusivas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnLimpiarClaveExclusiva
            // 
            this.btnLimpiarClaveExclusiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiarClaveExclusiva.Location = new System.Drawing.Point(1012, 14);
            this.btnLimpiarClaveExclusiva.Name = "btnLimpiarClaveExclusiva";
            this.btnLimpiarClaveExclusiva.Size = new System.Drawing.Size(137, 29);
            this.btnLimpiarClaveExclusiva.TabIndex = 49;
            this.btnLimpiarClaveExclusiva.Text = "Limpiar lista";
            this.btnLimpiarClaveExclusiva.UseVisualStyleBackColor = true;
            this.btnLimpiarClaveExclusiva.Click += new System.EventHandler(this.btnLimpiarClaveExclusiva_Click);
            // 
            // btnAgregarClaveExclusiva
            // 
            this.btnAgregarClaveExclusiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarClaveExclusiva.Location = new System.Drawing.Point(869, 14);
            this.btnAgregarClaveExclusiva.Name = "btnAgregarClaveExclusiva";
            this.btnAgregarClaveExclusiva.Size = new System.Drawing.Size(137, 29);
            this.btnAgregarClaveExclusiva.TabIndex = 48;
            this.btnAgregarClaveExclusiva.Text = "Agregar al listado";
            this.btnAgregarClaveExclusiva.UseVisualStyleBackColor = true;
            this.btnAgregarClaveExclusiva.Click += new System.EventHandler(this.btnAgregarClaveExclusiva_Click);
            // 
            // lblClaveExclusiva
            // 
            this.lblClaveExclusiva.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaveExclusiva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveExclusiva.Location = new System.Drawing.Point(287, 17);
            this.lblClaveExclusiva.Name = "lblClaveExclusiva";
            this.lblClaveExclusiva.Size = new System.Drawing.Size(576, 23);
            this.lblClaveExclusiva.TabIndex = 43;
            this.lblClaveExclusiva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(7, 19);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(93, 18);
            this.label39.TabIndex = 42;
            this.label39.Text = "Clave SSA :";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA_Exclusiva
            // 
            this.txtClaveSSA_Exclusiva.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveSSA_Exclusiva.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA_Exclusiva.Decimales = 2;
            this.txtClaveSSA_Exclusiva.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA_Exclusiva.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA_Exclusiva.Location = new System.Drawing.Point(101, 18);
            this.txtClaveSSA_Exclusiva.MaxLength = 30;
            this.txtClaveSSA_Exclusiva.Name = "txtClaveSSA_Exclusiva";
            this.txtClaveSSA_Exclusiva.PermitirApostrofo = false;
            this.txtClaveSSA_Exclusiva.PermitirNegativos = false;
            this.txtClaveSSA_Exclusiva.Size = new System.Drawing.Size(180, 20);
            this.txtClaveSSA_Exclusiva.TabIndex = 0;
            this.txtClaveSSA_Exclusiva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA_Exclusiva.TextChanged += new System.EventHandler(this.txtClaveSSA_Exclusiva_TextChanged);
            this.txtClaveSSA_Exclusiva.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_Exclusiva_KeyDown);
            this.txtClaveSSA_Exclusiva.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Exclusiva_Validating);
            // 
            // tabPage_04_InformacionAdicional
            // 
            this.tabPage_04_InformacionAdicional.Controls.Add(this.groupBox6);
            this.tabPage_04_InformacionAdicional.Location = new System.Drawing.Point(4, 28);
            this.tabPage_04_InformacionAdicional.Name = "tabPage_04_InformacionAdicional";
            this.tabPage_04_InformacionAdicional.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_04_InformacionAdicional.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_04_InformacionAdicional.TabIndex = 3;
            this.tabPage_04_InformacionAdicional.Text = "Información adicional";
            this.tabPage_04_InformacionAdicional.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtInfoAdicional_01);
            this.groupBox6.Controls.Add(this.lblInfoAdicional_05);
            this.groupBox6.Controls.Add(this.lblITituloInfoAdicional_01);
            this.groupBox6.Controls.Add(this.lblITituloInfoAdicional_05);
            this.groupBox6.Controls.Add(this.lblInfoAdicional_01);
            this.groupBox6.Controls.Add(this.txtInfoAdicional_05);
            this.groupBox6.Controls.Add(this.txtInfoAdicional_02);
            this.groupBox6.Controls.Add(this.lblInfoAdicional_04);
            this.groupBox6.Controls.Add(this.lblITituloInfoAdicional_02);
            this.groupBox6.Controls.Add(this.lblITituloInfoAdicional_04);
            this.groupBox6.Controls.Add(this.lblInfoAdicional_02);
            this.groupBox6.Controls.Add(this.txtInfoAdicional_04);
            this.groupBox6.Controls.Add(this.txtInfoAdicional_03);
            this.groupBox6.Controls.Add(this.lblInfoAdicional_03);
            this.groupBox6.Controls.Add(this.lblITituloInfoAdicional_03);
            this.groupBox6.Location = new System.Drawing.Point(11, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1157, 592);
            this.groupBox6.TabIndex = 59;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Información adicional de la operación";
            // 
            // txtInfoAdicional_01
            // 
            this.txtInfoAdicional_01.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInfoAdicional_01.Decimales = 2;
            this.txtInfoAdicional_01.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtInfoAdicional_01.ForeColor = System.Drawing.Color.Black;
            this.txtInfoAdicional_01.Location = new System.Drawing.Point(90, 27);
            this.txtInfoAdicional_01.MaxLength = 100;
            this.txtInfoAdicional_01.Name = "txtInfoAdicional_01";
            this.txtInfoAdicional_01.PermitirApostrofo = false;
            this.txtInfoAdicional_01.PermitirNegativos = false;
            this.txtInfoAdicional_01.Size = new System.Drawing.Size(335, 20);
            this.txtInfoAdicional_01.TabIndex = 0;
            this.txtInfoAdicional_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblInfoAdicional_05
            // 
            this.lblInfoAdicional_05.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoAdicional_05.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAdicional_05.Location = new System.Drawing.Point(431, 130);
            this.lblInfoAdicional_05.Name = "lblInfoAdicional_05";
            this.lblInfoAdicional_05.Size = new System.Drawing.Size(702, 23);
            this.lblInfoAdicional_05.TabIndex = 58;
            this.lblInfoAdicional_05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblITituloInfoAdicional_01
            // 
            this.lblITituloInfoAdicional_01.Location = new System.Drawing.Point(18, 28);
            this.lblITituloInfoAdicional_01.Name = "lblITituloInfoAdicional_01";
            this.lblITituloInfoAdicional_01.Size = new System.Drawing.Size(67, 18);
            this.lblITituloInfoAdicional_01.TabIndex = 45;
            this.lblITituloInfoAdicional_01.Text = "Valor 1 :";
            this.lblITituloInfoAdicional_01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblITituloInfoAdicional_05
            // 
            this.lblITituloInfoAdicional_05.Location = new System.Drawing.Point(18, 132);
            this.lblITituloInfoAdicional_05.Name = "lblITituloInfoAdicional_05";
            this.lblITituloInfoAdicional_05.Size = new System.Drawing.Size(67, 18);
            this.lblITituloInfoAdicional_05.TabIndex = 57;
            this.lblITituloInfoAdicional_05.Text = "Valor 5 :";
            this.lblITituloInfoAdicional_05.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfoAdicional_01
            // 
            this.lblInfoAdicional_01.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoAdicional_01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAdicional_01.Location = new System.Drawing.Point(431, 26);
            this.lblInfoAdicional_01.Name = "lblInfoAdicional_01";
            this.lblInfoAdicional_01.Size = new System.Drawing.Size(702, 23);
            this.lblInfoAdicional_01.TabIndex = 46;
            this.lblInfoAdicional_01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfoAdicional_05
            // 
            this.txtInfoAdicional_05.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInfoAdicional_05.Decimales = 2;
            this.txtInfoAdicional_05.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtInfoAdicional_05.ForeColor = System.Drawing.Color.Black;
            this.txtInfoAdicional_05.Location = new System.Drawing.Point(90, 131);
            this.txtInfoAdicional_05.MaxLength = 100;
            this.txtInfoAdicional_05.Name = "txtInfoAdicional_05";
            this.txtInfoAdicional_05.PermitirApostrofo = false;
            this.txtInfoAdicional_05.PermitirNegativos = false;
            this.txtInfoAdicional_05.Size = new System.Drawing.Size(335, 20);
            this.txtInfoAdicional_05.TabIndex = 4;
            this.txtInfoAdicional_05.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInfoAdicional_02
            // 
            this.txtInfoAdicional_02.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInfoAdicional_02.Decimales = 2;
            this.txtInfoAdicional_02.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtInfoAdicional_02.ForeColor = System.Drawing.Color.Black;
            this.txtInfoAdicional_02.Location = new System.Drawing.Point(90, 53);
            this.txtInfoAdicional_02.MaxLength = 100;
            this.txtInfoAdicional_02.Name = "txtInfoAdicional_02";
            this.txtInfoAdicional_02.PermitirApostrofo = false;
            this.txtInfoAdicional_02.PermitirNegativos = false;
            this.txtInfoAdicional_02.Size = new System.Drawing.Size(335, 20);
            this.txtInfoAdicional_02.TabIndex = 1;
            this.txtInfoAdicional_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblInfoAdicional_04
            // 
            this.lblInfoAdicional_04.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoAdicional_04.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAdicional_04.Location = new System.Drawing.Point(431, 104);
            this.lblInfoAdicional_04.Name = "lblInfoAdicional_04";
            this.lblInfoAdicional_04.Size = new System.Drawing.Size(702, 23);
            this.lblInfoAdicional_04.TabIndex = 55;
            this.lblInfoAdicional_04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblITituloInfoAdicional_02
            // 
            this.lblITituloInfoAdicional_02.Location = new System.Drawing.Point(18, 54);
            this.lblITituloInfoAdicional_02.Name = "lblITituloInfoAdicional_02";
            this.lblITituloInfoAdicional_02.Size = new System.Drawing.Size(67, 18);
            this.lblITituloInfoAdicional_02.TabIndex = 48;
            this.lblITituloInfoAdicional_02.Text = "Valor 2 :";
            this.lblITituloInfoAdicional_02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblITituloInfoAdicional_04
            // 
            this.lblITituloInfoAdicional_04.Location = new System.Drawing.Point(18, 106);
            this.lblITituloInfoAdicional_04.Name = "lblITituloInfoAdicional_04";
            this.lblITituloInfoAdicional_04.Size = new System.Drawing.Size(67, 18);
            this.lblITituloInfoAdicional_04.TabIndex = 54;
            this.lblITituloInfoAdicional_04.Text = "Valor 4 :";
            this.lblITituloInfoAdicional_04.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfoAdicional_02
            // 
            this.lblInfoAdicional_02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoAdicional_02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAdicional_02.Location = new System.Drawing.Point(431, 52);
            this.lblInfoAdicional_02.Name = "lblInfoAdicional_02";
            this.lblInfoAdicional_02.Size = new System.Drawing.Size(702, 23);
            this.lblInfoAdicional_02.TabIndex = 49;
            this.lblInfoAdicional_02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfoAdicional_04
            // 
            this.txtInfoAdicional_04.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInfoAdicional_04.Decimales = 2;
            this.txtInfoAdicional_04.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtInfoAdicional_04.ForeColor = System.Drawing.Color.Black;
            this.txtInfoAdicional_04.Location = new System.Drawing.Point(90, 105);
            this.txtInfoAdicional_04.MaxLength = 100;
            this.txtInfoAdicional_04.Name = "txtInfoAdicional_04";
            this.txtInfoAdicional_04.PermitirApostrofo = false;
            this.txtInfoAdicional_04.PermitirNegativos = false;
            this.txtInfoAdicional_04.Size = new System.Drawing.Size(335, 20);
            this.txtInfoAdicional_04.TabIndex = 3;
            this.txtInfoAdicional_04.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtInfoAdicional_03
            // 
            this.txtInfoAdicional_03.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtInfoAdicional_03.Decimales = 2;
            this.txtInfoAdicional_03.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtInfoAdicional_03.ForeColor = System.Drawing.Color.Black;
            this.txtInfoAdicional_03.Location = new System.Drawing.Point(90, 79);
            this.txtInfoAdicional_03.MaxLength = 100;
            this.txtInfoAdicional_03.Name = "txtInfoAdicional_03";
            this.txtInfoAdicional_03.PermitirApostrofo = false;
            this.txtInfoAdicional_03.PermitirNegativos = false;
            this.txtInfoAdicional_03.Size = new System.Drawing.Size(335, 20);
            this.txtInfoAdicional_03.TabIndex = 2;
            this.txtInfoAdicional_03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblInfoAdicional_03
            // 
            this.lblInfoAdicional_03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoAdicional_03.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAdicional_03.Location = new System.Drawing.Point(431, 78);
            this.lblInfoAdicional_03.Name = "lblInfoAdicional_03";
            this.lblInfoAdicional_03.Size = new System.Drawing.Size(702, 23);
            this.lblInfoAdicional_03.TabIndex = 52;
            this.lblInfoAdicional_03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblITituloInfoAdicional_03
            // 
            this.lblITituloInfoAdicional_03.Location = new System.Drawing.Point(18, 80);
            this.lblITituloInfoAdicional_03.Name = "lblITituloInfoAdicional_03";
            this.lblITituloInfoAdicional_03.Size = new System.Drawing.Size(67, 18);
            this.lblITituloInfoAdicional_03.TabIndex = 51;
            this.lblITituloInfoAdicional_03.Text = "Valor 3 :";
            this.lblITituloInfoAdicional_03.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage_05_Unidades
            // 
            this.tabPage_05_Unidades.Controls.Add(this.chkMarcarDesmarcarFarmacias);
            this.tabPage_05_Unidades.Controls.Add(this.grdFarmacias);
            this.tabPage_05_Unidades.Location = new System.Drawing.Point(4, 28);
            this.tabPage_05_Unidades.Name = "tabPage_05_Unidades";
            this.tabPage_05_Unidades.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_05_Unidades.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_05_Unidades.TabIndex = 1;
            this.tabPage_05_Unidades.Text = "Unidades";
            this.tabPage_05_Unidades.UseVisualStyleBackColor = true;
            // 
            // chkMarcarDesmarcarFarmacias
            // 
            this.chkMarcarDesmarcarFarmacias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcarFarmacias.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarFarmacias.Location = new System.Drawing.Point(933, 12);
            this.chkMarcarDesmarcarFarmacias.Name = "chkMarcarDesmarcarFarmacias";
            this.chkMarcarDesmarcarFarmacias.Size = new System.Drawing.Size(230, 19);
            this.chkMarcarDesmarcarFarmacias.TabIndex = 49;
            this.chkMarcarDesmarcarFarmacias.Text = "Seleccionar / Deseleccionar todo";
            this.chkMarcarDesmarcarFarmacias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarFarmacias.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcarFarmacias.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcarFarmacias_CheckedChanged);
            // 
            // grdFarmacias
            // 
            this.grdFarmacias.AccessibleDescription = "grdFarmacias, Sheet1, Row 0, Column 0, ";
            this.grdFarmacias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFarmacias.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFarmacias.Location = new System.Drawing.Point(14, 37);
            this.grdFarmacias.Name = "grdFarmacias";
            this.grdFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFarmacias_Sheet1});
            this.grdFarmacias.Size = new System.Drawing.Size(1149, 558);
            this.grdFarmacias.TabIndex = 48;
            // 
            // grdFarmacias_Sheet1
            // 
            this.grdFarmacias_Sheet1.Reset();
            this.grdFarmacias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFarmacias_Sheet1.ColumnCount = 4;
            this.grdFarmacias_Sheet1.RowCount = 12;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Farmacia";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Generar remisiones";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Remisiones generadas";
            this.grdFarmacias_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdFarmacias_Sheet1.Columns.Get(0).CellType = textCellType53;
            this.grdFarmacias_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Label = "Id Farmacia";
            this.grdFarmacias_Sheet1.Columns.Get(0).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Width = 120F;
            this.grdFarmacias_Sheet1.Columns.Get(1).CellType = textCellType54;
            this.grdFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFarmacias_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdFarmacias_Sheet1.Columns.Get(1).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(1).Width = 500F;
            this.grdFarmacias_Sheet1.Columns.Get(2).CellType = checkBoxCellType6;
            this.grdFarmacias_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(2).Label = "Generar remisiones";
            this.grdFarmacias_Sheet1.Columns.Get(2).Locked = false;
            this.grdFarmacias_Sheet1.Columns.Get(2).Width = 120F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = 0D;
            this.grdFarmacias_Sheet1.Columns.Get(3).CellType = numberCellType3;
            this.grdFarmacias_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdFarmacias_Sheet1.Columns.Get(3).Label = "Remisiones generadas";
            this.grdFarmacias_Sheet1.Columns.Get(3).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(3).Width = 140F;
            this.grdFarmacias_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFarmacias_Sheet1.Rows.Default.Height = 25F;
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPage_06_DocumentosDeComprobacion
            // 
            this.tabPage_06_DocumentosDeComprobacion.Controls.Add(this.groupBox2);
            this.tabPage_06_DocumentosDeComprobacion.Location = new System.Drawing.Point(4, 28);
            this.tabPage_06_DocumentosDeComprobacion.Name = "tabPage_06_DocumentosDeComprobacion";
            this.tabPage_06_DocumentosDeComprobacion.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_06_DocumentosDeComprobacion.TabIndex = 4;
            this.tabPage_06_DocumentosDeComprobacion.Text = "Documentos para comprobación";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdDocumentos);
            this.groupBox2.Location = new System.Drawing.Point(11, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1157, 592);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentos para comprobación";
            // 
            // grdDocumentos
            // 
            this.grdDocumentos.AccessibleDescription = "grdDocumentos, Sheet1, Row 0, Column 0, ";
            this.grdDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocumentos.BackColor = System.Drawing.SystemColors.Control;
            this.grdDocumentos.Location = new System.Drawing.Point(8, 117);
            this.grdDocumentos.Name = "grdDocumentos";
            this.grdDocumentos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDocumentos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDocumentos_Sheet1});
            this.grdDocumentos.Size = new System.Drawing.Size(1141, 471);
            this.grdDocumentos.TabIndex = 47;
            // 
            // grdDocumentos_Sheet1
            // 
            this.grdDocumentos_Sheet1.Reset();
            this.grdDocumentos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDocumentos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDocumentos_Sheet1.ColumnCount = 10;
            this.grdDocumentos_Sheet1.RowCount = 10;
            this.grdDocumentos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdDocumentos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPage_07_FacturasRelacionadas
            // 
            this.tabPage_07_FacturasRelacionadas.Controls.Add(this.groupBox1);
            this.tabPage_07_FacturasRelacionadas.Location = new System.Drawing.Point(4, 28);
            this.tabPage_07_FacturasRelacionadas.Name = "tabPage_07_FacturasRelacionadas";
            this.tabPage_07_FacturasRelacionadas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_07_FacturasRelacionadas.Size = new System.Drawing.Size(1176, 606);
            this.tabPage_07_FacturasRelacionadas.TabIndex = 2;
            this.tabPage_07_FacturasRelacionadas.Text = "Facturas a relacionar";
            this.tabPage_07_FacturasRelacionadas.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdFacturas);
            this.groupBox1.Location = new System.Drawing.Point(11, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1157, 592);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Facturas para comprobación";
            // 
            // grdFacturas
            // 
            this.grdFacturas.AccessibleDescription = "grdFacturas, Sheet1, Row 0, Column 0, ";
            this.grdFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFacturas.BackColor = System.Drawing.SystemColors.Control;
            this.grdFacturas.Location = new System.Drawing.Point(8, 19);
            this.grdFacturas.Name = "grdFacturas";
            this.grdFacturas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFacturas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFacturas_Sheet1});
            this.grdFacturas.Size = new System.Drawing.Size(1141, 569);
            this.grdFacturas.TabIndex = 47;
            this.grdFacturas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFacturas_KeyDown);
            // 
            // grdFacturas_Sheet1
            // 
            this.grdFacturas_Sheet1.Reset();
            this.grdFacturas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFacturas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFacturas_Sheet1.ColumnCount = 9;
            this.grdFacturas_Sheet1.RowCount = 10;
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Serie";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Serie-Folio";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha emisión";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cliente";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fuente de financiamiento";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Segmento de financiamiento";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Tipo de documento";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Tipo de insumo";
            this.grdFacturas_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            this.grdFacturas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(0).Label = "Serie";
            this.grdFacturas_Sheet1.Columns.Get(0).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(0).Width = 80F;
            this.grdFacturas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdFacturas_Sheet1.Columns.Get(1).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(1).Width = 80F;
            this.grdFacturas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(2).Label = "Serie-Folio";
            this.grdFacturas_Sheet1.Columns.Get(2).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(3).Label = "Fecha emisión";
            this.grdFacturas_Sheet1.Columns.Get(3).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(3).Width = 130F;
            this.grdFacturas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(4).Label = "Cliente";
            this.grdFacturas_Sheet1.Columns.Get(4).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(4).Width = 200F;
            this.grdFacturas_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(5).Label = "Fuente de financiamiento";
            this.grdFacturas_Sheet1.Columns.Get(5).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(5).Width = 150F;
            this.grdFacturas_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(6).Label = "Segmento de financiamiento";
            this.grdFacturas_Sheet1.Columns.Get(6).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(6).Width = 150F;
            this.grdFacturas_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(7).Label = "Tipo de documento";
            this.grdFacturas_Sheet1.Columns.Get(7).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(7).Width = 150F;
            this.grdFacturas_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(8).Label = "Tipo de insumo";
            this.grdFacturas_Sheet1.Columns.Get(8).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(8).Width = 150F;
            this.grdFacturas_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdFacturas_Sheet1.Rows.Default.Height = 25F;
            this.grdFacturas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmRemisionesGenerales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 665);
            this.Controls.Add(this.scTabControlExt1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRemisionesGenerales";
            this.Text = "Generar remisiones de unidades";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRemisionesGenerales_Load);
            this.Frame_04_TipoRemision.ResumeLayout(false);
            this.Frame_05_OrigenInsumo.ResumeLayout(false);
            this.Frame_06_TipoInsumo.ResumeLayout(false);
            this.Frame_02_FuentesFinanciamiento.ResumeLayout(false);
            this.Frame_02_FuentesFinanciamiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuentesDeFinanciamiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFuentesDeFinanciamiento_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.scTabControlExt1.ResumeLayout(false);
            this.tabPage_01_Parametros.ResumeLayout(false);
            this.Frame_11_RangoDeFechas.ResumeLayout(false);
            this.Frame_10_FormatosDeImpresion.ResumeLayout(false);
            this.Frame_08_FacturasAnticipadas.ResumeLayout(false);
            this.Frame_08_FacturasAnticipadas.PerformLayout();
            this.Frame_03_InformacionGeneral.ResumeLayout(false);
            this.Frame_07_OrigenDispensacion.ResumeLayout(false);
            this.tabPage_02_ProgramasSubPrograma.ResumeLayout(false);
            this.Frame_21_ProgramasSubProgramas.ResumeLayout(false);
            this.Frame_21_ProgramasSubProgramas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas_Sheet1)).EndInit();
            this.tabPage_03_Claves.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExcluidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExcluidas_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExclusivas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClavesExclusivas_Sheet1)).EndInit();
            this.tabPage_04_InformacionAdicional.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage_05_Unidades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).EndInit();
            this.tabPage_06_DocumentosDeComprobacion.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocumentos_Sheet1)).EndInit();
            this.tabPage_07_FacturasRelacionadas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Frame_04_TipoRemision;
        private System.Windows.Forms.GroupBox Frame_05_OrigenInsumo;
        private System.Windows.Forms.GroupBox Frame_06_TipoInsumo;
        private System.Windows.Forms.GroupBox Frame_02_FuentesFinanciamiento;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblIdSubCliente;
        private System.Windows.Forms.Label lblConcepto;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Label lblRubro;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtConcepto;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtRubro;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGenerarRemisiones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private SC_ControlsCS.scTabControlExt scTabControlExt1;
        private System.Windows.Forms.TabPage tabPage_01_Parametros;
        private System.Windows.Forms.TabPage tabPage_05_Unidades;
        private System.Windows.Forms.CheckBox chkTipoDeRemision_02_Servicio;
        private System.Windows.Forms.CheckBox chkTipoDeRemision_01_Producto;
        private System.Windows.Forms.CheckBox chkTipoDeInsumo_02_MaterialDeCuracion;
        private System.Windows.Forms.CheckBox chkTipoDeInsumo_01_Medicamento;
        private System.Windows.Forms.CheckBox chkOrigenInsumo_02_Consignacion;
        private System.Windows.Forms.CheckBox chkOrigenInsumo_01_Venta;
        private System.Windows.Forms.GroupBox Frame_03_InformacionGeneral;
        private System.Windows.Forms.GroupBox Frame_07_OrigenDispensacion;
        private SC_ControlsCS.scComboBoxExt cboNivelDeInformacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAsignarReferencias;
        private System.Windows.Forms.CheckBox chkProcesarParcialidades;
        private System.Windows.Forms.CheckBox chkProcesarCantidadesExcedentes;
        private System.Windows.Forms.CheckBox chkBeneficiarios_x_Jurisdiccion;
        private SC_ControlsCS.scComboBoxExt cboTipoDeBeneficiarios;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkEsProgramaEspecial;
        private System.Windows.Forms.CheckBox chkAplicarDocumentos;
        private System.Windows.Forms.CheckBox chkEsRemisionGeneral;
        private System.Windows.Forms.CheckBox chkSeparar__Venta_y_Vales;
        private SC_ControlsCS.scComboBoxExt cboOrigenDispensacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkEsComplemento;
        private System.Windows.Forms.CheckBox chkExcluirCantidadesConDecimales;
        private System.Windows.Forms.GroupBox Frame_08_FacturasAnticipadas;
        private System.Windows.Forms.CheckBox chkEsRelacionFacturaPrevia;
        private System.Windows.Forms.CheckBox chkEsFacturaPreviaEnCajas;
        private System.Windows.Forms.CheckBox chkEsRelacionDeMontos;
        private SC_ControlsCS.scTextBoxExt txtFactura_Folio;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scTextBoxExt txtFactura_Serie;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkProcesar_SoloClavesReferenciaRemisiones;
        private System.Windows.Forms.TabPage tabPage_07_FacturasRelacionadas;
        private System.Windows.Forms.GroupBox Frame_10_FormatosDeImpresion;
        private SC_ControlsCS.scComboBoxExt cboFormatosDeImpresion;
        private System.Windows.Forms.CheckBox chkGenerarDocumentos;
        private System.Windows.Forms.ToolStripButton btnExportarPDF;
        private FarPoint.Win.Spread.FpSpread grdFuentesDeFinanciamiento;
        private FarPoint.Win.Spread.SheetView grdFuentesDeFinanciamiento_Sheet1;
        private System.Windows.Forms.Button btnAgregarFuentes;
        private System.Windows.Forms.Button btnLimpiarFF;
        private FarPoint.Win.Spread.FpSpread grdFarmacias;
        private FarPoint.Win.Spread.SheetView grdFarmacias_Sheet1;
        private System.Windows.Forms.GroupBox Frame_11_RangoDeFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcarFarmacias;
        private System.Windows.Forms.TabPage tabPage_04_InformacionAdicional;
        private System.Windows.Forms.TabPage tabPage_06_DocumentosDeComprobacion;
        private System.Windows.Forms.Label lblInfoAdicional_01;
        private System.Windows.Forms.Label lblITituloInfoAdicional_01;
        private SC_ControlsCS.scTextBoxExt txtInfoAdicional_01;
        private System.Windows.Forms.Label lblInfoAdicional_05;
        private System.Windows.Forms.Label lblITituloInfoAdicional_05;
        private SC_ControlsCS.scTextBoxExt txtInfoAdicional_05;
        private System.Windows.Forms.Label lblInfoAdicional_04;
        private System.Windows.Forms.Label lblITituloInfoAdicional_04;
        private SC_ControlsCS.scTextBoxExt txtInfoAdicional_04;
        private System.Windows.Forms.Label lblInfoAdicional_03;
        private System.Windows.Forms.Label lblITituloInfoAdicional_03;
        private SC_ControlsCS.scTextBoxExt txtInfoAdicional_03;
        private System.Windows.Forms.Label lblInfoAdicional_02;
        private System.Windows.Forms.Label lblITituloInfoAdicional_02;
        private SC_ControlsCS.scTextBoxExt txtInfoAdicional_02;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdFacturas;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdDocumentos;
        private FarPoint.Win.Spread.SheetView grdFacturas_Sheet1;
        private FarPoint.Win.Spread.SheetView grdDocumentos_Sheet1;
        private System.Windows.Forms.TabPage tabPage_02_ProgramasSubPrograma;
        private System.Windows.Forms.TabPage tabPage_03_Claves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLimpiarClaveExclusiva;
        private System.Windows.Forms.Button btnAgregarClaveExclusiva;
        private System.Windows.Forms.Label lblClaveExclusiva;
        private System.Windows.Forms.Label label39;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA_Exclusiva;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnLimpiarClaveExcluida;
        private System.Windows.Forms.Button btnAgregarClaveExcluida;
        private System.Windows.Forms.Label lblClaveExcluida;
        private System.Windows.Forms.Label label33;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA_Excluida;
        private System.Windows.Forms.GroupBox Frame_21_ProgramasSubProgramas;
        private System.Windows.Forms.Button btnLimpiarPrograma;
        private System.Windows.Forms.Button btnAgregarPrograma;
        private FarPoint.Win.Spread.FpSpread grdProgramasSubProgramas;
        private FarPoint.Win.Spread.SheetView grdProgramasSubProgramas_Sheet1;
        private System.Windows.Forms.Label lblSubPrograma;
        private System.Windows.Forms.Label lblPrograma;
        private SC_ControlsCS.scTextBoxExt txtIdSubPrograma;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private SC_ControlsCS.scTextBoxExt txtIdPrograma;
        private FarPoint.Win.Spread.FpSpread grdClavesExclusivas;
        private FarPoint.Win.Spread.SheetView grdClavesExclusivas_Sheet1;
        private FarPoint.Win.Spread.FpSpread grdClavesExcluidas;
        private FarPoint.Win.Spread.SheetView grdClavesExcluidas_Sheet1;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}