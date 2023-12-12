namespace Dll_MA_IFacturacion.XSA
{
    partial class FrmCFDI_ComplementoDePago
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCFDI_ComplementoDePago));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboUsosCFDI = new SC_ControlsCS.scComboBoxExt();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.chkCargaManual = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdUUIDs = new FarPoint.Win.Spread.FpSpread();
            this.grdUUIDs_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.menuConceptos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAgregarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModificarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeerExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBloquearHoja = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblArchivoExcel = new SC_ControlsCS.scLabelExt();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkTieneDocumentoRelacionado = new System.Windows.Forms.CheckBox();
            this.txtRelacion__UUID = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelacion__Serie = new SC_ControlsCS.scTextBoxExt();
            this.txtRelacion__Folio = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaDePago = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtHoraDePago = new System.Windows.Forms.DateTimePicker();
            this.cboFormasDePago = new SC_ControlsCS.scComboBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.cboMonedas = new SC_ControlsCS.scComboBoxExt();
            this.FrameInformacionDelPago = new System.Windows.Forms.GroupBox();
            this.cboReceptor_RFC_BancoDestino = new SC_ControlsCS.scComboBoxExt();
            this.cboEmisor_RFC_BancoOrigen = new SC_ControlsCS.scComboBoxExt();
            this.label18 = new System.Windows.Forms.Label();
            this.lblTotalDocumentos = new SC_ControlsCS.scLabelExt();
            this.btnInformacionPago = new System.Windows.Forms.Button();
            this.btnReceptor_AgregarCuenta = new System.Windows.Forms.Button();
            this.btnEmisor_AgregarCuenta = new System.Windows.Forms.Button();
            this.cboReceptor_CuentaBeneficiario = new SC_ControlsCS.scComboBoxExt();
            this.cboEmisor_CuentaOrdenante = new SC_ControlsCS.scComboBoxExt();
            this.nmImportePago = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNumeroDeOperacion = new SC_ControlsCS.scTextBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtEmisor_Beneficiario = new SC_ControlsCS.scTextBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEmisor_CuentaOrdenante = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tmProceso_LoadInformacion = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUUIDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUUIDs_Sheet1)).BeginInit();
            this.menuConceptos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.FrameInformacionDelPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmImportePago)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(174, 17);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(557, 20);
            this.lblCliente.TabIndex = 12;
            this.lblCliente.Text = "scLabelExt1";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(106, 18);
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
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(113, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Uso del CFDI : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // cboUsosCFDI
            // 
            this.cboUsosCFDI.BackColorEnabled = System.Drawing.Color.White;
            this.cboUsosCFDI.Data = "";
            this.cboUsosCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsosCFDI.Filtro = " 1 = 1";
            this.cboUsosCFDI.FormattingEnabled = true;
            this.cboUsosCFDI.ListaItemsBusqueda = 20;
            this.cboUsosCFDI.Location = new System.Drawing.Point(146, 16);
            this.cboUsosCFDI.MostrarToolTip = false;
            this.cboUsosCFDI.Name = "cboUsosCFDI";
            this.cboUsosCFDI.Size = new System.Drawing.Size(33, 21);
            this.cboUsosCFDI.TabIndex = 3;
            this.cboUsosCFDI.Visible = false;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.chkCargaManual);
            this.FrameDetalles.Controls.Add(this.FrameProceso);
            this.FrameDetalles.Controls.Add(this.grdUUIDs);
            this.FrameDetalles.Location = new System.Drawing.Point(9, 323);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1167, 299);
            this.FrameDetalles.TabIndex = 6;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Listado de documentos";
            // 
            // chkCargaManual
            // 
            this.chkCargaManual.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCargaManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCargaManual.Location = new System.Drawing.Point(961, 0);
            this.chkCargaManual.Name = "chkCargaManual";
            this.chkCargaManual.Size = new System.Drawing.Size(196, 17);
            this.chkCargaManual.TabIndex = 51;
            this.chkCargaManual.Text = "Carga manual de CFDI";
            this.chkCargaManual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCargaManual.UseVisualStyleBackColor = true;
            this.chkCargaManual.CheckedChanged += new System.EventHandler(this.chkCargaManual_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(144, 126);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(835, 47);
            this.FrameProceso.TabIndex = 8;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 20);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(805, 15);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdUUIDs
            // 
            this.grdUUIDs.AccessibleDescription = "grdUUIDs, Sheet1, Row 0, Column 0, HGIOA";
            this.grdUUIDs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUUIDs.Location = new System.Drawing.Point(10, 19);
            this.grdUUIDs.Name = "grdUUIDs";
            this.grdUUIDs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUUIDs.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUUIDs_Sheet1});
            this.grdUUIDs.Size = new System.Drawing.Size(1147, 269);
            this.grdUUIDs.TabIndex = 1;
            this.grdUUIDs.EditModeOff += new System.EventHandler(this.grdUUIDs_EditModeOff);
            this.grdUUIDs.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdUUIDs_Advance);
            this.grdUUIDs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdUUIDs_KeyDown);
            // 
            // grdUUIDs_Sheet1
            // 
            this.grdUUIDs_Sheet1.Reset();
            this.grdUUIDs_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUUIDs_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUUIDs_Sheet1.ColumnCount = 9;
            this.grdUUIDs_Sheet1.RowCount = 12;
            this.grdUUIDs_Sheet1.Cells.Get(0, 0).Value = "HGIOA";
            this.grdUUIDs_Sheet1.Cells.Get(0, 4).Value = 500000D;
            this.grdUUIDs_Sheet1.Cells.Get(0, 5).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(0, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(0, 6).Value = 500000D;
            this.grdUUIDs_Sheet1.Cells.Get(0, 7).Value = 150000D;
            this.grdUUIDs_Sheet1.Cells.Get(0, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(0, 8).Value = 350000D;
            this.grdUUIDs_Sheet1.Cells.Get(1, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(1, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(1, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(1, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(2, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(2, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(2, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(3, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(3, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(3, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(4, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(4, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(4, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(5, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(5, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(5, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(6, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(6, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(6, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(7, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(7, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(7, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(8, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(8, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(8, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(9, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(9, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(9, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(10, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(10, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(10, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(11, 6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(11, 6).Value = 0D;
            this.grdUUIDs_Sheet1.Cells.Get(11, 8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Cells.Get(11, 8).Value = 0D;
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Serie";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Folio";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "UUID";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Parcialidad";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Valor nominal";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Total pagado";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Saldo anterior";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Abono a aplicar";
            this.grdUUIDs_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Restante";
            this.grdUUIDs_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            textCellType1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.grdUUIDs_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdUUIDs_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(0).Label = "Serie";
            this.grdUUIDs_Sheet1.Columns.Get(0).Locked = false;
            this.grdUUIDs_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(0).Width = 90F;
            this.grdUUIDs_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdUUIDs_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(1).Label = "Folio";
            this.grdUUIDs_Sheet1.Columns.Get(1).Locked = false;
            this.grdUUIDs_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(1).Width = 80F;
            this.grdUUIDs_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdUUIDs_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUUIDs_Sheet1.Columns.Get(2).Label = "UUID";
            this.grdUUIDs_Sheet1.Columns.Get(2).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(2).Width = 300F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdUUIDs_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(3).Label = "Parcialidad";
            this.grdUUIDs_Sheet1.Columns.Get(3).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(3).Width = 70F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 999999999999.99D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdUUIDs_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUUIDs_Sheet1.Columns.Get(4).Label = "Valor nominal";
            this.grdUUIDs_Sheet1.Columns.Get(4).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(4).Width = 110F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 999999999999.99D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdUUIDs_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUUIDs_Sheet1.Columns.Get(5).Label = "Total pagado";
            this.grdUUIDs_Sheet1.Columns.Get(5).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(5).Width = 110F;
            numberCellType4.DecimalPlaces = 4;
            numberCellType4.MaximumValue = 99999999.99D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdUUIDs_Sheet1.Columns.Get(6).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUUIDs_Sheet1.Columns.Get(6).Label = "Saldo anterior";
            this.grdUUIDs_Sheet1.Columns.Get(6).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(6).Width = 110F;
            numberCellType5.DecimalPlaces = 4;
            numberCellType5.MaximumValue = 99999999.99D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(7).CellType = numberCellType5;
            this.grdUUIDs_Sheet1.Columns.Get(7).Label = "Abono a aplicar";
            this.grdUUIDs_Sheet1.Columns.Get(7).Locked = false;
            this.grdUUIDs_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(7).Width = 110F;
            numberCellType6.DecimalPlaces = 4;
            numberCellType6.MaximumValue = 99999999.99D;
            numberCellType6.MinimumValue = 0D;
            numberCellType6.Separator = ",";
            numberCellType6.ShowSeparator = true;
            this.grdUUIDs_Sheet1.Columns.Get(8).CellType = numberCellType6;
            this.grdUUIDs_Sheet1.Columns.Get(8).Formula = "RC[-2]-RC[-1]";
            this.grdUUIDs_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdUUIDs_Sheet1.Columns.Get(8).Label = "Restante";
            this.grdUUIDs_Sheet1.Columns.Get(8).Locked = true;
            this.grdUUIDs_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUUIDs_Sheet1.Columns.Get(8).Width = 110F;
            this.grdUUIDs_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUUIDs_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // menuConceptos
            // 
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
            this.btnAgregarConcepto.Click += new System.EventHandler(this.btnAgregarConcepto_Click);
            // 
            // btnEliminarConcepto
            // 
            this.btnEliminarConcepto.Name = "btnEliminarConcepto";
            this.btnEliminarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnEliminarConcepto.Text = "Eliminar";
            this.btnEliminarConcepto.Click += new System.EventHandler(this.btnEliminarConcepto_Click);
            // 
            // btnModificarConcepto
            // 
            this.btnModificarConcepto.Name = "btnModificarConcepto";
            this.btnModificarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnModificarConcepto.Text = "Modificar";
            this.btnModificarConcepto.Click += new System.EventHandler(this.btnModificarConcepto_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(752, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 44);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Complemento de Pago";
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(12, 19);
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
            this.cboSeries.Location = new System.Drawing.Point(53, 19);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(157, 21);
            this.cboSeries.TabIndex = 0;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // tmProceso
            // 
            this.tmProceso.Enabled = true;
            this.tmProceso.Interval = 500;
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnObservacionesGral);
            this.groupBox2.Controls.Add(this.cboUsosCFDI);
            this.groupBox2.Location = new System.Drawing.Point(985, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 44);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones";
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(14, 15);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(91, 23);
            this.btnObservacionesGral.TabIndex = 0;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.toolStrip1);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.lblArchivoExcel);
            this.groupBox5.Controls.Add(this.cboHojas);
            this.groupBox5.Location = new System.Drawing.Point(9, 247);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1167, 74);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Archivo de excel";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoExcel,
            this.toolStripSeparator1,
            this.btnAbrirExcel,
            this.toolStripSeparator3,
            this.btnLeerExcel,
            this.toolStripSeparator4,
            this.btnBloquearHoja});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1161, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoExcel
            // 
            this.btnNuevoExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoExcel.Image")));
            this.btnNuevoExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoExcel.Name = "btnNuevoExcel";
            this.btnNuevoExcel.Size = new System.Drawing.Size(23, 22);
            this.btnNuevoExcel.Text = "Nuevo excel";
            this.btnNuevoExcel.Click += new System.EventHandler(this.btnNuevoExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirExcel.Image")));
            this.btnAbrirExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirExcel.Text = "&Abrir";
            this.btnAbrirExcel.Click += new System.EventHandler(this.btnAbrirExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLeerExcel
            // 
            this.btnLeerExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerExcel.Image")));
            this.btnLeerExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerExcel.Name = "btnLeerExcel";
            this.btnLeerExcel.Size = new System.Drawing.Size(23, 22);
            this.btnLeerExcel.Text = "Leer hoja";
            this.btnLeerExcel.ToolTipText = "Leer hoja";
            this.btnLeerExcel.Click += new System.EventHandler(this.btnLeerExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBloquearHoja
            // 
            this.btnBloquearHoja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBloquearHoja.Image = ((System.Drawing.Image)(resources.GetObject("btnBloquearHoja.Image")));
            this.btnBloquearHoja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBloquearHoja.Name = "btnBloquearHoja";
            this.btnBloquearHoja.Size = new System.Drawing.Size(23, 22);
            this.btnBloquearHoja.Text = "Bloquear / Desbloquear hoja";
            this.btnBloquearHoja.Click += new System.EventHandler(this.btnBloquearHoja_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(642, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hoja : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Archivo : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblArchivoExcel
            // 
            this.lblArchivoExcel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArchivoExcel.Location = new System.Drawing.Point(62, 45);
            this.lblArchivoExcel.MostrarToolTip = false;
            this.lblArchivoExcel.Name = "lblArchivoExcel";
            this.lblArchivoExcel.Size = new System.Drawing.Size(502, 20);
            this.lblArchivoExcel.TabIndex = 13;
            this.lblArchivoExcel.Text = "scLabelExt1";
            // 
            // cboHojas
            // 
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(698, 43);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(459, 21);
            this.cboHojas.TabIndex = 1;
            this.cboHojas.SelectedIndexChanged += new System.EventHandler(this.cboHojas_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
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
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar factura electrónica";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(23, 22);
            this.btnValidarDatos.Text = "Vista previa del documento";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // toolStripSeparatorVistaPrevia
            // 
            this.toolStripSeparatorVistaPrevia.Name = "toolStripSeparatorVistaPrevia";
            this.toolStripSeparatorVistaPrevia.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConsultarTimbres
            // 
            this.btnConsultarTimbres.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConsultarTimbres.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultarTimbres.Image")));
            this.btnConsultarTimbres.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultarTimbres.Name = "btnConsultarTimbres";
            this.btnConsultarTimbres.Size = new System.Drawing.Size(23, 22);
            this.btnConsultarTimbres.Text = "Consultar timbres";
            this.btnConsultarTimbres.Click += new System.EventHandler(this.btnConsultarTimbres_Click);
            // 
            // lblTimbresDisponibles
            // 
            this.lblTimbresDisponibles.Name = "lblTimbresDisponibles";
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(116, 22);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkTieneDocumentoRelacionado);
            this.groupBox4.Controls.Add(this.txtRelacion__UUID);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtRelacion__Serie);
            this.groupBox4.Controls.Add(this.txtRelacion__Folio);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(20, 93);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(461, 75);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información CFDI relacionado";
            // 
            // chkTieneDocumentoRelacionado
            // 
            this.chkTieneDocumentoRelacionado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.Location = new System.Drawing.Point(315, -1);
            this.chkTieneDocumentoRelacionado.Margin = new System.Windows.Forms.Padding(2);
            this.chkTieneDocumentoRelacionado.Name = "chkTieneDocumentoRelacionado";
            this.chkTieneDocumentoRelacionado.Size = new System.Drawing.Size(134, 20);
            this.chkTieneDocumentoRelacionado.TabIndex = 0;
            this.chkTieneDocumentoRelacionado.Text = "Sustituye a otro CFDI";
            this.chkTieneDocumentoRelacionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.UseVisualStyleBackColor = true;
            this.chkTieneDocumentoRelacionado.CheckedChanged += new System.EventHandler(this.chkTieneDocumentoRelacionado_CheckedChanged);
            // 
            // txtRelacion__UUID
            // 
            this.txtRelacion__UUID.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__UUID.Decimales = 2;
            this.txtRelacion__UUID.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__UUID.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__UUID.Location = new System.Drawing.Point(58, 45);
            this.txtRelacion__UUID.MaxLength = 10;
            this.txtRelacion__UUID.Name = "txtRelacion__UUID";
            this.txtRelacion__UUID.PermitirApostrofo = false;
            this.txtRelacion__UUID.PermitirNegativos = false;
            this.txtRelacion__UUID.Size = new System.Drawing.Size(391, 20);
            this.txtRelacion__UUID.TabIndex = 3;
            this.txtRelacion__UUID.Text = "0123456789012345678901234567890123456789";
            this.txtRelacion__UUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "UUID : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRelacion__Serie
            // 
            this.txtRelacion__Serie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Serie.Decimales = 2;
            this.txtRelacion__Serie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRelacion__Serie.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Serie.Location = new System.Drawing.Point(58, 20);
            this.txtRelacion__Serie.MaxLength = 10;
            this.txtRelacion__Serie.Name = "txtRelacion__Serie";
            this.txtRelacion__Serie.PermitirApostrofo = false;
            this.txtRelacion__Serie.PermitirNegativos = false;
            this.txtRelacion__Serie.Size = new System.Drawing.Size(93, 20);
            this.txtRelacion__Serie.TabIndex = 1;
            this.txtRelacion__Serie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRelacion__Folio
            // 
            this.txtRelacion__Folio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Folio.Decimales = 2;
            this.txtRelacion__Folio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__Folio.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Folio.Location = new System.Drawing.Point(241, 20);
            this.txtRelacion__Folio.MaxLength = 10;
            this.txtRelacion__Folio.Name = "txtRelacion__Folio";
            this.txtRelacion__Folio.PermitirApostrofo = false;
            this.txtRelacion__Folio.PermitirNegativos = false;
            this.txtRelacion__Folio.Size = new System.Drawing.Size(93, 20);
            this.txtRelacion__Folio.TabIndex = 2;
            this.txtRelacion__Folio.Text = "0123456789";
            this.txtRelacion__Folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Serie : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(196, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Folio : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "Fecha de pago :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Forma de pago :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDePago
            // 
            this.dtpFechaDePago.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaDePago.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDePago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDePago.Location = new System.Drawing.Point(106, 19);
            this.dtpFechaDePago.Name = "dtpFechaDePago";
            this.dtpFechaDePago.Size = new System.Drawing.Size(92, 20);
            this.dtpFechaDePago.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(239, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Hora : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtHoraDePago
            // 
            this.dtHoraDePago.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtHoraDePago.CustomFormat = "HH:mm:ss";
            this.dtHoraDePago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtHoraDePago.Location = new System.Drawing.Point(286, 19);
            this.dtHoraDePago.Name = "dtHoraDePago";
            this.dtHoraDePago.Size = new System.Drawing.Size(77, 20);
            this.dtHoraDePago.TabIndex = 1;
            // 
            // cboFormasDePago
            // 
            this.cboFormasDePago.BackColorEnabled = System.Drawing.Color.White;
            this.cboFormasDePago.Data = "";
            this.cboFormasDePago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormasDePago.Filtro = " 1 = 1";
            this.cboFormasDePago.FormattingEnabled = true;
            this.cboFormasDePago.ListaItemsBusqueda = 20;
            this.cboFormasDePago.Location = new System.Drawing.Point(106, 43);
            this.cboFormasDePago.MostrarToolTip = false;
            this.cboFormasDePago.Name = "cboFormasDePago";
            this.cboFormasDePago.Size = new System.Drawing.Size(375, 21);
            this.cboFormasDePago.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 15);
            this.label11.TabIndex = 28;
            this.label11.Text = "Moneda :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMonedas
            // 
            this.cboMonedas.BackColorEnabled = System.Drawing.Color.White;
            this.cboMonedas.Data = "";
            this.cboMonedas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonedas.Filtro = " 1 = 1";
            this.cboMonedas.FormattingEnabled = true;
            this.cboMonedas.ListaItemsBusqueda = 20;
            this.cboMonedas.Location = new System.Drawing.Point(106, 67);
            this.cboMonedas.MostrarToolTip = false;
            this.cboMonedas.Name = "cboMonedas";
            this.cboMonedas.Size = new System.Drawing.Size(375, 21);
            this.cboMonedas.TabIndex = 3;
            // 
            // FrameInformacionDelPago
            // 
            this.FrameInformacionDelPago.Controls.Add(this.cboReceptor_RFC_BancoDestino);
            this.FrameInformacionDelPago.Controls.Add(this.cboEmisor_RFC_BancoOrigen);
            this.FrameInformacionDelPago.Controls.Add(this.label18);
            this.FrameInformacionDelPago.Controls.Add(this.lblTotalDocumentos);
            this.FrameInformacionDelPago.Controls.Add(this.btnInformacionPago);
            this.FrameInformacionDelPago.Controls.Add(this.btnReceptor_AgregarCuenta);
            this.FrameInformacionDelPago.Controls.Add(this.btnEmisor_AgregarCuenta);
            this.FrameInformacionDelPago.Controls.Add(this.cboReceptor_CuentaBeneficiario);
            this.FrameInformacionDelPago.Controls.Add(this.cboEmisor_CuentaOrdenante);
            this.FrameInformacionDelPago.Controls.Add(this.nmImportePago);
            this.FrameInformacionDelPago.Controls.Add(this.label17);
            this.FrameInformacionDelPago.Controls.Add(this.txtNumeroDeOperacion);
            this.FrameInformacionDelPago.Controls.Add(this.label16);
            this.FrameInformacionDelPago.Controls.Add(this.txtEmisor_Beneficiario);
            this.FrameInformacionDelPago.Controls.Add(this.label14);
            this.FrameInformacionDelPago.Controls.Add(this.label15);
            this.FrameInformacionDelPago.Controls.Add(this.txtEmisor_CuentaOrdenante);
            this.FrameInformacionDelPago.Controls.Add(this.label13);
            this.FrameInformacionDelPago.Controls.Add(this.label12);
            this.FrameInformacionDelPago.Controls.Add(this.cboMonedas);
            this.FrameInformacionDelPago.Controls.Add(this.label11);
            this.FrameInformacionDelPago.Controls.Add(this.cboFormasDePago);
            this.FrameInformacionDelPago.Controls.Add(this.dtHoraDePago);
            this.FrameInformacionDelPago.Controls.Add(this.label10);
            this.FrameInformacionDelPago.Controls.Add(this.dtpFechaDePago);
            this.FrameInformacionDelPago.Controls.Add(this.label8);
            this.FrameInformacionDelPago.Controls.Add(this.label9);
            this.FrameInformacionDelPago.Controls.Add(this.groupBox4);
            this.FrameInformacionDelPago.Location = new System.Drawing.Point(9, 72);
            this.FrameInformacionDelPago.Margin = new System.Windows.Forms.Padding(2);
            this.FrameInformacionDelPago.Name = "FrameInformacionDelPago";
            this.FrameInformacionDelPago.Padding = new System.Windows.Forms.Padding(2);
            this.FrameInformacionDelPago.Size = new System.Drawing.Size(1167, 176);
            this.FrameInformacionDelPago.TabIndex = 4;
            this.FrameInformacionDelPago.TabStop = false;
            this.FrameInformacionDelPago.Text = "Información general del pago";
            // 
            // cboReceptor_RFC_BancoDestino
            // 
            this.cboReceptor_RFC_BancoDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboReceptor_RFC_BancoDestino.Data = "";
            this.cboReceptor_RFC_BancoDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReceptor_RFC_BancoDestino.Filtro = " 1 = 1";
            this.cboReceptor_RFC_BancoDestino.FormattingEnabled = true;
            this.cboReceptor_RFC_BancoDestino.ListaItemsBusqueda = 20;
            this.cboReceptor_RFC_BancoDestino.Location = new System.Drawing.Point(641, 67);
            this.cboReceptor_RFC_BancoDestino.MostrarToolTip = false;
            this.cboReceptor_RFC_BancoDestino.Name = "cboReceptor_RFC_BancoDestino";
            this.cboReceptor_RFC_BancoDestino.Size = new System.Drawing.Size(476, 21);
            this.cboReceptor_RFC_BancoDestino.TabIndex = 45;
            this.cboReceptor_RFC_BancoDestino.SelectedIndexChanged += new System.EventHandler(this.cboReceptor_RFC_BancoDestino_SelectedIndexChanged);
            // 
            // cboEmisor_RFC_BancoOrigen
            // 
            this.cboEmisor_RFC_BancoOrigen.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmisor_RFC_BancoOrigen.Data = "";
            this.cboEmisor_RFC_BancoOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmisor_RFC_BancoOrigen.Filtro = " 1 = 1";
            this.cboEmisor_RFC_BancoOrigen.FormattingEnabled = true;
            this.cboEmisor_RFC_BancoOrigen.ListaItemsBusqueda = 20;
            this.cboEmisor_RFC_BancoOrigen.Location = new System.Drawing.Point(641, 19);
            this.cboEmisor_RFC_BancoOrigen.MostrarToolTip = false;
            this.cboEmisor_RFC_BancoOrigen.Name = "cboEmisor_RFC_BancoOrigen";
            this.cboEmisor_RFC_BancoOrigen.Size = new System.Drawing.Size(476, 21);
            this.cboEmisor_RFC_BancoOrigen.TabIndex = 44;
            this.cboEmisor_RFC_BancoOrigen.SelectedIndexChanged += new System.EventHandler(this.cboEmisor_RFC_BancoOrigen_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(844, 120);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(122, 15);
            this.label18.TabIndex = 43;
            this.label18.Text = "Total de documentos :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalDocumentos
            // 
            this.lblTotalDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalDocumentos.Location = new System.Drawing.Point(1002, 118);
            this.lblTotalDocumentos.MostrarToolTip = false;
            this.lblTotalDocumentos.Name = "lblTotalDocumentos";
            this.lblTotalDocumentos.Size = new System.Drawing.Size(152, 20);
            this.lblTotalDocumentos.TabIndex = 42;
            this.lblTotalDocumentos.Text = "scLabelExt1";
            this.lblTotalDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnInformacionPago
            // 
            this.btnInformacionPago.Enabled = false;
            this.btnInformacionPago.Location = new System.Drawing.Point(1002, 142);
            this.btnInformacionPago.Margin = new System.Windows.Forms.Padding(2);
            this.btnInformacionPago.Name = "btnInformacionPago";
            this.btnInformacionPago.Size = new System.Drawing.Size(152, 20);
            this.btnInformacionPago.TabIndex = 12;
            this.btnInformacionPago.Text = "Información de SPEI";
            this.btnInformacionPago.UseVisualStyleBackColor = true;
            // 
            // btnReceptor_AgregarCuenta
            // 
            this.btnReceptor_AgregarCuenta.Location = new System.Drawing.Point(1122, 69);
            this.btnReceptor_AgregarCuenta.Margin = new System.Windows.Forms.Padding(2);
            this.btnReceptor_AgregarCuenta.Name = "btnReceptor_AgregarCuenta";
            this.btnReceptor_AgregarCuenta.Size = new System.Drawing.Size(32, 42);
            this.btnReceptor_AgregarCuenta.TabIndex = 9;
            this.btnReceptor_AgregarCuenta.Text = "...";
            this.btnReceptor_AgregarCuenta.UseVisualStyleBackColor = true;
            this.btnReceptor_AgregarCuenta.Click += new System.EventHandler(this.btnReceptor_AgregarCuenta_Click);
            // 
            // btnEmisor_AgregarCuenta
            // 
            this.btnEmisor_AgregarCuenta.Location = new System.Drawing.Point(1122, 20);
            this.btnEmisor_AgregarCuenta.Margin = new System.Windows.Forms.Padding(2);
            this.btnEmisor_AgregarCuenta.Name = "btnEmisor_AgregarCuenta";
            this.btnEmisor_AgregarCuenta.Size = new System.Drawing.Size(32, 45);
            this.btnEmisor_AgregarCuenta.TabIndex = 6;
            this.btnEmisor_AgregarCuenta.Text = "...";
            this.btnEmisor_AgregarCuenta.UseVisualStyleBackColor = true;
            this.btnEmisor_AgregarCuenta.Click += new System.EventHandler(this.btnEmisor_AgregarCuenta_Click);
            // 
            // cboReceptor_CuentaBeneficiario
            // 
            this.cboReceptor_CuentaBeneficiario.BackColorEnabled = System.Drawing.Color.White;
            this.cboReceptor_CuentaBeneficiario.Data = "";
            this.cboReceptor_CuentaBeneficiario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReceptor_CuentaBeneficiario.Filtro = " 1 = 1";
            this.cboReceptor_CuentaBeneficiario.FormattingEnabled = true;
            this.cboReceptor_CuentaBeneficiario.ListaItemsBusqueda = 20;
            this.cboReceptor_CuentaBeneficiario.Location = new System.Drawing.Point(641, 92);
            this.cboReceptor_CuentaBeneficiario.MostrarToolTip = false;
            this.cboReceptor_CuentaBeneficiario.Name = "cboReceptor_CuentaBeneficiario";
            this.cboReceptor_CuentaBeneficiario.Size = new System.Drawing.Size(476, 21);
            this.cboReceptor_CuentaBeneficiario.TabIndex = 8;
            // 
            // cboEmisor_CuentaOrdenante
            // 
            this.cboEmisor_CuentaOrdenante.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmisor_CuentaOrdenante.Data = "";
            this.cboEmisor_CuentaOrdenante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmisor_CuentaOrdenante.Filtro = " 1 = 1";
            this.cboEmisor_CuentaOrdenante.FormattingEnabled = true;
            this.cboEmisor_CuentaOrdenante.ListaItemsBusqueda = 20;
            this.cboEmisor_CuentaOrdenante.Location = new System.Drawing.Point(641, 43);
            this.cboEmisor_CuentaOrdenante.MostrarToolTip = false;
            this.cboEmisor_CuentaOrdenante.Name = "cboEmisor_CuentaOrdenante";
            this.cboEmisor_CuentaOrdenante.Size = new System.Drawing.Size(476, 21);
            this.cboEmisor_CuentaOrdenante.TabIndex = 5;
            // 
            // nmImportePago
            // 
            this.nmImportePago.DecimalPlaces = 4;
            this.nmImportePago.Location = new System.Drawing.Point(641, 118);
            this.nmImportePago.Margin = new System.Windows.Forms.Padding(2);
            this.nmImportePago.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nmImportePago.Name = "nmImportePago";
            this.nmImportePago.Size = new System.Drawing.Size(104, 20);
            this.nmImportePago.TabIndex = 10;
            this.nmImportePago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(517, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 15);
            this.label17.TabIndex = 41;
            this.label17.Text = "Monto del pago :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumeroDeOperacion
            // 
            this.txtNumeroDeOperacion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumeroDeOperacion.Decimales = 2;
            this.txtNumeroDeOperacion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumeroDeOperacion.ForeColor = System.Drawing.Color.Black;
            this.txtNumeroDeOperacion.Location = new System.Drawing.Point(641, 143);
            this.txtNumeroDeOperacion.MaxLength = 100;
            this.txtNumeroDeOperacion.Name = "txtNumeroDeOperacion";
            this.txtNumeroDeOperacion.PermitirApostrofo = false;
            this.txtNumeroDeOperacion.PermitirNegativos = false;
            this.txtNumeroDeOperacion.Size = new System.Drawing.Size(356, 20);
            this.txtNumeroDeOperacion.TabIndex = 11;
            this.txtNumeroDeOperacion.Text = "01234567890123456789012345678901234567890123456789";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(517, 144);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(122, 15);
            this.label16.TabIndex = 40;
            this.label16.Text = "Número de operación :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmisor_Beneficiario
            // 
            this.txtEmisor_Beneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEmisor_Beneficiario.Decimales = 2;
            this.txtEmisor_Beneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtEmisor_Beneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtEmisor_Beneficiario.Location = new System.Drawing.Point(802, 118);
            this.txtEmisor_Beneficiario.MaxLength = 50;
            this.txtEmisor_Beneficiario.Name = "txtEmisor_Beneficiario";
            this.txtEmisor_Beneficiario.PermitirApostrofo = false;
            this.txtEmisor_Beneficiario.PermitirNegativos = false;
            this.txtEmisor_Beneficiario.Size = new System.Drawing.Size(36, 20);
            this.txtEmisor_Beneficiario.TabIndex = 37;
            this.txtEmisor_Beneficiario.Visible = false;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(517, 95);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 15);
            this.label14.TabIndex = 38;
            this.label14.Text = "Cuenta beneficiario :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(517, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 15);
            this.label15.TabIndex = 36;
            this.label15.Text = "RFC banco receptor :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEmisor_CuentaOrdenante
            // 
            this.txtEmisor_CuentaOrdenante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEmisor_CuentaOrdenante.Decimales = 2;
            this.txtEmisor_CuentaOrdenante.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtEmisor_CuentaOrdenante.ForeColor = System.Drawing.Color.Black;
            this.txtEmisor_CuentaOrdenante.Location = new System.Drawing.Point(752, 118);
            this.txtEmisor_CuentaOrdenante.MaxLength = 50;
            this.txtEmisor_CuentaOrdenante.Name = "txtEmisor_CuentaOrdenante";
            this.txtEmisor_CuentaOrdenante.PermitirApostrofo = false;
            this.txtEmisor_CuentaOrdenante.PermitirNegativos = false;
            this.txtEmisor_CuentaOrdenante.Size = new System.Drawing.Size(44, 20);
            this.txtEmisor_CuentaOrdenante.TabIndex = 32;
            this.txtEmisor_CuentaOrdenante.Visible = false;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(517, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(122, 15);
            this.label13.TabIndex = 33;
            this.label13.Text = "Cuenta ordenante :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(517, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 15);
            this.label12.TabIndex = 31;
            this.label12.Text = "RFC banco emisor :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmProceso_LoadInformacion
            // 
            this.tmProceso_LoadInformacion.Enabled = true;
            this.tmProceso_LoadInformacion.Interval = 500;
            this.tmProceso_LoadInformacion.Tick += new System.EventHandler(this.tmProceso_LoadInformacion_Tick);
            // 
            // FrmCFDI_ComplementoDePago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 630);
            this.Controls.Add(this.FrameInformacionDelPago);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDetalles);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCFDI_ComplementoDePago";
            this.Text = "Registro de pagos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCFDI_ComplementoDePago_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUUIDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUUIDs_Sheet1)).EndInit();
            this.menuConceptos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.FrameInformacionDelPago.ResumeLayout(false);
            this.FrameInformacionDelPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmImportePago)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scComboBoxExt cboSeries;
        private SC_ControlsCS.scLabelExt lblSerie;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnObservacionesGral;
        private System.Windows.Forms.ContextMenuStrip menuConceptos;
        private System.Windows.Forms.ToolStripMenuItem btnAgregarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnModificarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarConcepto;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private SC_ControlsCS.scLabelExt lblArchivoExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboUsosCFDI;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevoExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAbrirExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLeerExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnBloquearHoja;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkTieneDocumentoRelacionado;
        private SC_ControlsCS.scTextBoxExt txtRelacion__UUID;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Serie;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Folio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaDePago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtHoraDePago;
        private SC_ControlsCS.scComboBoxExt cboFormasDePago;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scComboBoxExt cboMonedas;
        private System.Windows.Forms.GroupBox FrameInformacionDelPago;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scTextBoxExt txtEmisor_CuentaOrdenante;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtEmisor_Beneficiario;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private SC_ControlsCS.scTextBoxExt txtNumeroDeOperacion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nmImportePago;
        private SC_ControlsCS.scComboBoxExt cboReceptor_CuentaBeneficiario;
        private SC_ControlsCS.scComboBoxExt cboEmisor_CuentaOrdenante;
        private System.Windows.Forms.Button btnEmisor_AgregarCuenta;
        private System.Windows.Forms.Button btnReceptor_AgregarCuenta;
        private System.Windows.Forms.Button btnInformacionPago;
        private SC_ControlsCS.scLabelExt lblTotalDocumentos;
        private System.Windows.Forms.Label label18;
        private SC_ControlsCS.scComboBoxExt cboEmisor_RFC_BancoOrigen;
        private SC_ControlsCS.scComboBoxExt cboReceptor_RFC_BancoDestino;
        private FarPoint.Win.Spread.FpSpread grdUUIDs;
        private FarPoint.Win.Spread.SheetView grdUUIDs_Sheet1;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.CheckBox chkCargaManual;
        private System.Windows.Forms.Timer tmProceso_LoadInformacion;
    }
}