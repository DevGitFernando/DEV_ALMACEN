namespace DllTransferenciaSoft.Configuraciones
{
    partial class FrmConfigurarConexionHuellas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarConexionHuellas));
            this.FrameDatosConexion = new System.Windows.Forms.GroupBox();
            this.txtPagina = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWebService = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.FrameDatosConexion.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameDatosConexion
            // 
            this.FrameDatosConexion.Controls.Add(this.txtPagina);
            this.FrameDatosConexion.Controls.Add(this.label4);
            this.FrameDatosConexion.Controls.Add(this.txtWebService);
            this.FrameDatosConexion.Controls.Add(this.label3);
            this.FrameDatosConexion.Controls.Add(this.txtServidor);
            this.FrameDatosConexion.Controls.Add(this.label2);
            this.FrameDatosConexion.Location = new System.Drawing.Point(13, 33);
            this.FrameDatosConexion.Name = "FrameDatosConexion";
            this.FrameDatosConexion.Size = new System.Drawing.Size(454, 113);
            this.FrameDatosConexion.TabIndex = 6;
            this.FrameDatosConexion.TabStop = false;
            this.FrameDatosConexion.Text = "Datos de conexión";
            // 
            // txtPagina
            // 
            this.txtPagina.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPagina.Decimales = 2;
            this.txtPagina.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtPagina.ForeColor = System.Drawing.Color.Black;
            this.txtPagina.Location = new System.Drawing.Point(84, 77);
            this.txtPagina.MaxLength = 50;
            this.txtPagina.Name = "txtPagina";
            this.txtPagina.PermitirApostrofo = false;
            this.txtPagina.PermitirNegativos = false;
            this.txtPagina.Size = new System.Drawing.Size(361, 20);
            this.txtPagina.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Pagina web :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWebService
            // 
            this.txtWebService.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtWebService.Decimales = 2;
            this.txtWebService.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtWebService.ForeColor = System.Drawing.Color.Black;
            this.txtWebService.Location = new System.Drawing.Point(84, 51);
            this.txtWebService.MaxLength = 50;
            this.txtWebService.Name = "txtWebService";
            this.txtWebService.PermitirApostrofo = false;
            this.txtWebService.PermitirNegativos = false;
            this.txtWebService.Size = new System.Drawing.Size(361, 20);
            this.txtWebService.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Web service :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServidor
            // 
            this.txtServidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtServidor.Decimales = 2;
            this.txtServidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtServidor.ForeColor = System.Drawing.Color.Black;
            this.txtServidor.Location = new System.Drawing.Point(84, 25);
            this.txtServidor.MaxLength = 50;
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.PermitirApostrofo = false;
            this.txtServidor.PermitirNegativos = false;
            this.txtServidor.Size = new System.Drawing.Size(361, 20);
            this.txtServidor.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Servidor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(480, 25);
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
            // FrmConfigurarConexionHuellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 159);
            this.Controls.Add(this.FrameDatosConexion);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmConfigurarConexionHuellas";
            this.Text = "Configurar Conexion Huellas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConfigurarConexionHuellas_Load);
            this.FrameDatosConexion.ResumeLayout(false);
            this.FrameDatosConexion.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDatosConexion;
        private SC_ControlsCS.scTextBoxExt txtPagina;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtWebService;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtServidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
    }
}