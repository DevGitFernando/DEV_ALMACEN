namespace OficinaCentral.CuadrosBasicos
{
    partial class FrmSubCuadroBasico
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType11 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType12 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType7 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType8 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSubCuadroBasico));
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.grdClaves = new FarPoint.Win.Spread.FpSpread();
            this.grdClaves_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSubPrograma = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.cboNivel = new SC_ControlsCS.scComboBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrograma = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cboCliente = new SC_ControlsCS.scComboBoxExt();
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
            this.FrameClaves.Controls.Add(this.chkTodas);
            this.FrameClaves.Controls.Add(this.grdClaves);
            this.FrameClaves.Location = new System.Drawing.Point(7, 140);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(976, 342);
            this.FrameClaves.TabIndex = 2;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Claves SSA";
            // 
            // chkTodas
            // 
            this.chkTodas.Location = new System.Drawing.Point(874, 11);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(94, 17);
            this.chkTodas.TabIndex = 4;
            this.chkTodas.Text = "Marcar Todas";
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // grdClaves
            // 
            this.grdClaves.AccessibleDescription = "grdClaves, Sheet1, Row 0, Column 0, ";
            this.grdClaves.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdClaves.Location = new System.Drawing.Point(9, 31);
            this.grdClaves.Name = "grdClaves";
            this.grdClaves.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdClaves.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdClaves_Sheet1});
            this.grdClaves.Size = new System.Drawing.Size(961, 305);
            this.grdClaves.TabIndex = 1;
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
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Cantidad";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Bandera";
            this.grdClaves_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.ColumnHeader.Rows.Get(0).Height = 39F;
            this.grdClaves_Sheet1.Columns.Get(0).CellType = textCellType10;
            this.grdClaves_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Label = "Id Clave";
            this.grdClaves_Sheet1.Columns.Get(0).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(0).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(1).CellType = textCellType11;
            this.grdClaves_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Label = "Clave SSA";
            this.grdClaves_Sheet1.Columns.Get(1).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(1).Width = 190F;
            this.grdClaves_Sheet1.Columns.Get(2).CellType = textCellType12;
            this.grdClaves_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdClaves_Sheet1.Columns.Get(2).Label = "Descripción Clave";
            this.grdClaves_Sheet1.Columns.Get(2).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(2).Width = 500F;
            numberCellType7.DecimalPlaces = 0;
            numberCellType7.DecimalSeparator = ".";
            numberCellType7.MaximumValue = 10000000D;
            numberCellType7.MinimumValue = -10000000D;
            numberCellType7.Separator = ",";
            numberCellType7.ShowSeparator = true;
            this.grdClaves_Sheet1.Columns.Get(3).CellType = numberCellType7;
            this.grdClaves_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdClaves_Sheet1.Columns.Get(3).Label = "Cantidad";
            this.grdClaves_Sheet1.Columns.Get(3).Locked = false;
            this.grdClaves_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(3).Width = 100F;
            numberCellType8.DecimalPlaces = 0;
            numberCellType8.MaximumValue = 10000000D;
            numberCellType8.MinimumValue = -10000000D;
            this.grdClaves_Sheet1.Columns.Get(4).CellType = numberCellType8;
            this.grdClaves_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Label = "Bandera";
            this.grdClaves_Sheet1.Columns.Get(4).Locked = true;
            this.grdClaves_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(4).Visible = false;
            this.grdClaves_Sheet1.Columns.Get(5).CellType = checkBoxCellType4;
            this.grdClaves_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Label = "Marcar / Desmarcar";
            this.grdClaves_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdClaves_Sheet1.Columns.Get(5).Width = 100F;
            this.grdClaves_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdClaves_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cboSubPrograma);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cboNivel);
            this.groupBox3.Controls.Add(this.cboFarmacias);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboPrograma);
            this.groupBox3.Controls.Add(this.lblEstado);
            this.groupBox3.Controls.Add(this.cboEstados);
            this.groupBox3.Controls.Add(this.lblEmpresa);
            this.groupBox3.Controls.Add(this.cboCliente);
            this.groupBox3.Location = new System.Drawing.Point(7, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(976, 106);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Estado y Clientes";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(469, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Sub-Programa :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboSubPrograma
            // 
            this.cboSubPrograma.BackColorEnabled = System.Drawing.Color.White;
            this.cboSubPrograma.Data = "";
            this.cboSubPrograma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubPrograma.Filtro = " 1 = 1";
            this.cboSubPrograma.ListaItemsBusqueda = 20;
            this.cboSubPrograma.Location = new System.Drawing.Point(551, 75);
            this.cboSubPrograma.MostrarToolTip = false;
            this.cboSubPrograma.Name = "cboSubPrograma";
            this.cboSubPrograma.Size = new System.Drawing.Size(412, 21);
            this.cboSubPrograma.TabIndex = 5;
            this.cboSubPrograma.SelectedIndexChanged += new System.EventHandler(this.cboSubPrograma_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(497, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Nivel :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNivel
            // 
            this.cboNivel.BackColorEnabled = System.Drawing.Color.White;
            this.cboNivel.Data = "";
            this.cboNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNivel.Filtro = " 1 = 1";
            this.cboNivel.ListaItemsBusqueda = 20;
            this.cboNivel.Location = new System.Drawing.Point(551, 48);
            this.cboNivel.MostrarToolTip = false;
            this.cboNivel.Name = "cboNivel";
            this.cboNivel.Size = new System.Drawing.Size(412, 21);
            this.cboNivel.TabIndex = 3;
            this.cboNivel.SelectedIndexChanged += new System.EventHandler(this.cboNivel_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(490, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Farmacia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(551, 21);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(412, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.SelectedIndexChanged += new System.EventHandler(this.cboFarmacias_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Programa :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPrograma
            // 
            this.cboPrograma.BackColorEnabled = System.Drawing.Color.White;
            this.cboPrograma.Data = "";
            this.cboPrograma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrograma.Filtro = " 1 = 1";
            this.cboPrograma.ListaItemsBusqueda = 20;
            this.cboPrograma.Location = new System.Drawing.Point(69, 74);
            this.cboPrograma.MostrarToolTip = false;
            this.cboPrograma.Name = "cboPrograma";
            this.cboPrograma.Size = new System.Drawing.Size(382, 21);
            this.cboPrograma.TabIndex = 4;
            this.cboPrograma.SelectedIndexChanged += new System.EventHandler(this.cboPrograma_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(15, 22);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(53, 19);
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
            this.cboEstados.Location = new System.Drawing.Point(69, 20);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(382, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Location = new System.Drawing.Point(15, 49);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(53, 17);
            this.lblEmpresa.TabIndex = 18;
            this.lblEmpresa.Text = "Cliente :";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCliente
            // 
            this.cboCliente.BackColorEnabled = System.Drawing.Color.White;
            this.cboCliente.Data = "";
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.Filtro = " 1 = 1";
            this.cboCliente.ListaItemsBusqueda = 20;
            this.cboCliente.Location = new System.Drawing.Point(69, 47);
            this.cboCliente.MostrarToolTip = false;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(382, 21);
            this.cboCliente.TabIndex = 2;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(994, 25);
            this.toolStripBarraMenu.TabIndex = 5;
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
            // FrmSubCuadroBasico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 493);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameClaves);
            this.MaximumSize = new System.Drawing.Size(1000, 522);
            this.Name = "FrmSubCuadroBasico";
            this.Text = "Configuración de Claves para Sub-Cuadro Básico";
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
        private SC_ControlsCS.scComboBoxExt cboCliente;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboPrograma;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private FarPoint.Win.Spread.FpSpread grdClaves;
        private FarPoint.Win.Spread.SheetView grdClaves_Sheet1;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboSubPrograma;
        private System.Windows.Forms.Label label3;
        private SC_ControlsCS.scComboBoxExt cboNivel;
        private System.Windows.Forms.Label label2;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;

    }
}