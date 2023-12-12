namespace Farmacia.Inventario.Kardex
{
    partial class FrmKardexPorUbicacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKardexPorUbicacion));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEntrepaño = new System.Windows.Forms.Label();
            this.txtEntrepaño = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEstante = new System.Windows.Forms.Label();
            this.txtEstante = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPasillo = new System.Windows.Forms.Label();
            this.txtPasillo = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpFechaFinal);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.dtpFechaInicial);
            this.groupBox5.Location = new System.Drawing.Point(7, 205);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(575, 66);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rango Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(363, 27);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(320, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(91, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(148, 27);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(120, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEntrepaño);
            this.groupBox1.Controls.Add(this.txtEntrepaño);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblEstante);
            this.groupBox1.Controls.Add(this.txtEstante);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPasillo);
            this.groupBox1.Controls.Add(this.txtPasillo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(7, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(575, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ubicación";
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.Location = new System.Drawing.Point(211, 95);
            this.lblEntrepaño.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(347, 25);
            this.lblEntrepaño.TabIndex = 56;
            // 
            // txtEntrepaño
            // 
            this.txtEntrepaño.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntrepaño.Decimales = 2;
            this.txtEntrepaño.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntrepaño.ForeColor = System.Drawing.Color.Black;
            this.txtEntrepaño.Location = new System.Drawing.Point(124, 95);
            this.txtEntrepaño.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEntrepaño.MaxLength = 4;
            this.txtEntrepaño.Name = "txtEntrepaño";
            this.txtEntrepaño.PermitirApostrofo = false;
            this.txtEntrepaño.PermitirNegativos = false;
            this.txtEntrepaño.Size = new System.Drawing.Size(77, 22);
            this.txtEntrepaño.TabIndex = 2;
            this.txtEntrepaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEntrepaño.TextChanged += new System.EventHandler(this.txtEntrepaño_TextChanged);
            this.txtEntrepaño.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEntrepaño_KeyDown);
            this.txtEntrepaño.Validating += new System.ComponentModel.CancelEventHandler(this.txtEntrepaño_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "Posición :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstante
            // 
            this.lblEstante.Location = new System.Drawing.Point(211, 64);
            this.lblEstante.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(347, 25);
            this.lblEstante.TabIndex = 53;
            // 
            // txtEstante
            // 
            this.txtEstante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstante.Decimales = 2;
            this.txtEstante.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstante.ForeColor = System.Drawing.Color.Black;
            this.txtEstante.Location = new System.Drawing.Point(124, 64);
            this.txtEstante.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEstante.MaxLength = 4;
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.PermitirApostrofo = false;
            this.txtEstante.PermitirNegativos = false;
            this.txtEstante.Size = new System.Drawing.Size(77, 22);
            this.txtEstante.TabIndex = 1;
            this.txtEstante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstante.TextChanged += new System.EventHandler(this.txtEstante_TextChanged);
            this.txtEstante.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEstante_KeyDown);
            this.txtEstante.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstante_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(55, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 52;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasillo
            // 
            this.lblPasillo.Location = new System.Drawing.Point(211, 33);
            this.lblPasillo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(347, 25);
            this.lblPasillo.TabIndex = 50;
            // 
            // txtPasillo
            // 
            this.txtPasillo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasillo.Decimales = 2;
            this.txtPasillo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPasillo.ForeColor = System.Drawing.Color.Black;
            this.txtPasillo.Location = new System.Drawing.Point(124, 33);
            this.txtPasillo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPasillo.MaxLength = 4;
            this.txtPasillo.Name = "txtPasillo";
            this.txtPasillo.PermitirApostrofo = false;
            this.txtPasillo.PermitirNegativos = false;
            this.txtPasillo.Size = new System.Drawing.Size(77, 22);
            this.txtPasillo.TabIndex = 0;
            this.txtPasillo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasillo.TextChanged += new System.EventHandler(this.txtPasillo_TextChanged);
            this.txtPasillo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasillo_KeyDown);
            this.txtPasillo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasillo_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(55, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 20);
            this.label8.TabIndex = 49;
            this.label8.Text = "Rack :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(593, 58);
            this.toolStripBarraMenu.TabIndex = 5;
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
            this.toolStripSeparator.Size = new System.Drawing.Size(12, 58);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Enabled = false;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(54, 55);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(54, 55);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(54, 55);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(12, 58);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(12, 58);
            // 
            // FrmKardexPorUbicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 281);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmKardexPorUbicacion";
            this.ShowIcon = false;
            this.Text = "Kardex de Ubicación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmKardexPorUbicacion_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEntrepaño;
        private SC_ControlsCS.scTextBoxExt txtEntrepaño;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEstante;
        private SC_ControlsCS.scTextBoxExt txtEstante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPasillo;
        private SC_ControlsCS.scTextBoxExt txtPasillo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}