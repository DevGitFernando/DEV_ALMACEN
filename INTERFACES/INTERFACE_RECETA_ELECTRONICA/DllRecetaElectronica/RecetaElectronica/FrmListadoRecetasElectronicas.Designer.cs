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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoRecetasElectronicas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecetas = new System.Windows.Forms.Label();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.txtReferencia = new SC_ControlsCS.scTextBoxExt();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMensajes = new System.Windows.Forms.Label();
            this.listviewRecetas = new System.Windows.Forms.ListView();
            this.colSecuencial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFolioReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaReceta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreBeneficiario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreMedico = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecetasElectronicas = new System.Windows.Forms.ToolStripButton();
            this.btnRecetasElectronicas_Especifico = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRecetasElectronicas_EnviarRespuesta = new System.Windows.Forms.ToolStripButton();
            this.btnEstadisticas = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblRecetas);
            this.groupBox1.Controls.Add(this.btnBusqueda);
            this.groupBox1.Controls.Add(this.txtReferencia);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblMensajes);
            this.groupBox1.Controls.Add(this.listviewRecetas);
            this.groupBox1.Location = new System.Drawing.Point(8, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(869, 492);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(543, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 23);
            this.label3.TabIndex = 48;
            this.label3.Text = "Número de recetas :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRecetas
            // 
            this.lblRecetas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecetas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRecetas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecetas.Location = new System.Drawing.Point(723, 436);
            this.lblRecetas.Name = "lblRecetas";
            this.lblRecetas.Size = new System.Drawing.Size(138, 23);
            this.lblRecetas.TabIndex = 47;
            this.lblRecetas.Text = "label3";
            this.lblRecetas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Location = new System.Drawing.Point(665, 15);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(196, 23);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.Text = "Buscar recetas";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // txtReferencia
            // 
            this.txtReferencia.ColorFuenteNegativos = System.Drawing.Color.Red;
            this.txtReferencia.Decimales = 2;
            this.txtReferencia.EstiloTexto = SC_ControlsCS.EstiloCaptura.Texto;
            this.txtReferencia.ForeColor = System.Drawing.Color.Black;
            this.txtReferencia.Location = new System.Drawing.Point(87, 18);
            this.txtReferencia.MaxLength = 50;
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.PermitirApostrofo = false;
            this.txtReferencia.PermitirNegativos = false;
            this.txtReferencia.Size = new System.Drawing.Size(562, 20);
            this.txtReferencia.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(17, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filtro :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMensajes
            // 
            this.lblMensajes.BackColor = System.Drawing.Color.Black;
            this.lblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajes.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMensajes.Location = new System.Drawing.Point(2, 466);
            this.lblMensajes.Name = "lblMensajes";
            this.lblMensajes.Size = new System.Drawing.Size(865, 24);
            this.lblMensajes.TabIndex = 3;
            this.lblMensajes.Text = " Doble clic sobre el renglón para seleccionar";
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
            this.listviewRecetas.HideSelection = false;
            this.listviewRecetas.Location = new System.Drawing.Point(8, 43);
            this.listviewRecetas.Margin = new System.Windows.Forms.Padding(2);
            this.listviewRecetas.Name = "listviewRecetas";
            this.listviewRecetas.Size = new System.Drawing.Size(854, 384);
            this.listviewRecetas.TabIndex = 2;
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
            this.btnNuevo,
            this.toolStripSeparator1,
            this.btnRecetasElectronicas,
            this.btnRecetasElectronicas_Especifico,
            this.toolStripSeparator3,
            this.btnRecetasElectronicas_EnviarRespuesta,
            this.btnEstadisticas,
            this.toolStripSeparator2,
            this.btnImprimir});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(884, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRecetasElectronicas
            // 
            this.btnRecetasElectronicas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetasElectronicas.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetasElectronicas.Image")));
            this.btnRecetasElectronicas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetasElectronicas.Name = "btnRecetasElectronicas";
            this.btnRecetasElectronicas.Size = new System.Drawing.Size(23, 22);
            this.btnRecetasElectronicas.Text = "Descarga masiva de recetas electrónicas";
            this.btnRecetasElectronicas.Click += new System.EventHandler(this.btnRecetasElectronicas_Click);
            // 
            // btnRecetasElectronicas_Especifico
            // 
            this.btnRecetasElectronicas_Especifico.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetasElectronicas_Especifico.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetasElectronicas_Especifico.Image")));
            this.btnRecetasElectronicas_Especifico.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetasElectronicas_Especifico.Name = "btnRecetasElectronicas_Especifico";
            this.btnRecetasElectronicas_Especifico.Size = new System.Drawing.Size(23, 22);
            this.btnRecetasElectronicas_Especifico.Text = "Descargar receta electrónica";
            this.btnRecetasElectronicas_Especifico.Click += new System.EventHandler(this.btnRecetasElectronicas_Especifico_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRecetasElectronicas_EnviarRespuesta
            // 
            this.btnRecetasElectronicas_EnviarRespuesta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRecetasElectronicas_EnviarRespuesta.Image = ((System.Drawing.Image)(resources.GetObject("btnRecetasElectronicas_EnviarRespuesta.Image")));
            this.btnRecetasElectronicas_EnviarRespuesta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecetasElectronicas_EnviarRespuesta.Name = "btnRecetasElectronicas_EnviarRespuesta";
            this.btnRecetasElectronicas_EnviarRespuesta.Size = new System.Drawing.Size(23, 22);
            this.btnRecetasElectronicas_EnviarRespuesta.Text = "Enviar información de recetas atendidas";
            this.btnRecetasElectronicas_EnviarRespuesta.Click += new System.EventHandler(this.btnRecetasElectronicas_EnviarRespuesta_Click);
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEstadisticas.Image = ((System.Drawing.Image)(resources.GetObject("btnEstadisticas.Image")));
            this.btnEstadisticas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(23, 22);
            this.btnEstadisticas.Text = "&Estadisticas";
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 517);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmListadoRecetasElectronicas";
            this.Text = "Recetas electrónicas disponibles para surtido";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmListadoRecetasElectronicas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnRecetasElectronicas_Especifico;
        private System.Windows.Forms.Button btnBusqueda;
        private SC_ControlsCS.scTextBoxExt txtReferencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecetas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRecetasElectronicas_EnviarRespuesta;
        private System.Windows.Forms.ToolStripButton btnEstadisticas;
    }
}