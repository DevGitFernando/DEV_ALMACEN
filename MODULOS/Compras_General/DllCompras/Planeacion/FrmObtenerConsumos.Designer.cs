namespace DllCompras.Planeacion
{
    partial class FrmObtenerConsumos
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
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.ButtonCellType buttonCellType1 = new FarPoint.Win.Spread.CellType.ButtonCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmObtenerConsumos));
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblTitulo__Estado = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdConsumos = new FarPoint.Win.Spread.FpSpread();
            this.grdConsumos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActivarServicios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.txtMeses = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.FrameDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeses)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.label6);
            this.FrameDatos.Controls.Add(this.txtMeses);
            this.FrameDatos.Controls.Add(this.cboEstados);
            this.FrameDatos.Controls.Add(this.lblTitulo__Estado);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.cboFarmacias);
            this.FrameDatos.Location = new System.Drawing.Point(11, 26);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(753, 81);
            this.FrameDatos.TabIndex = 2;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "DatosGenerales";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(120, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(368, 21);
            this.cboEstados.TabIndex = 0;
            // 
            // lblTitulo__Estado
            // 
            this.lblTitulo__Estado.Location = new System.Drawing.Point(9, 23);
            this.lblTitulo__Estado.Name = "lblTitulo__Estado";
            this.lblTitulo__Estado.Size = new System.Drawing.Size(103, 13);
            this.lblTitulo__Estado.TabIndex = 48;
            this.lblTitulo__Estado.Text = "Estado :";
            this.lblTitulo__Estado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(120, 46);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(368, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdConsumos);
            this.groupBox1.Location = new System.Drawing.Point(11, 108);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(753, 399);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagenes";
            // 
            // grdConsumos
            // 
            this.grdConsumos.AccessibleDescription = "grdConsumos, Sheet1, Row 0, Column 0, ";
            this.grdConsumos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdConsumos.Location = new System.Drawing.Point(10, 20);
            this.grdConsumos.Margin = new System.Windows.Forms.Padding(2);
            this.grdConsumos.Name = "grdConsumos";
            this.grdConsumos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdConsumos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdConsumos_Sheet1});
            this.grdConsumos.Size = new System.Drawing.Size(732, 365);
            this.grdConsumos.TabIndex = 14;
            this.grdConsumos.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdConsumos_CellClick);
            this.grdConsumos.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdConsumos_ButtonClicked);
            // 
            // grdConsumos_Sheet1
            // 
            this.grdConsumos_Sheet1.Reset();
            this.grdConsumos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdConsumos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdConsumos_Sheet1.ColumnCount = 4;
            this.grdConsumos_Sheet1.RowCount = 10;
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEstado";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Consumos";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Actualizar";
            this.grdConsumos_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdConsumos_Sheet1.Columns.Get(1).Label = "Fecha";
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 100D;
            numberCellType1.MinimumValue = 1D;
            this.grdConsumos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdConsumos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(2).Label = "Consumos";
            this.grdConsumos_Sheet1.Columns.Get(2).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(2).Width = 184F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "...";
            this.grdConsumos_Sheet1.Columns.Get(3).CellType = buttonCellType1;
            this.grdConsumos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Label = "Actualizar";
            this.grdConsumos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Width = 90F;
            this.grdConsumos_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdConsumos_Sheet1.Rows.Default.Height = 25F;
            this.grdConsumos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnActivarServicios,
            this.toolStripSeparator5,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(776, 25);
            this.toolStripBarraMenu.TabIndex = 16;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnActivarServicios
            // 
            this.btnActivarServicios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActivarServicios.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarServicios.Image")));
            this.btnActivarServicios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActivarServicios.Name = "btnActivarServicios";
            this.btnActivarServicios.Size = new System.Drawing.Size(23, 22);
            this.btnActivarServicios.Text = "Revisar transferencias";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
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
            // 
            // txtMeses
            // 
            this.txtMeses.Location = new System.Drawing.Point(672, 32);
            this.txtMeses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMeses.Name = "txtMeses";
            this.txtMeses.Size = new System.Drawing.Size(58, 20);
            this.txtMeses.TabIndex = 49;
            this.txtMeses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(606, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 14);
            this.label6.TabIndex = 50;
            this.label6.Text = "Meses :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // FrmObtenerConsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 518);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDatos);
            this.Name = "FrmObtenerConsumos";
            this.Text = "Obtener Consumos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmObtenerConsumos_Load);
            this.FrameDatos.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblTitulo__Estado;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdConsumos;
        private FarPoint.Win.Spread.SheetView grdConsumos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnActivarServicios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.NumericUpDown txtMeses;
        private System.Windows.Forms.Label label6;
    }
}