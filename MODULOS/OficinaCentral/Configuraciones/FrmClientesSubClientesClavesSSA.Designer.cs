namespace OficinaCentral.Configuraciones
{
    partial class FrmClientesSubClientesClavesSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientesSubClientesClavesSSA));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.twSubClientes = new System.Windows.Forms.TreeView();
            this.mnSubClientes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarClientesDelEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.cargarClavesAsignadasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.buscarClavesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.mnClientes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMnActualizarListaClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cargarClavesAsignadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.buscarClavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.imgSales = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twClientes = new System.Windows.Forms.TreeView();
            this.lblClave = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.mnSubClientes.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.twSubClientes);
            this.groupBox2.Location = new System.Drawing.Point(596, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 626);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sub-Clientes";
            // 
            // twSubClientes
            // 
            this.twSubClientes.AllowDrop = true;
            this.twSubClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twSubClientes.ContextMenuStrip = this.mnSubClientes;
            this.twSubClientes.FullRowSelect = true;
            this.twSubClientes.ImageIndex = 1;
            this.twSubClientes.ImageList = this.imgGruposUsuarios;
            this.twSubClientes.Location = new System.Drawing.Point(8, 19);
            this.twSubClientes.Name = "twSubClientes";
            this.twSubClientes.PathSeparator = "|";
            this.twSubClientes.SelectedImageIndex = 0;
            this.twSubClientes.ShowNodeToolTips = true;
            this.twSubClientes.Size = new System.Drawing.Size(566, 599);
            this.twSubClientes.TabIndex = 1;
            this.twSubClientes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twSubClientes_AfterSelect);
            this.twSubClientes.DragDrop += new System.Windows.Forms.DragEventHandler(this.twSubClientes_DragDrop);
            this.twSubClientes.DragEnter += new System.Windows.Forms.DragEventHandler(this.twSubClientes_DragEnter);
            // 
            // mnSubClientes
            // 
            this.mnSubClientes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.eliminarClientesDelEstadoToolStripMenuItem,
            this.toolStripSeparator1,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator3,
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem,
            this.toolStripSeparator5,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator11,
            this.cargarClavesAsignadasToolStripMenuItem1,
            this.toolStripSeparator10,
            this.buscarClavesToolStripMenuItem1,
            this.toolStripSeparator4});
            this.mnSubClientes.Name = "mnGrupos";
            this.mnSubClientes.ShowImageMargin = false;
            this.mnSubClientes.Size = new System.Drawing.Size(250, 178);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(246, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar Clave SSA de Sub-Cliente ";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(246, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Clave SSA";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(246, 6);
            // 
            // imprimirReporteDeClavesAsignadasToolStripMenuItem
            // 
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem.Name = "imprimirReporteDeClavesAsignadasToolStripMenuItem";
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem.Text = "Imprimir Reporte de Claves Asignadas";
            this.imprimirReporteDeClavesAsignadasToolStripMenuItem.Click += new System.EventHandler(this.imprimirReporteDeClavesAsignadasToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(246, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(246, 6);
            // 
            // cargarClavesAsignadasToolStripMenuItem1
            // 
            this.cargarClavesAsignadasToolStripMenuItem1.Name = "cargarClavesAsignadasToolStripMenuItem1";
            this.cargarClavesAsignadasToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.cargarClavesAsignadasToolStripMenuItem1.Text = "Cargar Claves Asignadas";
            this.cargarClavesAsignadasToolStripMenuItem1.Click += new System.EventHandler(this.cargarClavesAsignadasToolStripMenuItem1_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(246, 6);
            // 
            // buscarClavesToolStripMenuItem1
            // 
            this.buscarClavesToolStripMenuItem1.Name = "buscarClavesToolStripMenuItem1";
            this.buscarClavesToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.buscarClavesToolStripMenuItem1.Text = "Buscar Claves";
            this.buscarClavesToolStripMenuItem1.Click += new System.EventHandler(this.buscarClavesToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(246, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Grupos.ICO");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(2, "Pill 2.ico");
            // 
            // mnClientes
            // 
            this.mnClientes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.btnMnActualizarListaClientes,
            this.toolStripSeparator6,
            this.cargarClavesAsignadasToolStripMenuItem,
            this.toolStripSeparator9,
            this.buscarClavesToolStripMenuItem,
            this.toolStripSeparator8});
            this.mnClientes.Name = "mnGrupos";
            this.mnClientes.ShowImageMargin = false;
            this.mnClientes.Size = new System.Drawing.Size(179, 94);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // btnMnActualizarListaClientes
            // 
            this.btnMnActualizarListaClientes.Name = "btnMnActualizarListaClientes";
            this.btnMnActualizarListaClientes.Size = new System.Drawing.Size(178, 22);
            this.btnMnActualizarListaClientes.Text = "Actualizar lista";
            this.btnMnActualizarListaClientes.Click += new System.EventHandler(this.btnMnActualizarListaClientes_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(175, 6);
            // 
            // cargarClavesAsignadasToolStripMenuItem
            // 
            this.cargarClavesAsignadasToolStripMenuItem.Name = "cargarClavesAsignadasToolStripMenuItem";
            this.cargarClavesAsignadasToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.cargarClavesAsignadasToolStripMenuItem.Text = "Cargar Claves Asignadas";
            this.cargarClavesAsignadasToolStripMenuItem.Click += new System.EventHandler(this.cargarClavesAsignadasToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(175, 6);
            // 
            // buscarClavesToolStripMenuItem
            // 
            this.buscarClavesToolStripMenuItem.Name = "buscarClavesToolStripMenuItem";
            this.buscarClavesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.buscarClavesToolStripMenuItem.Text = "Buscar Claves";
            this.buscarClavesToolStripMenuItem.Click += new System.EventHandler(this.buscarClavesToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(175, 6);
            // 
            // imgSales
            // 
            this.imgSales.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgSales.ImageStream")));
            this.imgSales.TransparentColor = System.Drawing.Color.Transparent;
            this.imgSales.Images.SetKeyName(0, "Pill 2.ico");
            this.imgSales.Images.SetKeyName(1, "Folder.ico");
            this.imgSales.Images.SetKeyName(2, "Config.ico");
            this.imgSales.Images.SetKeyName(3, "Usuario.ico");
            this.imgSales.Images.SetKeyName(4, "Icon 286.ico");
            this.imgSales.Images.SetKeyName(5, "Pantalla.ico");
            this.imgSales.Images.SetKeyName(6, "Window.ico");
            this.imgSales.Images.SetKeyName(7, "WinXPSetV4 Icon 59.ico");
            this.imgSales.Images.SetKeyName(8, "CarpetaAbierta03.ICO");
            this.imgSales.Images.SetKeyName(9, "Principal.ICO");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twClientes);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 626);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clientes";
            // 
            // twClientes
            // 
            this.twClientes.AllowDrop = true;
            this.twClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twClientes.ContextMenuStrip = this.mnClientes;
            this.twClientes.FullRowSelect = true;
            this.twClientes.ImageIndex = 1;
            this.twClientes.ImageList = this.imgGruposUsuarios;
            this.twClientes.Location = new System.Drawing.Point(6, 19);
            this.twClientes.Name = "twClientes";
            this.twClientes.PathSeparator = "|";
            this.twClientes.SelectedImageIndex = 0;
            this.twClientes.ShowNodeToolTips = true;
            this.twClientes.Size = new System.Drawing.Size(566, 599);
            this.twClientes.TabIndex = 0;
            this.twClientes.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.twClientes_BeforeLabelEdit);
            this.twClientes.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twSubClientes_ItemDrag);
            this.twClientes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twEstados_AfterSelect);
            // 
            // lblClave
            // 
            this.lblClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClave.Location = new System.Drawing.Point(7, 634);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(1170, 40);
            this.lblClave.TabIndex = 8;
            this.lblClave.Text = "label1";
            // 
            // FrmClientesSubClientesClavesSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmClientesSubClientesClavesSSA";
            this.Text = "Configuración de Claves SSA por Sub-Clientes ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmClientesSubClientesClavesSSA_Load);
            this.groupBox2.ResumeLayout(false);
            this.mnSubClientes.ResumeLayout(false);
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView twClientes;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        internal System.Windows.Forms.ImageList imgSales;
        private System.Windows.Forms.ContextMenuStrip mnSubClientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem eliminarClientesDelEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip mnClientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem btnMnActualizarListaClientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TreeView twSubClientes;
        private System.Windows.Forms.ToolStripMenuItem imprimirReporteDeClavesAsignadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cargarClavesAsignadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem buscarClavesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem cargarClavesAsignadasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem buscarClavesToolStripMenuItem1;

    }
}