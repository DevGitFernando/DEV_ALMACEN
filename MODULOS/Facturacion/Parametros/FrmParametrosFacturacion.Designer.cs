namespace Facturacion
{
    partial class FrmParametrosFacturacion
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Picture picture1 = new FarPoint.Win.Picture(null, FarPoint.Win.RenderStyle.Normal, System.Drawing.Color.Empty, 0, FarPoint.Win.HorizontalAlignment.Center, FarPoint.Win.VerticalAlignment.Center);
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParametrosFacturacion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.grdParametros = new FarPoint.Win.Spread.FpSpread();
            this.grdParametros_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboModulo = new SC_ControlsCS.scComboBoxExt();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.grdParametros);
            this.groupBox1.Location = new System.Drawing.Point(7, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1165, 559);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parámetros";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDescripcion.Location = new System.Drawing.Point(11, 499);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(1143, 50);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "label1";
            // 
            // grdParametros
            // 
            this.grdParametros.AccessibleDescription = "grdParametros, Sheet1, Row 0, Column 0, ";
            this.grdParametros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdParametros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdParametros.Location = new System.Drawing.Point(11, 17);
            this.grdParametros.Name = "grdParametros";
            this.grdParametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdParametros.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdParametros_Sheet1});
            this.grdParametros.Size = new System.Drawing.Size(1143, 476);
            this.grdParametros.TabIndex = 0;
            this.grdParametros.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.grdParametros_LeaveCell);
            // 
            // grdParametros_Sheet1
            // 
            this.grdParametros_Sheet1.Reset();
            this.grdParametros_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdParametros_Sheet1.ColumnCount = 8;
            this.grdParametros_Sheet1.RowCount = 12;
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Estado";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Farmacia";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Arbol";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Parámetro";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Valor";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "Descripción";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "EsDeSistema";
            this.grdParametros_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Es Editable";
            this.grdParametros_Sheet1.ColumnHeader.Rows.Get(0).Height = 35F;
            this.grdParametros_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdParametros_Sheet1.Columns.Get(0).Label = "Estado";
            this.grdParametros_Sheet1.Columns.Get(0).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(0).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdParametros_Sheet1.Columns.Get(1).Label = "Farmacia";
            this.grdParametros_Sheet1.Columns.Get(1).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(1).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdParametros_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(2).Label = "Arbol";
            this.grdParametros_Sheet1.Columns.Get(2).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(2).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(2).Width = 87F;
            this.grdParametros_Sheet1.Columns.Get(3).CellType = textCellType4;
            this.grdParametros_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(3).Label = "Parámetro";
            this.grdParametros_Sheet1.Columns.Get(3).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(3).Width = 300F;
            textCellType5.MaxLength = 300;
            this.grdParametros_Sheet1.Columns.Get(4).CellType = textCellType5;
            this.grdParametros_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(4).Label = "Valor";
            this.grdParametros_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(4).Width = 600F;
            this.grdParametros_Sheet1.Columns.Get(5).CellType = textCellType6;
            this.grdParametros_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdParametros_Sheet1.Columns.Get(5).Label = "Descripción";
            this.grdParametros_Sheet1.Columns.Get(5).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(5).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(5).Width = 245F;
            picture1.AlignHorz = FarPoint.Win.HorizontalAlignment.Center;
            picture1.AlignVert = FarPoint.Win.VerticalAlignment.Center;
            picture1.TransparencyColor = System.Drawing.Color.Empty;
            picture1.TransparencyTolerance = 0;
            checkBoxCellType1.BackgroundImage = picture1;
            this.grdParametros_Sheet1.Columns.Get(6).CellType = checkBoxCellType1;
            this.grdParametros_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(6).Label = "EsDeSistema";
            this.grdParametros_Sheet1.Columns.Get(6).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(6).Visible = false;
            this.grdParametros_Sheet1.Columns.Get(6).Width = 93F;
            this.grdParametros_Sheet1.Columns.Get(7).CellType = checkBoxCellType2;
            this.grdParametros_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(7).Label = "Es Editable";
            this.grdParametros_Sheet1.Columns.Get(7).Locked = true;
            this.grdParametros_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdParametros_Sheet1.Columns.Get(7).Width = 80F;
            this.grdParametros_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdParametros_Sheet1.Rows.Default.Height = 25F;
            this.grdParametros_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboModulo);
            this.groupBox3.Controls.Add(this.lblEstado);
            this.groupBox3.Controls.Add(this.cboEstados);
            this.groupBox3.Location = new System.Drawing.Point(7, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1165, 52);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de Operación";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Location = new System.Drawing.Point(620, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Módulo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboModulo
            // 
            this.cboModulo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboModulo.BackColorEnabled = System.Drawing.Color.White;
            this.cboModulo.Data = "";
            this.cboModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulo.Filtro = " 1 = 1";
            this.cboModulo.ListaItemsBusqueda = 20;
            this.cboModulo.Location = new System.Drawing.Point(674, 19);
            this.cboModulo.MostrarToolTip = false;
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(323, 21);
            this.cboModulo.TabIndex = 2;
            this.cboModulo.SelectedIndexChanged += new System.EventHandler(this.cboModulo_SelectedIndexChanged);
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblEstado.Location = new System.Drawing.Point(168, 21);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(52, 17);
            this.lblEstado.TabIndex = 19;
            this.lblEstado.Text = "Estado :";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(221, 19);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(323, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnGuardar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // FrmParametrosFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 645);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmParametrosFacturacion";
            this.Text = "Configuración de Parámetros";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmParametrosFacturacion_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdParametros_Sheet1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDescripcion;
        private FarPoint.Win.Spread.FpSpread grdParametros;
        private FarPoint.Win.Spread.SheetView grdParametros_Sheet1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboModulo;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}