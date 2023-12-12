namespace OficinaCentral.Catalogos
{
    partial class FrmProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Picture picture2 = new FarPoint.Win.Picture(null, FarPoint.Win.RenderStyle.Normal, System.Drawing.Color.Empty, 0, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center);
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType2 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPaqueteDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.btnKarrusel_Imagenes = new System.Windows.Forms.Button();
            this.chkEsSectorSalud = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.btnPresentacion = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cboSegmentos = new SC_ControlsCS.scComboBoxExt();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.txtIdLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.txtClaveInternaSal = new SC_ControlsCS.scTextBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.lblDescripcionSal = new System.Windows.Forms.Label();
            this.lblClaveSal = new System.Windows.Forms.Label();
            this.chkMedicamento = new System.Windows.Forms.CheckBox();
            this.chkCodigoEAN = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkDescomponer = new System.Windows.Forms.CheckBox();
            this.txtContenido = new SC_ControlsCS.scIntegerTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPresentaciones = new SC_ControlsCS.scComboBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSubFamilias = new SC_ControlsCS.scComboBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.cboFamilias = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipoProductos = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboClasificaciones = new SC_ControlsCS.scComboBoxExt();
            this.txtDescripcionCorta = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabCatProductos = new System.Windows.Forms.TabControl();
            this.tabProductos = new System.Windows.Forms.TabPage();
            this.tabEstadosCodigos = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdEstadosProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdEstadosProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdCodigosEAN = new FarPoint.Win.Spread.FpSpread();
            this.grdCodigosEAN_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabPrecios = new System.Windows.Forms.TabPage();
            this.FrameDatosPrecio = new System.Windows.Forms.GroupBox();
            this.txtDescuento = new SC_ControlsCS.scNumericTextBox();
            this.txtPrecioMaximo = new SC_ControlsCS.scNumericTextBox();
            this.txtUtilidad = new SC_ControlsCS.scNumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMargenMininoUtilidad_02_Superior = new SC_ControlsCS.scNumericTextBox();
            this.txtMargenMininoUtilidad_01_Inferior = new SC_ControlsCS.scNumericTextBox();
            this.txtPorcentaje_CopagoColaborador = new SC_ControlsCS.scNumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProducto.SuspendLayout();
            this.tabCatProductos.SuspendLayout();
            this.tabProductos.SuspendLayout();
            this.tabEstadosCodigos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadosProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadosProductos_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).BeginInit();
            this.tabPrecios.SuspendLayout();
            this.FrameDatosPrecio.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnGenerarPaqueteDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(834, 25);
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
            this.btnNuevo.ToolTipText = "[F5] Nuevo";
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
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.ToolTipText = "[F6] Guardar";
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "[F8] Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.ToolTipText = "[F10] Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarPaqueteDatos
            // 
            this.btnGenerarPaqueteDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPaqueteDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPaqueteDatos.Image")));
            this.btnGenerarPaqueteDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPaqueteDatos.Name = "btnGenerarPaqueteDatos";
            this.btnGenerarPaqueteDatos.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPaqueteDatos.Text = "Generar Paquete de Datos";
            this.btnGenerarPaqueteDatos.Click += new System.EventHandler(this.btnGenerarPaqueteDatos_Click);
            // 
            // FrameProducto
            // 
            this.FrameProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameProducto.Controls.Add(this.btnKarrusel_Imagenes);
            this.FrameProducto.Controls.Add(this.chkEsSectorSalud);
            this.FrameProducto.Controls.Add(this.label13);
            this.FrameProducto.Controls.Add(this.txtCodigoEAN);
            this.FrameProducto.Controls.Add(this.btnPresentacion);
            this.FrameProducto.Controls.Add(this.label12);
            this.FrameProducto.Controls.Add(this.cboSegmentos);
            this.FrameProducto.Controls.Add(this.lblLaboratorio);
            this.FrameProducto.Controls.Add(this.txtIdLaboratorio);
            this.FrameProducto.Controls.Add(this.txtClaveInternaSal);
            this.FrameProducto.Controls.Add(this.label14);
            this.FrameProducto.Controls.Add(this.lblDescripcionSal);
            this.FrameProducto.Controls.Add(this.lblClaveSal);
            this.FrameProducto.Controls.Add(this.chkMedicamento);
            this.FrameProducto.Controls.Add(this.chkCodigoEAN);
            this.FrameProducto.Controls.Add(this.label11);
            this.FrameProducto.Controls.Add(this.chkDescomponer);
            this.FrameProducto.Controls.Add(this.txtContenido);
            this.FrameProducto.Controls.Add(this.label10);
            this.FrameProducto.Controls.Add(this.cboPresentaciones);
            this.FrameProducto.Controls.Add(this.label9);
            this.FrameProducto.Controls.Add(this.label8);
            this.FrameProducto.Controls.Add(this.cboSubFamilias);
            this.FrameProducto.Controls.Add(this.label7);
            this.FrameProducto.Controls.Add(this.cboFamilias);
            this.FrameProducto.Controls.Add(this.label6);
            this.FrameProducto.Controls.Add(this.cboTipoProductos);
            this.FrameProducto.Controls.Add(this.label5);
            this.FrameProducto.Controls.Add(this.cboClasificaciones);
            this.FrameProducto.Controls.Add(this.txtDescripcionCorta);
            this.FrameProducto.Controls.Add(this.label4);
            this.FrameProducto.Controls.Add(this.label3);
            this.FrameProducto.Controls.Add(this.lblCancelado);
            this.FrameProducto.Controls.Add(this.txtId);
            this.FrameProducto.Controls.Add(this.txtDescripcion);
            this.FrameProducto.Controls.Add(this.label1);
            this.FrameProducto.Controls.Add(this.label2);
            this.FrameProducto.Location = new System.Drawing.Point(10, 10);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Size = new System.Drawing.Size(787, 584);
            this.FrameProducto.TabIndex = 0;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Datos Producto :";
            // 
            // btnKarrusel_Imagenes
            // 
            this.btnKarrusel_Imagenes.Location = new System.Drawing.Point(208, 44);
            this.btnKarrusel_Imagenes.Name = "btnKarrusel_Imagenes";
            this.btnKarrusel_Imagenes.Size = new System.Drawing.Size(26, 20);
            this.btnKarrusel_Imagenes.TabIndex = 34;
            this.btnKarrusel_Imagenes.Text = "...";
            this.btnKarrusel_Imagenes.UseVisualStyleBackColor = true;
            this.btnKarrusel_Imagenes.Visible = false;
            this.btnKarrusel_Imagenes.Click += new System.EventHandler(this.btnKarrusel_Imagenes_Click);
            // 
            // chkEsSectorSalud
            // 
            this.chkEsSectorSalud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsSectorSalud.Location = new System.Drawing.Point(576, 443);
            this.chkEsSectorSalud.Name = "chkEsSectorSalud";
            this.chkEsSectorSalud.Size = new System.Drawing.Size(199, 22);
            this.chkEsSectorSalud.TabIndex = 16;
            this.chkEsSectorSalud.Text = "Es del Sector Salud";
            this.chkEsSectorSalud.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(535, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 33;
            this.label13.Text = "EAN :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(610, 19);
            this.txtCodigoEAN.MaxLength = 20;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(165, 20);
            this.txtCodigoEAN.TabIndex = 1;
            this.txtCodigoEAN.Text = "01234567890123456789";
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // btnPresentacion
            // 
            this.btnPresentacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPresentacion.Location = new System.Drawing.Point(749, 552);
            this.btnPresentacion.Name = "btnPresentacion";
            this.btnPresentacion.Size = new System.Drawing.Size(26, 20);
            this.btnPresentacion.TabIndex = 14;
            this.btnPresentacion.Text = "...";
            this.btnPresentacion.UseVisualStyleBackColor = true;
            this.btnPresentacion.Click += new System.EventHandler(this.btnPresentacion_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.Location = new System.Drawing.Point(7, 501);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 30;
            this.label12.Text = "Segmento :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSegmentos
            // 
            this.cboSegmentos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSegmentos.BackColorEnabled = System.Drawing.Color.White;
            this.cboSegmentos.Data = "";
            this.cboSegmentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSegmentos.Filtro = " 1 = 1";
            this.cboSegmentos.FormattingEnabled = true;
            this.cboSegmentos.ListaItemsBusqueda = 20;
            this.cboSegmentos.Location = new System.Drawing.Point(122, 501);
            this.cboSegmentos.MostrarToolTip = false;
            this.cboSegmentos.Name = "cboSegmentos";
            this.cboSegmentos.Size = new System.Drawing.Size(421, 21);
            this.cboSegmentos.TabIndex = 11;
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.Location = new System.Drawing.Point(208, 527);
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(567, 20);
            this.lblLaboratorio.TabIndex = 15;
            this.lblLaboratorio.Text = "label12";
            this.lblLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdLaboratorio
            // 
            this.txtIdLaboratorio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtIdLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdLaboratorio.Decimales = 2;
            this.txtIdLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtIdLaboratorio.Location = new System.Drawing.Point(122, 527);
            this.txtIdLaboratorio.MaxLength = 4;
            this.txtIdLaboratorio.Name = "txtIdLaboratorio";
            this.txtIdLaboratorio.PermitirApostrofo = false;
            this.txtIdLaboratorio.PermitirNegativos = false;
            this.txtIdLaboratorio.Size = new System.Drawing.Size(82, 20);
            this.txtIdLaboratorio.TabIndex = 12;
            this.txtIdLaboratorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdLaboratorio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdLaboratorio_KeyDown);
            this.txtIdLaboratorio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdLaboratorio_Validating);
            // 
            // txtClaveInternaSal
            // 
            this.txtClaveInternaSal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveInternaSal.Decimales = 2;
            this.txtClaveInternaSal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveInternaSal.ForeColor = System.Drawing.Color.Black;
            this.txtClaveInternaSal.Location = new System.Drawing.Point(122, 44);
            this.txtClaveInternaSal.MaxLength = 6;
            this.txtClaveInternaSal.Name = "txtClaveInternaSal";
            this.txtClaveInternaSal.PermitirApostrofo = false;
            this.txtClaveInternaSal.PermitirNegativos = false;
            this.txtClaveInternaSal.Size = new System.Drawing.Size(82, 20);
            this.txtClaveInternaSal.TabIndex = 2;
            this.txtClaveInternaSal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveInternaSal.TextChanged += new System.EventHandler(this.txtClaveInternaSal_TextChanged);
            this.txtClaveInternaSal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveInternaSal_KeyDown);
            this.txtClaveInternaSal.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveInternaSal_Validating);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(7, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 20);
            this.label14.TabIndex = 28;
            this.label14.Text = "Clave Interna SSA :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionSal
            // 
            this.lblDescripcionSal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcionSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionSal.Location = new System.Drawing.Point(122, 68);
            this.lblDescripcionSal.Name = "lblDescripcionSal";
            this.lblDescripcionSal.Size = new System.Drawing.Size(653, 208);
            this.lblDescripcionSal.TabIndex = 3;
            this.lblDescripcionSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClaveSal
            // 
            this.lblClaveSal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaveSal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSal.Location = new System.Drawing.Point(610, 42);
            this.lblClaveSal.Name = "lblClaveSal";
            this.lblClaveSal.Size = new System.Drawing.Size(165, 20);
            this.lblClaveSal.TabIndex = 4;
            this.lblClaveSal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMedicamento
            // 
            this.chkMedicamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMedicamento.Location = new System.Drawing.Point(576, 419);
            this.chkMedicamento.Name = "chkMedicamento";
            this.chkMedicamento.Size = new System.Drawing.Size(199, 22);
            this.chkMedicamento.TabIndex = 15;
            this.chkMedicamento.Text = "Es Medicamento Controlado";
            this.chkMedicamento.UseVisualStyleBackColor = true;
            // 
            // chkCodigoEAN
            // 
            this.chkCodigoEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCodigoEAN.Location = new System.Drawing.Point(576, 497);
            this.chkCodigoEAN.Name = "chkCodigoEAN";
            this.chkCodigoEAN.Size = new System.Drawing.Size(199, 20);
            this.chkCodigoEAN.TabIndex = 18;
            this.chkCodigoEAN.Text = "Maneja Codigo EAN";
            this.chkCodigoEAN.UseVisualStyleBackColor = true;
            this.chkCodigoEAN.CheckedChanged += new System.EventHandler(this.chkCodigoEAN_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.Location = new System.Drawing.Point(7, 393);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 19;
            this.label11.Text = "Contenido paquete :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDescomponer
            // 
            this.chkDescomponer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDescomponer.Location = new System.Drawing.Point(576, 471);
            this.chkDescomponer.Name = "chkDescomponer";
            this.chkDescomponer.Size = new System.Drawing.Size(199, 20);
            this.chkDescomponer.TabIndex = 17;
            this.chkDescomponer.Text = "Componer";
            this.chkDescomponer.UseVisualStyleBackColor = true;
            this.chkDescomponer.CheckedChanged += new System.EventHandler(this.chkDescomponer_CheckedChanged);
            // 
            // txtContenido
            // 
            this.txtContenido.AllowNegative = true;
            this.txtContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtContenido.DigitsInGroup = 0;
            this.txtContenido.Flags = 0;
            this.txtContenido.Location = new System.Drawing.Point(122, 393);
            this.txtContenido.MaxDecimalPlaces = 0;
            this.txtContenido.MaxWholeDigits = 9;
            this.txtContenido.Name = "txtContenido";
            this.txtContenido.Prefix = "";
            this.txtContenido.RangeMax = 2147483647D;
            this.txtContenido.RangeMin = -2147483648D;
            this.txtContenido.Size = new System.Drawing.Size(77, 20);
            this.txtContenido.TabIndex = 7;
            this.txtContenido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Location = new System.Drawing.Point(7, 552);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Presentación :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPresentaciones
            // 
            this.cboPresentaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPresentaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboPresentaciones.Data = "";
            this.cboPresentaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPresentaciones.Filtro = " 1 = 1";
            this.cboPresentaciones.FormattingEnabled = true;
            this.cboPresentaciones.ListaItemsBusqueda = 20;
            this.cboPresentaciones.Location = new System.Drawing.Point(122, 552);
            this.cboPresentaciones.MostrarToolTip = false;
            this.cboPresentaciones.Name = "cboPresentaciones";
            this.cboPresentaciones.Size = new System.Drawing.Size(621, 21);
            this.cboPresentaciones.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(7, 527);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "Laboratorio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(7, 474);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sub-Familia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubFamilias
            // 
            this.cboSubFamilias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubFamilias.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubFamilias.Data = "";
            this.cboSubFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubFamilias.Filtro = " 1 = 1";
            this.cboSubFamilias.FormattingEnabled = true;
            this.cboSubFamilias.ListaItemsBusqueda = 20;
            this.cboSubFamilias.Location = new System.Drawing.Point(122, 474);
            this.cboSubFamilias.MostrarToolTip = false;
            this.cboSubFamilias.Name = "cboSubFamilias";
            this.cboSubFamilias.Size = new System.Drawing.Size(421, 21);
            this.cboSubFamilias.TabIndex = 10;
            this.cboSubFamilias.SelectedIndexChanged += new System.EventHandler(this.cboSubFamilias_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(7, 446);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Familia :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFamilias
            // 
            this.cboFamilias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFamilias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFamilias.Data = "";
            this.cboFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilias.Filtro = " 1 = 1";
            this.cboFamilias.FormattingEnabled = true;
            this.cboFamilias.ListaItemsBusqueda = 20;
            this.cboFamilias.Location = new System.Drawing.Point(122, 446);
            this.cboFamilias.MostrarToolTip = false;
            this.cboFamilias.Name = "cboFamilias";
            this.cboFamilias.Size = new System.Drawing.Size(421, 21);
            this.cboFamilias.TabIndex = 9;
            this.cboFamilias.SelectedIndexChanged += new System.EventHandler(this.cboFamilias_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tipo de producto :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoProductos
            // 
            this.cboTipoProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTipoProductos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoProductos.Data = "";
            this.cboTipoProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoProductos.Filtro = " 1 = 1";
            this.cboTipoProductos.FormattingEnabled = true;
            this.cboTipoProductos.ListaItemsBusqueda = 20;
            this.cboTipoProductos.Location = new System.Drawing.Point(122, 281);
            this.cboTipoProductos.MostrarToolTip = false;
            this.cboTipoProductos.Name = "cboTipoProductos";
            this.cboTipoProductos.Size = new System.Drawing.Size(653, 21);
            this.cboTipoProductos.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(7, 419);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Clasificación :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClasificaciones
            // 
            this.cboClasificaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClasificaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboClasificaciones.Data = "";
            this.cboClasificaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClasificaciones.Filtro = " 1 = 1";
            this.cboClasificaciones.FormattingEnabled = true;
            this.cboClasificaciones.ListaItemsBusqueda = 20;
            this.cboClasificaciones.Location = new System.Drawing.Point(122, 419);
            this.cboClasificaciones.MostrarToolTip = false;
            this.cboClasificaciones.Name = "cboClasificaciones";
            this.cboClasificaciones.Size = new System.Drawing.Size(421, 21);
            this.cboClasificaciones.TabIndex = 8;
            // 
            // txtDescripcionCorta
            // 
            this.txtDescripcionCorta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcionCorta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcionCorta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcionCorta.Decimales = 2;
            this.txtDescripcionCorta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcionCorta.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcionCorta.Location = new System.Drawing.Point(122, 369);
            this.txtDescripcionCorta.MaxLength = 100;
            this.txtDescripcionCorta.Name = "txtDescripcionCorta";
            this.txtDescripcionCorta.PermitirApostrofo = false;
            this.txtDescripcionCorta.PermitirNegativos = false;
            this.txtDescripcionCorta.Size = new System.Drawing.Size(653, 20);
            this.txtDescripcionCorta.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Descripción corta :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(535, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Clave SSA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(208, 21);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(87, 20);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(122, 21);
            this.txtId.MaxLength = 8;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(82, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(122, 305);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(653, 60);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Código Interno :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabCatProductos
            // 
            this.tabCatProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCatProductos.Controls.Add(this.tabProductos);
            this.tabCatProductos.Controls.Add(this.tabEstadosCodigos);
            this.tabCatProductos.Controls.Add(this.tabPrecios);
            this.tabCatProductos.Location = new System.Drawing.Point(11, 27);
            this.tabCatProductos.Name = "tabCatProductos";
            this.tabCatProductos.SelectedIndex = 0;
            this.tabCatProductos.Size = new System.Drawing.Size(819, 626);
            this.tabCatProductos.TabIndex = 1;
            // 
            // tabProductos
            // 
            this.tabProductos.Controls.Add(this.FrameProducto);
            this.tabProductos.Location = new System.Drawing.Point(4, 22);
            this.tabProductos.Name = "tabProductos";
            this.tabProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductos.Size = new System.Drawing.Size(811, 600);
            this.tabProductos.TabIndex = 0;
            this.tabProductos.Text = "Datos de Producto";
            this.tabProductos.UseVisualStyleBackColor = true;
            // 
            // tabEstadosCodigos
            // 
            this.tabEstadosCodigos.Controls.Add(this.groupBox2);
            this.tabEstadosCodigos.Controls.Add(this.groupBox3);
            this.tabEstadosCodigos.Location = new System.Drawing.Point(4, 22);
            this.tabEstadosCodigos.Name = "tabEstadosCodigos";
            this.tabEstadosCodigos.Padding = new System.Windows.Forms.Padding(3);
            this.tabEstadosCodigos.Size = new System.Drawing.Size(811, 600);
            this.tabEstadosCodigos.TabIndex = 1;
            this.tabEstadosCodigos.Text = "Estados y Codigos EAN";
            this.tabEstadosCodigos.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdEstadosProductos);
            this.groupBox2.Location = new System.Drawing.Point(10, 309);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(787, 283);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista de Estados que manejan este producto";
            // 
            // grdEstadosProductos
            // 
            this.grdEstadosProductos.AccessibleDescription = "grdEstadosProductos, Sheet1, Row 0, Column 0, ";
            this.grdEstadosProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdEstadosProductos.BackColor = System.Drawing.Color.Transparent;
            this.grdEstadosProductos.Location = new System.Drawing.Point(9, 19);
            this.grdEstadosProductos.Name = "grdEstadosProductos";
            this.grdEstadosProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdEstadosProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdEstadosProductos_Sheet1});
            this.grdEstadosProductos.Size = new System.Drawing.Size(768, 256);
            this.grdEstadosProductos.TabIndex = 0;
            // 
            // grdEstadosProductos_Sheet1
            // 
            this.grdEstadosProductos_Sheet1.Reset();
            this.grdEstadosProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdEstadosProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdEstadosProductos_Sheet1.ColumnCount = 3;
            this.grdEstadosProductos_Sheet1.RowCount = 10;
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Activo";
            this.grdEstadosProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.grdEstadosProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdEstadosProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grdEstadosProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(0).Width = 76F;
            this.grdEstadosProductos_Sheet1.Columns.Get(1).CellType = textCellType5;
            this.grdEstadosProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdEstadosProductos_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdEstadosProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdEstadosProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(1).Width = 353F;
            picture2.AlignHorz = FarPoint.Win.HorizontalAlignment.Center;
            picture2.AlignVert = FarPoint.Win.VerticalAlignment.Center;
            picture2.TransparencyColor = System.Drawing.Color.Empty;
            picture2.TransparencyTolerance = 0;
            checkBoxCellType3.BackgroundImage = picture2;
            checkBoxCellType3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
            checkBoxCellType3.TextAlign = FarPoint.Win.ButtonTextAlign.TextBottomPictTop;
            this.grdEstadosProductos_Sheet1.Columns.Get(2).CellType = checkBoxCellType3;
            this.grdEstadosProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(2).Label = "Activo";
            this.grdEstadosProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdEstadosProductos_Sheet1.Columns.Get(2).Width = 64F;
            this.grdEstadosProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdEstadosProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grdCodigosEAN);
            this.groupBox3.Location = new System.Drawing.Point(10, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(787, 292);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Codigos EAN relacionados al producto";
            // 
            // grdCodigosEAN
            // 
            this.grdCodigosEAN.AccessibleDescription = "grdCodigosEAN, Sheet1, Row 0, Column 0, ";
            this.grdCodigosEAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCodigosEAN.BackColor = System.Drawing.Color.Transparent;
            this.grdCodigosEAN.Location = new System.Drawing.Point(9, 19);
            this.grdCodigosEAN.Name = "grdCodigosEAN";
            this.grdCodigosEAN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCodigosEAN.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCodigosEAN_Sheet1});
            this.grdCodigosEAN.Size = new System.Drawing.Size(768, 267);
            this.grdCodigosEAN.TabIndex = 0;
            this.grdCodigosEAN.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdCodigosEAN_Advance);
            this.grdCodigosEAN.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdCodigosEAN_ButtonClicked);
            this.grdCodigosEAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdCodigosEAN_KeyDown);
            // 
            // grdCodigosEAN_Sheet1
            // 
            this.grdCodigosEAN_Sheet1.Reset();
            this.grdCodigosEAN_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCodigosEAN_Sheet1.ColumnCount = 7;
            this.grdCodigosEAN_Sheet1.RowCount = 5;
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Codigo EAN";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Activo";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Factor Unidosis";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Cont. Corrugado";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cajas Cama";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cajas Tarima";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Ver Imagen";
            this.grdCodigosEAN_Sheet1.ColumnHeader.Rows.Get(0).Height = 28F;
            textCellType6.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType6.MaxLength = 30;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).CellType = textCellType6;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Label = "Codigo EAN";
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Locked = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(0).Width = 180F;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).CellType = checkBoxCellType4;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Label = "Activo";
            this.grdCodigosEAN_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(1).Width = 40F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = 0D;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).CellType = numberCellType5;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCodigosEAN_Sheet1.Columns.Get(2).Label = "Factor Unidosis";
            this.grdCodigosEAN_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.MaximumValue = 10000000D;
            numberCellType6.MinimumValue = -10000000D;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).CellType = numberCellType6;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCodigosEAN_Sheet1.Columns.Get(3).Label = "Cont. Corrugado";
            this.grdCodigosEAN_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType7.DecimalPlaces = 0;
            numberCellType7.MaximumValue = 10000000D;
            numberCellType7.MinimumValue = -10000000D;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).CellType = numberCellType7;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Label = "Cajas Cama";
            this.grdCodigosEAN_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(4).Width = 55F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = -10000000D;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).CellType = numberCellType8;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Label = "Cajas Tarima";
            this.grdCodigosEAN_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(5).Width = 55F;
            buttonCellType2.ButtonColor = System.Drawing.Color.Silver;
            buttonCellType2.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType2.Text = "...";
            this.grdCodigosEAN_Sheet1.Columns.Get(6).CellType = buttonCellType2;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Label = "Ver Imagen";
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Locked = false;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCodigosEAN_Sheet1.Columns.Get(6).Width = 45F;
            this.grdCodigosEAN_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdCodigosEAN_Sheet1.RowHeader.Columns.Get(0).Width = 34F;
            this.grdCodigosEAN_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tabPrecios
            // 
            this.tabPrecios.Controls.Add(this.groupBox1);
            this.tabPrecios.Controls.Add(this.FrameDatosPrecio);
            this.tabPrecios.Location = new System.Drawing.Point(4, 22);
            this.tabPrecios.Name = "tabPrecios";
            this.tabPrecios.Size = new System.Drawing.Size(811, 600);
            this.tabPrecios.TabIndex = 2;
            this.tabPrecios.Text = "Información para venta general";
            this.tabPrecios.UseVisualStyleBackColor = true;
            // 
            // FrameDatosPrecio
            // 
            this.FrameDatosPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatosPrecio.Controls.Add(this.txtDescuento);
            this.FrameDatosPrecio.Controls.Add(this.txtPrecioMaximo);
            this.FrameDatosPrecio.Controls.Add(this.txtUtilidad);
            this.FrameDatosPrecio.Controls.Add(this.label15);
            this.FrameDatosPrecio.Controls.Add(this.label16);
            this.FrameDatosPrecio.Controls.Add(this.label17);
            this.FrameDatosPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameDatosPrecio.Location = new System.Drawing.Point(10, 10);
            this.FrameDatosPrecio.Name = "FrameDatosPrecio";
            this.FrameDatosPrecio.Size = new System.Drawing.Size(392, 121);
            this.FrameDatosPrecio.TabIndex = 1;
            this.FrameDatosPrecio.TabStop = false;
            this.FrameDatosPrecio.Text = "Datos de Precio";
            // 
            // txtDescuento
            // 
            this.txtDescuento.AllowNegative = true;
            this.txtDescuento.DigitsInGroup = 3;
            this.txtDescuento.Flags = 7680;
            this.txtDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuento.Location = new System.Drawing.Point(233, 54);
            this.txtDescuento.MaxDecimalPlaces = 4;
            this.txtDescuento.MaxLength = 15;
            this.txtDescuento.MaxWholeDigits = 15;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Prefix = "";
            this.txtDescuento.RangeMax = 1.7976931348623157E+308D;
            this.txtDescuento.RangeMin = -1.7976931348623157E+308D;
            this.txtDescuento.Size = new System.Drawing.Size(138, 22);
            this.txtDescuento.TabIndex = 1;
            this.txtDescuento.Text = "1.0000";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecioMaximo
            // 
            this.txtPrecioMaximo.AllowNegative = true;
            this.txtPrecioMaximo.DigitsInGroup = 3;
            this.txtPrecioMaximo.Flags = 7680;
            this.txtPrecioMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioMaximo.Location = new System.Drawing.Point(233, 27);
            this.txtPrecioMaximo.MaxDecimalPlaces = 4;
            this.txtPrecioMaximo.MaxLength = 15;
            this.txtPrecioMaximo.MaxWholeDigits = 15;
            this.txtPrecioMaximo.Name = "txtPrecioMaximo";
            this.txtPrecioMaximo.Prefix = "";
            this.txtPrecioMaximo.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecioMaximo.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecioMaximo.Size = new System.Drawing.Size(138, 22);
            this.txtPrecioMaximo.TabIndex = 0;
            this.txtPrecioMaximo.Text = "1.0000";
            this.txtPrecioMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.AllowNegative = true;
            this.txtUtilidad.DigitsInGroup = 3;
            this.txtUtilidad.Flags = 7680;
            this.txtUtilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidad.Location = new System.Drawing.Point(233, 81);
            this.txtUtilidad.MaxDecimalPlaces = 4;
            this.txtUtilidad.MaxLength = 15;
            this.txtUtilidad.MaxWholeDigits = 15;
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.Prefix = "";
            this.txtUtilidad.RangeMax = 1.7976931348623157E+308D;
            this.txtUtilidad.RangeMin = -1.7976931348623157E+308D;
            this.txtUtilidad.Size = new System.Drawing.Size(138, 22);
            this.txtUtilidad.TabIndex = 2;
            this.txtUtilidad.Text = "1.0000";
            this.txtUtilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(9, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(218, 25);
            this.label15.TabIndex = 20;
            this.label15.Text = "% Descuento :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(9, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(218, 25);
            this.label16.TabIndex = 19;
            this.label16.Text = "Precio Máximo :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(9, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(218, 25);
            this.label17.TabIndex = 18;
            this.label17.Text = "% Utilidad :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMargenMininoUtilidad_02_Superior);
            this.groupBox1.Controls.Add(this.txtMargenMininoUtilidad_01_Inferior);
            this.groupBox1.Controls.Add(this.txtPorcentaje_CopagoColaborador);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(407, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 121);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Margenes y Copago";
            // 
            // txtMargenMininoUtilidad_02_Superior
            // 
            this.txtMargenMininoUtilidad_02_Superior.AllowNegative = true;
            this.txtMargenMininoUtilidad_02_Superior.DigitsInGroup = 3;
            this.txtMargenMininoUtilidad_02_Superior.Flags = 7680;
            this.txtMargenMininoUtilidad_02_Superior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMargenMininoUtilidad_02_Superior.Location = new System.Drawing.Point(233, 54);
            this.txtMargenMininoUtilidad_02_Superior.MaxDecimalPlaces = 4;
            this.txtMargenMininoUtilidad_02_Superior.MaxLength = 15;
            this.txtMargenMininoUtilidad_02_Superior.MaxWholeDigits = 15;
            this.txtMargenMininoUtilidad_02_Superior.Name = "txtMargenMininoUtilidad_02_Superior";
            this.txtMargenMininoUtilidad_02_Superior.Prefix = "";
            this.txtMargenMininoUtilidad_02_Superior.RangeMax = 1.7976931348623157E+308D;
            this.txtMargenMininoUtilidad_02_Superior.RangeMin = -1.7976931348623157E+308D;
            this.txtMargenMininoUtilidad_02_Superior.Size = new System.Drawing.Size(138, 22);
            this.txtMargenMininoUtilidad_02_Superior.TabIndex = 1;
            this.txtMargenMininoUtilidad_02_Superior.Text = "1.0000";
            this.txtMargenMininoUtilidad_02_Superior.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMargenMininoUtilidad_01_Inferior
            // 
            this.txtMargenMininoUtilidad_01_Inferior.AllowNegative = true;
            this.txtMargenMininoUtilidad_01_Inferior.DigitsInGroup = 3;
            this.txtMargenMininoUtilidad_01_Inferior.Flags = 7680;
            this.txtMargenMininoUtilidad_01_Inferior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMargenMininoUtilidad_01_Inferior.Location = new System.Drawing.Point(233, 27);
            this.txtMargenMininoUtilidad_01_Inferior.MaxDecimalPlaces = 4;
            this.txtMargenMininoUtilidad_01_Inferior.MaxLength = 15;
            this.txtMargenMininoUtilidad_01_Inferior.MaxWholeDigits = 15;
            this.txtMargenMininoUtilidad_01_Inferior.Name = "txtMargenMininoUtilidad_01_Inferior";
            this.txtMargenMininoUtilidad_01_Inferior.Prefix = "";
            this.txtMargenMininoUtilidad_01_Inferior.RangeMax = 1.7976931348623157E+308D;
            this.txtMargenMininoUtilidad_01_Inferior.RangeMin = -1.7976931348623157E+308D;
            this.txtMargenMininoUtilidad_01_Inferior.Size = new System.Drawing.Size(138, 22);
            this.txtMargenMininoUtilidad_01_Inferior.TabIndex = 0;
            this.txtMargenMininoUtilidad_01_Inferior.Text = "1.0000";
            this.txtMargenMininoUtilidad_01_Inferior.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPorcentaje_CopagoColaborador
            // 
            this.txtPorcentaje_CopagoColaborador.AllowNegative = true;
            this.txtPorcentaje_CopagoColaborador.DigitsInGroup = 3;
            this.txtPorcentaje_CopagoColaborador.Flags = 7680;
            this.txtPorcentaje_CopagoColaborador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentaje_CopagoColaborador.Location = new System.Drawing.Point(233, 81);
            this.txtPorcentaje_CopagoColaborador.MaxDecimalPlaces = 4;
            this.txtPorcentaje_CopagoColaborador.MaxLength = 15;
            this.txtPorcentaje_CopagoColaborador.MaxWholeDigits = 15;
            this.txtPorcentaje_CopagoColaborador.Name = "txtPorcentaje_CopagoColaborador";
            this.txtPorcentaje_CopagoColaborador.Prefix = "";
            this.txtPorcentaje_CopagoColaborador.RangeMax = 1.7976931348623157E+308D;
            this.txtPorcentaje_CopagoColaborador.RangeMin = -1.7976931348623157E+308D;
            this.txtPorcentaje_CopagoColaborador.Size = new System.Drawing.Size(138, 22);
            this.txtPorcentaje_CopagoColaborador.TabIndex = 2;
            this.txtPorcentaje_CopagoColaborador.Text = "1.0000";
            this.txtPorcentaje_CopagoColaborador.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(9, 57);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(218, 16);
            this.label18.TabIndex = 20;
            this.label18.Text = "% Mínimo de utilidad superior :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(9, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(218, 25);
            this.label19.TabIndex = 19;
            this.label19.Text = "% Mínimo de utilidad inferior :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(9, 84);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(218, 16);
            this.label20.TabIndex = 18;
            this.label20.Text = "% Copago :";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 661);
            this.Controls.Add(this.tabCatProductos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmProductos";
            this.Text = "Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProductos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProducto.ResumeLayout(false);
            this.FrameProducto.PerformLayout();
            this.tabCatProductos.ResumeLayout(false);
            this.tabProductos.ResumeLayout(false);
            this.tabEstadosCodigos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadosProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstadosProductos_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCodigosEAN_Sheet1)).EndInit();
            this.tabPrecios.ResumeLayout(false);
            this.FrameDatosPrecio.ResumeLayout(false);
            this.FrameDatosPrecio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameProducto;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtDescripcionCorta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboClasificaciones;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboTipoProductos;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scComboBoxExt cboFamilias;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scComboBoxExt cboPresentaciones;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboSubFamilias;
        private SC_ControlsCS.scIntegerTextBox txtContenido;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkCodigoEAN;
        private System.Windows.Forms.CheckBox chkDescomponer;
        private System.Windows.Forms.CheckBox chkMedicamento;
        private System.Windows.Forms.TabControl tabCatProductos;
        private System.Windows.Forms.TabPage tabProductos;
        private System.Windows.Forms.TabPage tabEstadosCodigos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblClaveSal;
        private System.Windows.Forms.Label lblDescripcionSal;
        private SC_ControlsCS.scTextBoxExt txtClaveInternaSal;
        private System.Windows.Forms.Label label14;
        private FarPoint.Win.Spread.FpSpread grdCodigosEAN;
        private FarPoint.Win.Spread.SheetView grdCodigosEAN_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdEstadosProductos;
        private FarPoint.Win.Spread.SheetView grdEstadosProductos_Sheet1;
        private System.Windows.Forms.ToolTip toolTip;
        private SC_ControlsCS.scTextBoxExt txtIdLaboratorio;
        private System.Windows.Forms.Label lblLaboratorio;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scComboBoxExt cboSegmentos;
        private System.Windows.Forms.Button btnPresentacion;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkEsSectorSalud;
        private System.Windows.Forms.TabPage tabPrecios;
        private System.Windows.Forms.GroupBox FrameDatosPrecio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private SC_ControlsCS.scNumericTextBox txtDescuento;
        private SC_ControlsCS.scNumericTextBox txtPrecioMaximo;
        private SC_ControlsCS.scNumericTextBox txtUtilidad;
        private System.Windows.Forms.Button btnKarrusel_Imagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGenerarPaqueteDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scNumericTextBox txtMargenMininoUtilidad_02_Superior;
        private SC_ControlsCS.scNumericTextBox txtMargenMininoUtilidad_01_Inferior;
        private SC_ControlsCS.scNumericTextBox txtPorcentaje_CopagoColaborador;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
    }
}