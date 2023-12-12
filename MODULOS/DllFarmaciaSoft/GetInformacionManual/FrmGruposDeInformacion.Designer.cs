namespace DllFarmaciaSoft.GetInformacionManual
{
    partial class FrmGruposDeInformacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGruposDeInformacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnObtenerTransferencias = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDescargarImagenes = new System.Windows.Forms.ToolStripButton();
            this.toolStriplblResultado = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.FrmAvanze = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.grdCatalogos = new FarPoint.Win.Spread.FpSpread();
            this.grdCatalogos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameOrigenDatos = new System.Windows.Forms.GroupBox();
            this.chkCatalogoGeneral = new System.Windows.Forms.CheckBox();
            this.rdoSvrCentral = new System.Windows.Forms.RadioButton();
            this.rdoSvrRegional = new System.Windows.Forms.RadioButton();
            this.lblAvance = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrmAvanze.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos_Sheet1)).BeginInit();
            this.FrameOrigenDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnObtenerTransferencias,
            this.toolStripSeparator2,
            this.btnDescargarImagenes,
            this.toolStriplblResultado,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(644, 25);
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
            this.btnEjecutar.Text = "Obtener lista de catálogos";
            this.btnEjecutar.ToolTipText = "Obtener lista de catálogos";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnObtenerTransferencias
            // 
            this.btnObtenerTransferencias.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnObtenerTransferencias.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerTransferencias.Image")));
            this.btnObtenerTransferencias.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnObtenerTransferencias.Name = "btnObtenerTransferencias";
            this.btnObtenerTransferencias.Size = new System.Drawing.Size(23, 22);
            this.btnObtenerTransferencias.Text = "Descargar información";
            this.btnObtenerTransferencias.Click += new System.EventHandler(this.btnObtenerTransferencias_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDescargarImagenes
            // 
            this.btnDescargarImagenes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDescargarImagenes.Image = ((System.Drawing.Image)(resources.GetObject("btnDescargarImagenes.Image")));
            this.btnDescargarImagenes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDescargarImagenes.Name = "btnDescargarImagenes";
            this.btnDescargarImagenes.Size = new System.Drawing.Size(117, 22);
            this.btnDescargarImagenes.Text = "Descargar imagenes";
            this.btnDescargarImagenes.Click += new System.EventHandler(this.btnDescargarImagenes_Click);
            // 
            // toolStriplblResultado
            // 
            this.toolStriplblResultado.Name = "toolStriplblResultado";
            this.toolStriplblResultado.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkTodas);
            this.groupBox1.Controls.Add(this.FrmAvanze);
            this.groupBox1.Controls.Add(this.grdCatalogos);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 477);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Catalogos";
            // 
            // chkTodas
            // 
            this.chkTodas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.Location = new System.Drawing.Point(452, 0);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(159, 17);
            this.chkTodas.TabIndex = 2;
            this.chkTodas.Text = "Marcar / Desmarcar todos";
            this.chkTodas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // FrmAvanze
            // 
            this.FrmAvanze.Controls.Add(this.lblAvance);
            this.FrmAvanze.Controls.Add(this.progressBar1);
            this.FrmAvanze.Location = new System.Drawing.Point(76, 212);
            this.FrmAvanze.Name = "FrmAvanze";
            this.FrmAvanze.Size = new System.Drawing.Size(472, 60);
            this.FrmAvanze.TabIndex = 1;
            this.FrmAvanze.TabStop = false;
            this.FrmAvanze.Text = "Descargando información de catálogos";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(16, 21);
            this.progressBar1.MarqueeAnimationSpeed = 35;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(441, 18);
            this.progressBar1.Step = 50;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // grdCatalogos
            // 
            this.grdCatalogos.AccessibleDescription = "grdCatalogos, Sheet1, Row 0, Column 0, ";
            this.grdCatalogos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCatalogos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdCatalogos.Location = new System.Drawing.Point(9, 19);
            this.grdCatalogos.Name = "grdCatalogos";
            this.grdCatalogos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdCatalogos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdCatalogos_Sheet1});
            this.grdCatalogos.Size = new System.Drawing.Size(605, 450);
            this.grdCatalogos.TabIndex = 0;
            // 
            // grdCatalogos_Sheet1
            // 
            this.grdCatalogos_Sheet1.Reset();
            this.grdCatalogos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdCatalogos_Sheet1.ColumnCount = 3;
            this.grdCatalogos_Sheet1.RowCount = 5;
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Orden";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Catálogo";
            this.grdCatalogos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Procesar";
            this.grdCatalogos_Sheet1.Columns.Get(0).CellType = textCellType3;
            this.grdCatalogos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(0).Label = "Orden";
            this.grdCatalogos_Sheet1.Columns.Get(0).Locked = true;
            this.grdCatalogos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(0).Width = 70F;
            this.grdCatalogos_Sheet1.Columns.Get(1).CellType = textCellType4;
            this.grdCatalogos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdCatalogos_Sheet1.Columns.Get(1).Label = "Catálogo";
            this.grdCatalogos_Sheet1.Columns.Get(1).Locked = true;
            this.grdCatalogos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(1).Width = 400F;
            this.grdCatalogos_Sheet1.Columns.Get(2).CellType = checkBoxCellType2;
            this.grdCatalogos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(2).Label = "Procesar";
            this.grdCatalogos_Sheet1.Columns.Get(2).Locked = false;
            this.grdCatalogos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdCatalogos_Sheet1.Columns.Get(2).Width = 75F;
            this.grdCatalogos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdCatalogos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameOrigenDatos
            // 
            this.FrameOrigenDatos.Controls.Add(this.chkCatalogoGeneral);
            this.FrameOrigenDatos.Controls.Add(this.rdoSvrCentral);
            this.FrameOrigenDatos.Controls.Add(this.rdoSvrRegional);
            this.FrameOrigenDatos.Location = new System.Drawing.Point(12, 28);
            this.FrameOrigenDatos.Name = "FrameOrigenDatos";
            this.FrameOrigenDatos.Size = new System.Drawing.Size(621, 46);
            this.FrameOrigenDatos.TabIndex = 1;
            this.FrameOrigenDatos.TabStop = false;
            this.FrameOrigenDatos.Text = "Origen de datos";
            // 
            // chkCatalogoGeneral
            // 
            this.chkCatalogoGeneral.Location = new System.Drawing.Point(371, 19);
            this.chkCatalogoGeneral.Name = "chkCatalogoGeneral";
            this.chkCatalogoGeneral.Size = new System.Drawing.Size(169, 17);
            this.chkCatalogoGeneral.TabIndex = 3;
            this.chkCatalogoGeneral.Text = "Descargar catálogo general";
            this.chkCatalogoGeneral.UseVisualStyleBackColor = true;
            // 
            // rdoSvrCentral
            // 
            this.rdoSvrCentral.Location = new System.Drawing.Point(81, 19);
            this.rdoSvrCentral.Name = "rdoSvrCentral";
            this.rdoSvrCentral.Size = new System.Drawing.Size(117, 17);
            this.rdoSvrCentral.TabIndex = 0;
            this.rdoSvrCentral.TabStop = true;
            this.rdoSvrCentral.Text = "Servidor Central";
            this.rdoSvrCentral.UseVisualStyleBackColor = true;
            // 
            // rdoSvrRegional
            // 
            this.rdoSvrRegional.Location = new System.Drawing.Point(220, 19);
            this.rdoSvrRegional.Name = "rdoSvrRegional";
            this.rdoSvrRegional.Size = new System.Drawing.Size(117, 17);
            this.rdoSvrRegional.TabIndex = 1;
            this.rdoSvrRegional.TabStop = true;
            this.rdoSvrRegional.Text = "Servidor Regional";
            this.rdoSvrRegional.UseVisualStyleBackColor = true;
            // 
            // lblAvance
            // 
            this.lblAvance.Location = new System.Drawing.Point(16, 42);
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(441, 12);
            this.lblAvance.TabIndex = 3;
            this.lblAvance.Text = "Desde :";
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvance.Visible = false;
            // 
            // FrmGruposDeInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 561);
            this.Controls.Add(this.FrameOrigenDatos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGruposDeInformacion";
            this.Text = "Solicitar información de Catalogos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposDeInformacion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGruposDeInformacion_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrmAvanze.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCatalogos_Sheet1)).EndInit();
            this.FrameOrigenDatos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnObtenerTransferencias;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdCatalogos;
        private FarPoint.Win.Spread.SheetView grdCatalogos_Sheet1;
        private System.Windows.Forms.GroupBox FrameOrigenDatos;
        private System.Windows.Forms.RadioButton rdoSvrCentral;
        private System.Windows.Forms.RadioButton rdoSvrRegional;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.GroupBox FrmAvanze;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkCatalogoGeneral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStriplblResultado;
        private System.Windows.Forms.ToolStripButton btnDescargarImagenes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lblAvance;
    }
}