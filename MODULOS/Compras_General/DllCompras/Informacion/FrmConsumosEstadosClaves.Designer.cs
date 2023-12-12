namespace DllCompras.Informacion
{
    partial class FrmConsumosEstadosClaves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsumosEstadosClaves));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.cboJurisdiccion = new SC_ControlsCS.scComboBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDispAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.FrameReporte = new System.Windows.Forms.GroupBox();
            this.rdoDetallado = new System.Windows.Forms.RadioButton();
            this.rdoConcentrado = new System.Windows.Forms.RadioButton();
            this.FrameClave = new System.Windows.Forms.GroupBox();
            this.lblLaboratorio = new System.Windows.Forms.Label();
            this.txtLaboratorio = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblClavesConsulta = new System.Windows.Forms.Label();
            this.btnVerClaves = new System.Windows.Forms.Button();
            this.btnAgregarClave = new System.Windows.Forms.Button();
            this.lblContPaquete = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPresentacion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameReporte.SuspendLayout();
            this.FrameClave.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
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
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
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
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(702, 70);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(273, 65);
            this.FrameFechas.TabIndex = 4;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(110, 38);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(78, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(67, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(110, 14);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2012, 2, 28, 0, 0, 0, 0);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatos.Controls.Add(this.cboJurisdiccion);
            this.FrameDatos.Controls.Add(this.label7);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.cboEmpresas);
            this.FrameDatos.Controls.Add(this.cboEdo);
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Location = new System.Drawing.Point(10, 28);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(515, 107);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Estados";
            // 
            // cboJurisdiccion
            // 
            this.cboJurisdiccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboJurisdiccion.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdiccion.Data = "";
            this.cboJurisdiccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdiccion.Filtro = " 1 = 1";
            this.cboJurisdiccion.FormattingEnabled = true;
            this.cboJurisdiccion.ListaItemsBusqueda = 20;
            this.cboJurisdiccion.Location = new System.Drawing.Point(88, 74);
            this.cboJurisdiccion.MostrarToolTip = false;
            this.cboJurisdiccion.Name = "cboJurisdiccion";
            this.cboJurisdiccion.Size = new System.Drawing.Size(414, 21);
            this.cboJurisdiccion.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Jurisdicción :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(88, 20);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(414, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(88, 47);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(414, 21);
            this.cboEdo.TabIndex = 1;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEdo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDispAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(531, 28);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(165, 107);
            this.FrameDispensacion.TabIndex = 2;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Checked = true;
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(35, 23);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(35, 76);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoTpDispConsignacion.TabIndex = 1;
            this.rdoTpDispConsignacion.TabStop = true;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(35, 50);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.TabStop = true;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.lstClaves);
            this.FrameDetalles.Location = new System.Drawing.Point(10, 229);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(965, 325);
            this.FrameDetalles.TabIndex = 6;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles Claves";
            // 
            // lstClaves
            // 
            this.lstClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstClaves.Location = new System.Drawing.Point(9, 19);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(946, 300);
            this.lstClaves.TabIndex = 0;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            this.lstClaves.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstClaves_MouseDoubleClick);
            // 
            // FrameReporte
            // 
            this.FrameReporte.Controls.Add(this.rdoDetallado);
            this.FrameReporte.Controls.Add(this.rdoConcentrado);
            this.FrameReporte.Location = new System.Drawing.Point(702, 28);
            this.FrameReporte.Name = "FrameReporte";
            this.FrameReporte.Size = new System.Drawing.Size(273, 42);
            this.FrameReporte.TabIndex = 3;
            this.FrameReporte.TabStop = false;
            this.FrameReporte.Text = "Tipo de Reporte";
            // 
            // rdoDetallado
            // 
            this.rdoDetallado.Location = new System.Drawing.Point(148, 16);
            this.rdoDetallado.Name = "rdoDetallado";
            this.rdoDetallado.Size = new System.Drawing.Size(80, 15);
            this.rdoDetallado.TabIndex = 1;
            this.rdoDetallado.TabStop = true;
            this.rdoDetallado.Text = "Detallado";
            this.rdoDetallado.UseVisualStyleBackColor = true;
            // 
            // rdoConcentrado
            // 
            this.rdoConcentrado.Checked = true;
            this.rdoConcentrado.Location = new System.Drawing.Point(44, 16);
            this.rdoConcentrado.Name = "rdoConcentrado";
            this.rdoConcentrado.Size = new System.Drawing.Size(87, 15);
            this.rdoConcentrado.TabIndex = 0;
            this.rdoConcentrado.TabStop = true;
            this.rdoConcentrado.Text = "Concentrado";
            this.rdoConcentrado.UseVisualStyleBackColor = true;
            // 
            // FrameClave
            // 
            this.FrameClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameClave.Controls.Add(this.lblLaboratorio);
            this.FrameClave.Controls.Add(this.txtLaboratorio);
            this.FrameClave.Controls.Add(this.label6);
            this.FrameClave.Controls.Add(this.lblClavesConsulta);
            this.FrameClave.Controls.Add(this.btnVerClaves);
            this.FrameClave.Controls.Add(this.btnAgregarClave);
            this.FrameClave.Controls.Add(this.lblContPaquete);
            this.FrameClave.Controls.Add(this.label10);
            this.FrameClave.Controls.Add(this.lblPresentacion);
            this.FrameClave.Controls.Add(this.label1);
            this.FrameClave.Controls.Add(this.txtClaveSSA);
            this.FrameClave.Controls.Add(this.label8);
            this.FrameClave.Controls.Add(this.lblDescripcion);
            this.FrameClave.Location = new System.Drawing.Point(10, 136);
            this.FrameClave.Name = "FrameClave";
            this.FrameClave.Size = new System.Drawing.Size(965, 93);
            this.FrameClave.TabIndex = 5;
            this.FrameClave.TabStop = false;
            this.FrameClave.Text = "Datos de Clave";
            // 
            // lblLaboratorio
            // 
            this.lblLaboratorio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLaboratorio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLaboratorio.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblLaboratorio.Location = new System.Drawing.Point(164, 66);
            this.lblLaboratorio.Name = "lblLaboratorio";
            this.lblLaboratorio.Size = new System.Drawing.Size(790, 20);
            this.lblLaboratorio.TabIndex = 33;
            // 
            // txtLaboratorio
            // 
            this.txtLaboratorio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtLaboratorio.Decimales = 2;
            this.txtLaboratorio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtLaboratorio.ForeColor = System.Drawing.Color.Black;
            this.txtLaboratorio.Location = new System.Drawing.Point(73, 66);
            this.txtLaboratorio.MaxLength = 4;
            this.txtLaboratorio.Name = "txtLaboratorio";
            this.txtLaboratorio.PermitirApostrofo = false;
            this.txtLaboratorio.PermitirNegativos = false;
            this.txtLaboratorio.Size = new System.Drawing.Size(85, 20);
            this.txtLaboratorio.TabIndex = 31;
            this.txtLaboratorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLaboratorio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLaboratorio_KeyDown);
            this.txtLaboratorio.Validating += new System.ComponentModel.CancelEventHandler(this.txtLaboratorio_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Laboratorio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClavesConsulta
            // 
            this.lblClavesConsulta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClavesConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClavesConsulta.Location = new System.Drawing.Point(299, 13);
            this.lblClavesConsulta.Name = "lblClavesConsulta";
            this.lblClavesConsulta.Size = new System.Drawing.Size(142, 20);
            this.lblClavesConsulta.TabIndex = 30;
            this.lblClavesConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnVerClaves
            // 
            this.btnVerClaves.Image = ((System.Drawing.Image)(resources.GetObject("btnVerClaves.Image")));
            this.btnVerClaves.Location = new System.Drawing.Point(259, 11);
            this.btnVerClaves.Name = "btnVerClaves";
            this.btnVerClaves.Size = new System.Drawing.Size(34, 23);
            this.btnVerClaves.TabIndex = 2;
            this.btnVerClaves.UseVisualStyleBackColor = true;
            this.btnVerClaves.Click += new System.EventHandler(this.btnVerClaves_Click);
            // 
            // btnAgregarClave
            // 
            this.btnAgregarClave.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarClave.Image")));
            this.btnAgregarClave.Location = new System.Drawing.Point(224, 11);
            this.btnAgregarClave.Name = "btnAgregarClave";
            this.btnAgregarClave.Size = new System.Drawing.Size(34, 23);
            this.btnAgregarClave.TabIndex = 1;
            this.btnAgregarClave.UseVisualStyleBackColor = true;
            this.btnAgregarClave.Click += new System.EventHandler(this.btnAgregarClave_Click);
            // 
            // lblContPaquete
            // 
            this.lblContPaquete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContPaquete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblContPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContPaquete.Location = new System.Drawing.Point(885, 13);
            this.lblContPaquete.Name = "lblContPaquete";
            this.lblContPaquete.Size = new System.Drawing.Size(69, 20);
            this.lblContPaquete.TabIndex = 24;
            this.lblContPaquete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(818, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 20);
            this.label10.TabIndex = 23;
            this.label10.Text = "Contenido : ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPresentacion
            // 
            this.lblPresentacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPresentacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentacion.Location = new System.Drawing.Point(533, 13);
            this.lblPresentacion.Name = "lblPresentacion";
            this.lblPresentacion.Size = new System.Drawing.Size(273, 20);
            this.lblPresentacion.TabIndex = 22;
            this.lblPresentacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(447, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Presentación : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(73, 14);
            this.txtClaveSSA.MaxLength = 30;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(145, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.TextChanged += new System.EventHandler(this.txtClaveSSA_TextChanged);
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(5, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Clave SSA :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDescripcion.Location = new System.Drawing.Point(73, 39);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(881, 21);
            this.lblDescripcion.TabIndex = 26;
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Información";
            // 
            // FrmConsumosEstadosClaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.FrameClave);
            this.Controls.Add(this.FrameReporte);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConsumosEstadosClaves";
            this.Text = "Consumos Estados por Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoOrdenesDeCompras_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameDatos.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.FrameDetalles.ResumeLayout(false);
            this.FrameReporte.ResumeLayout(false);
            this.FrameClave.ResumeLayout(false);
            this.FrameClave.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.GroupBox FrameReporte;
        private System.Windows.Forms.RadioButton rdoDetallado;
        private System.Windows.Forms.RadioButton rdoConcentrado;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameClave;
        private System.Windows.Forms.Label lblContPaquete;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPresentacion;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scComboBoxExt cboJurisdiccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAgregarClave;
        private System.Windows.Forms.Button btnVerClaves;
        private System.Windows.Forms.Label lblClavesConsulta;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblLaboratorio;
        private SC_ControlsCS.scTextBoxExt txtLaboratorio;
        private System.Windows.Forms.Label label6;
    }
}