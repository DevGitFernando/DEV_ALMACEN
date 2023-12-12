namespace OficinaCentral.Configuraciones
{
    partial class FrmEstadosFarmacias_ClientesProgramas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstadosFarmacias_ClientesProgramas));
            FarPoint.Win.Spread.CellType.TextCellType textCellType36 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType37 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType38 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType39 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType40 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType41 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType42 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType11 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType12 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FrameEstados = new System.Windows.Forms.GroupBox();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameSubClientes = new System.Windows.Forms.GroupBox();
            this.grdSubClientes = new FarPoint.Win.Spread.FpSpread();
            this.grdSubClientes_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cboClientes = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameProgramas = new System.Windows.Forms.GroupBox();
            this.btnAsignarProgramas = new System.Windows.Forms.Button();
            this.grdSubProgramas = new FarPoint.Win.Spread.FpSpread();
            this.grdSubProgramas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cboProgramas = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.tmSubClientes = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameEstados.SuspendLayout();
            this.FrameSubClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes_Sheet1)).BeginInit();
            this.FrameProgramas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubProgramas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubProgramas_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(934, 25);
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
            this.FrameEstados.Location = new System.Drawing.Point(10, 26);
            this.FrameEstados.Name = "FrameEstados";
            this.FrameEstados.Size = new System.Drawing.Size(915, 48);
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
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(82, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(358, 21);
            this.cboEstados.TabIndex = 11;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            this.cboEstados.Validating += new System.ComponentModel.CancelEventHandler(this.cboEstados_Validating);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
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
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(547, 19);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(358, 21);
            this.cboFarmacias.TabIndex = 13;
            this.cboFarmacias.Validating += new System.ComponentModel.CancelEventHandler(this.cboFarmacias_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(485, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameSubClientes
            // 
            this.FrameSubClientes.Controls.Add(this.grdSubClientes);
            this.FrameSubClientes.Controls.Add(this.cboClientes);
            this.FrameSubClientes.Controls.Add(this.label2);
            this.FrameSubClientes.Location = new System.Drawing.Point(10, 76);
            this.FrameSubClientes.Name = "FrameSubClientes";
            this.FrameSubClientes.Size = new System.Drawing.Size(915, 219);
            this.FrameSubClientes.TabIndex = 8;
            this.FrameSubClientes.TabStop = false;
            this.FrameSubClientes.Text = "Clientes por Farmacia";
            // 
            // grdSubClientes
            // 
            this.grdSubClientes.AccessibleDescription = "grdSubClientes, Sheet1, Row 0, Column 0, ";
            this.grdSubClientes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdSubClientes.Location = new System.Drawing.Point(10, 46);
            this.grdSubClientes.Name = "grdSubClientes";
            this.grdSubClientes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdSubClientes.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdSubClientes_Sheet1});
            this.grdSubClientes.Size = new System.Drawing.Size(895, 165);
            this.grdSubClientes.TabIndex = 15;
            // 
            // grdSubClientes_Sheet1
            // 
            this.grdSubClientes_Sheet1.Reset();
            this.grdSubClientes_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdSubClientes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdSubClientes_Sheet1.ColumnCount = 5;
            this.grdSubClientes_Sheet1.RowCount = 5;
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Cliente";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Sub-Cliente";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre de Sub-Cliente";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status";
            this.grdSubClientes_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "StatusAux";
            this.grdSubClientes_Sheet1.Columns.Get(0).CellType = textCellType36;
            this.grdSubClientes_Sheet1.Columns.Get(0).Label = "Cliente";
            this.grdSubClientes_Sheet1.Columns.Get(0).Visible = false;
            this.grdSubClientes_Sheet1.Columns.Get(1).CellType = textCellType37;
            this.grdSubClientes_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(1).Label = "Sub-Cliente";
            this.grdSubClientes_Sheet1.Columns.Get(1).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(1).Width = 80F;
            this.grdSubClientes_Sheet1.Columns.Get(2).CellType = textCellType38;
            this.grdSubClientes_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdSubClientes_Sheet1.Columns.Get(2).Label = "Nombre de Sub-Cliente";
            this.grdSubClientes_Sheet1.Columns.Get(2).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(2).Width = 380F;
            this.grdSubClientes_Sheet1.Columns.Get(3).CellType = textCellType39;
            this.grdSubClientes_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(3).Label = "Status";
            this.grdSubClientes_Sheet1.Columns.Get(3).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(3).Width = 104F;
            this.grdSubClientes_Sheet1.Columns.Get(4).CellType = textCellType40;
            this.grdSubClientes_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(4).Label = "StatusAux";
            this.grdSubClientes_Sheet1.Columns.Get(4).Locked = true;
            this.grdSubClientes_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubClientes_Sheet1.Columns.Get(4).Visible = false;
            this.grdSubClientes_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdSubClientes_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // cboClientes
            // 
            this.cboClientes.BackColorEnabled = System.Drawing.Color.White;
            this.cboClientes.Data = "";
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.Filtro = " 1 = 1";
            this.cboClientes.FormattingEnabled = true;
            this.cboClientes.ListaItemsBusqueda = 20;
            this.cboClientes.Location = new System.Drawing.Point(82, 18);
            this.cboClientes.MostrarToolTip = false;
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(823, 21);
            this.cboClientes.TabIndex = 13;
            this.cboClientes.SelectedIndexChanged += new System.EventHandler(this.cboClientes_SelectedIndexChanged);
            this.cboClientes.Validating += new System.ComponentModel.CancelEventHandler(this.cboClientes_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameProgramas
            // 
            this.FrameProgramas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameProgramas.Controls.Add(this.btnAsignarProgramas);
            this.FrameProgramas.Controls.Add(this.grdSubProgramas);
            this.FrameProgramas.Controls.Add(this.cboProgramas);
            this.FrameProgramas.Controls.Add(this.label3);
            this.FrameProgramas.Location = new System.Drawing.Point(10, 301);
            this.FrameProgramas.Name = "FrameProgramas";
            this.FrameProgramas.Size = new System.Drawing.Size(915, 377);
            this.FrameProgramas.TabIndex = 9;
            this.FrameProgramas.TabStop = false;
            this.FrameProgramas.Text = "Programas y Sub-Programas por Clientes por Farmacia";
            // 
            // btnAsignarProgramas
            // 
            this.btnAsignarProgramas.Location = new System.Drawing.Point(751, 16);
            this.btnAsignarProgramas.Name = "btnAsignarProgramas";
            this.btnAsignarProgramas.Size = new System.Drawing.Size(154, 23);
            this.btnAsignarProgramas.TabIndex = 17;
            this.btnAsignarProgramas.Text = "Asignar Sub-Programas";
            this.btnAsignarProgramas.UseVisualStyleBackColor = true;
            this.btnAsignarProgramas.Click += new System.EventHandler(this.btnAsignarProgramas_Click);
            // 
            // grdSubProgramas
            // 
            this.grdSubProgramas.AccessibleDescription = "grdSubProgramas, Sheet1, Row 0, Column 0, ";
            this.grdSubProgramas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSubProgramas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdSubProgramas.Location = new System.Drawing.Point(10, 47);
            this.grdSubProgramas.Name = "grdSubProgramas";
            this.grdSubProgramas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdSubProgramas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdSubProgramas_Sheet1});
            this.grdSubProgramas.Size = new System.Drawing.Size(895, 321);
            this.grdSubProgramas.TabIndex = 16;
            // 
            // grdSubProgramas_Sheet1
            // 
            this.grdSubProgramas_Sheet1.Reset();
            this.grdSubProgramas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdSubProgramas_Sheet1.ColumnCount = 4;
            this.grdSubProgramas_Sheet1.RowCount = 5;
            this.grdSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Sub-Programa";
            this.grdSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre de Sub-Programa";
            this.grdSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Status";
            this.grdSubProgramas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Status Aux";
            this.grdSubProgramas_Sheet1.Columns.Get(0).CellType = textCellType41;
            this.grdSubProgramas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(0).Label = "Sub-Programa";
            this.grdSubProgramas_Sheet1.Columns.Get(0).Locked = true;
            this.grdSubProgramas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(0).Width = 80F;
            this.grdSubProgramas_Sheet1.Columns.Get(1).CellType = textCellType42;
            this.grdSubProgramas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdSubProgramas_Sheet1.Columns.Get(1).Label = "Nombre de Sub-Programa";
            this.grdSubProgramas_Sheet1.Columns.Get(1).Locked = true;
            this.grdSubProgramas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(1).Width = 380F;
            this.grdSubProgramas_Sheet1.Columns.Get(2).CellType = checkBoxCellType11;
            this.grdSubProgramas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(2).Label = "Status";
            this.grdSubProgramas_Sheet1.Columns.Get(2).Locked = false;
            this.grdSubProgramas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(2).Width = 104F;
            this.grdSubProgramas_Sheet1.Columns.Get(3).CellType = checkBoxCellType12;
            this.grdSubProgramas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(3).Label = "Status Aux";
            this.grdSubProgramas_Sheet1.Columns.Get(3).Locked = true;
            this.grdSubProgramas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdSubProgramas_Sheet1.Columns.Get(3).Visible = false;
            this.grdSubProgramas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdSubProgramas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // cboProgramas
            // 
            this.cboProgramas.BackColorEnabled = System.Drawing.Color.White;
            this.cboProgramas.Data = "";
            this.cboProgramas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProgramas.Filtro = " 1 = 1";
            this.cboProgramas.FormattingEnabled = true;
            this.cboProgramas.ListaItemsBusqueda = 20;
            this.cboProgramas.Location = new System.Drawing.Point(82, 17);
            this.cboProgramas.MostrarToolTip = false;
            this.cboProgramas.Name = "cboProgramas";
            this.cboProgramas.Size = new System.Drawing.Size(663, 21);
            this.cboProgramas.TabIndex = 13;
            this.cboProgramas.SelectedIndexChanged += new System.EventHandler(this.cboProgramas_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Programa :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmSubClientes
            // 
            this.tmSubClientes.Interval = 500;
            this.tmSubClientes.Tick += new System.EventHandler(this.tmSubClientes_Tick);
            // 
            // FrmEstadosFarmacias_ClientesProgramas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 681);
            this.Controls.Add(this.FrameProgramas);
            this.Controls.Add(this.FrameSubClientes);
            this.Controls.Add(this.FrameEstados);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmEstadosFarmacias_ClientesProgramas";
            this.Text = "Configuración de Programas por Cliente por Farmacia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmEstadosFarmacias_ClientesProgramas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameEstados.ResumeLayout(false);
            this.FrameSubClientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubClientes_Sheet1)).EndInit();
            this.FrameProgramas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubProgramas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubProgramas_Sheet1)).EndInit();
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
        private System.Windows.Forms.GroupBox FrameSubClientes;
        private System.Windows.Forms.GroupBox FrameProgramas;
        private SC_ControlsCS.scComboBoxExt cboClientes;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboProgramas;
        private System.Windows.Forms.Label label3;
        private FarPoint.Win.Spread.FpSpread grdSubClientes;
        private FarPoint.Win.Spread.SheetView grdSubClientes_Sheet1;
        private FarPoint.Win.Spread.FpSpread grdSubProgramas;
        private FarPoint.Win.Spread.SheetView grdSubProgramas_Sheet1;
        private System.Windows.Forms.Timer tmSubClientes;
        private System.Windows.Forms.Button btnAsignarProgramas;
    }
}