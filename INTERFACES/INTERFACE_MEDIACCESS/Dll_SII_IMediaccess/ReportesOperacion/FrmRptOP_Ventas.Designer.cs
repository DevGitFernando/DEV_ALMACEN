namespace Dll_SII_IMediaccess.ReportesOperacion
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
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.txtFarmacia = new SC_ControlsCS.scTextBoxExt();
            this.label8 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtEstado = new SC_ControlsCS.scTextBoxExt();
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
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1547, 25);
            this.toolStripBarraMenu.TabIndex = 3;
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
            this.FrameDatos.Controls.Add(this.lstResultado);
            this.FrameDatos.Location = new System.Drawing.Point(11, 191);
            this.FrameDatos.Margin = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Name = "FrameDatos";
            this.FrameDatos.Padding = new System.Windows.Forms.Padding(4);
            this.FrameDatos.Size = new System.Drawing.Size(1525, 475);
            this.FrameDatos.TabIndex = 4;
            this.FrameDatos.TabStop = false;
            this.FrameDatos.Text = "Resultado";
            // 
            // lstResultado
            // 
            this.lstResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultado.Location = new System.Drawing.Point(15, 23);
            this.lstResultado.Margin = new System.Windows.Forms.Padding(4);
            this.lstResultado.Name = "lstResultado";
            this.lstResultado.Size = new System.Drawing.Size(1499, 441);
            this.lstResultado.TabIndex = 0;
            this.lstResultado.UseCompatibleStateImageBehavior = false;
            // 
            // FrameParametros
            // 
            this.FrameParametros.Controls.Add(this.lblFarmacia);
            this.FrameParametros.Controls.Add(this.txtFarmacia);
            this.FrameParametros.Controls.Add(this.label8);
            this.FrameParametros.Controls.Add(this.lblEstado);
            this.FrameParametros.Controls.Add(this.txtEstado);
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
            this.FrameParametros.Location = new System.Drawing.Point(11, 34);
            this.FrameParametros.Margin = new System.Windows.Forms.Padding(4);
            this.FrameParametros.Name = "FrameParametros";
            this.FrameParametros.Padding = new System.Windows.Forms.Padding(4);
            this.FrameParametros.Size = new System.Drawing.Size(1028, 155);
            this.FrameParametros.TabIndex = 0;
            this.FrameParametros.TabStop = false;
            this.FrameParametros.Text = "Información de Parámetros";
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFarmacia.Location = new System.Drawing.Point(211, 55);
            this.lblFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(807, 26);
            this.lblFarmacia.TabIndex = 64;
            this.lblFarmacia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFarmacia
            // 
            this.txtFarmacia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtFarmacia.Decimales = 2;
            this.txtFarmacia.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtFarmacia.ForeColor = System.Drawing.Color.Black;
            this.txtFarmacia.Location = new System.Drawing.Point(101, 57);
            this.txtFarmacia.Margin = new System.Windows.Forms.Padding(4);
            this.txtFarmacia.MaxLength = 8;
            this.txtFarmacia.Name = "txtFarmacia";
            this.txtFarmacia.PermitirApostrofo = false;
            this.txtFarmacia.PermitirNegativos = false;
            this.txtFarmacia.Size = new System.Drawing.Size(100, 22);
            this.txtFarmacia.TabIndex = 1;
            this.txtFarmacia.Text = "01234567";
            this.txtFarmacia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFarmacia.TextChanged += new System.EventHandler(this.txtFarmacia_TextChanged);
            this.txtFarmacia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFarmacia_KeyDown);
            this.txtFarmacia.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmacia_Validating);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 58);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 20);
            this.label8.TabIndex = 63;
            this.label8.Text = "Farmacia :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEstado.Location = new System.Drawing.Point(211, 23);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(807, 26);
            this.lblEstado.TabIndex = 61;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEstado
            // 
            this.txtEstado.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtEstado.Decimales = 2;
            this.txtEstado.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtEstado.ForeColor = System.Drawing.Color.Black;
            this.txtEstado.Location = new System.Drawing.Point(101, 25);
            this.txtEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstado.MaxLength = 8;
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.PermitirApostrofo = false;
            this.txtEstado.PermitirNegativos = false;
            this.txtEstado.Size = new System.Drawing.Size(100, 22);
            this.txtEstado.TabIndex = 0;
            this.txtEstado.Text = "01234567";
            this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstado.TextChanged += new System.EventHandler(this.txtEstado_TextChanged);
            this.txtEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEstado_KeyDown);
            this.txtEstado.Validating += new System.ComponentModel.CancelEventHandler(this.txtEstado_Validating);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(11, 26);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 20);
            this.label6.TabIndex = 60;
            this.label6.Text = "Estado :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubPro
            // 
            this.lblSubPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubPro.Location = new System.Drawing.Point(740, 119);
            this.lblSubPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubPro.Name = "lblSubPro";
            this.lblSubPro.Size = new System.Drawing.Size(277, 26);
            this.lblSubPro.TabIndex = 58;
            this.lblSubPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubPro
            // 
            this.txtSubPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubPro.Decimales = 2;
            this.txtSubPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubPro.ForeColor = System.Drawing.Color.Black;
            this.txtSubPro.Location = new System.Drawing.Point(631, 119);
            this.txtSubPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubPro.MaxLength = 4;
            this.txtSubPro.Name = "txtSubPro";
            this.txtSubPro.PermitirApostrofo = false;
            this.txtSubPro.PermitirNegativos = false;
            this.txtSubPro.Size = new System.Drawing.Size(100, 22);
            this.txtSubPro.TabIndex = 5;
            this.txtSubPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubPro.TextChanged += new System.EventHandler(this.txtSubPro_TextChanged);
            this.txtSubPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubPro_KeyDown);
            this.txtSubPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubPro_Validating);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(521, 121);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 20);
            this.label7.TabIndex = 57;
            this.label7.Text = "Sub-Programa :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPro
            // 
            this.lblPro.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPro.Location = new System.Drawing.Point(740, 88);
            this.lblPro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPro.Name = "lblPro";
            this.lblPro.Size = new System.Drawing.Size(277, 26);
            this.lblPro.TabIndex = 56;
            this.lblPro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPro
            // 
            this.txtPro.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtPro.Decimales = 2;
            this.txtPro.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtPro.ForeColor = System.Drawing.Color.Black;
            this.txtPro.Location = new System.Drawing.Point(631, 88);
            this.txtPro.Margin = new System.Windows.Forms.Padding(4);
            this.txtPro.MaxLength = 4;
            this.txtPro.Name = "txtPro";
            this.txtPro.PermitirApostrofo = false;
            this.txtPro.PermitirNegativos = false;
            this.txtPro.Size = new System.Drawing.Size(100, 22);
            this.txtPro.TabIndex = 4;
            this.txtPro.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPro.TextChanged += new System.EventHandler(this.txtPro_TextChanged);
            this.txtPro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPro_KeyDown);
            this.txtPro.Validating += new System.ComponentModel.CancelEventHandler(this.txtPro_Validating);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(521, 91);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 55;
            this.label9.Text = "Programa :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubCte
            // 
            this.lblSubCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubCte.Location = new System.Drawing.Point(211, 119);
            this.lblSubCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubCte.Name = "lblSubCte";
            this.lblSubCte.Size = new System.Drawing.Size(277, 26);
            this.lblSubCte.TabIndex = 54;
            this.lblSubCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSubCte
            // 
            this.txtSubCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtSubCte.Decimales = 2;
            this.txtSubCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtSubCte.ForeColor = System.Drawing.Color.Black;
            this.txtSubCte.Location = new System.Drawing.Point(101, 119);
            this.txtSubCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubCte.MaxLength = 4;
            this.txtSubCte.Name = "txtSubCte";
            this.txtSubCte.PermitirApostrofo = false;
            this.txtSubCte.PermitirNegativos = false;
            this.txtSubCte.Size = new System.Drawing.Size(100, 22);
            this.txtSubCte.TabIndex = 3;
            this.txtSubCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSubCte.TextChanged += new System.EventHandler(this.txtSubCte_TextChanged);
            this.txtSubCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubCte_KeyDown);
            this.txtSubCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtSubCte_Validating);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 53;
            this.label5.Text = "Sub-Cliente :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCte
            // 
            this.lblCte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCte.Location = new System.Drawing.Point(211, 88);
            this.lblCte.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCte.Name = "lblCte";
            this.lblCte.Size = new System.Drawing.Size(277, 26);
            this.lblCte.TabIndex = 52;
            this.lblCte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCte
            // 
            this.txtCte.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCte.Decimales = 2;
            this.txtCte.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCte.ForeColor = System.Drawing.Color.Black;
            this.txtCte.Location = new System.Drawing.Point(101, 88);
            this.txtCte.Margin = new System.Windows.Forms.Padding(4);
            this.txtCte.MaxLength = 4;
            this.txtCte.Name = "txtCte";
            this.txtCte.PermitirApostrofo = false;
            this.txtCte.PermitirNegativos = false;
            this.txtCte.Size = new System.Drawing.Size(100, 22);
            this.txtCte.TabIndex = 2;
            this.txtCte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCte.TextChanged += new System.EventHandler(this.txtCte_TextChanged);
            this.txtCte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCte_KeyDown);
            this.txtCte.Validating += new System.ComponentModel.CancelEventHandler(this.txtCte_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 51;
            this.label2.Text = "Cliente :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameTipoDeReporte
            // 
            this.FrameTipoDeReporte.Controls.Add(this.rdoDemanda);
            this.FrameTipoDeReporte.Controls.Add(this.rdoNoSurtido);
            this.FrameTipoDeReporte.Controls.Add(this.rdoDevoluciones);
            this.FrameTipoDeReporte.Controls.Add(this.rdoSalidas);
            this.FrameTipoDeReporte.Location = new System.Drawing.Point(1047, 113);
            this.FrameTipoDeReporte.Margin = new System.Windows.Forms.Padding(4);
            this.FrameTipoDeReporte.Name = "FrameTipoDeReporte";
            this.FrameTipoDeReporte.Padding = new System.Windows.Forms.Padding(4);
            this.FrameTipoDeReporte.Size = new System.Drawing.Size(489, 75);
            this.FrameTipoDeReporte.TabIndex = 2;
            this.FrameTipoDeReporte.TabStop = false;
            this.FrameTipoDeReporte.Text = "Tipo de Reporte";
            // 
            // rdoDemanda
            // 
            this.rdoDemanda.Location = new System.Drawing.Point(369, 33);
            this.rdoDemanda.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDemanda.Name = "rdoDemanda";
            this.rdoDemanda.Size = new System.Drawing.Size(95, 21);
            this.rdoDemanda.TabIndex = 3;
            this.rdoDemanda.TabStop = true;
            this.rdoDemanda.Text = "Demanda";
            this.rdoDemanda.UseVisualStyleBackColor = true;
            // 
            // rdoNoSurtido
            // 
            this.rdoNoSurtido.Location = new System.Drawing.Point(257, 32);
            this.rdoNoSurtido.Margin = new System.Windows.Forms.Padding(4);
            this.rdoNoSurtido.Name = "rdoNoSurtido";
            this.rdoNoSurtido.Size = new System.Drawing.Size(112, 21);
            this.rdoNoSurtido.TabIndex = 2;
            this.rdoNoSurtido.TabStop = true;
            this.rdoNoSurtido.Text = "No Surtido";
            this.rdoNoSurtido.UseVisualStyleBackColor = true;
            // 
            // rdoDevoluciones
            // 
            this.rdoDevoluciones.Location = new System.Drawing.Point(121, 32);
            this.rdoDevoluciones.Margin = new System.Windows.Forms.Padding(4);
            this.rdoDevoluciones.Name = "rdoDevoluciones";
            this.rdoDevoluciones.Size = new System.Drawing.Size(120, 21);
            this.rdoDevoluciones.TabIndex = 1;
            this.rdoDevoluciones.TabStop = true;
            this.rdoDevoluciones.Text = "Devoluciones";
            this.rdoDevoluciones.UseVisualStyleBackColor = true;
            // 
            // rdoSalidas
            // 
            this.rdoSalidas.Location = new System.Drawing.Point(25, 32);
            this.rdoSalidas.Margin = new System.Windows.Forms.Padding(4);
            this.rdoSalidas.Name = "rdoSalidas";
            this.rdoSalidas.Size = new System.Drawing.Size(84, 21);
            this.rdoSalidas.TabIndex = 0;
            this.rdoSalidas.TabStop = true;
            this.rdoSalidas.Text = "Salidas";
            this.rdoSalidas.UseVisualStyleBackColor = true;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.dtpFechaFinal);
            this.FrameFechas.Controls.Add(this.label3);
            this.FrameFechas.Controls.Add(this.label4);
            this.FrameFechas.Controls.Add(this.dtpFechaInicial);
            this.FrameFechas.Location = new System.Drawing.Point(1047, 34);
            this.FrameFechas.Margin = new System.Windows.Forms.Padding(4);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Padding = new System.Windows.Forms.Padding(4);
            this.FrameFechas.Size = new System.Drawing.Size(489, 75);
            this.FrameFechas.TabIndex = 1;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Rango de Fechas";
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFinal.Location = new System.Drawing.Point(327, 32);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaFinal.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(264, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Fin :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(57, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Inicio :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInicial.Location = new System.Drawing.Point(120, 32);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(103, 22);
            this.dtpFechaInicial.TabIndex = 0;
            this.dtpFechaInicial.Value = new System.DateTime(2009, 5, 12, 0, 0, 0, 0);
            // 
            // FrmRptOP_Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1547, 677);
            this.Controls.Add(this.FrameTipoDeReporte);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.FrameParametros);
            this.Controls.Add(this.FrameDatos);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Margin = new System.Windows.Forms.Padding(5);
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
        private System.Windows.Forms.Label lblEstado;
        private SC_ControlsCS.scTextBoxExt txtEstado;
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
        private System.Windows.Forms.Label lblFarmacia;
        private SC_ControlsCS.scTextBoxExt txtFarmacia;
        private System.Windows.Forms.Label label8;
    }
}