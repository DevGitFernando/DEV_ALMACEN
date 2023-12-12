namespace Dll_IFacturacion.XSA
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
            this.lblCantidadConLetra = new SC_ControlsCS.scLabelExt();
            this.lblRegistros = new SC_ControlsCS.scLabelExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal = new SC_ControlsCS.scLabelExt();
            this.scLabelExt6 = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listvClaves = new System.Windows.Forms.ListView();
            this.colIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSATClaveProductoServicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescripcion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValorUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTasaIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIva = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSATClaveUnidadDeMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkAgregarInformacionComercial = new System.Windows.Forms.CheckBox();
            this.chkAplicarMascara = new System.Windows.Forms.CheckBox();
            this.chkMostrarInformacionConcentrada = new System.Windows.Forms.CheckBox();
            this.chkAplicarMascara_Claves = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mnAgregarDescripcion.SuspendLayout();
            this.FrameConceptoEspecial.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(911, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // lblRFC
            // 
            this.lblRFC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRFC.Location = new System.Drawing.Point(150, 18);
            this.lblRFC.MostrarToolTip = false;
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(129, 20);
            this.lblRFC.TabIndex = 1;
            this.lblRFC.Text = "Serie : ";
            this.lblRFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(282, 18);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(584, 20);
            this.txtClienteNombre.TabIndex = 2;
            // 
            // btlCliente
            // 
            this.btlCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btlCliente.Image = ((System.Drawing.Image)(resources.GetObject("btlCliente.Image")));
            this.btlCliente.Location = new System.Drawing.Point(874, 12);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(29, 33);
            this.btlCliente.TabIndex = 3;
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
            this.txtId.Location = new System.Drawing.Point(85, 18);
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
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.FrameConceptoEspecial);
            this.FrameDetalles.Controls.Add(this.lblCantidadConLetra);
            this.FrameDetalles.Controls.Add(this.lblRegistros);
            this.FrameDetalles.Controls.Add(this.scLabelExt4);
            this.FrameDetalles.Controls.Add(this.scLabelExt5);
            this.FrameDetalles.Controls.Add(this.lblSubTotal);
            this.FrameDetalles.Controls.Add(this.scLabelExt6);
            this.FrameDetalles.Controls.Add(this.lblIva);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Controls.Add(this.lblMensajes);
            this.FrameDetalles.Controls.Add(this.listvClaves);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 140);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1263, 441);
            this.FrameDetalles.TabIndex = 5;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la Factura";
            // 
            // lblCantidadConLetra
            // 
            this.lblCantidadConLetra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCantidadConLetra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCantidadConLetra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadConLetra.Location = new System.Drawing.Point(10, 352);
            this.lblCantidadConLetra.MostrarToolTip = false;
            this.lblCantidadConLetra.Name = "lblCantidadConLetra";
            this.lblCantidadConLetra.Size = new System.Drawing.Size(1008, 54);
            this.lblCantidadConLetra.TabIndex = 1;
            this.lblCantidadConLetra.Text = "scLabelExt7";
            // 
            // lblRegistros
            // 
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(10, 330);
            this.lblRegistros.MostrarToolTip = false;
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(621, 20);
            this.lblRegistros.TabIndex = 31;
            this.lblRegistros.Text = "scLabelExt7";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt4.Location = new System.Drawing.Point(972, 379);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt4.TabIndex = 30;
            this.scLabelExt4.Text = "Total :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt5.Location = new System.Drawing.Point(972, 355);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt5.TabIndex = 29;
            this.scLabelExt5.Text = "Iva :";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(1115, 330);
            this.lblSubTotal.MostrarToolTip = false;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal.TabIndex = 25;
            this.lblSubTotal.Text = "scLabelExt1";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt6
            // 
            this.scLabelExt6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt6.Location = new System.Drawing.Point(972, 330);
            this.scLabelExt6.MostrarToolTip = false;
            this.scLabelExt6.Name = "scLabelExt6";
            this.scLabelExt6.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt6.TabIndex = 28;
            this.scLabelExt6.Text = "Sub-Total :";
            this.scLabelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(1115, 355);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(137, 22);
            this.lblIva.TabIndex = 26;
            this.lblIva.Text = "scLabelExt2";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1115, 379);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 27;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(3, 414);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1257, 24);
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
            this.colClave,
            this.colSATClaveProductoServicio,
            this.colDescripcion,
            this.colValorUnitario,
            this.colCantidad,
            this.colTasaIva,
            this.colSubTotal,
            this.colIva,
            this.colTotal,
            this.colUnidadDeMedida,
            this.colSATClaveUnidadDeMedida});
            this.listvClaves.Location = new System.Drawing.Point(10, 16);
            this.listvClaves.Name = "listvClaves";
            this.listvClaves.Size = new System.Drawing.Size(1245, 311);
            this.listvClaves.TabIndex = 0;
            this.listvClaves.UseCompatibleStateImageBehavior = false;
            this.listvClaves.View = System.Windows.Forms.View.Details;
            // 
            // colIdentificador
            // 
            this.colIdentificador.Text = "Identificador";
            this.colIdentificador.Width = 80;
            // 
            // colClave
            // 
            this.colClave.Text = "Clave";
            this.colClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colClave.Width = 100;
            // 
            // colSATClaveProductoServicio
            // 
            this.colSATClaveProductoServicio.Text = "SAT Clave Producto";
            // 
            // colDescripcion
            // 
            this.colDescripcion.Text = "Descripción";
            this.colDescripcion.Width = 300;
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
            // colSATClaveUnidadDeMedida
            // 
            this.colSATClaveUnidadDeMedida.Text = "SAT Unidad de Medida";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblUltimoFolio);
            this.groupBox3.Controls.Add(this.scLabelExt1);
            this.groupBox3.Controls.Add(this.lblSerie);
            this.groupBox3.Controls.Add(this.cboSeries);
            this.groupBox3.Location = new System.Drawing.Point(928, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 109);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblUltimoFolio
            // 
            this.lblUltimoFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUltimoFolio.Location = new System.Drawing.Point(71, 54);
            this.lblUltimoFolio.MostrarToolTip = false;
            this.lblUltimoFolio.Name = "lblUltimoFolio";
            this.lblUltimoFolio.Size = new System.Drawing.Size(118, 20);
            this.lblUltimoFolio.TabIndex = 16;
            this.lblUltimoFolio.Text = "Serie : ";
            this.lblUltimoFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(7, 55);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(58, 20);
            this.scLabelExt1.TabIndex = 15;
            this.scLabelExt1.Text = "Ult. Folio : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(7, 25);
            this.lblSerie.MostrarToolTip = false;
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(58, 20);
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
            this.cboSeries.Location = new System.Drawing.Point(71, 24);
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
            this.btnObtenerDetalles,
            this.toolStripSeparator1,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1284, 25);
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
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(202, 279);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(868, 60);
            this.FrameProceso.TabIndex = 6;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(838, 24);
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
            this.groupBox2.Location = new System.Drawing.Point(1141, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 109);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(10, 52);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(114, 23);
            this.btnPago.TabIndex = 1;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(10, 23);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(114, 23);
            this.btnObservacionesGral.TabIndex = 0;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // mnAgregarDescripcion
            // 
            this.mnAgregarDescripcion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarDescripciónToolStripMenuItem});
            this.mnAgregarDescripcion.Name = "mnAgregarDescripcion";
            this.mnAgregarDescripcion.Size = new System.Drawing.Size(181, 26);
            // 
            // agregarDescripciónToolStripMenuItem
            // 
            this.agregarDescripciónToolStripMenuItem.Name = "agregarDescripciónToolStripMenuItem";
            this.agregarDescripciónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.agregarDescripciónToolStripMenuItem.Text = "Agregar descripción";
            this.agregarDescripciónToolStripMenuItem.Click += new System.EventHandler(this.agregarDescripciónToolStripMenuItem_Click);
            // 
            // FrameConceptoEspecial
            // 
            this.FrameConceptoEspecial.Controls.Add(this.lblConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.chkConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.txtIdConceptoEncabezado);
            this.FrameConceptoEspecial.Controls.Add(this.label2);
            this.FrameConceptoEspecial.Location = new System.Drawing.Point(687, 337);
            this.FrameConceptoEspecial.Name = "FrameConceptoEspecial";
            this.FrameConceptoEspecial.Size = new System.Drawing.Size(224, 50);
            this.FrameConceptoEspecial.TabIndex = 7;
            this.FrameConceptoEspecial.TabStop = false;
            this.FrameConceptoEspecial.Text = "Concepto encabezado";
            this.FrameConceptoEspecial.Visible = false;
            // 
            // lblConceptoEncabezado
            // 
            this.lblConceptoEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConceptoEncabezado.Location = new System.Drawing.Point(154, 18);
            this.lblConceptoEncabezado.MostrarToolTip = false;
            this.lblConceptoEncabezado.Name = "lblConceptoEncabezado";
            this.lblConceptoEncabezado.Size = new System.Drawing.Size(314, 20);
            this.lblConceptoEncabezado.TabIndex = 18;
            this.lblConceptoEncabezado.Text = "Serie : ";
            this.lblConceptoEncabezado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkConceptoEncabezado
            // 
            this.chkConceptoEncabezado.Location = new System.Drawing.Point(6, 0);
            this.chkConceptoEncabezado.Name = "chkConceptoEncabezado";
            this.chkConceptoEncabezado.Size = new System.Drawing.Size(135, 16);
            this.chkConceptoEncabezado.TabIndex = 0;
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
            this.txtIdConceptoEncabezado.Location = new System.Drawing.Point(85, 18);
            this.txtIdConceptoEncabezado.MaxLength = 4;
            this.txtIdConceptoEncabezado.Name = "txtIdConceptoEncabezado";
            this.txtIdConceptoEncabezado.PermitirApostrofo = false;
            this.txtIdConceptoEncabezado.PermitirNegativos = false;
            this.txtIdConceptoEncabezado.Size = new System.Drawing.Size(63, 20);
            this.txtIdConceptoEncabezado.TabIndex = 1;
            this.txtIdConceptoEncabezado.Text = "1234";
            this.txtIdConceptoEncabezado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdConceptoEncabezado.TextChanged += new System.EventHandler(this.txtIdConceptoEncabezado_TextChanged);
            this.txtIdConceptoEncabezado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdConceptoEncabezado_KeyDown);
            this.txtIdConceptoEncabezado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdConceptoEncabezado_Validating);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Id Concepto :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkAplicarMascara_Claves);
            this.groupBox4.Controls.Add(this.chkAgregarInformacionComercial);
            this.groupBox4.Controls.Add(this.chkAplicarMascara);
            this.groupBox4.Controls.Add(this.chkMostrarInformacionConcentrada);
            this.groupBox4.Location = new System.Drawing.Point(12, 76);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(911, 61);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información";
            // 
            // chkAgregarInformacionComercial
            // 
            this.chkAgregarInformacionComercial.Location = new System.Drawing.Point(111, 34);
            this.chkAgregarInformacionComercial.Margin = new System.Windows.Forms.Padding(2);
            this.chkAgregarInformacionComercial.Name = "chkAgregarInformacionComercial";
            this.chkAgregarInformacionComercial.Size = new System.Drawing.Size(263, 18);
            this.chkAgregarInformacionComercial.TabIndex = 1;
            this.chkAgregarInformacionComercial.Text = "Agregar información de Lotes y EAN";
            this.chkAgregarInformacionComercial.UseVisualStyleBackColor = true;
            // 
            // chkAplicarMascara
            // 
            this.chkAplicarMascara.Location = new System.Drawing.Point(447, 34);
            this.chkAplicarMascara.Margin = new System.Windows.Forms.Padding(2);
            this.chkAplicarMascara.Name = "chkAplicarMascara";
            this.chkAplicarMascara.Size = new System.Drawing.Size(263, 18);
            this.chkAplicarMascara.TabIndex = 3;
            this.chkAplicarMascara.Text = "Aplicar mascaras de Descripción y Presentación";
            this.chkAplicarMascara.UseVisualStyleBackColor = true;
            // 
            // chkMostrarInformacionConcentrada
            // 
            this.chkMostrarInformacionConcentrada.Location = new System.Drawing.Point(111, 13);
            this.chkMostrarInformacionConcentrada.Margin = new System.Windows.Forms.Padding(2);
            this.chkMostrarInformacionConcentrada.Name = "chkMostrarInformacionConcentrada";
            this.chkMostrarInformacionConcentrada.Size = new System.Drawing.Size(263, 18);
            this.chkMostrarInformacionConcentrada.TabIndex = 0;
            this.chkMostrarInformacionConcentrada.Text = "Mostrar información concentrada";
            this.chkMostrarInformacionConcentrada.UseVisualStyleBackColor = true;
            // 
            // chkAplicarMascara_Claves
            // 
            this.chkAplicarMascara_Claves.Location = new System.Drawing.Point(447, 13);
            this.chkAplicarMascara_Claves.Margin = new System.Windows.Forms.Padding(2);
            this.chkAplicarMascara_Claves.Name = "chkAplicarMascara_Claves";
            this.chkAplicarMascara_Claves.Size = new System.Drawing.Size(263, 18);
            this.chkAplicarMascara_Claves.TabIndex = 2;
            this.chkAplicarMascara_Claves.Text = "Aplicar mascaras de Clave";
            this.chkAplicarMascara_Claves.UseVisualStyleBackColor = true;
            // 
            // FrmFacturarRemision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 585);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.groupBox4.ResumeLayout(false);
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
        private SC_ControlsCS.scLabelExt lblCantidadConLetra;
        private SC_ControlsCS.scLabelExt lblRegistros;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt scLabelExt5;
        private SC_ControlsCS.scLabelExt lblSubTotal;
        private SC_ControlsCS.scLabelExt scLabelExt6;
        private SC_ControlsCS.scLabelExt lblIva;
        private SC_ControlsCS.scLabelExt lblTotal;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkMostrarInformacionConcentrada;
        private System.Windows.Forms.CheckBox chkAplicarMascara;
        private System.Windows.Forms.ColumnHeader colSATClaveProductoServicio;
        private System.Windows.Forms.ColumnHeader colSATClaveUnidadDeMedida;
        private System.Windows.Forms.CheckBox chkAgregarInformacionComercial;
        private System.Windows.Forms.CheckBox chkAplicarMascara_Claves;
    }
}