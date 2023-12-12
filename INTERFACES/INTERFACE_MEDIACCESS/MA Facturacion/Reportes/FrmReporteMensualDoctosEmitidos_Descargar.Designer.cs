namespace MA_Facturacion 
{
    partial class FrmReporteMensualDoctosEmitidos_Descargar
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
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblAvance = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmDescarga = new System.Windows.Forms.Timer(this.components);
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.lblAvance);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(9, 4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(333, 53);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            // 
            // lblAvance
            // 
            this.lblAvance.Location = new System.Drawing.Point(7, 32);
            this.lblAvance.Name = "lblAvance";
            this.lblAvance.Size = new System.Drawing.Size(318, 16);
            this.lblAvance.TabIndex = 20;
            this.lblAvance.Text = "N de M";
            this.lblAvance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(9, 14);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(316, 15);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // tmDescarga
            // 
            this.tmDescarga.Tick += new System.EventHandler(this.tmDescarga_Tick);
            // 
            // FrmReporteMensualDoctosEmitidos_Descargar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 64);
            this.Controls.Add(this.FrameProceso);
            this.Name = "FrmReporteMensualDoctosEmitidos_Descargar";
            this.Text = "Descargando Documentos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReporteMensualDoctosEmitidos_Descargar_FormClosing);
            this.Load += new System.EventHandler(this.FrmReporteMensualDoctosEmitidos_Descargar_Load);
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lblAvance;
        private System.Windows.Forms.Timer tmDescarga;
    }
}