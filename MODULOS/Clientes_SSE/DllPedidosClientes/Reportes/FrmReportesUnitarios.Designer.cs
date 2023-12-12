namespace DllPedidosClientes.Reportes
{
    partial class FrmReportesUnitarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportesUnitarios));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.FrameBeneficiario = new System.Windows.Forms.GroupBox();
            this.lblBeneficiario = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCliente = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.FrameListaReportes = new System.Windows.Forms.GroupBox();
            this.cboReporte = new SC_ControlsCS.scComboBoxExt();
            this.menuBeneficiarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listadoDeBeneficiariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.pagBeneficiario = new System.Windows.Forms.TabPage();
            this.pagMedico = new System.Windows.Forms.TabPage();
            this.FrameMedico = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.lblIdEspecialidad = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.lblMedico = new System.Windows.Forms.Label();
            this.txtMedico = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.pagClave = new System.Windows.Forms.TabPage();
            this.FrameClave = new System.Windows.Forms.GroupBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.pagFolio = new System.Windows.Forms.TabPage();
            this.FrameFolio = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNombrePersonal = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            this.FrameBeneficiario.SuspendLayout();
            this.FrameListaReportes.SuspendLayout();
            this.menuBeneficiarios.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.pagBeneficiario.SuspendLayout();
            this.pagMedico.SuspendLayout();
            this.FrameMedico.SuspendLayout();
            this.pagClave.SuspendLayout();
            this.FrameClave.SuspendLayout();
            this.pagFolio.SuspendLayout();
            this.FrameFolio.SuspendLayout();
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
            this.grdReporte.Size = new System.Drawing.Size(54, 39);
            this.grdReporte.TabIndex = 0;
            // 
            // FrameBeneficiario
            // 
            this.FrameBeneficiario.Controls.Add(this.lblBeneficiario);
            this.FrameBeneficiario.Controls.Add(this.txtBeneficiario);
            this.FrameBeneficiario.Controls.Add(this.label12);
            this.FrameBeneficiario.Controls.Add(this.lblSubCliente);
            this.FrameBeneficiario.Controls.Add(this.txtSubCliente);
            this.FrameBeneficiario.Controls.Add(this.label1);
            this.FrameBeneficiario.Controls.Add(this.lblCliente);
            this.FrameBeneficiario.Controls.Add(this.txtCliente);
            this.FrameBeneficiario.Controls.Add(this.label3);
            this.FrameBeneficiario.Location = new System.Drawing.Point(6, 6);
            this.FrameBeneficiario.Name = "FrameBeneficiario";
            this.FrameBeneficiario.Size = new System.Drawing.Size(481, 104);
            this.FrameBeneficiario.TabIndex = 0;
            this.FrameBeneficiario.TabStop = false;
            this.FrameBeneficiario.Text = "Parametros de Beneficiario";
            // 
            // lblBeneficiario
            // 
            this.lblBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficiario.Location = new System.Drawing.Point(153, 71);
            this.lblBeneficiario.Name = "lblBeneficiario";
            this.lblBeneficiario.Size = new System.Drawing.Size(321, 21);
            this.lblBeneficiario.TabIndex = 43;
            this.lblBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(88, 71);
            this.txtBeneficiario.MaxLength = 8;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(59, 20);
            this.txtBeneficiario.TabIndex = 2;
            this.txtBeneficiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBeneficiario.TextChanged += new System.EventHandler(this.txtBeneficiario_TextChanged);
            this.txtBeneficiario.Validating += new System.ComponentModel.CancelEventHandler(this.txtBeneficiario_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(17, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 16);
            this.label12.TabIndex = 42;
            this.label12.Text = "Beneficiario :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(154, 45);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(321, 21);
            this.lblSubCliente.TabIndex = 40;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCliente
            // 
            this.txtSubCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCliente.Decimales = 2;
            this.txtSubCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCliente.ForeColor = System.Drawing.Color.Black;
            this.txtSubCliente.Location = new System.Drawing.Point(89, 45);
            this.txtSubCliente.MaxLength = 4;
            this.txtSubCliente.Name = "txtSubCliente";
            this.txtSubCliente.PermitirApostrofo = false;
            this.txtSubCliente.PermitirNegativos = false;
            this.txtSubCliente.Size = new System.Drawing.Size(59, 20);
            this.txtSubCliente.TabIndex = 1;
            this.txtSubCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCliente.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
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
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(154, 20);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(321, 21);
            this.lblCliente.TabIndex = 37;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(89, 20);
            this.txtCliente.MaxLength = 4;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(59, 20);
            this.txtCliente.TabIndex = 0;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
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
            this.FrameListaReportes.Location = new System.Drawing.Point(10, 268);
            this.FrameListaReportes.Name = "FrameListaReportes";
            this.FrameListaReportes.Size = new System.Drawing.Size(503, 48);
            this.FrameListaReportes.TabIndex = 4;
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
            this.groupBox5.Size = new System.Drawing.Size(348, 79);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(119, 47);
            this.lblFarmacia.MostrarToolTip = true;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(223, 20);
            this.lblFarmacia.TabIndex = 25;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(75, 132);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(267, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(71, 47);
            this.txtFarmacia.MaxLength = 4;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(45, 20);
            this.txtFarmacia.TabIndex = 24;
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmacia_KeyDown);
            this.txtFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmacia_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 50);
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
            this.cboEstados.Location = new System.Drawing.Point(71, 20);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(271, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(17, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(364, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(149, 79);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(51, 47);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 24);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(51, 21);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.pagBeneficiario);
            this.tabReportes.Controls.Add(this.pagMedico);
            this.tabReportes.Controls.Add(this.pagClave);
            this.tabReportes.Controls.Add(this.pagFolio);
            this.tabReportes.Location = new System.Drawing.Point(10, 120);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(502, 142);
            this.tabReportes.TabIndex = 3;
            this.tabReportes.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabReportes_Selecting);
            // 
            // pagBeneficiario
            // 
            this.pagBeneficiario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagBeneficiario.Controls.Add(this.FrameBeneficiario);
            this.pagBeneficiario.Location = new System.Drawing.Point(4, 22);
            this.pagBeneficiario.Name = "pagBeneficiario";
            this.pagBeneficiario.Padding = new System.Windows.Forms.Padding(3);
            this.pagBeneficiario.Size = new System.Drawing.Size(494, 116);
            this.pagBeneficiario.TabIndex = 0;
            this.pagBeneficiario.Text = "Beneficiario";
            // 
            // pagMedico
            // 
            this.pagMedico.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagMedico.Controls.Add(this.FrameMedico);
            this.pagMedico.Location = new System.Drawing.Point(4, 22);
            this.pagMedico.Name = "pagMedico";
            this.pagMedico.Padding = new System.Windows.Forms.Padding(3);
            this.pagMedico.Size = new System.Drawing.Size(494, 116);
            this.pagMedico.TabIndex = 1;
            this.pagMedico.Text = "Medico";
            // 
            // FrameMedico
            // 
            this.FrameMedico.Controls.Add(this.label17);
            this.FrameMedico.Controls.Add(this.lblCedula);
            this.FrameMedico.Controls.Add(this.lblIdEspecialidad);
            this.FrameMedico.Controls.Add(this.label14);
            this.FrameMedico.Controls.Add(this.lblEspecialidad);
            this.FrameMedico.Controls.Add(this.lblMedico);
            this.FrameMedico.Controls.Add(this.txtMedico);
            this.FrameMedico.Controls.Add(this.label10);
            this.FrameMedico.Location = new System.Drawing.Point(7, 6);
            this.FrameMedico.Name = "FrameMedico";
            this.FrameMedico.Size = new System.Drawing.Size(481, 104);
            this.FrameMedico.TabIndex = 1;
            this.FrameMedico.TabStop = false;
            this.FrameMedico.Text = "Parametros de Medico";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(10, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 16);
            this.label17.TabIndex = 46;
            this.label17.Text = "Cedula :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCedula
            // 
            this.lblCedula.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCedula.Location = new System.Drawing.Point(89, 73);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(386, 21);
            this.lblCedula.TabIndex = 2;
            this.lblCedula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdEspecialidad
            // 
            this.lblIdEspecialidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdEspecialidad.Location = new System.Drawing.Point(89, 47);
            this.lblIdEspecialidad.Name = "lblIdEspecialidad";
            this.lblIdEspecialidad.Size = new System.Drawing.Size(59, 21);
            this.lblIdEspecialidad.TabIndex = 1;
            this.lblIdEspecialidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(10, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 16);
            this.label14.TabIndex = 43;
            this.label14.Text = "Especialidad :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEspecialidad.Location = new System.Drawing.Point(154, 47);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(321, 21);
            this.lblEspecialidad.TabIndex = 42;
            this.lblEspecialidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMedico
            // 
            this.lblMedico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMedico.Location = new System.Drawing.Point(154, 20);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(321, 21);
            this.lblMedico.TabIndex = 37;
            this.lblMedico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMedico
            // 
            this.txtMedico.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMedico.Decimales = 2;
            this.txtMedico.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtMedico.ForeColor = System.Drawing.Color.Black;
            this.txtMedico.Location = new System.Drawing.Point(89, 20);
            this.txtMedico.MaxLength = 6;
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.PermitirApostrofo = false;
            this.txtMedico.PermitirNegativos = false;
            this.txtMedico.Size = new System.Drawing.Size(59, 20);
            this.txtMedico.TabIndex = 0;
            this.txtMedico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMedico.TextChanged += new System.EventHandler(this.txtMedico_TextChanged);
            this.txtMedico.Validating += new System.ComponentModel.CancelEventHandler(this.txtMedico_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(23, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 36;
            this.label10.Text = "Medico :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pagClave
            // 
            this.pagClave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagClave.Controls.Add(this.FrameClave);
            this.pagClave.Location = new System.Drawing.Point(4, 22);
            this.pagClave.Name = "pagClave";
            this.pagClave.Padding = new System.Windows.Forms.Padding(3);
            this.pagClave.Size = new System.Drawing.Size(494, 116);
            this.pagClave.TabIndex = 2;
            this.pagClave.Text = "Clave";
            // 
            // FrameClave
            // 
            this.FrameClave.Controls.Add(this.lblClave);
            this.FrameClave.Controls.Add(this.txtClaveSSA);
            this.FrameClave.Controls.Add(this.label7);
            this.FrameClave.Location = new System.Drawing.Point(7, 6);
            this.FrameClave.Name = "FrameClave";
            this.FrameClave.Size = new System.Drawing.Size(481, 104);
            this.FrameClave.TabIndex = 2;
            this.FrameClave.TabStop = false;
            this.FrameClave.Text = "Parametros de Clave";
            // 
            // lblClave
            // 
            this.lblClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClave.Location = new System.Drawing.Point(89, 43);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(386, 49);
            this.lblClave.TabIndex = 1;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(89, 20);
            this.txtClaveSSA.MaxLength = 50;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(149, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.TextChanged += new System.EventHandler(this.txtClaveSSA_TextChanged);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(23, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "Clave :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pagFolio
            // 
            this.pagFolio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagFolio.Controls.Add(this.FrameFolio);
            this.pagFolio.Location = new System.Drawing.Point(4, 22);
            this.pagFolio.Name = "pagFolio";
            this.pagFolio.Padding = new System.Windows.Forms.Padding(3);
            this.pagFolio.Size = new System.Drawing.Size(494, 116);
            this.pagFolio.TabIndex = 3;
            this.pagFolio.Text = "Folio";
            // 
            // FrameFolio
            // 
            this.FrameFolio.Controls.Add(this.lblPersonal);
            this.FrameFolio.Controls.Add(this.label11);
            this.FrameFolio.Controls.Add(this.dtpFechaRegistro);
            this.FrameFolio.Controls.Add(this.label6);
            this.FrameFolio.Controls.Add(this.lblNombrePersonal);
            this.FrameFolio.Controls.Add(this.txtFolio);
            this.FrameFolio.Controls.Add(this.label9);
            this.FrameFolio.Location = new System.Drawing.Point(7, 6);
            this.FrameFolio.Name = "FrameFolio";
            this.FrameFolio.Size = new System.Drawing.Size(481, 104);
            this.FrameFolio.TabIndex = 2;
            this.FrameFolio.TabStop = false;
            this.FrameFolio.Text = "Parametros de Folio";
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(89, 46);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(86, 21);
            this.lblPersonal.TabIndex = 1;
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(19, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 16);
            this.label11.TabIndex = 40;
            this.label11.Text = "Personal :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(89, 74);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaRegistro.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Fecha Registro :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombrePersonal
            // 
            this.lblNombrePersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombrePersonal.Location = new System.Drawing.Point(181, 46);
            this.lblNombrePersonal.Name = "lblNombrePersonal";
            this.lblNombrePersonal.Size = new System.Drawing.Size(294, 21);
            this.lblNombrePersonal.TabIndex = 2;
            this.lblNombrePersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(89, 20);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(86, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.TextChanged += new System.EventHandler(this.txtFolio_TextChanged);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(23, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 36;
            this.label9.Text = "Folio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmReportesUnitarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 325);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.FrameListaReportes);
            this.Controls.Add(this.tabReportes);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReportesUnitarios";
            this.Text = "Reportes Detallados de Dispensación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReportesUnitarios_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReportesUnitarios_FormClosing);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            this.FrameBeneficiario.ResumeLayout(false);
            this.FrameBeneficiario.PerformLayout();
            this.FrameListaReportes.ResumeLayout(false);
            this.menuBeneficiarios.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.tabReportes.ResumeLayout(false);
            this.pagBeneficiario.ResumeLayout(false);
            this.pagMedico.ResumeLayout(false);
            this.FrameMedico.ResumeLayout(false);
            this.FrameMedico.PerformLayout();
            this.pagClave.ResumeLayout(false);
            this.FrameClave.ResumeLayout(false);
            this.FrameClave.PerformLayout();
            this.pagFolio.ResumeLayout(false);
            this.FrameFolio.ResumeLayout(false);
            this.FrameFolio.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameBeneficiario;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCliente;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameListaReportes;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private SC_ControlsCS.scComboBoxExt cboReporte;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip menuBeneficiarios;
        private System.Windows.Forms.ToolStripMenuItem listadoDeBeneficiariosToolStripMenuItem;
        private System.Windows.Forms.TabControl tabReportes;
        private System.Windows.Forms.TabPage pagBeneficiario;
        private System.Windows.Forms.TabPage pagMedico;
        private System.Windows.Forms.TabPage pagClave;
        private System.Windows.Forms.TabPage pagFolio;
        private System.Windows.Forms.GroupBox FrameMedico;
        private System.Windows.Forms.Label lblMedico;
        private SC_ControlsCS.scTextBoxExt txtMedico;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox FrameClave;
        private System.Windows.Forms.Label lblClave;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox FrameFolio;
        private System.Windows.Forms.Label lblNombrePersonal;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBeneficiario;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPersonal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label lblIdEspecialidad;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblEspecialidad;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
    }
}