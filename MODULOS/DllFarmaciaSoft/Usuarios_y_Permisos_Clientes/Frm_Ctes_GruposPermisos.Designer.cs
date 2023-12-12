namespace DllFarmaciaSoft.Usuarios_y_Permisos_Clientes
{
    partial class Frm_Ctes_GruposPermisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Ctes_GruposPermisos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twGruposPermisos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarPermisosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboArboles = new SC_ControlsCS.scComboBoxExt();
            this.twNavegador = new System.Windows.Forms.TreeView();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboSucursales = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twGruposPermisos);
            this.groupBox1.Location = new System.Drawing.Point(7, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 425);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Permisos grupos";
            // 
            // twGruposPermisos
            // 
            this.twGruposPermisos.AllowDrop = true;
            this.twGruposPermisos.ContextMenuStrip = this.mnGrupos;
            this.twGruposPermisos.ImageIndex = 0;
            this.twGruposPermisos.ImageList = this.imgGruposUsuarios;
            this.twGruposPermisos.Location = new System.Drawing.Point(6, 19);
            this.twGruposPermisos.Name = "twGruposPermisos";
            this.twGruposPermisos.PathSeparator = "|";
            this.twGruposPermisos.SelectedImageIndex = 0;
            this.twGruposPermisos.ShowNodeToolTips = true;
            this.twGruposPermisos.Size = new System.Drawing.Size(299, 400);
            this.twGruposPermisos.StateImageList = this.imgGruposUsuarios;
            this.twGruposPermisos.TabIndex = 0;
            this.twGruposPermisos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGruposPermisos_DragDrop);
            this.twGruposPermisos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGruposPermisos_AfterSelect);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboArboles);
            this.groupBox2.Controls.Add(this.twNavegador);
            this.groupBox2.Location = new System.Drawing.Point(324, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 425);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Grupo de opciones";
            // 
            // cboArboles
            // 
            this.cboArboles.BackColorEnabled = System.Drawing.Color.White;
            this.cboArboles.Data = "";
            this.cboArboles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArboles.Filtro = " 1 = 1";
            this.cboArboles.Location = new System.Drawing.Point(6, 36);
            this.cboArboles.MinimumSize = new System.Drawing.Size(50, 0);
            this.cboArboles.MostrarToolTip = false;
            this.cboArboles.Name = "cboArboles";
            this.cboArboles.Size = new System.Drawing.Size(299, 21);
            this.cboArboles.TabIndex = 7;
            this.cboArboles.SelectedIndexChanged += new System.EventHandler(this.cboArboles_SelectedIndexChanged);
            // 
            // twNavegador
            // 
            this.twNavegador.AllowDrop = true;
            this.twNavegador.ImageIndex = 0;
            this.twNavegador.ImageList = this.imgNavegacion;
            this.twNavegador.Location = new System.Drawing.Point(6, 67);
            this.twNavegador.Name = "twNavegador";
            this.twNavegador.PathSeparator = "|";
            this.twNavegador.SelectedImageIndex = 0;
            this.twNavegador.ShowNodeToolTips = true;
            this.twNavegador.Size = new System.Drawing.Size(299, 352);
            this.twNavegador.TabIndex = 1;
            this.twNavegador.DragDrop += new System.Windows.Forms.DragEventHandler(this.twNavegador_DragDrop);
            this.twNavegador.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twNavegador_AfterSelect);
            this.twNavegador.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twNavegador_ItemDrag);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblEstado);
            this.groupBox3.Controls.Add(this.cboEstados);
            this.groupBox3.Controls.Add(this.lblEmpresa);
            this.groupBox3.Controls.Add(this.cboSucursales);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(628, 54);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Estado y Farmacia";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(8, 18);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(48, 17);
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
            this.cboEstados.Location = new System.Drawing.Point(62, 18);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(243, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(320, 19);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(56, 17);
            this.lblEmpresa.TabIndex = 18;
            this.lblEmpresa.Text = "Farmacia :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSucursales
            // 
            this.cboSucursales.BackColorEnabled = System.Drawing.Color.White;
            this.cboSucursales.Data = "";
            this.cboSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSucursales.Filtro = " 1 = 1";
            this.cboSucursales.Location = new System.Drawing.Point(379, 19);
            this.cboSucursales.MostrarToolTip = false;
            this.cboSucursales.Name = "cboSucursales";
            this.cboSucursales.Size = new System.Drawing.Size(243, 21);
            this.cboSucursales.TabIndex = 1;
            this.cboSucursales.SelectedIndexChanged += new System.EventHandler(this.cboSucursales_SelectedIndexChanged);
            // 
            // Frm_Ctes_GruposPermisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 493);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Frm_Ctes_GruposPermisos";
            this.Text = "Permisos de grupo Clientes Administrativos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView twGruposPermisos;
        private System.Windows.Forms.TreeView twNavegador;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboArboles;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        internal System.Windows.Forms.ImageList imgNavegacion;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem actualizarPermisosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblEmpresa;
        private SC_ControlsCS.scComboBoxExt cboSucursales;

    }
}