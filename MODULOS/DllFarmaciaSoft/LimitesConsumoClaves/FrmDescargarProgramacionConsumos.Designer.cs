namespace DllFarmaciaSoft.LimitesConsumoClaves
{
    partial class FrmDescargarProgramacionConsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDescargarProgramacionConsumos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnObtenerImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStriplblResultado = new System.Windows.Forms.ToolStripLabel();
            this.lblProceso = new System.Windows.Forms.Label();
            this.FrameOrigenDatos = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoSvrCentral = new System.Windows.Forms.RadioButton();
            this.rdoSvrRegional = new System.Windows.Forms.RadioButton();
            this.nmAño = new System.Windows.Forms.NumericUpDown();
            this.nmMes = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameOrigenDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmAño)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMes)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnObtenerImagenes,
            this.toolStripSeparator2,
            this.toolStriplblResultado});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(452, 25);
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
            // btnObtenerImagenes
            // 
            this.btnObtenerImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerImagenes.Image")));
            this.btnObtenerImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerImagenes.Name = "btnObtenerImagenes";
            this.btnObtenerImagenes.Size = new System.Drawing.Size(23, 22);
            this.btnObtenerImagenes.Text = "Descargar información";
            this.btnObtenerImagenes.Click += new System.EventHandler(this.btnObtenerImagenes_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStriplblResultado
            // 
            this.toolStriplblResultado.Name = "toolStriplblResultado";
            this.toolStriplblResultado.Size = new System.Drawing.Size(0, 22);
            // 
            // lblProceso
            // 
            this.lblProceso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProceso.Location = new System.Drawing.Point(80, 4);
            this.lblProceso.Name = "lblProceso";
            this.lblProceso.Size = new System.Drawing.Size(361, 18);
            this.lblProceso.TabIndex = 2;
            this.lblProceso.Text = "label3";
            this.lblProceso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProceso.EnabledChanged += new System.EventHandler(this.lblProceso_EnabledChanged);
            // 
            // FrameOrigenDatos
            // 
            this.FrameOrigenDatos.Controls.Add(this.label2);
            this.FrameOrigenDatos.Controls.Add(this.label1);
            this.FrameOrigenDatos.Controls.Add(this.nmMes);
            this.FrameOrigenDatos.Controls.Add(this.nmAño);
            this.FrameOrigenDatos.Location = new System.Drawing.Point(9, 73);
            this.FrameOrigenDatos.Name = "FrameOrigenDatos";
            this.FrameOrigenDatos.Size = new System.Drawing.Size(434, 52);
            this.FrameOrigenDatos.TabIndex = 2;
            this.FrameOrigenDatos.TabStop = false;
            this.FrameOrigenDatos.Text = "Periodo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoSvrCentral);
            this.groupBox1.Controls.Add(this.rdoSvrRegional);
            this.groupBox1.Location = new System.Drawing.Point(9, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 46);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Origen de datos";
            // 
            // rdoSvrCentral
            // 
            this.rdoSvrCentral.Checked = true;
            this.rdoSvrCentral.Location = new System.Drawing.Point(77, 19);
            this.rdoSvrCentral.Name = "rdoSvrCentral";
            this.rdoSvrCentral.Size = new System.Drawing.Size(117, 17);
            this.rdoSvrCentral.TabIndex = 0;
            this.rdoSvrCentral.TabStop = true;
            this.rdoSvrCentral.Text = "Servidor Central";
            this.rdoSvrCentral.UseVisualStyleBackColor = true;
            // 
            // rdoSvrRegional
            // 
            this.rdoSvrRegional.Location = new System.Drawing.Point(240, 19);
            this.rdoSvrRegional.Name = "rdoSvrRegional";
            this.rdoSvrRegional.Size = new System.Drawing.Size(117, 17);
            this.rdoSvrRegional.TabIndex = 1;
            this.rdoSvrRegional.TabStop = true;
            this.rdoSvrRegional.Text = "Servidor Regional";
            this.rdoSvrRegional.UseVisualStyleBackColor = true;
            // 
            // nmAño
            // 
            this.nmAño.Location = new System.Drawing.Point(137, 23);
            this.nmAño.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.nmAño.Minimum = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            this.nmAño.Name = "nmAño";
            this.nmAño.Size = new System.Drawing.Size(70, 20);
            this.nmAño.TabIndex = 0;
            this.nmAño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmAño.Value = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            // 
            // nmMes
            // 
            this.nmMes.Location = new System.Drawing.Point(285, 22);
            this.nmMes.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmMes.Name = "nmMes";
            this.nmMes.Size = new System.Drawing.Size(69, 20);
            this.nmMes.TabIndex = 1;
            this.nmMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(81, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Año :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(232, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mes :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDescargarProgramacionConsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 134);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProceso);
            this.Controls.Add(this.FrameOrigenDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDescargarProgramacionConsumos";
            this.Text = "Descarga de Programación de Consumo Mensual";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDescargarProgramacionConsumos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameOrigenDatos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmAño)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmMes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnObtenerImagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStriplblResultado;
        private System.Windows.Forms.Label lblProceso;
        private System.Windows.Forms.GroupBox FrameOrigenDatos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoSvrCentral;
        private System.Windows.Forms.RadioButton rdoSvrRegional;
        private System.Windows.Forms.NumericUpDown nmMes;
        private System.Windows.Forms.NumericUpDown nmAño;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}