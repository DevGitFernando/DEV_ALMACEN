﻿namespace Almacen
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
            this.imgNavegacion_2 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNavegador = new System.Windows.Forms.ToolStripButton();
            this.btnCambiarPassword = new System.Windows.Forms.ToolStripButton();
            this.bntRegistroErrores = new System.Windows.Forms.ToolStripButton();
            this.btnBuscarActualizaciones = new System.Windows.Forms.ToolStripButton();
            this.btnGetInformacion = new System.Windows.Forms.ToolStripButton();
            this.btnExportarInformacion = new System.Windows.Forms.ToolStripButton();
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
            this.pcStatusComunicacion = new System.Windows.Forms.PictureBox();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.tmConteosCiclicos = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInicio = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPassword = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcStatusComunicacion)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.btnExportarInformacion,
            this.btnInformación});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(720, 40);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Visible = false;
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
            // btnExportarInformacion
            // 
            this.btnExportarInformacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarInformacion.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarInformacion.Image")));
            this.btnExportarInformacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarInformacion.Name = "btnExportarInformacion";
            this.btnExportarInformacion.Size = new System.Drawing.Size(36, 37);
            this.btnExportarInformacion.Text = "Exportar información";
            this.btnExportarInformacion.ToolTipText = "Exportar información";
            this.btnExportarInformacion.Click += new System.EventHandler(this.btnExportarInformacion_Click);
            // 
            // btnInformación
            // 
            this.btnInformación.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInformación.Image = ((System.Drawing.Image)(resources.GetObject("btnInformación.Image")));
            this.btnInformación.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInformación.Name = "btnInformación";
            this.btnInformación.Size = new System.Drawing.Size(36, 37);
            this.btnInformación.Text = "Información";
            this.btnInformación.Click += new System.EventHandler(this.btnInformación_Click);
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
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 359);
            this.BarraDeStatus.Margin = new System.Windows.Forms.Padding(2);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblFechaSistema,
            this.lblFarmacia,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(720, 26);
            this.BarraDeStatus.TabIndex = 26;
            this.BarraDeStatus.Visible = false;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Text = "Almacén v.1.0.0.0";
            this.lblModulo.Width = 122;
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
            this.mnPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnPrincipal.Name = "mnPrincipal";
            this.mnPrincipal.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.mnPrincipal.Size = new System.Drawing.Size(720, 28);
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
            // pcStatusComunicacion
            // 
            this.pcStatusComunicacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pcStatusComunicacion.Location = new System.Drawing.Point(515, 8);
            this.pcStatusComunicacion.Margin = new System.Windows.Forms.Padding(2);
            this.pcStatusComunicacion.Name = "pcStatusComunicacion";
            this.pcStatusComunicacion.Size = new System.Drawing.Size(105, 15);
            this.pcStatusComunicacion.TabIndex = 43;
            this.pcStatusComunicacion.TabStop = false;
            this.pcStatusComunicacion.Visible = false;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.Location = new System.Drawing.Point(624, 5);
            this.btnRefrescar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(89, 19);
            this.btnRefrescar.TabIndex = 47;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Visible = false;
            this.btnRefrescar.LocationChanged += new System.EventHandler(this.btnRefrescar_LocationChanged);
            this.btnRefrescar.SizeChanged += new System.EventHandler(this.btnRefrescar_SizeChanged);
            // 
            // tmConteosCiclicos
            // 
            this.tmConteosCiclicos.Interval = 30000;
            this.tmConteosCiclicos.Tick += new System.EventHandler(this.tmConteosCiclicos_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.Font = new System.Drawing.Font("Rockwell", 9F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator12,
            this.btnInicio,
            this.btnMenu,
            this.toolStripSeparator10,
            this.btnPassword});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(720, 70);
            this.menuStrip.TabIndex = 49;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 66);
            // 
            // btnInicio
            // 
            this.btnInicio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.btnSalir,
            this.toolStripSeparator4});
            this.btnInicio.Image = ((System.Drawing.Image)(resources.GetObject("btnInicio.Image")));
            this.btnInicio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInicio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(95, 63);
            this.btnInicio.Text = "Inicio";
            this.btnInicio.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 38);
            this.toolStripMenuItem1.Text = "Ayuda  F1";
            this.toolStripMenuItem1.Visible = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(170, 38);
            this.btnSalir.Text = "Salir";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // btnMenu
            // 
            this.btnMenu.AutoSize = false;
            this.btnMenu.Font = new System.Drawing.Font("Calisto MT", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(160, 65);
            this.btnMenu.Text = "Opciones";
            this.btnMenu.ToolTipText = "Pantallas";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 66);
            this.toolStripSeparator10.Visible = false;
            // 
            // btnPassword
            // 
            this.btnPassword.Image = ((System.Drawing.Image)(resources.GetObject("btnPassword.Image")));
            this.btnPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.ShowDropDownArrow = false;
            this.btnPassword.Size = new System.Drawing.Size(204, 63);
            this.btnPassword.Text = "Cambiar Password";
            this.btnPassword.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(720, 385);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.pcStatusComunicacion);
            this.Controls.Add(this.BarraDeStatus);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mnPrincipal);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.ShowIcon = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.pcStatusComunicacion)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.ToolStripButton btnExportarInformacion;
        private System.Windows.Forms.Timer tmCleanUp;
        private System.Windows.Forms.Timer tmCheckExistencia;
        private System.Windows.Forms.PictureBox pcStatusComunicacion;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Timer tmConteosCiclicos;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripDropDownButton btnInicio;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton btnMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripDropDownButton btnPassword;
    }
}

