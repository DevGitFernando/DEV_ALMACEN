namespace Planeacion.ObtenerInformacion
{
    partial class FrmGenerarPlaneacionManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGenerarPlaneacionManual));
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.chkCobraProducto = new System.Windows.Forms.CheckBox();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.frameImportacion = new System.Windows.Forms.GroupBox();
            this.lblTituloHoja = new System.Windows.Forms.Label();
            this.cboHojas = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraImportacion = new System.Windows.Forms.ToolStrip();
            this.btnExportarPlantilla = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar_CargaMasiva = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarDatos = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.lblProcesados = new SC_ControlsCS.scLabelExt();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lstVwInformacion = new System.Windows.Forms.ListView();
            this.tmValidacion = new System.Windows.Forms.Timer(this.components);
            this.chkPermiteTransferencia = new System.Windows.Forms.CheckBox();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.chkEsProducto_Sancion = new System.Windows.Forms.CheckBox();
            this.FrameEncabezado.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.frameImportacion.SuspendLayout();
            this.toolStripBarraImportacion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameEncabezado.Controls.Add(this.chkEsProducto_Sancion);
            this.FrameEncabezado.Controls.Add(this.txtObservaciones);
            this.FrameEncabezado.Controls.Add(this.label10);
            this.FrameEncabezado.Controls.Add(this.chkPermiteTransferencia);
            this.FrameEncabezado.Controls.Add(this.checkBox2);
            this.FrameEncabezado.Controls.Add(this.chkCobraProducto);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Location = new System.Drawing.Point(12, 30);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(1234, 156);
            this.FrameEncabezado.TabIndex = 2;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Generales de Pedido manual";
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Location = new System.Drawing.Point(409, 64);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(143, 17);
            this.checkBox2.TabIndex = 74;
            this.checkBox2.Text = "Cobre servicio";
            this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // chkCobraProducto
            // 
            this.chkCobraProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCobraProducto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCobraProducto.Location = new System.Drawing.Point(409, 23);
            this.chkCobraProducto.Name = "chkCobraProducto";
            this.chkCobraProducto.Size = new System.Drawing.Size(143, 17);
            this.chkCobraProducto.TabIndex = 8;
            this.chkCobraProducto.Text = "Cobra producto";
            this.chkCobraProducto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCobraProducto.UseVisualStyleBackColor = true;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1122, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaRegistro.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(1015, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1258, 27);
            this.toolStripBarraMenu.TabIndex = 3;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(24, 24);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(24, 24);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // frameImportacion
            // 
            this.frameImportacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameImportacion.Controls.Add(this.lblTituloHoja);
            this.frameImportacion.Controls.Add(this.cboHojas);
            this.frameImportacion.Controls.Add(this.toolStripBarraImportacion);
            this.frameImportacion.Location = new System.Drawing.Point(12, 192);
            this.frameImportacion.Name = "frameImportacion";
            this.frameImportacion.Size = new System.Drawing.Size(1234, 51);
            this.frameImportacion.TabIndex = 4;
            this.frameImportacion.TabStop = false;
            this.frameImportacion.Text = "Menú de Importación";
            // 
            // lblTituloHoja
            // 
            this.lblTituloHoja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTituloHoja.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloHoja.Location = new System.Drawing.Point(753, 22);
            this.lblTituloHoja.Name = "lblTituloHoja";
            this.lblTituloHoja.Size = new System.Drawing.Size(98, 15);
            this.lblTituloHoja.TabIndex = 22;
            this.lblTituloHoja.Text = "Seleccionar Hoja :";
            this.lblTituloHoja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboHojas
            // 
            this.cboHojas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHojas.BackColorEnabled = System.Drawing.Color.White;
            this.cboHojas.Data = "";
            this.cboHojas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHojas.Filtro = " 1 = 1";
            this.cboHojas.FormattingEnabled = true;
            this.cboHojas.ListaItemsBusqueda = 20;
            this.cboHojas.Location = new System.Drawing.Point(852, 19);
            this.cboHojas.MostrarToolTip = false;
            this.cboHojas.Name = "cboHojas";
            this.cboHojas.Size = new System.Drawing.Size(371, 21);
            this.cboHojas.TabIndex = 1;
            this.cboHojas.SelectedIndexChanged += new System.EventHandler(this.cboHojas_SelectedIndexChanged);
            // 
            // toolStripBarraImportacion
            // 
            this.toolStripBarraImportacion.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBarraImportacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportarPlantilla,
            this.toolStripSeparator10,
            this.btnAbrir,
            this.toolStripSeparator7,
            this.btnEjecutar,
            this.toolStripSeparator8,
            this.btnGuardar_CargaMasiva,
            this.toolStripSeparator9,
            this.btnValidarDatos});
            this.toolStripBarraImportacion.Location = new System.Drawing.Point(3, 16);
            this.toolStripBarraImportacion.Name = "toolStripBarraImportacion";
            this.toolStripBarraImportacion.Size = new System.Drawing.Size(1228, 27);
            this.toolStripBarraImportacion.TabIndex = 0;
            this.toolStripBarraImportacion.Text = "toolStrip1";
            // 
            // btnExportarPlantilla
            // 
            this.btnExportarPlantilla.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPlantilla.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPlantilla.Image")));
            this.btnExportarPlantilla.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPlantilla.Name = "btnExportarPlantilla";
            this.btnExportarPlantilla.Size = new System.Drawing.Size(24, 24);
            this.btnExportarPlantilla.Text = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.ToolTipText = "Exportar plantilla para pedido";
            this.btnExportarPlantilla.Click += new System.EventHandler(this.btnExportarPlantilla_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 27);
            // 
            // btnAbrir
            // 
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(24, 24);
            this.btnAbrir.Text = "Abrir plantilla de pedido";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(24, 24);
            this.btnEjecutar.Text = "Cargar información de plantilla";
            this.btnEjecutar.ToolTipText = "Cargar información de plantilla";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // btnGuardar_CargaMasiva
            // 
            this.btnGuardar_CargaMasiva.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar_CargaMasiva.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar_CargaMasiva.Image")));
            this.btnGuardar_CargaMasiva.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar_CargaMasiva.Name = "btnGuardar_CargaMasiva";
            this.btnGuardar_CargaMasiva.Size = new System.Drawing.Size(24, 24);
            this.btnGuardar_CargaMasiva.Text = "Subir plantilla ";
            this.btnGuardar_CargaMasiva.Click += new System.EventHandler(this.btnGuardar_CargaMasiva_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 27);
            // 
            // btnValidarDatos
            // 
            this.btnValidarDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarDatos.Image")));
            this.btnValidarDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarDatos.Name = "btnValidarDatos";
            this.btnValidarDatos.Size = new System.Drawing.Size(24, 24);
            this.btnValidarDatos.Text = "Validar información";
            this.btnValidarDatos.Click += new System.EventHandler(this.btnValidarDatos_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FrameProceso);
            this.groupBox2.Controls.Add(this.lstVwInformacion);
            this.groupBox2.Location = new System.Drawing.Point(12, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1234, 437);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Planeación";
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.lblProcesados);
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(168, 135);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(656, 52);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Descargando información";
            // 
            // lblProcesados
            // 
            this.lblProcesados.Location = new System.Drawing.Point(357, 3);
            this.lblProcesados.MostrarToolTip = false;
            this.lblProcesados.Name = "lblProcesados";
            this.lblProcesados.Size = new System.Drawing.Size(284, 14);
            this.lblProcesados.TabIndex = 15;
            this.lblProcesados.Text = "scLabelExt1";
            this.lblProcesados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProcesados.Visible = false;
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(10, 22);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(631, 17);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // lstVwInformacion
            // 
            this.lstVwInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwInformacion.HideSelection = false;
            this.lstVwInformacion.Location = new System.Drawing.Point(6, 19);
            this.lstVwInformacion.Name = "lstVwInformacion";
            this.lstVwInformacion.Size = new System.Drawing.Size(1219, 412);
            this.lstVwInformacion.TabIndex = 7;
            this.lstVwInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // chkPermiteTransferencia
            // 
            this.chkPermiteTransferencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPermiteTransferencia.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPermiteTransferencia.Location = new System.Drawing.Point(666, 24);
            this.chkPermiteTransferencia.Name = "chkPermiteTransferencia";
            this.chkPermiteTransferencia.Size = new System.Drawing.Size(143, 17);
            this.chkPermiteTransferencia.TabIndex = 75;
            this.chkPermiteTransferencia.Text = "Permite transferencia";
            this.chkPermiteTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPermiteTransferencia.UseVisualStyleBackColor = true;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(95, 101);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(1127, 37);
            this.txtObservaciones.TabIndex = 76;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(4, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 12);
            this.label10.TabIndex = 77;
            this.label10.Text = "Observaciones :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkEsProducto_Sancion
            // 
            this.chkEsProducto_Sancion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsProducto_Sancion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEsProducto_Sancion.Location = new System.Drawing.Point(666, 64);
            this.chkEsProducto_Sancion.Name = "chkEsProducto_Sancion";
            this.chkEsProducto_Sancion.Size = new System.Drawing.Size(143, 17);
            this.chkEsProducto_Sancion.TabIndex = 78;
            this.chkEsProducto_Sancion.Text = "Es Producto Sancion";
            this.chkEsProducto_Sancion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEsProducto_Sancion.UseVisualStyleBackColor = true;
            // 
            // FrmGenerarPlaneacionManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 688);
            this.Controls.Add(this.frameImportacion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameEncabezado);
            this.Name = "FrmGenerarPlaneacionManual";
            this.Text = "Registro De Planeación Manual";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGenerarPlaneacionManual_Load);
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.frameImportacion.ResumeLayout(false);
            this.frameImportacion.PerformLayout();
            this.toolStripBarraImportacion.ResumeLayout(false);
            this.toolStripBarraImportacion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox frameImportacion;
        private System.Windows.Forms.Label lblTituloHoja;
        private SC_ControlsCS.scComboBoxExt cboHojas;
        private System.Windows.Forms.ToolStrip toolStripBarraImportacion;
        private System.Windows.Forms.ToolStripButton btnExportarPlantilla;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnAbrir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnGuardar_CargaMasiva;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnValidarDatos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox FrameProceso;
        private SC_ControlsCS.scLabelExt lblProcesados;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmValidacion;
        private System.Windows.Forms.ListView lstVwInformacion;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox chkCobraProducto;
        private System.Windows.Forms.CheckBox chkPermiteTransferencia;
        private System.Windows.Forms.CheckBox chkEsProducto_Sancion;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
    }
}