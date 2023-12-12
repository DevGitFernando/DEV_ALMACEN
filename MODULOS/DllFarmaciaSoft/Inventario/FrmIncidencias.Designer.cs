namespace DllFarmaciaSoft.Inventario
{
    partial class FrmIncidencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIncidencias));
            this.FrameIncidencias = new System.Windows.Forms.GroupBox();
            this.twIncidencias = new System.Windows.Forms.TreeView();
            this.menuExcel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnExportarAExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.lstDetalles = new System.Windows.Forms.ListView();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnExcluirErrores = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameIncidencias.SuspendLayout();
            this.menuExcel.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameIncidencias
            // 
            this.FrameIncidencias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FrameIncidencias.Controls.Add(this.twIncidencias);
            this.FrameIncidencias.Location = new System.Drawing.Point(9, 28);
            this.FrameIncidencias.Name = "FrameIncidencias";
            this.FrameIncidencias.Size = new System.Drawing.Size(243, 406);
            this.FrameIncidencias.TabIndex = 0;
            this.FrameIncidencias.TabStop = false;
            this.FrameIncidencias.Text = "Tipos de incidencias";
            // 
            // twIncidencias
            // 
            this.twIncidencias.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.twIncidencias.ContextMenuStrip = this.menuExcel;
            this.twIncidencias.Location = new System.Drawing.Point(10, 16);
            this.twIncidencias.Name = "twIncidencias";
            this.twIncidencias.Size = new System.Drawing.Size(225, 380);
            this.twIncidencias.TabIndex = 0;
            this.twIncidencias.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twIncidencias_AfterSelect);
            // 
            // menuExcel
            // 
            this.menuExcel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarAExcel});
            this.menuExcel.Name = "menuExcel";
            this.menuExcel.Size = new System.Drawing.Size(158, 26);
            // 
            // btnExportarAExcel
            // 
            this.btnExportarAExcel.Name = "btnExportarAExcel";
            this.btnExportarAExcel.Size = new System.Drawing.Size(157, 22);
            this.btnExportarAExcel.Text = "Exportar a excel";
            this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.lstDetalles);
            this.FrameDetalles.Location = new System.Drawing.Point(257, 28);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(566, 406);
            this.FrameDetalles.TabIndex = 1;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles";
            // 
            // lstDetalles
            // 
            this.lstDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetalles.Location = new System.Drawing.Point(10, 16);
            this.lstDetalles.MultiSelect = false;
            this.lstDetalles.Name = "lstDetalles";
            this.lstDetalles.ShowItemToolTips = true;
            this.lstDetalles.Size = new System.Drawing.Size(547, 380);
            this.lstDetalles.TabIndex = 0;
            this.lstDetalles.UseCompatibleStateImageBehavior = false;
            this.lstDetalles.View = System.Windows.Forms.View.Details;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 437);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(829, 24);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "Clic derecho ver menú";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExcluirErrores,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(829, 25);
            this.toolStripBarraMenu.TabIndex = 12;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnExcluirErrores
            // 
            this.btnExcluirErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluirErrores.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluirErrores.Image")));
            this.btnExcluirErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluirErrores.Name = "btnExcluirErrores";
            this.btnExcluirErrores.Size = new System.Drawing.Size(23, 22);
            this.btnExcluirErrores.Text = "Excluir excepciones";
            this.btnExcluirErrores.Click += new System.EventHandler(this.btnExcluirErrores_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmIncidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 461);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.FrameIncidencias);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MaximumSize = new System.Drawing.Size(1300, 600);
            this.MinimumSize = new System.Drawing.Size(845, 500);
            this.Name = "FrmIncidencias";
            this.Text = "Incidencias encontradas en archivo de inventario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIncidencias_Load);
            this.FrameIncidencias.ResumeLayout(false);
            this.menuExcel.ResumeLayout(false);
            this.FrameDetalles.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameIncidencias;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.TreeView twIncidencias;
        private System.Windows.Forms.ListView lstDetalles;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ContextMenuStrip menuExcel;
        private System.Windows.Forms.ToolStripMenuItem btnExportarAExcel;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnExcluirErrores;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}