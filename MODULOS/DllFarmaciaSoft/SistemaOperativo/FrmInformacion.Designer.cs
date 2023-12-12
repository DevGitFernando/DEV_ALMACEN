namespace DllFarmaciaSoft.SistemaOperativo
{
    partial class FrmInformacion
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
            this.FrameModulos = new System.Windows.Forms.GroupBox();
            this.lvwModulos = new System.Windows.Forms.ListView();
            this.colModulo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colnformacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameModulos.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameModulos
            // 
            this.FrameModulos.Controls.Add(this.lvwModulos);
            this.FrameModulos.Location = new System.Drawing.Point(7, 2);
            this.FrameModulos.Name = "FrameModulos";
            this.FrameModulos.Size = new System.Drawing.Size(472, 190);
            this.FrameModulos.TabIndex = 1;
            this.FrameModulos.TabStop = false;
            // 
            // lvwModulos
            // 
            this.lvwModulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwModulos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colModulo,
            this.colVersion,
            this.colnformacion});
            this.lvwModulos.Location = new System.Drawing.Point(8, 14);
            this.lvwModulos.Name = "lvwModulos";
            this.lvwModulos.Size = new System.Drawing.Size(455, 166);
            this.lvwModulos.TabIndex = 1;
            this.lvwModulos.UseCompatibleStateImageBehavior = false;
            this.lvwModulos.View = System.Windows.Forms.View.Details;
            // 
            // colModulo
            // 
            this.colModulo.Text = "Módulo";
            this.colModulo.Width = 246;
            // 
            // colVersion
            // 
            this.colVersion.Text = "Versión";
            this.colVersion.Width = 80;
            // 
            // colnformacion
            // 
            this.colnformacion.Text = "Información";
            this.colnformacion.Width = 100;
            // 
            // FrmInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 197);
            this.Controls.Add(this.FrameModulos);
            this.Name = "FrmInformacion";
            this.Text = "Información módulos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInformacion_Load);
            this.FrameModulos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameModulos;
        private System.Windows.Forms.ListView lvwModulos;
        private System.Windows.Forms.ColumnHeader colModulo;
        private System.Windows.Forms.ColumnHeader colVersion;
        private System.Windows.Forms.ColumnHeader colnformacion;
    }
}