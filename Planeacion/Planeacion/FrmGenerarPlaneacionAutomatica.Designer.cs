namespace Planeacion.ObtenerInformacion
{
    partial class FrmGenerarPlaneacionAutomatica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarPlaneacionAutomatica));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnAutorizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.grVta = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nmBaja = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nmMedia = new System.Windows.Forms.NumericUpDown();
            this.lblMesesCaducidad = new System.Windows.Forms.Label();
            this.lblMeses = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nmDiasStockSeguridasd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nmAlta = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.grVta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasStockSeguridasd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAlta)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnAutorizar,
            this.toolStripSeparator5,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(581, 25);
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
            // btnAutorizar
            // 
            this.btnAutorizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutorizar.Image = ((System.Drawing.Image)(resources.GetObject("btnAutorizar.Image")));
            this.btnAutorizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(23, 22);
            this.btnAutorizar.Text = "Autorizar";
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Visible = false;
            // 
            // grVta
            // 
            this.grVta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grVta.Controls.Add(this.label5);
            this.grVta.Controls.Add(this.nmBaja);
            this.grVta.Controls.Add(this.label1);
            this.grVta.Controls.Add(this.nmMedia);
            this.grVta.Controls.Add(this.lblMesesCaducidad);
            this.grVta.Controls.Add(this.lblMeses);
            this.grVta.Controls.Add(this.label3);
            this.grVta.Controls.Add(this.label4);
            this.grVta.Controls.Add(this.nmDiasStockSeguridasd);
            this.grVta.Controls.Add(this.label2);
            this.grVta.Controls.Add(this.label6);
            this.grVta.Controls.Add(this.nmAlta);
            this.grVta.Location = new System.Drawing.Point(11, 28);
            this.grVta.Name = "grVta";
            this.grVta.Size = new System.Drawing.Size(558, 138);
            this.grVta.TabIndex = 1;
            this.grVta.TabStop = false;
            this.grVta.Text = "Datos generales de la Planeación";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 14);
            this.label5.TabIndex = 66;
            this.label5.Text = "Meses de stock de Baja rotación :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmBaja
            // 
            this.nmBaja.Location = new System.Drawing.Point(196, 78);
            this.nmBaja.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmBaja.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nmBaja.Name = "nmBaja";
            this.nmBaja.Size = new System.Drawing.Size(58, 20);
            this.nmBaja.TabIndex = 65;
            this.nmBaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmBaja.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmBaja.ValueChanged += new System.EventHandler(this.nmBaja_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "Meses de stock de Media rotación :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMedia
            // 
            this.nmMedia.Location = new System.Drawing.Point(196, 50);
            this.nmMedia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmMedia.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nmMedia.Name = "nmMedia";
            this.nmMedia.Size = new System.Drawing.Size(58, 20);
            this.nmMedia.TabIndex = 63;
            this.nmMedia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMedia.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmMedia.ValueChanged += new System.EventHandler(this.nmMedia_ValueChanged);
            // 
            // lblMesesCaducidad
            // 
            this.lblMesesCaducidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMesesCaducidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMesesCaducidad.Location = new System.Drawing.Point(486, 22);
            this.lblMesesCaducidad.Name = "lblMesesCaducidad";
            this.lblMesesCaducidad.Size = new System.Drawing.Size(56, 20);
            this.lblMesesCaducidad.TabIndex = 62;
            this.lblMesesCaducidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMeses
            // 
            this.lblMeses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMeses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMeses.Location = new System.Drawing.Point(486, 46);
            this.lblMeses.Name = "lblMeses";
            this.lblMeses.Size = new System.Drawing.Size(56, 20);
            this.lblMeses.TabIndex = 61;
            this.lblMeses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(308, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 14);
            this.label3.TabIndex = 60;
            this.label3.Text = "meses de información historica :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(49, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 14);
            this.label4.TabIndex = 58;
            this.label4.Text = "Dias de stock de seguridad:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmDiasStockSeguridasd
            // 
            this.nmDiasStockSeguridasd.Location = new System.Drawing.Point(196, 106);
            this.nmDiasStockSeguridasd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmDiasStockSeguridasd.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nmDiasStockSeguridasd.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmDiasStockSeguridasd.Name = "nmDiasStockSeguridasd";
            this.nmDiasStockSeguridasd.Size = new System.Drawing.Size(58, 20);
            this.nmDiasStockSeguridasd.TabIndex = 57;
            this.nmDiasStockSeguridasd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmDiasStockSeguridasd.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(308, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 14);
            this.label2.TabIndex = 54;
            this.label2.Text = "Meses de caducidad a validar :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 14);
            this.label6.TabIndex = 52;
            this.label6.Text = "Meses de stock de Alta rotación :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmAlta
            // 
            this.nmAlta.Location = new System.Drawing.Point(196, 22);
            this.nmAlta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmAlta.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nmAlta.Name = "nmAlta";
            this.nmAlta.Size = new System.Drawing.Size(58, 20);
            this.nmAlta.TabIndex = 2;
            this.nmAlta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmAlta.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmAlta.ValueChanged += new System.EventHandler(this.nmAlta_ValueChanged);
            // 
            // FrmGenerarPlaneacionAutomatica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 173);
            this.Controls.Add(this.grVta);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGenerarPlaneacionAutomatica";
            this.Text = "Registro de planeación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRegistroDePlaneacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grVta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmBaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasStockSeguridasd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAlta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnAutorizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox grVta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmAlta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmDiasStockSeguridasd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMesesCaducidad;
        private System.Windows.Forms.Label lblMeses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nmBaja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmMedia;
    }
}