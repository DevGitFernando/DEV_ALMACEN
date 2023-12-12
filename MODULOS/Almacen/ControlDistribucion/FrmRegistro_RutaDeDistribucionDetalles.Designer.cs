namespace Almacen.ControlDistribucion
{
    partial class FrmRegistro_RutaDeDistribucionDetalles
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
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer3 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer4 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistro_RutaDeDistribucionDetalles));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.grdDetalle = new FarPoint.Win.Spread.FpSpread();
            this.grdDetalle_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grdDetalle);
            this.groupBox1.Location = new System.Drawing.Point(16, 62);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1361, 556);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detallado";
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(865, 28);
            this.chkMarcarDesmarcar.Margin = new System.Windows.Forms.Padding(4);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(180, 22);
            this.chkMarcarDesmarcar.TabIndex = 3;
            this.chkMarcarDesmarcar.Text = "Marcar | DesMarcar todo";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // grdDetalle
            // 
            this.grdDetalle.AccessibleDescription = "grdDetalle, Sheet1, Row 0, Column 0, ";
            this.grdDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDetalle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDetalle.HorizontalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdDetalle.HorizontalScrollBar.Name = "";
            enhancedScrollBarRenderer3.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer3.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer3.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer3.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer3.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer3.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer3.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer3.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer3.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer3.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer3.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdDetalle.HorizontalScrollBar.Renderer = enhancedScrollBarRenderer3;
            this.grdDetalle.HorizontalScrollBar.TabIndex = 2;
            this.grdDetalle.Location = new System.Drawing.Point(12, 23);
            this.grdDetalle.Margin = new System.Windows.Forms.Padding(4);
            this.grdDetalle.Name = "grdDetalle";
            this.grdDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDetalle.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDetalle_Sheet1});
            this.grdDetalle.Size = new System.Drawing.Size(1336, 520);
            this.grdDetalle.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Seashell;
            this.grdDetalle.TabIndex = 1;
            this.grdDetalle.VerticalScrollBar.Buttons = new FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardLineButton,ThumbTrack,ForwardLineButton");
            this.grdDetalle.VerticalScrollBar.Name = "";
            enhancedScrollBarRenderer4.ArrowColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer4.ArrowHoveredColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer4.ArrowSelectedColor = System.Drawing.Color.DarkSlateGray;
            enhancedScrollBarRenderer4.ButtonBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer4.ButtonBorderColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer4.ButtonHoveredBackgroundColor = System.Drawing.Color.SlateGray;
            enhancedScrollBarRenderer4.ButtonHoveredBorderColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer4.ButtonSelectedBackgroundColor = System.Drawing.Color.DarkGray;
            enhancedScrollBarRenderer4.ButtonSelectedBorderColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer4.TrackBarBackgroundColor = System.Drawing.Color.CadetBlue;
            enhancedScrollBarRenderer4.TrackBarSelectedBackgroundColor = System.Drawing.Color.SlateGray;
            this.grdDetalle.VerticalScrollBar.Renderer = enhancedScrollBarRenderer4;
            this.grdDetalle.VerticalScrollBar.TabIndex = 3;
            this.grdDetalle.EditModeOff += new System.EventHandler(this.grdTransferencias_EditModeOff);
            // 
            // grdDetalle_Sheet1
            // 
            this.grdDetalle_Sheet1.Reset();
            this.grdDetalle_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDetalle_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDetalle_Sheet1.ColumnCount = 7;
            this.grdDetalle_Sheet1.RowCount = 16;
            this.grdDetalle_Sheet1.Cells.Get(1, 3).Locked = false;
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Ruta";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio Traspaso";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fecha";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Unidad Recibe";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Piezas";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Modificado";
            this.grdDetalle_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Agregar";
            this.grdDetalle_Sheet1.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderSeashell";
            this.grdDetalle_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdDetalle_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdDetalle_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalle_Sheet1.Columns.Get(0).Label = "Ruta";
            this.grdDetalle_Sheet1.Columns.Get(0).Locked = true;
            this.grdDetalle_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(0).Width = 170F;
            textCellType6.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType6.MaxLength = 15;
            this.grdDetalle_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdDetalle_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(1).Label = "Folio Traspaso";
            this.grdDetalle_Sheet1.Columns.Get(1).Locked = true;
            this.grdDetalle_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(1).Width = 130F;
            this.grdDetalle_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdDetalle_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(2).Label = "Fecha";
            this.grdDetalle_Sheet1.Columns.Get(2).Locked = true;
            this.grdDetalle_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(2).Width = 100F;
            this.grdDetalle_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdDetalle_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalle_Sheet1.Columns.Get(3).Label = "Unidad Recibe";
            this.grdDetalle_Sheet1.Columns.Get(3).Locked = true;
            this.grdDetalle_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(3).Width = 400F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            numberCellType2.SpinDecimalIncrement = 0F;
            this.grdDetalle_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdDetalle_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdDetalle_Sheet1.Columns.Get(4).Label = "Piezas";
            this.grdDetalle_Sheet1.Columns.Get(4).Locked = true;
            this.grdDetalle_Sheet1.Columns.Get(4).Width = 86F;
            checkBoxCellType3.TextAlign = FarPoint.Win.ButtonTextAlign.TextBottomPictTop;
            this.grdDetalle_Sheet1.Columns.Get(5).CellType = checkBoxCellType3;
            this.grdDetalle_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(5).Label = "Modificado";
            this.grdDetalle_Sheet1.Columns.Get(6).CellType = checkBoxCellType4;
            this.grdDetalle_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalle_Sheet1.Columns.Get(6).Label = "Agregar";
            this.grdDetalle_Sheet1.Columns.Get(6).Locked = false;
            this.grdDetalle_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalle_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdDetalle_Sheet1.RowHeader.DefaultStyle.Parent = "RowHeaderSeashell";
            this.grdDetalle_Sheet1.SheetCornerStyle.Parent = "CornerSeashell";
            this.grdDetalle_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1392, 58);
            this.toolStripBarraMenu.TabIndex = 10;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Visible = false;
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 58);
            this.toolStripSeparator.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(54, 55);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 58);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(54, 55);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 58);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // FrmRegistro_RutaDeDistribucionDetalles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1392, 629);
            this.ControlBox = false;
            this.Controls.Add(this.chkMarcarDesmarcar);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmRegistro_RutaDeDistribucionDetalles";
            this.ShowIcon = false;
            this.Text = "Detalles";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistro_RutaDeDistribucionDetalles_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdDetalle;
        private FarPoint.Win.Spread.SheetView grdDetalle_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
    }
}