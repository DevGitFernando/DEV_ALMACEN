namespace DllPedidosClientes.Reportes
{
    partial class FrmSurtimientoRecetas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSurtimientoRecetas));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNoSurtidoPzas = new System.Windows.Forms.Label();
            this.lblValesPzas = new System.Windows.Forms.Label();
            this.lblSurtidoPzas = new System.Windows.Forms.Label();
            this.lblNoSurtido = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblVales = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSurtidos = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblFolios = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grdResumen = new FarPoint.Win.Spread.FpSpread();
            this.grdResumen_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResumen_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(507, 25);
            this.toolStripBarraMenu.TabIndex = 14;
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
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.lblSubCte);
            this.FrameCliente.Controls.Add(this.txtSubCte);
            this.FrameCliente.Controls.Add(this.label1);
            this.FrameCliente.Controls.Add(this.lblCte);
            this.FrameCliente.Controls.Add(this.txtCte);
            this.FrameCliente.Controls.Add(this.label3);
            this.FrameCliente.Location = new System.Drawing.Point(10, 98);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(486, 71);
            this.FrameCliente.TabIndex = 1;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Parametros de Cliente";
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(154, 45);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(318, 21);
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
            this.lblCte.Size = new System.Drawing.Size(318, 21);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblFarmacia);
            this.groupBox5.Controls.Add(this.txtFarmacia);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(10, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(486, 72);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(153, 44);
            this.lblFarmacia.MostrarToolTip = false;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(319, 20);
            this.lblFarmacia.TabIndex = 26;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(89, 44);
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
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(89, 105);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(388, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(25, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 24;
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
            this.cboEstados.Location = new System.Drawing.Point(89, 16);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(383, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(31, 20);
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
            this.FrameFechas.Location = new System.Drawing.Point(10, 170);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(486, 50);
            this.FrameFechas.TabIndex = 19;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(300, 17);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(268, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(95, 20);
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
            this.dtpFechaInicial.Location = new System.Drawing.Point(138, 17);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNoSurtidoPzas);
            this.groupBox1.Controls.Add(this.lblValesPzas);
            this.groupBox1.Controls.Add(this.lblSurtidoPzas);
            this.groupBox1.Controls.Add(this.lblNoSurtido);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lblVales);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblSurtidos);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblFolios);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(10, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 134);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resumen de surtimiento recetas";
            // 
            // lblNoSurtidoPzas
            // 
            this.lblNoSurtidoPzas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNoSurtidoPzas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoSurtidoPzas.Location = new System.Drawing.Point(199, 104);
            this.lblNoSurtidoPzas.Name = "lblNoSurtidoPzas";
            this.lblNoSurtidoPzas.Size = new System.Drawing.Size(82, 21);
            this.lblNoSurtidoPzas.TabIndex = 48;
            this.lblNoSurtidoPzas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblValesPzas
            // 
            this.lblValesPzas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblValesPzas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValesPzas.Location = new System.Drawing.Point(199, 78);
            this.lblValesPzas.Name = "lblValesPzas";
            this.lblValesPzas.Size = new System.Drawing.Size(82, 21);
            this.lblValesPzas.TabIndex = 47;
            this.lblValesPzas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSurtidoPzas
            // 
            this.lblSurtidoPzas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSurtidoPzas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurtidoPzas.Location = new System.Drawing.Point(199, 53);
            this.lblSurtidoPzas.Name = "lblSurtidoPzas";
            this.lblSurtidoPzas.Size = new System.Drawing.Size(82, 21);
            this.lblSurtidoPzas.TabIndex = 46;
            this.lblSurtidoPzas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoSurtido
            // 
            this.lblNoSurtido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNoSurtido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoSurtido.Location = new System.Drawing.Point(287, 104);
            this.lblNoSurtido.Name = "lblNoSurtido";
            this.lblNoSurtido.Size = new System.Drawing.Size(82, 21);
            this.lblNoSurtido.TabIndex = 45;
            this.lblNoSurtido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(33, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(163, 16);
            this.label14.TabIndex = 44;
            this.label14.Text = "No surtido :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVales
            // 
            this.lblVales.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVales.Location = new System.Drawing.Point(287, 78);
            this.lblVales.Name = "lblVales";
            this.lblVales.Size = new System.Drawing.Size(82, 21);
            this.lblVales.TabIndex = 43;
            this.lblVales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(33, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 16);
            this.label12.TabIndex = 42;
            this.label12.Text = "Rectas surtidas por Vales :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSurtidos
            // 
            this.lblSurtidos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSurtidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurtidos.Location = new System.Drawing.Point(287, 53);
            this.lblSurtidos.Name = "lblSurtidos";
            this.lblSurtidos.Size = new System.Drawing.Size(82, 21);
            this.lblSurtidos.TabIndex = 41;
            this.lblSurtidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(33, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 16);
            this.label10.TabIndex = 40;
            this.label10.Text = "Recetas surtidas completas :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFolios
            // 
            this.lblFolios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFolios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolios.Location = new System.Drawing.Point(356, 18);
            this.lblFolios.Name = "lblFolios";
            this.lblFolios.Size = new System.Drawing.Size(116, 21);
            this.lblFolios.TabIndex = 39;
            this.lblFolios.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(274, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 16);
            this.label9.TabIndex = 38;
            this.label9.Text = "Recetas :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdResumen
            // 
            this.grdResumen.AccessibleDescription = "grdResumen, Sheet1, Row 0, Column 0, ";
            this.grdResumen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdResumen.Location = new System.Drawing.Point(15, 16);
            this.grdResumen.Name = "grdResumen";
            this.grdResumen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdResumen.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdResumen_Sheet1});
            this.grdResumen.Size = new System.Drawing.Size(456, 131);
            this.grdResumen.TabIndex = 46;
            this.grdResumen.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdResumen_CellDoubleClick);
            // 
            // grdResumen_Sheet1
            // 
            this.grdResumen_Sheet1.Reset();
            this.grdResumen_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdResumen_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdResumen_Sheet1.ColumnCount = 5;
            this.grdResumen_Sheet1.RowCount = 4;
            this.grdResumen_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Tipo Dispensación";
            this.grdResumen_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Claves";
            this.grdResumen_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Piezas";
            this.grdResumen_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Porcentaje Piezas";
            this.grdResumen_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Tipo";
            this.grdResumen_Sheet1.ColumnHeader.Rows.Get(0).Height = 29F;
            this.grdResumen_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdResumen_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdResumen_Sheet1.Columns.Get(0).Label = "Tipo Dispensación";
            this.grdResumen_Sheet1.Columns.Get(0).Locked = true;
            this.grdResumen_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(0).Width = 180F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000;
            numberCellType1.MinimumValue = 0;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdResumen_Sheet1.Columns.Get(1).CellType = numberCellType1;
            this.grdResumen_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(1).Label = "Claves";
            this.grdResumen_Sheet1.Columns.Get(1).Locked = true;
            this.grdResumen_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(1).Width = 70F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000;
            numberCellType2.MinimumValue = 0;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdResumen_Sheet1.Columns.Get(2).CellType = numberCellType2;
            this.grdResumen_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(2).Label = "Piezas";
            this.grdResumen_Sheet1.Columns.Get(2).Locked = true;
            this.grdResumen_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(2).Width = 70F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.Separator = ",";
            this.grdResumen_Sheet1.Columns.Get(3).CellType = numberCellType3;
            this.grdResumen_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdResumen_Sheet1.Columns.Get(3).Label = "Porcentaje Piezas";
            this.grdResumen_Sheet1.Columns.Get(3).Locked = true;
            this.grdResumen_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(3).Width = 79F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 10000000;
            numberCellType4.MinimumValue = 0;
            this.grdResumen_Sheet1.Columns.Get(4).CellType = numberCellType4;
            this.grdResumen_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(4).Label = "Tipo";
            this.grdResumen_Sheet1.Columns.Get(4).Locked = true;
            this.grdResumen_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResumen_Sheet1.Columns.Get(4).Visible = false;
            this.grdResumen_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdResumen_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdResumen);
            this.groupBox2.Location = new System.Drawing.Point(10, 355);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 155);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resumen de Claves y Piezas";
            // 
            // FrmSurtimientoRecetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 512);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmSurtimientoRecetas";
            this.Text = "Surtimiento de Recetas y Medicamentos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSurtimientoRecetas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResumen_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox FrameCliente;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFolios;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNoSurtido;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblVales;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSurtidos;
        private System.Windows.Forms.Label label10;
        private FarPoint.Win.Spread.FpSpread grdResumen;
        private FarPoint.Win.Spread.SheetView grdResumen_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblNoSurtidoPzas;
        private System.Windows.Forms.Label lblValesPzas;
        private System.Windows.Forms.Label lblSurtidoPzas;
    }
}