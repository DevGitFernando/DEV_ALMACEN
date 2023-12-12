﻿namespace DllPedidosClientes.Reportes
{
    partial class FrmDispensacionClaves
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDispensacionClaves));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.tmEjecuciones = new System.Windows.Forms.Timer(this.components);
            this.FrameParametros = new System.Windows.Forms.GroupBox();
            this.cboLocalidad = new SC_ControlsCS.scComboBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.cboFarmacia = new SC_ControlsCS.scComboBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.chkTipoTodasJuris = new System.Windows.Forms.CheckBox();
            this.cboJurisdicciones = new SC_ControlsCS.scComboBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTipoUnidad = new SC_ControlsCS.scComboBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.cboFarmacias = new SC_ControlsCS.scComboBoxExt();
            this.FrameDispensacion = new System.Windows.Forms.GroupBox();
            this.rdoTpDispAmbos = new System.Windows.Forms.RadioButton();
            this.rdoTpDispConsignacion = new System.Windows.Forms.RadioButton();
            this.rdoTpDispVenta = new System.Windows.Forms.RadioButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstClaves = new SC_ControlsCS.scListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoAnioClaves = new System.Windows.Forms.RadioButton();
            this.rdoClaves = new System.Windows.Forms.RadioButton();
            this.FrameInsumos = new System.Windows.Forms.GroupBox();
            this.rdoInsumoMatCuracion = new System.Windows.Forms.RadioButton();
            this.rdoInsumosAmbos = new System.Windows.Forms.RadioButton();
            this.rdoInsumosMedicamento = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoRecVales = new System.Windows.Forms.RadioButton();
            this.rdoRecVentas = new System.Windows.Forms.RadioButton();
            this.FrameImpresion = new System.Windows.Forms.GroupBox();
            this.chkTipoDeReporte = new System.Windows.Forms.CheckBox();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameParametros.SuspendLayout();
            this.FrameDispensacion.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.FrameInsumos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FrameImpresion.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnExportarExcel});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(870, 25);
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
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Enabled = false;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // tmEjecuciones
            // 
            this.tmEjecuciones.Interval = 500;
            this.tmEjecuciones.Tick += new System.EventHandler(this.tmEjecuciones_Tick);
            // 
            // FrameParametros
            // 
            this.FrameParametros.Controls.Add(this.cboLocalidad);
            this.FrameParametros.Controls.Add(this.label4);
            this.FrameParametros.Controls.Add(this.cboFarmacia);
            this.FrameParametros.Controls.Add(this.label3);
            this.FrameParametros.Controls.Add(this.cboJurisdicciones);
            this.FrameParametros.Controls.Add(this.label1);
            this.FrameParametros.Controls.Add(this.cboTipoUnidad);
            this.FrameParametros.Controls.Add(this.label8);
            this.FrameParametros.Location = new System.Drawing.Point(10, 28);
            this.FrameParametros.Name = "FrameParametros";
            this.FrameParametros.Size = new System.Drawing.Size(389, 130);
            this.FrameParametros.TabIndex = 0;
            this.FrameParametros.TabStop = false;
            this.FrameParametros.Text = "Información de Unidad";
            // 
            // cboLocalidad
            // 
            this.cboLocalidad.BackColorEnabled = System.Drawing.Color.White;
            this.cboLocalidad.Data = "";
            this.cboLocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocalidad.Filtro = " 1 = 1";
            this.cboLocalidad.FormattingEnabled = true;
            this.cboLocalidad.ListaItemsBusqueda = 20;
            this.cboLocalidad.Location = new System.Drawing.Point(104, 73);
            this.cboLocalidad.MostrarToolTip = false;
            this.cboLocalidad.Name = "cboLocalidad";
            this.cboLocalidad.Size = new System.Drawing.Size(275, 21);
            this.cboLocalidad.TabIndex = 2;
            this.cboLocalidad.SelectedIndexChanged += new System.EventHandler(this.cboLocalidad_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Localidad :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacia
            // 
            this.cboFarmacia.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacia.Data = "";
            this.cboFarmacia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacia.Filtro = " 1 = 1";
            this.cboFarmacia.FormattingEnabled = true;
            this.cboFarmacia.ListaItemsBusqueda = 20;
            this.cboFarmacia.Location = new System.Drawing.Point(104, 100);
            this.cboFarmacia.MostrarToolTip = false;
            this.cboFarmacia.Name = "cboFarmacia";
            this.cboFarmacia.Size = new System.Drawing.Size(275, 21);
            this.cboFarmacia.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(18, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Farmacia :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTipoTodasJuris
            // 
            this.chkTipoTodasJuris.Location = new System.Drawing.Point(255, 14);
            this.chkTipoTodasJuris.Name = "chkTipoTodasJuris";
            this.chkTipoTodasJuris.Size = new System.Drawing.Size(187, 18);
            this.chkTipoTodasJuris.TabIndex = 4;
            this.chkTipoTodasJuris.Text = "Reporte Concentrado en Excel";
            this.chkTipoTodasJuris.UseVisualStyleBackColor = true;
            this.chkTipoTodasJuris.Visible = false;
            // 
            // cboJurisdicciones
            // 
            this.cboJurisdicciones.BackColorEnabled = System.Drawing.Color.White;
            this.cboJurisdicciones.Data = "";
            this.cboJurisdicciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJurisdicciones.Filtro = " 1 = 1";
            this.cboJurisdicciones.FormattingEnabled = true;
            this.cboJurisdicciones.ListaItemsBusqueda = 20;
            this.cboJurisdicciones.Location = new System.Drawing.Point(104, 44);
            this.cboJurisdicciones.MostrarToolTip = false;
            this.cboJurisdicciones.Name = "cboJurisdicciones";
            this.cboJurisdicciones.Size = new System.Drawing.Size(275, 21);
            this.cboJurisdicciones.TabIndex = 1;
            this.cboJurisdicciones.SelectedIndexChanged += new System.EventHandler(this.cboJurisdicciones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Jurisdicción :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoUnidad
            // 
            this.cboTipoUnidad.BackColorEnabled = System.Drawing.Color.White;
            this.cboTipoUnidad.Data = "";
            this.cboTipoUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoUnidad.Filtro = " 1 = 1";
            this.cboTipoUnidad.FormattingEnabled = true;
            this.cboTipoUnidad.ListaItemsBusqueda = 20;
            this.cboTipoUnidad.Location = new System.Drawing.Point(104, 17);
            this.cboTipoUnidad.MostrarToolTip = false;
            this.cboTipoUnidad.Name = "cboTipoUnidad";
            this.cboTipoUnidad.Size = new System.Drawing.Size(275, 21);
            this.cboTipoUnidad.TabIndex = 0;
            this.cboTipoUnidad.SelectedIndexChanged += new System.EventHandler(this.cboTipoUnidad_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(19, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Tipo Unidad :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFarmacias
            // 
            this.cboFarmacias.BackColorEnabled = System.Drawing.Color.White;
            this.cboFarmacias.Data = "";
            this.cboFarmacias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFarmacias.Enabled = false;
            this.cboFarmacias.Filtro = " 1 = 1";
            this.cboFarmacias.FormattingEnabled = true;
            this.cboFarmacias.ListaItemsBusqueda = 20;
            this.cboFarmacias.Location = new System.Drawing.Point(418, 639);
            this.cboFarmacias.MostrarToolTip = false;
            this.cboFarmacias.Name = "cboFarmacias";
            this.cboFarmacias.Size = new System.Drawing.Size(274, 21);
            this.cboFarmacias.TabIndex = 1;
            this.cboFarmacias.Visible = false;
            // 
            // FrameDispensacion
            // 
            this.FrameDispensacion.Controls.Add(this.rdoTpDispAmbos);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispConsignacion);
            this.FrameDispensacion.Controls.Add(this.rdoTpDispVenta);
            this.FrameDispensacion.Location = new System.Drawing.Point(405, 28);
            this.FrameDispensacion.Name = "FrameDispensacion";
            this.FrameDispensacion.Size = new System.Drawing.Size(313, 43);
            this.FrameDispensacion.TabIndex = 1;
            this.FrameDispensacion.TabStop = false;
            this.FrameDispensacion.Text = "Tipo de Dispensación";
            // 
            // rdoTpDispAmbos
            // 
            this.rdoTpDispAmbos.Checked = true;
            this.rdoTpDispAmbos.Location = new System.Drawing.Point(33, 17);
            this.rdoTpDispAmbos.Name = "rdoTpDispAmbos";
            this.rdoTpDispAmbos.Size = new System.Drawing.Size(57, 17);
            this.rdoTpDispAmbos.TabIndex = 0;
            this.rdoTpDispAmbos.TabStop = true;
            this.rdoTpDispAmbos.Text = "Ambos";
            this.rdoTpDispAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispConsignacion
            // 
            this.rdoTpDispConsignacion.Location = new System.Drawing.Point(191, 17);
            this.rdoTpDispConsignacion.Name = "rdoTpDispConsignacion";
            this.rdoTpDispConsignacion.Size = new System.Drawing.Size(89, 17);
            this.rdoTpDispConsignacion.TabIndex = 2;
            this.rdoTpDispConsignacion.Text = "Consignación";
            this.rdoTpDispConsignacion.UseVisualStyleBackColor = true;
            // 
            // rdoTpDispVenta
            // 
            this.rdoTpDispVenta.Location = new System.Drawing.Point(114, 18);
            this.rdoTpDispVenta.Name = "rdoTpDispVenta";
            this.rdoTpDispVenta.Size = new System.Drawing.Size(58, 15);
            this.rdoTpDispVenta.TabIndex = 1;
            this.rdoTpDispVenta.Text = "Venta";
            this.rdoTpDispVenta.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label2);
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(722, 28);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(139, 86);
            this.FrameFechas.TabIndex = 3;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(58, 52);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(67, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Inicio :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(58, 26);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(67, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstClaves);
            this.groupBox1.Location = new System.Drawing.Point(10, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 369);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listado de Claves";
            // 
            // lstClaves
            // 
            this.lstClaves.Location = new System.Drawing.Point(13, 19);
            this.lstClaves.LockColumnSize = false;
            this.lstClaves.Name = "lstClaves";
            this.lstClaves.Size = new System.Drawing.Size(826, 339);
            this.lstClaves.TabIndex = 5;
            this.lstClaves.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoAnioClaves);
            this.groupBox3.Controls.Add(this.rdoClaves);
            this.groupBox3.Location = new System.Drawing.Point(1073, 256);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(191, 43);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Reporte";
            // 
            // rdoAnioClaves
            // 
            this.rdoAnioClaves.Location = new System.Drawing.Point(83, 15);
            this.rdoAnioClaves.Name = "rdoAnioClaves";
            this.rdoAnioClaves.Size = new System.Drawing.Size(89, 17);
            this.rdoAnioClaves.TabIndex = 1;
            this.rdoAnioClaves.Text = "Año / Claves";
            this.rdoAnioClaves.UseVisualStyleBackColor = true;
            // 
            // rdoClaves
            // 
            this.rdoClaves.Checked = true;
            this.rdoClaves.Location = new System.Drawing.Point(19, 16);
            this.rdoClaves.Name = "rdoClaves";
            this.rdoClaves.Size = new System.Drawing.Size(58, 15);
            this.rdoClaves.TabIndex = 0;
            this.rdoClaves.TabStop = true;
            this.rdoClaves.Text = "Claves";
            this.rdoClaves.UseVisualStyleBackColor = true;
            // 
            // FrameInsumos
            // 
            this.FrameInsumos.Controls.Add(this.rdoInsumoMatCuracion);
            this.FrameInsumos.Controls.Add(this.rdoInsumosAmbos);
            this.FrameInsumos.Controls.Add(this.rdoInsumosMedicamento);
            this.FrameInsumos.Location = new System.Drawing.Point(405, 71);
            this.FrameInsumos.Name = "FrameInsumos";
            this.FrameInsumos.Size = new System.Drawing.Size(313, 43);
            this.FrameInsumos.TabIndex = 2;
            this.FrameInsumos.TabStop = false;
            this.FrameInsumos.Text = "Tipo de Insumo";
            // 
            // rdoInsumoMatCuracion
            // 
            this.rdoInsumoMatCuracion.Location = new System.Drawing.Point(176, 19);
            this.rdoInsumoMatCuracion.Name = "rdoInsumoMatCuracion";
            this.rdoInsumoMatCuracion.Size = new System.Drawing.Size(122, 15);
            this.rdoInsumoMatCuracion.TabIndex = 2;
            this.rdoInsumoMatCuracion.Text = "Material de curación";
            this.rdoInsumoMatCuracion.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosAmbos
            // 
            this.rdoInsumosAmbos.Checked = true;
            this.rdoInsumosAmbos.Location = new System.Drawing.Point(14, 19);
            this.rdoInsumosAmbos.Name = "rdoInsumosAmbos";
            this.rdoInsumosAmbos.Size = new System.Drawing.Size(58, 15);
            this.rdoInsumosAmbos.TabIndex = 0;
            this.rdoInsumosAmbos.TabStop = true;
            this.rdoInsumosAmbos.Text = "Ambos";
            this.rdoInsumosAmbos.UseVisualStyleBackColor = true;
            // 
            // rdoInsumosMedicamento
            // 
            this.rdoInsumosMedicamento.Location = new System.Drawing.Point(78, 19);
            this.rdoInsumosMedicamento.Name = "rdoInsumosMedicamento";
            this.rdoInsumosMedicamento.Size = new System.Drawing.Size(92, 15);
            this.rdoInsumosMedicamento.TabIndex = 1;
            this.rdoInsumosMedicamento.Text = "Medicamento";
            this.rdoInsumosMedicamento.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoRecVales);
            this.groupBox2.Controls.Add(this.rdoRecVentas);
            this.groupBox2.Location = new System.Drawing.Point(1073, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 43);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reporte tipo atención Receta";
            // 
            // rdoRecVales
            // 
            this.rdoRecVales.Location = new System.Drawing.Point(145, 16);
            this.rdoRecVales.Name = "rdoRecVales";
            this.rdoRecVales.Size = new System.Drawing.Size(52, 17);
            this.rdoRecVales.TabIndex = 1;
            this.rdoRecVales.Text = "Vales";
            this.rdoRecVales.UseVisualStyleBackColor = true;
            // 
            // rdoRecVentas
            // 
            this.rdoRecVentas.Checked = true;
            this.rdoRecVentas.Location = new System.Drawing.Point(78, 16);
            this.rdoRecVentas.Name = "rdoRecVentas";
            this.rdoRecVentas.Size = new System.Drawing.Size(58, 15);
            this.rdoRecVentas.TabIndex = 0;
            this.rdoRecVentas.TabStop = true;
            this.rdoRecVentas.Text = "Ventas";
            this.rdoRecVentas.UseVisualStyleBackColor = true;
            // 
            // FrameImpresion
            // 
            this.FrameImpresion.Controls.Add(this.chkTipoDeReporte);
            this.FrameImpresion.Controls.Add(this.chkTipoTodasJuris);
            this.FrameImpresion.Location = new System.Drawing.Point(405, 115);
            this.FrameImpresion.Name = "FrameImpresion";
            this.FrameImpresion.Size = new System.Drawing.Size(456, 43);
            this.FrameImpresion.TabIndex = 7;
            this.FrameImpresion.TabStop = false;
            this.FrameImpresion.Text = "Tipo de reporte";
            // 
            // chkTipoDeReporte
            // 
            this.chkTipoDeReporte.Location = new System.Drawing.Point(41, 16);
            this.chkTipoDeReporte.Name = "chkTipoDeReporte";
            this.chkTipoDeReporte.Size = new System.Drawing.Size(187, 18);
            this.chkTipoDeReporte.TabIndex = 5;
            this.chkTipoDeReporte.Text = "Reporte Concentrado";
            this.chkTipoDeReporte.UseVisualStyleBackColor = true;
            this.chkTipoDeReporte.Visible = false;
            // 
            // FrmDispensacionClaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 527);
            this.Controls.Add(this.FrameImpresion);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cboFarmacias);
            this.Controls.Add(this.FrameInsumos);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDispensacion);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameParametros);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmDispensacionClaves";
            this.Text = "Consumos por Claves";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmReportesFacturacion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDispensacionClaves_KeyDown);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameParametros.ResumeLayout(false);
            this.FrameDispensacion.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.FrameInsumos.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.FrameImpresion.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmEjecuciones;
        private System.Windows.Forms.GroupBox FrameParametros;
        private SC_ControlsCS.scComboBoxExt cboTipoUnidad;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox FrameDispensacion;
        private System.Windows.Forms.RadioButton rdoTpDispConsignacion;
        private System.Windows.Forms.RadioButton rdoTpDispVenta;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private SC_ControlsCS.scComboBoxExt cboFarmacias;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoTpDispAmbos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoAnioClaves;
        private System.Windows.Forms.RadioButton rdoClaves;
        private System.Windows.Forms.GroupBox FrameInsumos;
        private System.Windows.Forms.RadioButton rdoInsumoMatCuracion;
        private System.Windows.Forms.RadioButton rdoInsumosAmbos;
        private System.Windows.Forms.RadioButton rdoInsumosMedicamento;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoRecVales;
        private System.Windows.Forms.RadioButton rdoRecVentas;
        private SC_ControlsCS.scComboBoxExt cboJurisdicciones;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scListView lstClaves;
        private System.Windows.Forms.CheckBox chkTipoTodasJuris;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private SC_ControlsCS.scComboBoxExt cboLocalidad;
        private System.Windows.Forms.Label label4;
        private SC_ControlsCS.scComboBoxExt cboFarmacia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox FrameImpresion;
        private System.Windows.Forms.CheckBox chkTipoDeReporte;
    }
}