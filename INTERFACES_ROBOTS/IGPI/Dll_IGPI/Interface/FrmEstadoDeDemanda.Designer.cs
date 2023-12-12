namespace Dll_IGPI.Interface
{
    partial class FrmEstadoDeDemanda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadoDeDemanda));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEnviar_R = new System.Windows.Forms.ToolStripButton();
            this.FrameSolicitudes = new System.Windows.Forms.GroupBox();
            this.grdSolicitudes = new FarPoint.Win.Spread.FpSpread();
            this.grdSolicitudes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmPeticiones = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameSolicitudes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolicitudes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolicitudes_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnEnviar_R});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(929, 25);
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
            // btnEnviar_R
            // 
            this.btnEnviar_R.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEnviar_R.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar_R.Image")));
            this.btnEnviar_R.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviar_R.Name = "btnEnviar_R";
            this.btnEnviar_R.Size = new System.Drawing.Size(23, 22);
            this.btnEnviar_R.Text = "&Status del Pedido";
            this.btnEnviar_R.Click += new System.EventHandler(this.btnEnviar_R_Click);
            // 
            // FrameSolicitudes
            // 
            this.FrameSolicitudes.Controls.Add(this.grdSolicitudes);
            this.FrameSolicitudes.Location = new System.Drawing.Point(7, 28);
            this.FrameSolicitudes.Name = "FrameSolicitudes";
            this.FrameSolicitudes.Size = new System.Drawing.Size(913, 427);
            this.FrameSolicitudes.TabIndex = 17;
            this.FrameSolicitudes.TabStop = false;
            this.FrameSolicitudes.Text = "Solicitudes";
            // 
            // grdSolicitudes
            // 
            this.grdSolicitudes.AccessibleDescription = "grdSolicitudes, Sheet1, Row 0, Column 0, ";
            this.grdSolicitudes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdSolicitudes.Location = new System.Drawing.Point(8, 19);
            this.grdSolicitudes.Name = "grdSolicitudes";
            this.grdSolicitudes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdSolicitudes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdSolicitudes_Sheet1});
            this.grdSolicitudes.Size = new System.Drawing.Size(897, 401);
            this.grdSolicitudes.TabIndex = 0;
            // 
            // grdSolicitudes_Sheet1
            // 
            this.grdSolicitudes_Sheet1.Reset();
            this.grdSolicitudes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdSolicitudes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdSolicitudes_Sheet1.ColumnCount = 8;
            this.grdSolicitudes_Sheet1.RowCount = 18;
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Número de Solicitud";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Producto";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Código EAN";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Descripción Clave";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Cant. Sol";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Cant. Disp";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "Fecha";
            this.grdSolicitudes_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Status";
            this.grdSolicitudes_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdSolicitudes_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdSolicitudes_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(0).Label = "Número de Solicitud";
            this.grdSolicitudes_Sheet1.Columns.Get(0).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdSolicitudes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(1).Label = "Producto";
            this.grdSolicitudes_Sheet1.Columns.Get(1).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(1).Width = 70F;
            this.grdSolicitudes_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdSolicitudes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(2).Label = "Código EAN";
            this.grdSolicitudes_Sheet1.Columns.Get(2).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(2).Width = 90F;
            textCellType4.MaxLength = 1000;
            this.grdSolicitudes_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdSolicitudes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdSolicitudes_Sheet1.Columns.Get(3).Label = "Descripción Clave";
            this.grdSolicitudes_Sheet1.Columns.Get(3).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(3).VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.grdSolicitudes_Sheet1.Columns.Get(3).Width = 280F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000;
            numberCellType1.MinimumValue = 0;
            this.grdSolicitudes_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdSolicitudes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(4).Label = "Cant. Sol";
            this.grdSolicitudes_Sheet1.Columns.Get(4).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000;
            numberCellType2.MinimumValue = 0;
            this.grdSolicitudes_Sheet1.Columns.Get(5).CellType = numberCellType2;
            this.grdSolicitudes_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(5).Label = "Cant. Disp";
            this.grdSolicitudes_Sheet1.Columns.Get(5).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(6).CellType = textCellType5;
            this.grdSolicitudes_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(6).Label = "Fecha";
            this.grdSolicitudes_Sheet1.Columns.Get(6).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(6).Width = 120F;
            this.grdSolicitudes_Sheet1.Columns.Get(7).CellType = textCellType6;
            this.grdSolicitudes_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(7).Label = "Status";
            this.grdSolicitudes_Sheet1.Columns.Get(7).Locked = true;
            this.grdSolicitudes_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSolicitudes_Sheet1.Columns.Get(7).VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.grdSolicitudes_Sheet1.Columns.Get(7).Width = 100F;
            this.grdSolicitudes_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdSolicitudes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmPeticiones
            // 
            this.tmPeticiones.Interval = 2000;
            this.tmPeticiones.Tick += new System.EventHandler(this.tmPeticiones_Tick);
            // 
            // FrmEstadoDeDemanda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 463);
            this.Controls.Add(this.FrameSolicitudes);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmEstadoDeDemanda";
            this.Text = "Estado de Solicitudes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadoDeDemanda_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEstadoDeDemanda_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameSolicitudes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSolicitudes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSolicitudes_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnEnviar_R;
        private System.Windows.Forms.GroupBox FrameSolicitudes;
        private FarPoint.Win.Spread.FpSpread grdSolicitudes;
        private FarPoint.Win.Spread.SheetView grdSolicitudes_Sheet1;
        private System.Windows.Forms.Timer tmPeticiones;
    }
}