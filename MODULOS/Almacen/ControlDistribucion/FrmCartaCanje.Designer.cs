namespace Almacen.ControlDistribucion
{
    partial class FrmCartaCanje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCartaCanje));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMeses = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFirma = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTitulo3 = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitulo2 = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTitulo1 = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAquienCo = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExpEn = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeses)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnGuardar,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(721, 25);
            this.toolStripBarraMenu.TabIndex = 8;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudMeses);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtFirma);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTitulo3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTitulo2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTitulo1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAquienCo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtExpEn);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 441);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de la Carta de canje";
            // 
            // nudMeses
            // 
            this.nudMeses.Location = new System.Drawing.Point(132, 89);
            this.nudMeses.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudMeses.Name = "nudMeses";
            this.nudMeses.Size = new System.Drawing.Size(50, 20);
            this.nudMeses.TabIndex = 61;
            this.nudMeses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 16);
            this.label6.TabIndex = 60;
            this.label6.Text = "Firma :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFirma
            // 
            this.txtFirma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFirma.Decimales = 2;
            this.txtFirma.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFirma.ForeColor = System.Drawing.Color.Black;
            this.txtFirma.Location = new System.Drawing.Point(132, 355);
            this.txtFirma.MaxLength = 500;
            this.txtFirma.Multiline = true;
            this.txtFirma.Name = "txtFirma";
            this.txtFirma.PermitirApostrofo = false;
            this.txtFirma.PermitirNegativos = false;
            this.txtFirma.Size = new System.Drawing.Size(551, 69);
            this.txtFirma.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 58;
            this.label5.Text = "Titulo 3 :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitulo3
            // 
            this.txtTitulo3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo3.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTitulo3.Decimales = 2;
            this.txtTitulo3.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTitulo3.ForeColor = System.Drawing.Color.Black;
            this.txtTitulo3.Location = new System.Drawing.Point(132, 275);
            this.txtTitulo3.MaxLength = 500;
            this.txtTitulo3.Multiline = true;
            this.txtTitulo3.Name = "txtTitulo3";
            this.txtTitulo3.PermitirApostrofo = false;
            this.txtTitulo3.PermitirNegativos = false;
            this.txtTitulo3.Size = new System.Drawing.Size(551, 74);
            this.txtTitulo3.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 56;
            this.label4.Text = "Titulo 2 :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitulo2
            // 
            this.txtTitulo2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo2.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTitulo2.Decimales = 2;
            this.txtTitulo2.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTitulo2.ForeColor = System.Drawing.Color.Black;
            this.txtTitulo2.Location = new System.Drawing.Point(132, 195);
            this.txtTitulo2.MaxLength = 500;
            this.txtTitulo2.Multiline = true;
            this.txtTitulo2.Name = "txtTitulo2";
            this.txtTitulo2.PermitirApostrofo = false;
            this.txtTitulo2.PermitirNegativos = false;
            this.txtTitulo2.Size = new System.Drawing.Size(551, 74);
            this.txtTitulo2.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 54;
            this.label2.Text = "Titulo 1 :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitulo1
            // 
            this.txtTitulo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTitulo1.Decimales = 2;
            this.txtTitulo1.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTitulo1.ForeColor = System.Drawing.Color.Black;
            this.txtTitulo1.Location = new System.Drawing.Point(132, 115);
            this.txtTitulo1.MaxLength = 500;
            this.txtTitulo1.Multiline = true;
            this.txtTitulo1.Name = "txtTitulo1";
            this.txtTitulo1.PermitirApostrofo = false;
            this.txtTitulo1.PermitirNegativos = false;
            this.txtTitulo1.Size = new System.Drawing.Size(551, 74);
            this.txtTitulo1.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 52;
            this.label1.Text = "A quién corresponda :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAquienCo
            // 
            this.txtAquienCo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAquienCo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAquienCo.Decimales = 2;
            this.txtAquienCo.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtAquienCo.ForeColor = System.Drawing.Color.Black;
            this.txtAquienCo.Location = new System.Drawing.Point(132, 63);
            this.txtAquienCo.MaxLength = 100;
            this.txtAquienCo.Name = "txtAquienCo";
            this.txtAquienCo.PermitirApostrofo = false;
            this.txtAquienCo.PermitirNegativos = false;
            this.txtAquienCo.Size = new System.Drawing.Size(551, 20);
            this.txtAquienCo.TabIndex = 51;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 20);
            this.label7.TabIndex = 50;
            this.label7.Text = "Meses Caducar :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "Expedido en :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExpEn
            // 
            this.txtExpEn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpEn.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtExpEn.Decimales = 2;
            this.txtExpEn.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtExpEn.ForeColor = System.Drawing.Color.Black;
            this.txtExpEn.Location = new System.Drawing.Point(132, 37);
            this.txtExpEn.MaxLength = 100;
            this.txtExpEn.Name = "txtExpEn";
            this.txtExpEn.PermitirApostrofo = false;
            this.txtExpEn.PermitirNegativos = false;
            this.txtExpEn.Size = new System.Drawing.Size(551, 20);
            this.txtExpEn.TabIndex = 3;
            // 
            // FrmCartaCanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 481);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmCartaCanje";
            this.Text = "Carta de Canje";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCartaCanje_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtTitulo3;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtTitulo2;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtTitulo1;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtAquienCo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtExpEn;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scTextBoxExt txtFirma;
        private System.Windows.Forms.NumericUpDown nudMeses;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
    }
}