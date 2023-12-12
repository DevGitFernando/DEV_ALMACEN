namespace Dll_MA_IFacturacion.PACs.FD
{
    partial class FrmInterface_FD
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
            this.btnObtenerXML = new System.Windows.Forms.Button();
            this.txtUUID = new SC_ControlsCS.scTextBoxExt();
            this.lblUUID = new SC_ControlsCS.scLabelExt();
            this.SuspendLayout();
            // 
            // btnObtenerXML
            // 
            this.btnObtenerXML.Location = new System.Drawing.Point(350, 326);
            this.btnObtenerXML.Margin = new System.Windows.Forms.Padding(4);
            this.btnObtenerXML.Name = "btnObtenerXML";
            this.btnObtenerXML.Size = new System.Drawing.Size(327, 28);
            this.btnObtenerXML.TabIndex = 2;
            this.btnObtenerXML.Text = "Obtener XML";
            this.btnObtenerXML.UseVisualStyleBackColor = true;
            this.btnObtenerXML.Click += new System.EventHandler(this.btnObtenerXML_Click);
            // 
            // txtUUID
            // 
            this.txtUUID.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUUID.Decimales = 2;
            this.txtUUID.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtUUID.ForeColor = System.Drawing.Color.Black;
            this.txtUUID.Location = new System.Drawing.Point(118, 40);
            this.txtUUID.Margin = new System.Windows.Forms.Padding(4);
            this.txtUUID.MaxLength = 100;
            this.txtUUID.Name = "txtUUID";
            this.txtUUID.PermitirApostrofo = false;
            this.txtUUID.PermitirNegativos = false;
            this.txtUUID.Size = new System.Drawing.Size(559, 22);
            this.txtUUID.TabIndex = 1;
            // 
            // lblUUID
            // 
            this.lblUUID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUUID.Location = new System.Drawing.Point(13, 37);
            this.lblUUID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUUID.MostrarToolTip = false;
            this.lblUUID.Name = "lblUUID";
            this.lblUUID.Size = new System.Drawing.Size(97, 25);
            this.lblUUID.TabIndex = 0;
            this.lblUUID.Text = "UUID : ";
            this.lblUUID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmInterface_FD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 367);
            this.Controls.Add(this.lblUUID);
            this.Controls.Add(this.txtUUID);
            this.Controls.Add(this.btnObtenerXML);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmInterface_FD";
            this.Text = "FrmInterface_FD";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObtenerXML;
        private SC_ControlsCS.scTextBoxExt txtUUID;
        private SC_ControlsCS.scLabelExt lblUUID;
    }
}