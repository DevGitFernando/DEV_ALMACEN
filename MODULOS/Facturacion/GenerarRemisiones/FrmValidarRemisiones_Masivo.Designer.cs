namespace Facturacion.GenerarRemisiones
{
    partial class FrmValidarRemisiones_Masivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidarRemisiones_Masivo));
            FarPoint.Win.Spread.CellType.TextCellType textCellType78 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType79 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType80 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType81 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType82 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType83 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType84 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType15 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType16 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType85 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType86 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType87 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType88 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.lblTipoFuenteFinanciamiento = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdFuenteFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.lblFuenteFinanciamiento = new System.Windows.Forms.Label();
            this.lblFinanciamiento = new System.Windows.Forms.Label();
            this.txtIdFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTipoInsumo = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameTipoRemision = new System.Windows.Forms.GroupBox();
            this.rdoRM_Todo = new System.Windows.Forms.RadioButton();
            this.rdoRM_Servicio = new System.Windows.Forms.RadioButton();
            this.rdoRM_Producto = new System.Windows.Forms.RadioButton();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.rdoOIN_Todos = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Consignacion = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Venta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoInsumoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFolios = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameTipoRemision.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.FrameFolios.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1284, 25);
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
            this.btnGenerarDocumentos.Text = "Generar documentos";
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
            this.btnIntegrarPaquetesDeDatos.Text = "Cargar información de remisiones manuales";
            this.btnIntegrarPaquetesDeDatos.Visible = false;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFechaDeProceso.Controls.Add(this.chkFechas);
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(980, 112);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(296, 60);
            this.FrameFechaDeProceso.TabIndex = 7;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Emisión de remisiones";
            // 
            // chkFechas
            // 
            this.chkFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.Location = new System.Drawing.Point(182, 0);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(104, 15);
            this.chkFechas.TabIndex = 0;
            this.chkFechas.Text = "Filtro de Fechas";
            this.chkFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(152, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(10, 27);
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
            this.dtpFechaFinal.Location = new System.Drawing.Point(198, 27);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 2;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(57, 27);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 1;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(12, 174);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(1265, 502);
            this.FrameUnidades.TabIndex = 10;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(219, 180);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(771, 57);
            this.FrameProceso.TabIndex = 3;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(745, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(1131, -1);
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
            this.grdUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(10, 18);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(1245, 476);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 14;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio Remisión";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha Remisión";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fuente financiamiento";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave Financiamiento";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Financiamiento";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Clave Remision";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Tipo de Remision";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Procesado";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "IdFarmaciaDispensacion";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "FarmaciaDispensacion";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "Referencia_Beneficiario";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "Referencia_NombreBeneficiario";
            this.grdUnidades_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType78;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Folio Remisión";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 90F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType79;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Fecha Remisión";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = textCellType80;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Fuente financiamiento";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 90F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = textCellType81;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Clave Financiamiento";
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 90F;
            this.grdUnidades_Sheet1.Columns.Get(4).CellType = textCellType82;
            this.grdUnidades_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(4).Label = "Financiamiento";
            this.grdUnidades_Sheet1.Columns.Get(4).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Width = 240F;
            this.grdUnidades_Sheet1.Columns.Get(5).CellType = textCellType83;
            this.grdUnidades_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(5).Label = "Clave Remision";
            this.grdUnidades_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).CellType = textCellType84;
            this.grdUnidades_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(6).Label = "Tipo de Remision";
            this.grdUnidades_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).Width = 200F;
            numberCellType8.DecimalPlaces = 2;
            numberCellType8.DecimalSeparator = ".";
            numberCellType8.MaximumValue = 999999999999.99D;
            numberCellType8.MinimumValue = 0D;
            numberCellType8.Separator = ",";
            numberCellType8.ShowSeparator = true;
            this.grdUnidades_Sheet1.Columns.Get(7).CellType = numberCellType8;
            this.grdUnidades_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUnidades_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdUnidades_Sheet1.Columns.Get(7).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Width = 86F;
            this.grdUnidades_Sheet1.Columns.Get(8).CellType = checkBoxCellType15;
            this.grdUnidades_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(8).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(8).Locked = false;
            this.grdUnidades_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(9).CellType = checkBoxCellType16;
            this.grdUnidades_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(9).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(9).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(9).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(10).CellType = textCellType85;
            this.grdUnidades_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(10).Label = "IdFarmaciaDispensacion";
            this.grdUnidades_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(10).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(10).Width = 164F;
            this.grdUnidades_Sheet1.Columns.Get(11).CellType = textCellType86;
            this.grdUnidades_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(11).Label = "FarmaciaDispensacion";
            this.grdUnidades_Sheet1.Columns.Get(11).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(11).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(11).Width = 168F;
            this.grdUnidades_Sheet1.Columns.Get(12).CellType = textCellType87;
            this.grdUnidades_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(12).Label = "Referencia_Beneficiario";
            this.grdUnidades_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(12).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(12).Width = 164F;
            this.grdUnidades_Sheet1.Columns.Get(13).CellType = textCellType88;
            this.grdUnidades_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(13).Label = "Referencia_NombreBeneficiario";
            this.grdUnidades_Sheet1.Columns.Get(13).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(13).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(13).Width = 168F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUnidades_Sheet1.Rows.Default.Height = 25F;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.lblTipoFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label7);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label11);
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Controls.Add(this.txtCte);
            this.FrameDatosOperacion.Controls.Add(this.lblCliente);
            this.FrameDatosOperacion.Controls.Add(this.lblSubCliente);
            this.FrameDatosOperacion.Controls.Add(this.txtSubCte);
            this.FrameDatosOperacion.Controls.Add(this.label5);
            this.FrameDatosOperacion.Controls.Add(this.cboTipoInsumo);
            this.FrameDatosOperacion.Controls.Add(this.label1);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(12, 29);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(660, 144);
            this.FrameDatosOperacion.TabIndex = 1;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // lblTipoFuenteFinanciamiento
            // 
            this.lblTipoFuenteFinanciamiento.Location = new System.Drawing.Point(453, 116);
            this.lblTipoFuenteFinanciamiento.Name = "lblTipoFuenteFinanciamiento";
            this.lblTipoFuenteFinanciamiento.Size = new System.Drawing.Size(190, 15);
            this.lblTipoFuenteFinanciamiento.TabIndex = 53;
            this.lblTipoFuenteFinanciamiento.Text = "Tipo segmento :";
            this.lblTipoFuenteFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 49;
            this.label7.Text = "Fuente de financiamiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFuenteFinanciamiento
            // 
            this.txtIdFuenteFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFuenteFinanciamiento.Decimales = 2;
            this.txtIdFuenteFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFuenteFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFuenteFinanciamiento.Location = new System.Drawing.Point(166, 18);
            this.txtIdFuenteFinanciamiento.MaxLength = 4;
            this.txtIdFuenteFinanciamiento.Name = "txtIdFuenteFinanciamiento";
            this.txtIdFuenteFinanciamiento.PermitirApostrofo = false;
            this.txtIdFuenteFinanciamiento.PermitirNegativos = false;
            this.txtIdFuenteFinanciamiento.Size = new System.Drawing.Size(75, 20);
            this.txtIdFuenteFinanciamiento.TabIndex = 0;
            this.txtIdFuenteFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFuenteFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFuenteFinanciamiento_TextChanged);
            this.txtIdFuenteFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFuenteFinanciamiento_KeyDown);
            this.txtIdFuenteFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFuenteFinanciamiento_Validating);
            // 
            // lblFuenteFinanciamiento
            // 
            this.lblFuenteFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuenteFinanciamiento.Location = new System.Drawing.Point(248, 18);
            this.lblFuenteFinanciamiento.Name = "lblFuenteFinanciamiento";
            this.lblFuenteFinanciamiento.Size = new System.Drawing.Size(395, 21);
            this.lblFuenteFinanciamiento.TabIndex = 50;
            this.lblFuenteFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinanciamiento
            // 
            this.lblFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinanciamiento.Location = new System.Drawing.Point(248, 41);
            this.lblFinanciamiento.Name = "lblFinanciamiento";
            this.lblFinanciamiento.Size = new System.Drawing.Size(395, 21);
            this.lblFinanciamiento.TabIndex = 52;
            this.lblFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdFinanciamiento
            // 
            this.txtIdFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFinanciamiento.Decimales = 2;
            this.txtIdFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFinanciamiento.Location = new System.Drawing.Point(166, 41);
            this.txtIdFinanciamiento.MaxLength = 4;
            this.txtIdFinanciamiento.Name = "txtIdFinanciamiento";
            this.txtIdFinanciamiento.PermitirApostrofo = false;
            this.txtIdFinanciamiento.PermitirNegativos = false;
            this.txtIdFinanciamiento.Size = new System.Drawing.Size(75, 20);
            this.txtIdFinanciamiento.TabIndex = 1;
            this.txtIdFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFinanciamiento_TextChanged);
            this.txtIdFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFinanciamiento_KeyDown);
            this.txtIdFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFinanciamiento_Validating);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 18);
            this.label11.TabIndex = 51;
            this.label11.Text = "Financiamiento :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(166, 64);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(75, 20);
            this.txtCte.TabIndex = 2;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(248, 64);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(395, 21);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(248, 87);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(395, 21);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(166, 87);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(75, 20);
            this.txtSubCte.TabIndex = 3;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoInsumo
            // 
            this.cboTipoInsumo.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoInsumo.Data = "";
            this.cboTipoInsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoInsumo.Filtro = " 1 = 1";
            this.cboTipoInsumo.FormattingEnabled = true;
            this.cboTipoInsumo.ListaItemsBusqueda = 20;
            this.cboTipoInsumo.Location = new System.Drawing.Point(166, 113);
            this.cboTipoInsumo.MostrarToolTip = false;
            this.cboTipoInsumo.Name = "cboTipoInsumo";
            this.cboTipoInsumo.Size = new System.Drawing.Size(281, 21);
            this.cboTipoInsumo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Tipo segmento :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameTipoRemision
            // 
            this.FrameTipoRemision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Todo);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Servicio);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Producto);
            this.FrameTipoRemision.Location = new System.Drawing.Point(678, 29);
            this.FrameTipoRemision.Name = "FrameTipoRemision";
            this.FrameTipoRemision.Size = new System.Drawing.Size(188, 81);
            this.FrameTipoRemision.TabIndex = 2;
            this.FrameTipoRemision.TabStop = false;
            this.FrameTipoRemision.Text = "Tipo de remisión";
            // 
            // rdoRM_Todo
            // 
            this.rdoRM_Todo.Location = new System.Drawing.Point(49, 56);
            this.rdoRM_Todo.Name = "rdoRM_Todo";
            this.rdoRM_Todo.Size = new System.Drawing.Size(91, 18);
            this.rdoRM_Todo.TabIndex = 22;
            this.rdoRM_Todo.TabStop = true;
            this.rdoRM_Todo.Text = "Ambos";
            this.rdoRM_Todo.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Servicio
            // 
            this.rdoRM_Servicio.Location = new System.Drawing.Point(49, 38);
            this.rdoRM_Servicio.Name = "rdoRM_Servicio";
            this.rdoRM_Servicio.Size = new System.Drawing.Size(91, 17);
            this.rdoRM_Servicio.TabIndex = 1;
            this.rdoRM_Servicio.TabStop = true;
            this.rdoRM_Servicio.Text = "Servicio";
            this.rdoRM_Servicio.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Producto
            // 
            this.rdoRM_Producto.Location = new System.Drawing.Point(49, 20);
            this.rdoRM_Producto.Name = "rdoRM_Producto";
            this.rdoRM_Producto.Size = new System.Drawing.Size(91, 17);
            this.rdoRM_Producto.TabIndex = 0;
            this.rdoRM_Producto.TabStop = true;
            this.rdoRM_Producto.Text = "Producto";
            this.rdoRM_Producto.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Todos);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Consignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Venta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(883, 29);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(177, 81);
            this.FrameOrigenInsumo.TabIndex = 3;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Origen de Insumos";
            // 
            // rdoOIN_Todos
            // 
            this.rdoOIN_Todos.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.rdoOIN_Todos.Location = new System.Drawing.Point(40, 56);
            this.rdoOIN_Todos.Name = "rdoOIN_Todos";
            this.rdoOIN_Todos.Size = new System.Drawing.Size(97, 18);
            this.rdoOIN_Todos.TabIndex = 22;
            this.rdoOIN_Todos.TabStop = true;
            this.rdoOIN_Todos.Text = "Ambos";
            this.rdoOIN_Todos.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Consignacion
            // 
            this.rdoOIN_Consignacion.Location = new System.Drawing.Point(40, 38);
            this.rdoOIN_Consignacion.Name = "rdoOIN_Consignacion";
            this.rdoOIN_Consignacion.Size = new System.Drawing.Size(97, 17);
            this.rdoOIN_Consignacion.TabIndex = 1;
            this.rdoOIN_Consignacion.TabStop = true;
            this.rdoOIN_Consignacion.Text = "Consignación";
            this.rdoOIN_Consignacion.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Venta
            // 
            this.rdoOIN_Venta.Location = new System.Drawing.Point(40, 19);
            this.rdoOIN_Venta.Name = "rdoOIN_Venta";
            this.rdoOIN_Venta.Size = new System.Drawing.Size(97, 18);
            this.rdoOIN_Venta.TabIndex = 0;
            this.rdoOIN_Venta.TabStop = true;
            this.rdoOIN_Venta.Text = "Venta";
            this.rdoOIN_Venta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoAmbos);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(1066, 29);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(211, 81);
            this.FrameTipoInsumo.TabIndex = 4;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoInsumoAmbos
            // 
            this.rdoInsumoAmbos.Location = new System.Drawing.Point(44, 56);
            this.rdoInsumoAmbos.Name = "rdoInsumoAmbos";
            this.rdoInsumoAmbos.Size = new System.Drawing.Size(62, 18);
            this.rdoInsumoAmbos.TabIndex = 2;
            this.rdoInsumoAmbos.TabStop = true;
            this.rdoInsumoAmbos.Text = "Ambos";
            this.rdoInsumoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMaterialDeCuracion
            // 
            this.rdoInsumoMaterialDeCuracion.Location = new System.Drawing.Point(44, 38);
            this.rdoInsumoMaterialDeCuracion.Name = "rdoInsumoMaterialDeCuracion";
            this.rdoInsumoMaterialDeCuracion.Size = new System.Drawing.Size(122, 17);
            this.rdoInsumoMaterialDeCuracion.TabIndex = 1;
            this.rdoInsumoMaterialDeCuracion.TabStop = true;
            this.rdoInsumoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoInsumoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamento
            // 
            this.rdoInsumoMedicamento.Location = new System.Drawing.Point(44, 20);
            this.rdoInsumoMedicamento.Name = "rdoInsumoMedicamento";
            this.rdoInsumoMedicamento.Size = new System.Drawing.Size(98, 17);
            this.rdoInsumoMedicamento.TabIndex = 0;
            this.rdoInsumoMedicamento.TabStop = true;
            this.rdoInsumoMedicamento.Text = "Medicamento";
            this.rdoInsumoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFolios.Controls.Add(this.txtFolioFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFolioInicial);
            this.FrameFolios.Controls.Add(this.label4);
            this.FrameFolios.Controls.Add(this.chkFolios);
            this.FrameFolios.Location = new System.Drawing.Point(678, 112);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(296, 60);
            this.FrameFolios.TabIndex = 6;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(203, 27);
            this.txtFolioFinal.MaxLength = 10;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(85, 20);
            this.txtFolioFinal.TabIndex = 2;
            this.txtFolioFinal.Text = "0123456789";
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(153, 31);
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
            this.txtFolioInicial.Location = new System.Drawing.Point(61, 27);
            this.txtFolioInicial.MaxLength = 10;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(85, 20);
            this.txtFolioInicial.TabIndex = 1;
            this.txtFolioInicial.Text = "0123456789";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Desde :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFolios
            // 
            this.chkFolios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.Location = new System.Drawing.Point(188, 0);
            this.chkFolios.Name = "chkFolios";
            this.chkFolios.Size = new System.Drawing.Size(100, 17);
            this.chkFolios.TabIndex = 0;
            this.chkFolios.Text = "Filtro por Folios";
            this.chkFolios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.UseVisualStyleBackColor = true;
            // 
            // FrmValidarRemisiones_Masivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 681);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoRemision);
            this.Controls.Add(this.FrameOrigenInsumo);
            this.Controls.Add(this.FrameTipoInsumo);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmValidarRemisiones_Masivo";
            this.Text = "Validación de remisiones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidarRemisiones_Masivo_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.FrameTipoRemision.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGenerarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private SC_ControlsCS.scComboBoxExt cboTipoInsumo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameTipoRemision;
        private System.Windows.Forms.RadioButton rdoRM_Todo;
        private System.Windows.Forms.RadioButton rdoRM_Servicio;
        private System.Windows.Forms.RadioButton rdoRM_Producto;
        private System.Windows.Forms.GroupBox FrameOrigenInsumo;
        private System.Windows.Forms.RadioButton rdoOIN_Todos;
        private System.Windows.Forms.RadioButton rdoOIN_Consignacion;
        private System.Windows.Forms.RadioButton rdoOIN_Venta;
        private System.Windows.Forms.GroupBox FrameTipoInsumo;
        private System.Windows.Forms.RadioButton rdoInsumoAmbos;
        private System.Windows.Forms.RadioButton rdoInsumoMaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamento;
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFolios;
        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtIdFuenteFinanciamiento;
        private System.Windows.Forms.Label lblFuenteFinanciamiento;
        private System.Windows.Forms.Label lblFinanciamiento;
        private SC_ControlsCS.scTextBoxExt txtIdFinanciamiento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTipoFuenteFinanciamiento;
    }
}