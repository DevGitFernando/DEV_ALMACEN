namespace Planeacion.ObtenerInformacion
{
    partial class FrmDescargarInformacionOperativa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDescargarInformacionOperativa));
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.lblFolio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nmMeses = new System.Windows.Forms.NumericUpDown();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.lblTitulo__Estado = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdConsumos = new FarPoint.Win.Spread.FpSpread();
            this.grdConsumos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerarPlaneacion = new System.Windows.Forms.ToolStripButton();
            this.grVta = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFechaRegistro = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nmMesesCaducidad = new System.Windows.Forms.NumericUpDown();
            this.btnActulizarExistencia = new System.Windows.Forms.Button();
            this.lblExistencia = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMeses)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).BeginInit();
            this.toolStripBarraMenu.SuspendLayout();
            this.grVta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCaducidad)).BeginInit();
            this.SuspendLayout();
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.txtFolio);
            this.FrameDatos.Controls.Add(this.lblFolio);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.label6);
            this.FrameDatos.Controls.Add(this.nmMeses);
            this.FrameDatos.Controls.Add(this.cboEstados);
            this.FrameDatos.Controls.Add(this.lblTitulo__Estado);
            this.FrameDatos.Location = new System.Drawing.Point(11, 26);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(677, 84);
            this.FrameDatos.TabIndex = 1;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos generales";
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(564, 52);
            this.txtFolio.MaxLength = 8;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 51;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblFolio
            // 
            this.lblFolio.Location = new System.Drawing.Point(493, 56);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(69, 12);
            this.lblFolio.TabIndex = 52;
            this.lblFolio.Text = "Folio :";
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(191, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "meses de información historica";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(19, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 14);
            this.label6.TabIndex = 50;
            this.label6.Text = "Descargar";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMeses
            // 
            this.nmMeses.Location = new System.Drawing.Point(126, 52);
            this.nmMeses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmMeses.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nmMeses.Name = "nmMeses";
            this.nmMeses.Size = new System.Drawing.Size(58, 20);
            this.nmMeses.TabIndex = 1;
            this.nmMeses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMeses.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmMeses.ValueChanged += new System.EventHandler(this.nmMeses_ValueChanged);
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.ListaItemsBusqueda = 20;
            this.cboEstados.Location = new System.Drawing.Point(126, 24);
            this.cboEstados.MostrarToolTip = false;
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(538, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // lblTitulo__Estado
            // 
            this.lblTitulo__Estado.Location = new System.Drawing.Point(19, 28);
            this.lblTitulo__Estado.Name = "lblTitulo__Estado";
            this.lblTitulo__Estado.Size = new System.Drawing.Size(102, 13);
            this.lblTitulo__Estado.TabIndex = 48;
            this.lblTitulo__Estado.Text = "Estado :";
            this.lblTitulo__Estado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grdConsumos);
            this.groupBox1.Location = new System.Drawing.Point(11, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(677, 399);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Descargar información de consumo";
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
            this.grdConsumos.Size = new System.Drawing.Size(654, 365);
            this.grdConsumos.TabIndex = 0;
            this.grdConsumos.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.grdConsumos_CellClick);
            this.grdConsumos.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.grdConsumos_ButtonClicked);
            // 
            // grdConsumos_Sheet1
            // 
            this.grdConsumos_Sheet1.Reset();
            this.grdConsumos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdConsumos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdConsumos_Sheet1.ColumnCount = 5;
            this.grdConsumos_Sheet1.RowCount = 10;
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEstado";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Fecha";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Consumos";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Fecha Actualizado";
            this.grdConsumos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Actualizar";
            this.grdConsumos_Sheet1.ColumnHeader.Rows.Get(0).Height = 40F;
            this.grdConsumos_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(0).Label = "IdEstado";
            this.grdConsumos_Sheet1.Columns.Get(0).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(0).Visible = false;
            this.grdConsumos_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(1).Label = "Fecha";
            this.grdConsumos_Sheet1.Columns.Get(1).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(1).Width = 100F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MaximumValue = 999999999.99D;
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdConsumos_Sheet1.Columns.Get(2).CellType = numberCellType1;
            this.grdConsumos_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdConsumos_Sheet1.Columns.Get(2).Label = "Consumos";
            this.grdConsumos_Sheet1.Columns.Get(2).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(2).Width = 184F;
            this.grdConsumos_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Label = "Fecha Actualizado";
            this.grdConsumos_Sheet1.Columns.Get(3).Locked = true;
            this.grdConsumos_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(3).Width = 193F;
            buttonCellType1.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;
            buttonCellType1.Text = "...";
            this.grdConsumos_Sheet1.Columns.Get(4).CellType = buttonCellType1;
            this.grdConsumos_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(4).Label = "Actualizar";
            this.grdConsumos_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdConsumos_Sheet1.Columns.Get(4).Width = 90F;
            this.grdConsumos_Sheet1.RowHeader.Columns.Default.Resizable = true;
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
            this.btnGenerarPlaneacion});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(701, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerarPlaneacion
            // 
            this.btnGenerarPlaneacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerarPlaneacion.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarPlaneacion.Image")));
            this.btnGenerarPlaneacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPlaneacion.Name = "btnGenerarPlaneacion";
            this.btnGenerarPlaneacion.Size = new System.Drawing.Size(23, 22);
            this.btnGenerarPlaneacion.Text = "Generar Planeación";
            this.btnGenerarPlaneacion.Click += new System.EventHandler(this.btnGenerarPlaneacion_Click);
            // 
            // grVta
            // 
            this.grVta.Controls.Add(this.label5);
            this.grVta.Controls.Add(this.label4);
            this.grVta.Controls.Add(this.lblFechaRegistro);
            this.grVta.Controls.Add(this.label1);
            this.grVta.Controls.Add(this.nmMesesCaducidad);
            this.grVta.Controls.Add(this.btnActulizarExistencia);
            this.grVta.Controls.Add(this.lblExistencia);
            this.grVta.Controls.Add(this.label2);
            this.grVta.Location = new System.Drawing.Point(11, 511);
            this.grVta.Name = "grVta";
            this.grVta.Size = new System.Drawing.Size(677, 105);
            this.grVta.TabIndex = 3;
            this.grVta.TabStop = false;
            this.grVta.Text = "Descargar información de existencias";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 16);
            this.label5.TabIndex = 54;
            this.label5.Text = "Actualizado al día :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(191, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 14);
            this.label4.TabIndex = 53;
            this.label4.Text = "meses de caducidad";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFechaRegistro
            // 
            this.lblFechaRegistro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFechaRegistro.Location = new System.Drawing.Point(126, 45);
            this.lblFechaRegistro.Name = "lblFechaRegistro";
            this.lblFechaRegistro.Size = new System.Drawing.Size(229, 21);
            this.lblFechaRegistro.TabIndex = 1;
            this.lblFechaRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 52;
            this.label1.Text = "Validar";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmMesesCaducidad
            // 
            this.nmMesesCaducidad.Location = new System.Drawing.Point(126, 71);
            this.nmMesesCaducidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nmMesesCaducidad.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmMesesCaducidad.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmMesesCaducidad.Name = "nmMesesCaducidad";
            this.nmMesesCaducidad.Size = new System.Drawing.Size(58, 20);
            this.nmMesesCaducidad.TabIndex = 2;
            this.nmMesesCaducidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmMesesCaducidad.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmMesesCaducidad.ValueChanged += new System.EventHandler(this.nmMesesCaducidad_ValueChanged);
            // 
            // btnActulizarExistencia
            // 
            this.btnActulizarExistencia.Location = new System.Drawing.Point(508, 20);
            this.btnActulizarExistencia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnActulizarExistencia.Name = "btnActulizarExistencia";
            this.btnActulizarExistencia.Size = new System.Drawing.Size(156, 71);
            this.btnActulizarExistencia.TabIndex = 3;
            this.btnActulizarExistencia.Text = "Actualizar información de existencias";
            this.btnActulizarExistencia.UseVisualStyleBackColor = true;
            this.btnActulizarExistencia.Click += new System.EventHandler(this.btnActulizarExistencia_Click);
            // 
            // lblExistencia
            // 
            this.lblExistencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExistencia.Location = new System.Drawing.Point(126, 20);
            this.lblExistencia.Name = "lblExistencia";
            this.lblExistencia.Size = new System.Drawing.Size(229, 21);
            this.lblExistencia.TabIndex = 0;
            this.lblExistencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Existencia :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDescargarInformacionOperativa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 622);
            this.Controls.Add(this.grVta);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDatos);
            this.Name = "FrmDescargarInformacionOperativa";
            this.Text = "Información de operación";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmObtenerConsumos_Load);
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmMeses)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos_Sheet1)).EndInit();
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.grVta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmMesesCaducidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label lblTitulo__Estado;
        private System.Windows.Forms.GroupBox groupBox1;
        private FarPoint.Win.Spread.FpSpread grdConsumos;
        private FarPoint.Win.Spread.SheetView grdConsumos_Sheet1;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnGenerarPlaneacion;
        private System.Windows.Forms.NumericUpDown nmMeses;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grVta;
        private System.Windows.Forms.Label lblExistencia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActulizarExistencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmMesesCaducidad;
        private System.Windows.Forms.Label lblFechaRegistro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label lblFolio;
    }
}