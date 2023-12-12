namespace DllPedidosClientes.Reportes
{
    partial class FrmTBC_NivelAbasto
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType33 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType34 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType35 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType36 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType5 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType37 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType38 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType39 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType40 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTBC_NivelAbasto));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.lstClaves = new SC_ControlsCS.scListView();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstClaves);
            this.groupBox3.Controls.Add(this.chkTodos);
            this.groupBox3.Controls.Add(this.grdExistencia);
            this.groupBox3.Location = new System.Drawing.Point(12, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(942, 368);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Unidades";
            // 
            // chkTodos
            // 
            this.chkTodos.Location = new System.Drawing.Point(773, 0);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(157, 17);
            this.chkTodos.TabIndex = 4;
            this.chkTodos.Text = "Marcar / Desmarcar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(12, 19);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.grdExistencia.Size = new System.Drawing.Size(918, 343);
            this.grdExistencia.TabIndex = 1;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 9;
            this.sheetView1.RowCount = 14;
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "Unidad";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "Jurisdicción";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "Consultar";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "Claves de Perfil";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "Claves con Existencia";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "Claves sin Existencia";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "Porcetaje de Abasto";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 40F;
            textCellType33.MaxLength = 6;
            this.sheetView1.Columns.Get(0).CellType = textCellType33;
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(0).Label = "Unidad";
            this.sheetView1.Columns.Get(0).Locked = true;
            this.sheetView1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).CellType = textCellType34;
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(1).Label = "Nombre";
            this.sheetView1.Columns.Get(1).Locked = true;
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Width = 340F;
            textCellType35.MaxLength = 500;
            this.sheetView1.Columns.Get(2).CellType = textCellType35;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "Url";
            this.sheetView1.Columns.Get(2).Locked = true;
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Visible = false;
            this.sheetView1.Columns.Get(2).Width = 113F;
            this.sheetView1.Columns.Get(3).CellType = textCellType36;
            this.sheetView1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(3).Label = "Jurisdicción";
            this.sheetView1.Columns.Get(3).Locked = true;
            this.sheetView1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(3).Visible = false;
            this.sheetView1.Columns.Get(3).Width = 150F;
            this.sheetView1.Columns.Get(4).CellType = checkBoxCellType5;
            this.sheetView1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(4).Label = "Consultar";
            this.sheetView1.Columns.Get(5).CellType = textCellType37;
            this.sheetView1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(5).Label = "Claves de Perfil";
            this.sheetView1.Columns.Get(5).Locked = true;
            this.sheetView1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(5).Width = 100F;
            this.sheetView1.Columns.Get(6).CellType = textCellType38;
            this.sheetView1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(6).Label = "Claves con Existencia";
            this.sheetView1.Columns.Get(6).Locked = true;
            this.sheetView1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(6).Width = 100F;
            this.sheetView1.Columns.Get(7).CellType = textCellType39;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(7).Label = "Claves sin Existencia";
            this.sheetView1.Columns.Get(7).Locked = true;
            this.sheetView1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(7).Width = 100F;
            this.sheetView1.Columns.Get(8).CellType = textCellType40;
            this.sheetView1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.Columns.Get(8).Label = "Porcetaje de Abasto";
            this.sheetView1.Columns.Get(8).Locked = true;
            this.sheetView1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(8).Width = 100F;
            this.sheetView1.RowHeader.Columns.Default.Resizable = true;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "";
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(962, 25);
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
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(12, 420);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(942, 52);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de colores para consulta de información";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(366, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con exito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(85, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(800, 20);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(66, 20);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsultando
            // 
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(174, 20);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(66, 20);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(673, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ejecución con errores :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(478, 20);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(66, 20);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(110, 83);
            this.lstClaves.LockColumnSize = false;
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(743, 178);
            this.lstClaves.TabIndex = 5;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            this.lstClaves.Visible = false;
            // 
            // FrmTBC_NivelAbasto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 480);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmTBC_NivelAbasto";
            this.Text = "Nivel de Abasto en Unidades";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTBC_NivelAbasto_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFinExito;
        private SC_ControlsCS.scListView lstClaves;
    }
}