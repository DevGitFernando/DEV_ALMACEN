namespace Dll_IFacturacion.XSA
{
    partial class FrmCFDI_Facturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCFDI_Facturas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboUsosCFDI = new SC_ControlsCS.scComboBoxExt();
            this.btlCliente = new System.Windows.Forms.Button();
            this.txtClienteNombre = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lblCantidadConLetra = new SC_ControlsCS.scLabelExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal = new SC_ControlsCS.scLabelExt();
            this.scLabelExt6 = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listvClaves = new System.Windows.Forms.ListView();
            this.colIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSAT_ProductoServicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValorUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTasaIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSAT_UnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoImpuesto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuConceptos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAgregarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModificarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboTiposDocumentos = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.btnEstablecimientos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.menuConceptos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboUsosCFDI);
            this.groupBox1.Controls.Add(this.btlCliente);
            this.groupBox1.Controls.Add(this.txtClienteNombre);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Uso del CFDI : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboUsosCFDI
            // 
            this.cboUsosCFDI.BackColorEnabled = System.Drawing.Color.White;
            this.cboUsosCFDI.Data = "";
            this.cboUsosCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsosCFDI.Filtro = " 1 = 1";
            this.cboUsosCFDI.FormattingEnabled = true;
            this.cboUsosCFDI.ListaItemsBusqueda = 20;
            this.cboUsosCFDI.Location = new System.Drawing.Point(86, 50);
            this.cboUsosCFDI.MostrarToolTip = false;
            this.cboUsosCFDI.Name = "cboUsosCFDI";
            this.cboUsosCFDI.Size = new System.Drawing.Size(405, 21);
            this.cboUsosCFDI.TabIndex = 20;
            // 
            // btlCliente
            // 
            this.btlCliente.Image = ((System.Drawing.Image)(resources.GetObject("btlCliente.Image")));
            this.btlCliente.Location = new System.Drawing.Point(460, 12);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(29, 33);
            this.btlCliente.TabIndex = 15;
            this.btlCliente.Text = "...";
            this.btlCliente.UseVisualStyleBackColor = true;
            this.btlCliente.Click += new System.EventHandler(this.btlCliente_Click);
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(154, 24);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(302, 20);
            this.txtClienteNombre.TabIndex = 14;
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
            this.txtId.Location = new System.Drawing.Point(86, 24);
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
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.lblCantidadConLetra);
            this.FrameDetalles.Controls.Add(this.scLabelExt4);
            this.FrameDetalles.Controls.Add(this.scLabelExt5);
            this.FrameDetalles.Controls.Add(this.lblSubTotal);
            this.FrameDetalles.Controls.Add(this.scLabelExt6);
            this.FrameDetalles.Controls.Add(this.lblIva);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Controls.Add(this.lblMensajes);
            this.FrameDetalles.Controls.Add(this.listvClaves);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 114);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1039, 464);
            this.FrameDetalles.TabIndex = 4;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la Factura";
            // 
            // lblCantidadConLetra
            // 
            this.lblCantidadConLetra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCantidadConLetra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadConLetra.Location = new System.Drawing.Point(12, 359);
            this.lblCantidadConLetra.MostrarToolTip = false;
            this.lblCantidadConLetra.Name = "lblCantidadConLetra";
            this.lblCantidadConLetra.Size = new System.Drawing.Size(768, 72);
            this.lblCantidadConLetra.TabIndex = 30;
            this.lblCantidadConLetra.Text = "scLabelExt1";
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt4.Location = new System.Drawing.Point(748, 409);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt4.TabIndex = 29;
            this.scLabelExt4.Text = "Total :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt5.Location = new System.Drawing.Point(748, 384);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt5.TabIndex = 28;
            this.scLabelExt5.Text = "Iva :";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(891, 359);
            this.lblSubTotal.MostrarToolTip = false;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal.TabIndex = 24;
            this.lblSubTotal.Text = "scLabelExt1";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt6
            // 
            this.scLabelExt6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt6.Location = new System.Drawing.Point(748, 359);
            this.scLabelExt6.MostrarToolTip = false;
            this.scLabelExt6.Name = "scLabelExt6";
            this.scLabelExt6.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt6.TabIndex = 27;
            this.scLabelExt6.Text = "Sub-Total :";
            this.scLabelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(891, 384);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(137, 22);
            this.lblIva.TabIndex = 25;
            this.lblIva.Text = "scLabelExt2";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(891, 409);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 26;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(3, 437);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1033, 24);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "< Clic derecho > Agregar, Modificar ó Quitar detalles";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listvClaves
            // 
            this.listvClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdentificador,
            this.colSAT_ProductoServicio,
            this.colClave,
            this.colDescripcion,
            this.colValorUnitario,
            this.colCantidad,
            this.colTasaIva,
            this.colSubTotal,
            this.colIva,
            this.colTotal,
            this.colSAT_UnidadDeMedida,
            this.colUnidadDeMedida,
            this.colTipoImpuesto});
            this.listvClaves.ContextMenuStrip = this.menuConceptos;
            this.listvClaves.Location = new System.Drawing.Point(10, 16);
            this.listvClaves.Name = "listvClaves";
            this.listvClaves.Size = new System.Drawing.Size(1021, 335);
            this.listvClaves.TabIndex = 0;
            this.listvClaves.UseCompatibleStateImageBehavior = false;
            this.listvClaves.View = System.Windows.Forms.View.Details;
            // 
            // colIdentificador
            // 
            this.colIdentificador.Text = "Identificador";
            this.colIdentificador.Width = 80;
            // 
            // colSAT_ProductoServicio
            // 
            this.colSAT_ProductoServicio.Text = "SAT Producto ó Servicio";
            // 
            // colClave
            // 
            this.colClave.Text = "Clave";
            this.colClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colClave.Width = 100;
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 250;
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
            this.colCantidad.Width = 88;
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
            // colSAT_UnidadDeMedida
            // 
            this.colSAT_UnidadDeMedida.Text = "SAT Unidad de Medida";
            // 
            // colUnidadDeMedida
            // 
            this.colUnidadDeMedida.Text = "Unidad de Medida";
            this.colUnidadDeMedida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colUnidadDeMedida.Width = 120;
            // 
            // colTipoImpuesto
            // 
            this.colTipoImpuesto.Text = "Impuesto";
            this.colTipoImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTipoImpuesto.Width = 50;
            // 
            // menuConceptos
            // 
            this.menuConceptos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregarConcepto,
            this.btnEliminarConcepto,
            this.btnModificarConcepto});
            this.menuConceptos.Name = "menuFolios";
            this.menuConceptos.Size = new System.Drawing.Size(126, 70);
            // 
            // btnAgregarConcepto
            // 
            this.btnAgregarConcepto.Name = "btnAgregarConcepto";
            this.btnAgregarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnAgregarConcepto.Text = "Agregar";
            this.btnAgregarConcepto.Click += new System.EventHandler(this.btnAgregarConcepto_Click);
            // 
            // btnEliminarConcepto
            // 
            this.btnEliminarConcepto.Name = "btnEliminarConcepto";
            this.btnEliminarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnEliminarConcepto.Text = "Eliminar";
            this.btnEliminarConcepto.Click += new System.EventHandler(this.btnEliminarConcepto_Click);
            // 
            // btnModificarConcepto
            // 
            this.btnModificarConcepto.Name = "btnModificarConcepto";
            this.btnModificarConcepto.Size = new System.Drawing.Size(125, 22);
            this.btnModificarConcepto.Text = "Modificar";
            this.btnModificarConcepto.Click += new System.EventHandler(this.btnModificarConcepto_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(681, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(4, 34);
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
            this.cboSeries.Location = new System.Drawing.Point(45, 34);
            this.cboSeries.MostrarToolTip = false;
            this.cboSeries.Name = "cboSeries";
            this.cboSeries.Size = new System.Drawing.Size(118, 21);
            this.cboSeries.TabIndex = 0;
            this.cboSeries.SelectedIndexChanged += new System.EventHandler(this.cboSeries_SelectedIndexChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(174, 270);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 66);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 30);
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
            this.groupBox2.Location = new System.Drawing.Point(860, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 80);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(14, 44);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(166, 23);
            this.btnPago.TabIndex = 21;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(14, 17);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(166, 23);
            this.btnObservacionesGral.TabIndex = 20;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboTiposDocumentos);
            this.groupBox4.Location = new System.Drawing.Point(511, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(164, 80);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tipo de Documento";
            // 
            // cboTiposDocumentos
            // 
            this.cboTiposDocumentos.BackColorEnabled = System.Drawing.Color.White;
            this.cboTiposDocumentos.Data = "";
            this.cboTiposDocumentos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposDocumentos.Filtro = " 1 = 1";
            this.cboTiposDocumentos.FormattingEnabled = true;
            this.cboTiposDocumentos.ListaItemsBusqueda = 20;
            this.cboTiposDocumentos.Location = new System.Drawing.Point(14, 34);
            this.cboTiposDocumentos.MostrarToolTip = false;
            this.cboTiposDocumentos.Name = "cboTiposDocumentos";
            this.cboTiposDocumentos.Size = new System.Drawing.Size(139, 21);
            this.cboTiposDocumentos.TabIndex = 0;
            this.cboTiposDocumentos.SelectedIndexChanged += new System.EventHandler(this.cboTiposDocumentos_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia,
            this.btnEstablecimientos,
            this.toolStripSeparator1,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1059, 25);
            this.toolStripBarraMenu.TabIndex = 17;
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
            this.lblTimbresDisponibles.Size = new System.Drawing.Size(115, 22);
            this.lblTimbresDisponibles.Text = "Timbres disponibles ";
            // 
            // btnEstablecimientos
            // 
            this.btnEstablecimientos.Image = ((System.Drawing.Image)(resources.GetObject("btnEstablecimientos.Image")));
            this.btnEstablecimientos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEstablecimientos.Name = "btnEstablecimientos";
            this.btnEstablecimientos.Size = new System.Drawing.Size(116, 22);
            this.btnEstablecimientos.Text = "Establecimientos";
            this.btnEstablecimientos.Click += new System.EventHandler(this.btnEstablecimientos_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Click += new System.EventHandler(this.toolStripSeparator1_Click);
            // 
            // FrmCFDI_Facturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 583);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCFDI_Facturas";
            this.Text = "Registro de facturas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCFDI_Facturas_Load);
            this.Shown += new System.EventHandler(this.FrmCFDI_Facturas_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.menuConceptos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView listvClaves;
        private System.Windows.Forms.ColumnHeader colIdentificador;
        private System.Windows.Forms.ColumnHeader colClave;
        private System.Windows.Forms.ColumnHeader colDescripcion;
        private System.Windows.Forms.ColumnHeader colCantidad;
        private System.Windows.Forms.ColumnHeader colValorUnitario;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colUnidadDeMedida;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scComboBoxExt cboSeries;
        private SC_ControlsCS.scLabelExt lblSerie;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ColumnHeader colTasaIva;
        private System.Windows.Forms.ColumnHeader colSubTotal;
        private System.Windows.Forms.ColumnHeader colIva;
        private System.Windows.Forms.Timer tmProceso;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnObservacionesGral;
        private SC_ControlsCS.scTextBoxExt txtClienteNombre;
        private System.Windows.Forms.Button btnPago;
        private System.Windows.Forms.GroupBox groupBox4;
        private SC_ControlsCS.scComboBoxExt cboTiposDocumentos;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ContextMenuStrip menuConceptos;
        private System.Windows.Forms.ToolStripMenuItem btnAgregarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnModificarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarConcepto;
        private System.Windows.Forms.ColumnHeader colTipoImpuesto;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt scLabelExt5;
        private SC_ControlsCS.scLabelExt lblSubTotal;
        private SC_ControlsCS.scLabelExt scLabelExt6;
        private SC_ControlsCS.scLabelExt lblIva;
        private SC_ControlsCS.scLabelExt lblTotal;
        private SC_ControlsCS.scLabelExt lblCantidadConLetra;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private System.Windows.Forms.Button btlCliente;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboUsosCFDI;
        private System.Windows.Forms.ColumnHeader colSAT_ProductoServicio;
        private System.Windows.Forms.ColumnHeader colSAT_UnidadDeMedida;
        private System.Windows.Forms.ToolStripButton btnEstablecimientos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}