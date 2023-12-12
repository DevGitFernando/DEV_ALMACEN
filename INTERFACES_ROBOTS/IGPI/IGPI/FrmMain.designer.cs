namespace Dll_IGPI
{
    using System;
    using System.Drawing;

    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.imgNavegacion_2 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNavegador = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLog = new System.Windows.Forms.ToolStripButton();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.BarraDeStatus = new System.Windows.Forms.StatusBar();
            this.lblModulo = new System.Windows.Forms.StatusBarPanel();
            this.lblFarmacia = new System.Windows.Forms.StatusBarPanel();
            this.lblServidor = new System.Windows.Forms.StatusBarPanel();
            this.lblBaseDeDatos = new System.Windows.Forms.StatusBarPanel();
            this.lblUsuarioConectado = new System.Windows.Forms.StatusBarPanel();
            this.mnOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMinimizar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBitacora = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.icoSystray = new System.Windows.Forms.NotifyIcon(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.txtRecepcion = new System.Windows.Forms.TextBox();
            this.groupBox_Protocolos = new System.Windows.Forms.GroupBox();
            this.btnRequest_O = new System.Windows.Forms.Button();
            this.btnRequest_S = new System.Windows.Forms.Button();
            this.btbRequest_B = new System.Windows.Forms.Button();
            this.btbRequest_K = new System.Windows.Forms.Button();
            this.btbRequest_A = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFlag = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLineNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrioridad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPtoVta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmHabiliar_R = new System.Windows.Forms.Timer(this.components);
            this.pcStatusComunicacion = new System.Windows.Forms.PictureBox();
            this.tmConexion = new System.Windows.Forms.Timer(this.components);
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRequest_R = new System.Windows.Forms.Button();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).BeginInit();
            this.mnOpciones.SuspendLayout();
            this.groupBox_Protocolos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcStatusComunicacion)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(581, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Visible = false;
            // 
            // imgNavegacion_2
            // 
            this.imgNavegacion_2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNavegacion_2.ImageStream")));
            this.imgNavegacion_2.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNavegacion_2.Images.SetKeyName(0, "Config.ico");
            this.imgNavegacion_2.Images.SetKeyName(1, "Folder.ico");
            this.imgNavegacion_2.Images.SetKeyName(2, "Window.ico");
            this.imgNavegacion_2.Images.SetKeyName(3, "Pantalla.ico");
            this.imgNavegacion_2.Images.SetKeyName(4, "WinXPSetV4 Icon 59.ico");
            this.imgNavegacion_2.Images.SetKeyName(5, "CarpetaAbierta03.ICO");
            this.imgNavegacion_2.Images.SetKeyName(6, "Principal.ICO");
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNavegador,
            this.toolStripSeparator6,
            this.btnLog});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1176, 40);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnNavegador
            // 
            this.btnNavegador.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNavegador.Image = ((System.Drawing.Image)(resources.GetObject("btnNavegador.Image")));
            this.btnNavegador.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNavegador.Name = "btnNavegador";
            this.btnNavegador.Size = new System.Drawing.Size(36, 37);
            this.btnNavegador.Text = "Menú";
            this.btnNavegador.Click += new System.EventHandler(this.btnNavegador_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 40);
            // 
            // btnLog
            // 
            this.btnLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLog.Image = ((System.Drawing.Image)(resources.GetObject("btnLog.Image")));
            this.btnLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(36, 37);
            this.btnLog.Text = "Mostrar log";
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
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
            // BarraDeStatus
            // 
            this.BarraDeStatus.Location = new System.Drawing.Point(0, 665);
            this.BarraDeStatus.Name = "BarraDeStatus";
            this.BarraDeStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.lblModulo,
            this.lblFarmacia,
            this.lblServidor,
            this.lblBaseDeDatos,
            this.lblUsuarioConectado});
            this.BarraDeStatus.ShowPanels = true;
            this.BarraDeStatus.Size = new System.Drawing.Size(1176, 26);
            this.BarraDeStatus.TabIndex = 26;
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblModulo.MinWidth = 15;
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Text = "Interface SII_Dll_IGPI v.1.0.0.0";
            this.lblModulo.Width = 170;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Width = 10;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Width = 10;
            // 
            // lblBaseDeDatos
            // 
            this.lblBaseDeDatos.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblBaseDeDatos.Name = "lblBaseDeDatos";
            this.lblBaseDeDatos.Width = 10;
            // 
            // lblUsuarioConectado
            // 
            this.lblUsuarioConectado.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.lblUsuarioConectado.Name = "lblUsuarioConectado";
            this.lblUsuarioConectado.Width = 10;
            // 
            // mnOpciones
            // 
            this.mnOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.btnAbrir,
            this.toolStripSeparator1,
            this.btnMinimizar,
            this.toolStripSeparator2,
            this.btnBitacora,
            this.toolStripSeparator3,
            this.btnSalir,
            this.toolStripSeparator4});
            this.mnOpciones.Name = "mnOpciones";
            this.mnOpciones.Size = new System.Drawing.Size(137, 122);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(133, 6);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(136, 22);
            this.btnAbrir.Text = "Mostrar";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(136, 22);
            this.btnMinimizar.Text = "Ocultar";
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // btnBitacora
            // 
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(136, 22);
            this.btnBitacora.Text = "Ver Bitacora";
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(133, 6);
            // 
            // btnSalir
            // 
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(136, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(133, 6);
            // 
            // icoSystray
            // 
            this.icoSystray.ContextMenuStrip = this.mnOpciones;
            this.icoSystray.Icon = ((System.Drawing.Icon)(resources.GetObject("icoSystray.Icon")));
            this.icoSystray.Text = "Servicio";
            this.icoSystray.Visible = true;
            this.icoSystray.DoubleClick += new System.EventHandler(this.icoSystray_DoubleClick);
            // 
            // txtRecepcion
            // 
            this.txtRecepcion.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtRecepcion.Location = new System.Drawing.Point(0, 40);
            this.txtRecepcion.Multiline = true;
            this.txtRecepcion.Name = "txtRecepcion";
            this.txtRecepcion.Size = new System.Drawing.Size(56, 625);
            this.txtRecepcion.TabIndex = 29;
            this.txtRecepcion.Visible = false;
            // 
            // groupBox_Protocolos
            // 
            this.groupBox_Protocolos.Controls.Add(this.btnRequest_R);
            this.groupBox_Protocolos.Controls.Add(this.btnRequest_O);
            this.groupBox_Protocolos.Controls.Add(this.btnRequest_S);
            this.groupBox_Protocolos.Controls.Add(this.btbRequest_B);
            this.groupBox_Protocolos.Controls.Add(this.btbRequest_K);
            this.groupBox_Protocolos.Controls.Add(this.btbRequest_A);
            this.groupBox_Protocolos.Controls.Add(this.txtID);
            this.groupBox_Protocolos.Controls.Add(this.label8);
            this.groupBox_Protocolos.Controls.Add(this.txtFlag);
            this.groupBox_Protocolos.Controls.Add(this.label9);
            this.groupBox_Protocolos.Controls.Add(this.txtCantidad);
            this.groupBox_Protocolos.Controls.Add(this.label7);
            this.groupBox_Protocolos.Controls.Add(this.txtCodigoEAN);
            this.groupBox_Protocolos.Controls.Add(this.label6);
            this.groupBox_Protocolos.Controls.Add(this.txtLineNumber);
            this.groupBox_Protocolos.Controls.Add(this.label5);
            this.groupBox_Protocolos.Controls.Add(this.txtPrioridad);
            this.groupBox_Protocolos.Controls.Add(this.label4);
            this.groupBox_Protocolos.Controls.Add(this.txtPuerto);
            this.groupBox_Protocolos.Controls.Add(this.label3);
            this.groupBox_Protocolos.Controls.Add(this.txtPtoVta);
            this.groupBox_Protocolos.Controls.Add(this.label2);
            this.groupBox_Protocolos.Controls.Add(this.txtOrden);
            this.groupBox_Protocolos.Controls.Add(this.label1);
            this.groupBox_Protocolos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox_Protocolos.Location = new System.Drawing.Point(56, 397);
            this.groupBox_Protocolos.Name = "groupBox_Protocolos";
            this.groupBox_Protocolos.Size = new System.Drawing.Size(1120, 268);
            this.groupBox_Protocolos.TabIndex = 38;
            this.groupBox_Protocolos.TabStop = false;
            this.groupBox_Protocolos.Text = "Probar Protocolos";
            this.groupBox_Protocolos.Visible = false;
            // 
            // btnRequest_O
            // 
            this.btnRequest_O.Location = new System.Drawing.Point(285, 149);
            this.btnRequest_O.Name = "btnRequest_O";
            this.btnRequest_O.Size = new System.Drawing.Size(160, 23);
            this.btnRequest_O.TabIndex = 41;
            this.btnRequest_O.Text = "Solicitar O";
            this.btnRequest_O.UseVisualStyleBackColor = true;
            this.btnRequest_O.Click += new System.EventHandler(this.btnRequest_O_Click);
            // 
            // btnRequest_S
            // 
            this.btnRequest_S.Location = new System.Drawing.Point(285, 123);
            this.btnRequest_S.Name = "btnRequest_S";
            this.btnRequest_S.Size = new System.Drawing.Size(160, 23);
            this.btnRequest_S.TabIndex = 40;
            this.btnRequest_S.Text = "Solicitar S";
            this.btnRequest_S.UseVisualStyleBackColor = true;
            this.btnRequest_S.Click += new System.EventHandler(this.btnRequest_S_Click);
            // 
            // btbRequest_B
            // 
            this.btbRequest_B.Location = new System.Drawing.Point(285, 77);
            this.btbRequest_B.Name = "btbRequest_B";
            this.btbRequest_B.Size = new System.Drawing.Size(160, 23);
            this.btbRequest_B.TabIndex = 39;
            this.btbRequest_B.Text = "Solicitar B";
            this.btbRequest_B.UseVisualStyleBackColor = true;
            this.btbRequest_B.Click += new System.EventHandler(this.btbRequest_B_Click);
            // 
            // btbRequest_K
            // 
            this.btbRequest_K.Location = new System.Drawing.Point(285, 48);
            this.btbRequest_K.Name = "btbRequest_K";
            this.btbRequest_K.Size = new System.Drawing.Size(160, 23);
            this.btbRequest_K.TabIndex = 38;
            this.btbRequest_K.Text = "Solicitar K";
            this.btbRequest_K.UseVisualStyleBackColor = true;
            // 
            // btbRequest_A
            // 
            this.btbRequest_A.Location = new System.Drawing.Point(285, 19);
            this.btbRequest_A.Name = "btbRequest_A";
            this.btbRequest_A.Size = new System.Drawing.Size(160, 23);
            this.btbRequest_A.TabIndex = 37;
            this.btbRequest_A.Text = "Solicitar A";
            this.btbRequest_A.UseVisualStyleBackColor = true;
            this.btbRequest_A.Click += new System.EventHandler(this.btbRequest_A_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(137, 227);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(142, 20);
            this.txtID.TabIndex = 17;
            this.txtID.Text = "1";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(38, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Id :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFlag
            // 
            this.txtFlag.Location = new System.Drawing.Point(137, 201);
            this.txtFlag.Name = "txtFlag";
            this.txtFlag.Size = new System.Drawing.Size(142, 20);
            this.txtFlag.TabIndex = 15;
            this.txtFlag.Text = "0";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(38, 201);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Flag :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(137, 175);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(142, 20);
            this.txtCantidad.TabIndex = 13;
            this.txtCantidad.Text = "1";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(38, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cantidad :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.Location = new System.Drawing.Point(137, 149);
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.Size = new System.Drawing.Size(142, 20);
            this.txtCodigoEAN.TabIndex = 11;
            this.txtCodigoEAN.Text = "7501125198090";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(38, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Producto :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLineNumber
            // 
            this.txtLineNumber.Location = new System.Drawing.Point(137, 123);
            this.txtLineNumber.Name = "txtLineNumber";
            this.txtLineNumber.Size = new System.Drawing.Size(142, 20);
            this.txtLineNumber.TabIndex = 9;
            this.txtLineNumber.Text = "1";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Numero de Lineas :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrioridad
            // 
            this.txtPrioridad.Location = new System.Drawing.Point(137, 97);
            this.txtPrioridad.Name = "txtPrioridad";
            this.txtPrioridad.Size = new System.Drawing.Size(142, 20);
            this.txtPrioridad.TabIndex = 7;
            this.txtPrioridad.Text = "3";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(38, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Prioridad :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(137, 71);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Size = new System.Drawing.Size(142, 20);
            this.txtPuerto.TabIndex = 5;
            this.txtPuerto.Text = "3";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(38, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Puerta de salida:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPtoVta
            // 
            this.txtPtoVta.Location = new System.Drawing.Point(137, 45);
            this.txtPtoVta.Name = "txtPtoVta";
            this.txtPtoVta.Size = new System.Drawing.Size(142, 20);
            this.txtPtoVta.TabIndex = 3;
            this.txtPtoVta.Text = "001";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(38, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pto Venta :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrden
            // 
            this.txtOrden.Location = new System.Drawing.Point(137, 19);
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Size = new System.Drawing.Size(142, 20);
            this.txtOrden.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(38, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Orden :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmHabiliar_R
            // 
            this.tmHabiliar_R.Interval = 10;
            this.tmHabiliar_R.Tick += new System.EventHandler(this.tmHabiliar_R_Tick);
            // 
            // pcStatusComunicacion
            // 
            this.pcStatusComunicacion.Location = new System.Drawing.Point(742, 43);
            this.pcStatusComunicacion.Name = "pcStatusComunicacion";
            this.pcStatusComunicacion.Size = new System.Drawing.Size(99, 33);
            this.pcStatusComunicacion.TabIndex = 42;
            this.pcStatusComunicacion.TabStop = false;
            // 
            // tmConexion
            // 
            this.tmConexion.Tick += new System.EventHandler(this.tmConexion_Tick);
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtxtLog.ShowSelectionMargin = true;
            this.rtxtLog.Size = new System.Drawing.Size(273, 357);
            this.rtxtLog.TabIndex = 44;
            this.rtxtLog.Text = "";
            this.rtxtLog.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtxtLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(903, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 357);
            this.panel1.TabIndex = 46;
            // 
            // btnRequest_R
            // 
            this.btnRequest_R.Location = new System.Drawing.Point(285, 175);
            this.btnRequest_R.Name = "btnRequest_R";
            this.btnRequest_R.Size = new System.Drawing.Size(160, 23);
            this.btnRequest_R.TabIndex = 42;
            this.btnRequest_R.Text = "Solicitar R";
            this.btnRequest_R.UseVisualStyleBackColor = true;
            this.btnRequest_R.Click += new System.EventHandler(this.btnRequest_R_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1176, 691);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox_Protocolos);
            this.Controls.Add(this.pcStatusComunicacion);
            this.Controls.Add(this.txtRecepcion);
            this.Controls.Add(this.BarraDeStatus);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.Text = "Interface GPI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblModulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFarmacia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblServidor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBaseDeDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUsuarioConectado)).EndInit();
            this.mnOpciones.ResumeLayout(false);
            this.groupBox_Protocolos.ResumeLayout(false);
            this.groupBox_Protocolos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcStatusComunicacion)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /*
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departamentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem estadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem municipiosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coloniasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requisicionesToolStripMenuItem;
        */
        private System.Windows.Forms.MenuStrip menuStrip;
        internal System.Windows.Forms.ImageList imgNavegacion_2;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNavegador;
        internal System.Windows.Forms.ImageList imgNavegacion;
        private System.Windows.Forms.StatusBar BarraDeStatus;
        private System.Windows.Forms.StatusBarPanel lblModulo;
        private System.Windows.Forms.StatusBarPanel lblFarmacia;
        private System.Windows.Forms.StatusBarPanel lblServidor;
        private System.Windows.Forms.StatusBarPanel lblBaseDeDatos;
        private System.Windows.Forms.StatusBarPanel lblUsuarioConectado;
        private System.Windows.Forms.ContextMenuStrip mnOpciones;
        private System.Windows.Forms.ToolStripMenuItem btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnMinimizar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnBitacora;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.NotifyIcon icoSystray;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TextBox txtRecepcion;
        private System.Windows.Forms.GroupBox groupBox_Protocolos;
        private System.Windows.Forms.TextBox txtOrden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPtoVta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrioridad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLineNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodigoEAN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFlag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btbRequest_A;
        private System.Windows.Forms.Button btbRequest_K;
        private System.Windows.Forms.Button btbRequest_B;
        private System.Windows.Forms.Timer tmHabiliar_R;
        private System.Windows.Forms.PictureBox pcStatusComunicacion;
        private System.Windows.Forms.Timer tmConexion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnLog;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Button btnRequest_S;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRequest_O;
        private System.Windows.Forms.Button btnRequest_R;

    }
}

