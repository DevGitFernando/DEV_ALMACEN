namespace DtUtileriasBD
{
    partial class FrmIntegrarBD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarBD));
            this.mnFTP = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDirectorioFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameTablas = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTablasDeControl = new SC_ControlsCS.scComboBoxExt();
            this.lstwTablasMigrar = new System.Windows.Forms.ListView();
            this.Tabla = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Registros = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RegMigrar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Procesada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnIntegracion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnMnIntegracion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnRepositorio = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnDescompresion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnErrores = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnLog = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnProcesso = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMnIntegradas = new System.Windows.Forms.ToolStripMenuItem();
            this.tmIntegrarBD = new System.Windows.Forms.Timer(this.components);
            this.tmRevisionFTP = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cboBasesDeDatosOrigen = new SC_ControlsCS.scComboBoxExt();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cboBasesDeDatosDestino = new SC_ControlsCS.scComboBoxExt();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarBD = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLogErrores = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWhere = new DtUtileriasBD.scTextBoxExt();
            this.mnFTP.SuspendLayout();
            this.FrameTablas.SuspendLayout();
            this.mnIntegracion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnFTP
            // 
            this.mnFTP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDirectorioFTP});
            this.mnFTP.Name = "mnFTP";
            this.mnFTP.Size = new System.Drawing.Size(68, 26);
            // 
            // btnDirectorioFTP
            // 
            this.btnDirectorioFTP.Name = "btnDirectorioFTP";
            this.btnDirectorioFTP.Size = new System.Drawing.Size(67, 22);
            // 
            // FrameTablas
            // 
            this.FrameTablas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTablas.Controls.Add(this.label1);
            this.FrameTablas.Controls.Add(this.cboTablasDeControl);
            this.FrameTablas.Controls.Add(this.lstwTablasMigrar);
            this.FrameTablas.Location = new System.Drawing.Point(12, 148);
            this.FrameTablas.Name = "FrameTablas";
            this.FrameTablas.Size = new System.Drawing.Size(1144, 414);
            this.FrameTablas.TabIndex = 1;
            this.FrameTablas.TabStop = false;
            this.FrameTablas.Text = "Listado de tablas";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tabla de control : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTablasDeControl
            // 
            this.cboTablasDeControl.BackColorEnabled = System.Drawing.Color.White;
            this.cboTablasDeControl.Data = "";
            this.cboTablasDeControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTablasDeControl.Filtro = " 1 = 1";
            this.cboTablasDeControl.FormattingEnabled = true;
            this.cboTablasDeControl.ListaItemsBusqueda = 5;
            this.cboTablasDeControl.Location = new System.Drawing.Point(116, 18);
            this.cboTablasDeControl.MostrarToolTip = false;
            this.cboTablasDeControl.Name = "cboTablasDeControl";
            this.cboTablasDeControl.Size = new System.Drawing.Size(1006, 21);
            this.cboTablasDeControl.TabIndex = 2;
            // 
            // lstwTablasMigrar
            // 
            this.lstwTablasMigrar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstwTablasMigrar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tabla,
            this.Registros,
            this.RegMigrar,
            this.Procesada});
            this.lstwTablasMigrar.FullRowSelect = true;
            this.lstwTablasMigrar.GridLines = true;
            this.lstwTablasMigrar.HideSelection = false;
            this.lstwTablasMigrar.Location = new System.Drawing.Point(10, 49);
            this.lstwTablasMigrar.MultiSelect = false;
            this.lstwTablasMigrar.Name = "lstwTablasMigrar";
            this.lstwTablasMigrar.ShowItemToolTips = true;
            this.lstwTablasMigrar.Size = new System.Drawing.Size(1122, 347);
            this.lstwTablasMigrar.TabIndex = 1;
            this.lstwTablasMigrar.UseCompatibleStateImageBehavior = false;
            this.lstwTablasMigrar.View = System.Windows.Forms.View.Details;
            // 
            // Tabla
            // 
            this.Tabla.Text = "Tabla";
            this.Tabla.Width = 460;
            // 
            // Registros
            // 
            this.Registros.Text = "Registros";
            this.Registros.Width = 80;
            // 
            // RegMigrar
            // 
            this.RegMigrar.Text = "Migrar";
            this.RegMigrar.Width = 80;
            // 
            // Procesada
            // 
            this.Procesada.Text = "Status";
            this.Procesada.Width = 140;
            // 
            // mnIntegracion
            // 
            this.mnIntegracion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMnIntegracion,
            this.btnMnIntegradas});
            this.mnIntegracion.Name = "mnIntegracion";
            this.mnIntegracion.Size = new System.Drawing.Size(135, 48);
            // 
            // btnMnIntegracion
            // 
            this.btnMnIntegracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMnRepositorio,
            this.btnMnDescompresion,
            this.btnMnErrores,
            this.btnMnLog,
            this.btnMnProcesso});
            this.btnMnIntegracion.Name = "btnMnIntegracion";
            this.btnMnIntegracion.Size = new System.Drawing.Size(134, 22);
            this.btnMnIntegracion.Text = "Integración";
            // 
            // btnMnRepositorio
            // 
            this.btnMnRepositorio.Name = "btnMnRepositorio";
            this.btnMnRepositorio.Size = new System.Drawing.Size(156, 22);
            this.btnMnRepositorio.Text = "Repositorio";
            this.btnMnRepositorio.Click += new System.EventHandler(this.btnMnRepositorio_Click);
            // 
            // btnMnDescompresion
            // 
            this.btnMnDescompresion.Name = "btnMnDescompresion";
            this.btnMnDescompresion.Size = new System.Drawing.Size(156, 22);
            this.btnMnDescompresion.Text = "Descompresión";
            this.btnMnDescompresion.Click += new System.EventHandler(this.btnMnDescompresion_Click);
            // 
            // btnMnErrores
            // 
            this.btnMnErrores.Name = "btnMnErrores";
            this.btnMnErrores.Size = new System.Drawing.Size(156, 22);
            this.btnMnErrores.Text = "Errores";
            this.btnMnErrores.Click += new System.EventHandler(this.btnMnErrores_Click);
            // 
            // btnMnLog
            // 
            this.btnMnLog.Name = "btnMnLog";
            this.btnMnLog.Size = new System.Drawing.Size(156, 22);
            this.btnMnLog.Text = "Log";
            this.btnMnLog.Click += new System.EventHandler(this.btnMnLog_Click);
            // 
            // btnMnProcesso
            // 
            this.btnMnProcesso.Name = "btnMnProcesso";
            this.btnMnProcesso.Size = new System.Drawing.Size(156, 22);
            this.btnMnProcesso.Text = "Proceso";
            this.btnMnProcesso.Click += new System.EventHandler(this.btnMnProcesso_Click);
            // 
            // btnMnIntegradas
            // 
            this.btnMnIntegradas.Name = "btnMnIntegradas";
            this.btnMnIntegradas.Size = new System.Drawing.Size(134, 22);
            this.btnMnIntegradas.Text = "Integradas";
            this.btnMnIntegradas.Click += new System.EventHandler(this.btnMnIntegradas_Click);
            // 
            // tmIntegrarBD
            // 
            this.tmIntegrarBD.Tick += new System.EventHandler(this.tmIntegrarBD_Tick);
            // 
            // tmRevisionFTP
            // 
            this.tmRevisionFTP.Tick += new System.EventHandler(this.tmRevisionFTP_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cboBasesDeDatosOrigen);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base de datos origen";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(536, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboBasesDeDatosOrigen
            // 
            this.cboBasesDeDatosOrigen.BackColorEnabled = System.Drawing.Color.White;
            this.cboBasesDeDatosOrigen.Data = "";
            this.cboBasesDeDatosOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBasesDeDatosOrigen.Filtro = " 1 = 1";
            this.cboBasesDeDatosOrigen.FormattingEnabled = true;
            this.cboBasesDeDatosOrigen.ListaItemsBusqueda = 5;
            this.cboBasesDeDatosOrigen.Location = new System.Drawing.Point(10, 19);
            this.cboBasesDeDatosOrigen.MostrarToolTip = false;
            this.cboBasesDeDatosOrigen.Name = "cboBasesDeDatosOrigen";
            this.cboBasesDeDatosOrigen.Size = new System.Drawing.Size(522, 21);
            this.cboBasesDeDatosOrigen.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.cboBasesDeDatosDestino);
            this.groupBox2.Location = new System.Drawing.Point(587, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 50);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Base de datos destino";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(536, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 19);
            this.button2.TabIndex = 1;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cboBasesDeDatosDestino
            // 
            this.cboBasesDeDatosDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboBasesDeDatosDestino.Data = "";
            this.cboBasesDeDatosDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBasesDeDatosDestino.Filtro = " 1 = 1";
            this.cboBasesDeDatosDestino.FormattingEnabled = true;
            this.cboBasesDeDatosDestino.ListaItemsBusqueda = 5;
            this.cboBasesDeDatosDestino.Location = new System.Drawing.Point(10, 19);
            this.cboBasesDeDatosDestino.MostrarToolTip = false;
            this.cboBasesDeDatosDestino.Name = "cboBasesDeDatosDestino";
            this.cboBasesDeDatosDestino.Size = new System.Drawing.Size(522, 21);
            this.cboBasesDeDatosDestino.TabIndex = 0;
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Inicializar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarBD
            // 
            this.btnIntegrarBD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarBD.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarBD.Image")));
            this.btnIntegrarBD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarBD.Name = "btnIntegrarBD";
            this.btnIntegrarBD.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarBD.Text = "Integrar Bases de Datos";
            this.btnIntegrarBD.ToolTipText = "Integrar Bases de Datos";
            this.btnIntegrarBD.Click += new System.EventHandler(this.btnIntegrarBD_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLogErrores
            // 
            this.btnLogErrores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLogErrores.Image = ((System.Drawing.Image)(resources.GetObject("btnLogErrores.Image")));
            this.btnLogErrores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogErrores.Name = "btnLogErrores";
            this.btnLogErrores.Size = new System.Drawing.Size(23, 22);
            this.btnLogErrores.Text = "Ver registro de Errores";
            this.btnLogErrores.ToolTipText = "Ver registro de Errores";
            this.btnLogErrores.Click += new System.EventHandler(this.btnLogErrores_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ContextMenuStrip = this.mnIntegracion;
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnIntegrarBD,
            this.toolStripSeparator1,
            this.btnLogErrores,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1166, 25);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWhere);
            this.groupBox3.Location = new System.Drawing.Point(12, 81);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1145, 61);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sentencia WHERE";
            // 
            // txtWhere
            // 
            this.txtWhere.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtWhere.Decimales = 2;
            this.txtWhere.EstiloTexto = DtUtileriasBD.EstiloCaptura.Texto;
            this.txtWhere.ForeColor = System.Drawing.Color.Black;
            this.txtWhere.Location = new System.Drawing.Point(10, 18);
            this.txtWhere.Margin = new System.Windows.Forms.Padding(2);
            this.txtWhere.MaxLength = 1000;
            this.txtWhere.Multiline = true;
            this.txtWhere.Name = "txtWhere";
            this.txtWhere.PermitirApostrofo = false;
            this.txtWhere.PermitirNegativos = false;
            this.txtWhere.Size = new System.Drawing.Size(1122, 33);
            this.txtWhere.TabIndex = 0;
            // 
            // FrmIntegrarBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 570);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameTablas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmIntegrarBD";
            this.Text = "Integrador de Bases de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmIntegrarBD_FormClosing);
            this.Load += new System.EventHandler(this.FrmIntegrarBD_Load);
            this.mnFTP.ResumeLayout(false);
            this.FrameTablas.ResumeLayout(false);
            this.mnIntegracion.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameTablas;
        private System.Windows.Forms.ListView lstwTablasMigrar;
        private System.Windows.Forms.ColumnHeader Tabla;
        private System.Windows.Forms.ColumnHeader Procesada;
        private System.Windows.Forms.Timer tmIntegrarBD;
        private System.Windows.Forms.ColumnHeader Registros;
        private System.Windows.Forms.ColumnHeader RegMigrar;
        private System.Windows.Forms.Timer tmRevisionFTP;
        private System.Windows.Forms.ContextMenuStrip mnFTP;
        private System.Windows.Forms.ToolStripMenuItem btnDirectorioFTP;
        private System.Windows.Forms.ContextMenuStrip mnIntegracion;
        private System.Windows.Forms.ToolStripMenuItem btnMnIntegracion;
        private System.Windows.Forms.ToolStripMenuItem btnMnIntegradas;
        private System.Windows.Forms.ToolStripMenuItem btnMnRepositorio;
        private System.Windows.Forms.ToolStripMenuItem btnMnDescompresion;
        private System.Windows.Forms.ToolStripMenuItem btnMnErrores;
        private System.Windows.Forms.ToolStripMenuItem btnMnLog;
        private System.Windows.Forms.ToolStripMenuItem btnMnProcesso;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private SC_ControlsCS.scComboBoxExt cboBasesDeDatosOrigen;
        private SC_ControlsCS.scComboBoxExt cboTablasDeControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private SC_ControlsCS.scComboBoxExt cboBasesDeDatosDestino;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnIntegrarBD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnLogErrores;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.GroupBox groupBox3;
        private scTextBoxExt txtWhere;
    }
}