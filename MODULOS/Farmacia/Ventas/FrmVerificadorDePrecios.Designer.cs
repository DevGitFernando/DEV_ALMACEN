namespace Farmacia.Ventas
{
    partial class FrmVerificadorDePrecios
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType9 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType10 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType11 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType12 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVerificadorDePrecios));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grdExistencia = new FarPoint.Win.Spread.FpSpread();
            this.grdExistencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblExistenciaProductoAux = new System.Windows.Forms.Label();
            this.lblExistenciaProducto = new System.Windows.Forms.Label();
            this.lblPrecioPublicoAux = new System.Windows.Forms.Label();
            this.lblPrecioPublico = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcionClave = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCodEAN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDescripcionProducto = new System.Windows.Forms.Label();
            this.txtIdProducto = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.grdExistencia);
            this.groupBox2.Location = new System.Drawing.Point(9, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(967, 342);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Productos pertenecientes a la misma Clave SSA";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(725, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Existencia Total :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(834, 312);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(125, 23);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "label3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdExistencia
            // 
            this.grdExistencia.AccessibleDescription = "grdExistencia, Sheet1, Row 0, Column 0, ";
            this.grdExistencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdExistencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdExistencia.Location = new System.Drawing.Point(9, 19);
            this.grdExistencia.Name = "grdExistencia";
            this.grdExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdExistencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdExistencia_Sheet1});
            this.grdExistencia.Size = new System.Drawing.Size(952, 290);
            this.grdExistencia.TabIndex = 0;
            this.grdExistencia.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.grdExistencia_EnterCell);
            this.grdExistencia.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdExistencia_CellDoubleClick);
            // 
            // grdExistencia_Sheet1
            // 
            this.grdExistencia_Sheet1.Reset();
            this.grdExistencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdExistencia_Sheet1.ColumnCount = 9;
            this.grdExistencia_Sheet1.RowCount = 10;
            this.grdExistencia_Sheet1.Cells.Get(0, 1).Value = "12345678901234567890";
            this.grdExistencia_Sheet1.Cells.Get(0, 4).Value = 0;
            this.grdExistencia_Sheet1.Cells.Get(0, 5).Value = 100;
            this.grdExistencia_Sheet1.Cells.Get(0, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(0, 6).Value = 0D;
            this.grdExistencia_Sheet1.Cells.Get(0, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(0, 7).Value = 100D;
            this.grdExistencia_Sheet1.Cells.Get(1, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(1, 5).Value = 100;
            this.grdExistencia_Sheet1.Cells.Get(1, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(1, 6).Value = 14.999999999999986D;
            this.grdExistencia_Sheet1.Cells.Get(1, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(1, 7).Value = 114.99999999999999D;
            this.grdExistencia_Sheet1.Cells.Get(2, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(2, 5).Value = 100;
            this.grdExistencia_Sheet1.Cells.Get(2, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(2, 6).Value = 14.999999999999986D;
            this.grdExistencia_Sheet1.Cells.Get(2, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(2, 7).Value = 114.99999999999999D;
            this.grdExistencia_Sheet1.Cells.Get(3, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(3, 5).Value = 1000;
            this.grdExistencia_Sheet1.Cells.Get(3, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(3, 6).Value = 150D;
            this.grdExistencia_Sheet1.Cells.Get(3, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(3, 7).Value = 1150D;
            this.grdExistencia_Sheet1.Cells.Get(4, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(4, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(4, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(4, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(4, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(4, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.Cells.Get(5, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(5, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(5, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(5, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(5, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(5, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.Cells.Get(6, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(6, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(6, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(6, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(6, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(6, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.Cells.Get(7, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(7, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(7, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(7, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(7, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(7, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.Cells.Get(8, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(8, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(8, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(8, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(8, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(8, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.Cells.Get(9, 4).Value = 15;
            this.grdExistencia_Sheet1.Cells.Get(9, 5).Value = 200;
            this.grdExistencia_Sheet1.Cells.Get(9, 6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(9, 6).Value = 29.999999999999972D;
            this.grdExistencia_Sheet1.Cells.Get(9, 7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Cells.Get(9, 7).Value = 229.99999999999997D;
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdProducto";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Código EAN";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Presentación";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "PorcIva";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "PrecioNormal";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "ImporteIva";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Precio";
            this.grdExistencia_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Existencia";
            this.grdExistencia_Sheet1.ColumnHeader.Rows.Get(0).Height = 33F;
            this.grdExistencia_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdExistencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Label = "IdProducto";
            this.grdExistencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(0).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(0).Width = 75F;
            this.grdExistencia_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdExistencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Label = "Código EAN";
            this.grdExistencia_Sheet1.Columns.Get(1).Locked = false;
            this.grdExistencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(1).Width = 120F;
            this.grdExistencia_Sheet1.Columns.Get(2).CellType = textCellType9;
            this.grdExistencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdExistencia_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdExistencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(2).Width = 400F;
            this.grdExistencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Label = "Presentación";
            this.grdExistencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdExistencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(3).Width = 130F;
            numberCellType9.DecimalPlaces = 4;
            numberCellType9.MinimumValue = 0D;
            this.grdExistencia_Sheet1.Columns.Get(4).CellType = numberCellType9;
            this.grdExistencia_Sheet1.Columns.Get(4).Label = "PorcIva";
            this.grdExistencia_Sheet1.Columns.Get(4).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(4).Width = 90F;
            numberCellType10.DecimalPlaces = 4;
            numberCellType10.MinimumValue = 0D;
            this.grdExistencia_Sheet1.Columns.Get(5).CellType = numberCellType10;
            this.grdExistencia_Sheet1.Columns.Get(5).Label = "PrecioNormal";
            this.grdExistencia_Sheet1.Columns.Get(5).Visible = false;
            this.grdExistencia_Sheet1.Columns.Get(5).Width = 90F;
            numberCellType11.DecimalPlaces = 4;
            numberCellType11.MinimumValue = 0D;
            this.grdExistencia_Sheet1.Columns.Get(6).CellType = numberCellType11;
            this.grdExistencia_Sheet1.Columns.Get(6).Formula = "((1+(RC[-2]/100))*RC[-1])-RC[-1]";
            this.grdExistencia_Sheet1.Columns.Get(6).Label = "ImporteIva";
            this.grdExistencia_Sheet1.Columns.Get(6).Visible = false;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdExistencia_Sheet1.Columns.Get(7).CellType = currencyCellType3;
            this.grdExistencia_Sheet1.Columns.Get(7).Formula = "RC[-2]+RC[-1]";
            this.grdExistencia_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExistencia_Sheet1.Columns.Get(7).Label = "Precio";
            this.grdExistencia_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(7).Width = 110F;
            numberCellType12.DecimalPlaces = 0;
            numberCellType12.MaximumValue = 10000000D;
            numberCellType12.MinimumValue = 0D;
            this.grdExistencia_Sheet1.Columns.Get(8).CellType = numberCellType12;
            this.grdExistencia_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdExistencia_Sheet1.Columns.Get(8).Label = "Existencia";
            this.grdExistencia_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdExistencia_Sheet1.Columns.Get(8).Width = 80F;
            this.grdExistencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdExistencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblExistenciaProductoAux);
            this.groupBox1.Controls.Add(this.lblExistenciaProducto);
            this.groupBox1.Controls.Add(this.lblPrecioPublicoAux);
            this.groupBox1.Controls.Add(this.lblPrecioPublico);
            this.groupBox1.Controls.Add(this.txtClaveSSA);
            this.groupBox1.Controls.Add(this.lblDescripcionClave);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCodEAN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDescripcionProducto);
            this.groupBox1.Controls.Add(this.txtIdProducto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(967, 160);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Producto";
            // 
            // lblExistenciaProductoAux
            // 
            this.lblExistenciaProductoAux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExistenciaProductoAux.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistenciaProductoAux.Location = new System.Drawing.Point(730, 21);
            this.lblExistenciaProductoAux.Name = "lblExistenciaProductoAux";
            this.lblExistenciaProductoAux.Size = new System.Drawing.Size(114, 26);
            this.lblExistenciaProductoAux.TabIndex = 14;
            this.lblExistenciaProductoAux.Text = "Existencia :";
            this.lblExistenciaProductoAux.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblExistenciaProducto
            // 
            this.lblExistenciaProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExistenciaProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExistenciaProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistenciaProducto.Location = new System.Drawing.Point(850, 18);
            this.lblExistenciaProducto.Name = "lblExistenciaProducto";
            this.lblExistenciaProducto.Size = new System.Drawing.Size(109, 33);
            this.lblExistenciaProducto.TabIndex = 13;
            this.lblExistenciaProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrecioPublicoAux
            // 
            this.lblPrecioPublicoAux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecioPublicoAux.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioPublicoAux.Location = new System.Drawing.Point(494, 21);
            this.lblPrecioPublicoAux.Name = "lblPrecioPublicoAux";
            this.lblPrecioPublicoAux.Size = new System.Drawing.Size(74, 26);
            this.lblPrecioPublicoAux.TabIndex = 12;
            this.lblPrecioPublicoAux.Text = "Precio :";
            this.lblPrecioPublicoAux.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPrecioPublico
            // 
            this.lblPrecioPublico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrecioPublico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrecioPublico.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioPublico.Location = new System.Drawing.Point(574, 18);
            this.lblPrecioPublico.Name = "lblPrecioPublico";
            this.lblPrecioPublico.Size = new System.Drawing.Size(150, 33);
            this.lblPrecioPublico.TabIndex = 11;
            this.lblPrecioPublico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.Enabled = false;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(112, 41);
            this.txtClaveSSA.MaxLength = 8;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(154, 20);
            this.txtClaveSSA.TabIndex = 1;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            // 
            // lblDescripcionClave
            // 
            this.lblDescripcionClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClave.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionClave.Location = new System.Drawing.Point(112, 65);
            this.lblDescripcionClave.Name = "lblDescripcionClave";
            this.lblDescripcionClave.Size = new System.Drawing.Size(847, 60);
            this.lblDescripcionClave.TabIndex = 10;
            this.lblDescripcionClave.Text = "label2";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Clave SSA :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodEAN
            // 
            this.txtCodEAN.Location = new System.Drawing.Point(112, 15);
            this.txtCodEAN.MaxLength = 15;
            this.txtCodEAN.Name = "txtCodEAN";
            this.txtCodEAN.Size = new System.Drawing.Size(154, 20);
            this.txtCodEAN.TabIndex = 0;
            this.txtCodEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodEAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodEAN_KeyDown);
            this.txtCodEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodEAN_Validating);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Código EAN :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionProducto
            // 
            this.lblDescripcionProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionProducto.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionProducto.Location = new System.Drawing.Point(215, 130);
            this.lblDescripcionProducto.Name = "lblDescripcionProducto";
            this.lblDescripcionProducto.Size = new System.Drawing.Size(744, 20);
            this.lblDescripcionProducto.TabIndex = 5;
            this.lblDescripcionProducto.Text = "label2";
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProducto.Decimales = 2;
            this.txtIdProducto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProducto.ForeColor = System.Drawing.Color.Black;
            this.txtIdProducto.Location = new System.Drawing.Point(112, 130);
            this.txtIdProducto.MaxLength = 8;
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.PermitirApostrofo = false;
            this.txtIdProducto.PermitirNegativos = false;
            this.txtIdProducto.Size = new System.Drawing.Size(97, 20);
            this.txtIdProducto.TabIndex = 2;
            this.txtIdProducto.Text = "01234567";
            this.txtIdProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProducto_KeyDown);
            this.txtIdProducto.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProducto_Validating);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Código Interno :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
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
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 537);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(984, 24);
            this.label11.TabIndex = 14;
            this.label11.Text = " Doble click sobre Código EAN para ver Lotes";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmVerificadorDePrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmVerificadorDePrecios";
            this.Text = "Verificador de Precios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmVerificadorDePrecios_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExistencia_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private FarPoint.Win.Spread.FpSpread grdExistencia;
        private FarPoint.Win.Spread.SheetView grdExistencia_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescripcionProducto;
        private SC_ControlsCS.scTextBoxExt txtIdProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox txtCodEAN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDescripcionClave;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblPrecioPublico;
        private System.Windows.Forms.Label lblPrecioPublicoAux;
        private System.Windows.Forms.Label lblExistenciaProductoAux;
        private System.Windows.Forms.Label lblExistenciaProducto;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.Label label11;
    }
}