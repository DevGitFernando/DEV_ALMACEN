namespace Dll_SII_IMediaccess.Ventas
{
    partial class FrmPagoCaja_MA
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblSubTotalIva0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalPago = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCambio = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdPagos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblMontoRestante = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPagos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.lblIva);
            this.groupBox1.Controls.Add(this.lblSubTotal);
            this.groupBox1.Controls.Add(this.lblSubTotalIva0);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Venta";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotal.Location = new System.Drawing.Point(252, 78);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(160, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(252, 51);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(160, 23);
            this.lblIva.TabIndex = 2;
            this.lblIva.Text = "Iva :";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(252, 23);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(160, 23);
            this.lblSubTotal.TabIndex = 1;
            this.lblSubTotal.Text = "Sub-Total :";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalIva0
            // 
            this.lblSubTotalIva0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalIva0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalIva0.Location = new System.Drawing.Point(151, 117);
            this.lblSubTotalIva0.Name = "lblSubTotalIva0";
            this.lblSubTotalIva0.Size = new System.Drawing.Size(127, 23);
            this.lblSubTotalIva0.TabIndex = 0;
            this.lblSubTotalIva0.Text = "Sub-Total Iva 0 :";
            this.lblSubTotalIva0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSubTotalIva0.Visible = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(138, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Iva :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(138, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sub-Total :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sub-Total Iva 0 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMontoRestante);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblTotalPago);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblCambio);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(452, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 117);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información de Pago";
            // 
            // lblTotalPago
            // 
            this.lblTotalPago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPago.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotalPago.Location = new System.Drawing.Point(255, 21);
            this.lblTotalPago.Name = "lblTotalPago";
            this.lblTotalPago.Size = new System.Drawing.Size(160, 25);
            this.lblTotalPago.TabIndex = 4;
            this.lblTotalPago.Text = "Total :";
            this.lblTotalPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(158, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 23);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total pago :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCambio
            // 
            this.lblCambio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.ForeColor = System.Drawing.Color.DarkRed;
            this.lblCambio.Location = new System.Drawing.Point(255, 49);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(160, 25);
            this.lblCambio.TabIndex = 5;
            this.lblCambio.Text = "Total :";
            this.lblCambio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(158, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "Cambio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(0, 341);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(897, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = " <F5> Pagar                                                                      " +
    "                                                                                " +
    "                          <F12> Salir";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdPagos);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox3.Location = new System.Drawing.Point(11, 123);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(875, 212);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detalles de Pago";
            // 
            // grdPagos
            // 
            this.grdPagos.AccessibleDescription = "grdPagos, Sheet1, Row 0, Column 0, ";
            this.grdPagos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPagos.Location = new System.Drawing.Point(10, 22);
            this.grdPagos.Name = "grdPagos";
            this.grdPagos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPagos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdPagos.Size = new System.Drawing.Size(856, 181);
            this.grdPagos.TabIndex = 0;
            this.grdPagos.EditModeOff += new System.EventHandler(this.grdPagos_EditModeOff);
            this.grdPagos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdPagos_Advance);
            this.grdPagos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdPagos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 7;
            this.grdProductos_Sheet1.RowCount = 6;
            this.grdProductos_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 0).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 2).Value = new decimal(new int[] {
            900,
            0,
            0,
            131072});
            this.grdProductos_Sheet1.Cells.Get(0, 3).Value = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.grdProductos_Sheet1.Cells.Get(0, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(0, 4).Value = 41D;
            this.grdProductos_Sheet1.Cells.Get(0, 5).Value = "12345678901234567879";
            this.grdProductos_Sheet1.Cells.Get(1, 1).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(1, 4).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(2, 4).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(3, 4).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(4, 4).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Cells.Get(5, 4).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Tipo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Pago con";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cambio";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Referencia";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Permite duplicidad";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Tipo";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 70F;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 180F;
            currencyCellType1.DecimalPlaces = 2;
            currencyCellType1.DecimalSeparator = ".";
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType1.Separator = ",";
            currencyCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = currencyCellType1;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(2).Width = 120F;
            currencyCellType2.CurrencySymbol = "$";
            currencyCellType2.DecimalPlaces = 2;
            currencyCellType2.DecimalSeparator = ".";
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType2.Separator = ",";
            currencyCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = currencyCellType2;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Pago con";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 110F;
            currencyCellType3.DecimalPlaces = 2;
            currencyCellType3.DecimalSeparator = ".";
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            currencyCellType3.Separator = ",";
            currencyCellType3.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = currencyCellType3;
            this.grdProductos_Sheet1.Columns.Get(4).Formula = "IF(RC[-1]-RC[-2]>0,RC[-1]-RC[-2],0)";
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cambio";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 110F;
            textCellType3.MaxLength = 20;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Referencia";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 210F;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = checkBoxCellType1;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Permite duplicidad";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 100F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblMontoRestante
            // 
            this.lblMontoRestante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMontoRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoRestante.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMontoRestante.Location = new System.Drawing.Point(255, 77);
            this.lblMontoRestante.Name = "lblMontoRestante";
            this.lblMontoRestante.Size = new System.Drawing.Size(160, 25);
            this.lblMontoRestante.TabIndex = 13;
            this.lblMontoRestante.Text = "Total :";
            this.lblMontoRestante.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(158, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 23);
            this.label8.TabIndex = 12;
            this.label8.Text = "Restante :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPagoCaja_MA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 365);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPagoCaja_MA";
            this.Text = "Capturar pago de Venta";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPagoCaja_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPagoCaja_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPagos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblSubTotalIva0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalPago;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdPagos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.Label lblMontoRestante;
        private System.Windows.Forms.Label label8;
    }
}