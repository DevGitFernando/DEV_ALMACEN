namespace DllTransferenciaSoft.IntegrarBD
{
    partial class FrmIntegrarBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarBD));
            this.FrameBasesDeDatos = new System.Windows.Forms.GroupBox();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.lstwBasesDeDatos = new System.Windows.Forms.ListView();
            this.Registro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BaseDeDatos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tamaño = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Porcentaje = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusProceso = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnFTP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDirectorioFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameTablas = new System.Windows.Forms.GroupBox();
            this.lstwTablasMigrar = new System.Windows.Forms.ListView();
            this.Tabla = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Registros = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RegMigrar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Procesada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarBD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFTP = new System.Windows.Forms.ToolStripButton();
            this.cboItemsFTP_Copiado = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDepurarFTP = new System.Windows.Forms.ToolStripButton();
            this.cboItemsFTP = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirFTP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogErrores = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cboTiposMigracion = new System.Windows.Forms.ToolStripComboBox();
            this.cboMesesMigracion = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogIntegracion = new System.Windows.Forms.ToolStripButton();
            this.tmIntegrarBD = new System.Windows.Forms.Timer(this.components);
            this.chkInicioSO = new System.Windows.Forms.CheckBox();
            this.tmRevisionFTP = new System.Windows.Forms.Timer(this.components);
            this.btnMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnIntegraciónDePaquetes = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDirectorios = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDir_BD__Integradas = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDir_BD__Repositorio = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDir_BD__Descompresion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDir_BD__Errores = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDir_BD__Log = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDir_BD__Proceso = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameBasesDeDatos.SuspendLayout();
            this.mnFTP.SuspendLayout();
            this.FrameTablas.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameBasesDeDatos
            // 
            this.FrameBasesDeDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameBasesDeDatos.Controls.Add(this.chkLog);
            this.FrameBasesDeDatos.Controls.Add(this.lstwBasesDeDatos);
            this.FrameBasesDeDatos.Location = new System.Drawing.Point(16, 34);
            this.FrameBasesDeDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameBasesDeDatos.Name = "FrameBasesDeDatos";
            this.FrameBasesDeDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameBasesDeDatos.Size = new System.Drawing.Size(1147, 217);
            this.FrameBasesDeDatos.TabIndex = 0;
            this.FrameBasesDeDatos.TabStop = false;
            this.FrameBasesDeDatos.Text = "Listado de Bases de Datos";
            // 
            // chkLog
            // 
            this.chkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLog.Location = new System.Drawing.Point(981, -1);
            this.chkLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(147, 21);
            this.chkLog.TabIndex = 1;
            this.chkLog.Text = "Habilitar registro";
            this.chkLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // lstwBasesDeDatos
            // 
            this.lstwBasesDeDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstwBasesDeDatos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Registro,
            this.BaseDeDatos,
            this.Tamaño,
            this.Fecha,
            this.Porcentaje,
            this.StatusProceso});
            this.lstwBasesDeDatos.FullRowSelect = true;
            this.lstwBasesDeDatos.GridLines = true;
            this.lstwBasesDeDatos.Location = new System.Drawing.Point(13, 20);
            this.lstwBasesDeDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstwBasesDeDatos.MultiSelect = false;
            this.lstwBasesDeDatos.Name = "lstwBasesDeDatos";
            this.lstwBasesDeDatos.ShowItemToolTips = true;
            this.lstwBasesDeDatos.Size = new System.Drawing.Size(1116, 189);
            this.lstwBasesDeDatos.TabIndex = 0;
            this.lstwBasesDeDatos.UseCompatibleStateImageBehavior = false;
            this.lstwBasesDeDatos.View = System.Windows.Forms.View.Details;
            // 
            // Registro
            // 
            this.Registro.Text = "Núm.";
            // 
            // BaseDeDatos
            // 
            this.BaseDeDatos.Text = "Base de Datos";
            this.BaseDeDatos.Width = 339;
            // 
            // Tamaño
            // 
            this.Tamaño.Text = "Tamaño MB";
            this.Tamaño.Width = 80;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha ";
            this.Fecha.Width = 140;
            // 
            // Porcentaje
            // 
            this.Porcentaje.Text = "Porcentaje";
            this.Porcentaje.Width = 70;
            // 
            // StatusProceso
            // 
            this.StatusProceso.Text = "Status";
            this.StatusProceso.Width = 129;
            // 
            // mnFTP
            // 
            this.mnFTP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDirectorioFTP});
            this.mnFTP.Name = "mnFTP";
            this.mnFTP.Size = new System.Drawing.Size(70, 26);
            // 
            // btnDirectorioFTP
            // 
            this.btnDirectorioFTP.Name = "btnDirectorioFTP";
            this.btnDirectorioFTP.Size = new System.Drawing.Size(69, 22);
            // 
            // FrameTablas
            // 
            this.FrameTablas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTablas.Controls.Add(this.lstwTablasMigrar);
            this.FrameTablas.Location = new System.Drawing.Point(16, 255);
            this.FrameTablas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTablas.Name = "FrameTablas";
            this.FrameTablas.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTablas.Size = new System.Drawing.Size(1147, 313);
            this.FrameTablas.TabIndex = 1;
            this.FrameTablas.TabStop = false;
            this.FrameTablas.Text = "Listado de tablas";
            // 
            // lstwTablasMigrar
            // 
            this.lstwTablasMigrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstwTablasMigrar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tabla,
            this.Registros,
            this.RegMigrar,
            this.Procesada});
            this.lstwTablasMigrar.FullRowSelect = true;
            this.lstwTablasMigrar.GridLines = true;
            this.lstwTablasMigrar.Location = new System.Drawing.Point(13, 20);
            this.lstwTablasMigrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstwTablasMigrar.MultiSelect = false;
            this.lstwTablasMigrar.Name = "lstwTablasMigrar";
            this.lstwTablasMigrar.ShowItemToolTips = true;
            this.lstwTablasMigrar.Size = new System.Drawing.Size(1116, 280);
            this.lstwTablasMigrar.TabIndex = 1;
            this.lstwTablasMigrar.UseCompatibleStateImageBehavior = false;
            this.lstwTablasMigrar.View = System.Windows.Forms.View.Details;
            // 
            // Tabla
            // 
            this.Tabla.Text = "Tabla";
            this.Tabla.Width = 460;
            // 
            // Registros
            // 
            this.Registros.Text = "Registros";
            this.Registros.Width = 80;
            // 
            // RegMigrar
            // 
            this.RegMigrar.Text = "Migrar";
            this.RegMigrar.Width = 80;
            // 
            // Procesada
            // 
            this.Procesada.Text = "Status";
            this.Procesada.Width = 140;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnIntegrarBD,
            this.toolStripSeparator1,
            this.btnFTP,
            this.cboItemsFTP_Copiado,
            this.toolStripSeparator5,
            this.btnDepurarFTP,
            this.cboItemsFTP,
            this.toolStripSeparator4,
            this.btnAbrirFTP,
            this.toolStripSeparator2,
            this.btnLogErrores,
            this.toolStripSeparator3,
            this.cboTiposMigracion,
            this.cboMesesMigracion,
            this.toolStripSeparator6,
            this.btnLogIntegracion,
            this.toolStripSeparator7,
            this.btnMenu});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1176, 28);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 25);
            this.btnNuevo.Text = "Inicializar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 28);
            // 
            // btnIntegrarBD
            // 
            this.btnIntegrarBD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarBD.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarBD.Image")));
            this.btnIntegrarBD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarBD.Name = "btnIntegrarBD";
            this.btnIntegrarBD.Size = new System.Drawing.Size(23, 25);
            this.btnIntegrarBD.Text = "Integrar Bases de Datos";
            this.btnIntegrarBD.ToolTipText = "Integrar Bases de Datos";
            this.btnIntegrarBD.Click += new System.EventHandler(this.btnIntegrarBD_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // btnFTP
            // 
            this.btnFTP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFTP.Image = ((System.Drawing.Image)(resources.GetObject("btnFTP.Image")));
            this.btnFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFTP.Name = "btnFTP";
            this.btnFTP.Size = new System.Drawing.Size(23, 25);
            this.btnFTP.Text = "Procesar sitio FTP";
            this.btnFTP.Click += new System.EventHandler(this.btnFTP_Click);
            // 
            // cboItemsFTP_Copiado
            // 
            this.cboItemsFTP_Copiado.AutoSize = false;
            this.cboItemsFTP_Copiado.DropDownHeight = 100;
            this.cboItemsFTP_Copiado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemsFTP_Copiado.DropDownWidth = 50;
            this.cboItemsFTP_Copiado.IntegralHeight = false;
            this.cboItemsFTP_Copiado.Items.AddRange(new object[] {
            "1 ",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9 ",
            "10 ",
            "15 ",
            "20 ",
            "25",
            "30"});
            this.cboItemsFTP_Copiado.Name = "cboItemsFTP_Copiado";
            this.cboItemsFTP_Copiado.Size = new System.Drawing.Size(65, 28);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // btnDepurarFTP
            // 
            this.btnDepurarFTP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDepurarFTP.Image = ((System.Drawing.Image)(resources.GetObject("btnDepurarFTP.Image")));
            this.btnDepurarFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDepurarFTP.Name = "btnDepurarFTP";
            this.btnDepurarFTP.Size = new System.Drawing.Size(23, 25);
            this.btnDepurarFTP.Text = "Depurar sitio FTP";
            this.btnDepurarFTP.Click += new System.EventHandler(this.btnDepurarFTP_Click);
            // 
            // cboItemsFTP
            // 
            this.cboItemsFTP.AutoSize = false;
            this.cboItemsFTP.DropDownHeight = 100;
            this.cboItemsFTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItemsFTP.DropDownWidth = 50;
            this.cboItemsFTP.IntegralHeight = false;
            this.cboItemsFTP.Items.AddRange(new object[] {
            "1 ",
            "5 ",
            "10 ",
            "15 ",
            "20 "});
            this.cboItemsFTP.Name = "cboItemsFTP";
            this.cboItemsFTP.Size = new System.Drawing.Size(65, 28);
            this.cboItemsFTP.ToolTipText = "Dias de creación de archivos para no ser borrados";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // btnAbrirFTP
            // 
            this.btnAbrirFTP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirFTP.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirFTP.Image")));
            this.btnAbrirFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirFTP.Name = "btnAbrirFTP";
            this.btnAbrirFTP.Size = new System.Drawing.Size(23, 25);
            this.btnAbrirFTP.Text = "Ir a directorio FTP";
            this.btnAbrirFTP.Click += new System.EventHandler(this.btnAbrirFTP_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // btnLogErrores
            // 
            this.btnLogErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLogErrores.Image = ((System.Drawing.Image)(resources.GetObject("btnLogErrores.Image")));
            this.btnLogErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogErrores.Name = "btnLogErrores";
            this.btnLogErrores.Size = new System.Drawing.Size(23, 25);
            this.btnLogErrores.Text = "Ver registro de Errores";
            this.btnLogErrores.ToolTipText = "Ver registro de Errores";
            this.btnLogErrores.Click += new System.EventHandler(this.btnLogErrores_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // cboTiposMigracion
            // 
            this.cboTiposMigracion.AutoSize = false;
            this.cboTiposMigracion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposMigracion.Items.AddRange(new object[] {
            "Diferencias ",
            "Todo"});
            this.cboTiposMigracion.Name = "cboTiposMigracion";
            this.cboTiposMigracion.Size = new System.Drawing.Size(160, 28);
            this.cboTiposMigracion.ToolTipText = "Tipo de integración";
            // 
            // cboMesesMigracion
            // 
            this.cboMesesMigracion.AutoSize = false;
            this.cboMesesMigracion.DropDownHeight = 100;
            this.cboMesesMigracion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMesesMigracion.DropDownWidth = 50;
            this.cboMesesMigracion.IntegralHeight = false;
            this.cboMesesMigracion.Items.AddRange(new object[] {
            "1 ",
            "2",
            "3 ",
            "4 ",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12 "});
            this.cboMesesMigracion.Name = "cboMesesMigracion";
            this.cboMesesMigracion.Size = new System.Drawing.Size(65, 28);
            this.cboMesesMigracion.ToolTipText = "Número de meses hacia atras para integración de información";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // btnLogIntegracion
            // 
            this.btnLogIntegracion.Image = ((System.Drawing.Image)(resources.GetObject("btnLogIntegracion.Image")));
            this.btnLogIntegracion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogIntegracion.Name = "btnLogIntegracion";
            this.btnLogIntegracion.Size = new System.Drawing.Size(154, 25);
            this.btnLogIntegracion.Text = "Log de integración";
            this.btnLogIntegracion.ToolTipText = "Log de integración";
            this.btnLogIntegracion.Click += new System.EventHandler(this.btnLogIntegracion_Click);
            // 
            // tmIntegrarBD
            // 
            this.tmIntegrarBD.Tick += new System.EventHandler(this.tmIntegrarBD_Tick);
            // 
            // chkInicioSO
            // 
            this.chkInicioSO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInicioSO.BackColor = System.Drawing.Color.Transparent;
            this.chkInicioSO.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkInicioSO.Location = new System.Drawing.Point(919, 6);
            this.chkInicioSO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkInicioSO.Name = "chkInicioSO";
            this.chkInicioSO.Size = new System.Drawing.Size(225, 21);
            this.chkInicioSO.TabIndex = 2;
            this.chkInicioSO.Text = "Iniciar con sistema operativo";
            this.chkInicioSO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkInicioSO.UseVisualStyleBackColor = false;
            this.chkInicioSO.CheckedChanged += new System.EventHandler(this.chkInicioSO_CheckedChanged);
            // 
            // tmRevisionFTP
            // 
            this.tmRevisionFTP.Tick += new System.EventHandler(this.tmRevisionFTP_Tick);
            // 
            // btnMenu
            // 
            this.btnMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConfiguracion,
            this.btnIntegraciónDePaquetes,
            this.btnDirectorios});
            this.btnMenu.Image = global::DllTransferenciaSoft.Properties.Resources.server_components;
            this.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(100, 25);
            this.btnMenu.Text = "Opciones";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 28);
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(375, 24);
            this.btnConfiguracion.Text = "Configuración";
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // btnIntegraciónDePaquetes
            // 
            this.btnIntegraciónDePaquetes.Name = "btnIntegraciónDePaquetes";
            this.btnIntegraciónDePaquetes.Size = new System.Drawing.Size(375, 24);
            this.btnIntegraciónDePaquetes.Text = "Integración de paquetes";
            this.btnIntegraciónDePaquetes.Click += new System.EventHandler(this.btnIntegraciónDePaquetes_Click);
            // 
            // btnDirectorios
            // 
            this.btnDirectorios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDir_BD__Repositorio,
            this.btnDir_BD__Descompresion,
            this.btnDir_BD__Errores,
            this.btnDir_BD__Log,
            this.btnDir_BD__Proceso,
            this.toolStripSeparator8,
            this.btnDir_BD__Integradas});
            this.btnDirectorios.Name = "btnDirectorios";
            this.btnDirectorios.Size = new System.Drawing.Size(375, 24);
            this.btnDirectorios.Text = "Directorios de integración de bases de datos";
            // 
            // btnDir_BD__Integradas
            // 
            this.btnDir_BD__Integradas.Name = "btnDir_BD__Integradas";
            this.btnDir_BD__Integradas.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Integradas.Text = "Integradas";
            this.btnDir_BD__Integradas.Click += new System.EventHandler(this.btnDir_BD__Integradas_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(178, 6);
            // 
            // btnDir_BD__Repositorio
            // 
            this.btnDir_BD__Repositorio.Name = "btnDir_BD__Repositorio";
            this.btnDir_BD__Repositorio.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Repositorio.Text = "Repositorio";
            this.btnDir_BD__Repositorio.Click += new System.EventHandler(this.btnDir_BD__Repositorio_Click);
            // 
            // btnDir_BD__Descompresion
            // 
            this.btnDir_BD__Descompresion.Name = "btnDir_BD__Descompresion";
            this.btnDir_BD__Descompresion.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Descompresion.Text = "Descompresión";
            this.btnDir_BD__Descompresion.Click += new System.EventHandler(this.btnDir_BD__Descompresion_Click);
            // 
            // btnDir_BD__Errores
            // 
            this.btnDir_BD__Errores.Name = "btnDir_BD__Errores";
            this.btnDir_BD__Errores.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Errores.Text = "Errores";
            this.btnDir_BD__Errores.Click += new System.EventHandler(this.btnDir_BD__Errores_Click);
            // 
            // btnDir_BD__Log
            // 
            this.btnDir_BD__Log.Name = "btnDir_BD__Log";
            this.btnDir_BD__Log.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Log.Text = "Log";
            this.btnDir_BD__Log.Click += new System.EventHandler(this.btnDir_BD__Log_Click);
            // 
            // btnDir_BD__Proceso
            // 
            this.btnDir_BD__Proceso.Name = "btnDir_BD__Proceso";
            this.btnDir_BD__Proceso.Size = new System.Drawing.Size(181, 24);
            this.btnDir_BD__Proceso.Text = "Proceso";
            this.btnDir_BD__Proceso.Click += new System.EventHandler(this.btnDir_BD__Proceso_Click);
            // 
            // FrmIntegrarBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 576);
            this.ControlBox = false;
            this.Controls.Add(this.chkInicioSO);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameTablas);
            this.Controls.Add(this.FrameBasesDeDatos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmIntegrarBD";
            this.Text = "Integrador de Bases de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmIntegrarBD_FormClosing);
            this.Load += new System.EventHandler(this.FrmIntegrarBD_Load);
            this.FrameBasesDeDatos.ResumeLayout(false);
            this.mnFTP.ResumeLayout(false);
            this.FrameTablas.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameBasesDeDatos;
        private System.Windows.Forms.GroupBox FrameTablas;
        private System.Windows.Forms.ListView lstwBasesDeDatos;
        private System.Windows.Forms.ListView lstwTablasMigrar;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnIntegrarBD;
        private System.Windows.Forms.ColumnHeader BaseDeDatos;
        private System.Windows.Forms.ColumnHeader Tamaño;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.ColumnHeader Porcentaje;
        private System.Windows.Forms.ColumnHeader StatusProceso;
        private System.Windows.Forms.ColumnHeader Tabla;
        private System.Windows.Forms.ColumnHeader Procesada;
        private System.Windows.Forms.Timer tmIntegrarBD;
        private System.Windows.Forms.ColumnHeader Registros;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cboTiposMigracion;
        private System.Windows.Forms.ColumnHeader RegMigrar;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.ToolStripButton btnLogErrores;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ColumnHeader Registro;
        private System.Windows.Forms.CheckBox chkInicioSO;
        private System.Windows.Forms.Timer tmRevisionFTP;
        private System.Windows.Forms.ToolStripButton btnFTP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip mnFTP;
        private System.Windows.Forms.ToolStripMenuItem btnDirectorioFTP;
        private System.Windows.Forms.ToolStripButton btnAbrirFTP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDepurarFTP;
        private System.Windows.Forms.ToolStripComboBox cboItemsFTP;
        private System.Windows.Forms.ToolStripComboBox cboItemsFTP_Copiado;
        private System.Windows.Forms.ToolStripComboBox cboMesesMigracion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnLogIntegracion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton btnMenu;
        private System.Windows.Forms.ToolStripMenuItem btnConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem btnIntegraciónDePaquetes;
        private System.Windows.Forms.ToolStripMenuItem btnDirectorios;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Integradas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Repositorio;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Descompresion;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Errores;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Log;
        private System.Windows.Forms.ToolStripMenuItem btnDir_BD__Proceso;
    }
}