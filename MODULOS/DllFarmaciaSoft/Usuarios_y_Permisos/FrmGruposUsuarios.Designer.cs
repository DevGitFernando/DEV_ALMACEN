namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmGruposUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGruposUsuarios));
            this.FrameGrupos = new System.Windows.Forms.GroupBox();
            this.twGrupos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.modificarNombreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.FrameUsuarios = new System.Windows.Forms.GroupBox();
            this.lwUsuarios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnUsuarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.agregarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.generarPasswordsStandarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.FrameEstadoFarmacia = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.cboSucursales = new SC_ControlsCS.scComboBoxExt();
            this.FrameGrupos.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameUsuarios.SuspendLayout();
            this.mnUsuarios.SuspendLayout();
            this.FrameEstadoFarmacia.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameGrupos
            // 
            this.FrameGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameGrupos.Controls.Add(this.twGrupos);
            this.FrameGrupos.Location = new System.Drawing.Point(7, 63);
            this.FrameGrupos.Name = "FrameGrupos";
            this.FrameGrupos.Size = new System.Drawing.Size(390, 454);
            this.FrameGrupos.TabIndex = 1;
            this.FrameGrupos.TabStop = false;
            this.FrameGrupos.Text = "Grupos";
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
            this.twGrupos.Location = new System.Drawing.Point(6, 19);
            this.twGrupos.Name = "twGrupos";
            this.twGrupos.PathSeparator = "|";
            this.twGrupos.SelectedImageIndex = 0;
            this.twGrupos.ShowNodeToolTips = true;
            this.twGrupos.Size = new System.Drawing.Size(378, 429);
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
            this.agregarToolStripMenuItem,
            this.toolStripSeparator1,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator2,
            this.modificarNombreToolStripMenuItem,
            this.toolStripSeparator3,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(146, 122);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.agregarToolStripMenuItem.Text = "Agregar";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // modificarNombreToolStripMenuItem
            // 
            this.modificarNombreToolStripMenuItem.Name = "modificarNombreToolStripMenuItem";
            this.modificarNombreToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.modificarNombreToolStripMenuItem.Text = "Modificar nombre";
            this.modificarNombreToolStripMenuItem.Click += new System.EventHandler(this.modificarNombreToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(142, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar grupos";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(142, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // FrameUsuarios
            // 
            this.FrameUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUsuarios.Controls.Add(this.lwUsuarios);
            this.FrameUsuarios.Location = new System.Drawing.Point(399, 63);
            this.FrameUsuarios.Name = "FrameUsuarios";
            this.FrameUsuarios.Size = new System.Drawing.Size(390, 454);
            this.FrameUsuarios.TabIndex = 2;
            this.FrameUsuarios.TabStop = false;
            this.FrameUsuarios.Text = "Usuarios";
            // 
            // lwUsuarios
            // 
            this.lwUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwUsuarios.ContextMenuStrip = this.mnUsuarios;
            this.lwUsuarios.FullRowSelect = true;
            this.lwUsuarios.Location = new System.Drawing.Point(6, 19);
            this.lwUsuarios.Name = "lwUsuarios";
            this.lwUsuarios.ShowItemToolTips = true;
            this.lwUsuarios.Size = new System.Drawing.Size(378, 429);
            this.lwUsuarios.SmallImageList = this.imgGruposUsuarios;
            this.lwUsuarios.TabIndex = 0;
            this.lwUsuarios.UseCompatibleStateImageBehavior = false;
            this.lwUsuarios.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwUsuarios_ItemDrag);
            this.lwUsuarios.DragEnter += new System.Windows.Forms.DragEventHandler(this.lwUsuarios_DragEnter);
            // 
            // mnUsuarios
            // 
            this.mnUsuarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.agregarToolStripMenuItem1,
            this.toolStripSeparator7,
            this.eliminarToolStripMenuItem1,
            this.toolStripSeparator8,
            this.actualizarToolStripMenuItem1,
            this.toolStripSeparator9,
            this.generarPasswordsStandarToolStripMenuItem});
            this.mnUsuarios.Name = "mnGrupos";
            this.mnUsuarios.ShowImageMargin = false;
            this.mnUsuarios.Size = new System.Drawing.Size(199, 116);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(195, 6);
            // 
            // agregarToolStripMenuItem1
            // 
            this.agregarToolStripMenuItem1.Name = "agregarToolStripMenuItem1";
            this.agregarToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.agregarToolStripMenuItem1.Text = "Agregar";
            this.agregarToolStripMenuItem1.Click += new System.EventHandler(this.agregarToolStripMenuItem1_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(195, 6);
            // 
            // eliminarToolStripMenuItem1
            // 
            this.eliminarToolStripMenuItem1.Name = "eliminarToolStripMenuItem1";
            this.eliminarToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.eliminarToolStripMenuItem1.Text = "Eliminar / Actualizar";
            this.eliminarToolStripMenuItem1.Click += new System.EventHandler(this.eliminarToolStripMenuItem1_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(195, 6);
            // 
            // actualizarToolStripMenuItem1
            // 
            this.actualizarToolStripMenuItem1.Name = "actualizarToolStripMenuItem1";
            this.actualizarToolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.actualizarToolStripMenuItem1.Text = "Actualizar lista de usuarios";
            this.actualizarToolStripMenuItem1.Click += new System.EventHandler(this.actualizarToolStripMenuItem1_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(195, 6);
            // 
            // generarPasswordsStandarToolStripMenuItem
            // 
            this.generarPasswordsStandarToolStripMenuItem.Name = "generarPasswordsStandarToolStripMenuItem";
            this.generarPasswordsStandarToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.generarPasswordsStandarToolStripMenuItem.Text = "Generar passwords Standard";
            this.generarPasswordsStandarToolStripMenuItem.Click += new System.EventHandler(this.generarPasswordsStandarToolStripMenuItem_Click);
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
            // FrameEstadoFarmacia
            // 
            this.FrameEstadoFarmacia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEstadoFarmacia.Controls.Add(this.lblEstado);
            this.FrameEstadoFarmacia.Controls.Add(this.cboEstados);
            this.FrameEstadoFarmacia.Controls.Add(this.lblFarmacia);
            this.FrameEstadoFarmacia.Controls.Add(this.cboSucursales);
            this.FrameEstadoFarmacia.Location = new System.Drawing.Point(7, 6);
            this.FrameEstadoFarmacia.Name = "FrameEstadoFarmacia";
            this.FrameEstadoFarmacia.Size = new System.Drawing.Size(782, 54);
            this.FrameEstadoFarmacia.TabIndex = 0;
            this.FrameEstadoFarmacia.TabStop = false;
            this.FrameEstadoFarmacia.Text = "Datos de Estado y Farmacia";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(7, 21);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(57, 17);
            this.lblEstado.TabIndex = 19;
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
            this.cboEstados.Location = new System.Drawing.Point(66, 20);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(318, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFarmacia.Location = new System.Drawing.Point(399, 21);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(57, 17);
            this.lblFarmacia.TabIndex = 18;
            this.lblFarmacia.Text = "Farmacia :";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSucursales
            // 
            this.cboSucursales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSucursales.BackColorEnabled = System.Drawing.Color.White;
            this.cboSucursales.Data = "";
            this.cboSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSucursales.Filtro = " 1 = 1";
            this.cboSucursales.ListaItemsBusqueda = 20;
            this.cboSucursales.Location = new System.Drawing.Point(458, 20);
            this.cboSucursales.MostrarToolTip = false;
            this.cboSucursales.Name = "cboSucursales";
            this.cboSucursales.Size = new System.Drawing.Size(318, 21);
            this.cboSucursales.TabIndex = 1;
            this.cboSucursales.SelectedIndexChanged += new System.EventHandler(this.cboSucursales_SelectedIndexChanged);
            // 
            // FrmGruposUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 522);
            this.Controls.Add(this.FrameEstadoFarmacia);
            this.Controls.Add(this.FrameUsuarios);
            this.Controls.Add(this.FrameGrupos);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmGruposUsuarios";
            this.Text = "Grupos y usuarios";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.FrameGrupos.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameUsuarios.ResumeLayout(false);
            this.mnUsuarios.ResumeLayout(false);
            this.FrameEstadoFarmacia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameGrupos;
        private System.Windows.Forms.GroupBox FrameUsuarios;
        private System.Windows.Forms.TreeView twGrupos;
        private System.Windows.Forms.ListView lwUsuarios;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ContextMenuStrip mnUsuarios;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarNombreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem1;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.GroupBox FrameEstadoFarmacia;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scComboBoxExt cboSucursales;
        private System.Windows.Forms.ToolStripMenuItem generarPasswordsStandarToolStripMenuItem;

    }
}