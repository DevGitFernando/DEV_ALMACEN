namespace DllCompras.ListasDePrecioClaves
{
    partial class FrmComClaveSSA_ProveedoresListaPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComClaveSSA_ProveedoresListaPrecios));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.grpDatosProveedor = new System.Windows.Forms.GroupBox();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.txtIdProveedor = new SC_ControlsCS.scTextBoxExt();
            this.lblIdProveedor = new System.Windows.Forms.Label();
            this.grpListaPreciosClaves = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.grpDatosProveedor.SuspendLayout();
            this.grpListaPreciosClaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1134, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // grpDatosProveedor
            // 
            this.grpDatosProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosProveedor.Controls.Add(this.lblNombreProveedor);
            this.grpDatosProveedor.Controls.Add(this.txtIdProveedor);
            this.grpDatosProveedor.Controls.Add(this.lblIdProveedor);
            this.grpDatosProveedor.Location = new System.Drawing.Point(8, 26);
            this.grpDatosProveedor.Name = "grpDatosProveedor";
            this.grpDatosProveedor.Size = new System.Drawing.Size(1118, 49);
            this.grpDatosProveedor.TabIndex = 10;
            this.grpDatosProveedor.TabStop = false;
            this.grpDatosProveedor.Text = "Datos de Proveedor";
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreProveedor.Location = new System.Drawing.Point(208, 20);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(903, 20);
            this.lblNombreProveedor.TabIndex = 5;
            this.lblNombreProveedor.Text = "label2";
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProveedor.Decimales = 2;
            this.txtIdProveedor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProveedor.ForeColor = System.Drawing.Color.Black;
            this.txtIdProveedor.Location = new System.Drawing.Point(114, 20);
            this.txtIdProveedor.MaxLength = 4;
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.PermitirApostrofo = false;
            this.txtIdProveedor.PermitirNegativos = false;
            this.txtIdProveedor.Size = new System.Drawing.Size(88, 20);
            this.txtIdProveedor.TabIndex = 3;
            this.txtIdProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProveedor_KeyDown);
            this.txtIdProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProveedor_Validating);
            // 
            // lblIdProveedor
            // 
            this.lblIdProveedor.Location = new System.Drawing.Point(14, 21);
            this.lblIdProveedor.Name = "lblIdProveedor";
            this.lblIdProveedor.Size = new System.Drawing.Size(97, 13);
            this.lblIdProveedor.TabIndex = 4;
            this.lblIdProveedor.Text = "Clave Proveedor :";
            this.lblIdProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpListaPreciosClaves
            // 
            this.grpListaPreciosClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListaPreciosClaves.Controls.Add(this.grdListaDePrecios);
            this.grpListaPreciosClaves.Location = new System.Drawing.Point(8, 75);
            this.grpListaPreciosClaves.Name = "grpListaPreciosClaves";
            this.grpListaPreciosClaves.Size = new System.Drawing.Size(1118, 480);
            this.grpListaPreciosClaves.TabIndex = 11;
            this.grpListaPreciosClaves.TabStop = false;
            this.grpListaPreciosClaves.Text = "Lista de Precios de Claves";
            // 
            // grdListaDePrecios
            // 
            this.grdListaDePrecios.AccessibleDescription = "grdListaDePrecios, Sheet1, Row 0, Column 0, ";
            this.grdListaDePrecios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdListaDePrecios.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdListaDePrecios.Location = new System.Drawing.Point(9, 19);
            this.grdListaDePrecios.Name = "grdListaDePrecios";
            this.grdListaDePrecios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdListaDePrecios.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdListaDePrecios_Sheet1});
            this.grdListaDePrecios.Size = new System.Drawing.Size(1102, 452);
            this.grdListaDePrecios.TabIndex = 0;
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 10;
            this.grdListaDePrecios_Sheet1.RowCount = 14;
            this.grdListaDePrecios_Sheet1.Cells.Get(0, 2).Value = "ACTIVO";
            this.grdListaDePrecios_Sheet1.Cells.Get(1, 2).Value = "CANCELADO";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "% Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "TasaIva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha Registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "Clave";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Width = 110F;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 270F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Status";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 73F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).CellType = currencyCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 70F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "% Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 81F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "TasaIva";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = currencyCellType2;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Width = 70F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType3.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).CellType = currencyCellType3;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Fecha Registro";
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Width = 90F;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Label = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Width = 90F;
            this.grdListaDePrecios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmComClaveSSA_ProveedoresListaPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 561);
            this.Controls.Add(this.grpListaPreciosClaves);
            this.Controls.Add(this.grpDatosProveedor);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmComClaveSSA_ProveedoresListaPrecios";
            this.Text = "Lista de Claves por Proveedor";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmComClaveSSA_ProveedoresListaPrecios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpDatosProveedor.ResumeLayout(false);
            this.grpDatosProveedor.PerformLayout();
            this.grpListaPreciosClaves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdListaDePrecios_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox grpDatosProveedor;
        private System.Windows.Forms.Label lblNombreProveedor;
        private SC_ControlsCS.scTextBoxExt txtIdProveedor;
        private System.Windows.Forms.Label lblIdProveedor;
        private System.Windows.Forms.GroupBox grpListaPreciosClaves;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
    }
}