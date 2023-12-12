namespace Almacen.Ubicaciones
{
    partial class FrmRptUbicacionesClaves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptUbicacionesClaves));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new SC_ControlsCS.scLabelExt();
            this.lblIdClaveSSA = new System.Windows.Forms.Label();
            this.txtIdClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSubFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.FrameLocalizaciones = new System.Windows.Forms.GroupBox();
            this.cboPasillos = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEntrepanos = new SC_ControlsCS.scComboBoxExt();
            this.cboEstantes = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoRptClave = new System.Windows.Forms.RadioButton();
            this.rdoRptUbicacion = new System.Windows.Forms.RadioButton();
            this.FrameUbicaciones = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExistencia = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExistencia = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdoRptAlmacenamiento = new System.Windows.Forms.RadioButton();
            this.rdoRptAmbos = new System.Windows.Forms.RadioButton();
            this.rdoRptPickeo = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameLocalizaciones.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameUbicaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1190, 25);
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
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.lblIdClaveSSA);
            this.groupBox1.Controls.Add(this.txtIdClaveSSA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1171, 77);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave SSA";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(110, 46);
            this.lblDescripcion.MostrarToolTip = false;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(1049, 21);
            this.lblDescripcion.TabIndex = 6;
            this.lblDescripcion.Text = "Descripcion";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdClaveSSA
            // 
            this.lblIdClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdClaveSSA.Location = new System.Drawing.Point(266, 19);
            this.lblIdClaveSSA.Name = "lblIdClaveSSA";
            this.lblIdClaveSSA.Size = new System.Drawing.Size(90, 20);
            this.lblIdClaveSSA.TabIndex = 5;
            this.lblIdClaveSSA.Text = "label2";
            this.lblIdClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIdClaveSSA.Visible = false;
            // 
            // txtIdClaveSSA
            // 
            this.txtIdClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdClaveSSA.Decimales = 2;
            this.txtIdClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtIdClaveSSA.Location = new System.Drawing.Point(110, 19);
            this.txtIdClaveSSA.MaxLength = 15;
            this.txtIdClaveSSA.Name = "txtIdClaveSSA";
            this.txtIdClaveSSA.PermitirApostrofo = false;
            this.txtIdClaveSSA.PermitirNegativos = false;
            this.txtIdClaveSSA.Size = new System.Drawing.Size(150, 20);
            this.txtIdClaveSSA.TabIndex = 0;
            this.txtIdClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdClaveSSA.TextChanged += new System.EventHandler(this.txtIdClaveSSA_TextChanged);
            this.txtIdClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdClaveSSA_KeyDown);
            this.txtIdClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdClaveSSA_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave SSA :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 45;
            this.label4.Text = "Sub-Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubFarmacias
            // 
            this.cboSubFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubFarmacias.Data = "";
            this.cboSubFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubFarmacias.Filtro = " 1 = 1";
            this.cboSubFarmacias.FormattingEnabled = true;
            this.cboSubFarmacias.ListaItemsBusqueda = 20;
            this.cboSubFarmacias.Location = new System.Drawing.Point(133, 15);
            this.cboSubFarmacias.MostrarToolTip = false;
            this.cboSubFarmacias.Name = "cboSubFarmacias";
            this.cboSubFarmacias.Size = new System.Drawing.Size(210, 21);
            this.cboSubFarmacias.TabIndex = 0;
            // 
            // FrameLocalizaciones
            // 
            this.FrameLocalizaciones.Controls.Add(this.cboPasillos);
            this.FrameLocalizaciones.Controls.Add(this.label2);
            this.FrameLocalizaciones.Controls.Add(this.cboEntrepanos);
            this.FrameLocalizaciones.Controls.Add(this.cboEstantes);
            this.FrameLocalizaciones.Controls.Add(this.label3);
            this.FrameLocalizaciones.Controls.Add(this.label5);
            this.FrameLocalizaciones.Location = new System.Drawing.Point(9, 151);
            this.FrameLocalizaciones.Name = "FrameLocalizaciones";
            this.FrameLocalizaciones.Size = new System.Drawing.Size(804, 54);
            this.FrameLocalizaciones.TabIndex = 3;
            this.FrameLocalizaciones.TabStop = false;
            this.FrameLocalizaciones.Text = "Ubicación";
            // 
            // cboPasillos
            // 
            this.cboPasillos.BackColorEnabled = System.Drawing.Color.White;
            this.cboPasillos.Data = "";
            this.cboPasillos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPasillos.Filtro = " 1 = 1";
            this.cboPasillos.FormattingEnabled = true;
            this.cboPasillos.ListaItemsBusqueda = 20;
            this.cboPasillos.Location = new System.Drawing.Point(81, 20);
            this.cboPasillos.MostrarToolTip = false;
            this.cboPasillos.Name = "cboPasillos";
            this.cboPasillos.Size = new System.Drawing.Size(175, 21);
            this.cboPasillos.TabIndex = 0;
            this.cboPasillos.SelectedIndexChanged += new System.EventHandler(this.cboPasillos_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "Rack :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEntrepanos
            // 
            this.cboEntrepanos.BackColorEnabled = System.Drawing.Color.White;
            this.cboEntrepanos.Data = "";
            this.cboEntrepanos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEntrepanos.Filtro = " 1 = 1";
            this.cboEntrepanos.FormattingEnabled = true;
            this.cboEntrepanos.ListaItemsBusqueda = 20;
            this.cboEntrepanos.Location = new System.Drawing.Point(600, 20);
            this.cboEntrepanos.MostrarToolTip = false;
            this.cboEntrepanos.Name = "cboEntrepanos";
            this.cboEntrepanos.Size = new System.Drawing.Size(175, 21);
            this.cboEntrepanos.TabIndex = 2;
            this.cboEntrepanos.SelectedIndexChanged += new System.EventHandler(this.cboEntrepanos_SelectedIndexChanged_1);
            // 
            // cboEstantes
            // 
            this.cboEstantes.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstantes.Data = "";
            this.cboEstantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstantes.Filtro = " 1 = 1";
            this.cboEstantes.FormattingEnabled = true;
            this.cboEstantes.ListaItemsBusqueda = 20;
            this.cboEstantes.Location = new System.Drawing.Point(330, 20);
            this.cboEstantes.MostrarToolTip = false;
            this.cboEstantes.Name = "cboEstantes";
            this.cboEstantes.Size = new System.Drawing.Size(175, 21);
            this.cboEstantes.TabIndex = 1;
            this.cboEstantes.SelectedIndexChanged += new System.EventHandler(this.cboEstantes_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(268, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(530, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Entrepaño :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rdoRptClave);
            this.groupBox3.Controls.Add(this.rdoRptUbicacion);
            this.groupBox3.Location = new System.Drawing.Point(9, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(424, 44);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Reporte";
            // 
            // rdoRptClave
            // 
            this.rdoRptClave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoRptClave.Checked = true;
            this.rdoRptClave.Location = new System.Drawing.Point(106, 17);
            this.rdoRptClave.Name = "rdoRptClave";
            this.rdoRptClave.Size = new System.Drawing.Size(71, 17);
            this.rdoRptClave.TabIndex = 0;
            this.rdoRptClave.TabStop = true;
            this.rdoRptClave.Text = "Por Clave";
            this.rdoRptClave.UseVisualStyleBackColor = true;
            // 
            // rdoRptUbicacion
            // 
            this.rdoRptUbicacion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdoRptUbicacion.Location = new System.Drawing.Point(227, 17);
            this.rdoRptUbicacion.Name = "rdoRptUbicacion";
            this.rdoRptUbicacion.Size = new System.Drawing.Size(92, 17);
            this.rdoRptUbicacion.TabIndex = 1;
            this.rdoRptUbicacion.Text = "Por Ubicación";
            this.rdoRptUbicacion.UseVisualStyleBackColor = true;
            // 
            // FrameUbicaciones
            // 
            this.FrameUbicaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUbicaciones.Controls.Add(this.grdProductos);
            this.FrameUbicaciones.Location = new System.Drawing.Point(9, 206);
            this.FrameUbicaciones.Name = "FrameUbicaciones";
            this.FrameUbicaciones.Size = new System.Drawing.Size(1171, 376);
            this.FrameUbicaciones.TabIndex = 4;
            this.FrameUbicaciones.TabStop = false;
            this.FrameUbicaciones.Text = "Ubicaciones de Producto";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(11, 19);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(1149, 348);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdProductos_CellDoubleClick);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 10;
            this.grdProductos_Sheet1.RowCount = 5;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA_Sal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "ClaveSSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "# Rack";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Rack";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "# Nivel";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Nivel";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "# Entrepaño";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Entrepaño";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Existencia";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "IdClaveSSA_Sal";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 91F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 20;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "ClaveSSA";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 100F;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 258F;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "# Rack";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 45F;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Rack";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 95F;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "# Nivel";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 50F;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Nivel";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 95F;
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "# Entrepaño";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "Entrepaño";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 95F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(9).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Existencia";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Width = 100F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cboSubFarmacias);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(794, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 587);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1190, 24);
            this.label11.TabIndex = 12;
            this.label11.Text = resources.GetString("label11.Text");
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rdoRptSinExistencia);
            this.groupBox4.Controls.Add(this.rdoRptTodos);
            this.groupBox4.Controls.Add(this.rdoRptConExistencia);
            this.groupBox4.Location = new System.Drawing.Point(439, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(349, 44);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Existencia";
            // 
            // rdoRptSinExistencia
            // 
            this.rdoRptSinExistencia.Location = new System.Drawing.Point(227, 17);
            this.rdoRptSinExistencia.Name = "rdoRptSinExistencia";
            this.rdoRptSinExistencia.Size = new System.Drawing.Size(91, 17);
            this.rdoRptSinExistencia.TabIndex = 2;
            this.rdoRptSinExistencia.Text = "Sin Existencia";
            this.rdoRptSinExistencia.UseVisualStyleBackColor = true;
            // 
            // rdoRptTodos
            // 
            this.rdoRptTodos.Checked = true;
            this.rdoRptTodos.Location = new System.Drawing.Point(30, 17);
            this.rdoRptTodos.Name = "rdoRptTodos";
            this.rdoRptTodos.Size = new System.Drawing.Size(55, 17);
            this.rdoRptTodos.TabIndex = 0;
            this.rdoRptTodos.TabStop = true;
            this.rdoRptTodos.Text = "Todos";
            this.rdoRptTodos.UseVisualStyleBackColor = true;
            // 
            // rdoRptConExistencia
            // 
            this.rdoRptConExistencia.Location = new System.Drawing.Point(115, 17);
            this.rdoRptConExistencia.Name = "rdoRptConExistencia";
            this.rdoRptConExistencia.Size = new System.Drawing.Size(95, 17);
            this.rdoRptConExistencia.TabIndex = 1;
            this.rdoRptConExistencia.Text = "Con Existencia";
            this.rdoRptConExistencia.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rdoRptAlmacenamiento);
            this.groupBox5.Controls.Add(this.rdoRptAmbos);
            this.groupBox5.Controls.Add(this.rdoRptPickeo);
            this.groupBox5.Location = new System.Drawing.Point(819, 151);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 54);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo de Ubicación";
            // 
            // rdoRptAlmacenamiento
            // 
            this.rdoRptAlmacenamiento.Location = new System.Drawing.Point(215, 22);
            this.rdoRptAlmacenamiento.Name = "rdoRptAlmacenamiento";
            this.rdoRptAlmacenamiento.Size = new System.Drawing.Size(103, 17);
            this.rdoRptAlmacenamiento.TabIndex = 2;
            this.rdoRptAlmacenamiento.Text = "Almacenamiento";
            this.rdoRptAlmacenamiento.UseVisualStyleBackColor = true;
            // 
            // rdoRptAmbos
            // 
            this.rdoRptAmbos.Checked = true;
            this.rdoRptAmbos.Location = new System.Drawing.Point(37, 22);
            this.rdoRptAmbos.Name = "rdoRptAmbos";
            this.rdoRptAmbos.Size = new System.Drawing.Size(58, 17);
            this.rdoRptAmbos.TabIndex = 0;
            this.rdoRptAmbos.TabStop = true;
            this.rdoRptAmbos.Text = "Ambos";
            this.rdoRptAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoRptPickeo
            // 
            this.rdoRptPickeo.Location = new System.Drawing.Point(122, 22);
            this.rdoRptPickeo.Name = "rdoRptPickeo";
            this.rdoRptPickeo.Size = new System.Drawing.Size(62, 17);
            this.rdoRptPickeo.TabIndex = 1;
            this.rdoRptPickeo.Text = "Pickeo";
            this.rdoRptPickeo.UseVisualStyleBackColor = true;
            // 
            // FrmRptUbicacionesClaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 611);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameUbicaciones);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameLocalizaciones);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRptUbicacionesClaves";
            this.Text = "Localización de Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptUbicacionesClaves_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRptUbicacionesClaves_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameLocalizaciones.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FrameUbicaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblIdClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtIdClaveSSA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboSubFarmacias;
        private System.Windows.Forms.GroupBox FrameLocalizaciones;
        private SC_ControlsCS.scComboBoxExt cboPasillos;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboEntrepanos;
        private SC_ControlsCS.scComboBoxExt cboEstantes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoRptClave;
        private System.Windows.Forms.RadioButton rdoRptUbicacion;
        private System.Windows.Forms.GroupBox FrameUbicaciones;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private SC_ControlsCS.scLabelExt lblDescripcion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExistencia;
        private System.Windows.Forms.RadioButton rdoRptSinExistencia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdoRptAlmacenamiento;
        private System.Windows.Forms.RadioButton rdoRptAmbos;
        private System.Windows.Forms.RadioButton rdoRptPickeo;
    }
}