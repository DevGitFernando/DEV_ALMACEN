namespace Dll_IGPI.Configuracion
{
    partial class FrmParametros
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParametros));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.grdParametros = new FarPoint.Win.Spread.FpSpread();
            this.grdParametros_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.grdParametros);
            this.groupBox1.Location = new System.Drawing.Point(7, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(833, 378);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(13, 318);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(809, 50);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "label1";
            // 
            // grdParametros
            // 
            this.grdParametros.AccessibleDescription = "grdParametros, Sheet1, Row 0, Column 0, ";
            this.grdParametros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdParametros.Location = new System.Drawing.Point(13, 17);
            this.grdParametros.Name = "grdParametros";
            this.grdParametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdParametros.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdParametros_Sheet1});
            this.grdParametros.Size = new System.Drawing.Size(809, 296);
            this.grdParametros.TabIndex = 0;
            this.grdParametros.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.grdParametros_LeaveCell);
            // 
            // grdParametros_Sheet1
            // 
            this.grdParametros_Sheet1.Reset();
            this.grdParametros_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdParametros_Sheet1.ColumnCount = 3;
            this.grdParametros_Sheet1.RowCount = 12;
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Parametro";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Valor";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdParametros_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdParametros_Sheet1.Columns.Get(0).CellType = textCellType10;
            this.grdParametros_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(0).Label = "Parametro";
            this.grdParametros_Sheet1.Columns.Get(0).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(0).Width = 266F;
            textCellType11.MaxLength = 300;
            this.grdParametros_Sheet1.Columns.Get(1).CellType = textCellType11;
            this.grdParametros_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(1).Label = "Valor";
            this.grdParametros_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(1).Width = 487F;
            this.grdParametros_Sheet1.Columns.Get(2).CellType = textCellType12;
            this.grdParametros_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdParametros_Sheet1.Columns.Get(2).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(2).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(2).Width = 245F;
            this.grdParametros_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(850, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            this.btnGuardar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 412);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmParametros";
            this.Text = "Configuración de Parametros de Interface MACH4";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmParametros_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdParametros;
        private FarPoint.Win.Spread.SheetView grdParametros_Sheet1;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}