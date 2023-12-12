namespace Facturacion.GenerarRemisiones
{
    partial class FrmValidarRemisiones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmValidarRemisiones));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidarPolizasBeneficiarios = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportarPDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelarRemision = new System.Windows.Forms.ToolStripButton();
            this.FramePrincipal = new System.Windows.Forms.GroupBox();
            this.lblIdConcepto = new System.Windows.Forms.Label();
            this.lblConcepto = new SC_ControlsCS.scLabelExt();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFechaRemision = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRubro = new System.Windows.Forms.Label();
            this.lblEstado = new SC_ControlsCS.scLabelExt();
            this.lblIdSubCliente = new System.Windows.Forms.Label();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSubCliente = new SC_ControlsCS.scLabelExt();
            this.lblCliente = new SC_ControlsCS.scLabelExt();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCancelado = new System.Windows.Forms.Label();
            this.txtFolio = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.FrameClaves = new System.Windows.Forms.GroupBox();
            this.lstDetalles = new SC_ControlsCS.scListView();
            this.FrameTotales = new System.Windows.Forms.GroupBox();
            this.lblTipoRemision = new System.Windows.Forms.Label();
            this.lblTotal = new SC_ControlsCS.scLabelExt();
            this.lblIva = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSubTotalGrabado = new SC_ControlsCS.scLabelExt();
            this.lblSubTotalSinGrabar = new SC_ControlsCS.scLabelExt();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FramePrincipal.SuspendLayout();
            this.FrameClaves.SuspendLayout();
            this.FrameTotales.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnGuardar,
            this.toolStripSeparator3,
            this.btnExportar,
            this.toolStripSeparator1,
            this.btnValidarPolizasBeneficiarios,
            this.toolStripSeparator4,
            this.btnImprimir,
            this.btnExportarPDF,
            this.toolStripSeparator2,
            this.btnCancelarRemision});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(864, 25);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportar
            // 
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(23, 22);
            this.btnExportar.Text = "&Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidarPolizasBeneficiarios
            // 
            this.btnValidarPolizasBeneficiarios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnValidarPolizasBeneficiarios.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarPolizasBeneficiarios.Image")));
            this.btnValidarPolizasBeneficiarios.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidarPolizasBeneficiarios.Name = "btnValidarPolizasBeneficiarios";
            this.btnValidarPolizasBeneficiarios.Size = new System.Drawing.Size(23, 22);
            this.btnValidarPolizasBeneficiarios.Text = "Validar Pólizas";
            this.btnValidarPolizasBeneficiarios.ToolTipText = "Validar polizas";
            this.btnValidarPolizasBeneficiarios.Click += new System.EventHandler(this.btnValidarPolizasBeneficiarios_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
            // btnExportarPDF
            // 
            this.btnExportarPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarPDF.Image")));
            this.btnExportarPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(23, 22);
            this.btnExportarPDF.Text = "Exportar a PDF";
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCancelarRemision
            // 
            this.btnCancelarRemision.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelarRemision.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarRemision.Image")));
            this.btnCancelarRemision.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelarRemision.Name = "btnCancelarRemision";
            this.btnCancelarRemision.Size = new System.Drawing.Size(23, 22);
            this.btnCancelarRemision.Text = "Cancelar remisión completa";
            this.btnCancelarRemision.ToolTipText = "Cancelar remisión completa";
            this.btnCancelarRemision.Click += new System.EventHandler(this.btnCancelarRemision_Click);
            // 
            // FramePrincipal
            // 
            this.FramePrincipal.Controls.Add(this.lblIdConcepto);
            this.FramePrincipal.Controls.Add(this.lblConcepto);
            this.FramePrincipal.Controls.Add(this.label11);
            this.FramePrincipal.Controls.Add(this.dtpFechaRemision);
            this.FramePrincipal.Controls.Add(this.label9);
            this.FramePrincipal.Controls.Add(this.lblRubro);
            this.FramePrincipal.Controls.Add(this.lblEstado);
            this.FramePrincipal.Controls.Add(this.lblIdSubCliente);
            this.FramePrincipal.Controls.Add(this.lblIdCliente);
            this.FramePrincipal.Controls.Add(this.label8);
            this.FramePrincipal.Controls.Add(this.lblSubCliente);
            this.FramePrincipal.Controls.Add(this.lblCliente);
            this.FramePrincipal.Controls.Add(this.label5);
            this.FramePrincipal.Controls.Add(this.label2);
            this.FramePrincipal.Controls.Add(this.lblCancelado);
            this.FramePrincipal.Controls.Add(this.txtFolio);
            this.FramePrincipal.Controls.Add(this.label1);
            this.FramePrincipal.Location = new System.Drawing.Point(10, 25);
            this.FramePrincipal.Name = "FramePrincipal";
            this.FramePrincipal.Size = new System.Drawing.Size(588, 156);
            this.FramePrincipal.TabIndex = 1;
            this.FramePrincipal.TabStop = false;
            this.FramePrincipal.Text = "Datos Generales de Remisión";
            // 
            // lblIdConcepto
            // 
            this.lblIdConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdConcepto.Location = new System.Drawing.Point(81, 69);
            this.lblIdConcepto.Name = "lblIdConcepto";
            this.lblIdConcepto.Size = new System.Drawing.Size(99, 21);
            this.lblIdConcepto.TabIndex = 55;
            this.lblIdConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConcepto
            // 
            this.lblConcepto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConcepto.Location = new System.Drawing.Point(187, 69);
            this.lblConcepto.MostrarToolTip = false;
            this.lblConcepto.Name = "lblConcepto";
            this.lblConcepto.Size = new System.Drawing.Size(388, 21);
            this.lblConcepto.TabIndex = 53;
            this.lblConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(9, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 16);
            this.label11.TabIndex = 54;
            this.label11.Text = "Concepto :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaRemision
            // 
            this.dtpFechaRemision.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaRemision.Enabled = false;
            this.dtpFechaRemision.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaRemision.Location = new System.Drawing.Point(481, 20);
            this.dtpFechaRemision.Name = "dtpFechaRemision";
            this.dtpFechaRemision.Size = new System.Drawing.Size(94, 20);
            this.dtpFechaRemision.TabIndex = 51;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(388, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 52;
            this.label9.Text = "Fecha Remisión :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRubro
            // 
            this.lblRubro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRubro.Location = new System.Drawing.Point(81, 43);
            this.lblRubro.Name = "lblRubro";
            this.lblRubro.Size = new System.Drawing.Size(99, 21);
            this.lblRubro.TabIndex = 50;
            this.lblRubro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(187, 43);
            this.lblEstado.MostrarToolTip = false;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(388, 21);
            this.lblEstado.TabIndex = 49;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIdSubCliente
            // 
            this.lblIdSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdSubCliente.Location = new System.Drawing.Point(81, 122);
            this.lblIdSubCliente.Name = "lblIdSubCliente";
            this.lblIdSubCliente.Size = new System.Drawing.Size(99, 21);
            this.lblIdSubCliente.TabIndex = 48;
            this.lblIdSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdCliente.Location = new System.Drawing.Point(81, 96);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(99, 21);
            this.lblIdCliente.TabIndex = 43;
            this.lblIdCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "Rubro :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCliente
            // 
            this.lblSubCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCliente.Location = new System.Drawing.Point(187, 122);
            this.lblSubCliente.MostrarToolTip = false;
            this.lblSubCliente.Name = "lblSubCliente";
            this.lblSubCliente.Size = new System.Drawing.Size(388, 21);
            this.lblSubCliente.TabIndex = 40;
            this.lblSubCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(187, 96);
            this.lblCliente.MostrarToolTip = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(388, 21);
            this.lblCliente.TabIndex = 6;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 36;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCancelado
            // 
            this.lblCancelado.BackColor = System.Drawing.Color.Transparent;
            this.lblCancelado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCancelado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCancelado.Location = new System.Drawing.Point(187, 19);
            this.lblCancelado.Name = "lblCancelado";
            this.lblCancelado.Size = new System.Drawing.Size(94, 20);
            this.lblCancelado.TabIndex = 32;
            this.lblCancelado.Text = "CANCELADA";
            this.lblCancelado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCancelado.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFolio.Decimales = 2;
            this.txtFolio.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFolio.ForeColor = System.Drawing.Color.Black;
            this.txtFolio.Location = new System.Drawing.Point(81, 19);
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
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "Folio :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameClaves
            // 
            this.FrameClaves.Controls.Add(this.lstDetalles);
            this.FrameClaves.Location = new System.Drawing.Point(12, 182);
            this.FrameClaves.Name = "FrameClaves";
            this.FrameClaves.Size = new System.Drawing.Size(841, 355);
            this.FrameClaves.TabIndex = 4;
            this.FrameClaves.TabStop = false;
            this.FrameClaves.Text = "Detalles de Remisión";
            // 
            // lstDetalles
            // 
            this.lstDetalles.Location = new System.Drawing.Point(14, 19);
            this.lstDetalles.LockColumnSize = false;
            this.lstDetalles.Name = "lstDetalles";
            this.lstDetalles.Size = new System.Drawing.Size(816, 325);
            this.lstDetalles.TabIndex = 6;
            this.lstDetalles.UseCompatibleStateImageBehavior = false;
            // 
            // FrameTotales
            // 
            this.FrameTotales.Controls.Add(this.lblTipoRemision);
            this.FrameTotales.Controls.Add(this.lblTotal);
            this.FrameTotales.Controls.Add(this.lblIva);
            this.FrameTotales.Controls.Add(this.label3);
            this.FrameTotales.Controls.Add(this.label4);
            this.FrameTotales.Controls.Add(this.lblSubTotalGrabado);
            this.FrameTotales.Controls.Add(this.lblSubTotalSinGrabar);
            this.FrameTotales.Controls.Add(this.label7);
            this.FrameTotales.Controls.Add(this.label6);
            this.FrameTotales.Location = new System.Drawing.Point(604, 25);
            this.FrameTotales.Name = "FrameTotales";
            this.FrameTotales.Size = new System.Drawing.Size(249, 156);
            this.FrameTotales.TabIndex = 3;
            this.FrameTotales.TabStop = false;
            this.FrameTotales.Text = "Resumen";
            // 
            // lblTipoRemision
            // 
            this.lblTipoRemision.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoRemision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTipoRemision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoRemision.Location = new System.Drawing.Point(8, 125);
            this.lblTipoRemision.Name = "lblTipoRemision";
            this.lblTipoRemision.Size = new System.Drawing.Size(230, 20);
            this.lblTipoRemision.TabIndex = 49;
            this.lblTipoRemision.Text = "CANCELADA";
            this.lblTipoRemision.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTipoRemision.Visible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotal.Location = new System.Drawing.Point(116, 95);
            this.lblTotal.MostrarToolTip = false;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(122, 21);
            this.lblTotal.TabIndex = 48;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIva
            // 
            this.lblIva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIva.Location = new System.Drawing.Point(116, 69);
            this.lblIva.MostrarToolTip = false;
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(122, 21);
            this.lblIva.TabIndex = 45;
            this.lblIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(64, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "IVA :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(64, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "Total :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalGrabado
            // 
            this.lblSubTotalGrabado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalGrabado.Location = new System.Drawing.Point(116, 44);
            this.lblSubTotalGrabado.MostrarToolTip = false;
            this.lblSubTotalGrabado.Name = "lblSubTotalGrabado";
            this.lblSubTotalGrabado.Size = new System.Drawing.Size(122, 21);
            this.lblSubTotalGrabado.TabIndex = 44;
            this.lblSubTotalGrabado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubTotalSinGrabar
            // 
            this.lblSubTotalSinGrabar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotalSinGrabar.Location = new System.Drawing.Point(116, 18);
            this.lblSubTotalSinGrabar.MostrarToolTip = false;
            this.lblSubTotalSinGrabar.Name = "lblSubTotalSinGrabar";
            this.lblSubTotalSinGrabar.Size = new System.Drawing.Size(122, 21);
            this.lblSubTotalSinGrabar.TabIndex = 41;
            this.lblSubTotalSinGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 16);
            this.label7.TabIndex = 42;
            this.label7.Text = "SubTotal sin Grabar :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 43;
            this.label6.Text = "SubTotal Grabado :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmValidarRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 548);
            this.Controls.Add(this.FrameTotales);
            this.Controls.Add(this.FrameClaves);
            this.Controls.Add(this.FramePrincipal);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmValidarRemisiones";
            this.Text = "Validación de Remisiones";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmValidarRemisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FramePrincipal.ResumeLayout(false);
            this.FramePrincipal.PerformLayout();
            this.FrameClaves.ResumeLayout(false);
            this.FrameTotales.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelarRemision;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.GroupBox FramePrincipal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCancelado;
        private SC_ControlsCS.scTextBoxExt txtFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox FrameClaves;
        private System.Windows.Forms.GroupBox FrameTotales;
        private SC_ControlsCS.scLabelExt lblSubCliente;
        private SC_ControlsCS.scLabelExt lblCliente;
        private SC_ControlsCS.scLabelExt lblSubTotalGrabado;
        private SC_ControlsCS.scLabelExt lblSubTotalSinGrabar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SC_ControlsCS.scListView lstDetalles;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.Label lblIdSubCliente;
        private System.Windows.Forms.Label lblRubro;
        private SC_ControlsCS.scLabelExt lblEstado;
        private SC_ControlsCS.scLabelExt lblTotal;
        private SC_ControlsCS.scLabelExt lblIva;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaRemision;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.Label lblIdConcepto;
        private SC_ControlsCS.scLabelExt lblConcepto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnValidarPolizasBeneficiarios;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label lblTipoRemision;
        private System.Windows.Forms.ToolStripButton btnExportarPDF;
    }
}