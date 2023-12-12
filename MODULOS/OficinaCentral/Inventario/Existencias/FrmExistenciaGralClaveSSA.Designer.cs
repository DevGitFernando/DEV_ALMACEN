namespace OficinaCentral.Inventario
{
    partial class FrmExistenciaGralClaveSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExistenciaGralClaveSSA));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstExistencia = new System.Windows.Forms.ListView();
            this.colClaveInterna = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClaveSSA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExistencia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoRptSinExist = new System.Windows.Forms.RadioButton();
            this.rdoRptTodos = new System.Windows.Forms.RadioButton();
            this.rdoRptConExist = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoClavesTodas = new System.Windows.Forms.RadioButton();
            this.rdoClavesUtilizadas = new System.Windows.Forms.RadioButton();
            this.rdoClavesSinUtilizar = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoRptDetalladoFarmacia = new System.Windows.Forms.RadioButton();
            this.rdoRptGeneral = new System.Windows.Forms.RadioButton();
            this.rdoRptDetalladoEstados = new System.Windows.Forms.RadioButton();
            this.lblDescripcionSal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(894, 25);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstExistencia);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.lblDescripcionSal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.grdExistencia);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(876, 483);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Existencias";
            // 
            // lstExistencia
            // 
            this.lstExistencia.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClaveInterna,
            this.colClaveSSA,
            this.colDescripcion,
            this.colExistencia});
            this.lstExistencia.FullRowSelect = true;
            this.lstExistencia.Location = new System.Drawing.Point(343, 140);
            this.lstExistencia.Name = "lstExistencia";
            this.lstExistencia.Size = new System.Drawing.Size(88, 64);
            this.lstExistencia.TabIndex = 7;
            this.lstExistencia.UseCompatibleStateImageBehavior = false;
            this.lstExistencia.View = System.Windows.Forms.View.Details;
            this.lstExistencia.Visible = false;
            // 
            // colClaveInterna
            // 
            this.colClaveInterna.Text = "Clave interna";
            this.colClaveInterna.Width = 120;
            // 
            // colClaveSSA
            // 
            this.colClaveSSA.Text = "Clave SSA ";
            this.colClaveSSA.Width = 120;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 470;
            // 
            // colExistencia
            // 
            this.colExistencia.Text = "Existencia";
            this.colExistencia.Width = 120;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoRptSinExist);
            this.groupBox4.Controls.Add(this.rdoRptTodos);
            this.groupBox4.Controls.Add(this.rdoRptConExist);
            this.groupBox4.Location = new System.Drawing.Point(588, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 45);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Existencias";
            // 
            // rdoRptSinExist
            // 
            this.rdoRptSinExist.AutoSize = true;
            this.rdoRptSinExist.Location = new System.Drawing.Point(176, 19);
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
            this.rdoRptTodos.Location = new System.Drawing.Point(11, 19);
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
            this.rdoRptConExist.Location = new System.Drawing.Point(75, 19);
            this.rdoRptConExist.Name = "rdoRptConExist";
            this.rdoRptConExist.Size = new System.Drawing.Size(95, 17);
            this.rdoRptConExist.TabIndex = 5;
            this.rdoRptConExist.Text = "Con Existencia";
            this.rdoRptConExist.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoClavesTodas);
            this.groupBox3.Controls.Add(this.rdoClavesUtilizadas);
            this.groupBox3.Controls.Add(this.rdoClavesSinUtilizar);
            this.groupBox3.Location = new System.Drawing.Point(7, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 44);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mostrar Claves";
            // 
            // rdoClavesTodas
            // 
            this.rdoClavesTodas.AutoSize = true;
            this.rdoClavesTodas.Location = new System.Drawing.Point(162, 19);
            this.rdoClavesTodas.Name = "rdoClavesTodas";
            this.rdoClavesTodas.Size = new System.Drawing.Size(55, 17);
            this.rdoClavesTodas.TabIndex = 1;
            this.rdoClavesTodas.Text = "Todas";
            this.rdoClavesTodas.UseVisualStyleBackColor = true;
            // 
            // rdoClavesUtilizadas
            // 
            this.rdoClavesUtilizadas.AutoSize = true;
            this.rdoClavesUtilizadas.Checked = true;
            this.rdoClavesUtilizadas.Location = new System.Drawing.Point(8, 19);
            this.rdoClavesUtilizadas.Name = "rdoClavesUtilizadas";
            this.rdoClavesUtilizadas.Size = new System.Drawing.Size(70, 17);
            this.rdoClavesUtilizadas.TabIndex = 0;
            this.rdoClavesUtilizadas.TabStop = true;
            this.rdoClavesUtilizadas.Text = "Utilizadas";
            this.rdoClavesUtilizadas.UseVisualStyleBackColor = true;
            // 
            // rdoClavesSinUtilizar
            // 
            this.rdoClavesSinUtilizar.AutoSize = true;
            this.rdoClavesSinUtilizar.Location = new System.Drawing.Point(83, 19);
            this.rdoClavesSinUtilizar.Name = "rdoClavesSinUtilizar";
            this.rdoClavesSinUtilizar.Size = new System.Drawing.Size(72, 17);
            this.rdoClavesSinUtilizar.TabIndex = 1;
            this.rdoClavesSinUtilizar.Text = "Sin utilizar";
            this.rdoClavesSinUtilizar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoRptDetalladoFarmacia);
            this.groupBox1.Controls.Add(this.rdoRptGeneral);
            this.groupBox1.Controls.Add(this.rdoRptDetalladoEstados);
            this.groupBox1.Location = new System.Drawing.Point(231, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de reporte";
            // 
            // rdoRptDetalladoFarmacia
            // 
            this.rdoRptDetalladoFarmacia.AutoSize = true;
            this.rdoRptDetalladoFarmacia.Location = new System.Drawing.Point(217, 19);
            this.rdoRptDetalladoFarmacia.Name = "rdoRptDetalladoFarmacia";
            this.rdoRptDetalladoFarmacia.Size = new System.Drawing.Size(134, 17);
            this.rdoRptDetalladoFarmacia.TabIndex = 2;
            this.rdoRptDetalladoFarmacia.Text = "Detallado por Farmacia";
            this.rdoRptDetalladoFarmacia.UseVisualStyleBackColor = true;
            // 
            // rdoRptGeneral
            // 
            this.rdoRptGeneral.AutoSize = true;
            this.rdoRptGeneral.Checked = true;
            this.rdoRptGeneral.Location = new System.Drawing.Point(11, 19);
            this.rdoRptGeneral.Name = "rdoRptGeneral";
            this.rdoRptGeneral.Size = new System.Drawing.Size(62, 17);
            this.rdoRptGeneral.TabIndex = 0;
            this.rdoRptGeneral.TabStop = true;
            this.rdoRptGeneral.Text = "General";
            this.rdoRptGeneral.UseVisualStyleBackColor = true;
            // 
            // rdoRptDetalladoEstados
            // 
            this.rdoRptDetalladoEstados.AutoSize = true;
            this.rdoRptDetalladoEstados.Location = new System.Drawing.Point(81, 19);
            this.rdoRptDetalladoEstados.Name = "rdoRptDetalladoEstados";
            this.rdoRptDetalladoEstados.Size = new System.Drawing.Size(129, 17);
            this.rdoRptDetalladoEstados.TabIndex = 1;
            this.rdoRptDetalladoEstados.Text = "Detallado por Estados";
            this.rdoRptDetalladoEstados.UseVisualStyleBackColor = true;
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSal.Location = new System.Drawing.Point(10, 409);
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(675, 63);
            this.lblDescripcionSal.TabIndex = 5;
            this.lblDescripcionSal.Text = "label1";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(691, 409);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(761, 413);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(88, 23);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(10, 66);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(859, 340);
            this.grdExistencia.TabIndex = 4;
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
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave interna";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "Clave interna";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 84F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 121F;
            textCellType3.MaxLength = 1000;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdExistencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 511F;
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
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmExistenciaGralClaveSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmExistenciaGralClaveSSA";
            this.Text = "Existencia general por Clave SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDescripcionSal;
        private System.Windows.Forms.RadioButton rdoRptDetalladoEstados;
        private System.Windows.Forms.RadioButton rdoRptGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoRptDetalladoFarmacia;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoClavesTodas;
        private System.Windows.Forms.RadioButton rdoClavesUtilizadas;
        private System.Windows.Forms.RadioButton rdoClavesSinUtilizar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoRptSinExist;
        private System.Windows.Forms.RadioButton rdoRptTodos;
        private System.Windows.Forms.RadioButton rdoRptConExist;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.ListView lstExistencia;
        private System.Windows.Forms.ColumnHeader colClaveInterna;
        private System.Windows.Forms.ColumnHeader colClaveSSA;
        private System.Windows.Forms.ColumnHeader colDescripcion;
        private System.Windows.Forms.ColumnHeader colExistencia;
    }
}