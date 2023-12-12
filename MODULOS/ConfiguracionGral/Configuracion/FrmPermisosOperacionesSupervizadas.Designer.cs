namespace Configuracion.Configuracion
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
            this.lwPersonal = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.mnClientes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twOperaciones = new System.Windows.Forms.TreeView();
            this.mnEstados = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarClientesDelEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgEstados = new System.Windows.Forms.ImageList(this.components);
            this.FrameEstadoFarmacia = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnEstados.SuspendLayout();
            this.FrameEstadoFarmacia.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lwPersonal);
            this.groupBox2.Location = new System.Drawing.Point(532, 78);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(520, 559);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuarios Operativos";
            // 
            // lwPersonal
            // 
            this.lwPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwPersonal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwPersonal.FullRowSelect = true;
            this.lwPersonal.HideSelection = false;
            this.lwPersonal.Location = new System.Drawing.Point(8, 23);
            this.lwPersonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lwPersonal.Name = "lwPersonal";
            this.lwPersonal.Size = new System.Drawing.Size(503, 527);
            this.lwPersonal.SmallImageList = this.imgGruposUsuarios;
            this.lwPersonal.TabIndex = 0;
            this.lwPersonal.UseCompatibleStateImageBehavior = false;
            this.lwPersonal.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwClientes_ItemDrag);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(611, 23);
            this.cboFarmacias.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(423, 24);
            this.cboFarmacias.TabIndex = 22;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
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
            this.cboEstados.Location = new System.Drawing.Point(88, 23);
            this.cboEstados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(423, 24);
            this.cboEstados.TabIndex = 20;
            this.cboEstados.SelectedValueChanged += new System.EventHandler(this.cboEstados_SelectedValueChanged);
            // 
            // mnClientes
            // 
            this.mnClientes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnClientes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.toolStripMenuItem3,
            this.toolStripSeparator8});
            this.mnClientes.Name = "mnGrupos";
            this.mnClientes.ShowImageMargin = false;
            this.mnClientes.Size = new System.Drawing.Size(151, 40);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(147, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(150, 24);
            this.toolStripMenuItem3.Text = "Actualizar lista";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(147, 6);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.twOperaciones);
            this.groupBox1.Location = new System.Drawing.Point(8, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(520, 559);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operaciones pantallas";
            // 
            // twOperaciones
            // 
            this.twOperaciones.AllowDrop = true;
            this.twOperaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twOperaciones.ContextMenuStrip = this.mnEstados;
            this.twOperaciones.FullRowSelect = true;
            this.twOperaciones.ImageIndex = 1;
            this.twOperaciones.ImageList = this.imgEstados;
            this.twOperaciones.Location = new System.Drawing.Point(12, 23);
            this.twOperaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.twOperaciones.Name = "twOperaciones";
            this.twOperaciones.PathSeparator = "|";
            this.twOperaciones.SelectedImageIndex = 0;
            this.twOperaciones.Size = new System.Drawing.Size(495, 527);
            this.twOperaciones.TabIndex = 0;
            this.twOperaciones.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twEstados_ItemDrag);
            this.twOperaciones.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twEstados_AfterSelect);
            this.twOperaciones.DragDrop += new System.Windows.Forms.DragEventHandler(this.twEstados_DragDrop);
            this.twOperaciones.DragEnter += new System.Windows.Forms.DragEventHandler(this.twEstados_DragEnter);
            // 
            // mnEstados
            // 
            this.mnEstados.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.mnEstados.Size = new System.Drawing.Size(243, 100);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar usuarios de Permiso";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.eliminarToolStripMenuItem.Text = "Eliminar usuario";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(239, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(242, 24);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(239, 6);
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
            // FrameEstadoFarmacia
            // 
            this.FrameEstadoFarmacia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEstadoFarmacia.Controls.Add(this.label2);
            this.FrameEstadoFarmacia.Controls.Add(this.cboFarmacias);
            this.FrameEstadoFarmacia.Controls.Add(this.lblFarmacia);
            this.FrameEstadoFarmacia.Controls.Add(this.cboEstados);
            this.FrameEstadoFarmacia.Location = new System.Drawing.Point(9, 7);
            this.FrameEstadoFarmacia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameEstadoFarmacia.Name = "FrameEstadoFarmacia";
            this.FrameEstadoFarmacia.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameEstadoFarmacia.Size = new System.Drawing.Size(1043, 66);
            this.FrameEstadoFarmacia.TabIndex = 8;
            this.FrameEstadoFarmacia.TabStop = false;
            this.FrameEstadoFarmacia.Text = "Información";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "Estado :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFarmacia.Location = new System.Drawing.Point(532, 26);
            this.lblFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(76, 21);
            this.lblFarmacia.TabIndex = 18;
            this.lblFarmacia.Text = "Farmacia :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPermisosOperacionesSupervizadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 642);
            this.Controls.Add(this.FrameEstadoFarmacia);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmPermisosOperacionesSupervizadas";
            this.ShowIcon = false;
            this.Text = "Permisos en pantallas Operación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosClientes_Load);
            this.groupBox2.ResumeLayout(false);
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnEstados.ResumeLayout(false);
            this.FrameEstadoFarmacia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lwPersonal;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView twOperaciones;
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
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.GroupBox FrameEstadoFarmacia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFarmacia;
    }
}