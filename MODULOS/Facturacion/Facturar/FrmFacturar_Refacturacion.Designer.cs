namespace Facturacion.Facturar
{
    partial class FrmFacturar_Refacturacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturar_Refacturacion));
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDetalles = new System.Windows.Forms.GroupBox();
            this.scLabelExt2 = new SC_ControlsCS.scLabelExt();
            this.lblTotal_Seleccionado = new SC_ControlsCS.scLabelExt();
            this.scLabelExt1 = new SC_ControlsCS.scLabelExt();
            this.lblRemisiones_Seleccionadas = new SC_ControlsCS.scLabelExt();
            this.chkMarcarDesmarcarTodo = new System.Windows.Forms.CheckBox();
            this.FrameProceso = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.grdRemisiones = new FarPoint.Win.Spread.FpSpread();
            this.grdRemisiones_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.scLabelExt4 = new SC_ControlsCS.scLabelExt();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotal_CFDI = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.lblSubTotalGrabado = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chkTieneDocumentoRelacionado = new System.Windows.Forms.CheckBox();
            this.txtRelacion__UUID = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelacion__Serie = new SC_ControlsCS.scTextBoxExt();
            this.txtRelacion__Folio = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDetalles.SuspendLayout();
            this.FrameProceso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnFacturar,
            this.toolStripSeparator2,
            this.toolStripSeparator3});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1234, 25);
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
            this.btnEjecutar.Text = "Consultar remisiones sin facturar";
            this.btnEjecutar.ToolTipText = "Consultar remisiones sin facturar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFacturar
            // 
            this.btnFacturar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFacturar.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturar.Image")));
            this.btnFacturar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(23, 22);
            this.btnFacturar.Text = "Generar facturas electrónicas";
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDetalles
            // 
            this.FrameDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDetalles.Controls.Add(this.scLabelExt2);
            this.FrameDetalles.Controls.Add(this.lblTotal_Seleccionado);
            this.FrameDetalles.Controls.Add(this.scLabelExt1);
            this.FrameDetalles.Controls.Add(this.lblRemisiones_Seleccionadas);
            this.FrameDetalles.Controls.Add(this.chkMarcarDesmarcarTodo);
            this.FrameDetalles.Controls.Add(this.FrameProceso);
            this.FrameDetalles.Controls.Add(this.grdRemisiones);
            this.FrameDetalles.Controls.Add(this.scLabelExt4);
            this.FrameDetalles.Controls.Add(this.lblTotal);
            this.FrameDetalles.Location = new System.Drawing.Point(12, 175);
            this.FrameDetalles.Name = "FrameDetalles";
            this.FrameDetalles.Size = new System.Drawing.Size(1214, 454);
            this.FrameDetalles.TabIndex = 2;
            this.FrameDetalles.TabStop = false;
            this.FrameDetalles.Text = "Remisiones";
            // 
            // scLabelExt2
            // 
            this.scLabelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt2.Location = new System.Drawing.Point(359, 419);
            this.scLabelExt2.MostrarToolTip = false;
            this.scLabelExt2.Name = "scLabelExt2";
            this.scLabelExt2.Size = new System.Drawing.Size(209, 22);
            this.scLabelExt2.TabIndex = 4;
            this.scLabelExt2.Text = "Importe remisiones seleccionadas :";
            this.scLabelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal_Seleccionado
            // 
            this.lblTotal_Seleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal_Seleccionado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal_Seleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_Seleccionado.Location = new System.Drawing.Point(570, 419);
            this.lblTotal_Seleccionado.MostrarToolTip = false;
            this.lblTotal_Seleccionado.Name = "lblTotal_Seleccionado";
            this.lblTotal_Seleccionado.Size = new System.Drawing.Size(129, 22);
            this.lblTotal_Seleccionado.TabIndex = 5;
            this.lblTotal_Seleccionado.Text = "scLabelExt3";
            this.lblTotal_Seleccionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scLabelExt1
            // 
            this.scLabelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt1.Location = new System.Drawing.Point(10, 419);
            this.scLabelExt1.MostrarToolTip = false;
            this.scLabelExt1.Name = "scLabelExt1";
            this.scLabelExt1.Size = new System.Drawing.Size(167, 22);
            this.scLabelExt1.TabIndex = 2;
            this.scLabelExt1.Text = "Remisiones seleccionadas :";
            this.scLabelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemisiones_Seleccionadas
            // 
            this.lblRemisiones_Seleccionadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRemisiones_Seleccionadas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRemisiones_Seleccionadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemisiones_Seleccionadas.Location = new System.Drawing.Point(180, 419);
            this.lblRemisiones_Seleccionadas.MostrarToolTip = false;
            this.lblRemisiones_Seleccionadas.Name = "lblRemisiones_Seleccionadas";
            this.lblRemisiones_Seleccionadas.Size = new System.Drawing.Size(129, 22);
            this.lblRemisiones_Seleccionadas.TabIndex = 3;
            this.lblRemisiones_Seleccionadas.Text = "scLabelExt3";
            this.lblRemisiones_Seleccionadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMarcarDesmarcarTodo
            // 
            this.chkMarcarDesmarcarTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMarcarDesmarcarTodo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.Location = new System.Drawing.Point(1019, 0);
            this.chkMarcarDesmarcarTodo.Name = "chkMarcarDesmarcarTodo";
            this.chkMarcarDesmarcarTodo.Size = new System.Drawing.Size(181, 17);
            this.chkMarcarDesmarcarTodo.TabIndex = 1;
            this.chkMarcarDesmarcarTodo.Text = "Marcar - Desmarcar todo";
            this.chkMarcarDesmarcarTodo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMarcarDesmarcarTodo.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcarTodo.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcarTodo_CheckedChanged);
            // 
            // FrameProceso
            // 
            this.FrameProceso.Controls.Add(this.pgBar);
            this.FrameProceso.Location = new System.Drawing.Point(230, 170);
            this.FrameProceso.Name = "FrameProceso";
            this.FrameProceso.Size = new System.Drawing.Size(710, 50);
            this.FrameProceso.TabIndex = 1;
            this.FrameProceso.TabStop = false;
            this.FrameProceso.Text = "Procesando";
            // 
            // pgBar
            // 
            this.pgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBar.ForeColor = System.Drawing.Color.Maroon;
            this.pgBar.Location = new System.Drawing.Point(13, 23);
            this.pgBar.MarqueeAnimationSpeed = 50;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(680, 13);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 0;
            // 
            // grdRemisiones
            // 
            this.grdRemisiones.AccessibleDescription = "grdRemisiones, Sheet1, Row 0, Column 0, ";
            this.grdRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRemisiones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdRemisiones.Location = new System.Drawing.Point(10, 19);
            this.grdRemisiones.Name = "grdRemisiones";
            this.grdRemisiones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdRemisiones.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdRemisiones_Sheet1});
            this.grdRemisiones.Size = new System.Drawing.Size(1196, 394);
            this.grdRemisiones.TabIndex = 0;
            this.grdRemisiones.EditModeOff += new System.EventHandler(this.grdRemisiones_EditModeOff);
            this.grdRemisiones.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdRemisiones_ButtonClicked);
            // 
            // grdRemisiones_Sheet1
            // 
            this.grdRemisiones_Sheet1.Reset();
            this.grdRemisiones_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdRemisiones_Sheet1.ColumnCount = 9;
            this.grdRemisiones_Sheet1.RowCount = 14;
            this.grdRemisiones_Sheet1.Cells.Get(0, 3).Value = "2018-09-01";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha de remisionado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Importe remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha inicial procesado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Fecha final procesado";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Farmacia";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Tipo de remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Tipo de remisión";
            this.grdRemisiones_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Facturar";
            this.grdRemisiones_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdRemisiones_Sheet1.Columns.Get(0).CellType = textCellType7;
            this.grdRemisiones_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Label = "Remisión";
            this.grdRemisiones_Sheet1.Columns.Get(0).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(0).Width = 105F;
            this.grdRemisiones_Sheet1.Columns.Get(1).CellType = textCellType8;
            this.grdRemisiones_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(1).Label = "Fecha de remisionado";
            this.grdRemisiones_Sheet1.Columns.Get(1).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(1).Width = 80F;
            numberCellType3.DecimalPlaces = 2;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 999999999999.99D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdRemisiones_Sheet1.Columns.Get(2).CellType = numberCellType3;
            this.grdRemisiones_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdRemisiones_Sheet1.Columns.Get(2).Label = "Importe remisión";
            this.grdRemisiones_Sheet1.Columns.Get(2).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(2).Width = 120F;
            this.grdRemisiones_Sheet1.Columns.Get(3).CellType = textCellType9;
            this.grdRemisiones_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(3).Label = "Fecha inicial procesado";
            this.grdRemisiones_Sheet1.Columns.Get(3).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(3).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(4).CellType = textCellType10;
            this.grdRemisiones_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(4).Label = "Fecha final procesado";
            this.grdRemisiones_Sheet1.Columns.Get(4).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(4).Width = 80F;
            this.grdRemisiones_Sheet1.Columns.Get(5).CellType = textCellType11;
            this.grdRemisiones_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(5).Label = "Farmacia";
            this.grdRemisiones_Sheet1.Columns.Get(5).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(5).Width = 270F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 99D;
            numberCellType4.MinimumValue = 0D;
            this.grdRemisiones_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdRemisiones_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(6).Label = "Tipo de remisión";
            this.grdRemisiones_Sheet1.Columns.Get(6).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(6).Visible = false;
            this.grdRemisiones_Sheet1.Columns.Get(6).Width = 50F;
            this.grdRemisiones_Sheet1.Columns.Get(7).CellType = textCellType12;
            this.grdRemisiones_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdRemisiones_Sheet1.Columns.Get(7).Label = "Tipo de remisión";
            this.grdRemisiones_Sheet1.Columns.Get(7).Locked = true;
            this.grdRemisiones_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(7).Width = 160F;
            this.grdRemisiones_Sheet1.Columns.Get(8).CellType = checkBoxCellType2;
            this.grdRemisiones_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(8).Label = "Facturar";
            this.grdRemisiones_Sheet1.Columns.Get(8).Locked = false;
            this.grdRemisiones_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdRemisiones_Sheet1.Columns.Get(8).Width = 65F;
            this.grdRemisiones_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdRemisiones_Sheet1.Rows.Default.Height = 25F;
            this.grdRemisiones_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // scLabelExt4
            // 
            this.scLabelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scLabelExt4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scLabelExt4.Location = new System.Drawing.Point(926, 419);
            this.scLabelExt4.MostrarToolTip = false;
            this.scLabelExt4.Name = "scLabelExt4";
            this.scLabelExt4.Size = new System.Drawing.Size(137, 22);
            this.scLabelExt4.TabIndex = 6;
            this.scLabelExt4.Text = "Total :";
            this.scLabelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(1069, 419);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(137, 22);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "scLabelExt3";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Cliente :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(85, 99);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(391, 21);
            this.lblCliente.TabIndex = 3;
            this.lblCliente.Text = "Serie : ";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.chkTieneDocumentoRelacionado);
            this.groupBox4.Controls.Add(this.lblCliente);
            this.groupBox4.Controls.Add(this.txtRelacion__UUID);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtRelacion__Serie);
            this.groupBox4.Controls.Add(this.txtRelacion__Folio);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(11, 27);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(1215, 143);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información CFDI relacionado";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotal_CFDI);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblIva);
            this.groupBox2.Controls.Add(this.lblSubTotalGrabado);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(887, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 106);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Importes de Factura";
            // 
            // lblTotal_CFDI
            // 
            this.lblTotal_CFDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal_CFDI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal_CFDI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal_CFDI.Location = new System.Drawing.Point(132, 74);
            this.lblTotal_CFDI.Name = "lblTotal_CFDI";
            this.lblTotal_CFDI.Size = new System.Drawing.Size(170, 20);
            this.lblTotal_CFDI.TabIndex = 2;
            this.lblTotal_CFDI.Text = "Núm. Cotización :";
            this.lblTotal_CFDI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Location = new System.Drawing.Point(19, 76);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 16);
            this.label15.TabIndex = 52;
            this.label15.Text = "Total : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.Location = new System.Drawing.Point(19, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 16);
            this.label11.TabIndex = 50;
            this.label11.Text = "Sub-Total : ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(132, 49);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(170, 20);
            this.lblIva.TabIndex = 1;
            this.lblIva.Text = "Núm. Cotización :";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalGrabado
            // 
            this.lblSubTotalGrabado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotalGrabado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalGrabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalGrabado.Location = new System.Drawing.Point(132, 23);
            this.lblSubTotalGrabado.Name = "lblSubTotalGrabado";
            this.lblSubTotalGrabado.Size = new System.Drawing.Size(170, 20);
            this.lblSubTotalGrabado.TabIndex = 0;
            this.lblSubTotalGrabado.Text = "Núm. Cotización :";
            this.lblSubTotalGrabado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(19, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 16);
            this.label13.TabIndex = 51;
            this.label13.Text = "Iva : ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTieneDocumentoRelacionado
            // 
            this.chkTieneDocumentoRelacionado.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.Checked = true;
            this.chkTieneDocumentoRelacionado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTieneDocumentoRelacionado.Location = new System.Drawing.Point(1020, 0);
            this.chkTieneDocumentoRelacionado.Margin = new System.Windows.Forms.Padding(2);
            this.chkTieneDocumentoRelacionado.Name = "chkTieneDocumentoRelacionado";
            this.chkTieneDocumentoRelacionado.Size = new System.Drawing.Size(181, 20);
            this.chkTieneDocumentoRelacionado.TabIndex = 4;
            this.chkTieneDocumentoRelacionado.Text = "Sustitución de CFDI previo";
            this.chkTieneDocumentoRelacionado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTieneDocumentoRelacionado.UseVisualStyleBackColor = true;
            // 
            // txtRelacion__UUID
            // 
            this.txtRelacion__UUID.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__UUID.Decimales = 2;
            this.txtRelacion__UUID.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__UUID.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__UUID.Location = new System.Drawing.Point(85, 71);
            this.txtRelacion__UUID.MaxLength = 10;
            this.txtRelacion__UUID.Name = "txtRelacion__UUID";
            this.txtRelacion__UUID.PermitirApostrofo = false;
            this.txtRelacion__UUID.PermitirNegativos = false;
            this.txtRelacion__UUID.Size = new System.Drawing.Size(391, 20);
            this.txtRelacion__UUID.TabIndex = 2;
            this.txtRelacion__UUID.Text = "0123456789012345678901234567890123456789";
            this.txtRelacion__UUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "UUID : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRelacion__Serie
            // 
            this.txtRelacion__Serie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRelacion__Serie.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Serie.Decimales = 2;
            this.txtRelacion__Serie.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRelacion__Serie.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Serie.Location = new System.Drawing.Point(85, 22);
            this.txtRelacion__Serie.MaxLength = 10;
            this.txtRelacion__Serie.Name = "txtRelacion__Serie";
            this.txtRelacion__Serie.PermitirApostrofo = false;
            this.txtRelacion__Serie.PermitirNegativos = false;
            this.txtRelacion__Serie.Size = new System.Drawing.Size(98, 20);
            this.txtRelacion__Serie.TabIndex = 0;
            this.txtRelacion__Serie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRelacion__Serie.TextChanged += new System.EventHandler(this.txtRelacion__Serie_TextChanged);
            this.txtRelacion__Serie.Validating += new System.ComponentModel.CancelEventHandler(this.txtRelacion__Serie_Validating);
            // 
            // txtRelacion__Folio
            // 
            this.txtRelacion__Folio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRelacion__Folio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRelacion__Folio.Decimales = 2;
            this.txtRelacion__Folio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtRelacion__Folio.ForeColor = System.Drawing.Color.Black;
            this.txtRelacion__Folio.Location = new System.Drawing.Point(85, 45);
            this.txtRelacion__Folio.MaxLength = 10;
            this.txtRelacion__Folio.Name = "txtRelacion__Folio";
            this.txtRelacion__Folio.PermitirApostrofo = false;
            this.txtRelacion__Folio.PermitirNegativos = false;
            this.txtRelacion__Folio.Size = new System.Drawing.Size(98, 20);
            this.txtRelacion__Folio.TabIndex = 1;
            this.txtRelacion__Folio.Text = "0123456789";
            this.txtRelacion__Folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRelacion__Folio.TextChanged += new System.EventHandler(this.txtRelacion__Folio_TextChanged);
            this.txtRelacion__Folio.Validating += new System.ComponentModel.CancelEventHandler(this.txtRelacion__Folio_Validating);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Serie : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Folio : ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmFacturar_Refacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 637);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameDetalles);
            this.Name = "FrmFacturar_Refacturacion";
            this.Text = "Refacturación de CFDIs";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFacturar_Refacturacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDetalles.ResumeLayout(false);
            this.FrameProceso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRemisiones_Sheet1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFacturar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox FrameDetalles;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcarTodo;
        private System.Windows.Forms.GroupBox FrameProceso;
        private System.Windows.Forms.ProgressBar pgBar;
        private FarPoint.Win.Spread.FpSpread grdRemisiones;
        private FarPoint.Win.Spread.SheetView grdRemisiones_Sheet1;
        private SC_ControlsCS.scLabelExt scLabelExt4;
        private SC_ControlsCS.scLabelExt lblTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private SC_ControlsCS.scLabelExt scLabelExt1;
        private SC_ControlsCS.scLabelExt lblRemisiones_Seleccionadas;
        private SC_ControlsCS.scLabelExt scLabelExt2;
        private SC_ControlsCS.scLabelExt lblTotal_Seleccionado;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scLabelExt lblCliente;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkTieneDocumentoRelacionado;
        private SC_ControlsCS.scTextBoxExt txtRelacion__UUID;
        private System.Windows.Forms.Label label7;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Serie;
        private SC_ControlsCS.scTextBoxExt txtRelacion__Folio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTotal_CFDI;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label lblSubTotalGrabado;
        private System.Windows.Forms.Label label13;
    }
}