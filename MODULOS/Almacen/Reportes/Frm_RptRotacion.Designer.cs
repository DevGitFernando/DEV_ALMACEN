namespace Almacen.Reportes
{
    partial class Frm_RptRotacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_RptRotacion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.lstResultado = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.nmDiasAnalisis = new System.Windows.Forms.NumericUpDown();
            this.cboTipoReporte = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoNoCB = new System.Windows.Forms.RadioButton();
            this.rdoCB = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nmBaja = new System.Windows.Forms.NumericUpDown();
            this.nmMedia = new System.Windows.Forms.NumericUpDown();
            this.nmAlta = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasAnalisis)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmBaja)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAlta)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameClaves
            // 
            this.FrameClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameClaves.Controls.Add(this.lstResultado);
            this.FrameClaves.Location = new System.Drawing.Point(7, 122);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(969, 429);
            this.FrameClaves.TabIndex = 4;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Productos";
            // 
            // lstResultado
            // 
            this.lstResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultado.Location = new System.Drawing.Point(10, 16);
            this.lstResultado.Name = "lstResultado";
            this.lstResultado.Size = new System.Drawing.Size(949, 404);
            this.lstResultado.TabIndex = 0;
            this.lstResultado.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dtpFechaInicial);
            this.groupBox2.Controls.Add(this.nmDiasAnalisis);
            this.groupBox2.Controls.Add(this.cboTipoReporte);
            this.groupBox2.Location = new System.Drawing.Point(8, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 92);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Reporte";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "Días de Análisis :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Tipo de Reporte :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Fecha de Revisión :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(121, 43);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(199, 20);
            this.dtpFechaInicial.TabIndex = 1;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // nmDiasAnalisis
            // 
            this.nmDiasAnalisis.Location = new System.Drawing.Point(121, 67);
            this.nmDiasAnalisis.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nmDiasAnalisis.Name = "nmDiasAnalisis";
            this.nmDiasAnalisis.Size = new System.Drawing.Size(75, 20);
            this.nmDiasAnalisis.TabIndex = 2;
            this.nmDiasAnalisis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboTipoReporte
            // 
            this.cboTipoReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoReporte.Data = "";
            this.cboTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoReporte.Filtro = " 1 = 1";
            this.cboTipoReporte.FormattingEnabled = true;
            this.cboTipoReporte.ListaItemsBusqueda = 20;
            this.cboTipoReporte.Location = new System.Drawing.Point(121, 16);
            this.cboTipoReporte.MostrarToolTip = false;
            this.cboTipoReporte.Name = "cboTipoReporte";
            this.cboTipoReporte.Size = new System.Drawing.Size(199, 21);
            this.cboTipoReporte.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoAmbos);
            this.groupBox1.Controls.Add(this.rdoNoCB);
            this.groupBox1.Controls.Add(this.rdoCB);
            this.groupBox1.Location = new System.Drawing.Point(354, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 91);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de Claves";
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Checked = true;
            this.rdoAmbos.Location = new System.Drawing.Point(26, 19);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(95, 18);
            this.rdoAmbos.TabIndex = 0;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoNoCB
            // 
            this.rdoNoCB.Location = new System.Drawing.Point(26, 65);
            this.rdoNoCB.Name = "rdoNoCB";
            this.rdoNoCB.Size = new System.Drawing.Size(118, 18);
            this.rdoNoCB.TabIndex = 2;
            this.rdoNoCB.Text = "No Cuadro Básico";
            this.rdoNoCB.UseVisualStyleBackColor = true;
            // 
            // rdoCB
            // 
            this.rdoCB.Location = new System.Drawing.Point(26, 42);
            this.rdoCB.Name = "rdoCB";
            this.rdoCB.Size = new System.Drawing.Size(95, 18);
            this.rdoCB.TabIndex = 1;
            this.rdoCB.Text = "Cuadro Básico";
            this.rdoCB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nmBaja);
            this.groupBox3.Controls.Add(this.nmMedia);
            this.groupBox3.Controls.Add(this.nmAlta);
            this.groupBox3.Location = new System.Drawing.Point(529, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 92);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Porcentajes para Calcular Rotación";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(311, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Baja :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(172, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Media :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Alta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmBaja
            // 
            this.nmBaja.Location = new System.Drawing.Point(359, 36);
            this.nmBaja.Name = "nmBaja";
            this.nmBaja.Size = new System.Drawing.Size(64, 20);
            this.nmBaja.TabIndex = 2;
            this.nmBaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmMedia
            // 
            this.nmMedia.Location = new System.Drawing.Point(220, 36);
            this.nmMedia.Name = "nmMedia";
            this.nmMedia.Size = new System.Drawing.Size(64, 20);
            this.nmMedia.TabIndex = 1;
            this.nmMedia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nmAlta
            // 
            this.nmAlta.Location = new System.Drawing.Point(71, 36);
            this.nmAlta.Name = "nmAlta";
            this.nmAlta.Size = new System.Drawing.Size(64, 20);
            this.nmAlta.TabIndex = 0;
            this.nmAlta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Frm_RptRotacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "Frm_RptRotacion";
            this.Text = "Reporte Rotación de Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.Frm_RptRotacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameClaves.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasAnalisis)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmBaja)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmAlta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.ListView lstResultado;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scComboBoxExt cboTipoReporte;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoNoCB;
        private System.Windows.Forms.RadioButton rdoCB;
        private System.Windows.Forms.RadioButton rdoAmbos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmDiasAnalisis;
        private System.Windows.Forms.NumericUpDown nmBaja;
        private System.Windows.Forms.NumericUpDown nmMedia;
        private System.Windows.Forms.NumericUpDown nmAlta;
        private System.Windows.Forms.Label label6;
    }
}