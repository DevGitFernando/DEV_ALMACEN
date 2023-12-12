namespace DllFarmaciaSoft.Vales
{
    partial class FrmValesExcepcion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValesExcepcion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIdEstado = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScriptGenerado = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLlaveGenerada = new SC_ControlsCS.scTextBoxExt();
            this.dtpFechaDeSistema = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnConfirmarHuella = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_04 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDecodificar = new System.Windows.Forms.ToolStripButton();
            this.lblDecodificado = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDecodificado);
            this.groupBox1.Controls.Add(this.txtIdFarmacia);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIdEstado);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtScriptGenerado);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLlaveGenerada);
            this.groupBox1.Controls.Add(this.dtpFechaDeSistema);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtIdFarmacia
            // 
            this.txtIdFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdFarmacia.Decimales = 2;
            this.txtIdFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtIdFarmacia.Location = new System.Drawing.Point(150, 52);
            this.txtIdFarmacia.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdFarmacia.MaxLength = 4;
            this.txtIdFarmacia.Name = "txtIdFarmacia";
            this.txtIdFarmacia.PermitirApostrofo = false;
            this.txtIdFarmacia.PermitirNegativos = false;
            this.txtIdFarmacia.Size = new System.Drawing.Size(77, 22);
            this.txtIdFarmacia.TabIndex = 57;
            this.txtIdFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 59;
            this.label5.Text = "Farmacia :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstado.Decimales = 2;
            this.txtIdEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstado.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstado.Location = new System.Drawing.Point(150, 22);
            this.txtIdEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdEstado.MaxLength = 2;
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.PermitirApostrofo = false;
            this.txtIdEstado.PermitirNegativos = false;
            this.txtIdEstado.Size = new System.Drawing.Size(77, 22);
            this.txtIdEstado.TabIndex = 56;
            this.txtIdEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 20);
            this.label3.TabIndex = 58;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 19);
            this.label2.TabIndex = 55;
            this.label2.Text = "Script generado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtScriptGenerado
            // 
            this.txtScriptGenerado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtScriptGenerado.Decimales = 2;
            this.txtScriptGenerado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtScriptGenerado.ForeColor = System.Drawing.Color.Black;
            this.txtScriptGenerado.Location = new System.Drawing.Point(150, 141);
            this.txtScriptGenerado.Margin = new System.Windows.Forms.Padding(4);
            this.txtScriptGenerado.MaxLength = 1000;
            this.txtScriptGenerado.Multiline = true;
            this.txtScriptGenerado.Name = "txtScriptGenerado";
            this.txtScriptGenerado.PermitirApostrofo = false;
            this.txtScriptGenerado.PermitirNegativos = false;
            this.txtScriptGenerado.ReadOnly = true;
            this.txtScriptGenerado.Size = new System.Drawing.Size(524, 107);
            this.txtScriptGenerado.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 113);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 19);
            this.label1.TabIndex = 53;
            this.label1.Text = "Llave generada :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLlaveGenerada
            // 
            this.txtLlaveGenerada.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtLlaveGenerada.Decimales = 2;
            this.txtLlaveGenerada.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtLlaveGenerada.ForeColor = System.Drawing.Color.Black;
            this.txtLlaveGenerada.Location = new System.Drawing.Point(150, 111);
            this.txtLlaveGenerada.Margin = new System.Windows.Forms.Padding(4);
            this.txtLlaveGenerada.MaxLength = 100;
            this.txtLlaveGenerada.Multiline = true;
            this.txtLlaveGenerada.Name = "txtLlaveGenerada";
            this.txtLlaveGenerada.PermitirApostrofo = false;
            this.txtLlaveGenerada.PermitirNegativos = false;
            this.txtLlaveGenerada.ReadOnly = true;
            this.txtLlaveGenerada.Size = new System.Drawing.Size(524, 22);
            this.txtLlaveGenerada.TabIndex = 1;
            this.txtLlaveGenerada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpFechaDeSistema
            // 
            this.dtpFechaDeSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeSistema.Enabled = false;
            this.dtpFechaDeSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeSistema.Location = new System.Drawing.Point(150, 81);
            this.dtpFechaDeSistema.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaDeSistema.Name = "dtpFechaDeSistema";
            this.dtpFechaDeSistema.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaDeSistema.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 18);
            this.label4.TabIndex = 51;
            this.label4.Text = "Fecha de vigencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnConfirmarHuella,
            this.toolStripSeparator_04,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnDecodificar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(717, 25);
            this.toolStripBarraMenu.TabIndex = 5;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Limpiar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConfirmarHuella
            // 
            this.btnConfirmarHuella.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConfirmarHuella.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmarHuella.Image")));
            this.btnConfirmarHuella.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfirmarHuella.Name = "btnConfirmarHuella";
            this.btnConfirmarHuella.Size = new System.Drawing.Size(23, 22);
            this.btnConfirmarHuella.Text = "Codificar";
            this.btnConfirmarHuella.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnConfirmarHuella.Click += new System.EventHandler(this.btnConfirmarHuella_Click);
            // 
            // toolStripSeparator_04
            // 
            this.toolStripSeparator_04.Name = "toolStripSeparator_04";
            this.toolStripSeparator_04.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Generar script";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDecodificar
            // 
            this.btnDecodificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDecodificar.Image = ((System.Drawing.Image)(resources.GetObject("btnDecodificar.Image")));
            this.btnDecodificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDecodificar.Name = "btnDecodificar";
            this.btnDecodificar.Size = new System.Drawing.Size(23, 22);
            this.btnDecodificar.Text = "Decodificar";
            this.btnDecodificar.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.btnDecodificar.Click += new System.EventHandler(this.btnDecodificar_Click);
            // 
            // lblDecodificado
            // 
            this.lblDecodificado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDecodificado.Location = new System.Drawing.Point(278, 82);
            this.lblDecodificado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDecodificado.Name = "lblDecodificado";
            this.lblDecodificado.Size = new System.Drawing.Size(396, 22);
            this.lblDecodificado.TabIndex = 60;
            this.lblDecodificado.Text = "Fecha de vigencia :";
            this.lblDecodificado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValesExcepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 306);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmValesExcepcion";
            this.Text = "Generar excepción de emisión de vales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValesExcepcion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaDeSistema;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtLlaveGenerada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtScriptGenerado;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnConfirmarHuella;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_04;
        private SC_ControlsCS.scTextBoxExt txtIdFarmacia;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtIdEstado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btnDecodificar;
        private System.Windows.Forms.Label lblDecodificado;
    }
}