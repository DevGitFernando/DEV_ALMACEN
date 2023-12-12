namespace DllFarmaciaSoft.QRCode
{
    partial class FrmQR_Reader
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
            this.FrameTablas = new System.Windows.Forms.GroupBox();
            this.cboTablas = new SC_ControlsCS.scComboBoxExt();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.listvwDatos = new System.Windows.Forms.ListView();
            this.FrameTablas.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameTablas
            // 
            this.FrameTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTablas.Controls.Add(this.cboTablas);
            this.FrameTablas.Location = new System.Drawing.Point(9, 3);
            this.FrameTablas.Name = "FrameTablas";
            this.FrameTablas.Size = new System.Drawing.Size(675, 52);
            this.FrameTablas.TabIndex = 0;
            this.FrameTablas.TabStop = false;
            this.FrameTablas.Text = "Tablas";
            // 
            // cboTablas
            // 
            this.cboTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTablas.BackColorEnabled = System.Drawing.Color.White;
            this.cboTablas.Data = "";
            this.cboTablas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTablas.Filtro = " 1 = 1";
            this.cboTablas.FormattingEnabled = true;
            this.cboTablas.ListaItemsBusqueda = 20;
            this.cboTablas.Location = new System.Drawing.Point(10, 19);
            this.cboTablas.MostrarToolTip = false;
            this.cboTablas.Name = "cboTablas";
            this.cboTablas.Size = new System.Drawing.Size(653, 21);
            this.cboTablas.TabIndex = 0;
            this.cboTablas.SelectedIndexChanged += new System.EventHandler(this.cboTablas_SelectedIndexChanged);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.listvwDatos);
            this.FrameDetalles.Location = new System.Drawing.Point(9, 55);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(675, 309);
            this.FrameDetalles.TabIndex = 1;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Detalles";
            // 
            // listvwDatos
            // 
            this.listvwDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listvwDatos.Location = new System.Drawing.Point(10, 16);
            this.listvwDatos.Name = "listvwDatos";
            this.listvwDatos.Size = new System.Drawing.Size(653, 282);
            this.listvwDatos.TabIndex = 0;
            this.listvwDatos.UseCompatibleStateImageBehavior = false;
            // 
            // FrmQR_Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 372);
            this.Controls.Add(this.FrameDetalles);
            this.Controls.Add(this.FrameTablas);
            this.Name = "FrmQR_Reader";
            this.Text = "QR_Reader";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmQR_Reader_Load);
            this.FrameTablas.ResumeLayout(false);
            this.FrameDetalles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameTablas;
        private SC_ControlsCS.scComboBoxExt cboTablas;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.ListView listvwDatos;
    }
}