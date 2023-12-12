namespace Dll_SII_INadro.Informacion
{
    partial class FrmINF_IntegrarCatalogos_Unidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmINF_IntegrarCatalogos_Unidades));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarDocumento = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEsquema = new System.Windows.Forms.Button();
            this.lblDocumentoEsquema = new SC_ControlsCS.scLabelExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnIntegrarDocumento,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(695, 25);
            this.toolStripBarraMenu.TabIndex = 1;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarDocumento
            // 
            this.btnIntegrarDocumento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarDocumento.Image")));
            this.btnIntegrarDocumento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarDocumento.Name = "btnIntegrarDocumento";
            this.btnIntegrarDocumento.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarDocumento.Text = "Procesar Cuadros Básicos";
            this.btnIntegrarDocumento.Click += new System.EventHandler(this.btnIntegrarDocumento_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEsquema
            // 
            this.btnEsquema.Location = new System.Drawing.Point(642, 16);
            this.btnEsquema.Name = "btnEsquema";
            this.btnEsquema.Size = new System.Drawing.Size(27, 23);
            this.btnEsquema.TabIndex = 32;
            this.btnEsquema.Text = "...";
            this.btnEsquema.UseVisualStyleBackColor = true;
            this.btnEsquema.Click += new System.EventHandler(this.btnEsquema_Click);
            // 
            // lblDocumentoEsquema
            // 
            this.lblDocumentoEsquema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDocumentoEsquema.Location = new System.Drawing.Point(67, 16);
            this.lblDocumentoEsquema.MostrarToolTip = false;
            this.lblDocumentoEsquema.Name = "lblDocumentoEsquema";
            this.lblDocumentoEsquema.Size = new System.Drawing.Size(569, 23);
            this.lblDocumentoEsquema.TabIndex = 31;
            this.lblDocumentoEsquema.Text = "scLabelExt1";
            this.lblDocumentoEsquema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "Ruta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDocumentoEsquema);
            this.groupBox1.Controls.Add(this.btnEsquema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 49);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Archivo de datos";
            // 
            // FrmINF_IntegrarCatalogos_Unidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 84);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmINF_IntegrarCatalogos_Unidades";
            this.Text = "Integración de catálogos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmINF_IntegrarCatalogos_Unidades_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnIntegrarDocumento;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Button btnEsquema;
        private SC_ControlsCS.scLabelExt lblDocumentoEsquema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}