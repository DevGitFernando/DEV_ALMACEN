namespace Almacen.OrdenCompraConsigna
{
    partial class FrmTEST
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
            FarPoint.Win.Spread.NamedStyle namedStyle3 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.NamedStyle namedStyle4 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer2 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType12 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType13 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType14 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.grdLotes = new FarPoint.Win.Spread.FpSpread();
            this.grdLotes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLotes
            // 
            this.grdLotes.AccessibleDescription = "grdLotes, Sheet1, Row 0, Column 0, 01";
            this.grdLotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdLotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdLotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes.Location = new System.Drawing.Point(12, 52);
            this.grdLotes.Name = "grdLotes";
            namedStyle3.BackColor = System.Drawing.Color.DarkGray;
            namedStyle3.CellType = generalCellType2;
            namedStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle3.Renderer = generalCellType2;
            namedStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(196)))), ((int)(((byte)(233)))));
            namedStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle4.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle4.Renderer = enhancedCornerRenderer2;
            namedStyle4.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle3,
            namedStyle4});
            this.grdLotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdLotes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdLotes_Sheet1});
            this.grdLotes.Size = new System.Drawing.Size(1345, 250);
            this.grdLotes.TabIndex = 1;
            this.grdLotes.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdLotes_CellClick);
            // 
            // grdLotes_Sheet1
            // 
            this.grdLotes_Sheet1.Reset();
            this.grdLotes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdLotes_Sheet1.ColumnCount = 20;
            this.grdLotes_Sheet1.RowCount = 5;
            this.grdLotes_Sheet1.Cells.Get(0, 0).Value = "01";
            this.grdLotes_Sheet1.Cells.Get(0, 8).Value = "2010-01";
            this.grdLotes_Sheet1.Cells.Get(0, 12).Value = 100D;
            this.grdLotes_Sheet1.Cells.Get(0, 14).Formula = "RC[-6]&\"-\"&RC[-14]";
            this.grdLotes_Sheet1.Cells.Get(0, 14).Value = "2010-01-01";
            this.grdLotes_Sheet1.Cells.Get(0, 17).Value = 50D;
            this.grdLotes_Sheet1.Cells.Get(0, 18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.Cells.Get(0, 18).Value = 0D;
            this.grdLotes_Sheet1.Cells.Get(1, 8).Value = "2010-01";
            this.grdLotes_Sheet1.Cells.Get(1, 14).Formula = "RC[-6]&RC[-14]";
            this.grdLotes_Sheet1.Cells.Get(1, 14).Value = "2010-01";
            this.grdLotes_Sheet1.Cells.Get(1, 18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.Cells.Get(2, 14).Formula = "RC[-6]&RC[-14]";
            this.grdLotes_Sheet1.Cells.Get(2, 14).Value = "";
            this.grdLotes_Sheet1.Cells.Get(2, 18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.Cells.Get(3, 14).Formula = "RC[-6]&RC[-14]";
            this.grdLotes_Sheet1.Cells.Get(3, 14).Value = "";
            this.grdLotes_Sheet1.Cells.Get(3, 18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.Cells.Get(4, 14).Formula = "RC[-6]&RC[-14]";
            this.grdLotes_Sheet1.Cells.Get(4, 14).Value = "";
            this.grdLotes_Sheet1.Cells.Get(4, 18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Fuente";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fuente Financ.";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Codigo EAN";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "SKU";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Clave de lote";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Meses por Caducar";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha de entrada";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Fecha de caducidad";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Status";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Existencia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Disponible devolución";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "Cantidad";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "AddColumna";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = "Ordenamiento";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = "DispensaciónBloqueada";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = "Es caducado";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = "Contenido paquete";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 18).Value = "Residuo";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 19).Value = "FLO";
            this.grdLotes_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdLotes_Sheet1.Columns.Get(0).CellType = textCellType12;
            this.grdLotes_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Label = "Id Fuente";
            this.grdLotes_Sheet1.Columns.Get(0).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Width = 65F;
            this.grdLotes_Sheet1.Columns.Get(1).CellType = textCellType13;
            this.grdLotes_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(1).Label = "Fuente Financ.";
            this.grdLotes_Sheet1.Columns.Get(1).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Width = 130F;
            this.grdLotes_Sheet1.Columns.Get(2).CellType = textCellType14;
            this.grdLotes_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Label = "Código";
            this.grdLotes_Sheet1.Columns.Get(2).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(2).Width = 80F;
            textCellType15.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            this.grdLotes_Sheet1.Columns.Get(3).CellType = textCellType15;
            this.grdLotes_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Label = "Codigo EAN";
            this.grdLotes_Sheet1.Columns.Get(3).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(3).Width = 122F;
            this.grdLotes_Sheet1.Columns.Get(4).CellType = textCellType16;
            this.grdLotes_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Label = "SKU";
            this.grdLotes_Sheet1.Columns.Get(4).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Width = 240F;
            this.grdLotes_Sheet1.Columns.Get(5).CellType = textCellType17;
            this.grdLotes_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(5).Label = "Clave de lote";
            this.grdLotes_Sheet1.Columns.Get(5).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Width = 110F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = -10000000D;
            this.grdLotes_Sheet1.Columns.Get(6).CellType = numberCellType8;
            this.grdLotes_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Label = "Meses por Caducar";
            this.grdLotes_Sheet1.Columns.Get(6).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Width = 75F;
            this.grdLotes_Sheet1.Columns.Get(7).CellType = textCellType18;
            this.grdLotes_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Label = "Fecha de entrada";
            this.grdLotes_Sheet1.Columns.Get(7).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Width = 70F;
            textCellType19.MaxLength = 7;
            this.grdLotes_Sheet1.Columns.Get(8).CellType = textCellType19;
            this.grdLotes_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Label = "Fecha de caducidad";
            this.grdLotes_Sheet1.Columns.Get(8).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Width = 70F;
            this.grdLotes_Sheet1.Columns.Get(9).CellType = textCellType20;
            this.grdLotes_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(9).Label = "Status";
            this.grdLotes_Sheet1.Columns.Get(9).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType9.DecimalPlaces = 0;
            numberCellType9.DecimalSeparator = ".";
            numberCellType9.MaximumValue = 10000000D;
            numberCellType9.MinimumValue = 0D;
            numberCellType9.Separator = ",";
            numberCellType9.ShowSeparator = true;
            this.grdLotes_Sheet1.Columns.Get(10).CellType = numberCellType9;
            this.grdLotes_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Label = "Existencia";
            this.grdLotes_Sheet1.Columns.Get(10).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Width = 70F;
            numberCellType10.DecimalPlaces = 0;
            numberCellType10.DecimalSeparator = ".";
            numberCellType10.MaximumValue = 10000000D;
            numberCellType10.MinimumValue = 0D;
            numberCellType10.Separator = ",";
            numberCellType10.ShowSeparator = true;
            this.grdLotes_Sheet1.Columns.Get(11).CellType = numberCellType10;
            this.grdLotes_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(11).Label = "Disponible devolución";
            this.grdLotes_Sheet1.Columns.Get(11).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(11).Width = 75F;
            numberCellType11.DecimalPlaces = 0;
            numberCellType11.MaximumValue = 10000000D;
            numberCellType11.MinimumValue = 0D;
            numberCellType11.Separator = ",";
            numberCellType11.ShowSeparator = true;
            this.grdLotes_Sheet1.Columns.Get(12).CellType = numberCellType11;
            this.grdLotes_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.grdLotes_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(12).Label = "Cantidad";
            this.grdLotes_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(12).Width = 70F;
            numberCellType12.DecimalPlaces = 0;
            numberCellType12.MaximumValue = 1D;
            numberCellType12.MinimumValue = 0D;
            this.grdLotes_Sheet1.Columns.Get(13).CellType = numberCellType12;
            this.grdLotes_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(13).Label = "AddColumna";
            this.grdLotes_Sheet1.Columns.Get(13).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(13).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(13).Width = 86F;
            this.grdLotes_Sheet1.Columns.Get(14).CellType = textCellType21;
            this.grdLotes_Sheet1.Columns.Get(14).Formula = "RC[-6]&RC[-14]";
            this.grdLotes_Sheet1.Columns.Get(14).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(14).Label = "Ordenamiento";
            this.grdLotes_Sheet1.Columns.Get(14).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(14).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(14).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(14).Width = 131F;
            this.grdLotes_Sheet1.Columns.Get(15).CellType = checkBoxCellType3;
            this.grdLotes_Sheet1.Columns.Get(15).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(15).Label = "DispensaciónBloqueada";
            this.grdLotes_Sheet1.Columns.Get(15).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(15).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(15).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(16).CellType = checkBoxCellType4;
            this.grdLotes_Sheet1.Columns.Get(16).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(16).Label = "Es caducado";
            this.grdLotes_Sheet1.Columns.Get(16).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(16).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(16).Visible = false;
            numberCellType13.DecimalPlaces = 0;
            numberCellType13.MaximumValue = 10000000D;
            numberCellType13.MinimumValue = 0D;
            this.grdLotes_Sheet1.Columns.Get(17).CellType = numberCellType13;
            this.grdLotes_Sheet1.Columns.Get(17).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(17).Label = "Contenido paquete";
            this.grdLotes_Sheet1.Columns.Get(17).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(17).Visible = false;
            numberCellType14.DecimalPlaces = 0;
            numberCellType14.MaximumValue = 10000000D;
            numberCellType14.MinimumValue = 0D;
            this.grdLotes_Sheet1.Columns.Get(18).CellType = numberCellType14;
            this.grdLotes_Sheet1.Columns.Get(18).Formula = "MOD(RC[-6],RC[-1])";
            this.grdLotes_Sheet1.Columns.Get(18).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(18).Label = "Residuo";
            this.grdLotes_Sheet1.Columns.Get(18).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(18).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(19).CellType = textCellType22;
            this.grdLotes_Sheet1.Columns.Get(19).Label = "FLO";
            this.grdLotes_Sheet1.Columns.Get(19).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(19).Width = 240F;
            this.grdLotes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmTEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 450);
            this.Controls.Add(this.grdLotes);
            this.Name = "FrmTEST";
            this.Text = "FrmTEST";
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread grdLotes;
        private FarPoint.Win.Spread.SheetView grdLotes_Sheet1;
    }
}