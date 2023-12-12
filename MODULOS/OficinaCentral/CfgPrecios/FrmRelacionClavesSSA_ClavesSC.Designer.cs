namespace OficinaCentral.CfgPrecios
{
    partial class FrmRelacionClavesSSA_ClavesSC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelacionClavesSSA_ClavesSC));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.label8 = new System.Windows.Forms.Label();
            this.twClientes = new System.Windows.Forms.TreeView();
            this.mnEstados = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarClientesDelEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cambiarMultiploToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo_Clave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtClaveSSA = new SC_ControlsCS.scTextBoxExt();
            this.lblClaveSSA = new System.Windows.Forms.Label();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.cboSubCliente = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCliente = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox2.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnEstados.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.FrameProducto.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Controls.Add(this.btnAgregar);
            this.groupBox2.Controls.Add(this.lwSales);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.cboGrupos);
            this.groupBox2.Location = new System.Drawing.Point(594, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(582, 600);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Claves por Grupo Terapeutico";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(10, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(561, 17);
            this.label7.TabIndex = 29;
            this.label7.Text = "Claves auxiliares";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Buscar :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBuscar.Decimales = 2;
            this.txtBuscar.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtBuscar.ForeColor = System.Drawing.Color.Black;
            this.txtBuscar.Location = new System.Drawing.Point(94, 43);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PermitirApostrofo = false;
            this.txtBuscar.PermitirNegativos = false;
            this.txtBuscar.Size = new System.Drawing.Size(433, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.Location = new System.Drawing.Point(533, 41);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(38, 23);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "...";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lwSales
            // 
            this.lwSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwSales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwSales.ContextMenuStrip = this.mnClientes;
            this.lwSales.FullRowSelect = true;
            this.lwSales.Location = new System.Drawing.Point(10, 93);
            this.lwSales.Name = "lwSales";
            this.lwSales.Size = new System.Drawing.Size(561, 498);
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
            this.lblEstado.Location = new System.Drawing.Point(10, 22);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(77, 13);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Grupo :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGrupos
            // 
            this.cboGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGrupos.BackColorEnabled = System.Drawing.Color.White;
            this.cboGrupos.Data = "";
            this.cboGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupos.Filtro = " 1 = 1";
            this.cboGrupos.ListaItemsBusqueda = 20;
            this.cboGrupos.Location = new System.Drawing.Point(94, 18);
            this.cboGrupos.MostrarToolTip = false;
            this.cboGrupos.Name = "cboGrupos";
            this.cboGrupos.Size = new System.Drawing.Size(477, 21);
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
            this.imgGruposUsuarios.Images.SetKeyName(3, "Pill 2 Cancel.ico");
            this.imgGruposUsuarios.Images.SetKeyName(4, "Pill 2 Cancelado.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.twClientes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(582, 378);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Claves asignadas";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(495, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Clave SSA licitada / Claves auxiliares";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // twClientes
            // 
            this.twClientes.AllowDrop = true;
            this.twClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twClientes.ContextMenuStrip = this.mnEstados;
            this.twClientes.FullRowSelect = true;
            this.twClientes.ImageIndex = 1;
            this.twClientes.ImageList = this.imgGruposUsuarios;
            this.twClientes.Location = new System.Drawing.Point(10, 35);
            this.twClientes.Name = "twClientes";
            this.twClientes.PathSeparator = "|";
            this.twClientes.SelectedImageIndex = 0;
            this.twClientes.ShowNodeToolTips = true;
            this.twClientes.Size = new System.Drawing.Size(561, 334);
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
            this.cambiarMultiploToolStripMenuItem,
            this.toolStripSeparator5,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnEstados.Name = "mnGrupos";
            this.mnEstados.ShowImageMargin = false;
            this.mnEstados.Size = new System.Drawing.Size(196, 122);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar Lista de Claves SSA";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Clave SSA";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
            // 
            // cambiarMultiploToolStripMenuItem
            // 
            this.cambiarMultiploToolStripMenuItem.Name = "cambiarMultiploToolStripMenuItem";
            this.cambiarMultiploToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cambiarMultiploToolStripMenuItem.Text = "Cambiar Multiplo";
            this.cambiarMultiploToolStripMenuItem.Click += new System.EventHandler(this.cambiarMultiploToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(192, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(192, 6);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(206, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Clave Interna :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // lblClave
            // 
            this.lblClave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClave.Location = new System.Drawing.Point(7, 632);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(1169, 40);
            this.lblClave.TabIndex = 8;
            this.lblClave.Text = "label1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.toolStrip1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblDescripcion);
            this.groupBox3.Controls.Add(this.txtClaveSSA);
            this.groupBox3.Controls.Add(this.lblClaveSSA);
            this.groupBox3.Location = new System.Drawing.Point(7, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(582, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Clave SSA";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo_Clave,
            this.toolStripSeparator6,
            this.toolStripButton2,
            this.toolStripSeparator10});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(576, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevo_Clave
            // 
            this.btnNuevo_Clave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo_Clave.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo_Clave.Image")));
            this.btnNuevo_Clave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo_Clave.Name = "btnNuevo_Clave";
            this.btnNuevo_Clave.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo_Clave.Text = "&Nueva Clave";
            this.btnNuevo_Clave.Click += new System.EventHandler(this.btnNuevo_Clave_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Ejecutar";
            this.toolStripButton2.ToolTipText = "Ejecutar";
            this.toolStripButton2.Visible = false;
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator10.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Clave SSA : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(10, 68);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(561, 43);
            this.lblDescripcion.TabIndex = 4;
            // 
            // txtClaveSSA
            // 
            this.txtClaveSSA.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveSSA.Decimales = 2;
            this.txtClaveSSA.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtClaveSSA.ForeColor = System.Drawing.Color.Black;
            this.txtClaveSSA.Location = new System.Drawing.Point(94, 43);
            this.txtClaveSSA.MaxLength = 20;
            this.txtClaveSSA.Name = "txtClaveSSA";
            this.txtClaveSSA.PermitirApostrofo = false;
            this.txtClaveSSA.PermitirNegativos = false;
            this.txtClaveSSA.Size = new System.Drawing.Size(174, 20);
            this.txtClaveSSA.TabIndex = 0;
            this.txtClaveSSA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaveSSA.TextChanged += new System.EventHandler(this.txtClaveSSA_TextChanged);
            this.txtClaveSSA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClaveSSA_KeyDown);
            this.txtClaveSSA.Validating += new System.ComponentModel.CancelEventHandler(this.txtClaveSSA_Validating);
            // 
            // lblClaveSSA
            // 
            this.lblClaveSSA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClaveSSA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClaveSSA.Location = new System.Drawing.Point(346, 43);
            this.lblClaveSSA.Name = "lblClaveSSA";
            this.lblClaveSSA.Size = new System.Drawing.Size(225, 20);
            this.lblClaveSSA.TabIndex = 6;
            this.lblClaveSSA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameProducto
            // 
            this.FrameProducto.Controls.Add(this.cboSubCliente);
            this.FrameProducto.Controls.Add(this.label3);
            this.FrameProducto.Controls.Add(this.cboCliente);
            this.FrameProducto.Controls.Add(this.label5);
            this.FrameProducto.Controls.Add(this.cboEstados);
            this.FrameProducto.Controls.Add(this.label4);
            this.FrameProducto.Location = new System.Drawing.Point(7, 26);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Size = new System.Drawing.Size(582, 98);
            this.FrameProducto.TabIndex = 1;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Estados";
            // 
            // cboSubCliente
            // 
            this.cboSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubCliente.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubCliente.Data = "";
            this.cboSubCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubCliente.Filtro = " 1 = 1";
            this.cboSubCliente.FormattingEnabled = true;
            this.cboSubCliente.ListaItemsBusqueda = 20;
            this.cboSubCliente.Location = new System.Drawing.Point(94, 69);
            this.cboSubCliente.MostrarToolTip = false;
            this.cboSubCliente.Name = "cboSubCliente";
            this.cboSubCliente.Size = new System.Drawing.Size(477, 21);
            this.cboSubCliente.TabIndex = 2;
            this.cboSubCliente.SelectedIndexChanged += new System.EventHandler(this.cboSubCliente_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sub-Cliente : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.BackColorEnabled = System.Drawing.Color.White;
            this.cboCliente.Data = "";
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.Filtro = " 1 = 1";
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.ListaItemsBusqueda = 20;
            this.cboCliente.Location = new System.Drawing.Point(94, 43);
            this.cboCliente.MostrarToolTip = false;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(477, 21);
            this.cboCliente.TabIndex = 1;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Cliente : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(94, 18);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(477, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator9,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            this.btnNuevo.Text = "&Nuevo";
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
            this.btnEjecutar.Enabled = false;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Visible = false;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator9.Visible = false;
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
            // FrmRelacionClavesSSA_ClavesSC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameProducto);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmRelacionClavesSSA_ClavesSC";
            this.Text = "Relación de Claves SSA";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosClientes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRelacionClavesSSA_ClavesSC_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnEstados.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.FrameProducto.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtClaveSSA;
        private System.Windows.Forms.Label lblClaveSSA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameProducto;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtBuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ToolStripMenuItem cambiarMultiploToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private SC_ControlsCS.scComboBoxExt cboSubCliente;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo_Clave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}