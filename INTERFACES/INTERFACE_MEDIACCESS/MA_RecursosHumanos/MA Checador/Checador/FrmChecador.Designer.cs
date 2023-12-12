namespace MA_Checador.Checador
{
    partial class FrmChecador
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
            this.FrameFotografia = new System.Windows.Forms.GroupBox();
            this.lblPuesto = new System.Windows.Forms.Label();
            this.pcFotografia = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAños = new System.Windows.Forms.Label();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.tmCerrarForma = new System.Windows.Forms.Timer(this.components);
            this.FrameFotografia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFotografia)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameFotografia
            // 
            this.FrameFotografia.Controls.Add(this.lblPuesto);
            this.FrameFotografia.Controls.Add(this.pcFotografia);
            this.FrameFotografia.Location = new System.Drawing.Point(8, 8);
            this.FrameFotografia.Name = "FrameFotografia";
            this.FrameFotografia.Size = new System.Drawing.Size(156, 219);
            this.FrameFotografia.TabIndex = 49;
            this.FrameFotografia.TabStop = false;
            this.FrameFotografia.Text = "Fotografia ";
            // 
            // lblPuesto
            // 
            this.lblPuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuesto.Location = new System.Drawing.Point(10, 179);
            this.lblPuesto.Name = "lblPuesto";
            this.lblPuesto.Size = new System.Drawing.Size(135, 25);
            this.lblPuesto.TabIndex = 61;
            this.lblPuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcFotografia
            // 
            this.pcFotografia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcFotografia.Location = new System.Drawing.Point(10, 23);
            this.pcFotografia.Name = "pcFotografia";
            this.pcFotografia.Size = new System.Drawing.Size(135, 146);
            this.pcFotografia.TabIndex = 0;
            this.pcFotografia.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDepartamento);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblAños);
            this.groupBox1.Controls.Add(this.lblPersonal);
            this.groupBox1.Controls.Add(this.lblMensaje);
            this.groupBox1.Location = new System.Drawing.Point(171, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 219);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDepartamento.Location = new System.Drawing.Point(6, 137);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(343, 32);
            this.lblDepartamento.TabIndex = 60;
            this.lblDepartamento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "Departamento :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(6, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Nombre :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAños
            // 
            this.lblAños.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAños.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAños.ForeColor = System.Drawing.Color.Red;
            this.lblAños.Location = new System.Drawing.Point(6, 172);
            this.lblAños.Name = "lblAños";
            this.lblAños.Size = new System.Drawing.Size(343, 39);
            this.lblAños.TabIndex = 57;
            this.lblAños.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAños.Visible = false;
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonal.Location = new System.Drawing.Point(6, 68);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(343, 52);
            this.lblPersonal.TabIndex = 56;
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMensaje
            // 
            this.lblMensaje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(6, 16);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(343, 33);
            this.lblMensaje.TabIndex = 55;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmCerrarForma
            // 
            this.tmCerrarForma.Interval = 2000;
            this.tmCerrarForma.Tick += new System.EventHandler(this.tmCerrarForma_Tick);
            // 
            // FrmChecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 237);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFotografia);
            this.Name = "FrmChecador";
            this.Text = "Personal";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmChecador_Load);
            this.Shown += new System.EventHandler(this.FrmChecador_Shown);
            this.FrameFotografia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFotografia)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameFotografia;
        private System.Windows.Forms.PictureBox pcFotografia;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblPersonal;
        internal System.Windows.Forms.Timer tmCerrarForma;
        private System.Windows.Forms.Label lblAños;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPuesto;



    }
}