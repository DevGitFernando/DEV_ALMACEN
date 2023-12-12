namespace Farmacia.Vales
{
    partial class FrmGeneracionValesReembolso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneracionValesReembolso));
            FarPoint.Win.Spread.CellType.TextCellType textCellType51 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType52 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType53 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType54 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType55 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType21 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType22 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVenta = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaDeSistema = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMostrarImpresionEnPantalla = new System.Windows.Forms.CheckBox();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPrograma = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCliente = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.tmSesion = new System.Windows.Forms.Timer(this.components);
            this.lblMensajes = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(984, 25);
            this.toolStripBarraMenu.TabIndex = 4;
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar (CTRL + G)";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblPersonal);
            this.groupBox3.Controls.Add(this.txtIdPersonal);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(9, 485);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(967, 49);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información de registro de venta";
            // 
            // lblPersonal
            // 
            this.lblPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(191, 18);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(766, 21);
            this.lblPersonal.TabIndex = 9;
            this.lblPersonal.Text = "Proveedor :";
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(85, 18);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(100, 20);
            this.txtIdPersonal.TabIndex = 0;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(15, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 12);
            this.label12.TabIndex = 8;
            this.label12.Text = "Personal :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(9, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(967, 315);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Vale";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(9, 19);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(948, 284);
            this.grdProductos.TabIndex = 0;
            this.grdProductos.EditModeOn += new System.EventHandler(this.grdProductos_EditModeOn_1);
            this.grdProductos.EditModeOff += new System.EventHandler(this.grdProductos_EditModeOff_1);
            this.grdProductos.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdProductos_Advance_1);
            this.grdProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdProductos_KeyDown_1);
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 7;
            this.grdProductos_Sheet1.RowCount = 7;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 0;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 4).Locked = false;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdClaveSSA";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "IdPresentacion";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Presentación";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cantidad 1er Vale";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Solicitud 2do Vale";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            textCellType51.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType51.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType51;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 75F;
            textCellType52.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType52.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType52;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "IdClaveSSA";
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType53;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 350F;
            textCellType54.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType54.MaxLength = 15;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = textCellType54;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "IdPresentacion";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 80F;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = textCellType55;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Presentación";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 108F;
            numberCellType21.DecimalPlaces = 0;
            numberCellType21.DecimalSeparator = ".";
            numberCellType21.MaximumValue = 10000000D;
            numberCellType21.MinimumValue = 0D;
            numberCellType21.Separator = ",";
            numberCellType21.SpinDecimalIncrement = 0F;
            numberCellType21.SpinIntegerIncrement = 0F;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType21;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Cantidad 1er Vale";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 70F;
            numberCellType22.DecimalPlaces = 0;
            numberCellType22.DecimalSeparator = ".";
            numberCellType22.MaximumValue = 10000000D;
            numberCellType22.MinimumValue = 0D;
            numberCellType22.Separator = ",";
            numberCellType22.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType22;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Solicitud 2do Vale";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 70F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtVenta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpFechaDeSistema);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkMostrarImpresionEnPantalla);
            this.groupBox1.Controls.Add(this.lblSubPro);
            this.groupBox1.Controls.Add(this.txtSubPrograma);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblPro);
            this.groupBox1.Controls.Add(this.txtPrograma);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblSubCte);
            this.groupBox1.Controls.Add(this.txtSubCliente);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCte);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(967, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos generales de Venta";
            // 
            // txtVenta
            // 
            this.txtVenta.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtVenta.Decimales = 2;
            this.txtVenta.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtVenta.ForeColor = System.Drawing.Color.Black;
            this.txtVenta.Location = new System.Drawing.Point(98, 46);
            this.txtVenta.MaxLength = 8;
            this.txtVenta.Name = "txtVenta";
            this.txtVenta.PermitirApostrofo = false;
            this.txtVenta.PermitirNegativos = false;
            this.txtVenta.Size = new System.Drawing.Size(100, 20);
            this.txtVenta.TabIndex = 1;
            this.txtVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVenta.Validating += new System.ComponentModel.CancelEventHandler(this.txtVenta_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(14, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 51;
            this.label6.Text = "Folio Vale :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDeSistema
            // 
            this.dtpFechaDeSistema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaDeSistema.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDeSistema.Enabled = false;
            this.dtpFechaDeSistema.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDeSistema.Location = new System.Drawing.Point(866, 20);
            this.dtpFechaDeSistema.Name = "dtpFechaDeSistema";
            this.dtpFechaDeSistema.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaDeSistema.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(764, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Fecha de sistema :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMostrarImpresionEnPantalla
            // 
            this.chkMostrarImpresionEnPantalla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMostrarImpresionEnPantalla.Location = new System.Drawing.Point(592, 25);
            this.chkMostrarImpresionEnPantalla.Name = "chkMostrarImpresionEnPantalla";
            this.chkMostrarImpresionEnPantalla.Size = new System.Drawing.Size(170, 17);
            this.chkMostrarImpresionEnPantalla.TabIndex = 47;
            this.chkMostrarImpresionEnPantalla.Text = "Mostrar vista previa impresión";
            this.chkMostrarImpresionEnPantalla.UseVisualStyleBackColor = true;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(698, 95);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(259, 21);
            this.lblSubPro.TabIndex = 46;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPrograma
            // 
            this.txtSubPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPrograma.Decimales = 2;
            this.txtSubPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtSubPrograma.Location = new System.Drawing.Point(592, 95);
            this.txtSubPrograma.MaxLength = 4;
            this.txtSubPrograma.Name = "txtSubPrograma";
            this.txtSubPrograma.PermitirApostrofo = false;
            this.txtSubPrograma.PermitirNegativos = false;
            this.txtSubPrograma.Size = new System.Drawing.Size(100, 20);
            this.txtSubPrograma.TabIndex = 5;
            this.txtSubPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(510, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(698, 70);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(259, 21);
            this.lblPro.TabIndex = 43;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrograma
            // 
            this.txtPrograma.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPrograma.Decimales = 2;
            this.txtPrograma.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPrograma.ForeColor = System.Drawing.Color.Black;
            this.txtPrograma.Location = new System.Drawing.Point(592, 70);
            this.txtPrograma.MaxLength = 4;
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.PermitirApostrofo = false;
            this.txtPrograma.PermitirNegativos = false;
            this.txtPrograma.Size = new System.Drawing.Size(100, 20);
            this.txtPrograma.TabIndex = 4;
            this.txtPrograma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrograma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPrograma.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(525, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(204, 95);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(259, 21);
            this.lblSubCte.TabIndex = 40;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCliente
            // 
            this.txtSubCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCliente.Decimales = 2;
            this.txtSubCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCliente.ForeColor = System.Drawing.Color.Black;
            this.txtSubCliente.Location = new System.Drawing.Point(98, 95);
            this.txtSubCliente.MaxLength = 4;
            this.txtSubCliente.Name = "txtSubCliente";
            this.txtSubCliente.PermitirApostrofo = false;
            this.txtSubCliente.PermitirNegativos = false;
            this.txtSubCliente.Size = new System.Drawing.Size(100, 20);
            this.txtSubCliente.TabIndex = 3;
            this.txtSubCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown_1);
            this.txtSubCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(204, 70);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(259, 21);
            this.lblCte.TabIndex = 37;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(98, 70);
            this.txtCliente.MaxLength = 4;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCliente.TabIndex = 2;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(866, 42);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(764, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Fecha de registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(204, 22);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(98, 22);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmSesion
            // 
            this.tmSesion.Tick += new System.EventHandler(this.tmSesion_Tick);
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(0, 537);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(984, 24);
            this.lblMensajes.TabIndex = 10;
            this.lblMensajes.Text = "<F5>Ver información adicional de venta";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmGeneracionValesReembolso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.lblMensajes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGeneracionValesReembolso";
            this.Text = "Emisón de Vales Autorizacion de Compra";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGeneracionVales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGeneracionVales_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPrograma;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPrograma;
        private System.Windows.Forms.Label label9;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.Timer tmSesion;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkMostrarImpresionEnPantalla;
        private System.Windows.Forms.DateTimePicker dtpFechaDeSistema;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scTextBoxExt txtVenta;
        private System.Windows.Forms.Label label6;
    }
}