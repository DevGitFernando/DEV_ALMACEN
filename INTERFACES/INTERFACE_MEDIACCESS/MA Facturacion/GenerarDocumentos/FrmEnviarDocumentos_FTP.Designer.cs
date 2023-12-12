namespace MA_Facturacion.GenerarDocumentos
{
    partial class FrmEnviarDocumentos_FTP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnviarDocumentos_FTP));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.FrameDirectorioDeTrabajo = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEnviarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.scComboBoxExt1 = new SC_ControlsCS.scComboBoxExt();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.scLabelExt3 = new SC_ControlsCS.scLabelExt();
            this.FrameDirectorioDeTrabajo.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDirectorioDeTrabajo
            // 
            this.FrameDirectorioDeTrabajo.Controls.Add(this.btnDirectorio);
            this.FrameDirectorioDeTrabajo.Controls.Add(this.lblDirectorioTrabajo);
            this.FrameDirectorioDeTrabajo.Location = new System.Drawing.Point(11, 71);
            this.FrameDirectorioDeTrabajo.Name = "FrameDirectorioDeTrabajo";
            this.FrameDirectorioDeTrabajo.Size = new System.Drawing.Size(937, 45);
            this.FrameDirectorioDeTrabajo.TabIndex = 2;
            this.FrameDirectorioDeTrabajo.TabStop = false;
            this.FrameDirectorioDeTrabajo.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(901, 15);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(26, 23);
            this.btnDirectorio.TabIndex = 1;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(11, 16);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(884, 21);
            this.lblDirectorioTrabajo.TabIndex = 0;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.FrameProceso);
            this.FrameUnidades.Controls.Add(this.chkMarcarDesmarcar);
            this.FrameUnidades.Controls.Add(this.grdUnidades);
            this.FrameUnidades.Location = new System.Drawing.Point(11, 119);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(937, 393);
            this.FrameUnidades.TabIndex = 3;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(122, 134);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(426, 57);
            this.FrameProceso.TabIndex = 2;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando documentos";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(400, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(803, 1);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(124, 16);
            this.chkMarcarDesmarcar.TabIndex = 0;
            this.chkMarcarDesmarcar.Text = "Marcar / Desmarcar";
            this.chkMarcarDesmarcar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(11, 17);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(916, 367);
            this.grdUnidades.TabIndex = 1;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 4;
            this.grdUnidades_Sheet1.RowCount = 12;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Nombre de archivo";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Enviar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Enviado";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdUnidades_Sheet1.ColumnHeader.Rows.Get(0).Height = 36F;
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Nombre de archivo";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 500F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = checkBoxCellType1;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Enviar";
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = checkBoxCellType2;
            this.grdUnidades_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Enviado";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 80F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = textCellType2;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Status";
            this.grdUnidades_Sheet1.Columns.Get(3).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 200F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEnviarDocumentos,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(957, 25);
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
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEnviarDocumentos
            // 
            this.btnEnviarDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnviarDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviarDocumentos.Image")));
            this.btnEnviarDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviarDocumentos.Name = "btnEnviarDocumentos";
            this.btnEnviarDocumentos.Size = new System.Drawing.Size(23, 22);
            this.btnEnviarDocumentos.Text = "Enviar documentos";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scLabelExt3);
            this.groupBox1.Controls.Add(this.scLabelExt2);
            this.groupBox1.Controls.Add(this.scComboBoxExt1);
            this.groupBox1.Controls.Add(this.scLabelExt1);
            this.groupBox1.Location = new System.Drawing.Point(11, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(937, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direcciones disponibles para envió";
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Location = new System.Drawing.Point(8, 19);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(63, 18);
            this.scLabelExt1.TabIndex = 0;
            this.scLabelExt1.Text = "Sitio : ";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scComboBoxExt1
            // 
            this.scComboBoxExt1.BackColorEnabled = System.Drawing.Color.White;
            this.scComboBoxExt1.Data = "";
            this.scComboBoxExt1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scComboBoxExt1.Filtro = " 1 = 1";
            this.scComboBoxExt1.FormattingEnabled = true;
            this.scComboBoxExt1.ListaItemsBusqueda = 20;
            this.scComboBoxExt1.Location = new System.Drawing.Point(71, 18);
            this.scComboBoxExt1.MostrarToolTip = false;
            this.scComboBoxExt1.Name = "scComboBoxExt1";
            this.scComboBoxExt1.Size = new System.Drawing.Size(359, 21);
            this.scComboBoxExt1.TabIndex = 0;
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Location = new System.Drawing.Point(459, 19);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(63, 18);
            this.scLabelExt2.TabIndex = 2;
            this.scLabelExt2.Text = "Directorio : ";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt3
            // 
            this.scLabelExt3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scLabelExt3.Location = new System.Drawing.Point(518, 18);
            this.scLabelExt3.MostrarToolTip = false;
            this.scLabelExt3.Name = "scLabelExt3";
            this.scLabelExt3.Size = new System.Drawing.Size(413, 21);
            this.scLabelExt3.TabIndex = 1;
            this.scLabelExt3.Text = "scLabelExt1";
            this.scLabelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmEnviarDocumentos_FTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 522);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDirectorioDeTrabajo);
            this.Controls.Add(this.FrameUnidades);
            this.Name = "FrmEnviarDocumentos_FTP";
            this.Text = "Envió de documentos vía FTP";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FrameDirectorioDeTrabajo.ResumeLayout(false);
            this.FrameUnidades.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDirectorioDeTrabajo;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEnviarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scComboBoxExt scComboBoxExt1;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt scLabelExt3;
        private SC_ControlsCS.scLabelExt scLabelExt2;
    }
}