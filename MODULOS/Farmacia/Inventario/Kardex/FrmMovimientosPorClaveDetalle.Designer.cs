namespace Farmacia.Inventario
{
    partial class FrmMovimientosPorClaveDetalle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMovimientosPorClaveDetalle));
            this.FrameDetalle = new System.Windows.Forms.GroupBox();
            this.listMovimientos = new System.Windows.Forms.ListView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameDetalle.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDetalle
            // 
            this.FrameDetalle.Controls.Add(this.listMovimientos);
            this.FrameDetalle.Location = new System.Drawing.Point(15, 59);
            this.FrameDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Name = "FrameDetalle";
            this.FrameDetalle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDetalle.Size = new System.Drawing.Size(932, 398);
            this.FrameDetalle.TabIndex = 5;
            this.FrameDetalle.TabStop = false;
            this.FrameDetalle.Text = "Detalle";
            // 
            // listMovimientos
            // 
            this.listMovimientos.HideSelection = false;
            this.listMovimientos.Location = new System.Drawing.Point(13, 23);
            this.listMovimientos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listMovimientos.Name = "listMovimientos";
            this.listMovimientos.Size = new System.Drawing.Size(905, 357);
            this.listMovimientos.TabIndex = 2;
            this.listMovimientos.UseCompatibleStateImageBehavior = false;
            this.listMovimientos.SelectedIndexChanged += new System.EventHandler(this.listMovimientos_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(960, 58);
            this.toolStripBarraMenu.TabIndex = 8;
            this.toolStripBarraMenu.Text = "toolStrip1";
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
            // FrmMovimientosPorClaveDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 473);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalle);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmMovimientosPorClaveDetalle";
            this.ShowIcon = false;
            this.Text = "Detalle de Movimientos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMovimientosPorClaveDetalle_Load);
            this.FrameDetalle.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDetalle;
        private System.Windows.Forms.ListView listMovimientos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}