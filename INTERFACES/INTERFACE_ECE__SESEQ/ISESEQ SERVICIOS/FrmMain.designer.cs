namespace ISESEQ_SERVICIOS
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
            this.bntRegistroErrores = new System.Windows.Forms.ToolStripButton();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.BarraDeStatus = new System.Windows.Forms.StatusBar();
            this.lblModulo = new System.Windows.Forms.StatusBarPanel();
            this.lblFarmacia = new System.Windows.Forms.StatusBarPanel();
            this.lblServidor = new System.Windows.Forms.StatusBarPanel();
            this.lblBaseDeDatos = new System.Windows.Forms.StatusBarPanel();
            this.lblUsuarioConectado = new System.Windows.Forms.StatusBarPanel();
            this.mnPrincipal = new System.Windows.Forms.MenuStrip();
            this.btnMenuDeOpciones = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpciones_Almacen = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAlmacen_01__GenerarTXT = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAlmacen_02__GenerarTXT_Pedidos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAlmacen_03__GenerarTXT_Transferencias = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpciones_Farmacia = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFarmacia_01_EnviarAcuses = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEstadisticas = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpciones_General = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEnviarInformacion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnColectivos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            this.mnPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(436, 18);
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
            this.bntRegistroErrores});
            this.toolStrip.Location = new System.Drawing.Point(0, 18);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(624, 31);
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
            this.btnNavegador.Size = new System.Drawing.Size(36, 28);
            this.btnNavegador.Text = "Menú";
            this.btnNavegador.Visible = false;
            this.btnNavegador.Click += new System.EventHandler(this.btnNavegador_Click);
            // 
            // bntRegistroErrores
            // 
            this.bntRegistroErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntRegistroErrores.Image = ((System.Drawing.Image)(resources.GetObject("bntRegistroErrores.Image")));
            this.bntRegistroErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntRegistroErrores.Name = "bntRegistroErrores";
            this.bntRegistroErrores.Size = new System.Drawing.Size(36, 28);
            this.bntRegistroErrores.Text = "Log errores";
            this.bntRegistroErrores.ToolTipText = "Registro de Errores";
            this.bntRegistroErrores.Click += new System.EventHandler(this.bntRegistroErrores_Click);
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
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 342);
            this.BarraDeStatus.Margin = new System.Windows.Forms.Padding(2);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblFarmacia,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(624, 20);
            this.BarraDeStatus.TabIndex = 25;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Text = "Oficina Central v.1.0.0.0";
            this.lblModulo.Width = 136;
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
            // mnPrincipal
            // 
            this.mnPrincipal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenuDeOpciones,
            this.btnOpciones_Almacen,
            this.btnOpciones_Farmacia,
            this.btnOpciones_General});
            this.mnPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnPrincipal.Name = "mnPrincipal";
            this.mnPrincipal.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnPrincipal.Size = new System.Drawing.Size(624, 29);
            this.mnPrincipal.TabIndex = 27;
            this.mnPrincipal.Text = "menuStrip1";
            // 
            // btnMenuDeOpciones
            // 
            this.btnMenuDeOpciones.Name = "btnMenuDeOpciones";
            this.btnMenuDeOpciones.Size = new System.Drawing.Size(149, 25);
            this.btnMenuDeOpciones.Text = "Menú de opciones";
            // 
            // btnOpciones_Almacen
            // 
            this.btnOpciones_Almacen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAlmacen_01__GenerarTXT,
            this.btnAlmacen_02__GenerarTXT_Pedidos,
            this.btnAlmacen_03__GenerarTXT_Transferencias,
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias});
            this.btnOpciones_Almacen.Name = "btnOpciones_Almacen";
            this.btnOpciones_Almacen.Size = new System.Drawing.Size(82, 25);
            this.btnOpciones_Almacen.Text = "Almacén";
            // 
            // btnAlmacen_01__GenerarTXT
            // 
            this.btnAlmacen_01__GenerarTXT.Name = "btnAlmacen_01__GenerarTXT";
            this.btnAlmacen_01__GenerarTXT.Size = new System.Drawing.Size(424, 26);
            this.btnAlmacen_01__GenerarTXT.Text = "Generar documentos ventas";
            this.btnAlmacen_01__GenerarTXT.Click += new System.EventHandler(this.btnAlmacen_01__GenerarTXT_Click);
            // 
            // btnAlmacen_02__GenerarTXT_Pedidos
            // 
            this.btnAlmacen_02__GenerarTXT_Pedidos.Name = "btnAlmacen_02__GenerarTXT_Pedidos";
            this.btnAlmacen_02__GenerarTXT_Pedidos.Size = new System.Drawing.Size(424, 26);
            this.btnAlmacen_02__GenerarTXT_Pedidos.Text = "Generar documentos por pedidos (ventas)";
            this.btnAlmacen_02__GenerarTXT_Pedidos.Click += new System.EventHandler(this.btnAlmacen_02__GenerarTXT_Pedidos_Click);
            // 
            // btnAlmacen_03__GenerarTXT_Transferencias
            // 
            this.btnAlmacen_03__GenerarTXT_Transferencias.Name = "btnAlmacen_03__GenerarTXT_Transferencias";
            this.btnAlmacen_03__GenerarTXT_Transferencias.Size = new System.Drawing.Size(424, 26);
            this.btnAlmacen_03__GenerarTXT_Transferencias.Text = "Generar documentos transferencias";
            this.btnAlmacen_03__GenerarTXT_Transferencias.Click += new System.EventHandler(this.btnAlmacen_03__GenerarTXT_Transferencias_Click);
            // 
            // btnAlmacen_04__GenerarTXT_PedidosTransferencias
            // 
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias.Name = "btnAlmacen_04__GenerarTXT_PedidosTransferencias";
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias.Size = new System.Drawing.Size(424, 26);
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias.Text = "Generar documentos por pedidos (transferencias)";
            this.btnAlmacen_04__GenerarTXT_PedidosTransferencias.Click += new System.EventHandler(this.btnAlmacen_04__GenerarTXT_PedidosTransferencias_Click);
            // 
            // btnOpciones_Farmacia
            // 
            this.btnOpciones_Farmacia.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFarmacia_01_EnviarAcuses,
            this.btnEstadisticas,
            this.btnColectivos});
            this.btnOpciones_Farmacia.Name = "btnOpciones_Farmacia";
            this.btnOpciones_Farmacia.Size = new System.Drawing.Size(84, 25);
            this.btnOpciones_Farmacia.Text = "Farmacia";
            // 
            // btnFarmacia_01_EnviarAcuses
            // 
            this.btnFarmacia_01_EnviarAcuses.Name = "btnFarmacia_01_EnviarAcuses";
            this.btnFarmacia_01_EnviarAcuses.Size = new System.Drawing.Size(205, 26);
            this.btnFarmacia_01_EnviarAcuses.Text = "Surtido de recetas";
            this.btnFarmacia_01_EnviarAcuses.Visible = false;
            this.btnFarmacia_01_EnviarAcuses.Click += new System.EventHandler(this.btnFarmacia_01_EnviarAcuses_Click);
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(205, 26);
            this.btnEstadisticas.Text = "Estadisticas";
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            // 
            // btnOpciones_General
            // 
            this.btnOpciones_General.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEnviarInformacion});
            this.btnOpciones_General.Name = "btnOpciones_General";
            this.btnOpciones_General.Size = new System.Drawing.Size(76, 25);
            this.btnOpciones_General.Text = "General";
            // 
            // btnEnviarInformacion
            // 
            this.btnEnviarInformacion.Name = "btnEnviarInformacion";
            this.btnEnviarInformacion.Size = new System.Drawing.Size(280, 26);
            this.btnEnviarInformacion.Text = "Enviar información operativa";
            this.btnEnviarInformacion.Click += new System.EventHandler(this.btnEnviarInformacion_Click);
            // 
            // btnColectivos
            // 
            this.btnColectivos.Name = "btnColectivos";
            this.btnColectivos.Size = new System.Drawing.Size(205, 26);
            this.btnColectivos.Text = "Colectivos";
            this.btnColectivos.Click += new System.EventHandler(this.btnColectivos_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(624, 362);
            this.Controls.Add(this.BarraDeStatus);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mnPrincipal);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.Text = "Interface SESEQ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).EndInit();
            this.mnPrincipal.ResumeLayout(false);
            this.mnPrincipal.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton bntRegistroErrores;
        private System.Windows.Forms.MenuStrip mnPrincipal;
        private System.Windows.Forms.ToolStripMenuItem btnOpciones_Almacen;
        private System.Windows.Forms.ToolStripMenuItem btnOpciones_Farmacia;
        private System.Windows.Forms.ToolStripMenuItem btnFarmacia_01_EnviarAcuses;
        private System.Windows.Forms.ToolStripMenuItem btnAlmacen_01__GenerarTXT;
        private System.Windows.Forms.ToolStripMenuItem btnAlmacen_02__GenerarTXT_Pedidos;
        private System.Windows.Forms.ToolStripMenuItem btnMenuDeOpciones;
        private System.Windows.Forms.ToolStripMenuItem btnEstadisticas;
        private System.Windows.Forms.ToolStripMenuItem btnOpciones_General;
        private System.Windows.Forms.ToolStripMenuItem btnEnviarInformacion;
        private System.Windows.Forms.ToolStripMenuItem btnAlmacen_03__GenerarTXT_Transferencias;
        private System.Windows.Forms.ToolStripMenuItem btnAlmacen_04__GenerarTXT_PedidosTransferencias;
        private System.Windows.Forms.ToolStripMenuItem btnColectivos;
    }
}

