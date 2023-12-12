namespace DllFarmaciaSoft.Facturacion
{
    partial class FrmConfigImpresionVentaDetalle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigImpresionVentaDetalle));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.cboUnidades = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.FrameBeneficiario = new System.Windows.Forms.GroupBox();
            this.chk016_SoloEtiqueta_FolioDocumento = new System.Windows.Forms.CheckBox();
            this.chk014_SoloEtiqueta_FolioReferencia = new System.Windows.Forms.CheckBox();
            this.chk012_SoloEtiqueta_Beneficiario = new System.Windows.Forms.CheckBox();
            this.chk015_FolioDocumento = new System.Windows.Forms.CheckBox();
            this.chk011_Beneficiario = new System.Windows.Forms.CheckBox();
            this.chk013_FolioReferencia = new System.Windows.Forms.CheckBox();
            this.FramePrograma = new System.Windows.Forms.GroupBox();
            this.chk017_Presentacion = new System.Windows.Forms.CheckBox();
            this.chk010_SoloEtiqueta_SubPrograma = new System.Windows.Forms.CheckBox();
            this.chk008_SoloEtiquetaPrograma = new System.Windows.Forms.CheckBox();
            this.chk007_Programa = new System.Windows.Forms.CheckBox();
            this.chk009_SubPrograma = new System.Windows.Forms.CheckBox();
            this.FrameCliente = new System.Windows.Forms.GroupBox();
            this.chk006_MostrarDescripcion_Perfil = new System.Windows.Forms.CheckBox();
            this.chk004_SoloEtiqueta_SubCliente = new System.Windows.Forms.CheckBox();
            this.chk002_SoloEtiqueta_Cliente = new System.Windows.Forms.CheckBox();
            this.chk005_IntercambiarSubCliente_Cliente = new System.Windows.Forms.CheckBox();
            this.chk001_Cliente = new System.Windows.Forms.CheckBox();
            this.chk003_SubCliente = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameUnidades.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.FrameBeneficiario.SuspendLayout();
            this.FramePrograma.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(764, 25);
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
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.cboUnidades);
            this.FrameUnidades.Controls.Add(this.label2);
            this.FrameUnidades.Location = new System.Drawing.Point(11, 28);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(745, 52);
            this.FrameUnidades.TabIndex = 1;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Unidades";
            // 
            // cboUnidades
            // 
            this.cboUnidades.BackColorEnabled = System.Drawing.Color.White;
            this.cboUnidades.Data = "";
            this.cboUnidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidades.Filtro = " 1 = 1";
            this.cboUnidades.FormattingEnabled = true;
            this.cboUnidades.ListaItemsBusqueda = 20;
            this.cboUnidades.Location = new System.Drawing.Point(77, 21);
            this.cboUnidades.MostrarToolTip = false;
            this.cboUnidades.Name = "cboUnidades";
            this.cboUnidades.Size = new System.Drawing.Size(659, 21);
            this.cboUnidades.TabIndex = 0;
            this.cboUnidades.SelectedIndexChanged += new System.EventHandler(this.cboUnidades_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Unidad :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.FrameBeneficiario);
            this.FramePrincipal.Controls.Add(this.FramePrograma);
            this.FramePrincipal.Controls.Add(this.FrameCliente);
            this.FramePrincipal.Location = new System.Drawing.Point(11, 81);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(745, 205);
            this.FramePrincipal.TabIndex = 2;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Opciones a mostrar / ocultar";
            // 
            // FrameBeneficiario
            // 
            this.FrameBeneficiario.Controls.Add(this.chk016_SoloEtiqueta_FolioDocumento);
            this.FrameBeneficiario.Controls.Add(this.chk014_SoloEtiqueta_FolioReferencia);
            this.FrameBeneficiario.Controls.Add(this.chk012_SoloEtiqueta_Beneficiario);
            this.FrameBeneficiario.Controls.Add(this.chk015_FolioDocumento);
            this.FrameBeneficiario.Controls.Add(this.chk011_Beneficiario);
            this.FrameBeneficiario.Controls.Add(this.chk013_FolioReferencia);
            this.FrameBeneficiario.Location = new System.Drawing.Point(497, 19);
            this.FrameBeneficiario.Name = "FrameBeneficiario";
            this.FrameBeneficiario.Size = new System.Drawing.Size(239, 176);
            this.FrameBeneficiario.TabIndex = 2;
            this.FrameBeneficiario.TabStop = false;
            this.FrameBeneficiario.Text = "Beneficiario, Folio referencia, Documento";
            // 
            // chk016_SoloEtiqueta_FolioDocumento
            // 
            this.chk016_SoloEtiqueta_FolioDocumento.Location = new System.Drawing.Point(48, 147);
            this.chk016_SoloEtiqueta_FolioDocumento.Name = "chk016_SoloEtiqueta_FolioDocumento";
            this.chk016_SoloEtiqueta_FolioDocumento.Size = new System.Drawing.Size(167, 18);
            this.chk016_SoloEtiqueta_FolioDocumento.TabIndex = 2;
            this.chk016_SoloEtiqueta_FolioDocumento.Text = "Solo etiqueta folio documento";
            this.chk016_SoloEtiqueta_FolioDocumento.UseVisualStyleBackColor = true;
            // 
            // chk014_SoloEtiqueta_FolioReferencia
            // 
            this.chk014_SoloEtiqueta_FolioReferencia.Location = new System.Drawing.Point(48, 90);
            this.chk014_SoloEtiqueta_FolioReferencia.Name = "chk014_SoloEtiqueta_FolioReferencia";
            this.chk014_SoloEtiqueta_FolioReferencia.Size = new System.Drawing.Size(167, 18);
            this.chk014_SoloEtiqueta_FolioReferencia.TabIndex = 0;
            this.chk014_SoloEtiqueta_FolioReferencia.Text = "Solo etiqueta folio referencia";
            this.chk014_SoloEtiqueta_FolioReferencia.UseVisualStyleBackColor = true;
            // 
            // chk012_SoloEtiqueta_Beneficiario
            // 
            this.chk012_SoloEtiqueta_Beneficiario.Location = new System.Drawing.Point(48, 36);
            this.chk012_SoloEtiqueta_Beneficiario.Name = "chk012_SoloEtiqueta_Beneficiario";
            this.chk012_SoloEtiqueta_Beneficiario.Size = new System.Drawing.Size(167, 18);
            this.chk012_SoloEtiqueta_Beneficiario.TabIndex = 4;
            this.chk012_SoloEtiqueta_Beneficiario.Text = "Solo etiqueta beneficiario";
            this.chk012_SoloEtiqueta_Beneficiario.UseVisualStyleBackColor = true;
            // 
            // chk015_FolioDocumento
            // 
            this.chk015_FolioDocumento.Location = new System.Drawing.Point(24, 123);
            this.chk015_FolioDocumento.Name = "chk015_FolioDocumento";
            this.chk015_FolioDocumento.Size = new System.Drawing.Size(131, 18);
            this.chk015_FolioDocumento.TabIndex = 1;
            this.chk015_FolioDocumento.Text = "Folio documento";
            this.chk015_FolioDocumento.UseVisualStyleBackColor = true;
            // 
            // chk011_Beneficiario
            // 
            this.chk011_Beneficiario.Location = new System.Drawing.Point(24, 19);
            this.chk011_Beneficiario.Name = "chk011_Beneficiario";
            this.chk011_Beneficiario.Size = new System.Drawing.Size(131, 18);
            this.chk011_Beneficiario.TabIndex = 3;
            this.chk011_Beneficiario.Text = "Beneficiario";
            this.chk011_Beneficiario.UseVisualStyleBackColor = true;
            // 
            // chk013_FolioReferencia
            // 
            this.chk013_FolioReferencia.Location = new System.Drawing.Point(24, 71);
            this.chk013_FolioReferencia.Name = "chk013_FolioReferencia";
            this.chk013_FolioReferencia.Size = new System.Drawing.Size(131, 18);
            this.chk013_FolioReferencia.TabIndex = 5;
            this.chk013_FolioReferencia.Text = "Folio referencia";
            this.chk013_FolioReferencia.UseVisualStyleBackColor = true;
            // 
            // FramePrograma
            // 
            this.FramePrograma.Controls.Add(this.chk017_Presentacion);
            this.FramePrograma.Controls.Add(this.chk010_SoloEtiqueta_SubPrograma);
            this.FramePrograma.Controls.Add(this.chk008_SoloEtiquetaPrograma);
            this.FramePrograma.Controls.Add(this.chk007_Programa);
            this.FramePrograma.Controls.Add(this.chk009_SubPrograma);
            this.FramePrograma.Location = new System.Drawing.Point(255, 19);
            this.FramePrograma.Name = "FramePrograma";
            this.FramePrograma.Size = new System.Drawing.Size(239, 176);
            this.FramePrograma.TabIndex = 1;
            this.FramePrograma.TabStop = false;
            this.FramePrograma.Text = "Programa y Sub-Programa, Presentación";
            // 
            // chk017_Presentacion
            // 
            this.chk017_Presentacion.Location = new System.Drawing.Point(32, 123);
            this.chk017_Presentacion.Name = "chk017_Presentacion";
            this.chk017_Presentacion.Size = new System.Drawing.Size(201, 18);
            this.chk017_Presentacion.TabIndex = 4;
            this.chk017_Presentacion.Text = "Presentación - Contenido paquete";
            this.chk017_Presentacion.UseVisualStyleBackColor = true;
            // 
            // chk010_SoloEtiqueta_SubPrograma
            // 
            this.chk010_SoloEtiqueta_SubPrograma.Location = new System.Drawing.Point(53, 90);
            this.chk010_SoloEtiqueta_SubPrograma.Name = "chk010_SoloEtiqueta_SubPrograma";
            this.chk010_SoloEtiqueta_SubPrograma.Size = new System.Drawing.Size(154, 18);
            this.chk010_SoloEtiqueta_SubPrograma.TabIndex = 3;
            this.chk010_SoloEtiqueta_SubPrograma.Text = "Solo etiqueta sub-programa";
            this.chk010_SoloEtiqueta_SubPrograma.UseVisualStyleBackColor = true;
            // 
            // chk008_SoloEtiquetaPrograma
            // 
            this.chk008_SoloEtiquetaPrograma.Location = new System.Drawing.Point(53, 36);
            this.chk008_SoloEtiquetaPrograma.Name = "chk008_SoloEtiquetaPrograma";
            this.chk008_SoloEtiquetaPrograma.Size = new System.Drawing.Size(154, 18);
            this.chk008_SoloEtiquetaPrograma.TabIndex = 1;
            this.chk008_SoloEtiquetaPrograma.Text = "Solo etiqueta programa";
            this.chk008_SoloEtiquetaPrograma.UseVisualStyleBackColor = true;
            // 
            // chk007_Programa
            // 
            this.chk007_Programa.Location = new System.Drawing.Point(32, 19);
            this.chk007_Programa.Name = "chk007_Programa";
            this.chk007_Programa.Size = new System.Drawing.Size(154, 18);
            this.chk007_Programa.TabIndex = 0;
            this.chk007_Programa.Text = "Programa";
            this.chk007_Programa.UseVisualStyleBackColor = true;
            // 
            // chk009_SubPrograma
            // 
            this.chk009_SubPrograma.Location = new System.Drawing.Point(32, 71);
            this.chk009_SubPrograma.Name = "chk009_SubPrograma";
            this.chk009_SubPrograma.Size = new System.Drawing.Size(154, 18);
            this.chk009_SubPrograma.TabIndex = 2;
            this.chk009_SubPrograma.Text = "Sub-Programa";
            this.chk009_SubPrograma.UseVisualStyleBackColor = true;
            // 
            // FrameCliente
            // 
            this.FrameCliente.Controls.Add(this.chk006_MostrarDescripcion_Perfil);
            this.FrameCliente.Controls.Add(this.chk004_SoloEtiqueta_SubCliente);
            this.FrameCliente.Controls.Add(this.chk002_SoloEtiqueta_Cliente);
            this.FrameCliente.Controls.Add(this.chk005_IntercambiarSubCliente_Cliente);
            this.FrameCliente.Controls.Add(this.chk001_Cliente);
            this.FrameCliente.Controls.Add(this.chk003_SubCliente);
            this.FrameCliente.Location = new System.Drawing.Point(13, 19);
            this.FrameCliente.Name = "FrameCliente";
            this.FrameCliente.Size = new System.Drawing.Size(239, 176);
            this.FrameCliente.TabIndex = 0;
            this.FrameCliente.TabStop = false;
            this.FrameCliente.Text = "Cliente, Sub-Cliente, Folio atención";
            // 
            // chk006_MostrarDescripcion_Perfil
            // 
            this.chk006_MostrarDescripcion_Perfil.Location = new System.Drawing.Point(13, 147);
            this.chk006_MostrarDescripcion_Perfil.Name = "chk006_MostrarDescripcion_Perfil";
            this.chk006_MostrarDescripcion_Perfil.Size = new System.Drawing.Size(218, 18);
            this.chk006_MostrarDescripcion_Perfil.TabIndex = 5;
            this.chk006_MostrarDescripcion_Perfil.Text = "Mostrar descripción de perfil de atención";
            this.chk006_MostrarDescripcion_Perfil.UseVisualStyleBackColor = true;
            // 
            // chk004_SoloEtiqueta_SubCliente
            // 
            this.chk004_SoloEtiqueta_SubCliente.Location = new System.Drawing.Point(34, 90);
            this.chk004_SoloEtiqueta_SubCliente.Name = "chk004_SoloEtiqueta_SubCliente";
            this.chk004_SoloEtiqueta_SubCliente.Size = new System.Drawing.Size(197, 18);
            this.chk004_SoloEtiqueta_SubCliente.TabIndex = 3;
            this.chk004_SoloEtiqueta_SubCliente.Text = "Solo etiqueta sub-cliente";
            this.chk004_SoloEtiqueta_SubCliente.UseVisualStyleBackColor = true;
            // 
            // chk002_SoloEtiqueta_Cliente
            // 
            this.chk002_SoloEtiqueta_Cliente.Location = new System.Drawing.Point(34, 36);
            this.chk002_SoloEtiqueta_Cliente.Name = "chk002_SoloEtiqueta_Cliente";
            this.chk002_SoloEtiqueta_Cliente.Size = new System.Drawing.Size(197, 18);
            this.chk002_SoloEtiqueta_Cliente.TabIndex = 1;
            this.chk002_SoloEtiqueta_Cliente.Text = "Solo etiqueta cliente";
            this.chk002_SoloEtiqueta_Cliente.UseVisualStyleBackColor = true;
            // 
            // chk005_IntercambiarSubCliente_Cliente
            // 
            this.chk005_IntercambiarSubCliente_Cliente.Location = new System.Drawing.Point(13, 123);
            this.chk005_IntercambiarSubCliente_Cliente.Name = "chk005_IntercambiarSubCliente_Cliente";
            this.chk005_IntercambiarSubCliente_Cliente.Size = new System.Drawing.Size(218, 18);
            this.chk005_IntercambiarSubCliente_Cliente.TabIndex = 4;
            this.chk005_IntercambiarSubCliente_Cliente.Text = "Intercambiar Sub-Cliente por Cliente";
            this.chk005_IntercambiarSubCliente_Cliente.UseVisualStyleBackColor = true;
            // 
            // chk001_Cliente
            // 
            this.chk001_Cliente.Location = new System.Drawing.Point(13, 19);
            this.chk001_Cliente.Name = "chk001_Cliente";
            this.chk001_Cliente.Size = new System.Drawing.Size(218, 18);
            this.chk001_Cliente.TabIndex = 0;
            this.chk001_Cliente.Text = "Cliente";
            this.chk001_Cliente.UseVisualStyleBackColor = true;
            // 
            // chk003_SubCliente
            // 
            this.chk003_SubCliente.Location = new System.Drawing.Point(13, 71);
            this.chk003_SubCliente.Name = "chk003_SubCliente";
            this.chk003_SubCliente.Size = new System.Drawing.Size(218, 18);
            this.chk003_SubCliente.TabIndex = 2;
            this.chk003_SubCliente.Text = "Sub-Cliente";
            this.chk003_SubCliente.UseVisualStyleBackColor = true;
            // 
            // FrmConfigImpresionVentaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 294);
            this.Controls.Add(this.FramePrincipal);
            this.Controls.Add(this.FrameUnidades);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigImpresionVentaDetalle";
            this.Text = "Configurar encabezados de impresión de ventas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigImpresionVentaDetalle_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameUnidades.ResumeLayout(false);
            this.FramePrincipal.ResumeLayout(false);
            this.FrameBeneficiario.ResumeLayout(false);
            this.FramePrograma.ResumeLayout(false);
            this.FrameCliente.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scComboBoxExt cboUnidades;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private System.Windows.Forms.CheckBox chk003_SubCliente;
        private System.Windows.Forms.CheckBox chk001_Cliente;
        private System.Windows.Forms.GroupBox FrameCliente;
        private System.Windows.Forms.CheckBox chk005_IntercambiarSubCliente_Cliente;
        private System.Windows.Forms.GroupBox FramePrograma;
        private System.Windows.Forms.CheckBox chk007_Programa;
        private System.Windows.Forms.CheckBox chk009_SubPrograma;
        private System.Windows.Forms.GroupBox FrameBeneficiario;
        private System.Windows.Forms.CheckBox chk015_FolioDocumento;
        private System.Windows.Forms.CheckBox chk011_Beneficiario;
        private System.Windows.Forms.CheckBox chk013_FolioReferencia;
        private System.Windows.Forms.CheckBox chk008_SoloEtiquetaPrograma;
        private System.Windows.Forms.CheckBox chk004_SoloEtiqueta_SubCliente;
        private System.Windows.Forms.CheckBox chk002_SoloEtiqueta_Cliente;
        private System.Windows.Forms.CheckBox chk010_SoloEtiqueta_SubPrograma;
        private System.Windows.Forms.CheckBox chk016_SoloEtiqueta_FolioDocumento;
        private System.Windows.Forms.CheckBox chk014_SoloEtiqueta_FolioReferencia;
        private System.Windows.Forms.CheckBox chk012_SoloEtiqueta_Beneficiario;
        private System.Windows.Forms.CheckBox chk006_MostrarDescripcion_Perfil;
        private System.Windows.Forms.CheckBox chk017_Presentacion;
    }
}