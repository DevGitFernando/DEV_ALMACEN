namespace DllPedidosClientes.Reportes
{
    partial class FrmReportesFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportesFacturacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.grdReporte_Sheet1 = new FarPoint.Win.Spread.SheetView();
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
            this.FrameListaReportes = new System.Windows.Forms.GroupBox();
            this.cboReporte = new SC_ControlsCS.scComboBoxExt();
            this.menuBeneficiarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listadoDeBeneficiariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
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
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).BeginInit();
            this.FrameCliente.SuspendLayout();
            this.FrameListaReportes.SuspendLayout();
            this.menuBeneficiarios.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(525, 25);
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
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.chkTodos);
            this.FrameResultado.Controls.Add(this.grdReporte);
            this.FrameResultado.Location = new System.Drawing.Point(605, 126);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(77, 50);
            this.FrameResultado.TabIndex = 5;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Datos de Farmacias";
            // 
            // chkTodos
            // 
            this.chkTodos.Location = new System.Drawing.Point(313, 11);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(157, 16);
            this.chkTodos.TabIndex = 5;
            this.chkTodos.Text = "Marcar / Desmarcar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            // 
            // grdReporte
            // 
            this.grdReporte.AccessibleDescription = "grdReporte, Sheet1, Row 0, Column 0, ";
            this.grdReporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdReporte.Location = new System.Drawing.Point(7, 34);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdReporte.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdReporte_Sheet1});
            this.grdReporte.Size = new System.Drawing.Size(54, 39);
            this.grdReporte.TabIndex = 0;
            // 
            // grdReporte_Sheet1
            // 
            this.grdReporte_Sheet1.Reset();
            this.grdReporte_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdReporte_Sheet1.ColumnCount = 3;
            this.grdReporte_Sheet1.RowCount = 5;
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            textCellType4.MaxLength = 500;
            this.grdReporte_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.grdReporte_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdReporte_Sheet1.Columns.Get(0).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Width = 55F;
            this.grdReporte_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdReporte_Sheet1.Columns.Get(1).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Width = 295F;
            this.grdReporte_Sheet1.Columns.Get(2).CellType = checkBoxCellType4;
            this.grdReporte_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdReporte_Sheet1.Columns.Get(2).Locked = false;
            this.grdReporte_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Width = 59F;
            this.grdReporte_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.FrameCliente.Location = new System.Drawing.Point(10, 113);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(503, 125);
            this.FrameCliente.TabIndex = 2;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Parametros de Cliente";
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(154, 94);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(337, 21);
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
            this.lblPro.Size = new System.Drawing.Size(337, 21);
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
            this.lblSubCte.Size = new System.Drawing.Size(337, 21);
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
            this.lblCte.Size = new System.Drawing.Size(337, 21);
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
            // FrameListaReportes
            // 
            this.FrameListaReportes.Controls.Add(this.cboReporte);
            this.FrameListaReportes.Location = new System.Drawing.Point(10, 356);
            this.FrameListaReportes.Name = "FrameListaReportes";
            this.FrameListaReportes.Size = new System.Drawing.Size(503, 48);
            this.FrameListaReportes.TabIndex = 6;
            this.FrameListaReportes.TabStop = false;
            this.FrameListaReportes.Text = "Reporte para Impresión";
            this.FrameListaReportes.Enter += new System.EventHandler(this.FrameListaReportes_Enter);
            // 
            // cboReporte
            // 
            this.cboReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboReporte.ContextMenuStrip = this.menuBeneficiarios;
            this.cboReporte.Data = "";
            this.cboReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReporte.Filtro = " 1 = 1";
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.Location = new System.Drawing.Point(12, 18);
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(479, 21);
            this.cboReporte.TabIndex = 0;
            // 
            // menuBeneficiarios
            // 
            this.menuBeneficiarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listadoDeBeneficiariosToolStripMenuItem});
            this.menuBeneficiarios.Name = "menuBeneficiarios";
            this.menuBeneficiarios.Size = new System.Drawing.Size(199, 26);
            // 
            // listadoDeBeneficiariosToolStripMenuItem
            // 
            this.listadoDeBeneficiariosToolStripMenuItem.Name = "listadoDeBeneficiariosToolStripMenuItem";
            this.listadoDeBeneficiariosToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.listadoDeBeneficiariosToolStripMenuItem.Text = "Listado de Beneficiarios";
            this.listadoDeBeneficiariosToolStripMenuItem.Click += new System.EventHandler(this.listadoDeBeneficiariosToolStripMenuItem_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblFarmacia);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.txtFarmacia);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(10, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(503, 79);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(89, 124);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(402, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(25, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(89, 20);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(402, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(31, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMedicamentoNOSP);
            this.FrameInsumos.Controls.Add(this.rdoInsumoMedicamentoSP);
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(10, 244);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(175, 109);
            this.FrameInsumos.TabIndex = 3;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMedicamentoNOSP
            // 
            this.rdoInsumoMedicamentoNOSP.Location = new System.Drawing.Point(36, 68);
            this.rdoInsumoMedicamentoNOSP.Name = "rdoInsumoMedicamentoNOSP";
            this.rdoInsumoMedicamentoNOSP.Size = new System.Drawing.Size(120, 17);
            this.rdoInsumoMedicamentoNOSP.TabIndex = 3;
            this.rdoInsumoMedicamentoNOSP.TabStop = true;
            this.rdoInsumoMedicamentoNOSP.Text = "No Seguro Popular";
            this.rdoInsumoMedicamentoNOSP.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamentoSP
            // 
            this.rdoInsumoMedicamentoSP.Location = new System.Drawing.Point(36, 50);
            this.rdoInsumoMedicamentoSP.Name = "rdoInsumoMedicamentoSP";
            this.rdoInsumoMedicamentoSP.Size = new System.Drawing.Size(120, 17);
            this.rdoInsumoMedicamentoSP.TabIndex = 2;
            this.rdoInsumoMedicamentoSP.TabStop = true;
            this.rdoInsumoMedicamentoSP.Text = "Seguro Popular";
            this.rdoInsumoMedicamentoSP.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(16, 86);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumoMatCuracion.TabIndex = 4;
            this.rdoInsumoMatCuracion.TabStop = true;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(16, 15);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(16, 34);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(122, 15);
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
            this.FrameDispensacion.Location = new System.Drawing.Point(191, 244);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(156, 109);
            this.FrameDispensacion.TabIndex = 4;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(25, 23);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(25, 75);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(25, 49);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.TabStop = true;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(353, 244);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(160, 109);
            this.FrameFechas.TabIndex = 5;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(61, 60);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 32);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(61, 29);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(157, 47);
            this.lblFarmacia.MostrarToolTip = true;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(334, 20);
            this.lblFarmacia.TabIndex = 25;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(89, 47);
            this.txtFarmacia.MaxLength = 4;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(62, 20);
            this.txtFarmacia.TabIndex = 1;
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmacia_KeyDown);
            this.txtFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmacia_Validating);
            // 
            // FrmReportesFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 412);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameListaReportes);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReportesFacturacion";
            this.Text = "Reportes Administrativos de Validación y Facturación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReportesFacturacion_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReportesFacturacion_FormClosing);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).EndInit();
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.FrameListaReportes.ResumeLayout(false);
            this.menuBeneficiarios.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.FrameInsumos.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameListaReportes;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private FarPoint.Win.Spread.SheetView grdReporte_Sheet1;
        private SC_ControlsCS.scComboBoxExt cboReporte;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamentoNOSP;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamentoSP;
        private System.Windows.Forms.RadioButton rdoInsumoMatCuracion;
        private System.Windows.Forms.RadioButton rdoInsumosAmbos;
        private System.Windows.Forms.RadioButton rdoInsumosMedicamento;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip menuBeneficiarios;
        private System.Windows.Forms.ToolStripMenuItem listadoDeBeneficiariosToolStripMenuItem;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
    }
}