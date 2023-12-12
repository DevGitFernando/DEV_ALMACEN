namespace DllRecetaElectronica.ECE
{
    partial class FrmListadoRecetasElectronicas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoRecetasElectronicas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listviewRecetas = new System.Windows.Forms.ListView();
            this.colSecuencial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreBeneficiario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreMedico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnRecetasElectronicas = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMensajes);
            this.groupBox1.Controls.Add(this.listviewRecetas);
            this.groupBox1.Location = new System.Drawing.Point(11, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1159, 563);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(3, 530);
            this.lblMensajes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(1153, 30);
            this.lblMensajes.TabIndex = 11;
            this.lblMensajes.Text = "Doble clic sobre el renglón para seleccionar";
            this.lblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listviewRecetas
            // 
            this.listviewRecetas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSecuencial,
            this.colFolioReceta,
            this.colFechaReceta,
            this.colNombreBeneficiario,
            this.colNombreMedico});
            this.listviewRecetas.Location = new System.Drawing.Point(10, 16);
            this.listviewRecetas.Name = "listviewRecetas";
            this.listviewRecetas.Size = new System.Drawing.Size(1138, 509);
            this.listviewRecetas.TabIndex = 0;
            this.listviewRecetas.UseCompatibleStateImageBehavior = false;
            this.listviewRecetas.View = System.Windows.Forms.View.Details;
            this.listviewRecetas.DoubleClick += new System.EventHandler(this.listviewRecetas_DoubleClick);
            // 
            // colSecuencial
            // 
            this.colSecuencial.Text = "Secuencial";
            this.colSecuencial.Width = 120;
            // 
            // colFolioReceta
            // 
            this.colFolioReceta.Text = "Folio de receta";
            this.colFolioReceta.Width = 150;
            // 
            // colFechaReceta
            // 
            this.colFechaReceta.Text = "Fecha de receta";
            this.colFechaReceta.Width = 150;
            // 
            // colNombreBeneficiario
            // 
            this.colNombreBeneficiario.Text = "Beneficiario";
            this.colNombreBeneficiario.Width = 340;
            // 
            // colNombreMedico
            // 
            this.colNombreMedico.Text = "Nombre médico preescribe";
            this.colNombreMedico.Width = 340;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRecetasElectronicas,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(1178, 25);
            this.toolStripBarraMenu.TabIndex = 5;
            this.toolStripBarraMenu.Text = "toolStrip1";
            // 
            // btnRecetasElectronicas
            // 
            this.btnRecetasElectronicas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetasElectronicas.Enabled = false;
            this.btnRecetasElectronicas.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetasElectronicas.Image")));
            this.btnRecetasElectronicas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetasElectronicas.Name = "btnRecetasElectronicas";
            this.btnRecetasElectronicas.Size = new System.Drawing.Size(23, 22);
            this.btnRecetasElectronicas.Text = "Receta electrónica";
            this.btnRecetasElectronicas.Click += new System.EventHandler(this.btnRecetasElectronicas_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "&Imprimir (CTRL + P)";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // FrmListadoRecetasElectronicas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 597);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmListadoRecetasElectronicas";
            this.Text = "Recetas electrónicas disponibles para surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoRecetasElectronicas_Load);
            this.groupBox1.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listviewRecetas;
        private System.Windows.Forms.ColumnHeader colFolioReceta;
        private System.Windows.Forms.ColumnHeader colFechaReceta;
        private System.Windows.Forms.ColumnHeader colNombreBeneficiario;
        private System.Windows.Forms.ColumnHeader colNombreMedico;
        private System.Windows.Forms.Label lblMensajes;
        private System.Windows.Forms.ColumnHeader colSecuencial;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnRecetasElectronicas;
        private System.Windows.Forms.ToolStripButton btnImprimir;
    }
}