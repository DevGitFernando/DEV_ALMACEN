namespace Dll_SII_IMediaccess.Ventas_IME
{
    partial class FrmRegistroDeVales
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistroDeVales));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSucursalSocioComercial = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSocioComercial = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaDeReceta = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolioVale = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus3 = new SC_ControlsCS.scLabelStatus();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(7, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(929, 284);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de vale";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(11, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(906, 261);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 3;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 140F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 600F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 110F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(945, 25);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSucursalSocioComercial);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblSocioComercial);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpFechaDeReceta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFolioVale);
            this.groupBox1.Controls.Add(this.scLabelStatus3);
            this.groupBox1.Location = new System.Drawing.Point(7, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(929, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información general de vale";
            // 
            // lblSucursalSocioComercial
            // 
            this.lblSucursalSocioComercial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSucursalSocioComercial.Location = new System.Drawing.Point(133, 46);
            this.lblSucursalSocioComercial.Name = "lblSucursalSocioComercial";
            this.lblSucursalSocioComercial.Size = new System.Drawing.Size(509, 20);
            this.lblSucursalSocioComercial.TabIndex = 56;
            this.lblSucursalSocioComercial.Text = "Nombre :";
            this.lblSucursalSocioComercial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 16);
            this.label6.TabIndex = 57;
            this.label6.Text = "Sucursal : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSocioComercial
            // 
            this.lblSocioComercial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSocioComercial.Location = new System.Drawing.Point(133, 20);
            this.lblSocioComercial.Name = "lblSocioComercial";
            this.lblSocioComercial.Size = new System.Drawing.Size(509, 20);
            this.lblSocioComercial.TabIndex = 54;
            this.lblSocioComercial.Text = "Nombre :";
            this.lblSocioComercial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 16);
            this.label3.TabIndex = 55;
            this.label3.Text = "Socio comercial : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDeReceta
            // 
            this.dtpFechaDeReceta.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeReceta.Enabled = false;
            this.dtpFechaDeReceta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeReceta.Location = new System.Drawing.Point(791, 20);
            this.dtpFechaDeReceta.Name = "dtpFechaDeReceta";
            this.dtpFechaDeReceta.Size = new System.Drawing.Size(128, 20);
            this.dtpFechaDeReceta.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(698, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Fecha de Vale :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioVale
            // 
            this.txtFolioVale.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioVale.Decimales = 2;
            this.txtFolioVale.Enabled = false;
            this.txtFolioVale.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioVale.ForeColor = System.Drawing.Color.Black;
            this.txtFolioVale.Location = new System.Drawing.Point(791, 46);
            this.txtFolioVale.MaxLength = 50;
            this.txtFolioVale.Name = "txtFolioVale";
            this.txtFolioVale.PermitirApostrofo = false;
            this.txtFolioVale.PermitirNegativos = false;
            this.txtFolioVale.Size = new System.Drawing.Size(128, 20);
            this.txtFolioVale.TabIndex = 1;
            this.txtFolioVale.Text = "E006493943";
            this.txtFolioVale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus3
            // 
            this.scLabelStatus3.Location = new System.Drawing.Point(700, 49);
            this.scLabelStatus3.Name = "scLabelStatus3";
            this.scLabelStatus3.Size = new System.Drawing.Size(90, 15);
            this.scLabelStatus3.TabIndex = 4;
            this.scLabelStatus3.Text = "Folio de vale : ";
            this.scLabelStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmRegistroDeVales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 393);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmRegistroDeVales";
            this.Text = "Registro de Vales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistroDeVales_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtFolioVale;
        private SC_ControlsCS.scLabelStatus scLabelStatus3;
        private System.Windows.Forms.DateTimePicker dtpFechaDeReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSocioComercial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSucursalSocioComercial;
        private System.Windows.Forms.Label label6;
    }
}