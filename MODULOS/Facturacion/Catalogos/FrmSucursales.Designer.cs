namespace Facturacion.Catalogos
{
    partial class FrmSucursales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSucursales));
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNumInterno = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumExterno = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveEstado = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtClaveEstado = new SC_ControlsCS.scTextBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.cboColonia = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClaveLocalidad = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClaveLocalidad = new SC_ControlsCS.scTextBoxExt();
            this.lblPais = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPais = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDom = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveMun = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClaveMun = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCP = new SC_ControlsCS.scTextBoxExt();
            this.lblNomFar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdFar = new SC_ControlsCS.scTextBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FramePrincipal.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePrincipal
            // 
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
            this.FramePrincipal.Size = new System.Drawing.Size(628, 306);
            this.FramePrincipal.TabIndex = 2;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos Generales Sucursales";
            // 
            // lblReferencia
            // 
            this.lblReferencia.Location = new System.Drawing.Point(17, 278);
            this.lblReferencia.Name = "lblReferencia";
            this.lblReferencia.Size = new System.Drawing.Size(71, 13);
            this.lblReferencia.TabIndex = 75;
            this.lblReferencia.Text = "Referencia :";
            this.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferencia
            // 
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(90, 274);
            this.txtReferencia.MaxLength = 100;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(523, 20);
            this.txtReferencia.TabIndex = 11;
            this.txtReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(371, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "No. Exterior :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumInterno
            // 
            this.txtNumInterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumInterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumInterno.Decimales = 2;
            this.txtNumInterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumInterno.ForeColor = System.Drawing.Color.Black;
            this.txtNumInterno.Location = new System.Drawing.Point(448, 248);
            this.txtNumInterno.MaxLength = 100;
            this.txtNumInterno.Name = "txtNumInterno";
            this.txtNumInterno.PermitirApostrofo = false;
            this.txtNumInterno.PermitirNegativos = false;
            this.txtNumInterno.Size = new System.Drawing.Size(165, 20);
            this.txtNumInterno.TabIndex = 10;
            this.txtNumInterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(17, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "No. Exterior :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumExterno
            // 
            this.txtNumExterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumExterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumExterno.Decimales = 2;
            this.txtNumExterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumExterno.ForeColor = System.Drawing.Color.Black;
            this.txtNumExterno.Location = new System.Drawing.Point(90, 248);
            this.txtNumExterno.MaxLength = 100;
            this.txtNumExterno.Name = "txtNumExterno";
            this.txtNumExterno.PermitirApostrofo = false;
            this.txtNumExterno.PermitirNegativos = false;
            this.txtNumExterno.Size = new System.Drawing.Size(165, 20);
            this.txtNumExterno.TabIndex = 9;
            this.txtNumExterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblClaveEstado
            // 
            this.lblClaveEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveEstado.Location = new System.Drawing.Point(159, 121);
            this.lblClaveEstado.Name = "lblClaveEstado";
            this.lblClaveEstado.Size = new System.Drawing.Size(451, 20);
            this.lblClaveEstado.TabIndex = 69;
            this.lblClaveEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Estado :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveEstado
            // 
            this.txtClaveEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveEstado.Decimales = 2;
            this.txtClaveEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveEstado.ForeColor = System.Drawing.Color.Black;
            this.txtClaveEstado.Location = new System.Drawing.Point(90, 121);
            this.txtClaveEstado.MaxLength = 4;
            this.txtClaveEstado.Name = "txtClaveEstado";
            this.txtClaveEstado.PermitirApostrofo = false;
            this.txtClaveEstado.PermitirNegativos = false;
            this.txtClaveEstado.Size = new System.Drawing.Size(66, 20);
            this.txtClaveEstado.TabIndex = 4;
            this.txtClaveEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCancelado
            // 
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(295, 70);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(157, 18);
            this.lblCancelado.TabIndex = 66;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // cboColonia
            // 
            this.cboColonia.BackColorEnabled = System.Drawing.Color.White;
            this.cboColonia.Data = "";
            this.cboColonia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColonia.Filtro = " 1 = 1";
            this.cboColonia.FormattingEnabled = true;
            this.cboColonia.ListaItemsBusqueda = 20;
            this.cboColonia.Location = new System.Drawing.Point(90, 196);
            this.cboColonia.MostrarToolTip = false;
            this.cboColonia.Name = "cboColonia";
            this.cboColonia.Size = new System.Drawing.Size(519, 21);
            this.cboColonia.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Colonia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveLocalidad
            // 
            this.lblClaveLocalidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveLocalidad.Location = new System.Drawing.Point(159, 171);
            this.lblClaveLocalidad.Name = "lblClaveLocalidad";
            this.lblClaveLocalidad.Size = new System.Drawing.Size(451, 20);
            this.lblClaveLocalidad.TabIndex = 63;
            this.lblClaveLocalidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Localidad :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveLocalidad
            // 
            this.txtClaveLocalidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveLocalidad.Decimales = 2;
            this.txtClaveLocalidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveLocalidad.ForeColor = System.Drawing.Color.Black;
            this.txtClaveLocalidad.Location = new System.Drawing.Point(90, 171);
            this.txtClaveLocalidad.MaxLength = 4;
            this.txtClaveLocalidad.Name = "txtClaveLocalidad";
            this.txtClaveLocalidad.PermitirApostrofo = false;
            this.txtClaveLocalidad.PermitirNegativos = false;
            this.txtClaveLocalidad.Size = new System.Drawing.Size(66, 20);
            this.txtClaveLocalidad.TabIndex = 6;
            this.txtClaveLocalidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPais
            // 
            this.lblPais.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPais.Location = new System.Drawing.Point(159, 94);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(451, 20);
            this.lblPais.TabIndex = 60;
            this.lblPais.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Pais :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPais
            // 
            this.txtPais.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPais.Decimales = 2;
            this.txtPais.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPais.ForeColor = System.Drawing.Color.Black;
            this.txtPais.Location = new System.Drawing.Point(90, 94);
            this.txtPais.MaxLength = 4;
            this.txtPais.Name = "txtPais";
            this.txtPais.PermitirApostrofo = false;
            this.txtPais.PermitirNegativos = false;
            this.txtPais.Size = new System.Drawing.Size(66, 20);
            this.txtPais.TabIndex = 3;
            this.txtPais.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(17, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Domicilio :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDom
            // 
            this.txtDom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDom.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDom.Decimales = 2;
            this.txtDom.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDom.ForeColor = System.Drawing.Color.Black;
            this.txtDom.Location = new System.Drawing.Point(90, 222);
            this.txtDom.MaxLength = 100;
            this.txtDom.Name = "txtDom";
            this.txtDom.PermitirApostrofo = false;
            this.txtDom.PermitirNegativos = false;
            this.txtDom.Size = new System.Drawing.Size(523, 20);
            this.txtDom.TabIndex = 8;
            this.txtDom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblClaveMun
            // 
            this.lblClaveMun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveMun.Location = new System.Drawing.Point(159, 146);
            this.lblClaveMun.Name = "lblClaveMun";
            this.lblClaveMun.Size = new System.Drawing.Size(451, 20);
            this.lblClaveMun.TabIndex = 54;
            this.lblClaveMun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(17, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Municipio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtClaveMun
            // 
            this.txtClaveMun.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveMun.Decimales = 2;
            this.txtClaveMun.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveMun.ForeColor = System.Drawing.Color.Black;
            this.txtClaveMun.Location = new System.Drawing.Point(90, 146);
            this.txtClaveMun.MaxLength = 4;
            this.txtClaveMun.Name = "txtClaveMun";
            this.txtClaveMun.PermitirApostrofo = false;
            this.txtClaveMun.PermitirNegativos = false;
            this.txtClaveMun.Size = new System.Drawing.Size(66, 20);
            this.txtClaveMun.TabIndex = 5;
            this.txtClaveMun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Código postal :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCP
            // 
            this.txtCP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCP.Decimales = 2;
            this.txtCP.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCP.ForeColor = System.Drawing.Color.Black;
            this.txtCP.Location = new System.Drawing.Point(90, 70);
            this.txtCP.MaxLength = 10;
            this.txtCP.Name = "txtCP";
            this.txtCP.PermitirApostrofo = false;
            this.txtCP.PermitirNegativos = false;
            this.txtCP.Size = new System.Drawing.Size(154, 20);
            this.txtCP.TabIndex = 2;
            this.txtCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCP.Validating += new System.ComponentModel.CancelEventHandler(this.txtCP_Validating);
            // 
            // lblNomFar
            // 
            this.lblNomFar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomFar.Location = new System.Drawing.Point(159, 45);
            this.lblNomFar.Name = "lblNomFar";
            this.lblNomFar.Size = new System.Drawing.Size(451, 20);
            this.lblNomFar.TabIndex = 47;
            this.lblNomFar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdFar
            // 
            this.txtIdFar.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFar.Decimales = 2;
            this.txtIdFar.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFar.ForeColor = System.Drawing.Color.Black;
            this.txtIdFar.Location = new System.Drawing.Point(90, 45);
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
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(90, 21);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(519, 21);
            this.cboEdo.TabIndex = 0;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(652, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmSucursales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 346);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePrincipal);
            this.Name = "FrmSucursales";
            this.Text = "Sucursales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSucursales_Load);
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox FramePrincipal;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtIdFar;
        private System.Windows.Forms.Label lblNomFar;
        private System.Windows.Forms.Label label11;
        private SC_ControlsCS.scTextBoxExt txtCP;
        private SC_ControlsCS.scComboBoxExt cboColonia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClaveLocalidad;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtClaveLocalidad;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtPais;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtDom;
        private System.Windows.Forms.Label lblClaveMun;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtClaveMun;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.Label lblClaveEstado;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtClaveEstado;
        private System.Windows.Forms.Label lblReferencia;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtNumInterno;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtNumExterno;
    }
}