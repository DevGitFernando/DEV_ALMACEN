namespace Dll_SII_IMediaccess.Ventas
{
    partial class FrmRecetasManuales
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecetasManuales));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConsecutivo = new SC_ControlsCS.scTextBoxExt();
            this.txtPlanBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEspecialidad = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombreMedico = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFolioReferencia = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNombreBeneficiario = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaDeReceta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolioReceta = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus3 = new SC_ControlsCS.scLabelStatus();
            this.txtFolioElegibilidad = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus1 = new SC_ControlsCS.scLabelStatus();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDiagnostico_04 = new System.Windows.Forms.Label();
            this.txtIdDiagnostico_04 = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiagnostico_03 = new System.Windows.Forms.Label();
            this.txtIdDiagnostico_03 = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDiagnostico_02 = new System.Windows.Forms.Label();
            this.txtIdDiagnostico_02 = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDiagnostico_01 = new System.Windows.Forms.Label();
            this.txtIdDiagnostico_01 = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRegistrarMedicos = new System.Windows.Forms.Button();
            this.lblMedico = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(7, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(929, 244);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Receta";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(10, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(909, 222);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 6;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(1, 3).Locked = false;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Tasa de IVA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType5.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType5.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            textCellType6.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType6.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Código";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 105F;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 558F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 100D;
            numberCellType3.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Tasa de IVA";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Visible = false;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.DecimalSeparator = ".";
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 70F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(944, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRegistrarMedicos);
            this.groupBox1.Controls.Add(this.lblMedico);
            this.groupBox1.Controls.Add(this.txtConsecutivo);
            this.groupBox1.Controls.Add(this.txtPlanBeneficiario);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtEspecialidad);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNombreMedico);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblFolioReferencia);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblNombreBeneficiario);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpFechaDeReceta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFolioReceta);
            this.groupBox1.Controls.Add(this.scLabelStatus3);
            this.groupBox1.Controls.Add(this.txtFolioElegibilidad);
            this.groupBox1.Controls.Add(this.scLabelStatus1);
            this.groupBox1.Location = new System.Drawing.Point(7, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(929, 124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información general de receta";
            // 
            // txtConsecutivo
            // 
            this.txtConsecutivo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtConsecutivo.Decimales = 2;
            this.txtConsecutivo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtConsecutivo.ForeColor = System.Drawing.Color.Black;
            this.txtConsecutivo.Location = new System.Drawing.Point(595, 23);
            this.txtConsecutivo.MaxLength = 50;
            this.txtConsecutivo.Name = "txtConsecutivo";
            this.txtConsecutivo.PermitirApostrofo = false;
            this.txtConsecutivo.PermitirNegativos = false;
            this.txtConsecutivo.Size = new System.Drawing.Size(57, 20);
            this.txtConsecutivo.TabIndex = 61;
            this.txtConsecutivo.Text = "01";
            this.txtConsecutivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPlanBeneficiario
            // 
            this.txtPlanBeneficiario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlanBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPlanBeneficiario.Decimales = 2;
            this.txtPlanBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPlanBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtPlanBeneficiario.Location = new System.Drawing.Point(135, 72);
            this.txtPlanBeneficiario.MaxLength = 150;
            this.txtPlanBeneficiario.Name = "txtPlanBeneficiario";
            this.txtPlanBeneficiario.PermitirApostrofo = false;
            this.txtPlanBeneficiario.PermitirNegativos = false;
            this.txtPlanBeneficiario.Size = new System.Drawing.Size(456, 20);
            this.txtPlanBeneficiario.TabIndex = 3;
            this.txtPlanBeneficiario.Text = "E006493943";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(14, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 16);
            this.label9.TabIndex = 60;
            this.label9.Text = "Plan : ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEspecialidad
            // 
            this.txtEspecialidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEspecialidad.Decimales = 2;
            this.txtEspecialidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtEspecialidad.ForeColor = System.Drawing.Color.Black;
            this.txtEspecialidad.Location = new System.Drawing.Point(700, 96);
            this.txtEspecialidad.MaxLength = 50;
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.PermitirApostrofo = false;
            this.txtEspecialidad.PermitirNegativos = false;
            this.txtEspecialidad.Size = new System.Drawing.Size(218, 20);
            this.txtEspecialidad.TabIndex = 5;
            this.txtEspecialidad.Text = "E006493943";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(615, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 58;
            this.label8.Text = "Especialidad: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombreMedico
            // 
            this.txtNombreMedico.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombreMedico.Decimales = 2;
            this.txtNombreMedico.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombreMedico.ForeColor = System.Drawing.Color.Black;
            this.txtNombreMedico.Location = new System.Drawing.Point(135, 96);
            this.txtNombreMedico.MaxLength = 150;
            this.txtNombreMedico.Name = "txtNombreMedico";
            this.txtNombreMedico.PermitirApostrofo = false;
            this.txtNombreMedico.PermitirNegativos = false;
            this.txtNombreMedico.Size = new System.Drawing.Size(85, 20);
            this.txtNombreMedico.TabIndex = 4;
            this.txtNombreMedico.Text = "E006493943";
            this.txtNombreMedico.TextChanged += new System.EventHandler(this.txtNombreMedico_TextChanged);
            this.txtNombreMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreMedico_KeyDown);
            this.txtNombreMedico.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombreMedico_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 16);
            this.label7.TabIndex = 56;
            this.label7.Text = "Nombre médico : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFolioReferencia
            // 
            this.lblFolioReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFolioReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolioReferencia.Location = new System.Drawing.Point(730, 48);
            this.lblFolioReferencia.Name = "lblFolioReferencia";
            this.lblFolioReferencia.Size = new System.Drawing.Size(188, 20);
            this.lblFolioReferencia.TabIndex = 54;
            this.lblFolioReferencia.Text = "Nombre :";
            this.lblFolioReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(620, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "Folio-Referencia :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreBeneficiario
            // 
            this.lblNombreBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreBeneficiario.Location = new System.Drawing.Point(135, 48);
            this.lblNombreBeneficiario.Name = "lblNombreBeneficiario";
            this.lblNombreBeneficiario.Size = new System.Drawing.Size(456, 20);
            this.lblNombreBeneficiario.TabIndex = 52;
            this.lblNombreBeneficiario.Text = "Nombre :";
            this.lblNombreBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 53;
            this.label5.Text = "Nombre beneficiario : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDeReceta
            // 
            this.dtpFechaDeReceta.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeReceta.Enabled = false;
            this.dtpFechaDeReceta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeReceta.Location = new System.Drawing.Point(827, 23);
            this.dtpFechaDeReceta.Name = "dtpFechaDeReceta";
            this.dtpFechaDeReceta.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaDeReceta.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(727, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Fecha de Receta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioReceta
            // 
            this.txtFolioReceta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioReceta.Decimales = 2;
            this.txtFolioReceta.Enabled = false;
            this.txtFolioReceta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioReceta.ForeColor = System.Drawing.Color.Black;
            this.txtFolioReceta.Location = new System.Drawing.Point(462, 23);
            this.txtFolioReceta.MaxLength = 50;
            this.txtFolioReceta.Name = "txtFolioReceta";
            this.txtFolioReceta.PermitirApostrofo = false;
            this.txtFolioReceta.PermitirNegativos = false;
            this.txtFolioReceta.Size = new System.Drawing.Size(128, 20);
            this.txtFolioReceta.TabIndex = 1;
            this.txtFolioReceta.Text = "E006493943";
            this.txtFolioReceta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus3
            // 
            this.scLabelStatus3.Location = new System.Drawing.Point(364, 23);
            this.scLabelStatus3.Name = "scLabelStatus3";
            this.scLabelStatus3.Size = new System.Drawing.Size(97, 20);
            this.scLabelStatus3.TabIndex = 4;
            this.scLabelStatus3.Text = "Folio de receta : ";
            this.scLabelStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioElegibilidad
            // 
            this.txtFolioElegibilidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioElegibilidad.Decimales = 2;
            this.txtFolioElegibilidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioElegibilidad.ForeColor = System.Drawing.Color.Black;
            this.txtFolioElegibilidad.Location = new System.Drawing.Point(135, 22);
            this.txtFolioElegibilidad.MaxLength = 50;
            this.txtFolioElegibilidad.Name = "txtFolioElegibilidad";
            this.txtFolioElegibilidad.PermitirApostrofo = false;
            this.txtFolioElegibilidad.PermitirNegativos = false;
            this.txtFolioElegibilidad.Size = new System.Drawing.Size(128, 20);
            this.txtFolioElegibilidad.TabIndex = 0;
            this.txtFolioElegibilidad.Text = "E006493943";
            this.txtFolioElegibilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioElegibilidad.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioElegibilidad_Validating);
            // 
            // scLabelStatus1
            // 
            this.scLabelStatus1.Location = new System.Drawing.Point(14, 21);
            this.scLabelStatus1.Name = "scLabelStatus1";
            this.scLabelStatus1.Size = new System.Drawing.Size(123, 20);
            this.scLabelStatus1.TabIndex = 0;
            this.scLabelStatus1.Text = "Elegibilidad : ";
            this.scLabelStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDiagnostico_04);
            this.groupBox3.Controls.Add(this.txtIdDiagnostico_04);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblDiagnostico_03);
            this.groupBox3.Controls.Add(this.txtIdDiagnostico_03);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblDiagnostico_02);
            this.groupBox3.Controls.Add(this.txtIdDiagnostico_02);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblDiagnostico_01);
            this.groupBox3.Controls.Add(this.txtIdDiagnostico_01);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(7, 154);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(929, 76);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Diagnósticos";
            // 
            // lblDiagnostico_04
            // 
            this.lblDiagnostico_04.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiagnostico_04.Location = new System.Drawing.Point(597, 43);
            this.lblDiagnostico_04.Name = "lblDiagnostico_04";
            this.lblDiagnostico_04.Size = new System.Drawing.Size(317, 20);
            this.lblDiagnostico_04.TabIndex = 26;
            this.lblDiagnostico_04.Text = "Nombre :";
            // 
            // txtIdDiagnostico_04
            // 
            this.txtIdDiagnostico_04.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDiagnostico_04.Decimales = 2;
            this.txtIdDiagnostico_04.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdDiagnostico_04.ForeColor = System.Drawing.Color.Black;
            this.txtIdDiagnostico_04.Location = new System.Drawing.Point(531, 43);
            this.txtIdDiagnostico_04.MaxLength = 4;
            this.txtIdDiagnostico_04.Name = "txtIdDiagnostico_04";
            this.txtIdDiagnostico_04.PermitirApostrofo = false;
            this.txtIdDiagnostico_04.PermitirNegativos = false;
            this.txtIdDiagnostico_04.Size = new System.Drawing.Size(60, 20);
            this.txtIdDiagnostico_04.TabIndex = 3;
            this.txtIdDiagnostico_04.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDiagnostico_04.TextChanged += new System.EventHandler(this.txtIdDiagnostico_04_TextChanged);
            this.txtIdDiagnostico_04.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDiagnostico_04_KeyDown);
            this.txtIdDiagnostico_04.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDiagnostico_04_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(468, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 27;
            this.label6.Text = "CIE 10 - 4 :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiagnostico_03
            // 
            this.lblDiagnostico_03.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiagnostico_03.Location = new System.Drawing.Point(597, 19);
            this.lblDiagnostico_03.Name = "lblDiagnostico_03";
            this.lblDiagnostico_03.Size = new System.Drawing.Size(317, 20);
            this.lblDiagnostico_03.TabIndex = 23;
            this.lblDiagnostico_03.Text = "Nombre :";
            // 
            // txtIdDiagnostico_03
            // 
            this.txtIdDiagnostico_03.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDiagnostico_03.Decimales = 2;
            this.txtIdDiagnostico_03.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdDiagnostico_03.ForeColor = System.Drawing.Color.Black;
            this.txtIdDiagnostico_03.Location = new System.Drawing.Point(531, 19);
            this.txtIdDiagnostico_03.MaxLength = 4;
            this.txtIdDiagnostico_03.Name = "txtIdDiagnostico_03";
            this.txtIdDiagnostico_03.PermitirApostrofo = false;
            this.txtIdDiagnostico_03.PermitirNegativos = false;
            this.txtIdDiagnostico_03.Size = new System.Drawing.Size(60, 20);
            this.txtIdDiagnostico_03.TabIndex = 2;
            this.txtIdDiagnostico_03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDiagnostico_03.TextChanged += new System.EventHandler(this.txtIdDiagnostico_03_TextChanged);
            this.txtIdDiagnostico_03.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDiagnostico_03_KeyDown);
            this.txtIdDiagnostico_03.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDiagnostico_03_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(468, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "CIE 10 - 3 :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiagnostico_02
            // 
            this.lblDiagnostico_02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiagnostico_02.Location = new System.Drawing.Point(143, 44);
            this.lblDiagnostico_02.Name = "lblDiagnostico_02";
            this.lblDiagnostico_02.Size = new System.Drawing.Size(317, 20);
            this.lblDiagnostico_02.TabIndex = 20;
            this.lblDiagnostico_02.Text = "Nombre :";
            // 
            // txtIdDiagnostico_02
            // 
            this.txtIdDiagnostico_02.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDiagnostico_02.Decimales = 2;
            this.txtIdDiagnostico_02.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdDiagnostico_02.ForeColor = System.Drawing.Color.Black;
            this.txtIdDiagnostico_02.Location = new System.Drawing.Point(77, 44);
            this.txtIdDiagnostico_02.MaxLength = 4;
            this.txtIdDiagnostico_02.Name = "txtIdDiagnostico_02";
            this.txtIdDiagnostico_02.PermitirApostrofo = false;
            this.txtIdDiagnostico_02.PermitirNegativos = false;
            this.txtIdDiagnostico_02.Size = new System.Drawing.Size(60, 20);
            this.txtIdDiagnostico_02.TabIndex = 1;
            this.txtIdDiagnostico_02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDiagnostico_02.TextChanged += new System.EventHandler(this.txtIdDiagnostico_02_TextChanged);
            this.txtIdDiagnostico_02.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDiagnostico_02_KeyDown);
            this.txtIdDiagnostico_02.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDiagnostico_02_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "CIE 10 - 2 :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiagnostico_01
            // 
            this.lblDiagnostico_01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiagnostico_01.Location = new System.Drawing.Point(143, 20);
            this.lblDiagnostico_01.Name = "lblDiagnostico_01";
            this.lblDiagnostico_01.Size = new System.Drawing.Size(317, 20);
            this.lblDiagnostico_01.TabIndex = 17;
            this.lblDiagnostico_01.Text = "Nombre :";
            // 
            // txtIdDiagnostico_01
            // 
            this.txtIdDiagnostico_01.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDiagnostico_01.Decimales = 2;
            this.txtIdDiagnostico_01.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdDiagnostico_01.ForeColor = System.Drawing.Color.Black;
            this.txtIdDiagnostico_01.Location = new System.Drawing.Point(77, 20);
            this.txtIdDiagnostico_01.MaxLength = 4;
            this.txtIdDiagnostico_01.Name = "txtIdDiagnostico_01";
            this.txtIdDiagnostico_01.PermitirApostrofo = false;
            this.txtIdDiagnostico_01.PermitirNegativos = false;
            this.txtIdDiagnostico_01.Size = new System.Drawing.Size(60, 20);
            this.txtIdDiagnostico_01.TabIndex = 0;
            this.txtIdDiagnostico_01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDiagnostico_01.TextChanged += new System.EventHandler(this.txtIdDiagnostico_01_TextChanged);
            this.txtIdDiagnostico_01.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDiagnostico_01_KeyDown);
            this.txtIdDiagnostico_01.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDiagnostico_01_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(14, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "CIE 10 - 1 :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRegistrarMedicos
            // 
            this.btnRegistrarMedicos.Location = new System.Drawing.Point(565, 96);
            this.btnRegistrarMedicos.Name = "btnRegistrarMedicos";
            this.btnRegistrarMedicos.Size = new System.Drawing.Size(26, 20);
            this.btnRegistrarMedicos.TabIndex = 64;
            this.btnRegistrarMedicos.Text = "...";
            this.btnRegistrarMedicos.UseVisualStyleBackColor = true;
            this.btnRegistrarMedicos.Click += new System.EventHandler(this.btnRegistrarMedicos_Click);
            // 
            // lblMedico
            // 
            this.lblMedico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMedico.Location = new System.Drawing.Point(226, 96);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(336, 20);
            this.lblMedico.TabIndex = 63;
            this.lblMedico.Text = "Nombre :";
            this.lblMedico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmRecetasManuales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 488);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmRecetasManuales";
            this.Text = "Captura de recetas manuales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRecetasManuales_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtFolioReceta;
        private SC_ControlsCS.scLabelStatus scLabelStatus3;
        private SC_ControlsCS.scTextBoxExt txtFolioElegibilidad;
        private SC_ControlsCS.scLabelStatus scLabelStatus1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDiagnostico_01;
        private SC_ControlsCS.scTextBoxExt txtIdDiagnostico_01;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDiagnostico_04;
        private SC_ControlsCS.scTextBoxExt txtIdDiagnostico_04;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiagnostico_03;
        private SC_ControlsCS.scTextBoxExt txtIdDiagnostico_03;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDiagnostico_02;
        private SC_ControlsCS.scTextBoxExt txtIdDiagnostico_02;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaDeReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFolioReferencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNombreBeneficiario;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtNombreMedico;
        private SC_ControlsCS.scTextBoxExt txtEspecialidad;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtPlanBeneficiario;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtConsecutivo;
        private System.Windows.Forms.Button btnRegistrarMedicos;
        private System.Windows.Forms.Label lblMedico;
    }
}