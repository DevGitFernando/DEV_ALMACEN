namespace Farmacia.Procesos
{
    partial class FrmCorteDelDia
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.dtpNuevaFechaSistema = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaSistema = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.dtpNuevaFechaSistema);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaSistema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Corte del Día";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(109, 71);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(135, 23);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Registrar Cambio de Día";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // dtpNuevaFechaSistema
            // 
            this.dtpNuevaFechaSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpNuevaFechaSistema.Enabled = false;
            this.dtpNuevaFechaSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNuevaFechaSistema.Location = new System.Drawing.Point(153, 45);
            this.dtpNuevaFechaSistema.Name = "dtpNuevaFechaSistema";
            this.dtpNuevaFechaSistema.Size = new System.Drawing.Size(91, 20);
            this.dtpNuevaFechaSistema.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nueva Fecha de Sistema :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaSistema
            // 
            this.dtpFechaSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaSistema.Enabled = false;
            this.dtpFechaSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaSistema.Location = new System.Drawing.Point(153, 19);
            this.dtpFechaSistema.Name = "dtpFechaSistema";
            this.dtpFechaSistema.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaSistema.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Actual de Sistema:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmCorteDelDia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 110);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCorteDelDia";
            this.Text = "Corte del Día ó Cambio de Día";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCorteDelDia_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaSistema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpNuevaFechaSistema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAceptar;
    }
}