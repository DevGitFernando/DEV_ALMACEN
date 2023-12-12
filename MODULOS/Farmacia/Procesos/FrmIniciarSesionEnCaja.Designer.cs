namespace Farmacia.Procesos
{
    partial class FrmIniciarSesionEnCaja
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDotacionInicial = new SC_ControlsCS.scCurrencyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaOpSistema = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDotacionInicial);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaOpSistema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(307, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // txtDotacionInicial
            // 
            this.txtDotacionInicial.AllowNegative = true;
            this.txtDotacionInicial.DecimalPoint = '.';
            this.txtDotacionInicial.DigitsInGroup = 3;
            this.txtDotacionInicial.Double = 1D;
            this.txtDotacionInicial.Flags = 7680;
            this.txtDotacionInicial.GroupSeparator = ',';
            this.txtDotacionInicial.Int = 0;
            this.txtDotacionInicial.Location = new System.Drawing.Point(160, 55);
            this.txtDotacionInicial.Long = ((long)(0));
            this.txtDotacionInicial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDotacionInicial.MaxDecimalPlaces = 2;
            this.txtDotacionInicial.MaxWholeDigits = 9;
            this.txtDotacionInicial.Name = "txtDotacionInicial";
            this.txtDotacionInicial.NegativeSign = '-';
            this.txtDotacionInicial.Prefix = "$";
            this.txtDotacionInicial.RangeMax = 1.7976931348623157E+308D;
            this.txtDotacionInicial.RangeMin = -1.7976931348623157E+308D;
            this.txtDotacionInicial.Size = new System.Drawing.Size(125, 22);
            this.txtDotacionInicial.TabIndex = 1;
            this.txtDotacionInicial.Text = "$1.00";
            this.txtDotacionInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDotacionInicial.Enter += new System.EventHandler(this.txtDotacionInicial_Enter);
            this.txtDotacionInicial.MouseEnter += new System.EventHandler(this.txtDotacionInicial_MouseEnter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Importe inicial :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaOpSistema
            // 
            this.dtpFechaOpSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaOpSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaOpSistema.Location = new System.Drawing.Point(160, 23);
            this.dtpFechaOpSistema.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaOpSistema.Name = "dtpFechaOpSistema";
            this.dtpFechaOpSistema.Size = new System.Drawing.Size(125, 22);
            this.dtpFechaOpSistema.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "F. Sistema :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.Location = new System.Drawing.Point(80, 111);
            this.btnIniciarSesion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(163, 28);
            this.btnIniciarSesion.TabIndex = 1;
            this.btnIniciarSesion.Text = "Iniciar sesión";
            this.btnIniciarSesion.UseVisualStyleBackColor = true;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // FrmIniciarSesionEnCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 148);
            this.Controls.Add(this.btnIniciarSesion);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmIniciarSesionEnCaja";
            this.ShowIcon = false;
            this.Text = "Iniciar sesión de Caja";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIniciarSesionEnCaja_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaOpSistema;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scCurrencyTextBox txtDotacionInicial;
        private System.Windows.Forms.Button btnIniciarSesion;
    }
}