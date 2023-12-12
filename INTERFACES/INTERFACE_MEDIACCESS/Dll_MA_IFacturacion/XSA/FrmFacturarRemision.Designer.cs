namespace Dll_MA_IFacturacion.XSA
{
    partial class FrmFacturarRemision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturarRemision));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRFC = new SC_ControlsCS.scLabelExt();
            this.txtClienteNombre = new SC_ControlsCS.scTextBoxExt();
            this.btlCliente = new System.Windows.Forms.Button();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listvClaves = new System.Windows.Forms.ListView();
            this.colIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCodigoEAN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValorUnitarioBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPorcentajeCobro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValorUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTasaIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblUltimoFolio = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnObtenerDetalles = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.mnAgregarDescripcion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.agregarDescripciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameConceptoEspecial = new System.Windows.Forms.GroupBox();
            this.lblConceptoEncabezado = new SC_ControlsCS.scLabelExt();
            this.chkConceptoEncabezado = new System.Windows.Forms.CheckBox();
            this.txtIdConceptoEncabezado = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.colSATClaveProductoServicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSATClaveUnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mnAgregarDescripcion.SuspendLayout();
            this.FrameConceptoEspecial.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblRFC);
            this.groupBox1.Controls.Add(this.txtClienteNombre);
            this.groupBox1.Controls.Add(this.btlCliente);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(989, 60);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // lblRFC
            // 
            this.lblRFC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRFC.Location = new System.Drawing.Point(200, 22);
            this.lblRFC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRFC.MostrarToolTip = false;
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(172, 25);
            this.lblRFC.TabIndex = 17;
            this.lblRFC.Text = "Serie : ";
            this.lblRFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(376, 22);
            this.txtClienteNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(559, 22);
            this.txtClienteNombre.TabIndex = 14;
            // 
            // btlCliente
            // 
            this.btlCliente.Image = ((System.Drawing.Image)(resources.GetObject("btlCliente.Image")));
            this.btlCliente.Location = new System.Drawing.Point(940, 15);
            this.btlCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(39, 41);
            this.btlCliente.TabIndex = 13;
            this.btlCliente.Text = "...";
            this.btlCliente.UseVisualStyleBackColor = true;
            this.btlCliente.Click += new System.EventHandler(this.btlCliente_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(8, 25);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.MostrarToolTip = true;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(21, 25);
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
            this.txtId.Location = new System.Drawing.Point(113, 22);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
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
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.lblMensajes);
            this.FrameDetalles.Controls.Add(this.listvClaves);
            this.FrameDetalles.Location = new System.Drawing.Point(16, 161);
            this.FrameDetalles.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDetalles.Size = new System.Drawing.Size(1456, 151);
            this.FrameDetalles.TabIndex = 4;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la Factura";
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(4, 117);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1448, 30);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "Clic Derecho Agregar Descripción";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMensajes.Visible = false;
            // 
            // listvClaves
            // 
            this.listvClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdentificador,
            this.colSATClaveProductoServicio,
            this.colSATClaveUnidadDeMedida,
            this.colCodigoEAN,
            this.colDescripcion,
            this.colValorUnitarioBase,
            this.colPorcentajeCobro,
            this.colValorUnitario,
            this.colCantidad,
            this.colTasaIva,
            this.colSubTotal,
            this.colIva,
            this.colTotal,
            this.colUnidadDeMedida});
            this.listvClaves.Location = new System.Drawing.Point(13, 20);
            this.listvClaves.Margin = new System.Windows.Forms.Padding(4);
            this.listvClaves.Name = "listvClaves";
            this.listvClaves.Size = new System.Drawing.Size(1431, 118);
            this.listvClaves.TabIndex = 0;
            this.listvClaves.UseCompatibleStateImageBehavior = false;
            this.listvClaves.View = System.Windows.Forms.View.Details;
            // 
            // colIdentificador
            // 
            this.colIdentificador.Text = "Identificador";
            this.colIdentificador.Width = 80;
            // 
            // colCodigoEAN
            // 
            this.colCodigoEAN.Text = "Código EAN";
            this.colCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCodigoEAN.Width = 100;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 300;
            // 
            // colValorUnitarioBase
            // 
            this.colValorUnitarioBase.Text = "Precio base";
            this.colValorUnitarioBase.Width = 80;
            // 
            // colPorcentajeCobro
            // 
            this.colPorcentajeCobro.Text = "Porcentaje";
            this.colPorcentajeCobro.Width = 80;
            // 
            // colValorUnitario
            // 
            this.colValorUnitario.Text = "Precio unitario";
            this.colValorUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colValorUnitario.Width = 80;
            // 
            // colCantidad
            // 
            this.colCantidad.Text = "Cantidad";
            this.colCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCantidad.Width = 70;
            // 
            // colTasaIva
            // 
            this.colTasaIva.Text = "Tasa iva";
            this.colTasaIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colSubTotal
            // 
            this.colSubTotal.Text = "Sub-Total";
            this.colSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSubTotal.Width = 90;
            // 
            // colIva
            // 
            this.colIva.Text = "Iva";
            this.colIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total";
            this.colTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colTotal.Width = 90;
            // 
            // colUnidadDeMedida
            // 
            this.colUnidadDeMedida.Text = "Unidad de Medida";
            this.colUnidadDeMedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colUnidadDeMedida.Width = 120;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblUltimoFolio);
            this.groupBox3.Controls.Add(this.scLabelExt1);
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(1013, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(272, 121);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblUltimoFolio
            // 
            this.lblUltimoFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUltimoFolio.Location = new System.Drawing.Point(95, 66);
            this.lblUltimoFolio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUltimoFolio.MostrarToolTip = false;
            this.lblUltimoFolio.Name = "lblUltimoFolio";
            this.lblUltimoFolio.Size = new System.Drawing.Size(157, 25);
            this.lblUltimoFolio.TabIndex = 16;
            this.lblUltimoFolio.Text = "Serie : ";
            this.lblUltimoFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(9, 68);
            this.scLabelExt1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(77, 25);
            this.scLabelExt1.TabIndex = 15;
            this.scLabelExt1.Text = "Ult. Folio : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(9, 31);
            this.lblSerie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerie.MostrarToolTip = false;
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(77, 25);
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
            this.cboSeries.Location = new System.Drawing.Point(95, 30);
            this.cboSeries.Margin = new System.Windows.Forms.Padding(4);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(156, 24);
            this.cboSeries.TabIndex = 0;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnObtenerDetalles,
            this.toolStripSeparator1,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1485, 25);
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
            // btnObtenerDetalles
            // 
            this.btnObtenerDetalles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerDetalles.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerDetalles.Image")));
            this.btnObtenerDetalles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerDetalles.Name = "btnObtenerDetalles";
            this.btnObtenerDetalles.Size = new System.Drawing.Size(23, 22);
            this.btnObtenerDetalles.Text = "Obtener detalles de remisión";
            this.btnObtenerDetalles.Click += new System.EventHandler(this.btnObtenerDetalles_Click);
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
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(146, 22);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(269, 343);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Size = new System.Drawing.Size(947, 123);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 28);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(907, 79);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // tmProceso
            // 
            this.tmProceso.Enabled = true;
            this.tmProceso.Interval = 500;
            this.tmProceso.Tick += new System.EventHandler(this.tmProceso_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPago);
            this.groupBox2.Controls.Add(this.btnObservacionesGral);
            this.groupBox2.Location = new System.Drawing.Point(1293, 34);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(179, 121);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(13, 64);
            this.btnPago.Margin = new System.Windows.Forms.Padding(4);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(152, 28);
            this.btnPago.TabIndex = 21;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(13, 28);
            this.btnObservacionesGral.Margin = new System.Windows.Forms.Padding(4);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(152, 28);
            this.btnObservacionesGral.TabIndex = 20;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // mnAgregarDescripcion
            // 
            this.mnAgregarDescripcion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarDescripciónToolStripMenuItem});
            this.mnAgregarDescripcion.Name = "mnAgregarDescripcion";
            this.mnAgregarDescripcion.Size = new System.Drawing.Size(213, 28);
            // 
            // agregarDescripciónToolStripMenuItem
            // 
            this.agregarDescripciónToolStripMenuItem.Name = "agregarDescripciónToolStripMenuItem";
            this.agregarDescripciónToolStripMenuItem.Size = new System.Drawing.Size(212, 24);
            this.agregarDescripciónToolStripMenuItem.Text = "Agregar descripción";
            this.agregarDescripciónToolStripMenuItem.Click += new System.EventHandler(this.agregarDescripciónToolStripMenuItem_Click);
            // 
            // FrameConceptoEspecial
            // 
            this.FrameConceptoEspecial.Controls.Add(this.lblConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.chkConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.txtIdConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.label2);
            this.FrameConceptoEspecial.Location = new System.Drawing.Point(16, 98);
            this.FrameConceptoEspecial.Margin = new System.Windows.Forms.Padding(4);
            this.FrameConceptoEspecial.Name = "FrameConceptoEspecial";
            this.FrameConceptoEspecial.Padding = new System.Windows.Forms.Padding(4);
            this.FrameConceptoEspecial.Size = new System.Drawing.Size(989, 57);
            this.FrameConceptoEspecial.TabIndex = 7;
            this.FrameConceptoEspecial.TabStop = false;
            this.FrameConceptoEspecial.Text = "Concepto encabezado";
            // 
            // lblConceptoEncabezado
            // 
            this.lblConceptoEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConceptoEncabezado.Location = new System.Drawing.Point(205, 22);
            this.lblConceptoEncabezado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConceptoEncabezado.MostrarToolTip = false;
            this.lblConceptoEncabezado.Name = "lblConceptoEncabezado";
            this.lblConceptoEncabezado.Size = new System.Drawing.Size(776, 25);
            this.lblConceptoEncabezado.TabIndex = 18;
            this.lblConceptoEncabezado.Text = "Serie : ";
            this.lblConceptoEncabezado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkConceptoEncabezado
            // 
            this.chkConceptoEncabezado.Location = new System.Drawing.Point(8, 2);
            this.chkConceptoEncabezado.Margin = new System.Windows.Forms.Padding(4);
            this.chkConceptoEncabezado.Name = "chkConceptoEncabezado";
            this.chkConceptoEncabezado.Size = new System.Drawing.Size(180, 20);
            this.chkConceptoEncabezado.TabIndex = 14;
            this.chkConceptoEncabezado.Text = "Concepto Encabezado";
            this.chkConceptoEncabezado.UseVisualStyleBackColor = true;
            this.chkConceptoEncabezado.CheckedChanged += new System.EventHandler(this.chkConceptoEncabezado_CheckedChanged);
            // 
            // txtIdConceptoEncabezado
            // 
            this.txtIdConceptoEncabezado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdConceptoEncabezado.Decimales = 2;
            this.txtIdConceptoEncabezado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdConceptoEncabezado.ForeColor = System.Drawing.Color.Black;
            this.txtIdConceptoEncabezado.Location = new System.Drawing.Point(113, 22);
            this.txtIdConceptoEncabezado.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdConceptoEncabezado.MaxLength = 4;
            this.txtIdConceptoEncabezado.Name = "txtIdConceptoEncabezado";
            this.txtIdConceptoEncabezado.PermitirApostrofo = false;
            this.txtIdConceptoEncabezado.PermitirNegativos = false;
            this.txtIdConceptoEncabezado.Size = new System.Drawing.Size(83, 22);
            this.txtIdConceptoEncabezado.TabIndex = 12;
            this.txtIdConceptoEncabezado.Text = "1234";
            this.txtIdConceptoEncabezado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdConceptoEncabezado.TextChanged += new System.EventHandler(this.txtIdConceptoEncabezado_TextChanged);
            this.txtIdConceptoEncabezado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdConceptoEncabezado_KeyDown);
            this.txtIdConceptoEncabezado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdConceptoEncabezado_Validating);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Clave :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // colSATClaveProductoServicio
            // 
            this.colSATClaveProductoServicio.Text = "SAT Clave Producto";
            // 
            // colSATClaveUnidadDeMedida
            // 
            this.colSATClaveUnidadDeMedida.Text = "SAT Unidad de Medida";
            // 
            // FrmFacturarRemision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1485, 655);
            this.Controls.Add(this.FrameConceptoEspecial);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmFacturarRemision";
            this.Text = "Generar Factura de Remisión";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFacturarRemision_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.mnAgregarDescripcion.ResumeLayout(false);
            this.FrameConceptoEspecial.ResumeLayout(false);
            this.FrameConceptoEspecial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView listvClaves;
        private System.Windows.Forms.ColumnHeader colIdentificador;
        private System.Windows.Forms.ColumnHeader colCodigoEAN;
        private System.Windows.Forms.ColumnHeader colDescripcion;
        private System.Windows.Forms.ColumnHeader colCantidad;
        private System.Windows.Forms.ColumnHeader colValorUnitario;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colUnidadDeMedida;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.Button btlCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scComboBoxExt cboSeries;
        private SC_ControlsCS.scLabelExt lblSerie;
        private SC_ControlsCS.scLabelExt lblUltimoFolio;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnObtenerDetalles;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ColumnHeader colTasaIva;
        private System.Windows.Forms.ColumnHeader colSubTotal;
        private System.Windows.Forms.ColumnHeader colIva;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnObservacionesGral;
        private SC_ControlsCS.scTextBoxExt txtClienteNombre;
        private System.Windows.Forms.Button btnPago;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ContextMenuStrip mnAgregarDescripcion;
        private System.Windows.Forms.ToolStripMenuItem agregarDescripciónToolStripMenuItem;
        private System.Windows.Forms.GroupBox FrameConceptoEspecial;
        private SC_ControlsCS.scTextBoxExt txtIdConceptoEncabezado;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scLabelExt lblRFC;
        private System.Windows.Forms.CheckBox chkConceptoEncabezado;
        private SC_ControlsCS.scLabelExt lblConceptoEncabezado;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private System.Windows.Forms.ColumnHeader colValorUnitarioBase;
        private System.Windows.Forms.ColumnHeader colPorcentajeCobro;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ColumnHeader colSATClaveProductoServicio;
        private System.Windows.Forms.ColumnHeader colSATClaveUnidadDeMedida;
    }
}