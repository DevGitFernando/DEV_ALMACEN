namespace Dll_IFacturacion.IntegracionBD
{
    partial class FrmIntegrarInformacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarInformacion));
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrar = new System.Windows.Forms.ToolStripButton();
            this.FrameVigencia = new System.Windows.Forms.GroupBox();
            this.cboBD = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.grdTablas = new FarPoint.Win.Spread.FpSpread();
            this.grdTablas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblIntegrando = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameVigencia.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTablas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTablas_Sheet1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnIntegrar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(578, 25);
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
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrar
            // 
            this.btnIntegrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrar.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrar.Image")));
            this.btnIntegrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrar.Name = "btnIntegrar";
            this.btnIntegrar.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrar.Text = "Integrar Tablas";
            this.btnIntegrar.Click += new System.EventHandler(this.btnIntegrar_Click);
            // 
            // FrameVigencia
            // 
            this.FrameVigencia.Controls.Add(this.cboBD);
            this.FrameVigencia.Controls.Add(this.label8);
            this.FrameVigencia.Location = new System.Drawing.Point(12, 28);
            this.FrameVigencia.Name = "FrameVigencia";
            this.FrameVigencia.Size = new System.Drawing.Size(553, 59);
            this.FrameVigencia.TabIndex = 3;
            this.FrameVigencia.TabStop = false;
            this.FrameVigencia.Text = "Base de Datos";
            // 
            // cboBD
            // 
            this.cboBD.BackColorEnabled = System.Drawing.Color.White;
            this.cboBD.Data = "";
            this.cboBD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBD.Filtro = " 1 = 1";
            this.cboBD.FormattingEnabled = true;
            this.cboBD.ListaItemsBusqueda = 20;
            this.cboBD.Location = new System.Drawing.Point(133, 23);
            this.cboBD.MostrarToolTip = false;
            this.cboBD.Name = "cboBD";
            this.cboBD.Size = new System.Drawing.Size(376, 21);
            this.cboBD.TabIndex = 43;
            this.cboBD.SelectedIndexChanged += new System.EventHandler(this.cboBD_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(40, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Base de Datos :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.chkTodas);
            this.FrameClaves.Controls.Add(this.grdTablas);
            this.FrameClaves.Location = new System.Drawing.Point(12, 95);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(553, 297);
            this.FrameClaves.TabIndex = 6;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Tablas a Integrar";
            // 
            // grdTablas
            // 
            this.grdTablas.AccessibleDescription = "grdTablas, Sheet1, Row 0, Column 0, ";
            this.grdTablas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdTablas.Location = new System.Drawing.Point(9, 21);
            this.grdTablas.Name = "grdTablas";
            this.grdTablas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdTablas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdTablas_Sheet1});
            this.grdTablas.Size = new System.Drawing.Size(538, 263);
            this.grdTablas.TabIndex = 0;
            // 
            // grdTablas_Sheet1
            // 
            this.grdTablas_Sheet1.Reset();
            this.grdTablas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdTablas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdTablas_Sheet1.ColumnCount = 5;
            this.grdTablas_Sheet1.RowCount = 10;
            this.grdTablas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "IdEnvio";
            this.grdTablas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "IdOrden";
            this.grdTablas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Nombre tabla";
            this.grdTablas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Integrar";
            this.grdTablas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status";
            this.grdTablas_Sheet1.ColumnHeader.Rows.Get(0).Height = 41F;
            numberCellType1.DecimalPlaces = 0;
            numberCellType1.MaximumValue = 10000000D;
            numberCellType1.MinimumValue = -10000000D;
            this.grdTablas_Sheet1.Columns.Get(0).CellType = numberCellType1;
            this.grdTablas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(0).Label = "IdEnvio";
            this.grdTablas_Sheet1.Columns.Get(0).Locked = true;
            this.grdTablas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(0).Visible = false;
            this.grdTablas_Sheet1.Columns.Get(0).Width = 84F;
            numberCellType2.DecimalPlaces = 0;
            numberCellType2.MaximumValue = 10000000D;
            numberCellType2.MinimumValue = -10000000D;
            this.grdTablas_Sheet1.Columns.Get(1).CellType = numberCellType2;
            this.grdTablas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(1).Label = "IdOrden";
            this.grdTablas_Sheet1.Columns.Get(1).Locked = true;
            this.grdTablas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(1).Visible = false;
            this.grdTablas_Sheet1.Columns.Get(1).Width = 100F;
            this.grdTablas_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.grdTablas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdTablas_Sheet1.Columns.Get(2).Label = "Nombre tabla";
            this.grdTablas_Sheet1.Columns.Get(2).Locked = true;
            this.grdTablas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(2).Width = 294F;
            this.grdTablas_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdTablas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(3).Label = "Integrar";
            this.grdTablas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(4).CellType = textCellType2;
            this.grdTablas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(4).Label = "Status";
            this.grdTablas_Sheet1.Columns.Get(4).Locked = true;
            this.grdTablas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdTablas_Sheet1.Columns.Get(4).Width = 140F;
            this.grdTablas_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdTablas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblIntegrando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(12, 396);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(553, 64);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de colores para la Integración de Tablas";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(177, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Integración con exito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Integrando :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(493, 24);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 25);
            this.lblFinError.TabIndex = 12;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntegrando
            // 
            this.lblIntegrando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIntegrando.Location = new System.Drawing.Point(100, 24);
            this.lblIntegrando.Name = "lblIntegrando";
            this.lblIntegrando.Size = new System.Drawing.Size(25, 25);
            this.lblIntegrando.TabIndex = 16;
            this.lblIntegrando.Text = "label2";
            this.lblIntegrando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(366, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(289, 24);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 25);
            this.lblFinExito.TabIndex = 14;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // chkTodas
            // 
            this.chkTodas.Location = new System.Drawing.Point(417, -1);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(122, 17);
            this.chkTodas.TabIndex = 4;
            this.chkTodas.Text = " Seleccionar Todas";
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // FrmIntegrarInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 473);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.FrameVigencia);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmIntegrarInformacion";
            this.Text = "Integrar Información de Base de Datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIntegrarInformacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameVigencia.ResumeLayout(false);
            this.FrameClaves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTablas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTablas_Sheet1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnIntegrar;
        private System.Windows.Forms.GroupBox FrameVigencia;
        private SC_ControlsCS.scComboBoxExt cboBD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox FrameClaves;
        private FarPoint.Win.Spread.FpSpread grdTablas;
        private FarPoint.Win.Spread.SheetView grdTablas_Sheet1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label lblIntegrando;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.CheckBox chkTodas;
    }
}