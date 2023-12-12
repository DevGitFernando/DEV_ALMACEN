namespace DllTransferenciaSoft.Auditoria
{
    partial class FrmFechasUnidades
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFechasUnidades));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdExistencia);
            this.groupBox3.Location = new System.Drawing.Point(8, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1016, 443);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Farmacias";
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(12, 15);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.grdExistencia.Size = new System.Drawing.Size(997, 421);
            this.grdExistencia.TabIndex = 1;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 10;
            this.sheetView1.RowCount = 18;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "Procesar";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha Sistema";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha Servidor";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha Sistema Unidad";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha Servidor Unidad";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "Diferencia Fecha Sistema";
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "Diferencia Fecha Servidor";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 40F;
            textCellType1.MaxLength = 6;
            this.sheetView1.Columns.Get(0).CellType = textCellType1;
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(0).Label = "Farmacia";
            this.sheetView1.Columns.Get(0).Locked = true;
            this.sheetView1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).CellType = textCellType2;
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(1).Label = "Nombre";
            this.sheetView1.Columns.Get(1).Locked = true;
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Width = 220F;
            textCellType3.MaxLength = 500;
            this.sheetView1.Columns.Get(2).CellType = textCellType3;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "Url";
            this.sheetView1.Columns.Get(2).Locked = true;
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Visible = false;
            this.sheetView1.Columns.Get(2).Width = 400F;
            this.sheetView1.Columns.Get(3).CellType = checkBoxCellType1;
            this.sheetView1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(3).Label = "Procesar";
            this.sheetView1.Columns.Get(3).Width = 50F;
            this.sheetView1.Columns.Get(4).CellType = textCellType4;
            this.sheetView1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(4).Label = "Fecha Sistema";
            this.sheetView1.Columns.Get(4).Locked = true;
            this.sheetView1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(4).Width = 100F;
            this.sheetView1.Columns.Get(5).CellType = textCellType5;
            this.sheetView1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(5).Label = "Fecha Servidor";
            this.sheetView1.Columns.Get(5).Locked = true;
            this.sheetView1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(5).Width = 100F;
            this.sheetView1.Columns.Get(6).CellType = textCellType6;
            this.sheetView1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(6).Label = "Fecha Sistema Unidad";
            this.sheetView1.Columns.Get(6).Locked = true;
            this.sheetView1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(6).Width = 100F;
            this.sheetView1.Columns.Get(7).CellType = textCellType7;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(7).Label = "Fecha Servidor Unidad";
            this.sheetView1.Columns.Get(7).Locked = true;
            this.sheetView1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(7).Width = 100F;
            this.sheetView1.Columns.Get(8).CellType = textCellType8;
            this.sheetView1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(8).Label = "Diferencia Fecha Sistema";
            this.sheetView1.Columns.Get(8).Locked = true;
            this.sheetView1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(8).Width = 100F;
            this.sheetView1.Columns.Get(9).CellType = textCellType9;
            this.sheetView1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(9).Label = "Diferencia Fecha Servidor";
            this.sheetView1.Columns.Get(9).Locked = true;
            this.sheetView1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(9).Width = 100F;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTodos);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Location = new System.Drawing.Point(8, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1016, 44);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Estados";
            // 
            // chkTodos
            // 
            this.chkTodos.Location = new System.Drawing.Point(612, 15);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(157, 24);
            this.chkTodos.TabIndex = 4;
            this.chkTodos.Text = "Marcar / Desmarcar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(247, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(306, 15);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(290, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1033, 25);
            this.toolStripBarraMenu.TabIndex = 2;
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
            this.toolStripSeparator2.Visible = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Visible = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmFechasUnidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 524);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmFechasUnidades";
            this.Text = "Auditar Fecha de Sistema y Fecha Servidor de Unidades vs Servidor Regional";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFechasUnidades_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}