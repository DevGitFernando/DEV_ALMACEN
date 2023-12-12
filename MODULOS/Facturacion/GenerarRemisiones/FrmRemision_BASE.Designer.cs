namespace Facturacion.GenerarRemisiones
{
    partial class FrmRemision_BASE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemision_BASE));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarRemisiones = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.txtConcepto = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.txtRubro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.rdoFechaCaptura = new System.Windows.Forms.RadioButton();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.rdoFechaReceta = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameTiposDeUnidades = new System.Windows.Forms.GroupBox();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipoUnidades = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControlFacturacion = new SC_ControlsCS.scTabControlExt();
            this.tabPagParametros = new System.Windows.Forms.TabPage();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.chkFoliosConcentrar = new System.Windows.Forms.CheckBox();
            this.grdFolios = new FarPoint.Win.Spread.FpSpread();
            this.grdFolios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.chkFoliosMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.chkUnidadesConcentrar = new System.Windows.Forms.CheckBox();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameTipoRemision = new System.Windows.Forms.GroupBox();
            this.rdoServicio = new System.Windows.Forms.RadioButton();
            this.rdoProducto = new System.Windows.Forms.RadioButton();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.rdoConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoVenta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoInsumoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.btnAgregarProgramasDeAtencion = new System.Windows.Forms.Button();
            this.lblSubPrograma = new System.Windows.Forms.Label();
            this.txtSubPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label19 = new System.Windows.Forms.Label();
            this.FrameFuentesFinanciamiento = new System.Windows.Forms.GroupBox();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.lblIdSubCliente = new System.Windows.Forms.Label();
            this.tabInformacionFacturar = new System.Windows.Forms.TabPage();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.grdReporte_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardarGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameTiposDeUnidades.SuspendLayout();
            this.tabControlFacturacion.SuspendLayout();
            this.tabPagParametros.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).BeginInit();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameTipoRemision.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameFuentesFinanciamiento.SuspendLayout();
            this.tabInformacionFacturar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).BeginInit();
            this.toolMenuFacturacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnGenerarRemisiones,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1106, 25);
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
            // btnGenerarRemisiones
            // 
            this.btnGenerarRemisiones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarRemisiones.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarRemisiones.Image")));
            this.btnGenerarRemisiones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarRemisiones.Name = "btnGenerarRemisiones";
            this.btnGenerarRemisiones.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarRemisiones.Text = "Obtener detalles de remisión";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(144, 102);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(298, 21);
            this.lblSubCliente.TabIndex = 40;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Sub-Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(144, 75);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(298, 21);
            this.lblCliente.TabIndex = 37;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConcepto
            // 
            this.lblConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConcepto.Location = new System.Drawing.Point(144, 50);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(298, 21);
            this.lblConcepto.TabIndex = 46;
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtConcepto
            // 
            this.txtConcepto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConcepto.Decimales = 2;
            this.txtConcepto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtConcepto.ForeColor = System.Drawing.Color.Black;
            this.txtConcepto.Location = new System.Drawing.Point(80, 51);
            this.txtConcepto.MaxLength = 4;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.PermitirApostrofo = false;
            this.txtConcepto.PermitirNegativos = false;
            this.txtConcepto.Size = new System.Drawing.Size(60, 20);
            this.txtConcepto.TabIndex = 1;
            this.txtConcepto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            this.txtConcepto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConcepto_KeyDown);
            this.txtConcepto.Validating += new System.ComponentModel.CancelEventHandler(this.txtConcepto_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Concepto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRubro
            // 
            this.lblRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRubro.Location = new System.Drawing.Point(144, 24);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(298, 21);
            this.lblRubro.TabIndex = 43;
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRubro
            // 
            this.txtRubro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRubro.Decimales = 2;
            this.txtRubro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRubro.ForeColor = System.Drawing.Color.Black;
            this.txtRubro.Location = new System.Drawing.Point(80, 26);
            this.txtRubro.MaxLength = 4;
            this.txtRubro.Name = "txtRubro";
            this.txtRubro.PermitirApostrofo = false;
            this.txtRubro.PermitirNegativos = false;
            this.txtRubro.Size = new System.Drawing.Size(60, 20);
            this.txtRubro.TabIndex = 0;
            this.txtRubro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRubro.TextChanged += new System.EventHandler(this.txtRubro_TextChanged);
            this.txtRubro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRubro_KeyDown);
            this.txtRubro.Validating += new System.ComponentModel.CancelEventHandler(this.txtRubro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Rubro :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.cboFarmacias);
            this.FrameFechas.Controls.Add(this.label8);
            this.FrameFechas.Controls.Add(this.rdoFechaCaptura);
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.rdoFechaReceta);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(934, 3);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(158, 142);
            this.FrameFechas.TabIndex = 9;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(94, 116);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(40, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(28, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // rdoFechaCaptura
            // 
            this.rdoFechaCaptura.Location = new System.Drawing.Point(20, 94);
            this.rdoFechaCaptura.Name = "rdoFechaCaptura";
            this.rdoFechaCaptura.Size = new System.Drawing.Size(122, 17);
            this.rdoFechaCaptura.TabIndex = 11;
            this.rdoFechaCaptura.TabStop = true;
            this.rdoFechaCaptura.Text = "Fecha de captura";
            this.rdoFechaCaptura.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(56, 46);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // rdoFechaReceta
            // 
            this.rdoFechaReceta.Location = new System.Drawing.Point(20, 74);
            this.rdoFechaReceta.Name = "rdoFechaReceta";
            this.rdoFechaReceta.Size = new System.Drawing.Size(122, 17);
            this.rdoFechaReceta.TabIndex = 10;
            this.rdoFechaReceta.TabStop = true;
            this.rdoFechaReceta.Text = "Fecha receta";
            this.rdoFechaReceta.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 26);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(56, 24);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameTiposDeUnidades
            // 
            this.FrameTiposDeUnidades.Controls.Add(this.cboJurisdicciones);
            this.FrameTiposDeUnidades.Controls.Add(this.label4);
            this.FrameTiposDeUnidades.Controls.Add(this.cboTipoUnidades);
            this.FrameTiposDeUnidades.Controls.Add(this.label6);
            this.FrameTiposDeUnidades.Location = new System.Drawing.Point(6, 147);
            this.FrameTiposDeUnidades.Name = "FrameTiposDeUnidades";
            this.FrameTiposDeUnidades.Size = new System.Drawing.Size(1087, 51);
            this.FrameTiposDeUnidades.TabIndex = 2;
            this.FrameTiposDeUnidades.TabStop = false;
            this.FrameTiposDeUnidades.Text = "Parámetros de Unidades";
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(622, 19);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(443, 21);
            this.cboJurisdicciones.TabIndex = 0;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(541, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 22;
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
            this.cboTipoUnidades.Location = new System.Drawing.Point(89, 19);
            this.cboTipoUnidades.MostrarToolTip = false;
            this.cboTipoUnidades.Name = "cboTipoUnidades";
            this.cboTipoUnidades.Size = new System.Drawing.Size(443, 21);
            this.cboTipoUnidades.TabIndex = 0;
            this.cboTipoUnidades.SelectedIndexChanged += new System.EventHandler(this.cboTipoUnidades_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(36, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tipo :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControlFacturacion
            // 
            this.tabControlFacturacion.Appearance = SC_ControlsCS.scTabAppearance.Buttons;
            this.tabControlFacturacion.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tabControlFacturacion.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabControlFacturacion.Controls.Add(this.tabPagParametros);
            this.tabControlFacturacion.Controls.Add(this.tabInformacionFacturar);
            this.tabControlFacturacion.CustomBackColor = false;
            this.tabControlFacturacion.CustomBackColorPages = false;
            this.tabControlFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFacturacion.HotTrack = true;
            this.tabControlFacturacion.Location = new System.Drawing.Point(0, 25);
            this.tabControlFacturacion.MostrarBorde = false;
            this.tabControlFacturacion.Name = "tabControlFacturacion";
            this.tabControlFacturacion.SelectedIndex = 0;
            this.tabControlFacturacion.Size = new System.Drawing.Size(1106, 513);
            this.tabControlFacturacion.TabIndex = 1;
            this.tabControlFacturacion.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlFacturacion_DrawItem);
            // 
            // tabPagParametros
            // 
            this.tabPagParametros.BackColor = System.Drawing.SystemColors.Info;
            this.tabPagParametros.Controls.Add(this.FrameFolios);
            this.tabPagParametros.Controls.Add(this.FrameUnidades);
            this.tabPagParametros.Controls.Add(this.FrameTipoRemision);
            this.tabPagParametros.Controls.Add(this.FrameOrigenInsumo);
            this.tabPagParametros.Controls.Add(this.FrameTipoInsumo);
            this.tabPagParametros.Controls.Add(this.FrameDispensacion);
            this.tabPagParametros.Controls.Add(this.FrameFuentesFinanciamiento);
            this.tabPagParametros.Controls.Add(this.FrameFechas);
            this.tabPagParametros.Controls.Add(this.FrameTiposDeUnidades);
            this.tabPagParametros.Location = new System.Drawing.Point(4, 28);
            this.tabPagParametros.Name = "tabPagParametros";
            this.tabPagParametros.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPagParametros.Size = new System.Drawing.Size(1098, 481);
            this.tabPagParametros.TabIndex = 0;
            this.tabPagParametros.Text = "Parámetros";
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.chkFoliosConcentrar);
            this.FrameFolios.Controls.Add(this.grdFolios);
            this.FrameFolios.Controls.Add(this.chkFoliosMarcarDesmarcar);
            this.FrameFolios.Location = new System.Drawing.Point(737, 200);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(355, 282);
            this.FrameFolios.TabIndex = 12;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // chkFoliosConcentrar
            // 
            this.chkFoliosConcentrar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFoliosConcentrar.Location = new System.Drawing.Point(81, 0);
            this.chkFoliosConcentrar.Name = "chkFoliosConcentrar";
            this.chkFoliosConcentrar.Size = new System.Drawing.Size(134, 17);
            this.chkFoliosConcentrar.TabIndex = 4;
            this.chkFoliosConcentrar.Text = "Concentrar folios";
            this.chkFoliosConcentrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFoliosConcentrar.UseVisualStyleBackColor = true;
            // 
            // grdFolios
            // 
            this.grdFolios.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.grdFolios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFolios.Location = new System.Drawing.Point(10, 20);
            this.grdFolios.Name = "grdFolios";
            this.grdFolios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFolios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFolios_Sheet1});
            this.grdFolios.Size = new System.Drawing.Size(336, 250);
            this.grdFolios.TabIndex = 3;
            // 
            // grdFolios_Sheet1
            // 
            this.grdFolios_Sheet1.Reset();
            this.grdFolios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFolios_Sheet1.ColumnCount = 4;
            this.grdFolios_Sheet1.RowCount = 12;
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha Registro";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesado";
            this.grdFolios_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdFolios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdFolios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Label = "Fecha Registro";
            this.grdFolios_Sheet1.Columns.Get(0).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Width = 100F;
            this.grdFolios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdFolios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdFolios_Sheet1.Columns.Get(1).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Width = 100F;
            this.grdFolios_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdFolios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdFolios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(2).Width = 80F;
            this.grdFolios_Sheet1.Columns.Get(3).CellType = checkBoxCellType2;
            this.grdFolios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(3).Label = "Procesado";
            this.grdFolios_Sheet1.Columns.Get(3).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(3).Width = 80F;
            this.grdFolios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // chkFoliosMarcarDesmarcar
            // 
            this.chkFoliosMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFoliosMarcarDesmarcar.Location = new System.Drawing.Point(219, 0);
            this.chkFoliosMarcarDesmarcar.Name = "chkFoliosMarcarDesmarcar";
            this.chkFoliosMarcarDesmarcar.Size = new System.Drawing.Size(126, 17);
            this.chkFoliosMarcarDesmarcar.TabIndex = 2;
            this.chkFoliosMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkFoliosMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFoliosMarcarDesmarcar.UseVisualStyleBackColor = true;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.chkUnidadesConcentrar);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(7, 200);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(724, 282);
            this.FrameUnidades.TabIndex = 11;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // chkUnidadesConcentrar
            // 
            this.chkUnidadesConcentrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUnidadesConcentrar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUnidadesConcentrar.Location = new System.Drawing.Point(452, 0);
            this.chkUnidadesConcentrar.Name = "chkUnidadesConcentrar";
            this.chkUnidadesConcentrar.Size = new System.Drawing.Size(134, 17);
            this.chkUnidadesConcentrar.TabIndex = 5;
            this.chkUnidadesConcentrar.Text = "Concentrar unidades";
            this.chkUnidadesConcentrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUnidadesConcentrar.UseVisualStyleBackColor = true;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(592, 0);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(126, 17);
            this.chkMarcarDesmarcar.TabIndex = 2;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(118, 105);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(412, 38);
            this.FrameProceso.TabIndex = 1;
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
            this.pgBar.Size = new System.Drawing.Size(384, 12);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(10, 20);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(709, 250);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 4;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre Unidad";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesado";
            this.grdUnidades_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Id Unidad";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 100F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Nombre Unidad";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 500F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = checkBoxCellType3;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 100F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType4;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Procesado";
            this.grdUnidades_Sheet1.Columns.Get(3).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 100F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameTipoRemision
            // 
            this.FrameTipoRemision.Controls.Add(this.rdoServicio);
            this.FrameTipoRemision.Controls.Add(this.rdoProducto);
            this.FrameTipoRemision.Location = new System.Drawing.Point(467, 3);
            this.FrameTipoRemision.Name = "FrameTipoRemision";
            this.FrameTipoRemision.Size = new System.Drawing.Size(110, 65);
            this.FrameTipoRemision.TabIndex = 7;
            this.FrameTipoRemision.TabStop = false;
            this.FrameTipoRemision.Text = "Tipo de remisión";
            // 
            // rdoServicio
            // 
            this.rdoServicio.Location = new System.Drawing.Point(16, 38);
            this.rdoServicio.Name = "rdoServicio";
            this.rdoServicio.Size = new System.Drawing.Size(88, 17);
            this.rdoServicio.TabIndex = 1;
            this.rdoServicio.TabStop = true;
            this.rdoServicio.Text = "Servicio";
            this.rdoServicio.UseVisualStyleBackColor = true;
            // 
            // rdoProducto
            // 
            this.rdoProducto.Location = new System.Drawing.Point(16, 20);
            this.rdoProducto.Name = "rdoProducto";
            this.rdoProducto.Size = new System.Drawing.Size(88, 17);
            this.rdoProducto.TabIndex = 0;
            this.rdoProducto.TabStop = true;
            this.rdoProducto.Text = "Producto";
            this.rdoProducto.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Controls.Add(this.rdoConsignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoVenta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(583, 3);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(122, 65);
            this.FrameOrigenInsumo.TabIndex = 5;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Origen de Insumos";
            // 
            // rdoConsignacion
            // 
            this.rdoConsignacion.Location = new System.Drawing.Point(16, 38);
            this.rdoConsignacion.Name = "rdoConsignacion";
            this.rdoConsignacion.Size = new System.Drawing.Size(98, 17);
            this.rdoConsignacion.TabIndex = 1;
            this.rdoConsignacion.TabStop = true;
            this.rdoConsignacion.Text = "Consignación";
            this.rdoConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoVenta
            // 
            this.rdoVenta.Location = new System.Drawing.Point(16, 19);
            this.rdoVenta.Name = "rdoVenta";
            this.rdoVenta.Size = new System.Drawing.Size(97, 18);
            this.rdoVenta.TabIndex = 0;
            this.rdoVenta.TabStop = true;
            this.rdoVenta.Text = "Venta";
            this.rdoVenta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoAmbos);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(711, 3);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(218, 65);
            this.FrameTipoInsumo.TabIndex = 6;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoInsumoAmbos
            // 
            this.rdoInsumoAmbos.Location = new System.Drawing.Point(146, 19);
            this.rdoInsumoAmbos.Name = "rdoInsumoAmbos";
            this.rdoInsumoAmbos.Size = new System.Drawing.Size(62, 18);
            this.rdoInsumoAmbos.TabIndex = 2;
            this.rdoInsumoAmbos.TabStop = true;
            this.rdoInsumoAmbos.Text = "Ambos";
            this.rdoInsumoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMaterialDeCuracion
            // 
            this.rdoInsumoMaterialDeCuracion.Location = new System.Drawing.Point(16, 38);
            this.rdoInsumoMaterialDeCuracion.Name = "rdoInsumoMaterialDeCuracion";
            this.rdoInsumoMaterialDeCuracion.Size = new System.Drawing.Size(136, 17);
            this.rdoInsumoMaterialDeCuracion.TabIndex = 1;
            this.rdoInsumoMaterialDeCuracion.TabStop = true;
            this.rdoInsumoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoInsumoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamento
            // 
            this.rdoInsumoMedicamento.Location = new System.Drawing.Point(16, 20);
            this.rdoInsumoMedicamento.Name = "rdoInsumoMedicamento";
            this.rdoInsumoMedicamento.Size = new System.Drawing.Size(98, 17);
            this.rdoInsumoMedicamento.TabIndex = 0;
            this.rdoInsumoMedicamento.TabStop = true;
            this.rdoInsumoMedicamento.Text = "Medicamento";
            this.rdoInsumoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.btnAgregarProgramasDeAtencion);
            this.FrameDispensacion.Controls.Add(this.lblSubPrograma);
            this.FrameDispensacion.Controls.Add(this.txtSubPrograma);
            this.FrameDispensacion.Controls.Add(this.label17);
            this.FrameDispensacion.Controls.Add(this.lblPrograma);
            this.FrameDispensacion.Controls.Add(this.txtPrograma);
            this.FrameDispensacion.Controls.Add(this.label19);
            this.FrameDispensacion.Location = new System.Drawing.Point(467, 69);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(461, 76);
            this.FrameDispensacion.TabIndex = 1;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Parámetros de Dispensación";
            // 
            // btnAgregarProgramasDeAtencion
            // 
            this.btnAgregarProgramasDeAtencion.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarProgramasDeAtencion.Image")));
            this.btnAgregarProgramasDeAtencion.Location = new System.Drawing.Point(418, 19);
            this.btnAgregarProgramasDeAtencion.Name = "btnAgregarProgramasDeAtencion";
            this.btnAgregarProgramasDeAtencion.Size = new System.Drawing.Size(32, 50);
            this.btnAgregarProgramasDeAtencion.TabIndex = 42;
            this.btnAgregarProgramasDeAtencion.UseVisualStyleBackColor = true;
            this.btnAgregarProgramasDeAtencion.Click += new System.EventHandler(this.btnAgregarProgramasDeAtencion_Click);
            // 
            // lblSubPrograma
            // 
            this.lblSubPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPrograma.Location = new System.Drawing.Point(154, 45);
            this.lblSubPrograma.Name = "lblSubPrograma";
            this.lblSubPrograma.Size = new System.Drawing.Size(258, 21);
            this.lblSubPrograma.TabIndex = 40;
            this.lblSubPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPrograma
            // 
            this.txtSubPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPrograma.Decimales = 2;
            this.txtSubPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtSubPrograma.Location = new System.Drawing.Point(89, 45);
            this.txtSubPrograma.MaxLength = 4;
            this.txtSubPrograma.Name = "txtSubPrograma";
            this.txtSubPrograma.PermitirApostrofo = false;
            this.txtSubPrograma.PermitirNegativos = false;
            this.txtSubPrograma.Size = new System.Drawing.Size(59, 20);
            this.txtSubPrograma.TabIndex = 1;
            this.txtSubPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPrograma.TextChanged += new System.EventHandler(this.txtSubPrograma_TextChanged);
            this.txtSubPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPrograma_KeyDown);
            this.txtSubPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPrograma_Validating);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "Sub-Programa :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrograma
            // 
            this.lblPrograma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrograma.Location = new System.Drawing.Point(154, 20);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(258, 21);
            this.lblPrograma.TabIndex = 37;
            this.lblPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrograma
            // 
            this.txtPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPrograma.Decimales = 2;
            this.txtPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtPrograma.Location = new System.Drawing.Point(89, 20);
            this.txtPrograma.MaxLength = 4;
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.PermitirApostrofo = false;
            this.txtPrograma.PermitirNegativos = false;
            this.txtPrograma.Size = new System.Drawing.Size(59, 20);
            this.txtPrograma.TabIndex = 0;
            this.txtPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrograma.TextChanged += new System.EventHandler(this.txtPrograma_TextChanged);
            this.txtPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrograma_KeyDown);
            this.txtPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtPrograma_Validating);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(25, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 16);
            this.label19.TabIndex = 36;
            this.label19.Text = "Programa :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFuentesFinanciamiento
            // 
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblIdCliente);
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblIdSubCliente);
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblConcepto);
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblSubCliente);
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblRubro);
            this.FrameFuentesFinanciamiento.Controls.Add(this.label1);
            this.FrameFuentesFinanciamiento.Controls.Add(this.txtConcepto);
            this.FrameFuentesFinanciamiento.Controls.Add(this.lblCliente);
            this.FrameFuentesFinanciamiento.Controls.Add(this.label9);
            this.FrameFuentesFinanciamiento.Controls.Add(this.label3);
            this.FrameFuentesFinanciamiento.Controls.Add(this.label7);
            this.FrameFuentesFinanciamiento.Controls.Add(this.txtRubro);
            this.FrameFuentesFinanciamiento.Location = new System.Drawing.Point(6, 3);
            this.FrameFuentesFinanciamiento.Name = "FrameFuentesFinanciamiento";
            this.FrameFuentesFinanciamiento.Size = new System.Drawing.Size(458, 142);
            this.FrameFuentesFinanciamiento.TabIndex = 0;
            this.FrameFuentesFinanciamiento.TabStop = false;
            this.FrameFuentesFinanciamiento.Text = "Fuente de Finaciamiento";
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdCliente.Location = new System.Drawing.Point(80, 75);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(59, 21);
            this.lblIdCliente.TabIndex = 2;
            this.lblIdCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdSubCliente
            // 
            this.lblIdSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdSubCliente.Location = new System.Drawing.Point(80, 102);
            this.lblIdSubCliente.Name = "lblIdSubCliente";
            this.lblIdSubCliente.Size = new System.Drawing.Size(59, 21);
            this.lblIdSubCliente.TabIndex = 3;
            this.lblIdSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabInformacionFacturar
            // 
            this.tabInformacionFacturar.BackColor = System.Drawing.Color.OldLace;
            this.tabInformacionFacturar.Controls.Add(this.grdReporte);
            this.tabInformacionFacturar.Controls.Add(this.toolMenuFacturacion);
            this.tabInformacionFacturar.Location = new System.Drawing.Point(4, 28);
            this.tabInformacionFacturar.Name = "tabInformacionFacturar";
            this.tabInformacionFacturar.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabInformacionFacturar.Size = new System.Drawing.Size(1098, 481);
            this.tabInformacionFacturar.TabIndex = 1;
            this.tabInformacionFacturar.Text = "Información a Remisionar";
            // 
            // grdReporte
            // 
            this.grdReporte.AccessibleDescription = "grdReporte, Sheet1, Row 0, Column 0, ";
            this.grdReporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdReporte.Location = new System.Drawing.Point(698, 56);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdReporte.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdReporte_Sheet1});
            this.grdReporte.Size = new System.Drawing.Size(202, 345);
            this.grdReporte.TabIndex = 3;
            // 
            // grdReporte_Sheet1
            // 
            this.grdReporte_Sheet1.Reset();
            this.grdReporte_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdReporte_Sheet1.ColumnCount = 10;
            this.grdReporte_Sheet1.RowCount = 10;
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Jurisdicción";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre jurisdicción";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Farmacia";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Nombre Farmacia";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha de Registro";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Folio";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha Mínima de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha Máxima de Cierre";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Remisionar";
            this.grdReporte_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            this.grdReporte_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdReporte_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Label = "Jurisdicción";
            this.grdReporte_Sheet1.Columns.Get(0).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Width = 65F;
            this.grdReporte_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdReporte_Sheet1.Columns.Get(1).Label = "Nombre jurisdicción";
            this.grdReporte_Sheet1.Columns.Get(1).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Width = 120F;
            this.grdReporte_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdReporte_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Label = "Farmacia";
            this.grdReporte_Sheet1.Columns.Get(2).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdReporte_Sheet1.Columns.Get(3).Label = "Nombre Farmacia";
            this.grdReporte_Sheet1.Columns.Get(3).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).Width = 180F;
            this.grdReporte_Sheet1.Columns.Get(4).CellType = textCellType9;
            this.grdReporte_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Label = "Fecha de Registro";
            this.grdReporte_Sheet1.Columns.Get(4).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(4).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(5).CellType = textCellType10;
            this.grdReporte_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Label = "Folio";
            this.grdReporte_Sheet1.Columns.Get(5).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(5).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(6).CellType = textCellType11;
            this.grdReporte_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(6).Label = "Fecha de Cierre";
            this.grdReporte_Sheet1.Columns.Get(6).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(6).Width = 80F;
            this.grdReporte_Sheet1.Columns.Get(7).CellType = textCellType12;
            this.grdReporte_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(7).Label = "Fecha Mínima de Cierre";
            this.grdReporte_Sheet1.Columns.Get(7).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(7).Width = 100F;
            this.grdReporte_Sheet1.Columns.Get(8).CellType = textCellType13;
            this.grdReporte_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(8).Label = "Fecha Máxima de Cierre";
            this.grdReporte_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(8).Width = 100F;
            this.grdReporte_Sheet1.Columns.Get(9).CellType = checkBoxCellType5;
            this.grdReporte_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(9).Label = "Remisionar";
            this.grdReporte_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(9).Width = 66F;
            this.grdReporte_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolMenuFacturacion
            // 
            this.toolMenuFacturacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.btnGuardarGrid,
            this.toolStripSeparator4,
            this.toolStripButton3,
            this.toolStripSeparator5,
            this.toolStripButton4});
            this.toolMenuFacturacion.Location = new System.Drawing.Point(3, 3);
            this.toolMenuFacturacion.Name = "toolMenuFacturacion";
            this.toolMenuFacturacion.Size = new System.Drawing.Size(1092, 25);
            this.toolMenuFacturacion.TabIndex = 2;
            this.toolMenuFacturacion.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Nuevo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardarGrid
            // 
            this.btnGuardarGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardarGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarGrid.Image")));
            this.btnGuardarGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardarGrid.Name = "btnGuardarGrid";
            this.btnGuardarGrid.Size = new System.Drawing.Size(23, 22);
            this.btnGuardarGrid.Text = "Guardar";
            this.btnGuardarGrid.Click += new System.EventHandler(this.btnGuardarGrid_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Enabled = false;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Cancelar";
            this.toolStripButton3.ToolTipText = "Cancelar";
            this.toolStripButton3.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Imprimir";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(250, 89);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage1";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(992, 162);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage2";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmRemision_BASE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 538);
            this.Controls.Add(this.tabControlFacturacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmRemision_BASE";
            this.Text = "Remisiones generales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRemision_BASE_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmRemision_BASE_Paint);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameTiposDeUnidades.ResumeLayout(false);
            this.tabControlFacturacion.ResumeLayout(false);
            this.tabPagParametros.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).EndInit();
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameTipoRemision.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.FrameDispensacion.PerformLayout();
            this.FrameFuentesFinanciamiento.ResumeLayout(false);
            this.FrameFuentesFinanciamiento.PerformLayout();
            this.tabInformacionFacturar.ResumeLayout(false);
            this.tabInformacionFacturar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).EndInit();
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Label lblConcepto;
        private SC_ControlsCS.scTextBoxExt txtConcepto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRubro;
        private SC_ControlsCS.scTextBoxExt txtRubro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameTiposDeUnidades;
        private SC_ControlsCS.scComboBoxExt cboTipoUnidades;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTabControlExt tabControlFacturacion;
        private System.Windows.Forms.TabPage tabPagParametros;
        private System.Windows.Forms.TabPage tabInformacionFacturar;
        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardarGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.GroupBox FrameFuentesFinanciamiento;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.Label lblSubPrograma;
        private SC_ControlsCS.scTextBoxExt txtSubPrograma;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPrograma;
        private SC_ControlsCS.scTextBoxExt txtPrograma;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblIdSubCliente;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private FarPoint.Win.Spread.SheetView grdReporte_Sheet1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameTipoInsumo;
        private System.Windows.Forms.RadioButton rdoInsumoMaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamento;
        private System.Windows.Forms.Button btnAgregarProgramasDeAtencion;
        private System.Windows.Forms.GroupBox FrameOrigenInsumo;
        private System.Windows.Forms.RadioButton rdoConsignacion;
        private System.Windows.Forms.RadioButton rdoVenta;
        private System.Windows.Forms.GroupBox FrameTipoRemision;
        private System.Windows.Forms.RadioButton rdoServicio;
        private System.Windows.Forms.RadioButton rdoProducto;
        private System.Windows.Forms.RadioButton rdoFechaCaptura;
        private System.Windows.Forms.RadioButton rdoFechaReceta;
        private System.Windows.Forms.RadioButton rdoInsumoAmbos;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.CheckBox chkFoliosMarcarDesmarcar;
        private FarPoint.Win.Spread.FpSpread grdFolios;
        private FarPoint.Win.Spread.SheetView grdFolios_Sheet1;
        private System.Windows.Forms.CheckBox chkFoliosConcentrar;
        private System.Windows.Forms.CheckBox chkUnidadesConcentrar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGenerarRemisiones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}