namespace DllFarmaciaSoft.ReportesQFB
{
    partial class FrmRegistrosSanitariosReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistrosSanitariosReporte));
            FarPoint.Win.Spread.CellType.TextCellType textCellType28 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType31 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType32 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType33 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType34 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType35 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType36 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType7 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegistro = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTipo = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarRegistrosSanitarios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.lblToolTip_ClaveSSA_Seleccionada = new System.Windows.Forms.ToolStripLabel();
            this.grdCodigosEAN_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.grdCodigosEAN = new FarPoint.Win.Spread.FpSpread();
            this.chkMarcarTodo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1251, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(1061, 3);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(64, 20);
            this.lblClaveSSA.TabIndex = 11;
            this.lblClaveSSA.Text = "label2";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClaveSSA.Visible = false;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(314, 19);
            this.txtClaveSSA.MaxLength = 30;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(128, 20);
            this.txtClaveSSA.TabIndex = 1;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Location = new System.Drawing.Point(1156, 3);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(21, 22);
            this.lblDescripcionClave.TabIndex = 10;
            this.lblDescripcionClave.Text = "label2";
            this.lblDescripcionClave.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(245, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clave SSA :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.label3);
            this.FrameClaves.Controls.Add(this.txtRegistro);
            this.FrameClaves.Controls.Add(this.label2);
            this.FrameClaves.Controls.Add(this.txtTipo);
            this.FrameClaves.Controls.Add(this.label1);
            this.FrameClaves.Controls.Add(this.txtLaboratorio);
            this.FrameClaves.Controls.Add(this.label4);
            this.FrameClaves.Controls.Add(this.txtClaveSSA);
            this.FrameClaves.Location = new System.Drawing.Point(12, 25);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(1233, 45);
            this.FrameClaves.TabIndex = 0;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Filtros";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(994, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtRegistro
            // 
            this.txtRegistro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRegistro.Decimales = 2;
            this.txtRegistro.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRegistro.ForeColor = System.Drawing.Color.Black;
            this.txtRegistro.Location = new System.Drawing.Point(1066, 19);
            this.txtRegistro.MaxLength = 30;
            this.txtRegistro.Name = "txtRegistro";
            this.txtRegistro.PermitirApostrofo = false;
            this.txtRegistro.PermitirNegativos = false;
            this.txtRegistro.Size = new System.Drawing.Size(128, 20);
            this.txtRegistro.TabIndex = 3;
            this.txtRegistro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(18, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tipo :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtTipo
            // 
            this.txtTipo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTipo.Decimales = 2;
            this.txtTipo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTipo.ForeColor = System.Drawing.Color.Black;
            this.txtTipo.Location = new System.Drawing.Point(67, 19);
            this.txtTipo.MaxLength = 30;
            this.txtTipo.Name = "txtTipo";
            this.txtTipo.PermitirApostrofo = false;
            this.txtTipo.PermitirNegativos = false;
            this.txtTipo.Size = new System.Drawing.Size(128, 20);
            this.txtTipo.TabIndex = 0;
            this.txtTipo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(474, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Laboratorio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtLaboratorio
            // 
            this.txtLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtLaboratorio.Decimales = 2;
            this.txtLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtLaboratorio.Location = new System.Drawing.Point(562, 19);
            this.txtLaboratorio.MaxLength = 50;
            this.txtLaboratorio.Name = "txtLaboratorio";
            this.txtLaboratorio.PermitirApostrofo = false;
            this.txtLaboratorio.PermitirNegativos = false;
            this.txtLaboratorio.Size = new System.Drawing.Size(417, 20);
            this.txtLaboratorio.TabIndex = 2;
            this.txtLaboratorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(12, 71);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(856, 45);
            this.FrameDirectorioDeTrabajo.TabIndex = 6;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(817, 15);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(26, 23);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(11, 16);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(800, 21);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(874, 71);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(371, 45);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando documentos";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(12, 19);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(350, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(481, 4);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1758, 27);
            this.miniToolStrip.TabIndex = 0;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDescargarRegistrosSanitarios
            // 
            this.btnDescargarRegistrosSanitarios.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarRegistrosSanitarios.Image")));
            this.btnDescargarRegistrosSanitarios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarRegistrosSanitarios.Name = "btnDescargarRegistrosSanitarios";
            this.btnDescargarRegistrosSanitarios.Size = new System.Drawing.Size(235, 22);
            this.btnDescargarRegistrosSanitarios.Text = "Descargar registros sanitarios marcados";
            this.btnDescargarRegistrosSanitarios.Click += new System.EventHandler(this.btnDescargarRegistrosSanitarios_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // lblToolTip_ClaveSSA_Seleccionada
            // 
            this.lblToolTip_ClaveSSA_Seleccionada.Name = "lblToolTip_ClaveSSA_Seleccionada";
            this.lblToolTip_ClaveSSA_Seleccionada.Size = new System.Drawing.Size(131, 22);
            this.lblToolTip_ClaveSSA_Seleccionada.Text = "Clave SSA Seleccionada";
            // 
            // grdCodigosEAN_Sheet1
            // 
            this.grdCodigosEAN_Sheet1.Reset();
            this.grdCodigosEAN_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCodigosEAN_Sheet1.ColumnCount = 11;
            this.grdCodigosEAN_Sheet1.RowCount = 16;
            this.grdCodigosEAN_Sheet1.AutoCalculation = false;
            this.grdCodigosEAN_Sheet1.Cells.Get(0, 1).Value = "010.000.000.00";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Tipo";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ClaveSSA";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "IdLaboratorio";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Laboratorio";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Registro";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "MD5";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "NombreArchivo";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Vigencia";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Descargable";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Descargar";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).CellType = textCellType28;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Label = "Tipo";
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Width = 120F;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).CellType = textCellType29;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Label = "ClaveSSA";
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Locked = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Width = 120F;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).CellType = textCellType30;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Label = "IdLaboratorio";
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Width = 96F;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).CellType = textCellType31;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Label = "Laboratorio";
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Width = 160F;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).CellType = textCellType32;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Label = "Descripción";
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Width = 312F;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).CellType = textCellType33;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Label = "Registro";
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Width = 110F;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).CellType = textCellType34;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Label = "MD5";
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Width = 232F;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).CellType = textCellType35;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Label = "NombreArchivo";
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Width = 151F;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).CellType = textCellType36;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Label = "Vigencia";
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Width = 80F;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).CellType = checkBoxCellType7;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Label = "Descargable";
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Width = 128F;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).CellType = checkBoxCellType8;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Label = "Descargar";
            this.grdCodigosEAN_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Width = 71F;
            this.grdCodigosEAN_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdCodigosEAN_Sheet1.ZoomFactor = 1.2F;
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // grdCodigosEAN
            // 
            this.grdCodigosEAN.AccessibleDescription = "grdCodigosEAN, Sheet1, Row 0, Column 0, ";
            this.grdCodigosEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCodigosEAN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCodigosEAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCodigosEAN.Location = new System.Drawing.Point(10, 47);
            this.grdCodigosEAN.Name = "grdCodigosEAN";
            this.grdCodigosEAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCodigosEAN.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCodigosEAN_Sheet1});
            this.grdCodigosEAN.Size = new System.Drawing.Size(1215, 398);
            this.grdCodigosEAN.TabIndex = 1;
            // 
            // chkMarcarTodo
            // 
            this.chkMarcarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.Location = new System.Drawing.Point(1066, 22);
            this.chkMarcarTodo.Name = "chkMarcarTodo";
            this.chkMarcarTodo.Size = new System.Drawing.Size(154, 17);
            this.chkMarcarTodo.TabIndex = 4;
            this.chkMarcarTodo.Text = "Marcar / Desmarcar todo";
            this.chkMarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarTodo_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMarcarTodo);
            this.groupBox1.Controls.Add(this.grdCodigosEAN);
            this.groupBox1.Controls.Add(this.toolStrip);
            this.groupBox1.Location = new System.Drawing.Point(12, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1233, 459);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Códigos EAN por Clave";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.btnDescargarRegistrosSanitarios,
            this.toolStripSeparator8,
            this.lblToolTip_ClaveSSA_Seleccionada});
            this.toolStrip.Location = new System.Drawing.Point(3, 16);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1227, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // FrmRegistrosSanitariosReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1251, 585);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.lblDescripcionClave);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblClaveSSA);
            this.Controls.Add(this.FrameProceso);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmRegistrosSanitariosReporte";
            this.Text = "Descargar Registros Sanitarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRgistrosSanitariosReporte_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameClaves.ResumeLayout(false);
            this.FrameClaves.PerformLayout();
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtTipo;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtLaboratorio;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnDescargarRegistrosSanitarios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel lblToolTip_ClaveSSA_Seleccionada;
        private FarPoint.Win.Spread.SheetView grdCodigosEAN_Sheet1;
        private FarPoint.Win.Spread.FpSpread grdCodigosEAN;
        private System.Windows.Forms.CheckBox chkMarcarTodo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtRegistro;
    }
}