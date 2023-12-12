namespace Facturacion.Catalogos
{
    partial class FrmBeneficiarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBeneficiarios));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.label8 = new System.Windows.Forms.Label();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.txtIdFar = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNomFar = new System.Windows.Forms.Label();
            this.txtCP = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.txtClaveMun = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblClaveMun = new System.Windows.Forms.Label();
            this.txtDom = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPais = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPais = new System.Windows.Forms.Label();
            this.txtClaveLocalidad = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblClaveLocalidad = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboColonia = new SC_ControlsCS.scComboBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtClaveEstado = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblClaveEstado = new System.Windows.Forms.Label();
            this.txtNumExterno = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumInterno = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.lblCliente = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSubCliente = new SC_ControlsCS.scTextBoxExt();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.lblBeneficiario = new System.Windows.Forms.Label();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnGuardar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(668, 25);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(23, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(110, 21);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(519, 21);
            this.cboEdo.TabIndex = 0;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // txtIdFar
            // 
            this.txtIdFar.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFar.Decimales = 2;
            this.txtIdFar.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFar.ForeColor = System.Drawing.Color.Black;
            this.txtIdFar.Location = new System.Drawing.Point(110, 45);
            this.txtIdFar.MaxLength = 4;
            this.txtIdFar.Name = "txtIdFar";
            this.txtIdFar.PermitirApostrofo = false;
            this.txtIdFar.PermitirNegativos = false;
            this.txtIdFar.Size = new System.Drawing.Size(66, 20);
            this.txtIdFar.TabIndex = 1;
            this.txtIdFar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdFar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdFar_KeyDown);
            this.txtIdFar.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdFar_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(35, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNomFar
            // 
            this.lblNomFar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomFar.Location = new System.Drawing.Point(179, 45);
            this.lblNomFar.Name = "lblNomFar";
            this.lblNomFar.Size = new System.Drawing.Size(451, 20);
            this.lblNomFar.TabIndex = 47;
            this.lblNomFar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCP
            // 
            this.txtCP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCP.Decimales = 2;
            this.txtCP.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCP.ForeColor = System.Drawing.Color.Black;
            this.txtCP.Location = new System.Drawing.Point(110, 168);
            this.txtCP.MaxLength = 10;
            this.txtCP.Name = "txtCP";
            this.txtCP.PermitirApostrofo = false;
            this.txtCP.PermitirNegativos = false;
            this.txtCP.Size = new System.Drawing.Size(154, 20);
            this.txtCP.TabIndex = 5;
            this.txtCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCP.Validating += new System.ComponentModel.CancelEventHandler(this.txtCP_Validating);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(26, 172);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Código postal :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveMun
            // 
            this.txtClaveMun.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveMun.Decimales = 2;
            this.txtClaveMun.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveMun.ForeColor = System.Drawing.Color.Black;
            this.txtClaveMun.Location = new System.Drawing.Point(110, 244);
            this.txtClaveMun.MaxLength = 4;
            this.txtClaveMun.Name = "txtClaveMun";
            this.txtClaveMun.PermitirApostrofo = false;
            this.txtClaveMun.PermitirNegativos = false;
            this.txtClaveMun.Size = new System.Drawing.Size(66, 20);
            this.txtClaveMun.TabIndex = 8;
            this.txtClaveMun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(35, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Municipio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveMun
            // 
            this.lblClaveMun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveMun.Location = new System.Drawing.Point(177, 244);
            this.lblClaveMun.Name = "lblClaveMun";
            this.lblClaveMun.Size = new System.Drawing.Size(452, 20);
            this.lblClaveMun.TabIndex = 54;
            this.lblClaveMun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDom
            // 
            this.txtDom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDom.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDom.Decimales = 2;
            this.txtDom.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDom.ForeColor = System.Drawing.Color.Black;
            this.txtDom.Location = new System.Drawing.Point(110, 320);
            this.txtDom.MaxLength = 100;
            this.txtDom.Name = "txtDom";
            this.txtDom.PermitirApostrofo = false;
            this.txtDom.PermitirNegativos = false;
            this.txtDom.Size = new System.Drawing.Size(524, 20);
            this.txtDom.TabIndex = 11;
            this.txtDom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(35, 324);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Domicilio :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPais
            // 
            this.txtPais.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPais.Decimales = 2;
            this.txtPais.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPais.ForeColor = System.Drawing.Color.Black;
            this.txtPais.Location = new System.Drawing.Point(110, 192);
            this.txtPais.MaxLength = 4;
            this.txtPais.Name = "txtPais";
            this.txtPais.PermitirApostrofo = false;
            this.txtPais.PermitirNegativos = false;
            this.txtPais.Size = new System.Drawing.Size(66, 20);
            this.txtPais.TabIndex = 6;
            this.txtPais.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(35, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Pais :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPais
            // 
            this.lblPais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPais.Location = new System.Drawing.Point(177, 192);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(452, 20);
            this.lblPais.TabIndex = 60;
            this.lblPais.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClaveLocalidad
            // 
            this.txtClaveLocalidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveLocalidad.Decimales = 2;
            this.txtClaveLocalidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveLocalidad.ForeColor = System.Drawing.Color.Black;
            this.txtClaveLocalidad.Location = new System.Drawing.Point(110, 269);
            this.txtClaveLocalidad.MaxLength = 4;
            this.txtClaveLocalidad.Name = "txtClaveLocalidad";
            this.txtClaveLocalidad.PermitirApostrofo = false;
            this.txtClaveLocalidad.PermitirNegativos = false;
            this.txtClaveLocalidad.Size = new System.Drawing.Size(66, 20);
            this.txtClaveLocalidad.TabIndex = 9;
            this.txtClaveLocalidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(35, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Localidad :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveLocalidad
            // 
            this.lblClaveLocalidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveLocalidad.Location = new System.Drawing.Point(177, 269);
            this.lblClaveLocalidad.Name = "lblClaveLocalidad";
            this.lblClaveLocalidad.Size = new System.Drawing.Size(452, 20);
            this.lblClaveLocalidad.TabIndex = 63;
            this.lblClaveLocalidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Colonia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColonia
            // 
            this.cboColonia.BackColorEnabled = System.Drawing.Color.White;
            this.cboColonia.Data = "";
            this.cboColonia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColonia.Filtro = " 1 = 1";
            this.cboColonia.FormattingEnabled = true;
            this.cboColonia.ListaItemsBusqueda = 20;
            this.cboColonia.Location = new System.Drawing.Point(110, 294);
            this.cboColonia.MostrarToolTip = false;
            this.cboColonia.Name = "cboColonia";
            this.cboColonia.Size = new System.Drawing.Size(520, 21);
            this.cboColonia.TabIndex = 10;
            // 
            // lblCancelado
            // 
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(313, 168);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(157, 18);
            this.lblCancelado.TabIndex = 66;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtClaveEstado
            // 
            this.txtClaveEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveEstado.Decimales = 2;
            this.txtClaveEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveEstado.ForeColor = System.Drawing.Color.Black;
            this.txtClaveEstado.Location = new System.Drawing.Point(110, 219);
            this.txtClaveEstado.MaxLength = 4;
            this.txtClaveEstado.Name = "txtClaveEstado";
            this.txtClaveEstado.PermitirApostrofo = false;
            this.txtClaveEstado.PermitirNegativos = false;
            this.txtClaveEstado.Size = new System.Drawing.Size(66, 20);
            this.txtClaveEstado.TabIndex = 7;
            this.txtClaveEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(35, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Estado :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveEstado
            // 
            this.lblClaveEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveEstado.Location = new System.Drawing.Point(177, 219);
            this.lblClaveEstado.Name = "lblClaveEstado";
            this.lblClaveEstado.Size = new System.Drawing.Size(452, 20);
            this.lblClaveEstado.TabIndex = 69;
            this.lblClaveEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumExterno
            // 
            this.txtNumExterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumExterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumExterno.Decimales = 2;
            this.txtNumExterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumExterno.ForeColor = System.Drawing.Color.Black;
            this.txtNumExterno.Location = new System.Drawing.Point(110, 346);
            this.txtNumExterno.MaxLength = 100;
            this.txtNumExterno.Name = "txtNumExterno";
            this.txtNumExterno.PermitirApostrofo = false;
            this.txtNumExterno.PermitirNegativos = false;
            this.txtNumExterno.Size = new System.Drawing.Size(165, 20);
            this.txtNumExterno.TabIndex = 12;
            this.txtNumExterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(35, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "No. Exterior :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumInterno
            // 
            this.txtNumInterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumInterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumInterno.Decimales = 2;
            this.txtNumInterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumInterno.ForeColor = System.Drawing.Color.Black;
            this.txtNumInterno.Location = new System.Drawing.Point(462, 346);
            this.txtNumInterno.MaxLength = 100;
            this.txtNumInterno.Name = "txtNumInterno";
            this.txtNumInterno.PermitirApostrofo = false;
            this.txtNumInterno.PermitirNegativos = false;
            this.txtNumInterno.Size = new System.Drawing.Size(166, 20);
            this.txtNumInterno.TabIndex = 13;
            this.txtNumInterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(389, 350);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "No. Exterior :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferencia
            // 
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(110, 372);
            this.txtReferencia.MaxLength = 100;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(524, 20);
            this.txtReferencia.TabIndex = 14;
            this.txtReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblReferencia
            // 
            this.lblReferencia.Location = new System.Drawing.Point(35, 376);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(71, 13);
            this.lblReferencia.TabIndex = 75;
            this.lblReferencia.Text = "Referencia :";
            this.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(35, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 16);
            this.label13.TabIndex = 78;
            this.label13.Text = "Cliente :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(110, 70);
            this.txtCliente.MaxLength = 4;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(66, 20);
            this.txtCliente.TabIndex = 2;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCliente_KeyDown);
            this.txtCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCliente_Validating);
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(179, 70);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(451, 21);
            this.lblCliente.TabIndex = 79;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(35, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 16);
            this.label12.TabIndex = 80;
            this.label12.Text = "Sub-Cliente :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubCliente
            // 
            this.txtSubCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCliente.Decimales = 2;
            this.txtSubCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCliente.ForeColor = System.Drawing.Color.Black;
            this.txtSubCliente.Location = new System.Drawing.Point(110, 95);
            this.txtSubCliente.MaxLength = 4;
            this.txtSubCliente.Name = "txtSubCliente";
            this.txtSubCliente.PermitirApostrofo = false;
            this.txtSubCliente.PermitirNegativos = false;
            this.txtSubCliente.Size = new System.Drawing.Size(66, 20);
            this.txtSubCliente.TabIndex = 3;
            this.txtSubCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCliente_KeyDown);
            this.txtSubCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCliente_Validating);
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(179, 95);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(451, 21);
            this.lblSubCliente.TabIndex = 81;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(10, 123);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 13);
            this.label14.TabIndex = 83;
            this.label14.Text = "Clave Beneficiario :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(110, 119);
            this.txtBeneficiario.MaxLength = 8;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(152, 20);
            this.txtBeneficiario.TabIndex = 4;
            this.txtBeneficiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBeneficiario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeneficiario_KeyDown);
            this.txtBeneficiario.Validating += new System.ComponentModel.CancelEventHandler(this.txtBeneficiario_Validating);
            // 
            // lblBeneficiario
            // 
            this.lblBeneficiario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficiario.Location = new System.Drawing.Point(110, 143);
            this.lblBeneficiario.Name = "lblBeneficiario";
            this.lblBeneficiario.Size = new System.Drawing.Size(520, 21);
            this.lblBeneficiario.TabIndex = 84;
            this.lblBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.lblBeneficiario);
            this.FramePrincipal.Controls.Add(this.txtBeneficiario);
            this.FramePrincipal.Controls.Add(this.label14);
            this.FramePrincipal.Controls.Add(this.lblSubCliente);
            this.FramePrincipal.Controls.Add(this.txtSubCliente);
            this.FramePrincipal.Controls.Add(this.label12);
            this.FramePrincipal.Controls.Add(this.lblCliente);
            this.FramePrincipal.Controls.Add(this.txtCliente);
            this.FramePrincipal.Controls.Add(this.label13);
            this.FramePrincipal.Controls.Add(this.lblReferencia);
            this.FramePrincipal.Controls.Add(this.txtReferencia);
            this.FramePrincipal.Controls.Add(this.label9);
            this.FramePrincipal.Controls.Add(this.txtNumInterno);
            this.FramePrincipal.Controls.Add(this.label5);
            this.FramePrincipal.Controls.Add(this.txtNumExterno);
            this.FramePrincipal.Controls.Add(this.lblClaveEstado);
            this.FramePrincipal.Controls.Add(this.label7);
            this.FramePrincipal.Controls.Add(this.txtClaveEstado);
            this.FramePrincipal.Controls.Add(this.lblCancelado);
            this.FramePrincipal.Controls.Add(this.cboColonia);
            this.FramePrincipal.Controls.Add(this.label2);
            this.FramePrincipal.Controls.Add(this.lblClaveLocalidad);
            this.FramePrincipal.Controls.Add(this.label4);
            this.FramePrincipal.Controls.Add(this.txtClaveLocalidad);
            this.FramePrincipal.Controls.Add(this.lblPais);
            this.FramePrincipal.Controls.Add(this.label3);
            this.FramePrincipal.Controls.Add(this.txtPais);
            this.FramePrincipal.Controls.Add(this.label10);
            this.FramePrincipal.Controls.Add(this.txtDom);
            this.FramePrincipal.Controls.Add(this.lblClaveMun);
            this.FramePrincipal.Controls.Add(this.label6);
            this.FramePrincipal.Controls.Add(this.txtClaveMun);
            this.FramePrincipal.Controls.Add(this.label11);
            this.FramePrincipal.Controls.Add(this.txtCP);
            this.FramePrincipal.Controls.Add(this.lblNomFar);
            this.FramePrincipal.Controls.Add(this.label1);
            this.FramePrincipal.Controls.Add(this.txtIdFar);
            this.FramePrincipal.Controls.Add(this.cboEdo);
            this.FramePrincipal.Controls.Add(this.label8);
            this.FramePrincipal.Location = new System.Drawing.Point(12, 28);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(644, 399);
            this.FramePrincipal.TabIndex = 2;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos Generales Beneficiario";
            // 
            // FrmBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 433);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePrincipal);
            this.Name = "FrmBeneficiarios";
            this.Text = "Beneficiario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmBeneficiarios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private SC_ControlsCS.scTextBoxExt txtIdFar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNomFar;
        private SC_ControlsCS.scTextBoxExt txtCP;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtClaveMun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblClaveMun;
        private SC_ControlsCS.scTextBoxExt txtDom;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtPais;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPais;
        private SC_ControlsCS.scTextBoxExt txtClaveLocalidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblClaveLocalidad;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboColonia;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtClaveEstado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblClaveEstado;
        private SC_ControlsCS.scTextBoxExt txtNumExterno;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtNumInterno;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label lblReferencia;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scTextBoxExt txtSubCliente;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Label label14;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private System.Windows.Forms.Label lblBeneficiario;
        private System.Windows.Forms.GroupBox FramePrincipal;
    }
}