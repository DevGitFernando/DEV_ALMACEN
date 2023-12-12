namespace Dll_IATP2.Configuracion
{
    partial class FrmTerminales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTerminales));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEsInterface = new System.Windows.Forms.CheckBox();
            this.btnGetMac = new System.Windows.Forms.Button();
            this.txtMac = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdTerminal = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.tmIMach = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEsInterface);
            this.groupBox1.Controls.Add(this.btnGetMac);
            this.groupBox1.Controls.Add(this.txtMac);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIdTerminal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(495, 146);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Generales";
            // 
            // chkEsInterface
            // 
            this.chkEsInterface.Location = new System.Drawing.Point(99, 116);
            this.chkEsInterface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEsInterface.Name = "chkEsInterface";
            this.chkEsInterface.Size = new System.Drawing.Size(383, 22);
            this.chkEsInterface.TabIndex = 3;
            this.chkEsInterface.Text = "Es servidor de interface";
            this.chkEsInterface.UseVisualStyleBackColor = true;
            // 
            // btnGetMac
            // 
            this.btnGetMac.Image = ((System.Drawing.Image)(resources.GetObject("btnGetMac.Image")));
            this.btnGetMac.Location = new System.Drawing.Point(184, 23);
            this.btnGetMac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetMac.Name = "btnGetMac";
            this.btnGetMac.Size = new System.Drawing.Size(51, 26);
            this.btnGetMac.TabIndex = 4;
            this.btnGetMac.UseVisualStyleBackColor = true;
            this.btnGetMac.Click += new System.EventHandler(this.btnGetMac_Click);
            // 
            // txtMac
            // 
            this.txtMac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMac.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMac.Decimales = 2;
            this.txtMac.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtMac.ForeColor = System.Drawing.Color.Black;
            this.txtMac.Location = new System.Drawing.Point(99, 86);
            this.txtMac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMac.MaxLength = 20;
            this.txtMac.Name = "txtMac";
            this.txtMac.PermitirApostrofo = false;
            this.txtMac.PermitirNegativos = false;
            this.txtMac.Size = new System.Drawing.Size(223, 22);
            this.txtMac.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "MAC :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdTerminal
            // 
            this.txtIdTerminal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdTerminal.Decimales = 2;
            this.txtIdTerminal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdTerminal.ForeColor = System.Drawing.Color.Black;
            this.txtIdTerminal.Location = new System.Drawing.Point(99, 25);
            this.txtIdTerminal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIdTerminal.MaxLength = 3;
            this.txtIdTerminal.Name = "txtIdTerminal";
            this.txtIdTerminal.PermitirApostrofo = false;
            this.txtIdTerminal.PermitirNegativos = false;
            this.txtIdTerminal.Size = new System.Drawing.Size(80, 22);
            this.txtIdTerminal.TabIndex = 0;
            this.txtIdTerminal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdTerminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdTerminal_KeyDown);
            this.txtIdTerminal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdTerminal_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Terminal :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(99, 55);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(381, 22);
            this.txtNombre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(521, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // tmIMach
            // 
            this.tmIMach.Interval = 500;
            this.tmIMach.Tick += new System.EventHandler(this.tmIMach_Tick);
            // 
            // FrmTerminales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 187);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmTerminales";
            this.Text = "Registro de Terrminales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTerminales_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdTerminal;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scTextBoxExt txtMac;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmIMach;
        private System.Windows.Forms.Button btnGetMac;
        private System.Windows.Forms.CheckBox chkEsInterface;
    }
}