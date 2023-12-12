namespace DllFarmaciaSoft
{
    partial class FrmDecodificacionSNK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDecodificacionSNK));
            this.FrameCodigo = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new SC_ControlsCS.scTextBoxExt();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.lblDescripcionClaveSSA = new SC_ControlsCS.scLabelExt();
            this.scLabelExt11 = new SC_ControlsCS.scLabelExt();
            this.lblClaveSSA = new SC_ControlsCS.scLabelExt();
            this.scLabelExt7 = new SC_ControlsCS.scLabelExt();
            this.lblNombreComercial = new SC_ControlsCS.scLabelExt();
            this.scLabelExt5 = new SC_ControlsCS.scLabelExt();
            this.lblUUID = new SC_ControlsCS.scLabelExt();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.lblResultado = new SC_ControlsCS.scLabelExt();
            this.lblNumeroDeFactura = new SC_ControlsCS.scLabelExt();
            this.scLabelExt20 = new SC_ControlsCS.scLabelExt();
            this.lblProveedor = new SC_ControlsCS.scLabelExt();
            this.scLabelExt18 = new SC_ControlsCS.scLabelExt();
            this.lblCaducidad = new SC_ControlsCS.scLabelExt();
            this.scLabelExt16 = new SC_ControlsCS.scLabelExt();
            this.lblClaveLote = new SC_ControlsCS.scLabelExt();
            this.scLabelExt14 = new SC_ControlsCS.scLabelExt();
            this.lblSubFarmacia = new SC_ControlsCS.scLabelExt();
            this.scLabelExt12 = new SC_ControlsCS.scLabelExt();
            this.lblCodigoEAN = new SC_ControlsCS.scLabelExt();
            this.scLabelExt10 = new SC_ControlsCS.scLabelExt();
            this.lblCodificadora = new SC_ControlsCS.scLabelExt();
            this.scLabelExt8 = new SC_ControlsCS.scLabelExt();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.scLabelExt6 = new SC_ControlsCS.scLabelExt();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.lblEmpresa = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameCodigo.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameCodigo
            // 
            this.FrameCodigo.Controls.Add(this.txtCodigo);
            this.FrameCodigo.Location = new System.Drawing.Point(12, 32);
            this.FrameCodigo.Name = "FrameCodigo";
            this.FrameCodigo.Size = new System.Drawing.Size(661, 74);
            this.FrameCodigo.TabIndex = 0;
            this.FrameCodigo.TabStop = false;
            this.FrameCodigo.Text = "Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigo.Decimales = 2;
            this.txtCodigo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Location = new System.Drawing.Point(11, 17);
            this.txtCodigo.MaxLength = 100;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.PermitirApostrofo = false;
            this.txtCodigo.PermitirNegativos = false;
            this.txtCodigo.Size = new System.Drawing.Size(641, 47);
            this.txtCodigo.TabIndex = 3;
            this.txtCodigo.Text = "00209000201007503001007663001|";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigo_Validating);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lblDescripcionClaveSSA);
            this.FrameInformacion.Controls.Add(this.scLabelExt11);
            this.FrameInformacion.Controls.Add(this.lblClaveSSA);
            this.FrameInformacion.Controls.Add(this.scLabelExt7);
            this.FrameInformacion.Controls.Add(this.lblNombreComercial);
            this.FrameInformacion.Controls.Add(this.scLabelExt5);
            this.FrameInformacion.Controls.Add(this.lblUUID);
            this.FrameInformacion.Controls.Add(this.scLabelExt3);
            this.FrameInformacion.Controls.Add(this.lblResultado);
            this.FrameInformacion.Controls.Add(this.lblNumeroDeFactura);
            this.FrameInformacion.Controls.Add(this.scLabelExt20);
            this.FrameInformacion.Controls.Add(this.lblProveedor);
            this.FrameInformacion.Controls.Add(this.scLabelExt18);
            this.FrameInformacion.Controls.Add(this.lblCaducidad);
            this.FrameInformacion.Controls.Add(this.scLabelExt16);
            this.FrameInformacion.Controls.Add(this.lblClaveLote);
            this.FrameInformacion.Controls.Add(this.scLabelExt14);
            this.FrameInformacion.Controls.Add(this.lblSubFarmacia);
            this.FrameInformacion.Controls.Add(this.scLabelExt12);
            this.FrameInformacion.Controls.Add(this.lblCodigoEAN);
            this.FrameInformacion.Controls.Add(this.scLabelExt10);
            this.FrameInformacion.Controls.Add(this.lblCodificadora);
            this.FrameInformacion.Controls.Add(this.scLabelExt8);
            this.FrameInformacion.Controls.Add(this.lblFarmacia);
            this.FrameInformacion.Controls.Add(this.scLabelExt6);
            this.FrameInformacion.Controls.Add(this.lblEstado);
            this.FrameInformacion.Controls.Add(this.scLabelExt4);
            this.FrameInformacion.Controls.Add(this.lblEmpresa);
            this.FrameInformacion.Controls.Add(this.scLabelExt1);
            this.FrameInformacion.Location = new System.Drawing.Point(12, 110);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(661, 398);
            this.FrameInformacion.TabIndex = 1;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información";
            // 
            // lblDescripcionClaveSSA
            // 
            this.lblDescripcionClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcionClaveSSA.Location = new System.Drawing.Point(130, 160);
            this.lblDescripcionClaveSSA.MostrarToolTip = false;
            this.lblDescripcionClaveSSA.Name = "lblDescripcionClaveSSA";
            this.lblDescripcionClaveSSA.Size = new System.Drawing.Size(522, 20);
            this.lblDescripcionClaveSSA.TabIndex = 28;
            this.lblDescripcionClaveSSA.Text = "scLabelExt7";
            this.lblDescripcionClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt11
            // 
            this.scLabelExt11.Location = new System.Drawing.Point(5, 160);
            this.scLabelExt11.MostrarToolTip = false;
            this.scLabelExt11.Name = "scLabelExt11";
            this.scLabelExt11.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt11.TabIndex = 27;
            this.scLabelExt11.Text = "Descripción Clave SSA : ";
            this.scLabelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(130, 136);
            this.lblClaveSSA.MostrarToolTip = false;
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(522, 20);
            this.lblClaveSSA.TabIndex = 26;
            this.lblClaveSSA.Text = "scLabelExt7";
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt7
            // 
            this.scLabelExt7.Location = new System.Drawing.Point(5, 136);
            this.scLabelExt7.MostrarToolTip = false;
            this.scLabelExt7.Name = "scLabelExt7";
            this.scLabelExt7.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt7.TabIndex = 25;
            this.scLabelExt7.Text = "Clave SSA : ";
            this.scLabelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreComercial
            // 
            this.lblNombreComercial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreComercial.Location = new System.Drawing.Point(130, 208);
            this.lblNombreComercial.MostrarToolTip = false;
            this.lblNombreComercial.Name = "lblNombreComercial";
            this.lblNombreComercial.Size = new System.Drawing.Size(522, 20);
            this.lblNombreComercial.TabIndex = 24;
            this.lblNombreComercial.Text = "scLabelExt9";
            this.lblNombreComercial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt5
            // 
            this.scLabelExt5.Location = new System.Drawing.Point(5, 208);
            this.scLabelExt5.MostrarToolTip = false;
            this.scLabelExt5.Name = "scLabelExt5";
            this.scLabelExt5.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt5.TabIndex = 23;
            this.scLabelExt5.Text = "Nombre comercial : ";
            this.scLabelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUUID
            // 
            this.lblUUID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUUID.Location = new System.Drawing.Point(130, 17);
            this.lblUUID.MostrarToolTip = false;
            this.lblUUID.Name = "lblUUID";
            this.lblUUID.Size = new System.Drawing.Size(522, 20);
            this.lblUUID.TabIndex = 22;
            this.lblUUID.Text = "scLabelExt2";
            this.lblUUID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.Location = new System.Drawing.Point(5, 17);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt3.TabIndex = 21;
            this.scLabelExt3.Text = "UUID : ";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResultado
            // 
            this.lblResultado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.Location = new System.Drawing.Point(130, 351);
            this.lblResultado.MostrarToolTip = false;
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(522, 37);
            this.lblResultado.TabIndex = 20;
            this.lblResultado.Text = "scLabelExt19";
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNumeroDeFactura
            // 
            this.lblNumeroDeFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumeroDeFactura.Location = new System.Drawing.Point(130, 326);
            this.lblNumeroDeFactura.MostrarToolTip = false;
            this.lblNumeroDeFactura.Name = "lblNumeroDeFactura";
            this.lblNumeroDeFactura.Size = new System.Drawing.Size(522, 20);
            this.lblNumeroDeFactura.TabIndex = 19;
            this.lblNumeroDeFactura.Text = "scLabelExt19";
            this.lblNumeroDeFactura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt20
            // 
            this.scLabelExt20.Location = new System.Drawing.Point(5, 326);
            this.scLabelExt20.MostrarToolTip = false;
            this.scLabelExt20.Name = "scLabelExt20";
            this.scLabelExt20.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt20.TabIndex = 18;
            this.scLabelExt20.Text = "Referencia : ";
            this.scLabelExt20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(130, 302);
            this.lblProveedor.MostrarToolTip = false;
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(522, 20);
            this.lblProveedor.TabIndex = 17;
            this.lblProveedor.Text = "scLabelExt17";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt18
            // 
            this.scLabelExt18.Location = new System.Drawing.Point(5, 302);
            this.scLabelExt18.MostrarToolTip = false;
            this.scLabelExt18.Name = "scLabelExt18";
            this.scLabelExt18.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt18.TabIndex = 16;
            this.scLabelExt18.Text = "Origen : ";
            this.scLabelExt18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCaducidad
            // 
            this.lblCaducidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCaducidad.Location = new System.Drawing.Point(130, 279);
            this.lblCaducidad.MostrarToolTip = false;
            this.lblCaducidad.Name = "lblCaducidad";
            this.lblCaducidad.Size = new System.Drawing.Size(522, 20);
            this.lblCaducidad.TabIndex = 15;
            this.lblCaducidad.Text = "scLabelExt15";
            this.lblCaducidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt16
            // 
            this.scLabelExt16.Location = new System.Drawing.Point(5, 279);
            this.scLabelExt16.MostrarToolTip = false;
            this.scLabelExt16.Name = "scLabelExt16";
            this.scLabelExt16.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt16.TabIndex = 14;
            this.scLabelExt16.Text = "Caducidad : ";
            this.scLabelExt16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveLote
            // 
            this.lblClaveLote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveLote.Location = new System.Drawing.Point(130, 256);
            this.lblClaveLote.MostrarToolTip = false;
            this.lblClaveLote.Name = "lblClaveLote";
            this.lblClaveLote.Size = new System.Drawing.Size(522, 20);
            this.lblClaveLote.TabIndex = 13;
            this.lblClaveLote.Text = "scLabelExt13";
            this.lblClaveLote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt14
            // 
            this.scLabelExt14.Location = new System.Drawing.Point(5, 256);
            this.scLabelExt14.MostrarToolTip = false;
            this.scLabelExt14.Name = "scLabelExt14";
            this.scLabelExt14.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt14.TabIndex = 12;
            this.scLabelExt14.Text = "Lote : ";
            this.scLabelExt14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubFarmacia
            // 
            this.lblSubFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubFarmacia.Location = new System.Drawing.Point(130, 232);
            this.lblSubFarmacia.MostrarToolTip = false;
            this.lblSubFarmacia.Name = "lblSubFarmacia";
            this.lblSubFarmacia.Size = new System.Drawing.Size(522, 20);
            this.lblSubFarmacia.TabIndex = 11;
            this.lblSubFarmacia.Text = "scLabelExt11";
            this.lblSubFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt12
            // 
            this.scLabelExt12.Location = new System.Drawing.Point(5, 232);
            this.scLabelExt12.MostrarToolTip = false;
            this.scLabelExt12.Name = "scLabelExt12";
            this.scLabelExt12.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt12.TabIndex = 10;
            this.scLabelExt12.Text = "Sub-Farmacia : ";
            this.scLabelExt12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodigoEAN
            // 
            this.lblCodigoEAN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodigoEAN.Location = new System.Drawing.Point(130, 184);
            this.lblCodigoEAN.MostrarToolTip = false;
            this.lblCodigoEAN.Name = "lblCodigoEAN";
            this.lblCodigoEAN.Size = new System.Drawing.Size(522, 20);
            this.lblCodigoEAN.TabIndex = 9;
            this.lblCodigoEAN.Text = "scLabelExt9";
            this.lblCodigoEAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt10
            // 
            this.scLabelExt10.Location = new System.Drawing.Point(5, 184);
            this.scLabelExt10.MostrarToolTip = false;
            this.scLabelExt10.Name = "scLabelExt10";
            this.scLabelExt10.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt10.TabIndex = 8;
            this.scLabelExt10.Text = "Código EAN : ";
            this.scLabelExt10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCodificadora
            // 
            this.lblCodificadora.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCodificadora.Location = new System.Drawing.Point(130, 112);
            this.lblCodificadora.MostrarToolTip = false;
            this.lblCodificadora.Name = "lblCodificadora";
            this.lblCodificadora.Size = new System.Drawing.Size(522, 20);
            this.lblCodificadora.TabIndex = 7;
            this.lblCodificadora.Text = "scLabelExt7";
            this.lblCodificadora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt8
            // 
            this.scLabelExt8.Location = new System.Drawing.Point(5, 112);
            this.scLabelExt8.MostrarToolTip = false;
            this.scLabelExt8.Name = "scLabelExt8";
            this.scLabelExt8.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt8.TabIndex = 6;
            this.scLabelExt8.Text = "Codificadora : ";
            this.scLabelExt8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(130, 89);
            this.lblFarmacia.MostrarToolTip = false;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(522, 20);
            this.lblFarmacia.TabIndex = 5;
            this.lblFarmacia.Text = "scLabelExt5";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt6
            // 
            this.scLabelExt6.Location = new System.Drawing.Point(5, 89);
            this.scLabelExt6.MostrarToolTip = false;
            this.scLabelExt6.Name = "scLabelExt6";
            this.scLabelExt6.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt6.TabIndex = 4;
            this.scLabelExt6.Text = "Unidad : ";
            this.scLabelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(130, 65);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(522, 20);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "scLabelExt3";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Location = new System.Drawing.Point(5, 65);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt4.TabIndex = 2;
            this.scLabelExt4.Text = "Estado : ";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEmpresa.Location = new System.Drawing.Point(130, 41);
            this.lblEmpresa.MostrarToolTip = false;
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(522, 20);
            this.lblEmpresa.TabIndex = 1;
            this.lblEmpresa.Text = "scLabelExt2";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(5, 41);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(127, 16);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Empresa : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(682, 25);
            this.toolStripBarraMenu.TabIndex = 8;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Limpiar pantalla";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmDecodificacionSNK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 518);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.FrameCodigo);
            this.Name = "FrmDecodificacionSNK";
            this.Text = "Decodificación de productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDecodificacionSNK_FormClosing);
            this.Load += new System.EventHandler(this.FrmDecodificacionSNK_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDecodificacionSNK_KeyDown);
            this.FrameCodigo.ResumeLayout(false);
            this.FrameCodigo.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameCodigo;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private SC_ControlsCS.scTextBoxExt txtCodigo;
        private SC_ControlsCS.scLabelExt lblEmpresa;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scLabelExt scLabelExt6;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt lblCodificadora;
        private SC_ControlsCS.scLabelExt scLabelExt8;
        private SC_ControlsCS.scLabelExt lblCodigoEAN;
        private SC_ControlsCS.scLabelExt scLabelExt10;
        private SC_ControlsCS.scLabelExt lblSubFarmacia;
        private SC_ControlsCS.scLabelExt scLabelExt12;
        private SC_ControlsCS.scLabelExt lblClaveLote;
        private SC_ControlsCS.scLabelExt scLabelExt14;
        private SC_ControlsCS.scLabelExt lblCaducidad;
        private SC_ControlsCS.scLabelExt scLabelExt16;
        private SC_ControlsCS.scLabelExt lblNumeroDeFactura;
        private SC_ControlsCS.scLabelExt scLabelExt20;
        private SC_ControlsCS.scLabelExt lblProveedor;
        private SC_ControlsCS.scLabelExt scLabelExt18;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private SC_ControlsCS.scLabelExt lblResultado;
        private SC_ControlsCS.scLabelExt lblUUID;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private SC_ControlsCS.scLabelExt lblNombreComercial;
        private SC_ControlsCS.scLabelExt scLabelExt5;
        private SC_ControlsCS.scLabelExt lblDescripcionClaveSSA;
        private SC_ControlsCS.scLabelExt scLabelExt11;
        private SC_ControlsCS.scLabelExt lblClaveSSA;
        private SC_ControlsCS.scLabelExt scLabelExt7;
    }
}