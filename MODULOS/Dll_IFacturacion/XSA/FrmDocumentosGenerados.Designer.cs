namespace Dll_IFacturacion.XSA
{
    partial class FrmDocumentosGenerados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocumentosGenerados));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.listDocumentos = new System.Windows.Forms.ListView();
            this.colDocumento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSerie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImporte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRFC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReceptor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatusDocto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuDocumentos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnActualizarTimbreCFDI = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDescargarXML_PAC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVvalidacionSAT = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_04 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirDocto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelarDocto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarDoctoMasiva = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_02 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarArchivos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_03 = new System.Windows.Forms.ToolStripSeparator();
            this.descargarRemision = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarRemision_Factura = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarRemision_Remisiones = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtFolioFinal = new SC_ControlsCS.scTextBoxExt();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.label5 = new System.Windows.Forms.Label();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.txtFolioInicial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.cboTiposDocumentos = new SC_ControlsCS.scComboBoxExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkPeriodo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.colFolioFiscal_Relacionado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCFDI_Relacionado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu.SuspendLayout();
            this.menuDocumentos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1149, 25);
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(115, 22);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // listDocumentos
            // 
            this.listDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listDocumentos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDocumento,
            this.colFecha,
            this.colSerie,
            this.colFolio,
            this.colImporte,
            this.colRFC,
            this.colReceptor,
            this.colStatus,
            this.colStatusDocto,
            this.colFolioFiscal_Relacionado,
            this.colCFDI_Relacionado});
            this.listDocumentos.ContextMenuStrip = this.menuDocumentos;
            this.listDocumentos.HideSelection = false;
            this.listDocumentos.Location = new System.Drawing.Point(10, 16);
            this.listDocumentos.Name = "listDocumentos";
            this.listDocumentos.Size = new System.Drawing.Size(1107, 376);
            this.listDocumentos.TabIndex = 0;
            this.listDocumentos.UseCompatibleStateImageBehavior = false;
            this.listDocumentos.View = System.Windows.Forms.View.Details;
            this.listDocumentos.SelectedIndexChanged += new System.EventHandler(this.listDocumentos_SelectedIndexChanged);
            this.listDocumentos.DoubleClick += new System.EventHandler(this.listDocumentos_DoubleClick);
            // 
            // colDocumento
            // 
            this.colDocumento.Text = "Documento";
            this.colDocumento.Width = 100;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha Expedición";
            this.colFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFecha.Width = 114;
            // 
            // colSerie
            // 
            this.colSerie.Text = "Serie";
            this.colSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colSerie.Width = 84;
            // 
            // colFolio
            // 
            this.colFolio.Text = "Folio";
            this.colFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFolio.Width = 80;
            // 
            // colImporte
            // 
            this.colImporte.Text = "Importe";
            this.colImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colImporte.Width = 120;
            // 
            // colRFC
            // 
            this.colRFC.Text = "RFC";
            this.colRFC.Width = 120;
            // 
            // colReceptor
            // 
            this.colReceptor.Text = "Nombre Receptor";
            this.colReceptor.Width = 281;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // colStatusDocto
            // 
            this.colStatusDocto.Text = "Status documento";
            this.colStatusDocto.Width = 123;
            // 
            // menuDocumentos
            // 
            this.menuDocumentos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizarTimbreCFDI,
            this.btnDescargarXML_PAC,
            this.toolStripSeparator4,
            this.btnVvalidacionSAT,
            this.toolStripSeparador_04,
            this.btnImprimirDocto,
            this.toolStripSeparador_01,
            this.btnCancelarDocto,
            this.btnCancelarDoctoMasiva,
            this.toolStripSeparador_02,
            this.btnDescargarArchivos,
            this.toolStripSeparador_03,
            this.descargarRemision});
            this.menuDocumentos.Name = "menuFolios";
            this.menuDocumentos.Size = new System.Drawing.Size(195, 210);
            // 
            // btnActualizarTimbreCFDI
            // 
            this.btnActualizarTimbreCFDI.Name = "btnActualizarTimbreCFDI";
            this.btnActualizarTimbreCFDI.Size = new System.Drawing.Size(194, 22);
            this.btnActualizarTimbreCFDI.Text = "Actualizar Timbre CFDI";
            this.btnActualizarTimbreCFDI.Click += new System.EventHandler(this.btnActualizarTimbreCFDI_Click);
            // 
            // btnDescargarXML_PAC
            // 
            this.btnDescargarXML_PAC.Name = "btnDescargarXML_PAC";
            this.btnDescargarXML_PAC.Size = new System.Drawing.Size(194, 22);
            this.btnDescargarXML_PAC.Text = "Descargar XML de PAC";
            this.btnDescargarXML_PAC.Click += new System.EventHandler(this.btnDescargarXML_PAC_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(191, 6);
            // 
            // btnVvalidacionSAT
            // 
            this.btnVvalidacionSAT.Name = "btnVvalidacionSAT";
            this.btnVvalidacionSAT.Size = new System.Drawing.Size(194, 22);
            this.btnVvalidacionSAT.Text = "Validación SAT";
            this.btnVvalidacionSAT.Click += new System.EventHandler(this.btnVvalidacionSAT_Click);
            // 
            // toolStripSeparador_04
            // 
            this.toolStripSeparador_04.Name = "toolStripSeparador_04";
            this.toolStripSeparador_04.Size = new System.Drawing.Size(191, 6);
            // 
            // btnImprimirDocto
            // 
            this.btnImprimirDocto.Name = "btnImprimirDocto";
            this.btnImprimirDocto.Size = new System.Drawing.Size(194, 22);
            this.btnImprimirDocto.Text = "Imprimir";
            this.btnImprimirDocto.Click += new System.EventHandler(this.btnImprimirDocto_Click);
            // 
            // toolStripSeparador_01
            // 
            this.toolStripSeparador_01.Name = "toolStripSeparador_01";
            this.toolStripSeparador_01.Size = new System.Drawing.Size(191, 6);
            // 
            // btnCancelarDocto
            // 
            this.btnCancelarDocto.Name = "btnCancelarDocto";
            this.btnCancelarDocto.Size = new System.Drawing.Size(194, 22);
            this.btnCancelarDocto.Text = "Cancelar";
            this.btnCancelarDocto.Click += new System.EventHandler(this.btnCancelarDocto_Click);
            // 
            // btnCancelarDoctoMasiva
            // 
            this.btnCancelarDoctoMasiva.Enabled = false;
            this.btnCancelarDoctoMasiva.Name = "btnCancelarDoctoMasiva";
            this.btnCancelarDoctoMasiva.Size = new System.Drawing.Size(194, 22);
            this.btnCancelarDoctoMasiva.Text = "Cancelación masiva";
            this.btnCancelarDoctoMasiva.Visible = false;
            this.btnCancelarDoctoMasiva.Click += new System.EventHandler(this.btnCancelarDoctoMasiva_Click);
            // 
            // toolStripSeparador_02
            // 
            this.toolStripSeparador_02.Name = "toolStripSeparador_02";
            this.toolStripSeparador_02.Size = new System.Drawing.Size(191, 6);
            // 
            // btnDescargarArchivos
            // 
            this.btnDescargarArchivos.Name = "btnDescargarArchivos";
            this.btnDescargarArchivos.Size = new System.Drawing.Size(194, 22);
            this.btnDescargarArchivos.Text = "Descargar PDF y XML";
            this.btnDescargarArchivos.Click += new System.EventHandler(this.btnDescargarArchivos_Click);
            // 
            // toolStripSeparador_03
            // 
            this.toolStripSeparador_03.Name = "toolStripSeparador_03";
            this.toolStripSeparador_03.Size = new System.Drawing.Size(191, 6);
            // 
            // descargarRemision
            // 
            this.descargarRemision.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargarRemision_Factura,
            this.descargarRemision_Remisiones});
            this.descargarRemision.Name = "descargarRemision";
            this.descargarRemision.Size = new System.Drawing.Size(194, 22);
            this.descargarRemision.Text = "Descargar Remisión";
            this.descargarRemision.Click += new System.EventHandler(this.descargarRemision_Click);
            // 
            // descargarRemision_Factura
            // 
            this.descargarRemision_Factura.Name = "descargarRemision_Factura";
            this.descargarRemision_Factura.Size = new System.Drawing.Size(134, 22);
            this.descargarRemision_Factura.Text = "Factura";
            this.descargarRemision_Factura.Click += new System.EventHandler(this.descargarRemision_Factura_Click);
            // 
            // descargarRemision_Remisiones
            // 
            this.descargarRemision_Remisiones.Name = "descargarRemision_Remisiones";
            this.descargarRemision_Remisiones.Size = new System.Drawing.Size(134, 22);
            this.descargarRemision_Remisiones.Text = "Remisiones";
            this.descargarRemision_Remisiones.Click += new System.EventHandler(this.descargarRemision_Remisiones_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMensajes);
            this.groupBox1.Controls.Add(this.listDocumentos);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1127, 423);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Documentos";
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(3, 396);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1121, 24);
            this.lblMensajes.TabIndex = 1;
            this.lblMensajes.Text = "< Doble Clic > Visualizar Documento";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtFolioFinal);
            this.groupBox4.Controls.Add(this.lblSerie);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.scLabelExt1);
            this.groupBox4.Controls.Add(this.txtFolioInicial);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.cboSeries);
            this.groupBox4.Controls.Add(this.cboTiposDocumentos);
            this.groupBox4.Location = new System.Drawing.Point(475, 29);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(490, 74);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Documentos, Series y Folios de Facturación";
            // 
            // txtFolioFinal
            // 
            this.txtFolioFinal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFinal.Decimales = 2;
            this.txtFolioFinal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFinal.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFinal.Location = new System.Drawing.Point(366, 42);
            this.txtFolioFinal.MaxLength = 10;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(93, 20);
            this.txtFolioFinal.TabIndex = 3;
            this.txtFolioFinal.Text = "0123456789";
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(6, 42);
            this.lblSerie.MostrarToolTip = false;
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(94, 20);
            this.lblSerie.TabIndex = 14;
            this.lblSerie.Text = "Serie : ";
            this.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(285, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Folio final : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(6, 20);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(94, 20);
            this.scLabelExt1.TabIndex = 15;
            this.scLabelExt1.Text = "Documento : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicial
            // 
            this.txtFolioInicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicial.Decimales = 2;
            this.txtFolioInicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicial.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicial.Location = new System.Drawing.Point(366, 19);
            this.txtFolioInicial.MaxLength = 10;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(93, 20);
            this.txtFolioInicial.TabIndex = 2;
            this.txtFolioInicial.Text = "0123456789";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(285, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Folio inicial : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSeries
            // 
            this.cboSeries.BackColorEnabled = System.Drawing.Color.White;
            this.cboSeries.Data = "";
            this.cboSeries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeries.Filtro = " 1 = 1";
            this.cboSeries.FormattingEnabled = true;
            this.cboSeries.ListaItemsBusqueda = 20;
            this.cboSeries.Location = new System.Drawing.Point(100, 43);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(169, 21);
            this.cboSeries.TabIndex = 1;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // cboTiposDocumentos
            // 
            this.cboTiposDocumentos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTiposDocumentos.Data = "";
            this.cboTiposDocumentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposDocumentos.Filtro = " 1 = 1";
            this.cboTiposDocumentos.FormattingEnabled = true;
            this.cboTiposDocumentos.ListaItemsBusqueda = 20;
            this.cboTiposDocumentos.Location = new System.Drawing.Point(100, 19);
            this.cboTiposDocumentos.MostrarToolTip = false;
            this.cboTiposDocumentos.Name = "cboTiposDocumentos";
            this.cboTiposDocumentos.Size = new System.Drawing.Size(169, 21);
            this.cboTiposDocumentos.TabIndex = 0;
            this.cboTiposDocumentos.SelectedIndexChanged += new System.EventHandler(this.cboTiposDocumentos_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCliente);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información del Receptor";
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(127, 23);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(327, 20);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "scLabelExt1";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(60, 23);
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
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkPeriodo);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dtpFechaFinal);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dtpFechaInicial);
            this.groupBox3.Location = new System.Drawing.Point(971, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 74);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Periodo";
            // 
            // chkPeriodo
            // 
            this.chkPeriodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPeriodo.Location = new System.Drawing.Point(57, -1);
            this.chkPeriodo.Name = "chkPeriodo";
            this.chkPeriodo.Size = new System.Drawing.Size(101, 17);
            this.chkPeriodo.TabIndex = 2;
            this.chkPeriodo.Text = "Todo el Periodo";
            this.chkPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPeriodo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fin : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(64, 43);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(84, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Inicio : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(64, 19);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(84, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // colFolioFiscal_Relacionado
            // 
            this.colFolioFiscal_Relacionado.Text = "Folio fiscal relacionado";
            // 
            // colCFDI_Relacionado
            // 
            this.colCFDI_Relacionado.Text = "CFDI Relacionado";
            // 
            // FrmDocumentosGenerados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 533);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmDocumentosGenerados";
            this.Text = "Documentos Electrónicos Generados";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDocumentosGenerados_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.menuDocumentos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView listDocumentos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private SC_ControlsCS.scComboBoxExt cboTiposDocumentos;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.ColumnHeader colDocumento;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colSerie;
        private System.Windows.Forms.ColumnHeader colFolio;
        private System.Windows.Forms.ColumnHeader colRFC;
        private System.Windows.Forms.ColumnHeader colReceptor;
        private System.Windows.Forms.CheckBox chkPeriodo;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ColumnHeader colImporte;
        private System.Windows.Forms.ContextMenuStrip menuDocumentos;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirDocto;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarDocto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparador_01;
        private SC_ControlsCS.scLabelExt lblSerie;
        private SC_ControlsCS.scComboBoxExt cboSeries;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colStatusDocto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparador_02;
        private System.Windows.Forms.ToolStripMenuItem btnDescargarArchivos;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scTextBoxExt txtFolioInicial;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtFolioFinal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparador_03;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparador_04;
        private System.Windows.Forms.ToolStripMenuItem btnActualizarTimbreCFDI;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem descargarRemision;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarDoctoMasiva;
        private System.Windows.Forms.ToolStripMenuItem btnDescargarXML_PAC;
        private System.Windows.Forms.ToolStripMenuItem descargarRemision_Factura;
        private System.Windows.Forms.ToolStripMenuItem descargarRemision_Remisiones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem btnVvalidacionSAT;
        private System.Windows.Forms.ColumnHeader colFolioFiscal_Relacionado;
        private System.Windows.Forms.ColumnHeader colCFDI_Relacionado;
    }
}