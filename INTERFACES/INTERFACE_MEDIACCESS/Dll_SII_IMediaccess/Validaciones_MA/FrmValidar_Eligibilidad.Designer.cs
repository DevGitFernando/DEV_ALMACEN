namespace Dll_SII_IMediaccess.Validaciones_MA
{
    partial class FrmValidar_Eligibilidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidar_Eligibilidad));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirDispensacion = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFolioReceta = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus3 = new SC_ControlsCS.scLabelStatus();
            this.btnSurtirReceta = new System.Windows.Forms.Button();
            this.btnvalidarElegibilidad = new System.Windows.Forms.Button();
            this.txtFolioEligibilidad = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus1 = new SC_ControlsCS.scLabelStatus();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.lblFolioRecetaAsociado = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus9 = new SC_ControlsCS.scLabelStatus();
            this.lblResultadoValidacion = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus5 = new SC_ControlsCS.scLabelStatus();
            this.lblCopago = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus10 = new SC_ControlsCS.scLabelStatus();
            this.lblProveedor = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus8 = new SC_ControlsCS.scLabelStatus();
            this.lblClaveProveedor = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus6 = new SC_ControlsCS.scLabelStatus();
            this.lblBeneficiario = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus4 = new SC_ControlsCS.scLabelStatus();
            this.lblEmpresa = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus2 = new SC_ControlsCS.scLabelStatus();
            this.lblProducto = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus11 = new SC_ControlsCS.scLabelStatus();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnAbrirDispensacion,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(893, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirDispensacion
            // 
            this.btnAbrirDispensacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirDispensacion.Enabled = false;
            this.btnAbrirDispensacion.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirDispensacion.Image")));
            this.btnAbrirDispensacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirDispensacion.Name = "btnAbrirDispensacion";
            this.btnAbrirDispensacion.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirDispensacion.Text = "&Guardar (CTRL + G)";
            this.btnAbrirDispensacion.Visible = false;
            this.btnAbrirDispensacion.Click += new System.EventHandler(this.btnAbrirDispensacion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar (CTRL + E) ";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
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
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFolioReceta);
            this.groupBox1.Controls.Add(this.scLabelStatus3);
            this.groupBox1.Controls.Add(this.btnSurtirReceta);
            this.groupBox1.Controls.Add(this.btnvalidarElegibilidad);
            this.groupBox1.Controls.Add(this.txtFolioEligibilidad);
            this.groupBox1.Controls.Add(this.scLabelStatus1);
            this.groupBox1.Location = new System.Drawing.Point(15, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(868, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elegibilidad";
            // 
            // txtFolioReceta
            // 
            this.txtFolioReceta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioReceta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioReceta.Decimales = 2;
            this.txtFolioReceta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioReceta.ForeColor = System.Drawing.Color.Black;
            this.txtFolioReceta.Location = new System.Drawing.Point(171, 57);
            this.txtFolioReceta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolioReceta.MaxLength = 50;
            this.txtFolioReceta.Name = "txtFolioReceta";
            this.txtFolioReceta.PermitirApostrofo = false;
            this.txtFolioReceta.PermitirNegativos = false;
            this.txtFolioReceta.Size = new System.Drawing.Size(277, 22);
            this.txtFolioReceta.TabIndex = 2;
            this.txtFolioReceta.Text = "E006493943";
            this.txtFolioReceta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus3
            // 
            this.scLabelStatus3.Location = new System.Drawing.Point(8, 57);
            this.scLabelStatus3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus3.Name = "scLabelStatus3";
            this.scLabelStatus3.Size = new System.Drawing.Size(163, 25);
            this.scLabelStatus3.TabIndex = 4;
            this.scLabelStatus3.Text = "Folio de receta : ";
            this.scLabelStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSurtirReceta
            // 
            this.btnSurtirReceta.Location = new System.Drawing.Point(457, 55);
            this.btnSurtirReceta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSurtirReceta.Name = "btnSurtirReceta";
            this.btnSurtirReceta.Size = new System.Drawing.Size(397, 28);
            this.btnSurtirReceta.TabIndex = 3;
            this.btnSurtirReceta.Text = "Surtir receta electrónica";
            this.btnSurtirReceta.UseVisualStyleBackColor = true;
            this.btnSurtirReceta.Click += new System.EventHandler(this.btnSurtirReceta_Click);
            // 
            // btnvalidarElegibilidad
            // 
            this.btnvalidarElegibilidad.Location = new System.Drawing.Point(457, 22);
            this.btnvalidarElegibilidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnvalidarElegibilidad.Name = "btnvalidarElegibilidad";
            this.btnvalidarElegibilidad.Size = new System.Drawing.Size(397, 28);
            this.btnvalidarElegibilidad.TabIndex = 1;
            this.btnvalidarElegibilidad.Text = "Solicitar información de Elegibilidad";
            this.btnvalidarElegibilidad.UseVisualStyleBackColor = true;
            this.btnvalidarElegibilidad.Click += new System.EventHandler(this.btnvalidarElegibilidad_Click);
            // 
            // txtFolioEligibilidad
            // 
            this.txtFolioEligibilidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioEligibilidad.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioEligibilidad.Decimales = 2;
            this.txtFolioEligibilidad.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioEligibilidad.ForeColor = System.Drawing.Color.Black;
            this.txtFolioEligibilidad.Location = new System.Drawing.Point(171, 23);
            this.txtFolioEligibilidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFolioEligibilidad.MaxLength = 50;
            this.txtFolioEligibilidad.Name = "txtFolioEligibilidad";
            this.txtFolioEligibilidad.PermitirApostrofo = false;
            this.txtFolioEligibilidad.PermitirNegativos = false;
            this.txtFolioEligibilidad.Size = new System.Drawing.Size(277, 22);
            this.txtFolioEligibilidad.TabIndex = 0;
            this.txtFolioEligibilidad.Text = "E006493943";
            this.txtFolioEligibilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus1
            // 
            this.scLabelStatus1.Location = new System.Drawing.Point(57, 23);
            this.scLabelStatus1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus1.Name = "scLabelStatus1";
            this.scLabelStatus1.Size = new System.Drawing.Size(113, 25);
            this.scLabelStatus1.TabIndex = 0;
            this.scLabelStatus1.Text = "Elegibilidad : ";
            this.scLabelStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lblProducto);
            this.FrameInformacion.Controls.Add(this.scLabelStatus11);
            this.FrameInformacion.Controls.Add(this.lblFolioRecetaAsociado);
            this.FrameInformacion.Controls.Add(this.scLabelStatus9);
            this.FrameInformacion.Controls.Add(this.lblResultadoValidacion);
            this.FrameInformacion.Controls.Add(this.scLabelStatus5);
            this.FrameInformacion.Controls.Add(this.lblCopago);
            this.FrameInformacion.Controls.Add(this.scLabelStatus10);
            this.FrameInformacion.Controls.Add(this.lblProveedor);
            this.FrameInformacion.Controls.Add(this.scLabelStatus8);
            this.FrameInformacion.Controls.Add(this.lblClaveProveedor);
            this.FrameInformacion.Controls.Add(this.scLabelStatus6);
            this.FrameInformacion.Controls.Add(this.lblBeneficiario);
            this.FrameInformacion.Controls.Add(this.scLabelStatus4);
            this.FrameInformacion.Controls.Add(this.lblEmpresa);
            this.FrameInformacion.Controls.Add(this.scLabelStatus2);
            this.FrameInformacion.Location = new System.Drawing.Point(15, 130);
            this.FrameInformacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameInformacion.Size = new System.Drawing.Size(868, 286);
            this.FrameInformacion.TabIndex = 2;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información";
            // 
            // lblFolioRecetaAsociado
            // 
            this.lblFolioRecetaAsociado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lblFolioRecetaAsociado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.lblFolioRecetaAsociado.Decimales = 2;
            this.lblFolioRecetaAsociado.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.lblFolioRecetaAsociado.ForeColor = System.Drawing.Color.Black;
            this.lblFolioRecetaAsociado.Location = new System.Drawing.Point(170, 215);
            this.lblFolioRecetaAsociado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblFolioRecetaAsociado.MaxLength = 50;
            this.lblFolioRecetaAsociado.Name = "lblFolioRecetaAsociado";
            this.lblFolioRecetaAsociado.PermitirApostrofo = false;
            this.lblFolioRecetaAsociado.PermitirNegativos = false;
            this.lblFolioRecetaAsociado.Size = new System.Drawing.Size(277, 22);
            this.lblFolioRecetaAsociado.TabIndex = 5;
            this.lblFolioRecetaAsociado.Text = "E006493943";
            this.lblFolioRecetaAsociado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus9
            // 
            this.scLabelStatus9.Location = new System.Drawing.Point(7, 214);
            this.scLabelStatus9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus9.Name = "scLabelStatus9";
            this.scLabelStatus9.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus9.TabIndex = 13;
            this.scLabelStatus9.Text = "Folio receta asociado : ";
            this.scLabelStatus9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResultadoValidacion
            // 
            this.lblResultadoValidacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultadoValidacion.Location = new System.Drawing.Point(170, 246);
            this.lblResultadoValidacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultadoValidacion.Name = "lblResultadoValidacion";
            this.lblResultadoValidacion.Size = new System.Drawing.Size(684, 26);
            this.lblResultadoValidacion.TabIndex = 5;
            this.lblResultadoValidacion.Text = "Elegibilidad : ";
            this.lblResultadoValidacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus5
            // 
            this.scLabelStatus5.Location = new System.Drawing.Point(7, 246);
            this.scLabelStatus5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus5.Name = "scLabelStatus5";
            this.scLabelStatus5.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus5.TabIndex = 11;
            this.scLabelStatus5.Text = "Resultado validación : ";
            this.scLabelStatus5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopago
            // 
            this.lblCopago.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCopago.Location = new System.Drawing.Point(170, 183);
            this.lblCopago.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopago.Name = "lblCopago";
            this.lblCopago.Size = new System.Drawing.Size(684, 26);
            this.lblCopago.TabIndex = 4;
            this.lblCopago.Text = "Elegibilidad : ";
            this.lblCopago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus10
            // 
            this.scLabelStatus10.Location = new System.Drawing.Point(7, 183);
            this.scLabelStatus10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus10.Name = "scLabelStatus10";
            this.scLabelStatus10.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus10.TabIndex = 9;
            this.scLabelStatus10.Text = "Copago : ";
            this.scLabelStatus10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProveedor
            // 
            this.lblProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProveedor.Location = new System.Drawing.Point(170, 151);
            this.lblProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(684, 26);
            this.lblProveedor.TabIndex = 3;
            this.lblProveedor.Text = "Elegibilidad : ";
            this.lblProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus8
            // 
            this.scLabelStatus8.Location = new System.Drawing.Point(7, 151);
            this.scLabelStatus8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus8.Name = "scLabelStatus8";
            this.scLabelStatus8.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus8.TabIndex = 7;
            this.scLabelStatus8.Text = "Nombre proveedor : ";
            this.scLabelStatus8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblClaveProveedor
            // 
            this.lblClaveProveedor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveProveedor.Location = new System.Drawing.Point(170, 120);
            this.lblClaveProveedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClaveProveedor.Name = "lblClaveProveedor";
            this.lblClaveProveedor.Size = new System.Drawing.Size(684, 26);
            this.lblClaveProveedor.TabIndex = 2;
            this.lblClaveProveedor.Text = "Elegibilidad : ";
            this.lblClaveProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus6
            // 
            this.scLabelStatus6.Location = new System.Drawing.Point(7, 120);
            this.scLabelStatus6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus6.Name = "scLabelStatus6";
            this.scLabelStatus6.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus6.TabIndex = 5;
            this.scLabelStatus6.Text = "Clave proveedor : ";
            this.scLabelStatus6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBeneficiario
            // 
            this.lblBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficiario.Location = new System.Drawing.Point(170, 88);
            this.lblBeneficiario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBeneficiario.Name = "lblBeneficiario";
            this.lblBeneficiario.Size = new System.Drawing.Size(684, 26);
            this.lblBeneficiario.TabIndex = 1;
            this.lblBeneficiario.Text = "Elegibilidad : ";
            this.lblBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus4
            // 
            this.scLabelStatus4.Location = new System.Drawing.Point(7, 88);
            this.scLabelStatus4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus4.Name = "scLabelStatus4";
            this.scLabelStatus4.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus4.TabIndex = 3;
            this.scLabelStatus4.Text = "Nombre beneficiario : ";
            this.scLabelStatus4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEmpresa.Location = new System.Drawing.Point(170, 23);
            this.lblEmpresa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(684, 26);
            this.lblEmpresa.TabIndex = 0;
            this.lblEmpresa.Text = "Elegibilidad : ";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus2
            // 
            this.scLabelStatus2.Location = new System.Drawing.Point(7, 23);
            this.scLabelStatus2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus2.Name = "scLabelStatus2";
            this.scLabelStatus2.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus2.TabIndex = 1;
            this.scLabelStatus2.Text = "Empresa : ";
            this.scLabelStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProducto
            // 
            this.lblProducto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProducto.Location = new System.Drawing.Point(170, 55);
            this.lblProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(684, 26);
            this.lblProducto.TabIndex = 14;
            this.lblProducto.Text = "Elegibilidad : ";
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus11
            // 
            this.scLabelStatus11.Location = new System.Drawing.Point(7, 55);
            this.scLabelStatus11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scLabelStatus11.Name = "scLabelStatus11";
            this.scLabelStatus11.Size = new System.Drawing.Size(164, 26);
            this.scLabelStatus11.TabIndex = 15;
            this.scLabelStatus11.Text = "Producto : ";
            this.scLabelStatus11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValidar_Eligibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 426);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmValidar_Eligibilidad";
            this.Text = "Validar elegibilidad";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidar_Eligibilidad_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameInformacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnAbrirDispensacion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private SC_ControlsCS.scTextBoxExt txtFolioEligibilidad;
        private SC_ControlsCS.scLabelStatus scLabelStatus1;
        private System.Windows.Forms.Button btnvalidarElegibilidad;
        private System.Windows.Forms.Button btnSurtirReceta;
        private SC_ControlsCS.scLabelStatus scLabelStatus2;
        private SC_ControlsCS.scLabelStatus lblEmpresa;
        private SC_ControlsCS.scLabelStatus lblClaveProveedor;
        private SC_ControlsCS.scLabelStatus scLabelStatus6;
        private SC_ControlsCS.scLabelStatus lblBeneficiario;
        private SC_ControlsCS.scLabelStatus scLabelStatus4;
        private SC_ControlsCS.scLabelStatus lblProveedor;
        private SC_ControlsCS.scLabelStatus scLabelStatus8;
        private SC_ControlsCS.scLabelStatus lblCopago;
        private SC_ControlsCS.scLabelStatus scLabelStatus10;
        private SC_ControlsCS.scLabelStatus lblResultadoValidacion;
        private SC_ControlsCS.scLabelStatus scLabelStatus5;
        private SC_ControlsCS.scTextBoxExt txtFolioReceta;
        private SC_ControlsCS.scLabelStatus scLabelStatus3;
        private SC_ControlsCS.scLabelStatus scLabelStatus9;
        private SC_ControlsCS.scTextBoxExt lblFolioRecetaAsociado;
        private SC_ControlsCS.scLabelStatus lblProducto;
        private SC_ControlsCS.scLabelStatus scLabelStatus11;
    }
}