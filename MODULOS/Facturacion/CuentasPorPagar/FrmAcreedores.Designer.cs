namespace Facturacion.CuentasPorPagar
{
    partial class FrmAcreedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAcreedores));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDomicilioFiscal = new System.Windows.Forms.GroupBox();
            this.txtD_Referencia = new SC_ControlsCS.scTextBoxExt();
            this.label24 = new System.Windows.Forms.Label();
            this.txtD_CodigoPostal = new SC_ControlsCS.scTextBoxExt();
            this.label23 = new System.Windows.Forms.Label();
            this.txtD_NoInterior = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtD_NoExterior = new SC_ControlsCS.scTextBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.txtD_Localidad = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.txtD_Calle = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.txtD_Colonia = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.txtD_Municipio = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtD_Estado = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtD_Pais = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameDatosSucursal = new System.Windows.Forms.GroupBox();
            this.txtRFC = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.txtRazonSocial = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDomicilioFiscal.SuspendLayout();
            this.FrameDatosSucursal.SuspendLayout();
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
            this.btnImprimir,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(698, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDomicilioFiscal
            // 
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Referencia);
            this.FrameDomicilioFiscal.Controls.Add(this.label24);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_CodigoPostal);
            this.FrameDomicilioFiscal.Controls.Add(this.label23);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_NoInterior);
            this.FrameDomicilioFiscal.Controls.Add(this.label10);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_NoExterior);
            this.FrameDomicilioFiscal.Controls.Add(this.label14);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Localidad);
            this.FrameDomicilioFiscal.Controls.Add(this.label13);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Calle);
            this.FrameDomicilioFiscal.Controls.Add(this.label11);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Colonia);
            this.FrameDomicilioFiscal.Controls.Add(this.label12);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Municipio);
            this.FrameDomicilioFiscal.Controls.Add(this.label7);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Estado);
            this.FrameDomicilioFiscal.Controls.Add(this.label8);
            this.FrameDomicilioFiscal.Controls.Add(this.txtD_Pais);
            this.FrameDomicilioFiscal.Controls.Add(this.label9);
            this.FrameDomicilioFiscal.Location = new System.Drawing.Point(11, 123);
            this.FrameDomicilioFiscal.Name = "FrameDomicilioFiscal";
            this.FrameDomicilioFiscal.Size = new System.Drawing.Size(675, 205);
            this.FrameDomicilioFiscal.TabIndex = 4;
            this.FrameDomicilioFiscal.TabStop = false;
            this.FrameDomicilioFiscal.Text = "Información de Domicilio";
            // 
            // txtD_Referencia
            // 
            this.txtD_Referencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Referencia.Decimales = 2;
            this.txtD_Referencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Referencia.ForeColor = System.Drawing.Color.Black;
            this.txtD_Referencia.Location = new System.Drawing.Point(108, 175);
            this.txtD_Referencia.MaxLength = 100;
            this.txtD_Referencia.Name = "txtD_Referencia";
            this.txtD_Referencia.PermitirApostrofo = false;
            this.txtD_Referencia.PermitirNegativos = false;
            this.txtD_Referencia.Size = new System.Drawing.Size(553, 20);
            this.txtD_Referencia.TabIndex = 6;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(16, 179);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(91, 13);
            this.label24.TabIndex = 44;
            this.label24.Text = "Referencia  :";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_CodigoPostal
            // 
            this.txtD_CodigoPostal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_CodigoPostal.Decimales = 2;
            this.txtD_CodigoPostal.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_CodigoPostal.ForeColor = System.Drawing.Color.Black;
            this.txtD_CodigoPostal.Location = new System.Drawing.Point(518, 71);
            this.txtD_CodigoPostal.MaxLength = 8;
            this.txtD_CodigoPostal.Name = "txtD_CodigoPostal";
            this.txtD_CodigoPostal.PermitirApostrofo = false;
            this.txtD_CodigoPostal.PermitirNegativos = false;
            this.txtD_CodigoPostal.Size = new System.Drawing.Size(143, 20);
            this.txtD_CodigoPostal.TabIndex = 9;
            this.txtD_CodigoPostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(425, 75);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(91, 13);
            this.label23.TabIndex = 42;
            this.label23.Text = "* Código Postal :";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_NoInterior
            // 
            this.txtD_NoInterior.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_NoInterior.Decimales = 2;
            this.txtD_NoInterior.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_NoInterior.ForeColor = System.Drawing.Color.Black;
            this.txtD_NoInterior.Location = new System.Drawing.Point(518, 45);
            this.txtD_NoInterior.MaxLength = 8;
            this.txtD_NoInterior.Name = "txtD_NoInterior";
            this.txtD_NoInterior.PermitirApostrofo = false;
            this.txtD_NoInterior.PermitirNegativos = false;
            this.txtD_NoInterior.Size = new System.Drawing.Size(143, 20);
            this.txtD_NoInterior.TabIndex = 8;
            this.txtD_NoInterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(424, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "No. Interior  :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_NoExterior
            // 
            this.txtD_NoExterior.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_NoExterior.Decimales = 2;
            this.txtD_NoExterior.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_NoExterior.ForeColor = System.Drawing.Color.Black;
            this.txtD_NoExterior.Location = new System.Drawing.Point(518, 19);
            this.txtD_NoExterior.MaxLength = 8;
            this.txtD_NoExterior.Name = "txtD_NoExterior";
            this.txtD_NoExterior.PermitirApostrofo = false;
            this.txtD_NoExterior.PermitirNegativos = false;
            this.txtD_NoExterior.Size = new System.Drawing.Size(143, 20);
            this.txtD_NoExterior.TabIndex = 7;
            this.txtD_NoExterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(424, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "* No. Exterior  :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Localidad
            // 
            this.txtD_Localidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Localidad.Decimales = 2;
            this.txtD_Localidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Localidad.ForeColor = System.Drawing.Color.Black;
            this.txtD_Localidad.Location = new System.Drawing.Point(108, 97);
            this.txtD_Localidad.MaxLength = 100;
            this.txtD_Localidad.Name = "txtD_Localidad";
            this.txtD_Localidad.PermitirApostrofo = false;
            this.txtD_Localidad.PermitirNegativos = false;
            this.txtD_Localidad.Size = new System.Drawing.Size(302, 20);
            this.txtD_Localidad.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 101);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "* Localidad  :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Calle
            // 
            this.txtD_Calle.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Calle.Decimales = 2;
            this.txtD_Calle.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Calle.ForeColor = System.Drawing.Color.Black;
            this.txtD_Calle.Location = new System.Drawing.Point(108, 149);
            this.txtD_Calle.MaxLength = 100;
            this.txtD_Calle.Name = "txtD_Calle";
            this.txtD_Calle.PermitirApostrofo = false;
            this.txtD_Calle.PermitirNegativos = false;
            this.txtD_Calle.Size = new System.Drawing.Size(302, 20);
            this.txtD_Calle.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "* Calle  :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Colonia
            // 
            this.txtD_Colonia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Colonia.Decimales = 2;
            this.txtD_Colonia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Colonia.ForeColor = System.Drawing.Color.Black;
            this.txtD_Colonia.Location = new System.Drawing.Point(108, 123);
            this.txtD_Colonia.MaxLength = 100;
            this.txtD_Colonia.Name = "txtD_Colonia";
            this.txtD_Colonia.PermitirApostrofo = false;
            this.txtD_Colonia.PermitirNegativos = false;
            this.txtD_Colonia.Size = new System.Drawing.Size(302, 20);
            this.txtD_Colonia.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(16, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "* Colonia  :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Municipio
            // 
            this.txtD_Municipio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Municipio.Decimales = 2;
            this.txtD_Municipio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Municipio.ForeColor = System.Drawing.Color.Black;
            this.txtD_Municipio.Location = new System.Drawing.Point(108, 71);
            this.txtD_Municipio.MaxLength = 100;
            this.txtD_Municipio.Name = "txtD_Municipio";
            this.txtD_Municipio.PermitirApostrofo = false;
            this.txtD_Municipio.PermitirNegativos = false;
            this.txtD_Municipio.Size = new System.Drawing.Size(302, 20);
            this.txtD_Municipio.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "* Municipio  :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Estado
            // 
            this.txtD_Estado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Estado.Decimales = 2;
            this.txtD_Estado.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Estado.ForeColor = System.Drawing.Color.Black;
            this.txtD_Estado.Location = new System.Drawing.Point(108, 45);
            this.txtD_Estado.MaxLength = 100;
            this.txtD_Estado.Name = "txtD_Estado";
            this.txtD_Estado.PermitirApostrofo = false;
            this.txtD_Estado.PermitirNegativos = false;
            this.txtD_Estado.Size = new System.Drawing.Size(302, 20);
            this.txtD_Estado.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "* Estado  :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Pais
            // 
            this.txtD_Pais.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Pais.Decimales = 2;
            this.txtD_Pais.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Pais.ForeColor = System.Drawing.Color.Black;
            this.txtD_Pais.Location = new System.Drawing.Point(108, 19);
            this.txtD_Pais.MaxLength = 100;
            this.txtD_Pais.Name = "txtD_Pais";
            this.txtD_Pais.PermitirApostrofo = false;
            this.txtD_Pais.PermitirNegativos = false;
            this.txtD_Pais.Size = new System.Drawing.Size(302, 20);
            this.txtD_Pais.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "* País  :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatosSucursal
            // 
            this.FrameDatosSucursal.Controls.Add(this.txtRFC);
            this.FrameDatosSucursal.Controls.Add(this.label3);
            this.FrameDatosSucursal.Controls.Add(this.lblCancelado);
            this.FrameDatosSucursal.Controls.Add(this.txtId);
            this.FrameDatosSucursal.Controls.Add(this.txtRazonSocial);
            this.FrameDatosSucursal.Controls.Add(this.label1);
            this.FrameDatosSucursal.Controls.Add(this.label2);
            this.FrameDatosSucursal.Location = new System.Drawing.Point(11, 26);
            this.FrameDatosSucursal.Name = "FrameDatosSucursal";
            this.FrameDatosSucursal.Size = new System.Drawing.Size(675, 94);
            this.FrameDatosSucursal.TabIndex = 3;
            this.FrameDatosSucursal.TabStop = false;
            this.FrameDatosSucursal.Text = "Información del Acreedor";
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC.Decimales = 2;
            this.txtRFC.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC.ForeColor = System.Drawing.Color.Black;
            this.txtRFC.Location = new System.Drawing.Point(108, 68);
            this.txtRFC.MaxLength = 13;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.PermitirApostrofo = false;
            this.txtRFC.PermitirNegativos = false;
            this.txtRFC.Size = new System.Drawing.Size(125, 20);
            this.txtRFC.TabIndex = 2;
            this.txtRFC.Text = "123456789012345";
            this.txtRFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "RFC :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(205, 22);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 12;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(108, 19);
            this.txtId.MaxLength = 4;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(83, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazonSocial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRazonSocial.Decimales = 2;
            this.txtRazonSocial.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRazonSocial.ForeColor = System.Drawing.Color.Black;
            this.txtRazonSocial.Location = new System.Drawing.Point(108, 44);
            this.txtRazonSocial.MaxLength = 100;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.PermitirApostrofo = false;
            this.txtRazonSocial.PermitirNegativos = false;
            this.txtRazonSocial.Size = new System.Drawing.Size(553, 20);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Clave Acreedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nombre  :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmAcreedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 340);
            this.Controls.Add(this.FrameDomicilioFiscal);
            this.Controls.Add(this.FrameDatosSucursal);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmAcreedores";
            this.Text = "Catálogo de Acreedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAcreedores_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDomicilioFiscal.ResumeLayout(false);
            this.FrameDomicilioFiscal.PerformLayout();
            this.FrameDatosSucursal.ResumeLayout(false);
            this.FrameDatosSucursal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameDomicilioFiscal;
        private SC_ControlsCS.scTextBoxExt txtD_Referencia;
        private System.Windows.Forms.Label label24;
        private SC_ControlsCS.scTextBoxExt txtD_CodigoPostal;
        private System.Windows.Forms.Label label23;
        private SC_ControlsCS.scTextBoxExt txtD_NoInterior;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtD_NoExterior;
        private System.Windows.Forms.Label label14;
        private SC_ControlsCS.scTextBoxExt txtD_Localidad;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtD_Calle;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtD_Colonia;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scTextBoxExt txtD_Municipio;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtD_Estado;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtD_Pais;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox FrameDatosSucursal;
        private SC_ControlsCS.scTextBoxExt txtRFC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtRazonSocial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}