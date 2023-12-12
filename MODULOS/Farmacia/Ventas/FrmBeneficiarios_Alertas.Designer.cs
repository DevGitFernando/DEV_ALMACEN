namespace Farmacia.Ventas
{
    partial class FrmBeneficiarios_Alertas
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
            this.lstBeneficiarios = new System.Windows.Forms.ListView();
            this.colNombreBeneficiario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstBeneficiarios);
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Beneficiarios";
            // 
            // lstBeneficiarios
            // 
            this.lstBeneficiarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNombreBeneficiario});
            this.lstBeneficiarios.Location = new System.Drawing.Point(10, 16);
            this.lstBeneficiarios.Name = "lstBeneficiarios";
            this.lstBeneficiarios.Size = new System.Drawing.Size(515, 150);
            this.lstBeneficiarios.TabIndex = 0;
            this.lstBeneficiarios.UseCompatibleStateImageBehavior = false;
            this.lstBeneficiarios.View = System.Windows.Forms.View.Details;
            // 
            // colNombreBeneficiario
            // 
            this.colNombreBeneficiario.Text = "Nombre de beneficiario";
            this.colNombreBeneficiario.Width = 480;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Black;
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(0, 187);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(551, 44);
            this.label17.TabIndex = 12;
            this.label17.Text = "Si el nombre de beneficiario que esta atendiendo aparacere en esta lista favor de" +
    " solicitar autorización para surtir la receta";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmBeneficiarios_Alertas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 231);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBeneficiarios_Alertas";
            this.Text = "Beneficiarios con alertas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Shown += new System.EventHandler(this.FrmBeneficiarios_Alertas_Shown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstBeneficiarios;
        private System.Windows.Forms.ColumnHeader colNombreBeneficiario;
        private System.Windows.Forms.Label label17;
    }
}