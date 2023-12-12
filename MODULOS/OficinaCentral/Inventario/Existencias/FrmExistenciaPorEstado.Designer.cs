namespace OficinaCentral.Inventario
{
    partial class FrmExistenciaPorEstado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaPorEstado));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalFarmacias = new System.Windows.Forms.Label();
            this.grdExistenciaFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdExistenciaFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblDescripcionClaveSSA = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtClaveSSA = new System.Windows.Forms.Label();
            this.tmExistencias = new System.Windows.Forms.Timer(this.components);
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1080, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(9, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Estado";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(68, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(242, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.Validating += new System.ComponentModel.CancelEventHandler(this.cboEstados_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdoRptSinExist);
            this.groupBox5.Controls.Add(this.rdoRptTodos);
            this.groupBox5.Controls.Add(this.rdoRptConExist);
            this.groupBox5.Location = new System.Drawing.Point(9, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(316, 45);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.AutoSize = true;
            this.rdoRptSinExist.Location = new System.Drawing.Point(203, 19);
            this.rdoRptSinExist.Name = "rdoRptSinExist";
            this.rdoRptSinExist.Size = new System.Drawing.Size(91, 17);
            this.rdoRptSinExist.TabIndex = 6;
            this.rdoRptSinExist.Text = "Sin Existencia";
            this.rdoRptSinExist.UseVisualStyleBackColor = true;
            // 
            // rdoRptTodos
            // 
            this.rdoRptTodos.AutoSize = true;
            this.rdoRptTodos.Checked = true;
            this.rdoRptTodos.Location = new System.Drawing.Point(22, 19);
            this.rdoRptTodos.Name = "rdoRptTodos";
            this.rdoRptTodos.Size = new System.Drawing.Size(55, 17);
            this.rdoRptTodos.TabIndex = 4;
            this.rdoRptTodos.TabStop = true;
            this.rdoRptTodos.Text = "Todos";
            this.rdoRptTodos.UseVisualStyleBackColor = true;
            // 
            // rdoRptConExist
            // 
            this.rdoRptConExist.AutoSize = true;
            this.rdoRptConExist.Location = new System.Drawing.Point(94, 19);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(95, 17);
            this.rdoRptConExist.TabIndex = 5;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdExistencia);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Location = new System.Drawing.Point(9, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 332);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Existencias por Clave SSA";
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(9, 19);
            this.grdExistencia.MoveActiveOnFocus = false;
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(586, 274);
            this.grdExistencia.TabIndex = 3;
            this.grdExistencia.TabStripInsertTab = false;
            this.grdExistencia.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.grdExistencia_LeaveCell);
            this.grdExistencia.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdExistencia_CellDoubleClick);
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 4;
            this.grdExistencia_Sheet1.RowCount = 10;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "Código";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 55F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 110F;
            textCellType3.MaxLength = 1000;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdExistencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 330F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 100000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdExistencia_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdExistencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExistencia_Sheet1.Columns.Get(3).Label = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Width = 87F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(374, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(486, 297);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(88, 23);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblTotalFarmacias);
            this.groupBox3.Controls.Add(this.grdExistenciaFarmacias);
            this.groupBox3.Location = new System.Drawing.Point(619, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(452, 332);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalles de Existencias por Farmacias";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(235, 297);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Existencia Total :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalFarmacias
            // 
            this.lblTotalFarmacias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFarmacias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFarmacias.Location = new System.Drawing.Point(347, 297);
            this.lblTotalFarmacias.Name = "lblTotalFarmacias";
            this.lblTotalFarmacias.Size = new System.Drawing.Size(88, 23);
            this.lblTotalFarmacias.TabIndex = 1;
            this.lblTotalFarmacias.Text = "label3";
            this.lblTotalFarmacias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdExistenciaFarmacias
            // 
            this.grdExistenciaFarmacias.AccessibleDescription = "grdExistenciaFarmacias, Sheet1, Row 0, Column 0, ";
            this.grdExistenciaFarmacias.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistenciaFarmacias.Location = new System.Drawing.Point(10, 19);
            this.grdExistenciaFarmacias.Name = "grdExistenciaFarmacias";
            this.grdExistenciaFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistenciaFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistenciaFarmacias_Sheet1});
            this.grdExistenciaFarmacias.Size = new System.Drawing.Size(434, 274);
            this.grdExistenciaFarmacias.TabIndex = 0;
            this.grdExistenciaFarmacias.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdExistenciaFarmacias_CellDoubleClick);
            // 
            // grdExistenciaFarmacias_Sheet1
            // 
            this.grdExistenciaFarmacias_Sheet1.Reset();
            this.grdExistenciaFarmacias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistenciaFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistenciaFarmacias_Sheet1.ColumnCount = 4;
            this.grdExistenciaFarmacias_Sheet1.RowCount = 10;
            this.grdExistenciaFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdExistenciaFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdExistenciaFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre";
            this.grdExistenciaFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Existencia";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(0).Visible = false;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).CellType = textCellType5;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).Label = "Nombre";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).Width = 230F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 100000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(3).Label = "Existencia";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(3).Width = 87F;
            this.grdExistenciaFarmacias_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistenciaFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblDescripcionClaveSSA
            // 
            this.lblDescripcionClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClaveSSA.Location = new System.Drawing.Point(19, 40);
            this.lblDescripcionClaveSSA.Name = "lblDescripcionClaveSSA";
            this.lblDescripcionClaveSSA.Size = new System.Drawing.Size(713, 54);
            this.lblDescripcionClaveSSA.TabIndex = 14;
            this.lblDescripcionClaveSSA.Text = "label2";
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(229, 16);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(204, 20);
            this.lblClaveSSA.TabIndex = 16;
            this.lblClaveSSA.Text = "txtClaveSSA";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClaveSSA.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtClaveSSA);
            this.groupBox4.Controls.Add(this.lblDescripcionClaveSSA);
            this.groupBox4.Controls.Add(this.lblClaveSSA);
            this.groupBox4.Location = new System.Drawing.Point(331, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(740, 101);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos de Clave SSA";
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtClaveSSA.Location = new System.Drawing.Point(19, 16);
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.Size = new System.Drawing.Size(204, 20);
            this.txtClaveSSA.TabIndex = 17;
            this.txtClaveSSA.Text = "txtClaveSSA";
            this.txtClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmExistencias
            // 
            this.tmExistencias.Tick += new System.EventHandler(this.tmExistencias_Tick);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmExistenciaPorEstado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 478);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmExistenciaPorEstado";
            this.Text = "Existencia por Estado por Clave SSA ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalFarmacias;
        private FarPoint.Win.Spread.FpSpread grdExistenciaFarmacias;
        private FarPoint.Win.Spread.SheetView grdExistenciaFarmacias_Sheet1;
        private System.Windows.Forms.Label lblDescripcionClaveSSA;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Timer tmExistencias;
        private System.Windows.Forms.Label txtClaveSSA;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}