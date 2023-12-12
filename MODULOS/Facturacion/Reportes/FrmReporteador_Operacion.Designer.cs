namespace Facturacion.Reportes
{
    partial class FrmReporteador_Operacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteador_Operacion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.rdoFechas_02_Venta = new System.Windows.Forms.RadioButton();
            this.rdoFechas_01_Remision = new System.Windows.Forms.RadioButton();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameDatosOperacion = new System.Windows.Forms.GroupBox();
            this.lblTipoFuenteFinanciamiento = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdFuenteFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.lblFuenteFinanciamiento = new System.Windows.Forms.Label();
            this.lblFinanciamiento = new System.Windows.Forms.Label();
            this.txtIdFinanciamiento = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTipoInsumo = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameTipoRemision = new System.Windows.Forms.GroupBox();
            this.rdoRM_Todo = new System.Windows.Forms.RadioButton();
            this.rdoRM_Servicio = new System.Windows.Forms.RadioButton();
            this.rdoRM_Producto = new System.Windows.Forms.RadioButton();
            this.FrameOrigenInsumo = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Todos = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Consignacion = new System.Windows.Forms.RadioButton();
            this.rdoOIN_Venta = new System.Windows.Forms.RadioButton();
            this.FrameTipoInsumo = new System.Windows.Forms.GroupBox();
            this.rdoInsumoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFolios = new System.Windows.Forms.CheckBox();
            this.FrameFormatosDeImpresion = new System.Windows.Forms.GroupBox();
            this.cboReporte = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.FrameDatosOperacion.SuspendLayout();
            this.FrameTipoRemision.SuspendLayout();
            this.FrameOrigenInsumo.SuspendLayout();
            this.FrameTipoInsumo.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.FrameFormatosDeImpresion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel,
            this.toolStripSeparator3,
            this.btnIntegrarPaquetesDeDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(679, 25);
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
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "&Exportar a excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarPaquetesDeDatos
            // 
            this.btnIntegrarPaquetesDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarPaquetesDeDatos.Enabled = false;
            this.btnIntegrarPaquetesDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarPaquetesDeDatos.Image")));
            this.btnIntegrarPaquetesDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarPaquetesDeDatos.Name = "btnIntegrarPaquetesDeDatos";
            this.btnIntegrarPaquetesDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarPaquetesDeDatos.Text = "Integrar transferencias";
            this.btnIntegrarPaquetesDeDatos.Visible = false;
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.rdoFechas_02_Venta);
            this.FrameFechaDeProceso.Controls.Add(this.rdoFechas_01_Remision);
            this.FrameFechaDeProceso.Controls.Add(this.chkFechas);
            this.FrameFechaDeProceso.Controls.Add(this.label10);
            this.FrameFechaDeProceso.Controls.Add(this.label12);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(343, 276);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(326, 75);
            this.FrameFechaDeProceso.TabIndex = 6;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Fecha";
            // 
            // rdoFechas_02_Venta
            // 
            this.rdoFechas_02_Venta.Location = new System.Drawing.Point(20, 47);
            this.rdoFechas_02_Venta.Name = "rdoFechas_02_Venta";
            this.rdoFechas_02_Venta.Size = new System.Drawing.Size(140, 18);
            this.rdoFechas_02_Venta.TabIndex = 24;
            this.rdoFechas_02_Venta.TabStop = true;
            this.rdoFechas_02_Venta.Text = "Fecha de dispensación";
            this.rdoFechas_02_Venta.UseVisualStyleBackColor = true;
            // 
            // rdoFechas_01_Remision
            // 
            this.rdoFechas_01_Remision.Location = new System.Drawing.Point(20, 23);
            this.rdoFechas_01_Remision.Name = "rdoFechas_01_Remision";
            this.rdoFechas_01_Remision.Size = new System.Drawing.Size(140, 18);
            this.rdoFechas_01_Remision.TabIndex = 23;
            this.rdoFechas_01_Remision.TabStop = true;
            this.rdoFechas_01_Remision.Text = "Fecha de remisión";
            this.rdoFechas_01_Remision.UseVisualStyleBackColor = true;
            // 
            // chkFechas
            // 
            this.chkFechas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.Location = new System.Drawing.Point(212, 0);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(104, 15);
            this.chkFechas.TabIndex = 0;
            this.chkFechas.Text = "Filtro de Fechas";
            this.chkFechas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFechas.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(166, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "Hasta :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(165, 23);
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
            this.dtpFechaFinal.Location = new System.Drawing.Point(212, 47);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaFinal.TabIndex = 2;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(212, 23);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaInicial.TabIndex = 1;
            // 
            // FrameDatosOperacion
            // 
            this.FrameDatosOperacion.Controls.Add(this.lblTipoFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label7);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFuenteFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.lblFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.txtIdFinanciamiento);
            this.FrameDatosOperacion.Controls.Add(this.label11);
            this.FrameDatosOperacion.Controls.Add(this.label3);
            this.FrameDatosOperacion.Controls.Add(this.txtCte);
            this.FrameDatosOperacion.Controls.Add(this.lblCliente);
            this.FrameDatosOperacion.Controls.Add(this.lblSubCliente);
            this.FrameDatosOperacion.Controls.Add(this.txtSubCte);
            this.FrameDatosOperacion.Controls.Add(this.label5);
            this.FrameDatosOperacion.Controls.Add(this.cboTipoInsumo);
            this.FrameDatosOperacion.Controls.Add(this.label1);
            this.FrameDatosOperacion.Location = new System.Drawing.Point(12, 28);
            this.FrameDatosOperacion.Name = "FrameDatosOperacion";
            this.FrameDatosOperacion.Size = new System.Drawing.Size(657, 144);
            this.FrameDatosOperacion.TabIndex = 1;
            this.FrameDatosOperacion.TabStop = false;
            this.FrameDatosOperacion.Text = "Información de operación";
            // 
            // lblTipoFuenteFinanciamiento
            // 
            this.lblTipoFuenteFinanciamiento.Location = new System.Drawing.Point(453, 116);
            this.lblTipoFuenteFinanciamiento.Name = "lblTipoFuenteFinanciamiento";
            this.lblTipoFuenteFinanciamiento.Size = new System.Drawing.Size(190, 15);
            this.lblTipoFuenteFinanciamiento.TabIndex = 53;
            this.lblTipoFuenteFinanciamiento.Text = "Tipo segmento :";
            this.lblTipoFuenteFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 49;
            this.label7.Text = "Fuente de financiamiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFuenteFinanciamiento
            // 
            this.txtIdFuenteFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFuenteFinanciamiento.Decimales = 2;
            this.txtIdFuenteFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFuenteFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFuenteFinanciamiento.Location = new System.Drawing.Point(166, 18);
            this.txtIdFuenteFinanciamiento.MaxLength = 4;
            this.txtIdFuenteFinanciamiento.Name = "txtIdFuenteFinanciamiento";
            this.txtIdFuenteFinanciamiento.PermitirApostrofo = false;
            this.txtIdFuenteFinanciamiento.PermitirNegativos = false;
            this.txtIdFuenteFinanciamiento.Size = new System.Drawing.Size(75, 20);
            this.txtIdFuenteFinanciamiento.TabIndex = 0;
            this.txtIdFuenteFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFuenteFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFuenteFinanciamiento_TextChanged);
            this.txtIdFuenteFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFuenteFinanciamiento_KeyDown);
            this.txtIdFuenteFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFuenteFinanciamiento_Validating);
            // 
            // lblFuenteFinanciamiento
            // 
            this.lblFuenteFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFuenteFinanciamiento.Location = new System.Drawing.Point(248, 18);
            this.lblFuenteFinanciamiento.Name = "lblFuenteFinanciamiento";
            this.lblFuenteFinanciamiento.Size = new System.Drawing.Size(395, 21);
            this.lblFuenteFinanciamiento.TabIndex = 50;
            this.lblFuenteFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFinanciamiento
            // 
            this.lblFinanciamiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinanciamiento.Location = new System.Drawing.Point(248, 41);
            this.lblFinanciamiento.Name = "lblFinanciamiento";
            this.lblFinanciamiento.Size = new System.Drawing.Size(395, 21);
            this.lblFinanciamiento.TabIndex = 52;
            this.lblFinanciamiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdFinanciamiento
            // 
            this.txtIdFinanciamiento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFinanciamiento.Decimales = 2;
            this.txtIdFinanciamiento.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFinanciamiento.ForeColor = System.Drawing.Color.Black;
            this.txtIdFinanciamiento.Location = new System.Drawing.Point(166, 41);
            this.txtIdFinanciamiento.MaxLength = 4;
            this.txtIdFinanciamiento.Name = "txtIdFinanciamiento";
            this.txtIdFinanciamiento.PermitirApostrofo = false;
            this.txtIdFinanciamiento.PermitirNegativos = false;
            this.txtIdFinanciamiento.Size = new System.Drawing.Size(75, 20);
            this.txtIdFinanciamiento.TabIndex = 1;
            this.txtIdFinanciamiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFinanciamiento.TextChanged += new System.EventHandler(this.txtIdFinanciamiento_TextChanged);
            this.txtIdFinanciamiento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFinanciamiento_KeyDown);
            this.txtIdFinanciamiento.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFinanciamiento_Validating);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 18);
            this.label11.TabIndex = 51;
            this.label11.Text = "Financiamiento :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
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
            this.txtCte.Location = new System.Drawing.Point(166, 64);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(75, 20);
            this.txtCte.TabIndex = 2;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(248, 64);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(395, 21);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(248, 87);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(395, 21);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(166, 87);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(75, 20);
            this.txtSubCte.TabIndex = 3;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 18);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoInsumo
            // 
            this.cboTipoInsumo.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoInsumo.Data = "";
            this.cboTipoInsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoInsumo.Filtro = " 1 = 1";
            this.cboTipoInsumo.FormattingEnabled = true;
            this.cboTipoInsumo.ListaItemsBusqueda = 20;
            this.cboTipoInsumo.Location = new System.Drawing.Point(166, 113);
            this.cboTipoInsumo.MostrarToolTip = false;
            this.cboTipoInsumo.Name = "cboTipoInsumo";
            this.cboTipoInsumo.Size = new System.Drawing.Size(281, 21);
            this.cboTipoInsumo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Tipo segmento :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameTipoRemision
            // 
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Todo);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Servicio);
            this.FrameTipoRemision.Controls.Add(this.rdoRM_Producto);
            this.FrameTipoRemision.Location = new System.Drawing.Point(12, 175);
            this.FrameTipoRemision.Name = "FrameTipoRemision";
            this.FrameTipoRemision.Size = new System.Drawing.Size(215, 98);
            this.FrameTipoRemision.TabIndex = 2;
            this.FrameTipoRemision.TabStop = false;
            this.FrameTipoRemision.Text = "Tipo de remisión";
            // 
            // rdoRM_Todo
            // 
            this.rdoRM_Todo.Location = new System.Drawing.Point(62, 68);
            this.rdoRM_Todo.Name = "rdoRM_Todo";
            this.rdoRM_Todo.Size = new System.Drawing.Size(91, 18);
            this.rdoRM_Todo.TabIndex = 22;
            this.rdoRM_Todo.TabStop = true;
            this.rdoRM_Todo.Text = "Ambos";
            this.rdoRM_Todo.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Servicio
            // 
            this.rdoRM_Servicio.Location = new System.Drawing.Point(62, 43);
            this.rdoRM_Servicio.Name = "rdoRM_Servicio";
            this.rdoRM_Servicio.Size = new System.Drawing.Size(91, 18);
            this.rdoRM_Servicio.TabIndex = 1;
            this.rdoRM_Servicio.TabStop = true;
            this.rdoRM_Servicio.Text = "Servicio";
            this.rdoRM_Servicio.UseVisualStyleBackColor = true;
            // 
            // rdoRM_Producto
            // 
            this.rdoRM_Producto.Location = new System.Drawing.Point(62, 19);
            this.rdoRM_Producto.Name = "rdoRM_Producto";
            this.rdoRM_Producto.Size = new System.Drawing.Size(91, 18);
            this.rdoRM_Producto.TabIndex = 0;
            this.rdoRM_Producto.TabStop = true;
            this.rdoRM_Producto.Text = "Producto";
            this.rdoRM_Producto.UseVisualStyleBackColor = true;
            // 
            // FrameOrigenInsumo
            // 
            this.FrameOrigenInsumo.Controls.Add(this.radioButton3);
            this.FrameOrigenInsumo.Controls.Add(this.radioButton2);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Todos);
            this.FrameOrigenInsumo.Controls.Add(this.radioButton1);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Consignacion);
            this.FrameOrigenInsumo.Controls.Add(this.rdoOIN_Venta);
            this.FrameOrigenInsumo.Location = new System.Drawing.Point(233, 175);
            this.FrameOrigenInsumo.Name = "FrameOrigenInsumo";
            this.FrameOrigenInsumo.Size = new System.Drawing.Size(215, 98);
            this.FrameOrigenInsumo.TabIndex = 3;
            this.FrameOrigenInsumo.TabStop = false;
            this.FrameOrigenInsumo.Text = "Origen de Insumos";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(267, 72);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(132, 18);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Ambos";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(267, 47);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(132, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Material de Curación";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Todos
            // 
            this.rdoOIN_Todos.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.rdoOIN_Todos.Location = new System.Drawing.Point(59, 68);
            this.rdoOIN_Todos.Name = "rdoOIN_Todos";
            this.rdoOIN_Todos.Size = new System.Drawing.Size(97, 18);
            this.rdoOIN_Todos.TabIndex = 22;
            this.rdoOIN_Todos.TabStop = true;
            this.rdoOIN_Todos.Text = "Ambos";
            this.rdoOIN_Todos.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(267, 23);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(132, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Medicamento";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Consignacion
            // 
            this.rdoOIN_Consignacion.Location = new System.Drawing.Point(59, 43);
            this.rdoOIN_Consignacion.Name = "rdoOIN_Consignacion";
            this.rdoOIN_Consignacion.Size = new System.Drawing.Size(97, 18);
            this.rdoOIN_Consignacion.TabIndex = 1;
            this.rdoOIN_Consignacion.TabStop = true;
            this.rdoOIN_Consignacion.Text = "Consignación";
            this.rdoOIN_Consignacion.UseVisualStyleBackColor = true;
            // 
            // rdoOIN_Venta
            // 
            this.rdoOIN_Venta.Location = new System.Drawing.Point(59, 19);
            this.rdoOIN_Venta.Name = "rdoOIN_Venta";
            this.rdoOIN_Venta.Size = new System.Drawing.Size(97, 18);
            this.rdoOIN_Venta.TabIndex = 0;
            this.rdoOIN_Venta.TabStop = true;
            this.rdoOIN_Venta.Text = "Venta";
            this.rdoOIN_Venta.UseVisualStyleBackColor = true;
            // 
            // FrameTipoInsumo
            // 
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoAmbos);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMaterialDeCuracion);
            this.FrameTipoInsumo.Controls.Add(this.rdoInsumoMedicamento);
            this.FrameTipoInsumo.Location = new System.Drawing.Point(454, 175);
            this.FrameTipoInsumo.Name = "FrameTipoInsumo";
            this.FrameTipoInsumo.Size = new System.Drawing.Size(215, 98);
            this.FrameTipoInsumo.TabIndex = 4;
            this.FrameTipoInsumo.TabStop = false;
            this.FrameTipoInsumo.Text = "Tipo de Insumos";
            // 
            // rdoInsumoAmbos
            // 
            this.rdoInsumoAmbos.Location = new System.Drawing.Point(46, 68);
            this.rdoInsumoAmbos.Name = "rdoInsumoAmbos";
            this.rdoInsumoAmbos.Size = new System.Drawing.Size(132, 18);
            this.rdoInsumoAmbos.TabIndex = 2;
            this.rdoInsumoAmbos.TabStop = true;
            this.rdoInsumoAmbos.Text = "Ambos";
            this.rdoInsumoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMaterialDeCuracion
            // 
            this.rdoInsumoMaterialDeCuracion.Location = new System.Drawing.Point(46, 43);
            this.rdoInsumoMaterialDeCuracion.Name = "rdoInsumoMaterialDeCuracion";
            this.rdoInsumoMaterialDeCuracion.Size = new System.Drawing.Size(132, 18);
            this.rdoInsumoMaterialDeCuracion.TabIndex = 1;
            this.rdoInsumoMaterialDeCuracion.TabStop = true;
            this.rdoInsumoMaterialDeCuracion.Text = "Material de Curación";
            this.rdoInsumoMaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumoMedicamento
            // 
            this.rdoInsumoMedicamento.Location = new System.Drawing.Point(46, 19);
            this.rdoInsumoMedicamento.Name = "rdoInsumoMedicamento";
            this.rdoInsumoMedicamento.Size = new System.Drawing.Size(132, 18);
            this.rdoInsumoMedicamento.TabIndex = 0;
            this.rdoInsumoMedicamento.TabStop = true;
            this.rdoInsumoMedicamento.Text = "Medicamento";
            this.rdoInsumoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.txtFolioFinal);
            this.FrameFolios.Controls.Add(this.label6);
            this.FrameFolios.Controls.Add(this.txtFolioInicial);
            this.FrameFolios.Controls.Add(this.label4);
            this.FrameFolios.Controls.Add(this.chkFolios);
            this.FrameFolios.Location = new System.Drawing.Point(12, 276);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(325, 75);
            this.FrameFolios.TabIndex = 5;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Folios";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(214, 33);
            this.txtFolioFinal.MaxLength = 8;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(96, 20);
            this.txtFolioFinal.TabIndex = 2;
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(164, 37);
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
            this.txtFolioInicial.Location = new System.Drawing.Point(61, 33);
            this.txtFolioInicial.MaxLength = 8;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(96, 20);
            this.txtFolioInicial.TabIndex = 1;
            this.txtFolioInicial.Text = "01234567";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "Desde :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFolios
            // 
            this.chkFolios.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.Location = new System.Drawing.Point(218, 0);
            this.chkFolios.Name = "chkFolios";
            this.chkFolios.Size = new System.Drawing.Size(100, 17);
            this.chkFolios.TabIndex = 0;
            this.chkFolios.Text = "Filtro por Folios";
            this.chkFolios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFolios.UseVisualStyleBackColor = true;
            // 
            // FrameFormatosDeImpresion
            // 
            this.FrameFormatosDeImpresion.Controls.Add(this.cboReporte);
            this.FrameFormatosDeImpresion.Location = new System.Drawing.Point(12, 353);
            this.FrameFormatosDeImpresion.Name = "FrameFormatosDeImpresion";
            this.FrameFormatosDeImpresion.Size = new System.Drawing.Size(657, 49);
            this.FrameFormatosDeImpresion.TabIndex = 7;
            this.FrameFormatosDeImpresion.TabStop = false;
            this.FrameFormatosDeImpresion.Text = "Reporte";
            // 
            // cboReporte
            // 
            this.cboReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboReporte.Data = "";
            this.cboReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReporte.Filtro = " 1 = 1";
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.ListaItemsBusqueda = 20;
            this.cboReporte.Location = new System.Drawing.Point(16, 18);
            this.cboReporte.MostrarToolTip = false;
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(627, 21);
            this.cboReporte.TabIndex = 20;
            this.cboReporte.SelectedIndexChanged += new System.EventHandler(this.cboReporte_SelectedIndexChanged);
            // 
            // FrmReporteador_Operacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 411);
            this.Controls.Add(this.FrameFormatosDeImpresion);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoRemision);
            this.Controls.Add(this.FrameOrigenInsumo);
            this.Controls.Add(this.FrameTipoInsumo);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameDatosOperacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmReporteador_Operacion";
            this.Text = "Reporteador de operación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReporteador_Remisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.FrameDatosOperacion.ResumeLayout(false);
            this.FrameDatosOperacion.PerformLayout();
            this.FrameTipoRemision.ResumeLayout(false);
            this.FrameOrigenInsumo.ResumeLayout(false);
            this.FrameTipoInsumo.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameFolios.PerformLayout();
            this.FrameFormatosDeImpresion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameDatosOperacion;
        private SC_ControlsCS.scComboBoxExt cboTipoInsumo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmEjecuciones;
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
        private System.Windows.Forms.GroupBox FrameFolios;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFolios;
        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtIdFuenteFinanciamiento;
        private System.Windows.Forms.Label lblFuenteFinanciamiento;
        private System.Windows.Forms.Label lblFinanciamiento;
        private SC_ControlsCS.scTextBoxExt txtIdFinanciamiento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTipoFuenteFinanciamiento;
        private System.Windows.Forms.GroupBox FrameFormatosDeImpresion;
        private SC_ControlsCS.scComboBoxExt cboReporte;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdoFechas_02_Venta;
        private System.Windows.Forms.RadioButton rdoFechas_01_Remision;
    }
}