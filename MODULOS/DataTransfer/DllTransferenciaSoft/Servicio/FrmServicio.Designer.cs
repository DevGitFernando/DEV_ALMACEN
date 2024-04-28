namespace DllTransferenciaSoft.Servicio
{
    partial class FrmServicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmServicio));
            this.tmObtenerInformacion = new System.Windows.Forms.Timer(this.components);
            this.tmIntegrarInformacion = new System.Windows.Forms.Timer(this.components);
            this.imgListaOpcionesServer = new System.Windows.Forms.ImageList(this.components);
            this.imgListaServers = new System.Windows.Forms.ImageList(this.components);
            this.tmEjecutarProcesos = new System.Windows.Forms.Timer(this.components);
            this.tmUrgentes = new System.Windows.Forms.Timer(this.components);
            this.FrameProcesosManual = new System.Windows.Forms.GroupBox();
            this.btnRespaldarBD = new System.Windows.Forms.Button();
            this.btnIntegracion = new System.Windows.Forms.Button();
            this.btnObtencion = new System.Windows.Forms.Button();
            this.FrameEnEjecucion = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkInicioSO = new System.Windows.Forms.CheckBox();
            this.pcServer = new System.Windows.Forms.PictureBox();
            this.btnDetener = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.lblDetener = new System.Windows.Forms.Label();
            this.lblIniciar = new System.Windows.Forms.Label();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.FrameProcesos = new System.Windows.Forms.GroupBox();
            this.cboProcesos = new SC_ControlsCS.scComboBoxExt();
            this.FrameTiempoEjecucion = new System.Windows.Forms.GroupBox();
            this.lblRespaldo = new System.Windows.Forms.Label();
            this.lblIntegracion = new System.Windows.Forms.Label();
            this.lblObtencion = new System.Windows.Forms.Label();
            this.pcServerInactivo = new System.Windows.Forms.PictureBox();
            this.pcServerStop = new System.Windows.Forms.PictureBox();
            this.pcServerStar = new System.Windows.Forms.PictureBox();
            this.tmRespaldosBD = new System.Windows.Forms.Timer(this.components);
            this.tmIniciarServicios = new System.Windows.Forms.Timer(this.components);
            this.tmLogIntegracion = new System.Windows.Forms.Timer(this.components);
            this.menuObtencion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnObtencion__Habilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnObtencion__Deshabilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIntegracion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnIntegracion__Habilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIntegracion__Deshabilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRespaldos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnRespaldos__Habilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRespaldos__Deshabilitar = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMenu = new System.Windows.Forms.Label();
            this.tmEnvioFTP = new System.Windows.Forms.Timer(this.components);
            this.FrameProcesosManual.SuspendLayout();
            this.FrameEnEjecucion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcServer)).BeginInit();
            this.FramePrincipal.SuspendLayout();
            this.FrameProcesos.SuspendLayout();
            this.FrameTiempoEjecucion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcServerInactivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcServerStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcServerStar)).BeginInit();
            this.menuObtencion.SuspendLayout();
            this.menuIntegracion.SuspendLayout();
            this.menuRespaldos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmObtenerInformacion
            // 
            this.tmObtenerInformacion.Interval = 120000;
            this.tmObtenerInformacion.Tick += new System.EventHandler(this.tmObtenerInformacion_Tick);
            // 
            // tmIntegrarInformacion
            // 
            this.tmIntegrarInformacion.Interval = 180000;
            this.tmIntegrarInformacion.Tick += new System.EventHandler(this.tmIntegrarInformacion_Tick);
            // 
            // imgListaOpcionesServer
            // 
            this.imgListaOpcionesServer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListaOpcionesServer.ImageStream")));
            this.imgListaOpcionesServer.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListaOpcionesServer.Images.SetKeyName(0, "Sqlmangr 029.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(1, "Sqlmangr 026.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(2, "Sqlmangr 031.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(3, "Sqlmangr 028.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(4, "Sqlmangr 001.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(5, "Sqlmangr 002.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(6, "Sqlmangr 005.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(7, "Sqlns 018.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(8, "Sqlns 020.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(9, "Sqlns 019.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(10, "Sqlns 101.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(11, "Isqlw 001.ico");
            this.imgListaOpcionesServer.Images.SetKeyName(12, "Sqlvdir 003.ico");
            // 
            // imgListaServers
            // 
            this.imgListaServers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListaServers.ImageStream")));
            this.imgListaServers.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListaServers.Images.SetKeyName(0, "Star.bmp");
            this.imgListaServers.Images.SetKeyName(1, "Copia de Star.bmp");
            // 
            // tmEjecutarProcesos
            // 
            this.tmEjecutarProcesos.Enabled = true;
            this.tmEjecutarProcesos.Interval = 90000;
            // 
            // tmUrgentes
            // 
            this.tmUrgentes.Enabled = true;
            this.tmUrgentes.Interval = 90000;
            // 
            // FrameProcesosManual
            // 
            this.FrameProcesosManual.Controls.Add(this.btnRespaldarBD);
            this.FrameProcesosManual.Controls.Add(this.btnIntegracion);
            this.FrameProcesosManual.Controls.Add(this.btnObtencion);
            this.FrameProcesosManual.Location = new System.Drawing.Point(13, 187);
            this.FrameProcesosManual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProcesosManual.Name = "FrameProcesosManual";
            this.FrameProcesosManual.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProcesosManual.Size = new System.Drawing.Size(261, 134);
            this.FrameProcesosManual.TabIndex = 34;
            this.FrameProcesosManual.TabStop = false;
            this.FrameProcesosManual.Text = "Ejecutar proceso manualmente";
            // 
            // btnRespaldarBD
            // 
            this.btnRespaldarBD.Location = new System.Drawing.Point(12, 95);
            this.btnRespaldarBD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRespaldarBD.Name = "btnRespaldarBD";
            this.btnRespaldarBD.Size = new System.Drawing.Size(240, 28);
            this.btnRespaldarBD.TabIndex = 19;
            this.btnRespaldarBD.Text = "Generar respaldo";
            this.btnRespaldarBD.UseVisualStyleBackColor = true;
            this.btnRespaldarBD.Click += new System.EventHandler(this.btnRespaldarBD_Click);
            // 
            // btnIntegracion
            // 
            this.btnIntegracion.Location = new System.Drawing.Point(12, 59);
            this.btnIntegracion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIntegracion.Name = "btnIntegracion";
            this.btnIntegracion.Size = new System.Drawing.Size(240, 28);
            this.btnIntegracion.TabIndex = 18;
            this.btnIntegracion.Text = "Integración de información";
            this.btnIntegracion.UseVisualStyleBackColor = true;
            this.btnIntegracion.Click += new System.EventHandler(this.btnIntegracion_Click);
            // 
            // btnObtencion
            // 
            this.btnObtencion.Location = new System.Drawing.Point(12, 23);
            this.btnObtencion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnObtencion.Name = "btnObtencion";
            this.btnObtencion.Size = new System.Drawing.Size(240, 28);
            this.btnObtencion.TabIndex = 16;
            this.btnObtencion.Text = "Obtención de información";
            this.btnObtencion.UseVisualStyleBackColor = true;
            this.btnObtencion.Click += new System.EventHandler(this.btnObtencion_Click);
            // 
            // FrameEnEjecucion
            // 
            this.FrameEnEjecucion.Controls.Add(this.progressBar);
            this.FrameEnEjecucion.Location = new System.Drawing.Point(319, 187);
            this.FrameEnEjecucion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameEnEjecucion.Name = "FrameEnEjecucion";
            this.FrameEnEjecucion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameEnEjecucion.Size = new System.Drawing.Size(244, 134);
            this.FrameEnEjecucion.TabIndex = 41;
            this.FrameEnEjecucion.TabStop = false;
            this.FrameEnEjecucion.Text = "Ejecutando : ";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(20, 42);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(203, 49);
            this.progressBar.Step = 50;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 0;
            // 
            // chkInicioSO
            // 
            this.chkInicioSO.Location = new System.Drawing.Point(24, 442);
            this.chkInicioSO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkInicioSO.Name = "chkInicioSO";
            this.chkInicioSO.Size = new System.Drawing.Size(241, 21);
            this.chkInicioSO.TabIndex = 33;
            this.chkInicioSO.Text = "Iniciar con sistema operativo";
            this.chkInicioSO.UseVisualStyleBackColor = true;
            this.chkInicioSO.CheckedChanged += new System.EventHandler(this.chkInicioSO_CheckedChanged);
            // 
            // pcServer
            // 
            this.pcServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcServer.Image = ((System.Drawing.Image)(resources.GetObject("pcServer.Image")));
            this.pcServer.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcServer.InitialImage")));
            this.pcServer.Location = new System.Drawing.Point(37, 55);
            this.pcServer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcServer.Name = "pcServer";
            this.pcServer.Size = new System.Drawing.Size(82, 99);
            this.pcServer.TabIndex = 32;
            this.pcServer.TabStop = false;
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(129, 113);
            this.btnDetener.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(32, 30);
            this.btnDetener.TabIndex = 29;
            this.btnDetener.UseVisualStyleBackColor = true;
            this.btnDetener.Click += new System.EventHandler(this.btnDetener_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(129, 76);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(32, 30);
            this.btnIniciar.TabIndex = 27;
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // lblDetener
            // 
            this.lblDetener.AutoSize = true;
            this.lblDetener.Location = new System.Drawing.Point(164, 121);
            this.lblDetener.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetener.Name = "lblDetener";
            this.lblDetener.Size = new System.Drawing.Size(55, 16);
            this.lblDetener.TabIndex = 31;
            this.lblDetener.Text = "Detener";
            // 
            // lblIniciar
            // 
            this.lblIniciar.AutoSize = true;
            this.lblIniciar.Location = new System.Drawing.Point(164, 84);
            this.lblIniciar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIniciar.Name = "lblIniciar";
            this.lblIniciar.Size = new System.Drawing.Size(42, 16);
            this.lblIniciar.TabIndex = 30;
            this.lblIniciar.Text = "Iniciar";
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.FrameEnEjecucion);
            this.FramePrincipal.Controls.Add(this.FrameProcesos);
            this.FramePrincipal.Controls.Add(this.FrameTiempoEjecucion);
            this.FramePrincipal.Controls.Add(this.pcServerInactivo);
            this.FramePrincipal.Controls.Add(this.pcServerStop);
            this.FramePrincipal.Controls.Add(this.pcServerStar);
            this.FramePrincipal.Controls.Add(this.chkInicioSO);
            this.FramePrincipal.Controls.Add(this.FrameProcesosManual);
            this.FramePrincipal.Location = new System.Drawing.Point(16, 0);
            this.FramePrincipal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FramePrincipal.Size = new System.Drawing.Size(597, 474);
            this.FramePrincipal.TabIndex = 35;
            this.FramePrincipal.TabStop = false;
            // 
            // FrameProcesos
            // 
            this.FrameProcesos.Controls.Add(this.cboProcesos);
            this.FrameProcesos.Controls.Add(this.pcServer);
            this.FrameProcesos.Controls.Add(this.btnDetener);
            this.FrameProcesos.Controls.Add(this.btnIniciar);
            this.FrameProcesos.Controls.Add(this.lblDetener);
            this.FrameProcesos.Controls.Add(this.lblIniciar);
            this.FrameProcesos.Location = new System.Drawing.Point(13, 15);
            this.FrameProcesos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProcesos.Name = "FrameProcesos";
            this.FrameProcesos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProcesos.Size = new System.Drawing.Size(261, 167);
            this.FrameProcesos.TabIndex = 40;
            this.FrameProcesos.TabStop = false;
            this.FrameProcesos.Text = "Procesos";
            // 
            // cboProcesos
            // 
            this.cboProcesos.BackColorEnabled = System.Drawing.Color.White;
            this.cboProcesos.Data = "";
            this.cboProcesos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcesos.Filtro = " 1 = 1";
            this.cboProcesos.FormattingEnabled = true;
            this.cboProcesos.ListaItemsBusqueda = 20;
            this.cboProcesos.Location = new System.Drawing.Point(12, 22);
            this.cboProcesos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboProcesos.MostrarToolTip = false;
            this.cboProcesos.Name = "cboProcesos";
            this.cboProcesos.Size = new System.Drawing.Size(239, 24);
            this.cboProcesos.TabIndex = 35;
            this.cboProcesos.SelectedIndexChanged += new System.EventHandler(this.cboProcesos_SelectedIndexChanged);
            // 
            // FrameTiempoEjecucion
            // 
            this.FrameTiempoEjecucion.Controls.Add(this.lblRespaldo);
            this.FrameTiempoEjecucion.Controls.Add(this.lblIntegracion);
            this.FrameTiempoEjecucion.Controls.Add(this.lblObtencion);
            this.FrameTiempoEjecucion.Location = new System.Drawing.Point(13, 324);
            this.FrameTiempoEjecucion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTiempoEjecucion.Name = "FrameTiempoEjecucion";
            this.FrameTiempoEjecucion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTiempoEjecucion.Size = new System.Drawing.Size(261, 111);
            this.FrameTiempoEjecucion.TabIndex = 39;
            this.FrameTiempoEjecucion.TabStop = false;
            this.FrameTiempoEjecucion.Text = "Inicio de procesos";
            // 
            // lblRespaldo
            // 
            this.lblRespaldo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRespaldo.Location = new System.Drawing.Point(11, 76);
            this.lblRespaldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRespaldo.Name = "lblRespaldo";
            this.lblRespaldo.Size = new System.Drawing.Size(241, 23);
            this.lblRespaldo.TabIndex = 2;
            this.lblRespaldo.Text = "Respaldo :";
            this.lblRespaldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntegracion
            // 
            this.lblIntegracion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIntegracion.Location = new System.Drawing.Point(11, 49);
            this.lblIntegracion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIntegracion.Name = "lblIntegracion";
            this.lblIntegracion.Size = new System.Drawing.Size(241, 23);
            this.lblIntegracion.TabIndex = 1;
            this.lblIntegracion.Text = "Integración :";
            this.lblIntegracion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblObtencion
            // 
            this.lblObtencion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblObtencion.Location = new System.Drawing.Point(11, 22);
            this.lblObtencion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObtencion.Name = "lblObtencion";
            this.lblObtencion.Size = new System.Drawing.Size(241, 23);
            this.lblObtencion.TabIndex = 0;
            this.lblObtencion.Text = "Obtención :";
            this.lblObtencion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pcServerInactivo
            // 
            this.pcServerInactivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcServerInactivo.Image = ((System.Drawing.Image)(resources.GetObject("pcServerInactivo.Image")));
            this.pcServerInactivo.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcServerInactivo.InitialImage")));
            this.pcServerInactivo.Location = new System.Drawing.Point(319, 58);
            this.pcServerInactivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcServerInactivo.Name = "pcServerInactivo";
            this.pcServerInactivo.Size = new System.Drawing.Size(82, 99);
            this.pcServerInactivo.TabIndex = 38;
            this.pcServerInactivo.TabStop = false;
            this.pcServerInactivo.Visible = false;
            // 
            // pcServerStop
            // 
            this.pcServerStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcServerStop.Image = ((System.Drawing.Image)(resources.GetObject("pcServerStop.Image")));
            this.pcServerStop.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcServerStop.InitialImage")));
            this.pcServerStop.Location = new System.Drawing.Point(500, 58);
            this.pcServerStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcServerStop.Name = "pcServerStop";
            this.pcServerStop.Size = new System.Drawing.Size(82, 99);
            this.pcServerStop.TabIndex = 37;
            this.pcServerStop.TabStop = false;
            this.pcServerStop.Visible = false;
            // 
            // pcServerStar
            // 
            this.pcServerStar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcServerStar.Image = ((System.Drawing.Image)(resources.GetObject("pcServerStar.Image")));
            this.pcServerStar.InitialImage = ((System.Drawing.Image)(resources.GetObject("pcServerStar.InitialImage")));
            this.pcServerStar.Location = new System.Drawing.Point(409, 58);
            this.pcServerStar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pcServerStar.Name = "pcServerStar";
            this.pcServerStar.Size = new System.Drawing.Size(82, 99);
            this.pcServerStar.TabIndex = 36;
            this.pcServerStar.TabStop = false;
            this.pcServerStar.Visible = false;
            // 
            // tmRespaldosBD
            // 
            this.tmRespaldosBD.Interval = 60000;
            this.tmRespaldosBD.Tick += new System.EventHandler(this.tmRespaldosBD_Tick);
            // 
            // tmIniciarServicios
            // 
            this.tmIniciarServicios.Tick += new System.EventHandler(this.tmIniciarServicios_Tick);
            // 
            // tmLogIntegracion
            // 
            this.tmLogIntegracion.Tick += new System.EventHandler(this.tmLogIntegracion_Tick);
            // 
            // menuObtencion
            // 
            this.menuObtencion.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuObtencion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnObtencion__Habilitar,
            this.btnObtencion__Deshabilitar});
            this.menuObtencion.Name = "menuServicios";
            this.menuObtencion.ShowImageMargin = false;
            this.menuObtencion.Size = new System.Drawing.Size(134, 52);
            // 
            // btnObtencion__Habilitar
            // 
            this.btnObtencion__Habilitar.Name = "btnObtencion__Habilitar";
            this.btnObtencion__Habilitar.Size = new System.Drawing.Size(133, 24);
            this.btnObtencion__Habilitar.Text = "Habilitar";
            this.btnObtencion__Habilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // btnObtencion__Deshabilitar
            // 
            this.btnObtencion__Deshabilitar.Name = "btnObtencion__Deshabilitar";
            this.btnObtencion__Deshabilitar.Size = new System.Drawing.Size(133, 24);
            this.btnObtencion__Deshabilitar.Text = "Deshabilitar";
            this.btnObtencion__Deshabilitar.Click += new System.EventHandler(this.btnDeshabilitar_Click);
            // 
            // menuIntegracion
            // 
            this.menuIntegracion.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuIntegracion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnIntegracion__Habilitar,
            this.btnIntegracion__Deshabilitar});
            this.menuIntegracion.Name = "menuServicios";
            this.menuIntegracion.ShowImageMargin = false;
            this.menuIntegracion.Size = new System.Drawing.Size(134, 52);
            // 
            // btnIntegracion__Habilitar
            // 
            this.btnIntegracion__Habilitar.Name = "btnIntegracion__Habilitar";
            this.btnIntegracion__Habilitar.Size = new System.Drawing.Size(133, 24);
            this.btnIntegracion__Habilitar.Text = "Habilitar";
            this.btnIntegracion__Habilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // btnIntegracion__Deshabilitar
            // 
            this.btnIntegracion__Deshabilitar.Name = "btnIntegracion__Deshabilitar";
            this.btnIntegracion__Deshabilitar.Size = new System.Drawing.Size(133, 24);
            this.btnIntegracion__Deshabilitar.Text = "Deshabilitar";
            this.btnIntegracion__Deshabilitar.Click += new System.EventHandler(this.btnDeshabilitar_Click);
            // 
            // menuRespaldos
            // 
            this.menuRespaldos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuRespaldos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRespaldos__Habilitar,
            this.btnRespaldos__Deshabilitar});
            this.menuRespaldos.Name = "menuServicios";
            this.menuRespaldos.ShowImageMargin = false;
            this.menuRespaldos.Size = new System.Drawing.Size(134, 52);
            // 
            // btnRespaldos__Habilitar
            // 
            this.btnRespaldos__Habilitar.Name = "btnRespaldos__Habilitar";
            this.btnRespaldos__Habilitar.Size = new System.Drawing.Size(133, 24);
            this.btnRespaldos__Habilitar.Text = "Habilitar";
            this.btnRespaldos__Habilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // btnRespaldos__Deshabilitar
            // 
            this.btnRespaldos__Deshabilitar.Name = "btnRespaldos__Deshabilitar";
            this.btnRespaldos__Deshabilitar.Size = new System.Drawing.Size(133, 24);
            this.btnRespaldos__Deshabilitar.Text = "Deshabilitar";
            this.btnRespaldos__Deshabilitar.Click += new System.EventHandler(this.btnDeshabilitar_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.Location = new System.Drawing.Point(0, 473);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(636, 28);
            this.lblMenu.TabIndex = 36;
            this.lblMenu.Text = "Clic derecho en botones para ver menú";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmEnvioFTP
            // 
            this.tmEnvioFTP.Interval = 1000;
            this.tmEnvioFTP.Tick += new System.EventHandler(this.tmEnvioFTP_Tick);
            // 
            // FrmServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 501);
            this.ControlBox = false;
            this.Controls.Add(this.lblMenu);
            this.Controls.Add(this.FramePrincipal);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmServicio";
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
            this.Text = "Servicio";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServicio_FormClosing);
            this.Load += new System.EventHandler(this.FrmServicio_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmServicio_KeyDown);
            this.FrameProcesosManual.ResumeLayout(false);
            this.FrameEnEjecucion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcServer)).EndInit();
            this.FramePrincipal.ResumeLayout(false);
            this.FrameProcesos.ResumeLayout(false);
            this.FrameProcesos.PerformLayout();
            this.FrameTiempoEjecucion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcServerInactivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcServerStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcServerStar)).EndInit();
            this.menuObtencion.ResumeLayout(false);
            this.menuIntegracion.ResumeLayout(false);
            this.menuRespaldos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Timer tmObtenerInformacion;
        internal System.Windows.Forms.Timer tmIntegrarInformacion;
        internal System.Windows.Forms.ImageList imgListaOpcionesServer;
        internal System.Windows.Forms.ImageList imgListaServers;
        internal System.Windows.Forms.Timer tmEjecutarProcesos;
        internal System.Windows.Forms.Timer tmUrgentes;
        internal System.Windows.Forms.GroupBox FrameProcesosManual;
        internal System.Windows.Forms.Button btnIntegracion;
        internal System.Windows.Forms.Button btnObtencion;
        internal System.Windows.Forms.CheckBox chkInicioSO;
        internal System.Windows.Forms.PictureBox pcServer;
        internal System.Windows.Forms.Button btnDetener;
        internal System.Windows.Forms.Button btnIniciar;
        internal System.Windows.Forms.Label lblDetener;
        internal System.Windows.Forms.Label lblIniciar;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private SC_ControlsCS.scComboBoxExt cboProcesos;
        internal System.Windows.Forms.PictureBox pcServerInactivo;
        internal System.Windows.Forms.PictureBox pcServerStop;
        internal System.Windows.Forms.PictureBox pcServerStar;
        private System.Windows.Forms.GroupBox FrameTiempoEjecucion;
        private System.Windows.Forms.Label lblObtencion;
        private System.Windows.Forms.Label lblIntegracion;
        private System.Windows.Forms.GroupBox FrameProcesos;
        private System.Windows.Forms.Timer tmRespaldosBD;
        internal System.Windows.Forms.Button btnRespaldarBD;
        private System.Windows.Forms.Label lblRespaldo;
        private System.Windows.Forms.GroupBox FrameEnEjecucion;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer tmIniciarServicios;
        private System.Windows.Forms.Timer tmLogIntegracion;
        private System.Windows.Forms.ContextMenuStrip menuObtencion;
        private System.Windows.Forms.ToolStripMenuItem btnObtencion__Habilitar;
        private System.Windows.Forms.ToolStripMenuItem btnObtencion__Deshabilitar;
        private System.Windows.Forms.ContextMenuStrip menuIntegracion;
        private System.Windows.Forms.ToolStripMenuItem btnIntegracion__Habilitar;
        private System.Windows.Forms.ToolStripMenuItem btnIntegracion__Deshabilitar;
        private System.Windows.Forms.ContextMenuStrip menuRespaldos;
        private System.Windows.Forms.ToolStripMenuItem btnRespaldos__Habilitar;
        private System.Windows.Forms.ToolStripMenuItem btnRespaldos__Deshabilitar;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Timer tmEnvioFTP;
    }
}