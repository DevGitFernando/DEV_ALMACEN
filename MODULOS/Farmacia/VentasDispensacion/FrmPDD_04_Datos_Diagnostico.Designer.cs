namespace Farmacia.VentasDispensacion
{
    partial class FrmPDD_04_Datos_Diagnostico
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
            this.FrameDatosAdicionales = new System.Windows.Forms.GroupBox();
            this.txtBeneficio = new SC_ControlsCS.scTextBoxExt();
            this.lblBeneficio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.txtIdDiagnostico = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCerrar = new System.Windows.Forms.Label();
            this.FrameDatosAdicionales.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDatosAdicionales
            // 
            this.FrameDatosAdicionales.Controls.Add(this.txtBeneficio);
            this.FrameDatosAdicionales.Controls.Add(this.lblBeneficio);
            this.FrameDatosAdicionales.Controls.Add(this.label3);
            this.FrameDatosAdicionales.Controls.Add(this.lblDiagnostico);
            this.FrameDatosAdicionales.Controls.Add(this.txtIdDiagnostico);
            this.FrameDatosAdicionales.Controls.Add(this.label10);
            this.FrameDatosAdicionales.Location = new System.Drawing.Point(10, 8);
            this.FrameDatosAdicionales.Name = "FrameDatosAdicionales";
            this.FrameDatosAdicionales.Size = new System.Drawing.Size(514, 147);
            this.FrameDatosAdicionales.TabIndex = 2;
            this.FrameDatosAdicionales.TabStop = false;
            this.FrameDatosAdicionales.Text = "Información";
            // 
            // txtBeneficio
            // 
            this.txtBeneficio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficio.Decimales = 2;
            this.txtBeneficio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficio.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficio.Location = new System.Drawing.Point(89, 76);
            this.txtBeneficio.MaxLength = 4;
            this.txtBeneficio.Name = "txtBeneficio";
            this.txtBeneficio.PermitirApostrofo = false;
            this.txtBeneficio.PermitirNegativos = false;
            this.txtBeneficio.Size = new System.Drawing.Size(60, 20);
            this.txtBeneficio.TabIndex = 6;
            this.txtBeneficio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBeneficio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeneficio_KeyDown);
            this.txtBeneficio.Validating += new System.ComponentModel.CancelEventHandler(this.txtBeneficio_Validating);
            // 
            // lblBeneficio
            // 
            this.lblBeneficio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficio.Location = new System.Drawing.Point(89, 99);
            this.lblBeneficio.Name = "lblBeneficio";
            this.lblBeneficio.Size = new System.Drawing.Size(412, 33);
            this.lblBeneficio.TabIndex = 7;
            this.lblBeneficio.Text = "Nombre :";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Beneficio :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDiagnostico.Location = new System.Drawing.Point(89, 39);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(412, 33);
            this.lblDiagnostico.TabIndex = 5;
            this.lblDiagnostico.Text = "Nombre :";
            // 
            // txtIdDiagnostico
            // 
            this.txtIdDiagnostico.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDiagnostico.Decimales = 2;
            this.txtIdDiagnostico.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdDiagnostico.ForeColor = System.Drawing.Color.Black;
            this.txtIdDiagnostico.Location = new System.Drawing.Point(89, 16);
            this.txtIdDiagnostico.MaxLength = 4;
            this.txtIdDiagnostico.Name = "txtIdDiagnostico";
            this.txtIdDiagnostico.PermitirApostrofo = false;
            this.txtIdDiagnostico.PermitirNegativos = false;
            this.txtIdDiagnostico.Size = new System.Drawing.Size(60, 20);
            this.txtIdDiagnostico.TabIndex = 5;
            this.txtIdDiagnostico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDiagnostico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDiagnostico_KeyDown);
            this.txtIdDiagnostico.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDiagnostico_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(14, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "Diagnóstico :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCerrar
            // 
            this.lblCerrar.BackColor = System.Drawing.Color.Black;
            this.lblCerrar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCerrar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCerrar.Location = new System.Drawing.Point(0, 162);
            this.lblCerrar.Name = "lblCerrar";
            this.lblCerrar.Size = new System.Drawing.Size(533, 24);
            this.lblCerrar.TabIndex = 14;
            this.lblCerrar.Text = "<F12> Cerrar pantalla        [ X ]  ";
            this.lblCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCerrar.Click += new System.EventHandler(this.lblCerrar_Click);
            // 
            // FrmPDD_04_Datos_Diagnostico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 186);
            this.ControlBox = false;
            this.Controls.Add(this.lblCerrar);
            this.Controls.Add(this.FrameDatosAdicionales);
            this.Name = "FrmPDD_04_Datos_Diagnostico";
            this.Text = "Información de Diagnóstico y Beneficio";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPDD_04_Datos_Diagnostico_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPDD_04_Datos_Diagnostico_KeyDown);
            this.FrameDatosAdicionales.ResumeLayout(false);
            this.FrameDatosAdicionales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDatosAdicionales;
        private SC_ControlsCS.scTextBoxExt txtBeneficio;
        private System.Windows.Forms.Label lblBeneficio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDiagnostico;
        private SC_ControlsCS.scTextBoxExt txtIdDiagnostico;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCerrar;
    }
}