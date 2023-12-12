namespace DllFarmaciaSoft.Devoluciones
{
    partial class FrmListadoMotivosDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoMotivosDev));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblIdTipo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalle = new System.Windows.Forms.GroupBox();
            this.grdMotivos = new FarPoint.Win.Spread.FpSpread();
            this.grdMotivos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMotivos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // myControlInicio
            // 
            this.myControlInicio.Text = "Motivos Devolución";
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1417, 27);
            this.toolStripBarraMenu.TabIndex = 7;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(29, 24);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.lblIdTipo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1064, 54);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(291, 95);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Tipo de Movimiento";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(109, 58);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(640, 23);
            this.lblDescripcion.TabIndex = 12;
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdTipo
            // 
            this.lblIdTipo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdTipo.Location = new System.Drawing.Point(109, 28);
            this.lblIdTipo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIdTipo.Name = "lblIdTipo";
            this.lblIdTipo.Size = new System.Drawing.Size(108, 23);
            this.lblIdTipo.TabIndex = 11;
            this.lblIdTipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tipo Movto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalle
            // 
            this.FrameDetalle.Controls.Add(this.grdMotivos);
            this.FrameDetalle.Location = new System.Drawing.Point(15, 34);
            this.FrameDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Size = new System.Drawing.Size(843, 375);
            this.FrameDetalle.TabIndex = 9;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Motivos por los que se aplica Devolución";
            // 
            // grdMotivos
            // 
            this.grdMotivos.AccessibleDescription = "grdMotivos, Sheet1, Row 0, Column 0, ";
            this.grdMotivos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdMotivos.Location = new System.Drawing.Point(12, 20);
            this.grdMotivos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdMotivos.Name = "grdMotivos";
            this.grdMotivos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdMotivos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdMotivos_Sheet1});
            this.grdMotivos.Size = new System.Drawing.Size(820, 347);
            this.grdMotivos.TabIndex = 0;
            // 
            // grdMotivos_Sheet1
            // 
            this.grdMotivos_Sheet1.Reset();
            this.grdMotivos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdMotivos_Sheet1.ColumnCount = 5;
            this.grdMotivos_Sheet1.RowCount = 8;
            this.grdMotivos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdTipoMovto";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Id Motivo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Motivo";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Marcar";
            this.grdMotivos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status";
            this.grdMotivos_Sheet1.ColumnHeader.Rows.Get(0).Height = 23F;
            this.grdMotivos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdMotivos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Label = "IdTipoMovto";
            this.grdMotivos_Sheet1.Columns.Get(0).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(0).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(0).Width = 73F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdMotivos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdMotivos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(1).Label = "Id Motivo";
            this.grdMotivos_Sheet1.Columns.Get(1).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(1).Visible = false;
            this.grdMotivos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdMotivos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdMotivos_Sheet1.Columns.Get(2).Label = "Motivo";
            this.grdMotivos_Sheet1.Columns.Get(2).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(2).Width = 500F;
            this.grdMotivos_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdMotivos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(3).Label = "Marcar";
            this.grdMotivos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(4).CellType = checkBoxCellType2;
            this.grdMotivos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(4).Label = "Status";
            this.grdMotivos_Sheet1.Columns.Get(4).Locked = true;
            this.grdMotivos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdMotivos_Sheet1.Columns.Get(4).Visible = false;
            this.grdMotivos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdMotivos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmListadoMotivosDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1417, 420);
            this.Controls.Add(this.FrameDetalle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmListadoMotivosDev";
            this.ShowIcon = false;
            this.Text = "Motivos Devolución";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmListadoMotivosDev_FormClosing);
            this.Load += new System.EventHandler(this.FrmListadoMotivosDev_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblIdTipo;
        private System.Windows.Forms.GroupBox FrameDetalle;
        private FarPoint.Win.Spread.FpSpread grdMotivos;
        private FarPoint.Win.Spread.SheetView grdMotivos_Sheet1;
    }
}