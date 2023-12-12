namespace DllFarmaciaSoft.Lotes
{
    partial class FrmMtto_LotesConsignacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMtto_LotesConsignacion));
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.NamedStyle namedStyle2 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType2 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
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
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.grdLotes = new FarPoint.Win.Spread.FpSpread();
            this.grdLotes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.lblCorregido = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCodigoEAN = new System.Windows.Forms.Label();
            this.txtIdProducto = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblContenido = new System.Windows.Forms.Label();
            this.lblDescripcionSSA = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameLotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).BeginInit();
            this.FrameInformacion.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(722, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            // 
            // FrameLotes
            // 
            this.FrameLotes.Controls.Add(this.grdLotes);
            this.FrameLotes.Location = new System.Drawing.Point(10, 208);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Size = new System.Drawing.Size(702, 288);
            this.FrameLotes.TabIndex = 2;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes del Producto";
            // 
            // grdLotes
            // 
            this.grdLotes.AccessibleDescription = "grdLotes, Sheet1, Row 0, Column 0, ";
            this.grdLotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdLotes.Location = new System.Drawing.Point(12, 17);
            this.grdLotes.Name = "grdLotes";
            namedStyle1.BackColor = System.Drawing.Color.DarkGray;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.Renderer = generalCellType1;
            namedStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(196)))), ((int)(((byte)(233)))));
            namedStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle2.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle2.Renderer = enhancedCornerRenderer1;
            namedStyle2.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1,
            namedStyle2});
            this.grdLotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdLotes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdLotes_Sheet1});
            this.grdLotes.Size = new System.Drawing.Size(681, 260);
            this.grdLotes.TabIndex = 3;
            // 
            // grdLotes_Sheet1
            // 
            this.grdLotes_Sheet1.Reset();
            this.grdLotes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdLotes_Sheet1.ColumnCount = 11;
            this.grdLotes_Sheet1.RowCount = 10;
            this.grdLotes_Sheet1.Cells.Get(0, 5).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.Cells.Get(0, 6).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Codigo";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo EAN";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Id SubFarmacia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave de lote";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Meses por Caducar";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha de Entrada";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha de Caducidad";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Existencia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Status";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "StatusReferencia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Status";
            this.grdLotes_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdLotes_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdLotes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Label = "Código";
            this.grdLotes_Sheet1.Columns.Get(0).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(0).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(0).Width = 80F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            this.grdLotes_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdLotes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Label = "Codigo EAN";
            this.grdLotes_Sheet1.Columns.Get(1).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(1).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(1).Width = 122F;
            this.grdLotes_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdLotes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Label = "Id SubFarmacia";
            this.grdLotes_Sheet1.Columns.Get(2).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Width = 75F;
            this.grdLotes_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdLotes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(3).Label = "Clave de lote";
            this.grdLotes_Sheet1.Columns.Get(3).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Width = 145F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            this.grdLotes_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdLotes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Label = "Meses por Caducar";
            this.grdLotes_Sheet1.Columns.Get(4).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd";
            this.grdLotes_Sheet1.Columns.Get(5).CellType = dateTimeCellType1;
            this.grdLotes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Label = "Fecha de Entrada";
            this.grdLotes_Sheet1.Columns.Get(5).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Width = 75F;
            dateTimeCellType2.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType2.Calendar")));
            dateTimeCellType2.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType2.DateDefault = new System.DateTime(2010, 3, 19, 13, 28, 7, 0);
            dateTimeCellType2.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType2.TimeDefault = new System.DateTime(2010, 3, 19, 13, 28, 7, 0);
            dateTimeCellType2.UserDefinedFormat = "yyyy-MM-dd";
            this.grdLotes_Sheet1.Columns.Get(6).CellType = dateTimeCellType2;
            this.grdLotes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Label = "Fecha de Caducidad";
            this.grdLotes_Sheet1.Columns.Get(6).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Width = 75F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            this.grdLotes_Sheet1.Columns.Get(7).CellType = numberCellType2;
            this.grdLotes_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Label = "Existencia";
            this.grdLotes_Sheet1.Columns.Get(7).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Width = 70F;
            this.grdLotes_Sheet1.Columns.Get(8).CellType = textCellType5;
            this.grdLotes_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Label = "Status";
            this.grdLotes_Sheet1.Columns.Get(8).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Width = 70F;
            this.grdLotes_Sheet1.Columns.Get(9).CellType = checkBoxCellType1;
            this.grdLotes_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(9).Label = "StatusReferencia";
            this.grdLotes_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(9).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(10).CellType = checkBoxCellType2;
            this.grdLotes_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Label = "Status";
            this.grdLotes_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Width = 50F;
            this.grdLotes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lblCorregido);
            this.FrameInformacion.Controls.Add(this.label3);
            this.FrameInformacion.Controls.Add(this.lblCodigoEAN);
            this.FrameInformacion.Controls.Add(this.txtIdProducto);
            this.FrameInformacion.Controls.Add(this.label7);
            this.FrameInformacion.Controls.Add(this.lblContenido);
            this.FrameInformacion.Controls.Add(this.lblDescripcionSSA);
            this.FrameInformacion.Controls.Add(this.label8);
            this.FrameInformacion.Controls.Add(this.lblClaveSSA);
            this.FrameInformacion.Controls.Add(this.lblPresentacion);
            this.FrameInformacion.Controls.Add(this.lblProducto);
            this.FrameInformacion.Controls.Add(this.label4);
            this.FrameInformacion.Controls.Add(this.label1);
            this.FrameInformacion.Location = new System.Drawing.Point(10, 31);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(702, 173);
            this.FrameInformacion.TabIndex = 0;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información del Producto";
            // 
            // lblCorregido
            // 
            this.lblCorregido.BackColor = System.Drawing.Color.Transparent;
            this.lblCorregido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCorregido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorregido.Location = new System.Drawing.Point(195, 20);
            this.lblCorregido.Name = "lblCorregido";
            this.lblCorregido.Size = new System.Drawing.Size(98, 20);
            this.lblCorregido.TabIndex = 22;
            this.lblCorregido.Text = "CORREGIDO";
            this.lblCorregido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCorregido.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(431, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Código EAN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(515, 20);
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(177, 20);
            this.lblCodigoEAN.TabIndex = 20;
            this.lblCodigoEAN.Text = "Codigo EAN :";
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProducto.Decimales = 2;
            this.txtIdProducto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProducto.ForeColor = System.Drawing.Color.Black;
            this.txtIdProducto.Location = new System.Drawing.Point(107, 20);
            this.txtIdProducto.MaxLength = 8;
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.PermitirApostrofo = false;
            this.txtIdProducto.PermitirNegativos = false;
            this.txtIdProducto.Size = new System.Drawing.Size(82, 20);
            this.txtIdProducto.TabIndex = 0;
            this.txtIdProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProducto_KeyDown);
            this.txtIdProducto.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProducto_Validating);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(517, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "Caja con :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblContenido
            // 
            this.lblContenido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContenido.Location = new System.Drawing.Point(603, 70);
            this.lblContenido.Name = "lblContenido";
            this.lblContenido.Size = new System.Drawing.Size(89, 20);
            this.lblContenido.TabIndex = 18;
            this.lblContenido.Text = "Producto :";
            this.lblContenido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescripcionSSA
            // 
            this.lblDescripcionSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSSA.Location = new System.Drawing.Point(107, 120);
            this.lblDescripcionSSA.Name = "lblDescripcionSSA";
            this.lblDescripcionSSA.Size = new System.Drawing.Size(585, 48);
            this.lblDescripcionSSA.TabIndex = 16;
            this.lblDescripcionSSA.Text = "label5";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(23, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(107, 94);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(175, 20);
            this.lblClaveSSA.TabIndex = 15;
            this.lblClaveSSA.Text = "Producto :";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(107, 70);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(376, 20);
            this.lblPresentacion.TabIndex = 14;
            this.lblPresentacion.Text = "label5";
            // 
            // lblProducto
            // 
            this.lblProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProducto.Location = new System.Drawing.Point(107, 46);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(585, 20);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "label5";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Dispensar por :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(46, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Producto :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmMtto_LotesConsignacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 504);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.FrameLotes);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmMtto_LotesConsignacion";
            this.Text = "Excepción Dispensación de Lotes de Venta con Existencia de Consignación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmModCaducidades_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameLotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).EndInit();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameInformacion.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameLotes;
        private FarPoint.Win.Spread.FpSpread grdLotes;
        private FarPoint.Win.Spread.SheetView grdLotes_Sheet1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblContenido;
        private System.Windows.Forms.Label lblDescripcionSSA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtIdProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCodigoEAN;
        private System.Windows.Forms.Label lblCorregido;
    }
}