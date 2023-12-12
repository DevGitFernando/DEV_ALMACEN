namespace MA_Facturacion.GenerarRemisiones
{
    partial class FrmListaClavesFinanciamiento
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
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstClaves = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblConcepto = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.lblDescConcepto = new System.Windows.Forms.Label();
            this.lblDescRubro = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FrameFolios.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.lstClaves);
            this.FrameFolios.Location = new System.Drawing.Point(9, 100);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(655, 378);
            this.FrameFolios.TabIndex = 0;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Detallado";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(10, 20);
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(633, 347);
            this.lstClaves.TabIndex = 0;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblConcepto);
            this.groupBox2.Controls.Add(this.lblRubro);
            this.groupBox2.Controls.Add(this.lblDescConcepto);
            this.groupBox2.Controls.Add(this.lblDescRubro);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(9, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(655, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fuente de Finaciamiento";
            // 
            // lblConcepto
            // 
            this.lblConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConcepto.Location = new System.Drawing.Point(89, 52);
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(59, 21);
            this.lblConcepto.TabIndex = 48;
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRubro
            // 
            this.lblRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRubro.Location = new System.Drawing.Point(89, 21);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(59, 21);
            this.lblRubro.TabIndex = 47;
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDescConcepto
            // 
            this.lblDescConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescConcepto.Location = new System.Drawing.Point(154, 52);
            this.lblDescConcepto.Name = "lblDescConcepto";
            this.lblDescConcepto.Size = new System.Drawing.Size(489, 21);
            this.lblDescConcepto.TabIndex = 46;
            this.lblDescConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescRubro
            // 
            this.lblDescRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescRubro.Location = new System.Drawing.Point(154, 21);
            this.lblDescRubro.Name = "lblDescRubro";
            this.lblDescRubro.Size = new System.Drawing.Size(489, 21);
            this.lblDescRubro.TabIndex = 43;
            this.lblDescRubro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(22, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Rubro :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Concepto :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmListaClavesFinanciamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 486);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameFolios);
            this.Name = "FrmListaClavesFinanciamiento";
            this.Text = "Listado de Claves de Financiamiento";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidarRemisiones_FoliosInvalidos_Load);
            this.FrameFolios.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstClaves;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDescConcepto;
        private System.Windows.Forms.Label lblDescRubro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblConcepto;
        private System.Windows.Forms.Label lblRubro;
    }
}