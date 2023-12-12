namespace Almacen.Pedidos
{
    partial class FrmActualizarSurtidoPedidosSegmento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmActualizarSurtidoPedidosSegmento));
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.twSegmentos = new System.Windows.Forms.TreeView();
            this.mnGrupos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgGruposUsuarios = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.modificarNombreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.FrameDatosGenerales = new System.Windows.Forms.GroupBox();
            this.lblFolioPedido = new SC_ControlsCS.scLabelExt();
            this.txtFolioSurtido = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStatusSurtimiento = new SC_ControlsCS.scLabelExt();
            this.lblFarmaciaSurtido = new SC_ControlsCS.scLabelExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFechaRegistro = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFechaPedido = new SC_ControlsCS.scLabelExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFarmaciaPedido = new SC_ControlsCS.scLabelExt();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameUsuarios = new System.Windows.Forms.GroupBox();
            this.lwClaves = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameClaves.SuspendLayout();
            this.mnGrupos.SuspendLayout();
            this.FrameDatosGenerales.SuspendLayout();
            this.FrameUsuarios.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameClaves
            // 
            this.FrameClaves.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FrameClaves.Controls.Add(this.twSegmentos);
            this.FrameClaves.Location = new System.Drawing.Point(9, 129);
            this.FrameClaves.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameClaves.Size = new System.Drawing.Size(600, 592);
            this.FrameClaves.TabIndex = 1;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Segmentos";
            // 
            // twSegmentos
            // 
            this.twSegmentos.AllowDrop = true;
            this.twSegmentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twSegmentos.ContextMenuStrip = this.mnGrupos;
            this.twSegmentos.FullRowSelect = true;
            this.twSegmentos.ImageIndex = 1;
            this.twSegmentos.ImageList = this.imgGruposUsuarios;
            this.twSegmentos.Location = new System.Drawing.Point(8, 21);
            this.twSegmentos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.twSegmentos.Name = "twSegmentos";
            this.twSegmentos.PathSeparator = "|";
            this.twSegmentos.SelectedImageIndex = 0;
            this.twSegmentos.ShowNodeToolTips = true;
            this.twSegmentos.Size = new System.Drawing.Size(583, 560);
            this.twSegmentos.StateImageList = this.imgGruposUsuarios;
            this.twSegmentos.TabIndex = 0;
            this.twSegmentos.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.twGrupos_ItemDrag);
            this.twSegmentos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twGrupos_AfterSelect);
            this.twSegmentos.DragDrop += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragDrop);
            this.twSegmentos.DragEnter += new System.Windows.Forms.DragEventHandler(this.twGrupos_DragEnter);
            // 
            // mnGrupos
            // 
            this.mnGrupos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnGrupos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.agregarToolStripMenuItem,
            this.toolStripSeparator3,
            this.toolStripMenuItem1,
            this.toolStripSeparator6,
            this.actualizarToolStripMenuItem,
            this.toolStripSeparator4});
            this.mnGrupos.Name = "mnGrupos";
            this.mnGrupos.ShowImageMargin = false;
            this.mnGrupos.Size = new System.Drawing.Size(170, 100);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.agregarToolStripMenuItem.Text = "Agregar";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 24);
            this.toolStripMenuItem1.Text = "Eliminar";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(166, 6);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.actualizarToolStripMenuItem.Text = "Actualizar grupos";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // imgGruposUsuarios
            // 
            this.imgGruposUsuarios.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGruposUsuarios.ImageStream")));
            this.imgGruposUsuarios.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGruposUsuarios.Images.SetKeyName(0, "Usuario.ico");
            this.imgGruposUsuarios.Images.SetKeyName(1, "Grupos.ICO");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // modificarNombreToolStripMenuItem
            // 
            this.modificarNombreToolStripMenuItem.Name = "modificarNombreToolStripMenuItem";
            this.modificarNombreToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
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
            // FrameDatosGenerales
            // 
            this.FrameDatosGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatosGenerales.Controls.Add(this.lblFolioPedido);
            this.FrameDatosGenerales.Controls.Add(this.txtFolioSurtido);
            this.FrameDatosGenerales.Controls.Add(this.label8);
            this.FrameDatosGenerales.Controls.Add(this.lblStatusSurtimiento);
            this.FrameDatosGenerales.Controls.Add(this.lblFarmaciaSurtido);
            this.FrameDatosGenerales.Controls.Add(this.label6);
            this.FrameDatosGenerales.Controls.Add(this.lblFechaRegistro);
            this.FrameDatosGenerales.Controls.Add(this.label3);
            this.FrameDatosGenerales.Controls.Add(this.label1);
            this.FrameDatosGenerales.Controls.Add(this.lblFechaPedido);
            this.FrameDatosGenerales.Controls.Add(this.label5);
            this.FrameDatosGenerales.Controls.Add(this.lblFarmaciaPedido);
            this.FrameDatosGenerales.Controls.Add(this.label4);
            this.FrameDatosGenerales.Controls.Add(this.label2);
            this.FrameDatosGenerales.Location = new System.Drawing.Point(17, 34);
            this.FrameDatosGenerales.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosGenerales.Name = "FrameDatosGenerales";
            this.FrameDatosGenerales.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameDatosGenerales.Size = new System.Drawing.Size(1311, 92);
            this.FrameDatosGenerales.TabIndex = 0;
            this.FrameDatosGenerales.TabStop = false;
            this.FrameDatosGenerales.Text = "Datos Generales";
            // 
            // lblFolioPedido
            // 
            this.lblFolioPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFolioPedido.Location = new System.Drawing.Point(128, 23);
            this.lblFolioPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFolioPedido.MostrarToolTip = false;
            this.lblFolioPedido.Name = "lblFolioPedido";
            this.lblFolioPedido.Size = new System.Drawing.Size(129, 25);
            this.lblFolioPedido.TabIndex = 53;
            this.lblFolioPedido.Text = "scLabelExt1";
            this.lblFolioPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFolioSurtido
            // 
            this.txtFolioSurtido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioSurtido.Decimales = 2;
            this.txtFolioSurtido.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioSurtido.ForeColor = System.Drawing.Color.Black;
            this.txtFolioSurtido.Location = new System.Drawing.Point(128, 54);
            this.txtFolioSurtido.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtFolioSurtido.MaxLength = 8;
            this.txtFolioSurtido.Name = "txtFolioSurtido";
            this.txtFolioSurtido.PermitirApostrofo = false;
            this.txtFolioSurtido.PermitirNegativos = false;
            this.txtFolioSurtido.Size = new System.Drawing.Size(128, 22);
            this.txtFolioSurtido.TabIndex = 0;
            this.txtFolioSurtido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioSurtido.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioSurtido_Validating);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.Location = new System.Drawing.Point(1037, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 16);
            this.label8.TabIndex = 50;
            this.label8.Text = "Status de Surtimiento";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusSurtimiento
            // 
            this.lblStatusSurtimiento.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStatusSurtimiento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusSurtimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusSurtimiento.Location = new System.Drawing.Point(1037, 49);
            this.lblStatusSurtimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusSurtimiento.MostrarToolTip = false;
            this.lblStatusSurtimiento.Name = "lblStatusSurtimiento";
            this.lblStatusSurtimiento.Size = new System.Drawing.Size(256, 25);
            this.lblStatusSurtimiento.TabIndex = 49;
            this.lblStatusSurtimiento.Text = "scLabelExt2";
            this.lblStatusSurtimiento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFarmaciaSurtido
            // 
            this.lblFarmaciaSurtido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFarmaciaSurtido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmaciaSurtido.Location = new System.Drawing.Point(396, 54);
            this.lblFarmaciaSurtido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmaciaSurtido.MostrarToolTip = false;
            this.lblFarmaciaSurtido.Name = "lblFarmaciaSurtido";
            this.lblFarmaciaSurtido.Size = new System.Drawing.Size(309, 25);
            this.lblFarmaciaSurtido.TabIndex = 47;
            this.lblFarmaciaSurtido.Text = "scLabelExt1";
            this.lblFarmaciaSurtido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(265, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 20);
            this.label6.TabIndex = 48;
            this.label6.Text = "Farmacia Pedido :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFechaRegistro
            // 
            this.lblFechaRegistro.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFechaRegistro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaRegistro.Location = new System.Drawing.Point(899, 52);
            this.lblFechaRegistro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaRegistro.MostrarToolTip = false;
            this.lblFechaRegistro.Name = "lblFechaRegistro";
            this.lblFechaRegistro.Size = new System.Drawing.Size(131, 25);
            this.lblFechaRegistro.TabIndex = 46;
            this.lblFechaRegistro.Text = "scLabelExt2";
            this.lblFechaRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.Location = new System.Drawing.Point(736, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Fecha de Pedido :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 15);
            this.label1.TabIndex = 43;
            this.label1.Text = "Folio Pedido :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFechaPedido
            // 
            this.lblFechaPedido.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFechaPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaPedido.Location = new System.Drawing.Point(899, 21);
            this.lblFechaPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaPedido.MostrarToolTip = false;
            this.lblFechaPedido.Name = "lblFechaPedido";
            this.lblFechaPedido.Size = new System.Drawing.Size(131, 25);
            this.lblFechaPedido.TabIndex = 39;
            this.lblFechaPedido.Text = "scLabelExt2";
            this.lblFechaPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.Location = new System.Drawing.Point(736, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "Fecha de Registro :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmaciaPedido
            // 
            this.lblFarmaciaPedido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFarmaciaPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmaciaPedido.Location = new System.Drawing.Point(396, 23);
            this.lblFarmaciaPedido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmaciaPedido.MostrarToolTip = false;
            this.lblFarmaciaPedido.Name = "lblFarmaciaPedido";
            this.lblFarmaciaPedido.Size = new System.Drawing.Size(309, 25);
            this.lblFarmaciaPedido.TabIndex = 1;
            this.lblFarmaciaPedido.Text = "scLabelExt1";
            this.lblFarmaciaPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Folio Surtido :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(265, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "Farmacia Surtido :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameUsuarios
            // 
            this.FrameUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameUsuarios.Controls.Add(this.lwClaves);
            this.FrameUsuarios.Location = new System.Drawing.Point(617, 129);
            this.FrameUsuarios.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUsuarios.Name = "FrameUsuarios";
            this.FrameUsuarios.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameUsuarios.Size = new System.Drawing.Size(711, 592);
            this.FrameUsuarios.TabIndex = 2;
            this.FrameUsuarios.TabStop = false;
            this.FrameUsuarios.Text = "Claves";
            // 
            // lwClaves
            // 
            this.lwClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lwClaves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwClaves.FullRowSelect = true;
            this.lwClaves.HideSelection = false;
            this.lwClaves.Location = new System.Drawing.Point(8, 21);
            this.lwClaves.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lwClaves.Name = "lwClaves";
            this.lwClaves.ShowItemToolTips = true;
            this.lwClaves.Size = new System.Drawing.Size(693, 560);
            this.lwClaves.SmallImageList = this.imgGruposUsuarios;
            this.lwClaves.TabIndex = 0;
            this.lwClaves.UseCompatibleStateImageBehavior = false;
            this.lwClaves.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lwClaves_ItemDrag);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1344, 27);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(29, 24);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // FrmActualizarSurtidoPedidosSegmento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 727);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameUsuarios);
            this.Controls.Add(this.FrameDatosGenerales);
            this.Controls.Add(this.FrameClaves);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmActualizarSurtidoPedidosSegmento";
            this.ShowIcon = false;
            this.Text = "Segmentación de Órdenes de Surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.FrameClaves.ResumeLayout(false);
            this.mnGrupos.ResumeLayout(false);
            this.FrameDatosGenerales.ResumeLayout(false);
            this.FrameDatosGenerales.PerformLayout();
            this.FrameUsuarios.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.TreeView twSegmentos;
        private System.Windows.Forms.ContextMenuStrip mnGrupos;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarNombreToolStripMenuItem;
        internal System.Windows.Forms.ImageList imgGruposUsuarios;
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox FrameDatosGenerales;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scLabelExt lblStatusSurtimiento;
        private SC_ControlsCS.scLabelExt lblFarmaciaSurtido;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scLabelExt lblFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scLabelExt lblFechaPedido;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scLabelExt lblFarmaciaPedido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameUsuarios;
        private System.Windows.Forms.ListView lwClaves;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private SC_ControlsCS.scTextBoxExt txtFolioSurtido;
        private SC_ControlsCS.scLabelExt lblFolioPedido;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}