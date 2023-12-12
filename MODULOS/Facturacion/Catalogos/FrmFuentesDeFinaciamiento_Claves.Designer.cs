namespace Facturacion.Catalogos
{
    partial class FrmFuentesDeFinaciamiento_Claves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFuentesDeFinaciamiento_Claves));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameTotales = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new SC_ControlsCS.scLabelExt();
            this.lblFinanciamiento = new SC_ControlsCS.scLabelExt();
            this.FrameValidarPoliza = new System.Windows.Forms.GroupBox();
            this.nmHasta = new System.Windows.Forms.NumericUpDown();
            this.nmDesde = new System.Windows.Forms.NumericUpDown();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.chkValidarReferencia = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.FrameTotales.SuspendLayout();
            this.FrameValidarPoliza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmHasta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDesde)).BeginInit();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            this.btnNuevo.Text = "Nuevo";
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
            this.btnGuardar.Text = "Guardar";
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
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameClaves
            // 
            this.FrameClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameClaves.Controls.Add(this.grdClaves);
            this.FrameClaves.Location = new System.Drawing.Point(9, 78);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(1167, 532);
            this.FrameClaves.TabIndex = 3;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Claves Asignadas";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(10, 16);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(1146, 506);
            this.grdClaves.TabIndex = 0;
            this.grdClaves.EditModeOn += new System.EventHandler(this.grdClaves_EditModeOn);
            this.grdClaves.EditModeOff += new System.EventHandler(this.grdClaves_EditModeOff);
            this.grdClaves.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdClaves_Advance);
            this.grdClaves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdClaves_KeyDown);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 9;
            this.grdClaves_Sheet1.RowCount = 20;
            this.grdClaves_Sheet1.Cells.Get(0, 3).Value = 100D;
            this.grdClaves_Sheet1.Cells.Get(0, 4).Value = 550D;
            this.grdClaves_Sheet1.Cells.Get(0, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(0, 5).Value = 5.5D;
            this.grdClaves_Sheet1.Cells.Get(0, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(0, 6).Value = 600D;
            this.grdClaves_Sheet1.Cells.Get(1, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(1, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(1, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(1, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(2, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(2, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(2, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(2, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(3, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(3, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(3, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(3, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(4, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(4, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(4, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(4, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(5, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(5, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(5, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(5, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(6, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(6, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(6, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(6, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(7, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(7, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(7, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(7, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(8, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(8, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(8, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(8, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(9, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(9, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(9, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(9, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(10, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(10, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(10, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(10, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(11, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(11, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(11, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(11, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(12, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(12, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(12, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(12, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(13, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(13, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(13, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(13, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(14, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(14, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(14, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(14, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(15, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(15, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(15, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(15, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(16, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(16, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(16, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(16, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(17, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(17, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(17, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(17, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(18, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(18, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(18, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(18, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(19, 5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Cells.Get(19, 5).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.Cells.Get(19, 6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Cells.Get(19, 6).Value = FarPoint.CalcEngine.CalcError.DivideByZero;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdClaveSSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Contenido Paquete";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad presupuestada";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cajas / Empaques";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Piezas";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Activar";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Guardado";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = false;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 140F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "IdClaveSSA";
            this.grdClaves_Sheet1.Columns.Get(1).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 80F;
            textCellType3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 500F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Contenido Paquete";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 100F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 1000000000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Cantidad presupuestada";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = false;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Width = 110F;
            numberCellType3.DecimalPlaces = 2;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 1000000000000D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdClaves_Sheet1.Columns.Get(5).Formula = "IF((RC[-1]/RC[-2])<=0.5,1,(RC[-1]/RC[-2]))";
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Cajas / Empaques";
            this.grdClaves_Sheet1.Columns.Get(5).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 90F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.DecimalSeparator = ".";
            numberCellType4.MaximumValue = 1000000000000D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdClaves_Sheet1.Columns.Get(6).Formula = "ROUND((RC[-1]),0)*RC[-3]";
            this.grdClaves_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(6).Label = "Piezas";
            this.grdClaves_Sheet1.Columns.Get(6).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(6).Width = 90F;
            this.grdClaves_Sheet1.Columns.Get(7).CellType = checkBoxCellType1;
            this.grdClaves_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(7).Label = "Activar";
            this.grdClaves_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(7).Width = 80F;
            this.grdClaves_Sheet1.Columns.Get(8).CellType = checkBoxCellType2;
            this.grdClaves_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(8).Label = "Guardado";
            this.grdClaves_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(8).Visible = false;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.Rows.Default.Height = 25F;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameTotales
            // 
            this.FrameTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTotales.Controls.Add(this.lblDescripcion);
            this.FrameTotales.Controls.Add(this.lblFinanciamiento);
            this.FrameTotales.Location = new System.Drawing.Point(9, 25);
            this.FrameTotales.Name = "FrameTotales";
            this.FrameTotales.Size = new System.Drawing.Size(568, 51);
            this.FrameTotales.TabIndex = 1;
            this.FrameTotales.TabStop = false;
            this.FrameTotales.Text = "Financiamiento";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(91, 18);
            this.lblDescripcion.MostrarToolTip = false;
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(464, 21);
            this.lblDescripcion.TabIndex = 8;
            this.lblDescripcion.Text = "Descripcion";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinanciamiento
            // 
            this.lblFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinanciamiento.Location = new System.Drawing.Point(18, 18);
            this.lblFinanciamiento.MostrarToolTip = false;
            this.lblFinanciamiento.Name = "lblFinanciamiento";
            this.lblFinanciamiento.Size = new System.Drawing.Size(67, 21);
            this.lblFinanciamiento.TabIndex = 0;
            this.lblFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameValidarPoliza
            // 
            this.FrameValidarPoliza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameValidarPoliza.Controls.Add(this.nmHasta);
            this.FrameValidarPoliza.Controls.Add(this.nmDesde);
            this.FrameValidarPoliza.Controls.Add(this.scLabelExt2);
            this.FrameValidarPoliza.Controls.Add(this.scLabelExt1);
            this.FrameValidarPoliza.Controls.Add(this.chkValidarReferencia);
            this.FrameValidarPoliza.Location = new System.Drawing.Point(583, 25);
            this.FrameValidarPoliza.Name = "FrameValidarPoliza";
            this.FrameValidarPoliza.Size = new System.Drawing.Size(592, 51);
            this.FrameValidarPoliza.TabIndex = 2;
            this.FrameValidarPoliza.TabStop = false;
            this.FrameValidarPoliza.Text = "Validar Referencias";
            // 
            // nmHasta
            // 
            this.nmHasta.Location = new System.Drawing.Point(527, 19);
            this.nmHasta.Name = "nmHasta";
            this.nmHasta.Size = new System.Drawing.Size(53, 20);
            this.nmHasta.TabIndex = 2;
            this.nmHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmDesde
            // 
            this.nmDesde.Location = new System.Drawing.Point(442, 19);
            this.nmDesde.Name = "nmDesde";
            this.nmDesde.Size = new System.Drawing.Size(53, 20);
            this.nmDesde.TabIndex = 1;
            this.nmDesde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(501, 19);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(20, 18);
            this.scLabelExt2.TabIndex = 2;
            this.scLabelExt2.Text = "y";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(272, 19);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(165, 18);
            this.scLabelExt1.TabIndex = 1;
            this.scLabelExt1.Text = "Longitud de Referencia entre ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkValidarReferencia
            // 
            this.chkValidarReferencia.Location = new System.Drawing.Point(14, 20);
            this.chkValidarReferencia.Name = "chkValidarReferencia";
            this.chkValidarReferencia.Size = new System.Drawing.Size(251, 19);
            this.chkValidarReferencia.TabIndex = 0;
            this.chkValidarReferencia.Text = "Validar Longitud de Referencia de Beneficiario";
            this.chkValidarReferencia.UseVisualStyleBackColor = true;
            // 
            // FrmFuentesDeFinaciamiento_Claves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 621);
            this.Controls.Add(this.FrameValidarPoliza);
            this.Controls.Add(this.FrameTotales);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFuentesDeFinaciamiento_Claves";
            this.Text = "Claves de Fuentes de Financiamiento";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFuentesDeFinaciamiento_Claves_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameClaves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.FrameTotales.ResumeLayout(false);
            this.FrameValidarPoliza.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmHasta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDesde)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameClaves;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.GroupBox FrameTotales;
        private SC_ControlsCS.scLabelExt lblDescripcion;
        private SC_ControlsCS.scLabelExt lblFinanciamiento;
        private System.Windows.Forms.GroupBox FrameValidarPoliza;
        private System.Windows.Forms.CheckBox chkValidarReferencia;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private System.Windows.Forms.NumericUpDown nmDesde;
        private System.Windows.Forms.NumericUpDown nmHasta;
    }
}