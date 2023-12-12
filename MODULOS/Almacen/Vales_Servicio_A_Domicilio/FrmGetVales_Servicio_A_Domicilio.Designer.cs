namespace Almacen.Vales_Servicio_A_Domicilio
{
    partial class FrmGetVales_Servicio_A_Domicilio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGetVales_Servicio_A_Domicilio));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActivarServicios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIntegrarPaquetesDeDatos = new System.Windows.Forms.ToolStripButton();
            this.FrameJurisdicciones = new System.Windows.Forms.GroupBox();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdUnidades = new FarPoint.Win.Spread.FpSpread();
            this.grdUnidades_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.FrameSeleccion = new System.Windows.Forms.GroupBox();
            this.chkTodas = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblFinError = new System.Windows.Forms.Label();
            this.lblConsultando = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFinExito = new System.Windows.Forms.Label();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameDiasRevision = new System.Windows.Forms.GroupBox();
            this.nmDiasRevision = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameJurisdicciones.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).BeginInit();
            this.FrameSeleccion.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.FrameDiasRevision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasRevision)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator4,
            this.btnActivarServicios,
            this.toolStripSeparator1,
            this.btnIntegrarPaquetesDeDatos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(891, 25);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnActivarServicios
            // 
            this.btnActivarServicios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActivarServicios.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarServicios.Image")));
            this.btnActivarServicios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActivarServicios.Name = "btnActivarServicios";
            this.btnActivarServicios.Size = new System.Drawing.Size(23, 22);
            this.btnActivarServicios.Text = "Recolectar pedidos";
            this.btnActivarServicios.Click += new System.EventHandler(this.btnActivarServicios_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnIntegrarPaquetesDeDatos
            // 
            this.btnIntegrarPaquetesDeDatos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIntegrarPaquetesDeDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnIntegrarPaquetesDeDatos.Image")));
            this.btnIntegrarPaquetesDeDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIntegrarPaquetesDeDatos.Name = "btnIntegrarPaquetesDeDatos";
            this.btnIntegrarPaquetesDeDatos.Size = new System.Drawing.Size(23, 22);
            this.btnIntegrarPaquetesDeDatos.Text = "Integrar servicios a domicilio";
            this.btnIntegrarPaquetesDeDatos.Click += new System.EventHandler(this.btnIntegrarPaquetesDeDatos_Click);
            // 
            // FrameJurisdicciones
            // 
            this.FrameJurisdicciones.Controls.Add(this.cboJurisdicciones);
            this.FrameJurisdicciones.Controls.Add(this.label1);
            this.FrameJurisdicciones.Location = new System.Drawing.Point(11, 27);
            this.FrameJurisdicciones.Name = "FrameJurisdicciones";
            this.FrameJurisdicciones.Size = new System.Drawing.Size(481, 49);
            this.FrameJurisdicciones.TabIndex = 1;
            this.FrameJurisdicciones.TabStop = false;
            this.FrameJurisdicciones.Text = "Información de Jurisdicciones";
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(96, 19);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(379, 21);
            this.cboJurisdicciones.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdUnidades);
            this.groupBox3.Location = new System.Drawing.Point(11, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(870, 353);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lista de Unidades";
            // 
            // grdUnidades
            // 
            this.grdUnidades.AccessibleDescription = "grdUnidades, Sheet1, Row 0, Column 0, ";
            this.grdUnidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdUnidades.Location = new System.Drawing.Point(11, 17);
            this.grdUnidades.Name = "grdUnidades";
            this.grdUnidades.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdUnidades.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
            this.grdUnidades.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdUnidades_Sheet1});
            this.grdUnidades.Size = new System.Drawing.Size(846, 325);
            this.grdUnidades.TabIndex = 0;
            // 
            // grdUnidades_Sheet1
            // 
            this.grdUnidades_Sheet1.Reset();
            this.grdUnidades_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdUnidades_Sheet1.ColumnCount = 5;
            this.grdUnidades_Sheet1.RowCount = 15;
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Farmacia";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Nombre";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Url";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Recolectar";
            this.grdUnidades_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Status";
            this.grdUnidades_Sheet1.ColumnHeader.Rows.Get(0).Height = 24F;
            textCellType1.MaxLength = 6;
            this.grdUnidades_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdUnidades_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Label = "Farmacia";
            this.grdUnidades_Sheet1.Columns.Get(0).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(0).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdUnidades_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(1).Label = "Nombre";
            this.grdUnidades_Sheet1.Columns.Get(1).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(1).Width = 380F;
            textCellType3.MaxLength = 500;
            this.grdUnidades_Sheet1.Columns.Get(2).CellType = textCellType3;
            this.grdUnidades_Sheet1.Columns.Get(2).Label = "Url";
            this.grdUnidades_Sheet1.Columns.Get(2).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(2).Visible = false;
            this.grdUnidades_Sheet1.Columns.Get(2).Width = 114F;
            this.grdUnidades_Sheet1.Columns.Get(3).CellType = checkBoxCellType1;
            this.grdUnidades_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Label = "Recolectar";
            this.grdUnidades_Sheet1.Columns.Get(3).Locked = false;
            this.grdUnidades_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(3).Width = 70F;
            this.grdUnidades_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.grdUnidades_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdUnidades_Sheet1.Columns.Get(4).Label = "Status";
            this.grdUnidades_Sheet1.Columns.Get(4).Locked = true;
            this.grdUnidades_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdUnidades_Sheet1.Columns.Get(4).Width = 270F;
            this.grdUnidades_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdUnidades_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrameSeleccion
            // 
            this.FrameSeleccion.Controls.Add(this.chkTodas);
            this.FrameSeleccion.Location = new System.Drawing.Point(672, 27);
            this.FrameSeleccion.Name = "FrameSeleccion";
            this.FrameSeleccion.Size = new System.Drawing.Size(209, 49);
            this.FrameSeleccion.TabIndex = 3;
            this.FrameSeleccion.TabStop = false;
            // 
            // chkTodas
            // 
            this.chkTodas.Location = new System.Drawing.Point(15, 15);
            this.chkTodas.Name = "chkTodas";
            this.chkTodas.Size = new System.Drawing.Size(176, 24);
            this.chkTodas.TabIndex = 0;
            this.chkTodas.Text = " Seleccionar todas las unidades";
            this.chkTodas.UseVisualStyleBackColor = true;
            this.chkTodas.CheckedChanged += new System.EventHandler(this.chkTodas_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.lblFinError);
            this.groupBox4.Controls.Add(this.lblConsultando);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblFinExito);
            this.groupBox4.Location = new System.Drawing.Point(11, 432);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(870, 44);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Código de colores para recolección de pedidos";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(360, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Ejecución con éxito :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(121, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Consultando :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinError
            // 
            this.lblFinError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinError.Location = new System.Drawing.Point(724, 18);
            this.lblFinError.Name = "lblFinError";
            this.lblFinError.Size = new System.Drawing.Size(25, 21);
            this.lblFinError.TabIndex = 2;
            this.lblFinError.Text = "label2";
            this.lblFinError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsultando
            // 
            this.lblConsultando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConsultando.Location = new System.Drawing.Point(210, 18);
            this.lblConsultando.Name = "lblConsultando";
            this.lblConsultando.Size = new System.Drawing.Size(25, 21);
            this.lblConsultando.TabIndex = 0;
            this.lblConsultando.Text = "label2";
            this.lblConsultando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(597, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ejecución con errores :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinExito
            // 
            this.lblFinExito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFinExito.Location = new System.Drawing.Point(472, 18);
            this.lblFinExito.Name = "lblFinExito";
            this.lblFinExito.Size = new System.Drawing.Size(25, 21);
            this.lblFinExito.TabIndex = 1;
            this.lblFinExito.Text = "label2";
            this.lblFinExito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameDiasRevision
            // 
            this.FrameDiasRevision.Controls.Add(this.label2);
            this.FrameDiasRevision.Controls.Add(this.nmDiasRevision);
            this.FrameDiasRevision.Location = new System.Drawing.Point(498, 27);
            this.FrameDiasRevision.Name = "FrameDiasRevision";
            this.FrameDiasRevision.Size = new System.Drawing.Size(169, 49);
            this.FrameDiasRevision.TabIndex = 2;
            this.FrameDiasRevision.TabStop = false;
            // 
            // nmDiasRevision
            // 
            this.nmDiasRevision.Location = new System.Drawing.Point(98, 19);
            this.nmDiasRevision.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nmDiasRevision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmDiasRevision.Name = "nmDiasRevision";
            this.nmDiasRevision.Size = new System.Drawing.Size(57, 20);
            this.nmDiasRevision.TabIndex = 0;
            this.nmDiasRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmDiasRevision.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Dias a revisar :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmGetVales_Servicio_A_Domicilio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 486);
            this.Controls.Add(this.FrameDiasRevision);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.FrameSeleccion);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameJurisdicciones);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmGetVales_Servicio_A_Domicilio";
            this.Text = "Recolectar Vales Servicios a Domicilio";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmGetPedidosCEDIS_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameJurisdicciones.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUnidades_Sheet1)).EndInit();
            this.FrameSeleccion.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.FrameDiasRevision.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmDiasRevision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnActivarServicios;
        private System.Windows.Forms.GroupBox FrameJurisdicciones;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private FarPoint.Win.Spread.FpSpread grdUnidades;
        private FarPoint.Win.Spread.SheetView grdUnidades_Sheet1;
        private System.Windows.Forms.GroupBox FrameSeleccion;
        private System.Windows.Forms.CheckBox chkTodas;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFinError;
        private System.Windows.Forms.Label lblConsultando;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFinExito;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnIntegrarPaquetesDeDatos;
        private System.Windows.Forms.GroupBox FrameDiasRevision;
        private System.Windows.Forms.NumericUpDown nmDiasRevision;
        private System.Windows.Forms.Label label2;
    }
}