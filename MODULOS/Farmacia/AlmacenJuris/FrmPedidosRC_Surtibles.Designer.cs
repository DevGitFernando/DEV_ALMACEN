namespace Farmacia.AlmacenJuris
{
    partial class FrmPedidosRC_Surtibles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPedidosRC_Surtibles));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdPedidos = new FarPoint.Win.Spread.FpSpread();
            this.grdPedidos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(791, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grdPedidos);
            this.groupBox2.Location = new System.Drawing.Point(10, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 369);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pedidos por Surtir";
            // 
            // grdPedidos
            // 
            this.grdPedidos.AccessibleDescription = "grdPedidos, Sheet1, Row 0, Column 0, ";
            this.grdPedidos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPedidos.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.grdPedidos.Location = new System.Drawing.Point(7, 17);
            this.grdPedidos.Name = "grdPedidos";
            this.grdPedidos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdPedidos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdPedidos_Sheet1});
            this.grdPedidos.Size = new System.Drawing.Size(761, 344);
            this.grdPedidos.TabIndex = 0;
            this.grdPedidos.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdPedidos_CellDoubleClick);
            // 
            // grdPedidos_Sheet1
            // 
            this.grdPedidos_Sheet1.Reset();
            this.grdPedidos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdPedidos_Sheet1.ColumnCount = 11;
            this.grdPedidos_Sheet1.RowCount = 15;
            this.grdPedidos_Sheet1.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Cells.Get(0, 7).Locked = true;
            this.grdPedidos_Sheet1.Cells.Get(0, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Empresa";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Estado";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Jurisdiccion";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Farmacia";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Pedido";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Estatus de Pedido";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Número de Surtimientos";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Fecha recepción";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "Centro de Salud";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "Cantidad solicitada";
            this.grdPedidos_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "Cantidad Entregada";
            this.grdPedidos_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdPedidos_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdPedidos_Sheet1.Columns.Get(0).Label = "Empresa";
            this.grdPedidos_Sheet1.Columns.Get(0).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdPedidos_Sheet1.Columns.Get(1).Label = "Estado";
            this.grdPedidos_Sheet1.Columns.Get(1).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdPedidos_Sheet1.Columns.Get(2).Label = "Jurisdiccion";
            this.grdPedidos_Sheet1.Columns.Get(2).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(2).Width = 66F;
            this.grdPedidos_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdPedidos_Sheet1.Columns.Get(3).Label = "Farmacia";
            this.grdPedidos_Sheet1.Columns.Get(3).Visible = false;
            this.grdPedidos_Sheet1.Columns.Get(3).Width = 67F;
            textCellType5.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.AlphaNumeric;
            textCellType5.MaxLength = 20;
            this.grdPedidos_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdPedidos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(4).Label = "Pedido";
            this.grdPedidos_Sheet1.Columns.Get(4).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(4).Width = 57F;
            this.grdPedidos_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.grdPedidos_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(5).Label = "Estatus de Pedido";
            this.grdPedidos_Sheet1.Columns.Get(5).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(5).Width = 117F;
            this.grdPedidos_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(6).Label = "Número de Surtimientos";
            this.grdPedidos_Sheet1.Columns.Get(6).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(6).Width = 78F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2009, 8, 27, 18, 33, 32, 0);
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dateTimeCellType1.TimeDefault = new System.DateTime(2009, 8, 27, 18, 33, 32, 0);
            dateTimeCellType1.UserDefinedFormat = "yyyy-MM-dd";
            this.grdPedidos_Sheet1.Columns.Get(7).CellType = dateTimeCellType1;
            this.grdPedidos_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(7).Label = "Fecha recepción";
            this.grdPedidos_Sheet1.Columns.Get(7).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(7).Width = 67F;
            this.grdPedidos_Sheet1.Columns.Get(8).CellType = textCellType7;
            this.grdPedidos_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdPedidos_Sheet1.Columns.Get(8).Label = "Centro de Salud";
            this.grdPedidos_Sheet1.Columns.Get(8).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(8).Width = 244F;
            this.grdPedidos_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(9).Label = "Cantidad solicitada";
            this.grdPedidos_Sheet1.Columns.Get(9).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(9).Width = 73F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000;
            numberCellType1.MinimumValue = 0;
            this.grdPedidos_Sheet1.Columns.Get(10).CellType = numberCellType1;
            this.grdPedidos_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(10).Label = "Cantidad Entregada";
            this.grdPedidos_Sheet1.Columns.Get(10).Locked = true;
            this.grdPedidos_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdPedidos_Sheet1.Columns.Get(10).Width = 69F;
            this.grdPedidos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdPedidos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmPedidosRC_Surtibles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 404);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmPedidosRC_Surtibles";
            this.Text = "Pedidos RC listos para Surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPedidosRC_Surtibles_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox2;
        private FarPoint.Win.Spread.FpSpread grdPedidos;
        private FarPoint.Win.Spread.SheetView grdPedidos_Sheet1;
    }
}