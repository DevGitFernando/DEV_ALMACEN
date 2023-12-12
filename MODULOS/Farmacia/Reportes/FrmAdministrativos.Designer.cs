namespace Farmacia.Reportes
{
    partial class FrmAdministrativos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdministrativos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.grdReporte_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
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
            this.rdoInsumoMedicamentoNOSP = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamentoSP = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumosAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumosMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDispAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.FrameListaReportes = new System.Windows.Forms.GroupBox();
            this.chkMostrarLotes = new System.Windows.Forms.CheckBox();
            this.chkMostrarPrecios = new System.Windows.Forms.CheckBox();
            this.chkMostrarPaquetes = new System.Windows.Forms.CheckBox();
            this.cboReporte = new SC_ControlsCS.scComboBoxExt();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.FrameTituloReporteValidacion = new System.Windows.Forms.GroupBox();
            this.cboTitulosReporte = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostrarDevoluciones = new System.Windows.Forms.CheckBox();
            this.FrameOrigenDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoOrigenDispensacion_01__General = new System.Windows.Forms.RadioButton();
            this.rdoOrigenDispensacion_03_Vales = new System.Windows.Forms.RadioButton();
            this.rdoOrigenDispensacion_02_Dispensacion = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoOrdenamiento_01__ClaveSSA = new System.Windows.Forms.RadioButton();
            this.rdoOrdenamiento_02__DescripcionClaveSSA = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).BeginInit();
            this.FrameFechas.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameListaReportes.SuspendLayout();
            this.FrameTituloReporteValidacion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameOrigenDispensacion.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(692, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.grdReporte);
            this.FrameResultado.Location = new System.Drawing.Point(837, 75);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(988, 214);
            this.FrameResultado.TabIndex = 5;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Resultado de Consulta";
            // 
            // grdReporte
            // 
            this.grdReporte.AccessibleDescription = "grdReporte, Sheet1, Row 0, Column 0, ";
            this.grdReporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdReporte.Location = new System.Drawing.Point(10, 19);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdReporte.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdReporte_Sheet1});
            this.grdReporte.Size = new System.Drawing.Size(957, 179);
            this.grdReporte.TabIndex = 0;
            // 
            // grdReporte_Sheet1
            // 
            this.grdReporte_Sheet1.Reset();
            this.grdReporte_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdReporte_Sheet1.ColumnCount = 9;
            this.grdReporte_Sheet1.RowCount = 5;
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cliente";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Programa";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Receta";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Poliza";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Beneficiario";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Insumo";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Surtido";
            textCellType1.MaxLength = 500;
            this.grdReporte_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdReporte_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Label = "Folio";
            this.grdReporte_Sheet1.Columns.Get(0).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Label = "Fecha";
            this.grdReporte_Sheet1.Columns.Get(1).Width = 78F;
            textCellType2.MaxLength = 500;
            this.grdReporte_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.grdReporte_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(2).Label = "Cliente";
            this.grdReporte_Sheet1.Columns.Get(2).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Width = 180F;
            textCellType3.MaxLength = 500;
            this.grdReporte_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.grdReporte_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(3).Label = "Programa";
            this.grdReporte_Sheet1.Columns.Get(3).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).Width = 150F;
            this.grdReporte_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.grdReporte_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Label = "Receta";
            this.grdReporte_Sheet1.Columns.Get(4).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.grdReporte_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Label = "Poliza";
            this.grdReporte_Sheet1.Columns.Get(5).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Width = 108F;
            this.grdReporte_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.grdReporte_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(6).Label = "Beneficiario";
            this.grdReporte_Sheet1.Columns.Get(6).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(6).Width = 252F;
            textCellType7.MaxLength = 1000;
            this.grdReporte_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.grdReporte_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.grdReporte_Sheet1.Columns.Get(7).Label = "Insumo";
            this.grdReporte_Sheet1.Columns.Get(7).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(7).Width = 243F;
            numberCellType1.DecimalPlaces = 2;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            this.grdReporte_Sheet1.Columns.Get(8).CellType = numberCellType1;
            this.grdReporte_Sheet1.Columns.Get(8).Label = "Surtido";
            this.grdReporte_Sheet1.Columns.Get(8).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(8).Width = 77F;
            this.grdReporte_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(521, 154);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(160, 81);
            this.FrameFechas.TabIndex = 5;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(61, 48);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 25);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(61, 23);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.lblSubPro);
            this.FrameCliente.Controls.Add(this.txtSubPro);
            this.FrameCliente.Controls.Add(this.label7);
            this.FrameCliente.Controls.Add(this.lblPro);
            this.FrameCliente.Controls.Add(this.txtPro);
            this.FrameCliente.Controls.Add(this.label9);
            this.FrameCliente.Controls.Add(this.lblSubCte);
            this.FrameCliente.Controls.Add(this.txtSubCte);
            this.FrameCliente.Controls.Add(this.label1);
            this.FrameCliente.Controls.Add(this.lblCte);
            this.FrameCliente.Controls.Add(this.txtCte);
            this.FrameCliente.Controls.Add(this.label3);
            this.FrameCliente.Location = new System.Drawing.Point(11, 28);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(670, 125);
            this.FrameCliente.TabIndex = 1;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Parámetros de Cliente";
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(154, 94);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(504, 21);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(89, 94);
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
            this.label7.Location = new System.Drawing.Point(7, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(154, 69);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(504, 21);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(89, 69);
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
            this.label9.Location = new System.Drawing.Point(22, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(154, 45);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(504, 21);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(89, 45);
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
            this.label1.Location = new System.Drawing.Point(18, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(154, 20);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(504, 21);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(89, 20);
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
            this.label3.Location = new System.Drawing.Point(42, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMedicamentoNOSP);
            this.FrameInsumos.Controls.Add(this.rdoInsumoMedicamentoSP);
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(11, 154);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(175, 126);
            this.FrameInsumos.TabIndex = 2;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMedicamentoNOSP
            // 
            this.rdoInsumoMedicamentoNOSP.Location = new System.Drawing.Point(36, 74);
            this.rdoInsumoMedicamentoNOSP.Name = "rdoInsumoMedicamentoNOSP";
            this.rdoInsumoMedicamentoNOSP.Size = new System.Drawing.Size(120, 21);
            this.rdoInsumoMedicamentoNOSP.TabIndex = 3;
            this.rdoInsumoMedicamentoNOSP.TabStop = true;
            this.rdoInsumoMedicamentoNOSP.Text = "No Seguro Popular";
            this.rdoInsumoMedicamentoNOSP.UseVisualStyleBackColor = true;
            this.rdoInsumoMedicamentoNOSP.CheckedChanged += new System.EventHandler(this.rdoInsumoMedicamentoNOSP_CheckedChanged);
            // 
            // rdoInsumoMedicamentoSP
            // 
            this.rdoInsumoMedicamentoSP.Location = new System.Drawing.Point(36, 56);
            this.rdoInsumoMedicamentoSP.Name = "rdoInsumoMedicamentoSP";
            this.rdoInsumoMedicamentoSP.Size = new System.Drawing.Size(120, 21);
            this.rdoInsumoMedicamentoSP.TabIndex = 2;
            this.rdoInsumoMedicamentoSP.TabStop = true;
            this.rdoInsumoMedicamentoSP.Text = "Seguro Popular";
            this.rdoInsumoMedicamentoSP.UseVisualStyleBackColor = true;
            this.rdoInsumoMedicamentoSP.CheckedChanged += new System.EventHandler(this.rdoInsumoMedicamentoSP_CheckedChanged);
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(16, 98);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(122, 19);
            this.rdoInsumoMatCuracion.TabIndex = 4;
            this.rdoInsumoMatCuracion.TabStop = true;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(16, 19);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(122, 19);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(16, 38);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(122, 19);
            this.rdoInsumosMedicamento.TabIndex = 1;
            this.rdoInsumosMedicamento.TabStop = true;
            this.rdoInsumosMedicamento.Text = "Medicamento";
            this.rdoInsumosMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDispAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(192, 154);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(156, 126);
            this.FrameDispensacion.TabIndex = 3;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(26, 20);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(94, 19);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(26, 73);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 21);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(26, 47);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 19);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.TabStop = true;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // FrameListaReportes
            // 
            this.FrameListaReportes.Controls.Add(this.chkMostrarLotes);
            this.FrameListaReportes.Controls.Add(this.chkMostrarPrecios);
            this.FrameListaReportes.Controls.Add(this.chkMostrarPaquetes);
            this.FrameListaReportes.Controls.Add(this.cboReporte);
            this.FrameListaReportes.Location = new System.Drawing.Point(11, 279);
            this.FrameListaReportes.Name = "FrameListaReportes";
            this.FrameListaReportes.Size = new System.Drawing.Size(670, 75);
            this.FrameListaReportes.TabIndex = 6;
            this.FrameListaReportes.TabStop = false;
            this.FrameListaReportes.Text = "Reporte para Impresión";
            // 
            // chkMostrarLotes
            // 
            this.chkMostrarLotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarLotes.Location = new System.Drawing.Point(210, 45);
            this.chkMostrarLotes.Name = "chkMostrarLotes";
            this.chkMostrarLotes.Size = new System.Drawing.Size(170, 17);
            this.chkMostrarLotes.TabIndex = 2;
            this.chkMostrarLotes.Text = "Mostrar lotes y caducidades";
            this.chkMostrarLotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarLotes.UseVisualStyleBackColor = true;
            // 
            // chkMostrarPrecios
            // 
            this.chkMostrarPrecios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarPrecios.Location = new System.Drawing.Point(12, 45);
            this.chkMostrarPrecios.Name = "chkMostrarPrecios";
            this.chkMostrarPrecios.Size = new System.Drawing.Size(110, 17);
            this.chkMostrarPrecios.TabIndex = 1;
            this.chkMostrarPrecios.Text = "Mostrar precios";
            this.chkMostrarPrecios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarPrecios.UseVisualStyleBackColor = true;
            // 
            // chkMostrarPaquetes
            // 
            this.chkMostrarPaquetes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarPaquetes.Location = new System.Drawing.Point(468, 45);
            this.chkMostrarPaquetes.Name = "chkMostrarPaquetes";
            this.chkMostrarPaquetes.Size = new System.Drawing.Size(189, 17);
            this.chkMostrarPaquetes.TabIndex = 3;
            this.chkMostrarPaquetes.Text = "Mostrar Validación por Paquetes";
            this.chkMostrarPaquetes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarPaquetes.UseVisualStyleBackColor = true;
            // 
            // cboReporte
            // 
            this.cboReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboReporte.Data = "";
            this.cboReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReporte.Filtro = " 1 = 1";
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.ListaItemsBusqueda = 20;
            this.cboReporte.Location = new System.Drawing.Point(12, 20);
            this.cboReporte.MostrarToolTip = false;
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(646, 21);
            this.cboReporte.TabIndex = 0;
            this.cboReporte.SelectedIndexChanged += new System.EventHandler(this.cboReporte_SelectedIndexChanged);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 461);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(692, 24);
            this.label11.TabIndex = 17;
            this.label11.Text = "<F7> Seleccionar Sub-Farmacias";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameTituloReporteValidacion
            // 
            this.FrameTituloReporteValidacion.Controls.Add(this.cboTitulosReporte);
            this.FrameTituloReporteValidacion.Location = new System.Drawing.Point(11, 354);
            this.FrameTituloReporteValidacion.Name = "FrameTituloReporteValidacion";
            this.FrameTituloReporteValidacion.Size = new System.Drawing.Size(670, 50);
            this.FrameTituloReporteValidacion.TabIndex = 7;
            this.FrameTituloReporteValidacion.TabStop = false;
            this.FrameTituloReporteValidacion.Text = "Titulo de reportes de validación";
            // 
            // cboTitulosReporte
            // 
            this.cboTitulosReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboTitulosReporte.Data = "";
            this.cboTitulosReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTitulosReporte.Filtro = " 1 = 1";
            this.cboTitulosReporte.FormattingEnabled = true;
            this.cboTitulosReporte.ListaItemsBusqueda = 20;
            this.cboTitulosReporte.Location = new System.Drawing.Point(12, 18);
            this.cboTitulosReporte.MostrarToolTip = false;
            this.cboTitulosReporte.Name = "cboTitulosReporte";
            this.cboTitulosReporte.Size = new System.Drawing.Size(645, 21);
            this.cboTitulosReporte.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostrarDevoluciones);
            this.groupBox1.Location = new System.Drawing.Point(521, 237);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(160, 43);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Devoluciones";
            // 
            // chkMostrarDevoluciones
            // 
            this.chkMostrarDevoluciones.Location = new System.Drawing.Point(13, 18);
            this.chkMostrarDevoluciones.Name = "chkMostrarDevoluciones";
            this.chkMostrarDevoluciones.Size = new System.Drawing.Size(128, 17);
            this.chkMostrarDevoluciones.TabIndex = 0;
            this.chkMostrarDevoluciones.Text = "Mostrar devoluciones";
            this.chkMostrarDevoluciones.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenDispensacion
            // 
            this.FrameOrigenDispensacion.Controls.Add(this.rdoOrigenDispensacion_01__General);
            this.FrameOrigenDispensacion.Controls.Add(this.rdoOrigenDispensacion_03_Vales);
            this.FrameOrigenDispensacion.Controls.Add(this.rdoOrigenDispensacion_02_Dispensacion);
            this.FrameOrigenDispensacion.Location = new System.Drawing.Point(354, 154);
            this.FrameOrigenDispensacion.Name = "FrameOrigenDispensacion";
            this.FrameOrigenDispensacion.Size = new System.Drawing.Size(156, 126);
            this.FrameOrigenDispensacion.TabIndex = 4;
            this.FrameOrigenDispensacion.TabStop = false;
            this.FrameOrigenDispensacion.Text = "Origen de Dispensación";
            // 
            // rdoOrigenDispensacion_01__General
            // 
            this.rdoOrigenDispensacion_01__General.Location = new System.Drawing.Point(25, 20);
            this.rdoOrigenDispensacion_01__General.Name = "rdoOrigenDispensacion_01__General";
            this.rdoOrigenDispensacion_01__General.Size = new System.Drawing.Size(94, 19);
            this.rdoOrigenDispensacion_01__General.TabIndex = 0;
            this.rdoOrigenDispensacion_01__General.TabStop = true;
            this.rdoOrigenDispensacion_01__General.Text = "Ambos";
            this.rdoOrigenDispensacion_01__General.UseVisualStyleBackColor = true;
            // 
            // rdoOrigenDispensacion_03_Vales
            // 
            this.rdoOrigenDispensacion_03_Vales.Location = new System.Drawing.Point(25, 73);
            this.rdoOrigenDispensacion_03_Vales.Name = "rdoOrigenDispensacion_03_Vales";
            this.rdoOrigenDispensacion_03_Vales.Size = new System.Drawing.Size(94, 21);
            this.rdoOrigenDispensacion_03_Vales.TabIndex = 2;
            this.rdoOrigenDispensacion_03_Vales.TabStop = true;
            this.rdoOrigenDispensacion_03_Vales.Text = "Vales";
            this.rdoOrigenDispensacion_03_Vales.UseVisualStyleBackColor = true;
            // 
            // rdoOrigenDispensacion_02_Dispensacion
            // 
            this.rdoOrigenDispensacion_02_Dispensacion.Location = new System.Drawing.Point(25, 47);
            this.rdoOrigenDispensacion_02_Dispensacion.Name = "rdoOrigenDispensacion_02_Dispensacion";
            this.rdoOrigenDispensacion_02_Dispensacion.Size = new System.Drawing.Size(94, 19);
            this.rdoOrigenDispensacion_02_Dispensacion.TabIndex = 1;
            this.rdoOrigenDispensacion_02_Dispensacion.TabStop = true;
            this.rdoOrigenDispensacion_02_Dispensacion.Text = "Dispensación";
            this.rdoOrigenDispensacion_02_Dispensacion.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoOrdenamiento_01__ClaveSSA);
            this.groupBox3.Controls.Add(this.rdoOrdenamiento_02__DescripcionClaveSSA);
            this.groupBox3.Location = new System.Drawing.Point(11, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(670, 50);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ordenamiento de reportes concentrados";
            // 
            // rdoOrdenamiento_01__ClaveSSA
            // 
            this.rdoOrdenamiento_01__ClaveSSA.Location = new System.Drawing.Point(134, 19);
            this.rdoOrdenamiento_01__ClaveSSA.Name = "rdoOrdenamiento_01__ClaveSSA";
            this.rdoOrdenamiento_01__ClaveSSA.Size = new System.Drawing.Size(168, 19);
            this.rdoOrdenamiento_01__ClaveSSA.TabIndex = 1;
            this.rdoOrdenamiento_01__ClaveSSA.TabStop = true;
            this.rdoOrdenamiento_01__ClaveSSA.Text = "Clave SSA";
            this.rdoOrdenamiento_01__ClaveSSA.UseVisualStyleBackColor = true;
            // 
            // rdoOrdenamiento_02__DescripcionClaveSSA
            // 
            this.rdoOrdenamiento_02__DescripcionClaveSSA.Location = new System.Drawing.Point(368, 19);
            this.rdoOrdenamiento_02__DescripcionClaveSSA.Name = "rdoOrdenamiento_02__DescripcionClaveSSA";
            this.rdoOrdenamiento_02__DescripcionClaveSSA.Size = new System.Drawing.Size(168, 19);
            this.rdoOrdenamiento_02__DescripcionClaveSSA.TabIndex = 2;
            this.rdoOrdenamiento_02__DescripcionClaveSSA.TabStop = true;
            this.rdoOrdenamiento_02__DescripcionClaveSSA.Text = "Descripción Clave SSA";
            this.rdoOrdenamiento_02__DescripcionClaveSSA.UseVisualStyleBackColor = true;
            // 
            // FrmAdministrativos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 485);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameOrigenDispensacion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameTituloReporteValidacion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FrameListaReportes);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAdministrativos";
            this.Text = "Reportes Administrativos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAdministrativos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAdministrativos_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).EndInit();
            this.FrameFechas.ResumeLayout(false);
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.FrameInsumos.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.FrameListaReportes.ResumeLayout(false);
            this.FrameTituloReporteValidacion.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.FrameOrigenDispensacion.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameCliente;
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
        private System.Windows.Forms.GroupBox FrameListaReportes;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private FarPoint.Win.Spread.SheetView grdReporte_Sheet1;
        private SC_ControlsCS.scComboBoxExt cboReporte;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamentoNOSP;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamentoSP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.CheckBox chkMostrarPaquetes;
        private System.Windows.Forms.CheckBox chkMostrarPrecios;
        private System.Windows.Forms.GroupBox FrameTituloReporteValidacion;
        private SC_ControlsCS.scComboBoxExt cboTitulosReporte;
        private System.Windows.Forms.CheckBox chkMostrarLotes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkMostrarDevoluciones;
        private System.Windows.Forms.GroupBox FrameOrigenDispensacion;
        private System.Windows.Forms.RadioButton rdoOrigenDispensacion_01__General;
        private System.Windows.Forms.RadioButton rdoOrigenDispensacion_03_Vales;
        private System.Windows.Forms.RadioButton rdoOrigenDispensacion_02_Dispensacion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoOrdenamiento_01__ClaveSSA;
        private System.Windows.Forms.RadioButton rdoOrdenamiento_02__DescripcionClaveSSA;
    }
}