namespace Facturacion.GenerarRemisiones
{
    partial class FrmRemision_ProgramasDeAtencion
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemision_ProgramasDeAtencion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdProgramasSubProgramas = new FarPoint.Win.Spread.FpSpread();
            this.grdProgramasSubProgramas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardarGrid = new System.Windows.Forms.ToolStripButton();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas_Sheet1)).BeginInit();
            this.toolMenuFacturacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdProgramasSubProgramas);
            this.groupBox1.Location = new System.Drawing.Point(10, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(714, 389);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grdProgramasSubProgramas
            // 
            this.grdProgramasSubProgramas.AccessibleDescription = "grdProgramasSubProgramas, Sheet1, Row 0, Column 0, ";
            this.grdProgramasSubProgramas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProgramasSubProgramas.Location = new System.Drawing.Point(10, 20);
            this.grdProgramasSubProgramas.Name = "grdProgramasSubProgramas";
            this.grdProgramasSubProgramas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProgramasSubProgramas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProgramasSubProgramas_Sheet1});
            this.grdProgramasSubProgramas.Size = new System.Drawing.Size(696, 361);
            this.grdProgramasSubProgramas.TabIndex = 0;
            // 
            // grdProgramasSubProgramas_Sheet1
            // 
            this.grdProgramasSubProgramas_Sheet1.Reset();
            this.grdProgramasSubProgramas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProgramasSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProgramasSubProgramas_Sheet1.ColumnCount = 6;
            this.grdProgramasSubProgramas_Sheet1.RowCount = 15;
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Renglon";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Id Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Id Sub-Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Sub-Programa";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Procesar";
            this.grdProgramasSubProgramas_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).Label = "Renglon";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(0).Visible = false;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Label = "Id Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(1).Width = 80F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Label = "Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(2).Width = 200F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Label = "Id Sub-Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(3).Width = 80F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).Label = "Sub-Programa";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).Locked = true;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(4).Width = 200F;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(5).CellType = checkBoxCellType1;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(5).Label = "Procesar";
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProgramasSubProgramas_Sheet1.Columns.Get(5).Width = 80F;
            this.grdProgramasSubProgramas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProgramasSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolMenuFacturacion
            // 
            this.toolMenuFacturacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator3,
            this.btnGuardarGrid});
            this.toolMenuFacturacion.Location = new System.Drawing.Point(0, 0);
            this.toolMenuFacturacion.Name = "toolMenuFacturacion";
            this.toolMenuFacturacion.Size = new System.Drawing.Size(732, 25);
            this.toolMenuFacturacion.TabIndex = 3;
            this.toolMenuFacturacion.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardarGrid
            // 
            this.btnGuardarGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardarGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarGrid.Image")));
            this.btnGuardarGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardarGrid.Name = "btnGuardarGrid";
            this.btnGuardarGrid.Size = new System.Drawing.Size(23, 22);
            this.btnGuardarGrid.Text = "Guardar";
            this.btnGuardarGrid.Click += new System.EventHandler(this.btnGuardarGrid_Click);
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(554, 24);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(162, 18);
            this.chkMarcarDesmarcar.TabIndex = 2;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar todo";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // FrmRemision_ProgramasDeAtencion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 419);
            this.Controls.Add(this.chkMarcarDesmarcar);
            this.Controls.Add(this.toolMenuFacturacion);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmRemision_ProgramasDeAtencion";
            this.Text = "Programas y Sub-Programas de Atención";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRemision_ProgramasDeAtencion_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProgramasSubProgramas_Sheet1)).EndInit();
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdProgramasSubProgramas;
        private FarPoint.Win.Spread.SheetView grdProgramasSubProgramas_Sheet1;
        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardarGrid;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
    }
}