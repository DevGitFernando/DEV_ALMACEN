namespace Facturacion.GenerarRemisiones
{
    partial class FrmListadoRemisiones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoRemisiones));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.FrameRemisiones = new System.Windows.Forms.GroupBox();
            this.lstRemisiones = new SC_ControlsCS.scListView();
            this.Folio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rubro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomRubro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Financiamiento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomFinanciamiento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SubCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NomSubCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Importe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TipoRemision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusRemision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TipoInsumo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameTipoRemisiones = new System.Windows.Forms.GroupBox();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoNoFacturable = new System.Windows.Forms.RadioButton();
            this.rdoFacturable = new System.Windows.Forms.RadioButton();
            this.FrameInsumos = new System.Windows.Forms.GroupBox();
            this.rdoAmbosInsumos = new System.Windows.Forms.RadioButton();
            this.rdoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoMedicamento = new System.Windows.Forms.RadioButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.chkTodasFechas = new System.Windows.Forms.CheckBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameRemisiones.SuspendLayout();
            this.FrameTipoRemisiones.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Imprimir";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // FrameRemisiones
            // 
            this.FrameRemisiones.Controls.Add(this.lstRemisiones);
            this.FrameRemisiones.Location = new System.Drawing.Point(11, 90);
            this.FrameRemisiones.Name = "FrameRemisiones";
            this.FrameRemisiones.Size = new System.Drawing.Size(1161, 606);
            this.FrameRemisiones.TabIndex = 5;
            this.FrameRemisiones.TabStop = false;
            this.FrameRemisiones.Text = "Listado de Remisiones";
            // 
            // lstRemisiones
            // 
            this.lstRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRemisiones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Folio,
            this.Fecha,
            this.Rubro,
            this.NomRubro,
            this.Financiamiento,
            this.NomFinanciamiento,
            this.Cliente,
            this.NomCliente,
            this.SubCliente,
            this.NomSubCliente,
            this.Importe,
            this.TipoRemision,
            this.StatusRemision,
            this.TipoInsumo});
            this.lstRemisiones.HideSelection = false;
            this.lstRemisiones.Location = new System.Drawing.Point(12, 19);
            this.lstRemisiones.LockColumnSize = false;
            this.lstRemisiones.Name = "lstRemisiones";
            this.lstRemisiones.Size = new System.Drawing.Size(1137, 579);
            this.lstRemisiones.TabIndex = 6;
            this.lstRemisiones.UseCompatibleStateImageBehavior = false;
            this.lstRemisiones.View = System.Windows.Forms.View.Details;
            // 
            // Folio
            // 
            this.Folio.Text = "Folio Remisión";
            this.Folio.Width = 110;
            // 
            // Fecha
            // 
            this.Fecha.Text = "Fecha Remisión";
            this.Fecha.Width = 100;
            // 
            // Rubro
            // 
            this.Rubro.Text = "Núm. Rubro";
            this.Rubro.Width = 70;
            // 
            // NomRubro
            // 
            this.NomRubro.Text = "Rubro";
            this.NomRubro.Width = 150;
            // 
            // Financiamiento
            // 
            this.Financiamiento.Text = "Núm. Concepto";
            this.Financiamiento.Width = 90;
            // 
            // NomFinanciamiento
            // 
            this.NomFinanciamiento.Text = "Concepto";
            this.NomFinanciamiento.Width = 150;
            // 
            // Cliente
            // 
            this.Cliente.Text = "Núm. Cliente";
            this.Cliente.Width = 80;
            // 
            // NomCliente
            // 
            this.NomCliente.Text = "Cliente";
            this.NomCliente.Width = 150;
            // 
            // SubCliente
            // 
            this.SubCliente.Text = "Núm. SubCliente";
            this.SubCliente.Width = 92;
            // 
            // NomSubCliente
            // 
            this.NomSubCliente.Text = "Sub-Cliente";
            this.NomSubCliente.Width = 150;
            // 
            // Importe
            // 
            this.Importe.Text = "Importe";
            this.Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Importe.Width = 100;
            // 
            // TipoRemision
            // 
            this.TipoRemision.Text = "Tipo Remisión";
            this.TipoRemision.Width = 100;
            // 
            // StatusRemision
            // 
            this.StatusRemision.Text = "Status Remisión";
            this.StatusRemision.Width = 120;
            // 
            // TipoInsumo
            // 
            this.TipoInsumo.Text = "Tipo Insumo";
            this.TipoInsumo.Width = 150;
            // 
            // FrameTipoRemisiones
            // 
            this.FrameTipoRemisiones.Controls.Add(this.rdoAmbos);
            this.FrameTipoRemisiones.Controls.Add(this.rdoNoFacturable);
            this.FrameTipoRemisiones.Controls.Add(this.rdoFacturable);
            this.FrameTipoRemisiones.Location = new System.Drawing.Point(11, 28);
            this.FrameTipoRemisiones.Name = "FrameTipoRemisiones";
            this.FrameTipoRemisiones.Size = new System.Drawing.Size(397, 59);
            this.FrameTipoRemisiones.TabIndex = 6;
            this.FrameTipoRemisiones.TabStop = false;
            this.FrameTipoRemisiones.Text = "Tipo Remisiones";
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Checked = true;
            this.rdoAmbos.Location = new System.Drawing.Point(300, 26);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(85, 17);
            this.rdoAmbos.TabIndex = 2;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoNoFacturable
            // 
            this.rdoNoFacturable.Location = new System.Drawing.Point(148, 26);
            this.rdoNoFacturable.Name = "rdoNoFacturable";
            this.rdoNoFacturable.Size = new System.Drawing.Size(127, 17);
            this.rdoNoFacturable.TabIndex = 1;
            this.rdoNoFacturable.Text = "No Facturable ";
            this.rdoNoFacturable.UseVisualStyleBackColor = true;
            // 
            // rdoFacturable
            // 
            this.rdoFacturable.Location = new System.Drawing.Point(11, 26);
            this.rdoFacturable.Name = "rdoFacturable";
            this.rdoFacturable.Size = new System.Drawing.Size(122, 17);
            this.rdoFacturable.TabIndex = 0;
            this.rdoFacturable.Text = "Facturable";
            this.rdoFacturable.UseVisualStyleBackColor = true;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoAmbosInsumos);
            this.FrameInsumos.Controls.Add(this.rdoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(414, 28);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(430, 59);
            this.FrameInsumos.TabIndex = 7;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo Insumos";
            // 
            // rdoAmbosInsumos
            // 
            this.rdoAmbosInsumos.Checked = true;
            this.rdoAmbosInsumos.Location = new System.Drawing.Point(317, 26);
            this.rdoAmbosInsumos.Name = "rdoAmbosInsumos";
            this.rdoAmbosInsumos.Size = new System.Drawing.Size(85, 17);
            this.rdoAmbosInsumos.TabIndex = 2;
            this.rdoAmbosInsumos.TabStop = true;
            this.rdoAmbosInsumos.Text = "Ambos";
            this.rdoAmbosInsumos.UseVisualStyleBackColor = true;
            // 
            // rdoMatCuracion
            // 
            this.rdoMatCuracion.Location = new System.Drawing.Point(165, 26);
            this.rdoMatCuracion.Name = "rdoMatCuracion";
            this.rdoMatCuracion.Size = new System.Drawing.Size(127, 17);
            this.rdoMatCuracion.TabIndex = 1;
            this.rdoMatCuracion.Text = "Material de Curación";
            this.rdoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoMedicamento
            // 
            this.rdoMedicamento.Location = new System.Drawing.Point(28, 26);
            this.rdoMedicamento.Name = "rdoMedicamento";
            this.rdoMedicamento.Size = new System.Drawing.Size(122, 17);
            this.rdoMedicamento.TabIndex = 0;
            this.rdoMedicamento.Text = "Medicamento";
            this.rdoMedicamento.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.chkTodasFechas);
            this.FrameFechas.Controls.Add(this.dtpFechaFin);
            this.FrameFechas.Controls.Add(this.label1);
            this.FrameFechas.Controls.Add(this.dtpFechaInicio);
            this.FrameFechas.Controls.Add(this.label9);
            this.FrameFechas.Location = new System.Drawing.Point(850, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(322, 59);
            this.FrameFechas.TabIndex = 8;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // chkTodasFechas
            // 
            this.chkTodasFechas.AutoSize = true;
            this.chkTodasFechas.Location = new System.Drawing.Point(202, 0);
            this.chkTodasFechas.Name = "chkTodasFechas";
            this.chkTodasFechas.Size = new System.Drawing.Size(110, 17);
            this.chkTodasFechas.TabIndex = 57;
            this.chkTodasFechas.Text = "Todas las Fechas";
            this.chkTodasFechas.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFin.Location = new System.Drawing.Point(210, 24);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaFin.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(165, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Fin :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicio.Location = new System.Drawing.Point(63, 24);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaInicio.TabIndex = 53;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(18, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Inicio :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            // 
            // FrmListadoRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 702);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.FrameTipoRemisiones);
            this.Controls.Add(this.FrameRemisiones);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoRemisiones";
            this.Text = "Listado Remisiones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoRemisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameRemisiones.ResumeLayout(false);
            this.FrameTipoRemisiones.ResumeLayout(false);
            this.FrameInsumos.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
            this.FrameFechas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.GroupBox FrameRemisiones;
        private SC_ControlsCS.scListView lstRemisiones;
        private System.Windows.Forms.GroupBox FrameTipoRemisiones;
        private System.Windows.Forms.RadioButton rdoAmbos;
        private System.Windows.Forms.RadioButton rdoNoFacturable;
        private System.Windows.Forms.RadioButton rdoFacturable;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoAmbosInsumos;
        private System.Windows.Forms.RadioButton rdoMatCuracion;
        private System.Windows.Forms.RadioButton rdoMedicamento;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkTodasFechas;
        private System.Windows.Forms.ColumnHeader Folio;
        private System.Windows.Forms.ColumnHeader Fecha;
        private System.Windows.Forms.ColumnHeader Rubro;
        private System.Windows.Forms.ColumnHeader NomRubro;
        private System.Windows.Forms.ColumnHeader Financiamiento;
        private System.Windows.Forms.ColumnHeader NomFinanciamiento;
        private System.Windows.Forms.ColumnHeader Cliente;
        private System.Windows.Forms.ColumnHeader NomCliente;
        private System.Windows.Forms.ColumnHeader SubCliente;
        private System.Windows.Forms.ColumnHeader NomSubCliente;
        private System.Windows.Forms.ColumnHeader Importe;
        private System.Windows.Forms.ColumnHeader TipoRemision;
        private System.Windows.Forms.ColumnHeader StatusRemision;
        private System.Windows.Forms.ColumnHeader TipoInsumo;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}