namespace SII_INT_Layouts.Procesar_Archivos
{
    partial class FrmConvertir_TXT_To_XLS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConvertir_TXT_To_XLS));
            this.FrameResultado = new System.Windows.Forms.GroupBox();
            this.lblTituloMigracion = new System.Windows.Forms.Label();
            this.btnArchivoConvertir = new System.Windows.Forms.Button();
            this.lblDocumentoConvertir = new SC_ControlsCS.scLabelExt();
            this.label3 = new System.Windows.Forms.Label();
            this.lstVwInformacion = new System.Windows.Forms.ListView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEsquema = new System.Windows.Forms.Button();
            this.lblDocumentoEsquema = new SC_ControlsCS.scLabelExt();
            this.label1 = new System.Windows.Forms.Label();
            this.lstEsquema = new System.Windows.Forms.ListView();
            this.esqColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FrameResultado.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameResultado
            // 
            this.FrameResultado.Controls.Add(this.lblTituloMigracion);
            this.FrameResultado.Controls.Add(this.btnArchivoConvertir);
            this.FrameResultado.Controls.Add(this.lblDocumentoConvertir);
            this.FrameResultado.Controls.Add(this.label3);
            this.FrameResultado.Controls.Add(this.lstVwInformacion);
            this.FrameResultado.Location = new System.Drawing.Point(404, 28);
            this.FrameResultado.Name = "FrameResultado";
            this.FrameResultado.Size = new System.Drawing.Size(803, 459);
            this.FrameResultado.TabIndex = 5;
            this.FrameResultado.TabStop = false;
            this.FrameResultado.Text = "Documento a convertir";
            // 
            // lblTituloMigracion
            // 
            this.lblTituloMigracion.Location = new System.Drawing.Point(517, -4);
            this.lblTituloMigracion.Name = "lblTituloMigracion";
            this.lblTituloMigracion.Size = new System.Drawing.Size(276, 18);
            this.lblTituloMigracion.TabIndex = 8;
            this.lblTituloMigracion.Text = "label2";
            this.lblTituloMigracion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnArchivoConvertir
            // 
            this.btnArchivoConvertir.Location = new System.Drawing.Point(766, 16);
            this.btnArchivoConvertir.Name = "btnArchivoConvertir";
            this.btnArchivoConvertir.Size = new System.Drawing.Size(27, 23);
            this.btnArchivoConvertir.TabIndex = 29;
            this.btnArchivoConvertir.Text = "...";
            this.btnArchivoConvertir.UseVisualStyleBackColor = true;
            this.btnArchivoConvertir.Click += new System.EventHandler(this.btnArchivoConvertir_Click);
            // 
            // lblDocumentoConvertir
            // 
            this.lblDocumentoConvertir.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDocumentoConvertir.Location = new System.Drawing.Point(58, 16);
            this.lblDocumentoConvertir.MostrarToolTip = false;
            this.lblDocumentoConvertir.Name = "lblDocumentoConvertir";
            this.lblDocumentoConvertir.Size = new System.Drawing.Size(702, 23);
            this.lblDocumentoConvertir.TabIndex = 28;
            this.lblDocumentoConvertir.Text = "scLabelExt1";
            this.lblDocumentoConvertir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "Ruta :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstVwInformacion
            // 
            this.lstVwInformacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwInformacion.Location = new System.Drawing.Point(10, 42);
            this.lstVwInformacion.Name = "lstVwInformacion";
            this.lstVwInformacion.Size = new System.Drawing.Size(783, 406);
            this.lstVwInformacion.TabIndex = 0;
            this.lstVwInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnEjecutar,
            this.toolStripSeparator2,
            this.btnGuardar,
            this.btnExportarExcel,
            this.toolStripSeparator,
            this.btnSalir,
            this.toolStripSeparator6});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1216, 25);
            this.toolStripBarraMenu.TabIndex = 6;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(23, 22);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "toolStripButton1";
            this.btnExportarExcel.ToolTipText = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEsquema);
            this.groupBox1.Controls.Add(this.lblDocumentoEsquema);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstEsquema);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 459);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Esquema";
            // 
            // btnEsquema
            // 
            this.btnEsquema.Location = new System.Drawing.Point(350, 16);
            this.btnEsquema.Name = "btnEsquema";
            this.btnEsquema.Size = new System.Drawing.Size(27, 23);
            this.btnEsquema.TabIndex = 29;
            this.btnEsquema.Text = "...";
            this.btnEsquema.UseVisualStyleBackColor = true;
            this.btnEsquema.Click += new System.EventHandler(this.btnEsquema_Click);
            // 
            // lblDocumentoEsquema
            // 
            this.lblDocumentoEsquema.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDocumentoEsquema.Location = new System.Drawing.Point(58, 16);
            this.lblDocumentoEsquema.MostrarToolTip = false;
            this.lblDocumentoEsquema.Name = "lblDocumentoEsquema";
            this.lblDocumentoEsquema.Size = new System.Drawing.Size(286, 23);
            this.lblDocumentoEsquema.TabIndex = 28;
            this.lblDocumentoEsquema.Text = "scLabelExt1";
            this.lblDocumentoEsquema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "Ruta :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstEsquema
            // 
            this.lstEsquema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstEsquema.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.esqColumns});
            this.lstEsquema.Location = new System.Drawing.Point(10, 42);
            this.lstEsquema.Name = "lstEsquema";
            this.lstEsquema.Size = new System.Drawing.Size(365, 406);
            this.lstEsquema.TabIndex = 0;
            this.lstEsquema.UseCompatibleStateImageBehavior = false;
            this.lstEsquema.View = System.Windows.Forms.View.Details;
            // 
            // esqColumns
            // 
            this.esqColumns.Text = "Columna";
            this.esqColumns.Width = 320;
            // 
            // FrmConvertir_TXT_To_XLS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameResultado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConvertir_TXT_To_XLS";
            this.ShowInTaskbar = true;
            this.Text = "Convertir archivo de texto a excel";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmConvertir_TXT_To_XLS_Load);
            this.FrameResultado.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameResultado;
        private System.Windows.Forms.ListView lstVwInformacion;
        private SC_ControlsCS.scLabelExt lblDocumentoConvertir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Button btnArchivoConvertir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEsquema;
        private SC_ControlsCS.scLabelExt lblDocumentoEsquema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstEsquema;
        private System.Windows.Forms.ColumnHeader esqColumns;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Label lblTituloMigracion;
        private System.Windows.Forms.ToolStripButton btnGuardar;
    }
}