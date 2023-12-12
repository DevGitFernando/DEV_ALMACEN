namespace Farmacia.VentasDispensacion
{
    partial class FrmPDD_03_Datos_Documento
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumReceta = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpFechaDeReceta = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.txtIdMedico = new SC_ControlsCS.scTextBoxExt();
            this.lblMedico = new System.Windows.Forms.Label();
            this.btnRegistrarMedicos = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cboTipoDeSurtimiento = new SC_ControlsCS.scComboBoxExt();
            this.label19 = new System.Windows.Forms.Label();
            this.txtUMedica = new SC_ControlsCS.scTextBoxExt();
            this.lblUnidadMedica = new System.Windows.Forms.Label();
            this.FrameDatosAdicionales = new System.Windows.Forms.GroupBox();
            this.lblCerrar = new System.Windows.Forms.Label();
            this.FrameDatosAdicionales.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Num. Referencia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumReceta
            // 
            this.txtNumReceta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumReceta.Decimales = 2;
            this.txtNumReceta.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumReceta.ForeColor = System.Drawing.Color.Black;
            this.txtNumReceta.Location = new System.Drawing.Point(110, 19);
            this.txtNumReceta.MaxLength = 20;
            this.txtNumReceta.Name = "txtNumReceta";
            this.txtNumReceta.PermitirApostrofo = false;
            this.txtNumReceta.PermitirNegativos = false;
            this.txtNumReceta.Size = new System.Drawing.Size(141, 20);
            this.txtNumReceta.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(328, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Fecha Receta :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDeReceta
            // 
            this.dtpFechaDeReceta.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeReceta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeReceta.Location = new System.Drawing.Point(426, 18);
            this.dtpFechaDeReceta.Name = "dtpFechaDeReceta";
            this.dtpFechaDeReceta.Size = new System.Drawing.Size(96, 20);
            this.dtpFechaDeReceta.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 16);
            this.label13.TabIndex = 18;
            this.label13.Text = "Médico :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdMedico
            // 
            this.txtIdMedico.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdMedico.Decimales = 2;
            this.txtIdMedico.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdMedico.ForeColor = System.Drawing.Color.Black;
            this.txtIdMedico.Location = new System.Drawing.Point(110, 94);
            this.txtIdMedico.MaxLength = 6;
            this.txtIdMedico.Name = "txtIdMedico";
            this.txtIdMedico.PermitirApostrofo = false;
            this.txtIdMedico.PermitirNegativos = false;
            this.txtIdMedico.Size = new System.Drawing.Size(60, 20);
            this.txtIdMedico.TabIndex = 4;
            this.txtIdMedico.Text = "123456";
            this.txtIdMedico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdMedico_KeyDown);
            this.txtIdMedico.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdMedico_Validating);
            // 
            // lblMedico
            // 
            this.lblMedico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMedico.Location = new System.Drawing.Point(176, 94);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(317, 20);
            this.lblMedico.TabIndex = 20;
            this.lblMedico.Text = "Nombre :";
            this.lblMedico.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRegistrarMedicos
            // 
            this.btnRegistrarMedicos.Location = new System.Drawing.Point(496, 93);
            this.btnRegistrarMedicos.Name = "btnRegistrarMedicos";
            this.btnRegistrarMedicos.Size = new System.Drawing.Size(26, 20);
            this.btnRegistrarMedicos.TabIndex = 10;
            this.btnRegistrarMedicos.Text = "...";
            this.btnRegistrarMedicos.UseVisualStyleBackColor = true;
            this.btnRegistrarMedicos.Click += new System.EventHandler(this.btnRegistrarMedicos_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(8, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Tipo surtimiento :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoDeSurtimiento
            // 
            this.cboTipoDeSurtimiento.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoDeSurtimiento.Data = "";
            this.cboTipoDeSurtimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDeSurtimiento.Filtro = " 1 = 1";
            this.cboTipoDeSurtimiento.FormattingEnabled = true;
            this.cboTipoDeSurtimiento.ListaItemsBusqueda = 20;
            this.cboTipoDeSurtimiento.Location = new System.Drawing.Point(110, 44);
            this.cboTipoDeSurtimiento.MostrarToolTip = false;
            this.cboTipoDeSurtimiento.Name = "cboTipoDeSurtimiento";
            this.cboTipoDeSurtimiento.Size = new System.Drawing.Size(412, 21);
            this.cboTipoDeSurtimiento.TabIndex = 2;
            this.cboTipoDeSurtimiento.SelectedIndexChanged += new System.EventHandler(this.cboTipoDeSurtimiento_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(8, 72);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 16);
            this.label19.TabIndex = 32;
            this.label19.Text = "Unidad Medica :";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUMedica
            // 
            this.txtUMedica.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtUMedica.Decimales = 2;
            this.txtUMedica.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtUMedica.ForeColor = System.Drawing.Color.Black;
            this.txtUMedica.Location = new System.Drawing.Point(110, 70);
            this.txtUMedica.MaxLength = 6;
            this.txtUMedica.Name = "txtUMedica";
            this.txtUMedica.PermitirApostrofo = false;
            this.txtUMedica.PermitirNegativos = false;
            this.txtUMedica.Size = new System.Drawing.Size(60, 20);
            this.txtUMedica.TabIndex = 3;
            this.txtUMedica.Text = "123456";
            this.txtUMedica.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUMedica.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUMedica_KeyDown);
            this.txtUMedica.Validating += new System.ComponentModel.CancelEventHandler(this.txtUMedica_Validating);
            // 
            // lblUnidadMedica
            // 
            this.lblUnidadMedica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUnidadMedica.Location = new System.Drawing.Point(176, 70);
            this.lblUnidadMedica.Name = "lblUnidadMedica";
            this.lblUnidadMedica.Size = new System.Drawing.Size(346, 20);
            this.lblUnidadMedica.TabIndex = 33;
            this.lblUnidadMedica.Text = "Nombre :";
            this.lblUnidadMedica.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrameDatosAdicionales
            // 
            this.FrameDatosAdicionales.Controls.Add(this.lblUnidadMedica);
            this.FrameDatosAdicionales.Controls.Add(this.txtUMedica);
            this.FrameDatosAdicionales.Controls.Add(this.label19);
            this.FrameDatosAdicionales.Controls.Add(this.cboTipoDeSurtimiento);
            this.FrameDatosAdicionales.Controls.Add(this.label12);
            this.FrameDatosAdicionales.Controls.Add(this.btnRegistrarMedicos);
            this.FrameDatosAdicionales.Controls.Add(this.lblMedico);
            this.FrameDatosAdicionales.Controls.Add(this.txtIdMedico);
            this.FrameDatosAdicionales.Controls.Add(this.label13);
            this.FrameDatosAdicionales.Controls.Add(this.dtpFechaDeReceta);
            this.FrameDatosAdicionales.Controls.Add(this.label9);
            this.FrameDatosAdicionales.Controls.Add(this.txtNumReceta);
            this.FrameDatosAdicionales.Controls.Add(this.label8);
            this.FrameDatosAdicionales.Location = new System.Drawing.Point(10, 8);
            this.FrameDatosAdicionales.Name = "FrameDatosAdicionales";
            this.FrameDatosAdicionales.Size = new System.Drawing.Size(535, 128);
            this.FrameDatosAdicionales.TabIndex = 2;
            this.FrameDatosAdicionales.TabStop = false;
            this.FrameDatosAdicionales.Text = "Datos de Receta";
            // 
            // lblCerrar
            // 
            this.lblCerrar.BackColor = System.Drawing.Color.Black;
            this.lblCerrar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCerrar.ForeColor = System.Drawing.SystemColors.Control;
            this.lblCerrar.Location = new System.Drawing.Point(0, 141);
            this.lblCerrar.Name = "lblCerrar";
            this.lblCerrar.Size = new System.Drawing.Size(554, 24);
            this.lblCerrar.TabIndex = 13;
            this.lblCerrar.Text = "<F12> Cerrar pantalla        [ X ]  ";
            this.lblCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCerrar.Click += new System.EventHandler(this.lblCerrar_Click);
            // 
            // FrmPDD_03_Datos_Documento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 165);
            this.ControlBox = false;
            this.Controls.Add(this.lblCerrar);
            this.Controls.Add(this.FrameDatosAdicionales);
            this.Name = "FrmPDD_03_Datos_Documento";
            this.Text = "Información de documento";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Activated += new System.EventHandler(this.FrmPDD_03_Datos_Documento_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPDD_03_Datos_Documento_FormClosing);
            this.Load += new System.EventHandler(this.FrmPDD_03_Datos_Documento_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPDD_03_Datos_Documento_KeyDown);
            this.FrameDatosAdicionales.ResumeLayout(false);
            this.FrameDatosAdicionales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtNumReceta;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpFechaDeReceta;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtIdMedico;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.Button btnRegistrarMedicos;
        private System.Windows.Forms.Label label12;
        private SC_ControlsCS.scComboBoxExt cboTipoDeSurtimiento;
        private System.Windows.Forms.Label label19;
        private SC_ControlsCS.scTextBoxExt txtUMedica;
        private System.Windows.Forms.Label lblUnidadMedica;
        private System.Windows.Forms.GroupBox FrameDatosAdicionales;
        private System.Windows.Forms.Label lblCerrar;

    }
}