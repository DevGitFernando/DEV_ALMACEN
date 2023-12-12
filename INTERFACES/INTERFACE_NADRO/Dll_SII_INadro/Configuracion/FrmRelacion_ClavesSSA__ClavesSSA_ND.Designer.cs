namespace Dll_SII_INadro.Configuracion
{
    partial class FrmRelacion_ClavesSSA__ClavesSSA_ND
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelacion_ClavesSSA__ClavesSSA_ND));
            FarPoint.Win.Spread.CellType.TextCellType textCellType25 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType26 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType27 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType28 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkStatusRelacion = new System.Windows.Forms.CheckBox();
            this.lblClaveSSA_Relacionada = new SC_ControlsCS.scLabelExt();
            this.lblClaveSSA = new SC_ControlsCS.scLabelExt();
            this.txtClaveSSA_Relacionda = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.txtBusqueda = new SC_ControlsCS.scTextBoxExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.cboFiltros = new SC_ControlsCS.scComboBoxExt();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGenerarRelacionDeClaves = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnGenerarRelacionDeClaves});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1035, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkStatusRelacion);
            this.groupBox1.Controls.Add(this.lblClaveSSA_Relacionada);
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.txtClaveSSA_Relacionda);
            this.groupBox1.Controls.Add(this.scLabelExt4);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.scLabelExt3);
            this.groupBox1.Controls.Add(this.lblEstado);
            this.groupBox1.Controls.Add(this.txtIdEstado);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 94);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FrameInformacion";
            // 
            // chkStatusRelacion
            // 
            this.chkStatusRelacion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStatusRelacion.Location = new System.Drawing.Point(856, 16);
            this.chkStatusRelacion.Name = "chkStatusRelacion";
            this.chkStatusRelacion.Size = new System.Drawing.Size(150, 24);
            this.chkStatusRelacion.TabIndex = 3;
            this.chkStatusRelacion.Text = "Relación activa";
            this.chkStatusRelacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStatusRelacion.UseVisualStyleBackColor = true;
            // 
            // lblClaveSSA_Relacionada
            // 
            this.lblClaveSSA_Relacionada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA_Relacionada.Location = new System.Drawing.Point(318, 68);
            this.lblClaveSSA_Relacionada.MostrarToolTip = false;
            this.lblClaveSSA_Relacionada.Name = "lblClaveSSA_Relacionada";
            this.lblClaveSSA_Relacionada.Size = new System.Drawing.Size(688, 20);
            this.lblClaveSSA_Relacionada.TabIndex = 8;
            this.lblClaveSSA_Relacionada.Text = "scLabelExt2";
            this.lblClaveSSA_Relacionada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(318, 41);
            this.lblClaveSSA.MostrarToolTip = false;
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(688, 20);
            this.lblClaveSSA.TabIndex = 7;
            this.lblClaveSSA.Text = "scLabelExt2";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClaveSSA_Relacionda
            // 
            this.txtClaveSSA_Relacionda.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA_Relacionda.Decimales = 2;
            this.txtClaveSSA_Relacionda.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA_Relacionda.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA_Relacionda.Location = new System.Drawing.Point(151, 68);
            this.txtClaveSSA_Relacionda.MaxLength = 20;
            this.txtClaveSSA_Relacionda.Name = "txtClaveSSA_Relacionda";
            this.txtClaveSSA_Relacionda.PermitirApostrofo = false;
            this.txtClaveSSA_Relacionda.PermitirNegativos = false;
            this.txtClaveSSA_Relacionda.Size = new System.Drawing.Size(161, 20);
            this.txtClaveSSA_Relacionda.TabIndex = 2;
            this.txtClaveSSA_Relacionda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA_Relacionda.TextChanged += new System.EventHandler(this.txtClaveSSA_Relacionda_TextChanged);
            this.txtClaveSSA_Relacionda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_Relacionda_KeyDown);
            this.txtClaveSSA_Relacionda.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Relacionda_Validating);
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(10, 68);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(138, 20);
            this.scLabelExt4.TabIndex = 5;
            this.scLabelExt4.Text = "Clave SSA Relacionada : ";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(151, 42);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(161, 20);
            this.txtClaveSSA.TabIndex = 1;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.TextChanged += new System.EventHandler(this.txtClaveSSA_TextChanged);
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.Location = new System.Drawing.Point(10, 42);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(138, 20);
            this.scLabelExt3.TabIndex = 3;
            this.scLabelExt3.Text = "Clave SSA : ";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(221, 16);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(457, 20);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "scLabelExt2";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(151, 16);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(64, 20);
            this.txtIdEstado.TabIndex = 0;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(10, 16);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(138, 20);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Estado : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBusqueda);
            this.groupBox2.Controls.Add(this.txtBusqueda);
            this.groupBox2.Controls.Add(this.scLabelExt2);
            this.groupBox2.Controls.Add(this.cboFiltros);
            this.groupBox2.Controls.Add(this.grdClaves);
            this.groupBox2.Location = new System.Drawing.Point(12, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1016, 372);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado de Claves SSA relacionadas";
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Location = new System.Drawing.Point(934, 18);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(72, 23);
            this.btnBusqueda.TabIndex = 2;
            this.btnBusqueda.Text = "Búsqueda";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBusqueda.Decimales = 2;
            this.txtBusqueda.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtBusqueda.ForeColor = System.Drawing.Color.Black;
            this.txtBusqueda.Location = new System.Drawing.Point(350, 21);
            this.txtBusqueda.MaxLength = 50;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.PermitirApostrofo = false;
            this.txtBusqueda.PermitirNegativos = false;
            this.txtBusqueda.Size = new System.Drawing.Size(578, 20);
            this.txtBusqueda.TabIndex = 1;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(21, 22);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(72, 20);
            this.scLabelExt2.TabIndex = 2;
            this.scLabelExt2.Text = "Filtrar por : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFiltros
            // 
            this.cboFiltros.BackColorEnabled = System.Drawing.Color.White;
            this.cboFiltros.Data = "";
            this.cboFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltros.Filtro = " 1 = 1";
            this.cboFiltros.FormattingEnabled = true;
            this.cboFiltros.ListaItemsBusqueda = 20;
            this.cboFiltros.Location = new System.Drawing.Point(96, 21);
            this.cboFiltros.MostrarToolTip = false;
            this.cboFiltros.Name = "cboFiltros";
            this.cboFiltros.Size = new System.Drawing.Size(248, 21);
            this.cboFiltros.TabIndex = 0;
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(10, 46);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(996, 316);
            this.grdClaves.TabIndex = 3;
            this.grdClaves.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdClaves_CellDoubleClick);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 7;
            this.grdClaves_Sheet1.RowCount = 10;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdClaveSSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave SSA Relacionada";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción Clave SSA Relacionada";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Status";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Status";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType25;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "IdClaveSSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType26;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 110F;
            textCellType27.MaxLength = 5000;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType27;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Descripción Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 310F;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = textCellType28;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Clave SSA Relacionada";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 110F;
            textCellType29.MaxLength = 5000;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = textCellType29;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Descripción Clave SSA Relacionada";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 310F;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = checkBoxCellType5;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Status";
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(6).CellType = textCellType30;
            this.grdClaves_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Label = "Status";
            this.grdClaves_Sheet1.Columns.Get(6).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Width = 100F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 506);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1035, 24);
            this.label11.TabIndex = 18;
            this.label11.Text = "Doble clic sobre el renglon para modificaciones";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGenerarRelacionDeClaves
            // 
            this.btnGenerarRelacionDeClaves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarRelacionDeClaves.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarRelacionDeClaves.Image")));
            this.btnGenerarRelacionDeClaves.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarRelacionDeClaves.Name = "btnGenerarRelacionDeClaves";
            this.btnGenerarRelacionDeClaves.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarRelacionDeClaves.Text = "Generar información de Claves OPM";
            this.btnGenerarRelacionDeClaves.Click += new System.EventHandler(this.btnGenerarRelacionDeClaves_Click);
            // 
            // FrmRelacion_ClavesSSA__ClavesSSA_ND
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 530);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmRelacion_ClavesSSA__ClavesSSA_ND";
            this.Text = "Relación de Claves SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRelacion_ClavesSSA__ClavesSSA_ND_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private SC_ControlsCS.scComboBoxExt cboFiltros;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scTextBoxExt txtBusqueda;
        private System.Windows.Forms.Button btnBusqueda;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA_Relacionda;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private SC_ControlsCS.scLabelExt lblClaveSSA_Relacionada;
        private SC_ControlsCS.scLabelExt lblClaveSSA;
        private System.Windows.Forms.CheckBox chkStatusRelacion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripButton btnGenerarRelacionDeClaves;
    }
}