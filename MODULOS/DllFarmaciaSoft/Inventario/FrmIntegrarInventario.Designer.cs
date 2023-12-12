namespace DllFarmaciaSoft.Inventario
{
    partial class FrmIntegrarInventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarInventario));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarRemisiones = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lblProcesados = new SC_ControlsCS.scLabelExt();
            this.lstVwInformacion = new System.Windows.Forms.ListView();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.tmPantalla = new System.Windows.Forms.Timer(this.components);
            this.FrameTipoInv = new System.Windows.Forms.GroupBox();
            this.rdoParcial = new System.Windows.Forms.RadioButton();
            this.rdoCompleto = new System.Windows.Forms.RadioButton();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.FrameTipoInv.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator6,
            this.btnExportarExcel,
            this.toolStripSeparator1,
            this.btnAbrir,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnValidarDatos,
            this.toolStripSeparator5,
            this.btnProcesarRemisiones,
            this.toolStripSeparator,
            this.btnSalir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1096, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(23, 22);
            this.btnAbrir.Text = "&Abrir";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Cargar inventario";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(23, 22);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProcesarRemisiones
            // 
            this.btnProcesarRemisiones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesarRemisiones.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesarRemisiones.Image")));
            this.btnProcesarRemisiones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesarRemisiones.Name = "btnProcesarRemisiones";
            this.btnProcesarRemisiones.Size = new System.Drawing.Size(23, 22);
            this.btnProcesarRemisiones.Text = "Integrar inventario interno";
            this.btnProcesarRemisiones.Click += new System.EventHandler(this.btnProcesarRemisiones_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboHojas);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(820, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hojas del documento";
            // 
            // cboHojas
            // 
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(23, 27);
            this.cboHojas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(788, 24);
            this.cboHojas.TabIndex = 0;
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.lblProcesados);
            this.FrameResultado.Controls.Add(this.lstVwInformacion);
            this.FrameResultado.Location = new System.Drawing.Point(16, 107);
            this.FrameResultado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameResultado.Size = new System.Drawing.Size(1067, 244);
            this.FrameResultado.TabIndex = 2;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Información";
            // 
            // lblProcesados
            // 
            this.lblProcesados.Location = new System.Drawing.Point(675, 4);
            this.lblProcesados.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcesados.MostrarToolTip = false;
            this.lblProcesados.Name = "lblProcesados";
            this.lblProcesados.Size = new System.Drawing.Size(379, 21);
            this.lblProcesados.TabIndex = 1;
            this.lblProcesados.Text = "scLabelExt1";
            this.lblProcesados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstVwInformacion
            // 
            this.lstVwInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwInformacion.Location = new System.Drawing.Point(13, 30);
            this.lstVwInformacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstVwInformacion.Name = "lstVwInformacion";
            this.lstVwInformacion.Size = new System.Drawing.Size(1039, 200);
            this.lstVwInformacion.TabIndex = 0;
            this.lstVwInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(155, 358);
            this.FrameProceso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameProceso.Size = new System.Drawing.Size(803, 126);
            this.FrameProceso.TabIndex = 10;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(20, 28);
            this.pgBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(765, 79);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 3;
            // 
            // tmValidacion
            // 
            this.tmValidacion.Tick += new System.EventHandler(this.tmValidacion_Tick);
            // 
            // tmPantalla
            // 
            this.tmPantalla.Tick += new System.EventHandler(this.tmPantalla_Tick);
            // 
            // FrameTipoInv
            // 
            this.FrameTipoInv.Controls.Add(this.rdoParcial);
            this.FrameTipoInv.Controls.Add(this.rdoCompleto);
            this.FrameTipoInv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FrameTipoInv.Location = new System.Drawing.Point(844, 34);
            this.FrameTipoInv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoInv.Name = "FrameTipoInv";
            this.FrameTipoInv.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FrameTipoInv.Size = new System.Drawing.Size(239, 71);
            this.FrameTipoInv.TabIndex = 11;
            this.FrameTipoInv.TabStop = false;
            this.FrameTipoInv.Text = "Tipo de Inventario";
            // 
            // rdoParcial
            // 
            this.rdoParcial.AutoSize = true;
            this.rdoParcial.Location = new System.Drawing.Point(135, 30);
            this.rdoParcial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoParcial.Name = "rdoParcial";
            this.rdoParcial.Size = new System.Drawing.Size(79, 21);
            this.rdoParcial.TabIndex = 1;
            this.rdoParcial.TabStop = true;
            this.rdoParcial.Text = "Parcial";
            this.rdoParcial.UseVisualStyleBackColor = true;
            // 
            // rdoCompleto
            // 
            this.rdoCompleto.AutoSize = true;
            this.rdoCompleto.Location = new System.Drawing.Point(23, 30);
            this.rdoCompleto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoCompleto.Name = "rdoCompleto";
            this.rdoCompleto.Size = new System.Drawing.Size(96, 21);
            this.rdoCompleto.TabIndex = 0;
            this.rdoCompleto.TabStop = true;
            this.rdoCompleto.Text = "Completo";
            this.rdoCompleto.UseVisualStyleBackColor = true;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 548);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1096, 30);
            this.lblMensajes.TabIndex = 12;
            this.lblMensajes.Text = "Seleccionar el Tipo de Inventario a Integrar";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmIntegrarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 578);
            this.ControlBox = false;
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.FrameTipoInv);
            this.Controls.Add(this.FrameProceso);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmIntegrarInventario";
            this.Text = "Integración de inventario interno (Ajuste de inventario)";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIntegrarInventario_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrameResultado.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.FrameTipoInv.ResumeLayout(false);
            this.FrameTipoInv.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox FrameResultado;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.ListView lstVwInformacion;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private SC_ControlsCS.scLabelExt lblProcesados;
        private System.Windows.Forms.ToolStripButton btnProcesarRemisiones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Timer tmValidacion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Timer tmPantalla;
        private System.Windows.Forms.GroupBox FrameTipoInv;
        private System.Windows.Forms.RadioButton rdoParcial;
        private System.Windows.Forms.RadioButton rdoCompleto;
        private System.Windows.Forms.Label lblMensajes;
    }
}