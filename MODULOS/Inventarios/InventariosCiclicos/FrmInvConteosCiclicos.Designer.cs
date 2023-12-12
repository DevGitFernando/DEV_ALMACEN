namespace Inventarios.InventariosCiclicos
{
    partial class FrmInvConteosCiclicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvConteosCiclicos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCerrarFolio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdo_Insumo_Todo = new System.Windows.Forms.RadioButton();
            this.rdo_Insumo_MaterialDeCuracion = new System.Windows.Forms.RadioButton();
            this.rdo_Insumo_Medicamento = new System.Windows.Forms.RadioButton();
            this.FrameUbicaciones = new System.Windows.Forms.GroupBox();
            this.rdo_U_Todo = new System.Windows.Forms.RadioButton();
            this.rdo_U_Almacenaje = new System.Windows.Forms.RadioButton();
            this.rdo_U_Picking = new System.Windows.Forms.RadioButton();
            this.lblPersonal = new System.Windows.Forms.Label();
            this.txtIdPersonal = new SC_ControlsCS.scTextBoxExt();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nmNumClaves = new System.Windows.Forms.NumericUpDown();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolioConteo = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFolioCiclico = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdDetalles = new FarPoint.Win.Spread.FpSpread();
            this.grdDetalles_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEncabezado.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameUbicaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumClaves)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator6,
            this.btnEjecutar,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCerrarFolio,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1344, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "Generar Folio";
            this.btnEjecutar.ToolTipText = "Generar Folio";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar inventario cicliclo";
            this.btnGuardar.ToolTipText = "Guarda Inventario Ciclico";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCerrarFolio
            // 
            this.btnCerrarFolio.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCerrarFolio.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarFolio.Image")));
            this.btnCerrarFolio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrarFolio.Name = "btnCerrarFolio";
            this.btnCerrarFolio.Size = new System.Drawing.Size(23, 22);
            this.btnCerrarFolio.Text = "Cerrar folio";
            this.btnCerrarFolio.ToolTipText = "Cerrar folio";
            this.btnCerrarFolio.Click += new System.EventHandler(this.btnCerrarFolio_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.groupBox1);
            this.FrameEncabezado.Controls.Add(this.FrameUbicaciones);
            this.FrameEncabezado.Controls.Add(this.lblPersonal);
            this.FrameEncabezado.Controls.Add(this.txtIdPersonal);
            this.FrameEncabezado.Controls.Add(this.label12);
            this.FrameEncabezado.Controls.Add(this.label4);
            this.FrameEncabezado.Controls.Add(this.nmNumClaves);
            this.FrameEncabezado.Controls.Add(this.lblCancelado);
            this.FrameEncabezado.Controls.Add(this.txtFolioConteo);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.txtFolioCiclico);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(8, 27);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(1327, 106);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos Inventario Ciclico";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdo_Insumo_Todo);
            this.groupBox1.Controls.Add(this.rdo_Insumo_MaterialDeCuracion);
            this.groupBox1.Controls.Add(this.rdo_Insumo_Medicamento);
            this.groupBox1.Location = new System.Drawing.Point(1034, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 57);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipos de insumos";
            // 
            // rdo_Insumo_Todo
            // 
            this.rdo_Insumo_Todo.Location = new System.Drawing.Point(14, 15);
            this.rdo_Insumo_Todo.Name = "rdo_Insumo_Todo";
            this.rdo_Insumo_Todo.Size = new System.Drawing.Size(262, 20);
            this.rdo_Insumo_Todo.TabIndex = 0;
            this.rdo_Insumo_Todo.TabStop = true;
            this.rdo_Insumo_Todo.Text = "Medicamento y Material de curación";
            this.rdo_Insumo_Todo.UseVisualStyleBackColor = true;
            // 
            // rdo_Insumo_MaterialDeCuracion
            // 
            this.rdo_Insumo_MaterialDeCuracion.Location = new System.Drawing.Point(148, 36);
            this.rdo_Insumo_MaterialDeCuracion.Name = "rdo_Insumo_MaterialDeCuracion";
            this.rdo_Insumo_MaterialDeCuracion.Size = new System.Drawing.Size(128, 20);
            this.rdo_Insumo_MaterialDeCuracion.TabIndex = 2;
            this.rdo_Insumo_MaterialDeCuracion.TabStop = true;
            this.rdo_Insumo_MaterialDeCuracion.Text = "Material de curación";
            this.rdo_Insumo_MaterialDeCuracion.UseVisualStyleBackColor = true;
            // 
            // rdo_Insumo_Medicamento
            // 
            this.rdo_Insumo_Medicamento.Location = new System.Drawing.Point(14, 36);
            this.rdo_Insumo_Medicamento.Name = "rdo_Insumo_Medicamento";
            this.rdo_Insumo_Medicamento.Size = new System.Drawing.Size(128, 20);
            this.rdo_Insumo_Medicamento.TabIndex = 1;
            this.rdo_Insumo_Medicamento.TabStop = true;
            this.rdo_Insumo_Medicamento.Text = "Medicamento";
            this.rdo_Insumo_Medicamento.UseVisualStyleBackColor = true;
            // 
            // FrameUbicaciones
            // 
            this.FrameUbicaciones.Controls.Add(this.rdo_U_Todo);
            this.FrameUbicaciones.Controls.Add(this.rdo_U_Almacenaje);
            this.FrameUbicaciones.Controls.Add(this.rdo_U_Picking);
            this.FrameUbicaciones.Location = new System.Drawing.Point(743, 39);
            this.FrameUbicaciones.Name = "FrameUbicaciones";
            this.FrameUbicaciones.Size = new System.Drawing.Size(285, 57);
            this.FrameUbicaciones.TabIndex = 4;
            this.FrameUbicaciones.TabStop = false;
            this.FrameUbicaciones.Text = "Tipos de ubicaciones";
            // 
            // rdo_U_Todo
            // 
            this.rdo_U_Todo.Location = new System.Drawing.Point(14, 15);
            this.rdo_U_Todo.Name = "rdo_U_Todo";
            this.rdo_U_Todo.Size = new System.Drawing.Size(128, 20);
            this.rdo_U_Todo.TabIndex = 0;
            this.rdo_U_Todo.TabStop = true;
            this.rdo_U_Todo.Text = "Pickeo y Almacenaje";
            this.rdo_U_Todo.UseVisualStyleBackColor = true;
            // 
            // rdo_U_Almacenaje
            // 
            this.rdo_U_Almacenaje.Location = new System.Drawing.Point(148, 36);
            this.rdo_U_Almacenaje.Name = "rdo_U_Almacenaje";
            this.rdo_U_Almacenaje.Size = new System.Drawing.Size(128, 20);
            this.rdo_U_Almacenaje.TabIndex = 2;
            this.rdo_U_Almacenaje.TabStop = true;
            this.rdo_U_Almacenaje.Text = "Exclusivo almacenaje";
            this.rdo_U_Almacenaje.UseVisualStyleBackColor = true;
            // 
            // rdo_U_Picking
            // 
            this.rdo_U_Picking.Location = new System.Drawing.Point(14, 36);
            this.rdo_U_Picking.Name = "rdo_U_Picking";
            this.rdo_U_Picking.Size = new System.Drawing.Size(128, 20);
            this.rdo_U_Picking.TabIndex = 1;
            this.rdo_U_Picking.TabStop = true;
            this.rdo_U_Picking.Text = "Exclusivo pickeo";
            this.rdo_U_Picking.UseVisualStyleBackColor = true;
            // 
            // lblPersonal
            // 
            this.lblPersonal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPersonal.Location = new System.Drawing.Point(252, 75);
            this.lblPersonal.Name = "lblPersonal";
            this.lblPersonal.Size = new System.Drawing.Size(485, 21);
            this.lblPersonal.TabIndex = 38;
            this.lblPersonal.Text = "Proveedor :";
            this.lblPersonal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdPersonal
            // 
            this.txtIdPersonal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdPersonal.Decimales = 2;
            this.txtIdPersonal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdPersonal.ForeColor = System.Drawing.Color.Black;
            this.txtIdPersonal.Location = new System.Drawing.Point(142, 75);
            this.txtIdPersonal.Name = "txtIdPersonal";
            this.txtIdPersonal.PermitirApostrofo = false;
            this.txtIdPersonal.PermitirNegativos = false;
            this.txtIdPersonal.Size = new System.Drawing.Size(100, 20);
            this.txtIdPersonal.TabIndex = 2;
            this.txtIdPersonal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdPersonal.TextChanged += new System.EventHandler(this.txtIdPersonal_TextChanged);
            this.txtIdPersonal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdPersonal_KeyDown);
            this.txtIdPersonal.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdPersonal_Validating);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 76);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 18);
            this.label12.TabIndex = 37;
            this.label12.Text = "Personal asignado :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(411, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "Número de claves a seleccionar para conteo :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmNumClaves
            // 
            this.nmNumClaves.Location = new System.Drawing.Point(668, 50);
            this.nmNumClaves.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nmNumClaves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmNumClaves.Name = "nmNumClaves";
            this.nmNumClaves.Size = new System.Drawing.Size(69, 20);
            this.nmNumClaves.TabIndex = 3;
            this.nmNumClaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmNumClaves.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(252, 48);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(98, 20);
            this.lblCancelado.TabIndex = 33;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolioConteo
            // 
            this.txtFolioConteo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioConteo.Decimales = 2;
            this.txtFolioConteo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioConteo.ForeColor = System.Drawing.Color.Black;
            this.txtFolioConteo.Location = new System.Drawing.Point(142, 48);
            this.txtFolioConteo.MaxLength = 8;
            this.txtFolioConteo.Name = "txtFolioConteo";
            this.txtFolioConteo.PermitirApostrofo = false;
            this.txtFolioConteo.PermitirNegativos = false;
            this.txtFolioConteo.Size = new System.Drawing.Size(100, 20);
            this.txtFolioConteo.TabIndex = 1;
            this.txtFolioConteo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioConteo.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioConteo_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Folio Conteo :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(1226, 15);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1130, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioCiclico
            // 
            this.txtFolioCiclico.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioCiclico.Decimales = 2;
            this.txtFolioCiclico.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioCiclico.ForeColor = System.Drawing.Color.Black;
            this.txtFolioCiclico.Location = new System.Drawing.Point(142, 20);
            this.txtFolioCiclico.MaxLength = 8;
            this.txtFolioCiclico.Name = "txtFolioCiclico";
            this.txtFolioCiclico.PermitirApostrofo = false;
            this.txtFolioCiclico.PermitirNegativos = false;
            this.txtFolioCiclico.Size = new System.Drawing.Size(100, 20);
            this.txtFolioCiclico.TabIndex = 0;
            this.txtFolioCiclico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio Ciclico :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdDetalles);
            this.groupBox2.Location = new System.Drawing.Point(8, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1327, 470);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Claves";
            // 
            // grdDetalles
            // 
            this.grdDetalles.AccessibleDescription = "grdDetalles, Sheet1, Row 0, Column 0, ";
            this.grdDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDetalles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grdDetalles.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdDetalles.Location = new System.Drawing.Point(4, 19);
            this.grdDetalles.Name = "grdDetalles";
            this.grdDetalles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDetalles.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDetalles_Sheet1});
            this.grdDetalles.Size = new System.Drawing.Size(1317, 445);
            this.grdDetalles.TabIndex = 0;
            this.grdDetalles.EditModeOff += new System.EventHandler(this.grdDetalles_EditModeOff);
            // 
            // grdDetalles_Sheet1
            // 
            this.grdDetalles_Sheet1.Reset();
            this.grdDetalles_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDetalles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDetalles_Sheet1.ColumnCount = 14;
            this.grdDetalles_Sheet1.RowCount = 4;
            this.grdDetalles_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Cells.Get(0, 3).Locked = true;
            this.grdDetalles_Sheet1.Cells.Get(0, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Cells.Get(1, 4).Locked = false;
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Clave SSA";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción Clave";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Producto";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Código EAN";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descripción";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Presentación";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Lote";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Rack";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Nivel";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Entrepaño";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Existencia Sistema";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = "Cantidad";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = "Observaciones";
            this.grdDetalles_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = "Validado";
            this.grdDetalles_Sheet1.ColumnHeader.Rows.Get(0).Height = 54F;
            this.grdDetalles_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdDetalles_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(0).Label = "Clave SSA";
            this.grdDetalles_Sheet1.Columns.Get(0).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(0).Width = 100F;
            this.grdDetalles_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdDetalles_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalles_Sheet1.Columns.Get(1).Label = "Descripción Clave";
            this.grdDetalles_Sheet1.Columns.Get(1).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(1).Width = 167F;
            textCellType3.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType3.MaxLength = 20;
            this.grdDetalles_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdDetalles_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(2).Label = "Producto";
            this.grdDetalles_Sheet1.Columns.Get(2).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(2).Width = 80F;
            textCellType4.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType4.MaxLength = 15;
            this.grdDetalles_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdDetalles_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(3).Label = "Código EAN";
            this.grdDetalles_Sheet1.Columns.Get(3).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(3).Width = 100F;
            this.grdDetalles_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdDetalles_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalles_Sheet1.Columns.Get(4).Label = "Descripción";
            this.grdDetalles_Sheet1.Columns.Get(4).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(4).Width = 240F;
            this.grdDetalles_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.grdDetalles_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdDetalles_Sheet1.Columns.Get(5).Label = "Presentación";
            this.grdDetalles_Sheet1.Columns.Get(5).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(5).Width = 75F;
            this.grdDetalles_Sheet1.Columns.Get(6).CellType = textCellType7;
            this.grdDetalles_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(6).Label = "Lote";
            this.grdDetalles_Sheet1.Columns.Get(6).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(6).Width = 90F;
            this.grdDetalles_Sheet1.Columns.Get(7).CellType = textCellType8;
            this.grdDetalles_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(7).Label = "Rack";
            this.grdDetalles_Sheet1.Columns.Get(7).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(7).Width = 75F;
            this.grdDetalles_Sheet1.Columns.Get(8).CellType = textCellType9;
            this.grdDetalles_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(8).Label = "Nivel";
            this.grdDetalles_Sheet1.Columns.Get(8).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(8).Width = 75F;
            this.grdDetalles_Sheet1.Columns.Get(9).CellType = textCellType10;
            this.grdDetalles_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(9).Label = "Entrepaño";
            this.grdDetalles_Sheet1.Columns.Get(9).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(9).Width = 75F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdDetalles_Sheet1.Columns.Get(10).CellType = numberCellType1;
            this.grdDetalles_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(10).Label = "Existencia Sistema";
            this.grdDetalles_Sheet1.Columns.Get(10).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(10).Visible = false;
            this.grdDetalles_Sheet1.Columns.Get(10).Width = 80F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdDetalles_Sheet1.Columns.Get(11).CellType = numberCellType2;
            this.grdDetalles_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(11).Label = "Cantidad";
            this.grdDetalles_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(11).Width = 80F;
            this.grdDetalles_Sheet1.Columns.Get(12).CellType = textCellType11;
            this.grdDetalles_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(12).Label = "Observaciones";
            this.grdDetalles_Sheet1.Columns.Get(12).Locked = false;
            this.grdDetalles_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(12).Width = 100F;
            this.grdDetalles_Sheet1.Columns.Get(13).CellType = checkBoxCellType1;
            this.grdDetalles_Sheet1.Columns.Get(13).Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.grdDetalles_Sheet1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(13).Label = "Validado";
            this.grdDetalles_Sheet1.Columns.Get(13).Locked = true;
            this.grdDetalles_Sheet1.Columns.Get(13).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDetalles_Sheet1.Columns.Get(13).Visible = false;
            this.grdDetalles_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdDetalles_Sheet1.Rows.Default.Height = 35F;
            this.grdDetalles_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmInvConteosCiclicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 611);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmInvConteosCiclicos";
            this.Text = "Inventarios Ciclicos Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmInvConteosCiclicos_Load);
            this.Shown += new System.EventHandler(this.FrmInvConteosCiclicos_Shown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.FrameUbicaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmNumClaves)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalles_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scTextBoxExt txtFolioCiclico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SC_ControlsCS.scTextBoxExt txtFolioConteo;
        private System.Windows.Forms.Label label2;
        private FarPoint.Win.Spread.FpSpread grdDetalles;
        private FarPoint.Win.Spread.SheetView grdDetalles_Sheet1;
        private System.Windows.Forms.Label lblCancelado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nmNumClaves;
        private System.Windows.Forms.ToolStripButton btnCerrarFolio;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPersonal;
        private SC_ControlsCS.scTextBoxExt txtIdPersonal;
        private System.Windows.Forms.GroupBox FrameUbicaciones;
        private System.Windows.Forms.RadioButton rdo_U_Todo;
        private System.Windows.Forms.RadioButton rdo_U_Almacenaje;
        private System.Windows.Forms.RadioButton rdo_U_Picking;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdo_Insumo_Todo;
        private System.Windows.Forms.RadioButton rdo_Insumo_MaterialDeCuracion;
        private System.Windows.Forms.RadioButton rdo_Insumo_Medicamento;
    }
}