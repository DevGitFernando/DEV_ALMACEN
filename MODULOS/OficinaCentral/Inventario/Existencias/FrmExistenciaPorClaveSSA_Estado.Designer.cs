namespace OficinaCentral.Inventario
{
    partial class FrmExistenciaPorClaveSSA_Estado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaPorClaveSSA_Estado));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDes = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalFarmacias = new System.Windows.Forms.Label();
            this.grdExistenciaFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdExistenciaFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmExistencias = new System.Windows.Forms.Timer(this.components);
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(930, 25);
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
            this.groupBox1.Controls.Add(this.lblDes);
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(913, 102);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave SSA";
            // 
            // lblDes
            // 
            this.lblDes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDes.Location = new System.Drawing.Point(94, 41);
            this.lblDes.Name = "lblDes";
            this.lblDes.Size = new System.Drawing.Size(809, 54);
            this.lblDes.TabIndex = 6;
            this.lblDes.Text = "label2";
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(279, 16);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(204, 20);
            this.lblClaveSSA.TabIndex = 5;
            this.lblClaveSSA.Text = "Clave SSA : ";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClaveSSA.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(94, 16);
            this.txtId.MaxLength = 20;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(180, 20);
            this.txtId.TabIndex = 3;
            this.txtId.Text = "01234567890123456789";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave SSA : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoRptSinExist);
            this.groupBox4.Controls.Add(this.rdoRptTodos);
            this.groupBox4.Controls.Add(this.rdoRptConExist);
            this.groupBox4.Location = new System.Drawing.Point(9, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(913, 45);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.AutoSize = true;
            this.rdoRptSinExist.Location = new System.Drawing.Point(525, 19);
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
            this.rdoRptTodos.Location = new System.Drawing.Point(296, 19);
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
            this.rdoRptConExist.Location = new System.Drawing.Point(389, 19);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(95, 17);
            this.rdoRptConExist.TabIndex = 5;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.grdExistencia);
            this.groupBox2.Location = new System.Drawing.Point(9, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 333);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Existencias";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(215, 301);
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
            this.lblTotal.Location = new System.Drawing.Point(327, 301);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(88, 23);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(10, 16);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(423, 280);
            this.grdExistencia.TabIndex = 0;
            this.grdExistencia.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdExistencia_CellDoubleClick);
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 3;
            this.grdExistencia_Sheet1.RowCount = 10;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 50F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 230F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 100000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 87F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblTotalFarmacias);
            this.groupBox3.Controls.Add(this.grdExistenciaFarmacias);
            this.groupBox3.Location = new System.Drawing.Point(458, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 333);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalles de Existencias por Estado";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(236, 301);
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
            this.lblTotalFarmacias.Location = new System.Drawing.Point(348, 301);
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
            this.grdExistenciaFarmacias.Location = new System.Drawing.Point(10, 16);
            this.grdExistenciaFarmacias.Name = "grdExistenciaFarmacias";
            this.grdExistenciaFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistenciaFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistenciaFarmacias_Sheet1});
            this.grdExistenciaFarmacias.Size = new System.Drawing.Size(443, 280);
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
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).CellType = textCellType3;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(1).Width = 70F;
            this.grdExistenciaFarmacias_Sheet1.Columns.Get(2).CellType = textCellType4;
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
            // tmExistencias
            // 
            this.tmExistencias.Tick += new System.EventHandler(this.tmExistencias_Tick);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmExistenciaPorClaveSSA_Estado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 512);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmExistenciaPorClaveSSA_Estado";
            this.Text = "Existencia por Clave SSA por Estados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistenciaFarmacias_Sheet1)).EndInit();
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
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label lblDes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalFarmacias;
        private FarPoint.Win.Spread.FpSpread grdExistenciaFarmacias;
        private FarPoint.Win.Spread.SheetView grdExistenciaFarmacias_Sheet1;
        private System.Windows.Forms.Timer tmExistencias;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}