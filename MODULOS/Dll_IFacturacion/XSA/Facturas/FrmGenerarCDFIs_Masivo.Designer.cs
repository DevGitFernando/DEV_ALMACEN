namespace Dll_IFacturacion.XSA
{
    partial class FrmGenerarCDFIs_Masivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarCDFIs_Masivo));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClienteNombre = new SC_ControlsCS.scTextBoxExt();
            this.btlCliente = new System.Windows.Forms.Button();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal_Facturado = new SC_ControlsCS.scLabelExt();
            this.scLabelExt7 = new SC_ControlsCS.scLabelExt();
            this.lblIva_Facturado = new SC_ControlsCS.scLabelExt();
            this.lblTotal_Facturado = new SC_ControlsCS.scLabelExt();
            this.lbl_Proceso_01___TotalDocumentos = new SC_ControlsCS.scLabelExt();
            this.lbl_Proceso_02___DocumentosGenerados = new SC_ControlsCS.scLabelExt();
            this.lbl_Proceso_03___ErroresGenerados = new SC_ControlsCS.scLabelExt();
            this.chkMarcarDesmarcarTodo = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdRemisiones = new FarPoint.Win.Spread.FpSpread();
            this.grdRemisiones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal = new SC_ControlsCS.scLabelExt();
            this.scLabelExt6 = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameParametros = new System.Windows.Forms.GroupBox();
            this.chkAplicarMascara = new System.Windows.Forms.CheckBox();
            this.lblElapsedTime = new SC_ControlsCS.scLabelExt();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFolios = new System.Windows.Forms.CheckBox();
            this.FrameTipoRemision = new System.Windows.Forms.GroupBox();
            this.rdoRM_Todo = new System.Windows.Forms.RadioButton();
            this.rdoRM_Servicio = new System.Windows.Forms.RadioButton();
            this.rdoRM_Producto = new System.Windows.Forms.RadioButton();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.rdoOIN_Todos = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Consignacion = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Venta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoInsumoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.lblCte = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFinanciamiento = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTipoInsumo = new SC_ControlsCS.scComboBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkGenerarNotasDeCredito = new System.Windows.Forms.CheckBox();
            this.FrameAsociacionRemisiones = new System.Windows.Forms.GroupBox();
            this.rdoBaseRemision_AsociadaFactura = new System.Windows.Forms.RadioButton();
            this.rdoBaseRemision_Normal = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameParametros.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.FrameTipoRemision.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.FrameAsociacionRemisiones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtClienteNombre);
            this.groupBox1.Controls.Add(this.btlCliente);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(129, 19);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(450, 20);
            this.txtClienteNombre.TabIndex = 14;
            // 
            // btlCliente
            // 
            this.btlCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btlCliente.Image = ((System.Drawing.Image)(resources.GetObject("btlCliente.Image")));
            this.btlCliente.Location = new System.Drawing.Point(584, 11);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(29, 33);
            this.btlCliente.TabIndex = 13;
            this.btlCliente.Text = "...";
            this.btlCliente.UseVisualStyleBackColor = true;
            this.btlCliente.Click += new System.EventHandler(this.btlCliente_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(6, 20);
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
            this.txtId.Location = new System.Drawing.Point(64, 19);
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
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.scLabelExt1);
            this.FrameDetalles.Controls.Add(this.scLabelExt2);
            this.FrameDetalles.Controls.Add(this.lblSubTotal_Facturado);
            this.FrameDetalles.Controls.Add(this.scLabelExt7);
            this.FrameDetalles.Controls.Add(this.lblIva_Facturado);
            this.FrameDetalles.Controls.Add(this.lblTotal_Facturado);
            this.FrameDetalles.Controls.Add(this.lbl_Proceso_01___TotalDocumentos);
            this.FrameDetalles.Controls.Add(this.lbl_Proceso_02___DocumentosGenerados);
            this.FrameDetalles.Controls.Add(this.lbl_Proceso_03___ErroresGenerados);
            this.FrameDetalles.Controls.Add(this.chkMarcarDesmarcarTodo);
            this.FrameDetalles.Controls.Add(this.FrameProceso);
            this.FrameDetalles.Controls.Add(this.grdRemisiones);
            this.FrameDetalles.Controls.Add(this.scLabelExt4);
            this.FrameDetalles.Controls.Add(this.scLabelExt5);
            this.FrameDetalles.Controls.Add(this.lblSubTotal);
            this.FrameDetalles.Controls.Add(this.scLabelExt6);
            this.FrameDetalles.Controls.Add(this.lblIva);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 263);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(994, 439);
            this.FrameDetalles.TabIndex = 6;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la Factura";
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt1.Location = new System.Drawing.Point(410, 407);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt1.TabIndex = 33;
            this.scLabelExt1.Text = "Total facturado :";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt2.Location = new System.Drawing.Point(410, 383);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt2.TabIndex = 32;
            this.scLabelExt2.Text = "Iva facturado :";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal_Facturado
            // 
            this.lblSubTotal_Facturado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal_Facturado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal_Facturado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal_Facturado.Location = new System.Drawing.Point(553, 358);
            this.lblSubTotal_Facturado.MostrarToolTip = false;
            this.lblSubTotal_Facturado.Name = "lblSubTotal_Facturado";
            this.lblSubTotal_Facturado.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal_Facturado.TabIndex = 28;
            this.lblSubTotal_Facturado.Text = "scLabelExt1";
            this.lblSubTotal_Facturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt7
            // 
            this.scLabelExt7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt7.Location = new System.Drawing.Point(410, 358);
            this.scLabelExt7.MostrarToolTip = false;
            this.scLabelExt7.Name = "scLabelExt7";
            this.scLabelExt7.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt7.TabIndex = 31;
            this.scLabelExt7.Text = "Sub-Total facturado :";
            this.scLabelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva_Facturado
            // 
            this.lblIva_Facturado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva_Facturado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva_Facturado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva_Facturado.Location = new System.Drawing.Point(553, 383);
            this.lblIva_Facturado.MostrarToolTip = false;
            this.lblIva_Facturado.Name = "lblIva_Facturado";
            this.lblIva_Facturado.Size = new System.Drawing.Size(137, 22);
            this.lblIva_Facturado.TabIndex = 29;
            this.lblIva_Facturado.Text = "scLabelExt2";
            this.lblIva_Facturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal_Facturado
            // 
            this.lblTotal_Facturado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal_Facturado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal_Facturado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_Facturado.Location = new System.Drawing.Point(553, 407);
            this.lblTotal_Facturado.MostrarToolTip = false;
            this.lblTotal_Facturado.Name = "lblTotal_Facturado";
            this.lblTotal_Facturado.Size = new System.Drawing.Size(137, 22);
            this.lblTotal_Facturado.TabIndex = 30;
            this.lblTotal_Facturado.Text = "scLabelExt3";
            this.lblTotal_Facturado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Proceso_01___TotalDocumentos
            // 
            this.lbl_Proceso_01___TotalDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Proceso_01___TotalDocumentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Proceso_01___TotalDocumentos.Location = new System.Drawing.Point(10, 358);
            this.lbl_Proceso_01___TotalDocumentos.MostrarToolTip = false;
            this.lbl_Proceso_01___TotalDocumentos.Name = "lbl_Proceso_01___TotalDocumentos";
            this.lbl_Proceso_01___TotalDocumentos.Size = new System.Drawing.Size(339, 22);
            this.lbl_Proceso_01___TotalDocumentos.TabIndex = 25;
            this.lbl_Proceso_01___TotalDocumentos.Text = "scLabelExt1";
            this.lbl_Proceso_01___TotalDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Proceso_02___DocumentosGenerados
            // 
            this.lbl_Proceso_02___DocumentosGenerados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Proceso_02___DocumentosGenerados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Proceso_02___DocumentosGenerados.Location = new System.Drawing.Point(10, 383);
            this.lbl_Proceso_02___DocumentosGenerados.MostrarToolTip = false;
            this.lbl_Proceso_02___DocumentosGenerados.Name = "lbl_Proceso_02___DocumentosGenerados";
            this.lbl_Proceso_02___DocumentosGenerados.Size = new System.Drawing.Size(339, 22);
            this.lbl_Proceso_02___DocumentosGenerados.TabIndex = 26;
            this.lbl_Proceso_02___DocumentosGenerados.Text = "scLabelExt2";
            this.lbl_Proceso_02___DocumentosGenerados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Proceso_03___ErroresGenerados
            // 
            this.lbl_Proceso_03___ErroresGenerados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Proceso_03___ErroresGenerados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Proceso_03___ErroresGenerados.Location = new System.Drawing.Point(10, 407);
            this.lbl_Proceso_03___ErroresGenerados.MostrarToolTip = false;
            this.lbl_Proceso_03___ErroresGenerados.Name = "lbl_Proceso_03___ErroresGenerados";
            this.lbl_Proceso_03___ErroresGenerados.Size = new System.Drawing.Size(339, 22);
            this.lbl_Proceso_03___ErroresGenerados.TabIndex = 27;
            this.lbl_Proceso_03___ErroresGenerados.Text = "scLabelExt3";
            this.lbl_Proceso_03___ErroresGenerados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMarcarDesmarcarTodo
            // 
            this.chkMarcarDesmarcarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.Location = new System.Drawing.Point(803, 0);
            this.chkMarcarDesmarcarTodo.Name = "chkMarcarDesmarcarTodo";
            this.chkMarcarDesmarcarTodo.Size = new System.Drawing.Size(181, 17);
            this.chkMarcarDesmarcarTodo.TabIndex = 24;
            this.chkMarcarDesmarcarTodo.Text = "Marcar - Desmarcar todo";
            this.chkMarcarDesmarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcarTodo_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(162, 187);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 66);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 28);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdRemisiones
            // 
            this.grdRemisiones.AccessibleDescription = "grdRemisiones, Sheet1, Row 0, Column 0, ";
            this.grdRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRemisiones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdRemisiones.Location = new System.Drawing.Point(10, 19);
            this.grdRemisiones.Name = "grdRemisiones";
            this.grdRemisiones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdRemisiones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdRemisiones_Sheet1});
            this.grdRemisiones.Size = new System.Drawing.Size(976, 327);
            this.grdRemisiones.TabIndex = 23;
            // 
            // grdRemisiones_Sheet1
            // 
            this.grdRemisiones_Sheet1.Reset();
            this.grdRemisiones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdRemisiones_Sheet1.ColumnCount = 10;
            this.grdRemisiones_Sheet1.RowCount = 14;
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fuente de financiamiento";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Sub-Total";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "IVA";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Importe ";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Serie";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Folio";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Facturar";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Procesado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Mensaje";
            this.grdRemisiones_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdRemisiones_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdRemisiones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Label = "Remisión";
            this.grdRemisiones_Sheet1.Columns.Get(0).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Width = 90F;
            this.grdRemisiones_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdRemisiones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(1).Label = "Fuente de financiamiento";
            this.grdRemisiones_Sheet1.Columns.Get(1).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(1).Width = 330F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdRemisiones_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdRemisiones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRemisiones_Sheet1.Columns.Get(2).Label = "Sub-Total";
            this.grdRemisiones_Sheet1.Columns.Get(2).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(2).Width = 85F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdRemisiones_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grdRemisiones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRemisiones_Sheet1.Columns.Get(3).Label = "IVA";
            this.grdRemisiones_Sheet1.Columns.Get(3).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(3).Width = 85F;
            numberCellType3.DecimalPlaces = 4;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdRemisiones_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grdRemisiones_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRemisiones_Sheet1.Columns.Get(4).Label = "Importe ";
            this.grdRemisiones_Sheet1.Columns.Get(4).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(4).Width = 85F;
            this.grdRemisiones_Sheet1.Columns.Get(5).CellType = textCellType3;
            this.grdRemisiones_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(5).Label = "Serie";
            this.grdRemisiones_Sheet1.Columns.Get(5).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(5).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(6).CellType = textCellType4;
            this.grdRemisiones_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(6).Label = "Folio";
            this.grdRemisiones_Sheet1.Columns.Get(6).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(6).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(7).CellType = checkBoxCellType1;
            this.grdRemisiones_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(7).Label = "Facturar";
            this.grdRemisiones_Sheet1.Columns.Get(7).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(7).Width = 65F;
            this.grdRemisiones_Sheet1.Columns.Get(8).CellType = checkBoxCellType2;
            this.grdRemisiones_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(8).Label = "Procesado";
            this.grdRemisiones_Sheet1.Columns.Get(8).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(8).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(9).CellType = textCellType5;
            this.grdRemisiones_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(9).Label = "Mensaje";
            this.grdRemisiones_Sheet1.Columns.Get(9).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(9).Width = 150F;
            this.grdRemisiones_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt4.Location = new System.Drawing.Point(705, 407);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt4.TabIndex = 22;
            this.scLabelExt4.Text = "Total :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt5.Location = new System.Drawing.Point(705, 383);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt5.TabIndex = 21;
            this.scLabelExt5.Text = "Iva :";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(848, 358);
            this.lblSubTotal.MostrarToolTip = false;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal.TabIndex = 14;
            this.lblSubTotal.Text = "scLabelExt1";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt6
            // 
            this.scLabelExt6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt6.Location = new System.Drawing.Point(705, 358);
            this.scLabelExt6.MostrarToolTip = false;
            this.scLabelExt6.Name = "scLabelExt6";
            this.scLabelExt6.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt6.TabIndex = 20;
            this.scLabelExt6.Text = "Sub-Total :";
            this.scLabelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(848, 383);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(137, 22);
            this.lblIva.TabIndex = 18;
            this.lblIva.Text = "scLabelExt2";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(848, 407);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(637, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 49);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(7, 20);
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
            this.cboSeries.Location = new System.Drawing.Point(48, 20);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(118, 21);
            this.cboSeries.TabIndex = 0;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1016, 25);
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
            this.btnEjecutar.Text = "Consultar remisiones sin facturar";
            this.btnEjecutar.ToolTipText = "Consultar remisiones sin facturar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar facturas electrónicas";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // tmProceso
            // 
            this.tmProceso.Enabled = true;
            this.tmProceso.Interval = 500;
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnPago);
            this.groupBox2.Controls.Add(this.btnObservacionesGral);
            this.groupBox2.Location = new System.Drawing.Point(818, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 49);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(108, 17);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(75, 23);
            this.btnPago.TabIndex = 21;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(14, 17);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(91, 23);
            this.btnObservacionesGral.TabIndex = 20;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(12, 77);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(619, 45);
            this.FrameDirectorioDeTrabajo.TabIndex = 4;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(583, 15);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(28, 23);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(11, 16);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(566, 21);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameParametros
            // 
            this.FrameParametros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameParametros.Controls.Add(this.chkAplicarMascara);
            this.FrameParametros.Location = new System.Drawing.Point(637, 77);
            this.FrameParametros.Margin = new System.Windows.Forms.Padding(2);
            this.FrameParametros.Name = "FrameParametros";
            this.FrameParametros.Padding = new System.Windows.Forms.Padding(2);
            this.FrameParametros.Size = new System.Drawing.Size(176, 45);
            this.FrameParametros.TabIndex = 5;
            this.FrameParametros.TabStop = false;
            this.FrameParametros.Text = "Parámetros";
            // 
            // chkAplicarMascara
            // 
            this.chkAplicarMascara.Location = new System.Drawing.Point(19, 19);
            this.chkAplicarMascara.Margin = new System.Windows.Forms.Padding(2);
            this.chkAplicarMascara.Name = "chkAplicarMascara";
            this.chkAplicarMascara.Size = new System.Drawing.Size(124, 19);
            this.chkAplicarMascara.TabIndex = 0;
            this.chkAplicarMascara.Text = "Aplicar mascaras";
            this.chkAplicarMascara.UseVisualStyleBackColor = true;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblElapsedTime.BackColor = System.Drawing.Color.Transparent;
            this.lblElapsedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.Location = new System.Drawing.Point(733, 2);
            this.lblElapsedTime.MostrarToolTip = false;
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(274, 16);
            this.lblElapsedTime.TabIndex = 34;
            this.lblElapsedTime.Text = "Sub-Total facturado :";
            this.lblElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFolios.Controls.Add(this.txtFolioFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFolioInicial);
            this.FrameFolios.Controls.Add(this.label4);
            this.FrameFolios.Controls.Add(this.chkFolios);
            this.FrameFolios.Location = new System.Drawing.Point(440, 209);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(296, 50);
            this.FrameFolios.TabIndex = 12;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(203, 19);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(85, 20);
            this.txtFolioFinal.TabIndex = 2;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(153, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "Hasta :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicial
            // 
            this.txtFolioInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicial.Decimales = 2;
            this.txtFolioInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicial.Location = new System.Drawing.Point(61, 19);
            this.txtFolioInicial.MaxLength = 8;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(85, 20);
            this.txtFolioInicial.TabIndex = 1;
            this.txtFolioInicial.Text = "01234567";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Desde :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFolios
            // 
            this.chkFolios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.Location = new System.Drawing.Point(188, 0);
            this.chkFolios.Name = "chkFolios";
            this.chkFolios.Size = new System.Drawing.Size(100, 17);
            this.chkFolios.TabIndex = 0;
            this.chkFolios.Text = "Filtro por Folios";
            this.chkFolios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.UseVisualStyleBackColor = true;
            // 
            // FrameTipoRemision
            // 
            this.FrameTipoRemision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Todo);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Servicio);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Producto);
            this.FrameTipoRemision.Location = new System.Drawing.Point(440, 124);
            this.FrameTipoRemision.Name = "FrameTipoRemision";
            this.FrameTipoRemision.Size = new System.Drawing.Size(106, 81);
            this.FrameTipoRemision.TabIndex = 8;
            this.FrameTipoRemision.TabStop = false;
            this.FrameTipoRemision.Text = "Tipo de remisión";
            // 
            // rdoRM_Todo
            // 
            this.rdoRM_Todo.Location = new System.Drawing.Point(16, 56);
            this.rdoRM_Todo.Name = "rdoRM_Todo";
            this.rdoRM_Todo.Size = new System.Drawing.Size(68, 18);
            this.rdoRM_Todo.TabIndex = 22;
            this.rdoRM_Todo.TabStop = true;
            this.rdoRM_Todo.Text = "Ambos";
            this.rdoRM_Todo.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Servicio
            // 
            this.rdoRM_Servicio.Location = new System.Drawing.Point(16, 38);
            this.rdoRM_Servicio.Name = "rdoRM_Servicio";
            this.rdoRM_Servicio.Size = new System.Drawing.Size(68, 17);
            this.rdoRM_Servicio.TabIndex = 1;
            this.rdoRM_Servicio.TabStop = true;
            this.rdoRM_Servicio.Text = "Servicio";
            this.rdoRM_Servicio.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Producto
            // 
            this.rdoRM_Producto.Location = new System.Drawing.Point(16, 20);
            this.rdoRM_Producto.Name = "rdoRM_Producto";
            this.rdoRM_Producto.Size = new System.Drawing.Size(68, 17);
            this.rdoRM_Producto.TabIndex = 0;
            this.rdoRM_Producto.TabStop = true;
            this.rdoRM_Producto.Text = "Producto";
            this.rdoRM_Producto.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Todos);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Consignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Venta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(551, 124);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(116, 81);
            this.FrameOrigenInsumo.TabIndex = 9;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Origen de Insumos";
            // 
            // rdoOIN_Todos
            // 
            this.rdoOIN_Todos.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.rdoOIN_Todos.Location = new System.Drawing.Point(16, 56);
            this.rdoOIN_Todos.Name = "rdoOIN_Todos";
            this.rdoOIN_Todos.Size = new System.Drawing.Size(92, 18);
            this.rdoOIN_Todos.TabIndex = 22;
            this.rdoOIN_Todos.TabStop = true;
            this.rdoOIN_Todos.Text = "Ambos";
            this.rdoOIN_Todos.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Consignacion
            // 
            this.rdoOIN_Consignacion.Location = new System.Drawing.Point(16, 38);
            this.rdoOIN_Consignacion.Name = "rdoOIN_Consignacion";
            this.rdoOIN_Consignacion.Size = new System.Drawing.Size(92, 17);
            this.rdoOIN_Consignacion.TabIndex = 1;
            this.rdoOIN_Consignacion.TabStop = true;
            this.rdoOIN_Consignacion.Text = "Consignación";
            this.rdoOIN_Consignacion.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Venta
            // 
            this.rdoOIN_Venta.Location = new System.Drawing.Point(16, 19);
            this.rdoOIN_Venta.Name = "rdoOIN_Venta";
            this.rdoOIN_Venta.Size = new System.Drawing.Size(92, 18);
            this.rdoOIN_Venta.TabIndex = 0;
            this.rdoOIN_Venta.TabStop = true;
            this.rdoOIN_Venta.Text = "Venta";
            this.rdoOIN_Venta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoAmbos);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(671, 124);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(142, 81);
            this.FrameTipoInsumo.TabIndex = 10;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoInsumoAmbos
            // 
            this.rdoInsumoAmbos.Location = new System.Drawing.Point(15, 56);
            this.rdoInsumoAmbos.Name = "rdoInsumoAmbos";
            this.rdoInsumoAmbos.Size = new System.Drawing.Size(60, 18);
            this.rdoInsumoAmbos.TabIndex = 2;
            this.rdoInsumoAmbos.TabStop = true;
            this.rdoInsumoAmbos.Text = "Ambos";
            this.rdoInsumoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMaterialDeCuracion
            // 
            this.rdoInsumoMaterialDeCuracion.Location = new System.Drawing.Point(15, 38);
            this.rdoInsumoMaterialDeCuracion.Name = "rdoInsumoMaterialDeCuracion";
            this.rdoInsumoMaterialDeCuracion.Size = new System.Drawing.Size(119, 17);
            this.rdoInsumoMaterialDeCuracion.TabIndex = 1;
            this.rdoInsumoMaterialDeCuracion.TabStop = true;
            this.rdoInsumoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoInsumoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamento
            // 
            this.rdoInsumoMedicamento.Location = new System.Drawing.Point(15, 20);
            this.rdoInsumoMedicamento.Name = "rdoInsumoMedicamento";
            this.rdoInsumoMedicamento.Size = new System.Drawing.Size(95, 17);
            this.rdoInsumoMedicamento.TabIndex = 0;
            this.rdoInsumoMedicamento.TabStop = true;
            this.rdoInsumoMedicamento.Text = "Medicamento";
            this.rdoInsumoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFechaDeProceso.Controls.Add(this.chkFechas);
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(742, 209);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(263, 50);
            this.FrameFechaDeProceso.TabIndex = 13;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Emisión de remisiones";
            // 
            // chkFechas
            // 
            this.chkFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.Location = new System.Drawing.Point(149, 0);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(104, 15);
            this.chkFechas.TabIndex = 0;
            this.chkFechas.Text = "Filtro de Fechas";
            this.chkFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(130, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(7, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Desde :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(177, 20);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(71, 20);
            this.dtpFechaFinal.TabIndex = 2;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(54, 20);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(71, 20);
            this.dtpFechaInicial.TabIndex = 1;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Controls.Add(this.txtCte);
            this.FrameDatosOperacion.Controls.Add(this.lblCte);
            this.FrameDatosOperacion.Controls.Add(this.lblSubCte);
            this.FrameDatosOperacion.Controls.Add(this.txtSubCte);
            this.FrameDatosOperacion.Controls.Add(this.label5);
            this.FrameDatosOperacion.Controls.Add(this.cboFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label2);
            this.FrameDatosOperacion.Controls.Add(this.cboTipoInsumo);
            this.FrameDatosOperacion.Controls.Add(this.label7);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(12, 124);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(422, 134);
            this.FrameDatosOperacion.TabIndex = 7;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(97, 23);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(55, 20);
            this.txtCte.TabIndex = 41;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCte
            // 
            this.lblCte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(157, 21);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(253, 21);
            this.lblCte.TabIndex = 44;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubCte
            // 
            this.lblSubCte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(157, 46);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(253, 21);
            this.lblSubCte.TabIndex = 46;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(97, 48);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(55, 20);
            this.txtSubCte.TabIndex = 42;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFinanciamiento
            // 
            this.cboFinanciamiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFinanciamiento.BackColorEnabled = System.Drawing.Color.White;
            this.cboFinanciamiento.Data = "";
            this.cboFinanciamiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFinanciamiento.Filtro = " 1 = 1";
            this.cboFinanciamiento.FormattingEnabled = true;
            this.cboFinanciamiento.ListaItemsBusqueda = 20;
            this.cboFinanciamiento.Location = new System.Drawing.Point(98, 76);
            this.cboFinanciamiento.MostrarToolTip = false;
            this.cboFinanciamiento.Name = "cboFinanciamiento";
            this.cboFinanciamiento.Size = new System.Drawing.Size(313, 21);
            this.cboFinanciamiento.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Financiamiento :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoInsumo
            // 
            this.cboTipoInsumo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTipoInsumo.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoInsumo.Data = "";
            this.cboTipoInsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoInsumo.Filtro = " 1 = 1";
            this.cboTipoInsumo.FormattingEnabled = true;
            this.cboTipoInsumo.ListaItemsBusqueda = 20;
            this.cboTipoInsumo.Location = new System.Drawing.Point(98, 103);
            this.cboTipoInsumo.MostrarToolTip = false;
            this.cboTipoInsumo.Name = "cboTipoInsumo";
            this.cboTipoInsumo.Size = new System.Drawing.Size(313, 21);
            this.cboTipoInsumo.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Tipo :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkGenerarNotasDeCredito);
            this.groupBox4.Location = new System.Drawing.Point(818, 77);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(191, 45);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Notas de crédito";
            // 
            // chkGenerarNotasDeCredito
            // 
            this.chkGenerarNotasDeCredito.Location = new System.Drawing.Point(19, 19);
            this.chkGenerarNotasDeCredito.Margin = new System.Windows.Forms.Padding(2);
            this.chkGenerarNotasDeCredito.Name = "chkGenerarNotasDeCredito";
            this.chkGenerarNotasDeCredito.Size = new System.Drawing.Size(170, 19);
            this.chkGenerarNotasDeCredito.TabIndex = 0;
            this.chkGenerarNotasDeCredito.Text = "Generar notas de crédito";
            this.chkGenerarNotasDeCredito.UseVisualStyleBackColor = true;
            // 
            // FrameAsociacionRemisiones
            // 
            this.FrameAsociacionRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameAsociacionRemisiones.Controls.Add(this.rdoBaseRemision_AsociadaFactura);
            this.FrameAsociacionRemisiones.Controls.Add(this.rdoBaseRemision_Normal);
            this.FrameAsociacionRemisiones.Location = new System.Drawing.Point(818, 124);
            this.FrameAsociacionRemisiones.Margin = new System.Windows.Forms.Padding(2);
            this.FrameAsociacionRemisiones.Name = "FrameAsociacionRemisiones";
            this.FrameAsociacionRemisiones.Padding = new System.Windows.Forms.Padding(2);
            this.FrameAsociacionRemisiones.Size = new System.Drawing.Size(190, 81);
            this.FrameAsociacionRemisiones.TabIndex = 11;
            this.FrameAsociacionRemisiones.TabStop = false;
            this.FrameAsociacionRemisiones.Text = "Base remisión";
            // 
            // rdoBaseRemision_AsociadaFactura
            // 
            this.rdoBaseRemision_AsociadaFactura.Location = new System.Drawing.Point(18, 41);
            this.rdoBaseRemision_AsociadaFactura.Name = "rdoBaseRemision_AsociadaFactura";
            this.rdoBaseRemision_AsociadaFactura.Size = new System.Drawing.Size(153, 17);
            this.rdoBaseRemision_AsociadaFactura.TabIndex = 3;
            this.rdoBaseRemision_AsociadaFactura.TabStop = true;
            this.rdoBaseRemision_AsociadaFactura.Text = "Asociada a facturas";
            this.rdoBaseRemision_AsociadaFactura.UseVisualStyleBackColor = true;
            this.rdoBaseRemision_AsociadaFactura.CheckedChanged += new System.EventHandler(this.rdoBaseRemision_AsociadaFactura_CheckedChanged);
            // 
            // rdoBaseRemision_Normal
            // 
            this.rdoBaseRemision_Normal.Location = new System.Drawing.Point(18, 23);
            this.rdoBaseRemision_Normal.Name = "rdoBaseRemision_Normal";
            this.rdoBaseRemision_Normal.Size = new System.Drawing.Size(129, 17);
            this.rdoBaseRemision_Normal.TabIndex = 2;
            this.rdoBaseRemision_Normal.TabStop = true;
            this.rdoBaseRemision_Normal.Text = "Normal";
            this.rdoBaseRemision_Normal.UseVisualStyleBackColor = true;
            this.rdoBaseRemision_Normal.CheckedChanged += new System.EventHandler(this.rdoBaseRemision_Normal_CheckedChanged);
            // 
            // FrmGenerarCDFIs_Masivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1016, 709);
            this.Controls.Add(this.FrameAsociacionRemisiones);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoRemision);
            this.Controls.Add(this.FrameOrigenInsumo);
            this.Controls.Add(this.FrameTipoInsumo);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.lblElapsedTime);
            this.Controls.Add(this.FrameParametros);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDetalles);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmGenerarCDFIs_Masivo";
            this.Text = "Facturación de remisiones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGenerarCDFIs_Masivo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameParametros.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.FrameTipoRemision.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.FrameAsociacionRemisiones.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnObservacionesGral;
        private SC_ControlsCS.scTextBoxExt txtClienteNombre;
        private System.Windows.Forms.Button btnPago;
        private SC_ControlsCS.scLabelExt lblSubTotal;
        private SC_ControlsCS.scLabelExt lblIva;
        private SC_ControlsCS.scLabelExt lblTotal;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt scLabelExt5;
        private SC_ControlsCS.scLabelExt scLabelExt6;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private FarPoint.Win.Spread.FpSpread grdRemisiones;
        private FarPoint.Win.Spread.SheetView grdRemisiones_Sheet1;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcarTodo;
        private System.Windows.Forms.GroupBox FrameParametros;
        private System.Windows.Forms.CheckBox chkAplicarMascara;
        private SC_ControlsCS.scLabelExt lbl_Proceso_01___TotalDocumentos;
        private SC_ControlsCS.scLabelExt lbl_Proceso_02___DocumentosGenerados;
        private SC_ControlsCS.scLabelExt lbl_Proceso_03___ErroresGenerados;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblSubTotal_Facturado;
        private SC_ControlsCS.scLabelExt scLabelExt7;
        private SC_ControlsCS.scLabelExt lblIva_Facturado;
        private SC_ControlsCS.scLabelExt lblTotal_Facturado;
        private SC_ControlsCS.scLabelExt lblElapsedTime;
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFolios;
        private System.Windows.Forms.GroupBox FrameTipoRemision;
        private System.Windows.Forms.RadioButton rdoRM_Todo;
        private System.Windows.Forms.RadioButton rdoRM_Servicio;
        private System.Windows.Forms.RadioButton rdoRM_Producto;
        private System.Windows.Forms.GroupBox FrameOrigenInsumo;
        private System.Windows.Forms.RadioButton rdoOIN_Todos;
        private System.Windows.Forms.RadioButton rdoOIN_Consignacion;
        private System.Windows.Forms.RadioButton rdoOIN_Venta;
        private System.Windows.Forms.GroupBox FrameTipoInsumo;
        private System.Windows.Forms.RadioButton rdoInsumoAmbos;
        private System.Windows.Forms.RadioButton rdoInsumoMaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdoInsumoMedicamento;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label lblCte;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboFinanciamiento;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboTipoInsumo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkGenerarNotasDeCredito;
        private System.Windows.Forms.GroupBox FrameAsociacionRemisiones;
        private System.Windows.Forms.RadioButton rdoBaseRemision_AsociadaFactura;
        private System.Windows.Forms.RadioButton rdoBaseRemision_Normal;
    }
}