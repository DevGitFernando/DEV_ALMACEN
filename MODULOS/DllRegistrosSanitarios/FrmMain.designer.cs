namespace DllRegistrosSanitarios
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
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.BarraDeStatus = new System.Windows.Forms.StatusBar();
            this.lblModulo = new System.Windows.Forms.StatusBarPanel();
            this.lblFarmacia = new System.Windows.Forms.StatusBarPanel();
            this.lblServidor = new System.Windows.Forms.StatusBarPanel();
            this.lblBaseDeDatos = new System.Windows.Forms.StatusBarPanel();
            this.lblUsuarioConectado = new System.Windows.Forms.StatusBarPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSincronizarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(581, 24);
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
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 447);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblFarmacia,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(832, 26);
            this.BarraDeStatus.TabIndex = 25;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Text = "Oficina Central v.1.0.0.0";
            this.lblModulo.Width = 158;
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
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSincronizarDatos,
            this.toolStripButton2});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(832, 55);
            this.toolStrip.TabIndex = 27;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnSincronizarDatos
            // 
            this.btnSincronizarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSincronizarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnSincronizarDatos.Image")));
            this.btnSincronizarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSincronizarDatos.Name = "btnSincronizarDatos";
            this.btnSincronizarDatos.Size = new System.Drawing.Size(52, 52);
            this.btnSincronizarDatos.Text = "Sincronizar datos";
            this.btnSincronizarDatos.Click += new System.EventHandler(this.btnSincronizarDatos_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton2.Text = "Sincronizar datos";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(832, 473);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.BarraDeStatus);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.Text = "Registros Sanitarios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
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
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.StatusBar BarraDeStatus;
        private System.Windows.Forms.StatusBarPanel lblModulo;
        private System.Windows.Forms.StatusBarPanel lblFarmacia;
        private System.Windows.Forms.StatusBarPanel lblServidor;
        private System.Windows.Forms.StatusBarPanel lblBaseDeDatos;
        private System.Windows.Forms.StatusBarPanel lblUsuarioConectado;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnSincronizarDatos;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

    }
}

