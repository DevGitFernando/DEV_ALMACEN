namespace Facturacion.Facturar
{
    partial class FrmFacturar_Concentrado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturar_Concentrado));
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.FrameAsociacionRemisiones = new System.Windows.Forms.GroupBox();
            this.rdoBaseRemision_AsociadaFactura = new System.Windows.Forms.RadioButton();
            this.rdoBaseRemision_Normal = new System.Windows.Forms.RadioButton();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFiltro_Folios = new System.Windows.Forms.CheckBox();
            this.FrameTipoRemision = new System.Windows.Forms.GroupBox();
            this.chkRM_Complemento = new System.Windows.Forms.CheckBox();
            this.rdoRM_Servicio = new System.Windows.Forms.RadioButton();
            this.rdoRM_Producto = new System.Windows.Forms.RadioButton();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.rdoOIN_Consignacion = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Venta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoInsumo_Ambos = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.chkFiltro_FechaRemisionado = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaRemision_Final = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaRemision_Inicial = new System.Windows.Forms.DateTimePicker();
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.lblVigencia = new SC_ControlsCS.scLabelExt();
            this.lblNumeroDeLicitacion = new SC_ControlsCS.scLabelExt();
            this.lblNumeroDeContrato = new SC_ControlsCS.scLabelExt();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.lblFuentesFinanciamiento = new SC_ControlsCS.scLabelExt();
            this.lblFinanciamiento = new SC_ControlsCS.scLabelExt();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.txtFuenteDeFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblTotal_Seleccionado = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.lblRemisiones_Seleccionadas = new SC_ControlsCS.scLabelExt();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblArchivo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboHojas = new System.Windows.Forms.ToolStripComboBox();
            this.btnLeerHoja = new System.Windows.Forms.ToolStripButton();
            this.lblNumeroDeRemisiones = new System.Windows.Forms.ToolStripLabel();
            this.chkMarcarDesmarcarTodo = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdRemisiones = new FarPoint.Win.Spread.FpSpread();
            this.grdRemisiones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.FrameFechaDePeriodoRemisionado = new System.Windows.Forms.GroupBox();
            this.chkFiltro_PeriodoRemisionado = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaPeriodoRemisionado_Final = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaPeriodoRemisionado_Inicial = new System.Windows.Forms.DateTimePicker();
            this.FrameTipoDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTipoDispensacion_02_vale = new System.Windows.Forms.RadioButton();
            this.rdoTipoDispensacion_01_Dispensacion = new System.Windows.Forms.RadioButton();
            this.Referencias = new System.Windows.Forms.GroupBox();
            this.nmPartidaGeneral = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cboReferencias_02 = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.cboReferencias_01 = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.FrameAsociacionRemisiones.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.FrameTipoRemision.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).BeginInit();
            this.FrameFechaDePeriodoRemisionado.SuspendLayout();
            this.FrameTipoDispensacion.SuspendLayout();
            this.Referencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmPartidaGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameAsociacionRemisiones
            // 
            this.FrameAsociacionRemisiones.Controls.Add(this.rdoBaseRemision_AsociadaFactura);
            this.FrameAsociacionRemisiones.Controls.Add(this.rdoBaseRemision_Normal);
            this.FrameAsociacionRemisiones.Location = new System.Drawing.Point(561, 128);
            this.FrameAsociacionRemisiones.Margin = new System.Windows.Forms.Padding(2);
            this.FrameAsociacionRemisiones.Name = "FrameAsociacionRemisiones";
            this.FrameAsociacionRemisiones.Padding = new System.Windows.Forms.Padding(2);
            this.FrameAsociacionRemisiones.Size = new System.Drawing.Size(161, 92);
            this.FrameAsociacionRemisiones.TabIndex = 6;
            this.FrameAsociacionRemisiones.TabStop = false;
            this.FrameAsociacionRemisiones.Text = "Base remisión";
            // 
            // rdoBaseRemision_AsociadaFactura
            // 
            this.rdoBaseRemision_AsociadaFactura.Location = new System.Drawing.Point(18, 44);
            this.rdoBaseRemision_AsociadaFactura.Name = "rdoBaseRemision_AsociadaFactura";
            this.rdoBaseRemision_AsociadaFactura.Size = new System.Drawing.Size(128, 17);
            this.rdoBaseRemision_AsociadaFactura.TabIndex = 1;
            this.rdoBaseRemision_AsociadaFactura.TabStop = true;
            this.rdoBaseRemision_AsociadaFactura.Text = "Asociada a facturas";
            this.rdoBaseRemision_AsociadaFactura.UseVisualStyleBackColor = true;
            // 
            // rdoBaseRemision_Normal
            // 
            this.rdoBaseRemision_Normal.Location = new System.Drawing.Point(18, 23);
            this.rdoBaseRemision_Normal.Name = "rdoBaseRemision_Normal";
            this.rdoBaseRemision_Normal.Size = new System.Drawing.Size(128, 17);
            this.rdoBaseRemision_Normal.TabIndex = 0;
            this.rdoBaseRemision_Normal.TabStop = true;
            this.rdoBaseRemision_Normal.Text = "Normal";
            this.rdoBaseRemision_Normal.UseVisualStyleBackColor = true;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.txtFolioFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFolioInicial);
            this.FrameFolios.Controls.Add(this.label4);
            this.FrameFolios.Controls.Add(this.chkFiltro_Folios);
            this.FrameFolios.Location = new System.Drawing.Point(727, 128);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(173, 92);
            this.FrameFolios.TabIndex = 7;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(56, 45);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(98, 20);
            this.txtFolioFinal.TabIndex = 1;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "Hasta :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicial
            // 
            this.txtFolioInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicial.Decimales = 2;
            this.txtFolioInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicial.Location = new System.Drawing.Point(56, 19);
            this.txtFolioInicial.MaxLength = 8;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(98, 20);
            this.txtFolioInicial.TabIndex = 0;
            this.txtFolioInicial.Text = "01234567";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Desde :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFiltro_Folios
            // 
            this.chkFiltro_Folios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_Folios.Location = new System.Drawing.Point(54, 69);
            this.chkFiltro_Folios.Name = "chkFiltro_Folios";
            this.chkFiltro_Folios.Size = new System.Drawing.Size(100, 18);
            this.chkFiltro_Folios.TabIndex = 2;
            this.chkFiltro_Folios.Text = "Filtro por Folios";
            this.chkFiltro_Folios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_Folios.UseVisualStyleBackColor = true;
            // 
            // FrameTipoRemision
            // 
            this.FrameTipoRemision.Controls.Add(this.chkRM_Complemento);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Servicio);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Producto);
            this.FrameTipoRemision.Location = new System.Drawing.Point(12, 128);
            this.FrameTipoRemision.Name = "FrameTipoRemision";
            this.FrameTipoRemision.Size = new System.Drawing.Size(122, 92);
            this.FrameTipoRemision.TabIndex = 2;
            this.FrameTipoRemision.TabStop = false;
            this.FrameTipoRemision.Text = "Tipo de remisión";
            // 
            // chkRM_Complemento
            // 
            this.chkRM_Complemento.Location = new System.Drawing.Point(16, 67);
            this.chkRM_Complemento.Name = "chkRM_Complemento";
            this.chkRM_Complemento.Size = new System.Drawing.Size(91, 17);
            this.chkRM_Complemento.TabIndex = 2;
            this.chkRM_Complemento.Text = "Complemento";
            this.chkRM_Complemento.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Servicio
            // 
            this.rdoRM_Servicio.Location = new System.Drawing.Point(16, 44);
            this.rdoRM_Servicio.Name = "rdoRM_Servicio";
            this.rdoRM_Servicio.Size = new System.Drawing.Size(78, 17);
            this.rdoRM_Servicio.TabIndex = 1;
            this.rdoRM_Servicio.TabStop = true;
            this.rdoRM_Servicio.Text = "Servicio";
            this.rdoRM_Servicio.UseVisualStyleBackColor = true;
            this.rdoRM_Servicio.CheckedChanged += new System.EventHandler(this.rdoRM_Servicio_CheckedChanged);
            // 
            // rdoRM_Producto
            // 
            this.rdoRM_Producto.Location = new System.Drawing.Point(16, 23);
            this.rdoRM_Producto.Name = "rdoRM_Producto";
            this.rdoRM_Producto.Size = new System.Drawing.Size(78, 17);
            this.rdoRM_Producto.TabIndex = 0;
            this.rdoRM_Producto.TabStop = true;
            this.rdoRM_Producto.Text = "Producto";
            this.rdoRM_Producto.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Consignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Venta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(138, 128);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(125, 92);
            this.FrameOrigenInsumo.TabIndex = 3;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Origen de Insumos";
            // 
            // rdoOIN_Consignacion
            // 
            this.rdoOIN_Consignacion.Location = new System.Drawing.Point(16, 44);
            this.rdoOIN_Consignacion.Name = "rdoOIN_Consignacion";
            this.rdoOIN_Consignacion.Size = new System.Drawing.Size(92, 17);
            this.rdoOIN_Consignacion.TabIndex = 1;
            this.rdoOIN_Consignacion.TabStop = true;
            this.rdoOIN_Consignacion.Text = "Consignación";
            this.rdoOIN_Consignacion.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Venta
            // 
            this.rdoOIN_Venta.Location = new System.Drawing.Point(16, 22);
            this.rdoOIN_Venta.Name = "rdoOIN_Venta";
            this.rdoOIN_Venta.Size = new System.Drawing.Size(92, 18);
            this.rdoOIN_Venta.TabIndex = 0;
            this.rdoOIN_Venta.TabStop = true;
            this.rdoOIN_Venta.Text = "Venta";
            this.rdoOIN_Venta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumo_Ambos);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(267, 128);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(153, 92);
            this.FrameTipoInsumo.TabIndex = 4;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoInsumo_Ambos
            // 
            this.rdoInsumo_Ambos.Location = new System.Drawing.Point(15, 67);
            this.rdoInsumo_Ambos.Name = "rdoInsumo_Ambos";
            this.rdoInsumo_Ambos.Size = new System.Drawing.Size(123, 17);
            this.rdoInsumo_Ambos.TabIndex = 2;
            this.rdoInsumo_Ambos.TabStop = true;
            this.rdoInsumo_Ambos.Text = "Ambos";
            this.rdoInsumo_Ambos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMaterialDeCuracion
            // 
            this.rdoInsumoMaterialDeCuracion.Location = new System.Drawing.Point(15, 44);
            this.rdoInsumoMaterialDeCuracion.Name = "rdoInsumoMaterialDeCuracion";
            this.rdoInsumoMaterialDeCuracion.Size = new System.Drawing.Size(123, 17);
            this.rdoInsumoMaterialDeCuracion.TabIndex = 1;
            this.rdoInsumoMaterialDeCuracion.TabStop = true;
            this.rdoInsumoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoInsumoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamento
            // 
            this.rdoInsumoMedicamento.Location = new System.Drawing.Point(15, 23);
            this.rdoInsumoMedicamento.Name = "rdoInsumoMedicamento";
            this.rdoInsumoMedicamento.Size = new System.Drawing.Size(123, 17);
            this.rdoInsumoMedicamento.TabIndex = 0;
            this.rdoInsumoMedicamento.TabStop = true;
            this.rdoInsumoMedicamento.Text = "Medicamento";
            this.rdoInsumoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.chkFiltro_FechaRemisionado);
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaRemision_Final);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaRemision_Inicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(1067, 128);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(159, 92);
            this.FrameFechaDeProceso.TabIndex = 9;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Emisión de remisiones";
            // 
            // chkFiltro_FechaRemisionado
            // 
            this.chkFiltro_FechaRemisionado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_FechaRemisionado.Location = new System.Drawing.Point(35, 70);
            this.chkFiltro_FechaRemisionado.Name = "chkFiltro_FechaRemisionado";
            this.chkFiltro_FechaRemisionado.Size = new System.Drawing.Size(104, 18);
            this.chkFiltro_FechaRemisionado.TabIndex = 2;
            this.chkFiltro_FechaRemisionado.Text = "Filtro de Fechas";
            this.chkFiltro_FechaRemisionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_FechaRemisionado.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(8, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Desde :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRemision_Final
            // 
            this.dtpFechaRemision_Final.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRemision_Final.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRemision_Final.Location = new System.Drawing.Point(63, 45);
            this.dtpFechaRemision_Final.Name = "dtpFechaRemision_Final";
            this.dtpFechaRemision_Final.Size = new System.Drawing.Size(76, 20);
            this.dtpFechaRemision_Final.TabIndex = 1;
            // 
            // dtpFechaRemision_Inicial
            // 
            this.dtpFechaRemision_Inicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRemision_Inicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRemision_Inicial.Location = new System.Drawing.Point(63, 19);
            this.dtpFechaRemision_Inicial.Name = "dtpFechaRemision_Inicial";
            this.dtpFechaRemision_Inicial.Size = new System.Drawing.Size(76, 20);
            this.dtpFechaRemision_Inicial.TabIndex = 0;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.lblVigencia);
            this.FrameDatosOperacion.Controls.Add(this.lblNumeroDeLicitacion);
            this.FrameDatosOperacion.Controls.Add(this.lblNumeroDeContrato);
            this.FrameDatosOperacion.Controls.Add(this.lblCliente);
            this.FrameDatosOperacion.Controls.Add(this.lblFuentesFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label18);
            this.FrameDatosOperacion.Controls.Add(this.label13);
            this.FrameDatosOperacion.Controls.Add(this.label16);
            this.FrameDatosOperacion.Controls.Add(this.label1);
            this.FrameDatosOperacion.Controls.Add(this.txtFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.txtFuenteDeFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label11);
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(12, 30);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(1214, 96);
            this.FrameDatosOperacion.TabIndex = 1;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // lblVigencia
            // 
            this.lblVigencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVigencia.Location = new System.Drawing.Point(829, 64);
            this.lblVigencia.MostrarToolTip = false;
            this.lblVigencia.Name = "lblVigencia";
            this.lblVigencia.Size = new System.Drawing.Size(371, 21);
            this.lblVigencia.TabIndex = 8;
            this.lblVigencia.Text = "Serie : ";
            this.lblVigencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNumeroDeLicitacion
            // 
            this.lblNumeroDeLicitacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumeroDeLicitacion.Location = new System.Drawing.Point(829, 39);
            this.lblNumeroDeLicitacion.MostrarToolTip = false;
            this.lblNumeroDeLicitacion.Name = "lblNumeroDeLicitacion";
            this.lblNumeroDeLicitacion.Size = new System.Drawing.Size(371, 21);
            this.lblNumeroDeLicitacion.TabIndex = 7;
            this.lblNumeroDeLicitacion.Text = "Serie : ";
            this.lblNumeroDeLicitacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNumeroDeContrato
            // 
            this.lblNumeroDeContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumeroDeContrato.Location = new System.Drawing.Point(829, 16);
            this.lblNumeroDeContrato.MostrarToolTip = false;
            this.lblNumeroDeContrato.Name = "lblNumeroDeContrato";
            this.lblNumeroDeContrato.Size = new System.Drawing.Size(371, 21);
            this.lblNumeroDeContrato.TabIndex = 6;
            this.lblNumeroDeContrato.Text = "Serie : ";
            this.lblNumeroDeContrato.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(153, 64);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(532, 21);
            this.lblCliente.TabIndex = 4;
            this.lblCliente.Text = "Serie : ";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFuentesFinanciamiento
            // 
            this.lblFuentesFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuentesFinanciamiento.Location = new System.Drawing.Point(239, 39);
            this.lblFuentesFinanciamiento.MostrarToolTip = false;
            this.lblFuentesFinanciamiento.Name = "lblFuentesFinanciamiento";
            this.lblFuentesFinanciamiento.Size = new System.Drawing.Size(446, 21);
            this.lblFuentesFinanciamiento.TabIndex = 3;
            this.lblFuentesFinanciamiento.Text = "Serie : ";
            this.lblFuentesFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinanciamiento
            // 
            this.lblFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinanciamiento.Location = new System.Drawing.Point(239, 16);
            this.lblFinanciamiento.MostrarToolTip = false;
            this.lblFinanciamiento.Name = "lblFinanciamiento";
            this.lblFinanciamiento.Size = new System.Drawing.Size(446, 21);
            this.lblFinanciamiento.TabIndex = 1;
            this.lblFinanciamiento.Text = "Serie : ";
            this.lblFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(691, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(132, 16);
            this.label18.TabIndex = 57;
            this.label18.Text = "Vigencia :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(691, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 16);
            this.label13.TabIndex = 53;
            this.label13.Text = "Número de Contrato :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(691, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(132, 16);
            this.label16.TabIndex = 55;
            this.label16.Text = "Número de Licitación :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 49;
            this.label1.Text = "Fuente de financiamiento :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFinanciamiento
            // 
            this.txtFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFinanciamiento.Decimales = 2;
            this.txtFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtFinanciamiento.Location = new System.Drawing.Point(153, 16);
            this.txtFinanciamiento.MaxLength = 4;
            this.txtFinanciamiento.Name = "txtFinanciamiento";
            this.txtFinanciamiento.PermitirApostrofo = false;
            this.txtFinanciamiento.PermitirNegativos = false;
            this.txtFinanciamiento.Size = new System.Drawing.Size(80, 20);
            this.txtFinanciamiento.TabIndex = 0;
            this.txtFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFinanciamiento.TextChanged += new System.EventHandler(this.txtFinanciamiento_TextChanged);
            this.txtFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFinanciamiento_KeyDown);
            this.txtFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtFinanciamiento_Validating);
            // 
            // txtFuenteDeFinanciamiento
            // 
            this.txtFuenteDeFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFuenteDeFinanciamiento.Decimales = 2;
            this.txtFuenteDeFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFuenteDeFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtFuenteDeFinanciamiento.Location = new System.Drawing.Point(153, 39);
            this.txtFuenteDeFinanciamiento.MaxLength = 4;
            this.txtFuenteDeFinanciamiento.Name = "txtFuenteDeFinanciamiento";
            this.txtFuenteDeFinanciamiento.PermitirApostrofo = false;
            this.txtFuenteDeFinanciamiento.PermitirNegativos = false;
            this.txtFuenteDeFinanciamiento.Size = new System.Drawing.Size(80, 20);
            this.txtFuenteDeFinanciamiento.TabIndex = 2;
            this.txtFuenteDeFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFuenteDeFinanciamiento.TextChanged += new System.EventHandler(this.txtFuenteDeFinanciamiento_TextChanged);
            this.txtFuenteDeFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFuenteDeFinanciamiento_KeyDown);
            this.txtFuenteDeFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtFuenteDeFinanciamiento_Validating);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(11, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 16);
            this.label11.TabIndex = 51;
            this.label11.Text = "Segmento financiamiento :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1234, 25);
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
            this.btnEjecutar.Text = "Consultar remisiones sin facturar";
            this.btnEjecutar.ToolTipText = "Consultar remisiones sin facturar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar facturas electrónicas";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.scLabelExt2);
            this.FrameDetalles.Controls.Add(this.lblTotal_Seleccionado);
            this.FrameDetalles.Controls.Add(this.scLabelExt1);
            this.FrameDetalles.Controls.Add(this.lblRemisiones_Seleccionadas);
            this.FrameDetalles.Controls.Add(this.toolStrip1);
            this.FrameDetalles.Controls.Add(this.chkMarcarDesmarcarTodo);
            this.FrameDetalles.Controls.Add(this.FrameProceso);
            this.FrameDetalles.Controls.Add(this.grdRemisiones);
            this.FrameDetalles.Controls.Add(this.scLabelExt4);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 275);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1214, 354);
            this.FrameDetalles.TabIndex = 11;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Remisiones";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt2.Location = new System.Drawing.Point(359, 319);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(209, 22);
            this.scLabelExt2.TabIndex = 27;
            this.scLabelExt2.Text = "Importe remisiones seleccionadas :";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal_Seleccionado
            // 
            this.lblTotal_Seleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal_Seleccionado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal_Seleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_Seleccionado.Location = new System.Drawing.Point(570, 319);
            this.lblTotal_Seleccionado.MostrarToolTip = false;
            this.lblTotal_Seleccionado.Name = "lblTotal_Seleccionado";
            this.lblTotal_Seleccionado.Size = new System.Drawing.Size(129, 22);
            this.lblTotal_Seleccionado.TabIndex = 26;
            this.lblTotal_Seleccionado.Text = "scLabelExt3";
            this.lblTotal_Seleccionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt1.Location = new System.Drawing.Point(10, 319);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(167, 22);
            this.scLabelExt1.TabIndex = 25;
            this.scLabelExt1.Text = "Remisiones seleccionadas :";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemisiones_Seleccionadas
            // 
            this.lblRemisiones_Seleccionadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemisiones_Seleccionadas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRemisiones_Seleccionadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemisiones_Seleccionadas.Location = new System.Drawing.Point(180, 319);
            this.lblRemisiones_Seleccionadas.MostrarToolTip = false;
            this.lblRemisiones_Seleccionadas.Name = "lblRemisiones_Seleccionadas";
            this.lblRemisiones_Seleccionadas.Size = new System.Drawing.Size(129, 22);
            this.lblRemisiones_Seleccionadas.TabIndex = 24;
            this.lblRemisiones_Seleccionadas.Text = "scLabelExt3";
            this.lblRemisiones_Seleccionadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoExcel,
            this.toolStripSeparator4,
            this.btnAbrirExcel,
            this.toolStripSeparator5,
            this.lblArchivo,
            this.toolStripSeparator6,
            this.toolStripLabel1,
            this.cboHojas,
            this.btnLeerHoja,
            this.lblNumeroDeRemisiones});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1208, 25);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoExcel
            // 
            this.btnNuevoExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoExcel.Image")));
            this.btnNuevoExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoExcel.Name = "btnNuevoExcel";
            this.btnNuevoExcel.Size = new System.Drawing.Size(23, 22);
            this.btnNuevoExcel.Text = "Nuevo excel";
            this.btnNuevoExcel.Click += new System.EventHandler(this.btnNuevoExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirExcel.Image")));
            this.btnAbrirExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirExcel.Text = "&Abrir";
            this.btnAbrirExcel.Click += new System.EventHandler(this.btnAbrirExcel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // lblArchivo
            // 
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(16, 22);
            this.lblArchivo.Text = "...";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel1.Text = "Hoja";
            // 
            // cboHojas
            // 
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(121, 25);
            // 
            // btnLeerHoja
            // 
            this.btnLeerHoja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerHoja.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerHoja.Image")));
            this.btnLeerHoja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerHoja.Name = "btnLeerHoja";
            this.btnLeerHoja.Size = new System.Drawing.Size(23, 22);
            this.btnLeerHoja.Text = "Leer hoja";
            this.btnLeerHoja.ToolTipText = "Leer hoja";
            this.btnLeerHoja.Click += new System.EventHandler(this.btnLeerHoja_Click);
            // 
            // lblNumeroDeRemisiones
            // 
            this.lblNumeroDeRemisiones.Name = "lblNumeroDeRemisiones";
            this.lblNumeroDeRemisiones.Size = new System.Drawing.Size(16, 22);
            this.lblNumeroDeRemisiones.Text = "...";
            // 
            // chkMarcarDesmarcarTodo
            // 
            this.chkMarcarDesmarcarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.Location = new System.Drawing.Point(1019, 0);
            this.chkMarcarDesmarcarTodo.Name = "chkMarcarDesmarcarTodo";
            this.chkMarcarDesmarcarTodo.Size = new System.Drawing.Size(181, 17);
            this.chkMarcarDesmarcarTodo.TabIndex = 1;
            this.chkMarcarDesmarcarTodo.Text = "Marcar - Desmarcar todo";
            this.chkMarcarDesmarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcarTodo_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(230, 170);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 50);
            this.FrameProceso.TabIndex = 2;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 13);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdRemisiones
            // 
            this.grdRemisiones.AccessibleDescription = "grdRemisiones, Sheet1, Row 0, Column 0, ";
            this.grdRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRemisiones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdRemisiones.Location = new System.Drawing.Point(10, 44);
            this.grdRemisiones.Name = "grdRemisiones";
            this.grdRemisiones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdRemisiones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdRemisiones_Sheet1});
            this.grdRemisiones.Size = new System.Drawing.Size(1196, 269);
            this.grdRemisiones.TabIndex = 0;
            this.grdRemisiones.EditModeOff += new System.EventHandler(this.grdRemisiones_EditModeOff);
            this.grdRemisiones.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdRemisiones_ButtonClicked);
            // 
            // grdRemisiones_Sheet1
            // 
            this.grdRemisiones_Sheet1.Reset();
            this.grdRemisiones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdRemisiones_Sheet1.ColumnCount = 10;
            this.grdRemisiones_Sheet1.RowCount = 14;
            this.grdRemisiones_Sheet1.Cells.Get(0, 3).Value = "2018-09-01";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha de remisionado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Importe remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha inicial procesado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha final procesado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Farmacia";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Tipo de remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Referencia 01";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Referencia 02";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Facturar";
            this.grdRemisiones_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdRemisiones_Sheet1.Columns.Get(0).CellType = textCellType17;
            this.grdRemisiones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Label = "Remisión";
            this.grdRemisiones_Sheet1.Columns.Get(0).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Width = 105F;
            this.grdRemisiones_Sheet1.Columns.Get(1).CellType = textCellType18;
            this.grdRemisiones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(1).Label = "Fecha de remisionado";
            this.grdRemisiones_Sheet1.Columns.Get(1).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(1).Width = 80F;
            numberCellType3.DecimalPlaces = 2;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 999999999999.99D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdRemisiones_Sheet1.Columns.Get(2).CellType = numberCellType3;
            this.grdRemisiones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRemisiones_Sheet1.Columns.Get(2).Label = "Importe remisión";
            this.grdRemisiones_Sheet1.Columns.Get(2).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(2).Width = 120F;
            this.grdRemisiones_Sheet1.Columns.Get(3).CellType = textCellType19;
            this.grdRemisiones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(3).Label = "Fecha inicial procesado";
            this.grdRemisiones_Sheet1.Columns.Get(3).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(3).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(4).CellType = textCellType20;
            this.grdRemisiones_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(4).Label = "Fecha final procesado";
            this.grdRemisiones_Sheet1.Columns.Get(4).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(4).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(5).CellType = textCellType21;
            this.grdRemisiones_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(5).Label = "Farmacia";
            this.grdRemisiones_Sheet1.Columns.Get(5).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(5).Width = 270F;
            this.grdRemisiones_Sheet1.Columns.Get(6).CellType = textCellType22;
            this.grdRemisiones_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(6).Label = "Tipo de remisión";
            this.grdRemisiones_Sheet1.Columns.Get(6).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(6).Width = 160F;
            this.grdRemisiones_Sheet1.Columns.Get(7).CellType = textCellType23;
            this.grdRemisiones_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(7).Label = "Referencia 01";
            this.grdRemisiones_Sheet1.Columns.Get(7).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(7).Width = 70F;
            this.grdRemisiones_Sheet1.Columns.Get(8).CellType = textCellType24;
            this.grdRemisiones_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(8).Label = "Referencia 02";
            this.grdRemisiones_Sheet1.Columns.Get(8).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(8).Width = 70F;
            this.grdRemisiones_Sheet1.Columns.Get(9).CellType = checkBoxCellType3;
            this.grdRemisiones_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(9).Label = "Facturar";
            this.grdRemisiones_Sheet1.Columns.Get(9).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(9).Width = 65F;
            this.grdRemisiones_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdRemisiones_Sheet1.Rows.Default.Height = 25F;
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt4.Location = new System.Drawing.Point(926, 319);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt4.TabIndex = 22;
            this.scLabelExt4.Text = "Total :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1069, 319);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechaDePeriodoRemisionado
            // 
            this.FrameFechaDePeriodoRemisionado.Controls.Add(this.chkFiltro_PeriodoRemisionado);
            this.FrameFechaDePeriodoRemisionado.Controls.Add(this.label2);
            this.FrameFechaDePeriodoRemisionado.Controls.Add(this.label7);
            this.FrameFechaDePeriodoRemisionado.Controls.Add(this.dtpFechaPeriodoRemisionado_Final);
            this.FrameFechaDePeriodoRemisionado.Controls.Add(this.dtpFechaPeriodoRemisionado_Inicial);
            this.FrameFechaDePeriodoRemisionado.Location = new System.Drawing.Point(906, 128);
            this.FrameFechaDePeriodoRemisionado.Name = "FrameFechaDePeriodoRemisionado";
            this.FrameFechaDePeriodoRemisionado.Size = new System.Drawing.Size(157, 92);
            this.FrameFechaDePeriodoRemisionado.TabIndex = 8;
            this.FrameFechaDePeriodoRemisionado.TabStop = false;
            this.FrameFechaDePeriodoRemisionado.Text = "Periodo de dispensación";
            // 
            // chkFiltro_PeriodoRemisionado
            // 
            this.chkFiltro_PeriodoRemisionado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_PeriodoRemisionado.Location = new System.Drawing.Point(35, 70);
            this.chkFiltro_PeriodoRemisionado.Name = "chkFiltro_PeriodoRemisionado";
            this.chkFiltro_PeriodoRemisionado.Size = new System.Drawing.Size(104, 18);
            this.chkFiltro_PeriodoRemisionado.TabIndex = 2;
            this.chkFiltro_PeriodoRemisionado.Text = "Filtro de Fechas";
            this.chkFiltro_PeriodoRemisionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFiltro_PeriodoRemisionado.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Desde :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaPeriodoRemisionado_Final
            // 
            this.dtpFechaPeriodoRemisionado_Final.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaPeriodoRemisionado_Final.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPeriodoRemisionado_Final.Location = new System.Drawing.Point(63, 45);
            this.dtpFechaPeriodoRemisionado_Final.Name = "dtpFechaPeriodoRemisionado_Final";
            this.dtpFechaPeriodoRemisionado_Final.Size = new System.Drawing.Size(76, 20);
            this.dtpFechaPeriodoRemisionado_Final.TabIndex = 1;
            // 
            // dtpFechaPeriodoRemisionado_Inicial
            // 
            this.dtpFechaPeriodoRemisionado_Inicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaPeriodoRemisionado_Inicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaPeriodoRemisionado_Inicial.Location = new System.Drawing.Point(63, 19);
            this.dtpFechaPeriodoRemisionado_Inicial.Name = "dtpFechaPeriodoRemisionado_Inicial";
            this.dtpFechaPeriodoRemisionado_Inicial.Size = new System.Drawing.Size(76, 20);
            this.dtpFechaPeriodoRemisionado_Inicial.TabIndex = 0;
            // 
            // FrameTipoDispensacion
            // 
            this.FrameTipoDispensacion.Controls.Add(this.rdoTipoDispensacion_02_vale);
            this.FrameTipoDispensacion.Controls.Add(this.rdoTipoDispensacion_01_Dispensacion);
            this.FrameTipoDispensacion.Location = new System.Drawing.Point(424, 128);
            this.FrameTipoDispensacion.Name = "FrameTipoDispensacion";
            this.FrameTipoDispensacion.Size = new System.Drawing.Size(133, 92);
            this.FrameTipoDispensacion.TabIndex = 5;
            this.FrameTipoDispensacion.TabStop = false;
            this.FrameTipoDispensacion.Text = "Tipo de dispensación";
            // 
            // rdoTipoDispensacion_02_vale
            // 
            this.rdoTipoDispensacion_02_vale.Location = new System.Drawing.Point(16, 44);
            this.rdoTipoDispensacion_02_vale.Name = "rdoTipoDispensacion_02_vale";
            this.rdoTipoDispensacion_02_vale.Size = new System.Drawing.Size(98, 17);
            this.rdoTipoDispensacion_02_vale.TabIndex = 1;
            this.rdoTipoDispensacion_02_vale.TabStop = true;
            this.rdoTipoDispensacion_02_vale.Text = "Vale";
            this.rdoTipoDispensacion_02_vale.UseVisualStyleBackColor = true;
            // 
            // rdoTipoDispensacion_01_Dispensacion
            // 
            this.rdoTipoDispensacion_01_Dispensacion.Location = new System.Drawing.Point(16, 23);
            this.rdoTipoDispensacion_01_Dispensacion.Name = "rdoTipoDispensacion_01_Dispensacion";
            this.rdoTipoDispensacion_01_Dispensacion.Size = new System.Drawing.Size(98, 17);
            this.rdoTipoDispensacion_01_Dispensacion.TabIndex = 0;
            this.rdoTipoDispensacion_01_Dispensacion.TabStop = true;
            this.rdoTipoDispensacion_01_Dispensacion.Text = "Dispensación";
            this.rdoTipoDispensacion_01_Dispensacion.UseVisualStyleBackColor = true;
            // 
            // Referencias
            // 
            this.Referencias.Controls.Add(this.nmPartidaGeneral);
            this.Referencias.Controls.Add(this.label9);
            this.Referencias.Controls.Add(this.cboReferencias_02);
            this.Referencias.Controls.Add(this.label8);
            this.Referencias.Controls.Add(this.cboReferencias_01);
            this.Referencias.Controls.Add(this.label5);
            this.Referencias.Location = new System.Drawing.Point(12, 222);
            this.Referencias.Name = "Referencias";
            this.Referencias.Size = new System.Drawing.Size(1214, 50);
            this.Referencias.TabIndex = 10;
            this.Referencias.TabStop = false;
            this.Referencias.Text = "Referencias";
            // 
            // nmPartidaGeneral
            // 
            this.nmPartidaGeneral.Location = new System.Drawing.Point(1071, 18);
            this.nmPartidaGeneral.Name = "nmPartidaGeneral";
            this.nmPartidaGeneral.Size = new System.Drawing.Size(102, 20);
            this.nmPartidaGeneral.TabIndex = 3;
            this.nmPartidaGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmPartidaGeneral.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(970, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 15);
            this.label9.TabIndex = 48;
            this.label9.Text = "Partida general  :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboReferencias_02
            // 
            this.cboReferencias_02.BackColorEnabled = System.Drawing.Color.White;
            this.cboReferencias_02.Data = "";
            this.cboReferencias_02.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReferencias_02.Filtro = " 1 = 1";
            this.cboReferencias_02.FormattingEnabled = true;
            this.cboReferencias_02.ListaItemsBusqueda = 20;
            this.cboReferencias_02.Location = new System.Drawing.Point(606, 18);
            this.cboReferencias_02.MostrarToolTip = false;
            this.cboReferencias_02.Name = "cboReferencias_02";
            this.cboReferencias_02.Size = new System.Drawing.Size(310, 21);
            this.cboReferencias_02.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(511, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 15);
            this.label8.TabIndex = 47;
            this.label8.Text = "Referencia 02 :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboReferencias_01
            // 
            this.cboReferencias_01.BackColorEnabled = System.Drawing.Color.White;
            this.cboReferencias_01.Data = "";
            this.cboReferencias_01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReferencias_01.Filtro = " 1 = 1";
            this.cboReferencias_01.FormattingEnabled = true;
            this.cboReferencias_01.ListaItemsBusqueda = 20;
            this.cboReferencias_01.Location = new System.Drawing.Point(136, 19);
            this.cboReferencias_01.MostrarToolTip = false;
            this.cboReferencias_01.Name = "cboReferencias_01";
            this.cboReferencias_01.Size = new System.Drawing.Size(310, 21);
            this.cboReferencias_01.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 15);
            this.label5.TabIndex = 45;
            this.label5.Text = "Referencia 01 :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmFacturar_Concentrado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 637);
            this.Controls.Add(this.Referencias);
            this.Controls.Add(this.FrameTipoDispensacion);
            this.Controls.Add(this.FrameFechaDePeriodoRemisionado);
            this.Controls.Add(this.FrameAsociacionRemisiones);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoRemision);
            this.Controls.Add(this.FrameOrigenInsumo);
            this.Controls.Add(this.FrameTipoInsumo);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalles);
            this.Name = "FrmFacturar_Concentrado";
            this.Text = "Generar factura concentrada";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFacturar_Concentrado_Load);
            this.FrameAsociacionRemisiones.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.FrameTipoRemision.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameDetalles.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).EndInit();
            this.FrameFechaDePeriodoRemisionado.ResumeLayout(false);
            this.FrameTipoDispensacion.ResumeLayout(false);
            this.Referencias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmPartidaGeneral)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameAsociacionRemisiones;
        private System.Windows.Forms.RadioButton rdoBaseRemision_AsociadaFactura;
        private System.Windows.Forms.RadioButton rdoBaseRemision_Normal;
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFiltro_Folios;
        private System.Windows.Forms.GroupBox FrameTipoRemision;
        private System.Windows.Forms.RadioButton rdoRM_Servicio;
        private System.Windows.Forms.RadioButton rdoRM_Producto;
        private System.Windows.Forms.GroupBox FrameOrigenInsumo;
        private System.Windows.Forms.RadioButton rdoOIN_Consignacion;
        private System.Windows.Forms.RadioButton rdoOIN_Venta;
        private System.Windows.Forms.GroupBox FrameTipoInsumo;
        private System.Windows.Forms.RadioButton rdoInsumoMaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamento;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.CheckBox chkFiltro_FechaRemisionado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaRemision_Final;
        private System.Windows.Forms.DateTimePicker dtpFechaRemision_Inicial;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcarTodo;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdRemisiones;
        private FarPoint.Win.Spread.SheetView grdRemisiones_Sheet1;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt lblTotal;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtFinanciamiento;
        private SC_ControlsCS.scTextBoxExt txtFuenteDeFinanciamiento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox FrameFechaDePeriodoRemisionado;
        private System.Windows.Forms.CheckBox chkFiltro_PeriodoRemisionado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaPeriodoRemisionado_Final;
        private System.Windows.Forms.DateTimePicker dtpFechaPeriodoRemisionado_Inicial;
        private System.Windows.Forms.GroupBox FrameTipoDispensacion;
        private System.Windows.Forms.RadioButton rdoTipoDispensacion_02_vale;
        private System.Windows.Forms.RadioButton rdoTipoDispensacion_01_Dispensacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private SC_ControlsCS.scLabelExt lblFinanciamiento;
        private SC_ControlsCS.scLabelExt lblFuentesFinanciamiento;
        private SC_ControlsCS.scLabelExt lblCliente;
        private SC_ControlsCS.scLabelExt lblNumeroDeLicitacion;
        private SC_ControlsCS.scLabelExt lblNumeroDeContrato;
        private SC_ControlsCS.scLabelExt lblVigencia;
        private System.Windows.Forms.GroupBox Referencias;
        private SC_ControlsCS.scComboBoxExt cboReferencias_01;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboReferencias_02;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkRM_Complemento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nmPartidaGeneral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevoExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnAbrirExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lblArchivo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripComboBox cboHojas;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnLeerHoja;
        private System.Windows.Forms.ToolStripLabel lblNumeroDeRemisiones;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt lblRemisiones_Seleccionadas;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblTotal_Seleccionado;
        private System.Windows.Forms.RadioButton rdoInsumo_Ambos;
    }
}