namespace DllRecetaElectronica.Catalogos
{
    partial class FrmAMPM_FirmasDigitales_Medicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAMPM_FirmasDigitales_Medicos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCargarFirma = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator_05 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.FrameFirmaDigital = new System.Windows.Forms.GroupBox();
            this.lblDirectorioFirma = new SC_ControlsCS.scLabelExt();
            this.picFirma = new SC_ControlsCS.scImageBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.FrameFirmaDigital.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCargarFirma,
            this.toolStripSeparator_05});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(648, 25);
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
            // btnCargarFirma
            // 
            this.btnCargarFirma.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCargarFirma.Image = ((System.Drawing.Image)(resources.GetObject("btnCargarFirma.Image")));
            this.btnCargarFirma.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCargarFirma.Name = "btnCargarFirma";
            this.btnCargarFirma.Size = new System.Drawing.Size(23, 22);
            this.btnCargarFirma.Text = "Recetas electrónicas";
            this.btnCargarFirma.Click += new System.EventHandler(this.btnCargarFirma_Click);
            // 
            // toolStripSeparator_05
            // 
            this.toolStripSeparator_05.Name = "toolStripSeparator_05";
            this.toolStripSeparator_05.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lblDirectorioFirma);
            this.FrameInformacion.Location = new System.Drawing.Point(11, 34);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(627, 81);
            this.FrameInformacion.TabIndex = 6;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información de la firma";
            // 
            // FrameFirmaDigital
            // 
            this.FrameFirmaDigital.Controls.Add(this.picFirma);
            this.FrameFirmaDigital.Location = new System.Drawing.Point(11, 118);
            this.FrameFirmaDigital.Name = "FrameFirmaDigital";
            this.FrameFirmaDigital.Size = new System.Drawing.Size(627, 481);
            this.FrameFirmaDigital.TabIndex = 7;
            this.FrameFirmaDigital.TabStop = false;
            this.FrameFirmaDigital.Text = "Firma";
            // 
            // lblDirectorioFirma
            // 
            this.lblDirectorioFirma.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioFirma.Location = new System.Drawing.Point(16, 24);
            this.lblDirectorioFirma.MostrarToolTip = false;
            this.lblDirectorioFirma.Name = "lblDirectorioFirma";
            this.lblDirectorioFirma.Size = new System.Drawing.Size(600, 41);
            this.lblDirectorioFirma.TabIndex = 0;
            this.lblDirectorioFirma.Text = "scLabelExt1";
            this.lblDirectorioFirma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picFirma
            // 
            this.picFirma.AutoScroll = true;
            this.picFirma.Location = new System.Drawing.Point(13, 20);
            this.picFirma.Name = "picFirma";
            this.picFirma.Size = new System.Drawing.Size(600, 452);
            this.picFirma.TabIndex = 0;
            // 
            // FrmAMPM_FirmasDigitales_Medicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 610);
            this.Controls.Add(this.FrameFirmaDigital);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmAMPM_FirmasDigitales_Medicos";
            this.Text = "Registro de firmas digitales de médicos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAMPM_FirmasDigitales_Medicos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.FrameFirmaDigital.ResumeLayout(false);
            this.FrameFirmaDigital.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCargarFirma;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_05;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private System.Windows.Forms.GroupBox FrameFirmaDigital;
        private SC_ControlsCS.scLabelExt lblDirectorioFirma;
        private SC_ControlsCS.scImageBox picFirma;
    }
}