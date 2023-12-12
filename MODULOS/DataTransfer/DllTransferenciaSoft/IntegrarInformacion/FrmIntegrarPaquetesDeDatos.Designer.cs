namespace DllTransferenciaSoft.IntegrarInformacion
{
    partial class FrmIntegrarPaquetesDeDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIntegrarPaquetesDeDatos));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblRuta = new SC_ControlsCS.scLabelExt();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRutaArchivos = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstArchivos = new System.Windows.Forms.ListView();
            this.colArchivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarArchivos = new System.Windows.Forms.ToolStripButton();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblRuta);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.btnRutaArchivos);
            this.groupBox4.Location = new System.Drawing.Point(8, 26);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(601, 67);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ruta de archivos";
            // 
            // lblRuta
            // 
            this.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRuta.Location = new System.Drawing.Point(12, 37);
            this.lblRuta.MostrarToolTip = false;
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(542, 20);
            this.lblRuta.TabIndex = 1;
            this.lblRuta.Text = "scLabelExt1";
            this.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta de archivos para integración";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRutaArchivos
            // 
            this.btnRutaArchivos.Location = new System.Drawing.Point(559, 38);
            this.btnRutaArchivos.Name = "btnRutaArchivos";
            this.btnRutaArchivos.Size = new System.Drawing.Size(31, 19);
            this.btnRutaArchivos.TabIndex = 2;
            this.btnRutaArchivos.Text = "...";
            this.btnRutaArchivos.UseVisualStyleBackColor = true;
            this.btnRutaArchivos.Click += new System.EventHandler(this.btnRutaArchivos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstArchivos);
            this.groupBox1.Location = new System.Drawing.Point(8, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 291);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paquetes de datos";
            // 
            // lstArchivos
            // 
            this.lstArchivos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colArchivo,
            this.colStatus});
            this.lstArchivos.Location = new System.Drawing.Point(10, 16);
            this.lstArchivos.Name = "lstArchivos";
            this.lstArchivos.Size = new System.Drawing.Size(580, 264);
            this.lstArchivos.TabIndex = 0;
            this.lstArchivos.UseCompatibleStateImageBehavior = false;
            this.lstArchivos.View = System.Windows.Forms.View.Details;
            // 
            // colArchivo
            // 
            this.colArchivo.Text = "Archivo";
            this.colArchivo.Width = 360;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 190;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnProcesarArchivos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(616, 25);
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
            // btnProcesarArchivos
            // 
            this.btnProcesarArchivos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProcesarArchivos.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesarArchivos.Image")));
            this.btnProcesarArchivos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcesarArchivos.Name = "btnProcesarArchivos";
            this.btnProcesarArchivos.Size = new System.Drawing.Size(23, 22);
            this.btnProcesarArchivos.Text = "Procesar";
            this.btnProcesarArchivos.Click += new System.EventHandler(this.btnProcesarTablas_Click);
            // 
            // FrmIntegrarPaquetesDeDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 390);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Name = "FrmIntegrarPaquetesDeDatos";
            this.Text = "Integrar paquetes de datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmIntegrarPaquetesDeDatos_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button btnRutaArchivos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstArchivos;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnProcesarArchivos;
        private System.Windows.Forms.ColumnHeader colArchivo;
        private System.Windows.Forms.ColumnHeader colStatus;
        private SC_ControlsCS.scLabelExt lblRuta;
    }
}