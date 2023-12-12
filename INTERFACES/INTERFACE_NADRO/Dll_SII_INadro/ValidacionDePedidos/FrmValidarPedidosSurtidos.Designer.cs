namespace Dll_SII_INadro.ValidacionDePedidos
{
    partial class FrmValidarPedidosSurtidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidarPedidosSurtidos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType5 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType6 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPaqueteDeDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdProductos = new FarPoint.Win.Spread.FpSpread();
            this.grdProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameEncabezado = new System.Windows.Forms.GroupBox();
            this.lblRecibida = new System.Windows.Forms.Label();
            this.txtReferenciaFolioPedido = new SC_ControlsCS.scTextBoxExt();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.txtClaveCliente = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).BeginInit();
            this.FrameEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnGenerarPaqueteDeDatos,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1020, 25);
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
            // btnGenerarPaqueteDeDatos
            // 
            this.btnGenerarPaqueteDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPaqueteDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPaqueteDeDatos.Image")));
            this.btnGenerarPaqueteDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPaqueteDeDatos.Name = "btnGenerarPaqueteDeDatos";
            this.btnGenerarPaqueteDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPaqueteDeDatos.Text = "Generar paquete de datos de Transferencia";
            this.btnGenerarPaqueteDeDatos.Click += new System.EventHandler(this.btnGenerarPaqueteDeDatos_Click);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdProductos);
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(997, 374);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalles de Pedido";
            // 
            // grdProductos
            // 
            this.grdProductos.AccessibleDescription = "grdProductos, Sheet1, Row 0, Column 0, ";
            this.grdProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdProductos.Location = new System.Drawing.Point(11, 16);
            this.grdProductos.Name = "grdProductos";
            this.grdProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdProductos_Sheet1});
            this.grdProductos.Size = new System.Drawing.Size(977, 351);
            this.grdProductos.TabIndex = 0;
            // 
            // grdProductos_Sheet1
            // 
            this.grdProductos_Sheet1.Reset();
            this.grdProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdProductos_Sheet1.ColumnCount = 9;
            this.grdProductos_Sheet1.RowCount = 14;
            this.grdProductos_Sheet1.Cells.Get(0, 3).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(0, 4).Value = 110D;
            this.grdProductos_Sheet1.Cells.Get(0, 5).Value = 3D;
            this.grdProductos_Sheet1.Cells.Get(0, 6).Value = 7D;
            this.grdProductos_Sheet1.Cells.Get(0, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(0, 7).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(0, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(0, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(1, 2).Locked = false;
            this.grdProductos_Sheet1.Cells.Get(1, 3).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(1, 4).Value = 120D;
            this.grdProductos_Sheet1.Cells.Get(1, 5).Value = 3D;
            this.grdProductos_Sheet1.Cells.Get(1, 6).Value = 7D;
            this.grdProductos_Sheet1.Cells.Get(1, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(1, 7).Value = 110D;
            this.grdProductos_Sheet1.Cells.Get(1, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(1, 8).Value = 10D;
            this.grdProductos_Sheet1.Cells.Get(2, 3).Value = 100D;
            this.grdProductos_Sheet1.Cells.Get(2, 4).Value = 90D;
            this.grdProductos_Sheet1.Cells.Get(2, 5).Value = 3D;
            this.grdProductos_Sheet1.Cells.Get(2, 6).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(2, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(2, 7).Value = 87D;
            this.grdProductos_Sheet1.Cells.Get(2, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(2, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(3, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(3, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(3, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(4, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(4, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(4, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(5, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(5, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(5, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(6, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(6, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(6, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(7, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(7, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(7, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(8, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(8, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(8, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(9, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(9, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(9, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(10, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(10, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(10, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(11, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(11, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(11, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(12, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(12, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(12, 8).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(13, 7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Cells.Get(13, 7).Value = 0D;
            this.grdProductos_Sheet1.Cells.Get(13, 8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Cells.Get(13, 8).Value = 0D;
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Codigo Int / EAN";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Codigo";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Cantidad Pedido";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cantidad Recibida";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Dañado / Mal estado";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Caducado";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Cantidad Recibida Final";
            this.grdProductos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Cantidad excedente";
            this.grdProductos_Sheet1.ColumnHeader.Rows.Get(0).Height = 46F;
            textCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType1.MaxLength = 13;
            this.grdProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Label = "Codigo Int / EAN";
            this.grdProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(0).Width = 120F;
            textCellType2.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType2.MaxLength = 13;
            this.grdProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Label = "Codigo";
            this.grdProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(1).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(1).Width = 97F;
            this.grdProductos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdProductos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdProductos_Sheet1.Columns.Get(2).Label = "Descripción";
            this.grdProductos_Sheet1.Columns.Get(2).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(2).Width = 400F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(3).CellType = numberCellType1;
            this.grdProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(3).Label = "Cantidad Pedido";
            this.grdProductos_Sheet1.Columns.Get(3).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(3).Width = 80F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.DecimalSeparator = ".";
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = 0D;
            numberCellType2.Separator = ",";
            numberCellType2.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(4).Label = "Cantidad Recibida";
            this.grdProductos_Sheet1.Columns.Get(4).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(4).Width = 80F;
            numberCellType3.DecimalPlaces = 0;
            numberCellType3.DecimalSeparator = ".";
            numberCellType3.MaximumValue = 10000000D;
            numberCellType3.MinimumValue = 0D;
            numberCellType3.Separator = ",";
            numberCellType3.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(5).CellType = numberCellType3;
            this.grdProductos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(5).Label = "Dañado / Mal estado";
            this.grdProductos_Sheet1.Columns.Get(5).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(5).Width = 80F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.DecimalSeparator = ".";
            numberCellType4.MaximumValue = 10000000D;
            numberCellType4.MinimumValue = 0D;
            numberCellType4.Separator = ",";
            numberCellType4.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(6).CellType = numberCellType4;
            this.grdProductos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(6).Label = "Caducado";
            this.grdProductos_Sheet1.Columns.Get(6).Locked = false;
            this.grdProductos_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(6).Width = 80F;
            numberCellType5.DecimalPlaces = 0;
            numberCellType5.DecimalSeparator = ".";
            numberCellType5.MaximumValue = 10000000D;
            numberCellType5.MinimumValue = 0D;
            numberCellType5.Separator = ",";
            numberCellType5.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(7).CellType = numberCellType5;
            this.grdProductos_Sheet1.Columns.Get(7).Formula = "RC[-3]-(RC[-2]+RC[-1])";
            this.grdProductos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Label = "Cantidad Recibida Final";
            this.grdProductos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(7).Visible = false;
            this.grdProductos_Sheet1.Columns.Get(7).Width = 80F;
            numberCellType6.DecimalPlaces = 0;
            numberCellType6.DecimalSeparator = ".";
            numberCellType6.MaximumValue = 10000000D;
            numberCellType6.MinimumValue = 0D;
            numberCellType6.Separator = ",";
            numberCellType6.ShowSeparator = true;
            this.grdProductos_Sheet1.Columns.Get(8).CellType = numberCellType6;
            this.grdProductos_Sheet1.Columns.Get(8).Formula = "IF(RC[-1]>RC[-5],RC[-1]-RC[-5],0)";
            this.grdProductos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdProductos_Sheet1.Columns.Get(8).Label = "Cantidad excedente";
            this.grdProductos_Sheet1.Columns.Get(8).Locked = true;
            this.grdProductos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdProductos_Sheet1.Columns.Get(8).Width = 80F;
            this.grdProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameEncabezado
            // 
            this.FrameEncabezado.Controls.Add(this.lblRecibida);
            this.FrameEncabezado.Controls.Add(this.txtReferenciaFolioPedido);
            this.FrameEncabezado.Controls.Add(this.label13);
            this.FrameEncabezado.Controls.Add(this.dtpFechaRegistro);
            this.FrameEncabezado.Controls.Add(this.label3);
            this.FrameEncabezado.Controls.Add(this.lblNombreCliente);
            this.FrameEncabezado.Controls.Add(this.txtClaveCliente);
            this.FrameEncabezado.Controls.Add(this.label2);
            this.FrameEncabezado.Controls.Add(this.txtFolio);
            this.FrameEncabezado.Controls.Add(this.label1);
            this.FrameEncabezado.Location = new System.Drawing.Point(12, 27);
            this.FrameEncabezado.Name = "FrameEncabezado";
            this.FrameEncabezado.Size = new System.Drawing.Size(997, 104);
            this.FrameEncabezado.TabIndex = 1;
            this.FrameEncabezado.TabStop = false;
            this.FrameEncabezado.Text = "Datos generales de Pedido";
            // 
            // lblRecibida
            // 
            this.lblRecibida.BackColor = System.Drawing.Color.Transparent;
            this.lblRecibida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRecibida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibida.Location = new System.Drawing.Point(249, 24);
            this.lblRecibida.Name = "lblRecibida";
            this.lblRecibida.Size = new System.Drawing.Size(102, 20);
            this.lblRecibida.TabIndex = 26;
            this.lblRecibida.Text = "RECIBIDA";
            this.lblRecibida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRecibida.Visible = false;
            // 
            // txtReferenciaFolioPedido
            // 
            this.txtReferenciaFolioPedido.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaFolioPedido.Decimales = 2;
            this.txtReferenciaFolioPedido.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaFolioPedido.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaFolioPedido.Location = new System.Drawing.Point(129, 24);
            this.txtReferenciaFolioPedido.MaxLength = 20;
            this.txtReferenciaFolioPedido.Name = "txtReferenciaFolioPedido";
            this.txtReferenciaFolioPedido.PermitirApostrofo = false;
            this.txtReferenciaFolioPedido.PermitirNegativos = false;
            this.txtReferenciaFolioPedido.Size = new System.Drawing.Size(114, 20);
            this.txtReferenciaFolioPedido.TabIndex = 0;
            this.txtReferenciaFolioPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReferenciaFolioPedido.Validating += new System.ComponentModel.CancelEventHandler(this.txtReferenciaFolioPedido_Validating);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(11, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Referencia de pedido :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(897, 24);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(91, 20);
            this.dtpFechaRegistro.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(750, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fecha Registro :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNombreCliente.Location = new System.Drawing.Point(249, 73);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(739, 21);
            this.lblNombreCliente.TabIndex = 6;
            this.lblNombreCliente.Text = "Proveedor :";
            this.lblNombreCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClaveCliente
            // 
            this.txtClaveCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtClaveCliente.Decimales = 2;
            this.txtClaveCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtClaveCliente.ForeColor = System.Drawing.Color.Black;
            this.txtClaveCliente.Location = new System.Drawing.Point(129, 73);
            this.txtClaveCliente.MaxLength = 4;
            this.txtClaveCliente.Name = "txtClaveCliente";
            this.txtClaveCliente.PermitirApostrofo = false;
            this.txtClaveCliente.PermitirNegativos = false;
            this.txtClaveCliente.Size = new System.Drawing.Size(114, 20);
            this.txtClaveCliente.TabIndex = 2;
            this.txtClaveCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(129, 48);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(114, 20);
            this.txtFolio.TabIndex = 1;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValidarPedidosSurtidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 516);
            this.Controls.Add(this.FrameEncabezado);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmValidarPedidosSurtidos";
            this.Text = "Validar pedidos surtidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidarPedidosSurtidos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdProductos_Sheet1)).EndInit();
            this.FrameEncabezado.ResumeLayout(false);
            this.FrameEncabezado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdProductos;
        private FarPoint.Win.Spread.SheetView grdProductos_Sheet1;
        private System.Windows.Forms.GroupBox FrameEncabezado;
        private System.Windows.Forms.Label lblRecibida;
        private SC_ControlsCS.scTextBoxExt txtReferenciaFolioPedido;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNombreCliente;
        private SC_ControlsCS.scTextBoxExt txtClaveCliente;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnGenerarPaqueteDeDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}