namespace Dll_MA_IFacturacion.XSA
{
    partial class FrmCancelarDocumentos
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
            this.tmDescarga = new System.Windows.Forms.Timer(this.components);
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmDescarga
            // 
            this.tmDescarga.Tick += new System.EventHandler(this.tmDescarga_Tick);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(10, 5);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Size = new System.Drawing.Size(610, 54);
            this.FrameProceso.TabIndex = 5;
            this.FrameProceso.TabStop = false;
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(11, 18);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(591, 23);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            // 
            // FrmCancelarDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 69);
            this.ControlBox = false;
            this.Controls.Add(this.FrameProceso);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmCancelarDocumentos";
            this.Text = "Cancelando documento : ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCancelarDocumentos_Load);
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmDescarga;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
    }
}