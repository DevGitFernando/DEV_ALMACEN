namespace Dll_IFacturacion.XSA.ObservacionesCancelacion
{
    partial class FrmObservaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmObservaciones));
            this.FrameObservaciones = new System.Windows.Forms.GroupBox();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.cboMotivosCancelacion = new SC_ControlsCS.scComboBoxExt();
            this.Frame_UUID_Relacionado = new System.Windows.Forms.GroupBox();
            this.chkTieneDocumentoRelacionado = new System.Windows.Forms.CheckBox();
            this.txtRelacion__UUID = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelacion__Serie = new SC_ControlsCS.scTextBoxExt();
            this.txtRelacion__Folio = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameObservaciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Frame_UUID_Relacionado.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameObservaciones
            // 
            this.FrameObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameObservaciones.Controls.Add(this.txtObservaciones);
            this.FrameObservaciones.Location = new System.Drawing.Point(9, 198);
            this.FrameObservaciones.Name = "FrameObservaciones";
            this.FrameObservaciones.Size = new System.Drawing.Size(475, 138);
            this.FrameObservaciones.TabIndex = 2;
            this.FrameObservaciones.TabStop = false;
            this.FrameObservaciones.Text = "Descripción del motivo de cancelación";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(12, 18);
            this.txtObservaciones.MaxLength = 500;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(453, 109);
            this.txtObservaciones.TabIndex = 0;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Location = new System.Drawing.Point(269, 342);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(104, 30);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(379, 342);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(104, 30);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFarmacia);
            this.groupBox1.Controls.Add(this.cboMotivosCancelacion);
            this.groupBox1.Location = new System.Drawing.Point(9, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motivos de cancelación";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.Location = new System.Drawing.Point(12, 22);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(104, 17);
            this.lblFarmacia.TabIndex = 15;
            this.lblFarmacia.Text = "Clave motivo :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMotivosCancelacion
            // 
            this.cboMotivosCancelacion.BackColorEnabled = System.Drawing.Color.White;
            this.cboMotivosCancelacion.Data = "";
            this.cboMotivosCancelacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotivosCancelacion.Filtro = " 1 = 1";
            this.cboMotivosCancelacion.ListaItemsBusqueda = 20;
            this.cboMotivosCancelacion.Location = new System.Drawing.Point(120, 20);
            this.cboMotivosCancelacion.MostrarToolTip = false;
            this.cboMotivosCancelacion.Name = "cboMotivosCancelacion";
            this.cboMotivosCancelacion.Size = new System.Drawing.Size(338, 21);
            this.cboMotivosCancelacion.TabIndex = 0;
            this.cboMotivosCancelacion.SelectedIndexChanged += new System.EventHandler(this.cboMotivosCancelacion_SelectedIndexChanged);
            // 
            // Frame_UUID_Relacionado
            // 
            this.Frame_UUID_Relacionado.Controls.Add(this.chkTieneDocumentoRelacionado);
            this.Frame_UUID_Relacionado.Controls.Add(this.txtRelacion__UUID);
            this.Frame_UUID_Relacionado.Controls.Add(this.label7);
            this.Frame_UUID_Relacionado.Controls.Add(this.txtRelacion__Serie);
            this.Frame_UUID_Relacionado.Controls.Add(this.txtRelacion__Folio);
            this.Frame_UUID_Relacionado.Controls.Add(this.label6);
            this.Frame_UUID_Relacionado.Controls.Add(this.label5);
            this.Frame_UUID_Relacionado.Location = new System.Drawing.Point(9, 92);
            this.Frame_UUID_Relacionado.Margin = new System.Windows.Forms.Padding(2);
            this.Frame_UUID_Relacionado.Name = "Frame_UUID_Relacionado";
            this.Frame_UUID_Relacionado.Padding = new System.Windows.Forms.Padding(2);
            this.Frame_UUID_Relacionado.Size = new System.Drawing.Size(475, 101);
            this.Frame_UUID_Relacionado.TabIndex = 1;
            this.Frame_UUID_Relacionado.TabStop = false;
            this.Frame_UUID_Relacionado.Text = "Información CFDI relacionado";
            // 
            // chkTieneDocumentoRelacionado
            // 
            this.chkTieneDocumentoRelacionado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.Location = new System.Drawing.Point(315, -1);
            this.chkTieneDocumentoRelacionado.Margin = new System.Windows.Forms.Padding(2);
            this.chkTieneDocumentoRelacionado.Name = "chkTieneDocumentoRelacionado";
            this.chkTieneDocumentoRelacionado.Size = new System.Drawing.Size(134, 20);
            this.chkTieneDocumentoRelacionado.TabIndex = 3;
            this.chkTieneDocumentoRelacionado.Text = "Sustituye a otro CFDI";
            this.chkTieneDocumentoRelacionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.UseVisualStyleBackColor = true;
            // 
            // txtRelacion__UUID
            // 
            this.txtRelacion__UUID.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__UUID.Decimales = 2;
            this.txtRelacion__UUID.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__UUID.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__UUID.Location = new System.Drawing.Point(58, 69);
            this.txtRelacion__UUID.MaxLength = 10;
            this.txtRelacion__UUID.Name = "txtRelacion__UUID";
            this.txtRelacion__UUID.PermitirApostrofo = false;
            this.txtRelacion__UUID.PermitirNegativos = false;
            this.txtRelacion__UUID.Size = new System.Drawing.Size(400, 20);
            this.txtRelacion__UUID.TabIndex = 2;
            this.txtRelacion__UUID.Text = "0123456789012345678901234567890123456789";
            this.txtRelacion__UUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "UUID : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRelacion__Serie
            // 
            this.txtRelacion__Serie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRelacion__Serie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Serie.Decimales = 2;
            this.txtRelacion__Serie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRelacion__Serie.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Serie.Location = new System.Drawing.Point(58, 20);
            this.txtRelacion__Serie.MaxLength = 10;
            this.txtRelacion__Serie.Name = "txtRelacion__Serie";
            this.txtRelacion__Serie.PermitirApostrofo = false;
            this.txtRelacion__Serie.PermitirNegativos = false;
            this.txtRelacion__Serie.Size = new System.Drawing.Size(93, 20);
            this.txtRelacion__Serie.TabIndex = 0;
            this.txtRelacion__Serie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRelacion__Serie.TextChanged += new System.EventHandler(this.txtRelacion__Serie_TextChanged);
            this.txtRelacion__Serie.Validating += new System.ComponentModel.CancelEventHandler(this.txtRelacion__Serie_Validating);
            // 
            // txtRelacion__Folio
            // 
            this.txtRelacion__Folio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRelacion__Folio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Folio.Decimales = 2;
            this.txtRelacion__Folio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__Folio.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Folio.Location = new System.Drawing.Point(58, 44);
            this.txtRelacion__Folio.MaxLength = 10;
            this.txtRelacion__Folio.Name = "txtRelacion__Folio";
            this.txtRelacion__Folio.PermitirApostrofo = false;
            this.txtRelacion__Folio.PermitirNegativos = false;
            this.txtRelacion__Folio.Size = new System.Drawing.Size(93, 20);
            this.txtRelacion__Folio.TabIndex = 1;
            this.txtRelacion__Folio.Text = "0123456789";
            this.txtRelacion__Folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRelacion__Folio.TextChanged += new System.EventHandler(this.txtRelacion__Folio_TextChanged);
            this.txtRelacion__Folio.Validating += new System.ComponentModel.CancelEventHandler(this.txtRelacion__Folio_Validating);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Serie : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Folio : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(494, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // FrmObservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 381);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.Frame_UUID_Relacionado);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.FrameObservaciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximumSize = new System.Drawing.Size(750, 600);
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "FrmObservaciones";
            this.Text = "Cancelación de CFDI";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmObservaciones_Load);
            this.FrameObservaciones.ResumeLayout(false);
            this.FrameObservaciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.Frame_UUID_Relacionado.ResumeLayout(false);
            this.Frame_UUID_Relacionado.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameObservaciones;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scComboBoxExt cboMotivosCancelacion;
        private System.Windows.Forms.GroupBox Frame_UUID_Relacionado;
        private System.Windows.Forms.CheckBox chkTieneDocumentoRelacionado;
        private SC_ControlsCS.scTextBoxExt txtRelacion__UUID;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Serie;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Folio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}