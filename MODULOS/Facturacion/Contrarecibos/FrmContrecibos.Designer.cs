namespace Facturacion.Contrarecibos
{
    partial class FrmContrecibos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContrecibos));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaDoc = new System.Windows.Forms.DateTimePicker();
            this.txtContraRecibo = new SC_ControlsCS.scTextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.FrameFacturas = new System.Windows.Forms.GroupBox();
            this.grdFacturas = new FarPoint.Win.Spread.FpSpread();
            this.grdFacturas_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.scTextBoxExt1 = new SC_ControlsCS.scTextBoxExt();
            this.txtCotizacion = new SC_ControlsCS.scTextBoxExt();
            this.toolMenuFacturacion.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameFacturas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolMenuFacturacion
            // 
            this.toolMenuFacturacion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator3,
            this.btnGuardar,
            this.toolStripSeparator4,
            this.btnCancelar,
            this.toolStripSeparator5,
            this.btnImprimir});
            this.toolMenuFacturacion.Location = new System.Drawing.Point(0, 0);
            this.toolMenuFacturacion.Name = "toolMenuFacturacion";
            this.toolMenuFacturacion.Size = new System.Drawing.Size(707, 25);
            this.toolMenuFacturacion.TabIndex = 3;
            this.toolMenuFacturacion.Text = "toolStrip1";
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.label4);
            this.FrameDatos.Controls.Add(this.dtpFechaDoc);
            this.FrameDatos.Controls.Add(this.txtContraRecibo);
            this.FrameDatos.Controls.Add(this.label3);
            this.FrameDatos.Controls.Add(this.label5);
            this.FrameDatos.Controls.Add(this.txtFolio);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.txtObservaciones);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.dtpFechaRegistro);
            this.FrameDatos.Location = new System.Drawing.Point(10, 28);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(687, 124);
            this.FrameDatos.TabIndex = 0;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos de Contrarecibo";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(456, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "Fecha Documento :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDoc
            // 
            this.dtpFechaDoc.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDoc.Enabled = false;
            this.dtpFechaDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDoc.Location = new System.Drawing.Point(582, 46);
            this.dtpFechaDoc.Name = "dtpFechaDoc";
            this.dtpFechaDoc.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaDoc.TabIndex = 2;
            // 
            // txtContraRecibo
            // 
            this.txtContraRecibo.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtContraRecibo.Decimales = 2;
            this.txtContraRecibo.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtContraRecibo.ForeColor = System.Drawing.Color.Black;
            this.txtContraRecibo.Location = new System.Drawing.Point(126, 44);
            this.txtContraRecibo.MaxLength = 10;
            this.txtContraRecibo.Name = "txtContraRecibo";
            this.txtContraRecibo.PermitirApostrofo = false;
            this.txtContraRecibo.PermitirNegativos = false;
            this.txtContraRecibo.Size = new System.Drawing.Size(215, 20);
            this.txtContraRecibo.TabIndex = 1;
            this.txtContraRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Contrarecibo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(34, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 41;
            this.label5.Text = "Observaciones :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(126, 18);
            this.txtFolio.MaxLength = 10;
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.PermitirApostrofo = false;
            this.txtFolio.PermitirNegativos = false;
            this.txtFolio.Size = new System.Drawing.Size(100, 20);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.Text = "0123456789";
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolio.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolio_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Folio Contrarecibo :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(126, 70);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(549, 42);
            this.txtObservaciones.TabIndex = 3;
            this.txtObservaciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(492, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Fecha Registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(582, 20);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(93, 20);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // FrameFacturas
            // 
            this.FrameFacturas.Controls.Add(this.grdFacturas);
            this.FrameFacturas.Location = new System.Drawing.Point(10, 154);
            this.FrameFacturas.Name = "FrameFacturas";
            this.FrameFacturas.Size = new System.Drawing.Size(687, 280);
            this.FrameFacturas.TabIndex = 1;
            this.FrameFacturas.TabStop = false;
            this.FrameFacturas.Text = "Relación de Facturas";
            // 
            // grdFacturas
            // 
            this.grdFacturas.AccessibleDescription = "grdFacturas, Sheet1, Row 0, Column 0, ";
            this.grdFacturas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFacturas.Location = new System.Drawing.Point(9, 19);
            this.grdFacturas.Name = "grdFacturas";
            this.grdFacturas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFacturas.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFacturas_Sheet1});
            this.grdFacturas.Size = new System.Drawing.Size(666, 253);
            this.grdFacturas.TabIndex = 0;
            this.grdFacturas.EditModeOn += new System.EventHandler(this.grdFacturas_EditModeOn);
            this.grdFacturas.EditModeOff += new System.EventHandler(this.grdFacturas_EditModeOff);
            this.grdFacturas.Advance += new FarPoint.Win.Spread.AdvanceEventHandler(this.grdFacturas_Advance);
            this.grdFacturas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFacturas_KeyDown);
            // 
            // grdFacturas_Sheet1
            // 
            this.grdFacturas_Sheet1.Reset();
            this.grdFacturas_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFacturas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFacturas_Sheet1.ColumnCount = 5;
            this.grdFacturas_Sheet1.RowCount = 10;
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "FolioFactura";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Núm. Factura";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Fecha";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Tipo de factura";
            this.grdFacturas_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Importe";
            this.grdFacturas_Sheet1.ColumnHeader.Rows.Get(0).Height = 31F;
            textCellType1.MaxLength = 10;
            this.grdFacturas_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdFacturas_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(0).Label = "FolioFactura";
            this.grdFacturas_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(0).Width = 90F;
            this.grdFacturas_Sheet1.Columns.Get(1).CellType = textCellType2;
            this.grdFacturas_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.grdFacturas_Sheet1.Columns.Get(1).Label = "Núm. Factura";
            this.grdFacturas_Sheet1.Columns.Get(1).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(1).VisualStyles = FarPoint.Win.VisualStyles.Auto;
            this.grdFacturas_Sheet1.Columns.Get(1).Width = 160F;
            dateTimeCellType1.Calendar = ((System.Globalization.Calendar)(resources.GetObject("dateTimeCellType1.Calendar")));
            dateTimeCellType1.CalendarSurroundingDaysColor = System.Drawing.SystemColors.GrayText;
            dateTimeCellType1.DateDefault = new System.DateTime(2012, 9, 12, 17, 37, 37, 0);
            dateTimeCellType1.TimeDefault = new System.DateTime(2012, 9, 12, 17, 37, 37, 0);
            dateTimeCellType1.UserDefinedFormat = "dd-mm-yyyy";
            this.grdFacturas_Sheet1.Columns.Get(2).CellType = dateTimeCellType1;
            this.grdFacturas_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(2).Label = "Fecha";
            this.grdFacturas_Sheet1.Columns.Get(2).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(2).Width = 100F;
            this.grdFacturas_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.grdFacturas_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(3).Label = "Tipo de factura";
            this.grdFacturas_Sheet1.Columns.Get(3).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(3).Width = 160F;
            numberCellType1.DecimalPlaces = 4;
            numberCellType1.DecimalSeparator = ".";
            numberCellType1.MinimumValue = 0D;
            numberCellType1.Separator = ",";
            numberCellType1.ShowSeparator = true;
            this.grdFacturas_Sheet1.Columns.Get(4).CellType = numberCellType1;
            this.grdFacturas_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdFacturas_Sheet1.Columns.Get(4).Label = "Importe";
            this.grdFacturas_Sheet1.Columns.Get(4).Locked = true;
            this.grdFacturas_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFacturas_Sheet1.Columns.Get(4).Width = 100F;
            this.grdFacturas_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.grdFacturas_Sheet1.Rows.Default.Height = 25F;
            this.grdFacturas_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // scTextBoxExt1
            // 
            this.scTextBoxExt1.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.scTextBoxExt1.Decimales = 2;
            this.scTextBoxExt1.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.scTextBoxExt1.ForeColor = System.Drawing.Color.Black;
            this.scTextBoxExt1.Location = new System.Drawing.Point(126, 44);
            this.scTextBoxExt1.MaxLength = 10;
            this.scTextBoxExt1.Name = "scTextBoxExt1";
            this.scTextBoxExt1.PermitirApostrofo = false;
            this.scTextBoxExt1.PermitirNegativos = false;
            this.scTextBoxExt1.Size = new System.Drawing.Size(215, 20);
            this.scTextBoxExt1.TabIndex = 42;
            this.scTextBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCotizacion
            // 
            this.txtCotizacion.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCotizacion.Decimales = 2;
            this.txtCotizacion.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCotizacion.ForeColor = System.Drawing.Color.Black;
            this.txtCotizacion.Location = new System.Drawing.Point(126, 18);
            this.txtCotizacion.MaxLength = 10;
            this.txtCotizacion.Name = "txtCotizacion";
            this.txtCotizacion.PermitirApostrofo = false;
            this.txtCotizacion.PermitirNegativos = false;
            this.txtCotizacion.Size = new System.Drawing.Size(100, 20);
            this.txtCotizacion.TabIndex = 0;
            this.txtCotizacion.Text = "0123456789";
            this.txtCotizacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmContrecibos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 441);
            this.Controls.Add(this.FrameFacturas);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolMenuFacturacion);
            this.Name = "FrmContrecibos";
            this.Text = "Registro de Contrarecibos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmContrecibos_Load);
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.FrameFacturas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFacturas_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.Label label5;
        private SC_ControlsCS.scTextBoxExt txtCotizacion;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private SC_ControlsCS.scTextBoxExt scTextBoxExt1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaDoc;
        private System.Windows.Forms.GroupBox FrameFacturas;
        private FarPoint.Win.Spread.FpSpread grdFacturas;
        private FarPoint.Win.Spread.SheetView grdFacturas_Sheet1;
        private SC_ControlsCS.scTextBoxExt txtContraRecibo;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}