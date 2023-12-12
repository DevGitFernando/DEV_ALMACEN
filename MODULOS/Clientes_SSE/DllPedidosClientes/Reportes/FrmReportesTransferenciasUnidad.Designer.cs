namespace DllPedidosClientes.Reportes
{
    partial class FrmReportesTransferenciasUnidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportesTransferenciasUnidad));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEstados = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.FrameTransferencias = new System.Windows.Forms.GroupBox();
            this.rdoTS = new System.Windows.Forms.RadioButton();
            this.rdoTE = new System.Windows.Forms.RadioButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDisAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoInsumoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumosAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumosMedicamento = new System.Windows.Forms.RadioButton();
            this.lblFarmacia = new SC_ControlsCS.scLabelExt();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.FrameTransferencias.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(523, 25);
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
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblFarmacia);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtFarmacia);
            this.groupBox5.Controls.Add(this.cboEstados);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Location = new System.Drawing.Point(10, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(503, 85);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Información de Unidad";
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.Location = new System.Drawing.Point(98, 385);
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(402, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Farmacia :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboEstados
            // 
            this.cboEstados.BackColorEnabled = System.Drawing.Color.White;
            this.cboEstados.Data = "";
            this.cboEstados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstados.Filtro = " 1 = 1";
            this.cboEstados.FormattingEnabled = true;
            this.cboEstados.Location = new System.Drawing.Point(88, 23);
            this.cboEstados.Name = "cboEstados";
            this.cboEstados.Size = new System.Drawing.Size(402, 21);
            this.cboEstados.TabIndex = 0;
            this.cboEstados.SelectedIndexChanged += new System.EventHandler(this.cboEstados_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(31, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameTransferencias
            // 
            this.FrameTransferencias.Controls.Add(this.rdoTS);
            this.FrameTransferencias.Controls.Add(this.rdoTE);
            this.FrameTransferencias.Location = new System.Drawing.Point(10, 119);
            this.FrameTransferencias.Name = "FrameTransferencias";
            this.FrameTransferencias.Size = new System.Drawing.Size(248, 66);
            this.FrameTransferencias.TabIndex = 2;
            this.FrameTransferencias.TabStop = false;
            this.FrameTransferencias.Text = "Tipo de Transferencia";
            // 
            // rdoTS
            // 
            this.rdoTS.Location = new System.Drawing.Point(147, 28);
            this.rdoTS.Name = "rdoTS";
            this.rdoTS.Size = new System.Drawing.Size(78, 15);
            this.rdoTS.TabIndex = 1;
            this.rdoTS.TabStop = true;
            this.rdoTS.Text = "Salida";
            this.rdoTS.UseVisualStyleBackColor = true;
            // 
            // rdoTE
            // 
            this.rdoTE.Location = new System.Drawing.Point(33, 28);
            this.rdoTE.Name = "rdoTE";
            this.rdoTE.Size = new System.Drawing.Size(79, 15);
            this.rdoTE.TabIndex = 0;
            this.rdoTE.TabStop = true;
            this.rdoTE.Text = "Entrada";
            this.rdoTE.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(265, 119);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(248, 66);
            this.FrameFechas.TabIndex = 3;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(95, 40);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(63, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(52, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(95, 17);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDisAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(10, 235);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(503, 54);
            this.FrameDispensacion.TabIndex = 5;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDisAmbos
            // 
            this.rdoTpDisAmbos.Checked = true;
            this.rdoTpDisAmbos.Location = new System.Drawing.Point(63, 23);
            this.rdoTpDisAmbos.Name = "rdoTpDisAmbos";
            this.rdoTpDisAmbos.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDisAmbos.TabIndex = 0;
            this.rdoTpDisAmbos.TabStop = true;
            this.rdoTpDisAmbos.Text = "Ambos";
            this.rdoTpDisAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(320, 23);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(94, 17);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(173, 23);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(94, 15);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoInsumoMatCuracion);
            this.groupBox3.Controls.Add(this.rdoInsumosAmbos);
            this.groupBox3.Controls.Add(this.rdoInsumosMedicamento);
            this.groupBox3.Location = new System.Drawing.Point(10, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(503, 47);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(320, 19);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(144, 15);
            this.rdoInsumoMatCuracion.TabIndex = 2;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Checked = true;
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(63, 19);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(64, 15);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(173, 19);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumosMedicamento.TabIndex = 1;
            this.rdoInsumosMedicamento.Text = "Medicamento";
            this.rdoInsumosMedicamento.UseVisualStyleBackColor = true;
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(156, 50);
            this.lblFarmacia.MostrarToolTip = true;
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(334, 20);
            this.lblFarmacia.TabIndex = 25;
            this.lblFarmacia.Text = "FARMACIA";
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(88, 50);
            this.txtFarmacia.MaxLength = 4;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(62, 20);
            this.txtFarmacia.TabIndex = 24;
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmReportesTransferenciasUnidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 298);
            this.Controls.Add(this.cboFarmacias);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameTransferencias);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmReportesTransferenciasUnidad";
            this.Text = "Reportes Administrativos de Transferencias";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReportesFacturacion_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.FrameTransferencias.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox groupBox5;
        private SC_ControlsCS.scComboBoxExt cboEstados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox FrameTransferencias;
        private System.Windows.Forms.RadioButton rdoTS;
        private System.Windows.Forms.RadioButton rdoTE;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDisAmbos;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoInsumoMatCuracion;
        private System.Windows.Forms.RadioButton rdoInsumosAmbos;
        private System.Windows.Forms.RadioButton rdoInsumosMedicamento;
        private SC_ControlsCS.scLabelExt lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
    }
}