namespace Dll_MA_IFacturacion.Catalogos
{
    partial class FrmClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientes));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.FrameDatosCliente = new System.Windows.Forms.GroupBox();
            this.txtNombreComercial = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRFC = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolStripTelefonos = new System.Windows.Forms.ToolStrip();
            this.btnTel_Add = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTel_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTel_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.listvTelefonos = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStripEmails = new System.Windows.Forms.ToolStrip();
            this.btnEmail_Add = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEmail_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEmail_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.listvEmails = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtD_NoInterior = new SC_ControlsCS.scTextBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtD_NoExterior = new SC_ControlsCS.scTextBoxExt();
            this.label17 = new System.Windows.Forms.Label();
            this.txtD_Pais = new SC_ControlsCS.scTextBoxExt();
            this.label31 = new System.Windows.Forms.Label();
            this.btnColonias = new System.Windows.Forms.Button();
            this.btnMunicipios = new System.Windows.Forms.Button();
            this.btnEstados = new System.Windows.Forms.Button();
            this.lblColonia = new SC_ControlsCS.scLabelExt();
            this.txtIdColonia = new SC_ControlsCS.scTextBoxExt();
            this.lblMunicipio = new SC_ControlsCS.scLabelExt();
            this.txtIdMunicipio = new SC_ControlsCS.scTextBoxExt();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.txtCodigoPostal = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCalle = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosCliente.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStripTelefonos.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStripEmails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Clave Cliente :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(117, 52);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(417, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(117, 28);
            this.txtId.MaxLength = 4;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(125, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.Enter += new System.EventHandler(this.txtId_Enter);
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(434, 28);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(100, 13);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCancelado.Visible = false;
            // 
            // FrameDatosCliente
            // 
            this.FrameDatosCliente.Controls.Add(this.txtNombreComercial);
            this.FrameDatosCliente.Controls.Add(this.label9);
            this.FrameDatosCliente.Controls.Add(this.txtRFC);
            this.FrameDatosCliente.Controls.Add(this.label3);
            this.FrameDatosCliente.Controls.Add(this.lblCancelado);
            this.FrameDatosCliente.Controls.Add(this.txtId);
            this.FrameDatosCliente.Controls.Add(this.txtNombre);
            this.FrameDatosCliente.Controls.Add(this.label1);
            this.FrameDatosCliente.Controls.Add(this.label2);
            this.FrameDatosCliente.Location = new System.Drawing.Point(10, 27);
            this.FrameDatosCliente.Name = "FrameDatosCliente";
            this.FrameDatosCliente.Size = new System.Drawing.Size(546, 165);
            this.FrameDatosCliente.TabIndex = 1;
            this.FrameDatosCliente.TabStop = false;
            this.FrameDatosCliente.Text = "Datos Cliente:";
            // 
            // txtNombreComercial
            // 
            this.txtNombreComercial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreComercial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombreComercial.Decimales = 2;
            this.txtNombreComercial.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombreComercial.ForeColor = System.Drawing.Color.Black;
            this.txtNombreComercial.Location = new System.Drawing.Point(117, 76);
            this.txtNombreComercial.MaxLength = 100;
            this.txtNombreComercial.Name = "txtNombreComercial";
            this.txtNombreComercial.PermitirApostrofo = false;
            this.txtNombreComercial.PermitirNegativos = false;
            this.txtNombreComercial.Size = new System.Drawing.Size(417, 20);
            this.txtNombreComercial.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Nombre comercial :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC.Decimales = 2;
            this.txtRFC.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC.ForeColor = System.Drawing.Color.Black;
            this.txtRFC.Location = new System.Drawing.Point(117, 100);
            this.txtRFC.MaxLength = 15;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.PermitirApostrofo = false;
            this.txtRFC.PermitirNegativos = false;
            this.txtRFC.Size = new System.Drawing.Size(125, 20);
            this.txtRFC.TabIndex = 3;
            this.txtRFC.Text = "123456789012345";
            this.txtRFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "RFC :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.toolStripTelefonos);
            this.groupBox5.Controls.Add(this.listvTelefonos);
            this.groupBox5.Location = new System.Drawing.Point(562, 195);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(449, 178);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Teléfonos";
            // 
            // toolStripTelefonos
            // 
            this.toolStripTelefonos.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTelefonos.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripTelefonos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTel_Add,
            this.toolStripSeparator6,
            this.btnTel_Edit,
            this.toolStripSeparator7,
            this.btnTel_Delete,
            this.toolStripSeparator8});
            this.toolStripTelefonos.Location = new System.Drawing.Point(3, 16);
            this.toolStripTelefonos.Name = "toolStripTelefonos";
            this.toolStripTelefonos.Size = new System.Drawing.Size(443, 39);
            this.toolStripTelefonos.TabIndex = 0;
            this.toolStripTelefonos.Text = "toolStrip2";
            // 
            // btnTel_Add
            // 
            this.btnTel_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTel_Add.Image = ((System.Drawing.Image)(resources.GetObject("btnTel_Add.Image")));
            this.btnTel_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTel_Add.Name = "btnTel_Add";
            this.btnTel_Add.Size = new System.Drawing.Size(36, 36);
            this.btnTel_Add.Text = "Agregar";
            this.btnTel_Add.Click += new System.EventHandler(this.btnTel_Add_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // btnTel_Edit
            // 
            this.btnTel_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTel_Edit.Image = ((System.Drawing.Image)(resources.GetObject("btnTel_Edit.Image")));
            this.btnTel_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTel_Edit.Name = "btnTel_Edit";
            this.btnTel_Edit.Size = new System.Drawing.Size(36, 36);
            this.btnTel_Edit.Text = "Modificar";
            this.btnTel_Edit.Click += new System.EventHandler(this.btnTel_Edit_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
            // 
            // btnTel_Delete
            // 
            this.btnTel_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTel_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btnTel_Delete.Image")));
            this.btnTel_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTel_Delete.Name = "btnTel_Delete";
            this.btnTel_Delete.Size = new System.Drawing.Size(36, 36);
            this.btnTel_Delete.Text = "Eliminar";
            this.btnTel_Delete.Click += new System.EventHandler(this.btnTel_Delete_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 39);
            // 
            // listvTelefonos
            // 
            this.listvTelefonos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvTelefonos.Location = new System.Drawing.Point(10, 58);
            this.listvTelefonos.Name = "listvTelefonos";
            this.listvTelefonos.Size = new System.Drawing.Size(427, 109);
            this.listvTelefonos.TabIndex = 1;
            this.listvTelefonos.UseCompatibleStateImageBehavior = false;
            this.listvTelefonos.View = System.Windows.Forms.View.Details;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.toolStripEmails);
            this.groupBox4.Controls.Add(this.listvEmails);
            this.groupBox4.Location = new System.Drawing.Point(562, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 165);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Correos Electrónicos";
            // 
            // toolStripEmails
            // 
            this.toolStripEmails.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEmails.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripEmails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEmail_Add,
            this.toolStripSeparator3,
            this.btnEmail_Edit,
            this.toolStripSeparator4,
            this.btnEmail_Delete,
            this.toolStripSeparator5});
            this.toolStripEmails.Location = new System.Drawing.Point(3, 16);
            this.toolStripEmails.Name = "toolStripEmails";
            this.toolStripEmails.Size = new System.Drawing.Size(443, 39);
            this.toolStripEmails.TabIndex = 0;
            this.toolStripEmails.Text = "toolStrip1";
            // 
            // btnEmail_Add
            // 
            this.btnEmail_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEmail_Add.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail_Add.Image")));
            this.btnEmail_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail_Add.Name = "btnEmail_Add";
            this.btnEmail_Add.Size = new System.Drawing.Size(36, 36);
            this.btnEmail_Add.Text = "Agregar";
            this.btnEmail_Add.Click += new System.EventHandler(this.btnEmail_Add_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // btnEmail_Edit
            // 
            this.btnEmail_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEmail_Edit.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail_Edit.Image")));
            this.btnEmail_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail_Edit.Name = "btnEmail_Edit";
            this.btnEmail_Edit.Size = new System.Drawing.Size(36, 36);
            this.btnEmail_Edit.Text = "Modificar";
            this.btnEmail_Edit.Click += new System.EventHandler(this.btnEmail_Edit_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnEmail_Delete
            // 
            this.btnEmail_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEmail_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btnEmail_Delete.Image")));
            this.btnEmail_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail_Delete.Name = "btnEmail_Delete";
            this.btnEmail_Delete.Size = new System.Drawing.Size(36, 36);
            this.btnEmail_Delete.Text = "Eliminar";
            this.btnEmail_Delete.Click += new System.EventHandler(this.btnEmail_Delete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // listvEmails
            // 
            this.listvEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listvEmails.Location = new System.Drawing.Point(10, 58);
            this.listvEmails.Name = "listvEmails";
            this.listvEmails.Size = new System.Drawing.Size(427, 96);
            this.listvEmails.TabIndex = 1;
            this.listvEmails.UseCompatibleStateImageBehavior = false;
            this.listvEmails.View = System.Windows.Forms.View.Details;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtD_NoInterior);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtD_NoExterior);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtD_Pais);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.btnColonias);
            this.groupBox2.Controls.Add(this.btnMunicipios);
            this.groupBox2.Controls.Add(this.btnEstados);
            this.groupBox2.Controls.Add(this.lblColonia);
            this.groupBox2.Controls.Add(this.txtIdColonia);
            this.groupBox2.Controls.Add(this.lblMunicipio);
            this.groupBox2.Controls.Add(this.txtIdMunicipio);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.txtIdEstado);
            this.groupBox2.Controls.Add(this.txtCodigoPostal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCalle);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(10, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(546, 178);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dirección";
            // 
            // txtD_NoInterior
            // 
            this.txtD_NoInterior.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_NoInterior.Decimales = 2;
            this.txtD_NoInterior.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_NoInterior.ForeColor = System.Drawing.Color.Black;
            this.txtD_NoInterior.Location = new System.Drawing.Point(275, 147);
            this.txtD_NoInterior.MaxLength = 20;
            this.txtD_NoInterior.Name = "txtD_NoInterior";
            this.txtD_NoInterior.PermitirApostrofo = false;
            this.txtD_NoInterior.PermitirNegativos = false;
            this.txtD_NoInterior.Size = new System.Drawing.Size(79, 20);
            this.txtD_NoInterior.TabIndex = 9;
            this.txtD_NoInterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(192, 150);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 14);
            this.label16.TabIndex = 75;
            this.label16.Text = "No. Interior  :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_NoExterior
            // 
            this.txtD_NoExterior.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_NoExterior.Decimales = 2;
            this.txtD_NoExterior.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_NoExterior.ForeColor = System.Drawing.Color.Black;
            this.txtD_NoExterior.Location = new System.Drawing.Point(102, 147);
            this.txtD_NoExterior.MaxLength = 20;
            this.txtD_NoExterior.Name = "txtD_NoExterior";
            this.txtD_NoExterior.PermitirApostrofo = false;
            this.txtD_NoExterior.PermitirNegativos = false;
            this.txtD_NoExterior.Size = new System.Drawing.Size(79, 20);
            this.txtD_NoExterior.TabIndex = 8;
            this.txtD_NoExterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 150);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 14);
            this.label17.TabIndex = 74;
            this.label17.Text = "* No. Exterior  :";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtD_Pais
            // 
            this.txtD_Pais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtD_Pais.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtD_Pais.Decimales = 2;
            this.txtD_Pais.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtD_Pais.ForeColor = System.Drawing.Color.Black;
            this.txtD_Pais.Location = new System.Drawing.Point(102, 19);
            this.txtD_Pais.MaxLength = 100;
            this.txtD_Pais.Name = "txtD_Pais";
            this.txtD_Pais.PermitirApostrofo = false;
            this.txtD_Pais.PermitirNegativos = false;
            this.txtD_Pais.Size = new System.Drawing.Size(432, 20);
            this.txtD_Pais.TabIndex = 0;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(8, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(91, 13);
            this.label31.TabIndex = 71;
            this.label31.Text = "* País  :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnColonias
            // 
            this.btnColonias.Location = new System.Drawing.Point(508, 95);
            this.btnColonias.Name = "btnColonias";
            this.btnColonias.Size = new System.Drawing.Size(26, 22);
            this.btnColonias.TabIndex = 6;
            this.btnColonias.Text = "...";
            this.btnColonias.UseVisualStyleBackColor = true;
            this.btnColonias.Click += new System.EventHandler(this.btnColonias_Click);
            // 
            // btnMunicipios
            // 
            this.btnMunicipios.Location = new System.Drawing.Point(508, 68);
            this.btnMunicipios.Name = "btnMunicipios";
            this.btnMunicipios.Size = new System.Drawing.Size(26, 22);
            this.btnMunicipios.TabIndex = 5;
            this.btnMunicipios.Text = "...";
            this.btnMunicipios.UseVisualStyleBackColor = true;
            this.btnMunicipios.Click += new System.EventHandler(this.btnMunicipios_Click);
            // 
            // btnEstados
            // 
            this.btnEstados.Location = new System.Drawing.Point(508, 42);
            this.btnEstados.Name = "btnEstados";
            this.btnEstados.Size = new System.Drawing.Size(26, 22);
            this.btnEstados.TabIndex = 4;
            this.btnEstados.Text = "...";
            this.btnEstados.UseVisualStyleBackColor = true;
            this.btnEstados.Click += new System.EventHandler(this.btnEstados_Click);
            // 
            // lblColonia
            // 
            this.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColonia.Location = new System.Drawing.Point(155, 95);
            this.lblColonia.MostrarToolTip = false;
            this.lblColonia.Name = "lblColonia";
            this.lblColonia.Size = new System.Drawing.Size(349, 21);
            this.lblColonia.TabIndex = 69;
            this.lblColonia.Text = "scLabelExt3";
            this.lblColonia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdColonia
            // 
            this.txtIdColonia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdColonia.Decimales = 2;
            this.txtIdColonia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdColonia.ForeColor = System.Drawing.Color.Black;
            this.txtIdColonia.Location = new System.Drawing.Point(102, 95);
            this.txtIdColonia.Name = "txtIdColonia";
            this.txtIdColonia.PermitirApostrofo = false;
            this.txtIdColonia.PermitirNegativos = false;
            this.txtIdColonia.Size = new System.Drawing.Size(47, 20);
            this.txtIdColonia.TabIndex = 3;
            this.txtIdColonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdColonia.TextChanged += new System.EventHandler(this.txtIdColonia_TextChanged);
            this.txtIdColonia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdColonia_KeyDown);
            this.txtIdColonia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdColonia_Validating);
            // 
            // lblMunicipio
            // 
            this.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMunicipio.Location = new System.Drawing.Point(155, 69);
            this.lblMunicipio.MostrarToolTip = false;
            this.lblMunicipio.Name = "lblMunicipio";
            this.lblMunicipio.Size = new System.Drawing.Size(349, 21);
            this.lblMunicipio.TabIndex = 68;
            this.lblMunicipio.Text = "scLabelExt2";
            this.lblMunicipio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdMunicipio
            // 
            this.txtIdMunicipio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdMunicipio.Decimales = 2;
            this.txtIdMunicipio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdMunicipio.ForeColor = System.Drawing.Color.Black;
            this.txtIdMunicipio.Location = new System.Drawing.Point(102, 69);
            this.txtIdMunicipio.Name = "txtIdMunicipio";
            this.txtIdMunicipio.PermitirApostrofo = false;
            this.txtIdMunicipio.PermitirNegativos = false;
            this.txtIdMunicipio.Size = new System.Drawing.Size(47, 20);
            this.txtIdMunicipio.TabIndex = 2;
            this.txtIdMunicipio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdMunicipio.TextChanged += new System.EventHandler(this.txtIdMunicipio_TextChanged);
            this.txtIdMunicipio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdMunicipio_KeyDown);
            this.txtIdMunicipio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdMunicipio_Validating);
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(155, 43);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(349, 21);
            this.lblEstado.TabIndex = 67;
            this.lblEstado.Text = "scLabelExt1";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(102, 43);
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(47, 20);
            this.txtIdEstado.TabIndex = 1;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstado.TextChanged += new System.EventHandler(this.txtIdEstado_TextChanged);
            this.txtIdEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstado_KeyDown);
            this.txtIdEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstado_Validating);
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoPostal.Decimales = 2;
            this.txtCodigoPostal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoPostal.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoPostal.Location = new System.Drawing.Point(455, 147);
            this.txtCodigoPostal.MaxLength = 10;
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.PermitirApostrofo = false;
            this.txtCodigoPostal.PermitirNegativos = false;
            this.txtCodigoPostal.Size = new System.Drawing.Size(79, 20);
            this.txtCodigoPostal.TabIndex = 10;
            this.txtCodigoPostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(361, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "Código Postal :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCalle
            // 
            this.txtCalle.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCalle.Decimales = 2;
            this.txtCalle.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCalle.ForeColor = System.Drawing.Color.Black;
            this.txtCalle.Location = new System.Drawing.Point(102, 120);
            this.txtCalle.MaxLength = 100;
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.PermitirApostrofo = false;
            this.txtCalle.PermitirNegativos = false;
            this.txtCalle.Size = new System.Drawing.Size(432, 20);
            this.txtCalle.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 65;
            this.label7.Text = "* Calle :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "* Colonia :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "* Municipio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "* Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1019, 25);
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
            this.btnGuardar.Text = "&Guardar";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 381);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameDatosCliente);
            this.Name = "FrmClientes";
            this.Text = "Clientes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmClientes_Load);
            this.FrameDatosCliente.ResumeLayout(false);
            this.FrameDatosCliente.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStripTelefonos.ResumeLayout(false);
            this.toolStripTelefonos.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStripEmails.ResumeLayout(false);
            this.toolStripEmails.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // SC_ControlsCS.scTextBoxExt 
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.GroupBox FrameDatosCliente;
        private SC_ControlsCS.scTextBoxExt txtRFC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStrip toolStripTelefonos;
        private System.Windows.Forms.ToolStripButton btnTel_Add;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnTel_Edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnTel_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ListView listvTelefonos;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStripEmails;
        private System.Windows.Forms.ToolStripButton btnEmail_Add;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEmail_Edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEmail_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ListView listvEmails;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scLabelExt lblColonia;
        private SC_ControlsCS.scTextBoxExt txtIdColonia;
        private SC_ControlsCS.scLabelExt lblMunicipio;
        private SC_ControlsCS.scTextBoxExt txtIdMunicipio;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private SC_ControlsCS.scTextBoxExt txtCodigoPostal;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtCalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnColonias;
        private System.Windows.Forms.Button btnMunicipios;
        private System.Windows.Forms.Button btnEstados;
        private SC_ControlsCS.scTextBoxExt txtD_Pais;
        private System.Windows.Forms.Label label31;
        private SC_ControlsCS.scTextBoxExt txtD_NoInterior;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scTextBoxExt txtD_NoExterior;
        private System.Windows.Forms.Label label17;
        private SC_ControlsCS.scTextBoxExt txtNombreComercial;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}