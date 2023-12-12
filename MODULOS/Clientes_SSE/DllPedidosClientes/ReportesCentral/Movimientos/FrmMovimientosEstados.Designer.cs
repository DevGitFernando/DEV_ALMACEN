namespace DllPedidosClientes.ReportesCentral
{
    partial class FrmMovimientosEstados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMovimientosEstados));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lstMovtos = new SC_ControlsCS.scListView();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameMovimientos = new System.Windows.Forms.GroupBox();
            this.rdoEntradas = new System.Windows.Forms.RadioButton();
            this.rdoDistribucion = new System.Windows.Forms.RadioButton();
            this.rdoDispensacion = new System.Windows.Forms.RadioButton();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstTotalizado = new SC_ControlsCS.scListView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameMovimientos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(862, 25);
            this.toolStripBarraMenu.TabIndex = 14;
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
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(381, 26);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(179, 89);
            this.FrameFechas.TabIndex = 18;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Periodo de revisión";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Periodo :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial.CustomFormat = "yyyy-MM";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(73, 33);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.ShowUpDown = true;
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 1, 0, 0, 0, 0);
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.lstMovtos);
            this.FrameResultado.Location = new System.Drawing.Point(10, 116);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(839, 239);
            this.FrameResultado.TabIndex = 19;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Detalles";
            // 
            // lstMovtos
            // 
            this.lstMovtos.Location = new System.Drawing.Point(10, 19);
            this.lstMovtos.LockColumnSize = false;
            this.lstMovtos.Name = "lstMovtos";
            this.lstMovtos.Size = new System.Drawing.Size(818, 209);
            this.lstMovtos.TabIndex = 1;
            this.lstMovtos.UseCompatibleStateImageBehavior = false;
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.cboJurisdicciones);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.cboEstados);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Location = new System.Drawing.Point(10, 26);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(365, 89);
            this.FrameDatos.TabIndex = 17;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Información Estado";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(79, 21);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(269, 21);
            this.cboEstados.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameMovimientos
            // 
            this.FrameMovimientos.Controls.Add(this.rdoEntradas);
            this.FrameMovimientos.Controls.Add(this.rdoDistribucion);
            this.FrameMovimientos.Controls.Add(this.rdoDispensacion);
            this.FrameMovimientos.Location = new System.Drawing.Point(566, 28);
            this.FrameMovimientos.Name = "FrameMovimientos";
            this.FrameMovimientos.Size = new System.Drawing.Size(283, 87);
            this.FrameMovimientos.TabIndex = 20;
            this.FrameMovimientos.TabStop = false;
            this.FrameMovimientos.Text = "Tipo Movimientos";
            // 
            // rdoEntradas
            // 
            this.rdoEntradas.Checked = true;
            this.rdoEntradas.Location = new System.Drawing.Point(13, 35);
            this.rdoEntradas.Name = "rdoEntradas";
            this.rdoEntradas.Size = new System.Drawing.Size(68, 15);
            this.rdoEntradas.TabIndex = 0;
            this.rdoEntradas.TabStop = true;
            this.rdoEntradas.Text = "Entradas";
            this.rdoEntradas.UseVisualStyleBackColor = true;
            // 
            // rdoDistribucion
            // 
            this.rdoDistribucion.Location = new System.Drawing.Point(182, 34);
            this.rdoDistribucion.Name = "rdoDistribucion";
            this.rdoDistribucion.Size = new System.Drawing.Size(92, 17);
            this.rdoDistribucion.TabIndex = 2;
            this.rdoDistribucion.Text = "Distribución";
            this.rdoDistribucion.UseVisualStyleBackColor = true;
            // 
            // rdoDispensacion
            // 
            this.rdoDispensacion.Location = new System.Drawing.Point(87, 31);
            this.rdoDispensacion.Name = "rdoDispensacion";
            this.rdoDispensacion.Size = new System.Drawing.Size(90, 23);
            this.rdoDispensacion.TabIndex = 1;
            this.rdoDispensacion.Text = "Dispensación";
            this.rdoDispensacion.UseVisualStyleBackColor = true;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.Location = new System.Drawing.Point(79, 48);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(269, 21);
            this.cboJurisdicciones.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Jurisdicción :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstTotalizado);
            this.groupBox1.Location = new System.Drawing.Point(10, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(839, 239);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Totalizados";
            // 
            // lstTotalizado
            // 
            this.lstTotalizado.Location = new System.Drawing.Point(10, 19);
            this.lstTotalizado.LockColumnSize = false;
            this.lstTotalizado.Name = "lstTotalizado";
            this.lstTotalizado.Size = new System.Drawing.Size(818, 209);
            this.lstTotalizado.TabIndex = 1;
            this.lstTotalizado.UseCompatibleStateImageBehavior = false;
            // 
            // FrmMovimientosEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 605);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameMovimientos);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmMovimientosEstados";
            this.Text = "Movimientos Proveedores Externos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmMovimientosEstados_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.FrameResultado.ResumeLayout(false);
            this.FrameDatos.ResumeLayout(false);
            this.FrameMovimientos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox FrameResultado;
        private SC_ControlsCS.scListView lstMovtos;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameMovimientos;
        private System.Windows.Forms.RadioButton rdoEntradas;
        private System.Windows.Forms.RadioButton rdoDistribucion;
        private System.Windows.Forms.RadioButton rdoDispensacion;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scListView lstTotalizado;
    }
}