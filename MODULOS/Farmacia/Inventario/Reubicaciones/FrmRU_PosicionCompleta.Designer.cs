namespace Farmacia.Inventario
{
    partial class FrmRU_PosicionCompleta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRU_PosicionCompleta));
            this.gpoUbicaciones = new System.Windows.Forms.GroupBox();
            this.lblEntrepaño = new System.Windows.Forms.Label();
            this.txtEntrepaño = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.lblEstante = new System.Windows.Forms.Label();
            this.txtEstante = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPasillo = new System.Windows.Forms.Label();
            this.txtPasillo = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEntrepañoDestino = new System.Windows.Forms.Label();
            this.txtIdEntrepañoDestino = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEstanteDestino = new System.Windows.Forms.Label();
            this.txtIdEstanteDestino = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPasilloDestino = new System.Windows.Forms.Label();
            this.txtIdPasilloDestino = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesar = new System.Windows.Forms.ToolStripButton();
            this.gpoUbicaciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpoUbicaciones
            // 
            this.gpoUbicaciones.Controls.Add(this.lblEntrepaño);
            this.gpoUbicaciones.Controls.Add(this.txtEntrepaño);
            this.gpoUbicaciones.Controls.Add(this.label10);
            this.gpoUbicaciones.Controls.Add(this.lblEstante);
            this.gpoUbicaciones.Controls.Add(this.txtEstante);
            this.gpoUbicaciones.Controls.Add(this.label3);
            this.gpoUbicaciones.Controls.Add(this.lblPasillo);
            this.gpoUbicaciones.Controls.Add(this.txtPasillo);
            this.gpoUbicaciones.Controls.Add(this.label6);
            this.gpoUbicaciones.Location = new System.Drawing.Point(14, 60);
            this.gpoUbicaciones.Margin = new System.Windows.Forms.Padding(4);
            this.gpoUbicaciones.Name = "gpoUbicaciones";
            this.gpoUbicaciones.Padding = new System.Windows.Forms.Padding(4);
            this.gpoUbicaciones.Size = new System.Drawing.Size(575, 138);
            this.gpoUbicaciones.TabIndex = 1;
            this.gpoUbicaciones.TabStop = false;
            this.gpoUbicaciones.Text = "Ubicación Origen";
            // 
            // lblEntrepaño
            // 
            this.lblEntrepaño.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntrepaño.Location = new System.Drawing.Point(211, 95);
            this.lblEntrepaño.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrepaño.Name = "lblEntrepaño";
            this.lblEntrepaño.Size = new System.Drawing.Size(347, 25);
            this.lblEntrepaño.TabIndex = 56;
            this.lblEntrepaño.Text = "label1";
            // 
            // txtEntrepaño
            // 
            this.txtEntrepaño.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEntrepaño.Decimales = 2;
            this.txtEntrepaño.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEntrepaño.ForeColor = System.Drawing.Color.Black;
            this.txtEntrepaño.Location = new System.Drawing.Point(124, 95);
            this.txtEntrepaño.Margin = new System.Windows.Forms.Padding(4);
            this.txtEntrepaño.MaxLength = 4;
            this.txtEntrepaño.Name = "txtEntrepaño";
            this.txtEntrepaño.PermitirApostrofo = false;
            this.txtEntrepaño.PermitirNegativos = false;
            this.txtEntrepaño.Size = new System.Drawing.Size(77, 22);
            this.txtEntrepaño.TabIndex = 2;
            this.txtEntrepaño.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEntrepaño.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEntrepaño_KeyDown);
            this.txtEntrepaño.Validating += new System.ComponentModel.CancelEventHandler(this.txtEntrepaño_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(20, 97);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 20);
            this.label10.TabIndex = 55;
            this.label10.Text = "Posición :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstante
            // 
            this.lblEstante.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstante.Location = new System.Drawing.Point(211, 64);
            this.lblEstante.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstante.Name = "lblEstante";
            this.lblEstante.Size = new System.Drawing.Size(347, 25);
            this.lblEstante.TabIndex = 53;
            this.lblEstante.Text = "label1";
            // 
            // txtEstante
            // 
            this.txtEstante.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstante.Decimales = 2;
            this.txtEstante.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstante.ForeColor = System.Drawing.Color.Black;
            this.txtEstante.Location = new System.Drawing.Point(124, 64);
            this.txtEstante.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstante.MaxLength = 4;
            this.txtEstante.Name = "txtEstante";
            this.txtEstante.PermitirApostrofo = false;
            this.txtEstante.PermitirNegativos = false;
            this.txtEstante.Size = new System.Drawing.Size(77, 22);
            this.txtEstante.TabIndex = 1;
            this.txtEstante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstante.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEstante_KeyDown);
            this.txtEstante.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstante_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(55, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 52;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasillo
            // 
            this.lblPasillo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPasillo.Location = new System.Drawing.Point(211, 33);
            this.lblPasillo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasillo.Name = "lblPasillo";
            this.lblPasillo.Size = new System.Drawing.Size(347, 25);
            this.lblPasillo.TabIndex = 50;
            this.lblPasillo.Text = "label1";
            // 
            // txtPasillo
            // 
            this.txtPasillo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPasillo.Decimales = 2;
            this.txtPasillo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPasillo.ForeColor = System.Drawing.Color.Black;
            this.txtPasillo.Location = new System.Drawing.Point(124, 33);
            this.txtPasillo.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasillo.MaxLength = 4;
            this.txtPasillo.Name = "txtPasillo";
            this.txtPasillo.PermitirApostrofo = false;
            this.txtPasillo.PermitirNegativos = false;
            this.txtPasillo.Size = new System.Drawing.Size(77, 22);
            this.txtPasillo.TabIndex = 0;
            this.txtPasillo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasillo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasillo_KeyDown);
            this.txtPasillo.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasillo_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(55, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Rack :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEntrepañoDestino);
            this.groupBox1.Controls.Add(this.txtIdEntrepañoDestino);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblEstanteDestino);
            this.groupBox1.Controls.Add(this.txtIdEstanteDestino);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblPasilloDestino);
            this.groupBox1.Controls.Add(this.txtIdPasilloDestino);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(14, 206);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(575, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ubicación Destino";
            // 
            // lblEntrepañoDestino
            // 
            this.lblEntrepañoDestino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEntrepañoDestino.Location = new System.Drawing.Point(211, 95);
            this.lblEntrepañoDestino.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrepañoDestino.Name = "lblEntrepañoDestino";
            this.lblEntrepañoDestino.Size = new System.Drawing.Size(347, 25);
            this.lblEntrepañoDestino.TabIndex = 56;
            this.lblEntrepañoDestino.Text = "label1";
            // 
            // txtIdEntrepañoDestino
            // 
            this.txtIdEntrepañoDestino.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEntrepañoDestino.Decimales = 2;
            this.txtIdEntrepañoDestino.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEntrepañoDestino.ForeColor = System.Drawing.Color.Black;
            this.txtIdEntrepañoDestino.Location = new System.Drawing.Point(124, 95);
            this.txtIdEntrepañoDestino.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdEntrepañoDestino.MaxLength = 4;
            this.txtIdEntrepañoDestino.Name = "txtIdEntrepañoDestino";
            this.txtIdEntrepañoDestino.PermitirApostrofo = false;
            this.txtIdEntrepañoDestino.PermitirNegativos = false;
            this.txtIdEntrepañoDestino.Size = new System.Drawing.Size(77, 22);
            this.txtIdEntrepañoDestino.TabIndex = 2;
            this.txtIdEntrepañoDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEntrepañoDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEntrepañoDestino_KeyDown);
            this.txtIdEntrepañoDestino.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEntrepañoDestino_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "Posición :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstanteDestino
            // 
            this.lblEstanteDestino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstanteDestino.Location = new System.Drawing.Point(211, 64);
            this.lblEstanteDestino.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstanteDestino.Name = "lblEstanteDestino";
            this.lblEstanteDestino.Size = new System.Drawing.Size(347, 25);
            this.lblEstanteDestino.TabIndex = 53;
            this.lblEstanteDestino.Text = "label1";
            // 
            // txtIdEstanteDestino
            // 
            this.txtIdEstanteDestino.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdEstanteDestino.Decimales = 2;
            this.txtIdEstanteDestino.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdEstanteDestino.ForeColor = System.Drawing.Color.Black;
            this.txtIdEstanteDestino.Location = new System.Drawing.Point(124, 64);
            this.txtIdEstanteDestino.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdEstanteDestino.MaxLength = 4;
            this.txtIdEstanteDestino.Name = "txtIdEstanteDestino";
            this.txtIdEstanteDestino.PermitirApostrofo = false;
            this.txtIdEstanteDestino.PermitirNegativos = false;
            this.txtIdEstanteDestino.Size = new System.Drawing.Size(77, 22);
            this.txtIdEstanteDestino.TabIndex = 1;
            this.txtIdEstanteDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdEstanteDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdEstanteDestino_KeyDown);
            this.txtIdEstanteDestino.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdEstanteDestino_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(55, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 52;
            this.label5.Text = "Nivel :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPasilloDestino
            // 
            this.lblPasilloDestino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPasilloDestino.Location = new System.Drawing.Point(211, 33);
            this.lblPasilloDestino.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasilloDestino.Name = "lblPasilloDestino";
            this.lblPasilloDestino.Size = new System.Drawing.Size(347, 25);
            this.lblPasilloDestino.TabIndex = 50;
            this.lblPasilloDestino.Text = "label1";
            // 
            // txtIdPasilloDestino
            // 
            this.txtIdPasilloDestino.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPasilloDestino.Decimales = 2;
            this.txtIdPasilloDestino.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPasilloDestino.ForeColor = System.Drawing.Color.Black;
            this.txtIdPasilloDestino.Location = new System.Drawing.Point(124, 33);
            this.txtIdPasilloDestino.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdPasilloDestino.MaxLength = 4;
            this.txtIdPasilloDestino.Name = "txtIdPasilloDestino";
            this.txtIdPasilloDestino.PermitirApostrofo = false;
            this.txtIdPasilloDestino.PermitirNegativos = false;
            this.txtIdPasilloDestino.Size = new System.Drawing.Size(77, 22);
            this.txtIdPasilloDestino.TabIndex = 0;
            this.txtIdPasilloDestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPasilloDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPasilloDestino_KeyDown);
            this.txtIdPasilloDestino.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPasilloDestino_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(55, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 20);
            this.label8.TabIndex = 49;
            this.label8.Text = "Rack :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.AutoSize = false;
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnProcesar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(603, 58);
            this.toolStripBarraMenu.TabIndex = 0;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(54, 55);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.AutoSize = false;
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(10, 58);
            // 
            // btnProcesar
            // 
            this.btnProcesar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesar.Image")));
            this.btnProcesar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(54, 55);
            this.btnProcesar.Text = "&Procesar";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // FrmRU_PosicionCompleta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 356);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpoUbicaciones);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmRU_PosicionCompleta";
            this.ShowIcon = false;
            this.Text = "Reubicación de Ubicación Completa";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRU_PosicionCompleta_Load);
            this.Shown += new System.EventHandler(this.FrmRU_PosicionCompleta_Shown);
            this.gpoUbicaciones.ResumeLayout(false);
            this.gpoUbicaciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpoUbicaciones;
        private System.Windows.Forms.Label lblEntrepaño;
        private SC_ControlsCS.scTextBoxExt txtEntrepaño;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblEstante;
        private SC_ControlsCS.scTextBoxExt txtEstante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPasillo;
        private SC_ControlsCS.scTextBoxExt txtPasillo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEntrepañoDestino;
        private SC_ControlsCS.scTextBoxExt txtIdEntrepañoDestino;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblEstanteDestino;
        private SC_ControlsCS.scTextBoxExt txtIdEstanteDestino;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPasilloDestino;
        private SC_ControlsCS.scTextBoxExt txtIdPasilloDestino;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnProcesar;
    }
}