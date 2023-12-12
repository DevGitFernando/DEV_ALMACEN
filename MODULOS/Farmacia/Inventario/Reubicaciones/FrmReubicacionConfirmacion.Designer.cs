namespace Farmacia.Inventario.Reubicaciones
{
    partial class FrmReubicacionConfirmacion
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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.NamedStyle namedStyle5 = new FarPoint.Win.Spread.NamedStyle("ColumnHeaderEnhanced");
            FarPoint.Win.Spread.NamedStyle namedStyle6 = new FarPoint.Win.Spread.NamedStyle("RowHeaderEnhanced");
            FarPoint.Win.Spread.NamedStyle namedStyle7 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer2 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.NamedStyle namedStyle8 = new FarPoint.Win.Spread.NamedStyle("DataAreaDefault");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReubicacionConfirmacion));
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdDescripcion = new FarPoint.Win.Spread.FpSpread();
            this.grdDescripcion_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblConfirmado = new System.Windows.Forms.Label();
            this.lblFolioReferencia = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirmar = new System.Windows.Forms.ToolStripButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDescripcion_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.SteelBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(0, 268);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(689, 52);
            this.label2.TabIndex = 31;
            this.label2.Text = "Se solicitara autorización a Folios con mas de 24 horas transcurridas. Es necesar" +
    "ia la huella de quien Autoriza.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(137, 25);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolio.MaxLength = 30;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(145, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.TextChanged += new System.EventHandler(this.txtFolio_TextChanged);
            this.txtFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolio_KeyDown);
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(80, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(312, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 15);
            this.label3.TabIndex = 34;
            this.label3.Text = "Folio Referencia:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdDescripcion);
            this.groupBox3.Location = new System.Drawing.Point(13, 101);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(663, 160);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalles";
            // 
            // grdDescripcion
            // 
            this.grdDescripcion.AccessibleDescription = "grdDescripcion, Sheet1, Row 0, Column 0, ";
            this.grdDescripcion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDescripcion.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdDescripcion.Location = new System.Drawing.Point(13, 25);
            this.grdDescripcion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grdDescripcion.Name = "grdDescripcion";
            namedStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(220)))), ((int)(((byte)(233)))));
            namedStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle5.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle5.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            namedStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            namedStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle6.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle6.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            namedStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(196)))), ((int)(((byte)(233)))));
            namedStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle7.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle7.Renderer = enhancedCornerRenderer2;
            namedStyle7.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            namedStyle8.BackColor = System.Drawing.SystemColors.Window;
            namedStyle8.CellType = generalCellType2;
            namedStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle8.Renderer = generalCellType2;
            this.grdDescripcion.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle5,
            namedStyle6,
            namedStyle7,
            namedStyle8});
            this.grdDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDescripcion.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDescripcion_Sheet1});
            this.grdDescripcion.Size = new System.Drawing.Size(636, 118);
            this.grdDescripcion.TabIndex = 0;
            this.grdDescripcion.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // grdDescripcion_Sheet1
            // 
            this.grdDescripcion_Sheet1.Reset();
            this.grdDescripcion_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDescripcion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDescripcion_Sheet1.ColumnCount = 3;
            this.grdDescripcion_Sheet1.RowCount = 1;
            this.grdDescripcion_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDescripcion_Sheet1.Cells.Get(0, 0).Locked = true;
            this.grdDescripcion_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDescripcion_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDescripcion_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdDescripcion_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.MaximumValue = 9999999D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.SpinDecimalIncrement = 0F;
            this.grdDescripcion_Sheet1.Cells.Get(0, 2).CellType = numberCellType5;
            this.grdDescripcion_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDescripcion_Sheet1.Cells.Get(0, 2).Locked = true;
            this.grdDescripcion_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDescripcion_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Cantidad de Claves";
            this.grdDescripcion_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Cantidad de Productos";
            this.grdDescripcion_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad en Piezas";
            this.grdDescripcion_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.DecimalSeparator = ",";
            numberCellType6.MaximumValue = 10000000D;
            numberCellType6.MinimumValue = 0D;
            this.grdDescripcion_Sheet1.Columns.Get(0).CellType = numberCellType6;
            this.grdDescripcion_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdDescripcion_Sheet1.Columns.Get(0).Label = "Cantidad de Claves";
            this.grdDescripcion_Sheet1.Columns.Get(0).Locked = true;
            this.grdDescripcion_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDescripcion_Sheet1.Columns.Get(0).Width = 140F;
            numberCellType7.DecimalPlaces = 0;
            numberCellType7.DecimalSeparator = ",";
            numberCellType7.MaximumValue = 10000000D;
            numberCellType7.MinimumValue = 0D;
            this.grdDescripcion_Sheet1.Columns.Get(1).CellType = numberCellType7;
            this.grdDescripcion_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdDescripcion_Sheet1.Columns.Get(1).Label = "Cantidad de Productos";
            this.grdDescripcion_Sheet1.Columns.Get(1).Locked = true;
            this.grdDescripcion_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDescripcion_Sheet1.Columns.Get(1).Width = 140F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.DecimalSeparator = ".";
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = 0D;
            numberCellType8.Separator = ",";
            numberCellType8.ShowSeparator = true;
            this.grdDescripcion_Sheet1.Columns.Get(2).CellType = numberCellType8;
            this.grdDescripcion_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdDescripcion_Sheet1.Columns.Get(2).Label = "Cantidad en Piezas";
            this.grdDescripcion_Sheet1.Columns.Get(2).Locked = true;
            this.grdDescripcion_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDescripcion_Sheet1.Columns.Get(2).Width = 140F;
            this.grdDescripcion_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdDescripcion_Sheet1.Rows.Get(0).Height = 37F;
            this.grdDescripcion_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblConfirmado);
            this.groupBox4.Controls.Add(this.lblFolioReferencia);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtFolio);
            this.groupBox4.Location = new System.Drawing.Point(13, 34);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(663, 64);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información";
            // 
            // lblConfirmado
            // 
            this.lblConfirmado.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblConfirmado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmado.Location = new System.Drawing.Point(120, -4);
            this.lblConfirmado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmado.Name = "lblConfirmado";
            this.lblConfirmado.Size = new System.Drawing.Size(131, 25);
            this.lblConfirmado.TabIndex = 36;
            this.lblConfirmado.Text = "CONFIRMADO";
            this.lblConfirmado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConfirmado.Visible = false;
            // 
            // lblFolioReferencia
            // 
            this.lblFolioReferencia.BackColor = System.Drawing.Color.Transparent;
            this.lblFolioReferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFolioReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolioReferencia.Location = new System.Drawing.Point(449, 25);
            this.lblFolioReferencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolioReferencia.Name = "lblFolioReferencia";
            this.lblFolioReferencia.Size = new System.Drawing.Size(147, 25);
            this.lblFolioReferencia.TabIndex = 35;
            this.lblFolioReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnFirmar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(689, 27);
            this.toolStripBarraMenu.TabIndex = 38;
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
            // btnFirmar
            // 
            this.btnFirmar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirmar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirmar.Image = ((System.Drawing.Image)(resources.GetObject("btnFirmar.Image")));
            this.btnFirmar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirmar.Name = "btnFirmar";
            this.btnFirmar.Size = new System.Drawing.Size(29, 24);
            this.btnFirmar.Text = "&Firmar";
            this.btnFirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFirmar.Click += new System.EventHandler(this.btnFirmar_Click);
            // 
            // FrmReubicacionConfirmacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 320);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmReubicacionConfirmacion";
            this.ShowIcon = false;
            this.Text = "Confirmación Reubicaciones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReubicacionConfirmacion_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDescripcion_Sheet1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdDescripcion;
        private FarPoint.Win.Spread.SheetView grdDescripcion_Sheet1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFirmar;
        private System.Windows.Forms.Label lblFolioReferencia;
        private System.Windows.Forms.Label lblConfirmado;
    }
}