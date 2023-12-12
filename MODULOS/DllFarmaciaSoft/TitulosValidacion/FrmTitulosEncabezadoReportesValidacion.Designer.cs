namespace DllFarmaciaSoft.TitulosValidacion
{
    partial class FrmTitulosEncabezadoReportesValidacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTitulosEncabezadoReportesValidacion));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.txtTituloReporte = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIdTitulo = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameCliente.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(603, 25);
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
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.cboEstados);
            this.FrameCliente.Controls.Add(this.label2);
            this.FrameCliente.Controls.Add(this.chkActivo);
            this.FrameCliente.Controls.Add(this.txtTituloReporte);
            this.FrameCliente.Controls.Add(this.label10);
            this.FrameCliente.Controls.Add(this.txtIdTitulo);
            this.FrameCliente.Controls.Add(this.label1);
            this.FrameCliente.Location = new System.Drawing.Point(12, 28);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(581, 103);
            this.FrameCliente.TabIndex = 0;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Datos Titulo Beneficiario";
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(96, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(475, 21);
            this.cboEstados.TabIndex = 54;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Estado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkActivo
            // 
            this.chkActivo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActivo.Location = new System.Drawing.Point(475, 47);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(96, 18);
            this.chkActivo.TabIndex = 53;
            this.chkActivo.Text = "Activo";
            this.chkActivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // txtTituloReporte
            // 
            this.txtTituloReporte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTituloReporte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTituloReporte.Decimales = 2;
            this.txtTituloReporte.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTituloReporte.ForeColor = System.Drawing.Color.Black;
            this.txtTituloReporte.Location = new System.Drawing.Point(96, 72);
            this.txtTituloReporte.MaxLength = 100;
            this.txtTituloReporte.Multiline = true;
            this.txtTituloReporte.Name = "txtTituloReporte";
            this.txtTituloReporte.PermitirApostrofo = false;
            this.txtTituloReporte.PermitirNegativos = false;
            this.txtTituloReporte.Size = new System.Drawing.Size(475, 20);
            this.txtTituloReporte.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(20, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "Titulo :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdTitulo
            // 
            this.txtIdTitulo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdTitulo.Decimales = 2;
            this.txtIdTitulo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdTitulo.ForeColor = System.Drawing.Color.Black;
            this.txtIdTitulo.Location = new System.Drawing.Point(96, 46);
            this.txtIdTitulo.MaxLength = 4;
            this.txtIdTitulo.Name = "txtIdTitulo";
            this.txtIdTitulo.PermitirApostrofo = false;
            this.txtIdTitulo.PermitirNegativos = false;
            this.txtIdTitulo.Size = new System.Drawing.Size(59, 20);
            this.txtIdTitulo.TabIndex = 2;
            this.txtIdTitulo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdTitulo.TextChanged += new System.EventHandler(this.txtIdTitulo_TextChanged);
            this.txtIdTitulo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdTitulo_KeyDown);
            this.txtIdTitulo.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdTitulo_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Clave titulo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTitulosEncabezadoReportesValidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 139);
            this.Controls.Add(this.FrameCliente);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmTitulosEncabezadoReportesValidacion";
            this.Text = "Titulos reportes de validación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmTitulosEncabezadoReportesValidacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameCliente.ResumeLayout(false);
            this.FrameCliente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameCliente;
        private SC_ControlsCS.scTextBoxExt txtIdTitulo;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtTituloReporte;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.CheckBox chkActivo;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label2;
    }
}