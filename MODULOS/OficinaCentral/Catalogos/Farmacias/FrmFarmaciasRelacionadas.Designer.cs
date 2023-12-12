namespace OficinaCentral.Catalogos
{
    partial class FrmFarmaciasRelacionadas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFarmaciasRelacionadas));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFarmacia);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.txtIdEstado);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtIdFarmacia);
            this.groupBox2.Location = new System.Drawing.Point(13, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(872, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de farmacia base";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(199, 59);
            this.lblFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(658, 26);
            this.lblFarmacia.TabIndex = 9;
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(199, 27);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(658, 26);
            this.lblEstado.TabIndex = 8;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(104, 29);
            this.txtIdEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdEstado.MaxLength = 4;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(87, 22);
            this.txtIdEstado.TabIndex = 0;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.Validated += new System.EventHandler(this.txtIdEstado_Validated);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(104, 59);
            this.txtIdFarmacia.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(87, 22);
            this.txtIdFarmacia.TabIndex = 1;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFarmacia_Validating);
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(898, 25);
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
            this.btnNuevo.Text = "&Nuevo";
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
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdFarmacias);
            this.groupBox1.Location = new System.Drawing.Point(13, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 414);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Farmacias relacionadas";
            // 
            // grdFarmacias
            // 
            this.grdFarmacias.AccessibleDescription = "grdFarmacias, Sheet1, Row 0, Column 0, ";
            this.grdFarmacias.BackColor = System.Drawing.Color.Transparent;
            this.grdFarmacias.Location = new System.Drawing.Point(7, 22);
            this.grdFarmacias.Margin = new System.Windows.Forms.Padding(4);
            this.grdFarmacias.Name = "grdFarmacias";
            this.grdFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFarmacias_Sheet1});
            this.grdFarmacias.Size = new System.Drawing.Size(858, 383);
            this.grdFarmacias.TabIndex = 1;
            this.grdFarmacias.EditModeOn += new System.EventHandler(this.grdFarmacias_EditModeOn);
            this.grdFarmacias.EditModeOff += new System.EventHandler(this.grdFarmacias_EditModeOff);
            this.grdFarmacias.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdFarmacias_Advance);
            this.grdFarmacias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFarmacias_KeyDown);
            // 
            // grdFarmacias_Sheet1
            // 
            this.grdFarmacias_Sheet1.Reset();
            this.grdFarmacias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFarmacias_Sheet1.ColumnCount = 3;
            this.grdFarmacias_Sheet1.RowCount = 8;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Farmacia";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdFarmacias_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            textCellType1.MaxLength = 4;
            this.grdFarmacias_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdFarmacias_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Label = "Id Farmacia";
            this.grdFarmacias_Sheet1.Columns.Get(0).Locked = false;
            this.grdFarmacias_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Width = 100F;
            textCellType2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdFarmacias_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFarmacias_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdFarmacias_Sheet1.Columns.Get(1).Locked = true;
            this.grdFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(1).Width = 400F;
            this.grdFarmacias_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdFarmacias_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(2).Label = "Status";
            this.grdFarmacias_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(2).Width = 79F;
            this.grdFarmacias_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmFarmaciasRelacionadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 559);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmFarmaciasRelacionadas";
            this.Text = "Farmacias relacionadas para transferencias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFarmaciasRelacionadas_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdFarmacias;
        private FarPoint.Win.Spread.SheetView grdFarmacias_Sheet1;
        private System.Windows.Forms.Label lblFarmacia;
        private System.Windows.Forms.Label lblEstado;
    }
}