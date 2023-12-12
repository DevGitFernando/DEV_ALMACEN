namespace DllPedidosClientes.Reportes
{
    partial class FrmListadoBeneficiarios
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoBeneficiarios));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grdBeneficiarios = new FarPoint.Win.Spread.FpSpread();
            this.grdBeneficiarios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoRptDetallado = new System.Windows.Forms.RadioButton();
            this.rdoRptConcentrado = new System.Windows.Forms.RadioButton();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBeneficiarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBeneficiarios_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTodos);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.grdBeneficiarios);
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(891, 323);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Beneficiarios";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(663, 295);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(123, 21);
            this.lblTotal.TabIndex = 36;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(574, 297);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 17);
            this.label6.TabIndex = 35;
            this.label6.Text = "Total :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdBeneficiarios
            // 
            this.grdBeneficiarios.AccessibleDescription = "grdBeneficiarios, Sheet1, Row 0, Column 0, ";
            this.grdBeneficiarios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdBeneficiarios.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdBeneficiarios.Location = new System.Drawing.Point(8, 31);
            this.grdBeneficiarios.Name = "grdBeneficiarios";
            this.grdBeneficiarios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdBeneficiarios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdBeneficiarios_Sheet1});
            this.grdBeneficiarios.Size = new System.Drawing.Size(877, 261);
            this.grdBeneficiarios.TabIndex = 0;
            // 
            // grdBeneficiarios_Sheet1
            // 
            this.grdBeneficiarios_Sheet1.Reset();
            this.grdBeneficiarios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdBeneficiarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdBeneficiarios_Sheet1.ColumnCount = 5;
            this.grdBeneficiarios_Sheet1.RowCount = 10;
            this.grdBeneficiarios_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdBeneficiarios_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdBeneficiarios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Beneficiario";
            this.grdBeneficiarios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio Referencia";
            this.grdBeneficiarios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre";
            this.grdBeneficiarios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Total";
            this.grdBeneficiarios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Seleccionar";
            this.grdBeneficiarios_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 20;
            this.grdBeneficiarios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdBeneficiarios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(0).Label = "Beneficiario";
            this.grdBeneficiarios_Sheet1.Columns.Get(0).Locked = true;
            this.grdBeneficiarios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(0).Width = 93F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdBeneficiarios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdBeneficiarios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(1).Label = "Folio Referencia";
            this.grdBeneficiarios_Sheet1.Columns.Get(1).Locked = true;
            this.grdBeneficiarios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(1).Width = 103F;
            this.grdBeneficiarios_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdBeneficiarios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdBeneficiarios_Sheet1.Columns.Get(2).Label = "Nombre";
            this.grdBeneficiarios_Sheet1.Columns.Get(2).Locked = true;
            this.grdBeneficiarios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(2).Width = 448F;
            numberCellType1.DecimalPlaces = 4;
            this.grdBeneficiarios_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdBeneficiarios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdBeneficiarios_Sheet1.Columns.Get(3).Label = "Total";
            this.grdBeneficiarios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(3).Width = 96F;
            this.grdBeneficiarios_Sheet1.Columns.Get(4).CellType = checkBoxCellType1;
            this.grdBeneficiarios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(4).Label = "Seleccionar";
            this.grdBeneficiarios_Sheet1.Columns.Get(4).Locked = false;
            this.grdBeneficiarios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBeneficiarios_Sheet1.Columns.Get(4).Width = 68F;
            this.grdBeneficiarios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdBeneficiarios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(915, 25);
            this.toolStripBarraMenu.TabIndex = 17;
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
            this.btnNuevo.Visible = false;
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
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoRptDetallado);
            this.FrameDispensacion.Controls.Add(this.rdoRptConcentrado);
            this.FrameDispensacion.Location = new System.Drawing.Point(12, 28);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(891, 44);
            this.FrameDispensacion.TabIndex = 18;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Reporte";
            // 
            // rdoRptDetallado
            // 
            this.rdoRptDetallado.Location = new System.Drawing.Point(151, 19);
            this.rdoRptDetallado.Name = "rdoRptDetallado";
            this.rdoRptDetallado.Size = new System.Drawing.Size(94, 17);
            this.rdoRptDetallado.TabIndex = 2;
            this.rdoRptDetallado.TabStop = true;
            this.rdoRptDetallado.Text = "Detallado";
            this.rdoRptDetallado.UseVisualStyleBackColor = true;
            // 
            // rdoRptConcentrado
            // 
            this.rdoRptConcentrado.Location = new System.Drawing.Point(25, 19);
            this.rdoRptConcentrado.Name = "rdoRptConcentrado";
            this.rdoRptConcentrado.Size = new System.Drawing.Size(94, 15);
            this.rdoRptConcentrado.TabIndex = 1;
            this.rdoRptConcentrado.TabStop = true;
            this.rdoRptConcentrado.Text = "Concentrado";
            this.rdoRptConcentrado.UseVisualStyleBackColor = true;
            // 
            // chkTodos
            // 
            this.chkTodos.Location = new System.Drawing.Point(728, 10);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(157, 16);
            this.chkTodos.TabIndex = 37;
            this.chkTodos.Text = "Marcar / Desmarcar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // FrmListadoBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 411);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmListadoBeneficiarios";
            this.Text = "Listado Beneficiarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoBeneficiarios_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBeneficiarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBeneficiarios_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDispensacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label6;
        private FarPoint.Win.Spread.FpSpread grdBeneficiarios;
        private FarPoint.Win.Spread.SheetView grdBeneficiarios_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoRptDetallado;
        private System.Windows.Forms.RadioButton rdoRptConcentrado;
        private System.Windows.Forms.CheckBox chkTodos;
    }
}