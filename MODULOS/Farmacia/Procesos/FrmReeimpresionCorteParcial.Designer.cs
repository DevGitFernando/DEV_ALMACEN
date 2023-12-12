namespace Farmacia.Procesos
{
    partial class FrmReeimpresionCorteParcial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReeimpresionCorteParcial));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdPersonalCortes = new FarPoint.Win.Spread.FpSpread();
            this.grdPersonalCortes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameTipoReporte = new System.Windows.Forms.GroupBox();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoDetDisp = new System.Windows.Forms.RadioButton();
            this.rdoCorteParcial = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonalCortes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonalCortes_Sheet1)).BeginInit();
            this.FrameTipoReporte.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(464, 25);
            this.toolStripBarraMenu.TabIndex = 8;
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
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaInicial);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 50);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha a Reimprimir";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(135, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Fecha :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(198, 17);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(111, 20);
            this.dtpFechaInicial.TabIndex = 12;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 10, 16, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdPersonalCortes);
            this.groupBox2.Location = new System.Drawing.Point(10, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 206);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cortes de Personal";
            // 
            // grdPersonalCortes
            // 
            this.grdPersonalCortes.AccessibleDescription = "grdPersonalCortes, Sheet1, Row 0, Column 0, ";
            this.grdPersonalCortes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPersonalCortes.Location = new System.Drawing.Point(10, 18);
            this.grdPersonalCortes.Name = "grdPersonalCortes";
            this.grdPersonalCortes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPersonalCortes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPersonalCortes_Sheet1});
            this.grdPersonalCortes.Size = new System.Drawing.Size(427, 181);
            this.grdPersonalCortes.TabIndex = 0;
            this.grdPersonalCortes.DoubleClick += new System.EventHandler(this.grdPersonalCortes_DoubleClick);
            // 
            // grdPersonalCortes_Sheet1
            // 
            this.grdPersonalCortes_Sheet1.Reset();
            this.grdPersonalCortes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPersonalCortes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPersonalCortes_Sheet1.ColumnCount = 2;
            this.grdPersonalCortes_Sheet1.RowCount = 7;
            this.grdPersonalCortes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Personal";
            this.grdPersonalCortes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de personal";
            this.grdPersonalCortes_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdPersonalCortes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPersonalCortes_Sheet1.Columns.Get(0).Label = "Personal";
            this.grdPersonalCortes_Sheet1.Columns.Get(0).Locked = true;
            this.grdPersonalCortes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPersonalCortes_Sheet1.Columns.Get(0).Width = 70F;
            this.grdPersonalCortes_Sheet1.Columns.Get(1).Label = "Nombre de personal";
            this.grdPersonalCortes_Sheet1.Columns.Get(1).Locked = true;
            this.grdPersonalCortes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPersonalCortes_Sheet1.Columns.Get(1).Width = 300F;
            this.grdPersonalCortes_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdPersonalCortes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameTipoReporte
            // 
            this.FrameTipoReporte.Controls.Add(this.rdoAmbos);
            this.FrameTipoReporte.Controls.Add(this.rdoDetDisp);
            this.FrameTipoReporte.Controls.Add(this.rdoCorteParcial);
            this.FrameTipoReporte.Location = new System.Drawing.Point(10, 80);
            this.FrameTipoReporte.Name = "FrameTipoReporte";
            this.FrameTipoReporte.Size = new System.Drawing.Size(445, 61);
            this.FrameTipoReporte.TabIndex = 11;
            this.FrameTipoReporte.TabStop = false;
            this.FrameTipoReporte.Text = "Tipo de Reporte";
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Location = new System.Drawing.Point(25, 27);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(67, 15);
            this.rdoAmbos.TabIndex = 0;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoDetDisp
            // 
            this.rdoDetDisp.Location = new System.Drawing.Point(293, 26);
            this.rdoDetDisp.Name = "rdoDetDisp";
            this.rdoDetDisp.Size = new System.Drawing.Size(134, 17);
            this.rdoDetDisp.TabIndex = 2;
            this.rdoDetDisp.TabStop = true;
            this.rdoDetDisp.Text = "Detalle Dispensación";
            this.rdoDetDisp.UseVisualStyleBackColor = true;
            // 
            // rdoCorteParcial
            // 
            this.rdoCorteParcial.Location = new System.Drawing.Point(149, 28);
            this.rdoCorteParcial.Name = "rdoCorteParcial";
            this.rdoCorteParcial.Size = new System.Drawing.Size(94, 15);
            this.rdoCorteParcial.TabIndex = 1;
            this.rdoCorteParcial.TabStop = true;
            this.rdoCorteParcial.Text = "Corte Parcial";
            this.rdoCorteParcial.UseVisualStyleBackColor = true;
            // 
            // FrmReeimpresionCorteParcial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 357);
            this.Controls.Add(this.FrameTipoReporte);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReeimpresionCorteParcial";
            this.Text = "Reimpresión de Cortes Parciales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReeimpresionCorteParcial_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonalCortes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPersonalCortes_Sheet1)).EndInit();
            this.FrameTipoReporte.ResumeLayout(false);
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
        private FarPoint.Win.Spread.FpSpread grdPersonalCortes;
        private FarPoint.Win.Spread.SheetView grdPersonalCortes_Sheet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameTipoReporte;
        private System.Windows.Forms.RadioButton rdoAmbos;
        private System.Windows.Forms.RadioButton rdoDetDisp;
        private System.Windows.Forms.RadioButton rdoCorteParcial;

    }
}