namespace OficinaCentral.CuadrosBasicos
{
    partial class FrmCB_ClavesExclusionPedidos
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCB_ClavesExclusionPedidos));
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSubClientes = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboClientes = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameClaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameClaves
            // 
            this.FrameClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameClaves.Controls.Add(this.chkTodas);
            this.FrameClaves.Controls.Add(this.grdClaves);
            this.FrameClaves.Location = new System.Drawing.Point(7, 116);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(1036, 488);
            this.FrameClaves.TabIndex = 2;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Claves SSA";
            // 
            // chkTodas
            // 
            this.chkTodas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTodas.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.Location = new System.Drawing.Point(928, 3);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(98, 15);
            this.chkTodas.TabIndex = 1;
            this.chkTodas.Text = "Marcar Todas";
            this.chkTodas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(9, 23);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(1017, 458);
            this.grdClaves.TabIndex = 0;
            this.grdClaves.EditModeOn += new System.EventHandler(this.grdClaves_EditModeOn);
            this.grdClaves.EditModeOff += new System.EventHandler(this.grdClaves_EditModeOff);
            this.grdClaves.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdClaves_Advance);
            this.grdClaves.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdClaves_KeyDown);
            // 
            // grdClaves_Sheet1
            // 
            this.grdClaves_Sheet1.Reset();
            this.grdClaves_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdClaves_Sheet1.ColumnCount = 6;
            this.grdClaves_Sheet1.RowCount = 16;
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Id Clave";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Clave SSA";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Descripción Clave";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Tipo Insumo";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Bandera";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType5;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Id Clave";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = false;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 150F;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Descripción Clave";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 500F;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Tipo Insumo";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 150F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType2;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Bandera";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = checkBoxCellType2;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 100F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboSubClientes);
            this.groupBox3.Controls.Add(this.lblEstado);
            this.groupBox3.Controls.Add(this.cboEstados);
            this.groupBox3.Controls.Add(this.lblEmpresa);
            this.groupBox3.Controls.Add(this.cboClientes);
            this.groupBox3.Location = new System.Drawing.Point(7, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1036, 85);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Estado y Clientes";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(535, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Sub-Clientes :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubClientes
            // 
            this.cboSubClientes.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubClientes.Data = "";
            this.cboSubClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubClientes.Filtro = " 1 = 1";
            this.cboSubClientes.ListaItemsBusqueda = 20;
            this.cboSubClientes.Location = new System.Drawing.Point(610, 53);
            this.cboSubClientes.MostrarToolTip = false;
            this.cboSubClientes.Name = "cboSubClientes";
            this.cboSubClientes.Size = new System.Drawing.Size(416, 21);
            this.cboSubClientes.TabIndex = 2;
            this.cboSubClientes.SelectedIndexChanged += new System.EventHandler(this.cboSubClientes_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(1, 27);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(74, 19);
            this.lblEstado.TabIndex = 19;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(79, 26);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(416, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(1, 54);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(74, 19);
            this.lblEmpresa.TabIndex = 18;
            this.lblEmpresa.Text = "Clientes :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClientes
            // 
            this.cboClientes.BackColorEnabled = System.Drawing.Color.White;
            this.cboClientes.Data = "";
            this.cboClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientes.Filtro = " 1 = 1";
            this.cboClientes.ListaItemsBusqueda = 20;
            this.cboClientes.Location = new System.Drawing.Point(79, 53);
            this.cboClientes.MostrarToolTip = false;
            this.cboClientes.Name = "cboClientes";
            this.cboClientes.Size = new System.Drawing.Size(416, 21);
            this.cboClientes.TabIndex = 1;
            this.cboClientes.SelectedIndexChanged += new System.EventHandler(this.cboClientes_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnGuardar,
            this.btnCancelar,
            this.toolStripSeparator3,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1052, 25);
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(23, 22);
            this.btnEjecutar.Text = "&Ejecutar";
            this.btnEjecutar.ToolTipText = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // FrmCB_ClavesExclusionPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 611);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameClaves);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCB_ClavesExclusionPedidos";
            this.Text = "Configuración de Claves excluidas de pedidos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGruposUsuarios_Load);
            this.FrameClaves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClaves_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblEmpresa;
        private SC_ControlsCS.scComboBoxExt cboClientes;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboSubClientes;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}