namespace MA_Facturacion.GenerarDocumentos
{
    partial class FrmGenerarContrarecibos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarContrarecibos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType7 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType9 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipoUnidades = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameTipoDocoto = new System.Windows.Forms.GroupBox();
            this.rdoFacturas_Ambas = new System.Windows.Forms.RadioButton();
            this.rdoFacturas_ConContrarecibo = new System.Windows.Forms.RadioButton();
            this.rdoFacturas_SinContrarecibo = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.cboEstado = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameTipoDocoto.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnGuardar,
            this.toolStripSeparator3,
            this.btnGenerarDocumentos,
            this.btnIntegrarPaquetesDeDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1357, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarDocumentos
            // 
            this.btnGenerarDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarDocumentos.Enabled = false;
            this.btnGenerarDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarDocumentos.Image")));
            this.btnGenerarDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarDocumentos.Name = "btnGenerarDocumentos";
            this.btnGenerarDocumentos.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarDocumentos.Text = "Generar documentos";
            this.btnGenerarDocumentos.Visible = false;
            this.btnGenerarDocumentos.Click += new System.EventHandler(this.btnGenerarDocumentos_Click);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.cboEstado);
            this.FrameDatosOperacion.Controls.Add(this.label1);
            this.FrameDatosOperacion.Controls.Add(this.cboFarmacias);
            this.FrameDatosOperacion.Controls.Add(this.label8);
            this.FrameDatosOperacion.Controls.Add(this.cboJurisdicciones);
            this.FrameDatosOperacion.Controls.Add(this.label4);
            this.FrameDatosOperacion.Controls.Add(this.cboTipoUnidades);
            this.FrameDatosOperacion.Controls.Add(this.label6);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(15, 37);
            this.FrameDatosOperacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosOperacion.Size = new System.Drawing.Size(653, 159);
            this.FrameDatosOperacion.TabIndex = 1;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(123, 123);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(511, 24);
            this.cboFarmacias.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(15, 128);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(123, 91);
            this.cboJurisdicciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(511, 24);
            this.cboJurisdicciones.TabIndex = 24;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Jurisdicción :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoUnidades
            // 
            this.cboTipoUnidades.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoUnidades.Data = "";
            this.cboTipoUnidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoUnidades.Filtro = " 1 = 1";
            this.cboTipoUnidades.FormattingEnabled = true;
            this.cboTipoUnidades.ListaItemsBusqueda = 20;
            this.cboTipoUnidades.Location = new System.Drawing.Point(123, 60);
            this.cboTipoUnidades.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTipoUnidades.MostrarToolTip = false;
            this.cboTipoUnidades.Name = "cboTipoUnidades";
            this.cboTipoUnidades.Size = new System.Drawing.Size(511, 24);
            this.cboTipoUnidades.TabIndex = 23;
            this.cboTipoUnidades.SelectedIndexChanged += new System.EventHandler(this.cboTipoUnidades_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "Tipo :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(16, 257);
            this.FrameUnidades.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUnidades.Size = new System.Drawing.Size(1329, 480);
            this.FrameUnidades.TabIndex = 13;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(292, 117);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Size = new System.Drawing.Size(495, 70);
            this.FrameProceso.TabIndex = 3;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 28);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(460, 23);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(1152, 0);
            this.chkMarcarDesmarcar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(165, 21);
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
            this.grdUnidades.Location = new System.Drawing.Point(15, 21);
            this.grdUnidades.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(1301, 451);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 9;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Farmacia";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "En contrarecibo";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Procesado";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha factura";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Núm. Factura";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Folio factura";
            this.grdUnidades_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType11;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Farmacia";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 340F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = checkBoxCellType7;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "En contrarecibo";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType8;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(4).CellType = checkBoxCellType9;
            this.grdUnidades_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(4).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(5).CellType = textCellType13;
            this.grdUnidades_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(5).Label = "Fecha factura";
            this.grdUnidades_Sheet1.Columns.Get(5).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(5).Width = 90F;
            this.grdUnidades_Sheet1.Columns.Get(6).CellType = textCellType14;
            this.grdUnidades_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).Label = "Núm. Factura";
            this.grdUnidades_Sheet1.Columns.Get(6).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).Width = 100F;
            currencyCellType3.DecimalPlaces = 2;
            currencyCellType3.DecimalSeparator = ".";
            currencyCellType3.MaximumValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            131072});
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType3.Separator = ",";
            currencyCellType3.ShowSeparator = true;
            this.grdUnidades_Sheet1.Columns.Get(7).CellType = currencyCellType3;
            this.grdUnidades_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUnidades_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdUnidades_Sheet1.Columns.Get(7).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Width = 100F;
            this.grdUnidades_Sheet1.Columns.Get(8).CellType = textCellType15;
            this.grdUnidades_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(8).Label = "Folio factura";
            this.grdUnidades_Sheet1.Columns.Get(8).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(8).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(8).Width = 100F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(15, 200);
            this.FrameDirectorioDeTrabajo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(1331, 55);
            this.FrameDirectorioDeTrabajo.TabIndex = 5;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(1283, 20);
            this.btnDirectorio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(35, 28);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(15, 20);
            this.lblDirectorioTrabajo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(1260, 26);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameTipoDocoto
            // 
            this.FrameTipoDocoto.Controls.Add(this.rdoFacturas_Ambas);
            this.FrameTipoDocoto.Controls.Add(this.rdoFacturas_ConContrarecibo);
            this.FrameTipoDocoto.Controls.Add(this.rdoFacturas_SinContrarecibo);
            this.FrameTipoDocoto.Location = new System.Drawing.Point(676, 112);
            this.FrameTipoDocoto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoDocoto.Name = "FrameTipoDocoto";
            this.FrameTipoDocoto.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoDocoto.Size = new System.Drawing.Size(668, 81);
            this.FrameTipoDocoto.TabIndex = 4;
            this.FrameTipoDocoto.TabStop = false;
            this.FrameTipoDocoto.Text = "Status de factura";
            // 
            // rdoFacturas_Ambas
            // 
            this.rdoFacturas_Ambas.Location = new System.Drawing.Point(515, 20);
            this.rdoFacturas_Ambas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoFacturas_Ambas.Name = "rdoFacturas_Ambas";
            this.rdoFacturas_Ambas.Size = new System.Drawing.Size(104, 23);
            this.rdoFacturas_Ambas.TabIndex = 2;
            this.rdoFacturas_Ambas.Text = "Ambas";
            this.rdoFacturas_Ambas.UseVisualStyleBackColor = true;
            // 
            // rdoFacturas_ConContrarecibo
            // 
            this.rdoFacturas_ConContrarecibo.Location = new System.Drawing.Point(281, 20);
            this.rdoFacturas_ConContrarecibo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoFacturas_ConContrarecibo.Name = "rdoFacturas_ConContrarecibo";
            this.rdoFacturas_ConContrarecibo.Size = new System.Drawing.Size(221, 23);
            this.rdoFacturas_ConContrarecibo.TabIndex = 1;
            this.rdoFacturas_ConContrarecibo.Text = "Facturas con contrarecibo";
            this.rdoFacturas_ConContrarecibo.UseVisualStyleBackColor = true;
            // 
            // rdoFacturas_SinContrarecibo
            // 
            this.rdoFacturas_SinContrarecibo.Checked = true;
            this.rdoFacturas_SinContrarecibo.Location = new System.Drawing.Point(48, 20);
            this.rdoFacturas_SinContrarecibo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoFacturas_SinContrarecibo.Name = "rdoFacturas_SinContrarecibo";
            this.rdoFacturas_SinContrarecibo.Size = new System.Drawing.Size(221, 23);
            this.rdoFacturas_SinContrarecibo.TabIndex = 0;
            this.rdoFacturas_SinContrarecibo.TabStop = true;
            this.rdoFacturas_SinContrarecibo.Text = "Facturas sin contrarecibo";
            this.rdoFacturas_SinContrarecibo.UseVisualStyleBackColor = true;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(676, 37);
            this.FrameFechaDeProceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(668, 74);
            this.FrameFechaDeProceso.TabIndex = 3;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Periodo de emisión de facturas";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(371, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 25);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(128, 21);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 25);
            this.label12.TabIndex = 2;
            this.label12.Text = "Desde :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(433, 21);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(105, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(191, 21);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(105, 22);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // cboEstado
            // 
            this.cboEstado.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstado.Data = "";
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.Filtro = " 1 = 1";
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.ListaItemsBusqueda = 20;
            this.cboEstado.Location = new System.Drawing.Point(123, 28);
            this.cboEstado.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstado.MostrarToolTip = false;
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(511, 24);
            this.cboEstado.TabIndex = 33;
            this.cboEstado.SelectedIndexChanged += new System.EventHandler(this.cboEstado_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGenerarContrarecibos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 748);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameTipoDocoto);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmGenerarContrarecibos";
            this.Text = "Generar contrarecibos ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReporteadorValidaciones_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReporteadorValidaciones_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameTipoDocoto.ResumeLayout(false);
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.ToolStripButton btnGenerarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.GroupBox FrameTipoDocoto;
        private System.Windows.Forms.RadioButton rdoFacturas_SinContrarecibo;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboTipoUnidades;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdoFacturas_ConContrarecibo;
        private System.Windows.Forms.RadioButton rdoFacturas_Ambas;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private SC_ControlsCS.scComboBoxExt cboEstado;
        private System.Windows.Forms.Label label1;
    }
}