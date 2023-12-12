namespace DllFarmaciaSoft.Productos
{
    partial class FrmDescargarImagenesDeProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDescargarImagenesDeProductos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType31 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType32 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType33 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType34 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType35 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType17 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType18 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType19 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType20 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMarcarTodo = new System.Windows.Forms.CheckBox();
            this.grdCodigosEAN = new FarPoint.Win.Spread.FpSpread();
            this.grdCodigosEAN_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnLaboratorios = new System.Windows.Forms.Button();
            this.btnClaveSSA_Antibioticos = new System.Windows.Forms.Button();
            this.btnClaveSSA_Controlados = new System.Windows.Forms.Button();
            this.btnClaveSSA = new System.Windows.Forms.Button();
            this.FrameAgrupacionImagenes = new System.Windows.Forms.GroupBox();
            this.rdoGpo_02_Laboratorio = new System.Windows.Forms.RadioButton();
            this.rdoGpo_01_ClaveSSA = new System.Windows.Forms.RadioButton();
            this.rdoGpo_03_General = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameAgrupacionImagenes.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1254, 25);
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
            this.btnExportar.Enabled = false;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Visible = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMarcarTodo);
            this.groupBox1.Controls.Add(this.grdCodigosEAN);
            this.groupBox1.Controls.Add(this.toolStrip);
            this.groupBox1.Location = new System.Drawing.Point(12, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1233, 423);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Códigos EAN";
            // 
            // chkMarcarTodo
            // 
            this.chkMarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.Location = new System.Drawing.Point(1070, 19);
            this.chkMarcarTodo.Name = "chkMarcarTodo";
            this.chkMarcarTodo.Size = new System.Drawing.Size(154, 17);
            this.chkMarcarTodo.TabIndex = 4;
            this.chkMarcarTodo.Text = "Marcar / Desmarcar todo";
            this.chkMarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarTodo_CheckedChanged);
            // 
            // grdCodigosEAN
            // 
            this.grdCodigosEAN.AccessibleDescription = "grdCodigosEAN, Sheet1, Row 0, Column 0, ";
            this.grdCodigosEAN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCodigosEAN.Location = new System.Drawing.Point(10, 47);
            this.grdCodigosEAN.Name = "grdCodigosEAN";
            this.grdCodigosEAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCodigosEAN.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCodigosEAN_Sheet1});
            this.grdCodigosEAN.Size = new System.Drawing.Size(1217, 367);
            this.grdCodigosEAN.TabIndex = 1;
            // 
            // grdCodigosEAN_Sheet1
            // 
            this.grdCodigosEAN_Sheet1.Reset();
            this.grdCodigosEAN_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCodigosEAN_Sheet1.ColumnCount = 11;
            this.grdCodigosEAN_Sheet1.RowCount = 16;
            this.grdCodigosEAN_Sheet1.Cells.Get(0, 0).Value = "010.000.0101.00";
            this.grdCodigosEAN_Sheet1.Cells.Get(0, 2).Value = "0123456789012345";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Id Producto";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Laboratorio";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Clave Tipo de Insumo";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Tipo de insumo";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Descargar";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Es Controlado";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Es Antibiotico";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Es Refrigerado";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).CellType = textCellType29;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Width = 120F;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).CellType = textCellType30;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Label = "Id Producto";
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Width = 90F;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).CellType = textCellType31;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Width = 130F;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).CellType = textCellType32;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Label = "Laboratorio";
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Width = 230F;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).CellType = textCellType33;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Label = "Descripción";
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Width = 380F;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).CellType = textCellType34;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Label = "Clave Tipo de Insumo";
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).CellType = textCellType35;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Label = "Tipo de insumo";
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Width = 140F;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).CellType = checkBoxCellType17;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Label = "Descargar";
            this.grdCodigosEAN_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(7).Width = 71F;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).CellType = checkBoxCellType18;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Label = "Es Controlado";
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(8).Width = 70F;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).CellType = checkBoxCellType19;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Label = "Es Antibiotico";
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(9).Width = 70F;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).CellType = checkBoxCellType20;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Label = "Es Refrigerado";
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Locked = true;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Visible = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(10).Width = 70F;
            this.grdCodigosEAN_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.btnDescargarImagenes,
            this.toolStripSeparator8});
            this.toolStrip.Location = new System.Drawing.Point(3, 16);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1227, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDescargarImagenes
            // 
            this.btnDescargarImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarImagenes.Image")));
            this.btnDescargarImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarImagenes.Name = "btnDescargarImagenes";
            this.btnDescargarImagenes.Size = new System.Drawing.Size(283, 22);
            this.btnDescargarImagenes.Text = "Descargar imagenes de productos seleccionados";
            this.btnDescargarImagenes.Click += new System.EventHandler(this.btnDescargarImagenes_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(12, 28);
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
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(874, 28);
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
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(172, 25);
            this.miniToolStrip.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProductos);
            this.groupBox2.Controls.Add(this.btnLaboratorios);
            this.groupBox2.Controls.Add(this.btnClaveSSA_Antibioticos);
            this.groupBox2.Controls.Add(this.btnClaveSSA_Controlados);
            this.groupBox2.Controls.Add(this.btnClaveSSA);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(856, 48);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parámetros";
            // 
            // btnProductos
            // 
            this.btnProductos.Location = new System.Drawing.Point(686, 19);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(160, 23);
            this.btnProductos.TabIndex = 4;
            this.btnProductos.Text = "Productos";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnLaboratorios
            // 
            this.btnLaboratorios.Location = new System.Drawing.Point(517, 19);
            this.btnLaboratorios.Name = "btnLaboratorios";
            this.btnLaboratorios.Size = new System.Drawing.Size(160, 23);
            this.btnLaboratorios.TabIndex = 3;
            this.btnLaboratorios.Text = "Laboratorios";
            this.btnLaboratorios.UseVisualStyleBackColor = true;
            this.btnLaboratorios.Click += new System.EventHandler(this.btnLaboratorios_Click);
            // 
            // btnClaveSSA_Antibioticos
            // 
            this.btnClaveSSA_Antibioticos.Location = new System.Drawing.Point(348, 19);
            this.btnClaveSSA_Antibioticos.Name = "btnClaveSSA_Antibioticos";
            this.btnClaveSSA_Antibioticos.Size = new System.Drawing.Size(160, 23);
            this.btnClaveSSA_Antibioticos.TabIndex = 2;
            this.btnClaveSSA_Antibioticos.Text = "Claves SSA Antibióticos";
            this.btnClaveSSA_Antibioticos.UseVisualStyleBackColor = true;
            this.btnClaveSSA_Antibioticos.Click += new System.EventHandler(this.btnClaveSSA_Antibioticos_Click);
            // 
            // btnClaveSSA_Controlados
            // 
            this.btnClaveSSA_Controlados.Location = new System.Drawing.Point(179, 19);
            this.btnClaveSSA_Controlados.Name = "btnClaveSSA_Controlados";
            this.btnClaveSSA_Controlados.Size = new System.Drawing.Size(160, 23);
            this.btnClaveSSA_Controlados.TabIndex = 1;
            this.btnClaveSSA_Controlados.Text = "Claves SSA Controlados";
            this.btnClaveSSA_Controlados.UseVisualStyleBackColor = true;
            this.btnClaveSSA_Controlados.Click += new System.EventHandler(this.btnClaveSSA_Controlados_Click);
            // 
            // btnClaveSSA
            // 
            this.btnClaveSSA.Location = new System.Drawing.Point(10, 19);
            this.btnClaveSSA.Name = "btnClaveSSA";
            this.btnClaveSSA.Size = new System.Drawing.Size(160, 23);
            this.btnClaveSSA.TabIndex = 0;
            this.btnClaveSSA.Text = "Claves SSA";
            this.btnClaveSSA.UseVisualStyleBackColor = true;
            this.btnClaveSSA.Click += new System.EventHandler(this.btnClaveSSA_Click);
            // 
            // FrameAgrupacionImagenes
            // 
            this.FrameAgrupacionImagenes.Controls.Add(this.rdoGpo_03_General);
            this.FrameAgrupacionImagenes.Controls.Add(this.rdoGpo_02_Laboratorio);
            this.FrameAgrupacionImagenes.Controls.Add(this.rdoGpo_01_ClaveSSA);
            this.FrameAgrupacionImagenes.Location = new System.Drawing.Point(874, 75);
            this.FrameAgrupacionImagenes.Name = "FrameAgrupacionImagenes";
            this.FrameAgrupacionImagenes.Size = new System.Drawing.Size(371, 48);
            this.FrameAgrupacionImagenes.TabIndex = 12;
            this.FrameAgrupacionImagenes.TabStop = false;
            this.FrameAgrupacionImagenes.Text = "Agrupar imagenes descargadas";
            // 
            // rdoGpo_02_Laboratorio
            // 
            this.rdoGpo_02_Laboratorio.Location = new System.Drawing.Point(162, 19);
            this.rdoGpo_02_Laboratorio.Name = "rdoGpo_02_Laboratorio";
            this.rdoGpo_02_Laboratorio.Size = new System.Drawing.Size(129, 20);
            this.rdoGpo_02_Laboratorio.TabIndex = 2;
            this.rdoGpo_02_Laboratorio.Text = "Laboratorio - Clave SSA";
            this.rdoGpo_02_Laboratorio.UseVisualStyleBackColor = true;
            // 
            // rdoGpo_01_ClaveSSA
            // 
            this.rdoGpo_01_ClaveSSA.Location = new System.Drawing.Point(13, 19);
            this.rdoGpo_01_ClaveSSA.Name = "rdoGpo_01_ClaveSSA";
            this.rdoGpo_01_ClaveSSA.Size = new System.Drawing.Size(148, 20);
            this.rdoGpo_01_ClaveSSA.TabIndex = 1;
            this.rdoGpo_01_ClaveSSA.Text = "Clave SSA - Laboratorio";
            this.rdoGpo_01_ClaveSSA.UseVisualStyleBackColor = true;
            // 
            // rdoGpo_03_General
            // 
            this.rdoGpo_03_General.Checked = true;
            this.rdoGpo_03_General.Location = new System.Drawing.Point(292, 19);
            this.rdoGpo_03_General.Name = "rdoGpo_03_General";
            this.rdoGpo_03_General.Size = new System.Drawing.Size(63, 20);
            this.rdoGpo_03_General.TabIndex = 3;
            this.rdoGpo_03_General.TabStop = true;
            this.rdoGpo_03_General.Text = "General";
            this.rdoGpo_03_General.UseVisualStyleBackColor = true;
            // 
            // FrmDescargarImagenesDeProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 558);
            this.Controls.Add(this.FrameAgrupacionImagenes);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.lblDescripcionClave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblClaveSSA);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameProceso);
            this.Name = "FrmDescargarImagenesDeProductos";
            this.Text = "Descargar imagenes de productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDescargarImagenesDeProductos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.FrameAgrupacionImagenes.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnDescargarImagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private FarPoint.Win.Spread.FpSpread grdCodigosEAN;
        private FarPoint.Win.Spread.SheetView grdCodigosEAN_Sheet1;
        private System.Windows.Forms.CheckBox chkMarcarTodo;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClaveSSA;
        private System.Windows.Forms.Button btnClaveSSA_Antibioticos;
        private System.Windows.Forms.Button btnClaveSSA_Controlados;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnLaboratorios;
        private System.Windows.Forms.GroupBox FrameAgrupacionImagenes;
        private System.Windows.Forms.RadioButton rdoGpo_02_Laboratorio;
        private System.Windows.Forms.RadioButton rdoGpo_01_ClaveSSA;
        private System.Windows.Forms.RadioButton rdoGpo_03_General;
    }
}