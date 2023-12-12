namespace OficinaCentral.Catalogos.Productos
{
    partial class FrmProductosRegistrosSanitarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductosRegistrosSanitarios));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosRegistro = new System.Windows.Forms.GroupBox();
            this.dtpUltimaActualizacionEnSistema = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoMeses = new System.Windows.Forms.RadioButton();
            this.rdoAños = new System.Windows.Forms.RadioButton();
            this.cboPresentaciones = new SC_ControlsCS.scComboBoxExt();
            this.btnPresentaciones = new System.Windows.Forms.Button();
            this.btnPaisesDeFabricacion = new System.Windows.Forms.Button();
            this.nmAñosCaducidad = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPaisesDeFabricacion = new SC_ControlsCS.scComboBoxExt();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new SC_ControlsCS.scComboBoxExt();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.lblFechaVigencia = new System.Windows.Forms.Label();
            this.txtFolioRegistroSanitario = new SC_ControlsCS.scTextBoxExt();
            this.lblConsecutivo = new System.Windows.Forms.Label();
            this.FrameArchivos = new System.Windows.Forms.GroupBox();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.lblRutaDocto = new SC_ControlsCS.scLabelExt();
            this.btnAsignarDocto = new System.Windows.Forms.Button();
            this.FrameCodigosEAN = new System.Windows.Forms.GroupBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.group = new System.Windows.Forms.GroupBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLaboratorio = new SC_ControlsCS.scLabelExt();
            this.lblDescripcionClaveSSA = new SC_ControlsCS.scLabelExt();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmAñosCaducidad)).BeginInit();
            this.FrameArchivos.SuspendLayout();
            this.FrameCodigosEAN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.group.SuspendLayout();
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
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1332, 25);
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
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "[F8] Cancelar";
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
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.ToolTipText = "[F10] Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // FrameDatosRegistro
            // 
            this.FrameDatosRegistro.Controls.Add(this.dtpUltimaActualizacionEnSistema);
            this.FrameDatosRegistro.Controls.Add(this.label6);
            this.FrameDatosRegistro.Controls.Add(this.rdoMeses);
            this.FrameDatosRegistro.Controls.Add(this.rdoAños);
            this.FrameDatosRegistro.Controls.Add(this.cboPresentaciones);
            this.FrameDatosRegistro.Controls.Add(this.btnPresentaciones);
            this.FrameDatosRegistro.Controls.Add(this.btnPaisesDeFabricacion);
            this.FrameDatosRegistro.Controls.Add(this.nmAñosCaducidad);
            this.FrameDatosRegistro.Controls.Add(this.label9);
            this.FrameDatosRegistro.Controls.Add(this.label8);
            this.FrameDatosRegistro.Controls.Add(this.label4);
            this.FrameDatosRegistro.Controls.Add(this.label7);
            this.FrameDatosRegistro.Controls.Add(this.cboPaisesDeFabricacion);
            this.FrameDatosRegistro.Controls.Add(this.lblStatus);
            this.FrameDatosRegistro.Controls.Add(this.cboStatus);
            this.FrameDatosRegistro.Controls.Add(this.dtpFechaVigencia);
            this.FrameDatosRegistro.Controls.Add(this.lblFechaVigencia);
            this.FrameDatosRegistro.Controls.Add(this.txtFolioRegistroSanitario);
            this.FrameDatosRegistro.Controls.Add(this.lblConsecutivo);
            this.FrameDatosRegistro.Location = new System.Drawing.Point(16, 148);
            this.FrameDatosRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatosRegistro.Name = "FrameDatosRegistro";
            this.FrameDatosRegistro.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatosRegistro.Size = new System.Drawing.Size(1300, 124);
            this.FrameDatosRegistro.TabIndex = 2;
            this.FrameDatosRegistro.TabStop = false;
            this.FrameDatosRegistro.Text = "Información de Registro Sanitario";
            // 
            // dtpUltimaActualizacionEnSistema
            // 
            this.dtpUltimaActualizacionEnSistema.CustomFormat = "yyyy-MM-dd H:mm";
            this.dtpUltimaActualizacionEnSistema.Enabled = false;
            this.dtpUltimaActualizacionEnSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpUltimaActualizacionEnSistema.Location = new System.Drawing.Point(443, 87);
            this.dtpUltimaActualizacionEnSistema.Margin = new System.Windows.Forms.Padding(4);
            this.dtpUltimaActualizacionEnSistema.Name = "dtpUltimaActualizacionEnSistema";
            this.dtpUltimaActualizacionEnSistema.Size = new System.Drawing.Size(166, 22);
            this.dtpUltimaActualizacionEnSistema.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(256, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 19);
            this.label6.TabIndex = 43;
            this.label6.Text = "Ultima actualización :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoMeses
            // 
            this.rdoMeses.Location = new System.Drawing.Point(990, 86);
            this.rdoMeses.Name = "rdoMeses";
            this.rdoMeses.Size = new System.Drawing.Size(78, 21);
            this.rdoMeses.TabIndex = 41;
            this.rdoMeses.TabStop = true;
            this.rdoMeses.Text = "Meses";
            this.rdoMeses.UseVisualStyleBackColor = true;
            this.rdoMeses.CheckedChanged += new System.EventHandler(this.rdoMeses_CheckedChanged);
            // 
            // rdoAños
            // 
            this.rdoAños.Location = new System.Drawing.Point(926, 84);
            this.rdoAños.Name = "rdoAños";
            this.rdoAños.Size = new System.Drawing.Size(66, 24);
            this.rdoAños.TabIndex = 40;
            this.rdoAños.TabStop = true;
            this.rdoAños.Text = "Años";
            this.rdoAños.UseVisualStyleBackColor = true;
            this.rdoAños.CheckedChanged += new System.EventHandler(this.rdoAños_CheckedChanged);
            // 
            // cboPresentaciones
            // 
            this.cboPresentaciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboPresentaciones.Data = "";
            this.cboPresentaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPresentaciones.Filtro = " 1 = 1";
            this.cboPresentaciones.FormattingEnabled = true;
            this.cboPresentaciones.ListaItemsBusqueda = 20;
            this.cboPresentaciones.Location = new System.Drawing.Point(793, 55);
            this.cboPresentaciones.Margin = new System.Windows.Forms.Padding(4);
            this.cboPresentaciones.MostrarToolTip = false;
            this.cboPresentaciones.Name = "cboPresentaciones";
            this.cboPresentaciones.Size = new System.Drawing.Size(457, 24);
            this.cboPresentaciones.TabIndex = 39;
            // 
            // btnPresentaciones
            // 
            this.btnPresentaciones.Location = new System.Drawing.Point(1256, 56);
            this.btnPresentaciones.Name = "btnPresentaciones";
            this.btnPresentaciones.Size = new System.Drawing.Size(26, 26);
            this.btnPresentaciones.TabIndex = 38;
            this.btnPresentaciones.Text = "...";
            this.btnPresentaciones.UseVisualStyleBackColor = true;
            this.btnPresentaciones.Click += new System.EventHandler(this.btnPresentaciones_Click);
            // 
            // btnPaisesDeFabricacion
            // 
            this.btnPaisesDeFabricacion.Location = new System.Drawing.Point(1256, 22);
            this.btnPaisesDeFabricacion.Name = "btnPaisesDeFabricacion";
            this.btnPaisesDeFabricacion.Size = new System.Drawing.Size(26, 26);
            this.btnPaisesDeFabricacion.TabIndex = 37;
            this.btnPaisesDeFabricacion.Text = "...";
            this.btnPaisesDeFabricacion.UseVisualStyleBackColor = true;
            this.btnPaisesDeFabricacion.Click += new System.EventHandler(this.btnPaisesDeFabricacion_Click);
            // 
            // nmAñosCaducidad
            // 
            this.nmAñosCaducidad.Location = new System.Drawing.Point(793, 85);
            this.nmAñosCaducidad.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nmAñosCaducidad.Name = "nmAñosCaducidad";
            this.nmAñosCaducidad.Size = new System.Drawing.Size(117, 22);
            this.nmAñosCaducidad.TabIndex = 7;
            this.nmAñosCaducidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(1074, 85);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 23);
            this.label9.TabIndex = 36;
            this.label9.Text = " despues de su fabricación";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(635, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 23);
            this.label8.TabIndex = 35;
            this.label8.Text = "Caducidad de :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(632, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pais de fabricación :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(632, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 16);
            this.label7.TabIndex = 33;
            this.label7.Text = "Presentación :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPaisesDeFabricacion
            // 
            this.cboPaisesDeFabricacion.BackColorEnabled = System.Drawing.Color.White;
            this.cboPaisesDeFabricacion.Data = "";
            this.cboPaisesDeFabricacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaisesDeFabricacion.Enabled = false;
            this.cboPaisesDeFabricacion.Filtro = " 1 = 1";
            this.cboPaisesDeFabricacion.FormattingEnabled = true;
            this.cboPaisesDeFabricacion.ListaItemsBusqueda = 20;
            this.cboPaisesDeFabricacion.Location = new System.Drawing.Point(793, 25);
            this.cboPaisesDeFabricacion.Margin = new System.Windows.Forms.Padding(4);
            this.cboPaisesDeFabricacion.MostrarToolTip = false;
            this.cboPaisesDeFabricacion.Name = "cboPaisesDeFabricacion";
            this.cboPaisesDeFabricacion.Size = new System.Drawing.Size(457, 24);
            this.cboPaisesDeFabricacion.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(276, 27);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 20);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status :";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.BackColorEnabled = System.Drawing.Color.White;
            this.cboStatus.Data = "";
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Enabled = false;
            this.cboStatus.Filtro = " 1 = 1";
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.ListaItemsBusqueda = 20;
            this.cboStatus.Location = new System.Drawing.Point(338, 25);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cboStatus.MostrarToolTip = false;
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(271, 24);
            this.cboStatus.TabIndex = 3;
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVigencia.Enabled = false;
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(443, 56);
            this.dtpFechaVigencia.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(166, 22);
            this.dtpFechaVigencia.TabIndex = 4;
            // 
            // lblFechaVigencia
            // 
            this.lblFechaVigencia.Location = new System.Drawing.Point(303, 57);
            this.lblFechaVigencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaVigencia.Name = "lblFechaVigencia";
            this.lblFechaVigencia.Size = new System.Drawing.Size(137, 20);
            this.lblFechaVigencia.TabIndex = 6;
            this.lblFechaVigencia.Text = "Fecha de Vigencia :";
            this.lblFechaVigencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioRegistroSanitario
            // 
            this.txtFolioRegistroSanitario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioRegistroSanitario.Decimales = 0;
            this.txtFolioRegistroSanitario.Enabled = false;
            this.txtFolioRegistroSanitario.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioRegistroSanitario.ForeColor = System.Drawing.Color.Black;
            this.txtFolioRegistroSanitario.Location = new System.Drawing.Point(65, 56);
            this.txtFolioRegistroSanitario.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioRegistroSanitario.MaxLength = 30;
            this.txtFolioRegistroSanitario.Name = "txtFolioRegistroSanitario";
            this.txtFolioRegistroSanitario.PermitirApostrofo = false;
            this.txtFolioRegistroSanitario.PermitirNegativos = false;
            this.txtFolioRegistroSanitario.Size = new System.Drawing.Size(197, 22);
            this.txtFolioRegistroSanitario.TabIndex = 0;
            // 
            // lblConsecutivo
            // 
            this.lblConsecutivo.Location = new System.Drawing.Point(12, 56);
            this.lblConsecutivo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConsecutivo.Name = "lblConsecutivo";
            this.lblConsecutivo.Size = new System.Drawing.Size(50, 23);
            this.lblConsecutivo.TabIndex = 0;
            this.lblConsecutivo.Text = "Folio :";
            this.lblConsecutivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameArchivos
            // 
            this.FrameArchivos.Controls.Add(this.btnDescargar);
            this.FrameArchivos.Controls.Add(this.lblRutaDocto);
            this.FrameArchivos.Controls.Add(this.btnAsignarDocto);
            this.FrameArchivos.Location = new System.Drawing.Point(960, 29);
            this.FrameArchivos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameArchivos.Name = "FrameArchivos";
            this.FrameArchivos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameArchivos.Size = new System.Drawing.Size(356, 116);
            this.FrameArchivos.TabIndex = 3;
            this.FrameArchivos.TabStop = false;
            this.FrameArchivos.Text = "Archivo";
            // 
            // btnDescargar
            // 
            this.btnDescargar.Enabled = false;
            this.btnDescargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargar.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargar.Image")));
            this.btnDescargar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDescargar.Location = new System.Drawing.Point(214, 49);
            this.btnDescargar.Margin = new System.Windows.Forms.Padding(4);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(126, 59);
            this.btnDescargar.TabIndex = 1;
            this.btnDescargar.Text = " Descargar documento";
            this.btnDescargar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // lblRutaDocto
            // 
            this.lblRutaDocto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRutaDocto.Location = new System.Drawing.Point(13, 19);
            this.lblRutaDocto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRutaDocto.MostrarToolTip = false;
            this.lblRutaDocto.Name = "lblRutaDocto";
            this.lblRutaDocto.Size = new System.Drawing.Size(327, 26);
            this.lblRutaDocto.TabIndex = 35;
            this.lblRutaDocto.Text = "scLabelExt3";
            this.lblRutaDocto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAsignarDocto
            // 
            this.btnAsignarDocto.Enabled = false;
            this.btnAsignarDocto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarDocto.Image = ((System.Drawing.Image)(resources.GetObject("btnAsignarDocto.Image")));
            this.btnAsignarDocto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAsignarDocto.Location = new System.Drawing.Point(80, 49);
            this.btnAsignarDocto.Margin = new System.Windows.Forms.Padding(4);
            this.btnAsignarDocto.Name = "btnAsignarDocto";
            this.btnAsignarDocto.Size = new System.Drawing.Size(126, 59);
            this.btnAsignarDocto.TabIndex = 0;
            this.btnAsignarDocto.Text = " Subir documento";
            this.btnAsignarDocto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignarDocto.UseVisualStyleBackColor = true;
            this.btnAsignarDocto.Click += new System.EventHandler(this.btnAsignarDocto_Click);
            // 
            // FrameCodigosEAN
            // 
            this.FrameCodigosEAN.Controls.Add(this.grdClaves);
            this.FrameCodigosEAN.Location = new System.Drawing.Point(16, 277);
            this.FrameCodigosEAN.Margin = new System.Windows.Forms.Padding(4);
            this.FrameCodigosEAN.Name = "FrameCodigosEAN";
            this.FrameCodigosEAN.Padding = new System.Windows.Forms.Padding(4);
            this.FrameCodigosEAN.Size = new System.Drawing.Size(1305, 374);
            this.FrameCodigosEAN.TabIndex = 4;
            this.FrameCodigosEAN.TabStop = false;
            this.FrameCodigosEAN.Text = "Códigos EAN registrados";
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(11, 20);
            this.grdClaves.Margin = new System.Windows.Forms.Padding(4);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(1280, 346);
            this.grdClaves.TabIndex = 0;
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 4;
            this.grdClaves_Sheet1.RowCount = 16;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Codigo EAN";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Presentación";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Codigo EAN";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = false;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Width = 150F;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 500F;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Presentación";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 150F;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 100F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // group
            // 
            this.group.Controls.Add(this.dtpFechaRegistro);
            this.group.Controls.Add(this.label3);
            this.group.Controls.Add(this.lblLaboratorio);
            this.group.Controls.Add(this.lblDescripcionClaveSSA);
            this.group.Controls.Add(this.txtFolio);
            this.group.Controls.Add(this.label5);
            this.group.Controls.Add(this.txtClaveSSA);
            this.group.Controls.Add(this.label2);
            this.group.Controls.Add(this.txtIdLaboratorio);
            this.group.Controls.Add(this.label1);
            this.group.Location = new System.Drawing.Point(16, 34);
            this.group.Margin = new System.Windows.Forms.Padding(4);
            this.group.Name = "group";
            this.group.Padding = new System.Windows.Forms.Padding(4);
            this.group.Size = new System.Drawing.Size(936, 111);
            this.group.TabIndex = 1;
            this.group.TabStop = false;
            this.group.Text = "Datos Generales:";
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd H:mm";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(762, 18);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(166, 22);
            this.dtpFechaRegistro.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(498, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 19);
            this.label3.TabIndex = 36;
            this.label3.Text = "Fecha de Registro en sistema :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.Location = new System.Drawing.Point(296, 46);
            this.lblLaboratorio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLaboratorio.MostrarToolTip = false;
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(631, 25);
            this.lblLaboratorio.TabIndex = 34;
            this.lblLaboratorio.Text = "scLabelExt3";
            this.lblLaboratorio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcionClaveSSA
            // 
            this.lblDescripcionClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClaveSSA.Location = new System.Drawing.Point(296, 79);
            this.lblDescripcionClaveSSA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionClaveSSA.MostrarToolTip = false;
            this.lblDescripcionClaveSSA.Name = "lblDescripcionClaveSSA";
            this.lblDescripcionClaveSSA.Size = new System.Drawing.Size(631, 25);
            this.lblDescripcionClaveSSA.TabIndex = 7;
            this.lblDescripcionClaveSSA.Text = "scLabelExt3";
            this.lblDescripcionClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(120, 18);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(168, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 33;
            this.label5.Text = "Folio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(120, 79);
            this.txtClaveSSA.Margin = new System.Windows.Forms.Padding(4);
            this.txtClaveSSA.MaxLength = 50;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(168, 22);
            this.txtClaveSSA.TabIndex = 2;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clave SSA :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdLaboratorio
            // 
            this.txtIdLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdLaboratorio.Decimales = 2;
            this.txtIdLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtIdLaboratorio.Location = new System.Drawing.Point(120, 49);
            this.txtIdLaboratorio.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdLaboratorio.MaxLength = 4;
            this.txtIdLaboratorio.Name = "txtIdLaboratorio";
            this.txtIdLaboratorio.PermitirApostrofo = false;
            this.txtIdLaboratorio.PermitirNegativos = false;
            this.txtIdLaboratorio.Size = new System.Drawing.Size(168, 22);
            this.txtIdLaboratorio.TabIndex = 1;
            this.txtIdLaboratorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdLaboratorio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdLaboratorio_KeyDown);
            this.txtIdLaboratorio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdLaboratorio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Laboratorio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProductosRegistrosSanitarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 660);
            this.Controls.Add(this.group);
            this.Controls.Add(this.FrameArchivos);
            this.Controls.Add(this.FrameDatosRegistro);
            this.Controls.Add(this.FrameCodigosEAN);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmProductosRegistrosSanitarios";
            this.Text = "Registros Sanitarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProductosRegistrosSanitarios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosRegistro.ResumeLayout(false);
            this.FrameDatosRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmAñosCaducidad)).EndInit();
            this.FrameArchivos.ResumeLayout(false);
            this.FrameCodigosEAN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.GroupBox FrameDatosRegistro;
        private SC_ControlsCS.scTextBoxExt txtFolioRegistroSanitario;
        private System.Windows.Forms.Label lblConsecutivo;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label lblFechaVigencia;
        private System.Windows.Forms.Label lblStatus;
        private SC_ControlsCS.scComboBoxExt cboStatus;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameArchivos;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.Button btnAsignarDocto;
        private System.Windows.Forms.GroupBox FrameCodigosEAN;
        private System.Windows.Forms.GroupBox group;
        private SC_ControlsCS.scTextBoxExt txtIdLaboratorio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label5;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scComboBoxExt cboPaisesDeFabricacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nmAñosCaducidad;
        private SC_ControlsCS.scLabelExt lblDescripcionClaveSSA;
        private SC_ControlsCS.scLabelExt lblLaboratorio;
        private SC_ControlsCS.scLabelExt lblRutaDocto;
        private System.Windows.Forms.Button btnPaisesDeFabricacion;
        private System.Windows.Forms.Button btnPresentaciones;
        private SC_ControlsCS.scComboBoxExt cboPresentaciones;
        private System.Windows.Forms.RadioButton rdoMeses;
        private System.Windows.Forms.RadioButton rdoAños;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpUltimaActualizacionEnSistema;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton btnExportar;
    }
}