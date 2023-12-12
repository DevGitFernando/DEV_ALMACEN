namespace OficinaCentral.CuadrosBasicos
{
    partial class FrmCuadrosBasicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCuadrosBasicos));
            this.FramePerfiles = new System.Windows.Forms.GroupBox();
            this.twGrupos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.lwUsuarios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnClaves = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bntBuscarClaves = new System.Windows.Forms.ToolStripMenuItem();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblProcesados = new SC_ControlsCS.scLabelExt();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSubClientes = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboClientes = new SC_ControlsCS.scComboBoxExt();
            this.FramePerfiles.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.mnClaves.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // FramePerfiles
            // 
            this.FramePerfiles.Controls.Add(this.twGrupos);
            this.FramePerfiles.Location = new System.Drawing.Point(7, 116);
            this.FramePerfiles.Name = "FramePerfiles";
            this.FramePerfiles.Size = new System.Drawing.Size(395, 489);
            this.FramePerfiles.TabIndex = 2;
            this.FramePerfiles.TabStop = false;
            this.FramePerfiles.Text = "Perfiles";
            // 
            // twGrupos
            // 
            this.twGrupos.AllowDrop = true;
            this.twGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twGrupos.ContextMenuStrip = this.mnGrupos;
            this.twGrupos.FullRowSelect = true;
            this.twGrupos.ImageIndex = 1;
            this.twGrupos.ImageList = this.imgGruposUsuarios;
            this.twGrupos.Location = new System.Drawing.Point(10, 16);
            this.twGrupos.Name = "twGrupos";
            this.twGrupos.PathSeparator = "|";
            this.twGrupos.SelectedImageIndex = 0;
            this.twGrupos.ShowNodeToolTips = true;
            this.twGrupos.Size = new System.Drawing.Size(375, 464);
            this.twGrupos.StateImageList = this.imgGruposUsuarios;
            this.twGrupos.TabIndex = 0;
            this.twGrupos.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twGrupos_ItemDrag);
            this.twGrupos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGrupos_AfterSelect);
            this.twGrupos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragDrop);
            this.twGrupos.DragEnter += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragEnter);
            // 
            // mnGrupos
            // 
            this.mnGrupos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator2,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(143, 66);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar perfiles";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(139, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.lwUsuarios);
            this.FrameClaves.Location = new System.Drawing.Point(408, 116);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(635, 489);
            this.FrameClaves.TabIndex = 3;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Claves SSA";
            // 
            // lwUsuarios
            // 
            this.lwUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwUsuarios.ContextMenuStrip = this.mnClaves;
            this.lwUsuarios.FullRowSelect = true;
            this.lwUsuarios.Location = new System.Drawing.Point(10, 16);
            this.lwUsuarios.Name = "lwUsuarios";
            this.lwUsuarios.ShowItemToolTips = true;
            this.lwUsuarios.Size = new System.Drawing.Size(615, 464);
            this.lwUsuarios.SmallImageList = this.imgGruposUsuarios;
            this.lwUsuarios.TabIndex = 0;
            this.lwUsuarios.UseCompatibleStateImageBehavior = false;
            this.lwUsuarios.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwUsuarios_ItemDrag);
            // 
            // mnClaves
            // 
            this.mnClaves.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bntBuscarClaves});
            this.mnClaves.Name = "mnClaves";
            this.mnClaves.Size = new System.Drawing.Size(145, 26);
            // 
            // bntBuscarClaves
            // 
            this.bntBuscarClaves.Name = "bntBuscarClaves";
            this.bntBuscarClaves.Size = new System.Drawing.Size(144, 22);
            this.bntBuscarClaves.Text = "Buscar claves";
            this.bntBuscarClaves.Click += new System.EventHandler(this.bntBuscarClaves_Click);
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
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator7,
            this.btnExportarExcel,
            this.toolStripSeparator1,
            this.btnAbrir,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator6});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1052, 25);
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(23, 22);
            this.btnAbrir.Text = "&Abrir";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
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
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tmValidacion
            // 
            this.tmValidacion.Tick += new System.EventHandler(this.tmValidacion_Tick);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.lblProcesados);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(225, 277);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(602, 56);
            this.FrameProceso.TabIndex = 4;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // lblProcesados
            // 
            this.lblProcesados.Location = new System.Drawing.Point(293, 5);
            this.lblProcesados.MostrarToolTip = false;
            this.lblProcesados.Name = "lblProcesados";
            this.lblProcesados.Size = new System.Drawing.Size(284, 14);
            this.lblProcesados.TabIndex = 14;
            this.lblProcesados.Text = "scLabelExt1";
            this.lblProcesados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProcesados.Visible = false;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(574, 19);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.label1);
            this.FramePrincipal.Controls.Add(this.cboSubClientes);
            this.FramePrincipal.Controls.Add(this.lblEstado);
            this.FramePrincipal.Controls.Add(this.cboEstados);
            this.FramePrincipal.Controls.Add(this.lblEmpresa);
            this.FramePrincipal.Controls.Add(this.cboClientes);
            this.FramePrincipal.Location = new System.Drawing.Point(7, 28);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(1036, 85);
            this.FramePrincipal.TabIndex = 1;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos de Estado y Clientes";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(535, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Sub-Clientes :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubClientes
            // 
            this.cboSubClientes.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubClientes.Data = "";
            this.cboSubClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubClientes.Filtro = " 1 = 1";
            this.cboSubClientes.ListaItemsBusqueda = 20;
            this.cboSubClientes.Location = new System.Drawing.Point(610, 53);
            this.cboSubClientes.MostrarToolTip = false;
            this.cboSubClientes.Name = "cboSubClientes";
            this.cboSubClientes.Size = new System.Drawing.Size(416, 21);
            this.cboSubClientes.TabIndex = 2;
            this.cboSubClientes.SelectedIndexChanged += new System.EventHandler(this.cboSubClientes_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(1, 27);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(74, 19);
            this.lblEstado.TabIndex = 19;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(79, 26);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(416, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(1, 54);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(74, 19);
            this.lblEmpresa.TabIndex = 18;
            this.lblEmpresa.Text = "Clientes :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClientes
            // 
            this.cboClientes.BackColorEnabled = System.Drawing.Color.White;
            this.cboClientes.Data = "";
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.Filtro = " 1 = 1";
            this.cboClientes.ListaItemsBusqueda = 20;
            this.cboClientes.Location = new System.Drawing.Point(79, 53);
            this.cboClientes.MostrarToolTip = false;
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(416, 21);
            this.cboClientes.TabIndex = 1;
            this.cboClientes.SelectedIndexChanged += new System.EventHandler(this.cboClientes_SelectedIndexChanged);
            // 
            // FrmCuadrosBasicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 611);
            this.Controls.Add(this.FramePrincipal);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FramePerfiles);
            this.Controls.Add(this.FrameClaves);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCuadrosBasicos";
            this.Text = "Cuadros Basicos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.FramePerfiles.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameClaves.ResumeLayout(false);
            this.mnClaves.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.FramePrincipal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FramePerfiles;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.TreeView twGrupos;
        private System.Windows.Forms.ListView lwUsuarios;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip mnClaves;
        private System.Windows.Forms.ToolStripMenuItem bntBuscarClaves;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Timer tmValidacion;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private SC_ControlsCS.scLabelExt lblProcesados;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboSubClientes;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblEmpresa;
        private SC_ControlsCS.scComboBoxExt cboClientes;
    }
}