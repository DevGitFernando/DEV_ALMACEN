namespace Dll_SII_INadro.Informacion
{
    partial class FrmIntegrarCuadrosBasicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarCuadrosBasicos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.btnLeerArchivoDeTexto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarDocumento = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDocumento = new System.Windows.Forms.GroupBox();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lblProcesados = new SC_ControlsCS.scLabelExt();
            this.lstVwInformacion = new System.Windows.Forms.ListView();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.tmCargaBase = new System.Windows.Forms.Timer(this.components);
            this.nmDepurarPrioridad = new System.Windows.Forms.NumericUpDown();
            this.chkDepurarPrioridades = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDocumento.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDepurarPrioridad)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnAbrir,
            this.btnLeerArchivoDeTexto,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnValidarDatos,
            this.toolStripSeparator5,
            this.btnIntegrarDocumento,
            this.toolStripSeparator,
            this.btnSalir,
            this.toolStripSeparator6});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(822, 25);
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
            // btnLeerArchivoDeTexto
            // 
            this.btnLeerArchivoDeTexto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeerArchivoDeTexto.Image = ((System.Drawing.Image)(resources.GetObject("btnLeerArchivoDeTexto.Image")));
            this.btnLeerArchivoDeTexto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeerArchivoDeTexto.Name = "btnLeerArchivoDeTexto";
            this.btnLeerArchivoDeTexto.Size = new System.Drawing.Size(23, 22);
            this.btnLeerArchivoDeTexto.Text = "Leer archivo de texto";
            this.btnLeerArchivoDeTexto.Click += new System.EventHandler(this.btnLeerArchivoDeTexto_Click);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(23, 22);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarDocumento
            // 
            this.btnIntegrarDocumento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarDocumento.Image")));
            this.btnIntegrarDocumento.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarDocumento.Name = "btnIntegrarDocumento";
            this.btnIntegrarDocumento.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarDocumento.Text = "Procesar Cuadros Básicos";
            this.btnIntegrarDocumento.Click += new System.EventHandler(this.btnIntegrarDocumento_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDocumento
            // 
            this.FrameDocumento.Controls.Add(this.chkDepurarPrioridades);
            this.FrameDocumento.Controls.Add(this.nmDepurarPrioridad);
            this.FrameDocumento.Location = new System.Drawing.Point(10, 28);
            this.FrameDocumento.Name = "FrameDocumento";
            this.FrameDocumento.Size = new System.Drawing.Size(800, 51);
            this.FrameDocumento.TabIndex = 2;
            this.FrameDocumento.TabStop = false;
            this.FrameDocumento.Text = "Información de Cuadros Básicos";
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.lblProcesados);
            this.FrameResultado.Controls.Add(this.lstVwInformacion);
            this.FrameResultado.Location = new System.Drawing.Point(10, 81);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(800, 281);
            this.FrameResultado.TabIndex = 3;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Información";
            // 
            // lblProcesados
            // 
            this.lblProcesados.Location = new System.Drawing.Point(506, 6);
            this.lblProcesados.MostrarToolTip = false;
            this.lblProcesados.Name = "lblProcesados";
            this.lblProcesados.Size = new System.Drawing.Size(284, 17);
            this.lblProcesados.TabIndex = 1;
            this.lblProcesados.Text = "scLabelExt1";
            this.lblProcesados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstVwInformacion
            // 
            this.lstVwInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwInformacion.Location = new System.Drawing.Point(10, 24);
            this.lstVwInformacion.Name = "lstVwInformacion";
            this.lstVwInformacion.Size = new System.Drawing.Size(780, 246);
            this.lstVwInformacion.TabIndex = 0;
            this.lstVwInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(116, 368);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(602, 102);
            this.FrameProceso.TabIndex = 4;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(574, 64);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // tmValidacion
            // 
            this.tmValidacion.Tick += new System.EventHandler(this.tmValidacion_Tick);
            // 
            // tmCargaBase
            // 
            this.tmCargaBase.Tick += new System.EventHandler(this.tmCargaBase_Tick);
            // 
            // nmDepurarPrioridad
            // 
            this.nmDepurarPrioridad.Location = new System.Drawing.Point(168, 19);
            this.nmDepurarPrioridad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nmDepurarPrioridad.Name = "nmDepurarPrioridad";
            this.nmDepurarPrioridad.Size = new System.Drawing.Size(120, 20);
            this.nmDepurarPrioridad.TabIndex = 0;
            this.nmDepurarPrioridad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkDepurarPrioridades
            // 
            this.chkDepurarPrioridades.Location = new System.Drawing.Point(28, 19);
            this.chkDepurarPrioridades.Name = "chkDepurarPrioridades";
            this.chkDepurarPrioridades.Size = new System.Drawing.Size(139, 20);
            this.chkDepurarPrioridades.TabIndex = 1;
            this.chkDepurarPrioridades.Text = "Depurar prioridades >= ";
            this.chkDepurarPrioridades.UseVisualStyleBackColor = true;
            this.chkDepurarPrioridades.CheckedChanged += new System.EventHandler(this.chkDepurarPrioridades_CheckedChanged);
            // 
            // FrmIntegrarCuadrosBasicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 479);
            this.ControlBox = false;
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.FrameDocumento);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmIntegrarCuadrosBasicos";
            this.Text = "Integración de Cuadros Básicos para remisiones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIntegrarCuadrosBasicos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDocumento.ResumeLayout(false);
            this.FrameResultado.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmDepurarPrioridad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameDocumento;
        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.ListView lstVwInformacion;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private SC_ControlsCS.scLabelExt lblProcesados;
        private System.Windows.Forms.ToolStripButton btnIntegrarDocumento;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Timer tmValidacion;
        private System.Windows.Forms.Timer tmCargaBase;
        private System.Windows.Forms.ToolStripButton btnLeerArchivoDeTexto;
        private System.Windows.Forms.NumericUpDown nmDepurarPrioridad;
        private System.Windows.Forms.CheckBox chkDepurarPrioridades;
    }
}