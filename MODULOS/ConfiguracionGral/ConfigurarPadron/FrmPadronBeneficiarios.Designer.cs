namespace Configuracion.ConfigurarPadron
{
    partial class FrmPadronBeneficiarios
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType11 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType12 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPadronBeneficiarios));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdPadrones = new FarPoint.Win.Spread.FpSpread();
            this.grdPadrones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBD = new System.Windows.Forms.CheckBox();
            this.cboBasesDeDatos = new SC_ControlsCS.scComboBoxExt();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPadrones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPadrones_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdPadrones);
            this.groupBox2.Location = new System.Drawing.Point(8, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(843, 313);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Padrones";
            // 
            // grdPadrones
            // 
            this.grdPadrones.AccessibleDescription = "grdPadrones, Sheet1, Row 0, Column 0, ";
            this.grdPadrones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPadrones.Location = new System.Drawing.Point(8, 19);
            this.grdPadrones.Name = "grdPadrones";
            this.grdPadrones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPadrones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPadrones_Sheet1});
            this.grdPadrones.Size = new System.Drawing.Size(827, 283);
            this.grdPadrones.TabIndex = 0;
            this.grdPadrones.EditModeOff += new System.EventHandler(this.grdPadrones_EditModeOff);
            this.grdPadrones.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdPadrones_Advance);
            // 
            // grdPadrones_Sheet1
            // 
            this.grdPadrones_Sheet1.Reset();
            this.grdPadrones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPadrones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPadrones_Sheet1.ColumnCount = 6;
            this.grdPadrones_Sheet1.RowCount = 10;
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Cliente";
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre Cliente";
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Padrón";
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status";
            this.grdPadrones_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Acceso local";
            this.grdPadrones_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            textCellType16.MaxLength = 4;
            this.grdPadrones_Sheet1.Columns.Get(0).CellType = textCellType16;
            this.grdPadrones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdPadrones_Sheet1.Columns.Get(0).Locked = false;
            this.grdPadrones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(0).Width = 70F;
            this.grdPadrones_Sheet1.Columns.Get(1).CellType = textCellType17;
            this.grdPadrones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(1).Label = "Cliente";
            this.grdPadrones_Sheet1.Columns.Get(1).Locked = false;
            this.grdPadrones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(1).Width = 70F;
            this.grdPadrones_Sheet1.Columns.Get(2).CellType = textCellType18;
            this.grdPadrones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPadrones_Sheet1.Columns.Get(2).Label = "Nombre Cliente";
            this.grdPadrones_Sheet1.Columns.Get(2).Locked = true;
            this.grdPadrones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(2).Width = 300F;
            this.grdPadrones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPadrones_Sheet1.Columns.Get(3).Label = "Padrón";
            this.grdPadrones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(3).Width = 200F;
            this.grdPadrones_Sheet1.Columns.Get(4).CellType = checkBoxCellType11;
            this.grdPadrones_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(4).Label = "Status";
            this.grdPadrones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(4).Width = 65F;
            this.grdPadrones_Sheet1.Columns.Get(5).CellType = checkBoxCellType12;
            this.grdPadrones_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(5).Label = "Acceso local";
            this.grdPadrones_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPadrones_Sheet1.Columns.Get(5).Width = 65F;
            this.grdPadrones_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdPadrones_Sheet1.Rows.Get(0).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(1).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(2).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(3).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(4).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(5).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(6).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(7).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(8).Height = 23F;
            this.grdPadrones_Sheet1.Rows.Get(9).Height = 23F;
            this.grdPadrones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(858, 25);
            this.toolStripBarraMenu.TabIndex = 10;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBD);
            this.groupBox1.Controls.Add(this.cboBasesDeDatos);
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(843, 56);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bases de Datos";
            // 
            // chkBD
            // 
            this.chkBD.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBD.Location = new System.Drawing.Point(777, 23);
            this.chkBD.Name = "chkBD";
            this.chkBD.Size = new System.Drawing.Size(58, 17);
            this.chkBD.TabIndex = 1;
            this.chkBD.Text = "Activa";
            this.chkBD.UseVisualStyleBackColor = true;
            // 
            // cboBasesDeDatos
            // 
            this.cboBasesDeDatos.BackColorEnabled = System.Drawing.Color.White;
            this.cboBasesDeDatos.Data = "";
            this.cboBasesDeDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBasesDeDatos.Filtro = " 1 = 1";
            this.cboBasesDeDatos.FormattingEnabled = true;
            this.cboBasesDeDatos.ListaItemsBusqueda = 20;
            this.cboBasesDeDatos.Location = new System.Drawing.Point(10, 19);
            this.cboBasesDeDatos.MostrarToolTip = false;
            this.cboBasesDeDatos.Name = "cboBasesDeDatos";
            this.cboBasesDeDatos.Size = new System.Drawing.Size(761, 21);
            this.cboBasesDeDatos.TabIndex = 0;
            this.cboBasesDeDatos.SelectedIndexChanged += new System.EventHandler(this.cboBasesDeDatos_SelectedIndexChanged);
            // 
            // FrmPadronBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 405);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPadronBeneficiarios";
            this.Text = "Configuración de Padron de Beneficiarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPadronBeneficiarios_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPadrones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPadrones_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdPadrones;
        private FarPoint.Win.Spread.SheetView grdPadrones_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt cboBasesDeDatos;
        private System.Windows.Forms.CheckBox chkBD;
    }
}