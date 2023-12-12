namespace DllTransferenciaSoft.IntegrarBD
{
    partial class FrmMttoFTP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMttoFTP));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrirFTP = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameBasesDeDatos = new System.Windows.Forms.GroupBox();
            this.chkMarcarTodo = new System.Windows.Forms.CheckBox();
            this.lstArchivos = new System.Windows.Forms.ListView();
            this.Registro = new System.Windows.Forms.ColumnHeader();
            this.BaseDeDatos = new System.Windows.Forms.ColumnHeader();
            this.Tamaño = new System.Windows.Forms.ColumnHeader();
            this.Fecha = new System.Windows.Forms.ColumnHeader();
            this.twFTP = new System.Windows.Forms.TreeView();
            this.imgNavegacion = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameBasesDeDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnAbrirFTP,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1040, 25);
            this.toolStripBarraMenu.TabIndex = 4;
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
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrirFTP
            // 
            this.btnAbrirFTP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirFTP.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirFTP.Image")));
            this.btnAbrirFTP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirFTP.Name = "btnAbrirFTP";
            this.btnAbrirFTP.Size = new System.Drawing.Size(23, 22);
            this.btnAbrirFTP.Text = "Ir a directorio FTP";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameBasesDeDatos
            // 
            this.FrameBasesDeDatos.Controls.Add(this.chkMarcarTodo);
            this.FrameBasesDeDatos.Controls.Add(this.lstArchivos);
            this.FrameBasesDeDatos.Location = new System.Drawing.Point(362, 28);
            this.FrameBasesDeDatos.Name = "FrameBasesDeDatos";
            this.FrameBasesDeDatos.Size = new System.Drawing.Size(670, 429);
            this.FrameBasesDeDatos.TabIndex = 5;
            this.FrameBasesDeDatos.TabStop = false;
            this.FrameBasesDeDatos.Text = "Listado de Bases de Datos";
            // 
            // chkMarcarTodo
            // 
            this.chkMarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.Location = new System.Drawing.Point(542, -1);
            this.chkMarcarTodo.Name = "chkMarcarTodo";
            this.chkMarcarTodo.Size = new System.Drawing.Size(110, 17);
            this.chkMarcarTodo.TabIndex = 1;
            this.chkMarcarTodo.Text = "Seleccionar todo";
            this.chkMarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarTodo.UseVisualStyleBackColor = true;
            // 
            // lstArchivos
            // 
            this.lstArchivos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Registro,
            this.BaseDeDatos,
            this.Tamaño,
            this.Fecha});
            this.lstArchivos.FullRowSelect = true;
            this.lstArchivos.GridLines = true;
            this.lstArchivos.Location = new System.Drawing.Point(10, 16);
            this.lstArchivos.MultiSelect = false;
            this.lstArchivos.Name = "lstArchivos";
            this.lstArchivos.ShowItemToolTips = true;
            this.lstArchivos.Size = new System.Drawing.Size(645, 400);
            this.lstArchivos.TabIndex = 0;
            this.lstArchivos.UseCompatibleStateImageBehavior = false;
            this.lstArchivos.View = System.Windows.Forms.View.Details;
            // 
            // Registro
            // 
            this.Registro.Text = "Núm.";
            // 
            // BaseDeDatos
            // 
            this.BaseDeDatos.Text = "Base de Datos";
            this.BaseDeDatos.Width = 339;
            // 
            // Tamaño
            // 
            this.Tamaño.Text = "Tamaño MB";
            this.Tamaño.Width = 80;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha ";
            this.Fecha.Width = 140;
            // 
            // twFTP
            // 
            this.twFTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twFTP.ImageIndex = 0;
            this.twFTP.ImageList = this.imgNavegacion;
            this.twFTP.Indent = 30;
            this.twFTP.LineColor = System.Drawing.Color.Blue;
            this.twFTP.Location = new System.Drawing.Point(10, 16);
            this.twFTP.Name = "twFTP";
            this.twFTP.SelectedImageIndex = 0;
            this.twFTP.Size = new System.Drawing.Size(325, 400);
            this.twFTP.TabIndex = 6;
            this.twFTP.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twFTP_AfterSelect);
            // 
            // imgNavegacion
            // 
            this.imgNavegacion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgNavegacion.ImageStream")));
            this.imgNavegacion.TransparentColor = System.Drawing.Color.Transparent;
            this.imgNavegacion.Images.SetKeyName(0, "Icon 286.ico");
            this.imgNavegacion.Images.SetKeyName(1, "Folder.ico");
            this.imgNavegacion.Images.SetKeyName(2, "CarpetaAbierta03.ICO");
            this.imgNavegacion.Images.SetKeyName(3, "db.ico");
            this.imgNavegacion.Images.SetKeyName(4, "Pantalla.ico");
            this.imgNavegacion.Images.SetKeyName(5, "Window.ico");
            this.imgNavegacion.Images.SetKeyName(6, "WinXPSetV4 Icon 59.ico");
            this.imgNavegacion.Images.SetKeyName(7, "Principal.ICO");
            this.imgNavegacion.Images.SetKeyName(8, "Config.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.twFTP);
            this.groupBox1.Location = new System.Drawing.Point(10, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 429);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directorio FTP";
            // 
            // FrmMttoFTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 468);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameBasesDeDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmMttoFTP";
            this.ShowInTaskbar = true;
            this.Text = "Borrar archivos de bases de datos de Sitio FTP";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMttoFTP_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameBasesDeDatos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnAbrirFTP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameBasesDeDatos;
        private System.Windows.Forms.CheckBox chkMarcarTodo;
        private System.Windows.Forms.ListView lstArchivos;
        private System.Windows.Forms.ColumnHeader Registro;
        private System.Windows.Forms.ColumnHeader BaseDeDatos;
        private System.Windows.Forms.ColumnHeader Tamaño;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.TreeView twFTP;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.ImageList imgNavegacion;
    }
}