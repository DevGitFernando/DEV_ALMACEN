namespace DllCompras.PerfilesPersonal
{
    partial class FrmPerfilesPersonalClavesSSA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPerfilesPersonalClavesSSA));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscar = new SC_ControlsCS.scTextBoxExt();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lwSales = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnClientes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.imgSales = new System.Windows.Forms.ImageList(this.components);
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboGrupos = new SC_ControlsCS.scComboBoxExt();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twClientes = new System.Windows.Forms.TreeView();
            this.mnEstados = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarClientesDelEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cargarClavesAsignadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buscarClavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.lblClave = new System.Windows.Forms.Label();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.cboFarmacia = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.agregarPersonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnEstados.SuspendLayout();
            this.grpDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.lwSales);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.cboGrupos);
            this.groupBox2.Location = new System.Drawing.Point(523, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(510, 412);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sales por Grupo Terapeutico";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Buscar :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBuscar
            // 
            this.txtBuscar.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBuscar.Decimales = 2;
            this.txtBuscar.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtBuscar.ForeColor = System.Drawing.Color.Black;
            this.txtBuscar.Location = new System.Drawing.Point(63, 50);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PermitirApostrofo = false;
            this.txtBuscar.PermitirNegativos = false;
            this.txtBuscar.Size = new System.Drawing.Size(395, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.VisibleChanged += new System.EventHandler(this.txtBuscar_VisibleChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(464, 49);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(38, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "...";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lwSales
            // 
            this.lwSales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwSales.ContextMenuStrip = this.mnClientes;
            this.lwSales.FullRowSelect = true;
            this.lwSales.Location = new System.Drawing.Point(6, 78);
            this.lwSales.Name = "lwSales";
            this.lwSales.Size = new System.Drawing.Size(496, 328);
            this.lwSales.SmallImageList = this.imgSales;
            this.lwSales.TabIndex = 3;
            this.lwSales.UseCompatibleStateImageBehavior = false;
            this.lwSales.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwSales_ItemDrag);
            this.lwSales.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lwSales_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 470;
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
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(11, 23);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 17);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Grupo :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGrupos
            // 
            this.cboGrupos.BackColorEnabled = System.Drawing.Color.White;
            this.cboGrupos.Data = "";
            this.cboGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupos.Filtro = " 1 = 1";
            this.cboGrupos.ListaItemsBusqueda = 20;
            this.cboGrupos.Location = new System.Drawing.Point(63, 21);
            this.cboGrupos.MostrarToolTip = false;
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Size = new System.Drawing.Size(439, 21);
            this.cboGrupos.TabIndex = 0;
            this.cboGrupos.SelectedValueChanged += new System.EventHandler(this.cboEstados_SelectedValueChanged);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Grupos.ICO");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(2, "Pill 2.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twClientes);
            this.groupBox1.Location = new System.Drawing.Point(7, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 412);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personal";
            // 
            // twClientes
            // 
            this.twClientes.AllowDrop = true;
            this.twClientes.ContextMenuStrip = this.mnEstados;
            this.twClientes.FullRowSelect = true;
            this.twClientes.ImageIndex = 1;
            this.twClientes.ImageList = this.imgGruposUsuarios;
            this.twClientes.Location = new System.Drawing.Point(8, 15);
            this.twClientes.Name = "twClientes";
            this.twClientes.PathSeparator = "|";
            this.twClientes.SelectedImageIndex = 0;
            this.twClientes.ShowNodeToolTips = true;
            this.twClientes.Size = new System.Drawing.Size(496, 391);
            this.twClientes.TabIndex = 0;
            this.twClientes.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twEstados_ItemDrag);
            this.twClientes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twEstados_AfterSelect);
            this.twClientes.DragDrop += new System.Windows.Forms.DragEventHandler(this.twEstados_DragDrop);
            this.twClientes.DragEnter += new System.Windows.Forms.DragEventHandler(this.twEstados_DragEnter);
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
            this.toolStripSeparator5,
            this.cargarClavesAsignadasToolStripMenuItem,
            this.toolStripSeparator4,
            this.buscarClavesToolStripMenuItem,
            this.toolStripSeparator6,
            this.agregarPersonalToolStripMenuItem});
            this.mnEstados.Name = "mnGrupos";
            this.mnEstados.ShowImageMargin = false;
            this.mnEstados.Size = new System.Drawing.Size(215, 194);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar Clave SSA de Personal ";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Clave SSA";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // cargarClavesAsignadasToolStripMenuItem
            // 
            this.cargarClavesAsignadasToolStripMenuItem.Name = "cargarClavesAsignadasToolStripMenuItem";
            this.cargarClavesAsignadasToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.cargarClavesAsignadasToolStripMenuItem.Text = "Cargar Claves Asignadas";
            this.cargarClavesAsignadasToolStripMenuItem.Click += new System.EventHandler(this.cargarClavesAsignadasToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
            // 
            // buscarClavesToolStripMenuItem
            // 
            this.buscarClavesToolStripMenuItem.Name = "buscarClavesToolStripMenuItem";
            this.buscarClavesToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.buscarClavesToolStripMenuItem.Text = "Buscar Claves";
            this.buscarClavesToolStripMenuItem.Click += new System.EventHandler(this.buscarClavesToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(208, 6);
            // 
            // lblClave
            // 
            this.lblClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClave.Location = new System.Drawing.Point(7, 480);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(1026, 40);
            this.lblClave.TabIndex = 8;
            this.lblClave.Text = "label1";
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.label3);
            this.grpDatos.Controls.Add(this.cboEdo);
            this.grpDatos.Controls.Add(this.cboFarmacia);
            this.grpDatos.Controls.Add(this.label4);
            this.grpDatos.Location = new System.Drawing.Point(7, 4);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(1026, 55);
            this.grpDatos.TabIndex = 9;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos de Origen";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(113, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(171, 22);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(330, 21);
            this.cboEdo.TabIndex = 30;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEdo_SelectedIndexChanged);
            // 
            // cboFarmacia
            // 
            this.cboFarmacia.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacia.Data = "";
            this.cboFarmacia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacia.Filtro = " 1 = 1";
            this.cboFarmacia.FormattingEnabled = true;
            this.cboFarmacia.ListaItemsBusqueda = 20;
            this.cboFarmacia.Location = new System.Drawing.Point(581, 21);
            this.cboFarmacia.MostrarToolTip = false;
            this.cboFarmacia.Name = "cboFarmacia";
            this.cboFarmacia.Size = new System.Drawing.Size(330, 21);
            this.cboFarmacia.TabIndex = 31;
            this.cboFarmacia.SelectedIndexChanged += new System.EventHandler(this.cboFarmacia_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(516, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // agregarPersonalToolStripMenuItem
            // 
            this.agregarPersonalToolStripMenuItem.Name = "agregarPersonalToolStripMenuItem";
            this.agregarPersonalToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.agregarPersonalToolStripMenuItem.Text = "Agregar Personal";
            this.agregarPersonalToolStripMenuItem.Click += new System.EventHandler(this.agregarPersonalToolStripMenuItem_Click);
            // 
            // FrmPerfilesPersonalClavesSSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 527);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmPerfilesPersonalClavesSSA";
            this.Text = "Configuración de Perfiles de Claves SSA por Personal ";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosClientes_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnEstados.ResumeLayout(false);
            this.grpDatos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView twClientes;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        internal System.Windows.Forms.ImageList imgSales;
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
        private SC_ControlsCS.scComboBoxExt cboGrupos;
        private System.Windows.Forms.ListView lwSales;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cargarClavesAsignadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarClavesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private SC_ControlsCS.scComboBoxExt cboFarmacia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem agregarPersonalToolStripMenuItem;

    }
}