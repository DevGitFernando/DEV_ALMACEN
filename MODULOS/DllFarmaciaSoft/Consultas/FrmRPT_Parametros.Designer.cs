namespace DllFarmaciaSoft
{
    partial class FrmRPT_Parametros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRPT_Parametros));
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdParametros = new FarPoint.Win.Spread.FpSpread();
            this.grdParametros_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCerrar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(942, 39);
            this.toolStripBarraMenu.TabIndex = 2;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(36, 36);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(36, 36);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTipText = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btnCerrar
            // 
            this.btnCerrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(36, 36);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.ToolTipText = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdParametros);
            this.groupBox2.Location = new System.Drawing.Point(10, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(922, 364);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado";
            // 
            // grdParametros
            // 
            this.grdParametros.AccessibleDescription = "grdParametros, Sheet1, Row 0, Column 0, ";
            this.grdParametros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdParametros.Location = new System.Drawing.Point(10, 16);
            this.grdParametros.Name = "grdParametros";
            this.grdParametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdParametros.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdParametros_Sheet1});
            this.grdParametros.Size = new System.Drawing.Size(906, 340);
            this.grdParametros.TabIndex = 0;
            this.grdParametros.EditModeOff += new System.EventHandler(this.grdParametros_EditModeOff);
            this.grdParametros.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdParametros_Advance);
            this.grdParametros.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdParametros_KeyDown);
            // 
            // grdParametros_Sheet1
            // 
            this.grdParametros_Sheet1.Reset();
            this.grdParametros_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdParametros_Sheet1.ColumnCount = 2;
            this.grdParametros_Sheet1.RowCount = 14;
            this.grdParametros_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código de Barras";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Comercial";
            this.grdParametros_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType7.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType7.MaxLength = 30;
            this.grdParametros_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdParametros_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(0).Label = "Código de Barras";
            this.grdParametros_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(0).Width = 150F;
            textCellType8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            textCellType8.MaxLength = 100;
            this.grdParametros_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdParametros_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(1).Label = "Descripción Comercial";
            this.grdParametros_Sheet1.Columns.Get(1).Locked = false;
            this.grdParametros_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(1).Width = 700F;
            this.grdParametros_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmRPT_Parametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 414);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmRPT_Parametros";
            this.Text = "Selección de Parámetros";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRPT_Parametros_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdParametros;
        private FarPoint.Win.Spread.SheetView grdParametros_Sheet1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCerrar;
    }
}