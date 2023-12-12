namespace SII_INT_Hitachi_Encoder
{
    using System;
    using System.Drawing;

    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.imgNavegacion_2 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNavegador = new System.Windows.Forms.ToolStripButton();
            this.btnCambiarPassword = new System.Windows.Forms.ToolStripButton();
            this.bntRegistroErrores = new System.Windows.Forms.ToolStripButton();
            this.btnBuscarActualizaciones = new System.Windows.Forms.ToolStripButton();
            this.btnGetInformacion = new System.Windows.Forms.ToolStripButton();
            this.btnInformación = new System.Windows.Forms.ToolStripButton();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.BarraDeStatus = new System.Windows.Forms.StatusBar();
            this.lblModulo = new System.Windows.Forms.StatusBarPanel();
            this.lblFechaSistema = new System.Windows.Forms.StatusBarPanel();
            this.lblFarmacia = new System.Windows.Forms.StatusBarPanel();
            this.lblServidor = new System.Windows.Forms.StatusBarPanel();
            this.lblBaseDeDatos = new System.Windows.Forms.StatusBarPanel();
            this.lblUsuarioConectado = new System.Windows.Forms.StatusBarPanel();
            this.tmDatosPersonalConectado = new System.Windows.Forms.Timer(this.components);
            this.tmServicioInformacion = new System.Windows.Forms.Timer(this.components);
            this.tmUpdater = new System.Windows.Forms.Timer(this.components);
            this.tmSesionDeUsuario = new System.Windows.Forms.Timer(this.components);
            this.mnPrincipal = new System.Windows.Forms.MenuStrip();
            this.tmCleanUp = new System.Windows.Forms.Timer(this.components);
            this.tmCheckExistencia = new System.Windows.Forms.Timer(this.components);
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(695, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Visible = false;
            // 
            // imgNavegacion_2
            // 
            this.imgNavegacion_2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNavegacion_2.ImageStream")));
            this.imgNavegacion_2.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNavegacion_2.Images.SetKeyName(0, "Config.ico");
            this.imgNavegacion_2.Images.SetKeyName(1, "Folder.ico");
            this.imgNavegacion_2.Images.SetKeyName(2, "Window.ico");
            this.imgNavegacion_2.Images.SetKeyName(3, "Pantalla.ico");
            this.imgNavegacion_2.Images.SetKeyName(4, "WinXPSetV4 Icon 59.ico");
            this.imgNavegacion_2.Images.SetKeyName(5, "CarpetaAbierta03.ICO");
            this.imgNavegacion_2.Images.SetKeyName(6, "Principal.ICO");
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNavegador,
            this.btnCambiarPassword,
            this.bntRegistroErrores,
            this.btnBuscarActualizaciones,
            this.btnGetInformacion,
            this.btnInformación});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(695, 40);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnNavegador
            // 
            this.btnNavegador.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNavegador.Image = ((System.Drawing.Image)(resources.GetObject("btnNavegador.Image")));
            this.btnNavegador.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNavegador.Name = "btnNavegador";
            this.btnNavegador.Size = new System.Drawing.Size(36, 37);
            this.btnNavegador.Text = "Menú";
            this.btnNavegador.ToolTipText = "Menú";
            this.btnNavegador.Click += new System.EventHandler(this.btnNavegador_Click);
            // 
            // btnCambiarPassword
            // 
            this.btnCambiarPassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCambiarPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnCambiarPassword.Image")));
            this.btnCambiarPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCambiarPassword.Name = "btnCambiarPassword";
            this.btnCambiarPassword.Size = new System.Drawing.Size(36, 37);
            this.btnCambiarPassword.Text = "Cambiar password";
            this.btnCambiarPassword.ToolTipText = "Cambiar password";
            this.btnCambiarPassword.Click += new System.EventHandler(this.btnCambiarPassword_Click);
            // 
            // bntRegistroErrores
            // 
            this.bntRegistroErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntRegistroErrores.Image = ((System.Drawing.Image)(resources.GetObject("bntRegistroErrores.Image")));
            this.bntRegistroErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntRegistroErrores.Name = "bntRegistroErrores";
            this.bntRegistroErrores.Size = new System.Drawing.Size(36, 37);
            this.bntRegistroErrores.Text = "Registro de Errores";
            this.bntRegistroErrores.ToolTipText = "Registro de Errores";
            this.bntRegistroErrores.Click += new System.EventHandler(this.bntRegistroErrores_Click);
            // 
            // btnBuscarActualizaciones
            // 
            this.btnBuscarActualizaciones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBuscarActualizaciones.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarActualizaciones.Image")));
            this.btnBuscarActualizaciones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscarActualizaciones.Name = "btnBuscarActualizaciones";
            this.btnBuscarActualizaciones.Size = new System.Drawing.Size(36, 37);
            this.btnBuscarActualizaciones.Text = "Buscar actualizaciones";
            this.btnBuscarActualizaciones.ToolTipText = "Buscar actualizaciones";
            this.btnBuscarActualizaciones.Click += new System.EventHandler(this.btnBuscarActualizaciones_Click);
            // 
            // btnGetInformacion
            // 
            this.btnGetInformacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGetInformacion.Image = ((System.Drawing.Image)(resources.GetObject("btnGetInformacion.Image")));
            this.btnGetInformacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetInformacion.Name = "btnGetInformacion";
            this.btnGetInformacion.Size = new System.Drawing.Size(36, 37);
            this.btnGetInformacion.Text = "Solicitar Información de Catalogos";
            this.btnGetInformacion.Click += new System.EventHandler(this.btnGetInformacion_Click);
            // 
            // btnInformación
            // 
            this.btnInformación.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInformación.Image = ((System.Drawing.Image)(resources.GetObject("btnInformación.Image")));
            this.btnInformación.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInformación.Name = "btnInformación";
            this.btnInformación.Size = new System.Drawing.Size(36, 37);
            this.btnInformación.Text = "Información";
            // 
            // imgNavegacion
            // 
            this.imgNavegacion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNavegacion.ImageStream")));
            this.imgNavegacion.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNavegacion.Images.SetKeyName(0, "Icon 286.ico");
            this.imgNavegacion.Images.SetKeyName(1, "Folder.ico");
            this.imgNavegacion.Images.SetKeyName(2, "Pantalla.ico");
            this.imgNavegacion.Images.SetKeyName(3, "Window.ico");
            this.imgNavegacion.Images.SetKeyName(4, "WinXPSetV4 Icon 59.ico");
            this.imgNavegacion.Images.SetKeyName(5, "CarpetaAbierta03.ICO");
            this.imgNavegacion.Images.SetKeyName(6, "Principal.ICO");
            this.imgNavegacion.Images.SetKeyName(7, "Config.ico");
            // 
            // BarraDeStatus
            // 
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 398);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblFechaSistema,
            this.lblFarmacia,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(695, 26);
            this.BarraDeStatus.TabIndex = 26;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Text = "Almacén v.1.0.0.0";
            this.lblModulo.Width = 105;
            // 
            // lblFechaSistema
            // 
            this.lblFechaSistema.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblFechaSistema.Name = "lblFechaSistema";
            this.lblFechaSistema.Width = 10;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Width = 10;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Width = 10;
            // 
            // lblBaseDeDatos
            // 
            this.lblBaseDeDatos.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblBaseDeDatos.Name = "lblBaseDeDatos";
            this.lblBaseDeDatos.Width = 10;
            // 
            // lblUsuarioConectado
            // 
            this.lblUsuarioConectado.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblUsuarioConectado.Name = "lblUsuarioConectado";
            this.lblUsuarioConectado.Width = 10;
            // 
            // tmDatosPersonalConectado
            // 
            this.tmDatosPersonalConectado.Interval = 5000;
            this.tmDatosPersonalConectado.Tick += new System.EventHandler(this.tmDatosPersonalConectado_Tick);
            // 
            // tmServicioInformacion
            // 
            this.tmServicioInformacion.Interval = 15000;
            this.tmServicioInformacion.Tick += new System.EventHandler(this.tmServicioInformacion_Tick);
            // 
            // tmUpdater
            // 
            this.tmUpdater.Interval = 1000;
            // 
            // tmSesionDeUsuario
            // 
            this.tmSesionDeUsuario.Interval = 30000;
            this.tmSesionDeUsuario.Tick += new System.EventHandler(this.tmSesionDeUsuario_Tick);
            // 
            // mnPrincipal
            // 
            this.mnPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnPrincipal.Name = "mnPrincipal";
            this.mnPrincipal.Size = new System.Drawing.Size(695, 24);
            this.mnPrincipal.TabIndex = 28;
            this.mnPrincipal.Text = "menuStrip1";
            this.mnPrincipal.Visible = false;
            // 
            // tmCleanUp
            // 
            this.tmCleanUp.Interval = 5000;
            this.tmCleanUp.Tick += new System.EventHandler(this.tmCleanUp_Tick);
            // 
            // tmCheckExistencia
            // 
            this.tmCheckExistencia.Interval = 5000;
            this.tmCheckExistencia.Tick += new System.EventHandler(this.tmCheckExistencia_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(695, 424);
            this.Controls.Add(this.BarraDeStatus);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.mnPrincipal);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.Text = "Menú principal del sistema";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaSistema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /*
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem estadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem municipiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coloniasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requisicionesToolStripMenuItem;
        */
        private System.Windows.Forms.MenuStrip menuStrip;
        internal System.Windows.Forms.ImageList imgNavegacion_2;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNavegador;
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.StatusBar BarraDeStatus;
        private System.Windows.Forms.StatusBarPanel lblModulo;
        private System.Windows.Forms.StatusBarPanel lblFarmacia;
        private System.Windows.Forms.StatusBarPanel lblServidor;
        private System.Windows.Forms.StatusBarPanel lblBaseDeDatos;
        private System.Windows.Forms.StatusBarPanel lblUsuarioConectado;
        private System.Windows.Forms.Timer tmDatosPersonalConectado;
        private System.Windows.Forms.ToolStripButton btnCambiarPassword;
        private System.Windows.Forms.StatusBarPanel lblFechaSistema;
        private System.Windows.Forms.Timer tmServicioInformacion;
        private System.Windows.Forms.Timer tmUpdater;
        private System.Windows.Forms.ToolStripButton btnBuscarActualizaciones;
        private System.Windows.Forms.ToolStripButton bntRegistroErrores;
        private System.Windows.Forms.ToolStripButton btnGetInformacion;
        private System.Windows.Forms.Timer tmSesionDeUsuario;
        private System.Windows.Forms.MenuStrip mnPrincipal;
        private System.Windows.Forms.ToolStripButton btnInformación;
        private System.Windows.Forms.Timer tmCleanUp;
        private System.Windows.Forms.Timer tmCheckExistencia;

    }
}

