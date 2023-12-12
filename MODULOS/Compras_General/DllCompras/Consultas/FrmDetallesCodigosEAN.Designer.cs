namespace DllCompras.Consultas
{
    partial class FrmDetallesCodigosEAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetallesCodigosEAN));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdOrdenCompras = new FarPoint.Win.Spread.FpSpread();
            this.grdOrdenCompras_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.lblContPaquete = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblIdClaveSSA = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblIdProveedor = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(803, 25);
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
            this.groupBox2.Controls.Add(this.grdOrdenCompras);
            this.groupBox2.Location = new System.Drawing.Point(10, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(782, 274);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado Folios Órdenes de Compra -- Códigos EAN";
            // 
            // grdOrdenCompras
            // 
            this.grdOrdenCompras.AccessibleDescription = "grdOrdenCompras, Sheet1, Row 0, Column 0, ";
            this.grdOrdenCompras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdOrdenCompras.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdOrdenCompras.Location = new System.Drawing.Point(7, 16);
            this.grdOrdenCompras.Name = "grdOrdenCompras";
            this.grdOrdenCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdOrdenCompras.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdOrdenCompras_Sheet1});
            this.grdOrdenCompras.Size = new System.Drawing.Size(766, 247);
            this.grdOrdenCompras.TabIndex = 0;
            this.grdOrdenCompras.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdOrdenCompras_CellDoubleClick);
            // 
            // grdOrdenCompras_Sheet1
            // 
            this.grdOrdenCompras_Sheet1.Reset();
            this.grdOrdenCompras_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdOrdenCompras_Sheet1.ColumnCount = 5;
            this.grdOrdenCompras_Sheet1.RowCount = 12;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 2).Locked = true;
            this.grdOrdenCompras_Sheet1.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Cells.Get(1, 3).Locked = false;
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Folio";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código EAN";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Cajas";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Importe";
            this.grdOrdenCompras_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 10;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Label = "Folio";
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(0).Width = 80F;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Label = "Código EAN";
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(1).Width = 100F;
            textCellType3.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType3.MaxLength = 100;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(2).Width = 310F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Label = "Cajas";
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(3).Width = 100F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Label = "Importe";
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Locked = true;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdOrdenCompras_Sheet1.Columns.Get(4).Width = 120F;
            this.grdOrdenCompras_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdOrdenCompras_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClaveSSA);
            this.groupBox1.Controls.Add(this.lblContPaquete);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblIdClaveSSA);
            this.groupBox1.Controls.Add(this.lblPresentacion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(10, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 104);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Clave";
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveSSA.Location = new System.Drawing.Point(92, 20);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(109, 20);
            this.lblClaveSSA.TabIndex = 30;
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblContPaquete
            // 
            this.lblContPaquete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContPaquete.Location = new System.Drawing.Point(707, 20);
            this.lblContPaquete.Name = "lblContPaquete";
            this.lblContPaquete.Size = new System.Drawing.Size(65, 20);
            this.lblContPaquete.TabIndex = 24;
            this.lblContPaquete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(640, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Contenido : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(207, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Clave Interna :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdClaveSSA
            // 
            this.lblIdClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdClaveSSA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdClaveSSA.Location = new System.Drawing.Point(288, 20);
            this.lblIdClaveSSA.Name = "lblIdClaveSSA";
            this.lblIdClaveSSA.Size = new System.Drawing.Size(85, 20);
            this.lblIdClaveSSA.TabIndex = 28;
            this.lblIdClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(470, 20);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(165, 20);
            this.lblPresentacion.TabIndex = 22;
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(390, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Presentación : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDescripcion.Location = new System.Drawing.Point(92, 44);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(680, 49);
            this.lblDescripcion.TabIndex = 26;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblIdProveedor);
            this.groupBox3.Controls.Add(this.lblProveedor);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(9, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(783, 56);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Proveedor";
            // 
            // lblIdProveedor
            // 
            this.lblIdProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdProveedor.Location = new System.Drawing.Point(95, 21);
            this.lblIdProveedor.Name = "lblIdProveedor";
            this.lblIdProveedor.Size = new System.Drawing.Size(68, 20);
            this.lblIdProveedor.TabIndex = 28;
            this.lblIdProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedor.Location = new System.Drawing.Point(169, 21);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(605, 20);
            this.lblProveedor.TabIndex = 22;
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(25, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Proveedor :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDetallesCodigosEAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 471);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDetallesCodigosEAN";
            this.Text = "Listado  Códigos EAN";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrdenCompras_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIdClaveSSA;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblContPaquete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblIdProveedor;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblClaveSSA;
    }
}