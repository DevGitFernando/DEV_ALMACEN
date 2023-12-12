namespace DllFarmaciaSoft.Usuarios_y_Permisos
{
    partial class FrmGruposPermisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGruposPermisos));
            this.FrameGrupos = new System.Windows.Forms.GroupBox();
            this.twGruposPermisos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarPermisosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.FrameOpciones = new System.Windows.Forms.GroupBox();
            this.lblModulos = new System.Windows.Forms.Label();
            this.cboArboles = new SC_ControlsCS.scComboBoxExt();
            this.twNavegador = new System.Windows.Forms.TreeView();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.FrameEstadoFarmacia = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.cboSucursales = new SC_ControlsCS.scComboBoxExt();
            this.FrameGrupos.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameOpciones.SuspendLayout();
            this.FrameEstadoFarmacia.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameGrupos
            // 
            this.FrameGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameGrupos.Controls.Add(this.twGruposPermisos);
            this.FrameGrupos.Location = new System.Drawing.Point(6, 63);
            this.FrameGrupos.Name = "FrameGrupos";
            this.FrameGrupos.Size = new System.Drawing.Size(390, 454);
            this.FrameGrupos.TabIndex = 1;
            this.FrameGrupos.TabStop = false;
            this.FrameGrupos.Text = "Permisos grupos";
            // 
            // twGruposPermisos
            // 
            this.twGruposPermisos.AllowDrop = true;
            this.twGruposPermisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twGruposPermisos.ContextMenuStrip = this.mnGrupos;
            this.twGruposPermisos.ImageIndex = 0;
            this.twGruposPermisos.ImageList = this.imgGruposUsuarios;
            this.twGruposPermisos.Location = new System.Drawing.Point(6, 19);
            this.twGruposPermisos.Name = "twGruposPermisos";
            this.twGruposPermisos.PathSeparator = "|";
            this.twGruposPermisos.SelectedImageIndex = 0;
            this.twGruposPermisos.ShowNodeToolTips = true;
            this.twGruposPermisos.Size = new System.Drawing.Size(378, 425);
            this.twGruposPermisos.StateImageList = this.imgGruposUsuarios;
            this.twGruposPermisos.TabIndex = 0;
            this.twGruposPermisos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGruposPermisos_AfterSelect);
            this.twGruposPermisos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGruposPermisos_DragDrop);
            this.twGruposPermisos.DragEnter += new System.Windows.Forms.DragEventHandler(this.twGruposPermisos_DragEnter);
            this.twGruposPermisos.DragOver += new System.Windows.Forms.DragEventHandler(this.twGruposPermisos_DragOver);
            // 
            // mnGrupos
            // 
            this.mnGrupos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator2,
            this.actualizarPermisosToolStripMenuItem,
            this.toolStripSeparator3});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(153, 66);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // actualizarPermisosToolStripMenuItem
            // 
            this.actualizarPermisosToolStripMenuItem.Name = "actualizarPermisosToolStripMenuItem";
            this.actualizarPermisosToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.actualizarPermisosToolStripMenuItem.Text = "Actualizar permisos";
            this.actualizarPermisosToolStripMenuItem.Click += new System.EventHandler(this.actualizarPermisosToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            this.imgGruposUsuarios.Images.SetKeyName(2, "Folder.ico");
            this.imgGruposUsuarios.Images.SetKeyName(3, "Pantalla.ico");
            // 
            // FrameOpciones
            // 
            this.FrameOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameOpciones.Controls.Add(this.lblModulos);
            this.FrameOpciones.Controls.Add(this.cboArboles);
            this.FrameOpciones.Controls.Add(this.twNavegador);
            this.FrameOpciones.Location = new System.Drawing.Point(399, 63);
            this.FrameOpciones.Name = "FrameOpciones";
            this.FrameOpciones.Size = new System.Drawing.Size(390, 454);
            this.FrameOpciones.TabIndex = 2;
            this.FrameOpciones.TabStop = false;
            this.FrameOpciones.Text = "Opciones";
            // 
            // lblModulos
            // 
            this.lblModulos.Location = new System.Drawing.Point(6, 17);
            this.lblModulos.Name = "lblModulos";
            this.lblModulos.Size = new System.Drawing.Size(57, 15);
            this.lblModulos.TabIndex = 8;
            this.lblModulos.Text = "Módulo : ";
            this.lblModulos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboArboles
            // 
            this.cboArboles.BackColorEnabled = System.Drawing.Color.White;
            this.cboArboles.Data = "";
            this.cboArboles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArboles.Filtro = " 1 = 1";
            this.cboArboles.ListaItemsBusqueda = 20;
            this.cboArboles.Location = new System.Drawing.Point(65, 15);
            this.cboArboles.MinimumSize = new System.Drawing.Size(50, 0);
            this.cboArboles.MostrarToolTip = false;
            this.cboArboles.Name = "cboArboles";
            this.cboArboles.Size = new System.Drawing.Size(319, 21);
            this.cboArboles.TabIndex = 0;
            this.cboArboles.SelectedIndexChanged += new System.EventHandler(this.cboArboles_SelectedIndexChanged);
            // 
            // twNavegador
            // 
            this.twNavegador.AllowDrop = true;
            this.twNavegador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twNavegador.ImageIndex = 0;
            this.twNavegador.ImageList = this.imgNavegacion;
            this.twNavegador.Location = new System.Drawing.Point(6, 42);
            this.twNavegador.Name = "twNavegador";
            this.twNavegador.PathSeparator = "|";
            this.twNavegador.SelectedImageIndex = 0;
            this.twNavegador.ShowNodeToolTips = true;
            this.twNavegador.Size = new System.Drawing.Size(378, 402);
            this.twNavegador.TabIndex = 1;
            this.twNavegador.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twNavegador_ItemDrag);
            this.twNavegador.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twNavegador_AfterSelect);
            this.twNavegador.DragDrop += new System.Windows.Forms.DragEventHandler(this.twNavegador_DragDrop);
            this.twNavegador.DragOver += new System.Windows.Forms.DragEventHandler(this.twNavegador_DragOver);
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
            this.cboEstados.Location = new System.Drawing.Point(66, 19);
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
            this.cboSucursales.Location = new System.Drawing.Point(458, 19);
            this.cboSucursales.MostrarToolTip = false;
            this.cboSucursales.Name = "cboSucursales";
            this.cboSucursales.Size = new System.Drawing.Size(318, 21);
            this.cboSucursales.TabIndex = 1;
            this.cboSucursales.SelectedIndexChanged += new System.EventHandler(this.cboSucursales_SelectedIndexChanged);
            // 
            // FrmGruposPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 522);
            this.Controls.Add(this.FrameEstadoFarmacia);
            this.Controls.Add(this.FrameGrupos);
            this.Controls.Add(this.FrameOpciones);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmGruposPermisos";
            this.Text = "Permisos de grupo";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.FrameGrupos.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameOpciones.ResumeLayout(false);
            this.FrameEstadoFarmacia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameGrupos;
        private System.Windows.Forms.GroupBox FrameOpciones;
        private System.Windows.Forms.TreeView twGruposPermisos;
        private System.Windows.Forms.TreeView twNavegador;
        private System.Windows.Forms.Label lblModulos;
        private SC_ControlsCS.scComboBoxExt cboArboles;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        internal System.Windows.Forms.ImageList imgNavegacion;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem actualizarPermisosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameEstadoFarmacia;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scComboBoxExt cboSucursales;

    }
}