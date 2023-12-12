namespace Dll_IFacturacion.XSA
{
    partial class FrmCFDI_Facturas_ImportarDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCFDI_Facturas_ImportarDatos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClienteNombre = new SC_ControlsCS.scTextBoxExt();
            this.cboUsosCFDI = new SC_ControlsCS.scComboBoxExt();
            this.btlCliente = new System.Windows.Forms.Button();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lblTitulo_Total_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_Iva_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_SubTotal_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblIva_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblTotal_Calculado = new SC_ControlsCS.scLabelExt();
            this.lblCantidadConLetra = new SC_ControlsCS.scLabelExt();
            this.lblRegistros = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_Total = new SC_ControlsCS.scLabelExt();
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
            this.colClaveLote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCaducidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTitulo_Iva = new SC_ControlsCS.scLabelExt();
            this.lblSubTotal = new SC_ControlsCS.scLabelExt();
            this.lblTitulo_SubTotal = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.menuConceptos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAgregarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModificarConcepto = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSerie = new SC_ControlsCS.scLabelExt();
            this.cboSeries = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorVistaPrevia = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarTimbres = new System.Windows.Forms.ToolStripButton();
            this.lblTimbresDisponibles = new System.Windows.Forms.ToolStripLabel();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmProceso = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnObservacionesGral = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkOmitirValidacionDeImportes = new System.Windows.Forms.CheckBox();
            this.chkForzarInformacionSAT = new System.Windows.Forms.CheckBox();
            this.chkClaveSSA_Base___EsIdentificador = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblArchivoExcel = new SC_ControlsCS.scLabelExt();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeerExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBloquearHoja = new System.Windows.Forms.ToolStripButton();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.btnEstablecimientos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.menuConceptos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtClienteNombre);
            this.groupBox1.Controls.Add(this.cboUsosCFDI);
            this.groupBox1.Controls.Add(this.btlCliente);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Receptor";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Uso del CFDI : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClienteNombre
            // 
            this.txtClienteNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClienteNombre.Decimales = 2;
            this.txtClienteNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClienteNombre.ForeColor = System.Drawing.Color.Black;
            this.txtClienteNombre.Location = new System.Drawing.Point(155, 18);
            this.txtClienteNombre.MaxLength = 100;
            this.txtClienteNombre.Name = "txtClienteNombre";
            this.txtClienteNombre.PermitirApostrofo = false;
            this.txtClienteNombre.PermitirNegativos = false;
            this.txtClienteNombre.Size = new System.Drawing.Size(423, 20);
            this.txtClienteNombre.TabIndex = 1;
            // 
            // cboUsosCFDI
            // 
            this.cboUsosCFDI.BackColorEnabled = System.Drawing.Color.White;
            this.cboUsosCFDI.Data = "";
            this.cboUsosCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsosCFDI.Filtro = " 1 = 1";
            this.cboUsosCFDI.FormattingEnabled = true;
            this.cboUsosCFDI.ListaItemsBusqueda = 20;
            this.cboUsosCFDI.Location = new System.Drawing.Point(87, 41);
            this.cboUsosCFDI.MostrarToolTip = false;
            this.cboUsosCFDI.Name = "cboUsosCFDI";
            this.cboUsosCFDI.Size = new System.Drawing.Size(492, 21);
            this.cboUsosCFDI.TabIndex = 3;
            // 
            // btlCliente
            // 
            this.btlCliente.Image = ((System.Drawing.Image)(resources.GetObject("btlCliente.Image")));
            this.btlCliente.Location = new System.Drawing.Point(584, 15);
            this.btlCliente.Name = "btlCliente";
            this.btlCliente.Size = new System.Drawing.Size(26, 46);
            this.btlCliente.TabIndex = 2;
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
            this.txtId.Location = new System.Drawing.Point(87, 18);
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
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.lblTitulo_Total_Calculado);
            this.FrameDetalles.Controls.Add(this.lblTitulo_Iva_Calculado);
            this.FrameDetalles.Controls.Add(this.lblSubTotal_Calculado);
            this.FrameDetalles.Controls.Add(this.lblTitulo_SubTotal_Calculado);
            this.FrameDetalles.Controls.Add(this.lblIva_Calculado);
            this.FrameDetalles.Controls.Add(this.lblTotal_Calculado);
            this.FrameDetalles.Controls.Add(this.lblCantidadConLetra);
            this.FrameDetalles.Controls.Add(this.lblRegistros);
            this.FrameDetalles.Controls.Add(this.lblTitulo_Total);
            this.FrameDetalles.Controls.Add(this.listvClaves);
            this.FrameDetalles.Controls.Add(this.lblTitulo_Iva);
            this.FrameDetalles.Controls.Add(this.lblSubTotal);
            this.FrameDetalles.Controls.Add(this.lblTitulo_SubTotal);
            this.FrameDetalles.Controls.Add(this.lblIva);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 174);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(994, 399);
            this.FrameDetalles.TabIndex = 5;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles de la Factura";
            // 
            // lblTitulo_Total_Calculado
            // 
            this.lblTitulo_Total_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Total_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Total_Calculado.Location = new System.Drawing.Point(503, 361);
            this.lblTitulo_Total_Calculado.MostrarToolTip = false;
            this.lblTitulo_Total_Calculado.Name = "lblTitulo_Total_Calculado";
            this.lblTitulo_Total_Calculado.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Total_Calculado.TabIndex = 30;
            this.lblTitulo_Total_Calculado.Text = "Total :";
            this.lblTitulo_Total_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo_Iva_Calculado
            // 
            this.lblTitulo_Iva_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Iva_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Iva_Calculado.Location = new System.Drawing.Point(503, 336);
            this.lblTitulo_Iva_Calculado.MostrarToolTip = false;
            this.lblTitulo_Iva_Calculado.Name = "lblTitulo_Iva_Calculado";
            this.lblTitulo_Iva_Calculado.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Iva_Calculado.TabIndex = 29;
            this.lblTitulo_Iva_Calculado.Text = "Iva :";
            this.lblTitulo_Iva_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal_Calculado
            // 
            this.lblSubTotal_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal_Calculado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal_Calculado.ForeColor = System.Drawing.Color.Red;
            this.lblSubTotal_Calculado.Location = new System.Drawing.Point(609, 311);
            this.lblSubTotal_Calculado.MostrarToolTip = false;
            this.lblSubTotal_Calculado.Name = "lblSubTotal_Calculado";
            this.lblSubTotal_Calculado.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal_Calculado.TabIndex = 5;
            this.lblSubTotal_Calculado.Text = "scLabelExt1";
            this.lblSubTotal_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo_SubTotal_Calculado
            // 
            this.lblTitulo_SubTotal_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_SubTotal_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_SubTotal_Calculado.Location = new System.Drawing.Point(503, 311);
            this.lblTitulo_SubTotal_Calculado.MostrarToolTip = false;
            this.lblTitulo_SubTotal_Calculado.Name = "lblTitulo_SubTotal_Calculado";
            this.lblTitulo_SubTotal_Calculado.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_SubTotal_Calculado.TabIndex = 28;
            this.lblTitulo_SubTotal_Calculado.Text = "Sub-Total :";
            this.lblTitulo_SubTotal_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva_Calculado
            // 
            this.lblIva_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva_Calculado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva_Calculado.ForeColor = System.Drawing.Color.Red;
            this.lblIva_Calculado.Location = new System.Drawing.Point(609, 336);
            this.lblIva_Calculado.MostrarToolTip = false;
            this.lblIva_Calculado.Name = "lblIva_Calculado";
            this.lblIva_Calculado.Size = new System.Drawing.Size(137, 22);
            this.lblIva_Calculado.TabIndex = 6;
            this.lblIva_Calculado.Text = "scLabelExt2";
            this.lblIva_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal_Calculado
            // 
            this.lblTotal_Calculado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal_Calculado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal_Calculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_Calculado.ForeColor = System.Drawing.Color.Red;
            this.lblTotal_Calculado.Location = new System.Drawing.Point(609, 361);
            this.lblTotal_Calculado.MostrarToolTip = false;
            this.lblTotal_Calculado.Name = "lblTotal_Calculado";
            this.lblTotal_Calculado.Size = new System.Drawing.Size(137, 22);
            this.lblTotal_Calculado.TabIndex = 7;
            this.lblTotal_Calculado.Text = "scLabelExt3";
            this.lblTotal_Calculado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidadConLetra
            // 
            this.lblCantidadConLetra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCantidadConLetra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadConLetra.Location = new System.Drawing.Point(15, 328);
            this.lblCantidadConLetra.MostrarToolTip = false;
            this.lblCantidadConLetra.Name = "lblCantidadConLetra";
            this.lblCantidadConLetra.Size = new System.Drawing.Size(483, 54);
            this.lblCantidadConLetra.TabIndex = 1;
            this.lblCantidadConLetra.Text = "scLabelExt7";
            // 
            // lblRegistros
            // 
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(15, 306);
            this.lblRegistros.MostrarToolTip = false;
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(373, 20);
            this.lblRegistros.TabIndex = 23;
            this.lblRegistros.Text = "scLabelExt7";
            this.lblRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitulo_Total
            // 
            this.lblTitulo_Total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Total.Location = new System.Drawing.Point(742, 361);
            this.lblTitulo_Total.MostrarToolTip = false;
            this.lblTitulo_Total.Name = "lblTitulo_Total";
            this.lblTitulo_Total.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Total.TabIndex = 22;
            this.lblTitulo_Total.Text = "Total :";
            this.lblTitulo_Total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listvClaves
            // 
            this.listvClaves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.colTipoImpuesto,
            this.colClaveLote,
            this.colCaducidad});
            this.listvClaves.Location = new System.Drawing.Point(10, 16);
            this.listvClaves.Name = "listvClaves";
            this.listvClaves.Size = new System.Drawing.Size(977, 288);
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
            this.colSAT_UnidadDeMedida.Text = "SAT Unidad de medida";
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
            // colClaveLote
            // 
            this.colClaveLote.Text = "Lote";
            // 
            // colCaducidad
            // 
            this.colCaducidad.Text = "Caducidad";
            // 
            // lblTitulo_Iva
            // 
            this.lblTitulo_Iva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_Iva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_Iva.Location = new System.Drawing.Point(742, 336);
            this.lblTitulo_Iva.MostrarToolTip = false;
            this.lblTitulo_Iva.Name = "lblTitulo_Iva";
            this.lblTitulo_Iva.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_Iva.TabIndex = 21;
            this.lblTitulo_Iva.Text = "Iva :";
            this.lblTitulo_Iva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(848, 311);
            this.lblSubTotal.MostrarToolTip = false;
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(137, 22);
            this.lblSubTotal.TabIndex = 2;
            this.lblSubTotal.Text = "scLabelExt1";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitulo_SubTotal
            // 
            this.lblTitulo_SubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo_SubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo_SubTotal.Location = new System.Drawing.Point(742, 311);
            this.lblTitulo_SubTotal.MostrarToolTip = false;
            this.lblTitulo_SubTotal.Name = "lblTitulo_SubTotal";
            this.lblTitulo_SubTotal.Size = new System.Drawing.Size(100, 22);
            this.lblTitulo_SubTotal.TabIndex = 20;
            this.lblTitulo_SubTotal.Text = "Sub-Total :";
            this.lblTitulo_SubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(848, 336);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(137, 22);
            this.lblIva.TabIndex = 3;
            this.lblIva.Text = "scLabelExt2";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(848, 361);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.groupBox3.Location = new System.Drawing.Point(636, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 70);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serie de Facturación";
            // 
            // lblSerie
            // 
            this.lblSerie.Location = new System.Drawing.Point(12, 28);
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
            this.cboSeries.Location = new System.Drawing.Point(53, 28);
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
            this.btnFacturar,
            this.toolStripSeparator2,
            this.btnValidarDatos,
            this.toolStripSeparatorVistaPrevia,
            this.btnEstablecimientos,
            this.toolStripSeparator5,
            this.btnConsultarTimbres,
            this.lblTimbresDisponibles});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1016, 25);
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
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(174, 270);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 66);
            this.FrameProceso.TabIndex = 6;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 28);
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
            this.groupBox2.Location = new System.Drawing.Point(815, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(191, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Observaciones y Pago";
            // 
            // btnPago
            // 
            this.btnPago.Location = new System.Drawing.Point(107, 26);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(75, 23);
            this.btnPago.TabIndex = 21;
            this.btnPago.Text = "Pago";
            this.btnPago.UseVisualStyleBackColor = true;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            // 
            // btnObservacionesGral
            // 
            this.btnObservacionesGral.Location = new System.Drawing.Point(14, 26);
            this.btnObservacionesGral.Name = "btnObservacionesGral";
            this.btnObservacionesGral.Size = new System.Drawing.Size(91, 23);
            this.btnObservacionesGral.TabIndex = 20;
            this.btnObservacionesGral.Text = "Observaciones";
            this.btnObservacionesGral.UseVisualStyleBackColor = true;
            this.btnObservacionesGral.Click += new System.EventHandler(this.btnObservacionesGral_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkOmitirValidacionDeImportes);
            this.groupBox5.Controls.Add(this.chkForzarInformacionSAT);
            this.groupBox5.Controls.Add(this.chkClaveSSA_Base___EsIdentificador);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.lblArchivoExcel);
            this.groupBox5.Controls.Add(this.toolStrip1);
            this.groupBox5.Controls.Add(this.cboHojas);
            this.groupBox5.Location = new System.Drawing.Point(12, 98);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(994, 74);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "|";
            // 
            // chkOmitirValidacionDeImportes
            // 
            this.chkOmitirValidacionDeImportes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOmitirValidacionDeImportes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOmitirValidacionDeImportes.Location = new System.Drawing.Point(284, 20);
            this.chkOmitirValidacionDeImportes.Name = "chkOmitirValidacionDeImportes";
            this.chkOmitirValidacionDeImportes.Size = new System.Drawing.Size(195, 17);
            this.chkOmitirValidacionDeImportes.TabIndex = 18;
            this.chkOmitirValidacionDeImportes.Text = "Omitir validación de importes";
            this.chkOmitirValidacionDeImportes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOmitirValidacionDeImportes.UseVisualStyleBackColor = true;
            // 
            // chkForzarInformacionSAT
            // 
            this.chkForzarInformacionSAT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkForzarInformacionSAT.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkForzarInformacionSAT.Location = new System.Drawing.Point(485, 20);
            this.chkForzarInformacionSAT.Name = "chkForzarInformacionSAT";
            this.chkForzarInformacionSAT.Size = new System.Drawing.Size(195, 17);
            this.chkForzarInformacionSAT.TabIndex = 17;
            this.chkForzarInformacionSAT.Text = "Forzar información del SAT";
            this.chkForzarInformacionSAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkForzarInformacionSAT.UseVisualStyleBackColor = true;
            // 
            // chkClaveSSA_Base___EsIdentificador
            // 
            this.chkClaveSSA_Base___EsIdentificador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkClaveSSA_Base___EsIdentificador.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkClaveSSA_Base___EsIdentificador.Location = new System.Drawing.Point(709, 20);
            this.chkClaveSSA_Base___EsIdentificador.Name = "chkClaveSSA_Base___EsIdentificador";
            this.chkClaveSSA_Base___EsIdentificador.Size = new System.Drawing.Size(277, 17);
            this.chkClaveSSA_Base___EsIdentificador.TabIndex = 1;
            this.chkClaveSSA_Base___EsIdentificador.Text = "Utilizar Clave SSA Base como Identificador";
            this.chkClaveSSA_Base___EsIdentificador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkClaveSSA_Base___EsIdentificador.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(470, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hoja : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Archivo : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblArchivoExcel
            // 
            this.lblArchivoExcel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblArchivoExcel.Location = new System.Drawing.Point(62, 45);
            this.lblArchivoExcel.MostrarToolTip = false;
            this.lblArchivoExcel.Name = "lblArchivoExcel";
            this.lblArchivoExcel.Size = new System.Drawing.Size(356, 20);
            this.lblArchivoExcel.TabIndex = 13;
            this.lblArchivoExcel.Text = "scLabelExt1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevoExcel,
            this.toolStripSeparator1,
            this.btnAbrirExcel,
            this.toolStripSeparator3,
            this.btnLeerExcel,
            this.toolStripSeparator4,
            this.btnBloquearHoja});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(988, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoExcel
            // 
            this.btnNuevoExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevoExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoExcel.Image")));
            this.btnNuevoExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoExcel.Name = "btnNuevoExcel";
            this.btnNuevoExcel.Size = new System.Drawing.Size(23, 22);
            this.btnNuevoExcel.Text = "Nuevo excel";
            this.btnNuevoExcel.Click += new System.EventHandler(this.btnNuevoExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirExcel.Image")));
            this.btnAbrirExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirExcel.Text = "&Abrir";
            this.btnAbrirExcel.Click += new System.EventHandler(this.btnAbrirExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLeerExcel
            // 
            this.btnLeerExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerExcel.Image")));
            this.btnLeerExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerExcel.Name = "btnLeerExcel";
            this.btnLeerExcel.Size = new System.Drawing.Size(23, 22);
            this.btnLeerExcel.Text = "Leer hoja";
            this.btnLeerExcel.ToolTipText = "Leer hoja";
            this.btnLeerExcel.Click += new System.EventHandler(this.btnLeerExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnBloquearHoja
            // 
            this.btnBloquearHoja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBloquearHoja.Image = ((System.Drawing.Image)(resources.GetObject("btnBloquearHoja.Image")));
            this.btnBloquearHoja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBloquearHoja.Name = "btnBloquearHoja";
            this.btnBloquearHoja.Size = new System.Drawing.Size(23, 22);
            this.btnBloquearHoja.Text = "Bloquear / Desbloquear hoja";
            this.btnBloquearHoja.Click += new System.EventHandler(this.btnBloquearHoja_Click);
            // 
            // cboHojas
            // 
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(526, 43);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(459, 21);
            this.cboHojas.TabIndex = 0;
            this.cboHojas.SelectedIndexChanged += new System.EventHandler(this.cboHojas_SelectedIndexChanged);
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmCFDI_Facturas_ImportarDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 581);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDetalles);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCFDI_Facturas_ImportarDatos";
            this.Text = "Generar Documentos Electrónicos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCFDI_Facturas_ImportarDatos_Load);
            this.Shown += new System.EventHandler(this.FrmCFDI_Facturas_ImportarDatos_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.menuConceptos.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
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
        private System.Windows.Forms.ContextMenuStrip menuConceptos;
        private System.Windows.Forms.ToolStripMenuItem btnAgregarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnModificarConcepto;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarConcepto;
        private System.Windows.Forms.ColumnHeader colTipoImpuesto;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevoExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAbrirExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLeerExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private SC_ControlsCS.scLabelExt lblArchivoExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnBloquearHoja;
        private SC_ControlsCS.scLabelExt lblSubTotal;
        private SC_ControlsCS.scLabelExt lblIva;
        private SC_ControlsCS.scLabelExt lblTotal;
        private SC_ControlsCS.scLabelExt lblTitulo_Total;
        private SC_ControlsCS.scLabelExt lblTitulo_Iva;
        private SC_ControlsCS.scLabelExt lblTitulo_SubTotal;
        private SC_ControlsCS.scLabelExt lblRegistros;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorVistaPrevia;
        private SC_ControlsCS.scLabelExt lblCantidadConLetra;
        private System.Windows.Forms.CheckBox chkClaveSSA_Base___EsIdentificador;
        private System.Windows.Forms.ColumnHeader colClaveLote;
        private System.Windows.Forms.ColumnHeader colCaducidad;
        private System.Windows.Forms.ToolStripButton btnConsultarTimbres;
        private System.Windows.Forms.ToolStripLabel lblTimbresDisponibles;
        private SC_ControlsCS.scLabelExt lblTitulo_Total_Calculado;
        private SC_ControlsCS.scLabelExt lblTitulo_Iva_Calculado;
        private SC_ControlsCS.scLabelExt lblSubTotal_Calculado;
        private SC_ControlsCS.scLabelExt lblTitulo_SubTotal_Calculado;
        private SC_ControlsCS.scLabelExt lblIva_Calculado;
        private SC_ControlsCS.scLabelExt lblTotal_Calculado;
        private System.Windows.Forms.ColumnHeader colSAT_ProductoServicio;
        private System.Windows.Forms.ColumnHeader colSAT_UnidadDeMedida;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboUsosCFDI;
        private System.Windows.Forms.CheckBox chkForzarInformacionSAT;
        private System.Windows.Forms.CheckBox chkOmitirValidacionDeImportes;
        private System.Windows.Forms.ToolStripButton btnEstablecimientos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}