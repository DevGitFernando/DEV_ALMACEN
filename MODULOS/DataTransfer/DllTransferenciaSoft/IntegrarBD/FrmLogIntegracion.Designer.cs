namespace DllTransferenciaSoft.IntegrarBD
{
    partial class FrmLogIntegracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogIntegracion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameBusqueda = new System.Windows.Forms.GroupBox();
            this.txtFiltro = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.FrameResultados = new System.Windows.Forms.GroupBox();
            this.grdResultados = new FarPoint.Win.Spread.FpSpread();
            this.grdResultados_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameRangoDeFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameTipoDeResultado = new System.Windows.Forms.GroupBox();
            this.rdoTodo = new System.Windows.Forms.RadioButton();
            this.rdoNoIntegrado = new System.Windows.Forms.RadioButton();
            this.rdoIntegrado = new System.Windows.Forms.RadioButton();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameBusqueda.SuspendLayout();
            this.FrameResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados_Sheet1)).BeginInit();
            this.FrameRangoDeFechas.SuspendLayout();
            this.FrameTipoDeResultado.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1055, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameBusqueda
            // 
            this.FrameBusqueda.Controls.Add(this.txtFiltro);
            this.FrameBusqueda.Controls.Add(this.label5);
            this.FrameBusqueda.Location = new System.Drawing.Point(10, 28);
            this.FrameBusqueda.Name = "FrameBusqueda";
            this.FrameBusqueda.Size = new System.Drawing.Size(387, 63);
            this.FrameBusqueda.TabIndex = 1;
            this.FrameBusqueda.TabStop = false;
            this.FrameBusqueda.Text = "Busqueda";
            // 
            // txtFiltro
            // 
            this.txtFiltro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFiltro.Decimales = 2;
            this.txtFiltro.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtFiltro.ForeColor = System.Drawing.Color.Black;
            this.txtFiltro.Location = new System.Drawing.Point(63, 23);
            this.txtFiltro.MaxLength = 50;
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.PermitirApostrofo = false;
            this.txtFiltro.PermitirNegativos = false;
            this.txtFiltro.Size = new System.Drawing.Size(314, 20);
            this.txtFiltro.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Filtro :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameResultados
            // 
            this.FrameResultados.Controls.Add(this.grdResultados);
            this.FrameResultados.Location = new System.Drawing.Point(10, 94);
            this.FrameResultados.Name = "FrameResultados";
            this.FrameResultados.Size = new System.Drawing.Size(1035, 401);
            this.FrameResultados.TabIndex = 4;
            this.FrameResultados.TabStop = false;
            this.FrameResultados.Text = "Resultados";
            // 
            // grdResultados
            // 
            this.grdResultados.AccessibleDescription = "grdResultados, Sheet1, Row 0, Column 0, 2010-01-01 10:01:00 a.m";
            this.grdResultados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdResultados.Location = new System.Drawing.Point(10, 17);
            this.grdResultados.Name = "grdResultados";
            this.grdResultados.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdResultados.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdResultados_Sheet1});
            this.grdResultados.Size = new System.Drawing.Size(1016, 373);
            this.grdResultados.TabIndex = 0;
            // 
            // grdResultados_Sheet1
            // 
            this.grdResultados_Sheet1.Reset();
            this.grdResultados_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdResultados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdResultados_Sheet1.ColumnCount = 3;
            this.grdResultados_Sheet1.RowCount = 16;
            this.grdResultados_Sheet1.Cells.Get(0, 0).Value = "2010-01-01 10:01:00 a.m";
            this.grdResultados_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha registro";
            this.grdResultados_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de archivo";
            this.grdResultados_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Resultado ";
            this.grdResultados_Sheet1.ColumnHeader.Rows.Get(0).Height = 31F;
            this.grdResultados_Sheet1.Columns.Get(0).CellType = textCellType22;
            this.grdResultados_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdResultados_Sheet1.Columns.Get(0).Label = "Fecha registro";
            this.grdResultados_Sheet1.Columns.Get(0).Locked = true;
            this.grdResultados_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResultados_Sheet1.Columns.Get(0).Width = 160F;
            textCellType23.MaxLength = 500;
            this.grdResultados_Sheet1.Columns.Get(1).CellType = textCellType23;
            this.grdResultados_Sheet1.Columns.Get(1).Label = "Nombre de archivo";
            this.grdResultados_Sheet1.Columns.Get(1).Locked = true;
            this.grdResultados_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResultados_Sheet1.Columns.Get(1).Width = 600F;
            this.grdResultados_Sheet1.Columns.Get(2).CellType = textCellType24;
            this.grdResultados_Sheet1.Columns.Get(2).Label = "Resultado ";
            this.grdResultados_Sheet1.Columns.Get(2).Locked = true;
            this.grdResultados_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdResultados_Sheet1.Columns.Get(2).Width = 200F;
            this.grdResultados_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdResultados_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameRangoDeFechas
            // 
            this.FrameRangoDeFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameRangoDeFechas.Controls.Add(this.label2);
            this.FrameRangoDeFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameRangoDeFechas.Controls.Add(this.label1);
            this.FrameRangoDeFechas.Location = new System.Drawing.Point(403, 28);
            this.FrameRangoDeFechas.Name = "FrameRangoDeFechas";
            this.FrameRangoDeFechas.Size = new System.Drawing.Size(319, 63);
            this.FrameRangoDeFechas.TabIndex = 2;
            this.FrameRangoDeFechas.TabStop = false;
            this.FrameRangoDeFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(206, 23);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(167, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(83, 23);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(78, 20);
            this.dtpFechaInicial.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Inicio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameTipoDeResultado
            // 
            this.FrameTipoDeResultado.Controls.Add(this.rdoTodo);
            this.FrameTipoDeResultado.Controls.Add(this.rdoNoIntegrado);
            this.FrameTipoDeResultado.Controls.Add(this.rdoIntegrado);
            this.FrameTipoDeResultado.Location = new System.Drawing.Point(725, 28);
            this.FrameTipoDeResultado.Name = "FrameTipoDeResultado";
            this.FrameTipoDeResultado.Size = new System.Drawing.Size(320, 63);
            this.FrameTipoDeResultado.TabIndex = 3;
            this.FrameTipoDeResultado.TabStop = false;
            this.FrameTipoDeResultado.Text = "Resultado";
            // 
            // rdoTodo
            // 
            this.rdoTodo.Location = new System.Drawing.Point(237, 24);
            this.rdoTodo.Name = "rdoTodo";
            this.rdoTodo.Size = new System.Drawing.Size(53, 18);
            this.rdoTodo.TabIndex = 2;
            this.rdoTodo.Text = "Todo";
            this.rdoTodo.UseVisualStyleBackColor = true;
            // 
            // rdoNoIntegrado
            // 
            this.rdoNoIntegrado.Location = new System.Drawing.Point(124, 24);
            this.rdoNoIntegrado.Name = "rdoNoIntegrado";
            this.rdoNoIntegrado.Size = new System.Drawing.Size(91, 18);
            this.rdoNoIntegrado.TabIndex = 1;
            this.rdoNoIntegrado.Text = "No integrado";
            this.rdoNoIntegrado.UseVisualStyleBackColor = true;
            // 
            // rdoIntegrado
            // 
            this.rdoIntegrado.Checked = true;
            this.rdoIntegrado.Location = new System.Drawing.Point(31, 24);
            this.rdoIntegrado.Name = "rdoIntegrado";
            this.rdoIntegrado.Size = new System.Drawing.Size(71, 18);
            this.rdoIntegrado.TabIndex = 0;
            this.rdoIntegrado.TabStop = true;
            this.rdoIntegrado.Text = "Integrado";
            this.rdoIntegrado.UseVisualStyleBackColor = true;
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
            // FrmLogIntegracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 503);
            this.Controls.Add(this.FrameTipoDeResultado);
            this.Controls.Add(this.FrameRangoDeFechas);
            this.Controls.Add(this.FrameResultados);
            this.Controls.Add(this.FrameBusqueda);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmLogIntegracion";
            this.Text = "Registro de Integración de Bases de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmLogIntegracion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameBusqueda.ResumeLayout(false);
            this.FrameBusqueda.PerformLayout();
            this.FrameResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados_Sheet1)).EndInit();
            this.FrameRangoDeFechas.ResumeLayout(false);
            this.FrameTipoDeResultado.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameBusqueda;
        private System.Windows.Forms.GroupBox FrameResultados;
        private FarPoint.Win.Spread.FpSpread grdResultados;
        private FarPoint.Win.Spread.SheetView grdResultados_Sheet1;
        internal SC_ControlsCS.scTextBoxExt txtFiltro;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox FrameRangoDeFechas;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameTipoDeResultado;
        private System.Windows.Forms.RadioButton rdoIntegrado;
        private System.Windows.Forms.RadioButton rdoTodo;
        private System.Windows.Forms.RadioButton rdoNoIntegrado;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
    }
}