namespace DllCompras.Seguimiento
{
    partial class FrmSeguimientoOrdenesCompras
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType21 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSeguimientoOrdenesCompras));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.grdTransferencia = new FarPoint.Win.Spread.FpSpread();
            this.grdTransferencia_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActivarServicios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboEmpresas = new SC_ControlsCS.scComboBoxExt();
            this.cboEdo = new SC_ControlsCS.scComboBoxExt();
            this.cboFarmaciasDestino = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.grpFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.grpDatos.SuspendLayout();
            this.grpFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(10, 486);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1165, 49);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de Colores para Consulta de Transferencias";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.Location = new System.Drawing.Point(507, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con Éxito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.Location = new System.Drawing.Point(268, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(871, 13);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 25);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsultando
            // 
            this.lblConsultando.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(357, 13);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(25, 25);
            this.lblConsultando.TabIndex = 16;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(744, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con Errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(619, 13);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 25);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chkTodas);
            this.groupBox3.Controls.Add(this.grdTransferencia);
            this.groupBox3.Location = new System.Drawing.Point(10, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1165, 379);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Órdenes de Compra";
            // 
            // chkTodas
            // 
            this.chkTodas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTodas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.Location = new System.Drawing.Point(1033, 0);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(122, 17);
            this.chkTodas.TabIndex = 1;
            this.chkTodas.Text = " Seleccionar Todas";
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // grdTransferencia
            // 
            this.grdTransferencia.AccessibleDescription = "grdTransferencia, Sheet1, Row 0, Column 0, ";
            this.grdTransferencia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransferencia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdTransferencia.Location = new System.Drawing.Point(9, 19);
            this.grdTransferencia.Name = "grdTransferencia";
            this.grdTransferencia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdTransferencia.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdTransferencia.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdTransferencia_Sheet1});
            this.grdTransferencia.Size = new System.Drawing.Size(1144, 351);
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
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Servidor";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Folio";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Fecha";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Consultar";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Folio Entrada";
            this.grdTransferencia_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Fecha de Entrada";
            textCellType17.MaxLength = 6;
            this.grdTransferencia_Sheet1.Columns.Get(0).CellType = textCellType17;
            this.grdTransferencia_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdTransferencia_Sheet1.Columns.Get(0).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(0).Width = 62F;
            this.grdTransferencia_Sheet1.Columns.Get(1).CellType = textCellType18;
            this.grdTransferencia_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTransferencia_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdTransferencia_Sheet1.Columns.Get(1).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(1).Width = 220F;
            textCellType19.MaxLength = 500;
            this.grdTransferencia_Sheet1.Columns.Get(2).CellType = textCellType19;
            this.grdTransferencia_Sheet1.Columns.Get(2).Label = "Url";
            this.grdTransferencia_Sheet1.Columns.Get(2).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(2).Visible = false;
            this.grdTransferencia_Sheet1.Columns.Get(2).Width = 347F;
            this.grdTransferencia_Sheet1.Columns.Get(3).CellType = textCellType20;
            this.grdTransferencia_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(3).Label = "Servidor";
            this.grdTransferencia_Sheet1.Columns.Get(3).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(3).Visible = false;
            this.grdTransferencia_Sheet1.Columns.Get(4).CellType = textCellType21;
            this.grdTransferencia_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Label = "Folio";
            this.grdTransferencia_Sheet1.Columns.Get(4).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(4).Width = 88F;
            this.grdTransferencia_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Label = "Fecha";
            this.grdTransferencia_Sheet1.Columns.Get(5).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(5).Width = 95F;
            this.grdTransferencia_Sheet1.Columns.Get(6).CellType = checkBoxCellType3;
            this.grdTransferencia_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Label = "Consultar";
            this.grdTransferencia_Sheet1.Columns.Get(6).Locked = false;
            this.grdTransferencia_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(6).Width = 80F;
            this.grdTransferencia_Sheet1.Columns.Get(7).CellType = textCellType22;
            this.grdTransferencia_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Label = "Status";
            this.grdTransferencia_Sheet1.Columns.Get(7).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(7).Width = 180F;
            this.grdTransferencia_Sheet1.Columns.Get(8).CellType = textCellType23;
            this.grdTransferencia_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Label = "Folio Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(8).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(8).Width = 100F;
            this.grdTransferencia_Sheet1.Columns.Get(9).CellType = textCellType24;
            this.grdTransferencia_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Label = "Fecha de Entrada";
            this.grdTransferencia_Sheet1.Columns.Get(9).Locked = true;
            this.grdTransferencia_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTransferencia_Sheet1.Columns.Get(9).Width = 130F;
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
            this.toolStripSeparator2,
            this.btnExportar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            this.btnActivarServicios.Text = "Iniciar Seguimiento";
            this.btnActivarServicios.Click += new System.EventHandler(this.btnActivarServicios_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // grpDatos
            // 
            this.grpDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatos.Controls.Add(this.label3);
            this.grpDatos.Controls.Add(this.label2);
            this.grpDatos.Controls.Add(this.cboEmpresas);
            this.grpDatos.Controls.Add(this.cboEdo);
            this.grpDatos.Controls.Add(this.cboFarmaciasDestino);
            this.grpDatos.Controls.Add(this.label1);
            this.grpDatos.Location = new System.Drawing.Point(10, 25);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(958, 79);
            this.grpDatos.TabIndex = 1;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos de Origen";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 33;
            this.label3.Text = "Empresa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(421, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Destino :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEmpresas
            // 
            this.cboEmpresas.BackColorEnabled = System.Drawing.Color.White;
            this.cboEmpresas.Data = "";
            this.cboEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpresas.Filtro = " 1 = 1";
            this.cboEmpresas.ListaItemsBusqueda = 20;
            this.cboEmpresas.Location = new System.Drawing.Point(78, 22);
            this.cboEmpresas.MostrarToolTip = false;
            this.cboEmpresas.Name = "cboEmpresas";
            this.cboEmpresas.Size = new System.Drawing.Size(331, 21);
            this.cboEmpresas.TabIndex = 0;
            this.cboEmpresas.SelectedIndexChanged += new System.EventHandler(this.cboEmpresas_SelectedIndexChanged);
            // 
            // cboEdo
            // 
            this.cboEdo.BackColorEnabled = System.Drawing.Color.White;
            this.cboEdo.Data = "";
            this.cboEdo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdo.Filtro = " 1 = 1";
            this.cboEdo.FormattingEnabled = true;
            this.cboEdo.ListaItemsBusqueda = 20;
            this.cboEdo.Location = new System.Drawing.Point(78, 49);
            this.cboEdo.MostrarToolTip = false;
            this.cboEdo.Name = "cboEdo";
            this.cboEdo.Size = new System.Drawing.Size(331, 21);
            this.cboEdo.TabIndex = 1;
            this.cboEdo.SelectedIndexChanged += new System.EventHandler(this.cboEdo_SelectedIndexChanged);
            // 
            // cboFarmaciasDestino
            // 
            this.cboFarmaciasDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFarmaciasDestino.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmaciasDestino.Data = "";
            this.cboFarmaciasDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmaciasDestino.Filtro = " 1 = 1";
            this.cboFarmaciasDestino.FormattingEnabled = true;
            this.cboFarmaciasDestino.ListaItemsBusqueda = 20;
            this.cboFarmaciasDestino.Location = new System.Drawing.Point(478, 22);
            this.cboFarmaciasDestino.MostrarToolTip = false;
            this.cboFarmaciasDestino.Name = "cboFarmaciasDestino";
            this.cboFarmaciasDestino.Size = new System.Drawing.Size(464, 21);
            this.cboFarmaciasDestino.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Estado :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpFechas
            // 
            this.grpFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFechas.Controls.Add(this.dtpFechaFinal);
            this.grpFechas.Controls.Add(this.label4);
            this.grpFechas.Controls.Add(this.label7);
            this.grpFechas.Controls.Add(this.dtpFechaInicial);
            this.grpFechas.Location = new System.Drawing.Point(974, 25);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(201, 79);
            this.grpFechas.TabIndex = 2;
            this.grpFechas.TabStop = false;
            this.grpFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(80, 47);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(40, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fin :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(40, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Inicio :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(80, 20);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 537);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1184, 24);
            this.lblMensajes.TabIndex = 22;
            this.lblMensajes.Text = " Doble clic sobre el folio de la orden para ver sus detalles";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmSeguimientoOrdenesCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.grpFechas);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmSeguimientoOrdenesCompras";
            this.Text = "Seguimiento de Órdenes de Compras";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmSeguimientoTransferenciaSalidas_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferencia_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grpDatos.ResumeLayout(false);
            this.grpFechas.ResumeLayout(false);
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
        private FarPoint.Win.Spread.FpSpread grdTransferencia;
        private FarPoint.Win.Spread.SheetView grdTransferencia_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnActivarServicios;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.GroupBox grpFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmaciasDestino;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboEmpresas;
        private SC_ControlsCS.scComboBoxExt cboEdo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.Label lblMensajes;
    }
}