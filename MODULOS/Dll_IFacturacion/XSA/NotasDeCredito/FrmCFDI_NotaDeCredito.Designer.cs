namespace Dll_IFacturacion.XSA
{
    partial class FrmCFDI_NotaDeCredito
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCFDI_NotaDeCredito));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClienteNombre = new SC_ControlsCS.scTextBoxExt();
            this.btlCliente = new System.Windows.Forms.Button();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.chkMultiples = new System.Windows.Forms.CheckBox();
            this.grdNota = new FarPoint.Win.Spread.FpSpread();
            this.grdNota_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblCantidadConLetra = new SC_ControlsCS.scLabelExt();
            this.lblRegistros = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_Total = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_Iva = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_SubTotal = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.menuConceptos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAgregarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModificarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblArchivoExcel = new SC_ControlsCS.scLabelExt();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeerExcel = new System.Windows.Forms.ToolStripButton();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNota_Sheet1)).BeginInit();
            this.menuConceptos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClienteNombre);
            this.groupBox1.Controls.Add(this.btlCliente);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 58);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(155, 23);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(423, 20);
            this.txtClienteNombre.TabIndex = 1;
            // 
            // btlCliente
            // 
            this.btlCliente.Location = new System.Drawing.Point(584, 23);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(26, 18);
            this.btlCliente.TabIndex = 2;
            this.btlCliente.Text = "...";
            this.btlCliente.UseVisualStyleBackColor = true;
            this.btlCliente.Click += new System.EventHandler(this.btlCliente_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(6, 22);
            this.lblCliente.MostrarToolTip = true;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(16, 20);
            this.lblCliente.TabIndex = 12;
            this.lblCliente.Text = "scLabelExt1";
            this.lblCliente.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(87, 23);
            this.txtId.MaxLength = 6;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(63, 20);
            this.txtId.TabIndex = 0;
            this.txtId.Text = "123456";
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.TextChanged += new System.EventHandler(this.txtId_TextChanged);
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.chkMultiples);
            this.FrameDetalles.Controls.Add(this.grdNota);
            this.FrameDetalles.Controls.Add(this.lblCantidadConLetra);
            this.FrameDetalles.Controls.Add(this.lblRegistros);
            this.FrameDetalles.Controls.Add(this.lblTitulo_Total);
            this.FrameDetalles.Controls.Add(this.lblTitulo_Iva);
            this.FrameDetalles.Controls.Add(this.lblSubTotal);
            this.FrameDetalles.Controls.Add(this.lblTitulo_SubTotal);
            this.FrameDetalles.Controls.Add(this.lblIva);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 207);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(994, 362);
            this.FrameDetalles.TabIndex = 6;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la nota de crédito";
            // 
            // chkMultiples
            // 
            this.chkMultiples.AutoSize = true;
            this.chkMultiples.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMultiples.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMultiples.Location = new System.Drawing.Point(818, -1);
            this.chkMultiples.Name = "chkMultiples";
            this.chkMultiples.Size = new System.Drawing.Size(171, 17);
            this.chkMultiples.TabIndex = 49;
            this.chkMultiples.Text = "Permite multibles facturas";
            this.chkMultiples.UseVisualStyleBackColor = true;
            this.chkMultiples.CheckedChanged += new System.EventHandler(this.chkMultiples_CheckedChanged);
            // 
            // grdNota
            // 
            this.grdNota.AccessibleDescription = "grdNota, Sheet1, Row 0, Column 0, HGIOA";
            this.grdNota.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdNota.Location = new System.Drawing.Point(10, 19);
            this.grdNota.Name = "grdNota";
            this.grdNota.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdNota.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdNota_Sheet1});
            this.grdNota.Size = new System.Drawing.Size(976, 254);
            this.grdNota.TabIndex = 0;
            this.grdNota.EditModeOff += new System.EventHandler(this.grdNota_EditModeOff);
            this.grdNota.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdNota_Advance);
            this.grdNota.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdNota_KeyDown);
            // 
            // grdNota_Sheet1
            // 
            this.grdNota_Sheet1.Reset();
            this.grdNota_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdNota_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdNota_Sheet1.ColumnCount = 10;
            this.grdNota_Sheet1.RowCount = 12;
            this.grdNota_Sheet1.Cells.Get(0, 0).Value = "HGIOA";
            this.grdNota_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(0, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(1, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(2, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(3, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(4, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(5, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(6, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(7, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(8, 9).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdNota_Sheet1.Cells.Get(9, 9).Value = 0D;
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Serie";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "UUID";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Importe Base";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Notas Anteriores";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Clave SSA";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Tasa Iva";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Importe a aplicar";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Importe Iva";
            this.grdNota_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Importe Sin Iva";
            this.grdNota_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            textCellType1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdNota_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdNota_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(0).Label = "Serie";
            this.grdNota_Sheet1.Columns.Get(0).Locked = false;
            this.grdNota_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(0).Width = 80F;
            this.grdNota_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdNota_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdNota_Sheet1.Columns.Get(1).Locked = false;
            this.grdNota_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(1).Width = 80F;
            this.grdNota_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdNota_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(2).Label = "UUID";
            this.grdNota_Sheet1.Columns.Get(2).Locked = true;
            this.grdNota_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(2).Width = 300F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 99999999.99D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdNota_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdNota_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdNota_Sheet1.Columns.Get(3).Label = "Importe Base";
            this.grdNota_Sheet1.Columns.Get(3).Locked = true;
            this.grdNota_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(3).Width = 120F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.MaximumValue = 99999999.99D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdNota_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdNota_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdNota_Sheet1.Columns.Get(4).Label = "Notas Anteriores";
            this.grdNota_Sheet1.Columns.Get(4).Locked = true;
            this.grdNota_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(4).Width = 120F;
            this.grdNota_Sheet1.Columns.Get(5).CellType = textCellType4;
            this.grdNota_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(5).Label = "Clave SSA";
            this.grdNota_Sheet1.Columns.Get(5).Locked = true;
            this.grdNota_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(5).Visible = false;
            this.grdNota_Sheet1.Columns.Get(6).CellType = numberCellType3;
            this.grdNota_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdNota_Sheet1.Columns.Get(6).Label = "Tasa Iva";
            this.grdNota_Sheet1.Columns.Get(6).Locked = true;
            this.grdNota_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MaximumValue = 99999999.99D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdNota_Sheet1.Columns.Get(7).CellType = numberCellType4;
            this.grdNota_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdNota_Sheet1.Columns.Get(7).Label = "Importe a aplicar";
            this.grdNota_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(7).Width = 140F;
            numberCellType5.DecimalPlaces = 4;
            numberCellType5.MaximumValue = 99999999.99D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdNota_Sheet1.Columns.Get(8).CellType = numberCellType5;
            this.grdNota_Sheet1.Columns.Get(8).Label = "Importe Iva";
            this.grdNota_Sheet1.Columns.Get(8).Locked = true;
            this.grdNota_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(8).Width = 140F;
            numberCellType6.DecimalPlaces = 4;
            numberCellType6.MaximumValue = 99999999.99D;
            numberCellType6.MinimumValue = 0D;
            numberCellType6.Separator = ",";
            numberCellType6.ShowSeparator = true;
            this.grdNota_Sheet1.Columns.Get(9).CellType = numberCellType6;
            this.grdNota_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdNota_Sheet1.Columns.Get(9).Label = "Importe Sin Iva";
            this.grdNota_Sheet1.Columns.Get(9).Locked = true;
            this.grdNota_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdNota_Sheet1.Columns.Get(9).Width = 140F;
            this.grdNota_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdNota_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblCantidadConLetra
            // 
            this.lblCantidadConLetra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCantidadConLetra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadConLetra.Location = new System.Drawing.Point(15, 298);
            this.lblCantidadConLetra.MostrarToolTip = false;
            this.lblCantidadConLetra.Name = "lblCantidadConLetra";
            this.lblCantidadConLetra.Size = new System.Drawing.Size(483, 54);
            this.lblCantidadConLetra.TabIndex = 2;
            this.lblCantidadConLetra.Text = "scLabelExt7";
            // 
            // lblRegistros
            // 
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(15, 276);
            this.lblRegistros.MostrarToolTip = false;
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(373, 20);
            this.lblRegistros.TabIndex = 1;
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitulo_Total
            // 
            this.lblTitulo_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Total.Location = new System.Drawing.Point(742, 331);
            this.lblTitulo_Total.MostrarToolTip = false;
            this.lblTitulo_Total.Name = "lblTitulo_Total";
            this.lblTitulo_Total.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Total.TabIndex = 22;
            this.lblTitulo_Total.Text = "Total :";
            this.lblTitulo_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo_Iva
            // 
            this.lblTitulo_Iva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Iva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Iva.Location = new System.Drawing.Point(742, 306);
            this.lblTitulo_Iva.MostrarToolTip = false;
            this.lblTitulo_Iva.Name = "lblTitulo_Iva";
            this.lblTitulo_Iva.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Iva.TabIndex = 21;
            this.lblTitulo_Iva.Text = "Iva :";
            this.lblTitulo_Iva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(848, 281);
            this.lblSubTotal.MostrarToolTip = false;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal.TabIndex = 3;
            this.lblSubTotal.Text = "scLabelExt1";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo_SubTotal
            // 
            this.lblTitulo_SubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_SubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_SubTotal.Location = new System.Drawing.Point(742, 281);
            this.lblTitulo_SubTotal.MostrarToolTip = false;
            this.lblTitulo_SubTotal.Name = "lblTitulo_SubTotal";
            this.lblTitulo_SubTotal.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_SubTotal.TabIndex = 20;
            this.lblTitulo_SubTotal.Text = "Sub-Total :";
            this.lblTitulo_SubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(848, 306);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(137, 22);
            this.lblIva.TabIndex = 4;
            this.lblIva.Text = "scLabelExt2";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(848, 331);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuConceptos
            // 
            this.menuConceptos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuConceptos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregarConcepto,
            this.btnEliminarConcepto,
            this.btnModificarConcepto});
            this.menuConceptos.Name = "menuFolios";
            this.menuConceptos.Size = new System.Drawing.Size(126, 70);
            // 
            // btnAgregarConcepto
            // 
            this.btnAgregarConcepto.Name = "btnAgregarConcepto";
            this.btnAgregarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnAgregarConcepto.Text = "Agregar";
            // 
            // btnEliminarConcepto
            // 
            this.btnEliminarConcepto.Name = "btnEliminarConcepto";
            this.btnEliminarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnEliminarConcepto.Text = "Eliminar";
            // 
            // btnModificarConcepto
            // 
            this.btnModificarConcepto.Name = "btnModificarConcepto";
            this.btnModificarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnModificarConcepto.Text = "Modificar";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(636, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 58);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(12, 22);
            this.lblSerie.MostrarToolTip = false;
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(41, 20);
            this.lblSerie.TabIndex = 14;
            this.lblSerie.Text = "Serie : ";
            this.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSeries
            // 
            this.cboSeries.BackColorEnabled = System.Drawing.Color.White;
            this.cboSeries.Data = "";
            this.cboSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeries.Filtro = " 1 = 1";
            this.cboSeries.FormattingEnabled = true;
            this.cboSeries.ListaItemsBusqueda = 20;
            this.cboSeries.Location = new System.Drawing.Point(53, 22);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(118, 21);
            this.cboSeries.TabIndex = 0;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(174, 332);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 47);
            this.FrameProceso.TabIndex = 7;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 20);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 15);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // tmProceso
            // 
            this.tmProceso.Enabled = true;
            this.tmProceso.Interval = 500;
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnObservacionesGral);
            this.groupBox2.Location = new System.Drawing.Point(815, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 58);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(15, 20);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(167, 23);
            this.btnObservacionesGral.TabIndex = 0;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1013, 27);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(24, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(24, 24);
            this.btnFacturar.Text = "Generar factura electrónica";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(24, 24);
            this.btnValidarDatos.Text = "Vista previa del documento";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // toolStripSeparatorVistaPrevia
            // 
            this.toolStripSeparatorVistaPrevia.Name = "toolStripSeparatorVistaPrevia";
            this.toolStripSeparatorVistaPrevia.Size = new System.Drawing.Size(6, 27);
            // 
            // btnConsultarTimbres
            // 
            this.btnConsultarTimbres.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConsultarTimbres.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarTimbres.Image")));
            this.btnConsultarTimbres.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultarTimbres.Name = "btnConsultarTimbres";
            this.btnConsultarTimbres.Size = new System.Drawing.Size(24, 24);
            this.btnConsultarTimbres.Text = "Consultar timbres";
            this.btnConsultarTimbres.Click += new System.EventHandler(this.btnConsultarTimbres_Click);
            // 
            // lblTimbresDisponibles
            // 
            this.lblTimbresDisponibles.Name = "lblTimbresDisponibles";
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(116, 24);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(87, 17);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(899, 20);
            this.txtDescripcion.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Detalle : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDescripcion);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(12, 86);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(994, 42);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Descripción";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.lblArchivoExcel);
            this.groupBox5.Controls.Add(this.toolStrip1);
            this.groupBox5.Controls.Add(this.cboHojas);
            this.groupBox5.Location = new System.Drawing.Point(12, 131);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(994, 72);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Cargar plantilla de excel";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(470, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hoja : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Archivo : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblArchivoExcel
            // 
            this.lblArchivoExcel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArchivoExcel.Location = new System.Drawing.Point(62, 46);
            this.lblArchivoExcel.MostrarToolTip = false;
            this.lblArchivoExcel.Name = "lblArchivoExcel";
            this.lblArchivoExcel.Size = new System.Drawing.Size(356, 20);
            this.lblArchivoExcel.TabIndex = 13;
            this.lblArchivoExcel.Text = "scLabelExt1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoExcel,
            this.toolStripSeparator1,
            this.btnAbrirExcel,
            this.toolStripSeparator3,
            this.btnLeerExcel});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(988, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoExcel
            // 
            this.btnNuevoExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoExcel.Image")));
            this.btnNuevoExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoExcel.Name = "btnNuevoExcel";
            this.btnNuevoExcel.Size = new System.Drawing.Size(24, 24);
            this.btnNuevoExcel.Text = "Nuevo excel";
            this.btnNuevoExcel.Click += new System.EventHandler(this.btnNuevoExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirExcel.Image")));
            this.btnAbrirExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(24, 24);
            this.btnAbrirExcel.Text = "&Abrir";
            this.btnAbrirExcel.Click += new System.EventHandler(this.btnAbrirExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnLeerExcel
            // 
            this.btnLeerExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerExcel.Image")));
            this.btnLeerExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerExcel.Name = "btnLeerExcel";
            this.btnLeerExcel.Size = new System.Drawing.Size(24, 24);
            this.btnLeerExcel.Text = "Leer hoja";
            this.btnLeerExcel.ToolTipText = "Leer hoja";
            this.btnLeerExcel.Click += new System.EventHandler(this.btnLeerExcel_Click);
            // 
            // cboHojas
            // 
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(526, 46);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(459, 21);
            this.cboHojas.TabIndex = 1;
            // 
            // FrmCFDI_NotaDeCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 577);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDetalles);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCFDI_NotaDeCredito";
            this.Text = "Registro de Notas de Crédito";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCFDI_NotaDeCredito_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameDetalles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNota_Sheet1)).EndInit();
            this.menuConceptos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.Button btlCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scComboBoxExt cboSeries;
        private SC_ControlsCS.scLabelExt lblSerie;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnObservacionesGral;
        private SC_ControlsCS.scTextBoxExt txtClienteNombre;
        private System.Windows.Forms.ContextMenuStrip menuConceptos;
        private System.Windows.Forms.ToolStripMenuItem btnAgregarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnModificarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarConcepto;
        private SC_ControlsCS.scLabelExt lblSubTotal;
        private SC_ControlsCS.scLabelExt lblIva;
        private SC_ControlsCS.scLabelExt lblTotal;
        private SC_ControlsCS.scLabelExt lblTitulo_Total;
        private SC_ControlsCS.scLabelExt lblTitulo_Iva;
        private SC_ControlsCS.scLabelExt lblTitulo_SubTotal;
        private SC_ControlsCS.scLabelExt lblRegistros;
        private SC_ControlsCS.scLabelExt lblCantidadConLetra;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private FarPoint.Win.Spread.FpSpread grdNota;
        private FarPoint.Win.Spread.SheetView grdNota_Sheet1;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scLabelExt lblArchivoExcel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevoExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAbrirExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLeerExcel;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.CheckBox chkMultiples;
    }
}