namespace Dll_ISESEQ.InformacionOperacion
{
    partial class FrmEntregaDeColectivos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.FrameClaveDeConfirmacion = new System.Windows.Forms.GroupBox();
            this.txtFolioRecetaColectivo = new SC_ControlsCS.scTextBoxExt();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblMensaje = new SC_ControlsCS.scLabelExt();
            this.FrameProceso.SuspendLayout();
            this.FrameClaveDeConfirmacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.lblMensaje);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(12, 77);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(251, 41);
            this.FrameProceso.TabIndex = 3;
            this.FrameProceso.TabStop = false;
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(9, 16);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(233, 16);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // FrameClaveDeConfirmacion
            // 
            this.FrameClaveDeConfirmacion.Controls.Add(this.txtFolioRecetaColectivo);
            this.FrameClaveDeConfirmacion.Location = new System.Drawing.Point(12, 12);
            this.FrameClaveDeConfirmacion.Name = "FrameClaveDeConfirmacion";
            this.FrameClaveDeConfirmacion.Size = new System.Drawing.Size(557, 62);
            this.FrameClaveDeConfirmacion.TabIndex = 0;
            this.FrameClaveDeConfirmacion.TabStop = false;
            this.FrameClaveDeConfirmacion.Text = "Clave de confirmación";
            // 
            // txtFolioRecetaColectivo
            // 
            this.txtFolioRecetaColectivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolioRecetaColectivo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioRecetaColectivo.Decimales = 2;
            this.txtFolioRecetaColectivo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioRecetaColectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolioRecetaColectivo.ForeColor = System.Drawing.Color.Black;
            this.txtFolioRecetaColectivo.Location = new System.Drawing.Point(9, 19);
            this.txtFolioRecetaColectivo.MaxLength = 50;
            this.txtFolioRecetaColectivo.Name = "txtFolioRecetaColectivo";
            this.txtFolioRecetaColectivo.PermitirApostrofo = false;
            this.txtFolioRecetaColectivo.PermitirNegativos = false;
            this.txtFolioRecetaColectivo.Size = new System.Drawing.Size(538, 31);
            this.txtFolioRecetaColectivo.TabIndex = 0;
            this.txtFolioRecetaColectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(269, 80);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(147, 38);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(422, 80);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(147, 38);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Location = new System.Drawing.Point(9, 16);
            this.lblMensaje.MostrarToolTip = false;
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(200, 16);
            this.lblMensaje.TabIndex = 4;
            this.lblMensaje.Text = "scLabelExt1";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmEntregaDeColectivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 127);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.FrameClaveDeConfirmacion);
            this.Controls.Add(this.FrameProceso);
            this.Name = "FrmEntregaDeColectivos";
            this.Text = "Confirmación de entrega de colectivos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEntregaDeColectivos_Load);
            this.FrameProceso.ResumeLayout(false);
            this.FrameClaveDeConfirmacion.ResumeLayout(false);
            this.FrameClaveDeConfirmacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.GroupBox FrameClaveDeConfirmacion;
        private SC_ControlsCS.scTextBoxExt txtFolioRecetaColectivo;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private SC_ControlsCS.scLabelExt lblMensaje;
    }
}