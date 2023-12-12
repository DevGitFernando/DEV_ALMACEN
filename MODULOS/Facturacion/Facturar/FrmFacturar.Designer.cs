namespace Facturacion.Facturar
{
    partial class FrmFacturar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturar));
            this.toolMenuFacturacion = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.lblFacturada = new System.Windows.Forms.Label();
            this.txtFolioRemision = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaRegistroRemision = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTipoFactura = new SC_ControlsCS.scLabelExt();
            this.lblFacturaElectronica = new SC_ControlsCS.scLabelExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolioFactura = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.txtObservaciones = new SC_ControlsCS.scTextBoxExt();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSubTotalGrabado = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSubTotalSinGrabar = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRemisionRelacionadaFactura = new System.Windows.Forms.Label();
            this.toolMenuFacturacion.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.toolMenuFacturacion.Size = new System.Drawing.Size(815, 25);
            this.toolMenuFacturacion.TabIndex = 0;
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
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(23, 22);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Controls.Add(this.lblFacturada);
            this.FrameDatos.Controls.Add(this.txtFolioRemision);
            this.FrameDatos.Controls.Add(this.label1);
            this.FrameDatos.Controls.Add(this.label2);
            this.FrameDatos.Controls.Add(this.dtpFechaRegistroRemision);
            this.FrameDatos.Location = new System.Drawing.Point(16, 188);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Size = new System.Drawing.Size(789, 59);
            this.FrameDatos.TabIndex = 2;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Datos de Remisión";
            // 
            // lblFacturada
            // 
            this.lblFacturada.BackColor = System.Drawing.Color.Transparent;
            this.lblFacturada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFacturada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturada.Location = new System.Drawing.Point(309, 22);
            this.lblFacturada.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacturada.Name = "lblFacturada";
            this.lblFacturada.Size = new System.Drawing.Size(173, 25);
            this.lblFacturada.TabIndex = 36;
            this.lblFacturada.Text = "NO FACTURABLE";
            this.lblFacturada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFacturada.Visible = false;
            // 
            // txtFolioRemision
            // 
            this.txtFolioRemision.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioRemision.Decimales = 2;
            this.txtFolioRemision.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioRemision.ForeColor = System.Drawing.Color.Black;
            this.txtFolioRemision.Location = new System.Drawing.Point(168, 22);
            this.txtFolioRemision.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioRemision.MaxLength = 10;
            this.txtFolioRemision.Name = "txtFolioRemision";
            this.txtFolioRemision.PermitirApostrofo = false;
            this.txtFolioRemision.PermitirNegativos = false;
            this.txtFolioRemision.Size = new System.Drawing.Size(132, 22);
            this.txtFolioRemision.TabIndex = 0;
            this.txtFolioRemision.Text = "0123456789";
            this.txtFolioRemision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioRemision.TextChanged += new System.EventHandler(this.txtFolioRemision_TextChanged);
            this.txtFolioRemision.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolioRemision_KeyDown);
            this.txtFolioRemision.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioRemision_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "Folio remisión :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(529, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 31;
            this.label2.Text = "Fecha Registro :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistroRemision
            // 
            this.dtpFechaRegistroRemision.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistroRemision.Enabled = false;
            this.dtpFechaRegistroRemision.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistroRemision.Location = new System.Drawing.Point(649, 22);
            this.dtpFechaRegistroRemision.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistroRemision.Name = "dtpFechaRegistroRemision";
            this.dtpFechaRegistroRemision.Size = new System.Drawing.Size(123, 22);
            this.dtpFechaRegistroRemision.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblTipoFactura);
            this.groupBox1.Controls.Add(this.lblFacturaElectronica);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtFolioFactura);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dtpFechaRegistro);
            this.groupBox1.Location = new System.Drawing.Point(16, 34);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(789, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Factura";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(481, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "Tipo :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTipoFactura
            // 
            this.lblTipoFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoFactura.Location = new System.Drawing.Point(547, 57);
            this.lblTipoFactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipoFactura.MostrarToolTip = false;
            this.lblTipoFactura.Name = "lblTipoFactura";
            this.lblTipoFactura.Size = new System.Drawing.Size(227, 25);
            this.lblTipoFactura.TabIndex = 44;
            this.lblTipoFactura.Text = "scLabelExt3";
            this.lblTipoFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFacturaElectronica
            // 
            this.lblFacturaElectronica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFacturaElectronica.Location = new System.Drawing.Point(168, 55);
            this.lblFacturaElectronica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFacturaElectronica.MostrarToolTip = false;
            this.lblFacturaElectronica.Name = "lblFacturaElectronica";
            this.lblFacturaElectronica.Size = new System.Drawing.Size(287, 25);
            this.lblFacturaElectronica.TabIndex = 2;
            this.lblFacturaElectronica.Text = "scLabelExt3";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(17, 57);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 20);
            this.label7.TabIndex = 43;
            this.label7.Text = "Factura Electrónica :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(45, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 41;
            this.label8.Text = "Observaciones :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFolioFactura
            // 
            this.txtFolioFactura.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolioFactura.Decimales = 2;
            this.txtFolioFactura.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolioFactura.ForeColor = System.Drawing.Color.Black;
            this.txtFolioFactura.Location = new System.Drawing.Point(168, 22);
            this.txtFolioFactura.Margin = new System.Windows.Forms.Padding(4);
            this.txtFolioFactura.MaxLength = 10;
            this.txtFolioFactura.Name = "txtFolioFactura";
            this.txtFolioFactura.PermitirApostrofo = false;
            this.txtFolioFactura.PermitirNegativos = false;
            this.txtFolioFactura.Size = new System.Drawing.Size(132, 22);
            this.txtFolioFactura.TabIndex = 0;
            this.txtFolioFactura.Text = "0123456789";
            this.txtFolioFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFolioFactura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFolioFactura_KeyDown);
            this.txtFolioFactura.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolioFactura_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(17, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "Folio Factura :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtObservaciones.Decimales = 2;
            this.txtObservaciones.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtObservaciones.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Location = new System.Drawing.Point(168, 86);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.MaxLength = 100;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.PermitirApostrofo = false;
            this.txtObservaciones.PermitirNegativos = false;
            this.txtObservaciones.Size = new System.Drawing.Size(604, 51);
            this.txtObservaciones.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(529, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 20);
            this.label10.TabIndex = 31;
            this.label10.Text = "Fecha Registro :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRegistro.Enabled = false;
            this.dtpFechaRegistro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRegistro.Location = new System.Drawing.Point(649, 25);
            this.dtpFechaRegistro.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(123, 22);
            this.dtpFechaRegistro.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(547, 113);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(227, 25);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Núm. Cotización :";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(385, 116);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 20);
            this.label15.TabIndex = 52;
            this.label15.Text = "Total : ";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.Location = new System.Drawing.Point(547, 82);
            this.lblIva.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(227, 25);
            this.lblIva.TabIndex = 2;
            this.lblIva.Text = "Núm. Cotización :";
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(385, 85);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 20);
            this.label13.TabIndex = 51;
            this.label13.Text = "Iva : ";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalGrabado
            // 
            this.lblSubTotalGrabado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalGrabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalGrabado.Location = new System.Drawing.Point(547, 50);
            this.lblSubTotalGrabado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTotalGrabado.Name = "lblSubTotalGrabado";
            this.lblSubTotalGrabado.Size = new System.Drawing.Size(227, 25);
            this.lblSubTotalGrabado.TabIndex = 1;
            this.lblSubTotalGrabado.Text = "Núm. Cotización :";
            this.lblSubTotalGrabado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(385, 53);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 20);
            this.label11.TabIndex = 50;
            this.label11.Text = "Sub-Total Grabado : ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalSinGrabar
            // 
            this.lblSubTotalSinGrabar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalSinGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotalSinGrabar.Location = new System.Drawing.Point(547, 20);
            this.lblSubTotalSinGrabar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTotalSinGrabar.Name = "lblSubTotalSinGrabar";
            this.lblSubTotalSinGrabar.Size = new System.Drawing.Size(227, 25);
            this.lblSubTotalSinGrabar.TabIndex = 0;
            this.lblSubTotalSinGrabar.Text = "Núm. Cotización :";
            this.lblSubTotalSinGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(385, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Sub-Total sin Grabar : ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRemisionRelacionadaFactura);
            this.groupBox2.Controls.Add(this.lblSubTotalSinGrabar);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblIva);
            this.groupBox2.Controls.Add(this.lblSubTotalGrabado);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(16, 249);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(789, 150);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Importes de Factura";
            // 
            // lblRemisionRelacionadaFactura
            // 
            this.lblRemisionRelacionadaFactura.BackColor = System.Drawing.Color.Transparent;
            this.lblRemisionRelacionadaFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRemisionRelacionadaFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemisionRelacionadaFactura.Location = new System.Drawing.Point(20, 116);
            this.lblRemisionRelacionadaFactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRemisionRelacionadaFactura.Name = "lblRemisionRelacionadaFactura";
            this.lblRemisionRelacionadaFactura.Size = new System.Drawing.Size(280, 25);
            this.lblRemisionRelacionadaFactura.TabIndex = 53;
            this.lblRemisionRelacionadaFactura.Text = "NO FACTURABLE";
            this.lblRemisionRelacionadaFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRemisionRelacionadaFactura.Visible = false;
            // 
            // FrmFacturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolMenuFacturacion);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmFacturar";
            this.Text = "Registro de Facturas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmFacturar_Load);
            this.toolMenuFacturacion.ResumeLayout(false);
            this.toolMenuFacturacion.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameDatos.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenuFacturacion;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FrameDatos;
        private SC_ControlsCS.scTextBoxExt txtFolioRemision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistroRemision;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scTextBoxExt txtFolioFactura;
        private System.Windows.Forms.Label label9;
        private SC_ControlsCS.scTextBoxExt txtObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFechaRegistro;
        private SC_ControlsCS.scLabelExt lblFacturaElectronica;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSubTotalGrabado;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSubTotalSinGrabar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private SC_ControlsCS.scLabelExt lblTipoFactura;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFacturada;
        private System.Windows.Forms.Label lblRemisionRelacionadaFactura;
    }
}