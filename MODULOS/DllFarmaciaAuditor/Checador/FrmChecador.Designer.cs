namespace DllFarmaciaAuditor.Checador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChecador));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnEntrada = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalida = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.lblDepartamento = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAños = new System.Windows.Forms.Label();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.FrameFotografia = new System.Windows.Forms.GroupBox();
            this.lblPuesto = new System.Windows.Forms.Label();
            this.pcFotografia = new System.Windows.Forms.PictureBox();
            this.tmCerrarForma = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameFotografia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcFotografia)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEntrada,
            this.toolStripSeparator1,
            this.btnSalida});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(807, 64);
            this.toolStrip.TabIndex = 28;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnEntrada
            // 
            this.btnEntrada.AutoSize = false;
            this.btnEntrada.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEntrada.Image = global::DllFarmaciaAuditor.Properties.Resources.icono_entrada;
            this.btnEntrada.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Size = new System.Drawing.Size(66, 61);
            this.btnEntrada.Text = "ENTRADA";
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // btnSalida
            // 
            this.btnSalida.AutoSize = false;
            this.btnSalida.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalida.Image = global::DllFarmaciaAuditor.Properties.Resources.icono_salida;
            this.btnSalida.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(66, 61);
            this.btnSalida.Text = "SALIDA";
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.lblDepartamento);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.label9);
            this.FrameDatos.Controls.Add(this.lblAños);
            this.FrameDatos.Controls.Add(this.lblPersonal);
            this.FrameDatos.Controls.Add(this.lblMensaje);
            this.FrameDatos.Location = new System.Drawing.Point(260, 67);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(536, 321);
            this.FrameDatos.TabIndex = 54;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Visible = false;
            // 
            // lblDepartamento
            // 
            this.lblDepartamento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartamento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDepartamento.Location = new System.Drawing.Point(6, 193);
            this.lblDepartamento.Name = "lblDepartamento";
            this.lblDepartamento.Size = new System.Drawing.Size(517, 32);
            this.lblDepartamento.TabIndex = 60;
            this.lblDepartamento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(6, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(517, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "Departamento :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(6, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(517, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Nombre :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAños
            // 
            this.lblAños.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAños.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAños.ForeColor = System.Drawing.Color.Red;
            this.lblAños.Location = new System.Drawing.Point(6, 230);
            this.lblAños.Name = "lblAños";
            this.lblAños.Size = new System.Drawing.Size(517, 39);
            this.lblAños.TabIndex = 57;
            this.lblAños.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAños.Visible = false;
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonal.Location = new System.Drawing.Point(6, 100);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(517, 52);
            this.lblPersonal.TabIndex = 56;
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMensaje
            // 
            this.lblMensaje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(6, 23);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(517, 33);
            this.lblMensaje.TabIndex = 55;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameFotografia
            // 
            this.FrameFotografia.Controls.Add(this.lblPuesto);
            this.FrameFotografia.Controls.Add(this.pcFotografia);
            this.FrameFotografia.Location = new System.Drawing.Point(12, 67);
            this.FrameFotografia.Name = "FrameFotografia";
            this.FrameFotografia.Size = new System.Drawing.Size(242, 321);
            this.FrameFotografia.TabIndex = 53;
            this.FrameFotografia.TabStop = false;
            this.FrameFotografia.Text = "Fotografia ";
            this.FrameFotografia.Visible = false;
            // 
            // lblPuesto
            // 
            this.lblPuesto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPuesto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuesto.Location = new System.Drawing.Point(10, 281);
            this.lblPuesto.Name = "lblPuesto";
            this.lblPuesto.Size = new System.Drawing.Size(221, 25);
            this.lblPuesto.TabIndex = 61;
            this.lblPuesto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcFotografia
            // 
            this.pcFotografia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcFotografia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pcFotografia.Location = new System.Drawing.Point(10, 23);
            this.pcFotografia.Name = "pcFotografia";
            this.pcFotografia.Size = new System.Drawing.Size(221, 248);
            this.pcFotografia.TabIndex = 0;
            this.pcFotografia.TabStop = false;
            // 
            // tmCerrarForma
            // 
            this.tmCerrarForma.Interval = 2500;
            this.tmCerrarForma.Tick += new System.EventHandler(this.tmCerrarForma_Tick);
            // 
            // FrmChecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 393);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.FrameFotografia);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChecador";
            this.Text = "Servicio de Checador";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmChecador_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameFotografia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcFotografia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnEntrada;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalida;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label lblDepartamento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAños;
        private System.Windows.Forms.Label lblPersonal;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.GroupBox FrameFotografia;
        private System.Windows.Forms.Label lblPuesto;
        private System.Windows.Forms.PictureBox pcFotografia;
        internal System.Windows.Forms.Timer tmCerrarForma;
    }
}