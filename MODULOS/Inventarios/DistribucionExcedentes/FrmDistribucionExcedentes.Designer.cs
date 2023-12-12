namespace Inventarios.DistribucionExcedentes
{
    partial class FrmDistribucionExcedentes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDistribucionExcedentes));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType12 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcelExcedentes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabExcedentes = new System.Windows.Forms.TabPage();
            this.FrameTipoDeCaducidades = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmMesesCaducidad = new System.Windows.Forms.NumericUpDown();
            this.rdoProximoCaducar = new System.Windows.Forms.RadioButton();
            this.rdoBuenaCaducidad = new System.Windows.Forms.RadioButton();
            this.FrameUbicaciones = new System.Windows.Forms.GroupBox();
            this.cboPasillos = new SC_ControlsCS.scComboBoxExt();
            this.cboEntrepanos = new SC_ControlsCS.scComboBoxExt();
            this.lblPasillo = new System.Windows.Forms.Label();
            this.lstvwExcedentes = new System.Windows.Forms.ListView();
            this.lblEntrepaño = new System.Windows.Forms.Label();
            this.lblEstante = new System.Windows.Forms.Label();
            this.cboEstantes = new SC_ControlsCS.scComboBoxExt();
            this.grdExcedente = new FarPoint.Win.Spread.FpSpread();
            this.grdExcedente_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameCriteriosExcedentes = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.lblMesesExist = new System.Windows.Forms.Label();
            this.lblMesesRev = new System.Windows.Forms.Label();
            this.nmMesesExistencia = new System.Windows.Forms.NumericUpDown();
            this.nmMesesRevision = new System.Windows.Forms.NumericUpDown();
            this.tabFaltantesJuris = new System.Windows.Forms.TabPage();
            this.lblProceso_Juris = new SC_ControlsCS.scLabelExt();
            this.lblDistribucion = new SC_ControlsCS.scLabelExt();
            this.lstvwFaltantesJuris = new System.Windows.Forms.ListView();
            this.menuFaltantes = new System.Windows.Forms.ToolStrip();
            this.btnNuevoFaltantes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutarFaltantes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDistribucion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDetenerJuris = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tabFaltantesOtrasJuris = new System.Windows.Forms.TabPage();
            this.lblDistribucionOtrasJuris = new SC_ControlsCS.scLabelExt();
            this.lblProceso_OtrasJuris = new SC_ControlsCS.scLabelExt();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.lstvwFaltantesOtrasJuris = new System.Windows.Forms.ListView();
            this.mneFaltantesOtrasJurisdicciones = new System.Windows.Forms.ToolStrip();
            this.btnNuevoFaltantesOtrasJuris = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutarFaltantesOtrasJuris = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDistribucionOtrasJuris = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDetenerOtrasJuris = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tabDistribuido = new System.Windows.Forms.TabPage();
            this.lstvwDistribuido = new System.Windows.Forms.ListView();
            this.menuDistribucion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo_Distribucion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar_Distribucion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarDistribuido = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.toolStripBarraMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabExcedentes.SuspendLayout();
            this.FrameTipoDeCaducidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCaducidad)).BeginInit();
            this.FrameUbicaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcedente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcedente_Sheet1)).BeginInit();
            this.FrameCriteriosExcedentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesRevision)).BeginInit();
            this.tabFaltantesJuris.SuspendLayout();
            this.menuFaltantes.SuspendLayout();
            this.tabFaltantesOtrasJuris.SuspendLayout();
            this.mneFaltantesOtrasJurisdicciones.SuspendLayout();
            this.tabDistribuido.SuspendLayout();
            this.menuDistribucion.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator,
            this.btnExportarExcelExcedentes,
            this.toolStripSeparator9,
            this.btnExportarExcel,
            this.toolStripSeparator8});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1060, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Obtener excedentes";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcelExcedentes
            // 
            this.btnExportarExcelExcedentes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcelExcedentes.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcelExcedentes.Image")));
            this.btnExportarExcelExcedentes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcelExcedentes.Name = "btnExportarExcelExcedentes";
            this.btnExportarExcelExcedentes.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcelExcedentes.Text = "Exportar excedentes";
            this.btnExportarExcelExcedentes.ToolTipText = "Exportar a Excel excedentes";
            this.btnExportarExcelExcedentes.Click += new System.EventHandler(this.btnExportarExcelExcedentes_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar distribución generada";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel distribución generada";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabExcedentes);
            this.tabControl.Controls.Add(this.tabFaltantesJuris);
            this.tabControl.Controls.Add(this.tabFaltantesOtrasJuris);
            this.tabControl.Controls.Add(this.tabDistribuido);
            this.tabControl.Location = new System.Drawing.Point(9, 27);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(998, 460);
            this.tabControl.TabIndex = 4;
            // 
            // tabExcedentes
            // 
            this.tabExcedentes.BackColor = System.Drawing.Color.Transparent;
            this.tabExcedentes.Controls.Add(this.FrameTipoDeCaducidades);
            this.tabExcedentes.Controls.Add(this.FrameUbicaciones);
            this.tabExcedentes.Controls.Add(this.grdExcedente);
            this.tabExcedentes.Controls.Add(this.FrameCriteriosExcedentes);
            this.tabExcedentes.Location = new System.Drawing.Point(4, 25);
            this.tabExcedentes.Name = "tabExcedentes";
            this.tabExcedentes.Padding = new System.Windows.Forms.Padding(3);
            this.tabExcedentes.Size = new System.Drawing.Size(990, 431);
            this.tabExcedentes.TabIndex = 0;
            this.tabExcedentes.Text = "Excedentes";
            this.tabExcedentes.UseVisualStyleBackColor = true;
            // 
            // FrameTipoDeCaducidades
            // 
            this.FrameTipoDeCaducidades.Controls.Add(this.label1);
            this.FrameTipoDeCaducidades.Controls.Add(this.nmMesesCaducidad);
            this.FrameTipoDeCaducidades.Controls.Add(this.rdoProximoCaducar);
            this.FrameTipoDeCaducidades.Controls.Add(this.rdoBuenaCaducidad);
            this.FrameTipoDeCaducidades.Location = new System.Drawing.Point(591, 6);
            this.FrameTipoDeCaducidades.Name = "FrameTipoDeCaducidades";
            this.FrameTipoDeCaducidades.Size = new System.Drawing.Size(393, 57);
            this.FrameTipoDeCaducidades.TabIndex = 56;
            this.FrameTipoDeCaducidades.TabStop = false;
            this.FrameTipoDeCaducidades.Text = "Tipo de caducidades";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(257, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 27);
            this.label1.TabIndex = 5;
            this.label1.Text = "Meses caducidad :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMesesCaducidad
            // 
            this.nmMesesCaducidad.Location = new System.Drawing.Point(327, 21);
            this.nmMesesCaducidad.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMesesCaducidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesCaducidad.Name = "nmMesesCaducidad";
            this.nmMesesCaducidad.Size = new System.Drawing.Size(55, 20);
            this.nmMesesCaducidad.TabIndex = 4;
            this.nmMesesCaducidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesCaducidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesCaducidad.ValueChanged += new System.EventHandler(this.nmMesesCaducidad_ValueChanged);
            // 
            // rdoProximoCaducar
            // 
            this.rdoProximoCaducar.Location = new System.Drawing.Point(22, 35);
            this.rdoProximoCaducar.Name = "rdoProximoCaducar";
            this.rdoProximoCaducar.Size = new System.Drawing.Size(221, 17);
            this.rdoProximoCaducar.TabIndex = 1;
            this.rdoProximoCaducar.TabStop = true;
            this.rdoProximoCaducar.Text = "Próximos a caducar ( de 1 a {0} meses )";
            this.rdoProximoCaducar.UseVisualStyleBackColor = true;
            // 
            // rdoBuenaCaducidad
            // 
            this.rdoBuenaCaducidad.Location = new System.Drawing.Point(22, 16);
            this.rdoBuenaCaducidad.Name = "rdoBuenaCaducidad";
            this.rdoBuenaCaducidad.Size = new System.Drawing.Size(221, 17);
            this.rdoBuenaCaducidad.TabIndex = 0;
            this.rdoBuenaCaducidad.TabStop = true;
            this.rdoBuenaCaducidad.Text = "Caducidad de mas de meses";
            this.rdoBuenaCaducidad.UseVisualStyleBackColor = true;
            // 
            // FrameUbicaciones
            // 
            this.FrameUbicaciones.Controls.Add(this.cboPasillos);
            this.FrameUbicaciones.Controls.Add(this.cboEntrepanos);
            this.FrameUbicaciones.Controls.Add(this.lblPasillo);
            this.FrameUbicaciones.Controls.Add(this.lstvwExcedentes);
            this.FrameUbicaciones.Controls.Add(this.lblEntrepaño);
            this.FrameUbicaciones.Controls.Add(this.lblEstante);
            this.FrameUbicaciones.Controls.Add(this.cboEstantes);
            this.FrameUbicaciones.Location = new System.Drawing.Point(6, 65);
            this.FrameUbicaciones.Name = "FrameUbicaciones";
            this.FrameUbicaciones.Size = new System.Drawing.Size(978, 51);
            this.FrameUbicaciones.TabIndex = 55;
            this.FrameUbicaciones.TabStop = false;
            this.FrameUbicaciones.Text = "Ubicaciones";
            // 
            // cboPasillos
            // 
            this.cboPasillos.BackColorEnabled = System.Drawing.Color.White;
            this.cboPasillos.Data = "";
            this.cboPasillos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPasillos.Enabled = false;
            this.cboPasillos.Filtro = " 1 = 1";
            this.cboPasillos.FormattingEnabled = true;
            this.cboPasillos.ListaItemsBusqueda = 20;
            this.cboPasillos.Location = new System.Drawing.Point(128, 19);
            this.cboPasillos.MostrarToolTip = false;
            this.cboPasillos.Name = "cboPasillos";
            this.cboPasillos.Size = new System.Drawing.Size(204, 21);
            this.cboPasillos.TabIndex = 49;
            this.cboPasillos.SelectedIndexChanged += new System.EventHandler(this.cboPasillos_SelectedIndexChanged);
            // 
            // cboEntrepanos
            // 
            this.cboEntrepanos.BackColorEnabled = System.Drawing.Color.White;
            this.cboEntrepanos.Data = "";
            this.cboEntrepanos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEntrepanos.Enabled = false;
            this.cboEntrepanos.Filtro = " 1 = 1";
            this.cboEntrepanos.FormattingEnabled = true;
            this.cboEntrepanos.ListaItemsBusqueda = 20;
            this.cboEntrepanos.Location = new System.Drawing.Point(725, 19);
            this.cboEntrepanos.MostrarToolTip = false;
            this.cboEntrepanos.Name = "cboEntrepanos";
            this.cboEntrepanos.Size = new System.Drawing.Size(204, 21);
            this.cboEntrepanos.TabIndex = 51;
            // 
            // lblPasillo
            // 
            this.lblPasillo.Location = new System.Drawing.Point(52, 21);
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(75, 16);
            this.lblPasillo.TabIndex = 54;
            this.lblPasillo.Text = "Pasillo :";
            this.lblPasillo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstvwExcedentes
            // 
            this.lstvwExcedentes.CheckBoxes = true;
            this.lstvwExcedentes.FullRowSelect = true;
            this.lstvwExcedentes.GridLines = true;
            this.lstvwExcedentes.Location = new System.Drawing.Point(25, 13);
            this.lstvwExcedentes.Name = "lstvwExcedentes";
            this.lstvwExcedentes.ShowItemToolTips = true;
            this.lstvwExcedentes.Size = new System.Drawing.Size(21, 24);
            this.lstvwExcedentes.TabIndex = 0;
            this.lstvwExcedentes.UseCompatibleStateImageBehavior = false;
            this.lstvwExcedentes.Visible = false;
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.Location = new System.Drawing.Point(649, 21);
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(75, 16);
            this.lblEntrepaño.TabIndex = 53;
            this.lblEntrepaño.Text = "Entrepaño :";
            this.lblEntrepaño.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstante
            // 
            this.lblEstante.Location = new System.Drawing.Point(350, 21);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(75, 16);
            this.lblEstante.TabIndex = 52;
            this.lblEstante.Text = "Estante :";
            this.lblEstante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstantes
            // 
            this.cboEstantes.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstantes.Data = "";
            this.cboEstantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstantes.Enabled = false;
            this.cboEstantes.Filtro = " 1 = 1";
            this.cboEstantes.FormattingEnabled = true;
            this.cboEstantes.ListaItemsBusqueda = 20;
            this.cboEstantes.Location = new System.Drawing.Point(426, 19);
            this.cboEstantes.MostrarToolTip = false;
            this.cboEstantes.Name = "cboEstantes";
            this.cboEstantes.Size = new System.Drawing.Size(204, 21);
            this.cboEstantes.TabIndex = 50;
            this.cboEstantes.SelectedIndexChanged += new System.EventHandler(this.cboEstantes_SelectedIndexChanged);
            // 
            // grdExcedente
            // 
            this.grdExcedente.AccessibleDescription = "grdExcedente, Sheet1, Row 0, Column 0, ";
            this.grdExcedente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdExcedente.BackColor = System.Drawing.Color.Transparent;
            this.grdExcedente.Location = new System.Drawing.Point(6, 122);
            this.grdExcedente.Name = "grdExcedente";
            this.grdExcedente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExcedente.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExcedente_Sheet1});
            this.grdExcedente.Size = new System.Drawing.Size(978, 303);
            this.grdExcedente.TabIndex = 2;
            // 
            // grdExcedente_Sheet1
            // 
            this.grdExcedente_Sheet1.Reset();
            this.grdExcedente_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExcedente_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExcedente_Sheet1.ColumnCount = 7;
            this.grdExcedente_Sheet1.RowCount = 18;
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Consumo";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Stock sugerido";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Existencia";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Excedente";
            this.grdExcedente_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Distribuir";
            this.grdExcedente_Sheet1.ColumnHeader.Rows.Get(0).Height = 28F;
            this.grdExcedente_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdExcedente_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdExcedente_Sheet1.Columns.Get(0).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(0).Width = 100F;
            this.grdExcedente_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdExcedente_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExcedente_Sheet1.Columns.Get(1).Label = "Descripción Clave";
            this.grdExcedente_Sheet1.Columns.Get(1).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(1).Width = 360F;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.DecimalSeparator = ".";
            numberCellType9.MaximumValue = 10000000D;
            numberCellType9.MinimumValue = 0D;
            numberCellType9.Separator = ",";
            numberCellType9.ShowSeparator = true;
            this.grdExcedente_Sheet1.Columns.Get(2).CellType = numberCellType9;
            this.grdExcedente_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExcedente_Sheet1.Columns.Get(2).Label = "Consumo";
            this.grdExcedente_Sheet1.Columns.Get(2).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(2).Width = 100F;
            numberCellType10.DecimalPlaces = 0;
            numberCellType10.DecimalSeparator = ".";
            numberCellType10.MaximumValue = 10000000D;
            numberCellType10.MinimumValue = 0D;
            numberCellType10.Separator = ",";
            numberCellType10.ShowSeparator = true;
            this.grdExcedente_Sheet1.Columns.Get(3).CellType = numberCellType10;
            this.grdExcedente_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExcedente_Sheet1.Columns.Get(3).Label = "Stock sugerido";
            this.grdExcedente_Sheet1.Columns.Get(3).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(3).Width = 100F;
            numberCellType11.DecimalPlaces = 0;
            numberCellType11.DecimalSeparator = ".";
            numberCellType11.MaximumValue = 10000000D;
            numberCellType11.MinimumValue = 0D;
            numberCellType11.Separator = ",";
            numberCellType11.ShowSeparator = true;
            this.grdExcedente_Sheet1.Columns.Get(4).CellType = numberCellType11;
            this.grdExcedente_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExcedente_Sheet1.Columns.Get(4).Label = "Existencia";
            this.grdExcedente_Sheet1.Columns.Get(4).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(4).Width = 100F;
            numberCellType12.DecimalPlaces = 0;
            numberCellType12.DecimalSeparator = ".";
            numberCellType12.MaximumValue = 10000000D;
            numberCellType12.MinimumValue = 0D;
            numberCellType12.Separator = ",";
            numberCellType12.ShowSeparator = true;
            this.grdExcedente_Sheet1.Columns.Get(5).CellType = numberCellType12;
            this.grdExcedente_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExcedente_Sheet1.Columns.Get(5).Label = "Excedente";
            this.grdExcedente_Sheet1.Columns.Get(5).Locked = true;
            this.grdExcedente_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(5).Width = 100F;
            this.grdExcedente_Sheet1.Columns.Get(6).CellType = checkBoxCellType3;
            this.grdExcedente_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(6).Label = "Distribuir";
            this.grdExcedente_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExcedente_Sheet1.Columns.Get(6).Width = 68F;
            this.grdExcedente_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdExcedente_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameCriteriosExcedentes
            // 
            this.FrameCriteriosExcedentes.Controls.Add(this.chkTodas);
            this.FrameCriteriosExcedentes.Controls.Add(this.lblMesesExist);
            this.FrameCriteriosExcedentes.Controls.Add(this.lblMesesRev);
            this.FrameCriteriosExcedentes.Controls.Add(this.nmMesesExistencia);
            this.FrameCriteriosExcedentes.Controls.Add(this.nmMesesRevision);
            this.FrameCriteriosExcedentes.Location = new System.Drawing.Point(6, 6);
            this.FrameCriteriosExcedentes.Name = "FrameCriteriosExcedentes";
            this.FrameCriteriosExcedentes.Size = new System.Drawing.Size(581, 57);
            this.FrameCriteriosExcedentes.TabIndex = 1;
            this.FrameCriteriosExcedentes.TabStop = false;
            this.FrameCriteriosExcedentes.Text = "Criterios para determinar excedentes";
            // 
            // chkTodas
            // 
            this.chkTodas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.Location = new System.Drawing.Point(423, 22);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(125, 17);
            this.chkTodas.TabIndex = 4;
            this.chkTodas.Text = "Marcar / Desmarcar";
            this.chkTodas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // lblMesesExist
            // 
            this.lblMesesExist.Location = new System.Drawing.Point(226, 17);
            this.lblMesesExist.Name = "lblMesesExist";
            this.lblMesesExist.Size = new System.Drawing.Size(119, 29);
            this.lblMesesExist.TabIndex = 3;
            this.lblMesesExist.Text = "Meses de existencia sugeridos :";
            this.lblMesesExist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMesesRev
            // 
            this.lblMesesRev.Location = new System.Drawing.Point(33, 17);
            this.lblMesesRev.Name = "lblMesesRev";
            this.lblMesesRev.Size = new System.Drawing.Size(119, 29);
            this.lblMesesRev.TabIndex = 2;
            this.lblMesesRev.Text = "Meses de revisión de consumos :";
            this.lblMesesRev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMesesExistencia
            // 
            this.nmMesesExistencia.Location = new System.Drawing.Point(348, 21);
            this.nmMesesExistencia.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMesesExistencia.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesExistencia.Name = "nmMesesExistencia";
            this.nmMesesExistencia.Size = new System.Drawing.Size(55, 20);
            this.nmMesesExistencia.TabIndex = 1;
            this.nmMesesExistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesExistencia.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nmMesesRevision
            // 
            this.nmMesesRevision.Location = new System.Drawing.Point(155, 21);
            this.nmMesesRevision.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMesesRevision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesRevision.Name = "nmMesesRevision";
            this.nmMesesRevision.Size = new System.Drawing.Size(55, 20);
            this.nmMesesRevision.TabIndex = 0;
            this.nmMesesRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesRevision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabFaltantesJuris
            // 
            this.tabFaltantesJuris.BackColor = System.Drawing.Color.Transparent;
            this.tabFaltantesJuris.Controls.Add(this.lblProceso_Juris);
            this.tabFaltantesJuris.Controls.Add(this.lblDistribucion);
            this.tabFaltantesJuris.Controls.Add(this.lstvwFaltantesJuris);
            this.tabFaltantesJuris.Controls.Add(this.menuFaltantes);
            this.tabFaltantesJuris.Location = new System.Drawing.Point(4, 25);
            this.tabFaltantesJuris.Name = "tabFaltantesJuris";
            this.tabFaltantesJuris.Padding = new System.Windows.Forms.Padding(3);
            this.tabFaltantesJuris.Size = new System.Drawing.Size(990, 431);
            this.tabFaltantesJuris.TabIndex = 1;
            this.tabFaltantesJuris.Text = "Faltantes Juris";
            this.tabFaltantesJuris.UseVisualStyleBackColor = true;
            // 
            // lblProceso_Juris
            // 
            this.lblProceso_Juris.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProceso_Juris.Location = new System.Drawing.Point(741, 5);
            this.lblProceso_Juris.MostrarToolTip = false;
            this.lblProceso_Juris.Name = "lblProceso_Juris";
            this.lblProceso_Juris.Size = new System.Drawing.Size(240, 20);
            this.lblProceso_Juris.TabIndex = 10;
            this.lblProceso_Juris.Text = "scLabelExt1";
            this.lblProceso_Juris.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDistribucion
            // 
            this.lblDistribucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistribucion.Location = new System.Drawing.Point(130, 6);
            this.lblDistribucion.MostrarToolTip = false;
            this.lblDistribucion.Name = "lblDistribucion";
            this.lblDistribucion.Size = new System.Drawing.Size(160, 20);
            this.lblDistribucion.TabIndex = 11;
            this.lblDistribucion.Text = "Distribucioón generada";
            this.lblDistribucion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstvwFaltantesJuris
            // 
            this.lstvwFaltantesJuris.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstvwFaltantesJuris.Location = new System.Drawing.Point(6, 31);
            this.lstvwFaltantesJuris.Name = "lstvwFaltantesJuris";
            this.lstvwFaltantesJuris.Size = new System.Drawing.Size(978, 394);
            this.lstvwFaltantesJuris.TabIndex = 5;
            this.lstvwFaltantesJuris.UseCompatibleStateImageBehavior = false;
            // 
            // menuFaltantes
            // 
            this.menuFaltantes.BackColor = System.Drawing.Color.Transparent;
            this.menuFaltantes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoFaltantes,
            this.toolStripSeparator2,
            this.btnEjecutarFaltantes,
            this.toolStripSeparator3,
            this.btnGenerarDistribucion,
            this.toolStripSeparator6,
            this.btnDetenerJuris,
            this.toolStripSeparator13});
            this.menuFaltantes.Location = new System.Drawing.Point(3, 3);
            this.menuFaltantes.Name = "menuFaltantes";
            this.menuFaltantes.Size = new System.Drawing.Size(984, 25);
            this.menuFaltantes.TabIndex = 4;
            this.menuFaltantes.Text = "Faltantes";
            // 
            // btnNuevoFaltantes
            // 
            this.btnNuevoFaltantes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoFaltantes.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoFaltantes.Image")));
            this.btnNuevoFaltantes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoFaltantes.Name = "btnNuevoFaltantes";
            this.btnNuevoFaltantes.Size = new System.Drawing.Size(23, 22);
            this.btnNuevoFaltantes.Text = "&Nuevo";
            this.btnNuevoFaltantes.Click += new System.EventHandler(this.btnNuevoFaltantes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutarFaltantes
            // 
            this.btnEjecutarFaltantes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutarFaltantes.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutarFaltantes.Image")));
            this.btnEjecutarFaltantes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutarFaltantes.Name = "btnEjecutarFaltantes";
            this.btnEjecutarFaltantes.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutarFaltantes.Text = "Obtener faltantes";
            this.btnEjecutarFaltantes.Click += new System.EventHandler(this.btnEjecutarFaltantes_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarDistribucion
            // 
            this.btnGenerarDistribucion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarDistribucion.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarDistribucion.Image")));
            this.btnGenerarDistribucion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarDistribucion.Name = "btnGenerarDistribucion";
            this.btnGenerarDistribucion.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarDistribucion.Text = "Generar distribución";
            this.btnGenerarDistribucion.Click += new System.EventHandler(this.btnGenerarDistribucion_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDetenerJuris
            // 
            this.btnDetenerJuris.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetenerJuris.Image = ((System.Drawing.Image)(resources.GetObject("btnDetenerJuris.Image")));
            this.btnDetenerJuris.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetenerJuris.Name = "btnDetenerJuris";
            this.btnDetenerJuris.Size = new System.Drawing.Size(23, 22);
            this.btnDetenerJuris.Text = "toolStripButton1";
            this.btnDetenerJuris.Click += new System.EventHandler(this.btnDetenerJuris_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // tabFaltantesOtrasJuris
            // 
            this.tabFaltantesOtrasJuris.Controls.Add(this.lblDistribucionOtrasJuris);
            this.tabFaltantesOtrasJuris.Controls.Add(this.lblProceso_OtrasJuris);
            this.tabFaltantesOtrasJuris.Controls.Add(this.cboJurisdicciones);
            this.tabFaltantesOtrasJuris.Controls.Add(this.lstvwFaltantesOtrasJuris);
            this.tabFaltantesOtrasJuris.Controls.Add(this.mneFaltantesOtrasJurisdicciones);
            this.tabFaltantesOtrasJuris.Location = new System.Drawing.Point(4, 25);
            this.tabFaltantesOtrasJuris.Name = "tabFaltantesOtrasJuris";
            this.tabFaltantesOtrasJuris.Size = new System.Drawing.Size(990, 431);
            this.tabFaltantesOtrasJuris.TabIndex = 2;
            this.tabFaltantesOtrasJuris.Text = "Faltantes otra jurisdiccion";
            this.tabFaltantesOtrasJuris.UseVisualStyleBackColor = true;
            // 
            // lblDistribucionOtrasJuris
            // 
            this.lblDistribucionOtrasJuris.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistribucionOtrasJuris.Location = new System.Drawing.Point(436, 2);
            this.lblDistribucionOtrasJuris.MostrarToolTip = false;
            this.lblDistribucionOtrasJuris.Name = "lblDistribucionOtrasJuris";
            this.lblDistribucionOtrasJuris.Size = new System.Drawing.Size(160, 20);
            this.lblDistribucionOtrasJuris.TabIndex = 12;
            this.lblDistribucionOtrasJuris.Text = "Distribucioón generada";
            this.lblDistribucionOtrasJuris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProceso_OtrasJuris
            // 
            this.lblProceso_OtrasJuris.BackColor = System.Drawing.Color.Transparent;
            this.lblProceso_OtrasJuris.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProceso_OtrasJuris.Location = new System.Drawing.Point(744, 3);
            this.lblProceso_OtrasJuris.MostrarToolTip = false;
            this.lblProceso_OtrasJuris.Name = "lblProceso_OtrasJuris";
            this.lblProceso_OtrasJuris.Size = new System.Drawing.Size(240, 19);
            this.lblProceso_OtrasJuris.TabIndex = 12;
            this.lblProceso_OtrasJuris.Text = "scLabelExt1";
            this.lblProceso_OtrasJuris.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(129, 1);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(301, 21);
            this.cboJurisdicciones.TabIndex = 8;
            // 
            // lstvwFaltantesOtrasJuris
            // 
            this.lstvwFaltantesOtrasJuris.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstvwFaltantesOtrasJuris.Location = new System.Drawing.Point(6, 31);
            this.lstvwFaltantesOtrasJuris.Name = "lstvwFaltantesOtrasJuris";
            this.lstvwFaltantesOtrasJuris.Size = new System.Drawing.Size(978, 397);
            this.lstvwFaltantesOtrasJuris.TabIndex = 7;
            this.lstvwFaltantesOtrasJuris.UseCompatibleStateImageBehavior = false;
            // 
            // mneFaltantesOtrasJurisdicciones
            // 
            this.mneFaltantesOtrasJurisdicciones.BackColor = System.Drawing.Color.Transparent;
            this.mneFaltantesOtrasJurisdicciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoFaltantesOtrasJuris,
            this.toolStripSeparator4,
            this.btnEjecutarFaltantesOtrasJuris,
            this.toolStripSeparator5,
            this.btnGenerarDistribucionOtrasJuris,
            this.toolStripSeparator7,
            this.btnDetenerOtrasJuris,
            this.toolStripSeparator14});
            this.mneFaltantesOtrasJurisdicciones.Location = new System.Drawing.Point(0, 0);
            this.mneFaltantesOtrasJurisdicciones.Name = "mneFaltantesOtrasJurisdicciones";
            this.mneFaltantesOtrasJurisdicciones.Size = new System.Drawing.Size(990, 25);
            this.mneFaltantesOtrasJurisdicciones.TabIndex = 6;
            this.mneFaltantesOtrasJurisdicciones.Text = "Faltantes";
            // 
            // btnNuevoFaltantesOtrasJuris
            // 
            this.btnNuevoFaltantesOtrasJuris.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoFaltantesOtrasJuris.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoFaltantesOtrasJuris.Image")));
            this.btnNuevoFaltantesOtrasJuris.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoFaltantesOtrasJuris.Name = "btnNuevoFaltantesOtrasJuris";
            this.btnNuevoFaltantesOtrasJuris.Size = new System.Drawing.Size(23, 22);
            this.btnNuevoFaltantesOtrasJuris.Text = "&Nuevo";
            this.btnNuevoFaltantesOtrasJuris.Click += new System.EventHandler(this.btnNuevoFaltantesOtrasJuris_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutarFaltantesOtrasJuris
            // 
            this.btnEjecutarFaltantesOtrasJuris.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutarFaltantesOtrasJuris.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutarFaltantesOtrasJuris.Image")));
            this.btnEjecutarFaltantesOtrasJuris.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutarFaltantesOtrasJuris.Name = "btnEjecutarFaltantesOtrasJuris";
            this.btnEjecutarFaltantesOtrasJuris.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutarFaltantesOtrasJuris.Text = "Obtener faltantes";
            this.btnEjecutarFaltantesOtrasJuris.Click += new System.EventHandler(this.btnEjecutarFaltantesOtrasJuris_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarDistribucionOtrasJuris
            // 
            this.btnGenerarDistribucionOtrasJuris.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarDistribucionOtrasJuris.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarDistribucionOtrasJuris.Image")));
            this.btnGenerarDistribucionOtrasJuris.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarDistribucionOtrasJuris.Name = "btnGenerarDistribucionOtrasJuris";
            this.btnGenerarDistribucionOtrasJuris.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarDistribucionOtrasJuris.Text = "Generar distribución";
            this.btnGenerarDistribucionOtrasJuris.Click += new System.EventHandler(this.btnGenerarDistribucionOtrasJuris_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDetenerOtrasJuris
            // 
            this.btnDetenerOtrasJuris.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetenerOtrasJuris.Image = ((System.Drawing.Image)(resources.GetObject("btnDetenerOtrasJuris.Image")));
            this.btnDetenerOtrasJuris.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetenerOtrasJuris.Name = "btnDetenerOtrasJuris";
            this.btnDetenerOtrasJuris.Size = new System.Drawing.Size(23, 22);
            this.btnDetenerOtrasJuris.Text = "toolStripButton1";
            this.btnDetenerOtrasJuris.Click += new System.EventHandler(this.btnDetenerOtrasJuris_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tabDistribuido
            // 
            this.tabDistribuido.Controls.Add(this.lstvwDistribuido);
            this.tabDistribuido.Controls.Add(this.menuDistribucion);
            this.tabDistribuido.Location = new System.Drawing.Point(4, 25);
            this.tabDistribuido.Name = "tabDistribuido";
            this.tabDistribuido.Size = new System.Drawing.Size(990, 431);
            this.tabDistribuido.TabIndex = 3;
            this.tabDistribuido.Text = "Distribución";
            this.tabDistribuido.UseVisualStyleBackColor = true;
            // 
            // lstvwDistribuido
            // 
            this.lstvwDistribuido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstvwDistribuido.Location = new System.Drawing.Point(6, 31);
            this.lstvwDistribuido.Name = "lstvwDistribuido";
            this.lstvwDistribuido.Size = new System.Drawing.Size(978, 397);
            this.lstvwDistribuido.TabIndex = 7;
            this.lstvwDistribuido.UseCompatibleStateImageBehavior = false;
            // 
            // menuDistribucion
            // 
            this.menuDistribucion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo_Distribucion,
            this.toolStripSeparator10,
            this.btnEjecutar_Distribucion,
            this.toolStripSeparator11,
            this.btnExportarDistribuido,
            this.toolStripSeparator12});
            this.menuDistribucion.Location = new System.Drawing.Point(0, 0);
            this.menuDistribucion.Name = "menuDistribucion";
            this.menuDistribucion.Size = new System.Drawing.Size(990, 25);
            this.menuDistribucion.TabIndex = 6;
            this.menuDistribucion.Text = "Faltantes";
            // 
            // btnNuevo_Distribucion
            // 
            this.btnNuevo_Distribucion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo_Distribucion.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo_Distribucion.Image")));
            this.btnNuevo_Distribucion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo_Distribucion.Name = "btnNuevo_Distribucion";
            this.btnNuevo_Distribucion.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo_Distribucion.Text = "&Nuevo";
            this.btnNuevo_Distribucion.Click += new System.EventHandler(this.btnNuevo_Distribucion_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar_Distribucion
            // 
            this.btnEjecutar_Distribucion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar_Distribucion.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar_Distribucion.Image")));
            this.btnEjecutar_Distribucion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar_Distribucion.Name = "btnEjecutar_Distribucion";
            this.btnEjecutar_Distribucion.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar_Distribucion.Text = "Obtener faltantes";
            this.btnEjecutar_Distribucion.Click += new System.EventHandler(this.btnEjecutar_Distribucion_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarDistribuido
            // 
            this.btnExportarDistribuido.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarDistribuido.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarDistribuido.Image")));
            this.btnExportarDistribuido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarDistribuido.Name = "btnExportarDistribuido";
            this.btnExportarDistribuido.Size = new System.Drawing.Size(23, 22);
            this.btnExportarDistribuido.Text = "Exportar distribución generada";
            this.btnExportarDistribuido.ToolTipText = "Exportar a Excel distribución generada";
            this.btnExportarDistribuido.Click += new System.EventHandler(this.btnExportarDistribuido_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(296, 532);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(455, 102);
            this.FrameProceso.TabIndex = 9;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 64);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // FrmDistribucionExcedentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 672);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDistribucionExcedentes";
            this.Text = "Distribución de excedentes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDistribucionExcedentes_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabExcedentes.ResumeLayout(false);
            this.FrameTipoDeCaducidades.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCaducidad)).EndInit();
            this.FrameUbicaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExcedente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcedente_Sheet1)).EndInit();
            this.FrameCriteriosExcedentes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesRevision)).EndInit();
            this.tabFaltantesJuris.ResumeLayout(false);
            this.tabFaltantesJuris.PerformLayout();
            this.menuFaltantes.ResumeLayout(false);
            this.menuFaltantes.PerformLayout();
            this.tabFaltantesOtrasJuris.ResumeLayout(false);
            this.tabFaltantesOtrasJuris.PerformLayout();
            this.mneFaltantesOtrasJurisdicciones.ResumeLayout(false);
            this.mneFaltantesOtrasJurisdicciones.PerformLayout();
            this.tabDistribuido.ResumeLayout(false);
            this.tabDistribuido.PerformLayout();
            this.menuDistribucion.ResumeLayout(false);
            this.menuDistribucion.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabExcedentes;
        private System.Windows.Forms.TabPage tabFaltantesJuris;
        private System.Windows.Forms.ListView lstvwExcedentes;
        private System.Windows.Forms.GroupBox FrameCriteriosExcedentes;
        private System.Windows.Forms.NumericUpDown nmMesesExistencia;
        private System.Windows.Forms.NumericUpDown nmMesesRevision;
        private System.Windows.Forms.Label lblMesesRev;
        private System.Windows.Forms.Label lblMesesExist;
        private System.Windows.Forms.ToolStrip menuFaltantes;
        private System.Windows.Forms.ToolStripButton btnNuevoFaltantes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEjecutarFaltantes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ListView lstvwFaltantesJuris;
        private FarPoint.Win.Spread.FpSpread grdExcedente;
        private FarPoint.Win.Spread.SheetView grdExcedente_Sheet1;
        private System.Windows.Forms.TabPage tabFaltantesOtrasJuris;
        private System.Windows.Forms.ListView lstvwFaltantesOtrasJuris;
        private System.Windows.Forms.ToolStrip mneFaltantesOtrasJurisdicciones;
        private System.Windows.Forms.ToolStripButton btnNuevoFaltantesOtrasJuris;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEjecutarFaltantesOtrasJuris;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnGenerarDistribucion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnGenerarDistribucionOtrasJuris;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnExportarExcelExcedentes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private SC_ControlsCS.scLabelExt lblProceso_Juris;
        private SC_ControlsCS.scLabelExt lblProceso_OtrasJuris;
        private System.Windows.Forms.TabPage tabDistribuido;
        private System.Windows.Forms.ListView lstvwDistribuido;
        private System.Windows.Forms.ToolStrip menuDistribucion;
        private System.Windows.Forms.ToolStripButton btnNuevo_Distribucion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnEjecutar_Distribucion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnExportarDistribuido;
        private System.Windows.Forms.ToolStripButton btnDetenerJuris;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton btnDetenerOtrasJuris;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private SC_ControlsCS.scLabelExt lblDistribucion;
        private SC_ControlsCS.scLabelExt lblDistribucionOtrasJuris;
        private SC_ControlsCS.scComboBoxExt cboPasillos;
        private System.Windows.Forms.Label lblPasillo;
        private SC_ControlsCS.scComboBoxExt cboEntrepanos;
        private SC_ControlsCS.scComboBoxExt cboEstantes;
        private System.Windows.Forms.Label lblEstante;
        private System.Windows.Forms.Label lblEntrepaño;
        private System.Windows.Forms.GroupBox FrameUbicaciones;
        private System.Windows.Forms.GroupBox FrameTipoDeCaducidades;
        private System.Windows.Forms.RadioButton rdoProximoCaducar;
        private System.Windows.Forms.RadioButton rdoBuenaCaducidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmMesesCaducidad;
    }
}