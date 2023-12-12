namespace Dll_MA_IFacturacion.XSA
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
            this.toolStripSeparador_04 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirDocto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelarDocto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_02 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarArchivos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_03 = new System.Windows.Forms.ToolStripSeparator();
            this.descargarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparador_05 = new System.Windows.Forms.ToolStripSeparator();
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu.SuspendLayout();
            this.menuDocumentos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1532, 25);
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
            this.colStatusDocto});
            this.listDocumentos.ContextMenuStrip = this.menuDocumentos;
            this.listDocumentos.Location = new System.Drawing.Point(13, 20);
            this.listDocumentos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listDocumentos.Name = "listDocumentos";
            this.listDocumentos.Size = new System.Drawing.Size(1475, 400);
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
            this.toolStripSeparador_04,
            this.btnImprimirDocto,
            this.toolStripSeparador_01,
            this.btnCancelarDocto,
            this.toolStripSeparador_02,
            this.btnDescargarArchivos,
            this.toolStripSeparador_03,
            this.descargarXMLToolStripMenuItem,
            this.toolStripSeparador_05});
            this.menuDocumentos.Name = "menuFolios";
            this.menuDocumentos.Size = new System.Drawing.Size(231, 154);
            // 
            // btnActualizarTimbreCFDI
            // 
            this.btnActualizarTimbreCFDI.Name = "btnActualizarTimbreCFDI";
            this.btnActualizarTimbreCFDI.Size = new System.Drawing.Size(230, 24);
            this.btnActualizarTimbreCFDI.Text = "Actualizar Timbre CFDI";
            this.btnActualizarTimbreCFDI.Click += new System.EventHandler(this.btnActualizarTimbreCFDI_Click);
            // 
            // toolStripSeparador_04
            // 
            this.toolStripSeparador_04.Name = "toolStripSeparador_04";
            this.toolStripSeparador_04.Size = new System.Drawing.Size(227, 6);
            // 
            // btnImprimirDocto
            // 
            this.btnImprimirDocto.Name = "btnImprimirDocto";
            this.btnImprimirDocto.Size = new System.Drawing.Size(230, 24);
            this.btnImprimirDocto.Text = "Imprimir";
            this.btnImprimirDocto.Click += new System.EventHandler(this.btnImprimirDocto_Click);
            // 
            // toolStripSeparador_01
            // 
            this.toolStripSeparador_01.Name = "toolStripSeparador_01";
            this.toolStripSeparador_01.Size = new System.Drawing.Size(227, 6);
            // 
            // btnCancelarDocto
            // 
            this.btnCancelarDocto.Name = "btnCancelarDocto";
            this.btnCancelarDocto.Size = new System.Drawing.Size(230, 24);
            this.btnCancelarDocto.Text = "Cancelar";
            this.btnCancelarDocto.Click += new System.EventHandler(this.btnCancelarDocto_Click);
            // 
            // toolStripSeparador_02
            // 
            this.toolStripSeparador_02.Name = "toolStripSeparador_02";
            this.toolStripSeparador_02.Size = new System.Drawing.Size(227, 6);
            // 
            // btnDescargarArchivos
            // 
            this.btnDescargarArchivos.Name = "btnDescargarArchivos";
            this.btnDescargarArchivos.Size = new System.Drawing.Size(230, 24);
            this.btnDescargarArchivos.Text = "Descargar PDF y XML";
            this.btnDescargarArchivos.Click += new System.EventHandler(this.btnDescargarArchivos_Click);
            // 
            // toolStripSeparador_03
            // 
            this.toolStripSeparador_03.Name = "toolStripSeparador_03";
            this.toolStripSeparador_03.Size = new System.Drawing.Size(227, 6);
            // 
            // descargarXMLToolStripMenuItem
            // 
            this.descargarXMLToolStripMenuItem.Name = "descargarXMLToolStripMenuItem";
            this.descargarXMLToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.descargarXMLToolStripMenuItem.Text = "Descargar XML";
            this.descargarXMLToolStripMenuItem.Click += new System.EventHandler(this.descargarXMLToolStripMenuItem_Click);
            // 
            // toolStripSeparador_05
            // 
            this.toolStripSeparador_05.Name = "toolStripSeparador_05";
            this.toolStripSeparador_05.Size = new System.Drawing.Size(227, 6);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMensajes);
            this.groupBox1.Controls.Add(this.listDocumentos);
            this.groupBox1.Location = new System.Drawing.Point(16, 187);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1503, 459);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Documentos";
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(4, 425);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1495, 30);
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
            this.groupBox4.Location = new System.Drawing.Point(865, 34);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(653, 90);
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
            this.txtFolioFinal.Location = new System.Drawing.Point(488, 52);
            this.txtFolioFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolioFinal.MaxLength = 10;
            this.txtFolioFinal.Name = "txtFolioFinal";
            this.txtFolioFinal.PermitirApostrofo = false;
            this.txtFolioFinal.PermitirNegativos = false;
            this.txtFolioFinal.Size = new System.Drawing.Size(123, 22);
            this.txtFolioFinal.TabIndex = 3;
            this.txtFolioFinal.Text = "0123456789";
            this.txtFolioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(8, 52);
            this.lblSerie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerie.MostrarToolTip = false;
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(125, 25);
            this.lblSerie.TabIndex = 14;
            this.lblSerie.Text = "Serie : ";
            this.lblSerie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(380, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Folio final : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(8, 25);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(125, 25);
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
            this.txtFolioInicial.Location = new System.Drawing.Point(488, 23);
            this.txtFolioInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolioInicial.MaxLength = 10;
            this.txtFolioInicial.Name = "txtFolioInicial";
            this.txtFolioInicial.PermitirApostrofo = false;
            this.txtFolioInicial.PermitirNegativos = false;
            this.txtFolioInicial.Size = new System.Drawing.Size(123, 22);
            this.txtFolioInicial.TabIndex = 2;
            this.txtFolioInicial.Text = "0123456789";
            this.txtFolioInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(380, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 16);
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
            this.cboSeries.Location = new System.Drawing.Point(133, 53);
            this.cboSeries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(224, 24);
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
            this.cboTiposDocumentos.Location = new System.Drawing.Point(133, 23);
            this.cboTiposDocumentos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboTiposDocumentos.MostrarToolTip = false;
            this.cboTiposDocumentos.Name = "cboTiposDocumentos";
            this.cboTiposDocumentos.Size = new System.Drawing.Size(224, 24);
            this.cboTiposDocumentos.TabIndex = 0;
            this.cboTiposDocumentos.SelectedIndexChanged += new System.EventHandler(this.cboTiposDocumentos_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCliente);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(16, 124);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(841, 59);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información del Receptor";
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(169, 22);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(657, 25);
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
            this.txtId.Location = new System.Drawing.Point(80, 22);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtId.MaxLength = 6;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(83, 22);
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
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
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
            this.groupBox3.Location = new System.Drawing.Point(865, 124);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(653, 59);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Periodo";
            // 
            // chkPeriodo
            // 
            this.chkPeriodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPeriodo.Location = new System.Drawing.Point(505, 25);
            this.chkPeriodo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPeriodo.Name = "chkPeriodo";
            this.chkPeriodo.Size = new System.Drawing.Size(135, 21);
            this.chkPeriodo.TabIndex = 2;
            this.chkPeriodo.Text = "Todo el Periodo";
            this.chkPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPeriodo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(271, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fin : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(336, 22);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(111, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Inicio : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(120, 22);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(111, 22);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(16, 34);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(841, 90);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Parámetros de Unidades";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(112, 53);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(713, 24);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(4, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 22);
            this.label8.TabIndex = 22;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(112, 23);
            this.cboEstados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(713, 24);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 22);
            this.label6.TabIndex = 22;
            this.label6.Text = "Estados :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(146, 22);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmDocumentosGenerados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 656);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
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
            this.groupBox5.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem descargarXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparador_05;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}