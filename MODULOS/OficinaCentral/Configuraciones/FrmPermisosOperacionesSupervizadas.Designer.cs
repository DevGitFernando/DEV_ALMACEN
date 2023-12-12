namespace OficinaCentral.Configuraciones
{
    partial class FrmPermisosOperacionesSupervizadas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPermisosOperacionesSupervizadas));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lwClientes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnClientes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twEstados = new System.Windows.Forms.TreeView();
            this.mnEstados = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarClientesDelEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgEstados = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnEstados.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Controls.Add(this.lwClientes);
            this.groupBox2.Location = new System.Drawing.Point(593, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 626);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clientes por Estado";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(11, 19);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 17);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(63, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(511, 21);
            this.cboEstados.TabIndex = 20;
            this.cboEstados.SelectedValueChanged += new System.EventHandler(this.cboEstados_SelectedValueChanged);
            // 
            // lwClientes
            // 
            this.lwClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwClientes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwClientes.ContextMenuStrip = this.mnClientes;
            this.lwClientes.FullRowSelect = true;
            this.lwClientes.Location = new System.Drawing.Point(6, 46);
            this.lwClientes.Name = "lwClientes";
            this.lwClientes.ShowItemToolTips = true;
            this.lwClientes.Size = new System.Drawing.Size(568, 574);
            this.lwClientes.SmallImageList = this.imgGruposUsuarios;
            this.lwClientes.TabIndex = 0;
            this.lwClientes.UseCompatibleStateImageBehavior = false;
            this.lwClientes.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwClientes_ItemDrag);
            // 
            // mnClientes
            // 
            this.mnClientes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolStripMenuItem3,
            this.toolStripSeparator8});
            this.mnClientes.Name = "mnGrupos";
            this.mnClientes.ShowImageMargin = false;
            this.mnClientes.Size = new System.Drawing.Size(126, 38);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(122, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(125, 22);
            this.toolStripMenuItem3.Text = "Actualizar lista";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(122, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twEstados);
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 626);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Farmacias";
            // 
            // twEstados
            // 
            this.twEstados.AllowDrop = true;
            this.twEstados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twEstados.ContextMenuStrip = this.mnEstados;
            this.twEstados.FullRowSelect = true;
            this.twEstados.ImageIndex = 1;
            this.twEstados.ImageList = this.imgEstados;
            this.twEstados.Location = new System.Drawing.Point(6, 19);
            this.twEstados.Name = "twEstados";
            this.twEstados.PathSeparator = "|";
            this.twEstados.SelectedImageIndex = 0;
            this.twEstados.ShowNodeToolTips = true;
            this.twEstados.Size = new System.Drawing.Size(568, 601);
            this.twEstados.TabIndex = 0;
            this.twEstados.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twEstados_ItemDrag);
            this.twEstados.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twEstados_AfterSelect);
            this.twEstados.DragDrop += new System.Windows.Forms.DragEventHandler(this.twEstados_DragDrop);
            this.twEstados.DragEnter += new System.Windows.Forms.DragEventHandler(this.twEstados_DragEnter);
            // 
            // mnEstados
            // 
            this.mnEstados.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.eliminarClientesDelEstadoToolStripMenuItem,
            this.toolStripSeparator1,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator3,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnEstados.Name = "mnGrupos";
            this.mnEstados.ShowImageMargin = false;
            this.mnEstados.Size = new System.Drawing.Size(203, 94);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar clientes de Farmacia";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar cliente";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
            // 
            // imgEstados
            // 
            this.imgEstados.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgEstados.ImageStream")));
            this.imgEstados.TransparentColor = System.Drawing.Color.Transparent;
            this.imgEstados.Images.SetKeyName(0, "Config.ico");
            this.imgEstados.Images.SetKeyName(1, "Folder.ico");
            this.imgEstados.Images.SetKeyName(2, "Usuario.ico");
            this.imgEstados.Images.SetKeyName(3, "Icon 286.ico");
            this.imgEstados.Images.SetKeyName(4, "Pantalla.ico");
            this.imgEstados.Images.SetKeyName(5, "Window.ico");
            this.imgEstados.Images.SetKeyName(6, "WinXPSetV4 Icon 59.ico");
            this.imgEstados.Images.SetKeyName(7, "CarpetaAbierta03.ICO");
            this.imgEstados.Images.SetKeyName(8, "Principal.ICO");
            // 
            // FrmPermisosOperacionesSupervizadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 637);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmPermisosOperacionesSupervizadas";
            this.Text = "Asignar permisos especiales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosClientes_Load);
            this.groupBox2.ResumeLayout(false);
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnEstados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lwClientes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView twEstados;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        internal System.Windows.Forms.ImageList imgEstados;
        private System.Windows.Forms.ContextMenuStrip mnEstados;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem eliminarClientesDelEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip mnClientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;

    }
}