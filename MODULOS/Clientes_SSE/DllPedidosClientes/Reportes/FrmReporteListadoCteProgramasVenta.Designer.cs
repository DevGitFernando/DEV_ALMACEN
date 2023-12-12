namespace DllPedidosClientes.Reportes
{
    partial class FrmReporteListadoCteProgramasVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteListadoCteProgramasVenta));
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.grdReporte = new FarPoint.Win.Spread.FpSpread();
            this.grdReporte_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameResultado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).BeginInit();
            this.FrameFechas.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(744, 25);
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
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.grdReporte);
            this.FrameResultado.Location = new System.Drawing.Point(10, 138);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(725, 287);
            this.FrameResultado.TabIndex = 5;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Resultado de Consulta";
            // 
            // grdReporte
            // 
            this.grdReporte.AccessibleDescription = "grdReporte, Sheet1, Row 0, Column 0, ";
            this.grdReporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdReporte.Location = new System.Drawing.Point(10, 19);
            this.grdReporte.Name = "grdReporte";
            this.grdReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdReporte.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdReporte_Sheet1});
            this.grdReporte.Size = new System.Drawing.Size(707, 260);
            this.grdReporte.TabIndex = 0;
            // 
            // grdReporte_Sheet1
            // 
            this.grdReporte_Sheet1.Reset();
            this.grdReporte_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdReporte_Sheet1.ColumnCount = 4;
            this.grdReporte_Sheet1.RowCount = 10;
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Cliente";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Receta";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Beneficiario";
            this.grdReporte_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Insumo";
            this.grdReporte_Sheet1.ColumnHeader.Rows.Get(0).Height = 38F;
            textCellType17.MaxLength = 500;
            this.grdReporte_Sheet1.Columns.Get(0).CellType = textCellType17;
            this.grdReporte_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(0).Label = "Cliente";
            this.grdReporte_Sheet1.Columns.Get(0).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(0).Width = 150F;
            this.grdReporte_Sheet1.Columns.Get(1).CellType = textCellType18;
            this.grdReporte_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Label = "Receta";
            this.grdReporte_Sheet1.Columns.Get(1).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(1).Width = 200F;
            this.grdReporte_Sheet1.Columns.Get(2).CellType = textCellType19;
            this.grdReporte_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdReporte_Sheet1.Columns.Get(2).Label = "Beneficiario";
            this.grdReporte_Sheet1.Columns.Get(2).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(2).Width = 150F;
            textCellType20.MaxLength = 1000;
            this.grdReporte_Sheet1.Columns.Get(3).CellType = textCellType20;
            this.grdReporte_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.grdReporte_Sheet1.Columns.Get(3).Label = "Insumo";
            this.grdReporte_Sheet1.Columns.Get(3).Locked = true;
            this.grdReporte_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdReporte_Sheet1.Columns.Get(3).Width = 150F;
            this.grdReporte_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdReporte_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(519, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(216, 104);
            this.FrameFechas.TabIndex = 3;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(86, 57);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(54, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(43, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(86, 28);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblFarmacia);
            this.groupBox5.Controls.Add(this.txtFarmacia);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.cboFarmacias);
            this.groupBox5.Controls.Add(this.cboEmpresas);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(10, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(503, 104);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 76);
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
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(89, 140);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(402, 21);
            this.cboFarmacias.TabIndex = 2;
            this.cboFarmacias.Visible = false;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.FormattingEnabled = true;
            this.cboEmpresas.Location = new System.Drawing.Point(89, 19);
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(402, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(23, 23);
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
            this.cboEstados.Location = new System.Drawing.Point(89, 46);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(402, 21);
            this.cboEstados.TabIndex = 1;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(31, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(157, 73);
            this.lblFarmacia.MostrarToolTip = true;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(334, 20);
            this.lblFarmacia.TabIndex = 25;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(89, 73);
            this.txtFarmacia.MaxLength = 4;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(62, 20);
            this.txtFarmacia.TabIndex = 2;
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmacia_KeyDown);
            this.txtFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmacia_Validating);
            // 
            // FrmReporteListadoCteProgramasVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 432);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameResultado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReporteListadoCteProgramasVenta";
            this.Text = "Reportes Administrativos Listado de Clientes-Programas atendidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReporteListadoCteProgramasVenta_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameResultado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReporte_Sheet1)).EndInit();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private FarPoint.Win.Spread.FpSpread grdReporte;
        private FarPoint.Win.Spread.SheetView grdReporte_Sheet1;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
    }
}