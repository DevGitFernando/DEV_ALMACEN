namespace Dll_SII_INadro.InformacionDeSalida
{
    partial class FrmINF_Salidas__Unidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmINF_Salidas__Unidades));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarDocumentos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.FrameTipoDeDocto = new System.Windows.Forms.GroupBox();
            this.rdoTomaDeExistencias = new System.Windows.Forms.RadioButton();
            this.rdoExistencias = new System.Windows.Forms.RadioButton();
            this.rdoRemisiones = new System.Windows.Forms.RadioButton();
            this.rdoRecibos = new System.Windows.Forms.RadioButton();
            this.rdoSurtidos = new System.Windows.Forms.RadioButton();
            this.FrameFechaDeProceso = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoDeDocto.SuspendLayout();
            this.FrameFechaDeProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnProcesarDocumentos,
            this.toolStripSeparator1,
            this.btnAbrir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(504, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProcesarDocumentos
            // 
            this.btnProcesarDocumentos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesarDocumentos.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesarDocumentos.Image")));
            this.btnProcesarDocumentos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesarDocumentos.Name = "btnProcesarDocumentos";
            this.btnProcesarDocumentos.Size = new System.Drawing.Size(23, 22);
            this.btnProcesarDocumentos.Text = "Generar pedido masivo";
            this.btnProcesarDocumentos.Click += new System.EventHandler(this.btnProcesarDocumentos_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Enabled = false;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(23, 22);
            this.btnAbrir.Text = "&Abrir";
            this.btnAbrir.Visible = false;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // FrameTipoDeDocto
            // 
            this.FrameTipoDeDocto.Controls.Add(this.rdoTomaDeExistencias);
            this.FrameTipoDeDocto.Controls.Add(this.rdoExistencias);
            this.FrameTipoDeDocto.Controls.Add(this.rdoRemisiones);
            this.FrameTipoDeDocto.Controls.Add(this.rdoRecibos);
            this.FrameTipoDeDocto.Controls.Add(this.rdoSurtidos);
            this.FrameTipoDeDocto.Location = new System.Drawing.Point(12, 31);
            this.FrameTipoDeDocto.Name = "FrameTipoDeDocto";
            this.FrameTipoDeDocto.Size = new System.Drawing.Size(484, 97);
            this.FrameTipoDeDocto.TabIndex = 1;
            this.FrameTipoDeDocto.TabStop = false;
            this.FrameTipoDeDocto.Text = "Tipo de documento";
            // 
            // rdoTomaDeExistencias
            // 
            this.rdoTomaDeExistencias.Location = new System.Drawing.Point(19, 64);
            this.rdoTomaDeExistencias.Name = "rdoTomaDeExistencias";
            this.rdoTomaDeExistencias.Size = new System.Drawing.Size(249, 18);
            this.rdoTomaDeExistencias.TabIndex = 2;
            this.rdoTomaDeExistencias.Text = "Tomas de existencias (Ajustes de Inventario)";
            this.rdoTomaDeExistencias.UseVisualStyleBackColor = true;
            this.rdoTomaDeExistencias.CheckedChanged += new System.EventHandler(this.rdoTomaDeExistencias_CheckedChanged);
            // 
            // rdoExistencias
            // 
            this.rdoExistencias.Location = new System.Drawing.Point(284, 40);
            this.rdoExistencias.Name = "rdoExistencias";
            this.rdoExistencias.Size = new System.Drawing.Size(81, 18);
            this.rdoExistencias.TabIndex = 4;
            this.rdoExistencias.Text = "Existencias";
            this.rdoExistencias.UseVisualStyleBackColor = true;
            this.rdoExistencias.CheckedChanged += new System.EventHandler(this.rdoExistencias_CheckedChanged);
            // 
            // rdoRemisiones
            // 
            this.rdoRemisiones.Location = new System.Drawing.Point(284, 16);
            this.rdoRemisiones.Name = "rdoRemisiones";
            this.rdoRemisiones.Size = new System.Drawing.Size(185, 18);
            this.rdoRemisiones.TabIndex = 3;
            this.rdoRemisiones.Text = "Remisiones (Válidación firmada)";
            this.rdoRemisiones.UseVisualStyleBackColor = true;
            this.rdoRemisiones.CheckedChanged += new System.EventHandler(this.rdoRemisiones_CheckedChanged);
            // 
            // rdoRecibos
            // 
            this.rdoRecibos.Location = new System.Drawing.Point(19, 40);
            this.rdoRecibos.Name = "rdoRecibos";
            this.rdoRecibos.Size = new System.Drawing.Size(248, 18);
            this.rdoRecibos.TabIndex = 1;
            this.rdoRecibos.Text = "Recibos (Tranferencias de Entrada y Pedidos)";
            this.rdoRecibos.UseVisualStyleBackColor = true;
            this.rdoRecibos.CheckedChanged += new System.EventHandler(this.rdoRecibos_CheckedChanged);
            // 
            // rdoSurtidos
            // 
            this.rdoSurtidos.Location = new System.Drawing.Point(19, 16);
            this.rdoSurtidos.Name = "rdoSurtidos";
            this.rdoSurtidos.Size = new System.Drawing.Size(223, 18);
            this.rdoSurtidos.TabIndex = 0;
            this.rdoSurtidos.Text = "Surtidos (Dispensación y Transferencias)";
            this.rdoSurtidos.UseVisualStyleBackColor = true;
            this.rdoSurtidos.CheckedChanged += new System.EventHandler(this.rdoSurtidos_CheckedChanged);
            // 
            // FrameFechaDeProceso
            // 
            this.FrameFechaDeProceso.Controls.Add(this.label2);
            this.FrameFechaDeProceso.Controls.Add(this.label1);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaFinal);
            this.FrameFechaDeProceso.Controls.Add(this.dtpFechaInicial);
            this.FrameFechaDeProceso.Location = new System.Drawing.Point(12, 129);
            this.FrameFechaDeProceso.Name = "FrameFechaDeProceso";
            this.FrameFechaDeProceso.Size = new System.Drawing.Size(484, 46);
            this.FrameFechaDeProceso.TabIndex = 2;
            this.FrameFechaDeProceso.TabStop = false;
            this.FrameFechaDeProceso.Text = "Fechas a procesar";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(273, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(78, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(324, 16);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(129, 16);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // FrmINF_Salidas__Unidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 184);
            this.Controls.Add(this.FrameFechaDeProceso);
            this.Controls.Add(this.FrameTipoDeDocto);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmINF_Salidas__Unidades";
            this.Text = "Generar documentos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmINF_Salidas__Unidades_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoDeDocto.ResumeLayout(false);
            this.FrameFechaDeProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProcesarDocumentos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameTipoDeDocto;
        private System.Windows.Forms.GroupBox FrameFechaDeProceso;
        private System.Windows.Forms.RadioButton rdoRemisiones;
        private System.Windows.Forms.RadioButton rdoRecibos;
        private System.Windows.Forms.RadioButton rdoSurtidos;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.RadioButton rdoExistencias;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoTomaDeExistencias;
        private System.Windows.Forms.ToolStripButton btnAbrir;
    }
}