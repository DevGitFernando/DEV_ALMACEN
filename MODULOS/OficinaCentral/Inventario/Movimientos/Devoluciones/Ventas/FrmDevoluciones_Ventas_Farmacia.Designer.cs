﻿namespace OficinaCentral.Inventario
{
    partial class FrmDevoluciones_Ventas_Farmacia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDevoluciones_Ventas_Farmacia));
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.CurrencyCellType currencyCellType1 = new FarPoint.Win.Spread.CellType.CurrencyCellType();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rdoVentaAmbos = new System.Windows.Forms.RadioButton();
            this.rdoCredito = new System.Windows.Forms.RadioButton();
            this.rdoPublicoGral = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdoConsultaAmbas = new System.Windows.Forms.RadioButton();
            this.rdoConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoVenta = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalFarmacias = new System.Windows.Forms.Label();
            this.grdFarmacias = new FarPoint.Win.Spread.FpSpread();
            this.grdFarmacias_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tmExistencias = new System.Windows.Forms.Timer(this.components);
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(688, 25);
            this.toolStripBarraMenu.TabIndex = 7;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(9, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 296);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Consulta";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboFarmacias);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboEstados);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 90);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos de Estado y Farmacia";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(75, 51);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(257, 21);
            this.cboFarmacias.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Farmacia :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(75, 24);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(257, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Estado :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rdoVentaAmbos);
            this.groupBox6.Controls.Add(this.rdoCredito);
            this.groupBox6.Controls.Add(this.rdoPublicoGral);
            this.groupBox6.Location = new System.Drawing.Point(10, 224);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(352, 43);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Tipo de Venta";
            // 
            // rdoVentaAmbos
            // 
            this.rdoVentaAmbos.AutoSize = true;
            this.rdoVentaAmbos.Location = new System.Drawing.Point(263, 17);
            this.rdoVentaAmbos.Name = "rdoVentaAmbos";
            this.rdoVentaAmbos.Size = new System.Drawing.Size(57, 17);
            this.rdoVentaAmbos.TabIndex = 2;
            this.rdoVentaAmbos.TabStop = true;
            this.rdoVentaAmbos.Text = "Ambas";
            this.rdoVentaAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoCredito
            // 
            this.rdoCredito.AutoSize = true;
            this.rdoCredito.Location = new System.Drawing.Point(44, 17);
            this.rdoCredito.Name = "rdoCredito";
            this.rdoCredito.Size = new System.Drawing.Size(58, 17);
            this.rdoCredito.TabIndex = 0;
            this.rdoCredito.TabStop = true;
            this.rdoCredito.Text = "Credito";
            this.rdoCredito.UseVisualStyleBackColor = true;
            // 
            // rdoPublicoGral
            // 
            this.rdoPublicoGral.AutoSize = true;
            this.rdoPublicoGral.Location = new System.Drawing.Point(135, 17);
            this.rdoPublicoGral.Name = "rdoPublicoGral";
            this.rdoPublicoGral.Size = new System.Drawing.Size(100, 17);
            this.rdoPublicoGral.TabIndex = 1;
            this.rdoPublicoGral.TabStop = true;
            this.rdoPublicoGral.Text = "Publico General";
            this.rdoPublicoGral.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdoConsultaAmbas);
            this.groupBox5.Controls.Add(this.rdoConsignacion);
            this.groupBox5.Controls.Add(this.rdoVenta);
            this.groupBox5.Location = new System.Drawing.Point(10, 126);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(352, 43);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tipo de Farmacia";
            // 
            // rdoConsultaAmbas
            // 
            this.rdoConsultaAmbas.AutoSize = true;
            this.rdoConsultaAmbas.Location = new System.Drawing.Point(263, 17);
            this.rdoConsultaAmbas.Name = "rdoConsultaAmbas";
            this.rdoConsultaAmbas.Size = new System.Drawing.Size(57, 17);
            this.rdoConsultaAmbas.TabIndex = 2;
            this.rdoConsultaAmbas.TabStop = true;
            this.rdoConsultaAmbas.Text = "Ambas";
            this.rdoConsultaAmbas.UseVisualStyleBackColor = true;
            // 
            // rdoConsignacion
            // 
            this.rdoConsignacion.AutoSize = true;
            this.rdoConsignacion.Location = new System.Drawing.Point(135, 17);
            this.rdoConsignacion.Name = "rdoConsignacion";
            this.rdoConsignacion.Size = new System.Drawing.Size(89, 17);
            this.rdoConsignacion.TabIndex = 1;
            this.rdoConsignacion.TabStop = true;
            this.rdoConsignacion.Text = "Consignación";
            this.rdoConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoVenta
            // 
            this.rdoVenta.AutoSize = true;
            this.rdoVenta.Location = new System.Drawing.Point(44, 17);
            this.rdoVenta.Name = "rdoVenta";
            this.rdoVenta.Size = new System.Drawing.Size(53, 17);
            this.rdoVenta.TabIndex = 0;
            this.rdoVenta.TabStop = true;
            this.rdoVenta.Text = "Venta";
            this.rdoVenta.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpFechaFinal);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.dtpFechaInicial);
            this.groupBox4.Location = new System.Drawing.Point(10, 175);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(352, 43);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(238, 13);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(207, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(44, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(91, 13);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblTotalFarmacias);
            this.groupBox3.Controls.Add(this.grdFarmacias);
            this.groupBox3.Location = new System.Drawing.Point(389, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 299);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Devoluciones de la Farmacia";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "Total Farmacia $ :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalFarmacias
            // 
            this.lblTotalFarmacias.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalFarmacias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFarmacias.Location = new System.Drawing.Point(132, 261);
            this.lblTotalFarmacias.Name = "lblTotalFarmacias";
            this.lblTotalFarmacias.Size = new System.Drawing.Size(135, 27);
            this.lblTotalFarmacias.TabIndex = 1;
            this.lblTotalFarmacias.Text = "label3";
            this.lblTotalFarmacias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdFarmacias
            // 
            this.grdFarmacias.AccessibleDescription = "grdFarmacias, Sheet1, Row 0, Column 0, ";
            this.grdFarmacias.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdFarmacias.Location = new System.Drawing.Point(10, 16);
            this.grdFarmacias.Name = "grdFarmacias";
            this.grdFarmacias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grdFarmacias.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grdFarmacias_Sheet1});
            this.grdFarmacias.Size = new System.Drawing.Size(270, 242);
            this.grdFarmacias.TabIndex = 0;
            // 
            // grdFarmacias_Sheet1
            // 
            this.grdFarmacias_Sheet1.Reset();
            this.grdFarmacias_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grdFarmacias_Sheet1.ColumnCount = 2;
            this.grdFarmacias_Sheet1.RowCount = 10;
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Fecha";
            this.grdFarmacias_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Venta";
            this.grdFarmacias_Sheet1.Columns.Get(0).CellType = textCellType1;
            this.grdFarmacias_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Label = "Fecha";
            this.grdFarmacias_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(0).Width = 99F;
            currencyCellType1.DecimalPlaces = 4;
            currencyCellType1.ShowSeparator = true;
            this.grdFarmacias_Sheet1.Columns.Get(1).CellType = currencyCellType1;
            this.grdFarmacias_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.grdFarmacias_Sheet1.Columns.Get(1).Label = "Venta";
            this.grdFarmacias_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grdFarmacias_Sheet1.Columns.Get(1).Width = 114F;
            this.grdFarmacias_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.grdFarmacias_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // FrmDevoluciones_Ventas_Farmacia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 336);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDevoluciones_Ventas_Farmacia";
            this.Text = "Consulta de Devoluciones de Ventas por Farmacia";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmDevoluciones_Ventas_Farmacia_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFarmacias_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalFarmacias;
        private FarPoint.Win.Spread.FpSpread grdFarmacias;
        private FarPoint.Win.Spread.SheetView grdFarmacias_Sheet1;
        private System.Windows.Forms.Timer tmExistencias;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdoConsignacion;
        private System.Windows.Forms.RadioButton rdoVenta;
        private System.Windows.Forms.RadioButton rdoConsultaAmbas;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rdoVentaAmbos;
        private System.Windows.Forms.RadioButton rdoCredito;
        private System.Windows.Forms.RadioButton rdoPublicoGral;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label3;
    }
}