namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    partial class FrmProgramacion_Dispensacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProgramacion_Dispensacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType12 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.FrameOrigenDatos = new System.Windows.Forms.GroupBox();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nmMes = new System.Windows.Forms.NumericUpDown();
            this.nmAño = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStriplblResultado = new System.Windows.Forms.ToolStripLabel();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdProgramacion = new FarPoint.Win.Spread.FpSpread();
            this.grdProgramacion_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FrameOrigenDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAño)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramacion_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameOrigenDatos
            // 
            this.FrameOrigenDatos.Controls.Add(this.lblSubCte);
            this.FrameOrigenDatos.Controls.Add(this.txtSubCte);
            this.FrameOrigenDatos.Controls.Add(this.label5);
            this.FrameOrigenDatos.Controls.Add(this.lblCte);
            this.FrameOrigenDatos.Controls.Add(this.txtCte);
            this.FrameOrigenDatos.Controls.Add(this.label3);
            this.FrameOrigenDatos.Location = new System.Drawing.Point(13, 29);
            this.FrameOrigenDatos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameOrigenDatos.Name = "FrameOrigenDatos";
            this.FrameOrigenDatos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameOrigenDatos.Size = new System.Drawing.Size(974, 98);
            this.FrameOrigenDatos.TabIndex = 4;
            this.FrameOrigenDatos.TabStop = false;
            this.FrameOrigenDatos.Text = "Datos del Cliente";
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(214, 54);
            this.lblSubCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(743, 26);
            this.lblSubCte.TabIndex = 46;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(110, 56);
            this.txtSubCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(97, 22);
            this.txtSubCte.TabIndex = 42;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(214, 24);
            this.lblCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(743, 26);
            this.lblCte.TabIndex = 44;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(110, 26);
            this.txtCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(97, 22);
            this.txtCte.TabIndex = 41;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(84, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mes :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(84, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Año :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMes
            // 
            this.nmMes.Location = new System.Drawing.Point(160, 52);
            this.nmMes.Margin = new System.Windows.Forms.Padding(4);
            this.nmMes.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMes.Name = "nmMes";
            this.nmMes.Size = new System.Drawing.Size(92, 22);
            this.nmMes.TabIndex = 1;
            this.nmMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nmAño
            // 
            this.nmAño.Location = new System.Drawing.Point(159, 22);
            this.nmAño.Margin = new System.Windows.Forms.Padding(4);
            this.nmAño.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.nmAño.Minimum = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            this.nmAño.Name = "nmAño";
            this.nmAño.Size = new System.Drawing.Size(93, 22);
            this.nmAño.TabIndex = 0;
            this.nmAño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmAño.Value = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.toolStriplblResultado,
            this.btnExportarExcel,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1344, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            // toolStriplblResultado
            // 
            this.toolStriplblResultado.Name = "toolStriplblResultado";
            this.toolStriplblResultado.Size = new System.Drawing.Size(0, 22);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdProgramacion);
            this.groupBox1.Location = new System.Drawing.Point(13, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1319, 550);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalles de programación";
            // 
            // grdProgramacion
            // 
            this.grdProgramacion.AccessibleDescription = "grdProgramacion, Sheet1, Row 0, Column 0, 010.000.0000";
            this.grdProgramacion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProgramacion.Location = new System.Drawing.Point(15, 25);
            this.grdProgramacion.Name = "grdProgramacion";
            this.grdProgramacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProgramacion.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProgramacion_Sheet1});
            this.grdProgramacion.Size = new System.Drawing.Size(1298, 513);
            this.grdProgramacion.TabIndex = 0;
            // 
            // grdProgramacion_Sheet1
            // 
            this.grdProgramacion_Sheet1.Reset();
            this.grdProgramacion_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProgramacion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProgramacion_Sheet1.ColumnCount = 6;
            this.grdProgramacion_Sheet1.RowCount = 10;
            this.grdProgramacion_Sheet1.Cells.Get(0, 0).Value = "010.000.0000";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave SSA";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Programación";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Ampliación";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Dispensación";
            this.grdProgramacion_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Porcentaje dispensado";
            this.grdProgramacion_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdProgramacion_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdProgramacion_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProgramacion_Sheet1.Columns.Get(0).Locked = false;
            this.grdProgramacion_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(0).Width = 130F;
            this.grdProgramacion_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdProgramacion_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProgramacion_Sheet1.Columns.Get(1).Label = "Descripción Clave SSA";
            this.grdProgramacion_Sheet1.Columns.Get(1).Locked = true;
            this.grdProgramacion_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(1).Width = 600F;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.DecimalSeparator = ".";
            numberCellType9.MaximumValue = 10000000D;
            numberCellType9.MinimumValue = 0D;
            numberCellType9.Separator = ",";
            numberCellType9.ShowSeparator = true;
            this.grdProgramacion_Sheet1.Columns.Get(2).CellType = numberCellType9;
            this.grdProgramacion_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProgramacion_Sheet1.Columns.Get(2).Label = "Programación";
            this.grdProgramacion_Sheet1.Columns.Get(2).Locked = true;
            this.grdProgramacion_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(2).Width = 110F;
            numberCellType10.DecimalPlaces = 0;
            numberCellType10.DecimalSeparator = ".";
            numberCellType10.MaximumValue = 10000000D;
            numberCellType10.MinimumValue = 0D;
            numberCellType10.Separator = ",";
            numberCellType10.ShowSeparator = true;
            this.grdProgramacion_Sheet1.Columns.Get(3).CellType = numberCellType10;
            this.grdProgramacion_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProgramacion_Sheet1.Columns.Get(3).Label = "Ampliación";
            this.grdProgramacion_Sheet1.Columns.Get(3).Locked = true;
            this.grdProgramacion_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(3).Width = 110F;
            numberCellType11.DecimalPlaces = 0;
            numberCellType11.DecimalSeparator = ".";
            numberCellType11.MaximumValue = 10000000D;
            numberCellType11.MinimumValue = 0D;
            numberCellType11.Separator = ",";
            numberCellType11.ShowSeparator = true;
            this.grdProgramacion_Sheet1.Columns.Get(4).CellType = numberCellType11;
            this.grdProgramacion_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProgramacion_Sheet1.Columns.Get(4).Label = "Dispensación";
            this.grdProgramacion_Sheet1.Columns.Get(4).Locked = true;
            this.grdProgramacion_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(4).Width = 110F;
            numberCellType12.DecimalPlaces = 2;
            numberCellType12.DecimalSeparator = ".";
            numberCellType12.MinimumValue = 0D;
            numberCellType12.Separator = ",";
            numberCellType12.ShowSeparator = true;
            this.grdProgramacion_Sheet1.Columns.Get(5).CellType = numberCellType12;
            this.grdProgramacion_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProgramacion_Sheet1.Columns.Get(5).Label = "Porcentaje dispensado";
            this.grdProgramacion_Sheet1.Columns.Get(5).Locked = true;
            this.grdProgramacion_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramacion_Sheet1.Columns.Get(5).Width = 110F;
            this.grdProgramacion_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdProgramacion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nmAño);
            this.groupBox2.Controls.Add(this.nmMes);
            this.groupBox2.Location = new System.Drawing.Point(995, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(336, 98);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Periodo";
            // 
            // FrmProgramacion_Dispensacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 689);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameOrigenDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProgramacion_Dispensacion";
            this.Text = "Programación de consumo ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProgramacion_Dispensacion_Load);
            this.FrameOrigenDatos.ResumeLayout(false);
            this.FrameOrigenDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAño)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramacion_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameOrigenDatos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmMes;
        private System.Windows.Forms.NumericUpDown nmAño;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStriplblResultado;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdProgramacion;
        private FarPoint.Win.Spread.SheetView grdProgramacion_Sheet1;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}