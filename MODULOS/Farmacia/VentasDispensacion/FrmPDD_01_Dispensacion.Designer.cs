namespace Farmacia.VentasDispensacion
{
    partial class FrmPDD_01_Dispensacion
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
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType2 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType3 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType4 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType5 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPDD_01_Dispensacion));
            this.DatosGenerales = new System.Windows.Forms.GroupBox();
            this.cboCliente = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTipoImpresion = new System.Windows.Forms.CheckBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetallesDispensacion = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVale = new System.Windows.Forms.ToolStripButton();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.btnDatosLotes = new System.Windows.Forms.Button();
            this.btnDatosNoSurtido = new System.Windows.Forms.Button();
            this.btnDatosServicioArea = new System.Windows.Forms.Button();
            this.btnDatosDiagnostico = new System.Windows.Forms.Button();
            this.btnDatosDocumento = new System.Windows.Forms.Button();
            this.btnDatosBeneficiario = new System.Windows.Forms.Button();
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.DatosGenerales.SuspendLayout();
            this.FrameDetallesDispensacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // DatosGenerales
            // 
            this.DatosGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DatosGenerales.Controls.Add(this.cboCliente);
            this.DatosGenerales.Controls.Add(this.label2);
            this.DatosGenerales.Controls.Add(this.chkTipoImpresion);
            this.DatosGenerales.Controls.Add(this.dtpFechaRegistro);
            this.DatosGenerales.Controls.Add(this.label3);
            this.DatosGenerales.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.DatosGenerales.Controls.Add(this.lblCancelado);
            this.DatosGenerales.Controls.Add(this.txtFolio);
            this.DatosGenerales.Controls.Add(this.label1);
            this.DatosGenerales.Location = new System.Drawing.Point(11, 29);
            this.DatosGenerales.Name = "DatosGenerales";
            this.DatosGenerales.Size = new System.Drawing.Size(821, 76);
            this.DatosGenerales.TabIndex = 0;
            this.DatosGenerales.TabStop = false;
            this.DatosGenerales.Text = "Datos Generales de Dispensación";
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.BackColorEnabled = System.Drawing.Color.White;
            this.cboCliente.Data = "";
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.Filtro = " 1 = 1";
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.ListaItemsBusqueda = 20;
            this.cboCliente.Location = new System.Drawing.Point(84, 42);
            this.cboCliente.MostrarToolTip = false;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(471, 21);
            this.cboCliente.TabIndex = 1;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTipoImpresion
            // 
            this.chkTipoImpresion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTipoImpresion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.Location = new System.Drawing.Point(561, 44);
            this.chkTipoImpresion.Name = "chkTipoImpresion";
            this.chkTipoImpresion.Size = new System.Drawing.Size(124, 17);
            this.chkTipoImpresion.TabIndex = 50;
            this.chkTipoImpresion.Text = "Imprimir a Detalle";
            this.chkTipoImpresion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTipoImpresion.UseVisualStyleBackColor = true;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(717, 15);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(619, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMostrarImpresionEnPantalla.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(684, 44);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(124, 17);
            this.chkMostrarImpresionEnPantalla.TabIndex = 47;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar Vista Previa impresión ";
            this.chkMostrarImpresionEnPantalla.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(209, 19);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(84, 19);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(119, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetallesDispensacion
            // 
            this.FrameDetallesDispensacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetallesDispensacion.Controls.Add(this.grdProductos);
            this.FrameDetallesDispensacion.Location = new System.Drawing.Point(11, 107);
            this.FrameDetallesDispensacion.Name = "FrameDetallesDispensacion";
            this.FrameDetallesDispensacion.Size = new System.Drawing.Size(821, 305);
            this.FrameDetallesDispensacion.TabIndex = 2;
            this.FrameDetallesDispensacion.TabStop = false;
            this.FrameDetallesDispensacion.Text = "Detalles de Dispensación";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(11, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(801, 284);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance);
            this.grdProductos.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdProductos_ButtonClicked);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 13;
            this.grdProductos_Sheet1.RowCount = 13;
            this.grdProductos_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 1).Locked = true;
            this.grdProductos_Sheet1.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(11, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(11, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(11, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(12, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(12, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Cells.Get(12, 8).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "TasaIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Precio";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Importe";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "ImporteIva";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "ImporteTotal";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Captura Por";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "EsIMach4";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Ultimo Costo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "Ver lotes";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 45F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Código Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 97F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Código";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 520F;
            numberCellType1.DecimalPlaces = 2;
            numberCellType1.MaximumValue = 100D;
            numberCellType1.MinimumValue = 0D;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "TasaIva";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 70F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = currencyCellType1;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Precio";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 90F;
            currencyCellType2.DecimalPlaces = 4;
            currencyCellType2.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            currencyCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = currencyCellType2;
            this.grdProductos_Sheet1.Columns.Get(6).Formula = "(RC[-2]*RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Importe";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 90F;
            currencyCellType3.DecimalPlaces = 4;
            currencyCellType3.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(7).CellType = currencyCellType3;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "((1+(RC[-4]/100))*RC[-1])-RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "ImporteIva";
            this.grdProductos_Sheet1.Columns.Get(7).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            currencyCellType4.DecimalPlaces = 4;
            currencyCellType4.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(8).CellType = currencyCellType4;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "RC[-2]+RC[-1]";
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "ImporteTotal";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 92F;
            this.grdProductos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Label = "Captura Por";
            this.grdProductos_Sheet1.Columns.Get(9).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(9).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(10).CellType = checkBoxCellType1;
            this.grdProductos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Label = "EsIMach4";
            this.grdProductos_Sheet1.Columns.Get(10).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(10).Visible = false;
            currencyCellType5.DecimalPlaces = 4;
            currencyCellType5.MinimumValue = new decimal(new int[] {
            0,
            0,
            0,
            262144});
            this.grdProductos_Sheet1.Columns.Get(11).CellType = currencyCellType5;
            this.grdProductos_Sheet1.Columns.Get(11).Label = "Ultimo Costo";
            this.grdProductos_Sheet1.Columns.Get(11).Visible = false;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "...";
            this.grdProductos_Sheet1.Columns.Get(12).CellType = buttonCellType1;
            this.grdProductos_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(12).Label = "Ver lotes";
            this.grdProductos_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.Rows.Default.Height = 22F;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnVale});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(842, 25);
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
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
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
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnVale
            // 
            this.btnVale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVale.Enabled = false;
            this.btnVale.Image = ((System.Drawing.Image)(resources.GetObject("btnVale.Image")));
            this.btnVale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVale.Name = "btnVale";
            this.btnVale.Size = new System.Drawing.Size(23, 22);
            this.btnVale.Text = "&Imprimir (CTRL + V)";
            this.btnVale.Visible = false;
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameInformacion.Controls.Add(this.btnDatosLotes);
            this.FrameInformacion.Controls.Add(this.btnDatosNoSurtido);
            this.FrameInformacion.Controls.Add(this.btnDatosServicioArea);
            this.FrameInformacion.Controls.Add(this.btnDatosDiagnostico);
            this.FrameInformacion.Controls.Add(this.btnDatosDocumento);
            this.FrameInformacion.Controls.Add(this.btnDatosBeneficiario);
            this.FrameInformacion.Location = new System.Drawing.Point(11, 414);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(821, 49);
            this.FrameInformacion.TabIndex = 9;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información Adicional";
            // 
            // btnDatosLotes
            // 
            this.btnDatosLotes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosLotes.Location = new System.Drawing.Point(551, 16);
            this.btnDatosLotes.Name = "btnDatosLotes";
            this.btnDatosLotes.Size = new System.Drawing.Size(127, 23);
            this.btnDatosLotes.TabIndex = 5;
            this.btnDatosLotes.Text = "[F7] Ver lotes";
            this.btnDatosLotes.UseVisualStyleBackColor = true;
            this.btnDatosLotes.Click += new System.EventHandler(this.btnDatosLotes_Click);
            // 
            // btnDatosNoSurtido
            // 
            this.btnDatosNoSurtido.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosNoSurtido.Location = new System.Drawing.Point(684, 16);
            this.btnDatosNoSurtido.Name = "btnDatosNoSurtido";
            this.btnDatosNoSurtido.Size = new System.Drawing.Size(127, 23);
            this.btnDatosNoSurtido.TabIndex = 4;
            this.btnDatosNoSurtido.Text = "[F9] No surtido";
            this.btnDatosNoSurtido.UseVisualStyleBackColor = true;
            this.btnDatosNoSurtido.Click += new System.EventHandler(this.btnDatosNoSurtido_Click);
            // 
            // btnDatosServicioArea
            // 
            this.btnDatosServicioArea.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosServicioArea.Location = new System.Drawing.Point(418, 16);
            this.btnDatosServicioArea.Name = "btnDatosServicioArea";
            this.btnDatosServicioArea.Size = new System.Drawing.Size(127, 23);
            this.btnDatosServicioArea.TabIndex = 3;
            this.btnDatosServicioArea.Text = "[F6] Servicio y área";
            this.btnDatosServicioArea.UseVisualStyleBackColor = true;
            this.btnDatosServicioArea.Click += new System.EventHandler(this.btnDatosServicioArea_Click);
            // 
            // btnDatosDiagnostico
            // 
            this.btnDatosDiagnostico.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosDiagnostico.Location = new System.Drawing.Point(143, 16);
            this.btnDatosDiagnostico.Name = "btnDatosDiagnostico";
            this.btnDatosDiagnostico.Size = new System.Drawing.Size(127, 23);
            this.btnDatosDiagnostico.TabIndex = 2;
            this.btnDatosDiagnostico.Text = "[F4] Diagnóstico";
            this.btnDatosDiagnostico.UseVisualStyleBackColor = true;
            this.btnDatosDiagnostico.Click += new System.EventHandler(this.btnDatosDiagnostico_Click);
            // 
            // btnDatosDocumento
            // 
            this.btnDatosDocumento.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosDocumento.Location = new System.Drawing.Point(10, 16);
            this.btnDatosDocumento.Name = "btnDatosDocumento";
            this.btnDatosDocumento.Size = new System.Drawing.Size(127, 23);
            this.btnDatosDocumento.TabIndex = 1;
            this.btnDatosDocumento.Text = "[F3] Documento";
            this.btnDatosDocumento.UseVisualStyleBackColor = true;
            this.btnDatosDocumento.Click += new System.EventHandler(this.btnDatosDocumento_Click);
            // 
            // btnDatosBeneficiario
            // 
            this.btnDatosBeneficiario.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDatosBeneficiario.Location = new System.Drawing.Point(276, 16);
            this.btnDatosBeneficiario.Name = "btnDatosBeneficiario";
            this.btnDatosBeneficiario.Size = new System.Drawing.Size(136, 23);
            this.btnDatosBeneficiario.TabIndex = 0;
            this.btnDatosBeneficiario.Text = "[F5] Beneficiario";
            this.btnDatosBeneficiario.UseVisualStyleBackColor = true;
            this.btnDatosBeneficiario.Click += new System.EventHandler(this.btnDatosBeneficiario_Click);
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // FrmPDD_01_Dispensacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 471);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.DatosGenerales);
            this.Controls.Add(this.FrameDetallesDispensacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmPDD_01_Dispensacion";
            this.Text = "Dispensación de Medicamentos y Material de Curación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPDD_01_Dispensacion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPDD_01_Dispensacion_KeyDown);
            this.DatosGenerales.ResumeLayout(false);
            this.DatosGenerales.PerformLayout();
            this.FrameDetallesDispensacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox DatosGenerales;
        private System.Windows.Forms.CheckBox chkTipoImpresion;
        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameDetallesDispensacion;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnVale;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboCliente;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private System.Windows.Forms.Button btnDatosServicioArea;
        private System.Windows.Forms.Button btnDatosDiagnostico;
        private System.Windows.Forms.Button btnDatosDocumento;
        private System.Windows.Forms.Button btnDatosBeneficiario;
        private System.Windows.Forms.Button btnDatosNoSurtido;
        private System.Windows.Forms.Button btnDatosLotes;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}