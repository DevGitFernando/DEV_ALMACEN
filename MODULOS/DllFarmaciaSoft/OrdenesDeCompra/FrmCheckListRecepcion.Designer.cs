namespace DllFarmaciaSoft.OrdenesDeCompra
{
    partial class FrmCheckListRecepcion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckListRecepcion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkRechazo_Firma = new System.Windows.Forms.CheckBox();
            this.chkRechazo = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkNO_Firma = new System.Windows.Forms.CheckBox();
            this.chkNO = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSI_Firma = new System.Windows.Forms.CheckBox();
            this.chkSI = new System.Windows.Forms.CheckBox();
            this.cboGrupos = new SC_ControlsCS.scComboBoxExt();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMotivo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.nmValor = new System.Windows.Forms.NumericUpDown();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmValor)).BeginInit();
            this.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(703, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
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
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmValor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cboGrupos);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMotivo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(671, 390);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkRechazo_Firma);
            this.groupBox4.Controls.Add(this.chkRechazo);
            this.groupBox4.Location = new System.Drawing.Point(123, 312);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(528, 65);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Repuesta Rechazo";
            // 
            // chkRechazo_Firma
            // 
            this.chkRechazo_Firma.Location = new System.Drawing.Point(113, 23);
            this.chkRechazo_Firma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRechazo_Firma.Name = "chkRechazo_Firma";
            this.chkRechazo_Firma.Size = new System.Drawing.Size(129, 22);
            this.chkRechazo_Firma.TabIndex = 1;
            this.chkRechazo_Firma.Text = "Requiere Firma";
            this.chkRechazo_Firma.UseVisualStyleBackColor = true;
            // 
            // chkRechazo
            // 
            this.chkRechazo.Location = new System.Drawing.Point(312, 23);
            this.chkRechazo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRechazo.Name = "chkRechazo";
            this.chkRechazo.Size = new System.Drawing.Size(137, 22);
            this.chkRechazo.TabIndex = 0;
            this.chkRechazo.Text = "Mostrar default";
            this.chkRechazo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkNO_Firma);
            this.groupBox3.Controls.Add(this.chkNO);
            this.groupBox3.Location = new System.Drawing.Point(123, 243);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(528, 65);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Repuesta NO";
            // 
            // chkNO_Firma
            // 
            this.chkNO_Firma.Location = new System.Drawing.Point(113, 23);
            this.chkNO_Firma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkNO_Firma.Name = "chkNO_Firma";
            this.chkNO_Firma.Size = new System.Drawing.Size(129, 22);
            this.chkNO_Firma.TabIndex = 1;
            this.chkNO_Firma.Text = "Requiere Firma";
            this.chkNO_Firma.UseVisualStyleBackColor = true;
            // 
            // chkNO
            // 
            this.chkNO.Location = new System.Drawing.Point(312, 23);
            this.chkNO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkNO.Name = "chkNO";
            this.chkNO.Size = new System.Drawing.Size(137, 22);
            this.chkNO.TabIndex = 0;
            this.chkNO.Text = "Mostrar default";
            this.chkNO.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSI_Firma);
            this.groupBox2.Controls.Add(this.chkSI);
            this.groupBox2.Location = new System.Drawing.Point(123, 175);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(528, 65);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Repuesta SI";
            // 
            // chkSI_Firma
            // 
            this.chkSI_Firma.Location = new System.Drawing.Point(113, 23);
            this.chkSI_Firma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSI_Firma.Name = "chkSI_Firma";
            this.chkSI_Firma.Size = new System.Drawing.Size(129, 22);
            this.chkSI_Firma.TabIndex = 1;
            this.chkSI_Firma.Text = "Requiere Firma";
            this.chkSI_Firma.UseVisualStyleBackColor = true;
            // 
            // chkSI
            // 
            this.chkSI.Location = new System.Drawing.Point(312, 23);
            this.chkSI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSI.Name = "chkSI";
            this.chkSI.Size = new System.Drawing.Size(137, 22);
            this.chkSI.TabIndex = 0;
            this.chkSI.Text = "Mostrar default";
            this.chkSI.UseVisualStyleBackColor = true;
            // 
            // cboGrupos
            // 
            this.cboGrupos.BackColorEnabled = System.Drawing.Color.White;
            this.cboGrupos.Data = "";
            this.cboGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupos.Filtro = " 1 = 1";
            this.cboGrupos.FormattingEnabled = true;
            this.cboGrupos.ListaItemsBusqueda = 20;
            this.cboGrupos.Location = new System.Drawing.Point(123, 26);
            this.cboGrupos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboGrupos.MostrarToolTip = false;
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Size = new System.Drawing.Size(527, 24);
            this.cboGrupos.TabIndex = 0;
            this.cboGrupos.SelectedIndexChanged += new System.EventHandler(this.cboGrupos_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(232, 63);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(101, 17);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "CANCELADO";
            this.lblStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Grupo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMotivo
            // 
            this.txtMotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtMotivo.Decimales = 2;
            this.txtMotivo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtMotivo.ForeColor = System.Drawing.Color.Black;
            this.txtMotivo.Location = new System.Drawing.Point(123, 58);
            this.txtMotivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMotivo.MaxLength = 3;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.PermitirApostrofo = false;
            this.txtMotivo.PermitirNegativos = false;
            this.txtMotivo.Size = new System.Drawing.Size(100, 22);
            this.txtMotivo.TabIndex = 1;
            this.txtMotivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMotivo.Validating += new System.ComponentModel.CancelEventHandler(this.txtMotivo_Validating);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Motivo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Descripción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDescripcion.Decimales = 2;
            this.txtDescripcion.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDescripcion.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Location = new System.Drawing.Point(123, 87);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.PermitirApostrofo = false;
            this.txtDescripcion.PermitirNegativos = false;
            this.txtDescripcion.Size = new System.Drawing.Size(527, 50);
            this.txtDescripcion.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Valor :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmValor
            // 
            this.nmValor.Location = new System.Drawing.Point(123, 144);
            this.nmValor.Name = "nmValor";
            this.nmValor.Size = new System.Drawing.Size(120, 22);
            this.nmValor.TabIndex = 15;
            this.nmValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmCheckListRecepcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 435);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmCheckListRecepcion";
            this.Text = "Items para evaluación a proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmCheckListRecepcion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmValor)).EndInit();
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
        private SC_ControlsCS.scComboBoxExt cboGrupos;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtMotivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtDescripcion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkRechazo_Firma;
        private System.Windows.Forms.CheckBox chkRechazo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkNO_Firma;
        private System.Windows.Forms.CheckBox chkNO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkSI_Firma;
        private System.Windows.Forms.CheckBox chkSI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmValor;
    }
}