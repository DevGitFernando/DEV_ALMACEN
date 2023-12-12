namespace OficinaCentral.Transferencias
{
    partial class FrmSeguimientoTransferenciaSalidas
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType13 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType14 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType15 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType16 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeguimientoTransferenciaSalidas));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdTransferencia = new FarPoint.Win.Spread.FpSpread();
            this.grdTransferencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActivarServicios = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameSeleccion = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFarmaciasDestino = new SC_ControlsCS.scComboBoxExt();
            this.FrameTodas = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameSeleccion.SuspendLayout();
            this.FrameTodas.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(12, 503);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1013, 52);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de colores para consulta de transferencias";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(431, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con exito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(192, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(795, 18);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 25);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsultando
            // 
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(281, 19);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(25, 25);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(668, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(543, 18);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 25);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdTransferencia);
            this.groupBox3.Location = new System.Drawing.Point(12, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1013, 362);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transferencias";
            // 
            // grdTransferencia
            // 
            this.grdTransferencia.AccessibleDescription = "grdTransferencia, Sheet1, Row 0, Column 0, ";
            this.grdTransferencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdTransferencia.Location = new System.Drawing.Point(11, 15);
            this.grdTransferencia.Name = "grdTransferencia";
            this.grdTransferencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdTransferencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdTransferencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdTransferencia_Sheet1});
            this.grdTransferencia.Size = new System.Drawing.Size(994, 341);
            this.grdTransferencia.TabIndex = 0;
            this.grdTransferencia.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdTransferencia_CellDoubleClick);
            // 
            // grdTransferencia_Sheet1
            // 
            this.grdTransferencia_Sheet1.Reset();
            this.grdTransferencia_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdTransferencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdTransferencia_Sheet1.ColumnCount = 10;
            this.grdTransferencia_Sheet1.RowCount = 15;
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEmrpesa";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Url";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Folio";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Consultar";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Folio Entrada";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha de Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(0).CellType = textCellType9;
            this.grdTransferencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Label = "IdEmrpesa";
            this.grdTransferencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Visible = false;
            textCellType10.MaxLength = 6;
            this.grdTransferencia_Sheet1.Columns.Get(1).CellType = textCellType10;
            this.grdTransferencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdTransferencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(1).Width = 62F;
            this.grdTransferencia_Sheet1.Columns.Get(2).CellType = textCellType11;
            this.grdTransferencia_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTransferencia_Sheet1.Columns.Get(2).Label = "Nombre";
            this.grdTransferencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(2).Width = 220F;
            textCellType12.MaxLength = 500;
            this.grdTransferencia_Sheet1.Columns.Get(3).CellType = textCellType12;
            this.grdTransferencia_Sheet1.Columns.Get(3).Label = "Url";
            this.grdTransferencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(3).Visible = false;
            this.grdTransferencia_Sheet1.Columns.Get(3).Width = 347F;
            this.grdTransferencia_Sheet1.Columns.Get(4).CellType = textCellType13;
            this.grdTransferencia_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Label = "Folio";
            this.grdTransferencia_Sheet1.Columns.Get(4).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Width = 90F;
            this.grdTransferencia_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Label = "Fecha";
            this.grdTransferencia_Sheet1.Columns.Get(5).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Width = 95F;
            this.grdTransferencia_Sheet1.Columns.Get(6).CellType = checkBoxCellType2;
            this.grdTransferencia_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Label = "Consultar";
            this.grdTransferencia_Sheet1.Columns.Get(6).Locked = false;
            this.grdTransferencia_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Width = 80F;
            this.grdTransferencia_Sheet1.Columns.Get(7).CellType = textCellType14;
            this.grdTransferencia_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Label = "Status";
            this.grdTransferencia_Sheet1.Columns.Get(7).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Width = 180F;
            this.grdTransferencia_Sheet1.Columns.Get(8).CellType = textCellType15;
            this.grdTransferencia_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Label = "Folio Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(8).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Width = 90F;
            this.grdTransferencia_Sheet1.Columns.Get(9).CellType = textCellType16;
            this.grdTransferencia_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Label = "Fecha de Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(9).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Width = 120F;
            this.grdTransferencia_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdTransferencia_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnActivarServicios,
            this.toolStripSeparator1,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1034, 25);
            this.toolStripBarraMenu.TabIndex = 19;
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
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click_1);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnActivarServicios
            // 
            this.btnActivarServicios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActivarServicios.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarServicios.Image")));
            this.btnActivarServicios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActivarServicios.Name = "btnActivarServicios";
            this.btnActivarServicios.Size = new System.Drawing.Size(23, 22);
            this.btnActivarServicios.Text = "toolStripButton1";
            this.btnActivarServicios.Click += new System.EventHandler(this.btnActivarServicios_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(213, 15);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(383, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(150, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(213, 42);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(383, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(106, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Farmacia origen :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameSeleccion
            // 
            this.FrameSeleccion.Controls.Add(this.label2);
            this.FrameSeleccion.Controls.Add(this.cboFarmaciasDestino);
            this.FrameSeleccion.Controls.Add(this.cboEstados);
            this.FrameSeleccion.Controls.Add(this.label1);
            this.FrameSeleccion.Controls.Add(this.label3);
            this.FrameSeleccion.Controls.Add(this.cboFarmacias);
            this.FrameSeleccion.Location = new System.Drawing.Point(12, 28);
            this.FrameSeleccion.Name = "FrameSeleccion";
            this.FrameSeleccion.Size = new System.Drawing.Size(718, 105);
            this.FrameSeleccion.TabIndex = 0;
            this.FrameSeleccion.TabStop = false;
            this.FrameSeleccion.Text = "Datos de Origen";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(106, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Farmacia destino :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmaciasDestino
            // 
            this.cboFarmaciasDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmaciasDestino.Data = "";
            this.cboFarmaciasDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmaciasDestino.Filtro = " 1 = 1";
            this.cboFarmaciasDestino.FormattingEnabled = true;
            this.cboFarmaciasDestino.Location = new System.Drawing.Point(213, 69);
            this.cboFarmaciasDestino.MostrarToolTip = false;
            this.cboFarmaciasDestino.Name = "cboFarmaciasDestino";
            this.cboFarmaciasDestino.Size = new System.Drawing.Size(383, 21);
            this.cboFarmaciasDestino.TabIndex = 2;
            // 
            // FrameTodas
            // 
            this.FrameTodas.Controls.Add(this.chkTodas);
            this.FrameTodas.Location = new System.Drawing.Point(736, 90);
            this.FrameTodas.Name = "FrameTodas";
            this.FrameTodas.Size = new System.Drawing.Size(286, 43);
            this.FrameTodas.TabIndex = 30;
            this.FrameTodas.TabStop = false;
            // 
            // chkTodas
            // 
            this.chkTodas.Location = new System.Drawing.Point(152, 14);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(118, 24);
            this.chkTodas.TabIndex = 29;
            this.chkTodas.Text = " Seleccionar Todas";
            this.chkTodas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label4);
            this.FrameFechas.Controls.Add(this.label7);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(736, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(286, 63);
            this.FrameFechas.TabIndex = 28;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(188, 29);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(161, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fin :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Inicio :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(66, 29);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 3;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
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
            // FrmSeguimientoTransferenciaSalidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 565);
            this.Controls.Add(this.FrameTodas);
            this.Controls.Add(this.FrameSeleccion);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmSeguimientoTransferenciaSalidas";
            this.Text = "Seguimiento de Transferencia de Salida";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameSeleccion.ResumeLayout(false);
            this.FrameTodas.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnActivarServicios;
        private System.Windows.Forms.Timer tmEjecuciones;
        private FarPoint.Win.Spread.FpSpread grdTransferencia;
        private FarPoint.Win.Spread.SheetView grdTransferencia_Sheet1;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameSeleccion;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmaciasDestino;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.GroupBox FrameTodas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
    }
}