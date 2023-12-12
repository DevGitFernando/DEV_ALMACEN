namespace Farmacia.Procesos
{
    partial class FrmCierresGenerales
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCierresGenerales));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdFolios = new FarPoint.Win.Spread.FpSpread();
            this.grdFolios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.rdoIndividual = new System.Windows.Forms.RadioButton();
            this.FrameTipoReporte = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameTipoReporte.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdFolios);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 267);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folio de Cierres";
            // 
            // grdFolios
            // 
            this.grdFolios.AccessibleDescription = "grdFolios, Sheet1, Row 0, Column 0, ";
            this.grdFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFolios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFolios.Location = new System.Drawing.Point(9, 19);
            this.grdFolios.Name = "grdFolios";
            this.grdFolios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFolios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFolios_Sheet1});
            this.grdFolios.Size = new System.Drawing.Size(413, 240);
            this.grdFolios.TabIndex = 1;
            this.grdFolios.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdFolios_CellDoubleClick);
            // 
            // grdFolios_Sheet1
            // 
            this.grdFolios_Sheet1.Reset();
            this.grdFolios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFolios_Sheet1.ColumnCount = 2;
            this.grdFolios_Sheet1.RowCount = 10;
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio Cierre";
            this.grdFolios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha Registro";
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = -10000000D;
            this.grdFolios_Sheet1.Columns.Get(0).CellType = numberCellType4;
            this.grdFolios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Label = "Folio Cierre";
            this.grdFolios_Sheet1.Columns.Get(0).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(0).Width = 105F;
            this.grdFolios_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdFolios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Label = "Fecha Registro";
            this.grdFolios_Sheet1.Columns.Get(1).Locked = true;
            this.grdFolios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFolios_Sheet1.Columns.Get(1).Width = 175F;
            this.grdFolios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFolios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(454, 25);
            this.toolStripBarraMenu.TabIndex = 9;
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFechaInicio);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 82);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango de Fechas";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(48, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Fecha Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(130, 47);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Value = new System.DateTime(2014, 7, 11, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(48, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Fecha Inicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(130, 21);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaInicio.TabIndex = 12;
            this.dtpFechaInicio.Value = new System.DateTime(2014, 7, 11, 0, 0, 0, 0);
            // 
            // rdoTodos
            // 
            this.rdoTodos.Location = new System.Drawing.Point(33, 50);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(84, 15);
            this.rdoTodos.TabIndex = 16;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = true;
            // 
            // rdoIndividual
            // 
            this.rdoIndividual.Location = new System.Drawing.Point(33, 24);
            this.rdoIndividual.Name = "rdoIndividual";
            this.rdoIndividual.Size = new System.Drawing.Size(84, 15);
            this.rdoIndividual.TabIndex = 17;
            this.rdoIndividual.TabStop = true;
            this.rdoIndividual.Text = "Individual";
            this.rdoIndividual.UseVisualStyleBackColor = true;
            // 
            // FrameTipoReporte
            // 
            this.FrameTipoReporte.Controls.Add(this.rdoIndividual);
            this.FrameTipoReporte.Controls.Add(this.rdoTodos);
            this.FrameTipoReporte.Location = new System.Drawing.Point(291, 28);
            this.FrameTipoReporte.Name = "FrameTipoReporte";
            this.FrameTipoReporte.Size = new System.Drawing.Size(151, 82);
            this.FrameTipoReporte.TabIndex = 12;
            this.FrameTipoReporte.TabStop = false;
            this.FrameTipoReporte.Text = "Tipo de Reporte";
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 387);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(454, 24);
            this.lblMensajes.TabIndex = 24;
            this.lblMensajes.Text = "Doble clic sobre el folio cierre para imprimir ( 1 Folio )";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCierresGenerales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 411);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameTipoReporte);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCierresGenerales";
            this.Text = "Listado de Cierres Generales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCierresGenerales_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolios_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.FrameTipoReporte.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdFolios;
        private FarPoint.Win.Spread.SheetView grdFolios_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.RadioButton rdoIndividual;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.GroupBox FrameTipoReporte;
        private System.Windows.Forms.Label lblMensajes;
    }
}