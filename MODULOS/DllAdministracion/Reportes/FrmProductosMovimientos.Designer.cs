namespace DllAdministracion.Reportes
{
    partial class FrmProductosMovimientos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductosMovimientos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.grpUnidad = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.grpDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoAmbos = new System.Windows.Forms.RadioButton();
            this.rdoConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoVenta = new System.Windows.Forms.RadioButton();
            this.grpFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lstMovimientos = new SC_ControlsCS.scListView();
            this.tabMovimientos = new System.Windows.Forms.TabControl();
            this.pgMovimientos = new System.Windows.Forms.TabPage();
            this.pgGlosario = new System.Windows.Forms.TabPage();
            this.lstGlosario = new SC_ControlsCS.scListView();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.grpUnidad.SuspendLayout();
            this.grpDispensacion.SuspendLayout();
            this.grpFechas.SuspendLayout();
            this.tabMovimientos.SuspendLayout();
            this.pgMovimientos.SuspendLayout();
            this.pgGlosario.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(819, 25);
            this.toolStripBarraMenu.TabIndex = 20;
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
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // grpUnidad
            // 
            this.grpUnidad.Controls.Add(this.label4);
            this.grpUnidad.Controls.Add(this.cboFarmacias);
            this.grpUnidad.Controls.Add(this.cboEmpresas);
            this.grpUnidad.Controls.Add(this.label6);
            this.grpUnidad.Controls.Add(this.cboEstados);
            this.grpUnidad.Controls.Add(this.label8);
            this.grpUnidad.Location = new System.Drawing.Point(12, 28);
            this.grpUnidad.Name = "grpUnidad";
            this.grpUnidad.Size = new System.Drawing.Size(395, 110);
            this.grpUnidad.TabIndex = 0;
            this.grpUnidad.TabStop = false;
            this.grpUnidad.Text = "Información de Unidad";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(80, 73);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(301, 21);
            this.cboFarmacias.TabIndex = 2;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.Location = new System.Drawing.Point(80, 19);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(301, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Empresa :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(80, 46);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(301, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(22, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpDispensacion
            // 
            this.grpDispensacion.Controls.Add(this.rdoAmbos);
            this.grpDispensacion.Controls.Add(this.rdoConsignacion);
            this.grpDispensacion.Controls.Add(this.rdoVenta);
            this.grpDispensacion.Location = new System.Drawing.Point(413, 85);
            this.grpDispensacion.Name = "grpDispensacion";
            this.grpDispensacion.Size = new System.Drawing.Size(395, 53);
            this.grpDispensacion.TabIndex = 2;
            this.grpDispensacion.TabStop = false;
            this.grpDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoAmbos
            // 
            this.rdoAmbos.Location = new System.Drawing.Point(42, 22);
            this.rdoAmbos.Name = "rdoAmbos";
            this.rdoAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoAmbos.TabIndex = 0;
            this.rdoAmbos.TabStop = true;
            this.rdoAmbos.Text = "Ambos";
            this.rdoAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoConsignacion
            // 
            this.rdoConsignacion.Location = new System.Drawing.Point(265, 22);
            this.rdoConsignacion.Name = "rdoConsignacion";
            this.rdoConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoConsignacion.TabIndex = 2;
            this.rdoConsignacion.TabStop = true;
            this.rdoConsignacion.Text = "Consignación";
            this.rdoConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoVenta
            // 
            this.rdoVenta.Location = new System.Drawing.Point(158, 22);
            this.rdoVenta.Name = "rdoVenta";
            this.rdoVenta.Size = new System.Drawing.Size(65, 15);
            this.rdoVenta.TabIndex = 1;
            this.rdoVenta.TabStop = true;
            this.rdoVenta.Text = "Venta";
            this.rdoVenta.UseVisualStyleBackColor = true;
            // 
            // grpFechas
            // 
            this.grpFechas.Controls.Add(this.dtpFechaFinal);
            this.grpFechas.Controls.Add(this.label2);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.dtpFechaInicial);
            this.grpFechas.Location = new System.Drawing.Point(413, 28);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(395, 56);
            this.grpFechas.TabIndex = 1;
            this.grpFechas.TabStop = false;
            this.grpFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(241, 22);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(210, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(56, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(98, 22);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // lstMovimientos
            // 
            this.lstMovimientos.Location = new System.Drawing.Point(6, 7);
            this.lstMovimientos.LockColumnSize = false;
            this.lstMovimientos.Name = "lstMovimientos";
            this.lstMovimientos.Size = new System.Drawing.Size(776, 334);
            this.lstMovimientos.TabIndex = 5;
            this.lstMovimientos.UseCompatibleStateImageBehavior = false;
            // 
            // tabMovimientos
            // 
            this.tabMovimientos.Controls.Add(this.pgMovimientos);
            this.tabMovimientos.Controls.Add(this.pgGlosario);
            this.tabMovimientos.Location = new System.Drawing.Point(12, 144);
            this.tabMovimientos.Name = "tabMovimientos";
            this.tabMovimientos.SelectedIndex = 0;
            this.tabMovimientos.Size = new System.Drawing.Size(796, 373);
            this.tabMovimientos.TabIndex = 22;
            // 
            // pgMovimientos
            // 
            this.pgMovimientos.Controls.Add(this.lstMovimientos);
            this.pgMovimientos.Location = new System.Drawing.Point(4, 22);
            this.pgMovimientos.Name = "pgMovimientos";
            this.pgMovimientos.Padding = new System.Windows.Forms.Padding(3);
            this.pgMovimientos.Size = new System.Drawing.Size(788, 347);
            this.pgMovimientos.TabIndex = 0;
            this.pgMovimientos.Text = "Detalle de Movimientos";
            this.pgMovimientos.UseVisualStyleBackColor = true;
            // 
            // pgGlosario
            // 
            this.pgGlosario.Controls.Add(this.lstGlosario);
            this.pgGlosario.Location = new System.Drawing.Point(4, 22);
            this.pgGlosario.Name = "pgGlosario";
            this.pgGlosario.Padding = new System.Windows.Forms.Padding(3);
            this.pgGlosario.Size = new System.Drawing.Size(788, 347);
            this.pgGlosario.TabIndex = 1;
            this.pgGlosario.Text = "Glosario de Movimientos";
            this.pgGlosario.UseVisualStyleBackColor = true;
            // 
            // lstGlosario
            // 
            this.lstGlosario.Location = new System.Drawing.Point(6, 6);
            this.lstGlosario.LockColumnSize = false;
            this.lstGlosario.Name = "lstGlosario";
            this.lstGlosario.Size = new System.Drawing.Size(776, 334);
            this.lstGlosario.TabIndex = 6;
            this.lstGlosario.UseCompatibleStateImageBehavior = false;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrmProductosMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 529);
            this.Controls.Add(this.tabMovimientos);
            this.Controls.Add(this.grpFechas);
            this.Controls.Add(this.grpDispensacion);
            this.Controls.Add(this.grpUnidad);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProductosMovimientos";
            this.Text = "Movimientos de Productos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProductosMovimientos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpUnidad.ResumeLayout(false);
            this.grpDispensacion.ResumeLayout(false);
            this.grpFechas.ResumeLayout(false);
            this.tabMovimientos.ResumeLayout(false);
            this.pgMovimientos.ResumeLayout(false);
            this.pgGlosario.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox grpUnidad;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grpDispensacion;
        private System.Windows.Forms.RadioButton rdoAmbos;
        private System.Windows.Forms.RadioButton rdoConsignacion;
        private System.Windows.Forms.RadioButton rdoVenta;
        private System.Windows.Forms.GroupBox grpFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scListView lstMovimientos;
        private System.Windows.Forms.TabControl tabMovimientos;
        private System.Windows.Forms.TabPage pgMovimientos;
        private System.Windows.Forms.TabPage pgGlosario;
        private SC_ControlsCS.scListView lstGlosario;
        private System.Windows.Forms.Timer tmEjecuciones;
    }
}