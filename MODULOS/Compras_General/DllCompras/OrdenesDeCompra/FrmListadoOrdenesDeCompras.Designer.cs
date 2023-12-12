namespace DllCompras.OrdenesDeCompra
{
    partial class FrmListadoOrdenesDeCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoOrdenesDeCompras));
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdOrdenCompras = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenCompras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkTodasLasFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNomPersonal = new System.Windows.Forms.Label();
            this.txtProveedor = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNomProv = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).BeginInit();
            this.FrameFechas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
            this.toolStripBarraMenu.TabIndex = 16;
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdOrdenCompras);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(964, 401);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Órdenes de Compra";
            // 
            // grdOrdenCompras
            // 
            this.grdOrdenCompras.AccessibleDescription = "grdOrdenCompras, Sheet1, Row 0, Column 0, ";
            this.grdOrdenCompras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdOrdenCompras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenCompras.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdOrdenCompras.Location = new System.Drawing.Point(9, 19);
            this.grdOrdenCompras.Name = "grdOrdenCompras";
            this.grdOrdenCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenCompras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenCompras_Sheet1});
            this.grdOrdenCompras.Size = new System.Drawing.Size(944, 375);
            this.grdOrdenCompras.TabIndex = 0;
            this.grdOrdenCompras.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdOrdenCompras_CellDoubleClick);
            // 
            // grdOrdenCompras_Sheet1
            // 
            this.grdOrdenCompras_Sheet1.Reset();
            this.grdOrdenCompras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenCompras_Sheet1.ColumnCount = 6;
            this.grdOrdenCompras_Sheet1.RowCount = 12;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Proveedor";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Importe";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType13.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType13.MaxLength = 10;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).CellType = textCellType13;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Label = "Folio";
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Width = 80F;
            textCellType14.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType14.MaxLength = 100;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).CellType = textCellType14;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Label = "Proveedor";
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Width = 300F;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).CellType = textCellType15;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Label = "Fecha de Registro";
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Width = 70F;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).CellType = textCellType16;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Label = "Status";
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Width = 140F;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Label = "Fecha Colocación";
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = -10000000D;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).CellType = numberCellType4;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Label = "Importe";
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(5).Width = 120F;
            this.grdOrdenCompras_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.chkTodasLasFechas);
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(674, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(302, 74);
            this.FrameFechas.TabIndex = 3;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // chkTodasLasFechas
            // 
            this.chkTodasLasFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTodasLasFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodasLasFechas.Location = new System.Drawing.Point(181, 0);
            this.chkTodasLasFechas.Name = "chkTodasLasFechas";
            this.chkTodasLasFechas.Size = new System.Drawing.Size(107, 16);
            this.chkTodasLasFechas.TabIndex = 20;
            this.chkTodasLasFechas.Text = "Todas las fechas";
            this.chkTodasLasFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodasLasFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(196, 30);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(164, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(61, 30);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtIdPersonal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblNomPersonal);
            this.groupBox1.Controls.Add(this.txtProveedor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblNomProv);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 74);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Búsqueda";
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(88, 45);
            this.txtIdPersonal.MaxLength = 8;
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(80, 20);
            this.txtIdPersonal.TabIndex = 28;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPersonal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPersonal_KeyDown);
            this.txtIdPersonal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonal_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "Comprador :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomPersonal
            // 
            this.lblNomPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomPersonal.Location = new System.Drawing.Point(173, 45);
            this.lblNomPersonal.Name = "lblNomPersonal";
            this.lblNomPersonal.Size = new System.Drawing.Size(475, 20);
            this.lblNomPersonal.TabIndex = 29;
            this.lblNomPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProveedor
            // 
            this.txtProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtProveedor.Decimales = 2;
            this.txtProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtProveedor.Location = new System.Drawing.Point(88, 21);
            this.txtProveedor.MaxLength = 8;
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.PermitirApostrofo = false;
            this.txtProveedor.PermitirNegativos = false;
            this.txtProveedor.Size = new System.Drawing.Size(80, 20);
            this.txtProveedor.TabIndex = 0;
            this.txtProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Proveedor :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomProv
            // 
            this.lblNomProv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomProv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomProv.Location = new System.Drawing.Point(173, 21);
            this.lblNomProv.Name = "lblNomProv";
            this.lblNomProv.Size = new System.Drawing.Size(475, 20);
            this.lblNomProv.TabIndex = 26;
            this.lblNomProv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListadoOrdenesDeCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 511);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoOrdenesDeCompras";
            this.Text = "Listado de Órdenes de Compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).EndInit();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdOrdenCompras;
        private FarPoint.Win.Spread.SheetView grdOrdenCompras_Sheet1;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.CheckBox chkTodasLasFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNomPersonal;
        private SC_ControlsCS.scTextBoxExt txtProveedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblNomProv;
    }
}