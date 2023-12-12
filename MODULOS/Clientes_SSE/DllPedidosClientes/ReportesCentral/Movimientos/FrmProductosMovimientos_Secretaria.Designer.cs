namespace DllPedidosClientes.ReportesCentral
{
    partial class FrmProductosMovimientos_Secretaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductosMovimientos_Secretaria));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.FrameEstado = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstClaves = new SC_ControlsCS.scListView();
            this.FramePeriodo = new System.Windows.Forms.GroupBox();
            this.FrameTipo = new System.Windows.Forms.GroupBox();
            this.rdoMaterial = new System.Windows.Forms.RadioButton();
            this.rdoMedicamento = new System.Windows.Forms.RadioButton();
            this.rdoTodos_Producto = new System.Windows.Forms.RadioButton();
            this.rdoFecha = new System.Windows.Forms.RadioButton();
            this.rdoTodos_Fecha = new System.Windows.Forms.RadioButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEstado.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FramePeriodo.SuspendLayout();
            this.FrameTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(989, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(418, 639);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(274, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            // 
            // FrameEstado
            // 
            this.FrameEstado.Controls.Add(this.cboEstados);
            this.FrameEstado.Controls.Add(this.label8);
            this.FrameEstado.Location = new System.Drawing.Point(9, 25);
            this.FrameEstado.Name = "FrameEstado";
            this.FrameEstado.Size = new System.Drawing.Size(358, 50);
            this.FrameEstado.TabIndex = 0;
            this.FrameEstado.TabStop = false;
            this.FrameEstado.Text = "Información de jurisdicciones";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(63, 17);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(279, 21);
            this.cboEstados.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "yyyy-MM";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(81, 17);
            this.dtpFecha.MinDate = new System.DateTime(1997, 1, 1, 0, 0, 0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(61, 20);
            this.dtpFecha.TabIndex = 0;
            this.dtpFecha.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstClaves);
            this.groupBox3.Location = new System.Drawing.Point(9, 81);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(968, 378);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Unidades";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(12, 19);
            this.lstClaves.LockColumnSize = false;
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(942, 345);
            this.lstClaves.TabIndex = 6;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // FramePeriodo
            // 
            this.FramePeriodo.Controls.Add(this.rdoTodos_Fecha);
            this.FramePeriodo.Controls.Add(this.rdoFecha);
            this.FramePeriodo.Controls.Add(this.dtpFecha);
            this.FramePeriodo.Location = new System.Drawing.Point(742, 25);
            this.FramePeriodo.Name = "FramePeriodo";
            this.FramePeriodo.Size = new System.Drawing.Size(235, 50);
            this.FramePeriodo.TabIndex = 2;
            this.FramePeriodo.TabStop = false;
            this.FramePeriodo.Text = "Periodo a Consultar";
            // 
            // FrameTipo
            // 
            this.FrameTipo.Controls.Add(this.rdoMaterial);
            this.FrameTipo.Controls.Add(this.rdoMedicamento);
            this.FrameTipo.Controls.Add(this.rdoTodos_Producto);
            this.FrameTipo.Location = new System.Drawing.Point(383, 25);
            this.FrameTipo.Name = "FrameTipo";
            this.FrameTipo.Size = new System.Drawing.Size(344, 50);
            this.FrameTipo.TabIndex = 1;
            this.FrameTipo.TabStop = false;
            this.FrameTipo.Text = "Tipo de Producto";
            // 
            // rdoMaterial
            // 
            this.rdoMaterial.AutoSize = true;
            this.rdoMaterial.Location = new System.Drawing.Point(206, 19);
            this.rdoMaterial.Name = "rdoMaterial";
            this.rdoMaterial.Size = new System.Drawing.Size(122, 17);
            this.rdoMaterial.TabIndex = 2;
            this.rdoMaterial.TabStop = true;
            this.rdoMaterial.Text = "Material de Curación";
            this.rdoMaterial.UseVisualStyleBackColor = true;
            // 
            // rdoMedicamento
            // 
            this.rdoMedicamento.AutoSize = true;
            this.rdoMedicamento.Location = new System.Drawing.Point(97, 19);
            this.rdoMedicamento.Name = "rdoMedicamento";
            this.rdoMedicamento.Size = new System.Drawing.Size(89, 17);
            this.rdoMedicamento.TabIndex = 1;
            this.rdoMedicamento.TabStop = true;
            this.rdoMedicamento.Text = "Medicamento";
            this.rdoMedicamento.UseVisualStyleBackColor = true;
            // 
            // rdoTodos_Producto
            // 
            this.rdoTodos_Producto.AutoSize = true;
            this.rdoTodos_Producto.Location = new System.Drawing.Point(20, 19);
            this.rdoTodos_Producto.Name = "rdoTodos_Producto";
            this.rdoTodos_Producto.Size = new System.Drawing.Size(55, 17);
            this.rdoTodos_Producto.TabIndex = 0;
            this.rdoTodos_Producto.TabStop = true;
            this.rdoTodos_Producto.Text = "Todos";
            this.rdoTodos_Producto.UseVisualStyleBackColor = true;
            // 
            // rdoFecha
            // 
            this.rdoFecha.AutoSize = true;
            this.rdoFecha.Location = new System.Drawing.Point(17, 19);
            this.rdoFecha.Name = "rdoFecha";
            this.rdoFecha.Size = new System.Drawing.Size(58, 17);
            this.rdoFecha.TabIndex = 1;
            this.rdoFecha.TabStop = true;
            this.rdoFecha.Text = "Fecha:";
            this.rdoFecha.UseVisualStyleBackColor = true;
            this.rdoFecha.CheckedChanged += new System.EventHandler(this.rdoFecha_CheckedChanged);
            // 
            // rdoTodos_Fecha
            // 
            this.rdoTodos_Fecha.AutoSize = true;
            this.rdoTodos_Fecha.Location = new System.Drawing.Point(163, 18);
            this.rdoTodos_Fecha.Name = "rdoTodos_Fecha";
            this.rdoTodos_Fecha.Size = new System.Drawing.Size(55, 17);
            this.rdoTodos_Fecha.TabIndex = 2;
            this.rdoTodos_Fecha.TabStop = true;
            this.rdoTodos_Fecha.Text = "Todos";
            this.rdoTodos_Fecha.UseVisualStyleBackColor = true;
            this.rdoTodos_Fecha.CheckedChanged += new System.EventHandler(this.rdoTodos_Fecha_CheckedChanged);
            // 
            // FrmProductosMovimientos_Secretaria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 466);
            this.Controls.Add(this.FrameTipo);
            this.Controls.Add(this.FramePeriodo);
            this.Controls.Add(this.FrameEstado);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cboFarmacias);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProductosMovimientos_Secretaria";
            this.Text = "Movimientos de Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReportesFacturacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEstado.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FramePeriodo.ResumeLayout(false);
            this.FramePeriodo.PerformLayout();
            this.FrameTipo.ResumeLayout(false);
            this.FrameTipo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmEjecuciones;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.GroupBox groupBox3;
        private SC_ControlsCS.scListView lstClaves;
        private System.Windows.Forms.GroupBox FramePeriodo;
        private System.Windows.Forms.GroupBox FrameTipo;
        private System.Windows.Forms.RadioButton rdoMaterial;
        private System.Windows.Forms.RadioButton rdoMedicamento;
        private System.Windows.Forms.RadioButton rdoTodos_Producto;
        private System.Windows.Forms.RadioButton rdoTodos_Fecha;
        private System.Windows.Forms.RadioButton rdoFecha;
    }
}