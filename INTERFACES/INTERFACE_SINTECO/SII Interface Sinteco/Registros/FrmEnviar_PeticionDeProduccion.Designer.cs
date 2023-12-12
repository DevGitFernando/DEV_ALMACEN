namespace SII_Interface_Sinteco.Registros
{
    partial class FrmEnviar_PeticionDeProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnviar_PeticionDeProduccion));
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("DataAreaMidnght");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.NamedStyle namedStyle2 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEnviarPeticion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.chkLoteBIS = new System.Windows.Forms.CheckBox();
            this.cboCaractesSeparador = new SC_ControlsCS.scComboBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.nmCantidad = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRegistroSanitario = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblClaveLote = new System.Windows.Forms.Label();
            this.lblSubFarmacia = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaCaducidad = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDescripcionClaveSSA = new System.Windows.Forms.Label();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSoloExistencias = new System.Windows.Forms.CheckBox();
            this.FrameMensaje = new System.Windows.Forms.GroupBox();
            this.lblMensaje_SINTECO = new SC_ControlsCS.scLabelExt();
            this.FrameLotes = new System.Windows.Forms.GroupBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.grdLotes = new FarPoint.Win.Spread.FpSpread();
            this.grdLotes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmXML = new System.Windows.Forms.Timer(this.components);
            this.cboAditamentos = new SC_ControlsCS.scComboBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAditamentos = new System.Windows.Forms.Button();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCantidad)).BeginInit();
            this.FrameMensaje.SuspendLayout();
            this.FrameLotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEnviarPeticion,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(839, 25);
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
            // btnEnviarPeticion
            // 
            this.btnEnviarPeticion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnviarPeticion.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarPeticion.Image")));
            this.btnEnviarPeticion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarPeticion.Name = "btnEnviarPeticion";
            this.btnEnviarPeticion.Size = new System.Drawing.Size(23, 22);
            this.btnEnviarPeticion.Text = "Procesar Consumos";
            this.btnEnviarPeticion.Click += new System.EventHandler(this.btnEnviarPeticion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.btnAditamentos);
            this.FrameInformacion.Controls.Add(this.cboAditamentos);
            this.FrameInformacion.Controls.Add(this.label11);
            this.FrameInformacion.Controls.Add(this.chkLoteBIS);
            this.FrameInformacion.Controls.Add(this.cboCaractesSeparador);
            this.FrameInformacion.Controls.Add(this.label10);
            this.FrameInformacion.Controls.Add(this.nmCantidad);
            this.FrameInformacion.Controls.Add(this.label8);
            this.FrameInformacion.Controls.Add(this.txtReferencia);
            this.FrameInformacion.Controls.Add(this.label7);
            this.FrameInformacion.Controls.Add(this.txtRegistroSanitario);
            this.FrameInformacion.Controls.Add(this.label4);
            this.FrameInformacion.Controls.Add(this.lblClaveLote);
            this.FrameInformacion.Controls.Add(this.lblSubFarmacia);
            this.FrameInformacion.Controls.Add(this.label5);
            this.FrameInformacion.Controls.Add(this.dtpFechaCaducidad);
            this.FrameInformacion.Controls.Add(this.label9);
            this.FrameInformacion.Controls.Add(this.label2);
            this.FrameInformacion.Controls.Add(this.lblLaboratorio);
            this.FrameInformacion.Controls.Add(this.label6);
            this.FrameInformacion.Controls.Add(this.lblDescripcionClaveSSA);
            this.FrameInformacion.Controls.Add(this.lblClaveSSA);
            this.FrameInformacion.Controls.Add(this.label1);
            this.FrameInformacion.Controls.Add(this.lblDescripcion);
            this.FrameInformacion.Controls.Add(this.txtCodigoEAN);
            this.FrameInformacion.Controls.Add(this.label3);
            this.FrameInformacion.Location = new System.Drawing.Point(13, 29);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(818, 191);
            this.FrameInformacion.TabIndex = 1;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información";
            // 
            // chkLoteBIS
            // 
            this.chkLoteBIS.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLoteBIS.Location = new System.Drawing.Point(704, 167);
            this.chkLoteBIS.Name = "chkLoteBIS";
            this.chkLoteBIS.Size = new System.Drawing.Size(101, 18);
            this.chkLoteBIS.TabIndex = 6;
            this.chkLoteBIS.Text = "Lote BIS";
            this.chkLoteBIS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLoteBIS.UseVisualStyleBackColor = true;
            this.chkLoteBIS.CheckedChanged += new System.EventHandler(this.chkLoteBIS_CheckedChanged);
            // 
            // cboCaractesSeparador
            // 
            this.cboCaractesSeparador.BackColorEnabled = System.Drawing.Color.White;
            this.cboCaractesSeparador.Data = "";
            this.cboCaractesSeparador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaractesSeparador.Filtro = " 1 = 1";
            this.cboCaractesSeparador.FormattingEnabled = true;
            this.cboCaractesSeparador.ListaItemsBusqueda = 20;
            this.cboCaractesSeparador.Location = new System.Drawing.Point(685, 65);
            this.cboCaractesSeparador.MostrarToolTip = false;
            this.cboCaractesSeparador.Name = "cboCaractesSeparador";
            this.cboCaractesSeparador.Size = new System.Drawing.Size(120, 21);
            this.cboCaractesSeparador.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(621, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 45;
            this.label10.Text = "Separador :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmCantidad
            // 
            this.nmCantidad.InterceptArrowKeys = false;
            this.nmCantidad.Location = new System.Drawing.Point(386, 165);
            this.nmCantidad.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nmCantidad.Name = "nmCantidad";
            this.nmCantidad.Size = new System.Drawing.Size(181, 20);
            this.nmCantidad.TabIndex = 5;
            this.nmCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmCantidad.ThousandsSeparator = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(300, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.TabIndex = 43;
            this.label8.Text = "Cantidad :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferencia
            // 
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(109, 163);
            this.txtReferencia.MaxLength = 20;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(180, 20);
            this.txtReferencia.TabIndex = 4;
            this.txtReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(4, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Referencia :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRegistroSanitario
            // 
            this.txtRegistroSanitario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRegistroSanitario.Decimales = 2;
            this.txtRegistroSanitario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRegistroSanitario.ForeColor = System.Drawing.Color.Black;
            this.txtRegistroSanitario.Location = new System.Drawing.Point(349, 65);
            this.txtRegistroSanitario.MaxLength = 30;
            this.txtRegistroSanitario.Name = "txtRegistroSanitario";
            this.txtRegistroSanitario.PermitirApostrofo = false;
            this.txtRegistroSanitario.PermitirNegativos = false;
            this.txtRegistroSanitario.Size = new System.Drawing.Size(180, 20);
            this.txtRegistroSanitario.TabIndex = 1;
            this.txtRegistroSanitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(248, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "Registro sanitario :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveLote
            // 
            this.lblClaveLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveLote.Location = new System.Drawing.Point(386, 139);
            this.lblClaveLote.Name = "lblClaveLote";
            this.lblClaveLote.Size = new System.Drawing.Size(181, 20);
            this.lblClaveLote.TabIndex = 38;
            this.lblClaveLote.Text = "label7";
            this.lblClaveLote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubFarmacia
            // 
            this.lblSubFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubFarmacia.Location = new System.Drawing.Point(109, 139);
            this.lblSubFarmacia.Name = "lblSubFarmacia";
            this.lblSubFarmacia.Size = new System.Drawing.Size(181, 20);
            this.lblSubFarmacia.TabIndex = 37;
            this.lblSubFarmacia.Text = "label4";
            this.lblSubFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(585, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 18);
            this.label5.TabIndex = 36;
            this.label5.Text = "Fecha de caducidad :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaCaducidad
            // 
            this.dtpFechaCaducidad.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaCaducidad.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCaducidad.Location = new System.Drawing.Point(704, 139);
            this.dtpFechaCaducidad.Name = "dtpFechaCaducidad";
            this.dtpFechaCaducidad.Size = new System.Drawing.Size(101, 20);
            this.dtpFechaCaducidad.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(296, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 18);
            this.label9.TabIndex = 33;
            this.label9.Text = "Clave de lote :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 31;
            this.label2.Text = "Sub-Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.Location = new System.Drawing.Point(109, 114);
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(696, 20);
            this.lblLaboratorio.TabIndex = 30;
            this.lblLaboratorio.Text = "label4";
            this.lblLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Laboratorio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcionClaveSSA
            // 
            this.lblDescripcionClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClaveSSA.Location = new System.Drawing.Point(109, 90);
            this.lblDescripcionClaveSSA.Name = "lblDescripcionClaveSSA";
            this.lblDescripcionClaveSSA.Size = new System.Drawing.Size(696, 20);
            this.lblDescripcionClaveSSA.TabIndex = 28;
            this.lblDescripcionClaveSSA.Text = "label4";
            this.lblDescripcionClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(109, 65);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(133, 20);
            this.lblClaveSSA.TabIndex = 27;
            this.lblClaveSSA.Text = "label4";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Clave SSA :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(248, 16);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(557, 20);
            this.lblDescripcion.TabIndex = 24;
            this.lblDescripcion.Text = "label4";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(109, 16);
            this.txtCodigoEAN.MaxLength = 15;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(133, 20);
            this.txtCodigoEAN.TabIndex = 0;
            this.txtCodigoEAN.Text = "012345678901234";
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoEAN_KeyDown);
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Código EAN :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSoloExistencias
            // 
            this.chkSoloExistencias.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSoloExistencias.Location = new System.Drawing.Point(637, 3);
            this.chkSoloExistencias.Name = "chkSoloExistencias";
            this.chkSoloExistencias.Size = new System.Drawing.Size(184, 18);
            this.chkSoloExistencias.TabIndex = 2;
            this.chkSoloExistencias.Text = "Mostrar sólo Lotes con Existencia";
            this.chkSoloExistencias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSoloExistencias.UseVisualStyleBackColor = true;
            this.chkSoloExistencias.CheckedChanged += new System.EventHandler(this.chkSoloExistencias_CheckedChanged);
            // 
            // FrameMensaje
            // 
            this.FrameMensaje.Controls.Add(this.lblMensaje_SINTECO);
            this.FrameMensaje.Location = new System.Drawing.Point(12, 429);
            this.FrameMensaje.Name = "FrameMensaje";
            this.FrameMensaje.Size = new System.Drawing.Size(819, 127);
            this.FrameMensaje.TabIndex = 3;
            this.FrameMensaje.TabStop = false;
            this.FrameMensaje.Text = "Mensaje";
            // 
            // lblMensaje_SINTECO
            // 
            this.lblMensaje_SINTECO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje_SINTECO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMensaje_SINTECO.Location = new System.Drawing.Point(11, 16);
            this.lblMensaje_SINTECO.MostrarToolTip = false;
            this.lblMensaje_SINTECO.Name = "lblMensaje_SINTECO";
            this.lblMensaje_SINTECO.Size = new System.Drawing.Size(797, 103);
            this.lblMensaje_SINTECO.TabIndex = 0;
            this.lblMensaje_SINTECO.Text = "scLabelExt1";
            // 
            // FrameLotes
            // 
            this.FrameLotes.Controls.Add(this.lblAyuda);
            this.FrameLotes.Controls.Add(this.grdLotes);
            this.FrameLotes.Location = new System.Drawing.Point(13, 222);
            this.FrameLotes.Name = "FrameLotes";
            this.FrameLotes.Size = new System.Drawing.Size(818, 207);
            this.FrameLotes.TabIndex = 2;
            this.FrameLotes.TabStop = false;
            this.FrameLotes.Text = "Lotes del producto";
            // 
            // lblAyuda
            // 
            this.lblAyuda.BackColor = System.Drawing.Color.Black;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.ForeColor = System.Drawing.SystemColors.Control;
            this.lblAyuda.Location = new System.Drawing.Point(3, 180);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(812, 24);
            this.lblAyuda.TabIndex = 10;
            this.lblAyuda.Text = "Doble clic sobre el renglón del Lote para generar el XML";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grdLotes
            // 
            this.grdLotes.AccessibleDescription = "grdLotes, Sheet1, Row 0, Column 0, ";
            this.grdLotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdLotes.Location = new System.Drawing.Point(10, 17);
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
            this.grdLotes.Size = new System.Drawing.Size(798, 160);
            this.grdLotes.TabIndex = 0;
            this.grdLotes.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdLotes_CellDoubleClick);
            // 
            // grdLotes_Sheet1
            // 
            this.grdLotes_Sheet1.Reset();
            this.grdLotes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdLotes_Sheet1.ColumnCount = 11;
            this.grdLotes_Sheet1.RowCount = 10;
            this.grdLotes_Sheet1.Cells.Get(0, 6).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.Cells.Get(0, 7).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.Cells.Get(0, 10).Value = new System.DateTime(2008, 9, 12, 0, 0, 0, 0);
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Código";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo EAN";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "# Sub Farmacia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Sub Farmacia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Clave de lote";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Meses por Caducar";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha de entrada";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha de Caducidad";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Status";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Existencia";
            this.grdLotes_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Fecha de Caducidad Maxima";
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
            this.grdLotes_Sheet1.Columns.Get(2).Label = "# Sub Farmacia";
            this.grdLotes_Sheet1.Columns.Get(2).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(2).Width = 51F;
            this.grdLotes_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdLotes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(3).Label = "Sub Farmacia";
            this.grdLotes_Sheet1.Columns.Get(3).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(3).Width = 126F;
            this.grdLotes_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdLotes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdLotes_Sheet1.Columns.Get(4).Label = "Clave de lote";
            this.grdLotes_Sheet1.Columns.Get(4).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(4).Width = 215F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            this.grdLotes_Sheet1.Columns.Get(5).CellType = numberCellType1;
            this.grdLotes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(5).Label = "Meses por Caducar";
            this.grdLotes_Sheet1.Columns.Get(5).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            textCellType6.MaxLength = 10;
            this.grdLotes_Sheet1.Columns.Get(6).CellType = textCellType6;
            this.grdLotes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Label = "Fecha de entrada";
            this.grdLotes_Sheet1.Columns.Get(6).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(6).Width = 75F;
            textCellType7.MaxLength = 7;
            this.grdLotes_Sheet1.Columns.Get(7).CellType = textCellType7;
            this.grdLotes_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Label = "Fecha de Caducidad";
            this.grdLotes_Sheet1.Columns.Get(7).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(7).Width = 75F;
            this.grdLotes_Sheet1.Columns.Get(8).CellType = textCellType8;
            this.grdLotes_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Label = "Status";
            this.grdLotes_Sheet1.Columns.Get(8).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(8).Width = 70F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdLotes_Sheet1.Columns.Get(9).CellType = numberCellType2;
            this.grdLotes_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(9).Label = "Existencia";
            this.grdLotes_Sheet1.Columns.Get(9).Locked = true;
            this.grdLotes_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(9).Width = 70F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2010, 3, 19, 13, 27, 52, 0);
            dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd";
            this.grdLotes_Sheet1.Columns.Get(10).CellType = dateTimeCellType1;
            this.grdLotes_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Label = "Fecha de Caducidad Maxima";
            this.grdLotes_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdLotes_Sheet1.Columns.Get(10).Visible = false;
            this.grdLotes_Sheet1.Columns.Get(10).Width = 75F;
            this.grdLotes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdLotes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmXML
            // 
            this.tmXML.Tick += new System.EventHandler(this.tmXML_Tick);
            // 
            // cboAditamentos
            // 
            this.cboAditamentos.BackColorEnabled = System.Drawing.Color.White;
            this.cboAditamentos.Data = "";
            this.cboAditamentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAditamentos.Filtro = " 1 = 1";
            this.cboAditamentos.FormattingEnabled = true;
            this.cboAditamentos.ListaItemsBusqueda = 20;
            this.cboAditamentos.Location = new System.Drawing.Point(109, 39);
            this.cboAditamentos.MostrarToolTip = false;
            this.cboAditamentos.Name = "cboAditamentos";
            this.cboAditamentos.Size = new System.Drawing.Size(664, 21);
            this.cboAditamentos.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(4, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 47;
            this.label11.Text = "Aditamentos :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAditamentos
            // 
            this.btnAditamentos.Location = new System.Drawing.Point(776, 38);
            this.btnAditamentos.Name = "btnAditamentos";
            this.btnAditamentos.Size = new System.Drawing.Size(29, 23);
            this.btnAditamentos.TabIndex = 48;
            this.btnAditamentos.Text = "...";
            this.btnAditamentos.UseVisualStyleBackColor = true;
            this.btnAditamentos.Click += new System.EventHandler(this.btnAditamentos_Click);
            // 
            // FrmEnviar_PeticionDeProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 564);
            this.Controls.Add(this.chkSoloExistencias);
            this.Controls.Add(this.FrameLotes);
            this.Controls.Add(this.FrameMensaje);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmEnviar_PeticionDeProduccion";
            this.Text = "Enviar petición de produccion";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEnviar_PeticionDeProduccion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameInformacion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmCantidad)).EndInit();
            this.FrameMensaje.ResumeLayout(false);
            this.FrameLotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLotes_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEnviarPeticion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private System.Windows.Forms.GroupBox FrameMensaje;
        private SC_ControlsCS.scLabelExt lblMensaje_SINTECO;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label lblDescripcionClaveSSA;
        private System.Windows.Forms.Label lblLaboratorio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaCaducidad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox FrameLotes;
        private FarPoint.Win.Spread.FpSpread grdLotes;
        private FarPoint.Win.Spread.SheetView grdLotes_Sheet1;
        private System.Windows.Forms.Label lblSubFarmacia;
        private System.Windows.Forms.Label lblClaveLote;
        private System.Windows.Forms.CheckBox chkSoloExistencias;
        private System.Windows.Forms.Label lblAyuda;
        private SC_ControlsCS.scTextBoxExt txtRegistroSanitario;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nmCantidad;
        private System.Windows.Forms.Timer tmXML;
        private SC_ControlsCS.scComboBoxExt cboCaractesSeparador;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkLoteBIS;
        private SC_ControlsCS.scComboBoxExt cboAditamentos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAditamentos;
    }
}