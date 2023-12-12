namespace OficinaCentral.FarmaciasConvenioVales
{
    partial class FrmConfigurarFarmaciasProv_Vales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfigurarFarmaciasProv_Vales));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twGrupos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.marcarReembolsoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.desmarcarEsReembolsoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lwUsuarios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.mnProveedores = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnBuscarProv = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.mnProveedores.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twGrupos);
            this.groupBox1.Location = new System.Drawing.Point(7, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 425);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Farmacias";
            // 
            // twGrupos
            // 
            this.twGrupos.AllowDrop = true;
            this.twGrupos.ContextMenuStrip = this.mnGrupos;
            this.twGrupos.FullRowSelect = true;
            this.twGrupos.ImageIndex = 1;
            this.twGrupos.ImageList = this.imgGruposUsuarios;
            this.twGrupos.Location = new System.Drawing.Point(10, 19);
            this.twGrupos.Name = "twGrupos";
            this.twGrupos.PathSeparator = "|";
            this.twGrupos.SelectedImageIndex = 0;
            this.twGrupos.ShowNodeToolTips = true;
            this.twGrupos.Size = new System.Drawing.Size(350, 400);
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
            this.toolStripSeparator4,
            this.marcarReembolsoToolStripMenuItem,
            this.toolStripSeparator1,
            this.desmarcarEsReembolsoToolStripMenuItem});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(182, 116);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
            // 
            // marcarReembolsoToolStripMenuItem
            // 
            this.marcarReembolsoToolStripMenuItem.Name = "marcarReembolsoToolStripMenuItem";
            this.marcarReembolsoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.marcarReembolsoToolStripMenuItem.Text = "Marcar Es Reembolso";
            this.marcarReembolsoToolStripMenuItem.Click += new System.EventHandler(this.marcarReembolsoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // desmarcarEsReembolsoToolStripMenuItem
            // 
            this.desmarcarEsReembolsoToolStripMenuItem.Name = "desmarcarEsReembolsoToolStripMenuItem";
            this.desmarcarEsReembolsoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.desmarcarEsReembolsoToolStripMenuItem.Text = "Desmarcar Es Reembolso";
            this.desmarcarEsReembolsoToolStripMenuItem.Click += new System.EventHandler(this.desmarcarEsReembolsoToolStripMenuItem_Click);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lwUsuarios);
            this.groupBox2.Location = new System.Drawing.Point(387, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 425);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proveedores";
            // 
            // lwUsuarios
            // 
            this.lwUsuarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwUsuarios.ContextMenuStrip = this.mnProveedores;
            this.lwUsuarios.FullRowSelect = true;
            this.lwUsuarios.Location = new System.Drawing.Point(10, 19);
            this.lwUsuarios.Name = "lwUsuarios";
            this.lwUsuarios.ShowItemToolTips = true;
            this.lwUsuarios.Size = new System.Drawing.Size(447, 400);
            this.lwUsuarios.SmallImageList = this.imgGruposUsuarios;
            this.lwUsuarios.TabIndex = 0;
            this.lwUsuarios.UseCompatibleStateImageBehavior = false;
            this.lwUsuarios.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwUsuarios_ItemDrag);
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
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboFarmacias);
            this.groupBox3.Controls.Add(this.lblEstado);
            this.groupBox3.Controls.Add(this.cboEstados);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(847, 54);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Estado y Farmacias";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(388, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(447, 20);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(333, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(67, 22);
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
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(118, 20);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(243, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // mnProveedores
            // 
            this.mnProveedores.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBuscarProv});
            this.mnProveedores.Name = "mnFarmacias";
            this.mnProveedores.Size = new System.Drawing.Size(167, 48);
            // 
            // btnBuscarProv
            // 
            this.btnBuscarProv.Name = "btnBuscarProv";
            this.btnBuscarProv.Size = new System.Drawing.Size(166, 22);
            this.btnBuscarProv.Text = "Buscar Proveedor";
            this.btnBuscarProv.Click += new System.EventHandler(this.btnBuscarProv_Click);
            // 
            // FrmConfigurarFarmaciasProv_Vales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 493);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmConfigurarFarmaciasProv_Vales";
            this.Text = "Configuración de Farmacias Proveedores Vales";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.groupBox1.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.mnProveedores.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.ToolStripMenuItem marcarReembolsoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem desmarcarEsReembolsoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnProveedores;
        private System.Windows.Forms.ToolStripMenuItem btnBuscarProv;

    }
}