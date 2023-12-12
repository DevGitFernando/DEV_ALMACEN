namespace Almacen.Cajas
{
    partial class FrmCajasDistribucion
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
            this.FrameNumCaja = new System.Windows.Forms.GroupBox();
            this.txtIdCaja = new SC_ControlsCS.scTextBoxExt();
            this.txtOtro = new SC_ControlsCS.scTextBoxExt();
            this.lblStatusCaja = new System.Windows.Forms.Label();
            this.FrameNumCaja.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameNumCaja
            // 
            this.FrameNumCaja.Controls.Add(this.txtIdCaja);
            this.FrameNumCaja.Controls.Add(this.lblStatusCaja);
            this.FrameNumCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameNumCaja.Location = new System.Drawing.Point(10, 7);
            this.FrameNumCaja.Name = "FrameNumCaja";
            this.FrameNumCaja.Size = new System.Drawing.Size(288, 123);
            this.FrameNumCaja.TabIndex = 1;
            this.FrameNumCaja.TabStop = false;
            this.FrameNumCaja.Text = "Caja";
            // 
            // txtIdCaja
            // 
            this.txtIdCaja.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdCaja.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdCaja.Decimales = 2;
            this.txtIdCaja.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCaja.ForeColor = System.Drawing.Color.Black;
            this.txtIdCaja.Location = new System.Drawing.Point(12, 24);
            this.txtIdCaja.MaxLength = 23;
            this.txtIdCaja.Name = "txtIdCaja";
            this.txtIdCaja.PermitirApostrofo = false;
            this.txtIdCaja.PermitirNegativos = false;
            this.txtIdCaja.Size = new System.Drawing.Size(265, 47);
            this.txtIdCaja.TabIndex = 1;
            this.txtIdCaja.Text = "012345678901234567890123";
            this.txtIdCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdCaja.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdCaja_Validating);
            // 
            // txtOtro
            // 
            this.txtOtro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOtro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtOtro.Decimales = 2;
            this.txtOtro.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtOtro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtro.ForeColor = System.Drawing.Color.Black;
            this.txtOtro.Location = new System.Drawing.Point(36, 191);
            this.txtOtro.MaxLength = 23;
            this.txtOtro.Name = "txtOtro";
            this.txtOtro.PermitirApostrofo = false;
            this.txtOtro.PermitirNegativos = false;
            this.txtOtro.Size = new System.Drawing.Size(42, 20);
            this.txtOtro.TabIndex = 2;
            this.txtOtro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStatusCaja
            // 
            this.lblStatusCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusCaja.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblStatusCaja.Location = new System.Drawing.Point(13, 75);
            this.lblStatusCaja.Name = "lblStatusCaja";
            this.lblStatusCaja.Size = new System.Drawing.Size(264, 38);
            this.lblStatusCaja.TabIndex = 53;
            this.lblStatusCaja.Text = "Caja : ";
            // 
            // FrmCajasDistribucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 138);
            this.Controls.Add(this.txtOtro);
            this.Controls.Add(this.FrameNumCaja);
            this.Name = "FrmCajasDistribucion";
            this.Text = "Liberación de Cajas de Distribución";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCajasDistribucion_Load);
            this.FrameNumCaja.ResumeLayout(false);
            this.FrameNumCaja.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameNumCaja;
        private SC_ControlsCS.scTextBoxExt txtIdCaja;
        private SC_ControlsCS.scTextBoxExt txtOtro;
        private System.Windows.Forms.Label lblStatusCaja;
    }
}