namespace DllFarmaciaSoft.Ventas
{
    partial class FrmConsultarExistenciasEnFarmacias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultarExistenciasEnFarmacias));
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodEAN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.rdoFarmacia = new System.Windows.Forms.RadioButton();
            this.rdoTodas = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.lblFinError = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1149, 27);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(29, 24);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(29, 24);
            this.btnImprimir.Text = "&Imprimir";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.lblDescripcionClave);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCodEAN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(453, 416);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(309, 34);
            this.lblClaveSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(136, 25);
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
            this.txtClaveSSA.Location = new System.Drawing.Point(101, 34);
            this.txtClaveSSA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(199, 22);
            this.txtClaveSSA.TabIndex = 1;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Location = new System.Drawing.Point(13, 65);
            this.lblDescripcionClave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(432, 106);
            this.lblDescripcionClave.TabIndex = 10;
            this.lblDescripcionClave.Text = "label2";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clave SSA :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodEAN
            // 
            this.txtCodEAN.Location = new System.Drawing.Point(133, 351);
            this.txtCodEAN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCodEAN.MaxLength = 15;
            this.txtCodEAN.Name = "txtCodEAN";
            this.txtCodEAN.Size = new System.Drawing.Size(263, 22);
            this.txtCodEAN.TabIndex = 3;
            this.txtCodEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodEAN_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 354);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Código EAN :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Enabled = false;
            this.lblDescripcion.Location = new System.Drawing.Point(13, 213);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(432, 116);
            this.lblDescripcion.TabIndex = 5;
            this.lblDescripcion.Text = "label2";
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(127, 183);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtId.MaxLength = 8;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(107, 22);
            this.txtId.TabIndex = 2;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código Sistema :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboFarmacias);
            this.groupBox2.Controls.Add(this.rdoFarmacia);
            this.groupBox2.Controls.Add(this.rdoTodas);
            this.groupBox2.Location = new System.Drawing.Point(477, 34);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(661, 64);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Farmacia a Consultar";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(229, 23);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(413, 24);
            this.cboFarmacias.TabIndex = 2;
            // 
            // rdoFarmacia
            // 
            this.rdoFarmacia.Location = new System.Drawing.Point(136, 26);
            this.rdoFarmacia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoFarmacia.Name = "rdoFarmacia";
            this.rdoFarmacia.Size = new System.Drawing.Size(92, 21);
            this.rdoFarmacia.TabIndex = 1;
            this.rdoFarmacia.Text = "Farmacia";
            this.rdoFarmacia.UseVisualStyleBackColor = true;
            this.rdoFarmacia.CheckedChanged += new System.EventHandler(this.rdoFarmacia_CheckedChanged);
            // 
            // rdoTodas
            // 
            this.rdoTodas.Location = new System.Drawing.Point(27, 26);
            this.rdoTodas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoTodas.Name = "rdoTodas";
            this.rdoTodas.Size = new System.Drawing.Size(92, 21);
            this.rdoTodas.TabIndex = 0;
            this.rdoTodas.Text = "Todas";
            this.rdoTodas.UseVisualStyleBackColor = true;
            this.rdoTodas.CheckedChanged += new System.EventHandler(this.rdoTodas_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdExistencia);
            this.groupBox3.Location = new System.Drawing.Point(477, 106);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(661, 345);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Existencias";
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(15, 22);
            this.grdExistencia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(632, 313);
            this.grdExistencia.TabIndex = 0;
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 4;
            this.grdExistencia_Sheet1.RowCount = 10;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Existencia";
            textCellType13.MaxLength = 6;
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType13;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 68F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType14;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 264F;
            textCellType15.MaxLength = 500;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType15;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Url";
            this.grdExistencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(2).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 347F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = -1D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdExistencia_Sheet1.Columns.Get(3).CellType = numberCellType5;
            this.grdExistencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Label = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Width = 87F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(875, 22);
            this.lblFinError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(33, 31);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(705, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Consulta Erronea :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(396, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Consulta Exitosa :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(545, 22);
            this.lblFinExito.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(33, 31);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(97, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblConsultando
            // 
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(216, 22);
            this.lblConsultando.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(33, 31);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(16, 458);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(1123, 64);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de Colores para Consulta de Existencia";
            // 
            // FrmConsultarExistenciasEnFarmacias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 530);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmConsultarExistenciasEnFarmacias";
            this.ShowIcon = false;
            this.Text = "Consulta Inventario otras Farmacias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConsultarExistenciasEnFarmacias_FormClosing);
            this.Load += new System.EventHandler(this.FrmConsultarExistenciasEnFarmacias_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConsultarExistenciasEnFarmacias_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCodEAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.RadioButton rdoFarmacia;
        private System.Windows.Forms.RadioButton rdoTodas;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}