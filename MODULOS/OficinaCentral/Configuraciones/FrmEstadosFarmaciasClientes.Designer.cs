namespace OficinaCentral.Configuraciones
{
    partial class FrmEstadosFarmaciasClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadosFarmaciasClientes));
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSubCliente = new System.Windows.Forms.Label();
            this.grdSubClientes = new FarPoint.Win.Spread.FpSpread();
            this.grdSubClientes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.twFarmaciasClientes = new System.Windows.Forms.TreeView();
            this.tmGrid = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.mnClientes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mnEstados.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Controls.Add(this.lwClientes);
            this.groupBox2.Location = new System.Drawing.Point(513, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(650, 628);
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
            this.cboEstados.Size = new System.Drawing.Size(581, 21);
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
            this.lwClientes.Size = new System.Drawing.Size(638, 574);
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.twEstados);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 628);
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
            this.twEstados.Location = new System.Drawing.Point(9, 19);
            this.twEstados.Name = "twEstados";
            this.twEstados.PathSeparator = "|";
            this.twEstados.SelectedImageIndex = 0;
            this.twEstados.ShowNodeToolTips = true;
            this.twEstados.Size = new System.Drawing.Size(482, 601);
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
            this.mnEstados.Size = new System.Drawing.Size(205, 94);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(201, 6);
            // 
            // eliminarClientesDelEstadoToolStripMenuItem
            // 
            this.eliminarClientesDelEstadoToolStripMenuItem.Name = "eliminarClientesDelEstadoToolStripMenuItem";
            this.eliminarClientesDelEstadoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.eliminarClientesDelEstadoToolStripMenuItem.Text = "Eliminar Clientes de Farmacia";
            this.eliminarClientesDelEstadoToolStripMenuItem.Click += new System.EventHandler(this.eliminarClientesDelEstadoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar Cliente";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(201, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar lista";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(201, 6);
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
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(4, 6);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1175, 669);
            this.tabControl.TabIndex = 8;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1167, 643);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clientes por Farmacia";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1167, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sub-Cliente por Farmacia";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lblSubCliente);
            this.groupBox4.Controls.Add(this.grdSubClientes);
            this.groupBox4.Controls.Add(this.toolStripBarraMenu);
            this.groupBox4.Location = new System.Drawing.Point(513, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(650, 628);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sub-Clientes";
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(507, 16);
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(137, 17);
            this.lblSubCliente.TabIndex = 8;
            this.lblSubCliente.Text = "label1";
            this.lblSubCliente.Visible = false;
            // 
            // grdSubClientes
            // 
            this.grdSubClientes.AccessibleDescription = "grdSubClientes, Sheet1, Row 0, Column 0, ";
            this.grdSubClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSubClientes.BackColor = System.Drawing.Color.Transparent;
            this.grdSubClientes.Location = new System.Drawing.Point(6, 48);
            this.grdSubClientes.Name = "grdSubClientes";
            this.grdSubClientes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdSubClientes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdSubClientes_Sheet1});
            this.grdSubClientes.Size = new System.Drawing.Size(638, 573);
            this.grdSubClientes.TabIndex = 7;
            // 
            // grdSubClientes_Sheet1
            // 
            this.grdSubClientes_Sheet1.Reset();
            this.grdSubClientes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdSubClientes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdSubClientes_Sheet1.ColumnCount = 6;
            this.grdSubClientes_Sheet1.RowCount = 5;
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Cliente";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Clave";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Nombre";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Asignar";
            this.grdSubClientes_Sheet1.ColumnHeader.Rows.Get(0).Height = 29F;
            this.grdSubClientes_Sheet1.Columns.Get(0).CellType = textCellType11;
            this.grdSubClientes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdSubClientes_Sheet1.Columns.Get(0).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.grdSubClientes_Sheet1.Columns.Get(0).Visible = false;
            this.grdSubClientes_Sheet1.Columns.Get(1).CellType = textCellType12;
            this.grdSubClientes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdSubClientes_Sheet1.Columns.Get(1).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.grdSubClientes_Sheet1.Columns.Get(1).Visible = false;
            this.grdSubClientes_Sheet1.Columns.Get(2).CellType = textCellType13;
            this.grdSubClientes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(2).Label = "Cliente";
            this.grdSubClientes_Sheet1.Columns.Get(2).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
            this.grdSubClientes_Sheet1.Columns.Get(2).Visible = false;
            this.grdSubClientes_Sheet1.Columns.Get(3).CellType = textCellType14;
            this.grdSubClientes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(3).Label = "Clave";
            this.grdSubClientes_Sheet1.Columns.Get(3).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(3).Width = 70F;
            this.grdSubClientes_Sheet1.Columns.Get(4).CellType = textCellType15;
            this.grdSubClientes_Sheet1.Columns.Get(4).Label = "Nombre";
            this.grdSubClientes_Sheet1.Columns.Get(4).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(4).Width = 350F;
            this.grdSubClientes_Sheet1.Columns.Get(5).CellType = checkBoxCellType3;
            this.grdSubClientes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(5).Label = "Asignar";
            this.grdSubClientes_Sheet1.Columns.Get(5).Width = 70F;
            this.grdSubClientes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdSubClientes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar,
            this.toolStripSeparator5});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(3, 16);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(644, 25);
            this.toolStripBarraMenu.TabIndex = 6;
            this.toolStripBarraMenu.Text = "toolStrip1";
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.twFarmaciasClientes);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 628);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Farmacias";
            // 
            // twFarmaciasClientes
            // 
            this.twFarmaciasClientes.AllowDrop = true;
            this.twFarmaciasClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twFarmaciasClientes.ContextMenuStrip = this.mnEstados;
            this.twFarmaciasClientes.FullRowSelect = true;
            this.twFarmaciasClientes.ImageIndex = 1;
            this.twFarmaciasClientes.ImageList = this.imgEstados;
            this.twFarmaciasClientes.Location = new System.Drawing.Point(9, 19);
            this.twFarmaciasClientes.Name = "twFarmaciasClientes";
            this.twFarmaciasClientes.PathSeparator = "|";
            this.twFarmaciasClientes.SelectedImageIndex = 0;
            this.twFarmaciasClientes.ShowNodeToolTips = true;
            this.twFarmaciasClientes.Size = new System.Drawing.Size(482, 601);
            this.twFarmaciasClientes.TabIndex = 0;
            this.twFarmaciasClientes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twFarmaciasClientes_AfterSelect);
            // 
            // tmGrid
            // 
            this.tmGrid.Tick += new System.EventHandler(this.tmGrid_Tick);
            // 
            // FrmEstadosFarmaciasClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 681);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmEstadosFarmaciasClientes";
            this.Text = "Configuración de Clientes por Farmacia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosClientes_Load);
            this.groupBox2.ResumeLayout(false);
            this.mnClientes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.mnEstados.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView twFarmaciasClientes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private FarPoint.Win.Spread.FpSpread grdSubClientes;
        private FarPoint.Win.Spread.SheetView grdSubClientes_Sheet1;
        private System.Windows.Forms.Label lblSubCliente;
        private System.Windows.Forms.Timer tmGrid;

    }
}