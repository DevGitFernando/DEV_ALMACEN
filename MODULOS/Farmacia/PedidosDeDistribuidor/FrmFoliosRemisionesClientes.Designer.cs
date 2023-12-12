namespace Farmacia.PedidosDeDistribuidor
{
    partial class FrmFoliosRemisionesClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFoliosRemisionesClientes));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.FrameTipoDisp = new System.Windows.Forms.GroupBox();
            this.rdoSinTerminar = new System.Windows.Forms.RadioButton();
            this.rdoTerminadas = new System.Windows.Forms.RadioButton();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstFoliosRemisiones = new System.Windows.Forms.ListView();
            this.mnFolios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnTerminarFolio = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReferenciaDocto = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new SC_ControlsCS.scTextBoxExt();
            this.lblDistribuidor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIdDistribuidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaDoc = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoIndividual = new System.Windows.Forms.RadioButton();
            this.rdoTodos = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoDisp.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.mnFolios.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(696, 25);
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
            this.btnNuevo.Text = "&Nuevo (CTRL + N)";
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
            // FrameTipoDisp
            // 
            this.FrameTipoDisp.Controls.Add(this.rdoSinTerminar);
            this.FrameTipoDisp.Controls.Add(this.rdoTerminadas);
            this.FrameTipoDisp.Location = new System.Drawing.Point(11, 130);
            this.FrameTipoDisp.Name = "FrameTipoDisp";
            this.FrameTipoDisp.Size = new System.Drawing.Size(229, 47);
            this.FrameTipoDisp.TabIndex = 1;
            this.FrameTipoDisp.TabStop = false;
            this.FrameTipoDisp.Text = "Status Folios Remisión";
            // 
            // rdoSinTerminar
            // 
            this.rdoSinTerminar.BackColor = System.Drawing.Color.Transparent;
            this.rdoSinTerminar.Location = new System.Drawing.Point(116, 19);
            this.rdoSinTerminar.Name = "rdoSinTerminar";
            this.rdoSinTerminar.Size = new System.Drawing.Size(87, 17);
            this.rdoSinTerminar.TabIndex = 1;
            this.rdoSinTerminar.Text = "Sin Terminar";
            this.rdoSinTerminar.UseVisualStyleBackColor = false;
            // 
            // rdoTerminadas
            // 
            this.rdoTerminadas.BackColor = System.Drawing.Color.Transparent;
            this.rdoTerminadas.Checked = true;
            this.rdoTerminadas.Location = new System.Drawing.Point(28, 19);
            this.rdoTerminadas.Name = "rdoTerminadas";
            this.rdoTerminadas.Size = new System.Drawing.Size(79, 17);
            this.rdoTerminadas.TabIndex = 0;
            this.rdoTerminadas.TabStop = true;
            this.rdoTerminadas.Text = "Terminadas";
            this.rdoTerminadas.UseVisualStyleBackColor = false;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.lstFoliosRemisiones);
            this.FrameFolios.Location = new System.Drawing.Point(11, 177);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(675, 303);
            this.FrameFolios.TabIndex = 3;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Lista de folios";
            // 
            // lstFoliosRemisiones
            // 
            this.lstFoliosRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFoliosRemisiones.ContextMenuStrip = this.mnFolios;
            this.lstFoliosRemisiones.Location = new System.Drawing.Point(10, 19);
            this.lstFoliosRemisiones.Name = "lstFoliosRemisiones";
            this.lstFoliosRemisiones.Size = new System.Drawing.Size(655, 274);
            this.lstFoliosRemisiones.TabIndex = 0;
            this.lstFoliosRemisiones.UseCompatibleStateImageBehavior = false;
            this.lstFoliosRemisiones.DoubleClick += new System.EventHandler(this.lstFoliosRemisiones_DoubleClick);
            // 
            // mnFolios
            // 
            this.mnFolios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTerminarFolio});
            this.mnFolios.Name = "mnClaves";
            this.mnFolios.Size = new System.Drawing.Size(173, 26);
            // 
            // btnTerminarFolio
            // 
            this.btnTerminarFolio.Name = "btnTerminarFolio";
            this.btnTerminarFolio.Size = new System.Drawing.Size(172, 22);
            this.btnTerminarFolio.Text = "Ver Folio Remisión";
            this.btnTerminarFolio.Click += new System.EventHandler(this.btnTerminarFolio_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReferenciaDocto);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblCliente);
            this.groupBox1.Controls.Add(this.txtCliente);
            this.groupBox1.Controls.Add(this.lblDistribuidor);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtIdDistribuidor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(11, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Distribuidor";
            // 
            // txtReferenciaDocto
            // 
            this.txtReferenciaDocto.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferenciaDocto.Decimales = 2;
            this.txtReferenciaDocto.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferenciaDocto.ForeColor = System.Drawing.Color.Black;
            this.txtReferenciaDocto.Location = new System.Drawing.Point(97, 73);
            this.txtReferenciaDocto.MaxLength = 20;
            this.txtReferenciaDocto.Name = "txtReferenciaDocto";
            this.txtReferenciaDocto.PermitirApostrofo = false;
            this.txtReferenciaDocto.PermitirNegativos = false;
            this.txtReferenciaDocto.Size = new System.Drawing.Size(137, 20);
            this.txtReferenciaDocto.TabIndex = 28;
            this.txtReferenciaDocto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "Ref. Documento :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCliente
            // 
            this.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCliente.Location = new System.Drawing.Point(240, 47);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(425, 21);
            this.lblCliente.TabIndex = 27;
            this.lblCliente.Text = "Proveedor :";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCliente
            // 
            this.txtCliente.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtCliente.Decimales = 2;
            this.txtCliente.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtCliente.ForeColor = System.Drawing.Color.Black;
            this.txtCliente.Location = new System.Drawing.Point(97, 47);
            this.txtCliente.MaxLength = 20;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PermitirApostrofo = false;
            this.txtCliente.PermitirNegativos = false;
            this.txtCliente.Size = new System.Drawing.Size(137, 20);
            this.txtCliente.TabIndex = 1;
            this.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCliente_KeyDown);
            this.txtCliente.Validating += new System.ComponentModel.CancelEventHandler(this.txtCliente_Validating);
            // 
            // lblDistribuidor
            // 
            this.lblDistribuidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistribuidor.Location = new System.Drawing.Point(166, 18);
            this.lblDistribuidor.Name = "lblDistribuidor";
            this.lblDistribuidor.Size = new System.Drawing.Size(499, 21);
            this.lblDistribuidor.TabIndex = 9;
            this.lblDistribuidor.Text = "Distribuidor :";
            this.lblDistribuidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "Cliente Destino :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdDistribuidor
            // 
            this.txtIdDistribuidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDistribuidor.Decimales = 2;
            this.txtIdDistribuidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdDistribuidor.ForeColor = System.Drawing.Color.Black;
            this.txtIdDistribuidor.Location = new System.Drawing.Point(97, 19);
            this.txtIdDistribuidor.MaxLength = 4;
            this.txtIdDistribuidor.Name = "txtIdDistribuidor";
            this.txtIdDistribuidor.PermitirApostrofo = false;
            this.txtIdDistribuidor.PermitirNegativos = false;
            this.txtIdDistribuidor.Size = new System.Drawing.Size(63, 20);
            this.txtIdDistribuidor.TabIndex = 0;
            this.txtIdDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDistribuidor.TextChanged += new System.EventHandler(this.txtIdDistribuidor_TextChanged);
            this.txtIdDistribuidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDistribuidor_KeyDown);
            this.txtIdDistribuidor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDistribuidor_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Distribuidor :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrameFechas
            // 
            this.FrameFechas.Controls.Add(this.label5);
            this.FrameFechas.Controls.Add(this.dtpFechaDoc);
            this.FrameFechas.Location = new System.Drawing.Point(246, 130);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(211, 47);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Periodo de revisión";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Fecha Documento :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFechaDoc
            // 
            this.dtpFechaDoc.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDoc.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaDoc.Location = new System.Drawing.Point(122, 17);
            this.dtpFechaDoc.Name = "dtpFechaDoc";
            this.dtpFechaDoc.ShowUpDown = true;
            this.dtpFechaDoc.Size = new System.Drawing.Size(83, 20);
            this.dtpFechaDoc.TabIndex = 0;
            this.dtpFechaDoc.Value = new System.DateTime(2012, 7, 31, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoIndividual);
            this.groupBox2.Controls.Add(this.rdoTodos);
            this.groupBox2.Location = new System.Drawing.Point(463, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(223, 47);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Reporte Folios";
            // 
            // rdoIndividual
            // 
            this.rdoIndividual.BackColor = System.Drawing.Color.Transparent;
            this.rdoIndividual.Location = new System.Drawing.Point(125, 19);
            this.rdoIndividual.Name = "rdoIndividual";
            this.rdoIndividual.Size = new System.Drawing.Size(87, 17);
            this.rdoIndividual.TabIndex = 1;
            this.rdoIndividual.Text = "Individual";
            this.rdoIndividual.UseVisualStyleBackColor = false;
            // 
            // rdoTodos
            // 
            this.rdoTodos.BackColor = System.Drawing.Color.Transparent;
            this.rdoTodos.Checked = true;
            this.rdoTodos.Location = new System.Drawing.Point(31, 19);
            this.rdoTodos.Name = "rdoTodos";
            this.rdoTodos.Size = new System.Drawing.Size(79, 17);
            this.rdoTodos.TabIndex = 0;
            this.rdoTodos.TabStop = true;
            this.rdoTodos.Text = "Todos";
            this.rdoTodos.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(0, 485);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(696, 24);
            this.label11.TabIndex = 11;
            this.label11.Text = "Click derecho para ver Opciones de Folio de Remisión";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmFoliosRemisionesClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 509);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoDisp);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmFoliosRemisionesClientes";
            this.Text = "Listado de Folios de Remisiones por Clientes";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoFoliosRemisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoDisp.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.mnFolios.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FrameFechas.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox FrameTipoDisp;
        private System.Windows.Forms.RadioButton rdoSinTerminar;
        private System.Windows.Forms.RadioButton rdoTerminadas;
        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstFoliosRemisiones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDistribuidor;
        private SC_ControlsCS.scTextBoxExt txtIdDistribuidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaDoc;
        private System.Windows.Forms.Label lblCliente;
        private SC_ControlsCS.scTextBoxExt txtCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoIndividual;
        private System.Windows.Forms.RadioButton rdoTodos;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private SC_ControlsCS.scTextBoxExt txtReferenciaDocto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip mnFolios;
        private System.Windows.Forms.ToolStripMenuItem btnTerminarFolio;
        private System.Windows.Forms.Label label11;
    }
}