namespace Farmacia.Inventario
{
    partial class FrmMarcarProductosInventario
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
            this.lblMarcadosInventario = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProductosEnInventario = new System.Windows.Forms.Label();
            this.lblProductosActivos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMarcar = new System.Windows.Forms.Button();
            this.btnDesmarcar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMarcadosInventario);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblProductosEnInventario);
            this.groupBox1.Controls.Add(this.lblProductosActivos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos";
            // 
            // lblMarcadosInventario
            // 
            this.lblMarcadosInventario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMarcadosInventario.Location = new System.Drawing.Point(267, 21);
            this.lblMarcadosInventario.Name = "lblMarcadosInventario";
            this.lblMarcadosInventario.Size = new System.Drawing.Size(91, 21);
            this.lblMarcadosInventario.TabIndex = 5;
            this.lblMarcadosInventario.Text = "label4";
            this.lblMarcadosInventario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(194, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "Marcados :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProductosEnInventario
            // 
            this.lblProductosEnInventario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductosEnInventario.Location = new System.Drawing.Point(267, 50);
            this.lblProductosEnInventario.Name = "lblProductosEnInventario";
            this.lblProductosEnInventario.Size = new System.Drawing.Size(91, 21);
            this.lblProductosEnInventario.TabIndex = 3;
            this.lblProductosEnInventario.Text = "label3";
            this.lblProductosEnInventario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductosActivos
            // 
            this.lblProductosActivos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductosActivos.Location = new System.Drawing.Point(77, 21);
            this.lblProductosActivos.Name = "lblProductosActivos";
            this.lblProductosActivos.Size = new System.Drawing.Size(91, 21);
            this.lblProductosActivos.TabIndex = 2;
            this.lblProductosActivos.Text = "label4";
            this.lblProductosActivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(191, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "En Inventario :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Activos :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMarcar
            // 
            this.btnMarcar.Location = new System.Drawing.Point(180, 92);
            this.btnMarcar.Name = "btnMarcar";
            this.btnMarcar.Size = new System.Drawing.Size(99, 23);
            this.btnMarcar.TabIndex = 1;
            this.btnMarcar.Text = "Marcar";
            this.btnMarcar.UseVisualStyleBackColor = true;
            this.btnMarcar.Click += new System.EventHandler(this.btnMarcar_Click);
            // 
            // btnDesmarcar
            // 
            this.btnDesmarcar.Location = new System.Drawing.Point(285, 92);
            this.btnDesmarcar.Name = "btnDesmarcar";
            this.btnDesmarcar.Size = new System.Drawing.Size(99, 23);
            this.btnDesmarcar.TabIndex = 2;
            this.btnDesmarcar.Text = "Desmarcar";
            this.btnDesmarcar.UseVisualStyleBackColor = true;
            this.btnDesmarcar.Click += new System.EventHandler(this.btnDesmarcar_Click);
            // 
            // FrmMarcarProductosInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 123);
            this.Controls.Add(this.btnDesmarcar);
            this.Controls.Add(this.btnMarcar);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMarcarProductosInventario";
            this.Text = "Marcar-Desmarcar Productos para Ajuste de Inventario";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMarcarProductosInventario_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblProductosEnInventario;
        private System.Windows.Forms.Label lblProductosActivos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMarcar;
        private System.Windows.Forms.Button btnDesmarcar;
        private System.Windows.Forms.Label lblMarcadosInventario;
        private System.Windows.Forms.Label label4;
    }
}