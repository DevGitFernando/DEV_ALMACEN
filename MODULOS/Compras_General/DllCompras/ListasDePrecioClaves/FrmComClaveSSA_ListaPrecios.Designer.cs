namespace DllCompras.ListasDePrecioClaves
{
    partial class FrmComClaveSSA_ListaPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmComClaveSSA_ListaPrecios));
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType7 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType8 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType9 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.grpDatosClave = new System.Windows.Forms.GroupBox();
            this.lblDescripcionClaveSSA = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.txtClaveInterna = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveInterna = new System.Windows.Forms.Label();
            this.grpListaPreciosClaves = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.grpDatosClave.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1084, 25);
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
            // grpDatosClave
            // 
            this.grpDatosClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosClave.Controls.Add(this.lblDescripcionClaveSSA);
            this.grpDatosClave.Controls.Add(this.lblClaveSSA);
            this.grpDatosClave.Controls.Add(this.txtClaveInterna);
            this.grpDatosClave.Controls.Add(this.lblClaveInterna);
            this.grpDatosClave.Location = new System.Drawing.Point(10, 28);
            this.grpDatosClave.Name = "grpDatosClave";
            this.grpDatosClave.Size = new System.Drawing.Size(1063, 106);
            this.grpDatosClave.TabIndex = 10;
            this.grpDatosClave.TabStop = false;
            this.grpDatosClave.Text = "Datos de Clave";
            // 
            // lblDescripcionClaveSSA
            // 
            this.lblDescripcionClaveSSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClaveSSA.Location = new System.Drawing.Point(94, 42);
            this.lblDescripcionClaveSSA.Name = "lblDescripcionClaveSSA";
            this.lblDescripcionClaveSSA.Size = new System.Drawing.Size(960, 54);
            this.lblDescripcionClaveSSA.TabIndex = 6;
            this.lblDescripcionClaveSSA.Text = "label2";
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(248, 19);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(204, 20);
            this.lblClaveSSA.TabIndex = 5;
            this.lblClaveSSA.Text = "label2";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClaveInterna
            // 
            this.txtClaveInterna.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveInterna.Decimales = 2;
            this.txtClaveInterna.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveInterna.ForeColor = System.Drawing.Color.Black;
            this.txtClaveInterna.Location = new System.Drawing.Point(94, 19);
            this.txtClaveInterna.MaxLength = 20;
            this.txtClaveInterna.Name = "txtClaveInterna";
            this.txtClaveInterna.PermitirApostrofo = false;
            this.txtClaveInterna.PermitirNegativos = false;
            this.txtClaveInterna.Size = new System.Drawing.Size(148, 20);
            this.txtClaveInterna.TabIndex = 3;
            this.txtClaveInterna.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveInterna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveInterna_KeyDown);
            this.txtClaveInterna.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveInterna_Validating);
            // 
            // lblClaveInterna
            // 
            this.lblClaveInterna.Location = new System.Drawing.Point(14, 21);
            this.lblClaveInterna.Name = "lblClaveInterna";
            this.lblClaveInterna.Size = new System.Drawing.Size(76, 13);
            this.lblClaveInterna.TabIndex = 4;
            this.lblClaveInterna.Text = "Clave SSA :";
            this.lblClaveInterna.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpListaPreciosClaves
            // 
            this.grpListaPreciosClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpListaPreciosClaves.Controls.Add(this.grdListaDePrecios);
            this.grpListaPreciosClaves.Location = new System.Drawing.Point(10, 136);
            this.grpListaPreciosClaves.Name = "grpListaPreciosClaves";
            this.grpListaPreciosClaves.Size = new System.Drawing.Size(1063, 417);
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
            this.grdListaDePrecios.Size = new System.Drawing.Size(1044, 390);
            this.grdListaDePrecios.TabIndex = 0;
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 10;
            this.grdListaDePrecios_Sheet1.RowCount = 12;
            this.grdListaDePrecios_Sheet1.Cells.Get(0, 2).Value = "ACTIVO";
            this.grdListaDePrecios_Sheet1.Cells.Get(1, 2).Value = "CANCELADO";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave Proveedor";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre Proveedor";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "% Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "TasaIva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "Clave Proveedor";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Nombre Proveedor";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 240F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Status";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 73F;
            currencyCellType7.DecimalPlaces = 4;
            currencyCellType7.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType7.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).CellType = currencyCellType7;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 70F;
            numberCellType5.DecimalPlaces = 4;
            numberCellType5.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = numberCellType5;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "% Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 81F;
            numberCellType6.DecimalPlaces = 4;
            numberCellType6.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = numberCellType6;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "TasaIva";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            currencyCellType8.DecimalPlaces = 4;
            currencyCellType8.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType8.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = currencyCellType8;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Width = 70F;
            currencyCellType9.DecimalPlaces = 4;
            currencyCellType9.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType9.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).CellType = currencyCellType9;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Fecha registro";
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
            // FrmComClaveSSA_ListaPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.grpListaPreciosClaves);
            this.Controls.Add(this.grpDatosClave);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmComClaveSSA_ListaPrecios";
            this.Text = "Listado de Proveedores por Clave";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_ListaPreciosClaveSSA_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpDatosClave.ResumeLayout(false);
            this.grpDatosClave.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpDatosClave;
        private System.Windows.Forms.Label lblDescripcionClaveSSA;
        private System.Windows.Forms.Label lblClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtClaveInterna;
        private System.Windows.Forms.Label lblClaveInterna;
        private System.Windows.Forms.GroupBox grpListaPreciosClaves;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
    }
}