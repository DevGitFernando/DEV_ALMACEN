namespace Farmacia.Procesos
{
    partial class FrmReeimpresionCorteDiario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReeimpresionCorteDiario));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFechaSistema = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoDetDisp = new System.Windows.Forms.RadioButton();
            this.rdoTiraDeAuditoria = new System.Windows.Forms.RadioButton();
            this.rdoCorteDiario = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFechaSistema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Corte del Día";
            // 
            // dtpFechaSistema
            // 
            this.dtpFechaSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaSistema.Enabled = false;
            this.dtpFechaSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaSistema.Location = new System.Drawing.Point(236, 17);
            this.dtpFechaSistema.Name = "dtpFechaSistema";
            this.dtpFechaSistema.Size = new System.Drawing.Size(97, 20);
            this.dtpFechaSistema.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(130, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha de Sistema :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(485, 25);
            this.toolStripBarraMenu.TabIndex = 9;
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoDetDisp);
            this.groupBox2.Controls.Add(this.rdoTiraDeAuditoria);
            this.groupBox2.Controls.Add(this.rdoCorteDiario);
            this.groupBox2.Location = new System.Drawing.Point(10, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 52);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Reporte";
            // 
            // rdoDetDisp
            // 
            this.rdoDetDisp.Location = new System.Drawing.Point(289, 19);
            this.rdoDetDisp.Name = "rdoDetDisp";
            this.rdoDetDisp.Size = new System.Drawing.Size(125, 17);
            this.rdoDetDisp.TabIndex = 3;
            this.rdoDetDisp.TabStop = true;
            this.rdoDetDisp.Text = "Detalle Dispensación";
            this.rdoDetDisp.UseVisualStyleBackColor = true;
            // 
            // rdoTiraDeAuditoria
            // 
            this.rdoTiraDeAuditoria.Location = new System.Drawing.Point(163, 19);
            this.rdoTiraDeAuditoria.Name = "rdoTiraDeAuditoria";
            this.rdoTiraDeAuditoria.Size = new System.Drawing.Size(125, 17);
            this.rdoTiraDeAuditoria.TabIndex = 1;
            this.rdoTiraDeAuditoria.TabStop = true;
            this.rdoTiraDeAuditoria.Text = "Tira de Auditoría";
            this.rdoTiraDeAuditoria.UseVisualStyleBackColor = true;
            // 
            // rdoCorteDiario
            // 
            this.rdoCorteDiario.Checked = true;
            this.rdoCorteDiario.Location = new System.Drawing.Point(37, 19);
            this.rdoCorteDiario.Name = "rdoCorteDiario";
            this.rdoCorteDiario.Size = new System.Drawing.Size(125, 17);
            this.rdoCorteDiario.TabIndex = 0;
            this.rdoCorteDiario.TabStop = true;
            this.rdoCorteDiario.Text = "Corte Diario";
            this.rdoCorteDiario.UseVisualStyleBackColor = true;
            // 
            // FrmReeimpresionCorteDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 140);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmReeimpresionCorteDiario";
            this.Text = "Reimpresión Corte Diario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReeimpresionCorteDiario_Load);
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaSistema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoCorteDiario;
        private System.Windows.Forms.RadioButton rdoTiraDeAuditoria;
        private System.Windows.Forms.RadioButton rdoDetDisp;
    }
}