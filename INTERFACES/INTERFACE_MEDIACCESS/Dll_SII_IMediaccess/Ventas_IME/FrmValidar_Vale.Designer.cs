namespace Dll_SII_IMediaccess.Ventas_IME
{
    partial class FrmValidar_Vale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidar_Vale));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.lblSocioComercial = new System.Windows.Forms.Label();
            this.txtFolioVale = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus2 = new SC_ControlsCS.scLabelStatus();
            this.txtIdSucursal = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus3 = new SC_ControlsCS.scLabelStatus();
            this.btnSurtirReceta = new System.Windows.Forms.Button();
            this.btnSolicitarInformacionVale = new System.Windows.Forms.Button();
            this.txtSocioComercial = new SC_ControlsCS.scTextBoxExt();
            this.scLabelStatus1 = new SC_ControlsCS.scLabelStatus();
            this.FrameInformacion = new System.Windows.Forms.GroupBox();
            this.lblResultadoValidacion = new SC_ControlsCS.scLabelStatus();
            this.scLabelStatus5 = new SC_ControlsCS.scLabelStatus();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(598, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar (CTRL + E) ";
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
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSucursal);
            this.groupBox1.Controls.Add(this.lblSocioComercial);
            this.groupBox1.Controls.Add(this.txtFolioVale);
            this.groupBox1.Controls.Add(this.scLabelStatus2);
            this.groupBox1.Controls.Add(this.txtIdSucursal);
            this.groupBox1.Controls.Add(this.scLabelStatus3);
            this.groupBox1.Controls.Add(this.btnSurtirReceta);
            this.groupBox1.Controls.Add(this.btnSolicitarInformacionVale);
            this.groupBox1.Controls.Add(this.txtSocioComercial);
            this.groupBox1.Controls.Add(this.scLabelStatus1);
            this.groupBox1.Location = new System.Drawing.Point(11, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de vale";
            // 
            // lblSucursal
            // 
            this.lblSucursal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSucursal.Location = new System.Drawing.Point(212, 46);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(353, 21);
            this.lblSucursal.TabIndex = 29;
            this.lblSucursal.Text = "Sucursal :";
            this.lblSucursal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSocioComercial
            // 
            this.lblSocioComercial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSocioComercial.Location = new System.Drawing.Point(212, 19);
            this.lblSocioComercial.Name = "lblSocioComercial";
            this.lblSocioComercial.Size = new System.Drawing.Size(353, 21);
            this.lblSocioComercial.TabIndex = 28;
            this.lblSocioComercial.Text = "Socio :";
            this.lblSocioComercial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFolioVale
            // 
            this.txtFolioVale.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFolioVale.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioVale.Decimales = 2;
            this.txtFolioVale.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFolioVale.ForeColor = System.Drawing.Color.Black;
            this.txtFolioVale.Location = new System.Drawing.Point(109, 74);
            this.txtFolioVale.MaxLength = 50;
            this.txtFolioVale.Name = "txtFolioVale";
            this.txtFolioVale.PermitirApostrofo = false;
            this.txtFolioVale.PermitirNegativos = false;
            this.txtFolioVale.Size = new System.Drawing.Size(97, 20);
            this.txtFolioVale.TabIndex = 2;
            this.txtFolioVale.Text = "E006493943";
            this.txtFolioVale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // scLabelStatus2
            // 
            this.scLabelStatus2.Location = new System.Drawing.Point(16, 74);
            this.scLabelStatus2.Name = "scLabelStatus2";
            this.scLabelStatus2.Size = new System.Drawing.Size(90, 20);
            this.scLabelStatus2.TabIndex = 6;
            this.scLabelStatus2.Text = "Folio de Vale : ";
            this.scLabelStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdSucursal
            // 
            this.txtIdSucursal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdSucursal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdSucursal.Decimales = 2;
            this.txtIdSucursal.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtIdSucursal.ForeColor = System.Drawing.Color.Black;
            this.txtIdSucursal.Location = new System.Drawing.Point(109, 46);
            this.txtIdSucursal.MaxLength = 50;
            this.txtIdSucursal.Name = "txtIdSucursal";
            this.txtIdSucursal.PermitirApostrofo = false;
            this.txtIdSucursal.PermitirNegativos = false;
            this.txtIdSucursal.Size = new System.Drawing.Size(97, 20);
            this.txtIdSucursal.TabIndex = 1;
            this.txtIdSucursal.Text = "E006493943";
            this.txtIdSucursal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdSucursal.TextChanged += new System.EventHandler(this.txtIdSucursal_TextChanged);
            this.txtIdSucursal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdSucursal_KeyDown);
            this.txtIdSucursal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdSucursal_Validating);
            // 
            // scLabelStatus3
            // 
            this.scLabelStatus3.Location = new System.Drawing.Point(16, 46);
            this.scLabelStatus3.Name = "scLabelStatus3";
            this.scLabelStatus3.Size = new System.Drawing.Size(90, 20);
            this.scLabelStatus3.TabIndex = 4;
            this.scLabelStatus3.Text = "Sucursal Socio : ";
            this.scLabelStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSurtirReceta
            // 
            this.btnSurtirReceta.Location = new System.Drawing.Point(109, 101);
            this.btnSurtirReceta.Name = "btnSurtirReceta";
            this.btnSurtirReceta.Size = new System.Drawing.Size(456, 23);
            this.btnSurtirReceta.TabIndex = 4;
            this.btnSurtirReceta.Text = "Surtir receta vale";
            this.btnSurtirReceta.UseVisualStyleBackColor = true;
            this.btnSurtirReceta.Click += new System.EventHandler(this.btnSurtirReceta_Click);
            // 
            // btnSolicitarInformacionVale
            // 
            this.btnSolicitarInformacionVale.Location = new System.Drawing.Point(212, 73);
            this.btnSolicitarInformacionVale.Name = "btnSolicitarInformacionVale";
            this.btnSolicitarInformacionVale.Size = new System.Drawing.Size(353, 23);
            this.btnSolicitarInformacionVale.TabIndex = 3;
            this.btnSolicitarInformacionVale.Text = "Solicitar información de Vale";
            this.btnSolicitarInformacionVale.UseVisualStyleBackColor = true;
            this.btnSolicitarInformacionVale.Click += new System.EventHandler(this.btnSolicitarInformacionVale_Click);
            // 
            // txtSocioComercial
            // 
            this.txtSocioComercial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSocioComercial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSocioComercial.Decimales = 2;
            this.txtSocioComercial.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtSocioComercial.ForeColor = System.Drawing.Color.Black;
            this.txtSocioComercial.Location = new System.Drawing.Point(109, 19);
            this.txtSocioComercial.MaxLength = 50;
            this.txtSocioComercial.Name = "txtSocioComercial";
            this.txtSocioComercial.PermitirApostrofo = false;
            this.txtSocioComercial.PermitirNegativos = false;
            this.txtSocioComercial.Size = new System.Drawing.Size(97, 20);
            this.txtSocioComercial.TabIndex = 0;
            this.txtSocioComercial.Text = "E006493943";
            this.txtSocioComercial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSocioComercial.TextChanged += new System.EventHandler(this.txtSocioComercial_TextChanged);
            this.txtSocioComercial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSocioComercial_KeyDown);
            this.txtSocioComercial.Validating += new System.ComponentModel.CancelEventHandler(this.txtSocioComercial_Validating);
            // 
            // scLabelStatus1
            // 
            this.scLabelStatus1.Location = new System.Drawing.Point(16, 19);
            this.scLabelStatus1.Name = "scLabelStatus1";
            this.scLabelStatus1.Size = new System.Drawing.Size(90, 20);
            this.scLabelStatus1.TabIndex = 0;
            this.scLabelStatus1.Text = "Socio comercial : ";
            this.scLabelStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameInformacion
            // 
            this.FrameInformacion.Controls.Add(this.lblResultadoValidacion);
            this.FrameInformacion.Controls.Add(this.scLabelStatus5);
            this.FrameInformacion.Location = new System.Drawing.Point(11, 162);
            this.FrameInformacion.Name = "FrameInformacion";
            this.FrameInformacion.Size = new System.Drawing.Size(576, 49);
            this.FrameInformacion.TabIndex = 2;
            this.FrameInformacion.TabStop = false;
            this.FrameInformacion.Text = "Información";
            // 
            // lblResultadoValidacion
            // 
            this.lblResultadoValidacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultadoValidacion.Location = new System.Drawing.Point(128, 16);
            this.lblResultadoValidacion.Name = "lblResultadoValidacion";
            this.lblResultadoValidacion.Size = new System.Drawing.Size(437, 21);
            this.lblResultadoValidacion.TabIndex = 5;
            this.lblResultadoValidacion.Text = "Elegibilidad : ";
            this.lblResultadoValidacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scLabelStatus5
            // 
            this.scLabelStatus5.Location = new System.Drawing.Point(6, 16);
            this.scLabelStatus5.Name = "scLabelStatus5";
            this.scLabelStatus5.Size = new System.Drawing.Size(123, 21);
            this.scLabelStatus5.TabIndex = 11;
            this.scLabelStatus5.Text = "Resultado validación : ";
            this.scLabelStatus5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValidar_Vale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 220);
            this.Controls.Add(this.FrameInformacion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmValidar_Vale";
            this.Text = "Validar vale";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidar_Vale_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameInformacion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameInformacion;
        private SC_ControlsCS.scTextBoxExt txtSocioComercial;
        private SC_ControlsCS.scLabelStatus scLabelStatus1;
        private System.Windows.Forms.Button btnSolicitarInformacionVale;
        private System.Windows.Forms.Button btnSurtirReceta;
        private SC_ControlsCS.scLabelStatus lblResultadoValidacion;
        private SC_ControlsCS.scLabelStatus scLabelStatus5;
        private SC_ControlsCS.scTextBoxExt txtIdSucursal;
        private SC_ControlsCS.scLabelStatus scLabelStatus3;
        private SC_ControlsCS.scTextBoxExt txtFolioVale;
        private SC_ControlsCS.scLabelStatus scLabelStatus2;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label lblSocioComercial;
    }
}