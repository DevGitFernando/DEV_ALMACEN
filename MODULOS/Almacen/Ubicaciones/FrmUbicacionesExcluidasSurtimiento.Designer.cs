namespace Almacen.Ubicaciones
{
    partial class FrmUbicacionesExcluidasSurtimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUbicacionesExcluidasSurtimiento));
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType9 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType10 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType11 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType12 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.grdBaldas = new FarPoint.Win.Spread.FpSpread();
            this.grdBaldas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEstante = new System.Windows.Forms.Label();
            this.txtEstante = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPasillo = new System.Windows.Forms.Label();
            this.txtPasillo = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBaldas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBaldas_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(784, 25);
            this.toolStripBarraMenu.TabIndex = 0;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // grdBaldas
            // 
            this.grdBaldas.AccessibleDescription = "grdBaldas, Sheet1, Row 0, Column 0, ";
            this.grdBaldas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBaldas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdBaldas.Location = new System.Drawing.Point(9, 19);
            this.grdBaldas.Name = "grdBaldas";
            this.grdBaldas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdBaldas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdBaldas_Sheet1});
            this.grdBaldas.Size = new System.Drawing.Size(748, 428);
            this.grdBaldas.TabIndex = 0;
            this.grdBaldas.EditModeOff += new System.EventHandler(this.grdPasillos_EditModeOff);
            this.grdBaldas.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdPasillos_Advance);
            // 
            // grdBaldas_Sheet1
            // 
            this.grdBaldas_Sheet1.Reset();
            this.grdBaldas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdBaldas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdBaldas_Sheet1.ColumnCount = 7;
            this.grdBaldas_Sheet1.RowCount = 8;
            this.grdBaldas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Entrepaño";
            this.grdBaldas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdBaldas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Pickeo";
            this.grdBaldas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Excluidas";
            this.grdBaldas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "StatusAux";
            this.grdBaldas_Sheet1.ColumnHeader.Rows.Get(0).Height = 24F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.MaximumValue = 1000D;
            numberCellType3.MinimumValue = 0D;
            this.grdBaldas_Sheet1.Columns.Get(0).CellType = numberCellType3;
            this.grdBaldas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(0).Label = "Entrepaño";
            this.grdBaldas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdBaldas_Sheet1.Columns.Get(1).CellType = textCellType5;
            this.grdBaldas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdBaldas_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdBaldas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(1).Width = 300F;
            this.grdBaldas_Sheet1.Columns.Get(2).CellType = checkBoxCellType9;
            this.grdBaldas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(2).Label = "Pickeo";
            this.grdBaldas_Sheet1.Columns.Get(2).Locked = true;
            this.grdBaldas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(3).CellType = checkBoxCellType10;
            this.grdBaldas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(3).Label = "Excluidas";
            this.grdBaldas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(4).CellType = checkBoxCellType11;
            this.grdBaldas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(4).Label = "StatusAux";
            this.grdBaldas_Sheet1.Columns.Get(4).Locked = true;
            this.grdBaldas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(4).Visible = false;
            this.grdBaldas_Sheet1.Columns.Get(5).CellType = checkBoxCellType12;
            this.grdBaldas_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(5).Locked = true;
            this.grdBaldas_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(5).Visible = false;
            this.grdBaldas_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.grdBaldas_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(6).Locked = true;
            this.grdBaldas_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdBaldas_Sheet1.Columns.Get(6).Visible = false;
            this.grdBaldas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdBaldas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblEstante);
            this.groupBox1.Controls.Add(this.txtEstante);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPasillo);
            this.groupBox1.Controls.Add(this.txtPasillo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // lblEstante
            // 
            this.lblEstante.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstante.Location = new System.Drawing.Point(149, 43);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(609, 20);
            this.lblEstante.TabIndex = 47;
            this.lblEstante.Text = "label1";
            // 
            // txtEstante
            // 
            this.txtEstante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstante.Decimales = 2;
            this.txtEstante.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstante.ForeColor = System.Drawing.Color.Black;
            this.txtEstante.Location = new System.Drawing.Point(63, 43);
            this.txtEstante.MaxLength = 4;
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.PermitirApostrofo = false;
            this.txtEstante.PermitirNegativos = false;
            this.txtEstante.Size = new System.Drawing.Size(80, 20);
            this.txtEstante.TabIndex = 1;
            this.txtEstante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstante.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstante_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasillo
            // 
            this.lblPasillo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasillo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPasillo.Location = new System.Drawing.Point(149, 18);
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(609, 20);
            this.lblPasillo.TabIndex = 44;
            this.lblPasillo.Text = "label1";
            // 
            // txtPasillo
            // 
            this.txtPasillo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasillo.Decimales = 2;
            this.txtPasillo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPasillo.ForeColor = System.Drawing.Color.Black;
            this.txtPasillo.Location = new System.Drawing.Point(63, 18);
            this.txtPasillo.MaxLength = 4;
            this.txtPasillo.Name = "txtPasillo";
            this.txtPasillo.PermitirApostrofo = false;
            this.txtPasillo.PermitirNegativos = false;
            this.txtPasillo.Size = new System.Drawing.Size(80, 20);
            this.txtPasillo.TabIndex = 0;
            this.txtPasillo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasillo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasillo_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "Rack :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdBaldas);
            this.groupBox2.Location = new System.Drawing.Point(10, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(766, 454);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entrepaños";
            // 
            // FrmUbicacionesExcluidasSurtimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmUbicacionesExcluidasSurtimiento";
            this.Text = "Exclusión de ubicaciones para surtimiento";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAlmacenPasillosEstantesBaldas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBaldas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBaldas_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private FarPoint.Win.Spread.FpSpread grdBaldas;
        private FarPoint.Win.Spread.SheetView grdBaldas_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtPasillo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPasillo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblEstante;
        private SC_ControlsCS.scTextBoxExt txtEstante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}