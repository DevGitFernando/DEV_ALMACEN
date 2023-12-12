namespace Dll_IATP2.Interface
{
    partial class FrmOrdenesDeAcondicionamiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdenesDeAcondicionamiento));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarArchivoDeSalida = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeerCodigo = new System.Windows.Forms.ToolStripButton();
            this.FrameInformacionGeneral = new System.Windows.Forms.GroupBox();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumeroDeDocumento = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.toolStripEmails = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.listView_Beneficiarios = new System.Windows.Forms.ListView();
            this.colIdPaciente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombrePaciente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCama = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreMedico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacionGeneral.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.toolStripEmails.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2,
            this.btnGenerarArchivoDeSalida,
            this.toolStripSeparator1,
            this.btnLeerCodigo});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1289, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarArchivoDeSalida
            // 
            this.btnGenerarArchivoDeSalida.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarArchivoDeSalida.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarArchivoDeSalida.Image")));
            this.btnGenerarArchivoDeSalida.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarArchivoDeSalida.Name = "btnGenerarArchivoDeSalida";
            this.btnGenerarArchivoDeSalida.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarArchivoDeSalida.Text = "Generar archivo de salida";
            this.btnGenerarArchivoDeSalida.Click += new System.EventHandler(this.btnGenerarArchivoDeSalida_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLeerCodigo
            // 
            this.btnLeerCodigo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerCodigo.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerCodigo.Image")));
            this.btnLeerCodigo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerCodigo.Name = "btnLeerCodigo";
            this.btnLeerCodigo.Size = new System.Drawing.Size(23, 22);
            this.btnLeerCodigo.Text = "Lector datamatrix";
            this.btnLeerCodigo.Click += new System.EventHandler(this.btnLeerCodigo_Click);
            // 
            // FrameInformacionGeneral
            // 
            this.FrameInformacionGeneral.Controls.Add(this.lblCancelado);
            this.FrameInformacionGeneral.Controls.Add(this.lblSubPro);
            this.FrameInformacionGeneral.Controls.Add(this.txtSubPro);
            this.FrameInformacionGeneral.Controls.Add(this.label7);
            this.FrameInformacionGeneral.Controls.Add(this.lblPro);
            this.FrameInformacionGeneral.Controls.Add(this.txtPro);
            this.FrameInformacionGeneral.Controls.Add(this.label9);
            this.FrameInformacionGeneral.Controls.Add(this.lblSubCte);
            this.FrameInformacionGeneral.Controls.Add(this.txtSubCte);
            this.FrameInformacionGeneral.Controls.Add(this.label5);
            this.FrameInformacionGeneral.Controls.Add(this.lblCte);
            this.FrameInformacionGeneral.Controls.Add(this.txtCte);
            this.FrameInformacionGeneral.Controls.Add(this.label4);
            this.FrameInformacionGeneral.Controls.Add(this.txtNumeroDeDocumento);
            this.FrameInformacionGeneral.Controls.Add(this.label2);
            this.FrameInformacionGeneral.Controls.Add(this.txtObservaciones);
            this.FrameInformacionGeneral.Controls.Add(this.label10);
            this.FrameInformacionGeneral.Controls.Add(this.dtpFechaRegistro);
            this.FrameInformacionGeneral.Controls.Add(this.label3);
            this.FrameInformacionGeneral.Controls.Add(this.txtFolio);
            this.FrameInformacionGeneral.Controls.Add(this.label1);
            this.FrameInformacionGeneral.Location = new System.Drawing.Point(12, 35);
            this.FrameInformacionGeneral.Name = "FrameInformacionGeneral";
            this.FrameInformacionGeneral.Size = new System.Drawing.Size(1266, 189);
            this.FrameInformacionGeneral.TabIndex = 1;
            this.FrameInformacionGeneral.TabStop = false;
            this.FrameInformacionGeneral.Text = "Información general";
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(290, 25);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(131, 25);
            this.lblCancelado.TabIndex = 59;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(859, 142);
            this.lblSubPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(395, 26);
            this.lblSubPro.TabIndex = 58;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(772, 144);
            this.txtSubPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(77, 22);
            this.txtSubPro.TabIndex = 6;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(652, 145);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 57;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(859, 112);
            this.lblPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(395, 26);
            this.lblPro.TabIndex = 56;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(772, 114);
            this.txtPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(77, 22);
            this.txtPro.TabIndex = 5;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(652, 115);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 20);
            this.label9.TabIndex = 55;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(237, 142);
            this.lblSubCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(395, 26);
            this.lblSubCte.TabIndex = 54;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(150, 144);
            this.txtSubCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(77, 22);
            this.txtSubCte.TabIndex = 4;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 20);
            this.label5.TabIndex = 53;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(237, 112);
            this.lblCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(395, 26);
            this.lblCte.TabIndex = 52;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(150, 114);
            this.txtCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(77, 22);
            this.txtCte.TabIndex = 3;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 115);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 20);
            this.label4.TabIndex = 51;
            this.label4.Text = "Cliente :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumeroDeDocumento
            // 
            this.txtNumeroDeDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroDeDocumento.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumeroDeDocumento.Decimales = 2;
            this.txtNumeroDeDocumento.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumeroDeDocumento.ForeColor = System.Drawing.Color.Black;
            this.txtNumeroDeDocumento.Location = new System.Drawing.Point(150, 54);
            this.txtNumeroDeDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumeroDeDocumento.MaxLength = 50;
            this.txtNumeroDeDocumento.Multiline = true;
            this.txtNumeroDeDocumento.Name = "txtNumeroDeDocumento";
            this.txtNumeroDeDocumento.PermitirApostrofo = false;
            this.txtNumeroDeDocumento.PermitirNegativos = false;
            this.txtNumeroDeDocumento.Size = new System.Drawing.Size(240, 22);
            this.txtNumeroDeDocumento.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 41;
            this.label2.Text = "Núm. Documento :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(150, 83);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(1104, 22);
            this.txtObservaciones.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(18, 87);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 15);
            this.label10.TabIndex = 39;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1129, 26);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(121, 22);
            this.dtpFechaRegistro.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(988, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 23);
            this.label3.TabIndex = 37;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(150, 26);
            this.txtFolio.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(132, 22);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Controls.Add(this.toolStripEmails);
            this.FrameDetalles.Controls.Add(this.listView_Beneficiarios);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 230);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1266, 444);
            this.FrameDetalles.TabIndex = 2;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Listado de ordenes";
            // 
            // toolStripEmails
            // 
            this.toolStripEmails.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEmails.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripEmails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.toolStripSeparator3,
            this.btnEdit,
            this.btnView,
            this.toolStripSeparator4,
            this.btnDelete,
            this.toolStripSeparator5});
            this.toolStripEmails.Location = new System.Drawing.Point(3, 18);
            this.toolStripEmails.Name = "toolStripEmails";
            this.toolStripEmails.Size = new System.Drawing.Size(1260, 39);
            this.toolStripEmails.TabIndex = 0;
            this.toolStripEmails.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.Text = "Agregar";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.Text = "Modificar";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(36, 36);
            this.btnView.Text = "Visualizar";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // listView_Beneficiarios
            // 
            this.listView_Beneficiarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Beneficiarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdPaciente,
            this.colNombrePaciente,
            this.colCama,
            this.colNombreMedico});
            this.listView_Beneficiarios.Location = new System.Drawing.Point(15, 60);
            this.listView_Beneficiarios.Name = "listView_Beneficiarios";
            this.listView_Beneficiarios.Size = new System.Drawing.Size(1235, 372);
            this.listView_Beneficiarios.TabIndex = 1;
            this.listView_Beneficiarios.UseCompatibleStateImageBehavior = false;
            this.listView_Beneficiarios.View = System.Windows.Forms.View.Details;
            // 
            // colIdPaciente
            // 
            this.colIdPaciente.Text = "Id Paciente";
            this.colIdPaciente.Width = 120;
            // 
            // colNombrePaciente
            // 
            this.colNombrePaciente.Text = "Nombre paciente";
            this.colNombrePaciente.Width = 350;
            // 
            // colCama
            // 
            this.colCama.Text = "Número de cama";
            this.colCama.Width = 140;
            // 
            // colNombreMedico
            // 
            this.colNombreMedico.Text = "Nombre de quien preescribe";
            this.colNombreMedico.Width = 340;
            // 
            // FrmOrdenesDeAcondicionamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 685);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.FrameInformacionGeneral);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmOrdenesDeAcondicionamiento";
            this.Text = "Ordenes de acondicionamiento";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmOrdenesDeAcondicionamiento_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacionGeneral.ResumeLayout(false);
            this.FrameInformacionGeneral.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameDetalles.PerformLayout();
            this.toolStripEmails.ResumeLayout(false);
            this.toolStripEmails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGenerarArchivoDeSalida;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.GroupBox FrameInformacionGeneral;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView listView_Beneficiarios;
        private System.Windows.Forms.ToolStrip toolStripEmails;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ColumnHeader colNombrePaciente;
        private System.Windows.Forms.ColumnHeader colCama;
        private System.Windows.Forms.ColumnHeader colNombreMedico;
        private System.Windows.Forms.ColumnHeader colIdPaciente;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private SC_ControlsCS.scTextBoxExt txtNumeroDeDocumento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnLeerCodigo;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.ToolStripButton btnView;
    }
}