namespace Dll_IRE_INTERMED.Informacion
{
    partial class FrmInformacionDeRecetas
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacionDeRecetas));
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmRevisarGeneracion = new System.Windows.Forms.Timer(this.components);
            this.tmIniciarProceso = new System.Windows.Forms.Timer(this.components);
            this.FrameProceso = new System.Windows.Forms.GroupBox();
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
            this.pgBar.Location = new System.Drawing.Point(9, 16);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(410, 16);
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
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(8, 25);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(428, 45);
            this.FrameProceso.TabIndex = 4;
            this.FrameProceso.TabStop = false;
            // 
            // toolStripProceso
            // 
            this.toolStripProceso.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDetener});
            this.toolStripProceso.Location = new System.Drawing.Point(0, 0);
            this.toolStripProceso.Name = "toolStripProceso";
            this.toolStripProceso.Size = new System.Drawing.Size(444, 25);
            this.toolStripProceso.TabIndex = 5;
            this.toolStripProceso.Text = "Barra de herramientas";
            // 
            // btnDetener
            // 
            this.btnDetener.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDetener.Image = ((System.Drawing.Image)(resources.GetObject("btnDetener.Image")));
            this.btnDetener.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(23, 22);
            this.btnDetener.Text = "Cancelar impresión";
            this.btnDetener.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // FrmInformacionDeRecetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 77);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripProceso);
            this.Controls.Add(this.FrameProceso);
            this.Name = "FrmInformacionDeRecetas";
            this.Text = "Descargar recetas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInformacionDeRecetas_FormClosing);
            this.Load += new System.EventHandler(this.FrmInformacionDeRecetas_Load);
            this.FrameProceso.ResumeLayout(false);
            this.toolStripProceso.ResumeLayout(false);
            this.toolStripProceso.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmRevisarGeneracion;
        private System.Windows.Forms.Timer tmIniciarProceso;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ToolStrip toolStripProceso;
        private System.Windows.Forms.ToolStripButton btnDetener;

    }
}