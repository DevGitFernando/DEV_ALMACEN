namespace MA_Facturacion.GenerarRemisiones
{
    partial class FrmPendientesDeRemisionar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPendientesDeRemisionar));
            this.FrameUnidades = new System.Windows.Forms.GroupBox();
            this.lstDetalles = new SC_ControlsCS.scListView();
            this.toolStripBarraMenu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalPiezas = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameUnidades.SuspendLayout();
            this.toolStripBarraMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrameUnidades
            // 
            this.FrameUnidades.Controls.Add(this.lstDetalles);
            this.FrameUnidades.Location = new System.Drawing.Point(9, 29);
            this.FrameUnidades.Name = "FrameUnidades";
            this.FrameUnidades.Size = new System.Drawing.Size(976, 317);
            this.FrameUnidades.TabIndex = 5;
            this.FrameUnidades.TabStop = false;
            this.FrameUnidades.Text = "Detalles de Unidades";
            // 
            // lstDetalles
            // 
            this.lstDetalles.Location = new System.Drawing.Point(12, 19);
            this.lstDetalles.LockColumnSize = false;
            this.lstDetalles.Name = "lstDetalles";
            this.lstDetalles.Size = new System.Drawing.Size(958, 287);
            this.lstDetalles.TabIndex = 6;
            this.lstDetalles.UseCompatibleStateImageBehavior = false;
            // 
            // toolStripBarraMenu
            // 
            this.toolStripBarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripSeparator,
            this.btnEjecutar});
            this.toolStripBarraMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripBarraMenu.Name = "toolStripBarraMenu";
            this.toolStripBarraMenu.Size = new System.Drawing.Size(992, 25);
            this.toolStripBarraMenu.TabIndex = 18;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalPiezas);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(9, 349);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(976, 42);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total";
            // 
            // lblTotalPiezas
            // 
            this.lblTotalPiezas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalPiezas.Location = new System.Drawing.Point(858, 13);
            this.lblTotalPiezas.Name = "lblTotalPiezas";
            this.lblTotalPiezas.Size = new System.Drawing.Size(112, 22);
            this.lblTotalPiezas.TabIndex = 44;
            this.lblTotalPiezas.Text = "Total de piezas :";
            this.lblTotalPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(763, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 17);
            this.label9.TabIndex = 43;
            this.label9.Text = "Total de Piezas :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPendientesDeRemisionar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 404);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStripBarraMenu);
            this.Controls.Add(this.FrameUnidades);
            this.Name = "FrmPendientesDeRemisionar";
            this.Text = "Unidades con Piezas Pendientes de Remisionar";
            this.TituloMensajeValidarControl = "SC_Solutions";
            this.Load += new System.EventHandler(this.FrmPendientesDeRemisionar_Load);
            this.FrameUnidades.ResumeLayout(false);
            this.toolStripBarraMenu.ResumeLayout(false);
            this.toolStripBarraMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FrameUnidades;
        private SC_ControlsCS.scListView lstDetalles;
        private System.Windows.Forms.ToolStrip toolStripBarraMenu;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalPiezas;
    }
}