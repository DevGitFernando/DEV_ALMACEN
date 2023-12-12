namespace Farmacia.Vales
{
    partial class FrmGeneracionServicioDomicilio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneracionServicioDomicilio));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_01 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_02 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_03 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_04 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpHora_Hasta = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHora_Desde = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDomicilioBeneficiario = new System.Windows.Forms.Button();
            this.lblDomicilioBeneficiario = new SC_ControlsCS.scLabelExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNombreBeneficiario = new SC_ControlsCS.scLabelExt();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRegistrarSerDom = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator_01,
            this.btnGuardar,
            this.toolStripSeparator_02,
            this.btnCancelar,
            this.toolStripSeparator_03,
            this.btnImprimir,
            this.toolStripSeparator_04,
            this.btnRegistrarSerDom});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(618, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            // toolStripSeparator_01
            // 
            this.toolStripSeparator_01.Name = "toolStripSeparator_01";
            this.toolStripSeparator_01.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator_02
            // 
            this.toolStripSeparator_02.Name = "toolStripSeparator_02";
            this.toolStripSeparator_02.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator_03
            // 
            this.toolStripSeparator_03.Name = "toolStripSeparator_03";
            this.toolStripSeparator_03.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator_03.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator_04
            // 
            this.toolStripSeparator_04.Name = "toolStripSeparator_04";
            this.toolStripSeparator_04.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 52);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(414, 0);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(171, 17);
            this.chkMostrarImpresionEnPantalla.TabIndex = 47;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar Vista Previa Impresión ";
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(494, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(364, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(188, 20);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.Enabled = false;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(82, 20);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpHora_Hasta);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtpHora_Desde);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(597, 46);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Horario sugerido de entrega";
            // 
            // dtpHora_Hasta
            // 
            this.dtpHora_Hasta.CustomFormat = "H:00";
            this.dtpHora_Hasta.Enabled = false;
            this.dtpHora_Hasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora_Hasta.Location = new System.Drawing.Point(367, 17);
            this.dtpHora_Hasta.Name = "dtpHora_Hasta";
            this.dtpHora_Hasta.ShowUpDown = true;
            this.dtpHora_Hasta.Size = new System.Drawing.Size(73, 20);
            this.dtpHora_Hasta.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(300, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Y :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpHora_Desde
            // 
            this.dtpHora_Desde.CustomFormat = "H:00";
            this.dtpHora_Desde.Enabled = false;
            this.dtpHora_Desde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHora_Desde.Location = new System.Drawing.Point(173, 17);
            this.dtpHora_Desde.Name = "dtpHora_Desde";
            this.dtpHora_Desde.ShowUpDown = true;
            this.dtpHora_Desde.Size = new System.Drawing.Size(73, 20);
            this.dtpHora_Desde.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(106, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Entre las :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDomicilioBeneficiario);
            this.groupBox3.Controls.Add(this.lblDomicilioBeneficiario);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblNombreBeneficiario);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(597, 191);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos del Beneficiario";
            // 
            // btnDomicilioBeneficiario
            // 
            this.btnDomicilioBeneficiario.Location = new System.Drawing.Point(514, 24);
            this.btnDomicilioBeneficiario.Name = "btnDomicilioBeneficiario";
            this.btnDomicilioBeneficiario.Size = new System.Drawing.Size(71, 23);
            this.btnDomicilioBeneficiario.TabIndex = 36;
            this.btnDomicilioBeneficiario.Text = "Domicilio";
            this.btnDomicilioBeneficiario.UseVisualStyleBackColor = true;
            this.btnDomicilioBeneficiario.Click += new System.EventHandler(this.btnDomicilioBeneficiario_Click);
            // 
            // lblDomicilioBeneficiario
            // 
            this.lblDomicilioBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDomicilioBeneficiario.Location = new System.Drawing.Point(79, 51);
            this.lblDomicilioBeneficiario.MostrarToolTip = false;
            this.lblDomicilioBeneficiario.Name = "lblDomicilioBeneficiario";
            this.lblDomicilioBeneficiario.Size = new System.Drawing.Size(506, 129);
            this.lblDomicilioBeneficiario.TabIndex = 35;
            this.lblDomicilioBeneficiario.Text = "scLabelExt2";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "Domicilio :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreBeneficiario
            // 
            this.lblNombreBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreBeneficiario.Location = new System.Drawing.Point(79, 25);
            this.lblNombreBeneficiario.MostrarToolTip = false;
            this.lblNombreBeneficiario.Name = "lblNombreBeneficiario";
            this.lblNombreBeneficiario.Size = new System.Drawing.Size(429, 20);
            this.lblNombreBeneficiario.TabIndex = 33;
            this.lblNombreBeneficiario.Text = "scLabelExt1";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "Nombre :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRegistrarSerDom
            // 
            this.btnRegistrarSerDom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRegistrarSerDom.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrarSerDom.Image")));
            this.btnRegistrarSerDom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRegistrarSerDom.Name = "btnRegistrarSerDom";
            this.btnRegistrarSerDom.Size = new System.Drawing.Size(23, 22);
            this.btnRegistrarSerDom.Text = "Registrar Servicio a domicilio";
            this.btnRegistrarSerDom.Click += new System.EventHandler(this.btnRegistrarSerDom_Click);
            // 
            // FrmGeneracionServicioDomicilio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 342);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGeneracionServicioDomicilio";
            this.Text = "Generación de servicio a domicilio";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGeneracionServicioDomicilio_Load);
            this.Shown += new System.EventHandler(this.FrmGeneracionServicioDomicilio_Shown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_01;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_02;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_03;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_04;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpHora_Desde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHora_Hasta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scLabelExt lblNombreBeneficiario;
        private SC_ControlsCS.scLabelExt lblDomicilioBeneficiario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDomicilioBeneficiario;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnRegistrarSerDom;
    }
}