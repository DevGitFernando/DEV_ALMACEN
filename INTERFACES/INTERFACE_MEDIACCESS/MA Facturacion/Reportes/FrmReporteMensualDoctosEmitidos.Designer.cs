namespace MA_Facturacion.Reportes
{
    partial class FrmReporteMensualDoctosEmitidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteMensualDoctosEmitidos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarPDFs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarXMLs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMes = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpAño = new System.Windows.Forms.DateTimePicker();
            this.chkReporteGeneral = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnExportar,
            this.toolStripSeparator4,
            this.btnDescargarPDFs,
            this.toolStripSeparator1,
            this.btnDescargarXMLs,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(370, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar a excel";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDescargarPDFs
            // 
            this.btnDescargarPDFs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDescargarPDFs.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarPDFs.Image")));
            this.btnDescargarPDFs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarPDFs.Name = "btnDescargarPDFs";
            this.btnDescargarPDFs.Size = new System.Drawing.Size(23, 22);
            this.btnDescargarPDFs.Text = "Descargar PDF\'s";
            this.btnDescargarPDFs.Click += new System.EventHandler(this.btnDescargarPDFs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDescargarXMLs
            // 
            this.btnDescargarXMLs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDescargarXMLs.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarXMLs.Image")));
            this.btnDescargarXMLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarXMLs.Name = "btnDescargarXMLs";
            this.btnDescargarXMLs.Size = new System.Drawing.Size(23, 22);
            this.btnDescargarXMLs.Text = "Descargar XMLs";
            this.btnDescargarXMLs.Click += new System.EventHandler(this.btnDescargarXMLs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkReporteGeneral);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboMes);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dtpAño);
            this.groupBox3.Location = new System.Drawing.Point(12, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 53);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Periodo";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Mes : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMes
            // 
            this.cboMes.BackColorEnabled = System.Drawing.Color.White;
            this.cboMes.Data = "";
            this.cboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMes.Filtro = " 1 = 1";
            this.cboMes.FormattingEnabled = true;
            this.cboMes.ListaItemsBusqueda = 20;
            this.cboMes.Location = new System.Drawing.Point(210, 22);
            this.cboMes.MostrarToolTip = false;
            this.cboMes.Name = "cboMes";
            this.cboMes.Size = new System.Drawing.Size(110, 21);
            this.cboMes.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Año : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpAño
            // 
            this.dtpAño.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpAño.CustomFormat = "yyyy";
            this.dtpAño.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAño.Location = new System.Drawing.Point(78, 23);
            this.dtpAño.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpAño.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dtpAño.Name = "dtpAño";
            this.dtpAño.ShowUpDown = true;
            this.dtpAño.Size = new System.Drawing.Size(58, 20);
            this.dtpAño.TabIndex = 0;
            // 
            // chkReporteGeneral
            // 
            this.chkReporteGeneral.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReporteGeneral.Location = new System.Drawing.Point(210, 0);
            this.chkReporteGeneral.Name = "chkReporteGeneral";
            this.chkReporteGeneral.Size = new System.Drawing.Size(134, 17);
            this.chkReporteGeneral.TabIndex = 25;
            this.chkReporteGeneral.Text = "Información general";
            this.chkReporteGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReporteGeneral.UseVisualStyleBackColor = true;
            // 
            // FrmReporteMensualDoctosEmitidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 89);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReporteMensualDoctosEmitidos";
            this.Text = "Reporte mensual de documentos emitidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReporteMensualDoctosEmitidos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpAño;
        private SC_ControlsCS.scComboBoxExt cboMes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnDescargarPDFs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDescargarXMLs;
        private System.Windows.Forms.CheckBox chkReporteGeneral;
    }
}