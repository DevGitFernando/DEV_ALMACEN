namespace DllFarmaciaSoft.Reporteador
{
    partial class FrmReporteador
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteador));
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmRevisarGeneracion = new System.Windows.Forms.Timer(this.components);
            this.tmIniciarProceso = new System.Windows.Forms.Timer(this.components);
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblTituloReporte = new SC_ControlsCS.scLabelExt();
            this.toolStripProceso = new System.Windows.Forms.ToolStrip();
            this.btnDetener = new System.Windows.Forms.ToolStripButton();
            this.FrameProceso.SuspendLayout();
            this.toolStripProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(12, 49);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(547, 25);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // tmRevisarGeneracion
            // 
            this.tmRevisarGeneracion.Tick += new System.EventHandler(this.tmRevisarGeneracion_Tick);
            // 
            // tmIniciarProceso
            // 
            this.tmIniciarProceso.Tick += new System.EventHandler(this.tmIniciarProceso_Tick);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameProceso.Controls.Add(this.toolStripProceso);
            this.FrameProceso.Controls.Add(this.lblTituloReporte);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(11, 7);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4);
            this.FrameProceso.Size = new System.Drawing.Size(571, 137);
            this.FrameProceso.TabIndex = 4;
            this.FrameProceso.TabStop = false;
            // 
            // lblTituloReporte
            // 
            this.lblTituloReporte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTituloReporte.Location = new System.Drawing.Point(12, 16);
            this.lblTituloReporte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloReporte.MostrarToolTip = false;
            this.lblTituloReporte.Name = "lblTituloReporte";
            this.lblTituloReporte.Size = new System.Drawing.Size(547, 22);
            this.lblTituloReporte.TabIndex = 3;
            this.lblTituloReporte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripProceso
            // 
            this.toolStripProceso.AutoSize = false;
            this.toolStripProceso.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripProceso.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStripProceso.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDetener});
            this.toolStripProceso.Location = new System.Drawing.Point(492, 80);
            this.toolStripProceso.Name = "toolStripProceso";
            this.toolStripProceso.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProceso.Size = new System.Drawing.Size(78, 48);
            this.toolStripProceso.TabIndex = 5;
            this.toolStripProceso.Text = "Barra de herramientas";
            // 
            // btnDetener
            // 
            this.btnDetener.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetener.Image = ((System.Drawing.Image)(resources.GetObject("btnDetener.Image")));
            this.btnDetener.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(44, 45);
            this.btnDetener.Text = "Cancelar impresión";
            this.btnDetener.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // FrmReporteador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 158);
            this.ControlBox = false;
            this.Controls.Add(this.FrameProceso);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmReporteador";
            this.ShowIcon = false;
            this.Text = "Informe en proceso";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmReporteador_FormClosing);
            this.Load += new System.EventHandler(this.FrmReporteador_Load);
            this.FrameProceso.ResumeLayout(false);
            this.toolStripProceso.ResumeLayout(false);
            this.toolStripProceso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmRevisarGeneracion;
        private System.Windows.Forms.Timer tmIniciarProceso;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ToolStrip toolStripProceso;
        private System.Windows.Forms.ToolStripButton btnDetener;
        private SC_ControlsCS.scLabelExt lblTituloReporte;

    }
}