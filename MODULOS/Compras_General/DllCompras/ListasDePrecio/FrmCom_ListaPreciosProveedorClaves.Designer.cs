﻿namespace DllCompras.ListasDePrecio
{
    partial class FrmCom_ListaPreciosProveedorClaves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCom_ListaPreciosProveedorClaves));
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType13 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType14 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType15 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdListaDePrecios = new FarPoint.Win.Spread.FpSpread();
            this.grdListaDePrecios_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.toolStripBarraMenu.TabIndex = 5;
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
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblProveedor);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1065, 49);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Proveedor";
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(208, 20);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(850, 20);
            this.lblProveedor.TabIndex = 5;
            this.lblProveedor.Text = "label2";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(114, 20);
            this.txtId.MaxLength = 4;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(88, 20);
            this.txtId.TabIndex = 3;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdListaDePrecios);
            this.groupBox2.Location = new System.Drawing.Point(9, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1065, 475);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Precios de Claves";
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
            this.grdListaDePrecios.Size = new System.Drawing.Size(1049, 447);
            this.grdListaDePrecios.TabIndex = 0;
            // 
            // grdListaDePrecios_Sheet1
            // 
            this.grdListaDePrecios_Sheet1.Reset();
            this.grdListaDePrecios_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdListaDePrecios_Sheet1.ColumnCount = 11;
            this.grdListaDePrecios_Sheet1.RowCount = 14;
            this.grdListaDePrecios_Sheet1.Cells.Get(0, 3).Value = "ACTIVO";
            this.grdListaDePrecios_Sheet1.Cells.Get(1, 3).Value = "CANCELADO";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "P.Unitario";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "% Descuento";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "TasaIva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Iva";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Importe";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha registro";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.ColumnHeader.Rows.Get(0).Height = 37F;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).CellType = textCellType13;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Label = "Clave";
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(0).Width = 110F;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).CellType = textCellType14;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(1).Width = 270F;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).CellType = textCellType15;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(2).Width = 104F;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Label = "Status";
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(3).Width = 73F;
            currencyCellType13.DecimalPlaces = 4;
            currencyCellType13.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType13.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).CellType = currencyCellType13;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Label = "P.Unitario";
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(4).Width = 70F;
            numberCellType9.DecimalPlaces = 4;
            numberCellType9.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).CellType = numberCellType9;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Label = "% Descuento";
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(5).Width = 81F;
            numberCellType10.DecimalPlaces = 4;
            numberCellType10.MinimumValue = 0D;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).CellType = numberCellType10;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Label = "TasaIva";
            this.grdListaDePrecios_Sheet1.Columns.Get(6).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            currencyCellType14.DecimalPlaces = 4;
            currencyCellType14.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType14.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).CellType = currencyCellType14;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Label = "Iva";
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(7).Width = 70F;
            currencyCellType15.DecimalPlaces = 4;
            currencyCellType15.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType15.ShowSeparator = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).CellType = currencyCellType15;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Label = "Importe";
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(8).Width = 70F;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Label = "Fecha registro";
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(9).Width = 90F;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Label = "Fecha Vigencia";
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Locked = true;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdListaDePrecios_Sheet1.Columns.Get(10).Width = 90F;
            this.grdListaDePrecios_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdListaDePrecios_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmCom_ListaPreciosProveedorClaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCom_ListaPreciosProveedorClaves";
            this.Text = "Lista de Precios por Proveedor por Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCom_ListaPreciosProveedorClaves_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblProveedor;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdListaDePrecios;
        private FarPoint.Win.Spread.SheetView grdListaDePrecios_Sheet1;
    }
}