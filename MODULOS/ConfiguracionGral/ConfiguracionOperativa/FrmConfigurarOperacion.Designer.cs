namespace Configuracion.ConfiguracionOperativa
{
    partial class FrmConfigurarOperacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarOperacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacia_Base = new SC_ControlsCS.scComboBoxExt();
            this.cboEstado_Base = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFarmaciaFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFarmaciaInicial = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFarmacia_Destino = new SC_ControlsCS.scComboBoxExt();
            this.cboEstado_Destino = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameConfiguraciones_01_Generales = new System.Windows.Forms.GroupBox();
            this.chk_10_Contraseñas = new System.Windows.Forms.CheckBox();
            this.chk_08_Permisos = new System.Windows.Forms.CheckBox();
            this.chk_09_Productos = new System.Windows.Forms.CheckBox();
            this.chk_07_Usuarios = new System.Windows.Forms.CheckBox();
            this.chk_06_Personal = new System.Windows.Forms.CheckBox();
            this.chk_05_Servicios_y_Areas = new System.Windows.Forms.CheckBox();
            this.chk_04_Programas_y_SubProgramas = new System.Windows.Forms.CheckBox();
            this.chk_03_Clientes_y_SubClientes = new System.Windows.Forms.CheckBox();
            this.chk_02_MovimientosDeInventario = new System.Windows.Forms.CheckBox();
            this.chk_01_SubFarmacias = new System.Windows.Forms.CheckBox();
            this.FrameCopiarConfiguracionBase = new System.Windows.Forms.GroupBox();
            this.chk_11_ConfiguracionBase = new System.Windows.Forms.CheckBox();
            this.txtDesplazamientoFarmacias = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.FrameConfiguraciones_02_Base = new System.Windows.Forms.GroupBox();
            this.chk_16_ProveedoresVales = new System.Windows.Forms.CheckBox();
            this.chk_15_FarmaciasConvenio = new System.Windows.Forms.CheckBox();
            this.chk_14_EmpresaRelacionada = new System.Windows.Forms.CheckBox();
            this.chk_13_Conexiones = new System.Windows.Forms.CheckBox();
            this.chk_12_Perfiles = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.FrameConfiguraciones_01_Generales.SuspendLayout();
            this.FrameCopiarConfiguracionBase.SuspendLayout();
            this.FrameConfiguraciones_02_Base.SuspendLayout();
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
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(873, 25);
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
            this.btnGenerarDocumentos.Text = "Generar validaciónes";
            this.btnGenerarDocumentos.Click += new System.EventHandler(this.btnGenerarDocumentos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(11, 277);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(850, 427);
            this.FrameUnidades.TabIndex = 0;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(712, 0);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(126, 17);
            this.chkMarcarDesmarcar.TabIndex = 0;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(190, 150);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(472, 38);
            this.FrameProceso.TabIndex = 2;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 18);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(443, 12);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(10, 17);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(830, 401);
            this.grdUnidades.TabIndex = 1;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 4;
            this.grdUnidades_Sheet1.RowCount = 20;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Unidad";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 100F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Nombre Unidad";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 480F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType2;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(3).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 80F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboFarmacia_Base);
            this.groupBox5.Controls.Add(this.cboEstado_Base);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(11, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(422, 81);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Operación base";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacia_Base
            // 
            this.cboFarmacia_Base.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacia_Base.Data = "";
            this.cboFarmacia_Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacia_Base.Filtro = " 1 = 1";
            this.cboFarmacia_Base.FormattingEnabled = true;
            this.cboFarmacia_Base.ListaItemsBusqueda = 20;
            this.cboFarmacia_Base.Location = new System.Drawing.Point(76, 50);
            this.cboFarmacia_Base.MostrarToolTip = false;
            this.cboFarmacia_Base.Name = "cboFarmacia_Base";
            this.cboFarmacia_Base.Size = new System.Drawing.Size(332, 21);
            this.cboFarmacia_Base.TabIndex = 1;
            // 
            // cboEstado_Base
            // 
            this.cboEstado_Base.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstado_Base.Data = "";
            this.cboEstado_Base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado_Base.Filtro = " 1 = 1";
            this.cboEstado_Base.FormattingEnabled = true;
            this.cboEstado_Base.ListaItemsBusqueda = 20;
            this.cboEstado_Base.Location = new System.Drawing.Point(76, 22);
            this.cboEstado_Base.MostrarToolTip = false;
            this.cboEstado_Base.Name = "cboEstado_Base";
            this.cboEstado_Base.Size = new System.Drawing.Size(332, 21);
            this.cboEstado_Base.TabIndex = 0;
            this.cboEstado_Base.SelectedIndexChanged += new System.EventHandler(this.cboEstado_Base_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FrameFolios);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboFarmacia_Destino);
            this.groupBox1.Controls.Add(this.cboEstado_Destino);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(440, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operación destino";
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.txtFarmaciaFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFarmaciaInicial);
            this.FrameFolios.Controls.Add(this.label3);
            this.FrameFolios.Location = new System.Drawing.Point(76, 75);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(332, 45);
            this.FrameFolios.TabIndex = 24;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Farmacias";
            // 
            // txtFarmaciaFinal
            // 
            this.txtFarmaciaFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmaciaFinal.Decimales = 2;
            this.txtFarmaciaFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmaciaFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFarmaciaFinal.Location = new System.Drawing.Point(232, 17);
            this.txtFarmaciaFinal.MaxLength = 4;
            this.txtFarmaciaFinal.Name = "txtFarmaciaFinal";
            this.txtFarmaciaFinal.PermitirApostrofo = false;
            this.txtFarmaciaFinal.PermitirNegativos = false;
            this.txtFarmaciaFinal.Size = new System.Drawing.Size(85, 20);
            this.txtFarmaciaFinal.TabIndex = 2;
            this.txtFarmaciaFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(182, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "Hasta :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFarmaciaInicial
            // 
            this.txtFarmaciaInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmaciaInicial.Decimales = 2;
            this.txtFarmaciaInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmaciaInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFarmaciaInicial.Location = new System.Drawing.Point(90, 17);
            this.txtFarmaciaInicial.MaxLength = 4;
            this.txtFarmaciaInicial.Name = "txtFarmaciaInicial";
            this.txtFarmaciaInicial.PermitirApostrofo = false;
            this.txtFarmaciaInicial.PermitirNegativos = false;
            this.txtFarmaciaInicial.Size = new System.Drawing.Size(85, 20);
            this.txtFarmaciaInicial.TabIndex = 1;
            this.txtFarmaciaInicial.Text = "01234567";
            this.txtFarmaciaInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(40, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "Desde :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacia_Destino
            // 
            this.cboFarmacia_Destino.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacia_Destino.Data = "";
            this.cboFarmacia_Destino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacia_Destino.Filtro = " 1 = 1";
            this.cboFarmacia_Destino.FormattingEnabled = true;
            this.cboFarmacia_Destino.ListaItemsBusqueda = 20;
            this.cboFarmacia_Destino.Location = new System.Drawing.Point(76, 50);
            this.cboFarmacia_Destino.MostrarToolTip = false;
            this.cboFarmacia_Destino.Name = "cboFarmacia_Destino";
            this.cboFarmacia_Destino.Size = new System.Drawing.Size(332, 21);
            this.cboFarmacia_Destino.TabIndex = 1;
            // 
            // cboEstado_Destino
            // 
            this.cboEstado_Destino.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstado_Destino.Data = "";
            this.cboEstado_Destino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado_Destino.Filtro = " 1 = 1";
            this.cboEstado_Destino.FormattingEnabled = true;
            this.cboEstado_Destino.ListaItemsBusqueda = 20;
            this.cboEstado_Destino.Location = new System.Drawing.Point(76, 22);
            this.cboEstado_Destino.MostrarToolTip = false;
            this.cboEstado_Destino.Name = "cboEstado_Destino";
            this.cboEstado_Destino.Size = new System.Drawing.Size(332, 21);
            this.cboEstado_Destino.TabIndex = 0;
            this.cboEstado_Destino.SelectedIndexChanged += new System.EventHandler(this.cboEstado_Destino_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Estado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameConfiguraciones_01_Generales
            // 
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_10_Contraseñas);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_08_Permisos);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_09_Productos);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_07_Usuarios);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_06_Personal);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_05_Servicios_y_Areas);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_04_Programas_y_SubProgramas);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_03_Clientes_y_SubClientes);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_02_MovimientosDeInventario);
            this.FrameConfiguraciones_01_Generales.Controls.Add(this.chk_01_SubFarmacias);
            this.FrameConfiguraciones_01_Generales.Location = new System.Drawing.Point(11, 155);
            this.FrameConfiguraciones_01_Generales.Margin = new System.Windows.Forms.Padding(2);
            this.FrameConfiguraciones_01_Generales.Name = "FrameConfiguraciones_01_Generales";
            this.FrameConfiguraciones_01_Generales.Padding = new System.Windows.Forms.Padding(2);
            this.FrameConfiguraciones_01_Generales.Size = new System.Drawing.Size(850, 69);
            this.FrameConfiguraciones_01_Generales.TabIndex = 4;
            this.FrameConfiguraciones_01_Generales.TabStop = false;
            this.FrameConfiguraciones_01_Generales.Text = "Configuraciones generales";
            // 
            // chk_10_Contraseñas
            // 
            this.chk_10_Contraseñas.Location = new System.Drawing.Point(718, 41);
            this.chk_10_Contraseñas.Margin = new System.Windows.Forms.Padding(2);
            this.chk_10_Contraseñas.Name = "chk_10_Contraseñas";
            this.chk_10_Contraseñas.Size = new System.Drawing.Size(114, 20);
            this.chk_10_Contraseñas.TabIndex = 9;
            this.chk_10_Contraseñas.Text = "Contraseñas";
            this.chk_10_Contraseñas.UseVisualStyleBackColor = true;
            // 
            // chk_08_Permisos
            // 
            this.chk_08_Permisos.Location = new System.Drawing.Point(584, 41);
            this.chk_08_Permisos.Margin = new System.Windows.Forms.Padding(2);
            this.chk_08_Permisos.Name = "chk_08_Permisos";
            this.chk_08_Permisos.Size = new System.Drawing.Size(129, 20);
            this.chk_08_Permisos.TabIndex = 8;
            this.chk_08_Permisos.Text = "Permisos";
            this.chk_08_Permisos.UseVisualStyleBackColor = true;
            // 
            // chk_09_Productos
            // 
            this.chk_09_Productos.Location = new System.Drawing.Point(718, 17);
            this.chk_09_Productos.Margin = new System.Windows.Forms.Padding(2);
            this.chk_09_Productos.Name = "chk_09_Productos";
            this.chk_09_Productos.Size = new System.Drawing.Size(114, 20);
            this.chk_09_Productos.TabIndex = 7;
            this.chk_09_Productos.Text = "Productos";
            this.chk_09_Productos.UseVisualStyleBackColor = true;
            // 
            // chk_07_Usuarios
            // 
            this.chk_07_Usuarios.Location = new System.Drawing.Point(584, 17);
            this.chk_07_Usuarios.Margin = new System.Windows.Forms.Padding(2);
            this.chk_07_Usuarios.Name = "chk_07_Usuarios";
            this.chk_07_Usuarios.Size = new System.Drawing.Size(129, 20);
            this.chk_07_Usuarios.TabIndex = 6;
            this.chk_07_Usuarios.Text = "Usuarios";
            this.chk_07_Usuarios.UseVisualStyleBackColor = true;
            // 
            // chk_06_Personal
            // 
            this.chk_06_Personal.Location = new System.Drawing.Point(398, 41);
            this.chk_06_Personal.Margin = new System.Windows.Forms.Padding(2);
            this.chk_06_Personal.Name = "chk_06_Personal";
            this.chk_06_Personal.Size = new System.Drawing.Size(165, 20);
            this.chk_06_Personal.TabIndex = 5;
            this.chk_06_Personal.Text = "Personal";
            this.chk_06_Personal.UseVisualStyleBackColor = true;
            // 
            // chk_05_Servicios_y_Areas
            // 
            this.chk_05_Servicios_y_Areas.Location = new System.Drawing.Point(398, 17);
            this.chk_05_Servicios_y_Areas.Margin = new System.Windows.Forms.Padding(2);
            this.chk_05_Servicios_y_Areas.Name = "chk_05_Servicios_y_Areas";
            this.chk_05_Servicios_y_Areas.Size = new System.Drawing.Size(165, 20);
            this.chk_05_Servicios_y_Areas.TabIndex = 4;
            this.chk_05_Servicios_y_Areas.Text = "Servicios y Áreas";
            this.chk_05_Servicios_y_Areas.UseVisualStyleBackColor = true;
            // 
            // chk_04_Programas_y_SubProgramas
            // 
            this.chk_04_Programas_y_SubProgramas.Location = new System.Drawing.Point(213, 41);
            this.chk_04_Programas_y_SubProgramas.Margin = new System.Windows.Forms.Padding(2);
            this.chk_04_Programas_y_SubProgramas.Name = "chk_04_Programas_y_SubProgramas";
            this.chk_04_Programas_y_SubProgramas.Size = new System.Drawing.Size(165, 20);
            this.chk_04_Programas_y_SubProgramas.TabIndex = 3;
            this.chk_04_Programas_y_SubProgramas.Text = "Programas y Sub-Programas";
            this.chk_04_Programas_y_SubProgramas.UseVisualStyleBackColor = true;
            // 
            // chk_03_Clientes_y_SubClientes
            // 
            this.chk_03_Clientes_y_SubClientes.Location = new System.Drawing.Point(213, 17);
            this.chk_03_Clientes_y_SubClientes.Margin = new System.Windows.Forms.Padding(2);
            this.chk_03_Clientes_y_SubClientes.Name = "chk_03_Clientes_y_SubClientes";
            this.chk_03_Clientes_y_SubClientes.Size = new System.Drawing.Size(165, 20);
            this.chk_03_Clientes_y_SubClientes.TabIndex = 2;
            this.chk_03_Clientes_y_SubClientes.Text = "Clientes y Sub-Clientes";
            this.chk_03_Clientes_y_SubClientes.UseVisualStyleBackColor = true;
            // 
            // chk_02_MovimientosDeInventario
            // 
            this.chk_02_MovimientosDeInventario.Location = new System.Drawing.Point(28, 41);
            this.chk_02_MovimientosDeInventario.Margin = new System.Windows.Forms.Padding(2);
            this.chk_02_MovimientosDeInventario.Name = "chk_02_MovimientosDeInventario";
            this.chk_02_MovimientosDeInventario.Size = new System.Drawing.Size(165, 20);
            this.chk_02_MovimientosDeInventario.TabIndex = 1;
            this.chk_02_MovimientosDeInventario.Text = "Movimientos de inventario";
            this.chk_02_MovimientosDeInventario.UseVisualStyleBackColor = true;
            // 
            // chk_01_SubFarmacias
            // 
            this.chk_01_SubFarmacias.Location = new System.Drawing.Point(28, 17);
            this.chk_01_SubFarmacias.Margin = new System.Windows.Forms.Padding(2);
            this.chk_01_SubFarmacias.Name = "chk_01_SubFarmacias";
            this.chk_01_SubFarmacias.Size = new System.Drawing.Size(165, 20);
            this.chk_01_SubFarmacias.TabIndex = 0;
            this.chk_01_SubFarmacias.Text = "Sub-Farmacias";
            this.chk_01_SubFarmacias.UseVisualStyleBackColor = true;
            // 
            // FrameCopiarConfiguracionBase
            // 
            this.FrameCopiarConfiguracionBase.Controls.Add(this.chk_11_ConfiguracionBase);
            this.FrameCopiarConfiguracionBase.Controls.Add(this.txtDesplazamientoFarmacias);
            this.FrameCopiarConfiguracionBase.Controls.Add(this.label7);
            this.FrameCopiarConfiguracionBase.Location = new System.Drawing.Point(11, 106);
            this.FrameCopiarConfiguracionBase.Name = "FrameCopiarConfiguracionBase";
            this.FrameCopiarConfiguracionBase.Size = new System.Drawing.Size(422, 45);
            this.FrameCopiarConfiguracionBase.TabIndex = 25;
            this.FrameCopiarConfiguracionBase.TabStop = false;
            this.FrameCopiarConfiguracionBase.Text = "Copiar configuración base";
            // 
            // chk_11_ConfiguracionBase
            // 
            this.chk_11_ConfiguracionBase.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_11_ConfiguracionBase.Location = new System.Drawing.Point(205, 15);
            this.chk_11_ConfiguracionBase.Margin = new System.Windows.Forms.Padding(2);
            this.chk_11_ConfiguracionBase.Name = "chk_11_ConfiguracionBase";
            this.chk_11_ConfiguracionBase.Size = new System.Drawing.Size(203, 20);
            this.chk_11_ConfiguracionBase.TabIndex = 26;
            this.chk_11_ConfiguracionBase.Text = "Habiliar copia de configuración base";
            this.chk_11_ConfiguracionBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_11_ConfiguracionBase.UseVisualStyleBackColor = true;
            // 
            // txtDesplazamientoFarmacias
            // 
            this.txtDesplazamientoFarmacias.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDesplazamientoFarmacias.Decimales = 2;
            this.txtDesplazamientoFarmacias.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtDesplazamientoFarmacias.ForeColor = System.Drawing.Color.Black;
            this.txtDesplazamientoFarmacias.Location = new System.Drawing.Point(106, 16);
            this.txtDesplazamientoFarmacias.MaxLength = 4;
            this.txtDesplazamientoFarmacias.Name = "txtDesplazamientoFarmacias";
            this.txtDesplazamientoFarmacias.PermitirApostrofo = false;
            this.txtDesplazamientoFarmacias.PermitirNegativos = false;
            this.txtDesplazamientoFarmacias.Size = new System.Drawing.Size(85, 20);
            this.txtDesplazamientoFarmacias.TabIndex = 1;
            this.txtDesplazamientoFarmacias.Text = "01234567";
            this.txtDesplazamientoFarmacias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 33;
            this.label7.Text = "Desplazamiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameConfiguraciones_02_Base
            // 
            this.FrameConfiguraciones_02_Base.Controls.Add(this.chk_16_ProveedoresVales);
            this.FrameConfiguraciones_02_Base.Controls.Add(this.chk_15_FarmaciasConvenio);
            this.FrameConfiguraciones_02_Base.Controls.Add(this.chk_14_EmpresaRelacionada);
            this.FrameConfiguraciones_02_Base.Controls.Add(this.chk_13_Conexiones);
            this.FrameConfiguraciones_02_Base.Controls.Add(this.chk_12_Perfiles);
            this.FrameConfiguraciones_02_Base.Location = new System.Drawing.Point(12, 224);
            this.FrameConfiguraciones_02_Base.Margin = new System.Windows.Forms.Padding(2);
            this.FrameConfiguraciones_02_Base.Name = "FrameConfiguraciones_02_Base";
            this.FrameConfiguraciones_02_Base.Padding = new System.Windows.Forms.Padding(2);
            this.FrameConfiguraciones_02_Base.Size = new System.Drawing.Size(850, 48);
            this.FrameConfiguraciones_02_Base.TabIndex = 26;
            this.FrameConfiguraciones_02_Base.TabStop = false;
            this.FrameConfiguraciones_02_Base.Text = "Configuraciones base";
            // 
            // chk_16_ProveedoresVales
            // 
            this.chk_16_ProveedoresVales.Location = new System.Drawing.Point(718, 17);
            this.chk_16_ProveedoresVales.Margin = new System.Windows.Forms.Padding(2);
            this.chk_16_ProveedoresVales.Name = "chk_16_ProveedoresVales";
            this.chk_16_ProveedoresVales.Size = new System.Drawing.Size(114, 20);
            this.chk_16_ProveedoresVales.TabIndex = 7;
            this.chk_16_ProveedoresVales.Text = "Proveedores vales";
            this.chk_16_ProveedoresVales.UseVisualStyleBackColor = true;
            // 
            // chk_15_FarmaciasConvenio
            // 
            this.chk_15_FarmaciasConvenio.Location = new System.Drawing.Point(584, 17);
            this.chk_15_FarmaciasConvenio.Margin = new System.Windows.Forms.Padding(2);
            this.chk_15_FarmaciasConvenio.Name = "chk_15_FarmaciasConvenio";
            this.chk_15_FarmaciasConvenio.Size = new System.Drawing.Size(129, 20);
            this.chk_15_FarmaciasConvenio.TabIndex = 6;
            this.chk_15_FarmaciasConvenio.Text = "Farmacias convenio";
            this.chk_15_FarmaciasConvenio.UseVisualStyleBackColor = true;
            // 
            // chk_14_EmpresaRelacionada
            // 
            this.chk_14_EmpresaRelacionada.Location = new System.Drawing.Point(398, 17);
            this.chk_14_EmpresaRelacionada.Margin = new System.Windows.Forms.Padding(2);
            this.chk_14_EmpresaRelacionada.Name = "chk_14_EmpresaRelacionada";
            this.chk_14_EmpresaRelacionada.Size = new System.Drawing.Size(165, 20);
            this.chk_14_EmpresaRelacionada.TabIndex = 4;
            this.chk_14_EmpresaRelacionada.Text = "Empresa relacionada";
            this.chk_14_EmpresaRelacionada.UseVisualStyleBackColor = true;
            // 
            // chk_13_Conexiones
            // 
            this.chk_13_Conexiones.Location = new System.Drawing.Point(213, 17);
            this.chk_13_Conexiones.Margin = new System.Windows.Forms.Padding(2);
            this.chk_13_Conexiones.Name = "chk_13_Conexiones";
            this.chk_13_Conexiones.Size = new System.Drawing.Size(165, 20);
            this.chk_13_Conexiones.TabIndex = 2;
            this.chk_13_Conexiones.Text = "Conexiones";
            this.chk_13_Conexiones.UseVisualStyleBackColor = true;
            // 
            // chk_12_Perfiles
            // 
            this.chk_12_Perfiles.Location = new System.Drawing.Point(28, 17);
            this.chk_12_Perfiles.Margin = new System.Windows.Forms.Padding(2);
            this.chk_12_Perfiles.Name = "chk_12_Perfiles";
            this.chk_12_Perfiles.Size = new System.Drawing.Size(165, 20);
            this.chk_12_Perfiles.TabIndex = 0;
            this.chk_12_Perfiles.Text = "Perfiles de atención";
            this.chk_12_Perfiles.UseVisualStyleBackColor = true;
            // 
            // FrmConfigurarOperacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 709);
            this.Controls.Add(this.FrameConfiguraciones_02_Base);
            this.Controls.Add(this.FrameCopiarConfiguracionBase);
            this.Controls.Add(this.FrameConfiguraciones_01_Generales);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmConfigurarOperacion";
            this.Text = "Configuración de operaciones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigurarOperacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.FrameConfiguraciones_01_Generales.ResumeLayout(false);
            this.FrameCopiarConfiguracionBase.ResumeLayout(false);
            this.FrameCopiarConfiguracionBase.PerformLayout();
            this.FrameConfiguraciones_02_Base.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacia_Base;
        private SC_ControlsCS.scComboBoxExt cboEstado_Base;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboFarmacia_Destino;
        private SC_ControlsCS.scComboBoxExt cboEstado_Destino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameConfiguraciones_01_Generales;
        private System.Windows.Forms.CheckBox chk_09_Productos;
        private System.Windows.Forms.CheckBox chk_07_Usuarios;
        private System.Windows.Forms.CheckBox chk_06_Personal;
        private System.Windows.Forms.CheckBox chk_05_Servicios_y_Areas;
        private System.Windows.Forms.CheckBox chk_04_Programas_y_SubProgramas;
        private System.Windows.Forms.CheckBox chk_03_Clientes_y_SubClientes;
        private System.Windows.Forms.CheckBox chk_02_MovimientosDeInventario;
        private System.Windows.Forms.CheckBox chk_01_SubFarmacias;
        private System.Windows.Forms.CheckBox chk_08_Permisos;
        private System.Windows.Forms.CheckBox chk_10_Contraseñas;
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFarmaciaFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFarmaciaInicial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameCopiarConfiguracionBase;
        private SC_ControlsCS.scTextBoxExt txtDesplazamientoFarmacias;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chk_11_ConfiguracionBase;
        private System.Windows.Forms.GroupBox FrameConfiguraciones_02_Base;
        private System.Windows.Forms.CheckBox chk_16_ProveedoresVales;
        private System.Windows.Forms.CheckBox chk_15_FarmaciasConvenio;
        private System.Windows.Forms.CheckBox chk_14_EmpresaRelacionada;
        private System.Windows.Forms.CheckBox chk_13_Conexiones;
        private System.Windows.Forms.CheckBox chk_12_Perfiles;
    }
}