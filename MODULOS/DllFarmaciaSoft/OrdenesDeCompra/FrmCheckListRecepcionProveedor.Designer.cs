namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmCheckListRecepcionProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckListRecepcionProveedor));
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType7 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType9 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType10 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType11 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType12 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.FrameDetalle = new System.Windows.Forms.GroupBox();
            this.grdMotivos = new FarPoint.Win.Spread.FpSpread();
            this.grdMotivos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1075, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FrameDetalle
            // 
            this.FrameDetalle.Controls.Add(this.grdMotivos);
            this.FrameDetalle.Location = new System.Drawing.Point(10, 28);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Size = new System.Drawing.Size(1055, 428);
            this.FrameDetalle.TabIndex = 9;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Listado de puntos a evaluar";
            // 
            // grdMotivos
            // 
            this.grdMotivos.AccessibleDescription = "grdMotivos, Sheet1, Row 0, Column 0, ";
            this.grdMotivos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMotivos.Location = new System.Drawing.Point(9, 18);
            this.grdMotivos.Name = "grdMotivos";
            this.grdMotivos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMotivos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMotivos_Sheet1});
            this.grdMotivos.Size = new System.Drawing.Size(1036, 404);
            this.grdMotivos.TabIndex = 0;
            this.grdMotivos.EditModeOff += new System.EventHandler(this.grdMotivos_EditModeOff);
            // 
            // grdMotivos_Sheet1
            // 
            this.grdMotivos_Sheet1.Reset();
            this.grdMotivos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMotivos_Sheet1.ColumnCount = 11;
            this.grdMotivos_Sheet1.RowCount = 20;
            this.grdMotivos_Sheet1.Cells.Get(1, 3).Locked = false;
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdGrupo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdMotivo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Grupo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Comprobación";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "SI";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "SI_RequiereFirma";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "NO";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "NO_ReguiereFirma";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Rechazo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Rechazo_RequiereFirma";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Comentario";
            this.grdMotivos_Sheet1.ColumnHeader.Rows.Get(0).Height = 23F;
            this.grdMotivos_Sheet1.Columns.Get(0).CellType = textCellType6;
            this.grdMotivos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Label = "IdGrupo";
            this.grdMotivos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(0).Width = 73F;
            this.grdMotivos_Sheet1.Columns.Get(1).CellType = textCellType7;
            this.grdMotivos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(1).Label = "IdMotivo";
            this.grdMotivos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(1).Visible = false;
            textCellType8.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType8.MaxLength = 100;
            this.grdMotivos_Sheet1.Columns.Get(2).CellType = textCellType8;
            this.grdMotivos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(2).Label = "Grupo";
            this.grdMotivos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(2).Width = 250F;
            this.grdMotivos_Sheet1.Columns.Get(3).CellType = textCellType9;
            this.grdMotivos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(3).Label = "Comprobación";
            this.grdMotivos_Sheet1.Columns.Get(3).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(3).Width = 320F;
            this.grdMotivos_Sheet1.Columns.Get(4).CellType = checkBoxCellType7;
            this.grdMotivos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(4).Label = "SI";
            this.grdMotivos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(5).CellType = checkBoxCellType8;
            this.grdMotivos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(5).Label = "SI_RequiereFirma";
            this.grdMotivos_Sheet1.Columns.Get(5).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(5).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(6).CellType = checkBoxCellType9;
            this.grdMotivos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(6).Label = "NO";
            this.grdMotivos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(7).CellType = checkBoxCellType10;
            this.grdMotivos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(7).Label = "NO_ReguiereFirma";
            this.grdMotivos_Sheet1.Columns.Get(7).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(7).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(8).CellType = checkBoxCellType11;
            this.grdMotivos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(8).Label = "Rechazo";
            this.grdMotivos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(9).CellType = checkBoxCellType12;
            this.grdMotivos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(9).Label = "Rechazo_RequiereFirma";
            this.grdMotivos_Sheet1.Columns.Get(9).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(9).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(10).CellType = textCellType10;
            this.grdMotivos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(10).Label = "Comentario";
            this.grdMotivos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(10).Width = 230F;
            this.grdMotivos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmCheckListRecepcionProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 465);
            this.Controls.Add(this.FrameDetalle);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCheckListRecepcionProveedor";
            this.Text = "Evaluación de recepción de orden de compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmListadoMotivosDev_FormClosing);
            this.Load += new System.EventHandler(this.FrmListadoMotivosDev_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.GroupBox FrameDetalle;
        private FarPoint.Win.Spread.FpSpread grdMotivos;
        private FarPoint.Win.Spread.SheetView grdMotivos_Sheet1;
    }
}