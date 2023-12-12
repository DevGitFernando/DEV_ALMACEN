namespace Dll_IFacturacion.Configuracion
{
    partial class FrmFactSucursales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactSucursales));
            this.tabOpcionesConfiguracion = new SC_ControlsCS.scTabControlExt();
            this.tabDatosSucursal = new System.Windows.Forms.TabPage();
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
            this.txtRazonSocial = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSucursales = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.tabFoliosSeries = new System.Windows.Forms.TabPage();
            this.lvwFoliosSeries = new System.Windows.Forms.ListView();
            this.Asignado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Bloqueado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAñoAprobacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvNumAprobacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvSerie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTipoDocumento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDocumento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDesde = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHasta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvFolio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuFolios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAsignar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDesasignar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRFC = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.tabOpcionesConfiguracion.SuspendLayout();
            this.tabDatosSucursal.SuspendLayout();
            this.FrameDomicilioFiscal.SuspendLayout();
            this.FrameDatosSucursal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabFoliosSeries.SuspendLayout();
            this.menuFolios.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabOpcionesConfiguracion
            // 
            this.tabOpcionesConfiguracion.Appearance = SC_ControlsCS.scTabAppearance.Buttons;
            this.tabOpcionesConfiguracion.BackColor = System.Drawing.SystemColors.Control;
            this.tabOpcionesConfiguracion.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabOpcionesConfiguracion.Controls.Add(this.tabDatosSucursal);
            this.tabOpcionesConfiguracion.Controls.Add(this.tabFoliosSeries);
            this.tabOpcionesConfiguracion.CustomBackColor = false;
            this.tabOpcionesConfiguracion.CustomBackColorPages = false;
            this.tabOpcionesConfiguracion.Location = new System.Drawing.Point(7, 74);
            this.tabOpcionesConfiguracion.MostrarBorde = false;
            this.tabOpcionesConfiguracion.Name = "tabOpcionesConfiguracion";
            this.tabOpcionesConfiguracion.SelectedIndex = 0;
            this.tabOpcionesConfiguracion.Size = new System.Drawing.Size(696, 387);
            this.tabOpcionesConfiguracion.TabIndex = 2;
            this.tabOpcionesConfiguracion.SelectedIndexChanged += new System.EventHandler(this.tabOpcionesConfiguracion_SelectedIndexChanged);
            // 
            // tabDatosSucursal
            // 
            this.tabDatosSucursal.Controls.Add(this.FrameDomicilioFiscal);
            this.tabDatosSucursal.Controls.Add(this.FrameDatosSucursal);
            this.tabDatosSucursal.Controls.Add(this.groupBox2);
            this.tabDatosSucursal.Location = new System.Drawing.Point(4, 28);
            this.tabDatosSucursal.Name = "tabDatosSucursal";
            this.tabDatosSucursal.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatosSucursal.Size = new System.Drawing.Size(688, 355);
            this.tabDatosSucursal.TabIndex = 0;
            this.tabDatosSucursal.Text = "Datos de Sucursal";
            this.tabDatosSucursal.UseVisualStyleBackColor = true;
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
            this.FrameDomicilioFiscal.Location = new System.Drawing.Point(5, 127);
            this.FrameDomicilioFiscal.Name = "FrameDomicilioFiscal";
            this.FrameDomicilioFiscal.Size = new System.Drawing.Size(675, 224);
            this.FrameDomicilioFiscal.TabIndex = 22;
            this.FrameDomicilioFiscal.TabStop = false;
            this.FrameDomicilioFiscal.Text = "Información de Domicilio Fiscal";
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
            this.FrameDatosSucursal.Controls.Add(this.txtRazonSocial);
            this.FrameDatosSucursal.Controls.Add(this.label4);
            this.FrameDatosSucursal.Location = new System.Drawing.Point(5, 49);
            this.FrameDatosSucursal.Name = "FrameDatosSucursal";
            this.FrameDatosSucursal.Size = new System.Drawing.Size(675, 76);
            this.FrameDatosSucursal.TabIndex = 21;
            this.FrameDatosSucursal.TabStop = false;
            this.FrameDatosSucursal.Text = "Información de Sucursal";
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC.Decimales = 2;
            this.txtRFC.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC.ForeColor = System.Drawing.Color.Black;
            this.txtRFC.Location = new System.Drawing.Point(108, 45);
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
            this.label3.Location = new System.Drawing.Point(16, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "RFC :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRazonSocial.Decimales = 2;
            this.txtRazonSocial.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRazonSocial.ForeColor = System.Drawing.Color.Black;
            this.txtRazonSocial.Location = new System.Drawing.Point(108, 21);
            this.txtRazonSocial.MaxLength = 100;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.PermitirApostrofo = false;
            this.txtRazonSocial.PermitirNegativos = false;
            this.txtRazonSocial.Size = new System.Drawing.Size(553, 20);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Razón Social :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboSucursales);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.lblFarmacia);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Location = new System.Drawing.Point(6, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 47);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información";
            // 
            // cboSucursales
            // 
            this.cboSucursales.BackColorEnabled = System.Drawing.Color.White;
            this.cboSucursales.Data = "";
            this.cboSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSucursales.Filtro = " 1 = 1";
            this.cboSucursales.ListaItemsBusqueda = 20;
            this.cboSucursales.Location = new System.Drawing.Point(385, 17);
            this.cboSucursales.MostrarToolTip = false;
            this.cboSucursales.Name = "cboSucursales";
            this.cboSucursales.Size = new System.Drawing.Size(275, 21);
            this.cboSucursales.TabIndex = 17;
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(43, 19);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(62, 17);
            this.lblEstado.TabIndex = 19;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.Location = new System.Drawing.Point(320, 18);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(62, 17);
            this.lblFarmacia.TabIndex = 18;
            this.lblFarmacia.Text = "Farmacia :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(107, 17);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(189, 21);
            this.cboEstados.TabIndex = 16;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // tabFoliosSeries
            // 
            this.tabFoliosSeries.Controls.Add(this.lvwFoliosSeries);
            this.tabFoliosSeries.Location = new System.Drawing.Point(4, 28);
            this.tabFoliosSeries.Name = "tabFoliosSeries";
            this.tabFoliosSeries.Size = new System.Drawing.Size(688, 355);
            this.tabFoliosSeries.TabIndex = 4;
            this.tabFoliosSeries.Text = "Folios y Series";
            this.tabFoliosSeries.UseVisualStyleBackColor = true;
            // 
            // lvwFoliosSeries
            // 
            this.lvwFoliosSeries.BackColor = System.Drawing.SystemColors.Window;
            this.lvwFoliosSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Asignado,
            this.Bloqueado,
            this.lvAñoAprobacion,
            this.lvNumAprobacion,
            this.lvSerie,
            this.lvTipoDocumento,
            this.lvDocumento,
            this.lvDesde,
            this.lvHasta,
            this.lvFolio,
            this.Key});
            this.lvwFoliosSeries.ContextMenuStrip = this.menuFolios;
            this.lvwFoliosSeries.FullRowSelect = true;
            this.lvwFoliosSeries.Location = new System.Drawing.Point(6, 6);
            this.lvwFoliosSeries.MultiSelect = false;
            this.lvwFoliosSeries.Name = "lvwFoliosSeries";
            this.lvwFoliosSeries.ShowItemToolTips = true;
            this.lvwFoliosSeries.Size = new System.Drawing.Size(671, 335);
            this.lvwFoliosSeries.TabIndex = 0;
            this.lvwFoliosSeries.UseCompatibleStateImageBehavior = false;
            this.lvwFoliosSeries.View = System.Windows.Forms.View.Details;
            this.lvwFoliosSeries.SelectedIndexChanged += new System.EventHandler(this.lvwFoliosSeries_SelectedIndexChanged);
            // 
            // Asignado
            // 
            this.Asignado.Text = "Asignado";
            // 
            // Bloqueado
            // 
            this.Bloqueado.Text = "Bloqueado";
            this.Bloqueado.Width = 70;
            // 
            // lvAñoAprobacion
            // 
            this.lvAñoAprobacion.Text = "Año aprobación";
            this.lvAñoAprobacion.Width = 100;
            // 
            // lvNumAprobacion
            // 
            this.lvNumAprobacion.Text = "Núm. Aprobación";
            this.lvNumAprobacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvNumAprobacion.Width = 100;
            // 
            // lvSerie
            // 
            this.lvSerie.Text = "Serie";
            this.lvSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvSerie.Width = 100;
            // 
            // lvTipoDocumento
            // 
            this.lvTipoDocumento.Text = "Tipo documento";
            this.lvTipoDocumento.Width = 120;
            // 
            // lvDocumento
            // 
            this.lvDocumento.Text = "Documento";
            this.lvDocumento.Width = 120;
            // 
            // lvDesde
            // 
            this.lvDesde.Text = "Desde";
            this.lvDesde.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvDesde.Width = 90;
            // 
            // lvHasta
            // 
            this.lvHasta.Text = "Hasta";
            this.lvHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvHasta.Width = 90;
            // 
            // lvFolio
            // 
            this.lvFolio.Text = "Ultimo folio utilizado";
            this.lvFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvFolio.Width = 120;
            // 
            // Key
            // 
            this.Key.Text = "Identificador";
            // 
            // menuFolios
            // 
            this.menuFolios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAsignar,
            this.btnDesasignar});
            this.menuFolios.Name = "menuFolios";
            this.menuFolios.Size = new System.Drawing.Size(132, 48);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(152, 22);
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnDesasignar
            // 
            this.btnDesasignar.Name = "btnDesasignar";
            this.btnDesasignar.Size = new System.Drawing.Size(152, 22);
            this.btnDesasignar.Text = "Desasignar";
            this.btnDesasignar.Click += new System.EventHandler(this.btnDesasignar_Click);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(711, 25);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblRFC);
            this.groupBox1.Controls.Add(this.lblEmpresa);
            this.groupBox1.Controls.Add(this.cboEmpresas);
            this.groupBox1.Location = new System.Drawing.Point(7, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 44);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Empresas";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(483, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "RFC : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRFC
            // 
            this.lblRFC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRFC.Location = new System.Drawing.Point(537, 16);
            this.lblRFC.Name = "lblRFC";
            this.lblRFC.Size = new System.Drawing.Size(124, 18);
            this.lblRFC.TabIndex = 1;
            this.lblRFC.Text = "RFC Sucursal  :";
            this.lblRFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(53, 18);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(65, 17);
            this.lblEmpresa.TabIndex = 0;
            this.lblEmpresa.Text = "Empresa :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(119, 16);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(359, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // FrmFactSucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 467);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.tabOpcionesConfiguracion);
            this.Name = "FrmFactSucursales";
            this.Text = "Catálogo de Sucursales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFactSucursales_Load);
            this.tabOpcionesConfiguracion.ResumeLayout(false);
            this.tabDatosSucursal.ResumeLayout(false);
            this.FrameDomicilioFiscal.ResumeLayout(false);
            this.FrameDomicilioFiscal.PerformLayout();
            this.FrameDatosSucursal.ResumeLayout(false);
            this.FrameDatosSucursal.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabFoliosSeries.ResumeLayout(false);
            this.menuFolios.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SC_ControlsCS.scTabControlExt tabOpcionesConfiguracion;
        private System.Windows.Forms.TabPage tabDatosSucursal;
        private System.Windows.Forms.TabPage tabFoliosSeries;
        private System.Windows.Forms.ListView lvwFoliosSeries;
        private System.Windows.Forms.ColumnHeader lvAñoAprobacion;
        private System.Windows.Forms.ColumnHeader lvNumAprobacion;
        private System.Windows.Forms.ColumnHeader lvSerie;
        private System.Windows.Forms.ColumnHeader lvDesde;
        private System.Windows.Forms.ColumnHeader lvHasta;
        private System.Windows.Forms.ColumnHeader lvFolio;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEmpresa;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label lblRFC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip menuFolios;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scComboBoxExt cboSucursales;
        private System.Windows.Forms.GroupBox groupBox2;
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
        private SC_ControlsCS.scTextBoxExt txtRazonSocial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader Asignado;
        private System.Windows.Forms.ColumnHeader Bloqueado;
        private System.Windows.Forms.ColumnHeader Key;
        private System.Windows.Forms.ToolStripMenuItem btnAsignar;
        private System.Windows.Forms.ToolStripMenuItem btnDesasignar;
        private System.Windows.Forms.ColumnHeader lvDocumento;
        private System.Windows.Forms.ColumnHeader lvTipoDocumento;


    }
}