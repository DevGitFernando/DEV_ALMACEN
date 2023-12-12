namespace Farmacia.Catalogos
{
    partial class FrmBeneficiarios_Identificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBeneficiarios_Identificacion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel_01_Frontal = new SC_ControlsCS.scImageBox();
            this.panel_02_Reverso = new SC_ControlsCS.scImageBox();
            this.toolStripDigitalizacion = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDigitalizar_01_Frente = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDigitalizar_02_Reverso = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cboCamaras = new SC_ControlsCS.scComboBoxExt();
            this.chkHabilitarZoom = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStripDigitalizacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1074, 25);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHabilitarZoom);
            this.groupBox1.Controls.Add(this.splitContainer);
            this.groupBox1.Controls.Add(this.toolStripDigitalizacion);
            this.groupBox1.Location = new System.Drawing.Point(11, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1056, 488);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagenes";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(2, 54);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panel_01_Frontal);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panel_02_Reverso);
            this.splitContainer.Size = new System.Drawing.Size(1052, 432);
            this.splitContainer.SplitterDistance = 527;
            this.splitContainer.TabIndex = 14;
            // 
            // panel_01_Frontal
            // 
            this.panel_01_Frontal.AutoScroll = true;
            this.panel_01_Frontal.AutoSize = false;
            this.panel_01_Frontal.Location = new System.Drawing.Point(54, 160);
            this.panel_01_Frontal.Margin = new System.Windows.Forms.Padding(2);
            this.panel_01_Frontal.Name = "panel_01_Frontal";
            this.panel_01_Frontal.Size = new System.Drawing.Size(203, 168);
            this.panel_01_Frontal.TabIndex = 2;
            // 
            // panel_02_Reverso
            // 
            this.panel_02_Reverso.AutoScroll = true;
            this.panel_02_Reverso.AutoSize = false;
            this.panel_02_Reverso.Location = new System.Drawing.Point(129, 144);
            this.panel_02_Reverso.Margin = new System.Windows.Forms.Padding(2);
            this.panel_02_Reverso.Name = "panel_02_Reverso";
            this.panel_02_Reverso.Size = new System.Drawing.Size(203, 168);
            this.panel_02_Reverso.TabIndex = 2;
            // 
            // toolStripDigitalizacion
            // 
            this.toolStripDigitalizacion.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripDigitalizacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.btnDigitalizar_01_Frente,
            this.toolStripSeparator4,
            this.btnDigitalizar_02_Reverso,
            this.toolStripSeparator3});
            this.toolStripDigitalizacion.Location = new System.Drawing.Point(2, 15);
            this.toolStripDigitalizacion.Name = "toolStripDigitalizacion";
            this.toolStripDigitalizacion.Size = new System.Drawing.Size(1052, 39);
            this.toolStripDigitalizacion.TabIndex = 13;
            this.toolStripDigitalizacion.Text = "Digitalizar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btnDigitalizar_01_Frente
            // 
            this.btnDigitalizar_01_Frente.Image = ((System.Drawing.Image)(resources.GetObject("btnDigitalizar_01_Frente.Image")));
            this.btnDigitalizar_01_Frente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDigitalizar_01_Frente.Name = "btnDigitalizar_01_Frente";
            this.btnDigitalizar_01_Frente.Size = new System.Drawing.Size(156, 36);
            this.btnDigitalizar_01_Frente.Text = "F4 - Digitalizar frontal";
            this.btnDigitalizar_01_Frente.Click += new System.EventHandler(this.btnDigitalizar_01_Frente_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnDigitalizar_02_Reverso
            // 
            this.btnDigitalizar_02_Reverso.Image = ((System.Drawing.Image)(resources.GetObject("btnDigitalizar_02_Reverso.Image")));
            this.btnDigitalizar_02_Reverso.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDigitalizar_02_Reverso.Name = "btnDigitalizar_02_Reverso";
            this.btnDigitalizar_02_Reverso.Size = new System.Drawing.Size(159, 36);
            this.btnDigitalizar_02_Reverso.Text = "F6 - Digitalizar reverso";
            this.btnDigitalizar_02_Reverso.Click += new System.EventHandler(this.btnDigitalizar_02_Reverso_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.cboCamaras);
            this.groupBox2.Location = new System.Drawing.Point(11, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1056, 57);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camaras";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(36, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 18);
            this.label16.TabIndex = 57;
            this.label16.Text = "Digitizar con :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCamaras
            // 
            this.cboCamaras.BackColorEnabled = System.Drawing.Color.White;
            this.cboCamaras.Data = "";
            this.cboCamaras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamaras.Filtro = " 1 = 1";
            this.cboCamaras.FormattingEnabled = true;
            this.cboCamaras.ListaItemsBusqueda = 20;
            this.cboCamaras.Location = new System.Drawing.Point(154, 19);
            this.cboCamaras.MostrarToolTip = false;
            this.cboCamaras.Name = "cboCamaras";
            this.cboCamaras.Size = new System.Drawing.Size(867, 21);
            this.cboCamaras.TabIndex = 56;
            // 
            // chkHabilitarZoom
            // 
            this.chkHabilitarZoom.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHabilitarZoom.Location = new System.Drawing.Point(702, 24);
            this.chkHabilitarZoom.Name = "chkHabilitarZoom";
            this.chkHabilitarZoom.Size = new System.Drawing.Size(342, 24);
            this.chkHabilitarZoom.TabIndex = 15;
            this.chkHabilitarZoom.Text = "Habilitar zoom";
            this.chkHabilitarZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHabilitarZoom.UseVisualStyleBackColor = true;
            this.chkHabilitarZoom.Visible = false;
            this.chkHabilitarZoom.CheckedChanged += new System.EventHandler(this.chkHabilitarZoom_CheckedChanged);
            // 
            // FrmBeneficiarios_Identificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 589);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmBeneficiarios_Identificacion";
            this.Text = "Identificación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmBeneficiarios_Identificacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.toolStripDigitalizacion.ResumeLayout(false);
            this.toolStripDigitalizacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStripDigitalizacion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDigitalizar_01_Frente;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDigitalizar_02_Reverso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scComboBoxExt cboCamaras;
        private SC_ControlsCS.scImageBox panel_01_Frontal;
        private SC_ControlsCS.scImageBox panel_02_Reverso;
        private System.Windows.Forms.CheckBox chkHabilitarZoom;
    }
}