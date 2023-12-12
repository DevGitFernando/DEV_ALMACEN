namespace Dll_SII_INadro.InformacionDeSalida
{
    partial class FrmINF_Salidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmINF_Salidas));
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstvUnidades = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDocumentoGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDocumentoPorFarmacia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameTipoDeDocto = new System.Windows.Forms.GroupBox();
            this.nmCauses = new System.Windows.Forms.NumericUpDown();
            this.rdoCatalogo = new System.Windows.Forms.RadioButton();
            this.rdoTomaDeExistencias = new System.Windows.Forms.RadioButton();
            this.rdoExistencias = new System.Windows.Forms.RadioButton();
            this.rdoRemisiones = new System.Windows.Forms.RadioButton();
            this.rdoRecibos = new System.Windows.Forms.RadioButton();
            this.rdoSurtidos = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.lblFechaEnProceso = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkSepararPorCauses = new System.Windows.Forms.CheckBox();
            this.rdoDatos_Historico = new System.Windows.Forms.RadioButton();
            this.rdoDatos_Generar = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoDeDocto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCauses)).BeginInit();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstvUnidades);
            this.groupBox1.Location = new System.Drawing.Point(1076, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 91);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unidades";
            // 
            // lstvUnidades
            // 
            this.lstvUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvUnidades.ContextMenuStrip = this.contextMenu;
            this.lstvUnidades.Location = new System.Drawing.Point(10, 19);
            this.lstvUnidades.Name = "lstvUnidades";
            this.lstvUnidades.Size = new System.Drawing.Size(87, 63);
            this.lstvUnidades.TabIndex = 0;
            this.lstvUnidades.UseCompatibleStateImageBehavior = false;
            this.lstvUnidades.SelectedIndexChanged += new System.EventHandler(this.lstvUnidades_SelectedIndexChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDocumentoGeneral,
            this.btnDocumentoPorFarmacia});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(123, 48);
            // 
            // btnDocumentoGeneral
            // 
            this.btnDocumentoGeneral.Name = "btnDocumentoGeneral";
            this.btnDocumentoGeneral.Size = new System.Drawing.Size(122, 22);
            this.btnDocumentoGeneral.Text = "General";
            this.btnDocumentoGeneral.Click += new System.EventHandler(this.btnDocumentoGeneral_Click);
            // 
            // btnDocumentoPorFarmacia
            // 
            this.btnDocumentoPorFarmacia.Name = "btnDocumentoPorFarmacia";
            this.btnDocumentoPorFarmacia.Size = new System.Drawing.Size(122, 22);
            this.btnDocumentoPorFarmacia.Text = "Farmacia";
            this.btnDocumentoPorFarmacia.Click += new System.EventHandler(this.btnDocumentoPorFarmacia_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnProcesarDocumentos,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1230, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProcesarDocumentos
            // 
            this.btnProcesarDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesarDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesarDocumentos.Image")));
            this.btnProcesarDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesarDocumentos.Name = "btnProcesarDocumentos";
            this.btnProcesarDocumentos.Size = new System.Drawing.Size(23, 22);
            this.btnProcesarDocumentos.Text = "Generar pedido masivo";
            this.btnProcesarDocumentos.Click += new System.EventHandler(this.btnProcesarDocumentos_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameTipoDeDocto
            // 
            this.FrameTipoDeDocto.Controls.Add(this.nmCauses);
            this.FrameTipoDeDocto.Controls.Add(this.rdoCatalogo);
            this.FrameTipoDeDocto.Controls.Add(this.rdoTomaDeExistencias);
            this.FrameTipoDeDocto.Controls.Add(this.rdoExistencias);
            this.FrameTipoDeDocto.Controls.Add(this.rdoRemisiones);
            this.FrameTipoDeDocto.Controls.Add(this.rdoRecibos);
            this.FrameTipoDeDocto.Controls.Add(this.rdoSurtidos);
            this.FrameTipoDeDocto.Location = new System.Drawing.Point(12, 28);
            this.FrameTipoDeDocto.Name = "FrameTipoDeDocto";
            this.FrameTipoDeDocto.Size = new System.Drawing.Size(648, 65);
            this.FrameTipoDeDocto.TabIndex = 1;
            this.FrameTipoDeDocto.TabStop = false;
            this.FrameTipoDeDocto.Text = "Tipo de documento";
            // 
            // nmCauses
            // 
            this.nmCauses.Location = new System.Drawing.Point(492, 15);
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
            this.nmCauses.TabIndex = 3;
            this.nmCauses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmCauses.Value = new decimal(new int[] {
            2007,
            0,
            0,
            0});
            // 
            // rdoCatalogo
            // 
            this.rdoCatalogo.Location = new System.Drawing.Point(559, 41);
            this.rdoCatalogo.Name = "rdoCatalogo";
            this.rdoCatalogo.Size = new System.Drawing.Size(81, 18);
            this.rdoCatalogo.TabIndex = 6;
            this.rdoCatalogo.Text = "Catálogos";
            this.rdoCatalogo.UseVisualStyleBackColor = true;
            this.rdoCatalogo.CheckedChanged += new System.EventHandler(this.rdoCatalogo_CheckedChanged);
            // 
            // rdoTomaDeExistencias
            // 
            this.rdoTomaDeExistencias.Location = new System.Drawing.Point(268, 40);
            this.rdoTomaDeExistencias.Name = "rdoTomaDeExistencias";
            this.rdoTomaDeExistencias.Size = new System.Drawing.Size(249, 18);
            this.rdoTomaDeExistencias.TabIndex = 4;
            this.rdoTomaDeExistencias.Text = "Tomas de existencias (Ajustes de Inventario)";
            this.rdoTomaDeExistencias.UseVisualStyleBackColor = true;
            this.rdoTomaDeExistencias.CheckedChanged += new System.EventHandler(this.rdoTomaDeExistencias_CheckedChanged);
            // 
            // rdoExistencias
            // 
            this.rdoExistencias.Location = new System.Drawing.Point(559, 17);
            this.rdoExistencias.Name = "rdoExistencias";
            this.rdoExistencias.Size = new System.Drawing.Size(81, 18);
            this.rdoExistencias.TabIndex = 5;
            this.rdoExistencias.Text = "Existencias";
            this.rdoExistencias.UseVisualStyleBackColor = true;
            this.rdoExistencias.CheckedChanged += new System.EventHandler(this.rdoExistencias_CheckedChanged);
            // 
            // rdoRemisiones
            // 
            this.rdoRemisiones.Location = new System.Drawing.Point(268, 16);
            this.rdoRemisiones.Name = "rdoRemisiones";
            this.rdoRemisiones.Size = new System.Drawing.Size(224, 18);
            this.rdoRemisiones.TabIndex = 2;
            this.rdoRemisiones.Text = "Remisiones (Válidación firmada)     Causes";
            this.rdoRemisiones.UseVisualStyleBackColor = true;
            this.rdoRemisiones.CheckedChanged += new System.EventHandler(this.rdoRemisiones_CheckedChanged);
            // 
            // rdoRecibos
            // 
            this.rdoRecibos.Location = new System.Drawing.Point(17, 40);
            this.rdoRecibos.Name = "rdoRecibos";
            this.rdoRecibos.Size = new System.Drawing.Size(248, 18);
            this.rdoRecibos.TabIndex = 1;
            this.rdoRecibos.Text = "Recibos (Tranferencias de Entrada y Pedidos)";
            this.rdoRecibos.UseVisualStyleBackColor = true;
            this.rdoRecibos.CheckedChanged += new System.EventHandler(this.rdoRecibos_CheckedChanged);
            // 
            // rdoSurtidos
            // 
            this.rdoSurtidos.Location = new System.Drawing.Point(17, 16);
            this.rdoSurtidos.Name = "rdoSurtidos";
            this.rdoSurtidos.Size = new System.Drawing.Size(223, 18);
            this.rdoSurtidos.TabIndex = 0;
            this.rdoSurtidos.Text = "Surtidos (Dispensación y Transferencias)";
            this.rdoSurtidos.UseVisualStyleBackColor = true;
            this.rdoSurtidos.CheckedChanged += new System.EventHandler(this.rdoSurtidos_CheckedChanged);
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.label2);
            this.FrameFechaDeProceso.Controls.Add(this.label1);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(900, 28);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(147, 65);
            this.FrameFechaDeProceso.TabIndex = 2;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Fechas a procesar";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(58, 38);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(58, 15);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 539);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1230, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "Clic derecho para ver el menú";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Location = new System.Drawing.Point(12, 144);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(1035, 388);
            this.FrameUnidades.TabIndex = 4;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(200, 140);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(455, 57);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(426, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
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
            this.grdUnidades.Size = new System.Drawing.Size(1016, 364);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 8;
            this.grdUnidades_Sheet1.RowCount = 16;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Cliente";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Procesado";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Inicio";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fin";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Procesando";
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Unidad";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Cliente";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = textCellType9;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Nombre Unidad";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 480F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType3;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(4).CellType = checkBoxCellType4;
            this.grdUnidades_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(4).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(5).CellType = textCellType10;
            this.grdUnidades_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(5).Label = "Inicio";
            this.grdUnidades_Sheet1.Columns.Get(5).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).CellType = textCellType11;
            this.grdUnidades_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(6).Label = "Fin";
            this.grdUnidades_Sheet1.Columns.Get(6).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).CellType = textCellType12;
            this.grdUnidades_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Label = "Procesando";
            this.grdUnidades_Sheet1.Columns.Get(7).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(7).Width = 80F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(898, 0);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(126, 17);
            this.chkMarcarDesmarcar.TabIndex = 2;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // lblFechaEnProceso
            // 
            this.lblFechaEnProceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaEnProceso.Location = new System.Drawing.Point(1076, 123);
            this.lblFechaEnProceso.Name = "lblFechaEnProceso";
            this.lblFechaEnProceso.Size = new System.Drawing.Size(105, 57);
            this.lblFechaEnProceso.TabIndex = 13;
            this.lblFechaEnProceso.Text = "label3";
            this.lblFechaEnProceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFechaEnProceso.TextChanged += new System.EventHandler(this.lblFechaEnProceso_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDirectorio);
            this.groupBox2.Controls.Add(this.lblDirectorioTrabajo);
            this.groupBox2.Location = new System.Drawing.Point(12, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1035, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(988, 14);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(36, 23);
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
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(971, 21);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoDatos_Historico);
            this.groupBox4.Controls.Add(this.rdoDatos_Generar);
            this.groupBox4.Location = new System.Drawing.Point(666, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 65);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Origen de datos";
            // 
            // chkSepararPorCauses
            // 
            this.chkSepararPorCauses.Location = new System.Drawing.Point(1076, 196);
            this.chkSepararPorCauses.Name = "chkSepararPorCauses";
            this.chkSepararPorCauses.Size = new System.Drawing.Size(105, 18);
            this.chkSepararPorCauses.TabIndex = 15;
            this.chkSepararPorCauses.Text = "Separar causes";
            this.chkSepararPorCauses.UseVisualStyleBackColor = true;
            // 
            // rdoDatos_Historico
            // 
            this.rdoDatos_Historico.Location = new System.Drawing.Point(101, 28);
            this.rdoDatos_Historico.Name = "rdoDatos_Historico";
            this.rdoDatos_Historico.Size = new System.Drawing.Size(101, 18);
            this.rdoDatos_Historico.TabIndex = 24;
            this.rdoDatos_Historico.TabStop = true;
            this.rdoDatos_Historico.Text = "Datos historicos";
            this.rdoDatos_Historico.UseVisualStyleBackColor = true;
            // 
            // rdoDatos_Generar
            // 
            this.rdoDatos_Generar.Location = new System.Drawing.Point(21, 28);
            this.rdoDatos_Generar.Name = "rdoDatos_Generar";
            this.rdoDatos_Generar.Size = new System.Drawing.Size(63, 18);
            this.rdoDatos_Generar.TabIndex = 23;
            this.rdoDatos_Generar.TabStop = true;
            this.rdoDatos_Generar.Text = "Generar";
            this.rdoDatos_Generar.UseVisualStyleBackColor = true;
            // 
            // FrmINF_Salidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 563);
            this.Controls.Add(this.chkSepararPorCauses);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblFechaEnProceso);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameTipoDeDocto);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmINF_Salidas";
            this.Text = "Generar documentos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmINF_Salidas_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoDeDocto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmCauses)).EndInit();
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstvUnidades;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProcesarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem btnDocumentoGeneral;
        private System.Windows.Forms.ToolStripMenuItem btnDocumentoPorFarmacia;
        private System.Windows.Forms.GroupBox FrameTipoDeDocto;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.RadioButton rdoRemisiones;
        private System.Windows.Forms.RadioButton rdoRecibos;
        private System.Windows.Forms.RadioButton rdoSurtidos;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.RadioButton rdoExistencias;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.RadioButton rdoTomaDeExistencias;
        private System.Windows.Forms.RadioButton rdoCatalogo;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.Label lblFechaEnProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.NumericUpDown nmCauses;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoDatos_Historico;
        private System.Windows.Forms.RadioButton rdoDatos_Generar;
        private System.Windows.Forms.CheckBox chkSepararPorCauses;
    }
}