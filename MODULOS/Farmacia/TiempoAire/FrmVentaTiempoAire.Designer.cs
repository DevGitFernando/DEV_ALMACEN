namespace Farmacia.TiempoAire
{
    partial class FrmVentaTiempoAire
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVentaTiempoAire));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdCompanias = new FarPoint.Win.Spread.FpSpread();
            this.grdCompanias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdMontos = new FarPoint.Win.Spread.FpSpread();
            this.grdMontos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmCompanias = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConfirmar = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCelular = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAutoriza = new System.Windows.Forms.Label();
            this.txtIdAutoriza = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cboTipoVenta = new SC_ControlsCS.scComboBoxExt();
            this.txtCompania = new SC_ControlsCS.scTextBoxExt();
            this.txtIdMonto = new SC_ControlsCS.scTextBoxExt();
            this.lblDescMonto = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDescCompania = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonalTA = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmAutorizado = new System.Windows.Forms.Timer(this.components);
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanias_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMontos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMontos_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdCompanias);
            this.groupBox2.Location = new System.Drawing.Point(9, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 207);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compañias de Tiempo Aire";
            // 
            // grdCompanias
            // 
            this.grdCompanias.AccessibleDescription = "grdCompanias, Sheet1, Row 0, Column 0, ";
            this.grdCompanias.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCompanias.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdCompanias.Location = new System.Drawing.Point(12, 16);
            this.grdCompanias.Name = "grdCompanias";
            this.grdCompanias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCompanias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCompanias_Sheet1});
            this.grdCompanias.Size = new System.Drawing.Size(368, 185);
            this.grdCompanias.TabIndex = 0;
            this.grdCompanias.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // grdCompanias_Sheet1
            // 
            this.grdCompanias_Sheet1.Reset();
            this.grdCompanias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCompanias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCompanias_Sheet1.ColumnCount = 2;
            this.grdCompanias_Sheet1.RowCount = 8;
            this.grdCompanias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Compañia";
            this.grdCompanias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Compañia";
            this.grdCompanias_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdCompanias_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCompanias_Sheet1.Columns.Get(0).Label = "Id Compañia";
            this.grdCompanias_Sheet1.Columns.Get(0).Locked = true;
            this.grdCompanias_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCompanias_Sheet1.Columns.Get(0).Width = 80F;
            this.grdCompanias_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdCompanias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCompanias_Sheet1.Columns.Get(1).Label = "Compañia";
            this.grdCompanias_Sheet1.Columns.Get(1).Locked = true;
            this.grdCompanias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCompanias_Sheet1.Columns.Get(1).Width = 230F;
            this.grdCompanias_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdCompanias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdMontos);
            this.groupBox3.Location = new System.Drawing.Point(407, 30);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 207);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Listado de Montos";
            // 
            // grdMontos
            // 
            this.grdMontos.AccessibleDescription = "grdMontos, Sheet1, Row 0, Column 0, ";
            this.grdMontos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMontos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdMontos.Location = new System.Drawing.Point(12, 16);
            this.grdMontos.Name = "grdMontos";
            this.grdMontos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMontos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMontos_Sheet1});
            this.grdMontos.Size = new System.Drawing.Size(368, 185);
            this.grdMontos.TabIndex = 0;
            this.grdMontos.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // grdMontos_Sheet1
            // 
            this.grdMontos_Sheet1.Reset();
            this.grdMontos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMontos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMontos_Sheet1.ColumnCount = 4;
            this.grdMontos_Sheet1.RowCount = 8;
            this.grdMontos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdCompania";
            this.grdMontos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Monto";
            this.grdMontos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripcion";
            this.grdMontos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Monto";
            this.grdMontos_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdMontos_Sheet1.Columns.Get(0).Label = "IdCompania";
            this.grdMontos_Sheet1.Columns.Get(0).Visible = false;
            this.grdMontos_Sheet1.Columns.Get(0).Width = 72F;
            this.grdMontos_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdMontos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMontos_Sheet1.Columns.Get(1).Label = "Monto";
            this.grdMontos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMontos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMontos_Sheet1.Columns.Get(1).Width = 80F;
            this.grdMontos_Sheet1.Columns.Get(2).CellType = textCellType5;
            this.grdMontos_Sheet1.Columns.Get(2).Label = "Descripcion";
            this.grdMontos_Sheet1.Columns.Get(2).Width = 230F;
            this.grdMontos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdMontos_Sheet1.Columns.Get(3).Label = "Monto";
            this.grdMontos_Sheet1.Columns.Get(3).Visible = false;
            this.grdMontos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdMontos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmCompanias
            // 
            this.tmCompanias.Tick += new System.EventHandler(this.tmCompanias_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConfirmar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCelular);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblAutoriza);
            this.groupBox1.Controls.Add(this.txtIdAutoriza);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cboTipoVenta);
            this.groupBox1.Controls.Add(this.txtCompania);
            this.groupBox1.Controls.Add(this.txtIdMonto);
            this.groupBox1.Controls.Add(this.lblDescMonto);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblDescCompania);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblPersonal);
            this.groupBox1.Controls.Add(this.txtIdPersonalTA);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 199);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la Venta";
            // 
            // txtConfirmar
            // 
            this.txtConfirmar.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConfirmar.Decimales = 2;
            this.txtConfirmar.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmar.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmar.Location = new System.Drawing.Point(316, 146);
            this.txtConfirmar.MaxLength = 20;
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.PermitirApostrofo = false;
            this.txtConfirmar.PermitirNegativos = false;
            this.txtConfirmar.Size = new System.Drawing.Size(232, 38);
            this.txtConfirmar.TabIndex = 6;
            this.txtConfirmar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(103, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 34);
            this.label5.TabIndex = 38;
            this.label5.Text = "Confirmar Numero :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCelular
            // 
            this.txtCelular.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCelular.Decimales = 2;
            this.txtCelular.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCelular.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCelular.ForeColor = System.Drawing.Color.Black;
            this.txtCelular.Location = new System.Drawing.Point(316, 100);
            this.txtCelular.MaxLength = 20;
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.PermitirApostrofo = false;
            this.txtCelular.PermitirNegativos = false;
            this.txtCelular.Size = new System.Drawing.Size(232, 38);
            this.txtCelular.TabIndex = 5;
            this.txtCelular.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(160, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 28);
            this.label3.TabIndex = 36;
            this.label3.Text = "Num. Celular :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAutoriza
            // 
            this.lblAutoriza.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAutoriza.Location = new System.Drawing.Point(152, 81);
            this.lblAutoriza.Name = "lblAutoriza";
            this.lblAutoriza.Size = new System.Drawing.Size(51, 20);
            this.lblAutoriza.TabIndex = 34;
            this.lblAutoriza.Visible = false;
            // 
            // txtIdAutoriza
            // 
            this.txtIdAutoriza.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdAutoriza.Decimales = 2;
            this.txtIdAutoriza.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdAutoriza.ForeColor = System.Drawing.Color.Black;
            this.txtIdAutoriza.Location = new System.Drawing.Point(92, 81);
            this.txtIdAutoriza.MaxLength = 4;
            this.txtIdAutoriza.Name = "txtIdAutoriza";
            this.txtIdAutoriza.PermitirApostrofo = false;
            this.txtIdAutoriza.PermitirNegativos = false;
            this.txtIdAutoriza.Size = new System.Drawing.Size(54, 20);
            this.txtIdAutoriza.TabIndex = 4;
            this.txtIdAutoriza.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdAutoriza.Visible = false;
            this.txtIdAutoriza.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdAutoriza_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(30, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Autoriza :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(13, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Tipo Venta :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoVenta
            // 
            this.cboTipoVenta.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoVenta.Data = "";
            this.cboTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoVenta.FormattingEnabled = true;
            this.cboTipoVenta.Location = new System.Drawing.Point(92, 54);
            this.cboTipoVenta.Name = "cboTipoVenta";
            this.cboTipoVenta.Size = new System.Drawing.Size(297, 21);
            this.cboTipoVenta.TabIndex = 2;
            this.cboTipoVenta.SelectedIndexChanged += new System.EventHandler(this.cboTipoVenta_SelectedIndexChanged);
            // 
            // txtCompania
            // 
            this.txtCompania.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCompania.Decimales = 2;
            this.txtCompania.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCompania.ForeColor = System.Drawing.Color.Black;
            this.txtCompania.Location = new System.Drawing.Point(92, 25);
            this.txtCompania.MaxLength = 2;
            this.txtCompania.Name = "txtCompania";
            this.txtCompania.PermitirApostrofo = false;
            this.txtCompania.PermitirNegativos = false;
            this.txtCompania.Size = new System.Drawing.Size(54, 20);
            this.txtCompania.TabIndex = 0;
            this.txtCompania.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCompania.TextChanged += new System.EventHandler(this.txtCompania_TextChanged);
            this.txtCompania.Validating += new System.ComponentModel.CancelEventHandler(this.txtCompania_Validating);
            // 
            // txtIdMonto
            // 
            this.txtIdMonto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdMonto.Decimales = 2;
            this.txtIdMonto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdMonto.ForeColor = System.Drawing.Color.Black;
            this.txtIdMonto.Location = new System.Drawing.Point(483, 25);
            this.txtIdMonto.MaxLength = 2;
            this.txtIdMonto.Name = "txtIdMonto";
            this.txtIdMonto.PermitirApostrofo = false;
            this.txtIdMonto.PermitirNegativos = false;
            this.txtIdMonto.Size = new System.Drawing.Size(54, 20);
            this.txtIdMonto.TabIndex = 1;
            this.txtIdMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdMonto.TextChanged += new System.EventHandler(this.txtMonto_TextChanged);
            this.txtIdMonto.Validating += new System.ComponentModel.CancelEventHandler(this.txtMonto_Validating);
            // 
            // lblDescMonto
            // 
            this.lblDescMonto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescMonto.Location = new System.Drawing.Point(543, 25);
            this.lblDescMonto.Name = "lblDescMonto";
            this.lblDescMonto.Size = new System.Drawing.Size(232, 20);
            this.lblDescMonto.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(420, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Monto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescCompania
            // 
            this.lblDescCompania.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescCompania.Location = new System.Drawing.Point(152, 25);
            this.lblDescCompania.Name = "lblDescCompania";
            this.lblDescCompania.Size = new System.Drawing.Size(237, 20);
            this.lblDescCompania.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Compañia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(543, 54);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(232, 20);
            this.lblPersonal.TabIndex = 7;
            // 
            // txtIdPersonalTA
            // 
            this.txtIdPersonalTA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonalTA.Decimales = 2;
            this.txtIdPersonalTA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonalTA.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonalTA.Location = new System.Drawing.Point(483, 54);
            this.txtIdPersonalTA.MaxLength = 4;
            this.txtIdPersonalTA.Name = "txtIdPersonalTA";
            this.txtIdPersonalTA.PermitirApostrofo = false;
            this.txtIdPersonalTA.PermitirNegativos = false;
            this.txtIdPersonalTA.Size = new System.Drawing.Size(54, 20);
            this.txtIdPersonalTA.TabIndex = 3;
            this.txtIdPersonalTA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPersonalTA.TextChanged += new System.EventHandler(this.txtIdPersonal_TextChanged);
            this.txtIdPersonalTA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPersonalTA_KeyDown);
            this.txtIdPersonalTA.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonal_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(410, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cliente  TA :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(806, 25);
            this.toolStripBarraMenu.TabIndex = 13;
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
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click_1);
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
            // tmAutorizado
            // 
            this.tmAutorizado.Tick += new System.EventHandler(this.tmAutorizado_Tick);
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // FrmVentaTiempoAire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 448);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmVentaTiempoAire";
            this.Text = "Registro de Tiempo Aire Cargado";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVentaTiempoAire_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanias_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMontos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMontos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdCompanias;
        private FarPoint.Win.Spread.SheetView grdCompanias_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdMontos;
        private FarPoint.Win.Spread.SheetView grdMontos_Sheet1;
        private System.Windows.Forms.Timer tmCompanias;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdPersonalTA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblPersonal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescCompania;
        private System.Windows.Forms.Label lblDescMonto;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtCompania;
        private SC_ControlsCS.scTextBoxExt txtIdMonto;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scComboBoxExt cboTipoVenta;
        private System.Windows.Forms.Label lblAutoriza;
        private SC_ControlsCS.scTextBoxExt txtIdAutoriza;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtCelular;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtConfirmar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer tmAutorizado;
        private System.Windows.Forms.Timer tmSesion;
    }
}