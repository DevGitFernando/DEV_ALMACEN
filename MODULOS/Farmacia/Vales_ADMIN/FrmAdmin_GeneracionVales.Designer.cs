namespace Farmacia.Vales_ADMIN
{
    partial class FrmAdmin_GeneracionVales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdmin_GeneracionVales));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesar = new System.Windows.Forms.ToolStripButton();
            this.FrameGeneracion = new System.Windows.Forms.GroupBox();
            this.dtpFechaVigencia = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nmNumeroDeFolios = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameGeneracion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumeroDeFolios)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnProcesar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(372, 25);
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProcesar
            // 
            this.btnProcesar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesar.Image")));
            this.btnProcesar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(23, 22);
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // FrameGeneracion
            // 
            this.FrameGeneracion.Controls.Add(this.dtpFechaVigencia);
            this.FrameGeneracion.Controls.Add(this.label5);
            this.FrameGeneracion.Controls.Add(this.label4);
            this.FrameGeneracion.Controls.Add(this.nmNumeroDeFolios);
            this.FrameGeneracion.Location = new System.Drawing.Point(12, 28);
            this.FrameGeneracion.Name = "FrameGeneracion";
            this.FrameGeneracion.Size = new System.Drawing.Size(270, 104);
            this.FrameGeneracion.TabIndex = 50;
            this.FrameGeneracion.TabStop = false;
            this.FrameGeneracion.Text = "Generar folios";
            // 
            // dtpFechaVigencia
            // 
            this.dtpFechaVigencia.CustomFormat = "yyyy-MM";
            this.dtpFechaVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVigencia.Location = new System.Drawing.Point(115, 28);
            this.dtpFechaVigencia.Name = "dtpFechaVigencia";
            this.dtpFechaVigencia.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaVigencia.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(41, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 48;
            this.label5.Text = "Folios :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(41, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 46;
            this.label4.Text = "Vigencia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmNumeroDeFolios
            // 
            this.nmNumeroDeFolios.Location = new System.Drawing.Point(115, 58);
            this.nmNumeroDeFolios.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nmNumeroDeFolios.Name = "nmNumeroDeFolios";
            this.nmNumeroDeFolios.Size = new System.Drawing.Size(78, 20);
            this.nmNumeroDeFolios.TabIndex = 4;
            this.nmNumeroDeFolios.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmAdmin_GeneracionVales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 181);
            this.Controls.Add(this.FrameGeneracion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmAdmin_GeneracionVales";
            this.Text = "Integración de Folios para Vales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmAdmin_GeneracionVales_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameGeneracion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmNumeroDeFolios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnProcesar;
        private System.Windows.Forms.GroupBox FrameGeneracion;
        private System.Windows.Forms.DateTimePicker dtpFechaVigencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmNumeroDeFolios;
    }
}