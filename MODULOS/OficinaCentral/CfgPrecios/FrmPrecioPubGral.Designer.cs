namespace OficinaCentral.CfgPrecios
{
    partial class FrmPrecioPubGral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrecioPubGral));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameProducto = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodigoEAN = new SC_ControlsCS.scTextBoxExt();
            this.txtIdProducto = new SC_ControlsCS.scTextBoxExt();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.rdoProducto = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.grpProductos = new FarPoint.Win.Spread.FpSpread();
            this.grpProductos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameListaProductos = new System.Windows.Forms.GroupBox();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.tmEjecucion = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos_Sheet1)).BeginInit();
            this.FrameListaProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator2,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(857, 25);
            this.toolStripBarraMenu.TabIndex = 8;
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameProducto
            // 
            this.FrameProducto.Controls.Add(this.label13);
            this.FrameProducto.Controls.Add(this.txtCodigoEAN);
            this.FrameProducto.Controls.Add(this.txtIdProducto);
            this.FrameProducto.Controls.Add(this.lblDescripcion);
            this.FrameProducto.Controls.Add(this.rdoProducto);
            this.FrameProducto.Controls.Add(this.rdoTodos);
            this.FrameProducto.Location = new System.Drawing.Point(11, 28);
            this.FrameProducto.Name = "FrameProducto";
            this.FrameProducto.Size = new System.Drawing.Size(837, 98);
            this.FrameProducto.TabIndex = 0;
            this.FrameProducto.TabStop = false;
            this.FrameProducto.Text = "Producto(s) a Asignar Precio";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(329, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "EAN :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoEAN
            // 
            this.txtCodigoEAN.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoEAN.Decimales = 2;
            this.txtCodigoEAN.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoEAN.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoEAN.Location = new System.Drawing.Point(366, 18);
            this.txtCodigoEAN.MaxLength = 25;
            this.txtCodigoEAN.Name = "txtCodigoEAN";
            this.txtCodigoEAN.PermitirApostrofo = false;
            this.txtCodigoEAN.PermitirNegativos = false;
            this.txtCodigoEAN.Size = new System.Drawing.Size(166, 20);
            this.txtCodigoEAN.TabIndex = 2;
            this.txtCodigoEAN.Text = "0123456789012345678901234";
            this.txtCodigoEAN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoEAN.Validating += new System.ComponentModel.CancelEventHandler(this.txtCodigoEAN_Validating);
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProducto.Decimales = 2;
            this.txtIdProducto.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProducto.ForeColor = System.Drawing.Color.Black;
            this.txtIdProducto.Location = new System.Drawing.Point(247, 20);
            this.txtIdProducto.MaxLength = 8;
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.PermitirApostrofo = false;
            this.txtIdProducto.PermitirNegativos = false;
            this.txtIdProducto.Size = new System.Drawing.Size(71, 20);
            this.txtIdProducto.TabIndex = 1;
            this.txtIdProducto.Text = "12345678";
            this.txtIdProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdProducto_KeyDown);
            this.txtIdProducto.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdProducto_Validating);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(32, 43);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(797, 41);
            this.lblDescripcion.TabIndex = 4;
            this.lblDescripcion.Text = "label1";
            // 
            // rdoProducto
            // 
            this.rdoProducto.Location = new System.Drawing.Point(179, 20);
            this.rdoProducto.Name = "rdoProducto";
            this.rdoProducto.Size = new System.Drawing.Size(68, 15);
            this.rdoProducto.TabIndex = 0;
            this.rdoProducto.Text = "Producto";
            this.rdoProducto.UseVisualStyleBackColor = true;
            this.rdoProducto.CheckedChanged += new System.EventHandler(this.rdoProducto_CheckedChanged);
            // 
            // rdoTodos
            // 
            this.rdoTodos.Checked = true;
            this.rdoTodos.Location = new System.Drawing.Point(586, 18);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(71, 15);
            this.rdoTodos.TabIndex = 3;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = true;
            this.rdoTodos.CheckedChanged += new System.EventHandler(this.rdoTodos_CheckedChanged);
            // 
            // grpProductos
            // 
            this.grpProductos.AccessibleDescription = "grpProductos, Sheet1, Row 0, Column 0, ";
            this.grpProductos.AllowUserZoom = false;
            this.grpProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpProductos.Location = new System.Drawing.Point(11, 16);
            this.grpProductos.Name = "grpProductos";
            this.grpProductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grpProductos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grpProductos_Sheet1});
            this.grpProductos.Size = new System.Drawing.Size(818, 321);
            this.grpProductos.TabIndex = 0;
            // 
            // grpProductos_Sheet1
            // 
            this.grpProductos_Sheet1.Reset();
            this.grpProductos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grpProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grpProductos_Sheet1.ColumnCount = 8;
            this.grpProductos_Sheet1.RowCount = 5;
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Codigo";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Descripción";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Utilidad";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Precio Máximo";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Descuento";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "UtilidadRef";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "PrecioMaximoRef";
            this.grpProductos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "DescuentoRef";
            textCellType1.MaxLength = 10;
            this.grpProductos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grpProductos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(0).Label = "Código";
            this.grpProductos_Sheet1.Columns.Get(0).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(0).Width = 80F;
            textCellType2.MaxLength = 1000;
            this.grpProductos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grpProductos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grpProductos_Sheet1.Columns.Get(1).Label = "Descripción";
            this.grpProductos_Sheet1.Columns.Get(1).Locked = true;
            this.grpProductos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(1).Width = 439F;
            numberCellType1.DecimalPlaces = 2;
            numberCellType1.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grpProductos_Sheet1.Columns.Get(2).Label = "Utilidad";
            this.grpProductos_Sheet1.Columns.Get(2).Width = 75F;
            numberCellType2.DecimalPlaces = 4;
            numberCellType2.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.grpProductos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grpProductos_Sheet1.Columns.Get(3).Label = "Precio Máximo";
            this.grpProductos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(3).Width = 90F;
            numberCellType3.DecimalPlaces = 2;
            numberCellType3.MaximumValue = 100;
            numberCellType3.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(4).CellType = numberCellType3;
            this.grpProductos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grpProductos_Sheet1.Columns.Get(4).Label = "Descuento";
            this.grpProductos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grpProductos_Sheet1.Columns.Get(4).Width = 75F;
            numberCellType4.DecimalPlaces = 2;
            numberCellType4.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(5).CellType = numberCellType4;
            this.grpProductos_Sheet1.Columns.Get(5).Label = "UtilidadRef";
            this.grpProductos_Sheet1.Columns.Get(5).Visible = false;
            numberCellType5.DecimalPlaces = 2;
            numberCellType5.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(6).CellType = numberCellType5;
            this.grpProductos_Sheet1.Columns.Get(6).Label = "PrecioMaximoRef";
            this.grpProductos_Sheet1.Columns.Get(6).Visible = false;
            this.grpProductos_Sheet1.Columns.Get(6).Width = 90F;
            numberCellType6.DecimalPlaces = 2;
            numberCellType6.MaximumValue = 100;
            numberCellType6.MinimumValue = 0;
            this.grpProductos_Sheet1.Columns.Get(7).CellType = numberCellType6;
            this.grpProductos_Sheet1.Columns.Get(7).Label = "DescuentoRef";
            this.grpProductos_Sheet1.Columns.Get(7).Visible = false;
            this.grpProductos_Sheet1.Columns.Get(7).Width = 100F;
            this.grpProductos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grpProductos_Sheet1.RowHeader.Columns.Get(0).Width = 36F;
            this.grpProductos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameListaProductos
            // 
            this.FrameListaProductos.Controls.Add(this.pgBar);
            this.FrameListaProductos.Controls.Add(this.lblMensaje);
            this.FrameListaProductos.Controls.Add(this.grpProductos);
            this.FrameListaProductos.Location = new System.Drawing.Point(11, 128);
            this.FrameListaProductos.Name = "FrameListaProductos";
            this.FrameListaProductos.Size = new System.Drawing.Size(837, 345);
            this.FrameListaProductos.TabIndex = 1;
            this.FrameListaProductos.TabStop = false;
            this.FrameListaProductos.Text = "Productos";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(130, 125);
            this.pgBar.MarqueeAnimationSpeed = 40;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(536, 53);
            this.pgBar.Step = 50;
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgBar.TabIndex = 2;
            this.pgBar.Visible = false;
            // 
            // lblMensaje
            // 
            this.lblMensaje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(130, 76);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(536, 46);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "GUARDANDO INFORMACIÓN";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmEjecucion
            // 
            this.tmEjecucion.Interval = 500;
            this.tmEjecucion.Tick += new System.EventHandler(this.tmEjecucion_Tick);
            // 
            // FrmPrecioPubGral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 481);
            this.Controls.Add(this.FrameListaProductos);
            this.Controls.Add(this.FrameProducto);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPrecioPubGral";
            this.Text = "Asignación de Precios para Publico General";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPrecioPubGral_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameProducto.ResumeLayout(false);
            this.FrameProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpProductos_Sheet1)).EndInit();
            this.FrameListaProductos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameProducto;
        private FarPoint.Win.Spread.FpSpread grpProductos;
        private FarPoint.Win.Spread.SheetView grpProductos_Sheet1;
        private System.Windows.Forms.GroupBox FrameListaProductos;
        private System.Windows.Forms.RadioButton rdoProducto;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.Label lblDescripcion;
        private SC_ControlsCS.scTextBoxExt txtIdProducto;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Timer tmEjecucion;
        private System.Windows.Forms.Label label13;
        private SC_ControlsCS.scTextBoxExt txtCodigoEAN;
        private System.Windows.Forms.ProgressBar pgBar;

    }
}