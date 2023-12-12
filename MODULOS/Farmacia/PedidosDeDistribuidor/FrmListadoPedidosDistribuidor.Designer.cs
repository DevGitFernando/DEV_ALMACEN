namespace Farmacia.PedidosDeDistribuidor
{
    partial class FrmListadoPedidosDistribuidor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoPedidosDistribuidor));
            FarPoint.Win.Spread.CellType.TextCellType textCellType25 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType26 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType27 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType28 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(804, 25);
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
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdExistencia);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(781, 370);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedidos";
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(11, 19);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(762, 342);
            this.grdExistencia.TabIndex = 4;
            this.grdExistencia.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdExistencia_CellDoubleClick);
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 6;
            this.grdExistencia_Sheet1.RowCount = 10;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEmpresa";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdEstado";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Id Farmacia";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Farmacia";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Folio";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "FechaRegistro";
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType25;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "IdEmpresa";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 84F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType26;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "IdEstado";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 90F;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType27;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Id Farmacia";
            this.grdExistencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 83F;
            textCellType28.MaxLength = 1000;
            this.grdExistencia_Sheet1.Columns.Get(3).CellType = textCellType28;
            this.grdExistencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(3).Label = "Farmacia";
            this.grdExistencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Width = 437F;
            this.grdExistencia_Sheet1.Columns.Get(4).CellType = textCellType29;
            this.grdExistencia_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(4).Label = "Folio";
            this.grdExistencia_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(4).Width = 87F;
            this.grdExistencia_Sheet1.Columns.Get(5).CellType = textCellType30;
            this.grdExistencia_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(5).Label = "FechaRegistro";
            this.grdExistencia_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(5).Width = 98F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmListadoPedidosDistribuidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 407);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoPedidosDistribuidor";
            this.Text = "Listado de Pedidos para Distribuir";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmExistenciaPorClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
    }
}