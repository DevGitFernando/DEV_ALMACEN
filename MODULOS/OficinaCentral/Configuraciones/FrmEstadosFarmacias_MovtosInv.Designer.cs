namespace OficinaCentral.Configuraciones
{
    partial class FrmEstadosFarmacias_MovtosInv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadosFarmacias_MovtosInv));
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameEstados = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameServicios = new System.Windows.Forms.GroupBox();
            this.grdAreas = new FarPoint.Win.Spread.FpSpread();
            this.grdAreas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmSubClientes = new System.Windows.Forms.Timer(this.components);
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEstados.SuspendLayout();
            this.FrameServicios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAreas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAreas_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(745, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            // FrameEstados
            // 
            this.FrameEstados.Controls.Add(this.cboEstados);
            this.FrameEstados.Controls.Add(this.label4);
            this.FrameEstados.Controls.Add(this.cboFarmacias);
            this.FrameEstados.Controls.Add(this.label1);
            this.FrameEstados.Location = new System.Drawing.Point(10, 28);
            this.FrameEstados.Name = "FrameEstados";
            this.FrameEstados.Size = new System.Drawing.Size(727, 55);
            this.FrameEstados.TabIndex = 7;
            this.FrameEstados.TabStop = false;
            this.FrameEstados.Text = "Datos de Estado y Farmacia";
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(72, 21);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(236, 21);
            this.cboEstados.TabIndex = 11;
            this.cboEstados.Validating += new System.ComponentModel.CancelEventHandler(this.cboEstados_Validating);
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Estado :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(389, 21);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(331, 21);
            this.cboFarmacias.TabIndex = 13;
            this.cboFarmacias.Validating += new System.ComponentModel.CancelEventHandler(this.cboFarmacias_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(323, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameServicios
            // 
            this.FrameServicios.Controls.Add(this.grdAreas);
            this.FrameServicios.Location = new System.Drawing.Point(10, 85);
            this.FrameServicios.Name = "FrameServicios";
            this.FrameServicios.Size = new System.Drawing.Size(727, 343);
            this.FrameServicios.TabIndex = 8;
            this.FrameServicios.TabStop = false;
            this.FrameServicios.Text = "Lista de Movimientos de Inventario";
            // 
            // grdAreas
            // 
            this.grdAreas.AccessibleDescription = "grdAreas, Sheet1, Row 0, Column 0, ";
            this.grdAreas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdAreas.Location = new System.Drawing.Point(10, 16);
            this.grdAreas.Name = "grdAreas";
            this.grdAreas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdAreas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdAreas_Sheet1});
            this.grdAreas.Size = new System.Drawing.Size(710, 321);
            this.grdAreas.TabIndex = 15;
            // 
            // grdAreas_Sheet1
            // 
            this.grdAreas_Sheet1.Reset();
            this.grdAreas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdAreas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdAreas_Sheet1.ColumnCount = 5;
            this.grdAreas_Sheet1.RowCount = 14;
            this.grdAreas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Movto";
            this.grdAreas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de Movimiento de Inventario";
            this.grdAreas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Efecto";
            this.grdAreas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdAreas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status Aux";
            this.grdAreas_Sheet1.Columns.Get(0).CellType = textCellType4;
            this.grdAreas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(0).Label = "Id Movto";
            this.grdAreas_Sheet1.Columns.Get(0).Locked = true;
            this.grdAreas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(0).Width = 80F;
            this.grdAreas_Sheet1.Columns.Get(1).CellType = textCellType5;
            this.grdAreas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdAreas_Sheet1.Columns.Get(1).Label = "Nombre de Movimiento de Inventario";
            this.grdAreas_Sheet1.Columns.Get(1).Locked = true;
            this.grdAreas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(1).Width = 380F;
            this.grdAreas_Sheet1.Columns.Get(2).CellType = textCellType6;
            this.grdAreas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(2).Label = "Efecto";
            this.grdAreas_Sheet1.Columns.Get(2).Locked = true;
            this.grdAreas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Bottom;
            this.grdAreas_Sheet1.Columns.Get(2).Width = 90F;
            this.grdAreas_Sheet1.Columns.Get(3).CellType = checkBoxCellType3;
            this.grdAreas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(3).Label = "Status";
            this.grdAreas_Sheet1.Columns.Get(3).Locked = false;
            this.grdAreas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(3).Width = 104F;
            this.grdAreas_Sheet1.Columns.Get(4).CellType = checkBoxCellType4;
            this.grdAreas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(4).Label = "Status Aux";
            this.grdAreas_Sheet1.Columns.Get(4).Locked = true;
            this.grdAreas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdAreas_Sheet1.Columns.Get(4).Visible = false;
            this.grdAreas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdAreas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tmSubClientes
            // 
            this.tmSubClientes.Interval = 500;
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
            // FrmEstadosFarmacias_MovtosInv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 440);
            this.Controls.Add(this.FrameServicios);
            this.Controls.Add(this.FrameEstados);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmEstadosFarmacias_MovtosInv";
            this.Text = "Configuración de Movimientos de Inventario por Farmacia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosFarmacias_ClientesProgramas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEstados.ResumeLayout(false);
            this.FrameServicios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAreas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAreas_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.GroupBox FrameEstados;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FrameServicios;
        private FarPoint.Win.Spread.FpSpread grdAreas;
        private FarPoint.Win.Spread.SheetView grdAreas_Sheet1;
        private System.Windows.Forms.Timer tmSubClientes;
        private System.Windows.Forms.ToolStripButton btnGuardar;
    }
}