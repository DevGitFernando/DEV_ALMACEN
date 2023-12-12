namespace Farmacia.PedidosDeDistribuidor
{
    partial class FrmListadoFoliosRemisiones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoFoliosRemisiones));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FrameTipoDisp = new System.Windows.Forms.GroupBox();
            this.rdoNoAdmon = new System.Windows.Forms.RadioButton();
            this.rdoAdmon = new System.Windows.Forms.RadioButton();
            this.FrameFolios = new System.Windows.Forms.GroupBox();
            this.lstFoliosRemisiones = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDistribuidor = new System.Windows.Forms.Label();
            this.txtIdDistribuidor = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.FrameFechas = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFechaDoc = new System.Windows.Forms.DateTimePicker();
            this.toolStripBarraMenu.SuspendLayout();
            this.FrameTipoDisp.SuspendLayout();
            this.FrameFolios.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FrameFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar,
            this.toolStripSeparator1});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(699, 25);
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
            // FrameTipoDisp
            // 
            this.FrameTipoDisp.Controls.Add(this.rdoNoAdmon);
            this.FrameTipoDisp.Controls.Add(this.rdoAdmon);
            this.FrameTipoDisp.Location = new System.Drawing.Point(12, 73);
            this.FrameTipoDisp.Name = "FrameTipoDisp";
            this.FrameTipoDisp.Size = new System.Drawing.Size(393, 47);
            this.FrameTipoDisp.TabIndex = 1;
            this.FrameTipoDisp.TabStop = false;
            this.FrameTipoDisp.Text = "Tipo Unidades";
            // 
            // rdoNoAdmon
            // 
            this.rdoNoAdmon.BackColor = System.Drawing.Color.Transparent;
            this.rdoNoAdmon.Location = new System.Drawing.Point(213, 19);
            this.rdoNoAdmon.Name = "rdoNoAdmon";
            this.rdoNoAdmon.Size = new System.Drawing.Size(117, 17);
            this.rdoNoAdmon.TabIndex = 1;
            this.rdoNoAdmon.Text = "No Administradas";
            this.rdoNoAdmon.UseVisualStyleBackColor = false;
            // 
            // rdoAdmon
            // 
            this.rdoAdmon.BackColor = System.Drawing.Color.Transparent;
            this.rdoAdmon.Checked = true;
            this.rdoAdmon.Location = new System.Drawing.Point(55, 19);
            this.rdoAdmon.Name = "rdoAdmon";
            this.rdoAdmon.Size = new System.Drawing.Size(108, 17);
            this.rdoAdmon.TabIndex = 0;
            this.rdoAdmon.TabStop = true;
            this.rdoAdmon.Text = "Administradas";
            this.rdoAdmon.UseVisualStyleBackColor = false;
            // 
            // FrameFolios
            // 
            this.FrameFolios.Controls.Add(this.lstFoliosRemisiones);
            this.FrameFolios.Location = new System.Drawing.Point(12, 121);
            this.FrameFolios.Name = "FrameFolios";
            this.FrameFolios.Size = new System.Drawing.Size(675, 322);
            this.FrameFolios.TabIndex = 3;
            this.FrameFolios.TabStop = false;
            this.FrameFolios.Text = "Lista de folios";
            // 
            // lstFoliosRemisiones
            // 
            this.lstFoliosRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFoliosRemisiones.Location = new System.Drawing.Point(10, 19);
            this.lstFoliosRemisiones.Name = "lstFoliosRemisiones";
            this.lstFoliosRemisiones.Size = new System.Drawing.Size(655, 293);
            this.lstFoliosRemisiones.TabIndex = 0;
            this.lstFoliosRemisiones.UseCompatibleStateImageBehavior = false;
            this.lstFoliosRemisiones.DoubleClick += new System.EventHandler(this.lstFoliosRemisiones_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDistribuidor);
            this.groupBox1.Controls.Add(this.txtIdDistribuidor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Distribuidor";
            // 
            // lblDistribuidor
            // 
            this.lblDistribuidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDistribuidor.Location = new System.Drawing.Point(186, 18);
            this.lblDistribuidor.Name = "lblDistribuidor";
            this.lblDistribuidor.Size = new System.Drawing.Size(479, 21);
            this.lblDistribuidor.TabIndex = 9;
            this.lblDistribuidor.Text = "Distribuidor :";
            this.lblDistribuidor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIdDistribuidor
            // 
            this.txtIdDistribuidor.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdDistribuidor.Decimales = 2;
            this.txtIdDistribuidor.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdDistribuidor.ForeColor = System.Drawing.Color.Black;
            this.txtIdDistribuidor.Location = new System.Drawing.Point(80, 18);
            this.txtIdDistribuidor.MaxLength = 4;
            this.txtIdDistribuidor.Name = "txtIdDistribuidor";
            this.txtIdDistribuidor.PermitirApostrofo = false;
            this.txtIdDistribuidor.PermitirNegativos = false;
            this.txtIdDistribuidor.Size = new System.Drawing.Size(100, 20);
            this.txtIdDistribuidor.TabIndex = 0;
            this.txtIdDistribuidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIdDistribuidor.TextChanged += new System.EventHandler(this.txtIdDistribuidor_TextChanged);
            this.txtIdDistribuidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIdDistribuidor_KeyDown);
            this.txtIdDistribuidor.Validating += new System.ComponentModel.CancelEventHandler(this.txtIdDistribuidor_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 22);
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
            this.FrameFechas.Location = new System.Drawing.Point(411, 73);
            this.FrameFechas.Name = "FrameFechas";
            this.FrameFechas.Size = new System.Drawing.Size(276, 47);
            this.FrameFechas.TabIndex = 2;
            this.FrameFechas.TabStop = false;
            this.FrameFechas.Text = "Periodo de revisión";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(38, 19);
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
            this.dtpFechaDoc.Location = new System.Drawing.Point(145, 16);
            this.dtpFechaDoc.Name = "dtpFechaDoc";
            this.dtpFechaDoc.ShowUpDown = true;
            this.dtpFechaDoc.Size = new System.Drawing.Size(87, 20);
            this.dtpFechaDoc.TabIndex = 0;
            this.dtpFechaDoc.Value = new System.DateTime(2012, 7, 27, 0, 0, 0, 0);
            // 
            // FrmListadoFoliosRemisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 449);
            this.Controls.Add(this.FrameFechas);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FrameFolios);
            this.Controls.Add(this.FrameTipoDisp);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmListadoFoliosRemisiones";
            this.Text = "Listado de folios de remisión";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoFoliosRemisiones_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.FrameTipoDisp.ResumeLayout(false);
            this.FrameFolios.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox FrameTipoDisp;
        private System.Windows.Forms.RadioButton rdoNoAdmon;
        private System.Windows.Forms.RadioButton rdoAdmon;
        private System.Windows.Forms.GroupBox FrameFolios;
        private System.Windows.Forms.ListView lstFoliosRemisiones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDistribuidor;
        private SC_ControlsCS.scTextBoxExt txtIdDistribuidor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FrameFechas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFechaDoc;
    }
}