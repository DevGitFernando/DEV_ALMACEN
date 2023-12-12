namespace Almacen.Pedidos
{
    partial class FrmPCM_Claves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPCM_Claves));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkProcesar = new System.Windows.Forms.CheckBox();
            this.grdJurisdicciones = new FarPoint.Win.Spread.FpSpread();
            this.grdJurisdicciones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.FrameMesesRevision = new System.Windows.Forms.GroupBox();
            this.lblMesesRev = new System.Windows.Forms.Label();
            this.nmMesesRevision = new System.Windows.Forms.NumericUpDown();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdJurisdicciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJurisdicciones_Sheet1)).BeginInit();
            this.FrameMesesRevision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesRevision)).BeginInit();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator,
            this.btnExportarExcel,
            this.toolStripSeparator8});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(604, 25);
            this.toolStripBarraMenu.TabIndex = 4;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Obtener excedentes";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar distribución generada";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel distribución generada";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkProcesar);
            this.groupBox1.Controls.Add(this.grdJurisdicciones);
            this.groupBox1.Location = new System.Drawing.Point(10, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 289);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Consumo Mensual";
            // 
            // chkProcesar
            // 
            this.chkProcesar.AutoSize = true;
            this.chkProcesar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkProcesar.Location = new System.Drawing.Point(471, 0);
            this.chkProcesar.Name = "chkProcesar";
            this.chkProcesar.Size = new System.Drawing.Size(101, 17);
            this.chkProcesar.TabIndex = 5;
            this.chkProcesar.Text = "Procesar Todas";
            this.chkProcesar.UseVisualStyleBackColor = true;
            this.chkProcesar.CheckedChanged += new System.EventHandler(this.chkProcesar_CheckedChanged);
            // 
            // grdJurisdicciones
            // 
            this.grdJurisdicciones.AccessibleDescription = "grdJurisdicciones, Sheet1, Row 0, Column 0, ";
            this.grdJurisdicciones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdJurisdicciones.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdJurisdicciones.Location = new System.Drawing.Point(12, 19);
            this.grdJurisdicciones.Name = "grdJurisdicciones";
            this.grdJurisdicciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdJurisdicciones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdJurisdicciones_Sheet1});
            this.grdJurisdicciones.Size = new System.Drawing.Size(563, 263);
            this.grdJurisdicciones.TabIndex = 1;
            // 
            // grdJurisdicciones_Sheet1
            // 
            this.grdJurisdicciones_Sheet1.Reset();
            this.grdJurisdicciones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdJurisdicciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdJurisdicciones_Sheet1.ColumnCount = 4;
            this.grdJurisdicciones_Sheet1.RowCount = 12;
            this.grdJurisdicciones_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdJurisdicciones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Jurisdicción";
            this.grdJurisdicciones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdJurisdicciones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdJurisdicciones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Procesar";
            this.grdJurisdicciones_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 20;
            this.grdJurisdicciones_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdJurisdicciones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(0).Label = "Id Jurisdicción";
            this.grdJurisdicciones_Sheet1.Columns.Get(0).Locked = true;
            this.grdJurisdicciones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(0).Width = 87F;
            this.grdJurisdicciones_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdJurisdicciones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdJurisdicciones_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdJurisdicciones_Sheet1.Columns.Get(1).Locked = true;
            this.grdJurisdicciones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(1).Width = 270F;
            this.grdJurisdicciones_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdJurisdicciones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(2).Label = "Status";
            this.grdJurisdicciones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(2).Width = 90F;
            this.grdJurisdicciones_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdJurisdicciones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdJurisdicciones_Sheet1.Columns.Get(3).Label = "Procesar";
            this.grdJurisdicciones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdJurisdicciones_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdJurisdicciones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(12, 19);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(295, 24);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // FrameMesesRevision
            // 
            this.FrameMesesRevision.Controls.Add(this.lblMesesRev);
            this.FrameMesesRevision.Controls.Add(this.nmMesesRevision);
            this.FrameMesesRevision.Location = new System.Drawing.Point(10, 28);
            this.FrameMesesRevision.Name = "FrameMesesRevision";
            this.FrameMesesRevision.Size = new System.Drawing.Size(260, 55);
            this.FrameMesesRevision.TabIndex = 11;
            this.FrameMesesRevision.TabStop = false;
            // 
            // lblMesesRev
            // 
            this.lblMesesRev.Location = new System.Drawing.Point(9, 17);
            this.lblMesesRev.Name = "lblMesesRev";
            this.lblMesesRev.Size = new System.Drawing.Size(168, 25);
            this.lblMesesRev.TabIndex = 4;
            this.lblMesesRev.Text = "Meses de Revisión de Consumos :";
            this.lblMesesRev.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMesesRevision
            // 
            this.nmMesesRevision.Location = new System.Drawing.Point(183, 20);
            this.nmMesesRevision.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMesesRevision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMesesRevision.Name = "nmMesesRevision";
            this.nmMesesRevision.Size = new System.Drawing.Size(55, 20);
            this.nmMesesRevision.TabIndex = 3;
            this.nmMesesRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesRevision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(158, 378);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(318, 55);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando Información";
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 442);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(604, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "Este proceso Obtendrá el PCM del Mes Anterior";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPCM_Claves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 466);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.FrameMesesRevision);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPCM_Claves";
            this.Text = "Consumo Mensual";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPCM_Claves_FormClosing);
            this.Load += new System.EventHandler(this.FrmPCM_Claves_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdJurisdicciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJurisdicciones_Sheet1)).EndInit();
            this.FrameMesesRevision.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesRevision)).EndInit();
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.GroupBox FrameMesesRevision;
        private System.Windows.Forms.Label lblMesesRev;
        private System.Windows.Forms.NumericUpDown nmMesesRevision;
        private System.Windows.Forms.GroupBox FrameProceso;
        private FarPoint.Win.Spread.FpSpread grdJurisdicciones;
        private FarPoint.Win.Spread.SheetView grdJurisdicciones_Sheet1;
        private System.Windows.Forms.CheckBox chkProcesar;
        private System.Windows.Forms.Label lblMensajes;
    }
}