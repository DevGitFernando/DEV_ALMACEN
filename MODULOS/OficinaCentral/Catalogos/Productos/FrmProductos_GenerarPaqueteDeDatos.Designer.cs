namespace OficinaCentral.Catalogos
{
    partial class FrmProductos_GenerarPaqueteDeDatos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductos_GenerarPaqueteDeDatos));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesarArchivos = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIdProducto_01_Inicial = new SC_ControlsCS.scTextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdProducto_02_Final = new SC_ControlsCS.scTextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnProcesarArchivos});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(371, 25);
            this.toolStripBarraMenu.TabIndex = 1;
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
            this.btnProcesarArchivos.Click += new System.EventHandler(this.btnProcesarArchivos_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIdProducto_02_Final);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIdProducto_01_Inicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rango de productos";
            // 
            // txtIdProducto_01_Inicial
            // 
            this.txtIdProducto_01_Inicial.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProducto_01_Inicial.Decimales = 2;
            this.txtIdProducto_01_Inicial.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProducto_01_Inicial.ForeColor = System.Drawing.Color.Black;
            this.txtIdProducto_01_Inicial.Location = new System.Drawing.Point(183, 31);
            this.txtIdProducto_01_Inicial.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdProducto_01_Inicial.MaxLength = 8;
            this.txtIdProducto_01_Inicial.Name = "txtIdProducto_01_Inicial";
            this.txtIdProducto_01_Inicial.PermitirApostrofo = false;
            this.txtIdProducto_01_Inicial.PermitirNegativos = false;
            this.txtIdProducto_01_Inicial.Size = new System.Drawing.Size(140, 22);
            this.txtIdProducto_01_Inicial.TabIndex = 3;
            this.txtIdProducto_01_Inicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "IdProducto inicial :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdProducto_02_Final
            // 
            this.txtIdProducto_02_Final.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtIdProducto_02_Final.Decimales = 2;
            this.txtIdProducto_02_Final.EstiloTexto = SC_ControlsCS.EstiloCaptura.FolioNumerico;
            this.txtIdProducto_02_Final.ForeColor = System.Drawing.Color.Black;
            this.txtIdProducto_02_Final.Location = new System.Drawing.Point(183, 61);
            this.txtIdProducto_02_Final.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdProducto_02_Final.MaxLength = 8;
            this.txtIdProducto_02_Final.Name = "txtIdProducto_02_Final";
            this.txtIdProducto_02_Final.PermitirApostrofo = false;
            this.txtIdProducto_02_Final.PermitirNegativos = false;
            this.txtIdProducto_02_Final.Size = new System.Drawing.Size(140, 22);
            this.txtIdProducto_02_Final.TabIndex = 5;
            this.txtIdProducto_02_Final.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "IdProducto final :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmProductos_GenerarPaqueteDeDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 146);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Name = "FrmProductos_GenerarPaqueteDeDatos";
            this.Text = "Generar paquete de datos";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmProductos_GenerarPaqueteDeDatos_Load);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnProcesarArchivos;
        private System.Windows.Forms.GroupBox groupBox1;
        private SC_ControlsCS.scTextBoxExt txtIdProducto_01_Inicial;
        private System.Windows.Forms.Label label1;
        private SC_ControlsCS.scTextBoxExt txtIdProducto_02_Final;
        private System.Windows.Forms.Label label2;
    }
}