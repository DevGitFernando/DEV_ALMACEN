namespace Farmacia.PedidosDeDistribuidor
{
    partial class FrmListadoTiposRemisionesDist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoTiposRemisionesDist));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameTipoDisp = new System.Windows.Forms.GroupBox();
            this.cboReporte = new SC_ControlsCS.scComboBoxExt();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstFoliosRemisiones = new System.Windows.Forms.ListView();
            this.FrameDist = new System.Windows.Forms.GroupBox();
            this.lblDistribuidor = new System.Windows.Forms.Label();
            this.txtIdDistribuidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPrecioAdmon = new SC_ControlsCS.scNumericTextBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDirectorio = new System.Windows.Forms.Button();
            this.lblDirectorioTrabajo = new SC_ControlsCS.scLabelExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoDisp.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.FrameDist.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportarExcel,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(699, 25);
            this.toolStripBarraMenu.TabIndex = 7;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameTipoDisp
            // 
            this.FrameTipoDisp.Controls.Add(this.cboReporte);
            this.FrameTipoDisp.Location = new System.Drawing.Point(12, 73);
            this.FrameTipoDisp.Name = "FrameTipoDisp";
            this.FrameTipoDisp.Size = new System.Drawing.Size(252, 47);
            this.FrameTipoDisp.TabIndex = 1;
            this.FrameTipoDisp.TabStop = false;
            this.FrameTipoDisp.Text = "Tipo Reportes Remisiones";
            // 
            // cboReporte
            // 
            this.cboReporte.BackColorEnabled = System.Drawing.Color.White;
            this.cboReporte.Data = "";
            this.cboReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReporte.Filtro = " 1 = 1";
            this.cboReporte.FormattingEnabled = true;
            this.cboReporte.ListaItemsBusqueda = 20;
            this.cboReporte.Location = new System.Drawing.Point(8, 17);
            this.cboReporte.MostrarToolTip = false;
            this.cboReporte.Name = "cboReporte";
            this.cboReporte.Size = new System.Drawing.Size(237, 21);
            this.cboReporte.TabIndex = 1;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.FrameProceso);
            this.FrameFolios.Controls.Add(this.lstFoliosRemisiones);
            this.FrameFolios.Location = new System.Drawing.Point(12, 168);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(675, 322);
            this.FrameFolios.TabIndex = 4;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Lista de Folios";
            // 
            // lstFoliosRemisiones
            // 
            this.lstFoliosRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFoliosRemisiones.Location = new System.Drawing.Point(10, 19);
            this.lstFoliosRemisiones.Name = "lstFoliosRemisiones";
            this.lstFoliosRemisiones.Size = new System.Drawing.Size(655, 293);
            this.lstFoliosRemisiones.TabIndex = 0;
            this.lstFoliosRemisiones.UseCompatibleStateImageBehavior = false;
            this.lstFoliosRemisiones.DoubleClick += new System.EventHandler(this.lstFoliosRemisiones_DoubleClick);
            // 
            // FrameDist
            // 
            this.FrameDist.Controls.Add(this.lblDistribuidor);
            this.FrameDist.Controls.Add(this.txtIdDistribuidor);
            this.FrameDist.Controls.Add(this.label2);
            this.FrameDist.Location = new System.Drawing.Point(12, 26);
            this.FrameDist.Name = "FrameDist";
            this.FrameDist.Size = new System.Drawing.Size(675, 47);
            this.FrameDist.TabIndex = 0;
            this.FrameDist.TabStop = false;
            this.FrameDist.Text = "Datos Distribuidor";
            // 
            // lblDistribuidor
            // 
            this.lblDistribuidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistribuidor.Location = new System.Drawing.Point(186, 18);
            this.lblDistribuidor.Name = "lblDistribuidor";
            this.lblDistribuidor.Size = new System.Drawing.Size(479, 21);
            this.lblDistribuidor.TabIndex = 9;
            this.lblDistribuidor.Text = "Distribuidor :";
            this.lblDistribuidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdDistribuidor
            // 
            this.txtIdDistribuidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDistribuidor.Decimales = 2;
            this.txtIdDistribuidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdDistribuidor.ForeColor = System.Drawing.Color.Black;
            this.txtIdDistribuidor.Location = new System.Drawing.Point(80, 18);
            this.txtIdDistribuidor.MaxLength = 4;
            this.txtIdDistribuidor.Name = "txtIdDistribuidor";
            this.txtIdDistribuidor.PermitirApostrofo = false;
            this.txtIdDistribuidor.PermitirNegativos = false;
            this.txtIdDistribuidor.Size = new System.Drawing.Size(100, 20);
            this.txtIdDistribuidor.TabIndex = 0;
            this.txtIdDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDistribuidor.TextChanged += new System.EventHandler(this.txtIdDistribuidor_TextChanged);
            this.txtIdDistribuidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDistribuidor_KeyDown);
            this.txtIdDistribuidor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDistribuidor_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Distribuidor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.label1);
            this.FrameFechas.Controls.Add(this.dtpFechaFin);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicio);
            this.FrameFechas.Location = new System.Drawing.Point(266, 73);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(329, 47);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(174, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha Fin :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(241, 16);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.ShowUpDown = true;
            this.dtpFechaFin.Size = new System.Drawing.Size(81, 20);
            this.dtpFechaFin.TabIndex = 1;
            this.dtpFechaFin.Value = new System.DateTime(2012, 7, 27, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Fecha Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(88, 17);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.ShowUpDown = true;
            this.dtpFechaInicio.Size = new System.Drawing.Size(81, 20);
            this.dtpFechaInicio.TabIndex = 0;
            this.dtpFechaInicio.Value = new System.DateTime(2012, 7, 27, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPrecioAdmon);
            this.groupBox2.Location = new System.Drawing.Point(597, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(90, 47);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Precio Admon";
            // 
            // txtPrecioAdmon
            // 
            this.txtPrecioAdmon.AllowNegative = true;
            this.txtPrecioAdmon.DigitsInGroup = 3;
            this.txtPrecioAdmon.Flags = 7680;
            this.txtPrecioAdmon.Location = new System.Drawing.Point(18, 17);
            this.txtPrecioAdmon.MaxDecimalPlaces = 2;
            this.txtPrecioAdmon.MaxLength = 15;
            this.txtPrecioAdmon.MaxWholeDigits = 9;
            this.txtPrecioAdmon.Name = "txtPrecioAdmon";
            this.txtPrecioAdmon.Prefix = "";
            this.txtPrecioAdmon.RangeMax = 1.7976931348623157E+308D;
            this.txtPrecioAdmon.RangeMin = -1.7976931348623157E+308D;
            this.txtPrecioAdmon.Size = new System.Drawing.Size(52, 20);
            this.txtPrecioAdmon.TabIndex = 2;
            this.txtPrecioAdmon.Text = "0.00";
            this.txtPrecioAdmon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(129, 110);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(417, 102);
            this.FrameProceso.TabIndex = 3;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Generando Reportes Remisiones";
            // 
            // pgBar
            // 
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(15, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(388, 64);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDirectorio);
            this.groupBox3.Controls.Add(this.lblDirectorioTrabajo);
            this.groupBox3.Location = new System.Drawing.Point(12, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(675, 45);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Directorio de trabajo";
            // 
            // btnDirectorio
            // 
            this.btnDirectorio.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectorio.Image")));
            this.btnDirectorio.Location = new System.Drawing.Point(636, 12);
            this.btnDirectorio.Name = "btnDirectorio";
            this.btnDirectorio.Size = new System.Drawing.Size(28, 23);
            this.btnDirectorio.TabIndex = 0;
            this.btnDirectorio.UseVisualStyleBackColor = true;
            this.btnDirectorio.Click += new System.EventHandler(this.btnDirectorio_Click);
            // 
            // lblDirectorioTrabajo
            // 
            this.lblDirectorioTrabajo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDirectorioTrabajo.Location = new System.Drawing.Point(11, 14);
            this.lblDirectorioTrabajo.MostrarToolTip = false;
            this.lblDirectorioTrabajo.Name = "lblDirectorioTrabajo";
            this.lblDirectorioTrabajo.Size = new System.Drawing.Size(619, 21);
            this.lblDirectorioTrabajo.TabIndex = 18;
            this.lblDirectorioTrabajo.Text = "scLabelExt1";
            this.lblDirectorioTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmListadoTiposRemisionesDist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 500);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameDist);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoDisp);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoTiposRemisionesDist";
            this.Text = "Listado de Remisiones capturadas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoFoliosRemisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoDisp.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.FrameDist.ResumeLayout(false);
            this.FrameDist.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.FrameProceso.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameTipoDisp;
        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstFoliosRemisiones;
        private System.Windows.Forms.GroupBox FrameDist;
        private System.Windows.Forms.Label lblDistribuidor;
        private SC_ControlsCS.scTextBoxExt txtIdDistribuidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private SC_ControlsCS.scComboBoxExt cboReporte;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scNumericTextBox txtPrecioAdmon;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDirectorio;
        private SC_ControlsCS.scLabelExt lblDirectorioTrabajo;
    }
}