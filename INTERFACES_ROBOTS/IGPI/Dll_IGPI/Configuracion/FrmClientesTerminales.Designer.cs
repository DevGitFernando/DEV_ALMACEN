namespace Dll_IGPI.Configuracion
{
    partial class FrmClientesTerminales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientesTerminales));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdCliente = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdTerminales = new FarPoint.Win.Spread.FpSpread();
            this.grdTerminales_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmIGPI = new System.Windows.Forms.Timer();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(598, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIdCliente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Location = new System.Drawing.Point(7, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 47);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Cliente";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdCliente.Decimales = 2;
            this.txtIdCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdCliente.ForeColor = System.Drawing.Color.Black;
            this.txtIdCliente.Location = new System.Drawing.Point(70, 18);
            this.txtIdCliente.MaxLength = 4;
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.PermitirApostrofo = false;
            this.txtIdCliente.PermitirNegativos = false;
            this.txtIdCliente.Size = new System.Drawing.Size(61, 20);
            this.txtIdCliente.TabIndex = 0;
            this.txtIdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdCliente_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(137, 17);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(436, 20);
            this.lblCliente.TabIndex = 3;
            this.lblCliente.Text = "Descripción :";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdTerminales);
            this.groupBox2.Location = new System.Drawing.Point(7, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(582, 230);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Terminales";
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
            this.grdTerminales.Size = new System.Drawing.Size(565, 202);
            this.grdTerminales.TabIndex = 0;
            this.grdTerminales.EditModeOn += new System.EventHandler(this.grdTerminales_EditModeOn);
            this.grdTerminales.EditModeOff += new System.EventHandler(this.grdTerminales_EditModeOff);
            this.grdTerminales.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdTerminales_Advance);
            this.grdTerminales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdTerminales_KeyDown);
            // 
            // grdTerminales_Sheet1
            // 
            this.grdTerminales_Sheet1.Reset();
            this.grdTerminales_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdTerminales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdTerminales_Sheet1.ColumnCount = 5;
            this.grdTerminales_Sheet1.RowCount = 8;
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Terminal";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de Terminal";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Asignar";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Habilitar";
            this.grdTerminales_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Ventanilla de Dispensación";
            this.grdTerminales_Sheet1.ColumnHeader.Rows.Get(0).Height = 32F;
            textCellType1.MaxLength = 4;
            this.grdTerminales_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdTerminales_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(0).Label = "Terminal";
            this.grdTerminales_Sheet1.Columns.Get(0).Locked = false;
            this.grdTerminales_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdTerminales_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTerminales_Sheet1.Columns.Get(1).Label = "Nombre de Terminal";
            this.grdTerminales_Sheet1.Columns.Get(1).Locked = true;
            this.grdTerminales_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(1).Width = 235F;
            this.grdTerminales_Sheet1.Columns.Get(2).CellType = checkBoxCellType1;
            this.grdTerminales_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(2).Label = "Asignar";
            this.grdTerminales_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(2).Width = 65F;
            this.grdTerminales_Sheet1.Columns.Get(3).CellType = checkBoxCellType2;
            this.grdTerminales_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(3).Label = "Habilitar";
            this.grdTerminales_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(3).Width = 65F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 99D;
            numberCellType1.MinimumValue = 0D;
            this.grdTerminales_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdTerminales_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(4).Label = "Ventanilla de Dispensación";
            this.grdTerminales_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTerminales_Sheet1.Columns.Get(4).Width = 83F;
            this.grdTerminales_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdTerminales_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmIGPI
            // 
            this.tmIGPI.Interval = 500;
            this.tmIGPI.Tick += new System.EventHandler(this.tmIGPI_Tick);
            // 
            // FrmClientesTerminales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 318);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmClientesTerminales";
            this.Text = "Asignar Terminales a Clientes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmClientesTerminales_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdTerminales;
        private FarPoint.Win.Spread.SheetView grdTerminales_Sheet1;
        private System.Windows.Forms.Timer tmIGPI;
    }
}