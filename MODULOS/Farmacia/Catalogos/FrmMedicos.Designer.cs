namespace Farmacia.Catalogos
{
    partial class FrmMedicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMedicos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEspecialidad = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCedula = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaterno = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPaterno = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtD_NoInterior = new SC_ControlsCS.scTextBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtD_NoExterior = new SC_ControlsCS.scTextBoxExt();
            this.label17 = new System.Windows.Forms.Label();
            this.txtD_Pais = new SC_ControlsCS.scTextBoxExt();
            this.label31 = new System.Windows.Forms.Label();
            this.lblColonia = new SC_ControlsCS.scLabelExt();
            this.txtIdColonia = new SC_ControlsCS.scTextBoxExt();
            this.lblMunicipio = new SC_ControlsCS.scLabelExt();
            this.txtIdMunicipio = new SC_ControlsCS.scTextBoxExt();
            this.txtCodigoPostal = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCalle = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(928, 58);
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
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 4);
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
            this.toolStripSeparator1.Size = new System.Drawing.Size(12, 4);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(12, 4);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboEspecialidad);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCedula);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMaterno);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPaterno);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 66);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(900, 194);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Médico:";
            // 
            // cboEspecialidad
            // 
            this.cboEspecialidad.BackColorEnabled = System.Drawing.Color.White;
            this.cboEspecialidad.Data = "";
            this.cboEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEspecialidad.Filtro = " 1 = 1";
            this.cboEspecialidad.FormattingEnabled = true;
            this.cboEspecialidad.ListaItemsBusqueda = 20;
            this.cboEspecialidad.Location = new System.Drawing.Point(153, 154);
            this.cboEspecialidad.Margin = new System.Windows.Forms.Padding(4);
            this.cboEspecialidad.MostrarToolTip = false;
            this.cboEspecialidad.Name = "cboEspecialidad";
            this.cboEspecialidad.Size = new System.Drawing.Size(337, 24);
            this.cboEspecialidad.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 159);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Especialidad :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCedula
            // 
            this.txtCedula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCedula.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCedula.Decimales = 2;
            this.txtCedula.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtCedula.ForeColor = System.Drawing.Color.Black;
            this.txtCedula.Location = new System.Drawing.Point(635, 154);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(4);
            this.txtCedula.MaxLength = 30;
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.PermitirApostrofo = false;
            this.txtCedula.PermitirNegativos = false;
            this.txtCedula.Size = new System.Drawing.Size(245, 22);
            this.txtCedula.TabIndex = 5;
            this.txtCedula.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(501, 159);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Número Cédula :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaterno
            // 
            this.txtMaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMaterno.Decimales = 2;
            this.txtMaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMaterno.ForeColor = System.Drawing.Color.Black;
            this.txtMaterno.Location = new System.Drawing.Point(153, 119);
            this.txtMaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaterno.MaxLength = 50;
            this.txtMaterno.Name = "txtMaterno";
            this.txtMaterno.PermitirApostrofo = false;
            this.txtMaterno.PermitirNegativos = false;
            this.txtMaterno.Size = new System.Drawing.Size(727, 22);
            this.txtMaterno.TabIndex = 3;
            this.txtMaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 124);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Apellido Materno :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPaterno
            // 
            this.txtPaterno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPaterno.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPaterno.Decimales = 2;
            this.txtPaterno.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPaterno.ForeColor = System.Drawing.Color.Black;
            this.txtPaterno.Location = new System.Drawing.Point(153, 87);
            this.txtPaterno.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaterno.MaxLength = 50;
            this.txtPaterno.Name = "txtPaterno";
            this.txtPaterno.PermitirApostrofo = false;
            this.txtPaterno.PermitirNegativos = false;
            this.txtPaterno.Size = new System.Drawing.Size(727, 22);
            this.txtPaterno.TabIndex = 2;
            this.txtPaterno.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Apellido Paterno :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(416, 22);
            this.lblCancelado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(101, 17);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(153, 22);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.MaxLength = 6;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(84, 22);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(153, 55);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(727, 22);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Id. Médico :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(52, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtD_NoInterior);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtD_NoExterior);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtD_Pais);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.lblColonia);
            this.groupBox2.Controls.Add(this.txtIdColonia);
            this.groupBox2.Controls.Add(this.lblMunicipio);
            this.groupBox2.Controls.Add(this.txtIdMunicipio);
            this.groupBox2.Controls.Add(this.txtCodigoPostal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCalle);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(15, 267);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(900, 186);
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
            this.txtD_NoInterior.Location = new System.Drawing.Point(449, 149);
            this.txtD_NoInterior.Margin = new System.Windows.Forms.Padding(4);
            this.txtD_NoInterior.MaxLength = 20;
            this.txtD_NoInterior.Name = "txtD_NoInterior";
            this.txtD_NoInterior.PermitirApostrofo = false;
            this.txtD_NoInterior.PermitirNegativos = false;
            this.txtD_NoInterior.Size = new System.Drawing.Size(139, 22);
            this.txtD_NoInterior.TabIndex = 9;
            this.txtD_NoInterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(339, 153);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 17);
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
            this.txtD_NoExterior.Location = new System.Drawing.Point(136, 149);
            this.txtD_NoExterior.Margin = new System.Windows.Forms.Padding(4);
            this.txtD_NoExterior.MaxLength = 20;
            this.txtD_NoExterior.Name = "txtD_NoExterior";
            this.txtD_NoExterior.PermitirApostrofo = false;
            this.txtD_NoExterior.PermitirNegativos = false;
            this.txtD_NoExterior.Size = new System.Drawing.Size(139, 22);
            this.txtD_NoExterior.TabIndex = 8;
            this.txtD_NoExterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(11, 153);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(121, 17);
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
            this.txtD_Pais.Location = new System.Drawing.Point(136, 23);
            this.txtD_Pais.Margin = new System.Windows.Forms.Padding(4);
            this.txtD_Pais.MaxLength = 100;
            this.txtD_Pais.Name = "txtD_Pais";
            this.txtD_Pais.PermitirApostrofo = false;
            this.txtD_Pais.PermitirNegativos = false;
            this.txtD_Pais.Size = new System.Drawing.Size(744, 22);
            this.txtD_Pais.TabIndex = 0;
            this.txtD_Pais.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(11, 28);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(121, 16);
            this.label31.TabIndex = 71;
            this.label31.Text = "* País  :";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblColonia
            // 
            this.lblColonia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColonia.Location = new System.Drawing.Point(213, 86);
            this.lblColonia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColonia.MostrarToolTip = false;
            this.lblColonia.Name = "lblColonia";
            this.lblColonia.Size = new System.Drawing.Size(668, 26);
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
            this.txtIdColonia.Location = new System.Drawing.Point(136, 86);
            this.txtIdColonia.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdColonia.Name = "txtIdColonia";
            this.txtIdColonia.PermitirApostrofo = false;
            this.txtIdColonia.PermitirNegativos = false;
            this.txtIdColonia.Size = new System.Drawing.Size(68, 22);
            this.txtIdColonia.TabIndex = 3;
            this.txtIdColonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdColonia.TextChanged += new System.EventHandler(this.txtIdColonia_TextChanged);
            this.txtIdColonia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdColonia_KeyDown);
            this.txtIdColonia.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdColonia_Validating);
            // 
            // lblMunicipio
            // 
            this.lblMunicipio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMunicipio.Location = new System.Drawing.Point(213, 54);
            this.lblMunicipio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMunicipio.MostrarToolTip = false;
            this.lblMunicipio.Name = "lblMunicipio";
            this.lblMunicipio.Size = new System.Drawing.Size(668, 26);
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
            this.txtIdMunicipio.Location = new System.Drawing.Point(136, 54);
            this.txtIdMunicipio.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdMunicipio.Name = "txtIdMunicipio";
            this.txtIdMunicipio.PermitirApostrofo = false;
            this.txtIdMunicipio.PermitirNegativos = false;
            this.txtIdMunicipio.Size = new System.Drawing.Size(68, 22);
            this.txtIdMunicipio.TabIndex = 2;
            this.txtIdMunicipio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdMunicipio.TextChanged += new System.EventHandler(this.txtIdMunicipio_TextChanged);
            this.txtIdMunicipio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdMunicipio_KeyDown);
            this.txtIdMunicipio.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdMunicipio_Validating);
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoPostal.Decimales = 2;
            this.txtCodigoPostal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoPostal.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoPostal.Location = new System.Drawing.Point(741, 149);
            this.txtCodigoPostal.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoPostal.MaxLength = 10;
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.PermitirApostrofo = false;
            this.txtCodigoPostal.PermitirNegativos = false;
            this.txtCodigoPostal.Size = new System.Drawing.Size(139, 22);
            this.txtCodigoPostal.TabIndex = 10;
            this.txtCodigoPostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(616, 153);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 18);
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
            this.txtCalle.Location = new System.Drawing.Point(136, 117);
            this.txtCalle.Margin = new System.Windows.Forms.Padding(4);
            this.txtCalle.MaxLength = 100;
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.PermitirApostrofo = false;
            this.txtCalle.PermitirNegativos = false;
            this.txtCalle.Size = new System.Drawing.Size(744, 22);
            this.txtCalle.TabIndex = 7;
            this.txtCalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(11, 122);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 65;
            this.label7.Text = "* Calle :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 91);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 16);
            this.label9.TabIndex = 64;
            this.label9.Text = "* Colonia :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 16);
            this.label10.TabIndex = 63;
            this.label10.Text = "* Municipio :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmMedicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmMedicos";
            this.ShowIcon = false;
            this.Text = "Médicos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMedicos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtMaterno;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtPaterno;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtCedula;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEspecialidad;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scTextBoxExt txtD_NoInterior;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scTextBoxExt txtD_NoExterior;
        private System.Windows.Forms.Label label17;
        private SC_ControlsCS.scTextBoxExt txtD_Pais;
        private System.Windows.Forms.Label label31;
        private SC_ControlsCS.scLabelExt lblColonia;
        private SC_ControlsCS.scTextBoxExt txtIdColonia;
        private SC_ControlsCS.scLabelExt lblMunicipio;
        private SC_ControlsCS.scTextBoxExt txtIdMunicipio;
        private SC_ControlsCS.scTextBoxExt txtCodigoPostal;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtCalle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}