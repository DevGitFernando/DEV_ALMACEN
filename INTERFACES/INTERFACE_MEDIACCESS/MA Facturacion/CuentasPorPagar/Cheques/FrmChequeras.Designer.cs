namespace MA_Facturacion.CuentasPorPagar
{
    partial class FrmChequeras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChequeras));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumDeSerie = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUltimoFolio = new System.Windows.Forms.Label();
            this.lblUltimo = new System.Windows.Forms.Label();
            this.lblBanco = new System.Windows.Forms.Label();
            this.txtBanco = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolioFin = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolioInicio = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumDeSerie);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblUltimoFolio);
            this.groupBox1.Controls.Add(this.lblUltimo);
            this.groupBox1.Controls.Add(this.lblBanco);
            this.groupBox1.Controls.Add(this.txtBanco);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFolioFin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFolioInicio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 155);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Chequera";
            // 
            // txtNumDeSerie
            // 
            this.txtNumDeSerie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNumDeSerie.Decimales = 2;
            this.txtNumDeSerie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNumDeSerie.ForeColor = System.Drawing.Color.Black;
            this.txtNumDeSerie.Location = new System.Drawing.Point(101, 128);
            this.txtNumDeSerie.MaxLength = 20;
            this.txtNumDeSerie.Name = "txtNumDeSerie";
            this.txtNumDeSerie.PermitirApostrofo = false;
            this.txtNumDeSerie.PermitirNegativos = false;
            this.txtNumDeSerie.Size = new System.Drawing.Size(112, 20);
            this.txtNumDeSerie.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Número de Serie :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUltimoFolio
            // 
            this.lblUltimoFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUltimoFolio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUltimoFolio.Location = new System.Drawing.Point(380, 127);
            this.lblUltimoFolio.Name = "lblUltimoFolio";
            this.lblUltimoFolio.Size = new System.Drawing.Size(111, 20);
            this.lblUltimoFolio.TabIndex = 5;
            this.lblUltimoFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUltimoFolio.Visible = false;
            // 
            // lblUltimo
            // 
            this.lblUltimo.Location = new System.Drawing.Point(307, 131);
            this.lblUltimo.Name = "lblUltimo";
            this.lblUltimo.Size = new System.Drawing.Size(67, 13);
            this.lblUltimo.TabIndex = 49;
            this.lblUltimo.Text = "Último Folio :";
            this.lblUltimo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUltimo.Visible = false;
            // 
            // lblBanco
            // 
            this.lblBanco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanco.Location = new System.Drawing.Point(175, 76);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(316, 20);
            this.lblBanco.TabIndex = 48;
            this.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBanco
            // 
            this.txtBanco.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBanco.Decimales = 2;
            this.txtBanco.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBanco.ForeColor = System.Drawing.Color.Black;
            this.txtBanco.Location = new System.Drawing.Point(101, 76);
            this.txtBanco.MaxLength = 3;
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.PermitirApostrofo = false;
            this.txtBanco.PermitirNegativos = false;
            this.txtBanco.Size = new System.Drawing.Size(68, 20);
            this.txtBanco.TabIndex = 2;
            this.txtBanco.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBanco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBanco_KeyDown);
            this.txtBanco.Validating += new System.ComponentModel.CancelEventHandler(this.txtBanco_Validating);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Banco :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioFin
            // 
            this.txtFolioFin.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFin.Decimales = 2;
            this.txtFolioFin.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFin.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFin.Location = new System.Drawing.Point(380, 99);
            this.txtFolioFin.MaxLength = 6;
            this.txtFolioFin.Name = "txtFolioFin";
            this.txtFolioFin.PermitirApostrofo = false;
            this.txtFolioFin.PermitirNegativos = false;
            this.txtFolioFin.Size = new System.Drawing.Size(111, 20);
            this.txtFolioFin.TabIndex = 4;
            this.txtFolioFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(325, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Folio fin :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioInicio
            // 
            this.txtFolioInicio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioInicio.Decimales = 2;
            this.txtFolioInicio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioInicio.ForeColor = System.Drawing.Color.Black;
            this.txtFolioInicio.Location = new System.Drawing.Point(101, 102);
            this.txtFolioInicio.MaxLength = 6;
            this.txtFolioInicio.Name = "txtFolioInicio";
            this.txtFolioInicio.PermitirApostrofo = false;
            this.txtFolioInicio.PermitirNegativos = false;
            this.txtFolioInicio.Size = new System.Drawing.Size(112, 20);
            this.txtFolioInicio.TabIndex = 3;
            this.txtFolioInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Folio inico :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(101, 50);
            this.txtDescripcion.MaxLength = 50;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(390, 20);
            this.txtDescripcion.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Descripción :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(172, 28);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 5;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(101, 24);
            this.txtId.MaxLength = 6;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(68, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(524, 25);
            this.toolStripBarraMenu.TabIndex = 0;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            // FrmChequeras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 191);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmChequeras";
            this.Text = "Chequeras";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmChequeras_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scTextBoxExt txtFolioFin;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtFolioInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBanco;
        private SC_ControlsCS.scTextBoxExt txtBanco;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUltimo;
        private System.Windows.Forms.Label lblUltimoFolio;
        private SC_ControlsCS.scTextBoxExt txtNumDeSerie;
        private System.Windows.Forms.Label label6;
    }
}