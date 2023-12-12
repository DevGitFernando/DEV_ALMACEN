namespace Configuracion.Configuracion
{
    partial class FrmTerminales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTerminales));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameTerminales = new System.Windows.Forms.GroupBox();
            this.grdTerminales = new FarPoint.Win.Spread.FpSpread();
            this.grdTerminales_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTerminales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTerminales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTerminales_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(639, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameTerminales
            // 
            this.FrameTerminales.Controls.Add(this.grdTerminales);
            this.FrameTerminales.Location = new System.Drawing.Point(7, 28);
            this.FrameTerminales.Name = "FrameTerminales";
            this.FrameTerminales.Size = new System.Drawing.Size(624, 265);
            this.FrameTerminales.TabIndex = 9;
            this.FrameTerminales.TabStop = false;
            this.FrameTerminales.Text = "Terminales";
            // 
            // grdTerminales
            // 
            this.grdTerminales.AccessibleDescription = "grdTerminales, Sheet1, Row 0, Column 0, ";
            this.grdTerminales.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdTerminales.Location = new System.Drawing.Point(8, 19);
            this.grdTerminales.Name = "grdTerminales";
            this.grdTerminales.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdTerminales.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdTerminales_Sheet1});
            this.grdTerminales.Size = new System.Drawing.Size(608, 236);
            this.grdTerminales.TabIndex = 0;
            // 
            // grdTerminales_Sheet1
            // 
            this.grdTerminales_Sheet1.Reset();
            this.grdTerminales_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdTerminales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdTerminales_Sheet1.ColumnCount = 5;
            this.grdTerminales_Sheet1.RowCount = 10;
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Terminal";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de Terminal";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "MAC Address";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Servidor";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Habilitar";
            this.grdTerminales_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            textCellType1.MaxLength = 4;
            this.grdTerminales_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdTerminales_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(0).Label = "Terminal";
            this.grdTerminales_Sheet1.Columns.Get(0).Locked = true;
            this.grdTerminales_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdTerminales_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTerminales_Sheet1.Columns.Get(1).Label = "Nombre de Terminal";
            this.grdTerminales_Sheet1.Columns.Get(1).Locked = true;
            this.grdTerminales_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(1).Width = 235F;
            this.grdTerminales_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdTerminales_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(2).Label = "MAC Address";
            this.grdTerminales_Sheet1.Columns.Get(2).Locked = true;
            this.grdTerminales_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(2).Width = 127F;
            this.grdTerminales_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdTerminales_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(3).Label = "Servidor";
            this.grdTerminales_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(3).Width = 65F;
            this.grdTerminales_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.grdTerminales_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(4).Label = "Habilitar";
            this.grdTerminales_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(4).Width = 65F;
            this.grdTerminales_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdTerminales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmTerminales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 300);
            this.Controls.Add(this.FrameTerminales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmTerminales";
            this.Text = "Terminales de registradas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTerminales_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTerminales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTerminales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTerminales_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameTerminales;
        private FarPoint.Win.Spread.FpSpread grdTerminales;
        private FarPoint.Win.Spread.SheetView grdTerminales_Sheet1;
    }
}