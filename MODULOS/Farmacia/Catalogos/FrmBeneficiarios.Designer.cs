namespace Farmacia.Catalogos
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
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosPersonales = new System.Windows.Forms.GroupBox();
            this.lblMensaje_TipoDeIdentificacion = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboTiposDeIdentificacion = new SC_ControlsCS.scComboBoxExt();
            this.btnIdentificacion = new System.Windows.Forms.Button();
            this.btnBusquedaCURP = new System.Windows.Forms.Button();
            this.btnGenerarCURP = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cboEstadosResidencia = new SC_ControlsCS.scComboBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.cboDerechoHabiencias = new SC_ControlsCS.scComboBoxExt();
            this.lbl_TipoDeCurp = new System.Windows.Forms.Label();
            this.txtCURP = new SC_ControlsCS.scTextBoxExt();
            this.label14 = new System.Windows.Forms.Label();
            this.lblJuris = new System.Windows.Forms.Label();
            this.txtIdJuris = new SC_ControlsCS.scTextBoxExt();
            this.label22 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboTipoDeBeneficiario = new SC_ControlsCS.scComboBoxExt();
            this.txtDomicilio = new SC_ControlsCS.scTextBoxExt();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSexo = new SC_ControlsCS.scComboBoxExt();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMaterno = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPaterno = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.txtSubCliente = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.FrameDatosGenerales = new System.Windows.Forms.GroupBox();
            this.dtpFechaInicioVigencia = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label11 = new System.Windows.Forms.Label();
            this.FrameReferenciaBeneficiario = new System.Windows.Forms.GroupBox();
            this.txtId_BeneficiarioReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatosPersonales.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.FrameDatosGenerales.SuspendLayout();
            this.FrameReferenciaBeneficiario.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(811, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 58);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(54, 55);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 58);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(54, 55);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrameDatosPersonales
            // 
            this.FrameDatosPersonales.Controls.Add(this.lblMensaje_TipoDeIdentificacion);
            this.FrameDatosPersonales.Controls.Add(this.label15);
            this.FrameDatosPersonales.Controls.Add(this.cboTiposDeIdentificacion);
            this.FrameDatosPersonales.Controls.Add(this.btnIdentificacion);
            this.FrameDatosPersonales.Controls.Add(this.btnBusquedaCURP);
            this.FrameDatosPersonales.Controls.Add(this.btnGenerarCURP);
            this.FrameDatosPersonales.Controls.Add(this.label17);
            this.FrameDatosPersonales.Controls.Add(this.cboEstadosResidencia);
            this.FrameDatosPersonales.Controls.Add(this.label16);
            this.FrameDatosPersonales.Controls.Add(this.cboDerechoHabiencias);
            this.FrameDatosPersonales.Controls.Add(this.lbl_TipoDeCurp);
            this.FrameDatosPersonales.Controls.Add(this.txtCURP);
            this.FrameDatosPersonales.Controls.Add(this.label14);
            this.FrameDatosPersonales.Controls.Add(this.lblJuris);
            this.FrameDatosPersonales.Controls.Add(this.txtIdJuris);
            this.FrameDatosPersonales.Controls.Add(this.label22);
            this.FrameDatosPersonales.Controls.Add(this.label13);
            this.FrameDatosPersonales.Controls.Add(this.cboTipoDeBeneficiario);
            this.FrameDatosPersonales.Controls.Add(this.txtDomicilio);
            this.FrameDatosPersonales.Controls.Add(this.lblDomicilio);
            this.FrameDatosPersonales.Controls.Add(this.label8);
            this.FrameDatosPersonales.Controls.Add(this.cboSexo);
            this.FrameDatosPersonales.Controls.Add(this.dtpFechaNacimiento);
            this.FrameDatosPersonales.Controls.Add(this.label7);
            this.FrameDatosPersonales.Controls.Add(this.txtMaterno);
            this.FrameDatosPersonales.Controls.Add(this.label4);
            this.FrameDatosPersonales.Controls.Add(this.txtPaterno);
            this.FrameDatosPersonales.Controls.Add(this.label6);
            this.FrameDatosPersonales.Controls.Add(this.lblCancelado);
            this.FrameDatosPersonales.Controls.Add(this.txtBeneficiario);
            this.FrameDatosPersonales.Controls.Add(this.txtNombre);
            this.FrameDatosPersonales.Controls.Add(this.label1);
            this.FrameDatosPersonales.Controls.Add(this.label2);
            this.FrameDatosPersonales.Location = new System.Drawing.Point(11, 156);
            this.FrameDatosPersonales.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatosPersonales.Name = "FrameDatosPersonales";
            this.FrameDatosPersonales.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatosPersonales.Size = new System.Drawing.Size(788, 444);
            this.FrameDatosPersonales.TabIndex = 2;
            this.FrameDatosPersonales.TabStop = false;
            this.FrameDatosPersonales.Text = "Información Beneficiario    ";
            // 
            // lblMensaje_TipoDeIdentificacion
            // 
            this.lblMensaje_TipoDeIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje_TipoDeIdentificacion.Location = new System.Drawing.Point(520, 244);
            this.lblMensaje_TipoDeIdentificacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensaje_TipoDeIdentificacion.Name = "lblMensaje_TipoDeIdentificacion";
            this.lblMensaje_TipoDeIdentificacion.Size = new System.Drawing.Size(249, 26);
            this.lblMensaje_TipoDeIdentificacion.TabIndex = 9;
            this.lblMensaje_TipoDeIdentificacion.Text = "CURP (18 carácteres) :";
            this.lblMensaje_TipoDeIdentificacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(13, 247);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(181, 16);
            this.label15.TabIndex = 62;
            this.label15.Text = "Tipo de identificación :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTiposDeIdentificacion
            // 
            this.cboTiposDeIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTiposDeIdentificacion.BackColorEnabled = System.Drawing.Color.White;
            this.cboTiposDeIdentificacion.Data = "";
            this.cboTiposDeIdentificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposDeIdentificacion.Filtro = " 1 = 1";
            this.cboTiposDeIdentificacion.FormattingEnabled = true;
            this.cboTiposDeIdentificacion.ListaItemsBusqueda = 20;
            this.cboTiposDeIdentificacion.Location = new System.Drawing.Point(197, 244);
            this.cboTiposDeIdentificacion.Margin = new System.Windows.Forms.Padding(4);
            this.cboTiposDeIdentificacion.MostrarToolTip = false;
            this.cboTiposDeIdentificacion.Name = "cboTiposDeIdentificacion";
            this.cboTiposDeIdentificacion.Size = new System.Drawing.Size(313, 24);
            this.cboTiposDeIdentificacion.TabIndex = 8;
            this.cboTiposDeIdentificacion.SelectedIndexChanged += new System.EventHandler(this.cboTiposDeIdentificacion_SelectedIndexChanged);
            // 
            // btnIdentificacion
            // 
            this.btnIdentificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIdentificacion.Image = global::Farmacia.Properties.Resources.camera_edit;
            this.btnIdentificacion.Location = new System.Drawing.Point(680, 54);
            this.btnIdentificacion.Margin = new System.Windows.Forms.Padding(4);
            this.btnIdentificacion.Name = "btnIdentificacion";
            this.btnIdentificacion.Size = new System.Drawing.Size(89, 54);
            this.btnIdentificacion.TabIndex = 60;
            this.btnIdentificacion.UseVisualStyleBackColor = true;
            this.btnIdentificacion.Click += new System.EventHandler(this.btnIdentificacion_Click);
            // 
            // btnBusquedaCURP
            // 
            this.btnBusquedaCURP.Location = new System.Drawing.Point(445, 277);
            this.btnBusquedaCURP.Margin = new System.Windows.Forms.Padding(4);
            this.btnBusquedaCURP.Name = "btnBusquedaCURP";
            this.btnBusquedaCURP.Size = new System.Drawing.Size(152, 28);
            this.btnBusquedaCURP.TabIndex = 11;
            this.btnBusquedaCURP.Text = "Curps Genericas";
            this.btnBusquedaCURP.UseVisualStyleBackColor = true;
            this.btnBusquedaCURP.Click += new System.EventHandler(this.btnBusquedaCURP_Click);
            // 
            // btnGenerarCURP
            // 
            this.btnGenerarCURP.Location = new System.Drawing.Point(13, 367);
            this.btnGenerarCURP.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerarCURP.Name = "btnGenerarCURP";
            this.btnGenerarCURP.Size = new System.Drawing.Size(181, 28);
            this.btnGenerarCURP.TabIndex = 58;
            this.btnGenerarCURP.Text = "Generar";
            this.btnGenerarCURP.UseVisualStyleBackColor = true;
            this.btnGenerarCURP.Visible = false;
            this.btnGenerarCURP.Click += new System.EventHandler(this.btnGenerarCURP_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(13, 214);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(181, 16);
            this.label17.TabIndex = 57;
            this.label17.Text = "Estado de residencia :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstadosResidencia
            // 
            this.cboEstadosResidencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstadosResidencia.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstadosResidencia.Data = "";
            this.cboEstadosResidencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadosResidencia.Filtro = " 1 = 1";
            this.cboEstadosResidencia.FormattingEnabled = true;
            this.cboEstadosResidencia.ListaItemsBusqueda = 20;
            this.cboEstadosResidencia.Location = new System.Drawing.Point(197, 210);
            this.cboEstadosResidencia.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstadosResidencia.MostrarToolTip = false;
            this.cboEstadosResidencia.Name = "cboEstadosResidencia";
            this.cboEstadosResidencia.Size = new System.Drawing.Size(313, 24);
            this.cboEstadosResidencia.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(13, 181);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(181, 16);
            this.label16.TabIndex = 55;
            this.label16.Text = "Derechohabiencia :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDerechoHabiencias
            // 
            this.cboDerechoHabiencias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDerechoHabiencias.BackColorEnabled = System.Drawing.Color.White;
            this.cboDerechoHabiencias.Data = "";
            this.cboDerechoHabiencias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDerechoHabiencias.Filtro = " 1 = 1";
            this.cboDerechoHabiencias.FormattingEnabled = true;
            this.cboDerechoHabiencias.ListaItemsBusqueda = 20;
            this.cboDerechoHabiencias.Location = new System.Drawing.Point(197, 177);
            this.cboDerechoHabiencias.Margin = new System.Windows.Forms.Padding(4);
            this.cboDerechoHabiencias.MostrarToolTip = false;
            this.cboDerechoHabiencias.Name = "cboDerechoHabiencias";
            this.cboDerechoHabiencias.Size = new System.Drawing.Size(313, 24);
            this.cboDerechoHabiencias.TabIndex = 6;
            // 
            // lbl_TipoDeCurp
            // 
            this.lbl_TipoDeCurp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_TipoDeCurp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TipoDeCurp.ForeColor = System.Drawing.Color.Red;
            this.lbl_TipoDeCurp.Location = new System.Drawing.Point(651, 282);
            this.lbl_TipoDeCurp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_TipoDeCurp.Name = "lbl_TipoDeCurp";
            this.lbl_TipoDeCurp.Size = new System.Drawing.Size(119, 18);
            this.lbl_TipoDeCurp.TabIndex = 12;
            this.lbl_TipoDeCurp.Text = "GENERICA";
            this.lbl_TipoDeCurp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCURP
            // 
            this.txtCURP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCURP.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCURP.Decimales = 2;
            this.txtCURP.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCURP.ForeColor = System.Drawing.Color.Black;
            this.txtCURP.Location = new System.Drawing.Point(197, 278);
            this.txtCURP.Margin = new System.Windows.Forms.Padding(4);
            this.txtCURP.MaxLength = 20;
            this.txtCURP.Name = "txtCURP";
            this.txtCURP.PermitirApostrofo = false;
            this.txtCURP.PermitirNegativos = false;
            this.txtCURP.Size = new System.Drawing.Size(237, 22);
            this.txtCURP.TabIndex = 10;
            this.txtCURP.Tag = "";
            this.txtCURP.Text = "012345678901234567";
            this.txtCURP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCURP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCURP_KeyDown);
            this.txtCURP.Validating += new System.ComponentModel.CancelEventHandler(this.txtCURP_Validating);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(13, 283);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(181, 16);
            this.label14.TabIndex = 52;
            this.label14.Text = "Número de identificación :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblJuris
            // 
            this.lblJuris.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblJuris.Location = new System.Drawing.Point(332, 406);
            this.lblJuris.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJuris.Name = "lblJuris";
            this.lblJuris.Size = new System.Drawing.Size(439, 25);
            this.lblJuris.TabIndex = 16;
            this.lblJuris.Text = "Nombre :";
            this.lblJuris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdJuris
            // 
            this.txtIdJuris.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdJuris.Decimales = 2;
            this.txtIdJuris.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdJuris.ForeColor = System.Drawing.Color.Black;
            this.txtIdJuris.Location = new System.Drawing.Point(197, 406);
            this.txtIdJuris.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdJuris.MaxLength = 3;
            this.txtIdJuris.Name = "txtIdJuris";
            this.txtIdJuris.PermitirApostrofo = false;
            this.txtIdJuris.PermitirNegativos = false;
            this.txtIdJuris.Size = new System.Drawing.Size(119, 22);
            this.txtIdJuris.TabIndex = 15;
            this.txtIdJuris.Text = "123456";
            this.txtIdJuris.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdJuris.TextChanged += new System.EventHandler(this.txtIdJuris_TextChanged);
            this.txtIdJuris.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdJuris_KeyDown);
            this.txtIdJuris.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdJuris_Validating);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(13, 407);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(181, 20);
            this.label22.TabIndex = 49;
            this.label22.Text = "Jurisdicción :";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(13, 314);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(181, 16);
            this.label13.TabIndex = 21;
            this.label13.Text = "Tipo de beneficiario :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoDeBeneficiario
            // 
            this.cboTipoDeBeneficiario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTipoDeBeneficiario.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoDeBeneficiario.Data = "";
            this.cboTipoDeBeneficiario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDeBeneficiario.Filtro = " 1 = 1";
            this.cboTipoDeBeneficiario.FormattingEnabled = true;
            this.cboTipoDeBeneficiario.ListaItemsBusqueda = 20;
            this.cboTipoDeBeneficiario.Location = new System.Drawing.Point(197, 310);
            this.cboTipoDeBeneficiario.Margin = new System.Windows.Forms.Padding(4);
            this.cboTipoDeBeneficiario.MostrarToolTip = false;
            this.cboTipoDeBeneficiario.Name = "cboTipoDeBeneficiario";
            this.cboTipoDeBeneficiario.Size = new System.Drawing.Size(313, 24);
            this.cboTipoDeBeneficiario.TabIndex = 13;
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDomicilio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDomicilio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDomicilio.Decimales = 2;
            this.txtDomicilio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDomicilio.ForeColor = System.Drawing.Color.Black;
            this.txtDomicilio.Location = new System.Drawing.Point(197, 342);
            this.txtDomicilio.Margin = new System.Windows.Forms.Padding(4);
            this.txtDomicilio.MaxLength = 200;
            this.txtDomicilio.Multiline = true;
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.PermitirApostrofo = false;
            this.txtDomicilio.PermitirNegativos = false;
            this.txtDomicilio.Size = new System.Drawing.Size(571, 56);
            this.txtDomicilio.TabIndex = 14;
            this.txtDomicilio.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.Location = new System.Drawing.Point(13, 347);
            this.lblDomicilio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(181, 16);
            this.lblDomicilio.TabIndex = 19;
            this.lblDomicilio.Text = "Domicilio :";
            this.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(528, 150);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Sexo :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSexo
            // 
            this.cboSexo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSexo.BackColorEnabled = System.Drawing.Color.White;
            this.cboSexo.Data = "";
            this.cboSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSexo.Filtro = " 1 = 1";
            this.cboSexo.FormattingEnabled = true;
            this.cboSexo.ListaItemsBusqueda = 20;
            this.cboSexo.Location = new System.Drawing.Point(592, 145);
            this.cboSexo.Margin = new System.Windows.Forms.Padding(4);
            this.cboSexo.MostrarToolTip = false;
            this.cboSexo.Name = "cboSexo";
            this.cboSexo.Size = new System.Drawing.Size(176, 24);
            this.cboSexo.TabIndex = 5;
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(197, 145);
            this.dtpFechaNacimiento.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaNacimiento.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(13, 150);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Fecha Nacimiento :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaterno
            // 
            this.txtMaterno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMaterno.Decimales = 2;
            this.txtMaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMaterno.ForeColor = System.Drawing.Color.Black;
            this.txtMaterno.Location = new System.Drawing.Point(197, 113);
            this.txtMaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaterno.MaxLength = 50;
            this.txtMaterno.Name = "txtMaterno";
            this.txtMaterno.PermitirApostrofo = false;
            this.txtMaterno.PermitirNegativos = false;
            this.txtMaterno.Size = new System.Drawing.Size(472, 22);
            this.txtMaterno.TabIndex = 3;
            this.txtMaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Apellido Materno :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPaterno
            // 
            this.txtPaterno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPaterno.Decimales = 2;
            this.txtPaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPaterno.ForeColor = System.Drawing.Color.Black;
            this.txtPaterno.Location = new System.Drawing.Point(197, 84);
            this.txtPaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaterno.MaxLength = 50;
            this.txtPaterno.Name = "txtPaterno";
            this.txtPaterno.PermitirApostrofo = false;
            this.txtPaterno.PermitirNegativos = false;
            this.txtPaterno.Size = new System.Drawing.Size(472, 22);
            this.txtPaterno.TabIndex = 2;
            this.txtPaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Apellido Paterno :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(659, 27);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(101, 17);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(197, 23);
            this.txtBeneficiario.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeneficiario.MaxLength = 8;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(123, 22);
            this.txtBeneficiario.TabIndex = 0;
            this.txtBeneficiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBeneficiario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeneficiario_KeyDown);
            this.txtBeneficiario.Validating += new System.ComponentModel.CancelEventHandler(this.txtBeneficiario_Validating);
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(197, 54);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(472, 22);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Id. Beneficiario :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.lblSubCliente);
            this.FrameEncabezado.Controls.Add(this.txtSubCliente);
            this.FrameEncabezado.Controls.Add(this.label5);
            this.FrameEncabezado.Controls.Add(this.lblCliente);
            this.FrameEncabezado.Controls.Add(this.txtCliente);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Location = new System.Drawing.Point(11, 61);
            this.FrameEncabezado.Margin = new System.Windows.Forms.Padding(4);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Padding = new System.Windows.Forms.Padding(4);
            this.FrameEncabezado.Size = new System.Drawing.Size(788, 94);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Cliente:";
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(305, 50);
            this.lblSubCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(467, 26);
            this.lblSubCliente.TabIndex = 46;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCliente
            // 
            this.txtSubCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCliente.Decimales = 2;
            this.txtSubCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCliente.ForeColor = System.Drawing.Color.Black;
            this.txtSubCliente.Location = new System.Drawing.Point(199, 50);
            this.txtSubCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCliente.MaxLength = 4;
            this.txtSubCliente.Name = "txtSubCliente";
            this.txtSubCliente.PermitirApostrofo = false;
            this.txtSubCliente.PermitirNegativos = false;
            this.txtSubCliente.Size = new System.Drawing.Size(97, 22);
            this.txtSubCliente.TabIndex = 1;
            this.txtSubCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCliente.TextChanged += new System.EventHandler(this.txtSubCliente_TextChanged);
            this.txtSubCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCliente_KeyDown);
            this.txtSubCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCliente_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 20);
            this.label5.TabIndex = 45;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(305, 20);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(467, 26);
            this.lblCliente.TabIndex = 44;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(199, 20);
            this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
            this.txtCliente.MaxLength = 4;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(97, 22);
            this.txtCliente.TabIndex = 0;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCliente_KeyDown);
            this.txtCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCliente_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 20);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDatosGenerales
            // 
            this.FrameDatosGenerales.Controls.Add(this.dtpFechaInicioVigencia);
            this.FrameDatosGenerales.Controls.Add(this.label9);
            this.FrameDatosGenerales.Controls.Add(this.dtpFechaVigencia);
            this.FrameDatosGenerales.Controls.Add(this.label10);
            this.FrameDatosGenerales.Controls.Add(this.txtReferencia);
            this.FrameDatosGenerales.Controls.Add(this.label11);
            this.FrameDatosGenerales.Location = new System.Drawing.Point(11, 604);
            this.FrameDatosGenerales.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatosGenerales.Name = "FrameDatosGenerales";
            this.FrameDatosGenerales.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatosGenerales.Size = new System.Drawing.Size(788, 97);
            this.FrameDatosGenerales.TabIndex = 3;
            this.FrameDatosGenerales.TabStop = false;
            this.FrameDatosGenerales.Text = "Información General Beneficiario     ";
            // 
            // dtpFechaInicioVigencia
            // 
            this.dtpFechaInicioVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicioVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicioVigencia.Location = new System.Drawing.Point(197, 59);
            this.dtpFechaInicioVigencia.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicioVigencia.Name = "dtpFechaInicioVigencia";
            this.dtpFechaInicioVigencia.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaInicioVigencia.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 62);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "F. Inicio Vigencia :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(651, 59);
            this.dtpFechaVigencia.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaVigencia.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.Location = new System.Drawing.Point(533, 62);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "F. Fin Vigencia :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReferencia
            // 
            this.txtReferencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(197, 26);
            this.txtReferencia.Margin = new System.Windows.Forms.Padding(4);
            this.txtReferencia.MaxLength = 20;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(575, 22);
            this.txtReferencia.TabIndex = 0;
            this.txtReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(181, 25);
            this.label11.TabIndex = 12;
            this.label11.Text = "Num.Póliza / Referencia :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameReferenciaBeneficiario
            // 
            this.FrameReferenciaBeneficiario.Controls.Add(this.txtId_BeneficiarioReferencia);
            this.FrameReferenciaBeneficiario.Controls.Add(this.label12);
            this.FrameReferenciaBeneficiario.Location = new System.Drawing.Point(11, 702);
            this.FrameReferenciaBeneficiario.Margin = new System.Windows.Forms.Padding(4);
            this.FrameReferenciaBeneficiario.Name = "FrameReferenciaBeneficiario";
            this.FrameReferenciaBeneficiario.Padding = new System.Windows.Forms.Padding(4);
            this.FrameReferenciaBeneficiario.Size = new System.Drawing.Size(788, 66);
            this.FrameReferenciaBeneficiario.TabIndex = 4;
            this.FrameReferenciaBeneficiario.TabStop = false;
            this.FrameReferenciaBeneficiario.Text = "Información adicional Beneficiario";
            // 
            // txtId_BeneficiarioReferencia
            // 
            this.txtId_BeneficiarioReferencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtId_BeneficiarioReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId_BeneficiarioReferencia.Decimales = 2;
            this.txtId_BeneficiarioReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId_BeneficiarioReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtId_BeneficiarioReferencia.Location = new System.Drawing.Point(197, 26);
            this.txtId_BeneficiarioReferencia.Margin = new System.Windows.Forms.Padding(4);
            this.txtId_BeneficiarioReferencia.MaxLength = 20;
            this.txtId_BeneficiarioReferencia.Name = "txtId_BeneficiarioReferencia";
            this.txtId_BeneficiarioReferencia.PermitirApostrofo = false;
            this.txtId_BeneficiarioReferencia.PermitirNegativos = false;
            this.txtId_BeneficiarioReferencia.Size = new System.Drawing.Size(575, 22);
            this.txtId_BeneficiarioReferencia.TabIndex = 0;
            this.txtId_BeneficiarioReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId_BeneficiarioReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_BeneficiarioReferencia_KeyDown);
            this.txtId_BeneficiarioReferencia.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_BeneficiarioReferencia_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(13, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(181, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "Referencia auxiliar :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmBeneficiarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 784);
            this.Controls.Add(this.FrameReferenciaBeneficiario);
            this.Controls.Add(this.FrameDatosGenerales);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.FrameDatosPersonales);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmBeneficiarios";
            this.ShowIcon = false;
            this.Text = "Beneficiarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmBeneficiarios_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatosPersonales.ResumeLayout(false);
            this.FrameDatosPersonales.PerformLayout();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.FrameDatosGenerales.ResumeLayout(false);
            this.FrameDatosGenerales.PerformLayout();
            this.FrameReferenciaBeneficiario.ResumeLayout(false);
            this.FrameReferenciaBeneficiario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox FrameDatosPersonales;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.Label lblSubCliente;
        private SC_ControlsCS.scTextBoxExt txtSubCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCliente;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtMaterno;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtPaterno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scComboBoxExt cboSexo;
        private System.Windows.Forms.GroupBox FrameDatosGenerales;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFechaInicioVigencia;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtDomicilio;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.GroupBox FrameReferenciaBeneficiario;
        private SC_ControlsCS.scTextBoxExt txtId_BeneficiarioReferencia;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scComboBoxExt cboTipoDeBeneficiario;
        private System.Windows.Forms.Label lblJuris;
        private SC_ControlsCS.scTextBoxExt txtIdJuris;
        private System.Windows.Forms.Label label22;
        private SC_ControlsCS.scTextBoxExt txtCURP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_TipoDeCurp;
        private System.Windows.Forms.Label label17;
        private SC_ControlsCS.scComboBoxExt cboEstadosResidencia;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scComboBoxExt cboDerechoHabiencias;
        private System.Windows.Forms.Button btnGenerarCURP;
        private System.Windows.Forms.Button btnBusquedaCURP;
        private System.Windows.Forms.Button btnIdentificacion;
        private System.Windows.Forms.Label label15;
        private SC_ControlsCS.scComboBoxExt cboTiposDeIdentificacion;
        private System.Windows.Forms.Label lblMensaje_TipoDeIdentificacion;
    }
}