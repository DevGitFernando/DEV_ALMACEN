namespace DllFarmaciaSoft.ReportesOperacion
{
    partial class FrmRptOP_Ventas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRptOP_Ventas));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameDatos = new System.Windows.Forms.GroupBox();
            this.lstResultado = new System.Windows.Forms.ListView();
            this.FrameParametros = new System.Windows.Forms.GroupBox();
            this.lblBeneficiario = new System.Windows.Forms.Label();
            this.txtBeneficiario = new SC_ControlsCS.scTextBoxExt();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSubPro = new System.Windows.Forms.Label();
            this.txtSubPro = new SC_ControlsCS.scTextBoxExt();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPro = new System.Windows.Forms.Label();
            this.txtPro = new SC_ControlsCS.scTextBoxExt();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSubCte = new System.Windows.Forms.Label();
            this.txtSubCte = new SC_ControlsCS.scTextBoxExt();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCte = new System.Windows.Forms.Label();
            this.txtCte = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMascaras = new System.Windows.Forms.CheckBox();
            this.FrameTipoDeReporte = new System.Windows.Forms.GroupBox();
            this.rdoDemanda = new System.Windows.Forms.RadioButton();
            this.rdoNoSurtido = new System.Windows.Forms.RadioButton();
            this.rdoDevoluciones = new System.Windows.Forms.RadioButton();
            this.rdoSalidas = new System.Windows.Forms.RadioButton();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameDatos.SuspendLayout();
            this.FrameParametros.SuspendLayout();
            this.FrameTipoDeReporte.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnExportarExcel,
            this.btnImprimir,
            this.toolStripSeparator4});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1184, 25);
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
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Visible = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // FrameDatos
            // 
            this.FrameDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameDatos.Controls.Add(this.lstResultado);
            this.FrameDatos.Location = new System.Drawing.Point(8, 130);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Size = new System.Drawing.Size(1170, 422);
            this.FrameDatos.TabIndex = 3;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Resultado";
            // 
            // lstResultado
            // 
            this.lstResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultado.Location = new System.Drawing.Point(11, 19);
            this.lstResultado.Name = "lstResultado";
            this.lstResultado.Size = new System.Drawing.Size(1152, 395);
            this.lstResultado.TabIndex = 0;
            this.lstResultado.UseCompatibleStateImageBehavior = false;
            // 
            // FrameParametros
            // 
            this.FrameParametros.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameParametros.Controls.Add(this.lblBeneficiario);
            this.FrameParametros.Controls.Add(this.txtBeneficiario);
            this.FrameParametros.Controls.Add(this.label6);
            this.FrameParametros.Controls.Add(this.lblSubPro);
            this.FrameParametros.Controls.Add(this.txtSubPro);
            this.FrameParametros.Controls.Add(this.label7);
            this.FrameParametros.Controls.Add(this.lblPro);
            this.FrameParametros.Controls.Add(this.txtPro);
            this.FrameParametros.Controls.Add(this.label9);
            this.FrameParametros.Controls.Add(this.lblSubCte);
            this.FrameParametros.Controls.Add(this.txtSubCte);
            this.FrameParametros.Controls.Add(this.label5);
            this.FrameParametros.Controls.Add(this.lblCte);
            this.FrameParametros.Controls.Add(this.txtCte);
            this.FrameParametros.Controls.Add(this.label2);
            this.FrameParametros.Location = new System.Drawing.Point(8, 24);
            this.FrameParametros.Name = "FrameParametros";
            this.FrameParametros.Size = new System.Drawing.Size(887, 106);
            this.FrameParametros.TabIndex = 0;
            this.FrameParametros.TabStop = false;
            this.FrameParametros.Text = "Información de Parámetros";
            // 
            // lblBeneficiario
            // 
            this.lblBeneficiario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBeneficiario.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBeneficiario.Location = new System.Drawing.Point(152, 68);
            this.lblBeneficiario.Name = "lblBeneficiario";
            this.lblBeneficiario.Size = new System.Drawing.Size(724, 21);
            this.lblBeneficiario.TabIndex = 61;
            this.lblBeneficiario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBeneficiario
            // 
            this.txtBeneficiario.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtBeneficiario.Decimales = 2;
            this.txtBeneficiario.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtBeneficiario.ForeColor = System.Drawing.Color.Black;
            this.txtBeneficiario.Location = new System.Drawing.Point(76, 68);
            this.txtBeneficiario.MaxLength = 8;
            this.txtBeneficiario.Name = "txtBeneficiario";
            this.txtBeneficiario.PermitirApostrofo = false;
            this.txtBeneficiario.PermitirNegativos = false;
            this.txtBeneficiario.Size = new System.Drawing.Size(70, 20);
            this.txtBeneficiario.TabIndex = 4;
            this.txtBeneficiario.Text = "01234567";
            this.txtBeneficiario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBeneficiario.TextChanged += new System.EventHandler(this.txtBeneficiario_TextChanged);
            this.txtBeneficiario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBeneficiario_KeyDown);
            this.txtBeneficiario.Validating += new System.ComponentModel.CancelEventHandler(this.txtBeneficiario_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 60;
            this.label6.Text = "Beneficiario :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubPro
            // 
            this.lblSubPro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(596, 43);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(280, 21);
            this.lblSubPro.TabIndex = 58;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(522, 43);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(70, 20);
            this.txtSubPro.TabIndex = 3;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(438, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 16);
            this.label7.TabIndex = 57;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(596, 18);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(280, 21);
            this.lblPro.TabIndex = 56;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(522, 18);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(70, 20);
            this.txtPro.TabIndex = 2;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(438, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 16);
            this.label9.TabIndex = 55;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(152, 43);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(280, 21);
            this.lblSubCte.TabIndex = 54;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(76, 43);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(70, 20);
            this.txtSubCte.TabIndex = 1;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 53;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(152, 18);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(280, 21);
            this.lblCte.TabIndex = 52;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(76, 18);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(70, 20);
            this.txtCte.TabIndex = 0;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMascaras
            // 
            this.chkMascaras.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMascaras.Location = new System.Drawing.Point(156, 0);
            this.chkMascaras.Name = "chkMascaras";
            this.chkMascaras.Size = new System.Drawing.Size(113, 17);
            this.chkMascaras.TabIndex = 4;
            this.chkMascaras.Text = "Aplicar Mascaras";
            this.chkMascaras.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMascaras.UseVisualStyleBackColor = true;
            // 
            // FrameTipoDeReporte
            // 
            this.FrameTipoDeReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameTipoDeReporte.Controls.Add(this.chkMascaras);
            this.FrameTipoDeReporte.Controls.Add(this.rdoDemanda);
            this.FrameTipoDeReporte.Controls.Add(this.rdoNoSurtido);
            this.FrameTipoDeReporte.Controls.Add(this.rdoDevoluciones);
            this.FrameTipoDeReporte.Controls.Add(this.rdoSalidas);
            this.FrameTipoDeReporte.Location = new System.Drawing.Point(901, 24);
            this.FrameTipoDeReporte.Name = "FrameTipoDeReporte";
            this.FrameTipoDeReporte.Size = new System.Drawing.Size(278, 61);
            this.FrameTipoDeReporte.TabIndex = 1;
            this.FrameTipoDeReporte.TabStop = false;
            this.FrameTipoDeReporte.Text = "Tipo de Reporte";
            // 
            // rdoDemanda
            // 
            this.rdoDemanda.Location = new System.Drawing.Point(144, 39);
            this.rdoDemanda.Name = "rdoDemanda";
            this.rdoDemanda.Size = new System.Drawing.Size(92, 17);
            this.rdoDemanda.TabIndex = 3;
            this.rdoDemanda.TabStop = true;
            this.rdoDemanda.Text = "Demanda";
            this.rdoDemanda.UseVisualStyleBackColor = true;
            // 
            // rdoNoSurtido
            // 
            this.rdoNoSurtido.Location = new System.Drawing.Point(42, 39);
            this.rdoNoSurtido.Name = "rdoNoSurtido";
            this.rdoNoSurtido.Size = new System.Drawing.Size(92, 17);
            this.rdoNoSurtido.TabIndex = 2;
            this.rdoNoSurtido.TabStop = true;
            this.rdoNoSurtido.Text = "No Surtido";
            this.rdoNoSurtido.UseVisualStyleBackColor = true;
            // 
            // rdoDevoluciones
            // 
            this.rdoDevoluciones.Location = new System.Drawing.Point(144, 15);
            this.rdoDevoluciones.Name = "rdoDevoluciones";
            this.rdoDevoluciones.Size = new System.Drawing.Size(92, 17);
            this.rdoDevoluciones.TabIndex = 1;
            this.rdoDevoluciones.TabStop = true;
            this.rdoDevoluciones.Text = "Devoluciones";
            this.rdoDevoluciones.UseVisualStyleBackColor = true;
            // 
            // rdoSalidas
            // 
            this.rdoSalidas.Location = new System.Drawing.Point(42, 15);
            this.rdoSalidas.Name = "rdoSalidas";
            this.rdoSalidas.Size = new System.Drawing.Size(92, 17);
            this.rdoSalidas.TabIndex = 0;
            this.rdoSalidas.TabStop = true;
            this.rdoSalidas.Text = "Salidas";
            this.rdoSalidas.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label3);
            this.FrameFechas.Controls.Add(this.label4);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(901, 86);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(278, 44);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(184, 22);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(142, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(18, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(59, 22);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(80, 20);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrmRptOP_Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.FrameTipoDeReporte);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameParametros);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmRptOP_Ventas";
            this.Text = "Ventas";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmRptOP_Ventas_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameDatos.ResumeLayout(false);
            this.FrameParametros.ResumeLayout(false);
            this.FrameParametros.PerformLayout();
            this.FrameTipoDeReporte.ResumeLayout(false);
            this.FrameFechas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.GroupBox FrameDatos;
        private System.Windows.Forms.ListView lstResultado;
        private System.Windows.Forms.GroupBox FrameParametros;
        private System.Windows.Forms.Label lblSubPro;
        private SC_ControlsCS.scTextBoxExt txtSubPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPro;
        private SC_ControlsCS.scTextBoxExt txtPro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSubCte;
        private SC_ControlsCS.scTextBoxExt txtSubCte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCte;
        private SC_ControlsCS.scTextBoxExt txtCte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBeneficiario;
        private SC_ControlsCS.scTextBoxExt txtBeneficiario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox FrameTipoDeReporte;
        private System.Windows.Forms.RadioButton rdoDevoluciones;
        private System.Windows.Forms.RadioButton rdoSalidas;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.RadioButton rdoNoSurtido;
        private System.Windows.Forms.RadioButton rdoDemanda;
        private System.Windows.Forms.CheckBox chkMascaras;
    }
}