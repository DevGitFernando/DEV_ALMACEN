namespace OficinaCentral.Catalogos
{
    partial class FrmProveedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProveedores));
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType7 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProveedores = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdDiasDePlazo = new FarPoint.Win.Spread.FpSpread();
            this.grdDiasDePlazo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAliasNombre = new SC_ControlsCS.scTextBoxExt();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCredito = new SC_ControlsCS.scCurrencyBox();
            this.chkSuspendido = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkCredito = new System.Windows.Forms.CheckBox();
            this.txtTelefonos = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCodigoPostal = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomicilio = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboColonia = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMunicipios = new SC_ControlsCS.scComboBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.txtRFC = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtId = new SC_ControlsCS.scTextBoxExt();
            this.txtNombre = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiasDePlazo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiasDePlazo_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnImprimir,
            this.toolStripSeparator3,
            this.btnProveedores});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1034, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "toolStripButton1";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnImprimir.Text = "&Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProveedores
            // 
            this.btnProveedores.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProveedores.Image = ((System.Drawing.Image)(resources.GetObject("btnProveedores.Image")));
            this.btnProveedores.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(23, 22);
            this.btnProveedores.Text = "Generar archivo";
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.grdDiasDePlazo);
            this.groupBox3.Location = new System.Drawing.Point(627, 50);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(383, 258);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Días de credito";
            // 
            // grdDiasDePlazo
            // 
            this.grdDiasDePlazo.AccessibleDescription = "grdDiasDePlazo, Sheet1, Row 0, Column 0, ";
            this.grdDiasDePlazo.BackColor = System.Drawing.Color.Transparent;
            this.grdDiasDePlazo.Location = new System.Drawing.Point(9, 19);
            this.grdDiasDePlazo.Name = "grdDiasDePlazo";
            this.grdDiasDePlazo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdDiasDePlazo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdDiasDePlazo_Sheet1});
            this.grdDiasDePlazo.Size = new System.Drawing.Size(366, 232);
            this.grdDiasDePlazo.TabIndex = 0;
            this.grdDiasDePlazo.EditModeOff += new System.EventHandler(this.grdDiasDePlazo_EditModeOff);
            this.grdDiasDePlazo.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdDiasDePlazo_Advance);
            // 
            // grdDiasDePlazo_Sheet1
            // 
            this.grdDiasDePlazo_Sheet1.Reset();
            this.grdDiasDePlazo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdDiasDePlazo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdDiasDePlazo_Sheet1.ColumnCount = 3;
            this.grdDiasDePlazo_Sheet1.RowCount = 5;
            this.grdDiasDePlazo_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Días";
            this.grdDiasDePlazo_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Activo";
            this.grdDiasDePlazo_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Default";
            this.grdDiasDePlazo_Sheet1.ColumnHeader.Rows.Get(0).Height = 28F;
            numberCellType4.DecimalPlaces = 0;
            numberCellType4.MaximumValue = 360D;
            numberCellType4.MinimumValue = 0D;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).CellType = numberCellType4;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).Label = "Días";
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).Locked = false;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.grdDiasDePlazo_Sheet1.Columns.Get(0).Width = 100F;
            this.grdDiasDePlazo_Sheet1.Columns.Get(1).CellType = checkBoxCellType7;
            this.grdDiasDePlazo_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(1).Label = "Activo";
            this.grdDiasDePlazo_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(1).Width = 90F;
            this.grdDiasDePlazo_Sheet1.Columns.Get(2).CellType = checkBoxCellType8;
            this.grdDiasDePlazo_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(2).Label = "Default";
            this.grdDiasDePlazo_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdDiasDePlazo_Sheet1.Columns.Get(2).Width = 90F;
            this.grdDiasDePlazo_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdDiasDePlazo_Sheet1.RowHeader.Columns.Get(0).Width = 34F;
            this.grdDiasDePlazo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAliasNombre);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtCredito);
            this.groupBox1.Controls.Add(this.chkSuspendido);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.chkCredito);
            this.groupBox1.Controls.Add(this.txtTelefonos);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCodigoPostal);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDomicilio);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboColonia);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboMunicipios);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboEstados);
            this.groupBox1.Controls.Add(this.txtRFC);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblCancelado);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1019, 318);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Proveedor:";
            // 
            // txtAliasNombre
            // 
            this.txtAliasNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAliasNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAliasNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtAliasNombre.Decimales = 2;
            this.txtAliasNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtAliasNombre.ForeColor = System.Drawing.Color.Black;
            this.txtAliasNombre.Location = new System.Drawing.Point(113, 96);
            this.txtAliasNombre.MaxLength = 100;
            this.txtAliasNombre.Name = "txtAliasNombre";
            this.txtAliasNombre.PermitirApostrofo = false;
            this.txtAliasNombre.PermitirNegativos = false;
            this.txtAliasNombre.Size = new System.Drawing.Size(508, 20);
            this.txtAliasNombre.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 22);
            this.label16.TabIndex = 29;
            this.label16.Text = "Alias  :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCredito
            // 
            this.txtCredito.BackColor = System.Drawing.SystemColors.Window;
            this.txtCredito.ColorNumerosNegativos = System.Drawing.Color.Red;
            this.txtCredito.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCredito.Location = new System.Drawing.Point(374, 266);
            this.txtCredito.MaximaLongitud = 12;
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(98, 20);
            this.txtCredito.TabIndex = 13;
            // 
            // chkSuspendido
            // 
            this.chkSuspendido.Location = new System.Drawing.Point(113, 292);
            this.chkSuspendido.Name = "chkSuspendido";
            this.chkSuspendido.Size = new System.Drawing.Size(144, 17);
            this.chkSuspendido.TabIndex = 12;
            this.chkSuspendido.Text = "Credito Suspendido";
            this.chkSuspendido.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(259, 269);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Línea de crédito :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkCredito
            // 
            this.chkCredito.Location = new System.Drawing.Point(113, 267);
            this.chkCredito.Name = "chkCredito";
            this.chkCredito.Size = new System.Drawing.Size(144, 17);
            this.chkCredito.TabIndex = 11;
            this.chkCredito.Text = "Tiene Limite de Credito";
            this.chkCredito.UseVisualStyleBackColor = true;
            this.chkCredito.CheckedChanged += new System.EventHandler(this.chkCredito_CheckedChanged);
            // 
            // txtTelefonos
            // 
            this.txtTelefonos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefonos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelefonos.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtTelefonos.Decimales = 2;
            this.txtTelefonos.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtTelefonos.ForeColor = System.Drawing.Color.Black;
            this.txtTelefonos.Location = new System.Drawing.Point(113, 243);
            this.txtTelefonos.MaxLength = 30;
            this.txtTelefonos.Name = "txtTelefonos";
            this.txtTelefonos.PermitirApostrofo = false;
            this.txtTelefonos.PermitirNegativos = false;
            this.txtTelefonos.Size = new System.Drawing.Size(508, 20);
            this.txtTelefonos.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Telefonos :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoPostal.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCodigoPostal.Decimales = 2;
            this.txtCodigoPostal.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCodigoPostal.ForeColor = System.Drawing.Color.Black;
            this.txtCodigoPostal.Location = new System.Drawing.Point(113, 219);
            this.txtCodigoPostal.MaxLength = 10;
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.PermitirApostrofo = false;
            this.txtCodigoPostal.PermitirNegativos = false;
            this.txtCodigoPostal.Size = new System.Drawing.Size(118, 20);
            this.txtCodigoPostal.TabIndex = 8;
            this.txtCodigoPostal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Codigo Postal :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDomicilio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDomicilio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtDomicilio.Decimales = 2;
            this.txtDomicilio.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtDomicilio.ForeColor = System.Drawing.Color.Black;
            this.txtDomicilio.Location = new System.Drawing.Point(113, 195);
            this.txtDomicilio.MaxLength = 100;
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.PermitirApostrofo = false;
            this.txtDomicilio.PermitirNegativos = false;
            this.txtDomicilio.Size = new System.Drawing.Size(508, 20);
            this.txtDomicilio.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Domicilio :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Colonia :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColonia
            // 
            this.cboColonia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboColonia.BackColorEnabled = System.Drawing.Color.White;
            this.cboColonia.Data = "";
            this.cboColonia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColonia.Filtro = " 1 = 1";
            this.cboColonia.FormattingEnabled = true;
            this.cboColonia.ListaItemsBusqueda = 20;
            this.cboColonia.Location = new System.Drawing.Point(113, 169);
            this.cboColonia.MostrarToolTip = false;
            this.cboColonia.Name = "cboColonia";
            this.cboColonia.Size = new System.Drawing.Size(508, 21);
            this.cboColonia.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Municipio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMunicipios
            // 
            this.cboMunicipios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMunicipios.BackColorEnabled = System.Drawing.Color.White;
            this.cboMunicipios.Data = "";
            this.cboMunicipios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMunicipios.Filtro = " 1 = 1";
            this.cboMunicipios.FormattingEnabled = true;
            this.cboMunicipios.ListaItemsBusqueda = 20;
            this.cboMunicipios.Location = new System.Drawing.Point(113, 144);
            this.cboMunicipios.MostrarToolTip = false;
            this.cboMunicipios.Name = "cboMunicipios";
            this.cboMunicipios.Size = new System.Drawing.Size(508, 21);
            this.cboMunicipios.TabIndex = 5;
            this.cboMunicipios.SelectedIndexChanged += new System.EventHandler(this.cboMunicipios_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Estado :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(113, 119);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(508, 21);
            this.cboEstados.TabIndex = 4;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // txtRFC
            // 
            this.txtRFC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtRFC.Decimales = 2;
            this.txtRFC.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtRFC.ForeColor = System.Drawing.Color.Black;
            this.txtRFC.Location = new System.Drawing.Point(113, 73);
            this.txtRFC.MaxLength = 15;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.PermitirApostrofo = false;
            this.txtRFC.PermitirNegativos = false;
            this.txtRFC.Size = new System.Drawing.Size(508, 20);
            this.txtRFC.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "RFC :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.AutoSize = true;
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(295, 29);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(81, 13);
            this.lblCancelado.TabIndex = 4;
            this.lblCancelado.Text = "CANCELADO";
            this.lblCancelado.Visible = false;
            // 
            // txtId
            // 
            this.txtId.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtId.Decimales = 2;
            this.txtId.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtId.ForeColor = System.Drawing.Color.Black;
            this.txtId.Location = new System.Drawing.Point(113, 27);
            this.txtId.MaxLength = 4;
            this.txtId.Name = "txtId";
            this.txtId.PermitirApostrofo = false;
            this.txtId.PermitirNegativos = false;
            this.txtId.Size = new System.Drawing.Size(118, 20);
            this.txtId.TabIndex = 0;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtNombre.Decimales = 2;
            this.txtNombre.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtNombre.ForeColor = System.Drawing.Color.Black;
            this.txtNombre.Location = new System.Drawing.Point(113, 50);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PermitirApostrofo = false;
            this.txtNombre.PermitirNegativos = false;
            this.txtNombre.Size = new System.Drawing.Size(508, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Clave Proveedor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmProveedores";
            this.Text = "Proveedores";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProveedores_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDiasDePlazo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDiasDePlazo_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProveedores;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtAliasNombre;
        private System.Windows.Forms.Label label16;
        private SC_ControlsCS.scCurrencyBox txtCredito;
        private System.Windows.Forms.CheckBox chkSuspendido;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkCredito;
        private SC_ControlsCS.scTextBoxExt txtTelefonos;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtCodigoPostal;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtDomicilio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private SC_ControlsCS.scComboBoxExt cboColonia;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboMunicipios;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private SC_ControlsCS.scTextBoxExt txtRFC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtId;
        private SC_ControlsCS.scTextBoxExt txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdDiasDePlazo;
        private FarPoint.Win.Spread.SheetView grdDiasDePlazo_Sheet1;
    }
}