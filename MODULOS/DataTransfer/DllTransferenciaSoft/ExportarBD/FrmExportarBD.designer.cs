namespace DllTransferenciaSoft.ExportarBD
{
    partial class FrmExportarBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportarBD));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarTablas = new System.Windows.Forms.ToolStripButton();
            this.grpCatalogosProcesar = new System.Windows.Forms.GroupBox();
            this.FrameBD = new System.Windows.Forms.GroupBox();
            this.lblBD = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lstwDetalles = new SC_ControlsCS.scListView();
            this.Tabla = new System.Windows.Forms.ColumnHeader();
            this.TablaAux = new System.Windows.Forms.ColumnHeader();
            this.Procesada = new System.Windows.Forms.ColumnHeader();
            this.toolStripBarraMenu.SuspendLayout();
            this.grpCatalogosProcesar.SuspendLayout();
            this.FrameBD.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnProcesarTablas});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(667, 25);
            this.toolStripBarraMenu.TabIndex = 37;
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
            // btnProcesarTablas
            // 
            this.btnProcesarTablas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesarTablas.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesarTablas.Image")));
            this.btnProcesarTablas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesarTablas.Name = "btnProcesarTablas";
            this.btnProcesarTablas.Size = new System.Drawing.Size(23, 22);
            this.btnProcesarTablas.Text = "Procesar";
            this.btnProcesarTablas.Click += new System.EventHandler(this.btnProcesarTablas_Click);
            // 
            // grpCatalogosProcesar
            // 
            this.grpCatalogosProcesar.Controls.Add(this.FrameBD);
            this.grpCatalogosProcesar.Controls.Add(this.lstwDetalles);
            this.grpCatalogosProcesar.Location = new System.Drawing.Point(12, 28);
            this.grpCatalogosProcesar.Name = "grpCatalogosProcesar";
            this.grpCatalogosProcesar.Size = new System.Drawing.Size(529, 325);
            this.grpCatalogosProcesar.TabIndex = 38;
            this.grpCatalogosProcesar.TabStop = false;
            this.grpCatalogosProcesar.Text = "Catálogos a Procesar";
            // 
            // FrameBD
            // 
            this.FrameBD.Controls.Add(this.lblBD);
            this.FrameBD.Controls.Add(this.pgBar);
            this.FrameBD.Location = new System.Drawing.Point(30, 60);
            this.FrameBD.Name = "FrameBD";
            this.FrameBD.Size = new System.Drawing.Size(465, 102);
            this.FrameBD.TabIndex = 39;
            this.FrameBD.TabStop = false;
            this.FrameBD.Visible = false;
            // 
            // lblBD
            // 
            this.lblBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBD.Location = new System.Drawing.Point(10, 69);
            this.lblBD.Name = "lblBD";
            this.lblBD.Size = new System.Drawing.Size(444, 20);
            this.lblBD.TabIndex = 3;
            this.lblBD.Text = "label1";
            this.lblBD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(10, 25);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(444, 38);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // lstwDetalles
            // 
            this.lstwDetalles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tabla,
            this.TablaAux,
            this.Procesada});
            this.lstwDetalles.FullRowSelect = true;
            this.lstwDetalles.GridLines = true;
            this.lstwDetalles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstwDetalles.HideSelection = false;
            this.lstwDetalles.Location = new System.Drawing.Point(10, 16);
            this.lstwDetalles.LockColumnSize = true;
            this.lstwDetalles.MultiSelect = false;
            this.lstwDetalles.Name = "lstwDetalles";
            this.lstwDetalles.Size = new System.Drawing.Size(508, 297);
            this.lstwDetalles.TabIndex = 38;
            this.lstwDetalles.UseCompatibleStateImageBehavior = false;
            this.lstwDetalles.View = System.Windows.Forms.View.Details;
            this.lstwDetalles.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstwDetalles_ColumnWidthChanging);
            // 
            // Tabla
            // 
            this.Tabla.Text = "Catalogo";
            this.Tabla.Width = 0;
            // 
            // TablaAux
            // 
            this.TablaAux.Text = "Catalogo";
            this.TablaAux.Width = 360;
            // 
            // Procesada
            // 
            this.Procesada.Text = "Status";
            this.Procesada.Width = 120;
            // 
            // FrmExportarBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 436);
            this.Controls.Add(this.grpCatalogosProcesar);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmExportarBD";
            this.Text = "Migración de Información";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmServicioSinConexion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpCatalogosProcesar.ResumeLayout(false);
            this.FrameBD.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnProcesarTablas;
        private System.Windows.Forms.GroupBox grpCatalogosProcesar;
        private SC_ControlsCS.scListView lstwDetalles;
        private System.Windows.Forms.ColumnHeader Tabla;
        private System.Windows.Forms.ColumnHeader Procesada;
        private System.Windows.Forms.GroupBox FrameBD;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lblBD;
        private System.Windows.Forms.ColumnHeader TablaAux;
    }
}